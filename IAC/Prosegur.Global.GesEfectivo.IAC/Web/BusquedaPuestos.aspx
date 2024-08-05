<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaPuestos.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaPuestos" EnableEventValidation="false"
    MasterPageFile="~/Master/Master.Master" %>

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
                                        <asp:Label ID="lblAplicacion" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelAplicacion" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlAplicacion" runat="server" Width="225px" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCodigoPuesto" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCodigoPuesto" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="25" Width="225px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblHostPuesto" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHostPuesto" runat="server" MaxLength="30" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="225px"></asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        <asp:RadioButtonList ID="cblVigente" runat="server" RepeatDirection="Horizontal"
                                            CssClass="Text02" TextAlign="Left" Width="260px" BorderWidth="0px">
                                            <asp:ListItem Text="Vigente" Value="1" Selected="True" />
                                            <asp:ListItem Text="Não Vigente" Value="0" />
                                            <asp:ListItem Text="Qualquer" Value="" />
                                        </asp:RadioButtonList>
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
                    <asp:Label ID="lblSubTitulosPuestos" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pro:ProsegurGridView ID="ProsegurGridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                        ColunasSelecao="CodigoPuesto,CodigoHostPuesto,CodigoDelegacion,CodigoAplicacion"
                                        EstiloDestaque="GridLinhaDestaque" GridPadrao="False" PageSize="10" AutoGenerateColumns="False"
                                        Ajax="True" GerenciarControleManualmente="True" ExibirCabecalhoQuandoVazio="false"
                                        NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0" PaginacaoAutomatica="True"
                                        Width="99%">
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
                                        </TextBox>
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemStyle HorizontalAlign="Center" Width="200"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditar" CssClass="imgButton" OnClick="imgEditar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/sustituir.png" ID="imgDuplicar" CssClass="imgButton" OnClick="imgDuplicar_OnClick"/>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluir" CssClass="imgButton" OnClick="imgExcluir_OnClick" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CodigoDelegacion" HeaderText="Delegación" SortExpression="CodigoDelegacion" />
                                            <asp:BoundField DataField="CodigoPuesto" HeaderText="Codigo Puesto" SortExpression="CodigoPuesto" />
                                            <asp:BoundField DataField="CodigoHostPuesto" HeaderText="Host Puesto" SortExpression="CodigoHostPuesto" />
                                            <asp:BoundField HeaderText="Aplicación" SortExpression="DescripcionAplicacion" />
                                            <asp:TemplateField HeaderText="vigente" SortExpression="PuestoVigente">
                                                <ItemStyle HorizontalAlign="Center" Width="100"></ItemStyle>
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
                            <asp:Label ID="lblTituloPuesto" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table class="tabela_campos">
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDelegacion" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDelegacion" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false"
                                        Width="30%">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblAplicacionForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td >
                                    <asp:UpdatePanel runat="server" ID="upAplicacion">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlAplicacionForm" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="30%"
                                                AutoPostBack="False">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvAplicacion" ControlToValidate="ddlAplicacionForm"
                                                Text="*" ErrorMessage="" Display="Dynamic" ValidationGroup="Gravar" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigoPuestoForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="upCodigoPuesto">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCodigoPuestoForm" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="225px" MaxLength="25">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvCodigoPuesto" ControlToValidate="txtCodigoPuestoForm"
                                                Text="*" ErrorMessage="" Display="Dynamic" ValidationGroup="Gravar" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblHostPuestoForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtHostPuestoForm" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="225px" MaxLength="30">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvhostPuesto" ControlToValidate="txtHostPuestoForm"
                                                Text="*" ErrorMessage="" Display="Dynamic" ValidationGroup="Gravar" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblVigente" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkVigente" runat="server" />
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
                          <asp:Button runat="server" ID="btnConsomeImporteMaximo" CssClass="btn-excluir"/>
                    </div>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn_cancelar"/>
                </td>
                  <td>
                    <asp:Button runat="server" ID="btnConfigurarParametros" CssClass="btn-config"/>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>