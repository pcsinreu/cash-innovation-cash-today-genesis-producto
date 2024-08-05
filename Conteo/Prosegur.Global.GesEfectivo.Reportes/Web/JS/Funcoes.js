var intSW = screen.width;   // Screen Width
var intSH = screen.height;  // Screen Height
var myWin

/// <summary>
/// Método responsável por abrir uma janela PopUp
/// </summary>
function AbrirJanela(theURL, wname, setarFoco) {

    var intWW = intSW * 0.9; // 90% do tamanho da tela
    var intWH = intSH * 0.9; // 90% do tamanho da tela
    var intWT = (intSH - intWH) / 2;
    var intWL = (intSW - intWW) / 2;

    myWin = window.open(theURL, wname, 'left=' + intWL + ',top=' + intWT + ',width=' + intWW + ',height=' + intWH + ' scrollbars=1');

    if (setarFoco)
        var foco = setTimeout("FocoJanela();", 1500);

}
/// <summary>
/// Método responsável aplicar o foco na janela
/// </summary>
function FocoJanela() {

    try {
        if (myWin != null)
            myWin.focus();
    }
    catch (err)
    { }
}

function disableEnterKey(e) {
    var key;

    if (window.event)
        key = window.event.keyCode;     //IE
    else
        key = e.which;     //firefox

    if (key == 13 && document.activeElement.type != "image")
        return false;
    else
        return true;
}


/// <summary>
/// Método responsável por desabilitar os botões
/// </summary>
function desabilitar_botoes(BotoesClienteIds) {
    // Cria um vetor com os códigos dos botões que foram passados como parametro
    var arrBotoesClienteIds = BotoesClienteIds.split(',')

    // Para cada botão
    for (var i = 0; i < arrBotoesClienteIds.length; i++) {
        // Desabilita o botão
        document.getElementById(arrBotoesClienteIds[i]).disabled = true;
    }
}

/// <summary>
/// Método responsável por aplicar estilo ao botão
/// da framework Prosegur, dependendo de seu estado
/// </summary>
function AlteraEstiloBotao(btnControle, foco, cssNormal, cssFoco) {
    var estilo;

    try {
        if (foco) {
            estilo = cssFoco;
        }
        else {
            estilo = cssNormal;
        }

        btnControle.className = estilo;

    }
    catch (err) {

    }
}

var registro_vigente = true;
function status_registro(status) {
    this.registro_vigente = status;
}

/// <summary>
/// Método responsável por exibir a mensagem de linha preenchida e também de confirmação
/// Nota: Caso não queira exibir mensagem de confirmação passar o parametro como: ''.
/// </summary>
function VerificarRegistroSelecionadoGridView(IdGridView, MsgAlertLinhaPreenchida, MsgConfirmacao) {
    if (document.getElementById('objValorSelecionado_' + IdGridView).value != '') {

        //caso não seja vigente e necessite de confirmação do usuário.
        //Esta validação é apenas para exclusão (por isso utiliza o parametro
        //MsgConfirmacao, pois quando ele não for vazio será uma exclusão), caso 
        //seja outra ação, não entrará no if abaixo.
        if (!this.registro_vigente && MsgConfirmacao != '')
            return false;

        //Verifica se foi passado a mensagem para confirmação               
        if (MsgConfirmacao != '') {
            if (confirm(MsgConfirmacao))
                return true
            else
                return false;
        }
        else {
            return true;
        }
    }
    else {
        alert(MsgAlertLinhaPreenchida);
        return false;
    }
}


/// <summary>
/// Método responsável por exibir a mensagem das linhas selecionados no checkbox e também de confirmação
/// </summary>
function VerificarRegistrosSelecionadosGridView(IdGridView, MsgAlertLinhaPreenchida, MsgLinhasPreenchidas, MsgConfirmacao) {
    try {

        var Gridview = document.getElementById(IdGridView);
        var QteLinhaPreenchida = 0;

        var Elementocheckbox = Gridview.getElementsByTagName("input");
        for (var i = 0; i < Elementocheckbox.length; i++) {
            if (Elementocheckbox[i].type === 'checkbox')
                if (Elementocheckbox[i].checked == true)
                    QteLinhaPreenchida++;

        }

        if (QteLinhaPreenchida == 0) {
            alert(MsgAlertLinhaPreenchida);
        } else if (QteLinhaPreenchida > 1 && MsgLinhasPreenchidas.length > 0) {
            alert(MsgLinhasPreenchidas);
        } else if (MsgConfirmacao.length > 0) {
            if (confirm(MsgConfirmacao))
                return true
            else
                return false;
        } else {
            return true;
        }

    } catch (e) {
        return true;
    }
}

