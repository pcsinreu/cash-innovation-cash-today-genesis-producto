/// <reference path="../jquery-1.10.1.min.js" />
/// <reference path="../knockout-3.4.0.js" />
/// <reference path="../knockout.mapping-latest.js" />
/// <reference path="Tipos.js" />
/// <reference path="PantallaAbonoVM.js" />

function DetalleVM(viewModel, abonoValor, informarOtraCuenta) {
    var self = this;
    if ((viewModel == null) || (abonoValor == null)) return;

    
    //#region Atributos

    self.Context = viewModel;
    self.Abono = viewModel.Abono();
    self.LabelDetalleCampoCliente = viewModel.LabelDetalleCampoCliente();

    //Para calcular disponibles
    self.ValoresDisponibles = new ValoresDisponibles();

    //Todos os abonos do mesmo número externo
    self.ListaAbonosValores = ko.observableArray(RetornaListaAbonoValor(self, abonoValor));

    //Selecionar o mesmo da tela
    self.AbonoValorSelecionado = ko.observable(self.ListaAbonosValores().filter(function (a) {

        return (abonoValor.Cuenta() && (abonoValor.Cuenta().Identificador() == a.Cuenta().Identificador()) && (abonoValor.Importe() != 0 && abonoValor.Importe() == a.Importe()));
        //return (a.Divisa().CodigoISO() == abonoValor.Divisa().CodigoISO());
    })[0]);


    if (!self.AbonoValorSelecionado()) {
        self.AbonoValorSelecionado = ko.observable(self.ListaAbonosValores().filter(function (a) {
            return (a.Divisa().CodigoISO() == abonoValor.Divisa().CodigoISO());
        })[0]);
    }

    //Ordenar Cuentas Disponibles caso já exista cuenta setada.
    self.OrdenarCuentasDisponibles = function () {
        if (abonoValor.Cuenta().CodigoCuentaBancaria()) {
            self.OrdenarCuentasDisponiblesSeExistente();
        }
        else {
            self.OrdenarDataBancarioDefecto();
        }
    }


    self.OrdenarCuentasAbonoValorCambiado = function () {
        var cuentaSelecionada = null;
        var datoBacarioSelecionado = null;

        for (i = 0; i < self.AbonoValorSelecionado().CuentasDisponibles().length ; ++i) {
            for (j = 0; j < self.AbonoValorSelecionado().CuentasDisponibles()[i].DatosBancarios().length ; ++j) {
                if (self.AbonoValorSelecionado().CuentasDisponibles()[i].DatosBancarios()[j].CodigoCuentaBancaria() == self.AbonoValorSelecionado().Cuenta().CodigoCuentaBancaria() &&
                    self.AbonoValorSelecionado().CuentasDisponibles()[i].DatosBancarios()[j].CodigoDocumento() == self.AbonoValorSelecionado().Cuenta().CodigoDocumento() &&
                    self.AbonoValorSelecionado().CuentasDisponibles()[i].DatosBancarios()[j].CodigoTipoCuentaBancaria() == self.AbonoValorSelecionado().Cuenta().CodigoTipoCuentaBancaria() &&
                    self.AbonoValorSelecionado().CuentasDisponibles()[i].DatosBancarios()[j].DescripcionTitularidad() == self.AbonoValorSelecionado().Cuenta().DescripcionTitularidad()) {

                    cuentaSelecionada = self.AbonoValorSelecionado().CuentasDisponibles()[i];
                    datoBacarioSelecionado = self.AbonoValorSelecionado().CuentasDisponibles()[i].DatosBancarios()[j];
                }
            }
        }

        if (cuentaSelecionada) {
            var indexCuenta = self.AbonoValorSelecionado().CuentasDisponibles().indexOf(cuentaSelecionada);
            MoverElementoArray(self.AbonoValorSelecionado().CuentasDisponibles(), indexCuenta, 0);

            var indexDatoBancario = self.AbonoValorSelecionado().CuentasDisponibles()[0].DatosBancarios().indexOf(datoBacarioSelecionado);
            MoverElementoArray(self.AbonoValorSelecionado().CuentasDisponibles()[0].DatosBancarios(), indexDatoBancario, 0);
        }
    }

    self.OrdenarCuentasDisponiblesSeExistente = function () {
        var cuentaSelecionada = null;
        var datoBacarioSelecionado = null;

        for (i = 0; i < self.AbonoValorSelecionado().CuentasDisponibles().length ; ++i) {
            for (j = 0; j < self.AbonoValorSelecionado().CuentasDisponibles()[i].DatosBancarios().length ; ++j) {
                if (self.AbonoValorSelecionado().CuentasDisponibles()[i].DatosBancarios()[j].CodigoCuentaBancaria() == abonoValor.Cuenta().CodigoCuentaBancaria() &&
                    self.AbonoValorSelecionado().CuentasDisponibles()[i].DatosBancarios()[j].CodigoDocumento() == abonoValor.Cuenta().CodigoDocumento() &&
                    self.AbonoValorSelecionado().CuentasDisponibles()[i].DatosBancarios()[j].CodigoTipoCuentaBancaria() == abonoValor.Cuenta().CodigoTipoCuentaBancaria() &&
                    self.AbonoValorSelecionado().CuentasDisponibles()[i].DatosBancarios()[j].DescripcionTitularidad() == abonoValor.Cuenta().DescripcionTitularidad()) {

                    cuentaSelecionada = self.AbonoValorSelecionado().CuentasDisponibles()[i];
                    datoBacarioSelecionado = self.AbonoValorSelecionado().CuentasDisponibles()[i].DatosBancarios()[j];
                }
            }
        }

        if (cuentaSelecionada) {
            var indexCuenta = self.AbonoValorSelecionado().CuentasDisponibles().indexOf(cuentaSelecionada);
            MoverElementoArray(self.AbonoValorSelecionado().CuentasDisponibles(), indexCuenta, 0);

            var indexDatoBancario = self.AbonoValorSelecionado().CuentasDisponibles()[0].DatosBancarios().indexOf(datoBacarioSelecionado);
            MoverElementoArray(self.AbonoValorSelecionado().CuentasDisponibles()[0].DatosBancarios(), indexDatoBancario, 0);
        }
    }

    self.OrdenarDataBancarioDefecto = function(){
        if (self.ListaAbonosValores().length > 0) {
            
            for (l = 0; l < self.ListaAbonosValores().length ; ++l) {
                if (self.ListaAbonosValores()[l].CuentasDisponibles().length > 0) {
                    for (i = 0; i < self.ListaAbonosValores()[l].CuentasDisponibles().length ; ++i) {
                        if (self.ListaAbonosValores()[l].CuentasDisponibles()[i].DatosBancarios().length > 0) {
                            for (j = 0; j < self.ListaAbonosValores()[l].CuentasDisponibles()[i].DatosBancarios().length ; ++j) {
                                if (self.ListaAbonosValores()[l].CuentasDisponibles()[i].DatosBancarios()[j].BolDefecto() === true) {
                                    var datoBancarioDefecto = self.ListaAbonosValores()[l].CuentasDisponibles()[i].DatosBancarios()[j];
                                    break;
                                }
                            }
                        }

                        indexDatoBancario = self.ListaAbonosValores()[l].CuentasDisponibles()[i].DatosBancarios().indexOf(datoBancarioDefecto);
                        MoverElementoArray(self.ListaAbonosValores()[l].CuentasDisponibles()[i].DatosBancarios(), indexDatoBancario, 0);
                    }
                }
            }
        }
    }

    self.OrdenarCuentasDisponibles();

    //Divisas
    self.DivisaSelecionada = ko.observable(SelecionaDivisa(self.AbonoValorSelecionado(), self.ValoresDisponibles.ListaDivisas()));
    self.AbonoValorSelecionado.subscribe(function (novoValor) {
        self.DivisaSelecionada(SelecionaDivisa(novoValor, self.ValoresDisponibles.ListaDivisas()));
        self.VefiricaModificacaoCliente(novoValor);
    });

    //Busca cliente
    self.Clientes = ko.observableArray();
    if (self.Context.Abono().Bancos().length > 0) {
        self.Clientes.push(self.Context.Abono().Bancos()[0]);
    }

    self.BuscaClientes = new UtilitarioBusca(self.Clientes);
    self.BuscaClientes.Propriedad.subscribe(function (novoCliente) {
        if (novoCliente != null && novoCliente.length > 0) {
            self.AbonoValorSelecionado().Cliente(novoCliente[0]);
        }
        else {
            self.AbonoValorSelecionado().Cliente(null);
        }
    });

    //CadastroDatosBancarios
    self.ExibirCadastroDatosBancarios = ko.observable(false);
    self.OcultarCadastroDatosBancarios = ko.observable(false);

    //Util
    self.InformarOtraCuentaRef = informarOtraCuenta;
    self.AlteracaoAbonoValor = false;

    //#endregion

    //#region Operações

    self.CambiarObservaciones = function () {

        if (self.AbonoValorSelecionado().Cuenta() && self.AbonoValorSelecionado().Cuenta().Observaciones) {
            self.AbonoValorSelecionado().Observaciones(self.AbonoValorSelecionado().Cuenta().Observaciones());
        }
    }

    self.VefiricaModificacaoCliente = function (aValorSelc) {
        aValorSelc.Cliente.subscribe(function (novoCliente) {

            if (self.AlteracaoAbonoValor)
                return;

            if (!novoCliente) {
                self.AbonoValorSelecionado().CuentasDisponibles.removeAll();
                return;
            }

            self.Context.MesageLoading(self.Context.Diccionarios.msg_VinculandoAbono);
            self.AbonoValorSelecionado().CuentasDisponibles.removeAll();
            self.AbonoValorSelecionado().SubCliente(null);
            self.AbonoValorSelecionado().PtoServicio(null);
            jQuery.ajax({
                url: 'PantallaAbono.aspx/ObtenerDatosBancarios',
                contentType: "application/json; charset=utf-8",
                type: "POST",
                dataType: "json",
                data: "{identificadorCliente: '" + novoCliente.Identificador() + "', identificadorDivisa: '" + aValorSelc.Divisa().Identificador() + "'}",
                success: function (data, text) {
                    var respuesta = JSON.parse(data.d);
                    if (respuesta.CodigoError == "0" && respuesta.Respuesta != null) {
                        $.each(respuesta.Respuesta, function (i, banco) {
                            self.AbonoValorSelecionado().CuentasDisponibles.push(new BancoInformacion(banco));
                        });
                    } else {
                        self.Context.MesageAlertaErrorDescriptiva(respuesta.MensajeErrorDescriptiva);
                        self.Context.MesageAlertaError(respuesta.MensajeError);
                    }
                    self.Context.MesageLoading("");
                },
                error: function (request, status, error) {
                    self.Context.MesageAlertaErrorDescriptiva(request.responseText);
                    self.Context.MesageAlertaError(error);
                }
            });
        });
    }

    //Anadir nuevo datos bancarios
    self.AnadirNuevoDatosBancarios = function () {
        self.Context.DatosBancariosVM(new CadastroDatosBancariosVM(self));
        self.ExibirCadastroDatosBancarios(true);
    }

    //Configura as divisas da Combox
    self.ConfigDivisa = function (option, item) {
        ko.applyBindingsToNode(option, {
            enable: ((!self.AbonoValorSelecionado().Novo && self.AbonoValorSelecionado().Divisa().CodigoISO() == item.CodigoISO) ||
                (self.AbonoValorSelecionado().Novo && item.PossuiValorDisponible())),
            style: { color: item.Color }
        }, item);
    };

    //Seleciona o AbonoValor para edição
    self.CambiarAbonoValor = function (abonoValorCambiar) {
        self.AlteracaoAbonoValor = true;

        if (self.AbonoValorSelecionado().Novo) {
            ZerarAbonoValor(self.AbonoValorSelecionado());
        }
        self.AbonoValorSelecionado(abonoValorCambiar);

        self.BuscaClientes.jsonString(abonoValorCambiar.Cliente());

        self.AlteracaoAbonoValor = false;
    };

    //Cria novo AbonoValor de uma divisa com valores disponíveis ou adiciona um novo na lista dos AbonosValores
    self.InformarOtraCuenta = function (criarNovo) {

        //Valida se o AbonoValor ao menos uma conta para detalhamento
        if (!self.AbonoValorSelecionado().CuentasDisponibles() || self.AbonoValorSelecionado().CuentasDisponibles().length == 0) {
            self.Context.MesageAlertaJS(self.Context.Diccionarios.msg_ListaCuentasOCuentaVacia);
            return;
        }

        if ((self.AbonoValorSelecionado() != null) && (self.AbonoValorSelecionado().Novo)) {
            if (self.AbonoValorSelecionado().Importe() == 0) {
                self.Context.MesageAlertaJS(self.Context.Diccionarios.msg_AbonoSinInformarValor);
                return;
            }
            self.AbonoValorSelecionado().Novo = false;
            self.ListaAbonosValores.push(self.AbonoValorSelecionado());
        }
        if (!criarNovo) return;


        var divisasPendentes = self.ValoresDisponibles.DivisasConValoresDisponibles();
        if (divisasPendentes.length == 0) {
            self.Context.MesageAlertaJS(self.Context.Diccionarios.msg_NingunDivisaConValorDisp);
            if (self.AbonoValorSelecionado() == null) {
                self.AbonoValorSelecionado(self.ListaAbonosValores()[0]);
            }
            return;
        }

        var novoAbonoValor = self.ListaAbonosValores().filter(function (a) {
            return (a.Divisa().CodigoISO() == divisasPendentes[0].CodigoISO);
        })[0];
        novoAbonoValor = new AbonoValor(ko.mapping.toJS(novoAbonoValor));//Clone

        ConfigAbonoValor(self, novoAbonoValor, true);
        self.AbonoValorSelecionado(novoAbonoValor)


        if (self.Abono.TipoAbono() != "Elemento") {
            self.EliminacionInformacionTerminosAbono(novoAbonoValor);
        }


        //Eliminación de información de la cuenta y Terminos para el nuevo AbonoValor
        //self.EliminacionInformacionCuentaYTerminos(novoAbonoValor);
    };

    self.EliminacionInformacionTerminosAbono = function (novoAbonoValor) {

        if (novoAbonoValor.AbonoSaldo().ListaTerminoIAC() != null && novoAbonoValor.AbonoSaldo().ListaTerminoIAC().length > 0) {

            $.each(novoAbonoValor.AbonoSaldo().ListaTerminoIAC(), function (index, term) {
                term.Valor("");
            });
        }

    }

    //Exclui o AbonoValor
    self.BorrarAbonoValor = function (abonoValorBorrar) {
        if (confirm(self.Context.Diccionarios.msg_ConfirmarExcluirAbonoEle)) {
            self.ListaAbonosValores.remove(abonoValorBorrar);
            ZerarAbonoValor(abonoValorBorrar);

            /*if (self.AbonoValorSelecionado() == abonoValorBorrar) {
                self.InformarOtraCuenta();
            }*/
        }
    };

    self.BorrarAbonoValorSinConfirmacion = function (abonoValorBorrar) {
        self.ListaAbonosValores.remove(abonoValorBorrar);
        ZerarAbonoValor(abonoValorBorrar);
    };

    //Confirma as modificações nos AbonosValores
    self.Confirmar = function () {
        //Salva AbonoValor se novo
        if (self.AbonoValorSelecionado().Novo) {
            self.InformarOtraCuenta(false);
        }

        if (ExhisteValoresNegativos(self) == true) {
            return
        };

        //Valida Cuentas llenas
        if (!self.AbonoValorSelecionado().Cuenta()  || self.AbonoValorSelecionado().CuentasDisponibles().length == 0) {
            self.Context.MesageAlertaJS(self.Context.Diccionarios.msg_ListaCuentasOCuentaVacia);
            return;
        }

        if (self.AbonoValorSelecionado().Importe() === 0) {
            self.Context.MesageAlertaJS(self.Context.Diccionarios.msg_AbonoSinInformarValor);
            return;
        }

        if(self.GestionarAbonosValoresVinculados(true)){

            //Valida cuentas
            //var cuentasOk = true;
            //$.each(self.ValoresDisponibles.ListaDivisas(), function (index, div) {
            //    var abonosPorDivisa = self.ListaAbonosValores().filter(function (a) {
            //        return (a.Divisa().CodigoISO() == div.CodigoISO);
            //    });
            //    for (var i = 0; i < abonosPorDivisa.length; i++) {
            //        var ctdCuentas = self.ListaAbonosValores().filter(function (a) {
            //            return ((a.Cuenta().CodigoCuentaBancaria() == self.ListaAbonosValores()[i].Cuenta().CodigoCuentaBancaria()) && 
            //                (a.Divisa().CodigoISO() == div.CodigoISO));
            //        }).length;
            //        if (ctdCuentas > 1) {
            //            self.Context.MesageAlertaJS(self.Context.Diccionarios.msg_DivisasConLaMismaCuenta + div.Descripcion);
            //            cuentasOk = false;
            //            break;
            //        }
            //    }
            //    if (!cuentasOk) { return false; }
            //});
            //if (!cuentasOk) { return; }



            self.Context.OcultarDetallar(true);
        }
    };

    self.GestionarAbonosValoresVinculados = function (esConfimacion) {
        //Remove os abonos copiados
        self.Abono.AbonosValor.removeAll(self.Abono.AbonosValor().filter(function (a) {
            return self.Context.AbonoValorPeloTipo.Filtrar(a, self.AbonoValorSelecionado());
        }));

        //Adiciona os abonos modificados
        for (var i = 0; i < self.ListaAbonosValores().length; i++) {
            self.Abono.AbonosValor.push(self.ListaAbonosValores()[i]);
        }

        //Valida cantidades
        var divisasPendentes = self.ValoresDisponibles.DivisasConValoresDisponibles();

        if ((self.Abono.TipoAbono() == "Elemento") && (divisasPendentes.length != 0)) {
            self.Context.MesageAlertaJS(self.Context.Diccionarios.msg_DivisasConValoresDisp);
            var abonoPendente = self.ListaAbonosValores().filter(function (a) {
                return (a.Divisa().CodigoISO() == divisasPendentes[0].CodigoISO);
            })[0];
            self.AbonoValorSelecionado(abonoPendente);

            return false;
        }
        else if (self.Abono.TipoAbono() != "Elemento") {

            self.Context.DetallarDivisaAbonar(self.AbonoValorSelecionado(), self.ListaAbonosValores(), esConfimacion);
        }

        return true;
    }

    //Fecha a tela cancelando todas as modificações
    self.Cancelar = function () {
        //Limpar objetos

        if (self.InformarOtraCuentaRef == false) {

            if (self.Abono.TipoAbono() == "Elemento") {
                self.Context.EliminarAbono(self.AbonoValorSelecionado());
            }
            else {

                var abonosValorRemover = self.ListaAbonosValores().filter(function (a) {
                    return self.Context.AbonoValorPeloTipo.Filtrar(a, self.AbonoValorSelecionado());
                });

                $.each(abonosValorRemover, function (lista, item) {
                    self.BorrarAbonoValorSinConfirmacion(item);
                });

                self.GestionarAbonosValoresVinculados(false);

                self.Context.BorrarSnapShot(self.AbonoValorSelecionado());
            }
        }


        self.Context.OcultarDetallar(true);
    };

    //Zera as quantidades para abono pedido
    self.ZerarCantidadQuandoAbonoPedido = function() {
        if (self.Abono.TipoAbono() == "Pedido") {
            $.each(self.AbonoValorSelecionado().Divisa().ListaEfectivo(), function (lista, item) {
                item.Cantidad(0);
            });

            $.each(self.AbonoValorSelecionado().Divisa().ListaMedioPago(), function (lista, item) {
                item.Cantidad(0);
                item.Importe(0);
            });

            self.AbonoValorSelecionado().Divisa().Totales().TotalEfectivo(0);
            self.AbonoValorSelecionado().Divisa().Totales().TotalCheque(0);
            self.AbonoValorSelecionado().Divisa().Totales().TotalOtroValor(0);
            self.AbonoValorSelecionado().Divisa().Totales().TotalTarjeta(0);
            self.AbonoValorSelecionado().Divisa().Totales().TotalTicket(0);
        }
    }

    //#endregion

    self.VefiricaModificacaoCliente(self.AbonoValorSelecionado());

    if (self.InformarOtraCuentaRef == true) {
        self.InformarOtraCuenta(self.InformarOtraCuentaRef);
    }

    if (self.InformarOtraCuentaRef == false)
        self.ZerarCantidadQuandoAbonoPedido();

    IninitializePopupCuenta(self);
};

