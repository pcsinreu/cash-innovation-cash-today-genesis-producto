<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Principal.Master" CodeBehind="MantenimientoSubCliente.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoSubCliente" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register Src="~/Controles/ucDatosBanc.ascx" TagPrefix="uc1" TagName="ucDatosBanc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Mantenimiento de SubCliente</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0" border="0px">
        <tr>
            <td class="titulo02">
                <table cellpadding="0" cellspacing="4" border="0">
                    <tr>
                        <td>
                            <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                        </td>
                        <td>
                            <asp:Label ID="lblTituloSubCliente" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td align="left">
                <table class="tabela_campos"   border="0px" style="width: 100%;">
                    <tr>
                        <td class="espaco_inicial">&nbsp;
                        </td>

                        <td align="right">
                            <asp:Label ID="lblCliente" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td align="left">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="left">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtCliente" runat="server" Width="160px" MaxLength="15"
                                                    ReadOnly="True" Enabled="False"></asp:TextBox>
                                                <asp:CustomValidator ID="csvClienteObrigatorio" runat="server" ControlToValidate="txtCliente" ErrorMessage="" Text="*"></asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td align="left">
                                        <pro:Botao ID="btnBuscarCliente" runat="server" EstiloIcone="EstiloIcone" Habilitado="True" Tipo="Consultar" Titulo="039_btncliente">
                                        </pro:Botao>
                                    </td>
                                </tr>
                            </table>
                        </td>

                        <td align="right">
                            <asp:Label ID="lblTipoSubCliente" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlTipoSubCliente" runat="server" Width="208px">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="csvTipoSubClienteObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlTipoSubCliente" Text="*"></asp:CustomValidator>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblCodigoAjeno" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodigoAjeno" runat="server" Width="225px" MaxLength="25" CssClass="Text02" AutoPostBack="True" Enabled="False" ReadOnly="True" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblDesCodigoAjeno" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td class="tamanho_celula" align="left" colspan="3">
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDesCodigoAjeno" runat="server" Width="225px" MaxLength="50" CssClass="Text02" AutoPostBack="True" Enabled="False" ReadOnly="True" />
                                    </td>
                                    <td>
                                        <pro:Botao ID="btnAltaAjeno" runat="server" ExecutaValidacaoCliente="True"
                                            Habilitado="True" Tipo="Novo" ExibirLabelBtn="false" EstiloIcone="EstiloIcone"
                                            Titulo="btnCodigoAjeno">
                                        </pro:Botao>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblCodSubCliente" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtCodigoSubCliente" runat="server" MaxLength="15" AutoPostBack="true" CssClass="Text02" Width="200px"></asp:TextBox>
                                                <asp:CustomValidator ID="csvCodSubClienteObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtCodigoSubCliente" Text="*"></asp:CustomValidator>
                                                <asp:CustomValidator ID="csvCodSubClienteExistente" runat="server" ErrorMessage="" ControlToValidate="txtCodigoSubCliente">*</asp:CustomValidator>
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
                        <td class="tamanho_celula" align="right">
                            <asp:Label ID="lblDescSubCliente" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtDescSubCliente" runat="server" MaxLength="100" AutoPostBack="true" CssClass="Text02" Width="200px"></asp:TextBox>
                                                <asp:CustomValidator ID="csvDescSubClienteObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtDescSubCliente" Text="*"></asp:CustomValidator>
                                                <asp:CustomValidator ID="csvDescSubClienteExistente" runat="server" ErrorMessage="" ControlToValidate="txtDescSubCliente" Text="*"></asp:CustomValidator>
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
                                    <td>
                                        <pro:Botao ID="btnDireccion" runat="server" EstiloIcone="EstiloIcone" Habilitado="True" Tipo="Consultar" ExibirLabelBtn="true" Titulo="039_lbl_direccion">
                                        </pro:Botao>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotSaldo" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="upChkTotSaldo" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:CheckBox ID="chkTotSaldo" runat="server" AutoPostBack="true" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkVigente" runat="server" AutoPostBack="True" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblProprioTotSaldo" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="upChkProprioTotSaldo" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:CheckBox ID="chkProprioTotSaldo" runat="server" Enabled="false" AutoPostBack="true" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="titulo02" colspan="5" style="display: table-cell">
                            <table cellpadding="0" cellspacing="4" border="0">
                                <tr>
                                    <td>
                                        <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTituloTotSaldo" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:UpdatePanel ID="upTotSaldo" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phTotSaldo" runat="server"></asp:PlaceHolder>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="titulo02" colspan="5" style="display: table-cell">
                            <table cellpadding="0" cellspacing="4" border="0">
                                <tr>
                                    <td>
                                        <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTituloDatosBanc" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <uc1:ucDatosBanc runat="server" ID="ucDatosBanc" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:UpdatePanel ID="UpdatePanelAcaoPagina" runat="server">
                    <ContentTemplate>
                        <center>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: auto; text-align: center; padding: 2px 15px;">
                                    <pro:Botao ID="btnGrabar" runat="server" Habilitado="True" Tipo="Salvar" Titulo="btnGrabar" ExibirLabelBtn="True" ExecutaValidacaoCliente="true">
                                    </pro:Botao>
                                </td>
                                <td style="width: auto; text-align: center; padding: 2px 15px;">
                                    <pro:Botao ID="btnAnadirTotalizador" runat="server" EstiloIcone="EstiloIcone" Habilitado="True" ExibirLabelBtn="True" Tipo="Novo" Titulo="039_btn_AnadirTotalizador">
                                    </pro:Botao>
                                </td>
                                <td style="width: auto; text-align: center; padding: 2px 15px;">
                                    <pro:Botao ID="btnAnadirCuenta" runat="server" EstiloIcone="EstiloIcone" Habilitado="True" ExibirLabelBtn="True" Tipo="Novo" Titulo="039_btn_AnadirCuenta">
                                    </pro:Botao>
                                </td>
                                <td style="width: auto; text-align: center; padding: 2px 15px;">
                                    <pro:Botao ID="btnCancelar" runat="server" EstiloIcone="EstiloIcone" Habilitado="True" Tipo="Sair" Titulo="btnCancelar">
                                    </pro:Botao>
                                </td>
                                <td style="width: auto; text-align: center; padding: 2px 15px;">
                                    <pro:Botao ID="btnVolver" runat="server" EstiloIcone="EstiloIcone" Habilitado="True" Tipo="Voltar" Titulo="btnVolver">
                                    </pro:Botao>
                                </td>
                            </tr>
                        </table>
                            </center>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
