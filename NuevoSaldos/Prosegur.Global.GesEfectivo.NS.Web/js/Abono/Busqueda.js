Date.prototype.addDays = function (days) {
    var dat = new Date(this.valueOf());
    dat.setDate(dat.getDate() + days);
    return dat;
}

Date.prototype.formatAbonoTela = function () {
    var yyyy = this.getFullYear();
    var mm = this.getMonth() < 9 ? "0" + (this.getMonth() + 1) : (this.getMonth() + 1); // getMonth() is zero-based
    var dd = this.getDate() < 10 ? "0" + this.getDate() : this.getDate();
    var hh = this.getHours() < 10 ? "0" + this.getHours() : this.getHours();
    var min = this.getMinutes() < 10 ? "0" + this.getMinutes() : this.getMinutes();
    var ss = this.getSeconds() < 10 ? "0" + this.getSeconds() : this.getSeconds();
    return "".concat(dd) + "/" + "".concat(mm) + "/" + "".concat(yyyy) + " " + "".concat(hh) + ":" + "".concat(min) + ":" + "".concat(ss);
};

var pageBusqueda = 0; var listaClientes; var listaAbonos; var esbanco = false; var nombresArchivos = ''; var nombresReportes = ''; var ArchivosParciales = false; var ReportesParciales = false; var codigoDelegacion = '';

function cargarValoresIniciales() {

    console.log('Cargar Valores Iniciales');

    $('#dvValores').html('');
    for (i = 0; i < _Divisas.length; i++) {
        var conteudo = "";
        conteudo += "<div id='dv" + _Divisas[i].Codigo + "' style='width: 100%; display: inline-block;'>";
        conteudo += "<input name='chk" + _Divisas[i].Codigo + "' type='checkbox' id='chk" + _Divisas[i].Codigo + "' value='" + _Divisas[i].Identificador + "' onchange='SeleccionarValor(" + '"' + _Divisas[i].Identificador + '"' + ", " + '"' + "chk" + _Divisas[i].Codigo + '"' + ")'>";
        conteudo += _Divisas[i].Descripcion + "</div>";
        $('#dvValores').append(conteudo);
    }
    atribuirFuncion(document.getElementById('dvValores'));

    var selectEstados = document.getElementById("txtEstado");
    selectEstados.innerHTML = "";
    var option = document.createElement("option");
    option.text = _Diccionario.Valores_Seleccionar;
    option.value = '';
    selectEstados.add(option);
    for (i = 0; i < _Estados.length; i++) {
        option = document.createElement("option");
        option.text = _Estados[i].Descripcion;
        option.value = _Estados[i].Codigo;
        selectEstados.add(option);
    }

    var selectTipoAbono = document.getElementById("txtTipoAbono");
    selectTipoAbono.innerHTML = "";
    var option = document.createElement("option");
    option.text = _Diccionario.Valores_Seleccionar;
    option.value = '';
    selectTipoAbono.add(option);
    for (i = 0; i < _TipoAbono.length; i++) {
        option = document.createElement("option");
        option.text = _TipoAbono[i].Descripcion;
        option.value = _TipoAbono[i].Codigo;
        selectTipoAbono.add(option);
    }

    var fechaHasta = new Date();
    var fechaDesde = fechaHasta.addDays(-5);

    document.getElementById('txtFechaDesde').value = fechaDesde.formatAbonoTela();
    document.getElementById('txtFechaHasta').value = fechaHasta.formatAbonoTela();

    _Filtro.FechaAbonoHasta = Date.parseExact(document.getElementById('txtFechaHasta').value, "dd/MM/yyyy HH:mm:ss");
    _Filtro.FechaAbonoDesde = Date.parseExact(document.getElementById('txtFechaDesde').value, "dd/MM/yyyy HH:mm:ss");

    obtenerAbonos();
}

function atribuirFuncion(a) {
    inputs = a.getElementsByTagName('input');
    for (x = 0; x < inputs.length; x++) { if (inputs[x].type == 'checkbox') { inputs[x].onkeydown = function () { HomeEnd(a); }; } }
}

