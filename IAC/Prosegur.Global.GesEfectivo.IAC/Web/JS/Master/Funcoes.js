

var intSW = screen.width;   // Screen Width
var intSH = screen.height;  // Screen Height
var myWin = '0';
var flagImpresso = false;

/// <summary>
/// Método responsável por redirecionar para a página
/// de login quando uma sessão é expirada
/// </summary>
function RedirecionarLogin(paginaLogin) {

    var modalWindow = window.dialogArguments;

    if (modalWindow != null || (window.opener != null)) {
        if (modalWindow != null) {
            modalWindow.location = paginaLogin;
        }
        else {
            window.opener.location = paginaLogin;
        }
        self.close();
    }
    else {
        window.location = paginaLogin;
    }

}

function Fechando() {

    try {
        if (!flagImpresso) {
            window.alert('Aguarde carregar a pagina...');
            return false;
        }
        return true;
    }
    catch (e) {
    }
}

function ImprimirPagina() {

    try {
        var wait = setTimeout("window.print();", 2000);
        flagImpresso = true;
    }
    catch (e) {

    }
}

function AbrirJanela(theURL, wname, setarFoco) {

    try {
        var intWW = intSW * 0.90; // 90% do tamanho da tela
        var intWH = intSH * 0.80; // 80% do tamanho da tela
        var intWT = (intSH - intWH) / 2;
        var intWL = (intSW - intWW) / 2;

        myWin = window.open(theURL, wname, 'left=' + intWL + ',top=' + intWT + ',width=' + intWW + ',height=' + intWH + ' scrollbars=1');

        if (setarFoco)
            var foco = setTimeout("FocoJanela();", 1500);
    }
    catch (e) {
    }

}

function AoPressionarEnter(ObjetoAtual, ProximoFoco, CaracterEspecial) {

    var numeroPrecinto;

    if (document.all)
        tecla = event.keyCode;
    else {
        tecla = e.which;
    }

    if ((tecla == 13) || (tecla == 9)) {

        numeroPrecinto = document.getElementById(ObjetoAtual).value;

        if (numeroPrecinto != '') {

            if (numeroPrecinto.charAt(numeroPrecinto.length - 1) == CaracterEspecial) {
                numeroPrecinto = numeroPrecinto.slice(0, numeroPrecinto.length - 1);
                document.getElementById(ObjetoAtual).value = numeroPrecinto;
            }

        }

        document.getElementById(ProximoFoco).focus();

        return false;
    }
    return true;
}

function FocoJanela() {
    try {
        if (this.myWin != '0')
            this.myWin.focus();
    }
    catch (e) {
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
        alert(Err.Description);
    }
}

function ValidaMail(email) {
    if (email.value == "") {
        alert("Informe seu e-mail.");
        email.focus();
        email.select();
        return false;
    } else {
        prim = email.value.indexOf("@")
        if (prim < 2) {
            alert("O e-mail informado parece não estar correto.");
            email.focus();
            email.select();
            return false;
        }
        if (email.value.indexOf("@", prim + 1) != -1) {
            alert("O e-mail informado parece não estar correto.");
            email.focus();
            email.select();
            return false;
        }
        if (email.value.indexOf(".") < 1) {
            alert("O e-mail informado parece não estar correto.");
            email.focus();
            email.select();
            return false;
        }
        if (email.value.indexOf(" ") != -1) {
            alert("O e-mail informado parece não estar correto.");
            email.focus();
            email.select();
            return false;
        }
        if (email.value.indexOf("zipmeil.com") > 0) {
            alert("O e-mail informado parece não estar correto.");
            email.focus();
            email.select();
            return false;
        }
        if (email.value.indexOf("hotmeil.com") > 0) {
            alert("O e-mail informado parece não estar correto.");
            email.focus();
            email.select();
            return false;
        }
        if (email.value.indexOf(".@") > 0) {
            alert("O e-mail informado parece não estar correto.");
            email.focus();
            email.select();
            return false;
        }
        if (email.value.indexOf("@.") > 0) {
            alert("O e-mail informado parece não estar correto.");
            email.focus();
            email.select();
            return false;
        }
        if (email.value.indexOf(".com.br.") > 0) {
            alert("O e-mail informado parece não estar correto.");
            email.focus();
            email.select();
            return false;
        }
        if (email.value.indexOf("/") > 0) {
            alert("O e-mail informado parece não estar correto.");
            email.focus();
            email.select();
            return false;
        }
        if (email.value.indexOf("[") > 0) {
            alert("O e-mail informado parece não estar correto.");
            email.focus();
            email.select();
            return false;
        }
        if (email.value.indexOf("]") > 0) {
            alert("O e-mail informado parece não estar correto.");
            email.focus();
            email.select();
            return false;
        }
        if (email.value.indexOf("(") > 0) {
            alert("O e-mail informado parece não estar correto.");
            email.focus();
            email.select();
            return false;
        }
        if (email.value.indexOf(")") > 0) {
            alert("O e-mail informado parece não estar correto.");
            email.focus();
            email.select();
            return false;
        }
        if (email.value.indexOf("..") > 0) {
            alert("O e-mail informado parece não estar correto.");
            email.focus();
            email.select();
            return false;
        }
    }
    return true;
}

