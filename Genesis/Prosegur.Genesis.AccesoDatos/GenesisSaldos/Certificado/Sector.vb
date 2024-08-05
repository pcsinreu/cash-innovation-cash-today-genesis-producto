Imports Prosegur.DbHelper

' Alias
Imports CSCertificacion = Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion

Namespace GenesisSaldos.Certificacion

    ''' <summary>
    ''' Classe CertificadoSector
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Sector

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
        Public Shared Sub DeletarCertificadoSector(IdentificadorCertificado As String, ByRef ObjTransacion As Transacao)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CertificadoSectorDeletar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CERTIFICADO", ProsegurDbType.Objeto_Id, IdentificadorCertificado))

            ObjTransacion.AdicionarItemTransacao(cmd)
        End Sub

#End Region

    End Class

End Namespace