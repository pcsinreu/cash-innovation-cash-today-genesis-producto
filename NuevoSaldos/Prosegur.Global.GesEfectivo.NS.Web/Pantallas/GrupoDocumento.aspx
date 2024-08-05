<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master"
    CodeBehind="GrupoDocumento.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.GrupoDocumento" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <asp:PlaceHolder ID="phDatosDocumento" runat="server">
        </asp:PlaceHolder>
        <asp:UpdatePanel ID="upCuentaOrigen" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder ID="phCuentaOrigen" runat="server">
                </asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="upCuentaDestino" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder ID="phCuentaDestino" runat="server">
                </asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="clear:both;"></div>
        <asp:UpdatePanel ID="uppListaValores" runat="server">
            <ContentTemplate>
                <asp:PlaceHolder ID="phListaValores" runat="server">
                </asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="clear:both;"></div>
        <asp:PlaceHolder ID="phInfAdicionales" runat="server">
        </asp:PlaceHolder>
        <div style="clear:both;"></div>
        <asp:UpdatePanel ID="uppContainerDocumentos" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder ID="phContainerDocumentos" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="clear:both;"></div>
        <asp:UpdatePanel ID="uppListaSimplificadaDeDocumentos" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder ID="phListaSimplificadaDeDocumentos" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="clear:both;"></div>
        <asp:UpdatePanel ID="uppListaContenedoresGrp" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder ID="phListaContenedoresGrp" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="clear:both;"></div>
    </div>
    <br />
    <br />
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <asp:PlaceHolder ID="phAcciones" runat="server"></asp:PlaceHolder>
</asp:Content>
