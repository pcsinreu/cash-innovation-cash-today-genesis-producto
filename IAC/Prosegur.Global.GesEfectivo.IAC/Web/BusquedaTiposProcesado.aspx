<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaTiposProcesado.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaTiposProcesado" EnableEventValidation="false"
    MasterPageFile="~/Master/Master.Master" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title></title>
     
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
       <asp:HiddenField runat="server" id="hiddenCodigo"/>
        <asp:UpdatePanel ID="UpdatePanelGeral" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
            <ContentTemplate>
                <div id="Filtros">
                    <fieldset id="ExpandCollapseDiv" class="ui-fieldset ui-widget ui-widget-content ui-corner-all ui-fieldset-toggleable">
                        <legend class="ui-fieldset-legend ui-corner-all ui-state-default ui-state-active">
                            <div id="DivFiltros" class="legend-expand" onclick="ocultarExibir();">
                                <asp:Label ID="lblTitulosTiposProcesado" CssClass="legent-text" runat="server">Filtrar Clientes</asp:Label>
                            </div>
                        </legend>
                        <div class="accordion" style="display: none;">
                            <table class="tabela_campos">
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCodigoTipoProcesado" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCodigoTipoProcesado" runat="server" Width="150px" MaxLength="15" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDescricaoTipoProcesado" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtDescricaoTipoProcesado" runat="server" Width="250px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCaracteristicas" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:ListBox ID="lstCaracteristicas" runat="server" SelectionMode="Multiple" Width="420px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:ListBox>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblVigente" runat="server" CssClass="label2" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkVigente" runat="server" Checked="true" CssClass="label2" />
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
                    <asp:Label ID="lblSubTitulosTiposProcesado" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center" width="100%">
                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pro:ProsegurGridView ID="ProsegurGridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                        ColunasSelecao="Codigo" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                                        PageSize="10" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                                        NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0" PaginacaoAutomatica="True"
                                        Width="99%" Height="100%" ExibirCabecalhoQuandoVazio="false">
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
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditar" OnClick="imgEditar_OnClick" CssClass="imgButton" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultar" OnClick="imgConsultar_OnClick" CssClass="imgButton" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluir" OnClick="imgExcluir_OnClick" CssClass="imgButton" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Codigo" HeaderText="Codigo" SortExpression="Codigo" />
                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                                            <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" SortExpression="Observaciones" />
                                            <asp:TemplateField HeaderText="Vigente" SortExpression="vigente">
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
                    <asp:Panel runat="server" ID="pnForm">
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloTiposProcesadoForm" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table class="tabela_campos">
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigoTiposProcesado" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <table cellpadding="0px" cellspacing="0px" width="100%">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtCodigoTiposProcesado" runat="server" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                                            MaxLength="15" Width="150px"></asp:TextBox>
                                                        <asp:CustomValidator ID="csvCodigoObrigatorio" runat="server" ControlToValidate="txtCodigoTiposProcesado"
                                                            ErrorMessage="001_msg_canalcodigoobrigatorio" Text="*"></asp:CustomValidator>
                                                        <asp:CustomValidator ID="csvCodigoExistente" runat="server" ControlToValidate="txtCodigoTiposProcesado"
                                                            ErrorMessage="001_msg_canalcodigoobrigatorio" Text="*"></asp:CustomValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDescricaoTiposProcesado" runat="server" CssClass="label2" Width="134px"></asp:Label>
                                </td>
                                <td>
                                    <table cellpadding="0px" cellspacing="0px" width="100%">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtDescricaoTiposProcesado" runat="server" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                                            MaxLength="50" Width="255px"></asp:TextBox>
                                                        <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ControlToValidate="txtDescricaoTiposProcesado"
                                                            ErrorMessage="001_msg_canaldescripcionobrigatorio" Text="*"></asp:CustomValidator>
                                                        <asp:CustomValidator ID="csvDescricaoExistente" runat="server" ControlToValidate="txtCodigoTiposProcesado"
                                                            ErrorMessage="001_msg_canalcodigoobrigatorio" Text="*"></asp:CustomValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblObservaciones" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtObservaciones" runat="server" MaxLength="4000" AutoPostBack="false"
                                        CssClass="ui-fieldset ui-widget ui-widget-content ui-corner-all ui-fieldset-toggleable" Height="96px" Width="580px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblVigenteForm" runat="server" CssClass="label2" Text="lblVigente"></asp:Label>
                                </td>
                                <td align="left" style="width: 201px;">
                                    <asp:CheckBox ID="ckVigenteForm" runat="server" AutoPostBack="false" />
                                </td>
                            </tr>
                        </table>
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloCaracteristicas" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table class="tabela_campos"  >
                            <tr>
                                <td align="right" width="420px">
                                        <asp:ListBox ID="lstCaracteristicasDisponiveis" runat="server" Height="120px" SelectionMode="Multiple"
                                            Width="420px"></asp:ListBox>
                                    </td>
                                <td align="center" width="68px">
                                    <asp:ImageButton ID="imbAdicionarTodasCaracteristicas" runat="server" ImageUrl="~/Imagenes/pag07.png"
                                        Style="height: 25px" /><br />
                                    <asp:ImageButton ID="imbAdicionarCaracteristicasSelecionadas" runat="server" ImageUrl="~/Imagenes/pag05.png" /><br />
                                    <asp:ImageButton ID="imbRemoverCaracteristicasSelecionadas" runat="server" ImageUrl="~/Imagenes/pag06.png" /><br />
                                    <asp:ImageButton ID="imbRemoverTodasCaracteristicas" runat="server" ImageUrl="~/Imagenes/pag08.png" />
                                </td>
                                <td align="left" width="420px">
                                    <asp:ListBox ID="lstCaracteristicasSelecionadas" runat="server" Height="120px" SelectionMode="Multiple"
                                        Width="420px"></asp:ListBox>
                                </td>
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
                    <asp:Button runat="server" ID="btnSalvar" CssClass="btn-salvar"/>
                </td>
                <td >
                     <asp:Button runat="server" ID="btnBajaConfirm"  CssClass="btn-excluir"/>
                    <div class="botaoOcultar">
                         <asp:Button runat="server" ID="btnBaja" CssClass="btn-excluir"/>
                    </div>
                 
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn_cancelar"/>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>