var janela = null;
/// <summary>
/// Método responsável pela exibição dos pop-ups
/// Nota: A variável janela acima é utilizada por este método.
/// </summary>
function AbrirPopup(url, nome, altura, largura, outrosParametros) {

    var esq = (screen.availWidth / 2) - (largura / 2);
    var topo = (screen.availHeight / 2) - (altura / 2);
    var outros = 'left=' + esq + ', top=' + topo + ', width=' + largura + ', height=' + altura + outrosParametros;

    this.janela = window.open(url, nome, outros);
}

function AbrirPopupNova(url, nome, altura, largura, outrosParametros) {

    var esq = (screen.availWidth / 2) - (largura / 2);
    var topo = (screen.availHeight / 2) - (altura / 2);
    var outros = 'left=' + esq + ', top=' + topo + ', width=' + largura + ', height=' + altura + outrosParametros;

    window.open(url, nome, outros);

}

/// <summary>
/// Método responsável por forçar o popup sempre na frente da tela pai.
/// Nota: 
/// </summary>
function modal() {
    if (this.janela && this.janela != null)
        this.janela.focus();
}

/// <summary>
/// Método responsável pela exibição dos pop-ups modal.
/// Nota: 
/// </summary>
function AbrirPopupModal(url, altura, largura) {
    //var parametros = "dialogWidth:" + largura + "px; dialogHeight:" + altura + "px; center:yes"
    //var myArguments = new Object();
    //var r = window.showModalDialog(url, myArguments, parametros);

    //if (typeof r != "undefined")
    //    window.document.forms[0].submit();

    var redimensionavel = 0;
    var nombre = 'popup';
    var WindowRef = null;
    //redimensionavel = (redimensionavel === true ? "yes" : "no");
    if (navigator.appName == 'Netscape') {
        //document.getElementById('dvBloquearTela').style.display = "block";
        var parametros = "width=" + largura + ",height=" + altura + ",centerscreen=yes,resizable=" + redimensionavel + ",maximize=" + redimensionavel + ",minimize=" + redimensionavel;

        WindowRef = window.open(url, nombre, parametros);

        if (!WindowRef.opener) {
            WindowRef.opener = self;
        }

        if (typeof WindowRef != "undefined") {
            $(WindowRef).load(function () {
                $(WindowRef).unload(function () {
                    var valores = null;
                    //document.getElementById('dvBloquearTela').style.display = "none";
                    window.document.forms[0].submit();
                });
            });
        };
    } else {
        var parametros = "dialogWidth:" + largura + "px; dialogHeight:" + altura + "px; center:yes; resizable:" + redimensionavel + ";maximize:" + redimensionavel + ";minimize:" + redimensionavel;
        var myArguments = self;
        var WindowRef = window.showModalDialog(url, myArguments, parametros);

        if (typeof WindowRef != "undefined")
            doPostBackAsync("__Page", WindowRef);
    };
}

/// <summary>
/// Valida se o valor do campo foi alterado caso contrário, mantem o foco até que o valor do controle seja alterado
/// Nota: 
/// </summary>
var acionar = true
function ValidaCampo(valor, obj) {
    if (valor != '') {
        if (obj.value == valor) {
            obj.focus()
            acionar = false;
            return false;
        }
        else {
            acionar = true;
            return true;
        }
    }
    else {
        return true;
    }
}

/// <summary>
/// 
/// Nota: 
/// </summary>
function PodeAcionar() {
    return acionar
}

/// <summary>
/// 
/// Nota: 
/// </summary>
function ValorNumerico(evt) {
    var evento = window.Event ? true : false;
    //var tecla = evento ? evt.which : evt.keyCode;
    tecla = evt.keyCode;
    return (tecla <= 13 || (tecla >= 48 && tecla <= 57));
}

