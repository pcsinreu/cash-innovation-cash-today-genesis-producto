Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

Namespace GenesisSaldos.Certificacion

    ''' <summary>
    ''' Classe FormulariosCertificados
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Formularios

#Region "[CONSULTAR]"

        ''' <summary>
        ''' Retorna os certificados
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RetornarFormulariosCertificados(_peticion As Prosegur.Genesis.Comon.Peticion(Of Clases.ConfiguracionReporte), ByRef _respuesta As Prosegur.Genesis.Comon.Respuesta(Of List(Of DataRow))) As List(Of DataRow)
            Dim query As New StringBuilder

            Dim _command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            _command.CommandText = My.Resources.RecuperarConfiguracionReporte

            If _peticion.Parametro.Codigo <> String.Empty Then
                query.AppendLine(" AND FC.COD_CONFIG_REPORTE LIKE []COD_CONFIG_REPORTE ")
                _command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CONFIG_REPORTE", ProsegurDbType.Identificador_Alfanumerico, "%" & _peticion.Parametro.Codigo & "%"))
            End If
            If _peticion.Parametro.Descripcion <> String.Empty Then

                query.AppendLine(" AND FC.DES_CONFIG_REPORTE LIKE []DES_CONFIG_REPORTE")
                _command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_DIRECCION", ProsegurDbType.Identificador_Alfanumerico, "%" & _peticion.Parametro.Descripcion & "%"))

            End If
            If _peticion.Parametro.Direccion <> String.Empty Then

                query.AppendLine(" AND FC.DES_DIRECCION LIKE []DES_DIRECCION")
                _command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_DIRECCION", ProsegurDbType.Identificador_Alfanumerico, "%" & _peticion.Parametro.Direccion & "%"))

            End If

            If _peticion.Parametro.TiposClientes IsNot Nothing AndAlso _peticion.Parametro.TiposClientes.Count > 0 Then
                Dim TiposClientesIdentificador As New List(Of String)
                For Each objTipoCliente In _peticion.Parametro.TiposClientes
                    If Not String.IsNullOrEmpty(objTipoCliente.Identificador) Then
                        TiposClientesIdentificador.Add(objTipoCliente.Identificador)
                    End If
                Next
                If TiposClientesIdentificador IsNot Nothing AndAlso TiposClientesIdentificador.Count > 0 Then
                    _command.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, TiposClientesIdentificador, "OID_TIPO_CLIENTE", _command, "AND", "CTC", , False)
                End If
            End If

            If _peticion.Parametro.TiposReporte IsNot Nothing AndAlso _peticion.Parametro.TiposReporte.Count > 0 Then
                Dim TiposReporteIdentificador As New List(Of String)
                For Each objTiposReporte In _peticion.Parametro.TiposReporte
                    If objTiposReporte <> Enumeradores.TipoReporte.NoDefinido Then
                        TiposReporteIdentificador.Add(Convert.ToInt32(objTiposReporte))
                    End If
                Next
                If TiposReporteIdentificador IsNot Nothing AndAlso TiposReporteIdentificador.Count > 0 Then
                    _command.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, TiposReporteIdentificador, "NEL_TIPO_REPORTE", _command, "AND", "FC", , False)
                End If
            End If

            If _peticion.Parametro.Clientes IsNot Nothing AndAlso _peticion.Parametro.Clientes.Count > 0 Then
                Dim ClientesIdentificador As New List(Of String)
                For Each objCliente In _peticion.Parametro.Clientes
                    If Not String.IsNullOrEmpty(objCliente.Identificador) Then
                        ClientesIdentificador.Add(objCliente.Identificador)
                    End If
                Next
                If ClientesIdentificador IsNot Nothing AndAlso ClientesIdentificador.Count > 0 Then
                    _command.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, ClientesIdentificador, "OID_CLIENTE", _command, "AND", "C", , False)
                End If
            End If

            _command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(_command.CommandText, query.ToString))

            _respuesta.ParametrosPaginacion = New Prosegur.Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion()

            Dim dt As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GENESIS, _command, _peticion.ParametrosPaginacion, _respuesta.ParametrosPaginacion)

            If dt Is Nothing OrElse dt.Rows.Count < 1 Then
                Return Nothing
            End If

            Return dt.AsEnumerable().ToList()

        End Function

        Public Shared Function RecuperarConfiguracionReporte(IdentificadorCliente As String,
                                                             IdentificadorTipoCliente As String,
                                                             TipoReporte As List(Of Enumeradores.TipoReporte)) As List(Of Clases.ConfiguracionReporte)

            Dim objConfiguraciones As List(Of Clases.ConfiguracionReporte) = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(AccesoDatos.Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.ConfiguracionReporteRecuperar
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Objeto_Id, IdentificadorCliente))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TIPO_CLIENTE", ProsegurDbType.Objeto_Id, IdentificadorTipoCliente))

            If TipoReporte IsNot Nothing AndAlso TipoReporte.Count > 0 Then

                Dim objColTiposReporte As New List(Of String)

                For Each tr In TipoReporte
                    objColTiposReporte.Add(Extenciones.RecuperarValor(tr))
                Next
                Dim query As New StringBuilder

                query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, objColTiposReporte, "NEL_TIPO_REPORTE", cmd, "AND", "CF"))

                cmd.CommandText = String.Format(cmd.CommandText, query.ToString)

            Else
                cmd.CommandText = String.Format(cmd.CommandText, String.Empty)
            End If

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

            Return RellenarConfiguracionReportes(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd))
        End Function

        Public Shared Function RellenarConfiguracionReportes(tdConfigReporte As DataTable) As List(Of Clases.ConfiguracionReporte)

            Dim objConfReporte As New List(Of Clases.ConfiguracionReporte)

            If tdConfigReporte IsNot Nothing AndAlso tdConfigReporte.Rows.Count > 0 Then

                Dim IdentificadorConfiguracionReporte As String = String.Empty
                Dim objConfiguracionReporte As Clases.ConfiguracionReporte = Nothing

                For Each objRow In tdConfigReporte.Rows

                    IdentificadorConfiguracionReporte = Util.AtribuirValorObj(objRow("OID_CONFIG_REPORTE"), GetType(String))

                    objConfiguracionReporte = (From cr In objConfReporte Where cr.Identificador = IdentificadorConfiguracionReporte).FirstOrDefault

                    If objConfiguracionReporte Is Nothing Then

                        Dim confReporte As New Clases.ConfiguracionReporte

                        With confReporte
                            .Identificador = Util.AtribuirValorObj(objRow("OID_CONFIG_REPORTE"), GetType(String))
                            .Codigo = Util.AtribuirValorObj(objRow("COD_CONFIG_REPORTE"), GetType(String))
                            .Descripcion = Util.AtribuirValorObj(objRow("DES_CONFIG_REPORTE"), GetType(String))
                            .TiposClientes = New ObservableCollection(Of Clases.TipoCliente)
                            .Direccion = Util.AtribuirValorObj(objRow("DES_DIRECCION"), GetType(String))
                            .UsuarioCreacion = Util.AtribuirValorObj(objRow("DES_USUARIO_CREACION"), GetType(String))
                            .TipoReporte = Extenciones.RecuperarEnum(Of Enumeradores.TipoReporte)(Util.AtribuirValorObj(objRow("NEL_TIPO_REPORTE"), GetType(String)))
                            .MascaraNombre = Util.AtribuirValorObj(objRow("COD_MASCARA_NOMBRE"), GetType(String))
                            If objRow("COD_RENDERIZADOR") IsNot DBNull.Value Then
                                .CodigoRedenrizador = [Enum].Parse(GetType(Enumeradores.TipoRenderizador), Util.AtribuirValorObj(objRow("COD_RENDERIZADOR"), GetType(String)))
                            End If
                            .DescripcionExtension = Util.AtribuirValorObj(objRow("DES_EXTENSION"), GetType(String))
                            .DescripcionSeparador = Util.AtribuirValorObj(objRow("DES_SEPARADOR"), GetType(String))
                            .ParametrosReporte = New ObservableCollection(Of Clases.ParametroReporte)
                        End With

                        objConfReporte.Add(confReporte)

                        objConfiguracionReporte = (From cr In objConfReporte Where cr.Identificador = IdentificadorConfiguracionReporte).FirstOrDefault

                    End If

                    objConfiguracionReporte.ParametrosReporte.Add(New Clases.ParametroReporte With { _
                                                                  .Identificador = Util.AtribuirValorObj(objRow("OID_TIPO_REPORTEXPARAMETRO"), GetType(String)), _
                                                                  .Codigo = Util.AtribuirValorObj(objRow("COD_PARAMETRO"), GetType(String)), _
                                                                  .Descripcion = Util.AtribuirValorObj(objRow("DES_PARAMETRO"), GetType(String))})
                Next

            End If

            Return objConfReporte
        End Function

        Public Shared Function ObtenerConfiguracionesReportes(objIdentificadores As List(Of String)) As DataTable
            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandType = CommandType.Text
                cmd.CommandText = My.Resources.ObtenerConfiguracionReporte.ToString()
                If objIdentificadores IsNot Nothing Then
                    If objIdentificadores.Count = 1 Then
                        cmd.CommandText &= " AND CF.OID_CONFIG_REPORTE = '" & objIdentificadores(0) & "' "
                    ElseIf objIdentificadores.Count > 0 Then
                        cmd.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, objIdentificadores, "OID_CONFIG_REPORTE", cmd, "AND", "CF", , False)
                    End If
                End If
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function

        Public Shared Function ObtenerTipoClientesReporte(Id As String) As DataTable
            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandType = CommandType.Text
                cmd.CommandText = My.Resources.ObtenerTipoClientesReporte.ToString()
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_REPORTE", ProsegurDbType.Identificador_Alfanumerico, Id))
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function

        Public Shared Function ObtenerClientesReporte(Id As String) As DataTable
            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandType = CommandType.Text
                cmd.CommandText = My.Resources.ObtenerClientesReporte.ToString()
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_REPORTE", ProsegurDbType.Identificador_Alfanumerico, Id))
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function

