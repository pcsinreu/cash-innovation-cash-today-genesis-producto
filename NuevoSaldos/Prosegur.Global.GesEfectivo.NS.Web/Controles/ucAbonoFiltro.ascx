<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucAbonoFiltro.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucAbonoFiltro" %>

<%@ Import Namespace="Prosegur.Framework.Dicionario" %>
<%@ Register Src="~/Controles/ucBusquedaAvanzada.ascx" TagPrefix="ns" TagName="ucBusquedaAvanzada" %>
<%@ Register Src="~/Controles/ucBusqueda.ascx" TagPrefix="ns" TagName="ucBusqueda" %>

<asp:Literal ID="litAux" runat="server"></asp:Literal>

<div class="dvForm">

    <!-- ko if: $root.Abono().TipoAbono() == "Elemento" -->
    <div id="dvNumeroExterno">
        <span id="lblNumeroExterno"><%= Tradutor.Traduzir("071_Comon_Campo_NumeroExterno")%></span><br>
        <input name="txtNumeroExterno" type="text" id="txtNumeroExterno" data-bind='value: NumeroExterno' style="width: 169px;" />
    </div>
    <div id="dvPrecinto">
        <span id="lblPrecinto"><%= Tradutor.Traduzir("071_Comon_Campo_Precinto")%></span><br>
        <input name="txtPrecinto" type="text" id="txtPrecinto" data-bind='value: Precinto' style="width: 169px;" />
    </div>
    <div class="dvclear"></div>
    <!-- /ko -->

    <ns:ucBusqueda runat="server" ID="UcSector" Tipo="Sector" Titulo="Sector" EsMulti="True" VisibilidadInicial="True" UtilitarioBusca="BuscaSectores" />

    <div id="dvConsiderarTodosNiveis" style="height: auto !important;">
        <br />
        <input id="chkAcumularValorSectoresHijos" type="checkbox" data-bind='checked: ConsiderarTodosLosNiveles' />
        <span id="lblConsiderarTodosNiveis"><%= Tradutor.Traduzir("071_BusquedaAvanzada_Acumular_Valores_Hijos")%></span><br>
    </div>
    <div class="dvclear"></div>

    <!-- ko if: $root.Abono().TipoAbono() != "Pedido" -->
    <ns:ucBusqueda runat="server" ID="UcClienteFiltroInterno" IDAssociacao="UcSubClienteFiltroInterno" Tipo="Cliente" Titulo="" EsMulti="False" VisibilidadInicial="True" UtilitarioBusca="BuscaClientes" />
    <ns:ucBusqueda runat="server" ID="UcSubClienteFiltroInterno" IDAssociacao="UcPtoServicioFiltroInterno" IDAssociacaoPadre="UcClienteFiltroInterno" Tipo="SubCliente" Titulo="SubCliente" EsMulti="False" VisibilidadInicial="False" UtilitarioBusca="BuscaSubClientes" />
    <ns:ucBusqueda runat="server" ID="UcPtoServicioFiltroInterno" IDAssociacaoPadre="UcSubClienteFiltroInterno" Tipo="PtoServicio" Titulo="PtoServicio" EsMulti="False" VisibilidadInicial="False" UtilitarioBusca="BuscaPuntosServicio" />
    <div class="dvclear"></div>
    <!-- /ko -->

    <!-- ko if: $root.Abono().TipoAbono() != "Elemento" -->
    <ns:ucBusqueda runat="server" ID="UcCanalFiltroInterno" IDAssociacao="UcSubCanalFiltroInterno" Tipo="Canal" Titulo="Canal" EsMulti="False" VisibilidadInicial="True" UtilitarioBusca="BuscaCanales" />
    <ns:ucBusqueda runat="server" ID="UcSubCanalFiltroInterno" IDAssociacaoPadre="UcCanalFiltroInterno" Tipo="SubCanal" Titulo="SubCanal" EsMulti="False" VisibilidadInicial="False" UtilitarioBusca="BuscaSubCanales" />
    <div class="dvclear"></div>

    <div id="dvTipoMedioPago" style="width: 390px !important; height: auto !important; margin-bottom: 0 !important;">
        <div class="lista">
            <div>
                <span id="lblTipoMedioPago"><%= Tradutor.Traduzir("071_Comon_Campo_Valores")%></span>
            </div>
            <div class="dvclear"></div>
            <div id="dvTipoMedioPagoValores" class="valores" style="width: 360px; height: 55px;">
                <table>
                    <tbody data-bind='foreach: ValoresElegiveis'>
                        <tr>
                            <td style="padding: 0">
                                <input type="checkbox" data-bind='checked: Elegivel' style="padding: 0; margin: 0" /></td>
                            <td style="padding: 0"><span data-bind='text: Opcion' style="padding: 0; margin: 0"></span></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div id="dvTipoMedioPagoAcciones" class="acciones" style="width: 12px; margin-left: 3px;">
                <button type="button" value="" id="btnTipoMedioPagoAgregarTodos" class="imagem" data-bind='click: $root.SeleccionarTodosValoresElegiveis'>
                    <img src="<%= ResolveUrl("~/Imagenes/ckeck_true.png")%>" alt="" />
                </button>
                <br />
                <button type="button" value="" id="btnTipoMedioPagoDesAgregarTodos" class="imagem" data-bind='click: $root.RemoverSeleccionTodasValoresElegiveis'>
                    <img src="<%= ResolveUrl("~/Imagenes/ckeck_false.png")%>" alt="" />
                </button>
            </div>
        </div>
    </div>

    <div id="dvDivisas" style="width: 390px !important; height: auto !important; margin-bottom: 0 !important;">
        <div class="lista">
            <div>
                <span id="lblDivisas"><%= Tradutor.Traduzir("071_Comon_Campo_Divisa")%></span>
            </div>
            <div class="dvclear"></div>
            <div id="dvDivisasValores" class="valores" style="width: 360px; height: 55px;">
                <table>
                    <tbody data-bind='foreach: DivisasElegiveis'>
                        <tr>
                            <td style="padding: 0;">
                                <input type="checkbox" data-bind='checked: Elegivel' style="padding: 0; margin: 0" /></td>
                            <td style="padding: 0;"><span data-bind='text: Opcion' style="padding: 0; margin: 0"></span></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div id="dvAcciones" class="acciones" style="width: 12px; margin-left: 3px;">
                <button type="button" value="" id="btnAgregarTodos" class="imagem" data-bind="click: $root.SeleccionarTodasDivisasElegiveis">
                    <img src="<%= ResolveUrl("~/Imagenes/ckeck_true.png")%>" alt="" />
                </button>
                <br />
                <button type="button" value="" id="btnDesAgregarTodos" class="imagem" data-bind="click: $root.RemoverSeleccionTodasDivisasElegiveis">
                    <img src="<%= ResolveUrl("~/Imagenes/ckeck_false.png")%>" alt="" />
                </button>
            </div>
        </div>
    </div>

    <div class="dvclear"></div>
    <!-- /ko -->

    <div id="dvSolamenteUnDatoBancario">
        <br />
        <input id="chkSolamenteUnDatoBancario" type="checkbox" data-bind='checked: ClientesConSoloUnDatoBancario' />
        <span id="lblSolamenteUnDatoBancario"><%= Tradutor.Traduzir("071_Filtro_Campo_SolamenteUnDatoBancario")%></span><br>
        <!-- ko if: $root.Abono().TipoAbono() != "Elemento" -->
        <input id="chkValoresCeroYNegativo" type="checkbox" data-bind='checked: ValoresCeroYNegativos' />
        <span id="lblValoresCeroYNegativo"><%= Tradutor.Traduzir("071_Filtro_Campo_ValoresCeroYNegativos")%></span><br>
        <!-- /ko -->
    </div>

    <div class="dvclear"></div>

    <div id="dvBoton" style="width: 95%; height: 40px !important; margin-top: 20px !important;">
        <center>
        <input type="button" value="<%= Tradutor.Traduzir("071_Comon_Boton_Voltar")%>" data-bind='click: $root.VoltarFiltro' id="btnVoltar" class="boton" />
        <input type="button" value="<%= Tradutor.Traduzir("071_Comon_Boton_Buscar")%>" data-bind='click: $root.Buscar' id="btnBuscar" class="boton" />
        <input type="button" value="<%= Tradutor.Traduzir("071_Comon_Boton_Limpar")%>" data-bind='click: function(data, event) { $root.LimparFiltro(true, data, event) }' id="btnLimpar" class="boton" />
        </center>
    </div>

</div>

