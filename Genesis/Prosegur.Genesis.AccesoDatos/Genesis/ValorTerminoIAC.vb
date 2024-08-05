Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Genesis

    ''' <summary>
    ''' Classe ValorTerminoIAC
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 06/09/2013
    ''' </history>
    Public Class ValorTerminoIAC


        Shared Function ObtenerValorTerminoIAC(identificadoresRemesa As List(Of String), identificadoresBulto As List(Of String), identificadoresParcial As List(Of String)) As DataTable
            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.ObtenerValorTerminoIACElemento
                Dim filtroRemesa As String = ""
                Dim filtroBulto As String = ""
                Dim filtroParcial As String = ""

                If identificadoresRemesa IsNot Nothing Then
                    If identificadoresRemesa.Count = 1 Then
                        filtroRemesa &= " IAC.OID_REMESA "
                        filtroRemesa &= " = []OID_REMESA "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Descricao_Curta, identificadoresRemesa(0)))
                    ElseIf identificadoresRemesa.Count > 0 Then
                        filtroRemesa &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresRemesa, "OID_REMESA", cmd, "", "IAC", , False)
                    Else
                        filtroRemesa &= " IAC.OID_REMESA "
                        filtroRemesa &= " = NULL "
                    End If
                Else
                    filtroRemesa &= " IAC.OID_REMESA "
                    filtroRemesa &= " = NULL "
                End If

                If identificadoresBulto IsNot Nothing Then
                    If identificadoresBulto.Count = 1 Then
                        filtroBulto &= " IAC.OID_BULTO "
                        filtroBulto &= " = []OID_BULTO "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Descricao_Curta, identificadoresBulto(0)))
                    ElseIf identificadoresBulto.Count > 0 Then
                        filtroBulto &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresBulto, "OID_BULTO", cmd, "", "IAC", , False)
                    Else
                        filtroBulto &= " IAC.OID_BULTO "
                        filtroBulto &= " = NULL "
                    End If
                Else
                    filtroBulto &= " IAC.OID_BULTO "
                    filtroBulto &= " = NULL "
                End If

                If identificadoresParcial IsNot Nothing Then
                    If identificadoresParcial.Count = 1 Then
                        filtroParcial &= " IAC.OID_PARCIAL "
                        filtroParcial &= " = []OID_PARCIAL "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Descricao_Curta, identificadoresParcial(0)))
                    ElseIf identificadoresParcial.Count > 0 Then
                        filtroParcial &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresParcial, "OID_PARCIAL", cmd, "", "", , False)
                    Else
                        filtroParcial &= " IAC.OID_PARCIAL "
                        filtroParcial &= " = NULL "
                    End If
                Else
                    filtroParcial &= " IAC.OID_PARCIAL "
                    filtroParcial &= " = NULL "
                End If


                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtroRemesa, filtroBulto, filtroParcial))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function












        ''' <summary>
        ''' Obtener los valores de terminos para el elemento
        ''' </summary>
        ''' <param name="identificadorElemento"></param>
        ''' <param name="TipoElemento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerValorTerminoPorElemento(identificadorElemento As String, TipoElemento As Enumeradores.TipoElemento) As DataTable

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandType = CommandType.Text

            'De acordo com o tipo de elemento recupera a query correta.
            Select Case TipoElemento
                Case Enumeradores.TipoElemento.Remesa
                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorIacRemesaRecuperar)
                Case Enumeradores.TipoElemento.Bulto
                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorIacBultoRecuperar)
                Case Enumeradores.TipoElemento.Parcial
                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorIACParcialRecuperar)
            End Select

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ELEMENTO", ProsegurDbType.Objeto_Id, identificadorElemento))

            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
        End Function










        ''' <summary>
        ''' Recupera os ValorTerminoIAC.
        ''' </summary>
        ''' <param name="identificadorTermino"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarValoresTerminosIAC(identificadorTermino As String) As ObservableCollection(Of Clases.TerminoValorPosible)

            Dim listaValores As New ObservableCollection(Of Clases.TerminoValorPosible)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoIACRecuperarPorTermino)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TERMINO", ProsegurDbType.Objeto_Id, identificadorTermino))

            Using dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                For Each itemRow In dt.Rows

                    Dim valor As New Clases.TerminoValorPosible
                    With valor
                        .Identificador = Util.AtribuirValorObj(itemRow("OID_VALOR"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(itemRow("COD_VALOR"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(itemRow("DES_VALOR"), GetType(String))
                        .CodigoUsuario = Util.AtribuirValorObj(itemRow("COD_USUARIO"), GetType(String))
                        .EstaActivo = Util.AtribuirValorObj(itemRow("BOL_VIGENTE"), GetType(String))
                        .FechaHoraActualizacion = Util.AtribuirValorObj(itemRow("FYH_ACTUALIZACION"), GetType(DateTime))
                        .ValorDefecto = Util.AtribuirValorObj(itemRow("BOL_VALOR_DEFECTO"), GetType(Boolean))
                        listaValores.Add(valor)
                    End With

                Next

            End Using

            Return listaValores
        End Function

        Public Shared Function RecuperarValoresTerminosIAC(identificadoresTerminos As List(Of Object)) As List(Of Tuple(Of String, ObservableCollection(Of Clases.TerminoValorPosible)))

            Dim TerminosValores As New List(Of Tuple(Of String, ObservableCollection(Of Clases.TerminoValorPosible)))
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandType = CommandType.Text
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ValorTerminosIACRecuperarPorTerminos, Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresTerminos, "OID_TERMINO", cmd, "WHERE", "TIAC")))

            Using dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                Dim valores As New ObservableCollection(Of Clases.TerminoValorPosible)

                For Each itemRow In dt.Rows

                    Dim identificadorTermino As String = Util.AtribuirValorObj(itemRow("OID_TERMINO"), GetType(String))

                    Dim valor As New Clases.TerminoValorPosible With
                        {
                            .Identificador = Util.AtribuirValorObj(itemRow("OID_VALOR"), GetType(String)),
                            .Codigo = Util.AtribuirValorObj(itemRow("COD_VALOR"), GetType(String)),
                            .Descripcion = Util.AtribuirValorObj(itemRow("DES_VALOR"), GetType(String)),
                            .CodigoUsuario = Util.AtribuirValorObj(itemRow("COD_USUARIO"), GetType(String)),
                            .EstaActivo = Util.AtribuirValorObj(itemRow("BOL_VIGENTE"), GetType(String)),
                            .FechaHoraActualizacion = Util.AtribuirValorObj(itemRow("FYH_ACTUALIZACION"), GetType(DateTime)),
                            .ValorDefecto = Util.AtribuirValorObj(itemRow("BOL_VALOR_DEFECTO"), GetType(Boolean))
                        }

                    If TerminosValores.Exists(Function(e) e.Item1 = identificadorTermino) Then
                        Dim tuple = TerminosValores.FirstOrDefault(Function(e) e.Item1 = identificadorTermino)
                        Dim tValores As ObservableCollection(Of Clases.TerminoValorPosible) = tuple.Item2
                        tValores.Add(valor)
                        TerminosValores.RemoveAll(Function(r) r.Item1 = identificadorTermino)

                        TerminosValores.Add(New Tuple(Of String, ObservableCollection(Of Clases.TerminoValorPosible)) _
                                            (identificadorTermino, tValores))

                    Else
                        TerminosValores.Add(New Tuple(Of String, ObservableCollection(Of Clases.TerminoValorPosible)) _
                                            (identificadorTermino, New ObservableCollection(Of Clases.TerminoValorPosible) From {valor}))
                    End If

                Next

            End Using

            Return TerminosValores

        End Function

        ''' <summary>
        ''' Recupera os valores de termino para o elemento
        ''' </summary>
        ''' <param name="IdElemento"></param>
        ''' <param name="TipoElemento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarValorTerminoPorElemento(IdElemento As String, TipoElemento As Enumeradores.TipoElemento) As List(Of KeyValuePair(Of String, String))

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            'De acordo com o tipo de elemento recupera a query correta.
            Select Case TipoElemento

                Case Enumeradores.TipoElemento.Remesa

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorIacRemesaRecuperar)

                Case Enumeradores.TipoElemento.Bulto

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorIacBultoRecuperar)

                Case Enumeradores.TipoElemento.Parcial

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorIACParcialRecuperar)

            End Select

            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ELEMENTO", ProsegurDbType.Objeto_Id, IdElemento))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
            Dim objValoresTermino As List(Of KeyValuePair(Of String, String)) = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objValoresTermino = New List(Of KeyValuePair(Of String, String))

                For Each dr In dt.Rows
                    objValoresTermino.Add(New KeyValuePair(Of String, String) _
                                         (Util.AtribuirValorObj(dr("COD_TERMINO_IAC"), GetType(String)), Util.AtribuirValorObj(dr("DES_VALOR_IAC"), GetType(String))))
                Next

            End If

            Return objValoresTermino
        End Function

    End Class

End Namespace

