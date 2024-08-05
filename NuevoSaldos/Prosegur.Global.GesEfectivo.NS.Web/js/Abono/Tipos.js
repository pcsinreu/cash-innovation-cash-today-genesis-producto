/// <reference path="../jquery-1.10.1.min.js" />
/// <reference path="../knockout-3.4.0.js" />
/// <reference path="../knockout.mapping-latest.js" />

//#region Tipos servidor
function Abono(abono) {
    var self = this;

    var mapeamento = {
        'Bancos': {
            create: function (options) {
                return new AbonoInformacion(options.data);
            }
        },
        'AbonosValor': {
            create: function (options) {
                return new AbonoValor(options.data);
            }
        },
        'SnapshotsAbonoSaldo': {
            create: function (options) {
                return new AbonoValor(options.data);
            }
        }
    };

    if (typeof (abono) == "string")
        ko.mapping.fromJSON(abono, mapeamento, self);
    else
        ko.mapping.fromJS(abono, mapeamento, self);

    self.BuscaBancos = new UtilitarioBusca(self.Bancos);
}

function TerminoIAC(termino) {
    var self = this;
    //self.ValoresPosibles = ko.observableArray();

    if (typeof (termino) == "string")
        ko.mapping.fromJSON(termino, {}, self);
    else
        ko.mapping.fromJS(termino, {}, self);

    self.PermitirDatePicker = ko.computed(function () {
        return self.AceptarDigitacion() ? 'True' : 'False';
    });

    self.ValorDefecto = ko.computed(function () {

        if (!self.ValoresPosibles())
            return "";

        var valorDefecto = self.ValoresPosibles().filter(function (item) {
            return item.ValorDefecto() == true;
        });

        if (valorDefecto.length > 0) {
            self.Valor(valorDefecto[0].Descripcion());
        }

    });
}

function AbonoInformacion(inf) {
    var self = this;

    if (typeof (inf) == "string")
        ko.mapping.fromJSON(inf, {}, self);
    else
        ko.mapping.fromJS(inf, {}, self);

    self.CodigoDescripcion = ko.computed(function () {
        return self.Codigo() + " " + self.Descripcion();
    });
}

function AbonoValor(abonoValor) {
    var self = this;

    self.Novo = true;
    self.AbonoElemento = ko.observable();
    self.AbonoSaldo = ko.observable();
    self.Divisa = ko.observable();
    self.Cuenta = ko.observable();
    self.Cliente = ko.observable();
    self.SubCliente = ko.observable();
    self.PtoServicio = ko.observable();
    self.Importe = ko.observable(function () {
        console.log(self.Importe);
        return "0,00";
    });
    self.BancoCuenta = ko.observable();

    var mapeamento = {
        'AbonoElemento': {
            create: function (options) {
                return new AbonoElemento(options.data);
            }
        },
        'AbonoSaldo': {
            create: function (options) {
                return new AbonoSaldo(options.data, options.parent);
            }
        },
        'Divisa': {
            create: function (options) {
                return new DivisaAbono(options.data);
            }
        },
        'CuentasDisponibles': {
            create: function (options) {
                var banco = new BancoInformacion(options.data);
                //if (options.parent.BancoCuenta() != null && banco.Identificador() == options.parent.BancoCuenta().Identificador()) {
                //    options.parent.BancoCuenta(banco);

                //    if (options.parent.Cuenta() != null) {
                //        var cuenta = banco.DatosBancarios().filter(function (c) {
                //            return (c.Identificador() == options.parent.Cuenta().Identificador());
                //        });
                //        if (cuenta.length > 0) options.parent.Cuenta(cuenta[0]);
                //    }
                //}
                return banco;
            }
        },
        'BancoCuenta': {
            create: function (options) {
                return new BancoInformacion(options.data);
            }
        },
        'Cuenta': {
            create: function (options) {
                return new DatoBancario(options.data);
            }
        }
    };

    if (typeof (abonoValor) == "string")
        ko.mapping.fromJSON(abonoValor, mapeamento, self);
    else
        ko.mapping.fromJS(abonoValor, mapeamento, self);

    self.ImporteTotalEfectivo = ko.observable();
    self.ImporteTotalEfectivoDisponible = ko.observable();

    self.TiposMedioPagoTotales = ko.observableArray();
    self.DivisaImporte = ko.computed(function () {
        return (self.Divisa() != null) ? IncluirFormato(self.Divisa().CodigoISO(), self.Importe()) : "";
    });
    self.DivisaImporteTotalEfectivo = ko.computed(function () {
        return (self.Divisa() != null) ? IncluirFormato(self.Divisa().CodigoISO(), self.ImporteTotalEfectivo()) : "";
    });
    self.DivisaImporteTotalEfectivoDisponible = ko.computed(function () {
        return (self.Divisa() != null) ? IncluirFormato(self.Divisa().CodigoISO(), self.ImporteTotalEfectivoDisponible()) : "";
    });

    self.TipoMedioPagoTotalPeloTipo = function (tipoMedioPago) {
        var tpMdTot = self.TiposMedioPagoTotales().filter(function (tmp) {
            return ((tmp.CodigoISODivisa == self.Divisa().CodigoISO()) && (tmp.TipoMedioPago == tipoMedioPago));
        })[0];

        if (!tpMdTot) {
            tpMdTot = new TipoMedioPagoTotales(self.Divisa().CodigoISO(), self.Divisa().ColorHTML(), tipoMedioPago);
            self.TiposMedioPagoTotales.push(tpMdTot);
        }

        return tpMdTot;
    }

    self.InfoCliente = ko.computed(function () {
        return (((self.Cliente() && self.Cliente().Codigo() && self.Cliente().Descripcion()) ? self.Cliente().Codigo() + " " + self.Cliente().Descripcion() : "") + " " +
            ((self.SubCliente() && self.SubCliente().Codigo() && self.SubCliente().Descripcion()) ? self.SubCliente().Codigo() + " " + self.SubCliente().Descripcion() : "") + " " +
            ((self.PtoServicio() && self.PtoServicio().Codigo() && self.PtoServicio().Descripcion()) ? self.PtoServicio().Codigo() + " " + self.PtoServicio().Descripcion() : ""));
    });
}

