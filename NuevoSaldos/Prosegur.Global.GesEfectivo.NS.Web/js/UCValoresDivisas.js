function InicializaUCValoresDivisas(parametros, totalesDivisas, totalesEfectivo, totalesMedioPago) {

    function RemoverFormato(valor) {

        if (valor.length > 0) {
            var sd = parametros.SeparadorDecimal;
            var sm = parametros.SeparadorMilhar;
            var separadorDecimal = valor.substr(valor.length - 3);
            var valorInteiro;
            var valorDecimal;
            if (separadorDecimal.length > 1) {
                for (var i = 0; i < separadorDecimal.length; i++) {
                    if (separadorDecimal.charAt(i) == ".") {
                        existeDecimal = true;
                        var valores = valor.toString().split(".");

                        //retira a virgula do número inteiro.
                        valorInteiro = valores[0].replace(/\,/g, '');
                        valorDecimal = valores[1];
                        if (valorDecimal.length == 1) {
                            valorDecimal = valorDecimal + "0";
                        }

                        valor = valorInteiro + "." + valorDecimal;
                        break;
                    }
                    else if (separadorDecimal.charAt(i) == ",") {
                        existeDecimal = true;
                        valores = valor.toString().split(",");

                        //retira o ponto do valor inteiro
                        valorInteiro = valores[0].replace(/\./g, '');
                        valorDecimal = valores[1];
                        if (valorDecimal.length == 1) {
                            valorDecimal = valorDecimal + "0";
                        }

                        valor = valorInteiro + "." + valorDecimal;
                        break;
                    }
                }
            }
            else {
                //retira o ponto
                valor = valor.replace(/\./g, '');

                //retira a vírgula
                valor = valor.replace(/\,/g, '');
            }
        }

        if (valor == '') {
            valor = '0';
        }

        return valor;
    };

    function IncluirFormato(valor) {

        if (valor === undefined)
            return 0;

        if (valor != 0) {
            var sd = parametros.SeparadorDecimal;
            var sm = parametros.SeparadorMilhar;
            valor = RemoverFormato(valor);

            var valores = valor.toString().split(".");
            if (valores.length == 1) {
                valor = valores[0] + "00";
            }
            else if (valores.length == 2) {

                if (valores[1].length == 1) {
                    valores[1] = valores[1] + "0";
                }

                valor = valores[0] + valores[1];
            }

            valor = valor.replace(/(\d{1})(\d{11})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 8 digitos
            valor = valor.replace(/(\d{1})(\d{8})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 8 digitos
            valor = valor.replace(/(\d{1})(\d{5})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 5 digitos
            valor = valor.replace(/(\d{1})(\d{0,2})$/, "$1" + sd + "$2") //coloca virgula antes dos últimos 2 digitos
        }

        return valor;
    };

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

    parametros.UnidadesMedidaDisponiveis = $.map(parametros.UnidadesMedidaDisponiveis, function (unidadeMedida) {
        return new ObjetoNaoSerializavel(unidadeMedida);
    });

    parametros.CalidadesDisponiveis = $.map(parametros.CalidadesDisponiveis, function (calidad) {
        return new ObjetoNaoSerializavel(calidad);
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

    ko.bindingHandlers.addLinhaDivisa = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).change(function () {
                if (bindingContext.$index() == bindingContext.$parent.totalesDivisas().length - 1) {
                    bindingContext.$parent.totalesDivisas.push(new TotalPorDivisa(null, bindingContext.$root.divisasDisponiveis, bindingContext.$root));
                    $('select').unbind('keypress').keypress(TratamentosDropdown);
                }
            });
        }
    };

    ko.bindingHandlers.addLinhaEfectivo = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).change(function () {
                if (bindingContext.$index() == bindingContext.$parent.totalesEfectivo().length - 1) {
                    var divisaAtual = bindingContext.$parent.totalesEfectivo()[bindingContext.$index()].divisa;
                    divisaAtual.isModified(true);
                    if (divisaAtual() == null) {
                        divisaAtual(bindingContext.$parent.totalesEfectivo()[bindingContext.$index() - 1].divisa());
                    }
                    bindingContext.$parent.totalesEfectivo.push(new TotalEfectivo(null, bindingContext.$root));
                    $('select').unbind('keypress').keypress(TratamentosDropdown);
                }
            });
        }
    };

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

    function TotalPorDivisa(total, divisasDisponiveis, modelo) {
        var self = this;
        var raiz = modelo;

        self.totalGeneral = ko.observable("");
        self.totalEfectivo = ko.observable("");
        self.totalOtroValor = ko.observable("");
        self.totalCheque = ko.observable("");
        self.totalTarjeta = ko.observable("");
        self.totalTicket = ko.observable("");
        self.divisainterna = ko.observable("");

        self.divisa = ko.observable("");
        self.divisa.subscribe(function (valordivisa) {
            self.divisainterna(valordivisa);
            if (valordivisa == null) {
                self.totalGeneral(null);
                self.totalEfectivo(null);
                self.totalCheque(null);
                self.totalOtroValor(null);
                self.totalTarjeta(null);
                self.totalTicket(null);
            }
        });

        self.corLinha = ko.computed(function () {
            return (self.divisa() == null ? '' : self.divisa().Color);
        });
        if (total != null) {
            if (total.Divisa != null) {
                self.divisa(divisasDisponiveis.filter(function (divisaDisponivel) {
                    return divisaDisponivel.Identificador == total.Divisa.Identificador;
                })[0]);
                //self.divisa.isModified(true);
            }
            if (total.TotalGeneral != 0) {
                self.totalGeneral(IncluirFormato(total.TotalGeneral));
            }
            if (total.TotalEfectivo != 0) {
                self.totalEfectivo(IncluirFormato(total.TotalEfectivo));
            }
            if (total.TotalOtroValor != 0) {
                self.totalOtroValor(IncluirFormato(total.TotalOtroValor));
            }
            if (total.TotalCheque != 0) {
                self.totalCheque(IncluirFormato(total.TotalCheque));
            }
            if (total.TotalTarjeta != 0) {
                self.totalTarjeta(IncluirFormato(total.TotalTarjeta));
            }
            if (total.TotalTicket != 0) {
                self.totalTicket(IncluirFormato(total.TotalTicket));
            }
        }

        this.toJSON = function () {
            return {
                Divisa: self.divisa(),
                TotalGeneral: self.totalGeneral(),
                TotalEfectivo: self.totalEfectivo(),
                TotalOtroValor: self.totalOtroValor(),
                TotalCheque: self.totalCheque(),
                TotalTarjeta: self.totalTarjeta(),
                TotalTicket: self.totalTicket()
            };
        };

    }

    function TotalesPorDivisasViewModel() {
        var self = this;

        self.dicionario = parametros.Dicionario;

        self.divisasDisponiveis = parametros.DivisasDisponiveis;

        self.totalesDivisas = ko.observableArray($.map(parametros.TotalesPorDivisa, function (total) {
            return new TotalPorDivisa(total, self.divisasDisponiveis, self);
        }));

        self.trabajarConTotalGeneral = parametros.TrabajarConTotalGeneral;

        self.exhibirTotalesDivisa = parametros.ExhibirTotalesDivisa;
        self.exhibirTotalesEfectivo = parametros.ExhibirTotalesEfectivo;
        self.exhibirTotalesMedioPago = parametros.ExhibirTotalesMedioPago;
        self.editavel = (parametros.Modo != "Consulta");

        // Metodos
        self.RemoverTotalPorDivisa = function (item) {
            if (item == self) {
                if (confirm(self.dicionario.confirmacionExclusionTodos)) {
                    while (self.totalesDivisas().length > 1) {
                        self.totalesDivisas.shift();
                    }
                }
            } else {
                self.totalesDivisas.remove(item);
            }
        };

        if (self.editavel) {
            self.totalesDivisas.push(new TotalPorDivisa(null, self.divisasDisponiveis, self));
        }

        self.totalesDivisasSerializar = ko.computed(function () {
            if (self.editavel) {
                return self.totalesDivisas.slice(0, self.totalesDivisas().length - 1);
            } else {
                return self.totalesDivisas();
            }
        });

    }

    ko.applyBindings(new TotalesPorDivisasViewModel(), $("#" + totalesDivisas).get(0));

    function TotalEfectivo(total, modeloRaiz) {

        var self = this;
        var raiz = modeloRaiz;

        self.dicionario = parametros.Dicionario;

        self.divisa = ko.observable().extend({ required: true });
        self.corLinha = ko.computed(function () {
            return (self.divisa() == null ? '' : self.divisa().Color);
        });
        self.denominacion = ko.observable().extend({
            required: {
                onlyIf: function () {
                    return self.divisa() != null;
                }
            }
        });
        self.denominaciones = ko.computed(function () {
            var retorno = [];
            if (self.divisa() != null) {
                retorno = self.divisa().Denominaciones;

            }
            return retorno;
        });
        self.habilitarDenominaciones = ko.computed(function () {
            return self.divisa() != null;
        });

        self.billeteMoneda = ko.computed(function () {
            var retorno = '';
            if (self.denominacion() != null) {
                retorno = (self.denominacion().EsBillete ? "../Imagenes/Money.gif" : "../Imagenes/Coins.gif");
            }
            else {
                retorno = "";
            }
            return retorno;
        });
        self.unidadMedida = ko.observable().extend({
            required: {
                onlyIf: function () {
                    return raiz.trabajarConUnidadMedida && self.denominacion() != null;
                }
            }
        });
        self.unidadesMedida = ko.computed(function () {
            var retorno = [];
            if (self.denominacion() != null) {
                if (self.denominacion().EsBillete) {
                    retorno = raiz.unidadesMedidaBilleteDisponiveis;
                } else {
                    retorno = raiz.unidadesMedidaMonedaDisponiveis;
                }
            }
            return retorno;
        });
        self.habilitarUnidadMedida = ko.computed(function () {
            if (self.denominacion() != null) {
                if (self.unidadesMedida() != null) {
                    // verifica se na lista de unidade de medida existe uma como padrão..
                    var valorPadrao = self.unidadesMedida().filter(function (unidad) { return unidad.EsPadron == true; })[0];
                    if (valorPadrao != null) {
                        self.unidadMedida(valorPadrao);
                    }
                }

                return true;
            }
            else {
                return false;
            }
        });
        self.cantidad = ko.observable();
        self.cantidadCalculada = ko.computed({
            read: function () {
                if (self.denominacion() == null || (raiz.trabajarConUnidadMedida && self.unidadMedida() == null)) {
                    self.cantidad(null);
                }
                return self.cantidad();
            },
            write: function (novoValor) {
                var negativo = false;

                //se só existe o menos, então apaga
                if (novoValor === "-") {
                    novoValor = "";
                }

                if (novoValor != null) {
                    // verifica se o valor digitado possui o sinal de negativo..
                    negativo = novoValor.toString().indexOf("-") > -1;
                }

                self.cantidad(novoValor);
                novoValor = (new String(novoValor)).replace(/[^\d\,]/g, "").replace(/,/g, '.');
                if (!isNaN(novoValor)) {

                    //verifica se o valor é diferente de zero
                    if (Number(novoValor) == 0) {
                        self.valor(null);
                        self.cantidad(null);
                        return;
                    }

                    var formatValor;
                    if (raiz.trabajarConUnidadMedida) {
                        // Inserindo validação para não calcular a quantidade se a unidade de medida ainda não estiver selecionada
                        if (self.unidadMedida() != null) {
                            formatValor = IncluirFormato(+(Number(self.denominacion().Valor) * Number(novoValor) * Number(self.unidadMedida().ValorUnidad)).toFixed(2));

                            // se é valor negativo então inclui o sinal no inicio do valor
                            if (negativo) {
                                formatValor = "-" + formatValor;
                            }

                            self.valor(formatValor);
                        } else {
                            self.valor(null);
                        }
                    } else {
                        formatValor = IncluirFormato(+(Number(self.denominacion().Valor) * Number(novoValor)).toFixed(2));

                        // se é valor negativo então inclui o sinal no inicio do valor
                        if (negativo) {
                            formatValor = "-" + formatValor;
                        }

                        self.valor(formatValor);
                    }
                }
            }
        }).extend({
            onlyIf: function () {
                return self.denominacion() != null && (raiz.trabajarConUnidadMedida && self.unidadMedida() != null);
            }
        });;
        self.valor = ko.observable();
        self.valorCalculado = ko.computed({
            read: function () {
                if (self.denominacion() == null || (raiz.trabajarConUnidadMedida && self.unidadMedida() == null)) {
                    self.valor(null);
                }
                return (self.valor() == null ? null : self.valor().toLocaleString());
            },
            write: function (novoValor) {
                var negativo = false;

                //se só existe o menos, então apaga
                if (novoValor === "-") {
                    novoValor = "";
                }

                if (novoValor != null) {
                    // verifica se o valor digitado possui o sinal de negativo..
                    negativo = novoValor.toString().indexOf("-") > -1;
                }

                var resto = 0;
                self.valor(novoValor);
                novoValor = (new String(novoValor)).replace(/[^\d\,]/g, "").replace(/,/g, '.');
                if (!isNaN(novoValor)) {

                    //verifica se o valor é diferente de zero
                    if (Number(novoValor) == 0) {
                        self.valor(null);
                        self.cantidad(null);
                        return;
                    }

                    if (raiz.trabajarConUnidadMedida) {
                        // Inserindo validação para não calcular o valor se a unidade de medida ainda não estiver selecionada
                        if (self.unidadMedida() != null) {
                            if (+(Number(novoValor) / Number(self.denominacion().Valor) % Number(self.unidadMedida().ValorUnidad)).toFixed(2) != 0) {
                                self.cantidad(null);
                                self.valor(null);
                                alert(self.dicionario.divisionentero.replace('{0}', novoValor).replace('{1}', Number(self.denominacion().Valor)).replace('{2}', Number(self.unidadMedida().ValorUnidad)));
                            }
                            else {
                                // se é valor negativo então inclui o sinal no inicio do valor
                                if (negativo) {
                                    self.cantidad("-" + (+(Number(novoValor) / Number(self.denominacion().Valor) / Number(self.unidadMedida().ValorUnidad)).toFixed(2)));
                                }
                                else {
                                    self.cantidad(+(Number(novoValor) / Number(self.denominacion().Valor) / Number(self.unidadMedida().ValorUnidad)).toFixed(2));
                                }
                            }
                        } else {
                            self.cantidad(null);
                        }
                    } else {
                        //retira as casas decimais porque estava dando erro com esse valor (0.50) % (0.05) não restava zero.
                        if (+(novoValor * 100) % (self.denominacion().Valor * 100) != 0) {
                            self.cantidad(null);
                            self.valor(null);

                            //Alert para validação dos efectivos.
                            alert(self.dicionario.divisioninvalidaEfectivo.replace('{0}', novoValor).replace('{1}', Number(self.denominacion().Valor)));
                        } else {

                            // se é valor negativo então inclui o sinal no inicio do valor
                            if (negativo) {
                                self.cantidad("-" + (+(Number(novoValor) / Number(self.denominacion().Valor)).toFixed(2)));
                            }
                            else {
                                self.cantidad(+(Number(novoValor) / Number(self.denominacion().Valor)).toFixed(2));
                            }
                        }
                    }
                }
            }
        }).extend({
            Required: {
                onlyIf: function () {
                    return self.denominacion() != null && (raiz.trabajarConUnidadMedida && self.unidadMedida() != null);
                }
            }
        });;

        self.calidad = ko.observable();
        self.calidades = ko.observableArray();

        self.denominacion.subscribe(function (novoValor) {
            if (novoValor != null) {
                self.cantidadCalculada(self.cantidad());

                var calidadesPorTipoDenominacion = raiz.calidadesDisponiveis.filter(function (c) {
                    return ((c.TipoCalidad == "Ambos") || (c.TipoCalidad == (novoValor.EsBillete ? "Billete" : "Moneda")));
                });
                self.calidades(calidadesPorTipoDenominacion)
            }
        });
        self.unidadMedida.subscribe(function (novoValor) {
            if (novoValor != null) {
                if (self.cantidad() == null) {
                    self.cantidad(null);
                    self.valor(null);
                } else {
                    self.cantidadCalculada(self.cantidad());
                }
            } else {
                self.valor(null);
                self.cantidad(null);
            }
        });
        self.habilitarValoresCalculados = ko.computed(function () {
            var retorno = false;
            var denominacionPreenchida = self.denominacion() != null;
            var unidadMedidaPreenchida = self.unidadMedida() != null;
            if (denominacionPreenchida) {
                if (raiz.trabajarConUnidadMedida) {
                    if (unidadMedidaPreenchida) {
                        retorno = true;
                    }
                } else {
                    retorno = true;
                }
            }
            return retorno;
        });

        if (total != null) {
            if (total.Divisa != null) {
                self.divisa(raiz.divisasDisponiveis.filter(function (divisaDisponivel) {
                    return divisaDisponivel.Identificador == total.Divisa.Identificador;
                })[0]);
                self.divisa.isModified(true);
            }
            if (total.Denominacion != null && self.divisa() != undefined) {
                self.denominacion(self.divisa().Denominaciones.filter(function (denominacionDisponivel) {
                    return denominacionDisponivel.Identificador == total.Denominacion.Identificador;
                })[0]);
                self.denominacion.isModified(true);
            }
            if (total.UnidadMedida != null) {
                self.unidadMedida(raiz.unidadesMedidaDisponiveis.filter(function (unidadeMedidaDisponivel) {
                    return unidadeMedidaDisponivel.Identificador == total.UnidadMedida.Identificador;
                })[0]);
                self.unidadMedida.isModified(true);
            }
            if (total.Cantidad != 0) {
                self.cantidad(total.Cantidad);
            }
            if (total.Valor != 0) {
                self.valor(IncluirFormato(total.Valor));
            }
            
            if (total.Calidad != null) {
                self.calidad(raiz.calidadesDisponiveis.filter(function (calidadDisponivel) {
                    return calidadDisponivel.Identificador == total.Calidad.Identificador;
                })[0]);
            } else {
                if (parametros.Modo == "Consulta") {
                    self.calidad(raiz.calidadesDisponiveis.filter(function (calidadDisponivel) {
                        return calidadDisponivel.Identificador == self.dicionario.calidadNoDefinida;
                    })[0]);
                }
            }
        }

        this.toJSON = function () {
            return {
                Divisa: self.divisa(),
                Denominacion: self.denominacion(),
                UnidadMedida: self.unidadMedida(),
                Cantidad: self.cantidad(),
                Valor: self.valor(),
                Calidad: self.calidad()
            };
        };
    }

    function TotalesEfectivoViewModel() {
        var self = this;

        self.dicionario = parametros.Dicionario;

        self.divisasDisponiveis = parametros.DivisasDisponiveis.filter(function (divisaDisponivel) {
            return divisaDisponivel.Denominaciones != null && divisaDisponivel.Denominaciones.length > 0;
        });

        self.unidadesMedidaDisponiveis = parametros.UnidadesMedidaDisponiveis;
        self.unidadesMedidaBilleteDisponiveis = self.unidadesMedidaDisponiveis.filter(function (unidadeMedida) { return unidadeMedida.TipoUnidadMedida == 'Billete'; });
        self.unidadesMedidaMonedaDisponiveis = self.unidadesMedidaDisponiveis.filter(function (unidadeMedida) { return unidadeMedida.TipoUnidadMedida == 'Moneda'; });
        self.calidadesDisponiveis = parametros.CalidadesDisponiveis;

        self.totalesEfectivo = ko.observableArray($.map(parametros.TotalesEfectivo, function (total) {
            return new TotalEfectivo(total, self);
        }));

        self.trabajarConUnidadMedida = parametros.TrabajarConUnidadMedida;
        self.trabajarConCalidad = parametros.TrabajarConCalidad;

        self.exhibirTotalesDivisa = parametros.ExhibirTotalesDivisa;
        self.exhibirTotalesEfectivo = parametros.ExhibirTotalesEfectivo;
        self.exhibirTotalesMedioPago = parametros.ExhibirTotalesMedioPago;

        self.editavel = (parametros.Modo != "Consulta");

        // Metodos
        self.RemoverTotalEfectivo = function (item) {
            if (item == self) {
                if (confirm(self.dicionario.confirmacionExclusionTodos)) {
                    while (self.totalesEfectivo().length > 1) {
                        self.totalesEfectivo.shift();
                    }
                }
            } else {
                self.totalesEfectivo.remove(item);
            }
        };

        if (self.editavel) {
            self.totalesEfectivo.push(new TotalEfectivo(null, self));
        }

        self.totalesEfectivoSerializar = ko.computed(function () {
            if (self.editavel) {
                return self.totalesEfectivo.slice(0, self.totalesEfectivo().length - 1);
            } else {
                return self.totalesEfectivo();
            }
        });

        self.totalesEfectivoGrilla = ko.computed(function () {
            var totales = [];

            $.each(self.totalesEfectivo(), function (index, totalEfectivo) {
                if (totalEfectivo.divisa() === undefined)
                    return;

                var total;

                for (var i = 0; i < totales.length; i++) {
                    if (totales[i].Identificador === totalEfectivo.divisa().Identificador) {
                        total = totales[i];
                    }
                }

                if (total === undefined) {
                    var valorCalculado = 0;

                    if (totalEfectivo.valorCalculado() != null) {
                        valorCalculado = parseFloat(RemoverFormato(totalEfectivo.valorCalculado()));

                        if (isNaN(valorCalculado)) {
                            valorCalculado = 0;
                        }
                    }

                    totales.push({
                        Identificador: totalEfectivo.divisa().Identificador,
                        Descripcion: totalEfectivo.divisa().Descripcion,
                        TotalMoneda: totalEfectivo.divisa().CodigoSimbolo != null ? totalEfectivo.divisa().CodigoSimbolo + ' ' + IncluirFormato(valorCalculado) : IncluirFormato(valorCalculado),
                        Color: totalEfectivo.divisa().Color,
                        CodigoSimbolo: totalEfectivo.divisa().CodigoSimbolo,
                        Total: valorCalculado
                    });
                }
                else {
                    var valorCalculado = 0;
                    if (totalEfectivo.valorCalculado() != null) {
                        valorCalculado = parseFloat(RemoverFormato(totalEfectivo.valorCalculado()));

                        if (isNaN(valorCalculado)) {
                            valorCalculado = 0;
                        }
                    }

                    total.Total = parseFloat(parseFloat(total.Total) + valorCalculado).toFixed(2);
                    total.TotalMoneda = total.CodigoSimbolo != null ? total.CodigoSimbolo + ' ' + IncluirFormato(total.Total) : IncluirFormato(total.Total);
                }
            });

            return totales;
        });

    }

    ko.applyBindings(new TotalesEfectivoViewModel(), $("#" + totalesEfectivo).get(0));

    function TotalMedioPago(total, modeloRaiz) {
        var self = this;
        var raiz = modeloRaiz;

        self.divisa = ko.observable().extend({ required: true });

        self.corLinha = ko.computed(function () {
            return (self.divisa() == null ? '' : self.divisa().Color);
        });
        self.tiposMedioPago = ko.computed(function () {
            var retorno = [];
            if (self.divisa() != null) {
                $.each(self.divisa().MediosPago, function (indice, medioPago) {
                    if (retorno.indexOf(medioPago.Tipo) == -1) {
                        retorno.push(medioPago.Tipo);
                    }
                });
            }
            return retorno;
        });
        self.tipoMedioPago = ko.observable().extend({ required: true });
        self.habilitarTipoMedioPago = ko.computed(function () {
            return self.divisa() != null;
        })
        self.mediosPago = ko.computed(function () {
            var retorno = [];
            if (self.tipoMedioPago() != null) {
                retorno = self.divisa().MediosPago.filter(function (medioPago) {
                    return medioPago.Tipo == self.tipoMedioPago();
                });
            }
            return retorno;
        });
        self.medioPago = ko.observable().extend({
            required: {
                onlyIf: function () {
                    return self.tipoMedioPago() != null;
                }
            }
        });
        self.habilitarMedioPago = ko.computed(function () {
            return self.tipoMedioPago() != null;
        });
        self.habilitarUnidadMedida = ko.computed(function () {
            return self.medioPago() != null;
        });
        self.unidadMedida = ko.observable().extend({
            required: {
                onlyIf: function () {
                    return raiz.trabajarConUnidadMedida;
                }
            }
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
        self.valor = ko.observable();

        self.terminos = ko.observableArray([]);

        if (total != null) {
            if (total.Divisa != null) {
                self.divisa(raiz.divisasDisponiveis.filter(function (divisaDisponivel) {
                    return divisaDisponivel.Identificador == total.Divisa.Identificador;
                })[0]);
                self.divisa.isModified(true);
            }
            if (total.MedioPago != null) {
                self.tipoMedioPago(self.tiposMedioPago().filter(function (tipoMedioPagoDisponivel) {
                    return tipoMedioPagoDisponivel == total.MedioPago.Tipo;
                })[0]);
                self.tipoMedioPago.isModified(true);

                self.medioPago(self.mediosPago().filter(function (medioPagoDisponivel) {
                    return medioPagoDisponivel.Identificador == total.MedioPago.Identificador;
                })[0]);
                self.medioPago.isModified(true);
            }
            if (total.UnidadMedida != null) {
                self.unidadMedida(raiz.unidadesMedidaMedioPagoDisponiveis.filter(function (unidadMedidaDisponivel) {
                    return unidadMedidaDisponivel.Identificador == total.UnidadMedida.Identificador;
                })[0]);
                self.unidadMedida.isModified(true);
            }
            if (total.Cantidad != 0) {
                self.cantidad(total.Cantidad);
            }
            if (total.Valor != 0) {
                self.valor(IncluirFormato(total.Valor));
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
        self.unidadMedida.subscribe(resetaListaTerminos);

        resetaListaTerminos(false);

        this.toJSON = function () {
            return {
                Divisa: self.divisa(),
                MedioPago: new ObjetoNaoSerializavel(self.medioPago()),
                UnidadMedida: self.unidadMedida(),
                Cantidad: self.cantidad(),
                Valor: self.valor(),
                Terminos: self.terminos()
            };
        };

    }

    function TotalesMedioPagoViewModel() {
        var self = this;

        self.dicionario = parametros.Dicionario;
        self.divisasDisponiveis = parametros.DivisasDisponiveis.filter(function (divisaDisponivel) {
            return divisaDisponivel.MediosPago != null && divisaDisponivel.MediosPago.length > 0;
        });
        self.tiposMedioPagoDisponiveis = parametros.TiposMedioPagoDisponiveis;

        self.unidadesMedidaDisponiveis = parametros.UnidadesMedidaDisponiveis;
        self.unidadesMedidaMedioPagoDisponiveis = self.unidadesMedidaDisponiveis.filter(function (unidadeMedida) { return unidadeMedida.TipoUnidadMedida == 'MedioPago'; });

        self.trabajarConUnidadMedida = parametros.TrabajarConUnidadMedida;

        self.exhibirTotalesDivisa = parametros.ExhibirTotalesDivisa;
        self.exhibirTotalesEfectivo = parametros.ExhibirTotalesEfectivo;
        self.exhibirTotalesMedioPago = parametros.ExhibirTotalesMedioPago;

        self.totalMedioPagoEdicao = ko.observable();

        self.editavel = (parametros.Modo != "Consulta");

        self.totalesMedioPago = ko.observableArray($.map(parametros.TotalesMedioPago, function (total) {
            return new TotalMedioPago(total, self);
        }));

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

        self.totalesMedioPagoGrilla = ko.computed(function () {
            var totales = [];
            var tDivisa;

            $.each(self.totalesMedioPago(), function (index, totalMedioPago) {
                if (totalMedioPago.divisa() === undefined)
                    return;

                if (totalMedioPago.tipoMedioPago() === undefined)
                    return;

                var total;
                tDivisa = undefined;

                for (var i = 0; i < totales.length; i++) {
                    if (totales[i].Identificador === totalMedioPago.divisa().Identificador) {
                        tDivisa = totales[i];
                        for (var j = 0; j < tDivisa.TotalesMedioPago.length; j++) {

                            if (tDivisa.TotalesMedioPago[j].Identificador === totalMedioPago.tipoMedioPago()) {
                                total = tDivisa.TotalesMedioPago[j];
                            }
                        }
                    }
                }


                if (tDivisa === undefined) {

                    tDivisa = {
                        Identificador: totalMedioPago.divisa().Identificador,
                        Descripcion: totalMedioPago.divisa().Descripcion,
                        TotalMoneda: totalMedioPago.divisa().CodigoSimbolo != null ? totalMedioPago.divisa().CodigoSimbolo + ' ' + IncluirFormato(valorCalculado) : IncluirFormato(valorCalculado),
                        Color: totalMedioPago.divisa().Color,
                        CodigoSimbolo: totalMedioPago.divisa().CodigoSimbolo,
                        TotalesMedioPago: []
                    }
                    totales.push(tDivisa);

                }

                if (total === undefined) {

                    var valorCalculado = 0;

                    if (totalMedioPago.valor() != null) {
                        valorCalculado = parseFloat(RemoverFormato(totalMedioPago.valor()));

                        if (isNaN(valorCalculado)) {
                            valorCalculado = 0;
                        }
                    }

                    tDivisa.TotalesMedioPago.push({
                        Identificador: totalMedioPago.tipoMedioPago(),
                        TotalMoneda: totalMedioPago.divisa().CodigoSimbolo != null ? totalMedioPago.divisa().CodigoSimbolo + ' ' + IncluirFormato(valorCalculado) : IncluirFormato(valorCalculado),
                        Total: valorCalculado
                    });
                }
                else {
                    var valorCalculado = 0;
                    if (totalMedioPago.valor() != null) {
                        valorCalculado = parseFloat(RemoverFormato(totalMedioPago.valor()));

                        if (isNaN(valorCalculado)) {
                            valorCalculado = 0;
                        }
                    }

                    total.Total = parseFloat(parseFloat(total.Total) + valorCalculado).toFixed(2);
                    total.TotalMoneda = tDivisa.CodigoSimbolo != null ? tDivisa.CodigoSimbolo + ' ' + IncluirFormato(total.Total) : IncluirFormato(total.Total);
                }
            });

            return totales;
        });
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

    ko.applyBindings(new TotalesMedioPagoViewModel(), $("#" + totalesMedioPago).get(0));
}

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

            }, null, { disposeWhenNodeIsRemoved: element });

            return { controlsDescendantBindings: true };
        }
    };
}());