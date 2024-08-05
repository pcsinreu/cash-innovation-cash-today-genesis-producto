Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports System.Web.Script.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones

Public Class ucFiltroDivisas
    Inherits UcBase

#Region "Propriedades"

#Region "    Entrada    "

    ''' <summary>
    ''' Define el titulo del controle
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Property Titulo As String

    ''' <summary>
    ''' Define se serán consideradas las divisas inactivas
    ''' </summary>
    ''' <value>Bolean</value>
    ''' <returns>Bolean</returns>
    ''' <remarks></remarks>
    Public Property MostrarOpcionDivisasInactivas As Boolean

    Public Property MostrarTiposValores As Boolean
    Public Property MostrarSolamenteDivisas As Boolean

    Public ReadOnly Property ConsiderarValoresItemSelecionado() As String
        Get
            Dim selecionado As String = String.Empty
            If rblConsiderarValoresAmbos.Checked Then
                selecionado = "AMBOS"
            ElseIf rblConsiderarValoresDisponivel.Checked Then
                selecionado = "DISPONIVEL"
            Else
                selecionado = "NODISPONIVEL"
            End If

            Return selecionado
        End Get
    End Property

    Public ReadOnly Property FormatoItemSelecionado() As String
        Get
            Dim selecionado As String = String.Empty
            If rblFormatoPDF.Checked Then
                selecionado = "PDF"
            ElseIf rblFormatoEXCEL.Checked Then
                selecionado = "EXCEL"
            End If

            Return selecionado
        End Get
    End Property

    Public ReadOnly Property NoConsiderarSectoresHijos() As Boolean
        Get
            Return chkNoConsiderarSectoresHijos.Checked
        End Get
    End Property
#End Region

#Region "    Salida    "

    Private _IdentificadoresDivisas As List(Of String)
    ''' <summary>
    ''' Define la lista de divisas selecionadas
    ''' </summary>
    ''' <value></value>
    ''' <returns>List(Of String)</returns>
    ''' <remarks></remarks>
    Public Property IdentificadoresDivisas As List(Of String)
        Get
            If _IdentificadoresDivisas Is Nothing Then
                _IdentificadoresDivisas = New List(Of String)
            End If
            Return _IdentificadoresDivisas
        End Get
        Set(value As List(Of String))
            _IdentificadoresDivisas = value
        End Set
    End Property

    Private _IdentificadorEfectivos As List(Of String)
    ''' <summary>
    ''' Define la lista de efectivos selecionados
    ''' </summary>
    ''' <value></value>
    ''' <returns>List(Of String)</returns>
    ''' <remarks></remarks>
    Public Property IdentificadoresEfectivos As List(Of String)
        Get
            If _IdentificadorEfectivos Is Nothing Then
                _IdentificadorEfectivos = New List(Of String)
            End If
            Return _IdentificadorEfectivos
        End Get
        Set(value As List(Of String))
            _IdentificadorEfectivos = value
        End Set
    End Property

    Private _IdentificadoresMediosPago As List(Of String)
    ''' <summary>
    ''' Define la lista de medios pagos selecionados
    ''' </summary>
    ''' <value></value>
    ''' <returns>List(Of String)</returns>
    ''' <remarks></remarks>
    Public Property IdentificadoresMediosPago As List(Of String)
        Get
            If _IdentificadoresMediosPago Is Nothing Then
                _IdentificadoresMediosPago = New List(Of String)
            End If
            Return _IdentificadoresMediosPago
        End Get
        Set(value As List(Of String))
            _IdentificadoresMediosPago = value
        End Set
    End Property

    ''' <summary>
    ''' Define se serán filtrados los totales efectivos
    ''' </summary>
    ''' <value>Bolean</value>
    ''' <returns>Bolean</returns>
    ''' <remarks></remarks>
    Public Property TotalesEfectivo As Boolean

    ''' <summary>
    ''' Define se serán filtrados los totales del tipo medio pago
    ''' </summary>
    ''' <value>Bolean</value>
    ''' <returns>Bolean</returns>
    ''' <remarks></remarks>
    Public Property TotalesTipoMedioPago As Boolean

    Public ReadOnly Property TipoValor() As String
        Get
            Dim tempTipoValor As String = String.Empty
            If rbTiposValoresEfectivo.Checked Then
                tempTipoValor = "EFECTIVO"
            ElseIf rbTiposValoresCheque.Checked Then
                tempTipoValor = "CHEQUE"
            ElseIf rbTiposValoresTicket.Checked Then
                tempTipoValor = "TICKET"
            ElseIf rbTiposValoresOtrosValores.Checked Then
                tempTipoValor = "OTROS_VALORES"
            End If
            Return tempTipoValor
        End Get
    End Property

#End Region

#End Region

