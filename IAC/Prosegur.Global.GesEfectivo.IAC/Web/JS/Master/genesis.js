/* Genesis Script */
/* Variaveis Globais */
var currentDelegacionDateGMT = new Date();

/* Fim Variaveis Globais */

//or as a Number prototype method:
Number.prototype.padLeft = function (n, str) {
    return Array(n - String(this).length + 1).join(str || '0') + this;
}

$(document).ready(function () {
    /* MENU */
    $("#menu").menu();

    /* EFEITO DO MENU */
    $(".menu-trigger img").hover(function () {
        $(".menu-box").show();
    });

    $(".content, .top-navigarion, .footer, .layout-center").on("mouseover", function () {
        $(".menu-box").hide();
    });

   //$(".ui-fieldset-legend").bind("click", toggleFieldSetFunc);

    if (Sys) {
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        if (prm) {
            prm.add_endRequest(endAjaxRequest);
        }
    }

    // Função praticamente duplicado para atender ao novo layout do NovoSaldos. Porém a anterior foi mantida para não atrapalhar lugares que à usam.
    $(".subtitulobar").bind("click", AlternarSubTitulo);

    if (Sys) {
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        if (prm) {
            prm.add_endRequest(endAjaxRequestSubTitulo);
        }
    }

    // Função praticamente duplicado para atender ao novo layout do NovoSaldos. Porém a anterior foi mantida para não atrapalhar lugares que à usam.
    $(".subtituloitembar").bind("click", AlternarSubTituloItem);

    if (Sys) {
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        if (prm) {
            prm.add_endRequest(endAjaxRequestSubTituloItem);
        }
    }

    $("input,select").focus(function () {
        try {
            document.getElementById('__LASTFOCUS').value = this.id;
        }
        catch (e) { }
    });

    configurarTabulacao();
    ConfigurarDropdown();
});

function ConfigurarDropdown() {
    $("select").unbind('keypress').keypress(TratamentosDropdown);
}

function TratamentosDropdown(evento) {
    if (evento.which == 32) {
        evento.preventDefault();
        this.dispatchEvent(new MouseEvent('mousedown'));
    } else {
        var elementos = $(this).data("elementosAtalho");
        if (elementos != null) {
            var caractere = String.fromCharCode(evento.which);
            for (var i = 0; i < elementos.length; i++) {
                if (caractere == elementos[i].CodigoAcceso) {
                    evento.preventDefault();
                    $(this).prop("selectedIndex", i + 1);
                    $(this).change();
                }
            }
        }
    }
}

/**
* Sort arrays of objects by their property names
* @param {String} propName
* @param {Boolean} descending
* Solução encontrada em: 'http://stackoverflow.com/questions/12415722/loop-a-jquery-selection-in-order-of-tab-index'
*/
Array.prototype.sortByObjectProperty = function (propName, descending) {
    return this.sort(function (a, b) {
        if (typeof b[propName] == 'number' && typeof a[propName] == 'number') {
            return (descending) ? b[propName] - a[propName] : a[propName] - b[propName];
        } else if (typeof b[propName] == 'string' && typeof a[propName] == 'string') {
            return (descending) ? b[propName] > a[propName] : a[propName] > b[propName];
        } else {
            return this;
        }
    });
};

function configurarTabulacao() {
    var inputsSelector = ":input[tabindex]:not(:hidden)";
    $(inputsSelector).keydown(function (evt) {
        if (evt.which === 13 || evt.which === 9) {
            var elms = [];
            var targetIndexFocus;

            $(inputsSelector).not(":disabled").each(function (index) {
                elms.push({
                    elm: $(this),
                    tabindex: parseInt($(this).attr("tabindex"))
                });
            });

            elms.sortByObjectProperty('tabindex');

            $(elms).each(function (index) {
                if (this.elm.attr("id") === $(evt.target).attr("id")) {
                    targetIndexFocus = index;
                }
            });

            targetIndexFocus = targetIndexFocus + (evt.shiftKey ? -1 : 1);

            evt.stopPropagation();
            evt.preventDefault();

            if (targetIndexFocus === -1) {
                elms[elms.length - 1].elm.focus();
                return;
            }

            if (targetIndexFocus === elms.length) {
                elms[0].elm.focus();
                return
            }

            elms[targetIndexFocus].elm.focus();
        }
    });
}

