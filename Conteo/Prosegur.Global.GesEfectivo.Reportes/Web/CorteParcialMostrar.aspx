<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CorteParcialMostrar.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.CorteParcialMostrar" %>
<%@ Register Src="~/Controles/Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc" %>
<%@ Register Src="~/Controles/Erro.ascx" TagName="Erro" TagPrefix="uc" %>
<%@ Register Src="~/Controles/Crystal.ascx" TagName="Report" TagPrefix="uc" %>

<HTTP-EQUIV="PRAGMA" content="NO-CACHE"></HTTP-EQUIV>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="<%=Page.ResolveUrl("~/App_Themes/Padrao/css/genesis.css")%>" rel="stylesheet" />
    <link href="<%=Page.ResolveUrl("~/App_Themes/Padrao/css/dhtmlgoodies_calendar.css")%>" rel="stylesheet" />
    <link href="<%=Page.ResolveUrl("~/App_Themes/Padrao/css/genesis_customization_theme.css")%>" rel="stylesheet" />
    <link href="<%=Page.ResolveUrl("~/App_Themes/Padrao/css/jquery-ui-1.10.3.custom.min.css")%>" rel="stylesheet" />
    <link href="<%=Page.ResolveUrl("~/App_Themes/Padrao/css/js_color_picker_v2.css")%>" rel="stylesheet" />

    <!-- SCRIPT -->
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Master/jquery-1.10.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Master/jquery-ui-1.10.3.custom.min.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Master/jquery-ui-timepicker-addon.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Master/date-en-US.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Master/genesis.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Master/ie10fix.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Master/shortcut.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Master/Funcoes.js")%>"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
         <div id="dvAlert" class="genesisAlert" style="display: none;">
            <div class="fundo"></div>
            <div id="dvAlertPanel" class="painel loading">
                <div id="dvAlertLabel" class="Label">Loading</div>
                <div id="dvAlertClose" class="Close" style="visibility:hidden;">
                    <button type="button" onclick="Javascript: genesisAlertError('','');">x</button>
                </div>
            </div>
            <div id="dvAlertErro" style="display: none;"></div>
        </div>
        <div class="page">
            <div class="header">
                <div class="ui-gn-menu">
                    <div class="menu-trigger">
                        <asp:LinkButton runat="server" ID="linkImagemMenu" CausesValidation="false">
                            <img src="<%=Page.ResolveUrl("~/App_Themes/Padrao/css/img/logo.png")%>" alt="" />
                        </asp:LinkButton>
                    </div>
                     <div class="menu-box" style="display: none !important;">
                            <div id="divMenu" runat="server" style="display: none !important;">
                                <ul id="menu" class="ui-menu-list ui-helper-reset" style="display: none !important;">
                                </ul>
                            </div>
                        </div>
                </div>
                <div class="top-navigarion">
                    <div class="page-title">
                        <asp:Label runat="server" ID="lblTitulo"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="layout-center" id="pnlConteudo">

         <uc:Report ID="rptCorteParcial" runat="server" Report="crCorteParcialTXT.rpt" />

        </div>
        <div class="ui-gn-panel-footer">
            <div class="ui-gn-version-panel">
                <div>
                    <asp:Label ID="lblVersao" runat="server"></asp:Label>
                </div>
            </div>
            <div class="ui-gn-panel-center ui-gn-footer-panel-simple-main-template">
            </div>
            <div class="ui-gn-institucional-panel">
            </div>
        </div>
    </form>
</body>
</html>


