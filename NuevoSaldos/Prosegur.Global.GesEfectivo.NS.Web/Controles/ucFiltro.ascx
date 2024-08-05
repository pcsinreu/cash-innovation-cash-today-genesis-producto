<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucFiltro.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucFiltro" %>


<div style="width: 100%; margin-left: 5px;">
    <asp:UpdatePanel ID="upFiltro" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
    <ContentTemplate>
    <div id="dvComponentes" style="width: 100%; height: 290px; overflow-y: auto; overflow-x: hidden">
        <div id="dvFiltroCliente">
            <asp:PlaceHolder ID="phCliente" runat="server"></asp:PlaceHolder>
        </div>
        <div id="dvFiltroCanal">
            <asp:PlaceHolder ID="phCanal" runat="server"></asp:PlaceHolder>
        </div>
        <div id="dvFiltroSector">
            <asp:PlaceHolder ID="phSector" runat="server"></asp:PlaceHolder>
        </div>
        <div id="dvFiltroDocumento" style="width:95%;">
            <asp:PlaceHolder ID="phFiltroDocumento" runat="server"></asp:PlaceHolder>
        </div>
        <div id="dvFiltroContenedor" style="width:95%;">
            <asp:PlaceHolder ID="phFiltroContenedor" runat="server"></asp:PlaceHolder>
        </div>
        <div id="dvFiltroRemesa" style="width:95%;">
            <asp:PlaceHolder ID="phFiltroRemesa" runat="server"></asp:PlaceHolder>
        </div>
        <div id="dvFiltroBulto" style="width:95%;">
            <asp:PlaceHolder ID="phFiltroBulto" runat="server"></asp:PlaceHolder>
        </div>
        <div id="dvFiltroParcial" style="width:95%;">
            <asp:PlaceHolder ID="phFiltroParcial" runat="server"></asp:PlaceHolder>
        </div>
        <div id="dvFiltroDivisas" style="width:95%;">
            <asp:PlaceHolder ID="phFiltroDivisas" runat="server"></asp:PlaceHolder>
        </div>
    </div>
    </ContentTemplate></asp:UpdatePanel>
    <div style="height: auto; width: 100%; position: relative; margin-top: 15px;">
        <div style="width: 48%; float: right;">
            <asp:Button ID="btnLimpiar" runat="server" Text="Button" class="ui-button" Width="100" />
        </div>
        <div style="width: 120px; float: right;">
            <asp:Button ID="btnBuscar" Text="Buscar" runat="server" class="ui-button" Width="100" />
        </div>
        <div class="dvclear">
        </div>
    </div>

</div>