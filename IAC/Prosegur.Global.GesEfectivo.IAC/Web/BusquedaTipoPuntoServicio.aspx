<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaTipoPuntoServicio.aspx.vb" 
Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BuesquedaTipoPuntoServicio" 
MasterPageFile="~/Master/Master.Master"%>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
 <%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Busqueda de Tipo Puntos Servicios</title>
     
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
    <asp:UpdatePanel ID="UpdatePanelGeral" runat="server">
        <ContentTemplate>
            <div id="Filtros">
                <fieldset id="ExpandCollapseDiv" class="ui-fieldset ui-widget ui-widget-content ui-corner-all ui-fieldset-toggleable">
                    <legend class="ui-fieldset-legend ui-corner-all ui-state-default ui-state-active">
                        <div id="DivFiltros" class="legend-expand" onclick="ocultarExibir();">
                            <asp:Label ID="lblTitulosCriteriosBusqueda" CssClass="legent-text" runat="server">Filtrar Clientes</asp:Label>
                        </div>
                    </legend>
                    <div class="accordion" style="display: none;">
                        <table class="tabela_campos">
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigoTpPuntoServicio" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodigoTpServicio" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" 
                                        Width="155px" MaxLength="15"></asp:TextBox>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDescricao" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescricao" runat="server" Width="255px" MaxLength="50"
                                        CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblVigente" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td colspan="3" >
                                    <asp:CheckBox ID="chkVigente" runat="server" Checked="" />
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
                <asp:Label ID="lblSubTitulosPuntoServicos" CssClass="ui-panel-title" runat="server"></asp:Label>
            </div>
               <table class="tabela_interna">
                <tr>
                    <td align="center">
                        <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <pro:ProsegurGridView ID="GdvResultado" runat="server" AllowPaging="True" AllowSorting="True"
                                    ColunasSelecao="codTipoPuntoServicio" EstiloDestaque="GridLinhaDestaque" 
                                    GridPadrao="False" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                                    ExibirCabecalhoQuandoVazio="False" NumeroRegistros="0" OrdenacaoAutomatica="True"
                                    PaginaAtual="0" PaginacaoAutomatica="True" Width="99%" 
                                    AgruparRadioButtonsPeloName="False" 
                                    ConfigurarNumeroRegistrosManualmente="False" EnableModelValidation="True" 
                                    HeaderSpanStyle="">
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
                                                <ItemStyle HorizontalAlign="Center" Width="210"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditar" CssClass="imgButton" OnClick="imgEditar_OnClick"  />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultar" CssClass="imgButton" OnClick="imgConsultar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluir" CssClass="imgButton" OnClick="imgExcluir_OnClick"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        <asp:BoundField DataField="codTipoPuntoServicio" HeaderText="codigo" SortExpression="codTipoPuntoServicio" />
                                        <asp:BoundField DataField="desTipoPuntoServicio" HeaderText="descripcion" SortExpression="desTipoPuntoServicio" />
                                        <asp:TemplateField HeaderText="BolActivo">
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
    </asp:UpdatePanel>
        <div style="margin-top: 20px;">
            <asp:UpdatePanel runat="server" ID="updForm">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="pnForm" Visible="False">
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloTipoPunto" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table class="tabela_campos"  >
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodTipoPunto" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>

                                    <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCodigoTipoPunto" runat="server" MaxLength="15" AutoPostBack="False"
                                                CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="150px"></asp:TextBox>&nbsp;<asp:CustomValidator ID="csvCodigoTipoCliente"
                                                    runat="server" ErrorMessage=""
                                                    ControlToValidate="txtCodigoTipoPunto" Text="*"></asp:CustomValidator>
                                            <asp:CustomValidator ID="csvCodigoExistente"
                                                runat="server" ErrorMessage="" ControlToValidate="txtCodigoTipoPunto" Text="*">*</asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDescripcion" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td class="style4">
                                    <asp:UpdatePanel ID="UpdatePanelDescripcion" runat="server" >
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtDescricaoTipPunto" runat="server" Width="225px"
                                                MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" AutoPostBack="False" />
                                            <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ErrorMessage=""
                                                ControlToValidate="txtDescricaoTipPunto" Text="*"></asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblVigenteForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td class="style4">
                                    <asp:CheckBox ID="chkVigenteForm" runat="server" />
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
                    <asp:Button runat="server" ID="btnGrabar" CssClass="btn-salvar"/>
                </td>
                <td >
                     <asp:Button runat="server" ID="btnBajaConfirm"  CssClass="btn-excluir"/>
                    <div class="botaoOcultar">
                         <asp:Button runat="server" ID="btnBaja" CssClass="btn-excluir"/>
                        <asp:Button runat="server" ID="btnConsomeCodigoAjeno" CssClass="btn-excluir"/>
                    </div>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn_cancelar"/>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>