Imports System.Text
Imports Prosegur.Genesis.ContractoServicio.Contractos.Notification
Imports Prosegur.Genesis.LogicaNegocio.Notificacion

Namespace Notification
    Public Class AccionRegistrarEventoNilo
        Public Shared Function Ejecutar(peticion As Nilo.Request) As Nilo.Response
            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder

            ' Inicializo el objeto de respuesta
            Dim respuesta As New Nilo.Response With {.StatusCode = "200", .StatusDescription = "Success"}
            Dim identificadorLlamada = String.Empty

            ' Validar campos obligatorios
            If ValidarPeticion(peticion, respuesta) Then
                'Logueo la peticion de entrada
                Dim pais = Genesis.Pais.ObtenerPaisPorDefault(peticion.Context.Country)
                Dim codigoPais = String.Empty
                If pais IsNot Nothing Then
                    codigoPais = pais.Codigo
                End If
                Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, "OnlineNotificationEventOp", identificadorLlamada)
                If Not String.IsNullOrWhiteSpace(identificadorLlamada) Then
                    Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, "OnlineNotificationEventOp", Comon.Util.VersionCompleta, AccesoDatos.Util.ToXML(peticion), codigoPais, AccesoDatos.Util.ToXML(peticion).GetHashCode)
                End If
                If peticion.Integration = "cash-service-agreement" Then
                    respuesta = AccionAcuerdoServicio.Ejecutar(peticion, identificadorLlamada)
                    If peticion.Source = "nilo" Then
                        'Llamar a delivered-messages
                        AccionDeliveredMessages.Ejecutar(peticion, respuesta, identificadorLlamada)
                    End If
                End If

            End If

            'Logueo la respuesta
            If Not String.IsNullOrWhiteSpace(identificadorLlamada) Then
                Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamada, AccesoDatos.Util.ToXML(respuesta), respuesta.StatusCode, respuesta.StatusDescription, AccesoDatos.Util.ToXML(respuesta).GetHashCode)
            End If

            Return respuesta
        End Function


        Private Shared Function ValidarPeticion(ByRef request As Nilo.Request, ByRef response As Nilo.Response) As Boolean
            Dim resp As Boolean = True
            If request Is Nothing OrElse request.Context Is Nothing OrElse String.IsNullOrWhiteSpace(request.Context.Country) Then
                resp = False
                response.StatusCode = "400"
                response.StatusDescription = "Bad request"
            End If

            Return resp
        End Function

    End Class
End Namespace