<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucDropDownList.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.ucDropDownList" %>
<div>
    <table class="tabela_campos">
        <tr>
            <td class="tamanho_celula" align="left">
                <asp:Label ID="lblDropDown" runat="server" Text="Setor" CssClass="label2"></asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" ID="ddl" CssClass="Text01"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ID="rfvDDL" ControlToValidate="ddl">*</asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
</div>
