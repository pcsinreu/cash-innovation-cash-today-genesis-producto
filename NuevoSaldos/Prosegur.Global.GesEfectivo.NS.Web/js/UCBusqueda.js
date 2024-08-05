var _busquedaPage = 0; var _busquedaLista; var _busquedaTipo = ''; var eventChange = false; var _legendaFirst = 'Primera'; var _legendaLast = 'Ultima';
var _sufix = ''; var _associacion = '';

function ExibirPopupBusquedaAvanzado(tipo, sufix) {

    if (tipo) { _busquedaTipo = tipo };
    if (sufix) { _sufix = sufix };

    ConfigBusquedaTipoSetor(tipo);
    
    if (esMult(_busquedaTipo, _sufix)) { crearModal('dvBusquedaAvanzada', 778, 500, function () { }); } else { crearModal('dvBusquedaAvanzada', 778, 500, function () { }); }
    cargarControleBusqueda(_busquedaTipo, _sufix);
    showModal('dvBusquedaAvanzada');

    $('#txtBusquedaCodigo').val($('#txtBusquedaCodigo_' + tipo + '_' + sufix).val());    
    $('#txtBusquedaDescripcion').val($('#txtBusquedaDescripcion_' + tipo + '_' + sufix).val());
}

function ClosePopupBusquedaAvanzado(tipo, sufix) {
    limparCampoBusqueda(tipo, sufix);
    closeModal('dvBusquedaAvanzada');
    $('#txtBusquedaCodigo_' + tipo + '_' + sufix).focus();
    _busquedaLista = null;
    _busquedaTipo = '';
    _busquedaPage = 0;
}

function cargarControleBusqueda(tipo, sufix) {

    if (!eventChange) {
        //eventChange = true;

        limparCampoBusqueda(tipo, sufix);

        var padre = ObtenerPadre(tipo, sufix);

        if (esMult(_busquedaTipo, _sufix) && $('#txtBusquedaSeleccionados_' + tipo + '_' + sufix).val() != '') {
            _SeleccionadosBusquedaAvanzada.splice(0, _SeleccionadosBusquedaAvanzada.length);
            var _selecionados = JSON.parse($('#txtBusquedaSeleccionados_' + tipo + '_' + sufix).val());
            for (var i = 0; i < _selecionados.length; i++) {
                _SeleccionadosBusquedaAvanzada[_SeleccionadosBusquedaAvanzada.length] = JSON.parse('{"Identificador":"' + _selecionados[i].Identificador + '","Codigo":"' + _selecionados[i].Codigo + '","Descripcion":"' + _selecionados[i].Descripcion + '"}');
            }
        }

        genesisAlertLoading(_DiccionarioBusqueda.msg_loading + _DiccionarioBusqueda.msg_obtenerValores);
        var identificadorPadre = '';
        if (padre != null && padre.length > 0) { identificadorPadre = padre[0].Identificador; }

        obtenerValoresAvanzado($('#txtBusquedaCodigo_' + tipo + "_" + sufix).val(), $('#txtBusquedaDescripcion_' + tipo + "_" + sufix).val(), identificadorPadre, true);
        //eventChange = false;
    }
}

function limparCampo(tipo, sufix) {
    console.log("Limpar valores: " + tipo);
    eventChange = true;

    var txtCodigo = $('#txtBusquedaCodigo_' + tipo + '_' + sufix);
    var txtDescripcion = $('#txtBusquedaDescripcion_' + tipo + '_' + sufix);
    var possuiBind = $('#hdPossuiBind_' + tipo + "_" + sufix).val();

    $('#txtBusquedaSeleccionados_' + tipo + '_' + sufix).val('').change();
    $('#txtBusquedaCodigo_' + tipo + '_' + sufix).focus();

    //if (possuiBind == "0") {
        txtCodigo.val('');
        txtDescripcion.val('');
    //}

    configuraFiltro(tipo, sufix);
    eventChange = false;
}

