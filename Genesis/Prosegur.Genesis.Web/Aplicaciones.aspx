<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master"
    CodeBehind="Aplicaciones.aspx.vb" Inherits="Prosegur.Genesis.Web.Aplicaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        // script que acompanha o timeout da session
        window.setTimeout(function(){
            location.href = 'Default.aspx';
        }, <%= System.Web.HttpContext.Current.Session.Timeout * 60 * 1000 %>);
    </script>
    <script type="text/javascript" src="./_Scripts/Script_PageAplicaciones.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div  style="width:auto; margin:0 auto; position:relative; vertical-align:middle;">
    <div class="page valignMiddleWrapper" style="background-size: auto;">
        <div class="versionAplicacionLista">
            <img alt="Genesis" src="_Imagens/genesis.png" /><br />
            <asp:Label runat="server" ID="lblVersao"></asp:Label>
        </div>
        <div id="dvListaAplicaciones" class="appsbox" style="display:none;">
        </div>
        <div style="clear:both;"></div>
    </div>
</div>
</asp:Content>
