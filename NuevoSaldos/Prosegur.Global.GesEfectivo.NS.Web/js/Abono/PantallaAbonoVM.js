/// <reference path="../jquery-1.10.1.min.js" />
/// <reference path="../knockout-3.4.0.js" />
/// <reference path="../knockout.mapping-latest.js" />
/// <reference path="Tipos.js" />
/// <reference path="PantallaDetalle.js" />

function PantallaAbonoVM(dados) {
    var self = this;

    //#region Atributos
    self.Abono = ko.observable(new Abono(dados.Abono));
    self.AbonoValorPeloTipo = new AbonoValorElementoOSaldos(self.Abono().TipoAbono());
    self.Diccionarios = dados.Diccionarios;
    self.BKPAbono = new AbonoBkp(self.Abono().TipoAbono(), self.Abono().TipoValor(), self.Abono().BuscaBancos.jsonString(), self.Abono().CrearDocumentoPases());
    self.ListaTiposAbono = ko.observableArray(dados.ListaTiposAbono);
    self.ListaValoresEfectivo = ko.observableArray(dados.ListaValoresEfectivo);
    self.OrdenadorAbonos = new OrdenadorListaAbonoValor(self.Abono().AbonosValor);
    self.ListaTerminosDocPases = null;
    self.TemplateDatosBancarios = {
        peticionDatosBancarios: null,
        listaTipoCuenta: null
    }
    self.DatosBancariosVM = ko.observable();
    self.Usuario = dados.NombreUsuario;
    self.PermiteSaldoNegativo = dados.PermiteSaldoNegativo;
    self.DivasAbonarCompensatorias = ko.observableArray();
    //Filtro
    self.Filtro = ko.observable(ObterFiltroPeloTipoAbono(self.Abono().TipoAbono(), self.ListaTiposAbono()));
    self.ExibirFiltros = ko.observable(false);
    self.OcultarFiltros = ko.observable(false);
    self.ListaResultadoFiltro = ko.observableArray(MapearListaResultadoFiltro(dados.ListaResultadoFiltro));
    self.OrdenadorResultadoFiltros = new OrdenadorListaAbonoValor(self.ListaResultadoFiltro);
    //Detalles
    self.DetalleVM = ko.observable();
    self.ExibirDetallar = ko.observable(false);
    self.OcultarDetallar = ko.observable(false);
    //Datos Documento Pases
    self.DatosDocumentoPasesVM = ko.observable();
    self.ExibirDatosGeneracionDocPases = ko.observable(false);
    self.OcultarDatosGeneracionDocPases = ko.observable(false);
    //Usabilidade
    self.MesageLoading = ko.observable("");
    self.MesageAlerta = ko.observable("");
    self.MesageAlertaJS = ko.observable("");
    self.MesageConfirmacaoJS = ko.observable("");
    self.ResultadoConfirmacaoJS = ko.observable();
    self.MesageAlertaVinculo = "";
    self.MesageAlertaError = ko.observable("");
    self.MesageAlertaErrorDescriptiva = ko.observable("");
    self.AlertRedirectVM = ko.observable();
    //Label's dinâmicas
    self.LabelGridResultadoFiltroCliente = ko.observable("");
    self.LabelDetalleCampoCliente = ko.observable("");
    self.LabelGridAbonosCliente = ko.observable("");
    //#endregion

    //#region Busca de terminos do formulario pases
    self.Abono().CrearDocumentoPases.subscribe(function () {
        self.BKPAbono.CrearDocumentoPases = !self.Abono().CrearDocumentoPases();
        if (!self.ResetandoFiltrosAbono) {
            self.ListaTerminosDocPasesEjecutar();
        }
    });

    self.ListaTerminosDocPasesEjecutar = function () {
        if (self.Abono().CrearDocumentoPases()) {
            if (self.ListaTerminosDocPases && self.ListaTerminosDocPases.length > 0) {
                self.CargarListaTerminosDocPases();
            } else {
                //self.MesageLoading("Buscando terminos"); 
                self.MesageLoading(self.Diccionarios.msg_RetornandoResultadoFiltro);
                llamadaAjax('PantallaAbono.aspx/ListaTerminosDocPases', 'POST', null, self.ListaTerminosDocPasesExito, self.ListaTerminosDocPasesError);
            
            }
        } else {
            self.LimparTerminosDocPases();
        }
    }

    self.ListaTerminosDocPasesExito = function (data) {
        if (data) {
            var respuesta = JSON.parse(data.d);
            if (respuesta.CodigoError == "0" && respuesta.Respuesta != null) {
                self.ListaTerminosDocPases = respuesta.Respuesta;
                self.CargarListaTerminosDocPases();
            }
            else {
                self.Abono().CrearDocumentoPases(false);
                self.MesageAlertaJS(respuesta.MensajeError);
            }
        }

        self.MesageLoading("");
    }

    self.CargarListaTerminosDocPases = function () {
        if (self.ListaTerminosDocPases && self.ListaTerminosDocPases.length > 0) {
            self.ListaTerminosDocPases.forEach(function (terminoIAC) {
                terminoIAC.ValorOriginal = terminoIAC.Valor;
            });

            if (self.Abono().AbonosValor().length > 0) {
                self.Abono().AbonosValor().forEach(function (aValor) {
                    aValor.AbonoSaldo().ListaTerminoIAC(MapearListaTerminosIAC(self.ListaTerminosDocPases));
                });
            }
        }
    }

    self.ListaTerminosDocPasesError = function (request, status, error) {
        self.MesageLoading("");
        self.Abono().CrearDocumentoPases(false);
        self.MesageAlertaErrorDescriptiva(request.responseText);
        self.MesageAlertaError(error);
    }

    self.LimparTerminosDocPases = function () {

        if (self.Abono().AbonosValor().length > 0 && self.Abono().AbonosValor()[0].AbonoSaldo().ListaTerminoIAC() && self.Abono().AbonosValor()[0].AbonoSaldo().ListaTerminoIAC().length > 0) {

            if (ComprobarSiHanCambiadoTerminosValores()) {
                self.ConfirmarLimpezaTerminos()
                return;
            }

            self.Abono().AbonosValor().forEach(function (aValor) {
                aValor.AbonoSaldo().ListaTerminoIAC.removeAll();
            });
        }

        //self.ListaTerminosDocPases = null;
    }

    self.ConfirmarLimpezaTerminos = function () {

        self.MesageConfirmacaoJS(self.Diccionarios.msg_ValidacionTerminosIllenos);

        if (!self.ResultadoConfirmacaoJS()) {
            self.ResetandoFiltrosAbono = true;

            self.Abono().CrearDocumentoPases(self.BKPAbono.CrearDocumentoPases);

            self.ResetandoFiltrosAbono = false;
            return;
        }

        self.BKPAbono.CrearDocumentoPases = self.Abono().CrearDocumentoPases();

        self.Abono().AbonosValor().forEach(function (aValor) {
            aValor.AbonoSaldo().ListaTerminoIAC.removeAll();
        });

        self.ListaTerminosDocPases = null;
    }

    function ComprobarSiHanCambiadoTerminosValores() {
        var terminoValorAlterado = false;

        self.Abono().AbonosValor().forEach(function (aValor) {

            aValor.AbonoSaldo().ListaTerminoIAC().forEach(function (termino) {

                if (termino.Valor() && (termino.Valor() != termino.ValorOriginal())) {
                    terminoValorAlterado = true;
                }
            });
        });

        return terminoValorAlterado;
    }
    //#endregion

    //#region Filtros

    //Modificação do tipo de Abono
    self.TipoAbonoBkp = '';
    self.Abono().TipoAbono.subscribe(function (novoValor) {
        //Verifica modificação dos filtros
        if (!self.ResetandoFiltrosAbono && !self.ConfimarMudancaEstadoFiltro()) return;
        self.ConfigTiposAbono(novoValor);
    });

    self.ConfigTiposAbono = function (novoValor) {

        self.AbonoValorPeloTipo = new AbonoValorElementoOSaldos(novoValor);
        self.Abono().BuscaBancos.jsonString('');
        self.Abono().CrearDocumentoPases(false);
        self.TipoAbonoBkp = novoValor;

        self.ConfigFiltroPeloTipoAbono(true);

        if (self.Abono().TipoAbono() != "Elemento") {
            self.Abono().CrearDocumentoPases(true);
        }
    };

    self.ConfigFiltroPeloTipoAbono = function (cambiouTipo) {
        if (cambiouTipo) {
            //Seta o filtro pelo tipo do Abono
            if (self.Abono().TipoAbono() != "NoDefinido") self.LimparFiltro();
            return;
        } else {
            self.Filtro(ObterFiltroPeloTipoAbono(self.Abono().TipoAbono(), self.ListaTiposAbono()));
        }

        self.ConfigLabelsPantalla();

        //Se Abono por Pedido filtra o saldo do Solicitante selecionado
        if (self.Abono().TipoAbono() == "Pedido") {
            self.Filtro().Clientes(self.Abono().Bancos());
        }
    };

    self.ConfigLabelsPantalla = function (cambiouTipo) {
        switch (self.Abono().TipoAbono()) {
            case "Saldos":
                self.Abono().BuscaBancos.Titulo(self.Diccionarios.Comon_Campo_Banco_Saldos);
                self.LabelGridResultadoFiltroCliente(self.Diccionarios.Abono_Grid_TituloColumna_Cliente_Saldos);
                self.LabelGridAbonosCliente(self.Diccionarios.Abono_Grid_TituloColumna_Cliente_Saldos);
                self.LabelDetalleCampoCliente(self.Diccionarios.Detalle_Campo_Cliente_Saldos);
                self.LabelGridAbonosCliente(self.Diccionarios.Abono_Grid2_TituloColumna_Cliente_Saldos);
                self.Filtro().BuscaClientes.Titulo(self.Diccionarios.Comon_Campo_Cliente_Saldos);
                break;
            case "Pedido":
                self.Abono().BuscaBancos.Titulo(self.Diccionarios.Comon_Campo_Banco_Pedido);
                self.LabelGridResultadoFiltroCliente(self.Diccionarios.Abono_Grid_TituloColumna_Cliente_Pedido);
                self.LabelGridAbonosCliente(self.Diccionarios.Abono_Grid_TituloColumna_Cliente_Pedido);
                self.LabelDetalleCampoCliente(self.Diccionarios.Detalle_Campo_Cliente_Pedido);
                self.LabelGridAbonosCliente(self.Diccionarios.Abono_Grid2_TituloColumna_Cliente_Pedido);
                self.Filtro().BuscaClientes.Titulo(self.Diccionarios.Comon_Campo_Cliente_Pedido);
                break;
            case "Elemento":
                self.Abono().BuscaBancos.Titulo(self.Diccionarios.Comon_Campo_Banco_Elemento);
                self.LabelGridResultadoFiltroCliente(self.Diccionarios.Abono_Grid_TituloColumna_Cliente_Elemento);
                self.LabelGridAbonosCliente(self.Diccionarios.Abono_Grid_TituloColumna_Cliente_Elemento);
                self.LabelDetalleCampoCliente(self.Diccionarios.Detalle_Campo_Cliente_Elemento);
                self.LabelGridAbonosCliente(self.Diccionarios.Abono_Grid2_TituloColumna_Cliente_Elemento);
                self.Filtro().BuscaClientes.Titulo(self.Diccionarios.Comon_Campo_Cliente_Elemento);
                break;
            default:
                self.Abono().BuscaBancos.Titulo("");
                self.LabelGridResultadoFiltroCliente("");
                self.LabelGridAbonosCliente("");
                self.LabelDetalleCampoCliente("");
                self.LabelGridAbonosCliente("");
                if (self.Filtro()) self.Filtro().BuscaClientes.Titulo("");
        }
    };

    //Modificação do banco de Abono
    self.Abono().Bancos.subscribe(function (novoValor) {
        //Verifica modificação dos filtros
        if (!self.ResetandoFiltrosAbono && !self.ConfimarMudancaEstadoFiltro()) return;
        if (novoValor && novoValor != '') {
            self.ConfigFiltroPeloTipoAbono(false);
        }
        self.RetonaResultadoFiltro();
    });

    //Modificação do valor
    self.Abono().TipoValor.subscribe(function (novoValor) {
        //Verifica modificação dos filtros
        if (!self.ResetandoFiltrosAbono && !self.ConfimarMudancaEstadoFiltro()) return;
        self.RetonaResultadoFiltro();
    });

    //Modificação do Filtro - atualiza o Filtro do Tipo Abono
    self.Filtro.subscribe(function (novoValor) {
        var tipoAbonoFiltrado = self.ListaTiposAbono().filter(function (a) {
            return a.Codigo == self.Abono().TipoAbono();
        });
        if ((tipoAbonoFiltrado != null) && (novoValor != null)) {
            tipoAbonoFiltrado[0].Filtro = ko.mapping.toJS(novoValor);
        }
        self.ListaResultadoFiltro.removeAll();
        //self.Abono().AbonosValor.removeAll(); 
    });

    //Informa a exibição dos filtros
    self.FiltrarElementos = function () {
        if (!self.Filtro())
            self.LimparFiltro();

        self.ExibirFiltros(true);
    };

    self.VoltarFiltro = function () {
        self.OcultarFiltros(true);
    }

    self.Buscar = function () {
        self.RetonaResultadoFiltro();
    }

    //Indica se possui os dados do cabeçalho do Abono e Filtro pelo tipo
    self.VerificaPossuiCabecalhoAbono = function () {
        return ((self.Abono().Bancos().length > 0) && (self.Filtro() != null) &&
            (((self.Abono().TipoAbono() == "Elemento") && (self.Abono().TipoValor() != "NoDefinido")) ||
            ((self.Abono().TipoAbono() != "NoDefinido"))));
    };
    self.PossuiCabecalhoAbono = ko.computed(self.VerificaPossuiCabecalhoAbono);

    //Indica se permite filtrar
    self.VerificaPermiteFiltrar = function () {
        return ((self.Abono().Bancos().length > 0) &&
            (((self.Abono().TipoAbono() == "Elemento") && (self.Abono().TipoValor() != "NoDefinido")) ||
            ((self.Abono().TipoAbono() != "NoDefinido"))));
    };
    self.PermiteFiltrar = ko.computed(self.VerificaPermiteFiltrar);

    //Informa a modificação dos valores de busca sem necessidade de realizar a busca
    self.ResetandoFiltrosAbono = false;
    //Executa a busca dos dados
    self.RetonaResultadoFiltro = function () {
        if (self.ResetandoFiltrosAbono) return;

        //Verifica o preenchimento dos campos necessários para busca
        if (self.VerificaPossuiCabecalhoAbono()) {
            self.MesageLoading(self.Diccionarios.msg_RetornandoResultadoFiltro);

            //Informa os elementos já vinculados para filtro
            if ((self.Filtro()) && (self.Abono().TipoAbono() == "Elemento")) {
                self.Filtro().IdentificadoresElementosSeleccionados.removeAll();
                for (var i = 0; i < self.Abono().AbonosValor().length; i++) {
                    self.Filtro().IdentificadoresElementosSeleccionados.push(self.Abono().AbonosValor()[i].AbonoElemento().IdentificadorRemesa());
                }
            }

            llamadaAjax('PantallaAbono.aspx/RetornaResultadoFiltro', 'POST', "{filtroJson: '" + ((self.Filtro()) ? ko.mapping.toJSON(self.Filtro()) : '') + "', abonoJson: '" + ko.mapping.toJSON(self.Abono()) + "'}",
                self.RetonaResultadoFiltroSuccess, self.RetonaResultadoFiltroError);

        } else {
            self.ListaResultadoFiltro.removeAll();
        }
    };

    self.RetonaResultadoFiltroSuccess = function(data, text){
        var respuesta = JSON.parse(data.d);
        if (respuesta.CodigoError == "0" && respuesta.Respuesta != null) {
            self.Filtro(new Filtro(respuesta.Respuesta.Filtro));
            self.OcultarFiltros(true);
            self.ListaResultadoFiltro(MapearListaResultadoFiltro(respuesta.Respuesta.ListaResultado));
            self.DetallarAbonosValoresSaldosCasoExistam();
            self.MesageLoading("");
        } else {
            self.MesageAlertaErrorDescriptiva(respuesta.MensajeErrorDescriptiva);
            self.MesageAlertaError(respuesta.MensajeError);
        }
    }

    self.RetonaResultadoFiltroError = function(request, status, error){
        self.MesageAlertaErrorDescriptiva(request.responseText);
        self.MesageAlertaError(error);
    } 

    self.ConfimarMudancaEstadoFiltro = function () {
        if (self.Abono().AbonosValor().length > 0) {
            //Confirmação de alteração dos filtros para Abono com AbonoValor
            self.MesageConfirmacaoJS(self.Diccionarios.msg_ValidacaoFiltro);
            if (!self.ResultadoConfirmacaoJS()) {
                //FALSE: Retorna os dados anteriores
                self.ResetandoFiltrosAbono = true;
                self.Abono().BuscaBancos.jsonString(self.BKPAbono.jsonBanco);
                self.Abono().TipoAbono(self.BKPAbono.TipoAbono);
                self.Abono().TipoValor(self.BKPAbono.TipoValor);
                self.Abono().CrearDocumentoPases(self.BKPAbono.CrearDocumentoPases);
                self.ResetandoFiltrosAbono = false;
                return false;
            };
            //Limpa os abonos valores

            //self.Abono().AbonosValor.removeAll();
            while (self.Abono().AbonosValor().length > 0) {
                self.EliminarAbono(self.Abono().AbonosValor()[0]);
            }
        }
        //Sempre guarda backup dos dados do Abono
        self.BKPAbono.jsonBanco = self.Abono().BuscaBancos.jsonString();
        self.BKPAbono.TipoAbono = self.Abono().TipoAbono();
        self.BKPAbono.TipoValor = self.Abono().TipoValor();
        self.BKPAbono.CrearDocumentoPases = self.Abono().CrearDocumentoPases();

        return true;
    }

    self.SeleccionarTodasDivisasElegiveis = function () {
        self.MarcarODesmarcarDivasOValoresElegiveis(self.Filtro().DivisasElegiveis(), true);
    }

    self.RemoverSeleccionTodasDivisasElegiveis = function () {
        self.MarcarODesmarcarDivasOValoresElegiveis(self.Filtro().DivisasElegiveis(), false);
    }

    self.SeleccionarTodosValoresElegiveis = function (valorBooleano) {
        self.MarcarODesmarcarDivasOValoresElegiveis(self.Filtro().ValoresElegiveis(), true);
    }

    self.RemoverSeleccionTodasValoresElegiveis = function () {
        self.MarcarODesmarcarDivasOValoresElegiveis(self.Filtro().ValoresElegiveis(), false);
    }

    self.MarcarODesmarcarDivasOValoresElegiveis = function (listaItens, valorBooleano) {
        if (listaItens != null && listaItens.length > 0) {
            $.each(listaItens, function (lista, item) {
                item.Elegivel(valorBooleano);
            });
        }
    }

    //Limpa o filtro
    self.LimparFiltro = function (esLimpiar) {

        if (esLimpiar == null) {
            esLimpiar = false;
        }

        jQuery.ajax({
            url: 'PantallaAbono.aspx/LimparFiltro',
            contentType: "application/json; charset=utf-8",
            type: "POST",
            dataType: "json",
            data: "{abonoJson: '" + ko.mapping.toJSON(self.Abono()) + "', esLimpiar:" + esLimpiar + "}",
            success: function (data, text) {
                var respuesta = JSON.parse(data.d);
                if (respuesta.CodigoError == "0" && respuesta.Respuesta != null) {
                    //self.Filtro(new Filtro(respuesta.Respuesta))
                    SetarFiltroPeloTipoAbono(respuesta.Respuesta, self.ListaTiposAbono());
                    self.ConfigFiltroPeloTipoAbono(false);
                } else {
                    self.MesageAlertaErrorDescriptiva(respuesta.MensajeErrorDescriptiva);
                    self.MesageAlertaError(respuesta.MensajeError);
                }
            },
            error: function (request, status, error) {
                self.MesageAlertaErrorDescriptiva(request.responseText);
                self.MesageAlertaError(error);
            }
        });
    }

    self.Filtro.subscribe(function(novoValor) {
        self.ConfigLabelsPantalla();
    });
    //#endregion

    //#region Vinculo
    //Vincula um item filtrado ao abono
    self.VincularAbono = function (abonoValor, evento) {
        self.MesageLoading(self.Diccionarios.msg_VinculandoAbono);

        //Filtrar as divisas do mesmo CodigoElemento (NumeroExterno ou Precinto)
        var divisasAbonar = self.ListaResultadoFiltro().filter(function (a) {
            return self.AbonoValorPeloTipo.Filtrar(a, abonoValor);
        });

        var identificadorSolicitante = '';
        if (self.Abono().TipoAbono() != "Pedido") {
            identificadorSolicitante = self.Abono().Bancos()[0].Identificador();
        }

        jQuery.ajax({
            url: 'PantallaAbono.aspx/AbonarDivisa',
            contentType: "application/json; charset=utf-8",
            type: "POST",
            dataType: "json",
            data: "{abonosJson: '" + ko.mapping.toJSON(divisasAbonar) +
                "', todos: false, tipoAbonoStr: '" + self.Abono().TipoAbono() +
                "', identificadorSolicitante: '" + identificadorSolicitante + "'}",
            success: function (data, text) {
                var respuesta = JSON.parse(data.d);
                if (respuesta.CodigoError == "0" && respuesta.Respuesta != null) {
                    var abonoValorVinculado = new AbonoValor(respuesta.Respuesta[0]);
                    var abonoValorExistente = self.Abono().AbonosValor().filter(function (item) {
                        return self.AbonoValorPeloTipo.Filtrar(item, abonoValorVinculado);
                    })[0];

                    var informarOtraCuenta = true;
                    if (!abonoValorExistente) {
                        self.ConfigVincularAbono(divisasAbonar, respuesta.Respuesta);
                        informarOtraCuenta = false;
                    }

                    self.DetallarAbonoValor(abonoValorVinculado, informarOtraCuenta);
                    self.MesageLoading("");
                    //Verifica mensagem de validação dos dados de Cuenta
                    if (respuesta.MensajeError) {
                        self.MesageAlertaJS(respuesta.MensajeError);
                    }
                } else {
                    self.MesageAlertaErrorDescriptiva(respuesta.MensajeErrorDescriptiva);
                    self.MesageAlertaError(respuesta.MensajeError);
                }
            },
            error: function (request, status, error) {
                self.MesageAlertaErrorDescriptiva(request.responseText);
                self.MesageAlertaError(error);
            }
        });
    };

    //Vincula todos os itens filtrados ao abono
    self.VincularTodosAbonos = function () {
        self.MesageLoading(self.Diccionarios.msg_VinculandoAbonos);

        //Filtrar as divisas do mesmo CodigoElemento (NumeroExterno ou Precinto)
        var divisasAbonar = self.ListaResultadoFiltro().filter(function (a) {
            return (true);
        });

        jQuery.ajax({
            url: 'PantallaAbono.aspx/AbonarDivisa',
            contentType: "application/json; charset=utf-8",
            type: "POST",
            dataType: "json",
            data: "{abonosJson: '" + ko.mapping.toJSON(divisasAbonar) + "', todos: true, tipoAbonoStr: '" + self.Abono().TipoAbono() + "'}",
            success: function (data, text) {
                var respuesta = JSON.parse(data.d);
                if (respuesta.CodigoError == "0" && respuesta.Respuesta != null) {
                    self.ConfigVincularAbono(divisasAbonar, respuesta.Respuesta);
                    self.MesageLoading("");
                    //Verifica mensagem de validação dos dados de Cuenta
                    if (respuesta.MensajeError) {
                        self.MesageAlertaJS(respuesta.MensajeError);
                    }
                } else {
                    self.MesageAlertaErrorDescriptiva(respuesta.MensajeErrorDescriptiva);
                    self.MesageAlertaError(respuesta.MensajeError);
                }
            },
            error: function (request, status, error) {
                self.MesageAlertaErrorDescriptiva(request.responseText);
                self.MesageAlertaError(error);
            }
        });
    };

    //Centraliza a regra de abonar divisa
    self.ConfigVincularAbono = function (divisasAbonar, respuesta) {
        for (var i = 0; i < respuesta.length; i++) {
            var abonoValorAbonar = divisasAbonar.filter(function (a) {
                return self.AbonoValorPeloTipo.FiltrarPorDivisa(a, respuesta[i], true);
            })[0];
            if (abonoValorAbonar) {
                //Remove da lista de vinculo
                self.ListaResultadoFiltro.remove(abonoValorAbonar);

                //Adiciona para historico senão existir
                var snap = self.Abono().SnapshotsAbonoSaldo().filter(function (a) {
                    return self.AbonoValorPeloTipo.FiltrarPorDivisa(a, abonoValorAbonar, true);
                });
                if (snap.length == 0) {
                    //self.HistoricoAbonos.push(abonoValorAbonar);
                    self.Abono().SnapshotsAbonoSaldo.push(abonoValorAbonar);
                }
            }

            //Configura AbonoValor
            var abonoValorVincular = new AbonoValor(respuesta[i]);
            //Seta conta bancária default se houver alguma
            if ((abonoValorVincular.CuentasDisponibles() != null) && (abonoValorVincular.CuentasDisponibles().length > 0)) {
                //Seleciona primeiro banco com conta padrão
                var bancoCuentaDefecto = abonoValorVincular.CuentasDisponibles().filter(function (b) {
                    return b.DatosBancarios().filter(function (c) {
                        return (c.BolDefecto() == true);
                    }).length > 0;
                })[0];
                //Se não encontrou pega o primeiro banco
                if (bancoCuentaDefecto == null) {
                    bancoCuentaDefecto = abonoValorVincular.CuentasDisponibles()[0];
                }
                //Seta o banco no AbonoValor
                abonoValorVincular.BancoCuenta(bancoCuentaDefecto);
                //Seleciona a primeira conta padrão do banco
                var cuentaDefecto = abonoValorVincular.BancoCuenta().DatosBancarios().filter(function (c) {
                    return (c.BolDefecto() == true);
                })[0];
                //Se não encontrou pega a primeira conta do banco
                if (cuentaDefecto == null) {
                    cuentaDefecto = abonoValorVincular.BancoCuenta().DatosBancarios()[0];
                }
                //Seta o banco no AbonoValor
                abonoValorVincular.Cuenta(cuentaDefecto);
            }

            if ((self.Abono().TipoAbono() != "Elemento")) {
                //Seta lista de terminos
                abonoValorVincular.AbonoSaldo().ListaTerminoIAC(MapearListaTerminosIAC(self.ListaTerminosDocPases));
            }
            //Adiciona o AbonoValor vindo do servidor com a Divisa configurada para o Abono
            self.Abono().AbonosValor.push(abonoValorVincular);
        }
    }

    //Elimina o item do abono
    self.EliminarAbono = function (abonoValor, evento) {
        if ((typeof (evento) != "undefined")) {
            self.MesageLoading(self.Diccionarios.msg_EliminandoAbono);
        }

        if (self.Abono().TipoAbono() == "Elemento") {
            self.EliminarAbonoValoresElemento(abonoValor, evento);
        }
        else {
            self.EliminarAbonosSaldoEPedido(abonoValor);
        }
    };

    self.EliminarAbonoValoresElemento = function (abonoValor, evento) {
        //Filtrar os abonos do mesmo CodigoElemento (NumeroExterno ou Precinto)
        var abonosValorRemover = self.Abono().AbonosValor().filter(function (a) {
            return self.AbonoValorPeloTipo.Filtrar(a, abonoValor);
        });
        //Remove todos os abonos do mesmo CodigoElemento (NumeroExterno ou Precinto)
        self.Abono().AbonosValor.removeAll(abonosValorRemover);
        //Filtra o abono do historico
        //var abonosHistorico = self.HistoricoAbonos().filter(function (h) {
        var abonosHistorico = self.Abono().SnapshotsAbonoSaldo().filter(function (h) {
            return self.AbonoValorPeloTipo.Filtrar(h, abonoValor);
        });
        if (abonosHistorico.length > 0) {
            //Remove do histórico e volta para vincular
            //self.HistoricoAbonos.removeAll(abonosHistorico);
            self.Abono().SnapshotsAbonoSaldo.removeAll(abonosHistorico);
            for (var i = 0; i < abonosHistorico.length; i++) {
                self.ListaResultadoFiltro.push(abonosHistorico[i]);
            }
        } else {
            $.each(abonosValorRemover, function (i, a) {
                var eliminouAbono = self.ListaResultadoFiltro().filter(function (f) {
                    return self.AbonoValorPeloTipo.FiltrarPorDivisa(f, a);
                }).length > 0;

                if (!eliminouAbono) self.ListaResultadoFiltro.push(a);
            });
        }


        if ((typeof (evento) != "undefined")) {
            self.MesageLoading("");
        }
    }

    self.EliminarAbonosSaldoEPedido = function (abonoValor) {
        var esConfirmacion = self.Abono().AbonosValor().filter(function (s) {
            return s.AbonoSaldo().IdentificadorSnapshot() == abonoValor.AbonoSaldo().IdentificadorSnapshot();
        });
        self.Abono().AbonosValor.remove(abonoValor);
        self.DetallarDivisaAbonar(abonoValor, self.Abono().AbonosValor(), (esConfirmacion.length > 1));
        self.MesageLoading("");
    }

    self.BorrarSnapShot = function (abonoValor) {
        self.Abono().SnapshotsAbonoSaldo.removeAll(self.Abono().SnapshotsAbonoSaldo().filter(function (a) {
            return self.AbonoValorPeloTipo.Filtrar(a, abonoValor);
        }));
    }

    self.CalcularDiferenciaImporteDivisaAbonarEAbonosValores = function (divisaAbonar, abonosValores) {
        var somaImporteAbonosValores = 0;

        listaResultado = abonosValores.filter(function (item) {
            return (item.AbonoSaldo().IdentificadorSnapshot() == divisaAbonar.AbonoSaldo().IdentificadorSnapshot());
        });

        if (listaResultado != null) {
            $.each(listaResultado, function (iabv, abv) {
                somaImporteAbonosValores += ObtenerValorNumerico(abv.Importe());
            });

            divisaAbonar.AbonoSaldo().Importe(SubtrairValoresTolerenacia2(divisaAbonar.AbonoSaldo().Importe(), somaImporteAbonosValores));
        }
    }

    self.DetallarDivisaAbonar = function (abonoValorSelecionado, listaAbonosValores, esConfimacion) {
        ////1 - remover DIVISA_ABONAR de mesmo IdentificadorSnapShot
        var divisaAbobarLista = self.ListaResultadoFiltro.remove(function (a) {
            return a.AbonoSaldo().IdentificadorSnapshot() == abonoValorSelecionado.AbonoSaldo().IdentificadorSnapshot();
        });

        ////2 - gerar nova DIVISA_ABONAR pela diferença SNAPSHOT - ABONO_VALOR(s)
        var abonosHistorico = self.Abono().SnapshotsAbonoSaldo().filter(function (s) {
            return s.AbonoSaldo().IdentificadorSnapshot() == abonoValorSelecionado.AbonoSaldo().IdentificadorSnapshot();
        });

        if (!esConfimacion) {
            if (abonosHistorico.length <= 0) return;
            self.Abono().SnapshotsAbonoSaldo.removeAll(abonosHistorico);
        }

        var divisaAbonar = new AbonoValor(ko.mapping.toJS(abonosHistorico[0]));//Clone

        //#region Atualiza totais

        /*divisaAbonar.AbonoSaldo().Divisa().Totales().TotalCheque(self.AbonoValorSelecionado().Divisa().Totales().TotalesAbonoDisponible.TotalChequeDisponible());
        divisaAbonar.AbonoSaldo().Importe(divisaAbonar.AbonoSaldo().Importe() + divisaAbonar.AbonoSaldo().Divisa().Totales().TotalCheque());
        divisaAbonar.AbonoSaldo().Divisa().Totales().TotalEfectivo(self.AbonoValorSelecionado().Divisa().Totales().TotalesAbonoDisponible.TotalEfectivoDisponible());
        divisaAbonar.AbonoSaldo().Importe(divisaAbonar.AbonoSaldo().Importe() + divisaAbonar.AbonoSaldo().Divisa().Totales().TotalEfectivo());
        divisaAbonar.AbonoSaldo().Divisa().Totales().TotalOtroValor(self.AbonoValorSelecionado().Divisa().Totales().TotalesAbonoDisponible.TotalOtroValorDisponible());
        divisaAbonar.AbonoSaldo().Importe(divisaAbonar.AbonoSaldo().Importe() + divisaAbonar.AbonoSaldo().Divisa().Totales().TotalOtroValor());
        divisaAbonar.AbonoSaldo().Divisa().Totales().TotalTarjeta(self.AbonoValorSelecionado().Divisa().Totales().TotalesAbonoDisponible.TotalTarjetaDisponible());
        divisaAbonar.AbonoSaldo().Importe(divisaAbonar.AbonoSaldo().Importe() + divisaAbonar.AbonoSaldo().Divisa().Totales().TotalTarjeta());
        divisaAbonar.AbonoSaldo().Divisa().Totales().TotalTicket(self.AbonoValorSelecionado().Divisa().Totales().TotalesAbonoDisponible.TotalTicketDisponible());
        divisaAbonar.AbonoSaldo().Importe(divisaAbonar.AbonoSaldo().Importe() + divisaAbonar.AbonoSaldo().Divisa().Totales().TotalTicket());
    
        ////Atualiza Efectivos
        $.each(divisaAbonar.AbonoSaldo().Divisa().ListaEfectivo(), function (ie, efect) {
            efect.Cantidad(efect.EfectivoDisponible.CantidadDisponible());
            efect.Importe(efect.EfectivoDisponible.ValorDisponible());
            divisaAbonar.AbonoSaldo().Importe(divisaAbonar.AbonoSaldo().Importe() + efect.EfectivoDisponible.ValorDisponible());
        });
    
        ////Atualiza Medio Pagos
        $.each(divisaAbonar.AbonoSaldo().Divisa().ListaMedioPago(), function (imp, mp) {
            mp.Cantidad(mp.MedioPagoDisponible.CantidadDisponible());
            mp.Importe(mp.MedioPagoDisponible.ValorDisponible());
            divisaAbonar.AbonoSaldo().Importe(divisaAbonar.AbonoSaldo().Importe() + mp.MedioPagoDisponible.ValorDisponible());
        });*/
        //#endregion

        self.CalcularDiferenciaImporteDivisaAbonarEAbonosValores(divisaAbonar, listaAbonosValores);

        if (divisaAbonar.AbonoSaldo().Importe() != 0) {

            divisaAbonar.DivisaContieneResto(divisaAbonar.AbonoSaldo().Importe() != ObtenerValorNumerico(abonosHistorico[0].AbonoSaldo().Importe()))
            self.ListaResultadoFiltro.push(divisaAbonar);
        }
    }

    //Elimina todos os itens do abono
    self.EliminarTodosAbonos = function () {
        self.MesageLoading(self.Diccionarios.msg_EliminandoAbonos);

        while (self.Abono().AbonosValor().length > 0) {
            self.EliminarAbono(self.Abono().AbonosValor()[0]);
        }

        self.MesageLoading("");
    };
    //#endregion

    //Informa a exibição dos detalhes de um item do abono
    self.DetallarAbonoValor = function (abonoValorDetallar, informarOtraCuenta) {
        self.DetalleVM(new DetalleVM(self, abonoValorDetallar, informarOtraCuenta));
        self.ExibirDetallar(true);
    };

    //#region Persistencia

    //Validaciones
    self.VerificarSeHaErrosAoGrabarAbono = function () {

        var erro = false

        self.Abono().AbonosValor().forEach(function (av) {
            if (!av.Cuenta().CodigoCuentaBancaria() || av.CuentasDisponibles().length == 0) {
                self.MesageAlertaJS(self.Diccionarios.msg_ListaCuentasOCuentaVacia);
                erro = true;
            }
        });

        return erro;
    }

    //Grava o abono
    self.Grabar = function () {

        if (self.VerificarSeHaErrosAoGrabarAbono())
            return;

        self.MesageLoading(self.Diccionarios.msg_GrabandoAbono);

        var jsonData = "{abonoJson: " + ko.mapping.toJSON(self.Abono()) + "}";

        jQuery.ajax({
            url: 'PantallaAbono.aspx/GrabarAbono',
            contentType: "application/json; charset=utf-8",
            type: "POST",
            dataType: "json",
            data: jsonData,
            success: function (data, text) {
                var respuesta = JSON.parse(data.d);
                if (respuesta.CodigoError == "0" && respuesta.Respuesta != null) {
                    self.MesageLoading("");
                    self.Abono(new Abono(respuesta.Respuesta));
                    self.MesageAlertaJS(self.Diccionarios.msg_GrabadoElAbono);
                } else {
                    self.MesageAlertaErrorDescriptiva(respuesta.MensajeErrorDescriptiva);
                    self.MesageAlertaError(respuesta.MensajeError);
                }
            },
            error: function (request, status, error) {
                self.MesageAlertaErrorDescriptiva(request.responseText);
                self.MesageAlertaError(error);
            }
        });
    };

    //Grava e finaliza o abono
    self.GenerarDocumentoPases = function () {

        if (self.VerificarSeHaErrosAoGrabarAbono())
            return;

        //self.DatosDocumentoPasesVM(new DatosGeneracionDocumentoPasesVM(self));
        //self.DatosDocumentoPasesVM().PreencherDatosEnAbonoSaldo.subscribe(function (novoValor) {
        //    if (novoValor) {
        //        $.each(self.Abono().AbonosValor(), function (i, aValor) {
        //            aValor.AbonoSaldo().SectoresDocumento = self.DatosDocumentoPasesVM().SetoresDocPases;
        //            aValor.AbonoSaldo().CanalesDocumento = self.DatosDocumentoPasesVM().CanalesDocPases;
        //            aValor.AbonoSaldo().SubCanalesDocumento = self.DatosDocumentoPasesVM().SubCanalesDocPases;
        //        });
        //        self.GrabarYContinuar();
        //    }
        //});
        //self.ExibirDatosGeneracionDocPases(true); 

        self.GrabarYContinuar();
    };

    self.GrabarYFinalizar = function () {

        if (self.Abono().CrearDocumentoPases()) {
            self.GenerarDocumentoPases();
            return;
        }
        self.GrabarYContinuar();
    };

    self.GrabarYContinuar = function () {

        if (self.VerificarSeHaErrosAoGrabarAbono())
            return;

        self.MesageConfirmacaoJS(self.Diccionarios.msg_ConfirGrabaryFinalizarAbono);
        if (self.ResultadoConfirmacaoJS()) {
            self.MesageLoading(self.Diccionarios.msg_GrabandoyFinalizandoAbono);

            var jsonData = "{abonoJson: " + ko.mapping.toJSON(self.Abono()) + "}";

            jQuery.ajax({
                url: 'PantallaAbono.aspx/GrabarYFinalizarAbono',
                contentType: "application/json; charset=utf-8",
                type: "POST",
                dataType: "json",
                data: jsonData,
                success: function (data, text) {
                    var respuesta = JSON.parse(data.d);
                    if (respuesta.CodigoError == "0" && respuesta.Respuesta != null) {
                        self.MesageLoading("");
                        console.log("GrabarYFinalizar");
                        self.AlertRedirectVM(new AlertRedirect(self.Diccionarios.msg_GrabadoyFinalizadoElAbono, 'PantallaVisualizar.aspx?IdentificadorAbono=' + respuesta.Respuesta.Identificador + '&Modo=Consulta'));
                    } else {
                        self.MesageAlertaErrorDescriptiva(respuesta.MensajeErrorDescriptiva);
                        self.MesageAlertaError(respuesta.MensajeError);
                    }
                },
                error: function (request, status, error) {
                    self.MesageAlertaErrorDescriptiva(request.responseText);
                    self.MesageAlertaError(error);
                }
            });
        }
    };

    //Anula o abono
    self.Anular = function () {
        self.MesageConfirmacaoJS(self.Diccionarios.msg_ConfirAnulandoAbono);
        if (self.ResultadoConfirmacaoJS()) {
            self.MesageLoading(self.Diccionarios.msg_AnulandoAbono);

            jQuery.ajax({
                url: 'PantallaAbono.aspx/AnularAbono',
                contentType: "application/json; charset=utf-8",
                type: "POST",
                dataType: "json",
                data: "{abonoJson: '" + ko.mapping.toJSON(self.Abono()) + "'}",
                success: function (data, text) {
                    var respuesta = JSON.parse(data.d);
                    if (respuesta.CodigoError == "0" && respuesta.Respuesta != null) {
                        self.MesageLoading("");
                        console.log("Anular");
                        self.AlertRedirectVM(new AlertRedirect(self.Diccionarios.msg_AnuladoElAbono, 'Busqueda.aspx'));
                    } else {
                        self.MesageAlertaErrorDescriptiva(respuesta.MensajeErrorDescriptiva);
                        self.MesageAlertaError(respuesta.MensajeError);
                    }
                },
                error: function (request, status, error) {
                    self.MesageAlertaErrorDescriptiva(request.responseText);
                    self.MesageAlertaError(error);
                }
            });
        }
    };
    //#endregion

    //#region Utilidades
    self.BuscarTemplatePeticionDatosBancarios = function () {
        if (!self.TemplateDatosBancarios.peticionDatosBancarios) {
            llamadaAjax('../../ServiciosInterface.asmx/obtenerTiposCuenta', 'POST', null, self.BusquedaListaTipoCuentaExito, self.BusquedaListaTipoCuentaError);
        }

        if (!self.TemplateDatosBancarios.listaTipoCuenta) {
            llamadaAjax('../../ServiciosInterface.asmx//ObtenerNuevoDatosBancarios', 'POST', null, self.BusquedaPeticionDatosBancariosExito, self.BusquedaPeticionDatosBancariosError);
        }
    }

    self.BusquedaListaTipoCuentaExito = function (data) {
        if (data && data.d) {
            self.TemplateDatosBancarios.listaTipoCuenta = jQuery.parseJSON(data.d);
        }
    }

    self.BusquedaListaTipoCuentaError = function (request, status, error) {
        //Lançar erro
    }

    self.BusquedaPeticionDatosBancariosExito = function (data) {
        if (data && data.d) {
            self.TemplateDatosBancarios.peticionDatosBancarios = jQuery.parseJSON(data.d);
        }
    }

    self.BusquedaPeticionDatosBancariosError = function (request, status, error) {
        //Lançar erro
    }

    self.DetallarAbonosValoresSaldosCasoExistam = function () {
        //Caso seja modificação de abonos a tela será aberta com uma lista de abonosValores predeterminados, no caso se saldos e pedido as Divisas devem ser Detalhadas
        if (self.Abono().TipoAbono() != "Elemento" && self.Abono().AbonosValor().length > 0) {
            for (index = 0; index < self.Abono().AbonosValor().length; ++index)
                self.DetallarDivisaAbonar(self.Abono().AbonosValor()[index], self.Abono().AbonosValor(), true);
        }
    }

    //#endregions

    self.BuscarTemplatePeticionDatosBancarios();

    self.ConfigLabelsPantalla();

    self.DetallarAbonosValoresSaldosCasoExistam();
    
}