/// <summary>
/// Function para verificar qual radio buton foi selecionado e habilitar ou desabilitar o textbox
/// Usado na Pagina ABMCliente.aspx
/// </summary>
function SituacaoTextBox(estado, txtIdPSID, txtDescripcionID, btnBuscarID) {
    if (estado == 0) {
        document.getElementById(txtIdPSID).disabled = true;
        document.getElementById(txtDescripcionID).disabled = true;
        document.getElementById(btnBuscarID).disabled = true;
        document.getElementById(txtIdPSID).value = '';
        document.getElementById(txtDescripcionID).value = '';
    }
    else {
        document.getElementById(txtIdPSID).disabled = false;
        document.getElementById(txtDescripcionID).disabled = false;
        document.getElementById(btnBuscarID).disabled = false;
    }
}

/// <summary>
/// Método responsável pela exibição dos pop-ups modal.
/// Nota: 
/// </summary>
//function AbrirPopupModal(url, altura, largura) {

//    var parametros = "dialogWidth:" + largura + "px; dialogHeight:" + altura + "px; center:yes"
//    var myArguments = self;
//    var r = window.showModalDialog(url, myArguments, parametros);

//    if (typeof r != "undefined")
//        window.document.forms(0).submit();

//}
//function AbrirPopupModal(url, altura, largura, redimensionavel) {

//    redimensionavel = (redimensionavel === true ? "yes" : "no");
//    var parametros = "dialogWidth:" + largura + "px; dialogHeight:" + altura + "px; center:yes; resizable:" + redimensionavel + ";maximize:" + redimensionavel + ";minimize:" + redimensionavel;
//    var myArguments = self;
//    var r = window.showModalDialog(url, myArguments, parametros);

//    if (typeof r != "undefined")
//        doPostBackAsync("__Page", r);
//    //window.document.forms(0).submit();

//}

