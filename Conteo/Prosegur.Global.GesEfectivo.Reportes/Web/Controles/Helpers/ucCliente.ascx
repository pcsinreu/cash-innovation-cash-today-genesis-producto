<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucCliente.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.ucCliente" %>
<div>
    <div style="width: 100%; position: relative;">
        <div style="margin-bottom: 5px;" >
            <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
        </div>
        <div style="margin-bottom: 5px;" >
            <asp:PlaceHolder runat="server" ID="phSubCliente"></asp:PlaceHolder>
        </div>
        <div style="margin-bottom: 5px;" >
            <asp:PlaceHolder runat="server" ID="phPtoServicio"></asp:PlaceHolder>
        </div>
        <div class="dvclear"></div>
    </div>
</div>