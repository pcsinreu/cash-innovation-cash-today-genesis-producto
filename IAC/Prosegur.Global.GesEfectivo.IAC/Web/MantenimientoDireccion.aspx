<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoDireccion.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoDireccion" EnableEventValidation="false" MasterPageFile="Master/MasterModal.Master" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>IAC - Mantenimiento de Direccion</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <style type="text/css">
        .style7 {
            width: 226px;
        }

        .style8 {
            width: 292px;
        }

        .style10 {
            width: 121px;
        }
    </style>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content-modal">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="tabela_campos" >
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblCodigo" runat="server" Text="lblCodigo" CssClass="label2"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodigo" runat="server" Width="201px" Enabled="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </td>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblDescricao" runat="server" CssClass="label2" Text="lblDescricao"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescricao" runat="server" Width="201px" Enabled="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblDireccion" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabela_campos" >
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblPais" runat="server" CssClass="label2">lblPais</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPais" runat="server" Width="201px" MaxLength="50" AutoPostBack="false" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" ></asp:TextBox>
                            &nbsp;<asp:CustomValidator ID="csvPais" runat="server" Text="*"></asp:CustomValidator>
                        </td>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblTelefono" runat="server" CssClass="label2">lblTelefono</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTelefono" runat="server" Width="201px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblProvincia" runat="server" CssClass="label2">lblProvincia</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtProvincia" runat="server" Width="201px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </td>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblNFiscal" runat="server" CssClass="label2">lblNFiscal</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNFiscal" runat="server" MaxLength="50" Width="201px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblCiudad" runat="server" CssClass="label2">lblCiudad</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCiudad" runat="server" Width="201px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </td>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblCodigoPostal" runat="server" CssClass="label2">lblCodigoPostal</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodigoPostal" runat="server" Width="201px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblEmail" runat="server" CssClass="label2">lblEmail</asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" Width="549px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDireccion1" runat="server" CssClass="label2">lblDireccion1</asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtDireccion" runat="server" MaxLength="500" Width="549px" AutoPostBack="false" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                            &nbsp;<asp:CustomValidator ID="csvDireccion" runat="server" Text="*"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblDireccion2" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtDireccion2" runat="server" MaxLength="500" Width="549px" AutoPostBack="false" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" ></asp:TextBox>
                            &nbsp;<asp:CustomValidator ID="csvDireccion2" runat="server" Text="*"></asp:CustomValidator>
                        </td>
                    </tr>
                </table>
                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblDadosAdicionais" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabela_campos">
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblCampo1" runat="server" Text="lblCampo1" CssClass="label2"></asp:Label>
                        </td>
                        <td  style="width: 266px;">
                            <asp:TextBox ID="txtCampos1" runat="server" Width="201px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </td>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblCategoria1" runat="server" CssClass="label2" Text="lblCategoria1"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCategoria1" runat="server" Width="201px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblCampo2" runat="server" Text="lblCampo2" CssClass="label2"></asp:Label>
                        </td>
                        <td  style="width: 266px;">
                            <asp:TextBox ID="txtCampo2" runat="server" Width="201px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </td>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblCategoria2" runat="server" CssClass="label2" Text="lblCategoria2"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCategoria2" runat="server" Width="201px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblCampo3" runat="server" Text="lblCampo1" CssClass="label2"></asp:Label>
                        </td>
                        <td  style="width: 266px;">
                            <asp:TextBox ID="txtCampo3" runat="server" Width="201px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </td>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblCategoria3" runat="server" CssClass="label2" Text="lblCategoria3"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCategoria3" runat="server" Width="201px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table style="margin-top: 15px;">
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnGrabar" CssClass="ui-button" Width="130"/>
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnBaja" CssClass="ui-button" Width="130" Visible="False"/>
                            <div class="botaoOcultar">
                                 <asp:Button runat="server" ID="btnBajaConfirmado" CssClass="ui-button" Width="130"/>
                            </div>
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
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnGrabar" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