function ExhisteValoresNegativos(vm) {      

    if (vm.Context.Abono().TipoAbono() != "Elemento" && vm.Context.PermiteSaldoNegativo == false) {

        for (var j = 0; j < vm.ListaAbonosValores().length; j++) {

            var aValor = vm.ListaAbonosValores()[j];

            for (var i = 0; i < aValor.Divisa().ListaEfectivo().length; i++) {
                if (aValor.Divisa().ListaEfectivo()[i].Cantidad() < 0) {
                    vm.Context.MesageAlertaJS(vm.Context.Diccionarios.msg_PermiteSaldoNegativo);
                    return true;
                }
            }

            for (var i = 0; i < aValor.Divisa().ListaMedioPago().length; i++) {
                if (aValor.Divisa().ListaMedioPago()[i].Cantidad() < 0) {
                    vm.Context.MesageAlertaJS(vm.Context.Diccionarios.msg_PermiteSaldoNegativo);
                    return true;
                };
                if (aValor.Divisa().ListaMedioPago()[i].Importe() < 0) {
                    vm.Context.MesageAlertaJS(vm.Context.Diccionarios.msg_PermiteSaldoNegativo);
                    return true;
                };
            }

            if (aValor.Divisa().Totales().TotalEfectivo() < 0) {
                vm.Context.MesageAlertaJS(vm.Context.Diccionarios.msg_PermiteSaldoNegativo);
                return true;
            };
            if (aValor.Divisa().Totales().TotalCheque() < 0) {
                vm.Context.MesageAlertaJS(vm.Context.Diccionarios.msg_PermiteSaldoNegativo);
                return true;
            };
            if (aValor.Divisa().Totales().TotalOtroValor() < 0) {
                vm.Context.MesageAlertaJS(vm.Context.Diccionarios.msg_PermiteSaldoNegativo);
                return true;
            };
            if (aValor.Divisa().Totales().TotalTarjeta() < 0) {
                vm.Context.MesageAlertaJS(vm.Context.Diccionarios.msg_PermiteSaldoNegativo);
                return true;
            };
            if (aValor.Divisa().Totales().TotalTicket() < 0) {
                vm.Context.MesageAlertaJS(vm.Context.Diccionarios.msg_PermiteSaldoNegativo);
                return true;
            };
        }
    }
    return false;
}

