Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.DataBaseHelper

Namespace Genesis
    Public Class ValorTerminoDocumento

        Shared Function ObtenerValorTerminoDocumento_v2(identificadoresDocumento As List(Of String)) As DataTable
            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.ValorTerminoDocumentoObtener_v2
                Dim filtro As String = ""

                If identificadoresDocumento IsNot Nothing Then
                    If identificadoresDocumento.Count = 1 Then
                        filtro &= " AND TD.OID_DOCUMENTO = []OID_DOCUMENTO "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Descricao_Curta, identificadoresDocumento(0)))
                    ElseIf identificadoresDocumento.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresDocumento, "OID_DOCUMENTO", cmd, "AND", "TD", , False)
                    End If
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function

        ''' <summary>
        ''' Insere o valor do termino para o documento.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub Inserir_v2(valores As ObservableCollection(Of Clases.Transferencias.ValorTerminoInserir))
            Try

                If valores IsNot Nothing AndAlso valores.Count > 0 Then

                    For Each valor In valores

                        Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoDocumentoInserir_v2)

                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_VALOR_TERMINOXDOCUMENTO", ProsegurDbType.Objeto_Id, System.Guid.NewGuid.ToString))
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, valor.identificadorDocumento))
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TERMINO", ProsegurDbType.Objeto_Id, valor.identificadorTermino))
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_VALOR", ProsegurDbType.Descricao_Longa, valor.valor))
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, valor.usuarioModificacion))
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, valor.usuarioModificacion))

                            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

                        End Using

                    Next valor

                End If

            Catch ex As Exception
                Throw

            End Try

        End Sub

        ''' <summary>
        ''' Insere o valor do termino para o documento.
        ''' </summary>
        ''' <param name="identificadorDocumento">identificador do documento.</param>
        ''' <param name="termino">Termino com o valor.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoDocumentoInserir(identificadorDocumento As String, termino As Clases.TerminoIAC, usuario As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)


            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoDocumentoInserir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_VALOR_TERMINOXDOCUMENTO", ProsegurDbType.Objeto_Id, System.Guid.NewGuid.ToString))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TERMINO", ProsegurDbType.Objeto_Id, termino.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_VALOR", ProsegurDbType.Descricao_Longa, termino.Valor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_MIGRACION", ProsegurDbType.Descricao_Longa, termino.CodigoMigracion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        Public Shared Sub ValorTerminoDocumentoInserir(identificadorDocumento As String, termino As Clases.TerminoIAC, usuario As String, ByRef transaccion As DataBaseHelper.Transaccion)

            Dim sql = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoDocumentoInserir)
            Dim sp As New SPWrapper(sql, True, CommandType.Text)

            sp.AgregarParam("OID_VALOR_TERMINOXDOCUMENTO", ProsegurDbType.Objeto_Id, System.Guid.NewGuid.ToString)
            sp.AgregarParam("OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento)
            sp.AgregarParam("OID_TERMINO", ProsegurDbType.Objeto_Id, termino.Identificador)
            sp.AgregarParam("DES_VALOR", ProsegurDbType.Descricao_Longa, termino.Valor)
            sp.AgregarParam("COD_MIGRACION", ProsegurDbType.Descricao_Longa, termino.CodigoMigracion)
            sp.AgregarParam("DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, usuario)
            sp.AgregarParam("DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario)

            AccesoDB.EjecutarSP(sp, Constantes.CONEXAO_GENESIS, False, transaccion)
        End Sub

        ''' <summary>
        ''' Exclui o valor do termino para o documento.
        ''' </summary>
        ''' <param name="identificadorDocumento">Identificador do documento que será utilizado na exclusão.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoDocumentoExcluir(identificadorDocumento As String)
            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoDocumentoExcluir)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            End Using
        End Sub

        Public Shared Sub ValorTerminoDocumentoExcluir(identificadorDocumento As String, ByRef transaccion As DataBaseHelper.Transaccion)
            Dim sql = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoDocumentoExcluir)
            Dim sp As New SPWrapper(sql, True, CommandType.Text)

            sp.AgregarParam("OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento)
            AccesoDB.EjecutarSP(sp, Constantes.CONEXAO_GENESIS, False, transaccion)

        End Sub

        ''' <summary>
        ''' Obtém os valores dos terminos para o documento.
        ''' </summary>
        ''' <param name="identificadorDocumento">Identificador do documento que será utilizado na exclusão.</param>
        ''' <remarks></remarks>
        Public Shared Function RecuperarValorTerminoDocumento(identificadorDocumento As String) As ObservableCollection(Of Clases.TerminoIAC)
            Dim valoresTerminos As New ObservableCollection(Of Clases.TerminoIAC)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerValorTerminoDocumento)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                For Each dr As DataRow In dt.Rows
                    valoresTerminos.Add(PopularTerminos(dr))
                Next
            End Using

            Return valoresTerminos
        End Function


        Private Shared Function PopularTerminos(row As DataRow) As Clases.TerminoIAC

            Dim objTermino As New Clases.TerminoIAC
            Util.AtribuirValorObjeto(Of String)(objTermino.Identificador, row("OID_TERMINO"))
            Util.AtribuirValorObjeto(Of String)(objTermino.Codigo, row("COD_TERMINO"))
            Util.AtribuirValorObjeto(Of String)(objTermino.Valor, row("DES_VALOR"))

            Return objTermino
        End Function

    End Class
End Namespace
