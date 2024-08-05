//#region ViewModel para o Grid de MedioPago a Detalhar
// parametros   =   Lista de parametros carregada no servidor (ucSaldoMedioPagoDetallar.ascx.vb)
// totalesEfectivo  =   Identificador do panel do controle ucSaldoMedioPagoDetallar
function InicializaUcSaldoMedioPagoDetallar(parametros, totalesMedioPago, ucClientID) {

    function ObjetoNaoSerializavel(objeto) {
        for (propriedade in objeto) {
            if (objeto.hasOwnProperty(propriedade)) {
                this[propriedade] = objeto[propriedade];
            }
        }
        this.toJSON = function () {
            return { Identificador: this.Identificador };
        };
    }

    parametros.DivisasDisponiveis = $.map(parametros.DivisasDisponiveis, function (divisa) {
        if (divisa.Denominaciones != null) {
            divisa.Denominaciones = $.map(divisa.Denominaciones, function (denominacion) {
                return new ObjetoNaoSerializavel(denominacion);
            });
        }
        return new ObjetoNaoSerializavel(divisa);
    });

    //ko.validation.configure();

    // enable validation
    ko.validation.init({
        errorsAsTitle: false,
        insertMessages: true,
        decorateInputElement: false,
        registerExtenders: true,
        messagesOnModified: true,
        parseInputAttributes: true,
        messageTemplate: null
    });

    //#region bindingHandlers

    ko.bindingHandlers.addLinhaMedioPago = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).change(function () {
                if (bindingContext.$index() == bindingContext.$parent.totalesMedioPago().length - 1) {
                    var divisaAtual = bindingContext.$parent.totalesMedioPago()[bindingContext.$index()].divisa;
                    divisaAtual.isModified(true);
                    if (divisaAtual() == null) {
                        divisaAtual(bindingContext.$parent.totalesMedioPago()[bindingContext.$index() - 1].divisa());
                    }
                    bindingContext.$parent.totalesMedioPago.push(new TotalMedioPago(null, bindingContext.$root));
                    $('select').unbind('keypress').keypress(TratamentosDropdown);
                }
            });
        }
    };

    ko.bindingHandlers.addLinhaTermino = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).change(function () {
                var totalMedioPagoEdicao = bindingContext.$parents[2].totalMedioPagoEdicao();
                var lista = totalMedioPagoEdicao.terminos;
                if (lista()[lista().length - 1] == bindingContext.$parents[1]) {
                    var novaLista = [];
                    var terminos = totalMedioPagoEdicao.terminosDisponiveis();
                    for (var i = 0; i < terminos.length; i++) {
                        novaLista.push(new TerminoMedioPago($.extend(true, {}, terminos[i]), null, null));
                    }
                    lista.push(novaLista);
                    $('select').unbind('keypress').keypress(TratamentosDropdown);
                }
            });
        }
    };

    ko.bindingHandlers.teclaAtalho = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).data("elementosAtalho", ko.unwrap(valueAccessor()));
        }
    };

    ko.bindingHandlers.mostrarCalendario = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            var habilitado = allBindings.get('enable');
            if (habilitado) {
                AbrirCalendario(element, ko.unwrap(valueAccessor()));
            }
        }
    };

    //#endregion

    //#region ViewModel - Efetivo a detalhar

    function TotalesMedioPagoViewModelDetallar() {

        var self = this;

        self.dicionario = parametros.Dicionario;
        self.clienteID = ucClientID;
        self.divisasDisponiveis = parametros.DivisasDisponiveis.filter(function (divisaDisponivel) {
            return divisaDisponivel.MediosPago != null && divisaDisponivel.MediosPago.length > 0;
        });
        self.tipoMedioPagoDisponivel = parametros.TipoMedioPagoDisponivel;
        self.exhibirTotalesMedioPago = parametros.ExhibirTotalesMedioPago;

        self.totalMedioPagoEdicao = ko.observable();

        self.editavel = (parametros.Modo != "Consulta");

        self.inicializar = false;

        self.totalesMedioPago = ko.observableArray($.map(parametros.TotalesMedioPago, function (total) {
            return new TotalMedioPago(total, self);
        }));

        self.inicializar = true;

        self.totalesMedioPagoModificar = [];
        self.TotalMedioPagoCambiado = self.totalesMedioPago()[0];
        // Metodos
        self.RemoverTotalMedioPago = function (item) {
            if (item == self) {
                if (confirm(self.dicionario.confirmacionExclusionTodos)) {
                    while (self.totalesMedioPago().length > 1) {
                        self.totalesMedioPago.shift();
                    }
                }
            } else {
                self.totalesMedioPago.remove(item);
            }
        };

        self.RemoverItemTermino = function (item) {
            if (item == self) {
                if (confirm(self.dicionario.confirmacionExclusionTodos)) {
                    while (self.totalMedioPagoEdicao().terminos().length > 1) {
                        self.totalMedioPagoEdicao().terminos.shift();
                    }
                }
            } else {
                self.totalMedioPagoEdicao().terminos.remove(item);
            }
        };

        self.editarMedioPago = function (item) {
            //ExibirTerminos();
            self.totalMedioPagoEdicao(item);
        };

        self.fecharDetalheTermino = function (item) {
            //EsconderTerminos();
            self.totalMedioPagoEdicao(null);
        };

        if (self.editavel) {
            self.totalesMedioPago.push(new TotalMedioPago(null, self));
        }

        self.totalesMedioPagoSerializar = ko.computed(function () {
            if (self.editavel) {
                //var _totalesMedioPago = self.totalesMedioPago.slice(0, self.totalesMedioPago().length - 1);
                //$.each(_totalesMedioPago, function (indice, total) {
                //    total.terminos(total.terminos.slice(0, total.terminos().length - 1));
                //});
                //return _totalesMedioPago;
                return self.totalesMedioPago.slice(0, self.totalesMedioPago().length - 1);
            } else {
                return self.totalesMedioPago();
            }
        });

        self.totalesMedioPagoGrilla = [];
        self.dummy = ko.observable();
        self.totalesMedioPagoGrilla = ko.computed(function () {

            self.dummy();

            var totalesModificar = [];
            var tDivisaModificar;

            $.each(self.totalesMedioPagoModificar, function (index, totalMedioPago) {
                if (totalMedioPago.divisa() === undefined)
                    return;

                var total;
                tDivisaModificar = undefined;

                for (var i = 0; i < totalesModificar.length; i++) {
                    if (totalesModificar[i].Identificador === totalMedioPago.divisa().Identificador) {
                        tDivisaModificar = totalesModificar[i];
                        for (var j = 0; j < tDivisaModificar.TotalesMedioPago.length; j++) {

                            if (tDivisaModificar.TotalesMedioPago[j].Identificador === parametros.TipoMedioPagoDisponivel) {
                                total = tDivisaModificar.TotalesMedioPago[j];
                            }
                        }
                    }
                }


                if (tDivisaModificar === undefined) {

                    tDivisaModificar = {
                        Identificador: totalMedioPago.divisa().Identificador,
                        TotalMoneda: totalMedioPago.divisa().CodigoSimbolo != null ? totalMedioPago.divisa().CodigoSimbolo + ' ' + IncluirFormato(valorCalculado, parametros) : IncluirFormato(valorCalculado, parametros),
                        Color: totalMedioPago.divisa().Color,
                        CodigoSimbolo: totalMedioPago.divisa().CodigoSimbolo,
                        Descripcion: total > 0 ? self.dicionario.importenegativodetallar + " " : self.dicionario.importepositivodetallar + " ",
                        TotalesMedioPago: []
                    }
                    totalesModificar.push(tDivisaModificar);

                }

                if (total === undefined) {

                    var valorCalculado = 0;

                    if (totalMedioPago.valor() != null) {
                        valorCalculado = parseFloat(RemoverFormato(totalMedioPago.valor(), parametros));

                        if (isNaN(valorCalculado)) {
                            valorCalculado = 0;
                        }
                    }

                    if (valorCalculado > 0) {
                        tDivisaModificar.TotalesMedioPago.push({
                            Identificador: parametros.TipoMedioPagoDisponivel,
                            TotalMoneda: totalMedioPago.divisa().CodigoSimbolo != null ? totalMedioPago.divisa().CodigoSimbolo + ' ' + IncluirFormato(valorCalculado, parametros) : IncluirFormato(valorCalculado, parametros),
                            Total: valorCalculado,
                            Codigo: 1
                        });
                    }
                    else {
                        tDivisaModificar.TotalesMedioPago.push({
                            Identificador: parametros.TipoMedioPagoDisponivel,
                            TotalMoneda: totalMedioPago.divisa().CodigoSimbolo != null ? totalMedioPago.divisa().CodigoSimbolo + ' ' + IncluirFormato(valorCalculado, parametros) : IncluirFormato(valorCalculado, parametros),
                            Total: valorCalculado,
                            Codigo: -1
                        });
                    };

                }
                else {
                    var valorCalculado = 0;
                    if (totalMedioPago.valor() != null) {
                        valorCalculado = parseFloat(RemoverFormato(totalMedioPago.valor(), parametros));

                        if (isNaN(valorCalculado)) {
                            valorCalculado = 0;
                        }
                    }

                    if (valorCalculado > 0) {

                        if (tDivisaModificar.TotalesMedioPago.length == 1) {
                            // se valor existente for positivo
                            if (tDivisaModificar.TotalesMedioPago[0].Codigo == 1) {
                                // pode somar com valor existente
                                total.Total = parseFloat(parseFloat(total.Total) + valorCalculado).toFixed(2);
                            }
                            else
                                // deve-se criar outra linha para o valor negativo
                            {
                                tDivisaModificar.TotalesMedioPago.push({
                                    Identificador: parametros.TipoMedioPagoDisponivel,
                                    TotalMoneda: tDivisaModificar.CodigoSimbolo != null ? tDivisaModificar.CodigoSimbolo + ' ' + IncluirFormato(valorCalculado, parametros) : IncluirFormato(valorCalculado, parametros),
                                    Total: valorCalculado,
                                    Codigo: 1
                                });
                            }
                        }
                        else if (tDivisaModificar.TotalesMedioPago.length == 2) {
                            // se valor existente for positivo
                            if (tDivisaModificar.TotalesMedioPago[0].Codigo == 1) {
                                total = tDivisaModificar.TotalesMedioPago[0];
                                total.Total = parseFloat(parseFloat(total.Total) + valorCalculado).toFixed(2);
                            }
                            else {
                                total = tDivisaModificar.TotalesMedioPago[1];
                                total.Total = parseFloat(parseFloat(total.Total) + valorCalculado).toFixed(2);
                            }
                        }

                    } else {
                        if (tDivisaModificar.TotalesMedioPago.length == 1) {
                            // se valor existente for positivo
                            if (tDivisaModificar.TotalesMedioPago[0].Codigo == -1) {
                                // pode somar com valor existente
                                total.Total = parseFloat(parseFloat(total.Total) + valorCalculado).toFixed(2);
                            }
                            else
                                // deve-se criar outra linha para o valor negativo
                            {
                                tDivisaModificar.TotalesMedioPago.push({
                                    Identificador: parametros.TipoMedioPagoDisponivel,
                                    TotalMoneda: tDivisaModificar.CodigoSimbolo != null ? tDivisaModificar.CodigoSimbolo + ' ' + IncluirFormato(valorCalculado, parametros) : IncluirFormato(valorCalculado, parametros),
                                    Total: valorCalculado,
                                    Codigo: -1
                                });
                            }
                        }
                        else if (tDivisaModificar.TotalesMedioPago.length == 2) {
                            // se valor existente for positivo
                            if (tDivisaModificar.TotalesMedioPago[0].Codigo == -1) {
                                total = tDivisaModificar.TotalesMedioPago[0];
                                total.Total = parseFloat(parseFloat(total.Total) + valorCalculado).toFixed(2);
                            }
                            else {
                                total = tDivisaModificar.TotalesMedioPago[1];
                                total.Total = parseFloat(parseFloat(total.Total) + valorCalculado).toFixed(2);
                            }
                        }
                    };

                    if (tDivisaModificar != null) {
                        total.TotalMoneda = tDivisaModificar.CodigoSimbolo != null ? tDivisaModificar.CodigoSimbolo + ' ' + IncluirFormato(total.Total, parametros) : IncluirFormato(total.Total, parametros);
                    }
                    else if (totalMedioPago != null && totalMedioPago.divisa != null) {
                        total.TotalMoneda = totalMedioPago.divisa().CodigoSimbolo != null ? totalMedioPago.divisa().CodigoSimbolo + ' ' + IncluirFormato(total.Total, parametros) : IncluirFormato(total.Total, parametros);
                    }
                    else {
                        total.TotalMoneda = IncluirFormato(total.Total, parametros)
                    };
                }

            });

            // VM TOTALES ORIGINAL

            var totales = [];
            var tDivisa;

            $.each(self.totalesMedioPago(), function (index, totalMedioPago) {
                if (totalMedioPago.divisa() === undefined)
                    return;

                var total;
                tDivisa = undefined;

                for (var i = 0; i < totales.length; i++) {
                    if (totales[i].Identificador === totalMedioPago.divisa().Identificador) {
                        tDivisa = totales[i];
                        for (var j = 0; j < tDivisa.TotalesMedioPago.length; j++) {

                            if (tDivisa.TotalesMedioPago[j].Identificador === parametros.TipoMedioPagoDisponivel) {
                                total = tDivisa.TotalesMedioPago[j];
                            }
                        }
                    }
                }


                if (tDivisa === undefined) {

                    tDivisa = {
                        Identificador: totalMedioPago.divisa().Identificador,
                        TotalMoneda: totalMedioPago.divisa().CodigoSimbolo != null ? totalMedioPago.divisa().CodigoSimbolo + ' ' + IncluirFormato(valorCalculado, parametros) : IncluirFormato(valorCalculado, parametros),
                        Color: totalMedioPago.divisa().Color,
                        CodigoSimbolo: totalMedioPago.divisa().CodigoSimbolo,
                        Descripcion: total > 0 ? self.dicionario.importenegativodetallar + " " : self.dicionario.importepositivodetallar + " ",
                        TotalesMedioPago: []
                    }
                    totales.push(tDivisa);

                }

                if (total === undefined) {

                    var valorCalculado = 0;

                    if (totalMedioPago.valor() != null) {
                        valorCalculado = parseFloat(RemoverFormato(totalMedioPago.valor(), parametros));

                        if (isNaN(valorCalculado)) {
                            valorCalculado = 0;
                        }
                    }

                    if (valorCalculado > 0) {
                        tDivisa.TotalesMedioPago.push({
                            Identificador: parametros.TipoMedioPagoDisponivel,
                            TotalMoneda: totalMedioPago.divisa().CodigoSimbolo != null ? totalMedioPago.divisa().CodigoSimbolo + ' ' + IncluirFormato(valorCalculado, parametros) : IncluirFormato(valorCalculado, parametros),
                            Total: valorCalculado,
                            Codigo: 1
                        });
                    }
                    else {
                        tDivisa.TotalesMedioPago.push({
                            Identificador: parametros.TipoMedioPagoDisponivel,
                            TotalMoneda: totalMedioPago.divisa().CodigoSimbolo != null ? totalMedioPago.divisa().CodigoSimbolo + ' ' + IncluirFormato(valorCalculado, parametros) : IncluirFormato(valorCalculado, parametros),
                            Total: valorCalculado,
                            Codigo: -1
                        });
                    };

                }
                else {
                    var valorCalculado = 0;
                    if (totalMedioPago.valor() != null) {
                        valorCalculado = parseFloat(RemoverFormato(totalMedioPago.valor(), parametros));

                        if (isNaN(valorCalculado)) {
                            valorCalculado = 0;
                        }
                    }

                    if (valorCalculado > 0) {

                        if (tDivisa.TotalesMedioPago.length == 1) {
                            // se valor existente for positivo
                            if (tDivisa.TotalesMedioPago[0].Codigo == 1) {
                                // pode somar com valor existente
                                total.Total = parseFloat(parseFloat(total.Total) + valorCalculado).toFixed(2);
                            }
                            else
                                // deve-se criar outra linha para o valor negativo
                            {
                                tDivisa.TotalesMedioPago.push({
                                    Identificador: parametros.TipoMedioPagoDisponivel,
                                    TotalMoneda: tDivisa.CodigoSimbolo != null ? tDivisa.CodigoSimbolo + ' ' + IncluirFormato(valorCalculado, parametros) : IncluirFormato(valorCalculado, parametros),
                                    Total: valorCalculado,
                                    Codigo: 1
                                });
                            }
                        }
                        else if (tDivisa.TotalesMedioPago.length == 2) {
                            // se valor existente for positivo
                            if (tDivisa.TotalesMedioPago[0].Codigo == 1) {
                                total = tDivisa.TotalesMedioPago[0];
                                total.Total = parseFloat(parseFloat(total.Total) + valorCalculado).toFixed(2);
                            }
                            else {
                                total = tDivisa.TotalesMedioPago[1];
                                total.Total = parseFloat(parseFloat(total.Total) + valorCalculado).toFixed(2);
                            }
                        }

                    } else {
                        if (tDivisa.TotalesMedioPago.length == 1) {
                            // se valor existente for positivo
                            if (tDivisa.TotalesMedioPago[0].Codigo == -1) {
                                // pode somar com valor existente
                                total.Total = parseFloat(parseFloat(total.Total) + valorCalculado).toFixed(2);
                            }
                            else
                                // deve-se criar outra linha para o valor negativo
                            {
                                tDivisa.TotalesMedioPago.push({
                                    Identificador: parametros.TipoMedioPagoDisponivel,
                                    TotalMoneda: tDivisa.CodigoSimbolo != null ? tDivisa.CodigoSimbolo + ' ' + IncluirFormato(valorCalculado, parametros) : IncluirFormato(valorCalculado, parametros),
                                    Total: valorCalculado,
                                    Codigo: -1
                                });
                            }
                        }
                        else if (tDivisa.TotalesMedioPago.length == 2) {
                            // se valor existente for positivo
                            if (tDivisa.TotalesMedioPago[0].Codigo == -1) {
                                total = tDivisa.TotalesMedioPago[0];
                                total.Total = parseFloat(parseFloat(total.Total) + valorCalculado).toFixed(2);
                            }
                            else {
                                total = tDivisa.TotalesMedioPago[1];
                                total.Total = parseFloat(parseFloat(total.Total) + valorCalculado).toFixed(2);
                            }
                        }
                    };

                    if (tDivisa != null) {
                        total.TotalMoneda = tDivisa.CodigoSimbolo != null ? tDivisa.CodigoSimbolo + ' ' + IncluirFormato(total.Total, parametros) : IncluirFormato(total.Total, parametros);
                    }
                    else if (totalEfectivo != null && totalEfectivo.divisa != null) {
                        total.TotalMoneda = totalEfectivo.divisa().CodigoSimbolo != null ? totalEfectivo.divisa().CodigoSimbolo + ' ' + IncluirFormato(total.Total, parametros) : IncluirFormato(total.Total, parametros);
                    }
                    else {
                        total.TotalMoneda = IncluirFormato(total.Total, parametros)
                    };
                }

            });

            var totalesMezclados = CalcularValoresMediosPago(totales, totalesModificar);
            var totalesGeneral = AgruparValoresPositivosYNegativos(totalesMezclados, self.dicionario);

            return totalesGeneral;

        });

    }

    //#endregion

    //#region Item da ViewModel - Efetivo a detalhar

    function TotalMedioPago(total, modeloRaiz) {
        var self = this;
        var raiz = modeloRaiz;

        self.divisa = ko.observable().extend({ required: true });
        self.medioPagoDetallar = ko.observable(false);
        self.corLinha = ko.computed(function () {
            return (self.divisa() == null ? '' : self.divisa().Color);
        });

        self.mediosPago = ko.computed(function () {
            var retorno = [];
            if ((self.divisa() != null) && (raiz.tipoMedioPagoDisponivel != null)) {
                retorno = self.divisa().MediosPago.filter(function (medioPago) {
                    return medioPago.Tipo == raiz.tipoMedioPagoDisponivel;
                });
            }
            return retorno;
        });
        self.medioPago = ko.observable().extend({
            required: {
                onlyIf: function () {
                    return raiz.tipoMedioPagoDisponivel != null;
                }
            }
        });
        self.medioPago.subscribe(function (novovalor) {
            atualizaTotalesMedioPago(raiz);
        });
        self.habilitarMedioPago = ko.computed(function () {
            return (self.divisa() != null && raiz.tipoMedioPagoDisponivel != null);
        });

        self.HabilitarCantidadValor = ko.computed(function () {
            if (raiz.trabajarConUnidadMedida && self.medioPago() != null) {
                return true;
            }
            else if (!raiz.trabajarConUnidadMedida && self.medioPago() != null) {
                return true;
            }
            else {
                return false;
            }
        });

        self.cantidad = ko.observable();
        self.cantidad.subscribe(function (novovalor) {
            atualizaTotalesMedioPago(raiz);
        });
        self.valor = ko.observable();

        self.valor.subscribe(function (valor) {

            if (raiz.inicializar == true && parametros.Modo != 'Consulta') {

                var novoValor = parseFloat(RemoverFormato(valor, parametros));

                if (raiz.totalesMedioPagoModificar != null && raiz.totalesMedioPagoModificar.length > 0) {

                    if (novoValor > 0) {

                        var tDivisaModificar;
                        var valorTotalModificar;

                        $.each(raiz.totalesMedioPagoModificar, function (index, totalMedioPago) {
                            if (totalMedioPago.divisa() === undefined)
                                return;


                            var totalMedioPagoValor = 0;

                            if (totalMedioPago.divisa().Identificador == self.divisa().Identificador) {
                                tDivisaModificar = totalMedioPago;

                                totalMedioPagoValor = parseFloat(RemoverFormato(totalMedioPago.valor(), parametros))
                                valorTotalModificar = parseFloat(RemoverFormato(valorTotalModificar, parametros))

                                if (totalMedioPagoValor > 0) {
                                    valorTotalModificar = valorTotalModificar + totalMedioPagoValor;
                                };

                                return;
                            };


                        });

                        if (tDivisaModificar != null) {

                            // se existem valores detalhados
                            if (raiz.totalesMedioPago != null && raiz.totalesMedioPago().length > 0) {

                                var valorTotalDetallar = 0;

                                $.each(raiz.totalesMedioPago(), function (index, totalMedioPago) {
                                    if (totalMedioPago.divisa() === undefined)
                                        return;

                                    if (totalMedioPago.divisa().Identificador == self.divisa().Identificador) {

                                        var totalMedioPagoValor = parseFloat(RemoverFormato(totalMedioPago.valor(), parametros))
                                        valorTotalDetallar = parseFloat(RemoverFormato(valorTotalDetallar, parametros))

                                        if (totalMedioPagoValor > 0) {
                                            valorTotalDetallar = valorTotalDetallar + totalMedioPagoValor;
                                        };
                                    };
                                });


                                if (valorTotalDetallar > 0) {

                                    if (valorTotalDetallar > valorTotalModificar) {
                                        self.cantidad(null);
                                        self.valor(null);
                                    };

                                } else {
                                    if (novoValor > valorTotalModificar) {
                                        self.cantidad(null);
                                        self.valor(null);
                                    };
                                };
                            }
                            else {
                                if (valorTotalModificar > 0) {
                                    if (novoValor > valorTotalModificar) {
                                        self.cantidad(null);
                                        self.valor(null);
                                    };
                                };
                            };
                        }
                        else {
                            self.cantidad(null);
                            self.valor(null);
                        };

                    }

                    else {

                        var tDivisaModificar;
                        var valorTotalModificar = 0;

                        $.each(raiz.totalesMedioPagoModificar, function (index, totalMedioPago) {
                            if (totalMedioPago.divisa() === undefined)
                                return;

                            var totalMedioPagoValor = parseFloat(RemoverFormato(totalMedioPago.valor(), parametros))
                            valorTotalModificar = parseFloat(RemoverFormato(valorTotalModificar, parametros))

                            if (totalMedioPagoValor < 0) {
                                valorTotalModificar = valorTotalModificar + totalMedioPagoValor;
                                tDivisaModificar = totalMedioPago;
                            };
                        });

                        var tValorNegativoDivisaModificar = parseFloat(RemoverFormato(valorTotalModificar, parametros));

                        if (tDivisaModificar != null) {

                            // se existem valores detalhados
                            if (raiz.totalesMedioPago != null && raiz.totalesMedioPago().length > 0) {

                                var valorTotalDetallar = 0;

                                $.each(raiz.totalesMedioPago(), function (index, totalMedioPago) {
                                    if (totalMedioPago.divisa() === undefined)
                                        return;

                                    var totalMedioPagoValor = parseFloat(RemoverFormato(totalMedioPago.valor(), parametros))
                                    valorTotalDetallar = parseFloat(RemoverFormato(valorTotalDetallar, parametros))

                                    if (totalMedioPagoValor < 0) {
                                        valorTotalDetallar = valorTotalDetallar + totalMedioPagoValor;
                                    };
                                });

                                if (valorTotalDetallar < 0) {

                                    if (valorTotalDetallar < tValorNegativoDivisaModificar) {
                                        self.cantidad(null);
                                        self.valor(null);
                                    };

                                } else {
                                    if (novoValor < tValorNegativoDivisaModificar) {
                                        self.cantidad(null);
                                        self.valor(null);
                                    };
                                };
                            }
                            else {
                                if (tValorNegativoDivisaModificar < 0) {
                                    if (novoValor > tValorNegativoDivisaModificar) {
                                        self.cantidad(null);
                                        self.valor(null);
                                    };
                                };
                            };
                        }
                        else {
                            self.cantidad(null);
                            self.valor(null);
                        };
                    };

                }
                else {
                    self.cantidad(null);
                    self.valor(null);
                };
            };
        });

        //self.valor.subscribe(function (valor) {

        //    if (raiz.inicializar == true && parametros.Modo != 'Consulta') {

        //        var novoValor = parseFloat(RemoverFormato(valor, parametros));

        //        if (raiz.totalesMedioPagoModificar != null && raiz.totalesMedioPagoModificar.length > 0) {

        //            var tDivisaModificar;

        //            $.each(raiz.totalesMedioPagoModificar, function (index, totalMedioPago) {
        //                if (totalMedioPago.divisa() === undefined) {
        //                    return;
        //                };

        //                if (totalMedioPago.divisa().Identificador == self.divisa().Identificador) {
        //                    tDivisaModificar = totalMedioPago;
        //                    return;
        //                };

        //            });

        //            if (tDivisaModificar != null) {

        //                var tValorDivisaModificar = parseFloat(RemoverFormato(tDivisaModificar.valor(), parametros));

        //                if (novoValor > 0) {

        //                    // se existem valores detalhados
        //                    if (raiz.totalesMedioPago != null && raiz.totalesMedioPago().length > 0) {

        //                        var valorTotalDetallar = 0;

        //                        $.each(raiz.totalesMedioPago(), function (index, totalMedioPago) {
        //                            if (totalMedioPago.divisa() === undefined)
        //                                return;

        //                            if (totalMedioPago.divisa().Identificador == self.divisa().Identificador) {

        //                                var totalMPValor = parseFloat(RemoverFormato(totalMedioPago.valor(), parametros))
        //                                valorTotalDetallar = parseFloat(RemoverFormato(valorTotalDetallar, parametros))

        //                                if (parseFloat(totalMedioPago.valor()) > 0) {
        //                                    valorTotalDetallar = valorTotalDetallar + totalMPValor;
        //                                };
        //                            };
        //                        });


        //                        if (valorTotalDetallar > 0) {

        //                            if (valorTotalDetallar > tValorDivisaModificar) {
        //                                self.cantidad(null);
        //                                self.valor(null);
        //                            };

        //                        } else {
        //                            if (novoValor > tValorDivisaModificar) {
        //                                self.cantidad(null);
        //                                self.valor(null);
        //                            };
        //                        };
        //                    }
        //                    else {
        //                        if (tValorDivisaModificar > 0) {
        //                            if (novoValor > tValorDivisaModificar) {
        //                                self.cantidad(null);
        //                                self.valor(null);
        //                            };
        //                        };
        //                    };
        //                }
        //                else {

        //                    // se existem valores detalhados
        //                    if (raiz.totalesMedioPago != null && raiz.totalesMedioPago().length > 0) {

        //                        var valorTotalDetallar = 0;

        //                        $.each(raiz.totalesMedioPago(), function (index, totalMedioPago) {
        //                            if (totalMedioPago.divisa() === undefined)
        //                                return;

        //                            if (totalMedioPago.divisa().Identificador == self.divisa().Identificador) {

        //                                var totalMPValor = parseFloat(RemoverFormato(totalMedioPago.valor(), parametros))
        //                                valorTotalDetallar = parseFloat(RemoverFormato(valorTotalDetallar, parametros))

        //                                if (totalMPValor < 0) {
        //                                    valorTotalDetallar = valorTotalDetallar + totalMPValor;
        //                                };
        //                            };
        //                        });


        //                        if (valorTotalDetallar < 0) {

        //                            if (valorTotalDetallar < tValorDivisaModificar) {
        //                                self.cantidad(null);
        //                                self.valor(null);
        //                            };

        //                        } else {
        //                            if (novoValor < tValorDivisaModificar) {
        //                                self.cantidad(null);
        //                                self.valor(null);
        //                            };
        //                        };
        //                    }
        //                    else {
        //                        if (tValorDivisaModificar < 0) {
        //                            if (novoValor < tValorDivisaModificar) {
        //                                self.cantidad(null);
        //                                self.valor(null);
        //                            };
        //                        };
        //                    };

        //                };

        //            }
        //            else {
        //                self.cantidad(null);
        //                self.valor(null);
        //            };
        //        }
        //        else {
        //            self.cantidad(null);
        //            self.valor(null);
        //        };
        //    };
        //});

        self.terminos = ko.observableArray([]);
        self.terminos.subscribe(function (novovalor) {
            atualizaTotalesMedioPago(raiz);
        });
        if (total != null) {
            if (total.Divisa != null) {
                self.divisa(raiz.divisasDisponiveis.filter(function (divisaDisponivel) {
                    return divisaDisponivel.Identificador == total.Divisa.Identificador;
                })[0]);
                self.divisa.isModified(true);
            }
            if (total.MedioPago != null) {
                self.medioPago(self.mediosPago().filter(function (medioPagoDisponivel) {
                    return medioPagoDisponivel.Identificador == total.MedioPago.Identificador;
                })[0]);
                self.medioPago.isModified(true);
            }
            if (total.Cantidad != 0) {
                self.cantidad(total.Cantidad);
            }
            if (total.Valor != 0) {
                self.valor(IncluirFormato(total.Valor, parametros));
            }
            if (total.Terminos != null && total.Terminos.length > 0) {
                $.each(total.Terminos, function (indice, terminos) {
                    self.terminos.push($.map(terminos, function (termino) {
                        return new TerminoMedioPago(termino, self, raiz);
                    }));
                });
            }
        }
        self.habilitarValoresCalculados = ko.computed(function () {
            var retorno = true;
            if (self.medioPago() == null) {
                retorno = false;
            } else {
                if (self.medioPago().Terminos != null) {
                    $.each(self.medioPago().Terminos, function (indice, termino) {
                        if (termino.EstaActivo) {
                            retorno = false;
                        }
                    });
                }
            }
            return retorno;
        });
        self.detallarTerminos = ko.computed(function () {
            return (self.medioPago() != null) && !self.habilitarValoresCalculados();
        });
        self.terminosDisponiveis = ko.computed(function () {
            var retorno;
            if (self.medioPago() != null && self.medioPago().Terminos != null) {
                retorno = $.map(self.medioPago().Terminos.filter(function (termino) {
                    return termino.EstaActivo;
                }), function (termino) {
                    return new ObjetoNaoSerializavel(termino);
                });
            } else {
                retorno = [];
            }
            return retorno;
        });

        var resetaListaTerminos = function (limpar) {
            if (limpar != false) {
                self.terminos.removeAll();
            }
            if (raiz.editavel) {
                var novaLista = [];
                var terminosDisp = self.terminosDisponiveis();
                for (var i = 0; i < terminosDisp.length; i++) {
                    novaLista.push(new TerminoMedioPago($.extend(true, {}, terminosDisp[i]), null, null));
                }
                if (novaLista.length > 0) {
                    self.terminos.push(novaLista);
                }
            }
        };

        self.medioPago.subscribe(resetaListaTerminos);

        resetaListaTerminos(false);

        this.toJSON = function () {
            return {
                Divisa: self.divisa(),
                Descripcion: self.divisa().Descripcion,
                MedioPago: new ObjetoNaoSerializavel(self.medioPago()),
                TipoMedioPago: raiz.tipoMedioPagoDisponivel,
                Cantidad: self.cantidad(),
                Valor: self.valor(),
                Terminos: self.terminos()
            };
        };

    }

    //#endregion

    //Calcular diferença entre os valores selecionados e valores imputados
    function CalcularValoresMediosPago(totales, totalesModificar) {

        var retorno = [];
        var valoresCargar = [];

        if ((totalesModificar != null || totalesModificar.length > 0) && (totales == null || totales.length == 0)) {
            return totalesModificar;

        }
        else if ((totales != null || totales.length > 0) && (totalesModificar == null || totalesModificar.length == 0)) {
            return totales;

        }
        else {

            valoresCargar = totalesModificar;

            for (var i = 0; i < totalesModificar.length; i++) {

                var totalDetallar = totales.filter(function (divisa) {
                    return divisa.Identificador == totalesModificar[i].Identificador;
                })[0];

                //#region Existe totalModificado para Divisa

                if (totalDetallar != null && totalDetallar.TotalesMedioPago != null && totalDetallar.TotalesMedioPago.length > 0) {

                    // For - totales Medios Pago

                    for (var j = 0; j < totalesModificar[i].TotalesMedioPago.length; j++) {

                        var valorCalculado = totalesModificar[i].TotalesMedioPago[j].Total;

                        // valor positivo
                        if (valorCalculado > 0) {

                            var valorDetallarPositivo = totalDetallar.TotalesMedioPago.filter(function (val) {
                                return val.Total > 0;
                            })[0];

                            if (valorDetallarPositivo != null) {
                                valorCalculado = valorDetallarPositivo.Total - totalesModificar[i].TotalesMedioPago[j].Total;
                            };

                            valorCalculado = Math.abs(valorCalculado);

                        }
                            // valor negativo
                        else if (valorCalculado < 0) {

                            var valorDetallarNegativo = totalDetallar.TotalesMedioPago.filter(function (val) {
                                return val.Total < 0;
                            })[0];

                            if (valorDetallarNegativo != null) {
                                valorCalculado = valorDetallarNegativo.Total - totalesModificar[i].TotalesMedioPago[j].Total;
                            };

                            valorCalculado = valorCalculado > 0 ? (-1) * (valorCalculado) : valorCalculado;

                        };

                        var tDivisa = {
                            Identificador: totalesModificar[i].Identificador,
                            TotalMoneda: totalesModificar[i].TotalMoneda,
                            Color: totalesModificar[i].Color,
                            CodigoSimbolo: totalesModificar[i].CodigoSimbolo,
                            Descripcion: totalesModificar[i].Descripcion,
                            TotalesMedioPago: []
                        }

                        tDivisa.TotalesMedioPago.push({
                            Identificador: parametros.TipoMedioPagoDisponivel,
                            TotalMoneda: totalesModificar[i].CodigoSimbolo != null ? totalesModificar[i].CodigoSimbolo + ' ' + IncluirFormato(valorCalculado, parametros) : IncluirFormato(valorCalculado, parametros),
                            Total: valorCalculado
                        });

                        retorno.push(tDivisa);

                    };
                }
                    //#endregion

                    //#region Não existe totalModificado para Divisa
                else {
                    var tDivisa = {
                        Identificador: totalesModificar[i].Identificador,
                        TotalMoneda: totalesModificar[i].TotalMoneda,
                        Color: totalesModificar[i].Color,
                        CodigoSimbolo: totalesModificar[i].CodigoSimbolo,
                        Descripcion: totalesModificar[i].Descripcion,
                        TotalesMedioPago: totalesModificar[i].TotalesMedioPago
                    }
                    retorno.push(tDivisa);
                };

                //#endregion

            };
        };

        return retorno;
    }

    // Agrupa os valores das divisas, porque nos controles de meio de pagamento os valores totais das divisas ficam na mesma linha
    function AgruparValoresPositivosYNegativos(totales, dicionario) {

        var totalesGeneral = [];
        var tTotalesGeneral;

        //#region For - Totales - Formará 2 linhas (Positivo / Negativo)
        for (var i = 0; i < totales.length; i++) {

            //#region For - Totales Medios Pago

            for (var j = 0; j < totales[i].TotalesMedioPago.length; j++) {

                if (totales[i].TotalesMedioPago[j].Total > 0) {

                    if (totalesGeneral.length < 2) {
                        if (totalesGeneral.length == 0) {
                            tTotalesGeneral = {
                                Codigo: 1,
                                Descripcion: dicionario.importepositivodetallar + " ",
                                Valores: []
                            }
                            totalesGeneral.push(tTotalesGeneral);
                        }
                        else {
                            if (totalesGeneral[0].Codigo == -1) {
                                tTotalesGeneral = {
                                    Codigo: 1,
                                    Descripcion: dicionario.importepositivodetallar + " ",
                                    Valores: []
                                }
                                totalesGeneral.push(tTotalesGeneral);
                            }

                        }
                        { continue; }
                    } else { break; }

                }
                else if (totales[i].TotalesMedioPago[j].Total < 0) {

                    if (totalesGeneral.length < 2) {
                        if (totalesGeneral.length == 0) {
                            tTotalesGeneral = {
                                Codigo: -1,
                                Descripcion: dicionario.importenegativodetallar + " ",
                                Valores: []
                            }
                            totalesGeneral.push(tTotalesGeneral);
                        }
                        else {
                            if (totalesGeneral[0].Codigo == 1) {
                                tTotalesGeneral = {
                                    Codigo: -1,
                                    Descripcion: dicionario.importenegativodetallar + " ",
                                    Valores: []
                                }
                                totalesGeneral.push(tTotalesGeneral);
                            }
                        }
                        { continue; }
                    } else { break; }
                }
                else {

                    if (totalesGeneral.length < 2) {
                        if (totalesGeneral.length == 0) {
                            tTotalesGeneral = {
                                Codigo: -1,
                                Descripcion: dicionario.importenegativodetallar + " ",
                                Valores: []
                            }
                            totalesGeneral.push(tTotalesGeneral);
                            tTotalesGeneral = {
                                Codigo: 1,
                                Descripcion: dicionario.importepositivodetallar + " ",
                                Valores: []
                            }
                            totalesGeneral.push(tTotalesGeneral);
                        }
                        else {
                            if (totalesGeneral[0].Codigo == 1) {
                                tTotalesGeneral = {
                                    Codigo: -1,
                                    Descripcion: dicionario.importenegativodetallar + " ",
                                    Valores: []
                                }
                                totalesGeneral.push(tTotalesGeneral);
                            }
                            else {
                                tTotalesGeneral = {
                                    Codigo: 1,
                                    Descripcion: dicionario.importepositivodetallar + " ",
                                    Valores: []
                                }
                                totalesGeneral.push(tTotalesGeneral);
                            }
                        }
                        { continue; }
                    } else { break; }

                }
            }

            //#endregion

        }

        //#endregion

        //#region For - Totales - Agrupará as divisas na mesma linha

        for (var i = 0; i < totales.length; i++) {

            //#region For - TotalesMedioPago

            for (var j = 0; j < totales[i].TotalesMedioPago.length; j++) {

                // se valor corrente for 'positivo'
                if (totales[i].TotalesMedioPago[j].Total > 0) {
                    // verifica se existe somente uma linha nos 'totalesgenerales'
                    if (totalesGeneral.length == 1) {
                        if (totalesGeneral[0].Codigo == 1) {
                            totalesGeneral[0].Valores.push({
                                Valor: totales[i].TotalesMedioPago[j].TotalMoneda + ";",
                                Cor: totales[i].Color
                            });
                        }
                    }
                        // verificar se 'totales' possui valores positivos e negativos
                    else if (totalesGeneral.length == 2) {
                        if (totalesGeneral[0].Codigo == 1) {
                            totalesGeneral[0].Valores.push({
                                Valor: totales[i].TotalesMedioPago[j].TotalMoneda + ";",
                                Cor: totales[i].Color
                            });
                        }
                        else {
                            totalesGeneral[1].Valores.push({
                                Valor: totales[i].TotalesMedioPago[j].TotalMoneda + ";",
                                Cor: totales[i].Color
                            });
                        }
                    }
                }
                else if (totales[i].TotalesMedioPago[j].Total < 0) {
                    // verifica se existe somente uma linha nos 'totalesgenerales'
                    if (totalesGeneral.length == 1) {
                        if (totalesGeneral[0].Codigo == -1) {
                            totalesGeneral[0].Valores.push({
                                Valor: totales[i].TotalesMedioPago[j].TotalMoneda + ";",
                                Cor: totales[i].Color
                            });
                        }
                    }
                        // verificar se 'totales' possui valores positivos e negativos
                    else if (totalesGeneral.length == 2) {
                        if (totalesGeneral[0].Codigo == -1) {
                            totalesGeneral[0].Valores.push({
                                Valor: totales[i].TotalesMedioPago[j].TotalMoneda + ";",
                                Cor: totales[i].Color
                            });
                        }
                        else {
                            totalesGeneral[1].Valores.push({
                                Valor: totales[i].TotalesMedioPago[j].TotalMoneda + ";",
                                Cor: totales[i].Color
                            });
                        }
                    }
                }
                else {
                    if (totalesGeneral.length == 2) {
                        totalesGeneral[0].Valores.push({
                            Valor: totales[i].TotalesMedioPago[j].TotalMoneda + ";",
                            Cor: totales[i].Color
                        });
                        totalesGeneral[1].Valores.push({
                            Valor: totales[i].TotalesMedioPago[j].TotalMoneda + ";",
                            Cor: totales[i].Color
                        });
                    }
                }
            }
            //#endregion
        }

        //#endregion

        return totalesGeneral;

    }

    function TerminoMedioPago(termino, modeloTotal, modeloRaiz) {
        var self = this;
        var totalMedioPago = modeloTotal;
        var raizMedioPago = modeloRaiz;

        self.dicionario = parametros.Dicionario;

        for (propriedade in termino) {
            if (termino.hasOwnProperty(propriedade)) {
                if (termino[propriedade] instanceof Array) {
                    this[propriedade] = ko.observableArray(termino[propriedade]);
                } else {
                    this[propriedade] = ko.observable(termino[propriedade]);
                }
            }
        }

        if (self.Mascara() != null && self.Mascara().ExpresionRegular != null) {
            self.Valor.extend({ pattern: { message: self.dicionario.mascaratermino, params: new RegExp(self.Mascara().ExpresionRegular) } });

        }
        if (self.Formato().Codigo == "6") {
            if (self.Valor() != null) {
                self.Valor($.parseJSON(self.Valor().toLowerCase()));
            }
        }

        this.toJSON = function () {
            return {
                Identificador: this.Identificador(),
                Valor: this.Valor()
            };
        };
    }

    var vm = new TotalesMedioPagoViewModelDetallar();
    // Instância da função ControlarSaldo para realizar a comunicação entre as ViewModel's (saldoModificar  ->  saldoModificado)
    var saldo = GetControlarSaldo();
    saldo.RegistrarVMSaldoMedioPagoDetallar(vm);

    //Registra hiddenfield valores
    vm.totalesMedioPagoGrilla.subscribe(function (novovalor) {
        atualizaTotalesMedioPago(vm);
    });

    //Bind ViewModel
    ko.applyBindings(vm, $("#" + totalesMedioPago).get(0));

}