/// <summary>
/// formata para moeda 
/// Nota: 
/// </summary>
function moeda(evt, z) {

    var evento = window.Event ? true : false;
    var tecla = evento ? evt.which : evt.keyCode;

    var teclaPresionada = String.fromCharCode(tecla);

    //bloqueia letras    
    if (!((teclaPresionada >= "0") && (teclaPresionada <= "9"))) {
        window.event.keyCode = 0;
    }

    //Se for tabulação cancela a formatação
    if (tecla == 9) {
        return false;
    }

    v = z.value;
    v = v.replace(/\D/g, "") //permite digitar apenas números
    v = v.replace(/[0-9]{15}/, "") //limita pra máximo 999.999.999.999,99
    v = v.replace(/(\d{1})(\d{11})$/, "$1.$2") //coloca ponto antes dos últimos 8 digitos
    v = v.replace(/(\d{1})(\d{8})$/, "$1.$2") //coloca ponto antes dos últimos 8 digitos
    v = v.replace(/(\d{1})(\d{5})$/, "$1.$2") //coloca ponto antes dos últimos 5 digitos
    v = v.replace(/(\d{1})(\d{1,2})$/, "$1,$2") //coloca virgula antes dos últimos 2 digitos
    z.value = v;
}


/// <summary>
/// Método responsável por exibir a mensagem de linha preenchida e também de confirmação
/// Nota: Caso não queira exibir mensagem de confirmação passar o parametro como: ''  
/// </summary>
function VerificarConfirmacaoCanelamento(MsgConfirmacao) {
    if (confirm(MsgConfirmacao))
        return true;
    else
        return false;
}

function replaceAll(str, de, para) {
    var pos = str.indexOf(de);
    while (pos > -1) {
        str = str.replace(de, para);
        pos = str.indexOf(de);
    }
    return (str);
}

/// <summary>
/// Pega os registro selecionados no grid e armazena no campo hidden.
/// Nota: 
/// </summary>
function TerminoIacSelecionado(obj, oid, id) {
    var selecionados = document.getElementById(id).value;
    selecionados = replaceAll(selecionados, ",", "|");

    if (obj.checked == true) {
        if (selecionados != '') {
            if (document.getElementById(id).value == undefined) {
                selecionados = oid;
            }
            else {

                var aux = selecionados.split("|");
                var existe = false;
                for (i = 0; i < aux.length; i++) {
                    if (aux[i] == oid) {
                        existe = true;
                    }
                }

                if (!existe) {
                    selecionados = selecionados + '|' + oid;
                }
            }
        }
        else {
            selecionados = oid;
        }
    }
    else {
        var aux = selecionados.split("|");
        selecionados = '';
        for (i = 0; i < aux.length; i++) {
            if (aux[i] != oid) {
                if (selecionados != '') {
                    selecionados = selecionados + '|' + aux[i];
                } else {
                    selecionados = aux[i];
                }
            }
        }
    }

    document.getElementById(id).value = replaceAll(selecionados, ",", "|");
}

/// <summary>
/// Adiciona o valor do registro em dois campos hiddens diferentes.
/// Nota: 
/// </summary>
function TerminoIac(obj, oid, id, oidtermino, idtermino) {
    var selecionados = document.getElementById(id).value;
    var terminos = document.getElementById(idtermino).value;

    if (obj.checked == true) {
        if (selecionados != '') {
            if (document.getElementById(id).value == undefined) {
                selecionados = oid;
                terminos = oidtermino;
            }
            else {
                selecionados = selecionados + '|' + oid;
                terminos = terminos + '|' + oidtermino;
            }
        }
        else {
            selecionados = oid;
            terminos = oidtermino;
        }
    }
    else {
        var aux = selecionados.split("|");
        var auxtermino = terminos.split("|");

        selecionados = '';
        terminos = '';
        for (i = 0; i < aux.length; i++) {
            if (aux[i] != oid) {
                if (selecionados != '') {
                    selecionados = selecionados + '|' + aux[i];
                    terminos = terminos + '|' + auxtermino[i];
                }
                else {
                    selecionados = aux[i];
                    terminos = auxtermino[i];
                }
            }
        }
    }

    document.getElementById(id).value = selecionados;
    document.getElementById(idtermino).value = terminos;

}