function limparCampoBusqueda(tipo, sufix) {

    if (!tipo) { tipo = _busquedaTipo; } else { _busquedaTipo = tipo; }
    if (!sufix) { sufix = _sufix; } else { _sufix = sufix; }

    console.log("Limpar valores busqueda: " + tipo);
    eventChange = true;
    $('#dvBusquedaAvanzada > #dvBusquedaLista').html("<span style='padding:5px; color: #999999;font-style: italic;'>" + _DiccionarioBusqueda.msg_nenhumRegistroEncontrado + " </span>");
    _SeleccionadosBusquedaAvanzada.splice(0, _SeleccionadosBusquedaAvanzada.length);
    $('#txtBusquedaCodigo').val('');
    $('#txtBusquedaDescripcion').val('');
    ConfigBusquedaTipoSetor(tipo);
    $('#txtBusquedaCodigo').focus();
    eventChange = false;
}

function obtenerValorPorCodigo(tipo, campo, sufix, associacion) {
    $('#txtBusquedaDescripcion_' + tipo + '_' + sufix).val('');
    obtenerValor(tipo, campo, sufix, associacion);
}

function obtenerValorPorDescription(tipo, campo, sufix, associacion) {
    $('#txtBusquedaCodigo_' + tipo + '_' + sufix).val('');
    obtenerValor(tipo, campo, sufix, associacion);
}

function obtenerValor(tipo, campo, sufix, associacion) {
    if (!eventChange) {

        var padre = ObtenerPadre(tipo, sufix);

        obtenerValores($('#txtBusquedaCodigo_' + tipo + '_' + sufix), $('#txtBusquedaDescripcion_' + tipo + '_' + sufix), tipo, padre, false, sufix, associacion);
    }

}

function ObtenerPadre(tipo, sufix) {
    var padre = null;
    var sufixoPadre = $('#hdAssociacionPadre_' + sufix).val();


    switch (tipo) {
        case 'SubCliente':
            if ($('#txtBusquedaSeleccionados_Cliente_' + sufixoPadre).val() != '') {
                padre = JSON.parse($('#txtBusquedaSeleccionados_Cliente_' + sufixoPadre).val());
            }
            break;
        case 'PtoServicio':
            if ($('#txtBusquedaSeleccionados_SubCliente_' + sufixoPadre).val() != '') {
                padre = JSON.parse($('#txtBusquedaSeleccionados_SubCliente_' + sufixoPadre).val());
            }
            break;
        case 'SubCanal':
            if ($('#txtBusquedaSeleccionados_Canal' + sufixoPadre).val() != '') {
                padre = JSON.parse($('#txtBusquedaSeleccionados_Canal_' + sufixoPadre).val());
            }
            break;
        case 'Planta':
            if ($('#txtBusquedaSeleccionados_Delegacion_' + sufixoPadre).val() != '') {
                padre = JSON.parse($('#txtBusquedaSeleccionados_Delegacion_' + sufixoPadre).val());
            }
            break;
        case 'Sector':
            if ($('#txtBusquedaSeleccionados_Planta' + sufixoPadre).val() != '') {
                padre = JSON.parse($('#txtBusquedaSeleccionados_Planta' + sufixoPadre).val());
            }
            break;
    }

    return padre;
}

function obtenerValores(codigo, descripcion, tipo, padre, esBusquedaAvanzada, sufix, associacion) {
    if (!eventChange) {
        if (codigo.val()) { codigo.val(codigo.val().toUpperCase()) };
        genesisAlertLoading(_DiccionarioBusqueda.msg_loading + _DiccionarioBusqueda.msg_obtenerValores);

        var identificadorPadre = '';
        if (tipo != null) { _busquedaTipo = tipo; }
        if (padre != null) { identificadorPadre = padre[0].Identificador; }
        if (sufix != null) { _sufix = sufix }
        if (associacion != null) { _associacion = associacion }
        obtenerValoresAvanzado(codigo.val(), descripcion.val(), identificadorPadre, esBusquedaAvanzada);
    }
}

