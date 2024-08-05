Imports System.IO
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Xml.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel

Public Class ucEfectivo
    Inherits UcBase

#Region "Variaveis"

    ''' <descripcion>
    ''' Controle rellenado dentro del Popup blank usado para rellenar divisas en el controle ucEfectivo.
    ''' </descripcion>
    Private WithEvents _ucPopupAgregarDivisa As ucAgregarDivisa

#End Region

#Region "Propriedades"

    ''' <summary>
    ''' Divisas del cliente rellenadas en el controle ucEfectivo.
    ''' </summary>
    Public Property DivisaIAC As ObservableCollection(Of Divisa)

    ''' <summary>
    ''' Lista de UnidadMedida, rellenada de uno archivo XML.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UnidadMedida As ObservableCollection(Of UnidadMedida)

    ''' <summary>
    ''' Lista de Calidad, rellenada de uno archivo XML.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Calidad As ObservableCollection(Of Calidad)

    ''' <summary>
    ''' Modo de operación: {Alta, Modificacion, Consulta}
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Modo As Enumeradores.Modo

    ''' <summary>
    ''' Tipo de valor: {Declarado, Contado}
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TipoValor As Comon.Enumeradores.TipoValor

    ''' <summary>
    ''' Recibe el retorno del controle ucAgregarDivisa.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListaDivisasSelecionadas As New ObservableCollection(Of Divisa)

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

            lblFiltroDivisa.Text = Traduzir("012_efectivo")
            CargarDivisas()

        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try
    End Sub

    'Public Overrides Sub ConfigurarTabIndexControle()
    '    MyBase.ConfigurarTabIndexControle()

    '    Aplicacao.Util.Utilidad.ConfigurarTabIndex(_ucDivisaEfectivo, btnPopupAgregar, _ucPopupAgregarDivisa)
    'End Sub

    Private Sub CargarDivisas()

        CargarUnidadesMedida()
        CargarCalidades()

        CargarDivisaEfectivo()
        If Not Modo = Enumeradores.Modo.Consulta AndAlso Not Modo = Enumeradores.Modo.Baja Then
            CargarAgregarDivisa(_ucDivisaEfectivo.DivisasActualizadas)
        End If
    End Sub

    Private Sub CargarDivisaEfectivo()

        '_ucDivisaEfectivo = LoadControl("~/Controles/ucDivisaEfectivo.ascx")
        If DivisaIAC IsNot Nothing Then

            ' solo irá ejecutar el método, se el modo es distinto de consulta e la divisa poser valores totales o alguna denominacion.
            If Modo <> Enumeradores.Modo.Consulta AndAlso _
              (DivisaIAC.Exists(Function(f) f.ValoresTotalesEfectivo IsNot Nothing AndAlso f.ValoresTotalesEfectivo.Count > 0) OrElse _
                DivisaIAC.Exists(Function(f) f.Denominaciones IsNot Nothing AndAlso f.Denominaciones.Count > 0)) Then

                Aplicacao.Util.Utilidad.AgregarDependenciasEnDivisa(DivisaIAC, "E")
            End If
        End If

        _ucDivisaEfectivo.DivisaIAC = DivisaIAC
        _ucDivisaEfectivo.Modo = Modo
        _ucDivisaEfectivo.TipoValor = TipoValor
        _ucDivisaEfectivo.UnidadMedida = UnidadMedida
        _ucDivisaEfectivo.Calidad = Calidad
        '_divucDivisas.Controls.Add(_ucDivisaEfectivo)
    End Sub

    Private Sub CargarAgregarDivisa(DivisasActualizadas As ObservableCollection(Of Clases.Divisa))

        _ucPopupAgregarDivisa = LoadControl("~/Controles/ucAgregarDivisa.ascx")
        _ucPopupAgregarDivisa.DivisasActualizadas = DivisasActualizadas
        _ucPopupAgregarDivisa.EsEfectivo = True
        _ucPopupAgregarDivisa.Titulo = Traduzir("012_titulopopup")
        _popupBlank.PopupBase = _ucPopupAgregarDivisa

    End Sub

    ''' <summary>
    ''' Guardar los datos para el objeto Divisa
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GuardarDatos()
        Try
            If _ucDivisaEfectivo IsNot Nothing Then
                _ucDivisaEfectivo.GuardarDatos()
                DivisaIAC = _ucDivisaEfectivo.DivisaIAC
            End If

        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try

    End Sub

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
                If _ucDivisaEfectivo IsNot Nothing Then
                    Dim divisasFoco As ObservableCollection(Of Clases.Divisa) = ListaDivisasSelecionadas.Clonar()
                    ListaDivisasSelecionadas.AddRange(_ucDivisaEfectivo.DivisasActualizadas)
                    _ucDivisaEfectivo.DivisasActualizadas.Clear()
                    _ucDivisaEfectivo.DivisasActualizadas.AddRange(ListaDivisasSelecionadas.OrderBy(Function(f) f.Descripcion))

                    Dim Repeater As Repeater = _ucDivisaEfectivo.FindControl("rptDivisaEfectivo")
                    Repeater.DataSource = _ucDivisaEfectivo.DivisasActualizadas
                    Repeater.DataBind()

                    Dim itemDivisaAux As Clases.Divisa = Nothing
                    For Each _itemBound As RepeaterItem In Repeater.Items
                        ' recupera la divisa currente
                        itemDivisaAux = _ucDivisaEfectivo.DivisasActualizadas(_itemBound.ItemIndex)

                        If (divisasFoco.Exists(Function(d) d.Identificador = itemDivisaAux.Identificador)) Then
                            Dim controleFoco As Control = _itemBound.FindControl("txtImporteDivisa")

                            If (controleFoco IsNot Nothing) Then
                                controleFoco.Focus()
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
            Me.GuardarDatos()
            _ucPopupAgregarDivisa.CargarDatosGridView()
            popupBlank.AbrirPopup()

        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try

    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' OnError en ucDivisa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overloads Sub OnError(sender As Object, e As ErroEventArgs) Handles _ucDivisaEfectivo.Erro

        If e IsNot Nothing Then
            MyBase.NotificarErro(e.Erro)
        End If

    End Sub

    ''' <summary>
    ''' Obtener Unidades de Medida
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargarUnidadesMedida()
        Try
            UnidadMedida = LogicaNegocio.Genesis.UnidadMedida.ObtenerUnidadesMedida

        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Obtener Calidades
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargarCalidades()
        Try
            Calidad = LogicaNegocio.Genesis.Calidad.ObtenerCalidades

        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try

    End Sub


#End Region


End Class