<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucBusquedaAvanzada.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucBusquedaAvanzada" %>

<%@ Import Namespace="Prosegur.Framework.Dicionario" %>

<div id="dvBusquedaAvanzada" title="BusquedaAvanzada" style="display: none;">
    <input type="hidden" name="hdnBusquedaAvanzadaIdentificador" id="hdnBusquedaAvanzadaIdentificador">
    <input type="hidden" name="hdnBusquedaAvanzadaCodigo" id="hdnBusquedaAvanzadaCodigo">
    <input type="hidden" name="hdnBusquedaAvanzadaDescripcion" id="hdnBusquedaAvanzadaDescripcion">
    <div class="dvForm dvFormNO">

        <div class="pnl_title">
            <span id="lblCriteriosBusqueda"><%= Tradutor.Traduzir("071_BusquedaAvanzada_Titulo_Filtro")%></span>
            <input type="button" class="butonFiltroBusqueda" disabled="disabled" />
        </div>
        <div class="dvclear"></div>

        <div>
            <span id="lblBusquedaCodigo"><%= Tradutor.Traduzir("071_BusquedaAvanzada_Campo_Codigo")%></span><br>
            <input id="txtBusquedaCodigo" type="text" maxlength="15" onkeypress="if (WebForm_TextBoxKeyHandler(event) == false) return false;" onkeydown="javascript: return event.keyCode != 13" style="width: 100px;">
        </div>
        <div>
            <span id="ucBusquedaAvanzada_lblDescripcion"><%= Tradutor.Traduzir("071_BusquedaAvanzada_Campo_Descripcion")%></span><br>
            <input id="txtBusquedaDescripcion" type="text" maxlength="100" onkeypress="if (WebForm_TextBoxKeyHandler(event) == false) return false;" onkeydown="javascript: return event.keyCode != 13" style="width: 215px;">
        </div>
        <div>
            <br>
            <input type="button" value="<%= Tradutor.Traduzir("071_Comon_Boton_Buscar")%>" id="btnBusquedaBuscar" class="boton" onclick="javascript: obtenerValores($('#txtBusquedaCodigo'), $('#txtBusquedaDescripcion'), null, null, true);">
        </div>
        <div>
            <br>
            <input type="button" value="<%= Tradutor.Traduzir("071_Comon_Boton_Limpar")%>" id="btnBusquedaLimpiar" class="boton" onclick="javascript: limparCampoBusqueda();">
        </div>
        
        <div class="dvclear"></div>

        <div class="tipoSector">
            <br />
            <input id="chkConsiderarTodosNiveis" type="checkbox" />
            <span id="lblConsiderarTodosNiveis"><%= Tradutor.Traduzir("071_Filtro_Campo_ConsiderarTodosNiveis")%></span><br>
        </div>

        <div class="pnl_title">
            <span id="lblucBusquedaAvanzadaResultado"><%= Tradutor.Traduzir("071_BusquedaAvanzada_Titulo_Respuesta")%></span>
            <input type="button" class="butonFiltroBusqueda" disabled="disabled" />
        </div>

        <div id="dvBusquedaLista" style="width: 98%;">
        </div>

        <div class="dvclear"></div>
        <div id="dvBoton" style="width: 95%;">
            <center>
            <input type="button" value="<%= Tradutor.Traduzir("071_Comon_Boton_Aceptar")%>" id="btnAceptar" class="boton" onclick="javascript: aceptarBusquedaAvanzada();">
            <input type="button" value="<%= Tradutor.Traduzir("071_Comon_Boton_Cancelar")%>" id="btnCancelar" class="boton" onclick="javascript: ClosePopupBusquedaAvanzado();" />
        </center>
        </div>
        <asp:Literal ID="litDiccionario" runat="server"></asp:Literal>
        <asp:Literal ID="litScript" runat="server"></asp:Literal>
    </div>
</div>





