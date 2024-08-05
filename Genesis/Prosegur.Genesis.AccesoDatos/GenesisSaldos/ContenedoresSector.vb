Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Framework.Dicionario
Imports System.Transactions
Namespace GenesisSaldos

    ''' <summary>
    ''' Classe ContenedoresSector
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ContenedoresSector

#Region "[CONSULTAR]"
        Public Shared Function ConsultarContenedoresSector(Peticion As Contenedores.ConsultarContenedoresSector.Peticion)
            'Cria comando SQL
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With comando
                .CommandType = CommandType.StoredProcedure
                .CommandText = String.Format("UTIL_{0}.FN_CONSULTARCONTENEDORESSECTOR", Prosegur.Genesis.Comon.Util.Version)

                'Cria o parâmetro para a consulta
                ' .Parameters.Add(Util.CriarParametroOracle("RESULT", ParameterDirection.ReturnValue, DBNull.Value, OracleClient.OracleType.Number))
                ' .Parameters.Add(Util.CriarParametroOracle("P_COD_PREFIJO", ParameterDirection.Input, PrefixoReciboRemesa, OracleClient.OracleType.VarChar))
                ' .Parameters.Add(Util.CriarParametroOracle("P_COD_DELEGACION", ParameterDirection.Input, codigoDelegacion, OracleClient.OracleType.VarChar))
            End With

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, comando)

            If comando IsNot Nothing AndAlso comando.Parameters IsNot Nothing AndAlso comando.Parameters("RESULT") IsNot Nothing Then
                'Resgata o valor do parâmetro de saída
                Return comando.Parameters("RESULT").Value.ToString()
            Else
                Return String.Empty
            End If
        End Function
#End Region


    End Class

End Namespace