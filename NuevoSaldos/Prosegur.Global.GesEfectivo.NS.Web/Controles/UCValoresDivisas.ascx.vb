Imports System.Runtime.Serialization
Imports System.Web.Services
Imports Prosegur.Genesis.Comon
Imports Newtonsoft.Json
Imports Prosegur.Global.GesEfectivo.NuevoSaldos.Web.UCValoresDivisas.ParametrosUCValoresDivisas
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Framework.Dicionario
Imports System.Collections.ObjectModel

Public Class UCValoresDivisas
    Inherits UcBase

#Region "Propriedades"

    Public Property ExhibirTitulo As Boolean = True
    Public Property ExhibirTotalesDivisa As Boolean = True
    Public Property ExhibirTotalesEfectivo As Boolean = True
    Public Property ExhibirTotalesMedioPago As Boolean = True

    Private _DivisasPostback As ObservableCollection(Of Clases.Divisa)
    Private _Divisas As ObservableCollection(Of Clases.Divisa)
    Private _DivisasRetorno As ObservableCollection(Of Clases.Divisa) = Nothing
    Public Property Divisas As ObservableCollection(Of Clases.Divisa)
        Get

            Dim retorno As ObservableCollection(Of Clases.Divisa)
            If Page.IsPostBack Then
                If _DivisasRetorno Is Nothing Then

                    Try
                        _DivisasRetorno = New ObservableCollection(Of Clases.Divisa)
                        'Recupera os campos hiddens serializados
                        Dim totalesDivisa As Linq.JArray = JsonConvert.DeserializeObject(hdnTotalesPorDivisa.Value, New JsonSerializerSettings() With {.Converters = {New Converters.StringEnumConverter()}})
                        Dim totalesEfectivo As Linq.JArray = JsonConvert.DeserializeObject(hdnTotalesEfectivo.Value, New JsonSerializerSettings() With {.Converters = {New Converters.StringEnumConverter()}})
                        Dim totalesMedioPago As Linq.JArray = JsonConvert.DeserializeObject(hdnTotalesMedioPago.Value, New JsonSerializerSettings() With {.Converters = {New Converters.StringEnumConverter()}})

                        Dim divisa As Clases.Divisa = Nothing
                        Dim denominacion As Clases.Denominacion = Nothing
                        Dim medioPago As Clases.MedioPago = Nothing
                        Dim unidadMedida As Clases.UnidadMedida = Nothing
                        Dim calidad As Clases.Calidad = Nothing

                        If totalesDivisa IsNot Nothing Then
                            'Realiza o distinct nos totales divisa somando as linhas duplicadas
                            Dim totalesDivisaDistintos = (From
                                                            totalDivisa
                                                        In
                                                            (From
                                                                itemTotalDivisa
                                                            In
                                                                totalesDivisa
                                                            Select New With {
                                                                .IdentificadorDivisa = If(itemTotalDivisa("Divisa") Is Nothing, String.Empty, itemTotalDivisa("Divisa").Value(Of String)("Identificador")),
                                                                .TotalGeneral = ParseDecimal(itemTotalDivisa.Value(Of String)("TotalGeneral")),
                                                                .TotalEfectivo = ParseDecimal(itemTotalDivisa.Value(Of String)("TotalEfectivo")),
                                                                .TotalOtroValor = ParseDecimal(itemTotalDivisa.Value(Of String)("TotalOtroValor")),
                                                                .TotalCheque = ParseDecimal(itemTotalDivisa.Value(Of String)("TotalCheque")),
                                                                .TotalTarjeta = ParseDecimal(itemTotalDivisa.Value(Of String)("TotalTarjeta")),
                                                                .TotalTicket = ParseDecimal(itemTotalDivisa.Value(Of String)("TotalTicket"))})
                                                        Group
                                                            totalDivisa By
                                                                totalDivisa.IdentificadorDivisa Into Group
                                                        Select New With {
                                                            .IdentificadorDivisa = IdentificadorDivisa,
                                                            .TotalGeneral = Group.Sum(Function(d) d.TotalGeneral),
                                                            .TotalEfectivo = Group.Sum(Function(d) d.TotalEfectivo),
                                                            .TotalOtroValor = Group.Sum(Function(d) d.TotalOtroValor),
                                                            .TotalCheque = Group.Sum(Function(d) d.TotalCheque),
                                                            .TotalTarjeta = Group.Sum(Function(d) d.TotalTarjeta),
                                                            .TotalTicket = Group.Sum(Function(d) d.TotalTicket)})

                            'preenche os totales por divisa
                            For Each totalDivisa In totalesDivisaDistintos
                                Dim totalDivisaLocal = totalDivisa
                                If Not String.IsNullOrEmpty(totalDivisaLocal.IdentificadorDivisa) AndAlso _
                                    _DivisasRetorno.Find(Function(divisaRetorno) divisaRetorno.Identificador = totalDivisaLocal.IdentificadorDivisa) Is Nothing Then
                                    divisa = DivisasDisponiveis.Find(Function(divisaDisponivel) divisaDisponivel.Identificador = totalDivisaLocal.IdentificadorDivisa).GetDivisa().Clonar()
                                    divisa.ValoresTotalesDivisa = New ObservableCollection(Of Clases.ValorDivisa)()
                                    divisa.ValoresTotalesDivisa.Add(New Clases.ValorDivisa() With {.Importe = totalDivisa.TotalGeneral, .InformadoPor = Enumeradores.TipoContado.NoDefinido, .TipoValor = TipoValor})
                                    divisa.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)()
                                    divisa.ValoresTotalesEfectivo.Add(New Clases.ValorEfectivo() With {.Importe = totalDivisa.TotalEfectivo, .InformadoPor = Enumeradores.TipoContado.NoDefinido, .TipoValor = TipoValor, .TipoDetalleEfectivo = Enumeradores.TipoDetalleEfectivo.Mezcla})
                                    divisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)()
                                    divisa.ValoresTotalesTipoMedioPago.Add(New Clases.ValorTipoMedioPago() With {.Importe = totalDivisa.TotalOtroValor, .InformadoPor = Enumeradores.TipoContado.NoDefinido, .TipoValor = TipoValor, .TipoMedioPago = Enumeradores.TipoMedioPago.OtroValor, .Cantidad = 0})
                                    divisa.ValoresTotalesTipoMedioPago.Add(New Clases.ValorTipoMedioPago() With {.Importe = totalDivisa.TotalCheque, .InformadoPor = Enumeradores.TipoContado.NoDefinido, .TipoValor = TipoValor, .TipoMedioPago = Enumeradores.TipoMedioPago.Cheque, .Cantidad = 0})
                                    divisa.ValoresTotalesTipoMedioPago.Add(New Clases.ValorTipoMedioPago() With {.Importe = totalDivisa.TotalTarjeta, .InformadoPor = Enumeradores.TipoContado.NoDefinido, .TipoValor = TipoValor, .TipoMedioPago = Enumeradores.TipoMedioPago.Tarjeta, .Cantidad = 0})
                                    divisa.ValoresTotalesTipoMedioPago.Add(New Clases.ValorTipoMedioPago() With {.Importe = totalDivisa.TotalTicket, .InformadoPor = Enumeradores.TipoContado.NoDefinido, .TipoValor = TipoValor, .TipoMedioPago = Enumeradores.TipoMedioPago.Ticket, .Cantidad = 0})
                                    divisa.Denominaciones = New ObservableCollection(Of Clases.Denominacion)()
                                    divisa.MediosPago = New ObservableCollection(Of Clases.MedioPago)()
                                    _DivisasRetorno.Add(divisa)
                                End If
                            Next

                        End If

                        If totalesEfectivo IsNot Nothing Then

                            'Realiza o distinct nos totales efectivo somando as linhas duplicadas
                            Dim totalesEfectivoDistintos = (From
                                                                totalEfectivo
                                                            In
                                                            (From
                                                                itemTotalEfectivo
                                                            In
                                                                totalesEfectivo Where itemTotalEfectivo("Divisa") IsNot Nothing
                                                            Select New With {
                                                                .IdentificadorDivisa = itemTotalEfectivo("Divisa").Value(Of String)("Identificador"),
                                                                .IdentificadorDenominacion = If(itemTotalEfectivo("Denominacion") Is Nothing, String.Empty, itemTotalEfectivo("Denominacion").Value(Of String)("Identificador")),
                                                                .IdentificadorUnidadMedida = If(itemTotalEfectivo("UnidadMedida") Is Nothing, String.Empty, itemTotalEfectivo("UnidadMedida").Value(Of String)("Identificador")),
                                                                .Cantidad = ParseInteiro(itemTotalEfectivo.Value(Of String)("Cantidad")),
                                                                .Valor = ParseDecimal(itemTotalEfectivo.Value(Of String)("Valor")),
                                                                .IdentificadorCalidad = If(itemTotalEfectivo("Calidad") Is Nothing, String.Empty, itemTotalEfectivo("Calidad").Value(Of String)("Identificador"))})
                                                            Group totalEfectivo By
                            totalEfectivo.IdentificadorDivisa,
                            totalEfectivo.IdentificadorDenominacion,
                            totalEfectivo.IdentificadorUnidadMedida,
                            totalEfectivo.IdentificadorCalidad
                            Into Group
                                                            Select New With {
                                                                .IdentificadorDivisa = IdentificadorDivisa,
                                                                .IdentificadorDenominacion = IdentificadorDenominacion,
                                                                .IdentificadorUnidadMedida = IdentificadorUnidadMedida,
                                                                .Cantidad = Group.Sum(Function(te) te.Cantidad),
                                                                .Valor = Group.Sum(Function(te) te.Valor),
                                                                .IdentificadorCalidad = IdentificadorCalidad})

                            Dim identificadorDivisaEfectivo As String = String.Empty
                            Dim identificadorDenominacionEfectivo As String = String.Empty
                            For Each totalEfectivo In totalesEfectivoDistintos
                                Dim totalEfectivoLocal = totalEfectivo
                                If identificadorDivisaEfectivo <> totalEfectivo.IdentificadorDivisa Then
                                    identificadorDivisaEfectivo = totalEfectivo.IdentificadorDivisa
                                    identificadorDenominacionEfectivo = String.Empty
                                    divisa = _DivisasRetorno.Find(Function(divisaRetorno) divisaRetorno.Identificador = identificadorDivisaEfectivo)
                                    If divisa Is Nothing Then
                                        divisa = DivisasDisponiveis.Find(Function(divisaDisponivel) divisaDisponivel.Identificador = identificadorDivisaEfectivo).GetDivisa().Clonar()
                                        If divisa IsNot Nothing Then
                                            divisa.Denominaciones = New ObservableCollection(Of Clases.Denominacion)()
                                            divisa.MediosPago = New ObservableCollection(Of Clases.MedioPago)()
                                            _DivisasRetorno.Add(divisa)
                                        End If
                                    End If
                                End If

                                If identificadorDenominacionEfectivo <> totalEfectivo.IdentificadorDenominacion Then
                                    identificadorDenominacionEfectivo = totalEfectivo.IdentificadorDenominacion
                                    denominacion = divisa.Denominaciones.Find(Function(denominacionRetorno) denominacionRetorno.Identificador = identificadorDenominacionEfectivo)
                                    If denominacion Is Nothing Then
                                        denominacion = DivisasDisponiveis.Find(Function(divisaDisponivel) divisaDisponivel.Identificador = identificadorDivisaEfectivo).Denominaciones.Find(Function(denominacionDisponivel) denominacionDisponivel.Identificador = identificadorDenominacionEfectivo).Clonar()
                                        If denominacion IsNot Nothing Then
                                            denominacion.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)()
                                            divisa.Denominaciones.Add(denominacion)
                                        End If
                                    End If
                                End If
                                If denominacion IsNot Nothing Then
                                    denominacion.ValorDenominacion.Add(New Clases.ValorDenominacion() With {.UnidadMedida = If(String.IsNullOrEmpty(totalEfectivo.IdentificadorUnidadMedida), Nothing, UnidadesMedidaDisponiveis.Find(Function(umd) umd.Identificador = totalEfectivoLocal.IdentificadorUnidadMedida)),
                                                                                                            .Cantidad = totalEfectivo.Cantidad,
                                                                                                            .Importe = totalEfectivo.Valor,
                                                                                                            .Calidad = If(String.IsNullOrEmpty(totalEfectivo.IdentificadorCalidad), Nothing, CalidadesDisponiveis.Find(Function(cd) cd.Identificador = totalEfectivoLocal.IdentificadorCalidad)),
                                                                                                            .TipoValor = TipoValor,
                                                                                                            .InformadoPor = Enumeradores.TipoContado.NoDefinido})
                                End If
                            Next

                        End If

                        If totalesMedioPago IsNot Nothing Then

                            'Realiza o distinct nos totales medio pago somando as linhas duplicadas
                            Dim totalesMedioPagoDistintos = (From
                                                                totalMedioPago
                                                            In
                                                            (From itemTotalMedioPago
                                                            In
                                                                totalesMedioPago
                                                             Where itemTotalMedioPago("Divisa") IsNot Nothing AndAlso itemTotalMedioPago("MedioPago") IsNot Nothing
                                                             Select New With {
                                                                .IdentificadorDivisa = itemTotalMedioPago("Divisa").Value(Of String)("Identificador"),
                                                                .IdentificadorMedioPago = If(itemTotalMedioPago("MedioPago") Is Nothing, String.Empty, itemTotalMedioPago("MedioPago").Value(Of String)("Identificador")),
                                                                .IdentificadorUnidadMedida = If(itemTotalMedioPago("UnidadMedida") Is Nothing, String.Empty, itemTotalMedioPago("UnidadMedida").Value(Of String)("Identificador")),
                                                                .Cantidad = ParseInteiro(itemTotalMedioPago.Value(Of String)("Cantidad")),
                                                                .Valor = ParseDecimal(itemTotalMedioPago.Value(Of String)("Valor")),
                                                                .Terminos = itemTotalMedioPago("Terminos")})
                                                            Group totalMedioPago By
                            totalMedioPago.IdentificadorDivisa,
                            totalMedioPago.IdentificadorMedioPago,
                            totalMedioPago.IdentificadorUnidadMedida
                            Into Group
                                                            Select New With {
                                                                .IdentificadorDivisa = IdentificadorDivisa,
                                                                .IdentificadorMedioPago = IdentificadorMedioPago,
                                                                .IdentificadorUnidadMedida = IdentificadorUnidadMedida,
                                                                .Cantidad = Group.Sum(Function(te) te.Cantidad),
                                                                .Valor = Group.Sum(Function(te) te.Valor),
                                                                .Terminos = Group.Aggregate(Function(resultado, item)
                                                                                                DirectCast(item.Terminos, Linq.JArray).Values(Of Linq.JToken).ToList().ForEach(Sub(i) DirectCast(resultado.Terminos, Linq.JArray).Add(i))
                                                                                                Return resultado
                                                                                            End Function).Terminos})


                            Dim identificadorDivisaMedioPago As String = String.Empty
                            Dim identificadorMedioPagoDistinto As String = String.Empty
                            For Each totalMedioPago In totalesMedioPagoDistintos
                                Dim totalMedioPagoLocal = totalMedioPago

                                If identificadorDivisaMedioPago <> totalMedioPago.IdentificadorDivisa Then
                                    identificadorDivisaMedioPago = totalMedioPago.IdentificadorDivisa
                                    identificadorMedioPagoDistinto = String.Empty
                                    divisa = _DivisasRetorno.Find(Function(divisaRetorno) divisaRetorno.Identificador = identificadorDivisaMedioPago)
                                    If divisa Is Nothing Then
                                        divisa = DivisasDisponiveis.Find(Function(divisaDisponivel) divisaDisponivel.Identificador = identificadorDivisaMedioPago).GetDivisa().Clonar()
                                        If divisa IsNot Nothing Then
                                            divisa.Denominaciones = New ObservableCollection(Of Clases.Denominacion)()
                                            divisa.MediosPago = New ObservableCollection(Of Clases.MedioPago)()
                                            _DivisasRetorno.Add(divisa)
                                        End If
                                    End If
                                End If

                                If identificadorMedioPagoDistinto <> totalMedioPago.IdentificadorMedioPago Then
                                    identificadorMedioPagoDistinto = totalMedioPago.IdentificadorMedioPago
                                    medioPago = divisa.MediosPago.Find(Function(medioPagoRetorno) medioPagoRetorno.Identificador = identificadorMedioPagoDistinto)
                                    If medioPago Is Nothing Then
                                        medioPago = DivisasDisponiveis.Find(Function(divisaDisponivel) divisaDisponivel.Identificador = identificadorDivisaMedioPago).MediosPago.Find(Function(medioPagoDisponivel) medioPagoDisponivel.Identificador = identificadorMedioPagoDistinto).Clonar()
                                        If medioPago IsNot Nothing Then
                                            medioPago.Valores = New ObservableCollection(Of Clases.ValorMedioPago)()
                                            divisa.MediosPago.Add(medioPago)
                                        End If
                                    End If
                                End If

                                If totalMedioPago.Terminos IsNot Nothing AndAlso totalMedioPago.Terminos.Count > 0 Then

                                    Dim TerminosConValor As New ObservableCollection(Of Clases.Termino)
                                    Dim linha As Integer = 0
                                    For Each linhaTermino In totalMedioPago.Terminos

                                        Dim terminos As List(Of Clases.Termino) = linhaTermino.ToList().ConvertAll(Of Clases.Termino)(Function(t)
                                                                                                                                          Dim termino = DivisasDisponiveis.Find(Function(divisaDisponivel) divisaDisponivel.Identificador = identificadorDivisaMedioPago).MediosPago.Find(Function(medioPagoDisponivel) medioPagoDisponivel.Identificador = identificadorMedioPagoDistinto).Terminos.Find(Function(terminoDisponivel) terminoDisponivel.Identificador = t.Value(Of String)("Identificador")).Clonar()
                                                                                                                                          termino.Valor = t.Value(Of String)("Valor")
                                                                                                                                          termino.NecIndiceGrupo = linha
                                                                                                                                          Return termino
                                                                                                                                      End Function)

                                        Dim ExisteValor As Boolean = False
                                        For Each termino As Clases.Termino In If(terminos IsNot Nothing, terminos, 0)

                                            If Not String.IsNullOrEmpty(termino.Valor) Then
                                                ExisteValor = True
                                                Exit For
                                            End If

                                        Next termino

                                        If ExisteValor Then
                                            TerminosConValor.AddRange(terminos)
                                            linha = linha + 1
                                        End If

                                    Next linhaTermino

                                    medioPago.Valores.Add(New Clases.ValorMedioPago() With {
                                                              .UnidadMedida = If(String.IsNullOrEmpty(totalMedioPago.IdentificadorUnidadMedida), Nothing, UnidadesMedidaDisponiveis.Find(Function(unidadMedidaDisponivel) unidadMedidaDisponivel.Identificador = totalMedioPagoLocal.IdentificadorUnidadMedida)),
                                                              .TipoValor = TipoValor,
                                                              .InformadoPor = Enumeradores.TipoContado.NoDefinido,
                                                              .Cantidad = totalMedioPago.Cantidad,
                                                              .Importe = totalMedioPago.Valor,
                                                              .Terminos = TerminosConValor
                                                          })

                                ElseIf Not String.IsNullOrEmpty(totalMedioPago.IdentificadorMedioPago) Then

                                    medioPago.Valores.Add(New Clases.ValorMedioPago() With {
                                                              .UnidadMedida = If(String.IsNullOrEmpty(totalMedioPago.IdentificadorUnidadMedida), Nothing, UnidadesMedidaDisponiveis.Find(Function(unidadMedidaDisponivel) unidadMedidaDisponivel.Identificador = totalMedioPagoLocal.IdentificadorUnidadMedida)),
                                                              .TipoValor = TipoValor,
                                                              .InformadoPor = Enumeradores.TipoContado.NoDefinido,
                                                              .Cantidad = totalMedioPago.Cantidad,
                                                              .Importe = totalMedioPago.Valor,
                                                              .Terminos = Nothing})

                                End If

                            Next totalMedioPago

                        End If

                    Catch ex As Genesis.Excepcion.NegocioExcepcion
                        Throw

                    Catch ex As Exception
                        Throw

                    End Try
                End If
                'BorrarTerminosSinValores(_DivisasRetorno)
                retorno = _DivisasRetorno
            Else
                If _Divisas Is Nothing Then
                    _Divisas = New ObservableCollection(Of Clases.Divisa)()

                End If
                retorno = _Divisas

            End If

            Return retorno

        End Get
        Set(value As ObservableCollection(Of Clases.Divisa))
            _Divisas = value
        End Set

    End Property

    Private _Modo As Enumeradores.Modo
    Public Property Modo As Enumeradores.Modo
        Get
            Return _Modo
        End Get
        Set(value As Enumeradores.Modo)
            If Not {Enumeradores.Modo.Alta, Enumeradores.Modo.Modificacion, Enumeradores.Modo.Consulta}.Contains(value) Then
                Throw New ArgumentException()
            End If
            _Modo = value
        End Set
    End Property

    Dim _DivisasDisponiveis As ObservableCollection(Of DivisaJS) = Nothing
    Private ReadOnly Property DivisasDisponiveis As ObservableCollection(Of DivisaJS)
        Get
            If _DivisasDisponiveis Is Nothing Then

                If Me.Modo = Enumeradores.Modo.Consulta Then
                    _DivisasDisponiveis = (From divisa In Genesis.LogicaNegocio.Genesis.Divisas.ObtenerDivisas(Nothing, Nothing, False, Nothing, True, True, False, False, True).OrderBy(Function(d) d.Descripcion) Select New DivisaJS(divisa)).ToObservableCollection()
                Else
                    _DivisasDisponiveis = (From divisa In Genesis.LogicaNegocio.Genesis.Divisas.ObtenerDivisas(Nothing, Nothing, False, True, True, True, True, True, True).OrderBy(Function(d) d.Descripcion) Select New DivisaJS(divisa)).ToObservableCollection()
                End If

            End If
            Return _DivisasDisponiveis
        End Get
    End Property

    Dim _UnidadesMedidaDisponiveis As ObservableCollection(Of Clases.UnidadMedida) = Nothing
    Private ReadOnly Property UnidadesMedidaDisponiveis As ObservableCollection(Of Clases.UnidadMedida)
        Get
            If _UnidadesMedidaDisponiveis Is Nothing Then
                _UnidadesMedidaDisponiveis = Genesis.LogicaNegocio.Genesis.UnidadMedida.ObtenerUnidadesMedida()
            End If
            Return _UnidadesMedidaDisponiveis
        End Get
    End Property

    Dim _CalidadesDisponiveis As ObservableCollection(Of Clases.Calidad) = Nothing
    Private ReadOnly Property CalidadesDisponiveis As ObservableCollection(Of Clases.Calidad)
        Get
            If _CalidadesDisponiveis Is Nothing Then
                If Modo = Enumeradores.Modo.Consulta Then
                    _CalidadesDisponiveis = New ObservableCollection(Of Clases.Calidad)
                    _CalidadesDisponiveis.Add(New Clases.Calidad With {.Descripcion = Tradutor.Traduzir("055_calidadNoDefinida"), .Identificador = "CAL_NO_DEFINIDA", .Codigo = "CAL_NO_DEFINIDA"})
                    _CalidadesDisponiveis.AddRange(Genesis.LogicaNegocio.Genesis.Calidad.ObtenerCalidades().Where(Function(c) c.Codigo <> "CAL_NO_DEFINIDA").ToObservableCollection())
                Else
                    _CalidadesDisponiveis = Genesis.LogicaNegocio.Genesis.Calidad.ObtenerCalidades().Where(Function(c) c.Codigo <> "CAL_NO_DEFINIDA").ToObservableCollection()
                End If
            End If
            Return _CalidadesDisponiveis
        End Get
    End Property

    Public Property TipoValor As Enumeradores.TipoValor
    Public Property TrabajarConUnidadMedida As Boolean
    Public Property TrabajarConCalidad As Boolean

