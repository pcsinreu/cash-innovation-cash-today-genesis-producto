Imports System.Runtime.Serialization
Imports System.Web.Services
Imports Prosegur.Genesis.Comon
Imports Newtonsoft.Json
Imports Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucSaldoMedioPagoModificar.ParametrosUcSaldoMedioPagoModificar
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Framework.Dicionario
Imports System.Collections.ObjectModel

Public Class ucSaldoMedioPagoModificar
    Inherits UcBase

#Region "Propriedades"

    Public Property ExhibirTitulo As Boolean = True
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

                        Dim totalesMedioPago As Linq.JArray = JsonConvert.DeserializeObject(hdnTotalesMedioPago.Value, New JsonSerializerSettings() With {.Converters = {New Converters.StringEnumConverter()}})

                        Dim divisa As Clases.Divisa = Nothing
                        Dim medioPago As Clases.MedioPago = Nothing

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
                                                              .UnidadMedida = Nothing,
                                                              .TipoValor = TipoValor,
                                                              .InformadoPor = Enumeradores.TipoContado.NoDefinido,
                                                              .Cantidad = totalMedioPago.Cantidad,
                                                              .Importe = totalMedioPago.Valor,
                                                              .Terminos = TerminosConValor
                                                          })

                                ElseIf Not String.IsNullOrEmpty(totalMedioPago.IdentificadorMedioPago) Then

                                    medioPago.Valores.Add(New Clases.ValorMedioPago() With {
                                                              .UnidadMedida = Nothing,
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
                    If _Divisas IsNot Nothing AndAlso _Divisas.Count > 0 AndAlso _DivisasDisponiveis IsNot Nothing AndAlso _DivisasDisponiveis.Count > 0 Then
                        _DivisasDisponiveis = _DivisasDisponiveis.RemoveAll(Function(r) Not _Divisas.Exists(Function(e) e.Identificador = r.Identificador))
                    End If
                Else
                    _DivisasDisponiveis = (From divisa In Genesis.LogicaNegocio.Genesis.Divisas.ObtenerDivisas(Nothing, Nothing, False, True, True, True, True, True, True).OrderBy(Function(d) d.Descripcion) Select New DivisaJS(divisa)).ToObservableCollection()
                    If _Divisas IsNot Nothing AndAlso _Divisas.Count = 1 AndAlso _DivisasDisponiveis IsNot Nothing AndAlso _DivisasDisponiveis.Count > 0 Then
                        _DivisasDisponiveis = _DivisasDisponiveis.RemoveAll(Function(r) Not _Divisas.Exists(Function(e) e.Identificador = r.Identificador))
                    End If
                End If

            End If
            Return _DivisasDisponiveis
        End Get
    End Property

    Private ReadOnly Property TipoMedioPagoDisponivel As String
        Get
            Return TipoMedioPago
        End Get
    End Property

    Public Property TipoValor As Enumeradores.TipoValor
    Public Property TipoMedioPago As String

#End Region

#Region "Eventos"
    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender

        Dim parametros As New ParametrosUcSaldoMedioPagoModificar()
        parametros.Modo = Modo
        parametros.HabilitaCheckBox = HabilitaCheckBox
        parametros.DivisasDisponiveis = DivisasDisponiveis
        parametros.TipoMedioPagoDisponivel = TipoMedioPagoDisponivel

        parametros.TiposMedioPagoDisponiveis = New List(Of String)([Enum].GetNames(GetType(Enumeradores.TipoMedioPago)))

        parametros.TotalesMedioPago = New List(Of TotalMedioPago)()
        For Each divisa In Divisas
            If divisa.MediosPago IsNot Nothing Then
                For Each medioPago In divisa.MediosPago.OrderBy(Function(mp) mp.Tipo.ToString() & "_" & mp.Descripcion)
                    Dim medioPagoLocal = medioPago
                    If medioPago.Valores IsNot Nothing Then
                        Dim unidadesMedida = (From valor In medioPago.Valores Select valor.UnidadMedida).Distinct()
                        For Each unidadeMedida In unidadesMedida
                            Dim unidadeMedidaLocal = unidadeMedida
                            Dim cantidad As Decimal = 0
                            Dim importe As Decimal = 0
                            Dim detallar As Boolean
                            Dim color As Drawing.Color
                            If unidadeMedida Is Nothing Then
                                medioPago.Valores.FindAll(Function(valor) valor.UnidadMedida Is Nothing).ToList().ForEach(Sub(valor)
                                                                                                                              cantidad += valor.Cantidad
                                                                                                                              importe += valor.Importe
                                                                                                                              color = valor.Color
                                                                                                                              detallar = valor.Detallar
                                                                                                                      
                                                                                                                          End Sub)
                            Else
                                medioPago.Valores.FindAll(Function(valor) valor.UnidadMedida.Identificador = unidadeMedidaLocal.Identificador).ToList().ForEach(Sub(valor)
                                                                                                                                                                    cantidad += valor.Cantidad
                                                                                                                                                                    importe += valor.Importe
                                                                                                                                                                    color = valor.Color
                                                                                                                                                                    detallar = valor.Detallar
                                                                                                                                                                End Sub)
                            End If
                            parametros.TotalesMedioPago.Add(New TotalMedioPago(divisa, medioPago, cantidad, importe, detallar, color, Nothing))
                        Next
                    End If
                Next
            End If
            If divisa.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso divisa.ValoresTotalesTipoMedioPago.Count > 0 Then
                For Each vtotal In divisa.ValoresTotalesTipoMedioPago
                    parametros.TotalesMedioPago.Add(New TotalMedioPago(divisa, Nothing, 0, 0, False, Nothing, vtotal))
                Next
            End If

        Next

        parametros.Dicionario = New Dictionary(Of String, String)() From {
            {"divisa", Tradutor.Traduzir("055_divisa")},
            {"remover", Tradutor.Traduzir("055_remover")},
            {"selecione", Tradutor.Traduzir("055_selecione")},
            {"preenchaAlgumTotal", Tradutor.Traduzir("055_preenchaAlgumTotal")},
            {"valorNumerico", Tradutor.Traduzir("055_valorNumerico")},
            {"tituloComponente", Tradutor.Traduzir("055_tituloComponente")},
            {"totalesMedioPago", Tradutor.Traduzir("055_totalesMedioPago")},
            {"totalesMedioPagoGrilla", Tradutor.Traduzir("055_totalesMedioPagoGrilla")},
            {"cantidad", Tradutor.Traduzir("055_cantidad")},
            {"valor", Tradutor.Traduzir("055_valor")},
            {"tipoMedioPago", Tradutor.Traduzir("055_tipoMedioPago")},
            {"medioPago", Tradutor.Traduzir("055_medioPago")},
            {"informacionesComplementares", Tradutor.Traduzir("055_informacionesComplementares")},
            {"terminosMedioPago", Tradutor.Traduzir("055_terminosMedioPago")},
            {"mascaratermino", Tradutor.Traduzir("055_mascaratermino")},
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

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ucSaldoMedioPago_Inicializar_" & Me.ClientID, "InicializaUcSaldoMedioPagoModificar(" & JsonConvert.SerializeObject(parametros, New Converters.StringEnumConverter()) & ", '" & pnlTotalesMedioPago.ClientID & "','" & Me.ClientID & "');", True)

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

    Public Function verificarTipoMedioPago(identificadorDivisa As String, identificadorMedioPago As String, tipoMedioPago As String) As Boolean

        Return DivisasDisponiveis IsNot Nothing AndAlso DivisasDisponiveis.Count > 0 AndAlso
               DivisasDisponiveis.Exists(Function(e) e.Identificador = identificadorDivisa AndAlso TipoMedioPagoDisponivel = tipoMedioPago)

    End Function

    Public Function RecuperarDivisa(totalesMedioPago As Linq.JArray, idDivisa As String) As Clases.Divisa

        Dim divisa As Clases.Divisa = Nothing
        Dim medioPago As Clases.MedioPago = Nothing

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

                If Not idDivisa = totalMedioPago.IdentificadorDivisa Then
                    Continue For
                End If

                Dim totalMedioPagoLocal = totalMedioPago

                If identificadorDivisaMedioPago <> totalMedioPago.IdentificadorDivisa Then
                    identificadorDivisaMedioPago = totalMedioPago.IdentificadorDivisa
                    identificadorMedioPagoDistinto = String.Empty
                    divisa = If(_DivisasRetorno Is Nothing, Nothing, _DivisasRetorno.Find(Function(divisaRetorno) divisaRetorno.Identificador = identificadorDivisaMedioPago))
                    If divisa Is Nothing Then
                        divisa = DivisasDisponiveis.Find(Function(divisaDisponivel) divisaDisponivel.Identificador = identificadorDivisaMedioPago).GetDivisa().Clonar()
                        If divisa IsNot Nothing Then
                            divisa.MediosPago = New ObservableCollection(Of Clases.MedioPago)()
                            divisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)()
                        End If
                    Else
                        divisa.MediosPago = New ObservableCollection(Of Clases.MedioPago)()
                        divisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)()
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
                                              .UnidadMedida = Nothing,
                                              .TipoValor = TipoValor,
                                              .InformadoPor = Enumeradores.TipoContado.NoDefinido,
                                              .Cantidad = totalMedioPago.Cantidad,
                                              .Importe = totalMedioPago.Valor,
                                              .Terminos = TerminosConValor
                                          })

                ElseIf Not String.IsNullOrEmpty(totalMedioPago.IdentificadorMedioPago) Then

                    medioPago.Valores.Add(New Clases.ValorMedioPago() With {
                                              .UnidadMedida = Nothing,
                                              .TipoValor = TipoValor,
                                              .InformadoPor = Enumeradores.TipoContado.NoDefinido,
                                              .Cantidad = totalMedioPago.Cantidad,
                                              .Importe = totalMedioPago.Valor,
                                              .Terminos = Nothing})

                Else

                    divisa.ValoresTotalesTipoMedioPago.Add(New Clases.ValorTipoMedioPago() With {
                                              .Cantidad = totalMedioPago.Cantidad,
                                              .Importe = totalMedioPago.Valor,
                                              .TipoMedioPago = [Enum].Parse(GetType(Enumeradores.TipoMedioPago), TipoMedioPagoDisponivel),
                                              .TipoValor = Enumeradores.TipoValor.NoDefinido
                                              })


                End If

            Next totalMedioPago

        End If

        Return divisa

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

    Public Class ParametrosUcSaldoMedioPagoModificar