function endAjaxRequest(sender, args) {
    $(".ui-fieldset-legend").unbind('click', toggleFieldSetFunc).bind("click", toggleFieldSetFunc);
    $(document).unbind('focusin.dialog');
}

function toggleFieldSetFunc() {
    changeIcon(this, '.ui-fieldset-toggler', 'ui-icon-minusthick', 'ui-icon-plusthick');
    $(this).next().toggle();
}

// Função praticamente duplicado para atender ao novo layout do NovoSaldos. Porém a anterior foi mantida para não atrapalhar lugares que à usam.
function endAjaxRequestSubTitulo(sender, args) {
    $(".subtitulobar").unbind('click', AlternarSubTitulo).bind("click", AlternarSubTitulo);
    $(document).unbind('focusin.dialog');
}

// Função praticamente duplicado para atender ao novo layout do NovoSaldos. Porém a anterior foi mantida para não atrapalhar lugares que à usam.
function endAjaxRequestSubTituloItem(sender, args) {
    $(".subtituloitembar").unbind('click', AlternarSubTituloItem).bind("click", AlternarSubTituloItem);
    $(document).unbind('focusin.dialog');
}

// Função praticamente duplicado para atender ao novo layout do NovoSaldos. Porém a anterior foi mantida para não atrapalhar lugares que à usam.
function AlternarSubTitulo() {
    changeIcon(this, '.subtitulobar-alternar', 'iconesubtitulo-menor', 'iconesubtitulo-major');
    $(this).next().toggle();
}

// Função praticamente duplicado para atender ao novo layout do NovoSaldos. Porém a anterior foi mantida para não atrapalhar lugares que à usam.
function AlternarSubTituloItem() {
    changeIcon(this, '.subtituloitembar-alternar', 'iconesubtituloitem-menor', 'iconesubtituloitem-major');
    $(this).next().toggle();
}
function ExibirMensagemSimNao(mensagem, titulo, scriptSim, btnSim, btnNao) {
    var myButtons = {};
    myButtons[btnSim] = function () {
        setTimeout(scriptSim);
        //__doPostBack(scriptSim, "");
        $(this).dialog('close');
    };
    myButtons[btnNao] = function () {
        $(this).dialog('close');
    };
    ExibirMensagem(mensagem, titulo, "", null, myButtons);
}
function ExibirMensagem(mensagem, titulo, scriptFechar, btnFechar, botoes) {
    
    var erro = "<div style='float: left;'><span class='ui-icon ui-icon-alert' style='float: left; margin: 0 7px 100px 0;'></span></div><div style='float: right; width: 91%'>" + mensagem + "</div>";


    $(document).ready(function () {
        var myButtons = {};
        if (botoes !== null && botoes !== undefined) {
            myButtons = botoes;
        }
        else {
            myButtons[btnFechar] = function () {
                $(this).dialog('close');
            };
        }

        if ($("#dialog-message").length == 0) {
            var texto = "sadfa";
            var $dialog = $('<div id="dialog-message"> <p>  </p></div>');
            
            var focusable;

            $('body').append($dialog);
            $("#dialog-message").dialog({
                modal: true,
                title: titulo,
                resizable: false,
                draggable: true,
                closeText: btnFechar,
                close: function () { clearTimeout(focusable); },
                width: '450',
                height: 'auto',
                buttons: myButtons,
                open: function () {
                    focusable = setTimeout(function () { $(".ui-dialog-buttonset button").get(0).focus() }, 1);
                    // $("#" + btnFechar + "").focus();
                },
                focus: function (event, ui) {
                    focusable = setTimeout(function () { $(".ui-dialog-buttonset button").get(0).focus() }, 1);
                    // $("#" + btnFechar + "").focus();
                }

            });

            $("#dialog-message p").html(erro)
         
        } else {
            $("#dialog-message").dialog("option", "buttons", myButtons);
            $("#dialog-message").dialog("open");
            $("#dialog-message p").html(erro)
        }

        $(".ui-dialog-buttonset button").removeAttr('class').addClass('ui-button').addClass('ui-button-message');
    });

}

