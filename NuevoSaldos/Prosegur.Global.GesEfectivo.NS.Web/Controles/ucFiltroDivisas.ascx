<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucFiltroDivisas.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucFiltroDivisas" %>
<%@ Register Src="~/Controles/ucRadioButtonList.ascx" TagName="ucRadioButtonList" TagPrefix="ns" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/js/Controles/ucFiltroDivisas.js" />
    </Scripts>
</asp:ScriptManagerProxy>

<div id="ucFiltroDivisas" runat="server" style="width: 999px;">

    <table id="tblucFiltroDivisas">
        <tr>
            <td style="width: 250px;">
                <asp:Label ID="lbltituloFiltroDivisas" runat="server" Text="Selección divisas" /><br />
            </td>
        </tr>
        <tr>
            <td style="width: 250px;">
                <asp:CheckBox ID="chkDivisasInactivas" runat="server" Text="Divisas inactivas" onchange="CargarDivisas();ReglasControle();" />
            </td>
            <td style="padding-left: 15px; width: 250px;">

                <asp:CheckBox ID="chkTotalesEfectivos" runat="server" Text="Totales efectivos" />

            </td>
            <td style="padding-left: 15px; width: 250px;">
                <asp:CheckBox ID="chkTotalesTipoMedioPago" runat="server" Text="Totales Medios Pago" />
            </td>
        </tr>
        <tr>
            <td>
                <div id="divlblTituloDivisas" style="width: 250px;">
                    <asp:Label ID="lbltituloDivisas" runat="server" Text="Divisas" />
                </div>
            </td>
            <td style="padding-left: 15px;">
                <div id="divlblTituloEfectivos" style="width: 250px;">
                    <asp:Label ID="lblTituloEfectivos" runat="server" Text="Efectivos" />
                </div>
            </td>
            <td style="padding-left: 15px;">
                <div id="divlblTituloTipoMedioPago" style="width: 250px;">
                    <asp:Label ID="lblTituloTipoMediosPago" runat="server" Text="Medios Pago" />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divCheckBoxListDivisas" style="overflow-x: hidden; overflow-y: auto; width: 250px; height: 210px; border: solid 1px">
                </div>
            </td>
            <td style="padding-left: 15px; vertical-align: top;">
                <fieldset id="fsTiposValores" runat="server" visible="false" style="margin-top: -3px;">
                    <legend>Tipos de Valores</legend>
                    <asp:RadioButton ID="rbTiposValoresEfectivo" runat="server" Text="Efectivo" GroupName="TipoValor" Checked="true" />
                    <asp:RadioButton ID="rbTiposValoresCheque" runat="server" Text="Cheque" GroupName="TipoValor" />
                    <asp:RadioButton ID="rbTiposValoresTicket" runat="server" Text="Ticket" GroupName="TipoValor" />
                    <asp:RadioButton ID="rbTiposValoresOtrosValores" runat="server" Text="Otros Valores" GroupName="TipoValor" />
                </fieldset>
                <asp:Panel runat="server" ID="filtroTransaciones" Visible="false">
                    <br />
                    <fieldset id="fsConsiderarValores" runat="server" style="margin-top: -3px;">
                        <legend><%=Prosegur.Framework.Dicionario.Tradutor.Traduzir("057_considerarvalores")%></legend>
                        <asp:RadioButton ID="rblConsiderarValoresAmbos" runat="server" GroupName="ConsiderarValores" Checked="true" />
                        <asp:RadioButton ID="rblConsiderarValoresDisponivel" runat="server" GroupName="ConsiderarValores" />
                        <asp:RadioButton ID="rblConsiderarValoresNdisponivel" runat="server" GroupName="ConsiderarValores" />
                    </fieldset>
                    <br />
                    <fieldset id="fsFormato" runat="server" style="margin-top: -3px;">
                        <legend><%=Prosegur.Framework.Dicionario.Tradutor.Traduzir("057_formato")%></legend>
                        <asp:RadioButton ID="rblFormatoPDF" runat="server" GroupName="Formato" Checked="true" />
                        <asp:RadioButton ID="rblFormatoEXCEL" runat="server" GroupName="Formato" />
                    </fieldset>
                    <br />
                    <div>
                        <asp:CheckBox ID="chkNoConsiderarSectoresHijos" runat="server" Text="No Considerar Sectores Hijos"/>
                    </div>
                </asp:Panel>
                <div id="dvCheckBoxListEfectivos" runat="server" style="display: block;">
                    <div id="divCheckBoxListEfectivos" style="overflow-x: hidden; overflow-y: auto; width: 250px; height: 210px; border: solid 1px"></div>
                </div>
            </td>
            <td style="padding-left: 15px;">
                <div id="dvCheckBoxListMediosPago" runat="server" style="display: block;">
                    <div id="divCheckBoxListMediosPago" style="overflow-x: hidden; overflow-y: auto; width: 250px; height: 210px; border: solid 1px"></div>
                </div>
            </td>
        </tr>
    </table>
