Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Text
Imports System.Collections.ObjectModel

Namespace Genesis

    ''' <summary>
    ''' Classe Denominacion
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Denominacion

        Shared Function ObtenerDenominacionesPorDivisa_v2(codigosDivisas As List(Of String),
                                                          identificadoresDivisas As List(Of String),
                                                          codigosDenominaciones As List(Of String),
                                                          identificadoresDenominaciones As List(Of String)) As DataTable
            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.DenominacionObtener_v2
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
                        filtro &= " AND D.OID_DIVISA = []OID_DIVISA "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Descricao_Curta, identificadoresDivisas(0)))
                    ElseIf identificadoresDivisas.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresDivisas, "OID_DIVISA", cmd, "AND", "D", , False)
                    End If
                End If

                If codigosDenominaciones IsNot Nothing Then
                    If codigosDenominaciones.Count = 1 Then
                        filtro &= " AND D.COD_DENOMINACION = []COD_DENOMINACION "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DENOMINACION", ProsegurDbType.Descricao_Curta, codigosDenominaciones(0)))
                    ElseIf codigosDenominaciones.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosDenominaciones, "COD_DENOMINACION", cmd, "AND", "D", , False)
                    End If
                End If

                If identificadoresDenominaciones IsNot Nothing Then
                    If identificadoresDenominaciones.Count = 1 Then
                        filtro &= " AND D.OID_DENOMINACION = []OID_DENOMINACION "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DENOMINACION", ProsegurDbType.Descricao_Curta, identificadoresDenominaciones(0)))
                    ElseIf identificadoresDenominaciones.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresDenominaciones, "OID_DENOMINACION", cmd, "AND", "D", , False)
                    End If
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function

        Shared Function ObtenerDenominacionesPorDivisa_ConTransaccion(codigosDivisas As List(Of String),
                                                          identificadoresDivisas As List(Of String),
                                                          codigosDenominaciones As List(Of String),
                                                          identificadoresDenominaciones As List(Of String),
                                                          ByRef transaccion As DataBaseHelper.Transaccion) As DataTable

            Dim wrapper As New DataBaseHelper.SPWrapper(String.Empty, False, CommandType.Text)
            Dim query As String = My.Resources.DenominacionObtener_v2
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
                    filtro &= " AND D.OID_DIVISA = []OID_DIVISA "
                    wrapper.AgregarParam("OID_DIVISA", ProsegurDbType.Descricao_Curta, identificadoresDivisas(0))
                ElseIf identificadoresDivisas.Count > 0 Then
                    filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresDivisas, "OID_DIVISA", wrapper, transaccion, "AND", "D", , False)
                End If
            End If

            If codigosDenominaciones IsNot Nothing Then
                If codigosDenominaciones.Count = 1 Then
                    filtro &= " AND D.COD_DENOMINACION = []COD_DENOMINACION "
                    wrapper.AgregarParam("COD_DENOMINACION", ProsegurDbType.Descricao_Curta, codigosDenominaciones(0))
                ElseIf codigosDenominaciones.Count > 0 Then
                    filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosDenominaciones, "COD_DENOMINACION", wrapper, transaccion, "AND", "D", , False)
                End If
            End If

            If identificadoresDenominaciones IsNot Nothing Then
                If identificadoresDenominaciones.Count = 1 Then
                    filtro &= " AND D.OID_DENOMINACION = []OID_DENOMINACION "
                    wrapper.AgregarParam("OID_DENOMINACION", ProsegurDbType.Descricao_Curta, identificadoresDenominaciones(0))
                ElseIf identificadoresDenominaciones.Count > 0 Then
                    filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresDenominaciones, "OID_DENOMINACION", wrapper, transaccion, "AND", "D", , False)
                End If
            End If

            wrapper.SP = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))

            Dim Ds As DataSet = DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

            Return IIf(Ds IsNot Nothing AndAlso Ds.Tables.Count > 0, Ds.Tables(0), New DataTable)
        End Function


        Public Shared Function ObtenerValoresElemento_v2(ByRef identificadoresRemesas As List(Of String)) As DataTable

            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.ValoresDenominacionElemento_v2
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

                Dim query As String = My.Resources.ValoresDenominacionElemento_v3
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

        Public Shared Function ObtenerValoresAbonosValores(ByRef identificadorAbonoValor As String) As DataTable

            Dim dt As New DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandText = My.Resources.AbonoRecuperarValoresAbonosValores

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ABONO", ProsegurDbType.Identificador_Alfanumerico, identificadorAbonoValor))

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function
        Public Shared Function ObtenerValoresElementoAbono(identificadoresRemesas As List(Of String), tipoValor As Enumeradores.TipoValor) As DataTable

            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String

                If tipoValor = Enumeradores.TipoValor.Declarado Then
                    query = My.Resources.ValoresDenominacionAbonoDeclarado
                Else
                    query = My.Resources.ValoresDenominacionAbonoContado
                End If

                Dim filtro As String = ""

                If identificadoresRemesas IsNot Nothing Then
                    If identificadoresRemesas.Count = 1 Then
                        filtro &= " AND EFEC.OID_REMESA = []OID_REMESA "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Descricao_Curta, identificadoresRemesas(0)))
                    ElseIf identificadoresRemesas.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresRemesas, "OID_REMESA", cmd, "AND", "EFEC", , False)
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
        ''' Retorna os dados da denominação
        ''' </summary>
        ''' <param name="OidDenominacion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarDenominacion(OidDenominacion As String) As Clases.Denominacion

            Dim objDenominacion As Clases.Denominacion = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DenominacionRecuperar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DENOMINACION", ProsegurDbType.Objeto_Id, OidDenominacion))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objDenominacion = New Clases.Denominacion

                With objDenominacion
                    .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_DENOMINACION"), GetType(String))
                    .CodigoUsuario = Util.AtribuirValorObj(dt.Rows(0)("COD_USUARIO"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_DENOMINACION"), GetType(String))
                    .EsBillete = Util.AtribuirValorObj(dt.Rows(0)("BOL_BILLETE"), GetType(String))
                    .EstaActivo = Util.AtribuirValorObj(dt.Rows(0)("BOL_VIGENTE"), GetType(String))
                    .FechaHoraActualizacion = Util.AtribuirValorObj(dt.Rows(0)("FYH_ACTUALIZACION"), GetType(String))
                    .Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_DENOMINACION"), GetType(String))
                    .Valor = Util.AtribuirValorObj(dt.Rows(0)("NUM_VALOR"), GetType(String))
                End With
            End If

            Return objDenominacion
        End Function

        ''' <summary>
        ''' Retorna uma lista de denominações
        ''' </summary>
        ''' <param name="ListaIdentificadores">Lista de identificadores de denominações</param>
        ''' <param name="EsNotIn">Define se irá executar NotIn ou In</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarDenominaciones(IdentificadorDivisa As String, _
                                                       ListaIdentificadores As ObservableCollection(Of String), _
                                              Optional EsNotIn As Boolean = False) As ObservableCollection(Of Clases.Denominacion)

            Dim objDenominacion As Clases.Denominacion = Nothing

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DenominacionesRecuperar)
            Comando.CommandType = CommandType.Text

            If Not String.IsNullOrEmpty(IdentificadorDivisa) Then
                Comando.CommandText &= " AND D.OID_DIVISA = []OID_DIVISA "
                Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Objeto_Id, IdentificadorDivisa))
            End If

            If ListaIdentificadores IsNot Nothing AndAlso ListaIdentificadores.Count > 0 Then
                Comando.CommandText &= (Util.PrepararQuery(Constantes.CONEXAO_GENESIS, Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, ListaIdentificadores, "OID_DENOMINACION", Comando, "AND", "D", , EsNotIn)))
            End If

            Comando.CommandText &= " ORDER BY D.BOL_BILLETE DESC, D.NUM_VALOR DESC"
            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, Comando.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)
            Dim Denominaciones As New ObservableCollection(Of Clases.Denominacion)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each row In dt.Rows
                    objDenominacion = New Clases.Denominacion

                    With objDenominacion
                        .Identificador = Util.AtribuirValorObj(row("OID_DENOMINACION"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(row("COD_DENOMINACION"), GetType(String))
                        .CodigoUsuario = Util.AtribuirValorObj(row("COD_USUARIO"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(row("DES_DENOMINACION"), GetType(String))
                        .EsBillete = Util.AtribuirValorObj(row("BOL_BILLETE"), GetType(String))
                        .EstaActivo = Util.AtribuirValorObj(row("BOL_VIGENTE"), GetType(String))
                        .FechaHoraActualizacion = Util.AtribuirValorObj(row("FYH_ACTUALIZACION"), GetType(String))
                        .Valor = Util.AtribuirValorObj(row("NUM_VALOR"), GetType(String))
                    End With
                    Denominaciones.Add(objDenominacion)

                Next row

            End If

            Return If(Denominaciones.Count = 0, Nothing, Denominaciones)
        End Function

        ''' <summary>
        ''' Obtener denominaciones (Componentes de los reportes)
        ''' </summary>
        ''' <param name="Filtros">Recibe un keyvaluepair conteniendo el Campo del filtro y una lista con valores para el filtro</param>
        ''' <param name="EsActivo">Recibi un valor boleano para las divisas activas o inactivas</param>
        ''' <param name="Ordenacion">Recibe un enumerador de ordenación</param>
        ''' <returns>Lista de divisas</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerDenominaciones(Filtros As List(Of KeyValuePair(Of String, List(Of String))), _
                                                     EsActivo As Boolean, _
                                                     Ordenacion As String) As ObservableCollection(Of Clases.Denominacion)

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim Consulta As New StringBuilder()
            Consulta.AppendLine(My.Resources.ObtenerDenominaciones)

            If Filtros IsNot Nothing AndAlso Filtros.Count > 0 Then
                For Each filtro In Filtros
                    Consulta.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, filtro.Value, filtro.Key, Comando, " AND", "D", , False))

                Next filtro

            Else
                Return Nothing

            End If

            If EsActivo Then
                Consulta.AppendLine(" AND D.BOL_VIGENTE = []BOL_VIGENTE")
                Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_VIGENTE", ProsegurDbType.Logico, EsActivo))

            End If

            If Not String.IsNullOrEmpty(Ordenacion) Then
                Consulta.AppendLine(Ordenacion)

            Else
                Consulta.AppendLine(" ORDER BY DIV.DES_DIVISA, D.BOL_BILLETE DESC, D.NUM_VALOR")

            End If

            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, Consulta.ToString)
            Comando.CommandType = CommandType.Text

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)
            Dim Denominaciones As New ObservableCollection(Of Clases.Denominacion)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row In dt.Rows
                    Denominaciones.Add(CargarDenominacion(row))

                Next row

            End If

            Return If(Denominaciones.Count = 0, Nothing, Denominaciones)
        End Function

        Public Shared Function RecuperarDenominaciones(IdentificadorDivisa As String, CodigoIso As String) As ObservableCollection(Of Clases.Denominacion)

            Dim objDenominacion As Clases.Denominacion = Nothing

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerDenominaciones)
            Comando.CommandType = CommandType.Text

            If Not String.IsNullOrEmpty(IdentificadorDivisa) Then
                Comando.CommandText &= " AND D.OID_DIVISA = []OID_DIVISA "
                Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Objeto_Id, IdentificadorDivisa))
            Else

                If Not String.IsNullOrEmpty(CodigoIso) Then
                    Comando.CommandText &= " AND DIV.COD_ISO_DIVISA = []COD_ISO_DIVISA "
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ISO_DIVISA", ProsegurDbType.Objeto_Id, CodigoIso))
                End If

            End If
            Comando.CommandText &= " AND D.BOL_VIGENTE = 1 ORDER BY D.BOL_BILLETE DESC, D.NUM_VALOR DESC"
            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, Comando.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)
            Dim Denominaciones As New ObservableCollection(Of Clases.Denominacion)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each row In dt.Rows
                    objDenominacion = New Clases.Denominacion

                    With objDenominacion
                        .Identificador = Util.AtribuirValorObj(row("OID_DENOMINACION"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(row("COD_DENOMINACION"), GetType(String))
                        .CodigoUsuario = Util.AtribuirValorObj(row("COD_USUARIO"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(row("DES_DENOMINACION"), GetType(String))
                        .EsBillete = Util.AtribuirValorObj(row("BOL_BILLETE"), GetType(String))
                        .EstaActivo = Util.AtribuirValorObj(row("BOL_VIGENTE"), GetType(String))
                        .FechaHoraActualizacion = Util.AtribuirValorObj(row("FYH_ACTUALIZACION"), GetType(String))
                        .Valor = Util.AtribuirValorObj(row("NUM_VALOR"), GetType(String))
                    End With
                    Denominaciones.Add(objDenominacion)

                Next row

            End If

            Return If(Denominaciones.Count = 0, Nothing, Denominaciones)
        End Function

        ''' <summary>
        ''' Metodo getDenominacionesByDivisas
        ''' </summary>
        ''' <param name="Identificador"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerDenominacionesPorDivisa(Identificador As String, _
                                                     Optional EsActivoDenominacion As Boolean = False,
                                                     Optional EliminarDenominacionZero As Boolean = False,
                                                     Optional Totales As ObservableCollection(Of Clases.ImporteTotal) = Nothing) As ObservableCollection(Of Clases.Denominacion)

            ' criar comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            ' obter comando sql
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerDenominacionesPorDivisa.ToString)
            comando.CommandType = CommandType.Text

            ' criar parameter
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Objeto_Id, Identificador))

            ' criar objeto denominacion coleccion
            Dim ListaDenominaciones As New ObservableCollection(Of Clases.Denominacion)

            ' executar query
            Dim dtDenominaciones As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

            ' se encontrou algum registro
            If dtDenominaciones IsNot Nothing AndAlso dtDenominaciones.Rows.Count > 0 Then

                ' percorrer os registros encontrados
                For Each row As DataRow In dtDenominaciones.Rows

                    Dim EsActivo As Boolean
                    Util.AtribuirValorObjeto(EsActivo, row("BOL_VIGENTE"), GetType(Boolean))

                    If EsActivoDenominacion AndAlso Not EsActivo Then
                        Continue For
                    End If

                    Dim Valor As Decimal = 0
                    Util.AtribuirValorObjeto(Valor, row("NUM_VALOR"), GetType(Decimal))
                    If EliminarDenominacionZero AndAlso Valor = 0 Then
                        Continue For

                    End If

                    ' adicionar divisa para coleção
                    ListaDenominaciones.Add(PopularDenominaciones(row))

                Next row

                ' retornar coleção de divisas
                Return ListaDenominaciones

            End If

            Return Nothing

        End Function

        ''' <summary>
        ''' Popula o objeto denominacion através de datarows
        ''' </summary>
        ''' <param name="row"></param>
        ''' <returns></returns>
        ''' <history>
        ''' [marcel.espiritosanto] 22/08/2013 Criado
        ''' </history>
        Private Shared Function PopularDenominaciones(row As DataRow) As Clases.Denominacion


            ' criar objeto denominaciones
            Dim objDenominacion As New Clases.Denominacion

            Util.AtribuirValorObjeto(objDenominacion.Identificador, row("OID_DENOMINACION"), GetType(String))
            Util.AtribuirValorObjeto(objDenominacion.Codigo, row("COD_DENOMINACION"), GetType(String))
            Util.AtribuirValorObjeto(objDenominacion.Descripcion, row("DES_DENOMINACION"), GetType(String))
            Util.AtribuirValorObjeto(objDenominacion.EsBillete, row("BOL_BILLETE"), GetType(Boolean))
            Util.AtribuirValorObjeto(objDenominacion.Valor, row("NUM_VALOR"), GetType(Decimal))
            Util.AtribuirValorObjeto(objDenominacion.Peso, row("NUM_PESO"), GetType(Decimal))
            Util.AtribuirValorObjeto(objDenominacion.EstaActivo, row("BOL_VIGENTE"), GetType(Boolean))
            Util.AtribuirValorObjeto(objDenominacion.CodigoUsuario, row("COD_USUARIO"), GetType(String))
            Util.AtribuirValorObjeto(objDenominacion.FechaHoraActualizacion, row("FYH_ACTUALIZACION"), GetType(DateTime))

            Return objDenominacion

        End Function
#End Region

#Region "Metodos"

        ''' <summary>
        ''' Crear objeto denominacion
        ''' </summary>
        ''' <param name="row">Línea de la tabla</param>
        ''' <returns>Objeto Denominacion</returns>
        ''' <remarks></remarks>
        Private Shared Function CargarDenominacion(row As DataRow) As Clases.Denominacion

            Return New Clases.Denominacion() With {.Identificador = Util.AtribuirValorObj(row("OID_DENOMINACION"), GetType(String)), _
                                                   .Codigo = Util.AtribuirValorObj(row("COD_DENOMINACION"), GetType(String)), _
                                                   .CodigoUsuario = Util.AtribuirValorObj(row("COD_USUARIO"), GetType(String)), _
                                                   .Descripcion = Util.AtribuirValorObj(row("DES_DENOMINACION"), GetType(String)), _
                                                   .EsBillete = Util.AtribuirValorObj(row("BOL_BILLETE"), GetType(String)), _
                                                   .EstaActivo = Util.AtribuirValorObj(row("BOL_VIGENTE"), GetType(String)), _
                                                   .FechaHoraActualizacion = Util.AtribuirValorObj(row("FYH_ACTUALIZACION"), GetType(String)), _
                                                   .Valor = Util.AtribuirValorObj(row("NUM_VALOR"), GetType(String))}

        End Function

#End Region





    End Class

End Namespace