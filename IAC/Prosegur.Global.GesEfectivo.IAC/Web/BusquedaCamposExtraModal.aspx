<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterModal.Master" CodeBehind="BusquedaCamposExtraModal.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaCamposExtraModal" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>

<asp:Content runat="server" ContentPlaceHolderID="head">

    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <%--<script type="text/javascript">
        function ExibirLoading(mostrar) {
            if (mostrar)
                $('#divloadingCamposExtras').css('display', 'block');
            else
                $('#divloadingCamposExtras').css('display', 'none');
        }
    </script>--%>
    <style type="text/css">
        .style1 {
            width: 160px;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content-modal">
         <%--<div id="divloadingCamposExtras" class="AlertLoading" style="display: none;"></div>--%>
        <div class="ui-panel-titlebar">
            <asp:Label ID="lblCamposPatron" CssClass="ui-panel-title" runat="server"></asp:Label>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label ID="lblIACPatron" Text="IAC" runat="server" CssClass="label2" nowrap="false" Style="margin-left: 8px;" />
                <br />
                <asp:TextBox ID="txtCodigoIACPatron" runat="server" Enabled="false" Style="margin-left: 8px; margin-bottom: 10px;"></asp:TextBox>
                <div style="clear: both;"></div>
                <asp:PlaceHolder ID="phInfAdicionalesPatron" runat="server"></asp:PlaceHolder>
                <div style="clear: both;"></div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="ui-panel-titlebar">
            <asp:Label ID="lblCamposDinamicos" CssClass="ui-panel-title" runat="server"></asp:Label>
        </div>
        <asp:UpdatePanel ID="upIAC" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label ID="lblIAC" Text="IAC" runat="server" CssClass="label2" nowrap="false" Style="margin-left: 8px;" />
                <br />
                <asp:DropDownList ID="ddlIAC" runat="server" Width="225px" AutoPostBack="true" Style="margin-left: 8px; margin-bottom: 10px;" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                </asp:DropDownList>
                <div style="clear: both;"></div>
                <asp:PlaceHolder ID="phInfAdicionalesDinamico" runat="server"></asp:PlaceHolder>
                <div style="clear: both;"></div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <div style="position: absolute; bottom: 0; width: 100%;">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table class="tabela_campos">
                    <tr>
                        <td style="text-align: center">
                            <asp:Button runat="server" ID="btnAceptar" CssClass="ui-button" Width="100" />
                            <asp:Button runat="server" ID="btnCancelar" CssClass="ui-button" Width="100" />
                            <div class="botaoOcultar">
                                <asp:Button runat="server" ID="btnConsomeSelecionar" />
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