function obtenerValoresAvanzado(codigo, descripcion, identificadorPadre, esBusquedaAvanzada) {
    if (!eventChange) {

        if (!identificadorPadre) {
            var padre = ObtenerPadre(_busquedaTipo, _sufix);
            if (padre != null && padre.length > 0) { identificadorPadre = padre[0].Identificador; }
        }

        console.log("obtenerValoresAvanzado: (codigo='" + codigo + "' -  descripcion = '" + descripcion + "' - identificadorPadre: '" + identificadorPadre + "' - tipo = '" + _busquedaTipo + "')");
        
        if (_busquedaTipo == 'Sector') {
            obtenerValoresAvanzadoTipoSector(codigo, descripcion, identificadorPadre, esBusquedaAvanzada);
        }
        else {
            obtenerValoresAvanzadoInterno(codigo, descripcion, identificadorPadre, esBusquedaAvanzada);
        }
           
    }
}

function obtenerValoresAvanzadoInterno(codigo, descripcion, identificadorPadre, esBusquedaAvanzada) {
    jQuery.ajax({
        url: '../../ServiciosInterface.asmx/obtenerValoresAvanzado',
        type: "POST",
        dataType: "json",
        data: "{codigo: '" + codigo + "', descripcion: '" + descripcion + "', identificadorPadre: '" + identificadorPadre + "', tipo: '" + _busquedaTipo + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data, text) {
            var json_x = JSON.parse(data.d);
            if (json_x.CodigoError == "0") {
                if (json_x.Respuesta != null) {
                    _busquedaLista = json_x.Respuesta;
                    cargarListaBusqueda(1, esBusquedaAvanzada, _busquedaTipo, _sufix);
                } else {
                    if (!esBusquedaAvanzada) { ExibirPopupBusquedaAvanzado(_busquedaTipo, _sufix); }
                    console.log(_DiccionarioBusqueda.msg_producidoError);
                    $('#dvBusquedaLista').html("<span style='padding:5px; color: #999999;font-style: italic;'>" + _DiccionarioBusqueda.msg_nenhumRegistroEncontrado + "</span>");
                }
            } else {
                genesisAlertError(_DiccionarioBusqueda.msg_producidoError + json_x.MensajeError, json_x.MensajeErrorDescriptiva);
            }
            genesisAlertLoading("");
        },
        error: function (request, status, error) {
            genesisAlertError(_DiccionarioBusqueda.msg_producidoError + error, request.responseText);
            genesisAlertLoading("");
        }
    });
}

function obtenerValoresAvanzadoTipoSector(codigo, descripcion, identificadorPadre, esBusquedaAvanzada) {

    var considerarTodosNiveis = $('#chkConsiderarTodosNiveis')[0].checked;

    jQuery.ajax({
        url: '../../ServiciosInterface.asmx/obtenerValoresAvanzadoTipoSector',
        type: "POST",
        dataType: "json",
        data: "{codigo: '" + codigo + "', descripcion: '" + descripcion + "', identificadorPadre: '" + identificadorPadre + "', considerarTodosNiveis: '" + considerarTodosNiveis + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data, text) {
            var json_x = JSON.parse(data.d);
            if (json_x.CodigoError == "0") {
                if (json_x.Respuesta != null) {
                    _busquedaLista = json_x.Respuesta;
                    cargarListaBusqueda(1, esBusquedaAvanzada, _busquedaTipo, _sufix);
                } else {
                    if (!esBusquedaAvanzada) { ExibirPopupBusquedaAvanzado(_busquedaTipo, _sufix); }
                    console.log(_DiccionarioBusqueda.msg_producidoError);
                    $('#dvBusquedaLista').html("<span style='padding:5px; color: #999999;font-style: italic;'>" + _DiccionarioBusqueda.msg_nenhumRegistroEncontrado + "</span>");
                }
            } else {
                genesisAlertError(_DiccionarioBusqueda.msg_producidoError + json_x.MensajeError, json_x.MensajeErrorDescriptiva);
            }
            genesisAlertLoading("");
        },
        error: function (request, status, error) {
            genesisAlertError(_DiccionarioBusqueda.msg_producidoError + error, request.responseText);
            genesisAlertLoading("");
        }
    });
}