function TipoMedioPagoTotales(codIsoDivisa, colorDivisa, tipoMPago) {
    var self = this;

    self.CodigoISODivisa = codIsoDivisa;
    self.ColorDivisa = colorDivisa;
    self.TipoMedioPago = tipoMPago;

    self.ImporteTotalMedioPagos = ko.observable(0);
    self.ImporteTotalMedioPagosDisponible = ko.observable(0);

    self.DivisaImporteTotalMedioPagos = ko.computed(function () {
        return (self.CodigoISODivisa) ? IncluirFormato(self.CodigoISODivisa, self.ImporteTotalMedioPagos()) : "";
    });
    self.DivisaImporteTotalMedioPagosDisponible = ko.computed(function () {
        return (self.CodigoISODivisa) ? IncluirFormato(self.CodigoISODivisa, self.ImporteTotalMedioPagosDisponible()) : "";
    });
}

function BancoInformacion(bancoInformation) {
    var self = this;

    var mapeamento = {
        'DatosBancarios': {
            create: function (options) {
                return new DatoBancario(options.data);
            },
            key: function (data) {
                return ko.utils.unwrapObservable(data.Identificador);
            }
        }
    };

    if (typeof (bancoInformation) == "string")
        ko.mapping.fromJSON(bancoInformation, mapeamento, self);
    else
        ko.mapping.fromJS(bancoInformation, mapeamento, self);

    self.InfoBanco = ko.computed(function () {
        return self.Codigo() && self.Descripcion() ? self.Codigo() + " - " + self.Descripcion() : "";
    });
}

function DatoBancario(dato) {
    var self = this;

    if (typeof (dato) == "string")
        ko.mapping.fromJSON(dato, {}, self);
    else
        ko.mapping.fromJS(dato, {}, self);

    self.InfoCuenta = ko.computed(function () {
        return self.CodigoCuentaBancaria() && self.CodigoTipoCuentaBancaria() ? self.CodigoCuentaBancaria() + " - " + self.CodigoTipoCuentaBancaria() : "";
    });
}

function AbonoElemento(abonoElemento) {
    var self = this;

    self.Divisa = ko.observable();

    var mapeamento = {
        'Divisa': {
            create: function (options) {
                return new DivisaAbono(options.data);
            }
        }
    };

    if (typeof (abonoElemento) == "string")
        ko.mapping.fromJSON(abonoElemento, mapeamento, self);
    else
        ko.mapping.fromJS(abonoElemento, mapeamento, self);

    self.DivisaImporte = ko.computed(function () {
        return (self.Divisa() != null) ? self.Divisa().CodigoISO() + " " + self.Importe() : "";
    });
}

function AbonoSaldo(abonoSaldo, abonoValor) {
    var self = this;

    self.Divisa = ko.observable();
    self.SubCanal = ko.observable();

    var mapeamento = {
        'Divisa': {
            create: function (options) {
                return new DivisaAbono(options.data);
            }
        },
        'SubCanal': {
            create: function (options) {
                return new AbonoInformacion(options.data);
            }
        },
        'SectoresDocumento': {
            create: function (options) {
                return new AbonoInformacion(options.data);
            }
        },
        'CanalesDocumento': {
            create: function (options) {
                return new AbonoInformacion(options.data);
            }
        },
        'SubCanalesDocumento': {
            create: function (options) {
                return new AbonoInformacion(options.data);
            }
        },
        'ListaSaldoCuenta': {
            create: function (options) {
                return new AbonoSaldoCuenta(options.data);
            }
        },
        'ListaTerminoIAC': {
            create: function (options) {
                return new TerminoIAC(options.data);
            }
        },
        'ListaSaldoCuenta': {
            create: function (options) {
                return new SnapshotSaldo(options.data);
            }
        }
    };

    if (typeof (abonoSaldo) == "string")
        ko.mapping.fromJSON(abonoSaldo, mapeamento, self);
    else
        ko.mapping.fromJS(abonoSaldo, mapeamento, self);

    self.DivisaImporte = ko.computed(function () {
        return (self.Divisa() != null) ? self.Divisa().CodigoISO() + " " + self.Importe() : "";
    });

    self.VisualizacionListaTerminos = ko.computed(function () {
        return ((self.ListaTerminoIAC() != null) && (self.ListaTerminoIAC().length > 0));
    });


    //self.BuscaSectoresDocumento = new UtilitarioBusca(self.SectoresDocumento);
    //self.BuscaCanalesDocumento = new UtilitarioBusca(self.CanalesDocumento);
    //self.BuscaSubCanalesDocumento = new UtilitarioBusca(self.SubCanalesDocumento);
}