function MapearListaResultadoFiltro(listaAbonosValor) {
    var listaAbonosValorObservables = [];
    for (var i = 0; i < listaAbonosValor.length; i++) {
        var abonoValor = new AbonoValor(listaAbonosValor[i]);
        listaAbonosValorObservables.push(abonoValor);
    }
    return listaAbonosValorObservables;
}

function MapearListaTerminosIAC(listaTerminos) {
    var listaTerminosObservables = [];

    if (!listaTerminos)
        return listaTerminosObservables;

    listaTerminos.forEach(function (item) {
        var terminoObservable = new TerminoIAC(item);
        listaTerminosObservables.push(terminoObservable)
    });

    return listaTerminosObservables;
}

function ObterFiltroPeloTipoAbono(tipoAbono, listaTipos) {
    var tipoAbonoFiltrado = listaTipos.filter(function (a) {
        return a.Codigo == tipoAbono;
    });

    if (tipoAbonoFiltrado && tipoAbonoFiltrado.length > 0 && tipoAbonoFiltrado[0].Filtro) {
        return new Filtro(tipoAbonoFiltrado[0].Filtro);
    } else {
        return null;
    }
}

function SetarFiltroPeloTipoAbono(filtro, listaTipos) {
    var tipoAbonoFiltrado = listaTipos.filter(function (a) {
        return a.Codigo == filtro.TipoAbono;
    });

    if (tipoAbonoFiltrado && tipoAbonoFiltrado.length > 0) {
        tipoAbonoFiltrado[0].Filtro = filtro;
    }
}