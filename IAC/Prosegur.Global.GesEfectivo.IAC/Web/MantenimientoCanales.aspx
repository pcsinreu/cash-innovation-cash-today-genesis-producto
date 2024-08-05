<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoCanales.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoCanales" MasterPageFile="~/Principal.Master"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Mantenimiento de Canales</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 257px;
        }
        .style2
        {
            width: 297px;
        }
    </style>
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
                            <asp:Label ID="lblTituloCanales" runat="server"></asp:Label>
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
                        <td align="right">
                            <asp:Label ID="lblCodigoCanal" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtCodigoCanal" runat="server" MaxLength="15" AutoPostBack="true"
                                        CssClass="Text02" IdDivExibicao="DivCodigo"></asp:TextBox>
                                    <asp:CustomValidator ID="csvCodigoObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtCodigoCanal"
                                        Text="*"></asp:CustomValidator>
                                    <asp:CustomValidator ID="csvCodigoCanalExistente" runat="server" ErrorMessage=""
                                        ControlToValidate="txtCodigoCanal">*</asp:CustomValidator>
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
                        <td class="style2" align="right">
                            <asp:Label ID="lblDescricaoCanal" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtDescricaoCanal" runat="server" Width="230px" MaxLength="50" AutoPostBack="true"
                                        CssClass="Text02" IdDivExibicao="DivDescricao"></asp:TextBox>
                                    <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ErrorMessage="001_msg_canaldescripcionobrigatorio"
                                        ControlToValidate="txtDescricaoCanal" Text="*"></asp:CustomValidator>
                                    <asp:CustomValidator ID="csvDescripcionExistente" runat="server" ErrorMessage=""
                                        ControlToValidate="txtDescricaoCanal">*</asp:CustomValidator>
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
                        <td class="espaco_inicial">
                            &nbsp;</td>
                        <td align="right">
                            <asp:Label ID="lblCodigoAjeno" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                                    <asp:TextBox ID="txtCodigoAjeno" runat="server" MaxLength="25" AutoPostBack="true"
                                        CssClass="Text02" IdDivExibicao="DivCodigo" Width="80%" Enabled="False" 
                                        ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="width: 20px;">
                            &nbsp;</td>
                        <td class="style2" align="right">
                            <asp:Label ID="lblDescriAjeno" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td class="style1">
                                    <asp:TextBox ID="txtDescricaoAjeno" runat="server" Width="230px" 
                                MaxLength="50" AutoPostBack="true"
                                        CssClass="Text02" IdDivExibicao="DivDescricao" Enabled="False" 
                                        ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="width: 20px;">
                                    <pro:Botao ID="btnAjeno" runat="server" Habilitado="True"
                                        Tipo="Novo" Titulo="btnCodigoAjeno">
                                    </pro:Botao>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblObservaciones" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtObservaciones" runat="server" MaxLength="4000" CssClass="Text02"
                                Height="96px" Width="628px" TextMode="MultiLine"></asp:TextBox>
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
                        <td class="style1">
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
                            <asp:Label ID="lblSubTitulosCanales" runat="server"></asp:Label>
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
                <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <pro:ProsegurGridView ID="ProsegurGridView1" runat="server" AllowPaging="True" AllowSorting="True"
                            ColunasSelecao="codigo" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
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
                            <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </TextBox>
                            <Columns>
                                <asp:BoundField DataField="codigo" HeaderText="Código" SortExpression="codigo" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                                <asp:BoundField DataField="observaciones" HeaderText="Observación" />
                                <asp:TemplateField HeaderText="Vigente" SortExpression="Vigente">
                                    <ItemTemplate>
                                        <asp:Image ID="imgVigente" runat="server" /></ItemTemplate>
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
                <asp:UpdatePanel ID="UpdatePanelBtnsGrid" runat="server" UpdateMode="Conditional">
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
