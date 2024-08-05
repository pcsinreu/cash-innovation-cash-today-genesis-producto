<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Erro.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.Erro" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
            <table width="100%" style="background-color: white" runat="server" id="tblError" visible="True">
                <tr>
                    <td style="width: 50px">
                        
                    </td>
                    <td align="left">
                        <table width="100%" style="background-color: white">
                            <tr>
                                <td>
                                    <asp:Label ID="lblError" runat="server" Font-Bold="False" Text="[lblError]" CssClass="Lbl2"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trLkbDetalhes" runat="server">
                                <td>
                                    <asp:LinkButton ID="lkbExibeDetalhes" TabIndex="1" runat="server" CausesValidation="False"
                                        OnClientClick="return ExibirDetalhes(this);">LinkButton</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divTxtErro" style="display: none">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtErro" CssClass="Text02" TabIndex="1" runat="server" Height="60px"
                                                        Width="460px" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:LinkButton ID="lkbCopiar" TabIndex="1" runat="server" CausesValidation="False">LinkButton</asp:LinkButton>
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
