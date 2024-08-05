Imports Prosegur.DbHelper

' Alias
Imports CSCertificacion = Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion

Namespace GenesisSaldos.Certificacion
    Public Class Filtros

        Public Shared Function RecuperarDetalles(IdentificadorCertificado As String, CodigoCertificado As String) As DataTable

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            comando.CommandText = String.Format(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RecuperarDetalles), If(String.IsNullOrEmpty(IdentificadorCertificado), "COD_CERTIFICADO", "OID_CERTIFICADO"))
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "VALOR", ProsegurDbType.Identificador_Alfanumerico, If(String.IsNullOrEmpty(IdentificadorCertificado), CodigoCertificado, IdentificadorCertificado)))

            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

        End Function

        Public Shared Function RecuperarDelegacionesPlantas(IdentificadorCertificado As String) As DataTable

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RecuperarDelegacionesPlantas)
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CERTIFICADO", ProsegurDbType.Identificador_Alfanumerico, IdentificadorCertificado))

            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

        End Function

        Public Shared Function RecuperarSectores(IdentificadorCertificado As String) As DataTable

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RecuperarSectores)
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CERTIFICADO", ProsegurDbType.Identificador_Alfanumerico, IdentificadorCertificado))

            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

        End Function

        Public Shared Function RecuperarSubCanales(IdentificadorCertificado As String) As DataTable

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RecuperarSubCanales)
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CERTIFICADO", ProsegurDbType.Identificador_Alfanumerico, IdentificadorCertificado))

            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

        End Function

    End Class

End Namespace