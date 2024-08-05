<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoCaracteristicas.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoCaracteristicas" MasterPageFile="~/Principal.Master" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register Src="Controles/Erro.ascx" TagName="Erro" TagPrefix="uc1" %>
<%@ Register Src="Controles/Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Mantenimiento de Subcanales</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="tabela_interna" border="0" align="center" cellpadding="0" cellspacing="0"
        bgcolor="#FFFFFF">
        <tr>
            <td class="titulo02">
                <table cellpadding="0" cellspacing="4" cellpadding="0" border="0">
                    <tr>
                        <td>
                            <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                        </td>
                        <td>
                            <asp:Label ID="lblTituloCaracteristicas" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 768px;"   border="0">
                    <tr>
                        <td class="style2" align="right" style="font-size: medium; font-family: Arial">
                            <asp:Label ID="lblCodigoCaracteristica" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtCodigoCaracteristica" runat="server" MaxLength="15" AutoPostBack="True"></asp:TextBox><asp:CustomValidator
                                                    ID="csvCodigoCaracteristicaObrigatorio" runat="server" ControlToValidate="txtCodigoCaracteristica">*</asp:CustomValidator><asp:CustomValidator
                                                        ID="csvCodigoCaracteristica" runat="server" ErrorMessage="" ControlToValidate="txtCodigoCaracteristica">*</asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdateProgress ID="UpdateProgressCodigo" runat="server" AssociatedUpdatePanelID="UpdatePanelCodigo">
                                            <ProgressTemplate>
                                                <img src="Imagenes/loader1.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="style3" align="right" style="font-size: medium; font-family: Arial">
                            <asp:Label ID="lblDescricao" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtDescricao" runat="server" Width="295px" MaxLength="50" AutoPostBack="True"></asp:TextBox><asp:CustomValidator
                                                    ID="csvDescricaoObrigatorio" runat="server" ControlToValidate="txtDescricao">*</asp:CustomValidator><asp:CustomValidator
                                                        ID="csvDescripcion" runat="server" ErrorMessage="" ControlToValidate="txtDescricao">*</asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdateProgress ID="UpdateProgressDescricao" runat="server" AssociatedUpdatePanelID="UpdatePanelDescricao">
                                            <ProgressTemplate>
                                                <img src="Imagenes/loader1.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2" align="right" style="font-size: medium; font-family: Arial">
                            <asp:Label ID="lblCodigoConteo" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelCodigoConteo" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtCodigoConteo" runat="server" MaxLength="15" AutoPostBack="True"></asp:TextBox><asp:CustomValidator
                                                    ID="csvCodigoConteoObrigatorio" runat="server" ControlToValidate="txtCodigoConteo">*</asp:CustomValidator><asp:CustomValidator
                                                        ID="csvCodigoConteo" runat="server" ErrorMessage="" ControlToValidate="txtCodigoConteo">*</asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdateProgress ID="UpdateProgressCodigoConteo" runat="server" AssociatedUpdatePanelID="UpdatePanelCodigoConteo">
                                            <ProgressTemplate>
                                                <img src="Imagenes/loader1.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblObservaciones" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtObservaciones" runat="server" MaxLength="500" TextMode="MultiLine"
                                CssClass="Text02" Height="76px" Width="610px"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2" align="right" style="font-size: medium; font-family: Arial">
                            <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:CheckBox ID="chkVigente" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:UpdatePanel ID="UpdatePanelAcaoPagina" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 25%; text-align: center">
                                    <pro:Botao ID="btnGrabar" runat="server" Habilitado="True" Tipo="Salvar" Titulo="btnGrabar"
                                        ExibirLabelBtn="True" ExecutaValidacaoCliente="True">
                                    </pro:Botao>
                                </td>
                                <td style="width: 25%; text-align: center">
                                    <pro:Botao ID="btnCancelar" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                        Tipo="Sair" Titulo="btnCancelar">
                                    </pro:Botao>
                                </td>
                                <td style="width: 25%; text-align: center">
                                    <pro:Botao ID="btnVolver" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                        Tipo="Voltar" Titulo="btnVolver">
                                    </pro:Botao>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
