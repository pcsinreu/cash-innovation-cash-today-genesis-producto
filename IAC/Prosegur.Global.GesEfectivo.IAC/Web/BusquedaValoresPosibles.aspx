<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaValoresPosibles.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaValoresPosibles" EnableEventValidation="false"
    MasterPageFile="~/Master/Master.Master" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Búsqueda Valores Posibles</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
        <script type="text/javascript">
        function ocultarExibir() {
            $("#DivFiltros").toggleClass("legend-expand");
            $(".accordion").slideToggle("fast");
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="content">
    <asp:HiddenField runat="server" id="hiddenCodigo"/>
    <asp:UpdatePanel ID="UpdatePanelGeral" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
        <ContentTemplate>
        <div id="Filtros" style="display: block;">
            <fieldset id="ExpandCollapseDiv" class="ui-fieldset ui-widget ui-widget-content ui-corner-all ui-fieldset-toggleable">
                <legend class="ui-fieldset-legend ui-corner-all ui-state-default ui-state-active">
                    <div id="DivFiltros" class="legend-collapse" onclick="ocultarExibir();">
                        <asp:Label ID="lblTitulosValoresPosibles" CssClass="legent-text" runat="server">Filtrar Clientes</asp:Label>
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
                                    <asp:Label ID="lblTerminos" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td >
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlTerminos" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" AutoPostBack="True" >
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="csvTermino" runat="server" ControlToValidate="ddlTerminos">*</asp:CustomValidator><asp:CustomValidator
                                                ID="csvTermino1" runat="server" ErrorMessage="" ControlToValidate="ddlTerminos">*</asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
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
                    <asp:Label ID="lblSubTitulosValoresPosibles" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
            <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center" width="100%">
                        <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <pro:ProsegurGridView ID="ProsegurGridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                    ColunasSelecao="Codigo" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                                    AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True" NumeroRegistros="0"
                                    OrdenacaoAutomatica="True" PaginaAtual="0" PaginacaoAutomatica="True" Width="99%"
                                    Height="100%" ExibirCabecalhoQuandoVazio="False" AgruparRadioButtonsPeloName="False"
                                    ConfigurarNumeroRegistrosManualmente="False" HeaderSpanStyle="">
                                    <Pager ID="objPager_ProsegurGridView1">
                                        <FirstPageButton Visible="True">
                                            <Image Url="mvwres://Prosegur.Web, Version=3.0.1010.2601, Culture=neutral, PublicKeyToken=a8a76e1d318ac1f1/Prosegur.Web.pFirst.gif">
                                            </Image>
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
                                        <asp:BoundField DataField="Codigo" HeaderText="Codigo" SortExpression="Codigo" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                                        <asp:CheckBoxField HeaderText="Valor por Defecto" DataField="esValorDefecto" SortExpression="esValorDefecto" >
                                              <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:CheckBoxField>
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
    <asp:UpdatePanel runat="server" ID="updForm">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnForm" Visible="True">
                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblTituloValoresPosibles" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabela_campos">
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblCodigo" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td>
                            <table style="margin: 0px !important;">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" >
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtCodigo" runat="server" MaxLength="15" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox><asp:CustomValidator
                                                    ID="csvCodigo" runat="server" ControlToValidate="txtCodigo">*</asp:CustomValidator>
                                                <asp:CustomValidator ID="csvCodigoExistente" runat="server" ErrorMessage="" ControlToValidate="txtCodigo">*</asp:CustomValidator>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblDescricao" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td>
                            <table style="margin: 0px !important;">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtDescricao" runat="server" Width="260px" MaxLength="36" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                                                <asp:CustomValidator ID="csvDescricao" runat="server" ControlToValidate="txtDescricao">*</asp:CustomValidator>
                                                <asp:CustomValidator ID="csvDescripcionExistente" runat="server" ErrorMessage=""
                                                    ControlToValidate="txtDescricao">*</asp:CustomValidator>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblValorDefecto" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelValorDefecto" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBox ID="chkValorDefecto" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
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