function SnapshotSaldo(snap) {
    var self = this;

    var mapeamento = {
        'Divisa': {
            create: function (options) {
                return new DivisaAbono(options.data);
            }
        }
    };

    if (typeof (snap) == "string")
        ko.mapping.fromJSON(snap, mapeamento, self);
    else
        ko.mapping.fromJS(snap, mapeamento, self);
}

function AbonoSaldoCuenta(abonoSaldoCuenta) {
    var self = this;

    self.Divisa = ko.observable();

    var mapeamento = {
        'Divisa': {
            create: function (options) {
                return new DivisaAbono(options.data);
            }
        }
    };

    if (typeof (abonoSaldoCuenta) == "string")
        ko.mapping.fromJSON(abonoSaldoCuenta, mapeamento, self);
    else
        ko.mapping.fromJS(abonoSaldoCuenta, mapeamento, self);
}

function DivisaAbono(divisa) {
    var self = this;

    self.Totales = ko.observable();

    var mapeamento = {
        'ListaEfectivo': {
            create: function (options) {
                return new EfectivoAbono(options.data);
            }
        },
        'ListaMedioPago': {
            create: function (options) {
                return new MedioPagoAbono(options.data);
            }
        },
        'Totales': {
            create: function (options) {
                return new TotalesAbono(options.data);
            }
        }
    };

    if (typeof (divisa) == "string")
        ko.mapping.fromJSON(divisa, mapeamento, self);
    else
        ko.mapping.fromJS(divisa, mapeamento, self);
}

function EfectivoAbono(efec) {
    var self = this;

    self.EfectivoDisponible = new EfectivoDisponible();

    if (typeof (efec) == "string")
        ko.mapping.fromJSON(efec, {}, self);
    else
        ko.mapping.fromJS(efec, {}, self);
}

function TotalesAbono(tot) {
    var self = this;

    self.TotalesAbonoDisponible = new TotalesAbonoDisponible();

    if (typeof (tot) == "string")
        ko.mapping.fromJSON(tot, {}, self);
    else
        ko.mapping.fromJS(tot, {}, self);
}

function MedioPagoAbono(mdPago) {
    var self = this;

    self.DescripcionTipoMedioPago = ko.observable();
    self.MedioPagoDisponible = new MedioPagoDisponible();

    if (typeof (mdPago) == "string")
        ko.mapping.fromJSON(mdPago, {}, self);
    else
        ko.mapping.fromJS(mdPago, {}, self);
}

function Filtro(filtro) {
    var self = this;

    var mapeamento = {
        'Clientes': {
            create: function (options) {
                return new AbonoInformacion(options.data);
            }
        },
        'SubClientes': {
            create: function (options) {
                return new AbonoInformacion(options.data);
            }
        },
        'PuntosServicio': {
            create: function (options) {
                return new AbonoInformacion(options.data);
            }
        },
        'Sectores': {
            create: function (options) {
                return new AbonoInformacion(options.data);
            }
        },
        'Canales': {
            create: function (options) {
                return new AbonoInformacion(options.data);
            }
        },
        'SubCanales': {
            create: function (options) {
                return new AbonoInformacion(options.data);
            }
        }
    };

    if (typeof (filtro) == "string")
        ko.mapping.fromJSON(filtro, mapeamento, self);
    else
        ko.mapping.fromJS(filtro, mapeamento, self);

    self.Resumo = ko.computed(function () {
        var resumoFiltro = '';
        if (self.Sectores().length > 0) {
            for (var i = 0; i < self.Sectores().length; i++) {
                resumoFiltro += self.Sectores()[i].Descripcion();
                if (resumoFiltro.length >= 80) {
                    resumoFiltro += ' [...]'
                    break;
                }
                if (i != self.Sectores().length - 1) resumoFiltro += ', ';
            }
        }
        return resumoFiltro;
    });

    self.ResumoCompleto = ko.computed(function () {
        var resumoFiltro = '';
        if (self.Sectores().length > 0) {
            for (var i = 0; i < self.Sectores().length; i++) {
                resumoFiltro += self.Sectores()[i].Descripcion();
                if (i != self.Sectores().length - 1) resumoFiltro += ', ';
            }
        }
        return resumoFiltro;
    });

    self.BuscaClientes = new UtilitarioBusca(self.Clientes);
    self.BuscaSubClientes = new UtilitarioBusca(self.SubClientes);
    self.BuscaPuntosServicio = new UtilitarioBusca(self.PuntosServicio);
    self.BuscaSectores = new UtilitarioBusca(self.Sectores);
    self.BuscaCanales = new UtilitarioBusca(self.Canales);
    self.BuscaSubCanales = new UtilitarioBusca(self.SubCanales);
}