function cargarListaBusqueda(p, esBusquedaAvanzada, tipo, sufix) {
    console.log("cargarListaBusqueda");

    eventChange = true;
    if (_busquedaLista != null) {

        console.log("cuantidad registros: " + _busquedaLista.length);

        if (_busquedaLista.length == 1) {

            var tipo = _busquedaTipo;
            var codigo = _busquedaLista[0].Codigo;
            var descripcion = _busquedaLista[0].Descripcion;
            var identificador = _busquedaLista[0].Identificador;
            var possuiBind = $('#hdPossuiBind_' + tipo + "_" + sufix).val();
            if (esBusquedaAvanzada) { ClosePopupBusquedaAvanzado(); }

            console.log(codigo + ' - ' + descripcion + ' - ' + identificador);

            if (possuiBind == '0') {
                $('#txtBusquedaCodigo_' + tipo + '_' + sufix).val(codigo);
                $('#txtBusquedaDescripcion_' + tipo + '_' + sufix).val(descripcion);
            }

            $('#txtBusquedaSeleccionados_' + tipo + '_' + sufix).val('[{"Identificador":"' + identificador + '","Codigo":"' + codigo + '","Descripcion":"' + descripcion + '"}]').change();
            $('#txtBusquedaDescripcion_' + tipo + '_' + sufix).focus();
            configuraFiltro(tipo, sufix);

        } else if (_busquedaLista.length > 1) {

            if (!esBusquedaAvanzada) {
                ExibirPopupBusquedaAvanzado(_busquedaTipo, _sufix);
            }
            cargarPaginasBusqueda(p);

        } else {
            $('#dvBusquedaLista').html("<span style='padding:5px; color: #999999;font-style: italic;'>" + _DiccionarioBusqueda.msg_nenhumRegistroEncontrado + "</span>");
            if (!esBusquedaAvanzada) { showModal('dvBusquedaAvanzada'); }
        }
    }
    eventChange = false;
}

