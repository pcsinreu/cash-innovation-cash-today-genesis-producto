<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master"
    CodeBehind="HistoricoSaldos.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.HistoricoSaldos" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Src="~/Controles/ucFiltroDivisas.ascx" TagName="ucFiltroDivisas" TagPrefix="ns" %>
<%@ Register Src="~/Controles/ucRadioButtonList.ascx" TagName="ucRadioButtonList" TagPrefix="ns" %>
<%@ Register Src="~/Controles/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="ui-panel-titlebar" style="margin-bottom: 2px; padding-left: 20px;">
            <asp:Label ID="lblSubTitulo" runat="server" Text="Saldos" Style="color: #767676 !important; font-size: 9pt;" />
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
                <div style="height: 10px;"></div>
                <div style="height: 20px;">
                    <asp:CheckBox ID="chkTodosNiveis" runat="server" Checked="false" Text="Considerar todos los niveles" />
                </div>                
                <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>                               
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="height: 20px;color: #5e5e5e;">
            <asp:Label ID="lblFechaSaldo" runat="server" Text="Fecha" />
            <br />
            <asp:TextBox ID="txtFechaSaldo" SkinID="filter-textbox" runat="server" Height="10px" Width="118px" />
            <br />
        </div>              
        <div style="height: 20px;"></div>
        <div>
            <asp:PlaceHolder runat="server" ID="phUcFiltroDivisas">
                <ns:ucFiltroDivisas ID="ucFiltroDivisas" runat="server" Titulo="Seleción de divisas"
                    MostrarOpcionDivisasInactivas="true" />
            </asp:PlaceHolder>
        </div>
        <div class="dvclear">
        </div>
        <div class="dvclear">
        </div>
        <div class="dvUsarFloat">
            <div>
                <fieldset>
                    <legend runat="server" id="lgTipoValor">Tipo valor</legend>
                    <asp:RadioButton ID="rbTiposDatosAmbos" runat="server" Checked="true" GroupName="TiposDatos" Text="Regresar Valores de Efectivos y de Medios de Pago" /><br />
                    <asp:RadioButton ID="rbTipoDatosEfectivos" runat="server" GroupName="TiposDatos" Text="Regresar Sólo Valores de Efectivos" /><br />
                    <asp:RadioButton ID="rbTipoDatosMediosPago" runat="server" GroupName="TiposDatos" Text="Regresar Sólo Valores de Medios de Pago" /><br />
                </fieldset>
            </div>
            <div>
                <asp:CheckBox runat="server" ID="chkDetalharSetor" Text="Discriminar por Setor" />
            </div>
            <div>
                <asp:CheckBox runat="server" ID="chkDetalharDivisa" Text="Detalhar Divisas" />
            </div>
            <div>
                <asp:CheckBox runat="server" ID="chkClienteSemSaldo" Text="Considerar Clientes sem Saldos" />
            </div>

            <div>
                <ns:ucRadioButtonList ID="ucFormato" runat="server" Titulo="Formato" />
            </div>
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
