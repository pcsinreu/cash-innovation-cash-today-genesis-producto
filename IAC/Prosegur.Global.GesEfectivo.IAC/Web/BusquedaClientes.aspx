<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="BusquedaClientes.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaClientes" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario.Tradutor" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Búsqueda de Clientes</title>
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
        .styleLabel {
            float: left;
            width: 120px;
            padding-top: 5px;
        }

        .styleLabel2 {
            float: left;
            width: 70px;
            padding-top: 5px;
        }

        .styleLabel3 {
            float: left;
            padding-top: 5px;
        }

        .styleColuna1 {
            float: left;
            width: 231px;
            margin-right: 5px;
        }

        .styleColuna2 {
            float: left;
            width: 250px;
            margin-right: 5px;
        }

        .auto-style1 {
            height: 72px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <asp:HiddenField runat="server" ID="hiddenCodigo" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
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
                                    <td colspan="5" class="tamanho_celula">
                                        <div>
                                            <div>
                                                <div class="styleLabel">
                                                    <asp:Label ID="lblCodCliente" runat="server" CssClass="label2"></asp:Label>
                                                </div>
                                                <div class="styleColuna1">
                                                    <asp:TextBox ID="txtCodCliente" runat="server" MaxLength="15" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="198"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div>
                                                <div class="styleLabel">
                                                    <asp:Label ID="lblDescCliente" runat="server" CssClass="label2"></asp:Label>
                                                </div>
                                                <div class="styleColuna2">
                                                    <asp:TextBox ID="txtDescCliente" runat="server" Width="227px" MaxLength="100" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                                </div>
                                            </div>


                                            <div>
                                                <div class="styleLabel2">
                                                    <asp:Label ID="lblVigente" runat="server" CssClass="label2"></asp:Label>
                                                </div>
                                                <div class="styleLabel2">
                                                    <asp:CheckBox Style="margin-left: 0px !important;" ID="chkVigente" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" class="tamanho_celula">
                                        <div>
                                            <div>
                                                <div class="styleLabel">
                                                    <asp:Label ID="lblTipoCliente" runat="server" CssClass="label2"></asp:Label>
                                                </div>
                                                <div class="styleColuna1">
                                                    <asp:DropDownList ID="ddlTipoCliente" runat="server" Width="208px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>

                                            <div>
                                                <div class="styleLabel">
                                                    <asp:Label ID="lblTotSaldo" runat="server" CssClass="label2"></asp:Label>
                                                </div>
                                                <div class="styleColuna2">

                                                    <asp:DropDownList ID="ddlTipoTotalSaldo" runat="server" Width="237px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>


                                            <div>
                                                <div class="styleLabel2">
                                                    <asp:Label ID="lblTipoBanco" runat="server" CssClass="label2">Tipo Banco</asp:Label>
                                                </div>
                                                <div style="margin-left: 15px;" class="styleColuna2">
                                                    <asp:DropDownList ID="ddlTipoBanco" runat="server" Width="237px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
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

                        </div>
                    </fieldset>
                </div>
                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblSubTitulo" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabelaGrid">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="pnGrid" Visible="False">
                                        <dx:ASPxGridView runat="server" ID="gvClientes" AutoGenerateColumns="False" KeyFieldName="OidCliente" Width="99%" OnHtmlRowCreated="gvClientes_OnHtmlRowCreated" DataSourceForceStandardPaging="True">
                                            <SettingsPager Position="Bottom" Mode="ShowPager">
                                                <PageSizeItemSettings Items="10, 20, 50" />
                                            </SettingsPager>
                                            <Styles>
                                                <AlternatingRow CssClass="dxgvDataRow tr-color"></AlternatingRow>
                                                <RowHotTrack CssClass="GridLinhaDestaque2"></RowHotTrack>
                                            </Styles>
                                            <SettingsBehavior EnableRowHotTrack="True" SortMode="DisplayText" AllowSort="False"></SettingsBehavior>
                                            <ClientSideEvents EndCallback="function(s,e) {s.SetVisible(s.GetVisibleRowsOnPage() > 0);}" />
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
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="DesCliente">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CodTipoCliente">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <DataItemTemplate>
                                                        <asp:Label runat="server" ID="lblDesTipoCliente"></asp:Label>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="DesTipoCliente">
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
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanelGridSemRegistro" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlSemRegistro" runat="server" Visible="false">
                                        <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro">
                                            <tr>
                                                <td style="border-width: 0;"></td>
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
                            <asp:Label ID="lblTituloCliente" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table class="tabela_campos">
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodClienteForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCodigoCliente" runat="server" MaxLength="15" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="200px"></asp:TextBox>
                                            <asp:CustomValidator ID="csvCodClienteObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtCodigoCliente" Text="*"></asp:CustomValidator>
                                            <asp:CustomValidator ID="csvCodClienteExistente" runat="server" ErrorMessage="" ControlToValidate="txtCodigoCliente">*</asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDescClienteForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <table style="margin: 0px !Important">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtDescClienteForm" runat="server" MaxLength="100" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="200px"></asp:TextBox>
                                                        <asp:CustomValidator ID="csvDescClienteObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtDescClienteForm" Text="*"></asp:CustomValidator>
                                                        <asp:CustomValidator ID="csvDescClienteExistente" runat="server" ErrorMessage="" ControlToValidate="txtDescClienteForm">*</asp:CustomValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td align="left">
                                                <asp:Button runat="server" ID="btnDireccion" CssClass="ui-button" Width="120" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigoAjeno" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodigoAjeno" runat="server" Width="200px" MaxLength="25" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="True" ReadOnly="True" />
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDesCodigoAjeno" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <table style="margin: 0px !Important">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDesCodigoAjeno" runat="server" Width="200px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="True" ReadOnly="True" />
                                            </td>
                                            <td>
                                                <asp:Button runat="server" ID="btnAltaAjeno" CssClass="ui-button" Width="150" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">
                                    <asp:Label ID="lblTipoClienteForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td align="left" class="auto-style1">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlTipoClienteForm" runat="server" Width="208px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" AutoPostBack="True">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="csvTipoClienteObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlTipoClienteForm" Text="*"></asp:CustomValidator>
                                        </ContentTemplate>

                                    </asp:UpdatePanel>
                                </td>
                                <td class="auto-style1" colspan="2">


                                    <div>
                                        <div class="styleLabel3">
                                            <asp:Label ID="lblTotSaldoForm" runat="server" CssClass="label2"></asp:Label>
                                        </div>

                                        <div class="styleLabel2">
                                            <asp:UpdatePanel ID="upChkTotSaldo" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="chkTotSaldo" runat="server" AutoPostBack="true" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="styleLabel3">
                                            <asp:Label ID="lblProprioTotSaldo" runat="server" CssClass="label2"></asp:Label>
                                        </div>

                                        <div class="styleLabel2">
                                            <asp:UpdatePanel ID="upChkProprioTotSaldo" runat="server">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="chkProprioTotSaldo" runat="server" Enabled="false" AutoPostBack="true" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="styleLabel3">
                                            <asp:Label ID="lblVigenteForm" runat="server" CssClass="label2"></asp:Label>
                                        </div>

                                        <div class="styleLabel2">
                                            <asp:CheckBox ID="chkVigenteForm" runat="server" AutoPostBack="True" />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div>
                                        <div class="styleLabel3">
                                            <asp:Label ID="lblGrabaSaldoHistorico" runat="server" CssClass="label2"></asp:Label>
                                        </div>
                                        <div class="styleLabel2">
                                            <asp:UpdatePanel ID="upGrabaSaldoHistorico" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="chkGrabaSaldoHistorico" runat="server" AutoPostBack="true" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </td>
                                <div id="divFechaSaldoHistorico" runat="server">
                                    <td>
                                        <div class="styleLabel3">
                                            <asp:Label ID="lblFechaSaldoHistorico" runat="server" CssClass="label2"></asp:Label>
                                        </div>
                                    </td>
                                    <td align="left">
                                        <asp:UpdatePanel ID="upFechaSaldoHistorico" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlFechaSaldoHistorico" runat="server" Width="208px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="true" >
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </div>
                            </tr>
                        </table>
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloBancoForm" CssClass="ui-panel-title" runat="server">Banco</asp:Label>
                        </div>








                        <table class="tabela_campos">
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigoBancarioForm" runat="server" CssClass="label2">Código Bancario</asp:Label>
                                </td>
                                <td class="auto-style1">

                                    <asp:TextBox ID="txtCodigoBancarioForm" runat="server" Width="200px" MaxLength="25" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"  />
                                </td>
                                <td class="auto-style1">
                                    <asp:Label ID="Label3" runat="server" CssClass="label2"></asp:Label>

                                    <div>




                                        <div class="styleLabel" style="text-align: right; width: 154px;">
                                            <asp:Label ID="lblBancoCapitalForm" runat="server" Style="width: 101px;" CssClass="label2">Banco Capital</asp:Label>
                                        </div>

                                        <div class="styleLabel2">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="chkBancoCapitalForm" runat="server" AutoPostBack="true" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="styleLabel">
                                            <asp:Label ID="lblBancoComisionForm" runat="server" CssClass="label2">Banco Comision</asp:Label>
                                        </div>

                                        <div class="styleLabel2">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="chkBancoComisionForm" runat="server" AutoPostBack="true" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>


                                    </div>
                                </td>
                                <td class="tamanho_celula"></td>
                            </tr>

                        </table>

                        <div class="dvclear"></div>
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloFacturacion" CssClass="ui-panel-title" runat="server">Facturacion</asp:Label>
                        </div>

                        

                        <table class="tabela_campos">
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblComisionCliente" runat="server" CssClass="label2">Por. Comisión Cliente</asp:Label>
                                </td>
                                <td class="auto-style1">

                                    <asp:TextBox ID="txtComisionCliente" onkeypress="return bloqueialetrasAceitaVirgulaPunto(event,this);" runat="server" Width="200px" MaxLength="25" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="True"  />
                                </td>                           
                            </tr>

                        </table>

                          <div class="dvclear"></div>
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
                          <asp:Button runat="server" ID="btnAlertaSim" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnAlertaNao" CssClass="btn-excluir" />
                    </div>
                    
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn_cancelar"/>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
