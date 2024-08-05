Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper

Namespace Genesis.Job
    Public Class NotificarMovimientosNoAcreditados
        Public Shared Sub Ejecutar(identificadorLlamada As String, peticion As ContractoServicio.Contractos.Job.NotificarMovimientosNoAcreditados.Peticion,
                                   ByRef respuesta As ContractoServicio.Contractos.Job.NotificarMovimientosNoAcreditados.Respuesta,
                                   ByRef movimientos As List(Of ContractoServicio.Contractos.Job.NotificarMovimientosNoAcreditados.Salida.MovimientoNoAcreditado),
                                   Optional ByRef log As StringBuilder = Nothing)

            Try
                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                spw = ColectarPeticion(peticion, identificadorLlamada)
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                PoblarRespuesta(ds, respuesta, movimientos)
                spw = Nothing
                ds.Dispose()

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

        End Sub

        Private Shared Function ColectarPeticion(peticion As ContractoServicio.Contractos.Job.NotificarMovimientosNoAcreditados.Peticion,
                                                 identificadorLlamada As String) As SPWrapper
            Dim SP As String = String.Format("SAPR_PJOB_{0}.snotificar_mov_noacreditados", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada)
            spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Descricao_Curta, peticion.Configuracion.IdentificadorAjeno)
            spw.AgregarParam("par$cod_pais", ProsegurDbType.Descricao_Curta, peticion.CodigoPais)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Curta, peticion.Configuracion.Usuario)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Curta, Util.GetCultureUser())
            spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
            spw.AgregarParam("par$rc_movimientos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "movimientos")

            Return spw

        End Function

        Private Shared Sub PoblarRespuesta(ds As DataSet, respuesta As ContractoServicio.Contractos.Job.NotificarMovimientosNoAcreditados.Respuesta,
                                           movimientos As List(Of ContractoServicio.Contractos.Job.NotificarMovimientosNoAcreditados.Salida.MovimientoNoAcreditado))

            Dim dsNameTableMovimientos As String = "movimientos"

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                ' Validaciones
                If ds.Tables.Contains("validaciones") AndAlso ds.Tables("validaciones").Rows.Count > 0 Then

                    If respuesta.Detalles Is Nothing Then respuesta.Detalles = New List(Of ContractoServicio.Contractos.Job.NotificarMovimientosNoAcreditados.Salida.Detalle)
                    For Each row As DataRow In ds.Tables("validaciones").Rows
                        respuesta.Detalles.Add(New ContractoServicio.Contractos.Job.NotificarMovimientosNoAcreditados.Salida.Detalle With {.Codigo = Util.AtribuirValorObj(row(0), GetType(String)), .Descripcion = Util.AtribuirValorObj(row(1), GetType(String))})
                    Next
                End If

                If ds.Tables.Contains(dsNameTableMovimientos) AndAlso ds.Tables(dsNameTableMovimientos).Rows.Count > 0 Then

                    Dim rMovimientos As DataRow() = ds.Tables(dsNameTableMovimientos).Select()

                    If rMovimientos IsNot Nothing AndAlso rMovimientos.Count > 0 Then

                        If movimientos Is Nothing Then movimientos = New List(Of ContractoServicio.Contractos.Job.NotificarMovimientosNoAcreditados.Salida.MovimientoNoAcreditado)

                        For Each rowMovimiento As DataRow In rMovimientos

                            Dim movimiento As New ContractoServicio.Contractos.Job.NotificarMovimientosNoAcreditados.Salida.MovimientoNoAcreditado

                            With movimiento

                                .CodTipoPlanificacion = Util.AtribuirValorObj(rowMovimiento("COD_TIPO_PLANIFICACION"), GetType(String))
                                .DesPlanificacion = Util.AtribuirValorObj(rowMovimiento("DES_PLANIFICACION"), GetType(String))
                                .CodPlanificacionBanco = Util.AtribuirValorObj(rowMovimiento("COD_PLANIFICACION_BANCO_OPE"), GetType(String))
                                .DesPlanificacionBanco = Util.AtribuirValorObj(rowMovimiento("DES_PLANIFICACION_BANCO"), GetType(String))
                                .CodDeviceId = Util.AtribuirValorObj(rowMovimiento("COD_DEVICEID"), GetType(String))
                                .CodCliente = Util.AtribuirValorObj(rowMovimiento("COD_CLIENTE_OPE"), GetType(String))
                                .HorMovimiento = Util.AtribuirValorObj(rowMovimiento("HOR_MOVIMIENTO"), GetType(String))
                                .CodGrupoMovimiento = Util.AtribuirValorObj(rowMovimiento("COD_GRUPO_MOVIMIENTO"), GetType(String))
                                .CodMovimiento = Util.AtribuirValorObj(rowMovimiento("COD_MOVIMIENTO"), GetType(String))
                                .DesCliente = Util.AtribuirValorObj(rowMovimiento("DES_CLIENTE"), GetType(String))
                                .HorasTranscurridas = Util.AtribuirValorObj(rowMovimiento("HORAS_TRANSCURRIDAS"), GetType(Integer))
                                .CodPtoServicio = Util.AtribuirValorObj(rowMovimiento("COD_PTO_SERVICIO_OPE"), GetType(String))
                                .DesPtoServicio = Util.AtribuirValorObj(rowMovimiento("DES_PTO_SERVICIO"), GetType(String))

                            End With

                            movimientos.Add(movimiento)

                        Next

                    End If

                End If

            End If
        End Sub

    End Class
End Namespace
