<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaCaracteristicas.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaCaracteristicas" EnableEventValidation="false"
    MasterPageFile="~/Master/Master.Master" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register Src="Controles/Erro.ascx" TagName="Erro" TagPrefix="uc1" %>
<%@ Register Src="Controles/Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Busqueda Caracteristicas</title>
     
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
                                <asp:Label ID="lblTituloCaracteristicas" CssClass="legent-text" runat="server">Filtrar Clientes</asp:Label>
                            </div>
                        </legend>
                        <div class="accordion" style="display: none;">
                            <table class="tabela_campos"  >
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCodigo" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCodigo" runat="server" MaxLength="15" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDescricao" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDescricao" runat="server" Width="260px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCodigoConteo" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCodigoConteo" runat="server" MaxLength="15" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblVigente" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkVigente" runat="server" Checked="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <table style="margin: 0px !important">
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
                    <asp:Label ID="lblSubTitulosClientes" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pro:ProsegurGridView ID="ProsegurGridView" runat="server" AllowPaging="True" AllowSorting="True"
                                        ColunasSelecao="Codigo" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                                        AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True" NumeroRegistros="0"
                                        OrdenacaoAutomatica="True" PaginaAtual="0" PaginacaoAutomatica="True" Width="99%"
                                        Height="100%" ExibirCabecalhoQuandoVazio="False">
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
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Codigo" HeaderText="Codigo" SortExpression="Codigo" />
                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                                            <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" SortExpression="Observaciones" />
                                            <asp:BoundField DataField="CodigoConteo" HeaderText="CodigoConteo" SortExpression="CodigoConteo">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
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
            <asp:UpdatePanel runat="server" ID="updForm" >
                <ContentTemplate>
                    <asp:Panel runat="server" ID="pnForm" Visible="False">
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloForm" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table style="width: 768px;"   border="0">
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigoCaracteristica" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodigoCaracteristica" runat="server" MaxLength="15" AutoPostBack="True" OnTextChanged="txtCodigoCaracteristica_OnTextChanged" CssClass="ui-fieldset ui-widget ui-widget-content ui-corner-all ui-fieldset-toggleable ui-gn-mandatory"></asp:TextBox><asp:CustomValidator
                                        ID="csvCodigoCaracteristicaObrigatorio" runat="server" ControlToValidate="txtCodigoCaracteristica">*</asp:CustomValidator><asp:CustomValidator
                                            ID="csvCodigoCaracteristica" runat="server" ErrorMessage="" ControlToValidate="txtCodigoCaracteristica">*</asp:CustomValidator>
                                </td>
                                <td class="tamanho_celula" style="width: 140px !important;">
                                    <asp:Label ID="lblDescricaoForm" runat="server" CssClass="label2" ></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescricaoForm" runat="server" Width="205px" MaxLength="50" AutoPostBack="True" OnTextChanged="txtDescricaoForm_OnTextChanged" CssClass="ui-fieldset ui-widget ui-widget-content ui-corner-all ui-fieldset-toggleable ui-gn-mandatory"></asp:TextBox>
                                    <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ControlToValidate="txtDescricaoForm">*</asp:CustomValidator><asp:CustomValidator
                                            ID="csvDescripcion" runat="server" ErrorMessage="" ControlToValidate="txtDescricaoForm">*</asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigoconteoForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td colspan="3">

                                    <asp:TextBox ID="txtCodigoConteoForm" runat="server" MaxLength="15" AutoPostBack="True" OnTextChanged="txtCodigoConteoForm_OnTextChanged" CssClass="ui-fieldset ui-widget ui-widget-content ui-corner-all ui-fieldset-toggleable ui-gn-mandatory"></asp:TextBox><asp:CustomValidator
                                        ID="csvCodigoConteoObrigatorio" runat="server" ControlToValidate="txtCodigoConteoForm">*</asp:CustomValidator><asp:CustomValidator
                                            ID="csvCodigoConteo" runat="server" ErrorMessage="" ControlToValidate="txtCodigoConteoForm">*</asp:CustomValidator>

                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblObservaciones" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtObservaciones" runat="server" MaxLength="500" TextMode="MultiLine"
                                        CssClass="Text02" Height="76px" Width="610px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblVigenteForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:CheckBox ID="ckbVigenteForm" runat="server" />
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
                    <asp:Button runat="server" ID="btnSalvar" CssClass="btn-salvar"/>
                </td>
                <td>
                        <asp:Button runat="server" ID="btnCancelar" CssClass="btn_cancelar"/>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
