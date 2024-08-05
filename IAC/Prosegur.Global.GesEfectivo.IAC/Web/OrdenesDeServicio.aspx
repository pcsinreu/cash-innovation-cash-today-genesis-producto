<%@ Page Title="Ordenes de Servicio" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="OrdenesDeServicio.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.OrdenesDeServicio" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario.Tradutor" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>


<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">
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
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upFiltrosBusqueda" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
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

                                    <div class="dvclear"></div>
                                    <div style="float: left; margin-left: 7px; margin-bottom: 5px;">
                                        <div style="display: inline; float: left; width: 60px; margin: 5px 2px 0px 2px;">
                                            <asp:Label ID="lblContrato" runat="server" CssClass="label2"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtContrato" runat="server"
                                            CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                            Width="195" MaxLength="255" style="margin-left:60px"></asp:TextBox>
                                    </div>
                                    <div style="float: left; margin-left: 44px; margin-bottom: 5px;">
                                        <div style="display: inline; float: left; width: 60px; margin: 5px 2px 0px 2px;">
                                            <asp:Label ID="lblOS" runat="server" CssClass="label2"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtOS" runat="server"
                                            CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                            Width="195" MaxLength="255"></asp:TextBox>
                                    </div>
                                    <div style="float: left; margin-left: 31px; margin-bottom: 12px; width: 450px">
                                        <div style="display: inline; float: left; width: 120px; margin: 5px 2px 0px 2px;">
                                            <asp:Label ID="lblProducto" runat="server" CssClass="label2">
                                            </asp:Label>
                                        </div>
                                        <dx:ASPxDropDownEdit CssClass="ui-corner-all" ReadOnly="true" ID="ddlProductos" runat="server" ClientIDMode="Static" ClientInstanceName="ddlProductos" Height="25px" Width="300px" AnimationType="None">
                                            <DropDownWindowStyle BackColor="#EDEDED" />
                                            <DropDownWindowTemplate>
                                                <dx:ASPxListBox Width="100%" ID="listProductos" ClientIDMode="Static" ClientInstanceName="listProductos" SelectionMode="CheckColumn" runat="server">
                                                    <Border BorderStyle="None" />
                                                    <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                                    <Items></Items>
                                                    <ClientSideEvents SelectedIndexChanged="function(s,e) { OnListBoxSelectionChanged(s,e,ddlProductos, false) }" />
                                                </dx:ASPxListBox>
                                                <clientsideevents textchanged="function(s,e) { SynchronizeListBoxValues(s,e,listProductos, false) }"
                                                    dropdown="function(s,e) { SynchronizeListBoxValues(s,e,listProductos, false) }" />
                                            </DropDownWindowTemplate>
                                        </dx:ASPxDropDownEdit>
                                    </div>
                                    <div class="dvclear"></div>

                                    <div style="float: left; margin-left: 7px; margin-bottom: 5px;">
                                        <div style="display: inline; float: left; width: 60px; margin: 5px 2px 0px 2px;">
                                            <asp:Label ID="lblInicio" runat="server" CssClass="label2"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtInicio" runat="server"
                                            CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                            Width="195" style="margin-left:60px"></asp:TextBox>
                                    </div>
                                    <div style="float: left; margin-left: 20px; margin-bottom: 5px;">
                                        <div style="display: inline; float: left; width: 60px; margin: 5px 2px 0px 2px;">
                                            <asp:Label ID="lblFin" runat="server" CssClass="label2"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtFin" runat="server"
                                            CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                            Width="195"></asp:TextBox>
                                    </div>
                                    <div style="float: left; margin-left: 7px; margin-bottom: 7px; width: 450px">
                                        <div style="display: inline; float: left; width: 120px; margin: 5px 2px 0px 2px;">
                                            <asp:Label ID="lblEstado" runat="server" CssClass="label2">
                                            </asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="ui-corner-all" ReadOnly="true" Height="25px" Width="300px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="ddlEstado"
                                        InitialValue="" runat="server" ForeColor="Red" />
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
                <asp:Panel runat="server" ID="PanelGrilla">
                <div class="ui-panel-titlebar" style="margin-top:10px">
                    <asp:Label ID="lblTituloOrdenesServicio" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <div style="text-align: center;" id="divGrilla" runat="server" visible="false">
                    <dx:ASPxGridView ID="gridOrdenesServicio" ClientInstanceName="gridOrdenesServicio" runat="server"
                        KeyFieldName="OidAcuerdoServicio" Width="100%" AutoGenerateColumns="false"
                        EnableTheming="True" OnPageIndexChanged="gridOrdenesServicio_PageIndexChanged"
                        Theme="Office2010Silver">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center">
                                     <DataItemTemplate>
                                                <asp:ImageButton
                                                    ID="imgOrdenServicio"
                                                    runat="server"
                                                    ImageUrl="App_Themes/Padrao/css/img/button/buscar.png"
                                                    Width="16px"
                                                    CssClass="imgButton"                                                    
                                                    CommandArgument="<%# Container.KeyValue %>"/>
                                            </DataItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="FechaReferencia" VisibleIndex="2">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">  
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="FechaCalculo" VisibleIndex="3" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Cliente" VisibleIndex="4">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="SubCliente" VisibleIndex="5">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="PuntoServicio" VisibleIndex="6">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Contrato" VisibleIndex="7">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="OrdenServicio" VisibleIndex="8" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Producto" VisibleIndex="9" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Estado" VisibleIndex="10" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="OidSaldoAcuerdoRef" VisibleIndex="11" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn FieldName="CodigoProducto" VisibleIndex="12" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsLoadingPanel Mode="Disabled" />
                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                        <SettingsPager PageSize="20">
                            <PageSizeItemSettings Visible="True" />
                        </SettingsPager>
                        <SettingsBehavior AllowSort="False" AllowSelectSingleRowOnly="True" ProcessFocusedRowChangedOnServer="True" ProcessSelectionChangedOnServer="True" AllowFocusedRow="True" />
                    </dx:ASPxGridView>

                    </div>
                    </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="upOrdenesServicio" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="content">
                <asp:Panel runat="server" ID="panelOrdenesServicio" Visible="false" style="margin-top:10px">

                    <div class="ui-panel-titlebar">
                        <asp:Label ID="lblTituloForm" CssClass="ui-panel-title" runat="server"></asp:Label>
                    </div>

                    <table class="tabela_campos">
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblClienteDet" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <div style="position: relative">
                                    <asp:TextBox ID="txtClienteDet" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="215" ReadOnly="true"></asp:TextBox>
                                </div>
                            </td>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblSubClienteDet" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSubClienteDet" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="215" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblPuntoDet" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPuntoServicioDet" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="215px" ReadOnly="true"
                                    MaxLength="6" ClientIDMode="Static"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblContratoDet" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtContratoDet" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="215px" ReadOnly="true"
                                    MaxLength="15"></asp:TextBox>
                            </td>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblOSDet" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOrdenServicioDet" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="215px" ReadOnly="true"
                                    MaxLength="100"></asp:TextBox>
                            </td>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblProductoDet" runat="server" CssClass="label2" nowrap="false"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProductoDet" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="215px" ReadOnly="true"
                                    MaxLength="6" ClientIDMode="Static"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblFechaReferenciaDet" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <div style="position: relative">
                                    <asp:TextBox ID="txtFechaReferenciaDet" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="215" ReadOnly="true"></asp:TextBox>
                                </div>
                            </td>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblFechaCalculoDet" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaCalculoDet" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="215" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblEstadoDet" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEstadoDet" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="215px" ReadOnly="true"
                                    MaxLength="6" ClientIDMode="Static"></asp:TextBox>
                            </td>
                        </tr>

                    </table>
                    <%--Detalles--%>
                    <br />
