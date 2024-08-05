Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Configuration

Namespace Genesis

    Public Class Notificacion

        Public Shared Function CargarNotificaciones(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.CargarNotificacion.Peticion) As ContractoServicio.Contractos.Genesis.Notificacion.CargarNotificacion.Respuesta

            Dim objRespuesta As New ContractoServicio.Contractos.Genesis.Notificacion.CargarNotificacion.Respuesta

            Try

                ValidarParametroCargarNotificaciones(Peticion)

                If Peticion.obtenerIdentificadores Then
                    ObtenerIdentificadores(Peticion.desLogin, Peticion.codigosDelegacion,
                                           Peticion.codigosPlanta, Peticion.identificadoresTipoSector, Peticion.codigosSector)

                    objRespuesta.codigosDelegaciones = Peticion.codigosDelegacion
                    objRespuesta.codigosPlantas = Peticion.codigosPlanta
                    objRespuesta.identificadoresTipoSectores = Peticion.identificadoresTipoSector
                    objRespuesta.codigosSectores = Peticion.codigosSector

                End If

                Dim dtRetorno = AccesoDatos.Genesis.Notificacion.CargarNotificaciones(Peticion.desde,
                                                                            Peticion.hasta,
                                                                            Peticion.codigoAplicacion,
                                                                            Peticion.desLogin,
                                                                            Peticion.codigosSector,
                                                                            Peticion.codigosPlanta,
                                                                            Peticion.codigosDelegacion,
                                                                            Peticion.identificadoresTipoSector,
                                                                            Peticion.leidas)


                objRespuesta.notificaciones = PreecheCargarNotificaciones(dtRetorno, Peticion.actualDelegacion)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = String.Empty

            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.CodigoError = ex.Codigo
                objRespuesta.MensajeError = ex.Descricao
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString
            End Try

            Return objRespuesta

        End Function
        Public Shared Function ObtenerCantidadNotificaciones(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.ObtenerCantidadNotificaciones.Peticion) As ContractoServicio.Contractos.Genesis.Notificacion.ObtenerCantidadNotificaciones.Respuesta

            Dim objRespuesta As New ContractoServicio.Contractos.Genesis.Notificacion.ObtenerCantidadNotificaciones.Respuesta

            Try

                ValidarParametrosObtenerCantidadNotificaciones(Peticion)

                If Peticion.obtenerIdentificadores Then
                    ObtenerIdentificadores(Peticion.desLogin, Peticion.codigosDelegacion,
                                           Peticion.codigosPlanta, Peticion.identificadoresTipoSector, Peticion.codigosSector)

                    objRespuesta.codigosDelegaciones = Peticion.codigosDelegacion
                    objRespuesta.codigosPlantas = Peticion.codigosPlanta
                    objRespuesta.identificadoresTipoSectores = Peticion.identificadoresTipoSector
                    objRespuesta.codigosSectores = Peticion.codigosSector

                End If

                objRespuesta.cantidadNotificaciones = AccesoDatos.Genesis.Notificacion.ObtenerCantidadNotificaciones(Peticion.desde,
                                                                            Peticion.hasta,
                                                                            Peticion.codigoAplicacion,
                                                                            Peticion.desLogin,
                                                                            Peticion.codigosSector,
                                                                            Peticion.codigosPlanta,
                                                                            Peticion.codigosDelegacion,
                                                                            Peticion.identificadoresTipoSector,
                                                                            Peticion.leidas)

                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = String.Empty

            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.CodigoError = ex.Codigo
                objRespuesta.MensajeError = ex.Descricao
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString
            End Try

            Return objRespuesta

        End Function
        Private Shared Sub ValidarParametroCargarNotificaciones(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.CargarNotificacion.Peticion)

            If String.IsNullOrEmpty(Peticion.codigoAplicacion) Then
                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(String.Format(Tradutor.Traduzir("gen_srv_msg_id_obligatorio"), "codigoAplicacion"))
            End If

            If String.IsNullOrEmpty(Peticion.desLogin) AndAlso
                (Peticion.codigosDelegacion Is Nothing OrElse Peticion.codigosDelegacion.Count = 0) AndAlso
                (Peticion.codigosPlanta Is Nothing OrElse Peticion.codigosPlanta.Count = 0) AndAlso
                (Peticion.identificadoresTipoSector Is Nothing OrElse Peticion.identificadoresTipoSector.Count = 0) AndAlso
                (Peticion.codigosSector Is Nothing OrElse Peticion.codigosSector.Count = 0) Then

                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(String.Format(Tradutor.Traduzir("gen_srv_msg_id_obligatorio"), "(desLogin,codigosDelegacion,codigosPlanta,identificadoresTipoSector,codigosSector)"))

            End If

        End Sub

        Private Shared Sub ValidarParametrosObtenerCantidadNotificaciones(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.ObtenerCantidadNotificaciones.Peticion)

            If String.IsNullOrEmpty(Peticion.codigoAplicacion) Then
                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(String.Format(Tradutor.Traduzir("gen_srv_msg_id_obligatorio"), "codigoAplicacion"))
            End If

            If String.IsNullOrEmpty(Peticion.desLogin) AndAlso
                (Peticion.codigosDelegacion Is Nothing OrElse Peticion.codigosDelegacion.Count = 0) AndAlso
                (Peticion.codigosPlanta Is Nothing OrElse Peticion.codigosPlanta.Count = 0) AndAlso
                (Peticion.identificadoresTipoSector Is Nothing OrElse Peticion.identificadoresTipoSector.Count = 0) AndAlso
                (Peticion.codigosSector Is Nothing OrElse Peticion.codigosSector.Count = 0) Then

                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(String.Format(Tradutor.Traduzir("gen_srv_msg_id_obligatorio"), "(desLogin,codigosDelegacion,codigosPlanta,identificadoresTipoSector,codigosSector)"))

            End If

        End Sub

        Private Shared Sub ValidarParametroObtenerIdentificadores(desLogin As String)

            If String.IsNullOrEmpty(desLogin) Then
                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(String.Format(Tradutor.Traduzir("gen_srv_msg_id_obligatorio"), "(desLogin)"))
            End If

        End Sub

        Private Shared Sub PreencheObtenerIdentificadores(dtRetorno As DataTable,
                                                        ByRef codigosDelegaciones As List(Of String),
                                                        ByRef codigosPlantas As List(Of String),
                                                        ByRef identificadoresTiposSectores As List(Of String),
                                                        ByRef codigosSectores As List(Of String))

            If dtRetorno IsNot Nothing AndAlso dtRetorno.Rows.Count > 0 Then

                codigosDelegaciones = (From row In dtRetorno.AsEnumerable()
                           Where (row.Field(Of String)("COD_DELEGACION") IsNot Nothing AndAlso Not String.IsNullOrEmpty(row.Field(Of String)("COD_DELEGACION")))
               Select codigoDelegacion = row.Field(Of String)("COD_DELEGACION")
               ).Distinct().ToList()

                codigosPlantas = (From row In dtRetorno.AsEnumerable()
                           Where (row.Field(Of String)("COD_PLANTA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(row.Field(Of String)("COD_PLANTA")))
               Select identificadorPlanta = row.Field(Of String)("COD_PLANTA")
               ).Distinct().ToList()


                identificadoresTiposSectores = (From row In dtRetorno.AsEnumerable()
                           Where (row.Field(Of String)("OID_PLANTA_TIPO_SECTOR") IsNot Nothing AndAlso Not String.IsNullOrEmpty(row.Field(Of String)("OID_PLANTA_TIPO_SECTOR")))
               Select identificadorTipoSector = row.Field(Of String)("OID_PLANTA_TIPO_SECTOR")
               ).Distinct().ToList()

                codigosSectores = (From row In dtRetorno.AsEnumerable()
                           Where (row.Field(Of String)("COD_SECTOR") IsNot Nothing AndAlso Not String.IsNullOrEmpty(row.Field(Of String)("COD_SECTOR")))
               Select identificadorSector = row.Field(Of String)("COD_SECTOR")
               ).Distinct().ToList()

            End If

        End Sub

        ''' <summary>
        ''' Busca os identificadores de delegações,plantas, tipos setores e setores
        ''' necessários para identificar as privacidades das notificações
        ''' carregar apenas uma vez e guardar em memória
        ''' </summary>
        ''' <remarks></remarks>
        Private Shared Sub ObtenerIdentificadores(desLogin As String,
                                                       ByRef codigosDelegaciones As List(Of String),
                                                       ByRef codigosPlantas As List(Of String),
                                                       ByRef identificadoresTiposSectores As List(Of String),
                                                       ByRef codigosSectores As List(Of String))

            ValidarParametroObtenerIdentificadores(desLogin)

            PreencheObtenerIdentificadores(AccesoDatos.Genesis.Notificacion.ObtenerIdentificadores(desLogin),
                                           codigosDelegaciones, codigosPlantas, identificadoresTiposSectores, codigosSectores)

        End Sub

        Private Shared Function PreecheCargarNotificaciones(dtRetorno As Data.DataTable, delegation As Clases.Delegacion) As ObservableCollection(Of Clases.CentralNotificacion.Notificacion)

            Dim lstNotificacion As New ObservableCollection(Of Clases.CentralNotificacion.Notificacion)

            If dtRetorno IsNot Nothing AndAlso dtRetorno.Rows.Count > 0 Then
                For Each dtRow As Data.DataRow In dtRetorno.Rows
                    Dim oidNotificacion As String = Util.AtribuirValorObj(dtRow("OID_NOTIFICACION"), GetType(String))
                    Dim objNotificacion As Clases.CentralNotificacion.Notificacion = lstNotificacion.FirstOrDefault(Function(a) a.Identificador = oidNotificacion)
                    If objNotificacion Is Nothing Then
                        objNotificacion = New Clases.CentralNotificacion.Notificacion
                        With objNotificacion
                            .Identificador = oidNotificacion
                            .BolActivo = Util.AtribuirValorObj(dtRow("BOL_ACTIVO"), GetType(Boolean))
                            .FechaCreacion = Util.AtribuirValorObj(dtRow("GMT_CREACION"), GetType(DateTime))
                            .FechaCreacion = .FechaCreacion.QuieroExibirEstaFechaEnLaPatalla(delegation)
                            .FechaModificacion = Util.AtribuirValorObj(dtRow("GMT_MODIFICACION"), GetType(DateTime))
                            .FechaModificacion = .FechaModificacion.QuieroExibirEstaFechaEnLaPatalla(delegation)
                            .ObservacionNotificacion = Util.AtribuirValorObj(dtRow("OBS_NOTIFICACION"), GetType(String))
                            .ObservacionParametros = Util.AtribuirValorObj(dtRow("OBS_PARAMETROS"), GetType(String))
                            .TipoNotificacion = New Clases.CentralNotificacion.TipoNotificacion
                            .TipoNotificacion.Identificador = Util.AtribuirValorObj(dtRow("OID_TIPO_NOTIFICACION"), GetType(String))
                            .TipoNotificacion.CodigoTipoNotificacion = Util.AtribuirValorObj(dtRow("COD_TIPO_NOTIFICACION"), GetType(String))
                            .TipoNotificacion.DescripcionTipoNotificacion = Util.AtribuirValorObj(dtRow("DES_TIPO_NOTIFICACION"), GetType(String))
                            .TipoNotificacion.CodigoAplicacion = Util.AtribuirValorObj(dtRow("COD_APLICACION"), GetType(String))
                            .TipoNotificacion.BolEventoRelacionado = Util.AtribuirValorObj(dtRow("BOL_EVENTO_RELACIONADO"), GetType(String))
                            .UsuarioCreacion = Util.AtribuirValorObj(dtRow("DES_USUARIO_CREACION"), GetType(String))
                            .UsuarioModificacion = Util.AtribuirValorObj(dtRow("DES_USUARIO_MODIFICACION"), GetType(String))
                            .DestinosNotificacion = New List(Of Clases.CentralNotificacion.DestinoNotificacion)
                        End With
                        lstNotificacion.Add(objNotificacion)
                    End If

                    Dim objDestinoNotificacion As New Clases.CentralNotificacion.DestinoNotificacion
                    With objDestinoNotificacion
                        .Identificador = Util.AtribuirValorObj(dtRow("OID_DESTINO_NOTIFICACION"), GetType(String))
                        .BolLida = Util.AtribuirValorObj(dtRow("BOL_LEIDA"), GetType(Boolean))
                        .FechaCreacion = Util.AtribuirValorObj(dtRow("GMT_CREACION_DESTINO"), GetType(DateTime))
                        .FechaModificacion = Util.AtribuirValorObj(dtRow("GMT_MODIFICACION_DESTINO"), GetType(DateTime))
                        .UsuarioCreacion = Util.AtribuirValorObj(dtRow("DES_USUARIO_CREACION_DESTINO"), GetType(String))
                        .UsuarioModificacion = Util.AtribuirValorObj(dtRow("DES_USUARIO_MOD_DESTINO"), GetType(String))
                        .IdentificadorDelegacion = Util.AtribuirValorObj(dtRow("OID_DELEGACION"), GetType(String))
                        .IdentificadorNotificacion = Util.AtribuirValorObj(dtRow("OID_NOTIFICACION"), GetType(String))
                        .IdentificadorPlanta = Util.AtribuirValorObj(dtRow("OID_PLANTA"), GetType(String))
                        .IdentificadorSector = Util.AtribuirValorObj(dtRow("OID_SECTOR"), GetType(String))
                        .IdentificadorTipoSectorPlanta = Util.AtribuirValorObj(dtRow("OID_TIPO_SECTORXPLANTA"), GetType(String))
                        .IdentificadorUsuario = Util.AtribuirValorObj(dtRow("OID_USUARIO"), GetType(String))
                    End With
                    objNotificacion.DestinosNotificacion.Add(objDestinoNotificacion)
                Next
            End If

            Return lstNotificacion
        End Function

        Public Shared Function GrabarNotification(peticion As ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacion.Peticion) As ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacion.Respuesta

            Dim objRespuesta As New ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacion.Respuesta

            Try

                ValidarParametroAnadirNotificacion(peticion)
                Dim identificadorNotificacion As String = String.Empty

                AccesoDatos.Genesis.Notificacion.AnadirNotification(peticion.Cultura, peticion.CodigoTipoNotification, peticion.ObservacionNotificacion, peticion.ObservacionParametros, peticion.CodigoTipoDestino, peticion.IdentificadorDestino,
                                                                    peticion.Usuario, identificadorNotificacion)


                objRespuesta.IdentificadorNotificacion = identificadorNotificacion

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)

                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString

            End Try

            Return objRespuesta

        End Function

        Public Shared Function GrabarNotificacionLeido(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacionLeido.Peticion) As ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacionLeido.Respuesta

            Dim objRespuesta As New ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacionLeido.Respuesta
            objRespuesta.exito = False
            Try

                ValidarParametroGrabarNotificacionLeido(Peticion)

                AccesoDatos.Genesis.Notificacion.GrabarNotificacionLeido(Peticion.identificadoresDestinoNotificacion,
                                                                            Peticion.leido,
                                                                            Peticion.usuario)

                objRespuesta.exito = True
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = String.Empty

            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.CodigoError = ex.Codigo
                objRespuesta.MensajeError = ex.Descricao
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString
            End Try

            Return objRespuesta

        End Function

        Private Shared Sub ValidarParametroGrabarNotificacionLeido(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacionLeido.Peticion)
            If Peticion.identificadoresDestinoNotificacion Is Nothing OrElse Peticion.identificadoresDestinoNotificacion.Count = 0 Then
                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(String.Format(Tradutor.Traduzir("gen_srv_msg_id_obligatorio"), "identificadorDestinoNotificacion"))
            End If
        End Sub

        Private Shared Sub ValidarParametroAnadirNotificacion(peticion As ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacion.Peticion)

            If peticion.CodigoTipoDestino Is Nothing Then
                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(String.Format(Tradutor.Traduzir("gen_srv_msg_id_obligatorio"), "codigoTipoDestino"))
            End If

            If peticion.CodigoTipoNotification Is Nothing Then
                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(String.Format(Tradutor.Traduzir("gen_srv_msg_id_obligatorio"), "codigoTipoNotification"))
            End If

            If peticion.IdentificadorDestino Is Nothing Then
                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(String.Format(Tradutor.Traduzir("gen_srv_msg_id_obligatorio"), "identificadorDestino"))
            End If

            If peticion.ObservacionNotificacion Is Nothing Then
                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(String.Format(Tradutor.Traduzir("gen_srv_msg_id_obligatorio"), "observacionNotificacion"))
            End If

            If peticion.Usuario Is Nothing Then
                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(String.Format(Tradutor.Traduzir("gen_srv_msg_id_obligatorio"), "usuario"))
            End If

        End Sub

        Public Shared Function RecibirMensajeExterno(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.RecibirMensajeExterno.Peticion) As ContractoServicio.Contractos.Genesis.Notificacion.RecibirMensajeExterno.Respuesta

            Dim objRespuesta As New ContractoServicio.Contractos.Genesis.Notificacion.RecibirMensajeExterno.Respuesta

            Try

                Dim dsResult As DataSet = AccesoDatos.Genesis.Notificacion.RecibirMensajeExterno(Peticion)

                objRespuesta = PreencheRecibirMensajeExterno(dsResult)

                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = String.Empty

            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.CodigoError = ex.Codigo
                objRespuesta.MensajeError = ex.Descricao
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString
            End Try

            Return objRespuesta

        End Function

        Private Shared Function PreencheRecibirMensajeExterno(dsResult As DataSet) As ContractoServicio.Contractos.Genesis.Notificacion.RecibirMensajeExterno.Respuesta
            Dim objRespuesta As New ContractoServicio.Contractos.Genesis.Notificacion.RecibirMensajeExterno.Respuesta

            If dsResult IsNot Nothing Then
                If dsResult.Tables.Contains("NotificacionesOK") Then
                    objRespuesta.MensajesOk = New List(Of ContractoServicio.Contractos.Genesis.Notificacion.RecibirMensajeExterno.MensajeOk)

                    Dim dtNotificacionesOK As DataTable = dsResult.Tables("NotificacionesOK")
                    For Each dtRow In dtNotificacionesOK.Rows
                        Dim objMensajeOk As New ContractoServicio.Contractos.Genesis.Notificacion.RecibirMensajeExterno.MensajeOk
                        With objMensajeOk
                            .Identificador = Util.AtribuirValorObj(dtRow("ID"), GetType(String))
                            .TipoMensaje = Util.AtribuirValorObj(dtRow("TIPO"), GetType(String))
                        End With
                        objRespuesta.MensajesOk.Add(objMensajeOk)
                    Next
                End If

                If dsResult.Tables.Contains("NotificacionesError") Then
                    objRespuesta.MensajesError = New List(Of ContractoServicio.Contractos.Genesis.Notificacion.RecibirMensajeExterno.MensajeError)

                    Dim dtNotificacionesError As DataTable = dsResult.Tables("NotificacionesError")
                    For Each dtRow In dtNotificacionesError.Rows
                        Dim objMensajeError As New ContractoServicio.Contractos.Genesis.Notificacion.RecibirMensajeExterno.MensajeError
                        With objMensajeError
                            .Identificador = Util.AtribuirValorObj(dtRow("ID"), GetType(String))
                            .TipoMensaje = Util.AtribuirValorObj(dtRow("TIPO"), GetType(String))
                            .DescripcionError = Util.AtribuirValorObj(dtRow("ERROR"), GetType(String))
                        End With
                        objRespuesta.MensajesError.Add(objMensajeError)
                    Next
                End If
            End If

            Return objRespuesta
        End Function

    End Class

End Namespace
