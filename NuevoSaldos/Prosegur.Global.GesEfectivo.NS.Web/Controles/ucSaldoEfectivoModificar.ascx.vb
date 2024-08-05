Imports System.Runtime.Serialization
Imports System.Web.Services
Imports Prosegur.Genesis.Comon
Imports Newtonsoft.Json
Imports Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucSaldoEfectivoModificar.ParametrosucSaldoEfectivoModificar
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Framework.Dicionario
Imports System.Collections.ObjectModel

Public Class ucSaldoEfectivoModificar
    Inherits UcBase

#Region "Propriedades"

    Public Property ExhibirTitulo As Boolean = True
    Public Property ExhibirTotalesEfectivo As Boolean = True

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
                        Dim totalesEfectivo As Linq.JArray = JsonConvert.DeserializeObject(hdnTotalesEfectivo.Value, New JsonSerializerSettings() With {.Converters = {New Converters.StringEnumConverter()}})

                        Dim divisa As Clases.Divisa = Nothing
                        Dim denominacion As Clases.Denominacion = Nothing
                        Dim medioPago As Clases.MedioPago = Nothing
                        Dim unidadMedida As Clases.UnidadMedida = Nothing
                        Dim calidad As Clases.Calidad = Nothing

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

    Private _HabilitaCheckBox As Boolean
    Public Property HabilitaCheckBox As Boolean
        Get
            Return _HabilitaCheckBox
        End Get
        Set(value As Boolean)
            _HabilitaCheckBox = value
        End Set
    End Property

    Dim _DivisasDisponiveis As ObservableCollection(Of DivisaJS) = Nothing
    Private ReadOnly Property DivisasDisponiveis As ObservableCollection(Of DivisaJS)
        Get
            If _DivisasDisponiveis Is Nothing Then

                If Me.Modo = Enumeradores.Modo.Consulta Then
                    _DivisasDisponiveis = (From divisa In Genesis.LogicaNegocio.Genesis.Divisas.ObtenerDivisas(Nothing, Nothing, False, Nothing, True, True, False, False, True).OrderBy(Function(d) d.Descripcion) Select New DivisaJS(divisa)).ToObservableCollection()
                    If _Divisas IsNot Nothing AndAlso _Divisas.Count = 1 AndAlso _DivisasDisponiveis IsNot Nothing AndAlso _DivisasDisponiveis.Count > 0 Then
                        _DivisasDisponiveis = _DivisasDisponiveis.RemoveAll(Function(r) r.Identificador <> _Divisas.First.Identificador)
                    End If
                Else
                    _DivisasDisponiveis = (From divisa In Genesis.LogicaNegocio.Genesis.Divisas.ObtenerDivisas(Nothing, Nothing, False, True, True, True, True, True, True).OrderBy(Function(d) d.Descripcion) Select New DivisaJS(divisa)).ToObservableCollection()
                    If _Divisas IsNot Nothing AndAlso _Divisas.Count = 1 AndAlso _DivisasDisponiveis IsNot Nothing AndAlso _DivisasDisponiveis.Count > 0 Then
                        _DivisasDisponiveis = _DivisasDisponiveis.RemoveAll(Function(r) r.Identificador <> _Divisas.First.Identificador)
                    End If
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
    Public Property TrabajarConTotalGeneral As Boolean
    Public Property TrabajarConUnidadMedida As Boolean
    Public Property TrabajarConCalidad As Boolean
    Public Property TrabajarConNivelDetalle As Boolean

#End Region

