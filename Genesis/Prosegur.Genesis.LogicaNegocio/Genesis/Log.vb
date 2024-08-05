Imports Prosegur.Global.Saldos.ContractoServicio
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comunicacion
Imports System.Configuration
Imports Prosegur.Genesis.ContractoServicio
Imports System.IO

Namespace Genesis

    ''' <summary>
    ''' Clase Log
    ''' </summary>
    ''' <remarks></remarks>
    ''' [victor.ramos] 26/03/2014 - Criado
    Public Class Log

        Public Shared Sub GuardarLogExecucao(erro As String,
                                             codigoUsuario As String,
                                             delegacion As String,
                                             aplicacion As Comon.Enumeradores.Aplicacion,
                                    Optional puesto As String = Nothing)

            Try
                ' Se o objeto não está vazio
                If erro IsNot Nothing Then

                    ' Grava o Erro no banco
                    AccesoDatos.Genesis.Log.GuardarLogExecucao(erro.ToString, codigoUsuario, delegacion, aplicacion, puesto)

                End If
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                ' caso o banco não esteja online, gravar no event viewer
                GravarErroEventViewer(erro.ToString)
            End Try

        End Sub

        Public Shared Sub GenerarIdentificador(nombreRecurso As String,
                                              ByRef identificadorLlamada As String)
            'Logueo 
            Dim pais = Genesis.Pais.ObtenerPaisPorDefault("")
            Dim codPais As String = pais.Codigo

            Logeo.Log.Movimiento.Logger.GenerarIdentificador(codPais, nombreRecurso, identificadorLlamada)
        End Sub

        Public Shared Sub AgregaDetalle(identificadorLlamada As String,
                                        origen As String,
                                        descripcion As String,
                                        identificador As String)

            If Not String.IsNullOrWhiteSpace(identificadorLlamada) Then
                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                      origen,
                                                      Comon.Util.VersionCompleta.ToString(),
                                                      descripcion,
                                                      identificador)
            End If
        End Sub
        Public Shared Sub Iniciar(nombreRecurso As String,
                                  objJson As String,
                                  identificadorLlamada As String)
            'Logueo 
            Dim pais = Genesis.Pais.ObtenerPaisPorDefault("")
            Dim codPais As String = pais.Codigo

            If Not String.IsNullOrWhiteSpace(identificadorLlamada) Then
                Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, nombreRecurso, Comon.Util.VersionCompleta, objJson, codPais, objJson.GetHashCode)
            End If
        End Sub

        Public Shared Sub Finalizar(objRespuesta As Object,
                                  identificadorLlamada As String)

            Dim respuestaJson = Newtonsoft.Json.JsonConvert.SerializeObject(objRespuesta)
            Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamada, respuestaJson, objRespuesta.Codigo, objRespuesta.Descripcion, respuestaJson.GetHashCode)
        End Sub

        Private Shared Sub GravarErroEventViewer(DescricaoErro As String)

            Try

                ' criar objeto
                Dim objEventLog As New EventLog

                'Register the Application as an Event Source
                If Not EventLog.SourceExists(Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS) Then
                    EventLog.CreateEventSource(Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS, Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS)
                End If

                'log the entry
                objEventLog.Source = Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS
                objEventLog.WriteEntry(DescricaoErro, EventLogEntryType.Error)

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)

                'Verifica se a pasta de logs dos erros existe.
                Dim caminhoLog As String = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath) & "\\logs\\"
                If Not Directory.Exists(caminhoLog) Then

                    'Se não existir a pasta é criada.
                    Directory.CreateDirectory(caminhoLog)

                End If

                ' gravar log de erro na pasta logs do site
                Dim logFile As New StreamWriter(System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath) & "/logs/" & DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss").ToString() & ".txt", True)
                logFile.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") & " - " & DescricaoErro)
                logFile.Close()

            End Try

        End Sub

    End Class
End Namespace