function HomeEnd(a) {

    if (event.which || event.keyCode) {

        if ((event.which == 35) || (event.keyCode == 35)) {
            inputs = a.getElementsByTagName('input');
            for (x = 0; x < inputs.length; x++) {
                if (inputs[x].type == 'checkbox') {
                    inputs[x].focus();
                }
            }
        }
        if ((event.which == 36) || (event.keyCode == 36)) {
            inputs = a.getElementsByTagName('input');
            for (x = inputs.length - 1; x >= 0; x--) {
                if (inputs[x].type == 'checkbox') {
                    inputs[x].focus();
                }
            }
        }
    }
    return false;
}

function visibleNumExterno(a) {
    if (a.selectedIndex != 2) { $('#dvNumeroExterno').css("display", "none"); $('#txtNumeroExterno').val('').change(); _Filtro.NumeroExterno = ''; console.log("Visible Numero Externo: OFF"); } else { $('#dvNumeroExterno').css("display", "block"); console.log("Visible Numero Externo: ON"); }
    _Filtro.CodigoTipo = a.value;
}

function SeleccionarValor(a, b) {

    console.log(a);
    if (document.getElementById(b).checked == true) {
        _Filtro.IdentificadoresDivisas[_Filtro.IdentificadoresDivisas.length] = a;
    } else {
        for (i = 0; i < _Filtro.IdentificadoresDivisas.length; i++) {
            if (_Filtro.IdentificadoresDivisas[i] === a) {
                _Filtro.IdentificadoresDivisas.splice(i, 1);
            }
        }
    }
}

function seleccionarTodos(a, b) {
    inputs = a.getElementsByTagName('input');
    _Filtro.IdentificadoresDivisas.splice(0, _Filtro.IdentificadoresDivisas.length);

        console.log(_Filtro.IdentificadoresDivisas);
    var listaItem = "";
    for (x = 0; x < inputs.length; x++) {
        if (inputs[x].type == 'checkbox') {
            inputs[x].checked = b;

            if (b == true) {
                _Filtro.IdentificadoresDivisas[_Filtro.IdentificadoresDivisas.length] = inputs[x].value;
            }
        }
    }
}

