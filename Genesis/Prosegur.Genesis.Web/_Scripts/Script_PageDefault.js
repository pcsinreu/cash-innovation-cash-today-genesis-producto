
/// <reference path="scripts.js" />

// variables defecto
var paises = null;
var Default_lblUsuario = 'Default_lblUsuario';
var Default_lblSenha = 'Default_lblSenha';
var Default_lblPlanta = 'Default_lblPlanta';
var Default_lblPais = 'Default_lblPais';
var msg_loading = 'msg_loading';
var msg_producidoError = 'msg_producidoError';
var gen_msg_camporequerido = 'gen_msg_camporequerido';
var msg_obtenerPaises = 'obtenerPaises';
var msg_cargarPaises = 'cargarPais';
var msg_iniciarSesion = 'iniciarSesion';
var msg_seleccione = 'Seleccione';
var msg_version = 'VERSION';

function btnLogin_OnClick() {
    if (validarDatos()) {
        iniciarSesion();
    }
}

function btnCancelar_OnClick() {
    $("#txtUsuario").val("");
    $("#txtSenha").val("");
    cargarPaises();
}

function cargarPaises() {
    console.log("Cargar Paises");

    genesisAlertLoading(msg_loading + msg_cargarPaises);

    if (paises.length > 0) {
        for (i = 0; i < paises.length; i++) {
            $("#ddlPais").append(new Option(paises[i].DescripcionPais, paises[i].CodigoPais));
        }
    }
    genesisAlertLoading("");
}

function obtenerPaises() {
    console.log("Obtener Paises");

    genesisAlertLoading(msg_loading + msg_obtenerPaises);
    jQuery.ajax({
        url: 'Default.aspx/obtenerPaises',
        type: "POST",
        dataType: "json",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        success: function (data, text) {
            var json_x = JSON.parse(data.d);
            if (json_x.CodigoError == "0" && json_x.Respuesta.length > 0) {
                paises = json_x.Respuesta;
                genesisAlertLoading("");
                cargarPaises();

            } else {
                genesisAlertError(json_x.MensajeError, json_x.MensajeErrorDescriptiva);
            }
        },
        error: function (request, status, error) {
            genesisAlertError(msg_producidoError + error, request.responseText);
        }
    });
}

function validarDatos() {
    console.log("Validar Datos");

    var msg_error = "";
    if ($("#txtUsuario").val() == ""){
        msg_error += String.format(gen_msg_camporequerido, Default_lblUsuario) + '<br>';
    }
    if ($("#txtSenha").val() == "") {
        msg_error += String.format(gen_msg_camporequerido, Default_lblSenha) + '<br>';
    }
    if ($("#ddlPais").val() == "") {
        msg_error += String.format(gen_msg_camporequerido, Default_lblPais) + '<br>';
    }

    if (msg_error != "") {
        genesisAlertError(msg_error, "");
        return false;
    }
    return true;
}

function iniciarSesion() {
    console.log("Iniciar Sesión");
    genesisAlertLoading(msg_loading + msg_iniciarSesion);
    console.log("Pasó el alert loading");
    //$("#txtUsuario").val().replace("\\", "\\\\") + "', password: '" + $("#txtSenha").val() + "', descripcionPais: '" + $("#ddlPais").val() 
    console.log("Usuario: " + $("#txtUsuario").val().replace("\\", "\\\\"));
    console.log("Pais: " + $("#ddlPais").val());

    jQuery.ajax({
        url: 'Default.aspx/iniciarSesion',
        type: "POST",
        dataType: "json",
        data: "{usuario: '" + $("#txtUsuario").val().replace("\\", "\\\\") + "', password: '" + $("#txtSenha").val() + "', codigoPais: '" + $("#ddlPais").val() + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data, text) {
            var json_x = JSON.parse(data.d);
            console.log(json_x);
            if (json_x.CodigoError == "0" && json_x.Respuesta != null) {
                console.log("Salió todo bien");
                genesisAlertLoading("");
                window.location.href = json_x.Respuesta;
            } else {
                console.log("Todo mal");
                genesisAlertError(json_x.MensajeError, json_x.MensajeErrorDescriptiva);
            }
        },
        error: function (request, status, error) {
            genesisAlertError(msg_producidoError + error, request.responseText);
        }
    });
}

$(document).ready(function () {
    obtenerPaises();
    campoPrincipal = "txtUsuario";
});
