<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucFiltroElementos.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucFiltroElementos" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/js/ucListaElementos.js" />
    </Scripts>
</asp:ScriptManagerProxy>
<div class="ui-panel-titlebar">
    <asp:Label ID="lblTitulo" runat="server" Text="" CssClass="ui-panel-title" /></div>
<div class="dvpadre">
    <div id="dvPrecinto" runat="server" style="display: none;">
        <div class='legenda'>
            <asp:Label runat="server" ID="lblPrecinto" SkinID="filtro-label" Text=""></asp:Label>
        </div>
        <div class='campo'>
            <asp:TextBox runat="server" ID="txtPrecinto" SkinID="filtro-textbox" onkeypress="if(event.keyCode==13) {event.preventDefault(); this.value+=';'; return false; }"></asp:TextBox>
            <img src="../Imagenes/BarCodeSeleccionar.png" style="vertical-align: middle" />
        </div>
    </div>
    <div id="dvTipoContenedor" runat="server" style="display: none;">
        <div class='legenda'>
            <asp:Label runat="server" ID="lblTipoContenedor" Text="" SkinID="filtro-label"></asp:Label>
        </div>
        <div class='campo'>
            <asp:DropDownList runat="server" ID="ddlTipoContenedor" SkinID="filtro-dropdownlist">
            </asp:DropDownList>
        </div>
    </div>
    <div id="dvTipoServicio" runat="server" style="display: none;">
        <div class='legenda'>
            <asp:Label runat="server" ID="lblTipoServicio" Text="" SkinID="filtro-label"></asp:Label>
        </div>
        <div class='campo'>
            <asp:DropDownList runat="server" ID="ddlTipoServicio" SkinID="filtro-dropdownlist">
            </asp:DropDownList>
        </div>
    </div>
    <div id="dvTipoFormato" runat="server" style="display: none;">
        <div class='legenda'>
            <asp:Label runat="server" ID="lblTipoFormato" SkinID="filtro-label" Text=""></asp:Label>
        </div>
        <div class='campo'>
            <asp:DropDownList runat="server" ID="ddlTipoFormato" SkinID="filtro-dropdownlist">
            </asp:DropDownList>
        </div>
    </div>
    <div id="dvNumeroExterno" runat="server" style="display: none;">
        <div class='legenda'>
            <asp:Label runat="server" ID="lblNumeroExterno" SkinID="filtro-label" Text=""></asp:Label>
        </div>
        <div class='campo'>
            <asp:TextBox runat="server" ID="txtNumeroExterno" SkinID="filtro-textbox" onkeypress="if(event.keyCode==13) {event.preventDefault(); this.value+=';'; return false; }"></asp:TextBox>
            <img src="../Imagenes/BarCodeSeleccionar.png" style="vertical-align: middle" />
        </div>
    </div>
    <div id="dvCodigoComprovante" runat="server" style="display: none;">
        <div class='legenda'>
            <asp:Label runat="server" ID="lblCodigoComprovante" SkinID="filtro-label" Text=""></asp:Label>
        </div>
        <div class='campo'>
            <asp:TextBox runat="server" ID="txtCodigoComprovante" SkinID="filtro-textbox" onkeypress="if(event.keyCode==13) {event.preventDefault(); this.value+=';'; return false; }"></asp:TextBox>
            <img src="../Imagenes/BarCodeSeleccionar.png" style="vertical-align: middle" />
        </div>
    </div>
    <div id="dvCodigoExterno" runat="server" style="display: none;">
        <div class='legenda'>
            <asp:Label runat="server" ID="lblCodigoExterno" SkinID="filtro-label" Text=""></asp:Label>
        </div>
        <div class='campo'>
            <asp:TextBox runat="server" ID="txtCodigoExterno" SkinID="filtro-textbox" onkeypress="if(event.keyCode==13) {event.preventDefault(); this.value+=';'; return false; }"></asp:TextBox>
            <img src="../Imagenes/BarCodeSeleccionar.png" style="vertical-align: middle" />
        </div>
    </div>
    <div id="dvCodigoRuta" runat="server" style="display: none;">
        <div class='legenda'>
            <asp:Label runat="server" ID="lblCodigoRuta" SkinID="filtro-label" Text=""></asp:Label>
        </div>
        <div class='campo'>
            <asp:TextBox runat="server" ID="txtCodigoRuta" SkinID="filtro-textbox" onkeypress="if(event.keyCode==13) return false;"></asp:TextBox>
        </div>
    </div>
    <div id="dvCodigoContenedor" runat="server" style="display: none;">
        <div class='legenda'>
            <asp:Label runat="server" ID="lblCodigoContenedor" SkinID="filtro-label" Text=""></asp:Label>
        </div>
        <div class='campo'>
            <asp:TextBox runat="server" ID="txtCodigoContenedor" SkinID="filtro-textbox"></asp:TextBox>
        </div>
    </div>
    <div id="dvFechaAltaDesde" runat="server" style="display: none;">
        <div class='legenda'>
            <asp:Label runat="server" ID="lblFechaAltaDesde" SkinID="filtro-label" Text=""></asp:Label>
        </div>
        <div class='campo'>
            <asp:TextBox runat="server" ID="txtFechaAltaDesde" SkinID="filtro-textbox"></asp:TextBox>
        </div>
    </div>
    <div id="dvFechaAltaHasta" runat="server" style="display: none;">
        <div class='legenda'>
            <asp:Label runat="server" ID="lblFechaAltaHasta" SkinID="filtro-label" Text=""></asp:Label>
        </div>
        <div class='campo'>
            <asp:TextBox runat="server" ID="txtFechaAltaHasta" SkinID="filtro-textbox"></asp:TextBox>
        </div>
    </div>
    <div class="dvclear">
    </div>
</div>