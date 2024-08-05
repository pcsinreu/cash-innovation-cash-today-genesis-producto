/// <reference path="../jquery-1.10.1.min.js" />
/// <reference path="../knockout-3.4.0.js" />
/// <reference path="../knockout.mapping-latest.js" />
/// <reference path="Tipos.js" />
/// <reference path="PantallaAbonoVM.js" />

function CadastroDatosBancariosVM(vmDetalle) {
    var self = this;
    if (vmDetalle == null) return;

    //#region Atributos
    self.Context = vmDetalle

    self.NuevaCuenta = ko.observable();
    self.NuevaCuenta(new PeticionDatosBancarios(self.Context.Context.TemplateDatosBancarios));
    self.DatoBancarioObservable = self.NuevaCuenta().peticionDatosBancarios.DatosBancarios()[0];

    self.Bancos = ko.observableArray();
    self.TipoCuentaSelecionado = ko.observable();
    self.BusquedaBancos = new UtilitarioBusca(self.Bancos);

    self.CodigoDescripcionBanco = ko.computed(function () {
        if (self.Bancos().length > 0) {
            return self.Bancos()[0].Codigo() + " " + self.Bancos()[0].Descripcion();
        }
        return '';
    });
    self.PermiteSelecaoBanco = ko.computed(function () {
        return (self.Context.Abono.TipoAbono() == "Pedido");
    });
    //#endregion

    //#region Aciones
    //#region Metodos Aceptar Cuenta
    self.AceptarNuevaCuenta = function () {
        if (self.NuevaCuenta && self.NuevaCuenta().peticionDatosBancarios) {

            //Verifica se campos obrigatorios foram preenchidos.
            if (!self.ComprobarCamponsIllenos())
                return;

            self.ComprobarContasEstandarMismoBancoEAceptarDatosBancarios();
        }
    }

    self.AceptarNuevaCuentaInterno = function () {
            self.cargarDatosPeticion();
            var request = {
                peticionDatosBancarios: self.NuevaCuenta().peticionDatosBancarios
            }
            llamadaAjax('../../ServiciosInterface.asmx/GrabarNuevaConta', 'POST', ko.mapping.toJSON(request),
                self.AceptarNuevaCuentaExito, self.AceptarNuevaCuentaError);
        }

    self.AceptarNuevaCuentaExito = function (data) {

        respuesta = JSON.parse(data.d);

        if (respuesta.CodigoError == 0) {
            var datosBancarios = self.NuevaCuenta().peticionDatosBancarios.DatosBancarios()[0];
            var nuevaCuenta = {
                Identificador: datosBancarios.Identificador(),
                BolDefecto: datosBancarios.bolDefecto(),
                CodigoCuentaBancaria: datosBancarios.CodigoCuentaBancaria(),
                CodigoDocumento: datosBancarios.CodigoDocumento(),
                DescripcionTitularidad: datosBancarios.DescripcionTitularidad(),
                Observaciones: datosBancarios.DescripcionObs,
                CodigoTipoCuentaBancaria: self.TraduzirValorTipoCuenta(datosBancarios.CodigoTipoCuentaBancaria())
            }
            var DatoBancarioInfo = new DatoBancario(nuevaCuenta);

            var bancoExistente =  self.BuscarBancoExistente();

            if (bancoExistente != null) {
                self.Context.AbonoValorSelecionado().BancoCuenta(bancoExistente);
                self.Context.AbonoValorSelecionado().BancoCuenta().DatosBancarios.push(DatoBancarioInfo);
            }
            else {
                var novoBancoObservable = new BancoInformacion({
                    Identificador: self.Bancos()[0].Identificador,
                    Codigo: self.Bancos()[0].Codigo,
                    Descripcion: self.Bancos()[0].Descripcion,
                    DatosBancarios: []
                });
                novoBancoObservable.DatosBancarios.push(DatoBancarioInfo);
                self.Context.AbonoValorSelecionado().CuentasDisponibles.push(novoBancoObservable);
                self.Context.AbonoValorSelecionado().BancoCuenta(novoBancoObservable);
            }

            self.Context.AbonoValorSelecionado().Cuenta(DatoBancarioInfo);
            self.Context.AbonoValorSelecionado().Observaciones(DatoBancarioInfo.Observaciones());
        }
        else {
            alert(respuesta.MensajeError);
        }

        self.Ocultar();
    }

    self.TraduzirValorTipoCuenta = function (idTipoCuenta) {
        var description = null;
        self.NuevaCuenta().listaTipoCuenta().forEach(function (item) {
            if (item.Id() == idTipoCuenta) {
                description = item.Descripcion();
            }
        });

        return description;
    }

    self.AceptarNuevaCuentaError = function (request, status, error) {
        alert(error + ":" + request.Mesage);
    }

    self.cargarDatosPeticion = function () {
        var peticionDatosBancarios = self.NuevaCuenta().peticionDatosBancarios;
        //Dados cabecario peticion
        peticionDatosBancarios.CodigoUsuario(self.Context.Context.Usuario);
        
        if (self.Context.AbonoValorSelecionado().PtoServicio() != null) {
            peticionDatosBancarios.IdentificadorPuntoServicio(self.Context.AbonoValorSelecionado().PtoServicio().Identificador());
        }
        
        if (self.Context.AbonoValorSelecionado().SubCliente() != null) {
            peticionDatosBancarios.IdentificadorSubCliente(self.Context.AbonoValorSelecionado().SubCliente().Identificador());
        }

        peticionDatosBancarios.IdentificadorCliente(self.Context.AbonoValorSelecionado().Cliente().Identificador());
        
        //Datos item en lista de Datos Bancarios
        var nuevoDatosBancarios = self.NuevaCuenta().peticionDatosBancarios.DatosBancarios()[0];
        nuevoDatosBancarios.Identificador(GenerarId());
        nuevoDatosBancarios.CodigoTipoCuentaBancaria(self.TipoCuentaSelecionado().Id ? self.TipoCuentaSelecionado().Id() : 1);
        nuevoDatosBancarios.Cliente = self.Context.AbonoValorSelecionado().Cliente();
        nuevoDatosBancarios.SubCliente = self.Context.AbonoValorSelecionado().SubCliente();
        nuevoDatosBancarios.PuntoServicio = self.Context.AbonoValorSelecionado().PtoServicio();
        nuevoDatosBancarios.Divisa = self.Context.AbonoValorSelecionado().Divisa();
        nuevoDatosBancarios.Banco = self.Bancos()[0];
    }

    self.ComprobarCamponsIllenos = function(){

        if (!self.DatoBancarioObservable.CodigoCuentaBancaria() || !self.DatoBancarioObservable.DescripcionTitularidad()) {
            self.Context.Context.MesageAlertaJS(self.Context.Context.Diccionarios.msg_CamposCadastroDatosBancarios);
            return false;
        }

        return true;
    }

    self.ComprobarContasEstandarMismoBancoEAceptarDatosBancarios = function () {

        var datosBancarios = self.NuevaCuenta().peticionDatosBancarios.DatosBancarios()[0];
        if (datosBancarios.bolDefecto()) {
            var bancoExistente = self.BuscarBancoExistente();

            if (bancoExistente != null) {

                var cuentaEstandar = bancoExistente.DatosBancarios().filter(function (item) {
                    return item.BolDefecto();
                })[0];

                if (cuentaEstandar != null) {
                    
                    self.Context.Context.MesageConfirmacaoJS(self.Context.Context.Diccionarios.msg_CuentaEstandarYaExiste);

                    if (self.Context.Context.ResultadoConfirmacaoJS()) {

                        cuentaEstandar.BolDefecto(false);
                        llamadaAjax('../../ServiciosInterface.asmx/AlterarCuentaEstandar', 'POST', "{ identificador: '" + cuentaEstandar.Identificador() + "',  bolDefecto: '" + cuentaEstandar.BolDefecto() + "'}",
                                self.AlterarCuentaEstandarExito, self.AlterarCuentaEstandarError);
                    }
                    else {
                        datosBancarios.bolDefecto(false);

                        self.AceptarNuevaCuentaInterno();
                    }
                }
                else {
                    self.AceptarNuevaCuentaInterno();
                }
            }
            else {
                self.AceptarNuevaCuentaInterno();
            }
        } else {
            self.AceptarNuevaCuentaInterno();
        }
    }

    self.AlterarCuentaEstandarExito = function (data) {
        self.AceptarNuevaCuentaInterno();
    }

    self.AlterarCuentaEstandarError = function (request, status, error) {
        self.Context.Context.MesageAlertaJS(error + ":" + request.Mesage);
    }

    self.BuscarBancoExistente = function(){
        
        return self.Context.AbonoValorSelecionado().CuentasDisponibles().filter(function (banco) {
            return banco.Identificador() == self.Bancos()[0].Identificador();
        })[0];
    }

    //#endregion

    self.CancelarNuevaCuenta = function () {
        self.Ocultar();
    }

    self.Ocultar = function () {
        self.Context.OcultarCadastroDatosBancarios(true);
    }
    //#endregion

    if (self.Context.Abono.TipoAbono() != "Pedido") {
        self.Bancos(self.Context.Context.Abono().Bancos());
    }
};