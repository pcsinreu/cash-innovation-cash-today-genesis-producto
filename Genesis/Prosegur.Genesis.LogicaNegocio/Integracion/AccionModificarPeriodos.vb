Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports System.Text
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.ModificarPeriodos
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes

Namespace Integracion
    Public Class AccionModificarPeriodos

        Public Shared Function Ejecutar(peticion As ModificarPeriodos.Peticion) As ModificarPeriodos.Respuesta
            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder

            ' Inicializar obyecto de respuesta
            Dim respuesta As New ModificarPeriodos.Respuesta
            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Exito,
                           Contexto.Integraciones,
                           Funcionalidad.ModificarPeriodos,
                           "0000", "",
                           True)

            ' Validar campos obligatorios
            If ValidarPeticion(peticion, respuesta.Resultado) Then

                Try

                    If String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then peticion.Configuracion.Usuario = "SERVICIO_MODIFICAR_PERIODOS"

                    TiempoParcial = Now
                    AccesoDatos.GenesisSaldos.Periodos.ModificarPeriodos(peticion, respuesta)
                    log.AppendLine("Tiempo de acceso a datos: " & Now.Subtract(TiempoParcial).ToString() & ";")
                    TratarResultado(respuesta, peticion.Configuracion.LogDetallar)

                Catch ex As Excepcion.NegocioExcepcion

                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.ModificarPeriodos,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of Detalle)
                    respuesta.Resultado.Detalles.Add(New Detalle With {.Codigo = ex.Codigo, .Descripcion = ex.Descricao})

                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.ModificarPeriodos,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of Detalle)
                    Dim detalle As New Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                               detalle.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.ModificarPeriodos,
                               "0001", Util.RecuperarMensagemTratada(ex),
                               True)
                    respuesta.Resultado.Detalles.Add(detalle)

                End Try

            End If

            ' Tipo de respuesta
            respuesta.Resultado.Tipo = respuesta.Resultado.Codigo.Substring(0, 1)

            ' Tiempo de ejecucion
            respuesta.Resultado.TiempoDeEjecucion = Now.Subtract(TiempoInicial).ToString()

            ' Graba en el LOG el tiempo total de la ejecucion del proceso
            log.AppendLine("Tiempo total: " & respuesta.Resultado.TiempoDeEjecucion & ";")

            If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.LogDetallar Then
                ' Añadir el log en la respuesta del servicio
                respuesta.Resultado.Log = log.ToString().Trim()
            End If

            ' Respuesta
            Return respuesta

        End Function


        Private Shared Sub TratarResultado(respuesta As Respuesta, logDetallar As Boolean)

            If respuesta.Resultado.Detalles IsNot Nothing AndAlso respuesta.Resultado.Detalles.Count > 0 Then

                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.ModificarPeriodos,
                               "0000", "",
                               True)

            End If

            If Not logDetallar Then
                respuesta.Resultado.Detalles = Nothing
            End If

        End Sub

        Private Shared Function ValidarPeticion(ByRef peticion As ModificarPeriodos.Peticion, ByRef resultado As Resultado) As Boolean

            Dim resp As Boolean = True

            ' Validar obyecto peticion y valor del token
            If Util.ValidarToken(peticion, resultado) Then


                '' Es obligatorio informar los movimientos
                If peticion.Periodos Is Nothing OrElse peticion.Periodos.Count = 0 Then

                    AccesoDatos.Util.resultado(resultado.Codigo,
                           resultado.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.ModificarPeriodos,
                           "0000", "", True)

                    If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.LogDetallar Then
                        If resultado.Detalles Is Nothing Then resultado.Detalles = New List(Of Detalle)
                        Dim d As New Detalle
                        AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.ModificarPeriodos,
                           "0001", "", True)
                        resultado.Detalles.Add(d)
                    End If

                    resp = False

                End If

                ' Como el ENUM tiene valores NO POR DEFECTO, ya que empieza de 1, 2 y 3... si su valor es 0 es porque no se cargo
                If peticion.Accion = 0 Then
                    AccesoDatos.Util.resultado(resultado.Codigo,
                           resultado.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.ModificarPeriodos,
                           "0000", "", True)

                    If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.LogDetallar Then
                        If resultado.Detalles Is Nothing Then resultado.Detalles = New List(Of Detalle)
                        Dim d As New Detalle
                        AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.ModificarPeriodos,
                           "0002", "", True)
                        resultado.Detalles.Add(d)
                    End If

                    resp = False
                End If

            Else
                'No valida Token
                resp = False
            End If

            Return resp
        End Function

    End Class

End Namespace