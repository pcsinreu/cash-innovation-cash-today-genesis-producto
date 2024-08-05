<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master"
    CodeBehind="ValidacionEjecucion.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ValidacionEjecucion" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario" %>
<%@ Register Src="~/Controles/PopupCertificados.ascx" TagName="Certificado" TagPrefix="ucPopup" %>
<%@ Register Src="~/Controles/ucLista.ascx" TagName="Lista" TagPrefix="ucLista" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="ui-panel-titlebar">
            <asp:Label ID="lblTituloEntrada" runat="server" Text="Filtros" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
        </div>
        <div style="width: 100%; margin-left: 5px; margin-top: 5px;">
            <div class="dvUsarFloat">
                <asp:Label ID="lblAviso" runat="server" Style="color: #767676; font-size: 9pt; font-style: italic;"></asp:Label>
                <div class="dvclear"></div>
                <div id="dvClienteTotalizador" runat="server">
                    <asp:Label ID="lblClienteTotalizador" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="lblDatoClienteTotalizador" runat="server" Text="" CssClass="valor"></asp:Label>
                </div>
                <div id="dvTipoCertificado" runat="server">
                    <asp:Label ID="lblTipoCertificado" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="lblDatoTipoCertificado" runat="server" Text="" CssClass="valor"></asp:Label>
                </div>
                <div id="dvFecha" runat="server">
                    <asp:Label ID="lblFecha" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="lblDatoFecha" runat="server" Text="" CssClass="valor"></asp:Label>
                </div>
                <div id="dvIdentificador" runat="server">
                    <asp:Label ID="lblIdentificador" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="lblDatoIdentificador" runat="server" Text="" CssClass="valor"></asp:Label>
                </div>
                <div id="dvUltimoEjecucion" runat="server" style="height:auto">
                    <asp:Label ID="lblUltimoEjecucion" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="lblDatoUltimoEjecucion" runat="server" Text="" CssClass="valor"></asp:Label>
                </div>
                <div class="dvclear"></div>
                <div style="margin-top: 0px; width: auto !important; height:auto !important;">
                    <div id="dvListaDelegaciones" runat="server" style="margin-top: 0px; margin-right: 15px; width: auto !important;">
                        <ucLista:Lista runat="server" ID="listaDelegaciones" titulo="Delegaciones" modo="Consulta" />
                    </div>
                    <div id="dvListaSectores" runat="server" style="margin-top: 0px; margin-right: 15px; width: auto !important;">
                        <ucLista:Lista runat="server" ID="listaSectores" titulo="Sectores" modo="Consulta" />
                    </div>
                    <div id="dvListaSubCanales" runat="server" style="margin-top: 0px; margin-right: 15px; width: auto !important;">
                        <ucLista:Lista runat="server" ID="listaSubCanales" titulo="SubCanales" modo="Consulta" />
                    </div>                    
                </div>
                <div class="dvclear"></div>                
                <div style="margin-top: 0px; width: auto !important; height:auto !important;">
                    <br />
                    <asp:Label ID="lblCliSubCliPtoTotalizaSaldo" runat="server" Style="color: #767676; font-size: 9pt; font-style: italic;"></asp:Label><br />
                    <br />
                    <div id="dvListaClientes" runat="server" style="margin-top: 0px; margin-right: 15px; width: auto !important;">
                        <ucLista:Lista runat="server" ID="listaClientes" titulo="Clientes" modo="Consulta" />
                    </div>
                    <div id="dvListaSubClientes" runat="server" style="margin-top: 0px; margin-right: 15px; width: auto !important;">
                        <ucLista:Lista runat="server" ID="listaSubClientes" titulo="SubClientes" modo="Consulta" />
                    </div>
                    <div id="dvListaPuntosServicio" runat="server" style="margin-top: 0px; margin-right: 15px; width: auto !important;">
                        <ucLista:Lista runat="server" ID="listaPuntosServicio" titulo="Puntos de Servicio" modo="Consulta" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <center>
        <table>
            <tr>
                <td>
                    <ns:Boton ID="btnCancelar" runat="server" Text="F8 Eliminar" ImageUrl="~/App_Themes/Padrao/css/img/button/borrar.png"
                        TeclaAtalho="F8" />
                </td>
                <td>
                    <ns:Boton ID="btnEjecutarCertificado" runat="server" Text="F9 Executar" ImageUrl="~/App_Themes/Padrao/css/img/iconos/icon_active.png"
                        TeclaAtalho="F9" />
                </td>
            </tr>
        </table>
    </center>
</asp:Content>