function atualizaTotalesMedioPago(raiz) {
    var jsonValor = ko.toJSON(raiz.totalesMedioPagoSerializar);
    var hdfVal = $("#" + raiz.clienteID + "_hdftotalesMedioPago").val().split(";");

    $("#" + raiz.clienteID + "_hdftotalesMedioPago").val(hdfVal[0] + ";" + jsonValor);
}
//#endregion

/*
 * Determines if a field is required or not based on a function or value
 * Parameter: boolean function, or boolean value
 * Example
 *
 * viewModel = {
 *   var vm = this;
 *   vm.isRequired = ko.observable(false);
 *   vm.requiredField = ko.observable().extend({ conditional_required: vm.isRequired});
 * }   
*/
ko.validation.rules['conditional_required'] = {
    validator: function (val, condition) {
        var required = false;
        if (typeof condition == 'function') {
            required = condition();
        }
        else {
            required = condition;
        }

        if (required) {
            return !(val == undefined || val == null || val.length == 0);
        }
        else {
            return true;
        }
    },
    message: ko.validation.rules.required.message
}

/*
 * This rules checks the given array of objects/observables and returns 
 * true if at least one of the elements validates agains the the default
 * 'required' rules
 * 
 * Example:
 * 
 *
 * self.mobilePhone.extend({ requiresOneOf: [self.homePhone, self.mobilePhone] });
 * self.homePhone.extend({ requiresOneOf: [self.homePhone, self.mobilePhone] }); 
 *
*/
ko.validation.rules['requiresOneOf'] = {
    getValue: function (o) {
        return (typeof o === 'function' ? o() : o);
    },
    validator: function (val, options) {
        var self = this;

        var anyOne = ko.utils.arrayFirst(options.fields, function (field) {
            var stringTrimRegEx = /^\s+|\s+$/g,
                      testVal;

            var val = self.getValue(field);

            if (val === undefined || val === null)
                return !options.required;

            testVal = val;
            if (typeof (val) == "string") {
                testVal = val.replace(stringTrimRegEx, '');
            }

            return ((testVal + '').length > 0);

        });

        return (anyOne != null);
    },
    message: 'One of these fields is required'
};

