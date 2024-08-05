<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaTipoSector.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaTipoSector" EnableEventValidation="false"
    MasterPageFile="~/Master/Master.Master" %>

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
    <style type="text/css">
        .style1 {
            height: 76px;
        }

        .style3 {
            height: 28px;
        }

        .style4 {
            height: 28px;
            width: 231px;
        }

        .style6 {
            width: 231px;
        }

        .style7 {
            height: 28px;
            width: 5px;
        }

        .style9 {
            width: 5px;
        }
    </style>
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
                                <asp:Label ID="lblTitulosTiposSetor" CssClass="legent-text" runat="server">Filtrar Clientes</asp:Label>
                            </div>
                        </legend>
                        <div class="accordion" style="display: none;">
                            <table class="tabela_campos"  >
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCodigoTipoSetor" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCodigoTipoSector" runat="server" Width="200px"
                                            MaxLength="15" cssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDescricaoTipoSector" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDescricaoTipoSector" runat="server" Width="260px"
                                            MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCaracteristicas" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td rowspan="4">
                                        <asp:ListBox ID="lstCaracteristicas" runat="server" SelectionMode="Multiple"
                                            Width="420px"></asp:ListBox>
                                        &nbsp;
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblVigente" runat="server" CssClass="label2" Text="lblVigente"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkVigente" runat="server" Checked="true" CssClass="label2" />
                                    </td>

                                </tr>
                            </table>
                            <table style="margin-top: 20px;">
                                <tr>
                                    <td align="right" colspan="5">
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
                            </table>
                        </div>
                    </fieldset>
                </div>
                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblSubTitulosTiposSetor" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center" width="100%">
                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pro:ProsegurGridView ID="ProsegurGridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                        ColunasSelecao="codTipoSector" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                                        PageSize="10" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                                        NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0" PaginacaoAutomatica="True"
                                        Width="99%" Height="100%" ExibirCabecalhoQuandoVazio="false">
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
                                                <ItemStyle HorizontalAlign="Center" Width="150"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditar"  OnClick="imgEditar_OnClick" CssClass="imgButton" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultar" OnClick="imgConsultar_OnClick" CssClass="imgButton" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluir" OnClick="imgExcluir_OnClick" CssClass="imgButton" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="codTipoSector" HeaderText="Codigo" SortExpression="codTipoSector" />
                                            <asp:BoundField DataField="desTipoSector" HeaderText="Descripcion" SortExpression="desTipoSector" />
                                            <asp:TemplateField HeaderText="Vigente">
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
                            <asp:Label ID="lblTituloTiposSetor" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                            <table class="tabela_campos"  >
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCodigoTiposSetor" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtCodigoTipoSetor" runat="server" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                                    MaxLength="15" Width="160px"></asp:TextBox>
                                                &nbsp;<asp:CustomValidator ID="csvCodigoObrigatorio" runat="server" 
                                                    ControlToValidate="txtCodigoTipoSetor" Text="*"></asp:CustomValidator>
                                                <asp:CustomValidator ID="csvCodigoExistente" runat="server" 
                                                    ControlToValidate="txtCodigoTipoSetor" Text="*"></asp:CustomValidator>
                                            </ContentTemplate> 
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtCodigoTipoSetor" 
                                                    EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel> 
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDescricaoTiposSetor" runat="server" CssClass="label2" 
                                            Width="134px"></asp:Label>
                                    </td>
                                    <td colspan="4" >
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                           <ContentTemplate>
                                                <asp:TextBox ID="txtDescricaoTiposSetor" runat="server" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                                    MaxLength="50" Width="250px"></asp:TextBox>
                                                <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ControlToValidate="txtDescricaoTiposSetor"
                                                    ErrorMessage="019_msg_tipoSetordescripcionobligatorio" Text="*"></asp:CustomValidator>
                                                </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtDescricaoTiposSetor" 
                                                    EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCodigoAjeno" runat="server" CssClass="label2" Width="134px"></asp:Label>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtCodigoAjeno" runat="server" AutoPostBack="true" 
                                            CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="25" Width="160px" Enabled="False" 
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDescricaoCodeA" runat="server" CssClass="label2" Width="134px"></asp:Label>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtDescricaoCodigoAjeno" runat="server" AutoPostBack="true" 
                                            CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="50" Width="100%" Enabled="False" 
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td >
                                        <asp:Button runat="server" ID="btnAltaAjeno" CssClass="ui-button" Width="150"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblVigenteForm" runat="server" CssClass="label2" Text="lblVigente"></asp:Label>
                                    </td>
                                    <td  colspan="4">
                                        <asp:CheckBox ID="chkVigenteForm" runat="server" CssClass="label2" AutoPostBack="false" />
                                    </td>
                                </tr>
                                </table>
                          <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloCaracteristicas" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                               <ContentTemplate>
                                    <table class="tabela_campos"  >
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCaracteristicasForm" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td align="right" width="420px">
                                        <asp:ListBox ID="lstCaracteristicasDisponiveis" runat="server" Height="120px" SelectionMode="Multiple"
                                            Width="400px"></asp:ListBox>
                                    </td>
                                    <td align="center" width="68px">
                                        <asp:ImageButton ID="imbAdicionarTodasCaracteristicas" runat="server" ImageUrl="~/Imagenes/pag07.png"
                                            Style="height: 25px" /><br />
                                        <asp:ImageButton ID="imbAdicionarCaracteristicasSelecionadas" runat="server" ImageUrl="~/Imagenes/pag05.png" /><br />
                                        <asp:ImageButton ID="imbRemoverCaracteristicasSelecionadas" runat="server" ImageUrl="~/Imagenes/pag06.png" /><br />
                                        <asp:ImageButton ID="imbRemoverTodasCaracteristicas" runat="server" ImageUrl="~/Imagenes/pag08.png" />
                                    </td>
                                    <td align="left" width="420px">
                                        <asp:ListBox ID="lstCaracteristicasSelecionadas" runat="server" Height="120px" SelectionMode="Multiple"
                                            Width="400px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:ListBox>
                                            
                                    </td>
                                    <td>
                                        <asp:CustomValidator ID="csvlstCaracteristicas" runat="server" ControlToValidate="lstCaracteristicasSelecionadas"
                                                        ErrorMessage="019_msg_CaracteristicaObrigatoria" Text="*"></asp:CustomValidator>
                                    </td>
                                </tr>
                            </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="lstCaracteristicasSelecionadas" 
                                        EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
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
                        <asp:Button runat="server" ID="btnConsomeCodigoAjeno" CssClass="btn-excluir"/>
                    </div>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn_cancelar"/>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>

