<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucContainerDocumentos.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucContainerDocumentos" %>
<asp:HiddenField runat="server" ID="hdnExibirModal" EnableViewState="true" />
<div style="width: 100%;">
    <asp:PlaceHolder ID="phListaElementosDelGrupo" runat="server"></asp:PlaceHolder>
</div>
<div style="width: 100%;">
    <div id="dvFiltro" runat="server"  style="display:none">
        <div class="ui-panel-titlebar">
            <asp:Label ID="lblTituloFiltro" runat="server" Text="" SkinID="filtro-label_titulo" Style="color: #767676 !important; margin-right: 5px !important;" />
            <asp:Image ID="btnVisualizarFiltro" runat="server" style="border: none; width: auto !important; height: auto !important; cursor: pointer;" ImageUrl="~/Imagenes/Lupa.png"/>
            <asp:Label ID="lblLegendaFiltro" runat="server" Text="" SkinID="filtro-label_titulo" Style="color: #CCCCCC !important; margin-right: 5px !important;" />
        </div>
        <div id="dvFiltroPopup" runat="server" style="display: none;">
            <div class="ui-widget-overlay ui-front"></div>
            <div class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-front ui-draggable" style="height: auto; width: 778px; top: 21.5px; left: 290.5px; display: block;" tabindex="-1" role="dialog">
                <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                    <span id="ui-id-53" class="ui-dialog-title" style="color: #5e5e5e">
                        <asp:Literal ID="lblTituloPopUpFiltro" runat="server"></asp:Literal>
                    </span>
                    <div id="dvCerrarPopUp" runat="server" style="position: absolute; right: .3em; border-style: none; cursor: pointer;">
                        <span class="ui-button-icon-primary ui-icon ui-icon-closethick"></span>
                    </div>
                </div>
                <div id="dvFiltroPopupConteudo" style="width: auto; min-height: 250px; max-height: none; height: auto;" class="ui-dialog-content ui-widget-content">
                    <asp:PlaceHolder ID="phFiltros" runat="server"></asp:PlaceHolder>
                </div>
            </div>
        </div>
    </div>

    <asp:PlaceHolder ID="phRespuestaDelFiltro" runat="server"></asp:PlaceHolder>
</div>
