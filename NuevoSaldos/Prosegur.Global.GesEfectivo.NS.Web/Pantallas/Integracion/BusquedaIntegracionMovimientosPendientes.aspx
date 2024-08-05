<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaIntegracionMovimientosPendientes.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.BusquedaIntegracionMovimientosPendientes" MasterPageFile="~/Master/Master.Master" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>

<%@ Register Src="~/Controles/Boton.ascx" TagName="Boton" TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .dxeListBoxItem {
            color: black !important;
            background-color: white !important;
        }

        .dxeEditArea {
            height: 10px !important;
            max-height: 10px !important;
            font-size: 12px !important;
        }

        .dxEditors_edtDropDownDisabled {
            max-height: 10px !important;
        }

        .dxEditors_edtDropDown {
            max-height: 10px !important;
        }

        .dxlbd {
            width: 262px !important;
        }

        .dxWeb_mSubMenuItemChecked {
            visibility: hidden !important;
        }

        .CssStyleClick {
            cursor: pointer;
            white-space: nowrap !important;
        }

            .CssStyleClick:hover {
                cursor: pointer;
                text-decoration: underline;
                white-space: nowrap !important;
            }

        .CssStyleNotClick {
            cursor: not-allowed;
            white-space: nowrap !important;
        }


        .CorHeader {
            background-color: red !important;
            color: green !important;
        }

        .dxpgColumnFieldValue {
            background-color: #ededed !important;
        }

        .dx-pivotgrid .dx-word-wrap .dx-pivotgrid-collapsed > span, .dx-pivotgrid .dx-word-wrap .dx-pivotgrid-expanded > span, .dx-pivotgrid .dx-word-wrap .dx-pivotgrid-sorted > span {
            white-space: nowrap !important;
        }

        .imgConfig {
            background-image: url("~/Imagenes/config.png");
        }
    </style>

    <script type="text/javascript">
        // <![CDATA[
        var textSeparator = ";";

        function OnInit(s, e) {
            //LoadingPanel.Hide();
        }

        function OnListBoxSelectionChanged(listBox, args, dropDown, seleccionaTodos) {

            if (args.index == 0 && seleccionaTodos)
                args.isSelected ? listBox.SelectAll() : listBox.UnselectAll();
            UpdateSelectAllItemState(listBox, seleccionaTodos);
            UpdateText(listBox, dropDown, seleccionaTodos);
        }
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
        function SynchronizeListBoxValues(dropDown, args, listBox, seleccionaTodos) {
            if (listBox != null) {

                listBox.UnselectAll();
                var texts = dropDown.GetText().split(textSeparator);
                var values = GetValuesByTexts(texts, listBox);
                listBox.SelectValues(values);
                SynchronizeListBoxValues(listBox, seleccionaTodos);
                UpdateText(listBox, dropDown, seleccionaTodos);
            }
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
        function ShipOutChanged() {
            if (document.getElementsByName('dvFechaGestion')[0].style.display == 'none') {
                document.getElementsByName('dvFecha')[0].style.display = 'none';
                document.getElementsByName('dvFechaDesde')[0].style.display = 'none';
                document.getElementsByName('dvFechaHasta')[0].style.display = 'none';
                document.getElementsByName('dvFechaGestion')[0].style.display = 'block';
            } else {
                document.getElementsByName('dvFecha')[0].style.display = 'block';
                document.getElementsByName('dvFechaDesde')[0].style.display = 'block';
                document.getElementsByName('dvFechaHasta')[0].style.display = 'block';
                document.getElementsByName('dvFechaGestion')[0].style.display = 'none';
            }
        }


        function InitPopupMenuHandler(s, e) {
            var imgButton = document.getElementById('ImgButton1');
        }

        function button1_Click(s, e) {
            if (grid.IsCustomizationWindowVisible())
                grid.HideCustomizationWindow();
            else
                grid.ShowCustomizationWindow();
            UpdateButtonText();
        }
        function grid_CustomizationWindowCloseUp(s, e) {
            UpdateButtonText();
        }
        function UpdateButtonText() {
            var text = grid.IsCustomizationWindowVisible() ? "Ocultar" : "Mostrar";
            text += " Seletor de Columnas";
            button1.SetText(text);
        }

        function seletorColunas() {

            if (document.getElementsByClassName("dxpc-headerText").length > 0) {
                document.getElementsByClassName("dxpc-headerText")[0].textContent = document.getElementById("hfTitleCustomization").value;
            }
        }

        // <![CDATA[
        function grid_SelectionChanged(s, e) {
            s.GetSelectedFieldValues("ActualID", GetSelectedFieldValuesCallback);
        }
        function GetSelectedFieldValuesCallback(values) {
            selList.BeginUpdate();
            try {
                selList.ClearItems();
                for (var i = 0; i < values.length; i++) {
                    selList.AddItem(values[i]);
                }
            } finally {
                selList.EndUpdate();
            }
            document.getElementById("selCount").innerHTML = grid.GetSelectedRowCount();
        }
      // ]]>


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <asp:UpdatePanel ID="UpTodo" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder ID="phUcPopUp" runat="server"></asp:PlaceHolder>

                <div id="dvTituloFiltro" runat="server" class="ui-panel-titlebar" style="display: none; margin-bottom: 2px; padding-left: 20px;">
                    <asp:Label ID="lblFiltros" runat="server" Text="Saldos" Style="color: #767676 !important; font-size: 9pt;" />
                </div>
                <div id="dvFiltros" runat="server" style="display: none; margin-left: 10px; margin-top: 5px;">
                    <div style="display: block;">
                        <table id="tablaFiltro">
                            <tr>
                                <td>
                                    <div class="tamanho_celula" style="padding-top: 5px;">
                                        <asp:Label ID="lblTipoCodigo" Text="Tipo código" runat="server"></asp:Label>
                                    </div>
                                    <asp:DropDownList Style="" ID="ddlTipoCodigo" runat="server" Height="22px" Width="291px"></asp:DropDownList>
                                </td>
                                <td style="padding-left: 10px">
                                    <div class="tamanho_celula" style="padding-top: 5px;">
                                        <asp:Label ID="lblCodigo" Text="Código" runat="server"></asp:Label>
                                    </div>
                                    <asp:TextBox ID="txtCodigo" Text="" autocomplete="txtCodigo" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="291px" MaxLength="60"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="tamanho_celula" style="padding-top: 5px;">
                                        <asp:Label ID="lblTipoError" Text="Tipo error" runat="server"></asp:Label>
                                    </div>
                                    <dx:ASPxDropDownEdit ID="ddlTipoError" runat="server" ClientInstanceName="ddlTipoError" AutoPostBack="true" Height="22px" Width="291px">
                                        <DropDownWindowStyle BackColor="#EDEDED" />
                                        <DropDownWindowTemplate>
                                            <dx:ASPxListBox Width="100%" ID="listTipoError" ClientInstanceName="listTipoError" SelectionMode="CheckColumn" runat="server">
                                                <Border BorderStyle="None" />
                                                <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                                <Items></Items>
                                                <ClientSideEvents SelectedIndexChanged="function(s,e) { OnListBoxSelectionChanged(s,e,ddlTipoError, true) }" />
                                            </dx:ASPxListBox>

                                        </DropDownWindowTemplate>
                                        <ClientSideEvents TextChanged="function(s,e) { SynchronizeListBoxValues(s,e,listTipoError, true) }"
                                            DropDown="function(s,e) { SynchronizeListBoxValues(s,e,listTipoError, true) }" />
                                    </dx:ASPxDropDownEdit>
                                </td>
                                <td style="padding-left: 10px">
                                    <div class="tamanho_celula" style="padding-top: 5px;">
                                        <asp:Label ID="lblMensajeError" Text="Error" runat="server" CssClass="label2"></asp:Label>
                                    </div>
                                    <asp:TextBox ID="txtError" Text="" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="291px" MaxLength="2000">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="tamanho_celula" style="padding-top: 5px;">
                                        <asp:Label ID="lblEstado" Text="Estado" runat="server" CssClass="label2"></asp:Label>
                                    </div>
                                    <dx:ASPxDropDownEdit ID="ddlStado" runat="server" ClientIDMode="Static" ClientInstanceName="ddlEstado" Height="22px" Width="291px" AnimationType="None">
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
                                </td>
                                <td style="padding-left: 10px">
                                    <div class="tamanho_celula" style="padding-top: 5px;">
                                        <asp:Label ID="lblCodigoProceso" Text="Código Proceso" runat="server" CssClass="label2"></asp:Label>
                                    </div>
                                    <asp:TextBox ID="txtCodigoProceso" Text="" autocomplete="txtCodigoProceso" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="291px" MaxLength="100">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:UpdatePanel ID="upSector" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:PlaceHolder runat="server" ID="phSector"></asp:PlaceHolder>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:UpdatePanel ID="upCliente" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div id="divBotonesFiltros" runat="server" style="margin-top: 10px">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" />
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" class="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" />
                    </div>
                </div>

                <div id="dvRegistros" runat="server" class="ui-panel-titlebar" style="margin-top: 10px">
                    <asp:Label ID="lblRegistros" SkinID="filtro-label_titulo" runat="server" Text="Registros" Style="color: #767676 !important;" />
                </div>

                <div style="width: 78%; align-content: center; text-align: center; align-items: center; margin: 4px;">
                    <dx:ASPxGridView ID="grilla" ClientInstanceName="grid" runat="server"
                        KeyFieldName="ID" Width="100%">
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataColumn FieldName="ID" Visible="false" VisibleIndex="6" />
                            <dx:GridViewDataColumn FieldName="ActualID" VisibleIndex="1" />
                            <dx:GridViewDataColumn FieldName="TipoError" VisibleIndex="2" />
                            <dx:GridViewDataColumn FieldName="CodProceso" VisibleIndex="3" />
                            <dx:GridViewDataColumn FieldName="Reintentos" VisibleIndex="4" />
                            <dx:GridViewDataColumn Width="80px" FieldName="Estado" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:Label runat="server"
                                        Visible='<%# If(Eval("CodEstado").ToString() = "CE", True, False) %>'
                                        Text='<%# MyBase.RecuperarValorDic("lblCerrado") %>'>
                                    </asp:Label>
                                     <asp:Label runat="server"
                                        Visible='<%# If(Eval("CodEstado").ToString() = "PD", True, False) %>'
                                        Text='<%# MyBase.RecuperarValorDic("lblPendiente") %>'>
                                    </asp:Label>
                                     <asp:Label runat="server"
                                        Visible='<%# If(Eval("CodEstado").ToString() = "AB", True, False) %>'
                                        Text='<%# MyBase.RecuperarValorDic("lblAbierto") %>'>
                                    </asp:Label>
                                </DataItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn VisibleIndex="6" CellStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:ImageButton runat="server" ImageUrl="~/Imagenes/next.png" ID="imgVerDetalle" CssClass="imgButton" OnCommand="imgVerDetalle" UseSubmitBehavior="False" CommandArgument='<%# Container.KeyValue %>' />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                        </Columns>
                        <ClientSideEvents SelectionChanged="grid_SelectionChanged" />
                    </dx:ASPxGridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <center>
        <table>
            <tr align="center">
                <td>
                    <asp:Button  ID="btnReenviar" Text="Reenviar" ClientIDMode="Static" runat="server" class="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" />
                </td>
                <td>
                    <asp:Button  ID="btnParar" Text="Reenviar" ClientIDMode="Static" runat="server" class="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" />
                </td>
            </tr>
        </table>
    </center>
</asp:Content>

