Imports Prosegur.DbHelper

Public Class ContadoPuesto

#Region "[VARIÁVEIS]"

#End Region

#Region "[LISTAR]"

    ''' <summary>
    ''' Método responsável por listar os contados por puesto para o arquivo CSV
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 13/08/2010 Criado
    ''' </history>
    Public Shared Function ListarContadosPuestoCSV(CodigoDelegacion As String, _
                                                   CodigoPuesto As String, _
                                                   CodigoOperario As String, _
                                                   HoraInicio As Nullable(Of DateTime), _
                                                   HoraFin As Nullable(Of DateTime), _
                                                   NumRemesa As String, _
                                                   NumPrecinto As String, _
                                                   CodigoCliente As String, _
                                                   CodSubcliente As String, _
                                                   TipoFecha As Integer, _
                                                   FechaInicio As DateTime, _
                                                   FechaFin As DateTime, _
                                                   Optional BolIncidencia As Integer = 0) As List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto)

        ' Declara variável de retorno
        Dim objRetorno As New List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto)

        Dim conexao As IDbConnection = AcessoDados.Conectar(Constantes.CONEXAO_GE)

        ' criar comando
        Dim comando As IDbCommand = conexao.CreateCommand()

        'Cria DataReader
        Dim dr As IDataReader = Nothing

        Try

            ' Limpa o objeto da memória quando termina de usá-lo
            Using comando

                ' obter procedure
                comando.CommandText = Constantes.SP_CONTADO_PUESTO_CSV
                comando.CommandType = CommandType.StoredProcedure

                ' setar parametros

                ' cursores
                comando.Parameters.Add(Util.CriarParametroOracle("cv_InfParcial", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("cv_Declarados", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("cv_Efectivos", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("cv_MediosPago", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("cv_Observaciones", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))

                ' parametros
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDelegacion))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, If(CodigoPuesto = String.Empty, DBNull.Value, CodigoPuesto.Replace("'", ""))))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_OPERARIO", ProsegurDbType.Identificador_Alfanumerico, If(CodigoOperario = String.Empty, DBNull.Value, CodigoOperario.Replace("'", ""))))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "HORA_INI", ProsegurDbType.Data_Hora, If(HoraInicio.HasValue, HoraInicio.Value, DBNull.Value)))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "HORA_FIN", ProsegurDbType.Data_Hora, If(HoraFin.HasValue, HoraFin.Value, DBNull.Value)))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NUM_REMESA", ProsegurDbType.Identificador_Alfanumerico, If(NumRemesa = String.Empty, DBNull.Value, NumRemesa.Replace("'", ""))))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NUM_PRECINTO", ProsegurDbType.Identificador_Alfanumerico, If(NumPrecinto = String.Empty, DBNull.Value, NumPrecinto.Replace("'", ""))))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, If(CodigoCliente = String.Empty, DBNull.Value, CodigoCliente)))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, If(CodSubcliente = String.Empty, DBNull.Value, CodSubcliente)))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "TIPO_FECHA", ProsegurDbType.Inteiro_Curto, TipoFecha))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_INI", DbType.Date, FechaInicio))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_FIN", DbType.Date, FechaFin))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_INCIDENCIA", ProsegurDbType.Inteiro_Curto, BolIncidencia))

                ' executar consulta
                dr = comando.ExecuteReader()

                'Percorre o dr e retorna uma coleção de respaldos completos.
                objRetorno = RetornarContadosPuesto(dr)


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
    ''' Método responsável por listar os contados por puesto para o arquivo CSV
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 20/08/2010 Criado
    ''' </history>
    Public Shared Function ListarContadosPuestoPDF(CodigoDelegacion As String, _
                                                   CodigoPuesto As String, _
                                                   CodigoOperario As String, _
                                                   HoraInicio As Nullable(Of DateTime), _
                                                   HoraFin As Nullable(Of DateTime), _
                                                   NumRemesa As String, _
                                                   NumPrecinto As String, _
                                                   CodigoCliente As String, _
                                                   CodSubcliente As String, _
                                                   TipoFecha As Integer, _
                                                   FechaInicio As DateTime, _
                                                   FechaFin As DateTime, _
                                                   Optional BolIncidencia As Integer = 0) As List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto)

        ' Declara variável de retorno
        Dim objRetorno As New List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto)

        Dim conexao As IDbConnection = AcessoDados.Conectar(Constantes.CONEXAO_GE)

        ' criar comando
        Dim comando As IDbCommand = conexao.CreateCommand()

        ' cria datareader
        Dim dr As IDataReader = Nothing

        Try
            ' Limpa o objeto da memória quando termina de usá-lo
            Using comando

                ' obter procedure
                comando.CommandText = Constantes.SP_CONTADO_PUESTO_PDF
                comando.CommandType = CommandType.StoredProcedure

                ' setar parametros

                ' cursores
                comando.Parameters.Add(Util.CriarParametroOracle("cv_InfParcial", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("cv_Declarados", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("cv_Efectivos", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("cv_MediosPago", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("cv_Observaciones", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))

                ' parametros
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDelegacion))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, If(CodigoPuesto = String.Empty, DBNull.Value, CodigoPuesto.Replace("'", ""))))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_OPERARIO", ProsegurDbType.Identificador_Alfanumerico, If(CodigoOperario = String.Empty, DBNull.Value, CodigoOperario.Replace("'", ""))))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "HORA_INI", ProsegurDbType.Data_Hora, If(HoraInicio.HasValue, HoraInicio.Value, DBNull.Value)))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "HORA_FIN", ProsegurDbType.Data_Hora, If(HoraFin.HasValue, HoraFin.Value, DBNull.Value)))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NUM_REMESA", ProsegurDbType.Identificador_Alfanumerico, If(NumRemesa = String.Empty, DBNull.Value, NumRemesa.Replace("'", ""))))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NUM_PRECINTO", ProsegurDbType.Identificador_Alfanumerico, If(NumPrecinto = String.Empty, DBNull.Value, NumPrecinto.Replace("'", ""))))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, If(CodigoCliente = String.Empty, DBNull.Value, CodigoCliente)))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, If(CodSubcliente = String.Empty, DBNull.Value, CodSubcliente)))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "TIPO_FECHA", ProsegurDbType.Inteiro_Curto, TipoFecha))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_INI", DbType.Date, FechaInicio))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_FIN", DbType.Date, FechaFin))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_INCIDENCIA", ProsegurDbType.Inteiro_Curto, BolIncidencia))

                ' executar consulta
                dr = comando.ExecuteReader()


                'Percorre o dr e retorna uma coleção de respaldos completos.
                objRetorno = RetornarContadosPuesto(dr)

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
    ''' Percorre o dr e retorna uma coleção de contados por puesto
    ''' </summary>
    ''' <param name="drContados">Objeto com os dados do retornados pela procedure do relatório</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/08/2010 Criado
    ''' </history>
    Private Shared Function RetornarContadosPuesto(drContados As IDataReader) As List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto)

        Dim objContadosXPuesto As New List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto)

        While (drContados.Read)
            ' preenche informações das parciais retornadas pelo cursor cv_InfParcial: 
            objContadosXPuesto.Add(ObtenerInfParcial(drContados))
        End While

        ' Vai para o próximo cursor
        drContados.NextResult()

        While (drContados.Read)
            ' preenche declarados retornados pelo cursor cv_Declarados: 
            ObtenerDeclaradoXParcial(drContados, objContadosXPuesto)
        End While

        ' Vai para o próximo cursor
        drContados.NextResult()

        While (drContados.Read)
            ' se o cursor retornar apenas uma coluna, deve mover para o próximo
            If drContados.FieldCount = 1 Then
                Exit While
            End If
            ' preenche efetivos retornados pelo cursor cv_Efectivos: 
            ObtenerEfectivoXParcial(drContados, objContadosXPuesto)
        End While

        ' Vai para o próximo cursor
        drContados.NextResult()

        While (drContados.Read)
            ' se o cursor retornar apenas uma coluna, deve mover para o próximo
            If drContados.FieldCount = 1 Then
                Exit While
            End If
            ' preenche meios de pagamento retornados pelo cursor cv_MediosPago: 
            ObtenerMedioPagoXParcial(drContados, objContadosXPuesto)
        End While

        ' Vai para o próximo cursor
        drContados.NextResult()

        While (drContados.Read)
            ' se o cursor retornar apenas uma coluna, deve mover para o próximo
            If drContados.FieldCount = 1 Then
                Exit While
            End If
            ' preenche observações retornadas pelo cursor cv_Observaciones: 
            ObtenerObservacionesXParcial(drContados, objContadosXPuesto)
        End While

        Return objContadosXPuesto

    End Function

    ''' <summary>
    ''' Preenche informações da parcial
    ''' </summary>
    ''' <param name="drContados"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/08/2010 Criado
    ''' </history>
    Private Shared Function ObtenerInfParcial(drContados As IDataReader) As ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto

        Dim infParcial As New ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto

        With infParcial
            Util.AtribuirValorObjeto(infParcial.CodPuesto, drContados("COD_PUESTO"), GetType(String))
            Util.AtribuirValorObjeto(infParcial.CodUsuario, drContados("COD_USUARIO"), GetType(String))
            Util.AtribuirValorObjeto(infParcial.CodCliente, drContados("COD_CLIENTE"), GetType(String))
            Util.AtribuirValorObjeto(infParcial.NombreCliente, drContados("NOMBRE_CLIENTE"), GetType(String))
            Util.AtribuirValorObjeto(infParcial.CodSubcliente, drContados("COD_SUBCLIENTE"), GetType(String))
            Util.AtribuirValorObjeto(infParcial.NombreSubcliente, drContados("NOMBRE_SUBCLIENTE"), GetType(String))
            Util.AtribuirValorObjeto(infParcial.PuntoServicio, drContados("PUNTO_SERVICIO"), GetType(String))
            infParcial.FechaProceso = Util.VerificarDBNull(drContados("FECHA_PROCESO"))
            Util.AtribuirValorObjeto(infParcial.FechaTransporte, drContados("FECHA_TRANSPORTE"), GetType(String))
            Util.AtribuirValorObjeto(infParcial.NumRemesa, drContados("NUM_REMESA"), GetType(String))
            Util.AtribuirValorObjeto(infParcial.NumPrecinto, drContados("NUM_PRECINTO"), GetType(String))
            Util.AtribuirValorObjeto(infParcial.NumParcial, drContados("NUM_PARCIAL"), GetType(String))
        End With

        Return infParcial

    End Function

    ''' <summary>
    ''' Preenche declarados da parcial
    ''' </summary>
    ''' <param name="drContados"></param>
    ''' <param name="objContados"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/08/2010 Criado
    ''' </history>
    Private Shared Sub ObtenerDeclaradoXParcial(drContados As IDataReader, _
                                                 ByRef objContados As List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto))

        Dim declarado As New ContractoServ.ContadoPuesto.ListarContadoPuesto.Declarado

        ' obtém a parcial do declarado
        Dim infParcial As ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto

        infParcial = (From p As ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto In objContados _
                      Where p.NumRemesa = drContados("NUM_REMESA") AndAlso _
                      p.NumPrecinto = drContados("NUM_PRECINTO") AndAlso _
                      p.NumParcial = drContados("NUM_PARCIAL")).FirstOrDefault()

        If infParcial IsNot Nothing Then

            ' se encontrou parcial, preenche declarado
            With declarado
                Util.AtribuirValorObjeto(declarado.DesDivisa, drContados("DES_DIVISA"), GetType(String))
                Util.AtribuirValorObjeto(declarado.NumImporteTotal, drContados("NUM_IMPORTE_TOTAL"), GetType(Decimal))
                ' Tipo de declarado (‘P’ – parcial, ‘B’ – bulto y ‘R’ - remesa)
                Util.AtribuirValorObjeto(declarado.TipoDeclarados, drContados("TIPO_DECLARADO"), GetType(String))
            End With

            ' adiciona declarado à parcial
            infParcial.Declarados.Add(declarado)

        End If

    End Sub

    ''' <summary>
    ''' Preenche efetivos da parcial
    ''' </summary>
    ''' <param name="drContados"></param>
    ''' <param name="objContados"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/08/2010 Criado
    ''' </history>
    Private Shared Sub ObtenerEfectivoXParcial(drContados As IDataReader, _
                                                ByRef objContados As List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto))

        Dim efectivo As New ContractoServ.ContadoPuesto.ListarContadoPuesto.Efectivo

        ' obtém a parcial do declarado
        Dim infParcial As ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto

        infParcial = (From p As ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto In objContados _
                      Where p.NumRemesa = drContados("NUM_REMESA") AndAlso _
                      p.NumPrecinto = drContados("NUM_PRECINTO") AndAlso _
                      p.NumParcial = drContados("NUM_PARCIAL")).FirstOrDefault()

        If infParcial IsNot Nothing Then

            ' se encontrou parcial, preenche efetivo
            With efectivo
                Util.AtribuirValorObjeto(efectivo.Divisa, drContados("DIVISA"), GetType(String))
                Util.AtribuirValorObjeto(efectivo.Denominacion, drContados("DENOMINACION"), GetType(Decimal))
                Util.AtribuirValorObjeto(efectivo.Unidades, drContados("UNIDADES"), GetType(Integer))
                Util.AtribuirValorObjeto(efectivo.Falsos, drContados("FALSO"), GetType(Integer))
                Util.AtribuirValorObjeto(efectivo.Tipo, drContados("TIPO_EFECTIVO"), GetType(String))
            End With

            ' adiciona efetivo à parcial
            infParcial.Efectivos.Add(efectivo)

        End If

    End Sub

    ''' <summary>
    ''' Preenche efetivos da parcial
    ''' </summary>
    ''' <param name="drContados"></param>
    ''' <param name="objContados"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/08/2010 Criado
    ''' </history>
    Private Shared Sub ObtenerMedioPagoXParcial(drContados As IDataReader, _
                                                 ByRef objContados As List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto))

        Dim mediosPago As New ContractoServ.ContadoPuesto.ListarContadoPuesto.MedioPago

        ' obtém a parcial do declarado
        Dim infParcial As ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto

        infParcial = (From p As ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto In objContados _
                      Where p.NumRemesa = drContados("NUM_REMESA") AndAlso _
                      p.NumPrecinto = drContados("NUM_PRECINTO") AndAlso _
                      p.NumParcial = drContados("NUM_PARCIAL")).FirstOrDefault()

        If infParcial IsNot Nothing Then

            ' se encontrou parcial, preenche meio de pagamento
            With mediosPago
                Util.AtribuirValorObjeto(mediosPago.Divisa, drContados("DIVISA"), GetType(String))
                ' Tipo del medio de pago (Cheque, Otros Valores o Ticket)
                Util.AtribuirValorObjeto(mediosPago.TipoMedioPago, drContados("TIPO_MEDIO_PAGO"), GetType(String))
                Util.AtribuirValorObjeto(mediosPago.Valor, drContados("VALOR"), GetType(Decimal))
            End With

            ' adiciona declarados à parcial
            infParcial.MediosPago.Add(mediosPago)

        End If

    End Sub

    ''' <summary>
    ''' Preenche efetivos da parcial
    ''' </summary>
    ''' <param name="drContados"></param>
    ''' <param name="objContados"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/08/2010 Criado
    ''' </history>
    Private Shared Sub ObtenerObservacionesXParcial(drContados As IDataReader, _
                                                    ByRef objContados As List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto))

        Dim infParcial As ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto

        ' obtém a parcial do declarado
        infParcial = (From p As ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto In objContados _
                      Where p.NumRemesa = drContados("NUM_REMESA") AndAlso _
                      p.NumPrecinto = drContados("NUM_PRECINTO") AndAlso _
                      p.NumParcial = drContados("NUM_PARCIAL")).FirstOrDefault()

        If infParcial IsNot Nothing Then

            ' obtém observação 
            Dim observacao As String = String.Empty

            Util.AtribuirValorObjeto(observacao, drContados("DES_COMENTARIO"), GetType(String))

            ' adiciona observação à parcial
            infParcial.Observaciones.Add(observacao)

        End If

    End Sub

#End Region

End Class
