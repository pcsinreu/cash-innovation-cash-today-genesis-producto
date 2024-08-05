Imports Prosegur.DbHelper

Public Class RespaldoCompleto

    Private Shared _descricaoIACS As New Dictionary(Of String, String)

#Region "[LISTAR]"

    ''' <summary>
    ''' Método responsável por listar los respaldos completos para o arquivo CSV
    ''' </summary>
    ''' <param name="objPeticion">Objeto com os filtros da pesquisa</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 28/07/2009 Criado
    ''' [magnum.oliveira] 27/10/2009 Alterado
    ''' </history>
    Public Shared Function ListarRespaldoCompletoCSV(objPeticion As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Peticion) As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.RespaldoCompletoCSVColeccion

        ' Declara variável de retorno
        Dim objRetornaRespaldoCompleto As New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.RespaldoCompletoCSVColeccion

        Dim conexao As IDbConnection = AcessoDados.Conectar(Constantes.CONEXAO_GE)

        ' criar comando
        Dim comando As IDbCommand = conexao.CreateCommand()

        ' executar consulta
        Dim drRespaldoCompleto As IDataReader = Nothing

        Try

            ' Limpa o objeto da memória quando termina de usá-lo
            Using comando

                ' obter procedure
                comando.CommandText = Constantes.SP_RESPALDO_COMPLETO_CSV
                comando.CommandType = CommandType.StoredProcedure

                ' setar parametros
                comando.Parameters.Add(Util.CriarParametroOracle("CV_IACS", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("CV_RESPALDOC_DETALLE", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("CV_BILLETESFALSOS", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoCliente))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoDelegacion))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_BOOK_PROCESO", ProsegurDbType.Observacao_Curta, IIf(objPeticion.Procesos IsNot Nothing AndAlso objPeticion.Procesos.Count > 0, Util.RetornaStringListaValores(objPeticion.Procesos), DBNull.Value)))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FEC_INI", ProsegurDbType.Data_Hora, objPeticion.FechaDesde))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FEC_FIN", ProsegurDbType.Data_Hora, objPeticion.FechaHasta))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "ES_FEC_PROCESO", ProsegurDbType.Inteiro_Curto, objPeticion.EsFechaProceso))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "ESTADO_REMESA", ProsegurDbType.Identificador_Alfanumerico, objPeticion.EstadoRemesa))

                ' executar consulta
                drRespaldoCompleto = comando.ExecuteReader()

                'Percorre o dr e retorna uma coleção de respaldos completos.
                objRetornaRespaldoCompleto = RetornaColecaoRespaldoCompletoCSV(drRespaldoCompleto)

            End Using

        Finally

            ' Define a procedure que apaga a tabela temporária de IAC
            comando.CommandText = Constantes.SP_DROP_IAC_TABLE
            comando.CommandType = CommandType.StoredProcedure

            ' Limpa os parâmetros utilizados anteriormente
            comando.Parameters.Clear()

            ' Executa a procedure que apaga a tebela temporária de IAC
            comando.ExecuteNonQuery()

            ' Fecha a conexão do Data Reader
            If drRespaldoCompleto IsNot Nothing Then
                drRespaldoCompleto.Close()
                drRespaldoCompleto.Dispose()
            End If

            ' Fecha a conexão do banco
            AcessoDados.Desconectar(conexao)

        End Try

        Return objRetornaRespaldoCompleto
    End Function

    ''' <summary>
    ''' Percorre o dr e retorna uma coleção de respaldos completos
    ''' </summary>
    ''' <param name="drRespaldoCompleto">Objeto com os dados do respaldo completo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 28/07/2009 Criado
    ''' </history>
    Private Shared Function RetornaColecaoRespaldoCompletoCSV(drRespaldoCompleto As IDataReader) As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.RespaldoCompletoCSVColeccion

        Dim objRetornaRespaldoCompleto As New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.RespaldoCompletoCSVColeccion

        _descricaoIACS.Clear()

        While (drRespaldoCompleto.Read)

            'Verifica se a chave já existe para não adiciona-la duas vezes
            If Not _descricaoIACS.ContainsKey(drRespaldoCompleto(0).ToString()) Then
                _descricaoIACS.Add(drRespaldoCompleto(0).ToString(), drRespaldoCompleto(1).ToString())
            End If

        End While

        ' Vai para o próximo cursor
        drRespaldoCompleto.NextResult()

        While (drRespaldoCompleto.Read)

            ' adicionar para objeto
            objRetornaRespaldoCompleto.Add(PopularRespaldoCompletoCSV(drRespaldoCompleto))

        End While

        ' propaga o valor de IAC para todos os Parciais que contém somente valores de declarados (Não foram contados)
        DuplicaValorTerminoParaMesmoParcial(objRetornaRespaldoCompleto)

        ' Vai para o próximo cursor
        drRespaldoCompleto.NextResult()

        While (drRespaldoCompleto.Read)

            Dim RespaldoCompleto = (From rc In objRetornaRespaldoCompleto _
                                Where rc.Parcial = drRespaldoCompleto("PARCIAL") _
                                AndAlso rc.MedioPago = "Efectivo" _
                                AndAlso rc.DescricionDivisa = drRespaldoCompleto("DIVISA")).FirstOrDefault()

            If RespaldoCompleto IsNot Nothing Then

                If RespaldoCompleto.Falsos Is Nothing Then
                    RespaldoCompleto.Falsos = New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.FalsoColeccion()
                End If

                RespaldoCompleto.Falsos.Add(PopularFalso(drRespaldoCompleto))

            End If

        End While

        Return objRetornaRespaldoCompleto

    End Function

    ''' <summary>
    ''' Popula o datarow
    ''' </summary>
    ''' <param name="drRespaldoCompleto">Registro Atual com os dados do respaldo completo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 28/07/2009 Criado
    ''' </history>
    Private Shared Function PopularRespaldoCompletoCSV(drRespaldoCompleto As IDataReader) As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.RespaldoCompletoCSV

        Dim objRespaldoCompleto As New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.RespaldoCompletoCSV

        Util.AtribuirValorObjeto(objRespaldoCompleto.Recuento, drRespaldoCompleto("RECUENTO"), GetType(String))
        Util.AtribuirValorObjeto(objRespaldoCompleto.Fecha, drRespaldoCompleto("FECHA"), GetType(DateTime))
        Util.AtribuirValorObjeto(objRespaldoCompleto.Letra, drRespaldoCompleto("LETRA"), GetType(String))
        Util.AtribuirValorObjeto(objRespaldoCompleto.F22, drRespaldoCompleto("F22"), GetType(String))
        Util.AtribuirValorObjeto(objRespaldoCompleto.OidRemesaOri, drRespaldoCompleto("OID_REMESA_ORI"), GetType(String))
        Util.AtribuirValorObjeto(objRespaldoCompleto.CodSubCliente, drRespaldoCompleto("COD_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objRespaldoCompleto.Sucursal, drRespaldoCompleto("SUCURSAL"), GetType(String))
        Util.AtribuirValorObjeto(objRespaldoCompleto.DescricionSucursal, drRespaldoCompleto("DES_SUCURSAL"), GetType(String))
        objRespaldoCompleto.InformacionesIAC = RecuperarColecaoInformacaoIAC(drRespaldoCompleto)
        Util.AtribuirValorObjeto(objRespaldoCompleto.MedioPago, drRespaldoCompleto("MEDIO_PAGO"), GetType(String))
        Util.AtribuirValorObjeto(objRespaldoCompleto.DescricionMedioPago, drRespaldoCompleto("DES_MEDIO_PAGO"), GetType(String))
        Util.AtribuirValorObjeto(objRespaldoCompleto.Divisa, drRespaldoCompleto("DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objRespaldoCompleto.DescricionDivisa, drRespaldoCompleto("DES_DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objRespaldoCompleto.IngresadoSobre, drRespaldoCompleto("INGRESADO_POR_SOBRE"), GetType(Decimal))
        Util.AtribuirValorObjeto(objRespaldoCompleto.Contado, drRespaldoCompleto("CONTADO"), GetType(Decimal))
        Util.AtribuirValorObjeto(objRespaldoCompleto.Observaciones, drRespaldoCompleto("OBSERVACIONES"), GetType(String))
        Util.AtribuirValorObjeto(objRespaldoCompleto.Parcial, drRespaldoCompleto("PARCIAL"), GetType(String))

        Return objRespaldoCompleto

    End Function

    ''' <summary>
    ''' Retorna a lista de informações do IAC
    ''' </summary>
    ''' <param name="drRespaldoCompleto">Objeto com os dados do Respaldo completo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 06/08/2009 Criado
    ''' </history>
    Private Shared Function RecuperarColecaoInformacaoIAC(ByRef drRespaldoCompleto As IDataReader) As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIACColeccion

        ' Lista com as informações do IAC
        Dim lstInformacoesIAC As New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIACColeccion

        ' Posição do primeiro campo que possui as informações do IAC
        Dim posicaoInicial As Integer = drRespaldoCompleto.GetOrdinal("DES_SUCURSAL") + 1

        ' Posição do ultimo campo que possui as informações do IAC
        Dim posicaoFinal As Integer = drRespaldoCompleto.GetOrdinal("MEDIO_PAGO") - 1

        ' Variável que recebe as informações do IAC
        Dim informacaoIAC As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIAC

        ' Para cada informação do IAC
        For indice As Integer = posicaoInicial To posicaoFinal

            ' Cria uma nova instacia para receber as informações do IAC
            informacaoIAC = New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIAC

            ' Recupera o nome do campo
            'informacaoIAC.Descricao = drRespaldoCompleto.GetName(indice)
            informacaoIAC.Descricao = _descricaoIACS.Item(drRespaldoCompleto.GetName(indice))
            ' Recupera o valor do campo
            Util.AtribuirValorObjeto(informacaoIAC.Valor, drRespaldoCompleto.GetValue(indice), Nothing)

            ' Adiciona a informação do IAC na lista
            lstInformacoesIAC.Add(informacaoIAC)
        Next

        ' Retorna as informações do IAC
        Return lstInformacoesIAC

    End Function

    ''' <summary>
    ''' Método responsável por listar los respaldos completos para o arquivo PDF
    ''' </summary>
    ''' <param name="objPeticion">Objeto com os filtros da pesquisa</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 28/07/2009 Criado
    ''' [magnum.oliveira] 27/10/2009 Alterado
    ''' </history>
    Public Shared Function ListarRespaldoCompletoPDF(objPeticion As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Peticion) As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.RespaldoCompletoPDF

        ' Declara variável de retorno
        Dim objRetornaRespaldoCompleto As New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.RespaldoCompletoPDF

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' Limpa o objeto da memória quando termina de usá-lo
        Using comando

            ' obter query
            comando.CommandText = Constantes.SP_RESPALDO_COMPLETO_PDF
            comando.CommandType = CommandType.StoredProcedure

            ' setar parametros
            comando.Parameters.Add(Util.CriarParametroOracle("CV_RESPALDOP_DETALLE", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
            comando.Parameters.Add(Util.CriarParametroOracle("CV_RESPALDOP_SOBRES", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
            comando.Parameters.Add(Util.CriarParametroOracle("CV_SUMAVALORPORDIVISADECL", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
            comando.Parameters.Add(Util.CriarParametroOracle("CV_INFIAC", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
            comando.Parameters.Add(Util.CriarParametroOracle("CV_TOTALPARCIALESDECL", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
            comando.Parameters.Add(Util.CriarParametroOracle("CV_SUMAVALORPORDIVISAING", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
            comando.Parameters.Add(Util.CriarParametroOracle("CV_OBSERVACIONES", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoDelegacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_BOOK_PROCESO", ProsegurDbType.Observacao_Curta, IIf(objPeticion.Procesos IsNot Nothing AndAlso objPeticion.Procesos.Count > 0, Util.RetornaStringListaValores(objPeticion.Procesos), DBNull.Value)))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FEC_INI", ProsegurDbType.Data_Hora, objPeticion.FechaDesde))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FEC_FIN", ProsegurDbType.Data_Hora, objPeticion.FechaHasta))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "ES_FEC_PROCESO", ProsegurDbType.Inteiro_Curto, objPeticion.EsFechaProceso))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "ESTADO_REMESA", ProsegurDbType.Identificador_Alfanumerico, objPeticion.EstadoRemesa))

            ' executar consulta
            Dim drRespaldoCompleto As IDataReader = AcessoDados.ExecutarDataReader(Constantes.CONEXAO_GE, comando)

            ' Limpa o objeto da memória quando termina de usá-lo
            Using drRespaldoCompleto
                Try
                    ' Percorre o dr e retorna uma coleção de detalhes de respaldos completos.
                    objRetornaRespaldoCompleto.Detalles = RetornaColecaoDetalle(drRespaldoCompleto)

                    ' Vai para o próximo cursor
                    drRespaldoCompleto.NextResult()

                    ' Percorre o dr e retorna uma coleção de sobres de respaldos completos.
                    objRetornaRespaldoCompleto.Sobres = RetornaColecaoSobre(drRespaldoCompleto)

                    ' Vai para o próximo cursor
                    drRespaldoCompleto.NextResult()

                    ' Percorre o dr e retorna uma coleção de divisas declarados.
                    objRetornaRespaldoCompleto.Divisas = RetornaColecaoDivisa(drRespaldoCompleto)

                    ' Vai para o próximo Cursor
                    drRespaldoCompleto.NextResult()

                    ' Percorre o dr e retorna uma coleção informações do IAC de respaldos completos.
                    objRetornaRespaldoCompleto.InformacionesIAC = RetornaColecaoInformacaoIAC(drRespaldoCompleto)

                    ' Vai para o próximo cursor
                    drRespaldoCompleto.NextResult()

                    ' Percorre o dr e retorna o total de parciais declarados
                    objRetornaRespaldoCompleto.TotalParcialesDeclarados = RetornaTotalParciaisDeclarados(drRespaldoCompleto)

                    ' Vai para o próximo cursor
                    drRespaldoCompleto.NextResult()

                    ' Percorre o dr e retorna uma coleção de divisas ingressados.
                    PopulaDivisasIngressado(objRetornaRespaldoCompleto.Divisas, drRespaldoCompleto)

                    ' Vai para o próximo cursor
                    drRespaldoCompleto.NextResult()

                    ' Percorre o dr e retorna as observações
                    objRetornaRespaldoCompleto.Observaciones = RetornaColecaoObservaciones(drRespaldoCompleto)

                Finally
                    ' Fecha a conexão do Data Reader
                    If drRespaldoCompleto IsNot Nothing Then
                        drRespaldoCompleto.Close()
                        drRespaldoCompleto.Dispose()
                    End If
                    ' Fecha a conexão do banco
                    AcessoDados.Desconectar(comando.Connection)
                End Try
            End Using
        End Using

        Return objRetornaRespaldoCompleto

    End Function

    ''' <summary>
    ''' Percorre o dr e retorna uma coleção de divisas ingressados.
    ''' </summary>
    ''' <param name="Divisas"></param>
    ''' <param name="drDivisaIng"></param>
    ''' <history>
    ''' [jorge.viana] 04/01/2010 Criado
    ''' </history>
    ''' <remarks></remarks>
    Private Shared Sub PopulaDivisasIngressado(ByRef Divisas As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.DivisaColeccion, _
                                               drDivisaIng As IDataReader)

        While (drDivisaIng.Read)

            Dim fDivisas = From d In Divisas _
                           Where d.Parcial = drDivisaIng("OID_PARCIAL") AndAlso _
                           d.DescripcionMedioPago = drDivisaIng("MEDIO_PAGO") AndAlso _
                           d.Divisa = drDivisaIng("DIVISA")

            If fDivisas.Count > 0 Then
                Util.AtribuirValorObjeto(fDivisas.First.ImporteIngressado, drDivisaIng("IMPORTE"), GetType(Decimal))
            Else
                ' adicionar para objeto
                Divisas.Add(PopularDivisa(drDivisaIng, False))
            End If

        End While

    End Sub

    ''' <summary>
    ''' Percorre o dr e retorna uma coleção de detalhes de respaldos completos
    ''' </summary>
    ''' <param name="drDetalhe">Objeto com os dados dos detalhes do respaldo completo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 28/07/2009 Criado
    ''' </history>
    Private Shared Function RetornaColecaoDetalle(drDetalhe As IDataReader) As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.DetalleColeccion

        Dim objRetornaDetalle As New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.DetalleColeccion

        While (drDetalhe.Read)

            ' adicionar para objeto
            objRetornaDetalle.Add(PopularDetalle(drDetalhe))

        End While

        Return objRetornaDetalle

    End Function

    ''' <summary>
    ''' Popula o datarow
    ''' </summary>
    ''' <param name="drDetalhe">Registro Atual com os dados dos detalhes do respaldo completo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 28/07/2009 Criado
    ''' </history>
    Private Shared Function PopularDetalle(drDetalhe As IDataReader) As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Detalle

        Dim objDetalhe As New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Detalle

        Util.AtribuirValorObjeto(objDetalhe.Letra, drDetalhe("LETRA"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.Parcial, drDetalhe("PARCIAL"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.F22, drDetalhe("F22"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.OidRemesaOri, drDetalhe("OID_REMESA_ORI"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.CodSubCliente, drDetalhe("COD_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.Sucursal, drDetalhe("SUCURSAL"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.DescricionSucursal, drDetalhe("DES_SUCURSAL"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.Divisa, drDetalhe("DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.DescricionDivisa, drDetalhe("DES_DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.UnidadMoeda, drDetalhe("UNIDAD_MOEDA"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.Denominacion, drDetalhe("DENOMINACION"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.BolBillete, drDetalhe("BOL_BILLETE"), GetType(Boolean))
        Util.AtribuirValorObjeto(objDetalhe.Unidades, drDetalhe("UNIDADES"), GetType(Decimal))
        Util.AtribuirValorObjeto(objDetalhe.Recontado, drDetalhe("RECONTADO"), GetType(Decimal))
        Util.AtribuirValorObjeto(objDetalhe.NumeroSecuencia, drDetalhe("NRO_SOBRE"), GetType(Integer))

        Return objDetalhe

    End Function

    ''' <summary>
    ''' Percorre o dr e retorna uma coleção de sobres de respaldos completos
    ''' </summary>
    ''' <param name="drSobre">Objeto com os dados dos sobres do respaldo completo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 28/07/2009 Criado
    ''' </history>
    Private Shared Function RetornaColecaoSobre(drSobre As IDataReader) As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.SobreColeccion

        Dim objRetornaRespaldoCompleto As New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.SobreColeccion

        While (drSobre.Read)

            ' adicionar para objeto
            objRetornaRespaldoCompleto.Add(PopularSobre(drSobre))

        End While

        Return objRetornaRespaldoCompleto

    End Function

    ''' <summary>
    ''' Popula o datarow
    ''' </summary>
    ''' <param name="drSobre">Registro Atual com os dados dos sobres do respaldo completo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 28/07/2009 Criado
    ''' </history>
    Private Shared Function PopularSobre(drSobre As IDataReader) As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Sobre

        Dim objSobre As New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Sobre

        Util.AtribuirValorObjeto(objSobre.ParcialesContados, drSobre("PARCIALES_CONTADOS"), GetType(Decimal))

        If drSobre("PARCIALES_DECLARADOS") IsNot DBNull.Value AndAlso drSobre("PARCIALES_DECLARADOS") IsNot Nothing Then
            Util.AtribuirValorObjeto(objSobre.ParcialesIngresados, drSobre("PARCIALES_DECLARADOS"), GetType(Decimal))
        End If

        Util.AtribuirValorObjeto(objSobre.Parcial, drSobre("PARCIAL"), GetType(String))
        Util.AtribuirValorObjeto(objSobre.Sucursal, drSobre("COD_PUNTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(objSobre.DescricionSucursal, drSobre("DES_PUNTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(objSobre.F22, drSobre("COD_TRANSPORTE"), GetType(String))

        Return objSobre

    End Function

    ''' <summary>
    ''' Percorre o dr e retorna uma coleção de divisas de respaldos completos
    ''' </summary>
    ''' <param name="drDivisa">Objeto com os dados dos divisas do respaldo completo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 28/07/2009 Criado
    ''' </history>
    Private Shared Function RetornaColecaoDivisa(drDivisa As IDataReader) As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.DivisaColeccion

        Dim objRetornaRespaldoCompleto As New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.DivisaColeccion

        While (drDivisa.Read)

            ' adicionar para objeto
            objRetornaRespaldoCompleto.Add(PopularDivisa(drDivisa))

        End While

        Return objRetornaRespaldoCompleto

    End Function

    ''' <summary>
    ''' Popula o datarow
    ''' </summary>
    ''' <param name="drDivisa">Registro Atual com os dados dos divisas do respaldo completo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 28/07/2009 Criado
    ''' </history>
    Private Shared Function PopularDivisa(drDivisa As IDataReader, _
                                          Optional blImporteDecl As Boolean = True) As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Divisa

        Dim objDivisa As New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Divisa

        Util.AtribuirValorObjeto(objDivisa.Parcial, drDivisa("OID_PARCIAL"), GetType(String))
        Util.AtribuirValorObjeto(objDivisa.DescripcionMedioPago, drDivisa("MEDIO_PAGO"), GetType(String))
        Util.AtribuirValorObjeto(objDivisa.Divisa, drDivisa("DIVISA"), GetType(String))
        If (blImporteDecl) Then
            Util.AtribuirValorObjeto(objDivisa.Importe, drDivisa("IMPORTE"), GetType(Decimal))
        Else
            Util.AtribuirValorObjeto(objDivisa.ImporteIngressado, drDivisa("IMPORTE"), GetType(Decimal))
        End If

        Return objDivisa

    End Function

    ''' <summary>
    ''' Percorre o dr e retorna uma coleção de divisas de respaldos completos
    ''' </summary>
    ''' <param name="drInformacaoIAC">Objeto com os dados das informações do IAC do respaldo completo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 11/08/2009 Criado
    ''' </history>
    Private Shared Function RetornaColecaoInformacaoIAC(drInformacaoIAC As IDataReader) As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIACPDFColeccion

        Dim objRetornaInformacaoIAC As New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIACPDFColeccion

        While (drInformacaoIAC.Read)

            ' adicionar para objeto
            objRetornaInformacaoIAC.Add(PopularInformacaoIAC(drInformacaoIAC))

        End While

        Return objRetornaInformacaoIAC

    End Function

    Private Shared Function RetornaTotalParciaisDeclarados(drTotalParciaisDeclarados As IDataReader) As Integer

        If (drTotalParciaisDeclarados.Read) Then
            Return drTotalParciaisDeclarados("TOTAL_PARCIALES_DECL")
        End If

        Return 0

    End Function

    ''' <summary>
    ''' Popula o datarow
    ''' </summary>
    ''' <param name="drInformacaoIAC">Registro Atual com os dados dos divisas do respaldo completo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 11/08/2009 Criado
    ''' </history>
    Private Shared Function PopularInformacaoIAC(drInformacaoIAC As IDataReader) As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIACPDF


        ' Variável que recebe as informações do IAC
        Dim informacaoIAC As New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIACPDF

        Util.AtribuirValorObjeto(informacaoIAC.Parcial, drInformacaoIAC("OID_PARCIAL"), GetType(String))
        Util.AtribuirValorObjeto(informacaoIAC.Descricao, drInformacaoIAC("DES_IAC"), GetType(String))

        ' Retorna as informações do IAC
        Return informacaoIAC

    End Function

    ''' <summary>
    ''' Popula o datarow
    ''' </summary>
    ''' <param name="drFalso">Registro Atual com os dados dos falsos</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [cazevedo] 26/03/2010 Criado
    ''' </history>
    Private Shared Function PopularFalso(drFalso As IDataReader) As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Falso

        Dim objFalso As New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Falso

        Util.AtribuirValorObjeto(objFalso.Parcial, drFalso("PARCIAL"), GetType(String))
        Util.AtribuirValorObjeto(objFalso.Tipo, drFalso("TIPO"), GetType(String))
        Util.AtribuirValorObjeto(objFalso.Divisa, drFalso("DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objFalso.Denominacion, drFalso("DENOMINACION"), GetType(String))
        Util.AtribuirValorObjeto(objFalso.NumeroSerie, drFalso("NUMERO_SERIE"), GetType(String))
        Util.AtribuirValorObjeto(objFalso.NumeroPlancha, drFalso("NUMERO_PLANCHA"), GetType(String))
        Util.AtribuirValorObjeto(objFalso.Observacion, drFalso("OBSERVACION"), GetType(String))
        Util.AtribuirValorObjeto(objFalso.NumeroUnidades, drFalso("NUMERO_UNIDADES"), GetType(String))

        Return objFalso

    End Function

    ''' <summary>
    ''' Percorre o dr e retorna uma coleção de observações
    ''' </summary>
    ''' <param name="drRespaldoCompleto">Objeto com os dados dos detalhes do respaldo completo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [cazevedo] 31/05/2010 Criado
    ''' </history>
    Private Shared Function RetornaColecaoObservaciones(drRespaldoCompleto As IDataReader) As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.ObservacionColeccion

        Dim objObservaciones As New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.ObservacionColeccion

        While (drRespaldoCompleto.Read)

            ' adicionar para objeto
            objObservaciones.Add(PopularObservacion(drRespaldoCompleto))

        End While

        Return objObservaciones

    End Function

    ''' <summary>
    ''' Popula o datarow
    ''' </summary>
    ''' <param name="drObservacion">Registro Atual com os dados de observação</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [cazevedo] 31/05/2010 Criado
    ''' </history>
    Private Shared Function PopularObservacion(drObservacion As IDataReader) As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Observacion

        Dim objObservacion As New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Observacion

        Util.AtribuirValorObjeto(objObservacion.Parcial, drObservacion("PARCIAL"), GetType(String))
        Util.AtribuirValorObjeto(objObservacion.Descripcion, drObservacion("DESCRIPCION"), GetType(String))

        Return objObservacion

    End Function

    ''' <summary>
    ''' Propaga o valor de términos iac para os meios de pagamento que não foram contados
    ''' </summary>
    ''' <param name="objRetornaRespaldoCompletoCol"></param>
    ''' <remarks></remarks>
    Private Shared Sub DuplicaValorTerminoParaMesmoParcial(ByRef objRetornaRespaldoCompletoCol As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.RespaldoCompletoCSVColeccion)

        For Each objRespaldoCompleto As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.RespaldoCompletoCSV In objRetornaRespaldoCompletoCol
            Dim objRespaldoCompletoLocal = objRespaldoCompleto

            Dim objInf = (From objinfAdi In objRespaldoCompleto.InformacionesIAC _
                          Where objinfAdi.Valor Is Nothing).ToList()

            ' se a linha não contém nenhum término com valor
            If objInf.Count = objRespaldoCompleto.InformacionesIAC.Count Then

                Dim objRespaInf = (From objRespaQ In objRetornaRespaldoCompletoCol _
                                   Where objRespaQ.InformacionesIAC.Where(Function(o) o.Valor IsNot Nothing).FirstOrDefault() IsNot Nothing _
                                   AndAlso objRespaQ.Parcial = objRespaldoCompletoLocal.Parcial).ToList()

                If objRespaInf IsNot Nothing AndAlso objRespaInf.Count > 0 Then

                    'para cada informacao de terminos vazia
                    For Each objInformacaoAdiCliente As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIAC In objRespaldoCompleto.InformacionesIAC
                        Dim objInformacaoAdiClienteLocal = objInformacaoAdiCliente

                        ' se estiver vazia
                        If objInformacaoAdiCliente.Valor Is Nothing Then
                            objInformacaoAdiCliente.Valor = objRespaInf.FirstOrDefault().InformacionesIAC.Where(Function(o) o.Descricao = objInformacaoAdiClienteLocal.Descricao).FirstOrDefault().Valor
                        End If

                    Next

                End If

            End If

        Next

    End Sub

#End Region

End Class
