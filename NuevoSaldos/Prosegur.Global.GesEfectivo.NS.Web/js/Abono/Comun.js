function crearModal(div_nombre, div_width, div_height, callback) {
    $('#' + div_nombre).dialog({
        modal: true,
        width: div_width,
        height: div_height,
        autoOpen: false,
        open: function () { $(".ui-dialog-titlebar-close").hide(); }
    });
}
function showModal(div_nombre) {
    if ($('#' + div_nombre).dialog("isOpen") != true) {
        $('#' + div_nombre).dialog("open").dialog({ closeOnEscape: false });;
    }
}
function closeModal(div_nombre) {
    if ($('#' + div_nombre).dialog("isOpen") == true) {
        $('#' + div_nombre).dialog("close");
    }
}

function IncluirFormato(codigoISO, importe) {

    codigoISO = '' + codigoISO + ' '; 

    importe = IncluirFormatoDecimal(importe);

    return codigoISO + importe;
};

function IncluirFormatoDecimal(importe) {
    if (!isNaN(importe)) {

        return Intl.NumberFormat(navigator.language, { minimumFractionDigits: 2, maximumFractionDigits: 2 }).format(importe);

        //importe = parseFloat(Math.round(importe * 1000) / 1000).toFixed(2);
        //var sd = ',';
        //var sm = '.';
        //importe = RemoverFormato(importe);

        //var valores = importe.toString().split(".");
        //if (valores.length == 1) {
        //    importe = valores[0] + "00";
        //}
        //else if (valores.length == 2) {

        //    if (valores[1].length == 1) {
        //        valores[1] = valores[1] + "0";
        //    }

        //    importe = valores[0] + valores[1];
        //}

        //importe = importe.replace(/(\d{1})(\d{11})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 8 digitos
        //importe = importe.replace(/(\d{1})(\d{8})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 8 digitos
        //importe = importe.replace(/(\d{1})(\d{5})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 5 digitos
        //importe = importe.replace(/(\d{1})(\d{0,2})$/, "$1" + sd + "$2") //coloca virgula antes dos últimos 2 digitos
    }

    return importe;
}


function RemoverFormato(valor) {
    if (!valor) valor = '';
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

    if (valor == '') {
        valor = '0';
    }

    return valor;
};

function SubtrairValoresTolerenacia2(valor1, valor2) {

    if (typeof (valor1) == "string") {
        valor1 = Number(valor1);
    }

    if (typeof (valor2) == "string") {
        valor2 = Number(valor2);
    }

    return ObtenerValorNumerico(valor1 - valor2);

}

function SomarValoresTolerenacia2(valor1, valor2) {
    if (typeof (valor1) == "string") {
        valor1 = Number(valor1);
    }

    if (typeof (valor2) == "string") {
        valor2 = Number(valor2);
    }

    return ObtenerValorNumerico(valor1 + valor2);
}

function ObtenerValorNumerico(valor) {

    if (typeof (valor) == "string") {

        valor = RemoverFormato(valor);

        valor = Number(valor);
    }

    valor = valor.toFixed(2);

    return Number(valor);

}

function MoverElementoArray(arr, indexDe, indexPara) {
    var elemento = arr[indexDe];
    arr.splice(indexDe, 1);
    arr.splice(indexPara, 0, elemento);
}