function ExibirMensagem2(mensagem, titulo, scriptFechar, btnFechar) {

    var erro = "<div style='float: left;'><span class='ui-icon ui-icon-alert' style='float: left; margin: 0 7px 100px 0;'></span></div><div style='float: right; width: 91%'>" + mensagem + "</div>";


    $(document).ready(function () {

        if ($("#dialog-message").length == 0) {
            var texto = "sadfa";
            var $dialog = $('<div id="dialog-message"> <p>  </p></div>');

            var myButtons = [];
            var btn = {
                id: btnFechar, text: btnFechar, click: function () {
                    $(this).dialog('close');

                }
            }
            myButtons.push(btn);

            var focusable;
            
            $('body').append($dialog);
            $("#dialog-message").dialog({
                modal: true,
                title: titulo,
                resizable: false,
                draggable: true,
                position: { my: "center top", at: "center top", of: window },
                closeText: btnFechar,
                close: function () { clearTimeout(focusable);},
                width: '450',
                height: 'auto',
                buttons: myButtons,
                open: function () {
                    focusable = setTimeout(function () { $(".ui-dialog-buttonset button").get(0).focus() }, 1);
                    // $("#" + btnFechar + "").focus();
                },
                focus: function (event, ui) {
                    focusable = setTimeout(function () { $(".ui-dialog-buttonset button").get(0).focus() }, 1);
                    // $("#" + btnFechar + "").focus();
                }

            });


            $("#dialog-message p").html(erro)

        } else {
            $("#dialog-message").dialog("open");
            $("#dialog-message p").html(erro)
        }

        $(".ui-dialog-buttonset button").removeAttr('class').addClass('ui-button').addClass('ui-button-message');;

    });

}

function ExibirMensagemNaoSim(mensagem, titulo, scriptSim, scriptNao, btnSim, btnNao) {
    
    var myButtons = {};
    myButtons[btnSim] = function () {
        setTimeout(scriptSim);
        $(this).dialog('close');
    };
    myButtons[btnNao] = function () {      
        setTimeout(scriptNao);       
        $(this).dialog('close');
    };
    ExibirMensagem(mensagem, titulo, "", null, myButtons);
}

function validaDelegacionDateGMT() {
    var currentDelegacionDateGMT;
    try {
        if (currentDelegacionDateGMT == "") {
            currentDelegacionDateGMT = new Date();
        } else if (currentDelegacionDateGMT == null) {
            currentDelegacionDateGMT = new Date();
        } else if (isNaN(Date.parse(currentDelegacionDateGMT))) {
            currentDelegacionDateGMT = new Date();
        } else if (Date.parse(currentDelegacionDateGMT) <= 0) {
            currentDelegacionDateGMT = new Date();
        }
    } catch (e) {
        currentDelegacionDateGMT = new Date();
    }
}


