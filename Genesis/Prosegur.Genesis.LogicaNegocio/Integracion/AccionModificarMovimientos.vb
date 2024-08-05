Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.ModificarMovimientos
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon

Namespace Integracion
    Public Class AccionModificarMovimientos
        Public Shared Function Ejecutar(peticion As Peticion) As Respuesta

            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder

            ' Inicializar obyecto de respuesta
            Dim respuesta As New Respuesta

            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Exito,
                           Contexto.Integraciones,
                           Funcionalidad.RecuperarMovimientos,
                           "0000", "",
                           True)

            'Logueo la peticion de entrada
            Dim identificadorLlamada As String
            identificadorLlamada = String.Empty
            Dim pais = Genesis.Pais.ObtenerPaisPorDefault(String.Empty)
            Dim codigoPais = String.Empty
            If pais IsNot Nothing Then
                codigoPais = pais.Codigo
            End If
            Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, "ModificarMovimientos", identificadorLlamada)
            If Not String.IsNullOrWhiteSpace(identificadorLlamada) Then
                Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, "ModificarMovimientos", Comon.Util.VersionCompleta, AccesoDatos.Util.ToXML(peticion), codigoPais, AccesoDatos.Util.ToXML(peticion).GetHashCode)
            End If


            Try
                If ValidarPeticion(peticion, respuesta) Then

                    AccesoDatos.Integracion.ModificarMovimientos.ActualizarExtradata(identificadorLlamada, peticion.Movimientos, peticion, respuesta)
                    log.AppendLine("Tiempo de acceso a datos: " & Now.Subtract(TiempoParcial).ToString() & "; ")


                End If
            Catch ex As Excepcion.NegocioExcepcion

                ' Respuesta defecto para Excepciones de Negocio
                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.ModificarMovimientos,
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
                                   Funcionalidad.ModificarMovimientos,
                                   "0000", "",
                                   True)

                If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of Detalle)
                Dim detalle As New Detalle
                AccesoDatos.Util.resultado(detalle.Codigo,
                                   detalle.Descripcion,
                                   Tipo.Error_Aplicacion,
                                   Contexto.Integraciones,
                                   Funcionalidad.ModificarMovimientos,
                                   "0001", Util.RecuperarMensagemTratada(ex),
                                   True)
                respuesta.Resultado.Detalles.Add(detalle)
            End Try


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
        Private Shared Function ValidarPeticion(peticion As Peticion,
                                             ByRef respuesta As Respuesta) As Boolean
            Dim resp As Boolean = False
            'Validacion de token
            If Util.ValidarToken(peticion, respuesta.Resultado) Then

                    If String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then peticion.Configuracion.Usuario = "SERVICIO_MODIFICAR_MOVIMIENTOS"

                    respuesta.Resultado.Detalles = New List(Of Detalle)

                    For Each movimiento In peticion.Movimientos

                    '2040170001 Es obligatorio informar el campo "Codigo Externo".
                    If movimiento.CodigoExterno IsNot Nothing AndAlso Not String.IsNullOrEmpty(movimiento.CodigoExterno) Then

                        '2040170002 Es obligatorio informar el campo "CampoExtra "..
                        If movimiento.CamposExtra IsNot Nothing AndAlso movimiento.CamposExtra.Count > 0 Then
                            For Each extradata In movimiento.CamposExtra

                                '2040170003 Es obligatorio informar el campo "Campo".
                                If extradata.Campo IsNot Nothing AndAlso Not String.IsNullOrEmpty(extradata.Campo) Then

                                    '2040170004 Error Es obligatorio informar el campo "Valor"
                                    If extradata.Valor IsNot Nothing Then
                                        If extradata.Valor = String.Empty Then
                                            extradata.Valor = " "
                                        End If
                                        resp = True
                                    Else
                                        '2040170004 Error Es obligatorio informar el campo "Valor"
                                        resp = False
                                        Dim oDetalle As New Detalle
                                        AccesoDatos.Util.resultado(oDetalle.Codigo,
                                            oDetalle.Descripcion,
                                            Tipo.Error_Negocio,
                                            Contexto.Integraciones,
                                            Funcionalidad.ModificarMovimientos,
                                            "0004", "", True)
                                        respuesta.Resultado.Detalles.Add(oDetalle)
                                    End If
                                Else
                                    '2040170003 Error Es obligatorio informar el campo "Campo".
                                    resp = False
                                    Dim oDetalle As New Detalle
                                    AccesoDatos.Util.resultado(oDetalle.Codigo,
                                        oDetalle.Descripcion,
                                        Tipo.Error_Negocio,
                                        Contexto.Integraciones,
                                        Funcionalidad.ModificarMovimientos,
                                        "0003", "", True)
                                    respuesta.Resultado.Detalles.Add(oDetalle)

                                End If
                            Next
                        Else
                            '2040170002 error Es obligatorio informar el campo "CampoExtra "..
                            resp = False
                            Dim oDetalle As New Detalle
                            AccesoDatos.Util.resultado(oDetalle.Codigo,
                                            oDetalle.Descripcion,
                                            Tipo.Error_Negocio,
                                            Contexto.Integraciones,
                                            Funcionalidad.ModificarMovimientos,
                                            "0002", "", True)
                            respuesta.Resultado.Detalles.Add(oDetalle)
                        End If


                    Else
                        '2040170001 Error Es obligatorio informar el campo "Codigo Externo".
                        resp = False
                        Dim oDetalle As New Detalle
                        AccesoDatos.Util.resultado(oDetalle.Codigo,
                                      oDetalle.Descripcion,
                                      Tipo.Error_Negocio,
                                      Contexto.Integraciones,
                                      Funcionalidad.ModificarMovimientos,
                                      "0001", "", True)
                        respuesta.Resultado.Detalles.Add(oDetalle)
                    End If

                Next
            End If

            If Not resp Then
                resp = False
                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                respuesta.Resultado.Descripcion,
                Tipo.Error_Negocio,
                Contexto.Integraciones,
                Funcionalidad.ModificarMovimientos,
                "0000", "", True)
            End If

            Return resp

        End Function

    End Class
End Namespace