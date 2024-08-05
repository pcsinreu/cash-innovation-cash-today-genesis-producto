Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports System.Text

Namespace Genesis

    Public Class Cajero

        Private Const COL_COD_CAJERO As String = "COD_CAJERO"
        Private Const COL_OID_PTO_SERVICIO As String = "OID_PTO_SERVICIO"
        Private Const COL_COD_DELEGACION As String = "COD_DELEGACION"

        Public Shared Function EsDatosATMValidos(codigoCajero As String, identificadorPtoServicio As String, codigoDelegacion As String) As Boolean
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CajeroEsDatosATMValidos)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CAJERO", ProsegurDbType.Identificador_Alfanumerico, codigoCajero))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, identificadorPtoServicio))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))

            Dim qt As Integer = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)
            Return qt > 0
        End Function

    End Class

End Namespace
