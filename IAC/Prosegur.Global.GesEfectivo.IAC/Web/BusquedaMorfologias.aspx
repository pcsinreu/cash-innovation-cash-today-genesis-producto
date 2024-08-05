<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaMorfologias.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaMorfologias" EnableEventValidation="false"
    MasterPageFile="~/Master/Master.Master" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
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
        <asp:UpdatePanel ID="UpdatePanelGeral" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="Filtros">
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
                                        <asp:Label ID="lblCodigo" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCodigo" runat="server" MaxLength="15" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDescricao" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDescricao" runat="server" Width="280px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:CheckBox ID="chkVigente" runat="server" Checked />
                                    </td>
                                </tr>
                            </table>
                            <table>
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
                    <asp:Label ID="lblSubTitulosDivisas" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">

                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pro:ProsegurGridView ID="GdvResultado" runat="server" AllowPaging="True" AllowSorting="True"
                                        ColunasSelecao="OidMorfologia" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
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
                                        <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px"></TextBox>
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemStyle HorizontalAlign="Center" Width="130"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultar" CssClass="imgButton" OnClick="imgConsultar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluir" CssClass="imgButton" OnClick="imgExcluir_OnClick" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CodMorfologia" HeaderText="" SortExpression="CodMorfologia" />
                                            <asp:BoundField DataField="DesMorfologia" HeaderText="" SortExpression="DesMorfologia" />
                                            <asp:BoundField DataField="DescripcionComponentes" HeaderText="" SortExpression="DescripcionComponentes" />
                                            <asp:TemplateField HeaderText="BolVigente" SortExpression="BolVigente">
                                                <ItemStyle HorizontalAlign="Center" Width="130"></ItemStyle>
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
                            <asp:Label ID="lblTituloMorfologia" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table class="tabela_campos"   border="0px">
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigoForm" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCodigoForm" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" MaxLength="15" AutoPostBack="False"></asp:TextBox>
                                            <asp:CustomValidator ID="csvCodigoObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtCodigoForm"
                                                Text="*"></asp:CustomValidator>
                                            <asp:CustomValidator ID="csvCodigoExistente" runat="server" ErrorMessage="" ControlToValidate="txtCodigoForm">*</asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDescricaoForm" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtDescricaoForm" runat="server" Width="272px" MaxLength="50" AutoPostBack="False"
                                                CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                                            <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ErrorMessage=""
                                                ControlToValidate="txtDescricaoForm" Text="*">*</asp:CustomValidator>
                                            <asp:CustomValidator ID="csvDescricaoExistente" runat="server" ErrorMessage="" ControlToValidate="txtDescricaoForm">*</asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblMetodoHabilitacion" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlMetodoHabilitacion" runat="server" Width="190px" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="csvMetodoHabilitacionObrigatorio" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlMetodoHabilitacion" Text="*">*</asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblAtivo" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkAtivo" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloComponentes" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table border="0" width="100%">
                            <tr>
                                <td style="">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <pro:ProsegurGridView ID="pgvComponentes" runat="server" AllowPaging="True" AllowSorting="False"
                                                ColunasSelecao="OidMorfologiaComponente" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                                                PageSize="10" AutoGenerateColumns="False" Ajax="True" Width="99%" ExibirCabecalhoQuandoVazio="false"
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
                                                <HeaderStyle Font-Bold="True" />
                                                <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                                <RowStyle CssClass="GridLinhaPadraoImpar" />
                                                <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </TextBox>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemStyle HorizontalAlign="Center" Width="130"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultarForm" CssClass="imgButton" OnClick="imgConsultarForm_OnClick" />
                                                            <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluirForm" CssClass="imgButton" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Codigo" HeaderText="" SortExpression="" />
                                                    <asp:BoundField DataField="DesFuncionContenedor" HeaderText="" SortExpression="" />
                                                    <asp:BoundField DataField="DesTipoContenedor" HeaderText="" SortExpression="" />
                                                    <asp:BoundField DataField="DesDivisaMedioPago" HeaderText="" SortExpression="" />
                                                    <asp:BoundField DataField="DesOrden" HeaderText="" SortExpression="" />
                                                </Columns>
                                            </pro:ProsegurGridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <asp:Panel runat="server" ID="pnBotoesGrid">
                                <td align="left" style="width: 50px">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
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
                                </asp:Panel>
                            </tr>
                        </table>
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
                    <asp:Button runat="server" ID="btnInserirComponente" CssClass="btn-novo"/>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnGrabar" CssClass="btn-salvar"/>
                </td>
                <td >
                     <asp:Button runat="server" ID="btnBajaConfirm"  CssClass="btn-excluir"/>
                    <div class="botaoOcultar">
                         <asp:Button runat="server" ID="btnBaja" CssClass="btn-excluir"/>
                        <asp:Button runat="server" ID="btnAtualizaComponentes" CssClass="btn-excluir"/>
                        <asp:Button runat="server" ID="btnBajaComponente" CssClass="btn-excluir"/>
                    </div>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn_cancelar"/>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
