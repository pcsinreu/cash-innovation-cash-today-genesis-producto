<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaProcedencias.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaProcedencias" EnableEventValidation="false"
    MasterPageFile="~/Master/Master.Master" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Busqueda Procedencias</title>
     
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
    <asp:UpdatePanel ID="UpdatePanelGeral" runat="server" UpdateMode="Conditional"  ChildrenAsTriggers="False">
        <ContentTemplate>
        <div id="Filtros">
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
                                    <asp:Label ID="lblTipoSubCliente" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTipoSubCliente" runat="server" Width="208px" AutoPostBack="false" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:DropDownList>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblTipoPuntoServicio" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTipoPuntoServicio" runat="server" Width="208px" AutoPostBack="false" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblTipoProcedencia" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlTipoProcedencia" runat="server" Width="208px" AutoPostBack="false" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:DropDownList>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblVigente" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:CheckBox ID="chkVigente" runat="server" Checked="true" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
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
                    <asp:Label ID="lblSubTitulosProcedencias" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
            <table class="tabela_interna" >
                <tr>
                    <td align="center">
                        <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <pro:ProsegurGridView ID="ProsegurGridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                    ColunasSelecao="OidProcedencia" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
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
                                    <HeaderStyle  Font-Bold="True" />
                                    <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                    <RowStyle CssClass="GridLinhaPadraoImpar" />
                                    <Columns>  
                                         <asp:TemplateField HeaderText="">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditar" OnClick="imgEditar_OnClick" CssClass="imgButton" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultar" OnClick="imgConsultar_OnClick"  CssClass="imgButton" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluir" OnClick="imgExcluir_OnClick"  CssClass="imgButton" />
                                                </ItemTemplate>
                                            </asp:TemplateField>                                      
                                        <asp:BoundField DataField="DescripcionTipoSubCliente" HeaderText="Tipo SubCliente" SortExpression="DescripcionTipoSubCliente" />
                                        <asp:BoundField DataField="DescripcionTipoPuntoServicio" HeaderText="Tipo Punto Servicio" SortExpression="DescripcionTipoPuntoServicio" />
                                        <asp:BoundField DataField="DescripcionTipoProcedencia" HeaderText="Tipo Procedencia" SortExpression="DescripcionTipoProcedencia"/>
                                        <asp:TemplateField HeaderText="Vigente" SortExpression="vigente">
                                             <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Image ID="imgVigente" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="OidProcedencia" Visible="false"/>
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
                        <asp:Panel runat="server" ID="pnForm" Visible="False">
                            <div class="ui-panel-titlebar">
                                <asp:Label ID="lblTituloProcedencia" CssClass="ui-panel-title" runat="server"></asp:Label>
                            </div>
                            <table class="tabela_campos"  >
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblTipoSubClienteForm" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelTipoSubCliente" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlTipoSubClienteForm" runat="server" Width="208px" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:DropDownList>
                                                <asp:CustomValidator ID="csvTipoSubClienteObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlTipoSubClienteForm" Text="*">
                                                </asp:CustomValidator>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblTipoPuntoServicioForm" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelTipoPuntoServicio" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlTipoPuntoServicioForm" runat="server" Width="208px" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:DropDownList>
                                                <asp:CustomValidator ID="csvTipoPuntoServicioObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlTipoPuntoServicioForm" Text="*">
                                                </asp:CustomValidator>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblTipoProcedenciaForm" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:UpdatePanel ID="UpdatePanelTipoProcedencia" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlTipoProcedenciaForm" runat="server" Width="208px" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:DropDownList>
                                                <asp:CustomValidator ID="csvTipoProcedenciaObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlTipoProcedenciaForm" Text="*">
                                                </asp:CustomValidator>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblVigenteForm" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:CheckBox ID="chkVigenteForm" runat="server" Checked="true" />
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
