Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes

Namespace Integracion

    Public Class AccionRecuperarPlanificaciones

        Public Shared Function Ejecutar(peticion As RecuperarPlanificaciones.Peticion) As RecuperarPlanificaciones.Respuesta

            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder

            ' Inicializar obyecto de respuesta
            Dim respuesta As New RecuperarPlanificaciones.Respuesta
            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Exito,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarPlanificaciones,
                           "0000", "",
                           True)

            ' Validar campos obligatorios
            If validarPeticion(peticion, respuesta.Resultado) Then

                Try

                    If String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then peticion.Configuracion.Usuario = "SERVICIO_RECUPERAR_PLANIFICACIONES"

                    TiempoParcial = Now
                    AccesoDatos.Genesis.Planificacion.Recuperar(peticion, respuesta)
                    log.AppendLine("Tiempo de acceso a datos: " & Now.Subtract(TiempoParcial).ToString() & "; ")

                    ' Temporario - Retirar na PGP-275
                    ' Si não tem nenhum erro, mas não foi encontrado a planificacion, retornar um error
                    If (respuesta.Resultado.Detalles Is Nothing OrElse respuesta.Resultado.Detalles.Count = 0) AndAlso
                            (respuesta.Planificaciones Is Nothing OrElse respuesta.Planificaciones.Count = 0) Then

                        Dim detalle As New Detalle
                        detalle.Codigo = "2040030002"
                        detalle.Descripcion = AccesoDatos.Util.RecuperarMensajes("2040030002", Funcionalidad.RecuperarPlanificaciones.ToString.ToUpper(), String.Empty)
                        respuesta.Resultado.Detalles = New List(Of Detalle)
                        respuesta.Resultado.Detalles.Add(detalle)

                    End If

                    TratarResultado(respuesta.Resultado, peticion.Configuracion.LogDetallar)

                Catch ex As Excepcion.NegocioExcepcion

                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarPlanificaciones,
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
                               Funcionalidad.RecuperarPlanificaciones,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of Detalle)
                    Dim detalle As New Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                               detalle.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarPlanificaciones,
                               "0001", Util.RecuperarMensagemTratada(ex),
                               True)
                    respuesta.Resultado.Detalles.Add(detalle)

                    respuesta.Planificaciones = Nothing

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

        Private Shared Sub TratarResultado(resultado As Resultado, logDetallar As Boolean)

            If resultado.Detalles IsNot Nothing AndAlso resultado.Detalles.Count > 0 Then

                AccesoDatos.Util.resultado(resultado.Codigo,
                               resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarPlanificaciones,
                               "0000", "",
                               True)

            End If

            If Not logDetallar Then
                resultado.Detalles = Nothing
            End If

        End Sub

        Private Shared Function validarPeticion(peticion As RecuperarPlanificaciones.Peticion,
                                                ByRef Resultado As Resultado) As Boolean

            ' Validar el token
            If Util.ValidarToken(peticion, Resultado) Then

                If peticion.Codigos IsNot Nothing Then
                    peticion.Codigos.RemoveAll(Function(x) String.IsNullOrEmpty(x))
                End If

                If String.IsNullOrEmpty(peticion.CodigoBanco) AndAlso (peticion.Codigos Is Nothing OrElse peticion.Codigos.Count = 0) Then

                    AccesoDatos.Util.resultado(Resultado.Codigo,
                           Resultado.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarPlanificaciones,
                           "0000", "", True)

                    If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.LogDetallar Then
                        If Resultado.Detalles Is Nothing Then Resultado.Detalles = New List(Of Detalle)
                        Dim d As New Detalle
                        AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarPlanificaciones,
                           "0001", "", True)
                        Resultado.Detalles.Add(d)
                    End If

                    Return False

                End If

            Else

                AccesoDatos.Util.resultado(Resultado.Codigo,
                               Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarPlanificaciones,
                               "0000", "",
                               True)

                Return False

            End If

            Return True
        End Function

    End Class

End Namespace

