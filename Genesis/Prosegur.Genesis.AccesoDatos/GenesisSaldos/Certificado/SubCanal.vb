Imports Prosegur.DbHelper

Namespace GenesisSaldos.Certificacion

    Public Class SubCanal

#Region "[DELETAR]"

        ''' <summary>
        ''' Deleta as delegações do certificado
        ''' </summary>
        ''' <param name="IdentificadorCertificado"></param>
        ''' <param name="ObjTransacion"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 05/06/2013 Criado
        ''' </history>
        Public Shared Sub DeletarCertificadoSubCanal(IdentificadorCertificado As String, ByRef ObjTransacion As Transacao)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CertificadoSubCanalDeletar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CERTIFICADO", ProsegurDbType.Objeto_Id, IdentificadorCertificado))

            ObjTransacion.AdicionarItemTransacao(cmd)
        End Sub

#End Region

    End Class

End Namespace