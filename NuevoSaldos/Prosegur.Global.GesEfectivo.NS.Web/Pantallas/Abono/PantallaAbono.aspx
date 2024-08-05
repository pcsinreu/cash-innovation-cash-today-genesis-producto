<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="PantallaAbono.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.PantallaAbono" %>

<%@ Import Namespace="Prosegur.Framework.Dicionario" %>
<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Src="~/Controles/ucCadastroDatosBancarios.ascx" TagPrefix="ns" TagName="ucCadastroDatosBancarios" %>
<%@ Register Src="~/Controles/ucAbonoDatosDocPases.ascx" TagPrefix="ns" TagName="ucAbonoDatosDocPases" %>
<%@ Register Src="~/Controles/ucAbonoDetalle.ascx" TagPrefix="ns" TagName="ucAbonoDetalle" %>
<%@ Register Src="~/Controles/ucAbonoFiltro.ascx" TagPrefix="ns" TagName="ucAbonoFiltro" %>
<%@ Register Src="~/Controles/ucBusquedaAvanzada.ascx" TagPrefix="ns" TagName="ucBusquedaAvanzada" %>
<%@ Register Src="~/Controles/ucBusqueda.ascx" TagPrefix="ns" TagName="ucBusqueda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery.paginate.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Abono/Tipos.js")%>"></script> 
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Abono/Comun.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Abono/CadastroDatosBancariosVM.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Abono/PantallaDetalle.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Abono/PantallaAbonoVM.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Abono/PantallaAbono.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Abono/Busqueda.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Abono/DatosGeneracionDocumentoPases.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="content" data-bind='with: Abono'>
        <div>
            <div class="pnl_title">
                <span><%= Tradutor.Traduzir("071_Abono_Titulo_TituloDefinicionProceso")%></span>
                <input type="button" class="butonFiltroBusqueda" disabled="disabled" />
            </div>
            <div class="dvclear"></div>
            <div class="dvForm">
                <div>
                    <span><%= Tradutor.Traduzir("071_Comon_Campo_TipoAbono")%></span><br>
                    <select id="cbTipoAbono" data-bind='options: $root.ListaTiposAbono, optionsText: "Descripcion", optionsValue: "Codigo", value: TipoAbono'></select>
                </div>
                <div data-bind="visible: (TipoAbono() != 'NoDefinido')">
                    <div data-bind="visible: (TipoAbono() == 'Pedido')">
                        <ns:ucBusqueda runat="server" ID="UcClienteSolicitante" Tipo="Cliente" Titulo="" EsMulti="False" VisibilidadInicial="True" UtilitarioBusca="BuscaBancos" />
                    </div>
                    <div data-bind="visible: (TipoAbono() != 'Pedido')">
                        <ns:ucBusqueda runat="server" ID="UcBancoSolicitante" Tipo="Banco" Titulo="" EsMulti="False" VisibilidadInicial="True" UtilitarioBusca="BuscaBancos" />
                    </div>
                    <ns:ucBusquedaAvanzada runat="server" ID="ucBusquedaAvanzada" />
                    <div data-bind='visible: (TipoAbono() == "Elemento")' style="display:none;">
                        <span><%= Tradutor.Traduzir("071_Comon_Campo_Valores")%></span><br>
                        <select data-bind='options: $root.ListaValoresEfectivo, optionsText: "Descripcion", optionsValue: "Codigo", value: TipoValor'></select>
                    </div>
                    <div data-bind='visible: ((TipoAbono() == "Saldos") || (TipoAbono() == "Pedido"))' style="display:none;">
                        <br />
                        <input type="checkbox" data-bind='checked: CrearDocumentoPases' />
                        <span><%= Tradutor.Traduzir("071_Abono_Campo_HacerDocumentoPases")%></span><br>
                    </div>
                </div>
                <div>
                    <span><%= Tradutor.Traduzir("071_Comon_Campo_Estado")%></span><br>
                    <input type="text" data-bind='value: CodigoEstado' class="form-control" readonly style="background-color:#efefef;" />
                </div>
            </div>
        </div>
        <div class="dvclear"></div>
        <div id="dvgridsAbono" data-bind='visible: (TipoAbono() != "NoDefinido")' style="display:none;">
            <div>
                <div class="pnl_title">
                    <span><%= Tradutor.Traduzir("071_Abono_Titulo_TituloResultadoFiltro")%></span>
                    <!-- ko if: $root.PermiteFiltrar -->
                    <input type="button" class="butonFiltroBusqueda" data-bind="click: $root.FiltrarElementos" />
                    <!-- /ko -->
                    <!-- ko ifnot: $root.PermiteFiltrar -->
                    <img src="../../Imagenes/LupaOff.png" />
                    <!-- /ko -->
                    <!-- ko if: $root.Filtro() -->
                    <span data-bind="text: $root.Filtro().Resumo, attr: { title: $root.Filtro().ResumoCompleto }" class="claro"></span>
                    <!-- /ko -->
                </div>
                <div class="gridAbono">
                    <table>
                        <thead>
                            <tr>
                                <th data-bind='visible: (TipoAbono() == "Elemento"), click: function () { $root.OrdenadorResultadoFiltros.Ordenar($root.OrdenadorResultadoFiltros.ColunaNumExterno); }' style="width: 15%;">
                                    <%= Tradutor.Traduzir("071_Abono_Grid_TituloColumna_NumExterno")%>
                                    <img data-bind='visible: ($root.OrdenadorResultadoFiltros.ColunaNumExterno.SortOrder() == 0)' src="../../Imagenes/ico_sort_asc.png" style="float: right" alt="ASC" />
                                    <img data-bind='visible: ($root.OrdenadorResultadoFiltros.ColunaNumExterno.SortOrder() == 1)' src="../../Imagenes/ico_sort_desc.png" style="float: right" alt="DESC" />
                                </th>
                                <th data-bind='visible: ((TipoAbono() == "Saldos") || (TipoAbono() == "Pedido")), click: function () { $root.OrdenadorResultadoFiltros.Ordenar($root.OrdenadorResultadoFiltros.ColunaSubCanal); }' style="width: 15%;">
                                    <%= Tradutor.Traduzir("071_Abono_Grid_TituloColumna_SubCanal")%>
                                    <img data-bind='visible: ($root.OrdenadorResultadoFiltros.ColunaSubCanal.SortOrder() == 0)' src="../../Imagenes/ico_sort_asc.png" style="float: right" alt="ASC" />
                                    <img data-bind='visible: ($root.OrdenadorResultadoFiltros.ColunaSubCanal.SortOrder() == 1)' src="../../Imagenes/ico_sort_desc.png" style="float: right" alt="DESC" />
                                </th>
                                <th data-bind='click: function () { $root.OrdenadorResultadoFiltros.Ordenar($root.OrdenadorResultadoFiltros.ColunaCliente); }' style="width: 60%;">
                                    <span data-bind="text: $root.LabelGridResultadoFiltroCliente"></span>
                                    <img data-bind='visible: ($root.OrdenadorResultadoFiltros.ColunaCliente.SortOrder() == 0)' src="../../Imagenes/ico_sort_asc.png" style="float: right" alt="ASC" />
                                    <img data-bind='visible: ($root.OrdenadorResultadoFiltros.ColunaCliente.SortOrder() == 1)' src="../../Imagenes/ico_sort_desc.png" style="float: right" alt="DESC" />
                                </th>
                                <th data-bind='click: function () { $root.OrdenadorResultadoFiltros.Ordenar($root.OrdenadorResultadoFiltros.ColunaValor); }' style="width: 20%;">
                                    <%= Tradutor.Traduzir("071_Abono_Grid_TituloColumna_Valor")%>
                                    <img data-bind='visible: ($root.OrdenadorResultadoFiltros.ColunaValor.SortOrder() == 0)' src="../../Imagenes/ico_sort_asc.png" style="float: right" alt="ASC" />
                                    <img data-bind='visible: ($root.OrdenadorResultadoFiltros.ColunaValor.SortOrder() == 1)' src="../../Imagenes/ico_sort_desc.png" style="float: right" alt="DESC" />
                                </th>
                                <!-- Não apagar commentário abaixo. Código pode ser reutilizado -->
                                <th style="width: 5%; text-align: center;">
                                    <!--<input type="button" data-bind='click: $root.VincularTodosAbonos' class="butonResultadoFiltroPequeno butonResultadoFiltroSeleccionar" />-->
                                    <input type="button" class="butonResultadoFiltroPequeno " />
                                </th>
                            </tr>
                        </thead>
                        <tbody data-bind='foreach: $root.ListaResultadoFiltro'>
                            <tr style="background-color:yellow">
                                <td data-bind='visible: ($root.Abono().TipoAbono() == "Elemento")' style="width: 15%; text-align: center;"><span data-bind='text: AbonoElemento().CodigoElemento'></span></td>
                                <td data-bind="visible: (($root.Abono().TipoAbono() == 'Saldos') || ($root.Abono().TipoAbono() == 'Pedido')), backGroundColor: DivisaContieneResto() ? '#FFFA84' : ''" style="width: 15%; text-align: center;"><span data-bind='    text: AbonoSaldo().SubCanal().Descripcion'></span></td>
                                <td style="width: 60%;" data-bind="backGroundColor: DivisaContieneResto() ? '#FFFA84' : '' ">
                                    <span data-bind='text: InfoCliente'></span>
                                    <img data-bind='visible: MultiplesCuentas' src="../../Imagenes/ico_multiples.png" style="float: right" alt="Múltiples Cuentas" />
                                </td>
                                <td style="width: 20%;text-align:right;" data-bind="backGroundColor: DivisaContieneResto() ? '#FFFA84' : '' ">
                                    <!-- ko if: $root.Abono().TipoAbono() == "Elemento" -->
                                    <span data-bind='visible: ($root.Abono().TipoAbono() == "Elemento"), text: IncluirFormato(AbonoElemento().Divisa().CodigoISO(), AbonoElemento().Importe()), style: { color: AbonoElemento().Divisa().ColorHTML }'></span>
                                    <!-- /ko -->
                                    <!-- ko if: (($root.Abono().TipoAbono() == "Saldos") || ($root.Abono().TipoAbono() == "Pedido")) -->
                                    <span data-bind='visible: (($root.Abono().TipoAbono() == "Saldos") || ($root.Abono().TipoAbono() == "Pedido")), text: IncluirFormato(AbonoSaldo().Divisa().CodigoISO(), AbonoSaldo().Importe()), style: { color: AbonoSaldo().Divisa().ColorHTML }'></span>
                                    <!-- /ko -->
                                </td>
                                <td style="width: 5%; text-align: center;" data-bind="backGroundColor: DivisaContieneResto() ? '#FFFA84' : '' ">
                                    <input type="button" data-bind="click: $root.VincularAbono" class="butonResultadoFiltroPequeno butonResultadoFiltroSeleccionar" /></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div style="padding-left: 24px;">
                <div class="pnl_title">
                    <span data-bind='visible: ((TipoAbono() == "Saldos") || (TipoAbono() == "Pedido"))'><%= Tradutor.Traduzir("071_Abono_Titulo_TituloSaldosAbonados")%></span>
                    <span data-bind='visible: (TipoAbono() == "Elemento")'><%= Tradutor.Traduzir("071_Abono_Titulo_TituloElementosAbonados")%></span>
                    <input type="button" class="butonFiltroBusqueda" disabled="disabled" />
                </div>
                <div class="gridAbono">
                    <table>
                        <thead>
                            <tr>
                                <!-- Não apagar commentário abaixo. Código pode ser reutilizado -->
                                <th style="width: 5%;">
                                    <!--<input type="button" data-bind='click: $root.EliminarTodosAbonos' class="butonResultadoFiltroPequeno butonResultadoFiltroEliminar" />-->
                                    <input type="button" class="butonResultadoFiltroPequeno" disabled/>
                                </th> 
                                <th data-bind='visible: (TipoAbono() == "Elemento"), click: function () { $root.OrdenadorAbonos.Ordenar($root.OrdenadorAbonos.ColunaNumExterno); }' style="width: 15%;">
                                    <%= Tradutor.Traduzir("071_Abono_Grid_TituloColumna_NumExterno")%>
                                    <img data-bind='visible: ($root.OrdenadorAbonos.ColunaNumExterno.SortOrder() == 0)' src="../../Imagenes/ico_sort_asc.png" style="float: right" alt="ASC" />
                                    <img data-bind='visible: ($root.OrdenadorAbonos.ColunaNumExterno.SortOrder() == 1)' src="../../Imagenes/ico_sort_desc.png" style="float: right" alt="DESC" />
                                </th>
                                <th data-bind='click: function () { $root.OrdenadorAbonos.Ordenar($root.OrdenadorAbonos.ColunaCliente); }' style="width: 50%;">
                                    <span data-bind="text: $root.LabelGridAbonosCliente"></span>
                                    <img data-bind='visible: ($root.OrdenadorAbonos.ColunaCliente.SortOrder() == 0)' src="../../Imagenes/ico_sort_asc.png" style="float: right" alt="ASC" />
                                    <img data-bind='visible: ($root.OrdenadorAbonos.ColunaCliente.SortOrder() == 1)' src="../../Imagenes/ico_sort_desc.png" style="float: right" alt="DESC" />
                                </th>
                                <th data-bind='click: function () { $root.OrdenadorAbonos.Ordenar($root.OrdenadorAbonos.ColunaBanco); }' style="width: 15%;">
                                    <%= Tradutor.Traduzir("071_Abono_Grid_TituloColumna_Banco")%>
                                    <img data-bind='visible: ($root.OrdenadorAbonos.ColunaBanco.SortOrder() == 0)' src="../../Imagenes/ico_sort_asc.png" style="float: right" alt="ASC" />
                                    <img data-bind='visible: ($root.OrdenadorAbonos.ColunaBanco.SortOrder() == 1)' src="../../Imagenes/ico_sort_desc.png" style="float: right" alt="DESC" />
                                </th>
                                <th data-bind='click: function () { $root.OrdenadorAbonos.Ordenar($root.OrdenadorAbonos.ColunaCuenta); }' style="width: 15%;">
                                    <%= Tradutor.Traduzir("071_Abono_Grid_TituloColumna_Cuenta")%>
                                    <img data-bind='visible: ($root.OrdenadorAbonos.ColunaCuenta.SortOrder() == 0)' src="../../Imagenes/ico_sort_asc.png" style="float: right" alt="ASC" />
                                    <img data-bind='visible: ($root.OrdenadorAbonos.ColunaCuenta.SortOrder() == 1)' src="../../Imagenes/ico_sort_desc.png" style="float: right" alt="DESC" />
                                </th>
                                <th data-bind='click: function () { $root.OrdenadorAbonos.Ordenar($root.OrdenadorAbonos.ColunaValor); }' style="width: 25%;">
                                    <%= Tradutor.Traduzir("071_Abono_Grid_TituloColumna_Valor")%>
                                    <img data-bind='visible: ($root.OrdenadorAbonos.ColunaValor.SortOrder() == 0)' src="../../Imagenes/ico_sort_asc.png" style="float: right" alt="ASC" />
                                    <img data-bind='visible: ($root.OrdenadorAbonos.ColunaValor.SortOrder() == 1)' src="../../Imagenes/ico_sort_desc.png" style="float: right" alt="DESC" />
                                </th>
                                <th style="width: 5%;"></th>
                            </tr>
                        </thead>
                        <tbody data-bind='foreach: AbonosValor'>
                            <tr>
                                <td style="width: 5%; text-align: center;">
                                    <input type="button" data-bind="click: $root.EliminarAbono" class="butonResultadoFiltroPequeno butonResultadoFiltroEliminar" /></td>
                                <td data-bind='visible: ($root.Abono().TipoAbono() == "Elemento")' style="width: 15%; text-align: center;"><span data-bind='text: AbonoElemento().CodigoElemento'></span></td>
                                <td style="width: 50%;">
                                    <span data-bind='text: InfoCliente'></span>
                                    <img data-bind='visible: MultiplesCuentas' src="../../Imagenes/ico_multiples.png" style="float: right" alt="Múltiples Cuentas" />
                                </td>
                                <td style="width: 15%; text-align: left;">
                                    <span data-bind="text: BancoCuenta() ? BancoCuenta().InfoBanco : ''"></span>
                                </td>
                                <td style="width: 15%; text-align: right;">
                                    <span data-bind="text: Cuenta() ? Cuenta().CodigoCuentaBancaria : ''"></span>
                                </td>
                                <td style="width: 25%; text-align:right;">
                                    <span data-bind='text: IncluirFormato(Divisa().CodigoISO(), Importe()), style: { color: (Divisa() != null) ? Divisa().ColorHTML : "black" }'></span>
                                </td>
                                <td style="width: 5%; text-align: center;">
                                    <input type="button" class="butonResultadoFiltroPequeno butonResultadoFiltroModificar" data-bind='click: $root.DetallarAbonoValor' /></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="dvclear"></div>
        </div>

    </div>

    <div id="dvFiltro" title="Filtro" data-bind='with: Filtro' style="display: none;">
        <ns:ucAbonoFiltro runat="server" ID="ucAbonoFiltro" />
    </div>

    <div id="dvDetalle" title="Detalle" data-bind='with: DetalleVM' style="display: none;">
        <ns:ucAbonoDetalle runat="server" ID="ucAbonoDetalle" />
    </div>

    <div id="dvCadastroDatosBancarios" title="DatosBancarios" style="display: none;" data-bind="with: DatosBancariosVM">
        <ns:ucCadastroDatosBancarios runat="server" ID="ucCadastroDatosBancarios" />
    </div>

    <div id="dvGenerarDocumentoPases" title="GeneracionDocPases" style="display: none;" data-bind="with: DatosDocumentoPasesVM">
        <ns:ucAbonoDatosDocPases runat="server" ID="ucAbonoDatosDocPases" />
    </div>

    <div id="dvAlertRedirect" title="<%= Tradutor.Traduzir("071_Comon_Titulo_Alert")%>" data-bind='with: AlertRedirectVM' style="display: none;">

        <div class="dvForm" style="width:99%;">
            <div style="width:99%;">
                <span data-bind='text: Mensaje'></span>
            </div>
            <div style="width:99%; margin:0;">
                <center>
                    <input type="button" value="OK" data-bind='click: function () { window.location.href = Location; }' id="btnOK2" class="boton" />
                </center>
            </div>
        </div>
    </div>

    <asp:Literal ID="litDicionario" runat="server"></asp:Literal>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <div class="dvForm dvFormNO">
        <div style="margin: 0; width: 100%;">
        <center>
            <input type="button" data-bind='click: Grabar, visible: (PossuiCabecalhoAbono), enable: (Abono().AbonosValor().length > 0)' value="<%= Tradutor.Traduzir("071_Abono_Boton_Grabar")%>" id="btnGrabar" class="boton" style="width:150px; color:#232323;" />
            <input type="button" data-bind='click: GrabarYFinalizar, visible: (PossuiCabecalhoAbono), enable: (Abono().AbonosValor().length > 0)' value="<%= Tradutor.Traduzir("071_Abono_Boton_Confirmar")%>" id="btnConfirmar" class="boton" style="width:150px; color:#232323;" />
            <input type="button" data-bind='click: Anular, visible: (Abono().CodigoEstado() == "EnCurso")' value="<%= Tradutor.Traduzir("071_Abono_Boton_Anular")%>" id="btnAnular"  class="boton" style="width:150px; color:#232323;" />
        </center>
        </div>
        <div class="dvclear"></div>
        <iframe id=Defib src="../MantenerSession.aspx" frameBorder=no width=0 height=0 runat="server"></iframe>
    </div>
</asp:Content>
