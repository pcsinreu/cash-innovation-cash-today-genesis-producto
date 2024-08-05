<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web._Default"
    EnableEventValidation="false" MasterPageFile="~/Master/Master.Master" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ocultarExibir() {
            $("#DivFiltros").toggleClass("legend-collapse");
            $(".accordion").slideToggle("fast");
        };
        function ManterFiltroAberto() {
            $("#DivFiltros").addClass("legend-expand");
            $(".accordion").show();
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div id="Filtros" style="display: none;">
            <fieldset id="ExpandCollapseDiv" class="ui-fieldset ui-widget ui-widget-content ui-corner-all ui-fieldset-toggleable">
                <legend class="ui-fieldset-legend ui-corner-all ui-state-default ui-state-active">
                    <div id="DivFiltros" class="legend-expand" onclick="ocultarExibir();">
                        <asp:Label ID="lblTitulo" CssClass="legent-text" runat="server">Filtrar Clientes</asp:Label>
                    </div>
                </legend>
                <div class="accordion" style="height: 30px; background-color: #00bfff; display: none;">
                   
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
