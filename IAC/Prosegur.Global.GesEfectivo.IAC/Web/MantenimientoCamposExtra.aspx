<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterModal.Master" CodeBehind="MantenimientoCamposExtra.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoCamposExtra" %>

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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblSubTitulo" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabela_campos">
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblCliente" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCliente" Enabled="false" runat="server" Width="555px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblSucliente" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSubcliente" Enabled="false" runat="server" Width="555px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblPtoServicio" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPtoServicio" Enabled="false" runat="server" Width="555px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div style="margin-top: 10px; min-height:100px">
                    <asp:UpdatePanel ID="upDatosBanc" runat="server">
                        <ContentTemplate>
                            <asp:PlaceHolder ID="phDatosBanc" runat="server"></asp:PlaceHolder>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <table class="tabela_campos" style="margin-top: 10px;">
                    <tr>
                        <td style="text-align: center">
                            <asp:Button runat="server" ID="btnAddDatosBancarios" CssClass="ui-button" Width="180" />
                        </td>
                    </tr>
                </table>
                <script type="text/javascript">
                    //Script necessário para evitar que dê erro ao clicar duas vezes em algum controle que esteja dentro do updatepanel.
                    var prm = Sys.WebForms.PageRequestManager.getInstance();
                    prm.add_initializeRequest(initializeRequest);

                    function initializeRequest(sender, args) {
                        if (prm.get_isInAsyncPostBack()) {
                            args.set_cancel(true);
                        }
                    }
                </script>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <div style="position: relative; bottom: 0; width: 100%; margin-top: 40px;">
        <table class="tabela_campos">
            <tr>
                <td style="text-align: center">
                    <asp:Button runat="server" ID="btnGrabar" CssClass="ui-button" Width="100" />
                    <asp:Button runat="server" ID="btnCancelar" CssClass="ui-button" Width="100" />
                    <div class="botaoOcultar">
                        <asp:Button runat="server" ID="btnConsomeDatosBancarios" CssClass="btn-excluir" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
