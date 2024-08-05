<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="BusquedaPeriodosAcreditacion.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaPeriodosAcreditacion" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

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
                                    <div style="margin-left: 7px; margin-bottom: 7px; width: 450px">
                                        <div style="display: inline; float: left; width: 120px; margin: 5px 2px 0px 2px;">
                                            <asp:Label ID="lblEstado" runat="server" CssClass="label2"> </asp:Label>
                                        </div>
                                        <div>
                                            <dx:ASPxDropDownEdit CssClass="ui-corner-all" ReadOnly="true" ID="ddlEstadoPeriodo" runat="server" ClientIDMode="Static" ClientInstanceName="ddlEstadoPeriodo" Height="25px" Width="300px" AnimationType="None">
                                                <DropDownWindowStyle BackColor="#EDEDED" />
                                                <DropDownWindowTemplate>
                                                    <dx:ASPxListBox Width="100%" ID="listEstadoPeriodo" ClientIDMode="Static" ClientInstanceName="listEstadoPeriodo" SelectionMode="CheckColumn" runat="server">
                                                        <Border BorderStyle="None" />
                                                        <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                                        <Items></Items>
                                                        <ClientSideEvents SelectedIndexChanged="function(s,e) { OnListBoxSelectionChanged(s,e,ddlEstadoPeriodo, true) }" />
                                                    </dx:ASPxListBox>

                                                    <clientsideevents textchanged="function(s,e) { SynchronizeListBoxValues(s,e,listEstado, true) }"
                                                        dropdown="function(s,e) { SynchronizeListBoxValues(s,e,listEstado, true) }" />
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="padding: 4px">
                                                                <dx:ASPxButton ID="btnEstadosPeriodos" AutoPostBack="False" runat="server" Text="btnEstadosPeriodos" Style="float: right">
                                                                    <ClientSideEvents Click="function(s, e){ ddlEstadoPeriodo.HideDropDown(); }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </DropDownWindowTemplate>
                                            </dx:ASPxDropDownEdit>
                                        </div>
                                    </div>

                                    <div>
                                        <asp:UpdatePanel runat="server" ID="upUcClienteform" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                            <ContentTemplate>
                                                <asp:PlaceHolder runat="server" ID="phClientesForm"></asp:PlaceHolder>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div>
                                        <asp:UpdatePanel ID="updUcPlanificacionUc" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                            <ContentTemplate>
                                                <asp:PlaceHolder runat="server" ID="phPlanificacion"></asp:PlaceHolder>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div>
                                        <asp:UpdatePanel runat="server" ID="upDeviceID" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                            <ContentTemplate>
                                                <asp:PlaceHolder runat="server" ID="phDeviceID"></asp:PlaceHolder>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="dvclear"></div>

                                    <div style="float: left; margin-left: 7px; margin-bottom: 5px;">
                                        <div style="display: inline; float: left; width: 120px; margin: 5px 2px 0px 2px;">
                                            <asp:Label ID="lblFechaPeriodo" runat="server" CssClass="label2"></asp:Label>
                                        </div>
                                    </div>
                                    <div style="float: left; margin-left: 2px; margin-bottom: 5px;">

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
                    <asp:Label ID="lblTituloPeriodos" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <div id="dvExportGrid" runat="server" style="margin-top: 10px; width: auto; background-color: #dcdcdc; border: 1px solid #9f9f9f; border-bottom: 0px; text-align: right;">
                    <div style="height: 22px">
                        <div id="ImgButton1" class="btn" style="margin-right: 5px; padding-right: 0px; padding-left: 0px;">
                            <img alt="" style="height: 16px" class="arrange" src="<%=Page.ResolveUrl("~/Imagenes/config.png")%>" />
                        </div>
                    </div>
                    <dx:ASPxPopupMenu ID="ASPxPopupMenu1" runat="server" ClientInstanceName="ASPxPopupMenuClientControl"
                        PopupElementID="ImgButton1" ShowPopOutImages="True"
                        PopupHorizontalAlign="LeftSides" PopupVerticalAlign="Below" PopupAction="MouseOver">
                        <Items>
                            <dx:MenuItem GroupName="Export" Text="Exportar" Name="Export">
                                <Items>
                                    <dx:MenuItem GroupName="Export" Text="PDF" Name="PDF"></dx:MenuItem>
                                    <dx:MenuItem GroupName="Export" Text="CSV" Name="CSV"></dx:MenuItem>
                                    <dx:MenuItem GroupName="Export" Text="XLS" Name="XLS"></dx:MenuItem>
                                    <dx:MenuItem GroupName="Export" Text="XLSX" Name="XLSX"></dx:MenuItem>
                                </Items>
                            </dx:MenuItem>
                        </Items>
                        <ClientSideEvents ItemClick="function(s, e) {
                                    if('|PDF|CSV|XLSX|'.includes(e.item.name)) {
                                        __doPostBack('ctl00$ContentPlaceHolder1$btnExport',e.item.name)
                                    }
                                }" />
                    </dx:ASPxPopupMenu>
                </div>
                <div style="text-align: center;" id="divGrilla" runat="server">
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server"
                        KeyFieldName="OidPeriodo" Width="100%" AutoGenerateColumns="False"
                        EnableTheming="True"
                        Theme="Office2010Silver">
                        <ClientSideEvents />
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="Banco" VisibleIndex="1">
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Planificacion" VisibleIndex="2">
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="DeviceId" VisibleIndex="3">
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="FyhInicio" VisibleIndex="4">
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="FyhFin" VisibleIndex="5">
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Divisa" VisibleIndex="6">
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="TipoLimite" VisibleIndex="7">
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="LimiteConfigurado" VisibleIndex="8" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ValorActual" VisibleIndex="9" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Estado" VisibleIndex="10" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Acreditado" VisibleIndex="11" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                        <SettingsBehavior AllowSort="False" />
                    </dx:ASPxGridView>
                </div>
                <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid"></dx:ASPxGridViewExporter>
                <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExportt" AutoPostBack="true" runat="server" Visible="false" Text="export" ClientIDMode="Static" UseSubmitBehavior="False" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="cphBotonesRodapie">
    <center>
        <table>
            <tr align="center">
                <td>
                    
                    <asp:Button runat="server" Width="100px" ID="btnDesbloq" CssClass="ui-button" />
                </td>
            </tr>
        </table>
    </center>
    
</asp:Content>
