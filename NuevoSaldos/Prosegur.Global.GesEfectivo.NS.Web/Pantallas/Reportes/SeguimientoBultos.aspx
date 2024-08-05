<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SeguimientoBultos.aspx.vb" MasterPageFile="~/Master/Master.Master" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.SeguimientoBultos" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Src="~/Controles/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc1" %>
<%@ Register Src="~/Controles/ucRadioButtonList.ascx" TagName="ucRadioButtonList"
    TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content">
        <div class="ui-panel-titlebar" style="margin-bottom: 2px; padding-left: 20px;">
            <asp:Label ID="lblSubTitulo" runat="server" Text="Seguimiento de Bultos" Style="color: #767676 !important; font-size: 9pt;" />
        </div>

        <div class="dvUsarFloat">

            <div style="width: 150px;">
                <uc1:ucTextBox ID="Codigo" runat="server" Tamano="38" Titulo="Codigo" />
            </div>
            <div style="width: 300px;">
                <uc2:ucRadioButtonList ID="ucListaFiltrarPor" runat="server" Titulo="Filtro" />
            </div>
        </div>
        <div class="dvUsarFloat">
            <div style="width: 100px;">
                <uc2:ucRadioButtonList ID="ucFormato" runat="server" Titulo="Formato" />
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <div style="text-align: center">
        <ns:Boton ID="btnGenerarReporte" runat="server" Enabled="true" Text="F4 Grabar" ImageUrl="~/App_Themes/Padrao/css/img/iconos/atajo_general.png"
            TeclaAtalho="F4" />
    </div>
</asp:Content>
