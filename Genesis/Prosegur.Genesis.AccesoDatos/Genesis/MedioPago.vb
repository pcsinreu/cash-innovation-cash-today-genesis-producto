Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Text
Imports System.Collections.ObjectModel

Namespace Genesis

    ''' <summary>
    ''' Classe MedioPago
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MedioPago

        Shared Function ObtenerMedioPagosPorDivisa_v2(codigosDivisas As List(Of String), identificadoresDivisas As List(Of String),
                                                      codigosMediosPagos As List(Of String), identificadoresMediosPagos As List(Of String)) As DataTable
            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.ObtenerMediosPago_v2
                Dim filtro As String = ""

                If codigosDivisas IsNot Nothing Then
                    If codigosDivisas.Count = 1 Then
                        filtro &= " AND DIV.COD_ISO_DIVISA = []COD_ISO_DIVISA "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ISO_DIVISA", ProsegurDbType.Descricao_Curta, codigosDivisas(0)))
                    ElseIf codigosDivisas.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosDivisas, "COD_ISO_DIVISA", cmd, "AND", "DIV", , False)
                    End If
                End If

                If identificadoresDivisas IsNot Nothing Then
                    If identificadoresDivisas.Count = 1 Then
                        filtro &= " AND MP.OID_DIVISA = []OID_DIVISA "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Descricao_Curta, identificadoresDivisas(0)))
                    ElseIf identificadoresDivisas.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresDivisas, "OID_DIVISA", cmd, "AND", "MP", , False)
                    End If
                End If

                If codigosMediosPagos IsNot Nothing Then
                    If codigosMediosPagos.Count = 1 Then
                        filtro &= " AND MP.COD_MEDIO_PAGO = []COD_MEDIO_PAGO "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_MEDIO_PAGO", ProsegurDbType.Descricao_Curta, codigosMediosPagos(0)))
                    ElseIf codigosMediosPagos.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosMediosPagos, "COD_MEDIO_PAGO", cmd, "AND", "MP", , False)
                    End If
                End If

                If identificadoresMediosPagos IsNot Nothing Then
                    If identificadoresMediosPagos.Count = 1 Then
                        filtro &= " AND MP.OID_MEDIO_PAGO = []OID_MEDIO_PAGO "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_MEDIO_PAGO", ProsegurDbType.Descricao_Curta, identificadoresMediosPagos(0)))
                    ElseIf identificadoresMediosPagos.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresMediosPagos, "OID_MEDIO_PAGO", cmd, "AND", "MP", , False)
                    End If
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function

        Shared Function ObtenerMedioPagosPorDivisa_ConTransacion(codigosDivisas As List(Of String), identificadoresDivisas As List(Of String),
                                                      codigosMediosPagos As List(Of String), identificadoresMediosPagos As List(Of String),
                                                      ByRef transaccion As DataBaseHelper.Transaccion) As DataTable



            Dim wrapper As New DataBaseHelper.SPWrapper(String.Empty, False, CommandType.Text)

            Dim query As String = My.Resources.ObtenerMediosPago_v2
            Dim filtro As String = ""

            If codigosDivisas IsNot Nothing Then
                If codigosDivisas.Count = 1 Then
                    filtro &= " AND DIV.COD_ISO_DIVISA = []COD_ISO_DIVISA "
                    wrapper.AgregarParam("COD_ISO_DIVISA", ProsegurDbType.Descricao_Curta, codigosDivisas(0))
                ElseIf codigosDivisas.Count > 0 Then
                    filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosDivisas, "COD_ISO_DIVISA", wrapper, transaccion, "AND", "DIV", , False)
                End If
            End If

            If identificadoresDivisas IsNot Nothing Then
                If identificadoresDivisas.Count = 1 Then
                    filtro &= " AND MP.OID_DIVISA = []OID_DIVISA "
                    wrapper.AgregarParam("OID_DIVISA", ProsegurDbType.Descricao_Curta, identificadoresDivisas(0))
                ElseIf identificadoresDivisas.Count > 0 Then
                    filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresDivisas, "OID_DIVISA", wrapper, transaccion, "AND", "MP", , False)
                End If
            End If

            If codigosMediosPagos IsNot Nothing Then
                If codigosMediosPagos.Count = 1 Then
                    filtro &= " AND MP.COD_MEDIO_PAGO = []COD_MEDIO_PAGO "
                    wrapper.AgregarParam("COD_MEDIO_PAGO", ProsegurDbType.Descricao_Curta, codigosMediosPagos(0))
                ElseIf codigosMediosPagos.Count > 0 Then
                    filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosMediosPagos, "COD_MEDIO_PAGO", wrapper, transaccion, "AND", "MP", , False)
                End If
            End If

            If identificadoresMediosPagos IsNot Nothing Then
                If identificadoresMediosPagos.Count = 1 Then
                    filtro &= " AND MP.OID_MEDIO_PAGO = []OID_MEDIO_PAGO "
                    wrapper.AgregarParam("OID_MEDIO_PAGO", ProsegurDbType.Descricao_Curta, identificadoresMediosPagos(0))
                ElseIf identificadoresMediosPagos.Count > 0 Then
                    filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresMediosPagos, "OID_MEDIO_PAGO", wrapper, transaccion, "AND", "MP", , False)
                End If
            End If

            wrapper.SP = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))

            Dim Ds As DataSet = DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

            Return IIf(Ds IsNot Nothing AndAlso Ds.Tables.Count > 0, Ds.Tables(0), New DataTable)

        End Function

        Shared Function ObtenerValoresElemento_v2(ByRef identificadoresRemesas As List(Of String)) As DataTable

            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.ValoresMedioPagoElemento_v2
                Dim filtro As String = ""

                If identificadoresRemesas IsNot Nothing Then
                    If identificadoresRemesas.Count = 1 Then
                        filtro &= " AND OID_REMESA = []OID_REMESA "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Descricao_Curta, identificadoresRemesas(0)))
                    ElseIf identificadoresRemesas.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresRemesas, "OID_REMESA", cmd, "AND", "", , False)
                    End If
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt

        End Function

        Public Shared Function ObtenerValoresElemento_v3(ByRef identificadoresRemesas As List(Of String)) As DataTable

            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.ValoresMedioPagoElemento_v3
                Dim filtro As String = ""

                If identificadoresRemesas IsNot Nothing Then
                    If identificadoresRemesas.Count = 1 Then
                        filtro &= " AND OID_REMESA = []OID_REMESA "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Descricao_Curta, identificadoresRemesas(0)))
                    ElseIf identificadoresRemesas.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresRemesas, "OID_REMESA", cmd, "AND", "", , False)
                    End If
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt

        End Function

        Shared Function ObtenerValoresElementoAbono(identificadoresRemesas As List(Of String), tipoValor As Enumeradores.TipoValor) As DataTable

            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String

                If tipoValor = Enumeradores.TipoValor.Declarado Then
                    query = My.Resources.ValoresMedioPagoAbonoDeclarado
                Else
                    query = My.Resources.ValoresMedioPagoAbonoContado
                End If

                Dim filtro As String = ""

                If identificadoresRemesas IsNot Nothing Then
                    If identificadoresRemesas.Count = 1 Then
                        filtro &= " AND MPV.OID_REMESA = []OID_REMESA "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Descricao_Curta, identificadoresRemesas(0)))
                    ElseIf identificadoresRemesas.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresRemesas, "OID_REMESA", cmd, "AND", "MPV", , False)
                    End If
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt

        End Function

