/// <summary>
/// Armazenda duas colunas do grid em campos hiddens verdadeiros e falso, separando os valores verdadeiros e falsos.
/// Nota: 
/// </summary>
function ResultadosTrueOrFalse(obj, oidcampo, idVerdadeiro, idFalso, hidVerdadeiro, hidFalso) {
    executarlinha = false
    var Verdadeiros = document.getElementById(idVerdadeiro).value;
    var Falsos = document.getElementById(idFalso).value;
    var ValorVerdadeiro = document.getElementById(hidVerdadeiro).value;
    var ValorFalso = document.getElementById(hidFalso).value;

    if (obj.checked == true) {
        if (Verdadeiros != '') {
            if (document.getElementById(idVerdadeiro).value == undefined) {

                Verdadeiros = oidcampo;
                ValorVerdadeiro = oidcampo;
            }
            else {
                Verdadeiros = Verdadeiros + '|' + oidcampo;
                ValorVerdadeiro = ValorVerdadeiro + '|' + oidcampo;
            }
        }
        else {
            Verdadeiros = oidcampo;
            ValorVerdadeiro = oidcampo;
        }

        var auxfalso = Falsos.split("|");
        var auxvalorfalso = ValorFalso.split("|");
        Falsos = '';
        ValorFalso = '';

        for (i = 0; i < auxfalso.length; i++) {
            if (auxfalso[i] != oidcampo) {
                if (Falsos != '') {
                    Falsos = Falsos + '|' + auxfalso[i];
                    ValorFalso = ValorFalso + '|' + auxvalorfalso[i];
                }
                else {
                    Falsos = auxfalso[i];
                    ValorFalso = auxvalorfalso[i];
                }
            }
        }
    }
    else {
        if (Falsos != '') {
            if (document.getElementById(idFalso).value == undefined) {
                Falsos = oidcampo;
                ValorFalso = oidcampo;
            }
            else {
                Falsos = Falsos + '|' + oidcampo;
                ValorFalso = ValorFalso + '|' + oidcampo;
            }
        }
        else {
            Falsos = oidcampo;
            ValorFalso = oidcampo;
        }

        var auxverdadeiro = Verdadeiros.split("|");
        var auxvalorverdadeiro = ValorVerdadeiro.split("|");
        Verdadeiros = '';
        ValorVerdadeiro = '';

        for (i = 0; i < auxverdadeiro.length; i++) {
            if (auxverdadeiro[i] != oidcampo) {
                if (Verdadeiros != '') {
                    Verdadeiros = Verdadeiros + '|' + auxverdadeiro[i];
                    ValorVerdadeiro = ValorVerdadeiro + '|' + auxvalorverdadeiro[i];
                }
                else {
                    Verdadeiros = auxverdadeiro[i];
                    ValorVerdadeiro = auxvalorverdadeiro[i];
                }
            }
        }
    }

    document.getElementById(idVerdadeiro).value = Verdadeiros;
    document.getElementById(idFalso).value = Falsos;
    document.getElementById(hidVerdadeiro).value = ValorVerdadeiro;
    document.getElementById(hidFalso).value = ValorFalso;
}

/// <summary>
/// Marcha o checkbox clicando na linha do grid.
/// Nota: 
/// </summary>
var executarlinha = true;
function SelecionaCheckBox(objid, oid, id) {
    var obj = document.getElementById(objid);

    if (executarlinha) {
        if (obj.checked) {
            obj.checked = false;
            TerminoIacSelecionado(obj, oid, id);
        }
        else {
            obj.checked = true;
            TerminoIacSelecionado(obj, oid, id);
        }
    }
    else {
        executarlinha = true;
        TerminoIacSelecionado(obj, oid, id);
    }
}

/// <summary>
/// Marca o registro selecionado clicando na linha do grid.
/// Nota: 
/// </summary>
function SelecionaRegistroGrid(oid, id) {

    var objHd = document.getElementById(id);

    if (objHd.value == oid)
        objHd.value = "";
    else
        objHd.value = oid;
}

