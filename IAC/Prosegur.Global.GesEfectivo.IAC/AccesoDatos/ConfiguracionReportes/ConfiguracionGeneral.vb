Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class ConfiguracionGeneral

    ''' <summary>
    ''' configuracines generales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 04/07/2013 Criado
    ''' </history>
    Public Shared Function GetConfiguracionGeneralReportes() As ContractoServicio.Configuracion.General.Respuesta
        ' Declara variável de retorno
        Dim objRetorno As New ContractoServicio.Configuracion.General.Respuesta

        Dim conexao As IDbConnection = AcessoDados.Conectar(Constantes.CONEXAO_GE)

        ' criar comando
        Dim comando As IDbCommand = conexao.CreateCommand()

        'Cria DataReader
        Dim dr As IDataReader = Nothing

        Try
            Dim listaConfig As New List(Of ContractoServicio.Configuracion.General.ConfiguracionGeneral)

            ' Limpa o objeto da memória quando termina de usá-lo
            Using comando

                ' obter procedure
                comando.CommandText = My.Resources.RecuperarConfiguracionesGeneral
                comando.CommandType = CommandType.Text

                ' executar consulta
                dr = comando.ExecuteReader()
                While (dr.Read)
                    Dim config As New ContractoServicio.Configuracion.General.ConfiguracionGeneral
                    ' adicionar para objeto
                    Util.AtribuirValorObjeto(config.OIDConfiguracionGeneral, dr("OID_CONFIGURACION_GENERAL"), GetType(String))
                    Util.AtribuirValorObjeto(config.DesReporte, dr("DES_REPORTE"), GetType(String))
                    Util.AtribuirValorObjeto(config.CodReporte, dr("COD_REPORTE"), GetType(String))
                    Util.AtribuirValorObjeto(config.FormatoArchivo, dr("NEC_FORMATO_ARCHIVO"), GetType(String))
                    Util.AtribuirValorObjeto(config.ExtensionArchivo, dr("DES_EXTENSION_ARCHIVO"), GetType(String))
                    Util.AtribuirValorObjeto(config.Separador, dr("COD_SEPARADOR"), GetType(String))
                    Util.AtribuirValorObjeto(config.CodUsuario, dr("COD_USUARIO"), GetType(String))
                    Util.AtribuirValorObjeto(config.FechaAtualizacion, dr("FYH_ACTUALIZACION"), GetType(DateTime))

                    listaConfig.Add(config)
                End While

                'Percorre o dr e retorna uma coleção de respaldos completos.
                objRetorno.ConfiguracionGeneralColeccion = listaConfig

            End Using
        Finally

            ' Fecha a conexão do Data Reader
            If dr IsNot Nothing Then
                dr.Close()
                dr.Dispose()
            End If

            ' Fecha a conexão do banco
            AcessoDados.Desconectar(conexao)
        End Try

        Return objRetorno
    End Function

    ''' <summary>
    ''' Inserir configuracines generales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 08/07/2013 Criado
    ''' </history>
    Public Shared Function InserirConfiguracionGeneralReporte(peticion As ContractoServicio.Configuracion.General.Peticion) As ContractoServicio.Configuracion.General.Respuesta
        ' Declara variável de retorno
        Dim objRetorno As New ContractoServicio.Configuracion.General.Respuesta

        Dim conexao As IDbConnection = AcessoDados.Conectar(Constantes.CONEXAO_GE)

        ' criar comando
        Dim comando As IDbCommand = conexao.CreateCommand()

        Try
            ' Limpa o objeto da memória quando termina de usá-lo
            Using comando
                peticion.ConfiguracionGeneral.OIDConfiguracionGeneral = Guid.NewGuid.ToString

                comando.CommandText = Util.PrepararQuery(My.Resources.InserirConfiguracionesGeneral)
                comando.CommandType = CommandType.Text

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CONFIGURACION_GENERAL", ProsegurDbType.Objeto_Id, peticion.ConfiguracionGeneral.OIDConfiguracionGeneral))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_REPORTE", ProsegurDbType.Descricao_Longa, peticion.ConfiguracionGeneral.DesReporte.ToUpper))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_REPORTE", ProsegurDbType.Descricao_Curta, peticion.ConfiguracionGeneral.CodReporte))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_FORMATO_ARCHIVO", ProsegurDbType.Identificador_Numerico, peticion.ConfiguracionGeneral.FormatoArchivo))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_EXTENSION_ARCHIVO", ProsegurDbType.Descricao_Longa, peticion.ConfiguracionGeneral.ExtensionArchivo))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SEPARADOR", ProsegurDbType.Descricao_Curta, peticion.ConfiguracionGeneral.Separador))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Descricao_Curta, peticion.ConfiguracionGeneral.CodUsuario))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))

                comando.ExecuteNonQuery()

            End Using
        Finally

            ' Fecha a conexão do banco
            AcessoDados.Desconectar(conexao)
        End Try

        Return objRetorno
    End Function

    ''' <summary>
    ''' Exclui um ou mais registros de configurações gerais.
    ''' </summary>
    ''' <param name="peticion">Configurações a serem excluidas.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExcluirConfiguracionGeneralReporte(peticion As ContractoServicio.Configuracion.General.Peticion) As ContractoServicio.Configuracion.General.Respuesta
        ' Declara variável de retorno
        Dim objRetorno As New ContractoServicio.Configuracion.General.Respuesta

        Dim conexao As IDbConnection = AcessoDados.Conectar(Constantes.CONEXAO_GE)

        ' criar comando
        Dim comando As IDbCommand = conexao.CreateCommand()
        'Cria DataReader
        Dim dr As IDataReader = Nothing

        Try
            ' Limpa o objeto da memória quando termina de usá-lo
            Using comando
                'Recupera as configurações gerais que estão sendo utilizadas em alguma configuração
                comando.CommandText = My.Resources.RecuperarConfiguracionesGeneralEmUso
                comando.CommandType = CommandType.Text
                comando.CommandText &= Util.MontarClausulaIn(peticion.ListaOIDConfiguracionGeneral, "OID_CONFIGURACION_GENERAL", comando, "WHERE", "T")
                comando.CommandText = Util.PrepararQuery(comando.CommandText)
                dr = comando.ExecuteReader()
            End Using

            While (dr.Read)
                'remove a configuração geral da lista a ser exlcuída
                peticion.ListaOIDConfiguracionGeneral.Remove(dr("OID_CONFIGURACION_GENERAL"))

                objRetorno.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT

                Dim formatoArq As String = String.Empty

                Select Case dr("NEC_FORMATO_ARCHIVO")
                    Case 1
                        formatoArq = "CSV"
                    Case 2
                        formatoArq = "PDF"
                    Case 3
                        formatoArq = "EXCEL"
                    Case 4
                        formatoArq = "MHTML"
                End Select
                objRetorno.MensajeError = objRetorno.MensajeError & String.Format(Traduzir("062_msg_configuracionGeneral_em_uso"), dr("DES_REPORTE") & "." & formatoArq)
                objRetorno.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
            End While

            'remove as configurações gerais que não pertence a nenhuma configuração
            'as que pertencem a alguma configuração foi removida acima
            If peticion.ListaOIDConfiguracionGeneral.Count > 0 Then
                ' Limpa o objeto da memória quando termina de usá-lo
                Using comando
                    comando.CommandText = My.Resources.ExcluirConfiguracionesGeneral
                    comando.CommandType = CommandType.Text
                    comando.CommandText &= Util.MontarClausulaIn(peticion.ListaOIDConfiguracionGeneral, "OID_CONFIGURACION_GENERAL", comando)
                    comando.CommandText = Util.PrepararQuery(comando.CommandText)
                    comando.ExecuteNonQuery()
                End Using
            End If
        
        Finally

            ' Fecha a conexão do Data Reader
            If dr IsNot Nothing Then
                dr.Close()
                dr.Dispose()
            End If
            ' Fecha a conexão do banco
            AcessoDados.Desconectar(conexao)

        End Try

        Return objRetorno
    End Function

    ''' <summary>
    ''' Atualizar configuracines generales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 09/07/2013 Criado
    ''' </history>
    Public Shared Function AtualizarConfiguracionGeneralReporte(peticion As ContractoServicio.Configuracion.General.Peticion) As ContractoServicio.Configuracion.General.Respuesta
        ' Declara variável de retorno
        Dim objRetorno As New ContractoServicio.Configuracion.General.Respuesta

        Dim conexao As IDbConnection = AcessoDados.Conectar(Constantes.CONEXAO_GE)

        ' criar comando
        Dim comando As IDbCommand = conexao.CreateCommand()

        Try
            ' Limpa o objeto da memória quando termina de usá-lo
            Using comando
                comando.CommandText = Util.PrepararQuery(My.Resources.AtualizarConfiguracionGeneral)
                comando.CommandType = CommandType.Text

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CONFIGURACION_GENERAL", ProsegurDbType.Objeto_Id, peticion.ConfiguracionGeneral.OIDConfiguracionGeneral))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_REPORTE", ProsegurDbType.Descricao_Curta, peticion.ConfiguracionGeneral.CodReporte))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_FORMATO_ARCHIVO", ProsegurDbType.Identificador_Numerico, peticion.ConfiguracionGeneral.FormatoArchivo))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_EXTENSION_ARCHIVO", ProsegurDbType.Descricao_Longa, peticion.ConfiguracionGeneral.ExtensionArchivo))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SEPARADOR", ProsegurDbType.Descricao_Curta, peticion.ConfiguracionGeneral.Separador))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Descricao_Curta, peticion.ConfiguracionGeneral.CodUsuario))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))

                comando.ExecuteNonQuery()

            End Using
        Finally

            ' Fecha a conexão do banco
            AcessoDados.Desconectar(conexao)
        End Try

        Return objRetorno
    End Function

    ''' <summary>
    ''' Recupera uma configuração
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 09/07/2013 Criado
    ''' </history>
    Public Shared Function GetConfiguracionGeneralReporte(peticion As ContractoServicio.Configuracion.General.Peticion) As ContractoServicio.Configuracion.General.Respuesta

        ' Declara variável de retorno
        Dim objRetorno As New ContractoServicio.Configuracion.General.Respuesta
        objRetorno.ConfiguracionGeneral = New ContractoServicio.Configuracion.General.ConfiguracionGeneral

        Dim conexao As IDbConnection = AcessoDados.Conectar(Constantes.CONEXAO_GE)

        ' criar comando
        Dim comando As IDbCommand = conexao.CreateCommand()

        'Cria DataReader
        Dim dr As IDataReader = Nothing

        Try
            ' Limpa o objeto da memória quando termina de usá-lo
            Using comando

                ' obter procedure
                comando.CommandText = Util.PrepararQuery(My.Resources.RecuperarConfiguracionGeneral)
                comando.CommandType = CommandType.Text

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CONFIGURACION_GENERAL", ProsegurDbType.Objeto_Id, peticion.ConfiguracionGeneral.OIDConfiguracionGeneral))

                ' executar consulta
                dr = comando.ExecuteReader()
                If (dr.Read) Then
                    ' adicionar para objeto
                    Util.AtribuirValorObjeto(objRetorno.ConfiguracionGeneral.OIDConfiguracionGeneral, dr("OID_CONFIGURACION_GENERAL"), GetType(String))
                    Util.AtribuirValorObjeto(objRetorno.ConfiguracionGeneral.DesReporte, dr("DES_REPORTE"), GetType(String))
                    Util.AtribuirValorObjeto(objRetorno.ConfiguracionGeneral.CodReporte, dr("COD_REPORTE"), GetType(String))
                    Util.AtribuirValorObjeto(objRetorno.ConfiguracionGeneral.FormatoArchivo, dr("NEC_FORMATO_ARCHIVO"), GetType(String))
                    Util.AtribuirValorObjeto(objRetorno.ConfiguracionGeneral.ExtensionArchivo, dr("DES_EXTENSION_ARCHIVO"), GetType(String))
                    Util.AtribuirValorObjeto(objRetorno.ConfiguracionGeneral.Separador, dr("COD_SEPARADOR"), GetType(String))
                    Util.AtribuirValorObjeto(objRetorno.ConfiguracionGeneral.CodUsuario, dr("COD_USUARIO"), GetType(String))
                    Util.AtribuirValorObjeto(objRetorno.ConfiguracionGeneral.FechaAtualizacion, dr("FYH_ACTUALIZACION"), GetType(DateTime))
                End If

            End Using
        Finally

            ' Fecha a conexão do Data Reader
            If dr IsNot Nothing Then
                dr.Close()
                dr.Dispose()
            End If

            ' Fecha a conexão do banco
            AcessoDados.Desconectar(conexao)
        End Try

        Return objRetorno
    End Function

End Class
