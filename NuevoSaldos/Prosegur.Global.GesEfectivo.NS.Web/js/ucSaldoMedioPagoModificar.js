//#region ViewModel para o Grid de MedioPago a Modificar
// parametros   =   Lista de parametros carregada no servidor (ucSaldoMedioPagoModificar.ascx.vb)
// totalesEfectivo  =   Identificador do panel do controle ucSaldoMedioPagoModificar
function InicializaUcSaldoMedioPagoModificar(parametros, totalesMedioPago, ucClientID) {

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

    //#region ViewModel - MedioPago a modificar

    function TotalesMedioPagoViewModel() {
        var self = this;

        self.dicionario = parametros.Dicionario;
        self.clienteID = ucClientID;
        self.divisasDisponiveis = parametros.DivisasDisponiveis.filter(function (divisaDisponivel) {
            return divisaDisponivel.MediosPago != null && divisaDisponivel.MediosPago.length > 0;
        });
        self.tiposMedioPagoDisponiveis = parametros.TiposMedioPagoDisponiveis;
        self.tipoMedioPagoDisponivel = parametros.TipoMedioPagoDisponivel;
        self.exhibirTotalesMedioPago = parametros.ExhibirTotalesMedioPago;
        self.TotalMedioPagoCambiado = ko.observable();
        self.totalMedioPagoEdicao = ko.observable();

        self.editavel = (parametros.Modo != "Consulta");
        self.habilitaCheckBox = parametros.HabilitaCheckBox;

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

        self.editarMedioPago = function (item) {
            self.totalMedioPagoEdicao(item);
        };

        self.ManejoSaldo = function (item) {
            self.TotalMedioPagoCambiado(item);
            return true;
        };

        if (self.editavel) {
            self.totalesMedioPago.push(new TotalMedioPago(null, self));
        }

        self.totalesMedioPagoSerializar = ko.computed(function () {
            if (self.editavel) {
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
                        TotalMoneda: totalMedioPago.divisa().CodigoSimbolo != null ? totalMedioPago.divisa().CodigoSimbolo + ' ' + IncluirFormato(valorCalculado, parametros) : IncluirFormato(valorCalculado, parametros),
                        Color: totalMedioPago.divisa().Color,
                        CodigoSimbolo: totalMedioPago.divisa().CodigoSimbolo,
                        Descripcion: total > 0 ? self.dicionario.importenegativomodificar + " " : self.dicionario.importepositivomodificar + " ",
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

                    tDivisa.TotalesMedioPago.push({
                        Identificador: totalMedioPago.tipoMedioPago(),
                        TotalMoneda: totalMedioPago.divisa().CodigoSimbolo != null ? totalMedioPago.divisa().CodigoSimbolo + ' ' + IncluirFormato(valorCalculado, parametros) : IncluirFormato(valorCalculado, parametros),
                        Total: valorCalculado
                    });
                }
                else {
                    var valorCalculado = 0;
                    if (totalMedioPago.valor() != null) {
                        valorCalculado = parseFloat(RemoverFormato(totalMedioPago.valor(), parametros));

                        if (isNaN(valorCalculado)) {
                            valorCalculado = 0;
                        }
                    }

                    total.Total = parseFloat(parseFloat(total.Total) + valorCalculado).toFixed(2);
                    total.TotalMoneda = tDivisa.CodigoSimbolo != null ? tDivisa.CodigoSimbolo + ' ' + IncluirFormato(total.Total, parametros) : IncluirFormato(total.Total, parametros);
                }

            });

            var totalesGeneral = AgruparValoresPositivosYNegativos(totales, self.dicionario);

            return totalesGeneral;

        });
    }

    //#endregion

    //#region Item da ViewModel - MedioPago a modificar

    function TotalMedioPago(total, modeloRaiz) {
        var self = this;
        var raiz = modeloRaiz;

        self.divisa = ko.observable().extend({ required: true });
        self.detallar = ko.observable(false);

        if (total.Detallar == true) {
            self.detallar(true);
        };

        self.detallar.subscribe(function (novovalor) {
            atualizaTotalesMedioPagoModificar(raiz);
        });

        self.corLinha = ko.computed(function () {
            return (self.divisa() == null ? '' : self.divisa().Color);
        });

        self.corFundo = ko.computed(function () {
            return total.ColorFondo;
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
        self.tipoMedioPagoDisponivel = raiz.tipoMedioPagoDisponivel;
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
            if (total.Cantidad != 0) {
                self.cantidad(total.Cantidad);
            }
            if (total.Valor != 0) {
                self.valor(IncluirFormato(total.Valor, parametros));
            }
        }
        self.habilitarValoresCalculados = ko.computed(function () {
            var retorno = true;
            if (self.medioPago() == null) {
                retorno = false;
            }
            return retorno;
        });

        this.toJSON = function () {
            return {
                Divisa: self.divisa(),
                Descripcion: self.divisa().Descripcion,
                MedioPago: new ObjetoNaoSerializavel(self.medioPago()),
                TipoMedioPago: raiz.tipoMedioPagoDisponivel,
                Cantidad: self.cantidad(),
                Valor: self.valor()
            };
        };

    }

    //#endregion

    // Agrupa os valores das divisas, porque nos controles de meio de pagamento os valores totais das divisas ficam na mesma linha
    function AgruparValoresPositivosYNegativos(totales, dicionario) {

        var totalesGeneral = [];
        var tTotalesGeneral;

        for (var i = 0; i < totales.length; i++) {

            for (var j = 0; j < totales[i].TotalesMedioPago.length; j++) {

                if (totales[i].TotalesMedioPago[j].Total > 0) {

                    if (totalesGeneral.length < 2) {
                        if (totalesGeneral.length == 0) {
                            tTotalesGeneral = {
                                Codigo: 1,
                                Descripcion: dicionario.importepositivomodificar + " ",
                                Valores: []
                            }
                            totalesGeneral.push(tTotalesGeneral);
                        }
                        else {
                            if (totalesGeneral[0].Codigo == -1) {
                                tTotalesGeneral = {
                                    Codigo: 1,
                                    Descripcion: dicionario.importepositivomodificar + " ",
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
                                Descripcion: dicionario.importenegativomodificar + " ",
                                Valores: []
                            }
                            totalesGeneral.push(tTotalesGeneral);
                        }
                        else {
                            if (totalesGeneral[0].Codigo == 1) {
                                tTotalesGeneral = {
                                    Codigo: -1,
                                    Descripcion: dicionario.importenegativomodificar + " ",
                                    Valores: []
                                }
                                totalesGeneral.push(tTotalesGeneral);
                            }
                        }
                        { continue; }
                    } else { break; }
                }
            }
        }

        for (var i = 0; i < totales.length; i++) {

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
                else {
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
            }
        }

        return totalesGeneral;

    }

    var vm = new TotalesMedioPagoViewModel();
    // Instância da função ControlarSaldo para realizar a comunicação entre as ViewModel's (saldoModificar  ->  saldoModificado)
    var saldo = GetControlarSaldo();
    saldo.RegistrarVMSaldoMedioPagoModificar(vm);

    //Bind ViewModel
    ko.applyBindings(vm, $("#" + totalesMedioPago).get(0));

}

function atualizaTotalesMedioPagoModificar(raiz) {
    var listaSel = $.grep(raiz.totalesMedioPagoSerializar(), function (val, index) {
        return val.detallar();
    });
    var jsonValor = ko.toJSON(listaSel);
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