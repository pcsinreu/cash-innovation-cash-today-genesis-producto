<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoDenominaciones.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoDenominaciones" MasterPageFile="Master/MasterModal.Master" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>

<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>IAC - Mantenimiento de Denominações</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content-modal">
    <asp:UpdatePanel runat="server" ID="pnGeral">
        <ContentTemplate>
            <div class="ui-panel-titlebar">
                <asp:Label ID="lblTituloDenominacion" CssClass="ui-panel-title" runat="server"></asp:Label>
            </div>
            <table class="tabela_campos" >
                <tr>
                    <td class="tamanho_celula">
                        <asp:Label ID="lblCodigoDenominacion" runat="server" CssClass="label2"></asp:Label>
                    </td>
                    <td>
                        <table style="margin: 0px !Important;">
                            <tr>
                                <td >
                                    <asp:UpdatePanel ID="UpdatePanelCodigoDenominacion" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCodigoDenominacion" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" MaxLength="15" AutoPostBack="True"></asp:TextBox>
                                            <asp:CustomValidator ID="csvCodigoDenominacionExistente" runat="server" ControlToValidate="txtCodigoDenominacion">*</asp:CustomValidator>
                                            <asp:CustomValidator ID="csvCodigoDenominacionObrigatorio" runat="server" ControlToValidate="txtCodigoDenominacion">*</asp:CustomValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tamanho_celula">
                        <asp:Label ID="lblDescricaoDenominacion" runat="server" CssClass="label2"></asp:Label>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="txtDescricaoDenominacion" runat="server" Width="200px" MaxLength="50"
                                    AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                                <asp:CustomValidator ID="csvDescripcionDenominacionObrigatorio" runat="server" ControlToValidate="txtDescricaoDenominacion">*</asp:CustomValidator>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="tamanho_celula">
                        <asp:Label ID="lblValor" runat="server" CssClass="label2"></asp:Label>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanelValor" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="txtValorDenominacion" runat="server" MaxLength="19" Style="text-align: right"
                                    AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                                <asp:CustomValidator ID="csvValorDenominacionObrigatorio" runat="server" ControlToValidate="txtValorDenominacion">*</asp:CustomValidator>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="tamanho_celula">
                        <asp:Label ID="lblPeso" runat="server" CssClass="label2"></asp:Label>
                    </td>
                    <td style="height: 28px;">
                        <asp:TextBox ID="txtPesoDenominacion" runat="server" MaxLength="18" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Style="text-align: right"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tamanho_celula">
                        <asp:Label ID="lblCodigoAcceso" runat="server" CssClass="label2"></asp:Label>
                    </td>
                    <td style="height: 28px;">
                        <asp:UpdatePanel ID="UpdatePanelCodigoAcceso" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="txtCodigoAcceso" runat="server" MaxLength="1" Style="text-align: right"
                                    AutoPostBack="True" Width="20" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="tamanho_celula">
                        <asp:Label ID="lblIndicadorBilhete" runat="server" CssClass="label2"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIndicadorBilhete" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="tamanho_celula">
                        <asp:Label ID="lblVigente" runat="server" CssClass="label2"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkVigente" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="tamanho_celula">
                        <asp:Label ID="lblCodigoAjeno" runat="server" CssClass="label2"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCodigoAjeno" runat="server" Width="225px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="True" ReadOnly="True" />
                    </td>
                </tr>
                <tr>
                    <td class="tamanho_celula" >
                        <asp:Label ID="lblDesCodigoAjeno" runat="server" class="label2" align="right"></asp:Label>
                    </td>
                    <td>
                        <table style="margin: 0px 0px 0px -2px !Important;">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtDesCodigoAjeno" runat="server" Width="225px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="True" ReadOnly="True" />
                                </td>
                                <td>
                                   <asp:Button runat="server" ID="btnAltaAjenoDenominacion" Text="btnCodigoAjeno" CssClass="ui-button" Width="100"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td align="center">
                        <asp:UpdatePanel ID="UpdatePanelAcaoPagina" runat="server">
                            <ContentTemplate>
                                <table style="margin: 0px !Important;">
                                    <tr>
                                        <td >
                                            <asp:Button runat="server" ID="btnGrabar" CssClass="ui-button" Width="100"/>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <div class="botaoOcultar">
                <asp:Button runat="server" ID="btnConsomeCodigoAjeno" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
                
    </div>
</asp:Content>