<%--                    <asp:UpdatePanel ID="UpdatePanelDetalles" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" Visible="false">
                        <ContentTemplate>--%>

                            <div class="ui-panel-titlebar">
                                <asp:Label ID="lblSubtituloDetalles" CssClass="ui-panel-title" runat="server"></asp:Label>
                            </div>
                            <table class="tabela_interna" style="margin-top: 10px;">
                                <tr>
                                    <td align="center">
                                        <div style="text-align: center;">
                                            <dx:ASPxGridView ID="gridDetalles" runat="server"
                                                KeyFieldName="OidMaquina" Width="99%" AutoGenerateColumns="False"
                                                EnableTheming="True"
                                                Theme="Office2010Silver">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="Tipo" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                                        <Settings AllowDragDrop="False" />
                                                        <CellStyle HorizontalAlign="Left">
                                                        </CellStyle>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="Cantidad" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center">
                                                        <Settings AllowDragDrop="False" />
                                                        <CellStyle HorizontalAlign="Left">
                                                        </CellStyle>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="Divisa" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center">
                                                        <Settings AllowDragDrop="False" />
                                                        <CellStyle HorizontalAlign="Left">
                                                        </CellStyle>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="TipoMercancia" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center">
                                                        <Settings AllowDragDrop="False" />
                                                        <CellStyle HorizontalAlign="Left">
                                                        </CellStyle>
                                                    </dx:GridViewDataTextColumn>

                                                    <dx:GridViewDataTextColumn FieldName="Total" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center">
                                                    </dx:GridViewDataTextColumn>

                                                </Columns>
                                                <Settings ShowFilterRow="False" ShowFilterRowMenu="false" />
                                                <SettingsPager PageSize="20" Mode="ShowAllRecords" >
                                                    <PageSizeItemSettings Visible="false" />
                                                </SettingsPager>
                                                <SettingsBehavior AllowSort="False"/>
                                            </dx:ASPxGridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
