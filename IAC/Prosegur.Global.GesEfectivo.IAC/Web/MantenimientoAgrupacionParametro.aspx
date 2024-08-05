<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoAgrupacionParametro.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoAgrupacionParametro"
    MasterPageFile="~/Principal.Master" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Mantenimiento de Parámetros</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanelGeral" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
        <ContentTemplate>
            <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="titulo02">
                        <table cellpadding="0" cellspacing="4" border="0">
                            <tr>
                                <td>
                                    <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                                </td>
                                <td>
                                    <asp:Label ID="lblSubTitulosAgrupacionParametro" runat="server"></asp:Label>
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
                        <table class="tabela_campos"   id="TableFields" runat="server">
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right" style="font-size: medium; font-family: Arial">
                                    <asp:Label ID="lblAplicacion" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left" style="font-size: medium; font-family: Arial">
                                    <asp:UpdatePanel ID="UpdatePanelDdlAplicacion" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlAplicacion" runat="server" Width="265px" AutoPostBack="True">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="csvDdlAplicacionObrigatorio" runat="server" ControlToValidate="ddlAplicacion"
                                                ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td align="right" style="font-size: medium; font-family: Arial">
                                    <asp:Label ID="lblNivel" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left" style="font-size: medium; font-family: Arial;" colspan="2">
                                    <asp:UpdatePanel ID="UpdatePanelDdlNivel" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlNivel" runat="server" Width="265px" AutoPostBack="True">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="csvDdlNivelObrigatorio" runat="server" ControlToValidate="ddlNivel"
                                                ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td width="20px">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right" style="font-size: medium; font-family: Arial">
                                    <asp:Label ID="lblDescripcionCorto" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left" style="font-size: medium; font-family: Arial" colspan="4">
                                    <asp:UpdatePanel ID="UpdatePanelTxtDescripcionCorto" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtDescripcionCorto" runat="server" CssClass="Text02" MaxLength="30"
                                                Width="650px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="csvTxtDescripcionCortoObrigatorio" runat="server"
                                                ControlToValidate="txtDescripcionCorto" ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td width="20px">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right" style="font-size: medium; font-family: Arial">
                                    <asp:Label ID="lblDescripcionLarga" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left" style="font-size: medium; font-family: Arial" colspan="4">
                                    <asp:UpdatePanel ID="UpdatePanelTxtDescripcionLarga" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtDescripcionLarga" runat="server" CssClass="Text02" Height="48px"
                                                MaxLength="100" TextMode="MultiLine" Width="650"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="csvTxtDescripcionLargaObrigatorio" runat="server"
                                                ControlToValidate="txtDescripcionLarga" ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td width="20px">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right" style="font-size: medium; font-family: Arial">
                                    <asp:Label ID="lblOrden" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left" style="font-size: medium; font-family: Arial">
                                    <asp:UpdatePanel ID="UpdatePanelTxtOrden" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtOrden" runat="server" CssClass="Text02" MaxLength="9" Width="58px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="csvTxtOrden" runat="server" ControlToValidate="txtOrden"
                                                ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td align="right" style="font-size: medium; font-family: Arial">
                                    &nbsp;
                                </td>
                                <td align="left" style="font-size: medium; font-family: Arial" colspan="2">
                                    &nbsp;
                                </td>
                                <td width="20px">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    &nbsp;
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
                    <td align="center">
                        <asp:UpdatePanel ID="UpdatePanelAcaoPagina" runat="server">
                            <ContentTemplate>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 25%; text-align: center">
                                            <pro:Botao ID="btnGrabar" runat="server" Habilitado="True" Tipo="Salvar" Titulo="btnGrabar"
                                                ExibirLabelBtn="True" ExecutaValidacaoCliente="false">
                                            </pro:Botao>
                                        </td>
                                        <td style="width: 25%; text-align: center">
                                            <pro:Botao ID="btnCancelar" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                Tipo="Sair" Titulo="btnCancelar">
                                            </pro:Botao>
                                        </td>
                                        <td style="width: 25%; text-align: center">
                                            <pro:Botao ID="btnVolver" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                Tipo="Voltar" Titulo="btnVolver" Visible="False">
                                            </pro:Botao>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