#Region "Total Medio Pago"

#Region "Total Medio Pago"

        Public Class TotalMedioPago
            Public Property Remover As Boolean
            Public Property Divisa As Clases.Divisa
            Public Property MedioPago As Clases.MedioPago
            Public Property Cantidad As Decimal
            Public Property Valor As Decimal
            Public Property Detallar As Boolean
            Public Property ColorFondo As String
            Public Sub New()

            End Sub

            Public Sub New(divisa As Clases.Divisa, _
                           medioPago As Clases.MedioPago, _
                           cantidad As Int64,
                           importe As Decimal,
                           detallar As Boolean,
                           color As Drawing.Color,
                           valorTotal As Clases.ValorTipoMedioPago)

                Me.Divisa = divisa
                Me.MedioPago = medioPago

                If valorTotal Is Nothing Then

                    Me.Cantidad = cantidad
                    Me.Valor = importe
                    Me.Detallar = detallar
                    Me.ColorFondo = Drawing.ColorTranslator.ToHtml(color)

                Else

                    Me.Valor = valorTotal.Importe
                    Me.Detallar = valorTotal.Detallar
                    Me.ColorFondo = Drawing.ColorTranslator.ToHtml(valorTotal.Color)

                End If

            End Sub

        End Class

#End Region

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
        Public Property HabilitaCheckBox As Boolean
        Public Property ExhibirTotalesMedioPago As Boolean
        Public Property DivisasDisponiveis As ObservableCollection(Of DivisaJS)
        Public Property UnidadesMedidaDisponiveis As ObservableCollection(Of Clases.UnidadMedida)
        Public Property TiposMedioPagoDisponiveis As List(Of String)
        Public Property TipoMedioPagoDisponivel As String
        Public Property TotalesMedioPago As List(Of TotalMedioPago)
        Public Property SeparadorDecimal As String
        Public Property SeparadorMilhar As String

    End Class

#End Region

End Class