<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucCheckBoxList.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucCheckBoxList" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/js/Controles/ucCheckBoxList.js" />
    </Scripts>
</asp:ScriptManagerProxy>

<div id="ucCheckBoxList" runat="server">
    <asp:HiddenField ID="hdnJson" runat="server"></asp:HiddenField>
    <asp:Label ID="lbltituloCheckBoxList" runat="server" Text="TextBox" /><br />
    <table>
        <tr>
            <td>
                <input type="checkbox" id="chkMarcarTodos" name="chkMarcarTodos" runat="server" onchange="MarcarDesmarcarTodos(this.id);" />
                <asp:Label ID="lblchkMarcarTodos" runat="server" Text="Label">Marcar todos</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divCheckBoxList" runat="server" style="width: 250px; height: 210px; border: solid 1px; overflow: auto;">
                    <asp:CheckBoxList ID="chkGenerico" runat="server" onchange="ControleSelecionados(this.id);" />
                    <asp:Literal ID="lblMesaje" runat="server"></asp:Literal>
                </div>
            </td>
        </tr>
    </table>
</div>
