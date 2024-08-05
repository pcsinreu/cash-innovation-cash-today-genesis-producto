<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucSaldoEfectivoModificar.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucSaldoEfectivoModificar" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/js/ucSaldoEfectivoModificar.js" />
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


<div style="width: 50%; margin-left: 10px !important;" id="dvDivisas" runat="server">

    <!-- TOTALES EFECTIVO -->
    <asp:Panel ID="pnlTotalesEfectivo" runat="server" data-bind="enable: exhibirTotalesEfectivo ">
        <input type="hidden" name="hdnTotalesEfectivo" id="hdnTotalesEfectivo" runat="server" data-bind="value: ko.toJSON(totalesEfectivoSerializar)" />

        <div style="margin-left: 0px;">
            <table class="ui-datatable ui-datatable-data-middle tablaDivisa">
                <thead>
                    <tr>
                        <th style="width: 20px; padding: 0px !important; min-width: 20px;"></th>
                        <th style="width: 10px; padding: 0px !important; min-width: 10px;"></th>
                        <th data-bind="text: dicionario.nivelDetalle" style="max-width: 80px;" />
                        <th data-bind="text: dicionario.denominacion" style="max-width: 150px;" />
                        <th data-bind="text: ''" style="max-width: 30px;" />
                        <th data-bind="text: dicionario.unidadMedida, visible: trabajarConUnidadMedida" style="max-width: 90px;" />
                        <th data-bind="text: dicionario.cantidad" style="max-width: 70px;" />
                        <th data-bind="text: dicionario.valor" style="max-width: 90px;" />
                        <th data-bind="text: dicionario.calidad, visible: trabajarConCalidad" style="max-width: 90px;" />
                    </tr>
                </thead>
                <tbody data-bind="foreach: totalesEfectivo">
                    <tr data-bind="style: { backgroundColor: corFundo }">
                        <td style="max-width: 20px; text-align: center">
                            <div class="styled-input">
                                <input type="checkbox" runat="server" style="width: auto; border: none;" data-bind="checked: detallar, click: $root.ManejoSaldo, enable: $root.habilitaCheckBox" />
                            </div>
                        </td>
                        <td data-bind="style: { backgroundColor: corLinha }" style="width: 10px; min-width: 10px;"></td>
                        <td style="max-width: 80px;">
                            <span style="padding-left: 5px; width: 100%; border: none;" data-bind="text: tipoDetalle" />
                        </td>
                        <td style="max-width: 150px;">
                            <div class="styled-select" >
                                <select style="padding-left: 0px; width: 100%; border: none;" data-bind="style: { backgroundColor: corFundo }, options: denominaciones, optionsCaption: '-', value: denominacion, optionsText: 'Descripcion', addLinhaEfectivo: true, enable: $root.editavel, teclaAtalho: denominaciones">
                                </select>
                            </div>
                        </td>
                        <td style="text-align: center; max-width: 30px; padding-left: 3px !important;">
                            <img alt="" data-bind="attr: { src: billeteMoneda }" />
                        </td>
                        <td data-bind="visible: $root.trabajarConUnidadMedida" style="max-width: 90px;">
                            <div class="styled-select">
                                <select style="padding-left: 0px; width: 100%; border: none" data-bind="style: { backgroundColor: corFundo }, options: unidadesMedida, optionsCaption: '-', value: unidadMedida, optionsText: 'Descripcion', addLinhaEfectivo: true, enable: $root.editavel">
                                </select>
                            </div>
                        </td>
                        <td style="max-width: 70px;">
                            <input class="Grid" data-bind="value: cantidadCalculada, addLinhaEfectivo: true, enable: $root.editavel" maxlength="14" onkeypress="return bloqueialetras(event,this);" />
                        </td>
                        <td style="max-width: 90px;">
                            <input class="Grid" data-bind="value: valorCalculado, addLinhaEfectivo: true, enable: $root.editavel" maxlength="24" onkeyup="moedaIAC(event,this,',','.');" />
                        </td>
                        <td data-bind="visible: $root.trabajarConCalidad" style="max-width: 90px;">
                            <div class="styled-select">
                                <select style="padding-left: 0px; width: 100%; border: none" data-bind="style: { backgroundColor: corFundo }, options: $root.calidadesDisponiveis, optionsCaption: $root.dicionario.selecione, value: calidad, optionsText: 'Descripcion', addLinhaEfectivo: true, enable: $root.editavel">
                                </select>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div class="dvclear">&nbsp;</div>
        <div>
            <%--<table class="ui-datatable ui-datatable-data-middle tableTotales">--%>
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
            <%--<table>
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
                        <!-- ko foreach: Valores -->
                        <td data-bind="text: Valor, style: { color: Cor }"></td>
                        <!-- /ko -->
                    </tr>
                </tbody>
            </table>--%>
        </div>
    </asp:Panel>

</div>