function AbrirCalendario(elemento, mostrarHoras,tipoHora) {
    var mostrarHorasBool = eval(mostrarHoras.toLowerCase());
    var mesesName = eval(_meses);
    var diasName = eval(_dias);
    var diasMin1 = eval(_dias);
    var total = diasMin1.length;
    var diasMin = [];
    var currentDate = new Date();

    for (i = 0; i < total; i++) {
        diasMin.unshift(diasMin1.pop().substr(0, 2));
    }


    $(document).ready(function () {
        var elem;
        // Modificado para aceitar o ID do elemento ou o próprio elemento
        if (typeof elemento == "string") {
            elem = $("#" + elemento + "").get(0);
        } else {
            elem = elemento;
        }

        PageMethods.GetDateServerUTC(function (dateUTC) {
            var dtSplit = dateUTC.split(", ");
            var dateServerUTC = new Date(dtSplit[0], dtSplit[1] - 1, dtSplit[2], dtSplit[3], dtSplit[4], dtSplit[5]);
            currentDelegacionDateGMT = dateServerUTC;
            currentDelegacionDateGMT.setMinutes(currentDelegacionDateGMT.getMinutes() + _GMT + _AjusteVerano);
            validaDelegacionDateGMT();
            currentDate = currentDelegacionDateGMT;

            //            if (mostrarHorasBool) {
            //                $(elem).val(currentDate.getDate().padLeft(2) + "/" + (currentDate.getMonth() + 1).padLeft(2) + "/" + currentDate.getFullYear().padLeft(4) + " " + currentDate.getHours().padLeft(2) + ":" + currentDate.getMinutes().padLeft(2) + ":" + currentDate.getSeconds().padLeft(2));
            //            } else {
            //                $(elem).val(currentDate.getDate().padLeft(2) + "/" + (currentDate.getMonth() + 1).padLeft(2) + "/" + currentDate.getFullYear().padLeft(4));
            //            }

        });


        if (mostrarHorasBool) {

            $(elem).attr('maxLength', 19).keypress(function (event) {
                return mask(true, event, this, '##/##/#### ##:##:##', 19);
            }).keyup(function (e) {
                var inputVal = $(this).val();
                var dateReg = /^\d{2}\/\d{2}\/\d{4} \d{2}:\d{2}:\d{2}$/;
                if (!dateReg.test(inputVal)) {
                    //apresentar icone de erro
                    if ($("#error").length == 0) {

                        // $(this).after('<span id="error" class="ui-icon ui-icon-alert"></span>');

                    }
                } else {
                    //$("#error").remove()
                }
            });
        } else {
            $(elem).attr('maxLength', 10).keypress(function (event) {
                return mask(true, event, this, '##/##/####', 10);
            });;
        }

        var corpoDateTime = {
            controlType: 'select',
            showOn: "button",
            constrainInput: false,
            //buttonImage: _imgCalendar != null ? _imgCalendar : "../../App_Themes/Padrao/css/img/button/Calendar.png",
            buttonImage: _imgCalendar != null ? _imgCalendar : "../../Imagenes/Calendar.png",
            dateFormat: "dd/mm/yy",
            timeFormat: "HH:mm:ss",
            altFormat: "dd/mm/yy",
            monthNamesShort: mesesName,
            dayNames: diasName,
            dayNamesMin: diasMin,
            changeMonth: true,
            changeYear: true,
            nextText: _bntProximo,
            prevText: _btnAnterior,
            hourText: _horas,
            minuteText: _minutos,
            secondText: _segundos,
            currentText: _btnAgora,
            closeText: _btnConfirma,
            showTime: false,
            //            defaultValue: function () {
            //                if (mostrarHorasBool) {
            //                    return currentDate.getDate().padLeft(2) + "/" + (currentDate.getMonth() + 1).padLeft(2) + "/" + currentDate.getFullYear().padLeft(4) + " " + currentDate.getHours().padLeft(2) + ":" + currentDate.getMinutes().padLeft(2) + ":" + currentDate.getSeconds().padLeft(2);
            //                } else {
            //                    return currentDate.getDate().padLeft(2) + "/" + (currentDate.getMonth() + 1).padLeft(2) + "/" + currentDate.getFullYear().padLeft(4);
            //                }

            //            },
            beforeShow: function (picker, inst) {
                if (!$(elem).val().toString()) {

                    var url = window.location.pathname;
                    var myPageName = url.substring(url.lastIndexOf('/') + 1);

                    $.ajax({
                        type: "POST",
                        async: false,
                        url: myPageName + "/GetDateServerUTC",
                        data: "{ }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var dtSplit = msg.d.split(", ");
                            var dateServerUTC = new Date(dtSplit[0], dtSplit[1] - 1, dtSplit[2], dtSplit[3], dtSplit[4], dtSplit[5]);
                            currentDelegacionDateGMT = dateServerUTC;
                            currentDelegacionDateGMT.setMinutes(currentDelegacionDateGMT.getMinutes() + _GMT + _AjusteVerano);
                            validaDelegacionDateGMT();
                            currentDate = currentDelegacionDateGMT;

                            if (mostrarHorasBool) {
                                if (tipoHora == 0) {
                                    $(elem).val(currentDate.getDate().padLeft(2) + "/" + (currentDate.getMonth() + 1).padLeft(2) + "/" + currentDate.getFullYear().padLeft(4) + " " + new Date().getHours().padLeft(2) + ":" + new Date().getMinutes().padLeft(2) + ":" + new Date().getSeconds().padLeft(2));
                                } else if (tipoHora == 1) {
                                    $(elem).val(currentDate.getDate().padLeft(2) + "/" + (currentDate.getMonth() + 1).padLeft(2) + "/" + currentDate.getFullYear().padLeft(4) + " 00:00:00");
                                } else {
                                    $(elem).val(currentDate.getDate().padLeft(2) + "/" + (currentDate.getMonth() + 1).padLeft(2) + "/" + currentDate.getFullYear().padLeft(4) + " 23:59:59");
                                }
                            } else {
                                $(elem).val(currentDate.getDate().padLeft(2) + "/" + (currentDate.getMonth() + 1).padLeft(2) + "/" + currentDate.getFullYear().padLeft(4));
                            }
                        }
                    });


                }
            },
            onClose: function (dateText, obj) {
                var DataAtual = new Date();
                DataAtual.setHours(0, 0, 0, 0);

                var currentDate = $(this).datepicker("getDate");

                if (mostrarHoras == true) {

                    if (currentDate != null) {
                        if (DataAtual.toGMTString() != currentDate.toGMTString()) {
                            var mesres = (currentDate.getMonth() + 1) < 10 ? "0" + (currentDate.getMonth() + 1).toString() : (currentDate.getMonth() + 1);
                            var diares = (currentDate.getDate()) < 10 ? "0" + (currentDate.getDate()).toString() : (currentDate.getDate());

                            //$(elem).val(diares + "/" + mesres + "/" + currentDate.getFullYear() + " " + currentDate.getHours() + ":" + currentDate.getMinutes() + ":" + currentDate.getSeconds());
                            //$(this).datepicker("setDate", new Date());
                        } else {
                            DataAtual = new Date();
                            var mesres = (DataAtual.getMonth() + 1) < 10 ? "0" + (DataAtual.getMonth() + 1).toString() : (DataAtual.getMonth() + 1);
                            var diares = (DataAtual.getDate()) < 10 ? "0" + (DataAtual.getDate()).toString() : (DataAtual.getDate());
                            var hourres = (DataAtual.getHours()) < 10 ? "0" + (DataAtual.getHours()).toString() : (DataAtual.getHours());
                            var minres = (DataAtual.getMinutes()) < 10 ? "0" + (DataAtual.getMinutes()).toString() : (DataAtual.getMinutes());
                            var secres = (DataAtual.getSeconds()) < 10 ? "0" + (DataAtual.getSeconds()).toString() : (DataAtual.getSeconds());

                            $(elem).val(diares + "/" + mesres +
                        "/" + DataAtual.getFullYear() + " " + hourres + ":" + minres + ":" + secres);
                        }
                    }
                }

            }

        }
        if (mostrarHorasBool) {
            $(elem).datetimepicker(corpoDateTime);

            $(elem).blur(function () {
                //var parsedDate = Globalize.parseDate($(this).val());
                var parsedDate = Date.parseExact($(this).val(), "dd/MM/yyyy HH:mm:ss");
                if (isNaN(parsedDate) || parsedDate == null) {
                    $(this).val('');
                }
            });

        } else {
            $(elem).datepicker(corpoDateTime);

            $(elem).blur(function () {
                //var parsedDate = Globalize.parseDate($(this).val());
                var parsedDate = Date.parseExact($(this).val(), "dd/MM/yyyy");
                if (isNaN(parsedDate) || parsedDate == null) {
                    $(this).val('');
                }
            });

        }
    });
}