function PeticionDatosBancarios(templateDatosBancarios) {
    var self = this;

    if (typeof (templateDatosBancarios) == "string")
        ko.mapping.fromJSON(templateDatosBancarios, {}, self);
    else
        ko.mapping.fromJS(templateDatosBancarios, {}, self);
}
//#endregion

//#region Utilidad pantalla
//Utilitário para "Estratégia" de AbonoValor do tipo Elemento ou Saldos
function AbonoValorElementoOSaldos(tipoAbono) {
    var self = this;

    self.TipoAbono = tipoAbono;

    //Filtra AbonoValor pelo TipoAbono
    self.Filtrar = function (aValor1, aValor2, aValor2IsJSON) {
        if (self.TipoAbono == "Elemento") {
            if (aValor2IsJSON) {
                return (aValor1.AbonoElemento().CodigoElemento() == aValor2.AbonoElemento.CodigoElemento);
            }
            return (aValor1.AbonoElemento().CodigoElemento() == aValor2.AbonoElemento().CodigoElemento());
        } else {
            if (aValor2IsJSON) {
                return (aValor1.AbonoSaldo().IdentificadorSnapshot() == aValor2.AbonoSaldo.IdentificadorSnapshot);
            }
            return (aValor1.AbonoSaldo().IdentificadorSnapshot() == aValor2.AbonoSaldo().IdentificadorSnapshot());
        }
    }

    //Filtra AbonoValor pela Divisa pelo TipoAbono
    self.FiltrarPorDivisa = function (aValor1, aValor2, aValor2IsJSON) {
        if (self.Filtrar(aValor1, aValor2, aValor2IsJSON)) {
            if (self.TipoAbono == "Elemento") {
                if (aValor2IsJSON) {
                    return (aValor1.AbonoElemento().Divisa().CodigoISO() == aValor2.AbonoElemento.Divisa.CodigoISO);
                }
                return (aValor1.AbonoElemento().Divisa().CodigoISO() == aValor2.AbonoElemento().Divisa().CodigoISO());
            } else {
                if (aValor2IsJSON) {
                    return (aValor1.AbonoSaldo().Divisa().Identificador() == aValor2.AbonoSaldo.Divisa.Identificador);
                }
                return (aValor1.AbonoSaldo().Divisa().Identificador() == aValor2.AbonoSaldo().Divisa().Identificador());
            }
        }
        return false;
    }

    //Retonar lista de efectivos pelo TipoAbono
    self.ListaEfectivo = function (aValor) {
        if (self.TipoAbono == "Elemento") {
            return aValor.AbonoElemento().Divisa().ListaEfectivo();
        } else {
            return aValor.AbonoSaldo().Divisa().ListaEfectivo();
        }
    }

    //Retonar lista de medio pagos pelo TipoAbono
    self.ListaMedioPago = function (aValor) {
        if (self.TipoAbono == "Elemento") {
            return aValor.AbonoElemento().Divisa().ListaMedioPago();
        } else {
            return aValor.AbonoSaldo().Divisa().ListaMedioPago();
        }
    }

    //Retonar Totales pelo TipoAbono
    self.Totales = function (aValor) {
        if (self.TipoAbono == "Elemento") {
            return aValor.AbonoElemento().Divisa().Totales();
        } else {
            return aValor.AbonoSaldo().Divisa().Totales();
        }
    }

    //Retorna CodigoISO da divisa do AbonoValor
    self.CodigoISO = function (aValor) {
        if (self.TipoAbono == "Elemento") {
            return aValor.AbonoElemento().Divisa().CodigoISO();
        } else {
            return aValor.AbonoSaldo().Divisa().CodigoISO();
        }
    }
}

function AbonoBkp(tipoAbono, tipoValor, jsonBanco, crearDocumentoPases) {
    var self = this;
    self.TipoAbono = tipoAbono;
    self.TipoValor = tipoValor;
    self.jsonBanco = jsonBanco;
    self.CrearDocumentoPases = crearDocumentoPases;
}

//#region Auxiliares para Ordenação de listas de AbonoValor
function CabecalhoAbonoValor(clave) {
    var self = this;
    self.Clave = clave;
    self.SortOrder = ko.observable(0);
}

