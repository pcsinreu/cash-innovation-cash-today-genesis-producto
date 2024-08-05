<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoParametro.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoParametro" MasterPageFile="~/Principal.Master"
    EnableEventValidation="false" %>

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
                                    <asp:Label ID="lblSubTitulosCriteriosBusqueda" runat="server"></asp:Label>
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
                        <table class="tabela_campos"  >
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right" style="font-size: medium; font-family: Arial">
                                    <asp:Label ID="lblAplicacion" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left" style="font-size: medium; font-family: Arial">
                                    <asp:TextBox ID="txtAplicacion" runat="server" CssClass="Text02" MaxLength="50" Width="208px"
                                        Enabled="False"></asp:TextBox>
                                </td>
                                <td align="right" style="font-size: medium; font-family: Arial">
                                    <asp:Label ID="lblTipoDado" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left" style="font-size: medium; font-family: Arial; width: 86px;">
                                    <asp:Label ID="lblValueTipoDado" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left" style="font-size: medium; font-family: Arial">
                                    <asp:Label ID="lblObligatorio" runat="server" CssClass="Lbl2"></asp:Label>
                                    &nbsp;
                                    <asp:Label ID="lblValueObligatorio" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right" style="font-size: medium; font-family: Arial">
                                    <asp:Label ID="lblNivel" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left" style="font-size: medium; font-family: Arial">
                                    <asp:TextBox ID="txtNivel" runat="server" CssClass="Text02" MaxLength="50" Width="118px"
                                        Enabled="False"></asp:TextBox>
                                </td>
                                <td align="right" style="font-size: medium; font-family: Arial">
                                    <asp:Label ID="lblAgrupacion" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left" style="font-size: medium; font-family: Arial;" colspan="2">
                                    <asp:UpdatePanel ID="UpdatePanelDdlAgrupacion" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlAgrupacion" runat="server" CssClass="Text02" Width="275px"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="csvDdlAgrupacionObrigatorio" runat="server" ControlToValidate="ddlAgrupacion"
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
                                    <asp:Label ID="lblCodParametro" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left" style="font-size: medium; font-family: Arial">
                                    <asp:TextBox ID="txtCodParametro" runat="server" CssClass="Text02" MaxLength="50"
                                        Width="118px" Enabled="False"></asp:TextBox>
                                </td>
                                <td align="right" style="font-size: medium; font-family: Arial">
                                    <asp:Label ID="lblDescripcionCorto" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left" style="font-size: medium; font-family: Arial;" colspan="2">
                                    <asp:UpdatePanel ID="UpdatePanelTxtDescripcionCorto" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtDescripcionCorto" runat="server" CssClass="Text02" MaxLength="50"
                                                Width="268px"></asp:TextBox>
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
                                                MaxLength="500" TextMode="MultiLine" Width="650"></asp:TextBox>
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
                                    <asp:Label ID="lblTipoComponent" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left" style="font-size: medium; font-family: Arial">
                                    <asp:TextBox ID="txtTipoComponent" runat="server" CssClass="Text02" MaxLength="50"
                                        Width="118px" Enabled="False"></asp:TextBox>
                                </td>
                                <td align="right" style="font-size: medium; font-family: Arial">
                                    <asp:Label ID="lblOrden" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left" style="font-size: medium; font-family: Arial" colspan="2">
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
                <asp:Panel ID="pnlOpciones" runat="server" Visible="false">
                    <tr>
                        <td class="titulo02">
                            <table cellpadding="0" cellspacing="3" border="0">
                                <tr>
                                    <td>
                                        <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSubTituloOpciones" runat="server"></asp:Label>
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
                            <table border="0" width="100%">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="">
                                        <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <pro:ProsegurGridView ID="ProsegurGridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                                    ColunasSelecao="CodigoOpcion" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
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
                                                    <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px"> &nbsp; </TextBox>
                                                    <Columns>
                                                        <asp:BoundField DataField="CodigoOpcion" HeaderText="Código A" SortExpression="CodigoOpcion" />
                                                        <asp:BoundField DataField="DescripcionOpcion" HeaderText="Descripción A" SortExpression="DescripcionOpcion" />
                                                        <asp:TemplateField HeaderText="Vigente">
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgVigente" runat="server" /></ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </pro:ProsegurGridView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnAltaOpcion" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="btnBajaOpcion" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="btnModificacionOpcion" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="btnConsultarOpcion" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlSemRegistro" runat="server" Visible="false">
                                <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro">
                                    <tr>
                                        <td style="border-width: 0;">
                                            
                                        </td>
                                        <td style="border-width: 0;">
                                            <asp:Label ID="lblSemRegistro" runat="server" Text="Label" CssClass="Lbl2">Não existem dados a serem exibidos.</asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 25%; text-align: center">
                                        <pro:Botao ID="btnAltaOpcion" runat="server" Habilitado="True" Tipo="Novo" Titulo="btnAlta"
                                            ExibirLabelBtn="True">
                                        </pro:Botao>
                                    </td>
                                    <td style="width: 25%; text-align: center">
                                        <pro:Botao ID="btnBajaOpcion" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                            Tipo="Excluir" Titulo="btnBaja">
                                        </pro:Botao>
                                    </td>
                                    <td style="width: 25%; text-align: center">
                                        <pro:Botao ID="btnModificacionOpcion" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                            Tipo="Editar" Titulo="btnModificacion">
                                        </pro:Botao>
                                    </td>
                                    <td style="width: 25%; text-align: center">
                                        <pro:Botao ID="btnConsultarOpcion" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                            Tipo="Localizar" Titulo="btnConsulta">
                                        </pro:Botao>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </asp:Panel>
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
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