ko.bindingHandlers['if'] = (function () {
    var cloneNodes = function (nodesArray, shouldCleanNodes) {
        for (var i = 0, j = nodesArray.length, newNodesArray = []; i < j; i++) {
            var clonedNode = nodesArray[i].cloneNode(true);
            newNodesArray.push(shouldCleanNodes ? ko.cleanNode(clonedNode) : clonedNode);
        }
        return newNodesArray;
    };

    return {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            var trueNodes = [], falseNodes = [], isFirstRender = true, wasLastTrue;

            function saveTemplates() {
                var depth = 0;
                var target = trueNodes;
                ko.utils.arrayForEach(ko.virtualElements.childNodes(element), function (node) {
                    if (node.nodeType === 8) {
                        if (node.text) {
                            if (/<!--\s*ko\b/.test(node.text)) {
                                depth += 1;
                            } else if (/<!--\s*\/ko\b/.test(node.text)) {
                                depth -= 1;
                            } else if (depth === 0 && /<!--\s*else\s*-->/.test(node.text)) {
                                target = falseNodes;
                                return;
                            }
                        } else {
                            if (/^\s*ko\b/.test(node.nodeValue)) {
                                depth += 1;
                            } else if (/^\s*\/ko\b/.test(node.nodeValue)) {
                                depth -= 1;
                            } else if (depth === 0 && /\s*else\s*/.test(node.nodeValue)) {
                                target = falseNodes;
                                return;
                            }
                        }
                    }

                    target.push(node);
                });
                trueNodes = cloneNodes(trueNodes, true /* shouldCleanNodes */);
                falseNodes = cloneNodes(falseNodes, true /* shouldCleanNodes */);
            }

            ko.computed(function () {
                var dataValue = ko.utils.unwrapObservable(valueAccessor()),
                    isTrue = !!dataValue,    // Cast value to boolean
                    renderNodes = isTrue ? trueNodes : falseNodes;  // Set before templates are saved so that the initial bind is to the original nodes

                if (isFirstRender || isTrue !== wasLastTrue) {
                    if (isFirstRender) {
                        saveTemplates();
                    } else {
                        renderNodes = cloneNodes(renderNodes);
                    }

                    ko.virtualElements.setDomNodeChildren(element, renderNodes);
                    ko.applyBindingsToDescendants(bindingContext, element);

                    wasLastTrue = isTrue;
                }
                isFirstRender = false;

            }, null, {
                disposeWhenNodeIsRemoved: element
            });

            return { controlsDescendantBindings: true };
        }
    };
}());