Imports System.Data


Public Class Util

    ''' <summary>
    ''' Trata a exception
    ''' </summary>
    ''' <param name="ObjExcecao"></param>
    ''' <param name="MsgErro"></param>
    ''' <remarks></remarks>
    Public Shared Sub Tratar(ObjExcecao As Exception, MsgErro As String)

        If TypeOf (ObjExcecao) Is OracleClient.OracleException Then

            ' O erro é do Oracle
            Select Case DirectCast(ObjExcecao, OracleClient.OracleException).Code

                Case 1 ' Erro de campo chave em duplicidade

                    Throw New NegocioExcepcion(Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, MsgErro)

                    Return

            End Select

        ElseIf TypeOf (ObjExcecao) Is SqlClient.SqlException Then

            ' O erro é do SQL Server
            Select Case DirectCast(ObjExcecao, SqlClient.SqlException).Number

                Case 547 ' Erro de Constraint de FK

                    Throw New NegocioExcepcion(Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, MsgErro)

                    Return

            End Select

        End If

        ' Se chegou até aqui é porque não é nenhum dos erros "Tratáveis".
        ' Simplesmente retorna o erro
        Throw ObjExcecao

    End Sub

End Class