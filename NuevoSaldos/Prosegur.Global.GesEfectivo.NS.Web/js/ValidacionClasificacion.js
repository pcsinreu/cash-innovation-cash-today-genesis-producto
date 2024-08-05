function validarValoresClasificacion(_Diccionario, dvUcSaldoEfectivoMPModificar, pnlSaldoEfectivoModificar, pnlSaldoEfectivoDetallar, pnlSaldoMedioPagoModificar, pnlSaldoMedioPagoDetallar) {

    if (document.getElementById(dvUcSaldoEfectivoMPModificar).style.display == 'none') {
        return true;
    }

    var error = [];
    var hayValores = false;

    //#region Validação EFETIVO

    var valoresModificar = [];
    var valoresDetallar = [];

    inputs = document.getElementById(pnlSaldoEfectivoModificar).getElementsByTagName('input');
    for (x = 0; x < inputs.length; x++) {
        var _valor = inputs[x].value.substr(inputs[x].value.indexOf(';') + 1);
        if (inputs[x].type == 'hidden' && inputs[x].id.indexOf('hdftotalesEfectivo') > -1 && _valor != '' && _valor != '[]') {
            try {
                valoresModificar[valoresModificar.length] = JSON.parse(_valor);
                if (valoresModificar[valoresModificar.length - 1][valoresModificar.length - 1].Valor != null) {
                    hayValores = true;
                }
            } catch (err) {
                console.log(inputs[x].id + ": " + inputs[x].value);
            }
        }
    }

    if (valoresModificar.length > 0) {

        inputs = document.getElementById(pnlSaldoEfectivoDetallar).getElementsByTagName('input');
        for (x = 0; x < inputs.length; x++) {
            var _valor = inputs[x].value.substr(inputs[x].value.indexOf(';') + 1);
            if (inputs[x].type == 'hidden' && inputs[x].id.indexOf('hdftotalesEfectivo') > -1 && _valor != '' && _valor != '[]') {
                try {
                    valoresDetallar[valoresDetallar.length] = JSON.parse(_valor);
                    if (valoresDetallar[valoresDetallar.length - 1][valoresDetallar.length - 1].Valor != null) {
                        hayValores = true;
                    }
                } catch (err) {
                    console.log(inputs[x].id + ": " + inputs[x].value);
                }
            }
        }
    }

    for (x = 0; x < valoresModificar.length; x++) {

        var valorTotalDivisa = 0;
        var identificadorDivisa = '';
        var descripcionDivisa = '';

        for (y = 0; y < valoresModificar[x].length; y++) {
            if (valoresModificar[x][y] != undefined || valoresModificar[x][y] != null) {
                valorTotalDivisa += RondaValor(parseFloat(RemoverFormato_v2(valoresModificar[x][y].Valor)));
                identificadorDivisa = valoresModificar[x][y].Divisa.Identificador;
                descripcionDivisa = valoresModificar[x][y].Descripcion;
            }
        }

        var valorTotalDivisaDetalle = 0;
        for (d = 0; d < valoresDetallar.length; d++) {
            for (z = 0; z < valoresDetallar[d].length; z++) {
                if (valoresDetallar[d][z] != undefined || valoresDetallar[d][z] != null) {
                    if (valoresDetallar[d][z].Divisa.Identificador == identificadorDivisa) {
                        valorTotalDivisaDetalle += RondaValor(parseFloat(RemoverFormato_v2(valoresDetallar[d][z].Valor)));
                    }
                }
            }
        }

        if (RondaValor(valorTotalDivisa) != RondaValor(valorTotalDivisaDetalle)) {
            error[error.length] = JSON.parse('{"Identificador": "' + identificadorDivisa + '", "Descripcion": "' + descripcionDivisa + '", "TipoMedioPago": "' + '' + '", "Diferencia": "' + (valorTotalDivisa - valorTotalDivisaDetalle) + '"}');
        }
    }

    //#endregion

    //#region Validação MEDIO PAGO

    var valoresModificarMP = [];
    var valoresDetallarMP = [];

    inputs = document.getElementById(pnlSaldoMedioPagoModificar).getElementsByTagName('input');
    for (x = 0; x < inputs.length; x++) {
        var _valor = inputs[x].value.substr(inputs[x].value.indexOf(';') + 1);
        if (inputs[x].type == 'hidden' && inputs[x].id.indexOf('hdftotalesMedioPago') > -1 && _valor != '' && _valor != '[]') {
            try {
                valoresModificarMP[valoresModificarMP.length] = JSON.parse(_valor);
                if (valoresModificarMP[valoresModificarMP.length - 1][valoresModificarMP.length - 1].Valor != null) {
                    hayValores = true;
                }
            } catch (err) {
                console.log(inputs[x].id + ": " + inputs[x].value);
            }
        }
    }

    if (valoresModificarMP.length > 0) {

        inputs = document.getElementById(pnlSaldoMedioPagoDetallar).getElementsByTagName('input');
        for (x = 0; x < inputs.length; x++) {
            var _valor = inputs[x].value.substr(inputs[x].value.indexOf(';') + 1);
            if (inputs[x].type == 'hidden' && inputs[x].id.indexOf('hdftotalesMedioPago') > -1 && _valor != '' && _valor != '[]') {
                try {
                    valoresDetallarMP[valoresDetallarMP.length] = JSON.parse(_valor);
                    if (valoresDetallarMP[valoresDetallarMP.length - 1][valoresDetallarMP.length - 1].Valor != null) {
                        hayValores = true;
                    }
                } catch (err) {
                    console.log(inputs[x].id + ": " + inputs[x].value);
                }
            }
        }
    }

    var valorTotalDivisa = [];
    var idxModificar = 0;

    for (x = 0; x < valoresModificarMP.length; x++) {

        var identificadorDivisa = '';
        var descripcionDivisa = '';
        var tipoMedioPago = '';

        for (y = 0; y < valoresModificarMP[x].length; y++) {

            if (valoresModificarMP[x][y] != undefined || valoresModificarMP[x][y] != null) {

                identificadorDivisa = valoresModificarMP[x][y].Divisa.Identificador;
                descripcionDivisa = valoresModificarMP[x][y].Descripcion;
                tipoMedioPago = valoresModificarMP[x][y].TipoMedioPago;

                if (idxModificar == 0) {

                    valorTotalDivisa[idxModificar] = [];
                    valorTotalDivisa[idxModificar][0] = identificadorDivisa;
                    valorTotalDivisa[idxModificar][1] = descripcionDivisa;
                    valorTotalDivisa[idxModificar][2] = tipoMedioPago;
                    valorTotalDivisa[idxModificar][3] = RondaValor(parseFloat(RemoverFormato_v2(valoresModificarMP[x][y].Valor)));
                    idxModificar++;
                }
                else {

                    var indice = VerificarExisteDivisaTipoMedioPago(valorTotalDivisa, identificadorDivisa, tipoMedioPago);

                    // Não existe
                    if (indice == -1) {

                        valorTotalDivisa[idxModificar] = [];
                        valorTotalDivisa[idxModificar][0] = identificadorDivisa;
                        valorTotalDivisa[idxModificar][1] = descripcionDivisa;
                        valorTotalDivisa[idxModificar][2] = tipoMedioPago;
                        valorTotalDivisa[idxModificar][3] = RondaValor(parseFloat(RemoverFormato_v2(valoresModificarMP[x][y].Valor)));
                        idxModificar++;
                    }
                        // existe
                    else {
                        valorTotalDivisa[indice][3] += RondaValor(parseFloat(RemoverFormato_v2(valoresModificarMP[x][y].Valor)));
                    }

                }

            }

        } //valoresModificarMP
    }
    var valorTotalDivisaDetalle = [];

    var idxDetallar = 0;
    for (d = 0; d < valoresDetallarMP.length; d++) {

        for (z = 0; z < valoresDetallarMP[d].length; z++) {
            if (valoresDetallarMP[d][z] != undefined && valoresDetallarMP[d][z] != null) {

                if (valoresDetallarMP[d][z].Valor != undefined && valoresDetallarMP[d][z].Valor != 'NaN') {

                    identificadorDivisa = valoresDetallarMP[d][z].Divisa.Identificador;
                    descripcionDivisa = valoresDetallarMP[d][z].Descripcion;
                    tipoMedioPago = valoresDetallarMP[d][z].TipoMedioPago;

                    if (idxDetallar == 0) {

                        valorTotalDivisaDetalle[idxDetallar] = [];
                        valorTotalDivisaDetalle[idxDetallar][0] = identificadorDivisa;
                        valorTotalDivisaDetalle[idxDetallar][1] = descripcionDivisa;
                        valorTotalDivisaDetalle[idxDetallar][2] = tipoMedioPago;
                        valorTotalDivisaDetalle[idxDetallar][3] = RondaValor(parseFloat(RemoverFormato_v2(valoresDetallarMP[d][z].Valor)));
                        idxDetallar++;
                    }
                    else {

                        var indice = VerificarExisteDivisaTipoMedioPago(valorTotalDivisaDetalle, identificadorDivisa, tipoMedioPago);

                        // Não existe
                        if (indice == -1) {

                            valorTotalDivisaDetalle[idxDetallar] = [];
                            valorTotalDivisaDetalle[idxDetallar][0] = identificadorDivisa;
                            valorTotalDivisaDetalle[idxDetallar][1] = descripcionDivisa;
                            valorTotalDivisaDetalle[idxDetallar][2] = tipoMedioPago;
                            valorTotalDivisaDetalle[idxDetallar][3] = RondaValor(parseFloat(RemoverFormato_v2(valoresDetallarMP[d][z].Valor)));
                            idxDetallar++;
                        }
                            // existe
                        else {
                            valorTotalDivisaDetalle[indice][3] += parseFloat(RemoverFormato_v2(valoresDetallarMP[d][z].Valor));
                        }

                    }
                }
            }

        }
    }

    if ((valorTotalDivisa != null && valorTotalDivisa.length > 0) && (valorTotalDivisaDetalle != null && valorTotalDivisaDetalle.length > 0)) {

        for (a = 0; a < valorTotalDivisa.length; a++) {

            var valorDetalleRecuperado = valorTotalDivisaDetalle.filter(function (valorDetalle) { return valorDetalle[0] == valorTotalDivisa[a][0] && valorDetalle[2] == valorTotalDivisa[a][2] });

            if (valorDetalleRecuperado != null && valorDetalleRecuperado.length > 0) {

                if (RondaValor(valorTotalDivisa[a][3]) != RondaValor(valorDetalleRecuperado[0][3])) {
                    error[error.length] = JSON.parse('{"Identificador": "' + valorTotalDivisa[a][0] + '", "Descripcion": "' + valorTotalDivisa[a][1] + '", "TipoMedioPago": "' + valorTotalDivisa[a][2] + '"}');
                }

            } else {
                error[error.length] = JSON.parse('{"Identificador": "' + valorTotalDivisa[a][0] + '", "Descripcion": "' + valorTotalDivisa[a][1] + '", "TipoMedioPago": "' + valorTotalDivisa[a][2] + '"}');
            }

        }

    }
    else if ((valorTotalDivisa != null && valorTotalDivisa.length > 0) && (valorTotalDivisaDetalle == null || valorTotalDivisaDetalle.length == 0)) {

        for (a = 0; a < valorTotalDivisa.length; a++) {
            error[error.length] = JSON.parse('{"Identificador": "' + valorTotalDivisa[a][0] + '", "Descripcion": "' + valorTotalDivisa[a][1] + '", "TipoMedioPago": "' + valorTotalDivisa[a][2] + '"}');
        }

    } else if ((valorTotalDivisa != null && valorTotalDivisa.length > 0) && (valorTotalDivisaDetalle == null || valorTotalDivisaDetalle.length == 0)) {
        for (a = 0; a < valorTotalDivisaDetalle.length; a++) {
            error[error.length] = JSON.parse('{"Identificador": "' + valorTotalDivisaDetalle[a][0] + '", "Descripcion": "' + valorTotalDivisaDetalle[a][1] + '", "TipoMedioPago": "' + valorTotalDivisaDetalle[a][2] + '"}');
        }
    }

    //#endregion

    if (error.length == 0 && hayValores == false) {

        alert(_Diccionario.msg_sinvalores);
    } else if (error.length > 0) {
        var divisas = '';
        var divisaAnterior = '';
        var tiposMedioPago = '';
        for (d = 0; d < error.length; d++) {

            if (error[d].TipoMedioPago != '') {

                if (tiposMedioPago.indexOf(error[d].TipoMedioPago + '/' + error[d].Descripcion + '; ') == -1) {
                    tiposMedioPago += error[d].TipoMedioPago + '/' + error[d].Descripcion + '; ';
                }
            }
            else {
                if (divisas.indexOf(error[d].Descripcion + '; ') == -1) {
                    divisas += error[d].Descripcion + '; ';
                }
            }
        }

        if (tiposMedioPago != '' && divisas != '') {
            alert(_Diccionario.msg_valoresinsuficienteefectivos + ' ' + divisas + '<br /> <br />' + _Diccionario.msg_valoresinsuficientesmediospago + ' ' + tiposMedioPago);
        }
        else if (tiposMedioPago != '' && divisas == '') {
            alert(_Diccionario.msg_valoresinsuficientesmediospago + ' ' + tiposMedioPago);
        }
        else if (tiposMedioPago == '' && divisas != '') {
            alert(_Diccionario.msg_valoresinsuficienteefectivos + ' ' + divisas);
        }

    } else {
        return true;
    }

    return false;
}

