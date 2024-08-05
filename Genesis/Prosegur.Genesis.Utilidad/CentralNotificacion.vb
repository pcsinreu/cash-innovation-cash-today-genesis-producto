Imports Prosegur.Genesis.Comon
Imports System.Threading
Imports System.Threading.Tasks
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.ObtenerPreferencias
Imports Prosegur.Genesis.Comunicacion
Imports Prosegur.Genesis.Comon.Clases
Imports System.Text
Imports System.Windows.Forms
Imports Prosegur.Framework.Dicionario.Tradutor

Public Class CentralNotificacion


#Region "VARIÁVEIS"

    Private _segundosNotificacion As Integer = 30
    Private _threadBuscarCantidadNotificaciones As Thread

    Private _desLogin As String
    Private _codigosDelegaciones As List(Of String)
    Private _codigosPlantas As List(Of String)
    Private _identificadoresTipoSectores As List(Of String)
    Private _codigosSectores As List(Of String)
    Private _aplicacion As Comon.Enumeradores.CodigoAplicacion
    Private _esBuscarCodigos As Boolean
    Private _finalizarThreads As Boolean
    Private _autoResetEvent As New AutoResetEvent(False)

#End Region

#Region "[EVENTOS]"

    Public Delegate Sub EventoCantidadNotificacionesDelegate(cantidadNotificaciones As Integer)
    Public Event EventoCantidadNotificaciones As EventoCantidadNotificacionesDelegate

#End Region

#Region "CONSTRUTORES"

    Public Sub New()

    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="aplicaion"></param>
    ''' <param name="desLogin"></param>
    ''' <remarks></remarks>
    Public Sub New(aplicaion As Comon.Enumeradores.CodigoAplicacion, desLogin As String)

        Me._desLogin = desLogin
        Me._aplicacion = aplicaion
        Me.ConfigurarPreferenciaAutualizacionAutomatica()

    End Sub

