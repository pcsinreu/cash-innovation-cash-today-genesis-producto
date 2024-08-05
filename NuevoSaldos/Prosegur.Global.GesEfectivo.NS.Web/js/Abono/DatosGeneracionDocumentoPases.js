/// <reference path="../jquery-1.10.1.min.js" />
/// <reference path="../knockout-3.4.0.js" />
/// <reference path="../knockout.mapping-latest.js" />
/// <reference path="Tipos.js" />
/// <reference path="PantallaAbonoVM.js" />
/// <reference path="PantallaAbono.js" />

function DatosGeneracionDocumentoPasesVM(viewModel) {
    self = this;

    if (!viewModel) return;

    //#region Atributos

    self.Context = viewModel;

    self.SetoresDocPases = ko.observableArray();
    self.CanalesDocPases = ko.observableArray();
    self.SubCanalesDocPases = ko.observableArray();
    
    self.BusquedaSetor = new UtilitarioBusca(self.SetoresDocPases);
    self.BusquedaCanal = new UtilitarioBusca(self.CanalesDocPases);
    self.BusquedaSubCanal = new UtilitarioBusca(self.SubCanalesDocPases);

    self.PreencherDatosEnAbonoSaldo = ko.observable(false);

    //#endregion

    //#region Acciones

    self.Cancelar = function () {
        self.Context.OcultarDatosGeneracionDocPases(true);
    }

    self.Aceptar = function () {

        if (!self.VerificarCamposlleno()) {
            self.Context.MesageAlertaJS(self.Context.Diccionarios.msg_CamposDocPasesObligatorio);
            return;
        }

        self.Context.OcultarDatosGeneracionDocPases(true);
        self.PreencherDatosEnAbonoSaldo(true);
    }

    self.VerificarCamposlleno = function () {
        return ((self.SetoresDocPases() && self.SetoresDocPases().length > 0) &&
            (self.CanalesDocPases() && self.CanalesDocPases().length > 0) &&
            (self.SubCanalesDocPases() && self.SubCanalesDocPases().length > 0));
    }

}