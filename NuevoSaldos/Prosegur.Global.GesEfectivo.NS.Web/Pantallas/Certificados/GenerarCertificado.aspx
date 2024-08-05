<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="GenerarCertificado.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.GenerarCertificado" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario" %>
<%@ Register Src="~/Controles/PopupCertificados.ascx" TagName="Certificado" TagPrefix="ucPopup" %>
<%@ Register Src="~/Controles/ucLista.ascx" TagName="Lista" TagPrefix="ucLista" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content">
        <div runat="server" id="dvEntrada">
            <div class="ui-panel-titlebar">
                <asp:Label ID="lblTituloEntrada" runat="server" Text="Filtros" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
            </div>
            <div style="width: 100%; margin-left: 5px; margin-top: 5px;">
                <div class="dvUsarFloat">
                    <div id="dvTipoCertificado" runat="server" style="margin-top: 0px;">
                        <b><asp:Label ID="lblTipoCertificado" runat="server" Text="Tipo Certificado"></asp:Label></b><br />
                        <asp:DropDownList ID="ddlTipoCertificado" runat="server"></asp:DropDownList>
                    </div>
                    <div class="dvclear"></div>
                    <div id="dvCliente" runat="server" style="margin-top: 0px;">
                        <asp:UpdatePanel ID="upCliente" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:PlaceHolder ID="phCliente" runat="server"></asp:PlaceHolder>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div id="dvFecha" runat="server" style="margin-top: 0px;">
                        <b><asp:Label ID="lblFecha" runat="server" Text=""></asp:Label></b><br />
                        <asp:TextBox ID="txtFecha" runat="server" />
                    </div>
                    <div class="dvclear"></div>
                    
                    <div id="dvListaDelegaciones" runat="server" style="margin-top: 0px; width: auto !important;">
                        <ucLista:Lista runat="server" ID="listaDelegaciones" modo="Alta" />                        
                    </div>
                    <div id="dvListaSectores" runat="server" style="margin-top: 0px; width: auto !important;">
                        <ucLista:Lista runat="server" ID="listaSectores" modo="Alta" />                        
                    </div>
                    <div id="dvListaSubCanales" runat="server" style="margin-top: 0px; width: auto !important;">
                        <ucLista:Lista runat="server" ID="listaSubCanales" modo="Alta" />                        
                    </div>
                    <div class="dvclear"></div>
                </div>
            </div>
        </div>        
    </div>
    <ucPopup:Certificado ID="popupCuestion" AutoAbrirPopup="false" Width="650" EsModal="false" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <center>
        <table>
            <tr>
                <td>                   
                    <ns:Boton ID="btnConsultaConfiguracaoSaldo" runat="server" Enabled="false" Text="" ImageUrl="~/App_Themes/Padrao/css/img/iconos/atajo_general.png" TeclaAtalho="F4" />                    
                </td>
                <td>                    
                    <ns:Boton ID="btnValidarCertificado" runat="server" Text="" ImageUrl="~/App_Themes/Padrao/css/img/iconos/icon_active.png" TeclaAtalho="F10" />                    
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
