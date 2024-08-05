<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master"
    CodeBehind="Transacciones.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.Transacciones" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Src="~/Controles/ucFiltroDivisas.ascx" TagName="ucFiltroDivisas" TagPrefix="ns" %>
<%@ Register Src="~/Controles/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="ns" %>
<%@ Register Src="~/Controles/ucRadioButtonList.ascx" TagName="ucRadioButtonList" TagPrefix="ns" %>
<%@ Register Src="~/Controles/ucCheckBoxList.ascx" TagName="ucCheckBoxList" TagPrefix="ns" %>
<%@ Register Src="~/Controles/UcSeleccion.ascx" TagName="ucSeleccion" TagPrefix="ns" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".filtro").css("visibility", "visible");
            ReCargarDivisas(); ReCargarEfectivosMediosPago(); ReReglasControle();
        });
    </script>
    <div class="content filtro" style="visibility:hidden;">
        <div class="ui-panel-titlebar" style="margin-bottom: 2px; padding-left: 20px;">
            <asp:Label ID="lblSubTitulo" runat="server" Text="Transacciones" Style="color: #767676 !important; font-size: 9pt;" />
        </div>
        <asp:UpdatePanel ID="upSector" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder runat="server" ID="phSector"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="upCanal" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder runat="server" ID="phCanal"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="upCliente" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="dvUsarFloat">
            <div>
                <ns:ucTextBox runat="server" ID="ucFechaDesde" TipoInterno="FechaHora" Titulo="<% =Prosegur.Framework.Dicionario.Tradutor.Traduzir('057_fechahoradesde')%>" />
            </div>
            <div>
                <ns:ucTextBox runat="server" ID="ucFechaHasta" TipoInterno="FechaHora" Titulo="<%=Prosegur.Framework.Dicionario.Tradutor.Traduzir('057_fechahorahasta')%>" />
            </div>
            <div>
                <asp:Label ID="lblTituloFiltroFecha" runat="server"/>
                <br />
                <asp:DropDownList runat="server" ID="ddlFiltroFecha" Width="160">
                </asp:DropDownList>
            </div>
            <div class="dvclear"></div>
        </div>
        <div class="dvUsarFloat">
            <div>
                <fieldset id="fsNotificado" style="margin-top: -3px;">
                    <legend><asp:Label ID="lblNotificado" runat="server"/></legend>
                    <asp:RadioButton ID="rbNotificadoAmbos" runat="server" Text="Ambos" GroupName="Notificado" Checked="true" />
                    <asp:RadioButton ID="rbNotificado" runat="server" Text="Notificado" GroupName="Notificado" />
                    <asp:RadioButton ID="rbNoNotificado" runat="server" Text="No notificado" GroupName="Notificado" />
                </fieldset>
            </div>
            <div>
                <fieldset id="fsAcreditado" style="margin-top: -3px;">
                    <legend><asp:Label ID="lblAcreditado" runat="server"/></legend>
                    <asp:RadioButton ID="rbAcreditadoAmbos" runat="server" Text="Ambos" GroupName="Acreditado" Checked="true" />
                    <asp:RadioButton ID="rbAcreditado" runat="server" Text="Acredito" GroupName="Acreditado" />
                    <asp:RadioButton ID="rbNoAcreditado" runat="server" Text="No acreditado" GroupName="Acreditado" />
                </fieldset>
            </div>
            <div class="dvclear"></div>
        </div>
        <div>
            <table>
                <tr>
                    <td style="vertical-align:top">
                        <ns:ucCheckBoxList runat="server" ID="ucFormularios" SelecionarTodos="true" Titulo="<% =Prosegur.Framework.Dicionario.Tradutor.Traduzir('057_formularios')%>" />
                    </td>
                    <td style="vertical-align:top; padding-left:10px;">
                        <ns:ucSeleccion ID="ucColumnasAdicionales" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <ns:ucFiltroDivisas ID="ucFiltroDivisas" runat="server" Titulo="Seleción de divisas" MostrarOpcionDivisasInactivas="true" />
        </div>
        <div class="dvclear">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <div style="text-align: center">
        <ns:Boton ID="btnGenerarReporte" runat="server" Enabled="true" Text="F4 Grabar" ImageUrl="~/App_Themes/Padrao/css/img/iconos/atajo_general.png"
            TeclaAtalho="F4" />
    </div>
</asp:Content>
