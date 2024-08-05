<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucOrigenMovimiento.ascx.vb"
    EnableTheming="true" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucOrigenMovimiento" %>

<div id="ucOrigenMovimiento" style="width:auto; height:30px">
    <div id="dvtxtOrigenMovimiento" style="float:left; padding-left:10px;">
        <asp:TextBox ID="txtOrigen" runat="server" SkinID="filter-textbox" Width="45px" ReadOnly="True" />
    </div>
    <div id="dvtxtCodDelegacion" style="float:left; padding-left:10px;">
        <asp:TextBox ID="txtCodDelegacion" runat="server" SkinID="filter-textbox" 
            Width="100px" ReadOnly="True" />
    </div>
    <div id="dvtxtDelegacion" style="float:left;">
        <asp:TextBox ID="txtDelegacion" runat="server" SkinID="filter-textbox" 
            ReadOnly="True" Width="200px" />
    </div>
    <div id="dvtxtCodPlanta" style="float:left; padding-left:10px;">
        <asp:TextBox ID="txtCodPlanta" runat="server" SkinID="filter-textbox" 
            Width="100px" ReadOnly="true" />
    </div>
    <div id="dvtxtDesPlanta" style="float:left;">
        <asp:TextBox ID="txtDesPlanta" runat="server" SkinID="filter-textbox" 
            ReadOnly="True" Width="200px" />
    </div>
    <div id="dvtxtCodSector" style="float:left; padding-left:10px;">
        <asp:TextBox ID="txtCodSector" runat="server" SkinID="filter-textbox" 
            Width="100px" ReadOnly="True" />
    </div>
    <div id="dvtxtDesSector" style="float:left;">
        <asp:TextBox ID="txtDesSector" runat="server" SkinID="filter-textbox" 
            ReadOnly="True" Width="200px" />
    </div>
</div>
