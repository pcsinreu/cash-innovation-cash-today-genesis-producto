Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel

Namespace GenesisSaldos
    Public Class EfectivoAnteriorDocumento

        Public Shared Sub Inserir(identificadorDocumento As String, _
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

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.EfectivoAnteriorDocumentoInserir)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_EFECTIVO_ANTERIORXDOC", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DENOMINACION", ProsegurDbType.Objeto_Id, identificadorDenominacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Objeto_Id, identificadorDivisa))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_UNIDAD_MEDIDA", ProsegurDbType.Objeto_Id, identificadorUnidadMedida))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CALIDAD", ProsegurDbType.Descricao_Curta, identificadorCalidad))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_NIVEL_DETALLE", ProsegurDbType.Descricao_Curta, nivelDetalhe))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_EFECTIVO_TOTAL", ProsegurDbType.Descricao_Curta, tipoEfectivoTotal))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NUM_IMPORTE", ProsegurDbType.Numero_Decimal, numImporte))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_CANTIDAD", ProsegurDbType.Numero_Decimal, cantidad))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, Usuario))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, Usuario))
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            End If
        End Sub

        Public Shared Function RecuperarPorDocumento(identificadorDocumento As String) As List(Of Clases.Divisa)
            Dim divisas As List(Of Clases.Divisa) = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.EfectivoAnteriorDocumentoRecuperarPorDoc)
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

                    divisas.Add(divisa)
                Next
            End If

            Return divisas
        End Function

        Public Shared Sub Excluir(identificadorDocumento As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.EfectivoAnteriorDocumentoExcluir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub
    End Class
End Namespace