#End Region

#Region "Eventos"
    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender

        Dim parametros As New ParametrosUCValoresDivisas()
        parametros.Modo = Modo
        parametros.DivisasDisponiveis = DivisasDisponiveis
        parametros.UnidadesMedidaDisponiveis = UnidadesMedidaDisponiveis
        parametros.CalidadesDisponiveis = CalidadesDisponiveis
        parametros.ExhibirTotalesDivisa = ExhibirTotalesDivisa
        parametros.ExhibirTotalesEfectivo = ExhibirTotalesEfectivo
        parametros.ExhibirTotalesMedioPago = ExhibirTotalesMedioPago

        parametros.TiposMedioPagoDisponiveis = New List(Of String)([Enum].GetNames(GetType(Enumeradores.TipoMedioPago)))

        parametros.TotalesPorDivisa = New List(Of TotalPorDivisa)()
        Divisas.ToList().ForEach(Sub(divisa)
                                     Dim tdiv = New TotalPorDivisa(divisa)
                                     If tdiv.TotalCheque <> 0 OrElse tdiv.TotalEfectivo <> 0 OrElse tdiv.TotalGeneral <> 0 OrElse tdiv.TotalOtroValor <> 0 OrElse tdiv.TotalTarjeta <> 0 OrElse tdiv.TotalTicket <> 0 Then
                                         parametros.TotalesPorDivisa.Add(tdiv)
                                     End If
                                 End Sub)

        parametros.TotalesEfectivo = New List(Of TotalEfectivo)()
        For Each divisa In Divisas
            If divisa.Denominaciones IsNot Nothing Then
                For Each denominacion In divisa.Denominaciones
                    If denominacion.ValorDenominacion IsNot Nothing Then
                        For Each valor In denominacion.ValorDenominacion
                            parametros.TotalesEfectivo.Add(New TotalEfectivo(divisa, denominacion, valor))
                        Next
                    End If
                Next
            End If
        Next

        parametros.TotalesMedioPago = New List(Of TotalMedioPago)()
        For Each divisa In Divisas
            If divisa.MediosPago IsNot Nothing Then
                For Each medioPago In divisa.MediosPago.OrderBy(Function(mp) mp.Tipo.ToString() & "_" & mp.Descripcion)
                    Dim medioPagoLocal = medioPago
                    If medioPago.Valores IsNot Nothing Then
                        Dim unidadesMedida = (From valor In medioPago.Valores Select valor.UnidadMedida).Distinct()
                        For Each unidadeMedida In unidadesMedida
                            Dim unidadeMedidaLocal = unidadeMedida
                            Dim terminos As New ObservableCollection(Of ObservableCollection(Of Clases.Termino))()
                            Dim cantidad As Decimal = 0
                            Dim importe As Decimal = 0
                            If unidadeMedida Is Nothing Then
                                medioPago.Valores.FindAll(Function(valor) valor.UnidadMedida Is Nothing).ToList().ForEach(Sub(valor)
                                                                                                                              cantidad += valor.Cantidad
                                                                                                                              importe += valor.Importe
                                                                                                                              If valor.Terminos IsNot Nothing AndAlso valor.Terminos.Count > 0 Then
                                                                                                                                  Dim quantidadeLinha As Integer = valor.Terminos.Max(Function(t) t.NecIndiceGrupo)
                                                                                                                                  For indice As Integer = 0 To quantidadeLinha
                                                                                                                                      Dim indiceTermino As Integer = indice
                                                                                                                                      Dim listaTerminos = valor.Terminos.FindAll(Function(t) t.NecIndiceGrupo = indiceTermino).Clonar()
                                                                                                                                      For Each terminoMP In medioPagoLocal.Terminos
                                                                                                                                          Dim terminoMPLocal = terminoMP
                                                                                                                                          If listaTerminos.Find(Function(termino) termino.Identificador = terminoMPLocal.Identificador) Is Nothing Then
                                                                                                                                              listaTerminos.Add(terminoMP.Clonar())
                                                                                                                                          End If
                                                                                                                                      Next
                                                                                                                                      terminos.Add(listaTerminos)
                                                                                                                                  Next
                                                                                                                              End If
                                                                                                                          End Sub)
                            Else
                                medioPago.Valores.FindAll(Function(valor) valor.UnidadMedida.Identificador = unidadeMedidaLocal.Identificador).ToList().ForEach(Sub(valor)
                                                                                                                                                                    cantidad += valor.Cantidad
                                                                                                                                                                    importe += valor.Importe
                                                                                                                                                                    If valor.Terminos IsNot Nothing AndAlso valor.Terminos.Count > 0 Then

                                                                                                                                                                        Dim quantidadeLinha As Integer = valor.Terminos.Max(Function(t) t.NecIndiceGrupo)
                                                                                                                                                                        For indice As Integer = 0 To quantidadeLinha
                                                                                                                                                                            Dim indiceTermino As Integer = indice
                                                                                                                                                                            Dim listaTerminos = valor.Terminos.FindAll(Function(t) t.NecIndiceGrupo = indiceTermino).Clonar()
                                                                                                                                                                            For Each terminoMP In medioPagoLocal.Terminos
                                                                                                                                                                                Dim terminoMPLocal = terminoMP
                                                                                                                                                                                If listaTerminos.Find(Function(termino) termino.Identificador = terminoMPLocal.Identificador) Is Nothing Then
                                                                                                                                                                                    listaTerminos.Add(terminoMP.Clonar())
                                                                                                                                                                                End If
                                                                                                                                                                            Next
                                                                                                                                                                            terminos.Add(listaTerminos)
                                                                                                                                                                        Next
                                                                                                                                                                    End If
                                                                                                                                                                End Sub)
                            End If
                            parametros.TotalesMedioPago.Add(New TotalMedioPago(divisa, medioPago, unidadeMedida, cantidad, importe, terminos))
                        Next
                    Else
                        parametros.TotalesMedioPago.Add(New TotalMedioPago(divisa, medioPago, Nothing, 0, 0, Nothing))
                    End If
                Next
            End If
        Next

        parametros.Dicionario = New Dictionary(Of String, String)() From {
            {"divisa", Tradutor.Traduzir("055_divisa")},
            {"totalGeneral", Tradutor.Traduzir("055_totalGeneral")},
            {"totalEfectivo", Tradutor.Traduzir("055_totalEfectivo")},
            {"totalOtroValor", Tradutor.Traduzir("055_totalOtroValor")},
            {"totalCheque", Tradutor.Traduzir("055_totalCheque")},
            {"totalTarjeta", Tradutor.Traduzir("055_totalTarjeta")},
            {"totalTicket", Tradutor.Traduzir("055_totalTicket")},
            {"remover", Tradutor.Traduzir("055_remover")},
            {"selecione", Tradutor.Traduzir("055_selecione")},
            {"calidadNoDefinida", "CAL_NO_DEFINIDA"},
            {"preenchaAlgumTotal", Tradutor.Traduzir("055_preenchaAlgumTotal")},
            {"valorNumerico", Tradutor.Traduzir("055_valorNumerico")},
            {"totalesPorDivisa", Tradutor.Traduzir("055_totalesPorDivisa")},
            {"totalesEfectivo", Tradutor.Traduzir("055_totalesEfectivo")},
            {"totalesEfectivoGrilla", Tradutor.Traduzir("055_totalesEfectivoGrilla")},
            {"tituloComponente", Tradutor.Traduzir("055_tituloComponente")},
            {"totalesMedioPago", Tradutor.Traduzir("055_totalesMedioPago")},
            {"totalesMedioPagoGrilla", Tradutor.Traduzir("055_totalesMedioPagoGrilla")},
            {"denominacion", Tradutor.Traduzir("055_denominacion")},
            {"billeteMoneda", Tradutor.Traduzir("055_billeteMoneda")},
            {"unidadMedida", Tradutor.Traduzir("055_unidadMedida")},
            {"cantidad", Tradutor.Traduzir("055_cantidad")},
            {"valor", Tradutor.Traduzir("055_valor")},
            {"calidad", Tradutor.Traduzir("055_calidad")},
            {"billete", Tradutor.Traduzir("055_billete")},
            {"moneda", Tradutor.Traduzir("055_moneda")},
            {"tipoMedioPago", Tradutor.Traduzir("055_tipoMedioPago")},
            {"medioPago", Tradutor.Traduzir("055_medioPago")},
            {"informacionesComplementares", Tradutor.Traduzir("055_informacionesComplementares")},
            {"terminosMedioPago", Tradutor.Traduzir("055_terminosMedioPago")},
            {"mascaratermino", Tradutor.Traduzir("055_mascaratermino")},
            {"confirmacionExclusionTodos", Tradutor.Traduzir("055_confirmacionExclusionTodos")},
            {"divisionentero", Tradutor.Traduzir("012_divisioninvalida")},
            {"divisioninvalidaEfectivo", Tradutor.Traduzir("012_divisioninvalidaEfectivo")},
            {"tituloMonedasAgregadas", Tradutor.Traduzir("055_tituloMonedasAgregadas")}
            }

        parametros.TrabajarConTotalGeneral = False
        parametros.TrabajarConUnidadMedida = TrabajarConUnidadMedida
        parametros.TrabajarConCalidad = TrabajarConCalidad AndAlso (TipoValor <> Enumeradores.TipoValor.Declarado)
        parametros.SeparadorDecimal = MyBase._DecimalSeparador
        parametros.SeparadorMilhar = MyBase._MilharSeparador

        parametros.TotalesPorDivisa = (From P In parametros.TotalesPorDivisa
                                       Select P
                                       Order By P.Divisa.Descripcion).ToList()

        parametros.TotalesEfectivo = (From P In parametros.TotalesEfectivo
                                      Select P
                                      Order By P.Divisa.Descripcion, P.Denominacion.Descripcion,
                                      P.Denominacion.EsBillete Descending, P.UnidadMedida.Descripcion,
                                      If(P.Calidad Is Nothing, String.Empty, P.Calidad.Descripcion)).ToList()

        parametros.TotalesMedioPago = (From P In parametros.TotalesMedioPago
                                      Select P
                                      Order By P.Divisa.Descripcion, P.MedioPago.Tipo,
                                      P.MedioPago.Descripcion, If(P.UnidadMedida Is Nothing, String.Empty, P.UnidadMedida.Descripcion)).ToList()


        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UCValoresDivisas_Inicializar_" & Me.ClientID, "InicializaUCValoresDivisas(" & JsonConvert.SerializeObject(parametros, New Converters.StringEnumConverter()) & ", '" & pnlTotalesPorDivisa.ClientID & "', '" & pnlTotalesEfectivo.ClientID & "', '" & pnlTotalesMedioPago.ClientID & "');", True)

    End Sub

