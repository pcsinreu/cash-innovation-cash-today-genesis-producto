<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Cabecalho.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.Cabecalho" %>
<table style="border-collapse: collapse; width:100%; height:56px; border-style:none; background-color: #FFFFFF;">
    <tr>
        <td style="background-image:url(imagenes/imgLeft_cab.jpg); background-repeat:no-repeat; background-size: 249px 70px;  width:30%;">
            &nbsp;
        </td>
        <td style="color: Black; font-family: Verdana; font-size: 20px; text-align:center; vertical-align:middle;">
            <asp:Label ID="lblTitulo" runat="server" TabIndex="1000" Text="[Gestión de Efectivos]"></asp:Label>
        </td>
        <td style="text-align:right; width:30%;">
            <div runat="server" id="dvVersion">
                <img src="imagenes/imgRight_cab.jpg" width="246" height="50" /><br />
                <div style="margin-right:10px;">
                <asp:Label ID="lblVersion" runat="server" Text="[Versión]"></asp:Label>
                <asp:Label ID="Label1" runat="server" Text=" - "></asp:Label>
                <asp:Label ID="lblUsuario" runat="server" Text="[Usuário]"></asp:Label>
                <asp:Label ID="Label2" runat="server" Text=" - "></asp:Label>
                <asp:Label ID="lblData" runat="server" Text="[Data]"></asp:Label>
                <asp:Label ID="Label3" runat="server" Text=" - "></asp:Label>
                <asp:LinkButton ID="lbSair" TabIndex="999" runat="server">[Sair]</asp:LinkButton>
                </div>
            </div>
        </td>        
    </tr>        
</table>

