<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterModal.Master" CodeBehind="OrdenesDeServicioBillingPopup.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.OrdenesDeServicioBillingPopup" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>

<asp:Content runat="server" ContentPlaceHolderID="head">

    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <style type="text/css">
        .style1 {
            width: 160px;
        }
    </style>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content-modal">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
               <iframe runat="server" id="ifNotificacion" style="width: 100%; height: 600px; max-width: 1300px;"></iframe>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <div style="position: fixed; bottom: 0; width: 100%;">
    </div>
</asp:Content>
