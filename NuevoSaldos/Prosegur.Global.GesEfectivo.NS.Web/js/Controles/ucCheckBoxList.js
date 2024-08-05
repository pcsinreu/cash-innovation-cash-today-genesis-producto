// Função para marcar e des
function MarcarDesmarcarTodos(IdControle) {
    var IdhdnJson = IdControle.replace("chkMarcarTodos", "hdnJson");
    var hdnJson = document.getElementById(IdhdnJson);
    var parametros = JSON.parse(hdnJson.value);

    var chkMarcarTodos = document.getElementById(IdControle);

    if (chkMarcarTodos != undefined) {
        var nome = 'divCheckBoxList_' + parametros.IDComponente;
        var divcheckboxlist = $('[id$=' + nome + ']')[0];

        var checkboxlist = divcheckboxlist.getElementsByTagName('input');

        if (checkboxlist != undefined) {

            if (chkMarcarTodos.checked == true) {
                for (a = 0; a < checkboxlist.length; a++) {
                    checkboxlist[a].checked = true;
                }

            }
            else {
                for (a = 0; a < checkboxlist.length; a++) {
                    checkboxlist[a].checked = false;
                }

            }
        }

    }
}

function ControleSelecionados(IdControle) {
   
    var IdhdnJson = IdControle.replace("chkGenerico", "hdnJson");
    var IdchkMarcarTodos = IdControle.replace("chkGenerico", "chkMarcarTodos");

    var hdnJson = document.getElementById(IdhdnJson);
    var chkMarcarTodos = document.getElementById(IdchkMarcarTodos);

    var parametros = JSON.parse(hdnJson.value);

    if (chkMarcarTodos != undefined) {

        var ContadorMarcados = 0;

        var nome = 'divCheckBoxList_' + parametros.IDComponente;
        var divcheckboxlist = $('[id$=' + nome + ']')[0];

        var checkboxlist = divcheckboxlist.getElementsByTagName('input');

        if (checkboxlist != undefined) {

            for (a = 0; a < checkboxlist.length; a++) {
                if (checkboxlist[a].checked == true) {
                    ContadorMarcados += 1;
                }
            }
            if (ContadorMarcados == checkboxlist.length) {
                chkMarcarTodos.checked = true;

            }
            else {
                chkMarcarTodos.checked = false;

            }
        }
    }
}