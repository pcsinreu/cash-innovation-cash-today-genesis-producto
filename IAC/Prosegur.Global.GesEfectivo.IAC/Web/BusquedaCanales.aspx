<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaCanales.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaCanales" EnableEventValidation="false"
    MasterPageFile="~/Master/Master.Master" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Busqueda Canales</title>

    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ocultarExibir() {
            $("#DivFiltros").toggleClass("legend-collapse");
            $(".accordion").slideToggle("fast");
        };
        function ManterFiltroAberto() {
            $("#DivFiltros").addClass("legend-expand");
            $(".accordion").show();
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <asp:HiddenField runat="server" ID="hiddenCodigo" />
        <asp:HiddenField runat="server" ID="hiddenCodigoForm" />
        <asp:UpdatePanel ID="UpdatePanelGeral" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
            <ContentTemplate>
                <div id="Filtros" style="display: block;">
                    <fieldset id="ExpandCollapseDiv" class="ui-fieldset ui-widget ui-widget-content ui-corner-all ui-fieldset-toggleable">
                        <legend class="ui-fieldset-legend ui-corner-all ui-state-default ui-state-active">
                            <div id="DivFiltros" class="legend-expand" onclick="ocultarExibir();">
                                <asp:Label ID="lblSubTitulosCriteriosBusqueda" CssClass="legent-text" runat="server">Filtrar Clientes</asp:Label>
                            </div>
                        </legend>
                        <div class="accordion" style="display: none;">
                            <table class="tabela_campos">
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCodigoCanal" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCodigoCanal" runat="server" MaxLength="15" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDescricaoCanal" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDescricaoCanal" runat="server" Width="227px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblVigente" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:CheckBox ID="chkVigente" runat="server" Checked="true" />
                                    </td>
                                </tr>
                            </table>
                            <table style="margin: 0px !Important;">
                                <tr>
                                    <td>
                                        <asp:Button runat="server" ID="btnBuscar" CssClass="ui-button" Width="100px" />
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="btnLimpar" CssClass="ui-button" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>
                </div>
                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblSubTitulosCanales" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pro:ProsegurGridView ID="ProsegurGridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                        ColunasSelecao="codigo" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                                        PageSize="10" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                                        ExibirCabecalhoQuandoVazio="false" NumeroRegistros="0" OrdenacaoAutomatica="True"
                                        PaginaAtual="0" PaginacaoAutomatica="True" Width="99%">
                                        <Pager ID="objPager_ProsegurGridView1">
                                            <FirstPageButton Visible="True">
                                            </FirstPageButton>
                                            <LastPageButton Visible="True">
                                            </LastPageButton>
                                            <Summary Text="Página {0} de {1} ({2} itens)" />
                                            <SummaryStyle>
                                            </SummaryStyle>
                                        </Pager>
                                        <HeaderStyle Font-Bold="True" />
                                        <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                        <RowStyle CssClass="GridLinhaPadraoImpar" />
                                        <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px">                                                
                                        </TextBox>
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemStyle HorizontalAlign="Center" Width="150"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditar" CssClass="imgButton" OnClick="imgEditar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultar" CssClass="imgButton" OnClick="imgConsultar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluir" CssClass="imgButton" OnClick="imgExcluir_OnClick" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="codigo" HeaderText="Código" SortExpression="codigo" />
                                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" SortExpression="descripcion" />
                                            <asp:BoundField DataField="observaciones" HeaderText="Observación" SortExpression="observaciones" />
                                            <asp:TemplateField HeaderText="vigente" SortExpression="vigente">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Image ID="imgVigente" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </pro:ProsegurGridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanelGridSemRegistro" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="False">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlSemRegistro" runat="server" Visible="false">
                                        <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro">
                                            <tr>
                                                <td style="border-width: 0;"></td>
                                                <td style="border-width: 0;">
                                                    <asp:Label ID="lblSemRegistro" runat="server" Text="Label" CssClass="label2">Não existem dados a serem exibidos.</asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div style="margin-top: 20px;">
            <asp:UpdatePanel runat="server" ID="updForm">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="pnForm" Visible="True">
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloCanales" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table class="tabela_campos"  >
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigoCanalForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCodigoCanalForm" runat="server" MaxLength="15" AutoPostBack="False"
                                                CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" IdDivExibicao="DivCodigo"></asp:TextBox>
                                            <asp:CustomValidator ID="csvCodigoObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtCodigoCanalForm"
                                                Text="*"></asp:CustomValidator>
                                            <asp:CustomValidator ID="csvCodigoCanalExistente" runat="server" ErrorMessage=""
                                                ControlToValidate="txtCodigoCanalForm">*</asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDescricaoCanalForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtDescricaoCanalForm" runat="server" Width="398px" MaxLength="50" AutoPostBack="False"
                                                CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" IdDivExibicao="DivDescricao"></asp:TextBox>
                                            <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ErrorMessage="001_msg_canaldescripcionobrigatorio"
                                                ControlToValidate="txtDescricaoCanalForm" Text="*"></asp:CustomValidator>
                                            <asp:CustomValidator ID="csvDescripcionExistente" runat="server" ErrorMessage=""
                                                ControlToValidate="txtDescricaoCanalForm">*</asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigoAjeno" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodigoAjeno" runat="server" MaxLength="25" AutoPostBack="False"
                                        CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" IdDivExibicao="DivCodigo" Enabled="False"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDescriAjeno" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescricaoAjeno" runat="server" Width="230px"
                                        MaxLength="50" AutoPostBack="False"
                                        CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" IdDivExibicao="DivDescricao" Enabled="False"
                                        ReadOnly="True"></asp:TextBox>
                                    <asp:Button runat="server" ID="btnAltaAjeno" CssClass="ui-button" Width="150" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblObservaciones" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtObservaciones" runat="server" MaxLength="4000" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                        Height="96px" Width="760px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblVigenteForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td colspan="4">
                                    <asp:CheckBox ID="chkVigenteForm" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblSubTitulosCanalesForm" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <div>
                            <asp:UpdatePanel ID="updGridForm" runat="server">
                                <ContentTemplate>
                                    <pro:ProsegurGridView ID="GridSubCanales" runat="server" AllowPaging="True" AllowSorting="True"
                                        ColunasSelecao="codigo" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                                        PageSize="10" AutoGenerateColumns="False" Ajax="True" Width="99%" ExibirCabecalhoQuandoVazio="false"
                                        Height="100%">
                                        <Pager ID="objPager_ProsegurGridView2">
                                            <FirstPageButton Visible="True">
                                            </FirstPageButton>
                                            <LastPageButton Visible="True">
                                            </LastPageButton>
                                            <Summary Text="Página {0} de {1} ({2} itens)" />
                                            <SummaryStyle>
                                            </SummaryStyle>
                                        </Pager>
                                        <HeaderStyle CssClass="GridTitulo" Font-Bold="True" />
                                        <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                        <RowStyle CssClass="GridLinhaPadraoImpar" />
                                        <TextBox ID="objTextoProsegurGridView2" AutoPostBack="True" MaxLength="10" Width="30px"> </TextBox>
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemStyle HorizontalAlign="Center" Width="150"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditarForm" OnClick="imgEditarForm_OnClick" CssClass="imgButton" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultarForm" CssClass="imgButton" OnClick="imgConsultarForm_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluirForm" CssClass="imgButton" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="codigo" HeaderText="Código" SortExpression="codigo" />
                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                                            <asp:BoundField DataField="observaciones" HeaderText="Observación" />
                                            <asp:TemplateField HeaderText="Vigente" SortExpression="Vigente">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Image ID="imgVigente" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </pro:ProsegurGridView>
                                    <asp:Panel ID="pnSemRegistroForm" runat="server" Visible="false">
                                        <tr>
                                            <td align="center">
                                                <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro">
                                                    <tr>
                                                        <td style="border-width: 0;"></td>
                                                        <td style="border-width: 0;">
                                                            <asp:Label ID="lblSemRegistroForm" runat="server" Text="Label" CssClass="label2">Não existem dados a serem exibidos.</asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="cphBotonesRodapie">
    <center>
        <table>
            <tr align="center">
                <td>
                    <asp:Button runat="server" ID="btnNovo" CssClass="btn-novo"/>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnSalvar" CssClass="btn-salvar"/>
                </td>
                <td >
                     <asp:Button runat="server" ID="btnBajaConfirm"  CssClass="btn-excluir"/>
                    <div class="botaoOcultar">
                        <asp:Button runat="server" ID="btnBaja" CssClass="btn-excluir"/>
                         <asp:Button runat="server" ID="btnBajaSubCanal" CssClass="btn-excluir"/>
                        <asp:Button runat="server" ID="btnConsomeCodigoAjeno" CssClass="btn-excluir"/>
                        <asp:Button runat="server" ID="btnConsomeSubCanal" CssClass="btn-excluir"/>
                    </div>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn_cancelar"/>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
