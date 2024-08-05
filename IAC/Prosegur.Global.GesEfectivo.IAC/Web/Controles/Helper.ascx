<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Helper.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.Helper" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<table width="100%" cellpadding="0px" cellspacing="0px">
    <tr>
        <td width="170px">
            <asp:TextBox ID="txtHelper" runat="server" Width="170px"></asp:TextBox><asp:CustomValidator
                ID="csvTxtHelper" runat="server" ControlToValidate="txtHelper" ErrorMessage=""
                Text="*"></asp:CustomValidator>
            <asp:HiddenField ID="hidSessionValor" runat="server" />
            <asp:HiddenField ID="hidSessionLimpar" runat="server" />
        </td>
        <td align="left">
            <pro:Botao ID="btnHelper" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                ExibirLabelBtn="false" Tipo="Consultar">
            </pro:Botao>
        </td>
    </tr>
</table>
