Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel

Namespace Genesis
    Public Class UnidadMedida

        Shared Function ObtenerUnidadMedidaPorDivisa_v2(codigosUnidadMedida As List(Of String), identificadoresUnidadMedida As List(Of String)) As DataTable
            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.ObtenerUnidadesMedida_v2
                Dim filtro As String = ""

                If codigosUnidadMedida IsNot Nothing Then
                    If codigosUnidadMedida.Count = 1 Then
                        filtro &= " AND UM.COD_UNIDAD_MEDIDA = []COD_UNIDAD_MEDIDA "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_UNIDAD_MEDIDA", ProsegurDbType.Descricao_Curta, codigosUnidadMedida(0)))
                    ElseIf codigosUnidadMedida.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosUnidadMedida, "COD_UNIDAD_MEDIDA", cmd, "AND", "UM", , False)
                    End If
                End If

                If identificadoresUnidadMedida IsNot Nothing Then
                    If identificadoresUnidadMedida.Count = 1 Then
                        filtro &= " AND UM.OID_UNIDAD_MEDIDA = []OID_UNIDAD_MEDIDA "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_UNIDAD_MEDIDA", ProsegurDbType.Descricao_Curta, identificadoresUnidadMedida(0)))
                    ElseIf identificadoresUnidadMedida.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresUnidadMedida, "OID_UNIDAD_MEDIDA", cmd, "AND", "UM", , False)
                    End If
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function














        ''' <summary>
        ''' Recuperar unidade medida padron.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarUnidadMedida(Optional RecuperarUnidadPadron As Boolean? = Nothing, _
                                                     Optional IdentificadorUnidadeMedida As String = "", _
                                                     Optional objTipoUnidadMedida As Enumeradores.TipoUnidadMedida? = Nothing) As Clases.UnidadMedida

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.UnidadMedidaRecuperarDefecto)
            cmd.CommandType = CommandType.Text

            If (RecuperarUnidadPadron.HasValue) Then
                cmd.CommandText &= " AND BOL_DEFECTO = []BOL_DEFECTO "
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_DEFECTO", ProsegurDbType.Logico, RecuperarUnidadPadron))
            End If

            If Not String.IsNullOrEmpty(IdentificadorUnidadeMedida) Then
                cmd.CommandText &= " AND OID_UNIDAD_MEDIDA = []OID_UNIDAD_MEDIDA "
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_UNIDAD_MEDIDA", ProsegurDbType.Objeto_Id, IdentificadorUnidadeMedida))
            End If

            If objTipoUnidadMedida IsNot Nothing Then
                cmd.CommandText &= " AND COD_TIPO_UNIDAD_MEDIDA = []COD_TIPO_UNIDAD_MEDIDA "
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_UNIDAD_MEDIDA", ProsegurDbType.Inteiro_Curto, RecuperarValor(objTipoUnidadMedida)))
            End If

            cmd.CommandText += " ORDER BY NUM_VALOR_UNIDAD DESC"
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            Dim unidadMedida As Clases.UnidadMedida = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim dr As DataRow = dt.Rows(0)
                unidadMedida = New Clases.UnidadMedida
                unidadMedida.Identificador = Util.AtribuirValorObj(dr("OID_UNIDAD_MEDIDA"), GetType(String))
                unidadMedida.Codigo = Util.AtribuirValorObj(dr("COD_UNIDAD_MEDIDA"), GetType(String))
                unidadMedida.Descripcion = Util.AtribuirValorObj(dr("DES_UNIDAD_MEDIDA"), GetType(String))
                unidadMedida.EsPadron = Util.AtribuirValorObj(dr("BOL_DEFECTO"), GetType(Boolean))
                unidadMedida.TipoUnidadMedida = RecuperarEnum(Of Enumeradores.TipoUnidadMedida)(dr("COD_TIPO_UNIDAD_MEDIDA"))
                unidadMedida.ValorUnidad = Util.AtribuirValorObj(dr("NUM_VALOR_UNIDAD"), GetType(Decimal))
            End If

            Return unidadMedida
        End Function

        ''' <summary>
        ''' Obtener todas las Unidades Medidas
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerUnidadesMedida(Optional RecuperarUnidadPadron As Boolean? = Nothing) As ObservableCollection(Of Clases.UnidadMedida)

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerUnidadesMedida)
            Comando.CommandType = CommandType.Text

            If (RecuperarUnidadPadron.HasValue) Then
                Comando.CommandText &= " WHERE BOL_DEFECTO = []BOL_DEFECTO "
                Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_DEFECTO", ProsegurDbType.Logico, RecuperarUnidadPadron))
            End If

            Comando.CommandText += " ORDER BY DES_UNIDAD_MEDIDA"
            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, Comando.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)
            Dim UnidadMedida As Clases.UnidadMedida = Nothing
            Dim ListaUnidadesMedida As New ObservableCollection(Of Clases.UnidadMedida)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each row In dt.Rows

                    UnidadMedida = New Clases.UnidadMedida
                    UnidadMedida.Identificador = Util.AtribuirValorObj(row("OID_UNIDAD_MEDIDA"), GetType(String))
                    UnidadMedida.Codigo = Util.AtribuirValorObj(row("COD_UNIDAD_MEDIDA"), GetType(String))
                    UnidadMedida.Descripcion = Util.AtribuirValorObj(row("DES_UNIDAD_MEDIDA"), GetType(String))
                    UnidadMedida.EsPadron = Util.AtribuirValorObj(row("BOL_DEFECTO"), GetType(Boolean))
                    UnidadMedida.TipoUnidadMedida = RecuperarEnum(Of Enumeradores.TipoUnidadMedida)(row("COD_TIPO_UNIDAD_MEDIDA"))
                    UnidadMedida.ValorUnidad = Util.AtribuirValorObj(row("NUM_VALOR_UNIDAD"), GetType(Decimal))

                    ListaUnidadesMedida.Add(UnidadMedida)

                Next row

            End If

            Return ListaUnidadesMedida

        End Function

        

    End Class
End Namespace

