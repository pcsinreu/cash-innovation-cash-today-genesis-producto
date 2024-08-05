// Carregar divisas inicialmente na tela de acordo com conteudo do JSON (Carregado no Load do controle ucFiltroDivisas.ascx)
function CargarDivisas() {

    // Recuperar controle chkDivisasInactivas
    var chkDivisasInactivas = $('[id$=chkDivisasInactivas]')[0];

    // Condição criado porque existe a possibilidade do controle 'chkDivisasInactivas' não estar presente na tela
    // por regras de negocio do controle. Então sempre que isto ocorrer a variavel controle assumirá FALSE.
    var controle = false;
    if (chkDivisasInactivas != undefined) {
        controle = chkDivisasInactivas.checked;
    }
    // Recuperar divContainer dos checkbox's de divisas
    var div = document.getElementById("divCheckBoxListDivisas");

    // Converter ParametrosJSON em variavel JavaScript
    var parametros = JSON.parse(ParametrosJSON);

    var conteudo = "<table id='tblDivisas'>";
    var contador = 0;
    // Loop de divisas (JSON)
    for (var i = 0; i < parametros.Divisas.length; i++) {
        contador += 1;
        var id = "checkbox_" + i;
        parametros.Divisas[i].EsSelecionado = false;
        // Validação para valores com mais de 30 caracteres
        var valor = parametros.Divisas[i].Descripcion;
        var valorTooltip = '';
        if (parametros.Divisas[i].Descripcion.length > 30) {
            valor = SubStringJSON(parametros.Divisas[i].Descripcion);
            valorTooltip = parametros.Divisas[i].Descripcion;
        }
        if (controle == true) {
            // Se a variavel controle estiver TRUE, o componente será carregado com todas as divisas (Ativas e inativas)
            //parametros.Divisas[i].EsSelecionado = true;
            conteudo += "<tr><td>";
            conteudo += "<input type='checkbox' id=" + id + " title='" + valorTooltip + "' onchange='ActualizarControle();ReglasControle();' value=" + parametros.Divisas[i].Identificador + " /> " + valor + "";
            conteudo += "</td></tr>";
        }
        else {
            //parametros.Divisas[i].EsSelecionado = false;
            // Se a variavel controle estiver FALSE, o componente será carregado somente com as divisas ativas.
            if (parametros.Divisas[i].EstaActivo) {
                conteudo += "<tr><td>";
                conteudo += "<input type='checkbox' id=" + id + " title='" + valorTooltip + "' onchange='ActualizarControle();ReglasControle();' value=" + parametros.Divisas[i].Identificador + " /> " + valor + "";
                conteudo += "</td></tr>";
            }
        }
    }
    conteudo += "</table>";
    // Atribui a divContainer de divisas o conteudo gerado no Loop de divisas do JSON
    div.innerHTML = conteudo;

    if (contador > 0) {
        div.style.height = '210px';
        div.style.width = 'auto';
        div.style.border = 'solid 1px';
    }
    else {
        div.style.border = '';
        div.innerHTML = parametros.MesajeDivisa;
    }

    ParametrosJSON = JSON.stringify(parametros);
    var hdnViewState = $('[id$=hdnViewState]')[0];
    hdnViewState.value = ParametrosJSON;
}

