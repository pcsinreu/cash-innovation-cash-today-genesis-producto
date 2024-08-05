<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoDivisas.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoDivisas" MasterPageFile="~/Principal.Master"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Mantenimiento de Divisas</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td class="titulo02">
                <table cellpadding="0" cellspacing="4" border="0" border="0">
                    <tr>
                        <td>
                            <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                        </td>
                        <td>
                            <asp:Label ID="lblTituloDivisas" runat="server"></asp:Label>
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
            <td>
                <table class="tabela_campos"   border="0">
                    <tr>
                        <td class="espaco_inicial">&nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblCodigoDivisa" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtCodigoDivisa" runat="server" MaxLength="15" AutoPostBack="true"
                                        CssClass="Text02"></asp:TextBox>
                                    <asp:CustomValidator ID="csvCodigoObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtCodigoDivisa"
                                        Text="*"></asp:CustomValidator>
                                    <asp:CustomValidator ID="csvCodigoDivisaExistente" runat="server" ErrorMessage=""
                                        ControlToValidate="txtCodigoDivisa">*</asp:CustomValidator>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 20px;">
                            <asp:UpdateProgress ID="UpdateProgressCodigo" runat="server" AssociatedUpdatePanelID="UpdatePanelCodigo">
                                <ProgressTemplate>
                                    <img src="Imagenes/loader1.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                        <td class="tamanho_celula" align="right">
                            <asp:Label ID="lblDescricaoDivisa" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtDescricaoDivisa" runat="server" Width="272px" MaxLength="50"
                                        AutoPostBack="true" CssClass="Text02"></asp:TextBox>
                                    <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ErrorMessage=""
                                        ControlToValidate="txtDescricaoDivisa" Text="*">*</asp:CustomValidator>
                                    <asp:CustomValidator ID="csvDescricaoDivisaExistente" runat="server" ErrorMessage=""
                                        ControlToValidate="txtDescricaoDivisa">*</asp:CustomValidator>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 20px;">
                            <asp:UpdateProgress ID="UpdateProgressDescricao" runat="server" AssociatedUpdatePanelID="UpdatePanelDescricao">
                                <ProgressTemplate>
                                    <img src="Imagenes/loader1.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblSimbolo" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtSimbolo" runat="server" MaxLength="5" AutoPostBack="true" CssClass="Text02"></asp:TextBox>
                        </td>
                        <td class="tamanho_celula" align="right">
                            <asp:Label ID="lblColor" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td align="left">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelCodigoCor" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtColor" runat="server" Width="83px" MaxLength="15" CssClass="Text02"></asp:TextBox>
                                                <cc1:ColorPickerExtender ID="txtColor_ColorPickerExtender" runat="server" Enabled="True"
                                                    OnClientColorSelectionChanged="colorChanged" TargetControlID="txtColor">
                                                </cc1:ColorPickerExtender>
                                                <asp:CustomValidator ID="csvCodigoColorObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtColor"
                                                    Text="*">*</asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td align="right" width="150">
                                        <asp:Label ID="lblCodigoAcesso" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td width="45" align="right">
                                        <asp:UpdatePanel ID="UpdatePanelCodigoAcesso" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtCodigoAcesso" runat="server" MaxLength="1" AutoPostBack="true"
                                                    CssClass="Text02" Width="20px"></asp:TextBox>
                                                <asp:CustomValidator ID="csvCodigoAcessoObrigatorio" runat="server" ErrorMessage=""
                                                    ControlToValidate="txtCodigoAcesso" Text="*">*</asp:CustomValidator>
                                                <asp:CustomValidator ID="csvCodigoAcessoExiste" runat="server" ErrorMessage="" ControlToValidate="txtCodigoAcesso">*</asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelCodigoAcesso">
                                <ProgressTemplate>
                                    <img src="Imagenes/loader1.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td> 
                        <td align="right">
                            <asp:Label ID="lblCodigoAjeno" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtCodigoAjeno" runat="server" Width="225px" MaxLength="50" CssClass="Text02" AutoPostBack="True" ReadOnly="True" />
                        </td>
                        <td class="tamanho_celula" align="right">
                            <asp:Label ID="lblDesCodigoAjeno" runat="server" class="tamanho_celula" align="right"></asp:Label>
                        </td>
                        <td class="tamanho_celula" align="left" colspan="3">
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDesCodigoAjeno" runat="server" Width="225px" MaxLength="50" CssClass="Text02" AutoPostBack="True" ReadOnly="True" />
                                    </td>
                                    <td>
                                        <pro:Botao ID="btnAltaAjeno" runat="server" ExecutaValidacaoCliente="True" Habilitado="True" Tipo="Novo" ExibirLabelBtn="false" EstiloIcone="EstiloIcone" Titulo="btnCodigoAjeno">
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
                            <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:CheckBox ID="chkVigente" runat="server" />
                        </td>
                        <td class="tamanho_celula" align="right">&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="titulo02">
                <table cellpadding="0" cellspacing="4" border="0">
                    <tr>
                        <td>
                            <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                        </td>
                        <td>
                            <asp:Label ID="lblSubTitulosDivisas" runat="server"></asp:Label>
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
            <td align="center">
                <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <pro:ProsegurGridView ID="GdvDenominaciones" runat="server" AllowPaging="True" AllowSorting="True"
                            ColunasSelecao="Codigo" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                            PageSize="10" AutoGenerateColumns="False" Ajax="True" Width="95%" ExibirCabecalhoQuandoVazio="false"
                            Height="100%">
                            <Pager ID="objPager_ProsegurGridView1">
                                <FirstPageButton Visible="True">
                                </FirstPageButton>
                                <LastPageButton Visible="True">
                                </LastPageButton>
                                <Summary Text="Página {0} de {1} ({2} itens)" />
                                <SummaryStyle>
                                </SummaryStyle>
                            </Pager>
                            <HeaderStyle CssClass="GridTitulo" Font-Bold="True" />
                            <AlternatingRowStyle CssClass="GridLinhaAlternada" />
                            <RowStyle CssClass="GridLinhaPadrao" />
                            <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px"> </TextBox>
                            <Columns>
                                <asp:BoundField DataField="Codigo" HeaderText="Código" SortExpression="codigo" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                                <asp:BoundField DataField="Valor" HeaderText="Valor" SortExpression="Valor" />
                                <asp:BoundField DataField="Peso" HeaderText="Peso" SortExpression="Peso" />
                                <asp:TemplateField HeaderText="EsBillete" SortExpression="EsBillete">
                                    <ItemTemplate>
                                        <asp:Image ID="imgBillete" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vigente" SortExpression="Vigente">
                                    <ItemTemplate>
                                        <asp:Image ID="imgVigente" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </pro:ProsegurGridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAlta" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnBaja" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnModificacion" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnConsulta" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
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
            <td align="center">
                <asp:UpdatePanel ID="UpdatePanelBtnsGrid" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 25%; text-align: center">
                                    <pro:Botao ID="btnAlta" runat="server" Habilitado="True" Tipo="Novo" Titulo="btnAlta"
                                        ExibirLabelBtn="True">
                                    </pro:Botao>
                                </td>
                                <td style="width: 25%; text-align: center">
                                    <pro:Botao ID="btnBaja" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                        Tipo="Excluir" Titulo="btnBaja">
                                    </pro:Botao>
                                </td>
                                <td style="width: 25%; text-align: center">
                                    <pro:Botao ID="btnModificacion" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                        Tipo="Editar" Titulo="btnModificacion">
                                    </pro:Botao>
                                </td>
                                <td style="width: 25%; text-align: center">
                                    <pro:Botao ID="btnConsulta" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                        Tipo="Localizar" Titulo="btnConsulta">
                                    </pro:Botao>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgressBtns" runat="server" AssociatedUpdatePanelID="UpdatePanelBtnsGrid">
                    <ProgressTemplate>
                        <div id="divLoading" class="AlertLoading" style="visibility: hidden;">
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
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