function SelecionarRegistroGridTipoRadio(gridviewID) {
    $(document).ready(function () {

        $("#" + gridviewID + " tr").on("click", function () {

            if ($(this).find("input:radio").length > 0) {


                $("#" + gridviewID + " input:radio:checked").each(function () {
                    this.checked = false;
                });

                $(this).find("input:radio").each(function () {
                    this.checked = true;
                });
            }
        });

    });
}
function checkAll(objRef) {
    var GridView = objRef.parentNode.parentNode.parentNode;
    var inputList = GridView.getElementsByTagName("input");
    for (var i = 0; i < inputList.length; i++) {
        //Get the Cell To find out ColumnIndex
        var row = inputList[i].parentNode.parentNode;
        if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
            if (objRef.checked) {
                //If the header checkbox is checked
                //check all checkboxes
                //and highlight all rows
                //row.style.backgroundColor = "aqua";
                inputList[i].checked = true;
            }
            else {
                //If the header checkbox is checked
                //uncheck all checkboxes
                //and change rowcolor back to original 
                if (row.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    //row.style.backgroundColor = "#C2D69B";
                }
                else {
                    //row.style.backgroundColor = "white";
                }
                inputList[i].checked = false;
            }
        }
    }
}
/* ---------------------------------------------------------------------------------------------------------------------------------------------*/
function Limpar(valor, validos) {
    // retira caracteres invalidos da string
    var result = "";
    var aux;
    for (var i = 0; i < valor.length; i++) {
        aux = validos.indexOf(valor.substring(i, i + 1));
        if (aux >= 0) {
            result += aux;
        }
    }
    return result;
}

