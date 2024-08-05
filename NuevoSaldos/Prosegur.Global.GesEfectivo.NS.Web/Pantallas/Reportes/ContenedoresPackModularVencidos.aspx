<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="ContenedoresPackModularVencidos.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ContenedoresPackModularVencidos" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Src="~/Controles/ucFiltroDivisas.ascx" TagName="ucFiltroDivisas" TagPrefix="ns" %>
<%@ Register Src="~/Controles/ucRadioButtonList.ascx" TagName="ucRadioButtonList" TagPrefix="ns" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="ui-panel-titlebar" style="margin-bottom: 2px; padding-left: 20px;">
            <asp:Label ID="lblSubTitulo" runat="server" Style="color: #767676 !important; font-size: 9pt;" />
        </div>
<%--        <div style="width: 180px;">            
            <b><asp:Label ID="lblTipoSector" runat="server"></asp:Label></b><br />
            <asp:DropDownList ID="ddlTipoSector" runat="server" Style="width: 160px"></asp:DropDownList>
        </div>--%>
        <asp:UpdatePanel ID="upSector" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder runat="server" ID="phSector"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="upCliente" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="upCanal" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder runat="server" ID="phCanal"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="upTipoContenedor" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="dvUsarFloat">
                    <div>
                        <asp:PlaceHolder runat="server" ID="phTipoContenedor"></asp:PlaceHolder>
                    </div>
                    <div style="height: 20px; color: #5e5e5e;">
                        <asp:Label ID="lblFechaVencimentoDesde" runat="server" Text="Fecha Vencimento Desde" />
                        <br />
                        <asp:TextBox ID="txtFechaVencimentoDesde" SkinID="filter-textbox" runat="server" Height="10px" Width="118px" />
                        <br />
                    </div>
                    <div style="height: 20px; color: #5e5e5e;">
                        <asp:Label ID="lblFechaVencimentoHasta" runat="server" Text="Fecha Vencimento Hasta" />
                        <br />
                        <asp:TextBox ID="txtFechaVencimentoHasta" SkinID="filter-textbox" runat="server" Height="10px" Width="118px" />
                        <br />
                    </div>
                    <div style="padding-top: 10px">
                        <b>
                            <asp:CheckBox runat="server" ID="chkNoConsiderarHijos" Text="No considerar sectores hijos" /></b>
                    </div>
                    <div>
                        <b>
                            <ns:ucRadioButtonList ID="ucDiscriminarPor" runat="server" Titulo="Discriminar por" />
                        </b>
                    </div>
                    <div>
                        <b>
                            <ns:ucRadioButtonList ID="ucFormato" runat="server" />
                        </b>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
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