function cargarPaginasBusqueda(p) {
    console.log("cargar pagina: " + p);

    pageBusqueda = p;
    if (pageBusqueda < 1) { pageBusqueda = 1; }

    var conteudo = '<div style="height: ';
    if (esMult(_busquedaTipo, _sufix)) { conteudo += '260px'; } else { conteudo += '210px'; }
    conteudo += ';" class="gridAbono gridAbonoNO"><table><thead><tr><th style="width: 5%;"></th><th style="width: 20%;">' + _DiccionarioBusqueda.Campo_Codigo + '</th><th style="width: 75%;">' + _DiccionarioBusqueda.Campo_Descripcion + '</th></tr></thead><tbody>';

    var cuant = 10;
    if (cuant > _busquedaLista.length) { cuant = _busquedaLista.length; }

    var cuantPaginas = 1;
    if (_busquedaLista.length % cuant == 0) {
        cuantPaginas = _busquedaLista.length / cuant;
    } else {
        cuantPaginas = (_busquedaLista.length - (_busquedaLista.length % cuant)) / cuant + 1;
    }

    if (pageBusqueda > cuantPaginas) { pageBusqueda = cuantPaginas; }

    var max = (pageBusqueda * cuant);
    var min = (max - cuant);

    if (max > _busquedaLista.length) { max = _busquedaLista.length; }

    for (i = min; i < max; i++) {
        var cor = 'FFFFFF';
        if (i % 2 == 0) { cor = 'FFFDF2'; }
        conteudo += '<tr style="background-color: #' + cor + ';"><td style="text-align:center; width: 5%;"><input value="' + _busquedaLista[i].Identificador + '" name="rbSelecionado" type="';
        if (!esMult(_busquedaTipo, _sufix)) {
            conteudo += 'radio';
        } else {
            conteudo += 'checkbox';
        }
        conteudo += '" id="rbSelecionado' + i + '" class="radio_selecao" Codigo="' + _busquedaLista[i].Codigo + '"  Identificador="' + _busquedaLista[i].Identificador + '" Descripcion="' + _busquedaLista[i].Descripcion + '" onclick="javascript: seleccionarBusquedaAvanzada();" onkeydown="javascript: cambiarPagina();" /></td><td style="width: 20%;">' + _busquedaLista[i].Codigo + '</td><td style="width: 75%;">' + _busquedaLista[i].Descripcion + '</td></tr>';
    }

    conteudo += '</tbody></table></div>';

    if (cuantPaginas > 1) {
        conteudo += '<div id="dvPaginacion" style="width:650px; height:auto; margin-left: 5px;"></div>';
        document.getElementById('dvBusquedaLista').innerHTML = conteudo;

        $("#dvPaginacion").paginate({
            count: cuantPaginas,
            start: pageBusqueda,
            display: 21,
            border: false,
            text_color: '#888',
            background_color: '#EEE',
            text_hover_color: 'black',
            background_hover_color: '#CFCFCF',
            legendaFirst: _legendaFirst,
            legendaLast: _legendaLast,
            onChange: function (page) {
                cargarPaginasBusqueda(page);
            }
        });
    } else { document.getElementById('dvBusquedaLista').innerHTML = conteudo; }

    if (_busquedaLista.length > 0) {
        inputs = document.getElementById('dvBusquedaLista').getElementsByTagName('input');
        for (x = 0; x < inputs.length; x++) {
            for (s = 0; s < _SeleccionadosBusquedaAvanzada.length; s++) {
                if (_SeleccionadosBusquedaAvanzada[s].Identificador == inputs[x].getAttribute('Identificador')) {
                    inputs[x].checked = true;
                }
            }
        }
        inputs[0].focus();
    }
}

function seleccionarBusquedaAvanzada() {
    eventChange = true;

    //https://procde.prosegur.com/jira/browse/GENPLATINT-2121
    //if (esMult(_busquedaTipo, _sufix)) { _SeleccionadosBusquedaAvanzada.splice(0, _SeleccionadosBusquedaAvanzada.length); }

    inputs = document.getElementById('dvBusquedaLista').getElementsByTagName('input');
    for (x = 0; x < inputs.length; x++) {
        if (inputs[x].checked == true) {

            if (!valorExisteBusquedaAvanzada(inputs[x].getAttribute('Identificador'))) {
                _SeleccionadosBusquedaAvanzada[_SeleccionadosBusquedaAvanzada.length] = JSON.parse('{"Identificador":"' + inputs[x].getAttribute('Identificador') + '","Codigo":"' + inputs[x].getAttribute('Codigo') + '","Descripcion":"' + inputs[x].getAttribute('Descripcion') + '"}');

                if (esMult(_busquedaTipo, _sufix)) {
                    $('#txtBusquedaCodigo_' + _busquedaTipo + '_' + _sufix).val(inputs[x].getAttribute('Codigo'));
                    $('#txtBusquedaDescripcion_' + _busquedaTipo + '_' + _sufix).val(inputs[x].getAttribute('Descripcion'));
                }
            }

        } else {

            for (s = 0; s < _SeleccionadosBusquedaAvanzada.length; s++) {
                if (_SeleccionadosBusquedaAvanzada[s].Identificador == inputs[x].getAttribute('Identificador')) {
                    _SeleccionadosBusquedaAvanzada.splice(s, 1);
                }
            }
        }
    }
    eventChange = false;
}

function valorExisteBusquedaAvanzada(a) {
    for (s = 0; s < _SeleccionadosBusquedaAvanzada.length; s++) {
        if (_SeleccionadosBusquedaAvanzada[s].Identificador == a) {
            return true;
        }
    }
    return false;
}

