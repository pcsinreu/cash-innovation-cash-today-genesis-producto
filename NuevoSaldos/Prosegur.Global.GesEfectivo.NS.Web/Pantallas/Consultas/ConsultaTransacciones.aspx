<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="ConsultaTransacciones.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ConsultaTransacciones" %>


<%@ MasterType VirtualPath="~/Master/Master.Master" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid.Export" TagPrefix="dx" %>

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


            LoadingPanel.Hide();
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

        function AtualizarPuntos() {
            __doPostBack('<%=btnChangeMaquina.ClientID %>');

        }

        function AtualizaDelegaciones() {

            __doPostBack('<%=btnChangeDelegaciones.UniqueID %>');

        }

    </script>

    <script>

        var accordion1Active = <%=hfAccordion1.Value %>;
        var accordion2Active = JSON.parse( <%="'" + hfAccordion2.Value + "'" %>);

        $(document).ready(function () {
            configuraAccordion();
            LoadingPanel.Hide();
        });
        function ChangedTipoTransaccionesSelected() {
            __doPostBack('<%=btnChangedTipoTransacciones.UniqueID %>');
        }
        function configuraAccordion() {
            $(".filtro").css("visibility", "visible");

            inicializeAccordion("0", JSON.parse(document.getElementById('<%=hfAccordion2.ClientID %>').value)["0"]);
            inicializeAccordion("1", JSON.parse(document.getElementById('<%=hfAccordion2.ClientID %>').value)["1"]);
            inicializeAccordion("2", JSON.parse(document.getElementById('<%=hfAccordion2.ClientID %>').value)["2"]);

            $("ul.trackAccordion1").accordion({
                autoHeight: true,
                collapsible: true,
                heightStyle: "content",
                active: accordion1Active,
                activate: function (event, ui) {
                    var index = -1;

                    if (ui.newHeader[0] != null)
                        index = $(this).children('li').index(ui.newHeader[0].parentNode)

                    document.getElementById('<%=hfAccordion1.ClientID %>').value = index;


                }
            });
        }


        function pageLoad(sender, args) {

            configuraAccordion();
        }
        function inicializeAccordion(classe, activ) {


            $("ul.itemAccordion" + classe).accordion({
                autoHeight: true,
                collapsible: true,
                heightStyle: "content",
                active: activ,
                activate: function (event, ui) {
                    var index = 1;

                    if (ui.newHeader[0] != null)
                        index = $(this).children('li.nestedElems').index(ui.newHeader[0].parentNode)

                    var accordion2Active = JSON.parse(document.getElementById('<%=hfAccordion2.ClientID %>').value);

                    accordion2Active[classe] = index;

                    document.getElementById('<%=hfAccordion2.ClientID %>').value = JSON.stringify(accordion2Active);


                }
            });
        }



    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upSector" runat="server" ChildrenAsTriggers="true" UpdateMode="Always" style="margin-left: 10px;">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpTodo" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:PlaceHolder ID="phUcPopUp" runat="server"></asp:PlaceHolder>
            <div class="content filtro" style="color: #767676; visibility: hidden;">
                <ul class="trackAccordion1">
                    <li>
                        <!-- element which hold Individual class in accordian -->

                        <a runat="server" id="Titulo_Filtro" class="is-expanded" href="#">Filtro</a>
                        <div style="padding-right: 11px; padding-left: 11px;">
                            <ul class="course_beginner itemAccordion0 nestedElemAccor ">

                                <li class="nestedElems">
                                    <a id="Titulo_FiltroGeneral" runat="server" href="#" class="begNestElem ui-corner-all"></a>

                                    <div id="dvFiltroGeneral" class="ui-corner-bottom ui-accordion-content-active" aria-expanded="true" aria-hidden="false" style="padding: 10px;">
                                        <div id="dvTipoTransacciones" runat="server" style="float: left; margin: 0px 10px 10px 0px;">
                                            <asp:Label ID="lblTipoTransacciones" runat="server" Text="lblTipoTransacciones" /><br />

                                            <dx:ASPxDropDownEdit ClientInstanceName="dropDownTipoTransacciones" ID="dropDownTipoTransacciones" Width="270px" runat="server" AnimationType="None" CssClass="dxHeight" Height="10px" Theme="Default">
                                                <DropDownWindowStyle BackColor="#EDEDED" Font-Names="Arial" Font-Size="11px">
                                                    <Paddings Padding="0px" />
                                                </DropDownWindowStyle>
                                                <DropDownWindowTemplate>
                                                    <dx:ASPxListBox Width="100%" ID="listBoxTipoTransacciones" ClientInstanceName="listBoxTipoTransacciones" SelectionMode="CheckColumn" runat="server">
                                                        <Border BorderStyle="None" />
                                                        <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                                        <Items></Items>
                                                        <ClientSideEvents SelectedIndexChanged="function(s,e) { OnListBoxSelectionChanged(s,e,dropDownTipoTransacciones, true) }" />
                                                    </dx:ASPxListBox>
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="padding: 4px">
                                                                <dx:ASPxButton ID="btnTipoTransacciones" AutoPostBack="False" runat="server" Text="Close" Style="float: right">
                                                                    <ClientSideEvents Click="function(s, e){ dropDownTipoTransacciones.HideDropDown(); ChangedTipoTransaccionesSelected();}" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </DropDownWindowTemplate>
                                                <ClientSideEvents TextChanged="function(s,e) { SynchronizeListBoxValues(s,e,listBoxTipoTransacciones, true) }" DropDown="function(s,e) { SynchronizeListBoxValues(s,e,listBoxTipoTransacciones, true) }" CloseUp="function(s, e) {
	ChangedTipoTransaccionesSelected();
}" />
                                            </dx:ASPxDropDownEdit>
                                        </div>
                                        <div id="dvModalidad" runat="server" style="float: left; margin: 0px 10px 10px 0px;">
                                            <asp:Label ID="lblModalidad" runat="server" Text="lblModalidad" /><br />
                                            <dx:ASPxDropDownEdit ClientInstanceName="dropDownModalidad" ID="dropDownModalidad" Width="270px" runat="server" AnimationType="None">
                                                <DropDownWindowStyle BackColor="#EDEDED" />
                                                <DropDownWindowTemplate>
                                                    <dx:ASPxListBox Width="100%" ID="listBoxModalidad" ClientInstanceName="listBoxModalidad" SelectionMode="CheckColumn" runat="server">
                                                        <Border BorderStyle="None" />
                                                        <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                                        <Items></Items>
                                                        <ClientSideEvents SelectedIndexChanged="function(s,e) { OnListBoxSelectionChanged(s,e,dropDownModalidad, true) }" />
                                                    </dx:ASPxListBox>
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="padding: 4px">
                                                                <dx:ASPxButton ID="btnModalidad" AutoPostBack="False" runat="server" Text="Close" Style="float: right">
                                                                    <ClientSideEvents Click="function(s, e){ dropDownModalidad.HideDropDown();}" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </DropDownWindowTemplate>
                                                <ClientSideEvents TextChanged="function(s,e) { SynchronizeListBoxValues(s,e,listBoxModalidad, true) }" DropDown="function(s,e) { SynchronizeListBoxValues(s,e,listBoxModalidad, true) }" />
                                            </dx:ASPxDropDownEdit>
                                        </div>
                                        <div id="dvNotificadion" runat="server" style="float: left; margin: 0px 10px 10px 0px;">
                                            <asp:Label ID="lblNotificacion" runat="server" Text="lblNotificacion" /><br />
                                            <dx:ASPxDropDownEdit ClientInstanceName="dropDownNotificacion" ID="dropDownNotificacion" Width="270px" runat="server" AnimationType="None">
                                                <DropDownWindowStyle BackColor="#EDEDED" />
                                                <DropDownWindowTemplate>
                                                    <dx:ASPxListBox Width="100%" ID="listBoxNotificacion" ClientInstanceName="listBoxNotificacion" SelectionMode="CheckColumn" runat="server">
                                                        <Border BorderStyle="None" />
                                                        <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                                        <Items></Items>
                                                        <ClientSideEvents SelectedIndexChanged="function(s,e) { OnListBoxSelectionChanged(s,e,dropDownNotificacion, true) }" />
                                                    </dx:ASPxListBox>
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="padding: 4px">
                                                                <dx:ASPxButton ID="btnNotificacion" AutoPostBack="False" runat="server" Text="btnNotificacion" Style="float: right">
                                                                    <ClientSideEvents Click="function(s, e){ dropDownNotificacion.HideDropDown(); }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </DropDownWindowTemplate>
                                                <ClientSideEvents TextChanged="function(s,e) { SynchronizeListBoxValues(s,e,listBoxNotificacion, true) }" DropDown="function(s,e) { SynchronizeListBoxValues(s,e,listBoxNotificacion, true) }" />
                                            </dx:ASPxDropDownEdit>
                                        </div>

                                        <div id="dvExtraData" runat="server" style="float: left; margin: 0px 10px 10px 0px;">
                                            <asp:UpdatePanel ID="upCampoExtra1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:PlaceHolder runat="server" ID="phCampoExtra"></asp:PlaceHolder>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="dvclear"></div>
                                        <div id="dvDelegaciones" runat="server" style="float: left; margin: 0px 10px 10px 0px;">
                                            <asp:Label ID="lblDelegaciones" runat="server" Text="lblDelegaciones" /><br />
                                            <dx:ASPxDropDownEdit ClientInstanceName="dropDownDelegaciones" ID="dropDownDelegaciones" Width="270px" runat="server" AnimationType="None">
                                                <DropDownWindowStyle BackColor="#EDEDED" />
                                                <DropDownWindowTemplate>
                                                    <dx:ASPxListBox Width="100%" ID="listBoxDelegaciones" ClientInstanceName="listBoxDelegaciones" SelectionMode="CheckColumn" runat="server">
                                                        <Border BorderStyle="None" />
                                                        <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                                        <Items></Items>
                                                        <ClientSideEvents SelectedIndexChanged="function(s,e) { OnListBoxSelectionChanged(s,e,dropDownDelegaciones, true) }" />
                                                    </dx:ASPxListBox>
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="padding: 4px">
                                                                <dx:ASPxButton ID="btnDelegaciones" AutoPostBack="False" runat="server" Text="btnDelegaciones" Style="float: right">
                                                                    <ClientSideEvents Click="function(s, e){ dropDownDelegaciones.HideDropDown(); }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </DropDownWindowTemplate>
                                                <ClientSideEvents TextChanged="function(s,e) { SynchronizeListBoxValues(s,e,listBoxDelegaciones, true) }" DropDown="function(s,e) { SynchronizeListBoxValues(s,e,listBoxDelegaciones, true) }" />
                                            </dx:ASPxDropDownEdit>
                                        </div>
                                        <div id="dvCanales" runat="server" style="padding: 0px; float: left; margin: 0px 10px 2px 0px; font-family: Arial, Helvetica, sans-serif; font-size: 11px;">
                                            <asp:Label ID="lblCanales" runat="server" Text="lblCanales" /><br />
                                            <dx:ASPxDropDownEdit ClientInstanceName="dropDownCanales" ID="dropDownCanales" Width="270px" runat="server" AnimationType="None">
                                                <DropDownWindowStyle BackColor="#EDEDED" />
                                                <DropDownWindowTemplate>
                                                    <dx:ASPxListBox Width="100%" ID="listBoxCanales" ClientInstanceName="listBoxCanales" SelectionMode="CheckColumn" runat="server">
                                                        <Border BorderStyle="None" />
                                                        <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                                        <Items></Items>
                                                        <ClientSideEvents SelectedIndexChanged="function(s,e) { OnListBoxSelectionChanged(s,e,dropDownCanales, true) }" />
                                                    </dx:ASPxListBox>
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="padding: 4px">
                                                                <dx:ASPxButton ID="btnCanales" AutoPostBack="False" runat="server" Text="btnCanales" Style="float: right">
                                                                    <ClientSideEvents Click="function(s, e){ dropDownCanales.HideDropDown(); }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </DropDownWindowTemplate>
                                                <ClientSideEvents TextChanged="function(s,e) { SynchronizeListBoxValues(s,e,listBoxCanales, true) }" DropDown="function(s,e) { SynchronizeListBoxValues(s,e,listBoxCanales, true) }" />
                                            </dx:ASPxDropDownEdit>
                                        </div>

                                        <div id="dvAcreditacion" runat="server" style="float: left; margin: 0px 10px 10px 0px;">
                                            <asp:Label ID="lblAcreditacion" runat="server" Text="lblAcreditacion" /><br />
                                            <dx:ASPxDropDownEdit ClientInstanceName="dropDownAcreditacion" ID="dropDownAcreditacion" Width="270px" runat="server" AnimationType="None">
                                                <DropDownWindowStyle BackColor="#EDEDED" />
                                                <DropDownWindowTemplate>
                                                    <dx:ASPxListBox Width="100%" ID="listBoxAcreditacion" ClientInstanceName="listBoxAcreditacion" SelectionMode="CheckColumn" runat="server">
                                                        <Border BorderStyle="None" />
                                                        <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                                        <Items></Items>
                                                        <ClientSideEvents SelectedIndexChanged="function(s,e) { OnListBoxSelectionChanged(s,e,dropDownAcreditacion, true) }" />
                                                    </dx:ASPxListBox>
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="padding: 4px">
                                                                <dx:ASPxButton ID="btnAcreditacion" AutoPostBack="False" runat="server" Text="btnAcreditacion" Style="float: right">
                                                                    <ClientSideEvents Click="function(s, e){ dropDownAcreditacion.HideDropDown(); }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </DropDownWindowTemplate>
                                                <ClientSideEvents TextChanged="function(s,e) { SynchronizeListBoxValues(s,e,listBoxAcreditacion, true) }" DropDown="function(s,e) { SynchronizeListBoxValues(s,e,listBoxAcreditacion, true) }" />
                                            </dx:ASPxDropDownEdit>
                                        </div>
                                        <asp:UpdatePanel ID="upCampoExtra2" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div id="dvCampoExtraValor" runat="server" style="float: left; margin: 0px 10px 10px 0px;">
                                                    <asp:Label ID="lblCampoExtraValor" runat="server" Text="lblCampoExtraValor" />
                                                    <br />
                                                    <asp:TextBox ID="txtCampoExtra" runat="server" Text="" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <div class="dvclear"></div>
                                        <div runat="server" id="dvFecha" name="dvFecha" style="float: left; margin: 0px 10px 10px 0px;">
                                            <asp:Label ID="lblFecha" runat="server" Text="lblFecha" /><br />
                                            <dx:ASPxComboBox ID="comboFecha" runat="server"
                                                CallbackPageSize="15" Width="270px"
                                                EnableCallbackMode="True" EnableViewState="False">
                                            </dx:ASPxComboBox>
                                        </div>
                                        <div runat="server" id="dvFechaDesde" name="dvFechaDesde" style="float: left; margin: 0px 10px 10px 0px;">
                                            <br />
                                            <asp:TextBox ID="txtFechaDesde" runat="server" />
                                        </div>
                                        <div runat="server" id="dvFechaHasta" name="dvFechaHasta" style="float: left; margin: 0px 10px 10px 0px;">
                                            <br />
                                            <asp:Label ID="lblA" runat="server" Text="lblA" />&nbsp;&nbsp;
                <asp:TextBox ID="txtFechaHasta" runat="server" />
                                        </div>
                                        <div runat="server" id="dvFechaGestion" name="dvFechaGestion" style="float: left; margin: 0px 10px 10px 0px; display: none;">
                                            <asp:Label ID="lblFechaGestion" runat="server" Text="lblFechaGestion" /><br />
                                            <asp:TextBox ID="txtFechaGestion" runat="server" />
                                        </div>
                                        <div id="dvShipOut" style="float: left; margin: 0px 10px 10px 0px;">
                                            <br />
                                            <dx:ASPxCheckBox ID="ckbShipOut" runat="server" ClientInstanceName="checkBox" Text="ckbShipOut" EnableViewState="False" CheckState="Unchecked">
                                                <ClientSideEvents CheckedChanged="ShipOutChanged" />
                                            </dx:ASPxCheckBox>
                                        </div>

                                        <div id="dvImporteInformativo" style="float: left; margin: 0px 10px 10px 0px;">
                                            <br />
                                            <dx:ASPxCheckBox ID="ckbImporteInformativo" runat="server" ClientInstanceName="checkBox" Text="ckbImporteInformativo" EnableViewState="False" CheckState="Unchecked">
                                            </dx:ASPxCheckBox>
                                        </div>

                                        <div class="dvclear"></div>
                                    </div>

                                </li>
                            </ul>
                            <ul class="course_beginner itemAccordion1 nestedElemAccor ">
                                <li class="nestedElems">
                                    <a runat="server" id="Titulo_FiltroMaquinas" href="#" class="intNestElem"></a>

                                    <div id="dvFiltroMaquinas" style="padding: 10px;">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div id="dvCliente" style="float: left; margin: 0px 10px 10px 0px;">
                                                    <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="dvclear"></div>

                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div id="dvSector" style="float: left; margin: 0px 10px 10px 0px;">
                                                    <asp:PlaceHolder runat="server" ID="phSector"></asp:PlaceHolder>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>

                                </li>

                            </ul>
                            <ul class="course_beginner itemAccordion2 nestedElemAccor ">
                                <li class="nestedElems">
                                    <a runat="server" id="Titulo_FiltroPlanificacion" href="#" class="advNestElem"></a>





                                    <div id="dvFiltroPlanificacion" style="padding: 10px">


                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div id="divBancoFacturacion" style="float: left; margin: 0px 10px 10px 0px;">
                                                    <asp:PlaceHolder runat="server" ID="phBancoComision"></asp:PlaceHolder>

                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>


                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div id="divPlanificacion" style="float: left; margin: 0px 10px 10px 0px;">
                                                    <asp:PlaceHolder runat="server" ID="phPlanificacion"></asp:PlaceHolder>

                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <div id="dvTipoPlanificacion" runat="server" style="float: left; margin: 0px 10px 10px 0px;">
                                            <asp:Label ID="lblTipoPlanificacion" runat="server" Text="lblTipoPlanificacion" /><br />
                                            <dx:ASPxDropDownEdit ClientInstanceName="dropDownTipoPlanificacion" ID="dropDownTipoPlanificacion" Width="270px" runat="server" AnimationType="None">
                                                <DropDownWindowStyle BackColor="#EDEDED" />
                                                <DropDownWindowTemplate>
                                                    <dx:ASPxListBox Width="100%" ID="listBoxTipoPlanificacion" ClientInstanceName="listBoxTipoPlanificacion" SelectionMode="CheckColumn" runat="server">
                                                        <Border BorderStyle="None" />
                                                        <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                                        <Items></Items>
                                                        <ClientSideEvents SelectedIndexChanged="function(s,e) { OnListBoxSelectionChanged(s,e,dropDownTipoPlanificacion, true) }" />
                                                    </dx:ASPxListBox>
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="padding: 4px">
                                                                <dx:ASPxButton ID="btnTipoPlanificacion" AutoPostBack="False" runat="server" Text="btnTipoPlanificacion" Style="float: right">
                                                                    <ClientSideEvents Click="function(s, e){ dropDownTipoPlanificacion.HideDropDown();}" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </DropDownWindowTemplate>
                                                <ClientSideEvents TextChanged="function(s,e) { SynchronizeListBoxValues(s,e,listBoxTipoPlanificacion, true) }" DropDown="function(s,e) { SynchronizeListBoxValues(s,e,listBoxTipoPlanificacion, true) }" />
                                            </dx:ASPxDropDownEdit>
                                        </div>

                                        <div class="dvclear">
                                        </div>
                                        <asp:UpdatePanel ID="upBanco" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div id="dvBanco" runat="server" style="float: left; margin: 0px 10px 10px 0px;">
                                                    <asp:PlaceHolder runat="server" ID="phBanco"></asp:PlaceHolder>
                                                </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="dvclear">
                                        </div>
                                    </div>
                                </li>
                            </ul>
                            <div>
                                <div style="margin: 5px 0px 0px 5px !important; padding: 0px !important; height: auto; text-align: right;">

                                    <asp:Button ID="btnLimpar" runat="server" CssClass="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px; margin-right: 10px;" />
                                    <asp:Button ID="btnBuscar" runat="server" CssClass="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" />
                                </div>
                            </div>
                        </div>
                    </li>
                    <!-- END element which hold Individual class in accordian -->
                </ul>


                <div id="dvSemDatos" runat="server" class="ui-corner-bottom ui-corner-top" style="margin-top: 10px; border: 1px solid #aaa; padding: 10px;">

                    <span id="lblSemDatos" runat="server" style="color: #767676; font-size: 9pt; font-style: italic;">sem dados</span>
                </div>
                <div id="dvExportGrid" runat="server" style="margin-top: 10px; width: auto; /*height: 20px; */ background-color: #dcdcdc; border: 1px solid #9f9f9f; border-bottom: 0px; text-align: right;">

                    <div style="height: 22px">



                        <div id="ImgButton1" class="btn" style="margin-right: 5px; padding-right: 0px; padding-left: 0px;">
                            <%--padding: 0px; margin-right: 5px;">--%>
                            <img alt="" style="height: 16px" class="arrange" src="<%=Page.ResolveUrl("~/Imagenes/config.png")%>" />
                        </div>
                    </div>
                    <dx:ASPxPopupMenu ID="ASPxPopupMenu1" runat="server" ClientInstanceName="ASPxPopupMenuClientControl"
                        PopupElementID="ImgButton1" ShowPopOutImages="True"
                        PopupHorizontalAlign="LeftSides" PopupVerticalAlign="Below" PopupAction="MouseOver">
                        <Items>
                            <dx:MenuItem GroupName="colSelect" Text="Mostrar Seletor de Colunas" Name="colSelect">
                            </dx:MenuItem>
                            <dx:MenuItem GroupName="Export" Text="Exportar" Name="Export">
                                <Items>
                                    <dx:MenuItem GroupName="Export" Text="PDF" Name="PDF"></dx:MenuItem>
                                    <dx:MenuItem GroupName="Export" Text="CSV" Name="CSV"></dx:MenuItem>
                                    <dx:MenuItem GroupName="Export" Text="XLS" Name="XLS"></dx:MenuItem>
                                    <dx:MenuItem GroupName="Export" Text="XLSX" Name="XLSX"></dx:MenuItem>
                                </Items>
                            </dx:MenuItem>
                            <dx:MenuItem GroupName="BorrarPreferencias" Text="Borrar Preferencias" Name="BorrarPreferencias">
                            </dx:MenuItem>
                            <dx:MenuItem GroupName="Expand" Text="Expandir/Retraer" Name="Expand">
                            </dx:MenuItem>

                        </Items>
                        <ClientSideEvents Init="InitPopupMenuHandler" ItemClick="function(s, e) {
                        
                
        if (	e.item.name == 'colSelect'){
                
                            pivotGrid.ChangeCustomizationFieldsVisibility();
	
        }else if('|PDF|CSV|XLSX|'.includes(e.item.name)) {
                    __doPostBack('ctl00$ContentPlaceHolder1$btnExport',e.item.name)

        }else if(e.item.name == 'BorrarPreferencias') {
                    __doPostBack('ctl00$ContentPlaceHolder1$btnBorrarPreferencias',e.item.name)

        }else if(e.item.name == 'Expand') {
                    __doPostBack('ctl00$ContentPlaceHolder1$btnExpand',e.item.name)

        }
                        
}" />
                    </dx:ASPxPopupMenu>
                </div>
                <div style="margin-top: 0px; overflow: auto;">

                    <div style="margin-right: 1px;">
                        <dx:ASPxPivotGrid ID="grid" runat="server" Style="padding-right: 10px;" ClientIDMode="AutoID" Width="100%" ClientInstanceName="pivotGrid" CustomizationFieldsLeft="300" CustomizationFieldsTop="400">

                            <Fields>
                                <dx:PivotGridField ID="gvCampoExtraValor" Area="RowArea" FieldName="CampoExtraValor" Caption="CampoExtraValor" AllowedAreas="RowArea" Options-AllowDrag="True" AreaIndex="1">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>
                                <dx:PivotGridField ID="gvCodExternoBase" Area="RowArea" FieldName="CodExternoBase" Caption="CodExternoBase" AllowedAreas="RowArea" Options-AllowDrag="True" AreaIndex="2">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>
                                <dx:PivotGridField ID="gvCodExterno" Area="RowArea" FieldName="CodExterno" Caption="CodExterno" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="False" AreaIndex="3">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                                <dx:PivotGridField ID="gvfechaGestao" Area="RowArea" FieldName="FechaGestion" Caption="gvfechaGestao" AllowedAreas="RowArea" Options-AllowDrag="True" AreaIndex="4">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                                <dx:PivotGridField ID="gvHoraGestion" Area="RowArea" FieldName="HoraGestion" Caption="HoraGestion" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="True" AreaIndex="5">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>
                                <dx:PivotGridField ID="gvFechaAcreditacion" Area="RowArea" FieldName="FechaAcreditacion" Caption="FechaAcreditacion" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="false" AreaIndex="6">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>
                                <dx:PivotGridField ID="gvHoraAcreditacion" Area="RowArea" FieldName="HoraAcreditacion" Caption="HoraAcreditacion" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="false" AreaIndex="7">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                                <dx:PivotGridField ID="gvFechaNotificacion" Area="RowArea" FieldName="FechaNotificacion" Caption="FechaNotificacion" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="false" AreaIndex="8">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                                <dx:PivotGridField ID="gvHoraNotificacion" Area="RowArea" FieldName="HoraNotificacion" Caption="HoraNotificacion" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="False" AreaIndex="9">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                                <dx:PivotGridField ID="gvFechaCreacion" Area="RowArea" FieldName="FechaCreacion" Caption="FechaCreacion" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="false" AreaIndex="10">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                                <dx:PivotGridField ID="gvHoraCreacion" Area="RowArea" FieldName="HoraCreacion" Caption="HoraCreacion" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="False" AreaIndex="11">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                                <dx:PivotGridField ID="gvMaquina" Area="RowArea" FieldName="Maquina" Caption="Maquina" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="True" AreaIndex="12">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                                <dx:PivotGridField ID="gvPuntoServicio" Area="RowArea" FieldName="PuntoServicio" Caption="PuntoServicio" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="false" AreaIndex="13">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                                <dx:PivotGridField ID="gvCliente" Area="RowArea" FieldName="Cliente" Caption="Cliente" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="False" AreaIndex="14">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>


                                <dx:PivotGridField ID="gvSubCliente" Area="RowArea" FieldName="SubCliente" Caption="SubCliente" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="False" AreaIndex="15">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                                <dx:PivotGridField ID="gvDelegacion" Area="RowArea" FieldName="Delegacion" Caption="Delegacion" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="False" AreaIndex="16">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                                <dx:PivotGridField ID="gvCanal" Area="RowArea" FieldName="Canal" Caption="Canal" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="False" AreaIndex="17">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                                <dx:PivotGridField ID="gvSubCanal" Area="RowArea" FieldName="SubCanal" Caption="SubCanal" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="False" AreaIndex="18">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                                <dx:PivotGridField ID="gvTipoTransaccion" Area="RowArea" FieldName="TipoTransaccion" Caption="TipoTransaccion" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="true" AreaIndex="19">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                                <dx:PivotGridField ID="gvFormulario" Area="RowArea" FieldName="Formulario" Caption="Formulario" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="False" AreaIndex="20">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                                <dx:PivotGridField ID="gvCodResponsable" Area="RowArea" FieldName="CodResponsable" Caption="CodResponsable" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="False" AreaIndex="21">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                                <dx:PivotGridField ID="gvNombreResponsable" Area="RowArea" FieldName="NombreResponsable" Caption="NombreResponsable" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="False" AreaIndex="22">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                                <dx:PivotGridField ID="gvReceiptNumber" Area="RowArea" FieldName="ReceiptNumber" Caption="ReceiptNumber" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="False" AreaIndex="23">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                                <dx:PivotGridField ID="gvBarcode" Area="RowArea" FieldName="Barcode" Caption="Barcode" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="False" AreaIndex="24">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                                <dx:PivotGridField ID="gvModalidad" Area="RowArea" FieldName="Modalidad" Caption="Modalidad" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="False" AreaIndex="25">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                                <dx:PivotGridField ID="gvNotificacion" Area="RowArea" FieldName="Notificacion" Caption="Notificacion" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="False" AreaIndex="26">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                             

                                <dx:PivotGridField ID="gvImporteContado" Area="DataArea" FieldName="ImporteContado" Caption="ImporteContado" AllowedAreas="DataArea" Options-AllowDrag="True" AreaIndex="0" CellFormat-FormatString="'">
                                    <CellStyle BackColor="White" Wrap="False">
                                    </CellStyle>
                                    <HeaderStyle BackColor="#0000CC" CssClass="CorHeader" />
                                    <ValueStyle BackColor="Red">
                                    </ValueStyle>
                                    <ValueTotalStyle BackColor="#009933">
                                    </ValueTotalStyle>
                                </dx:PivotGridField>
                                <%--         <dx:PivotGridField ID="gvImporteDeclarado" Area="DataArea" FieldName="ImporteDeclarado"  Caption="ImporteDeclarado" AllowedAreas="DataArea" Options-AllowDrag="True"  AreaIndex="1">
                        <HeaderStyle BackColor="#dcdcdc" />
                    </dx:PivotGridField>--%>

                                <%--        <dx:PivotGridField ID="gvImporte" Area="DataArea" FieldName="Importe"  Caption="Importe" AllowedAreas="DataArea" Options-AllowDrag="True"  AreaIndex="1">
                        <HeaderStyle BackColor="#dcdcdc" />
                    </dx:PivotGridField>--%>


                                <dx:PivotGridField ID="gvDivisa" Area="RowArea" FieldName="Divisa" Caption="Divisa" TotalsVisibility="None" AllowedAreas="RowArea" Options-AllowDrag="True" AreaIndex="28">
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>

                                <dx:PivotGridField ID="gvImporteInformativo" Area="RowArea" FieldName="ImporteInformativo" Caption="Inf." TotalsVisibility="None" AllowedAreas="RowArea" Options-AllowDrag="True" AreaIndex="29">
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>
                                <dx:PivotGridField ID="gvCantidad" Area="RowArea" FieldName="Cantidad" Caption="Cantidad" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="False" AreaIndex="30">
                                  <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>
                                <dx:PivotGridField ID="gvBaseDeviceId" Area="RowArea" FieldName="BaseDeviceId" Caption="BaseDeviceId" AllowedAreas="RowArea" Options-AllowDrag="True" Visible="False" AreaIndex="31">
                                  <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>
                                <dx:PivotGridField ID="gvColumnPivot" Area="ColumnArea" FieldName="ColumnPivot" Caption="ColumnPivot" TotalsVisibility="None" AllowedAreas="ColumnArea" Options-AllowDrag="True" AreaIndex="0">
                                    <HeaderStyle BackColor="#dcdcdc" />
                                    <ValueStyle BackColor="White">
                                    </ValueStyle>
                                </dx:PivotGridField>




                            </Fields>
                            <ClientSideEvents CustomizationFieldsVisibleChanged="function(s, e) {
	
        seletorColunas();
}" />
                            <OptionsView ShowContextMenus="false" ShowColumnGrandTotalHeader="false" ShowRowGrandTotals="False" ShowDataHeaders="False" ShowFilterHeaders="false" ShowColumnHeaders="false" ShowColumnGrandTotals="false" ShowRowTotals="false" ShowCustomTotalsForSingleValues="false" ShowGrandTotalsForSingleValues="false" ShowRowGrandTotalHeader="false" ShowTotalsForSingleValues="false" ShowColumnTotals="False" />
                            <OptionsCustomization AllowCustomizationForm="true" AllowDragInCustomizationForm="true" AllowFilterInCustomizationForm="true" AllowSortInCustomizationForm="true" CustomizationWindowWidth="300" CustomizationWindowHeight="182" AllowPrefilter="false" AllowExpand="true" AllowExpandOnDoubleClick="false" CustomizationFormLayout="StackedSideBySide" FilterPopupWindowMinWidth="200px" />
                            <%-- <OptionsView ShowContextMenus="false" ShowColumnGrandTotalHeader="false" ShowDataHeaders="False" ShowFilterHeaders="false" ShowColumnHeaders="false" ShowColumnGrandTotals="false" ShowCustomTotalsForSingleValues="false" ShowGrandTotalsForSingleValues="false" ShowColumnTotals="False" ShowRowTotals="False" ShowTotalsForSingleValues="false" ShowRowGrandTotalHeader="False" ShowRowGrandTotals="False" />
                    <OptionsCustomization AllowCustomizationForm="true" AllowDragInCustomizationForm="true" AllowFilterInCustomizationForm="true" AllowSortInCustomizationForm="true" CustomizationWindowWidth="260" CustomizationWindowHeight="250" AllowPrefilter="false" AllowExpand="true" AllowExpandOnDoubleClick="false" CustomizationFormLayout="TopPanelOnly" FilterPopupWindowMinWidth="200px" />--%>
                            <OptionsPager Position="Bottom" RowsPerPage="8">
                                <PageSizeItemSettings Visible="True">
                                </PageSizeItemSettings>
                            </OptionsPager>
                            <OptionsChartDataSource ProvideDataByColumns="false" ProvideEmptyCells="false" />
                            <OptionsFilter GroupFilterMode="List" NativeCheckBoxes="true" ShowOnlyAvailableItems="true" ShowHiddenItems="true" />
                            <Styles>
                                <HeaderStyle BackColor="Yellow" />
                                <CustomTotalCellStyle BackColor="#993399">
                                </CustomTotalCellStyle>
                            </Styles>
                        </dx:ASPxPivotGrid>
                    </div>
                    <dx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="grid" Visible="False" />
                    <dx:ASPxButton ID="btnDetalle" runat="server" Text="BtnDetalle" Visible="false" UseSubmitBehavior="False" />
                    <dx:ASPxButton ID="btnExport" ClientInstanceName="btnExportt" AutoPostBack="true" runat="server" Visible="false" Text="export" ClientIDMode="Static" OnClick="btn_Click" UseSubmitBehavior="False" />
                    <dx:ASPxButton ID="btnPopUp" ClientInstanceName="btnPopUp" AutoPostBack="true" runat="server" Visible="false" Text="btnPopUp" ClientIDMode="Static" UseSubmitBehavior="False" />
                    <dx:ASPxButton ID="btnBorrarPreferencias" ClientInstanceName="btnBorrarPreferencias" AutoPostBack="true" runat="server" Visible="false" Text="btnBorrarPreferencias" ClientIDMode="Static" UseSubmitBehavior="False" />
                    <dx:ASPxButton ID="btnExpand" ClientInstanceName="btnBorrarPreferencias" AutoPostBack="true" runat="server" Visible="false" Text="btnBorrarPreferencias" ClientIDMode="Static" UseSubmitBehavior="False" />
                    <dx:ASPxButton ID="btnChangeDelegaciones" ClientInstanceName="btnChangeDelegaciones" AutoPostBack="true" runat="server" Visible="false" Text="btnChangeDelegaciones" ClientIDMode="Static" UseSubmitBehavior="False" />
                    <dx:ASPxButton ID="btnChangeMaquina" ClientInstanceName="btnChangeMaquina" AutoPostBack="true" runat="server" Visible="false" Text="btnChangeMaquina" UseSubmitBehavior="False" />
                    <dx:ASPxButton ID="btnChangedTipoTransacciones" ClientInstanceName="btnChangedTipoTransacciones" AutoPostBack="true" runat="server" Visible="false" Text="btnChangedTipoTransacciones" UseSubmitBehavior="False" />

                </div>
            </div>
            <asp:HiddenField ID="hfCanales" runat="server" />
            <asp:HiddenField ID="hfTitleCustomization" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hfAccordion1" runat="server" />
            <asp:HiddenField ID="hfAccordion2" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
</asp:Content>

