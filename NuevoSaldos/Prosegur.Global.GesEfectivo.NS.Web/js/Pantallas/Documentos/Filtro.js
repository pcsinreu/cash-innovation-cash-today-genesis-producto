$(document).ready(function () {


    AlteraVisibilidadeCamposFiltro('True');
    HabilitaDesabilitaDisponible();

});


function AlteraVisibilidadeCamposFiltro(Ativo) {

    if (Ativo === "True" && $('#ctl00_ContentPlaceHolder1_rdNenhum').is(':checked')) {

        $('#ctl00_ContentPlaceHolder1_txtNComprovante').attr('disabled', 'disabled');
        $('#ctl00_ContentPlaceHolder1_lstItensAdicionados').attr('disabled', 'disabled');

        $('#ctl00_ContentPlaceHolder1_txtNComprovante').text = "";

        $('#ctl00_ContentPlaceHolder1_lstItensAdicionados option').remove();


    }
    else {

        $('#ctl00_ContentPlaceHolder1_txtNComprovante').removeAttr('disabled');
        $('#ctl00_ContentPlaceHolder1_lstItensAdicionados').removeAttr('disabled');
    }

}

function HabilitaDesabilitaDisponible() {

    if ($('#ctl00_ContentPlaceHolder1_chkDisponivel').is(':checked')) {        
        $('#ctl00_ContentPlaceHolder1_rdDisponivel').removeAttr('disabled');
        $('#ctl00_ContentPlaceHolder1_rdNaoDisponivel').removeAttr('disabled');

        if ($('#ctl00_ContentPlaceHolder1_rdDisponivel:checked,#ctl00_ContentPlaceHolder1_rdNaoDisponivel:checked').length == 0) {
            $('#ctl00_ContentPlaceHolder1_rdDisponivel').get(0).checked = true;
        }

    }
    else {

        $('#ctl00_ContentPlaceHolder1_rdDisponivel').attr('disabled', 'disabled');
        $('#ctl00_ContentPlaceHolder1_rdNaoDisponivel').attr('disabled', 'disabled');

    }
}