/// <reference path="jquery.min.js" />
/// <reference path="genesis.js" />
/// <reference path="ucCentralNotificacionesVM.js" />
var viewModel;
function InicializarCentralNotificacionesVM(dados) {
    if (dados.CodigoError != "0") {
        genesisAlertError(dados.MensajeError, dados.MensajeErrorDescriptiva);
        return;
    }

    viewModel = new CentralNotificacionesVM(dados.Respuesta);

    //#region Usabilidade
  /*  viewModel.MesageLoading.subscribe(function (novoValor) { genesisAlertLoading(novoValor); });
    viewModel.MesageAlerta.subscribe(function (novoValor) { ExibirMensageAlerta(novoValor, viewModel); });
    viewModel.MesageAlertaJS.subscribe(function (novoValor) { ExibirMensageAlertaJS(novoValor, viewModel); });
    viewModel.MesageConfirmacaoJS.subscribe(function (novoValor) { ExibirMensageConfirmacaoJS(novoValor, viewModel); });
    viewModel.MesageAlertaError.subscribe(function (novoValor) { ExibirMensageAlertaError(novoValor, viewModel); });
    crearModal('dvFiltro', 860, 410, function () { });
    viewModel.ExibirFiltros.subscribe(function (novoValor) { ExibirPopupFiltrar(novoValor, viewModel); });
    viewModel.OcultarFiltros.subscribe(function (novoValor) { OcultarPopupFiltrar(novoValor, viewModel); });
    crearModal('dvDetalle', 1220, 550, function () { });
    viewModel.ExibirDetallar.subscribe(function (novoValor) { ExibirPopupDetallar(novoValor, viewModel); });
    //Popup chadop do popup Detalle [refatorar]
    crearModal('dvCadastroDatosBancarios', 500, 320, function () { });

    crearModal('dvAlertRedirect', 500, 120, function () { });
    viewModel.AlertRedirectVM.subscribe(function (novoValor) { ExibirPopupAlertRedirect(novoValor); });
    viewModel.OcultarDetallar.subscribe(function (novoValor) { OcultarPopupDetallar(novoValor, viewModel); });*/
    //#endregion

    ko.applyBindings(viewModel, $('.ucCentralNotificaciones')[0]);

}