//Formata número tipo moeda usando o evento onKeyDown

function Formata(campo, tammax, teclapres, decimal) {
    var tecla = teclapres.keyCode;
    vr = Limpar(campo.value, "0123456789");
    tam = vr.length;
    dec = decimal

    if (tam < tammax && tecla != 8) { tam = vr.length + 1; }

    if (tecla == 8)
    { tam = tam - 1; }

    if (tecla == 8 || tecla >= 48 && tecla <= 57 || tecla >= 96 && tecla <= 105) {

        if (tam <= dec)
        { campo.value = vr; }

        if ((tam > dec) && (tam <= 5)) {
            campo.value = vr.substr(0, tam - 2) + "," + vr.substr(tam - dec, tam);
        }
        if ((tam >= 6) && (tam <= 8)) {
            campo.value = vr.substr(0, tam - 5) + "." + vr.substr(tam - 5, 3) + "," + vr.substr(tam - dec, tam);
        }
        if ((tam >= 9) && (tam <= 11)) {
            campo.value = vr.substr(0, tam - 8) + "." + vr.substr(tam - 8, 3) + "." + vr.substr(tam - 5, 3) + "," + vr.substr(tam - dec, tam);
        }
        if ((tam >= 12) && (tam <= 14)) {
            campo.value = vr.substr(0, tam - 11) + "." + vr.substr(tam - 11, 3) + "." + vr.substr(tam - 8, 3) + "." + vr.substr(tam - 5, 3) + "," + vr.substr(tam - dec, tam);
        }
        if ((tam >= 15) && (tam <= 17)) {
            campo.value = vr.substr(0, tam - 14) + "." + vr.substr(tam - 14, 3) + "." + vr.substr(tam - 11, 3) + "." + vr.substr(tam - 8, 3) + "." + vr.substr(tam - 5, 3) + "," + vr.substr(tam - 2, tam);
        }
    }

}

document.onkeydown = function (e) {
    if (e.which === 27) {
        FecharModalTerminos();
        ModalHistorico(false);
    }
};

function FecharModalTerminos() {
    var divs = $('[id$=dvTerminos]');
    if (divs != undefined) {
        for (i = 0; i < divs.length; i++) {
            divs[i].style.display = 'none';
        }
    }
}

function ModalHistorico(modal) {
    var objHistorico = document.getElementById("dvHistorico");
    if (objHistorico != undefined) {
        if (modal == true) {
            objHistorico.style.display = 'block';
        } else {
            objHistorico.style.display = 'none';
        }
    }
    //if (objHistorico.style.display == 'block') { objHistorico.style.display = 'none'; } else { objHistorico.style.display = 'block'; }

}

/* ---------------------------------------------------------------------------------------------------------------------------------------------*/

//http://bloggingabout.net/blogs/mveken/archive/2008/01/02/performing-async-postback-from-javascript.aspx
function doPostBackAsync(eventName, eventArgs) {
    var prm = Sys.WebForms.PageRequestManager.getInstance();

    if (!Array.contains(prm._asyncPostBackControlIDs, eventName)) {
        prm._asyncPostBackControlIDs.push(eventName);
    }

    if (!Array.contains(prm._asyncPostBackControlClientIDs, eventName)) {
        prm._asyncPostBackControlClientIDs.push(eventName);
    }

    __doPostBack(eventName, eventArgs);
}

function visualizarFiltro(valor, campo) {
    var hdn = $('[id$=hdnExibirModal]')[0];
    hdn.value = valor;
    var objFiltro = document.getElementById(campo);
    objFiltro.style.display = valor;
}