function OrdenadorListaAbonoValor(listaAbonoValor) {
    var self = this;
    self.ListaAbonoValorOrdenar = listaAbonoValor;
    self.ColunaNumExterno = new CabecalhoAbonoValor('nExt');
    self.ColunaSubCanal = new CabecalhoAbonoValor('sCanal');
    self.ColunaCliente = new CabecalhoAbonoValor('cliente');
    self.ColunaValor = new CabecalhoAbonoValor('valorE');
    self.ColunaValor = new CabecalhoAbonoValor('valorS');
    self.ColunaCuenta = new CabecalhoAbonoValor('cuenta');
    self.ColunaBanco = new CabecalhoAbonoValor('banco');

    self.Ordenar = function (cabecalho) {
        switch (cabecalho.Clave) {
            case 'nExt':
                if (cabecalho.SortOrder() == 1) {
                    self.ListaAbonoValorOrdenar.sort(function (a, b) { return a.AbonoElemento().CodigoElemento() < b.AbonoElemento().CodigoElemento() ? -1 : a.AbonoElemento().CodigoElemento() > b.AbonoElemento().CodigoElemento() ? 1 : 0; });
                    cabecalho.SortOrder(0);
                } else {
                    self.ListaAbonoValorOrdenar.sort(function (a, b) { return a.AbonoElemento().CodigoElemento() > b.AbonoElemento().CodigoElemento() ? -1 : a.AbonoElemento().CodigoElemento() < b.AbonoElemento().CodigoElemento() ? 1 : 0; });
                    cabecalho.SortOrder(1);
                }
                break;
            case 'sCanal':
                if (cabecalho.SortOrder() == 1) {
                    self.ListaAbonoValorOrdenar.sort(function (a, b) { return a.AbonoSaldo().SubCanal().Descripcion() < b.AbonoSaldo().CodigoElemento() ? -1 : a.AbonoSaldo().SubCanal().Descripcion() > b.AbonoSaldo().SubCanal().Descripcion() ? 1 : 0; });
                    cabecalho.SortOrder(0);
                } else {
                    self.ListaAbonoValorOrdenar.sort(function (a, b) { return a.AbonoSaldo().SubCanal().Descripcion() > b.AbonoSaldo().SubCanal().Descripcion() ? -1 : a.AbonoSaldo().SubCanal().Descripcion() < b.AbonoSaldo().SubCanal().Descripcion() ? 1 : 0; });
                    cabecalho.SortOrder(1);
                }
                break;
            case 'cliente':
                if (cabecalho.SortOrder() == 1) {
                    self.ListaAbonoValorOrdenar.sort(function (a, b) { return a.InfoCliente() < b.InfoCliente() ? -1 : a.InfoCliente() > b.InfoCliente() ? 1 : 0; });
                    cabecalho.SortOrder(0);
                } else {
                    self.ListaAbonoValorOrdenar.sort(function (a, b) { return a.InfoCliente() > b.InfoCliente() ? -1 : a.InfoCliente() < b.InfoCliente() ? 1 : 0; });
                    cabecalho.SortOrder(1);
                }
                break;
            case 'valorE':
                if (cabecalho.SortOrder() == 1) {
                    self.ListaAbonoValorOrdenar.sort(function (a, b) { return a.AbonoElemento().DivisaImporte() < b.AbonoElemento().DivisaImporte() ? -1 : a.AbonoElemento().DivisaImporte() > b.AbonoElemento().DivisaImporte() ? 1 : 0; });
                    cabecalho.SortOrder(0);
                } else {
                    self.ListaAbonoValorOrdenar.sort(function (a, b) { return a.AbonoElemento().DivisaImporte() > b.AbonoElemento().DivisaImporte() ? -1 : a.AbonoElemento().DivisaImporte() < b.AbonoElemento().DivisaImporte() ? 1 : 0; });
                    cabecalho.SortOrder(1);
                }
                break;
            case 'valorS':
                if (cabecalho.SortOrder() == 1) {
                    self.ListaAbonoValorOrdenar.sort(function (a, b) { return a.AbonoSaldo().DivisaImporte() < b.AbonoSaldo().DivisaImporte() ? -1 : a.AbonoSaldo().DivisaImporte() > b.AbonoSaldo().DivisaImporte() ? 1 : 0; });
                    cabecalho.SortOrder(0);
                } else {
                    self.ListaAbonoValorOrdenar.sort(function (a, b) { return a.AbonoSaldo().DivisaImporte() > b.AbonoSaldo().DivisaImporte() ? -1 : a.AbonoSaldo().DivisaImporte() < b.AbonoSaldo().DivisaImporte() ? 1 : 0; });
                    cabecalho.SortOrder(1);
                }
                break;
            case 'cuenta':
                if (cabecalho.SortOrder() == 1) {
                    self.ListaAbonoValorOrdenar.sort(function (a, b) { return a.Cuenta().CodigoCuentaBancaria() < b.Cuenta().CodigoCuentaBancaria() ? -1 : a.Cuenta().CodigoCuentaBancaria() > b.Cuenta().CodigoCuentaBancaria() ? 1 : 0; });
                    cabecalho.SortOrder(0);
                } else {
                    self.ListaAbonoValorOrdenar.sort(function (a, b) { return a.Cuenta().CodigoCuentaBancaria() > b.Cuenta().CodigoCuentaBancaria() ? -1 : a.Cuenta().CodigoCuentaBancaria() < b.Cuenta().CodigoCuentaBancaria() ? 1 : 0; });
                    cabecalho.SortOrder(1);
                }
            case 'banco':
                if (cabecalho.SortOrder() == 1) {
                    self.ListaAbonoValorOrdenar.sort(function (a, b) { return a.BancoCuenta().Codigo() < b.BancoCuenta().Codigo() ? -1 : a.BancoCuenta().Codigo() > b.BancoCuenta().Codigo() ? 1 : 0; });
                    cabecalho.SortOrder(0);
                } else {
                    self.ListaAbonoValorOrdenar.sort(function (a, b) { return a.BancoCuenta().Codigo() > b.BancoCuenta().Codigo() ? -1 : a.BancoCuenta().Codigo() < b.BancoCuenta().Codigo() ? 1 : 0; });
                    cabecalho.SortOrder(1);
                }
                break;
        }
    };
}
//#endregion

