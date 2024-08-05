<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UcItens.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.UcItens" %>
<div runat="server" id="DivPrincipal">
    <fieldset class="ui-fieldset ui-fieldset-toggleable certificados">
        <legend class="ui-fieldset-legend ui-corner-all ui-state-default"><span class="ui-fieldset-toggler ui-icon ui-icon-minusthick">
        </span>
            <asp:Label ID="lblTitulo" runat="server" />
        </legend>
        <div class="ui-fieldset-content">
            <asp:UpdatePanel ID="UpdatePanelDelegaciones" runat="server">
                <ContentTemplate>
                    <table>
                        <tbody>
                            <tr valign="middle" style="text-align:center">
                                <td>
                                    <asp:ListBox ID="lstItensDisponiveis" runat="server" Rows="4" Height="150px" Width="200px"
                                        SelectionMode="Multiple" />
                                </td>
                                <td >
                                    <ul class="btnPick">
                                        <li>
                                            <asp:ImageButton ID="imbAddAllItens" SkinID="button_navegate_add_all" runat="server">
                                            </asp:ImageButton></li>
                                        <li>
                                            <asp:ImageButton ID="imbAddItens" SkinID="button_navegate_add" runat="server"></asp:ImageButton></li>
                                        <li>
                                            <asp:ImageButton ID="imbRemoveAllItens" SkinID="button_navegate_remove_all" runat="server">
                                            </asp:ImageButton></li>
                                        <li>
                                            <asp:ImageButton ID="imbRemoveItens" SkinID="button_navegate_remove" runat="server">
                                            </asp:ImageButton></li>
                                    </ul>
                                </td>
                                <td>
                                    <asp:ListBox ID="lstItensSelecionadas" runat="server" Rows="4" Height="150px" Width="200px"
                                        SelectionMode="Multiple" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </fieldset>
</div>
