<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucCentralNotificaciones.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucCentralNotificaciones" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/js/ucCentralNotificacionesVM.js" />
        <asp:ScriptReference Path="~/js/ucCentralNotificaciones.js" />
    </Scripts>
</asp:ScriptManagerProxy>
<script>
    var index = 'qpsstats-active-tab';
    //  Define friendly data store name
    var dataStore = window.sessionStorage;
    var oldIndex = 0;

    function atribuiEventos() {

        $("#dvIconNotif").unbind("click");
        $("#dvIconNotif").click(function () {
            $("#<%= dvNotificaciones.ClientID %> ").toggle();
        });

        //  Start magic!
        try {
            // getter: Fetch previous value
            oldIndex = dataStore.getItem(index);
        } catch (e) { }

        $('#tbNotificacion').tabs({
            active: oldIndex,
            activate: function (event, ui) {
                //  Get future value
                var newIndex = ui.newTab.parent().children().index(ui.newTab);
                //  Set future value
                try {
                    dataStore.setItem(index, newIndex);
                } catch (e) { }
            }
        });
        AbrirCalendario('txtDesde', 'true');
        AbrirCalendario('txtHasta', 'true');
    };

    $(document).ready(function () {
        atribuiEventos();
    });

    $(document).mouseup(function (e) {
        var container = $("#<%= dvNotificaciones.ClientID %>");
        var calendario = $("#ui-datepicker-div");
        var dialog = $(".ui-dialog");
        var dvIconNotif = $("#dvIconNotif");

        if (dvIconNotif.has(e.target).length === 0) {
            if ((!container.is(e.target) // if the target of the click isn't the container...
                && container.has(e.target).length === 0) &&
                (!calendario.is(e.target) // if the target of the click isn't the container...
                && calendario.has(e.target).length === 0) &&
                (!dialog.is(e.target) // if the target of the click isn't the container...
                && dialog.has(e.target).length === 0)) // ... nor a descendant of the container
            {
                container.hide();
            }
        }
    });

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_initializeRequest(InitializeRequest);

    var estadoDiv;

    function InitializeRequest(sender, args) {
        estadoDiv = $("#<%= dvNotificaciones.ClientID %> ").is(":hidden")
    }

    prm.add_endRequest(OnEndRequest);
    function OnEndRequest(sender, args) {
        atribuiEventos();
        if (!estadoDiv) {
            $("#<%= dvNotificaciones.ClientID %> ").show();
        };
    }