//#region Utilitario para comunicação entre a busca [cliente/subclientes/puntosservicos] e knockout
function UtilitarioBusca(propriedad) {
    var self = this;
    self.Propriedad = propriedad;
    self.jsonString = propriedad() ? ko.observable(ko.mapping.toJSON(propriedad)) : ko.observable();

    self.Titulo = ko.observable();

    self.jsonString.subscribe(function (novoValor) {
        if (self.Propriedad()) { self.Propriedad.removeAll(); }
        if (novoValor) {

            jsonArray = []
            if (typeof (novoValor) == "string")
                var jsonArray = JSON.parse(novoValor);
            else
                jsonArray.push(novoValor);

            for (var i = 0; i < jsonArray.length; i++) {
                self.Propriedad.push(new AbonoInformacion(jsonArray[i]));
            }
        }
        
    });
    self.Codigo = ko.computed(function () {
        return (self.Propriedad() && self.Propriedad().length == 1) ? self.Propriedad()[0].Codigo() : '';
    });

    self.Descripcion = ko.computed(function () {
        return (self.Propriedad() && self.Propriedad().length == 1) ? self.Propriedad()[0].Descripcion() : '';
    });

    self.VisualizacionValoresMulti = ko.computed(function () {
        return (self.Propriedad().length > 1);
    });

    self.InfoBancos = ko.computed(function () {
        return (self.Codigo() && self.Descripcion()) ? self.Codigo() + " - " + self.Descripcion() : '';
    });
}
//#endregion

//#region Auxiliares para cálculo das quantidades/valores disponiveis
function EfectivoDisponible(codDenominacion, valor) {
    var self = this;
    self.CodigoDenominacion = codDenominacion;
    self.Valor = valor;
    self.CantidadDisponible = ko.observable(0);
    self.ValorDisponible = ko.computed(function () {
        return self.Valor > 0 ? (self.CantidadDisponible() * self.Valor) : 0;
    });
}

function MedioPagoDisponible(cod, tipo) {
    var self = this;
    self.Codigo = cod;
    self.TipoMedioPago = tipo;
    self.CantidadDisponible = ko.observable(0);
    self.ValorDisponible = ko.observable(0);
}

function TotalesAbonoDisponible() {
    var self = this;
    self.TotalChequeDisponible = ko.observable(0);
    self.TotalEfectivoDisponible = ko.observable(0);
    self.TotalOtroValorDisponible = ko.observable(0);
    self.TotalTarjetaDisponible = ko.observable(0);
    self.TotalTicketDisponible = ko.observable(0);
}

function DivisaAbonoPantalla(cod, desc, color) {
    var self = this;
    self.CodigoISO = cod;
    self.Descripcion = desc;
    self.Color = color;

    self.ListaEfectivosDisponibles = [];
    self.ListaMedioPagosDisponibles = [];
    self.TotalesDisponibles = null;

    //Retorna as Divisas com Valores disponiveis
    self.PossuiValorDisponible = function () {
        var efecsDisp = self.ListaEfectivosDisponibles.filter(function (e) {
            return (e.CantidadDisponible() != 0);
        });
        if (efecsDisp.length > 0) return true;

        var mpsDisp = self.ListaMedioPagosDisponibles.filter(function (e) {
            return ((e.CantidadDisponible() != 0) || (e.ValorDisponible() != 0));
        });
        if (mpsDisp.length > 0) return true;

        return ((self.TotalesDisponibles.TotalChequeDisponible() != 0) ||
            (self.TotalesDisponibles.TotalEfectivoDisponible() != 0) ||
            (self.TotalesDisponibles.TotalOtroValorDisponible() != 0) ||
            (self.TotalesDisponibles.TotalTarjetaDisponible() != 0) ||
            (self.TotalesDisponibles.TotalTicketDisponible() != 0));
    };
}

