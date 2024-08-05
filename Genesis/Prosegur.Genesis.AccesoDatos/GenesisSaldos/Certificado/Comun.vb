Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Framework.Dicionario
Imports System.Transactions
Imports System.Configuration

' Alias
Imports CSCertificacion = Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion
Imports CSIntegracion = Prosegur.Genesis.ContractoServicio.Contractos.Integracion


Namespace GenesisSaldos.Certificacion

    Public Class Comun

#Region "[GENERAR]"

        Public Shared Sub GenerarCertificado(Peticion As CSCertificacion.GenerarCertificado.Peticion)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim IdentificadorCertificado As String = String.Empty

            Try
                Dim oidSectorClausulaIN As String = Guid.NewGuid.ToString
                Dim oidSubCanalClausulaIN As String = Guid.NewGuid.ToString
                Dim oidDelegacionClausulaIN As String = Guid.NewGuid.ToString
                Dim objErro As String = String.Empty

                If Not Peticion.EsTodosSectores Then
                    'Insere os subcanales selecionados
                    For Each codigoSector In Peticion.CodigosSectores
                        Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                            cmd.CommandText = "SAPR_SCLAUSULA_IN_INSERT_###VERSION###"
                            cmd.CommandType = CommandType.StoredProcedure

                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "PAR$OID_CLAUSULA_IN", ProsegurDbType.Descricao_Longa, oidSectorClausulaIN))
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "PAR$ITEM", ProsegurDbType.Descricao_Longa, codigoSector))

                            AcessoDados.ExecutarNonQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)
                        End Using
                    Next
                End If

                If Not Peticion.EsTodosCanales Then
                    'Insere os setores selecionados
                    For Each codigoSubCanal In Peticion.CodigosSubCanales
                        Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                            cmd.CommandText = "SAPR_SCLAUSULA_IN_INSERT_###VERSION###"
                            cmd.CommandType = CommandType.StoredProcedure

                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "PAR$OID_CLAUSULA_IN", ProsegurDbType.Descricao_Longa, oidSubCanalClausulaIN))
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "PAR$ITEM", ProsegurDbType.Descricao_Longa, codigoSubCanal))

                            AcessoDados.ExecutarNonQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)
                        End Using
                    Next
                End If

                'Insere as delegaciones selecionadas
                For Each codigoDelegacion In Peticion.CodigosDelegaciones
                    Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                        cmd.CommandText = "SAPR_SCLAUSULA_IN_INSERT_###VERSION###"
                        cmd.CommandType = CommandType.StoredProcedure

                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "PAR$OID_CLAUSULA_IN", ProsegurDbType.Descricao_Longa, oidDelegacionClausulaIN))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "PAR$ITEM", ProsegurDbType.Descricao_Longa, codigoDelegacion))

                        AcessoDados.ExecutarNonQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)
                    End Using
                Next

                Using comando

                    ' obter procedure
                    comando.CommandText = Constantes.SP_SALDOS_GENERAR_CERTIFICADO_EJECUTAR
                    comando.CommandType = CommandType.StoredProcedure

                    comando.Parameters.Add(Util.CriarParametroOracle("P_IDENTIFICADOR", ParameterDirection.InputOutput, String.Empty, OracleClient.OracleType.VarChar, 36))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "P_CODIGO_CLIENTE", ProsegurDbType.Objeto_Id, If(Peticion.Cliente IsNot Nothing,
                                                                                                                                                                 Peticion.Cliente.Codigo, String.Empty)))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "P_CODIGO_EXTERNO", ProsegurDbType.Descricao_Longa, Peticion.CodigoExterno))

                    If Peticion.CodigoEstado = Prosegur.Genesis.Comon.Enumeradores.EstadoCertificado.Definitivo.RecuperarValor Then
                        comando.Parameters.Add(Util.CriarParametroOracle("P_CODIGO", ParameterDirection.InputOutput, Peticion.CodigoCertificadoDefinitivo, OracleClient.OracleType.VarChar, 500))
                    Else
                        comando.Parameters.Add(Util.CriarParametroOracle("P_CODIGO", ParameterDirection.InputOutput, Peticion.CodigoCertificado, OracleClient.OracleType.VarChar, 500))
                    End If

                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "P_FYH_CERTIFICADO", ProsegurDbType.Data_Hora, Peticion.FyhCertificado.QuieroGrabarGMTZeroEnLaBBDD(Peticion.DelegacionLogada)))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "P_TODOS_SECTORES", ProsegurDbType.Logico, Peticion.EsTodosSectores))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "P_TODOS_SUBCANALES", ProsegurDbType.Logico, Peticion.EsTodosCanales))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "P_DESCRIPCION_USUARIO", ProsegurDbType.Identificador_Alfanumerico, Peticion.UsuarioCreacion))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "P_OID_SECTOR_CLAUSULA_IN", ProsegurDbType.Descricao_Longa, oidSectorClausulaIN))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "P_OID_SUBCANAL_CLAUSULA_IN", ProsegurDbType.Descricao_Longa, oidSubCanalClausulaIN))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "P_OID_DELEGACION_CLAUSULA_IN", ProsegurDbType.Descricao_Longa, oidDelegacionClausulaIN))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "P_COD_ESTADO", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodigoEstado))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "P_CULTURA", ProsegurDbType.Identificador_Alfanumerico, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                                                                                                                           Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                                                                                                                           Tradutor.CulturaSistema.Name,
                                                                                                                                                                          If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty))))
                    comando.Parameters.Add(Util.CriarParametroOracle("P_ERROR", ParameterDirection.InputOutput, String.Empty, OracleClient.OracleType.VarChar, 4000))

                    ' executar consulta,
                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, comando)

                    objErro = Util.AtribuirValorObj(comando.Parameters("P_ERROR").Value, GetType(String))
                    IdentificadorCertificado = Util.AtribuirValorObj(comando.Parameters("P_IDENTIFICADOR").Value, GetType(String))

                    If Not String.IsNullOrEmpty(objErro) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, objErro)
                    End If


                End Using

            Catch ex As Excepcion.NegocioExcepcion

                If Not String.IsNullOrEmpty(IdentificadorCertificado) Then
                    BorrarCertificado(IdentificadorCertificado)
                End If

                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If

            Catch ex As Exception
                Try
                    If Not String.IsNullOrEmpty(IdentificadorCertificado) Then
                        BorrarCertificado(IdentificadorCertificado)
                    End If
                Catch exBorrar As Exception
                    'NÃO FAZ NADA
                End Try

                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

        End Sub

        Public Shared Sub GenerarReporteCertificado(DatosCertificado As ContractoServicio.Contractos.GenesisSaldos.Reporte.GenerarInforme.Reporte,
                                                    FechaProgramacion As DateTime,
                                                    ByRef objTransacion As Transacao)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim IdentificadorCertificado As String = String.Empty

            Try

                Dim Parametros As String = String.Empty

                Parametros = String.Format("{0}={1}", Prosegur.Genesis.Comon.Constantes.CODIGO_PARAMETRO_REPORTE_TIPO_REPORTE, DatosCertificado.TipoReporte.RecuperarValor)
                Parametros &= "|" & String.Format("{0}={1}", Prosegur.Genesis.Comon.Constantes.CODIGO_PARAMETRO_REPORTE_CODIGO_DELEGACION, DatosCertificado.CodigoDelegacion)
                Parametros &= "|" & String.Format("{0}={1}", Prosegur.Genesis.Comon.Constantes.CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_CONFIG_REPORTE, DatosCertificado.IdentificadorConfiguracionReporte)

                If DatosCertificado.ParametrosReporte IsNot Nothing AndAlso DatosCertificado.ParametrosReporte.Count > 0 Then

                    For Each objParametro In DatosCertificado.ParametrosReporte
                        Parametros &= "|" & String.Format("{0}={1}", objParametro.Codigo, objParametro.DescripcionValor)
                    Next

                End If

                Using comando

                    ' obter procedure
                    comando.CommandText = Constantes.SP_GEPR_YDUPR_SCREAR_ITEM_2
                    comando.CommandType = CommandType.StoredProcedure

                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PROCESO", ProsegurDbType.Identificador_Alfanumerico, "GEN_RPT_CERTIF"))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_VALOR", ProsegurDbType.Descricao_Longa, Parametros))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_POSEE_DEPENDENCIA", ProsegurDbType.Logico, False))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_ORIGEN", ProsegurDbType.Identificador_Alfanumerico, "GENESIS"))
                    If FechaProgramacion <> DateTime.MinValue Then
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PROGRAMACION", ProsegurDbType.Data_Hora, FechaProgramacion))
                    Else
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PROGRAMACION", ProsegurDbType.Data_Hora, DBNull.Value))
                    End If

                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PREFIJO", ProsegurDbType.Identificador_Alfanumerico, String.Empty))
                    comando.Parameters.Add(Util.CriarParametroOracle("OID_ITEM_PROCESO", ParameterDirection.InputOutput, String.Empty, OracleClient.OracleType.VarChar, 36))

                    ' executar consulta,
                    objTransacion.AdicionarItemTransacao(comando)

                End Using

            Catch ex As Exception
                Throw ex
            End Try

        End Sub

