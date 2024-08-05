Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace GenesisSaldos
    Public Class MedioPagoAnteriorDocumento
        Public Shared Function Inserir(identificadorDocumento As String, _
                                                    identificadorDivisa As String, _
                                                    identificadorMedioPago As String, _
                                                    nivelDetalhe As String, _
                                                    tipoMedioPago As String, _
                                                    cantidad As Decimal, _
                                                    numImporte As Decimal, _
                                                    usuario As String) As String

            Dim identificadorMedioPagoDocumento As String = String.Empty
            If numImporte <> 0 OrElse cantidad <> 0 Then
                identificadorMedioPagoDocumento = Guid.NewGuid.ToString
                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.MedioPagoAnteriorDocumentoInserir)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_MEDIO_PAGO_ANTERIORXDOC", ProsegurDbType.Objeto_Id, identificadorMedioPagoDocumento))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Objeto_Id, identificadorDivisa))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, identificadorMedioPago))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_MEDIO_PAGO", ProsegurDbType.Descricao_Curta, tipoMedioPago))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_UNIDAD_MEDIDA", ProsegurDbType.Objeto_Id, Nothing))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_NIVEL_DETALLE", ProsegurDbType.Descricao_Curta, nivelDetalhe))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NUM_IMPORTE", ProsegurDbType.Numero_Decimal, numImporte))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_CANTIDAD", ProsegurDbType.Numero_Decimal, cantidad))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, usuario))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            End If

            Return identificadorMedioPagoDocumento
        End Function

        Public Shared Function RecuperarPorDocumento(identificadorDocumento As String) As List(Of Clases.Divisa)
            Dim divisas As List(Of Clases.Divisa) = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.MedioPagoAnteriorDocumentoRecuperarPorDoc)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                'Recupera os valores do medio pago
                divisas = New List(Of Clases.Divisa)

                For Each OID_DIVISA In (From r In dt Select r.Field(Of String)("OID_DIVISA")).Distinct()
                    Dim divisa As New Clases.Divisa
                    Dim filtro As New System.Text.StringBuilder
                    divisa.Identificador = OID_DIVISA

                    'Mediopago
                    divisa.MediosPago = New ObservableCollection(Of Clases.MedioPago)

                    'Totais de Mediopago
                    divisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)

                    ' Recupera os registros detalhados
                    'For Each drMedioPago In dt.Select(filtro.ToString())
                    For Each OID_MEDIO_PAGO In (From r In dt.AsEnumerable().Where(
                                                Function(r) r.Field(Of String)("OID_DIVISA") = divisa.Identificador _
                                                AndAlso r.Field(Of String)("COD_NIVEL_DETALLE") = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor())
                                                Select r.Field(Of String)("OID_MEDIO_PAGO")).Distinct()

                        Dim medioPago As New Clases.MedioPago
                        medioPago.Valores = New ObservableCollection(Of Clases.ValorMedioPago)
                        medioPago.Identificador = OID_MEDIO_PAGO


                        filtro = New System.Text.StringBuilder
                        filtro.AppendFormat("OID_DIVISA='{0}'", divisa.Identificador)
                        filtro.AppendFormat(" AND COD_NIVEL_DETALLE ='{0}'", Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor())
                        filtro.AppendFormat(" AND OID_MEDIO_PAGO='{0}'", OID_MEDIO_PAGO)

                        'Recupera o valor para cada medio pago
                        For Each drValor In dt.Select(filtro.ToString())
                            Dim valor As New Clases.ValorMedioPago
                            valor.Cantidad = Util.AtribuirValorObj(drValor("NEL_CANTIDAD"), GetType(Decimal))
                            valor.Importe = Util.AtribuirValorObj(drValor("NUM_IMPORTE"), GetType(Decimal))
                            medioPago.Tipo = RecuperarEnum(Of Enumeradores.TipoMedioPago)(drValor("COD_TIPO_MEDIO_PAGO"))
                            medioPago.Valores.Add(valor)
                        Next

                        divisa.MediosPago.Add(medioPago)
                    Next

                    filtro = New System.Text.StringBuilder
                    filtro.AppendFormat("OID_DIVISA='{0}'", OID_DIVISA)
                    filtro.AppendFormat(" AND COD_NIVEL_DETALLE ='{0}'", Enumeradores.TipoNivelDetalhe.Total.RecuperarValor())

                    'Recupera os valores de total
                    For Each drValor In dt.Select(filtro.ToString)
                        Dim valor As New Clases.ValorTipoMedioPago
                        valor.TipoMedioPago = RecuperarEnum(Of Enumeradores.TipoMedioPago)(drValor("COD_TIPO_MEDIO_PAGO"))
                        valor.Cantidad = Util.AtribuirValorObj(drValor("NEL_CANTIDAD"), GetType(Decimal))
                        valor.Importe = Util.AtribuirValorObj(drValor("NUM_IMPORTE"), GetType(Decimal))
                        divisa.ValoresTotalesTipoMedioPago.Add(valor)
                    Next

                    divisas.Add(divisa)
                Next
            End If

            Return divisas
        End Function

        Public Shared Sub Excluir(identificadorDocumento As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.MedioPagoAnteriorDocumentoExcluir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub
    End Class

End Namespace
