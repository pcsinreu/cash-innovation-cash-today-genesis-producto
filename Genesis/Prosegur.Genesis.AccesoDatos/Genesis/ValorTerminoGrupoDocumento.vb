Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon

Namespace Genesis
    Public Class ValorTerminoGrupoDocumento

#Region "[INSERIR]"

        ''' <summary>
        ''' Insere o valor do termino para o grupo de documento.
        ''' </summary>
        ''' <param name="identificadorGrupoDocumento">identificador do grupo de documento.</param>
        ''' <param name="termino">Termino com o valor.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoGrupoDocumentoInserir(identificadorGrupoDocumento As String, termino As Clases.TerminoIAC, usuario As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoGrupoDocumentoInserir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_VALOR_TERMINOXGRUPODOC", ProsegurDbType.Objeto_Id, System.Guid.NewGuid.ToString))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_GRUPO_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorGrupoDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TERMINO", ProsegurDbType.Objeto_Id, termino.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_MIGRACION", ProsegurDbType.Identificador_Alfanumerico, termino.CodigoMigracion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_VALOR", ProsegurDbType.Descricao_Longa, termino.Valor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

#End Region

#Region "[DELETAR]"

        ''' <summary>
        ''' Exclui o valor do termino para o grupo de documento.
        ''' </summary>
        ''' <param name="identificadorGrupoDocumento">Identificador do grupo de documento que será utilizado na exclusão.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoGrupoDocumentoExcluir(identificadorGrupoDocumento As String)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoGrupoDocumentoExcluir)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_GRUPO_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorGrupoDocumento))

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            End Using

        End Sub

#End Region

#Region "[CONSULTAR]"

        ''' <summary>
        ''' Recupera os terminos do documento.
        ''' </summary>
        ''' <param name="identificadorGrupoDocumento">identificador do grupo de documento.</param>
        ''' <remarks></remarks>
        Public Shared Function ValorTerminoGrupoDocumentoRecuperar(identificadorGrupoDocumento As String) As List(Of Clases.TerminoIAC)
            Dim listaTerminos As List(Of Clases.TerminoIAC) = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoGrupoDocumentoRecuperar)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_GRUPO_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorGrupoDocumento))
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                listaTerminos = New List(Of Clases.TerminoIAC)
                For Each dr In dt.Rows
                    Dim termino = New Clases.TerminoIAC
                    With termino
                        .Identificador = Util.AtribuirValorObj(dr("OID_TERMINO"), GetType(String))
                        .Valor = Util.AtribuirValorObj(dr("DES_VALOR"), GetType(String))
                    End With

                    listaTerminos.Add(termino)
                Next

            End If

            Return listaTerminos
        End Function

#End Region
    End Class
End Namespace

