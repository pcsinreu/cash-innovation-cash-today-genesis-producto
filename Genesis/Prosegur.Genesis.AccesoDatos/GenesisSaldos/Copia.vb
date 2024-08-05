Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon

Namespace GenesisSaldos

    Public Class Copia
        ''' <summary>
        ''' Método que insere as cópias para um formulario
        ''' </summary>
        ''' <param name="copias">Cópias</param>
        ''' <remarks></remarks>
        Public Shared Sub GuardarCopiasFormulario(identificadorFormulario As String, copias As List(Of Clases.Copia))

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CopiaInserirPorFormulario)
            cmd.CommandType = CommandType.Text

            For Each copia In copias

                cmd.Parameters.Clear()

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_COPIA", ProsegurDbType.Objeto_Id, copia.Identificador))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, identificadorFormulario))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_DESTINO_COPIA", ProsegurDbType.Descricao_Longa, copia.Destino))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_CANTIDAD_COPIA", ProsegurDbType.Inteiro_Longo, copia.Cantidad))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_MIGRACION", ProsegurDbType.Descricao_Longa, copia.CodigoMigracion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, copia.UsuarioCreacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, copia.UsuarioModificacion))

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

            Next

        End Sub
        ''' <summary>
        ''' Método que exclui as cópias de um formulario
        ''' </summary>
        ''' <param name="identificadorFormulario">identificadorFormulario</param>
        ''' <remarks></remarks>
        Public Shared Sub BorrarCopiasFormulario(identificadorFormulario As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CopiaExcluirPorFormulario)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, identificadorFormulario))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        End Sub
        ''' <summary>
        ''' Recupera todos as cópias de um formulário
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperaCopias(identidicadorFormulario) As List(Of Clases.Copia)

            Dim listaCopias As New List(Of Clases.Copia)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim dt As DataTable

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, identidicadorFormulario))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CopiaRecuperarCopiaFormulario)
            cmd.CommandType = CommandType.Text

            dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row In dt.Rows
                    Dim copia As New Clases.Copia
                    With copia
                        .Identificador = Util.AtribuirValorObj(row("OID_COPIA"), GetType(String))
                        .IdentificadorFormulario = Util.AtribuirValorObj(row("OID_FORMULARIO"), GetType(String))
                        .Destino = Util.AtribuirValorObj(row("DES_DESTINO_COPIA"), GetType(String))
                        .Cantidad = Util.AtribuirValorObj(row("NEL_CANTIDAD_COPIA"), GetType(Integer))
                        .CodigoMigracion = Util.AtribuirValorObj(row("COD_MIGRACION"), GetType(String))
                        .FechaHoraCreacion = Util.AtribuirValorObj(row("GMT_CREACION"), GetType(DateTime))
                        .UsuarioCreacion = Util.AtribuirValorObj(row("DES_USUARIO_CREACION"), GetType(String))
                        .FechaHoraModificacion = Util.AtribuirValorObj(row("GMT_MODIFICACION"), GetType(DateTime))
                        .UsuarioModificacion = Util.AtribuirValorObj(row("DES_USUARIO_MODIFICACION"), GetType(String))
                    End With
                    listaCopias.Add(copia)
                Next
            End If

            Return listaCopias
        End Function
    End Class

End Namespace
