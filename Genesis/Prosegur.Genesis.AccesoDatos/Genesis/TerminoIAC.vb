Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Genesis
    ''' <summary>
    ''' Classe TerminoIAC
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 05/09/2013
    ''' </history>
    Public Class TerminoIAC

        Shared Function ObtenerTerminosIAC(identificadores As List(Of String)) As DataTable
            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.ObtenerTerminos_v4
                Dim filtro As String = ""

                If identificadores IsNot Nothing Then
                    If identificadores.Count = 1 Then
                        filtro &= " AND TI.OID_IAC = []OID_IAC "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC", ProsegurDbType.Descricao_Curta, identificadores(0)))
                    ElseIf identificadores.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadores, "OID_IAC", cmd, "AND", "TI", , False)
                    End If
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function















        ''' <summary>
        ''' Obtener el TerminosIAC por lo Identificador
        ''' </summary>
        ''' <param name="identificadorTerminosIAC"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerTerminosIACPorIdentificador(identificadorTerminosIAC As String) As DataTable
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TerminoIACRecuperarPorGrupoTermino)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC", ProsegurDbType.Objeto_Id, identificadorTerminosIAC))

            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
        End Function

        Public Shared Function ObtenerTerminosIACPorEmissorDocumento(codEmissorDocumento As String) As DataTable
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TerminoIACRecuperarPorEmissorDocumento)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_EMISOR_DOCUMENTO", ProsegurDbType.Objeto_Id, codEmissorDocumento))

            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
        End Function

        ''' <summary>
        ''' Obtener el TerminosIAC por lo Codigo
        ''' </summary>
        ''' <param name="CodigosTerminosIAC"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerTerminosIACPorCodigos(CodigosTerminosIAC As List(Of String)) As DataTable
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TerminoIACRecuperarPorCodigo)
            cmd.CommandType = CommandType.Text

            cmd.CommandText = String.Format(cmd.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, CodigosTerminosIAC, "COD_TERMINO", cmd, "WHERE", "T"))
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

        End Function

        ''' <summary>
        ''' Recupera os terminos de IAC Por grupo de IAC.
        ''' </summary>
        ''' <param name="identificadorGrupoIAC"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarTerminosIAC(identificadorGrupoIAC As String) As ObservableCollection(Of Clases.TerminoIAC)

            Dim listaTerminoIAC As New ObservableCollection(Of Clases.TerminoIAC)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TerminoIACRecuperarPorGrupoTermino)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC", ProsegurDbType.Objeto_Id, identificadorGrupoIAC))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Dim valores As List(Of Tuple(Of String, ObservableCollection(Of Clases.TerminoValorPosible))) = Nothing
                Dim identificadoresTerminos = (From r As DataRow In dt.Rows
                                               Where Util.AtribuirValorObj(r.Item("BOL_VALORES_POSIBLES"), GetType(Boolean)) = True
                                               Select r.Item("OID_TERMINO")).ToList

                If identificadoresTerminos IsNot Nothing AndAlso identificadoresTerminos.Count > 0 Then

                    valores = AccesoDatos.Genesis.ValorTerminoIAC.RecuperarValoresTerminosIAC(identificadoresTerminos)

                End If

                For Each row In dt.Rows
                    Dim objTerminoIAC As New Clases.TerminoIAC

                    'Objetos de chave estrangeira
                    objTerminoIAC.Formato = New Clases.Formato

                    With objTerminoIAC
                        .Identificador = Util.AtribuirValorObj(row("OID_TERMINO"), GetType(String))
                        .BuscarParcial = Util.AtribuirValorObj(row("BOL_BUSQUEDA_PARCIAL"), GetType(Boolean))
                        .EsCampoClave = Util.AtribuirValorObj(row("BOL_CAMPO_CLAVE"), GetType(Boolean))
                        .Orden = Util.AtribuirValorObj(row("NEC_ORDEN"), GetType(Integer))
                        .EsObligatorio = Util.AtribuirValorObj(row("BOL_ES_OBLIGATORIO"), GetType(Boolean))
                        .CodigoUsuario = Util.AtribuirValorObj(row("COD_USUARIO"), GetType(String))
                        .FechaHoraActualizacion = Util.AtribuirValorObj(row("FYH_ACTUALIZACION"), GetType(DateTime))
                        .EsTerminoCopia = Util.AtribuirValorObj(row("BOL_TERMINO_COPIA"), GetType(Boolean))
                        .EsProtegido = Util.AtribuirValorObj(row("BOL_ES_PROTEGIDO"), GetType(Boolean))
                        .CodigoMigracion = Util.AtribuirValorObj(row("COD_MIGRACION"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(row("COD_TERMINO"), GetType(String))
                        .Observacion = Util.AtribuirValorObj(row("OBS_TERMINO"), GetType(String))
                        .Longitud = Util.AtribuirValorObj(row("NEC_LONGITUD"), GetType(Integer))
                        .MostrarDescripcionConCodigo = Util.AtribuirValorObj(row("BOL_MOSTRAR_CODIGO"), GetType(Boolean))
                        .TieneValoresPosibles = Util.AtribuirValorObj(row("BOL_VALORES_POSIBLES"), GetType(Boolean))
                        .AceptarDigitacion = Util.AtribuirValorObj(row("BOL_ACEPTAR_DIGITACION"), GetType(Boolean))
                        .EstaActivo = Util.AtribuirValorObj(row("BOL_VIGENTE"), GetType(Boolean))
                        .EsEspecificoDeSaldos = Util.AtribuirValorObj(row("BOL_ESPECIFICO_DE_SALDOS"), GetType(Boolean))
                        .Descripcion = Util.AtribuirValorObj(row("DES_TERMINO"), GetType(String))

                        With objTerminoIAC.Formato
                            .Identificador = Util.AtribuirValorObj(row("OID_FORMATO"), GetType(String))
                            .Codigo = Util.AtribuirValorObj(row("COD_FORMATO"), GetType(String))
                            .CodigoUsuario = Util.AtribuirValorObj(row("F_COD_USUARIO"), GetType(String))
                            .Descripcion = Util.AtribuirValorObj(row("DES_FORMATO"), GetType(String))
                            .FechaHoraActualizacion = Util.AtribuirValorObj(row("F_FYH_ACTUALIZACION"), GetType(DateTime))
                        End With

                        'Verifica se possui algoritimo de validação.
                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(row("OID_ALGORITMO_VALIDACION"), GetType(String))) Then
                            objTerminoIAC.AlgoritmoValidacion = New Clases.AlgoritmoValidacion
                            With objTerminoIAC.AlgoritmoValidacion
                                .Identificador = Util.AtribuirValorObj(row("OID_ALGORITMO_VALIDACION"), GetType(String))
                                .Codigo = Util.AtribuirValorObj(row("COD_ALGORITMO_VALIDACION"), GetType(String))
                                .Descripcion = Util.AtribuirValorObj(row("DES_ALGORITMO_VALIDACION"), GetType(String))
                                .Observacion = Util.AtribuirValorObj(row("OBS_ALGORITMO_VALIDACION"), GetType(String))
                                .CodigoUsuario = Util.AtribuirValorObj(row("AV_COD_USUARIO"), GetType(String))
                                .FechaHoraAplicacion = Util.AtribuirValorObj(row("AV_FYH_ACTUALIZACION"), GetType(DateTime))
                            End With
                        End If

                        'Verifica se possui mascara.
                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(row("OID_MASCARA"), GetType(String))) Then
                            objTerminoIAC.Mascara = New Clases.Mascara
                            With objTerminoIAC.Mascara
                                .Identificador = Util.AtribuirValorObj(row("OID_MASCARA"), GetType(String))
                                .Codigo = Util.AtribuirValorObj(row("COD_MASCARA"), GetType(String))
                                .Descripcion = Util.AtribuirValorObj(row("DES_MASCARA"), GetType(String))
                                .ExpresionRegular = Util.AtribuirValorObj(row("DES_EXP_REGULAR"), GetType(String))
                                .CodigoUsuario = Util.AtribuirValorObj(row("M_COD_USUARIO"), GetType(String))
                                .FechaHoraActualizacion = Util.AtribuirValorObj(row("M_FYH_ACTUALIZACION"), GetType(DateTime))
                            End With
                        End If

                        If valores IsNot Nothing AndAlso valores.Count > 0 Then

                            If valores.Exists(Function(e) e.Item1 = .Identificador) Then
                                .ValoresPosibles = valores.FirstOrDefault(Function(v) v.Item1 = .Identificador).Item2
                            End If

                        End If

                    End With

                    listaTerminoIAC.Add(objTerminoIAC)
                Next
            End If
            Return New ObservableCollection(Of Clases.TerminoIAC)(listaTerminoIAC.OrderBy(Function(term) term.Orden).ToList())
        End Function

        Public Shared Function RecuperarTerminosConValoresPosiblesIAC(identificadorIAC As String) As ObservableCollection(Of Clases.TerminoIAC)

            Dim listaTerminoIAC As New ObservableCollection(Of Clases.TerminoIAC)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TerminoIACRecuperarPorGrupoTermino)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC", ProsegurDbType.Objeto_Id, identificadorIAC))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim valores As List(Of Tuple(Of String, ObservableCollection(Of Clases.TerminoValorPosible))) = Nothing
                Dim identificadoresTerminos = (From r As DataRow In dt.Rows
                                               Where Util.AtribuirValorObj(r.Item("BOL_VALORES_POSIBLES"), GetType(Boolean)) = True
                                               Select r.Item("OID_TERMINO")).ToList

                If identificadoresTerminos IsNot Nothing AndAlso identificadoresTerminos.Count > 0 Then

                    valores = AccesoDatos.Genesis.ValorTerminoIAC.RecuperarValoresTerminosIAC(identificadoresTerminos)

                End If

                For Each row In dt.Rows

                    Dim objTerminoIAC As New Clases.TerminoIAC

                    'Objetos de chave estrangeira
                    objTerminoIAC.Formato = New Clases.Formato

                    With objTerminoIAC
                        .Identificador = Util.AtribuirValorObj(row("OID_TERMINO"), GetType(String))
                        .BuscarParcial = Util.AtribuirValorObj(row("BOL_BUSQUEDA_PARCIAL"), GetType(Boolean))
                        .EsCampoClave = Util.AtribuirValorObj(row("BOL_CAMPO_CLAVE"), GetType(Boolean))
                        .Orden = Util.AtribuirValorObj(row("NEC_ORDEN"), GetType(Integer))
                        .EsObligatorio = Util.AtribuirValorObj(row("BOL_ES_OBLIGATORIO"), GetType(Boolean))
                        .CodigoUsuario = Util.AtribuirValorObj(row("COD_USUARIO"), GetType(String))
                        .FechaHoraActualizacion = Util.AtribuirValorObj(row("FYH_ACTUALIZACION"), GetType(DateTime))
                        .EsTerminoCopia = Util.AtribuirValorObj(row("BOL_TERMINO_COPIA"), GetType(Boolean))
                        .EsProtegido = Util.AtribuirValorObj(row("BOL_ES_PROTEGIDO"), GetType(Boolean))
                        .CodigoMigracion = Util.AtribuirValorObj(row("COD_MIGRACION"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(row("COD_TERMINO"), GetType(String))
                        .Observacion = Util.AtribuirValorObj(row("OBS_TERMINO"), GetType(String))
                        .Longitud = Util.AtribuirValorObj(row("NEC_LONGITUD"), GetType(Integer))
                        .MostrarDescripcionConCodigo = Util.AtribuirValorObj(row("BOL_MOSTRAR_CODIGO"), GetType(Boolean))
                        .TieneValoresPosibles = Util.AtribuirValorObj(row("BOL_VALORES_POSIBLES"), GetType(Boolean))
                        .AceptarDigitacion = Util.AtribuirValorObj(row("BOL_ACEPTAR_DIGITACION"), GetType(Boolean))
                        .EstaActivo = Util.AtribuirValorObj(row("BOL_VIGENTE"), GetType(Boolean))
                        .EsEspecificoDeSaldos = Util.AtribuirValorObj(row("BOL_ESPECIFICO_DE_SALDOS"), GetType(Boolean))
                        .Descripcion = Util.AtribuirValorObj(row("DES_TERMINO"), GetType(String))

                        With objTerminoIAC.Formato
                            .Identificador = Util.AtribuirValorObj(row("OID_FORMATO"), GetType(String))
                            .Codigo = Util.AtribuirValorObj(row("COD_FORMATO"), GetType(String))
                            .CodigoUsuario = Util.AtribuirValorObj(row("F_COD_USUARIO"), GetType(String))
                            .Descripcion = Util.AtribuirValorObj(row("DES_FORMATO"), GetType(String))
                            .FechaHoraActualizacion = Util.AtribuirValorObj(row("F_FYH_ACTUALIZACION"), GetType(DateTime))
                        End With

                        'Verifica se possui algoritimo de validação.
                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(row("OID_ALGORITMO_VALIDACION"), GetType(String))) Then
                            objTerminoIAC.AlgoritmoValidacion = New Clases.AlgoritmoValidacion
                            With objTerminoIAC.AlgoritmoValidacion
                                .Identificador = Util.AtribuirValorObj(row("OID_ALGORITMO_VALIDACION"), GetType(String))
                                .Codigo = Util.AtribuirValorObj(row("COD_ALGORITMO_VALIDACION"), GetType(String))
                                .Descripcion = Util.AtribuirValorObj(row("DES_ALGORITMO_VALIDACION"), GetType(String))
                                .Observacion = Util.AtribuirValorObj(row("OBS_ALGORITMO_VALIDACION"), GetType(String))
                                .CodigoUsuario = Util.AtribuirValorObj(row("AV_COD_USUARIO"), GetType(String))
                                .FechaHoraAplicacion = Util.AtribuirValorObj(row("AV_FYH_ACTUALIZACION"), GetType(DateTime))
                            End With
                        End If

                        'Verifica se possui mascara.
                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(row("OID_MASCARA"), GetType(String))) Then
                            objTerminoIAC.Mascara = New Clases.Mascara
                            With objTerminoIAC.Mascara
                                .Identificador = Util.AtribuirValorObj(row("OID_MASCARA"), GetType(String))
                                .Codigo = Util.AtribuirValorObj(row("COD_MASCARA"), GetType(String))
                                .Descripcion = Util.AtribuirValorObj(row("DES_MASCARA"), GetType(String))
                                .ExpresionRegular = Util.AtribuirValorObj(row("DES_EXP_REGULAR"), GetType(String))
                                .CodigoUsuario = Util.AtribuirValorObj(row("M_COD_USUARIO"), GetType(String))
                                .FechaHoraActualizacion = Util.AtribuirValorObj(row("M_FYH_ACTUALIZACION"), GetType(DateTime))
                            End With
                        End If

                        If valores IsNot Nothing AndAlso valores.Count > 0 Then

                            If valores.Exists(Function(e) e.Item1 = .Identificador) Then
                                .ValoresPosibles = valores.FirstOrDefault(Function(v) v.Item1 = .Identificador).Item2
                            End If

                        End If

                    End With

                    listaTerminoIAC.Add(objTerminoIAC)

                Next

            End If

            Return listaTerminoIAC

        End Function

    End Class

End Namespace


