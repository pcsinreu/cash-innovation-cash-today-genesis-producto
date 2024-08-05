<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="Documento.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.Documento" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/ValidacionClasificacion.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <asp:PlaceHolder ID="phDatosDocumento" runat="server"></asp:PlaceHolder>
        <asp:UpdatePanel ID="upCuentaOrigen" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder ID="phCuentaOrigen" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="upCuentaDestino" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder ID="phCuentaDestino" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="uppElemento" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:PlaceHolder ID="phElemento" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="clear:both;"></div>
        <asp:PlaceHolder ID="phInfAdicionales" runat="server"></asp:PlaceHolder>
        <div style="clear:both;"></div>
        <asp:UpdatePanel ID="uppValores" runat="server">
            <ContentTemplate>
                <asp:PlaceHolder ID="phValores" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="clear:both;"></div>
        <asp:UpdatePanel ID="upSaldo" runat="server">
            <ContentTemplate>
                <asp:PlaceHolder ID="phSaldo" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="clear:both;"></div>
        <asp:UpdatePanel ID="uppInfContenedor" runat="server">
            <ContentTemplate>
                <asp:PlaceHolder ID="phInfContenedor" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="clear:both;"></div>
        <div id="dv_MensajeExterno" runat="server">
            <div class="ui-panel-titlebar">
                <asp:Label ID="lblTitulo_MensajeExterno" runat="server" Text="Mensaje Externa" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
            </div>
            <div style="width: 100%; margin-left: 5px; margin-top: 5px; font-size: 12px; font-weight: bold; color: #767676;">
                <div style="width: 100%;">
                    <div style="width: 100%;">
                        <asp:Label ID="lblMensajeExterno" runat="server" />
                    </div>
                    <div class="dvclear"></div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <asp:PlaceHolder ID="phAcciones" runat="server"></asp:PlaceHolder>
</asp:Content>
