Imports Prosegur.DbHelper
Imports Prosegur.Genesis
Imports System.Text
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports System.Threading.Tasks
Imports System.Data.OracleClient

Namespace Genesis

    Public Class Divisas


        Public Shared Function ObtenerDivisas_v2(codigosDivisas As List(Of String),
                                                  identificadoresDivisas As List(Of String),
                                                  codigosDenominaciones As List(Of String),
                                                  identificadoresDenominaciones As List(Of String),
                                                  codigosMediosPagos As List(Of String),
                                                  identificadoresMediosPagos As List(Of String)) As ObservableCollection(Of Clases.Divisa)

            Dim dtDivisas As DataTable = Nothing
            Dim dtDenominaciones As DataTable = Nothing
            Dim dtMedioPago As DataTable = Nothing

            Dim TDivisas As New Task(Sub()
                                         dtDivisas = ObtenerDivisasEnLaBase_v2(codigosDivisas, identificadoresDivisas)
                                     End Sub)
            TDivisas.Start()

            Dim TDenominaciones As New Task(Sub()
                                                dtDenominaciones = Denominacion.ObtenerDenominacionesPorDivisa_v2(codigosDivisas, identificadoresDivisas, codigosDenominaciones, identificadoresDenominaciones)
                                            End Sub)
            TDenominaciones.Start()

            Dim TMedioPago As New Task(Sub()
                                           dtMedioPago = MedioPago.ObtenerMedioPagosPorDivisa_v2(codigosDivisas, identificadoresDivisas, codigosMediosPagos, identificadoresMediosPagos)
                                       End Sub)
            TMedioPago.Start()

            ' Aguarda que as tasks terminem antes de continuar
            Task.WaitAll(New Task() {TDivisas, TDenominaciones, TMedioPago})

            Return cargarDivisas(dtDivisas, dtDenominaciones, dtMedioPago)

        End Function

        Public Shared Function ObtenerDivisasConTrasaccion(codigosDivisas As List(Of String),
                                                  identificadoresDivisas As List(Of String),
                                                  codigosDenominaciones As List(Of String),
                                                  identificadoresDenominaciones As List(Of String),
                                                  codigosMediosPagos As List(Of String),
                                                  identificadoresMediosPagos As List(Of String),
                                                  ByRef transaccion As DataBaseHelper.Transaccion) As ObservableCollection(Of Clases.Divisa)

            Dim dtDivisas As DataTable = Nothing
            Dim dtDenominaciones As DataTable = Nothing
            Dim dtMedioPago As DataTable = Nothing

            dtDivisas = ObtenerDivisasEnLaBase_ConTransaccion(codigosDivisas, identificadoresDivisas, transaccion)
            
            dtDenominaciones = Denominacion.ObtenerDenominacionesPorDivisa_ConTransaccion(codigosDivisas, identificadoresDivisas, codigosDenominaciones, identificadoresDenominaciones, transaccion)

            dtMedioPago = MedioPago.ObtenerMedioPagosPorDivisa_ConTransacion(codigosDivisas, identificadoresDivisas, codigosMediosPagos, identificadoresMediosPagos, transaccion)

            Return cargarDivisas(dtDivisas, dtDenominaciones, dtMedioPago)

        End Function

        Public Shared Function ObtenerDivisasPorCodigosConTransaccion(codigosDivisas As List(Of String), ByRef transaccion As DataBaseHelper.Transaccion) As ObservableCollection(Of Clases.Divisa)
            Return ObtenerDivisasConTrasaccion(codigosDivisas, Nothing, Nothing, Nothing, Nothing, Nothing, transaccion)
        End Function

        Public Shared Function ObtenerDivisasPorCodigos_v2(codigosDivisas As List(Of String),
                                                           codigosDenominaciones As List(Of String)) As ObservableCollection(Of Clases.Divisa)
            Return ObtenerDivisas_v2(codigosDivisas, Nothing, codigosDenominaciones, Nothing, Nothing, Nothing)
        End Function

        Public Shared Function ObtenerDivisasPorCodigos_v2(codigosDivisas As List(Of String),
                                                           codigosDenominaciones As List(Of String),
                                                           codigosMedioPagos As List(Of String)) As ObservableCollection(Of Clases.Divisa)
            Return ObtenerDivisas_v2(codigosDivisas, Nothing, codigosDenominaciones, Nothing, codigosMedioPagos, Nothing)
        End Function

        Public Shared Function ObtenerDivisasPorIdentificadores_v2(IdentificadoresDivisas As List(Of String)) As ObservableCollection(Of Clases.Divisa)
            Return ObtenerDivisas_v2(Nothing, IdentificadoresDivisas, Nothing, Nothing, Nothing, Nothing)
        End Function

        Public Shared Function ObtenerDivisasPorIdentificadores_v2(IdentificadoresDivisas As List(Of String),
                                                                   identificadoresDenominaciones As List(Of String),
                                                                   identificadoresMediosPagos As List(Of String)) As ObservableCollection(Of Clases.Divisa)
            Return ObtenerDivisas_v2(Nothing, IdentificadoresDivisas, Nothing, identificadoresDenominaciones, Nothing, identificadoresMediosPagos)
        End Function

        Public Shared Sub ObtenerValoresDeLosDocumentos_v2(ByRef documentos As ObservableCollection(Of Clases.Documento))

            If documentos IsNot Nothing AndAlso documentos.Count > 0 Then

                Dim identificadoresDocumento As List(Of String) = documentos.Select(Function(d) d.Identificador).ToList

                If identificadoresDocumento IsNot Nothing AndAlso identificadoresDocumento.Count > 0 Then

                    Try
                        Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                            Dim query As String = My.Resources.DivisasObtenerValoresDelDocumento_v2
                            Dim filtro As String = ""

                            If identificadoresDocumento.Count = 1 Then

                                filtro &= " AND OID_DOCUMENTO = []OID_DOCUMENTO "
                                command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", _
                                                                                            ProsegurDbType.Objeto_Id, identificadoresDocumento(0)))

                            Else

                                filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresDocumento, "OID_DOCUMENTO", _
                                                                                       command, "AND")

                            End If

                            command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))
                            command.CommandType = CommandType.Text

                            cargarValores_v2(documentos, AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command), True)

                        End Using

                    Catch ex As Exception
                        Throw
                    Finally
                        GC.Collect()
                    End Try

                End If
            End If
        End Sub

        Public Shared Sub cargarValores_v2(ByRef documentos As ObservableCollection(Of Clases.Documento), _
                                           ByRef dtValores As DataTable,
                                           Optional rellenarTipoValorNoDefinido As Boolean = False, _
                                           Optional esDisponibleNoDefinido As Boolean = False)

            If dtValores IsNot Nothing AndAlso dtValores.Rows.Count > 0 Then

                If documentos Is Nothing Then
                    documentos = New ObservableCollection(Of Clases.Documento)
                End If

                Dim identificadoresDocumentos As List(Of String) = documentos.Select(Function(d) d.Identificador).ToList

                Dim identificadoresDivisas As List(Of String) = dtValores.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_DIVISA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DIVISA"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_DIVISA")) _
                                                                         .Distinct() _
                                                                         .ToList()

                Dim identificadoresDenominaciones As List(Of String) = dtValores.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_DENOMINACION") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DENOMINACION"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_DENOMINACION")) _
                                                                         .Distinct() _
                                                                         .ToList()

                Dim identificadoresMediosPagos As List(Of String) = dtValores.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_MEDIO_PAGO") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_MEDIO_PAGO"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_MEDIO_PAGO")) _
                                                                         .Distinct() _
                                                                         .ToList()

                Dim identificadoresUnidadMedida As List(Of String) = dtValores.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_UNIDAD_MEDIDA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_UNIDAD_MEDIDA"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_UNIDAD_MEDIDA")) _
                                                                         .Distinct() _
                                                                         .ToList()

                Dim identificadoresCalidad As List(Of String) = dtValores.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_CALIDAD") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_CALIDAD"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_CALIDAD")) _
                                                                         .Distinct() _
                                                                         .ToList()

                Dim divisas As ObservableCollection(Of Clases.Divisa) = ObtenerDivisas_v2(Nothing, identificadoresDivisas,
                                                                                          Nothing, identificadoresDenominaciones,
                                                                                          Nothing, identificadoresMediosPagos)

                If divisas IsNot Nothing AndAlso divisas.Count > 0 Then


                    Dim dtUnidadMedida As DataTable = Nothing
                    Dim dtCalidad As DataTable = Nothing
                    Dim dtValorTermino As DataTable = Nothing

                    Dim TUnidadMedida As New Task(Sub()
                                                      dtUnidadMedida = UnidadMedida.ObtenerUnidadMedidaPorDivisa_v2(Nothing, identificadoresUnidadMedida)
                                                  End Sub)
                    TUnidadMedida.Start()

                    Dim TCalidad As New Task(Sub()
                                                 dtCalidad = Calidad.ObtenerCalidadPorDivisa_v2(Nothing, identificadoresCalidad)
                                             End Sub)
                    TCalidad.Start()

                    Dim TValorTermino As New Task(Sub()
                                                      dtValorTermino = ValorTerminoMedioPago.ObtenerValorTerminoMedioPago_v2(identificadoresDocumentos)
                                                  End Sub)
                    TValorTermino.Start()

                    Task.WaitAll(New Task() {TUnidadMedida, TCalidad, TValorTermino})

                    For Each documento In documentos

                        Dim _valores = dtValores.AsEnumerable().Where(Function(r) r.Field(Of String)("OID_DOCUMENTO") = documento.Identificador)

                        If _valores IsNot Nothing AndAlso _valores.Count > 0 Then

                            If documento.Divisas Is Nothing Then
                                documento.Divisas = New ObservableCollection(Of Clases.Divisa)
                            End If

                            Dim objDenominacion As Clases.Denominacion = Nothing
                            Dim objMedioPago As Clases.MedioPago = Nothing
                            Dim Disponible As Boolean

                            For Each valor In _valores

                                Dim divisa As Clases.Divisa = documento.Divisas.FirstOrDefault(Function(d) d.Identificador = Util.AtribuirValorObj(valor("OID_DIVISA"), GetType(String)))

                                If divisa Is Nothing Then
                                    divisa = divisas.FirstOrDefault(Function(d) d.Identificador = Util.AtribuirValorObj(valor("OID_DIVISA"), GetType(String))).Clonar()
                                    documento.Divisas.Add(divisa)
                                End If

                                If divisa IsNot Nothing Then

                                    Disponible = If(valor.Table.Columns.Contains("BOL_DISPONIBLE"), Util.AtribuirValorObj(valor("BOL_DISPONIBLE"), GetType(String)), Nothing)
                                    Dim TipoValor As Enumeradores.TipoValor = If(rellenarTipoValorNoDefinido, Enumeradores.TipoValor.NoDefinido, If(esDisponibleNoDefinido, If(Disponible, Enumeradores.TipoValor.NoDefinido, Enumeradores.TipoValor.NoDisponible), If(Disponible, Enumeradores.TipoValor.Disponible, Enumeradores.TipoValor.NoDisponible)))

                                    If valor.Table.Columns.Contains("OID_DENOMINACION") AndAlso String.IsNullOrEmpty(Util.AtribuirValorObj(valor("OID_DENOMINACION"), GetType(String))) AndAlso _
                                        valor.Table.Columns.Contains("OID_MEDIO_PAGO") AndAlso String.IsNullOrEmpty(Util.AtribuirValorObj(valor("OID_MEDIO_PAGO"), GetType(String))) Then

                                        If valor.Table.Columns.Contains("COD_TIPO_MEDIO_PAGO") AndAlso String.IsNullOrEmpty(Util.AtribuirValorObj(valor("COD_TIPO_MEDIO_PAGO"), GetType(String))) Then

                                            If valor.Table.Columns.Contains("COD_NIVEL_DETALLE") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(valor("COD_NIVEL_DETALLE"), GetType(String))) AndAlso _
                                                Util.AtribuirValorObj(valor("COD_NIVEL_DETALLE"), GetType(String)) = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor Then

                                                If divisa.ValoresTotalesEfectivo Is Nothing Then divisa.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)
                                                divisa.ValoresTotalesEfectivo.Add(New Clases.ValorEfectivo With { _
                                                                                     .TipoDetalleEfectivo = Enumeradores.TipoDetalleEfectivo.Mezcla, _
                                                                                     .TipoValor = TipoValor, _
                                                                                     .Importe = If(valor.Table.Columns.Contains("IMPORTE"), Util.AtribuirValorObj(valor("IMPORTE"), GetType(Decimal)), Nothing)})

                                            ElseIf valor.Table.Columns.Contains("COD_NIVEL_DETALLE") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(valor("COD_NIVEL_DETALLE"), GetType(String))) AndAlso _
                                                Util.AtribuirValorObj(valor("COD_NIVEL_DETALLE"), GetType(String)) = Enumeradores.TipoNivelDetalhe.TotalGeral.RecuperarValor Then

                                                If divisa.ValoresTotalesDivisa Is Nothing Then divisa.ValoresTotalesDivisa = New ObservableCollection(Of Clases.ValorDivisa)
                                                divisa.ValoresTotalesDivisa.Add(New Clases.ValorDivisa With { _
                                                                                     .TipoValor = TipoValor, _
                                                                                     .Importe = If(valor.Table.Columns.Contains("IMPORTE"), Util.AtribuirValorObj(valor("IMPORTE"), GetType(Decimal)), Nothing)})
                                            End If

                                        Else

                                            If divisa.ValoresTotalesTipoMedioPago Is Nothing Then divisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)
                                            divisa.ValoresTotalesTipoMedioPago.Add(New Clases.ValorTipoMedioPago With { _
                                                                                      .TipoValor = TipoValor, _
                                                                                      .Importe = If(valor.Table.Columns.Contains("IMPORTE"), Util.AtribuirValorObj(valor("IMPORTE"), GetType(Decimal)), Nothing), _
                                                                                      .Cantidad = If(valor.Table.Columns.Contains("CANTIDAD"), Util.AtribuirValorObj(valor("CANTIDAD"), GetType(Int64)), Nothing), _
                                                                                      .TipoMedioPago = If(valor.Table.Columns.Contains("COD_TIPO_MEDIO_PAGO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(valor("COD_TIPO_MEDIO_PAGO"), GetType(String))), Extenciones.RecuperarEnum(Of Enumeradores.TipoMedioPago)(Util.AtribuirValorObj(valor("COD_TIPO_MEDIO_PAGO"), GetType(String))), Enumeradores.TipoMedioPago.OtroValor)})

                                        End If

                                    End If

                                    If valor.Table.Columns.Contains("OID_DENOMINACION") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(valor("OID_DENOMINACION"), GetType(String))) AndAlso _
                                       valor.Table.Columns.Contains("IMPORTE") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(valor("IMPORTE"), GetType(String))) AndAlso _
                                       divisa.Denominaciones IsNot Nothing AndAlso divisa.Denominaciones.Count > 0 Then

                                        objDenominacion = (From den In divisa.Denominaciones Where den.Identificador = Util.AtribuirValorObj(valor("OID_DENOMINACION"), GetType(String))).FirstOrDefault

                                        If objDenominacion IsNot Nothing Then

                                            If objDenominacion.ValorDenominacion Is Nothing Then objDenominacion.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)

                                            Dim objValor As New Clases.ValorDenominacion
                                            objValor.Cantidad = If(valor.Table.Columns.Contains("CANTIDAD"), Util.AtribuirValorObj(valor("CANTIDAD"), GetType(Int64)), Nothing)
                                            objValor.Importe = If(valor.Table.Columns.Contains("IMPORTE"), Util.AtribuirValorObj(valor("IMPORTE"), GetType(Decimal)), Nothing)
                                            objValor.TipoValor = TipoValor

                                            If valor.Table.Columns.Contains("OID_CALIDAD") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(valor("OID_CALIDAD"), GetType(String))) AndAlso _
                                                dtCalidad IsNot Nothing AndAlso dtCalidad.Rows.Count > 0 Then

                                                Dim calidad = dtCalidad.Select("OID_CALIDAD = '" & Util.AtribuirValorObj(valor("OID_CALIDAD"), GetType(String)) & "'")

                                                If calidad IsNot Nothing AndAlso calidad.Count > 0 Then
                                                    objValor.Calidad = New Clases.Calidad With {
                                                                    .Identificador = If(calidad(0).Table.Columns.Contains("OID_CALIDAD"), Util.AtribuirValorObj(calidad(0)("OID_CALIDAD"), GetType(String)), Nothing),
                                                                    .Codigo = If(calidad(0).Table.Columns.Contains("COD_CALIDAD"), Util.AtribuirValorObj(calidad(0)("COD_CALIDAD"), GetType(String)), Nothing),
                                                                    .Descripcion = If(calidad(0).Table.Columns.Contains("DES_CALIDAD"), Util.AtribuirValorObj(calidad(0)("DES_CALIDAD"), GetType(String)), Nothing)
                                                        }
                                                End If

                                            End If

                                            If valor.Table.Columns.Contains("OID_UNIDAD_MEDIDA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(valor("OID_UNIDAD_MEDIDA"), GetType(String))) AndAlso _
                                                dtUnidadMedida IsNot Nothing AndAlso dtUnidadMedida.Rows.Count > 0 Then

                                                Dim unidadMedida = dtUnidadMedida.Select("OID_UNIDAD_MEDIDA = '" & Util.AtribuirValorObj(valor("OID_UNIDAD_MEDIDA"), GetType(String)) & "'")

                                                If unidadMedida IsNot Nothing AndAlso unidadMedida.Count > 0 Then
                                                    objValor.UnidadMedida = New Clases.UnidadMedida With {
                                                                    .Identificador = If(unidadMedida(0).Table.Columns.Contains("OID_UNIDAD_MEDIDA"), Util.AtribuirValorObj(unidadMedida(0)("OID_UNIDAD_MEDIDA"), GetType(String)), Nothing),
                                                                    .Codigo = If(unidadMedida(0).Table.Columns.Contains("COD_UNIDAD_MEDIDA"), Util.AtribuirValorObj(unidadMedida(0)("COD_UNIDAD_MEDIDA"), GetType(String)), Nothing),
                                                                    .Descripcion = If(unidadMedida(0).Table.Columns.Contains("DES_UNIDAD_MEDIDA"), Util.AtribuirValorObj(unidadMedida(0)("DES_UNIDAD_MEDIDA"), GetType(String)), Nothing),
                                                                    .EsPadron = If(unidadMedida(0).Table.Columns.Contains("BOL_DEFECTO"), Util.AtribuirValorObj(unidadMedida(0)("BOL_DEFECTO"), GetType(Boolean)), Nothing),
                                                                    .TipoUnidadMedida = RecuperarEnum(Of Enumeradores.TipoUnidadMedida)(Util.AtribuirValorObj(unidadMedida(0)("COD_TIPO_UNIDAD_MEDIDA"), GetType(String))),
                                                                    .ValorUnidad = If(unidadMedida(0).Table.Columns.Contains("NUM_VALOR_UNIDAD"), Util.AtribuirValorObj(unidadMedida(0)("NUM_VALOR_UNIDAD"), GetType(Decimal)), Nothing)
                                                        }
                                                End If
                                            End If

                                            objDenominacion.ValorDenominacion.Add(objValor)

                                        End If

                                    End If

                                    If valor.Table.Columns.Contains("OID_MEDIO_PAGO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(valor("OID_MEDIO_PAGO"), GetType(String))) AndAlso _
                                       valor.Table.Columns.Contains("IMPORTE") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(valor("IMPORTE"), GetType(String))) AndAlso _
                                       divisa.MediosPago IsNot Nothing AndAlso divisa.MediosPago.Count > 0 Then

                                        objMedioPago = (From MP In divisa.MediosPago Where MP.Identificador = Util.AtribuirValorObj(valor("OID_MEDIO_PAGO"), GetType(String))).FirstOrDefault

                                        If objMedioPago IsNot Nothing Then

                                            Dim valoresTerminos As New ObservableCollection(Of Clases.Termino)
                                            If (Not String.IsNullOrEmpty(documento.Identificador)) AndAlso objMedioPago.Terminos IsNot Nothing Then

                                                Dim valorTermino = dtValorTermino.Select("OID_DOCUMENTO = '" & documento.Identificador & "' ")

                                                If valorTermino IsNot Nothing AndAlso valorTermino.Count > 0 Then

                                                    For Each v In valorTermino
                                                        Dim termino As Clases.Termino = objMedioPago.Terminos.Where(Function(t) t.Identificador = Util.AtribuirValorObj(v("OID_TERMINO"), GetType(String))).FirstOrDefault
                                                        If termino IsNot Nothing Then
                                                            termino.Valor = Util.AtribuirValorObj(v("DES_VALOR"), GetType(String))
                                                            termino.NecIndiceGrupo = Util.AtribuirValorObj(v("NEC_INDICE_GRUPO"), GetType(String))
                                                            valoresTerminos.Add(termino)
                                                        End If
                                                    Next

                                                End If
                                            End If

                                            If objMedioPago.Valores Is Nothing Then objMedioPago.Valores = New ObservableCollection(Of Clases.ValorMedioPago)
                                            objMedioPago.Valores.Add(New Clases.ValorMedioPago With {.Cantidad = If(valor.Table.Columns.Contains("CANTIDAD"), Util.AtribuirValorObj(valor("CANTIDAD"), GetType(Int64)), Nothing), _
                                                                                                     .Importe = If(valor.Table.Columns.Contains("IMPORTE"), Util.AtribuirValorObj(valor("IMPORTE"), GetType(Decimal)), Nothing), _
                                                                                                     .Terminos = valoresTerminos, _
                                                                                                     .TipoValor = TipoValor})

                                        End If

                                    End If

                                    If valor.Table.Columns.Contains("IMPORTE") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(valor("IMPORTE"), GetType(String))) Then
                                        If divisa.ValoresTotales Is Nothing Then divisa.ValoresTotales = New ObservableCollection(Of Clases.ImporteTotal)
                                        If divisa.ValoresTotales.FirstOrDefault(Function(x) x.TipoValor = TipoValor) IsNot Nothing Then
                                            divisa.ValoresTotales.FirstOrDefault(Function(x) x.TipoValor = TipoValor).Importe += If(valor.Table.Columns.Contains("IMPORTE"), Util.AtribuirValorObj(valor("IMPORTE"), GetType(Decimal)), Nothing)
                                        Else
                                            Dim Total As New Clases.ImporteTotal
                                            Total.Importe = If(valor.Table.Columns.Contains("IMPORTE"), Util.AtribuirValorObj(valor("IMPORTE"), GetType(Decimal)), Nothing)
                                            Total.TipoValor = TipoValor
                                            divisa.ValoresTotales.Add(Total)
                                        End If
                                    End If

                                End If

                            Next

                        End If

                    Next

                End If

            End If

        End Sub

        Public Shared Function cargarDivisas(dtDivisas As DataTable, dtDenominaciones As DataTable, dtMedioPago As DataTable) As ObservableCollection(Of Clases.Divisa)

            Dim divisas As ObservableCollection(Of Clases.Divisa) = Nothing

            If dtDivisas IsNot Nothing AndAlso dtDivisas.Rows.Count > 0 Then

                divisas = New ObservableCollection(Of Clases.Divisa)

                For Each rowDivisa In dtDivisas.Rows

                    Dim divisa As New Clases.Divisa

                    With divisa

                        .ValoresTotales = New ObservableCollection(Of Clases.ImporteTotal)

                        .Identificador = If(rowDivisa.Table.Columns.Contains("OID_DIVISA"), Util.AtribuirValorObj(rowDivisa("OID_DIVISA"), GetType(String)), Nothing)
                        .CodigoISO = If(rowDivisa.Table.Columns.Contains("COD_ISO_DIVISA"), Util.AtribuirValorObj(rowDivisa("COD_ISO_DIVISA"), GetType(String)), Nothing)
                        .Descripcion = If(rowDivisa.Table.Columns.Contains("DES_DIVISA"), Util.AtribuirValorObj(rowDivisa("DES_DIVISA"), GetType(String)), Nothing)
                        .CodigoSimbolo = If(rowDivisa.Table.Columns.Contains("COD_SIMBOLO"), Util.AtribuirValorObj(rowDivisa("COD_SIMBOLO"), GetType(String)), Nothing)
                        .EstaActivo = If(rowDivisa.Table.Columns.Contains("BOL_VIGENTE"), Util.AtribuirValorObj(rowDivisa("BOL_VIGENTE"), GetType(Boolean)), Nothing)
                        .CodigoUsuario = If(rowDivisa.Table.Columns.Contains("COD_USUARIO"), Util.AtribuirValorObj(rowDivisa("COD_USUARIO"), GetType(String)), Nothing)
                        .FechaHoraTransporte = If(rowDivisa.Table.Columns.Contains("FYH_ACTUALIZACION"), Util.AtribuirValorObj(rowDivisa("FYH_ACTUALIZACION"), GetType(DateTime)), Nothing)
                        .CodigoAcceso = If(rowDivisa.Table.Columns.Contains("COD_ACCESO"), Util.AtribuirValorObj(rowDivisa("COD_ACCESO"), GetType(String)), Nothing)
                        .Color = If(rowDivisa.Table.Columns.Contains("COD_COLOR"), Util.AtribuirValorObj(rowDivisa("COD_COLOR"), GetType(Drawing.Color)), Nothing)

                        If dtDenominaciones IsNot Nothing AndAlso dtDenominaciones.Rows.Count > 0 Then

                            Dim denominaciones = dtDenominaciones.Select("OID_DIVISA = '" & Util.AtribuirValorObj(rowDivisa("OID_DIVISA"), GetType(String)) & "'")
                            If denominaciones IsNot Nothing Then
                                .Denominaciones = New ObservableCollection(Of Clases.Denominacion)

                                For Each rowDenominacion In denominaciones

                                    Dim denominacion As New Clases.Denominacion
                                    With denominacion
                                        .Identificador = If(rowDenominacion.Table.Columns.Contains("OID_DENOMINACION"), Util.AtribuirValorObj(rowDenominacion("OID_DENOMINACION"), GetType(String)), Nothing)
                                        .Codigo = If(rowDenominacion.Table.Columns.Contains("COD_DENOMINACION"), Util.AtribuirValorObj(rowDenominacion("COD_DENOMINACION"), GetType(String)), Nothing)
                                        .CodigoUsuario = If(rowDenominacion.Table.Columns.Contains("COD_USUARIO"), Util.AtribuirValorObj(rowDenominacion("COD_USUARIO"), GetType(String)), Nothing)
                                        .Descripcion = If(rowDenominacion.Table.Columns.Contains("DES_DENOMINACION"), Util.AtribuirValorObj(rowDenominacion("DES_DENOMINACION"), GetType(String)), Nothing)
                                        .EsBillete = If(rowDenominacion.Table.Columns.Contains("BOL_BILLETE"), Util.AtribuirValorObj(rowDenominacion("BOL_BILLETE"), GetType(Boolean)), Nothing)
                                        .EstaActivo = If(rowDenominacion.Table.Columns.Contains("BOL_VIGENTE"), Util.AtribuirValorObj(rowDenominacion("BOL_VIGENTE"), GetType(Boolean)), Nothing)
                                        .FechaHoraActualizacion = If(rowDenominacion.Table.Columns.Contains("FYH_ACTUALIZACION"), Util.AtribuirValorObj(rowDenominacion("FYH_ACTUALIZACION"), GetType(DateTime)), Nothing)
                                        .Valor = If(rowDenominacion.Table.Columns.Contains("NUM_VALOR"), Util.AtribuirValorObj(rowDenominacion("NUM_VALOR"), GetType(Decimal)), Nothing)
                                    End With

                                    .Denominaciones.Add(denominacion)
                                Next

                            End If

                        End If

                        If dtMedioPago IsNot Nothing AndAlso dtMedioPago.Rows.Count > 0 Then

                            Dim mediosPagos = dtMedioPago.Select("OID_DIVISA = '" & Util.AtribuirValorObj(rowDivisa("OID_DIVISA"), GetType(String)) & "'")
                            If mediosPagos IsNot Nothing Then
                                .MediosPago = New ObservableCollection(Of Clases.MedioPago)

                                For Each rowMedioPago In mediosPagos

                                    Dim _medioPago = .MediosPago.FirstOrDefault(Function(x) x.Identificador = rowMedioPago("OID_MEDIO_PAGO"))
                                    If _medioPago Is Nothing Then
                                        Dim medioPago As New Clases.MedioPago
                                        With medioPago
                                            .Identificador = If(rowMedioPago.Table.Columns.Contains("OID_MEDIO_PAGO"), Util.AtribuirValorObj(rowMedioPago("OID_MEDIO_PAGO"), GetType(String)), Nothing)
                                            .Codigo = If(rowMedioPago.Table.Columns.Contains("COD_MEDIO_PAGO"), Util.AtribuirValorObj(rowMedioPago("COD_MEDIO_PAGO"), GetType(String)), Nothing)
                                            .Descripcion = If(rowMedioPago.Table.Columns.Contains("DES_MEDIO_PAGO"), Util.AtribuirValorObj(rowMedioPago("DES_MEDIO_PAGO"), GetType(String)), Nothing)
                                            .Observacion = If(rowMedioPago.Table.Columns.Contains("OBS_MEDIO_PAGO"), Util.AtribuirValorObj(rowMedioPago("OBS_MEDIO_PAGO"), GetType(String)), Nothing)
                                            .EstaActivo = If(rowMedioPago.Table.Columns.Contains("BOL_VIGENTE"), Util.AtribuirValorObj(rowMedioPago("BOL_VIGENTE"), GetType(Boolean)), Nothing)
                                            .CodigoUsuario = If(rowMedioPago.Table.Columns.Contains("COD_USUARIO"), Util.AtribuirValorObj(rowMedioPago("COD_USUARIO"), GetType(String)), Nothing)
                                            .FechaHoraActualizacion = If(rowMedioPago.Table.Columns.Contains("FYH_ACTUALIZACION"), Util.AtribuirValorObj(rowMedioPago("FYH_ACTUALIZACION"), GetType(DateTime)), Nothing)
                                            If rowMedioPago.Table.Columns.Contains("COD_TIPO_MEDIO_PAGO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowMedioPago("COD_TIPO_MEDIO_PAGO"), GetType(String))) Then
                                                .Tipo = EnumExtension.RecuperarEnum(Of Enumeradores.TipoMedioPago)(Util.AtribuirValorObj(rowMedioPago("COD_TIPO_MEDIO_PAGO"), GetType(String)))
                                            End If
                                            .Terminos = New ObservableCollection(Of Clases.Termino)
                                            Dim termino As Clases.Termino = TerminoMedioPago.PopularMedioPagosConTerminos_v2(rowMedioPago)
                                            If termino IsNot Nothing Then
                                                .Terminos.Add(termino)
                                            End If
                                        End With
                                        .MediosPago.Add(medioPago)
                                    Else
                                        Dim termino As Clases.Termino = TerminoMedioPago.PopularMedioPagosConTerminos_v2(rowMedioPago)
                                        If termino IsNot Nothing Then

                                            Dim _termino As Clases.Termino = _medioPago.Terminos.FirstOrDefault(Function(x) x.Identificador = termino.Identificador)

                                            If _termino Is Nothing Then
                                                .MediosPago.FirstOrDefault(Function(x) x.Identificador = rowMedioPago("OID_MEDIO_PAGO")).Terminos.Add(termino)
                                            Else

                                                Dim valor As New Clases.TerminoValorPosible
                                                valor.Identificador = If(rowMedioPago.Table.Columns.Contains("OID_VALOR"), Util.AtribuirValorObj(rowMedioPago("OID_VALOR"), GetType(String)), Nothing)
                                                valor.Codigo = If(rowMedioPago.Table.Columns.Contains("COD_VALOR"), Util.AtribuirValorObj(rowMedioPago("COD_VALOR"), GetType(String)), Nothing)
                                                valor.Descripcion = If(rowMedioPago.Table.Columns.Contains("DES_VALOR"), Util.AtribuirValorObj(rowMedioPago("DES_VALOR"), GetType(String)), Nothing)
                                                valor.EstaActivo = If(rowMedioPago.Table.Columns.Contains("VT_BOL_VIGENTE"), Util.AtribuirValorObj(rowMedioPago("VT_BOL_VIGENTE"), GetType(String)), Nothing)
                                                _termino.ValoresPosibles.Add(valor)

                                            End If

                                        End If
                                    End If
                                Next

                            End If

                        End If

                    End With

                    divisas.Add(divisa)
                Next
            End If

            Return divisas
        End Function

        Private Shared Function ObtenerDivisasEnLaBase_v2(parcodigosDivisas As List(Of String), identificadoresDivisas As List(Of String)) As DataTable
            Dim dt As DataTable
            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.ObtenerDivisas_v2
                Dim filtro As String = ""

                If parcodigosDivisas IsNot Nothing Then
                    Dim codigosDivisas As List(Of String) = parcodigosDivisas.Distinct().ToList
                    If codigosDivisas.Count = 1 Then
                        filtro &= " AND D.COD_ISO_DIVISA = []COD_ISO_DIVISA "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ISO_DIVISA", ProsegurDbType.Descricao_Curta, codigosDivisas(0)))
                    ElseIf codigosDivisas.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosDivisas, "COD_ISO_DIVISA", cmd, "AND", "D", , False)
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

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function

        Private Shared Function ObtenerDivisasEnLaBase_ConTransaccion(codigosDivisas As List(Of String), identificadoresDivisas As List(Of String), ByRef transaccion As DataBaseHelper.Transaccion) As DataTable

            Dim wrapper As New DataBaseHelper.SPWrapper(String.Empty, False, CommandType.Text)

            Dim query As String = My.Resources.ObtenerDivisas_v2
            Dim filtro As String = ""

            If codigosDivisas IsNot Nothing Then
                If codigosDivisas.Count = 1 Then
                    filtro &= " AND D.COD_ISO_DIVISA = []COD_ISO_DIVISA "
                    wrapper.AgregarParam("COD_ISO_DIVISA", ProsegurDbType.Descricao_Curta, codigosDivisas(0))
                ElseIf codigosDivisas.Count > 0 Then
                    filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosDivisas, "COD_ISO_DIVISA", wrapper, transaccion, "AND", "D", , False)
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

            wrapper.SP = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))

            Dim Ds As DataSet = DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

            Return IIf(Ds IsNot Nothing AndAlso Ds.Tables.Count > 0, Ds.Tables(0), New DataTable)

        End Function

        Public Shared Function ObtenerListaDivisas() As List(Of Clases.Abono.AbonoInformacion)
            Dim Divisas As List(Of Clases.Abono.AbonoInformacion) = Nothing

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtro As String = ""

                    filtro &= " AND D.BOL_VIGENTE = []BOL_VIGENTE "
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_VIGENTE", ProsegurDbType.Logico, True))

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerDivisas_v2, filtro))
                    command.CommandType = CommandType.Text

                    Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                        Divisas = New List(Of Clases.Abono.AbonoInformacion)

                        For Each rowCliente In dt.Rows

                            Divisas.Add(New Clases.Abono.AbonoInformacion With {.Identificador = Util.AtribuirValorObj(rowCliente("OID_DIVISA"), GetType(String)), _
                                        .Descripcion = Util.AtribuirValorObj(rowCliente("DES_DIVISA"), GetType(String)), _
                                        .Codigo = Util.AtribuirValorObj(rowCliente("COD_ISO_DIVISA"), GetType(String))
                                        })

                        Next

                    End If


                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return Divisas
        End Function





















        ''' <summary>
        ''' Obtene las divisas, pero solamente las informaciones necesarioas para los servicios de Integraciones
        ''' </summary>
        ''' <param name="valores">Valores que debe ser recuperados</param>
        ''' <returns>Una collecion de Clases.Divisa</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerDivisasParaIntegraciones(valores As ContractoServicio.Contractos.Integracion.Comon.Valores,
                                                      Optional codigoAjeno As String = "") As ObservableCollection(Of Clases.Divisa)

            ' Verifica se existe valores
            If valores Is Nothing Then
                Return Nothing
            End If

            ' Hace TODAS las busquedas necesarias
            Dim dtDivisas As DataTable = Nothing
            Dim dtDenominaciones As DataTable = Nothing
            Dim dtMedioPago As DataTable = Nothing
            Dim dtCodigosAjeno As DataTable = Nothing

            Dim TDivisas As New Task(Sub()
                                         dtDivisas = ObtenerIdentificadoresDivisas(valores, codigoAjeno)
                                     End Sub)
            TDivisas.Start()

            Dim TDenominaciones As New Task(Sub()
                                                dtDenominaciones = ObtenerIdentificadoresDenominaciones(valores, codigoAjeno)
                                            End Sub)
            TDenominaciones.Start()

            Dim TMedioPago As New Task(Sub()
                                           dtMedioPago = ObtenerIdentificadoresMedioPagos(valores, codigoAjeno)
                                       End Sub)
            TMedioPago.Start()

            Dim TCodigosAjeno As New Task(Sub()
                                              dtCodigosAjeno = ObtenerCodigosAjeno(codigoAjeno)
                                          End Sub)
            TCodigosAjeno.Start()

            ' Aguarda que as tasks terminem antes de continuar
            Task.WaitAll(New Task() {TDivisas, TDenominaciones, TMedioPago, TCodigosAjeno})

            Return cargarDivisas_v2(dtDivisas, dtDenominaciones, dtMedioPago, dtCodigosAjeno)

        End Function

        Public Shared Function ObtenerIdentificadoresDivisas(valores As ContractoServicio.Contractos.Integracion.Comon.Valores,
                                                             codigoAjeno As String) As DataTable

            If valores.divisas Is Nothing Then
                Return Nothing
            End If

            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.DivisasObtenerIdentificadoresDivisas
                Dim codigosDivisas As New List(Of String)
                cmd.CommandText = ""
                cmd.CommandType = CommandType.Text

                If valores.divisas IsNot Nothing Then
                    For Each _divisa In valores.divisas
                        If Not codigosDivisas.Contains(_divisa.codigoDivisa) Then
                            codigosDivisas.Add(_divisa.codigoDivisa)
                        End If
                    Next
                End If

                If valores.mediosDePago IsNot Nothing Then
                    For Each _divisa In valores.mediosDePago
                        If Not codigosDivisas.Contains(_divisa.codigoDivisa) Then
                            codigosDivisas.Add(_divisa.codigoDivisa)
                        End If
                    Next
                End If

                Dim filtro As String = ""
                Dim inner As String = ""

                If codigosDivisas IsNot Nothing Then

                    If Not String.IsNullOrEmpty(codigoAjeno) Then

                        inner = " INNER JOIN GEPR_TCODIGO_AJENO CA ON CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA' AND CA.OID_TABLA_GENESIS = D.OID_DIVISA "

                        filtro &= " AND CA.COD_IDENTIFICADOR = []COD_IDENTIFICADOR "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDENTIFICADOR", ProsegurDbType.Descricao_Curta, codigoAjeno))

                        If codigosDivisas.Count = 1 Then
                            filtro &= " AND CA.COD_AJENO = []COD_AJENO "
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_AJENO", ProsegurDbType.Descricao_Curta, codigosDivisas(0)))
                        ElseIf codigosDivisas.Count > 0 Then
                            filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosDivisas, "COD_AJENO", cmd, "AND", "CA", , False)
                        End If

                    Else
                        If codigosDivisas.Count = 1 Then
                            filtro &= " AND D.COD_ISO_DIVISA = []COD_ISO_DIVISA "
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ISO_DIVISA", ProsegurDbType.Descricao_Curta, codigosDivisas(0)))
                        ElseIf codigosDivisas.Count > 0 Then
                            filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosDivisas, "COD_ISO_DIVISA", cmd, "AND", "D", , False)
                        End If
                    End If

                End If

                cmd.CommandText &= Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, inner, filtro))
                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt

        End Function

        Public Shared Function ObtenerIdentificadoresDenominaciones(valores As ContractoServicio.Contractos.Integracion.Comon.Valores,
                                                                    codigoAjeno As String) As DataTable

            If valores.divisas Is Nothing OrElse valores.divisas.Count = 0 Then
                Return Nothing
            End If

            Dim dt As DataTable = Nothing
            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.DivisasObtenerIdentificadoresDenominaciones
                cmd.CommandText = ""
                cmd.CommandType = CommandType.Text

                For Each _divisas In valores.divisas

                    If _divisas.denominaciones IsNot Nothing AndAlso _divisas.denominaciones.Count > 0 Then
                        _divisas.denominaciones.RemoveAll(Function(x) x Is Nothing OrElse x.codigoDenominacion Is Nothing OrElse String.IsNullOrEmpty(x.codigoDenominacion))
                    End If

                    If _divisas.denominaciones IsNot Nothing AndAlso _divisas.denominaciones.Count > 0 AndAlso _divisas.denominaciones(0) IsNot Nothing Then

                        Dim codigosdenominaciones As List(Of String) = (From den In _divisas.denominaciones Select den.codigoDenominacion).ToList

                        Dim filtro As String = ""
                        Dim inner As String = ""

                        If codigosdenominaciones IsNot Nothing Then

                            If Not String.IsNullOrEmpty(codigoAjeno) Then

                                inner = " INNER JOIN GEPR_TCODIGO_AJENO CA ON CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TDENOMINACION' AND CA.OID_TABLA_GENESIS = DN.OID_DENOMINACION "
                                inner &= " INNER JOIN GEPR_TCODIGO_AJENO CA2 ON CA2.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA' AND CA2.OID_TABLA_GENESIS = DN.OID_DIVISA "

                                filtro &= " AND CA2.COD_IDENTIFICADOR = []COD_IDENTIFICADOR AND CA2.COD_AJENO = []COD_ISO_DIVISA_" & _divisas.codigoDivisa & " "
                                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDENTIFICADOR", ProsegurDbType.Descricao_Curta, codigoAjeno))
                                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ISO_DIVISA_" & _divisas.codigoDivisa, ProsegurDbType.Descricao_Curta, _divisas.codigoDivisa))

                                If codigosdenominaciones.Count = 1 Then

                                    filtro &= " AND CA.COD_IDENTIFICADOR = []COD_IDENTIFICADOR AND CA.COD_AJENO = []COD_AJENO "
                                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_AJENO", ProsegurDbType.Descricao_Curta, codigosdenominaciones(0)))

                                Else

                                    filtro &= " AND CA.COD_IDENTIFICADOR = []COD_IDENTIFICADOR AND CA.COD_AJENO in ("

                                    Dim i As Integer = 0
                                    For Each den In codigosdenominaciones

                                        If i > 0 Then
                                            filtro &= ","
                                        End If
                                        filtro &= "[]COD_AJENO" & _divisas.codigoDivisa & "_" & i.ToString
                                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_AJENO" & _divisas.codigoDivisa & "_" & i.ToString, ProsegurDbType.Descricao_Curta, den))
                                        i = i + 1

                                    Next

                                    filtro &= ") "

                                End If

                            Else

                                filtro &= " AND D.COD_ISO_DIVISA = []COD_ISO_DIVISA_" & _divisas.codigoDivisa & " "
                                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ISO_DIVISA_" & _divisas.codigoDivisa, ProsegurDbType.Descricao_Curta, _divisas.codigoDivisa))

                                If codigosdenominaciones.Count = 1 Then
                                    filtro &= " AND DN.COD_DENOMINACION = []COD_DENOMINACION_" & _divisas.codigoDivisa & " "
                                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DENOMINACION_" & _divisas.codigoDivisa, ProsegurDbType.Descricao_Curta, codigosdenominaciones(0)))
                                ElseIf codigosdenominaciones.Count > 0 Then

                                    filtro &= " AND DN.COD_DENOMINACION in ("

                                    Dim i As Integer = 0
                                    For Each den In codigosdenominaciones

                                        If i > 0 Then
                                            filtro &= ","
                                        End If
                                        filtro &= "[]COD_DENOMINACION_" & _divisas.codigoDivisa & "_" & i.ToString
                                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DENOMINACION_" & _divisas.codigoDivisa & "_" & i.ToString, ProsegurDbType.Descricao_Curta, den))
                                        i = i + 1

                                    Next

                                    filtro &= ") "

                                End If

                            End If

                        End If

                        If Not String.IsNullOrEmpty(cmd.CommandText) Then
                            cmd.CommandText &= vbNewLine & " UNION " & vbNewLine
                        End If

                        cmd.CommandText &= Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, inner, filtro))

                    End If

                Next

                If Not String.IsNullOrEmpty(cmd.CommandText) Then
                    dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
                End If

            End Using

            Return dt

        End Function

        Public Shared Function ObtenerIdentificadoresMedioPagos(valores As ContractoServicio.Contractos.Integracion.Comon.Valores,
                                                                codigoAjeno As String) As DataTable

            If valores.mediosDePago Is Nothing OrElse valores.mediosDePago.Count = 0 Then
                Return Nothing
            End If

            Dim dt As DataTable
            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.DivisasObtenerIdentificadoresMediosDePago
                cmd.CommandText = ""
                cmd.CommandType = CommandType.Text

                Dim codigosMedioDePago As List(Of String) = (From med In valores.mediosDePago Select med.codigoMedioDePago).ToList

                For Each _medioPago In valores.mediosDePago

                    Dim filtro As String = ""
                    Dim inner As String = ""

                    If Not String.IsNullOrEmpty(codigoAjeno) Then

                        inner = " INNER JOIN GEPR_TCODIGO_AJENO CA ON CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TMEDIO_PAGO' AND CA.OID_TABLA_GENESIS = MP.OID_MEDIO_PAGO "
                        inner &= " INNER JOIN GEPR_TCODIGO_AJENO CA2 ON CA2.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA' AND CA2.OID_TABLA_GENESIS = MP.OID_DIVISA "

                        filtro &= " AND CA.COD_IDENTIFICADOR = []COD_IDENTIFICADOR AND CA.COD_AJENO = []COD_AJENO "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_AJENO", ProsegurDbType.Descricao_Curta, _medioPago.codigoMedioDePago))

                        filtro &= " AND CA2.COD_IDENTIFICADOR = []COD_IDENTIFICADOR AND CA2.COD_AJENO = []COD_ISO_DIVISA_" & _medioPago.codigoDivisa & " "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDENTIFICADOR", ProsegurDbType.Descricao_Curta, codigoAjeno))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ISO_DIVISA_" & _medioPago.codigoDivisa, ProsegurDbType.Descricao_Curta, _medioPago.codigoDivisa))

                    Else

                        filtro &= " AND MP.COD_MEDIO_PAGO = []COD_MEDIO_PAGO_" & _medioPago.codigoMedioDePago & " "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_MEDIO_PAGO_" & _medioPago.codigoMedioDePago, ProsegurDbType.Descricao_Curta, _medioPago.codigoMedioDePago))

                    End If

                    If Not String.IsNullOrEmpty(cmd.CommandText) Then
                        cmd.CommandText &= vbNewLine & " UNION " & vbNewLine
                    End If

                    cmd.CommandText &= Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, inner, filtro))

                Next

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt

        End Function

        Public Shared Function ObtenerCodigosAjeno(codigoAjeno As String) As DataTable

            If String.IsNullOrEmpty(codigoAjeno) Then
                Return Nothing
            End If

            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DivisasObtenerCodigosAjenos)
                cmd.CommandType = CommandType.Text

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDENTIFICADOR", ProsegurDbType.Descricao_Curta, codigoAjeno))

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt

        End Function


        Public Shared Function cargarDivisas_v2(dtDivisas As DataTable, dtDenominaciones As DataTable, dtMedioPago As DataTable,
                                                             dtCodigosAjeno As DataTable) As ObservableCollection(Of Clases.Divisa)

            Dim divisas As New ObservableCollection(Of Clases.Divisa)

            If dtDivisas IsNot Nothing AndAlso dtDivisas.Rows.Count > 0 Then

                divisas = New ObservableCollection(Of Clases.Divisa)

                For Each rowDivisa In dtDivisas.Rows

                    Dim divisa As New Clases.Divisa

                    With divisa

                        .Identificador = Util.AtribuirValorObj(rowDivisa("OID_DIVISA"), GetType(String))
                        .CodigoISO = Util.AtribuirValorObj(rowDivisa("COD_ISO_DIVISA"), GetType(String))
                        .CodigosAjeno = CargarCodigoAjeno(dtCodigosAjeno, .Identificador, "GEPR_TDIVISA")

                        If dtDenominaciones IsNot Nothing AndAlso dtDenominaciones.Rows.Count > 0 Then

                            .Denominaciones = New ObservableCollection(Of Clases.Denominacion)

                            For Each rowDenominacion In From DN In dtDenominaciones.Rows Where DN("OID_DIVISA") = rowDivisa("OID_DIVISA")

                                Dim denominacion As New Clases.Denominacion

                                With denominacion

                                    .Identificador = Util.AtribuirValorObj(rowDenominacion("OID_DENOMINACION"), GetType(String))
                                    .Codigo = Util.AtribuirValorObj(rowDenominacion("COD_DENOMINACION"), GetType(String))
                                    .EsBillete = Util.AtribuirValorObj(rowDenominacion("BOL_BILLETE"), GetType(Boolean))
                                    .CodigosAjeno = CargarCodigoAjeno(dtCodigosAjeno, .Identificador, "GEPR_TDENOMINACION")

                                End With

                                .Denominaciones.Add(denominacion)
                            Next

                        End If

                        If dtMedioPago IsNot Nothing AndAlso dtMedioPago.Rows.Count > 0 Then

                            .MediosPago = New ObservableCollection(Of Clases.MedioPago)

                            For Each rowMedioPago In From MP In dtMedioPago.Rows Where MP("COD_ISO_DIVISA") = rowDivisa("COD_ISO_DIVISA")

                                Dim medioPago As New Clases.MedioPago

                                With medioPago

                                    .Identificador = Util.AtribuirValorObj(rowMedioPago("OID_MEDIO_PAGO"), GetType(String))
                                    .Codigo = Util.AtribuirValorObj(rowMedioPago("COD_MEDIO_PAGO"), GetType(String))
                                    .Tipo = RecuperarEnum(Of Enumeradores.TipoMedioPago)(Util.AtribuirValorObj(rowMedioPago("COD_TIPO_MEDIO_PAGO"), GetType(String)))
                                    .CodigosAjeno = CargarCodigoAjeno(dtCodigosAjeno, .Identificador, "GEPR_TMEDIO_PAGO")

                                End With

                                .MediosPago.Add(medioPago)

                            Next

                        End If

                    End With
                    divisas.Add(divisa)

                Next

            End If


            Return divisas

        End Function

        Private Shared Function CargarCodigoAjeno(dtCodigosAjeno As DataTable,
                                                  identificador As String,
                                                  tabla As String) As ObservableCollection(Of Clases.CodigoAjeno)

            Dim codigos As ObservableCollection(Of Clases.CodigoAjeno) = Nothing

            If dtCodigosAjeno IsNot Nothing AndAlso dtCodigosAjeno.Rows.Count > 0 Then

                Dim valores = dtCodigosAjeno.Select(" COD_TIPO_TABLA_GENESIS = '" & tabla & "' AND OID_TABLA_GENESIS = '" & identificador & "'")

                If valores IsNot Nothing AndAlso valores.Count > 0 Then

                    codigos = New ObservableCollection(Of Clases.CodigoAjeno)

                    For Each valor In valores

                        codigos.Add(New Clases.CodigoAjeno With {.IdentificadorTablaGenesis = Util.AtribuirValorObj(valor("OID_TABLA_GENESIS"), GetType(String)), _
                                                    .CodigoTipoTablaGenesis = Util.AtribuirValorObj(valor("COD_TIPO_TABLA_GENESIS"), GetType(String)), _
                                                    .CodigoIdentificador = Util.AtribuirValorObj(valor("COD_IDENTIFICADOR"), GetType(String)), _
                                                    .Codigo = Util.AtribuirValorObj(valor("COD_AJENO"), GetType(String)), _
                                                    .Descripcion = Util.AtribuirValorObj(valor("DES_AJENO"), GetType(String)), _
                                                    .EsDefecto = Util.AtribuirValorObj(valor("BOL_DEFECTO"), GetType(Boolean)), _
                                                    .EsActivo = Util.AtribuirValorObj(valor("BOL_ACTIVO"), GetType(Boolean))
                                                   })
                    Next

                End If

            End If

            Return codigos

        End Function



#Region "[CONSULTAS]"

        ''' <summary>
        ''' Obtener los valores del documento
        ''' </summary>
        ''' <param name="identificadorDocumento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerValoresPorDocumento(identificadorDocumento As String) As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DivisasRecuperarValoresPorDocumento)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                Return dt

            End Using

            Return Nothing
        End Function

        ''' <summary>
        ''' Obtener los valores del Elemento
        ''' </summary>
        ''' <param name="identificadorElemento"></param>
        ''' <param name="TipoElemento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerValoresPorElemento(identificadorElemento As String, TipoElemento As Enumeradores.TipoElemento) As DataTable

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Select Case TipoElemento

                Case Enumeradores.TipoElemento.Remesa

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DivisaRecuperarValorConIdRemesa)

                Case Enumeradores.TipoElemento.Bulto

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DivisaRecuperarValorConIdBulto)

                Case Enumeradores.TipoElemento.Parcial

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DivisaRecuperarValorConIdParcial)

            End Select

            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ELEMENTO", ProsegurDbType.Identificador_Alfanumerico, identificadorElemento))
            'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, IdentificadorDocumento))

            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

        End Function

