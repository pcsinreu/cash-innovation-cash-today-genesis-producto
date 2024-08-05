Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel


Public Class ucMedioPago
    Inherits UcBase

#Region "Variaveis"

    ''' <summary>
    ''' ucAgregarDivisa
    ''' </summary>
    ''' <descripcion>
    ''' Controle rellenado dentro del Popup blank usado para rellenar divisas en el controle ucEfectivo.
    ''' </descripcion>
    ''' <remarks></remarks>
    Private WithEvents _ucPopupAgregarDivisa As ucAgregarDivisa
    Private WithEvents _ucDivisaMedioPago As ucDivisaMedioPago

#End Region

#Region "Propriedades"

    ''' <summary>
    ''' Divisas del cliente rellenadas en el controle ucEfectivo.
    ''' </summary>
    Public Property DivisasIAC As ObservableCollection(Of Clases.Divisa)

    ''' <summary>
    '''  Recibe el retorno del controle ucAgregarDivisa.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListaDivisasSelecionadas As ObservableCollection(Of Clases.Divisa)

    ''' <summary>
    ''' Modo de operación: {Alta, Modificacion, Consulta}
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Modo As Comon.Enumeradores.Modo

    ''' <summary>
    ''' Tipo Valor: {Contado, Declarado}
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TipoValor As Comon.Enumeradores.TipoValor

#End Region

#Region "Eventos Pagina"

    ''' <summary>
    ''' Evento Load do controle ucEfectivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Select Case Modo

                Case Comon.Enumeradores.Modo.Alta, Comon.Enumeradores.Modo.Modificacion
                    btnPopupAgregar.Visible = True

                Case Comon.Enumeradores.Modo.Consulta
                    btnPopupAgregar.Visible = False

            End Select
            lblFiltroDivisa.Text = Traduzir("012_mediospago")
            CargarDivisas()

        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try
    End Sub

    'Public Overrides Sub ConfigurarTabIndexControle()
    '    MyBase.ConfigurarTabIndexControle()

    '    Aplicacao.Util.Utilidad.ConfigurarTabIndex(_ucDivisaMedioPago, btnPopupAgregar, _ucPopupAgregarDivisa)
    'End Sub

#End Region

