function totalDenominacion(controle, idControle, idLinha, valorDenominacion) {
    try {
        var msg = '';
        var txtCantidad = document.getElementById(controle.id.replace(idControle, "txtCantidad"));
        var txtImporteDenominacion = document.getElementById(controle.id.replace(idControle, "txtImporteDenominacion"));
        var ddlUnidad = document.getElementById(controle.id.replace(idControle, "ddlUnidad"));

        var cantidadValue;
        var ImporteDenominacionValue;
        var total = 0.00;

        if (ddlUnidad != undefined) {
            var hdnImporteDenominacion = $('[id$=hdnImporteDenominacion]')[0];
            var codigoUnidade = ddlUnidad.options[ddlUnidad.selectedIndex].value;
            var valorUnidade = recuperarValorUnidade(codigoUnidade);

            if (valorUnidade > 0) {
                // procura pelo total
                //retira o nome do controle rptDetalle + o id da linha + o controle corrente
                var id = controle.id.replace(idControle, '');
                id = id.replace("rptDetalle_" + idLinha + "_", '');
                id = id + "txtImporteTotal";
                var txtImporteTotal = document.getElementById(id);

                if (idControle == "txtImporteDenominacion") {
                    if (hdnImporteDenominacion.value.RemoverFormato() != txtImporteDenominacion.value.RemoverFormato()) {
                        total = parseFloat(txtImporteDenominacion.value.RemoverFormato()) / parseFloat(valorDenominacion);
                        total = total.toFixed(2);
                        //se o valor for vazio não faz nada..
                        if ((total % parseFloat(valorUnidade)) != 0) {
                            
                            var par = JSON.parse(ParametrosJSON)
                            var msg = par.MensagemDivisioninvalida.replace("[X]", txtImporteDenominacion.value);
                            msg = msg.replace("[Y]", valorUnidade);
                            msg = msg.replace("[Z]", valorDenominacion);
                            if (txtImporteDenominacion.value != '') {
                                alert(msg);
                            }

                            txtImporteDenominacion.value = '';

                            //se o valor existia
                            if (txtCantidad.value != '') {
                                var cantidadAnterior = parseInt(txtCantidad.value);
                                var txtImporteDenominacionAnterior = parseInt(cantidadAnterior) * parseFloat(valorUnidade) * parseFloat(valorDenominacion);
                                total = parseFloat(txtImporteTotal.value.RemoverFormato()) - txtImporteDenominacionAnterior;
                                txtImporteTotal.value = total.toFixed(2);
                                txtImporteTotal.value = txtImporteTotal.value.IncluirFormato();
                            }

                            txtCantidad.value = '';
                            cantidadValue = '';
                            ImporteDenominacionValue = '';
                        }
                        else {

                            //valor anterior
                            var totalAnterior = parseFloat(txtImporteTotal.value.RemoverFormato());
                            var cantidad = Math.round((parseFloat(txtImporteDenominacion.value.RemoverFormato()) / parseFloat(valorDenominacion)) / parseFloat(valorUnidade));
                            
                            if (txtCantidad.value != '') {
                                var cantidadAnterior = parseInt(txtCantidad.value);
                                var txtImporteDenominacionAnterior = parseInt(cantidadAnterior) * parseFloat(valorUnidade) * parseFloat(valorDenominacion);

                                total = parseFloat(txtImporteTotal.value.RemoverFormato()) - txtImporteDenominacionAnterior + parseFloat(txtImporteDenominacion.value.RemoverFormato());
                                txtImporteTotal.value = total.toFixed(2);
                                txtImporteTotal.value = txtImporteTotal.value.IncluirFormato();
                            }
                            else {
                                total = parseFloat(txtImporteTotal.value.RemoverFormato()) + parseFloat(txtImporteDenominacion.value.RemoverFormato());
                                txtImporteTotal.value = total.toFixed(2);
                                txtImporteTotal.value = txtImporteTotal.value.IncluirFormato();
                            }

                            if (parseInt(cantidad) == 0) {
                                txtCantidad.value = '';
                            }
                            else {
                                txtCantidad.value = parseInt(cantidad);
                            }

                            cantidadValue = txtCantidad.value;
                            ImporteDenominacionValue = txtImporteDenominacion.value.RemoverFormato();
                        }
                    }
                }
                else {
                    if (txtCantidad.value == '') {
                        //atualiza o saldo se necessãrio
                        //verifica se o valor já existia no total:
                        if (txtImporteDenominacion.value != '') {
                            var txtImporteDenominacionAnterior = parseFloat(txtImporteDenominacion.value.RemoverFormato());
                            total = parseFloat(txtImporteTotal.value.RemoverFormato()) - txtImporteDenominacionAnterior;

                            txtImporteTotal.value = total.toFixed(2);
                            txtImporteTotal.value = txtImporteTotal.value.IncluirFormato();
                        }

                        txtImporteDenominacion.value = '';
                        hdnImporteDenominacion.value = '';

                        cantidadValue = '';
                        ImporteDenominacionValue = '';
                    }
                    else {
                        var valorAtual = parseInt(txtCantidad.value) * parseFloat(valorUnidade) * parseFloat(valorDenominacion);
                        valorAtual = parseFloat(valorAtual.toFixed(2));
                        //verifica se o valor já existia no total:
                        if (txtImporteDenominacion.value != '') {
                            var txtImporteDenominacionAnterior = parseFloat(txtImporteDenominacion.value.RemoverFormato());
                            total = parseFloat(txtImporteTotal.value.RemoverFormato()) - txtImporteDenominacionAnterior + valorAtual;
                            txtImporteTotal.value = total.toFixed(2);
                            txtImporteTotal.value = txtImporteTotal.value.IncluirFormato();
                        }
                        else {
                            total = parseFloat(txtImporteTotal.value.RemoverFormato()) + valorAtual;
                            txtImporteTotal.value = total.toFixed(2);
                            txtImporteTotal.value = txtImporteTotal.value.IncluirFormato();
                        }

                        txtImporteDenominacion.value = valorAtual;
                        txtImporteDenominacion.value = txtImporteDenominacion.value.IncluirFormato();
                        hdnImporteDenominacion.value = valorAtual;

                        cantidadValue = txtCantidad.value;
                        ImporteDenominacionValue = txtImporteDenominacion.value.RemoverFormato();
                    }
                }
            }
        }

    } catch (e) {
        alert(e.Message);
    }
}

