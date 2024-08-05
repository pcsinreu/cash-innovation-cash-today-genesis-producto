<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoMorfologia.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoMorfologia" MasterPageFile="~/Principal.Master"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Mantenimiento de MediosPago</title>
     
    <link href="Css/bootstrap.css" rel="stylesheet" />
    <link href="Css/bootstrap-theme.css" rel="stylesheet" />
    <script src="JS/bootstrap.js" type="text/javascript"></script>
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
                            <asp:Label ID="lblTituloMorfologia" runat="server"></asp:Label>
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
                <table class="tabela_campos"   border="0px">
                    <tr>
                        <td class="espaco_inicial">&nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblCodigo" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="Text02" MaxLength="15" AutoPostBack="true"></asp:TextBox>
                                    <asp:CustomValidator ID="csvCodigoObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtCodigo"
                                        Text="*"></asp:CustomValidator>
                                    <asp:CustomValidator ID="csvCodigoExistente" runat="server" ErrorMessage="" ControlToValidate="txtCodigo">*</asp:CustomValidator>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="txtCodigo" EventName="TextChanged" />
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
                            <asp:Label ID="lblDescricao" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtDescricao" runat="server" Width="272px" MaxLength="50" AutoPostBack="true"
                                        CssClass="Text02"></asp:TextBox>
                                    <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ErrorMessage=""
                                        ControlToValidate="txtDescricao" Text="*">*</asp:CustomValidator>
                                    <asp:CustomValidator ID="csvDescricaoExistente" runat="server" ErrorMessage="" ControlToValidate="txtDescricao">*</asp:CustomValidator>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="txtDescricao" EventName="TextChanged" />
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
                            <asp:Label ID="lblMetodoHabilitacion" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlMetodoHabilitacion" runat="server" Width="190px" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="csvMetodoHabilitacionObrigatorio" runat="server" ErrorMessage=""
                                        ControlToValidate="ddlMetodoHabilitacion" Text="*">*</asp:CustomValidator>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlMetodoHabilitacion" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblAtivo" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:CheckBox ID="chkAtivo" runat="server" />
                        </td>
                        <td></td>
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
                            <asp:Label ID="lblTituloComponentes" runat="server"></asp:Label>
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
                <table border="0" width="100%">
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td style="">
                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pro:ProsegurGridView ID="pgvComponentes" runat="server" AllowPaging="True" AllowSorting="True"
                                        ColunasSelecao="OidMorfologiaComponente" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                                        PageSize="10" AutoGenerateColumns="False" Ajax="True" Width="95%" ExibirCabecalhoQuandoVazio="false"
                                        Height="100%" GerenciarControleManualmente="true">
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
                                        <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </TextBox>
                                        <Columns>
                                            <asp:BoundField DataField="Codigo" HeaderText="" SortExpression="" />
                                            <asp:BoundField DataField="DesFuncionContenedor" HeaderText="" SortExpression="" />
                                            <asp:BoundField DataField="DesTipoContenedor" HeaderText="" SortExpression="" />
                                            <asp:BoundField DataField="DesDivisaMedioPago" HeaderText="" SortExpression="" />
                                            <asp:BoundField DataField="DesOrden" HeaderText="" SortExpression="" />
                                        </Columns>
                                    </pro:ProsegurGridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAlta" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnBaja" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnConsulta" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnAcima" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnAbaixo" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" style="width: 50px">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnAcima" runat="server" Height="23px" ImageUrl="~/Imagenes/pag03.png"
                                                    Width="25px" />&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnAbaixo" runat="server" ImageUrl="~/Imagenes/pag02.png" />&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>&nbsp;
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


    <div id="modalMorfologia" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="exampleModalLabel">Manutenção de Componentes</h4>
                </div>
                <iframe id="frameContent" frameborder="0" style="height: 470px; background-image: url(imagenes/bg.jpg); width: 100%;"></iframe>
            </div>
        </div>
    </div>

</asp:Content>