#End Region

#Region "Metodos"

    Private Function ParseDecimal(valor As String) As Decimal
        Dim retorno As Decimal
        If String.IsNullOrEmpty(valor) Then
            retorno = 0
        Else
            retorno = Decimal.Parse(valor)
        End If
        Return retorno
    End Function

    Private Function ParseInteiro(valor As String) As Integer
        Dim retorno As Decimal
        If String.IsNullOrEmpty(valor) Then
            retorno = 0
        Else
            retorno = Integer.Parse(valor)
        End If
        Return retorno
    End Function

    Public Sub ValidarDivisas()
        Try
            Dim Mesajes As StringBuilder = ValidarDivisas(Divisas)
            If Mesajes.Length > 0 Then
                Throw New Genesis.Excepcion.NegocioExcepcion(Mesajes.ToString)

            End If

        Catch ex As Genesis.Excepcion.NegocioExcepcion
            Throw

        Catch ex As Exception
            Throw

        End Try

    End Sub

    Private Function ValidarDivisas(Divisas As ObservableCollection(Of Clases.Divisa)) As StringBuilder
        Dim Mesajes As New StringBuilder
        If Divisas IsNot Nothing Then

            For Each divisa In Divisas

                If divisa.Denominaciones IsNot Nothing AndAlso divisa.Denominaciones.Count > 0 Then

                    For Each denominacion In divisa.Denominaciones

                        If denominacion.ValorDenominacion IsNot Nothing AndAlso denominacion.ValorDenominacion.Count > 0 Then

                            For Each valordenominacion In denominacion.ValorDenominacion

                                If (TrabajarConUnidadMedida AndAlso valordenominacion.UnidadMedida Is Nothing) Then
                                    Mesajes.AppendLine(String.Format(Tradutor.Traduzir("055_unidadmedidaobligatorio"), denominacion.Descripcion, divisa.Descripcion, ""))
                                    Continue For

                                End If

                                If valordenominacion.Cantidad > 0 AndAlso (IsDBNull(valordenominacion.Importe) OrElse CDbl(valordenominacion.Importe = 0.0)) Then
                                    Mesajes.AppendLine(String.Format(Tradutor.Traduzir("055_importeobligatorio"), denominacion.Descripcion, divisa.Descripcion))

                                ElseIf valordenominacion.Cantidad <= 0 AndAlso valordenominacion.Importe > 0 Then
                                    Mesajes.AppendLine(String.Format(Tradutor.Traduzir("055_cantidadobligatorio"), denominacion.Descripcion, divisa.Descripcion))

                                End If

                            Next valordenominacion

                        End If

                    Next denominacion

                End If

                If divisa.MediosPago IsNot Nothing AndAlso divisa.MediosPago.Count > 0 Then

                    For Each mediopago In divisa.MediosPago

                        If mediopago.Valores IsNot Nothing AndAlso mediopago.Valores.Count > 0 Then

                            For Each valoresmediospago In mediopago.Valores

                                If valoresmediospago.Cantidad > 0 AndAlso (IsDBNull(valoresmediospago.Importe) OrElse valoresmediospago.Importe = 0.0) Then
                                    Mesajes.AppendLine(String.Format(Tradutor.Traduzir("055_importemediopagoobligatorio"), mediopago.Descripcion, divisa.Descripcion))

                                ElseIf (valoresmediospago.Cantidad = 0 OrElse IsDBNull(valoresmediospago.Cantidad)) AndAlso valoresmediospago.Importe > 0 Then
                                    Mesajes.AppendLine(String.Format(Tradutor.Traduzir("055_cantidadmediopagoobligatorio"), mediopago.Descripcion, divisa.Descripcion))

                                End If

                                If valoresmediospago.Terminos IsNot Nothing AndAlso valoresmediospago.Terminos.Count > 0 Then

                                    For Each termino In valoresmediospago.Terminos

                                        If Not String.IsNullOrEmpty(termino.Valor) AndAlso termino.Mascara IsNot Nothing AndAlso Not String.IsNullOrEmpty(termino.Mascara.ExpresionRegular) Then

                                            If Not Regex.IsMatch(termino.Valor, termino.Mascara.ExpresionRegular) Then
                                                Mesajes.AppendLine(String.Format(Tradutor.Traduzir("055_mascaraterminoinvalida"), termino.Valor, termino.Descripcion, mediopago.Descripcion, divisa.Descripcion, termino.Mascara.ExpresionRegular))

                                            End If

                                        End If

                                    Next termino

                                End If

                            Next valoresmediospago

                        End If

                    Next mediopago

                End If

            Next divisa

        End If

        Return Mesajes

    End Function

