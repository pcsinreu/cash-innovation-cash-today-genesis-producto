<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoTerminoMediosPago.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoTerminoMediosPago"
    EnableEventValidation="false" MasterPageFile="Master/MasterModal.Master" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>

<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>IAC - Mantenimiento de Terminos de Médio de Pago</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content-modal">
        <asp:UpdatePanel runat="server" ID="pnGeral">
            <ContentTemplate>
                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblTituloTerminoMedioPago" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table width="100%">
                    <tr>
                        <td align="left">
                            <table class="tabela_campos" style="margin: 0px !Important">
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCodigoTerminoMedioPago" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <table width="100%" style="margin: 0px !Important">
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanelCodigoTerminoMedioPago" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtCodigoTerminoMedioPago" runat="server" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                                                MaxLength="15" Width="140px"></asp:TextBox>
                                                            <asp:CustomValidator ID="csvCodigoObrigatorio" runat="server" ControlToValidate="txtCodigoTerminoMedioPago"
                                                                ErrorMessage="" Text="*"></asp:CustomValidator>
                                                            <asp:CustomValidator ID="csvCodigoMedioPagoExistente" runat="server" ControlToValidate="txtCodigoTerminoMedioPago"
                                                                ErrorMessage="">*</asp:CustomValidator>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDescricaoTerminoMedioPago" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelDescricaoMedioPago" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtDescricaoTerminoMedioPago" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" MaxLength="15"
                                                    Width="330px" AutoPostBack="False"></asp:TextBox><asp:CustomValidator ID="csvDescricaoTerminoMedioPago"
                                                        runat="server" ControlToValidate="txtDescricaoTerminoMedioPago" ErrorMessage=""
                                                        Text="*"></asp:CustomValidator>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblObservaciones" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtObservaciones" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Height="96px"
                                            MaxLength="4000" TextMode="MultiLine" Width="653px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblVigente" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:CheckBox ID="chkVigente" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblTipoFormato" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td >
                                        <asp:UpdatePanel ID="UpdatePanelTipoFormato" runat="server">
                                            <ContentTemplate>
                                                <table style="margin: 0px 0px 0px -2px !Important;">
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="ddlTipoFormato" runat="server" Width="190px" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:CustomValidator ID="csvTipoFormatoObrigatorio" runat="server" ControlToValidate="ddlTipoFormato"
                                                                ErrorMessage="">*</asp:CustomValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td colspan="2">
                                        <table class="tabela_campos" style="margin: 0px !Important">
                                            <tr>
                                                <td class="tamanho_celula">
                                                    <asp:Label ID="lblLongitud" runat="server" CssClass="label2"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanelLongitud" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtLongitud" runat="server" MaxLength="2" Width="100px" Enabled="False"
                                                                AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                                                            <asp:CustomValidator ID="csvLongitudeObrigatorio" runat="server" ControlToValidate="txtLongitud"
                                                                ErrorMessage="">*</asp:CustomValidator>
                                                            <asp:CustomValidator ID="csvLongitudeValorInvalido" runat="server" ControlToValidate="txtLongitud"
                                                                ErrorMessage="">*</asp:CustomValidator>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td class="tamanho_celula">
                                                    <asp:Label ID="lblValorInicial" runat="server" CssClass="label2"></asp:Label>
                                                </td>
                                                <td >
                                                    <asp:UpdatePanel ID="UpdatePanelValorInicial" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtValorInicial" runat="server" AutoPostBack="false" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                                                MaxLength="50" Width="80px"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblValidacaoFormato" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelFormatoMascara" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlFormatoMascara" runat="server" Width="190px" AutoPostBack="true" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblMostrarCodigo" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkMostrarCodigo" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblValidacaoFormula" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelFormulaAlgoritmo" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlFormulaAlgoritmo" runat="server" Width="190px" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td colspan="2">
                                        <table id="tbl_btn_valores" runat="server" style="margin: 0px !Important">
                                            <tr>
                                                <td>
                                                    <asp:Button runat="server" ID="btnValoresPosibles" CssClass="ui-button" Width="150" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="margin: 20px 0px 0px 0px !Important">
                                <tr>
                                    <td>
                                        <asp:Button runat="server" ID="btnGrabar" CssClass="ui-button" Width="120" />
                                        <div class="botaoOcultar">
                                            <asp:Button runat="server" ID="btnConsomeValores"/>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

