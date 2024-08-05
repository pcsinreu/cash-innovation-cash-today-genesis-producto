Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Genesis

    Public Class Calidad

        Shared Function ObtenerCalidadPorDivisa_v2(codigosCalidad As List(Of String), identificadoresCalidad As List(Of String)) As DataTable
            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.ObtenerCalidad_v2
                Dim filtro As String = ""

                If codigosCalidad IsNot Nothing Then
                    If codigosCalidad.Count = 1 Then
                        filtro &= " AND C.COD_CALIDAD = []COD_CALIDAD "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CALIDAD", ProsegurDbType.Descricao_Curta, codigosCalidad(0)))
                    ElseIf codigosCalidad.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosCalidad, "COD_CALIDAD", cmd, "AND", "C", , False)
                    End If
                End If

                If identificadoresCalidad IsNot Nothing Then
                    If identificadoresCalidad.Count = 1 Then
                        filtro &= " AND C.OID_CALIDAD = []OID_CALIDAD "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CALIDAD", ProsegurDbType.Descricao_Curta, identificadoresCalidad(0)))
                    ElseIf identificadoresCalidad.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresCalidad, "OID_CALIDAD", cmd, "AND", "C", , False)
                    End If
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function













        ''' <summary>
        ''' Obtener todas las Unidades Medidas
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerCalidades() As ObservableCollection(Of Clases.Calidad)

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerCalidades)
            Comando.CommandType = CommandType.Text

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)
            Dim Calidad As Clases.Calidad = Nothing
            Dim ListaCalidades As New ObservableCollection(Of Clases.Calidad)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each row In dt.Rows

                    Calidad = New Clases.Calidad
                    Calidad.Identificador = Util.AtribuirValorObj(row("OID_CALIDAD"), GetType(String))
                    Calidad.Codigo = Util.AtribuirValorObj(row("COD_CALIDAD"), GetType(String))
                    Calidad.Descripcion = Util.AtribuirValorObj(row("DES_CALIDAD"), GetType(String))
                    Calidad.TipoCalidad = Extenciones.RecuperarEnum(Of Enumeradores.TipoCalidad)(Util.AtribuirValorObj(row("COD_TIPO_CALIDAD"), GetType(String)))

                    ListaCalidades.Add(Calidad)

                Next row

            End If

            Return ListaCalidades

        End Function


        ''' <summary>
        ''' obtem a qualidade
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerCalidade(CodigoCalidad As String) As Clases.Calidad

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CalidadRecuperar)
            Comando.CommandType = CommandType.Text

            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CALIDAD", ProsegurDbType.Identificador_Alfanumerico, CodigoCalidad))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)
            Dim Calidad As Clases.Calidad = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Calidad = New Clases.Calidad
                Calidad.Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_CALIDAD"), GetType(String))
                Calidad.Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_CALIDAD"), GetType(String))
                Calidad.Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_CALIDAD"), GetType(String))

            End If

            Return Calidad
        End Function

        ''' <summary>
        ''' obtem a qualidade pelo seu identificador
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerCalidadePorIdentificador(identificador As String) As Clases.Calidad

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerCalidadPorIdentificador)
            Comando.CommandType = CommandType.Text

            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CALIDAD", ProsegurDbType.Objeto_Id, identificador))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)
            Dim Calidad As Clases.Calidad = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Calidad = New Clases.Calidad
                Calidad.Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_CALIDAD"), GetType(String))
                Calidad.Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_CALIDAD"), GetType(String))
                Calidad.Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_CALIDAD"), GetType(String))

            End If

            Return Calidad
        End Function

    End Class

End Namespace