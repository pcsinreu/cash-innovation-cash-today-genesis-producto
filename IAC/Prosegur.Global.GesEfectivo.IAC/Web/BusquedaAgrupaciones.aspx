<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaAgrupaciones.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaAgrupaciones" EnableEventValidation="false"
    MasterPageFile="~/Master/Master.Master" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Busqueda Agrupaciones</title>
     
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
                                        <asp:Label ID="lblCodigoAgrupacion" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCodigoAgrupacion" runat="server" MaxLength="15" CssClass="Text02"
                                            Width="150"></asp:TextBox>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDescricaoAgrupacion" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDescricaoAgrupacion" runat="server" Width="208px" MaxLength="50"
                                            CssClass="Text02"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDivisaEfectivo" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelDivisaEfectivo" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlDivisaEfectivo" runat="server" Width="208px" AutoPostBack="False">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblVigente" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkVigente" runat="server" Checked />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDivisaTicket" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelDivisaTicket" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlDivisaTicket" runat="server" Width="208px" AutoPostBack="False">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblTicket" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelTicket" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlTicket" runat="server" Width="208px" AutoPostBack="False">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDivisaOtrosValores" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelDivisaOtrosValores" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlDivisaOtrosValores" runat="server" Width="208px" AutoPostBack="False">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblOtrosValores" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelOtrosValores" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlOtrosValores" runat="server" Width="208px" AutoPostBack="False">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDivisaCheques" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelDivisaCheques" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlDivisaCheques" runat="server" Width="208px" AutoPostBack="False">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCheques" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelCheques" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlCheques" runat="server" Width="208px" AutoPostBack="False">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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
                    <asp:Label ID="lblSubTitulosAgrupaciones" CssClass="ui-panel-title" runat="server"></asp:Label>
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
                            &nbsp;&nbsp;&nbsp;&nbsp;                                        
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
                                            <asp:BoundField DataField="observacion" HeaderText="Observación" SortExpression="observacion" />
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
            <asp:UpdatePanel runat="server" ID="updForm">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="pnForm" Visible="True">
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloAgrupaciones" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table class="tabela_campos">
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigoAgrupacionForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td width="150px">
                                    <table width="100%" cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtCodigoAgrupacionForm" runat="server" MaxLength="15" AutoPostBack="False"
                                                            CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox><asp:CustomValidator ID="csvCodigoObrigatorio" runat="server"
                                                                ErrorMessage="" ControlToValidate="txtCodigoAgrupacionForm" Text="*"></asp:CustomValidator><asp:CustomValidator
                                                                    ID="csvCodigoAgrupacionExistente" runat="server" ErrorMessage="" ControlToValidate="txtCodigoAgrupacionForm">*</asp:CustomValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:UpdateProgress ID="UpdateProgressCodigo" runat="server" AssociatedUpdatePanelID="UpdatePanelCodigo">
                                                    <ProgressTemplate>
                                                        <img src="Imagenes/loader1.gif" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDescricaoAgrupacionForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td width="310px">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtDescricaoAgrupacionForm" runat="server" Width="235px" MaxLength="50"
                                                            AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox><asp:CustomValidator ID="csvDescricaoObrigatorio"
                                                                runat="server" ErrorMessage="001_msg_Agrupaciondescripcionobrigatorio" ControlToValidate="txtDescricaoAgrupacionForm"
                                                                Text="*"></asp:CustomValidator><asp:CustomValidator ID="csvDescripcionExistente"
                                                                    runat="server" ErrorMessage="" ControlToValidate="txtDescricaoAgrupacionForm">*</asp:CustomValidator>
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
                                    <asp:TextBox ID="txtObservaciones" runat="server" MaxLength="4000" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                        Height="96px" Width="610px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblVigenteForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:CheckBox ID="chkVigenteForm" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblSubTitulosAgrupacionesForm" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table class="tabela_interna" align="center" cellpadding="0" cellspacing="2">
                            <tr>
                                <td width="60%">
                                    <asp:UpdatePanel ID="UpdatePanelTreeView" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table style="margin: 0px !important" width="750px">
                                                <tr>
                                                    <td id="TdTreeViewDivisa" style="width: 40%; border-width: 1px; vertical-align: top;"
                                                        runat="server">
                                                        <table style="margin: 0px !important" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:TreeView ID="TrvDivisas" runat="server" ShowLines="True" CssClass="tree">
                                                                        <SelectedNodeStyle CssClass="TreeViewSelecionado" />
                                                                    </asp:TreeView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td id="TdBtnInEx" align="center" style="width: 5%; border-width: 0;" runat="server">
                                                        <asp:ImageButton ID="imgBtnIncluir" runat="server" ImageUrl="~/Imagenes/pag05.png" />
                                                        <br />
                                                        <asp:ImageButton ID="imgBtnExcluir" runat="server" ImageUrl="~/Imagenes/pag06.png" />
                                                    </td>
                                                    <td style="width: 40%; border-width: 1px; vertical-align: top;">
                                                        <table style="margin: 0px !important" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:TreeView ID="TrvAgrupaciones" runat="server" ShowLines="True" CssClass="tree">
                                                                        <SelectedNodeStyle CssClass="TreeViewSelecionado" />
                                                                    </asp:TreeView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="imgBtnIncluir" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="imgBtnExcluir" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanelCsvTreeViewAgrupacion" runat="server">
                                        <ContentTemplate>
                                            <asp:CustomValidator ID="csvTrvAgrupaciones" runat="server" ErrorMessage="" ControlToValidate=""
                                                Text="*"></asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlSemRegistroForm" runat="server" Visible="false">
                            <tr>
                                <td align="center">
                                    <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro">
                                        <tr>
                                            <td style="border-width: 0;">
                                                </td>
                                            <td style="border-width: 0;">
                                                <asp:Label ID="lblSemRegistroForm" runat="server" Text="Label" CssClass="label2">Não existem dados a serem exibidos.</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </asp:Panel>
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