Imports Prosegur.Framework.Dicionario
Imports System.Transactions
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis
Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Xml.Serialization
Imports System.IO
Imports System.Xml
Imports System.Runtime.Serialization.Formatters.Binary

Namespace Genesis

    ''' <summary>
    ''' Clase AccionDivisas
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [marcel.espiritosanto] 22/08/2013 - Criado
    ''' </history>
    Public Class Divisas

        ''' <summary>
        ''' Obtene las divisas, pero solamente las informaciones necesarioas para los servicios de Integraciones
        ''' </summary>
        ''' <param name="valores">Valores que debe ser recuperados</param>
        ''' <returns>Una collecion de Clases.Divisa</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerDivisasParaIntegraciones(valores As ContractoServicio.Contractos.Integracion.Comon.Valores,
                                                      Optional codigoAjeno As String = "") As ObservableCollection(Of Clases.Divisa)
            Return AccesoDatos.Genesis.Divisas.ObtenerDivisasParaIntegraciones(valores, codigoAjeno)
        End Function










        Public Shared Function ObtenerValoresPorElemento(identificadorElemento As String, TipoElemento As Enumeradores.TipoElemento, _
                                                         ByRef denominacionesPosibles As ObservableCollection(Of Clases.Denominacion), _
                                                         ByRef mediopagoPosibles As ObservableCollection(Of Clases.MedioPago)) As ObservableCollection(Of Clases.Divisa)
            Dim objDivisas As New ObservableCollection(Of Clases.Divisa)
            Try
                Dim tdDivisas As DataTable = AccesoDatos.Genesis.Divisas.ObtenerValoresPorElemento(identificadorElemento, TipoElemento)
                objDivisas = cargarDivisasDelElemento(tdDivisas, identificadorElemento, TipoElemento, denominacionesPosibles, mediopagoPosibles)
            Catch ex As Exception
                Throw
            End Try
            Return objDivisas
        End Function


        Public Shared Function ObtenerValoresPorDocumento(oidDocumento As String, ByRef DivisasPosibles As ObservableCollection(Of Clases.Divisa)) As ObservableCollection(Of Clases.Divisa)
            Dim objDivisas As New ObservableCollection(Of Clases.Divisa)
            Try
                Dim tdDivisas As DataTable = AccesoDatos.Genesis.Divisas.ObtenerValoresPorDocumento(oidDocumento)
                objDivisas = cargarDivisas(tdDivisas, DivisasPosibles, True, )
            Catch ex As Exception
                Throw
            End Try
            Return objDivisas
        End Function

        Public Shared Sub configuracionNivelSaldos(ByRef _divisas As ObservableCollection(Of Clases.Divisa),
                                                   ConfiguracionNivelSaldos As Enumeradores.ConfiguracionNivelSaldos)

            If _divisas IsNot Nothing AndAlso _divisas.Count > 0 Then

                For Each _divisa In _divisas

                    _divisa.ValoresTotales = Nothing

                    If (_divisa.Denominaciones IsNot Nothing AndAlso _divisa.Denominaciones.Count > 0 AndAlso _
                        _divisa.ValoresTotalesEfectivo IsNot Nothing AndAlso _divisa.ValoresTotalesEfectivo.Count > 0) Then

                        ' Efectivos
                        Dim valorImporteTotal As Double = _divisa.ValoresTotalesEfectivo.Sum(Function(vtd) vtd.Importe)
                        Dim valorImporteDetalle As Double = 0
                        For Each den In _divisa.Denominaciones
                            If den.ValorDenominacion IsNot Nothing AndAlso den.ValorDenominacion.Count > 0 Then
                                valorImporteDetalle += den.ValorDenominacion.Sum(Function(vtd) vtd.Importe)
                            End If
                        Next

                        If valorImporteDetalle <> 0 AndAlso valorImporteTotal <> 0 Then
                            If ConfiguracionNivelSaldos = Enumeradores.ConfiguracionNivelSaldos.Detalle Then
                                _divisa.ValoresTotalesEfectivo = Nothing
                            ElseIf ConfiguracionNivelSaldos = Enumeradores.ConfiguracionNivelSaldos.Total Then
                                _divisa.Denominaciones = Nothing
                            End If
                        End If

                    End If

                    If (_divisa.MediosPago IsNot Nothing AndAlso _divisa.MediosPago.Count > 0 AndAlso _
                        _divisa.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso _divisa.ValoresTotalesTipoMedioPago.Count > 0) Then

                        ' Cheques
                        Dim valorTotalMedioPagoCheque As Comon.Clases.ValorTipoMedioPago = _divisa.ValoresTotalesTipoMedioPago.Where(Function(v) v.TipoMedioPago = Comon.Enumeradores.TipoMedioPago.Cheque AndAlso v.Importe <> 0).FirstOrDefault
                        If valorTotalMedioPagoCheque IsNot Nothing Then

                            Dim valorDetalleMedioPagoCheque As Comon.Clases.MedioPago = _divisa.MediosPago.Where(Function(v) v.Tipo = Comon.Enumeradores.TipoMedioPago.Cheque AndAlso v.Valores IsNot Nothing AndAlso v.Valores.Count > 0).FirstOrDefault
                            If valorDetalleMedioPagoCheque IsNot Nothing AndAlso valorDetalleMedioPagoCheque.Valores.FirstOrDefault(Function(x) x.Importe <> 0) IsNot Nothing Then

                                If ConfiguracionNivelSaldos = Enumeradores.ConfiguracionNivelSaldos.Detalle Then
                                    _divisa.ValoresTotalesTipoMedioPago.RemoveAll(Function(v) v.TipoMedioPago = Comon.Enumeradores.TipoMedioPago.Cheque)
                                ElseIf ConfiguracionNivelSaldos = Enumeradores.ConfiguracionNivelSaldos.Total Then
                                    _divisa.MediosPago.RemoveAll(Function(v) v.Tipo = Comon.Enumeradores.TipoMedioPago.Cheque)
                                End If

                            End If

                        End If

                        ' Tickets
                        Dim valorTotalMedioPagoTicket As Comon.Clases.ValorTipoMedioPago = _divisa.ValoresTotalesTipoMedioPago.Where(Function(v) v.TipoMedioPago = Comon.Enumeradores.TipoMedioPago.Ticket AndAlso v.Importe <> 0).FirstOrDefault
                        If valorTotalMedioPagoTicket IsNot Nothing AndAlso valorTotalMedioPagoTicket.Importe <> 0 Then

                            Dim valorDetalleMedioPagoTicket As Comon.Clases.MedioPago = _divisa.MediosPago.Where(Function(v) v.Tipo = Comon.Enumeradores.TipoMedioPago.Ticket AndAlso v.Valores IsNot Nothing AndAlso v.Valores.Count > 0).FirstOrDefault
                            If valorDetalleMedioPagoTicket IsNot Nothing AndAlso valorDetalleMedioPagoTicket.Valores.FirstOrDefault(Function(x) x.Importe <> 0) IsNot Nothing Then

                                If ConfiguracionNivelSaldos = Enumeradores.ConfiguracionNivelSaldos.Detalle Then
                                    _divisa.ValoresTotalesTipoMedioPago.RemoveAll(Function(v) v.TipoMedioPago = Comon.Enumeradores.TipoMedioPago.Ticket)
                                ElseIf ConfiguracionNivelSaldos = Enumeradores.ConfiguracionNivelSaldos.Total Then
                                    _divisa.MediosPago.RemoveAll(Function(v) v.Tipo = Comon.Enumeradores.TipoMedioPago.Ticket)
                                End If

                            End If

                        End If

                        ' Otros
                        Dim valorTotalMedioPagoOtros As Comon.Clases.ValorTipoMedioPago = _divisa.ValoresTotalesTipoMedioPago.Where(Function(v) v.TipoMedioPago = Comon.Enumeradores.TipoMedioPago.OtroValor AndAlso v.Importe <> 0).FirstOrDefault
                        If valorTotalMedioPagoOtros IsNot Nothing AndAlso valorTotalMedioPagoOtros.Importe <> 0 Then

                            Dim valorDetalleMedioPagoOtros As Comon.Clases.MedioPago = _divisa.MediosPago.Where(Function(v) v.Tipo = Comon.Enumeradores.TipoMedioPago.OtroValor AndAlso v.Valores IsNot Nothing AndAlso v.Valores.Count > 0).FirstOrDefault
                            If valorDetalleMedioPagoOtros IsNot Nothing AndAlso valorDetalleMedioPagoOtros.Valores.FirstOrDefault(Function(x) x.Importe <> 0) IsNot Nothing Then

                                If ConfiguracionNivelSaldos = Enumeradores.ConfiguracionNivelSaldos.Detalle Then
                                    _divisa.ValoresTotalesTipoMedioPago.RemoveAll(Function(v) v.TipoMedioPago = Comon.Enumeradores.TipoMedioPago.OtroValor)
                                ElseIf ConfiguracionNivelSaldos = Enumeradores.ConfiguracionNivelSaldos.Total Then
                                    _divisa.MediosPago.RemoveAll(Function(v) v.Tipo = Comon.Enumeradores.TipoMedioPago.OtroValor)
                                End If

                            End If

                        End If

                    End If
                Next

            End If

        End Sub

        Public Shared Function cargarDivisasDelElemento(dtDivisas As DataTable, _
                                                        IdElemento As String, _
                                                        TipoElemento As Enumeradores.TipoElemento, _
                                                        ByRef denominacionesPosibles As ObservableCollection(Of Clases.Denominacion), _
                                                        ByRef mediopagoPosibles As ObservableCollection(Of Clases.MedioPago)) As ObservableCollection(Of Clases.Divisa)

            Dim objDivisasRetorno As New ObservableCollection(Of Clases.Divisa)

            If dtDivisas IsNot Nothing AndAlso dtDivisas.Rows.Count > 0 Then

                Dim identificadorDivisa As String = String.Empty
                Dim identificadorDenominacion As String = String.Empty
                Dim identificadorMedioPago As String = String.Empty
                'Dim identificadorDocumento As String = String.Empty
                Dim objDenominacion As Clases.Denominacion = Nothing
                Dim objMedioPago As Clases.MedioPago = Nothing

                For Each objRow In dtDivisas.Rows

                    identificadorDivisa = Util.AtribuirValorObj(objRow("OID_DIVISA"), GetType(String))
                    identificadorDenominacion = Util.AtribuirValorObj(objRow("OID_DENOMINACION"), GetType(String))
                    identificadorMedioPago = Util.AtribuirValorObj(objRow("OID_MEDIO_PAGO"), GetType(String))
                    'identificadorDocumento = Util.AtribuirValorObj(objRow("OID_DOCUMENTO"), GetType(String))

                    If objDivisasRetorno.FirstOrDefault(Function(x) x.Identificador = identificadorDivisa) Is Nothing Then

                        objDivisasRetorno.Add(New Clases.Divisa With { _
                                       .CodigoISO = Util.AtribuirValorObj(objRow("COD_ISO_DIVISA"), GetType(String)), _
                                       .CodigoSimbolo = Util.AtribuirValorObj(objRow("COD_SIMBOLO"), GetType(String)), _
                                       .CodigoUsuario = Util.AtribuirValorObj(objRow("COD_USUARIO"), GetType(String)), _
                                       .Denominaciones = Nothing, _
                                       .Descripcion = Util.AtribuirValorObj(objRow("DES_DIVISA"), GetType(String)), _
                                       .EstaActivo = Util.AtribuirValorObj(objRow("DIVISA_ACTIVA"), GetType(String)), _
                                       .FechaHoraTransporte = Util.AtribuirValorObj(objRow("FYH_ACTUALIZACION"), GetType(String)), _
                                       .Icono = Nothing, _
                                       .Identificador = identificadorDivisa, _
                                       .MediosPago = Nothing, _
                                       .Color = Util.AtribuirValorObj(objRow("COD_COLOR"), GetType(Drawing.Color))})
                    End If

                    Dim objDivisa As Clases.Divisa = objDivisasRetorno.FirstOrDefault(Function(x) x.Identificador = identificadorDivisa)

                    If String.IsNullOrEmpty(identificadorDenominacion) AndAlso String.IsNullOrEmpty(identificadorMedioPago) Then
                        With objDivisa
                            Dim TotalesEfectivo As ObservableCollection(Of Clases.ValorEfectivo) = CargarValores(Of Clases.ValorEfectivo)(objRow)
                            If TotalesEfectivo IsNot Nothing AndAlso TotalesEfectivo.Count > 0 Then
                                If .ValoresTotalesEfectivo Is Nothing Then .ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)
                                .ValoresTotalesEfectivo.AddRange(TotalesEfectivo)
                            End If

                            Dim TotalesDivisa As ObservableCollection(Of Clases.ValorDivisa) = CargarValores(Of Clases.ValorDivisa)(objRow)
                            If TotalesDivisa IsNot Nothing AndAlso TotalesDivisa.Count > 0 Then
                                If .ValoresTotalesDivisa Is Nothing Then .ValoresTotalesDivisa = New ObservableCollection(Of Clases.ValorDivisa)
                                .ValoresTotalesDivisa.AddRange(TotalesDivisa)
                            End If

                            Dim TotalesTipoMedioPago As ObservableCollection(Of Clases.ValorTipoMedioPago) = CargarValores(Of Clases.ValorTipoMedioPago)(objRow)
                            If TotalesTipoMedioPago IsNot Nothing AndAlso TotalesTipoMedioPago.Count > 0 Then
                                If .ValoresTotalesTipoMedioPago Is Nothing Then .ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)
                                .ValoresTotalesTipoMedioPago.AddRange(TotalesTipoMedioPago)
                            End If
                        End With
                    End If

                    'Se exister denominação, recupera os dados da denominação
                    If Not String.IsNullOrEmpty(identificadorDenominacion) Then
                        If objDivisa.Denominaciones Is Nothing Then objDivisa.Denominaciones = New ObservableCollection(Of Clases.Denominacion)

                        objDenominacion = (From den In objDivisa.Denominaciones Where den.Identificador = identificadorDenominacion).FirstOrDefault
                        If objDenominacion Is Nothing Then

                            objDenominacion = denominacionesPosibles.FirstOrDefault(Function(x) x.Identificador = identificadorDenominacion).Clonar
                            If objDenominacion Is Nothing Then
                                objDivisa.Denominaciones.Add(AccesoDatos.Genesis.Denominacion.RecuperarDenominacion(identificadorDenominacion))
                                objDenominacion = (From den In objDivisa.Denominaciones Where den.Identificador = identificadorDenominacion).FirstOrDefault

                                denominacionesPosibles.Add(objDenominacion)
                            Else
                                objDivisa.Denominaciones.Add(objDenominacion)
                            End If
                        End If
                        objDivisa.Denominaciones.FirstOrDefault(Function(x) x.Identificador = identificadorDenominacion).ValorDenominacion = AccesoDatos.Genesis.Divisas.RecuperarValorEfectivoPorDenominacion(IdElemento, TipoElemento, identificadorDenominacion)
                    End If

                    'Se existir valores para medio pago recupera o valor do medio pago
                    If Not String.IsNullOrEmpty(identificadorMedioPago) Then
                        If objDivisa.MediosPago Is Nothing Then objDivisa.MediosPago = New ObservableCollection(Of Clases.MedioPago)

                        objMedioPago = (From mp In objDivisa.MediosPago Where mp.Identificador = identificadorMedioPago).FirstOrDefault
                        If objMedioPago Is Nothing Then

                            objMedioPago = mediopagoPosibles.FirstOrDefault(Function(x) x.Identificador = identificadorMedioPago).Clonar
                            If objMedioPago Is Nothing Then
                                objDivisa.MediosPago.Add(AccesoDatos.Genesis.MedioPago.RecuperarMedioPago(identificadorMedioPago))
                                objMedioPago = (From mp In objDivisa.MediosPago Where mp.Identificador = identificadorMedioPago).FirstOrDefault

                                mediopagoPosibles.Add(objMedioPago)
                            Else
                                objDivisa.MediosPago.Add(objMedioPago)
                            End If
                        End If
                        objDivisa.MediosPago.FirstOrDefault(Function(x) x.Identificador = identificadorMedioPago).Valores = AccesoDatos.Genesis.Divisas.RecuperarValorMedioPago(IdElemento, TipoElemento, identificadorMedioPago, objMedioPago.Terminos)
                    End If

                Next
            Else
                Return Nothing
            End If

            Return objDivisasRetorno
        End Function

        Public Shared Function Clonar(obj As Object) As Object
            Dim xml As String = ObjectToXml(obj, obj.GetType)

            Dim clone As Object = XmlToObject(xml, obj.GetType)

            Return clone
        End Function

        Public Shared Function XmlToObject(Xml As String, ObjType As System.Type) As Object

            Dim ser As XmlSerializer
            ser = New XmlSerializer(ObjType)
            Dim strReader As StringReader
            strReader = New StringReader(Xml)
            Dim xmlReader As XmlTextReader
            xmlReader = New XmlTextReader(strReader)
            Dim obj As Object
            obj = ser.Deserialize(xmlReader)
            xmlReader.Close()
            strReader.Close()
            Return obj

        End Function

        Public Shared Function ObjectToXml(Obj As Object, ObjType As System.Type) As String
            Dim writer As StringWriter = New StringWriter
            Dim serializer As XmlSerializer = New XmlSerializer(ObjType)

            serializer.Serialize(writer, Obj)

            Return writer.ToString()
        End Function

        Public Shared Function cargarDivisas(dtDivisas As DataTable, _
                                                DivisasPosibles As ObservableCollection(Of Clases.Divisa), _
                                                Optional rellenarTipoValorNoDefinido As Boolean = False, _
                                                Optional esDisponibleNoDefinido As Boolean = False, _
                                                Optional identificadorDocumento As String = "",
                                                Optional dtUnidadMedida As DataTable = Nothing,
                                                Optional dtCalidad As DataTable = Nothing,
                                                Optional EsConsiderarSomaZero As Boolean = False) As ObservableCollection(Of Clases.Divisa)

            Dim objDivisasRetorno As New ObservableCollection(Of Clases.Divisa)

            If dtDivisas IsNot Nothing AndAlso dtDivisas.Rows.Count > 0 Then

                Dim IdentificadorDivisa As String = String.Empty
                Dim IdentificadorDenominacion As String = String.Empty
                Dim IdentificadorMedioPago As String = String.Empty
                Dim objDivisa As Clases.Divisa = Nothing
                Dim objDenominacion As Clases.Denominacion = Nothing
                Dim objMedioPago As Clases.MedioPago = Nothing
                Dim CodTipoMedioPago As String = String.Empty
                Dim CodNivelDetalle As String = String.Empty
                Dim Disponible As Boolean

                For Each objRow As DataRow In dtDivisas.Rows

                    IdentificadorDivisa = Util.AtribuirValorObj(objRow("OID_DIVISA"), GetType(String))
                    IdentificadorDenominacion = Util.AtribuirValorObj(objRow("OID_DENOMINACION"), GetType(String))
                    IdentificadorMedioPago = Util.AtribuirValorObj(objRow("OID_MEDIO_PAGO"), GetType(String))
                    CodTipoMedioPago = Util.AtribuirValorObj(objRow("COD_TIPO_MEDIO_PAGO"), GetType(String))
                    CodNivelDetalle = Util.AtribuirValorObj(objRow("COD_NIVEL_DETALLE"), GetType(String))
                    Disponible = Util.AtribuirValorObj(objRow("BOL_DISPONIBLE"), GetType(Boolean))
                    Dim TipoValor As Enumeradores.TipoValor = If(rellenarTipoValorNoDefinido, Enumeradores.TipoValor.NoDefinido, If(esDisponibleNoDefinido, If(Disponible, Enumeradores.TipoValor.NoDefinido, Enumeradores.TipoValor.NoDisponible), If(Disponible, Enumeradores.TipoValor.Disponible, Enumeradores.TipoValor.NoDisponible)))

                    objDivisa = objDivisasRetorno.FirstOrDefault(Function(x) x.Identificador = IdentificadorDivisa)

                    If objDivisa Is Nothing Then
                        Dim _divisa As Clases.Divisa = DivisasPosibles.FirstOrDefault(Function(x) x.Identificador = IdentificadorDivisa)
                        objDivisa = Clonar(_divisa)
                        objDivisa.Color = _divisa.Color
                        objDivisasRetorno.Add(objDivisa)
                    End If

                    'Verifica se a divisa ja foi adicionada na lista de divisas
                    objDivisa = objDivisasRetorno.FirstOrDefault(Function(x) x.Identificador = IdentificadorDivisa)

                    If objDivisa Is Nothing Then

                        objDivisasRetorno.Add(DivisasPosibles.FirstOrDefault(Function(x) x.Identificador = IdentificadorDivisa))

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
                                                                     .TipoValor = TipoValor, _
                                                                     .Importe = Util.AtribuirValorObj(objRow("IMPORTE"), GetType(Decimal))})

                                ' Verifica se é total geral
                            ElseIf CodNivelDetalle = Enumeradores.TipoNivelDetalhe.TotalGeral.RecuperarValor Then

                                If objDivisa.ValoresTotalesDivisa Is Nothing Then objDivisa.ValoresTotalesDivisa = New ObservableCollection(Of Clases.ValorDivisa)
                                objDivisa.ValoresTotalesDivisa.Add(New Clases.ValorDivisa With { _
                                                                     .TipoValor = TipoValor, _
                                                                     .Importe = Util.AtribuirValorObj(objRow("IMPORTE"), GetType(Decimal))})
                            End If

                        Else

                            If objDivisa.ValoresTotalesTipoMedioPago Is Nothing Then objDivisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)
                            objDivisa.ValoresTotalesTipoMedioPago.Add(New Clases.ValorTipoMedioPago With { _
                                                                      .TipoValor = TipoValor, _
                                                                      .Importe = Util.AtribuirValorObj(objRow("IMPORTE"), GetType(Decimal)), _
                                                                      .Cantidad = Util.AtribuirValorObj(objRow("CANTIDAD"), GetType(Int64)), _
                                                                      .TipoMedioPago = Extenciones.RecuperarEnum(Of Enumeradores.TipoMedioPago)(Util.AtribuirValorObj(objRow("COD_TIPO_MEDIO_PAGO"), GetType(String)))})

                        End If

                    End If

                    If Not String.IsNullOrEmpty(IdentificadorDenominacion) AndAlso objDivisa.Denominaciones IsNot Nothing AndAlso objDivisa.Denominaciones.Count > 0 Then

                        objDenominacion = (From den In objDivisa.Denominaciones Where den.Identificador = IdentificadorDenominacion).FirstOrDefault

                        If objDenominacion IsNot Nothing Then

                            If objDenominacion.ValorDenominacion Is Nothing Then objDenominacion.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)

                            Dim objValor As New Clases.ValorDenominacion
                            objValor.Cantidad = Util.AtribuirValorObj(objRow("CANTIDAD"), GetType(Int64))
                            objValor.Importe = Util.AtribuirValorObj(objRow("IMPORTE"), GetType(Decimal))
                            objValor.TipoValor = TipoValor

                            If objRow.Table.Columns.Contains("OID_CALIDAD") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_CALIDAD"), GetType(String))) AndAlso _
                                               dtCalidad IsNot Nothing AndAlso dtCalidad.Rows.Count > 0 Then

                                Dim calidad = dtCalidad.Select("OID_CALIDAD = '" & Util.AtribuirValorObj(objRow("OID_CALIDAD"), GetType(String)) & "'")

                                If calidad IsNot Nothing AndAlso calidad.Count > 0 Then
                                    objValor.Calidad = New Clases.Calidad With {
                                                    .Identificador = If(calidad(0).Table.Columns.Contains("OID_CALIDAD"), Util.AtribuirValorObj(calidad(0)("OID_CALIDAD"), GetType(String)), Nothing),
                                                    .Codigo = If(calidad(0).Table.Columns.Contains("COD_CALIDAD"), Util.AtribuirValorObj(calidad(0)("COD_CALIDAD"), GetType(String)), Nothing),
                                                    .Descripcion = If(calidad(0).Table.Columns.Contains("DES_CALIDAD"), Util.AtribuirValorObj(calidad(0)("DES_CALIDAD"), GetType(String)), Nothing)
                                        }
                                End If

                            End If

                            If objRow.Table.Columns.Contains("OID_UNIDAD_MEDIDA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_UNIDAD_MEDIDA"), GetType(String))) AndAlso _
                                                dtUnidadMedida IsNot Nothing AndAlso dtUnidadMedida.Rows.Count > 0 Then

                                Dim unidadMedida = dtUnidadMedida.Select("OID_UNIDAD_MEDIDA = '" & Util.AtribuirValorObj(objRow("OID_UNIDAD_MEDIDA"), GetType(String)) & "'")

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


                    If Not String.IsNullOrEmpty(IdentificadorMedioPago) AndAlso objDivisa.MediosPago IsNot Nothing AndAlso objDivisa.MediosPago.Count > 0 Then

                        objMedioPago = (From MP In objDivisa.MediosPago Where MP.Identificador = IdentificadorMedioPago).FirstOrDefault

                        If objMedioPago IsNot Nothing Then


                            Dim objValoresTerminos As New ObservableCollection(Of Clases.Termino)
                            If (Not String.IsNullOrEmpty(identificadorDocumento)) AndAlso objMedioPago.Terminos IsNot Nothing Then
                                For Each objTermino In objMedioPago.Terminos.Clonar()
                                    Dim objValoresTerminosMedioPago As ObservableCollection(Of Clases.Termino) = AccesoDatos.Genesis.ValorTerminoMedioPago.RecuperarListaValorTerminoPorDocumento(objTermino.Identificador, identificadorDocumento)

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
                            objMedioPago.Valores.Add(New Clases.ValorMedioPago With {.Cantidad = Util.AtribuirValorObj(objRow("CANTIDAD"), GetType(Int64)), _
                                                                                     .Importe = Util.AtribuirValorObj(objRow("IMPORTE"), GetType(Decimal)), _
                                                                                     .Terminos = objValoresTerminos, _
                                                                                     .TipoValor = TipoValor})


                        End If

                    End If


                    If objDivisa.ValoresTotales Is Nothing Then
                        objDivisa.ValoresTotales = New ObservableCollection(Of Clases.ImporteTotal)
                    End If

                    If objDivisa.ValoresTotales.FirstOrDefault(Function(x) x.TipoValor = TipoValor) IsNot Nothing Then
                        objDivisa.ValoresTotales.FirstOrDefault(Function(x) x.TipoValor = TipoValor).Importe += Util.AtribuirValorObj(objRow("IMPORTE"), GetType(Decimal))
                    Else
                        Dim Total As New Clases.ImporteTotal
                        Total.Importe = Util.AtribuirValorObj(objRow("IMPORTE"), GetType(Decimal))
                        Total.TipoValor = TipoValor
                        objDivisa.ValoresTotales.Add(Total)
                    End If


                Next

            End If

            Prosegur.Genesis.Comon.Util.BorrarItemsDivisasSinValoresCantidades(objDivisasRetorno)
            Prosegur.Genesis.Comon.Util.CalcularTotalesItemsDivisas(objDivisasRetorno, Enumeradores.TipoValor.NoDefinido)

            If Not EsConsiderarSomaZero Then
                'Remove os valores que em que o total da divisa for zero
                If objDivisasRetorno IsNot Nothing AndAlso objDivisasRetorno.Count > 0 Then
                    objDivisasRetorno = objDivisasRetorno.Where(Function(t) t.ValoresTotales IsNot Nothing AndAlso t.ValoresTotales.Count > 0 AndAlso t.ValoresTotales(0).Importe <> 0).ToObservableCollection
                End If
            End If

            Return objDivisasRetorno
        End Function



        Public Shared Function CargarValores(Of T As Clases.Valor)(objRow As DataRow) As ObservableCollection(Of T)

            Dim objValor As Clases.Valor = Nothing
            Dim objValores As ObservableCollection(Of T) = Nothing

            'nivelDetalle = Enumeradores.TipoNivelDetalhe.Total
            If GetType(T) Is GetType(Clases.ValorEfectivo) Then

                If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("T_Diferencia"), GetType(String))) Then
                    If objValores Is Nothing Then objValores = New ObservableCollection(Of T)

                    objValor = New Clases.ValorEfectivo
                    With objValor
                        .Importe = Util.AtribuirValorObj(objRow("T_Diferencia"), GetType(String))
                        .TipoValor = Enumeradores.TipoValor.Diferencia
                    End With

                    objValores.Add(objValor)
                End If


                If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("T_Declarado"), GetType(String))) Then
                    If objValores Is Nothing Then objValores = New ObservableCollection(Of T)

                    objValor = New Clases.ValorEfectivo
                    With objValor
                        .Importe = Util.AtribuirValorObj(objRow("T_Declarado"), GetType(String))
                        .TipoValor = Enumeradores.TipoValor.Declarado
                    End With

                    objValores.Add(objValor)

                End If

            End If

            'nivelDetalle = Enumeradores.TipoNivelDetalhe.TotalGeral
            If GetType(T) Is GetType(Clases.ValorDivisa) Then

                If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("G_Diferencia"), GetType(String))) Then
                    If objValores Is Nothing Then objValores = New ObservableCollection(Of T)

                    objValor = New Clases.ValorDivisa
                    With objValor
                        .Importe = Util.AtribuirValorObj(objRow("G_Diferencia"), GetType(String))
                        .TipoValor = Enumeradores.TipoValor.Diferencia
                    End With

                    objValores.Add(objValor)
                End If


                If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("G_Declarado"), GetType(String))) Then
                    If objValores Is Nothing Then objValores = New ObservableCollection(Of T)

                    objValor = New Clases.ValorDivisa
                    With objValor
                        .Importe = Util.AtribuirValorObj(objRow("G_Declarado"), GetType(String))
                        .TipoValor = Enumeradores.TipoValor.Declarado
                    End With

                    objValores.Add(objValor)

                End If

            End If

            If GetType(T) Is GetType(Clases.ValorTipoMedioPago) Then

                If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("MP_Diferencia"), GetType(String))) Then
                    If objValores Is Nothing Then objValores = New ObservableCollection(Of T)

                    objValor = New Clases.ValorTipoMedioPago With { _
                    .Importe = Util.AtribuirValorObj(objRow("MP_Diferencia"), GetType(String)), _
                    .TipoValor = Enumeradores.TipoValor.Diferencia, _
                    .TipoMedioPago = Extenciones.RecuperarEnum(Of Enumeradores.TipoMedioPago)(Util.AtribuirValorObj(objRow("COD_TIPO_MEDIO_PAGO"), GetType(String))), _
                    .InformadoPor = Enumeradores.TipoContado.NoDefinido}

                    objValores.Add(objValor)
                End If


                If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("MP_Declarado"), GetType(String))) Then
                    If objValores Is Nothing Then objValores = New ObservableCollection(Of T)

                    objValor = New Clases.ValorTipoMedioPago With { _
                    .Importe = Util.AtribuirValorObj(objRow("MP_Declarado"), GetType(String)), _
                    .TipoValor = Enumeradores.TipoValor.Declarado, _
                    .TipoMedioPago = Extenciones.RecuperarEnum(Of Enumeradores.TipoMedioPago)(Util.AtribuirValorObj(objRow("COD_TIPO_MEDIO_PAGO"), GetType(String))), _
                    .InformadoPor = Enumeradores.TipoContado.NoDefinido}

                    objValores.Add(objValor)

                End If

            End If


            Return objValores
        End Function












        ''' <summary>
        ''' Recupera las divisas por los filtros.
        ''' </summary>
        ''' <param name="Divisa">Los filtros usados en la consulta son: 
        '''                      Identificador, Descripcion, CodigoIso caso hay valores</param> 
        ''' <param name="ListaCodigoIso">Es usado caso no hay un objDivisa, entonces el filtro será por una lista de CodigosIso.</param>
        ''' <param name="EsNotInCodigoIso">Define si la clausula IN del ListaCodigoIso arriba, se quedará por NotIn o In. Case True "NotIn", case False "In"</param> 
        '''<param name="EsActivo">Define el estado de las divisas a seren buscadas</param>
        ''' <param name="BuscarDenominaciones">Define se serán buscadas las denominaciones de la divisa</param>
        ''' <param name="BuscarMediosPago">Define se serán buscados los medios pagos de la divisa</param>
        ''' <param name="EsActivoDenominacion">Define se las denominaciones son activos</param>
        ''' <param name="EsActivoMedioPago">Define se los medios pago son activos</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <Observacion>
        ''' Caso no hay filtros para ejecutar lá búsqueda, el método buscará todas las divisas activas.
        ''' </Observacion>
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
                                              Optional EsActivoMedioPago As Boolean = False, _
                                              Optional EliminarDenominacionZero As Boolean = False) As ObservableCollection(Of Clases.Divisa)

            Try
                Return AccesoDatos.Genesis.Divisas.ObtenerDivisas(Divisa, ListaCodigoIso, EsNotInCodigoIso, EsActivo, BuscarDenominaciones, BuscarMediosPago, EsActivoDenominacion, EsActivoMedioPago, EliminarDenominacionZero)

            Catch ex As Exception
                Throw

            End Try

        End Function

        Public Shared Function ObtenerListaDivisas() As List(Of Clases.Abono.AbonoInformacion)
            Return AccesoDatos.Genesis.Divisas.ObtenerListaDivisas()
        End Function

        ''' <summary>
        ''' Recupera a divisa de acordo com o codigo da denominação
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarDivisaPorCodigoDenominacion(codigoDenominacion As String) As Clases.Divisa

            ' Variável que receberá a divisa
            Dim divisa As Comon.Clases.Divisa = Nothing

            ' Se o código da denominação foi informado
            If Not String.IsNullOrEmpty(codigoDenominacion) Then

                ' recupera os dados da divisa
                divisa = AccesoDatos.Genesis.Divisas.ObtenerDivisaPorCodigoDenominacion(codigoDenominacion)

            End If

            ' Retorna a divisa
            Return divisa

        End Function


        Public Shared Function ObtenerPlanxDivisas(oidPlanificacion As String) As List(Of Clases.Divisa)

            Dim resultado As List(Of Clases.Divisa) = New List(Of Clases.Divisa)

            If Not String.IsNullOrEmpty(oidPlanificacion) Then

                resultado = AccesoDatos.Genesis.Divisas.ObtenerPlanxDivisas(oidPlanificacion)

            End If

            Return resultado

        End Function

        Public Shared Function PreencherDivisas(objTabela As DataTable)

            Dim objDivisas As ObservableCollection(Of Clases.Divisa) = AccesoDatos.Genesis.Divisas.ObtenerDivisas(objTabela.AsEnumerable() _
                                                                                                                   .Select(Function(r) r.Field(Of String)("OID_DIVISA")) _
                                                                                                                   .Distinct() _
                                                                                                                   .ToList())
            For Each objDivisa In objDivisas


                Dim identificadoresDenominaciones As List(Of String) = objTabela.AsEnumerable() _
                                                                            .Where(Function(r) Not String.IsNullOrEmpty(r.Field(Of String)("OID_DENOMINACION"))) _
                                                                            .Select(Function(r) r.Field(Of String)("OID_DENOMINACION")) _
                                                                            .Distinct() _
                                                                            .ToList()

                objDivisa.Denominaciones = AccesoDatos.Genesis.Denominacion.RecuperarDenominaciones(objDivisa.Identificador, New ObservableCollection(Of String)(identificadoresDenominaciones))

                objDivisa.MediosPago = LogicaNegocio.Genesis.MedioPago.ObtenerMediosPago(objDivisa.Identificador, objTabela.AsEnumerable() _
                                                                            .Where(Function(r) Not String.IsNullOrEmpty(r.Field(Of String)("OID_MEDIO_PAGO"))) _
                                                                            .Select(Function(r) r.Field(Of String)("OID_MEDIO_PAGO")) _
                                                                            .Distinct() _
                                                                            .ToList())

            Next

            Return objDivisas
        End Function

    End Class

End Namespace