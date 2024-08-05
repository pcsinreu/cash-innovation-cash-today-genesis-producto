<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucSaldoMedioPagoDetallar.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucSaldoMedioPagoDetallar" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/js/ucSaldoMedioPagoDetallar.js" />
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

<div style="margin-left: 10px !important;" id="dvDivisasMedioPagoDetallar" runat="server">
    <!-- TOTALES MEDIO PAGO -->
    <asp:Panel ID="pnlTotalesMedioPago" runat="server" data-bind="enable: exhibirTotalesMedioPago ">
        <input type="hidden" name="hdnTotalesMedioPago" id="hdnTotalesMedioPago" runat="server" data-bind="value: ko.toJSON(totalesMedioPagoSerializar)" />

        <div style="margin-left: 0px;">
            <table class="ui-datatable ui-datatable-data-middle tablaDivisa">
                <thead>
                    <tr>
                        <th style="width: 10px; padding: 0px !important; min-width: 10px;"></th>
                        <th data-bind="text: dicionario.divisa" style="max-width: 130px;" />
                        <th data-bind="text: dicionario.medioPago" style="max-width: 200px;" />
                        <th data-bind="text: dicionario.cantidad" style="max-width: 100px;" />
                        <th data-bind="text: dicionario.valor" style="max-width: 100px;" />
                        <th data-bind="text: dicionario.informacionesComplementares" style="max-width: 70px;" />
                        <th class="GridImagen">
                            <img src="../Imagenes/Quitar.png" style="cursor: pointer; width: auto !important; height: auto !important;" data-bind="click: RemoverTotalMedioPago, visible: (editavel && (totalesMedioPago().length > 1)), attr: { alt: dicionario.remover }" /></th>
                    </tr>
                </thead>
                <tbody data-bind="foreach: totalesMedioPago">
                    <tr>
                        <td data-bind="style: { backgroundColor: corLinha }" style="width: 10px; min-width: 0px;"></td>
                        <td style="max-width: 130px;">
                            <div class="styled-select">
                                <select style="padding-left: 0px; width: 100%; border: none;" data-bind="options: $root.divisasDisponiveis, optionsCaption: $root.dicionario.selecione, value: divisa, optionsText: 'Descripcion', addLinhaMedioPago: true, teclaAtalho: $root.divisasDisponiveis, enable: $root.editavel" />
                            </div>
                        </td>

                        <td style="max-width: 200px;">
                            <div class="styled-select">
                                <select style="padding-left: 0px; width: 100%; border: none;" data-bind="options: mediosPago, optionsCaption: $root.dicionario.selecione, value: medioPago, optionsText: 'Descripcion', addLinhaMedioPago: true, enable: $root.editavel && habilitarMedioPago, teclaAtalho: mediosPago" />
                            </div>
                        </td>
                        <td style="max-width: 100px;">
                            <input class="Grid" data-bind="value: cantidad, addLinhaMedioPago: true, enable: $root.editavel && HabilitarCantidadValor" maxlength="14" onkeypress="return bloqueialetras(event,this);" />
                        </td>
                        <td style="max-width: 100px;">
                            <input class="Grid" data-bind="value: valor, addLinhaMedioPago: true, enable: $root.editavel && HabilitarCantidadValor" maxlength="24" onkeyup="moedaIAC(event,this,',','.');" />
                        </td>
                        <!-- ko if: detallarTerminos -->
                        <td class="GridImagen" style="max-width: 70px;">
                            <input class="ImagenLink" type="image" alt="" src="<%=Page.ResolveUrl("~/Imagenes/Lupa.png")%>" data-bind="enable: true, click: $root.editarMedioPago" style="margin-top: 3px !important; border: none; width: auto !important; height: auto !important;" />

                        </td>
                        <!-- else -->
                        <td class="GridImagen" style="max-width: 70px;">
                            <img style="margin-left: 1px; margin-top: 3px; border: none; width: auto !important; height: auto !important;" alt="" src="<%=Page.ResolveUrl("~/Imagenes/LupaOff.png")%>" />

                        </td>
                        <!-- /ko -->
                        <td class="GridImagen">
                            <input class="ImagenLink" type="image" src="../Imagenes/Quitar.png" data-bind="click: $root.RemoverTotalMedioPago, visible: ($root.editavel && ($index() != $root.totalesMedioPago().length - 1)), attr: { alt: $root.dicionario.remover }" style="width: auto !important; height: auto !important; padding: 2px 0px 0px 0px !important;" />

                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="dvclear">&nbsp;</div>
        <div>
            <table>
                <!-- ko if: totalesMedioPagoGrilla().length == 0 -->
                <tbody>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </tbody>
                <!-- /ko -->
                <tbody data-bind="foreach: totalesMedioPagoGrilla">
                    <tr>
                        <td data-bind="text: Descripcion"></td>
                        <!-- ko foreach: Valores -->
                        <td data-bind="text: Valor, style: { color: Cor }"></td>
                        <!-- /ko -->
                    </tr>
                </tbody>
            </table>

        </div>

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

