<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="InformacionNuevoSaldos.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.InformacionNuevoSaldos" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Src="~/Controles/Boton.ascx" TagName="Boton" TagPrefix="uc1" %>
<%@ Register TagPrefix="info" Namespace="Prosegur.Genesis.Comon.Pantallas.Controles" Assembly="Prosegur.Genesis.Comon" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="ui-panel-titlebar">
            <asp:Label ID="lblTitulo" runat="server" Text="[titulo]" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
        </div>
        <div style="width: 100%; margin-left: 5px; margin-top: 5px;">
            <info:ucInformacionGenesis ID="informacionGenesis" runat="server" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <center>
        <table style="border-collapse: collapse;">
            <tr>
                <td>
                    <uc1:Boton ID="btnExportar" runat="server" Text="[InformacionGenesis_btnExportar]" ImageUrl="~/App_Themes/Padrao/css/img/button/gravar.png" OnClick="btnExportar_Click"/>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
