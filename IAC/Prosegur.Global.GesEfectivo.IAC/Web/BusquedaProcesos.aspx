<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaProcesos.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaProcesos" EnableEventValidation="false"
    MasterPageFile="~/Master/Master.Master" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Busqueda Procesos</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ocultarExibir() {
            $("#DivFiltros").toggleClass("legend-expand");
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
                            <div id="DivFiltros" class="legend-collapse" onclick="ocultarExibir();">
                                <asp:Label ID="lblTitulosBusquedaProcesos" CssClass="legent-text" runat="server">Filtrar Clientes</asp:Label>
                            </div>
                        </legend>
                        <div class="accordion" style="display: block;">
                            <asp:UpdatePanel ID="updUcClienteUc" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                <ContentTemplate>
                                    <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <table class="tabela_campos">
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCanal" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="lstCanal" runat="server" AutoPostBack="True" Height="90px" SelectionMode="Multiple"
                                            Width="245px"></asp:ListBox>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblSubCanal" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td colspan="5">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:ListBox ID="lstSubCanais" runat="server" Height="90px" SelectionMode="Multiple"
                                                    Width="285px"></asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDelegacion" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="lstDelegacion" runat="server" Height="90px" SelectionMode="Multiple"
                                            Width="245px"></asp:ListBox>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblProducto" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td colspan="5">
                                        <asp:ListBox ID="lstProducto" runat="server" Height="90px" SelectionMode="Multiple"
                                            Width="285px"></asp:ListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblVigente" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td colspan="7">
                                        <asp:CheckBox ID="chkVigente" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8">
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
                    <asp:Label ID="lblSubTitulosBusquedaProcesos" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center" width="100%">
                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pro:ProsegurGridView ID="ProsegurGridViewProcesso" runat="server" Ajax="False" AllowPaging="True"
                                        AllowSorting="True" AutoGenerateColumns="False" ColunasSelecao="CodigoDelegacion,CodigoCliente,CodigoSubcliente,CodigoPuntoServicio,CodigoSubcanal,IdentificadorProceso"
                                        EstiloDestaque="GridLinhaDestaque" ExibirCabecalhoQuandoVazio="False" GerenciarControleManualmente="True"
                                        GridPadrao="False" Height="100%" NumeroRegistros="0" OrdenacaoAutomatica="True"
                                        PaginaAtual="0" PaginacaoAutomatica="True" Width="99%">
                                        <Pager ID="objPager_ProsegurGridView1">
                                            <FirstPageButton Visible="True">
                                            </FirstPageButton>
                                            <LastPageButton Visible="True">
                                            </LastPageButton>
                                            <Summary Text="Pgina {0} de {1} ({2} itens)" />
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
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/sustituir.png" ID="imgDuplicar" CssClass="imgButton" OnClick="imgDuplicar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultar" OnClick="imgConsultar_OnClick" CssClass="imgButton" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluir" CssClass="imgButton" OnClick="imgExcluir_OnClick" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DescripcionCliente" HeaderText="DescripcionCliente" SortExpression="DescripcionCliente" />
                                            <asp:BoundField DataField="DescripcionSubcliente" HeaderText="DescripcionSubcliente"
                                                SortExpression="DescripcionSubcliente" />
                                            <asp:BoundField DataField="DescripcionPuntoServicio" HeaderText="DescripcionPuntoServicio"
                                                SortExpression="DescripcionPuntoServicio" />
                                            <asp:BoundField DataField="DescripcionCanal" HeaderText="DescripcionCanal" SortExpression="DescripcionCanal" />
                                            <asp:BoundField DataField="DescripcionSubcanal" HeaderText="DescripcionSubcanal"
                                                SortExpression="DescripcionSubcanal" />
                                            <asp:BoundField DataField="DescripcionClaseBillete" HeaderText="DescripcionClaseBillete"
                                                SortExpression="DescripcionClaseBillete" />
                                            <asp:BoundField DataField="DescripcionProceso" HeaderText="DescripcionProceso" SortExpression="DescripcionProceso" />
                                            <asp:BoundField DataField="CodigoDelegacion" HeaderText="CodigoDelegacion" SortExpression="CodigoDelegacion">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Vigente" SortExpression="vigente">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Image ID="imgVigente" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="IdentificadorProceso" HeaderText="IdentificadorProceso"
                                                SortExpression="IdentificadorProceso" Visible="false" />
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
                            <asp:UpdatePanel ID="UpdatePanelGridSemRegistro" runat="server" ChildrenAsTriggers="False"
                                UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlSemRegistro" runat="server" Visible="false">
                                        <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro">
                                            <tr>
                                                <td style="border-width: 0;">
                                                   
                                                </td>
                                                <td style="border-width: 0;">
                                                    <asp:Label ID="lblSemRegistro" runat="server" CssClass="label2" Text="Label">Não existem dados a serem exibidos.</asp:Label>
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
                </td> </tr> </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

        <asp:UpdatePanel runat="server" ID="updForm" UpdateMode="Conditional" ChildrenAsTriggers="False">
            <ContentTemplate>
                <asp:Panel runat="server" ID="pnForm" Visible="True">
                    <div class="ui-panel-titlebar">
                        <asp:Label ID="lblTituloProcesos" CssClass="ui-panel-title" runat="server"></asp:Label>
                    </div>
                    <asp:UpdatePanel ID="updUcClientForm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                        <ContentTemplate>
                            <asp:PlaceHolder runat="server" ID="phClienteForm"></asp:PlaceHolder>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <table class="tabela_campos">
                        <tr>
                            <td>
                                <table class="tabela_campos">
                                    <tr>
                                        <td class="tamanho_celula">
                                            <asp:Label ID="lblCanalForm" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:ListBox ID="lstCanalForm" runat="server" AutoPostBack="True" Height="90px" SelectionMode="Multiple"
                                                Width="200px"></asp:ListBox>
                                        </td>
                                        <td class="tamanho_celula">
                                            <asp:Label ID="lblSubCanalForm" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:UpdatePanel ID="UpdatePanelSubCanal" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <table width="100%" cellpadding="0px" cellspacing="0px">
                                                        <tr>
                                                            <td>
                                                                <asp:ListBox ID="lstSubCanaisForm" runat="server" Height="90px" SelectionMode="Multiple" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                                                    Width="200px"></asp:ListBox>

                                                                <asp:CustomValidator ID="csvSubCanalObrigatorio" runat="server" ControlToValidate="lstSubCanais"
                                                                    ErrorMessage="">*</asp:CustomValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="lstCanalForm" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td class="tamanho_celula">
                                            <asp:Label ID="lblDelegacionForm" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtDelegacion" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table class="tabela_campos">
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblDescricaoProceso" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <table width="100%" cellpadding="0px" cellspacing="0px">
                                    <tr>
                                        <td width="300px">
                                            <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtDescricaoProceso" runat="server" Width="272px" MaxLength="50"
                                                        AutoPostBack="true" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                                                    <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ErrorMessage=""
                                                        ControlToValidate="txtDescricaoProceso" Text="*"></asp:CustomValidator>
                                                    <asp:CustomValidator ID="csvDescripcionExistente" runat="server" ErrorMessage=""
                                                        ControlToValidate="txtDescricaoProceso">*</asp:CustomValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblProductoForm" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanelProduto" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlProducto" runat="server" Width="400px" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="csvProductoObrigatorio" runat="server" ControlToValidate="ddlProducto"
                                            ErrorMessage="" Text="*"></asp:CustomValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblModalidad" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanelModalidad" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlModalidad" runat="server" Width="400px" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="csvModalidadObrigatorio" runat="server" ControlToValidate="ddlModalidad"
                                            ErrorMessage="" Text="*"></asp:CustomValidator>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlProducto" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblIACParcial" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanelIACParcial" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlIACParcial" runat="server" Width="400px" Enabled="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlModalidad" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblIACBulto" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanelIACBulto" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlIACBulto" runat="server" Width="400px" Enabled="False" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlModalidad" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </td>
                        </tr>
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblIACRemesa" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanelIACRemesa" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlIACRemesa" runat="server" Width="400px" Enabled="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlModalidad" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <asp:UpdatePanel ID="upUcClienteFatu" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                        <ContentTemplate>
                            <asp:PlaceHolder runat="server" ID="phClienteFatu"></asp:PlaceHolder>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <table class="tabela_campos">
                      <tr id="vigenteLinha" runat="server">
                            <td class="tamanho_celula">
                                <asp:Label ID="lblVigenteForm" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkVigenteForm" runat="server" />
                            </td>
                        </tr>
                </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server" ID="updForm2" UpdateMode="Conditional" ChildrenAsTriggers="True">
            <ContentTemplate>
                <asp:Panel runat="server" ID="pnForm2">
                    <div class="ui-panel-titlebar">
                        <asp:UpdatePanel ID="UpdatePanelInformacionDeclarado" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="lblSubTitulosInformacionDelDeclarado" CssClass="ui-panel-title" runat="server"></asp:Label>
                                <asp:CustomValidator ID="csvInformacioDeclaradoObrigatorio" runat="server" ErrorMessage=""
                                    Text="*" ValidationGroup="InformacionDelDeclarado"></asp:CustomValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <table class="tabela_campos" style="margin-bottom: 20px; margin-left: 120px;">
                        <tr>
                            <td>
                                <asp:RadioButton ID="rdbPorAgrupaciones" runat="server" CssClass="label2" AutoPostBack="True" GroupName="InformacionDelDeclarado" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="rdbPorMedioPago" runat="server" CssClass="label2" AutoPostBack="True" GroupName="InformacionDelDeclarado" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlAgrupacao" runat="server">
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblSubTitulosAgrupaciones" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td id="Td1" runat="server" style="width: 350px; border-width: 1px; vertical-align: top;">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:UpdatePanel ID="UpdatePanelAgrupacionesPossibles" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:ListBox ID="lstAgrupacionesPosibles" runat="server" Height="120px" SelectionMode="Multiple"
                                                                        Width="350px"></asp:ListBox>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="center" style="width: 68px; border-width: 0;">
                                                <asp:ImageButton ID="imgBtnAgrupacionesIncluirTodas" runat="server" ImageUrl="~/Imagenes/pag07.png" /><br />
                                                <asp:ImageButton ID="imgBtnAgrupacionesIncluir" runat="server" ImageUrl="~/Imagenes/pag05.png" /><br />
                                                <asp:ImageButton ID="imgBtnAgrupacionesExcluir" runat="server" ImageUrl="~/Imagenes/pag06.png" /><br />
                                                <asp:ImageButton ID="imgBtnAgrupacionesExcluirTodas" runat="server" ImageUrl="~/Imagenes/pag08.png" />
                                            </td>
                                            <td style="width: 350px; border-width: 1px; vertical-align: top;">
                                                <asp:UpdatePanel ID="UpdatePanelAgrupacionCompoenProceso" runat="server">
                                                    <ContentTemplate>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:ListBox ID="lstAgrupacionesCompoenProceso" runat="server" Height="120px" SelectionMode="Multiple"
                                                                        Width="350px"></asp:ListBox>
                                                                </td>
                                                                <td>
                                                                    <asp:CustomValidator ID="csvAgrupacionesCompoenProcesoObrigatorio" runat="server"
                                                                        ControlToValidate="" ErrorMessage="" Text="*"></asp:CustomValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlMediosPago" runat="server">
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblSubTitulosMediosPago" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table style="width: auto; margin-top: 10px; margin-bottom: 10px;" cellspacing="0"
                            cellpadding="0" border="0">
                            <tr>
                                <td align="left">
                                    <asp:UpdatePanel ID="UpdatePanelTreeView" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table cellpadding="0" cellspacing="2">
                                                <tr>
                                                    <td align="left">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <table style="margin: 0px !important">
                                                                        <tr>
                                                                            <td id="TdTreeViewDivisa" runat="server" style="width: 350px; border-width: 1px; vertical-align: top;">
                                                                                <table style="margin: 0px !important">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:TreeView ID="TrvDivisas" runat="server" CssClass="tree" ShowLines="True">
                                                                                                <SelectedNodeStyle CssClass="TreeViewSelecionado" />
                                                                                            </asp:TreeView>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td align="center" style="width: 68px; border-width: 0;">
                                                                                <asp:ImageButton ID="imgBtnIncluirTreeview" runat="server" ImageUrl="~/Imagenes/pag05.png" />
                                                                                <br />
                                                                                <asp:ImageButton ID="imgBtnExcluirTreeview" runat="server" ImageUrl="~/Imagenes/pag06.png" />
                                                                            </td>
                                                                            <td style="width: 350px; border-width: 1px; vertical-align: top;">
                                                                                <table style="margin: 0px !important">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:TreeView ID="TrvProcesos" runat="server" CssClass="tree" ShowLines="True">
                                                                                                <SelectedNodeStyle CssClass="TreeViewSelecionado" />
                                                                                            </asp:TreeView>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="3">
                                                                    <asp:Label ID="lblNota" runat="server" CssClass="label2" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <asp:CustomValidator ID="csvTrvProcesos" runat="server" ControlToValidate="" ErrorMessage=""
                                                            Text="*"></asp:CustomValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div class="ui-panel-titlebar">
                        <asp:Label ID="lblSubTitulosModoContaje" CssClass="ui-panel-title" runat="server"></asp:Label>
                    </div>
                    <table class="tabela_campos" style="margin-left: 120px;">
                        <tr>
                            <td>
                                <table style="margin: 0px !Important;">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkContarChequeTotales" CssClass="label2" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkContarTicketTotales" CssClass="label2" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkContarOtrosValoresTotales" CssClass="label2" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="chkContarTarjetaTotales" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <table class="tabela_campos">
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblObservaciones" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtObservaciones" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Height="96px"
                                    MaxLength="500" TextMode="MultiLine" Width="610px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
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
                      <asp:Button runat="server" ID="btnTerminoMedioPago" CssClass="btn-check"/>
                </td>
                <td>
                      <asp:Button runat="server" ID="btnTolerancia" CssClass="btn-config"/>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnSalvar" CssClass="btn-salvar"/>
                </td>
                <td >
                     <asp:Button runat="server" ID="btnBajaConfirm"  CssClass="btn-excluir"/>
                    <div class="botaoOcultar">
                         <asp:Button runat="server" ID="btnBaja" CssClass="btn-excluir"/>
                        <asp:Button runat="server" ID="btnConsomeMediosPagos" />
                        <asp:Button runat="server" ID="btnConsomeTerminos" />
                     </div>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn_cancelar"/>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
