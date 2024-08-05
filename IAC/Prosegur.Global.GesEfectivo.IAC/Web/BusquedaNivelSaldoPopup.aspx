<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaNivelSaldoPopup.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaNivelSaldoPopup" MasterPageFile="Master/MasterModal.Master" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>

<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>IAC - Búsqueda de Nivel Saldo</title>

    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <base target="_self" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content-modal">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:UpdatePanel ID="updUcClienteUc" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                    <ContentTemplate>
                        <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table class="tabela_campos">
                    <tr>
                        <td>
                            <table class="tabela_campos" style="margin: 0px !important;">
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblSubCanal" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <table style="margin: 0px !important;">
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlSubCanal" runat="server" Width="163px" ReadOnly="True" AutoPostBack="true" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:DropDownList>
                                                    <asp:CustomValidator ID="csvSubCanal" runat="server" ErrorMessage="" ControlToValidate="ddlSubCanal" Text="*"></asp:CustomValidator>
                                                    <td>&nbsp;&nbsp;</td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table style="margin: 0px !important;">
                    <tr>
                        <td style="width: 25%; text-align: center">
                            <asp:Button runat="server" ID="btnAceptar" CssClass="ui-button" Width="130" />
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnLimpar" CssClass="ui-button" Width="130" />
                        </td>
                    </tr>
                </table>

                <script src="JS/FuncaoAjax.js" type="text/javascript"></script>
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
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAceptar" />
                <asp:AsyncPostBackTrigger ControlID="btnLimpar" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

