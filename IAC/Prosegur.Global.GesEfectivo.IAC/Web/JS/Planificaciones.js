var txtDiaSemanaAtual;

///Funções específicas da tela de planificações
function formataHora() {
    $('.txtDiaSemana').mask('00:00:00');
    $('#txtMinutosAcreditacionForm').mask('999999');
    habilitaFecha();
}

function ObtenerCampoDiaSemana(campo) {

    var numeroCampoAtual = campo.getAttribute('numero_campo');
    var id_componente = campo.getAttribute('id_componente');

    //obtendo o numero do campo abaixo do atual
    var numeroCampoAbaixo = parseInt(numeroCampoAtual) + 1;

    //obtendo o prefixo padrão de todos os campos
    var prefixoNomeCampo = id_componente.replace(/[0-9]/g, '');

    //formando o nome do campo abaixo
    var idCampoAbaixo = prefixoNomeCampo + numeroCampoAbaixo;

    var teste = "[id_componente='" + idCampoAbaixo + "']";

    var txtDiaSemanaAbaixo = document.querySelector(teste);

    return txtDiaSemanaAbaixo;
}

//Habilita os textBoxs do grid de programações na tela de planificação
//Regra: Quando o foco sai do campo atual, a data informada deve ser válida
//Se não for, o campo é limpo e o botão abaixo é desabilitado
function ValidarHoraInformada(campo) {

    //Valida a hora no momento que o cursor sai do campo
    var isValid = /^([0-1]?[0-9]|2[0-3]):([0-5][0-9])(:[0-5][0-9])?$/.test(campo.value);

    //Se a hora for inválida, desabilitar o campo abaixo 
    if (!isValid) {
        campo.value = "";

        var txtDiaSemanaAbaixo = ObtenerCampoDiaSemana(campo);

        //verificando se existe o campo
        if (txtDiaSemanaAbaixo) {
            if (txtDiaSemanaAbaixo.value == "")
                txtDiaSemanaAbaixo.disabled = true;
        }
    }
    HabilitarTxtProgramaciones(campo);

    var hdfCambioHorario = document.getElementById("hdfCambioHorario");
    if (hdfCambioHorario.value != "HorarioCambiado") {
        var hdfAcao = document.getElementById("hdfAcao").value;

        if (hdfAcao == "Modificacion") {           
            VerificarCambio(campo);
        }
    }

    return false;
}


//Habilita os textBoxs do grid de programações na tela de planificação
//Regra: À medida que o textBox de cima for informado, deve ser habilitado o componente abaixo
function HabilitarTxtProgramaciones(campo) {

    var txtDiaSemanaAbaixo = ObtenerCampoDiaSemana(campo);

    //verificando se existe o campo
    if (txtDiaSemanaAbaixo) {

        if (campo.value != "" || (txtDiaSemanaAbaixo.value != "" && !txtDiaSemanaAbaixo.disabled ))
            txtDiaSemanaAbaixo.disabled = false;
        else
            txtDiaSemanaAbaixo.disabled = true;

        HabilitarTxtProgramaciones(txtDiaSemanaAbaixo);

    }


}

//Habilita os componentes de seleção de data
function habilitaFecha() {
    
    var elements = document.getElementsByClassName('ui-datepicker-trigger');

    if (elements) {
        if (elements.length > 0) {
            var hdfAcao = document.getElementById("hdfAcao").value;
            var fechaInicio = elements[0];
            var fechaFin = elements[1];
            if (hdfAcao == "Baja" || hdfAcao == "Consulta") {

                fechaInicio.disabled = true;
                fechaFin.disabled = true;
            }
            else {
                fechaInicio.disabled = false;
                fechaFin.disabled = false;
            }
        }
    }
}

//Habilita os textBoxs do grid de programações na tela de planificação após o postback 
function HabilitaTxt(id_campo) {

    var hdfAcao = document.getElementById("hdfAcao").value;

    if (hdfAcao == "Modificacion" || hdfAcao == "Alta") {
        var elements = document.getElementsByName(id_campo);
        if (elements) {
            var campo = elements[0];
            if (campo) {

                if (campo.value != "")
                    campo.disabled = false;

                HabilitarTxtProgramaciones(campo);
            }
        }
    }
}

///Se o usuario clicar em sim, não realizar mais verificações de mudanças em horarios
function CambioHorarioSim() {
    var hdfCambioHorario = document.getElementById("hdfCambioHorario");

    if (hdfCambioHorario)
        hdfCambioHorario.value = "HorarioCambiado";

    return false;
}

//Se o usuario clicar em não ,voltar o campo para o valor salvo na planificação
function CambioHorarioNao(valorOriginal) {

    var txtDiaSemana = txtDiaSemanaAtual;

    if (txtDiaSemana)
        txtDiaSemana.value = (valorOriginal != "null" ? valorOriginal : "");

    HabilitarTxtProgramaciones(txtDiaSemana);

    txtDiaSemanaAtual = null;

    return false;
}

//function VerificaCambioHorario(campo) {
//    var numeroCampoAtual = campo.getAttribute('numero_campo');
//    var id_componente = campo.getAttribute('id_componente');
//    var idCampo = id_componente.replace(/[0-9]/g, '');
//    var valor = campo.value;

//    txtDiaSemanaAtual = campo;

//    jQuery.ajax({
//        url: 'BusquedaPlanificaciones.aspx/VerificaModificacionHorario',
//        type: "POST",
//        dataType: "json",
//        data: "{valor: '" + valor + "', campo: '" + idCampo + "', numero_campo: '" + numeroCampoAtual + "'}",
//        contentType: "application/json; charset=utf-8",
//        success: function (data, text) {

//            var respuesta = JSON.parse(data.d);
//            if (respuesta.HayModificacion) {
//                var scriptSim = "CambioHorarioSim()";
//                var scriptNao = "CambioHorarioNao('" + respuesta.Valor + "')";
//                ExibirMensagemNaoSim(_Diccionario.msg_horario_cambiado, _Diccionario.titulo_msg, scriptSim, scriptNao, _Diccionario.btnSim, _Diccionario.btnNao);
//            }
//            return false;

//        },
//        error: function (request, status, error) {
//        }
//    });
//    return false;
//}


function VerificarCambio(campo) {
    
    var valor_anterior = campo.getAttribute('valor_anterior'); 
    var valor_atual = campo.value;

    //Armazena campo atual para retornar o valor anterior caso o usuario clique em 'Não'
    txtDiaSemanaAtual = campo;

    if (valor_atual != valor_anterior) {
        var scriptSim = "CambioHorarioSim()";
        var scriptNao = "CambioHorarioNao('" + valor_anterior + "')";
        ExibirMensagemNaoSim(_Diccionario.msg_horario_cambiado, _Diccionario.titulo_msg, scriptSim, scriptNao, _Diccionario.btnSim, _Diccionario.btnNao);
    }    

}