/// <summary>
/// Marca o checkbox quando é clicado na linha do grid. 
/// </summary>
function AlteraEstadoCheckBox(objid) {
    var obj = document.getElementById(objid);

    if (executarlinha) {
        obj.checked = !obj.checked;
    }
    else {
        executarlinha = true;
    }
}


/// <summary>
/// Seleciona o checkbox clicando na linha do grid.
/// Nota: 
/// </summary>
function SelecionaCheckBoxTerminosIac(objid, oid, id, oidtermino, idtermino) {
    var obj = document.getElementById(objid);

    if (executarlinha) {

        if (obj.checked) {
            obj.checked = false;
            TerminoIac(obj, oid, id, oidtermino, idtermino);
        }
        else {
            obj.checked = true;
            TerminoIac(obj, oid, id, oidtermino, idtermino);
        }
    }
    else {
        executarlinha = true;
        TerminoIac(obj, oid, id, oidtermino, idtermino);
    }

}

/// <summary>
/// 
/// Nota: 
/// </summary>
function MensagemItemSelecionado(obj, id, msgconfirmacao) {
    var selecionados = document.getElementById(id).value;

    if (selecionados = '') {
        if (document.getElementById(id).value == undefined) {
            alert(msgconfirmacao);
        }
    }
}

/// <summary>
/// Desabilita um objeto da tela.
/// Nota: 
/// </summary>
function DesabilitaBotaoProsegur(obj) {
    if (obj.disabled == false) {
        obj.disabled = true;
        return true;
    }
    else {
        return false;
    }
}

/// <summary>
/// Desabilita um objeto da tela.
/// Nota: 
/// </summary>
function ConfigurarBotaoProsegurById(objid, disabled) {

    var obj = document.getElementById(objid);

    if (disabled == 1) {
        obj.disabled = true;
    }
    else {
        obj.disabled = false;
    }

}

/// <summary>
/// Lista os controles da tela.
/// Nota: 
/// </summary>
var _listaControles = new Array();
var indice = 0;
function DesabilitaControles(Objeto) {
    if (Objeto != null) {
        _listaControles[indice] = Objeto;
        indice++;
    }
}

/// <summary>
/// Desabilita todos os controles informados na função _listaControles.
/// Nota: 
/// </summary>
function Desabilitar() {
    for (i = 0; i < indice; i++) {
        _listaControles[i].disabled = true;
    }
}

/// <summary>
/// Armazena o valor do checkbox.
/// Nota: 
/// </summary>
function ArmazenarValor(obj, id) {
    var dados;

    if (obj.checked) {
        dados = 'True';
    } else {
        dados = 'False';
    }

    document.getElementById(id).value = dados;

}

/// <summary>
/// Verfica se algum valor e diferente do inicial.
/// Nota: 
/// </summary>
var objArrayObjetoaValidar = new Array();
var objArrayValoresObjetosInicial = new Array();
function VerificarObjetosMoficados(objArrayObjetoaValidar, objArrayValoresObjetosInicial) {

    var objValorCorrente;
    var objValorInicial;
    var situacao = false;

    for (i = 0; i < objArrayObjetoaValidar.length; i++) {
        objValorCorrente = document.getElementById(objArrayObjetoaValidar[i]).value;
        objValorInicial = document.getElementById(objArrayValoresObjetosInicial[i]).value;

        if (objValorCorrente != objValorInicial) {
            situacao = true;
        }
    }

    return situacao;
}

/// <summary>
/// Mostra mensagem de cancelamento atraves do resultado obtido da função VerificarObjetosModificados.
/// Nota: 
/// </summary>
function VerificarCanelamento(MsgConfirmacao, bolconf) {

    if (bolconf) {
        if (confirm(MsgConfirmacao)) {
            return true;
        }
        else {
            return false;
        }
    }
    else {
        return true;
    }
}

/// <summary>
/// Aciona a tecla informada ao pressionar o enter.
/// Nota: 
/// </summary>

