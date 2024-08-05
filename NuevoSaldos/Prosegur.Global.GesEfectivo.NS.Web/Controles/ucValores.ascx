<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucValores.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucValores" %>
<asp:UpdatePanel ID="upValores" runat="server">
    <ContentTemplate>
        <div class="ui-panel-titlebar">
            <asp:Label ID="lblTitulo" runat="server" Text="Valores" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
        </div>
        <asp:PlaceHolder runat="server" ID="phUCValoresDivisas"></asp:PlaceHolder>
    </ContentTemplate>
</asp:UpdatePanel>
