<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoConfiguracionParametro.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoConfiguracionParametro"
    EnableEventValidation="false" MasterPageFile="~/Master/Master.Master" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Configuracion Parámetro</title>
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="ui-panel-titlebar">
            <asp:Label ID="lblSubTitulosConfiguracionParametros" CssClass="ui-panel-title" runat="server"></asp:Label>
        </div>
        <asp:UpdatePanel ID="upnConfiguracionParametro" runat="server">
            <ContentTemplate>
                <dxtc:ASPxPageControl ID="tabConfiguracionParametro" runat="server" EnableDefaultAppearance="true"
                    EnableHierarchyRecreation="true" ViewStateMode="Enabled" ActiveTabIndex="0" TabStyle-VerticalAlign="Top" >
                </dxtc:ASPxPageControl>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="cphBotonesRodapie">
    <center>
        <table>
            <tr align="center">
                <td>
                    <asp:Button runat="server" ID="btnAceptar" CssClass="btn-salvar" ValidationGroup="none"/>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn_cancelar" ValidationGroup="none"/>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
