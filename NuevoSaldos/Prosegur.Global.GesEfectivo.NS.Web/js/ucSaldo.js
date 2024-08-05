/// <reference path="ucSaldoEfectivoModificar.js" />
/// <reference path="ucSaldoEfectivoDetallar.js" />

/// <reference path="ucSaldoMedioPagoModificar.js" />
/// <reference path="ucSaldoMedioPagoDetallar.js" />

//#region Função que controla a comunicação entre as viewmodel's dos controles 'Saldo a Modificar' e 'Saldo Modificado'

function ControlarSaldo() {

    var TotalesEfectivoViewModelModificar = [];
    var TotalesEfectivoViewModelDetallar = [];

    //#region VIEW MODEL - EFECTIVO

    this.RegistrarVMSaldoModificar = function (vm) {

        TotalesEfectivoViewModelModificar.push(vm);

        vm.TotalEfectivoCambiado.subscribe(function (novoValor) {
            var vm2 = TotalesEfectivoViewModelDetallar.filter(function (vm2F) {
                return vm2F.divisasDisponiveis[0].Identificador == novoValor.divisa().Identificador;
            })[0];

            // se checkbox estiver marcado
            if (novoValor.detallar() == true) {
                // adiciona item marcado na coleção de valores a modificar e notifica ao campo calculado mudança de estado para refletir o total na tela.
                vm2.totalesEfectivosModificar.push(novoValor);
                vm2.dummy.notifySubscribers();
            }
            else {
                vm2.totalesEfectivosModificar = $.grep(vm2.totalesEfectivosModificar, function (val, index) {
                    return val != novoValor;
                });

                // remove todos os items da grid de saldo modificado
                //while (vm2.totalesEfectivo().length > 1) {
                //    vm2.totalesEfectivo.shift();
                //}

                vm2.dummy.notifySubscribers();
            }
        });
    };

    this.RegistrarVMSaldoDetallar = function (vm) {
        TotalesEfectivoViewModelDetallar.push(vm);

        for (var i = 0; i < TotalesEfectivoViewModelDetallar.length; i++) {
            atualizaTotalesEfectivo(TotalesEfectivoViewModelDetallar[i]);
        };

        if (TotalesEfectivoViewModelModificar != null) {

            for (var i = 0; i < TotalesEfectivoViewModelModificar.length; i++) {

                if (TotalesEfectivoViewModelModificar[i].divisasDisponiveis[0].Identificador == vm.divisasDisponiveis[0].Identificador) {

                    if (TotalesEfectivoViewModelModificar[i].totalesEfectivo != null) {

                        for (var j = 0; j < TotalesEfectivoViewModelModificar[i].totalesEfectivo().length; j++) {

                            if (TotalesEfectivoViewModelModificar[i].totalesEfectivo()[j].detallar() == true) {

                                TotalesEfectivoViewModelModificar[i].ManejoSaldo(TotalesEfectivoViewModelModificar[i].totalesEfectivo()[j]);
                                atualizaTotalesEfectivoModificar(TotalesEfectivoViewModelModificar[i]);
                                
                            };

                        };

                    };
                };
            };
        };
    };
    //#endregion

    //#region VIEW MODEL - MEDIO PAGO
    var TotalesMedioPagoViewModelModificar = [];
    var TotalesMedioPagoViewModelDetallar = [];

    this.RegistrarVMSaldoMedioPagoModificar = function (vmMP) {

        TotalesMedioPagoViewModelModificar.push(vmMP);

        vmMP.TotalMedioPagoCambiado.subscribe(function (novoValor) {
            // recupera ViewModel a ser modificada
            var vmMP2 = TotalesMedioPagoViewModelDetallar.filter(function (vmMP2F) {
                return vmMP2F.tipoMedioPagoDisponivel == novoValor.tipoMedioPagoDisponivel;
            })[0];

            // Se checkbox estiver marcado
            if (novoValor.detallar() == true) {
                // adiciona item marcado na coleção de valores a modificar e notifica ao campo calculado mudança de estado para refletir o total na tela.
                vmMP2.totalesMedioPagoModificar.push(novoValor);
                vmMP2.dummy.notifySubscribers();
            }
            else {
                vmMP2.totalesMedioPagoModificar = $.grep(vmMP2.totalesMedioPagoModificar, function (val, index) {
                    return val != novoValor;
                });

                // Remove item do grid de modificado
                //$.each(vmMP2.totalesMedioPago(), function (index, totalMedioPago) {

                //    if (totalMedioPago == null || totalMedioPago.divisa() == null)
                //        return;

                //    if (totalMedioPago.divisa().Identificador == novoValor.divisa().Identificador) {
                //        vmMP2.totalesMedioPago.remove(totalMedioPago);
                //    };

                //});
                vmMP2.dummy.notifySubscribers();
            }
        });
    };

    this.RegistrarVMSaldoMedioPagoDetallar = function (vmMP) {
        TotalesMedioPagoViewModelDetallar.push(vmMP);

        for (var i = 0; i < TotalesMedioPagoViewModelDetallar.length; i++) {
            atualizaTotalesMedioPago(TotalesMedioPagoViewModelDetallar[i]);
        };

        if (TotalesMedioPagoViewModelModificar != null) {

            for (var i = 0; i < TotalesMedioPagoViewModelModificar.length; i++) {

                if (TotalesMedioPagoViewModelModificar[i].tipoMedioPagoDisponivel == vmMP.tipoMedioPagoDisponivel) {

                    if (TotalesMedioPagoViewModelModificar[i].totalesMedioPago != null) {

                        for (var j = 0; j < TotalesMedioPagoViewModelModificar[i].totalesMedioPago().length; j++) {

                            if (TotalesMedioPagoViewModelModificar[i].totalesMedioPago()[j].detallar() == true) {

                                TotalesMedioPagoViewModelModificar[i].ManejoSaldo(TotalesMedioPagoViewModelModificar[i].totalesMedioPago()[j]);
                                atualizaTotalesMedioPagoModificar(TotalesMedioPagoViewModelModificar[i]);
                                
                            };

                        };

                    };
                };
            };
        };
    };

    };

    //#endregion

//#endregion

//#region Construtores

var _ControlarSaldo;

function GetControlarSaldo() {
    if (_ControlarSaldo == null) {
        _ControlarSaldo = new ControlarSaldo();
    }
    return _ControlarSaldo;
}

function InicializarSaldo() {
    _ControlarSaldo = new ControlarSaldo();
}

//#endregion

//#region Funções GENÉRICAS

function RemoverFormato(valor, parametros) {

    if (valor != null) {

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
    }

    return '0';
};

function IncluirFormato(valor, parametros) {

    if (valor === undefined)
        return 0;

    if (valor != 0) {
        var sd = parametros.SeparadorDecimal;
        var sm = parametros.SeparadorMilhar;
        valor = RemoverFormato(valor, parametros);

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

//#endregion