<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucEfectivo.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucEfectivo" EnableTheming="true" %>
<%@ Register Src="~/Controles/Popup.ascx" TagName="Popup" TagPrefix="ns" %>
<%@ Register src="ucDivisaEfectivo.ascx" tagname="ucDivisaEfectivo" tagprefix="uc1" %>
<fieldset class="ui-fieldset ui-fieldset-toggleable">
    <legend class="ui-fieldset-legend ui-corner-all ui-state-default"><span class="ui-fieldset-toggler ui-icon ui-icon-minusthick">
    </span>
        <asp:Label ID="lblFiltroDivisa" runat="server" Text="Efectivo" /></legend>
    <div class="ui-fieldset-content">
        <div id="divucDivisas" runat="server">
            <uc1:ucDivisaEfectivo ID="_ucDivisaEfectivo" runat="server" />
        </div>
        <asp:UpdatePanel ID="upPopupAgregar" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Button ID="btnPopupAgregar" SkinID="filter-button" runat="server" Text="Agregar"
                AutoPostBack="true" />
            <ns:Popup ID="popupBlank" EsModal="true" AutoAbrirPopup="false" runat="server" Width="500"
                Height="430" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnPopupAgregar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    </div>
</fieldset>