</script>
<style>
    .dvQtdNotif {
        border-radius: 25px;
        width: 15px;
        height: 15px;
        background-color: red;
        color: white;
        font-weight: bold;
        text-align: center;
        line-height: 18px;
        position: absolute;
        top: 0px;
        right: 0px;
    }

    .tab_content {
        border: 1px;
        height: 144px;
    }

    .triangle-isosceles {
        background-color: white;
        height: 15px;
        width:  15px;
        position: absolute;
        top: -9px; left: 104px;
    transform:             rotate( 45deg );
        -moz-transform:    rotate( 45deg );
        -ms-transform:     rotate( 45deg );
        -o-transform:      rotate( 45deg );
        -webkit-transform: rotate( 45deg );
        border-left: 1px solid #ccc;
        border-top: 1px solid #ccc;
        }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
    <ContentTemplate>        
        <div id="dvNotif" runat="server" style="display: block; position: absolute; top: 0px; left: 0px;">            
            <div id="dvIconNotif" style="width: 31px; height: 25px; cursor: pointer;" >
                <img src="<%=Page.ResolveUrl("~/App_Themes/Padrao/css/img/central.png")%>" alt="" style="position: absolute; bottom: 0px; left: 0px;" />
                <div id="dvQtdNotif" class="dvQtdNotif" data-bind="visible: NotificacionesNoLeidas().length > 0" style="display:none;">                    
                    <span data-bind="text: NotificacionesNoLeidas().length"></span>
                </div>
            </div>
            <div id="dvNotificaciones" style="display: none; width: 847px; height: 192px; position: absolute; left: -107px; top: 42px; background-color: white; border: 1px solid #ccc !important; border-radius: 10px; z-index: 1000;" runat="server">
                <div class="triangle-isosceles"></div>
                <div style="position: absolute; right: 17px; top: 13px; z-index: 1; font-size: 12px;">
                    <asp:Label ID="lblActualizacionAut" runat="server"></asp:Label>                    
                    <input type="text" id="txtTiempoAct" data-bind='value: ActualizacionAutomatica' maxlength="3" style="width:30px;" onkeypress="return bloqueialetras(event,this);" />
                    <asp:Label ID="lblSegundos" runat="server"></asp:Label>
                </div>
                <div id="tbNotificacion" style="position: relative; top: 0px; left: 0px; width: 830px; height: 174px; margin: 6px;" >
                    <ul>
                        <li><a href="#tbNoLeidas">
                            <asp:Label ID="lblTbNoLeida" runat="server"></asp:Label></a>
                        </li>
                        <li><a href="#tbLeidas">
                            <asp:Label ID="lblTbLeida" runat="server"></asp:Label></a>
                        </li>
                    </ul>
                    <div class="tab_content">
                        <div id="tbNoLeidas">
                            <div>
                                <input type="button" id="btnMarcarLeida" class="ui-button" style="height: 20px; padding: 0px !important; width: 120px; margin: 1px;" data-bind='click: MarcarLeida' value="<%= Tradutor.Traduzir("073_btnMarcarLeida")%>" />
                                <div style="float: right;">
                                    <select id="ddlPrivadaNoLeida" style="margin-right: 10px;" data-bind="options: OpcoesPrivacidade,
                                                                                                    value: PrivacidadeSelecionadaNoLeida"></select>
                                    <input type="button" id="btnBuscarNoLeida" class="ui-button" style="height: 20px; padding: 0px !important; width: 60px; margin: 1px; float: right" data-bind='click: BuscarNoLeida' value="<%= Tradutor.Traduzir("073_btnBuscarNoLeida")%>" />
                                </div>
                                <div style="height: 98px; overflow: auto; margin-top: 5px;">
                                    <table id="grvNoLeida" class="ui-datatable ui-datatable-data" style="border-style:None;width:100%;border-collapse:collapse;" data-bind='visible: (NotificacionesNoLeidasGrid().length > 0)'>
                                        <thead>
                                            <tr style="font-size: 9px;">
                                                <th style="width: 20px;">
                                                    <input type="checkbox" id="chkTodoNoLeida" data-bind="click: CheckAllNoLeidaClick" />
                                                </th>
                                                <th style="width: 350px;">
                                                    <%= Tradutor.Traduzir("073_colNotificaciones")%>
                                                </th>
                                                <th style="width: 30px;">
                                                    <%= Tradutor.Traduzir("073_colCreacion")%>
                                                </th>
                                                <th style="width: 20px;"></th>
                                                <th style="width: 20px;"></th>
                                            </tr>
                                        </thead>
                                        <tbody data-bind='foreach: NotificacionesNoLeidasGrid' class="ui-datatable-data">
                                            <tr style="background-color:#FFFDF2;">
                                                <td style="width: 20px; text-align: center; font-size: 7pt;">
                                                    <input type="checkbox" data-bind="checked: Selecionado, click: SelecionadoClick($parent, false)" />
                                                </td>
                                                <td style="width: 350px; text-align: left; font-size: 7pt;" data-bind="event: { dblclick: function () { alert(ObservacionNotificacion()); } }  ">
                                                    <span data-bind="text: ColumnaNotificacion()"></span>
                                                </td>
                                                <td  style="width: 30px; font-size: 7pt;">
                                                    <span data-bind="text: FechaCreacionFormatada()"></span>
                                                </td>
                                                <td style="width: 20px; text-align: center;">
                                                    <img data-bind="attr: { src: ImgPrivado() }" src='<%=Page.ResolveUrl("~/Imagenes/privado.png")%>' />
                                                </td>
                                                <td style="width: 20px; text-align: center;">
                                                    <img src='<%=Page.ResolveUrl("~/Imagenes/advertencia.png")%>' data-bind='attr: { src: ImgAccion(), alt: TipoNotificacion.CodigoAplicacion() }, visible: (TipoNotificacion.BolEventoRelacionado()), click: ExecutarAcao' />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div data-bind='visible: (NotificacionesNoLeidasGrid().length <= 0)'>
                                        <asp:Label ID="lblSemRegistroNoleida" runat="server" Style="color: #767676; font-size: 9pt; font-style: italic;"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="tbLeidas">
                            <div>
                                <input type="button" id="btnMarcarNoLeida" class="ui-button" style="height: 20px; padding: 0px !important; width: 120px; margin: 1px;" data-bind='click: MarcarNoLeida' value="<%= Tradutor.Traduzir("073_btnMarcarNoLeida")%>" />
                                <div style="float: right;">
                                    <select id="ddlPrivadaLeida" style="margin-right: 10px;" data-bind="options: OpcoesPrivacidade,
                                                                                                        value: PrivacidadeSelecionadaLeida"></select>
                                    <asp:Label ID="lblDesde" runat="server"></asp:Label>
                                    <input type="text" id="txtDesde" data-bind="text: CalendarioDe(), value: CalendarioDe" style="width:100px;" />
                                    <asp:Label ID="lblHasta" runat="server"></asp:Label>
                                    <input type="text" id="txtHasta" data-bind="text: CalendarioHasta(), value: CalendarioHasta" style="width:100px;" />
                                    <input type="button" id="btnBuscar" class="ui-button" style="height: 20px; padding: 0px !important; width: 60px; margin: 1px; float: right" data-bind='click: BuscarLeida' value="<%= Tradutor.Traduzir("073_btnBuscar")%>" />
                                </div>
                                <div style="height: 98px; overflow: auto; margin-top: 5px;">
                                    <table id="grvLeida" class="ui-datatable ui-datatable-data" style="border-style:None;width:100%;border-collapse:collapse;" data-bind='visible: (NotificacionesLeidas().length > 0)'>
                                        <thead>
                                            <tr style="font-size: 9px;">
                                                <th style="width: 20px;">
                                                    <input type="checkbox" id="chkTodoLeida" data-bind="click: CheckAllLeidaClick" />
                                                </th>
                                                <th style="width: 350px;">
                                                    <%= Tradutor.Traduzir("073_colNotificaciones")%>
                                                </th>
                                                <th style="width: 150px;">
                                                    <%= Tradutor.Traduzir("073_colLeida")%>
                                                </th>
                                                <th style="width: 30px;">                                                    
                                                    <%= Tradutor.Traduzir("073_colCreacion")%>
                                                </th>
                                                <th style="width: 20px;"></th>
                                                <th style="width: 20px;"></th>
                                            </tr>
                                        </thead>
                                        <tbody data-bind='foreach: NotificacionesLeidas' class="ui-datatable-data">
                                            <tr style="background-color:#FFFDF2;">
                                                <td style="width: 20px; text-align: center; font-size: 7pt;">
                                                    <input type="checkbox" data-bind="checked: Selecionado, click: SelecionadoClick($parent, true)" />
                                                </td>
                                                <td style="width: 320px; text-align: left; font-size: 7pt;" data-bind="event: { dblclick: function () { alert(ObservacionNotificacion()); } }  ">
                                                    <span data-bind="text: ColumnaNotificacion()"></span>
                                                </td>
                                                <td  style="width: 150px; font-size: 7pt;">
                                                    <span data-bind="text: ColumnaLeida()"></span>
                                                </td>
                                                <td  style="width: 30px; font-size: 7pt;">
                                                    <span data-bind="text: FechaCreacionFormatada()"></span>
                                                </td>
                                                <td style="width: 20px; text-align: center;">
                                                    <img data-bind="attr: { src: ImgPrivado() }" src='<%=Page.ResolveUrl("~/Imagenes/privado.png")%>' />
                                                </td>
                                                <td style="width: 20px; text-align: center;">
                                                    <img src='<%=Page.ResolveUrl("~/Imagenes/advertencia.png")%>' data-bind='attr: { src: ImgAccion(), alt: TipoNotificacion.CodigoAplicacion() }, visible: (TipoNotificacion.BolEventoRelacionado()), click: ExecutarAcao' />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div data-bind='visible: (NotificacionesLeidas().length <= 0)'>
                                        <asp:Label ID="lblSemRegistro" runat="server" Style="color: #767676; font-size: 9pt; font-style: italic;" Text="lblSemRegistro"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
