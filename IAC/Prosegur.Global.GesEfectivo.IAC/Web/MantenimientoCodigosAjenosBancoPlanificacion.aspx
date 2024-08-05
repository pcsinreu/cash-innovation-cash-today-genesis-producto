<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoCodigosAjenosBancoPlanificacion.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoCodigosAjenosBancoPlanificacion" EnableEventValidation="false" MasterPageFile="Master/MasterModal.Master" %>

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
                        <td class="tamanho_celula"></td>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblCodGenesis" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblCodAjeno" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblDesAjeno" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblCliente" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodCliente" Enabled="false" runat="server" Width="150px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodAjenoCliente" runat="server" MaxLength="25" Width="150px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDesAjenoCliente" runat="server" MaxLength="50" Width="315px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
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
    <div style="position: absolute; bottom: 0; width: 100%;">
        <table class="tabela_campos">
            <tr>
                <td style="text-align: center">
                    <asp:Button runat="server" ID="btnGrabar" CssClass="ui-button" Width="100" />
                    <asp:Button runat="server" ID="btnCancelar" CssClass="ui-button" Width="100" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
