$(document).ready(function () {
    $('.page').keypress(function (e) {
        if (e.which == 13) {
            e.preventDefault();
        }
    });
    $('.txtEliminar').val('');
});

function seleccionarElemento(a, b) {
    var $btn = $(a);
    var $div = $btn.parents('.dvListaElemento');
    if (b != null) {
        $div.css("background-color", b);
    } else {
        $div.css("background-color", '#f9f0d1');
    }
}

function buscarElemento(a) {
    var valor = a.value;
    var $btn = $(".dvListaElemento input[value='" + valor + "']");
    if ($btn.length == 1) {
        $btn.focus();
    } else {
        var arrayElements = document.getElementsByTagName('input');
        for (var i = 0; i < arrayElements.length; i++) {
            if (arrayElements[i].type == 'hidden') {
                if (arrayElements[i].name == valor) {
                    var $btn = $(".dvListaElemento input[value='" + arrayElements[i].value + "']");
                    if ($btn.length >= 1) {
                        $btn.focus();
                    }
                }
            }
        }
    }
    a.value = "";
}

function seleccionarAgregarElemento(a) {
    var valor = a.value;
    var $btn = $(".dvListaElemento input[value='" + valor + "']");

    a.value = "";
    if ($btn.length == 1) {
        __doPostBack('agregar_elemento', valor);
    } else {
        var arrayElements = document.getElementsByTagName('input');
        for (var i = 0; i < arrayElements.length; i++) {
            if (arrayElements[i].type == 'hidden') {
                if (arrayElements[i].name == valor) {
                    valor = arrayElements[i].value;
                }
            }
        }
        __doPostBack('agregar_elemento', valor);
    }
}

function quitarSelecionado(a, b) {
    if (!event.keyCode || event.keyCode == 46 || event.keyCode == 13) {
        var $btn = $(".dvListaElemento input[value='" + a + "']");
        var txt = document.getElementById(b);
        txt.focus();
        if ($btn.length == 1) {
            __doPostBack('remover_elemento', a);
        } else {
            var arrayElements = document.getElementsByTagName('input');
            for (var i = 0; i < arrayElements.length; i++) {
                if (arrayElements[i].type == 'hidden') {
                    if (arrayElements[i].name == a) {
                        a = arrayElements[i].value;
                    }
                }
            }
            __doPostBack('remover_elemento', a);
        }
        
        txt.value ='';
        return false;
    }
}