#End Region

#Region "[CONVERTIR]"
        Public Shared Sub ConvertirCertificado(Peticion As CSCertificacion.DatosCertificacion.Peticion, nuevoCodigo As String)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Try
                Dim objErro As String = String.Empty

                Using comando

                    ' obter procedure
                    comando.CommandText = Constantes.SP_SALDOS_GENERAR_CERTIFICADO_CONVERTIR
                    comando.CommandType = CommandType.StoredProcedure

                    comando.Parameters.Add(Util.CriarParametroOracle("P_IDENTIFICADOR", ParameterDirection.InputOutput, String.Empty, OracleClient.OracleType.VarChar, 36))

                    If Peticion.CodigoEstado = Prosegur.Genesis.Comon.Enumeradores.EstadoCertificado.Definitivo.RecuperarValor Then
                        comando.Parameters.Add(Util.CriarParametroOracle("P_CODIGO", ParameterDirection.InputOutput, Peticion.CodigoCertificadoDefinitivo, OracleClient.OracleType.VarChar, 500))
                    Else
                        comando.Parameters.Add(Util.CriarParametroOracle("P_CODIGO", ParameterDirection.InputOutput, Peticion.CodigoCertificado, OracleClient.OracleType.VarChar, 500))
                    End If

                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "P_CODIGO_ESTADO_CONVERSION", ProsegurDbType.Observacao_Longa, Peticion.CodigoEstado))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "P_DESCRIPCION_USUARIO", ProsegurDbType.Observacao_Longa, Peticion.UsuarioCreacion))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "P_NUEVO_CODIGO", ProsegurDbType.Observacao_Longa, nuevoCodigo))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "P_CULTURA", ProsegurDbType.Observacao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                                                                                                                           Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                                                                                                                           Tradutor.CulturaSistema.Name,
                                                                                                                                                                          If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty))))
                    comando.Parameters.Add(Util.CriarParametroOracle("P_ERROR", ParameterDirection.Output, String.Empty, OracleClient.OracleType.VarChar, 4000))

                    ' executar consulta
                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, comando)

                    objErro = Util.AtribuirValorObj(comando.Parameters("P_ERROR").Value, GetType(String))

                    If Not String.IsNullOrEmpty(objErro) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, objErro.Replace("ORA-20001:", String.Empty))
                    End If


                End Using

            Catch ex As Excepcion.NegocioExcepcion
                Throw ex
            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If

            End Try

        End Sub
        
#End Region

