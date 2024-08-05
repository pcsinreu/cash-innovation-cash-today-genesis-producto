<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucSaldo.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucSaldo" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/js/knockout-3.4.0.js" />
        <asp:ScriptReference Path="~/js/knockout.mapping-latest.js" />     
        <asp:ScriptReference Path="~/js/knockout.validation.min.js" />
        <asp:ScriptReference Path="~/js/Localization/pt-BR.js" />
        <asp:ScriptReference Path="~/js/ucSaldo.js" />
    </Scripts>
</asp:ScriptManagerProxy>
<script type="text/javascript">
    function OnActiveTabChangedModificar(s, e) {
        pagecontrolDetallar.SetActiveTabIndex(e.tab.index);
    }
    function OnActiveTabChangedDetallar(s, e) {
        pagecontrolModificar.SetActiveTabIndex(e.tab.index);
    }
</script>

<asp:Panel ID="pnlUcSaldoEfectivoModificar" runat="server">
</asp:Panel>
<asp:Panel ID="pnlUcSaldoMPModificar" runat="server">
</asp:Panel>

<div id="divUcSaldoEfectivoMPModificar" runat="server" style="float: left; display: none;">

    <div class="ui-panel-titlebar">
        <asp:Label ID="lblTituloSaldoModificar" runat="server" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
    </div>

    <div style="color: #767676; padding: 5px 0px 0px 0px;">
        <dx:ASPxPageControl ID="pageControlEfectivoMPModificar" runat="server" ClientInstanceName="pagecontrolModificar">
            <ClientSideEvents ActiveTabChanged="OnActiveTabChangedModificar" />
        </dx:ASPxPageControl>
    </div>

</div>

<asp:Panel ID="pnlUcSaldoEfectivoDetallar" runat="server">
</asp:Panel>
<asp:Panel ID="pnlUcSaldoMPDetallar" runat="server">
</asp:Panel>

<div id="divUcSaldoEfectivoMPDetallar" runat="server" style="float: left; padding-left: 10px; display: none;">

    <div class="ui-panel-titlebar">
        <asp:Label ID="lblTituloSaldoDetallar" runat="server" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
    </div>

    <div style="color: #767676; padding: 5px 0px 0px 0px;">
        <dx:ASPxPageControl ID="pageControlEfectivoMPDetallar" runat="server" ClientInstanceName="pagecontrolDetallar">
            <ClientSideEvents ActiveTabChanged="OnActiveTabChangedDetallar" />
        </dx:ASPxPageControl>
    </div>

</div>

<iframe id=Defib src="MantenerSession.aspx" frameBorder=no width=0 height=0 runat="server"></iframe>