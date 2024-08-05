<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="InformacionIAC.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.InformacionIAC" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register TagPrefix="info" Namespace="Prosegur.Genesis.Comon.Pantallas.Controles" Assembly="Prosegur.Genesis.Comon" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="content">
                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblSubTitulo" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <div>
                    <info:ucInformacionGenesis ID="informacionGenesis" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="cphBotonesRodapie">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <center>
                <table>
                    <tr align="center">
                        <td>
                           <asp:Button runat="server" ID="btnExportar" CssClass="btn-salvar" OnClick="btnExportar_Click"/>
                        </td>
                    </tr>
                </table>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
