Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports System.Threading.Tasks
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports System.Text
Imports Prosegur.Framework.Dicionario

Namespace GenesisSaldos

    ''' <summary>
    ''' Classe de Formulários.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 04/09/2013 - Criado
    ''' </history>
    Public Class Formulario

#Region " Procedure - Recuperar"

        Public Shared Function recuperarFormularioAbono(identificadorSector As String,
                                                        usuario As String,
                                                        ByRef transaccion As DataBaseHelper.Transaccion) As Clases.Formulario

            Dim _Formularios As New List(Of Clases.Formulario)

            Try

                If Not String.IsNullOrEmpty(identificadorSector) Then
                    Dim ds As DataSet = Nothing

                    Dim spw As SPWrapper = ColectarFormularioAbono(identificadorSector, usuario)
                    ds = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False, transaccion)

                    Dim _gruposIAC As List(Of Clases.GrupoTerminosIAC) = AccesoDatos.GenesisSaldos.Documento.CargarGrupoTerminosIAC(ds)
                    Dim _acciones As List(Of Clases.AccionContable) = AccesoDatos.GenesisSaldos.Documento.CargarAccionContable(ds)
                    _Formularios = AccesoDatos.GenesisSaldos.Documento.CargarFormulario(ds, _acciones, _gruposIAC)

                End If

            Catch ex As Exception

                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If

            End Try

            Return _Formularios.FirstOrDefault
        End Function

        Public Shared Function ColectarFormularioAbono(identificadorSector As String,
                                                        usuario As String) As SPWrapper

            Dim SP As String = String.Format("sapr_pformulario_{0}.srecuperar_form_pases_abono", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_sector", ParamTypes.String, identificadorSector, ParameterDirection.Input, False)
            spw.AgregarParam("par$doc_rc_formulario", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_formulario")
            spw.AgregarParam("par$doc_rc_accion_contable", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_accion_contable")
            spw.AgregarParam("par$doc_rc_estado_acc_contable", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_estado_acc_contable")
            spw.AgregarParam("par$doc_rc_caract_formulario", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_caract_formulario")
            spw.AgregarParam("par$doc_rc_grp_terminos_indiv", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_grp_terminos_indiv")
            spw.AgregarParam("par$doc_rc_terminos_indiv", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_terminos_indiv")
            spw.AgregarParam("par$usuario", ParamTypes.String, usuario, ParameterDirection.Input, False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ParamTypes.Long, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            Return spw
        End Function

#End Region









#Region "[NUEVA CONSULTA]"

        Private Shared Function ObtenerFormularios_v2(codigosFormularios As List(Of String),
                                                      identificadoresFormularios As List(Of String)) As ObservableCollection(Of Clases.Formulario)

            Dim tdFormularios As DataTable = Nothing
            Dim tdAccionContable As DataTable = Nothing
            Dim tdCaracteristicas As DataTable = Nothing
            Dim tdIAC As DataTable = Nothing
            Dim tdTerminos As DataTable = Nothing

            Dim TFormulario As New Task(Sub()
                                            tdFormularios = HacerConsultaConFormularios(My.Resources.ObtenerFormularios_v2.ToString(), codigosFormularios, identificadoresFormularios)
                                        End Sub)
            TFormulario.Start()

            Dim TAccionContable As New Task(Sub()
                                                tdAccionContable = HacerConsultaConFormularios(My.Resources.ObtenerAccionContable_v2.ToString(), codigosFormularios, identificadoresFormularios)
                                            End Sub)
            TAccionContable.Start()

            Dim TCaracteristica As New Task(Sub()
                                                tdCaracteristicas = HacerConsultaConFormularios(My.Resources.ObtenerCaracteristicas_v2.ToString(), codigosFormularios, identificadoresFormularios)
                                            End Sub)
            TCaracteristica.Start()

            Dim TIAC As New Task(Sub()
                                     tdIAC = HacerConsultaConFormularios(My.Resources.ObtenerIAC_v2.ToString(), codigosFormularios, identificadoresFormularios)
                                 End Sub)
            TIAC.Start()

            Dim TTermino As New Task(Sub()
                                         tdTerminos = HacerConsultaConFormularios(My.Resources.ObtenerTerminos_v2.ToString(), codigosFormularios, identificadoresFormularios)
                                     End Sub)
            TTermino.Start()

            ' Aguarda que as tasks terminem antes de continuar
            Task.WaitAll(New Task() {TFormulario, TAccionContable, TCaracteristica, TIAC, TTermino})

            Return cargarFormularios(tdFormularios, tdAccionContable, tdCaracteristicas, tdIAC, tdTerminos)

        End Function

        Public Shared Function ObtenerFormulariosPorCodigos_v2(codigosFormularios As List(Of String)) As ObservableCollection(Of Clases.Formulario)
            Return ObtenerFormularios_v2(codigosFormularios, Nothing)
        End Function

        Public Shared Function ObtenerFormulariosPorIdentificadores_v2(IdentificadoresFormularios As List(Of String)) As ObservableCollection(Of Clases.Formulario)
            Return ObtenerFormularios_v2(Nothing, IdentificadoresFormularios)
        End Function

        Public Shared Function cargarFormularios(tdFormularios As DataTable,
                                                 tdAccionContable As DataTable,
                                                 tdCaracteristicas As DataTable,
                                                 tdIAC As DataTable,
                                                 tdTerminos As DataTable) As ObservableCollection(Of Clases.Formulario)

            Dim formularios As ObservableCollection(Of Clases.Formulario) = Nothing


            If tdFormularios IsNot Nothing AndAlso tdFormularios.Rows.Count > 0 Then

                formularios = New ObservableCollection(Of Clases.Formulario)

                For Each rowFormulario In tdFormularios.Rows

                    Dim formulario As New Clases.Formulario

                    With formulario

                        .AccionContable = New Clases.AccionContable
                        .TipoDocumento = New Clases.TipoDocumento
                        .Identificador = If(rowFormulario.Table.Columns.Contains("OID_FORMULARIO"), Util.AtribuirValorObj(rowFormulario("OID_FORMULARIO"), GetType(String)), Nothing)
                        .Codigo = If(rowFormulario.Table.Columns.Contains("COD_FORMULARIO"), Util.AtribuirValorObj(rowFormulario("COD_FORMULARIO"), GetType(String)), Nothing)
                        .Descripcion = If(rowFormulario.Table.Columns.Contains("DES_FORMULARIO"), Util.AtribuirValorObj(rowFormulario("DES_FORMULARIO"), GetType(String)), Nothing)
                        .Icono = If(rowFormulario.Table.Columns.Contains("BIN_ICONO_FORMULARIO"), Util.AtribuirValorObj(rowFormulario("BIN_ICONO_FORMULARIO"), GetType(Byte())), Nothing)
                        .EstaActivo = If(rowFormulario.Table.Columns.Contains("BOL_ACTIVO"), Util.AtribuirValorObj(rowFormulario("BOL_ACTIVO"), GetType(String)), Nothing)

                        If rowFormulario.Table.Columns.Contains("COD_COLOR") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowFormulario("COD_COLOR"), GetType(String))) Then
                            .Color = System.Drawing.Color.FromName(rowFormulario("COD_COLOR").ToString)
                        End If

                        With .TipoDocumento
                            .Identificador = If(rowFormulario.Table.Columns.Contains("OID_TIPO_DOCUMENTO"), Util.AtribuirValorObj(rowFormulario("OID_TIPO_DOCUMENTO"), GetType(String)), Nothing)
                            .Codigo = If(rowFormulario.Table.Columns.Contains("COD_TIPO_DOCUMENTO"), Util.AtribuirValorObj(rowFormulario("COD_TIPO_DOCUMENTO"), GetType(String)), Nothing)
                            .Descripcion = If(rowFormulario.Table.Columns.Contains("DES_TIPO_DOCUMENTO"), Util.AtribuirValorObj(rowFormulario("DES_TIPO_DOCUMENTO"), GetType(String)), Nothing)
                            .EstaActivo = If(rowFormulario.Table.Columns.Contains("TD_BOL_ACTIVO"), Util.AtribuirValorObj(rowFormulario("TD_BOL_ACTIVO"), GetType(Boolean)), Nothing)
                        End With

                        With .AccionContable
                            .Identificador = If(rowFormulario.Table.Columns.Contains("OID_ACCION_CONTABLE"), Util.AtribuirValorObj(rowFormulario("OID_ACCION_CONTABLE"), GetType(String)), Nothing)
                            .Codigo = If(rowFormulario.Table.Columns.Contains("COD_ACCION_CONTABLE"), Util.AtribuirValorObj(rowFormulario("COD_ACCION_CONTABLE"), GetType(String)), Nothing)
                            .Descripcion = If(rowFormulario.Table.Columns.Contains("DES_ACCION_CONTABLE"), Util.AtribuirValorObj(rowFormulario("DES_ACCION_CONTABLE"), GetType(String)), Nothing)
                            .EstaActivo = If(rowFormulario.Table.Columns.Contains("AC_BOL_ACTIVO"), Util.AtribuirValorObj(rowFormulario("AC_BOL_ACTIVO"), GetType(String)), Nothing)

                            If tdAccionContable IsNot Nothing AndAlso tdAccionContable.Rows.Count > 0 AndAlso _
                                rowFormulario.Table.Columns.Contains("OID_ACCION_CONTABLE") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowFormulario("OID_ACCION_CONTABLE"), GetType(String))) Then

                                Dim acciones = tdAccionContable.Select("OID_ACCION_CONTABLE = '" & Util.AtribuirValorObj(rowFormulario("OID_ACCION_CONTABLE"), GetType(String)) & "'")

                                If acciones IsNot Nothing Then
                                    .Acciones = New ObservableCollection(Of Clases.AccionTransaccion)
                                    For Each rowAccion In acciones

                                        Dim EstadoAccionContable As New Clases.EstadoAccionContable
                                        With EstadoAccionContable
                                            .Identificador = If(rowAccion.Table.Columns.Contains("OID_ESTADOXACCION_CONTABLE"), Util.AtribuirValorObj(rowAccion("OID_ESTADOXACCION_CONTABLE"), GetType(String)), Nothing)
                                            .IdentificadorAccionContable = If(rowAccion.Table.Columns.Contains("OID_ACCION_CONTABLE"), Util.AtribuirValorObj(rowAccion("OID_ACCION_CONTABLE"), GetType(String)), Nothing)
                                            .Codigo = If(rowAccion.Table.Columns.Contains("COD_ESTADO"), Util.AtribuirValorObj(rowAccion("COD_ESTADO"), GetType(String)), Nothing)
                                            .OrigemDisponible = If(rowAccion.Table.Columns.Contains("COD_ACCION_ORIGEN_DISPONIBLE"), Util.AtribuirValorObj(rowAccion("COD_ACCION_ORIGEN_DISPONIBLE"), GetType(String)), Nothing)
                                            .OrigemNoDisponible = If(rowAccion.Table.Columns.Contains("COD_ACCION_ORIGEN_NODISP"), Util.AtribuirValorObj(rowAccion("COD_ACCION_ORIGEN_NODISP"), GetType(String)), Nothing)
                                            .DestinoDisponible = If(rowAccion.Table.Columns.Contains("COD_ACCION_DESTINO_DISPONIBLE"), Util.AtribuirValorObj(rowAccion("COD_ACCION_DESTINO_DISPONIBLE"), GetType(String)), Nothing)
                                            .DestinoNoDisponible = If(rowAccion.Table.Columns.Contains("COD_ACCION_DESTINO_NODISP"), Util.AtribuirValorObj(rowAccion("COD_ACCION_DESTINO_NODISP"), GetType(String)), Nothing)
                                        End With

                                        AccionContable.ObtenerAccionesTransacciones_v2(EstadoAccionContable, .Acciones)

                                    Next
                                End If
                            End If

                        End With

                        If tdCaracteristicas IsNot Nothing AndAlso tdCaracteristicas.Rows.Count > 0 AndAlso _
                            rowFormulario.Table.Columns.Contains("OID_FORMULARIO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowFormulario("OID_FORMULARIO"), GetType(String))) Then

                            Dim codigoCaracterisiticas As List(Of String) = tdCaracteristicas.AsEnumerable() _
                                                                            .Where(Function(r) r.Field(Of String)("OID_FORMULARIO") = Util.AtribuirValorObj(rowFormulario("OID_FORMULARIO"), GetType(String))) _
                                                                            .Select(Function(r) r.Field(Of String)("COD_CARACT_FORMULARIO")) _
                                                                            .Distinct() _
                                                                            .ToList()
                            If codigoCaracterisiticas IsNot Nothing Then
                                .Caracteristicas = New List(Of Enumeradores.CaracteristicaFormulario)
                                For Each _caracteristica In codigoCaracterisiticas
                                    If ExisteEnum(Of Enumeradores.CaracteristicaFormulario)(_caracteristica) Then
                                        .Caracteristicas.Add(RecuperarEnum(Of Enumeradores.CaracteristicaFormulario)(_caracteristica))
                                    End If
                                Next
                            End If
                        End If

                        If tdIAC IsNot Nothing AndAlso tdIAC.Rows.Count > 0 Then

                            'GrupoTerminosIAC individual
                            If rowFormulario.Table.Columns.Contains("OID_IAC_INDIVIDUAL") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowFormulario("OID_IAC_INDIVIDUAL"), GetType(String))) Then

                                Dim _iacs = tdIAC.Select("OID_IAC = '" & Util.AtribuirValorObj(rowFormulario("OID_IAC_INDIVIDUAL"), GetType(String)) & "'")

                                If _iacs IsNot Nothing Then

                                    .GrupoTerminosIACIndividual = New Clases.GrupoTerminosIAC

                                    With .GrupoTerminosIACIndividual

                                        .Identificador = If(_iacs(0).Table.Columns.Contains("OID_IAC"), Util.AtribuirValorObj(_iacs(0)("OID_IAC"), GetType(String)), Nothing)
                                        .Codigo = If(_iacs(0).Table.Columns.Contains("COD_IAC"), Util.AtribuirValorObj(_iacs(0)("COD_IAC"), GetType(String)), Nothing)
                                        .Descripcion = If(_iacs(0).Table.Columns.Contains("DES_IAC"), Util.AtribuirValorObj(_iacs(0)("DES_IAC"), GetType(String)), Nothing)
                                        .Observacion = If(_iacs(0).Table.Columns.Contains("OBS_IAC"), Util.AtribuirValorObj(_iacs(0)("OBS_IAC"), GetType(String)), Nothing)
                                        .EstaActivo = If(_iacs(0).Table.Columns.Contains("BOL_VIGENTE"), Util.AtribuirValorObj(_iacs(0)("BOL_VIGENTE"), GetType(Boolean)), Nothing)
                                        .EsInvisible = If(_iacs(0).Table.Columns.Contains("BOL_INVISIBLE"), Util.AtribuirValorObj(_iacs(0)("BOL_INVISIBLE"), GetType(Boolean)), Nothing)
                                        .CopiarDeclarados = If(_iacs(0).Table.Columns.Contains("BOL_COPIA_DECLARADOS"), Util.AtribuirValorObj(_iacs(0)("BOL_COPIA_DECLARADOS"), GetType(Boolean)), Nothing)
                                        .TerminosIAC = New ObservableCollection(Of Clases.TerminoIAC)

                                        If tdTerminos IsNot Nothing AndAlso tdTerminos.Rows.Count > 0 AndAlso _
                                            _iacs(0).Table.Columns.Contains("OID_IAC") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_iacs(0)("OID_IAC"), GetType(String))) Then

                                            Dim _Terminos = tdTerminos.Select("OID_IAC = '" & Util.AtribuirValorObj(_iacs(0)("OID_IAC"), GetType(String)) & "'")

                                            If _Terminos IsNot Nothing Then


                                                Dim valores As List(Of Tuple(Of String, ObservableCollection(Of Clases.TerminoValorPosible))) = Nothing
                                                Dim identificadoresTerminos = (From r As DataRow In tdTerminos.Rows
                                                                               Where Util.AtribuirValorObj(r.Item("BOL_VALORES_POSIBLES"), GetType(Boolean)) = True
                                                                               Select r.Item("OID_TERMINO")).ToList

                                                If identificadoresTerminos IsNot Nothing AndAlso identificadoresTerminos.Count > 0 Then

                                                    valores = AccesoDatos.Genesis.ValorTerminoIAC.RecuperarValoresTerminosIAC(identificadoresTerminos)

                                                End If

                                                For Each termino In _Terminos

                                                    Dim terminoIAC As New Clases.TerminoIAC
                                                    terminoIAC.Formato = New Clases.Formato

                                                    With terminoIAC
                                                        .Identificador = If(termino.Table.Columns.Contains("OID_TERMINO"), Util.AtribuirValorObj(termino("OID_TERMINO"), GetType(String)), Nothing)
                                                        .BuscarParcial = If(termino.Table.Columns.Contains("BOL_BUSQUEDA_PARCIAL"), Util.AtribuirValorObj(termino("BOL_BUSQUEDA_PARCIAL"), GetType(Boolean)), Nothing)
                                                        .EsCampoClave = If(termino.Table.Columns.Contains("BOL_CAMPO_CLAVE"), Util.AtribuirValorObj(termino("BOL_CAMPO_CLAVE"), GetType(Boolean)), Nothing)
                                                        .Orden = If(termino.Table.Columns.Contains("NEC_ORDEN"), Util.AtribuirValorObj(termino("NEC_ORDEN"), GetType(Integer)), Nothing)
                                                        .EsObligatorio = If(termino.Table.Columns.Contains("BOL_ES_OBLIGATORIO"), Util.AtribuirValorObj(termino("BOL_ES_OBLIGATORIO"), GetType(Boolean)), Nothing)
                                                        .EsTerminoCopia = If(termino.Table.Columns.Contains("BOL_TERMINO_COPIA"), Util.AtribuirValorObj(termino("BOL_TERMINO_COPIA"), GetType(Boolean)), Nothing)
                                                        .EsProtegido = If(termino.Table.Columns.Contains("BOL_ES_PROTEGIDO"), Util.AtribuirValorObj(termino("BOL_ES_PROTEGIDO"), GetType(Boolean)), Nothing)
                                                        .Codigo = If(termino.Table.Columns.Contains("COD_TERMINO"), Util.AtribuirValorObj(termino("COD_TERMINO"), GetType(String)), Nothing)
                                                        .Observacion = If(termino.Table.Columns.Contains("OBS_TERMINO"), Util.AtribuirValorObj(termino("OBS_TERMINO"), GetType(String)), Nothing)
                                                        .Longitud = If(termino.Table.Columns.Contains("NEC_LONGITUD"), Util.AtribuirValorObj(termino("NEC_LONGITUD"), GetType(Integer)), Nothing)
                                                        .MostrarDescripcionConCodigo = If(termino.Table.Columns.Contains("BOL_MOSTRAR_CODIGO"), Util.AtribuirValorObj(termino("BOL_MOSTRAR_CODIGO"), GetType(Boolean)), Nothing)
                                                        .TieneValoresPosibles = If(termino.Table.Columns.Contains("BOL_VALORES_POSIBLES"), Util.AtribuirValorObj(termino("BOL_VALORES_POSIBLES"), GetType(Boolean)), Nothing)
                                                        .AceptarDigitacion = If(termino.Table.Columns.Contains("BOL_ACEPTAR_DIGITACION"), Util.AtribuirValorObj(termino("BOL_ACEPTAR_DIGITACION"), GetType(Boolean)), Nothing)
                                                        .EstaActivo = If(termino.Table.Columns.Contains("BOL_VIGENTE"), Util.AtribuirValorObj(termino("BOL_VIGENTE"), GetType(Boolean)), Nothing)
                                                        .EsEspecificoDeSaldos = If(termino.Table.Columns.Contains("BOL_ESPECIFICO_DE_SALDOS"), Util.AtribuirValorObj(termino("BOL_ESPECIFICO_DE_SALDOS"), GetType(Boolean)), Nothing)
                                                        .Descripcion = If(termino.Table.Columns.Contains("DES_TERMINO"), Util.AtribuirValorObj(termino("DES_TERMINO"), GetType(String)), Nothing)

                                                        With terminoIAC.Formato
                                                            .Identificador = If(termino.Table.Columns.Contains("OID_FORMATO"), Util.AtribuirValorObj(termino("OID_FORMATO"), GetType(String)), Nothing)
                                                            .Codigo = If(termino.Table.Columns.Contains("COD_FORMATO"), Util.AtribuirValorObj(termino("COD_FORMATO"), GetType(String)), Nothing)
                                                            .Descripcion = If(termino.Table.Columns.Contains("DES_FORMATO"), Util.AtribuirValorObj(termino("DES_FORMATO"), GetType(String)), Nothing)
                                                        End With

                                                        'Verifica se possui algoritimo de validação.
                                                        If termino.Table.Columns.Contains("OID_ALGORITMO_VALIDACION") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(termino("OID_ALGORITMO_VALIDACION"), GetType(String))) Then
                                                            .AlgoritmoValidacion = New Clases.AlgoritmoValidacion
                                                            With .AlgoritmoValidacion
                                                                .Identificador = If(termino.Table.Columns.Contains("OID_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(termino("OID_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                                                                .Codigo = If(termino.Table.Columns.Contains("COD_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(termino("COD_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                                                                .Descripcion = If(termino.Table.Columns.Contains("DES_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(termino("DES_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                                                                .Observacion = If(termino.Table.Columns.Contains("OBS_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(termino("OBS_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                                                            End With
                                                        End If

                                                        'Verifica se possui mascara.
                                                        If termino.Table.Columns.Contains("OID_MASCARA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(termino("OID_MASCARA"), GetType(String))) Then
                                                            .Mascara = New Clases.Mascara
                                                            With .Mascara
                                                                .Identificador = If(termino.Table.Columns.Contains("OID_MASCARA"), Util.AtribuirValorObj(termino("OID_MASCARA"), GetType(String)), Nothing)
                                                                .Codigo = If(termino.Table.Columns.Contains("COD_MASCARA"), Util.AtribuirValorObj(termino("COD_MASCARA"), GetType(String)), Nothing)
                                                                .Descripcion = If(termino.Table.Columns.Contains("DES_MASCARA"), Util.AtribuirValorObj(termino("DES_MASCARA"), GetType(String)), Nothing)
                                                                .ExpresionRegular = If(termino.Table.Columns.Contains("DES_EXP_REGULAR"), Util.AtribuirValorObj(termino("DES_EXP_REGULAR"), GetType(String)), Nothing)
                                                            End With
                                                        End If

                                                        If valores IsNot Nothing AndAlso valores.Count > 0 Then

                                                            If valores.Exists(Function(e) e.Item1 = .Identificador) Then
                                                                .ValoresPosibles = valores.FirstOrDefault(Function(v) v.Item1 = .Identificador).Item2
                                                            End If

                                                        End If

                                                    End With

                                                    .TerminosIAC.Add(terminoIAC)


                                                Next

                                                .TerminosIAC = New ObservableCollection(Of Clases.TerminoIAC)(.TerminosIAC.OrderBy(Function(termino) termino.Orden).ToList())
                                            End If

                                        End If

                                    End With

                                End If
                            End If


                            'GrupoTerminosIAC Grupo
                            If rowFormulario.Table.Columns.Contains("OID_IAC_GRUPO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowFormulario("OID_IAC_GRUPO"), GetType(String))) Then

                                Dim _iacs = tdIAC.Select("OID_IAC = '" & Util.AtribuirValorObj(rowFormulario("OID_IAC_GRUPO"), GetType(String)) & "'")

                                If _iacs IsNot Nothing Then

                                    .GrupoTerminosIACGrupo = New Clases.GrupoTerminosIAC

                                    With .GrupoTerminosIACGrupo

                                        .Identificador = If(_iacs(0).Table.Columns.Contains("OID_IAC"), Util.AtribuirValorObj(_iacs(0)("OID_IAC"), GetType(String)), Nothing)
                                        .Codigo = If(_iacs(0).Table.Columns.Contains("COD_IAC"), Util.AtribuirValorObj(_iacs(0)("COD_IAC"), GetType(String)), Nothing)
                                        .Descripcion = If(_iacs(0).Table.Columns.Contains("DES_IAC"), Util.AtribuirValorObj(_iacs(0)("DES_IAC"), GetType(String)), Nothing)
                                        .Observacion = If(_iacs(0).Table.Columns.Contains("OBS_IAC"), Util.AtribuirValorObj(_iacs(0)("OBS_IAC"), GetType(String)), Nothing)
                                        .EstaActivo = If(_iacs(0).Table.Columns.Contains("BOL_VIGENTE"), Util.AtribuirValorObj(_iacs(0)("BOL_VIGENTE"), GetType(Boolean)), Nothing)
                                        .EsInvisible = If(_iacs(0).Table.Columns.Contains("BOL_INVISIBLE"), Util.AtribuirValorObj(_iacs(0)("BOL_INVISIBLE"), GetType(Boolean)), Nothing)
                                        .CopiarDeclarados = If(_iacs(0).Table.Columns.Contains("BOL_COPIA_DECLARADOS"), Util.AtribuirValorObj(_iacs(0)("BOL_COPIA_DECLARADOS"), GetType(Boolean)), Nothing)
                                        .TerminosIAC = New ObservableCollection(Of Clases.TerminoIAC)

                                        If tdTerminos IsNot Nothing AndAlso tdTerminos.Rows.Count > 0 AndAlso _
                                            _iacs(0).Table.Columns.Contains("OID_IAC") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_iacs(0)("OID_IAC"), GetType(String))) Then

                                            Dim _Terminos = tdTerminos.Select("OID_IAC = '" & Util.AtribuirValorObj(_iacs(0)("OID_IAC"), GetType(String)) & "'")

                                            If _Terminos IsNot Nothing Then

                                                For Each termino In _Terminos

                                                    Dim terminoIAC As New Clases.TerminoIAC
                                                    terminoIAC.Formato = New Clases.Formato

                                                    With terminoIAC
                                                        .Identificador = If(termino.Table.Columns.Contains("OID_TERMINO"), Util.AtribuirValorObj(termino("OID_TERMINO"), GetType(String)), Nothing)
                                                        .BuscarParcial = If(termino.Table.Columns.Contains("BOL_BUSQUEDA_PARCIAL"), Util.AtribuirValorObj(termino("BOL_BUSQUEDA_PARCIAL"), GetType(Boolean)), Nothing)
                                                        .EsCampoClave = If(termino.Table.Columns.Contains("BOL_CAMPO_CLAVE"), Util.AtribuirValorObj(termino("BOL_CAMPO_CLAVE"), GetType(Boolean)), Nothing)
                                                        .Orden = If(termino.Table.Columns.Contains("NEC_ORDEN"), Util.AtribuirValorObj(termino("NEC_ORDEN"), GetType(Integer)), Nothing)
                                                        .EsObligatorio = If(termino.Table.Columns.Contains("BOL_ES_OBLIGATORIO"), Util.AtribuirValorObj(termino("BOL_ES_OBLIGATORIO"), GetType(Boolean)), Nothing)
                                                        .EsTerminoCopia = If(termino.Table.Columns.Contains("BOL_TERMINO_COPIA"), Util.AtribuirValorObj(termino("BOL_TERMINO_COPIA"), GetType(Boolean)), Nothing)
                                                        .EsProtegido = If(termino.Table.Columns.Contains("BOL_ES_PROTEGIDO"), Util.AtribuirValorObj(termino("BOL_ES_PROTEGIDO"), GetType(Boolean)), Nothing)
                                                        .Codigo = If(termino.Table.Columns.Contains("COD_TERMINO"), Util.AtribuirValorObj(termino("COD_TERMINO"), GetType(String)), Nothing)
                                                        .Observacion = If(termino.Table.Columns.Contains("OBS_TERMINO"), Util.AtribuirValorObj(termino("OBS_TERMINO"), GetType(String)), Nothing)
                                                        .Longitud = If(termino.Table.Columns.Contains("NEC_LONGITUD"), Util.AtribuirValorObj(termino("NEC_LONGITUD"), GetType(Integer)), Nothing)
                                                        .MostrarDescripcionConCodigo = If(termino.Table.Columns.Contains("BOL_MOSTRAR_CODIGO"), Util.AtribuirValorObj(termino("BOL_MOSTRAR_CODIGO"), GetType(Boolean)), Nothing)
                                                        .TieneValoresPosibles = If(termino.Table.Columns.Contains("BOL_VALORES_POSIBLES"), Util.AtribuirValorObj(termino("BOL_VALORES_POSIBLES"), GetType(Boolean)), Nothing)
                                                        .AceptarDigitacion = If(termino.Table.Columns.Contains("BOL_ACEPTAR_DIGITACION"), Util.AtribuirValorObj(termino("BOL_ACEPTAR_DIGITACION"), GetType(Boolean)), Nothing)
                                                        .EstaActivo = If(termino.Table.Columns.Contains("BOL_VIGENTE"), Util.AtribuirValorObj(termino("BOL_VIGENTE"), GetType(Boolean)), Nothing)
                                                        .EsEspecificoDeSaldos = If(termino.Table.Columns.Contains("BOL_ESPECIFICO_DE_SALDOS"), Util.AtribuirValorObj(termino("BOL_ESPECIFICO_DE_SALDOS"), GetType(Boolean)), Nothing)
                                                        .Descripcion = If(termino.Table.Columns.Contains("DES_TERMINO"), Util.AtribuirValorObj(termino("DES_TERMINO"), GetType(String)), Nothing)

                                                        With terminoIAC.Formato
                                                            .Identificador = If(termino.Table.Columns.Contains("OID_FORMATO"), Util.AtribuirValorObj(termino("OID_FORMATO"), GetType(String)), Nothing)
                                                            .Codigo = If(termino.Table.Columns.Contains("COD_FORMATO"), Util.AtribuirValorObj(termino("COD_FORMATO"), GetType(String)), Nothing)
                                                            .Descripcion = If(termino.Table.Columns.Contains("DES_FORMATO"), Util.AtribuirValorObj(termino("DES_FORMATO"), GetType(String)), Nothing)
                                                        End With

                                                        'Verifica se possui algoritimo de validação.
                                                        If termino.Table.Columns.Contains("OID_ALGORITMO_VALIDACION") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(termino("OID_ALGORITMO_VALIDACION"), GetType(String))) Then
                                                            .AlgoritmoValidacion = New Clases.AlgoritmoValidacion
                                                            With .AlgoritmoValidacion
                                                                .Identificador = If(termino.Table.Columns.Contains("OID_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(termino("OID_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                                                                .Codigo = If(termino.Table.Columns.Contains("COD_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(termino("COD_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                                                                .Descripcion = If(termino.Table.Columns.Contains("DES_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(termino("DES_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                                                                .Observacion = If(termino.Table.Columns.Contains("OBS_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(termino("OBS_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                                                            End With
                                                        End If

                                                        'Verifica se possui mascara.
                                                        If termino.Table.Columns.Contains("OID_MASCARA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(termino("OID_MASCARA"), GetType(String))) Then
                                                            .Mascara = New Clases.Mascara
                                                            With .Mascara
                                                                .Identificador = If(termino.Table.Columns.Contains("OID_MASCARA"), Util.AtribuirValorObj(termino("OID_MASCARA"), GetType(String)), Nothing)
                                                                .Codigo = If(termino.Table.Columns.Contains("COD_MASCARA"), Util.AtribuirValorObj(termino("COD_MASCARA"), GetType(String)), Nothing)
                                                                .Descripcion = If(termino.Table.Columns.Contains("DES_MASCARA"), Util.AtribuirValorObj(termino("DES_MASCARA"), GetType(String)), Nothing)
                                                                .ExpresionRegular = If(termino.Table.Columns.Contains("DES_EXP_REGULAR"), Util.AtribuirValorObj(termino("DES_EXP_REGULAR"), GetType(String)), Nothing)
                                                            End With
                                                        End If
                                                    End With

                                                    .TerminosIAC.Add(terminoIAC)

                                                Next
                                            End If

                                        End If

                                    End With

                                End If

                            End If

                        End If

                    End With

                    formularios.Add(formulario)

                Next

            End If

            Return formularios

        End Function

        Private Shared Function HacerConsultaConFormularios(ByRef query As String,
                                                            Optional ByRef codigosFormularios As List(Of String) = Nothing,
                                                            Optional ByRef identificadoresFormularios As List(Of String) = Nothing) As DataTable
            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim filtro As String = ""

                If codigosFormularios IsNot Nothing Then
                    If codigosFormularios.Count = 1 Then
                        filtro &= " AND F.COD_FORMULARIO = []COD_FORMULARIO "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_FORMULARIO", ProsegurDbType.Descricao_Curta, codigosFormularios(0)))
                    ElseIf codigosFormularios.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosFormularios, "COD_FORMULARIO", cmd, "AND", "F", , False)
                    End If
                End If

                If identificadoresFormularios IsNot Nothing Then
                    If identificadoresFormularios.Count = 1 Then
                        filtro &= " AND F.OID_FORMULARIO = []OID_FORMULARIO "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Descricao_Curta, identificadoresFormularios(0)))
                    ElseIf identificadoresFormularios.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresFormularios, "OID_FORMULARIO", cmd, "AND", "F", , False)
                    End If
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function

        Shared Function ObtenerTerminosIACPorCodigoFormulario(codigoFormulario As String, esIndividual As Boolean, listaTerminos As List(Of String)) As Clases.GrupoTerminosIAC

            Dim tdIAC As DataTable = Nothing
            Dim tdTerminos As DataTable = Nothing

            Dim TIAC As New Task(Sub()
                                     tdIAC = ObtenerGrupoTerminosIAC(codigoFormulario, esIndividual)
                                 End Sub)
            TIAC.Start()

            Dim TTermino As New Task(Sub()
                                         tdTerminos = ObtenerTerminosIAC(codigoFormulario, esIndividual, listaTerminos)
                                     End Sub)
            TTermino.Start()

            ' Aguarda que as tasks terminem antes de continuar
            Task.WaitAll(New Task() {TIAC, TTermino})

            Return cargarGrupoTerminosIAC(tdIAC, tdTerminos)

        End Function

        Private Shared Function cargarGrupoTerminosIAC(tdIAC As DataTable,
                                                       tdTerminos As DataTable) As Clases.GrupoTerminosIAC

            Dim _GrupoTerminosIAC = New Clases.GrupoTerminosIAC

            If tdIAC IsNot Nothing AndAlso tdIAC.Rows.Count > 0 Then

                With _GrupoTerminosIAC

                    .Identificador = If(tdIAC.Rows(0).Table.Columns.Contains("OID_IAC"), Util.AtribuirValorObj(tdIAC.Rows(0)("OID_IAC"), GetType(String)), Nothing)
                    .Codigo = If(tdIAC.Rows(0).Table.Columns.Contains("COD_IAC"), Util.AtribuirValorObj(tdIAC.Rows(0)("COD_IAC"), GetType(String)), Nothing)
                    .Descripcion = If(tdIAC.Rows(0).Table.Columns.Contains("DES_IAC"), Util.AtribuirValorObj(tdIAC.Rows(0)("DES_IAC"), GetType(String)), Nothing)
                    .Observacion = If(tdIAC.Rows(0).Table.Columns.Contains("OBS_IAC"), Util.AtribuirValorObj(tdIAC.Rows(0)("OBS_IAC"), GetType(String)), Nothing)
                    .EstaActivo = If(tdIAC.Rows(0).Table.Columns.Contains("BOL_VIGENTE"), Util.AtribuirValorObj(tdIAC.Rows(0)("BOL_VIGENTE"), GetType(Boolean)), Nothing)
                    .EsInvisible = If(tdIAC.Rows(0).Table.Columns.Contains("BOL_INVISIBLE"), Util.AtribuirValorObj(tdIAC.Rows(0)("BOL_INVISIBLE"), GetType(Boolean)), Nothing)
                    .CopiarDeclarados = If(tdIAC.Rows(0).Table.Columns.Contains("BOL_COPIA_DECLARADOS"), Util.AtribuirValorObj(tdIAC.Rows(0)("BOL_COPIA_DECLARADOS"), GetType(Boolean)), Nothing)
                    .TerminosIAC = New ObservableCollection(Of Clases.TerminoIAC)

                    If tdTerminos IsNot Nothing AndAlso tdTerminos.Rows.Count > 0 AndAlso _
                        tdIAC.Rows(0).Table.Columns.Contains("OID_IAC") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(tdIAC.Rows(0)("OID_IAC"), GetType(String))) Then

                        Dim _Terminos = tdTerminos.Select("OID_IAC = '" & Util.AtribuirValorObj(tdIAC.Rows(0)("OID_IAC"), GetType(String)) & "'")

                        If _Terminos IsNot Nothing Then

                            For Each termino In _Terminos

                                Dim terminoIAC As New Clases.TerminoIAC
                                terminoIAC.Formato = New Clases.Formato

                                With terminoIAC
                                    .Identificador = If(termino.Table.Columns.Contains("OID_TERMINO"), Util.AtribuirValorObj(termino("OID_TERMINO"), GetType(String)), Nothing)
                                    .BuscarParcial = If(termino.Table.Columns.Contains("BOL_BUSQUEDA_PARCIAL"), Util.AtribuirValorObj(termino("BOL_BUSQUEDA_PARCIAL"), GetType(Boolean)), Nothing)
                                    .EsCampoClave = If(termino.Table.Columns.Contains("BOL_CAMPO_CLAVE"), Util.AtribuirValorObj(termino("BOL_CAMPO_CLAVE"), GetType(Boolean)), Nothing)
                                    .Orden = If(termino.Table.Columns.Contains("NEC_ORDEN"), Util.AtribuirValorObj(termino("NEC_ORDEN"), GetType(Integer)), Nothing)
                                    .EsObligatorio = If(termino.Table.Columns.Contains("BOL_ES_OBLIGATORIO"), Util.AtribuirValorObj(termino("BOL_ES_OBLIGATORIO"), GetType(Boolean)), Nothing)
                                    .EsTerminoCopia = If(termino.Table.Columns.Contains("BOL_TERMINO_COPIA"), Util.AtribuirValorObj(termino("BOL_TERMINO_COPIA"), GetType(Boolean)), Nothing)
                                    .EsProtegido = If(termino.Table.Columns.Contains("BOL_ES_PROTEGIDO"), Util.AtribuirValorObj(termino("BOL_ES_PROTEGIDO"), GetType(Boolean)), Nothing)
                                    .Codigo = If(termino.Table.Columns.Contains("COD_TERMINO"), Util.AtribuirValorObj(termino("COD_TERMINO"), GetType(String)), Nothing)
                                    .Observacion = If(termino.Table.Columns.Contains("OBS_TERMINO"), Util.AtribuirValorObj(termino("OBS_TERMINO"), GetType(String)), Nothing)
                                    .Longitud = If(termino.Table.Columns.Contains("NEC_LONGITUD"), Util.AtribuirValorObj(termino("NEC_LONGITUD"), GetType(Integer)), Nothing)
                                    .MostrarDescripcionConCodigo = If(termino.Table.Columns.Contains("BOL_MOSTRAR_CODIGO"), Util.AtribuirValorObj(termino("BOL_MOSTRAR_CODIGO"), GetType(Boolean)), Nothing)
                                    .TieneValoresPosibles = If(termino.Table.Columns.Contains("BOL_VALORES_POSIBLES"), Util.AtribuirValorObj(termino("BOL_VALORES_POSIBLES"), GetType(Boolean)), Nothing)
                                    .AceptarDigitacion = If(termino.Table.Columns.Contains("BOL_ACEPTAR_DIGITACION"), Util.AtribuirValorObj(termino("BOL_ACEPTAR_DIGITACION"), GetType(Boolean)), Nothing)
                                    .EstaActivo = If(termino.Table.Columns.Contains("BOL_VIGENTE"), Util.AtribuirValorObj(termino("BOL_VIGENTE"), GetType(Boolean)), Nothing)
                                    .EsEspecificoDeSaldos = If(termino.Table.Columns.Contains("BOL_ESPECIFICO_DE_SALDOS"), Util.AtribuirValorObj(termino("BOL_ESPECIFICO_DE_SALDOS"), GetType(Boolean)), Nothing)
                                    .Descripcion = If(termino.Table.Columns.Contains("DES_TERMINO"), Util.AtribuirValorObj(termino("DES_TERMINO"), GetType(String)), Nothing)

                                    With terminoIAC.Formato
                                        .Identificador = If(termino.Table.Columns.Contains("OID_FORMATO"), Util.AtribuirValorObj(termino("OID_FORMATO"), GetType(String)), Nothing)
                                        .Codigo = If(termino.Table.Columns.Contains("COD_FORMATO"), Util.AtribuirValorObj(termino("COD_FORMATO"), GetType(String)), Nothing)
                                        .Descripcion = If(termino.Table.Columns.Contains("DES_FORMATO"), Util.AtribuirValorObj(termino("DES_FORMATO"), GetType(String)), Nothing)
                                    End With

                                    'Verifica se possui algoritimo de validação.
                                    If termino.Table.Columns.Contains("OID_ALGORITMO_VALIDACION") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(termino("OID_ALGORITMO_VALIDACION"), GetType(String))) Then
                                        .AlgoritmoValidacion = New Clases.AlgoritmoValidacion
                                        With .AlgoritmoValidacion
                                            .Identificador = If(termino.Table.Columns.Contains("OID_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(termino("OID_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                                            .Codigo = If(termino.Table.Columns.Contains("COD_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(termino("COD_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                                            .Descripcion = If(termino.Table.Columns.Contains("DES_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(termino("DES_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                                            .Observacion = If(termino.Table.Columns.Contains("OBS_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(termino("OBS_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                                        End With
                                    End If

                                    'Verifica se possui mascara.
                                    If termino.Table.Columns.Contains("OID_MASCARA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(termino("OID_MASCARA"), GetType(String))) Then
                                        .Mascara = New Clases.Mascara
                                        With .Mascara
                                            .Identificador = If(termino.Table.Columns.Contains("OID_MASCARA"), Util.AtribuirValorObj(termino("OID_MASCARA"), GetType(String)), Nothing)
                                            .Codigo = If(termino.Table.Columns.Contains("COD_MASCARA"), Util.AtribuirValorObj(termino("COD_MASCARA"), GetType(String)), Nothing)
                                            .Descripcion = If(termino.Table.Columns.Contains("DES_MASCARA"), Util.AtribuirValorObj(termino("DES_MASCARA"), GetType(String)), Nothing)
                                            .ExpresionRegular = If(termino.Table.Columns.Contains("DES_EXP_REGULAR"), Util.AtribuirValorObj(termino("DES_EXP_REGULAR"), GetType(String)), Nothing)
                                        End With
                                    End If
                                End With

                                .TerminosIAC.Add(terminoIAC)
                            Next
                        End If
                    End If
                End With
            End If

            Return _GrupoTerminosIAC
        End Function

        Shared Function ObtenerGrupoTerminosIAC(codigoFormulario As String, esIndividual As Boolean) As DataTable

            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim filtro As String = ""
                Dim tipo As String = "OID_IAC_INDIVIDUAL"
                If Not esIndividual Then
                    tipo = "OID_IAC_GRUPO"
                End If

                filtro &= " AND F.COD_FORMULARIO = []COD_FORMULARIO "
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_FORMULARIO", ProsegurDbType.Descricao_Curta, codigoFormulario))

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerIAC_v3.ToString(), tipo, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function

        Shared Function ObtenerTerminosIAC(codigoFormulario As String, esIndividual As Boolean, listaTerminos As List(Of String)) As DataTable

            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim filtro1 As String = ""
                Dim filtro2 As String = ""

                Dim tipo As String = "OID_IAC_INDIVIDUAL"
                If Not esIndividual Then
                    tipo = "OID_IAC_GRUPO"
                End If

                filtro1 &= " AND F.COD_FORMULARIO = []COD_FORMULARIO "
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_FORMULARIO", ProsegurDbType.Descricao_Curta, codigoFormulario))

                If listaTerminos IsNot Nothing AndAlso listaTerminos.Count > 0 Then
                    filtro2 &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, listaTerminos, "COD_TERMINO", cmd, "AND", "T", , False)
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerTerminos_v3.ToString(), tipo, filtro1, filtro2))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function

        Public Shared Function ObtenerFormulariosConLasCaracteristicas_v2(caracteristicas As List(Of Enumeradores.CaracteristicaFormulario),
                                                                          Optional simplificado As Boolean = False) As ObservableCollection(Of Clases.Formulario)

            Dim formularios As ObservableCollection(Of Clases.Formulario) = Nothing

            If caracteristicas IsNot Nothing AndAlso caracteristicas.Count > 0 Then

                Try
                    Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                        'Dim inner As String = ""
                        Dim filtro As String = ""

                        Dim _caracteristicas As New List(Of String)
                        For Each c In caracteristicas
                            _caracteristicas.Add(c.RecuperarValor())
                        Next

                        If _caracteristicas.Count = 1 Then

                            filtro &= " AND COD_CARACT_FORMULARIO = []COD_CARACT_FORMULARIO "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CARACT_FORMULARIO", _
                                                                                        ProsegurDbType.Objeto_Id, _caracteristicas(0)))

                        Else

                            filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, _caracteristicas, "COD_CARACT_FORMULARIO", _
                                                                                   command, "AND")

                        End If

                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "CANTIDAD_CARACTERISTICAS", ProsegurDbType.Identificador_Alfanumerico, caracteristicas.Count))

                        command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.FormularioRecuperarOidPorCaracteristicas_V2, filtro))
                        command.CommandType = CommandType.Text

                        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            formularios = New ObservableCollection(Of Clases.Formulario)

                            For Each row In dt.Rows
                                Dim _formulario As New Clases.Formulario
                                With _formulario
                                    .Identificador = If(dt.Rows(0).Table.Columns.Contains("OID_FORMULARIO"), Util.AtribuirValorObj(dt.Rows(0)("OID_FORMULARIO"), GetType(String)), Nothing)
                                    .Codigo = If(dt.Rows(0).Table.Columns.Contains("COD_FORMULARIO"), Util.AtribuirValorObj(dt.Rows(0)("COD_FORMULARIO"), GetType(String)), Nothing)
                                    .EstaActivo = True
                                    .TipoDocumento = New Clases.TipoDocumento
                                    .TipoDocumento.Identificador = If(dt.Rows(0).Table.Columns.Contains("OID_TIPO_DOCUMENTO"), Util.AtribuirValorObj(dt.Rows(0)("OID_TIPO_DOCUMENTO"), GetType(String)), Nothing)
                                End With
                                formularios.Add(_formulario)
                            Next
                        End If

                    End Using

                Catch ex As Exception
                    Throw
                Finally
                    GC.Collect()
                End Try

            Else
                Return Nothing
            End If

            If formularios IsNot Nothing AndAlso formularios.Count > 0 AndAlso Not simplificado Then
                formularios = ObtenerFormularios_v2(Nothing, formularios.Select(Function(x) x.Identificador).ToList)
            End If

            Return formularios
        End Function

        Public Shared Function ObtenerFormulariosConLasCaracteristicas_v2(caracteristicas As List(Of Enumeradores.CaracteristicaFormulario),
                                                                 Optional simplificado As Boolean = False,
                                                                 Optional ByRef TransaccionActual As Transaccion = Nothing) As ObservableCollection(Of Clases.Formulario)

            Dim formularios As ObservableCollection(Of Clases.Formulario) = Nothing

            If caracteristicas IsNot Nothing AndAlso caracteristicas.Count > 0 Then

                Try
                    Dim wrapper As New DataBaseHelper.SPWrapper(String.Empty, False, CommandType.Text)
                    Dim filtro As String = ""

                    Dim _caracteristicas As New List(Of String)
                    For Each c In caracteristicas
                        _caracteristicas.Add(c.RecuperarValor())
                    Next

                    If _caracteristicas.Count = 1 Then

                        filtro &= " AND COD_CARACT_FORMULARIO = []COD_CARACT_FORMULARIO "
                        wrapper.AgregarParam("COD_CARACT_FORMULARIO", ProsegurDbType.Objeto_Id, _caracteristicas(0))

                    Else

                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, _caracteristicas, "COD_CARACT_FORMULARIO", _
                                                                               wrapper, TransaccionActual, "AND")

                    End If

                    wrapper.AgregarParam("CANTIDAD_CARACTERISTICAS", ProsegurDbType.Identificador_Alfanumerico, caracteristicas.Count)

                    wrapper.SP = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.FormularioRecuperarOidPorCaracteristicas_V2, filtro))

                    Dim ds As DataSet = DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, TransaccionActual)

                    Dim dt As DataTable = IIf(ds IsNot Nothing AndAlso ds.Tables.Count > 0, ds.Tables(0), Nothing)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        formularios = New ObservableCollection(Of Clases.Formulario)

                        For Each row In dt.Rows
                            Dim _formulario As New Clases.Formulario
                            With _formulario
                                .Identificador = If(dt.Rows(0).Table.Columns.Contains("OID_FORMULARIO"), Util.AtribuirValorObj(dt.Rows(0)("OID_FORMULARIO"), GetType(String)), Nothing)
                                .Codigo = If(dt.Rows(0).Table.Columns.Contains("COD_FORMULARIO"), Util.AtribuirValorObj(dt.Rows(0)("COD_FORMULARIO"), GetType(String)), Nothing)
                                .EstaActivo = True
                                .TipoDocumento = New Clases.TipoDocumento
                                .TipoDocumento.Identificador = If(dt.Rows(0).Table.Columns.Contains("OID_TIPO_DOCUMENTO"), Util.AtribuirValorObj(dt.Rows(0)("OID_TIPO_DOCUMENTO"), GetType(String)), Nothing)
                            End With
                            formularios.Add(_formulario)
                        Next
                    End If

                Catch ex As Exception
                    Throw
                Finally
                    GC.Collect()
                End Try

            Else
                Return Nothing
            End If

            If formularios IsNot Nothing AndAlso formularios.Count > 0 AndAlso Not simplificado Then
                formularios = ObtenerFormularios_v2(Nothing, formularios.Select(Function(x) x.Identificador).ToList)
            End If

            Return formularios
        End Function

        Shared Function ObtenerFormularioSimplificado(codigoFormulario As String) As Clases.Formulario
            Dim formulario As New Clases.Formulario

            If codigoFormulario IsNot Nothing AndAlso codigoFormulario.Count > 0 Then

                Try
                    Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_FORMULARIO", _
                                                                                        ProsegurDbType.Descricao_Curta, codigoFormulario))

                        command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.Formulario_ObtenerFormularioSimplificado)
                        command.CommandType = CommandType.Text

                        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                            With formulario
                                .Identificador = If(dt.Rows(0).Table.Columns.Contains("OID_FORMULARIO"), Util.AtribuirValorObj(dt.Rows(0)("OID_FORMULARIO"), GetType(String)), Nothing)
                                .TipoDocumento = New Clases.TipoDocumento
                                .TipoDocumento.Identificador = If(dt.Rows(0).Table.Columns.Contains("OID_TIPO_DOCUMENTO"), Util.AtribuirValorObj(dt.Rows(0)("OID_TIPO_DOCUMENTO"), GetType(String)), Nothing)
                            End With

                        End If

                    End Using

                Catch ex As Exception
                    Throw
                Finally
                    GC.Collect()
                End Try

            Else
                Return Nothing
            End If

            Return formulario
        End Function

#End Region

#Region "[CONSULTAS]"

        Public Shared Function ObtenerFormularios(objIdentificadoresFormularios As List(Of String)) As DataTable
            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandType = CommandType.Text
                cmd.CommandText = My.Resources.ObtenerFormularios.ToString()
                If objIdentificadoresFormularios IsNot Nothing Then
                    If objIdentificadoresFormularios.Count = 1 Then
                        cmd.CommandText &= " AND F.OID_FORMULARIO = '" & objIdentificadoresFormularios(0) & "' "
                    ElseIf objIdentificadoresFormularios.Count > 0 Then
                        cmd.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, objIdentificadoresFormularios, "OID_FORMULARIO", cmd, "AND", "F", , False)
                    End If
                End If
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function

        Public Shared Function ObtenerFormulariosPorCodigo(objCodigosFormularios As List(Of String)) As DataTable
            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandType = CommandType.Text
                cmd.CommandText = My.Resources.ObtenerFormularios.ToString()
                If objCodigosFormularios IsNot Nothing Then
                    If objCodigosFormularios.Count = 1 Then
                        cmd.CommandText &= " AND F.COD_FORMULARIO = '" & objCodigosFormularios(0) & "' "
                    ElseIf objCodigosFormularios.Count > 0 Then
                        cmd.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, objCodigosFormularios, "COD_FORMULARIO", cmd, "AND", "F", , False)
                    End If
                End If
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function

        Public Shared Function ObtenerFormularios() As List(Of Clases.Formulario)

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim Consulta As New StringBuilder()
            Consulta.AppendLine(My.Resources.ObtenerFormularios)

            ' preparar query
            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, Consulta.ToString)
            Comando.CommandType = CommandType.Text

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)
            Dim Formularios As List(Of Clases.Formulario) = Nothing
            If dt.Rows.Count > 0 Then

                Formularios = New List(Of Clases.Formulario)
                For Each row In dt.Rows

                    Dim Formulario As New Clases.Formulario
                    Formulario.AccionContable = New Clases.AccionContable
                    Formulario.TipoDocumento = New Clases.TipoDocumento

                    With Formulario

                        .Identificador = Util.AtribuirValorObj(row("OID_FORMULARIO"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(row("COD_FORMULARIO"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(row("DES_FORMULARIO"), GetType(String))

                        If row("cod_color") IsNot Nothing Then
                            .Color = System.Drawing.Color.FromName(row("cod_color").ToString)
                        End If

                        .Icono = Util.AtribuirValorObj(row("BIN_ICONO_FORMULARIO"), GetType(Byte()))
                        .EstaActivo = Util.AtribuirValorObj(row("BOL_ACTIVO"), GetType(String))
                        .FechaHoraCreacion = Util.AtribuirValorObj(row("GMT_CREACION"), GetType(DateTime))
                        .FechaHoraModificacion = Util.AtribuirValorObj(row("GMT_MODIFICACION"), GetType(DateTime))
                        .UsuarioCreacion = Util.AtribuirValorObj(row("DES_USUARIO_CREACION"), GetType(String))
                        .UsuarioModificacion = Util.AtribuirValorObj(row("DES_USUARIO_MODIFICACION"), GetType(String))

                        .AccionContable = AccionContable.ObtenerAccionContable(Util.AtribuirValorObj(row("OID_ACCION_CONTABLE"), GetType(String)))

                        Formulario.Caracteristicas = CaracteristicaFormulario.RecuperarCaracteristicasFormulario(Formulario.Identificador)

                    End With

                    If True Then

                    End If

                    Formularios.Add(Formulario)

                Next row
            End If
            Return Formularios

        End Function

        ''' <summary>
        ''' Recupera o formulário pelo identificador
        ''' </summary>
        ''' <param name="identificador"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [claudioniz.pereira] 04/09/2013 - Criado
        ''' </history>
        Public Shared Function ObtenerFormulario(identificador As String) As Clases.Formulario

            Dim objFormulario As Clases.Formulario = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.FormularioRecuperarPorIdentificador)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, identificador))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objFormulario = New Clases.Formulario
                objFormulario.AccionContable = New Clases.AccionContable
                objFormulario.TipoDocumento = New Clases.TipoDocumento

                With objFormulario

                    .Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_FORMULARIO"), GetType(String))
                    .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_FORMULARIO"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_FORMULARIO"), GetType(String))

                    If dt.Rows(0)("cod_color") IsNot Nothing Then
                        .Color = System.Drawing.Color.FromName(dt.Rows(0)("cod_color").ToString)
                    End If

                    .Icono = Util.AtribuirValorObj(dt.Rows(0)("BIN_ICONO_FORMULARIO"), GetType(Byte()))
                    .EstaActivo = Util.AtribuirValorObj(dt.Rows(0)("BOL_ACTIVO"), GetType(String))
                    .FechaHoraCreacion = Util.AtribuirValorObj(dt.Rows(0)("GMT_CREACION"), GetType(DateTime))
                    .FechaHoraModificacion = Util.AtribuirValorObj(dt.Rows(0)("GMT_MODIFICACION"), GetType(DateTime))
                    .UsuarioCreacion = Util.AtribuirValorObj(dt.Rows(0)("DES_USUARIO_CREACION"), GetType(String))
                    .UsuarioModificacion = Util.AtribuirValorObj(dt.Rows(0)("DES_USUARIO_MODIFICACION"), GetType(String))

                    .AccionContable = AccionContable.ObtenerAccionContable(Util.AtribuirValorObj(dt.Rows(0)("OID_ACCION_CONTABLE"), GetType(String)))

                    ' TipoDocumento
                    With objFormulario.TipoDocumento
                        .Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_TIPO_DOCUMENTO"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_TIPO_DOCUMENTO"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_TIPO_DOCUMENTO"), GetType(String))
                        .EstaActivo = Util.AtribuirValorObj(dt.Rows(0)("TD_BOL_ACTIVO"), GetType(Boolean))
                        .FechaHoraCreacion = Util.AtribuirValorObj(dt.Rows(0)("TD_GMT_CREACION"), GetType(DateTime))
                        .FechaHoraModificacion = Util.AtribuirValorObj(dt.Rows(0)("TD_GMT_MODIFICACION"), GetType(DateTime))
                        .UsuarioCreacion = Util.AtribuirValorObj(dt.Rows(0)("TD_DES_USUARIO_CREACION"), GetType(String))
                        .UsuarioModificacion = Util.AtribuirValorObj(dt.Rows(0)("TD_DES_USUARIO_MODIFICACION"), GetType(String))
                    End With

                    'GrupoTerminosIAC individual
                    'Verifica se não é nulo
                    If Not String.IsNullOrEmpty(Util.AtribuirValorObj(dt.Rows(0)("OID_IAC_INDIVIDUAL"), GetType(String))) Then
                        objFormulario.GrupoTerminosIACIndividual = Genesis.GrupoTerminosIAC.RecuperarGrupoTerminosIAC(dt.Rows(0)("OID_IAC_INDIVIDUAL"))
                    End If

                    'GrupoTerminosIAC Grupo
                    'Verifica se não é nulo
                    If Not String.IsNullOrEmpty(Util.AtribuirValorObj(dt.Rows(0)("OID_IAC_GRUPO"), GetType(String))) Then
                        objFormulario.GrupoTerminosIACGrupo = Genesis.GrupoTerminosIAC.RecuperarGrupoTerminosIAC(dt.Rows(0)("OID_IAC_GRUPO"))
                    End If

                    'OID_FILTRO_FORMULARIO
                    'Verifica se não é nulo
                    If Not String.IsNullOrEmpty(Util.AtribuirValorObj(dt.Rows(0)("OID_FILTRO_FORMULARIO"), GetType(String))) Then
                        objFormulario.FiltroFormulario = New Clases.Transferencias.FiltroFormulario
                        With objFormulario.FiltroFormulario
                            .Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_FILTRO_FORMULARIO"), GetType(String))
                            .Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_FILTRO_FORMULARIO"), GetType(String))
                            .SoloMovimientoDisponible = Util.AtribuirValorObj(dt.Rows(0)("BOL_SOLO_DISPONIBLE"), GetType(Boolean))
                            .UtilizaDocumentosConValor = Util.AtribuirValorObj(dt.Rows(0)("BOL_CON_VALOR"), GetType(Boolean))
                            .UtilizaDocumentosConBulto = Util.AtribuirValorObj(dt.Rows(0)("FF_BOL_CON_BULTO"), GetType(Boolean))
                            .SoloMovimientoDeReenvio = Util.AtribuirValorObj(dt.Rows(0)("BOL_SOLO_REENVIO"), GetType(Boolean))
                            .SoloMovimientoDeSustitucion = Util.AtribuirValorObj(dt.Rows(0)("BOL_SOLO_SUSTITUCION"), GetType(Boolean))
                            .MovimientoConFechaEspecifica = Util.AtribuirValorObj(dt.Rows(0)("BOL_CON_FECHA_ESPECIFICA"), GetType(Boolean))
                            .CantidadDiasBusquedaInicio = Util.AtribuirValorObj(dt.Rows(0)("NEC_DIAS_BUSQUEDA_INICIO"), GetType(Boolean))
                            .CantidadDiasBusquedaFin = Util.AtribuirValorObj(dt.Rows(0)("NEC_DIAS_BUSQUEDA_FIN"), GetType(Boolean))
                            .EstaActivo = Util.AtribuirValorObj(dt.Rows(0)("FF_BOL_ACTIVO"), GetType(Boolean))
                            .FechaHoraCreacion = Util.AtribuirValorObj(dt.Rows(0)("FF_GMT_CREACION"), GetType(DateTime))
                            .UsuarioCreacion = Util.AtribuirValorObj(dt.Rows(0)("FF_DES_USUARIO_CREACION"), GetType(String))
                            .FechaHoraModificacion = Util.AtribuirValorObj(dt.Rows(0)("FF_GMT_MODIFICACION"), GetType(DateTime))
                            .UsuarioModificacion = Util.AtribuirValorObj(dt.Rows(0)("FF_DES_USUARIO_MODIFICACION"), GetType(String))
                        End With
                    End If

                    objFormulario.Caracteristicas = CaracteristicaFormulario.RecuperarCaracteristicasFormulario(objFormulario.Identificador)

                End With
            End If

            Return objFormulario
        End Function

        ''' <summary>
        ''' Recupera o formulário pelo identificador
        ''' </summary>
        ''' <param name="formulario">Clases.Formulario</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [maoliveira] 20/11/2013 - Criado
        ''' </history>
        Public Shared Function ObtenerFormulario(formulario As Clases.Formulario) As Clases.Formulario

            Dim objFormulario As Clases.Formulario = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.FormularioRecuperarPorCodigo)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_FORMULARIO", ProsegurDbType.Identificador_Alfanumerico, formulario.Codigo))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objFormulario = New Clases.Formulario
                objFormulario.AccionContable = New Clases.AccionContable
                objFormulario.TipoDocumento = New Clases.TipoDocumento

                With objFormulario

                    .Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_FORMULARIO"), GetType(String))
                    .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_FORMULARIO"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_FORMULARIO"), GetType(String))

                    If dt.Rows(0)("cod_color") IsNot Nothing Then
                        .Color = System.Drawing.Color.FromName(dt.Rows(0)("cod_color").ToString)
                    End If

                    .Icono = Util.AtribuirValorObj(dt.Rows(0)("BIN_ICONO_FORMULARIO"), GetType(Byte()))
                    .EstaActivo = Util.AtribuirValorObj(dt.Rows(0)("BOL_ACTIVO"), GetType(String))
                    .FechaHoraCreacion = Util.AtribuirValorObj(dt.Rows(0)("GMT_CREACION"), GetType(DateTime))
                    .FechaHoraModificacion = Util.AtribuirValorObj(dt.Rows(0)("GMT_MODIFICACION"), GetType(DateTime))
                    .UsuarioCreacion = Util.AtribuirValorObj(dt.Rows(0)("DES_USUARIO_CREACION"), GetType(String))
                    .UsuarioModificacion = Util.AtribuirValorObj(dt.Rows(0)("DES_USUARIO_MODIFICACION"), GetType(String))

                    .AccionContable = AccionContable.ObtenerAccionContable(Util.AtribuirValorObj(dt.Rows(0)("OID_ACCION_CONTABLE"), GetType(String)))

                    ' TipoDocumento
                    With objFormulario.TipoDocumento
                        .Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_TIPO_DOCUMENTO"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_TIPO_DOCUMENTO"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_TIPO_DOCUMENTO"), GetType(String))
                        .EstaActivo = Util.AtribuirValorObj(dt.Rows(0)("TD_BOL_ACTIVO"), GetType(Boolean))
                        .FechaHoraCreacion = Util.AtribuirValorObj(dt.Rows(0)("TD_GMT_CREACION"), GetType(DateTime))
                        .FechaHoraModificacion = Util.AtribuirValorObj(dt.Rows(0)("TD_GMT_MODIFICACION"), GetType(DateTime))
                        .UsuarioCreacion = Util.AtribuirValorObj(dt.Rows(0)("TD_DES_USUARIO_CREACION"), GetType(String))
                        .UsuarioModificacion = Util.AtribuirValorObj(dt.Rows(0)("TD_DES_USUARIO_MODIFICACION"), GetType(String))
                    End With

                    'GrupoTerminosIAC individual
                    'Verifica se não é nulo
                    If Not String.IsNullOrEmpty(Util.AtribuirValorObj(dt.Rows(0)("OID_IAC_INDIVIDUAL"), GetType(String))) Then
                        objFormulario.GrupoTerminosIACIndividual = Genesis.GrupoTerminosIAC.RecuperarGrupoTerminosIAC(dt.Rows(0)("OID_IAC_INDIVIDUAL"))
                    End If

                    'GrupoTerminosIAC Grupo
                    'Verifica se não é nulo
                    If Not String.IsNullOrEmpty(Util.AtribuirValorObj(dt.Rows(0)("OID_IAC_GRUPO"), GetType(String))) Then
                        objFormulario.GrupoTerminosIACGrupo = Genesis.GrupoTerminosIAC.RecuperarGrupoTerminosIAC(dt.Rows(0)("OID_IAC_GRUPO"))
                    End If

                    'GrupoTerminosIAC
                    'Verifica se não é nulo
                    If Not String.IsNullOrEmpty(Util.AtribuirValorObj(dt.Rows(0)("OID_FILTRO_FORMULARIO"), GetType(String))) Then
                        objFormulario.FiltroFormulario = New Clases.Transferencias.FiltroFormulario
                        With objFormulario.FiltroFormulario
                            .Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_FILTRO_FORMULARIO"), GetType(String))
                            .Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_FILTRO_FORMULARIO"), GetType(String))
                            .SoloMovimientoDisponible = Util.AtribuirValorObj(dt.Rows(0)("BOL_SOLO_DISPONIBLE"), GetType(Boolean))
                            .UtilizaDocumentosConValor = Util.AtribuirValorObj(dt.Rows(0)("BOL_CON_VALOR"), GetType(Boolean))
                            .UtilizaDocumentosConBulto = Util.AtribuirValorObj(dt.Rows(0)("FF_BOL_CON_BULTO"), GetType(Boolean))
                            .SoloMovimientoDeReenvio = Util.AtribuirValorObj(dt.Rows(0)("BOL_SOLO_REENVIO"), GetType(Boolean))
                            .SoloMovimientoDeSustitucion = Util.AtribuirValorObj(dt.Rows(0)("BOL_SOLO_SUSTITUCION"), GetType(Boolean))
                            .MovimientoConFechaEspecifica = Util.AtribuirValorObj(dt.Rows(0)("BOL_CON_FECHA_ESPECIFICA"), GetType(Boolean))
                            .CantidadDiasBusquedaInicio = Util.AtribuirValorObj(dt.Rows(0)("NEC_DIAS_BUSQUEDA_INICIO"), GetType(Boolean))
                            .CantidadDiasBusquedaFin = Util.AtribuirValorObj(dt.Rows(0)("NEC_DIAS_BUSQUEDA_FIN"), GetType(Boolean))
                            .EstaActivo = Util.AtribuirValorObj(dt.Rows(0)("FF_BOL_ACTIVO"), GetType(Boolean))
                            .FechaHoraCreacion = Util.AtribuirValorObj(dt.Rows(0)("FF_GMT_CREACION"), GetType(DateTime))
                            .UsuarioCreacion = Util.AtribuirValorObj(dt.Rows(0)("FF_DES_USUARIO_CREACION"), GetType(String))
                            .FechaHoraModificacion = Util.AtribuirValorObj(dt.Rows(0)("FF_GMT_MODIFICACION"), GetType(DateTime))
                            .UsuarioModificacion = Util.AtribuirValorObj(dt.Rows(0)("FF_DES_USUARIO_MODIFICACION"), GetType(String))
                        End With
                    End If

                    objFormulario.Caracteristicas = CaracteristicaFormulario.RecuperarCaracteristicasFormulario(objFormulario.Identificador)

                End With
            End If

            Return objFormulario

        End Function

        ''' <summary>
        ''' Recupera todos os formulários
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [guilherme.corsino] 10/09/2013 - Criado
        ''' </history>
        Public Shared Function ObtenerFormulario(objPeticion As Prosegur.Genesis.Comon.Peticion(Of Clases.Sector), _
                                                 ByRef objRespuesta As Prosegur.Genesis.Comon.Respuesta(Of List(Of Clases.Formulario)), _
                                                 CodigosCaracteristicasParaIgnorar As List(Of String), CodRelacionFormulario As String) _
                                                 As List(Of Clases.Formulario)

            Dim listaFormularios As New List(Of Clases.Formulario)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RecuperarFormulario)
            cmd.CommandType = CommandType.Text

            Dim objSql As String = ""

            If CodigosCaracteristicasParaIgnorar IsNot Nothing AndAlso CodigosCaracteristicasParaIgnorar.Count > 0 Then
                Dim indiceParametro As Integer = 0
                For Each codigoCaracteristica In CodigosCaracteristicasParaIgnorar
                    objSql &= String.Format(" and not exists (SELECT 1 FROM SAPR_TCARACTFORMXFORMULARIO CO INNER JOIN SAPR_TCARACT_FORMULARIO CF on CF.OID_CARACT_FORMULARIO = CO.OID_CARACT_FORMULARIO WHERE CO.OID_FORMULARIO = F.oid_formulario AND CF.COD_CARACT_FORMULARIO = []CODCARACT_{0} )", indiceParametro)
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, String.Format("CODCARACT_{0}", indiceParametro), ProsegurDbType.Observacao_Curta, codigoCaracteristica))
                    indiceParametro += 1
                Next
            End If

            If objPeticion.Parametro.EsActivo Then
                objSql &= " and F.BOL_ACTIVO = 1 "
            End If

            If String.IsNullOrEmpty(objSql) Then
                cmd.CommandText = String.Format(cmd.CommandText, " ")
            Else
                cmd.CommandText = String.Format(cmd.CommandText, objSql)
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OIDSECTOR", ProsegurDbType.Objeto_Id, objPeticion.Parametro.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_RELACION_CON_FORMULARIO", ProsegurDbType.Identificador_Alfanumerico, CodRelacionFormulario))

            objRespuesta.ParametrosPaginacion = New Prosegur.Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion()

            Using DT As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GENESIS, cmd, objPeticion.ParametrosPaginacion, objRespuesta.ParametrosPaginacion)

                For Each dr In DT.Rows

                    Dim objFormulario = New Clases.Formulario
                    objFormulario.AccionContable = New Clases.AccionContable
                    objFormulario.TipoDocumento = New Clases.TipoDocumento

                    With objFormulario

                        .Identificador = Util.AtribuirValorObj(dr("OID_FORMULARIO"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(dr("COD_FORMULARIO"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(dr("DES_FORMULARIO"), GetType(String))

                        If dr("cod_color") IsNot Nothing Then
                            .Color = System.Drawing.Color.FromName(dr("cod_color").ToString)
                        End If

                        .AccionContable = AccionContable.ObtenerAccionContable(Util.AtribuirValorObj(dr("OID_ACCION_CONTABLE"), GetType(String)))

                        .Icono = Util.AtribuirValorObj(dr("BIN_ICONO_FORMULARIO"), GetType(Byte()))
                        .EstaActivo = Util.AtribuirValorObj(dr("BOL_ACTIVO"), GetType(String))
                        .FechaHoraCreacion = Util.AtribuirValorObj(dr("GMT_CREACION"), GetType(DateTime))
                        .FechaHoraModificacion = Util.AtribuirValorObj(dr("GMT_MODIFICACION"), GetType(DateTime))
                        .UsuarioCreacion = Util.AtribuirValorObj(dr("DES_USUARIO_CREACION"), GetType(String))
                        .UsuarioModificacion = Util.AtribuirValorObj(dr("DES_USUARIO_MODIFICACION"), GetType(String))

                        objFormulario.Caracteristicas = CaracteristicaFormulario.RecuperarCaracteristicasFormulario(objFormulario.Identificador)

                    End With

                    If True Then

                    End If

                    listaFormularios.Add(objFormulario)

                Next


            End Using

            Return listaFormularios
        End Function

        ''' <summary>
        ''' Recupera os formulários de reenvio automático.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [claudioniz.pereira] 15/01/2014 - Criado
        ''' </history>
        Public Shared Function ObtenerFormulariosReenvioAutomatico() As List(Of Clases.Formulario)

            Dim listaFormularios As New List(Of Clases.Formulario)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = My.Resources.FormularioReenvioAutomatico
            cmd.CommandType = CommandType.Text

            Using DT As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                For Each dr In DT.Rows

                    Dim objFormulario = New Clases.Formulario
                    objFormulario.AccionContable = New Clases.AccionContable
                    objFormulario.TipoDocumento = New Clases.TipoDocumento

                    With objFormulario

                        .Identificador = Util.AtribuirValorObj(dr("OID_FORMULARIO"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(dr("COD_FORMULARIO"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(dr("DES_FORMULARIO"), GetType(String))

                        If dr("cod_color") IsNot Nothing Then
                            .Color = System.Drawing.Color.FromName(dr("cod_color").ToString)
                        End If

                        .Icono = Util.AtribuirValorObj(dr("BIN_ICONO_FORMULARIO"), GetType(Byte()))
                        .EstaActivo = Util.AtribuirValorObj(dr("BOL_ACTIVO"), GetType(String))
                        .FechaHoraCreacion = Util.AtribuirValorObj(dr("GMT_CREACION"), GetType(DateTime))
                        .FechaHoraModificacion = Util.AtribuirValorObj(dr("GMT_MODIFICACION"), GetType(DateTime))
                        .UsuarioCreacion = Util.AtribuirValorObj(dr("DES_USUARIO_CREACION"), GetType(String))
                        .UsuarioModificacion = Util.AtribuirValorObj(dr("DES_USUARIO_MODIFICACION"), GetType(String))

                        .AccionContable = AccionContable.ObtenerAccionContable(Util.AtribuirValorObj(dr("OID_ACCION_CONTABLE"), GetType(String)))

                        objFormulario.Caracteristicas = CaracteristicaFormulario.RecuperarCaracteristicasFormulario(objFormulario.Identificador)

                    End With

                    If True Then

                    End If

                    listaFormularios.Add(objFormulario)

                Next
            End Using

            Return listaFormularios
        End Function

        ''' <summary>
        ''' Verifica se o formulário tem documentos
        ''' </summary>
        ''' <param name="identificadorFormulario"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FormularioHayDocumentos(identificadorFormulario As String) As Boolean

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.FormularioVerificaHayDocumentos)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, identificadorFormulario))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)
        End Function

        ''' <summary>
        ''' Verifica se o sector está configurado para ser utilizado no formulário informado
        ''' </summary>
        ''' <param name="identificadorFormulario"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FormularioVerificarSector(identificadorFormulario As String, identificadorSector As String, SectorOrigem As Boolean) As Boolean

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.FormularioVerificarSector)
            cmd.CommandType = CommandType.Text

            'se for sector de origem envia O(Origem) senão enviao D(DESTINO)
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_RELACION_CON_FORMULARIO", ProsegurDbType.Descricao_Curta, If(SectorOrigem = True, "O", "D")))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, identificadorFormulario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Objeto_Id, identificadorSector))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)
        End Function

        ''' <summary>
        ''' Exclui o formulário e as tabelas relacionadas
        ''' gepr_ttipo_sectorxformulario, sapr_tcaractformxformulario,sapr_tcopia
        ''' sapr_tsecuencia_comprobante, SAPR_TESTADOXACCION_CONTABLE, SAPR_TACCION_CONTABLE
        ''' SAPR_TFORMULARIOXTIPO_BULTO,SAPR_TFORMULARIO
        ''' </summary>
        ''' <param name="identificadorFormulario"></param>
        ''' <remarks></remarks>
        Public Shared Sub BorrarFormulario(identificadorFormulario As String)

            'Cria comando SQL
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            With comando
                .CommandType = CommandType.Text
                .CommandText = My.Resources.FormularioExcluir.Replace("##OID_FORM##", identificadorFormulario)
            End With
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, comando)
        End Sub

        ''' <summary>
        ''' Insere o formulário e as tabelas relacionadas
        ''' gepr_ttipo_sectorxformulario, sapr_tcaractformxformulario,sapr_tcopia
        ''' sapr_tsecuencia_comprobante, SAPR_TESTADOXACCION_CONTABLE, SAPR_TACCION_CONTABLE
        ''' SAPR_TFORMULARIOXTIPO_BULTO,SAPR_TFORMULARIO
        ''' </summary>
        ''' <param name="formulario"></param>
        ''' <remarks></remarks>
        Public Shared Sub GuardarFormulario(formulario As Clases.Formulario)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.FormularioInserir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, formulario.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ACCION_CONTABLE", ProsegurDbType.Objeto_Id, formulario.AccionContable.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC_INDIVIDUAL", ProsegurDbType.Objeto_Id, If(formulario.GrupoTerminosIACIndividual Is Nothing, Nothing, formulario.GrupoTerminosIACIndividual.Identificador)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC_GRUPO", ProsegurDbType.Objeto_Id, If(formulario.GrupoTerminosIACGrupo Is Nothing, Nothing, formulario.GrupoTerminosIACGrupo.Identificador)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FILTRO_FORMULARIO", ProsegurDbType.Objeto_Id, If(formulario.FiltroFormulario Is Nothing, Nothing, formulario.FiltroFormulario.Identificador)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TIPO_DOCUMENTO", ProsegurDbType.Objeto_Id, formulario.TipoDocumento.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_FORMULARIO", ProsegurDbType.Descricao_Longa, formulario.Codigo))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_FORMULARIO", ProsegurDbType.Descricao_Longa, formulario.Descripcion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_COLOR", ProsegurDbType.Descricao_Longa, formulario.Color.Name()))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BIN_ICONO_FORMULARIO", ProsegurDbType.Binario, formulario.Icono))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_ACTIVO", ProsegurDbType.Inteiro_Curto, formulario.EstaActivo))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, formulario.UsuarioCreacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, formulario.UsuarioModificacion))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        End Sub

        ''' <summary>
        ''' Atualiza o formulário
        ''' </summary>
        ''' <param name="formulario"></param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizaFormulario(formulario As Clases.Formulario)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.FormularioAtualizar)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, formulario.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ACCION_CONTABLE", ProsegurDbType.Objeto_Id, formulario.AccionContable.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC_INDIVIDUAL", ProsegurDbType.Objeto_Id, If(formulario.GrupoTerminosIACIndividual Is Nothing, Nothing, formulario.GrupoTerminosIACIndividual.Identificador)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC_GRUPO", ProsegurDbType.Objeto_Id, If(formulario.GrupoTerminosIACGrupo Is Nothing, Nothing, formulario.GrupoTerminosIACGrupo.Identificador)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FILTRO_FORMULARIO", ProsegurDbType.Objeto_Id, If(formulario.FiltroFormulario Is Nothing, Nothing, formulario.FiltroFormulario.Identificador)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TIPO_DOCUMENTO", ProsegurDbType.Objeto_Id, formulario.TipoDocumento.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_FORMULARIO", ProsegurDbType.Descricao_Longa, formulario.Codigo))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_FORMULARIO", ProsegurDbType.Descricao_Longa, formulario.Descripcion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_COLOR", ProsegurDbType.Descricao_Longa, formulario.Color.Name()))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BIN_ICONO_FORMULARIO", ProsegurDbType.Binario, formulario.Icono))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_ACTIVO", ProsegurDbType.Inteiro_Curto, formulario.EstaActivo))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, formulario.UsuarioModificacion))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        End Sub

        ''' <summary>
        ''' Recupera todos formulários
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [victor.ramos] 09/01/2014 - Criado
        ''' </history>
        Public Shared Function ObtenerFormularioPaginacion(objPeticion As Prosegur.Genesis.Comon.Peticion(Of Clases.Sector), _
                                                 ByRef objRespuesta As Prosegur.Genesis.Comon.Respuesta(Of List(Of Clases.Formulario))) As List(Of Clases.Formulario)

            Dim listaFormularios As New List(Of Clases.Formulario)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RecuperarTodosFormularios)
            cmd.CommandType = CommandType.Text

            objRespuesta.ParametrosPaginacion = New Prosegur.Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion()

            Using DT As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GENESIS, cmd, objPeticion.ParametrosPaginacion, objRespuesta.ParametrosPaginacion)

                For Each dr In DT.Rows

                    Dim objFormulario = New Clases.Formulario
                    objFormulario.AccionContable = New Clases.AccionContable
                    objFormulario.TipoDocumento = New Clases.TipoDocumento

                    With objFormulario

                        .Identificador = Util.AtribuirValorObj(dr("OID_FORMULARIO"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(dr("COD_FORMULARIO"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(dr("DES_FORMULARIO"), GetType(String))

                        If dr("cod_color") IsNot Nothing Then
                            .Color = System.Drawing.Color.FromName(dr("cod_color").ToString)
                        End If

                        .AccionContable = AccionContable.ObtenerAccionContable(Util.AtribuirValorObj(dr("OID_ACCION_CONTABLE"), GetType(String)))

                        .Icono = Util.AtribuirValorObj(dr("BIN_ICONO_FORMULARIO"), GetType(Byte()))
                        .EstaActivo = Util.AtribuirValorObj(dr("BOL_ACTIVO"), GetType(String))
                        .FechaHoraCreacion = Util.AtribuirValorObj(dr("GMT_CREACION"), GetType(DateTime))
                        .FechaHoraModificacion = Util.AtribuirValorObj(dr("GMT_MODIFICACION"), GetType(DateTime))
                        .UsuarioCreacion = Util.AtribuirValorObj(dr("DES_USUARIO_CREACION"), GetType(String))
                        .UsuarioModificacion = Util.AtribuirValorObj(dr("DES_USUARIO_MODIFICACION"), GetType(String))

                        objFormulario.Caracteristicas = CaracteristicaFormulario.RecuperarCaracteristicasFormulario(objFormulario.Identificador)

                    End With

                    If True Then

                    End If

                    listaFormularios.Add(objFormulario)

                Next


            End Using

            Return listaFormularios
        End Function

#End Region

        Public Shared Function ObtenerGrupoTerminoIACPorCodigoFormulario(codigoFormulario As String, esIndividual As Boolean) As String

            If codigoFormulario IsNot Nothing Then
                Dim grupoTerminoIAC As String = Nothing
                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerGrupoTerminoIACPorFormulario)
                cmd.CommandType = CommandType.Text

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_FORMULARIO", ProsegurDbType.Identificador_Alfanumerico, codigoFormulario))

                Dim dataTable As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                If dataTable IsNot Nothing AndAlso dataTable.Rows.Count > 0 Then
                    If esIndividual Then
                        grupoTerminoIAC = Util.AtribuirValorObj(dataTable.Rows(0).Item("OID_IAC_INDIVIDUAL"), GetType(String))
                    Else
                        grupoTerminoIAC = Util.AtribuirValorObj(dataTable.Rows(0).Item("OID_IAC_GRUPO"), GetType(String))
                    End If
                End If

                Return grupoTerminoIAC

            End If

            Return Nothing
        End Function

        Public Shared Function PreparaFiltroFormulario(ConjuntosCaracteristicas As ObservableCollection(Of ObservableCollection(Of Enumeradores.CaracteristicaFormulario)), ByRef comando As IDbCommand) As String
            Dim filtroIn As String = String.Empty

            Dim oidFormularios As New List(Of String)
            If ConjuntosCaracteristicas IsNot Nothing AndAlso ConjuntosCaracteristicas.Count > 0 Then
                Dim ListaConjuntosCaracteristicas As New List(Of List(Of String))
                'Para cada conjunto de caracteristicas
                For Each conjunto In ConjuntosCaracteristicas
                    If conjunto IsNot Nothing AndAlso conjunto.Count > 0 Then
                        Dim listaCaracteristicas As New List(Of String)
                        'Para cada caracteristica do conjunto
                        For Each carac In conjunto
                            listaCaracteristicas.Add(carac.RecuperarValor())
                        Next
                        ListaConjuntosCaracteristicas.Add(listaCaracteristicas)
                    End If
                Next
                oidFormularios = ObtenerCodigosFormulariosConLosConjuntosCaracteristicas(ListaConjuntosCaracteristicas)
            End If

            If oidFormularios IsNot Nothing AndAlso oidFormularios.Count > 0 Then
                filtroIn = " AND " & Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, oidFormularios, "OID_FORMULARIO", comando, String.Empty, "DOCU", , False)
            End If

            Return filtroIn
        End Function

        Private Shared Function ObtenerCodigosFormulariosConLosConjuntosCaracteristicas(ConjuntoCaracteristicas As List(Of List(Of String))) As List(Of String)

            Dim formularios As New List(Of String)
            Dim filtroIn As String = String.Empty
            Dim filtroHaving As String = String.Empty
            Dim subSelect As String = String.Empty

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.FormularioRecuperarPorConjuntoCaracteristicas)
            cmd.CommandType = CommandType.Text

            If ConjuntoCaracteristicas IsNot Nothing AndAlso ConjuntoCaracteristicas.Count > 0 Then
                For i As Integer = 0 To ConjuntoCaracteristicas.Count - 1
                    'Se o conjunto de característica contém características
                    If ConjuntoCaracteristicas(i).Count > 0 Then
                        filtroIn = String.Empty
                        filtroHaving = String.Empty
                        'Monta a clásula in do conjunto de característica
                        filtroIn = Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, ConjuntoCaracteristicas(i), "COD_CARACT_FORMULARIO", cmd, String.Empty, "CARA", "COD" & i, False)
                        filtroHaving = "[]CANTIDAD_CARACTERISTICAS" & i

                        subSelect = subSelect & If(i > 0, " UNION ", "") & My.Resources.FormularioSubSelectRecuperarPorConjuntoCaracteristicas.ToString

                        subSelect = String.Format(subSelect, filtroIn, filtroHaving)

                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "CANTIDAD_CARACTERISTICAS" & i, ProsegurDbType.Identificador_Alfanumerico, ConjuntoCaracteristicas(i).Count))

                    End If
                Next i
            Else
                subSelect = "''"
            End If

            'cmd.CommandText = Replace(String.Format(cmd.CommandText, subSelect), "[]", ":")
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, subSelect))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                formularios = New List(Of String)
                For Each row In dt.Rows
                    formularios.Add(Util.AtribuirValorObj(row("OID_FORMULARIO"), GetType(String)))
                Next
            Else
                Return Nothing
            End If

            Return formularios

        End Function

    End Class
End Namespace

