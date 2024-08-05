Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel

Namespace GenesisSaldos
    ''' <summary>
    ''' Classe de efectivo de documento.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EfectivoDocumento

        Public Shared Sub Inserir_v2(valores As ObservableCollection(Of Clases.Transferencias.EfectivoDocumentoInserir),
                             Optional ByRef _transacion As DbHelper.Transacao = Nothing)

            Try

                For Each valor As Clases.Transferencias.EfectivoDocumentoInserir In valores

                    Using comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                        comando.CommandText = My.Resources.EfectivoDocumentoInserir

                        If Not String.IsNullOrEmpty(valor.identificadorUnidadMedida) OrElse valor.nivelDetalhe <> Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor() Then
                            comando.CommandText = String.Format(comando.CommandText, "[]OID_UNIDAD_MEDIDA")
                            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_UNIDAD_MEDIDA", ProsegurDbType.Objeto_Id, valor.identificadorUnidadMedida))
                        Else
                            comando.CommandText = String.Format(comando.CommandText, " (SELECT OID_UNIDAD_MEDIDA FROM GEPR_TUNIDAD_MEDIDA U WHERE U.BOL_DEFECTO = 1 AND U.COD_TIPO_UNIDAD_MEDIDA = ( SELECT (CASE WHEN D.BOL_BILLETE = 1 THEN 0 ELSE 1 END) FROM GEPR_TDENOMINACION D WHERE D.OID_DENOMINACION = []OID_DENOMINACION))")
                        End If

                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_EFECTIVOXDOCUMENTO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString))
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, valor.identificadorDocumento))
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Objeto_Id, valor.identificadorDivisa))
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DENOMINACION", ProsegurDbType.Objeto_Id, valor.identificadorDenominacion))
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_NIVEL_DETALLE", ProsegurDbType.Identificador_Alfanumerico, valor.nivelDetalhe))
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_EFECTIVO_TOTAL", ProsegurDbType.Identificador_Alfanumerico, valor.tipoEfectivoTotal))
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CALIDAD", ProsegurDbType.Objeto_Id, valor.identificadorCalidad))
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NUM_IMPORTE", ProsegurDbType.Numero_Decimal, valor.numImporte))
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_CANTIDAD", ProsegurDbType.Inteiro_Longo, valor.cantidad))
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, valor.usuarioModificacion))
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, valor.usuarioModificacion))

                        comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

                        If _transacion IsNot Nothing Then
                            _transacion.AdicionarItemTransacao(comando)
                        Else
                            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, comando)
                        End If

                    End Using
                Next

            Catch ex As Exception
                Throw
            End Try

        End Sub

        ''' <summary>
        ''' Insere um efectivo de documento.
        ''' </summary>
        ''' <param name="identificadorDocumento">Es una referencia a la entidad de DOCUMENTO</param>
        ''' <param name="identificadorDivisa">Es una referencia a la entidad de DIVISA</param>
        ''' <param name="identificadorDenominacion">Es una referencia a la entidad de DENOMINACION</param>
        ''' <param name="nivelDetalhe">Indica el NIVEL de DETALLE: D=Detallado; T=total;</param>
        ''' <param name="tipoEfectivoTotal">Cuando el NIVEL de DETALLE sea T, será utilizado para indicar lo tipo de Total: B=Billete; M=Moneda;</param>
        ''' <param name="identificadorCalidad">Indica la Calidad de la Denominación: E=Excelente; B=Buena; P=Pésimo;</param>
        ''' <param name="numImporte">Indica el valor del importe</param>
        ''' <param name="Usuario">Usuario de creación/modificación</param>
        ''' <param name="cantidad">Cantidad de Efectivo informado</param>
        ''' <remarks></remarks>
        Public Shared Sub EfectivoDocumentoInserir(identificadorDocumento As String, _
                                                    identificadorDivisa As String, _
                                                    identificadorDenominacion As String, _
                                                    identificadorUnidadMedida As String, _
                                                    nivelDetalhe As String, _
                                                    tipoEfectivoTotal As String, _
                                                    identificadorCalidad As String, _
                                                    numImporte As Decimal, _
                                                    Usuario As String, _
                                                    cantidad As Decimal)

            If numImporte <> 0 OrElse cantidad <> 0 Then
                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandText = My.Resources.EfectivoDocumentoInserir
                cmd.CommandType = CommandType.Text

                If Not String.IsNullOrEmpty(identificadorUnidadMedida) OrElse nivelDetalhe <> Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor() Then
                    cmd.CommandText = String.Format(cmd.CommandText, "[]OID_UNIDAD_MEDIDA")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_UNIDAD_MEDIDA", ProsegurDbType.Objeto_Id, identificadorUnidadMedida))
                Else
                    cmd.CommandText = String.Format(cmd.CommandText, " (SELECT OID_UNIDAD_MEDIDA FROM GEPR_TUNIDAD_MEDIDA U WHERE U.BOL_DEFECTO = 1 AND U.COD_TIPO_UNIDAD_MEDIDA = ( SELECT (CASE WHEN D.BOL_BILLETE = 1 THEN 0 ELSE 1 END) FROM GEPR_TDENOMINACION D WHERE D.OID_DENOMINACION = []OID_DENOMINACION))")
                End If

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_EFECTIVOXDOCUMENTO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Objeto_Id, identificadorDivisa))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DENOMINACION", ProsegurDbType.Objeto_Id, identificadorDenominacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_NIVEL_DETALLE", ProsegurDbType.Descricao_Curta, nivelDetalhe))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_EFECTIVO_TOTAL", ProsegurDbType.Descricao_Curta, tipoEfectivoTotal))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CALIDAD", ProsegurDbType.Descricao_Curta, identificadorCalidad))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NUM_IMPORTE", ProsegurDbType.Numero_Decimal, numImporte))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_CANTIDAD", ProsegurDbType.Numero_Decimal, cantidad))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, Usuario))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, Usuario))
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            End If
        End Sub

        ''' <summary>
        ''' Recupera os efectivos do documento informado
        ''' </summary>
        ''' <param name="identificadorDocumento"></param>
        ''' <remarks></remarks>
        Public Shared Function RecuperarEfectivoPorDocumento(identificadorDocumento As String) As List(Of Clases.Divisa)
            Dim divisas As List(Of Clases.Divisa) = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.EfectivoDocumentoRecuperarPorDocumento)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                'Recupera os valores das denominações
                divisas = New List(Of Clases.Divisa)

                For Each OID_DIVISA In (From r In dt.AsEnumerable() Select r.Field(Of String)("OID_DIVISA")).Distinct()

                    Dim filtro As New System.Text.StringBuilder
                    filtro.AppendFormat("OID_DIVISA='{0}'", OID_DIVISA)

                    Dim divisa As New Clases.Divisa
                    'Denominaciones
                    divisa.Denominaciones = New ObservableCollection(Of Clases.Denominacion)

                    'Valor total efectivo da divisa.
                    divisa.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)

                    'Valor total das divisas
                    divisa.ValoresTotalesDivisa = New ObservableCollection(Of Clases.ValorDivisa)

                    'Valor total dos efectivos
                    divisa.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)

                    divisa.Identificador = OID_DIVISA
                    filtro = New System.Text.StringBuilder
                    filtro.AppendFormat("OID_DIVISA='{0}'", divisa.Identificador)
                    filtro.AppendFormat(" AND COD_NIVEL_DETALLE ='{0}'", Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor())

                    ' Recupera os registros detalhados
                    For Each drDenominacion In dt.Select(filtro.ToString())

                        If divisa.Denominaciones IsNot Nothing AndAlso drDenominacion IsNot Nothing Then

                            If Not divisa.Denominaciones.Exists(Function(d) d.Identificador = Util.AtribuirValorObj(drDenominacion("OID_DENOMINACION"), GetType(String))) Then

                                Dim denominacion As New Clases.Denominacion
                                denominacion.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)
                                denominacion.Identificador = Util.AtribuirValorObj(drDenominacion("OID_DENOMINACION"), GetType(String))

                                filtro = New System.Text.StringBuilder
                                filtro.AppendFormat("OID_DIVISA='{0}'", divisa.Identificador)
                                filtro.AppendFormat(" AND COD_NIVEL_DETALLE ='{0}'", Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor())
                                filtro.AppendFormat(" AND OID_DENOMINACION ='{0}'", denominacion.Identificador)

                                'Recupera o valor para cada valor
                                For Each drValor In dt.Select(filtro.ToString())
                                    Dim valor As New Clases.ValorDenominacion
                                    valor.Calidad = New Clases.Calidad
                                    valor.Calidad.Identificador = Util.AtribuirValorObj(drValor("OID_CALIDAD"), GetType(String))
                                    valor.Cantidad = Util.AtribuirValorObj(drValor("NEL_CANTIDAD"), GetType(Decimal))
                                    valor.Importe = Util.AtribuirValorObj(drValor("NUM_IMPORTE"), GetType(Decimal))
                                    valor.UnidadMedida = New Clases.UnidadMedida() With {.Identificador = Util.AtribuirValorObj(drValor("OID_UNIDAD_MEDIDA"), GetType(String))}
                                    denominacion.ValorDenominacion.Add(valor)
                                Next

                                divisa.Denominaciones.Add(denominacion)
                            End If

                        End If
                    Next

                    filtro = New System.Text.StringBuilder
                    filtro.AppendFormat("OID_DIVISA='{0}'", OID_DIVISA)
                    filtro.AppendFormat(" AND COD_NIVEL_DETALLE ='{0}'", Enumeradores.TipoNivelDetalhe.Total.RecuperarValor())

                    'Recupera os valores de total
                    For Each drValor In dt.Select(filtro.ToString)
                        Dim valor As New Clases.ValorEfectivo
                        If drValor("COD_TIPO_EFECTIVO_TOTAL") IsNot Nothing Then
                            valor.TipoDetalleEfectivo = RecuperarEnum(Of Enumeradores.TipoDetalleEfectivo)(drValor("COD_TIPO_EFECTIVO_TOTAL"))
                        End If

                        valor.Importe = Util.AtribuirValorObj(drValor("NUM_IMPORTE"), GetType(Decimal))
                        divisa.ValoresTotalesEfectivo.Add(valor)
                    Next

                    filtro = New System.Text.StringBuilder
                    filtro.AppendFormat("OID_DIVISA='{0}'", OID_DIVISA)
                    filtro.AppendFormat(" AND COD_NIVEL_DETALLE ='{0}'", Enumeradores.TipoNivelDetalhe.TotalGeral.RecuperarValor())

                    'Recupera os valores de total geral
                    For Each drValor In dt.Select(filtro.ToString)
                        Dim valor As New Clases.ValorDivisa
                        valor.Importe = Util.AtribuirValorObj(drValor("NUM_IMPORTE"), GetType(Decimal))
                        divisa.ValoresTotalesDivisa.Add(valor)
                    Next

                    'filtro = New System.Text.StringBuilder
                    'filtro.AppendFormat("OID_DIVISA='{0}'", OID_DIVISA)
                    'filtro.AppendFormat(" AND COD_NIVEL_DETALLE ='{0}'", Enumeradores.TipoNivelDetalhe.Total.RecuperarValor())

                    ''Recupera os valores de total efectivo
                    'For Each drValor In dt.Select(filtro.ToString)
                    '    Dim valor As New Clases.ValorEfectivo
                    '    valor.Importe = Util.AtribuirValorObj(drValor("NUM_IMPORTE"), GetType(Decimal))
                    '    divisa.ValoresTotalesEfectivo.Add(valor)
                    'Next

                    divisas.Add(divisa)
                Next
            End If

            Return divisas
        End Function

        ''' <summary>
        ''' Exclui todos os efectivo do documento informado.
        ''' </summary>
        ''' <param name="identificadorDocumento">identificador do Documento</param>
        ''' <remarks></remarks>
        Public Shared Sub EfectivoDocumentoExcluir(identificadorDocumento As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.EfectivoDocumentoExcluir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub
    End Class

End Namespace
