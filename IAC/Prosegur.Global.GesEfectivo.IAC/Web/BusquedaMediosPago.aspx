<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaMediosPago.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaMediosPago" EnableEventValidation="false"
    MasterPageFile="~/Master/Master.Master" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Búsqueda de Médios de Pago</title>
     
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
        <asp:HiddenField runat="server" ID="hiddenCodigoForm" />
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
                                        <asp:Label ID="lblTipoMedioPago" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:CheckBoxList ID="chkListTipoMedioPago" runat="server" AppendDataBoundItems="True"
                                            RepeatDirection="Horizontal">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCodigoMedioPago" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCodigoMedioPago" runat="server" MaxLength="15" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                            Width="150"></asp:TextBox>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDescricaoMedioPago" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDescricaoMedioPago" runat="server" Width="227px" MaxLength="50"
                                            CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDivisa" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelDivisa" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlDivisa" runat="server" Width="208px" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblMercancia" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkMercancia" runat="server" Checked="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:CheckBox ID="chkVigente" runat="server" Checked="True"   />
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
                    <asp:Label ID="lblSubTitulosMediosPago" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pro:ProsegurGridView ID="ProsegurGridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                        ColunasSelecao="codigo,CodigoDivisa,CodigoTipoMedioPago" EstiloDestaque="GridLinhaDestaque"
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
                                        <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px">                                        
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                        
                                        </TextBox>
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemStyle HorizontalAlign="Center" Width="210"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditar" CssClass="imgButton" OnClick="imgEditar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/sustituir.png" ID="imgDuplicar" CssClass="imgButton" OnClick="imgDuplicar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultar" CssClass="imgButton" OnClick="imgConsultar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluir" CssClass="imgButton" OnClick="imgExcluir_OnClick" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DescripcionTipoMedioPago" HeaderText="Tipo Médio Pago"
                                                SortExpression="DescripcionTipoMedioPago" />
                                            <asp:BoundField DataField="codigo" HeaderText="Código" SortExpression="codigo" />
                                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" SortExpression="descripcion" />
                                            <asp:BoundField DataField="DescripcionDivisa" HeaderText="Divisa" SortExpression="DescripcionDivisa" />
                                            <asp:TemplateField HeaderText="vigente" SortExpression="vigente">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Image ID="imgVigente" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CodigoDivisa" HeaderText="Código" SortExpression="codigo"
                                                Visible="false" />
                                            <asp:BoundField DataField="CodigoTipoMedioPago" HeaderText="Código" SortExpression="codigo"
                                                Visible="false" />
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
        <asp:UpdatePanel runat="server" ID="updForm">
            <ContentTemplate>
                <asp:Panel runat="server" ID="pnForm" Visible="True">
                    <div class="ui-panel-titlebar">
                        <asp:Label ID="lblTituloMediosPago" CssClass="ui-panel-title" runat="server"></asp:Label>
                    </div>
                    <table class="tabela_campos"   border="0px">
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblTipoMedioPagoForm" runat="server" CssClass="Lbl2"></asp:Label>
                            </td>
                            <td>
                                <table style="margin: 0px !Important">
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanelTipoMedioPago" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlTipoMedioPago" runat="server" Width="208px" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="csvTipoMedioPagoObrigatorio" runat="server" ErrorMessage=""
                                                        ControlToValidate="ddlTipoMedioPago" Text="*"></asp:CustomValidator><asp:CustomValidator
                                                            ID="csvTipoMedioPagoExistente" runat="server" ErrorMessage="" ControlToValidate="ddlTipoMedioPago">*</asp:CustomValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblDivisaForm" runat="server" CssClass="Lbl2"></asp:Label>
                            </td>
                            <td>
                                <table style="margin: 0px !Important;">
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlDivisaForm" runat="server" Width="240px" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="csvDivisaObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlDivisaForm"
                                                        Text="*"></asp:CustomValidator><asp:CustomValidator ID="csvDivisaMedioPagoExistente"
                                                            runat="server" ErrorMessage="" ControlToValidate="ddlDivisaForm">*</asp:CustomValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblCodigoMedioPagoForm" runat="server" CssClass="Lbl2"></asp:Label>
                            </td>
                            <td>
                                <table style="margin: 0px !Important;">
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtCodigoMedioPagoForm" runat="server" MaxLength="15" AutoPostBack="False"
                                                        CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="200px"></asp:TextBox><asp:CustomValidator ID="csvCodigoObrigatorio"
                                                            runat="server" ErrorMessage="" ControlToValidate="txtCodigoMedioPagoForm" Text="*"></asp:CustomValidator><asp:CustomValidator
                                                                ID="csvCodigoMedioPagoExistente" runat="server" ErrorMessage="" ControlToValidate="txtCodigoMedioPagoForm">*</asp:CustomValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblDescricaoMedioPagoForm" runat="server" CssClass="Lbl2"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtDescricaoMedioPagoForm" runat="server" Width="235px" MaxLength="50"
                                            CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" AutoPostBack="False"></asp:TextBox><asp:CustomValidator ID="csvDescricaoObrigatorio"
                                                runat="server" ErrorMessage="001_msg_MedioPagodescripcionobrigatorio" ControlToValidate="txtDescricaoMedioPagoForm"
                                                Text="*"></asp:CustomValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblCodigoAcceso" runat="server" CssClass="Lbl2"></asp:Label>
                            </td>
                            <td colspan="3">
                                <table style="margin: 0px !Important;">
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanelCodigoAcceso" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtCodigoAcceso" runat="server" MaxLength="1" AutoPostBack="false"
                                                        CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="20px"></asp:TextBox><asp:CustomValidator ID="csvCodigoAccesoObligatorio"
                                                            runat="server" ErrorMessage="" ControlToValidate="txtCodigoAcceso" Text="*"></asp:CustomValidator><asp:CustomValidator
                                                                ID="csvCodigoAccesoExistente" runat="server" ErrorMessage="" ControlToValidate="txtCodigoAcceso">*</asp:CustomValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblObservaciones" runat="server" CssClass="Lbl2"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtObservaciones" runat="server" MaxLength="4000" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                    Height="96px" Width="640px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblMercanciaForm" runat="server" CssClass="Lbl2"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:UpdatePanel ID="updMercancia" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="chkMercanciaForm" runat="server" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlTipoMedioPago" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblVigenteForm" runat="server" CssClass="Lbl2"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:CheckBox ID="chkVigenteForm" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <div class="ui-panel-titlebar">
                        <asp:Label ID="lblSubTitulosMediosPagoForm" CssClass="ui-panel-title" runat="server"></asp:Label>
                    </div>
                    <table border="0" width="100%">
                        <tr>
                            <td style="">
                                <asp:UpdatePanel ID="UpdatePanelGridForm" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <pro:ProsegurGridView ID="GridForm" runat="server" AllowPaging="True" AllowSorting="False"
                                            ColunasSelecao="codigo" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                                            PageSize="10" AutoGenerateColumns="False" Ajax="True" Width="95%" ExibirCabecalhoQuandoVazio="false"
                                            Height="100%">
                                            <Pager ID="objPager_GridForm">
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
                                            <TextBox ID="objTextoGridForm" AutoPostBack="True" MaxLength="10" Width="30px"> &nbsp; </TextBox>
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemStyle HorizontalAlign="Center" Width="150"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditarForm" CssClass="imgButton" OnClick="imgEditarForm_OnClick" />
                                                        <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultarForm" CssClass="imgButton" OnClick="imgConsultarForm_OnClick"/>
                                                        <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluirForm" CssClass="imgButton" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="codigo" HeaderText="Código" SortExpression="" />
                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="" />
                                                <asp:BoundField DataField="DescripcionFormato" HeaderText="Formato" SortExpression="" />
                                                <asp:TemplateField HeaderText="Vigente" SortExpression="">
                                                      <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Image ID="imgVigente" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="OrdenTermino" HeaderText="OrdenTermino" SortExpression=""
                                                    Visible="false" />
                                            </Columns>
                                        </pro:ProsegurGridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnAcima" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnAbaixo" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td align="left" style="width: 50px">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel runat="server" ID="pnBotoesGrid" Visible="True">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnAcima" runat="server" Height="23px" ImageUrl="~/Imagenes/pag03.png"
                                                            Width="25px" />&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnAbaixo" runat="server" ImageUrl="~/Imagenes/pag02.png" />&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlSemRegistroForm" runat="server" Visible="false">
                                    <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro">
                                        <tr>
                                            <td style="border-width: 0;">
                                                </td>
                                            <td style="border-width: 0;">
                                                <asp:Label ID="lblSemRegistroForm" runat="server" Text="Label" CssClass="Lbl2">Não existem dados a serem exibidos.</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
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
                    <asp:Button runat="server" ID="btnSalvar" CssClass="btn-salvar"/>
                </td>
                <td >
                     <asp:Button runat="server" ID="btnBajaConfirm"  CssClass="btn-excluir"/>
                    <div class="botaoOcultar">
                         <asp:Button runat="server" ID="Button1" CssClass="btn-excluir"/>
                       <asp:Button runat="server" ID="btnConsomeTerminoMedioPago"/>
                       <asp:Button runat="server" ID="btnBajaTermino"/>
                        <asp:Button runat="server" ID="btnBaja" />
                    </div>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn_cancelar"/>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
