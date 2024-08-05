<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master"  CodeBehind="BusquedaTipoProcedencia.aspx.vb" 
Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaTipoProcedencia" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Busqueda de Tipos Procedencia</title>
     
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
                        <asp:Label ID="lblCriteriosBusqueda" CssClass="legent-text" runat="server">Filtrar Clientes</asp:Label>
                    </div>
                </legend>
                <div class="accordion" style="display: none;">
                   <table class="tabela_campos"  >
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodTipoProcedencia" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td >
                                    <asp:TextBox ID="txtCodTipoProcedencia" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" 
                                        Width="155px" MaxLength="15"></asp:TextBox>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDescripcion" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtDescricao" runat="server" Width="255px" MaxLength="50"
                                        CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblVigente" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td colspan="1" align="left" class="style1">
                                    <asp:CheckBox ID="chkVigente" runat="server" Checked="True" />
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
                    <asp:Label ID="lblResultadoTipoProcedencia" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
               <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center">
                        <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <pro:ProsegurGridView ID="GdvResultado" runat="server" AllowPaging="True" AllowSorting="True"
                                    ColunasSelecao="codTipoProcedencia" EstiloDestaque="GridLinhaDestaque" 
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
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                                <ItemStyle HorizontalAlign="Center" Width="150"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditar" CssClass="imgButton" OnClick="imgEditar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultar" CssClass="imgButton" OnCLICK="imgConsultar_OnClick"/>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluir" CssClass="imgButton" OnClick="imgExcluir_OnClick" />
                                                </ItemTemplate>
                                            </asp:TemplateField>      
                                        <asp:BoundField DataField="codTipoProcedencia" HeaderText="codigo" SortExpression="codTipoProcedencia" />
                                        <asp:BoundField DataField="desTipoProcedencia" HeaderText="descripcion" SortExpression="desTipoProcedencia" />
                                        <asp:TemplateField HeaderText="BolActivo">
                                            <ItemStyle HorizontalAlign="Center" Width="150"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Image ID="imgVigente" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="oidTipoProcedenciae" HeaderText="OidTipoProcedencia" SortExpression="oidTipoProcedencia" Visible="false" />
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
                <asp:Panel runat="server" ID="pnForm" Visible="True">
                    <div class="ui-panel-titlebar">
                        <asp:Label ID="lblTituloProcedencia" CssClass="ui-panel-title" runat="server"></asp:Label>
                    </div>
                     <table class="tabela_campos"  >
                <tr>
                    <td class="tamanho_celula">
                        <asp:Label ID="lblCodTipoProcedenciaForm" runat="server" CssClass="label2"></asp:Label>
                    </td>
                    <td >
                        <table cellpadding="0px" cellspacing="0px" style="width: 120%">
                            <tr>
                                <td width="250px">
                                    <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCodigoTipoProcedencia" runat="server" MaxLength="15" AutoPostBack="False"
                                                CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="148px"></asp:TextBox>&nbsp;<asp:CustomValidator ID="csvCodigoTipoProcedencia"
                                                    runat="server" ErrorMessage="" 
                                                ControlToValidate="txtCodigoTipoProcedencia" Text="*"></asp:CustomValidator>
                                                    <asp:CustomValidator ID="csvCodigoExistente"
                                                    runat="server" ErrorMessage="" ControlToValidate="txtCodigoTipoProcedencia" Text="*">*</asp:CustomValidator>
                                        </ContentTemplate> 
                                    </asp:UpdatePanel>
                                </td>
                           </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tamanho_celula">
                        <asp:Label ID="lblDescripcionForm" runat="server" CssClass="label2"></asp:Label>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanelDescripcion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <asp:TextBox ID="txtDescricaoTipoProcedencia" runat="server" Width="225px" 
                                MaxLength="50" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"/>
                                <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ErrorMessage="" 
                                ControlToValidate="txtDescricaoTipoProcedencia" Text="*"></asp:CustomValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
                </tr>
                <tr>
                    <td class="tamanho_celula">
                        <asp:Label ID="lblVigenteForm" runat="server" CssClass="label2"></asp:Label>
                    </td>
                    <td >
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