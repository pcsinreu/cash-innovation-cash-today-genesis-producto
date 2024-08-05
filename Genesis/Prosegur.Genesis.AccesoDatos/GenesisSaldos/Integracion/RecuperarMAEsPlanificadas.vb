Imports Prosegur.DbHelper
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports Prosegur.Genesis.ContractoServicio
Imports System.Linq
Imports System.Text

Namespace GenesisSaldos.Integracion
    Public Class RecuperarMAEsPlanificadas
        Public Shared Sub Recuperar(identificadorLlamada As String, peticion As Contractos.Integracion.RecuperarMAEsPlanificadas.Peticion,
                    ByRef respuesta As Contractos.Integracion.RecuperarMAEsPlanificadas.Respuesta,
           Optional ByRef log As StringBuilder = Nothing)
            Try
                If log Is Nothing Then log = New StringBuilder
                Dim TiempoParcial As DateTime = Now

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                TiempoParcial = Now

                spw = ColectarPeticion(identificadorLlamada, peticion)
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

        Private Shared Function ColectarPeticion(identificadorLlamada As String, peticion As Contractos.Integracion.RecuperarMAEsPlanificadas.Peticion) As SPWrapper
            Dim SP As String = String.Format("SAPR_PPLANIFICACION_{0}.srecuperar_maes_planificadas", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)
            Dim index As Int16 = 0

            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada, , False)
            spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Descricao_Curta, peticion.Configuracion.IdentificadorAjeno, , False)
            spw.AgregarParam("par$cod_pais", ProsegurDbType.Descricao_Curta, peticion.CodigoPais, , False)
            spw.AgregarParam("par$cod_planificacion", ProsegurDbType.Descricao_Curta, peticion.CodigoPlanificacion, , False)
            spw.AgregarParam("par$acod_device_id", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$anel_index", ProsegurDbType.Inteiro_Curto, Nothing, , True)

            For Each deviceID As String In peticion.DeviceIDs
                spw.Param("par$acod_device_id").AgregarValorArray(deviceID)
                spw.Param("par$anel_index").AgregarValorArray(index)
                index += 1
            Next

            If peticion.FechaHora <> DateTime.MinValue Then
                spw.AgregarParam("par$cod_fecha", ProsegurDbType.Data_Hora, peticion.FechaHora, , False)
            Else
                spw.AgregarParam("par$cod_fecha", ProsegurDbType.Data_Hora, Nothing, , False)
            End If


            If peticion.FechaHora <> DateTime.MinValue AndAlso Not String.IsNullOrEmpty(peticion.FechaHora.ToString("%K")) Then

                Dim GMTHoraLocalCalculado As String = Convert.ToInt32(peticion.FechaHora.ToString("zzz").Split(":")(0))
                Dim GMTMinutoLocalCalculado As String = Convert.ToInt32(peticion.FechaHora.ToString("zzz").Split(":")(1))
                GMTMinutoLocalCalculado += GMTHoraLocalCalculado * 60

                spw.AgregarParam("par$nel_gmt_minuto", ProsegurDbType.Inteiro_Curto, GMTMinutoLocalCalculado, , False)

            Else
                spw.AgregarParam("par$nel_gmt_minuto", ProsegurDbType.Inteiro_Curto, DBNull.Value, , False)

            End If


            'If peticion.FechaHora <> DateTime.MinValue Then
            '    'spw.AgregarParam("par$cod_fecha", ProsegurDbType.Descricao_Curta, peticion.FechaHora.ToString("dd/MM/yyyy HH:mm:ss zzz"), , False)
            '    spw.AgregarParam("par$cod_fecha", ProsegurDbType.Descricao_Curta, peticion.FechaHora.ToString("dd-MM-yyyy"), , False)
            'Else
            '    spw.AgregarParam("par$cod_fecha", ProsegurDbType.Descricao_Curta, Nothing, , False)
            'End If

            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Curta, peticion.Configuracion.Usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Curta, Util.GetCultureUser())
            spw.AgregarParam("par$rc_maquinas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "maquinas")
            spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")

            Return spw

        End Function

        Private Shared Sub PoblarRespuesta(ds As DataSet, respuesta As Contractos.Integracion.RecuperarMAEsPlanificadas.Respuesta)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                Dim dtMaquinas As DataTable = ds.Tables("maquinas")
                Dim dtValidaciones As DataTable = ds.Tables("validaciones")
                Dim unaMaquina As Contractos.Integracion.RecuperarMAEsPlanificadas.Maquina


                If dtMaquinas IsNot Nothing AndAlso dtMaquinas.Rows.Count > 0 Then
                    If respuesta.Maquinas Is Nothing Then
                        respuesta.Maquinas = New List(Of Contractos.Integracion.RecuperarMAEsPlanificadas.Maquina)()
                    End If
                    For Each fila As DataRow In dtMaquinas.Rows
                        unaMaquina = New Contractos.Integracion.RecuperarMAEsPlanificadas.Maquina()

                        Util.AtribuirValorObjeto(unaMaquina.DeviceID, fila("COD_DEVICE_ID"), GetType(String))
                        Util.AtribuirValorObjeto(unaMaquina.Codigo, fila("CODIGO_RESULTADO"), GetType(String))
                        Util.AtribuirValorObjeto(unaMaquina.Resultado, fila("DESCRIPCION_RESULTADO"), GetType(String))

                        respuesta.Maquinas.Add(unaMaquina)
                    Next
                End If

                If dtValidaciones IsNot Nothing AndAlso dtValidaciones.Rows.Count > 0 Then
                    If respuesta.Resultado Is Nothing Then
                        respuesta.Resultado = New Contractos.Integracion.Comon.Resultado()
                    End If
                    If respuesta.Resultado.Detalles Is Nothing OrElse respuesta.Resultado.Detalles.Count = 0 Then
                        respuesta.Resultado.Detalles = New List(Of Contractos.Integracion.Comon.Detalle)()
                    End If
                    Dim objDetalle As Contractos.Integracion.Comon.Detalle
                    For Each fila As DataRow In dtValidaciones.Rows
                        objDetalle = New Contractos.Integracion.Comon.Detalle()

                        Util.AtribuirValorObjeto(objDetalle.Codigo, fila("CODIGO"), GetType(String))
                        Util.AtribuirValorObjeto(objDetalle.Descripcion, fila("DESCRIPCION"), GetType(String))

                        respuesta.Resultado.Detalles.Add(objDetalle)
                    Next
                End If

            End If
        End Sub
    End Class
End Namespace

