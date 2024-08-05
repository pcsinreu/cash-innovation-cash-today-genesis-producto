Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes

Namespace Integracion

    Public Class AccionRecuperarMAEs

        Public Shared Function Ejecutar(peticion As RecuperarMAEs.Peticion) As RecuperarMAEs.Respuesta

            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder
            Dim identificadorLlamada As String
            identificadorLlamada = String.Empty

            ' Inicializar obyecto de respuesta
            Dim respuesta As New RecuperarMAEs.Respuesta
            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Exito,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarMAEs,
                           "0000", "",
                           True)

            'Obtener Codigo Pais para generar oidLlamada
            Dim pais = Genesis.Pais.ObtenerPaisPorDefault("")
            Dim codigoPais As String = pais.Codigo

            Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, "RecuperarMAEs", identificadorLlamada)
            If identificadorLlamada IsNot Nothing Then
                Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, "RecuperarMAEs", Comon.Util.VersionCompleta, AccesoDatos.Util.ToXML(peticion), codigoPais, AccesoDatos.Util.ToXML(peticion).GetHashCode)
            End If

            ' Validar campos obligatorios
            If validarPeticion(peticion, respuesta.Resultado) Then

                Try

                    If String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then peticion.Configuracion.Usuario = "SERVICIO_RECUPERAR_MAES"

                    TiempoParcial = Now
                    AccesoDatos.Genesis.Maquina.Recuperar(identificadorLlamada, peticion, respuesta)
                    log.AppendLine("Tiempo de acceso a datos: " & Now.Subtract(TiempoParcial).ToString() & "; ")

                    ' Temporario - Retirar na PGP-275
                    ' Si não tem nenhum erro, mas não foi encontrado a maquina, retornar um error
                    If (respuesta.Resultado.Detalles Is Nothing OrElse respuesta.Resultado.Detalles.Count = 0) AndAlso
                            (respuesta.MAEs Is Nothing OrElse respuesta.MAEs.Count = 0) Then

                        Dim detalle As New Detalle
                        detalle.Codigo = "2040040001"
                        detalle.Descripcion = AccesoDatos.Util.RecuperarMensajes("2040040001", Funcionalidad.RecuperarMAEs.ToString.ToUpper(), String.Empty)
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
                               Funcionalidad.RecuperarMAEs,
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
                               Funcionalidad.RecuperarMAEs,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of Detalle)
                    Dim detalle As New Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                               detalle.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarMAEs,
                               "0001", Util.RecuperarMensagemTratada(ex),
                               True)
                    respuesta.Resultado.Detalles.Add(detalle)

                    respuesta.MAEs = Nothing

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

            'Logueo la respuesta
            If identificadorLlamada IsNot Nothing Then
                Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamada, AccesoDatos.Util.ToXML(respuesta), respuesta.Resultado.Codigo, respuesta.Resultado.Descripcion, AccesoDatos.Util.ToXML(respuesta).GetHashCode)
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
                               Funcionalidad.RecuperarMAEs,
                               "0000", "",
                               True)

            End If

            If Not logDetallar Then
                resultado.Detalles = Nothing
            End If

        End Sub

        Private Shared Function validarPeticion(Peticion As RecuperarMAEs.Peticion,
                                                ByRef Resultado As Resultado) As Boolean

            ' Validar el token
            If Not Util.ValidarToken(Peticion, Resultado) Then

                AccesoDatos.Util.resultado(Resultado.Codigo,
                               Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarMAEs,
                               "0000", "",
                               True)

                Return False
            End If

            Return True
        End Function

    End Class

End Namespace