function ValoresDisponibles() {
    var self = this;

    self.ListaDivisas = ko.observableArray();
    self.DivisaPeloCodigo = function (codigoISO) {
        return self.ListaDivisas().filter(function (d) {
            return d.CodigoISO == codigoISO;
        })[0];
    };

    //Retorna o objeto singleton por Divisa/Denominação para Efectivos disponíveis
    self.RetornaListaEfectivoDisponible = function (divisaAbono, efec) {
        var divisa = self.DivisaPeloCodigo(divisaAbono.CodigoISO());
        if (!divisa) {
            divisa = new DivisaAbonoPantalla(divisaAbono.CodigoISO(), divisaAbono.Descripcion(), divisaAbono.ColorHTML())
            self.ListaDivisas.push(divisa);
        }

        var efecsDisp = divisa.ListaEfectivosDisponibles.filter(function (e) {
            return (e.CodigoDenominacion == efec.Codigo());
        });
        var efecAbono = divisaAbono.ListaEfectivo().filter(function (e) { return (e.Codigo() == efec.Codigo()); });

        var efecDisp = null;
        if (efecsDisp.length > 0) {
            efecDisp = efecsDisp[0];
            efecDisp.CantidadDisponible(efecDisp.CantidadDisponible() - efecAbono[0].Cantidad());
        } else {
            efecDisp = new EfectivoDisponible(efec.Codigo(), efec.Valor())
            efecDisp.CantidadDisponible(efec.Cantidad() - efecAbono[0].Cantidad());
            divisa.ListaEfectivosDisponibles.push(efecDisp);
        }

        return efecDisp;        
    };

    //Retorna o objeto singleton por Divisa/Denominação para Medios Pagos disponíveis
    self.RetornaListaMedioPagoDisponible = function (divisaAbono, mp) {
        var divisa = self.DivisaPeloCodigo(divisaAbono.CodigoISO());
        if (!divisa) {
            divisa = new DivisaAbonoPantalla(divisaAbono.CodigoISO(), divisaAbono.Descripcion(), divisaAbono.ColorHTML())
            self.ListaDivisas.push(divisa);
        }

        var mpsDisp = divisa.ListaMedioPagosDisponibles.filter(function (e) {
            return (e.Codigo == mp.Codigo() && e.TipoMedioPago == mp.TipoMedioPago());
        });
        var mpAbono = divisaAbono.ListaMedioPago().filter(function (e) { return (e.Codigo() == mp.Codigo() && e.TipoMedioPago() == mp.TipoMedioPago()); });

        var mpDisp = null;
        if (mpsDisp.length > 0) {
            mpDisp = mpsDisp[0];
            mpDisp.CantidadDisponible(mpDisp.CantidadDisponible() - mpAbono[0].Cantidad());
            mpDisp.ValorDisponible(mpDisp.ValorDisponible() - mpAbono[0].Importe());
        } else {
            mpDisp = new MedioPagoDisponible(mp.Codigo(), mp.TipoMedioPago())
            mpDisp.CantidadDisponible(mp.Cantidad() - mpAbono[0].Cantidad());
            mpDisp.ValorDisponible(mp.Importe() - mpAbono[0].Importe());
            divisa.ListaMedioPagosDisponibles.push(mpDisp);
        }
                
        return mpDisp;
    };

    //Retorna o objeto singleton por Divisa para valores Totales disponíveis
    self.RetornaTotalesDisponible = function (divisaAbono, tots) {
        var divisa = self.DivisaPeloCodigo(divisaAbono.CodigoISO());
        if (!divisa) {
            divisa = new DivisaAbonoPantalla(divisaAbono.CodigoISO(), divisaAbono.Descripcion(), divisaAbono.ColorHTML())
            self.ListaDivisas.push(divisa);
        }

        if (divisa.TotalesDisponibles == null) {
            var totDisp = new TotalesAbonoDisponible();
            totDisp.TotalChequeDisponible(SubtrairValoresTolerenacia2(tots.TotalCheque(), divisaAbono.Totales().TotalCheque()));
            totDisp.TotalEfectivoDisponible(SubtrairValoresTolerenacia2(tots.TotalEfectivo(), divisaAbono.Totales().TotalEfectivo()));
            totDisp.TotalOtroValorDisponible(SubtrairValoresTolerenacia2(tots.TotalOtroValor(), divisaAbono.Totales().TotalOtroValor()));
            totDisp.TotalTarjetaDisponible(SubtrairValoresTolerenacia2(tots.TotalTarjeta(), divisaAbono.Totales().TotalTarjeta()));
            totDisp.TotalTicketDisponible(SubtrairValoresTolerenacia2(tots.TotalTicket(), divisaAbono.Totales().TotalTicket()));
            divisa.TotalesDisponibles = totDisp;
        } else {
            divisa.TotalesDisponibles.TotalChequeDisponible(SubtrairValoresTolerenacia2(divisa.TotalesDisponibles.TotalChequeDisponible(), divisaAbono.Totales().TotalCheque()));
            divisa.TotalesDisponibles.TotalEfectivoDisponible(SubtrairValoresTolerenacia2(divisa.TotalesDisponibles.TotalEfectivoDisponible(), divisaAbono.Totales().TotalEfectivo()));
            divisa.TotalesDisponibles.TotalOtroValorDisponible(SubtrairValoresTolerenacia2(divisa.TotalesDisponibles.TotalOtroValorDisponible(), divisaAbono.Totales().TotalOtroValor()));
            divisa.TotalesDisponibles.TotalTarjetaDisponible(SubtrairValoresTolerenacia2(divisa.TotalesDisponibles.TotalTarjetaDisponible(), divisaAbono.Totales().TotalTarjeta()));
            divisa.TotalesDisponibles.TotalTicketDisponible(SubtrairValoresTolerenacia2(divisa.TotalesDisponibles.TotalTicketDisponible(), divisaAbono.Totales().TotalTicket()));
        }

        return divisa.TotalesDisponibles;
    };

    //Retorna as Divisas com Valores disponiveis
    self.DivisasConValoresDisponibles = function () {
        return self.ListaDivisas().filter(function (d) {
            return d.PossuiValorDisponible();
        });
    };
}