// Atualizar os controles de efetivo e mediopago de acordo com as divisas selecionadas
function ActualizarControle() {

    var checkboxlist = document.getElementById('divCheckBoxListDivisas').getElementsByTagName('input');

    var divEfectivos = document.getElementById("divCheckBoxListEfectivos");
    var divMediosPago = document.getElementById("divCheckBoxListMediosPago");

    var parametros = JSON.parse(ParametrosJSON);
    var conteudoEfectivo = "<table id='tblEfectivos'>";
    var conteudoMedioPago = "<table id='tblMediosPago'>";

    var PoserEfectivo = 0;
    var PoserMedioPago = 0;

    var CantidadDivisaSelecionada = 0;

    // for divisas JSON
    for (var i = 0; i < parametros.Divisas.length; i++) {

        if (checkboxlist != undefined) {

            for (c = 0; c < checkboxlist.length; c++) {
                // if (checkboxlist[c].checked == true) {

                if (checkboxlist[c].value == parametros.Divisas[i].Identificador) {

                    if (checkboxlist[c].checked == true) {
                        CantidadDivisaSelecionada += 1;
                        parametros.Divisas[i].EsSelecionado = true;
                        if (parametros.Divisas[i].Efectivos != null) {
                            PoserEfectivo = 1;
                            for (var j = 0; j < parametros.Divisas[i].Efectivos.length; j++) {

                                // Validação para valores com mais de 30 caracteres
                                var valor = parametros.Divisas[i].Efectivos[j].Descripcion;
                                var valorTooltip = '';
                                if (parametros.Divisas[i].Efectivos[j].Descripcion.length > 30) {
                                    valor = SubStringJSON(parametros.Divisas[i].Efectivos[j].Descripcion);
                                    valorTooltip = parametros.Divisas[i].Efectivos[j].Descripcion;
                                }

                                var idefectivo = "checkboxefectivo_" + j;
                                conteudoEfectivo += "<tr><td>";
                                if (parametros.Divisas[i].Efectivos[j].EsSelecionado == true) {
                                    conteudoEfectivo += "<input type='checkbox' id=" + idefectivo + " title='" + valorTooltip + "' onchange='AlmacenarEfectivos();RechazarEfectivos();' value=" + parametros.Divisas[i].Efectivos[j].Identificador + " checked> " + valor + "</input>";
                                }
                                else {
                                    conteudoEfectivo += "<input type='checkbox' id=" + idefectivo + " title='" + valorTooltip + "' onchange='AlmacenarEfectivos();RechazarEfectivos();' value=" + parametros.Divisas[i].Efectivos[j].Identificador + "> " + valor + "</input>";
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
                                    conteudoMedioPago += "<input type='checkbox' id=" + idmediopago + " title='" + valorTooltip + "' onchange='AlmacenarMediosPago();RechazarMediosPago();' value=" + parametros.Divisas[i].MediosPago[k].Identificador + " checked> " + valor + "</input>";
                                }
                                else {
                                    conteudoMedioPago += "<input type='checkbox' id=" + idmediopago + " title='" + valorTooltip + "' onchange='AlmacenarMediosPago();RechazarMediosPago();' value=" + parametros.Divisas[i].MediosPago[k].Identificador + " > " + valor + "</input>";
                                }
                                conteudoMedioPago += "</td></tr>";

                            } // for MediosPago
                        } // if MediosPago not nothing
                    } // if checkbox está marcado
                    else {
                        parametros.Divisas[i].EsSelecionado = false;
                        if (parametros.Divisas[i].Efectivos != null) {
                            for (var j = 0; j < parametros.Divisas[i].Efectivos.length; j++) {
                                parametros.Divisas[i].Efectivos[j].EsSelecionado = false;
                            }

                        }
                        if (parametros.Divisas[i].MediosPago != null) {
                            for (var k = 0; k < parametros.Divisas[i].MediosPago.length; k++) {
                                parametros.Divisas[i].MediosPago[k].EsSelecionado = false;
                            }
                        }
                    }
                } //(checkboxlist[c].value == parametros.Divisas[i].Identificador)
            } // for checkboxlist
        } // if checkboxlist not undefined
    } // for divisas

    if (PoserEfectivo == 1) {
        //divEfectivos.style.height = '210px';
        //divEfectivos.style.width = '250px;';
        //divEfectivos.style.border = 'solid 1px';

        conteudoEfectivo += "</table>";

    }
    else {
        //divEfectivos.style.border = 'none';
        // divEfectivos.style.width = 'auto';
        conteudoEfectivo = '';
        if (CantidadDivisaSelecionada == 1) {
            divEfectivos.innerHTML = parametros.sMesajeEfectivo;
            conteudoEfectivo = parametros.sMesajeEfectivo;
        } else if (CantidadDivisaSelecionada > 1) {
            conteudoEfectivo = parametros.pMesajeEfectivo;
        }
        else {
            conteudoEfectivo = parametros.MesajeDivisasNoSelecionadas;
        }
    }

    if (PoserMedioPago == 1) {
        //divMediosPago.style.height = '210px';
        //divMediosPago.style.width = '250px;';
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
        } else {
            conteudoMedioPago = parametros.MesajeDivisasNoSelecionadas;
        }
    }

    divEfectivos.innerHTML = conteudoEfectivo;
    divMediosPago.innerHTML = conteudoMedioPago;

    ParametrosJSON = JSON.stringify(parametros);
    var hdnViewState = $('[id$=hdnViewState]')[0];
    hdnViewState.value = ParametrosJSON;
}