#End Region

#Region "Overrides"

    Protected Overrides Sub TraduzirControles()
        lblTituloComponente.Text = Tradutor.Traduzir("055_lblTituloComponente_" + TipoValor.ToString())
    End Sub

    Protected Overrides Sub Inicializar()
        MyBase.Inicializar()
        lgsubtitulobar.Visible = ExhibirTitulo
        dvsubtitulobar.Visible = ExhibirTitulo
        If Not ExhibirTitulo Then
            dvDivisas.Style.Item("margin-left") = "0px !important"
        End If
    End Sub

#End Region

#Region "Clases"

    Public Class ParametrosUCValoresDivisas

#Region "Total por Divisa"
        Public Class TotalPorDivisa
            Public Property Divisa As Clases.Divisa
            Public Property TotalGeneral As Decimal
            Public Property TotalEfectivo As Decimal
            Public Property TotalOtroValor As Decimal
            Public Property TotalCheque As Decimal
            Public Property TotalTarjeta As Decimal
            Public Property TotalTicket As Decimal

            Public Sub New()

            End Sub

            Public Sub New(divisa As Clases.Divisa)
                Me.Divisa = divisa
                Me.TotalGeneral = SomarTotalDivisa(divisa)
                Me.TotalEfectivo = SomarTotalEfectivo(divisa)
                Me.TotalOtroValor = SomarTotalTipoMedioPago(divisa, Enumeradores.TipoMedioPago.OtroValor)
                Me.TotalCheque = SomarTotalTipoMedioPago(divisa, Enumeradores.TipoMedioPago.Cheque)
                Me.TotalTarjeta = SomarTotalTipoMedioPago(divisa, Enumeradores.TipoMedioPago.Tarjeta)
                Me.TotalTicket = SomarTotalTipoMedioPago(divisa, Enumeradores.TipoMedioPago.Ticket)
            End Sub

            Private Function SomarTotalDivisa(divisa As Clases.Divisa) As Decimal
                Dim total As Decimal
                If divisa.ValoresTotalesDivisa IsNot Nothing Then
                    For i = 0 To divisa.ValoresTotalesDivisa.Count - 1
                        total += divisa.ValoresTotalesDivisa(i).Importe
                    Next
                End If
                Return total
            End Function

            Private Function SomarTotalEfectivo(divisa As Clases.Divisa) As Decimal
                Dim total As Decimal
                If divisa.ValoresTotalesEfectivo IsNot Nothing Then
                    For i = 0 To divisa.ValoresTotalesEfectivo.Count - 1
                        If divisa.ValoresTotalesEfectivo(i).TipoDetalleEfectivo = Enumeradores.TipoDetalleEfectivo.Mezcla Then
                            total += divisa.ValoresTotalesEfectivo(i).Importe
                        End If
                    Next
                End If
                Return total
            End Function

            Private Function SomarTotalTipoMedioPago(divisa As Clases.Divisa, tipoMedioPago As Enumeradores.TipoMedioPago?) As Decimal
                Dim total As Decimal
                If divisa.ValoresTotalesTipoMedioPago IsNot Nothing Then
                    For i = 0 To divisa.ValoresTotalesTipoMedioPago.Count - 1
                        If Not tipoMedioPago.HasValue OrElse divisa.ValoresTotalesTipoMedioPago(i).TipoMedioPago = tipoMedioPago Then
                            total += divisa.ValoresTotalesTipoMedioPago(i).Importe
                        End If
                    Next
                End If
                Return total
            End Function
        End Class

