<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucSaldoEfectivoDetallar.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucSaldoEfectivoDetallar" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/js/ucSaldoEfectivoDetallar.js" />
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


<div style="width: 50%; margin-left: 10px !important;" id="dvDivisasEfectivoDetallar" runat="server">

    <!-- TOTALES EFECTIVO -->
    <asp:Panel ID="pnlTotalesEfectivo" runat="server" data-bind="enable: exhibirTotalesEfectivo ">
        <input type="hidden" name="hdnTotalesEfectivoDetallar" id="hdnTotalesEfectivoDetallar" runat="server" data-bind="value: ko.toJSON(totalesEfectivoSerializar)" />

        <div style="margin-left: 0px;">
            <table class="ui-datatable ui-datatable-data-middle tablaDivisa">
                <thead>
                    <tr>
                        <th style="width: 10px; padding: 0px !important; min-width: 10px;"></th>
                        <th data-bind="text: dicionario.denominacion" style="max-width: 150px;" />
                        <th data-bind="text: ''" style="max-width: 30px;" />
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
                         <td style="max-width: 150px;">
                            <div class="styled-select">
                                <select style="padding-left: 0px; width: 100%; border: none;" data-bind="options: denominaciones, optionsCaption: $root.dicionario.selecione, value: denominacion, optionsText: 'Descripcion', addLinhaEfectivo: true, enable: $root.editavel && habilitarDenominaciones, teclaAtalho: denominaciones">
                                </select>
                            </div>
                        </td>
                        <td style="text-align: center; max-width: 30px; padding-left: 3px !important;">
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
                            <input class="Grid" data-bind="value: valorCalculado, addLinhaEfectivo: true, enable: $root.editavel && habilitarValoresCalculados" maxlength="24" onkeyup="moedaIAC(event,this,',','.');" />
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
        </div>

        <div class="dvclear">&nbsp;</div>
        <div>
            <table>
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
                        <td data-bind="text: Descripcion"></td>
                        <td data-bind="text: TotalMoneda, style: { color: Color }"></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </asp:Panel>

</div>

