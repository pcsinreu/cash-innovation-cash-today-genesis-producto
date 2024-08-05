<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="BusquedaSubClientes.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaSubClientes" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario.Tradutor" %>
<%@ Register Src="~/Controles/ucDatosBanc.ascx" TagPrefix="uc1" TagName="ucDatosBanc" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Búsqueda de SubClientes</title>
     
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="Filtros" style="display: block;">
                    <fieldset id="ExpandCollapseDiv" class="ui-fieldset ui-widget ui-widget-content ui-corner-all ui-fieldset-toggleable">
                        <legend class="ui-fieldset-legend ui-corner-all ui-state-default ui-state-active">
                            <div id="DivFiltros" class="legend-collapse" onclick="ocultarExibir();">
                                <asp:Label ID="lblSubTitulosCriteriosBusqueda" CssClass="legent-text" runat="server">Filtrar Clientes</asp:Label>
                            </div>
                        </legend>
                        <div class="accordion" style="display: block;">
                             <asp:UpdatePanel ID="updUcClienteUc" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                <ContentTemplate>
                                    <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            
                            <table class="tabela_campos" style="margin-top: -18px;">
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCodSubCliente" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCodSubCliente" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                            MaxLength="15" Width="150"></asp:TextBox>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDescSubCliente" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDescSubCliente" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                            MaxLength="100" Width="215px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblTipoSubCliente" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>

                                        <asp:DropDownList ID="ddlTipoSubCliente" runat="server" 
                                            Width="198px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                                        </asp:DropDownList>

                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblTotSaldo" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTipoTotalSaldo" runat="server"  Width="230px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                                        </asp:DropDownList>
                                    </td>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblVigente" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:CheckBox ID="chkVigente" runat="server" Checked="true" />
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
                </div>
                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblSubTitulo" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                  <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center">
                              <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                             <asp:Panel runat="server" ID="pnGrid" Visible="False">
                                     <dx:ASPxGridView runat="server" ID="gvSubClientes" AutoGenerateColumns="False" KeyFieldName="OidSubCliente" Width="99%" OnHtmlRowCreated="gvSubClientes_OnHtmlRowCreated"  DataSourceForceStandardPaging="True" OnPageIndexChanged="gvSubClientes_OnPageIndexChanged">
                                        <SettingsPager Position="Bottom" Mode="ShowPager">
                                            <PageSizeItemSettings Items="10, 20, 50" />
                                        </SettingsPager>
                                        <Styles>
                                            <AlternatingRow CssClass="dxgvDataRow tr-color"></AlternatingRow>
                                            <RowHotTrack CssClass="GridLinhaDestaque2"></RowHotTrack>
                                        </Styles>
                                        <SettingsBehavior  EnableRowHotTrack="True" SortMode="DisplayText" AllowSort="False"></SettingsBehavior>
                                         <ClientSideEvents EndCallback="function(s,e) {s.SetVisible(s.GetVisibleRowsOnPage() > 0);}"/>
                                         <Columns>
                                          <dx:GridViewDataColumn Width="140">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                                                <DataItemTemplate>
                                                     <asp:Image ID="imgEdicao" runat="server" />
                                                     <asp:Image ID="imgConsultar" runat="server" />
                                                     <asp:Image ID="imgExcluir" runat="server" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="CodCliente">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                 <DataItemTemplate>
                                                    <asp:Label runat="server" ID="lblDesCliente"></asp:Label>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="DesCliente">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </dx:GridViewDataTextColumn>
                                              <dx:GridViewDataTextColumn FieldName="CodSubCliente">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="DesSubCliente">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="CodTipoSubCliente">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <DataItemTemplate>
                                                    <asp:Label runat="server" ID="lblDesTipoSubCliente"></asp:Label>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                              <dx:GridViewDataTextColumn FieldName="DesTipoSubCliente">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="BolTotalizadorSaldo">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <DataItemTemplate>
                                                    <asp:Label runat="server" ID="lblBolTotalizadorSaldo"></asp:Label>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                           <dx:GridViewDataColumn FieldName="BolVigente">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                                                <DataItemTemplate>
                                                      <asp:Image ID="imgBolvigente" runat="server" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
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
                    <asp:Panel runat="server" ID="pnForm" Visible="false">
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloSubCliente" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <asp:UpdatePanel ID="updUcClienteForm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <asp:Panel runat="server" ID="pnUcClienteform" Enabled="True">
                                     <asp:PlaceHolder runat="server" ID="phClientesForm"></asp:PlaceHolder>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <table class="tabela_campos" style="margin-top: -18px;">
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblTipoSubClienteForm" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td colspan="4">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlTipoSubClienteForm" runat="server" Width="208px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="csvTipoSubClienteObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlTipoSubClienteForm" Text="*"></asp:CustomValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblCodigoAjeno" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodigoAjeno" runat="server" Width="200px" MaxLength="25" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="True" ReadOnly="True" Enabled="False"/>
                        </td>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblDesCodigoAjeno" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td colspan="2">
                            <table style="margin: 0px !Important">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDesCodigoAjeno" runat="server" Width="200px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="True" ReadOnly="True" Enabled="False" />
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="btnAltaAjeno" CssClass="ui-button" Width="150" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblCodSubClienteForm" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtCodigoSubClienteForm" runat="server" MaxLength="15" AutoPostBack="True" OnTextChanged="txtCodigoSubClienteForm_TextChanged" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="200px"></asp:TextBox>
                                    <asp:CustomValidator ID="csvCodSubClienteObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtCodigoSubClienteForm" Text="*"></asp:CustomValidator>
                                    <asp:CustomValidator ID="csvCodSubClienteExistente" runat="server" ErrorMessage="" ControlToValidate="txtCodigoSubClienteForm">*</asp:CustomValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblDescSubClienteForm" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtDescSubClienteForm" runat="server" MaxLength="100" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="200px"></asp:TextBox>
                                    <asp:CustomValidator ID="csvDescSubClienteObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtDescSubClienteForm" Text="*"></asp:CustomValidator>
                                    <asp:CustomValidator ID="csvDescSubClienteExistente" runat="server" ErrorMessage="" ControlToValidate="txtDescSubClienteForm" Text="*"></asp:CustomValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                             <asp:Button runat="server" ID="btnDireccion" CssClass="ui-button" Width="150" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblTotSaldoForm" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="upChkTotSaldo" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:CheckBox ID="chkTotSaldo" runat="server" AutoPostBack="true" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblVigenteForm" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:CheckBox ID="chkVigenteForm" runat="server" AutoPostBack="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblProprioTotSaldo" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td colspan="4">
                            <asp:UpdatePanel ID="upChkProprioTotSaldo" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:CheckBox ID="chkProprioTotSaldo" runat="server" Enabled="false" AutoPostBack="true" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                        </table>
                          <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloTotSaldo" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <asp:UpdatePanel ID="upTotSaldo" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:PlaceHolder ID="phTotSaldo" runat="server"></asp:PlaceHolder>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="ui-panel-titlebar" style="margin-top: 20px;">
                            <asp:Label ID="lblTituloDatosBanc" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>

                       <asp:UpdatePanel ID="upDatosBanc" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phDatosBanc" runat="server"></asp:PlaceHolder>
                                </ContentTemplate>
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
                    <asp:Button runat="server" ID="btnAnadirTotalizador" CssClass="btn-novo"/>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnAnadirCuenta" CssClass="btn-novo"/>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnGrabar" CssClass="btn-salvar"/>
                </td>
                <td >
                     <asp:Button runat="server" ID="btnBajaConfirm"  CssClass="btn-excluir"/>
                    <div class="botaoOcultar">
                        <asp:Button runat="server" ID="btnConsomeTotalizador" CssClass="btn-excluir"/>
                       <asp:Button runat="server" ID="btnConsomeCodigoAjeno" CssClass="btn-excluir"/>
                        <asp:Button runat="server" ID="btnBaja"  CssClass="btn-excluir"/>
                        <asp:Button runat="server" ID="btnHabilitaEdicao"  CssClass="btn-excluir"/>
                        <asp:Button runat="server" ID="btnHabilitaConsulta"  CssClass="btn-excluir"/>
                        <asp:Button runat="server" ID="btnHabilitarExclusao" CssClass="ui-button"/>
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