#Region "Eventos Popup"

    ''' <summary>
    ''' Evento Erro do Popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ucPopupAgregarDivisa_Erro(sender As Object, e As ErroEventArgs) Handles _ucPopupAgregarDivisa.Erro
        MyBase.NotificarErro(e.Erro)
    End Sub

    ''' <summary>
    ''' Evento Fechado do Popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ucPopupAgregarDivisa_Fechado(sender As Object, e As PopupEventArgs) Handles _ucPopupAgregarDivisa.Fechado
        Try

            If e.Resultado IsNot Nothing AndAlso CType(e.Resultado, ObservableCollection(Of Clases.Divisa)).Count > 0 Then

                ListaDivisasSelecionadas = DirectCast(e.Resultado, ObservableCollection(Of Clases.Divisa))
                If _ucDivisaMedioPago IsNot Nothing Then
                    Dim divisasFoco As ObservableCollection(Of Clases.Divisa) = ListaDivisasSelecionadas.Clonar()
                    ListaDivisasSelecionadas.AddRange(_ucDivisaMedioPago.DivisasActualizadas)

                    _ucDivisaMedioPago.DivisasActualizadas.Clear()
                    _ucDivisaMedioPago.DivisasActualizadas.AddRange(ListaDivisasSelecionadas.OrderBy(Function(f) f.Descripcion))

                    Dim Repeater As Repeater = _ucDivisaMedioPago.FindControl("rptDivisa")
                    Repeater.DataSource = _ucDivisaMedioPago.DivisasActualizadas
                    Repeater.DataBind()

                    Dim itemDivisaAux As Clases.Divisa = Nothing

                    ' loop en los itens del repeater de divisas, o sea, cada divisa rellenada en el controle
                    For Each _itemBound As RepeaterItem In Repeater.Items
                        ' recupera la divisa currente
                        itemDivisaAux = _ucDivisaMedioPago.DivisasActualizadas(_itemBound.ItemIndex)

                        If (divisasFoco.Exists(Function(d) d.Identificador = itemDivisaAux.Identificador)) Then
                            Dim controle As Control = _itemBound.FindControl("btnBorrarDivisa")

                            If (controle IsNot Nothing) Then
                                controle.Focus()
                            End If

                            Exit For
                        End If
                    Next
                End If
            Else
                Me.btnPopupAgregar.Focus()
            End If

        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento Click do botao Agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub btnAgregar_Click(sender As Object, e As System.EventArgs) Handles btnPopupAgregar.Click
        Try
            GuardarDatos(False)
            _ucPopupAgregarDivisa.CargarDatosGridView()
            _popupBlank.AbrirPopup()
        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Actualizar el objeto de divisas original con los cambios ejecutados.
    ''' </summary>
    ''' <param name="EsValidacion">Define se el metodo ActualizarControels irá llamar la validación de los datos rellenados</param>
    ''' <remarks></remarks>
    Public Sub GuardarDatos(EsValidacion As Boolean)
        Try
            If _ucDivisaMedioPago IsNot Nothing Then
                _ucDivisaMedioPago.GuardarDatos(EsValidacion)
                If EsValidacion Then
                    DivisasIAC = _ucDivisaMedioPago.DivisasIAC
                End If
            End If
        Catch ex As Exception
            'MyBase.NotificarErro(ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Cargar las divisas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargarDivisas()
        CargarDivisaMediosPago()
        If Not Modo = Enumeradores.Modo.Consulta AndAlso Not Modo = Enumeradores.Modo.Baja Then
            CargarAgregarDivisa(_ucDivisaMedioPago.DivisasActualizadas)
        End If
    End Sub

    ''' <summary>
    ''' Cargar los medios pago
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargarDivisaMediosPago()

        _ucDivisaMedioPago = LoadControl("~/Controles/ucDivisaMedioPago.ascx")
        AddHandler _ucDivisaMedioPago.Erro, AddressOf OnError
        If DivisasIAC IsNot Nothing Then
            If Modo = Enumeradores.Modo.Modificacion AndAlso _
                (DivisasIAC.Exists(Function(f) f.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso f.ValoresTotalesTipoMedioPago.Count > 0) OrElse _
                DivisasIAC.Exists(Function(f) f.MediosPago IsNot Nothing AndAlso f.MediosPago.Count > 0)) Then

                Aplicacao.Util.Utilidad.AgregarDependenciasEnDivisa(DivisasIAC, "M")

            End If
        End If
        _ucDivisaMedioPago.DivisasIAC = DivisasIAC
        _ucDivisaMedioPago.Modo = Modo
        _ucDivisaMedioPago.TipoValor = TipoValor

        divucDivisas.Controls.Add(_ucDivisaMedioPago)

    End Sub

    ''' <summary>
    ''' Cargar el popup de agregar divisa
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargarAgregarDivisa(DivisasActualizadas As ObservableCollection(Of Clases.Divisa))

        _ucPopupAgregarDivisa = LoadControl("~/Controles/ucAgregarDivisa.ascx")
        _ucPopupAgregarDivisa.DivisasActualizadas = DivisasActualizadas
        _ucPopupAgregarDivisa.EsEfectivo = False
        _ucPopupAgregarDivisa.Titulo = Traduzir("012_titulopopup")
        popupBlank.PopupBase = _ucPopupAgregarDivisa

    End Sub

    ''' <summary>
    ''' OnError en ucDivisa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overloads Sub OnError(sender As Object, e As ErroEventArgs)
        If e IsNot Nothing Then
            MyBase.NotificarErro(e.Erro)
        End If
    End Sub

#End Region

End Class