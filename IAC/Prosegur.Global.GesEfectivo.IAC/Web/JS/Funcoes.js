//addRange
function addRange(arrayDestino, arrayOrigem) {
    try {
        for (var i = 0; i < arrayOrigem.length; i++) {
            var item = arrayOrigem[i];
            arrayDestino.push(item);
        }
        return true;
    }
    catch (err) {
        return false;
    }
}


//trim completo
function trim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}

//left trim
function ltrim(str) {
    return str.replace(/^\s+/, "");
}

//right trim
function rtrim(str) {
    return str.replace(/\s+$/, "");
}

// Checa se o elemento esta visivel
function isVisible(e) {
    //returns true is should be visible to user.
    if (typeof e == "string") {
        e = xGetElementById(e);
    }
    while (e.nodeName.toLowerCase() != 'body' && e.style.display.toLowerCase() != 'none' && e.style.visibility.toLowerCase() != 'hidden') {
        e = e.parentNode;
    }
    if (e.nodeName.toLowerCase() == 'body') {
        return true;
    } else {
        return false;
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


// -----------------------------------------------------------------------------
// <summary>
// Formata a data no formato informado
// </summary>
// <param name="campo">Campo onde é digitado a data</param>
// <param name="evt">Evento do campo</param>
// <param name="formato">Formato da data (Atualmente só suporta dd/mm/yyyy ou mm/dd/yyyy)</param>
// <param name="sep">Separador usado na data</param>
// <history>
// 	[rafael.gans]	09/05/2006	Created
// </history>
// -----------------------------------------------------------------------------
function formatarData(campo, evt, formato, sep) {
    var tecla = evento ? evt.which : evt.keyCode;
    var valor = parseInt(tecla) - 48;
    var tam = campo.value.length + 1;
    var eDia = formato.substr(tam - 1, 1) == 'd' ? true : false
    var error = true;

    try {
        //Se é valor numérico
        if (ValorNumerico(evt)) {
            //Para não aceitar 00
            if (tam == 2 || tam == 5)
                if (campo.value.substr(tam - 2, 1) == 0 && valor == 0) error = false;

            if (eDia) {
                //Se é o primeiro digito do dia
                if (tam == 1 || tam == 4) {
                    if (valor > 3) error = false;
                } else if (tam == 2 || tam == 5) {
                    if (campo.value.substr(tam - 2, 1) == 3 && valor > 1) error = false;
                }
            } else {
                //Se é o primeiro digito do mês
                if (tam == 1 || tam == 4) {
                    if (valor > 1) error = false;
                } else if (tam == 2 || tam == 5) {
                    if (campo.value.substr(tam - 2, 1) == 1 && valor > 2) error = false;
                }
            }
        } else { error = false; }

        //Se passou em todas validações e é a posição 2 ou 5 adiciona o separador
        if (error) {
            if (tam == 2 || tam == 5) {
                campo.value += String(valor) + String(sep);
                error = false;
            }
        }
    } catch (ex) { error = false; }

    return error;
}

// -----------------------------------------------------------------------------
// <summary>
// Valida se a data informada é válida para o formato informado
// </summary>
// <param name="campo">Campo onde é digitado a data</param>
// <param name="formato">Formato da data (Atualmente só suporta dd/mm/yyyy ou mm/dd/yyyy)</param>
// <param name="msgdatainvalida">Mensagem a ser exibida quando a data for inválida</param>
// <history>
// 	[rafael.gans]	09/05/2006	Created
// </history>
// -----------------------------------------------------------------------------
function validarData(campo, formato, msgdatainvalida) {
    var dia, mes, ano;
    var vr = campo.value;

    if (vr == '') return true;

    try {
        // Recupera o dia, mês e ano para testarmos
        dia = vr.substr(formato.indexOf('d'), 2);
        mes = vr.substr(formato.indexOf('M'), 2);
        ano = vr.substr(formato.indexOf('y'), 4);

        var error = false;

        // Verifica se o dia, mês e ano são números
        // Depois testa se o ano é válido (entre 1850 e 3000), se o mês é válido e se o dia
        // existe no mês/ano informado
        if (IsNumeric(dia) == false) error = true;
        else if (IsNumeric(mes) == false) error = true;
        else if (IsNumeric(ano) == false) error = true;
        else if ((ano < 1850) || (ano > 3000)) error = true;
        else if (parseInt(mes, 10) < 1 || parseInt(mes, 10) > 12) error = true;
        else if (parseInt(dia, 10) == 0 || parseInt(dia, 10) > MaiorDiaMes(parseInt(mes, 10) - 1, parseInt(ano, 10))) error = true;

    } catch (e) { error = true; }

    // Caso tenha ocorrido erro, exibe mensagem e volta o foco para o campo
    if (error == true) {
        alert(msgdatainvalida);
        campo.value = '';
        campo.focus();
        return false
    }
    else return true;
}

var registro_vigente = true;
function status_registro(status) {
    this.registro_vigente = status;
}

/// <summary>
/// Método responsável por exibir a mensagem de linha preenchida e também de confirmação
/// Nota: Caso não queira exibir mensagem de confirmação passar o parametro como: ''.
/// </summary>
function VerificarRegistroSelecionadoGridView(IdGridView, MsgAlertLinhaPreenchida, MsgConfirmacao, MsgRegistroBorrado) {
    if (document.getElementById('objValorSelecionado_' + IdGridView).value != '') {

        //caso não seja vigente e necessite de confirmação do usuário.
        //Esta validação é apenas para exclusão (por isso utiliza o parametro
        //MsgConfirmacao, pois quando ele não for vazio será uma exclusão), caso 
        //seja outra ação, não entrará no if abaixo.
        if (!this.registro_vigente && MsgConfirmacao != '') {
            alert(MsgRegistroBorrado);
            return false;
        }
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

function FecharAtualizarPaginaPai() {
    window.close();
    AtualizarPaginaPai();
}

function AtualizarPaginaPai() {
    if (window.opener != null)
        window.opener.document.forms[0].submit();
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
function AbrirPopupModal(url, altura, largura, nombre) {

    var isSessionExpired = funcIsSessionExpired(url, altura, largura, nombre);

}
function funcIsSessionExpired(url, altura, largura, nombre) {
    //PageMethods em Prosegur.Global.GesEfectivo.IAC.Web.Base.isSessionExpired()
    PageMethods.isSessionExpired(OnSucceeded, OnFailed);
    function OnSucceeded(result, usercontext, methodName) {
        retorna(result, url);

        function retorna(response, url) {

            if (response != undefined) {
                if (response == '' || !response)
                    AbrirPopupModalNuevo(url, altura, largura, false, nombre);
                else {
                    //PageMethods em Prosegur.Global.GesEfectivo.IAC.Web.Base.urlLoginExpired()
                    funcUrlLoginExpired();
                }
            }
        }
    }
    function OnFailed(error, userContext, methodName) {
        alert("Error");
    }
}
function funcUrlLoginExpired() {
    //PageMethods em Prosegur.Global.GesEfectivo.IAC.Web.Base.isSessionExpired()
    PageMethods.urlLoginExpired(OnSucceeded, OnFailed)
    function OnSucceeded(result, usercontext, methodName) {
        RedirecionaPaginaNormal(result);
        //alert('envia msg e redireciona....');
    }
    function OnFailed(error, userContext, methodName) {
        alert("Error");
    }
}



/// <summary>
/// Método responsável pela exibição dos pop-ups modal.
/// Nota: 
/// </summary>
function AbrirPopupModalScroll(url, altura, largura, scroll, nombre) {

    AbrirPopupModal(url, altura, largura, false, nombre);

    //var parametros;

    //if (scroll) {
    //    parametros = "dialogWidth:" + largura + "px; dialogHeight:" + altura + "px; center:yes; scroll: yes;";
    //}
    //else {
    //    parametros = "dialogWidth:" + largura + "px; dialogHeight:" + altura + "px; center:yes; scroll: no;";
    //}

    //var myArguments = new Object();
    //var r = window.showModalDialog(url + '&esModal=True', myArguments, parametros);

    //if (typeof r != "undefined") {
    //    window.document.forms[0].submit();
    //}
}

function AbrirPopupModalNuevo(url, altura, largura, redimensionavel, nombre) {
    var WindowRef = null;
    redimensionavel = (redimensionavel === true ? "yes" : "no");
    if (navigator.appName == 'Netscape') {
        var dvBloquearTela = document.getElementById('dvBloquearTela');
        if (dvBloquearTela)
            dvBloquearTela.style.display = "block";
        var parametros = "width=" + largura + ",height=" + altura + ",center=1,resizable=" + redimensionavel + ",maximize=" + redimensionavel + ",minimize=" + redimensionavel;

        WindowRef = window.open(url, nombre, parametros);

        if (!WindowRef.opener) {
            WindowRef.opener = self;
        }

        if (typeof WindowRef != "undefined") {
            WindowRef.addEventListener('load', function () {
                WindowRef.addEventListener('unload', function () {
                    var valores = null;
                    var dvBloquearTela = document.getElementById('dvBloquearTela');
                    if (dvBloquearTela)
                        dvBloquearTela.style.display = "none";
                    window.document.forms[0].submit();
                    //alert('1');
                    //if (WindowRef.returnValue != null) {
                    //    alert('2');
                    //    valores = WindowRef.returnValue.split("|");
                    //}
                    //if (valores != null && valores[1] == "SesionExpirada") {
                    //    alert('3');
                    //    window.location.href = valores[2];
                    //}
                    //else {
                    //    alert('4');
                    //    document.getElementById('dvBloquearTela').style.display = "none";
                    //    window.document.forms[0].submit();
                    //};
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

function AbrirModalBootstrap(url, callback) {
    $('#frameContent').attr('src', url + '&esModal=True');
    //$('#modalMorfologia .modal-external-content').load(url + '&esModal=True', function () {
    $('#modalMorfologia').modal({ backdrop: 'static', keyboard: false, show: true });
    //});
}

function BloquearColar() {
    var ctrl = window.event.ctrlKey;
    var tecla = window.event.keyCode;
    if (ctrl && tecla == 67) { event.keyCode = 0; event.returnValue = false; }
    if (ctrl && tecla == 86) { event.keyCode = 0; event.returnValue = false; }
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

function bloqueialetras(evt, z) {
    //Verifica se o controle está dentro do controle de diferencias
    var objDiferencia = $(z).parents("div[id*='UCValoresDivisasDiferencias']")[0];
    var objDiferenciaEfectivoClasificacion = $(z).parents("div[id*='dvDivisasEfectivoDetallar']")[0];
    var objDiferenciaMedioPagoClasificacion = $(z).parents("div[id*='dvDivisasMedioPagoDetallar']")[0];
    if ((objDiferencia != null && objDiferencia != undefined) || (objDiferenciaEfectivoClasificacion != null && objDiferenciaEfectivoClasificacion != undefined) || (objDiferenciaMedioPagoClasificacion != null && objDiferenciaMedioPagoClasificacion != undefined)) {
        return bloqueialetrasAceitaNegativo(evt, z);
    }

    var evento = window.Event ? true : false;
    var tecla = (window.Event) ? evt.which : evt.keyCode;
    var teclaPresionada;
    //condição inserida para falha apresentada no ie8.
    if (tecla == undefined) {
        var keyCode = evt.keyCode ? evt.keyCode : evt.which;
        teclaPresionada = String.fromCharCode(keyCode);

    }

    else {
        teclaPresionada = String.fromCharCode(tecla);
        /*alert(teclaPresionada);*/
    }

    //bloqueia letras e se estourar o numero do maxlength
    if (!(teclaPresionada >= "0" && teclaPresionada <= "9") || z.maxLength - 4 == z.value.length) {
        window.event.keyCode = 0;
        return false;
    }

}

function bloqueialetrasAceitaNegativo(evt, z) {

    var evento = window.Event ? true : false;
    var tecla = (window.Event) ? evt.which : evt.keyCode;
    var teclaPresionada;
    //condição inserida para falha apresentada no ie8.
    if (tecla == undefined) {
        var keyCode = evt.keyCode ? evt.keyCode : evt.which;
        teclaPresionada = String.fromCharCode(keyCode);

    }

    else {
        teclaPresionada = String.fromCharCode(tecla);
        /*alert(teclaPresionada);*/
    }

    //bloqueia letras e se estourar o numero do maxlength
    if (!(teclaPresionada >= "0" && teclaPresionada <= "9") || z.maxLength - 4 == z.value.length) {
        if (teclaPresionada != "-") {
            window.event.keyCode = 0;
            return false;
        }
    }

    //verifica se pressionado o menos
    if (teclaPresionada == "-") {
        //verifica se o menos já existe no controle..
        if (z.value.indexOf("-") > -1) {
            return false;
        }

        //inclui o menos na possição inicial..
        SetEnd(z, z.length);
        return true;
    }
}

function bloqueialetrasAceitaVirgulaPunto(evt, z) {

    var evento = window.Event ? true : false;
    var tecla = (window.Event) ? evt.which : evt.keyCode;
    var teclaPresionada;
    //condição inserida para falha apresentada no ie8.
    if (tecla == undefined) {
        var keyCode = evt.keyCode ? evt.keyCode : evt.which;
        teclaPresionada = String.fromCharCode(keyCode);

    }

    else {
        teclaPresionada = String.fromCharCode(tecla);
        /*alert(teclaPresionada);*/
    }
    var spl = z.value.split(",");
    if (spl.length > 1 &&  doGetCaretPosition(z)  > spl[0].length && spl[1].length > 3) {
        return false;
    }


    //bloqueia letras e se estourar o numero do maxlength
    if (!((teclaPresionada >= "0" && teclaPresionada <= "9") || (teclaPresionada == "," || teclaPresionada == "." )) || z.maxLength - 4 == z.value.length) {
        if (teclaPresionada != "," && teclaPresionada != ".") {
            return false;
        }
    }

    //verifica se pressionado o menos
    if (teclaPresionada == ",") {
        //verifica se o menos já existe no controle..
        if (z.value.indexOf(",") > -1) {
            return false;
        }

        //inclui o menos na possição inicial..
        return true;
    }
}

function doGetCaretPosition(oField) {

    // Initialize
    var iCaretPos = 0;

    // IE Support
    if (document.selection) {

        // Set focus on the element
        oField.focus();

        // To get cursor position, get empty selection range
        var oSel = document.selection.createRange();

        // Move selection start to 0 position
        oSel.moveStart('character', -oField.value.length);

        // The caret position is selection length
        iCaretPos = oSel.text.length;
    }

    // Firefox support
    else if (oField.selectionStart || oField.selectionStart == '0')
        iCaretPos = oField.selectionDirection == 'backward' ? oField.selectionStart : oField.selectionEnd;

    // Return results
    return iCaretPos;
}
function NumerosVirgulaPunto(id) {//Solo numeros
    var element = document.getElementById(id); 
    var string = element.value;
    var out = '';
    var filtro = '1234567890,.';//Caracteres validos

    //Recorrer el texto y verificar si el caracter se encuentra en la lista de validos 
    for (var i = 0; i < string.length; i++)
        if (filtro.indexOf(string.charAt(i)) != -1)
            //Se añaden a la salida los caracteres validos
            out += string.charAt(i);

    //Retornar valor filtrado
    element.value = out;
    return string = out;
    // true;
} 

function BloqueiaLetrasOnChange(id) {

    valor = document.getElementById(id).value;
    valor2 = ""
    var shift = window.event.shiftKey;
    var ctrl = window.event.ctrlKey;
    var tecla = window.event.keyCode;

    for (var i = 0; i < valor.length; i++) {
        if (valor.substring(i, i + 1) >= "0" || valor.substring(i, i + 1) <= "9") {
            valor2 += valor.substring(i, i + 1);
        }
    }
    if (valor != valor2) {
        document.getElementById(id).value = valor2;
    }
    return true;
}

function BloqueiaLetrasOnPaste(id) {

    valor = window.clipboardData.getData("Text");
    valor2 = ""
    var shift = window.event.shiftKey;
    var ctrl = window.event.ctrlKey;
    var tecla = window.event.keyCode;

    for (var i = 0; i < valor.length; i++) {
        if (valor.substring(i, i + 1) >= "0" && valor.substring(i, i + 1) <= "9") {
            valor2 += valor.substring(i, i + 1);
        }
    }
    if (valor != valor2) {
        window.clipboardData.clearData("Text");
        window.clipboardData.setData("Text", valor2);
        document.getElementById(id).value = valor2;
    }
    return true;
}


function VerificarNumeroDecimal(z, sd, sm) {

    v = z.value;

    var virgula = v.substr(v.length - 3);

    if ((virgula.substr(0, 1) != sd) && (virgula.substr(1, 1) != sd) && (virgula.substr(virgula.length - 1) != sd)) {

        if (v.length > 0) {

            v = v + sd + "00";

        } else {

            v = 0 + sd + "00";
        }

    } else {

        if (virgula.substr(1, 1) == sd) {

            v = v + "0";

        } else {

            if (virgula.substr(virgula.length - 1) == sd) {

                v = v + "00";
            }
        }
    }

    v = v.replace(/\D/g, "") //permite digitar apenas números
    v = v.replace(/[0-9]{18}/, "") //limita pra máximo 999.999.999.999,99

    if (v.length > 14) {

        v = v.substr(0, 14);
    }

    var zeroesquerda = v.substr(0, 1);
    if (zeroesquerda == 0 && v.length > 3) {

        v = v.substr(1, v.length);
    }

    v = v.replace(/(\d{1})(\d{11})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 8 digitos
    v = v.replace(/(\d{1})(\d{8})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 8 digitos
    v = v.replace(/(\d{1})(\d{5})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 5 digitos
    v = v.replace(/(\d{1})(\d{0,2})$/, "$1" + sd + "$2") //coloca virgula antes dos últimos 2 digitos

    if (v == ("0" + sd)) {

        v = "0" + sd + "00";
    }

    z.value = v;

}


function VerificarNumeroDecimal4(z, sd, sm) {
    var aux = z.value;
    v = z.value;
    if (v == "") {
        return;
    }
    aux = v.split(sd).join('');
    if ((v.length - aux.length) > 1  ){
        z.value = "";
        return;
    }
    v = v.split(sm).join('');
   
    var idx = v.length - 5;
    if (idx <0) {
        idx = 0;
    }
    var virgula = v.substr(idx);

    if (!virgula.includes(sd)) {

        if (v.length > 0) {

            v = v + sd + "0000";

        } else {

            v = 0 + sd + "0000";
        }

    } else {

       

            if (virgula.substr(virgula.length - 1).includes(sd)) {

                v = v + "0000";
            } else if (virgula.substr(virgula.length - 2).includes(sd)) {

                v = v + "000";
            } else if (virgula.substr(virgula.length - 3).includes(sd)) {

                v = v + "00";
            } else if (virgula.substr(virgula.length - 4).includes(sd)) {

                v = v + "0";
            }
     
    }

    v = v.replace(/(\d{1})(\d{13})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 8 digitos
    v = v.replace(/(\d{1})(\d{10})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 8 digitos
    v = v.replace(/(\d{1})(\d{7})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 5 digitos
    if (!v.includes(sd)) {
        v = v.replace(/(\d{1})(\d{0,4})$/, "$1" + sd + "$2") //coloca virgula antes dos últimos 2 digitos
    }

    if (v == ("0" + sd)) {

        v = "0" + sd + "00";
    }

    z.value = v;

}

// Define se pressionou as teclas de decimais (',' ou '.')
var pressinouDecimal = false;

/// <summary>
/// Formata para moeda de acordo com as casas decimais informadas
/// Nota:
/// evt: Evento
/// z: Objeto
/// sd: Separador Decimal
/// sm: Separador Milhar
/// </summary>
function moeda(evt, obj, sd, sm) {

    // Recebe o valor ASCII da tecla pressionada
    var tecla = window.Event ? evt.which : evt.keyCode;

    // Recebe o valor da tecla
    var valorTecla = String.fromCharCode(tecla);

    // Define as teclas de decimais (',')
    var teclaDecimal = "110, 188";

    // Verifica o separador decimal
    if (sd == ".")
        // Define as teclas de decimais ('.')
        teclaDecimal = "190, 194";

    // Verifica se as seguintes teclas foram pressionadas "left arrow", "up arrow", "right arrow", "down arrow", "backspace", "shift"
    // "page up", "page down", "insert", "home", "delete", "end", "tab"
    if (tecla != 37 && tecla != 38 && tecla != 39 && tecla != 40 && tecla != 8 && tecla != 16 &&
          tecla != 33 && tecla != 34 && tecla != 35 && tecla != 36 && tecla != 45 && tecla != 46 && tecla != 9) {

        // Valor do campo de texto existente
        var valor = obj.value;

        // Recupera a posição do cursor
        var posCursor = RetornaPosicaoCursor(obj);

        // Recupera a posição da virgula
        var posVirgula = valor.lastIndexOf(sd);

        // Se o tamanho do valor informado é o mesmo que o tamanho máximo do campo
        if (obj.maxLength == valor.length) {

            // Se a posição do cursor está na parte das casas decimais
            if (posCursor >= obj.maxLength - 2) {

                // Insere o número dentro do valor quando o tamanho já estorou o limite
                valor = valor.substr(0, posCursor) + RetornaTeclaPressionada(tecla) + valor.substr(posCursor, 2);
            }
        }

        // Se a virgula não existe formata o número adicionando a virgula com las casas decimais
        if (posVirgula < 0) {

            // Adiciona as casas decimais
            valor += "00";
        }
        else {

            // Divide o número pelo o número de separadores decimais existentes
            var numeroSeparado = valor.split(sd);

            // Limpa o valor informado
            valor = ""

            // Para cada parte do número existente
            for (var i = 0; i < numeroSeparado.length; i++) {

                // Se for a parte decima, último elemento da separação
                if (i == numeroSeparado.length - 1) {

                    // Se existe algum número na parte de casas decimais
                    if (numeroSeparado[i].length > 0) {

                        // Recupera a parte decimal
                        valor += (numeroSeparado[i] + "00").substr(0, 2);
                    }
                }
                else {

                    // Recupera número novamente, mas sem o caracter separador de decimais
                    valor += numeroSeparado[i];
                }
            }
        }

        // Remove os caracteres não númericos do valor
        valor = valor.replace(/\D/g, "")

        // Realiza a formatação do número
        valor = valor.replace(/(\d{1})(\d{11})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 8 digitos
        valor = valor.replace(/(\d{1})(\d{8})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 8 digitos
        valor = valor.replace(/(\d{1})(\d{5})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 5 digitos
        valor = valor.replace(/(\d{1})(\d{0,2})$/, "$1" + sd + "$2") //coloca virgula antes dos últimos 2 digitos

        // Devolve o valor formatado para o controle de texto.
        obj.value = valor;

        // Verifica se as teclas de decimais foram pressionadas
        if (teclaDecimal.indexOf(tecla) > -1) {

            // Se pressionou a tecla de decimal
            if (pressinouDecimal) {

                // Define que a tecla foi pressionada para "falso"
                pressinouDecimal = false;

                // Posiciona o cursor antes da virgula
                SetEnd(obj, 3);

                // Sai do método
                return true;
            }
            else {

                // Define que a tecla foi pressionada para "true"
                pressinouDecimal = true;

                // Posiciona o cursor depois da virgula
                SetEnd(obj, 2)

                // Sai do método
                return true;
            }
        }

        // Define que a tecla foi pressionada para "falso"
        pressinouDecimal = false;

        // Se o tamanho do valor informado é o mesmo que o tamanho máximo do campo
        if (obj.maxLength == valor.length) {

            // Se o curso está na posição de casas decimais
            if (obj.maxLength - posCursor <= 2) {

                //Se está na penúltima casa decimal
                if (obj.maxLength - posCursor == 2) {

                    // Define a posição do cursor na penúltima posição
                    posCursor = 1;
                }
                else {
                    // Se a última tecla digitada é o último caracter do campo de texto
                    if (obj.value.substr(obj.value.length - 1, 1) == RetornaTeclaPressionada(tecla)) {

                        // Define a posição do cursor na última posição
                        posCursor = 0;
                    }
                    else {

                        // Define a posição do cursor na penúltima posição
                        posCursor = 1;
                    }
                }
            }
            else {

                // Não altera a posição do cursor
                var tam = obj.value.length % 4 == 0 ? obj.value.length - 1 : obj.value.length;
                posCursor = tam - posCursor;
            }
        }
        else {

            // Verifica a posição do Cursor
            if (obj.value.length - posCursor == 1) {

                // Define a posição do cursor na penúltima posição
                posCursor = 1;

            } else if (obj.value.length - posCursor == 3) {

                // Define a posição do cursor antes da virgula
                posCursor = 3;
            }
            else {

                // Não altera a posição do cursor
                var tam = obj.value.length % 4 == 0 ? obj.value.length - 1 : obj.value.length;
                posCursor = tam - posCursor;
            }
        }

        // Posiciona o cursor antes da virgula
        SetEnd(obj, posCursor);
    }
}

//Funcao: MascaraMoeda
//Sinopse: Mascara de preenchimento de moeda
//Parametro:
//   objTextBox : Objeto (TextBox)
//   SeparadorMilesimo : Caracter separador de milésimos
//   SeparadorDecimal : Caracter separador de decimais
//   e : Evento
//Retorno: Booleano
//Baseado em: http://codigofonte.uol.com.br/codigos/mascara-de-moeda
//Foram necessarias alterações para funcionamento 
//-----------------------------------------------------
function MascaraMoeda(e, objTextBox, SeparadorDecimal, SeparadorMilesimo) {

    var sep = 0;
    var key = '';
    var i = j = 0;
    var len = len2 = 0;
    var strCheck = '0123456789';
    var aux = aux2 = '';
    var whichCode = (window.Event) ? e.which : e.keyCode;
    if (whichCode == 13) return true;
    //key = String.fromCharCode(whichCode); // Valor para o código da Chave
    key = e.key;
    if (strCheck.indexOf(key) == -1) return false; // Chave inválida
    len = objTextBox.value.length;
    for (i = 0; i < len; i++)
        if ((objTextBox.value.charAt(i) != '0') && (objTextBox.value.charAt(i) != SeparadorDecimal)) break;
    aux = '';
    for (; i < len; i++)
        if (strCheck.indexOf(objTextBox.value.charAt(i)) != -1)
            aux += objTextBox.value.charAt(i);
    // aux += key;
    len = aux.length;
    if (len == 0) objTextBox.value = '';
    if (len == 1) objTextBox.value = '0' + SeparadorDecimal + '0' + aux;
    if (len == 2) objTextBox.value = '0' + SeparadorDecimal + aux;
    if (len > 2) {
        aux2 = '';
        for (j = 0, i = len - 3; i >= 0; i--) {
            if (j == 3) {
                aux2 += SeparadorMilesimo;
                j = 0;
            }
            aux2 += aux.charAt(i);
            j++;
        }
        objTextBox.value = '';
        len2 = aux2.length;
        for (i = len2 - 1; i >= 0; i--)
            objTextBox.value += aux2.charAt(i);
        objTextBox.value += SeparadorDecimal + aux.substr(len - 2, len);
    }
    return false;

}


/// <summary>
/// Recupera a posição do cursor
/// Nota:
/// obj: Objeto
/// </summary>
function RetornaPosicaoCursor(obj) {

    // Variáveis usadas no método
    var sel, range, txtRange, posicao = -1;

    // Verifica se o inicio da seleção é um número
    if (typeof obj.selectionStart == "number") {

        // Recupera a posição do inicio da seleção
        i = obj.selectionStart;
        posicao = i;
    }
        // Verifica se existe elemento selecionado no documento e o objeto permite criar um TextRange
    else if (document.selection && obj.createTextRange) {

        // Recupera o elemento que está selecionado no documento
        sel = document.selection;

        // Se existe documento selecionado
        if (sel) {

            // Cria um novo Range
            range = sel.createRange();
            // Cria um novo TextRange com base no objeto passado como parâmetro
            txtRange = obj.createTextRange();
            // Define uma nova posição para o cursor
            txtRange.setEndPoint("EndToStart", range);
            // Recupera a nova posição do cursor
            posicao = txtRange.text.length;
        }
    } else {

        // Atribui null aos eventos do objeto passado como parâmetro
        obj.onkeyup = null;
        obj.onclick = null;
    }

    // Retorna a posição do cursor dentro do objeto
    return posicao;
}

/// <summary>
/// Retorna o valor da tecla que foi pressionada
/// Nota:
/// tecla: Tecla
/// </summary>
function RetornaTeclaPressionada(tecla) {

    // Verifica o código da tecla digitada e retorna o número
    switch (tecla) {
        case 48, 96:
            return 0;

        case 49, 97:
            return 1;

        case 50, 98:
            return 2;

        case 51, 99:
            return 3;

        case 52, 100:
            return 4;

        case 53, 101:
            return 5;

        case 54, 102:
            return 6;

        case 55, 103:
            return 7;

        case 56, 104:
            return 8;

        case 57, 105:
            return 9;

        default:
            return '';
    }
}

/// <summary>
/// seta o cursor dentro textbox no indice passado como parametro.
/// </summary>
function SetEnd(TB, indice) {
    if (document.selection) {
        if (TB.createTextRange) {
            var FieldRange = TB.createTextRange();
            alert(FieldRange);
            FieldRange.moveStart('character', TB.value.length - indice);
            FieldRange.collapse();
            FieldRange.select();
        }
    }
    else if (TB.selectionStart || TB.selectionStart == '0') {
        TB.setSelectionRange(TB.value.length - indice, TB.value.length - indice);
    }
}


/// <summary>
/// Limita a quantidade de caracteres no textbox.
/// </summary>
function limitaCaracteres(id, numCaracteres) {

    valor = document.getElementById(id).value;

    var shift = window.event.shiftKey;
    var ctrl = window.event.ctrlKey;
    var tecla = window.event.keyCode;

    if (valor.length >= numCaracteres) {


        if (tecla == 40 || tecla == 38 || tecla == 37 || tecla == 39) {
            return true;
        }

        document.getElementById(id).value = document.getElementById(id).value.substring(0, numCaracteres);

        if (shift == false) {
            return false;
        }

        if (ctrl == false) {
            return false;
        } else {
            window.event.keyCode = 0;
            return false;
        }


    } else {
        return true;
    }
}

/// <summary>
/// Limita a quantidade de caracteres no textbox.
/// </summary>
function limitaCaracteresKeyPress(id, numCaracteres) {

    valor = document.getElementById(id).value;

    var shift = window.event.shiftKey;
    var ctrl = window.event.ctrlKey;
    var tecla = window.event.keyCode;

    if (valor.length >= numCaracteres) {

        if (shift == true) {
            return false;
        }

        if (ctrl == true) {
            return false;
        } else {
            window.event.keyCode = 0;
            return false;
        }


    } else {
        return true;
    }
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

/// <summary>
/// Pega os registro selecionados no grid e armazena no campo hidden.
/// Nota: 
/// </summary>
function TerminoIacSelecionado(obj, oid, id) {

    var selecionados = document.getElementById(id).value;

    if (obj.checked == true) {
        if (selecionados != '') {
            if (document.getElementById(id).value == undefined) {
                selecionados = oid;
            }
            else {
                selecionados = selecionados + '|' + oid;
            }
        }
        else {
            selecionados = oid;
        }
    }
    else {
        //        var aux = selecionados.split("|");
        //        selecionados = '';
        //        for (i = 0; i < aux.length; i++) {
        //            if (aux[i] != oid) {
        //                if (selecionados != '') {
        //                    selecionados = selecionados + '|' + aux[i];
        //                } else {
        //                    selecionados = aux[i];
        //                }
        //            }
        //        }
        if (selecionados.indexOf("|") >= 0) {
            var splitSelecionados = selecionados.split("|");
            // caso o item selecionado está na 1ª posição
            if (splitSelecionados[0] == oid)
                selecionados = selecionados.replace(oid + '|', '');
            else
                selecionados = selecionados.replace('|' + oid, '');

        } else {
            selecionados = "";
        }
    }

    document.getElementById(id).value = selecionados;
    // limpa a variável
    selecionados = "";

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
/// Armazenda duas colunas do grid em campos hiddens verdadeiros e falso, separando os valores verdadeiros e falsos.
/// Nota: 
/// </summary>
function BloqueiaCheckBoxResultadosTrueOrFalse(obj, oidcampo, idVerdadeiro, idFalso, hidVerdadeiro, hidFalso, idobjDesabilita, idDesabilitaVerdadeiro, idDesabilitaFalso, hidDesabilitaVerdadeiro, hidDesabilitaFalso) {
    executarlinha = false
    var objDesabilita;

    if (idobjDesabilita != '') {

        objDesabilita = document.getElementById(idobjDesabilita);
        if (obj.checked == true) {
            objDesabilita.disabled = true;
            objDesabilita.checked = false;
            ResultadosTrueOrFalse(objDesabilita, oidcampo, idDesabilitaVerdadeiro, idDesabilitaFalso, hidDesabilitaVerdadeiro, hidDesabilitaFalso);
        } else {
            objDesabilita.disabled = false;
        }
    }

    ResultadosTrueOrFalse(obj, oidcampo, idVerdadeiro, idFalso, hidVerdadeiro, hidFalso);


    if (idobjDesabilita != '') {
        document.getElementById(idobjDesabilita).value = objDesabilita;
    }
}

function DesbloqueiaCheckBoxResultadosTrueOrFalse(obj, oidcampo, idVerdadeiro, idFalso, hidVerdadeiro, hidFalso, idobjDesabilita, idDesabilitaVerdadeiro, idDesabilitaFalso, hidDesabilitaVerdadeiro, hidDesabilitaFalso) {
    executarlinha = false
    var objDesabilita;

    if (idobjDesabilita != '') {

        objDesabilita = document.getElementById(idobjDesabilita);
        if (obj.checked == false) {
            objDesabilita.disabled = true;
            objDesabilita.checked = false;
            ResultadosTrueOrFalse(objDesabilita, oidcampo, idDesabilitaVerdadeiro, idDesabilitaFalso, hidDesabilitaVerdadeiro, hidDesabilitaFalso);
        } else {
            objDesabilita.disabled = false;
        }
    }

    ResultadosTrueOrFalse(obj, oidcampo, idVerdadeiro, idFalso, hidVerdadeiro, hidFalso);


    if (idobjDesabilita != '') {
        document.getElementById(idobjDesabilita).value = objDesabilita;
    }
}
/// <summary>
/// Marcha o checkbox clicando na linha do grid.
/// Nota: 
/// </summary>
var executarlinha = true;
function SelecionaCheckBox(objid, oid, id) {
    var obj = document.getElementById(objid);

    if (executarlinha && obj.disabled == false) {

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

function DesabilitaCheckBoxGrid(id) {

    if (document.getElementById(id) != null) {
        document.getElementById(id).disabled = true;
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
/// Evento aciona os botões informados ao pressionar enter e esc.
/// Nota: 
/// </summary>
function EventoEnter(idEnter, e) {

    evt = e || window.event;
    var keyPressed = evt.which || evt.keyCode;

    if (keyPressed == 13) {
        //    document.getElementById(idEnter).focus();
        document.getElementById(idEnter).click();
        return false;
    } else { return true; }
}

/// <summary>
/// Redireciona pagina
/// Nota: 
/// </summary>
function RedirecionaPaginaNormal(url) {
    if (url != '') {
        document.location.href = url;
    }
}

function SelecionaTodosChecks(spanChk, hdnId, items) {
    var oItem = spanChk.children;
    var selecionados = document.getElementById(hdnId).value;

    var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
    xState = theBox.checked;
    elm = theBox.form.elements;

    for (i = 0; i < elm.length; i++)

        if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {

            if (elm[i].checked != xState)
                elm[i].checked = xState;

        }

    if (xState == true) {
        selecionados = items;
    } else { selecionados = ''; }

    document.getElementById(hdnId).value = selecionados;
}

/// <summary>
/// Marcha o checkbox clicando na linha do grid.
/// Nota: 
/// </summary>
function SelecionaCheckBoxDesmarcaTodos(objid, oid, id, oidChk) {
    var obj = document.getElementById(objid);
    var obj1 = document.getElementById(oidChk);

    if (executarlinha && obj.disabled == false) {

        if (obj.checked) {
            obj.checked = false;
            obj1.checked = false;
            TerminoIacSelecionado(obj, oid, id);
        }
        else {
            obj.checked = true;
            TerminoIacSelecionado(obj, oid, id);
        }
    }
    else {
        if (obj.checked == false) {
            obj.checked = false;
            obj1.checked = false;
        }
        executarlinha = true;
        TerminoIacSelecionado(obj, oid, id);
    }

}

/// <summary>
/// Verifica se algum checkbox no grid foi selecionado
/// Nota: 
/// </summary>
function VerificarItensMarcados(idchk, msgConfirm, msgSelecionar) {
    var spanChk = document.getElementById(idchk);
    var oItem = spanChk.children;

    var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
    xState = theBox.checked;
    elm = theBox.form.elements;

    for (i = 0; i < elm.length; i++)

        if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {

            if (elm[i].checked == true) {
                if (confirm(msgConfirm))
                    return true;
                else
                    return false;
            }


        }

    alert(msgSelecionar);
    return false
}


if (navigator.userAgent.indexOf("MSIE") != -1) {
    // IE
} else {
    // Firefox
    HTMLElement.prototype.click = function () {
        var evt = this.ownerDocument.createEvent('MouseEvents');
        evt.initMouseEvent('click', true, true, this.ownerDocument.defaultView, 1, 0, 0, 0, 0, false, false, false, false, 0, null);
        this.dispatchEvent(evt);
    }
}

function DesmarcaTodosCheckBoxExceto(obj, idGrid, numColuna, oidcampo, idVerdadeiro, idFalso, hidVerdadeiro, hidFalso) {

    if (obj != '' && obj != undefined) {

        if (obj.checked == true) {

            var grid = document.getElementById(idGrid)
            if (grid != '' && grid != undefined && (grid.rows != '' && grid.rows != undefined)) {

                for (var i in grid.rows) {
                    var linha = grid.rows.item(parseInt(i));
                    if (linha != '' && linha != undefined) {
                        var celulaCheck = linha.cells.item(parseInt(numColuna));
                        var celulaOidCampo = linha.cells.item(1);
                        var check = celulaCheck.children.item(0).children.item(0);
                        if (check != '' && check != undefined && obj != check && check.tagName == "INPUT" && check.disabled == false) {
                            check.checked = false;
                            ResultadosTrueOrFalse(check, celulaOidCampo.textContent, idVerdadeiro, idFalso, hidVerdadeiro, hidFalso);
                        }
                    }
                }

            }
        }

        ResultadosTrueOrFalse(obj, oidcampo, idVerdadeiro, idFalso, hidVerdadeiro, hidFalso);
    }

}