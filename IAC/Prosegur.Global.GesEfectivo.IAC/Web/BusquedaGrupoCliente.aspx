<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaGrupoCliente.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaGrupoCliente" EnableEventValidation="false"
    MasterPageFile="~/Master/Master.Master" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - B&uacute;squeda Grupos de Clientes</title>
     
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
                            <table class="tabela_campos" >
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigoGrupoCliente" runat="server" CssClass="Lbl2"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtCodigoGrupoCliente" runat="server" MaxLength="15" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDescricaoGrupoCliente" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescricaoGrupoCliente" runat="server" Width="280px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                </td>
                            </tr>
                            </table>
                             <asp:UpdatePanel ID="updUcClienteUc" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                <ContentTemplate>
                                    <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <table class="tabela_campos">
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkVigente" runat="server" Checked />

                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblTipoGrupoCliente" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTipoGrupoCliente" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ></asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button runat="server" ID="btnBuscar" CssClass="ui-button" Width="100px" ClientIDMode="Static" />
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
                    <asp:Label ID="lblSubTitulosGrupoCliente" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
            <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center">
                        <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <pro:ProsegurGridView ID="ProsegurGridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                    ColunasSelecao="Codigo,Descripcion,Vigente"
                                    EstiloDestaque="GridLinhaDestaque" GridPadrao="False" PageSize="10" AutoGenerateColumns="False"
                                    Ajax="True" GerenciarControleManualmente="True" ExibirCabecalhoQuandoVazio="false"
                                    NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0" PaginacaoAutomatica="True"
                                    Width="99%" Height="100%">
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
                                                <ItemStyle HorizontalAlign="Center" Width="210"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditar" CssClass="imgButton" OnClick="imgEditar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultar" CssClass="imgButton" OnClick="imgConsultar_OnClick"/>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluir" CssClass="imgButton" OnClick="imgExcluir_OnClick" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        <asp:BoundField DataField="Codigo" HeaderText="Código" SortExpression="Codigo" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />                                        
                                        <asp:TemplateField HeaderText="Vigente">
                                              <ItemStyle HorizontalAlign="Center" Width="210"></ItemStyle>
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
                        <asp:UpdatePanel ID="UpdatePanelGridSemRegistro" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
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
                    <asp:Panel runat="server" ID="pnForm" Visible="False">
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloGrupoCliente" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table class="tabela_campos">
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigo" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upCodigo" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCodigo" runat="server" MaxLength="15" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                                IdDivExibicao="DivCodigo" Width="130px"></asp:TextBox>
                                            <asp:CustomValidator ID="csvCodigoObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtCodigo"
                                                Text="*"></asp:CustomValidator>
                                            <asp:CustomValidator ID="csvCodigoExistente" runat="server" ErrorMessage="" ControlToValidate="txtCodigo">*</asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDescripcion" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td>
                                    <table style="margin: 0px !Important;">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="upDescripcion" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtDescripcion" runat="server"  MaxLength="50" AutoPostBack="False"
                                                            CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" IdDivExibicao="DivDescripcion"></asp:TextBox>
                                                        <asp:CustomValidator ID="csvDescripcionObrigatorio" runat="server" ErrorMessage=""
                                                            ControlToValidate="txtDescripcion" Text="*" Width="1px"></asp:CustomValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnDireccion" runat="server" CssClass="ui-button" Width="130" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblTipoGrupoClienteForm" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                        <ContentTemplate>
                                            <asp:CustomValidator ID="csvTipoGrupoClienteObrigatorio" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlTipoGrupoClienteForm" Text="*" Width="1px"></asp:CustomValidator>
                                            <asp:DropDownList ID="ddlTipoGrupoClienteForm" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblVigenteForm" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td >
                                    <asp:CheckBox ID="chkVigenteForm" runat="server" Checked />
                                </td>
                            </tr>
                        </table>
                        <asp:UpdatePanel ID="updUcClienteForm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <asp:PlaceHolder runat="server" ID="phClienteForm"></asp:PlaceHolder>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="btnAnadir" CssClass="btn-novo" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" style="margin-top: 30px;">
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblClienteResultado" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <pro:ProsegurGridView ID="gdvClientes" runat="server" AllowPaging="False"
                                        AllowSorting="False" ColunasSelecao="Codigo" EstiloDestaque="GridLinhaDestaque"
                                        GridPadrao="False" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                                        NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0" PaginacaoAutomatica="True"
                                        Width="99%" Height="100%" ExibirCabecalhoQuandoVazio="False"
                                        AgruparRadioButtonsPeloName="False"
                                        ConfigurarNumeroRegistrosManualmente="False" EnableModelValidation="True"
                                        HeaderSpanStyle=""
                                        DataKeyNames="OidGrupoClienteDetalle,OidCliente">
                                        <Pager ID="objPager_gdvClientes">
                                            <FirstPageButton Visible="True">
                                                <Image Url="mvwres://Prosegur.Web, Version=3.1.1203.901, Culture=neutral, PublicKeyToken=a8a76e1d318ac1f1/Prosegur.Web.pFirst.gif">
                                                </Image>
                                            </FirstPageButton>
                                            <LastPageButton Visible="True">
                                            </LastPageButton>
                                            <Summary Text="Pgina {0} de {1} ({2} itens)" />
                                            <SummaryStyle>
                                            </SummaryStyle>
                                        </Pager>
                                        <HeaderStyle Font-Bold="True" />
                                        <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                        <RowStyle CssClass="GridLinhaPadraoImpar" HorizontalAlign="Center" />
                                        <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </TextBox>
                                        <Columns>
                                            <asp:BoundField DataField="CodCliente" HeaderText="CodCliente" SortExpression="CodCliente" />
                                            <asp:BoundField HeaderText="DesCliente" DataField="DesCliente" SortExpression="DesCliente" />
                                            
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" ImageUrl="~/App_Themes/Padrao/css/img/grid/deletar.png" CssClass="imgButton" CommandName="Deletar" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </pro:ProsegurGridView>

                                    <asp:Panel ID="pnlSemRegistroCliente" runat="server" Visible="false">
                                        <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro" style="margin: 0px !Important;">
                                            <tr>
                                                <td style="border-width: 0;">
                                                    <asp:Image ID="Image1" runat="server" src="Imagenes/info.png" />
                                                </td>
                                                <td style="border-width: 0;">
                                                    <asp:Label ID="lblSemRegistroCliente" runat="server" CssClass="Lbl2" Text="Label">Não existem dados a serem exibidos.</asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>

                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblSubClienteResultado" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <pro:ProsegurGridView ID="gdvSubClientes" runat="server" Ajax="True" AllowPaging="False"
                                        AllowSorting="False" AutoGenerateColumns="False" EstiloDestaque="GridLinhaDestaque"
                                        ExibirCabecalhoQuandoVazio="False" GerenciarControleManualmente="True" GridPadrao="False"
                                        Height="100%" NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0"
                                        PaginacaoAutomatica="True" Width="99%" AgruparRadioButtonsPeloName="False" ConfigurarNumeroRegistrosManualmente="False"
                                        EnableModelValidation="True" HeaderSpanStyle=""
                                        DataKeyNames="OidGrupoClienteDetalle,OidCliente,oidSubCliente">
                                        <Pager ID="objPager_gdvClientes0">
                                            <FirstPageButton Visible="True">
                                                <Image Url="mvwres://Prosegur.Web, Version=3.1.1203.901, Culture=neutral, PublicKeyToken=a8a76e1d318ac1f1/Prosegur.Web.pFirst.gif">
                                                </Image>
                                            </FirstPageButton>
                                            <LastPageButton Visible="True">
                                            </LastPageButton>
                                            <Summary Text="Pgina {0} de {1} ({2} itens)" />
                                            <SummaryStyle>
                                            </SummaryStyle>
                                        </Pager>
                                        <HeaderStyle Font-Bold="True" />
                                       <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                        <RowStyle CssClass="GridLinhaPadraoImpar" HorizontalAlign="Center" />
                                        <TextBox ID="objTextoProsegurGridView2" AutoPostBack="True" MaxLength="10"
                                            Width="30px"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </TextBox>
                                        <Columns>
                                            <asp:BoundField DataField="CodCliente" HeaderText="CodCliente" SortExpression="CodCliente" />
                                            <asp:BoundField HeaderText="DesCliente" DataField="DesCliente" SortExpression="DesCliente" />
                                            <asp:BoundField DataField="codSubCliente" HeaderText="codSubCliente" SortExpression="codSubCliente" />
                                            <asp:BoundField DataField="DesSubCliente" HeaderText="DesSubCliente" SortExpression="DesSubCliente" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" ImageUrl="~/App_Themes/Padrao/css/img/grid/deletar.png" CssClass="imgButton" CommandName="Deletar" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </pro:ProsegurGridView>

                                    <asp:Panel ID="pnlSemRegistroSubCliente" runat="server" Visible="false">
                                        <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro" style="margin: 0px !Important;">
                                            <tr>
                                                 <td style="border-width: 0;">
                                                    <asp:Image ID="Image2" runat="server" src="Imagenes/info.png" />
                                                </td>
                                                <td style="border-width: 0;">
                                                    <asp:Label ID="lblSemRegistroSubCliente" runat="server" CssClass="Lbl2" Text="Label">Não existem dados a serem exibidos.</asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>

                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblPtoServicioResultado" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <pro:ProsegurGridView ID="gdvPtoServicio" runat="server" Ajax="True" AllowPaging="False"
                                        AllowSorting="False" AutoGenerateColumns="False" EstiloDestaque="GridLinhaDestaque"
                                        ExibirCabecalhoQuandoVazio="False" GerenciarControleManualmente="True" GridPadrao="False"
                                        Height="100%" NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0"
                                        PaginacaoAutomatica="True" Width="99%" AgruparRadioButtonsPeloName="False" ConfigurarNumeroRegistrosManualmente="False"
                                        EnableModelValidation="True" HeaderSpanStyle=""
                                        DataKeyNames="OidGrupoClienteDetalle,OidCliente,oidSubCliente,oidPtoServicio">
                                        <Pager ID="objPager_gdvClientes1">
                                            <FirstPageButton Visible="True">
                                                <Image Url="mvwres://Prosegur.Web, Version=3.1.1203.901, Culture=neutral, PublicKeyToken=a8a76e1d318ac1f1/Prosegur.Web.pFirst.gif">
                                                </Image>
                                            </FirstPageButton>
                                            <LastPageButton Visible="True">
                                            </LastPageButton>
                                            <Summary Text="Pgina {0} de {1} ({2} itens)" />
                                            <SummaryStyle>
                                            </SummaryStyle>
                                        </Pager>
                                        <HeaderStyle  Font-Bold="True" />
                                       <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                        <RowStyle CssClass="GridLinhaPadraoImpar" HorizontalAlign="Center" />
                                        <TextBox ID="objTextoProsegurGridView3" AutoPostBack="True" MaxLength="10"
                                            Width="30px"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </TextBox>
                                        <Columns>
                                            <asp:BoundField DataField="CodCliente" HeaderText="CodCliente" SortExpression="CodCliente" />
                                            <asp:BoundField HeaderText="DesCliente" DataField="DesCliente" SortExpression="DesCliente" />
                                            <asp:BoundField DataField="codSubCliente" HeaderText="codSubCliente" SortExpression="codSubCliente" />
                                            <asp:BoundField DataField="DesSubCliente" HeaderText="DesSubCliente" SortExpression="DesSubCliente" />
                                            <asp:BoundField DataField="codPtoServicio" HeaderText="codPtoServicio" SortExpression="codPtoServicio" />
                                            <asp:BoundField DataField="DesPtoServicio" HeaderText="DesPtoServicio" SortExpression="DesPtoServicio" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" ImageUrl="~/App_Themes/Padrao/css/img/grid/deletar.png" CssClass="imgButton" CommandName="Deletar" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </pro:ProsegurGridView>

                                    <asp:Panel ID="pnlSemRegistroPtoServico" runat="server" Visible="false">
                                        <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro" style="margin: 0px !Important;">
                                            <tr>
                                                 <td style="border-width: 0;">
                                                    <asp:Image ID="Image3" runat="server" src="Imagenes/info.png" />
                                                </td>
                                                <td style="border-width: 0;">
                                                    <asp:Label ID="lblSemRegistroPtoServico" runat="server" CssClass="Lbl2" Text="Label">Não existem dados a serem exibidos.</asp:Label>
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
    </div>
     <script type="text/javascript">
         //Script necessário para evitar que dê erro ao clicar duas vezes em algum controle que esteja dentro do updatepanel.
         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_initializeRequest(initializeRequest);

         function initializeRequest(sender, args) {
             if (prm.get_isInAsyncPostBack()) {
                 args.set_cancel(true);
             }
         }
                </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="cphBotonesRodapie">
    <center>
        <table>
            <tr align="center">
               <%--  <td>
                    <asp:Button runat="server" ID="btnAnadir" CssClass="btn-novo"/>
                </td>--%>
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
                        <asp:Button runat="server" ID="btnConsomeDireccion" CssClass="btn-excluir"/>
                    </div>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn_cancelar"/>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