#Region "Eventos"

    Protected Overrides Sub TraduzirControles()

        Me.lbltituloDivisas.Text = "Divisas"
        Me.lblTituloEfectivos.Text = Traduzir("051_efectivos")
        Me.lblTituloTipoMediosPago.Text = Traduzir("051_mediospago")
        Me.chkDivisasInactivas.Text = Traduzir("051_divisasinactivas")
        Me.chkTotalesEfectivos.Text = Traduzir("051_totalesefectivos")
        Me.chkTotalesTipoMedioPago.Text = Traduzir("051_totalesmediospago")
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            ViewState("vsParametrosJSON") = Me.hdnViewState.Value

            If Not Me.IsPostBack Then

                Me.Exhibicion()
                Me.CargarDatos()
            End If

        Catch ex As Genesis.Excepcion.NegocioExcepcion
            MyBase.NotificarErro(ex)

        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try

    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Cargar el controle con la definición de los parámetros de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargarDatos()
        Dim Mesaje As New StringBuilder

        If Validar(Mesaje) Then

            Me.lbltituloFiltroDivisas.Text = Me.Titulo
            Me.chkDivisasInactivas.Visible = Me.MostrarOpcionDivisasInactivas

            Dim jss As New JavaScriptSerializer
            Dim parametros As New ParametrosJson(Genesis.LogicaNegocio.Genesis.Divisas.ObtenerDivisas(, , , , True, True, True, True).OrderBy(Function(x) x.Descripcion).ToObservableCollection, Not Me.chkDivisasInactivas.Checked)
            Me.hdnViewState.Value = jss.Serialize(parametros)
            ViewState("vsParametrosJSON") = Me.hdnViewState.Value

        Else
            Throw New Genesis.Excepcion.NegocioExcepcion(Mesaje.ToString)

        End If

    End Sub

    ''' <summary>
    ''' Almacenar los datos selecionados en el controle
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GuardarDatos()

        Dim jss As New JavaScriptSerializer
        Dim ParametrosJson As ParametrosJson = jss.Deserialize(Of ParametrosJson)(hdnViewState.Value)

        For Each divisa In ParametrosJson.Divisas

            If divisa.EsSelecionado Then
                If Not Me.IdentificadoresDivisas.Contains(divisa.Identificador) Then
                    Me.IdentificadoresDivisas.Add(divisa.Identificador)

                End If

                If divisa.Efectivos IsNot Nothing AndAlso divisa.Efectivos.Count > 0 Then
                    For Each efectivo In divisa.Efectivos

                        If efectivo.EsSelecionado Then
                            If Not Me.IdentificadoresEfectivos.Contains(efectivo.Identificador) Then
                                Me.IdentificadoresEfectivos.Add(efectivo.Identificador)
                            End If
                        End If

                    Next efectivo

                End If

                If divisa.MediosPago IsNot Nothing AndAlso divisa.MediosPago.Count > 0 Then
                    For Each mediopago In divisa.MediosPago

                        If mediopago.EsSelecionado Then
                            If Not Me.IdentificadoresMediosPago.Contains(mediopago.Identificador) Then
                                Me.IdentificadoresMediosPago.Add(mediopago.Identificador)
                            End If
                        End If

                    Next mediopago

                End If

            End If

        Next divisa

        Me.TotalesEfectivo = If(Me.chkTotalesEfectivos.Enabled, Me.chkTotalesEfectivos.Checked, False)
        Me.TotalesTipoMedioPago = If(Me.chkTotalesTipoMedioPago.Enabled, Me.chkTotalesTipoMedioPago.Checked, False)

        'Me.ltlParametrosJson.Text = jss.Serialize(ParametrosJson)
        Me.hdnViewState.Value = jss.Serialize(ParametrosJson)

    End Sub

    ''' <summary>
    ''' Validar el unico parámetro obligatorio del controle
    ''' </summary>
    ''' <param name="Mesajes">Lista de los errores</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Validar(ByRef Mesajes As StringBuilder) As Boolean

        If String.IsNullOrEmpty(Me.Titulo) Then
            Mesajes.AppendLine(String.Format(Traduzir("014_parametro_obrigatorio"), "Titulo"))
        End If

        If Mesajes.Length > 0 Then
            Return False

        End If

        Return True

    End Function

    Private Sub Exhibicion()

        fsTiposValores.Visible = MostrarTiposValores AndAlso Not MostrarSolamenteDivisas
        chkTotalesEfectivos.Visible = Not MostrarTiposValores AndAlso Not MostrarSolamenteDivisas
        chkTotalesTipoMedioPago.Visible = Not MostrarTiposValores AndAlso Not MostrarSolamenteDivisas
        lblTituloEfectivos.Visible = Not MostrarTiposValores AndAlso Not MostrarSolamenteDivisas
        lblTituloTipoMediosPago.Visible = Not MostrarTiposValores AndAlso Not MostrarSolamenteDivisas
        If MostrarTiposValores OrElse MostrarSolamenteDivisas Then
            dvCheckBoxListEfectivos.Style.Item("display") = "none"
            dvCheckBoxListMediosPago.Style.Item("display") = "none"
        End If

    End Sub

    Public Sub FiltroTransacionesExibir()
        filtroTransaciones.Visible = True
        rblConsiderarValoresAmbos.Text = Traduzir("057_ambos")
        rblConsiderarValoresDisponivel.Text = Traduzir("057_disponible")
        rblConsiderarValoresNdisponivel.Text = Traduzir("057_ndisponible")

        rblFormatoPDF.Text = Traduzir("056_formato_pdf")
        rblFormatoEXCEL.Text = Traduzir("056_formato_excel")

        chkNoConsiderarSectoresHijos.Text = Traduzir("057_NoConsiderarSectoresHijos")
    End Sub

