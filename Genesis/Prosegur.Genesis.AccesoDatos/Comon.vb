Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon
Imports Prosegur.Global.GesEfectivo
Imports ContractoServicio = Prosegur.Genesis.ContractoServicio
Imports ContractoComon = Prosegur.Genesis.ContractoServicio.Contractos.Comon


Public Class Comon

#Region "NumeroSerieBillete"

    ' Faz a busca no banco de dados de todos os numeros de série que possuem o idBulto da peticion. 
    Public Shared Function getNumeroDeSerieBillete(ByRef objPeticion As NumeroSerieBillete.GetNumeroDeSerieBillete.GetNumeroDeSerieBilletePeticion) _
                           As NumeroSerieBillete.GetNumeroDeSerieBillete.GetNumeroDeSerieBilleteRespuesta


        'Cria objRespuesta
        Dim objRespuesta As NumeroSerieBillete.GetNumeroDeSerieBillete.GetNumeroDeSerieBilleteRespuesta = _
            New NumeroSerieBillete.GetNumeroDeSerieBillete.GetNumeroDeSerieBilleteRespuesta()

        Try

            'Cria string de conexão
            Dim conexao As StringBuilder = DefineConexaoGetNumSerie(objPeticion.CodAplicacionGenesis)

            ' cria comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(conexao.ToString)

            'Chama o método para preparar o command para a execução.
            PreparaComandoGetNumSerie(comando, conexao.ToString, objPeticion)

            ' executa a consulta
            Dim dtNumSerie As DataTable = AcessoDados.ExecutarDataTable(conexao.ToString, comando)

            'Libera o espaço em memória
            comando.Dispose()

            ' caso encontre algum registro
            If dtNumSerie IsNot Nothing AndAlso dtNumSerie.Rows.Count > 0 Then
                objRespuesta = PreencheRespuestaGetNumSerie(dtNumSerie)
            End If

        Catch
            Throw
        End Try

        Return objRespuesta
    End Function

    'Método responsável de tratar a conexão de Numero de série
    Private Shared Function DefineConexaoGetNumSerie(CodAplicacionGenesis As Integer) As StringBuilder

        Dim strConexao As StringBuilder = New StringBuilder()

        'Trata de acordo com a aplicação do chamador.
        Select Case CodAplicacionGenesis

            'Caso a aplicação seja Conteo
            Case 1

                strConexao.Append(Constantes.CONEXAO_CONTEO)


        End Select

        Return strConexao

    End Function

    'Método responsável por configurar o Command para executar a consulta de numero de série.
    Private Shared Sub PreparaComandoGetNumSerie(ByRef comando As IDbCommand, conexao As String, _
                                                    objPeticion As NumeroSerieBillete.GetNumeroDeSerieBillete.GetNumeroDeSerieBilletePeticion)

        Dim valorParametro As String = Nothing
        Dim nomeParametro As String = Nothing

        ' monta a query
        Dim query As New StringBuilder

        If Not String.IsNullOrEmpty(objPeticion.IdBulto) Then
            nomeParametro = "NSER.OID_BULTO = BUL.OID_BULTO"
            query.Append(My.Resources.GetNumeroDeSerieBilletebyBulto)
            valorParametro = objPeticion.IdBulto

        Else
            nomeParametro = "NSER.OID_REMESA = REM.OID_REMESA"
            query.Append(My.Resources.GetNumeroDeSerieBillete)
            valorParametro = objPeticion.IdRemesa

        End If


        ' filtro para retornar somente os registros com o id do Bulto ou id da Remessa
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(conexao.ToString, "COD_PRECINTO", ProsegurDbType.Identificador_Alfanumerico, valorParametro))

        'Prepara o command para ser executado.
        comando.CommandText = Util.PrepararQuery(conexao.ToString, query.ToString)
        comando.CommandType = CommandType.Text

        comando.CommandText = String.Format(comando.CommandText, nomeParametro)

    End Sub

    'Preenche o objeto de respuesta do serviço.
    Private Shared Function PreencheRespuestaGetNumSerie(dtNumSerie As DataTable _
                                                        ) As NumeroSerieBillete.GetNumeroDeSerieBillete.GetNumeroDeSerieBilleteRespuesta

        'Cria a resposta 
        Dim objRespuesta = New NumeroSerieBillete.GetNumeroDeSerieBillete.GetNumeroDeSerieBilleteRespuesta()

        'Cria uma lista de denominaciones billetes para alocar as denominaciones
        Dim DenominacionesBilletes = New List(Of Clases.DenominacionBillete)

        'percorre os registros retornados do banco.
        For Each dtNumSerieItem In dtNumSerie.Rows

            Dim codDenominacion = dtNumSerieItem("COD_DENOMINACION")

            'Cria o objeto denominacionBillete
            Dim DenominacionBilleteRow As Clases.DenominacionBillete

            'Verifica se já existe essa denominacion na lista de denominaciones.
            If DenominacionesBilletes.Where(Function(x) x.Codigo = codDenominacion).Count = 0 Then

                'Cria uma nova denominacion
                DenominacionBilleteRow = New Clases.DenominacionBillete()

                'Acrescenta o codDenominacion retornado da base da dados.
                DenominacionBilleteRow.Codigo = codDenominacion

                'Adiciona na lista de denominaciones.
                DenominacionesBilletes.Add(DenominacionBilleteRow)

            Else

                'Seleciona a demoninacion na lista de denominaciones.
                DenominacionBilleteRow = DenominacionesBilletes.Where(Function(x) x.Codigo = codDenominacion).Single()

            End If

            'Cria o objeto de numero de serie.
            Dim numeroSerieRow As Clases.NumeroSerieBillete = New Clases.NumeroSerieBillete()

            'Acrescenta os valores do objeto
            numeroSerieRow.Identificador = dtNumSerieItem("OID_NUMERO_SERIE")
            numeroSerieRow.NombreArchivo = dtNumSerieItem("DES_NOMBRE_ARCHIVO")
            numeroSerieRow.NumeroSerie = dtNumSerieItem("COD_NUMERO_SERIE")

            'Faz a remoção dos registros daquela denominacion
            DenominacionesBilletes.Remove(DenominacionBilleteRow)

            'Verifica se já existe uma lista de números de série.
            If DenominacionBilleteRow.NumerosSerie Is Nothing Then

                'Se não existir, cria a lista.
                DenominacionBilleteRow.NumerosSerie = New List(Of Clases.NumeroSerieBillete)

            End If

            'Adiciona os números de série na lista.
            DenominacionBilleteRow.NumerosSerie.Add(numeroSerieRow)

            'Adiciona a demoniación na lista de denominaciones.
            DenominacionesBilletes.Add(DenominacionBilleteRow)

        Next

        'Adiciona a lista na respuesta.
        objRespuesta.DenominacionBilletes = DenominacionesBilletes

        Return objRespuesta

    End Function

    Public Shared Function setNumerodeSerieBillete(ByRef objpeticion As NumeroSerieBillete.SetNumeroDeSerieBillete.SetNumeroDeSerieBilletePeticion) _
                          As NumeroSerieBillete.SetNumeroDeSerieBillete.SetNumeroDeSerieBilleteRespuesta

        Dim objrespuesta As New NumeroSerieBillete.SetNumeroDeSerieBillete.SetNumeroDeSerieBilleteRespuesta

        Try


            Dim conexao = Nothing
            Dim valorparametroquery = Nothing
            Dim nomecampoparametro = Nothing


            nomecampoparametro = "TNUM.OID_REMESA"

            Select Case objpeticion.CodAplicacionGenesis

                Case 1
                    conexao = Constantes.CONEXAO_CONTEO

            End Select


            Dim transacao As DbHelper.Transacao = New DbHelper.Transacao(conexao)

            ExcluirNumeroDeSerieBillete(objpeticion, transacao, conexao)

            InserirNumeroDeSerieBillete(objpeticion, transacao, conexao)

            transacao.RealizarTransacao()


        Catch ex As Exception
            Throw
        End Try

        Return objrespuesta

    End Function

    Private Shared Sub ExcluirNumeroDeSerieBillete(ByRef objPeticion As NumeroSerieBillete.SetNumeroDeSerieBillete.SetNumeroDeSerieBilletePeticion, _
                                                   ByRef Transacao As DbHelper.Transacao,
                                                   Conexao As Object)

        Dim objRespuesta As New NumeroSerieBillete.SetNumeroDeSerieBillete.SetNumeroDeSerieBilleteRespuesta

        Try

            Dim valorParametroQuery = Nothing
            Dim nomeCampoParametro = Nothing

            nomeCampoParametro = "TNUM.OID_REMESA"

            ' cria comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Conexao)

            ' monta a query
            Dim query As New StringBuilder

            query.Append(My.Resources.SetNumeroDeSerieBilleteExcluir)

            If Not String.IsNullOrEmpty(objPeticion.idBulto) Then
                nomeCampoParametro = "TNUM.OID_BULTO"
                valorParametroQuery = objPeticion.idBulto

            Else
                valorParametroQuery = objPeticion.idRemesa

            End If

            ' filtro
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Conexao, "OID_REMESAORBULTO", ProsegurDbType.Objeto_Id, valorParametroQuery))

            comando.CommandText = Util.PrepararQuery(Conexao, query.ToString)
            comando.CommandType = CommandType.Text

            comando.CommandText = String.Format(comando.CommandText, nomeCampoParametro)


            Transacao.AdicionarItemTransacao(comando)

        Catch ex As Exception
            Throw
        End Try


    End Sub

    Private Shared Sub InserirNumeroDeSerieBillete(ByRef objPeticion As NumeroSerieBillete.SetNumeroDeSerieBillete.SetNumeroDeSerieBilletePeticion, _
                                                   ByRef Transacao As DbHelper.Transacao,
                                                   Conexao As Object)
        Try


            For Each itemDenominacionBillete In objPeticion.DenominacionBilletes


                For Each itemNumerosSerie In itemDenominacionBillete.NumerosSerie


                    ' criar comando
                    Dim cmd As IDbCommand = AcessoDados.CriarComando(Conexao)

                    ' monta a query
                    Dim query As New StringBuilder

                    query.Append(My.Resources.SetNumeroDeSerieBilleteInserir)

                    cmd.CommandText = Util.PrepararQuery(Conexao, query.ToString)
                    cmd.CommandType = CommandType.Text

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Conexao, "OID_NUMERO_SERIE", ProsegurDbType.Objeto_Id, System.Guid.NewGuid.ToString()))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Conexao, "COD_NUMERO_SERIE", ProsegurDbType.Descricao_Longa, itemNumerosSerie.NumeroSerie))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Conexao, "OID_DENOMINACION", ProsegurDbType.Objeto_Id, RecuperaOidDenominacionbyCodDenominacion(itemDenominacionBillete.Codigo, objPeticion.CodAplicacionGenesis)))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Conexao, "DES_NOMBRE_ARCHIVO", ProsegurDbType.Descricao_Longa, itemNumerosSerie.NombreArchivo))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Conexao, "OID_REMESA", ProsegurDbType.Objeto_Id, objPeticion.idRemesa))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Conexao, "OID_BULTO", ProsegurDbType.Objeto_Id, objPeticion.idBulto))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Conexao, "COD_USUARIO", ProsegurDbType.Descricao_Longa, itemDenominacionBillete.CodigoUsuario))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Conexao, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))

                    Transacao.AdicionarItemTransacao(cmd)

                Next


            Next

        Catch ex As Exception
            Throw
        End Try


    End Sub

    Private Shared Function RecuperaOidDenominacionbyCodDenominacion(codDenominacion As String, codAplicacion As Integer) As String

        Try

            Dim Conexao = Nothing

            If codAplicacion = 1 Then
                Conexao = Constantes.CONEXAO_CONTEO

            End If

            ' cria comando Inserção
            Dim comando As IDbCommand = AcessoDados.CriarComando(Conexao)

            ' monta a query
            Dim query As New StringBuilder

            query.Append(My.Resources.SelecionaOidDenominacionByCod)

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Conexao, "COD_DENOMINACION", ProsegurDbType.Descricao_Longa, codDenominacion))


            comando.CommandText = Util.PrepararQuery(Conexao, query.ToString)
            comando.CommandType = CommandType.Text

            ' executa a consulta
            Dim dtDenominacion As DataTable = AcessoDados.ExecutarDataTable(Conexao, comando)

            ' caso encontre algum registro
            If dtDenominacion IsNot Nothing AndAlso dtDenominacion.Rows.Count > 0 Then

                For Each itemRow In dtDenominacion.Rows

                    Return itemRow("OID_DENOMINACION").ToString

                Next


            End If

        Catch ex As Exception
            Throw
        End Try

        Return Nothing
    End Function

    Private Shared Function MontarConsultaTotalizadoresSaldosPorIdentificadores(ByRef cmd As IDbCommand,
                                                                                identificadorClienteSaldo As String,
                                                                                identificadorSubClienteSaldo As String,
                                                                                identificadorPuntoServicioSaldo As String,
                                                                                identificadorClienteMovimiento As String,
                                                                                identificadorSubClienteMovimiento As String,
                                                                                identificadorPuntoServicioMovimiento As String) As StringBuilder

        Dim sql As New StringBuilder

        'Cliente Saldo
        If Not String.IsNullOrWhiteSpace(identificadorClienteSaldo) Then
            If Not String.IsNullOrWhiteSpace(identificadorClienteSaldo) Then
                sql.AppendLine(" AND NIVEL_SALDO.OID_CLIENTE = []OID_CLIENTE_SALDO ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE_SALDO", ProsegurDbType.Identificador_Alfanumerico, identificadorClienteSaldo))
            End If

            'Sub Cliente Saldo
            If Not String.IsNullOrWhiteSpace(identificadorSubClienteSaldo) Then
                sql.AppendLine(" AND NIVEL_SALDO.OID_SUBCLIENTE = []OID_SUBCLIENTE_SALDO ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE_SALDO", ProsegurDbType.Identificador_Alfanumerico, identificadorSubClienteSaldo))
            Else
                sql.AppendLine(" AND NIVEL_SALDO.OID_SUBCLIENTE IS NULL ")
            End If

            'Punto Servicio Saldo
            If Not String.IsNullOrWhiteSpace(identificadorPuntoServicioSaldo) Then
                sql.AppendLine(" AND NIVEL_SALDO.OID_PTO_SERVICIO = []OID_PTO_SERVICIO_SALDO ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PTO_SERVICIO_SALDO", ProsegurDbType.Identificador_Alfanumerico, identificadorPuntoServicioSaldo))
            Else
                sql.AppendLine(" AND NIVEL_SALDO.OID_PTO_SERVICIO IS NULL ")
            End If
        End If

        'Cliente Movimienteo
        If Not String.IsNullOrWhiteSpace(identificadorClienteMovimiento) Then
            If Not String.IsNullOrWhiteSpace(identificadorClienteMovimiento) Then
                sql.AppendLine(" AND NIVEL_MOV.OID_CLIENTE = []OID_CLIENTE_MOV ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE_MOV", ProsegurDbType.Identificador_Alfanumerico, identificadorClienteMovimiento))
            End If

            'Sub Cliente Saldo
            If Not String.IsNullOrWhiteSpace(identificadorSubClienteMovimiento) Then
                sql.AppendLine(" AND NIVEL_MOV.OID_SUBCLIENTE = []OID_SUBCLIENTE_MOV ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE_MOV", ProsegurDbType.Identificador_Alfanumerico, identificadorSubClienteMovimiento))
            Else
                sql.AppendLine(" AND NIVEL_MOV.OID_SUBCLIENTE IS NULL ")
            End If

            'Punto Servicio Saldo
            If Not String.IsNullOrWhiteSpace(identificadorPuntoServicioMovimiento) Then
                sql.AppendLine(" AND NIVEL_MOV.OID_PTO_SERVICIO = []OID_PTO_SERVICIO_MOV ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PTO_SERVICIO_MOV", ProsegurDbType.Identificador_Alfanumerico, identificadorPuntoServicioMovimiento))
            Else
                sql.AppendLine(" AND NIVEL_MOV.OID_PTO_SERVICIO IS NULL ")
            End If
        End If

        Return sql

    End Function

    Private Shared Function MontarConsultaTotalizadoresSaldosPorCodigos(ByRef cmd As IDbCommand,
                                                                        codigoClienteSaldo As String,
                                                                        codigoSubClienteSaldo As String,
                                                                        codigoPuntoServicioSaldo As String,
                                                                        codigoClienteMovimiento As String,
                                                                        codigoSubClienteMovimiento As String,
                                                                        codigoPuntoServicioMovimiento As String) As StringBuilder

        Dim sql As New StringBuilder

        'Cliente Saldo
        If Not String.IsNullOrWhiteSpace(codigoClienteSaldo) Then
            If Not String.IsNullOrWhiteSpace(codigoClienteSaldo) Then
                sql.AppendLine(" AND CLI_SALDO.COD_CLIENTE = []COD_CLIENTE_SALDO ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE_SALDO", ProsegurDbType.Identificador_Alfanumerico, codigoClienteSaldo))
            End If

            'Sub Cliente Saldo
            If Not String.IsNullOrWhiteSpace(codigoSubClienteSaldo) Then
                sql.AppendLine(" AND SBCLI_SALDO.COD_SUBCLIENTE = []COD_SUBCLIENTE_SALDO ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE_SALDO", ProsegurDbType.Identificador_Alfanumerico, codigoSubClienteSaldo))
            Else
                sql.AppendLine(" AND NIVEL_SALDO.OID_SUBCLIENTE IS NULL ")
            End If

            'Punto Servicio Saldo
            If Not String.IsNullOrWhiteSpace(codigoPuntoServicioSaldo) Then
                sql.AppendLine(" AND PTS_SALDO.COD_PTO_SERVICIO = []COD_PTO_SERVICIO_SALDO ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO_SALDO", ProsegurDbType.Identificador_Alfanumerico, codigoPuntoServicioSaldo))
            Else
                sql.AppendLine(" AND NIVEL_SALDO.OID_PTO_SERVICIO IS NULL ")
            End If
        End If

        'Cliente Movimienteo
        If Not String.IsNullOrWhiteSpace(codigoClienteMovimiento) Then
            If Not String.IsNullOrWhiteSpace(codigoClienteMovimiento) Then
                sql.AppendLine(" AND CLI_MOV.COD_CLIENTE = []COD_CLIENTE_MOV ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE_MOV", ProsegurDbType.Identificador_Alfanumerico, codigoClienteMovimiento))
            End If

            'Sub Cliente Saldo
            If Not String.IsNullOrWhiteSpace(codigoSubClienteMovimiento) Then
                sql.AppendLine(" AND SBCLI_MOV.COD_SUBCLIENTE = []COD_SUBCLIENTE_MOV ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE_MOV", ProsegurDbType.Identificador_Alfanumerico, codigoSubClienteMovimiento))
            Else
                sql.AppendLine(" AND NIVEL_MOV.OID_SUBCLIENTE IS NULL ")
            End If

            'Punto Servicio Saldo
            If Not String.IsNullOrWhiteSpace(codigoPuntoServicioMovimiento) Then
                sql.AppendLine(" AND PTS_MOV.COD_PTO_SERVICIO = []COD_PTO_SERVICIO_MOV ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO_MOV", ProsegurDbType.Identificador_Alfanumerico, codigoPuntoServicioMovimiento))
            Else
                sql.AppendLine(" AND NIVEL_MOV.OID_PTO_SERVICIO IS NULL ")
            End If
        End If

        Return sql

    End Function

    Public Shared Function RecuperarTotalizadoresSaldosPorCodigo(codigoClienteSaldo As String,
                                                                codigoSubClienteSaldo As String,
                                                                codigoPuntoServicioSaldo As String,
                                                                codigoClienteMovimiento As String,
                                                                codigoSubClienteMovimiento As String,
                                                                codigoPuntoServicioMovimiento As String,
                                                                codigoSubCanal As String, Optional buscarTodosNiveles As Boolean = True) As List(Of Clases.TotalizadorSaldo)
        Dim TotalizadoresSaldos As List(Of Clases.TotalizadorSaldo) = Nothing

        If buscarTodosNiveles Then
            TotalizadoresSaldos = New List(Of Clases.TotalizadorSaldo)
            For Each totalizador In BuscarTotalizadoresSaldoPorCodigo(codigoClienteSaldo,
                                              codigoSubClienteSaldo,
                                              codigoPuntoServicioSaldo,
                                              codigoClienteMovimiento,
                                              String.Empty,
                                              String.Empty,
                                              codigoSubCanal)
                If (Not TotalizadoresSaldos.Any(Function(t) t.Equals(totalizador))) Then
                    TotalizadoresSaldos.Add(totalizador)
                End If
            Next

            If Not String.IsNullOrEmpty(codigoSubClienteMovimiento) Then

                For Each totalizador In BuscarTotalizadoresSaldoPorCodigo(codigoClienteSaldo,
                                              codigoSubClienteSaldo,
                                              codigoPuntoServicioSaldo,
                                              codigoClienteMovimiento,
                                              codigoSubClienteMovimiento,
                                              String.Empty,
                                              codigoSubCanal)
                    If (Not TotalizadoresSaldos.Any(Function(t) t.Equals(totalizador))) Then
                        TotalizadoresSaldos.Add(totalizador)
                    End If
                Next

            End If

            If Not String.IsNullOrEmpty(codigoPuntoServicioMovimiento) Then

                For Each totalizador In BuscarTotalizadoresSaldoPorCodigo(codigoClienteSaldo,
                                              codigoSubClienteSaldo,
                                              codigoPuntoServicioSaldo,
                                              codigoClienteMovimiento,
                                              codigoSubClienteMovimiento,
                                              codigoPuntoServicioMovimiento,
                                              codigoSubCanal)
                    If (Not TotalizadoresSaldos.Any(Function(t) t.Equals(totalizador))) Then
                        TotalizadoresSaldos.Add(totalizador)
                    End If
                Next

            End If
        Else
            TotalizadoresSaldos.AddRange(BuscarTotalizadoresSaldoPorCodigo(codigoClienteSaldo,
                                              codigoSubClienteSaldo,
                                              codigoPuntoServicioSaldo,
                                              codigoClienteMovimiento,
                                              codigoSubClienteMovimiento,
                                              codigoPuntoServicioMovimiento,
                                              codigoSubCanal))
        End If

        Return TotalizadoresSaldos

    End Function

    Private Shared Function BuscarTotalizadoresSaldoPorCodigo(codigoClienteSaldo As String,
                                                                codigoSubClienteSaldo As String,
                                                                codigoPuntoServicioSaldo As String,
                                                                codigoClienteMovimiento As String,
                                                                codigoSubClienteMovimiento As String,
                                                                codigoPuntoServicioMovimiento As String,
                                                                codigoSubCanal As String) As List(Of Clases.TotalizadorSaldo)
        Dim TotalizadoresSaldos As List(Of Clases.TotalizadorSaldo) = New List(Of Clases.TotalizadorSaldo)
        Try

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandType = CommandType.Text

            Dim sql As Text.StringBuilder = Nothing

            sql = MontarConsultaTotalizadoresSaldosPorCodigos(cmd,
                                                            codigoClienteSaldo, codigoSubClienteSaldo, codigoPuntoServicioSaldo,
                                                            codigoClienteMovimiento, codigoSubClienteMovimiento, codigoPuntoServicioMovimiento)

            'pelo menos um filtro deve ser informado
            If sql.Length > 0 Then
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ComonRecuperarTotalizadoresSaldo & sql.ToString)
                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    TotalizadoresSaldos = New List(Of Clases.TotalizadorSaldo)

                    'Recupera o subcanal
                    Dim subCanales = Genesis.SubCanal.ObtenerSubCanales()

                    For Each dr In dt.Select("OID_SUBCANAL IS NOT NULL")
                        Dim totalizador As New Clases.TotalizadorSaldo
                        With totalizador
                            .IdentificadorNivelMovimiento = Util.AtribuirValorObj(dr("OID_CONFIG_NIVEL_MOVIMIENTO"), GetType(String))
                            .IdentificadorNivelSaldo = Util.AtribuirValorObj(dr("OID_CONFIG_NIVEL_SALDO"), GetType(String))
                            .bolDefecto = Util.AtribuirValorObj(dr("BOL_DEFECTO"), GetType(Boolean))
                            .Cliente = New Clases.Cliente
                            .Cliente.Identificador = Util.AtribuirValorObj(dr("OID_CLIENTE"), GetType(String))
                            .Cliente.Codigo = Util.AtribuirValorObj(dr("COD_CLIENTE"), GetType(String))
                            .Cliente.Descripcion = Util.AtribuirValorObj(dr("DES_CLIENTE"), GetType(String))
                            .SubCanales = New List(Of Clases.SubCanal)
                            Dim subCanal = subCanales.Where(Function(sc) sc.Identificador = dr("OID_SUBCANAL")).FirstOrDefault
                            If subCanal IsNot Nothing Then
                                .SubCanales.Add(subCanal)
                            End If

                            If dr("OID_SUBCLIENTE") IsNot DBNull.Value Then
                                .SubCliente = New Clases.SubCliente
                                .SubCliente.Identificador = Util.AtribuirValorObj(dr("OID_SUBCLIENTE"), GetType(String))
                                .SubCliente.Codigo = Util.AtribuirValorObj(dr("COD_SUBCLIENTE"), GetType(String))
                                .SubCliente.Descripcion = Util.AtribuirValorObj(dr("DES_SUBCLIENTE"), GetType(String))

                                If dr("OID_PTO_SERVICIO") IsNot DBNull.Value Then
                                    .PuntoServicio = New Clases.PuntoServicio
                                    .PuntoServicio.Identificador = Util.AtribuirValorObj(dr("OID_PTO_SERVICIO"), GetType(String))
                                    .PuntoServicio.Codigo = Util.AtribuirValorObj(dr("COD_PTO_SERVICIO"), GetType(String))
                                    .PuntoServicio.Descripcion = Util.AtribuirValorObj(dr("DES_PTO_SERVICIO"), GetType(String))
                                End If
                            End If
                        End With

                        totalizador.NivelSaldo = Enumeradores.NivelSaldo.Cliente
                        If (Not String.IsNullOrEmpty(codigoSubClienteMovimiento)) Then
                            totalizador.NivelSaldo = Enumeradores.NivelSaldo.SubCliente
                            If (Not String.IsNullOrEmpty(codigoPuntoServicioMovimiento)) Then totalizador.NivelSaldo = Enumeradores.NivelSaldo.PuntoServicio
                        End If

                        TotalizadoresSaldos.Add(totalizador)
                    Next

                    'Totalizadores que não possui um subcanal vinculado.
                    For Each dr In dt.Select("OID_SUBCANAL IS NULL")
                        Dim totalizador As New Clases.TotalizadorSaldo
                        Dim filtro As New StringBuilder

                        With totalizador
                            .IdentificadorNivelMovimiento = Util.AtribuirValorObj(dr("OID_CONFIG_NIVEL_MOVIMIENTO"), GetType(String))
                            .IdentificadorNivelSaldo = Util.AtribuirValorObj(dr("OID_CONFIG_NIVEL_SALDO"), GetType(String))
                            .bolDefecto = Util.AtribuirValorObj(dr("BOL_DEFECTO"), GetType(Boolean))
                            .Cliente = New Clases.Cliente
                            .Cliente.Identificador = Util.AtribuirValorObj(dr("OID_CLIENTE"), GetType(String))
                            .Cliente.Codigo = Util.AtribuirValorObj(dr("COD_CLIENTE"), GetType(String))
                            .Cliente.Descripcion = Util.AtribuirValorObj(dr("DES_CLIENTE"), GetType(String))

                            filtro.AppendFormat("OID_SUBCANAL IS NOT NULL", .Cliente.Identificador)

                            If dr("OID_SUBCLIENTE") IsNot DBNull.Value Then
                                .SubCliente = New Clases.SubCliente
                                .SubCliente.Identificador = Util.AtribuirValorObj(dr("OID_SUBCLIENTE"), GetType(String))
                                .SubCliente.Codigo = Util.AtribuirValorObj(dr("COD_SUBCLIENTE"), GetType(String))
                                .SubCliente.Descripcion = Util.AtribuirValorObj(dr("DES_SUBCLIENTE"), GetType(String))

                                If dr("OID_PTO_SERVICIO") IsNot DBNull.Value Then
                                    .PuntoServicio = New Clases.PuntoServicio
                                    .PuntoServicio.Identificador = Util.AtribuirValorObj(dr("OID_PTO_SERVICIO"), GetType(String))
                                    .PuntoServicio.Codigo = Util.AtribuirValorObj(dr("COD_PTO_SERVICIO"), GetType(String))
                                    .PuntoServicio.Descripcion = Util.AtribuirValorObj(dr("DES_PTO_SERVICIO"), GetType(String))

                                End If
                            End If

                            Dim arrCanales As New List(Of String)
                            For Each drCanal In dt.Select(filtro.ToString)
                                arrCanales.Add(drCanal("OID_SUBCANAL"))
                            Next

                            If arrCanales.Count > 0 Then
                                'Todos os subcanais, exceto os subcanais que estão viculados á mesma combinação cliente,subcliente e puntosevicio.
                                .SubCanales = subCanales.Where(Function(sc) Not arrCanales.Contains(sc.Identificador)).ToList()
                            Else
                                .SubCanales = subCanales
                            End If

                            .SubCanales = .SubCanales.OrderBy(Function(a) a.Descripcion).ToList()
                        End With

                        totalizador.NivelSaldo = Enumeradores.NivelSaldo.Cliente
                        If (Not String.IsNullOrEmpty(codigoSubClienteMovimiento)) Then
                            totalizador.NivelSaldo = Enumeradores.NivelSaldo.SubCliente
                            If (Not String.IsNullOrEmpty(codigoPuntoServicioMovimiento)) Then totalizador.NivelSaldo = Enumeradores.NivelSaldo.PuntoServicio
                        End If

                        TotalizadoresSaldos.Add(totalizador)
                    Next

                    If Not String.IsNullOrEmpty(codigoSubCanal) Then
                        TotalizadoresSaldos = TotalizadoresSaldos.Where(Function(a) a.SubCanales.Exists(Function(b) b.Codigo = codigoSubCanal)).ToList()
                    End If

                End If
            End If

        Catch ex As Excepcion.NegocioExcepcion
            Throw
        Catch ex As Exception
            Throw
        End Try

        Return TotalizadoresSaldos
    End Function

    Public Shared Function RecuperarTotalizadoresSaldos(identificadorClienteSaldo As String,
                                                        identificadorSubClienteSaldo As String,
                                                        identificadorPuntoServicioSaldo As String,
                                                        identificadorClienteMovimiento As String,
                                                        identificadorSubClienteMovimiento As String,
                                                        identificadorPuntoServicioMovimiento As String,
                                                        identificadorSubCanal As String) As List(Of Clases.TotalizadorSaldo)

        Dim TotalizadoresSaldos As List(Of Clases.TotalizadorSaldo) = Nothing

        Try

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandType = CommandType.Text

            Dim sql As Text.StringBuilder = Nothing

            sql = MontarConsultaTotalizadoresSaldosPorIdentificadores(cmd,
                                                                      identificadorClienteSaldo, identificadorSubClienteSaldo, identificadorPuntoServicioSaldo,
                                                                      identificadorClienteMovimiento, identificadorSubClienteMovimiento, identificadorPuntoServicioMovimiento)

            'pelo menos um filtro deve ser informado
            If sql.Length > 0 Then
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ComonRecuperarTotalizadoresSaldo & sql.ToString)
                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    TotalizadoresSaldos = New List(Of Clases.TotalizadorSaldo)

                    'Recupera o subcanal
                    Dim subCanales = Genesis.SubCanal.ObtenerSubCanales()

                    For Each dr In dt.Select("OID_SUBCANAL IS NOT NULL")
                        Dim totalizador As New Clases.TotalizadorSaldo
                        With totalizador
                            .IdentificadorNivelMovimiento = Util.AtribuirValorObj(dr("OID_CONFIG_NIVEL_MOVIMIENTO"), GetType(String))
                            .IdentificadorNivelSaldo = Util.AtribuirValorObj(dr("OID_CONFIG_NIVEL_SALDO"), GetType(String))
                            .bolDefecto = Util.AtribuirValorObj(dr("BOL_DEFECTO"), GetType(Boolean))
                            .Cliente = New Clases.Cliente
                            .Cliente.Identificador = Util.AtribuirValorObj(dr("OID_CLIENTE"), GetType(String))
                            .Cliente.Codigo = Util.AtribuirValorObj(dr("COD_CLIENTE"), GetType(String))
                            .Cliente.Descripcion = Util.AtribuirValorObj(dr("DES_CLIENTE"), GetType(String))
                            .SubCanales = New List(Of Clases.SubCanal)
                            Dim subCanal = subCanales.Where(Function(sc) sc.Identificador = dr("OID_SUBCANAL")).FirstOrDefault
                            If subCanal IsNot Nothing Then
                                .SubCanales.Add(subCanal)
                            End If

                            If dr("OID_SUBCLIENTE") IsNot DBNull.Value Then
                                .SubCliente = New Clases.SubCliente
                                .SubCliente.Identificador = Util.AtribuirValorObj(dr("OID_SUBCLIENTE"), GetType(String))
                                .SubCliente.Codigo = Util.AtribuirValorObj(dr("COD_SUBCLIENTE"), GetType(String))
                                .SubCliente.Descripcion = Util.AtribuirValorObj(dr("DES_SUBCLIENTE"), GetType(String))

                                If dr("OID_PTO_SERVICIO") IsNot DBNull.Value Then
                                    .PuntoServicio = New Clases.PuntoServicio
                                    .PuntoServicio.Identificador = Util.AtribuirValorObj(dr("OID_PTO_SERVICIO"), GetType(String))
                                    .PuntoServicio.Codigo = Util.AtribuirValorObj(dr("COD_PTO_SERVICIO"), GetType(String))
                                    .PuntoServicio.Descripcion = Util.AtribuirValorObj(dr("DES_PTO_SERVICIO"), GetType(String))
                                End If
                            End If
                        End With

                        totalizador.NivelSaldo = Enumeradores.NivelSaldo.Cliente
                        If (Not String.IsNullOrEmpty(identificadorSubClienteMovimiento)) Then
                            totalizador.NivelSaldo = Enumeradores.NivelSaldo.SubCliente
                            If (Not String.IsNullOrEmpty(identificadorPuntoServicioMovimiento)) Then totalizador.NivelSaldo = Enumeradores.NivelSaldo.PuntoServicio
                        End If

                        TotalizadoresSaldos.Add(totalizador)
                    Next

                    'Totalizadores que não possui um subcanal vinculado.
                    For Each dr In dt.Select("OID_SUBCANAL IS NULL")
                        Dim totalizador As New Clases.TotalizadorSaldo
                        Dim filtro As New StringBuilder

                        With totalizador
                            .IdentificadorNivelMovimiento = Util.AtribuirValorObj(dr("OID_CONFIG_NIVEL_MOVIMIENTO"), GetType(String))
                            .IdentificadorNivelSaldo = Util.AtribuirValorObj(dr("OID_CONFIG_NIVEL_SALDO"), GetType(String))
                            .bolDefecto = Util.AtribuirValorObj(dr("BOL_DEFECTO"), GetType(Boolean))
                            .Cliente = New Clases.Cliente
                            .Cliente.Identificador = Util.AtribuirValorObj(dr("OID_CLIENTE"), GetType(String))
                            .Cliente.Codigo = Util.AtribuirValorObj(dr("COD_CLIENTE"), GetType(String))
                            .Cliente.Descripcion = Util.AtribuirValorObj(dr("DES_CLIENTE"), GetType(String))

                            filtro.AppendFormat("OID_SUBCANAL IS NOT NULL", .Cliente.Identificador)

                            If dr("OID_SUBCLIENTE") IsNot DBNull.Value Then
                                .SubCliente = New Clases.SubCliente
                                .SubCliente.Identificador = Util.AtribuirValorObj(dr("OID_SUBCLIENTE"), GetType(String))
                                .SubCliente.Codigo = Util.AtribuirValorObj(dr("COD_SUBCLIENTE"), GetType(String))
                                .SubCliente.Descripcion = Util.AtribuirValorObj(dr("DES_SUBCLIENTE"), GetType(String))

                                If dr("OID_PTO_SERVICIO") IsNot DBNull.Value Then
                                    .PuntoServicio = New Clases.PuntoServicio
                                    .PuntoServicio.Identificador = Util.AtribuirValorObj(dr("OID_PTO_SERVICIO"), GetType(String))
                                    .PuntoServicio.Codigo = Util.AtribuirValorObj(dr("COD_PTO_SERVICIO"), GetType(String))
                                    .PuntoServicio.Descripcion = Util.AtribuirValorObj(dr("DES_PTO_SERVICIO"), GetType(String))

                                End If
                            End If

                            Dim arrCanales As New List(Of String)
                            For Each drCanal In dt.Select(filtro.ToString)
                                arrCanales.Add(drCanal("OID_SUBCANAL"))
                            Next

                            If arrCanales.Count > 0 Then
                                'Todos os subcanais, exceto os subcanais que estão viculados á mesma combinação cliente,subcliente e puntosevicio.
                                .SubCanales = subCanales.Where(Function(sc) Not arrCanales.Contains(sc.Identificador)).ToList()
                            Else
                                .SubCanales = subCanales
                            End If

                            .SubCanales = .SubCanales.OrderBy(Function(a) a.Descripcion).ToList()
                        End With

                        totalizador.NivelSaldo = Enumeradores.NivelSaldo.Cliente
                        If (Not String.IsNullOrEmpty(identificadorSubClienteMovimiento)) Then
                            totalizador.NivelSaldo = Enumeradores.NivelSaldo.SubCliente
                            If (Not String.IsNullOrEmpty(identificadorPuntoServicioMovimiento)) Then totalizador.NivelSaldo = Enumeradores.NivelSaldo.PuntoServicio
                        End If

                        TotalizadoresSaldos.Add(totalizador)
                    Next

                    If Not String.IsNullOrEmpty(identificadorSubCanal) Then
                        Dim idSubCanal As String = identificadorSubCanal
                        TotalizadoresSaldos = TotalizadoresSaldos.Where(Function(a) a.SubCanales.Exists(Function(b) b.Identificador = idSubCanal OrElse b.Codigo = idSubCanal)).ToList()
                    End If

                End If
            End If

        Catch ex As Excepcion.NegocioExcepcion
            Throw
        Catch ex As Exception
            Throw
        End Try

        Return TotalizadoresSaldos

    End Function

    Private Shared Function List(Of T)() As List(Of T)
        Throw New NotImplementedException
    End Function

#End Region

End Class
