<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ResultadoSincronizacion.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.ResultadoSincronizacion" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="10px">
                </td>
                <td width="10%" align="left">
                    <asp:Label ID="lblResultado" runat="server" Text="Reultado" CssClass="Lbl2" Font-Bold="True"></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lblResultadoMostrar" runat="server" Text="" CssClass="Lbl2"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="10px">
                </td>
                <td width="10%" align="left">
                    <asp:Label ID="lblUrl" runat="server" Text="Url" CssClass="Lbl2" Font-Bold="True"></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lblUrlMostrar" runat="server" Text="" CssClass="Lbl2"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="20" colspan="3">
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Panel ID="pnlResultado" runat="server">
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