#End Region

#Region "Total Efectivo"

        Public Class TotalEfectivo
            Public Property Divisa As Clases.Divisa
            Public Property Denominacion As Clases.Denominacion
            Public Property UnidadMedida As Clases.UnidadMedida
            Public Property Cantidad As Decimal
            Public Property Valor As Decimal
            Public Property Calidad As Clases.Calidad

            Public Sub New()

            End Sub

            Public Sub New(divisa As Clases.Divisa, denominacion As Clases.Denominacion, valorDenominacion As Clases.ValorDenominacion)
                Me.Divisa = divisa
                Me.Denominacion = denominacion
                Me.UnidadMedida = valorDenominacion.UnidadMedida
                Me.Cantidad = valorDenominacion.Cantidad
                Me.Valor = valorDenominacion.Importe
                Me.Calidad = valorDenominacion.Calidad

            End Sub

        End Class

#End Region

#Region "Total Medio Pago"

        Public Class TotalMedioPago
            Public Property Remover As Boolean
            Public Property Divisa As Clases.Divisa
            Public Property MedioPago As Clases.MedioPago
            Public Property UnidadMedida As Clases.UnidadMedida
            Public Property Cantidad As Decimal
            Public Property Valor As Decimal
            Public Property Terminos As ObservableCollection(Of ObservableCollection(Of Clases.Termino))
            Public Sub New()

            End Sub

            Public Sub New(divisa As Clases.Divisa, _
                           medioPago As Clases.MedioPago, _
                           unidadMedida As Clases.UnidadMedida, _
                           cantidad As Decimal, _
                           valor As Decimal, _
                           terminos As ObservableCollection(Of ObservableCollection(Of Clases.Termino)))

                Me.Divisa = divisa
                Me.MedioPago = medioPago
                Me.UnidadMedida = unidadMedida
                Me.Cantidad = cantidad
                Me.Valor = valor
                Me.Terminos = terminos
            End Sub

        End Class

