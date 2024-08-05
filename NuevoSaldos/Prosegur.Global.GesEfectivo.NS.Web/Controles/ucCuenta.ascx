<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucCuenta.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucCuenta" %>
<div class="ui-panel-titlebar">
    <asp:Label ID="lblTitulo" runat="server" Text="Lista de Objectos" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
</div>
<div style="width: 100%; margin-left: 5px; margin-top: 5px;">
    <div style="width: 100%;">
        <div style="width: 100%; margin-bottom: 5px;">
            <asp:PlaceHolder runat="server" ID="phSector"></asp:PlaceHolder>
        </div>
        <div style="width: 100%; margin-bottom: 5px;">
            <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
        </div>
        <div style="width: 100%; margin-bottom: 5px;">
            <asp:PlaceHolder runat="server" ID="phCanal"></asp:PlaceHolder>
        </div>
        <div style="width: 100%; ">
            <div class="dvUsarFloat" style="margin: 0px;">
                <div id="dvTotalizador" runat="server" style="display: none;">
                    <asp:Label ID="lblTotalizador" runat="server" /><br />
                    <asp:Label ID="lblValorTotalizador" runat="server" CssClass="valor" />
                </div>
            </div>
        </div>
        <div class="dvclear"></div>
    </div>
</div>
