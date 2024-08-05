<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="BusquedaAprobacionCuentasBancarias.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaAprobacionCuentasBancarias" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario.Tradutor" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%--<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <script type="text/javascript">
        function grid_SelectionChanged(s, e) {
            var cantidad = detailGrid.GetSelectedRowCount();
            if (cantidad > 0) {
                document.getElementById("btnAprobar").disabled = false;
                document.getElementById("btnRechazar").disabled = false;
            }
            else {
                document.getElementById("btnAprobar").disabled = true;
                document.getElementById("btnRechazar").disabled = true;
            }
        }
        function deshabilitarBotones(s, e) {

            document.getElementById("btnAprobar").disabled = true;
            document.getElementById("btnRechazar").disabled = true;
        }
        function ocultarExibir() {
            $("#DivFiltros").toggleClass("legend-expand");
            $(".accordion").slideToggle("fast");
        };
        function ManterFiltroAberto() {
            $("#DivFiltros").addClass("legend-expand");
            $(".accordion").show();
        };
        function ClickComentario() {

            __doPostBack('<%=btnAprobar.UniqueID %>');
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="updForm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
        <ContentTemplate>
            <div class="content">
                <div id="Filtros" style="display: block;">
                    <fieldset id="ExpandCollapseDiv" class="ui-fieldset ui-widget ui-widget-content ui-corner-all ui-fieldset-toggleable">
                        <legend class="ui-fieldset-legend ui-corner-all ui-state-default ui-state-active">
                            <div id="DivFiltros" class="legend-collapse" onclick="ocultarExibir();">
                                <asp:Label ID="lblSubTitulosCriteriosBusqueda" CssClass="legent-text" runat="server"></asp:Label>
                            </div>
                        </legend>
                        <div class="accordion" style="display: block;">
                            <asp:UpdatePanel ID="uppDelPLa" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div>
                                        <asp:UpdatePanel ID="updUcClienteUc" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                            <ContentTemplate>
                                                <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
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
                    <asp:Label ID="lblTituloAprobacionCuentaBancaria" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <div style="text-align: center;">
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server"
                        KeyFieldName="OidDatoBancario" Width="100%" AutoGenerateColumns="False"
                        EnableTheming="True"
                        Theme="Office2010Silver">
                        <ClientSideEvents DetailRowExpanding="deshabilitarBotones" DetailRowCollapsing="deshabilitarBotones" />
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="Cliente" VisibleIndex="1">
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="SubCliente" VisibleIndex="2">
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="PuntoServicio" VisibleIndex="3">
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Divisa" VisibleIndex="4">
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="NumeroCampos" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataColumn VisibleIndex="6" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:ImageButton
                                        runat="server"
                                        ImageUrl="~/Imagenes/institution.png"
                                        CommandName="PopupComparativo"
                                        CommandArgument="<%# Container.KeyValue %>"
                                        ID="imgComparativo"
                                        CssClass="imgButton" />
                                </DataItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataColumn>

                        </Columns>
                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                        <Templates>
                            <DetailRow>
                                <br />
                                <dx:ASPxGridView ID="detailGrid" ClientInstanceName="detailGrid" runat="server"
                                    DataSourceID="gridDetailDataSource"
                                    KeyFieldName="OidDatoBancarioCambio"
                                    Width="90%"
                                    AutoGenerateColumns="False"
                                    OnBeforePerformDataSelect="detailGrid_DataSelect"
                                    OnInit="detailGrid_OnInit"
                                    EnableTheming="True"
                                    Theme="Office2010Silver">
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                            <HeaderTemplate>
                                                <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server"
                                                    ClientSideEvents-CheckedChanged="function(s, e) { detailGrid.SelectAllRowsOnPage(s.GetChecked()); }" />
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataColumn FieldName="CampoModificado" CellStyle-HorizontalAlign="Left" VisibleIndex="1">
                                            <CellStyle HorizontalAlign="Left">
                                            </CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="ValorActual" CellStyle-HorizontalAlign="Left" VisibleIndex="2">
                                            <CellStyle HorizontalAlign="Left">
                                            </CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="ValorModificado" CellStyle-HorizontalAlign="Left" VisibleIndex="3">
                                            <CellStyle HorizontalAlign="Left">
                                            </CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="UsuarioModificacion" VisibleIndex="4">
                                            <CellStyle HorizontalAlign="Left" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataDateColumn FieldName="FechaCambio"
                                            HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Center"
                                            VisibleIndex="5">
                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm:ss">
                                            </PropertiesDateEdit>
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataColumn FieldName="Comentario" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:ImageButton
                                                    ID="imgComentario"
                                                    runat="server"
                                                    ImageUrl="~/Imagenes/comentario_icon.png"
                                                    Width="16px"
                                                    CssClass="imgButton"
                                                    OnClientClick='<%# BuscaPostbackComentario(Container.KeyValue) %>'
                                                    CommandName="PopupComentarioAprobacion"
                                                    CommandArgument="<%# Container.KeyValue %>" />
                                            </DataItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="CantidadAprobaciones" VisibleIndex="7" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <div style="display: flex; align-items: center; justify-content: center;">
                                                    <div style="margin-right: 4px">
                                                        <%# Eval("CantidadAprobaciones") %>/<%# Eval("AprobacionesNecesarias") %></div>
                                                    <div>
                                                        <asp:Image
                                                            ID="imgInformacion"
                                                            runat="server"
                                                            ImageUrl="~/Imagenes/info_icon.png"
                                                            Width="16px"
                                                            CssClass="imgButton"
                                                            ToolTip='<%# Eval("Aprobadores") %>' />
                                                    </div>
                                                </div>
                                            </DataItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                    <SettingsPager PageSize="20">
                                        <PageSizeItemSettings Visible="false" ShowAllItem="true" />
                                    </SettingsPager>
                                    <ClientSideEvents SelectionChanged="grid_SelectionChanged" />
                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                </dx:ASPxGridView>
                            </DetailRow>
                        </Templates>
                        <SettingsPager PageSize="20"></SettingsPager>
                        <SettingsBehavior AllowSort="False" />
                        <SettingsDetail ShowDetailRow="true" />
                    </dx:ASPxGridView>

                    <asp:ObjectDataSource ID="gridDetailDataSource" runat="server"
                        SelectMethod="GetGrillaDetalle"
                        TypeName="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaAprobacionCuentasBancarias"></asp:ObjectDataSource>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
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
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <center>
        <table>
            <tr align="center">
                <td>
                    <asp:Button runat="server" ID="btnAprobar" Width="100px" ClientIDMode="Static" CssClass="ui-button" />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnRechazar" Width="100px" ClientIDMode="Static" CssClass="ui-button" />
                </td>
                <td>
                                           
                    <div class="botaoOcultar">

                        <asp:Button runat="server" ID="btnAlertaSi" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnAlertaNo" CssClass="btn-excluir" />
                    <dx:ASPxButton ID="btnComentario" ClientInstanceName="btnComentario" AutoPostBack="true" runat="server" Visible="false" Text="btnComentario" ClientIDMode="Static" UseSubmitBehavior="False" />

                    </div>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
