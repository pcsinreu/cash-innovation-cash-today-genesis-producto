Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor

Public Class DetalleParciales

    Private Shared _descricaoIACS As New Dictionary(Of String, String)

#Region "[LISTAR]"

    ''' <summary>
    ''' Método responsável por listar los detalles parciales (formato CSV)
    ''' </summary>
    ''' <param name="CodigoCliente"></param>
    ''' <param name="CodigoDelegacion"></param>
    ''' <param name="CodigoSubCliente"></param>
    ''' <param name="ConDenominacion"></param>
    ''' <param name="ConIncidencia"></param>
    ''' <param name="EsFechaProceso"></param>
    ''' <param name="FechaDesde"></param>
    ''' <param name="FechaHasta"></param>
    ''' <param name="NumeroPrecinto"></param>
    ''' <param name="NumeroRemesa"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''   [bruno.costa]  26/08/2010  alterado
    ''' </history>
    Public Shared Function ListarDetalleParcialesCSV(CodigoDelegacion As String, _
                                                  NumeroRemesa As String, _
                                                  NumeroPrecinto As String, _
                                                  CodigoCliente As String, _
                                                  CodigoSubCliente As String, _
                                                  EsFechaProceso As String, _
                                                  FechaDesde As DateTime, _
                                                  FechaHasta As DateTime, _
                                                  ConDenominacion As Integer, _
                                                  ConIncidencia As Integer) As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial)

        ' Declara variável de retorno
        Dim objRetornaDetalleParciales As New List(Of ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial)

        Dim conexao As IDbConnection = AcessoDados.Conectar(Constantes.CONEXAO_GE)

        ' criar comando
        Dim comando As IDbCommand = conexao.CreateCommand()

        Dim drDetalleParciales As IDataReader = Nothing

        Try
            ' Limpa o objeto da memória quando termina de usá-lo
            Using comando

                ' obter procedure
                comando.CommandText = Constantes.PKG_DETALLE_PARCIALES_CSV
                comando.CommandType = CommandType.StoredProcedure

                ' setar parametros
                comando.Parameters.Add(Util.CriarParametroOracle("CV_INFPARCIAL", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("CV_INFADCLIENTE", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("CV_DECLARADOS", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("CV_EFECTIVOS", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("CV_MEDIOSPAGO", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("CV_OBSERVACIONES", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDelegacion))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NUM_REMESA", ProsegurDbType.Identificador_Alfanumerico, IIf(NumeroRemesa = String.Empty, DBNull.Value, NumeroRemesa.Replace("'", ""))))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NUM_PRECINTO", ProsegurDbType.Identificador_Alfanumerico, IIf(NumeroPrecinto = String.Empty, DBNull.Value, NumeroPrecinto.Replace("'", ""))))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, IIf(CodigoCliente = String.Empty, DBNull.Value, CodigoCliente)))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, IIf(CodigoSubCliente = String.Empty, DBNull.Value, CodigoSubCliente)))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "TIPO_FECHA", ProsegurDbType.Inteiro_Curto, EsFechaProceso))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_INI", ProsegurDbType.Data_Hora, FechaDesde))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_FIN", ProsegurDbType.Data_Hora, FechaHasta))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_INCIDENCIA", ProsegurDbType.Inteiro_Curto, ConIncidencia))

                ' executar consulta
                drDetalleParciales = comando.ExecuteReader()

                objRetornaDetalleParciales = RetornaColecaoDetalleParciales(drDetalleParciales)

            End Using

        Finally

            ' Fecha a conexão do Data Reader
            If drDetalleParciales IsNot Nothing Then
                drDetalleParciales.Close()
                drDetalleParciales.Dispose()
            End If

            ' Fecha a conexão do banco
            AcessoDados.Desconectar(conexao)

        End Try

        Return objRetornaDetalleParciales
    End Function

    ''' <summary>
    ''' Método responsável por listar los detalles parciales (formato PDF)
    ''' </summary>
    ''' <param name="CodigoCliente"></param>
    ''' <param name="CodigoDelegacion"></param>
    ''' <param name="CodigoSubCliente"></param>
    ''' <param name="ConDenominacion"></param>
    ''' <param name="ConIncidencia"></param>
    ''' <param name="EsFechaProceso"></param>
    ''' <param name="FechaDesde"></param>
    ''' <param name="FechaHasta"></param>
    ''' <param name="NumeroPrecinto"></param>
    ''' <param name="NumeroRemesa"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''   [bruno.costa]  26/08/2010  criado
    ''' </history>
    Public Shared Function ListarDetalleParcialesPDF(CodigoDelegacion As String, _
                                                  NumeroRemesa As String, _
                                                  NumeroPrecinto As String, _
                                                  CodigoCliente As String, _
                                                  CodigoSubCliente As String, _
                                                  EsFechaProceso As String, _
                                                  FechaDesde As DateTime, _
                                                  FechaHasta As DateTime, _
                                                  ConDenominacion As Integer, _
                                                  ConIncidencia As Integer) As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial)

        ' Declara variável de retorno
        Dim objRetornaDetalleParciales As New List(Of ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial)

        Dim conexao As IDbConnection = AcessoDados.Conectar(Constantes.CONEXAO_GE)

        ' criar comando
        Dim comando As IDbCommand = conexao.CreateCommand()

        Dim drDetalleParciales As IDataReader = Nothing

        Try
            ' Limpa o objeto da memória quando termina de usá-lo
            Using comando

                ' obter procedure
                comando.CommandText = Constantes.PKG_DETALLE_PARCIALES_PDF
                comando.CommandType = CommandType.StoredProcedure

                ' setar parametros
                comando.Parameters.Add(Util.CriarParametroOracle("CV_INFPARCIAL", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("CV_INFADCLIENTE", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("CV_DECLARADOS", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("CV_EFECTIVOS", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("CV_MEDIOSPAGO", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("CV_OBSERVACIONES", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDelegacion))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NUM_REMESA", ProsegurDbType.Identificador_Alfanumerico, IIf(NumeroRemesa = String.Empty, DBNull.Value, NumeroRemesa.Replace("'", ""))))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NUM_PRECINTO", ProsegurDbType.Identificador_Alfanumerico, IIf(NumeroPrecinto = String.Empty, DBNull.Value, NumeroPrecinto.Replace("'", ""))))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, IIf(CodigoCliente = String.Empty, DBNull.Value, CodigoCliente)))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, IIf(CodigoSubCliente = String.Empty, DBNull.Value, CodigoSubCliente)))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "TIPO_FECHA", ProsegurDbType.Inteiro_Curto, EsFechaProceso))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_INI", ProsegurDbType.Data_Hora, FechaDesde))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_FIN", ProsegurDbType.Data_Hora, FechaHasta))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_INCIDENCIA", ProsegurDbType.Inteiro_Curto, ConIncidencia))

                ' executar consulta
                drDetalleParciales = comando.ExecuteReader()

                objRetornaDetalleParciales = RetornaColecaoDetalleParciales(drDetalleParciales)
            End Using

        Finally

            ' Fecha a conexão do Data Reader
            If drDetalleParciales IsNot Nothing Then
                drDetalleParciales.Close()
                drDetalleParciales.Dispose()
            End If

            ' Fecha a conexão do banco
            AcessoDados.Desconectar(conexao)
        End Try

        Return objRetornaDetalleParciales
    End Function

    Private Shared Function RetornaColecaoDetalleParciales(drDetalleParciales As IDataReader) As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial)

        ' Declara variável de retorno
        Dim objRetornaDetalleParciales As New List(Of ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial)

        ' popula os detalles parciales
        While (drDetalleParciales.Read)

            ' adicionar para objeto
            objRetornaDetalleParciales.Add(PopularDetalleParciales(drDetalleParciales))

        End While

        ' Vai para o próximo cursor
        drDetalleParciales.NextResult()

        ' popula os iacs
        While (drDetalleParciales.Read)

            ' adicionar para objeto
            PopularIACs(objRetornaDetalleParciales, drDetalleParciales)

        End While

        ' Vai para o próximo cursor
        drDetalleParciales.NextResult()

        ' popula os declarados
        While (drDetalleParciales.Read)

            ' se o cursor retornar apenas uma coluna, deve mover para o próximo
            If drDetalleParciales.FieldCount = 1 Then
                Exit While
            End If

            ' adicionar para objeto
            PopularDeclarados(objRetornaDetalleParciales, drDetalleParciales)

        End While

        ' Vai para o próximo cursor
        drDetalleParciales.NextResult()

        ' popula os efectivos
        While (drDetalleParciales.Read)

            ' se o cursor retornar apenas uma coluna, deve mover para o próximo
            If drDetalleParciales.FieldCount = 1 Then
                Exit While
            End If

            ' adicionar para objeto
            PopularEfectivos(objRetornaDetalleParciales, drDetalleParciales)

        End While

        ' Vai para o próximo cursor
        drDetalleParciales.NextResult()

        ' popula os medios pago
        While (drDetalleParciales.Read)

            ' se o cursor retornar apenas uma coluna, deve mover para o próximo
            If drDetalleParciales.FieldCount = 1 Then
                Exit While
            End If

            ' adicionar para objeto
            PopularMediosPago(objRetornaDetalleParciales, drDetalleParciales)

        End While

        ' Vai para o próximo cursor
        drDetalleParciales.NextResult()

        ' popula as observaciones
        While (drDetalleParciales.Read)

            ' se o cursor retornar apenas uma coluna, deve mover para o próximo
            If drDetalleParciales.FieldCount = 1 Then
                Exit While
            End If

            ' adicionar para objeto
            PopularObservaciones(objRetornaDetalleParciales, drDetalleParciales)

        End While

        Return objRetornaDetalleParciales

    End Function

    Private Shared Function PopularDetalleParciales(drDetalleParciales As IDataReader) As ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial

        Dim objDetalleParcial As New ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial

        Util.AtribuirValorObjeto(objDetalleParcial.NumeroRemesa, drDetalleParciales("NUM_REMESA"), GetType(String))
        Util.AtribuirValorObjeto(objDetalleParcial.NumeroPrecinto, drDetalleParciales("NUM_PRECINTO"), GetType(String))
        Util.AtribuirValorObjeto(objDetalleParcial.NumeroParcial, drDetalleParciales("NUM_PARCIAL"), GetType(String))
        Util.AtribuirValorObjeto(objDetalleParcial.OidParcial, drDetalleParciales("OID_PARCIAL"), GetType(String))
        Util.AtribuirValorObjeto(objDetalleParcial.CodigoCliente, drDetalleParciales("COD_CLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objDetalleParcial.NomeCliente, drDetalleParciales("NOMBRE_CLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objDetalleParcial.CodigoSubCliente, drDetalleParciales("COD_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objDetalleParcial.NomeSubCliente, drDetalleParciales("NOMBRE_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objDetalleParcial.PuntoServicio, drDetalleParciales("PUNTO_SERVICIO"), GetType(String))
        objDetalleParcial.FechaProceso = Util.VerificarDBNull(drDetalleParciales("FECHA_PROCESO"))
        Util.AtribuirValorObjeto(objDetalleParcial.FechaTransporte, drDetalleParciales("FECHA_TRANSPORTE"), GetType(DateTime))

        Return objDetalleParcial

    End Function

    Private Shared Sub PopularIACs(ByRef objRetornaDetalleParciales As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial), drDetalleParciales As IDataReader)

        Dim objDetalleParcial As ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial = objRetornaDetalleParciales.Find(Function(dp) dp.NumeroRemesa = drDetalleParciales("NUM_REMESA") AndAlso dp.NumeroPrecinto = drDetalleParciales("NUM_PRECINTO") AndAlso dp.OidParcial = drDetalleParciales("OID_PARCIAL"))

        If objDetalleParcial IsNot Nothing Then

            Dim objIAC As New ContractoServ.DetalleParciales.GetDetalleParciales.IAC

            Util.AtribuirValorObjeto(objIAC.CodigoTermino, drDetalleParciales("COD_TERMINO"), GetType(String))
            Util.AtribuirValorObjeto(objIAC.Descricao, drDetalleParciales("DES_TERMINO"), GetType(String))
            Util.AtribuirValorObjeto(objIAC.Valor, drDetalleParciales("DES_VALOR"), GetType(String))

            objDetalleParcial.IACs.Add(objIAC)

        End If

    End Sub

    Private Shared Sub PopularDeclarados(ByRef objRetornaDetalleParciales As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial), drDetalleParciales As IDataReader)

        Dim objDetalleParcial As ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial = objRetornaDetalleParciales.Find(Function(dp) dp.NumeroRemesa = drDetalleParciales("NUM_REMESA") AndAlso dp.NumeroPrecinto = drDetalleParciales("NUM_PRECINTO") AndAlso dp.OidParcial = drDetalleParciales("OID_PARCIAL"))

        If objDetalleParcial IsNot Nothing Then

            Dim objDeclarado As New ContractoServ.DetalleParciales.GetDetalleParciales.Declarado

            Util.AtribuirValorObjeto(objDeclarado.TipoDeclarado, drDetalleParciales("TIPO_DECLARADO"), GetType(String))
            Util.AtribuirValorObjeto(objDeclarado.Divisa, drDetalleParciales("DES_DIVISA"), GetType(String))
            Util.AtribuirValorObjeto(objDeclarado.ImporteTotal, drDetalleParciales("NUM_IMPORTE_TOTAL"), GetType(Decimal))

            objDetalleParcial.Declarados.Add(objDeclarado)

        End If

    End Sub

    Private Shared Sub PopularEfectivos(ByRef objRetornaDetalleParciales As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial), drDetalleParciales As IDataReader)

        Dim objDetalleParcial As ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial = objRetornaDetalleParciales.Find(Function(dp) dp.NumeroRemesa = drDetalleParciales("NUM_REMESA") AndAlso dp.NumeroPrecinto = drDetalleParciales("NUM_PRECINTO") AndAlso dp.OidParcial = drDetalleParciales("OID_PARCIAL"))

        If objDetalleParcial IsNot Nothing Then

            Dim objEfectivo As New ContractoServ.DetalleParciales.GetDetalleParciales.Efectivo

            Util.AtribuirValorObjeto(objEfectivo.Divisa, drDetalleParciales("DIVISA"), GetType(String))
            Util.AtribuirValorObjeto(objEfectivo.Denominacion, drDetalleParciales("DENOMINACION"), GetType(String))
            Util.AtribuirValorObjeto(objEfectivo.Unidades, drDetalleParciales("UNIDADES"), GetType(Integer))
            Util.AtribuirValorObjeto(objEfectivo.Falsos, drDetalleParciales("FALSO"), GetType(Integer))
            Util.AtribuirValorObjeto(objEfectivo.Tipo, drDetalleParciales("TIPO_EFECTIVO"), GetType(String))
            Util.AtribuirValorObjeto(objEfectivo.Calidad, drDetalleParciales("COD_CALIDAD"), GetType(String))

            If Not String.IsNullOrEmpty(objEfectivo.Calidad) Then
                objEfectivo.Calidad = Traduzir("005_billete_deteriorado")
            End If

            objDetalleParcial.Efectivos.Add(objEfectivo)

        End If

    End Sub

    Private Shared Sub PopularMediosPago(ByRef objRetornaDetalleParciales As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial), drDetalleParciales As IDataReader)

        Dim objDetalleParcial As ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial = objRetornaDetalleParciales.Find(Function(dp) dp.NumeroRemesa = drDetalleParciales("NUM_REMESA") AndAlso dp.NumeroPrecinto = drDetalleParciales("NUM_PRECINTO") AndAlso dp.OidParcial = drDetalleParciales("OID_PARCIAL"))

        If objDetalleParcial IsNot Nothing Then

            Dim objMedioPago As New ContractoServ.DetalleParciales.GetDetalleParciales.MedioPago

            Util.AtribuirValorObjeto(objMedioPago.Divisa, drDetalleParciales("DIVISA"), GetType(String))
            Util.AtribuirValorObjeto(objMedioPago.TipoMedioPago, drDetalleParciales("TIPO_MEDIO_PAGO"), GetType(String))
            Util.AtribuirValorObjeto(objMedioPago.Valor, drDetalleParciales("VALOR"), GetType(Decimal))

            objDetalleParcial.MediosPago.Add(objMedioPago)

        End If

    End Sub

    Private Shared Sub PopularObservaciones(ByRef objRetornaDetalleParciales As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial), drDetalleParciales As IDataReader)

        Dim objDetalleParcial As ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial = objRetornaDetalleParciales.Find(Function(dp) dp.NumeroRemesa = drDetalleParciales("NUM_REMESA") AndAlso dp.NumeroPrecinto = drDetalleParciales("NUM_PRECINTO") AndAlso dp.OidParcial = drDetalleParciales("OID_PARCIAL"))

        If objDetalleParcial IsNot Nothing Then

            objDetalleParcial.Observaciones.Add(drDetalleParciales("DES_COMENTARIO"))

        End If

    End Sub

#End Region

End Class
