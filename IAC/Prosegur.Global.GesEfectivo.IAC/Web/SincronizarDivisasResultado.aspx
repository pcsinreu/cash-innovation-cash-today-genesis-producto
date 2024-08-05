<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SincronizarDivisasResultado.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.SincronizarDivisasResultado" MasterPageFile="~/Master/MasterModal.Master" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>
<%@ Register Src="Controles/ResultadoSincronizacion.ascx" TagName="Resultado" TagPrefix="uc2" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>IAC - Sincronizacion de Divisas</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content-modal">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblTituloPagina" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table width="100%">
                    <asp:Panel ID="pnlConteoPrincipal" runat="server" Visible="true">
                        <tr>
                            <td align="center" colspan="4">
                                <table width="90%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <fieldset style="border-color: Black; border: 1px;">
                                                <legend align="center">
                                                    <asp:Label ID="lblTituloConteo" runat="server" Text="Label" CssClass="Lbl2" Font-Bold="True"></asp:Label>
                                                </legend>
                                                <asp:Panel ID="pnlConteo" runat="server">
                                                    <uc2:Resultado ID="ResultadoConteo" runat="server" />
                                                </asp:Panel>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlSalidasPrincipal" runat="server" Visible="true">
                        <tr>
                            <td align="center" colspan="4">
                                <table width="90%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <fieldset style="border-color: Black; border: 1px;">
                                                <legend align="center">
                                                    <asp:Label ID="lblTituloSalidas" runat="server" Text="Label" CssClass="Lbl2" Font-Bold="True"></asp:Label>
                                                </legend>
                                                <asp:Panel ID="pnlSalidas" runat="server">
                                                    <uc2:Resultado ID="ResultadoSalidas" runat="server" />
                                                </asp:Panel>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlAtmPrincipal" runat="server" Visible="true">
                        <tr>
                            <td align="center" colspan="4">
                                <table width="90%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <fieldset style="border-color: Black; border: 1px;">
                                                <legend align="center">
                                                    <asp:Label ID="lblTituloAtm" runat="server" Text="Label" CssClass="Lbl2" Font-Bold="True"></asp:Label>
                                                </legend>
                                                <asp:Panel ID="pnlAtm" runat="server">
                                                    <uc2:Resultado ID="ResultadoAtm" runat="server" />
                                                </asp:Panel>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </asp:Panel>

                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

