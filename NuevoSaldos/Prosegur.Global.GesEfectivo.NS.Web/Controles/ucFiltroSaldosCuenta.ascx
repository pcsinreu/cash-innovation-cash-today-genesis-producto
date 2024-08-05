<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucFiltroSaldosCuenta.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucFiltroSaldosCuenta" %>
<div style="margin: 5px 0px; width: 100%; color: #767676;">
    <asp:Label ID="lblTipoValores" runat="server" Text=""></asp:Label><br />
    <asp:CheckBoxList ID="cklTipoValores" runat="server" RepeatColumns="5">
    </asp:CheckBoxList>
    <div style="margin-left:15px;">
        <asp:CheckBox ID="chkConsiderarSomaZero" runat="server" Visible="false" />
    </div>
    <br />
    <asp:Label ID="lblDivisas" runat="server" Text=""></asp:Label><br />
    <div style="width: 93%; height:100px; overflow:auto; border-radius: 5px !important; padding: 10px; border: solid 1px #A8A8A8;">
        <asp:CheckBoxList ID="cklDivisas" runat="server" RepeatColumns="5">
        </asp:CheckBoxList>
    </div>
</div>