function AlmacenarEfectivos(controle) {

    var parametros = JSON.parse(ParametrosJSON);
    //var parametros = JSON.parse(hdnViewState.value);
    var checkboxlistDivisas = document.getElementById('divCheckBoxListDivisas').getElementsByTagName('input');
    var checkboxlistEfectivos = document.getElementById('divCheckBoxListEfectivos').getElementsByTagName('input');

    var CheckBoxDivisasMarcados = new Array();
    var CheckBoxDivisasDesmarcados = new Array();
    var contadorMarcados = 0;
    var contadorDesmarcados = 0;
    for (index = 0; index < checkboxlistDivisas.length; index++) {
        if (checkboxlistDivisas[index].checked == true) {
            CheckBoxDivisasMarcados[contadorMarcados] = checkboxlistDivisas[index];
            contadorMarcados += 1;
        }
        else {
            CheckBoxDivisasDesmarcados[contadorDesmarcados] = checkboxlistDivisas[index];
            contadorDesmarcados += 1;
        }
    }

    if (CheckBoxDivisasMarcados.length > 0) {
        for (a = 0; a < CheckBoxDivisasMarcados.length; a++) {
            for (var b = 0; b < parametros.Divisas.length; b++) {
                if (CheckBoxDivisasMarcados[a].value == parametros.Divisas[b].Identificador) {
                    parametros.Divisas[b].EsSelecionado = true;
                    if (parametros.Divisas[b].Efectivos != null) {
                        for (var c = 0; c < parametros.Divisas[b].Efectivos.length; c++) {
                            for (var d = 0; d < checkboxlistEfectivos.length; d++) {
                                if (parametros.Divisas[b].Efectivos[c].Identificador == checkboxlistEfectivos[d].value) {
                                    if (checkboxlistEfectivos[d].checked == true) {
                                        parametros.Divisas[b].Efectivos[c].EsSelecionado = true;
                                    }
                                    else {
                                        parametros.Divisas[b].Efectivos[c].EsSelecionado = false;
                                    }

                                }// efectivo JSON = efectivo checkbox
                            }// for checkboxlist EFECTIVOS
                        }// for EFECTIVOS
                    }// se divisa encontrada possui EFECTIVOS
                    break;
                }// encontra divisa marcada com objeto JSON
            }// for divisa JSON
        } // for divisas marcadas
    } // divisas marcadas

    ParametrosJSON = JSON.stringify(parametros);
    var hdnViewState = $('[id$=hdnViewState]')[0];
    hdnViewState.value = ParametrosJSON;
}

function AlmacenarMediosPago() {

    var parametros = JSON.parse(ParametrosJSON);
    //var parametros = JSON.parse(hdnViewState.value);
    var checkboxlistDivisas = document.getElementById('divCheckBoxListDivisas').getElementsByTagName('input');
    var checkboxlistMediosPago = document.getElementById('divCheckBoxListMediosPago').getElementsByTagName('input');

    var CheckBoxDivisasMarcados = new Array();
    var contadorMarcados = 0;
    for (index = 0; index < checkboxlistDivisas.length; index++) {
        if (checkboxlistDivisas[index].checked == true) {
            CheckBoxDivisasMarcados[contadorMarcados] = checkboxlistDivisas[index];
            contadorMarcados += 1;
        }
    }

    if (CheckBoxDivisasMarcados.length > 0) {
        for (a = 0; a < CheckBoxDivisasMarcados.length; a++) {
            for (var b = 0; b < parametros.Divisas.length; b++) {
                if (CheckBoxDivisasMarcados[a].value == parametros.Divisas[b].Identificador) {
                    parametros.Divisas[b].EsSelecionado = true;
                    if (parametros.Divisas[b].MediosPago != null) {
                        for (var c = 0; c < parametros.Divisas[b].MediosPago.length; c++) {
                            for (var d = 0; d < checkboxlistMediosPago.length; d++) {
                                if (parametros.Divisas[b].MediosPago[c].Identificador == checkboxlistMediosPago[d].value) {
                                    if (checkboxlistMediosPago[d].checked == true) {
                                        parametros.Divisas[b].MediosPago[c].EsSelecionado = true;
                                    }
                                    else {
                                        parametros.Divisas[b].MediosPago[c].EsSelecionado = false;
                                    }

                                }// efectivo JSON = efectivo checkbox
                            }// for checkboxlist EFECTIVOS
                        }// for EFECTIVOS
                    }// se divisa encontrada possui EFECTIVOS
                    break;
                }// encontra divisa marcada com objeto JSON
            }// for divisas JSON
        } // for divisas marcadas
    } // divisas marcadas

    ParametrosJSON = JSON.stringify(parametros);
    var hdnViewState = $('[id$=hdnViewState]')[0];
    hdnViewState.value = ParametrosJSON;
}