function aceptarBusquedaAvanzada() {
    console.log("seleccionarBusquedaAvanzada: Cantidad: " + _SeleccionadosBusquedaAvanzada.length);

    eventChange = true;

    if (_SeleccionadosBusquedaAvanzada.length == 0) {
        alert(_DiccionarioBusqueda.msg_nenhumRegistroSeleccionado);

    } else {

        cargarCampoMult(_busquedaTipo, _sufix, _SeleccionadosBusquedaAvanzada);
        var tipo = _busquedaTipo;
        ClosePopupBusquedaAvanzado(_busquedaTipo, _sufix);
        $('#txtBusquedaDescripcion_' + tipo + '_' + _sufix).focus();
        configuraFiltro(tipo, _sufix);
    }


    eventChange = false;
}

function configuraFiltro(tipo, sufix) {
    console.log("configuraFiltro - " + tipo);

    var meAsociacion = $('#hdAssociacion_' + sufix).val();
    var asociacionDelMeHijo = $('#hdAssociacion_' + meAsociacion).val();
    //eventChange = true;
    switch (tipo) {
        case 'Banco':
            break;
        case 'Cliente':
            //#region Logica Case Cliente
            if ($('#txtBusquedaSeleccionados_' + tipo + '_' + sufix).val() != '') {

                $('#' + meAsociacion).css("display", "block");
            }
            else {
                $('#' + meAsociacion).css("display", "none");
                
                $('#' + asociacionDelMeHijo).css("display", "none");
                
                $('#txtBusquedaSeleccionados_SubCliente' + "_" + meAsociacion).val('').change();
                $('#txtBusquedaSeleccionados_PtoServicio' + "_" + asociacionDelMeHijo).val('').change();
            }
            //#endregion
            break;
        case 'SubCliente':
            //#region Logica Case SubCliente
            if ($('#txtBusquedaSeleccionados_' + tipo + '_' + sufix).val() != '') {
                $('#'+meAsociacion).css("display", "block");
            }
            else {
                $('#' + meAsociacion).css("display", "none");
                $('#txtBusquedaSeleccionados_PtoServicio' + "_" + meAsociacion).val('').change();
            }
            //#endregion
            break;
        case 'PtoServicio':
            break;
        case 'Canal':
            //#region Logica Case Canal
            if ($('#txtBusquedaSeleccionados_' + tipo + '_' + sufix).val() != '') {
                $('#' + meAsociacion).css("display", "block");
            }
            else {
                $('#' + meAsociacion).css("display", "none");
                $('#txtBusquedaSeleccionados_SubCanal' + "_" + meAsociacion).val('').change();
            }
            //#endregion
            break;
        case 'SubCanal':
            break;
        case 'Delegacion':
            //#region Logica Case Delegacion
            if ($('#txtBusquedaSeleccionados_' + tipo + '_' + sufix).val() != '') {
                $('#' + meAsociacion).css("display", "block");
            }
            else {
                $('#' + meAsociacion).css("display", "none");
                $('#' + asociacionDelMeHijo).css("display", "none");
                $('#txtBusquedaSeleccionados_Planta_' + meAsociacion).val('').change();
                $('#txtBusquedaSeleccionados_Sector_' + asociacionDelMeHijo).val('').change();
            }
            //#endregion
            break;
        case 'Planta':
            if ($('#txtBusquedaSeleccionados_' + tipo + '_' + sufix).val() != '') {
                $('#dvBusqueda_Sector').css("display", "block");
            }
            else {
                $('#' + meAsociacion).css("display", "none");
                $('#txtBusquedaSeleccionados_Sector_' + meAsociacion).val('').change();
            }
            break;
        case 'Sector':
            break;
    }
    //eventChange = false;
}

