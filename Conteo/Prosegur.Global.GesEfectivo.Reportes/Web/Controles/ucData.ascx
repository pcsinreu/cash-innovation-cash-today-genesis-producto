<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucData.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.ucData" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<div>
    <table class="tabela_campos">
        <tr>
            <td class="tamanho_celula" align="left">
                <asp:Label ID="lblData" runat="server" Text="lblData"
                    CssClass="Lbl2"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDataDe" runat="server" Text="lblDataDe" CssClass="Lbl2"></asp:Label>
                <asp:TextBox ID="txtDataInicio" runat="server" MaxLength="10" Width="90px" 
                    CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                <asp:Label ID="lblDataAte" runat="server" Text="lblDataAte" CssClass="Lbl2"></asp:Label>
                <asp:TextBox ID="txtDataFin" runat="server" MaxLength="16" Width="90px" 
                    CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
            </td>
        </tr>
    </table>
</div>