function TeclaEnter(idBotao) {
    if (event.which || event.keyCode) {
        if ((event.which == 13) || (event.keyCode == 13)) {
            document.getElementById(idBotao).click();
            return false;
        }
    }
    else {
        return true;
    }
}

/// <summary>
/// Seleciona o texto do textbox
/// Nota: 
/// </summary>
function SelecionarTextBox(obj) {
    document.getElementById(obj).focus();
    document.getElementById(obj).select();
}

/// <summary>
/// Muda o foco para o campo informado ao pressionar a tecla Enter
/// Nota: 
/// </summary>
function TrataEnterTab(idControle) {
    if (event.which || event.keyCode) {
        if ((event.which == 13) || (event.keyCode == 13)) {
            document.getElementById(idControle).focus();
            return false;
        }
    }
    else {
        return true;
    }
}

/// <summary>
/// Mascara para data
/// </summary>
function mask(isNum, event, field, mask, maxLength) {

    var keyCode;
    if (event.srcElement)
        keyCode = event.keyCode;
    else if (event.target)
        keyCode = event.which;

    var maskStack = new Array();

    var isDynMask = false;
    if (mask.indexOf('[') != -1)
        isDynMask = true;

    var length = mask.length;

    for (var i = 0; i < length; i++)
        maskStack.push(mask.charAt(i));

    var value = field.value;
    var i = value.length;

    if (keyCode == 0 || keyCode == 8)
        return true;

    //código adaptado para aceitar X (maiúsculo) ou x (minúsculo), além de números
    if (isNum && (keyCode < 48 || keyCode > 57) && (keyCode != 88) && (keyCode != 120)) {

        event = event || window.event;  // cross-browser event

        if (event.stopPropagation) {
            // W3C standard variant
            event.stopPropagation();
        } else {
            // IE variant
            event.cancelBubble = true;
        }
        return false;
    }

    if (!isDynMask && i < length) {

        if (maskStack.toString().indexOf(String.fromCharCode(keyCode)) != -1 && keyCode != 8) {
            return false;
        } else {
            if (keyCode != 8) {
                if (maskStack[i] != '#') {
                    var old = field.value;
                    field.value = old + maskStack[i];
                }
            }

            //			if (autoTab(field, keyCode, length)) {
            //				if (!document.layers) {
            //					return true;
            //				} else if (keyCode != 8) {
            //					field.value += String.fromCharCode(keyCode);
            //					return false;
            //				} else {
            //					return true;
            //				}
            //			} else {
            //				return false;
            //			}				
        }

    } else if (isDynMask) {

        var maskChars = "";
        for (var j = 0; j < maskStack.length; j++)
            if (maskStack[j] != '#' && maskStack[j] != '[' && maskStack[j] != ']')
                maskChars += maskStack[j];

        var tempValue = "";
        for (var j = 0; j < value.length; j++) {
            if (maskChars.indexOf(value.charAt(j)) == -1)
                tempValue += value.charAt(j);
        }

        value = tempValue + String.fromCharCode(keyCode);

        if (maskChars.indexOf(String.fromCharCode(keyCode)) != -1) {
            return false;
        } else {

            var staticMask = mask.substring(mask.indexOf(']') + 1);
            var dynMask = mask.substring(mask.indexOf('[') + 1, mask.indexOf(']'));

            var realMask = new Array;

            if (mask.indexOf('[') == 0) {
                var countStaticMask = staticMask.length - 1;
                var countDynMask = dynMask.length - 1;
                for (var j = value.length - 1; j >= 0; j--) {
                    if (countStaticMask >= 0) {
                        realMask.push(staticMask.charAt(countStaticMask));
                        countStaticMask--;
                    }
                    if (countStaticMask < 0) {
                        if (countDynMask >= 0) {
                            if (dynMask.charAt(countDynMask) != '#') {
                                realMask.push(dynMask.charAt(countDynMask));
                                countDynMask--;
                            }
                        }
                        if (countDynMask == -1) {
                            countDynMask = dynMask.length - 1;
                        }
                        realMask.push(dynMask.charAt(countDynMask));
                        countDynMask--;
                    }
                }
            }

            var result = "";

            var countValue = 0;
            while (realMask.length > 0) {
                var c = realMask.pop();
                if (c == '#') {
                    result += value.charAt(countValue);
                    countValue++;
                } else {
                    result += c;
                }
            }

            field.value = result;

            if (maxLength != undefined && value.length == maxLength) {

                var form = field.form;
                for (var i = 0; i < form.elements.length; i++) {
                    if (form.elements[i] == field) {
                        field.blur();
                        //if alterado para quando a máscara for utilizada no último campo, não dê mensagem de erro quando tentar colocar o foco no "Salvar"
                        //if (form.elements[i + 1] != null)										 
                        if ((form.elements[i + 1] != null) && (form.elements[i + 1].name != "METHOD"))
                            form.elements[i + 1].focus();
                        break;
                    }
                }
            }

            return false;
        }
    } else {
        return false;
    }
    function autoTab(field, keyCode, length) {
        var i = field.value.length;

        if (i == length - 1) {

            field.value += String.fromCharCode(keyCode);

            var form = field.form;
            for (var i = 0; i < form.elements.length; i++) {
                if (form.elements[i] == field) {
                    field.blur();
                    //if alterado para quando a máscara for utilizada no último campo, não dê mensagem de erro quando tentar colocar o foco no "Salvar"
                    //if (form.elements[i + 1] != null)
                    if ((form.elements[i + 1] != null) && (form.elements[i + 1].name != "METHOD"))
                        form.elements[i + 1].focus();
                    break;
                }
            }

            return false;
        } else {
            return true;
        }
    }
}