</div>

<asp:HiddenField ID="hdnViewState" runat="server" />

<script type="text/javascript">

    var ParametrosJSON = ObtenerViewState();
    addToPostBack = function (func) {
        var old__doPostBack = __doPostBack;
        if (typeof __doPostBack != 'function') {
            __doPostBack = func;
        } else {
            __doPostBack = function (t, a) {
                if (func(t, a)) old__doPostBack(t, a);
            }
        }
    };

    $(document).ready(function () {
        ActualizarControle();
        ReCargarDivisas();

    });


    // Obter JSON guardado no campo Hidden
    function ObtenerViewState() {
        var hdnViewState = $('[id$=hdnViewState]')[0];
        return hdnViewState.value;
    }

    // Recarregar divisas na pagina marcando as opções selecionadas anteriormente
    function ReCargarDivisas() {

        var chkDivisasInactivas = $('[id$=chkDivisasInactivas]')[0];

        var controle = false;
        if (chkDivisasInactivas != undefined) {

            controle = chkDivisasInactivas.checked;
        }

        var div = document.getElementById("divCheckBoxListDivisas");
        var parametros = JSON.parse(ParametrosJSON);

        var conteudo = "<table id='tblDivisas'>";
        var contador = 0;
        for (var i = 0; i < parametros.Divisas.length; i++) {
            contador += 1;
            var id = "checkbox_" + i;

            // Validação para valores com mais de 30 caracteres
            var valor = parametros.Divisas[i].Descripcion;
            var valorTooltip = '';
            if (parametros.Divisas[i].Descripcion.length > 30) {
                valor = SubStringJSON(parametros.Divisas[i].Descripcion);
                valorTooltip = parametros.Divisas[i].Descripcion;
            }

            if (controle == true) {

                // todas divisas
                conteudo += "<tr><td>";
                if (parametros.Divisas[i].EsSelecionado == true) {
                    conteudo += "<input type='checkbox' id=" + id + "  title='" + valorTooltip + "' onchange='ActualizarControle();ReglasControle();' value=" + parametros.Divisas[i].Identificador + " checked> " + valor + "</input>";
                } else {
                    conteudo += "<input type='checkbox' id=" + id + "  title='" + valorTooltip + "' onchange='ActualizarControle();ReglasControle();' value=" + parametros.Divisas[i].Identificador + "> " + valor + "</input>";
                }
                conteudo += "</td></tr>";
            }
            else {

                //somente divisas ativas
                if (parametros.Divisas[i].EstaActivo) {
                    conteudo += "<tr><td>";
                    if (parametros.Divisas[i].EsSelecionado == true) {
                        conteudo += "<input type='checkbox' id=" + id + " title='" + valorTooltip + "' onchange='ActualizarControle();ReglasControle();' value=" + parametros.Divisas[i].Identificador + " checked> " + valor + "</input>";
                    } else {
                        conteudo += "<input type='checkbox' id=" + id + "  title='" + valorTooltip + "' onchange='ActualizarControle();ReglasControle();' value=" + parametros.Divisas[i].Identificador + "> " + valor + "</input>";
                    }
                    conteudo += "</td></tr>";
                }
            }
        }
        conteudo += "</table>";
        div.innerHTML = conteudo;

        if (contador > 0) {
            //div.style.height = '210px';
            //div.style.width = 'auto';
            //div.style.border = 'solid 1px';
        }
        else {
            //div.style.border = '';
            div.innerHTML = parametros.MesajeDivisa;
        }

        ParametrosJSON = JSON.stringify(parametros);
        var hdnViewState = $('[id$=hdnViewState]')[0];
        hdnViewState.value = ParametrosJSON;
    }

    // Recarregar efectivos e mediospago na pagina marcando as opções selecionadas anteriormente
    function ReCargarEfectivosMediosPago() {

        var checkboxlist = document.getElementById('divCheckBoxListDivisas').getElementsByTagName('input');

        var divEfectivos = document.getElementById("divCheckBoxListEfectivos");
        var divMediosPago = document.getElementById("divCheckBoxListMediosPago");

        var parametros = JSON.parse(ParametrosJSON);

        var conteudoEfectivo = "<table id='tblEfectivos'>";
        var conteudoMedioPago = "<table id='tblMediosPago'>";

        var PoserEfectivo = 0;
        var PoserMedioPago = 0;

        var CantidadDivisaSelecionada = 0;

        for (var i = 0; i < parametros.Divisas.length; i++) {

            if (checkboxlist != undefined) {

                for (c = 0; c < checkboxlist.length; c++) {
                    if (checkboxlist[c].checked == true) {

                        parametros.Divisas[i].EsSelecionado = true;

                        CantidadDivisaSelecionada += 1;

                        if (checkboxlist[c].value == parametros.Divisas[i].Identificador) {

                            if (parametros.Divisas[i].Efectivos != null) {
                                PoserEfectivo = 1;
                                for (var j = 0; j < parametros.Divisas[i].Efectivos.length; j++) {
                                    var idefectivo = "checkboxefectivo_" + j;
                                    conteudoEfectivo += "<tr><td>";

                                    // Validação para valores com mais de 30 caracteres
                                    var valor = parametros.Divisas[i].Efectivos[j].Descripcion;
                                    var valorTooltip = '';
                                    if (parametros.Divisas[i].Efectivos[j].Descripcion.length > 30) {
                                        valor = SubStringJSON(parametros.Divisas[i].Efectivos[j].Descripcion);
                                        valorTooltip = parametros.Divisas[i].Efectivos[j].Descripcion;
                                    }

                                    if (parametros.Divisas[i].Efectivos[j].EsSelecionado == true) {
                                        conteudoEfectivo += "<input type='checkbox' id=" + idefectivo + " title='" + valorTooltip + "' onchange='AlmacenarEfectivos(this);RechazarEfectivos();' value=" + parametros.Divisas[i].Efectivos[j].Identificador + " checked> " + valor + "</input>";

                                    } else {
                                        conteudoEfectivo += "<input type='checkbox' id=" + idefectivo + " title='" + valorTooltip + "' onchange='AlmacenarEfectivos(this);RechazarEfectivos();' value=" + parametros.Divisas[i].Efectivos[j].Identificador + "> " + valor + "</input>";
                                    }

                                    conteudoEfectivo += "</td></tr>";

                                } // for Efectivos
                            } // if Efectivos not nothing

                            if (parametros.Divisas[i].MediosPago != null) {
                                PoserMedioPago = 1;
                                for (var k = 0; k < parametros.Divisas[i].MediosPago.length; k++) {

                                    // Validação para valores com mais de 30 caracteres
                                    var valor = parametros.Divisas[i].MediosPago[k].Descripcion;
                                    var valorTooltip = '';
                                    if (parametros.Divisas[i].MediosPago[k].Descripcion.length > 30) {
                                        valor = SubStringJSON(parametros.Divisas[i].MediosPago[k].Descripcion);
                                        valorTooltip = parametros.Divisas[i].MediosPago[k].Descripcion;
                                    }

                                    var idmediopago = "checkboxmediopago_" + k;
                                    conteudoMedioPago += "<tr><td>";

                                    if (parametros.Divisas[i].MediosPago[k].EsSelecionado == true) {
                                        conteudoMedioPago += "<input type='checkbox' id=" + idmediopago + " title='" + valorTooltip + "' onchange='AlmacenarMediosPago(this);RechazarMediosPago();' value=" + parametros.Divisas[i].MediosPago[k].Identificador + " checked> " + valor + "</input>";

                                    } else {
                                        conteudoMedioPago += "<input type='checkbox' id=" + idmediopago + " title='" + valorTooltip + "' onchange='AlmacenarMediosPago(this);RechazarMediosPago();' value=" + parametros.Divisas[i].MediosPago[k].Identificador + "> " + valor + "</input>";
                                    }

                                    conteudoMedioPago += "</td></tr>";

                                } // for MediosPago
                            } // if MediosPago not nothing
                        } //(checkboxlist[c].value == parametros.Divisas[i].Identificador)
                    }
                    else {
                        parametros.Divisas[i].EsSelecionado = false;
                    } // if checkbox está marcado
                } // for checkboxlist
            } // if checkboxlist not undefined
        } // for divisas

        if (PoserEfectivo == 1) {
            //divEfectivos.style.height = '210px';
            //divEfectivos.style.width = '200px';
            //divEfectivos.style.border = 'solid 1px';

            conteudoEfectivo += "</table>";

        }
        else {
            //divEfectivos.style.border = 'none';
            //divEfectivos.style.width = 'auto';
            conteudoEfectivo = '';
            if (CantidadDivisaSelecionada == 1) {
                divEfectivos.innerHTML = parametros.sMesajeEfectivo;
                conteudoEfectivo = parametros.sMesajeEfectivo;
            } else if (CantidadDivisaSelecionada > 1) {
                conteudoEfectivo = parametros.pMesajeEfectivo;
            }
        }

        if (PoserMedioPago == 1) {
            //divMediosPago.style.height = '210px';
            //divMediosPago.style.width = '200px;';
            //divMediosPago.style.border = 'solid 1px';

            conteudoMedioPago += "</table>";

        }
        else {
            //divMediosPago.style.border = 'none';
            //divMediosPago.style.width = 'auto';
            conteudoMedioPago = '';

            if (CantidadDivisaSelecionada == 1) {
                conteudoMedioPago = parametros.sMesajeMedioPago;
            }
            else if (CantidadDivisaSelecionada > 1) {
                conteudoMedioPago = parametros.pMesajeMedioPago;
            }
        }

        divEfectivos.innerHTML = conteudoEfectivo;
        divMediosPago.innerHTML = conteudoMedioPago;

        ParametrosJSON = JSON.stringify(parametros);
        var hdnViewState = $('[id$=hdnViewState]')[0];
        hdnViewState.value = ParametrosJSON;
    }

    // Executar regras de condições iniciais
    function ReReglasControle() {

        var parametros = JSON.parse(ParametrosJSON);
        var chkTotalesEfectivos = $('[id$=chkTotalesEfectivos]')[0];
        var chkTotalesTipoMedioPago = $('[id$=chkTotalesTipoMedioPago]')[0];
        var checkboxlistDivisas = document.getElementById('divCheckBoxListDivisas').getElementsByTagName('input');

        var divEfectivos = document.getElementById("divCheckBoxListEfectivos");
        var divMediosPago = document.getElementById("divCheckBoxListMediosPago");

        var contadorMarcados = 0;

        if (checkboxlistDivisas != null) {
            for (var a = 0; a < checkboxlistDivisas.length; a++) {
                if (checkboxlistDivisas[a].checked == true) {
                    contadorMarcados += 1;
                }
            } //for checkboxlistDivisas
        }//checkboxlist divisas nao vazio

        if (contadorMarcados > 0) {
            chkTotalesEfectivos.disabled = false;
            chkTotalesTipoMedioPago.disabled = false;
            if (chkTotalesEfectivos.disabled == true) {
                chkTotalesEfectivos.checked = true;
            }
            if (chkTotalesTipoMedioPago.disabled == true) {
                chkTotalesTipoMedioPago.checked = true;
            }

        }

        else {
            if (chkTotalesEfectivos != null) {
                chkTotalesEfectivos.disabled = true;
            }

            if (chkTotalesTipoMedioPago != null) {
                chkTotalesTipoMedioPago.disabled = true;
            }

            if (chkTotalesEfectivos != null) {
                chkTotalesEfectivos.checked = true;
            }

            if (chkTotalesTipoMedioPago != null) {
                chkTotalesTipoMedioPago.checked = true;
            }

            divEfectivos.innerHTML = parametros.MesajeDivisasNoSelecionadas;
            divMediosPago.innerHTML = parametros.MesajeDivisasNoSelecionadas;

        }
    }

    // Ao carregar a página executa os scripts
    //document.onload = ReCargarDivisas(); ReCargarEfectivosMediosPago(); ReReglasControle();

</script>
