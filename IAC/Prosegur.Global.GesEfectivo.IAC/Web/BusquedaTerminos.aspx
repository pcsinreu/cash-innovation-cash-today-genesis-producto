<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaTerminos.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaTerminos" EnableEventValidation="false"
    MasterPageFile="~/Master/Master.Master" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Búsqueda de Terminos IAC</title>
     
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
    <asp:HiddenField runat="server" id="hiddenCodigo"/>
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
                            <table class="tabela_campos"   border="0">
                                <tr>
                                    <td class="tamanho_celula_auto">
                                        <asp:Label ID="lblCodigoTermino" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCodigoTermino" runat="server" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                            Width="150"></asp:TextBox>
                                    </td>
                                    <td class="tamanho_celula_auto">
                                        <asp:Label ID="lblDescricaoTermino" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDescricaoTermino" runat="server" Width="227px" MaxLength="50"
                                            CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula_auto">
                                        <asp:Label ID="lblDescricaoFormato" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelTipoFormato" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlTipoFormato" runat="server" Width="208px" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="tamanho_celula_auto">
                                        <asp:Label ID="lblMostraCodigo" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkMostraCodigo" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula_auto">
                                        <asp:Label ID="lblVigente" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkVigente" runat="server" Checked />
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
                    <asp:Label ID="lblSubTitulosTerminos" CssClass="ui-panel-title" runat="server"></asp:Label>
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
                                        PaginaAtual="0" PaginacaoAutomatica="True" Width="95%">
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
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditar" OnClick="imgEditar_OnClick" CssClass="imgButton" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultar" OnClick="imgConsultar_OnClick" CssClass="imgButton" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluir" OnClick="imgExcluir_OnClick" CssClass="imgButton" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="codigo" HeaderText="Código" SortExpression="codigo" />
                                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" SortExpression="descripcion" />
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
              <asp:Label ID="lblTituloTermino" CssClass="ui-panel-title" runat="server"></asp:Label>
          </div>
          <table class="tabela_campos"  >
              <tr>
                  <td class="tamanho_celula">
                      <asp:Label ID="lblCodigoTerminoForm" runat="server" CssClass="label2"></asp:Label>
                  </td>
                  <td >
                      <table width="100%" cellpadding="0px" cellspacing="0px">
                          <tr>
                              <td width="140px">
                                  <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server">
                                      <ContentTemplate>
                                          <asp:TextBox ID="txtCodigoTerminoForm" runat="server" MaxLength="50" AutoPostBack="False"
                                              CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="120px"></asp:TextBox><asp:CustomValidator ID="csvCodigoObrigatorio"
                                                  runat="server" ErrorMessage="" ControlToValidate="txtCodigoTerminoForm" Text="*"></asp:CustomValidator>
                                          <asp:CustomValidator ID="csvCodigoTerminoExistente" runat="server" ErrorMessage=""
                                              ControlToValidate="txtCodigoTerminoForm">*</asp:CustomValidator>
                                      </ContentTemplate>
                                  </asp:UpdatePanel>
                              </td>
                          </tr>
                      </table>
                  </td>
                  <td class="tamanho_celula">
                      <asp:Label ID="lblDescricaoTerminoForm" runat="server" CssClass="label2"></asp:Label>
                  </td>
                  <td>
                      <table width="100%" cellpadding="0px" cellspacing="0px">
                          <tr>
                              <td width="250px">
                                  <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server">
                                      <ContentTemplate>
                                          <asp:TextBox ID="txtDescricaoTerminoForm" runat="server" Width="225px" MaxLength="50"
                                              CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" AutoPostBack="False"></asp:TextBox><asp:CustomValidator ID="csvDescricaoObrigatorio"
                                                  runat="server" ErrorMessage="" ControlToValidate="txtDescricaoTerminoForm" Text="*"></asp:CustomValidator>
                                          <asp:CustomValidator ID="csvDescripcionExistente" runat="server" ErrorMessage=""
                                              ControlToValidate="txtDescricaoTerminoForm">*</asp:CustomValidator>
                                      </ContentTemplate>
                                  </asp:UpdatePanel>
                              </td>
                          </tr>
                      </table>
              </tr>
              <tr>
                  <td class="tamanho_celula">
                      <asp:Label ID="lblObservaciones" runat="server" CssClass="label2"></asp:Label>
                  </td>
                  <td colspan="3">
                      <asp:TextBox ID="txtObservaciones" runat="server" MaxLength="4000" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all "
                          Height="96px" Width="650px" TextMode="MultiLine"></asp:TextBox>
                  </td>
              </tr>
              <tr>
                  <td class="tamanho_celula">
                      <asp:Label ID="lblMostrarCodigo" runat="server" CssClass="label2"></asp:Label>
                  </td>
                  <td>
                      <asp:CheckBox ID="chkMostrarCodigo" runat="server" />
                  </td>
                  <td class="tamanho_celula">
                      <asp:Label ID="lblVigenteForm" runat="server" CssClass="label2"></asp:Label>
                  </td>
                  <td>
                      <asp:CheckBox ID="chkVigenteForm" runat="server" />
                  </td>
              </tr>
              <tr>
                  <td class="tamanho_celula">
                      <asp:Label ID="lblAceptarDigitacion" runat="server" CssClass="label2"></asp:Label>
                  </td>
                  <td colspan="3">
                      <asp:CheckBox ID="chkAceptarDigitacion" runat="server" />
                  </td>
                 
              </tr>
              <tr>
                  <td class="tamanho_celula">
                      <asp:Label ID="lblTipoFormato" runat="server" CssClass="label2"></asp:Label>
                  </td>
                  <td>
                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                          <ContentTemplate>
                              <asp:DropDownList ID="ddlTipoFormatoForm" runat="server" Width="208px" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory">
                              </asp:DropDownList>
                              <asp:CustomValidator ID="csvTipoFormatoObrigatorio" runat="server" ControlToValidate="ddlTipoFormatoForm"
                                  ErrorMessage="">*</asp:CustomValidator>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                  </td>
                  <td class="tamanho_celula">
                      <asp:Label ID="lblLongitud" runat="server" CssClass="label2"></asp:Label>
                  </td>
                  <td colspan="2">
                      <asp:UpdatePanel ID="UpdatePanelLongitud" runat="server" UpdateMode="Conditional">
                          <ContentTemplate>
                              <asp:TextBox ID="txtLongitud" runat="server" Width="100px" MaxLength="2" AutoPostBack="True"
                                  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" ></asp:TextBox>
                              <asp:CustomValidator ID="csvLongitudeObrigatorio" runat="server" ErrorMessage=""
                                  ControlToValidate="txtLongitud">*</asp:CustomValidator>
                              <asp:CustomValidator ID="csvLongitude" runat="server" ErrorMessage="" ControlToValidate="txtLongitud">*</asp:CustomValidator>
                          </ContentTemplate>
                          <Triggers>
                              <asp:AsyncPostBackTrigger ControlID="ddlTipoFormatoForm" EventName="SelectedIndexChanged" />
                               <asp:AsyncPostBackTrigger ControlID="ddlAlgoritmo" EventName="SelectedIndexChanged" />
                          </Triggers>
                      </asp:UpdatePanel>
                  </td>
              </tr>
              <tr>
                  <td class="tamanho_celula">
                      <asp:Label ID="lblValidacao" runat="server" CssClass="label2"></asp:Label>
                  </td>
                  <td colspan="3">
                      <table border="0" cellpadding="0" cellspacing="10" style="margin: 0px !important;">
                          <tr>
                              <td colspan="1">
                                  <asp:RadioButton ID="rbtSinValidacion" runat="server" GroupName="Validacion" AutoPostBack="True" />
                              </td>
                          </tr>
                          <tr>
                              <td>
                                  <asp:RadioButton ID="rbtMascara" runat="server" GroupName="Validacion" AutoPostBack="True" />
                              </td>
                              <td>
                                  <asp:UpdatePanel ID="UpdatePanelMascara" runat="server">
                                      <ContentTemplate>
                                          <asp:DropDownList ID="ddlMascara" runat="server" Width="200px" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory">
                                          </asp:DropDownList>
                                          <asp:CustomValidator ID="csvMascaraObrigatorio" runat="server" ControlToValidate="ddlMascara">*</asp:CustomValidator>
                                      </ContentTemplate>
                                  </asp:UpdatePanel>
                              </td>
                          </tr>
                          <tr>
                              <td colspan="1">
                                  <asp:RadioButton ID="rbtListaValores" runat="server" GroupName="Validacion" AutoPostBack="True" />
                              </td>
                          </tr>
                          <tr>
                              <td>
                                  <asp:RadioButton ID="rbtFormula" runat="server"
                                      GroupName="Validacion" AutoPostBack="True" />
                              </td>
                              <td>
                                  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                      <ContentTemplate>
                                          <asp:DropDownList ID="ddlAlgoritmo" runat="server" Width="300px"
                                              AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory">
                                          </asp:DropDownList>
                                          <asp:CustomValidator ID="csvAlgoritmoObrigatorio" runat="server" ControlToValidate="ddlAlgoritmo">*</asp:CustomValidator>
                                      </ContentTemplate>
                                  </asp:UpdatePanel>
                              </td>
                          </tr>
                      </table>
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
