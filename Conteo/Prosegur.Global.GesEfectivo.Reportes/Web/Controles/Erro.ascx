<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Erro.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.Erro" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table id="tbMensagem" runat="server" width="100%" style="background-color: white">
            <tr>
                <td style="width: 50px">
                    <asp:Image ID="imgErro" runat="server" ImageUrl="~/Imagenes/error.jpg" />
                </td>
                <td align="left">
                    <table width="100%" style="background-color: white">
                        <tr>
                            <td>
                                <asp:Label ID="lblError" runat="server" Font-Bold="False" Text="lblError" CssClass="Lbl2"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trLkbDetalhes" runat="server">
                            <td>
                                <asp:LinkButton ID="lkbExibeDetalhes" runat="server" TabIndex="1" CausesValidation="False"
                                    OnClientClick="return ExibirDetalhes(this);">LinkButton</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divTxtErro" style="display: none">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtErro" runat="server" Height="60px" Width="460px" ReadOnly="true"
                                                    TextMode="MultiLine" CssClass="Text01" TabIndex="1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:LinkButton ID="lkbCopiar" runat="server" CausesValidation="False" TabIndex="1">LinkButton</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
