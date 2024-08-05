<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master"
    CodeBehind="ObtenerNivelSaldos.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ObtenerNivelSaldos" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Src="~/Controles/PopupCertificados.ascx" TagName="Certificado" TagPrefix="ucPopup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset class="ui-fieldset ui-fieldset-toggleable" style="margin: 10px 5px 0 10px;">
        <legend class="ui-fieldset-legend ui-corner-all ui-state-default"><span class="ui-fieldset-toggler ui-icon ui-icon-minusthick">
        </span>
            <asp:Label ID="lblDadosEntrada" runat="server" Text="Dados de Entrada" />
        </legend>
        <div class="ui-fieldset-content">
            <ul class="form-filter">
                <li>
                    <asp:UpdatePanel ID="upbusca" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblClienteTotalizador" Width="25%" runat="server" SkinID="filter-label" Text="Cliente Totalizador de Saldos" />
                            &nbsp;<asp:TextBox ID="txtCodClienteTotalizador" runat="server" SkinID="form-textbox-mandatory"
                                MaxLength="15" AutoPostBack="True" />
                            <asp:ImageButton ID="btnBuscar" runat="server" CssClass="ui-button-helper ui-button"
                                ImageUrl="~/App_Themes/Padrao/css/img/grid/search.png" />
                            <asp:TextBox ID="txtDesClienteTotalizador" runat="server" SkinID="form-textbox-mandatory"
                                MaxLength="100" Width="320px" AutoPostBack="True" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </li>
                <li>
                    <asp:Label ID="lblSubCanal" Width="25%" runat="server" SkinID="filter-label" Text="SubCanal" />                    
                    <asp:DropDownList ID="ddlSubCanal" Style="margin-left:5px" SkinID="form-dropdownlist-mandatory-big" runat="server">
                    </asp:DropDownList>
                </li>
            </ul>
            <ul class="certificados-btns">
                <li>
                    <asp:Button ID="btnConsultar" SkinID="filter-button" runat="server" Text="Consultar" />
                </li>
                <li>
                    <asp:Button ID="btnLimpar" SkinID="filter-button" runat="server" Text="Limpar" />
                </li>
            </ul>
        </div>
    </fieldset>
    <div class="ui-panel ui-widget ui-corner-all ui-entity" style="margin: 10px 5px 0 10px;">
        <div class="ui-panel-titlebar ui-corner-all">
            <span class="ui-panel-title">
                <asp:Label ID="lblResultadoClienteTotalizador" runat="server" Text="[lblResultadoClienteTotalizador]"></asp:Label></span></div>
        <div class="ui-panel-content ">
            <asp:GridView ID="gdvClientesTotalizador" AutoGenerateColumns="False" runat="server"
                EnableModelValidation="True" EmptyDataText="Datos vazio">
                <Columns>
                    <asp:BoundField DataField="CodCliente" HeaderText="CodCliente" />
                    <asp:BoundField DataField="DesCliente" HeaderText="DesCliente" />
                    <asp:BoundField DataField="CodSubcliente" HeaderText="CodSubcliente" />
                    <asp:BoundField DataField="DesSubcliente" HeaderText="DesSubcliente" />
                    <asp:BoundField DataField="CodPtoServicio" HeaderText="CodPtoServicio" />
                    <asp:BoundField DataField="DesPtoServicio" HeaderText="DesPtoServicio" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="ui-panel ui-widget ui-corner-all ui-entity" style="margin: 10px 5px 0 10px;">
        <div class="ui-panel-titlebar ui-corner-all">
            <span class="ui-panel-title">
                <asp:Label ID="lblResultadoClienteNaoTotalizador" runat="server" Text="[lblResultadoClienteNaoTotalizador]"></asp:Label></span></div>
        <div class="ui-panel-content ">
            <asp:GridView ID="gdvClientesNaoTotalizador" AutoGenerateColumns="False" runat="server"
                EnableModelValidation="True">
                <Columns>
                    <asp:BoundField DataField="CodCliente" HeaderText="CodCliente" />
                    <asp:BoundField DataField="DesCliente" HeaderText="DesCliente" />
                    <asp:BoundField DataField="CodSubcliente" HeaderText="CodSubcliente" />
                    <asp:BoundField DataField="DesSubcliente" HeaderText="DesSubcliente" />
                    <asp:BoundField DataField="CodPtoServicio" HeaderText="CodPtoServicio" />
                    <asp:BoundField DataField="DesPtoServicio" HeaderText="DesPtoServicio" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div id="modal1" class="modal1">
        <ucPopup:Certificado ID="ucBusquedaCliente" EsModal="true" AutoAbrirPopup="false"
            runat="server" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
</asp:Content>
