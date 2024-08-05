<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Boton.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.Boton" %>
<asp:UpdatePanel runat="server" ID="upnBoton">
    <ContentTemplate>
        <asp:Panel ID="pnlControles" runat="server">
            <div class="ui-button filterInput" style="margin-right:5px;">
                <asp:ImageButton ID="imgBoton" runat="server" />
                <asp:Button ID="btnBoton" runat="server" style="cursor: pointer" />
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>