<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaMovimientosPendientesIntegracion.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaMovimientosPendientesIntegracion" 
    MasterPageFile="~/Master/Master.Master"
    %>
<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Busqueda de MAE</title>

    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <script src="JS/jquery.mask.min.js"></script>
    <link href="Css/popover.css" rel="stylesheet" />
    <script src="JS/Scripts/bootstrap.min.js"></script>
    <script type="text/javascript">
        var _fechaGMTDelegacion = null;
        var timeOut = null;
        function fecharModal() {
            window.parent.FecharModalMae();
        }
        function ocultarExibir() {
            $("#DivFiltros").toggleClass("legend-expand");
            $(".accordion").slideToggle("fast");
        };
        function ManterFiltroAberto() {
            $("#DivFiltros").addClass("legend-expand");
            $(".accordion").show();
        };

        function ActualizarData(data, info) {
            if (timeOut) {
                clearTimeout(timeOut);
            }
            _fechaGMTDelegacion = new Date(data);
            createPopover('#divTooltip', info);
            moveRelogio();
        }

        function moveRelogio() {

            if (_fechaGMTDelegacion != null) {
                _fechaGMTDelegacion.setSeconds(_fechaGMTDelegacion.getSeconds() + 1);
                var lblRelogio = document.getElementsByClassName('lblRelogio');
                //var lblRelogio = lblReloj
                if (lblRelogio)
                    lblRelogio.innerHTML = _fechaGMTDelegacion.toLocaleString();

                timeOut = setTimeout("moveRelogio()", 1000);
            }
        }

        function createPopover(item, content) {

            var $pop = $(item);
            $pop.popover({
                placement: 'top',
                trigger: 'hover',
                html: true,
                content: content
            });
            return $pop;
        };
    </script>
    <style type="text/css">
        .auto-style1 {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <asp:HiddenField runat="server" ID="hiddenCodigo" />
        <asp:UpdatePanel ID="UpdatePanelGeralFiltro" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
            <ContentTemplate>

                <div id="Filtros" style="display: block;">
                    <fieldset id="ExpandCollapseDiv" class="ui-fieldset ui-widget ui-widget-content ui-corner-all ui-fieldset-toggleable">
                        <legend class="ui-fieldset-legend ui-corner-all ui-state-default ui-state-active">
                            <div id="DivFiltros" class="legend-collapse" onclick="ocultarExibir();">
                                <asp:Label ID="lblSubTitulosCriteriosBusqueda" CssClass="legent-text" runat="server">Filtrar Clientes</asp:Label>
                            </div>
                        </legend>
                        <div class="accordion" style="display: block;">
                            <asp:UpdatePanel ID="uppDelPLa" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div style="width: inherit; height: 35px;">
                                        <div class="tamanho_celula" style="float: left; margin-left: 9px; padding-top: 5px;">
                                            <asp:Label ID="lblDelegacion" runat="server" CssClass="label2" nowrap="false"></asp:Label>
                                        </div>
                                        <div style="width: 335px; float: left; margin-left: 2px;">
                                            <asp:DropDownList ID="ddlDelegacion" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                                Width="301px">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="tamanho_celula" style="float: left; padding-top: 5px;">
                                            <asp:Label ID="lblPlanta" runat="server" CssClass="label2" nowrap="false"></asp:Label>
                                        </div>

                                        <div style="width: 335px; float: left;">
                                            <asp:DropDownList ID="ddlPlanta" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                                Width="301px">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div style="width: inherit; height: 35px;">
                                        <div class="tamanho_celula" style="float: left; margin-left: 9px; padding-top: 5px;">
                                            <asp:Label ID="lblDeviceID" runat="server" CssClass="label2"></asp:Label>
                                        </div>
                                        <div style="width: 335px; float: left; margin-left: 2px;">
                                            <asp:TextBox ID="txtDeviceID" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="291px"
                                                MaxLength="100"></asp:TextBox>
                                        </div>

                                        <div class="tamanho_celula" style="float: left; padding-top: 5px;">
                                            <asp:Label ID="lblDescripcion" runat="server" CssClass="label2"></asp:Label>
                                        </div>

                                        <div style="width: 335px; float: left;">
                                            <asp:TextBox ID="txtDescricao" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="291px"
                                                MaxLength="25"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div style="width: inherit; height: 35px;">
                                        <div class="tamanho_celula" style="float: left; margin-left: 9px; padding-top: 5px;">
                                            <asp:Label ID="lblMarca" runat="server" CssClass="label2" nowrap="false"></asp:Label>
                                        </div>
                                        <div style="width: 335px; float: left; margin-left: 2px">
                                            <asp:DropDownList ID="ddlMarca" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                                Width="301px">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="tamanho_celula" style="float: left; padding-top: 5px;">
                                            <asp:Label ID="lblModelo" runat="server" CssClass="label2" nowrap="false"></asp:Label>
                                        </div>

                                        <div style="width: 335px; float: left;">
                                            <asp:DropDownList ID="ddlModelo" runat="server" AutoPostBack="False" Enabled="false" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                                Width="301px">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
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
                    <asp:Label ID="lblSubTituloMAE" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabela_interna">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server">
                                <ContentTemplate>
                                    <pro:ProsegurGridView ID="GdvResultado" runat="server" AllowPaging="True" AllowSorting="True"
                                        ColunasSelecao="oidMaquina" EstiloDestaque="GridLinhaDestaque"
                                        GridPadrao="False" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                                        ExibirCabecalhoQuandoVazio="False" NumeroRegistros="0" OrdenacaoAutomatica="True"
                                        PaginaAtual="0" PaginacaoAutomatica="True" Width="99%"
                                        AgruparRadioButtonsPeloName="False"
                                        ConfigurarNumeroRegistrosManualmente="False" EnableModelValidation="True"
                                        HeaderSpanStyle="">
                                        <Pager ID="objPager_ProsegurGridView1">
                                            <FirstPageButton Visible="True">
                                                <Image Url="mvwres://Prosegur.Web, Version=3.1.1203.901, Culture=neutral, PublicKeyToken=a8a76e1d318ac1f1/Prosegur.Web.pFirst.gif">
                                                </Image>
                                            </FirstPageButton>
                                            <LastPageButton Visible="True">
                                            </LastPageButton>
                                            <Summary Text="Página {0} de {1} ({2} itens)" />
                                            <SummaryStyle>
                                            </SummaryStyle>
                                        </Pager>
                                        <HeaderStyle Font-Bold="True" />
                                        <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                        <RowStyle CssClass="GridLinhaPadraoImpar" />
                                        <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px">            
                                        </TextBox>
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemStyle HorizontalAlign="Center" Width="80"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditar" CssClass="imgButton" OnClick="imgEditar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultar" CssClass="imgButton" OnClick="imgConsultar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluir" CssClass="imgButton" OnClick="imgExcluir_OnClick" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CodigoCliente" HeaderText="" SortExpression="CodigoCliente" />
                                            <asp:BoundField DataField="Cliente" HeaderText="Cliente" SortExpression="Cliente" />
                                            <asp:BoundField DataField="CodigoSubCliente" HeaderText="" SortExpression="CodigoSubCliente" />
                                            <asp:BoundField DataField="SubCliente" HeaderText="SubCliente" SortExpression="SubCliente" />
                                            <asp:BoundField DataField="CodigoPtoServicio" HeaderText="" SortExpression="CodigoPtoServicio" />
                                            <asp:BoundField DataField="PtoServicio" HeaderText="PtoServicio" SortExpression="PtoServicio" />
                                            <asp:BoundField DataField="deviceID" HeaderText="Codigo" SortExpression="deviceID" />
                                            <asp:BoundField DataField="descripcion" HeaderText="Descricao" SortExpression="descripcion" />
                                            <asp:TemplateField HeaderText="vigente">
                                                <ItemStyle HorizontalAlign="Center" Width="50"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Image ID="imgVigente" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="fechaValor">
                                                <ItemStyle HorizontalAlign="Center" Width="50"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Image ID="imgFechaValor" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="desPlanificacion" HeaderText="Planificacion" SortExpression="desPlanificacion" />
                                            <asp:BoundField HeaderText="oidMaquina" Visible="False" />
                                        </Columns>
                                    </pro:ProsegurGridView>
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
                    <asp:Panel runat="server" ID="pnForm" Visible="False">
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloForm" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <div style="width: inherit; height: 110px;">
                            <div style="width: 839px; float: left;">
                                <div style="width: inherit; height: 35px;">
                                    <div class="tamanho_celula" style="float: left; margin-left: 9px; padding-top: 5px;">
                                        <asp:Label ID="lblDelegacionForm" runat="server" CssClass="label2"></asp:Label>
                                    </div>
                                    <div style="width: 335px; float: left; margin-left: 2px;">
                                        <asp:DropDownList ID="ddlDelegacionForm" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                            Width="225px" />
                                        <asp:CustomValidator ID="csvDelegacionForm" runat="server"
                                            ControlToValidate="ddlDelegacionForm" Display="Dynamic" ErrorMessage="" Text="*"></asp:CustomValidator>
                                    </div>

                                    <div class="tamanho_celula" style="float: left; padding-top: 5px;">
                                        <asp:Label ID="lblPlantaForm" runat="server" CssClass="label2"></asp:Label>
                                    </div>

                                    <div style="width: 230px; float: left;">
                                        <asp:DropDownList ID="ddlPlantaForm" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                            Width="225px" />
                                        <asp:CustomValidator ID="csvPlantaForm" runat="server"
                                            ControlToValidate="ddlPlantaForm" Display="Dynamic" ErrorMessage="" Text="*"></asp:CustomValidator>
                                    </div>
                                </div>


                                <div style="width: inherit; height: 35px;">
                                    <div class="tamanho_celula" style="float: left; margin-left: 9px; padding-top: 5px;">
                                        <asp:Label ID="lblDeviceIDForm" runat="server" CssClass="label2"></asp:Label>
                                    </div>
                                    <div style="width: 335px; float: left; margin-left: 2px;">
                                        <asp:TextBox ID="txtDeviceIDForm" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="215px"
                                            MaxLength="100"></asp:TextBox>
                                    </div>

                                    <div class="tamanho_celula" style="float: left; padding-top: 5px;">
                                        <asp:Label ID="lblDescripcionForm" runat="server" CssClass="label2"></asp:Label>
                                    </div>

                                    <div style="width: 230px; float: left;">
                                        <asp:TextBox ID="txtDescricaoForm" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="215px"
                                            MaxLength="100"></asp:TextBox>
                                        <asp:CustomValidator ID="csvDescricaoForm" runat="server"
                                            ControlToValidate="txtDescricaoForm" Display="Dynamic" ErrorMessage="" Text="*"></asp:CustomValidator>
                                    </div>
                                </div>


                                <div style="width: inherit; height: 35px;">
                                    <div class="tamanho_celula" style="float: left; margin-left: 9px; padding-top: 5px;">
                                        <asp:Label ID="lblMarcaForm" runat="server" CssClass="label2" nowrap="false"></asp:Label>
                                    </div>
                                    <div style="width: 335px; float: left; margin-left: 2px;">
                                        <asp:DropDownList ID="ddlMarcaForm" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                            Width="225px">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="csvMarcaForm" runat="server"
                                            ControlToValidate="ddlMarcaForm" Display="Dynamic" ErrorMessage="" Text="*"></asp:CustomValidator>
                                    </div>

                                    <div class="tamanho_celula" style="float: left; padding-top: 5px;">
                                        <asp:Label ID="lblModeloForm" runat="server" CssClass="label2" nowrap="false"></asp:Label>
                                    </div>

                                    <div style="width: 230px; float: left;">
                                        <asp:DropDownList ID="ddlModeloForm" runat="server" AutoPostBack="False" Enabled="false" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                            Width="225px">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="csvModeloForm" runat="server"
                                            ControlToValidate="ddlModeloForm" Display="Dynamic" ErrorMessage="" Text="*"></asp:CustomValidator>
                                    </div>
                                </div>

                            </div>
                            <div style="width: 300px; float: left;">
                                <div>

                                    <span>
                                        <asp:CheckBox ID="chkVigenteForm" runat="server" Checked="true" Enabled="false" />
                                    </span>
                                    <asp:Label ID="lblVigenteForm" runat="server" Enabled="true" CssClass="label2"></asp:Label>
                                </div>
                                <div>
                                    <span>
                                        <asp:CheckBox ID="chkConsideraRecuentos" runat="server" Checked="true" Enabled="false" />
                                    </span>
                                    <asp:Label ID="lblConsideraRecuentos" runat="server" Enabled="true" CssClass="label2"></asp:Label>
                                </div>
                                <div>
                                    <span>
                                        <asp:CheckBox ID="chkMultiClientes" runat="server" Checked="true" Enabled="true" AutoPostBack="True" />
                                    </span>
                                    <asp:Label ID="lblMultiClientes" runat="server" Enabled="true" CssClass="label2"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloPtoServ" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <div id="divMonocliente" runat="server">
                            <div runat="server" style="height: 30px;" id="divDesplazado">
                                <div style="margin-left: 7px; float: left;" class="ui-icon ui-icon-alert"></div>
                                <asp:Label ID="lblDesplazado" Style="float: left;" CssClass="ui-panel-title" runat="server"></asp:Label>
                            </div>
                            <div style="float: left; max-width: 80%;">
                                <asp:UpdatePanel ID="updUcClientesPtoServMono" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                    <ContentTemplate>
                                        <asp:Panel runat="server" ID="pnUcClientesPtoServMono" Enabled="False">
                                            <asp:PlaceHolder runat="server" ID="phClientesPtoServMono"></asp:PlaceHolder>
                                        </asp:Panel>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <asp:ImageButton ID="imgCodigoAjenoMono" runat="server" CssClass="imgButton" OnClick="imgCodigoAjenoMono_OnClick" />
                            <asp:ImageButton ID="imgBancoCapital" runat="server" CssClass="imgButton" ImageUrl="App_Themes/Padrao/css/img/iconos/institution.png" OnClick="imgCapitalDBancariosForm_OnClick" />
                            <br>
                        </div>
                        <div class="dvclear"></div>
                        <div id="divMulticliente" runat="server">
                            <div id="divAddPtoServ" runat="server">
                                <asp:UpdatePanel ID="updUcClientesPtoServ" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                    <ContentTemplate>
                                        <asp:PlaceHolder runat="server" ID="phClientesPtoServ"></asp:PlaceHolder>


                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button runat="server" ID="btnAddPtoServ" CssClass="ui-button" Width="100px" />
                                            <asp:CustomValidator ID="csvAddPtoServ" runat="server"
                                                Display="Dynamic" ErrorMessage="" Text="*"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="text-align: center;">
                                <dx:ASPxGridView ID="grid" runat="server"
                                    KeyFieldName="oidPtoServicio" Width="99%" AutoGenerateColumns="False">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="cliente" VisibleIndex="0">
                                            <Settings AllowDragDrop="True" />
                                            <CellStyle HorizontalAlign="Left">
                                            </CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="PorcComisionCliente" VisibleIndex="1">
                                            <Settings AllowDragDrop="True" />
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="subCliente" VisibleIndex="2">
                                            <Settings AllowDragDrop="True" />
                                            <CellStyle HorizontalAlign="Left">
                                            </CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="PtoServicio" VisibleIndex="3">
                                            <Settings AllowDragDrop="True" />
                                            <CellStyle HorizontalAlign="Left">
                                            </CellStyle>
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataColumn VisibleIndex="4" CellStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:ImageButton runat="server" ImageUrl="~/Imagenes/contain01.png" ID="imgVigenteForm" CssClass="imgButton" OnCommand="imgCodigoAjeno2_OnClick" CommandArgument='<%# Container.KeyValue %>' Visible='<%# DataBinder.Eval(Container.DataItem, "completo") %>' />
                                                <asp:ImageButton runat="server" ImageUrl="~/Imagenes/nocontain01.png" ID="imgNoVigenteForm" CssClass="imgButton" OnCommand="imgCodigoAjeno2_OnClick" CommandArgument='<%# Container.KeyValue %>' Visible='<%# Not DataBinder.Eval(Container.DataItem, "completo") %>' />
                                            </DataItemTemplate>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx:GridViewDataColumn>

                                        <dx:GridViewDataColumn VisibleIndex="5" CellStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/iconos/institution.png" ID="imgDatosBancariosForm" CssClass="imgButton" OnCommand="imgDatosBancariosForm_OnClick" CommandArgument='<%# Container.KeyValue %>' />
                                            </DataItemTemplate>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx:GridViewDataColumn>


                                        <dx:GridViewDataColumn VisibleIndex="6" CellStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluirForm" CssClass="imgButton" OnCommand="imgExcluirForm_OnClick" CommandArgument='<%# Container.KeyValue %>' />
                                            </DataItemTemplate>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="true" />
                                    <SettingsPager PageSize="20">

                                        <PageSizeItemSettings Visible="true" />
                                    </SettingsPager>
                                </dx:ASPxGridView>
                            </div>


                        </div>
                        <br />
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloPlanificacion" CssClass="ui-panel-title" runat="server" Text="Planificacines"></asp:Label>
                        </div>
                        <div style="width: inherit; height: 145px;">
                            <div style="width: inherit; height: 35px;">
                                <div class="tamanho_celula" style="float: left; margin-left: 9px; padding-top: 5px;">
                                    <asp:Label ID="lblTipoPlanificacion" runat="server" CssClass="label2" nowrap="false"></asp:Label>
                                </div>
                                <div style="width: 380px; float: left; margin-left: 2px;">
                                    <asp:DropDownList ID="ddlTipoPlanificacion" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                        Width="225px">
                                    </asp:DropDownList>
                                </div>
                                <div style="background-color: Window; width: 500px; float: left; height: 30px;">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                        <ContentTemplate>
                                            <asp:Panel runat="server" ID="pnUcBancoform" Enabled="False">
                                                <asp:PlaceHolder runat="server" ID="phBanco"></asp:PlaceHolder>

                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <div style="width: inherit; height: 35px;">
                                <div class="tamanho_celula" style="float: left; margin-left: 9px; padding-top: 5px;">
                                    <asp:Label ID="lblPlanificacion" runat="server" CssClass="label2"></asp:Label>
                                </div>
                                <div style="width: 380px; float: left; margin-left: 2px;">

                                    <div style="float: left;">
                                        <asp:TextBox ID="txtPlanificacion" Enabled="false" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="215px"
                                            MaxLength="100"></asp:TextBox>
                                        <asp:Button runat="server" ID="btnAddPlanificacion" CssClass="ui-button" Width="145px" Style="margin-left: 5px;" Enabled="false" Visible="false" />
                                        <asp:CustomValidator ID="csvAddPlanificacion" runat="server"
                                            Display="Dynamic" ErrorMessage="" Text="*"></asp:CustomValidator>
                                    </div>
                                    <div class="div_relogio">
                                        <div class="div_relog_label">
                                            <span class="lblRelogio" id="lblRelogio"></span>
                                        </div>
                                    </div>
                                    <a id="divTooltip" data-toggle="popover">
                                        <img src="Imagenes/help_parametros.png" style="margin-top: 5px !important;" />
                                    </a>
                                </div>

                                <div class="tamanho_celula" style="float: left; margin-left: 9px; padding-top: 5px;">
                                    <asp:Label ID="lblVigentePlan" runat="server" Enabled="true" CssClass="label2"></asp:Label>
                                </div>

                                <div style="float: left; padding-top: 5px;">
                                    <asp:CheckBox CssClass="teste1" ID="chkVigentePlan" runat="server" Enabled="false" Style="margin-left: 3px !important;" />
                                </div>
                            </div>


                            <div style="width: inherit; height: 35px;">
                                <div class="tamanho_celula" style="float: left; margin-left: 9px;">
                                    <asp:Label ID="lblFechaInicio" runat="server" CssClass="label2"></asp:Label>
                                </div>
                                <div style="width: 380px; float: left; margin-left: 2px;">
                                    <div style="position: relative">
                                        <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="130" Enabled="false"></asp:TextBox>
                                        <div id="dvFechaInicio" runat="server" style="width: 180px; height: 20px; position: absolute; bottom: 3px;"></div>
                                        <asp:CustomValidator ID="csvFechaInicio" runat="server"
                                            ControlToValidate="txtFechaInicio" Display="Dynamic" ErrorMessage="" Text="*"></asp:CustomValidator>
                                    </div>
                                </div>
                                <div class="tamanho_celula" style="float: left; margin-left: 9px; padding-top: 5px;">
                                    <asp:Label ID="lblControlaFacturacion" runat="server" Enabled="true" CssClass="label2">Controla Facturacion?</asp:Label>
                                </div>

                                <div style="float: left; padding-top: 5px;">
                                    <asp:CheckBox ID="chkControlaFacturacion" runat="server" Enabled="false" Style="margin-left: 3px !important;" />
                                </div>
                            </div>


                            <div style="width: inherit; height: 35px;">
                                <div class="tamanho_celula" style="float: left; margin-left: 9px;">
                                    <asp:Label ID="lblFechaFin" runat="server" CssClass="label2"></asp:Label>
                                </div>
                                <div style="width: 380px; float: left; margin-left: 2px;">
                                    <div style="position: relative">
                                        <asp:TextBox ID="txtFechaFin" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="130" Enabled="false"></asp:TextBox>
                                        <div id="dvFechaFin" runat="server" style="width: 180px; height: 20px; position: absolute; bottom: 3px;"></div>
                                        <asp:CustomValidator ID="csvFechaFin" runat="server"
                                            ControlToValidate="txtFechaFin" Display="Dynamic" ErrorMessage="" Text="*"></asp:CustomValidator>
                                    </div>
                                </div>

                                <div style="width: 160px; float: left; margin-left: 9px;">
                                    <asp:Button runat="server" ID="btnTransacciones" CssClass="ui-button" Width="145px" Style="" Text="Transacciones" />
                                </div>
                            </div>
                        </div>

                        <div runat="server" id="divPlanificacionPatron">
                            <div class="ui-panel-titlebar">
                                <asp:Label ID="lblTituloPlanificacionPatron" CssClass="ui-panel-title" runat="server" Text="Planificación Patron"></asp:Label>
                            </div>

                            <div style="width: inherit; height: 140px;">
                                <asp:UpdatePanel ID="upCanalesPatron" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                    <ContentTemplate>
                                        <div class="tamanho_celula" style="float: left; margin-left: 9px; padding-top: 5px;">
                                            <asp:Label ID="lblCanalPatron" runat="server" CssClass="label2" nowrap="false" Text="Canal"></asp:Label>
                                        </div>

                                        <div style="width: 380px; float: left; margin-left: 2px;">
                                            <asp:ListBox ID="lboxCanalPatron" runat="server" Enabled="false" Width="250px"></asp:ListBox>
                                        </div>

                                        <div class="tamanho_celula" style="float: left; margin-left: 9px; padding-top: 5px;">
                                            <asp:Label ID="lblSubCanalPatron" runat="server" CssClass="label2" nowrap="false" Text="SubCanal"></asp:Label>
                                        </div>

                                        <div style="width: 380px; float: left; margin-left: 2px;">
                                            <asp:ListBox ID="lboxSubCanalPatron" runat="server" Enabled="false" Width="250px"></asp:ListBox>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div runat="server" id="divPlanificacionMAE">
                            <div class="ui-panel-titlebar">
                                <asp:Label ID="lblTituloPlanificacionMAE" CssClass="ui-panel-title" runat="server" Text="Planificación MAE"></asp:Label>
                            </div>

                            <div>




                                <table>
                                    <tr>
                                        <td style="width: 450px;">
                                            <asp:UpdatePanel ID="updUcPuntoServicioUc" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                                <ContentTemplate>
                                                    <asp:PlaceHolder runat="server" ID="phPuntoServicio"></asp:PlaceHolder>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:UpdatePanel ID="upCanales" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Panel runat="server" ID="pnUcCanales" Enabled="True">
                                                        <asp:PlaceHolder runat="server" ID="phCanal"></asp:PlaceHolder>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button runat="server" ID="btnAgregarPlanificacionMAE" CssClass="ui-button" Width="145px" Style="" Text="Añadir" />&nbsp;
                                            <asp:Button runat="server" ID="btnCancelarPlanificacionMAE" CssClass="ui-button" Width="145px" Style="" Text="Cancelar edición" />
                                        </td>
                                    </tr>
                                </table>




                                <table class="tabela_interna" style="margin-top: 10px;">
                                    <tr>

                                        <td align="center">



                                            <div style="text-align: center;">
                                                <dx:ASPxGridView ID="gridPlanes" KeyFieldName="Indice" runat="server"
                                                    Width="99%" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn  FieldName="DisplayPuntos" VisibleIndex="0">
                                                            <Settings AllowDragDrop="False" />
                                                            <CellStyle HorizontalAlign="Left">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn  FieldName="DisplayCanales" VisibleIndex="2">
                                                            <Settings AllowDragDrop="False" />
                                                            <CellStyle HorizontalAlign="Left">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="DisplaySubCanales"  VisibleIndex="3">
                                                            <Settings AllowDragDrop="False" />
                                                            <CellStyle HorizontalAlign="Left">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataColumn VisibleIndex="4" CellStyle-HorizontalAlign="Center" >
                                                            <DataItemTemplate>
                                                                <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditarForm" CssClass="imgButton" OnCommand="imgEditarPLAN" CommandArgument='<%# Container.KeyValue %>' />

                                                            </DataItemTemplate>
                                                            <Settings AllowDragDrop="False" />
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn VisibleIndex="5" CellStyle-HorizontalAlign="Center">
                                                            <DataItemTemplate>
                                                                <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgQuitarForm" CssClass="imgButton" OnCommand="imgQuitarPLAN" CommandArgument='<%# Container.KeyValue %>' />

                                                            </DataItemTemplate>
                                                            <Settings AllowDragDrop="False" />
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dx:GridViewDataColumn>


                                                    </Columns>
                                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="true" />
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" />
                                                    <SettingsPager PageSize="20">

                                                        <PageSizeItemSettings Visible="true" />
                                                    </SettingsPager>
                                                </dx:ASPxGridView>
                                            </div>



                                        </td>
                                    </tr>
                                </table>





                            </div>
                        </div>
                        <div runat="server" id="divFacturacion">
                                <div class="ui-panel-titlebar" >
                                <asp:Label ID="lblFacturacion" CssClass="ui-panel-title" runat="server" Text="lblFacturacion"></asp:Label>
                            </div>

                            <div style="width: inherit; height: 140px;">
                                <div style="width: inherit; height: 35px;">
                                    <div class="tamanho_celula" style="float: left; margin-left: 9px; padding-top: 5px;">
                                        <asp:Label ID="lblBancoTesoreriaPadron" runat="server" CssClass="label2" nowrap="false">lblBancoTesoreriaPadron</asp:Label>
                                    </div>
                                    <div style="width: 450px; float: left; margin-left: 2px;">
                                        <asp:TextBox ID="txtBancoTesoreriaPadron" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="400px" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="tamanho_celula" style="float: left; margin-left: 9px; padding-top: 5px;">
                                        <asp:Label ID="lblCuentaTesoreriaPadron" runat="server" CssClass="label2" nowrap="false">lblCuentaTesoreriaPadron</asp:Label>
                                    </div>
                                    <div style="width: 450px; float: left; margin-left: 2px;">
                                        <asp:TextBox ID="txtCuentaTesoreriaPadron" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="400px" Enabled="false"></asp:TextBox>
                                    </div>

                                </div>

                                <div style="width: inherit; height: 35px;">
                                    <div style="background-color: Window; float: left; margin-left: -250px; height: 30px;">

                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                            <ContentTemplate>
                                                <asp:Panel runat="server" ID="pnUcClientesTesoreria" Enabled="False">
                                                    <asp:PlaceHolder runat="server" ID="phClientesTesoreria"></asp:PlaceHolder>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>


                                <div style="width: inherit; height: 35px;">
                                    <div class="tamanho_celula" style="float: left; margin-left: 9px;">
                                        <asp:Label ID="lblPorComisionMaquina" runat="server" CssClass="label2">lblPorComisionMaquina</asp:Label>
                                    </div>
                                    <div style="width: 200px; float: left; margin-left: 2px;">
                                        <div style="position: relative">
                                            <asp:TextBox ID="txtPorComisionMaquina" onkeypress="return bloqueialetrasAceitaVirgulaPunto(event,this);" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="130"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="tamanho_celula" style="float: left; margin-left: 9px;">
                                        <asp:Label ID="lblPorComisionPlanificacion" runat="server" CssClass="label2">lblPorComisionPlanificacion</asp:Label>
                                    </div>
                                    <div style="width: 200px; float: left; margin-left: 2px;">
                                        <div style="position: relative">
                                            <asp:TextBox ID="txtPorComisionPlanificacion" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="130" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div runat="server" id="divPorComisionCliente">
                                        <div class="tamanho_celula" style="float: left; margin-left: 9px;">
                                            <asp:Label ID="lblPorComisionCliente" runat="server" CssClass="label2">lblPorComisionCliente</asp:Label>
                                        </div>
                                        <div style="width: 200px; float: left; margin-left: 2px;">
                                            <div style="position: relative">
                                                <asp:TextBox ID="txtPorComisionCliente" onkeypress="return bloqueialetrasAceitaVirgulaPunto(event,this);" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="130" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        



                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
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

<asp:Content runat="server" ContentPlaceHolderID="cphBotonesRodapie">
    <center>
        <table>
            <tr align="center">
                <td>
                    <asp:Button runat="server" ID="btnNovo" CssClass="btn-novo" />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnGrabar" CssClass="btn-salvar" />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnBajaConfirm" CssClass="btn-excluir" />
                    <div class="botaoOcultar">
                        <asp:Button runat="server" ID="btnBaja" CssClass="btn-excluir" />

                        <asp:Button runat="server" ID="btnConsomeCodigoAjeno" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnCamposExtras" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnConsomePlanificacion" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnValidarDeviceID" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnConsomeGrabar" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnConsomeCancelar" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnConsomeBaja" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnConsomeNovo" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnConsomeAddPtoServ" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnConsomeFechaValor" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnConsomeTransacciones" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnAlertaSim" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnAlertaNao" CssClass="btn-excluir" />
                        <asp:Button runat="server" ID="btnHabilitarDeshabilitarPlanificacion" CssClass="btn-excluir" />
                    </div>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn_cancelar" OnClientClick="fecharModal();" />
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