function AbrirPopupModal(url, altura, largura, redimensionavel, nombre) {
    var WindowRef = null;
    redimensionavel = (redimensionavel === true ? "yes" : "no");
    if (navigator.appName == 'Netscape') {
        document.getElementById('dvBloquearTela').style.display = "block";
        var parametros = "width=" + largura + ",height=" + altura + ",center=1,resizable=" + redimensionavel + ",maximize=" + redimensionavel + ",minimize=" + redimensionavel;

        WindowRef = window.open(url, nombre, parametros);

        if (!WindowRef.opener) {
            WindowRef.opener = self;
        }

        if (typeof WindowRef != "undefined") {
            $(WindowRef).load(function () {
                $(WindowRef).unload(function () {
                    var valores = null;
                    if (WindowRef.returnValue != null) {
                        valores = WindowRef.returnValue.split("|");
                    }
                    if (valores != null && valores[1] == "SesionExpirada") {
                        window.location.href = valores[2];
                    }
                    else {
                        doPostBackAsync("__Page", WindowRef.returnValue);
                        document.getElementById('dvBloquearTela').style.display = "none";
                    };
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


function AbrirPopup(url, altura, largura) {

    var parametros = "width=" + largura + ", height=" + altura;

    window.open(url, "_blank", parametros);

}

/// <summary>
/// Método responsável pela exibição dos pop-ups modal.
/// Nota: 
/// </summary>
function AbrirPopupModalUcBuscaClientesBancos(url, obj_idps, obj_desc, altura, largura) {

    var val_idps = '';
    var val_desc = '';

    if (obj_idps != '' && obj_desc != '') {
        val_idps = document.getElementById(obj_idps).value;
        val_desc = document.getElementById(obj_desc).value;
    }

    //atualiza parametros url
    url += "&IdPS=" + val_idps + "&Descripcion=" + val_desc;

    var parametros = "dialogWidth:" + largura + "px; dialogHeight:" + altura + "px; center:yes"
    var myArguments = self;
    var r = window.showModalDialog(url, myArguments, parametros);

    if (typeof r != "undefined")
        window.document.forms(0).submit();

}

/// <summary>
/// Método responsável pode devolver valores para página pai.
/// Nota: 
/// </summary>
function DevolverValores(IdOrigem, DescricaoOrigem, HiddenOrigem, ValorIdOrigem, ValorDescricaoOrigem, ValorHiddenOrigem) {

    try {
        window.opener.document.getElementById(IdOrigem).value = ValorIdOrigem;
        window.opener.document.getElementById(DescricaoOrigem).value = ValorDescricaoOrigem;
        window.opener.document.getElementById(HiddenDestino).value = ValorHiddenDestino;
        window.returnValue = 0; window.close();
    }
    catch (e) {
        alert(e.description);
    }

}

/// <summary>
/// Permite apenas a inserção de números
/// Nota: 
/// </summary>
function ValorNumerico(evt) {

    var evento = window.Event ? true : false;
    tecla = evt.keyCode;

    return (tecla < 13 || (tecla >= 48 && tecla <= 57));
}

/// <summary>
/// Bloqueia Control c e Control V
/// Nota: 
/// </summary>
function ValidarControlCV() {

    var ctrl = window.event.ctrlKey;
    var tecla = window.event.keyCode;
    if (ctrl && tecla == 67) { return false; }
    if (ctrl && tecla == 86) { return false; }

}

function ValidaClickDireito(msg) {
    if (event.button == 2) { alert(msg); }
}

/// <summary>
/// Permite apenas a inserção de números e pontos
/// Nota: 
/// </summary>
function ValorNumericoEPonto(evt) {

    var evento = window.Event ? true : false;
    tecla = evt.keyCode;
    return (tecla < 13 || (tecla >= 48 && tecla <= 57) || tecla == 46);

}

/// <summary>
/// Altera a descricao do centro proceso de acordo com as selecoes dos dropdowns
/// Nota: Usada na tela de ABMCentroProceso
/// </summary>
function GenerarDescripcion(Matriz, Planta, TipoCentro, Descripcion) {

    if (document.getElementById(Matriz).disabled == false && document.getElementById(Matriz).length > 0) {
        document.getElementById(Descripcion).value = document.getElementById(Matriz)(document.getElementById(Matriz).selectedIndex).text + '/' + document.getElementById(TipoCentro)(document.getElementById(TipoCentro).selectedIndex).text;
    }
    else {
        document.getElementById(Descripcion).value = document.getElementById(Planta)(document.getElementById(Planta).selectedIndex).text + '/' + document.getElementById(TipoCentro)(document.getElementById(TipoCentro).selectedIndex).text;
    }
}

/// <summary>
/// Mostra a descrição do item selecionado no tootip do controler
/// Nota: Usado na tela ElijirCentroProceso e AMBCentroProceso
/// </summary>
function CambiarToolTip(IdCentroProceso) {

    if (document.getElementById(IdCentroProceso) != null) {
        if (document.getElementById(IdCentroProceso).disabled == false && document.getElementById(IdCentroProceso).length > 0) {
            document.getElementById(IdCentroProceso).title = document.getElementById(IdCentroProceso)(document.getElementById(IdCentroProceso).selectedIndex).text;
        }
        else {
            document.getElementById(IdCentroProceso).title = document.getElementById(IdCentroProceso)(document.getElementById(IdCentroProceso).selectedIndex).text;
        }
    }
}

/// <summary>
/// Altera o estado dos radiobuton
/// Nota: Usada na tela de ABMReporte
/// </summary>
function Habilita(CheckBox, Radio1, Radio2) {
    if (document.getElementById(CheckBox).checked == true) {
        document.getElementById(Radio1).disabled = false;
        document.getElementById(Radio2).disabled = false;
    }
    else {
        document.getElementById(Radio1).disabled = true;
        document.getElementById(Radio2).disabled = true;
    }
}

/// <summary>
/// Altera a cor do textbox Color na ABMFormulario
/// </summary>
function MudaCor(controle) {
    try {
        //controle.style.backgroundColor = '#' + controle.value;
        controle.style.setProperty("background", "#" + controle.value, "important");
    }
    catch (e) {
        controle.value = 'FFFFFF';
        controle.style.backgroundColor = '#FFFFFF';
    }

}

/// <summary>
/// Funcao que seleciona todos os itens de um listitem.
/// Acionado no clique do botao
/// </summary>
function SelecionarTudo(controle) {
    for (var i = 0; i <= document.getElementById(controle).length - 1; i++) {
        document.getElementById(controle)(i).selected = true;
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
    if (isNum && (keyCode < 48 || keyCode > 57) && (keyCode != 88) && (keyCode != 120))
        return false;

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
/// Mascara para moeda
/// </summary>
function moeda(evt, obj, sd, sm) {
   
    v = obj.value;

    // Recupera a posição da virgula
    var posVirgula = v.lastIndexOf(sd);

    // Se a virgula não existe formata o número adicionando a virgula com las casas decimais
    if (posVirgula < 0) {

        // Adiciona as casas decimais
        v += "00";
    }
    else {

        // Divide o número pelo o número de separadores decimais existentes
        var numeroSeparado = v.split(sd);

        // Limpa o valor informado
        v = ""

        // Para cada parte do número existente
        for (var i = 0; i < numeroSeparado.length; i++) {

            // Se for a parte decima, último elemento da separação
            if (i == numeroSeparado.length - 1) {

                // Se existe algum número na parte de casas decimais
                if (numeroSeparado[i].length > 0) {

                    // Recupera a parte decimal
                    v += (numeroSeparado[i] + "00").substr(0, 2);
                }
            }
            else {

                // Recupera número novamente, mas sem o caracter separador de decimais
                v += numeroSeparado[i];
            }
        }
    }

    v = v.replace(/\D/g, "") //permite digitar apenas números
    v = v.replace(/[0-9]{15}/, "") //limita pra máximo 999.999.999.999,99
    v = v.replace(/(\d{1})(\d{11})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 10 digitos
    v = v.replace(/(\d{1})(\d{8})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 7 digitos
    v = v.replace(/(\d{1})(\d{5})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 4 digitos
    v = v.replace(/(\d{1})(\d{0,2})$/, "$1" + sd + "$2") //coloca virgula antes dos últimos 2 digitos

    obj.value = v;

}

function BloquearColar() {
    var ctrl = window.event.ctrlKey;
    var tecla = window.event.keyCode;
    if (ctrl && tecla == 67) { event.keyCode = 0; event.returnValue = false; }
    if (ctrl && tecla == 86) { event.keyCode = 0; event.returnValue = false; }
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
function moedaIAC(evt, obj, sd, sm) {
    // Recebe o valor ASCII da tecla pressionada
    //var tecla = window.Event ? evt.which : evt.keyCode;
    var tecla = evt.keyCode;

    // Recebe o valor da tecla
    var valorTecla = String.fromCharCode(tecla);

    //Verifica se o controle está dentro do controle de diferencias
    var objDiferencia = $(obj).parents("div[id*='UCValoresDivisasDiferencias']")[0];
    var objDiferenciaEfectivoClasificacion = $(obj).parents("div[id*='dvDivisasEfectivoDetallar']")[0];
    var objDiferenciaMedioPagoClasificacion = $(obj).parents("div[id*='dvDivisasMedioPagoDetallar']")[0];
    
    var valorNegativo = false;

    if ((objDiferencia != null && objDiferencia != undefined) || (objDiferenciaEfectivoClasificacion != null && objDiferenciaEfectivoClasificacion != undefined) || (objDiferenciaMedioPagoClasificacion != null && objDiferenciaMedioPagoClasificacion != undefined)) {
        //verifica se o menos já existe no controle..
        var posicaoMenos = obj.value.indexOf("-");

        if (posicaoMenos > -1) {
            valorNegativo = true;
        }

        //verifica se pressionado o menos
        if (tecla === 109 || tecla === 189) {

            if (valorNegativo) {
                obj.value = obj.value.replace(/-/g, "");
                obj.value = "-" + obj.value;
            }

            return true;
        }
    }


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
        valor = valor.replace(/(\d{1})(\d{11})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 11 digitos
        valor = valor.replace(/(\d{1})(\d{8})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 8 digitos
        valor = valor.replace(/(\d{1})(\d{5})$/, "$1" + sm + "$2") //coloca ponto antes dos últimos 5 digitos
        valor = valor.replace(/(\d{1})(\d{0,2})$/, "$1" + sd + "$2") //coloca virgula antes dos últimos 2 digitos

        //se existe o sinal de negativo 
        if (valorNegativo) {
            valor = valor.replace(/-/g, "");
            valor = "-" + valor;
        }

        //obj.value = valor;
        // Devolve o valor formatado para o controle de texto.
        $(obj).val(valor);
        $(obj).blur(function () {
            $(obj).change();
        });

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

            }
            else {
                posCursor = 3;
            }
        }

        // Posiciona o cursor antes da virgula
        SetEnd(obj, posCursor);
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

function decimais(evt) {
    var tecla = (window.event) ? evt.keyCode : evt.which;
    if (tecla == 8 || tecla == 0)
        return true;
    if (tecla != 44 && tecla < 48 || tecla > 57)
        return false;
}


function bloqueialetrasImporte(evt, z) {

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
    if (!(teclaPresionada >= "0" && teclaPresionada <= "9" || teclaPresionada == ",") || z.maxLength - 4 == z.value.length) {
        window.event.keyCode = 0;
        return false;
    }

}

function bloquearletras(evt, z) {

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
    if (!(teclaPresionada >= "0" && teclaPresionada <= "9")) {
        window.event.keyCode = 0;
        return false;
    }

}

// Adicionado 22/08/2013
function bloqueialetras(evt, z) {
    //Verifica se o controle está dentro do controle de diferencias
    var objDiferencia = $(z).parents("div[id*='UCValoresDivisasDiferencias']")[0];
    if (objDiferencia != null && objDiferencia != undefined) {
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

function bloqueialetrasImporteNegativo(evt, z) {

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
    if (!(teclaPresionada >= "0" && teclaPresionada <= "9" || teclaPresionada == ",") || z.maxLength - 4 == z.value.length) {
        if (teclaPresionada != "-") {
            window.event.keyCode = 0;
            return false;
        }
    }

}
// Adicionado 06/12/2013
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

// Adicionado 22/08/2013
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

/// <summary>
/// Marcha o checkbox clicando na linha do grid.
/// Nota: 
/// </summary>
var executarlinha = true;
function SelecionaRegistroMarcado(objid, oid, id, tipo) {
    var obj = document.getElementById(objid);

    if (executarlinha) {

        if (obj.checked) {
            obj.checked = false;
            AdicionaIdSelecionado(obj, oid, id, tipo);
        }
        else {
            obj.checked = true;
            AdicionaIdSelecionado(obj, oid, id, tipo);
        }
    }
    else {
        executarlinha = true;
        AdicionaIdSelecionado(obj, oid, id, tipo);
    }

}


/// <summary>
/// Pega os registro selecionados no grid e armazena no campo hidden.
/// Nota: 
/// </summary>
function AdicionaIdSelecionado(obj, oid, id, tipo) {

    var selecionados = document.getElementById(id).value;
    if (tipo == 'CheckBox') {
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
        document.getElementById(id).value = selecionados;
    }
    else {
        if (obj.checked == true) {
            document.getElementById(id).value = oid;
        } else {
            document.getElementById(id).value = ''
        }
    }


}
function EventoEnter(idEnter) {

    if (event.which || event.keyCode) {
        if ((event.which == 13) || (event.keyCode == 13)) {
            document.getElementById(idEnter).focus();
            document.getElementById(idEnter).click();
            return false;
        } else { return true; }

    } else { return true; }
}


/// <summary>
/// Recebe o id de um campo e seta o focus no proximo campo text
/// Usado no ucBuscaClientesBancos
/// </summary>
var IdGlobal = "";
function ProximoCampo(b) {
    var a = document.getElementsByTagName("input"); var c = 0;
    for (var i = 0; i < a.length; i++) {
        if (a[i].type == 'text') {
            if (c == 1 && a[i].style.display != 'none') {
                IdGlobal = a[i].id; c = 2;
                setTimeout(function () { document.getElementById(IdGlobal).focus(); }, 1000);
            }
            if (a[i].id.indexOf(b) >= 0 && a[i].id.indexOf("txtNomeCliente") >= 0) { c = 1; }
        }
    }
}

/// <summary>
/// Marca ou desmarca todos os itens de um listBox de acordo com a seleção do checkBox
/// </summary>
function MarcarDesmarcarTodosItensListBox(idCheck, idListBox) {

    var check = document.getElementById(idCheck);
    var listBox = document.getElementById(idListBox);

    if (listBox != null) {
        for (var i = 0; i <= listBox.length - 1; i++) {
            listBox(i).selected = check.checked;
        }
        listBox.disabled = check.checked;
    }
}

/// <summary>
/// Seta o radiobutton selecionado no grid
/// </summary>
function SetaUnicoRadioButton(Chk, gridViewCtlId) {

    var grid = document.getElementById(gridViewCtlId);
    var gridLength = grid.rows.length;
    var sum = 0;
    if (grid.rows.length > 0) {

        for (i = 1; i < grid.rows.length; i++) {

            cell = grid.rows[i].cells[0];

            for (j = 0; j < cell.childNodes.length; j++) {

                if (cell.childNodes[j].type == "radio") {
                    cell.childNodes[j].checked = false;
                }
            }
        }
    }

    Chk.checked = true;
}


function mascaraData(evento) {
    var origem;
    var txtOrigem;
    var key;
    if (window.event) {
        key = evento.keyCode;
        if (key >= 48 && key <= 57) {
            origem = evento.srcElement;
            txtOrigem = origem.value;
            if (txtOrigem.length == 2 || txtOrigem.length == 5) {
                txtOrigem += "/";
                origem.value = txtOrigem;
            }
        } else {
            event.returnValue = false;
        }
    }
    else {
        key = evento.which;
        if (key >= 48 && key <= 57) {
            origem = evento.target;
            txtOrigem = origem.value;
            if (txtOrigem.length == 2 || txtOrigem.length == 5) {
                txtOrigem += "/";
                origem.value = txtOrigem;
            }
        } else {
            if (key != 8 && key != 0) {
                evento.preventDefault();
            }
        }
    }
}

//formata de forma generica os campos
function formataCampo(campo, Mascara, evt) {
    var boleanoMascara;

    var tecla = (window.Event) ? evt.which : evt.keyCode;
    var Digitato;

    //condição inserida para falha apresentada no ie8.
    if (tecla == undefined) {
        var keyCode = evt.keyCode ? evt.keyCode : evt.which;
        Digitato = String.fromCharCode(keyCode);
        //alert(Digitato);
    }

    else {
        Digitato = String.fromCharCode(tecla);
        //alert(Digitato);
    }

    exp = /\-|\.|\/|\(|\)| /g
    campoSoNumeros = campo.value.toString().replace(exp, "");

    var posicaoCampo = 0;
    var NovoValorCampo = "";
    var TamanhoMascara = campoSoNumeros.length;;

    //bloqueia letras e se estourar o numero do maxlength
    if (!(Digitato >= "0" && Digitato <= "9")) {
        window.event.keyCode = 0;
        return false;
    }

    if (Digitato != 8) { // backspace 
        for (i = 0; i <= TamanhoMascara; i++) {
            boleanoMascara = (Mascara.charAt(i) == "/")

            boleanoMascara = boleanoMascara || ((Mascara.charAt(i) == "(") || (Mascara.charAt(i) == ")") || (Mascara.charAt(i) == " "))

            alert(boleanoMascara);

            if (boleanoMascara) {
                NovoValorCampo += Mascara.charAt(i);
                TamanhoMascara++;
            } else {
                NovoValorCampo += campoSoNumeros.charAt(posicaoCampo);
                posicaoCampo++;
            }
        }
        campo.value = NovoValorCampo;
        return true;
    } else {
        return true;
    }
}

function validacionFechaHora(obj, mensagemFecha, mensagemHora, titulo, fechar) {

    var data = obj.value;
    var Hora;

    if (data == "") { return; }
    var valor = data.split("/");
    if (valor.length = 3) {
        mensagemFecha = mensagemFecha + "<br/><br/>" + obj.value;
        mensagemHora = mensagemHora + "<br/><br/>" + obj.value;
        if (valor[0].length != 2) {
            obj.value = "";
            ExibirMensagem(mensagemFecha, titulo, null, fechar);
            return;
        }
        else if (valor[1].length != 2) {
            obj.value = "";
            ExibirMensagem(mensagemFecha, titulo, null, fechar);
            return;
        }
        else if (valor[2].length > 4) {
            var ano_aux = valor[2].split(" ");
            valor[2] = ano_aux[0];
            Hora = ano_aux[1];
            if (Hora.length != 8) {
                obj.value = "";
                ExibirMensagem(mensagemHora, titulo, null, fechar);
                return;
            }

        }
        else if (valor[2].length < 4) {
            obj.value = "";
            ExibirMensagem(mensagemFecha, titulo, null, fechar);
            return;
        }
    }
    var dia = valor[0];
    var mes = valor[1];
    var ano = valor[2];

    var situacao = "";

    // verifica o dia valido para cada mes
    if ((dia < 01) || (dia < 01 || dia > 30) && (mes == 04 || mes == 06 || mes == 09 || mes == 11) || dia > 31) {
        situacao = "falsa";
    }
    // verifica se o mes e valido
    if (mes < 01 || mes > 12) {
        situacao = "falsa";
    }
    // verifica se e ano bissexto
    if (mes == 2 && (dia < 01 || dia > 29 || (dia > 28 && (parseInt(ano / 4) != ano / 4)))) {
        situacao = "falsa";
    }

    if (situacao == "falsa") {
        obj.value = "";
        ExibirMensagem(mensagemFecha, titulo, null, fechar);
        return;
    }

    if (Hora != undefined) {
        var Horas = Hora.split(":")
        var hh = Horas[0];
        var mm = Horas[1];
        var ss = Horas[2];

        if (hh > 23) {
            obj.value = "";
            ExibirMensagem(mensagemHora, titulo, null, fechar);
            return;
        }
        if (mm > 59) {
            obj.value = "";
            ExibirMensagem(mensagemHora, titulo, null, fechar);
            return;
        }
        if (ss > 59) {
            obj.value = "";
            ExibirMensagem(mensagemHora, titulo, null, fechar);
            return;
        }
    }

}

function DataHora(objeto, evt) {
    var tecla = (window.Event) ? evt.which : evt.keyCode;
    var Digitato;

    //condição inserida para falha apresentada no ie8.
    if (tecla == undefined) {
        var keyCode = evt.keyCode ? evt.keyCode : evt.which;
        Digitato = String.fromCharCode(keyCode);
        //alert(Digitato);
    }

    else {
        Digitato = String.fromCharCode(tecla);
        //alert(Digitato);
    }
    campo = eval(objeto);
    if ((campo.value == '00/00/0000 00:00:00') || (campo.value == '00/00/0000')) {
        campo.value = ""
    }

    caracteres = '0123456789';
    separacao1 = '/';
    separacao2 = ' ';
    separacao3 = ':';
    conjunto1 = 2;
    conjunto2 = 5;
    conjunto3 = 10;
    conjunto4 = 13;
    conjunto5 = 16;

    //bloqueia letras e se estourar o numero do maxlength
    if (!(Digitato >= "0" && Digitato <= "9")) {
        window.event.keyCode = 0;
        return false;
    }
    else {
        if (campo.value.length == conjunto1)
            campo.value = campo.value + separacao1;
        else if (campo.value.length == conjunto2)
            campo.value = campo.value + separacao1;
        else if (campo.value.length == conjunto3 && campo.maxLength > 10)
            campo.value = campo.value + separacao2;
        else if (campo.value.length == conjunto4)
            campo.value = campo.value + separacao3;
        else if (campo.value.length == conjunto5)
            campo.value = campo.value + separacao3;
    }

}

// Alterar icone ao esconder ou mostrar divs
function changeIcon(elemento, claseAlternar, clasemenor, clasemajor) {

    // Procura por ui-icon-minusthick.
    var cls = clasemenor;
    var reg = new RegExp('(\\s|^)' + cls + '(\\s|$)');
    var element = $(elemento).find('' + claseAlternar + ':first').get(0);
    var existe = element.className.match(new RegExp('(\\s|^)' + cls + '(\\s|$)'));

    // Altera icon 'ui-icon-minusthick' por 'ui-icon-plusthick'.
    if (existe) {
        element.className = element.className.replace(reg, ' ');
        cls = clasemajor;
        element.className += " " + cls;
    } // Altera icon 'ui-icon-plusthick' por 'ui-icon-minusthick'.
    else {
        cls = clasemajor;
        reg = new RegExp('(\\s|^)' + cls + '(\\s|$)');
        element.className = element.className.replace(reg, ' ');
        cls = clasemenor;
        element.className += " " + cls;
    }
}

function ValidarExpresionRegular(elemento, expr) {
    var oREGEXP = new RegExp(expr);
    if (!oREGEXP.test(elemento.value)) {
        $(elemento).val('');
    }
}

function AbrirPopupNova(url, nome, altura, largura, outrosParametros) {

    var esq = (screen.availWidth / 2) - (largura / 2);
    var topo = (screen.availHeight / 2) - (altura / 2);
    var outros = 'left=' + esq + ', top=' + topo + ', width=' + largura + ', height=' + altura + outrosParametros;

    window.open(url, nome, outros);

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

            if (Elementocheckbox[i].type === 'radio')
                if (Elementocheckbox[i].checked == true)
                    QteLinhaPreenchida++;
        }

        if (QteLinhaPreenchida == 0) {
            alert(MsgAlertLinhaPreenchida);
        } else if (QteLinhaPreenchida > 1 && MsgLinhasPreenchidas.length > 0) {
            alert(MsgLinhasPreenchidas);
        } else if (MsgConfirmacao.length > 0) {
            if (confirm(MsgConfirmacao))
                return true;
            else
                return false;
        } else {
            return true;
        }

    } catch (e) {
        return true;
    }
}
