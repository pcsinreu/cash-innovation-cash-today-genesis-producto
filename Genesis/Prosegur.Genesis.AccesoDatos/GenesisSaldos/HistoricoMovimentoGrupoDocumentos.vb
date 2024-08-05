Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel

Namespace GenesisSaldos

    ''' <summary>
    ''' Classe HistoricoGrupoDocumentos
    ''' </summary>
    ''' <remarks></remarks>
    Public Class HistoricoMovimentoGrupoDocumentos

#Region "[DELETAR]"

        ''' <summary>
        ''' Deleta o historico do grupo de documentos
        ''' </summary>
        Public Shared Sub DeletarHistorico(estado As Enumeradores.EstadoDocumento, _
                                           IdentificadorGrupoDocumentos As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.HistoricoMovimentoGrupoDocumentosDeletar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_GRUPO_DOCUMENTO", ProsegurDbType.Objeto_Id, IdentificadorGrupoDocumentos))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, estado.RecuperarValor()))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

#End Region

#Region "[INSERIR]"

        ''' <summary>
        ''' Insere o grupo de documentos
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub InserirHistorico(estado As Enumeradores.EstadoDocumento, _
                                           IdentificadorGrupoDocumentos As String,
                                           usuario As String)

            DeletarHistorico(estado, IdentificadorGrupoDocumentos)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.HistoricoMovimentoGrupoDocumentosInserir)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_HIST_MOV_GRUPO_DOCUMENTO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_GRUPO_DOCUMENTO", ProsegurDbType.Objeto_Id, IdentificadorGrupoDocumentos))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, estado.RecuperarValor()))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, usuario))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

#End Region

#Region "[CONSULTA]"

        ''' <summary>
        ''' Busca historico do grupo de documentos
        ''' </summary>
        ''' <param name="IdentificadorGrupoDocumentos"></param>
        ''' <remarks></remarks>
        Public Shared Function RecuperarHistorico(IdentificadorGrupoDocumentos As String) As ObservableCollection(Of Clases.HistoricoMovimientoDocumento)

            Dim objHistorico As New ObservableCollection(Of Clases.HistoricoMovimientoDocumento)

            If Not String.IsNullOrEmpty(IdentificadorGrupoDocumentos) Then
                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.HistoricoMovimentoGrupoDocumentosSelect)
                cmd.CommandType = CommandType.Text

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_GRUPO_DOCUMENTO", ProsegurDbType.Objeto_Id, IdentificadorGrupoDocumentos))

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    objHistorico = New ObservableCollection(Of Clases.HistoricoMovimientoDocumento)

                    For Each dr In dt.Rows

                        objHistorico.Add(New Clases.HistoricoMovimientoDocumento With { _
                                         .Estado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoDocumento)(Util.AtribuirValorObj(dr("COD_ESTADO"), GetType(String))), _
                                         .FechaHoraCreacion = Util.AtribuirValorObj(dr("GMT_CREACION"), GetType(DateTime)), _
                                         .FechaHoraModificacion = Util.AtribuirValorObj(dr("GMT_MODIFICACION"), GetType(DateTime)), _
                                         .UsuarioCreacion = Util.AtribuirValorObj(dr("DES_USUARIO_CREACION"), GetType(String)), _
                                         .UsuarioModificacion = Util.AtribuirValorObj(dr("DES_USUARIO_MODIFICACION"), GetType(String))})

                    Next

                End If
            End If

            Return objHistorico
        End Function

#End Region

    End Class

End Namespace