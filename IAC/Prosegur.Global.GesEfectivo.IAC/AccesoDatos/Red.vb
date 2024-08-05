Imports Prosegur.DBHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor

''' <summary>
''' Classe red
''' </summary>
''' <remarks></remarks>
Public Class Red

#Region "[CONSULTAS]"

    ''' <summary>
    ''' Recupera todas las redes existentes.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 12/01/2011 Criado
    ''' </history>
    Public Shared Function GetComboRedes() As DataTable

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetComboRedes.ToString())

        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

    End Function

#End Region

#Region "[INSERIR]"

#End Region

#Region "[ATUALIZAR]"

#End Region

#Region "[FILTROS]"

#End Region

End Class