<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucInfAdicionales.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucInfAdicionales" %>
<div id="ucInfAdicionales" runat="server" style="width:98%; display:none;">
    <div class="ui-panel-titlebar">
        <asp:Label ID="lblTitulo" runat="server" Text="" SkinID="filtro-label_titulo" />
    </div>
    <div class="dvUsarFloat" style="margin-left:8px;">
        <asp:PlaceHolder runat="server" ID="phControles"></asp:PlaceHolder>
        <div class="dvclear"></div>
    </div>
</div>