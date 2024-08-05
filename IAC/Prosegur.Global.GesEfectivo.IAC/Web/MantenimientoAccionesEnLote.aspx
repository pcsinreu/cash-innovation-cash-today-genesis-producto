<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="MantenimientoAccionesEnLote.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoAccionesEnLote" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
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
        $("#dvlblPlanta").css("opacity", "0");
        $("#dvPlanta").css("opacity", "0");

        function calculateTotal() {
            calculateTotalGrid('<%=gvSinRecuento.ClientID %>', '<%=lblValorTotalSinRecuento.ClientID %>');
            calculateTotalGrid('<%=gvConRecuento.ClientID %>', '<%=lblValorTotalConRecuento.ClientID %>');


        };

        function calculateTotalGrid(grid, label) {
            var checkedCheckBox = 0;
            var totalRows = 0;
            var dataGrid = document.getElementById(grid);//"<%=gvSinRecuento.ClientID %>");
            var valLabel = document.getElementById(label);//"<%=gvSinRecuento.ClientID %>");
            var rows = dataGrid.rows;
            for (var index = 1; index < rows.length; index++) {
                totalRows++;
                var checkBox = rows[index].cells[0].childNodes[1].childNodes[0];
                if (checkBox.checked)
                    checkedCheckBox++;
            }
            valLabel.innerHTML = checkedCheckBox.toString() + "/" + totalRows.toString();
        };
        function SelectAll(option, checkHeader) {
            var dataGrid = null;
            if (option == 1) {
                dataGrid = document.getElementById("<%= gvSinRecuento.ClientID %>");

            } else if (option == 2) {
                dataGrid = document.getElementById("<%= gvConRecuento.ClientID %>");
            }

            var rows = dataGrid.rows;
            for (var index = 1; index < rows.length; index++) {

                var checkBox = rows[index].cells[0].childNodes[1].childNodes[0];
                checkBox.checked = checkHeader.checked;
            }

            calculateTotal();
        };

    </script>
    <style type="text/css">
        .chkSemBorda {
            margin: 0px !important;
            padding-left: 0px !important;
            padding-right: 0px !important;
        }

        span input[type="checkbox"] {
            margin: 5px !important;
            padding-left: 0px !important;
            padding-right: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <asp:UpdatePanel ID="UpdatePanelGeral" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
            <ContentTemplate>
                <div id="Filtros">
                    <fieldset id="ExpandCollapseDiv" class="ui-fieldset ui-widget ui-widget-content ui-corner-all ui-fieldset-toggleable">
                        <legend class="ui-fieldset-legend ui-corner-all ui-state-default ui-state-active">
                            <div id="DivFiltros" class="legend-collapse" onclick="ocultarExibir();">
                                <asp:Label ID="lblSubTitulosCriteriosBusqueda" CssClass="legent-text" runat="server">Filtrar</asp:Label>
                            </div>
                        </legend>
                        <div class="accordion" style="display: block; position: relative;">

                            <div id="dvDelegacionPlanta" style="float: left; margin-left: 7px; margin-bottom: 7px;">
                                <asp:UpdatePanel ID="uppDelPLa" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div style="position: relative;">
                                            <div id="dvlblDelegacion" style="width: 120px; float: left; margin: 5px 2px 0px 2px;">
                                                <asp:Label ID="lblDelegacion" runat="server" CssClass="label2" nowrap="false"></asp:Label>
                                            </div>
                                            <div id="dvDelegacion" style="width: 330px; float: left;">
                                                <asp:DropDownList ID="ddlDelegacion" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="300px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>



                            <div id="dvCliente" style="float: left;">
                                <asp:UpdatePanel ID="updUcClienteUc" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                    <ContentTemplate>
                                        <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            <div style="clear: both;"></div>

                            <div id="dvBanco" style="float: left;">
                                <asp:UpdatePanel ID="updUcBancoUc" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                    <ContentTemplate>
                                        <asp:PlaceHolder runat="server" ID="phBanco"></asp:PlaceHolder>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            <div id="dvPlanificacion" style="float: left; margin-left: 4px;">
                                <asp:UpdatePanel ID="updUcPlanificacionUc" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                    <ContentTemplate>
                                        <asp:PlaceHolder runat="server" ID="phPlanificacion"></asp:PlaceHolder>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div style="clear: both;"></div>

                            <div id="dvAccion" style="float: left; margin-left: 7px; margin-bottom: 7px;">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div style="position: relative;">
                                            <div id="dvlblAccion" style="width: 120px; float: left; margin: 5px 2px 0px 2px;">
                                                <asp:Label ID="lblAccion" runat="server" CssClass="label2" nowrap="false"></asp:Label>
                                            </div>
                                            <div id="dvDdlAccion" style="width: 330px; float: left;">
                                                <asp:DropDownList ID="ddlAccion" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="300px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>


                            <div id="dvButton" style="float: left; text-align: right; width: 433px;">
                                <asp:Button runat="server" ID="btnBuscar" CssClass="ui-button" Width="100px" ClientIDMode="Static" />
                                <asp:Button runat="server" ID="btnLimpar" CssClass="ui-button" Width="100px" />
                            </div>

                        </div>
                    </fieldset>
                </div>
                <asp:UpdatePanel ID="UppResultados" runat="server" ChildrenAsTriggers="True" UpdateMode="Always">
                    <ContentTemplate>
                        <div id="dvSinValores" runat="server">
                            <asp:Label ID="lblSinValores" CssClass="ui-panel-title" runat="server">lblSinValores</asp:Label>
                        </div>
                        <div class="ui-panel-titlebar" id="dvSubTituloMAE" runat="server">
                            <asp:Label ID="lblSubTituloMAE" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <div style="position: relative;" id="dvResultados" runat="server">
                            <div id="dvSinRecuento" style="width: 500px; float: left; margin: 5px 2px 0px 2px;">
                                <fieldset class="ui-fieldset ui-widget ui-widget-content ui-corner-all">
                                    <legend class="ui-corner-all ui-state-default ui-state-active" style="padding: 2px 20px 2px 20px !important; color: #696969 !important;">
                                        <asp:Label ID="lblSinRecuento" runat="server">lblSinRecuento</asp:Label>
                                    </legend>
                                    <div style="position: relative; width: 100%; font-family: Arial, Verdana, sans-serif; color: #767676; font-size: 12px; font-weight: bold;">
                                        <div style="float: left; margin-left: 20px; margin-top: 5px;">
                                            <asp:Label ID="lblTotalSinRecuento" runat="server">lblTotalSinRecuento</asp:Label>
                                        </div>
                                        <div style="float: left; margin-left: 10px; margin-top: 3px; color: #696969; font-size: 14px;">
                                            <asp:Label ID="lblValorTotalSinRecuento" runat="server">0</asp:Label>
                                        </div>
                                        <div style="float: left; margin-left: 10px; margin-top: 2px;">
                                            <asp:ImageButton ID="btnTotalSinRecuento" runat="server" ImageUrl="~/App_Themes/Padrao/css/img/grid/hay_detalle.png" />
                                        </div>
                                        <div style="float: right;">
                                            <asp:Button runat="server" ID="btnAsignar" CssClass="ui-button ui-buttonDisabled" Width="100px" Enabled="false" />
                                        </div>
                                    </div>
                                    <div class="dvclear"></div>
                                    <div class="dvclear"></div>


                                    <div style="overflow: auto; height: 234px; margin-top: 10px;">

                                        <pro:ProsegurGridView ID="gvSinRecuento" runat="server" AgruparRadioButtonsPeloName="False" Ajax="True" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" ColunasSelecao="oidPtoServicio" ConfigurarNumeroRegistrosManualmente="False" EnableModelValidation="True" EstiloDestaque="GridLinhaDestaque" ExibirCabecalhoQuandoVazio="False" GerenciarControleManualmente="True" GridPadrao="False" HeaderSpanStyle="" NumeroRegistros="0" OrdenacaoAutomatica="True" PageSize="9" PaginaAtual="0" PaginacaoAutomatica="True" Width="99%">

                                            <HeaderStyle Font-Bold="True" />
                                            <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                            <RowStyle CssClass="GridLinhaPadraoImpar" />
                                            <TextBox ID="objTextoProsegurGridView2" AutoPostBack="True" MaxLength="10" Width="30px">
                                            </TextBox>
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkHeaderSinRecuento" Checked="True" onclick="javascript: SelectAll(1,this);" runat="server" Style="margin: 5px !important;" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkRow" runat="server" Checked="True" onclick="javascript: calculateTotal();" CssClass="chkSemBorda" />
                                                        <asp:HiddenField runat="server" ID="hfOidMaquina" Value='<%# Eval("oid_maquina") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" CssClass="chkSemBorda" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="chkSemBorda" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="descripcion" HeaderText="MAE" />
                                            </Columns>
                                        </pro:ProsegurGridView>

                                    </div>


                                    <%-- <div>

                                        <asp:GridView ID="gvSinRecuentoa" DataSourceID="ObjectDataSource1" runat="server" BorderStyle="None" Width="465px" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkHeader" AutoPostBack="true" OnCheckedChanged="chckchangedSinRecuento" runat="server" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkRow" runat="server" CssClass="chkSemBorda" />
                                                        <asp:HiddenField runat="server" ID="hfOidMaquina" Value='<%# Eval("oid_maquina") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" CssClass="chkSemBorda" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="chkSemBorda" />
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="descripcion" ItemStyle-HorizontalAlign="Left" ItemStyle-BackColor="White" HeaderStyle-Width="200px" HeaderText="gvDescripcion">
                                                    <HeaderStyle Width="200px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                </asp:BoundField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div style="height: auto; text-align: left; color: #767676; border-style: none; border: solid 1px #FFF; font-style: italic; font-weight: bold;">
                                                    <%# "Sem Registro" %>
                                                </div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>--%>
                                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="MaeSinRecuento" TypeName="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoAccionesEnLote" EnableViewState="false"></asp:ObjectDataSource>
                                </fieldset>
                            </div>
                            <div id="dvConRecuento" style="width: 500px; float: left; margin: 5px 2px 0px 2px;">
                                <fieldset class="ui-fieldset ui-widget ui-widget-content ui-corner-all">
                                    <legend class="ui-corner-all ui-state-default ui-state-active" style="padding: 2px 20px 2px 20px !important; color: #696969 !important;">
                                        <asp:Label ID="lblConRecuento" runat="server">lblConRecuento</asp:Label>
                                    </legend>

                                    <div style="position: relative; width: 100%; font-family: Arial, Verdana, sans-serif; color: #767676; font-size: 12px; font-weight: bold;">
                                        <div style="float: left; margin-left: 20px; margin-top: 5px;">
                                            <asp:Label ID="lblTotalConRecuento" runat="server">lblTotalConRecuento</asp:Label>
                                        </div>
                                        <div style="float: left; margin-left: 10px; margin-top: 3px; color: #696969; font-size: 14px;">
                                            <asp:Label ID="lblValorTotalConRecuento" runat="server">0</asp:Label>
                                        </div>
                                        <div style="float: left; margin-left: 10px; margin-top: 2px;">
                                            <asp:ImageButton ID="btnTotalConRecuento" runat="server" ImageUrl="~/App_Themes/Padrao/css/img/grid/hay_detalle.png" />
                                        </div>
                                        <div style="float: right;">
                                            <div class="botaoOcultar">
                                                <asp:Button runat="server" ID="btnQuitarOculto" Style="" />
                                            </div>
                                            <asp:Button runat="server" ID="btnQuitar" CssClass="ui-button ui-buttonDisabled" Width="100px" Enabled="false" />
                                        </div>
                                    </div>

                                    <div class="dvclear"></div>
                                    <div class="dvclear"></div>

                                    <div style="overflow: auto; height: 234px; margin-top: 10px;">

                                        <pro:ProsegurGridView ID="gvConRecuento" runat="server" AgruparRadioButtonsPeloName="False" Ajax="True" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" ColunasSelecao="oidPtoServicio" ConfigurarNumeroRegistrosManualmente="False" EnableModelValidation="True" EstiloDestaque="GridLinhaDestaque" ExibirCabecalhoQuandoVazio="False" GerenciarControleManualmente="True" GridPadrao="False" HeaderSpanStyle="" NumeroRegistros="0" OrdenacaoAutomatica="True" PageSize="9" PaginaAtual="0" PaginacaoAutomatica="True" Width="99%">

                                            <HeaderStyle Font-Bold="True" />
                                            <Pager ID="objPager_gvConRecuento" ItemsPerPage="9">
                                                <FirstPageButton Visible="True">
                                                    <Image Url="mvwres://Prosegur.Web, Version=3.1.1503.502, Culture=neutral, PublicKeyToken=a8a76e1d318ac1f1/Prosegur.Web.pFirst.gif">
                                                    </Image>
                                                </FirstPageButton>
                                                <LastPageButton Visible="True">
                                                </LastPageButton>
                                                <Summary Text="Página {0} de {1} ({2} itens)" />
                                            </Pager>
                                            <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                            <RowStyle CssClass="GridLinhaPadraoImpar" />
                                            <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px">
                                            </TextBox>
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkHeaderConRecuento" Checked="True" onclick="javascript: SelectAll(2, this);" runat="server" Style="margin: 5px !important;" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkRow" runat="server" onclick="javascript: calculateTotal();" CssClass="chkSemBorda" Checked="True" />
                                                        <asp:HiddenField runat="server" ID="hfOidMaquina" Value='<%# Eval("oid_maquina") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" CssClass="chkSemBorda" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="chkSemBorda" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="descripcion" HeaderText="MAE" />
                                            </Columns>
                                        </pro:ProsegurGridView>

                                    </div>
                                    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="MaeConRecuento" TypeName="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoAccionesEnLote" EnableViewState="false"></asp:ObjectDataSource>
                                </fieldset>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
</asp:Content>
