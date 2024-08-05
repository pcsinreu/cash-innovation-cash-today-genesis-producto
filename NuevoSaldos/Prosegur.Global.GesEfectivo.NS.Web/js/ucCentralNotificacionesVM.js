/// <reference path="jquery.min.js" />
/// <reference path="knockout.js" />
/// <reference path="knockout.mapping-latest.js" />
/// <reference path="Funcoes.js" />
/// <reference path="genesis.js" />
var msg_leida;
var Diccionarios;
var AplicacionGenesisSaldos;
var url = window.location.pathname;
var myPageName = url.substring(url.lastIndexOf('/') + 1);
var tmActuAutomatica;
var ImageFolderPath;
var TipoNotificacionDocumento;
var Usuario;
function CentralNotificacionesVM(dados) {
    var self = this;

    //#region Atributos
    msg_leida = dados.Diccionarios.msg_leida;
    Diccionarios = dados.Diccionarios;
    AplicacionGenesisSaldos = dados.AplicacionGenesisSaldos;
    ImageFolderPath = dados.ImageFolderPath;
    TipoNotificacionDocumento = dados.TipoNotificacionDocumento;
    Usuario = dados.Usuario;
    self.NotificacionesLeidas = ko.observableArray(MapearNotificaciones(dados.NotificacionesLeidas, true));
    self.NotificacionesNoLeidas = ko.observableArray(MapearNotificaciones(dados.NotificacionesNoLeidas, false));

    self.NotificacionesNoLeidasGrid = ko.observableArray(MapearNotificaciones(dados.NotificacionesNoLeidas, false, true));

    self.Diccionarios = dados.Diccionarios;
    self.Usuario = dados.Usuario;
    self.ActualizacionAutomatica = ko.observable(dados.ActualizacionAutomatica);
    self.PrivacidadeSelecionadaLeida = ko.observable();
    self.PrivacidadeSelecionadaNoLeida = ko.observable();
    self.OpcoesPrivacidade = dados.OpcoesPrivacidade;
    self.CalendarioDe = ko.observable(dados.CalendarioDe);
    self.CalendarioHasta = ko.observable(dados.CalendarioHasta);

    self.MarcarLeida = function () {
        var lstIdDestinoNotif = [];
        self.NotificacionesNoLeidasGrid().forEach(function (notificacion, i) {
            if (notificacion.Selecionado()) {                
                notificacion.DestinosNotificacion().forEach(function (destNotificacion, j) {
                    lstIdDestinoNotif.push(destNotificacion.Identificador());
                });
            }
        });
        if (lstIdDestinoNotif.length > 0) {
            MarcarLido(true, lstIdDestinoNotif, self.Usuario);
            $("#chkTodoLeida").prop('checked', false);
        }
    };
    self.MarcarNoLeida = function () {
        var lstIdDestinoNotif = [];
        self.NotificacionesLeidas().forEach(function (notificacion, i) {
            if (notificacion.Selecionado()) {
                notificacion.DestinosNotificacion().forEach(function (destNotificacion, j) {
                    lstIdDestinoNotif.push(destNotificacion.Identificador());
                });
            }
        });
        if (lstIdDestinoNotif.length > 0) {
            MarcarLido(false, lstIdDestinoNotif, self.Usuario);
            $("#chkTodoNoLeida").prop('checked', false);
        }
    };
    self.BuscarNoLeida = function () {
        self.NotificacionesNoLeidasGrid.removeAll();
        self.NotificacionesNoLeidasGrid(MapearNotificaciones(self.NotificacionesNoLeidas(), false, true));
    };
    self.BuscarLeida = function () {
        ActualizarNotificaciones();
    };
    
    self.CheckAllNoLeidaClick = function (objNotificacion) {        
        var checked = $("#chkTodoNoLeida").is(":checked");

        self.NotificacionesNoLeidasGrid().forEach(function (notificacion, i) {
            notificacion.Selecionado(checked);
        })

        return true;
    };

    self.CheckAllLeidaClick = function (objNotificacion) {
        var checked = $("#chkTodoLeida").is(":checked");

        self.NotificacionesLeidas().forEach(function (notificacion, i) {
            notificacion.Selecionado(checked);
        });

        return true;
    };
    //#endregion

    //ActualizacionAutomatica
    AtualizaTimerActualizacionAutomatica(self.ActualizacionAutomatica());

    self.ActualizacionAutomatica.subscribe(function () {
        AtualizaTimerActualizacionAutomatica(self.ActualizacionAutomatica());
        if (self.ActualizacionAutomatica() != "" && actualizacionAutomatica != null)
            GrabarActualizacionAutomatica(self.ActualizacionAutomatica());
    });
    

}

