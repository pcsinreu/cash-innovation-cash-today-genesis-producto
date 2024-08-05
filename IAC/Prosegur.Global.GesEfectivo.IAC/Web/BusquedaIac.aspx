<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaIac.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaIac"
    EnableEventValidation="false" MasterPageFile="~/Master/Master.Master" %>

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
        <asp:HiddenField runat="server" ID="hiddenCodigo" />
        <asp:UpdatePanel ID="UpdatePanelGeral" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
            <ContentTemplate>
                <div id="Filtros">
                    <fieldset id="ExpandCollapseDiv" class="ui-fieldset ui-widget ui-widget-content ui-corner-all ui-fieldset-toggleable">
                        <legend class="ui-fieldset-legend ui-corner-all ui-state-default ui-state-active">
                            <div id="DivFiltros" class="legend-expand" onclick="ocultarExibir();">
                                <asp:Label ID="lblTituloIac" CssClass="legent-text" runat="server">Filtrar Clientes</asp:Label>
                            </div>
                        </legend>
                        <div class="accordion" style="display: none;">
                            <table class="tabela_campos">
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCodigoIac" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCodigoIac" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" runat="server" Width="150px" MaxLength="15"></asp:TextBox>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDescricaoIac" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtDescricaoIac" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkVigente" runat="server" Checked="true" CssClass="Lbl2" />
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblTerminos" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="lstTerminos" runat="server" Height="80px" Width="260px" SelectionMode="Multiple"></asp:ListBox>
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
                    <asp:Label ID="lblSubTituloIac" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center" width="100%">
                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pro:ProsegurGridView ID="grdIac" runat="server" AllowPaging="True" AllowSorting="True"
                                        ColunasSelecao="CodidoIac" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                                        PageSize="10" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                                        NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0" PaginacaoAutomatica="True"
                                        Width="99%" Height="100%" ExibirCabecalhoQuandoVazio="false">
                                        <Pager ID="objPager_grdIac">
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
                                            <asp:BoundField DataField="CodidoIac" HeaderText="CodidoIac" SortExpression="CodidoIac" />
                                            <asp:BoundField DataField="DescripcionIac" HeaderText="DescripcionIac" SortExpression="DescripcionIac" />
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
                                                    <asp:Label ID="lblSemRegistro" runat="server" Text="Label" CssClass="Lbl2">Não existem dados a serem exibidos.</asp:Label>
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
                        <asp:HiddenField ID="hidTerminoIacSelecionado" runat="server" />
                        <asp:HiddenField ID="hidTerminoSelecionado" runat="server" />
                        <asp:HiddenField ID="hidObligatorio" runat="server" />
                        <asp:HiddenField ID="hidObligatorioFalse" runat="server" />
                        <asp:HiddenField ID="hidObligatorioValorTrue" runat="server" />
                        <asp:HiddenField ID="hidObligatorioValorFalse" runat="server" />
                        <asp:HiddenField ID="hidBusqueda" runat="server" />
                        <asp:HiddenField ID="hidBusquedaFalse" runat="server" />
                        <asp:HiddenField ID="hidCampoClave" runat="server" />
                        <asp:HiddenField ID="hidOrden" runat="server" />
                        <asp:HiddenField ID="hidCampoClaveFalse" runat="server" />
                        <asp:HiddenField ID="hidBusquedaValorTrue" runat="server" />
                        <asp:HiddenField ID="hidBusquedaValorFalse" runat="server" />
                        <asp:HiddenField ID="hidClaveValorVerdadeiro" runat="server" />
                        <asp:HiddenField ID="hidClaveValorFalso" runat="server" />
                        <asp:HiddenField ID="hdnObjeto" runat="server" />
                        <asp:HiddenField ID="hidCopiarTermino" runat="server" />
                        <asp:HiddenField ID="hidCopiarTerminoFalse" runat="server" />
                        <asp:HiddenField ID="hidCopiarTerminoValorVerdadeiro" runat="server" />
                        <asp:HiddenField ID="hidCopiarTerminoValorFalso" runat="server" />
                        <asp:HiddenField ID="hidProtegido" runat="server" />
                        <asp:HiddenField ID="hidProtegidoFalse" runat="server" />
                        <asp:HiddenField ID="hidProtegidoValorTrue" runat="server" />
                        <asp:HiddenField ID="hidProtegidoValorFalse" runat="server" />
                        <asp:HiddenField ID="hidInvisibleReporte" runat="server" />
                        <asp:HiddenField ID="hidInvisibleReporteFalse" runat="server" />
                        <asp:HiddenField ID="hidInvisibleReporteValorTrue" runat="server" />
                        <asp:HiddenField ID="hidInvisibleReporteValorFalse" runat="server" />
                        <asp:HiddenField ID="hidIdMecanizado" runat="server" />
                        <asp:HiddenField ID="hidIdMecanizadoFalse" runat="server" />
                        <asp:HiddenField ID="hidIdMecanizadoValorTrue" runat="server" />
                        <asp:HiddenField ID="hidIdMecanizadoValorFalse" runat="server" />

                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloIacForm" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table class="tabela_interna" >
                            <tr>
                                <td>
                                    <table class="tabela_campos" style="margin: 0px !important" >
                                        <tr>
                                            <td class="tamanho_celula">
                                                <asp:Label ID="lblCodigoIacForm" runat="server" CssClass="Lbl2"></asp:Label>
                                            </td>
                                            <td >
                                                <table width="100%" cellpadding="0px" cellspacing="0px">
                                                    <tr>
                                                        <td>
                                                            <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="txtCodigoIacForm" runat="server" MaxLength="15" AutoPostBack="False"
                                                                        CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="150px"></asp:TextBox>
                                                                    <asp:CustomValidator ID="csvCodigoObrigatorio" runat="server" ErrorMessage="001_msg_canalcodigoobrigatorio"
                                                                        ControlToValidate="txtCodigoIacForm" Text="*"></asp:CustomValidator>
                                                                    <asp:CustomValidator ID="csvCodigoExistente" runat="server" ErrorMessage="006_msg_codigoiacexistente"
                                                                        ControlToValidate="txtCodigoIacForm" Text="*"></asp:CustomValidator>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="tamanho_celula">
                                                <asp:Label ID="lblDescricaoIacForm" runat="server" CssClass="Lbl2"></asp:Label>
                                            </td>
                                            <td >
                                                <table width="100%" cellpadding="0px" cellspacing="0px">
                                                    <tr>
                                                        <td>
                                                            <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="txtDescricaoIacForm" runat="server" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                                                        MaxLength="50" Width="230px"></asp:TextBox>
                                                                    <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ControlToValidate="txtDescricaoIacForm"
                                                                        ErrorMessage="001_msg_canaldescripcionobrigatorio" Text="*"></asp:CustomValidator>
                                                                    <asp:CustomValidator ID="csvDescricaoExistente" runat="server" ControlToValidate="txtDescricaoIacForm"
                                                                        ErrorMessage="006_msg_descricaoiacexistente" Text="*"></asp:CustomValidator>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tamanho_celula">
                                                <asp:Label ID="lblObservacionesIac" runat="server" CssClass="Lbl2"></asp:Label>
                                            </td>
                                            <td colspan="4" style="margin-left: 40px">
                                                <asp:TextBox ID="txtObservacionesIac" runat="server" MaxLength="4000" AutoPostBack="False"
                                                    CssClass="Text02" Height="96px" Width="532px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tamanho_celula">
                                                <asp:Label ID="lblCopiarDeclarados" runat="server" CssClass="Lbl2"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkCopiarDeclarados" runat="server" CssClass="Lbl2" />
                                            </td>
                                            <td colspan="3">
                                                <asp:Label ID="lblDisponivelNuevoSaldos" runat="server" CssClass="Lbl2"></asp:Label>
                                                <asp:CheckBox ID="chkDisponivelNuevoSaldos" runat="server" CssClass="Lbl2" />
                                            </td>
                                        </tr>
                                       <tr>
                                            <td class="tamanho_celula">
                                                <asp:Label ID="lblInvisible" runat="server" CssClass="Lbl2"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:CheckBox ID="chkInvisible" runat="server" CssClass="Lbl2" />
                                            </td>
                                        </tr>
                                         <tr>
                                            <td class="tamanho_celula">
                                                <asp:Label ID="lblVigenteForm" runat="server" CssClass="Lbl2"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:CheckBox ID="chkVigenteForm" runat="server" CssClass="Lbl2" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <table class="tabela_interna">
                                                <tr>
                                                    <td>
                                                        <pro:ProsegurGridView ID="ProsegurGridView1" runat="server" AllowPaging="True" AllowSorting="False"
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
                                                                        <asp:CheckBox ID="chkAdicionaGridTerminos" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                                                                <asp:BoundField DataField="Observacion" HeaderText="Observacion" />
                                                            </Columns>
                                                        </pro:ProsegurGridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:ImageButton ID="btnAdiciona" runat="server" ImageUrl="~/Imagenes/pag02.png" />
                                                        <asp:ImageButton ID="btnremove" runat="server" ImageUrl="~/Imagenes/pag03.png" />
                                                    </td>
                                                </tr>
                                                </table >
                                                        <table  class="tabela_interna">
                                                            <tr >
                                                                <td rowspan="2" style="width: 100%;padding-left: 1px;">
                                                                    <pro:ProsegurGridView ID="ProsegurGridView2" runat="server" AllowPaging="False" AllowSorting="False"
                                                                        ColunasSelecao="CodigoTermino" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                                                                        PageSize="10" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                                                                        NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0" PaginacaoAutomatica="false"
                                                                        Width="99%" Height="100%" ExibirCabecalhoQuandoVazio="true">
                                                                        <Pager ID="Pager1">
                                                                            <FirstPageButton Visible="True">
                                                                            </FirstPageButton>
                                                                            <LastPageButton Visible="True">
                                                                            </LastPageButton>
                                                                            <Summary Text="P�gina {0} de {1} ({2} itens)" />
                                                                            <SummaryStyle>
                                                                            </SummaryStyle>
                                                                        </Pager>
                                                                        <HeaderStyle Font-Bold="True" />
                                                                        <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                                                        <RowStyle CssClass="GridLinhaPadraoImpar" />
                                                                        <TextBox ID="TextBox1" AutoPostBack="True" MaxLength="10" Width="30px"></TextBox>
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="adiciona">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkBusqueda" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="CodigoTermino" HeaderText="CodigoTermino" />
                                                                            <asp:BoundField DataField="DescripcionTermino" HeaderText="DescripcionTermino" />
                                                                            <asp:TemplateField HeaderText="EsObligatorio">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkObligatorio" runat="server" AutoPostBack="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="EsBusquedaParcial">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkBusqueda1" runat="server" AutoPostBack="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="EsCampoClave">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkAdicionaGridTerminosIac" runat="server" AutoPostBack="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="EsTerminoCopia">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkCopiaTermino" runat="server" AutoPostBack="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="esProtegido">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkProtegido" runat="server" AutoPostBack="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="OrdenTermino" HeaderText="OrdenTermino" Visible="false" />
                                                                            <asp:TemplateField HeaderText="esInvisibleRpte">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkInvisibleRpte" runat="server" AutoPostBack="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="esIdMecanizado">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkIdMecanizado" runat="server" AutoPostBack="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </pro:ProsegurGridView>
                                                                </td>
                                                                <td >
                                                                    <asp:CustomValidator ID="csvTerminoObligatorio" runat="server" ControlToValidate="hidValidaGrid2"
                                                                        ErrorMessage="006_msg_iacTerminoObligatorio" Text="*"></asp:CustomValidator>
                                                                    <asp:TextBox ID="hidValidaGrid2" Style="visibility: hidden;" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table style="margin: 0px !important">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnAcima" runat="server" Height="23px" ImageUrl="~/Imagenes/pag03.png"
                                                                                    Width="25px" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnAbaixo" runat="server" ImageUrl="~/Imagenes/pag02.png" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
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
