Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.NuevoSalidas.Remesa
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports System.Windows.Forms
Imports Prosegur.Framework.Dicionario.Tradutor

Namespace NuevoSalidas

    Public Class Puesto

        ''' <summary>
        ''' Obter postos por delegação.
        ''' </summary>
        ''' <param name="codigoDelegacion">Código da Delegação</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPuestosPorDelegacion(codigoDelegacion As String, CodigosSectores As List(Of String)) As ObservableCollection(Of Clases.Puesto)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Puesto_ObtenerPuestosPorDelegacion)
            cmd.CommandType = CommandType.Text

            If CodigosSectores IsNot Nothing AndAlso CodigosSectores.Count > 0 Then
                cmd.CommandText = String.Format(cmd.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, CodigosSectores, "COD_PUESTO", cmd, "AND", "P"))
            Else
                cmd.CommandText = String.Format(cmd.CommandText, String.Empty)
            End If

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))

            Return CargarPuestosPorDelegacion(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd))

        End Function

        ''' <summary>
        ''' Obter postos por delegação.
        ''' </summary>
        ''' <param name="codigoDelegacion">Código da Delegação</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPuestos(CodigoDelegacion As String, BolVigente As Boolean?) As ObservableCollection(Of Clases.Puesto)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            Dim SQLCondiciones As New System.Text.StringBuilder

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Puesto_ObtenerPuestos)
            cmd.CommandType = CommandType.Text

            If Not String.IsNullOrEmpty(CodigoDelegacion) Then
                SQLCondiciones.AppendLine(" AND PUE.COD_DELEGACION = []COD_DELEGACION")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDelegacion))
            End If

            If BolVigente.HasValue Then
                SQLCondiciones.AppendLine(" AND PUE.BOL_VIGENTE = []BOL_VIGENTE")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "BOL_VIGENTE", ProsegurDbType.Logico, BolVigente))
            End If

            SQLCondiciones.AppendLine(" ORDER BY PUE.COD_PUESTO")

            If SQLCondiciones.Length > 0 Then
                cmd.CommandText &= SQLCondiciones.ToString
            End If

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

            Return CargarPuestos(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd))

        End Function

        Private Shared Function CargarPuestos(dtRetorno As DataTable) As ObservableCollection(Of Clases.Puesto)

            ' Variável que recebe os postos
            Dim lstPuestos As ObservableCollection(Of Clases.Puesto) = Nothing

            ' Verifica se retornou algum dado
            If dtRetorno IsNot Nothing Then

                ' Cria uma nova instancia para a lista de postos
                lstPuestos = New ObservableCollection(Of Clases.Puesto)

                ' Para cada posto retornado
                For Each dr As DataRow In dtRetorno.Rows

                    ' Adiciona o posto
                    lstPuestos.Add(New Clases.Puesto With
                                   {
                                       .Codigo = If(Not IsDBNull(dr("COD_PUESTO")), dr("COD_PUESTO"), String.Empty)
                                   })
                Next dr

            End If

            ' Retorna as situações das remesas
            Return lstPuestos

        End Function

        Private Shared Function CargarPuestosPorDelegacion(dtRetorno As DataTable) As ObservableCollection(Of Clases.Puesto)

            ' Variável que recebe os postos
            Dim lstPuestos As ObservableCollection(Of Clases.Puesto) = Nothing

            ' Verifica se retornou algum dado
            If dtRetorno IsNot Nothing Then

                ' Cria uma nova instancia para a lista de postos
                lstPuestos = New ObservableCollection(Of Clases.Puesto)

                ' Para cada posto retornado
                For Each dr As DataRow In dtRetorno.Rows

                    ' Adiciona o posto
                    lstPuestos.Add(New Clases.Puesto With
                                   {
                                       .Codigo = dr("COD_PUESTO"),
                                       .CantidadRemesas = dr("CANTIDAD_REMESA"),
                                       .CantidadBultos = dr("CANTIDAD_BULTO")
                                   })
                Next

            End If

            ' Retorna as situações das remesas
            Return lstPuestos

        End Function

        Public Shared Sub ActualizarSaldoPuesto(Divisas As ObservableCollection(Of Clases.Divisa), _
                                         CodigoDelegacion As String, _
                                         CodigoPuesto As String, Transacion As Transacao)

            Try

                'Indica o que fazer com a transação
                Dim ExecutarTransacion As Boolean = False

                'Define se irá executar a transação neste método
                If (Transacion Is Nothing) Then
                    ExecutarTransacion = True
                    Transacion = New Transacao(Constantes.CONEXAO_SALIDAS)
                End If

                'Para cada efetivo é executado a autalização/inserção dos valores
                For Each divisa As Clases.Divisa In Divisas

                    'busca pelo Oid do Saldo do puesto
                    Dim IdentificadorSaldoPuesto As String = ObtenerIdentificadorSaldoPuesto(CodigoDelegacion, CodigoPuesto, divisa.CodigoISO)

                    'Verifica se foi encontrado o oid
                    If (String.IsNullOrEmpty(IdentificadorSaldoPuesto)) Then

                        'Obtem um novo Oid
                        IdentificadorSaldoPuesto = Guid.NewGuid.ToString

                        Dim IdentificadorPuesto As String = ObtenerIdentificadorPuesto(CodigoDelegacion, CodigoPuesto)
                        Dim IdentificadorDivisa As String = Genesis.Divisas.ObtenerIdentificadorDivisa(divisa.CodigoISO, Constantes.CONEXAO_SALIDAS)

                        If String.IsNullOrEmpty(IdentificadorPuesto) Then
                            Throw New Excepcion.NegocioExcepcion(Traduzir("007_srv_oidpuestonoencontrado"))
                        End If

                        If String.IsNullOrEmpty(IdentificadorDivisa) Then
                            Throw New Excepcion.NegocioExcepcion(Traduzir("007_srv_oiddivisanoencontrado"))
                        End If

                        'Não encontrou. Executa rotina de inserção
                        'TODO
                        ' validar objeto divisa
                        SaldoPuestoDivisaInserir(IdentificadorSaldoPuesto, IdentificadorPuesto, IdentificadorDivisa, divisa.ValoresTotalesDivisa.FirstOrDefault.Importe, Transacion)
                    Else
                        'Encontrou. Executa rotina de atualização
                        'Adiciona operação ao objeto de transação

                        'TODO
                        ' validar objeto divisa
                        SaldoPuestoTotalActualizar(IdentificadorSaldoPuesto, divisa.ValoresTotalesDivisa.FirstOrDefault.Importe, Transacion)
                    End If

                    ''Para cada ocorrecia de um Detalle adiciona transação para inserir e/ou atualizar os registros de saldos do posto detalhe
                    'For Each denominacion As Clases.Denominacion In divisa.Denominaciones

                    '    ' criar objeto de comando SQL
                    '    Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

                    '    'busca pelo Oid do Saldo do puesto
                    '    Dim IdentificadorSaldoPuestoDetalle As String = ObterOidSaldoPuestoDetalle(CodigoDelegacion, CodigoPuesto, divisa.CodigoISO, denominacion.Codigo)

                    '    'Verifica se o Oid foi retornado
                    '    If (String.IsNullOrEmpty(IdentificadorSaldoPuestoDetalle)) Then
                    '        'Oid Não encontrado. Executa a rotina de inserção

                    '        'Obter o OidDenominacion
                    '        Dim IdentificadorDenominacion As String = denominacion.ObterOidDenominacion(denominacion.Codigo)

                    '        'Insere o novo registro
                    '        InsereSaldoPuestoDetalle(IdentificadorDenominacion, IdentificadorSaldoPuesto, efetivoDetalle.NelCantidad, efetivoDetalle.NumImporteTotal, Transacion)

                    '    Else
                    '        'Oid encontrado. Executa a rotina de atualização
                    '        'Atualiza o valor para a denominacion
                    '        AtualizarSaldoPuestoDetalle(OidSaldoPuestoDetalle, efetivoDetalle.NelCantidad, efetivoDetalle.NumImporteTotal, Transacion)
                    '    End If

                    'Next

                Next

                'verifica se deve executar a transação
                If (ExecutarTransacion) Then
                    Transacion.RealizarTransacao()
                End If

            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try

        End Sub

        Public Shared Function ObtenerIdentificadorSaldoPuesto(CodigoDelegacion As String, _
                                                               CodigoPuesto As String, _
                                                               CodigoIsoDivisa As String) As String

            ' criar objeto de comando SQL
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            'Verifica registro com os dados informados
            comando.CommandText = My.Resources.Salidas_Puesto_ObtenerIdentificadorSaldoPuestoPorDivisaYPuesto

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDelegacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, CodigoPuesto))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ISO_DIVISA", ProsegurDbType.Identificador_Alfanumerico, CodigoIsoDivisa))

            'busca pelo Oid do Saldo do puesto
            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALIDAS, comando)

        End Function

        Public Shared Function ObtenerIdentificadorPuesto(CodigoDelegacion As String, CodigoPuesto As String) As String

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            comando.CommandType = CommandType.Text
            comando.CommandText = My.Resources.Salidas_Puesto_ObtenerIdentificadorPuesto
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDelegacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, CodigoPuesto))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, comando)

            Return Util.AtribuirValorObj(dt.Rows(0)("OID_PUESTO"), GetType(String))

        End Function

        Public Shared Sub SaldoPuestoDivisaInserir(IdentificadorSaldoPuesto As String, _
                                                   IdentificadorPuesto As String, _
                                                   IdentificadorDivisa As String, _
                                                   NumImporteTotal As Decimal, _
                                                   ByRef Transacion As Transacao)

            ' criar objeto de comando SQL
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            'Insere comando para inserir
            comando.CommandText = My.Resources.Salidas_Puesto_SaldoPuestoDivisaInserir
            'Parâmetros
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_SALDO_PUESTO", ProsegurDbType.Objeto_Id, IdentificadorSaldoPuesto))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_PUESTO", ProsegurDbType.Objeto_Id, IdentificadorPuesto))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_DIVISA", ProsegurDbType.Objeto_Id, IdentificadorDivisa))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "NUM_SALDO_DIVISA", ProsegurDbType.Numero_Decimal, NumImporteTotal))
            'Adiciona o item de transação
            Transacion.AdicionarItemTransacao(comando)

        End Sub

        Public Shared Sub SaldoPuestoTotalActualizar(IdentificadorSaldoPuesto As String, _
                                                     NumImporteTotal As Decimal, _
                                                     Optional ByRef Transacion As Transacao = Nothing)

            ' criar objeto de comando SQL
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            'insere comando para atualizar
            comando.CommandText = My.Resources.Salidas_Puesto_SaldoPuestoDivisaActualizar
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_SALDO_PUESTO", ProsegurDbType.Objeto_Id, IdentificadorSaldoPuesto))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "NUM_SALDO_DIVISA", ProsegurDbType.Numero_Decimal, NumImporteTotal))

            If (Transacion Is Nothing) Then
                'executa o comando
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, comando)
            Else
                'Adiciona o item de transação
                Transacion.AdicionarItemTransacao(comando)
            End If


        End Sub

        Public Shared Function ObtenerNecesidadFondoPuesto(CodigoDelegacion As String, CodigoPuesto As String, GestionaSaldoPuesto As Boolean) As ObservableCollection(Of Clases.Divisa)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            comando.CommandType = CommandType.Text
            comando.CommandText = My.Resources.Salidas_Puesto_ObtenerNecesidadFondoPuesto
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDelegacion))

            Dim remesaCondicion As New System.Text.StringBuilder
            Dim bultoCondicion As New System.Text.StringBuilder

            ' se controla saldo por posto, recupera as remesas e bulto que estão atribuidas para o posto específico.
            If GestionaSaldoPuesto Then

                remesaCondicion.Append(" AND REM.COD_ESTADO_REMESA = 'AS' AND PUE.COD_PUESTO = []COD_PUESTO ")
                bultoCondicion.Append(" AND BUL.COD_ESTADO_BULTO = 'AS' AND PUE.COD_PUESTO = []COD_PUESTO ")

                comando.CommandText = String.Format(comando.CommandText, remesaCondicion, bultoCondicion)
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, CodigoPuesto))

            Else ' se controla saldo por setor, recuperar as remesas e bultos que estão atribuidas ou pendentes de todos os postos(setor)

                remesaCondicion.Append(" AND (REM.COD_ESTADO_REMESA = 'AS' OR REM.COD_ESTADO_REMESA = 'PE') ")
                bultoCondicion.Append(" AND (BUL.COD_ESTADO_BULTO = 'AS' OR BUL.COD_ESTADO_BULTO = 'PE') ")

                comando.CommandText = String.Format(comando.CommandText, remesaCondicion, bultoCondicion)

            End If

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, comando.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, comando)

            Return CargarNecesidadFondoPuesto(dt)

        End Function

        Private Shared Function CargarNecesidadFondoPuesto(dt As DataTable) As ObservableCollection(Of Clases.Divisa)

            Dim divisas = New ObservableCollection(Of Clases.Divisa)

            If dt.Rows.Count > 0 Then

                For Each row In dt.Rows

                    Dim CodigoIso As String = Util.AtribuirValorObj(row("COD_ISO_DIVISA"), GetType(String))
                    Dim CodigoDenominacion As String = Util.AtribuirValorObj(row("COD_DENOMINACION"), GetType(String))

                    Dim divisa As Clases.Divisa = Nothing

                    ' se ainda não existe divisa na lista, busca todoas denominações
                    If Not divisas.Exists(Function(div) div.CodigoISO = CodigoIso) Then
                        divisa = New Clases.Divisa With {.Identificador = Util.AtribuirValorObj(row("OID_DIVISA"), GetType(String)),
                                                         .CodigoISO = CodigoIso,
                                                         .Descripcion = Util.AtribuirValorObj(row("DES_DIVISA"), GetType(String))
                                                        }
                        divisa.Denominaciones = Prosegur.Genesis.AccesoDatos.Genesis.Denominacion.RecuperarDenominaciones(String.Empty, divisa.CodigoISO)
                        divisas.Add(divisa)
                    Else

                        divisa = divisas.Where(Function(div) div.CodigoISO = CodigoIso).FirstOrDefault

                    End If

                    If divisa.Denominaciones IsNot Nothing AndAlso divisa.Denominaciones.Count > 0 Then

                        Dim denominacion As Clases.Denominacion = divisa.Denominaciones.Where(Function(den) den.Codigo = CodigoDenominacion).FirstOrDefault

                        Dim valorDenominacion As New Clases.ValorDenominacion With {.Cantidad = Util.AtribuirValorObj(row("NEL_CANTIDAD"), GetType(Integer)), _
                                                                                    .Importe = .Cantidad * denominacion.Valor, _
                                                                                    .TipoValor = TipoValor.NoDefinido
                                                                                   }

                        denominacion.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion) From {valorDenominacion}

                    End If

                Next row

            End If

            Return divisas

        End Function

    End Class

End Namespace