#Region "Eventos"

    Public Event EventoAgregarSaldoModificado As System.EventHandler

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender

        Dim parametros As New ParametrosucSaldoEfectivoModificar()
        parametros.Modo = Modo
        parametros.HabilitaCheckBox = HabilitaCheckBox
        parametros.DivisasDisponiveis = DivisasDisponiveis
        parametros.UnidadesMedidaDisponiveis = UnidadesMedidaDisponiveis
        parametros.CalidadesDisponiveis = CalidadesDisponiveis
        parametros.ExhibirTotalesEfectivo = ExhibirTotalesEfectivo
        parametros.TrabajarConUnidadMedida = TrabajarConUnidadMedida
        parametros.TrabajarConCalidad = TrabajarConCalidad
        parametros.TrabajarConNivelDetalle = TrabajarConNivelDetalle

        parametros.TotalesEfectivo = New List(Of TotalEfectivo)()

        For Each divisa In Divisas

            If divisa.Denominaciones IsNot Nothing Then

                For Each denominacion In divisa.Denominaciones.OrderByDescending(Function(o) o.Valor).ToObservableCollection
                    If denominacion.ValorDenominacion IsNot Nothing Then
                        For Each valor In denominacion.ValorDenominacion
                            parametros.TotalesEfectivo.Add(New TotalEfectivo(divisa, denominacion, valor, Nothing))
                        Next
                    End If
                Next

            End If

            If divisa.ValoresTotalesEfectivo IsNot Nothing AndAlso divisa.ValoresTotalesEfectivo.Count > 0 Then
                parametros.TotalesEfectivo.Add(New TotalEfectivo(divisa, Nothing, Nothing, divisa.ValoresTotalesEfectivo.FirstOrDefault))
            End If

        Next

        parametros.Dicionario = New Dictionary(Of String, String)() From {
            {"divisa", Tradutor.Traduzir("055_divisa")},
            {"remover", Tradutor.Traduzir("055_remover")},
            {"selecione", Tradutor.Traduzir("055_selecione")},
            {"calidadNoDefinida", "CAL_NO_DEFINIDA"},
            {"preenchaAlgumTotal", Tradutor.Traduzir("055_preenchaAlgumTotal")},
            {"valorNumerico", Tradutor.Traduzir("055_valorNumerico")},
            {"totalesEfectivo", Tradutor.Traduzir("055_totalesEfectivo")},
            {"totalesEfectivoGrilla", Tradutor.Traduzir("055_totalesEfectivoGrilla")},
            {"tituloComponente", Tradutor.Traduzir("055_tituloComponente")},
            {"denominacion", Tradutor.Traduzir("055_denominacion")},
            {"billeteMoneda", Tradutor.Traduzir("055_billeteMoneda")},
            {"unidadMedida", Tradutor.Traduzir("055_unidadMedida")},
            {"cantidad", Tradutor.Traduzir("055_cantidad")},
            {"valor", Tradutor.Traduzir("055_valor")},
            {"calidad", Tradutor.Traduzir("055_calidad")},
            {"billete", Tradutor.Traduzir("055_billete")},
            {"moneda", Tradutor.Traduzir("055_moneda")},
            {"confirmacionExclusionTodos", Tradutor.Traduzir("055_confirmacionExclusionTodos")},
            {"divisionentero", Tradutor.Traduzir("012_divisioninvalida")},
            {"divisioninvalidaEfectivo", Tradutor.Traduzir("012_divisioninvalidaEfectivo")},
            {"tituloMonedasAgregadas", Tradutor.Traduzir("055_tituloMonedasAgregadas")},
            {"nivelDetalle", Tradutor.Traduzir("055_nivel")},
            {"total", Tradutor.Traduzir("055_total")},
            {"detalle", Tradutor.Traduzir("055_detalle")},
            {"importepositivomodificar", Tradutor.Traduzir("055_importepositivomodificar")},
            {"importenegativomodificar", Tradutor.Traduzir("055_importenegativomodificar")}
            }

        parametros.SeparadorDecimal = MyBase._DecimalSeparador
        parametros.SeparadorMilhar = MyBase._MilharSeparador


        parametros.TotalesEfectivo = (From P In parametros.TotalesEfectivo
                                      Select P
                                      Order By P.Divisa.Descripcion).ToList()


        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ucSaldoEfectivo_Inicializar_" & Me.ClientID, "InicializaUcSaldoEfectivoModificar(" & JsonConvert.SerializeObject(parametros, New Converters.StringEnumConverter()) & ", '" & pnlTotalesEfectivo.ClientID & "','" & Me.ClientID & "');", True)

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

            Next divisa

        End If

        Return Mesajes

    End Function

    Public Function ActualizaDivisas() As ObservableCollection(Of Clases.Divisa)

        Return Me.Divisas

    End Function

    Public Function RecuperarDivisa(TotalesEfectivo As Linq.JArray) As Clases.Divisa

        Dim divisa As Clases.Divisa = Nothing
        Dim denominacion As Clases.Denominacion = Nothing
        Dim medioPago As Clases.MedioPago = Nothing
        Dim unidadMedida As Clases.UnidadMedida = Nothing
        Dim calidad As Clases.Calidad = Nothing

        If TotalesEfectivo IsNot Nothing Then

            'Realiza o distinct nos totales efectivo somando as linhas duplicadas
            Dim totalesEfectivoDistintos = (From
                                                totalEfectivo
                                            In
                                            (From
                                                itemTotalEfectivo
                                            In
                                                TotalesEfectivo Where itemTotalEfectivo("Divisa") IsNot Nothing
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
                    divisa = Divisas.Find(Function(divisaRetorno) divisaRetorno.Identificador = identificadorDivisaEfectivo)
                    If divisa Is Nothing Then
                        divisa = DivisasDisponiveis.Find(Function(divisaDisponivel) divisaDisponivel.Identificador = identificadorDivisaEfectivo).GetDivisa().Clonar()
                        If divisa IsNot Nothing Then
                            divisa.Denominaciones = New ObservableCollection(Of Clases.Denominacion)()
                            divisa.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)()
                            divisa.MediosPago = New ObservableCollection(Of Clases.MedioPago)
                            divisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)()
                        End If
                    Else
                        divisa.Denominaciones = New ObservableCollection(Of Clases.Denominacion)()
                        divisa.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)()
                        divisa.MediosPago = New ObservableCollection(Of Clases.MedioPago)
                        divisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)()
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
                    Else
                        denominacion.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)()
                    End If
                End If
                If denominacion IsNot Nothing Then
                    denominacion.ValorDenominacion.Add(New Clases.ValorDenominacion() With {.UnidadMedida = If(String.IsNullOrEmpty(totalEfectivo.IdentificadorUnidadMedida), Nothing, UnidadesMedidaDisponiveis.Find(Function(umd) umd.Identificador = totalEfectivoLocal.IdentificadorUnidadMedida)),
                                                                                            .Cantidad = totalEfectivo.Cantidad,
                                                                                            .Importe = totalEfectivo.Valor,
                                                                                            .Calidad = If(String.IsNullOrEmpty(totalEfectivo.IdentificadorCalidad), Nothing, If(totalEfectivo.IdentificadorCalidad = "CAL_NO_DEFINIDA", Nothing, CalidadesDisponiveis.Find(Function(cd) cd.Identificador = totalEfectivoLocal.IdentificadorCalidad))),
                                                                                            .TipoValor = TipoValor,
                                                                                            .InformadoPor = Enumeradores.TipoContado.NoDefinido})
                Else
                    divisa.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)
                    divisa.ValoresTotalesEfectivo.Add(New Clases.ValorEfectivo With {.Importe = totalEfectivo.Valor,
                                                                                     .TipoDetalleEfectivo = Enumeradores.TipoDetalleEfectivo.Mezcla,
                                                                                     .TipoValor = Enumeradores.TipoValor.NoDefinido})
                End If
            Next

        End If

        Return divisa

    End Function

    Public Function verificarDivisa(identificadorDivisa As String) As Boolean

        Return DivisasDisponiveis IsNot Nothing AndAlso DivisasDisponiveis.Count > 0 AndAlso DivisasDisponiveis.Exists(Function(e) e.Identificador = identificadorDivisa)

    End Function

