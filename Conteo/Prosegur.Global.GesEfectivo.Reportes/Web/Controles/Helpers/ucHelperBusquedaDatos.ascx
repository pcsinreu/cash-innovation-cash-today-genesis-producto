<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucHelperBusquedaDatos.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.ucHelperBusquedaDatos" %>
<div id="ucHelper" runat="server" class="classHelper">
    <table>
        <tr>
            <td class="tamanho_celula_helper">
                <asp:Label runat="server" ID="lblTitulo" CssClass="label2"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtCodigo" runat="server" onkeydown="javascript: return event.keyCode != 13"
                    AutoPostBack="true" MaxLength="15" Style="width: 80px;" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" /></td>
            <td>
                <div id="dvButtonBusqueda" runat="server" style="display: none; margin: 12px 2px 0px 2px; height: auto;">
                    <asp:ImageButton ID="imgButtonBusqueda" runat="server" ImageUrl="~/App_Themes/Padrao/css/img/button/buscar.png" />
                </div>
            </td>
            <td>
                <div id="dvDescripcion" runat="server" style="display: none;">
                    <asp:TextBox ID="txtDescripcion" runat="server" onkeydown="javascript: return event.keyCode != 13"
                        AutoPostBack="true" MaxLength="100" Style="width: 180px;" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" />
                </div>
            </td>
            <td>
                <div id="dvButtonLimpaCampo" runat="server" style="display: none; margin: 12px 0px 0px 2px; height: auto;">
                    <asp:ImageButton ID="imgButtonLimpaCampo" runat="server" ImageUrl="~/Imagenes/Quitar.png"
                        CssClass="butondefectoPequeno" />
                </div>
            </td>
        </tr>
    </table>
</div>
<asp:PlaceHolder ID="phUcPopUp" runat="server"></asp:PlaceHolder>