function GrabarActualizacionAutomatica(actualizacionAutomatica) {
    jQuery.ajax({
        url: myPageName + '/GrabarActualizacionAutomatica',
        contentType: "application/json; charset=utf-8",
        type: "POST",
        dataType: "json",
        data: "{actualizacionAutomatica: '" + actualizacionAutomatica + "'}",
        success: function (data, text) {
            
        },
        error: function (request, status, error) {
            alert(request.responseText);            
        }
    });
}

function AtualizaTimerActualizacionAutomatica(actualizacionAutomatica) {
    if (actualizacionAutomatica != "" && actualizacionAutomatica != null) {
        if (tmActuAutomatica != null)
            window.clearInterval(tmActuAutomatica);

        tmActuAutomatica = window.setInterval(ActualizarNotificaciones, parseInt(actualizacionAutomatica) * 1000);
    }
}
function MarcarLido(lido, lstIdDestNotificacion, usuario) {
    jQuery.ajax({
        url: myPageName + '/MarcarLeida',
        contentType: "application/json; charset=utf-8",
        type: "POST",
        dataType: "json",
        data: "{lido: " + lido + ", lstIdentificadoresDestino: " + ko.mapping.toJSON(lstIdDestNotificacion) + ", usuario: '" + usuario + "'}",
        success: function (data, text) {
            ActualizarNotificaciones();
        },
        error: function (request, status, error) {
            alert(request.responseText);/*
            self.MesageAlertaErrorDescriptiva(request.responseText);
            self.MesageAlertaError(error);*/
        }
    });
}
function Notificacion(notificacion, leida) {
    var self = this;

    self.Leida = leida;

    if (typeof (notificacion) == "string")
        ko.mapping.fromJSON(notificacion, {}, self);
    else
        ko.mapping.fromJS(notificacion, {}, self);

    self.ColumnaNotificacion = ko.computed(function () {
        var qtdNotifMax = 115;
        if (self.Leida)
            qtdNotifMax = 75;

        if (self.ObservacionNotificacion().length > qtdNotifMax)
            return self.ObservacionNotificacion().substring(0, qtdNotifMax - 5) + "...";
        else
            return self.ObservacionNotificacion();
    });

    self.ColumnaLeida = ko.computed(function () {
        if (self.Leida) {
            if (self.LidoPor != null) {
                if (self.Privada())
                    return self.LidoPor.FechaModificacionFormatada();
                else
                    return msg_leida.replace("{0}", self.LidoPor.UsuarioModificacion()).replace("{1}", self.LidoPor.FechaModificacionFormatada());
            }
        }
        return self.FechaModificacionFormatada;
    });

    self.ImgPrivado = ko.computed(function () {
        if (self.Privada())
            return ImageFolderPath + "/privado.png";
        else
            return ImageFolderPath + "/no_privado.png";
    });

    self.ImgAccion = ko.computed(function () {
        if (self.TipoNotificacion.CodigoAplicacion() != AplicacionGenesisSaldos)
            return ImageFolderPath + "/advertencia.png";
        else
            return ImageFolderPath + "/img_play.png";
    });

    self.ExecutarAcao = function (objNotificacion) {
        if (objNotificacion.TipoNotificacion.BolEventoRelacionado() 
            && objNotificacion.TipoNotificacion.CodigoAplicacion() == AplicacionGenesisSaldos
            && (objNotificacion.ObservacionParametros != "" || objNotificacion.ObservacionParametros != null)) {
            
            switch(objNotificacion.TipoNotificacion.CodigoTipoNotificacion()) {
                case TipoNotificacionDocumento:
                    if (!objNotificacion.Privada() && !objNotificacion.Lida()) {
                        ConfirmaExecutarAcao(objNotificacion);
                    } else {
                        ExecutaAcao(objNotificacion);
                    }                    
                    break;
                default:
                    alert(Diccionarios.msg_erro_accion1);
            }

            

        }        
    };
        
    self.Selecionado = ko.observable(false);
    self.SelecionadoClick = function (vm, leida) {
        var selTodos = true;
        if (leida) {
            vm.NotificacionesLeidas().forEach(function (notificacion, i) {
                if (!notificacion.Selecionado())
                    selTodos = false;
            });
            $("#chkTodoLeida").prop('checked', selTodos);
        } else {
            vm.NotificacionesNoLeidasGrid().forEach(function (notificacion, i) {
                if (!notificacion.Selecionado())
                    selTodos = false;
            });
            $("#chkTodoNoLeida").prop('checked', selTodos);
        }        
    };
}

