<%@ Control Language="VB" AutoEventWireup="false" CodeBehind="UcMaquina.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.UcMaquina" %>


 <style type="text/css">
      .ui-draggable .ui-dialog-titlebar
        {
            width: -webkit-fill-available;
        }
    </style>
<div>
    <div style="width: 100%; position: relative;">
        <div style="float: left;">
            <asp:PlaceHolder runat="server" ID="phDelegacion"></asp:PlaceHolder>
        </div>
        <div style="float: left;">
            <asp:PlaceHolder runat="server" ID="phSector"></asp:PlaceHolder>
        </div>
        <div class="dvclear"></div>
    </div>
</div>