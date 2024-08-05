<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="BusquedaCliente.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.BusquedaCliente" %>
<%@ Register Src="~/Controles/Popup.ascx" TagName="Popup" TagPrefix="ucPopup" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Label ID="lblCodigoCliente" runat="server" SkinID="form-label" Text="lblCodigoCliente" />
        <asp:TextBox ID="txtCodigoCliente" SkinID="form-textbox" runat="server" AutoPostBack="True" Width="80px" />
        <asp:ImageButton ID="imbBuscaCliente" runat="server" CssClass="ui-button" ImageUrl="~/App_Themes/Padrao/css/img/grid/search.png" />
        <asp:TextBox ID="txtDescricaoCliente" SkinID="form-textbox" runat="server" MaxLength="100" Width="150px" AutoPostBack="True" />
        
        <asp:Label ID="lblCodigoSubcliente" runat="server" SkinID="form-label" Text="lblCodigoCliente" />
        <asp:TextBox ID="txtCodigoSubcliente" SkinID="form-textbox" runat="server" AutoPostBack="True" Enabled="False" Width="80px" />
        <asp:ImageButton ID="imbBuscaSubcliente" runat="server" CssClass="ui-button" ImageUrl="~/App_Themes/Padrao/css/img/grid/search.png" Enabled="False" />
        <asp:TextBox ID="txtDescricaoSubcliente" SkinID="form-textbox" runat="server" MaxLength="100" Width="150px" AutoPostBack="True" Enabled="False" />
        
        <asp:Label ID="lblCodigoPuntoServicio" runat="server" SkinID="form-label" Text="lblCodigoPuntoServicio" />
        <asp:TextBox ID="txtCodigoPuntoServicio" SkinID="form-textbox" runat="server" AutoPostBack="True" Enabled="False" Width="80px" />
        <asp:ImageButton ID="imbBuscaPuntoServicio" runat="server" CssClass="ui-button" ImageUrl="~/App_Themes/Padrao/css/img/grid/search.png" Enabled="False" />
        <asp:TextBox ID="txtDescricaoPuntoServicio" SkinID="form-textbox" runat="server" MaxLength="100" Width="150px" AutoPostBack="True" Enabled="False" />
    </ContentTemplate>
</asp:UpdatePanel>
<ucPopup:Popup ID="popupBuscaCliente" EsModal="true" AutoAbrirPopup="false" runat="server" />
<ucPopup:Popup ID="popupBuscaSubcliente" EsModal="true" AutoAbrirPopup="false" runat="server" />
<ucPopup:Popup ID="popupBuscaPuntoServicio" EsModal="true" AutoAbrirPopup="false" runat="server" />