/// <summary>
/// Evento aciona os botões informados ao pressionar enter e esc.
/// Nota: 
/// </summary>
function EventoEnter(idEnter) {

    if (event.which || event.keyCode) {
        if ((event.which == 13) || (event.keyCode == 13)) {
            document.getElementById(idEnter).focus();
            document.getElementById(idEnter).click();
            return false;
        } else { return true; }

    } else { return true; }
}

/// configura evento onblur de um controle da tela (configura proximo controle que receberá o foco)
function SetarProximoFoco(IdControleRetorno, eTelaLogin) {

    var linkDetalhes = document.getElementById(idControleErro);
    var proximoControle = document.getElementById(IdControleRetorno);

    if (eTelaLogin == 'True') {

        if (linkDetalhes == undefined) {
            proximoControle.focus();
        }
        else {
            linkDetalhes.focus();
        }
    }
    else {
        proximoControle.focus();
    }

}

function HabilitarDesabilitarCheckbox(textBoxDesde, textBoxHasta, checkbox) {
    var txtDesde = document.getElementById(textBoxDesde);
    var txtHasta = document.getElementById(textBoxHasta);
    var chkbox = document.getElementById(checkbox);

    if (txtDesde.value == '' && txtHasta.value == '') {
        chkbox.disabled = true;
        chkbox.checked = false;
    }
    else {
        chkbox.disabled = false;
    }


}

/// <summary>
/// Obtem o gridview por ClientID.
/// </summary>
function getGridViewControl(gridViewId) {
    return document.getElementById(gridViewId);
}

/// <summary>
/// Obtem linha do gridview por ClientID e indice da linha.
/// </summary>
function getGridRow(gridViewId, rowIdx) {
    gridViewCtl = getGridViewControl(gridViewId);
    if (null != gridViewCtl) {
        return gridViewCtl.rows[rowIdx];
    }
    return null;
}

/// <summary>
/// Obtem célula do gridview por ClientID, indice da linha e da coluna.
/// </summary>
function getGridCell(gridViewId, rowIdx, colIdx) {
    var gridRow = getGridRow(gridViewId, rowIdx);
    if (null != gridRow) {
        return gridRow.cells[colIdx];
    }
    return null;
}

/// <summary>
/// Obtem texto da célula do gridview por ClientID, indice da linha e da coluna.
/// </summary>
function getCellValue(gridViewId, rowIdx, colIdx) {
    var gridCell = getGridCell(gridViewId, rowIdx, colIdx);
    if (null != gridCell) {
        return gridCell.innerText;
    }
    return null;
}