<%--                        </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    <%--Notificaciones--%>
                    <br />
<%--                    <asp:UpdatePanel ID="UpdatePanelNotificaciones" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True" Visible="true">
                        <ContentTemplate>--%>
                            <div class="ui-panel-titlebar">
                                <asp:Label ID="lblSubtituloNotificaciones" CssClass="ui-panel-title" runat="server"></asp:Label>
                            </div>
                            <table class="tabela_interna" style="margin-top: 10px;">
                                <tr>
                                    <td align="center">
                                        <div style="text-align: center;">
                                            <dx:ASPxGridView ID="gridNotificaciones" runat="server"
                                                KeyFieldName="OidIntegracion" Width="99%" AutoGenerateColumns="False"
                                                EnableTheming="True"
                                                Theme="Office2010Silver">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="Fecha" VisibleIndex="0">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <CellStyle HorizontalAlign="Left">
                                                        </CellStyle>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="OidIntegracion" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center">
                                                        <Settings AllowDragDrop="False" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <CellStyle HorizontalAlign="Left">
                                                        </CellStyle>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="Estado" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center">
                                                        <Settings AllowDragDrop="False" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <CellStyle HorizontalAlign="Left">
                                                        </CellStyle>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="Intentos" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center">
                                                        <Settings AllowDragDrop="False" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <CellStyle HorizontalAlign="Left">
                                                        </CellStyle>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="UltimoError" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center">
                                                        <Settings AllowDragDrop="False" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <CellStyle HorizontalAlign="Left">
                                                        </CellStyle>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataColumn FieldName="" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center">
                                                        <DataItemTemplate>
                                                            <asp:ImageButton
                                                                ID="imgVisualizar"
                                                                runat="server"
                                                                ImageUrl="App_Themes/Padrao/css/img/button/buscar.png"
                                                                Width="16px"
                                                                CssClass="imgButton" 
                                                                OnClick ="imgVisualizar_Click"
                                                                CommandArgument="<%# Container.KeyValue %>"/>
                                                        </DataItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <CellStyle HorizontalAlign="Center">
                                                        </CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center">
                                                        <DataItemTemplate>
                                                            <asp:ImageButton
                                                                ID="imgDetalles"
                                                                runat="server"
                                                                ImageUrl="App_Themes/Padrao/css/img/button/calendar.png"
                                                                Width="16px"
                                                                CssClass="imgButton"  
                                                                OnClick="imgDetalles_Click"
                                                                CommandArgument="<%# Container.KeyValue %>"/>
                                                        </DataItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <CellStyle HorizontalAlign="Center">
                                                        </CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataTextColumn FieldName="OidSaldoAcuerdoRef" VisibleIndex="7" Visible="false">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <CellStyle HorizontalAlign="Left">
                                                        </CellStyle>
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <Settings ShowFilterRow="False" ShowFilterRowMenu="false" />
                                                <SettingsPager PageSize="20" Mode="ShowAllRecords" >
                                                    <PageSizeItemSettings Visible="false" />
                                                </SettingsPager>
                                                <SettingsBehavior AllowSort="False" AllowFocusedRow="True" AllowSelectSingleRowOnly="True"/>
                                            </dx:ASPxGridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
<%--                        </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
   <%-- <script type="text/javascript">
        //Script necessário para evitar que dê erro ao clicar duas vezes em algum controle que esteja dentro do updatepanel.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(initializeRequest);

        function initializeRequest(sender, args) {
            if (prm.get_isInAsyncPostBack()) {
                args.set_cancel(true);
            }
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <center>
        <table>
            <tr align="center">
                <td>
                    <asp:Button runat="server" ID="btnRecalcular" Width="100px" ClientIDMode="Static" CssClass="ui-button"/>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnNotificar" Width="100px" ClientIDMode="Static" CssClass="ui-button"/>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnVisualizar" Width="100px" ClientIDMode="Static" CssClass="ui-button"/>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
