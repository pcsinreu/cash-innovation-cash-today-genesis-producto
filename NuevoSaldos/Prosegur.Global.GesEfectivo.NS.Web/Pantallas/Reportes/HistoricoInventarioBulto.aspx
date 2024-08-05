<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="HistoricoInventarioBulto.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.HistoricoInventarioBulto" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>

<%@ Register Src="~/Controles/ucRadioButtonList.ascx" TagName="ucRadioButtonList" TagPrefix="uc1" %>
<%@ Register Src="~/Controles/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc2" %>
<%@ Register Src="~/Controles/ucListaInventario.ascx" TagPrefix="ucli" TagName="ucListaInventario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="ui-panel-titlebar" style="margin-bottom: 2px; padding-left: 20px;">
            <asp:Label ID="lblFiltro" runat="server" Text="Inventário de Bultos" Style="color: #767676 !important; font-size: 9pt;" />
        </div>
        <asp:UpdatePanel ID="upSector" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder runat="server" ID="phSector"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="dvUsarFloat">
            <div style="width: 98%; height: auto;">
                <div style="width: 180px;">
                    <uc2:ucTextBox ID="ucFechaHoraDesde" runat="server" Tamano="20" TipoInterno="FechaHora" />
                </div>
                <div style="width: 180px;">
                    <uc2:ucTextBox ID="ucFechaHoraHasta" runat="server" Tamano="20" TipoInterno="FechaHora" />
                </div>
                <div style="width: 150px;">
                    <uc2:ucTextBox ID="ucCodigoInventario" runat="server" Tamano="38" />
                </div>

                <div style="width: 230px;">
                    <uc1:ucRadioButtonList ID="ucOrdenar" runat="server" Titulo="Ordenar por" />
                </div>
                <div style="width: 230px;">
                    <uc1:ucRadioButtonList ID="ucFormato" runat="server" Titulo="Ordenar por" />
                </div>
                <div style="height: 28px; padding-left: 30px;">
                    <br />
                    <asp:CheckBox runat="server" ID="chkDiscriminarSubSectores" Text="Discriminar SubSectores/Puestos" />
                </div>
                <div class="dvclear"></div>
                <div style="width: 300px;">
                    <ucli:ucListaInventario runat="server" ID="ucListaInventario" />
                </div>
                <div class="dvclear"></div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <div style="text-align: center">
        <ns:Boton ID="btnGenerarReporte" runat="server" Enabled="true" Text="F4 Grabar"
            ImageUrl="~/App_Themes/Padrao/css/img/iconos/atajo_general.png" TeclaAtalho="F4" />
    </div>
</asp:Content>
