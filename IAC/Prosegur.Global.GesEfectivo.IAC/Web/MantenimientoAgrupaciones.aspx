<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoAgrupaciones.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoAgrupaciones" MasterPageFile="~/Principal.Master"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Mantenimiento de Agrupaciones</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titulo02">
                <table cellpadding="0" cellspacing="4" cellpadding="0" border="0">
                    <tr>
                        <td>
                            <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                        </td>
                        <td>
                            <asp:Label ID="lblTituloAgrupaciones" runat="server"></asp:Label>
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
                <table class="tabela_campos"  >
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td align="right" width="100px">
                            <asp:Label ID="lblCodigoAgrupacion" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td width="150px">
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtCodigoAgrupacion" runat="server" MaxLength="15" AutoPostBack="true"
                                                    CssClass="Text02"></asp:TextBox><asp:CustomValidator ID="csvCodigoObrigatorio" runat="server"
                                                        ErrorMessage="" ControlToValidate="txtCodigoAgrupacion" Text="*"></asp:CustomValidator><asp:CustomValidator
                                                            ID="csvCodigoAgrupacionExistente" runat="server" ErrorMessage="" ControlToValidate="txtCodigoAgrupacion">*</asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdateProgress ID="UpdateProgressCodigo" runat="server" AssociatedUpdatePanelID="UpdatePanelCodigo">
                                            <ProgressTemplate>
                                                <img src="Imagenes/loader1.gif" /></ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tamanho_celula" align="right">
                            <asp:Label ID="lblDescricaoAgrupacion" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td width="310px">
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtDescricaoAgrupacion" runat="server" Width="235px" MaxLength="50"
                                                    AutoPostBack="true" CssClass="Text02"></asp:TextBox><asp:CustomValidator ID="csvDescricaoObrigatorio"
                                                        runat="server" ErrorMessage="001_msg_Agrupaciondescripcionobrigatorio" ControlToValidate="txtDescricaoAgrupacion"
                                                        Text="*"></asp:CustomValidator><asp:CustomValidator ID="csvDescripcionExistente"
                                                            runat="server" ErrorMessage="" ControlToValidate="txtDescricaoAgrupacion">*</asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdateProgress ID="UpdateProgressDescricao" runat="server" AssociatedUpdatePanelID="UpdatePanelDescricao">
                                            <ProgressTemplate>
                                                <img src="Imagenes/loader1.gif" /></ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblObservaciones" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtObservaciones" runat="server" MaxLength="4000" CssClass="Text02"
                                Height="96px" Width="610px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td>
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
            <td class="titulo02">
                <table cellpadding="0" cellspacing="4" cellpadding="0" border="0">
                    <tr>
                        <td>
                            <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                        </td>
                        <td>
                            <asp:Label ID="lblSubTitulosAgrupaciones" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="tabela_interna" align="center" cellpadding="0" cellspacing="2">
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td width="110px">
                            &nbsp;
                        </td>
                        <td width="60%">
                            <asp:UpdatePanel ID="UpdatePanelTreeView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table border="1" cellpadding="0" cellspacing="5" width="750px">
                                        <tr>
                                            <td id="TdTreeViewDivisa" style="width: 40%; border-width: 1px; vertical-align: top;"
                                                runat="server">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:TreeView ID="TrvDivisas" runat="server" ShowLines="True" CssClass="Lbl2">
                                                                <SelectedNodeStyle CssClass="TreeViewSelecionado" />
                                                            </asp:TreeView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td id="TdBtnInEx" align="center" style="width: 5%; border-width: 0;" runat="server">
                                                <asp:ImageButton ID="imgBtnIncluir" runat="server" ImageUrl="~/Imagenes/pag05.png" />
                                                <br />
                                                <asp:ImageButton ID="imgBtnExcluir" runat="server" ImageUrl="~/Imagenes/pag06.png" />
                                            </td>
                                            <td style="width: 40%; border-width: 1px; vertical-align: top;">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:TreeView ID="TrvAgrupaciones" runat="server" ShowLines="True" CssClass="Lbl2">
                                                                <SelectedNodeStyle CssClass="TreeViewSelecionado" />
                                                            </asp:TreeView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="imgBtnIncluir" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="imgBtnExcluir" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:UpdateProgress ID="UpdateProgressTreeView" runat="server" AssociatedUpdatePanelID="UpdatePanelTreeView">
                                <ProgressTemplate>
                                    <div id="divLoading" class="AlertLoading" style="visibility: hidden;">
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelCsvTreeViewAgrupacion" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:CustomValidator ID="csvTrvAgrupaciones" runat="server" ErrorMessage="" ControlToValidate=""
                                        Text="*"></asp:CustomValidator>
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
        <asp:Panel ID="pnlSemRegistro" runat="server" Visible="false">
            <tr>
                <td align="center">
                    <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro">
                        <tr>
                            <td style="border-width: 0;">
                                </td>
                            <td style="border-width: 0;">
                                <asp:Label ID="lblSemRegistro" runat="server" Text="Label" CssClass="Lbl2">Não existem dados a serem exibidos.</asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td>
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
                                        ExibirLabelBtn="True" ExecutaValidacaoCliente="true">
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
