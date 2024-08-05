<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucCadastroDatosBancarios.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucCadastroDatosBancarios" %>

<%@ Import Namespace="Prosegur.Framework.Dicionario" %>

<%@ Register Src="~/Controles/ucBusqueda.ascx" TagPrefix="ns" TagName="ucBusqueda" %>

<div class="dvForm dvFormNO">
    <div class="pnl_title">
        <span id="lblTituloCuentaValoresAbonos"><%= Tradutor.Traduzir("071_CadastroDatosBancarios_Titulo")%></span>
    </div>
    <div class="dvSeparator"></div>
    <div class="dvForm dvFormNO">
        <div style="width: 100%">
            <div style="width:85%">
                <div data-bind="visible: PermiteSelecaoBanco">
                    <ns:ucBusqueda runat="server" ID="UcBancoCuenta" Tipo="Banco" Titulo="Banco" EsMulti="False" VisibilidadInicial="True" UtilitarioBusca="BusquedaBancos" /> 
                </div>
                <div data-bind="visible: !PermiteSelecaoBanco()" style="width:100%">
                    <span>Banco</span>
                    <div class="dvSeparator"></div>
                    <input type="text" id="txtCliente" data-bind="value: CodigoDescripcionBanco" style="width: 98%;" readonly />
                </div>
            </div>
        </div>
        <div class="dvSeparator"></div>
        <div class="dvSeparator"></div>
        <div class="dvSeparator"></div>
        <div class="dvSeparator"></div>
        <div style="width: 100%">
            <div style="width:45%">
                <div style="width: 90%">
                    <span><%= Tradutor.Traduzir("071_CadastroDatosBancarios_Cuenta")%></span>
                </div>
                <div style="width: 90%">
                    <input type="text" class="inputTextLarge" data-bind="value: DatoBancarioObservable.CodigoCuentaBancaria" />
                </div>
            </div>
            <div style="width:45%; padding-left:3px">
                <div style="width: 90%">
                    <span><%= Tradutor.Traduzir("071_CadastroDatosBancarios_Tipo")%></span>
                </div>
                <div style="width: 90%">
                    <select style="width:99%" data-bind="options: NuevaCuenta().listaTipoCuenta(), optionsText: 'Descripcion', value: TipoCuentaSelecionado"></select>
                </div>
            </div>
        </div>
        <div class="dvSeparator"></div>
        <div style="width: 100%">
            <div style="width:45%">
                <div style="width: 90%">
                    <span><%= Tradutor.Traduzir("071_CadastroDatosBancarios_Titularidad")%></span>
                </div>
                <div style="width: 90%">
                    <input type="text" class="inputTextLarge" data-bind="value: DatoBancarioObservable.DescripcionTitularidad"/>
                 </div>
            </div>
            <div style="width:45%; padding-left:3px">
                <div style="width: 90%">
                    <span><%= Tradutor.Traduzir("071_CadastroDatosBancarios_Documento")%></span>
                </div>
                <div style="width: 90%">
                    <input type="text" class="inputTextLarge" data-bind="value: DatoBancarioObservable.CodigoDocumento"/>
                </div>
            </div>
        </div>
        <div class="dvSeparator"></div>
        <div style="width: 100%">
            <div style="width:45%;">
                <div style="width: 90%">
                <span><%= Tradutor.Traduzir("071_CadastroDatosBancarios_Observaciones")%></span>
                    </div>
                <div style="width: 90%">
                <input type="text" class="inputTextLarge" data-bind="value: DatoBancarioObservable.DescripcionObs"/>
                    </div>
            </div>
            <div style="width: 45%; padding-left: 3px;padding-top:12px">
                <span><%= Tradutor.Traduzir("071_CadastroDatosBancarios_CuentaPatron")%></span>
                <input type="checkbox" data-bind="checked: DatoBancarioObservable.bolDefecto" />
            </div>
        </div>
        <div class="dvSeparator"></div>
        <div id="dvBoton" style="width: 95%; height: 40px !important; margin-top: 25px !important;">
            <center>
                <input type="button" value="<%= Tradutor.Traduzir("071_CadastroDatosBancarios_BotonAceptar")%>" id="btnAceptar" data-bind="click: AceptarNuevaCuenta" class="boton" />
                <input type="button" value="<%= Tradutor.Traduzir("071_CadastroDatosBancarios_BotonCancelar")%>" id="btnCancelar" data-bind="click: CancelarNuevaCuenta" class="boton" />
            </center>
        </div>
    </div>
</div>
