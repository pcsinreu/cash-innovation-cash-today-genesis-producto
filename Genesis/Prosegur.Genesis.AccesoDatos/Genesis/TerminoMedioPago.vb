Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Genesis

    ''' <summary>
    ''' Classe Termino Medio Pago
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TerminoMedioPago


        Public Shared Function PopularMedioPagosConTerminos_v2(rowMedioPago As DataRow) As Clases.Termino
            Dim termino As Clases.Termino = Nothing

            If rowMedioPago IsNot Nothing AndAlso rowMedioPago.Table.Columns.Contains("T_OID_TERMINO") AndAlso _
                Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowMedioPago("T_OID_TERMINO"), GetType(String))) Then

                termino = New Clases.Termino

                With termino
                    .Identificador = If(rowMedioPago.Table.Columns.Contains("T_OID_TERMINO"), Util.AtribuirValorObj(rowMedioPago("T_OID_TERMINO"), GetType(String)), Nothing)
                    .Codigo = If(rowMedioPago.Table.Columns.Contains("T_COD_TERMINO"), Util.AtribuirValorObj(rowMedioPago("T_COD_TERMINO"), GetType(String)), Nothing)
                    .Descripcion = If(rowMedioPago.Table.Columns.Contains("T_DES_TERMINO"), Util.AtribuirValorObj(rowMedioPago("T_DES_TERMINO"), GetType(String)), Nothing)
                    .Observacion = If(rowMedioPago.Table.Columns.Contains("T_OBS_TERMINO"), Util.AtribuirValorObj(rowMedioPago("T_OBS_TERMINO"), GetType(String)), Nothing)
                    .ValorInicial = If(rowMedioPago.Table.Columns.Contains("T_DES_VALOR_INICIAL"), Util.AtribuirValorObj(rowMedioPago("T_DES_VALOR_INICIAL"), GetType(String)), Nothing)
                    .Longitud = If(rowMedioPago.Table.Columns.Contains("T_NEC_LONGITUD"), Util.AtribuirValorObj(rowMedioPago("T_NEC_LONGITUD"), GetType(Integer)), Nothing)
                    .MostrarDescripcionConCodigo = If(rowMedioPago.Table.Columns.Contains("T_BOL_MOSTRAR_CODIGO"), Util.AtribuirValorObj(rowMedioPago("T_BOL_MOSTRAR_CODIGO"), GetType(Int16)), Nothing)
                    .Orden = If(rowMedioPago.Table.Columns.Contains("T_NEC_ORDEN"), Util.AtribuirValorObj(rowMedioPago("T_NEC_ORDEN"), GetType(Integer)), Nothing)
                    .EstaActivo = If(rowMedioPago.Table.Columns.Contains("T_BOL_VIGENTE"), Util.AtribuirValorObj(rowMedioPago("T_BOL_VIGENTE"), GetType(Int16)), Nothing)
                    .CodigoUsuario = If(rowMedioPago.Table.Columns.Contains("T_COD_USUARIO"), Util.AtribuirValorObj(rowMedioPago("T_COD_USUARIO"), GetType(String)), Nothing)
                    .FechaHoraActualizacion = If(rowMedioPago.Table.Columns.Contains("T_FYH_ACTUALIZACION"), Util.AtribuirValorObj(rowMedioPago("T_FYH_ACTUALIZACION"), GetType(DateTime)), Nothing)

                    If rowMedioPago.Table.Columns.Contains("OID_FORMATO") Then
                        .Formato = New Clases.Formato
                        .Formato.Identificador = If(rowMedioPago.Table.Columns.Contains("OID_FORMATO"), Util.AtribuirValorObj(rowMedioPago("OID_FORMATO"), GetType(String)), Nothing)
                        .Formato.Codigo = If(rowMedioPago.Table.Columns.Contains("COD_FORMATO"), Util.AtribuirValorObj(rowMedioPago("COD_FORMATO"), GetType(String)), Nothing)
                        .Formato.Descripcion = If(rowMedioPago.Table.Columns.Contains("DES_FORMATO"), Util.AtribuirValorObj(rowMedioPago("DES_FORMATO"), GetType(String)), Nothing)
                    End If

                    If rowMedioPago.Table.Columns.Contains("OID_MASCARA") Then
                        .Mascara = New Clases.Mascara
                        .Mascara.Identificador = If(rowMedioPago.Table.Columns.Contains("OID_MASCARA"), Util.AtribuirValorObj(rowMedioPago("OID_MASCARA"), GetType(String)), Nothing)
                        .Mascara.Codigo = If(rowMedioPago.Table.Columns.Contains("COD_MASCARA"), Util.AtribuirValorObj(rowMedioPago("COD_MASCARA"), GetType(String)), Nothing)
                        .Mascara.Descripcion = If(rowMedioPago.Table.Columns.Contains("DES_MASCARA"), Util.AtribuirValorObj(rowMedioPago("DES_MASCARA"), GetType(String)), Nothing)
                        .Mascara.ExpresionRegular = If(rowMedioPago.Table.Columns.Contains("DES_EXP_REGULAR"), Util.AtribuirValorObj(rowMedioPago("DES_EXP_REGULAR"), GetType(String)), Nothing)
                    End If

                    If rowMedioPago.Table.Columns.Contains("OID_ALGORITMO_VALIDACION") Then
                        .AlgoritmoValidacion = New Clases.AlgoritmoValidacion
                        .AlgoritmoValidacion.Identificador = If(rowMedioPago.Table.Columns.Contains("OID_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(rowMedioPago("OID_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                        .AlgoritmoValidacion.Codigo = If(rowMedioPago.Table.Columns.Contains("COD_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(rowMedioPago("COD_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                        .AlgoritmoValidacion.Descripcion = If(rowMedioPago.Table.Columns.Contains("DES_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(rowMedioPago("DES_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                        .AlgoritmoValidacion.Observacion = If(rowMedioPago.Table.Columns.Contains("OBS_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(rowMedioPago("OBS_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                    End If

                    If rowMedioPago.Table.Columns.Contains("OID_VALOR") Then
                        .ValoresPosibles = New ObservableCollection(Of Clases.TerminoValorPosible)
                        Dim valor As New Clases.TerminoValorPosible
                        valor.Identificador = If(rowMedioPago.Table.Columns.Contains("OID_VALOR"), Util.AtribuirValorObj(rowMedioPago("OID_VALOR"), GetType(String)), Nothing)
                        valor.Codigo = If(rowMedioPago.Table.Columns.Contains("COD_VALOR"), Util.AtribuirValorObj(rowMedioPago("COD_VALOR"), GetType(String)), Nothing)
                        valor.Descripcion = If(rowMedioPago.Table.Columns.Contains("DES_VALOR"), Util.AtribuirValorObj(rowMedioPago("DES_VALOR"), GetType(String)), Nothing)
                        valor.EstaActivo = If(rowMedioPago.Table.Columns.Contains("VT_BOL_VIGENTE"), Util.AtribuirValorObj(rowMedioPago("VT_BOL_VIGENTE"), GetType(String)), Nothing)
                        .ValoresPosibles.Add(valor)
                    End If

                End With

            End If

            Return termino
        End Function


























        ''' <summary>
        ''' Recupera os terminos do meio de pagamento
        ''' </summary>
        ''' <param name="IdMedioPago"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarTerminoMedioPago(IdMedioPago As String) As ObservableCollection(Of Clases.Termino)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TerminoMedioPagoRecuperar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, IdMedioPago))

            Dim objTerminosMedioPago As ObservableCollection(Of Clases.Termino) = Nothing

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objTerminosMedioPago = New ObservableCollection(Of Clases.Termino)

                For Each dr In dt.Rows

                    objTerminosMedioPago.Add(New Clases.Termino With { _
                                             .AceptarDigitacion = False, _
                                             .AlgoritmoValidacion = New Clases.AlgoritmoValidacion With {.Codigo = Util.AtribuirValorObj(dr("COD_ALGORITMO_VALIDACION"), GetType(String)), _
                                                                                                         .CodigoUsuario = Util.AtribuirValorObj(dr("USUARIO_ALGORITMO"), GetType(String)), _
                                                                                                         .Descripcion = Util.AtribuirValorObj(dr("DES_ALGORITMO_VALIDACION"), GetType(String)), _
                                                                                                         .FechaHoraAplicacion = Util.AtribuirValorObj(dr("FYH_ALGORITMO"), GetType(DateTime)), _
                                                                                                         .Identificador = Util.AtribuirValorObj(dr("OID_ALGORITMO_VALIDACION"), GetType(String)), _
                                                                                                         .Observacion = Util.AtribuirValorObj(dr("OBS_ALGORITMO_VALIDACION"), GetType(String))}, _
                                             .Codigo = Util.AtribuirValorObj(dr("COD_TERMINO"), GetType(String)), _
                                             .CodigoMigracion = String.Empty, _
                                             .CodigoUsuario = Util.AtribuirValorObj(dr("USUARIO_TERMINO"), GetType(String)), _
                                             .Descripcion = Util.AtribuirValorObj(dr("DES_TERMINO"), GetType(String)), _
                                             .EsEspecificoDeSaldos = False, _
                                             .EstaActivo = Util.AtribuirValorObj(dr("TERMINO_ACTIVO"), GetType(Boolean)), _
                                             .FechaHoraActualizacion = Util.AtribuirValorObj(dr("FYH_TERMINO"), GetType(DateTime)), _
                                             .Formato = New Clases.Formato With {.Codigo = Util.AtribuirValorObj(dr("COD_FORMATO"), GetType(String)), _
                                                                                 .CodigoUsuario = Util.AtribuirValorObj(dr("USUARIO_FORMATO"), GetType(String)), _
                                                                                 .Descripcion = Util.AtribuirValorObj(dr("DES_FORMATO"), GetType(String)), _
                                                                                 .FechaHoraActualizacion = Util.AtribuirValorObj(dr("FYH_FORMATO"), GetType(DateTime)), _
                                                                                 .Identificador = Util.AtribuirValorObj(dr("OID_FORMATO"), GetType(String))}, _
                                             .Identificador = Util.AtribuirValorObj(dr("OID_TERMINO"), GetType(String)), _
                                             .Longitud = Util.AtribuirValorObj(dr("NEC_LONGITUD"), GetType(Integer)), _
                                             .Mascara = New Clases.Mascara With {.Codigo = Util.AtribuirValorObj(dr("COD_MASCARA"), GetType(String)), _
                                                                                 .CodigoUsuario = Util.AtribuirValorObj(dr("USUARIO_MASCARA"), GetType(String)), _
                                                                                 .Descripcion = Util.AtribuirValorObj(dr("DES_MASCARA"), GetType(String)), _
                                                                                 .ExpresionRegular = Util.AtribuirValorObj(dr("DES_EXP_REGULAR"), GetType(String)), _
                                                                                 .FechaHoraActualizacion = Util.AtribuirValorObj(dr("FYH_MASCARA"), GetType(DateTime)), _
                                                                                 .Identificador = Util.AtribuirValorObj(dr("OID_MASCARA"), GetType(String))}, _
                                             .MostrarDescripcionConCodigo = Util.AtribuirValorObj(dr("BOL_MOSTRAR_CODIGO"), GetType(Boolean)), _
                                             .Observacion = Util.AtribuirValorObj(dr("OBS_TERMINO"), GetType(String)), _
                                             .Orden = Util.AtribuirValorObj(dr("NEC_ORDEN"), GetType(Integer)), _
                                             .ValoresPosibles = RecuperarValoresPosiveis(.Identificador), _
                                             .TieneValoresPosibles = (.ValoresPosibles IsNot Nothing AndAlso .ValoresPosibles.Count > 0), _
                                             .ValorInicial = Util.AtribuirValorObj(dr("DES_VALOR_INICIAL"), GetType(String))})





                Next

            End If

            Return objTerminosMedioPago
        End Function

        ''' <summary>
        ''' Recupera os valores possiveis do termino de medio pago
        ''' </summary>
        ''' <param name="IdTermino"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function RecuperarValoresPosiveis(IdTermino As String) As ObservableCollection(Of Clases.TerminoValorPosible)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TerminoMedioPagoRecuperarValorPossivel)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TERMINO", ProsegurDbType.Objeto_Id, IdTermino))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
            Dim objValoresPosiblesTermino As ObservableCollection(Of Clases.TerminoValorPosible) = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objValoresPosiblesTermino = New ObservableCollection(Of Clases.TerminoValorPosible)

                For Each dr In dt.Rows

                    objValoresPosiblesTermino.Add(New Clases.TerminoValorPosible With { _
                                                  .Codigo = Util.AtribuirValorObj(dr("COD_VALOR"), GetType(String)), _
                                                  .CodigoUsuario = Util.AtribuirValorObj(dr("COD_USUARIO"), GetType(String)), _
                                                  .Descripcion = Util.AtribuirValorObj(dr("DES_VALOR"), GetType(String)), _
                                                  .EstaActivo = Util.AtribuirValorObj(dr("BOL_VIGENTE"), GetType(Boolean)), _
                                                  .FechaHoraActualizacion = Util.AtribuirValorObj(dr("FYH_ACTUALIZACION"), GetType(DateTime)), _
                                                  .Identificador = Util.AtribuirValorObj(dr("OID_VALOR"), GetType(String)), _
                                                  .ValorDefecto = False})
                Next

            End If

            Return objValoresPosiblesTermino
        End Function

        ''' <summary>
        ''' Metodo getDenominacionesByDivisas
        ''' </summary>
        ''' <param name="Identificador"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerTerminosPorMedioPago(Identificador As String) As ObservableCollection(Of Clases.Termino)

            ' criar comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            ' obter comando sql
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerTerminosPorMedioPago.ToString)
            comando.CommandType = CommandType.Text

            ' criar parameter
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, Identificador))
            'comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_VIGENTE", ProsegurDbType.Logico, True))

            ' criar objeto denominacion coleccion
            Dim ListaTermino As New ObservableCollection(Of Clases.Termino)

            ' executar query
            Dim dtTermino As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

            ' se encontrou algum registro
            If dtTermino IsNot Nothing AndAlso dtTermino.Rows.Count > 0 Then

                ' percorrer os registros encontrados
                For Each row As DataRow In dtTermino.Rows

                    ' adicionar divisa para coleção
                    ListaTermino.Add(PopularTerminos(row))

                Next row

                ' retornar coleção de terminos
                Return ListaTermino
            End If

            Return Nothing

        End Function

        ''' <summary>
        ''' Popula o objeto terminos através de datarows
        ''' </summary>
        ''' <param name="row"></param>
        ''' <returns></returns>
        ''' <history>
        ''' [marcel.espiritosanto] 22/08/2013 Criado
        ''' </history>
        Private Shared Function PopularTerminos(row As DataRow) As Clases.Termino

            Dim objTermino As New Clases.Termino

            Util.AtribuirValorObjeto(objTermino.Identificador, row("OID_TERMINO"), GetType(String))
            Util.AtribuirValorObjeto(objTermino.Codigo, row("COD_TERMINO"), GetType(String))
            Util.AtribuirValorObjeto(objTermino.Descripcion, row("DES_TERMINO"), GetType(String))
            Util.AtribuirValorObjeto(objTermino.Observacion, row("OBS_TERMINO"), GetType(String))
            Util.AtribuirValorObjeto(objTermino.ValorInicial, row("DES_VALOR_INICIAL"), GetType(String))
            Util.AtribuirValorObjeto(objTermino.AceptarDigitacion, 0, GetType(Int16))
            Util.AtribuirValorObjeto(objTermino.EsEspecificoDeSaldos, 0, GetType(Int16))
            Util.AtribuirValorObjeto(objTermino.CodigoMigracion, "", GetType(String))
            'Util.AtribuirValorObjeto(objTermino.Valor, Genesis.ValorTerminoMedioPago.RecuperarValorTermino(objTermino.Identificador), GetType(String))
            Util.AtribuirValorObjeto(objTermino.Longitud, row("NEC_LONGITUD"), GetType(Integer))
            Util.AtribuirValorObjeto(objTermino.MostrarDescripcionConCodigo, row("BOL_MOSTRAR_CODIGO"), GetType(Int16))
            Util.AtribuirValorObjeto(objTermino.Orden, row("NEC_ORDEN"), GetType(Integer))
            Util.AtribuirValorObjeto(objTermino.EstaActivo, row("BOL_VIGENTE"), GetType(Int16))
            Util.AtribuirValorObjeto(objTermino.CodigoUsuario, row("COD_USUARIO"), GetType(String))
            Util.AtribuirValorObjeto(objTermino.FechaHoraActualizacion, row("FYH_ACTUALIZACION"), GetType(DateTime))


            If Not IsDBNull(row("OID_FORMATO")) Then
                objTermino.Formato = Formato.ObtenerFormato(row("OID_FORMATO"))

            End If
            If Not IsDBNull(row("OID_MASCARA")) Then
                objTermino.Mascara = Mascara.ObtenerMascara(row("OID_MASCARA"))

            End If
            If Not IsDBNull(row("OID_ALGORITMO_VALIDACION")) Then
                objTermino.AlgoritmoValidacion = AlgoritmoValidacion.ObtenerAlgoritmoValidacion(row("OID_ALGORITMO_VALIDACION"))

            End If

            objTermino.ValoresPosibles = ValorTerminoMedioPago.ObtenerValoresPosiblesPorTermino(objTermino.Identificador)
            Util.AtribuirValorObjeto(objTermino.TieneValoresPosibles, If(objTermino.ValoresPosibles IsNot Nothing AndAlso objTermino.ValoresPosibles.Count > 0, 1, 0), GetType(Int16))

            Return objTermino

        End Function

        Public Shared Function PopularMedioPagosConTerminos(row As DataRow) As Clases.Termino

            Dim objTermino As New Clases.Termino

            Util.AtribuirValorObjeto(objTermino.Identificador, row("T_OID_TERMINO"), GetType(String))
            Util.AtribuirValorObjeto(objTermino.Codigo, row("T_COD_TERMINO"), GetType(String))
            Util.AtribuirValorObjeto(objTermino.Descripcion, row("T_DES_TERMINO"), GetType(String))
            Util.AtribuirValorObjeto(objTermino.Observacion, row("T_OBS_TERMINO"), GetType(String))
            Util.AtribuirValorObjeto(objTermino.ValorInicial, row("T_DES_VALOR_INICIAL"), GetType(String))
            Util.AtribuirValorObjeto(objTermino.AceptarDigitacion, 0, GetType(Int16))
            Util.AtribuirValorObjeto(objTermino.EsEspecificoDeSaldos, 0, GetType(Int16))
            Util.AtribuirValorObjeto(objTermino.CodigoMigracion, "", GetType(String))
            'Util.AtribuirValorObjeto(objTermino.Valor, Genesis.ValorTerminoMedioPago.RecuperarValorTermino(objTermino.Identificador), GetType(String))
            Util.AtribuirValorObjeto(objTermino.Longitud, row("T_NEC_LONGITUD"), GetType(Integer))
            Util.AtribuirValorObjeto(objTermino.MostrarDescripcionConCodigo, row("T_BOL_MOSTRAR_CODIGO"), GetType(Int16))
            Util.AtribuirValorObjeto(objTermino.Orden, row("T_NEC_ORDEN"), GetType(Integer))
            Util.AtribuirValorObjeto(objTermino.EstaActivo, row("T_BOL_VIGENTE"), GetType(Int16))
            Util.AtribuirValorObjeto(objTermino.CodigoUsuario, row("T_COD_USUARIO"), GetType(String))
            Util.AtribuirValorObjeto(objTermino.FechaHoraActualizacion, row("T_FYH_ACTUALIZACION"), GetType(DateTime))


            If Not IsDBNull(row("T_OID_FORMATO")) Then
                objTermino.Formato = Formato.ObtenerFormato(row("T_OID_FORMATO"))

            End If
            If Not IsDBNull(row("T_OID_MASCARA")) Then
                objTermino.Mascara = Mascara.ObtenerMascara(row("T_OID_MASCARA"))

            End If
            If Not IsDBNull(row("T_OID_ALGORITMO_VALIDACION")) Then
                objTermino.AlgoritmoValidacion = AlgoritmoValidacion.ObtenerAlgoritmoValidacion(row("T_OID_ALGORITMO_VALIDACION"))

            End If

            objTermino.ValoresPosibles = ValorTerminoMedioPago.ObtenerValoresPosiblesPorTermino(objTermino.Identificador)
            Util.AtribuirValorObjeto(objTermino.TieneValoresPosibles, If(objTermino.ValoresPosibles IsNot Nothing AndAlso objTermino.ValoresPosibles.Count > 0, 1, 0), GetType(Int16))

            Return objTermino

        End Function

    End Class

End Namespace