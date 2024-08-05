<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaDivisas.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaDivisas" EnableEventValidation="false"
    MasterPageFile="~/Master/Master.Master" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Busqueda de Divisas</title>
     
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
                            <table class="tabela_campos"  >
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCodigoIso" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCodigoIso" runat="server" MaxLength="15" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDescricaoDivisa" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtDescricaoDivisa" runat="server" Width="280px" MaxLength="50"
                                            CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:CheckBox ID="chkVigente" runat="server" Checked />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
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
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>
                </div>
                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblSubTitulosDivisas" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabela_interna">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pro:ProsegurGridView ID="GdvResultado" runat="server" AllowPaging="True" AllowSorting="True"
                                        ColunasSelecao="codigoiso" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
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
                            &nbsp;            
                                        </TextBox>
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemStyle HorizontalAlign="Center" Width="150"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditar" CssClass="imgButton" OnClick="imgEditar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultar" CssClass="imgButton" OnClick="imgConsultar_OnClick"/>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluir" CssClass="imgButton" OnClick="imgExcluir_OnClick" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="codigoiso" HeaderText="Código" SortExpression="codigoiso" />
                                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" SortExpression="descripcion" />
                                            <asp:BoundField DataField="codigosimbolo" HeaderText="Símbolo" SortExpression="codigosimbolo" />
                                            <asp:TemplateField HeaderText="vigente" SortExpression="vigente">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
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
                                                <td style="border-width: 0;">
                                                   
                                                </td>
                                                <td style="border-width: 0;">
                                                    <asp:Label ID="lblSemRegistro" runat="server" Text="Label" CssClass="Lbl2">Não existem dados a serem exibidos.</asp:Label>
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
                            <asp:Label ID="lblTituloDivisas" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table class="tabela_campos">
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigoDivisa" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCodigoDivisa" runat="server" MaxLength="15" AutoPostBack="true"
                                                CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                                            <asp:CustomValidator ID="csvCodigoObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtCodigoDivisa"
                                                Text="*"></asp:CustomValidator>
                                            <asp:CustomValidator ID="csvCodigoDivisaExistente" runat="server" ErrorMessage=""
                                                ControlToValidate="txtCodigoDivisa">*</asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDescricaoDivisaForm" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td colspan="4">
                                    <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtDescricaoDivisaForm" runat="server" Width="272px" MaxLength="50"
                                                AutoPostBack="true" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                                            <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ErrorMessage=""
                                                ControlToValidate="txtDescricaoDivisaForm" Text="*">*</asp:CustomValidator>
                                            <asp:CustomValidator ID="csvDescricaoDivisaExistente" runat="server" ErrorMessage=""
                                                ControlToValidate="txtDescricaoDivisaForm">*</asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblSimbolo" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSimbolo" runat="server" MaxLength="5" AutoPostBack="true" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblColor" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td>
                                    <table style="margin: 0px !Important;">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanelCodigoCor" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtColor" runat="server" Width="83px" MaxLength="15" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                                                        <cc1:ColorPickerExtender ID="txtColor_ColorPickerExtender" runat="server" Enabled="True"
                                                            OnClientColorSelectionChanged="colorChanged" TargetControlID="txtColor">
                                                        </cc1:ColorPickerExtender>
                                                        <asp:CustomValidator ID="csvCodigoColorObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtColor"
                                                            Text="*">*</asp:CustomValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td class="tamanho_celula">
                                                <asp:Label ID="lblCodigoAcesso" runat="server" CssClass="Lbl2"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanelCodigoAcesso" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtCodigoAcesso" runat="server" MaxLength="1" AutoPostBack="true"
                                                            CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="20px"></asp:TextBox>
                                                        <asp:CustomValidator ID="csvCodigoAcessoExiste" runat="server" ErrorMessage="" ControlToValidate="txtCodigoAcesso">*</asp:CustomValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigoAjeno" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodigoAjeno" runat="server" Width="225px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="True" ReadOnly="True" />
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDesCodigoAjeno" runat="server" class="tamanho_celula" align="right"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <table style="margin: 0px !Important;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDesCodigoAjeno" runat="server" Width="225px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="True" ReadOnly="True" />
                                            </td>
                                            <td>
                                                   <asp:Button runat="server" ID="btnAltaAjeno" Text="btnCodigoAjeno" CssClass="ui-button" Width="100"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblVigenteForm" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td colspan="5">
                                    <asp:CheckBox ID="chkVigenteForm" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <div style="margin-top: 20px;">
                            <div class="ui-panel-titlebar">
                                <asp:Label ID="lblSubTitulosDivisasForm" CssClass="ui-panel-title" runat="server"></asp:Label>
                            </div>
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <asp:UpdatePanel ID="UpdatePanelGridForm" runat="server">
                                            <ContentTemplate>
                                                <pro:ProsegurGridView ID="GdvDenominaciones" runat="server" AllowPaging="True" AllowSorting="False"
                                                    ColunasSelecao="Codigo" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
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
                                                    <HeaderStyle Font-Bold="True" />
                                                    <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                                    <RowStyle CssClass="GridLinhaPadraoImpar" />
                                                    <TextBox ID="objTextoProsegurGridView2" AutoPostBack="True" MaxLength="10" Width="30px"> </TextBox>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemStyle HorizontalAlign="Center" Width="150"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditar1" CssClass="imgButton" OnClick="imgEditar1_OnClick" />
                                                                <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultar1" CssClass="imgButton" OnClick="imgConsultar1_OnClick" />
                                                                <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluir1" CssClass="imgButton" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Codigo" HeaderText="Código" SortExpression="codigo" />
                                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                                                        <asp:BoundField DataField="Valor" HeaderText="Valor" SortExpression="Valor" />
                                                        <asp:BoundField DataField="Peso" HeaderText="Peso" SortExpression="Peso" />
                                                        <asp:TemplateField HeaderText="EsBillete" SortExpression="EsBillete">
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgBillete" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Vigente" SortExpression="Vigente">
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgVigente" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </pro:ProsegurGridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <asp:Panel ID="pnlSemRegistroForm" runat="server" Visible="false">
                                    <tr>
                                        <td align="center">
                                            <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro">
                                                <tr>
                                                    <td style="border-width: 0;">
                                                        </td>
                                                    <td style="border-width: 0;">
                                                        <asp:Label ID="lblSemRegistroForm" runat="server" Text="Label" CssClass="Lbl2">Não existem dados a serem exibidos.</asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </asp:Panel>
                            </table>
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
                        <asp:Button runat="server" ID="btnConsomeDenominaciones"/>
                        <asp:Button runat="server" ID="btnBajaDenominacion"/>
                         <asp:Button runat="server" ID="btnConsomeCodigoAjeno" />
                    </div>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn_cancelar"/>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