//Filtra todos os abonos do mesmo elemento
function RetornaListaAbonoValor(vm, abonoValor) {
    abonosValorCopia = [];

    var abonosValor = vm.Abono.AbonosValor().filter(function (a) {
        return vm.Context.AbonoValorPeloTipo.Filtrar(a, abonoValor);
    });

    for (var a = 0; a < abonosValor.length; a++) {
        var abonoValorCopia = new AbonoValor(ko.mapping.toJS(abonosValor[a]));//Clone
        abonoValorCopia.Novo = false;

        ConfigAbonoValor(vm, abonoValorCopia, false);
        RemoveEfectivoZerado(abonoValorCopia);

        abonosValorCopia.push(abonoValorCopia);
    }

    return abonosValorCopia;
}

//Zerar efectivos que no tem cantidad dispobible e cantidad informada
function RemoveEfectivoZerado(aValor) {

    aValor.Divisa().ListaEfectivo.removeAll(aValor.Divisa().ListaEfectivo().filter(function (ef) {
        return ef.Cantidad() === 0 && ef.EfectivoDisponible.CantidadDisponible() === 0;
    }));
}

//Configura a Cantidad/Importe do AbonoValor
function ConfigAbonoValor(vm, aValor, novo) {
    //Obtém a divisa
    var divisaPeloTipo = (vm.Abono.TipoAbono() == "Elemento") ? aValor.AbonoElemento().Divisa() : aValor.AbonoSaldo().Divisa();

    //#region Para cada Efectivo do AbonoValor
    for (var e = 0; e < aValor.Divisa().ListaEfectivo().length; e++) {
        var efectivoAbono = aValor.Divisa().ListaEfectivo()[e];
        var efectivoSaldo = divisaPeloTipo.ListaEfectivo().filter(function (ef) { return (ef.Codigo() == efectivoAbono.Codigo()); })[0];
        efectivoAbono.EfectivoDisponible = vm.ValoresDisponibles.RetornaListaEfectivoDisponible(aValor.Divisa(), efectivoSaldo);

        //#region Atualiza o Importe do Efectivo
        efectivoAbono.Cantidad.subscribe(function (novoValor) {

            var efecAbono = this;

            if (novoValor == "") {
                efecAbono.Cantidad(0);
            }

            efecAbono.Importe(novoValor * efecAbono.Valor());
        }, efectivoAbono);
        //#endregion

        //#region Atualiza Cantidad disponible
        efectivoAbono.Importe.subscribe(function (novoValor) {
            var efecAbono = this;
            novoValor = RemoverFormato(novoValor);

            if (vm.Context.Abono().TipoAbono() != "Elemento") {
                if (novoValor < 0 && vm.Context.PermiteSaldoNegativo == false) {
                    vm.Context.MesageAlertaJS(vm.Context.Diccionarios.msg_PermiteSaldoNegativo);
                    efecAbono.Cantidad(0);
                    return;
                }
            }

            //Busca o Efectivo do AbonoElemento ou AbonoSaldo
            var efectivoElemento = vm.Context.AbonoValorPeloTipo.ListaEfectivo(aValor).filter(function (e) {
                return (e.Codigo() == efecAbono.Codigo());
            })[0];

            var totalAbonado = 0;
            var abonosDivisa = vm.ListaAbonosValores().filter(function (a) { return vm.Context.AbonoValorPeloTipo.FiltrarPorDivisa(a, aValor); });
            for (var i = 0; i < abonosDivisa.length; i++) {
                totalAbonado += parseFloat(abonosDivisa[i].Divisa().ListaEfectivo().filter(function (e) {
                    return (e.Codigo() == efecAbono.Codigo());
                })[0].Cantidad());
            }

            var ctdNuevoAbono = 0;
            if ((vm.AbonoValorSelecionado().Novo) && vm.AbonoValorSelecionado().Divisa().CodigoISO() == vm.Context.AbonoValorPeloTipo.CodigoISO(aValor)) {
                ctdNuevoAbono = parseFloat(vm.AbonoValorSelecionado().Divisa().ListaEfectivo().filter(function (e) {
                    return (e.Codigo() == efecAbono.Codigo());
                })[0].Cantidad());
            }
            efecAbono.EfectivoDisponible.CantidadDisponible(efectivoElemento.Cantidad() - totalAbonado - ctdNuevoAbono);

            if (vm.Context.Abono().TipoAbono() == "Elemento") {
                if (efecAbono.EfectivoDisponible.CantidadDisponible() < 0) {
                    efecAbono.Cantidad(SomarValoresTolerenacia2(efecAbono.Cantidad(), efecAbono.EfectivoDisponible.CantidadDisponible()));
                }
            }
            else if (novoValor < 0 && vm.Context.PermiteSaldoNegativo == false) {
                vm.Context.MesageAlertaJS(vm.Context.Diccionarios.msg_PermiteSaldoNegativo);
                efecAbono.Cantidad(0);
                return;
            }

            TotalizaAbonoValor(aValor);

        }, efectivoAbono);
        //#endregion

        if (novo) efectivoAbono.Cantidad(0);
    }
    //#endregion

    //#region Para cada MedioPago do AbonoValor
    for (var e = 0; e < aValor.Divisa().ListaMedioPago().length; e++) {
        var medioPagoAbono = aValor.Divisa().ListaMedioPago()[e];
        var medioPagoSaldo = divisaPeloTipo.ListaMedioPago().filter(function (ef) { return (ef.Codigo() == medioPagoAbono.Codigo() && ef.TipoMedioPago() == medioPagoAbono.TipoMedioPago()); })[0];
        medioPagoAbono.MedioPagoDisponible = vm.ValoresDisponibles.RetornaListaMedioPagoDisponible(aValor.Divisa(), medioPagoSaldo);

        //#region Atualiza Cantidad disponible
        medioPagoAbono.Cantidad.subscribe(function (novoValor) {
            var mpAbono = this;

            if (novoValor == "") {
                mpAbono.Cantidad(0);
            }

            //Busca o MedioPago do AbonoElemento ou AbonoSaldo
            var medioPagoElemento = vm.Context.AbonoValorPeloTipo.ListaMedioPago(aValor).filter(function (e) {
                return (e.Codigo() == mpAbono.Codigo());
            })[0];

            //Atualiza cantidad disponivel
            var totalAbonado = 0;
            var abonosDivisa = vm.ListaAbonosValores().filter(function (a) { return a.Divisa().CodigoISO() == aValor.Divisa().CodigoISO(); });
            for (var i = 0; i < abonosDivisa.length; i++) {
                totalAbonado += parseFloat(abonosDivisa[i].Divisa().ListaMedioPago().filter(function (e) {
                    return (e.Codigo() == mpAbono.Codigo());
                })[0].Cantidad());
            }

            if (vm.Context.Abono().TipoAbono() == "Elemento") {
                if (mpAbono.MedioPagoDisponible.CantidadDisponible() < 0) {
                    mpAbono.Cantidad(mpAbono.Cantidad() + mpAbono.MedioPagoDisponible.CantidadDisponible());
                }
            }
            else if (totalAbonado < 0 && vm.Context.PermiteSaldoNegativo == false) {
                vm.Context.MesageAlertaJS(vm.Context.Diccionarios.msg_PermiteSaldoNegativo);
                mpAbono.Cantidad(0);
                return;
            }

            mpAbono.MedioPagoDisponible.CantidadDisponible(medioPagoElemento.Cantidad() - totalAbonado - ((vm.AbonoValorSelecionado().Novo && (vm.AbonoValorSelecionado().Divisa().CodigoISO() == vm.Context.AbonoValorPeloTipo.CodigoISO(aValor))) ? parseFloat(novoValor) : 0));

        }, medioPagoAbono);
        //#endregion

        //#region Atualiza Importe disponible
        medioPagoAbono.Importe.subscribe(function (novoValor) {
            var mpAbono = this;

            if (typeof (novoValor) == "string") {
                mpAbono.Importe(ObtenerValorNumerico(novoValor));
            }

            //Busca o MedioPago do AbonoElemento ou AbonoSaldo
            var medioPagoElemento = vm.Context.AbonoValorPeloTipo.ListaMedioPago(aValor).filter(function (e) {
                return (e.Codigo() == mpAbono.Codigo());
            })[0];

            //Atualiza o importe (valor) disponivel
            var totalAbonado = 0;
            var abonosDivisa = vm.ListaAbonosValores().filter(function (a) { return a.Divisa().CodigoISO() == aValor.Divisa().CodigoISO(); });
            for (var i = 0; i < abonosDivisa.length; i++) {
                totalAbonado += parseFloat(abonosDivisa[i].Divisa().ListaMedioPago().filter(function (e) {
                    return (e.Codigo() == mpAbono.Codigo());
                })[0].Importe());
            }

            if (vm.Context.Abono().TipoAbono() == "Elemento") {
                if (mpAbono.MedioPagoDisponible.ValorDisponible() < 0) {
                    mpAbono.ValorDisponible(mpAbono.ValorDisponible() + mpAbono.MedioPagoDisponible.ValorDisponible());
                }
            }
            else if (totalAbonado < 0 && vm.Context.PermiteSaldoNegativo == false) {
                vm.Context.MesageAlertaJS(vm.Context.Diccionarios.msg_PermiteSaldoNegativo);
                mpAbono.Importe(0);
                return;
            }

            mpAbono.MedioPagoDisponible.ValorDisponible(medioPagoElemento.Importe() - totalAbonado - ((vm.AbonoValorSelecionado().Novo && (vm.AbonoValorSelecionado().Divisa().CodigoISO() == vm.Context.AbonoValorPeloTipo.CodigoISO(aValor))) ? parseFloat(novoValor) : 0));

            TotalizaAbonoValor(aValor);

        }, medioPagoAbono);
        //#endregion

        if (novo) {
            medioPagoAbono.Cantidad(0);
            medioPagoAbono.Importe(0);
        }
    }
    //#endregion

    aValor.Divisa().Totales().TotalesAbonoDisponible = vm.ValoresDisponibles.RetornaTotalesDisponible(aValor.Divisa(), divisaPeloTipo.Totales());

    //#region Para cada Total do AbonoValor 

    //Valores Novos
    aValor.Divisa().Totales().TotalEfectivo.subscribe(function (novoValor) {
        var tot = this;
        if (typeof (novoValor) == "string") {
            tot.TotalEfectivo(ObtenerValorNumerico(novoValor));
            return;
        }

        var totalAbonado = 0;
        var abonosDivisa = vm.ListaAbonosValores().filter(function (a) { return a.Divisa().CodigoISO() == aValor.Divisa().CodigoISO(); });
        for (var i = 0; i < abonosDivisa.length; i++) {
            totalAbonado += ObtenerValorNumerico(abonosDivisa[i].Divisa().Totales().TotalEfectivo());
        }

        var valorEfectivo = ObtenerValorNumerico(vm.Context.AbonoValorPeloTipo.Totales(aValor).TotalEfectivo());
        novoValor = ObtenerValorNumerico((((vm.AbonoValorSelecionado().Novo && (vm.AbonoValorSelecionado().Divisa().CodigoISO() == vm.Context.AbonoValorPeloTipo.CodigoISO(aValor))) ? parseFloat(novoValor) : 0)));
        totalAbonado = ObtenerValorNumerico(totalAbonado);

        var resultadoTotalesEfectivo = ObtenerValorNumerico(valorEfectivo - totalAbonado - novoValor);
        tot.TotalesAbonoDisponible.TotalEfectivoDisponible(resultadoTotalesEfectivo);

        if (vm.Context.Abono().TipoAbono() == "Elemento") {
            if (tot.TotalesAbonoDisponible.TotalEfectivoDisponible() < 0) {
                tot.TotalEfectivo(tot.TotalEfectivo() + tot.TotalesAbonoDisponible.TotalEfectivoDisponible());
            }
        }
        else if (totalAbonado < 0 && vm.Context.PermiteSaldoNegativo == false) {
            vm.Context.MesageAlertaJS(vm.Context.Diccionarios.msg_PermiteSaldoNegativo);
            tot.TotalEfectivo(0);
            return;
        }

        TotalizaAbonoValor(aValor);
    }, aValor.Divisa().Totales());
    aValor.Divisa().Totales().TotalCheque.subscribe(function (novoValor) {
        var tot = this;
        if (typeof (novoValor) == "string") {
            tot.TotalCheque(ObtenerValorNumerico(novoValor));
            return;
        }

        novoValor = ObtenerValorNumerico(novoValor);
        var totalAbonado = 0;
        var abonosDivisa = vm.ListaAbonosValores().filter(function (a) { return a.Divisa().CodigoISO() == aValor.Divisa().CodigoISO(); });
        for (var i = 0; i < abonosDivisa.length; i++) {
            totalAbonado += ObtenerValorNumerico(abonosDivisa[i].Divisa().Totales().TotalCheque());
        }

        var valorCheque = ObtenerValorNumerico(vm.Context.AbonoValorPeloTipo.Totales(aValor).TotalCheque());
        novoValor = ObtenerValorNumerico(((vm.AbonoValorSelecionado().Novo && (vm.AbonoValorSelecionado().Divisa().CodigoISO() == vm.Context.AbonoValorPeloTipo.CodigoISO(aValor))) ? parseFloat(novoValor) : 0));
        totalAbonado = ObtenerValorNumerico(totalAbonado);

        var resultadoTotalesCheque = ObtenerValorNumerico(valorCheque - totalAbonado - novoValor);
        tot.TotalesAbonoDisponible.TotalChequeDisponible(resultadoTotalesCheque);


        if (vm.Context.Abono().TipoAbono() == "Elemento") {
            if (tot.TotalesAbonoDisponible.TotalChequeDisponible() < 0) {
                tot.TotalCheque(tot.TotalCheque() + tot.TotalesAbonoDisponible.TotalChequeDisponible());
            }
        }
        else if (totalAbonado < 0 && vm.Context.PermiteSaldoNegativo == false) {
            vm.Context.MesageAlertaJS(vm.Context.Diccionarios.msg_PermiteSaldoNegativo);
            tot.TotalCheque(0);
            return;
        }
        
        TotalizaAbonoValor(aValor);
    }, aValor.Divisa().Totales());
    aValor.Divisa().Totales().TotalOtroValor.subscribe(function (novoValor) {
        var tot = this;
        if (typeof (novoValor) == "string") {
            tot.TotalOtroValor(ObtenerValorNumerico(novoValor));
            return;
        }

        novoValor = ObtenerValorNumerico(novoValor);
        var totalAbonado = 0;
        var abonosDivisa = vm.ListaAbonosValores().filter(function (a) { return a.Divisa().CodigoISO() == aValor.Divisa().CodigoISO(); });
        for (var i = 0; i < abonosDivisa.length; i++) {
            totalAbonado += ObtenerValorNumerico(abonosDivisa[i].Divisa().Totales().TotalOtroValor());
        }

        var valorOtroValor = ObtenerValorNumerico(vm.Context.AbonoValorPeloTipo.Totales(aValor).TotalOtroValor());
        novoValor = ObtenerValorNumerico(((vm.AbonoValorSelecionado().Novo && (vm.AbonoValorSelecionado().Divisa().CodigoISO() == vm.Context.AbonoValorPeloTipo.CodigoISO(aValor))) ? parseFloat(novoValor) : 0));
        totalAbonado = ObtenerValorNumerico(totalAbonado);

        var resultadoTotalesOtroValor = ObtenerValorNumerico(valorOtroValor - totalAbonado - novoValor);
        tot.TotalesAbonoDisponible.TotalOtroValorDisponible(resultadoTotalesOtroValor);

        if (vm.Context.Abono().TipoAbono() == "Elemento") {
            if (tot.TotalesAbonoDisponible.TotalOtroValorDisponible() < 0) {
                tot.TotalOtroValor(tot.TotalOtroValor() + tot.TotalesAbonoDisponible.TotalOtroValorDisponible());
            }
        }
        else if (totalAbonado < 0 && vm.Context.PermiteSaldoNegativo == false) {
            vm.Context.MesageAlertaJS(vm.Context.Diccionarios.msg_PermiteSaldoNegativo);
            tot.TotalOtroValor(0);
            return;
        }
        
        TotalizaAbonoValor(aValor);
    }, aValor.Divisa().Totales());
    aValor.Divisa().Totales().TotalTarjeta.subscribe(function (novoValor) {
        var tot = this;
        if (typeof (novoValor) == "string") {
            tot.TotalTarjeta(ObtenerValorNumerico(novoValor));
            return;
        }

        novoValor = ObtenerValorNumerico(novoValor);
        var totalAbonado = 0;
        var abonosDivisa = vm.ListaAbonosValores().filter(function (a) { return a.Divisa().CodigoISO() == aValor.Divisa().CodigoISO(); });
        for (var i = 0; i < abonosDivisa.length; i++) {
            totalAbonado += ObtenerValorNumerico(abonosDivisa[i].Divisa().Totales().TotalTarjeta());
        }

        var valorTarjeta = ObtenerValorNumerico(vm.Context.AbonoValorPeloTipo.Totales(aValor).TotalTarjeta());
        novoValor = ObtenerValorNumerico(((vm.AbonoValorSelecionado().Novo && (vm.AbonoValorSelecionado().Divisa().CodigoISO() == vm.Context.AbonoValorPeloTipo.CodigoISO(aValor))) ? parseFloat(novoValor) : 0));
        totalAbonado = ObtenerValorNumerico(totalAbonado);

        var resultadoTotalesTarjeta = ObtenerValorNumerico(valorTarjeta - totalAbonado - novoValor);
        tot.TotalesAbonoDisponible.TotalTarjetaDisponible(resultadoTotalesTarjeta);

        if (vm.Context.Abono().TipoAbono() == "Elemento") {
            if (tot.TotalesAbonoDisponible.TotalTarjetaDisponible() < 0) {
                tot.TotalTarjeta(tot.TotalTarjeta() + tot.TotalesAbonoDisponible.TotalTarjetaDisponible());
            }
        }
        else if (totalAbonado < 0 && vm.Context.PermiteSaldoNegativo == false) {
            vm.Context.MesageAlertaJS(vm.Context.Diccionarios.msg_PermiteSaldoNegativo);
            tot.TotalTarjeta(0);
            return;
        }

        TotalizaAbonoValor(aValor);
    }, aValor.Divisa().Totales());
    aValor.Divisa().Totales().TotalTicket.subscribe(function (novoValor) {
        var tot = this;
        if (typeof (novoValor) == "string") {
            tot.TotalTicket(ObtenerValorNumerico(novoValor));
            return;
        }

        novoValor = ObtenerValorNumerico(novoValor);
        var totalAbonado = 0;
        var abonosDivisa = vm.ListaAbonosValores().filter(function (a) { return a.Divisa().CodigoISO() == aValor.Divisa().CodigoISO(); });
        for (var i = 0; i < abonosDivisa.length; i++) {
            totalAbonado += ObtenerValorNumerico(abonosDivisa[i].Divisa().Totales().TotalTicket());
        }

        var valorTicket = ObtenerValorNumerico(vm.Context.AbonoValorPeloTipo.Totales(aValor).TotalTicket());
        novoValor = ObtenerValorNumerico(((vm.AbonoValorSelecionado().Novo && (vm.AbonoValorSelecionado().Divisa().CodigoISO() == vm.Context.AbonoValorPeloTipo.CodigoISO(aValor))) ? parseFloat(novoValor) : 0));
        totalAbonado = ObtenerValorNumerico(totalAbonado);

        var resultadoTotalesTicket = ObtenerValorNumerico(valorTicket - totalAbonado - novoValor);
        tot.TotalesAbonoDisponible.TotalTicketDisponible(resultadoTotalesTicket);

        if (vm.Context.Abono().TipoAbono() == "Elemento") {
            if (tot.TotalesAbonoDisponible.TotalTicketDisponible() < 0) {
                tot.TotalTicket(tot.TotalTicket() + tot.TotalesAbonoDisponible.TotalTicketDisponible());
            }
        }
        else if (totalAbonado < 0 && vm.Context.PermiteSaldoNegativo == false) {
            vm.Context.MesageAlertaJS(vm.Context.Diccionarios.msg_PermiteSaldoNegativo);
            tot.TotalTicket(0);
            return;
        }

        TotalizaAbonoValor(aValor);
    }, aValor.Divisa().Totales());


    //#endregion

    if (novo) {
        aValor.Divisa().Totales().TotalEfectivo(0);
        aValor.Divisa().Totales().TotalCheque(0);
        aValor.Divisa().Totales().TotalOtroValor(0);
        aValor.Divisa().Totales().TotalTarjeta(0);
        aValor.Divisa().Totales().TotalTicket(0);

        if (vm.Context.Abono().TipoAbono() == "Elemento") {
            aValor.AbonoElemento().Codigo();
        }
        aValor.Observaciones();
        //Setar conta padrão
    }
    //else {
    //    if (aValor.CuentasDisponibles() && aValor.CuentasDisponibles().length > 0) {
    //        aValor.Cuenta(
    //            aValor.CuentasDisponibles().filter(function (c) {
    //                return c.CodigoTipoCuentaBancaria() == aValor.Cuenta().CodigoTipoCuentaBancaria() &&
    //                    c.CodigoCuentaBancaria() == aValor.Cuenta().CodigoCuentaBancaria()
    //            })[0]
    //        );
    //    }
    //}

    TotalizaAbonoValor(aValor);
}

