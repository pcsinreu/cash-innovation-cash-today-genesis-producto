Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.Framework.Dicionario.Tradutor

Namespace Genesis

    Public Class Puesto


        ''' <summary>
        ''' Obter postos por delegação.
        ''' </summary>
        ''' <param name="codigoDelegacion">Código da Delegação</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPuestos(CodigoDelegacion As String,
                                              BolVigente As Boolean?, _
                                              CodigoAplicacion As String) As ObservableCollection(Of Clases.Puesto)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim SQLCondiciones As New System.Text.StringBuilder

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.Genesis_Puesto_ObtenerPuestos)
            cmd.CommandType = CommandType.Text

            If Not String.IsNullOrEmpty(CodigoDelegacion) Then
                SQLCondiciones.AppendLine(" AND D.COD_DELEGACION = []COD_DELEGACION")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDelegacion))
            End If

            If BolVigente IsNot Nothing Then
                SQLCondiciones.AppendLine(" AND PUE.BOL_VIGENTE = []BOL_VIGENTE")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_VIGENTE", ProsegurDbType.Logico, BolVigente))
            End If

            SQLCondiciones.AppendLine(" ORDER BY PUE.COD_PUESTO")

            If SQLCondiciones.Length > 0 Then
                cmd.CommandText &= SQLCondiciones.ToString
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, CodigoAplicacion))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

            Return CargarPuestos(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd))

        End Function


        Private Shared Function CargarPuestos(dtRetorno As DataTable) As ObservableCollection(Of Clases.Puesto)

            ' Variável que recebe os postos
            Dim lstPuestos As ObservableCollection(Of Clases.Puesto) = Nothing

            ' Verifica se retornou algum dado
            If dtRetorno IsNot Nothing Then

                ' Cria uma nova instancia para a lista de postos
                lstPuestos = New ObservableCollection(Of Clases.Puesto)

                ' Para cada posto retornado
                For Each dr As DataRow In dtRetorno.Rows

                    ' Adiciona o posto
                    lstPuestos.Add(New Clases.Puesto With
                                   {
                                       .Codigo = Util.AtribuirValorObj(dr("COD_PUESTO"), GetType(String)), _
                                       .Activo = Util.AtribuirValorObj(dr("BOL_VIGENTE"), GetType(Boolean)), _
                                       .Identificador = Util.AtribuirValorObj(dr("OID_PUESTO"), GetType(String))
                                   })
                Next dr

            End If

            ' Retorna as situações das remesas
            Return lstPuestos

        End Function

    End Class

End Namespace