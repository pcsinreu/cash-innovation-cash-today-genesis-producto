
/// configura evento onblur de um controle da tela (configura proximo controle que receberá o foco)
function SetarProximoFoco(IdControleRetorno) {
    var proximoControle = document.getElementById(IdControleRetorno);
    proximoControle.focus();
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

var campoPrincipal = "";

function genesisAlertError(msg, erro) {
    genesisAlert(msg, erro, "error", true);
    setTimeout(function () {
        $("#btnClose").focus();
    }, 500);
}

function genesisAlertLoading(msg, naoAlterarFoco) {
    genesisAlert(msg, "", "loading", false, naoAlterarFoco);
}

function genesisAlert(msg, erro, tipo, close, naoAlterarFoco) {
    if (msg == "") {
        $("#dvAlert").css("display", "none");
        $("#dvAlertLabel").html("");
        $("#dvAlertErro").html("");
        if (naoAlterarFoco != true)
            focusInicial();
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

function focusInicial() {
    if (campoPrincipal != null && campoPrincipal != "") {
        $("#" + campoPrincipal).focus();
    }
}