<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UCValoresDivisas.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.UCValoresDivisas" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/js/knockout-3.4.0.js" />
        <asp:ScriptReference Path="~/js/knockout.mapping-latest.js" />        
        <asp:ScriptReference Path="~/js/knockout.validation.min.js" />
        <asp:ScriptReference Path="~/js/Localization/pt-BR.js" />
        <asp:ScriptReference Path="~/js/UCValoresDivisas.js" />
    </Scripts>
</asp:ScriptManagerProxy>
<style type="text/css">
    .Grid {
    }

    .GridFecha {
        padding-left: 0px;
        width: 80%;
        height: auto;
        border: none;
    }

    .GridImagen {
        text-align: center !important;
        width: 20px;
        padding: 1px 0px 0px 0px !important;
        vertical-align: middle;
        min-width: 0px !important;
    }

    .ImagenLink {
        cursor: pointer;
        border: none;
    }

    .tableTotales {
        width: 310px;
        margin: 0 0 0 auto;
    }

        .tableTotales th {
            max-width: 150px;
        }

        .tableTotales tbody tr td {
            padding-left: 5px;
            padding-right: 5px;
        }

            .tableTotales tbody tr td:last-child {
                text-align: right;
            }
</style>
<!-- GERAL -->
<fieldset style="border: none; margin-left: 0px !important; padding-left: 2px;">
    <legend id="lgsubtitulobar" runat="server" class="subtitulobar" style="margin-left: 0px !important;">
        <span class="subtitulobar-alternar iconesubtitulo iconesubtitulo-menor"></span>
        <asp:Label ID="lblTituloComponente" runat="server" Text="ValoresDeclarados" SkinID="SubTitulo" />
    </legend>

    <div style="margin-left: 10px !important;" id="dvDivisas" runat="server">
        <!-- TOTALES POR DIVISA -->
        <asp:Panel ID="pnlTotalesPorDivisa" runat="server" data-bind="visible: exhibirTotalesDivisa && (editavel || (!editavel && (totalesDivisas() != null && totalesDivisas().length > 0)))">
            <input type="hidden" name="hdnTotalesPorDivisa" id="hdnTotalesPorDivisa" runat="server"
                data-bind="value: ko.toJSON(totalesDivisasSerializar)" />
            <div class="subtitulobar-barra" id="dvsubtitulobar" runat="server">
            </div>
            <fieldset style="border: none; margin-left: 0px !important; margin-top: 5px; padding-left: 2px;">
                <legend class="subtituloitembar" style="margin-left: 0px !important; border-bottom: solid 1px #fff; margin-left: 10px; margin-top: 10px;">
                    <span class="subtituloitembar-alternar iconesubtituloitem iconesubtituloitem-menor"></span>
                    <asp:Label ID="lblTotalesPorDivisa" runat="server" SkinID="SubTituloItem" data-bind="text: dicionario.totalesPorDivisa" />
                </legend>
                <div style="margin-left: 0px;">
                    <table class="ui-datatable ui-datatable-data-middle tablaDivisa">
                        <thead>
                            <tr>
                                <th style="width: 10px; padding: 0px !important; min-width: 0px;"></th>
                                <th data-bind="text: dicionario.divisa" style="max-width: 200px;" />
                                <th data-bind="text: dicionario.totalGeneral, visible: trabajarConTotalGeneral" style="max-width: 90px;" />
                                <th data-bind="text: dicionario.totalEfectivo" style="max-width: 90px;" />
                                <th data-bind="text: dicionario.totalOtroValor" style="max-width: 90px;" />
                                <th data-bind="text: dicionario.totalCheque" style="max-width: 90px;" />
                                <th data-bind="text: dicionario.totalTarjeta" style="max-width: 90px;" />
                                <th data-bind="text: dicionario.totalTicket" style="max-width: 90px;" />
                                <th class="GridImagen">
                                    <img src="../Imagenes/Quitar.png" alt="" style="cursor: pointer; width: auto !important; height: auto !important;" data-bind="click: RemoverTotalPorDivisa, visible: (editavel && (totalesDivisas().length > 1)), attr: { alt: dicionario.remover }" />
                                </th>
                            </tr>
                        </thead>
                        <tbody data-bind="foreach: totalesDivisas">
                            <tr>
                                <td data-bind="style: { backgroundColor: corLinha }" style="width: 10px; min-width: 0px;"></td>
                                <td style="max-width: 200px;" class="label">
                                    <div class="styled-select">
                                        <select style="padding-left: 0px; width: 100%; border: none" data-bind="options: $root.divisasDisponiveis, optionsCaption: $root.dicionario.selecione, value: divisa, optionsText: 'Descripcion', addLinhaDivisa: true, teclaAtalho: $root.divisasDisponiveis, enable: $root.editavel">
                                        </select>
                                    </div>
                                </td>
                                <td data-bind="visible: $root.trabajarConTotalGeneral">
                                    <input class="Grid" data-bind="value: totalGeneral, addLinhaDivisa: true, enable: $root.editavel && (divisa() != null)" maxlength="24" onkeyup="moedaIAC(event,this,'<%=_DecimalSeparador%>','<%=_MilharSeparador%>');" onkeypress="return bloqueialetras(event, this);" onblur="moedaIAC(event,this,'<%=_DecimalSeparador%>','<%=_MilharSeparador%>');" />
                                </td>
                                <td>
                                    <input class="Grid" data-bind="value: totalEfectivo, addLinhaDivisa: true, enable: $root.editavel && (divisa() != null)" maxlength="24" onkeyup="moedaIAC(event,this,'<%=_DecimalSeparador%>','<%=_MilharSeparador%>');" onkeypress="return bloqueialetras(event, this);" onblur="moedaIAC(event,this,'<%=_DecimalSeparador%>','<%=_MilharSeparador%>');" />
                                </td>
                                <td>
                                    <input class="Grid" data-bind="value: totalOtroValor, addLinhaDivisa: true, enable: $root.editavel && (divisa() != null)" maxlength="24" onkeyup="moedaIAC(event,this,'<%=_DecimalSeparador%>','<%=_MilharSeparador%>');" onkeypress="return bloqueialetras(event, this);" onblur="moedaIAC(event,this,'<%=_DecimalSeparador%>','<%=_MilharSeparador%>');" />
                                </td>
                                <td>
                                    <input class="Grid" data-bind="value: totalCheque, addLinhaDivisa: true, enable: $root.editavel && (divisa() != null)" maxlength="24" onkeyup="moedaIAC(event,this,'<%=_DecimalSeparador%>','<%=_MilharSeparador%>');" onkeypress="return bloqueialetras(event, this);" onblur="moedaIAC(event,this,'<%=_DecimalSeparador%>','<%=_MilharSeparador%>');" />
                                </td>
                                <td>
                                    <input class="Grid" data-bind="value: totalTarjeta, addLinhaDivisa: true, enable: $root.editavel && (divisa() != null)" maxlength="24" onkeyup="moedaIAC(event,this,'<%=_DecimalSeparador%>','<%=_MilharSeparador%>');" onkeypress="return bloqueialetras(event, this);" onblur="moedaIAC(event,this,'<%=_DecimalSeparador%>','<%=_MilharSeparador%>');" />
                                </td>
                                <td>
                                    <input class="Grid" data-bind="value: totalTicket, addLinhaDivisa: true, enable: $root.editavel && (divisa() != null)" maxlength="24" onkeyup="moedaIAC(event,this,'<%=_DecimalSeparador%>','<%=_MilharSeparador%>');" onkeypress="return bloqueialetras(event, this);" onblur="moedaIAC(event,this,'<%=_DecimalSeparador%>','<%=_MilharSeparador%>');" />
                                </td>
                                <td class="GridImagen">
                                    <input class="ImagenLink" type="image" data-bind="click: $root.RemoverTotalPorDivisa, visible: ($root.editavel && ($index() != $root.totalesDivisas().length - 1)), attr: { alt: $root.dicionario.remover }"
                                        src="../Imagenes/Quitar.png" style="width: auto !important; height: auto !important; padding: 2px 0px 0px 0px !important;" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </fieldset>
        </asp:Panel>

        <!-- TOTALES EFECTIVO -->
        <asp:Panel ID="pnlTotalesEfectivo" runat="server" data-bind="visible: exhibirTotalesEfectivo && (editavel || (!editavel && (totalesEfectivo() != null && totalesEfectivo().length > 0)))">
            <input type="hidden" name="hdnTotalesEfectivo" id="hdnTotalesEfectivo" runat="server"
                data-bind="value: ko.toJSON(totalesEfectivoSerializar)" />
            <fieldset style="border: none; margin-left: 0px !important; margin-top: 5px; padding-left: 2px;">
                <legend class="subtituloitembar" style="margin-left: 0px !important; border-bottom: solid 1px #fff; margin-left: 10px;">
                    <span class="subtituloitembar-alternar iconesubtituloitem iconesubtituloitem-menor"></span>
                    <asp:Label ID="lblTotalesEfectivos" runat="server" SkinID="SubTituloItem" data-bind="text: dicionario.totalesEfectivo" />
                </legend>
                <div style="margin-left: 0px;">
                    <table class="ui-datatable ui-datatable-data-middle tablaDivisa">
                        <thead>
                            <tr>
                                <th style="width: 10px; padding: 0px !important; min-width: 0px;"></th>
                                <th data-bind="text: dicionario.divisa" style="max-width: 130px;" />
                                <th data-bind="text: dicionario.denominacion" style="max-width: 150px;" />
                                <th data-bind="text: dicionario.billeteMoneda" style="max-width: 90px;" />
                                <th data-bind="text: dicionario.unidadMedida, visible: trabajarConUnidadMedida" style="max-width: 90px;" />
                                <th data-bind="text: dicionario.cantidad" style="max-width: 90px;" />
                                <th data-bind="text: dicionario.valor" style="max-width: 90px;" />
                                <th data-bind="text: dicionario.calidad, visible: trabajarConCalidad" style="max-width: 90px;" />
                                <th class="GridImagen">
                                    <img src="../Imagenes/Quitar.png" alt="" style="cursor: pointer; width: auto !important; height: auto !important;" data-bind="click: RemoverTotalEfectivo, visible: (editavel && (totalesEfectivo().length > 1)), attr: { alt: dicionario.remover }" />
                                </th>
                            </tr>
                        </thead>
                        <tbody data-bind="foreach: totalesEfectivo">
                            <tr>
                                <td data-bind="style: { backgroundColor: corLinha }" style="width: 10px; min-width: 0px;"></td>
                                <td style="max-width: 130px;">
                                    <div class="styled-select">
                                        <select style="padding-left: 0px; width: 100%; border: none;" data-bind="options: $root.divisasDisponiveis, optionsCaption: $root.dicionario.selecione, value: divisa, optionsText: 'Descripcion', addLinhaEfectivo: true, teclaAtalho: $root.divisasDisponiveis, enable: $root.editavel">
                                        </select>
                                    </div>
                                </td>
                                <td style="max-width: 150px;">
                                    <div class="styled-select">
                                        <select style="padding-left: 0px; width: 100%; border: none;" data-bind="options: denominaciones, optionsCaption: $root.dicionario.selecione, value: denominacion, optionsText: 'Descripcion', addLinhaEfectivo: true, enable: $root.editavel && habilitarDenominaciones, teclaAtalho: denominaciones">
                                        </select>
                                    </div>
                                </td>

                                <td style="text-align: center; max-width: 90px; padding-left: 3px !important;">
                                    <img alt="" data-bind="attr: { src: billeteMoneda }" />
                                </td>

                                <td data-bind="visible: $root.trabajarConUnidadMedida" style="max-width: 90px;">
                                    <div class="styled-select">
                                        <select style="padding-left: 0px; width: 100%; border: none" data-bind="options: unidadesMedida, optionsCaption: $root.dicionario.selecione, value: unidadMedida, optionsText: 'Descripcion', addLinhaEfectivo: true, enable: $root.editavel && habilitarUnidadMedida">
                                        </select>
                                    </div>
                                </td>
                                <td style="max-width: 90px;">
                                    <input class="Grid" data-bind="value: cantidadCalculada, addLinhaEfectivo: true, enable: $root.editavel && habilitarValoresCalculados" maxlength="14" onkeypress="return bloqueialetras(event,this);" />
                                </td>
                                <td style="max-width: 90px;">
                                    <input class="Grid" data-bind="value: valorCalculado, addLinhaEfectivo: true, enable: $root.editavel && habilitarValoresCalculados" maxlength="24" onkeyup="moedaIAC(event,this,'<%=_DecimalSeparador%>','<%=_MilharSeparador%>');" />
                                </td>
                                <td data-bind="visible: $root.trabajarConCalidad" style="max-width: 90px;">
                                    <div class="styled-select">
                                        <select style="padding-left: 0px; width: 100%; border: none" data-bind="options: calidades, optionsCaption: $root.dicionario.selecione, value: calidad, optionsText: 'Descripcion', addLinhaEfectivo: true, enable: $root.editavel && habilitarUnidadMedida">
                                        </select>
                                    </div>
                                </td>
                                <td class="GridImagen">
                                    <input class="ImagenLink" type="image" src="../Imagenes/Quitar.png" data-bind="click: $root.RemoverTotalEfectivo, visible: ($root.editavel && ($index() != $root.totalesEfectivo().length - 1)), attr: { alt: $root.dicionario.remover }" style="width: auto !important; height: auto !important; padding: 2px 0px 0px 0px !important;" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="dvclear">&nbsp;</div>
                    <div>
                        <table class="ui-datatable ui-datatable-data-middle tableTotales">
                            <thead>
                                <tr>
                                    <th data-bind="text: dicionario.divisa" />
                                    <th data-bind="text: dicionario.valor" />
                                </tr>
                            </thead>
                            <!-- ko if: totalesEfectivoGrilla().length == 0 -->
                            <tbody>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </tbody>
                            <!-- /ko -->
                            <tbody data-bind="foreach: totalesEfectivoGrilla">
                                <tr>
                                    <td data-bind="text: Descripcion, style: { color: Color }"></td>
                                    <td data-bind="text: TotalMoneda, style: { color: Color }"></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </fieldset>
        </asp:Panel>

        <!-- TOTALES MEDIO PAGO -->
        <asp:Panel ID="pnlTotalesMedioPago" runat="server" data-bind="visible: exhibirTotalesMedioPago && (editavel || (!editavel && (totalesMedioPago() != null && totalesMedioPago().length > 0)))">
            <input type="hidden" name="hdnTotalesMedioPago" id="hdnTotalesMedioPago" runat="server" data-bind="value: ko.toJSON(totalesMedioPagoSerializar)" />

           <%-- <fieldset style="border: none; margin-left: 0px !important; margin-top: 5px; padding-left: 2px;">
                <legend class="subtituloitembar" style="margin-left: 0px !important; border-bottom: solid 1px #fff; margin-left: 10px;">
                    <span class="subtituloitembar-alternar iconesubtituloitem iconesubtituloitem-menor"></span>
                    <asp:Label ID="lbltotalesMedioPago" runat="server" SkinID="SubTituloItem" data-bind="text: dicionario.totalesMedioPago" />
                </legend>
            </fieldset>--%>

            <div id="dvTerminos" style="display: none" data-bind="visible: totalMedioPagoEdicao() != null, if: totalMedioPagoEdicao() != null">
                <%--<img src="../Imagenes/No.png" style="cursor: pointer;" data-bind="click: fecharDetalheTermino" alt="Fechar">--%>
                <div class="ui-widget-overlay ui-front"></div>
                <div id="drag" class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-front ui-draggable" tabindex="-1" role="dialog" style="position: absolute; height: auto; width: 500px; top: 30%; left: 35%; margin-top: -50px; margin-left: -50px;">
                    <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                        <span id="ui-id-53" class="ui-dialog-title" style="color: #5e5e5e" data-bind="text: dicionario.terminosMedioPago">
                            <%--<asp:Literal ID="litTituloTerminos" runat="server" ></asp:Literal>--%></span>
                        <div data-bind="click: fecharDetalheTermino" style="position: absolute; right: .3em; border-style: none; cursor: pointer;">
                            <span class="ui-button-icon-primary ui-icon ui-icon-closethick"></span>
                        </div>
                    </div>
                    <div id="dvTerminosConteudo" style="width: auto; min-height: 0px; max-height: none; height: 250px; overflow-y: auto;" class="ui-dialog-content ui-widget-content">
                        <div data-bind="visible: totalMedioPagoEdicao() != null, if: totalMedioPagoEdicao() != null">
                            <%--<img src="../Imagenes/Quitar.png" style="cursor: pointer;" data-bind="click: fecharDetalheTermino" alt="Fechar" />--%>
                            <div style="margin-left: 13px;">
                                <div class="dvUsarFloat">
                                    <div>
                                        <span data-bind="text: dicionario.tipoMedioPago"></span>
                                        <br />
                                        <input class="Grid" data-bind="value: totalMedioPagoEdicao().tipoMedioPago, enable: false" />
                                    </div>
                                    <div>
                                        <span data-bind="text: dicionario.medioPago"></span>
                                        <br />
                                        <input class="Grid" data-bind="value: totalMedioPagoEdicao().medioPago().Descripcion, enable: false" />
                                    </div>
                                    <div class="dvclear"></div>
                                </div>
                                <table class="ui-datatable ui-datatable-data-middle tablaDivisa">
                                    <thead>
                                        <tr>
                                            <th style="width: 25px; min-width: inherit;"></th>
                                            <!-- ko foreach: totalMedioPagoEdicao().terminosDisponiveis -->
                                            <th data-bind="text: Descripcion"></th>
                                            <!-- /ko -->
                                            <!-- ko if: editavel -->
                                            <th class="GridImagen">
                                                <img src="../Imagenes/Quitar.png" style="cursor: pointer; width: auto !important; height: auto !important; padding: 2px 0px 0px 0px !important;" data-bind="click: RemoverItemTermino, visible: totalMedioPagoEdicao().terminos().length > 1, attr: { alt: dicionario.remover }" /></th>
                                            <!-- /ko -->
                                        </tr>
                                    </thead>
                                    <tbody data-bind="foreach: { data: totalMedioPagoEdicao().terminos, as: 'itemTermino' }">

                                        <tr>
                                            <td style="width: 25px; min-width: inherit; text-align: center; color: #989898;">
                                                <span data-bind="text: $index() + 1"></span>
                                            </td>
                                            <!-- ko foreach: {data: $root.totalMedioPagoEdicao().terminosDisponiveis, as: 'terminoDisponivel'} -->
                                            <td>
                                                <%--<span data-bind="text: terminoDisponivel.Descripcion"></span>--%>
                                                <!-- ko foreach: {data: itemTermino, as: 'termino'} -->
                                                <!-- ko if: termino.Identificador() == terminoDisponivel.Identificador -->
                                                <!-- ko if: terminoDisponivel.ValoresPosibles != null && terminoDisponivel.ValoresPosibles.length > 0 -->
                                                <select style="padding-left: 0px; width: 100%; border: none;" data-bind="options: terminoDisponivel.ValoresPosibles, optionsCaption: $root.dicionario.selecione, value: termino.Valor, optionsValue: 'Descripcion', optionsText: 'Descripcion', addLinhaTermino: true, enable: $root.editavel"></select>
                                                <!-- else -->
                                                <!-- ko if: terminoDisponivel.Formato.Codigo == "1" -->
                                                <input class="Grid" data-bind="value: termino.Valor, attr: { maxlength: termino.Longitud() }, addLinhaTermino: true, enable: $root.editavel" />
                                                <!-- else -->
                                                <!-- ko if: terminoDisponivel.Formato.Codigo == "2" -->
                                                <input class="Grid" data-bind="value: termino.Valor, attr: { maxlength: termino.Longitud(), onkeypress: 'return bloquearletras(event,this);' }, addLinhaTermino: true, enable: $root.editavel" />
                                                <!-- else -->
                                                <!-- ko if: terminoDisponivel.Formato.Codigo == "3" -->
                                                <input class="Grid" data-bind="value: termino.Valor, attr: { maxlength: termino.Longitud(), onkeypress: 'return decimais(event);' }, addLinhaTermino: true, enable: $root.editavel" />
                                                <!-- else -->
                                                <!-- ko if: terminoDisponivel.Formato.Codigo == "4" -->
                                                <input class="GridFecha" data-bind="value: termino.Valor, addLinhaTermino: true, attr: { maxlength: termino.Longitud() }, mostrarCalendario: 'false', enable: $root.editavel" />
                                                <!-- else -->
                                                <!-- ko if: terminoDisponivel.Formato.Codigo == "5" -->
                                                <input class="GridFecha" data-bind="value: termino.Valor, addLinhaTermino: true, attr: { maxlength: termino.Longitud() }, mostrarCalendario: 'true', enable: $root.editavel" />
                                                <!-- else -->
                                                <!-- ko if: terminoDisponivel.Formato.Codigo == "6" -->
                                                <input type="checkbox" data-bind="checked: termino.Valor, addLinhaTermino: true, enable: $root.editavel" />
                                                <!-- else -->
                                                ERRO!
                                                <!-- /ko -->
                                                <!-- /ko -->
                                                <!-- /ko -->
                                                <!-- /ko -->
                                                <!-- /ko -->
                                                <!-- /ko -->
                                                <!-- /ko -->
                                                <!-- /ko -->
                                                <!-- /ko -->
                                            </td>
                                            <!-- /ko -->
                                            <!-- ko if: $root.editavel -->
                                            <td class="GridImagen">
                                                <input class="ImagenLink" type="image" style="width: auto !important; height: auto !important; padding: 2px 0px 0px 0px !important;" src="../Imagenes/Quitar.png" data-bind="click: $root.RemoverItemTermino, visible: $index() != $root.totalMedioPagoEdicao().terminos().length - 1, attr: { alt: $root.dicionario.remover }" /></td>
                                            <!-- /ko -->
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </asp:Panel>
    </div>
</fieldset>
