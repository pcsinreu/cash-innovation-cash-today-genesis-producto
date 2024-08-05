
// variables defecto
var aplicaciones = null;
var msg_loading = 'msg_loading';
var msg_redirecionando = 'msg_redirecionando';
var msg_producidoError = 'msg_producidoError';
var msg_obtenerAplicaciones = 'msg_obtenerAplicaciones';
var msg_crearToken = "msg_crearToken";
var boton_templade = '<button id="btn_{0}" type="button" class="app" onclick="return crearToken(\'{1}\');"><div style="background-image:url(./_Imagens/{2}.png);"><br /><span>{3}</span><br /><span class="version">{4}</span></div></button>';

function obtenerAplicaciones() {
    console.log("Obtener Aplicaciones");

    genesisAlertLoading(msg_loading + msg_obtenerAplicaciones);
    jQuery.ajax({
        url: 'Aplicaciones.aspx/obtenerAplicaciones',
        type: "POST",
        dataType: "json",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        success: function (data, text) {
            //alert(JSON.stringify(data));
            var json_x = JSON.parse(data.d);
            if (json_x.CodigoError == "0" && json_x.Respuesta.length > 0) {
                aplicaciones = json_x.Respuesta;
                genesisAlertLoading("");
                cargarAplicaciones();

            } else {
                genesisAlertError(json_x.MensajeError, json_x.MensajeErrorDescriptiva);
            }
        },
        error: function (request, status, error) {
            genesisAlertError(msg_producidoError + error, request.responseText);
        }
    });
}

function cargarAplicaciones() {
    var codigoHTML = "";
    for (i = 0; i < aplicaciones.length; i++) {
        if (i == 0) {
            campoPrincipal = "btn_" + aplicaciones[i].CodigoAplicacion;
        }
        codigoHTML += String.format(boton_templade, aplicaciones[i].CodigoAplicacion, aplicaciones[i].CodigoAplicacion, aplicaciones[i].CodigoAplicacion, aplicaciones[i].DescripcionAplicacion, aplicaciones[i].CodigoVersion);
    }
    $("#dvListaAplicaciones").html(codigoHTML);
    $("#dvListaAplicaciones").show("slow", function () {
        focusInicial();
    });
    
}

function crearToken(codigoAplicacion) {
    console.log("Crear Token");
    genesisAlertLoading(msg_loading + msg_crearToken);
    jQuery.ajax({
        url: 'Aplicaciones.aspx/crearToken',
        type: "POST",
        dataType: "json",
        data: "{codigoAplicacion: '" + codigoAplicacion + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data, text) {
            var json_x = JSON.parse(data.d);
            if (json_x.CodigoError == "0" && json_x.Respuesta != null) {
                genesisAlertLoading(msg_loading + msg_redirecionando);
                window.open(json_x.Respuesta);
                genesisAlertLoading("");
            } else {
                genesisAlertError(json_x.MensajeError, json_x.MensajeErrorDescriptiva);
            }
        },
        error: function (request, status, error) {
            genesisAlertError(msg_producidoError + error, request.responseText);
        }
    });
}

$(document).ready(function () {
    obtenerAplicaciones();
});