#Region "[CONSULTAR]"

        ''' <summary>
        ''' Retorna o codigo do certificado
        ''' </summary>
        ''' <param name="CodigoCertificado"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 21/06/2013 - Criado
        ''' </history>
        Public Shared Function RecuperarCodigoCertificado(CodigoCertificado As String) As String

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CertificadoRecuperarCodigo)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CERTIFICADO", ProsegurDbType.Observacao_Curta, CodigoCertificado))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)
        End Function

        ''' <summary>
        ''' Retorna os certificados
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 27/05/2013 - Criado
        ''' </history>
        Public Shared Function RetornarCertificados(Peticion As CSCertificacion.DatosCertificacion.Peticion, _
                                                    RecuperarCertificadoCompleto As Boolean,
                                                    RecuperarCertificadoPorCodigoExterno As Boolean, _
                                                    Optional RecuperarCertificadoDefinitivo As Boolean = False) As List(Of CSCertificacion.Certificado)

            Dim objCertificados As List(Of CSCertificacion.Certificado) = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.CertificadoRecuperar
            cmd.CommandType = CommandType.Text

            Dim CodigoCliente As String = String.Empty

            If Not String.IsNullOrEmpty(Peticion.CodigoExterno) AndAlso
               (Peticion.CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO OrElse RecuperarCertificadoPorCodigoExterno) Then

                Dim CodigoCertificado As String = String.Empty

                If Peticion.CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO AndAlso RecuperarCertificadoPorCodigoExterno Then
                    CodigoCertificado = Peticion.CodigoCertificadoDefinitivo
                Else
                    CodigoCertificado = Peticion.CodigoCertificado
                End If

                cmd.CommandText = String.Format(cmd.CommandText, " CT.COD_EXTERNO = []COD_EXTERNO ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_EXTERNO", ProsegurDbType.Descricao_Longa, CodigoCertificado))

            Else

                If Peticion.CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE OrElse _
                   Peticion.CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_SIN_CIERRE Then

                    Dim Estados As New List(Of String)

                    Estados.Add(Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE)
                    Estados.Add(Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_SIN_CIERRE)

                    If RecuperarCertificadoDefinitivo Then
                        Estados.Add(Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO)
                    End If

                    cmd.CommandText = String.Format(cmd.CommandText, " CL.COD_CLIENTE = []COD_CLIENTE ")

                    cmd.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Estados, "COD_ESTADO", cmd, "AND", "CT")

                Else

                    cmd.CommandText = String.Format(cmd.CommandText, " CL.COD_CLIENTE = []COD_CLIENTE AND CT.COD_ESTADO = []COD_ESTADO ")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodigoEstado))

                End If

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, If(Peticion.Cliente IsNot Nothing, Peticion.Cliente.Codigo, String.Empty)))

                cmd.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.CodigosDelegaciones, "COD_DELEGACION", cmd, "AND", "D")
                cmd.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.CodigosSectores, "COD_SECTOR", cmd, "AND", "S")
                cmd.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.CodigosSubCanales, "COD_SUBCANAL", cmd, "AND", "SC")

                If Peticion.EsTodosSectores Then
                    cmd.CommandText &= " AND CT.BOL_TODOS_SECTORES = []BOL_TODOS_SECTORES"
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_TODOS_SECTORES", ProsegurDbType.Logico, 1))
                End If

                If Peticion.EsTodosCanales Then
                    cmd.CommandText &= " AND CT.BOL_TODOS_CANALES = []BOL_TODOS_CANALES"
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_TODOS_CANALES", ProsegurDbType.Logico, 1))
                End If

                If Peticion.EsTodasDelegaciones Then
                    cmd.CommandText &= " AND CT.BOL_TODAS_DELEGACIONES = []BOL_TODAS_DELEGACIONES"
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_TODAS_DELEGACIONES", ProsegurDbType.Logico, 1))
                End If

                cmd.CommandText &= " ORDER BY CT.OID_CERTIFICADO"

            End If

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objCertificados = New List(Of CSCertificacion.Certificado)

                CodigoCliente = Util.AtribuirValorObj(dt.Rows(0)("COD_CLIENTE"), GetType(String))

                Dim OidCertificado As String = String.Empty
                Dim CodSubCanal As String = String.Empty
                Dim CodSector As String = String.Empty
                Dim CodDelegacion As String = String.Empty

                Dim objCertificado As CSCertificacion.Certificado = Nothing

                For Each dr In dt.Rows

                    OidCertificado = Util.AtribuirValorObj(dr("OID_CERTIFICADO"), GetType(String))
                    CodSubCanal = Util.AtribuirValorObj(dr("COD_SUBCANAL"), GetType(String))
                    CodSector = Util.AtribuirValorObj(dr("COD_SECTOR"), GetType(String))
                    CodDelegacion = Util.AtribuirValorObj(dr("COD_DELEGACION"), GetType(String))

                    If objCertificados.FindAll(Function(c) c.IdentificadorCertificado = OidCertificado).Count = 0 Then

                        Dim _FyhCertificado As DateTime = Util.AtribuirValorObj(dr("FYH_CERTIFICADO"), GetType(DateTime))
                        objCertificados.Add(New CSCertificacion.Certificado With { _
                                            .IdentificadorCertificado = OidCertificado, _
                                            .CodigoExterno = Util.AtribuirValorObj(dr("COD_EXTERNO"), GetType(String)), _
                                            .CodigoCertificado = Util.AtribuirValorObj(dr("COD_CERTIFICADO"), GetType(String)), _
                                            .CodigoEstado = Util.AtribuirValorObj(dr("COD_ESTADO"), GetType(String)), _
                                            .FyhCertificado = _FyhCertificado.QuieroExibirEstaFechaEnLaPatalla(Peticion.DelegacionLogada), _
                                            .CodigosDelegaciones = New List(Of String), _
                                            .CodigosSectores = New List(Of String), _
                                            .CodigosSubCanales = New List(Of String)})
                    End If

                    objCertificado = (From cert In objCertificados Where cert.IdentificadorCertificado = OidCertificado).FirstOrDefault

                    If objCertificado.CodigosSubCanales.FindAll(Function(sb) sb = CodSubCanal).Count = 0 Then
                        objCertificado.CodigosSubCanales.Add(CodSubCanal)
                    End If


                    If objCertificado.CodigosSectores.FindAll(Function(s) s = CodSector).Count = 0 Then
                        objCertificado.CodigosSectores.Add(CodSector)
                    End If


                    If objCertificado.CodigosDelegaciones.FindAll(Function(d) d = CodDelegacion).Count = 0 Then
                        objCertificado.CodigosDelegaciones.Add(CodDelegacion)
                    End If

                Next

                'Recupera todas as informações dos certificados
                If RecuperarCertificadoCompleto Then

                    If objCertificados IsNot Nothing AndAlso objCertificados.Count > 0 Then

                        'Para cada certificado recupera todos os seus dados.
                        For Each objCertificado In objCertificados
                            'Recupera as contas do certificado
                            objCertificado.Cuentas = Cuenta.RecuperarCuentas(objCertificado.IdentificadorCertificado)
                        Next

                    End If

                End If

            End If

            Return objCertificados
        End Function

        Public Shared Function RecuperaFechaUltimoCertificado(identificadorCuentaOrigen As String, _
                                                              identificadorCuentaDestino As String) As DateTime

            Dim objCertificados As List(Of CSCertificacion.Certificado) = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.RecuperaFechaUltimoCertificado
            cmd.CommandType = CommandType.Text

            Dim lstCuentas As New List(Of String) From {identificadorCuentaOrigen}

            If Not String.IsNullOrEmpty(identificadorCuentaDestino) Then
                lstCuentas.Add(identificadorCuentaDestino)
            End If

            Dim filtro As String = Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, lstCuentas, "OID_CUENTA", cmd, "AND")

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, filtro))

            Dim fecha = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)

            If fecha IsNot DBNull.Value Then
                Return fecha
            End If

            Return DateTime.MinValue

        End Function

        Public Shared Function RecuperarFiltrosCertificados(Peticion As CSCertificacion.DatosCertificacion.Peticion) As List(Of CSCertificacion.Certificado)

            Dim certificados As New List(Of CSCertificacion.Certificado)
            Dim drCertificados As IDataReader = Nothing
            Dim objErro As String = String.Empty

            Dim conexao As IDbConnection = AcessoDados.Conectar(Constantes.CONEXAO_GENESIS)

            ' criar comando
            Dim comando As IDbCommand = conexao.CreateCommand()

            Try

                comando.CommandText = Constantes.SP_SALDOS_RECUPERAR_FILTROS_CERTIFICADO
                comando.CommandType = CommandType.StoredProcedure

                comando.Parameters.Add(Util.CriarParametroOracle("P_IDENTIFICADOR", ParameterDirection.Input, Peticion.IdentificadorCertificado, OracleClient.OracleType.VarChar, 36))

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "P_CULTURA", OracleClient.OracleType.VarChar, If(Tradutor.CulturaSistema IsNot Nothing AndAlso Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name), Tradutor.CulturaSistema.Name,
                                                                                                                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), 36))

                comando.Parameters.Add(Util.CriarParametroOracle("P_CERTIFICADO", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("P_SECTORES", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("P_SUBCANALES", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("P_DELEGACIONES", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("P_ERROR", ParameterDirection.InputOutput, String.Empty, OracleClient.OracleType.VarChar, 4000))

                ' executar consulta
                drCertificados = AcessoDados.ExecutarDataReader(Constantes.CONEXAO_GENESIS, comando)

                objErro = Util.AtribuirValorObj(comando.Parameters("P_ERROR").Value, GetType(String))

                If Not String.IsNullOrEmpty(objErro) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, objErro.Replace("ORA-20001:", String.Empty))
                End If

                certificados = RecuperarDatosCertificados(drCertificados, Peticion.DelegacionLogada)

            Catch ex As Excepcion.NegocioExcepcion
                Throw

            Catch ex As Exception

                If ex.Message.Contains("ORA-20001") Then

                    Dim MsgErro As String = ex.Message

                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErro.Replace("ORA-20001", String.Empty))
                Else
                    Throw ex
                End If

            Finally

                ' Fecha a conexão do Data Reader
                If drCertificados IsNot Nothing Then
                    drCertificados.Close()
                    drCertificados.Dispose()
                End If

                ' Fecha a conexão do banco
                AcessoDados.Desconectar(conexao)
            End Try

            Return certificados

        End Function

        Private Shared Function RecuperarDatosCertificados(drCertificados As IDataReader, DelegacionLogada As Clases.Delegacion) As List(Of CSCertificacion.Certificado)

            Dim certificados As New List(Of CSCertificacion.Certificado)

            Dim certificado As New CSCertificacion.Certificado

            certificados.Clear()

            If (drCertificados.Read) Then
                ' popular certificado
                CargarCertificado(drCertificados, DelegacionLogada, certificado)
            End If

            ' Vai para o próximo cursor
            drCertificados.NextResult()

            While drCertificados.Read
                'popular sectores
                CargarSectores(drCertificados, certificado)
            End While

            ' Vai para o próximo cursor
            drCertificados.NextResult()

            While drCertificados.Read
                'Popular subcanais
                CargarSubCanales(drCertificados, certificado)
            End While

            ' Vai para o próximo cursor
            drCertificados.NextResult()

            While drCertificados.Read
                ' popular delegaciones
                CargarDelegaciones(drCertificados, certificado)
            End While

            certificados.Add(certificado)

            Return certificados

        End Function

        Public Shared Function RetornarCertificadosRelatorio(Peticion As CSCertificacion.DatosCertificacion.Peticion) As List(Of CSCertificacion.Certificado)

            Dim objCertificados As List(Of CSCertificacion.Certificado) = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.CertificadoRetornarCertificadosRelatorio
            cmd.CommandType = CommandType.Text

            Dim CodigoCliente As String = String.Empty

            If Not String.IsNullOrEmpty(Peticion.CodigoCertificado) Then

                cmd.CommandText = String.Format(cmd.CommandText, " CT.COD_EXTERNO = []COD_EXTERNO ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_EXTERNO", ProsegurDbType.Descricao_Longa, Peticion.CodigoCertificado))

                If Peticion.CodigosDelegaciones IsNot Nothing AndAlso Peticion.CodigosDelegaciones.Count > 0 Then
                    cmd.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.CodigosDelegaciones, "COD_DELEGACION", cmd, "AND", "D")
                End If

            Else

                Dim Estados As New List(Of String)

                If Peticion.CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE OrElse _
                   Peticion.CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_SIN_CIERRE Then

                    Estados.Add(Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE)
                    Estados.Add(Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_SIN_CIERRE)

                Else

                    Estados.Add(Peticion.CodigoEstado)

                End If

                Dim strSql As New List(Of String)
                If Peticion.Cliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(Peticion.Cliente.Codigo) Then
                    strSql.Add(" CL.COD_CLIENTE = []COD_CLIENTE")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, If(Peticion.Cliente IsNot Nothing, Peticion.Cliente.Codigo, String.Empty)))
                End If

                If Peticion.FyhCertificado <> Date.MinValue Then
                    strSql.Add(" CT.FYH_CERTIFICADO >= []FYH_CERTIFICADO ")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_CERTIFICADO", ProsegurDbType.Data_Hora, Peticion.FyhCertificado.QuieroGrabarGMTZeroEnLaBBDD(Peticion.DelegacionLogada)))
                End If

                If Peticion.CodigosDelegaciones IsNot Nothing AndAlso Peticion.CodigosDelegaciones.Count > 0 Then
                    strSql.Add(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.CodigosDelegaciones, "COD_DELEGACION", cmd, "", "D"))
                End If

                strSql.Add(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Estados, "COD_ESTADO", cmd, "", "CT"))

                cmd.CommandText = String.Format(cmd.CommandText, String.Join(" AND ", strSql))
            End If

            cmd.CommandText &= " ORDER BY FYH_CERTIFICADO DESC,COD_CONFIG_REPORTE, DES_SUBCANAL"

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objCertificados = New List(Of CSCertificacion.Certificado)

                CodigoCliente = Util.AtribuirValorObj(dt.Rows(0)("COD_CLIENTE"), GetType(String))

                Dim OidCertificado As String = String.Empty
                Dim CodSubCanal As String = String.Empty
                Dim CodDelegacion As String = String.Empty

                Dim objCertificado As CSCertificacion.Certificado = Nothing
                Dim lstPlantas As New Dictionary(Of String, List(Of Clases.Planta))

                'Recupera todas as plantas de cada delegação
                For Each OID_DELEGACION In (From r In dt.AsEnumerable()
                                              Select r.Field(Of String)("OID_DELEGACION")).Distinct()

                    lstPlantas.Add(OID_DELEGACION, Prosegur.Genesis.AccesoDatos.Genesis.Planta.RecuperarPlantas(OID_DELEGACION).ToList())
                Next

                For Each Certificado In (From tab In dt.AsEnumerable()
                Group tab By OID_CERTIFICADO = tab.Field(Of String)("OID_CERTIFICADO"),
                             OID_SUBCANAL = tab.Field(Of String)("OID_SUBCANAL"),
                             OID_CONFIG_REPORTE = tab.Field(Of String)("OID_CONFIG_REPORTE"),
                             FYH_CERTIFICADO = tab.Field(Of DateTime)("FYH_CERTIFICADO"),
                             COD_CONFIG_REPORTE = tab.Field(Of String)("COD_CONFIG_REPORTE"),
                             DES_SUBCANAL = tab.Field(Of String)("DES_SUBCANAL")
                    Into Group
                             Order By FYH_CERTIFICADO Descending, COD_CONFIG_REPORTE, DES_SUBCANAL
                    Select OID_SUBCANAL, OID_CONFIG_REPORTE, OID_CERTIFICADO)

                    objCertificado = New CSCertificacion.Certificado
                    objCertificado.CodigosDelegaciones = New List(Of String)

                    'Para cada cerficicado recupera os dados e as plantas do memso
                    For Each dr In dt.Select(String.Format("OID_CERTIFICADO ='{0}' AND OID_SUBCANAL='{1}' AND OID_CONFIG_REPORTE='{2}'", Certificado.OID_CERTIFICADO, Certificado.OID_SUBCANAL, Certificado.OID_CONFIG_REPORTE))

                        OidCertificado = Util.AtribuirValorObj(dr("OID_CERTIFICADO"), GetType(String))
                        CodSubCanal = Util.AtribuirValorObj(dr("COD_SUBCANAL"), GetType(String))
                        CodDelegacion = Util.AtribuirValorObj(dr("COD_DELEGACION"), GetType(String))

                        With objCertificado
                            .IdentificadorCertificado = OidCertificado
                            .CodigoExterno = Util.AtribuirValorObj(dr("COD_EXTERNO"), GetType(String))
                            .CodigoCertificado = Util.AtribuirValorObj(dr("COD_CERTIFICADO"), GetType(String))
                            .CodigoEstado = Util.AtribuirValorObj(dr("COD_ESTADO"), GetType(String))
                            .DescripcionEstado = EnumExtension.RecuperarEnum(Of Enumeradores.EstadoCertificado)(Util.AtribuirValorObj(dr("COD_ESTADO"), GetType(String))).ToString()
                            .FyhCertificado = Util.AtribuirValorObj(dr("FYH_CERTIFICADO"), GetType(DateTime))
                            .FyhCertificado = .FyhCertificado.QuieroExibirEstaFechaEnLaPatalla(Peticion.DelegacionLogada)
                            .CodigosSectores = New List(Of String)
                            .CodigosSubCanales = New List(Of String)
                            .SubCanales = New List(Of Clases.SubCanal)
                            .ConfigReporte = New Clases.ConfiguracionReporte
                            .Plantas = New List(Of Clases.Planta)
                        End With

                        objCertificado.CodigosSubCanales.Add(CodSubCanal)
                        objCertificado.CodigosDelegaciones.Add(CodDelegacion)

                        With objCertificado.ConfigReporte
                            .Identificador = Util.AtribuirValorObj(dr("OID_CONFIG_REPORTE"), GetType(String))
                            .Codigo = Util.AtribuirValorObj(dr("COD_CONFIG_REPORTE"), GetType(String))
                            .Descripcion = Util.AtribuirValorObj(dr("DES_CONFIG_REPORTE"), GetType(String))
                            .Direccion = Util.AtribuirValorObj(dr("DES_DIRECCION"), GetType(String))

                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(dr("COD_SITUACION_REPORTE"), GetType(String))) Then
                                .ResultadoReporte = New Clases.ResultadoReporte
                                .ResultadoReporte.CodigoEstado = Util.AtribuirValorObj(dr("COD_SITUACION_REPORTE"), GetType(String))
                                .ResultadoReporte.DescripcionErro = Util.AtribuirValorObj(dr("DES_ERROR_EJECUCION"), GetType(String))
                                .ResultadoReporte.FechaInicioEjecucion = Util.AtribuirValorObj(dr("FYH_INICIO_EJECUCION"), GetType(DateTime))
                                If .ResultadoReporte.FechaInicioEjecucion <> DateTime.MinValue Then
                                    .ResultadoReporte.FechaInicioEjecucion = .ResultadoReporte.FechaInicioEjecucion.QuieroExibirEstaFechaEnLaPatalla(Peticion.DelegacionLogada)
                                End If
                                .ResultadoReporte.FechaFinEjecucion = Util.AtribuirValorObj(dr("FYH_FIN_EJECUCION"), GetType(DateTime))
                                If .ResultadoReporte.FechaFinEjecucion <> DateTime.MinValue Then
                                    .ResultadoReporte.FechaFinEjecucion = .ResultadoReporte.FechaFinEjecucion.QuieroExibirEstaFechaEnLaPatalla(Peticion.DelegacionLogada)
                                End If
                            End If

                        End With

                        Dim objSubCanal As New Clases.SubCanal
                        With objSubCanal
                            .Identificador = Util.AtribuirValorObj(dr("OID_SUBCANAL"), GetType(String))
                            .Codigo = Util.AtribuirValorObj(dr("COD_SUBCANAL"), GetType(String))
                            .Descripcion = Util.AtribuirValorObj(dr("DES_SUBCANAL"), GetType(String))
                        End With
                        objCertificado.SubCanales.Add(objSubCanal)

                        Dim objCliente As New Clases.Cliente
                        With objCliente
                            .Identificador = Util.AtribuirValorObj(dr("OID_CLIENTE"), GetType(String))
                            .Codigo = Util.AtribuirValorObj(dr("COD_CLIENTE"), GetType(String))
                            .Descripcion = Util.AtribuirValorObj(dr("DES_CLIENTE"), GetType(String))
                        End With
                        objCertificado.Cliente = objCliente

                        If Not lstPlantas.ContainsKey(Util.AtribuirValorObj(dr("OID_DELEGACION"), GetType(String))) Then
                            lstPlantas.Add(Util.AtribuirValorObj(dr("OID_DELEGACION"), GetType(String)), Prosegur.Genesis.AccesoDatos.Genesis.Planta.RecuperarPlantas(Util.AtribuirValorObj(dr("OID_DELEGACION"), GetType(String))).ToList())
                        End If

                        objCertificado.Plantas.AddRange(lstPlantas.Item(Util.AtribuirValorObj(dr("OID_DELEGACION"), GetType(String))))



                    Next

                    objCertificados.Add(objCertificado)
                Next

            End If

            Return objCertificados
        End Function

        ''' <summary>
        ''' Recupera o Identificador do ultimo certicado
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 27/05/2013 - Criado
        ''' </history>
        Public Shared Function RecuperarOidUltimoCertificado(Peticion As CSCertificacion.DatosCertificacion.Peticion,
                                                             CodEstado As String) As String

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.CertificadoRetornarOid
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, If(Peticion.Cliente IsNot Nothing, Peticion.Cliente.Codigo, String.Empty)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Identificador_Alfanumerico, CodEstado))

            cmd.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.CodigosDelegaciones, "COD_DELEGACION", cmd, "AND", "D")
            cmd.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.CodigosSectores, "COD_SECTOR", cmd, "AND", "S")
            cmd.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.CodigosSubCanales, "COD_SUBCANAL", cmd, "AND", "SC")
            cmd.CommandText &= " ORDER BY CT.GMT_MODIFICACION DESC"
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            Dim IdentificadorCertificado As String = String.Empty

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                IdentificadorCertificado = Util.AtribuirValorObj(dt.Rows(0)("OID_CERTIFICADO"), GetType(String))
            End If

            Return IdentificadorCertificado
        End Function

        ''' <summary>
        ''' Recupera pelo cliente totalizador de saldos as configurações de nivel de movimentos
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [adans.klevanskis] 31/05/2013 - Criado
        ''' </history>
        Public Shared Function ObtenerNivelSaldos(Peticion As CSCertificacion.ObtenerNivelSaldos.Peticion) _
            As CSCertificacion.ObtenerNivelSaldos.NivelSaldos

            Dim objRespuesta As CSCertificacion.ObtenerNivelSaldos.NivelSaldos = Nothing
            Dim filtros As New System.Text.StringBuilder
            Dim indice As Integer = 0

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            comando.CommandText = My.Resources.ObtenerNivelSaldos
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodClienteTotalizador))

            If Peticion.CodSubCanal IsNot Nothing Then
                For Each Codigo In Peticion.CodSubCanal
                    indice += 1
                    If filtros.Length > 0 Then
                        filtros.Append(",")
                    End If
                    filtros.AppendLine("'" & Codigo & "'")
                Next

                If filtros.Length > 0 Then
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCANAL", ProsegurDbType.Descricao_Longa, filtros.ToString()))
                End If
            End If

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objRespuesta = New CSCertificacion.ObtenerNivelSaldos.NivelSaldos

                For Each dr In dt.Rows
                    Dim Totalizador As Boolean = Util.AtribuirValorObj(dr("ES_TOTALIZADOR"), GetType(Boolean))
                    If Totalizador Then
                        If objRespuesta.TotalizaSaldos Is Nothing Then
                            objRespuesta.TotalizaSaldos = New CSCertificacion.ObtenerNivelSaldos.ClienteTotalizadorDeSaldo
                        End If

                        If String.IsNullOrEmpty(objRespuesta.TotalizaSaldos.OidConfigNivelSaldo) Then
                            objRespuesta.TotalizaSaldos = ExtrairTotalizador(dr, Peticion.Delegacion)
                        End If

                        If objRespuesta.TotalizaSaldos.ClienteTotalizaEnClienteTotalizadorSaldo Is Nothing Then
                            objRespuesta.TotalizaSaldos.ClienteTotalizaEnClienteTotalizadorSaldo = New CSCertificacion.ObtenerNivelSaldos.ClienteTotalizaEnClienteTotalizadorSaldoColeccion
                        End If

                        Dim NovoCliente As New CSCertificacion.ObtenerNivelSaldos.ClienteTotalizaEnClienteTotalizadorSaldo
                        NovoCliente = ExtrairCliente(dr, Peticion.Delegacion)

                        objRespuesta.TotalizaSaldos.ClienteTotalizaEnClienteTotalizadorSaldo.Add(NovoCliente)
                    Else

                        If objRespuesta.NoTotalizaSaldos Is Nothing Then
                            objRespuesta.NoTotalizaSaldos = New CSCertificacion.ObtenerNivelSaldos.ClienteTotalizadorDeSaldoColeccion
                        End If

                        Dim OidConfigNivelSaldo = Util.AtribuirValorObj(dt.Rows(0)("OID_CONFIG_NIVEL_SALDO"), GetType(String))

                        Dim resultado = (From p In objRespuesta.NoTotalizaSaldos
                                        Where p.OidConfigNivelSaldo.Equals(OidConfigNivelSaldo)
                                        Select p).FirstOrDefault

                        If objRespuesta.NoTotalizaSaldos.Count = 0 OrElse resultado Is Nothing Then

                            Dim NaoTotalizadorNovo As New CSCertificacion.ObtenerNivelSaldos.ClienteTotalizadorDeSaldo
                            NaoTotalizadorNovo = ExtrairTotalizador(dr, Peticion.Delegacion)
                            NaoTotalizadorNovo.ClienteTotalizaEnClienteTotalizadorSaldo = New CSCertificacion.ObtenerNivelSaldos.ClienteTotalizaEnClienteTotalizadorSaldoColeccion
                            NaoTotalizadorNovo.ClienteTotalizaEnClienteTotalizadorSaldo.Add(ExtrairCliente(dr, Peticion.Delegacion))
                            objRespuesta.NoTotalizaSaldos.Add(NaoTotalizadorNovo)
                        Else
                            resultado.ClienteTotalizaEnClienteTotalizadorSaldo.Add(ExtrairCliente(dr, Peticion.Delegacion))
                        End If

                    End If
                Next

            End If

            Return objRespuesta

        End Function

        Public Shared Function ObtenerNivelSaldosClienteTotalizador(Peticion As CSCertificacion.ObtenerNivelSaldos.Peticion) _
            As CSCertificacion.ObtenerNivelSaldos.NivelSaldos

            Dim objRespuesta As CSCertificacion.ObtenerNivelSaldos.NivelSaldos = Nothing
            Dim filtros As New System.Text.StringBuilder
            Dim indice As Integer = 0

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            comando.CommandText = My.Resources.CertificadoObtenerNivelSaldosClienteTotalizador
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodClienteTotalizador))
            If Peticion.CodSubCanal IsNot Nothing Then
                Dim paramSubCanais As String = Util.MontarParametroIn(Constantes.CONEXAO_GENESIS, Peticion.CodSubCanal, "COD_SUBCANAL", comando)
                comando.CommandText = comando.CommandText + String.Format(" AND SUBCANAL.COD_SUBCANAL in({0})", paramSubCanais)
            End If

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objRespuesta = New CSCertificacion.ObtenerNivelSaldos.NivelSaldos
                objRespuesta.TotalizaSaldos = New CSCertificacion.ObtenerNivelSaldos.ClienteTotalizadorDeSaldo
                objRespuesta.TotalizaSaldos.ClienteTotalizaEnClienteTotalizadorSaldo = New CSCertificacion.ObtenerNivelSaldos.ClienteTotalizaEnClienteTotalizadorSaldoColeccion

                For Each dr In dt.Rows
                    objRespuesta.TotalizaSaldos.ClienteTotalizaEnClienteTotalizadorSaldo.Add(ExtrairCliente(dr, Peticion.Delegacion))
                Next

            End If

            Return objRespuesta

        End Function

        ''' <summary>
        ''' Retorna todos os Certificados do cliente
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [pgoncalves] 07/06/2013 - Criado
        ''' </history>
        Public Shared Function ObtenerCertificado(Peticion As CSCertificacion.ObtenerCertificado.Peticion) As CSCertificacion.ObtenerCertificado.CertificadoColeccion
            Dim filtros As New System.Text.StringBuilder
            Dim indice As Integer = 0
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.obterCertificado)
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.codigoCliente))

            If Peticion.estadoCertificado IsNot Nothing Then
                For Each item In Peticion.estadoCertificado
                    indice += 1
                    If filtros.Length > 0 Then
                        filtros.Append(",")
                    End If
                    filtros.AppendLine("'" & item & "'")
                Next

                If filtros.Length > 0 Then
                    comando.CommandText &= " AND CERT.COD_ESTADO IN (" & filtros.ToString() & ") "
                End If
            End If

            Dim objCertificadoColeccion As New CSCertificacion.ObtenerCertificado.CertificadoColeccion

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

            If dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objCertificadoColeccion = PopulaCertificado(dt, Genesis.Delegacion.ObtenerPorOid(Peticion.IdentificadorDelegacion))
            End If

            Return objCertificadoColeccion
        End Function


#End Region

#Region "[DELETAR]"

        ''' <summary>
        ''' Deleta o saldo do certificado
        ''' </summary>
        ''' <param name="OidCertificado"></param>
        ''' <param name="objTransacion"></param>
        ''' <remarks></remarks>
        Public Shared Sub DeletarCertificado(OidCertificado As String, ByRef objTransacion As Transacao)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CertificadoDeletar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CERTIFICADO", ProsegurDbType.Objeto_Id, OidCertificado))

            objTransacion.AdicionarItemTransacao(cmd)
        End Sub

        Public Shared Sub BorrarCertificado(IdentificadorCertificado As String)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            ' obter procedure
            comando.CommandText = Constantes.SP_SALDOS_BORRAR_CERTIFICADO
            comando.CommandType = CommandType.StoredProcedure

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "P_OID_CERTIFICADO", ProsegurDbType.Objeto_Id, IdentificadorCertificado))

            ' executar consulta
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, comando)

        End Sub

#End Region

#Region "[METODOS AUXILIARES]"

        Private Shared Function ExtrairCliente(dr As Object, Delegacion As Clases.Delegacion) As CSCertificacion.ObtenerNivelSaldos.ClienteTotalizaEnClienteTotalizadorSaldo
            Dim NovoCliente As New CSCertificacion.ObtenerNivelSaldos.ClienteTotalizaEnClienteTotalizadorSaldo
            NovoCliente.BolActivo = Util.AtribuirValorObj(dr("BOL_ACTIVO_MOVIMIENTO"), GetType(Boolean))
            NovoCliente.CodCliente = Util.AtribuirValorObj(dr("COD_CLIENTE_MOVIMIENTO"), GetType(String))
            NovoCliente.CodPtoServicio = Util.AtribuirValorObj(dr("COD_PTO_SERVICIO_MOVIMIENTO"), GetType(String))
            NovoCliente.CodSubcanal = Util.AtribuirValorObj(dr("COD_SUBCANAL_MOVIMIENTO"), GetType(String))
            NovoCliente.CodSubcliente = Util.AtribuirValorObj(dr("COD_SUBCLIENTE_MOVIMIENTO"), GetType(String))
            NovoCliente.DesCliente = Util.AtribuirValorObj(dr("DES_CLIENTE_MOVIMIENTO"), GetType(String))
            NovoCliente.DesPtoServicio = Util.AtribuirValorObj(dr("DES_PTO_SERVICIO_MOVIMIENTO"), GetType(String))
            NovoCliente.DesSubcanal = Util.AtribuirValorObj(dr("DES_SUBCANAL_MOVIMIENTO"), GetType(String))
            NovoCliente.DesSubcliente = Util.AtribuirValorObj(dr("DES_SUBCLIENTE_MOVIMIENTO"), GetType(String))
            NovoCliente.DesUsuarioCreacion = Util.AtribuirValorObj(dr("DES_USUARIO_CREAC_MOVIMIENTO"), GetType(String))
            NovoCliente.DesUsuarioModificacion = Util.AtribuirValorObj(dr("DES_USUARIO_MOD_MOVIMIENTO"), GetType(String))
            NovoCliente.FyhVigencia = Util.AtribuirValorObj(dr("FYH_VIGENCIA_MOVIMIENTO"), GetType(DateTime))
            NovoCliente.FyhVigencia = NovoCliente.FyhVigencia.QuieroExibirEstaFechaEnLaPatalla(Delegacion)
            NovoCliente.GmtCreacion = Util.AtribuirValorObj(dr("GMT_CREACION_MOVIMIENTO"), GetType(DateTime))
            NovoCliente.GmtCreacion = NovoCliente.GmtCreacion.QuieroExibirEstaFechaEnLaPatalla(Delegacion)
            NovoCliente.GmtModificacion = Util.AtribuirValorObj(dr("GMT_MODIFICACION_MOVIMIENTO"), GetType(DateTime))
            NovoCliente.GmtModificacion = NovoCliente.GmtModificacion.QuieroExibirEstaFechaEnLaPatalla(Delegacion)
            NovoCliente.OidCliente = Util.AtribuirValorObj(dr("OID_CLIENTE_MOVIMIENTO"), GetType(String))
            NovoCliente.OidConfigNivelMovimiento = Util.AtribuirValorObj(dr("OID_CONFIG_NIVEL_MOVIMIENTO"), GetType(String))
            NovoCliente.OidPtoServicio = Util.AtribuirValorObj(dr("OID_PTO_SERVICIO_MOVIMIENTO"), GetType(String))
            NovoCliente.OidSubcanal = Util.AtribuirValorObj(dr("OID_SUBCANAL_MOVIMIENTO"), GetType(String))
            NovoCliente.OidSubcliente = Util.AtribuirValorObj(dr("OID_SUBCLIENTE_MOVIMIENTO"), GetType(String))
            Return NovoCliente
        End Function

        Private Shared Function ExtrairTotalizador(dr As Object, Delegacion As Clases.Delegacion) As CSCertificacion.ObtenerNivelSaldos.ClienteTotalizadorDeSaldo
            Dim TotalizaSaldos As New CSCertificacion.ObtenerNivelSaldos.ClienteTotalizadorDeSaldo
            TotalizaSaldos.CodCliente = Util.AtribuirValorObj(dr("COD_CLIENTE_SALDO"), GetType(String))
            TotalizaSaldos.CodPtoServicio = Util.AtribuirValorObj(dr("COD_PTO_SERVICIO_SALDO"), GetType(String))
            TotalizaSaldos.CodSubcliente = Util.AtribuirValorObj(dr("COD_SUBCLIENTE_SALDO"), GetType(String))
            TotalizaSaldos.DesCliente = Util.AtribuirValorObj(dr("DES_CLIENTE_SALDO"), GetType(String))
            TotalizaSaldos.DesPtoServicio = Util.AtribuirValorObj(dr("DES_PTO_SERVICIO_SALDO"), GetType(String))
            TotalizaSaldos.DesSubcliente = Util.AtribuirValorObj(dr("DES_SUBCLIENTE_SALDO"), GetType(String))
            TotalizaSaldos.DesUsuarioCreacion = Util.AtribuirValorObj(dr("DES_USUARIO_CREACION_SALDO"), GetType(String))
            TotalizaSaldos.DesUsuarioModificacion = Util.AtribuirValorObj(dr("DES_USUARIO_MODIFICACION_SALDO"), GetType(String))
            TotalizaSaldos.GmtCreacion = Util.AtribuirValorObj(dr("GMT_CREACION_SALDO"), GetType(DateTime))
            TotalizaSaldos.GmtCreacion = TotalizaSaldos.GmtCreacion.QuieroExibirEstaFechaEnLaPatalla(Delegacion)
            TotalizaSaldos.GmtModificacion = Util.AtribuirValorObj(dr("GMT_MODIFICACION_SALDO"), GetType(DateTime))
            TotalizaSaldos.GmtModificacion = TotalizaSaldos.GmtModificacion.QuieroExibirEstaFechaEnLaPatalla(Delegacion)
            TotalizaSaldos.OidCliente = Util.AtribuirValorObj(dr("OID_CLIENTE_SALDO"), GetType(String))
            TotalizaSaldos.OidConfigNivelSaldo = Util.AtribuirValorObj(dr("OID_CONFIG_NIVEL_SALDO"), GetType(String))
            TotalizaSaldos.OidPtoServicio = Util.AtribuirValorObj(dr("OID_PTO_SERVICIO_SALDO"), GetType(String))
            TotalizaSaldos.OidSubcliente = Util.AtribuirValorObj(dr("OID_SUBCLIENTE_SALDO"), GetType(String))
            Return TotalizaSaldos
        End Function

        ''' <summary>
        ''' Valida se já existe uma certificação para a data/hora informada
        ''' </summary>
        ''' <param name="cuentaSaldoOrigen">Conta saldo de origem.</param>
        ''' <param name="cuentaSaldoDestino">Conta saldo de destino.</param>
        ''' <param name="fechaHoraPlanCertificacion">Data/Hora Plano Certificação.</param>
        ''' <param name="codigoCertificadoConflitante">[PARAMETRO DE RETORNO OPCIONAL] Código do certificado que está conflitante com a data.</param>
        ''' <returns>True: Caso a data informada seja válida. / False: Caso a data informada seja inválida</returns>
        ''' <remarks></remarks>
        Public Shared Function EsFechaHoraPlanCertificacionValida(cuentaSaldoOrigen As Clases.Cuenta,
                                                                  cuentaSaldoDestino As Clases.Cuenta,
                                                                  fechaHoraPlanCertificacion As DateTime,
                                                         Optional ByRef codigoCertificadoConflitante As String = "",
                                                         Optional ByRef fechaHoraPlanCertificacionAtual As Date? = Nothing) As Boolean

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                Dim sqlResource = My.Resources.CertificadoEsFechaHoraPlanCertificacionValida
                Dim cuentas As New ObservableCollection(Of Clases.Cuenta) 'Armazena os identificadores das contas saldo de origem e destino.
                Dim identificadoresSectores As New List(Of String)
                Dim identificadoresClientes As New List(Of String)
                Dim identificadoresSubCanales As New List(Of String)
                Dim sqlWhere As String = String.Empty
                Dim sqlOrderBy As String = String.Empty

                If (cuentaSaldoOrigen IsNot Nothing) Then
                    cuentas.Add(cuentaSaldoOrigen)
                End If

                If (cuentaSaldoDestino IsNot Nothing) Then
                    cuentas.Add(cuentaSaldoDestino)
                End If

                For Each cuentaSaldo In cuentas
                    If (cuentaSaldo.Cliente IsNot Nothing) Then
                        identificadoresClientes.Add(cuentaSaldo.Cliente.Identificador)
                    End If

                    If (cuentaSaldo.Sector IsNot Nothing) Then
                        identificadoresSectores.Add(cuentaSaldo.Sector.Identificador)
                    End If

                    If (cuentaSaldo.SubCanal IsNot Nothing) Then
                        identificadoresSubCanales.Add(cuentaSaldo.SubCanal.Identificador)
                    End If
                Next

                sqlWhere &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresClientes, "OID_CLIENTE", cmd, "AND", "NS")
                sqlWhere &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresSectores, "OID_SECTOR", cmd, "AND", "CTS")
                sqlWhere &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresSubCanales, "OID_SUBCANAL", cmd, "AND", "CTSC")
                sqlOrderBy = " ORDER BY CT.FYH_CERTIFICADO DESC "

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_CERTIFICADO", ProsegurDbType.Data_Hora, fechaHoraPlanCertificacion.QuieroGrabarGMTZeroEnLaBBDD(cuentaSaldoDestino.Sector.Delegacion)))

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, sqlResource & sqlWhere & sqlOrderBy)

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    codigoCertificadoConflitante = Util.AtribuirValorObj(dt.Rows(0)("COD_CERTIFICADO"), GetType(String))
                    Dim _fechaHora As DateTime = Util.AtribuirValorObj(dt.Rows(0)("FYH_CERTIFICADO"), GetType(DateTime))
                    fechaHoraPlanCertificacionAtual = _fechaHora.QuieroExibirEstaFechaEnLaPatalla(cuentaSaldoDestino.Sector.Delegacion)

                End If

                Return String.IsNullOrEmpty(codigoCertificadoConflitante)
            End Using
        End Function

        ''' <summary>
        ''' Preenche a coleção de certificados
        ''' </summary>
        ''' <param name="dt"></param>
        ''' <param name="delegacion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function PopulaCertificado(dt As DataTable, Delegacion As Clases.Delegacion) As CSCertificacion.ObtenerCertificado.CertificadoColeccion

            Dim objCertificadoCollecion As New CSCertificacion.ObtenerCertificado.CertificadoColeccion
            Dim objCertificado As CSCertificacion.ObtenerCertificado.Certificado = Nothing

            For Each dr In dt.Rows
                objCertificado = New CSCertificacion.ObtenerCertificado.Certificado
                objCertificado.IdentificadorCertificado = Util.AtribuirValorObj(dr("OID_CERTIFICADO"), GetType(String))
                objCertificado.codCertificado = Util.AtribuirValorObj(dr("COD_CERTIFICADO"), GetType(String))
                objCertificado.codEstado = Util.AtribuirValorObj(dr("COD_ESTADO"), GetType(String))
                objCertificado.fyhCertificado = Util.AtribuirValorObj(dr("FYH_CERTIFICADO"), GetType(DateTime))
                objCertificado.fyhCertificado = objCertificado.fyhCertificado.QuieroExibirEstaFechaEnLaPatalla(Delegacion)
                objCertificadoCollecion.Add(objCertificado)
            Next

            Return objCertificadoCollecion
        End Function

        Private Shared Sub CargarCertificado(drCertificados As IDataReader,
                                             Delegacion As Clases.Delegacion,
                                             ByRef certificado As CSCertificacion.Certificado)

            If drCertificados.GetName(0).Equals("OID_CERTIFICADO") Then
                certificado.IdentificadorCertificado = Util.AtribuirValorObj(drCertificados("OID_CERTIFICADO"), GetType(String))
            ElseIf drCertificados.GetName(0).Equals("COD_CERTIFICADO") Then
                certificado.CodigoCertificado = Util.AtribuirValorObj(drCertificados("COD_CERTIFICADO"), GetType(String))
            End If

            certificado.CodigoExterno = Util.AtribuirValorObj(drCertificados("COD_EXTERNO"), GetType(String))
            certificado.CodigoEstado = Util.AtribuirValorObj(drCertificados("COD_ESTADO"), GetType(String))
            certificado.FyhCertificado = Util.AtribuirValorObj(drCertificados("FYH_CERTIFICADO"), GetType(DateTime))
            certificado.FyhCertificado = certificado.FyhCertificado.QuieroExibirEstaFechaEnLaPatalla(Delegacion)
            certificado.Cliente = New Clases.Cliente
            certificado.Cliente.Identificador = Util.AtribuirValorObj(drCertificados("OID_CLIENTE"), GetType(String))
            certificado.Cliente.Codigo = Util.AtribuirValorObj(drCertificados("COD_CLIENTE"), GetType(String))
            certificado.Cliente.Descripcion = Util.AtribuirValorObj(drCertificados("DES_CLIENTE"), GetType(String))

        End Sub

        Private Shared Sub CargarSectores(drCertificados As IDataReader, ByRef certificado As CSCertificacion.Certificado)

            If certificado.CodigosSectores Is Nothing OrElse certificado.CodigosSectores.Count = 0 Then
                certificado.CodigosSectores = New List(Of String)
            End If

            If Not certificado.CodigosSectores.Exists(Function(e) e = drCertificados("COD_SECTOR")) AndAlso Not String.IsNullOrEmpty(drCertificados("COD_SECTOR")) Then
                certificado.CodigosSectores.Add(drCertificados("COD_SECTOR"))
            End If

        End Sub

        Private Shared Sub CargarDelegaciones(drCertificados As IDataReader, ByRef certificado As CSCertificacion.Certificado)

            If certificado.CodigosDelegaciones Is Nothing OrElse certificado.CodigosDelegaciones.Count = 0 Then
                certificado.CodigosDelegaciones = New List(Of String)
            End If

            If Not certificado.CodigosDelegaciones.Exists(Function(e) e = drCertificados("COD_DELEGACION")) AndAlso Not String.IsNullOrEmpty(drCertificados("COD_DELEGACION")) Then
                certificado.CodigosDelegaciones.Add(drCertificados("COD_DELEGACION"))
            End If

        End Sub

        Private Shared Sub CargarSubCanales(drCertificados As IDataReader, ByRef certificado As CSCertificacion.Certificado)

            If certificado.CodigosSubCanales Is Nothing OrElse certificado.CodigosSubCanales.Count = 0 Then
                certificado.CodigosSubCanales = New List(Of String)
            End If

            If Not certificado.CodigosSubCanales.Exists(Function(e) e = drCertificados("COD_SUBCANAL")) AndAlso Not String.IsNullOrEmpty(drCertificados("COD_SUBCANAL")) Then
                certificado.CodigosSubCanales.Add(drCertificados("COD_SUBCANAL"))
            End If

        End Sub

#End Region

    End Class

End Namespace