#End Region





#Region "Consultar"

        Public Shared Function ObtenerIdentificadorDivisa(CodigoIso As String, Conexao As String) As String
            Dim comando As IDbCommand = AcessoDados.CriarComando(Conexao)

            comando.CommandType = CommandType.Text
            comando.CommandText = My.Resources.Salidas_Divisa_RecuperarIdentificadorDivisa
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Conexao, "COD_ISO_DIVISA", ProsegurDbType.Identificador_Alfanumerico, CodigoIso))

            Dim dt As DataTable = DbHelper.AcessoDados.ExecutarDataTable(Conexao, comando)
            If dt.Rows.Count > 0 Then
                Return Util.AtribuirValorObj(dt.Rows(0)("OID_DIVISA"), GetType(String))
            End If
            Return String.Empty
        End Function


        ''' <summary>
        ''' RecuperarValorEfectivoPorDenominacion
        ''' </summary>
        ''' <param name="IdElemento"></param>
        ''' <param name="TipoElemento"></param>
        ''' <param name="IdDenominacion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarValorEfectivoPorDenominacion(IdElemento As String,
                                                                      TipoElemento As Enumeradores.TipoElemento,
                                                                      IdDenominacion As String) As ObservableCollection(Of Clases.ValorDenominacion)

            Dim objValoresDenominacion As New ObservableCollection(Of Clases.ValorDenominacion)

            'Recupera os Valores Contados
            'Dim valoresDenominacion = Efectivo.RecuperarValoresPorDenominacion(IdentificadorDocumento, IdElemento, IdDenominacion, TipoElemento)
            Dim valoresDenominacion = Efectivo.RecuperarValoresPorDenominacion(IdElemento, IdDenominacion, TipoElemento)
            If valoresDenominacion IsNot Nothing Then
                objValoresDenominacion = valoresDenominacion
            End If

            'Recupera os Valores Diferenças
            'valoresDenominacion = DiferenciaEfectivo.RecuperarValoresPorDenominacion(IdentificadorDocumento, IdElemento, IdDenominacion, TipoElemento)
            valoresDenominacion = DiferenciaEfectivo.RecuperarValoresPorDenominacion(IdElemento, IdDenominacion, TipoElemento)
            If valoresDenominacion IsNot Nothing Then
                objValoresDenominacion.AddRange(valoresDenominacion)
            End If

            'Recupera os Valores Declarado
            'valoresDenominacion = DeclaradoEfectivo.RecuperarValoresPorDenominacion(IdentificadorDocumento, IdElemento, IdDenominacion, TipoElemento)
            valoresDenominacion = DeclaradoEfectivo.RecuperarValoresPorDenominacion(IdElemento, IdDenominacion, TipoElemento)
            If valoresDenominacion IsNot Nothing Then
                objValoresDenominacion.AddRange(valoresDenominacion)
            End If

            Return objValoresDenominacion
        End Function

        ''' <summary>
        ''' RecuperarValorEfectivoPorDenominacion
        ''' </summary>
        ''' <param name="IdElemento"></param>
        ''' <param name="TipoElemento"></param>
        ''' <param name="IdMedioPago"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarValorMedioPago(IdElemento As String,
                                                        TipoElemento As Enumeradores.TipoElemento, _
                                                        IdMedioPago As String, _
                                                        TerminosMedioPago As ObservableCollection(Of Clases.Termino)) As ObservableCollection(Of Clases.ValorMedioPago)

            Dim objValoresMedioPago As ObservableCollection(Of Clases.ValorMedioPago) = Nothing

            Dim objValorMedioPago As Clases.ValorMedioPago = Nothing

            'Recupera os Valores Contados
            objValorMedioPago = MedioPago.RecuperarValorContadoMedioPago(IdElemento, IdMedioPago, TipoElemento, TerminosMedioPago)

            If objValorMedioPago IsNot Nothing Then

                If objValoresMedioPago Is Nothing Then objValoresMedioPago = New ObservableCollection(Of Clases.ValorMedioPago)

                'Recupera os valores contados dos efectivos
                objValoresMedioPago.Add(objValorMedioPago)

            End If

            'Recupera os Valores Diferenças
            objValorMedioPago = DiferenciaMedioPago.RecuperarValorMedioPago(IdElemento, IdMedioPago, TipoElemento)

            If objValorMedioPago IsNot Nothing Then

                If objValoresMedioPago Is Nothing Then objValoresMedioPago = New ObservableCollection(Of Clases.ValorMedioPago)

                'Recupera os valores diferente dos efectivos
                objValoresMedioPago.Add(objValorMedioPago)

            End If

            'Recupera os Valores Declarado
            objValorMedioPago = DeclaradoMedioPago.RecuperarValorMedioPago(IdElemento, IdMedioPago, TipoElemento, TerminosMedioPago)

            If objValorMedioPago IsNot Nothing Then

                If objValoresMedioPago Is Nothing Then objValoresMedioPago = New ObservableCollection(Of Clases.ValorMedioPago)

                'Recupera os valores declarados dos efectivos
                objValoresMedioPago.Add(objValorMedioPago)

            End If

            Return objValoresMedioPago
        End Function

        ''' <summary>
        ''' Popula os valores do bulto
        ''' </summary>
        ''' <param name="dt"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function PopularValores(dt As DataTable, IdElemento As String, TipoElemento As Enumeradores.TipoElemento) As ObservableCollection(Of Clases.Divisa)

            Dim objValores As ObservableCollection(Of Clases.Divisa) = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Dim objDivisa As Clases.Divisa = Nothing
                objValores = New ObservableCollection(Of Clases.Divisa)
                Dim IdDivisa As String = String.Empty
                Dim IdDenominacion As String = String.Empty
                Dim objDenominacion As Clases.Denominacion = Nothing
                Dim IdMedioPago As String = String.Empty
                Dim objMedioPago As Clases.MedioPago = Nothing
                Dim objValoresDivisa As List(Of Clases.ValorDivisa) = Nothing

                For Each dr In dt.Rows

                    IdDivisa = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))

                    objDivisa = (From valor In objValores Where valor.Identificador = IdDivisa).FirstOrDefault

                    If objDivisa Is Nothing Then

                        objValores.Add(New Clases.Divisa With { _
                                       .CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String)), _
                                       .CodigoSimbolo = Util.AtribuirValorObj(dr("COD_SIMBOLO"), GetType(String)), _
                                       .CodigoUsuario = Util.AtribuirValorObj(dr("COD_USUARIO"), GetType(String)), _
                                       .Denominaciones = Nothing, _
                                       .Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String)), _
                                       .EstaActivo = Util.AtribuirValorObj(dr("DIVISA_ACTIVA"), GetType(String)), _
                                       .FechaHoraTransporte = Util.AtribuirValorObj(dr("FYH_ACTUALIZACION"), GetType(String)), _
                                       .Icono = Nothing, _
                                       .Identificador = IdDivisa, _
                                       .MediosPago = Nothing, _
                                       .ValoresTotalesEfectivo = RecuperarValorDivisa(Of Clases.ValorEfectivo)(IdElemento, IdDivisa, TipoElemento), _
                                    .ValoresTotalesDivisa = RecuperarValorDivisa(Of Clases.ValorDivisa)(IdElemento, IdDivisa, TipoElemento), _
                                    .ValoresTotalesTipoMedioPago = RecuperarValorMedioPagoDivisa(IdElemento, IdDivisa, TipoElemento), _
                                    .Color = Util.AtribuirValorObj(dr("COD_COLOR"), GetType(Drawing.Color))})
                        '.ValoresTotalesEfectivo = RecuperarValorDivisa(Of Clases.ValorEfectivo)(Util.AtribuirValorObj(dr("OID_DOCUMENTO"), GetType(String)), IdElemento, IdDivisa, TipoElemento), _
                        '.ValoresTotalesDivisa = RecuperarValorDivisa(Of Clases.ValorDivisa)(Util.AtribuirValorObj(dr("OID_DOCUMENTO"), GetType(String)), IdElemento, IdDivisa, TipoElemento), _
                        '.ValoresTotalesTipoMedioPago = RecuperarValorMedioPagoDivisa(Util.AtribuirValorObj(dr("OID_DOCUMENTO"), GetType(String)), IdElemento, IdDivisa, TipoElemento), _

                        objDivisa = (From valor In objValores Where valor.Identificador = IdDivisa).FirstOrDefault

                    End If

                    IdDenominacion = Util.AtribuirValorObj(dr("OID_DENOMINACION"), GetType(String))

                    'Se exister denominação, recupera os dados da denominação
                    If Not String.IsNullOrEmpty(IdDenominacion) Then

                        If objDivisa.Denominaciones Is Nothing Then objDivisa.Denominaciones = New ObservableCollection(Of Clases.Denominacion)

                        objDenominacion = (From den In objDivisa.Denominaciones Where den.Identificador = IdDenominacion).FirstOrDefault

                        If objDenominacion Is Nothing Then

                            'Recupera as informações da denominação
                            objDivisa.Denominaciones.Add(Denominacion.RecuperarDenominacion(IdDenominacion))

                            objDenominacion = (From den In objDivisa.Denominaciones Where den.Identificador = IdDenominacion).FirstOrDefault

                        End If

                        'Recupera os valores da denominação
                        objDenominacion.ValorDenominacion = RecuperarValorEfectivoPorDenominacion(IdElemento, TipoElemento, IdDenominacion)

                    End If

                    IdMedioPago = Util.AtribuirValorObj(dr("OID_MEDIO_PAGO"), GetType(String))

                    'Se existir valores para medio pago recupera o valor do medio pago
                    If Not String.IsNullOrEmpty(IdMedioPago) Then

                        If objDivisa.MediosPago Is Nothing Then objDivisa.MediosPago = New ObservableCollection(Of Clases.MedioPago)

                        objMedioPago = (From mp In objDivisa.MediosPago Where mp.Identificador = IdMedioPago).FirstOrDefault

                        If objMedioPago Is Nothing Then

                            'Recupera o meio de pamgamento
                            objDivisa.MediosPago.Add(MedioPago.RecuperarMedioPago(IdMedioPago))

                            objMedioPago = (From mp In objDivisa.MediosPago Where mp.Identificador = IdMedioPago).FirstOrDefault

                        End If

                        'Recupera os valores do meio de pagamento
                        objMedioPago.Valores = RecuperarValorMedioPago(IdElemento, TipoElemento, IdMedioPago, objMedioPago.Terminos)

                    End If

                Next

                'TODO: Falta campo icono na base.
            End If

            Return objValores
        End Function

        ''' <summary>
        ''' Recupera os valores de total dos efectivos.
        ''' </summary>
        ''' <param name="IdentificadorElemento"></param>
        ''' <param name="IdentificadorDivisa"></param>
        ''' <param name="TipoElemento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarValorDivisa(Of T As Clases.Valor)(IdentificadorElemento As String, IdentificadorDivisa As String,
                                                             TipoElemento As Enumeradores.TipoElemento) As ObservableCollection(Of T)

            Dim objValor As T = Nothing
            Dim objValores As ObservableCollection(Of T) = Nothing
            Dim nivelDetalle As Enumeradores.TipoNivelDetalhe

            Select Case True
                Case GetType(T) Is GetType(Clases.ValorDivisa)
                    nivelDetalle = Enumeradores.TipoNivelDetalhe.TotalGeral
                Case GetType(T) Is GetType(Clases.ValorEfectivo)
                    nivelDetalle = Enumeradores.TipoNivelDetalhe.Total
                Case Else
                    Throw New Exception("Este método recupera somente os valores totais dos efetivos.")
            End Select

            'Recupera os valores de diferenças da divisa.
            objValor = DiferenciaEfectivo.RecuperarValorTotalDivisa(IdentificadorElemento, IdentificadorDivisa, TipoElemento, nivelDetalle)

            If objValor IsNot Nothing Then

                If objValores Is Nothing Then objValores = New ObservableCollection(Of T)

                objValores.Add(objValor)

            End If

            'Recupera os valores declarados da divisa.
            objValor = DeclaradoEfectivo.RecuperarValorTotalDivisa(IdentificadorElemento, IdentificadorDivisa, TipoElemento, nivelDetalle)

            If objValor IsNot Nothing Then

                If objValores Is Nothing Then objValores = New ObservableCollection(Of T)

                objValores.Add(objValor)

            End If

            Return objValores
        End Function

        ''' <summary>
        ''' Recupera os valores dos efectivos.
        ''' </summary>
        ''' <param name="IdentificadorElemento"></param>
        ''' <param name="IdentificadorDivisa"></param>
        ''' <param name="TipoElemento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarValorMedioPagoDivisa(IdentificadorElemento As String, IdentificadorDivisa As String,
                                                              TipoElemento As Enumeradores.TipoElemento) As ObservableCollection(Of Clases.ValorTipoMedioPago)

            Dim objValoresMedioPago As ObservableCollection(Of Clases.ValorTipoMedioPago) = Nothing
            Dim objValoresMedioPagoRetorno As ObservableCollection(Of Clases.ValorTipoMedioPago) = Nothing

            'Recupera os valores de diferenças da divisa.
            objValoresMedioPago = DiferenciaMedioPago.RecuperarValorTipoMedioPago(IdentificadorElemento, IdentificadorDivisa, TipoElemento)

            If objValoresMedioPago IsNot Nothing AndAlso objValoresMedioPago.Count > 0 Then

                If objValoresMedioPagoRetorno Is Nothing Then objValoresMedioPagoRetorno = New ObservableCollection(Of Clases.ValorTipoMedioPago)

                objValoresMedioPagoRetorno.AddRange(objValoresMedioPago)

            End If

            'Recupera os valores declarados da divisa.
            objValoresMedioPago = DeclaradoMedioPago.RecuperarValorTipoMedioPago(IdentificadorElemento, IdentificadorDivisa, TipoElemento)

            If objValoresMedioPago IsNot Nothing AndAlso objValoresMedioPago.Count > 0 Then

                If objValoresMedioPagoRetorno Is Nothing Then objValoresMedioPagoRetorno = New ObservableCollection(Of Clases.ValorTipoMedioPago)

                objValoresMedioPagoRetorno.AddRange(objValoresMedioPago)

            End If

            Return objValoresMedioPagoRetorno
        End Function

        ''' <summary>
        ''' Preenche os Valores
        ''' </summary>
        ''' <param name="dt">Tabla de datos</param>
        ''' <param name="rellenarTipoValorNoDefinido">Define si es para cambiar los tipo de valores para noDefinido</param>
        ''' <param name="esDisponibleNoDefinido">Define si es para cambiar solamente los valores disponibles para NoDefinido</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function PreencherValores(dt As DataTable, _
                                                 Optional rellenarTipoValorNoDefinido As Boolean = False, _
                                                 Optional esDisponibleNoDefinido As Boolean = False, _
                                                 Optional identificadorDocumento As String = "") As ObservableCollection(Of Clases.Divisa)

            Dim objDivisasRetorno As ObservableCollection(Of Clases.Divisa) = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objDivisasRetorno = New ObservableCollection(Of Clases.Divisa)
                Dim IdentificadorDivisa As String = String.Empty
                Dim IdentificadorDenominacion As String = String.Empty
                Dim IdentificadorMedioPago As String = String.Empty
                Dim objDivisa As Clases.Divisa = Nothing
                Dim objDenominacion As Clases.Denominacion = Nothing
                Dim objMedioPago As Clases.MedioPago = Nothing
                Dim CodTipoMedioPago As String = String.Empty
                Dim CodNivelDetalle As String = String.Empty
                Dim Disponible As Boolean

                For Each dr As DataRow In dt.Rows

                    IdentificadorDivisa = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))
                    IdentificadorDenominacion = Util.AtribuirValorObj(dr("OID_DENOMINACION"), GetType(String))
                    IdentificadorMedioPago = Util.AtribuirValorObj(dr("OID_MEDIO_PAGO"), GetType(String))
                    CodTipoMedioPago = Util.AtribuirValorObj(dr("COD_TIPO_MEDIO_PAGO"), GetType(String))
                    CodNivelDetalle = Util.AtribuirValorObj(dr("COD_NIVEL_DETALLE"), GetType(String))
                    Disponible = Util.AtribuirValorObj(dr("BOL_DISPONIBLE"), GetType(Boolean))

                    'Verifica se a divisa ja foi adicionada na lista de divisas
                    objDivisa = (From div In objDivisasRetorno Where div.Identificador = IdentificadorDivisa).FirstOrDefault

                    If objDivisa Is Nothing Then

                        'Recupera as informações da divisa.
                        objDivisasRetorno.AddRange(ObtenerDivisas(New Clases.Divisa With {.Identificador = IdentificadorDivisa}))

                        objDivisa = (From div In objDivisasRetorno Where div.Identificador = IdentificadorDivisa).FirstOrDefault

                    End If

                    'Verifica se os valores não estão detalhados.
                    If String.IsNullOrEmpty(IdentificadorDenominacion) AndAlso String.IsNullOrEmpty(IdentificadorMedioPago) Then

                        ' Verifica se existe tipo de meio de pagamento
                        If String.IsNullOrEmpty(CodTipoMedioPago) Then

                            ' Verifica se é total de efetivo
                            If CodNivelDetalle = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor Then

                                If objDivisa.ValoresTotalesEfectivo Is Nothing Then objDivisa.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)
                                objDivisa.ValoresTotalesEfectivo.Add(New Clases.ValorEfectivo With { _
                                                                     .TipoDetalleEfectivo = Enumeradores.TipoDetalleEfectivo.Mezcla, _
                                                                     .TipoValor = If(rellenarTipoValorNoDefinido, Enumeradores.TipoValor.NoDefinido, If(esDisponibleNoDefinido, If(Disponible, Enumeradores.TipoValor.NoDefinido, Enumeradores.TipoValor.NoDisponible), If(Disponible, Enumeradores.TipoValor.Disponible, Enumeradores.TipoValor.NoDisponible))), _
                                                                     .Importe = Util.AtribuirValorObj(dr("IMPORTE"), GetType(Decimal))})

                                ' Verifica se é total geral
                            ElseIf CodNivelDetalle = Enumeradores.TipoNivelDetalhe.TotalGeral.RecuperarValor Then

                                If objDivisa.ValoresTotalesDivisa Is Nothing Then objDivisa.ValoresTotalesDivisa = New ObservableCollection(Of Clases.ValorDivisa)
                                objDivisa.ValoresTotalesDivisa.Add(New Clases.ValorDivisa With { _
                                                                     .TipoValor = If(rellenarTipoValorNoDefinido, Enumeradores.TipoValor.NoDefinido, If(esDisponibleNoDefinido, If(Disponible, Enumeradores.TipoValor.NoDefinido, Enumeradores.TipoValor.NoDisponible), If(Disponible, Enumeradores.TipoValor.Disponible, Enumeradores.TipoValor.NoDisponible))), _
                                                                     .Importe = Util.AtribuirValorObj(dr("IMPORTE"), GetType(Decimal))})
                            End If

                        Else

                            If objDivisa.ValoresTotalesTipoMedioPago Is Nothing Then objDivisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)
                            objDivisa.ValoresTotalesTipoMedioPago.Add(New Clases.ValorTipoMedioPago With { _
                                                                      .TipoValor = If(rellenarTipoValorNoDefinido, Enumeradores.TipoValor.NoDefinido, If(esDisponibleNoDefinido, If(Disponible, Enumeradores.TipoValor.NoDefinido, Enumeradores.TipoValor.NoDisponible), If(Disponible, Enumeradores.TipoValor.Disponible, Enumeradores.TipoValor.NoDisponible))), _
                                                                      .Importe = Util.AtribuirValorObj(dr("IMPORTE"), GetType(Decimal)), _
                                                                      .Cantidad = Util.AtribuirValorObj(dr("CANTIDAD"), GetType(Int64)), _
                                                                      .TipoMedioPago = Extenciones.RecuperarEnum(Of Enumeradores.TipoMedioPago)(Util.AtribuirValorObj(dr("COD_TIPO_MEDIO_PAGO"), GetType(String)))})

                        End If

                    End If

                    If Not String.IsNullOrEmpty(IdentificadorDenominacion) AndAlso objDivisa.Denominaciones IsNot Nothing AndAlso objDivisa.Denominaciones.Count > 0 Then

                        objDenominacion = (From den In objDivisa.Denominaciones Where den.Identificador = IdentificadorDenominacion).FirstOrDefault

                        If objDenominacion IsNot Nothing Then

                            If objDenominacion.ValorDenominacion Is Nothing Then objDenominacion.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)


                            objDenominacion.ValorDenominacion.Add(New Clases.ValorDenominacion With {.Cantidad = Util.AtribuirValorObj(dr("CANTIDAD"), GetType(Int64)), _
                                                                                                     .Importe = Util.AtribuirValorObj(dr("IMPORTE"), GetType(Decimal)), _
                                                                                                     .TipoValor = If(rellenarTipoValorNoDefinido, Enumeradores.TipoValor.NoDefinido, If(esDisponibleNoDefinido, If(Disponible, Enumeradores.TipoValor.NoDefinido, Enumeradores.TipoValor.NoDisponible), If(Disponible, Enumeradores.TipoValor.Disponible, Enumeradores.TipoValor.NoDisponible))),
                                                                                                     .UnidadMedida = If(IsDBNull(dr("OID_UNIDAD_MEDIDA")), Nothing, Genesis.UnidadMedida.RecuperarUnidadMedida(, Util.AtribuirValorObj(Of String)(dr("OID_UNIDAD_MEDIDA")))),
                                                                                                     .Calidad = If(IsDBNull(dr("OID_CALIDAD")), Nothing, Genesis.Calidad.ObtenerCalidadePorIdentificador(Util.AtribuirValorObj(Of String)(dr("OID_CALIDAD"))))})


                        End If

                    End If

                    If Not String.IsNullOrEmpty(IdentificadorMedioPago) AndAlso objDivisa.MediosPago IsNot Nothing AndAlso objDivisa.MediosPago.Count > 0 Then

                        objMedioPago = (From MP In objDivisa.MediosPago Where MP.Identificador = IdentificadorMedioPago).FirstOrDefault

                        If objMedioPago IsNot Nothing Then


                            Dim objValoresTerminos As New ObservableCollection(Of Clases.Termino)
                            If (Not String.IsNullOrEmpty(identificadorDocumento)) AndAlso objMedioPago.Terminos IsNot Nothing Then
                                For Each objTermino In objMedioPago.Terminos.Clonar()
                                    Dim objValoresTerminosMedioPago As ObservableCollection(Of Clases.Termino) = ValorTerminoMedioPago.RecuperarListaValorTerminoPorDocumento(objTermino.Identificador, identificadorDocumento)

                                    If objValoresTerminosMedioPago IsNot Nothing Then

                                        For Each v In objValoresTerminosMedioPago
                                            objTermino.Valor = v.Valor
                                            objTermino.NecIndiceGrupo = v.NecIndiceGrupo
                                            objValoresTerminos.Add(objTermino.Clonar())
                                        Next

                                    End If

                                Next

                            End If


                            If objMedioPago.Valores Is Nothing Then objMedioPago.Valores = New ObservableCollection(Of Clases.ValorMedioPago)
                            '.TipoValor = If(rellenarTipoValorNoDefinido, Enumeradores.TipoValor.NoDefinido, If(Disponible, Enumeradores.TipoValor.Disponible, Enumeradores.TipoValor.NoDisponible))
                            objMedioPago.Valores.Add(New Clases.ValorMedioPago With {.Cantidad = Util.AtribuirValorObj(dr("CANTIDAD"), GetType(Int64)), _
                                                                                     .Importe = Util.AtribuirValorObj(dr("IMPORTE"), GetType(Decimal)), _
                                                                                     .Terminos = objValoresTerminos, _
                                                                                     .TipoValor = If(rellenarTipoValorNoDefinido, Enumeradores.TipoValor.NoDefinido, If(esDisponibleNoDefinido, If(Disponible, Enumeradores.TipoValor.NoDefinido, Enumeradores.TipoValor.NoDisponible), If(Disponible, Enumeradores.TipoValor.Disponible, Enumeradores.TipoValor.NoDisponible)))})


                        End If

                    End If

                Next

            End If

            Return objDivisasRetorno
        End Function

        ''' <summary>
        ''' Retorna os valores pelo oid documento.
        ''' </summary>
        ''' <param name="oidDocumento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarValoresPorDocumento(oidDocumento As String) As ObservableCollection(Of Clases.Divisa)
            Dim divisasRetorno As New ObservableCollection(Of Clases.Divisa)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DivisasRecuperarValoresPorDocumento)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, oidDocumento))

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                divisasRetorno = PreencherValores(dt, True, , oidDocumento)

            End Using

            Return divisasRetorno
        End Function



