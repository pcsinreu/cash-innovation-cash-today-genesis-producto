<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaSector.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaSector" 
EnableEventValidation="false" MasterPageFile="~/Master/Master.Master" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Busqueda de Sector</title>
     
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
    <asp:UpdatePanel id="uppGeral" runat="server">
        <ContentTemplate>
         <div id="Filtros" style="display: block;">
                    <fieldset id="ExpandCollapseDiv" class="ui-fieldset ui-widget ui-widget-content ui-corner-all ui-fieldset-toggleable">
                        <legend class="ui-fieldset-legend ui-corner-all ui-state-default ui-state-active">
                            <div id="DivFiltros" class="legend-collapse" onclick="ocultarExibir();">
                                <asp:Label ID="lblSubTitulosCriteriosBusqueda" CssClass="legent-text" runat="server">Filtrar Clientes</asp:Label>
                            </div>
                        </legend>
                        <div class="accordion" style="display: block;">
                            <table class="tabela_campos">
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigo" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>

                                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="225px" 
                                        MaxLength="25"></asp:TextBox>

                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDescripcion" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    
                                    <asp:TextBox ID="txtDescricao" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="225px" 
                                        MaxLength="50"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDelegacion" runat="server" CssClass="label2" nowrap="false"></asp:Label>
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlDelegacion" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                        Width="225px">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="csvDelegacion" runat="server" 
                                        ControlToValidate="ddlDelegacion" Display="Dynamic" ErrorMessage="" Text="*"></asp:CustomValidator>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblTipoSetor" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlTipoSector" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                        Width="220px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblPlanta" runat="server" CssClass="label2" nowrap="false"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPlanta" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                        Width="225px">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="csvPlanta" runat="server" ErrorMessage="" 
                                                ControlToValidate="ddlPlanta" Text="*" Display="Dynamic"></asp:CustomValidator>
                                </td>
                            </tr>
                                </table>
                            <asp:Panel runat="server" ID="pnSector" Enabled="False">
                                <asp:UpdatePanel ID="updUcSector" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                    <ContentTemplate>
                                        <asp:PlaceHolder runat="server" ID="phSector"></asp:PlaceHolder>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                            <table class="tabela_campos" style="margin-top: -19px;">   
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblVigente" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:CheckBox ID="chkVigente" runat="server" Checked />
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
                    <asp:Label ID="lblSubTitulosSector" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
            <table class="tabela_interna">
                <tr>
                    <td align="center">
                        <pro:ProsegurGridView ID="GdvResultado" runat="server" AllowPaging="True" AllowSorting="True"
                                    ColunasSelecao="oidSector" EstiloDestaque="GridLinhaDestaque" 
                            GridPadrao="False" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                                    ExibirCabecalhoQuandoVazio="False" NumeroRegistros="0" OrdenacaoAutomatica="True"
                                    PaginaAtual="0" PaginacaoAutomatica="True" Width="99%" 
                            AgruparRadioButtonsPeloName="False" 
                            ConfigurarNumeroRegistrosManualmente="False" EnableModelValidation="True" 
                            HeaderSpanStyle="">
                                    <Pager ID="objPager_ProsegurGridView1">
                                        <FirstPageButton Visible="True">
                                            <Image Url="mvwres://Prosegur.Web, Version=3.1.1203.901, Culture=neutral, PublicKeyToken=a8a76e1d318ac1f1/Prosegur.Web.pFirst.gif">
                                            </Image>
                                        </FirstPageButton>
                                        <LastPageButton Visible="True">
                                        </LastPageButton>
                                        <Summary Text="Página {0} de {1} ({2} itens)" />
                                        <SummaryStyle>
                                        </SummaryStyle>
                                    </Pager>
                                    <HeaderStyle  Font-Bold="True" />
                                    <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                    <RowStyle CssClass="GridLinhaPadraoImpar" />
                                    <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px">            
                                    </TextBox>
                                    <Columns>
                                         <asp:TemplateField HeaderText="">
                                                <ItemStyle HorizontalAlign="Center" Width="150"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditar" CssClass="imgButton" OnClick="imgEditar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultar" CssClass="imgButton" OnClick="imgConsultar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluir" CssClass="imgButton" OnClick="imgExcluir_OnClick"  />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        <asp:BoundField DataField="codSector" HeaderText="Codigo" SortExpression="codSector" />
                                        <asp:BoundField DataField="desSector" HeaderText="Descricao" SortExpression="desSector" />
                                        <asp:BoundField DataField="desPlanta" HeaderText="Planta" SortExpression="desPlanta" />
                                        <asp:BoundField DataField="desTipoSector" HeaderText="Tipo" SortExpression="desTipoSector" />
                                        <asp:BoundField DataField="desSectorPadre" HeaderText="SectorPadre" SortExpression="desSectorPadre" />
                                        <asp:TemplateField HeaderText="vigente">
                                             <ItemStyle HorizontalAlign="Center" Width="150"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Image ID="imgVigente" runat="server" />
                                                <asp:HiddenField ID="hidGrupo" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="oidSector" Visible="False" />
                                    </Columns>
                                </pro:ProsegurGridView>
                    </td>
                </tr>
                <tr>
                    <td align="center">
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
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
        <div style="margin-top: 20px;">
            <asp:UpdatePanel runat="server" ID="updForm">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="pnForm" Visible="False">
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloSectores" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table class="tabela_campos" >
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDelegacionForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlDelegacionForm" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                        Width="225px" />
                                    <asp:CustomValidator ID="csvDelegacionForm" runat="server"
                                        ControlToValidate="ddlDelegacionForm" Display="Dynamic" ErrorMessage="" Text="*"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblPlantaForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlPlantaForm" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                        Enabled="False" Width="225px" />
                                    <asp:CustomValidator ID="csvPlantaForm" runat="server"
                                        ControlToValidate="ddlPlantaForm" Display="Dynamic" ErrorMessage="" Text="*"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblTipoSector" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlTipoSectorForm" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                        Width="225px" Enabled="False" />
                                    <asp:CustomValidator ID="csvTipoSector" Display="Dynamic"
                                        runat="server" ErrorMessage="" ControlToValidate="ddlTipoSectorForm"
                                        Text="*" />
                                </td>
                            </tr>
                            </table>
                         <asp:Panel runat="server" ID="pnSectorForm" Enabled="False">
                                <asp:UpdatePanel ID="updUcSectorform" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                    <ContentTemplate>
                                        <asp:PlaceHolder runat="server" ID="phSectorForm"></asp:PlaceHolder>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                           <table class="tabela_campos" style="margin-top: -19px;" >
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigoSector" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td class="style1" colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCodigoSector" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                                Width="225px" AutoPostBack="false" MaxLength="25" Enabled="False" />
                                            &nbsp;
                                            <asp:CustomValidator ID="csvCodigoSector" runat="server" ErrorMessage=""
                                                ControlToValidate="txtCodigoSector" Text="*" Display="Dynamic" />
                                            <asp:CustomValidator ID="csvCodigoExistente" runat="server"
                                                ControlToValidate="txtCodigoSector" Display="Dynamic" ErrorMessage=""
                                                Text="*" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDescriptionSector" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtDescricaoSetor" runat="server" Width="225px"
                                                CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" MaxLength="50" Enabled="False" />
                                            <asp:CustomValidator ID="csvDescricaoSetor" runat="server" ErrorMessage=""
                                                ControlToValidate="txtDescricaoSetor" Text="*" Display="Dynamic" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigoAjeno" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td >
                                    <asp:TextBox ID="txtCodigoAjeno" runat="server" MaxLength="25"
                                        AutoPostBack="true" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="225px" Enabled="False"
                                        ReadOnly="True" />
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDescricaoCodigo" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <table style="margin: 0px !Important">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDescricaoAjeno" runat="server" AutoPostBack="true"
                                                    CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False" MaxLength="50" ReadOnly="True"
                                                    Width="225px" />
                                            </td>
                                            <td>
                                                <asp:Button runat="server" ID="btnAjeno" CssClass="ui-button" Width="130px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblVigenteForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:CheckBox ID="chkVigenteForm" runat="server" Enabled="False" />
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
                    <asp:Button runat="server" ID="btnImporteMaximo" Visible="false" CssClass="btn-novo"/>
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
                        <asp:Button runat="server" ID="btnAlertaSi" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnAlertaNo" CssClass="btn-excluir" />
                    </div>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn_cancelar"/>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>