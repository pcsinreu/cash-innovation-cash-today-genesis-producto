Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper

Namespace Genesis

    ''' <summary>
    ''' Classe Integracion
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Integracion

#Region "Inserção"

        Private Shared Function Cargar(dr As DataRow) As Clases.Integracion
            Return New Clases.Integracion With
            {
                .Identificador = Util.AtribuirValorObj(dr("OID_INTEGRACION"), GetType(String)),
                .IdentificadorTablaIntegracion = Util.AtribuirValorObj(dr("OID_TABLA_INTEGRACION"), GetType(String)),
                .CodigoTablaIntegracionEnum = RecuperarEnum(Of Enumeradores.TablaIntegracion)(Util.AtribuirValorObj(dr("COD_TABLA_INTEGRACION"), GetType(String))),
                .CodigoEstado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoIntegracion)(Util.AtribuirValorObj(dr("COD_ESTADO"), GetType(String))),
                .CodigoModuloOrigen = Extenciones.RecuperarEnum(Of Enumeradores.Aplicacion)(Util.AtribuirValorObj(dr("COD_MODULO_ORIGEN"), GetType(String))),
                .CodigoModuloDestino = Extenciones.RecuperarEnum(Of Enumeradores.Aplicacion)(Util.AtribuirValorObj(dr("COD_MODULO_DESTINO"), GetType(String))),
                .CodigoProceso = Extenciones.RecuperarEnum(Of Enumeradores.CodigoProcesoIntegracion)(Util.AtribuirValorObj(dr("COD_PROCESO"), GetType(String))),
                .Intentos = Util.AtribuirValorObj(dr("NEL_INTENTO_ENVIO"), GetType(Integer)),
                .FechaHoraCreacion = Util.AtribuirValorObj(dr("GMT_CREACION"), GetType(DateTime)),
                .UsuarioCreacion = Util.AtribuirValorObj(dr("DES_USUARIO_CREACION"), GetType(String)),
                .FechaHoraModificacion = Util.AtribuirValorObj(dr("GMT_MODIFICACION"), GetType(DateTime)),
                .UsuarioModificacion = Util.AtribuirValorObj(dr("DES_USUARIO_MODIFICACION"), GetType(String))
            }
        End Function

        Shared Function ObtenerIntegracion(integracion As Clases.Integracion) As Clases.Integracion

            If integracion IsNot Nothing Then

                Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)

                    Dim dtDados As DataTable
                    Dim sqlResource As String = My.Resources.ObtenerIntegracion

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "OID_TABLA_INTEGRACION", ProsegurDbType.Identificador_Alfanumerico, integracion.IdentificadorTablaIntegracion))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_PROCESO", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(integracion.CodigoProceso)))
                    cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, sqlResource)

                    dtDados = AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)

                    If (dtDados.Rows.Count > 0) Then
                        Return Cargar(dtDados.Rows(0))
                    End If

                End Using

            End If

            Return Nothing

        End Function

        Shared Function ObtenerIntegracion(integracion As Clases.Integracion, ByRef transaccion As DataBaseHelper.Transaccion) As Clases.Integracion

            If integracion IsNot Nothing Then

                Dim sql = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerIntegracion)
                Dim sp As New SPWrapper(sql, False, CommandType.Text)

                sp.AgregarParam("OID_TABLA_INTEGRACION", ProsegurDbType.Identificador_Alfanumerico, integracion.IdentificadorTablaIntegracion)
                sp.AgregarParam("COD_PROCESO", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(integracion.CodigoProceso))

                Dim ds As DataSet = AccesoDB.EjecutarSP(sp, Constantes.CONEXAO_GENESIS, False, transaccion)
                If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                    If (ds.Tables(0).Rows.Count > 0) Then
                        Return Cargar(ds.Tables(0).Rows(0))
                    End If
                End If

            End If

            Return Nothing

        End Function


        Public Shared Function InserirIntegracion(integracion As Clases.Integracion) As Clases.Integracion

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.InserirIntegracion)
            cmd.CommandType = CommandType.Text

            integracion.Identificador = Guid.NewGuid.ToString()

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_INTEGRACION", ProsegurDbType.Objeto_Id, integracion.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TABLA_INTEGRACION", ProsegurDbType.Objeto_Id, integracion.IdentificadorTablaIntegracion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TABLA_INTEGRACION", ProsegurDbType.Identificador_Alfanumerico, integracion.CodigoTablaIntegracionEnum.RecuperarValor()))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(integracion.CodigoEstado)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_MODULO_ORIGEN", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(integracion.CodigoModuloOrigen)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_MODULO_DESTINO", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(integracion.CodigoModuloDestino)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PROCESO", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(integracion.CodigoProceso)))

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, integracion.UsuarioCreacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, integracion.UsuarioModificacion))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

            Return integracion

        End Function

        Public Shared Function InserirIntegracion(integracion As Clases.Integracion, ByRef transaccion As DataBaseHelper.Transaccion) As Clases.Integracion
            Dim commandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.InserirIntegracion)
            Dim sp As New SPWrapper(commandText, True, CommandType.Text)

            integracion.Identificador = Guid.NewGuid.ToString()

            sp.AgregarParam("OID_INTEGRACION", ProsegurDbType.Objeto_Id, integracion.Identificador)
            sp.AgregarParam("OID_TABLA_INTEGRACION", ProsegurDbType.Objeto_Id, integracion.IdentificadorTablaIntegracion)
            sp.AgregarParam("COD_TABLA_INTEGRACION", ProsegurDbType.Identificador_Alfanumerico, integracion.CodigoTablaIntegracionEnum.RecuperarValor())
            sp.AgregarParam("COD_ESTADO", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(integracion.CodigoEstado))
            sp.AgregarParam("COD_MODULO_ORIGEN", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(integracion.CodigoModuloOrigen))
            sp.AgregarParam("COD_MODULO_DESTINO", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(integracion.CodigoModuloDestino))
            sp.AgregarParam("COD_PROCESO", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(integracion.CodigoProceso))
            sp.AgregarParam("DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, integracion.UsuarioCreacion)
            sp.AgregarParam("DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, integracion.UsuarioModificacion)

            AccesoDB.EjecutarSP(sp, Constantes.CONEXAO_GENESIS, False, transaccion)

            Return integracion

        End Function

        Public Shared Sub ActualizarIntegracion(integracion As Clases.Integracion)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ActualizarIntegracion)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(integracion.CodigoEstado)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_INTENTO_ENVIO", ProsegurDbType.Identificador_Alfanumerico, integracion.Intentos))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, integracion.UsuarioModificacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_INTEGRACION", ProsegurDbType.Objeto_Id, integracion.Identificador))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        End Sub

        Public Shared Sub ActualizarIntegracion(integracion As Clases.Integracion, ByRef transaccion As DataBaseHelper.Transaccion)
            Dim commandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ActualizarIntegracion)
            Dim sp As New SPWrapper(commandText, True, CommandType.Text)

            sp.AgregarParam("COD_ESTADO", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(integracion.CodigoEstado))
            sp.AgregarParam("NEL_INTENTO_ENVIO", ProsegurDbType.Identificador_Alfanumerico, integracion.Intentos)
            sp.AgregarParam("DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, integracion.UsuarioModificacion)
            sp.AgregarParam("OID_INTEGRACION", ProsegurDbType.Objeto_Id, integracion.Identificador)
            AccesoDB.EjecutarSP(sp, Constantes.CONEXAO_GENESIS, False, transaccion)
        End Sub

        Public Shared Function InserirIntegracionError(integracionError As Clases.IntegracionError, identificadorIntegracion As String) As Clases.IntegracionError

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.InserirIntegracionError)
            cmd.CommandType = CommandType.Text

            integracionError.Identificador = Guid.NewGuid.ToString()

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_INTEGRACION_ERROR", ProsegurDbType.Objeto_Id, integracionError.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_INTEGRACION", ProsegurDbType.Objeto_Id, identificadorIntegracion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OBS_ERROR", ProsegurDbType.Identificador_Alfanumerico, integracionError.DescricaoError))

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, integracionError.UsuarioCreacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, integracionError.UsuarioModificacion))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

            Return integracionError

        End Function

        Public Shared Function InserirIntegracionError(integracionError As Clases.IntegracionError, identificadorIntegracion As String, ByRef transaccion As DataBaseHelper.Transaccion) As Clases.IntegracionError
            Dim commandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.InserirIntegracionError)
            Dim sp As New SPWrapper(commandText, True, CommandType.Text)

            integracionError.Identificador = Guid.NewGuid.ToString()

            sp.AgregarParam("OID_INTEGRACION_ERROR", ProsegurDbType.Objeto_Id, integracionError.Identificador)
            sp.AgregarParam("OID_INTEGRACION", ProsegurDbType.Objeto_Id, identificadorIntegracion)
            sp.AgregarParam("OBS_ERROR", ProsegurDbType.Identificador_Alfanumerico, integracionError.DescricaoError)
            sp.AgregarParam("DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, integracionError.UsuarioCreacion)
            sp.AgregarParam("DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, integracionError.UsuarioModificacion)

            AccesoDB.EjecutarSP(sp, Constantes.CONEXAO_GENESIS, False, transaccion)

            Return integracionError

        End Function

        Public Shared Function RecuperarIntegracionPorIdentificadorTabla(identificadorLlamada As String, listaIdentificadores As List(Of String), codProceso As String, codOrigen As String, codDestino As String) As IntegracionGenerica.Respuesta

            Dim respuesta As New IntegracionGenerica.Respuesta
            Try
                Dim ds As DataSet = Nothing

                Dim spw As SPWrapper = ColetarIntegracionPorIdentificador(identificadorLlamada, listaIdentificadores, codProceso, codOrigen, codDestino)
                ds = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

                respuesta = PoblarIntegracionesPorIdentificador(ds)

            Catch ex As Exception
                Throw ex
            End Try

            Return respuesta
        End Function

        Private Shared Function ColetarIntegracionPorIdentificador(identificadorLlamada As String, listaActualId As List(Of String), codProceso As String, codOrigen As String, codDestino As String) As SPWrapper

            Dim SP As String = String.Format("gepr_pintegracion_{0}.sbusqueda_estado_integracion", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada, , False)
            spw.AgregarParam("par$aactual_id", ProsegurDbType.Descricao_Curta, Nothing, , True)
            'spw.AgregarParam("par$ades_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$cod_proceso", ProsegurDbType.Descricao_Curta, codProceso, , False)
            spw.AgregarParam("par$cod_origen", ProsegurDbType.Descricao_Curta, codOrigen, , False)
            spw.AgregarParam("par$cod_destino", ProsegurDbType.Descricao_Curta, codDestino, , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Curto, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$rc_estados", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "estado")

            For Each actualId In listaActualId
                spw.Param("par$aactual_id").AgregarValorArray(actualId)
            Next

            Return spw

        End Function

        Public Shared Function PoblarIntegracionesPorIdentificador(ds As DataSet) As IntegracionGenerica.Respuesta

            Dim respuesta As New IntegracionGenerica.Respuesta()
            Dim objPlanificacion As New Clases.Planificacion
            Dim dtEstadosPorActualId As DataTable = ds.Tables("estado")
            respuesta.ListaIntegracion = New List(Of Clases.Integracion)()
            Dim unaIntegracion As Clases.Integracion
            For Each dr In dtEstadosPorActualId.Rows

                unaIntegracion = New Clases.Integracion()

                unaIntegracion.CodigoEstado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoIntegracion)(Util.AtribuirValorObj(dr("COD_ESTADO"), GetType(String)))
                Util.AtribuirValorObjeto(unaIntegracion.IdentificadorTablaIntegracion, dr("OID_TABLA_INTEGRACION"), GetType(String))
                Util.AtribuirValorObjeto(unaIntegracion.Identificador, dr("OID_INTEGRACION"), GetType(String))
                Util.AtribuirValorObjeto(unaIntegracion.CodigoTablaIntegracion, dr("COD_TABLA_INTEGRACION"), GetType(String))

                respuesta.ListaIntegracion.Add(unaIntegracion)
            Next

            Return respuesta
        End Function

        Public Shared Function RecuperarIntegracionPendientes(peticion As IntegracionGenerica.Peticion) As IntegracionGenerica.Respuesta
            Dim IntegracionesPendientes As IntegracionGenerica.Respuesta

            Try
                Dim ds As DataSet = Nothing

                Dim spw As SPWrapper = ColetarIntegracionPendientes(peticion)
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False)

                IntegracionesPendientes = PoblarIntegracionesPendientes(ds)

            Catch ex As Exception
                Throw ex
            End Try

            Return IntegracionesPendientes
        End Function

        Private Shared Function ColetarIntegracionPendientes(peticion As IntegracionGenerica.Peticion) As SPWrapper

            Dim SP As String = String.Format("gepr_pintegracion_{0}.sbusqueda_pend_integracion", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, peticion.IdentificadorLlamada, , False)
            spw.AgregarParam("par$cod_proceso", ProsegurDbType.Descricao_Longa, peticion.CodigoProceso, , False)
            spw.AgregarParam("par$cod_origen", ProsegurDbType.Descricao_Longa, peticion.CodigoOrigen, , False)
            spw.AgregarParam("par$cod_destino", ProsegurDbType.Descricao_Longa, peticion.CodigoDestino, , False)
            spw.AgregarParam("par$cod_pais", ProsegurDbType.Descricao_Curta, peticion.CodigoPais, , False)
            spw.AgregarParam("par$desc_parametro_reintento", ProsegurDbType.Descricao_Longa, peticion.NombreParametroReintentoMaximo, , False)
            spw.AgregarParam("par$acod_estado", ProsegurDbType.Descricao_Curta, Nothing, , True)

            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Curto, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$rc_pendientes", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "rc_pendientes")

            For Each codEstado In peticion.ListaCodigosEstado
                spw.Param("par$acod_estado").AgregarValorArray(codEstado.RecuperarValor)
            Next

            Return spw

        End Function


        Private Shared Function PoblarIntegracionesPendientes(ds As DataSet) As IntegracionGenerica.Respuesta

            Dim respuesta = New IntegracionGenerica.Respuesta
            Dim dtActualIdPendientes As DataTable = ds.Tables("rc_pendientes")
            If Not dtActualIdPendientes Is Nothing Then
                For Each dr In dtActualIdPendientes.Rows
                    Dim integracion As New Clases.Integracion
                    Util.AtribuirValorObjeto(integracion.Identificador, dr("OID_INTEGRACION"), GetType(String))
                    Util.AtribuirValorObjeto(integracion.IdentificadorTablaIntegracion, dr("OID_TABLA_INTEGRACION"), GetType(String))
                    integracion.CodigoEstado = RecuperarEnum(Of EstadoIntegracion)(Util.AtribuirValorObj(dr("COD_ESTADO"), GetType(String)))
                    Util.AtribuirValorObjeto(integracion.CodigoTablaIntegracion, dr("COD_TABLA_INTEGRACION"), GetType(String))

                    respuesta.ListaIntegracion.Add(integracion)
                Next
            End If
            Return respuesta
        End Function

#End Region



        Public Shared Function ConfigurarIntegracion(identificadorLlamada As String, peticion As Contractos.Integracion.IntegracionSistemas.Integracion.Peticion,
                                        ByRef log As StringBuilder) As Contractos.Integracion.IntegracionSistemas.Integracion.Respuesta



            Dim respuesta As New Contractos.Integracion.IntegracionSistemas.Integracion.Respuesta
            Try

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                If peticion IsNot Nothing Then

                    spw = sColetarPeticion(identificadorLlamada, peticion, log)
                    ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)


                    If ds.Tables.Contains("validaciones") AndAlso ds.Tables("validaciones").Rows.Count > 0 Then
                        For Each rDocumento As DataRow In ds.Tables("validaciones").DefaultView.ToTable().Rows
                            respuesta.Tipo = Util.AtribuirValorObj(rDocumento("CODIGO"), GetType(String))
                            respuesta.Descripcion = Util.AtribuirValorObj(rDocumento("DESCRIPCION"), GetType(String))
                        Next
                    End If
                End If

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                     MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

            Return respuesta

        End Function
        Private Shared Function sColetarPeticion(identificadorLlamada As String, peticion As Contractos.Integracion.IntegracionSistemas.Integracion.Peticion, ByRef log As StringBuilder) As SPWrapper

            Dim SP As String = String.Format("GEPR_PINTEGRACION_{0}.sconfigurar_integracion", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)
            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada, , False)
            spw.AgregarParam("par$cod_tabla_integracion", ProsegurDbType.Descricao_Curta, peticion.CodigoTablaIntegracion, , False)
            spw.AgregarParam("par$oid_tabla_integracion", ProsegurDbType.Identificador_Alfanumerico, peticion.IdentificadorTablaIntegracion, , False)
            spw.AgregarParam("par$cod_estado", ProsegurDbType.Descricao_Curta, peticion.Estado.RecuperarValor(), , False)
            spw.AgregarParam("par$cod_estado_detalle", ProsegurDbType.Descricao_Curta, peticion.EstadoDetalle.RecuperarValor(), , False)
            spw.AgregarParam("par$cod_modulo_origen", ProsegurDbType.Descricao_Curta, peticion.ModuloOrigem, , False)
            spw.AgregarParam("par$cod_modulo_destino", ProsegurDbType.Descricao_Curta, peticion.ModuloDestino, , False)
            spw.AgregarParam("par$cod_proceso", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoProceso, , False)
            spw.AgregarParam("par$nel_incremento_intento", ProsegurDbType.Inteiro_Curto, peticion.IncrementoIntento, , False)

            If peticion.ReiniciarIntento Then
                spw.AgregarParam("par$nel_reiniciar_intento", ProsegurDbType.Inteiro_Curto, 1, , False)
            Else
                spw.AgregarParam("par$nel_reiniciar_intento", ProsegurDbType.Inteiro_Curto, 0, , False)
            End If

            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Curta, peticion.Usuario, , False)
            spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")

            If peticion.Detalle IsNot Nothing Then
                spw.AgregarParam("par$cod_error", ProsegurDbType.Descricao_Curta, peticion.Detalle.Codigo, , False)

                Dim detalle = peticion.Detalle.Descripcion

                If Not String.IsNullOrWhiteSpace(peticion.Log) Then
                    detalle = detalle & Environment.NewLine & "Comentarios Adicionales: " & peticion.Log
                End If
                spw.AgregarParam("par$des_detalle", ProsegurDbType.Descricao_Longa, detalle, , False)
            Else
                spw.AgregarParam("par$cod_error", ProsegurDbType.Descricao_Curta, DBNull.Value, , False)
                spw.AgregarParam("par$des_detalle", ProsegurDbType.Descricao_Longa, DBNull.Value, , False)
            End If



            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)
            spw.AgregarParamInfo("par$info_ejecucion")


            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            Return spw

        End Function
    End Class

End Namespace
