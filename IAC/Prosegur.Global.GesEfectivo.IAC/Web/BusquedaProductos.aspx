<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaProductos.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaProductos" EnableEventValidation="false"
    MasterPageFile="~/Master/Master.Master" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Busqueda Productos</title>
     
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
                        <asp:Label ID="lblTitulosProductos" CssClass="legent-text" runat="server">Filtrar Clientes</asp:Label>
                    </div>
                </legend>
                <div class="accordion" style="display: none;">
                     <table class="tabela_campos"  >
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigoProducto" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td >
                                    <asp:TextBox ID="txtCodigoProducto" runat="server" Width="200px" MaxLength="15" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                </td>
                                <td class="tamanho_celula" >
                                    <asp:Label ID="lblDescricaoProductos" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td >
                                    <asp:TextBox ID="txtDescricaoProducto" runat="server" Width="249px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all "></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblMaquina" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlMaquina" runat="server" Width="208px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                                    </asp:DropDownList>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblManual" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td >
                                    <asp:CheckBox ID="chkManual" runat="server" />
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
                            <tr>
                                <td colspan="5">
                                    <table style="margin:0px !important">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdateBtnBuscar" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button runat="server" ID="btnBuscar" CssClass="ui-button" Width="100px"/>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                 <asp:Button runat="server" ID="btnLimpar" CssClass="ui-button" Width="100px"/>
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
            <asp:Label ID="lblSubTitulosProductos" CssClass="ui-panel-title" runat="server"></asp:Label>
        </div>
            <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center" width="100%">
                        <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <pro:ProsegurGridView ID="GrdProductos" runat="server" AllowPaging="True" AllowSorting="True"
                                    ColunasSelecao="CodigoProducto" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                                    PageSize="10" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                                    NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0" PaginacaoAutomatica="True"
                                    Width="99%" Height="100%" ExibirCabecalhoQuandoVazio="False">
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
                                        <asp:TemplateField HeaderText="" SortExpression="">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate >
                                                <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultar"  OnClick="OnClick" CssClass="imgButton"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CodigoProducto" HeaderText="CodigoProducto" SortExpression="CodigoProducto">
                                             <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DescripcionProducto" HeaderText="DescripcionProducto"
                                            SortExpression="DescripcionProducto" />
                                        <asp:BoundField DataField="ClaseBillete" HeaderText="ClaseBillete" SortExpression="ClaseBillete" />
                                        <asp:BoundField DataField="FactorCorreccion" HeaderText="FactorCorreccion" SortExpression="FactorCorreccion" >
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="EsManual" SortExpression="EsManual">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate >
                                                <asp:Image ID="imgManual" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
            </div>
</asp:Content>
