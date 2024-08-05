<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="InventarioContenedoresConsultar.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.InventarioContenedoresConsultar" %>

<%@ Import Namespace="Prosegur.Framework.Dicionario" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Src="~/Controles/ucFiltroDivisas.ascx" TagName="ucFiltroDivisas" TagPrefix="ns" %>
<%@ Register Src="~/Controles/ucRadioButtonList.ascx" TagName="ucRadioButtonList" TagPrefix="ns" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="ui-panel-titlebar" style="margin-bottom: 2px; padding-left: 20px;">
            <asp:Label ID="lblSubTitulo" runat="server" Style="color: #767676 !important; font-size: 9pt;" />
        </div>
        <asp:UpdatePanel ID="upSector" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder runat="server" ID="phSector"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="upCliente" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="dvUsarFloat">
            <div style="height: 20px; color: #5e5e5e;">
                <asp:Label ID="lblCodInventario" runat="server" Text="Fecha/Hora Armado Hasta" />
                <br />
                <asp:TextBox ID="txtCodInventario" SkinID="filter-textbox" runat="server" Height="10px" Width="118px" />
                <br />
            </div>
            <div id="dvFechadesde" runat="server" style="display: block; height: 20px; color: #5e5e5e;">
                <asp:Label ID="lblFechaInventarioDesde" runat="server" Text="Fecha/Hora Armado Desde" />
                <br />
                <asp:TextBox ID="txtFechaInventarioDesde" SkinID="filter-textbox" runat="server" Height="10px" Width="118px" />
                <br />
            </div>
            <div id="dvFechaHasta" runat="server" style="display: block; height: 20px; color: #5e5e5e;">
                <asp:Label ID="lblFechaInventarioHasta" runat="server" Text="Fecha/Hora Armado Hasta" />
                <br />
                <asp:TextBox ID="txtFechaInventarioHasta" SkinID="filter-textbox" runat="server" Height="10px" Width="118px" />
                <br />
            </div>
            <div style="padding-top: 10px">
                <b>
                    <asp:CheckBox runat="server" ID="chkNoConsiderarHijos" Text="No considerar sectores hijos" /></b>
            </div>
            <div>
                <b>
                    <ns:ucRadioButtonList ID="ucDiscriminarPor" runat="server" Titulo="Discriminar por" />
                </b>
            </div>
            <div>
                <b>
                    <ns:ucRadioButtonList ID="ucFormato" runat="server" />
                </b>
            </div>
        </div>
        <div class="dvclear">
        </div>
        <div style="margin: 0px 25px 10px 0px; height: auto;">
            <asp:Button ID="btnBuscar" runat="server" class="ui-button" Style="display: block; height: 20px; padding: 0px !important; width: 100px; margin: 1px;" />
        </div>
        <div class="dvclear">
        </div>
        <div id="dvResultadoBusqueda" runat="server" class="ui-panel-titlebar" style="margin-bottom: 2px; padding-left: 20px;">
            <asp:Label ID="lblResultadosBusqueda" runat="server" Style="color: #767676 !important; font-size: 9pt;" />
        </div>
        <asp:GridView ID="gdvContenedor" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataKeyNames="CodigoInventario" AllowSorting="True" Visible="true"
            EnableModelValidation="True" BorderStyle="None" EmptyDataRowStyle-BorderWidth="5" OnPageIndexChanging="gdv_PageIndexChanging">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:RadioButton ID="rbSelecionado" ValidationGroup="rbSelecionado" GroupName="rbSelecionado" runat="server" OnCheckedChanged="rbSelecionado_CheckedChanged" AutoPostBack="true" />
                    </ItemTemplate>
                    <HeaderStyle Width="10px" />
                    <ItemStyle Width="10px" />
                    <FooterStyle Width="10px" />
                </asp:TemplateField>
                <asp:BoundField DataField="Sector" HeaderText="Sector" ReadOnly="True">
                    <HeaderStyle Width="75px" />
                    <ItemStyle Width="75px" />
                </asp:BoundField>
                <asp:BoundField DataField="Cliente" HeaderText="Cliente" ReadOnly="True">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="CodigoInventario" HeaderText="Codigo Inventario" ReadOnly="True">
                    <HeaderStyle Width="75px" />
                    <ItemStyle Width="75px" />
                </asp:BoundField>
                <asp:BoundField DataField="FechaInventario" HeaderText="Fecha/Hora Inventario" ReadOnly="True">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="CantContTeorico" HeaderText="CantContTeorico" ReadOnly="True">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="CantContInventariados" HeaderText="CantContTeorico" ReadOnly="True">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="Diferencia" HeaderText="Diferencia" ReadOnly="True">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" />
                </asp:BoundField>
            </Columns>
            <PagerSettings Mode="NumericFirstLast" />
            <EmptyDataTemplate>
                <div class="EmptyData">
                    <span>
                        <%# Tradutor.Traduzir("lblSemRegistro")%> 
                    </span>
                </div>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <div style="text-align: center">
        <ns:Boton ID="btnGenerarReporte" runat="server" Enabled="true" Text="F4 Grabar" ImageUrl="~/App_Themes/Padrao/css/img/iconos/atajo_general.png"
            TeclaAtalho="F4" />
    </div>
</asp:Content>

