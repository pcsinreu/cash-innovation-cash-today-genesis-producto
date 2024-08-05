Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace NuevoSalidas
    Public Class ValorTerminoRemesa

        ''' <summary>
        ''' Insere o valor do termino para a remesa.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub Inserir(identificadorRemesa As String,
                                  identificadorTermino As String,
                                  codigoTermino As String,
                                  valor As String,
                                  fyhActualizacion As DateTime,
                                  codigoUsuario As String)
            Try
                Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.ValorTerminoRemesaInserir)

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_VALOR_TERMINOXREMESA", ProsegurDbType.Objeto_Id, System.Guid.NewGuid.ToString))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_TERMINO", ProsegurDbType.Objeto_Id, identificadorTermino))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_TERMINO", ProsegurDbType.Objeto_Id, codigoTermino))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "DES_VALOR", ProsegurDbType.Descricao_Longa, valor))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, fyhActualizacion))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Descricao_Longa, codigoUsuario))

                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

                End Using

            Catch ex As Exception
                Throw

            End Try

        End Sub

        Public Shared Sub Borrar(identificadorRemesa As String)
            Try
                Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.ValorTerminoRemesaBorrar)

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))

                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

                End Using

            Catch ex As Exception
                Throw

            End Try

        End Sub

        Public Shared Function Recuperar(identificadorRemesa As String) As List(Of Clases.TerminoIAC)
            Dim valoresTerminos As New List(Of Clases.TerminoIAC)
            Try
                Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.ValorTerminoRemesaRecuperar)

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))

                    Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

                    For Each dr As DataRow In dt.Rows
                        Dim objTermino As New Clases.TerminoIAC
                        Util.AtribuirValorObjeto(Of String)(objTermino.Identificador, dr("OID_TERMINO"))
                        Util.AtribuirValorObjeto(Of String)(objTermino.Codigo, dr("COD_TERMINO"))
                        Util.AtribuirValorObjeto(Of String)(objTermino.Valor, dr("DES_VALOR"))

                        valoresTerminos.Add(objTermino)
                    Next

                End Using

            Catch ex As Exception
                Throw

            End Try

            Return valoresTerminos
        End Function

    End Class
End Namespace