function recuperarValorUnidade(codigoUnidade) {
    var par = JSON.parse(ParametrosJSON);
    for (var i = 0; i < par.UnidadesMedidas.length; i++) {
        var item = par.UnidadesMedidas[i];
        if (item.Codigo == codigoUnidade) {
            return item.Valor;
        }
    }
}

function DenominacionSemValor(controle, dropdrown) {
    var par = JSON.parse(ParametrosJSON);
    if (dropdrown) {
        alert(par.MensagemDenominacionSemValor);
    }
    else {
        if (controle.value != '') {
            controle.value = '';
            alert(par.MensagemDenominacionSemValor);
        }
    }

    return false;
}

function totalDivisa(controle) {
    try {
        var txtImporteTotal = document.getElementById(controle.id.replace('txtImporteDivisa', 'txtImporteTotal'));
        var valorAnterior = 0;

        var importeDivisaAtual = $('[id$=hdnImporteDivisaAtual]')[0];

        if (importeDivisaAtual.value == '') {
            valorAnterior = 0;
        }
        else {
            valorAnterior = parseFloat(importeDivisaAtual.value.RemoverFormato());
        }
        var total = 0.00;
        total = parseFloat(txtImporteTotal.value.RemoverFormato()) - valorAnterior + parseFloat(controle.value.RemoverFormato());
        txtImporteTotal.value = total.toFixed(2);
        txtImporteTotal.value = txtImporteTotal.value.IncluirFormato();
    } catch (e) {
        alert(e.Message);
    }
}

function AtualizarImporteDivisaAtual(controle) {
    var importeDivisaAtual = $('[id$=hdnImporteDivisaAtual]')[0];
    importeDivisaAtual.value = controle.value.RemoverFormato();
}

function AtualizarImporteDenominacion(controle) {
    var hdnImporteDenominacion = $('[id$=hdnImporteDenominacion]')[0];
    hdnImporteDenominacion.value = controle.value.RemoverFormato();
}

String.prototype.RemoverFormato = function () {
    var valor = this;
    if (valor.length > 0) {
        var par = JSON.parse(ParametrosJSON);
        var sd = par.SeparadorDecimal;
        var sm = par.SeparadorMilhar;
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

String.prototype.IncluirFormato = function () {
    var valor = this;
    if (valor.length > 0) {
        var par = JSON.parse(ParametrosJSON);
        var sd = par.SeparadorDecimal;
        var sm = par.SeparadorMilhar;
        valor = valor.RemoverFormato();

        var valores = valor.toString().split(".");
        if (valores.length == 1) {
            valor = valores[0] + "00";
        }
        else if (valores.length = 2) {
            valor = valores[0] + valores[1];
        }

        valor = valor.replace(/(\d{1})(\d{11})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 8 digitos
        valor = valor.replace(/(\d{1})(\d{8})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 8 digitos
        valor = valor.replace(/(\d{1})(\d{5})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 5 digitos
        valor = valor.replace(/(\d{1})(\d{0,2})$/, "$1" + sd + "$2") //coloca virgula antes dos últimos 2 digitos
    }

    return valor;
};
