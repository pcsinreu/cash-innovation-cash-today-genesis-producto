Imports Prosegur.DbHelper

Public Class CalculoMedioPago

#Region "[CONSULTAS]"

#End Region

#Region "[INSERIR]"

#End Region

#Region "[UPDATE]"


#End Region

#Region "[DELETE]"
    Public Shared Function Borrar(oidMaquina As String, codigosEstadosPeriodos As List(Of String), objTransacao As Transacao, Optional quitarVigente As Boolean = True) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim qry = Util.PrepararQuery(My.Resources.BorrarCalculoMedioPago)
        Dim where As String = String.Empty
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MAQUINA", ProsegurDbType.Objeto_Id, oidMaquina))
        If Not quitarVigente Then
            where = " and peri.fyh_inicio > sysdate "
        End If

        If codigosEstadosPeriodos IsNot Nothing AndAlso codigosEstadosPeriodos.Count > 0 Then
            Dim i As Integer = 0
            Dim largo As Integer = codigosEstadosPeriodos.Count - 1
            where = String.Format("{0}{1}", where, " and TEPE.COD_ESTADO_PERIODO in (")
            For Each codigoEstadoPeriodo As String In codigosEstadosPeriodos
                If i = largo Then
                    where = String.Format("{0} '{1}'", where, codigoEstadoPeriodo)
                Else

                    where = String.Format("{0} '{1}',", where, codigoEstadoPeriodo)
                End If
                i += 1
            Next

            where = String.Format("{0}{1}", where, " )")

        End If
        comando.CommandText = String.Format(qry, where)

        If objTransacao IsNot Nothing Then
            objTransacao.AdicionarItemTransacao(comando)
        Else
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        End If

        Return oidMaquina

    End Function

#End Region

#Region "[DEMAIS]"

#End Region
End Class
