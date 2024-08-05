Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos
Imports Prosegur.Genesis.Comon

Namespace Genesis
    Public Class Log

        Public Shared Sub GuardarLogExecucao(Erro As String, _
                                             Usuario As String, Delegacion As String,
                                             Aplicacion As Enumeradores.Aplicacion,
                                    Optional Puesto As String = Nothing)

            Dim comando As IDbCommand = Nothing
            Dim conexion As String = String.Empty

            Select Case Aplicacion

                Case Enumeradores.Aplicacion.GenesisNuevoSaldos

                    conexion = Constantes.CONEXAO_GENESIS

                    comando = AcessoDados.CriarComando(conexion)
                    comando.CommandText = My.Resources.GuardarLogExecucaoGenesisSaldos.ToString

                Case Enumeradores.Aplicacion.GenesisATM

                    conexion = Constantes.CONEXAO_ATM

                    comando = AcessoDados.CriarComando(conexion)
                    comando.CommandText = My.Resources.GuardarLogExecucaoGenesisATM.ToString

                Case Enumeradores.Aplicacion.GenesisSalidas

                    conexion = Constantes.CONEXAO_SALIDAS

                    comando = AcessoDados.CriarComando(conexion)
                    comando.CommandText = My.Resources.GuardarLogExecucaoGenesisSalidas.ToString
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_PUESTO", ProsegurDbType.Descricao_Curta, Puesto))

                Case Else
                    Exit Sub

            End Select

            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(conexion, "OID_LOG_ERROR", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(conexion, "DES_ERROR", ProsegurDbType.Descricao_Longa, Erro))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(conexion, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, Usuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(conexion, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, Delegacion))

            AcessoDados.ExecutarNonQuery(conexion, comando)

        End Sub

    End Class

End Namespace
