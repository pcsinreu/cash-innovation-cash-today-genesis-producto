Imports Prosegur.DbHelper

Public Class CorteParcial

#Region "[LISTAR]"

    ''' <summary>
    ''' Método responsável por listar los cortes parciais para o arquivo CSV
    ''' </summary>
    ''' <param name="objPeticion">Objeto com os filtros da pesquisa</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 17/07/2009 Criado
    ''' [magnum.oliveira] 27/10/2009 Alterado
    ''' </history>
    Public Shared Function ListarCorteParcialCSV(objPeticion As ContractoServ.CorteParcial.GetCortesParciais.Peticion) As ContractoServ.CorteParcial.GetCortesParciais.CorteParcialCSVColeccion

        ' Declara variável de retorno
        Dim objRetornaCorteParcial As New ContractoServ.CorteParcial.GetCortesParciais.CorteParcialCSVColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' Limpa o objeto da memória quando termina de usá-lo
        Using comando

            ' obter procedure
            comando.CommandText = Constantes.SP_CONTEO_PARCIAL_CSV
            comando.CommandType = CommandType.StoredProcedure

            ' setar parametros
            comando.Parameters.Add(Util.CriarParametroOracle("CV_RESPALDOCOMPLETO", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
            comando.Parameters.Add(Util.CriarParametroOracle("CV_BILLETESFALSOS", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoDelegacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CANAL", ProsegurDbType.Observacao_Curta, IIf(objPeticion.Canales IsNot Nothing AndAlso objPeticion.Canales.Count > 0, Util.RetornaStringListaValores(objPeticion.Canales), DBNull.Value)))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FEC_INI", ProsegurDbType.Data_Hora, objPeticion.FechaDesde))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FEC_FIN", ProsegurDbType.Data_Hora, objPeticion.FechaHasta))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "ES_PENDIENTE", ProsegurDbType.Logico, objPeticion.EsRemesaPendiente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "ES_FEC_PROCESO", ProsegurDbType.Inteiro_Curto, objPeticion.EsFechaProceso))

            ' executar consulta
            Dim drCorteParcial As IDataReader = AcessoDados.ExecutarDataReader(Constantes.CONEXAO_GE, comando)

            ' Limpa o objeto da memória quando termina de usá-lo
            Using drCorteParcial
                Try

                    'Percorre o dr e retorna uma coleção de cortes parciais.
                    objRetornaCorteParcial = RetornaColecaoCorteParcialCSV(drCorteParcial)

                Finally
                    ' Fecha a conexão do Data Reader
                    If drCorteParcial IsNot Nothing Then
                        drCorteParcial.Close()
                        drCorteParcial.Dispose()
                    End If
                    ' Fecha a conexão do banco
                    AcessoDados.Desconectar(comando.Connection)
                End Try
            End Using
        End Using

        Return objRetornaCorteParcial

    End Function

    ''' <summary>
    ''' Percorre o dr e retorna uma coleção de cortes parciais
    ''' </summary>
    ''' <param name="drCorteParcial">Objeto com os dados do corte parcial</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 17/07/2009 Criado
    ''' </history>
    Private Shared Function RetornaColecaoCorteParcialCSV(drCorteParcial As IDataReader) As ContractoServ.CorteParcial.GetCortesParciais.CorteParcialCSVColeccion

        Dim objRetornaCorteParcial As New ContractoServ.CorteParcial.GetCortesParciais.CorteParcialCSVColeccion

        While (drCorteParcial.Read)

            ' adicionar para objeto
            objRetornaCorteParcial.Add(PopularCorteParcialCSV(drCorteParcial))

        End While

        ' Vai para o próximo cursor
        drCorteParcial.NextResult()

        While (drCorteParcial.Read)

            Dim CorteParcial = (From cp In objRetornaCorteParcial _
                                Where cp.Remesa = drCorteParcial("REMESA") _
                                AndAlso cp.MedioPago = "Efectivo" _
                                AndAlso cp.DescricionDivisa = drCorteParcial("DIVISA")).FirstOrDefault()

            If CorteParcial IsNot Nothing Then

                If CorteParcial.Falsos Is Nothing Then
                    CorteParcial.Falsos = New ContractoServ.CorteParcial.GetCortesParciais.FalsoColeccion()
                End If

                CorteParcial.Falsos.Add(PopularFalso(drCorteParcial))

            End If

        End While

        Return objRetornaCorteParcial

    End Function

    ''' <summary>
    ''' Popula o datarow
    ''' </summary>
    ''' <param name="drCorteParcial">Registro Atual com os dados do corte parcial</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 17/07/2009 Criado
    ''' </history>
    Private Shared Function PopularCorteParcialCSV(drCorteParcial As IDataReader) As ContractoServ.CorteParcial.GetCortesParciais.CorteParcialCSV

        Dim objCorteParcial As New ContractoServ.CorteParcial.GetCortesParciais.CorteParcialCSV

        Util.AtribuirValorObjeto(objCorteParcial.Recuento, drCorteParcial("RECUENTO"), GetType(String))
        Util.AtribuirValorObjeto(objCorteParcial.Fecha, drCorteParcial("FECHA"), GetType(DateTime))
        Util.AtribuirValorObjeto(objCorteParcial.Letra, drCorteParcial("LETRA"), GetType(String))
        Util.AtribuirValorObjeto(objCorteParcial.F22, drCorteParcial("F22"), GetType(String))
        Util.AtribuirValorObjeto(objCorteParcial.OidRemesaOri, drCorteParcial("OID_REMESA_ORI"), GetType(String))
        Util.AtribuirValorObjeto(objCorteParcial.CodSubCliente, drCorteParcial("COD_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objCorteParcial.Estacion, drCorteParcial("ESTACION"), GetType(String))
        Util.AtribuirValorObjeto(objCorteParcial.DescricionEstacion, drCorteParcial("DES_ESTACION"), GetType(String))
        Util.AtribuirValorObjeto(objCorteParcial.MedioPago, drCorteParcial("MEDIO_PAGO"), GetType(String))
        Util.AtribuirValorObjeto(objCorteParcial.DescricionMedioPago, drCorteParcial("DES_MEDIO_PAGO"), GetType(String))
        Util.AtribuirValorObjeto(objCorteParcial.Divisa, drCorteParcial("DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objCorteParcial.DescricionDivisa, drCorteParcial("DES_DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objCorteParcial.DeclaradoRemesa, drCorteParcial("DECLARADO_REMESA"), GetType(Decimal))
        Util.AtribuirValorObjeto(objCorteParcial.DeclaradoBultoSuMaxRemesa, drCorteParcial("DECLARADO_BULTO_SUMAXREMESA"), GetType(Decimal))
        Util.AtribuirValorObjeto(objCorteParcial.DeclaradoParcialSuMaxRemesa, drCorteParcial("DECLARADO_PARCIAL_SUMAXREMESA"), GetType(Decimal))
        Util.AtribuirValorObjeto(objCorteParcial.IngresadoSobre, drCorteParcial("INGRESADO_POR_SOBRE"), GetType(Decimal))
        Util.AtribuirValorObjeto(objCorteParcial.Recontado, drCorteParcial("RECONTADO"), GetType(Decimal))
        Util.AtribuirValorObjeto(objCorteParcial.Observaciones, drCorteParcial("OBSERVACIONES"), GetType(String))
        Util.AtribuirValorObjeto(objCorteParcial.Remesa, drCorteParcial("OID_REMESA"), GetType(String))

        Return objCorteParcial

    End Function

    ''' <summary>
    ''' Método responsável por listar los cortes parciais para o arquivo PDF
    ''' </summary>
    ''' <param name="objPeticion">Objeto com os filtros da pesquisa</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 17/07/2009 Criado
    ''' [magnum.oliveira] 27/10/2009 Alterado
    ''' </history>
    Public Shared Function ListarCorteParcialPDF(objPeticion As ContractoServ.CorteParcial.GetCortesParciais.Peticion) As ContractoServ.CorteParcial.GetCortesParciais.CorteParcialPDF

        ' Declara variável de retorno
        Dim objRetornaCorteParcial As New ContractoServ.CorteParcial.GetCortesParciais.CorteParcialPDF

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' Limpa o objeto da memória quando termina de usá-lo
        Using comando
            ' obter query
            comando.CommandText = Constantes.SP_CONTEO_PARCIAL_PDF
            comando.CommandType = CommandType.StoredProcedure

            ' setar parametros
            comando.Parameters.Add(Util.CriarParametroOracle("CV_CORTEP_DETALLE", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
            comando.Parameters.Add(Util.CriarParametroOracle("CV_CORTEP_SOBRES", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
            comando.Parameters.Add(Util.CriarParametroOracle("CV_OBSERVACIONES", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
            comando.Parameters.Add(Util.CriarParametroOracle("CV_BILLETESFALSOS", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoDelegacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CANAL", ProsegurDbType.Observacao_Curta, IIf(objPeticion.Canales IsNot Nothing AndAlso objPeticion.Canales.Count > 0, Util.RetornaStringListaValores(objPeticion.Canales), DBNull.Value)))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FEC_INI", ProsegurDbType.Data_Hora, objPeticion.FechaDesde))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FEC_FIN", ProsegurDbType.Data_Hora, objPeticion.FechaHasta))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "ES_PENDIENTE", ProsegurDbType.Logico, objPeticion.EsRemesaPendiente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "ES_FEC_PROCESO", ProsegurDbType.Inteiro_Curto, objPeticion.EsFechaProceso))

            ' executar consulta
            Dim drCorteParcial As IDataReader = AcessoDados.ExecutarDataReader(Constantes.CONEXAO_GE, comando)

            ' Limpa o objeto da memória quando termina de usá-lo
            Using drCorteParcial
                Try
                    'Percorre o dr e retorna uma coleção de detalhes de cortes parciais.
                    objRetornaCorteParcial.Detalles = RetornaColecaoDetalle(drCorteParcial)

                    ' Vai para o próximo cursor
                    drCorteParcial.NextResult()

                    'Percorre o dr e retorna uma coleção de sobres de cortes parciais.
                    objRetornaCorteParcial.Sobres = RetornaColecaoSobre(drCorteParcial)

                    ' Vai para o próximo cursor
                    drCorteParcial.NextResult()

                    ' Percorre o dr e retorna uma coleção de observações de cortes parciais
                    objRetornaCorteParcial.Observaciones = RetornaColecaoObservacion(drCorteParcial)

                    ' Vai para o próximo cursor
                    drCorteParcial.NextResult()

                    ' Percorre o dr e retorna uma coleção de falsos
                    objRetornaCorteParcial.Falsos = RetornaColecaoFalso(drCorteParcial)
                Finally
                    ' Fecha a conexão do Data Reader
                    If drCorteParcial IsNot Nothing Then
                        drCorteParcial.Close()
                        drCorteParcial.Dispose()
                    End If
                    ' Fecha a conexão do banco
                    AcessoDados.Desconectar(comando.Connection)
                End Try
            End Using
        End Using

        ' Retorna os dados da consulta
        Return objRetornaCorteParcial

    End Function

    ''' <summary>
    ''' Percorre o dr e retorna uma coleção de detalhes de cortes parciais
    ''' </summary>
    ''' <param name="drDetalhe">Objeto com os dados dos detalhes do corte parcial</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 28/07/2009 Criado
    ''' </history>
    Private Shared Function RetornaColecaoDetalle(drDetalhe As IDataReader) As ContractoServ.CorteParcial.GetCortesParciais.DetalleColeccion

        Dim objRetornaCorteParcial As New ContractoServ.CorteParcial.GetCortesParciais.DetalleColeccion

        While (drDetalhe.Read)

            ' adicionar para objeto
            objRetornaCorteParcial.Add(PopularDetalle(drDetalhe))

        End While

        Return objRetornaCorteParcial

    End Function

    ''' <summary>
    ''' Popula o datarow
    ''' </summary>
    ''' <param name="drDetalhe">Registro Atual com os dados dos detalhes do corte parcial</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 28/07/2009 Criado
    ''' </history>
    Private Shared Function PopularDetalle(drDetalhe As IDataReader) As ContractoServ.CorteParcial.GetCortesParciais.Detalle

        Dim objDetalhe As New ContractoServ.CorteParcial.GetCortesParciais.Detalle

        Util.AtribuirValorObjeto(objDetalhe.Letra, drDetalhe("LETRA"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.Remesa, drDetalhe("REMESA"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.F22, drDetalhe("F22"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.OidRemesaOri, drDetalhe("OID_REMESA_ORI"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.CodSubCliente, drDetalhe("COD_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.FechaTransporte, drDetalhe("FEC_TRANSPORTE"), GetType(DateTime))
        Util.AtribuirValorObjeto(objDetalhe.Proceso, drDetalhe("PROCESO"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.Estacion, drDetalhe("ESTACION"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.DescricionEstacion, drDetalhe("DES_ESTACION"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.MedioPago, drDetalhe("MEDIO_PAGO"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.DescricionMedioPago, drDetalhe("DES_MEDIO_PAGO"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.Divisa, drDetalhe("DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.DescricionDivisa, drDetalhe("DES_DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objDetalhe.Declarado, drDetalhe("DECLARADO"), GetType(Decimal))
        Util.AtribuirValorObjeto(objDetalhe.Ingresado, drDetalhe("INGRESADO"), GetType(Decimal))
        Util.AtribuirValorObjeto(objDetalhe.Recontado, drDetalhe("RECONTADO"), GetType(Decimal))

        Return objDetalhe

    End Function

    ''' <summary>
    ''' Percorre o dr e retorna uma coleção de sobres de cortes parciais
    ''' </summary>
    ''' <param name="drSobre">Objeto com os dados dos sobres do corte parcial</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 28/07/2009 Criado
    ''' </history>
    Private Shared Function RetornaColecaoSobre(drSobre As IDataReader) As ContractoServ.CorteParcial.GetCortesParciais.SobreColeccion

        Dim objRetornaSobre As New ContractoServ.CorteParcial.GetCortesParciais.SobreColeccion

        While (drSobre.Read)

            ' adicionar para objeto
            objRetornaSobre.Add(PopularSobre(drSobre))

        End While

        Return objRetornaSobre

    End Function

    ''' <summary>
    ''' Popula o datarow
    ''' </summary>
    ''' <param name="drSobre">Registro Atual com os dados dos sobres do corte parcial</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 28/07/2009 Criado
    ''' </history>
    Private Shared Function PopularSobre(drSobre As IDataReader) As ContractoServ.CorteParcial.GetCortesParciais.Sobre

        Dim objSobre As New ContractoServ.CorteParcial.GetCortesParciais.Sobre

        Util.AtribuirValorObjeto(objSobre.ParcialesContados, drSobre("PARCIALES_CONTADOS"), GetType(Decimal))
        Util.AtribuirValorObjeto(objSobre.ParcialesIngresados, drSobre("PARCIALES_INGRESADOS"), GetType(Decimal))
        Util.AtribuirValorObjeto(objSobre.ParcialesDeclarados, drSobre("PARCIALES_DECLARADOS"), GetType(Decimal))
        Util.AtribuirValorObjeto(objSobre.Remesa, drSobre("REMESA"), GetType(String))

        Return objSobre

    End Function

    ''' <summary>
    ''' Percorre o dr e retorna uma coleção de observaciones de cortes parciais
    ''' </summary>
    ''' <param name="drObservaciones">Objeto com os dados dos observaciones do corte parcial</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 06/08/2009 Criado
    ''' </history>
    Private Shared Function RetornaColecaoObservacion(drObservaciones As IDataReader) As ContractoServ.CorteParcial.GetCortesParciais.ObservacionColeccion

        Dim objRetornaObservacion As New ContractoServ.CorteParcial.GetCortesParciais.ObservacionColeccion

        While (drObservaciones.Read)

            ' adicionar para objeto
            objRetornaObservacion.Add(PopularObservacion(drObservaciones))

        End While

        Return objRetornaObservacion

    End Function

    ''' <summary>
    ''' Popula o datarow
    ''' </summary>
    ''' <param name="drObservacion">Registro Atual com os dados da observacion do corte parcial</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 06/08/2009 Criado
    ''' </history>
    Private Shared Function PopularObservacion(drObservacion As IDataReader) As ContractoServ.CorteParcial.GetCortesParciais.Observacion

        Dim objObservacion As New ContractoServ.CorteParcial.GetCortesParciais.Observacion

        Util.AtribuirValorObjeto(objObservacion.Remesa, drObservacion("REMESA"), GetType(String))
        Util.AtribuirValorObjeto(objObservacion.Descricion, drObservacion("OBSERVACION"), GetType(String))

        Return objObservacion

    End Function

    ''' <summary>
    ''' Percorre o dr e retorna uma coleção de sobres de falsos
    ''' </summary>
    ''' <param name="drFalso">Objeto com os dados dos falsos</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [cazevedo] 26/03/2010 Criado
    ''' </history>
    Private Shared Function RetornaColecaoFalso(drFalso As IDataReader) As ContractoServ.CorteParcial.GetCortesParciais.FalsoColeccion

        Dim objRetornaFalso As New ContractoServ.CorteParcial.GetCortesParciais.FalsoColeccion

        While (drFalso.Read)

            ' adicionar para objeto
            objRetornaFalso.Add(PopularFalso(drFalso))

        End While

        Return objRetornaFalso

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
    Private Shared Function PopularFalso(drFalso As IDataReader) As ContractoServ.CorteParcial.GetCortesParciais.Falso

        Dim objFalso As New ContractoServ.CorteParcial.GetCortesParciais.Falso

        Util.AtribuirValorObjeto(objFalso.Remesa, drFalso("REMESA"), GetType(String))
        Util.AtribuirValorObjeto(objFalso.Tipo, drFalso("TIPO"), GetType(String))
        Util.AtribuirValorObjeto(objFalso.Divisa, drFalso("DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objFalso.Denominacion, drFalso("DENOMINACION"), GetType(String))
        Util.AtribuirValorObjeto(objFalso.NumeroSerie, drFalso("NUMERO_SERIE"), GetType(String))
        Util.AtribuirValorObjeto(objFalso.NumeroPlancha, drFalso("NUMERO_PLANCHA"), GetType(String))
        Util.AtribuirValorObjeto(objFalso.Observacion, drFalso("OBSERVACION"), GetType(String))
        Util.AtribuirValorObjeto(objFalso.NumeroUnidades, drFalso("NUMERO_UNIDADES"), GetType(String))

        Return objFalso

    End Function


#End Region

End Class