//Totaliza o importe do AbonoValor pelos Efectivo, MedioPagos e Totales
function TotalizaAbonoValor(aValor) {
    //Totaliza os efectivos
    var impTotalEfectivo = 0;
    var impTotalEfectivoDisponible = 0;
    for (var eI = 0; eI < aValor.Divisa().ListaEfectivo().length; eI++) {
        impTotalEfectivo += ObtenerValorNumerico(aValor.Divisa().ListaEfectivo()[eI].Importe());
        impTotalEfectivoDisponible += ObtenerValorNumerico(aValor.Divisa().ListaEfectivo()[eI].EfectivoDisponible.ValorDisponible());
    }
    aValor.ImporteTotalEfectivo(impTotalEfectivo);
    aValor.ImporteTotalEfectivoDisponible(impTotalEfectivoDisponible);

    //Totaliza os Medio Pagos
    var impTotalMedioPagos = 0;
    aValor.TiposMedioPagoTotales.removeAll();
    for (var mI = 0; mI < aValor.Divisa().ListaMedioPago().length; mI++) {
        var tipoMedioPago = aValor.TipoMedioPagoTotalPeloTipo(aValor.Divisa().ListaMedioPago()[mI].DescripcionTipoMedioPago());
        tipoMedioPago.ImporteTotalMedioPagos(tipoMedioPago.ImporteTotalMedioPagos() + ObtenerValorNumerico(aValor.Divisa().ListaMedioPago()[mI].Importe()));
        tipoMedioPago.ImporteTotalMedioPagosDisponible(tipoMedioPago.ImporteTotalMedioPagosDisponible() + ObtenerValorNumerico(aValor.Divisa().ListaMedioPago()[mI].MedioPagoDisponible.ValorDisponible()));

        impTotalMedioPagos += ObtenerValorNumerico(aValor.Divisa().ListaMedioPago()[mI].Importe());
    }

    var impTotales = 0;
    impTotales += ObtenerValorNumerico(aValor.Divisa().Totales().TotalCheque());
    impTotales += ObtenerValorNumerico(aValor.Divisa().Totales().TotalEfectivo());
    impTotales += ObtenerValorNumerico(aValor.Divisa().Totales().TotalOtroValor());
    impTotales += ObtenerValorNumerico(aValor.Divisa().Totales().TotalTarjeta());
    impTotales += ObtenerValorNumerico(aValor.Divisa().Totales().TotalTicket());
    impTotales += impTotalEfectivo;
    impTotales += impTotalMedioPagos;
    aValor.Importe(ObtenerValorNumerico(impTotales));
}

