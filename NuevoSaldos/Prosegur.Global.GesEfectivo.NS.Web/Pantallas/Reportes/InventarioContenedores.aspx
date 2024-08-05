<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="InventarioContenedores.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.InventarioContenedores" %>

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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <div style="text-align: center">
        <ns:Boton ID="btnGenerarReporte" runat="server" Enabled="true" Text="F4 Grabar" ImageUrl="~/App_Themes/Padrao/css/img/iconos/atajo_general.png"
            TeclaAtalho="F4" />
    </div>
</asp:Content>

