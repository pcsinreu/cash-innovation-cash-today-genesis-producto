Imports Prosegur.DbHelper
Imports System.Collections

Public Class ReciboF22Respaldo

#Region "[ATRIBUTOS]"

    Private Const CONST_COD_SUCURSAL As String = "CODSUCURSAL"
    Private Const CONST_DESC_SUCURSAL As String = "DESCSUCURSAL"
    Private Const CONST_LEGAJO As String = "LEGAJO"
    Private Const CONST_NUMEROSOBRE As String = "NUMEROSOBRE"

    ' chaves para código dos Medigos Pagos padrões para o relatório
    Private Const CONST_COD_CHEQUE_PESO As String = "CHEQU"
    Private Const CONST_COD_CHEQUE As String = "CHE"
    Private Const CONST_COD_TICKET As String = "TIC"
    Private Const CONST_COD_OTRO_VALOR As String = "OTR"

    ' constantes para o código do Tipo medio pago padrão
    Private Const CONST_COD_MEDIO_PAGO_CHEQUE = "codtipob"
    Private Const CONST_COD_MEDIO_PAGO_TICKET = "codtipoa"
    Private Const CONST_COD_MEDIO_PAGO_OUTROS = "codtipo"

#End Region

#Region "[LISTAR]"

    ''' <summary>
    ''' Método responsável por listar los Recibos F22 parciales
    ''' </summary>
    ''' <param name="objPeticion">Objeto com os filtros da pesquisa</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 23/03/2011 Criado    
    ''' </history>
    Public Shared Function ListarReciboF22Respaldo(objPeticion As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.Peticion) As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22RespaldoColeccion

        ' Declara variável de retorno
        Dim objReciboF22Col As New ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22RespaldoColeccion

        Dim conexao As IDbConnection = AcessoDados.Conectar(Constantes.CONEXAO_GE)

        ' criar comando
        Dim comando As IDbCommand = conexao.CreateCommand()

        ' Limpa o objeto da memória quando termina de usá-lo
        Using comando

            ' obter procedure
            comando.CommandText = Constantes.PKG_RECIBO_F22_RESPALDO_TXT
            comando.CommandType = CommandType.StoredProcedure

            ' setar parametros            
            comando.Parameters.Add(Util.CriarParametroOracle("cv_InfRemesa", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
            comando.Parameters.Add(Util.CriarParametroOracle("cv_Declarados", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
            comando.Parameters.Add(Util.CriarParametroOracle("cv_InfTermino", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
            comando.Parameters.Add(Util.CriarParametroOracle("cv_Efectivos", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
            comando.Parameters.Add(Util.CriarParametroOracle("cv_MediosPago", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
            comando.Parameters.Add(Util.CriarParametroOracle("cv_Observaciones", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "P_COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoDelegacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "P_COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "P_FECHA_INI", ProsegurDbType.Data_Hora, IIf(objPeticion.FechaDesde = Date.MinValue, DBNull.Value, objPeticion.FechaDesde)))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "P_FECHA_FIN", ProsegurDbType.Data_Hora, IIf(objPeticion.FechaHasta = Date.MaxValue, DBNull.Value, objPeticion.FechaHasta)))

            ' executar consulta            
            Dim drReciboF22 As IDataReader = comando.ExecuteReader()

            ' Limpa o objeto da memória quando termina de usá-lo
            Using drReciboF22

                Try
                    ' carrega a coleção de objetos com as remesas processadas
                    CarregarDadosRemesa(drReciboF22, objReciboF22Col)

                    ' Vai para o próximo cursor Términos
                    drReciboF22.NextResult()

                    ' carrega a coleção de objetos com os valores de declarados
                    CarregarDadosDeclarados(drReciboF22, objReciboF22Col)

                    ' Vai para o próximo cursor 
                    drReciboF22.NextResult()

                    ' carrega a coleção de objetos com os Valores Términos Sucursal
                    CarregarDadosTerminos(drReciboF22, objReciboF22Col)

                    ' Vai para o próximo cursor 
                    drReciboF22.NextResult()

                    ' carrega a coleção de objetos com os valores de efectivos
                    CarregarDadosEfectivos(drReciboF22, objReciboF22Col)

                    ' Vai para o próximo cursor 
                    drReciboF22.NextResult()

                    ' carrega a coleção de objetos com os valores de medio Pago
                    CarregarDadosMediosPago(drReciboF22, objReciboF22Col)

                    ' atualiza a soma de declarado e contados dos parciais para o nível de remesa
                    AtualizarSomaDeclaradosContados(objReciboF22Col)

                    ' atualiza a soma de declarado e contados dos parciais para o nível de remesa
                    AtualizarQuantidadeRespaldoRemesas(objReciboF22Col)

                Finally

                    ' Fecha a conexão do Data Reader
                    If drReciboF22 IsNot Nothing Then
                        drReciboF22.Close()
                    End If

                    ' Fecha a conexão do banco
                    AcessoDados.Desconectar(conexao)
                End Try

            End Using

        End Using

        Return objReciboF22Col

    End Function

    Private Shared Sub AtualizarQuantidadeRespaldoRemesas(ByRef objReciboF22Col As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22RespaldoColeccion)

        Dim objReciboF22 = (From ColecRel In objReciboF22Col _
                            Where ColecRel.TipoRegistro = "00")

        ' verifica todos o s registros a nível de remesa que existem no relatório
        If objReciboF22.Count > 0 Then

            'Para cada remesa do relatório, buscar a quantidade de parciais 
            For Each objRecibo As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22Respaldo In objReciboF22
                Dim objReciboLocal = objRecibo

                If objRecibo.ColMedioPagoDeclarados IsNot Nothing Then

                    Dim QtdParciais = (From ColecRel In objReciboF22Col _
                                       Where ColecRel.OidRemesa = objReciboLocal.OidRemesa _
                                         AndAlso ColecRel.TipoRegistro = "01" _
                                         AndAlso ColecRel.ColMedioPagoDeclarados IsNot Nothing _
                                       Select ColecRel.OidParcial).Distinct()

                    Dim objMedioPagoRespaldo = (From colMedioPago In objRecibo.ColMedioPagoDeclarados _
                                                Where colMedioPago.CodigoMedioPago = "RES" _
                                                AndAlso colMedioPago.DescripcionMedioPago = "RESPALDO")

                    If objMedioPagoRespaldo.Count > 0 Then

                        objMedioPagoRespaldo(0).ValorDeclaradoF22 = QtdParciais.Count()

                    Else

                        Dim objRespaldo As New ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado
                        objRespaldo.CodigoMedioPago = "RES"
                        objRespaldo.DescripcionMedioPago = "RESPALDO"
                        objRespaldo.ValorDeclaradoF22 = BuscaQuantidadeParciaisRemesa(objRecibo.OidRemesa)
                        objRespaldo.ValorDeclaradoSobres = QtdParciais.Count()
                        objRespaldo.ValorRecontadoSobres = QtdParciais.Count()
                        objRespaldo.CodigoDivisa = "ZZZZ"
                        objRecibo.ColMedioPagoDeclarados.Add(objRespaldo)

                    End If

                End If

            Next

        End If

    End Sub

    Private Shared Sub AtualizarSomaDeclaradosContados(ByRef objReciboF22Col As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22RespaldoColeccion)

        For Each objReciboF22 As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22Respaldo In objReciboF22Col
            Dim objReciboF22Local = objReciboF22

            ' valida se é um registro de remesa
            If objReciboF22.TipoRegistro = "00" Then

                ' se os declarados estão preenchidos para a remesa
                If objReciboF22.ColMedioPagoDeclarados IsNot Nothing AndAlso _
                   objReciboF22.ColMedioPagoDeclarados.Count > 0 Then

                    For Each objMedioPagEfec As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado In objReciboF22.ColMedioPagoDeclarados

                        ' captura a lista de parciais filhas
                        Dim listaParciais = (From objResult In objReciboF22Col _
                                             Where objResult.TipoRegistro = "01" _
                                             AndAlso objResult.OidRemesa = objReciboF22Local.OidRemesa)

                        If listaParciais.Count > 0 Then

                            For Each objParcial As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22Respaldo In listaParciais

                                If objParcial.ColMedioPagoDeclarados IsNot Nothing Then

                                    For Each objMedioPagEfecParcial As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado In objParcial.ColMedioPagoDeclarados

                                        If objMedioPagEfecParcial.CodigoMedioPago = objMedioPagEfec.CodigoMedioPago Then
                                            ' Soma os valores de declarado e contados de todas as parciais 
                                            objMedioPagEfec.ValorDeclaradoSobres += objMedioPagEfecParcial.ValorDeclaradoSobres
                                            objMedioPagEfec.ValorRecontadoSobres += objMedioPagEfecParcial.ValorRecontadoSobres
                                        End If

                                    Next

                                End If

                            Next

                        End If

                    Next

                Else

                    objReciboF22.ColMedioPagoDeclarados = New List(Of ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado)

                    ' captura a lista de parciais filhas
                    Dim listaParciais = (From objResult In objReciboF22Col _
                                         Where objResult.TipoRegistro = "01" _
                                         AndAlso objResult.OidRemesa = objReciboF22Local.OidRemesa)

                    If listaParciais.Count > 0 Then

                        For Each objParcial As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22Respaldo In listaParciais

                            If objParcial.ColMedioPagoDeclarados IsNot Nothing Then

                                For Each objMedioPagEfecParcial As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado In objParcial.ColMedioPagoDeclarados
                                    Dim objMedioPagoDeclarado As New ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado
                                    objMedioPagoDeclarado.CodigoMedioPago = objMedioPagEfecParcial.CodigoMedioPago
                                    objMedioPagoDeclarado.DescripcionMedioPago = objMedioPagEfecParcial.DescripcionMedioPago
                                    objMedioPagoDeclarado.ValorDeclaradoF22 = Decimal.Zero
                                    objMedioPagoDeclarado.ValorDeclaradoSobres += objMedioPagEfecParcial.ValorDeclaradoSobres
                                    objMedioPagoDeclarado.ValorRecontadoSobres += objMedioPagEfecParcial.ValorRecontadoSobres
                                    objReciboF22.ColMedioPagoDeclarados.Add(objMedioPagoDeclarado)
                                Next

                            End If

                        Next

                    End If

                End If

            End If
        Next

    End Sub

    Private Shared Sub CarregarDadosRemesa(drReciboF22 As IDataReader, ByRef objReciboF22Col As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22RespaldoColeccion)

        While (drReciboF22.Read)

            ' cria objeto do tipo do relatório
            Dim objReciboF22 As New ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22Respaldo

            'Atribuindo valores da consulta ao objeto
            Util.AtribuirValorObjeto(objReciboF22.TipoRegistro, drReciboF22("TIPO_REGISTRO"), GetType(String))
            Util.AtribuirValorObjeto(objReciboF22.CodigoRecuento, drReciboF22("DES_BOOK_PROCESO"), GetType(String))
            Util.AtribuirValorObjeto(objReciboF22.FechaRecaudacion, drReciboF22("FEC_TRANSPORTE"), GetType(Date))
            Util.AtribuirValorObjeto(objReciboF22.FechaSesion, drReciboF22("FYH_FIN_CONTEO"), GetType(Date))
            Util.AtribuirValorObjeto(objReciboF22.LetraReciboTransporte, drReciboF22("LETRA_RECIBO_TRANSPORTE"), GetType(String))
            Util.AtribuirValorObjeto(objReciboF22.NumReciboTransporte, drReciboF22("COD_TRANSPORTE"), GetType(String))

            If objReciboF22.TipoRegistro.Equals("00") Then
                Util.AtribuirValorObjeto(objReciboF22.Observaciones, drReciboF22("OBSERVACIONES_REMESA"), GetType(String))
            Else
                Util.AtribuirValorObjeto(objReciboF22.Observaciones, drReciboF22("OBSERVACIONES_PARCIAL"), GetType(String))
            End If

            'Verifica se existe 'Enter' na observação e se existir remove para não ser exibido no relatório
            If Not String.IsNullOrEmpty(objReciboF22.Observaciones) AndAlso objReciboF22.Observaciones.Contains(ControlChars.NewLine) Then
                objReciboF22.Observaciones = objReciboF22.Observaciones.Replace(ControlChars.NewLine, String.Empty)
            End If

            Util.AtribuirValorObjeto(objReciboF22.OidRemesa, drReciboF22("OID_REMESA"), GetType(String))
            Util.AtribuirValorObjeto(objReciboF22.OidParcial, drReciboF22("OID_PARCIAL"), GetType(String))

            ' adicionando o objeto do registro de relatório na coleção 
            objReciboF22Col.Add(objReciboF22)

        End While

    End Sub

    Private Shared Sub CarregarDadosTerminos(drReciboF22 As IDataReader, ByRef objReciboF22Col As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22RespaldoColeccion)

        While (drReciboF22.Read)

            ' cria objeto do tipo do relatório            

            Dim objReciboF22 = (From ColecRel In objReciboF22Col _
                                Where ColecRel.OidRemesa = drReciboF22("OID_REMESA"))

            ' verifica se é o término codSucursal
            If drReciboF22("CODIGO").ToString().ToUpper().Equals(CONST_COD_SUCURSAL) Then

                For i As Integer = 0 To objReciboF22.Count - 1 Step 1
                    objReciboF22(i).SucursalCliente = drReciboF22("DES_VALOR")
                Next

                ' se for DescSucursal
            ElseIf drReciboF22("CODIGO").ToString().ToUpper().Equals(CONST_DESC_SUCURSAL) Then

                For i As Integer = 0 To objReciboF22.Count - 1 Step 1
                    objReciboF22(i).DescripcionSucursal = drReciboF22("DES_VALOR")
                Next

                ' se for Legajo e NumeroSobre
            ElseIf drReciboF22("CODIGO").ToString().ToUpper().Equals(CONST_LEGAJO) OrElse _
                   drReciboF22("CODIGO").ToString().ToUpper().Equals(CONST_NUMEROSOBRE) Then

                ' carregando a nível de parciais os valores dos términos para Legajo e NumeroSobre
                Dim objReciboF22Parciais = (From ColecRel In objReciboF22Col _
                                            Where ColecRel.OidRemesa = drReciboF22("OID_REMESA") _
                                            AndAlso ColecRel.OidParcial = drReciboF22("OID_PARCIAL") _
                                            AndAlso ColecRel.TipoRegistro = "01")

                ' adicionando o valor de Legajo e NumeroSobre
                For i As Integer = 0 To objReciboF22Parciais.Count - 1 Step 1

                    If drReciboF22("CODIGO").ToString().ToUpper().Equals(CONST_LEGAJO) Then
                        objReciboF22Parciais(i).Legajo = drReciboF22("DES_VALOR")
                    Else
                        objReciboF22Parciais(i).NumSobre = drReciboF22("DES_VALOR")
                    End If

                Next

            End If

        End While

    End Sub

    Private Shared Sub CarregarDadosDeclarados(drReciboF22 As IDataReader, ByRef objReciboF22Col As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22RespaldoColeccion)

        Dim blExisteDeclarado = False

        ' para cada registro de declarados
        While (drReciboF22.Read)

            blExisteDeclarado = True

            ' somente se for remesa carregará o declarados da remesa. Para parciais será em Branco
            ' Documentação: Corresponde al declarado de la remesa. Discriminado por Divisa (efectivo), Cheques. Para divisa se mostrará el declarado del efectivo.
            If drReciboF22("TIPO_DECLARADO").Equals("R") Then

                Dim objReciboF22 = (From ColecRel In objReciboF22Col _
                                    Where ColecRel.OidRemesa = drReciboF22("OID_REMESA") _
                                      AndAlso ColecRel.TipoRegistro = "00")

                For i As Integer = 0 To objReciboF22.Count - 1 Step 1

                    If objReciboF22(i).ColMedioPagoDeclarados Is Nothing Then
                        objReciboF22(i).ColMedioPagoDeclarados = New List(Of ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado)
                    End If

                    ' busca nos dados da remesa se já existe valores de meio de pagamento para ela e soma os valores.
                    Dim objMedioPagoDecla = (From mp In objReciboF22(i).ColMedioPagoDeclarados _
                                             Where mp.CodigoMedioPago = drReciboF22("COD_ISO_DIVISA") _
                                               AndAlso mp.DescripcionMedioPago = drReciboF22("DES_DIVISA"))

                    If objMedioPagoDecla.Count > 0 Then

                        If drReciboF22("NUM_IMPORTE_EFECTIVO") IsNot DBNull.Value AndAlso _
                           Decimal.Parse(drReciboF22("NUM_IMPORTE_EFECTIVO")) > Decimal.Zero Then

                            ' se já existir soma o valor de declarado
                            objMedioPagoDecla(0).ValorDeclaradoF22 = Decimal.Parse(drReciboF22("NUM_IMPORTE_EFECTIVO"))

                        End If

                    Else

                        ' se não exitir adiciona um novo registro de MedioPago e declarados
                        If drReciboF22("NUM_IMPORTE_EFECTIVO") IsNot DBNull.Value AndAlso _
                           Decimal.Parse(drReciboF22("NUM_IMPORTE_EFECTIVO")) > Decimal.Zero Then

                            Dim objMedioPagoDeclarado As New ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado
                            objMedioPagoDeclarado.CodigoMedioPago = drReciboF22("COD_ISO_DIVISA")
                            objMedioPagoDeclarado.DescripcionMedioPago = drReciboF22("DES_DIVISA")
                            objMedioPagoDeclarado.ValorDeclaradoF22 = drReciboF22("NUM_IMPORTE_EFECTIVO")
                            objMedioPagoDeclarado.CodigoDivisa = drReciboF22("COD_ISO_DIVISA")
                            objReciboF22(i).ColMedioPagoDeclarados.Add(objMedioPagoDeclarado)

                        End If

                        If drReciboF22("NUM_IMPORTE_CHEQUE") IsNot DBNull.Value AndAlso _
                           Decimal.Parse(drReciboF22("NUM_IMPORTE_CHEQUE")) > Decimal.Zero Then

                            Dim objMedioPagoDeclarado As New ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado
                            'Caso tenga cheques contados para la divisa Peso se mostrará: Código= “CHEQU”  Descripción = “Cheques”, este es el único caso que no respectará la regla.
                            If drReciboF22("COD_ISO_DIVISA").Equals("PES") Then
                                objMedioPagoDeclarado.CodigoMedioPago = CONST_COD_CHEQUE_PESO
                            Else
                                objMedioPagoDeclarado.CodigoMedioPago = CONST_COD_CHEQUE & drReciboF22("COD_ISO_DIVISA").ToString().Substring(0, 2)
                            End If

                            objMedioPagoDeclarado.DescripcionMedioPago = "013_des_cheque"
                            objMedioPagoDeclarado.ValorDeclaradoF22 = drReciboF22("NUM_IMPORTE_CHEQUE")
                            objMedioPagoDeclarado.CodigoDivisa = drReciboF22("COD_ISO_DIVISA") & CONST_COD_CHEQUE
                            objReciboF22(i).ColMedioPagoDeclarados.Add(objMedioPagoDeclarado)

                        End If

                        If drReciboF22("NUM_IMPORTE_TICKET") IsNot DBNull.Value AndAlso _
                           Decimal.Parse(drReciboF22("NUM_IMPORTE_TICKET")) > Decimal.Zero Then

                            Dim objMedioPagoDeclarado As New ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado
                            objMedioPagoDeclarado.CodigoMedioPago = CONST_COD_TICKET & drReciboF22("COD_ISO_DIVISA").ToString().Substring(0, 2)
                            objMedioPagoDeclarado.DescripcionMedioPago = "013_des_ticket"
                            objMedioPagoDeclarado.ValorDeclaradoF22 = drReciboF22("NUM_IMPORTE_TICKET")
                            objMedioPagoDeclarado.CodigoDivisa = drReciboF22("COD_ISO_DIVISA") & CONST_COD_TICKET
                            objReciboF22(i).ColMedioPagoDeclarados.Add(objMedioPagoDeclarado)

                        End If

                        If drReciboF22("NUM_IMPORTE_OTRO_VALOR") IsNot DBNull.Value AndAlso _
                          Decimal.Parse(drReciboF22("NUM_IMPORTE_OTRO_VALOR")) > Decimal.Zero Then

                            Dim objMedioPagoDeclarado As New ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado
                            objMedioPagoDeclarado.CodigoMedioPago = CONST_COD_OTRO_VALOR & drReciboF22("COD_ISO_DIVISA").ToString().Substring(0, 2)
                            objMedioPagoDeclarado.DescripcionMedioPago = "013_des_outro_valor"
                            objMedioPagoDeclarado.ValorDeclaradoF22 = drReciboF22("NUM_IMPORTE_OTRO_VALOR")
                            objMedioPagoDeclarado.CodigoDivisa = drReciboF22("COD_ISO_DIVISA") & CONST_COD_OTRO_VALOR
                            objReciboF22(i).ColMedioPagoDeclarados.Add(objMedioPagoDeclarado)

                        End If

                    End If

                Next

            Else

                Dim objReciboF22 = (From ColecRel In objReciboF22Col _
                                    Where ColecRel.OidRemesa = drReciboF22("OID_REMESA") _
                                    AndAlso ColecRel.OidParcial = drReciboF22("OID_PARCIAL") _
                                    AndAlso ColecRel.TipoRegistro = "01")

                ' adicionando cada parcial para a coleção do relatório
                For i As Integer = 0 To objReciboF22.Count - 1 Step 1

                    If objReciboF22(i).ColMedioPagoDeclarados Is Nothing Then
                        objReciboF22(i).ColMedioPagoDeclarados = New List(Of ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado)
                    End If

                    ' se não exitir adiciona um novo registro de MedioPago e declarados
                    If drReciboF22("NUM_IMPORTE_EFECTIVO") IsNot DBNull.Value AndAlso _
                       Decimal.Parse(drReciboF22("NUM_IMPORTE_EFECTIVO")) > Decimal.Zero Then

                        Dim objMedioPagoDeclarado As New ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado
                        objMedioPagoDeclarado.CodigoMedioPago = drReciboF22("COD_ISO_DIVISA")
                        objMedioPagoDeclarado.DescripcionMedioPago = drReciboF22("DES_DIVISA")
                        objMedioPagoDeclarado.ValorDeclaradoSobres = drReciboF22("NUM_IMPORTE_EFECTIVO")
                        objMedioPagoDeclarado.CodigoDivisa = drReciboF22("COD_ISO_DIVISA")
                        objReciboF22(i).ColMedioPagoDeclarados.Add(objMedioPagoDeclarado)

                    End If

                    If drReciboF22("NUM_IMPORTE_CHEQUE") IsNot DBNull.Value AndAlso _
                       Decimal.Parse(drReciboF22("NUM_IMPORTE_CHEQUE")) > Decimal.Zero Then

                        Dim objMedioPagoDeclarado As New ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado
                        'Caso tenga cheques contados para la divisa Peso se mostrará: Código= “CHEQU”  Descripción = “Cheques”, este es el único caso que no respectará la regla.
                        If drReciboF22("COD_ISO_DIVISA").Equals("PES") Then
                            objMedioPagoDeclarado.CodigoMedioPago = CONST_COD_CHEQUE_PESO
                        Else
                            objMedioPagoDeclarado.CodigoMedioPago = CONST_COD_CHEQUE & drReciboF22("COD_ISO_DIVISA").ToString().Substring(0, 2)
                        End If

                        objMedioPagoDeclarado.DescripcionMedioPago = "013_des_cheque"
                        objMedioPagoDeclarado.ValorDeclaradoSobres = drReciboF22("NUM_IMPORTE_CHEQUE")
                        objMedioPagoDeclarado.CodigoDivisa = drReciboF22("COD_ISO_DIVISA") & CONST_COD_CHEQUE
                        objReciboF22(i).ColMedioPagoDeclarados.Add(objMedioPagoDeclarado)

                    End If

                    If drReciboF22("NUM_IMPORTE_TICKET") IsNot DBNull.Value AndAlso _
                       Decimal.Parse(drReciboF22("NUM_IMPORTE_TICKET")) > Decimal.Zero Then

                        Dim objMedioPagoDeclarado As New ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado
                        objMedioPagoDeclarado.CodigoMedioPago = CONST_COD_TICKET & drReciboF22("COD_ISO_DIVISA").ToString().Substring(0, 2)
                        objMedioPagoDeclarado.DescripcionMedioPago = "013_des_ticket"
                        objMedioPagoDeclarado.ValorDeclaradoSobres = drReciboF22("NUM_IMPORTE_TICKET")
                        objMedioPagoDeclarado.CodigoDivisa = drReciboF22("COD_ISO_DIVISA") & CONST_COD_TICKET
                        objReciboF22(i).ColMedioPagoDeclarados.Add(objMedioPagoDeclarado)

                    End If

                    If drReciboF22("NUM_IMPORTE_OTRO_VALOR") IsNot DBNull.Value AndAlso _
                      Decimal.Parse(drReciboF22("NUM_IMPORTE_OTRO_VALOR")) > Decimal.Zero Then

                        Dim objMedioPagoDeclarado As New ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado
                        objMedioPagoDeclarado.CodigoMedioPago = CONST_COD_OTRO_VALOR & drReciboF22("COD_ISO_DIVISA").ToString().Substring(0, 2)
                        objMedioPagoDeclarado.DescripcionMedioPago = "013_des_outro_valor"
                        objMedioPagoDeclarado.ValorDeclaradoSobres = drReciboF22("NUM_IMPORTE_OTRO_VALOR")
                        objMedioPagoDeclarado.CodigoDivisa = drReciboF22("COD_ISO_DIVISA") & CONST_COD_OTRO_VALOR
                        objReciboF22(i).ColMedioPagoDeclarados.Add(objMedioPagoDeclarado)

                    End If

                Next

            End If

        End While

    End Sub

    Private Shared Sub CarregarDadosMediosPago(drReciboF22 As IDataReader, ByRef objReciboF22Col As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22RespaldoColeccion)

        ' para cada registro de Medio Pago
        While (drReciboF22.Read)

            Dim objReciboF22 = (From ColecRel In objReciboF22Col _
                                Where ColecRel.OidRemesa = drReciboF22("OID_REMESA") _
                                AndAlso ColecRel.OidParcial = drReciboF22("OID_PARCIAL") _
                                AndAlso ColecRel.TipoRegistro = "01")

            For i As Integer = 0 To objReciboF22.Count - 1 Step 1

                If objReciboF22(i).ColMedioPagoDeclarados Is Nothing Then
                    objReciboF22(i).ColMedioPagoDeclarados = New List(Of ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado)
                End If

                ' Criando o código de medio pago de acordo com os dados retornados do banco
                Dim strCodMedioPago As String = String.Empty

                If drReciboF22("COD_TIPO_MEDIO_PAGO").ToString().Equals(CONST_COD_MEDIO_PAGO_CHEQUE) Then

                    'Caso tenga cheques contados para la divisa Peso se mostrará: Código= “CHEQU”  Descripción = “Cheques”, este es el único caso que no respectará la regla.
                    If drReciboF22("COD_ISO_DIVISA").Equals("PES") Then
                        strCodMedioPago = CONST_COD_CHEQUE_PESO
                    Else
                        strCodMedioPago = CONST_COD_CHEQUE & drReciboF22("COD_ISO_DIVISA").ToString().Substring(0, 2)
                    End If

                ElseIf drReciboF22("COD_TIPO_MEDIO_PAGO").ToString().Equals(CONST_COD_MEDIO_PAGO_OUTROS) Then

                    strCodMedioPago = CONST_COD_OTRO_VALOR & drReciboF22("COD_ISO_DIVISA").ToString().Substring(0, 2)

                ElseIf drReciboF22("COD_TIPO_MEDIO_PAGO").ToString().Equals(CONST_COD_MEDIO_PAGO_TICKET) Then

                    strCodMedioPago = CONST_COD_TICKET & drReciboF22("COD_ISO_DIVISA").ToString().Substring(0, 2)

                End If

                Dim objMedioPagoDeclarado = (From colMedioPago In objReciboF22(i).ColMedioPagoDeclarados _
                                             Where colMedioPago.CodigoMedioPago = strCodMedioPago)

                If objMedioPagoDeclarado.Count > 0 Then

                    objMedioPagoDeclarado(0).ValorRecontadoSobres = drReciboF22("VALOR")

                Else

                    Dim objMedioPagoDeclaradoNovo = New ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado
                    objMedioPagoDeclaradoNovo.CodigoMedioPago = drReciboF22("COD_MEDIO_PAGO")
                    objMedioPagoDeclaradoNovo.DescripcionMedioPago = drReciboF22("DES_TIPO_MEDIO_PAGO").ToString()
                    objMedioPagoDeclaradoNovo.ValorRecontadoSobres = drReciboF22("VALOR")
                    objMedioPagoDeclaradoNovo.CodigoDivisa = drReciboF22("COD_ISO_DIVISA") & drReciboF22("COD_MEDIO_PAGO")
                    objReciboF22(i).ColMedioPagoDeclarados.Add(objMedioPagoDeclaradoNovo)

                    ' adiciona o valor de efectivos a nível de remesa se não existir declarado remesa para a divisa
                    Dim objReciboF22Remesa = (From ColecRel In objReciboF22Col _
                                              Where ColecRel.OidRemesa = drReciboF22("OID_REMESA") _
                                                AndAlso ColecRel.TipoRegistro = "00")

                    If objReciboF22Remesa IsNot Nothing AndAlso objReciboF22Remesa.Count > 0 Then

                        ' verifica se a divisa de efetivo já existe a nível de remesa, se não existir ela será adicionada
                        If objReciboF22Remesa(0).ColMedioPagoDeclarados IsNot Nothing AndAlso objReciboF22Remesa(0).ColMedioPagoDeclarados.Where(Function(o) o.CodigoDivisa = objMedioPagoDeclaradoNovo.CodigoDivisa).ToList().Count = 0 Then

                            Dim objMedioPagoRemesa As New ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado
                            objMedioPagoRemesa.CodigoMedioPago = drReciboF22("COD_MEDIO_PAGO")
                            objMedioPagoRemesa.DescripcionMedioPago = drReciboF22("DES_TIPO_MEDIO_PAGO").ToString()
                            objMedioPagoRemesa.CodigoDivisa = drReciboF22("COD_ISO_DIVISA") & drReciboF22("COD_MEDIO_PAGO")
                            objReciboF22Remesa(0).ColMedioPagoDeclarados.Add(objMedioPagoRemesa)

                        End If

                    End If

                End If

            Next

        End While

    End Sub

    Private Shared Sub CarregarDadosEfectivos(drReciboF22 As IDataReader, ByRef objReciboF22Col As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22RespaldoColeccion)

        ' para cada registro de Efectivos
        While (drReciboF22.Read)

            Dim objReciboF22 = (From ColecRel In objReciboF22Col _
                                Where ColecRel.OidRemesa = drReciboF22("OID_REMESA") _
                                AndAlso ColecRel.OidParcial = drReciboF22("OID_PARCIAL") _
                                AndAlso ColecRel.TipoRegistro = "01")

            For i As Integer = 0 To objReciboF22.Count - 1 Step 1

                If objReciboF22(i).ColMedioPagoDeclarados Is Nothing Then
                    objReciboF22(i).ColMedioPagoDeclarados = New List(Of ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado)
                End If

                Dim objMedioPagoDeclarado = (From colMedioPago In objReciboF22(i).ColMedioPagoDeclarados _
                                             Where colMedioPago.CodigoMedioPago = drReciboF22("COD_ISO_DIVISA") _
                                               AndAlso colMedioPago.DescripcionMedioPago = drReciboF22("DES_DIVISA"))

                If objMedioPagoDeclarado.Count > 0 Then

                    objMedioPagoDeclarado(0).ValorRecontadoSobres = drReciboF22("RECONTADO")

                Else

                    Dim objMedioPagoDeclaradoNovo = New ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado
                    objMedioPagoDeclaradoNovo.CodigoMedioPago = drReciboF22("COD_ISO_DIVISA")
                    objMedioPagoDeclaradoNovo.DescripcionMedioPago = drReciboF22("DES_DIVISA")
                    objMedioPagoDeclaradoNovo.ValorRecontadoSobres = drReciboF22("RECONTADO")
                    objMedioPagoDeclaradoNovo.CodigoDivisa = drReciboF22("COD_ISO_DIVISA")
                    objReciboF22(i).ColMedioPagoDeclarados.Add(objMedioPagoDeclaradoNovo)

                    ' adiciona o valor de efectivos a nível de remesa se não existir declarado remesa para a divisa
                    Dim objReciboF22Remesa = (From ColecRel In objReciboF22Col _
                                              Where ColecRel.OidRemesa = drReciboF22("OID_REMESA") _                                                
                                                AndAlso ColecRel.TipoRegistro = "00")

                    If objReciboF22Remesa IsNot Nothing AndAlso objReciboF22Remesa.Count > 0 Then

                        If objReciboF22Remesa(0).ColMedioPagoDeclarados Is Nothing Then
                            objReciboF22Remesa(0).ColMedioPagoDeclarados = New List(Of ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado)
                        End If

                        ' verifica se a divisa de efetivo já existe a nível de remesa, se não existir ela será adicionada
                        If objReciboF22Remesa(0).ColMedioPagoDeclarados.Where(Function(o) o.CodigoDivisa = objMedioPagoDeclaradoNovo.CodigoDivisa).ToList().Count = 0 Then

                            Dim objMedioPagoRemesa As New ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.MedioPagoDeclarado
                            objMedioPagoRemesa.CodigoMedioPago = drReciboF22("COD_ISO_DIVISA")
                            objMedioPagoRemesa.DescripcionMedioPago = drReciboF22("DES_DIVISA")
                            objMedioPagoRemesa.ValorDeclaradoF22 = Decimal.Zero
                            objMedioPagoRemesa.CodigoDivisa = drReciboF22("COD_ISO_DIVISA")
                            objReciboF22Remesa(0).ColMedioPagoDeclarados.Add(objMedioPagoRemesa)

                        End If

                    End If

                End If

            Next

        End While

    End Sub

    ''' <summary>
    ''' Retorna a quantidade de parciais declaradas no legado
    ''' </summary>
    ''' <param name="oidRemesa">Oid da remesa que contém o número de parciais</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function BuscaQuantidadeParciaisRemesa(oidRemesa As String) As Decimal

        ' Limpa o objeto da memória quando termina de usá-lo
        Using comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            ' obter procedure
            comando.CommandText = Util.PrepararQuery(My.Resources.BuscaNecDeclaradosRemesa.ToString)
            comando.CommandType = CommandType.Text

            ' setar parametros                        
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "P_OID_REMESA", ProsegurDbType.Identificador_Alfanumerico, oidRemesa))

            Dim qtdNecParciales As Object = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando)

            If qtdNecParciales IsNot Nothing AndAlso qtdNecParciales IsNot DBNull.Value Then

                Return DirectCast(qtdNecParciales, Decimal)

            Else

                Return Decimal.Zero

            End If

        End Using

    End Function

#End Region

End Class
