Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Parametro
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Threading.Tasks
Imports System.Text
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis

Namespace Genesis

    Public Class Parametros

        Public Shared Function obtenerParametros(peticion As obtenerParametros.Peticion) As obtenerParametros.Respuesta

            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim log As New StringBuilder

            ' Inicializa objeto de respuesta
            Dim respuesta As New obtenerParametros.Respuesta
            respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_INTEGRACION_SEM_ERRO
            respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_INTEGRACION_SEM_ERRO
            respuesta.ValidacionesError = New List(Of ValidacionError)

            Try

                ' Validar el token
                validar(peticion)

                respuesta.parametros = AccesoDatos.Genesis.Parametros.obtenerParametros_v2(peticion)

            Catch ex As Excepcion.NegocioExcepcion

                respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_ERROR_INTEGRACION_FUNCIONAL
                respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_ERRO_INTEGRACION_FUNCIONAL

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)

                If ex.Message.ToUpper() = "TIMEOUT" Then
                    ' Respuesta defecto para Excepciones de Infraestructura
                    respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_ERROR_INTEGRACION_INFRAESTRUCTURA
                    respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_ERRO_INTEGRACION_INFRAESTRUCTURA

                Else

                    ' Respuesta defecto para Excepciones de Aplicaciones
                    respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_ERROR_INTEGRACION_APLICACION
                    respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_ERRO_INTEGRACION_APLICACION

                End If

                If peticion.validarPostError AndAlso ex.Message <> "ValidacionesError" Then
                    respuesta.ValidacionesError.Add(New ValidacionError With {.codigo = "VAL000", .descripcion = ex.ToString})
                End If

                If Not peticion.validarPostError Then
                    respuesta.ValidacionesError = Nothing
                End If

            End Try

            ' Graba en el LOG el tiempo total de la ejecucion del proceso
            log.AppendLine("Tiempo total: " & Now.Subtract(TiempoInicial).ToString() & vbNewLine & "; ")

            ' Añadir el log en la respuesta del servicio
            respuesta.TiempoDeEjecucion = log.ToString()

            ' Respuesta
            Return respuesta

        End Function

        Private Shared Sub validar(peticion As obtenerParametros.Peticion)

            Util.ValidarCampoObrigatorio(peticion.codigoDelegacion, "033_codigo_delegacion", GetType(String), False, True)
            If peticion.codigosParametro IsNot Nothing AndAlso peticion.codigosParametro.Count > 0 Then
                Util.ValidarCampoObrigatorio(peticion.codigoAplicacion, "033_codigo_aplicacion", GetType(String), False, True)
            End If

        End Sub

    End Class

End Namespace

