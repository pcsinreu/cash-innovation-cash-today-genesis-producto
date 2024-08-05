<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaDatosBancariosPopup.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaDatosBancariosPopup" MasterPageFile="Master/MasterModal.Master" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>

<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>IAC - Búsqueda de Nivel Saldo</title>

    <base target="_self" />
    <script src="JS/Funcoes.js" type="text/javascript"></script>


    <style type="text/css">
        .tamanhoColuna {
            width: 89px !important;
            max-width: 89px !important;
            min-width: 89px !important;
        }

        .tamanhoColuna2 {
            width: 150px !important;
            max-width: 150px !important;
            min-width: 150px !important;
        }

        .tamanhoColuna4 {
            width: 250px !important;
            max-width: 250px !important;
            min-width: 250px !important;
        }
    </style>

</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content-modal">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Label Style="font-weight:bold; color:red; margin-left: 7px;" ID="lblAprobacionPendienteMensaje"
                        runat="server" CssClass="label2"></asp:Label>
                    <div style="margin-top: 15px">
                        <asp:UpdatePanel ID="updUcClienteUc" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <table class="tabela_campos">
                        <tr>
                            <td>
                                <table class="tabela_campos" style="margin: 0px !important">

                                    <tr>
                                        <td class="tamanhoColuna">
                                            <asp:Label ID="lblCodigoBancario" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td class="tamanhoColuna2">
                                            <asp:TextBox ID="txtCodigoBancario" runat="server" Width="160px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td class="tamanhoColuna">
                                            <asp:Label ID="lblDefecto" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td class="tamanhoColuna4">
                                            <asp:CheckBox ID="chkDefecto" runat="server"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tamanhoColuna">
                                            <asp:Label ID="lblNroDocumento" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td class="tamanhoColuna2">
                                            <asp:TextBox ID="txtNroDocumento" runat="server" Width="160px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                        </td>

                                        <td class="tamanhoColuna">
                                            <asp:Label ID="lblTitularidad" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td class="tamanhoColuna4">
                                            <asp:TextBox ID="txtTitularidad" runat="server" Width="160px" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                                            <asp:CustomValidator ID="csvTitularidadObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtTitularidad" Text="*"></asp:CustomValidator>
                                        </td>


                                    </tr>

                                    <tr>

                                        <td class="tamanhoColuna">
                                            <asp:Label ID="lblAgencia" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td class="tamanhoColuna2">
                                            <asp:TextBox ID="txtAgencia" runat="server" Width="160px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                        </td>
                                        <td class="tamanhoColuna">
                                            <asp:Label ID="lblCuenta" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td class="tamanhoColuna4">
                                            <asp:TextBox ID="txtCuenta" runat="server" Width="160px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                                            <asp:CustomValidator ID="csvCuentaObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtCuenta" Text="*"></asp:CustomValidator>
                                        </td>
                                    </tr>


                                <tr>
                                     <td class="tamanhoColuna">
                                        <asp:Label ID="lblDivisa" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td  class="tamanhoColuna2">
                                        <asp:DropDownList ID="ddlDivisa" runat="server" Width="170px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:DropDownList>
                                        <asp:CustomValidator ID="csvDivisaObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlDivisa" Text="*"></asp:CustomValidator>
                                    </td>
                                     <td class="tamanhoColuna">
                                        <asp:Label ID="lblTipo" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td  class="tamanhoColuna2">
                                        <div style="float: left;"> 
                                            <dx:ASPxComboBox ID="cbxTipo" runat="server" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith"  Height="10px" CssClass="ui-corner-all ui-gn-mandatory" />
                                        </div>
                                        <asp:CustomValidator ID="csvTipoObrigatorio" runat="server" ErrorMessage="" ControlToValidate="cbxTipo" Text="*"></asp:CustomValidator>
                                    </td>
                                </tr>


                                    <tr>
                                        <td class="tamanhoColuna">
                                            <asp:Label ID="lblObs" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td class="tamanhoColuna2">
                                            <asp:TextBox ID="txtObs" runat="server" Width="160px" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                        </td>


                                    </tr>
                                </table>
                                <div class="ui-panel-titlebar">
                                    <asp:Label ID="lblDatosAdicionales" CssClass="ui-panel-title" runat="server">Datos Adicionales</asp:Label>
                                </div>


                                <table class="tabela_campos" style="margin: 0px !important">

                                    <tr>
                                        <td class="tamanhoColuna">
                                            <asp:Label ID="lblCampoAdicional1" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td class="tamanhoColuna2">
                                            <asp:TextBox ID="txtCampoAdicional1" runat="server" Width="160px" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                        </td>

                                        <td class="tamanhoColuna">
                                            <asp:Label ID="lblCampoAdicional2" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td class="tamanhoColuna4">
                                            <asp:TextBox ID="txtCampoAdicional2" runat="server" Width="160px" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                        </td>


                                    </tr>

                                    <tr>

                                        <td class="tamanhoColuna">
                                            <asp:Label ID="lblCampoAdicional3" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td class="tamanhoColuna2">
                                            <asp:TextBox ID="txtCampoAdicional3" runat="server" Width="160px" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                        </td>
                                        <td class="tamanhoColuna">
                                            <asp:Label ID="lblCampoAdicional4" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td class="tamanhoColuna4">
                                            <asp:TextBox ID="txtCampoAdicional4" runat="server" Width="160px" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>

                                        <td class="tamanhoColuna">
                                            <asp:Label ID="lblCampoAdicional5" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td class="tamanhoColuna2">
                                            <asp:TextBox ID="txtCampoAdicional5" runat="server" Width="160px" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                        </td>
                                        <td class="tamanhoColuna">
                                            <asp:Label ID="lblCampoAdicional6" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td class="tamanhoColuna4">
                                            <asp:TextBox ID="txtCampoAdicional6" runat="server" Width="160px" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>

                                        <td class="tamanhoColuna">
                                            <asp:Label ID="lblCampoAdicional7" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td class="tamanhoColuna2">
                                            <asp:TextBox ID="txtCampoAdicional7" runat="server" Width="160px" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                        </td>
                                        <td class="tamanhoColuna">
                                            <asp:Label ID="lblCampoAdicional8" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td class="tamanhoColuna4">
                                            <asp:TextBox ID="txtCampoAdicional8" runat="server" Width="160px" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                        </td>
                                    </tr>



                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="margin: 0px !Important">
                                    <tr>
                                        <td>
                                            <asp:Button runat="server" ID="btnAceptar" CssClass="ui-button" Width="130" />
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnLimpar" CssClass="ui-button" Width="130" />
                                            <div class="botaoOcultar">

                                                <asp:Button runat="server" ID="btnAlertaSi" CssClass="btn-excluir" />
                                                <asp:Button runat="server" ID="btnAlertaNo" CssClass="btn-excluir" />
                                            </div>
                                        </td>

                                    </tr>
                                </table>
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