#Region "Consulta"

        ''' <summary>
        ''' Recuepra os dados do medio pago
        ''' </summary>
        ''' <param name="IdMedioPago"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarMedioPago(IdMedioPago As String) As Clases.MedioPago

            Dim objMedioPago As Clases.MedioPago = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.MedioPagoRecuperar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, IdMedioPago))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objMedioPago = New Clases.MedioPago

                With objMedioPago
                    .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_MEDIO_PAGO"), GetType(String))
                    .CodigoUsuario = Util.AtribuirValorObj(dt.Rows(0)("COD_USUARIO"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_MEDIO_PAGO"), GetType(String))
                    .EstaActivo = Util.AtribuirValorObj(dt.Rows(0)("BOL_VIGENTE"), GetType(String))
                    .FechaHoraActualizacion = Util.AtribuirValorObj(dt.Rows(0)("FYH_ACTUALIZACION"), GetType(String))
                    .Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_MEDIO_PAGO"), GetType(String))
                    .Terminos = TerminoMedioPago.RecuperarTerminoMedioPago(.Identificador)
                    .Tipo = RecuperarEnum(Of Enumeradores.TipoMedioPago)(Util.AtribuirValorObj(dt.Rows(0)("COD_TIPO_MEDIO_PAGO"), GetType(String)))
                    .Observacion = Util.AtribuirValorObj(dt.Rows(0)("OBS_MEDIO_PAGO"), GetType(String))
                    .Valores = Nothing
                End With

            End If

            Return objMedioPago
        End Function

        ''' <summary>
        ''' Recupera os valores do medio pago no nivel de remesa
        ''' </summary>
        ''' <param name="IdElemento"></param>
        ''' <param name="IdMedioPago"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarValorContadoMedioPago(IdElemento As String, IdMedioPago As String, TipoElemento As Enumeradores.TipoElemento, _
                                                              TerminosMedioPago As ObservableCollection(Of Clases.Termino)) As Clases.ValorMedioPago

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Select Case TipoElemento

                Case Enumeradores.TipoElemento.Remesa

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.MedioPagoRecuperarValorContadoPorRemesa)

                Case Enumeradores.TipoElemento.Bulto

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.MedioPagoRecuperarValorContadoPorBulto)

                Case Enumeradores.TipoElemento.Parcial

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.MedioPagoRecuperarValorContadoPorParcial)

            End Select

            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, IdMedioPago))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ELEMENTO", ProsegurDbType.Objeto_Id, IdElemento))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            Dim objValorMedioPago As Clases.ValorMedioPago = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objValorMedioPago = New Clases.ValorMedioPago

                With objValorMedioPago
                    .Importe = Util.AtribuirValorObj(dt.Rows(0)("NUM_IMPORTE"), GetType(Decimal))
                    .InformadoPor = Extenciones.RecuperarEnum(Of Enumeradores.TipoContado)(Util.AtribuirValorObj(dt.Rows(0)("COD_TIPO_CONTADO"), GetType(Integer)))
                    .Cantidad = Util.AtribuirValorObj(dt.Rows(0)("CANTIDAD"), GetType(Integer))
                    .TipoValor = Enumeradores.TipoValor.Contado

                    If TerminosMedioPago IsNot Nothing AndAlso TerminosMedioPago.Count > 0 Then

                        Dim objValoresTerminosMedioPago As ObservableCollection(Of Clases.Termino) = ValorTerminoMedioPagoElemento.ValorTerminosMedioPagoElementoRecuperar(Util.AtribuirValorObj(dt.Rows(0)("OID_CONTADO_MEDIO_PAGO"), GetType(String)))

                        If objValoresTerminosMedioPago IsNot Nothing AndAlso objValoresTerminosMedioPago.Count > 0 Then

                            If .Terminos Is Nothing Then .Terminos = New ObservableCollection(Of Clases.Termino)

                            Dim ListaTerminos As New ObservableCollection(Of Clases.Termino)
                            For Each vmp In objValoresTerminosMedioPago

                                Dim Termino As Clases.Termino = (From ter In TerminosMedioPago Where ter.Identificador = vmp.Identificador).FirstOrDefault

                                If Termino IsNot Nothing Then
                                    Dim nTermino As Clases.Termino = Termino.Clonar()
                                    nTermino.Valor = vmp.Valor
                                    ListaTerminos.Add(nTermino)
                                End If

                            Next

                            .Terminos = ListaTerminos.Clonar

                        End If

                    End If

                End With

            End If

            Return objValorMedioPago
        End Function

        ''' <summary>
        ''' Retorna uma lista de medios pago
        ''' </summary>
        ''' <param name="ListaIdentificadores">Lista de identificadores de denominações</param>
        ''' <param name="EsNotIn">Define se irá executar NotIn ou In</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarMediosPago(IdentificadorDivisa As String, _
                                                   ListaIdentificadores As List(Of String), _
                                                   Optional EsNotIn As Boolean = False) As ObservableCollection(Of Clases.MedioPago)

            Dim objDenominacion As Clases.Denominacion = Nothing

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Comando.CommandText = My.Resources.MediosPagoRecuperar.ToString
            Comando.CommandType = CommandType.Text

            If Not String.IsNullOrEmpty(IdentificadorDivisa) Then
                Comando.CommandText &= " AND OID_DIVISA = []OID_DIVISA"
                Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Objeto_Id, IdentificadorDivisa))
            End If

            If ListaIdentificadores IsNot Nothing AndAlso ListaIdentificadores.Count > 0 Then
                Comando.CommandText &= (Util.PrepararQuery(Constantes.CONEXAO_GENESIS, Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, ListaIdentificadores, "OID_MEDIO_PAGO", Comando, "AND", "MP", , EsNotIn)))
            End If

            Comando.CommandText &= " ORDER BY MP.COD_MEDIO_PAGO,MP.DES_MEDIO_PAGO"

            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, Comando.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)
            Dim MediosPago As New ObservableCollection(Of Clases.MedioPago)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each row In dt.Rows
                    Dim objMedioPago As New Clases.MedioPago

                    With objMedioPago
                        .Codigo = Util.AtribuirValorObj(row("COD_MEDIO_PAGO"), GetType(String))
                        .CodigoUsuario = Util.AtribuirValorObj(row("COD_USUARIO"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(row("DES_MEDIO_PAGO"), GetType(String))
                        .EstaActivo = Util.AtribuirValorObj(row("BOL_VIGENTE"), GetType(String))
                        .FechaHoraActualizacion = Util.AtribuirValorObj(row("FYH_ACTUALIZACION"), GetType(String))
                        .Identificador = Util.AtribuirValorObj(row("OID_MEDIO_PAGO"), GetType(String))
                        .Terminos = TerminoMedioPago.RecuperarTerminoMedioPago(.Identificador)
                        .Tipo = RecuperarEnum(Of Enumeradores.TipoMedioPago)(Util.AtribuirValorObj(row("COD_TIPO_MEDIO_PAGO"), GetType(String)))
                        .Observacion = Util.AtribuirValorObj(row("OBS_MEDIO_PAGO"), GetType(String))
                        .Terminos = TerminoMedioPago.ObtenerTerminosPorMedioPago(objMedioPago.Identificador)
                        .Valores = Nothing
                    End With

                    MediosPago.Add(objMedioPago)

                Next row

            End If

            Return If(MediosPago.Count = 0, Nothing, MediosPago)
        End Function

        ''' <summary>
        ''' Recuepra os dados do medio pago
        ''' </summary>
        ''' <param name="codigo">Código Médio Pago</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Shared Function RecuperarMedioPagoPorCodigo(codigo As String) As Clases.MedioPago
            Dim objMedioPago As Clases.MedioPago = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.MedioPagoRecuperarPorCodigo)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_MEDIO_PAGO", ProsegurDbType.Observacao_Longa, codigo))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objMedioPago = New Clases.MedioPago

                With objMedioPago
                    .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_MEDIO_PAGO"), GetType(String))
                    .CodigoUsuario = Util.AtribuirValorObj(dt.Rows(0)("COD_USUARIO"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_MEDIO_PAGO"), GetType(String))
                    .EstaActivo = Util.AtribuirValorObj(dt.Rows(0)("BOL_VIGENTE"), GetType(String))
                    .FechaHoraActualizacion = Util.AtribuirValorObj(dt.Rows(0)("FYH_ACTUALIZACION"), GetType(String))
                    .Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_MEDIO_PAGO"), GetType(String))
                    .Terminos = TerminoMedioPago.RecuperarTerminoMedioPago(.Identificador)
                    .Tipo = RecuperarEnum(Of Enumeradores.TipoMedioPago)(Util.AtribuirValorObj(dt.Rows(0)("COD_TIPO_MEDIO_PAGO"), GetType(String)))
                    .Observacion = Util.AtribuirValorObj(dt.Rows(0)("OBS_MEDIO_PAGO"), GetType(String))
                    .Valores = Nothing
                End With

            End If

            Return objMedioPago
        End Function

        ''' <summary>
        ''' Metodo getDenominacionesByDivisas
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerMediosPagoConTerminos(IdentificadorDivisa As String,
                                                            ListaIdentificadores As List(Of String), _
                                                            Optional EsNotIn As Boolean = False,
                                                            Optional EsActivoMedioPago As Boolean = False) As ObservableCollection(Of Clases.MedioPago)

            ' criar comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            ' obter comando sql
            comando.CommandText = My.Resources.ObtenerMediosPago.ToString
            comando.CommandType = CommandType.Text

            If EsActivoMedioPago Then
                comando.CommandText &= "WHERE MP.OID_DIVISA = []OID_DIVISA AND MP.BOL_VIGENTE = []BOL_VIGENTE"
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_VIGENTE", ProsegurDbType.Logico, True))
            Else
                comando.CommandText &= "WHERE MP.OID_DIVISA = []OID_DIVISA"
            End If

            If ListaIdentificadores IsNot Nothing AndAlso ListaIdentificadores.Count > 0 Then
                comando.CommandText &= (Util.PrepararQuery(Constantes.CONEXAO_GENESIS, Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, ListaIdentificadores, "OID_MEDIO_PAGO", comando, "AND", "MP", , EsNotIn)))
            End If

            comando.CommandText &= " ORDER BY MP.COD_MEDIO_PAGO, MP.DES_MEDIO_PAGO, T.DES_TERMINO"

            ' criar parameter
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Objeto_Id, IdentificadorDivisa))
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

            Dim dtMedioPagos As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

            Return PopularMediosPagoConTerminos(dtMedioPagos)

        End Function

        ''' <summary>
        ''' Popula o objeto medio pago através de datarows
        ''' </summary>
        ''' <param name="row"></param>
        ''' <returns></returns>
        ''' <history>
        ''' [marcel.espiritosanto] 22/08/2013 Criado
        ''' </history>
        Private Shared Function PopularMediosPago(row As DataRow) As Clases.MedioPago

            Dim objMedioPago As New Clases.MedioPago

            Util.AtribuirValorObjeto(objMedioPago.Identificador, row("OID_MEDIO_PAGO"), GetType(String))
            Util.AtribuirValorObjeto(objMedioPago.Codigo, row("COD_MEDIO_PAGO"), GetType(String))
            Util.AtribuirValorObjeto(objMedioPago.Descripcion, row("DES_MEDIO_PAGO"), GetType(String))
            Util.AtribuirValorObjeto(objMedioPago.Observacion, row("OBS_MEDIO_PAGO"), GetType(String))
            Util.AtribuirValorObjeto(objMedioPago.EstaActivo, row("BOL_VIGENTE"), GetType(Boolean))
            Util.AtribuirValorObjeto(objMedioPago.CodigoUsuario, row("COD_USUARIO"), GetType(String))
            Util.AtribuirValorObjeto(objMedioPago.FechaHoraActualizacion, row("FYH_ACTUALIZACION"), GetType(DateTime))
            objMedioPago.Tipo = EnumExtension.RecuperarEnum(Of Enumeradores.TipoMedioPago)(Util.AtribuirValorObj(row("COD_TIPO_MEDIO_PAGO"), GetType(String)))

            objMedioPago.Terminos = TerminoMedioPago.ObtenerTerminosPorMedioPago(objMedioPago.Identificador)

            Return objMedioPago

        End Function


        Private Shared Function PopularMediosPagoConTerminos(dtMedioPagos As DataTable) As ObservableCollection(Of Clases.MedioPago)

            Dim objMedioPagos As New ObservableCollection(Of Clases.MedioPago)

            ' se encontrou algum registro
            If dtMedioPagos IsNot Nothing AndAlso dtMedioPagos.Rows.Count > 0 Then

                ' percorrer os registros encontrados
                For Each objRow As DataRow In dtMedioPagos.Rows

                    If objMedioPagos.FirstOrDefault(Function(x) x.Identificador = objRow("OID_MEDIO_PAGO")) Is Nothing Then

                        Dim objMedioPago As New Clases.MedioPago

                        Util.AtribuirValorObjeto(objMedioPago.Identificador, objRow("OID_MEDIO_PAGO"), GetType(String))
                        Util.AtribuirValorObjeto(objMedioPago.Codigo, objRow("COD_MEDIO_PAGO"), GetType(String))
                        Util.AtribuirValorObjeto(objMedioPago.Descripcion, objRow("DES_MEDIO_PAGO"), GetType(String))
                        Util.AtribuirValorObjeto(objMedioPago.Observacion, objRow("OBS_MEDIO_PAGO"), GetType(String))
                        Util.AtribuirValorObjeto(objMedioPago.EstaActivo, objRow("BOL_VIGENTE"), GetType(Boolean))
                        Util.AtribuirValorObjeto(objMedioPago.CodigoUsuario, objRow("COD_USUARIO"), GetType(String))
                        Util.AtribuirValorObjeto(objMedioPago.FechaHoraActualizacion, objRow("FYH_ACTUALIZACION"), GetType(DateTime))
                        objMedioPago.Tipo = EnumExtension.RecuperarEnum(Of Enumeradores.TipoMedioPago)(Util.AtribuirValorObj(objRow("COD_TIPO_MEDIO_PAGO"), GetType(String)))

                        objMedioPago.Terminos = New ObservableCollection(Of Clases.Termino)
                        objMedioPago.Terminos.Add(TerminoMedioPago.PopularMedioPagosConTerminos(objRow))
                    Else
                        objMedioPagos.FirstOrDefault(Function(x) x.Identificador = objRow("OID_MEDIO_PAGO")).Terminos.Add(TerminoMedioPago.PopularMedioPagosConTerminos(objRow))
                    End If

                Next

            End If

            Return objMedioPagos

        End Function

        ''' <summary>
        ''' Metodo getDenominacionesByDivisas
        ''' </summary>
        ''' <param name="Identificador"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerMediosPagoPorDivisa(Identificador As String, _
                                                 Optional EsActivoMedioPago As Boolean = True) As ObservableCollection(Of Clases.MedioPago)

            ' criar comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            ' obter comando sql
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerMedioPagoPorDivisa.ToString)
            comando.CommandType = CommandType.Text

            ' criar parameter
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Objeto_Id, Identificador))

            If EsActivoMedioPago Then
                comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(comando.CommandText, " AND MP.BOL_VIGENTE = []BOL_VIGENTE  "))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_VIGENTE", ProsegurDbType.Logico, EsActivoMedioPago))
            Else
                comando.CommandText = String.Format(comando.CommandText, String.Empty)
            End If

            ' criar objeto denominacion coleccion
            Dim ListaMedioPago As New ObservableCollection(Of Clases.MedioPago)

            ' executar query
            Dim dtMedioPago As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

            ' se encontrou algum registro
            If dtMedioPago IsNot Nothing AndAlso dtMedioPago.Rows.Count > 0 Then

                ' percorrer os registros encontrados
                For Each row As DataRow In dtMedioPago.Rows

                    Dim EsActivo As Boolean
                    Util.AtribuirValorObjeto(EsActivo, row("BOL_VIGENTE"), GetType(Boolean))

                    If EsActivoMedioPago AndAlso Not EsActivo Then
                        Continue For

                    End If
                    ' adicionar divisa para coleção
                    ListaMedioPago.Add(PopularMediosPago(row))

                Next row

                Return ListaMedioPago
            End If

            ' retornar coleção de medios pago
            Return Nothing

        End Function

#End Region

        

        

    End Class

End Namespace