#End Region

        ''' <summary>
        ''' Obtém as divisas através dos filtros, caso não sejam informados, todos os registros serão retornados
        ''' </summary>
        ''' <param name="Divisa">Los filtros usados en la consulta son: 
        '''                        Identificador, EsActivo caso hay valores.
        '''                        Si no hay valor en EsActivo la busqueda assume el valor 'True'.</param> 
        ''' <param name="ListaCodigoIso">Es usado caso no hay un objDivisa, entonces el filtro será por una lista de CodigosIso.</param>
        ''' <param name="EsNotInCodigoIso">Define si la clausula IN del ListaCodigoIso arriba, se quedará por NotIn o In. Case True "NotIn", case False "In"</param> 
        ''' <param name="EsActivo">Define el estado de las divisas a seren buscadas</param>
        ''' <param name="BuscarDenominaciones">Define se serán buscadas las denominaciones de la divisa</param>
        ''' <param name="BuscarMediosPago">Define se serán buscados los medios pagos de la divisa</param>
        ''' <param name="EsActivoDenominacion">Define se las denominaciones son activos</param>
        ''' <param name="EsActivoMedioPago">Define se los medios pago son activos</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [marcel.espiritosanto] 23/08/2013 Criado
        ''' </history>
        Public Shared Function ObtenerDivisas(Optional Divisa As Clases.Divisa = Nothing, _
                                              Optional ListaCodigoIso As ObservableCollection(Of String) = Nothing, _
                                              Optional EsNotInCodigoIso As Boolean = False, _
                                              Optional EsActivo As Nullable(Of Boolean) = Nothing, _
                                              Optional BuscarDenominaciones As Boolean = True, _
                                              Optional BuscarMediosPago As Boolean = True, _
                                              Optional EsActivoDenominacion As Boolean = False, _
                                              Optional EsActivoMedioPago As Boolean = True, _
                                              Optional EliminarDenominacionZero As Boolean = False) As ObservableCollection(Of Clases.Divisa)

            ' criar comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            ' cheques si hay filtros para el objDivisa, poes si no hay el proceso irá validar el filtro 'ListaCodigoIso'
            ' El proceso no ejecuta los dos filtros simultaneos. O es un o otro.
            Dim bolFiltros As Boolean = False

            Dim Consulta As New StringBuilder
            Consulta.Append(My.Resources.ObtenerDivisas.ToString)

            If Divisa IsNot Nothing AndAlso Not String.IsNullOrEmpty(Divisa.Identificador) Then

                Consulta.Append(" AND d.OID_DIVISA = []IDENTIFICADOR")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "IDENTIFICADOR", ProsegurDbType.Descricao_Curta, Divisa.Identificador))
                bolFiltros = True

            End If

            If Divisa IsNot Nothing AndAlso Not String.IsNullOrEmpty(Divisa.CodigoISO) Then

                Consulta.Append(" AND d.COD_ISO_DIVISA = []CODIGO")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "CODIGO", ProsegurDbType.Descricao_Curta, Divisa.CodigoISO))
                bolFiltros = True

            End If

            If Not bolFiltros AndAlso ListaCodigoIso IsNot Nothing AndAlso ListaCodigoIso.Count > 0 Then
                Consulta.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, ListaCodigoIso, "COD_ISO_DIVISA", comando, "AND", "d", , EsNotInCodigoIso))

            End If

            If EsActivo IsNot Nothing Then
                Consulta.Append(" AND d.BOL_VIGENTE = []BOL_VIGENTE")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_VIGENTE", ProsegurDbType.Logico, EsActivo))

            End If

            Consulta.Append(" ORDER BY d.COD_ISO_DIVISA, d.DES_DIVISA")

            ' preparar query
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, Consulta.ToString)
            comando.CommandType = CommandType.Text

            Dim dtDivisa As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

            ' criar objeto divisa coleccion
            Dim objDivisas As New ObservableCollection(Of Clases.Divisa)

            ' caso encontre algum registro
            If dtDivisa IsNot Nothing AndAlso dtDivisa.Rows.Count > 0 Then

                ' percorrer registros encontrados
                For Each row As DataRow In dtDivisa.Rows

                    Dim objDivisa As New Clases.Divisa

                    ' preencher a coleção com objetos divisa
                    objDivisas.Add(PopularDivisa(row, BuscarDenominaciones, BuscarMediosPago, EsActivoDenominacion, EsActivoMedioPago, EliminarDenominacionZero))

                Next row

            End If

            ' retornar coleção de divisas
            Return objDivisas

        End Function

        Public Shared Function ObtenerDivisas(identificadoresDivisas As List(Of String)) As ObservableCollection(Of Clases.Divisa)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            comando.CommandText = My.Resources.ObtenerDivisas.ToString
            comando.CommandType = CommandType.Text

            If identificadoresDivisas IsNot Nothing AndAlso identificadoresDivisas.Count > 0 Then
                comando.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresDivisas, "OID_DIVISA", comando, "AND")
            End If

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)
            Dim dtDivisa As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

            ' criar objeto divisa coleccion
            Dim objDivisas As New ObservableCollection(Of Clases.Divisa)

            ' caso encontre algum registro
            If dtDivisa IsNot Nothing AndAlso dtDivisa.Rows.Count > 0 Then

                ' percorrer registros encontrados
                For Each row As DataRow In dtDivisa.Rows

                    Dim objDivisa As New Clases.Divisa

                    ' preencher a coleção com objetos divisa
                    objDivisas.Add(PopularDivisa(row, False, False, False, , False))

                Next row

            End If

            ' retornar coleção de divisas
            Return objDivisas

        End Function

        ''' <summary>
        ''' Metodo ObtenerDivisaPorCodigoDenominacion
        ''' </summary>
        ''' <param name="codigoDenominacion"></param>
        ''' <returns>Clases.Divisa</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerDivisaPorCodigoDenominacion(codigoDenominacion As String, Optional buscarDenominacion As Boolean = False, Optional buscarMediosPago As Boolean = False) As Clases.Divisa

            ' criar comando
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            ' obter comando sql
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DivisaRecuperarPorCodigoDenominacion)
            cmd.CommandType = CommandType.Text

            ' criar parameter
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DENOMINACION", ProsegurDbType.Descricao_Curta, codigoDenominacion))

            ' executar query
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            ' Se encontrou algum registro
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                ' Popula a divisa
                Return PopularDivisa(dt.Rows(0), buscarDenominacion, buscarMediosPago, , True)

            End If

            Return Nothing

        End Function

        ''' <summary>
        ''' Cargar el objeto Divisa
        ''' </summary>
        ''' <param name="row"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [marcel.espiritosanto] 22/08/2013 Criado
        ''' </history>
        Private Shared Function PopularObjetoDivisa(row As DataRow) As Clases.Divisa

            ' criar objeto divisa
            Dim objDivisa As New Clases.Divisa

            Util.AtribuirValorObjeto(objDivisa.Identificador, row("OID_DIVISA"), GetType(String))
            Util.AtribuirValorObjeto(objDivisa.CodigoISO, row("COD_ISO_DIVISA"), GetType(String))
            Util.AtribuirValorObjeto(objDivisa.Descripcion, row("DES_DIVISA"), GetType(String))
            Util.AtribuirValorObjeto(objDivisa.CodigoSimbolo, row("COD_SIMBOLO"), GetType(String))
            Util.AtribuirValorObjeto(objDivisa.EstaActivo, row("BOL_VIGENTE"), GetType(Boolean))
            Util.AtribuirValorObjeto(objDivisa.CodigoUsuario, row("COD_USUARIO"), GetType(String))
            Util.AtribuirValorObjeto(objDivisa.FechaHoraTransporte, row("FYH_ACTUALIZACION"), GetType(String))

            ' retornar objeto divisa preenchido
            Return objDivisa

        End Function

        ''' <summary>
        ''' Cargar el objeto Divisa y Denominaciones
        ''' </summary>
        ''' <param name="row"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [marcel.espiritosanto] 22/08/2013 Criado
        ''' </history>
        Private Shared Function PopularDivisa(row As DataRow, _
                                     Optional BuscarDenominaciones As Boolean = True, _
                                     Optional BuscarMediosPago As Boolean = True, _
                                     Optional EsActivoDenominacion As Boolean = False, _
                                     Optional EsActivoMedioPago As Boolean = True, _
                                     Optional EliminarDenominacionZero As Boolean = False) As Clases.Divisa

            ' criar objeto divisa
            Dim objDivisa As New Clases.Divisa
            Dim objTotales As New ObservableCollection(Of Clases.ImporteTotal)

            Util.AtribuirValorObjeto(objDivisa.Identificador, row("OID_DIVISA"), GetType(String))
            Util.AtribuirValorObjeto(objDivisa.CodigoISO, row("COD_ISO_DIVISA"), GetType(String))
            Util.AtribuirValorObjeto(objDivisa.Descripcion, row("DES_DIVISA"), GetType(String))
            Util.AtribuirValorObjeto(objDivisa.CodigoSimbolo, row("COD_SIMBOLO"), GetType(String))
            Util.AtribuirValorObjeto(objDivisa.EstaActivo, row("BOL_VIGENTE"), GetType(Boolean))
            Util.AtribuirValorObjeto(objDivisa.CodigoUsuario, row("COD_USUARIO"), GetType(String))
            Util.AtribuirValorObjeto(objDivisa.FechaHoraTransporte, row("FYH_ACTUALIZACION"), GetType(DateTime))
            Util.AtribuirValorObjeto(objDivisa.CodigoAcceso, row("COD_ACCESO"), GetType(Char))
            Util.AtribuirValorObjeto(objDivisa.Color, row("COD_COLOR"), GetType(Drawing.Color))

            ' Se é para buscar denominações
            If BuscarDenominaciones Then

                ' crear objeto coleccion de denominaciones
                objDivisa.Denominaciones = Denominacion.ObtenerDenominacionesPorDivisa(objDivisa.Identificador, EsActivoDenominacion, EliminarDenominacionZero)

            End If

            ' Se é para buscar medios de pago
            If BuscarMediosPago Then

                ' crear objeto coleccion de mediospago
                ' OLD = objDivisa.MediosPago = MedioPago.ObtenerMediosPagoPorDivisa(objDivisa.Identificador, EsActivoMedioPago)
                objDivisa.MediosPago = MedioPago.ObtenerMediosPagoPorDivisa(objDivisa.Identificador, EsActivoMedioPago)

            End If

            objDivisa.ValoresTotales = objTotales

            ' retornar objeto divisa rellenado
            Return objDivisa

        End Function










        ''' <summary>
        ''' COPIA DO ObtenerValoresPorElemento - è para ser deletado futuramente.
        ''' </summary>
        ''' <param name="IdElemento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarValoresElemento(IdElemento As String, TipoElemento As Enumeradores.TipoElemento) As ObservableCollection(Of Clases.Divisa)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Select Case TipoElemento

                Case Enumeradores.TipoElemento.Remesa

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DivisaRecuperarValorConIdRemesa)

                Case Enumeradores.TipoElemento.Bulto

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DivisaRecuperarValorConIdBulto)

                Case Enumeradores.TipoElemento.Parcial

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DivisaRecuperarValorConIdParcial)

            End Select

            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ELEMENTO", ProsegurDbType.Identificador_Alfanumerico, IdElemento))
            'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, IdentificadorDocumento))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)


            Return PopularValores(dt, IdElemento, TipoElemento)
        End Function

        ''' <summary>
        ''' Validar divisas ativas
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ValidarItemsDivisasActivas(IdentificadoresDivisas As List(Of String),
                                                          IdentificadoresDenominaciones As List(Of String),
                                                          IdentificadoresMediosPagos As List(Of String)) As List(Of DataTable)

            Dim DataTables As New List(Of DataTable)
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            comando.CommandType = CommandType.Text

            If IdentificadoresDivisas IsNot Nothing AndAlso IdentificadoresDivisas.Count > 0 Then
                ' carrega consulta de divisas inativas
                comando.CommandText = My.Resources.RecuperarDivisasInactivas
                comando.CommandText = String.Format(comando.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, IdentificadoresDivisas, "OID_DIVISA", comando, "AND", "D"))
                comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

                Dim dtdivisa As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)
                DataTables.Add(dtdivisa)
                ' atribui nome para datatable 'GEPR_TDIVISA'
                dtdivisa.TableName = Enumeradores.Tabela.Divisa.RecuperarValor()

                If IdentificadoresDenominaciones IsNot Nothing AndAlso IdentificadoresDenominaciones.Count > 0 Then
                    ' carrega consulta de denominações inativas
                    comando.Parameters.Clear()
                    comando.CommandText = My.Resources.RecuperarDenominacionesInactivas
                    comando.CommandText = String.Format(comando.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, IdentificadoresDenominaciones, "OID_DENOMINACION", comando, "AND", "DE"))
                    comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

                    Dim dtdenominacion As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)
                    DataTables.Add(dtdenominacion)
                    ' atribui nome para datatable 'GEPR_TDENOMINACION'
                    dtdenominacion.TableName = Enumeradores.Tabela.Denominacion.RecuperarValor

                End If

                If IdentificadoresMediosPagos IsNot Nothing AndAlso IdentificadoresMediosPagos.Count > 0 Then

                    ' carrega consulta de meios de pagamentos inativos
                    comando.Parameters.Clear()
                    comando.CommandText = My.Resources.RecuperarMediosPagoInactivos
                    comando.CommandText = String.Format(comando.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, IdentificadoresMediosPagos, "OID_MEDIO_PAGO", comando, "AND", "MP"))
                    comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

                    Dim dtmedioPago As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)
                    DataTables.Add(dtmedioPago)
                    ' atribui nome para datatable 'GEPR_TMEDIO_PAGO'
                    dtmedioPago.TableName = Enumeradores.Tabela.MedioPago.RecuperarValor

                End If

            End If

            Return DataTables

        End Function

        Public Shared Function ObtenerPlanxDivisas(oidPlanificacion As String) As List(Of Clases.Divisa)


            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)


            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerPlanxDivisas)
            cmd.CommandType = CommandType.Text


            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PLANIFICACION", ProsegurDbType.Identificador_Alfanumerico, oidPlanificacion))


            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            Dim objDivisas As New List(Of Clases.Divisa)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each row As DataRow In dt.Rows

                    Dim objDivisa As New Clases.Divisa

                    ' preencher a coleção com objetos divisa
                    objDivisas.Add(PopularPlanxDivisa(row))

                Next row

            End If

            Return objDivisas

        End Function

        Private Shared Function PopularPlanxDivisa(row As DataRow) As Clases.Divisa
            Dim divisa = New Clases.Divisa
            Util.AtribuirValorObjeto(divisa.CodigoISO, row("COD_ISO_DIVISA"), GetType(String))
            Util.AtribuirValorObjeto(divisa.Descripcion, row("DES_DIVISA"), GetType(String))
            Return divisa
        End Function


    End Class

End Namespace