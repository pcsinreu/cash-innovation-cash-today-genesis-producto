<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucAbonoDatosDocPases.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucAbonoDatosDocPasses" %>

<%@ Import Namespace="Prosegur.Framework.Dicionario" %>

<%@ Register Src="~/Controles/ucBusqueda.ascx" TagPrefix="ns" TagName="ucBusqueda" %>

<div class="dvForm dvFormNO">
    <div class="pnl_title">
        <span id="lblTituloCuentaValoresAbonos"><%= Tradutor.Traduzir("071_GeneracionDocPases_Titulo")%></span>
    </div>
    <div class="dvSeparator"></div>
    <span><%= Tradutor.Traduzir("071_GeneracionDocPases_Informacion")%></span>
    <div class="dvSeparator"></div>
    <div class="dvForm dvFormNO">
        <div style="width: 100%">
            <div style="width:47%">
                <ns:ucBusqueda runat="server" ID="UcSectorDocPasses" Tipo="Sector" Titulo="Setor" EsMulti="False" VisibilidadInicial="True" UtilitarioBusca="BusquedaSetor" /> 
            </div>
        </div>
        <div class="dvSeparator"></div>
        <div class="dvSeparator"></div>
        <div class="dvSeparator"></div>
        <div class="dvSeparator"></div>
        <div style="width: 100%">
            <div style="width:47%">
                <ns:ucBusqueda runat="server" ID="UcCanalDocPasses" IDAssociacao="UcSubCanalDocPasses" Tipo="Canal" Titulo="Canal" EsMulti="False" VisibilidadInicial="True" UtilitarioBusca="BusquedaCanal" /> 
            </div>
            <div style="width:47%">
                <ns:ucBusqueda runat="server" ID="UcSubCanalDocPasses" IDAssociacaoPadre="UcCanalDocPasses" Tipo="SubCanal" Titulo="SubCanal" EsMulti="False" VisibilidadInicial="False" UtilitarioBusca="BusquedaSubCanal" /> 
            </div>
        </div>
        <div class="dvSeparator"></div>
        <div id="dvBoton" style="width: 95%; height: 40px !important; margin-top: 25px !important;">
            <center>
                <input type="button" value="<%= Tradutor.Traduzir("071_GeneracionDocPases_BotonAceptar")%>" id="btnAceptar" data-bind="click: Aceptar" class="boton" />
                <input type="button" value="<%= Tradutor.Traduzir("071_GeneracionDocPases_BotonCancelar")%>" id="btnCancelar" data-bind="click: Cancelar" class="boton" />
            </center>
        </div>
    </div>
</div>