function ConfirmaExecutarAcao(objNotificacion) {
    ExibirMensagemSimNaoNotificacion(Diccionarios.msg_confirma_accion, Diccionarios.aplicacao, Diccionarios.btnSim, Diccionarios.btnNao, objNotificacion);
}

function ExecutaAcao(objNotificacion) {
    var lstIdDestinoNotif = [];
    objNotificacion.DestinosNotificacion().forEach(function (destNotificacion, j) {
        lstIdDestinoNotif.push(destNotificacion.Identificador());
    });
    if (lstIdDestinoNotif.length > 0) {
        MarcarLido(true, lstIdDestinoNotif, Usuario);
    };
    AbrirPopupModalConsiderandoCaminhoPantalla('Documento.aspx?EsPopup=True&IdentificadorDocumento=' + objNotificacion.ObservacionParametros() + '&Modo=Consulta&NombrePopupModal=DetallarElemento', 600, 1000, true, "AcaoDocumento", true);
}

function ExibirMensagemSimNaoNotificacion(mensagem, titulo, btnSim, btnNao, objNotificacion) {
    var myButtons = {};
    myButtons[btnSim] = function () {
        $(this).dialog('close');
        ExecutaAcao(objNotificacion);
    };
    myButtons[btnNao] = function () {
        $(this).dialog('close');
    };
    ExibirMensagem(mensagem, titulo, "", null, myButtons);
}

function MapearNotificaciones(listaServidor, leida, filtrar) {
    var listaMapeada = [];
    $.each(listaServidor, function (i, a) {
        if (!leida && filtrar) {
            var privacidade = $("#ddlPrivadaNoLeida").prop('selectedIndex');
            var privada = false;
            if (privacidade > 0) {
                privada = (privacidade == 1);
                if (a.Privada == privada || a.Privada() == privada) {
                    listaMapeada.push(new Notificacion(a, leida));
                }
            } else {
                listaMapeada.push(new Notificacion(a, leida));
            }
        } else {
            listaMapeada.push(new Notificacion(a, leida));
        }
    });
    return listaMapeada;
}

function ActualizarNotificaciones() {
    var url = window.location.pathname;
    var myPageName = url.substring(url.lastIndexOf('/') + 1);
    $.ajax({
        type: "POST",
        async: false,
        url: myPageName + "/BuscarNotificaciones",
        data: "{ privacidadeLeida: " + $("#ddlPrivadaLeida").prop('selectedIndex') + ", desde: '" + $("#txtDesde").val() + "', hasta: '" + $("#txtHasta").val() + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var dataJSON = JSON.parse(data.d);
            viewModel.NotificacionesNoLeidas.removeAll();
            viewModel.NotificacionesNoLeidas(MapearNotificaciones(dataJSON.Respuesta.NotificacionesNoLeidas, false));

            viewModel.NotificacionesLeidas.removeAll();
            viewModel.NotificacionesLeidas(MapearNotificaciones(dataJSON.Respuesta.NotificacionesLeidas, true));
            
            viewModel.NotificacionesNoLeidasGrid.removeAll();
            viewModel.NotificacionesNoLeidasGrid(MapearNotificaciones(dataJSON.Respuesta.NotificacionesNoLeidas, false, true));
            
            /*
            $.each(dataJSON.Respuesta.NotificacionesNoLeidas, function (i, a) {
                viewModel.NotificacionesNoLeidas.push(new Notificacion(a, false));
            });
            */
        },
        error: function (e) {
            alert(request.responseText);
        }
    });
}