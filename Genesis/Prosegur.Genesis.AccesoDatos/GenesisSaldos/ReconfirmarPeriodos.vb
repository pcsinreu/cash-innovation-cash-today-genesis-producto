Imports System.Text
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.ReconfirmarPeriodos

Namespace GenesisSaldos
    Public Class ReconfirmarPeriodos

        Public Shared Sub ReconfirmarPeriodos(identificadorLlamada As String,
                                       peticion As Peticion,
                                       ByRef respuesta As Respuesta,
                                       Optional ByRef log As StringBuilder = Nothing)
            Try
                If log Is Nothing Then log = New StringBuilder
                Dim TiempoParcial As DateTime = Now

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                TiempoParcial = Now
                spw = ArmarWrapper(identificadorLlamada, peticion)
                log.AppendLine("Tiempo de acceso a datos (Parametros para procedure): " & Now.Subtract(TiempoParcial).ToString() & "; ")

                TiempoParcial = Now
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                log.AppendLine("Tiempo de acceso a datos (Ejecutar procedure): " & Now.Subtract(TiempoParcial).ToString() & "; ")

                TiempoParcial = Now
                PoblarRespuesta(ds, respuesta)

                spw = Nothing
                ds.Dispose()

                log.AppendLine("Tiempo de acceso a datos (Poblar objecto de respuesta): " & Now.Subtract(TiempoParcial).ToString() & "; ")

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                             MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

        End Sub

        Private Shared Function ArmarWrapper(identificadorLlamada As String, peticion As ContractoServicio.Contractos.Integracion.ReconfirmarPeriodos.Peticion) As SPWrapper
            Dim SP As String = String.Format("SAPR_PSERVICIO_{0}.sreconfirmar_periodos", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

#Region "Parametros de entrada"

            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada)

            'Arrays
            spw.AgregarParam("par$adevice_id", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aidentificador", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$atipo_periodo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)


            If peticion.Periodos IsNot Nothing AndAlso peticion.Periodos.Count > 0 Then
                For Each item In peticion.Periodos
                    spw.Param("par$adevice_id").AgregarValorArray(item.DeviceID)
                    spw.Param("par$aidentificador").AgregarValorArray(item.Identificador)
                    spw.Param("par$atipo_periodo").AgregarValorArray(item.CodigoTipoPeriodo)
                Next
            End If

            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.Configuracion.Usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, Util.GetCultureUser())
            spw.AgregarParam("par$cod_pais", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoPais, , False)


#End Region
#Region "Parametros de salida"
            spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
#End Region
            Return spw

        End Function

        Private Shared Sub PoblarRespuesta(ds As DataSet, ByRef respuesta As Respuesta)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                ' Validaciones
                If ds.Tables.Contains("validaciones") AndAlso ds.Tables("validaciones").Rows.Count > 0 Then

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ContractoServicio.Contractos.Integracion.Comon.Detalle)
                    For Each row As DataRow In ds.Tables("validaciones").Rows
                        respuesta.Resultado.Detalles.Add(New ContractoServicio.Contractos.Integracion.Comon.Detalle With {.Codigo = Util.AtribuirValorObj(row("CODIGO"), GetType(String)), .Descripcion = Util.AtribuirValorObj(row("MENSAJE"), GetType(String))})
                    Next
                    If respuesta.Resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Prosegur.Genesis.Comon.Enumeradores.Mensajes.Tipo.Error_Aplicacion) Then
                        'Ver si hay error de aplicacion 
                        Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Prosegur.Genesis.Comon.Enumeradores.Mensajes.Tipo.Error_Aplicacion,
                           Prosegur.Genesis.Comon.Enumeradores.Mensajes.Contexto.Integraciones,
                           Prosegur.Genesis.Comon.Enumeradores.Mensajes.Funcionalidad.ConfirmarPeriodos,
                           "0000", "", True)
                    ElseIf respuesta.Resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Prosegur.Genesis.Comon.Enumeradores.Mensajes.Tipo.Error_Negocio) Then
                        'Ver si hay error de negocio
                        Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Prosegur.Genesis.Comon.Enumeradores.Mensajes.Tipo.Error_Negocio,
                           Prosegur.Genesis.Comon.Enumeradores.Mensajes.Contexto.Integraciones,
                           Prosegur.Genesis.Comon.Enumeradores.Mensajes.Funcionalidad.ConfirmarPeriodos,
                           "0000", "", True)
                    End If
                Else
                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ContractoServicio.Contractos.Integracion.Comon.Detalle)

                    Dim unDetalle = New ContractoServicio.Contractos.Integracion.Comon.Detalle

                    Util.resultado(unDetalle.Codigo,
                           unDetalle.Descripcion,
                           Prosegur.Genesis.Comon.Enumeradores.Mensajes.Tipo.Exito,
                           Prosegur.Genesis.Comon.Enumeradores.Mensajes.Contexto.Integraciones,
                           Prosegur.Genesis.Comon.Enumeradores.Mensajes.Funcionalidad.ConfirmarPeriodos,
                           "0000", "", True)

                    respuesta.Resultado.Detalles.Add(unDetalle)
                End If

            End If
        End Sub


    End Class
End Namespace