function AtualizarArchivosAbono() {

    inputs = document.getElementById('dvRespuestasAbonos').getElementsByTagName('input');
    nombresArchivos = '';
    nombresReportes = '';

    for (x = 0; x < inputs.length; x++) {
        if (inputs[x].type == 'checkbox') {

            if (inputs[x].checked == true) {
                for (i = 0; i < listaAbonos.length; i++) {
                    if (listaAbonos[i].Identificador == inputs[x].value) {
                        for (r = 0; r < listaAbonos[i].DatosReporte.length; r++) {
                            if (listaAbonos[i].DatosReporte[r].CodigoSituacion == 'PR') {
                                if (listaAbonos[i].DatosReporte[r].Tipo == 3) {
                                    nombresArchivos += listaAbonos[i].DatosReporte[r].NombreArchivo + ';';
                                } else if (listaAbonos[i].DatosReporte[r].Tipo == 2) {
                                    nombresReportes += listaAbonos[i].DatosReporte[r].NombreArchivo + ';';
                                }
                            } else {
                                if (listaAbonos[i].DatosReporte[r].Tipo == 3) {
                                    ArchivosParciales = true;
                                } else if (listaAbonos[i].DatosReporte[r].Tipo == 2) {
                                    ReportesParciales = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        console.log(nombresArchivos + ' - ' + nombresReportes);
    }

    if (nombresArchivos != '') { document.getElementById('btnVisualizarArchivo').disabled = ''; } else { document.getElementById('btnVisualizarArchivo').disabled = 'disabled'; }
    if (nombresReportes != '') { document.getElementById('btnVisualizarReporte').disabled = ''; } else { document.getElementById('btnVisualizarReporte').disabled = 'disabled'; }

}

function seleccionarTodosAbonos() {

    var b = document.getElementById('chkSeleccionarTodosAbonos').checked;
    inputs = document.getElementById('dvRespuestasAbonos').getElementsByTagName('input');
    nombresArchivos = '';
    nombresReportes = '';

    for (x = 0; x < inputs.length; x++) {
        if (inputs[x].type == 'checkbox') {
            inputs[x].checked = b;
        }
    }
    AtualizarArchivosAbono();
}

function cargaValorFiltro(a) {

    console.log('Cargando filtro: ' + a.id + ' - ' + a.value);
    if (a.id == 'txtNumeroExterno') {
        _Filtro.NumeroExterno = a.value;
    } else if (a.id == 'txtCodigo') {
        _Filtro.Codigo = a.value;
    } else if (a.id == 'txtEstado') {
        _Filtro.CodigoEstado = a.value;
    } else if (a.id == 'txtFechaHasta') {
        _Filtro.FechaAbonoHasta = Date.parseExact(document.getElementById('txtFechaHasta').value, "dd/MM/yyyy HH:mm:ss");
        if (!_Filtro.FechaAbonoHasta) {
            _Filtro.FechaAbonoHasta = Date.MinValue;
        }
    } else if (a.id == 'txtFechaDesde') {
        _Filtro.FechaAbonoDesde = Date.parseExact(document.getElementById('txtFechaDesde').value, "dd/MM/yyyy HH:mm:ss");
        if (!_Filtro.FechaAbonoDesde) {
            _Filtro.FechaAbonoDesde = Date.MinValue;
        }
    }
}

function obtenerAbonos() {
    console.log("obtenerAbonos");
    genesisAlertLoading(_Diccionario.msg_loading + _Diccionario.msg_obtenerValores);

    if ($("input[id^='" + "txtBusquedaSeleccionados_Banco']").val() != '') {
        var _banco = JSON.parse($("input[id^='" + "txtBusquedaSeleccionados_Banco']").val());
        _Filtro.IdentificadorBanco = _banco[0].Identificador;
    } else {
        _Filtro.IdentificadorBanco = '';
    }
    
    if ($("input[id^='" + "txtBusquedaSeleccionados_Cliente']").val() != '') {
        var _cliente = JSON.parse($("input[id^='" + "txtBusquedaSeleccionados_Cliente']").val());
        _Filtro.IdentificadorCliente = _cliente[0].Identificador;
    } else {
        _Filtro.IdentificadorCliente = '';
    }
    
    _Filtro.TipoAbono = document.getElementById('txtTipoAbono').selected;

    jQuery.ajax({
        url: 'Busqueda.aspx/obtenerAbonos',
        type: "POST",
        dataType: "json",
        data: "{filtro: '" + JSON.stringify(_Filtro) + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data, text) {
            var json_x = JSON.parse(data.d);
            if (json_x.CodigoError == "0") {
                if (json_x.Respuesta != null) {
                    listaAbonos = json_x.Respuesta;
                    cargarAbono();
                } else {
                    document.getElementById('dvRespuestasAbonos').innerHTML = "<span style='padding:5px; color: #999999;font-style: italic;'>" + _Diccionario.msg_nenhumRegistroEncontrado + "</span>";
                }
                genesisAlertLoading("");
            } else {
                genesisAlertError(json_x.MensajeError, json_x.MensajeErrorDescriptiva);
            }
        },
        error: function (request, status, error) {
            genesisAlertError(_Diccionario.msg_producidoError + error, request.responseText);
        }
    });
}

function cargarAbono() {

    if (listaAbonos != null) {
        console.log("cargarAbonos: (cuantidad='" + listaAbonos.length + "')");
        
        var conteudo = '<table><thead><tr>';
        conteudo += '<th style="width: 2%;"><input type="checkbox" id="chkSeleccionarTodosAbonos" title="" onchange="seleccionarTodosAbonos()" value="1" /></th>';
        conteudo += '<th style="width: 10%;">' + _Diccionario.Grid_TituloColumna_FechaHora + '</th>';
        conteudo += '<th style="width: 27%;">' + _Diccionario.Grid_TituloColumna_Banco + '</th>';
        conteudo += '<th style="width: 27%;">' + _Diccionario.Grid_TituloColumna_Codigo + '</th>';
        conteudo += '<th style="width: 10%;">' + _Diccionario.Grid_TituloColumna_TipoAbono + '</th>';
        conteudo += '<th style="width: 10%;">' + _Diccionario.Grid_TituloColumna_Estado + '</th>';
        conteudo += '<th style="width: 6%;">' + _Diccionario.Grid_TituloColumna_Situacion + '</th>';
        conteudo += '<th style="width: 10%;">' + _Diccionario.Grid_TituloColumna_Aciones + '</th></tr></thead><tbody>';

        for (i = 0; i < listaAbonos.length; i++) {
            var cor = 'FFFFFF';
            if (i % 2 == 0) { cor = 'FFFDF2'; }

            var cor = 'gray';
            var error = '';
            var title = '';

            if (listaAbonos[i].CodigoEstado == 'PR' && error == '') { cor = 'green'; error = ''; }

            if (listaAbonos[i].DatosReporte != null && listaAbonos[i].DatosReporte.length > 0) {
                for (r = 0; r < listaAbonos[i].DatosReporte.length; r++) {
                    if (listaAbonos[i].DatosReporte[r].CodigoSituacion == 'ER') { cor = 'red'; title = 'Situación'; error += listaAbonos[i].DatosReporte[r].NombreArchivo + ': ' + listaAbonos[i].DatosReporte[r].DesErrorEjecucion + '; '; break; }
                    if (listaAbonos[i].DatosReporte[r].CodigoSituacion == 'EC' || listaAbonos[i].DatosReporte[r].CodigoSituacion == 'PE' && cor != 'red') { cor = 'yellow'; error = ''; }
                }
            }else {
                cor = 'gray';
            }
                         
            var fecha = new Date(listaAbonos[i].Fecha);
            conteudo += '<tr style="background-color: #' + cor + ';"><td><table style="text-align:center; border-collapse: collapse; border: none; padding: 0px; margin: 0px;"><tr style="border: none; padding: 0px; margin: 0px;"><td style="width:13px;border: none !important; padding: 0px; margin: 0px;">';
            if (listaAbonos[i].CodigoEstado == 'PR' && (listaAbonos[i].DatosReporte != null && listaAbonos[i].DatosReporte.length > 0)) {
                conteudo += '<input type="checkbox" id="chkAbono' + i + '" title="" onchange="AtualizarArchivosAbono()" value="' + listaAbonos[i].Identificador + '" style="vertical-align:baseline !important;" />';
            }
            
            var _banco = '';
            var _codigo = '';

            if (listaAbonos[i].Codigo != null) { _codigo = listaAbonos[i].Codigo; }

            if (listaAbonos[i].Bancos != null && listaAbonos[i].Bancos.length > 0) {
                if (listaAbonos[i].Bancos[0].Codigo != null) { _banco = listaAbonos[i].Bancos[0].Codigo; }
                if (listaAbonos[i].Bancos[0].Descripcion != null) { _banco += ' - ' + listaAbonos[i].Bancos[0].Descripcion; }
            }

            conteudo += '</td></tr></table></td>'; 
            conteudo += '<td style="text-align:center;">' + listaAbonos[i].FechaFormatada + '</td>';
            conteudo += '<td style="padding-left:5px;">' + _banco + '</td>';
            conteudo += '<td style="padding-left:5px;">' + _codigo + '</td>';
            conteudo += '<td style="text-align:center;">' + _Diccionario['Valores_TipoAbono_' + listaAbonos[i].TipoAbono] + '</td>';
            conteudo += '<td style="text-align:center;">' + _Diccionario['Valores_Estado_' + listaAbonos[i].CodigoEstado] + '</td>';
            conteudo += '<td style="text-align:center; width:13px;padding: 0px; margin: 0px;"><div><a href="#" id="lnkSituacion' + i + '" title="'+ title + '" + data-content="' + error + '" class="situacion"><img src="../../App_Themes/Padrao/css/images/ico_' + cor + '.png" name="ImgEstadoAbono_' + i + '" style="vertical-align:baseline !important;"/></a> </div></td>';
            conteudo += '<td><center>';

            conteudo += '<table style="width: auto; border-collapse: collapse; border: none; padding: 0px; margin: 0px;"><tr style="border: none; padding: 0px; margin: 0px;">';
            if (listaAbonos[i].CodigoEstado == 'EN') {
                conteudo += '<td style="width:30px;border: none !important; padding: 0px; margin: 0px;"><input type="button" name="ImgEditar_' + i + '" id="ImgEditar_' + i + '" class="butondefectoPequeno" style="width: 17px; height: 17px; border-width: 0px; background-image:url(' + "'" + '../../Imagenes/ico_edit_item.png' + "'" + ') !important; cursor: pointer;" onclick="javascript: accionEditar(' + "'" + i + "'" + ');"></td>';
                conteudo += '<td style="width:30px;border: none !important; padding: 0px; margin: 0px;"><input type="button" name="ImgEliminar_' + i + '" id="ImgEliminar_' + i + '" class="butondefectoPequeno" style="width: 17px; height: 17px; border-width: 0px; background-image:url(' + "'" + '../../Imagenes/ico_invalid.png' + "'" + ') !important; cursor: pointer;" onclick="javascript: accionEliminar(' + "'" + i + "'" + ');"></td>';
            } else {
                conteudo += '<td colspan=2 style="width:30px;border: none !important; padding: 0px; margin: 0px;"><input type="button" name="ImgVisualizar_' + i + '" id="ImgVisualizar_' + i + '" class="butondefectoPequeno" style="width: 17px; height: 17px; border-width: 0px; background-image:url(' + "'" + '../../Imagenes/ico_search.png' + "'" + ') !important; cursor: pointer;" onclick="javascript: accionVisualizar(' + "'" + i + "'" + ');"></td>';
                if (listaAbonos[i].CodigoEstado == 'PR') {
                    conteudo += '<td colspan=2 style="width:30px;border: none !important; padding: 0px; margin: 0px;"><input type="button" name="ImgImprimir_' + i + '" id="ImgImprimir_' + i + '" class="butondefectoPequeno" style="width: 17px; height: 17px; border-width: 0px; background-image:url(' + "'" + '../../Imagenes/ico_process.png' + "'" + ') !important; cursor: pointer;" onclick="javascript: accionImprimir(' + "'" + i + "'" + ');"></td>';
                }
            }

            if (listaAbonos[i].TipoAbono == 'SA' && listaAbonos[i].TipoAbono == 'PE' && listaAbonos[i].IdentificadorGrupoDocumento != '')
            {
                conteudo += '<td colspan=2 style="width:30px;border: none !important; padding: 0px; margin: 0px;"><input type="button" name="ImgImprimir_' + i + '" id="ImgImprimir_' + i + '" class="butondefectoPequeno" style="width: 17px; height: 17px; border-width: 0px; background-image:url(' + "'" + '../../Imagenes/ico_document.png' + "'" + ') !important; cursor: pointer;" onclick="javascript: accionVisualizarDocumento(' + "'" + i + "'" + ');"></td>'
            }

            conteudo += '</tr></table></center></td></tr>';
        }

        conteudo += '</tbody></table>';

        document.getElementById('dvRespuestasAbonos').innerHTML = conteudo;

        $(".situacion").popover({ trigger: 'click', placement: 'bottom' });


    }
}

function accionVisualizarArchivo() {
    console.log("Visualizar Archivo");
    var delegacion = '';

    if (codigoDelegacion != '') {
        delegacion = codigoDelegacion;
    } else {
        delegacion = _Filtro.codigoDelegacion;
    }

    if (nombresArchivos.length > 0 && delegacion != '') {
        if (ArchivosParciales) {
            if (confirm(_Diccionario.msg_Archivos_Parciales)) {
                window.location.href = 'ObtenerReportes.ashx?nombresArchivos=' + nombresArchivos + '&esReportes=False&codigoDelegacion=' + delegacion;
            }
        } else {
            window.location.href = 'ObtenerReportes.ashx?nombresArchivos=' + nombresArchivos + '&esReportes=False&codigoDelegacion=' + delegacion;
        }
    } else {
        alert(_Diccionario.msg_informacionesInvalidas)
    }
}

function accionVisualizarReporte() {
    console.log("Visualizar Reporte");
    var delegacion = '';

    if (codigoDelegacion != '') {
        delegacion = codigoDelegacion;
    } else {
        delegacion = _Filtro.codigoDelegacion;
    }

    if (nombresReportes.length > 0 && delegacion != '') {
        if (ArchivosParciales) {
            if (confirm(_Diccionario.msg_Reportes_Parciales)) {
                window.location.href = 'ObtenerReportes.ashx?nombresArchivos=' + nombresReportes + '&esReportes=True&codigoDelegacion=' + delegacion;
            }
        } else {
            window.location.href = 'ObtenerReportes.ashx?nombresArchivos=' + nombresReportes + '&esReportes=True&codigoDelegacion=' + delegacion;
        }
    } else {
        alert(_Diccionario.msg_informacionesInvalidas)
    }
}

function accionNuevoAbono() {
    console.log("Nuevo Abono");
    window.location.href = 'PantallaAbono.aspx?Modo=Alta';
}

function accionEditar(i) {
    console.log("Editar identificador: " + i);
    window.location.href = 'PantallaAbono.aspx?IdentificadorAbono=' + listaAbonos[i].Identificador + '&Modo=Modificacion';
}

function accionVisualizar(i) {
    console.log("Visualizar identificador: " + i);
    window.location.href = 'PantallaVisualizar.aspx?IdentificadorAbono=' + listaAbonos[i].Identificador + '&Modo=Consulta';
}

function accionVisualizarDocumento(i) {
    console.log("Visualizar Documeto: " + i);
    window.location.href = '../GrupoDocumento.aspx?GrupoDocumentos=' + listaAbonos[i].IdentificadorGrupoDocumento + '&Modo=Consulta';
}

function accionImprimir(i) {
    console.log("Imprimir identificador: " + i);
    genesisAlertLoading(_Diccionario.msg_loading + _Diccionario.msg_generandoReportes);

    jQuery.ajax({
        url: 'Busqueda.aspx/generarReportesAbono',
        type: "POST",
        dataType: "json",
        data: "{identificador: '" + listaAbonos[i].Identificador + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data, text) {
            var json_x = JSON.parse(data.d);
            if (json_x.CodigoError == "0") {
                if (json_x.Respuesta != null) {
                    window.location.href = 'PantallaVisualizar.aspx?IdentificadorAbono=' + json_x.Respuesta + '&Modo=AltaImpresion';
                }
            } else {
                genesisAlertError(json_x.MensajeError, json_x.MensajeErrorDescriptiva);
            }
        },
        error: function (request, status, error) {
            genesisAlertError(_Diccionario.msg_producidoError + error, request.responseText);
        }
    });
}

function accionEliminar(i) {
    console.log("Eliminar indice: " + i);

    var fecha = new Date(listaAbonos[i].Fecha);
    var _msg = _Diccionario.msg_Anular_Abono.split("|");
    console.log(listaAbonos[i]);
    if (confirm(_msg[0] + '"' + listaAbonos[i].Bancos[0].Descripcion + '"' + _msg[1] + '"' + _Diccionario['Valores_TipoAbono_' + listaAbonos[i].TipoAbono] + '"' + _msg[2] + '"' + fecha.formatAbonoTela() + '"' + _msg[3])) {

        genesisAlertLoading(_Diccionario.msg_loading + _Diccionario.msg_anulandoAbono);

        jQuery.ajax({
            url: 'Busqueda.aspx/anularAbono',
            type: "POST",
            dataType: "json",
            data: "{identificador: '" + listaAbonos[i].Identificador + "', filtro: '" + JSON.stringify(_Filtro) + "'}",
            contentType: "application/json; charset=utf-8",
            success: function (data, text) {
                var json_x = JSON.parse(data.d);
                if (json_x.CodigoError == "0") {
                    if (json_x.Respuesta != null) {
                        listaAbonos = json_x.Respuesta;
                        cargarAbono();
                    } else {
                        document.getElementById('dvRespuestasAbonos').innerHTML = "<span style='padding:5px; color: #999999;font-style: italic;'>" + _Diccionario.msg_nenhumRegistroEncontrado + "</span>";
                    }
                    genesisAlertLoading("");
                } else {
                    genesisAlertError(json_x.MensajeError, json_x.MensajeErrorDescriptiva);
                }
            },
            error: function (request, status, error) {
                genesisAlertError(_Diccionario.msg_producidoError + error, request.responseText);
            }
        });

        obtenerAbonos();
    }
}

function llamadaAjax(url, type, parametrosllamada, success, error) {
    jQuery.ajax({
        url: url,
        contentType: "application/json; charset=utf-8",
        type: type,
        dataType: "json",
        data: parametrosllamada,
        success: success,
        error: error
    });
}