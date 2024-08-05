Imports Prosegur.DbHelper
Imports System.Collections.Generic

<Serializable()> _
Public Class Detalles
    Inherits List(Of Detalle)

#Region "[VARIÁVEIS]"

    Private _Documento As Documento
    Private _Totales As Totales
    Private _Moneda As Moneda = New Moneda()

#End Region

#Region "[PROPRIEDADES]"

    Public Property Totales() As Totales
        Get
            If _Totales Is Nothing Then
                _Totales = New Totales()
            End If
            Return _Totales
        End Get
        Set(Value As Totales)
            _Totales = Value
        End Set
    End Property

    Public Property Documento() As Documento
        Get
            If _Documento Is Nothing Then
                _Documento = New Documento()
            End If
            Return _Documento
        End Get
        Set(Value As Documento)
            _Documento = Value
        End Set
    End Property

    Public Property Moneda() As Moneda
        Get
            Return _Moneda
        End Get
        Set(value As Moneda)
            _Moneda = value
        End Set
    End Property


#End Region

#Region "[MÉTODOS]"

    Public Sub Realizar()

        Dim objDetalle As Detalle = Nothing
        Dim objEspecie As Especie = Nothing
        Dim objMoneda As Moneda = Nothing

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DocumentoDetalleRealizar.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, Me.Documento.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMoneda", ProsegurDbType.Identificador_Numerico, If(Me.Moneda.Id = 0, DBNull.Value, Me.Moneda.Id)))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count Then

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                ' popula a classe especie
                objEspecie = New Especie()
                objEspecie.Id = dr("IdEspecie")

                If dr("DescEspecie") IsNot DBNull.Value Then
                    objEspecie.Descripcion = dr("DescEspecie")
                Else
                    objEspecie.Descripcion = String.Empty
                End If

                If dr("Uniforme") IsNot DBNull.Value Then
                    objEspecie.Uniforme = Convert.ToBoolean(dr("Uniforme"))
                Else
                    objEspecie.Uniforme = False
                End If

                If dr("Calidad") IsNot DBNull.Value Then
                    objEspecie.Calidad = dr("Calidad")
                Else
                    objEspecie.Calidad = String.Empty
                End If

                objEspecie.Moneda.Id = dr("IdMoneda")

                If dr("DescMoneda") IsNot DBNull.Value Then
                    objEspecie.Moneda.Descripcion = dr("DescMoneda")
                Else
                    objEspecie.Moneda.Descripcion = String.Empty
                End If

                If dr("SimboloMoneda") IsNot DBNull.Value Then
                    objEspecie.Moneda.Simbolo = dr("SimboloMoneda")
                Else
                    objEspecie.Moneda.Simbolo = String.Empty
                End If

                ' popula a classe detalle
                objDetalle = New Detalle()
                objDetalle.Especie = objEspecie

                If dr("Cantidad") IsNot DBNull.Value Then
                    objDetalle.Cantidad = dr("Cantidad")
                Else
                    objDetalle.Cantidad = 0
                End If

                If dr("Importe") IsNot DBNull.Value Then
                    objDetalle.Importe = dr("Importe")
                Else
                    objDetalle.Importe = 0
                End If

                ' Adiciona detalhe na coleção
                Me.Add(objDetalle)

                ' criar novo objeto total
                Dim objTotal As Negocio.Total = Nothing

                ' se existir algum objeto na coleção de totales
                If Me.Totales IsNot Nothing AndAlso Me.Totales.Count > 0 Then

                    ' obter o total da moeda em questão
                    Dim result = From col In Me.Totales _
                                 Where col.Moneda.Id = objEspecie.Moneda.Id

                    If result IsNot Nothing AndAlso result.Count > 0 Then
                        objTotal = result(0)
                        ' atualizar informações
                        objTotal.Importe += objDetalle.Importe
                        objTotal.HayNoUniformes = Not objEspecie.Uniforme
                        objTotal.HayUniformes = objEspecie.Uniforme
                    End If

                End If

                ' se o objeto total estiver nothing
                If objTotal Is Nothing Then

                    ' criar objeto, popular e adicionar para coleção
                    objTotal = New Negocio.Total
                    objTotal.Importe = 0
                    objTotal.Moneda = objEspecie.Moneda
                    objTotal.HayNoUniformes = False
                    objTotal.HayUniformes = False

                    objTotal.Importe += objDetalle.Importe
                    objTotal.HayNoUniformes = Not objEspecie.Uniforme
                    objTotal.HayUniformes = objEspecie.Uniforme

                    Me.Totales.Add(objTotal)

                End If

            Next

        End If

    End Sub

    Public Sub RealizarSimplificado()

        Dim objTotal As Negocio.Total = Nothing

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DocumentoDetalleRealizarSimplificado.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, Me.Documento.Id))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        'Limpa a coleção antes de realizar nova consulta
        Me.Clear()

        If dt IsNot Nothing AndAlso dt.Rows.Count Then

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                ' popula a classe detalle
                objTotal = New Negocio.Total

                ' criar novo objeto total
                objTotal = New Negocio.Total
                objTotal.Moneda.Id = dr("IdMoneda")
                objTotal.Moneda.Simbolo = dr("SimboloMoneda")
                objTotal.Importe = dr("Importe")

                Me.Totales.Add(objTotal)

            Next

        End If

    End Sub

    Public Sub GravarDetalles(IdDocumento As Integer, ByRef objTransacao As Transacao)

        If Me IsNot Nothing AndAlso Me.Count > 0 Then

            Dim comando As IDbCommand = Nothing

            For Each Detalle In Me

                comando = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
                comando.CommandType = CommandType.Text
                comando.CommandText = My.Resources.DocumentoDetalleRegistrar.ToString()

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, IdDocumento))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Importe", ProsegurDbType.Numero_Decimal, Detalle.Importe))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Cantidad", ProsegurDbType.Inteiro_Longo, Detalle.Cantidad))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdEspecie", ProsegurDbType.Identificador_Numerico, Detalle.Especie.Id))

                objTransacao.AdicionarItemTransacao(comando)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Apaga todos os detalles do documento
    ''' </summary>
    ''' <param name="IdDocumento"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    Public Sub BorrarDetalles(IdDocumento As Integer, ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.DocumentoDetalleBorrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Inteiro_Longo, IdDocumento))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

#End Region

End Class