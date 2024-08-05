Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon

Namespace Genesis

    Public Class TipoCliente

        Public Shared Function RecuperarTipoCliente(CodigoTipoCliente As String) As Clases.TipoCliente

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TipoClienteRecuperar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodigoTipoCliente))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
            Dim TipoCliente As Clases.TipoCliente = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                TipoCliente = New Clases.TipoCliente

                With TipoCliente
                    .Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_TIPO_CLIENTE"), GetType(String))
                    .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_TIPO_CLIENTE"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_TIPO_CLIENTE"), GetType(String))
                    .EstaActivo = Util.AtribuirValorObj(dt.Rows(0)("BOL_ACTIVO"), GetType(Boolean))
                End With
            End If

            Return TipoCliente
        End Function


    End Class

End Namespace