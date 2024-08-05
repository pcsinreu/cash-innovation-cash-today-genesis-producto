<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UcSeleccion.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.UcSeleccion" %>
<div runat="server" id="DivPrincipal" style="vertical-align: top;" class="ui-fieldset-content">
    <asp:UpdatePanel ID="UpdatePanelDelegaciones" runat="server">
        <ContentTemplate>
            <table>
                <tbody>
                    <tr id="trTitulos" runat="server">
                        <td id="tdTituloDisponiveis" runat="server"></td>
                        <td></td>
                        <td id="tdTituloSeleccionados" runat="server"></td>
                        <td></td>
                    </tr>
                    <tr style="text-align: center">
                        <td>
                            <asp:ListBox ID="lstItensDisponiveis" runat="server" SelectionMode="Multiple" Style="border: solid 1px; font-size:10px" />
                        </td>
                        <td>
                            <ul class="btnPick">
                                <li>
                                    <asp:ImageButton ID="imbAddAllItens" SkinID="button_navegate_add_all" runat="server"></asp:ImageButton></li>
                                <li>
                                    <asp:ImageButton ID="imbAddItens" SkinID="button_navegate_add" runat="server"></asp:ImageButton></li>
                                <li>
                                    <asp:ImageButton ID="imbRemoveAllItens" SkinID="button_navegate_remove_all" runat="server"></asp:ImageButton></li>
                                 <li>
                                    <asp:ImageButton ID="imbRemoveItens" SkinID="button_navegate_remove" runat="server"></asp:ImageButton></li>
                            </ul>
                        </td>
                        <td>
                            <asp:ListBox ID="lstItensSelecionadas" runat="server" SelectionMode="Multiple" Style="border: solid 1px;font-size:10px" />
                        </td>
                        <td id="tdOrdenacion" runat="server">
                            <ul class="btnPick">
                                <li>
                                    <asp:ImageButton ID="imbMoverItenArriba" SkinID="button_navegate_move_up" runat="server"></asp:ImageButton></li>
                                <li>
                                    <asp:ImageButton ID="imbMoverItenAbajo" SkinID="button_navegate_move_down" runat="server"></asp:ImageButton></li>
                            </ul>
                        </td>
                    </tr>
                </tbody>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