function AlertRedirect(msg, url) {
    var self = this;

    self.Mensaje = msg;
    self.Location = url;
}
//#endregion
//#endregion

//#region Utilitarios KO
ko.bindingHandlers.numeric = {
    init: function (element, valueAccessor) {
        $(element).on("keydown", function (event) {
            InputNumericLogicCore(event, true);
        });
    }
};

ko.bindingHandlers.integer = {
    init: function (element, valueAccessor) {
        $(element).on("keydown", function (event) {
            InputNumericLogicCore(event, false);
        });
    }
}

ko.bindingHandlers.datePicker = {
    init: function (element, valueAccessor) {

        element.id = element.id + "_" + GenerarId();

        if (valueAccessor()[0] == 'True') {
            AbrirCalendario(element.id, valueAccessor()[1]);
        }
    }
}

ko.bindingHandlers.readOnly = {
    init: function (element, valueAccessor) {
        element.readOnly = valueAccessor();
    }
}

ko.bindingHandlers.disabled = {
    init: function (element, valueAccessor) {
            element.disabled = valueAccessor();
    }
}

ko.bindingHandlers.backGroundColor = {
    init: function (element, valueAccessor) {
        element.style.backgroundColor = valueAccessor();
    }
}

ko.bindingHandlers.styleBorderThin = {
    init: function (element, valueAccessor) {
        if (valueAccessor() == true) {
            element.style.border = 'solid';
            element.style.borderWidth = "thin";
        }
    }
}

ko.bindingHandlers.maxLength = {
    init: function (element, valueAccessor) {
        $(element).on("keydown", function (event) {
            //Permite BackEspace, Delete, Enter, Esc e setas de movimento
            if ($(element).val().length >= valueAccessor() && (event.keyCode != 46 && event.keyCode != 8 && event.keyCode != 9 && event.keyCode != 27 && event.keyCode != 13) && (event.keyCode < 37 || event.keyCode > 40))
                event.preventDefault();
        });
    }
};

var formatNumber = function (element, valueAccessor, allBindingsAccessor) {
    // Provide a custom text value
    var value = valueAccessor(), allBindings = allBindingsAccessor();
    var strNumber = ko.utils.unwrapObservable(value);
    if (strNumber || strNumber == 0) {
        return IncluirFormatoDecimal(strNumber);
    }
    return '';
};

ko.bindingHandlers.decimalMask = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        $(element).text(formatNumber(element, valueAccessor, allBindingsAccessor));
        $(element).val(formatNumber(element, valueAccessor, allBindingsAccessor));
    },
    update: function (element, valueAccessor, allBindingsAccessor) {
        $(element).text(formatNumber(element, valueAccessor, allBindingsAccessor));
        $(element).val(formatNumber(element, valueAccessor, allBindingsAccessor));
    }
};

function InputNumericLogicCore(event, allowCommaAndPoint)
{
    // Allow: backspace, delete, tab, escape, and enter
    if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
        // Allow: Ctrl+A
        (event.keyCode == 65 && event.ctrlKey === true) ||
        // Allow: . , -
        (allowCommaAndPoint && (event.keyCode == 109 || event.keyCode == 188 || event.keyCode == 189 || event.keyCode == 190 || event.keyCode == 110)) ||
        // Allow: home, end, left, right
        (event.keyCode >= 35 && event.keyCode <= 39)) {
        // let it happen, don't do anything
        return;
    }
    else {
        // Ensure that it is a number and stop the keypress
        if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
            event.preventDefault();
        }
    }
}

//#endregion
