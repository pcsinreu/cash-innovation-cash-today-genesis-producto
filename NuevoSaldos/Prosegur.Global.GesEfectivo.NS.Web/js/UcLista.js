
function seleccionarTodos(a, b, c, dvlista, funcion) {
    inputs = a.getElementsByTagName('input');
    document.getElementById(c).value = '';
    var listaItem = "";
    for (x = 0; x < inputs.length; x++) {
        if (inputs[x].type == 'checkbox') {
            inputs[x].checked = b;

            if (b == true) {
                document.getElementById(c).value += inputs[x].value + ';';
                listaItem += inputs[x].value + ';';
            }
            else{
                listaItem += inputs[x].value + ';';
            }            
        }
    }
    if (funcion != undefined && funcion != "" && funcion != null) {        
        obtenerValores(dvlista, funcion, listaItem, b);
    }    
}

function cambiarLegenda(a, b) {
    a.innerText = b;
}

function SeleccionarValor(a, b, c) {
    if (document.getElementById(b).checked == true) {    
        document.getElementById(c).value += a + ';';
    } else {
        var res = document.getElementById(c).value.replace(a + ";", "");
        document.getElementById(c).value = res;
    }
    
}

function SeleccionarValorObtenerLista(a, b, c, dvlista, funcion) {
   
    if (document.getElementById(b).checked == true) {
        document.getElementById(c).value += a + ';';
        obtenerValores(dvlista, funcion, a, true);
    } else {
        var res = document.getElementById(c).value.replace(a + ";", "");
        document.getElementById(c).value = res;
        obtenerValores(dvlista, funcion, a, false);
    }

    
}

var listaValores;
var msg_obtenerDelegaciones = 'msg_obtenerDelegaciones';
var msg_cargarValores = 'msg_cargarValores';
var msg_loading = 'msg_loading';
var msg_producidoError = 'msg_producidoError';

function obtenerValores(dvlista, funcion, valor, inserir) {
    genesisAlertLoading(msg_loading + msg_obtenerDelegaciones);
    jQuery.ajax({
        url: funcion,
        type: "POST",
        dataType: "json",
        data: "{identificador: '" + valor + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data, text) {
            //alert(JSON.stringify(data));
            var json_x = JSON.parse(data.d);
            if (json_x.CodigoError == "0" && json_x.Respuesta.Lista.length > 0) {
                listaValores = json_x.Respuesta.Lista;
                genesisAlertLoading("");

                RemoverValores(dvlista);
                if (inserir) {
                    cargarValores(dvlista);
                }

                //atualiza a quantidade de itens que existem para ser selecionados
                var hidCantidadItens = dvlista + "_hidCantidadItens";
                
                var cant = document.getElementById(hidCantidadItens);
                var dvValores = document.getElementById(dvlista + "_dvValores");
                cant.value = dvValores.getElementsByTagName('input').length;
            } else {
                genesisAlertError(json_x.MensajeError, json_x.MensajeErrorDescriptiva);
            }
        },
        error: function (request, status, error) {
            genesisAlertError(msg_producidoError + error, request.responseText);
        }
    });
}

function RemoverValores(dvlista) {
   // debugger;
    var div = document.getElementById(dvlista + '_dvValores');

    if (listaValores.length > 0) {
        for (i = 0; i < listaValores.length; i++) {
                        
            $("#dv" + dvlista + '_' + listaValores[i].Identificador).remove();
            
        }
    }
}

function cargarValores(dvlista) {
    console.log("Cargar Valores");

    genesisAlertLoading(msg_loading + msg_cargarValores);

    var div = document.getElementById(dvlista + '_dvValores');

    if (listaValores.length > 0) {
        for (i = 0; i < listaValores.length; i++) {

            var item = "";

            item = '<div id="dv' + dvlista + '_' + listaValores[i].Identificador + '" style="width: 100%; display: inline-block;">';
            item += '<input name="' + dvlista + '$' + listaValores[i].Identificador + '" type="checkbox" id="' + dvlista + '_' + listaValores[i].Identificador + '" value="' + listaValores[i].Identificador + '" onchange="SeleccionarValor(\'' + listaValores[i].Identificador + '\', \'' + dvlista + '_' + listaValores[i].Identificador + '\', \'' + dvlista + '_hidValoresSeccionados\')" >' + listaValores[i].Descripcion;
            item += '</div>';

            $("#" + dvlista + '_dvValores').append(item);
                        
        }
    }
    genesisAlertLoading("");
}

function HomeEnd(a) {

    if (event.which || event.keyCode) {

        if ((event.which == 35) || (event.keyCode == 35)) {
            inputs = a.getElementsByTagName('input');
            for (x = 0; x < inputs.length; x++) {
                if (inputs[x].type == 'checkbox') {
                    inputs[x].focus();
                }
            }
        }
        if ((event.which == 36) || (event.keyCode == 36)) {
            inputs = a.getElementsByTagName('input');
            for (x = inputs.length - 1; x >= 0; x--) {
                if (inputs[x].type == 'checkbox') {
                    inputs[x].focus();
                }
            }
        }
    }
    return false;
}

function atribuirFuncion(a, b) {
    inputs = a.getElementsByTagName('input');
    for (x = 0; x < inputs.length; x++) {
        if (inputs[x].type == 'checkbox') {
            inputs[x].onkeydown = function () { HomeEnd(a); };
        }
    }
}