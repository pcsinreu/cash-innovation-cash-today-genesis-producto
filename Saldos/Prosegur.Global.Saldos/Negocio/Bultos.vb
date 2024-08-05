Imports Prosegur.DbHelper
Imports Prosegur
Imports System.Data.SqlClient
Imports System.Collections.Generic

<Serializable()> _
Public Class Bultos
    Inherits List(Of Bulto)

#Region "[VARIÁVEIS]"

    Private _Documento As Documento
    Private _Bultos As DSBultos

#End Region

#Region "[PROPRIEDADES]"

    Public Property Documento() As Documento
        Get
            If _Documento Is Nothing Then
                _Documento = New Documento()
            End If
            Return _Documento
        End Get
        Set(value As Documento)
            _Documento = value
        End Set
    End Property

#End Region

#Region "[METODOS]"

    Public Sub Realizar(Optional grado As Integer = 2)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.BultosRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, Me.Documento.Id))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        'Limpa a coleção antes de realizar nova consulta
        Me.Clear()

        If dt IsNot Nothing AndAlso dt.Rows.Count Then

            Dim objBulto As Negocio.Bulto = Nothing

            For Each dr As DataRow In dt.Rows

                objBulto = New Negocio.Bulto

                objBulto.NumPrecinto = dr("NumPrecinto")
                objBulto.CodBolsa = dr("CodBolsa")

                If grado = 2 Then

                    If dr("IdDestino") Is DBNull.Value Then
                        objBulto.Destino.Id = 0
                    Else
                        objBulto.Destino.Id = dr("IdDestino")
                    End If

                End If

                Me.Add(objBulto)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Apaga todos os bultos de um documento
    ''' </summary>
    ''' <param name="IdDocumento"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    Public Sub BorrarBultos(IdDocumento As Integer, ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.DocumentoBultosBorrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Inteiro_Longo, IdDocumento))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    ''' <summary>
    ''' Grava todos os bultos de um documento
    ''' </summary>
    ''' <param name="IdDocumento"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    Public Sub BultosRegistrar(IdDocumento As Integer, ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = Nothing

        If (Me IsNot Nothing AndAlso Me.Count > 0) Then

            For Each objBulto In Me

                If objBulto.NumPrecinto.Trim.Length <> 0 Then

                    comando = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
                    comando.CommandType = CommandType.Text
                    comando.CommandText = My.Resources.DocumentoBultosRegistrar.ToString()

                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Descricao_Longa, IdDocumento))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "NumPrecinto", ProsegurDbType.Descricao_Longa, objBulto.NumPrecinto))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodBolsa", ProsegurDbType.Descricao_Longa, objBulto.CodBolsa))

                    If objBulto.Destino.Id <> 0 Then
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDestino", ProsegurDbType.Inteiro_Longo, objBulto.Destino.Id))
                    Else
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDestino", ProsegurDbType.Inteiro_Longo, DBNull.Value))
                    End If

                    objTransacao.AdicionarItemTransacao(comando)

                End If

            Next

        End If

    End Sub

#End Region

End Class