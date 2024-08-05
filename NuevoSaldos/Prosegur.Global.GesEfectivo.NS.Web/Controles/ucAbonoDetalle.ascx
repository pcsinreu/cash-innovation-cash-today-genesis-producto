<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucAbonoDetalle.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucAbonoDetalle" %>

<%@ Import Namespace="Prosegur.Framework.Dicionario" %>

<%@ Register Src="~/Controles/ucCadastroDatosBancarios.ascx" TagPrefix="ns" TagName="ucCadastroDatosBancarios" %>
<%@ Register Src="~/Controles/ucBusqueda.ascx" TagPrefix="ns" TagName="ucBusqueda" %>

<div class="dvForm dvFormNO">
    <div class="pnl_title">
        <span id="lblTituloCuentaValoresAbonos"><%= Tradutor.Traduzir("071_Detalle_Titulo_CuentaValoresAbonos")%></span>
        <input type="button" class="butonFiltroBusqueda" disabled="disabled" />
    </div>
    <div class="dvclear"></div>
    <div>
        <div class="dvForm dvFormNO">
            <div id="dvAbonoValor" data-bind='with: AbonoValorSelecionado' style="width: 100%; margin: 5px 5px;">
                <div style="width: 35%; margin: 5px;">
                    <div data-bind='visible: ($root.Abono().TipoAbono() == "Elemento")' style="width: 48%;">
                        <span id="lblDivisaElemento"><%= Tradutor.Traduzir("071_Detalle_Campo_Divisa")%></span><br>
                        <select data-bind=' options: $parent.ValoresDisponibles.ListaDivisas, optionsText: "Descripcion", value: $parent.DivisaSelecionada,
                            optionsAfterRender: $parent.ConfigDivisa, style: { color: $parent.DivisaSelecionada().Color }'>
                        </select>
                    </div>
                    <div data-bind='visible: ($root.Abono().TipoAbono() == "Elemento")' style="width: 48%; margin-left: 10px;">
                        <span id="lblNumeroExterno"><%= Tradutor.Traduzir("071_Detalle_Campo_NumExterno")%></span><br>
                        <input type="text" id="txtNumeroExterno" data-bind='value: AbonoElemento().CodigoElemento' style="width: 98%;" readonly />
                    </div>
                    <div data-bind='visible: ($root.Abono().TipoAbono() == "Elemento")' class="dvclear"></div>
                    <div data-bind='visible: ($root.Abono().TipoAbono() != "Elemento")' style="width: 99%; ">
                        <span id="lblDivisaSaldos"><%= Tradutor.Traduzir("071_Detalle_Campo_Divisa")%></span><br>
                        <input type="text" data-bind="value: $parent.DivisaSelecionada().Descripcion, style: { color: $parent.DivisaSelecionada().Color }" id="txtDivisaSaldos" style="width: 98%;" readonly />
                    </div>
                    <div class="dvclear"></div>
                    <div style="width: 99%; margin-top: 5px;"  data-bind='visible: ($root.Abono().TipoAbono() != "Pedido")'>
                        <span id="lblCliente" data-bind='text: $root.LabelDetalleCampoCliente'></span><br>
                        <input type="text" id="txtCliente" data-bind='value: InfoCliente' style="width: 98%;" readonly />
                    </div>
                    <div class="dvclear"></div>
                    <div style="width: 103%; margin-top: 5px;" data-bind='visible: ($root.Abono().TipoAbono() == "Pedido")'>
                        <span id="lblClientePedido" data-bind='text: $root.LabelDetalleCampoCliente'></span><br>
                        <input type="text" id="txtClientePedido" data-bind='value: $parent.BuscaClientes.InfoBancos' style="width: 98%;" readonly />
                    </div>
                    <div class="dvclear"></div>
                    <div data-bind='visible: ($root.Abono().TipoAbono() == "Pedido")' style="width: 98%;">
                        <span id="lblDivisaBanco"><%= Tradutor.Traduzir("071_CadastroDatosBancarios_Banco")%></span><br>
                        <select data-bind='options: CuentasDisponibles, optionsText: "Descripcion", value: BancoCuenta' style="width: 99%;">
                        </select>
                    </div>
                    <div style="width: 100%;">
                        <div style="width: 94%;">
                            <span id="lblCuenta"><%= Tradutor.Traduzir("071_Detalle_Campo_Cuenta")%></span><br>
                            <select data-bind='options: BancoCuenta() ? BancoCuenta().DatosBancarios : [], optionsText: "InfoCuenta", value: Cuenta, event:{ change: $parent.CambiarObservaciones}' style="width: 100%;">
                            </select>
                        </div>
                        <div style="padding-top: 12px;" >
                            <input type="button" data-bind="click: $parent.AnadirNuevoDatosBancarios" style="background-image: url('../../Imagenes/Agregar.png'); background-repeat:no-repeat; height: 25px; width: 24px; background-position-x:center; background-position-y:center; border:none"/>
                        </div>
                    </div>
                </div>
                <div style="width: 23%; margin: 0 25px;">
                    <div style="width: 99%; margin-top: 5px;">
                        <span id="lblDocumento"><%= Tradutor.Traduzir("071_Detalle_Campo_Documento")%></span><br>
                        <input type="text" id="txtDocumento" data-bind="value: Cuenta() ? Cuenta().CodigoDocumento : ''" style="width: 98%;" readonly />
                    </div>
                    <div style="width: 99%;margin-top: 5px;" >
                        <span id="lblTitularidad"><%= Tradutor.Traduzir("071_Detalle_Campo_Titularidad")%></span><br>
                        <input type="text" id="txtTitularidad" data-bind="value: Cuenta() ? Cuenta().DescripcionTitularidad : ''" style="width: 98%;" readonly />
                    </div>
                    <div style="width: 99%; margin-top: 5px;">
                        <span id="lblObservaciones"><%= Tradutor.Traduzir("071_Detalle_Campo_Observaciones")%></span><br>
                        <input type="text" id="txtObservaciones" data-bind="value: Observaciones " style="width: 98%;" /><br>
                    </div>
                    <div data-bind='visible: ($root.Abono().TipoAbono() == "Elemento")' style="width: 99%; margin-top: 5px;">
                        <span id="lblCodigo"><%= Tradutor.Traduzir("071_Detalle_Campo_Codigo")%></span><br>
                        <input type="text" id="txtCodigo" data-bind='value: AbonoElemento().Codigo' style="width: 98%;" />
                    </div>
                    <div style="width: 99%; margin-top: 5px;">
                        <span id="lblValorTotal"><%= Tradutor.Traduzir("071_Detalle_Campo_ValorTotal")%></span><br>
                        <input type="text" id="txtValorTotal" data-bind='value: DivisaImporte, style: { color: (Divisa() != null) ? Divisa().ColorHTML : "black" }' style="width: 98%;text-align: right;" readonly />
                    </div>
                </div>
                <div class="gridAbono gridAbonoNO" style="width: 35% !important; margin: 5px 5px;height: 146px;" data-bind="styleBorderThin: AbonoSaldo().VisualizacionListaTerminos()">
                    <table id="Terminos" data-bind="visible: AbonoSaldo().VisualizacionListaTerminos" >
                        <thead>
                            <tr>
                                <th style="width: 7%;"><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_Termino")%></th>
                                <th style="width: 7%;"><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_Valor")%></th>
                            </tr>
                        </thead>
                        <tbody data-bind='foreach: AbonoSaldo().ListaTerminoIAC'>
                            <tr>
                                <td style="width: 15%; background-color: #EFEFEF"><span data-bind='text: Descripcion'></span></td>
                                <td data-bind="visible: (ValoresPosibles() == null && Formato.Codigo() == '1'), backGroundColor: EsProtegido() ? '#EFEFEF' : ''" style="width: 10%;">
                                    <input type="text" id="termino_texto" data-bind="value: Valor, readOnly: EsProtegido(), maxLength: Longitud(), backGroundColor: EsProtegido() ? '#EFEFEF' : ''"  />
                                </td>
                                <td data-bind="visible: (ValoresPosibles() == null && Formato.Codigo() == '2'), backGroundColor: EsProtegido() ? '#EFEFEF' : ''" style="width: 10%;">
                                    <input type="text" id="termino_entero" data-bind="integer, value: Valor, readOnly: EsProtegido(), maxLength: Longitud(), backGroundColor: EsProtegido() ? '#EFEFEF' : ''"/>
                                </td>
                                <td data-bind="visible: (ValoresPosibles() == null && Formato.Codigo() == '3'), backGroundColor: EsProtegido() ? '#EFEFEF' : ''" style="width: 10%;">
                                    <input type="text" id="termino_decimal" data-bind="numeric, value: Valor, readOnly: EsProtegido(), maxLength: Longitud(), backGroundColor: EsProtegido() ? '#EFEFEF' : ''"/>
                                </td>
                                <td data-bind="visible: (ValoresPosibles() == null && Formato.Codigo() == '4'), backGroundColor: EsProtegido() ? '#EFEFEF' : ''" style="width: 10%;">
                                    <input type="text" id="termino_fecha"  style="width: 122px;" data-bind="datePicker: [PermitirDatePicker(), 'False'], value: Valor, readOnly: EsProtegido(), maxLength: Longitud(), backGroundColor: EsProtegido() ? '#EFEFEF' : ''" />
                                </td>
                                <td data-bind="visible: (ValoresPosibles() == null && Formato.Codigo() == '5'), backGroundColor: EsProtegido() ? '#EFEFEF' : ''" style="width: 10%;">
                                    <input type="text"  id="termino_fecha_y_hora" style="width: 122px;" data-bind="datePicker: [PermitirDatePicker(), 'True'], value:Valor, readOnly: EsProtegido(), maxLength: Longitud(), backGroundColor: EsProtegido() ? '#EFEFEF' : ''" />
                                </td>
                                <td data-bind="visible: (ValoresPosibles() == null && Formato.Codigo() == '6'), backGroundColor: EsProtegido() ? '#EFEFEF' : ''" style="width: 10%;">
                                    <input type="checkbox" id="termino_Booleano" data-bind="checked: Valor, readOnly: EsProtegido(), backGroundColor: EsProtegido() ? '#EFEFEF' : ''" />
                                </td>
                                <!-- ko if: (ValoresPosibles() != null) -->
                                <td style="width: 10%;" data-bind="backGroundColor: EsProtegido() ? '#EFEFEF' : ''">
                                    <select id="termino_tiene_valores_posibles" data-bind="options: ValoresPosibles, optionsText: 'Descripcion', optionsValue: 'Descripcion', optionsCaption: '', value: Valor, disabled: EsProtegido(), backGroundColor: EsProtegido() ? '#EFEFEF' : ''">
                                    </select>
                                </td>
                                <!-- /ko -->
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div data-bind='with: AbonoValorSelecionado' style="width: 100%; margin: 5px 5px;">
                <%--Totales--%>
                <div class="gridAbono gridAbonoNO">
                    <table id="Totales" data-bind='with: Divisa().Totales'>
                        <thead>
                            <tr>
                                <th style="width: 20%;"><%= Tradutor.Traduzir("071_Detalle_Campo_TotalEfectivo")%></th>
                                <th style="width: 20%;"><%= Tradutor.Traduzir("071_Detalle_Campo_TotalOtroValor")%></th>
                                <th style="width: 20%;"><%= Tradutor.Traduzir("071_Detalle_Campo_TotalCheque")%></th>
                                <th style="width: 20%;"><%= Tradutor.Traduzir("071_Detalle_Campo_TotalTarjeta")%></th>
                                <th style="width: 20%;"><%= Tradutor.Traduzir("071_Detalle_Campo_TotalTicket")%></th>
                            </tr>
                        </thead>
                        <tbody>
                            <!-- ko if: ((!$parent.Novo) || ($parent.Novo && 
                            ((TotalesAbonoDisponible.TotalChequeDisponible() != 0) || (TotalCheque() != 0) || 
                            (TotalesAbonoDisponible.TotalEfectivoDisponible() != 0) || (TotalEfectivo() != 0) || 
                            (TotalesAbonoDisponible.TotalOtroValorDisponible() != 0) || (TotalOtroValor() != 0) || 
                            (TotalesAbonoDisponible.TotalTarjetaDisponible() != 0) || (TotalTarjeta() != 0) || 
                            (TotalesAbonoDisponible.TotalTicketDisponible() != 0) || (TotalTicket() != 0)))) -->
                            <tr>
                                <td style="width: 20%; background-color: #eeeeee; text-align:right">
                                    <span data-bind='text: TotalesAbonoDisponible.TotalEfectivoDisponible(), decimalMask:TotalesAbonoDisponible.TotalEfectivoDisponible()' style="margin-right:0"></span>
                                </td>
                                <td style="width: 20%; background-color: #eeeeee; text-align:right">
                                    <span data-bind='text: TotalesAbonoDisponible.TotalOtroValorDisponible(), decimalMask:TotalesAbonoDisponible.TotalOtroValorDisponible()' style="margin-right:0"></span>
                                </td>
                                <td style="width: 20%; background-color: #eeeeee; text-align:right">
                                    <span data-bind='text: TotalesAbonoDisponible.TotalChequeDisponible(), decimalMask:TotalesAbonoDisponible.TotalChequeDisponible()' style="margin-right:0"></span>
                                </td>
                                <td style="width: 20%; background-color: #eeeeee; text-align:right">
                                    <span data-bind='text: TotalesAbonoDisponible.TotalTarjetaDisponible(), decimalMask:TotalesAbonoDisponible.TotalTarjetaDisponible()' style="margin-right:0"></span>
                                </td>
                                <td style="width: 20%; background-color: #eeeeee; text-align:right">
                                    <span data-bind='text: TotalesAbonoDisponible.TotalTicketDisponible(), decimalMask:TotalesAbonoDisponible.TotalTicketDisponible()' style="margin-right:0"></span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%; background-color: #eeeeee; text-align:right">
                                    <!-- ko if: ($root.AbonoValorPeloTipo.Totales($parent).TotalEfectivo() != 0) -->
                                    <input type="text" data-bind='numeric, value: TotalEfectivo, decimalMask: TotalEfectivo' style="border: none; width: 99%; text-align: right" />
                                    <!-- /ko -->
                                </td>
                                <td style="width: 20%; background-color: #eeeeee; text-align:right">
                                    <!-- ko if: ($root.AbonoValorPeloTipo.Totales($parent).TotalOtroValor() != 0) -->
                                    <input type="text" data-bind='numeric, value: TotalOtroValor, decimalMask: TotalOtroValor' style="border: none; width: 99%; text-align: right" />
                                    <!-- /ko -->
                                </td>
                                <td style="width: 20%; background-color: #eeeeee; text-align:right">
                                    <!-- ko if: ($root.AbonoValorPeloTipo.Totales($parent).TotalCheque() != 0) -->
                                    <input type="text" data-bind='numeric, value: TotalCheque, decimalMask: TotalCheque' style="border: none; width: 99%; text-align: right" />
                                    <!-- /ko -->
                                </td>
                                <td style="width: 20%; background-color: #eeeeee; text-align:right">
                                    <!-- ko if: ($root.AbonoValorPeloTipo.Totales($parent).TotalTarjeta() != 0) -->
                                    <input type="text" data-bind='numeric, value: TotalTarjeta, decimalMask: TotalTarjeta' style="border: none; width: 99%; text-align: right" />
                                    <!-- /ko -->
                                </td>
                                <td style="width: 20%; background-color: #eeeeee; text-align:right">
                                    <!-- ko if: ($root.AbonoValorPeloTipo.Totales($parent).TotalTicket() != 0) -->
                                    <input type="text" data-bind='numeric, value: TotalTicket, decimalMask: TotalTicket' style="border: none; width: 99%; text-align: right" />
                                    <!-- /ko -->
                                </td>
                            </tr>
                            <!-- /ko -->
                        </tbody>
                    </table>
                </div>
                
                <%--Efectivos--%>
                <div class="dvclear"></div>
                <div class="gridAbono gridAbonoNO">
                    <table id="Efectivos">
                        <thead>
                            <tr>
                                <th style="width: 12%;"><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_Denominacion")%></th>
                                <th style="width: 12%;"><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_BilleteMoneda")%></th>
                                <th style="width: 19%;"><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_Disp_Cantidad")%></th>
                                <th style="width: 19%;"><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_Disp_Valor")%></th>
                                <th style="width: 19%;"><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_Inf_Cantidad")%></th>
                                <th style="width: 19%;"><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_Inf_Valor")%></th>
                            </tr>
                        </thead>
                        <tbody data-bind='foreach: Divisa().ListaEfectivo'>
                            <!-- ko if: ((!$parent.Novo) || ($parent.Novo && ((EfectivoDisponible.CantidadDisponible() != 0) || (Cantidad() != 0)))) -->
                            <tr>
                                <td style="width: 12%; background-color: #eeeeee; padding-left: 5px;">
                                    <span data-bind='text: Descripcion'></span>
                                </td>
                                <td style="width: 12%; background-color: #eeeeee; text-align: center;">
                                    <!-- ko if: (EsBillete) -->
                                    <img alt="Billete" src="../../Imagenes/Money.gif" />
                                    <!-- /ko -->
                                    <!-- ko ifnot: (EsBillete) -->
                                    <img alt="Moneda" src="../../Imagenes/Coins.gif" />
                                    <!-- /ko -->
                                </td>
                                <td style="width: 19%; background-color: #eeeeee; padding-left: 5px; text-align: right">
                                    <span data-bind='text: EfectivoDisponible.CantidadDisponible'" style="margin-right:0"></span>
                                </td>
                                <td style="width: 19%; background-color: #eeeeee; padding-left: 5px; text-align: right">
                                    <span data-bind='text: EfectivoDisponible.ValorDisponible, decimalMask: EfectivoDisponible.ValorDisponible' style="margin-right:0"></span>
                                </td>
                                <td style="width: 19%;">
                                    <input type="text" data-bind='numeric, value: Cantidad' style="width: 99%; border: none; text-align:right" />
                                </td>
                                <td style="width: 19%; background-color: #eeeeee; padding-left: 5px; text-align: right">
                                    <span data-bind='text: Importe, decimalMask:Importe' style="margin-right:0"></span>
                                </td>
                            </tr>
                            <!-- /ko -->
                        </tbody>
                    </table>
                </div>

                <%--Totales Efectivos--%>
                <div class="dvclear" data-bind='visible: (Divisa().ListaEfectivo() && Divisa().ListaEfectivo().length > 0)'></div>
                <div class="gridAbono gridAbonoNO" data-bind='visible: (Divisa().ListaEfectivo() && Divisa().ListaEfectivo().length > 0)' style="width: 20px">
                    <table id="TotalesEfectivos" style="float:right; width: 440px">
                        <thead>
                            <tr>
                                <th><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_TotalDisponible")%></th>
                                <th><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_TotalInformado")%></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <span data-bind='text: DivisaImporteTotalEfectivoDisponible, style: { color: (Divisa() != null) ? Divisa().ColorHTML : "black" }' style="float: right"></span>
                                </td>
                                <td>
                                    <span data-bind='text: DivisaImporteTotalEfectivo, style: { color: (Divisa() != null) ? Divisa().ColorHTML : "black" }' style="float: right"></span>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                
                <%--Medios Pagos--%>
                <div class="dvclear"></div>
                <div class="gridAbono gridAbonoNO">
                    <table id="MediosPagos">
                        <thead>
                            <tr>
                                <th style="width: 12%;"><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_Tipo_MedioPago")%></th>
                                <th style="width: 12%;"><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_MedioPago")%></th>
                                <th style="width: 19%;"><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_Disp_Cantidad")%></th>
                                <th style="width: 19%;"><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_Disp_Valor")%></th>
                                <th style="width: 19%;"><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_Inf_Cantidad")%></th>
                                <th style="width: 19%;"><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_Inf_Valor")%></th>
                            </tr>
                        </thead>
                        <tbody data-bind='foreach: Divisa().ListaMedioPago'>
                            <!-- ko if: ((!$parent.Novo) || ($parent.Novo && ((MedioPagoDisponible.CantidadDisponible() != 0) || (MedioPagoDisponible.ValorDisponible() != 0) || (Cantidad() != 0) || (Importe() != 0)))) -->
                            <tr>
                                <td style="width: 12%; background-color: #eeeeee; padding-left: 5px;">
                                    <span data-bind='text: DescripcionTipoMedioPago'></span>
                                </td>
                                <td style="width: 12%; background-color: #eeeeee; text-align: center;">
                                    <span data-bind='text: Descripcion'></span>
                                </td>
                                <td style="width: 19%; background-color: #eeeeee; padding-left: 5px; text-align:right">
                                    <span data-bind='text: MedioPagoDisponible.CantidadDisponible'></span>
                                </td>
                                <td style="width: 19%; background-color: #eeeeee; padding-left: 5px; text-align:right">
                                    <span data-bind='text: MedioPagoDisponible.ValorDisponible, decimalMask: MedioPagoDisponible.ValorDisponible'></span>
                                </td>
                                <td style="width: 19%;">
                                    <input type="text" data-bind='numeric, value: Cantidad' style="width: 99%; border: none; text-align:right" />
                                </td>
                                <td style="width: 19%;">
                                    <input type="text" data-bind='numeric, value: Importe, decimalMask: Importe' style="width: 99%; border: none; text-align:right" />
                                </td>
                            </tr>
                            <!-- /ko -->
                        </tbody>
                    </table>
                </div>

                <%--Totales Medios Pagos--%>
                <div class="dvclear" data-bind='visible: (Divisa().ListaMedioPago() && Divisa().ListaMedioPago().length > 0)'></div>
                <div class="gridAbono gridAbonoNO" data-bind='visible: (Divisa().ListaMedioPago() && Divisa().ListaMedioPago().length > 0)' style="width: 20px">
                    <table id="TotalesMediosPagos" style="float:right; width: 440px">
                        <thead>
                            <tr>
                                <th><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_Tipo_MedioPago")%></th>
                                <th><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_TotalDisponible")%></th>
                                <th><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_TotalInformado")%></th>
                            </tr>
                        </thead>
                        <tbody data-bind='foreach: TiposMedioPagoTotales'>
                            <tr>
                                <td>
                                    <span data-bind='text: TipoMedioPago' style="float: right"></span>
                                </td>
                                <td>
                                    <span data-bind='text: DivisaImporteTotalMedioPagosDisponible, style: { color: (ColorDivisa) ? ColorDivisa : "black" }' style="float: right"></span>
                                </td>
                                <td>
                                    <span data-bind='text: DivisaImporteTotalMedioPagos, style: { color: (ColorDivisa) ? ColorDivisa : "black" }' style="float: right"></span>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                
            </div>
        </div>
    </div>
    <div class="dvclear"></div>
    <div class="pnl_title">
        <span id="lblTituloAbonos"><%= Tradutor.Traduzir("071_Detalle_Titulo_Abonos")%></span>
        <input type="button" class="butonFiltroBusqueda" disabled="disabled" />
    </div>
    <div class="dvclear"></div>
    <div class="gridAbono gridAbonoNO">
        <table id="AbonosValores">
            <thead>
                <tr>
                    <th style="width: 15%;"><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_Banco")%></th>
                    <th style="width: 15%;"><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_Cuenta")%></th>
                    <th style="width: 10%;"><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_Documento")%></th>
                    <th style="width: 30%;"><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_Titularidad")%></th>
                    <th style="width: 10%;" data-bind='visible: ($root.Abono().TipoAbono() == "Elemento")'><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_Codigo")%></th>
                    <th style="width: 15%;"><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_Observaciones")%></th>
                    <th style="width: 10%;"><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_Valor")%></th>
                    <th style="width: 10%;"><%= Tradutor.Traduzir("071_Detalle_Grid_TituloColumna_Acciones")%></th>
                </tr>
            </thead>
            <tbody data-bind='foreach: ListaAbonosValores'>
                <tr>
                    <td style="width: 15%;"><span data-bind="text: BancoCuenta() ? BancoCuenta().InfoBanco : ''"></span></td>
                    <td style="width: 15%;"><span data-bind="text: Cuenta() ? Cuenta().InfoCuenta : ''"></span></td>
                    <td style="width: 10%;"><span data-bind="text: Cuenta() ? Cuenta().CodigoDocumento: ''"></span></td>
                    <td style="width: 30%;"><span data-bind="text: Cuenta() ? Cuenta().DescripcionTitularidad : ''"></span></td>
                    <td style="width: 10%;" data-bind='visible: ($root.Abono().TipoAbono() == "Elemento")'><span data-bind='    text: AbonoElemento().Codigo'></span></td>
                    <td style="width: 15%;"><span data-bind="text: Observaciones "></span></td>
                    <td style="width: 10%; text-align:right"><span data-bind='text: DivisaImporte, style: { color: (Divisa() != null) ? Divisa().ColorHTML : "black" }'></span></td>
                    <td style="width: 10%;">
                        <center>
                            <input type="button" data-bind='click: $parent.CambiarAbonoValor' class="butondefectoPequeno" style="width: 17px; height: 17px; border-width: 0; background-image:url('../../Imagenes/Editar.png') !important;" />
                            <input type="button" data-bind='click: $parent.BorrarAbonoValor' class="butondefectoPequeno" style="width: 17px; height: 17px; border-width: 0; background-image:url('../../Imagenes/Quitar2.png') !important;" />
                        </center>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="dvclear"></div>

    <div id="dvBoton" style="width: 95%; height: 40px !important; margin-top: 10px !important;">
        <center>
            <input type="button" value="<%= Tradutor.Traduzir("071_Detalle_Boton_Confirmar")%>" id="btnConfirmar" data-bind="click: Confirmar" class="boton" />
            <input type="button" value="<%= Tradutor.Traduzir("071_Detalle_Boton_OtraCuenta")%>" id="btnOtraCuenta" data-bind="click: InformarOtraCuenta" class="boton" style="width: 150px;" />
            <input type="button" value="<%= Tradutor.Traduzir("071_Detalle_Boton_Cancelar")%>" id="btnCancelar" data-bind="click: Cancelar" class="boton" />
        </center>
    </div>
</div>