#End Region

    Public Sub Notificar()

        Try
            If Not String.IsNullOrEmpty(Me._desLogin) Then

                'se todos os códigos estiverem vazios deve recupera-los e armazenar em memória, recuperar apenas uma vez
                If (Me._codigosDelegaciones Is Nothing OrElse Me._codigosDelegaciones.Count = 0) AndAlso
                    (Me._codigosPlantas Is Nothing OrElse Me._codigosPlantas.Count = 0) AndAlso
                    (Me._identificadoresTipoSectores Is Nothing OrElse Me._identificadoresTipoSectores.Count = 0) AndAlso
                    (Me._codigosSectores Is Nothing OrElse Me._codigosSectores.Count = 0) Then

                    _esBuscarCodigos = True

                End If

                _threadBuscarCantidadNotificaciones = New Thread(AddressOf BuscarNotificaciones)
                _threadBuscarCantidadNotificaciones.IsBackground = True
                _threadBuscarCantidadNotificaciones.Start()

            End If

        Catch ex As Exception
            LogarErroEventViewer(ex.Message)
            Throw ex
        End Try

    End Sub

    Public Sub ConfigurarPreferenciaAutualizacionAutomatica()

        Dim peticion As New ObtenerPreferenciasPeticion
        Dim respuesta As New ObtenerPreferenciasRespuesta
        Dim proxy As New ProxyPreferencias()

        peticion.codigoAplicacion = Me._aplicacion
        If Me._aplicacion = Enumeradores.CodigoAplicacion.SupervisorConteo Then
            peticion.codigoAplicacion = Enumeradores.CodigoAplicacion.Conteo
        End If
        peticion.CodigoFuncionalidad = Prosegur.Genesis.Comon.Constantes.CODIGO_PREFERENCIAS_FUNCIONALIDAD_CENTRAL_NOTIFICACION
        peticion.CodigoUsuario = _desLogin.ToUpper()

        respuesta = proxy.ObtenerPreferencias(peticion)

        If respuesta IsNot Nothing AndAlso respuesta.Preferencias IsNot Nothing AndAlso respuesta.Preferencias.Count > 0 Then
            Dim preferencia As PreferenciaUsuario

            preferencia = respuesta.Preferencias.Find(Function(p) p.CodigoPropriedad = Prosegur.Genesis.Comon.Constantes.CODIGO_PREFERENCIAS_PROPRIEDAD_ACTUALIZACION_AUTOMATICA)
            Me._segundosNotificacion = If(preferencia IsNot Nothing, preferencia.Valor, 30)
        End If

    End Sub

    Private Sub BuscarNotificaciones()

        Try

            Dim peticion As New ContractoServicio.Contractos.Genesis.Notificacion.ObtenerCantidadNotificaciones.Peticion
            Dim respuesta As ContractoServicio.Contractos.Genesis.Notificacion.ObtenerCantidadNotificaciones.Respuesta
            peticion.leidas = False
            peticion.obtenerIdentificadores = Me._esBuscarCodigos
            peticion.desLogin = Me._desLogin
            peticion.codigosDelegacion = Me._codigosDelegaciones
            peticion.codigosPlanta = Me._codigosPlantas
            peticion.identificadoresTipoSector = Me._identificadoresTipoSectores
            peticion.codigosSector = Me._codigosSectores
            peticion.codigoAplicacion = If(Me._aplicacion = Enumeradores.CodigoAplicacion.SupervisorConteo, Enumeradores.CodigoAplicacion.Conteo.RecuperarValor(), Me._aplicacion.RecuperarValor())

            Do

                respuesta = Prosegur.Genesis.Comunicacion.ClienteServicio.Comon.ObtenerCantidadNotificaciones(peticion)

                If Util.TratarRetornoServico(respuesta) Then

                    If Me._codigosDelegaciones Is Nothing Then
                        Me._codigosDelegaciones = respuesta.codigosDelegaciones
                        peticion.codigosDelegacion = Me._codigosDelegaciones
                    End If

                    If Me._codigosPlantas Is Nothing Then
                        Me._codigosPlantas = respuesta.codigosPlantas
                        peticion.codigosPlanta = Me._codigosPlantas
                    End If

                    If Me._identificadoresTipoSectores Is Nothing Then
                        Me._identificadoresTipoSectores = respuesta.identificadoresTipoSectores
                        peticion.identificadoresTipoSector = Me._identificadoresTipoSectores
                    End If

                    If Me._codigosSectores Is Nothing Then
                        Me._codigosSectores = respuesta.codigosSectores
                        peticion.codigosSector = Me._codigosSectores
                    End If

                    'Dispara o evento pra tela responsável por modificar o texto com a quantidade de notificações
                    RaiseEvent EventoCantidadNotificaciones(respuesta.cantidadNotificaciones)

                    'Seta o intervalo pra buscar novamente as notificações
                    _autoResetEvent.WaitOne(1000 * Me._segundosNotificacion)

                End If
                'depois que buscou as notificações seta como false
                'porque apenas a primeira vez irá buscar os códigos
                _esBuscarCodigos = False
                peticion.obtenerIdentificadores = Me._esBuscarCodigos


            Loop Until _finalizarThreads


        Catch ex As Exception
            Try

                LogarErroEventViewer(ex.Message)

            Catch ex1 As Exception
                ' exibir mensagem
                MessageBox.Show(ex.Message + "; " + ex1.Message, Traduzir("GENESIS_000_titulo_msgbox"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Try

    End Sub

    Private Shared Sub LogarErroEventViewer(msg As String)
        Try

            'Verifica se ja existe no Event Viewer uma entrada para armazenar as informações.
            If Not Diagnostics.EventLog.Exists(ContractoServicio.Constantes.NOME_LOG_EVENTOS) Then

                'Caso não exista cria uma.
                Diagnostics.EventLog.CreateEventSource(ContractoServicio.Constantes.NOME_LOG_EVENTOS, ContractoServicio.Constantes.NOME_LOG_EVENTOS)

            End If

            'Instancia objeto StringBuilder para configurar a mensagem a ser gravada
            Dim Mensagem As New StringBuilder()

            Mensagem.Append("Central de Notificaciones")
            Mensagem.Append(Environment.NewLine)
            Mensagem.Append("------------------------------------------------------------------------------------")
            Mensagem.Append(Environment.NewLine)
            Mensagem.Append("Exception: ")
            Mensagem.Append(msg)
            Mensagem.Append(Environment.NewLine)
            Mensagem.Append("------------------------------------------------------------------------------------")

            Diagnostics.EventLog.WriteEntry(ContractoServicio.Constantes.NOME_LOG_EVENTOS, Mensagem.ToString(), EventLogEntryType.Error)

        Catch ex As Exception
            'Tenta gravar no registro
        End Try
    End Sub

    Public Sub Close()
        If _threadBuscarCantidadNotificaciones IsNot Nothing AndAlso _threadBuscarCantidadNotificaciones.IsAlive Then
            _finalizarThreads = True
            _autoResetEvent.Set()
            _threadBuscarCantidadNotificaciones.Join()
        End If
    End Sub

End Class