function cambiarPagina() {
    if (event.which || event.keyCode) {

        if ((event.which == 35) || (event.keyCode == 35)) {
            cargarPaginasBusqueda(99999999999);
        }
        if ((event.which == 36) || (event.keyCode == 36)) {
            cargarPaginasBusqueda(1);
        }
        if ((event.which == 37) || (event.keyCode == 37)) {
            cargarPaginasBusqueda(pageBusqueda - 1);
        }
        if ((event.which == 39) || (event.keyCode == 39)) {
            cargarPaginasBusqueda(pageBusqueda + 1);
        }

    }
    return false;
}

function cargarCampoMult(tipo, sufix, _selecionados) {

    if (!sufix) { sufix = _sufix };
    if (!tipo) { tipo = _busquedaTipo };

    var possuiBind = $('#hdPossuiBind_' + tipo + "_" + sufix).val();

    eventChange = true;
    if (_selecionados.length == 1) {
        if (possuiBind == '0') {
            $('#txtBusquedaCodigo_' + tipo + '_' + sufix).val(_selecionados[0].Codigo);
            $('#txtBusquedaDescripcion_' + tipo + '_' + sufix).val(_selecionados[0].Descripcion);
        }
        
        $('#dvBusquedaBotonLimpar_' + tipo + '_' + sufix).css("display", "block");
        $('#dvBusquedaValores_' + tipo + '_' + sufix).css("display", "none");

    } else if (_selecionados.length > 1) {

        if (possuiBind == '0') {
            $('#txtBusquedaCodigo_' + tipo + '_' + sufix).val('');
            $('#txtBusquedaDescripcion_' + tipo + '_' + sufix).val('');
        }

        $('#dvBusquedaBotonLimpar_' + tipo + '_' + sufix).css("display", "none");
        $('#dvBusquedaValores_' + tipo + '_' + sufix).css("display", "block");
        $('#txtBusquedaValores_' + tipo + '_' + sufix).html('');

        if ($('#hdPossuiBind_' + tipo + '_' + sufix).val()== "0") {
            var conteudo = "";
            for (s = 0; s < _selecionados.length; s++) {
                conteudo += "<option value='" + _selecionados[s].Identificador + "'>" + _selecionados[s].Codigo + " " + _selecionados[s].Descripcion + "</option>";
            }

            $('#txtBusquedaValores_' + tipo + '_' + sufix).append(conteudo);
        }
    }

    $('#txtBusquedaSeleccionados_' + tipo + '_' + sufix).val(JSON.stringify(_selecionados)).change();

    limparCampoSelecaoSeBind(tipo, sufix);

    eventChange = false;
}

function limparCampoMult(tipo, sufix) {
    eventChange = true;

    console.log('limparCampoMult: ' + tipo);
    var _selecionados = JSON.parse($('#txtBusquedaSeleccionados_' + tipo + '_' + sufix).val());
    var select = document.getElementById('txtBusquedaValores_' + tipo + '_' + sufix).options;

    var itensSelecionados = 0;
    for (var i = 0, l = select.length, o; i < l; i++) {
        if (select[i].selected) {
            for (s = 0; s < _selecionados.length; s++) {
                if (_selecionados[s].Identificador == select[i].value) {
                    _selecionados.splice(s, 1);
                    ++itensSelecionados;
                }
            }
        }
    }

    if (itensSelecionados == 0) {
        return;
    }

    cargarCampoMult(tipo, sufix, _selecionados);
    eventChange = false;
}

function esMult(tipo, sufix) {
    var esMulti = $('#txtBusquedaMulti_' + tipo + '_' + sufix).val();
    if (esMulti == '1') { return true; } else { return false; }
}

function limparCampoSelecaoSeBind(tipo, sufix) {

    if ($('#hdPossuiBind_' + tipo + "_" + sufix).val() == "1") {
        _SeleccionadosBusquedaAvanzada.splice(0, _SeleccionadosBusquedaAvanzada.length);
    }
}

function ConfigBusquedaTipoSetor(tipo) {

    $('#chkConsiderarTodosNiveis')[0].checked = false;

    if (tipo == 'Sector') {
        $('.tipoSector').show();
    }
    else {
        $('.tipoSector').hide();
    }
}