Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Text
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.ContractoServicio

Namespace Genesis

    Public Class Notificacion

        ''' <summary>
        ''' Busca os identificadores de delegações,plantas, tipos setores e setores
        ''' necessários para identificar as privacidades das notificações
        ''' carregar apenas uma vez e guardar em memória
        ''' </summary>
        ''' <param name="desLogin"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerIdentificadores(desLogin As String) As DataTable

            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandType = CommandType.Text
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.Notificacion_ObtenerIdentificadores)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_LOGIN", ProsegurDbType.Identificador_Alfanumerico, desLogin.ToUpper()))

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function


        Public Shared Function CargarNotificaciones(desde As DateTime,
                                                    hasta As DateTime,
                                                    codigoAplicacion As String,
                                                    desLogin As String,
                                                    Optional codigosSector As List(Of String) = Nothing,
                                                    Optional codigosPlanta As List(Of String) = Nothing,
                                                    Optional codigosDelegacion As List(Of String) = Nothing,
                                                    Optional identificadoresTipoSector As List(Of String) = Nothing,
                                                    Optional leidas As Nullable(Of Boolean) = Nothing) As DataTable
            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.Notificacion_CargarNotificaciones
                Dim filtros As New List(Of String)

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))

                If desde <> DateTime.MinValue AndAlso hasta <> DateTime.MinValue Then
                    query += " AND ( N.GMT_CREACION >= TO_TIMESTAMP_TZ([]DESDE || ' +00:00','DD/MM/YYYY HH24:MI:SS TZH:TZM') AND N.GMT_CREACION <= TO_TIMESTAMP_TZ([]HASTA || ' +00:00','DD/MM/YYYY HH24:MI:SS TZH:TZM')) "
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DESDE", ProsegurDbType.Descricao_Curta, desde.ToString("dd/MM/yyyy HH:mm:ss")))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "HASTA", ProsegurDbType.Descricao_Curta, hasta.ToString("dd/MM/yyyy HH:mm:ss")))

                ElseIf desde <> DateTime.MinValue Then
                    query += " AND N.GMT_CREACION >= TO_TIMESTAMP_TZ([]DESDE || ' +00:00','DD/MM/YYYY HH24:MI:SS TZH:TZM') "
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DESDE", ProsegurDbType.Descricao_Curta, desde.ToString("dd/MM/yyyy HH:mm:ss")))

                ElseIf hasta <> DateTime.MinValue Then
                    query += " AND N.GMT_CREACION <= TO_TIMESTAMP_TZ([]HASTA || ' +00:00','DD/MM/YYYY HH24:MI:SS TZH:TZM') "
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "HASTA", ProsegurDbType.Descricao_Curta, hasta.ToString("dd/MM/yyyy HH:mm:ss")))
                End If

                If Not String.IsNullOrEmpty(desLogin) Then
                    filtros.Add(" UPPER(USU.DES_LOGIN) = []DES_LOGIN ")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_LOGIN", ProsegurDbType.Identificador_Alfanumerico, desLogin.ToUpper()))
                End If

                If codigosSector IsNot Nothing AndAlso codigosSector.Count > 0 Then
                    filtros.Add(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosSector, "COD_SECTOR", cmd, "", "SEC"))
                End If

                If codigosPlanta IsNot Nothing AndAlso codigosPlanta.Count > 0 Then
                    filtros.Add(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosPlanta, "COD_PLANTA", cmd, "", "PLA"))
                End If

                If codigosDelegacion IsNot Nothing AndAlso codigosDelegacion.Count > 0 Then
                    filtros.Add(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosDelegacion, "COD_DELEGACION", cmd, "", "DEL"))
                End If

                If identificadoresTipoSector IsNot Nothing AndAlso identificadoresTipoSector.Count > 0 Then
                    filtros.Add(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresTipoSector, "OID_TIPO_SECTORXPLANTA", cmd, "", "DN"))
                End If

                If leidas IsNot Nothing Then
                    query += " AND DN.BOL_LEIDA = []BOL_LEIDA "
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_LEIDA", ProsegurDbType.Logico, leidas))
                End If

                If filtros IsNot Nothing AndAlso filtros.Count > 0 Then
                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, " AND (" + String.Join(" OR ", filtros) + ")"))
                Else
                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, ""))
                End If

                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt

        End Function

        Public Shared Function ObtenerCantidadNotificaciones(desde As DateTime,
                                                    hasta As DateTime,
                                                    codigoAplicacion As String,
                                                    desLogin As String,
                                                    Optional codigosSector As List(Of String) = Nothing,
                                                    Optional codigosPlanta As List(Of String) = Nothing,
                                                    Optional codigosDelegacion As List(Of String) = Nothing,
                                                    Optional identificadoresTipoSector As List(Of String) = Nothing,
                                                    Optional leidas As Nullable(Of Boolean) = Nothing) As Integer

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.Notificacion_ObtenerCantidadNotificaciones
                Dim filtros As New List(Of String)

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))

                If desde <> DateTime.MinValue AndAlso hasta <> DateTime.MinValue Then
                    query += " AND N.GMT_CREACION >= []DESDE AND N.GMT_CREACION <= []HASTA "
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DESDE", ProsegurDbType.Data_Hora, desde))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "HASTA", ProsegurDbType.Data_Hora, hasta))

                ElseIf desde <> DateTime.MinValue Then
                    query += " AND N.GMT_CREACION >= []DESDE "
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DESDE", ProsegurDbType.Data_Hora, desde))

                ElseIf hasta <> DateTime.MinValue Then
                    query += " AND N.GMT_CREACION <= []HASTA "
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "HASTA", ProsegurDbType.Data_Hora, hasta))
                End If

                If Not String.IsNullOrEmpty(desLogin) Then
                    filtros.Add(" UPPER(USU.DES_LOGIN) = []DES_LOGIN ")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_LOGIN", ProsegurDbType.Identificador_Alfanumerico, desLogin.ToUpper()))
                End If

                If codigosSector IsNot Nothing AndAlso codigosSector.Count > 0 Then
                    filtros.Add(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosSector, "COD_SECTOR", cmd, "", "SEC"))
                End If

                If codigosPlanta IsNot Nothing AndAlso codigosPlanta.Count > 0 Then
                    filtros.Add(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosPlanta, "COD_PLANTA", cmd, "", "PLA"))
                End If

                If codigosDelegacion IsNot Nothing AndAlso codigosDelegacion.Count > 0 Then
                    filtros.Add(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosDelegacion, "COD_DELEGACION", cmd, "", "DEL"))
                End If

                If identificadoresTipoSector IsNot Nothing AndAlso identificadoresTipoSector.Count > 0 Then
                    filtros.Add(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresTipoSector, "OID_TIPO_SECTORXPLANTA", cmd, "", "DN"))
                End If

                If leidas IsNot Nothing Then
                    query += " AND DN.BOL_LEIDA = []BOL_LEIDA "
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_LEIDA", ProsegurDbType.Logico, leidas))
                End If

                If filtros IsNot Nothing AndAlso filtros.Count > 0 Then
                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, " AND (" + String.Join(" OR ", filtros) + ")"))
                Else
                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, ""))
                End If

                cmd.CommandType = CommandType.Text

                Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)

            End Using

        End Function

        Public Shared Sub AnadirNotification(codigoCultura As String, codigoTipoNotification As String, observacionNotificacion As String, observacionParametros As String, codigoTipoDestino As String,
                                                   identificadorDestino As String, usuario As String, identificadorNotificacion As String)

            Dim spw As SPWrapper = RellenarAnadirNotification(codigoCultura, codigoTipoNotification, observacionNotificacion, observacionParametros, codigoTipoDestino, identificadorDestino, usuario, identificadorNotificacion)
            AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

        End Sub

        Public Shared Sub GrabarNotificacionLeido(identificadorDestinoNotificacion As List(Of String),
                                                leido As Boolean,
                                                usuario As String)

            Dim spw As SPWrapper = RellenarGrabarNotificacionLeido(identificadorDestinoNotificacion, leido, usuario)
            AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

        End Sub

        Private Shared Function RellenarAnadirNotification(codigoCultura As String, codigoTipoNotification As String, observacionNotificacion As String, observacionParametros As String, codigoTipoDestino As String,
                                                   identificadorDestino As String, usuario As String, identificadorNotificacion As String)

            Dim SP As String = Constantes.SP_ANADIR_NOTIFICACION
            Dim spw As New SPWrapper(SP, True)

            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, codigoCultura, , False)
            spw.AgregarParam("par$cod_tipo_notificacion", ProsegurDbType.Identificador_Alfanumerico, codigoTipoNotification, , False)
            spw.AgregarParam("par$obs_notificacion", ParamWrapper.ParamTypes.String, observacionNotificacion, , False)
            spw.AgregarParam("par$obs_parametros", ParamWrapper.ParamTypes.String, observacionParametros, , False)
            spw.AgregarParam("par$cod_tipo_destino", ProsegurDbType.Observacao_Longa, Nothing, , True)
            spw.AgregarParam("par$oid_destino", ProsegurDbType.Observacao_Longa, Nothing, , True)
            spw.AgregarParam("par$usuario", ProsegurDbType.Descricao_Longa, usuario, , False)

            spw.AgregarParam("par$oid_notificacion", ProsegurDbType.Objeto_Id, identificadorNotificacion, ParameterDirection.Output, False)

            spw.Param("par$cod_tipo_destino").AgregarValorArray(codigoTipoDestino)
            spw.Param("par$oid_destino").AgregarValorArray(identificadorDestino)


            Return spw

        End Function

        Private Shared Function RellenarGrabarNotificacionLeido(identificadorDestinoNotificacion As List(Of String),
                                                                leido As Boolean,
                                                                usuario As String) As SPWrapper

            Dim SP As String = Constantes.SP_GRABAR_NOTIFICACION_LEIDO
            Dim spw As New SPWrapper(SP, True)

            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                                             Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                                             Tradutor.CulturaSistema.Name,
                                                                                             If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)

            spw.AgregarParam("par$oid_destino_notificacion", ProsegurDbType.Identificador_Alfanumerico, identificadorDestinoNotificacion, , True)
            spw.AgregarParam("par$leido", ProsegurDbType.Logico, leido, , False)

            If identificadorDestinoNotificacion IsNot Nothing AndAlso identificadorDestinoNotificacion.Count > 0 Then
                For Each id In identificadorDestinoNotificacion
                    spw.Param("par$oid_destino_notificacion").AgregarValorArray(id)
                Next
            End If
            spw.AgregarParam("par$usuario", ProsegurDbType.Descricao_Longa, usuario, , False)

            Return spw
        End Function

        Public Shared Function RecibirMensajeExterno(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.RecibirMensajeExterno.Peticion) As DataSet

            Dim spw As SPWrapper = RellenarRecibirMensajeExterno(Peticion)
            Dim dsResult As DataSet = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

            Dim retIdNotificacionError As String = String.Empty
            Dim retTipoNotificacionError As String = String.Empty
            Dim retNotificacionError As String = String.Empty

            If spw.Param("par$notificacion_error").Valor IsNot Nothing AndAlso spw.Param("par$notificacion_error").Valor.ToString() <> "null" Then

                retIdNotificacionError = spw.Param("par$oid_notificacion_error").Valor.ToString()
                retTipoNotificacionError = spw.Param("par$tipo_notificacion_error").Valor.ToString()
                retNotificacionError = spw.Param("par$notificacion_error").Valor.ToString()

            End If


            Dim dtNotificacionError As New DataTable("NotificacionesError")
            dtNotificacionError.Columns.Add("ID")
            dtNotificacionError.Columns.Add("TIPO")
            dtNotificacionError.Columns.Add("ERROR")

            If Not String.IsNullOrEmpty(retNotificacionError) Then

                Dim arrID As String() = retIdNotificacionError.Split(";")
                Dim arrTipo As String() = retTipoNotificacionError.Split(";")
                Dim arrErro As String() = retNotificacionError.Split(";")

                For i As Integer = 0 To arrID.Count - 1

                    dtNotificacionError.Rows.Add(arrID(i), arrTipo(i), arrErro(i))

                Next

            End If

            dsResult.Tables.Add(dtNotificacionError)

            Return dsResult
        End Function

        Private Shared Function RellenarRecibirMensajeExterno(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.RecibirMensajeExterno.Peticion) As SPWrapper

            Dim SP As String = Constantes.SP_RECIBIR_MENSAJE_EXTERNO
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                                             Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                                             Tradutor.CulturaSistema.Name,
                                                                                             If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)

            spw.AgregarParam("par$oid_mensaje", ProsegurDbType.Observacao_Longa, Nothing, , True)
            spw.AgregarParam("par$mensaje", ProsegurDbType.Observacao_Longa, Nothing, , True)
            spw.AgregarParam("par$tipo_mensaje", ProsegurDbType.Observacao_Longa, Nothing, , True)
            spw.AgregarParam("par$oid_msg_criterio", ProsegurDbType.Observacao_Longa, Nothing, , True)
            spw.AgregarParam("par$cod_criterio", ProsegurDbType.Observacao_Longa, Nothing, , True)
            spw.AgregarParam("par$val_criterio", ProsegurDbType.Observacao_Longa, Nothing, , True)
            spw.AgregarParam("par$usuario", ProsegurDbType.Observacao_Longa, Peticion.Usuario, , False)
            spw.AgregarParam("par$rc_notificaciones_ok", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "NotificacionesOK")
            spw.AgregarParam("par$oid_notificacion_error", ParamWrapper.ParamTypes.String, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$tipo_notificacion_error", ParamWrapper.ParamTypes.String, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$notificacion_error", ParamWrapper.ParamTypes.String, Nothing, ParameterDirection.Output, False)

            For Each mensaje In Peticion.Mensajes
                Dim idMensaje As String = Guid.NewGuid().ToString()
                spw.Param("par$oid_mensaje").AgregarValorArray(idMensaje)
                spw.Param("par$mensaje").AgregarValorArray(mensaje.Mensaje)
                spw.Param("par$tipo_mensaje").AgregarValorArray(mensaje.TipoMensaje)

                If mensaje.ColeccionCriterio IsNot Nothing AndAlso mensaje.ColeccionCriterio.Count > 0 Then
                    For Each criterio In mensaje.ColeccionCriterio
                        spw.Param("par$oid_msg_criterio").AgregarValorArray(idMensaje)
                        spw.Param("par$cod_criterio").AgregarValorArray(criterio.Codigo)
                        spw.Param("par$val_criterio").AgregarValorArray(criterio.Valor)
                    Next
                End If
            Next

            Return spw
        End Function

    End Class

End Namespace