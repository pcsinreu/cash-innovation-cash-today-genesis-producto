<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaAgrupacionesParametros.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaAgrupacionesParametros"
    EnableEventValidation="false" MasterPageFile="~/Master/Master.Master" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Busqueda Puestos</title>
     
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
                                        <asp:Label ID="lblAplicacion" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:UpdatePanel ID="UpdatePanelAplicacion" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlAplicacion" runat="server" Width="155px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblAgrupacion" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAgrupacion" runat="server" MaxLength="30" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="155px"></asp:TextBox>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblNivel" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelNivel" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlNivel" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="155px" Style="float: left"
                                                    AutoPostBack="True">
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
                                                    <asp:Button runat="server" ID="btnBuscar" CssClass="ui-button" Width="100px" ValidationGroup="none" />
                                                </td>
                                                <td>
                                                    <asp:Button runat="server" ID="btnLimpar" CssClass="ui-button" Width="100px" ValidationGroup="none" />
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
                    <asp:Label ID="lblSubTitulosAgrupacionParametro" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pro:ProsegurGridView ID="ProsegurGridView1" runat="server" AllowPaging="True" AllowSorting="False"
                                        ColunasSelecao="DescripcionCorta,CodigoAplicacion,CodigoNivel" EstiloDestaque="GridLinhaDestaque"
                                        GridPadrao="False" PageSize="10" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
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
                                                <ItemStyle HorizontalAlign="Center" Width="150"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditar" CssClass="imgButton" ValidationGroup="none" OnClick="imgEditar_OnClick" />
                                                  <%--  <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultar" OnClick="imgConsultar_OnClick" CssClass="imgButton"  ValidationGroup="none"/>--%>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluir" CssClass="imgButton" ValidationGroup="none" OnClick="imgExcluir_OnClick"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DescripcionAplicacion" HeaderText="Aplicación" />
                                            <asp:BoundField DataField="DescripcionNivel" HeaderText="Nivel" />
                                            <asp:BoundField DataField="DescripcionCorta" HeaderText="Agrupación" />
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
                            <asp:Label ID="lblSubTitulosAgrupacionParametroForm" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                                    <table class="tabela_campos" id="TableFields" runat="server">
                                        <tr>
                                            <td class="tamanho_celula">
                                                <asp:Label ID="lblAplicacionForm" runat="server" CssClass="label2"></asp:Label>
                                            </td>
                                            <td >
                                                <asp:UpdatePanel ID="UpdatePanelDdlAplicacion" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlAplicacionForm" runat="server" Width="265px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="csvDdlAplicacionObrigatorio" runat="server" ControlToValidate="ddlAplicacionForm"
                                                            ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td class="tamanho_celula">
                                                <asp:Label ID="lblNivelForm" runat="server" CssClass="label2"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:UpdatePanel ID="UpdatePanelDdlNivel" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlNivelForm" runat="server" Width="265px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="csvDdlNivelObrigatorio" runat="server" ControlToValidate="ddlNivelForm"
                                                            ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tamanho_celula">
                                                <asp:Label ID="lblDescripcionCorto" runat="server" CssClass="label2"></asp:Label>
                                            </td>
                                            <td colspan="4">
                                                <asp:UpdatePanel ID="UpdatePanelTxtDescripcionCorto" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtDescripcionCorto" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" MaxLength="30"
                                                            Width="650px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="csvTxtDescripcionCortoObrigatorio" runat="server"
                                                            ControlToValidate="txtDescripcionCorto" ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tamanho_celula">
                                                <asp:Label ID="lblDescripcionLarga" runat="server" CssClass="label2"></asp:Label>
                                            </td>
                                            <td colspan="4">
                                                <asp:UpdatePanel ID="UpdatePanelTxtDescripcionLarga" runat="server" >
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtDescripcionLarga" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Height="48px"
                                                            MaxLength="100" TextMode="MultiLine" Width="650"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="csvTxtDescripcionLargaObrigatorio" runat="server"
                                                            ControlToValidate="txtDescripcionLarga" ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tamanho_celula">
                                                <asp:Label ID="lblOrden" runat="server" CssClass="label2"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanelTxtOrden" runat="server" >
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtOrden" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" MaxLength="9" Width="58px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="csvTxtOrden" runat="server" ControlToValidate="txtOrden"
                                                            ErrorMessage="" Text="*"></asp:RequiredFieldValidator>
                                                    </ContentTemplate>
                                               </asp:UpdatePanel>
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
                    <asp:Button runat="server" ID="btnNovo" CssClass="btn-novo" ValidationGroup="none"/>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnSalvar" CssClass="btn-salvar" ValidationGroup="none"/>
                </td>
                <td >
                     <asp:Button runat="server" ID="btnBajaConfirm"  CssClass="btn-excluir" ValidationGroup="none"/>
                    <div class="botaoOcultar">
                         <asp:Button runat="server" ID="btnBaja" CssClass="btn-excluir" ValidationGroup="none"/>
                    </div>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn_cancelar" ValidationGroup="none"/>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>