function RechazarEfectivos() {

    var parametros = JSON.parse(ParametrosJSON);
    //var parametros = JSON.parse(hdnViewState.value);
    var checkboxlistDivisas = document.getElementById('divCheckBoxListDivisas').getElementsByTagName('input');

    var CheckBoxDivisasDesmarcados = new Array();
    var contadorDesmarcados = 0;
    for (index = 0; index < checkboxlistDivisas.length; index++) {
        if (checkboxlistDivisas[index].checked == false) {
            CheckBoxDivisasDesmarcados[contadorDesmarcados] = checkboxlistDivisas[index];
            contadorDesmarcados += 1;
        }
    }

    if (CheckBoxDivisasDesmarcados.length > 0) {
        for (a = 0; a < CheckBoxDivisasDesmarcados.length; a++) {
            for (var b = 0; b < parametros.Divisas.length; b++) {
                if (CheckBoxDivisasDesmarcados[a].value == parametros.Divisas[b].Identificador) {
                    parametros.Divisas[b].EsSelecionado = false;

                    if (parametros.Divisas[b].Efectivos != null) {
                        for (var c = 0; c < parametros.Divisas[b].Efectivos.length; c++) {
                            parametros.Divisas[b].Efectivos[c].EsSelecionado = false;
                        } // for efectivos JSON
                    }// se divisa encontrada possui EFECTIVOS

                    break;

                }// encontra divisa desmarcada com objeto JSON
            }// for divisas JSON
        }// for divisas desmarcadas
    }// divisas desmarcadas 

    ParametrosJSON = JSON.stringify(parametros);
    var hdnViewState = $('[id$=hdnViewState]')[0];
    hdnViewState.value = ParametrosJSON;

}

function RechazarMediosPago() {

    var parametros = JSON.parse(ParametrosJSON);
    //var parametros = JSON.parse(hdnViewState.value);
    var checkboxlistDivisas = document.getElementById('divCheckBoxListDivisas').getElementsByTagName('input');

    var CheckBoxDivisasDesmarcados = new Array();
    var contadorDesmarcados = 0;
    for (index = 0; index < checkboxlistDivisas.length; index++) {
        if (checkboxlistDivisas[index].checked == false) {
            CheckBoxDivisasDesmarcados[contadorDesmarcados] = checkboxlistDivisas[index];
            contadorDesmarcados += 1;
        }
    }

    if (CheckBoxDivisasDesmarcados.length > 0) {
        for (a = 0; a < CheckBoxDivisasDesmarcados.length; a++) {
            for (var b = 0; b < parametros.Divisas.length; b++) {
                if (CheckBoxDivisasDesmarcados[a].value == parametros.Divisas[b].Identificador) {
                    parametros.Divisas[b].EsSelecionado = false;

                    if (parametros.Divisas[b].MediosPago != null) {
                        for (var c = 0; c < parametros.Divisas[b].MediosPago.length; c++) {
                            parametros.Divisas[b].MediosPago[c].EsSelecionado = false;
                        } // for mediospago JSON
                    }// se divisa encontrada possui MEDIOSPAGO

                    break;

                }// encontra divisa desmarcada com objeto JSON
            }// for divisas JSON
        }// for divisas desmarcadas
    }// divisas desmarcadas 

    ParametrosJSON = JSON.stringify(parametros);
    var hdnViewState = $('[id$=hdnViewState]')[0];
    hdnViewState.value = ParametrosJSON;

}

//Por defecto ese campo estará deshabilitado y marcado. Al seleccionar alguna divisa, ese campo si quedará habilitado.
function ReglasControle() {

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
        chkTotalesEfectivos.disabled = true;
        chkTotalesTipoMedioPago.disabled = true;
        chkTotalesEfectivos.checked = true;
        chkTotalesTipoMedioPago.checked = true;

        divEfectivos.innerHTML = parametros.MesajeDivisasNoSelecionadas;
        divMediosPago.innerHTML = parametros.MesajeDivisasNoSelecionadas;


    }
}

// Remover strings a partir da posição 30.
function SubStringJSON(valor) {

    var novovalor = valor.substr(0, 30) + "...";
    return novovalor;

}
