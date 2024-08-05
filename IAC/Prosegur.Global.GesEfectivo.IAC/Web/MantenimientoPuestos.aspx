<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoPuestos.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoPuestos" MasterPageFile="~/Principal.Master"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Mantenimiento de Canales</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hidBtnGrabar" runat="server" />
    <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titulo02">
                <table cellpadding="0" cellspacing="4" cellpadding="0" border="0">
                    <tr>
                        <td>
                            <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                        </td>
                        <td>
                            <asp:Label ID="lblTituloPuesto" runat="server"></asp:Label>
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
            <td align="left">
                <table class="tabela_campos"   style="width: 100%">
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblDelegacion" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDelegacion" runat="server" CssClass="Text02" Enabled="false"
                                Width="30%">
                            </asp:TextBox>
                        </td>
                        <td style="width: 20px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblAplicacion" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td colspan="5">
                            <asp:UpdatePanel runat="server" ID="upAplicacion">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlAplicacion" runat="server" CssClass="Text02" Width="30%"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvAplicacion" ControlToValidate="ddlAplicacion"
                                        Text="*" ErrorMessage="" Display="Dynamic" ValidationGroup="Gravar" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 20px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblCodigoPuesto" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td colspan="5">
                            <asp:UpdatePanel runat="server" ID="upCodigoPuesto">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtCodigoPuesto" runat="server" CssClass="Text02" MaxLength="20">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvCodigoPuesto" ControlToValidate="txtCodigoPuesto"
                                        Text="*" ErrorMessage="" Display="Dynamic" ValidationGroup="Gravar" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 20px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblHostPuesto" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td colspan="5">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtHostPuesto" runat="server" CssClass="Text02" MaxLength="30">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvhostPuesto" ControlToValidate="txtHostPuesto"
                                        Text="*" ErrorMessage="" Display="Dynamic" ValidationGroup="Gravar" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 20px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:CheckBox ID="chkVigente" runat="server" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 14px;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;
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
                                        ExibirLabelBtn="True" ExecutaValidacaoCliente="False">
                                    </pro:Botao>
                                </td>
                                <td style="width: 25%; text-align: center">
                                    <pro:Botao ID="btnCancelar" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                        Tipo="Sair" Titulo="btnCancelar">
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