// verifica se existe divisa e tipo medio pago, se existir devolve o indice da coleção
function VerificarExisteDivisaTipoMedioPago(valorTotalDivisa, idDivisa, tipoMedioPago) {

    if (valorTotalDivisa != null && valorTotalDivisa.length > 0) {

        for (a = 0; a < valorTotalDivisa.length; a++) {

            //Coluna 0 = IdDivisa - Coluna 2 = TipoMedioPago
            if (valorTotalDivisa[a][0] == idDivisa && valorTotalDivisa[a][2] == tipoMedioPago) {
                return a;
            }
        }
    }

    return -1;

};

function RemoverFormato_v2(valor) {

    if (valor != null) {

        if (valor.length > 0) {
            var sd = ',';
            var sm = '.';
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
    }
    if (valor == '') {
        valor = '0';
    }

    return valor;
};

function validarSaldoClasificacion(identificadorDocumento, identificadorCuentaSaldos, btnConfirmar) {
    console.log("validarSaldoClasificacion: (identificadorDocumento='" + identificadorDocumento + "', identificadorCuentaSaldos = '" + identificadorCuentaSaldos + "')");
    $('#dvBloquearTela').css('display', 'block');
    jQuery.ajax({
        url: '../ServiciosInterface.asmx/validarSaldoClasificacion',
        type: "POST",
        dataType: "json",
        data: "{identificadorDocumento: '" + identificadorDocumento + "', identificadorCuentaSaldos: '" + identificadorCuentaSaldos + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data, text) {
            var json_x = JSON.parse(data.d);
            if (json_x.CodigoError == "0") {
                __doPostBack(btnConfirmar, "");
            } else {
                $('#dvBloquearTela').css('display', 'none');
                alert(json_x.MensajeError);
            }
        },
        error: function (request, status, error) {
            $('#dvBloquearTela').css('display', 'none');
            genesisAlertError('Error: ' + error, request.responseText);
        }
    });

    return false;
}