#End Region

#Region "Json"

    Private Class ParametrosJson

#Region "    Propriedades    "

        Public Property Divisas As List(Of Divisa)
        Public Property MesajeDivisa As String
        Public Property pMesajeEfectivo As String
        Public Property sMesajeEfectivo As String
        Public Property pMesajeMedioPago As String
        Public Property sMesajeMedioPago As String
        Public Property MesajeDivisasNoSelecionadas As String

#End Region

#Region "    Constructores    "

        Sub New()

        End Sub

        Sub New(pDivisas As ObservableCollection(Of Clases.Divisa), _
                EstaActivo As Boolean)

            If pDivisas IsNot Nothing AndAlso pDivisas.Count > 0 Then
                Divisas = New List(Of Divisa)
                For Each _divisa In pDivisas

                    Dim divisa As New Divisa
                    With divisa
                        .Identificador = _divisa.Identificador
                        .Descripcion = _divisa.Descripcion
                        .EstaActivo = _divisa.EstaActivo

                        If _divisa.Denominaciones IsNot Nothing AndAlso _divisa.Denominaciones.Count > 0 Then
                            .Efectivos = New List(Of Efectivos)
                            For Each _efectivo In _divisa.Denominaciones.OrderBy(Function(x) x.Descripcion).ToList
                                Dim efectivo As New Efectivos With {.Identificador = _efectivo.Identificador, _
                                                                    .Descripcion = _efectivo.Descripcion, _
                                                                    .IdentificadorDivisa = _divisa.Identificador}

                                .Efectivos.Add(efectivo)

                            Next _efectivo
                        End If

                        If _divisa.MediosPago IsNot Nothing AndAlso _divisa.MediosPago.Count > 0 Then
                            .MediosPago = New List(Of MediosPago)
                            For Each _mediopago In _divisa.MediosPago.OrderBy(Function(x) x.Descripcion).ToList

                                Dim mediopago As New MediosPago With {.Identificador = _mediopago.Identificador, _
                                                                      .Descripcion = _mediopago.Descripcion, _
                                                                      .IdentificadorDivisa = _divisa.Identificador}

                                .MediosPago.Add(mediopago)

                            Next _mediopago
                        End If

                    End With

                    Divisas.Add(divisa)

                Next _divisa
            End If


            Dim EsActivo As Boolean = Not EstaActivo

            Me.MesajeDivisa = String.Format(Traduzir("051_divisa_vacio"), If(EsActivo, Traduzir("051_activo"), String.Empty))
            Me.pMesajeEfectivo = String.Format(Traduzir("051_efectivo_vacio"), "s", "s", "s")
            Me.sMesajeEfectivo = String.Format(Traduzir("051_efectivo_vacio"), "", "", "")
            Me.pMesajeMedioPago = String.Format(Traduzir("051_mediopago_vacio"), "s", "s", "s")
            Me.sMesajeMedioPago = String.Format(Traduzir("051_mediopago_vacio"), "", "", "")
            Me.MesajeDivisasNoSelecionadas = Traduzir("051_divisasnoselecionadas")

        End Sub

#End Region

    End Class

    Private Class Divisa
        Public Property Identificador As String
        Public Property Descripcion As String
        Public Property EstaActivo As Boolean
        Public Property EsSelecionado As Boolean
        Public Property Efectivos As List(Of Efectivos)
        Public Property MediosPago As List(Of MediosPago)

    End Class

    Private Class Efectivos
        Public Property Identificador As String
        Public Property Descripcion As String
        Public Property IdentificadorDivisa As String
        Public Property EsSelecionado As Boolean

    End Class

    Private Class MediosPago
        Public Property Identificador As String
        Public Property Descripcion As String
        Public Property IdentificadorDivisa As String
        Public Property EsSelecionado As Boolean

    End Class

#End Region

End Class