<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="BusquedaAprobacionCuentasBancariasHistorico.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaAprobacionCuentasBancariasHistorico" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario.Tradutor" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%--<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        var textSeparator = ";";
        function SynchronizeListBoxValues(dropDown, args, listBox, seleccionaTodos) {
            if (listBox != null) {

                listBox.UnselectAll();
                var texts = dropDown.GetText().split(textSeparator);
                var values = GetValuesByTexts(texts, listBox);
                listBox.SelectValues(values);
                SynchronizeListBoxValues(listBox, seleccionaTodos);
                UpdateText(listBox, dropDown, seleccionaTodos);
            }
        };
        function OnListBoxSelectionChanged(listBox, args, dropDown, seleccionaTodos) {

            if (args.index == 0 && seleccionaTodos)
                args.isSelected ? listBox.SelectAll() : listBox.UnselectAll();
            UpdateSelectAllItemState(listBox, seleccionaTodos);
            UpdateText(listBox, dropDown, seleccionaTodos);
        };
        function UpdateSelectAllItemState(listBox, seleccionaTodos) {
            if (seleccionaTodos) {
                IsAllSelected(listBox) ? listBox.SelectIndices([0]) : listBox.UnselectIndices([0]);
            }
        }
        function IsAllSelected(listBox) {
            var selectedDataItemCount = listBox.GetItemCount() - (listBox.GetItem(0).selected ? 0 : 1);
            return listBox.GetSelectedItems().length == selectedDataItemCount;
        }

        function UpdateText(listBox, dropDown, seleccionaTodos) {
            var selectedItems = listBox.GetSelectedItems();
            dropDown.SetText(GetSelectedItemsText(selectedItems, seleccionaTodos));
        }
        function GetSelectedItemsText(items, seleccionaTodos) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                if (items[i].index != 0 || !seleccionaTodos)
                    texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function GetValuesByTexts(texts, listBox) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = listBox.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }
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
                                <asp:Label ID="lblSubTitulosCriteriosBusqueda" CssClass="legent-text" runat="server">
                                </asp:Label>
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
                                    <div>
                                        <asp:UpdatePanel ID="updUcUsuarioUcModificacion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                            <ContentTemplate>
                                                <asp:PlaceHolder runat="server" ID="phUsuarioModificacion"></asp:PlaceHolder>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div>
                                        <asp:UpdatePanel ID="updUcUsuarioUcAprobacion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                            <ContentTemplate>
                                                <asp:PlaceHolder runat="server" ID="phUsuarioAprobacion"></asp:PlaceHolder>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="dvclear"></div>
                                    <div style="float: left; margin-left: 7px; margin-bottom: 7px; width: 450px">
                                        <div style="display: inline; float: left; width: 120px; margin: 5px 2px 0px 2px;">
                                            <asp:Label ID="lblEstado" runat="server" CssClass="label2">
                                            </asp:Label>
                                        </div>
                                        <dx:ASPxDropDownEdit CssClass="ui-corner-all" ReadOnly="true" ID="ddlStado" runat="server" ClientIDMode="Static" ClientInstanceName="ddlEstado" Height="25px" Width="300px" AnimationType="None">
                                            <DropDownWindowStyle BackColor="#EDEDED" />
                                            <DropDownWindowTemplate>
                                                <dx:ASPxListBox Width="100%" ID="listStado" ClientIDMode="Static" ClientInstanceName="listEstado" SelectionMode="CheckColumn" runat="server">
                                                    <Border BorderStyle="None" />
                                                    <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                                    <Items></Items>
                                                    <ClientSideEvents SelectedIndexChanged="function(s,e) { OnListBoxSelectionChanged(s,e,ddlEstado, true) }" />
                                                </dx:ASPxListBox>
                                                <clientsideevents textchanged="function(s,e) { SynchronizeListBoxValues(s,e,listEstado, true) }"
                                                    dropdown="function(s,e) { SynchronizeListBoxValues(s,e,listEstado, true) }" />
                                            </DropDownWindowTemplate>
                                        </dx:ASPxDropDownEdit>
                                    </div>
                                    <div style="float: left; margin-left: 7px; margin-bottom: 12px; width: 450px">
                                        <div style="display: inline; float: left; width: 120px; margin: 5px 2px 0px 2px;">
                                            <asp:Label ID="lblCampo" runat="server" CssClass="label2">
                                            </asp:Label>
                                        </div>
                                        <dx:ASPxDropDownEdit CssClass="ui-corner-all" ReadOnly="true" ID="ddlCampos" runat="server" ClientIDMode="Static" ClientInstanceName="ddlCampos" Height="25px" Width="300px" AnimationType="None">
                                            <DropDownWindowStyle BackColor="#EDEDED" />
                                            <DropDownWindowTemplate>
                                                <dx:ASPxListBox Width="100%" ID="listCampos" ClientIDMode="Static" ClientInstanceName="listCampos" SelectionMode="CheckColumn" runat="server">
                                                    <Border BorderStyle="None" />
                                                    <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                                    <Items></Items>
                                                    <ClientSideEvents SelectedIndexChanged="function(s,e) { OnListBoxSelectionChanged(s,e,ddlCampos, true) }" />
                                                </dx:ASPxListBox>
                                                <clientsideevents textchanged="function(s,e) { SynchronizeListBoxValues(s,e,listCampos, true) }"
                                                    dropdown="function(s,e) { SynchronizeListBoxValues(s,e,listCampos, true) }" />
                                            </DropDownWindowTemplate>
                                        </dx:ASPxDropDownEdit>
                                    </div>
                                    <div class="dvclear"></div>
                                    <div style="float: left; margin-left: 7px; margin-bottom: 5px; width: 450px">
                                        <div style="display: inline; float: left; width: 120px; margin: 5px 2px 0px 2px;">
                                            <asp:Label ID="lblFechaDe" runat="server" CssClass="label2">
                                            </asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlTipoFecha" runat="server" CssClass="ui-corner-all" ReadOnly="true" Height="25px" Width="300px">
                                        </asp:DropDownList>
                                    </div>

                                    <div style="float: left; margin-left: 7px; margin-bottom: 5px;">
                                        <div style="display: inline; float: left; width: 60px; margin: 5px 2px 0px 2px;">
                                            <asp:Label ID="lblDesde" runat="server" CssClass="label2"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtFechaDesde" runat="server"
                                            CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                            Width="195"></asp:TextBox>
                                    </div>
                                    <div style="float: left; margin-left: 20px; margin-bottom: 5px;">
                                        <div style="display: inline; float: left; width: 60px; margin: 5px 2px 0px 2px;">
                                            <asp:Label ID="lblHasta" runat="server" CssClass="label2"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtFechaHasta" runat="server"
                                            CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                            Width="195"></asp:TextBox>
                                    </div>

                                    <div class="dvclear"></div>
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
                    <asp:Label ID="lblTituloHistorialCuentaBancaria" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <div style="text-align: center;" id="divGrilla" runat="server">
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server"
                        KeyFieldName="OidDatoBancario" Width="100%" AutoGenerateColumns="False"
                        EnableTheming="True"
                        Theme="Office2010Silver">
                        <ClientSideEvents />
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
                            <dx:GridViewDataTextColumn FieldName="CantAprobados" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CantPendientes" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CantRechazados" VisibleIndex="7" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                        <Templates>
                            <DetailRow>
                                <br />
                                <dx:ASPxGridView ID="detailGrid" ClientInstanceName="detailGrid" runat="server"
                                    DataSourceID="gridDetailDataSource"
                                    KeyFieldName="OidDatoBancarioCambio"
                                    Width="95%"
                                    AutoGenerateColumns="False"
                                    OnBeforePerformDataSelect="detailGrid_DataSelect"
                                    OnInit="detailGrid_OnInit"
                                    EnableTheming="True"
                                    Theme="Office2010Silver">
                                    <Columns>
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
                                            Width="130px"
                                            HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Center"
                                            VisibleIndex="5">
                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm:ss">
                                            </PropertiesDateEdit>
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataColumn Width="90px" FieldName="Comentario" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:ImageButton
                                                    ID="imgComentario"
                                                    runat="server"
                                                    ImageUrl="~/Imagenes/comentario_icon.png"
                                                    Width="16px"
                                                    CssClass="imgButton"
                                                    OnClientClick='<%# BuscaPostbackComentario(Container.KeyValue) %>'
                                                    CommandArgument="<%# Container.KeyValue %>" />
                                            </DataItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Width="80px" FieldName="Estado" VisibleIndex="7" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:Image runat="server" ImageUrl="~/Imagenes/contain01.png" ID="imgEstadoAprobado" CssClass="imgButton" Visible='<%# If(Eval("Estado").ToString() = "AP", True, False) %>' />
                                                <asp:Image runat="server" ImageUrl="~/Imagenes/contain_pending.png" ID="imgEstadoPendiente" CssClass="imgButton" Visible='<%# If(Eval("Estado").ToString() = "PD", True, False) %>' />
                                                <asp:Image runat="server" ImageUrl="~/Imagenes/nocontain01.png" ID="imgEstadoRechazado" CssClass="imgButton" Visible='<%# If(Eval("Estado").ToString() = "RE", True, False) %>' />
                                            </DataItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="CantidadAprobaciones" VisibleIndex="7" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <div style="display: flex; align-items: center; justify-content: center;">
                                                    <div style="margin-right: 4px">
                                                        <%# Eval("CantidadAprobaciones") %>/<%# Eval("AprobacionesNecesarias") %>
                                                    </div>
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
                                    <SettingsPager PageSize="10">
                                        <PageSizeItemSettings Visible="True" />
                                    </SettingsPager>
                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                </dx:ASPxGridView>
                            </DetailRow>
                        </Templates>
                        <SettingsPager PageSize="20">
                            <PageSizeItemSettings Visible="True" />
                        </SettingsPager>
                        <SettingsBehavior AllowSort="False" />
                        <SettingsDetail ShowDetailRow="true" />
                    </dx:ASPxGridView>

                    <asp:ObjectDataSource ID="gridDetailDataSource" runat="server"
                        SelectMethod="GetGrillaDetalle"
                        TypeName="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaAprobacionCuentasBancariasHistorico"></asp:ObjectDataSource>
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
    <div class="botaoOcultar">

        <dx:ASPxButton ID="btnComentario" ClientInstanceName="btnComentario" AutoPostBack="true" runat="server" Visible="false" Text="btnComentario" ClientIDMode="Static" UseSubmitBehavior="False" />

    </div>
</asp:Content>
