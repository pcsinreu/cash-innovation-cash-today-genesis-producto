<%@ Page Title="" Language="vb" EnableEventValidation="false" AutoEventWireup="false"
    MasterPageFile="~/Master/MasterModal.Master" CodeBehind="ConfiguracionReport.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.ConfiguracionReport" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master"%>
<%@ Register Src="Controles/ucCheckBoxList.ascx" TagName="ucCheckBoxList" TagPrefix="cbl" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxGridView" Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tabela_campos
        {
            width: 100% !important;
        }
        .botaoOcultar {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function carregando(exibir) {
            var div = document.getElementById("divLoading");
            if (div != null) {
                if (exibir) {
                    div.style.display = 'block';
                }
                else {
                    div.style.display = 'none';
                }
            }
        }

        function abrirCalendar(inputField, format, buttonObj, displayTime, language) {
            //atualiza o valor antes de atualizar pelo calendário.
            inputField.setAttribute("valor-anterior", inputField.value);
            displayCalendar(inputField, format, buttonObj, displayTime, language);
        }

        function atualizarValor(inputField) {
            inputField.setAttribute("valor-anterior", inputField.value);
        }

        function PostBack(inputField) {
            // Verifica se o valor foi alterado.
            //alert("Posback-onfucus:" + inputField.getAttribute("valor-onfocus"));
            //alert("Posback-value:" + inputField.value);
            if (inputField.value != inputField.getAttribute("valor-anterior")) {
                __doPostBack("textBox_" + inputField.id.replace("ctl00_ContentPlaceHolder1_", ""), inputField.value);
            }

            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="content-modal">
    <asp:UpdatePanel ID="upPrincipal" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="botaoOcultar">
                <asp:Button runat="server" ID="btnConfEscogido" />
            </div>
            <!--para não dar erro de objeto nulo.. -->
            <input type="hidden" name="objValorSelecionado_ctl00_ContentPlaceHolder1_P_COM_INVENTARIO_gdvInventario" />
            <div class="ui-panel-titlebar">
                <asp:Label ID="lblTituloConfiguracionReportes" CssClass="ui-panel-title" runat="server"></asp:Label>
            </div>
            <div>
                <div>
                    <asp:HiddenField runat="server" ID="hdReportes" />
                    <dx:ASPxGridView runat="server" ID="gvReportes" AutoGenerateColumns="False" KeyFieldName="OIDConfiguracionGeneral" Width="100%" OnHtmlRowCreated="gvReportes_OnHtmlRowCreated" OnPageIndexChanged="gvReportes_OnPageIndexChanged">
                        <SettingsPager Position="Bottom" Mode="ShowPager">
                            <PageSizeItemSettings Items="10, 20, 50" />
                        </SettingsPager>
                        <Styles>
                            <AlternatingRow CssClass="dxgvDataRow tr-color"></AlternatingRow>
                        </Styles>
                        <SettingsBehavior SortMode="DisplayText" AllowSort="False"></SettingsBehavior>
                        <Columns>
                            <dx:GridViewDataColumn Width="10px">
                                <DataItemTemplate>
                                    <input id="ckSelecionado" type="checkbox" runat="server" class="check_selecao" />
                                </DataItemTemplate>
                                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="DesReporte">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CodReporte">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="FormatoArchivo">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ExtensionArchivo">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Separador">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                </div>
                <div style="margin-top: 15px">
                    <table class="tabela_campos">
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblDescripcion" runat="server" CssClass="label2">Descripción Configuración</asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                    Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblNombreArchivo" runat="server" CssClass="label2">Nombre Archivo</asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtNombreArchivo" runat="server" MaxLength="100" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                    Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblRuta" runat="server" CssClass="label2">Ruta</asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtRuta" runat="server" MaxLength="100" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="ui-panel-titlebar">
                <asp:Label ID="lblFiltros" CssClass="ui-panel-title" runat="server"></asp:Label>
            </div>
            <table>
                <tr>
                    <td align="left">
                        <table runat="server" id="tabelaCampos1">
                        </table>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <table class="tabela_campos" runat="server" id="tabelaCampos">
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <div style="margin-top: 10px;">
                <asp:UpdatePanel ID="UpdatePanelBtnsGrid" runat="server">
                    <ContentTemplate>
                        <table cellpadding="10">
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="btnGenerarInforme" CssClass="btn-visualizar" />
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnLimpiar" CssClass="btn-limpar" />
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnGrabar" CssClass="btn-salvar" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>

