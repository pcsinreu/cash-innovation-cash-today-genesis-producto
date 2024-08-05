Imports System.Web.Services
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis
Imports Newtonsoft.Json

Partial Public MustInherit Class BaseCentralNotificacion
    Inherits System.Web.UI.Page

    <System.Web.Services.WebMethod()> _
    Public Shared Sub MarcarLeida(lido As Boolean, lstIdentificadoresDestino As String(), usuario As String)
        If lstIdentificadoresDestino IsNot Nothing AndAlso lstIdentificadoresDestino.Count > 0 Then
            Dim proxyComon As New Prosegur.Genesis.Comunicacion.ProxyComon
            Dim peticion As New Contractos.Genesis.Notificacion.GrabarNotificacionLeido.Peticion
            With peticion
                .identificadoresDestinoNotificacion = lstIdentificadoresDestino.ToList()
                .leido = lido
                .usuario = usuario
            End With
            Dim respuesta As Contractos.Genesis.Notificacion.GrabarNotificacionLeido.Respuesta
            respuesta = proxyComon.GrabarNotificacionLeido(peticion)
            If Not respuesta.exito Then
                Throw New Exception(respuesta.MensajeError)
            End If
        End If
    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Sub GrabarActualizacionAutomatica(actualizacionAutomatica As String)
        If Not String.IsNullOrEmpty(actualizacionAutomatica) Then
            Dim CodigoFuncionalidad = Prosegur.Genesis.Comon.Constantes.CODIGO_PREFERENCIAS_FUNCIONALIDAD_CENTRAL_NOTIFICACION
            Dim CodigoPropriedad = Prosegur.Genesis.Comon.Constantes.CODIGO_PREFERENCIAS_PROPRIEDAD_ACTUALIZACION_AUTOMATICA
            PreferenciasAplicacion.AtualizaPreferencia(CodigoFuncionalidad, CodigoPropriedad, actualizacionAutomatica)

            HttpContext.Current.Session("actAutomatica") = actualizacionAutomatica
        End If
    End Sub

    Public Shared Function BuscarNotificacionesLeida(leidas As Boolean, privacidadeLeida As Integer, desde As String, hasta As String) As ObservableCollection(Of Clases.CentralNotificacion.Notificacion)

        Dim desLogin As String = Nothing
        If HttpContext.Current.Session("desLogin") IsNot Nothing Then
            desLogin = HttpContext.Current.Session("desLogin")
            HttpContext.Current.Session("desLogin") = desLogin
        End If

        Dim codigosDelegacion As New List(Of String)
        If HttpContext.Current.Session("codigosDelegacion") IsNot Nothing Then
            codigosDelegacion = HttpContext.Current.Session("codigosDelegacion")
            HttpContext.Current.Session("codigosDelegacion") = codigosDelegacion
        End If

        Dim codigosPlanta As New List(Of String)
        If HttpContext.Current.Session("codigosPlanta") IsNot Nothing Then
            codigosPlanta = HttpContext.Current.Session("codigosPlanta")
            HttpContext.Current.Session("codigosPlanta") = codigosPlanta
        End If

        Dim codigosSector As New List(Of String)
        If HttpContext.Current.Session("codigosSector") IsNot Nothing Then
            codigosSector = HttpContext.Current.Session("codigosSector")
            HttpContext.Current.Session("codigosSector") = codigosSector
        End If

        Dim identificadoresTipoSector As New List(Of String)
        If HttpContext.Current.Session("identificadoresTipoSector") IsNot Nothing Then
            identificadoresTipoSector = HttpContext.Current.Session("identificadoresTipoSector")
            HttpContext.Current.Session("identificadoresTipoSector") = identificadoresTipoSector
        End If

        If codigosSector.Count > 0 Then

            Dim privado As Nullable(Of Boolean) = Nothing
            If leidas Then
                Dim objPrivado As Enumeradores.Privacidad = [Enum].Parse(GetType(Enumeradores.Privacidad), privacidadeLeida)
                If objPrivado = Enumeradores.Privacidad.Privado Then
                    privado = True
                ElseIf objPrivado = Enumeradores.Privacidad.NoPrivado Then
                    privado = False
                End If
            End If

            Dim peticion As New Contractos.Genesis.Notificacion.CargarNotificacion.Peticion
            With peticion
                .codigoAplicacion = Genesis.Comon.Enumeradores.Aplicacion.GenesisSaldos.RecuperarValor()
                .leidas = leidas
                .actualDelegacion = Base.InformacionUsuario.DelegacionSeleccionada

                If privado Is Nothing OrElse Not privado Then
                    If privado Is Nothing Then
                        .desLogin = desLogin
                    End If

                    .codigosDelegacion = New List(Of String)
                    'Add delegações que o usuario tem permissão
                    If codigosDelegacion IsNot Nothing Then
                        For Each delegacion In codigosDelegacion
                            .codigosDelegacion.Add(delegacion)
                        Next
                    End If

                    .codigosPlanta = New List(Of String)
                    'Add plantas que o usuario tem permissão
                    If codigosPlanta IsNot Nothing Then
                        For Each planta In codigosPlanta
                            .codigosPlanta.Add(planta)
                        Next
                    End If

                    .codigosSector = New List(Of String)
                    'Add setores que o usuario tem permissão
                    If codigosSector IsNot Nothing Then
                        For Each sector In codigosSector
                            .codigosSector.Add(sector)
                        Next
                    End If

                    .identificadoresTipoSector = New List(Of String)
                    'Add tipos de setores que o usuario tem permissão
                    If codigosSector IsNot Nothing Then
                        For Each tipoSector In codigosSector
                            .identificadoresTipoSector.Add(tipoSector)
                        Next
                    End If
                Else
                    .desLogin = desLogin
                End If

                If leidas Then
                    If Not String.IsNullOrEmpty(desde) Then
                        .desde = DateTime.Parse(desde)
                    End If
                    If Not String.IsNullOrEmpty(hasta) Then
                        .hasta = DateTime.Parse(hasta)
                    End If
                End If
            End With

            'Busca notificações
            Dim proxyComon As New Prosegur.Genesis.Comunicacion.ProxyComon
            Dim respuesta As Contractos.Genesis.Notificacion.CargarNotificacion.Respuesta
            respuesta = proxyComon.CargarNotificaciones(peticion)

            Return respuesta.notificaciones
        End If

        Return Nothing
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function BuscarNotificaciones(privacidadeLeida As Integer, desde As String, hasta As String) As String
        Dim respuestaJSON As New ContractoServicio.Comon.BaseRespuestaJSON
        respuestaJSON.Respuesta = New With
                                    {
                                        .NotificacionesLeidas = Nothing,
                                        .NotificacionesNoLeidas = Nothing
                                    }
        If HttpContext.Current.Session("codigosSector") IsNot Nothing Then

            Dim notificaciones = New ObservableCollection(Of Clases.CentralNotificacion.Notificacion)

            Dim lidas = BuscarNotificacionesLeida(True, privacidadeLeida, desde, hasta)
            Dim naoLidas = BuscarNotificacionesLeida(False, privacidadeLeida, desde, hasta)

            Dim lidasOrd = New ObservableCollection(Of Clases.CentralNotificacion.Notificacion)
            If lidas IsNot Nothing Then
                lidasOrd.AddRange(lidas.OrderByDescending(Function(a) a.FechaCreacion))
            End If

            Dim naoLidasOrd = New ObservableCollection(Of Clases.CentralNotificacion.Notificacion)
            If naoLidas IsNot Nothing Then
                naoLidasOrd.AddRange(naoLidas.OrderByDescending(Function(a) a.FechaCreacion))
            End If

            HttpContext.Current.Session("Notificaciones") = notificaciones

            If notificaciones IsNot Nothing Then

                respuestaJSON.Respuesta = New With
                                    {
                                        .NotificacionesLeidas = lidasOrd,
                                        .NotificacionesNoLeidas = naoLidasOrd
                                    }

            End If

        End If

        Return JsonConvert.SerializeObject(respuestaJSON, New Converters.StringEnumConverter())
    End Function
End Class