#End Region

        Public Class DivisaJS

            Private _divisa As Clases.Divisa

            Public Function GetDivisa() As Clases.Divisa
                Return _divisa
            End Function

            Public Sub New()
                _divisa = New Clases.Divisa()
            End Sub

            Public Sub New(divisa As Clases.Divisa)
                _divisa = divisa
            End Sub

            Public Property Identificador As String
                Get
                    Return _divisa.Identificador
                End Get
                Set(value As String)
                    _divisa.Identificador = value
                End Set
            End Property

            Public Property Descripcion As String
                Get
                    Return _divisa.Descripcion
                End Get
                Set(value As String)
                    _divisa.Descripcion = value
                End Set
            End Property

            Public Property Color As String
                Get
                    Return Drawing.ColorTranslator.ToHtml(_divisa.Color)
                End Get
                Set(value As String)
                    _divisa.Color = Drawing.ColorTranslator.FromHtml(value)
                End Set
            End Property

            Public Property CodigoAcceso As String
                Get
                    Return _divisa.CodigoAcceso
                End Get
                Set(value As String)
                    _divisa.CodigoAcceso = value
                End Set
            End Property

            Public Property CodigoSimbolo As String
                Get
                    Return _divisa.CodigoSimbolo
                End Get
                Set(value As String)
                    _divisa.CodigoSimbolo = value
                End Set
            End Property

            Public Property Denominaciones As ObservableCollection(Of Clases.Denominacion)
                Get
                    Return _divisa.Denominaciones
                End Get
                Set(value As ObservableCollection(Of Clases.Denominacion))
                    _divisa.Denominaciones = value
                End Set
            End Property

            Public Property MediosPago As ObservableCollection(Of Clases.MedioPago)
                Get
                    Return _divisa.MediosPago
                End Get
                Set(value As ObservableCollection(Of Clases.MedioPago))
                    _divisa.MediosPago = value
                End Set
            End Property
        End Class

        Public Property Dicionario As Dictionary(Of String, String)
        Public Property Modo As Enumeradores.Modo
        Public Property TrabajarConTotalGeneral As Boolean
        Public Property TrabajarConUnidadMedida As Boolean
        Public Property TrabajarConCalidad As Boolean
        Public Property ExhibirTotalesDivisa As Boolean
        Public Property ExhibirTotalesEfectivo As Boolean
        Public Property ExhibirTotalesMedioPago As Boolean
        Public Property DivisasDisponiveis As ObservableCollection(Of DivisaJS)
        Public Property UnidadesMedidaDisponiveis As ObservableCollection(Of Clases.UnidadMedida)
        Public Property CalidadesDisponiveis As ObservableCollection(Of Clases.Calidad)
        Public Property TiposMedioPagoDisponiveis As List(Of String)
        Public Property TotalesPorDivisa As List(Of TotalPorDivisa)
        Public Property TotalesEfectivo As List(Of TotalEfectivo)
        Public Property TotalesMedioPago As List(Of TotalMedioPago)
        Public Property SeparadorDecimal As String
        Public Property SeparadorMilhar As String

    End Class

#End Region

End Class