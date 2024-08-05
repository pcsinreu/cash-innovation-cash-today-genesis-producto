Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon

Namespace Integracion
    Public Class AccionRecuperarMAEsPlanificadas

        Public Shared Function Ejecutar(Peticion As RecuperarMAEsPlanificadas.Peticion) As RecuperarMAEsPlanificadas.Respuesta

            Dim respuesta As New RecuperarMAEsPlanificadas.Respuesta

            Dim TiempoInicial As DateTime = Now
            Dim log As New StringBuilder
            Dim identificadorLlamada As String
            identificadorLlamada = String.Empty
            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                       respuesta.Resultado.Descripcion,
                       Tipo.Exito,
                       Contexto.Integraciones,
                       Funcionalidad.RecuperarMAEsPlanificadas,
                       "0000", "", True)
            Dim pais = Genesis.Pais.ObtenerPaisPorCodigo(Peticion.CodigoPais, Peticion.Configuracion.IdentificadorAjeno)
            Dim codigoPais = String.Empty
            If pais IsNot Nothing Then
                codigoPais = pais.Codigo
            End If
            Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, "RecuperarMAEsPlanificadas", identificadorLlamada)
            If identificadorLlamada IsNot Nothing Then
                Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, "RecuperarMAEsPlanificadas", Comon.Util.VersionCompleta, AccesoDatos.Util.ToXML(Peticion), codigoPais, AccesoDatos.Util.ToXML(Peticion).GetHashCode)
            End If
            If validarPeticion(Peticion, respuesta.Resultado) Then

                Try

                    If Peticion.Configuracion Is Nothing Then Peticion.Configuracion = New RecuperarMAEsPlanificadas.Configuracion()
                    If String.IsNullOrEmpty(Peticion.Configuracion.Usuario) Then Peticion.Configuracion.Usuario = "RECUPERAR_MAES_PLANIFICADAS"

                    Dim TiempoParcial As Date = Now
                    'Ejecutar AccesoDatos.Recuperar
                    TiempoParcial = Now
                    AccesoDatos.GenesisSaldos.Integracion.RecuperarMAEsPlanificadas.Recuperar(identificadorLlamada, Peticion, respuesta, log)
                    log.AppendLine("Tiempo de acceso a datos: " & Now.Subtract(TiempoParcial).ToString() & "; ")

                Catch ex As Excepcion.NegocioExcepcion

                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarMAEsPlanificadas,
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
                               Funcionalidad.RecuperarMAEsPlanificadas,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of Detalle)
                    Dim detalle As New Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                               detalle.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.RecuperarMAEsPlanificadas,
                               "0001", Util.RecuperarMensagemTratada(ex),
                               True)
                    respuesta.Resultado.Detalles.Add(detalle)

                    respuesta.Maquinas = Nothing

                End Try

            End If

            ' Tipo de respuesta

            ' Tiempo de ejecucion
            respuesta.Resultado.TiempoDeEjecucion = Now.Subtract(TiempoInicial).ToString()

            ' Graba en el LOG el tiempo total de la ejecucion del proceso
            log.AppendLine("Tiempo total: " & respuesta.Resultado.TiempoDeEjecucion & ";")

            If Peticion IsNot Nothing AndAlso Peticion.Configuracion IsNot Nothing AndAlso Peticion.Configuracion.LogDetallar Then
                ' Añadir el log en la respuesta del servicio
                TratarResultado(respuesta.Resultado, Peticion.Configuracion.LogDetallar)
                respuesta.Resultado.Log = log.ToString().Trim()
            Else
                TratarResultado(respuesta.Resultado, True)
            End If
            respuesta.Resultado.Tipo = respuesta.Resultado.Codigo.Substring(0, 1)

            'Logueo la respuesta
            If identificadorLlamada IsNot Nothing Then
                Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamada, AccesoDatos.Util.ToXML(respuesta), respuesta.Resultado.Codigo, respuesta.Resultado.Descripcion, AccesoDatos.Util.ToXML(respuesta).GetHashCode)
            End If

            Return respuesta

        End Function

        Private Shared Sub TratarResultado(resultado As Resultado, logDetallar As Boolean)

            If resultado.Detalles IsNot Nothing Then
                If resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Aplicacion) Then
                    AccesoDatos.Util.resultado(resultado.Codigo,
                             resultado.Descripcion,
                             Tipo.Error_Aplicacion,
                             Contexto.Integraciones,
                             Funcionalidad.RecuperarMAEsPlanificadas,
                             "0000", "",
                             True)
                ElseIf resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Negocio) Then
                    AccesoDatos.Util.resultado(resultado.Codigo,
                             resultado.Descripcion,
                             Tipo.Error_Negocio,
                             Contexto.Integraciones,
                             Funcionalidad.RecuperarMAEsPlanificadas,
                             "0000", "",
                             True)
                End If

            End If

            If Not logDetallar Then
                resultado.Detalles = Nothing
            End If

        End Sub

        Private Shared Function validarPeticion(ByVal peticion As RecuperarMAEsPlanificadas.Peticion,
                                                ByRef Resultado As Resultado) As Boolean


            If peticion Is Nothing Then
                If Resultado.Detalles Is Nothing Then Resultado.Detalles = New List(Of Contractos.Integracion.Comon.Detalle)
                Dim d As New Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarMAEsPlanificadas,
                           "0001", "", True)
                Resultado.Detalles.Add(d)
                Return False
            ElseIf Not Util.ValidarToken(peticion, Resultado) Then
                Return False
            End If


            ' /*2040300007 - NO INFORMA MAQUINA ALGUNA */
            If peticion.DeviceIDs Is Nothing OrElse peticion.DeviceIDs.Count = 0 Then
                If Resultado.Detalles Is Nothing Then Resultado.Detalles = New List(Of Contractos.Integracion.Comon.Detalle)
                Dim d As New Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarMAEsPlanificadas,
                           "0007", "", True)
                Resultado.Detalles.Add(d)
                Return False

            End If

            If Resultado.Detalles IsNot Nothing AndAlso Resultado.Detalles.Count > 0 Then
                Return False
            End If
            Return True
        End Function


    End Class
End Namespace