#End Region

#Region "Overrides"

    Protected Overrides Sub Inicializar()
        MyBase.Inicializar()
        If Not ExhibirTitulo Then
            dvDivisas.Style.Item("margin-left") = "0px !important"
        End If
    End Sub

#End Region

#Region "Clases"

    Public Class ParametrosucSaldoEfectivoModificar

#Region "Total Efectivo"

        Public Class TotalEfectivo
            Public Property Divisa As Clases.Divisa
            Public Property Denominacion As Clases.Denominacion
            Public Property UnidadMedida As Clases.UnidadMedida
            Public Property Cantidad As Decimal
            Public Property Valor As Decimal
            Public Property Calidad As Clases.Calidad
            Public Property NivelDetalle As String
            Public Property Detallar As Boolean
            Public Property ColorFondo As String
                
            Public Sub New()

            End Sub

            Public Sub New(divisa As Clases.Divisa,
                           denominacion As Clases.Denominacion,
                           valorDenominacion As Clases.ValorDenominacion,
                           valorTotal As Clases.ValorEfectivo)
                Me.Divisa = divisa

                If denominacion IsNot Nothing Then
                    Me.Denominacion = denominacion
                    Me.UnidadMedida = valorDenominacion.UnidadMedida
                    Me.Cantidad = valorDenominacion.Cantidad
                    Me.Valor = valorDenominacion.Importe
                    Me.Calidad = valorDenominacion.Calidad
                    Me.NivelDetalle = Tradutor.Traduzir("055_detalle")
                    Me.Detallar = valorDenominacion.Detallar
                    Me.ColorFondo = Drawing.ColorTranslator.ToHtml(valorDenominacion.Color)
                Else
                    Me.Valor = valorTotal.Importe
                    Me.NivelDetalle = Tradutor.Traduzir("055_total")
                    Me.Detallar = valorTotal.Detallar
                    Me.ColorFondo = Drawing.ColorTranslator.ToHtml(valorTotal.Color)
                End If

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
        Public Property HabilitaCheckBox As Boolean
        Public Property Modo As Enumeradores.Modo
        Public Property TrabajarConUnidadMedida As Boolean
        Public Property TrabajarConCalidad As Boolean
        Public Property TrabajarConNivelDetalle As Boolean
        Public Property ExhibirTotalesEfectivo As Boolean
        Public Property DivisasDisponiveis As ObservableCollection(Of DivisaJS)
        Public Property EsEfectivoDetallar As Boolean
        Public Property UnidadesMedidaDisponiveis As ObservableCollection(Of Clases.UnidadMedida)
        Public Property CalidadesDisponiveis As ObservableCollection(Of Clases.Calidad)
        Public Property TotalesEfectivo As List(Of TotalEfectivo)
        Public Property SeparadorDecimal As String
        Public Property SeparadorMilhar As String

    End Class

#End Region

End Class