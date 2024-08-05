Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon

Namespace Genesis
    Public Class ConfigGrid
        ''' <summary>
        ''' Recupera as configurações de um GRID.
        ''' </summary>
        ''' <param name="CodigoUsuario"></param>
        ''' <param name="CodigoFuncionalidade"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarConfigGrid(CodigoUsuario As String, CodigoFuncionalidade As String) As ObservableCollection(Of Clases.ConfigGrid)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.Genesis_ConfigGrid_Recuperar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_USUARIO", ProsegurDbType.Descricao_Curta, CodigoUsuario.ToUpper))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_FUNCIONALIDAD", ProsegurDbType.Descricao_Curta, CodigoFuncionalidade))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
            Dim objConfigGrid As ObservableCollection(Of Clases.ConfigGrid) = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objConfigGrid = New ObservableCollection(Of Clases.ConfigGrid)()

                For Each dr In dt.Rows

                    objConfigGrid.Add(New Clases.ConfigGrid With {.ConfigXML = dr("DES_XML"),
                                                                  .CodigoControl = Util.AtribuirValorObj(dr("COD_CONTROL"), GetType(String)),
                                                                  .CodigoFuncionalidad = CodigoFuncionalidade})

                Next

            End If

            Return objConfigGrid
        End Function

        Public Shared Sub GuardarConfigGrid(CodigoUsuario As String, _
                                            CodigoFuncionalidad As String, _
                                            ConfigGrids As ObservableCollection(Of Clases.ConfigGrid))


            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            If (String.IsNullOrEmpty(CodigoFuncionalidad)) Then
                For Each ConfigGrid As Clases.ConfigGrid In ConfigGrids
                    Dim sqlDelete = My.Resources.Genesis_ConfigGrid_Borrar
                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, sqlDelete & " AND   COD_CONTROL = []COD_CONTROL")
                    cmd.CommandType = CommandType.Text

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario.ToUpper))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_FUNCIONALIDAD", ProsegurDbType.Identificador_Alfanumerico, ConfigGrid.CodigoFuncionalidad))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CONTROL", ProsegurDbType.Identificador_Alfanumerico, ConfigGrid.CodigoControl))

                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
                Next
            Else
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.Genesis_ConfigGrid_Borrar)
                cmd.CommandType = CommandType.Text

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario.ToUpper))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_FUNCIONALIDAD", ProsegurDbType.Identificador_Alfanumerico, CodigoFuncionalidad))

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            End If


            For Each ConfigGrid As Clases.ConfigGrid In ConfigGrids

                If ConfigGrid IsNot Nothing Then

                    cmd = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.Genesis_ConfigGrid_Grabar)
                    cmd.CommandType = CommandType.Text

                    Dim oidConfig As String = Guid.NewGuid.ToString

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIGURACION_GRID", ProsegurDbType.Objeto_Id, oidConfig))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario.ToUpper))
                    If (String.IsNullOrEmpty(CodigoFuncionalidad)) Then
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_FUNCIONALIDAD", ProsegurDbType.Identificador_Alfanumerico, ConfigGrid.CodigoFuncionalidad))
                    Else
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_FUNCIONALIDAD", ProsegurDbType.Identificador_Alfanumerico, CodigoFuncionalidad))
                    End If
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CONTROL", ProsegurDbType.Identificador_Alfanumerico, ConfigGrid.CodigoControl))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_XML", ProsegurDbType.Binario, ConfigGrid.ConfigXML))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_CREACION", ProsegurDbType.Data_Hora, DateTime.Now))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_MODIFICACION", ProsegurDbType.Data_Hora, DateTime.Now))

                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

                End If

            Next

            cmd.Dispose()
            cmd = Nothing

        End Sub

    End Class

End Namespace