#End Region

#Region "[INSERIR]"

        ''' <summary>
        ''' Insere o certificado
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <param name="objTransacion"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 27/05/2013 - Criado
        ''' </history>
        Public Shared Function InserirFormulariosCertificado(Peticion As Prosegur.Genesis.Comon.Clases.ConfiguracionReporte, ByRef objTransacion As Transacao) As String

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim OidConfigReporte As String = Guid.NewGuid.ToString

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ConfiguracionReporteInserir)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_REPORTE", ProsegurDbType.Objeto_Id, OidConfigReporte))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CONFIG_REPORTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.Codigo))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_CONFIG_REPORTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.Descripcion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_DIRECCION", ProsegurDbType.Identificador_Alfanumerico, Peticion.Direccion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Identificador_Alfanumerico, Peticion.UsuarioCreacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_TIPO_REPORTE", ProsegurDbType.Numero_Decimal, Peticion.TipoReporte))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_MASCARA_NOMBRE", ProsegurDbType.Observacao_Longa, Peticion.MascaraNombre))
            If Peticion.CodigoRedenrizador <> Enumeradores.TipoRenderizador.NoDefinido Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_RENDERIZADOR", ProsegurDbType.Observacao_Longa, Peticion.CodigoRedenrizador.ToString()))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_RENDERIZADOR", ProsegurDbType.Observacao_Longa, Nothing))
            End If
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_EXTENSION", ProsegurDbType.Observacao_Longa, Peticion.DescripcionExtension))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_SEPARADOR", ProsegurDbType.Observacao_Longa, Peticion.DescripcionSeparador))

            objTransacion.AdicionarItemTransacao(cmd)

            Return OidConfigReporte
        End Function

        Public Shared Sub InserirTipoClientesCertificados(Peticion As Prosegur.Genesis.Comon.Clases.ConfiguracionReporte, ByRef objTransacion As Transacao)

            For Each TipoClienteSelecionado As Clases.TipoCliente In Peticion.TiposClientes
                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                Dim OidConfigXTipoCli As String = Guid.NewGuid.ToString

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ConfigReporteTipoClienteInserir)
                cmd.CommandType = CommandType.Text

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIGRPXTIPOCLI", ProsegurDbType.Objeto_Id, OidConfigXTipoCli))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_REPORTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.Identificador))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TIPO_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, TipoClienteSelecionado.Identificador))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Identificador_Alfanumerico, Peticion.UsuarioCreacion))

                objTransacion.AdicionarItemTransacao(cmd)

            Next

        End Sub

        Public Shared Sub InserirClientesCertificados(Peticion As Prosegur.Genesis.Comon.Clases.ConfiguracionReporte, ByRef objTransacion As Transacao)

            For Each ClienteSelecionado As Clases.Cliente In Peticion.Clientes
                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                Dim OidConfigXCliente As String = Guid.NewGuid.ToString

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ConfigReporteClienteInserir)
                cmd.CommandType = CommandType.Text

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIGRPXCLIENTE", ProsegurDbType.Objeto_Id, OidConfigXCliente))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_REPORTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.Identificador))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, ClienteSelecionado.Identificador))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Identificador_Alfanumerico, Peticion.UsuarioCreacion))

                objTransacion.AdicionarItemTransacao(cmd)
            Next
        End Sub

        Public Shared Function AlterarFormulariosCertificado(Peticion As Prosegur.Genesis.Comon.Clases.ConfiguracionReporte, ByRef objTransacion As Transacao) As String

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim OidConfigReporte As String = Peticion.Identificador

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ConfigReportAlterar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_REPORTE", ProsegurDbType.Objeto_Id, OidConfigReporte))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CONFIG_REPORTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.Codigo))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_CONFIG_REPORTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.Descripcion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_DIRECCION", ProsegurDbType.Identificador_Alfanumerico, Peticion.Direccion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Identificador_Alfanumerico, Peticion.UsuarioCreacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_TIPO_REPORTE", ProsegurDbType.Numero_Decimal, Peticion.TipoReporte))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_MASCARA_NOMBRE", ProsegurDbType.Observacao_Longa, Peticion.MascaraNombre))
            If Peticion.CodigoRedenrizador <> Enumeradores.TipoRenderizador.NoDefinido Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_RENDERIZADOR", ProsegurDbType.Observacao_Longa, Peticion.CodigoRedenrizador.ToString()))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_RENDERIZADOR", ProsegurDbType.Observacao_Longa, Nothing))
            End If
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_EXTENSION", ProsegurDbType.Observacao_Longa, Peticion.DescripcionExtension))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_SEPARADOR", ProsegurDbType.Observacao_Longa, Peticion.DescripcionSeparador))

            objTransacion.AdicionarItemTransacao(cmd)

            Return OidConfigReporte
        End Function

        Public Shared Sub ExcluirFormulariosCertificado(OidConfigReporte As String, ByRef objTransacion As Transacao)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ConfigReporteExcluir)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_REPORTE", ProsegurDbType.Objeto_Id, OidConfigReporte))

            objTransacion.AdicionarItemTransacao(cmd)


        End Sub

        Public Shared Sub ExcluirTiposClientesFormulariosCertificado(oidConfigReporte As String, ByRef objTransacion As Transacao)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ConfigReporteTipoClienteExcluir)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_REPORTE", ProsegurDbType.Objeto_Id, oidConfigReporte))

            objTransacion.AdicionarItemTransacao(cmd)

        End Sub

        Public Shared Sub ExcluirClientesFormulariosCertificado(OidConfigReporte As String, ByRef objTransacion As Transacao)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ConfigReporteClienteExcluir)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_REPORTE", ProsegurDbType.Objeto_Id, OidConfigReporte))

            objTransacion.AdicionarItemTransacao(cmd)

        End Sub

        Public Shared Sub ExcluirParametrosReporte(OidConfigReporte As String, ByRef objTransacion As Transacao)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ConfigReporteExcluirParametros)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_REPORTE", ProsegurDbType.Objeto_Id, OidConfigReporte))

            objTransacion.AdicionarItemTransacao(cmd)

        End Sub

        Public Shared Sub ExcluirResultadoReporte(OidConfigReporte As String, ByRef objTransacion As Transacao)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ConfigReporteExcluirResultado)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_REPORTE", ProsegurDbType.Objeto_Id, OidConfigReporte))

            objTransacion.AdicionarItemTransacao(cmd)

        End Sub

        Public Shared Sub ExcluirParametrosResultado(OidConfigReporte As String, ByRef objTransacion As Transacao)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ConfigReporteExcluirParametrosResultado)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_REPORTE", ProsegurDbType.Objeto_Id, OidConfigReporte))

            objTransacion.AdicionarItemTransacao(cmd)

        End Sub

#End Region

    End Class

End Namespace