function alteraTeclaEnterPorTeclaTab() {
    $(document).ready(function () {
        $('input, select, a, img, div').keypress(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });
    });

}

function displayElemento(elementoID, valorDisplay) {
    var elemento = document.getElementById(elementoID);
    if (elemento != null)
    {
        elemento.style.display = valorDisplay;
    }
}


function exibirloading(valor) {
    if (valor == "") {
        $("#dvAlertNovo").css("display", "none");
        $("#dvAlertNovoLabel").html("");
    } else {
        $("#dvAlertNovoPainel").removeClass("error");
        $("#dvAlertNovoPainel").addClass("loaging");
        $("#dvAlertNovo").css("display", "block");
        $("#dvAlertNovoLabel").html(valor);
    }
}

function exibirerro(valor) {
    if (valor == "") {
        $("#dvAlertNovo").css("display", "none");
        $("#dvAlertNovoLabel").html("");
    } else {
        $("#dvAlertNovoPainel").removeClass("loaging");
        $("#dvAlertNovoPainel").addClass("error");
        $("#dvAlertNovo").css("display", "block");
        $("#dvAlertNovoLabel").html(valor);
    }
}





function genesisAlertError(msg, erro) {
    genesisAlert(msg, erro, "error", true);
}

function genesisAlertLoading(msg) {
    genesisAlert(msg, "", "loading", false);
}

function genesisAlert(msg, erro, tipo, close) {
    if (msg == "") {
        $("#dvAlert").css("display", "none");
        $("#dvAlertLabel").html("");
        $("#dvAlertErro").html("");
    } else {
        $("#dvAlertPanel").removeClass("loading");
        $("#dvAlertPanel").removeClass("error");
        $("#dvAlertPanel").addClass(tipo);
        $("#dvAlert").css("display", "block");
        $("#dvAlertLabel").html(msg);
        $("#dvAlertErro").html(erro);
        if (close) {
            $("#dvAlertClose").css("visibility", "visible");
        } else {
            $("#dvAlertClose").css("visibility", "hidden");
        }
    }
}

function ExibirMensagemModal(mensagem, titulo) {

    var erro = "<span class='ui-icon ui-icon-alert' style='float: left; margin: 0 7px 100px 0;'></span>" + mensagem;


    $(document).ready(function () {

        if ($("#dialog-message").length == 0) {
            var texto = "sadfa";
            var $dialog = $('<div id="dialog-message"> <p>  </p><center><br><br><img src="../../App_Themes/Padrao/css/img/loader.gif" alt="" /></center></div>');

            $('body').append($dialog);
            $("#dialog-message").dialog({
                dialogClass: "no-close",
                modal: true,
                title: titulo,
                resizable: false,
                draggable: true,
                width: '450',
                height: 'auto'

            });


            $("#dialog-message p").html(erro);

        } else {
            $("#dialog-message").dialog("open");
            $("#dialog-message p").html(erro);
        }

        $(".ui-dialog-buttonset button").removeAttr('class').addClass('ui-button').addClass('ui-button-message');;


    });

}
function ExibirMensagemModal2(mensagem, titulo) {

    var erro = "<span class='ui-icon ui-icon-alert' style='float: left; margin: 0 7px 100px 0;'></span>" + mensagem;


    $(document).ready(function () {

        if ($("#dialog-message").length == 0) {
            var texto = "sadfa";
            var $dialog = $('<div id="dialog-message"> <p>  </p><center><br><br><img src="../../App_Themes/Padrao/css/img/loader.gif" alt="" /></center></div>');

            $('body').append($dialog);
            $("#dialog-message").dialog({
                dialogClass: "no-close",
                modal: true,
                title: titulo,
                position: { my: "center top", at: "center top", of: window },
                resizable: false,
                draggable: true,
                width: '450',
                height: 'auto'

            });


            $("#dialog-message p").html(erro);

        } else {
            $("#dialog-message").dialog("open");
            $("#dialog-message p").html(erro);
        }

        $(".ui-dialog-buttonset button").removeAttr('class').addClass('ui-button').addClass('ui-button-message');;


    });

}

function FecharMensagemModal() {
    
    $(document).ready(function () {

            $("#dialog-message").dialog("close");
        
    });

}