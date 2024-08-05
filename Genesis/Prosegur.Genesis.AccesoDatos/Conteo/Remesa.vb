Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Conteo

    Public Class Remesa

        Public Shared Function RecuperarDatosParcializacionErronea(cantidadDias As Integer) As DataTable

            Dim comando As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_CONTEO)
            comando.CommandType = CommandType.Text
            comando.CommandText = My.Resources.ConteoRecuperarDatosParcializacionErronea

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "CANTIDAD_DIAS_BUSCAR_DATOS", ProsegurDbType.Inteiro_Curto, cantidadDias))

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_CONTEO, comando.CommandText)

            Return AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_CONTEO, comando)

        End Function

    End Class

End Namespace