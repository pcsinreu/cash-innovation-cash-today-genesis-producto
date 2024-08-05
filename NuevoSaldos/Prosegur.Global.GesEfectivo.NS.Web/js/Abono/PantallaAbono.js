/// <reference path="../jquery-1.10.1.min.js" />
/// <reference path="../genesis.js" />
/// <reference path="PantallaAbonoVM.js" />

function InicializarPantallaAbonoVM(dados) {
    if (dados.CodigoError != "0") {
        genesisAlertError(dados.MensajeError, dados.MensajeErrorDescriptiva);
        return;
    }

    var viewModel = new PantallaAbonoVM(dados.Respuesta);

    //#region Usabilidade
    viewModel.MesageLoading.subscribe(function (novoValor) { genesisAlertLoading(novoValor); });
    viewModel.MesageAlerta.subscribe(function (novoValor) { ExibirMensageAlerta(novoValor, viewModel); });
    viewModel.MesageAlertaJS.subscribe(function (novoValor) { ExibirMensageAlertaJS(novoValor, viewModel); });
    viewModel.MesageConfirmacaoJS.subscribe(function (novoValor) { ExibirMensageConfirmacaoJS(novoValor, viewModel); });
    viewModel.MesageAlertaError.subscribe(function (novoValor) { ExibirMensageAlertaError(novoValor, viewModel); });
    crearModal('dvFiltro', 860, 410, function () { });
    viewModel.ExibirFiltros.subscribe(function (novoValor) { ExibirPopupFiltrar(novoValor, viewModel); });
    viewModel.OcultarFiltros.subscribe(function (novoValor) { OcultarPopupFiltrar(novoValor, viewModel); });
    crearModal('dvDetalle', 1220, 550, function () { });
    viewModel.ExibirDetallar.subscribe(function (novoValor) { ExibirPopupDetallar(novoValor, viewModel); });
    //Popup chamado do popup Detalle [refatorar]
    crearModal('dvCadastroDatosBancarios', 500, 320, function () { });
    //Popup Generar Documento Pases
    crearModal('dvGenerarDocumentoPases', 860, 320, function () { });
    viewModel.ExibirDatosGeneracionDocPases.subscribe(function (novoValor) { ExibirPopupDatosGeneracionDocPases(novoValor, viewModel); });
    viewModel.OcultarDatosGeneracionDocPases.subscribe(function (novoValor) { OcultarPopupDatosGeneracionDocPases(novoValor, viewModel); });
    crearModal('dvAlertRedirect', 500, 120, function () { });
    viewModel.AlertRedirectVM.subscribe(function (novoValor) { ExibirPopupAlertRedirect(novoValor); });
    viewModel.OcultarDetallar.subscribe(function (novoValor) { OcultarPopupDetallar(novoValor, viewModel); });
    //#endregion

    ko.applyBindings(viewModel, $('.bindPage')[0]);
    ko.applyBindings(viewModel, $('#dvFiltro')[0]);
    ko.applyBindings(viewModel, $('#dvDetalle')[0]);
    if ($('#dvCadastroDatosBancarios')[0] != null)
        ko.applyBindings(viewModel, $('#dvCadastroDatosBancarios')[0]);
    if ($('#dvGenerarDocumentoPases')[0] != null)
        ko.applyBindings(viewModel, $('#dvGenerarDocumentoPases')[0]);
    if ($('#dvAlertRedirect')[0] != null)
        ko.applyBindings(viewModel, $('#dvAlertRedirect')[0]);
}

$(window).load(function () {
    SetarFocoInicial('cbTipoAbono');
});

function ExibirMensageAlerta(novoValor, viewModel) {
    if (novoValor != "") {
        genesisAlert(novoValor);
        viewModel.MesageAlerta("");
    }
}
function ExibirMensageAlertaJS(novoValor, viewModel) {
    if (novoValor != "") {
        alert(novoValor);
        viewModel.MesageAlertaJS("");
    }
}
function ExibirMensageConfirmacaoJS(novoValor, viewModel) {
    if (novoValor != "") {
        viewModel.ResultadoConfirmacaoJS(confirm(novoValor));
        viewModel.MesageConfirmacaoJS("");
    }
}
function ExibirMensageAlertaError(novoValor, viewModel) {
    if (novoValor != "") {
        genesisAlertError(novoValor, viewModel.MesageAlertaErrorDescriptiva());
        viewModel.MesageAlertaError("");
        viewModel.MesageAlertaErrorDescriptiva("");
    }
}

function ExibirPopupFiltrar(novoValor, viewModel) {
    if (novoValor) {
        viewModel.ExibirFiltros(false);
        viewModel.OcultarFiltros(false);
        showModal('dvFiltro');
    }
}
function OcultarPopupFiltrar(novoValor, viewModel) {
    if (novoValor) {
        viewModel.OcultarFiltros(false);
        closeModal('dvFiltro');
    }
}

function ExibirPopupAlertRedirect(novoValor) {
    if (novoValor) {
        showModal('dvAlertRedirect');
    }
}

function ExibirPopupDetallar(novoValor, viewModel) {
    if (novoValor) {
        viewModel.ExibirDetallar(false);
        viewModel.OcultarDetallar(false);
        showModal('dvDetalle');
    }
}
function OcultarPopupDetallar(novoValor, viewModel) {
    if (novoValor) {
        viewModel.OcultarDetallar(false);
        closeModal('dvDetalle');
    }
}

function ExibirPopupDatosGeneracionDocPases(novoValor, viewModel) {
    if (novoValor) {
        viewModel.ExibirDatosGeneracionDocPases(false);
        showModal('dvGenerarDocumentoPases');
    }
}

function OcultarPopupDatosGeneracionDocPases(novoValor, viewModel) {
    if (novoValor) {
        viewModel.OcultarDatosGeneracionDocPases(false);
        closeModal('dvGenerarDocumentoPases');
    }
}

function obtenerUserControl(UserControl_nombre, div_nombre) {

    if ($('#' + div_nombre).html() == '') {
        jQuery.ajax({
            url: 'PantallaAbono.aspx/obtenerUserControl',
            type: "POST",
            dataType: "json",
            data: "{name: '" + UserControl_nombre + "'}",
            contentType: "application/json; charset=utf-8",
            success: function (data, text) {
                var json_x = JSON.parse(data.d);
                if (json_x.CodigoError == "0" && json_x.Respuesta != null) {
                    $('#' + div_nombre).append(json_x.Respuesta);
                } else {
                    genesisAlertError(json_x.MensajeError, json_x.MensajeErrorDescriptiva);
                }
            },
            error: function (request, status, error) {
                genesisAlertError(error, request.responseText);
            }
        });
    }
}

function SetarFocoInicial(IdElemento) {
    $('#' + IdElemento).focus();
}