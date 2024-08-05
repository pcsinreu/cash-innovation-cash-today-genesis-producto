<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucDelegacion.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.ucDelegacion" %>
    
<table style="margin-left: 5px;" >
    <tr>
    <td class="tamanho_celula">
        <asp:Label ID="lblDelegacion" runat="server" Text="" CssClass="label2"></asp:Label></td>
        <td >            
            <div style="width:auto;"> 
            <div style="border:1px solid #ccc; " > 
                <asp:CheckBox ID="chkTodos" Visible="false" CssClass="Text01" runat="server" />
                </div>
                <div style="max-height:100px; overflow-y:auto;  border:1px solid #ccc; overflow-x:hidden; ">
                    <asp:CheckBoxList ID="cblDelegaciones"  Visible="false" runat="server" 
                        RepeatLayout="Flow"  >
                    </asp:CheckBoxList>
            <asp:RadioButtonList ID="rblDelegaciones" Visible="false" runat="server">
            </asp:RadioButtonList>                   
                </div> 
            </div>
        </td>
        <td> <asp:CustomValidator ID="csvDelegaciones"  runat="server" 
                        ErrorMessage="*" Text="*">*</asp:CustomValidator></td>
    </tr>
</table>