/// <summary>
/// Obtem a lorgura(offsetWidth) da célula do gridview por ClientID, indice da linha e da coluna.
/// </summary>
function getCellWidth(gridViewId, rowIdx, colIdx) {
    var gridCell = getGridCell(gridViewId, rowIdx, colIdx);
    if (null != gridCell) {
        return gridCell.offsetWidth - 1;
    }
    return null;
}

///<sumary>
/// Valida Data no Formato: ##/##/####
///</sumary>
function mascaraData(campoData) {
    var data = campoData.value;

    if (data.length == 2) {
        data = data + '/';
        campoData.value = data;
        return true;
    }
    if (data.length == 5) {
        data = data + '/';
        campoData.value = data;
        return true;
    }

    if (data.length == 10) {
        return campoData.value = data.substr(0, data.length - 1);
    }
    else if (data.length > 10) {
        return campoData.value = data.substr(0, data.length - 2);
    }
}



function RegistrarPopup(elementoPopup, autoOpen, height, width, modal, resizable, draggable, closeText, closeOnEscape, title, executeOnClose) {
    $(document).ready(function () {
        var elem = "#" + elementoPopup + "";
        //        $(elem).dialog({
        //            autoOpen: autoOpen,
        //            height: height,
        //            width: width,
        //            modal: modal,
        //            resizable: resizable,
        //            draggable: draggable,
        //            closeText: closeText,
        //            closeOnEscape: closeOnEscape,
        //            title: title,
        //            close: function (event, ui) {
        //                eval(executeOnClose);
        //                $('body').unbind("keypress");
        //            },
        //            open: function (event, ui) {
        //                $('body').bind("keypress", function (e) { var code = e.keyCode || e.which; if (e.keyCode == 13) return false; });
        //                
        //            }
        //        });

    });
}

function SelecionarTodosCheckBoxList(elemento, todos) {
    $(document).ready(function () {

        $("#" + elemento + " input:checkbox").change(function () {
            var selecionado = $("#" + todos);
            if (selecionado.prop('checked')) {
                $(selecionado).each(function () {
                    this.checked = false;
                });
            }
        });
        $("#" + todos).change(function () {
            if ($(this).prop('checked')) {
                $("#" + elemento + " input:checkbox").each(function () {
                    this.checked = true;
                });
            } else {
                $("#" + elemento + " input:checkbox").each(function () {
                    this.checked = false;
                });
            }
        });
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



function SelecionarRegistroGridTipoCheckBox(gridviewID, postBack) {
    $(document).ready(function () {

        $("#" + gridviewID + " tr").on("click", function (event) {

            if ($(this).find("input:checkbox").length > 0) {

                $(this).find("input:checkbox").each(function () {
                    if (!$(event.target).is("input:checkbox")) {
                        if (!$(this).attr("disabled")) {

                            if (this.checked) {
                                this.click();
                                this.checked = false;
                                $(this).attr("checked", false);
                            } else {
                                this.click();
                                this.checked = true;
                                $(this).attr("checked", true);
                            }
                        }

                    }
                });
            }

            //            eval(postBack);
        });

    });
}

//Exibe mensagem popup
function ExibirMensagem(mensagem, titulo, scriptFechar, btnFechar) {

    var erro = "<span class='ui-icon ui-icon-alert' style='float: left; margin: 0 7px 50px 0;'></span>" + mensagem;


    $(document).ready(function () {

        if ($("#dialog-message").length == 0) {
            var texto = "sadfa";
            var $dialog = $('<div id="dialog-message"> <p>  </p></div>');

            var myButtons = {};
            myButtons[btnFechar] = function () {
                $(this).dialog('close');

            };

            $('body').append($dialog);
            $("#dialog-message").dialog({
                modal: true,
                title: titulo,
                resizable: false,
                draggable: true,
                closeText: btnFechar,
                close: function () { eval(scriptFechar); },
                width: '450',
                height: 'auto',
                buttons: myButtons

            });


            $("#dialog-message p").html(erro)

        } else {
            $("#dialog-message").dialog("open");
            $("#dialog-message p").html(erro)
        }

        $(".ui-dialog-buttonset button").removeAttr('class').addClass('ui-button').addClass('ui-button-message'); ;


    });

}