//Seleciona divisa pelo abono
function SelecionaDivisa(aValor, listaDivisa) {
    if (aValor == null) return;
    var divisa = listaDivisa.filter(function (d) {
        return (d.CodigoISO == aValor.Divisa().CodigoISO());
    })[0];
    return divisa
}

//Zerar cantidades/importes
function ZerarAbonoValor(aValor) {
    for (var i = 0; i < aValor.Divisa().ListaEfectivo().length; i++) {
        aValor.Divisa().ListaEfectivo()[i].Cantidad(0);
    }
    for (var i = 0; i < aValor.Divisa().ListaMedioPago().length; i++) {
        aValor.Divisa().ListaMedioPago()[i].Cantidad(0);
        aValor.Divisa().ListaMedioPago()[i].Importe(0);
    }
    aValor.Divisa().Totales().TotalEfectivo(0);
    aValor.Divisa().Totales().TotalCheque(0);
    aValor.Divisa().Totales().TotalOtroValor(0);
    aValor.Divisa().Totales().TotalTarjeta(0);
    aValor.Divisa().Totales().TotalTicket(0);
}

function IninitializePopupCuenta(vmDetalle) {
    vmDetalle.ExibirCadastroDatosBancarios.subscribe(function (novoValor) { ExibirPopupCadastroDatosBancarios(novoValor, vmDetalle); });
    vmDetalle.OcultarCadastroDatosBancarios.subscribe(function (novoValor) { OcultarPopupCadastroDatosBancarios(novoValor, vmDetalle); });
}

function ExibirPopupCadastroDatosBancarios(novoValor, vmDetalle) {
    if (novoValor) {
        vmDetalle.ExibirCadastroDatosBancarios(false);
        showModal('dvCadastroDatosBancarios');
    }
}

function OcultarPopupCadastroDatosBancarios(novoValor, vmDetalle) {
    if (novoValor) {
        vmDetalle.OcultarCadastroDatosBancarios(false);
        closeModal('dvCadastroDatosBancarios');
    }
}