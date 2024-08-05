<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucInfContenedor.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucInfContenedor" %>

<div id="ucListaContenedor" runat="server" style="width: 98%; color: #767676; padding: 0px 0px 10px 0px; display: none;">
    <div class="ui-panel-titlebar" id="dvTitulo" runat="server">
        <asp:Label ID="lblTituloContenedor" runat="server" Text="Contenedor" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
    </div>
    <div class="dvUsarFloat" style="margin-left:10px;">
        <div id="dvCodigo" runat="server" style="display: none;">
            <asp:Label ID="lblCodigo" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="txtCodigo" runat="server" Text="" CssClass="valor"></asp:Label>
        </div>
        <div id="dvPrecintos" runat="server" style="display: none;">
            <asp:Label ID="lblPrecintos" runat="server" Text=""></asp:Label><br />
            <div id="lstItensPrecintos" runat="server" class="BuscarPorvalores">
                <asp:Literal ID="litPrecintos" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
</div>