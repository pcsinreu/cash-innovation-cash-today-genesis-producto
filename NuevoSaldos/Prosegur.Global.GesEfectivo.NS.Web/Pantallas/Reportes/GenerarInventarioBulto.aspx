<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="GenerarInventarioBulto.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.GenerarInventarioBulto" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>

<%@ Register Src="~/Controles/ucRadioButtonList.ascx" TagName="ucRadioButtonList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <fieldset class="ui-fieldset ui-fieldset-toggleable">
            <legend class="ui-fieldset-legend ui-corner-all ui-state-default"><span class="ui-fieldset-toggler ui-icon ui-icon-minusthick"></span>
                <asp:Label ID="lblFiltro" runat="server" Text="Inventário de Bultos" />
            </legend>
            <div class="ui-fieldset-content">
                <asp:UpdatePanel ID="upSector" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:PlaceHolder runat="server" ID="phSector"></asp:PlaceHolder>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="dvUsarFloat">
                    <div style="width: 98%; height: auto;">
                        <div>
                            <uc1:ucRadioButtonList ID="ucOrdenar" runat="server" Titulo="Ordenar por" />
                        </div>
                        <div>
                            <uc1:ucRadioButtonList ID="ucFormato" runat="server" Titulo="Ordenar por" />
                        </div>
                        <div style="height: 28px; padding-left: 30px;">
                            <br />
                            <asp:CheckBox runat="server" ID="chkDiscriminarSubSectores" Text="Discriminar SubSectores/Puestos" />
                        </div>
                        <div style="height: 28px; padding-left: 30px;">
                            <br />
                            <asp:CheckBox runat="server" ID="chkConsiderarBultoContenedor" Text="Considerar Bultos Contenidos en Contenedores" />
                        </div>
                    </div>
                    <div class="dvclear"></div>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <div style="text-align: center">
        <ns:Boton ID="btnGenerarReporte" runat="server" Enabled="true" Text="F4 Grabar"
            ImageUrl="~/App_Themes/Padrao/css/img/iconos/atajo_general.png" TeclaAtalho="F4" />
    </div>
</asp:Content>
