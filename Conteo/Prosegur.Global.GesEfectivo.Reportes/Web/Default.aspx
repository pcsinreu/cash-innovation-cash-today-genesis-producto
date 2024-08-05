<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.Reportes.Web._Default" MasterPageFile ="~/Master/Master.Master" %>
<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register assembly="Prosegur.Web" namespace="Prosegur.Web" tagprefix="pro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Reportes</title>
    <style type="text/css">
        .style2
        {
        	font-size:medium;
        	font-family:Arial;
        }
        .style3
        {
            display: block;
            font-weight: bold;
            font-size: 12px;
            height: 31px;         
            padding-left: 10px;
            padding-right: 10px;
            padding-top: 1px;
            background-image: url('imagenes/titulo01_bg.jpg');
            background-repeat: repeat-x;
        }
     </style>    
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>   
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
        </table>   
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>