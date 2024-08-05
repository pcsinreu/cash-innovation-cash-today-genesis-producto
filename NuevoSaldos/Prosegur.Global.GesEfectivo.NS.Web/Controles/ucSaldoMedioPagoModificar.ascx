<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucSaldoMedioPagoModificar.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucSaldoMedioPagoModificar" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/js/ucSaldoMedioPagoModificar.js" />
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

<div style="margin-left: 10px !important;" id="dvDivisas" runat="server" data-bind="with: Grid1">
    <!-- TOTALES MEDIO PAGO -->
    <asp:Panel ID="pnlTotalesMedioPago" runat="server" data-bind="enable: exhibirTotalesMedioPago ">
        <input type="hidden" name="hdnTotalesMedioPago" id="hdnTotalesMedioPago" runat="server" data-bind="value: ko.toJSON(totalesMedioPagoSerializar)" />

        <div style="margin-left: 0px;">
            <table class="ui-datatable ui-datatable-data-middle tablaDivisa">
                <thead>
                    <tr>
                        <th style="width: 20px; padding: 0px !important; min-width: 0px;"></th>
                        <th style="width: 10px; padding: 0px !important; min-width: 0px;"></th>
                        <th data-bind="text: dicionario.divisa" style="max-width: 130px;" />
                        <th data-bind="text: dicionario.medioPago" style="max-width: 110px;" />
                        <th data-bind="text: dicionario.cantidad" style="max-width: 100px;" />
                        <th data-bind="text: dicionario.valor" style="max-width: 100px;" />
                    </tr>
                </thead>
                <tbody data-bind="foreach: totalesMedioPago">
                    <tr data-bind="style: { backgroundColor: corFundo }">
                        <td style="max-width: 20px; text-align: center">
                            <div class="styled-input">
                                <input type="checkbox" style="width: auto; border: none;" data-bind="checked: detallar, click: $root.ManejoSaldo, enable: $root.habilitaCheckBox"  />
                            </div>
                        </td>
                        <td data-bind="style: { backgroundColor: corLinha }" style="width: 10px; min-width: 0px;"></td>
                        <td style="max-width: 130px;">
                            <div class="styled-select">
                                <select style="padding-left: 0px; width: 100%; border: none;" data-bind="style: { backgroundColor: corFundo }, options: $root.divisasDisponiveis, optionsCaption: '-', value: divisa, optionsText: 'Descripcion', addLinhaMedioPago: true, teclaAtalho: $root.divisasDisponiveis, enable: $root.editavel" />
                            </div>
                        </td>
                        <td style="max-width: 110px;">
                            <div class="styled-select">
                                <select style="padding-left: 0px; width: 100%; border: none;" data-bind="style: { backgroundColor: corFundo }, options: mediosPago, optionsCaption: '-', value: medioPago, optionsText: 'Descripcion', addLinhaMedioPago: true, enable: $root.editavel && habilitarMedioPago, teclaAtalho: mediosPago" />
                            </div>
                        </td>
                        <td style="max-width: 100px;">
                            <input class="Grid" data-bind="value: cantidad, addLinhaMedioPago: true, enable: $root.editavel && HabilitarCantidadValor" maxlength="14" onkeypress="return bloqueialetras(event,this);" />
                        </td>
                        <td style="max-width: 100px;">
                            <input class="Grid" data-bind="value: valor, addLinhaMedioPago: true, enable: $root.editavel && HabilitarCantidadValor" maxlength="24" onkeyup="moedaIAC(event,this,',','.');" />
                        </td>                       
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="dvclear">&nbsp;</div>
        <div>
            <%--<table class="ui-datatable ui-datatable-data-middle tableTotales">--%>
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

    </asp:Panel>
</div>

