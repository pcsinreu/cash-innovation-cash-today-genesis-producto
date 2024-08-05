//#region ViewModel para o Grid de Efetivos a Modificar
// parametros   =   Lista de parametros carregada no servidor (ucSaldoEfectivoModificar.ascx.vb)
// totalesEfectivo  =   Identificador do panel do controle ucSaldoEfectivoModificar
function InicializaUcSaldoEfectivoModificar(parametros, totalesEfectivo, ucClientID) {

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

    //#region bindingHandlers

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

    //#region ViewModel - Efetivo a Modificar

    function TotalesEfectivoViewModel() {
        var self = this;

        self.dicionario = parametros.Dicionario;
        self.clienteID = ucClientID;
        self.divisasDisponiveis = parametros.DivisasDisponiveis.filter(function (divisaDisponivel) {
            return divisaDisponivel.Denominaciones != null && divisaDisponivel.Denominaciones.length > 0;
        });

        self.unidadesMedidaDisponiveis = parametros.UnidadesMedidaDisponiveis;
        self.unidadesMedidaBilleteDisponiveis = self.unidadesMedidaDisponiveis.filter(function (unidadeMedida) { return unidadeMedida.TipoUnidadMedida == 'Billete'; });
        self.unidadesMedidaMonedaDisponiveis = self.unidadesMedidaDisponiveis.filter(function (unidadeMedida) { return unidadeMedida.TipoUnidadMedida == 'Moneda'; });
        self.calidadesDisponiveis = parametros.CalidadesDisponiveis;

        self.TotalEfectivoCambiado = ko.observable();
        self.totalesEfectivo = ko.observableArray($.map(parametros.TotalesEfectivo, function (total) {
            var obj = new TotalEfectivo(total, self)

            return obj;
        }));

        self.trabajarConUnidadMedida = parametros.TrabajarConUnidadMedida;
        self.trabajarConCalidad = parametros.TrabajarConCalidad;
        self.trabajarConNivelDetalle = parametros.TrabajarConNivelDetalle;

        self.exhibirTotalesEfectivo = parametros.ExhibirTotalesEfectivo;

        self.editavel = (parametros.Modo != "Consulta");
        self.habilitaCheckBox = parametros.HabilitaCheckBox;

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

        self.ManejoSaldo = function (item) {
            self.TotalEfectivoCambiado(item);
            return true;
        };

        if (self.editavel) {
            self.totalesEfectivo.push(new TotalEfectivo(null, self));
        };

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
                        valorCalculado = parseFloat(RemoverFormato(totalEfectivo.valorCalculado(), parametros));

                        if (isNaN(valorCalculado)) {
                            valorCalculado = 0;
                        }
                    }

                    if (valorCalculado > 0) {

                        totales.push({
                            Identificador: totalEfectivo.divisa().Identificador,
                            TotalMoneda: totalEfectivo.divisa().CodigoSimbolo != null ? totalEfectivo.divisa().CodigoSimbolo + ' ' + IncluirFormato(valorCalculado, parametros) : IncluirFormato(valorCalculado, parametros),
                            Color: totalEfectivo.divisa().Color,
                            CodigoSimbolo: totalEfectivo.divisa().CodigoSimbolo,
                            Total: valorCalculado,
                            Descripcion: self.dicionario.importepositivomodificar + " ",
                            Codigo: 1,
                            NivelDetalle: totalEfectivo.NivelDetalle
                        });
                    } else {
                        totales.push({
                            Identificador: totalEfectivo.divisa().Identificador,
                            TotalMoneda: totalEfectivo.divisa().CodigoSimbolo != null ? totalEfectivo.divisa().CodigoSimbolo + ' ' + IncluirFormato(valorCalculado, parametros) : IncluirFormato(valorCalculado, parametros),
                            Color: totalEfectivo.divisa().Color,
                            CodigoSimbolo: totalEfectivo.divisa().CodigoSimbolo,
                            Total: valorCalculado,
                            Descripcion: self.dicionario.importenegativomodificar + " ",
                            Codigo: -1,
                            NivelDetalle: totalEfectivo.NivelDetalle
                        });
                    }
                }
                else {
                    var valorCalculado = 0;
                    if (totalEfectivo.valorCalculado() != null) {
                        valorCalculado = parseFloat(RemoverFormato(totalEfectivo.valorCalculado(), parametros));

                        if (isNaN(valorCalculado)) {
                            valorCalculado = 0;
                        }
                    }

                    if (valorCalculado > 0) {
                        if (totales.length == 1) {
                            // se valor existente for positivo
                            if (totales[0].Codigo == 1) {
                                // pode somar com valor existente
                                total.Total = parseFloat(parseFloat(total.Total) + valorCalculado).toFixed(2);
                            }
                            else
                                // deve-se criar outra linha para o valor negativo
                            {
                                totales.push({
                                    Identificador: totalEfectivo.divisa().Identificador,
                                    TotalMoneda: totalEfectivo.divisa().CodigoSimbolo != null ? totalEfectivo.divisa().CodigoSimbolo + ' ' + IncluirFormato(valorCalculado, parametros) : IncluirFormato(valorCalculado, parametros),
                                    Color: totalEfectivo.divisa().Color,
                                    CodigoSimbolo: totalEfectivo.divisa().CodigoSimbolo,
                                    Total: valorCalculado,
                                    Descripcion: self.dicionario.importepositivomodificar + " ",
                                    Codigo: 1,
                                    NivelDetalle: totalEfectivo.NivelDetalle
                                });
                            }
                        }
                        else if (totales.length == 2) {
                            // se valor existente for positivo
                            if (totales[0].Codigo == 1) {
                                total = totales[0];
                                total.Total = parseFloat(parseFloat(total.Total) + valorCalculado).toFixed(2);
                            }
                            else {
                                total = totales[1];
                                total.Total = parseFloat(parseFloat(total.Total) + valorCalculado).toFixed(2);
                            }
                        }

                    }
                    else {
                        if (totales.length == 1) {
                            // se valor existente for positivo
                            if (totales[0].Codigo == -1) {
                                // pode somar com valor existente
                                total.Total = parseFloat(parseFloat(total.Total) + valorCalculado).toFixed(2);
                            }
                            else
                                // deve-se criar outra linha para o valor negativo
                            {
                                totales.push({
                                    Identificador: totalEfectivo.divisa().Identificador,
                                    TotalMoneda: totalEfectivo.divisa().CodigoSimbolo != null ? totalEfectivo.divisa().CodigoSimbolo + ' ' + IncluirFormato(valorCalculado, parametros) : IncluirFormato(valorCalculado, parametros),
                                    Color: totalEfectivo.divisa().Color,
                                    CodigoSimbolo: totalEfectivo.divisa().CodigoSimbolo,
                                    Total: valorCalculado,
                                    Descripcion: self.dicionario.importenegativomodificar + " ",
                                    Codigo: -1,
                                    NivelDetalle: totalEfectivo.NivelDetalle
                                });
                            }
                        }
                        else if (totales.length == 2) {
                            // se valor existente for positivo
                            if (totales[0].Codigo == -1) {
                                total = totales[0];
                                total.Total = parseFloat(parseFloat(total.Total) + valorCalculado).toFixed(2);
                            }
                            else {
                                total = totales[1];
                                total.Total = parseFloat(parseFloat(total.Total) + valorCalculado).toFixed(2);
                            }
                        }
                    }

                    total.TotalMoneda = total.CodigoSimbolo != null ? total.CodigoSimbolo + ' ' + IncluirFormato(total.Total, parametros) : IncluirFormato(total.Total, parametros);
                }
            });

            return totales;
        });

    }

    //#endregion

    //#region Item da ViewModel - Efetivo a Modificar

    function TotalEfectivo(total, modeloRaiz) {

        var self = this;
        var raiz = modeloRaiz;

        self.dicionario = parametros.Dicionario;
        self.detallar = ko.observable(false);

        if (total.Detallar == true) {
            self.detallar(true);
        };

        self.detallar.subscribe(function (novovalor) {
            atualizaTotalesEfectivoModificar(raiz);
        });

        self.divisa = ko.observable().extend({ required: true });
        self.corLinha = ko.computed(function () {
            return (self.divisa() == null ? '' : self.divisa().Color);
        });

        self.corFundo = ko.computed(function () {
            return total.ColorFondo;
        });

        self.tipoDetalle = ko.computed(function () {
            return self.NivelDetalle;
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
                retorno = raiz.unidadesMedidaBilleteDisponiveis;
                    retorno.push.apply(retorno, raiz.unidadesMedidaMonedaDisponiveis);
            }
            return retorno;
        });

        self.habilitarUnidadMedida = ko.computed(function () {
            if (self.denominacion() != null) {
                if (self.unidadesMedida() != null) {
                    // verifica se na lista de unidade de medida existe uma como padrão..
                    var valorPadrao = self.unidadesMedida().filter(function (unidad) { return unidad.EsPadron == true; })[0];
                    if (valorPadrao) {
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
                    negativo = novoValor.indexOf("-") > -1;
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
                            formatValor = IncluirFormato(+(Number(self.denominacion().Valor) * Number(novoValor) * Number(self.unidadMedida().ValorUnidad)).toFixed(2), parametros);

                            // se é valor negativo então inclui o sinal no inicio do valor
                            if (negativo) {
                                formatValor = "-" + formatValor;
                            }

                            self.valor(formatValor);
                        } else {
                            self.valor(null);
                        }
                    } else {
                        formatValor = IncluirFormato(+(Number(self.denominacion().Valor) * Number(novoValor)).toFixed(2), parametros);

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
                if ((self.denominacion() == null || (raiz.trabajarConUnidadMedida && self.unidadMedida() == null)) && (total != null && total.NivelDetalle != self.dicionario.total)) {
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
                    negativo = novoValor.indexOf("-") > -1;
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

        self.NivelDetalle = ko.observable();
        self.tipoDetalle = self.NivelDetalle;

        self.denominacion.subscribe(function (novoValor) {
            if (novoValor != null) {
                self.cantidadCalculada(self.cantidad());
            }
        });

        /*self.unidadMedida.subscribe(function (novoValor) {

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
        });*/

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

        self.calidad = ko.observable();

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
                self.valor(IncluirFormato(total.Valor, parametros));
            }
            if (total.NivelDetalle != null) {
                self.NivelDetalle(total.NivelDetalle)
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
                Descripcion: self.divisa().Descripcion,
                Denominacion: self.denominacion(),
                UnidadMedida: self.unidadMedida(),
                Cantidad: self.cantidad(),
                Valor: self.valor(),
                Calidad: self.calidad(),
                NivelDetalle: self.NivelDetalle
            };
        };

    }

    //#endregion

    var vm = new TotalesEfectivoViewModel();
    // Instância da função ControlarSaldo para realizar a comunicação entre as ViewModel's (saldoModificar  ->  saldoModificado)
    var saldo = GetControlarSaldo();
    saldo.RegistrarVMSaldoModificar(vm);

    //Bind ViewModel
    ko.applyBindings(vm, $("#" + totalesEfectivo).get(0));

}

function atualizaTotalesEfectivoModificar(raiz) {
    var listaSel = $.grep(raiz.totalesEfectivoSerializar(), function (val, index) {
        return val.detallar();
    });
    var jsonValor = ko.toJSON(listaSel);
    var hdfVal = $("#" + raiz.clienteID + "_hdftotalesEfectivo").val().split(";");

    $("#" + raiz.clienteID + "_hdftotalesEfectivo").val(hdfVal[0] + ";" + jsonValor);
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

            }, null, { disposeWhenNodeIsRemoved: element });

            return { controlsDescendantBindings: true };
        }
    };
}());