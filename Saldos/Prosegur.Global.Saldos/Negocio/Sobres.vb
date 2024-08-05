Imports Prosegur.DbHelper
Imports Prosegur
Imports System.Data.SqlClient
Imports System.Collections.Generic

<Serializable()> _
Public Class Sobres
    Inherits List(Of Sobre)

#Region "[VARIÁVEIS]"

    Private _Documento As Documento

#End Region

#Region "[PROPRIEDADES]"

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

#End Region

#Region "[MÉTODOS]"

    Public Sub Realizar(Optional ByRef grado As Int16 = 2)

        Dim i As Integer = Nothing 'não é utilizada no codigo original
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.SobresRealizar.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, Me.Documento.Id))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count Then

            Dim objSobre As Negocio.Sobre = Nothing

            For Each dr As DataRow In dt.Rows

                objSobre = New Negocio.Sobre

                If dr("ConDiferencia") IsNot DBNull.Value Then
                    objSobre.ConDiferencia = Convert.ToBoolean(dr("ConDiferencia"))
                Else
                    objSobre.ConDiferencia = False
                End If

                If dr("IdMoneda") IsNot DBNull.Value Then
                    objSobre.Moneda.Id = Convert.ToInt32(dr("IdMoneda"))
                Else
                    objSobre.Moneda.Id = 0
                End If

                If dr("Importe") IsNot DBNull.Value Then
                    objSobre.Importe = Convert.ToDecimal(dr("Importe"))
                Else
                    objSobre.Importe = 0
                End If

                If dr("NumSobre") IsNot DBNull.Value Then
                    objSobre.NumSobre = dr("NumSobre")
                Else
                    objSobre.NumSobre = String.Empty
                End If

                Me.Add(objSobre)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Apaga todos os sobres de um documento
    ''' </summary>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    Public Sub BorrarSobres(IdDocumento As Integer, ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.DocumentoSobresBorrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, IdDocumento))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    ''' <summary>
    ''' Inserir todos os sobres de um documento
    ''' </summary>
    ''' <param name="IdDocumento"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    Public Sub SobresRegistrar(IdDocumento As Integer, ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = Nothing

        If (Me IsNot Nothing AndAlso Me.Count > 0) Then

            For Each objSobre In Me

                comando = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
                comando.CommandType = CommandType.Text
                comando.CommandText = My.Resources.DocumentosSobreRegistrar.ToString()

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, IdDocumento))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "NumSobre", ProsegurDbType.Descricao_Longa, objSobre.NumSobre))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConDiferencia", ProsegurDbType.Logico, objSobre.ConDiferencia))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMoneda", ProsegurDbType.Identificador_Numerico, objSobre.Moneda.Id))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Importe", ProsegurDbType.Numero_Decimal, objSobre.Importe))

                objTransacao.AdicionarItemTransacao(comando)

            Next

        End If

    End Sub

#End Region

End Class