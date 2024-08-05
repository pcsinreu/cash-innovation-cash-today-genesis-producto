Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon

Public Class ucAcciones
    Inherits UcBase

#Region "[PROPRIEDADES]"

    Public Property btnGuardarVisible() As Boolean
        Get
            Return Me.btnGuardar.Visible
        End Get
        Set(value As Boolean)
            Me.btnGuardar.Visible = value
        End Set
    End Property

    Public Property btnGuardarConfirmarVisible() As Boolean
        Get
            Return Me.btnGuardarConfirmar.Visible
        End Get
        Set(value As Boolean)
            Me.btnGuardarConfirmar.Visible = value
        End Set
    End Property

    Public Property btnConfirmarVisible() As Boolean
        Get
            Return Me.btnConfirmar.Visible
        End Get
        Set(value As Boolean)
            Me.btnConfirmar.Visible = value
        End Set
    End Property

    Public Property btnAceptarVisible() As Boolean
        Get
            Return Me.btnAceptar.Visible
        End Get
        Set(value As Boolean)
            Me.btnAceptar.Visible = value
        End Set
    End Property

    Public Property btnRechazarVisible() As Boolean
        Get
            Return Me.btnRechazar.Visible
        End Get
        Set(value As Boolean)
            Me.btnRechazar.Visible = value
        End Set
    End Property

    Public Property btnAnularVisible() As Boolean
        Get
            Return Me.btnAnular.Visible
        End Get
        Set(value As Boolean)
            Me.btnAnular.Visible = value
        End Set
    End Property

    Public Property btnImprimirVisible() As Boolean
        Get
            Return Me.btnImprimir.Visible
        End Get
        Set(value As Boolean)
            Me.btnImprimir.Visible = value
        End Set
    End Property

    Public Property btnCancelarVisible() As Boolean
        Get
            Return Me.btnCancelar.Visible
        End Get
        Set(value As Boolean)
            Me.btnCancelar.Visible = value
        End Set
    End Property

    Public Property btnModificarVisible() As Boolean
        Get
            Return Me.btnModificar.Visible
        End Get
        Set(value As Boolean)
            Me.btnModificar.Visible = value
        End Set
    End Property

    Public Property btnVisualizarVisible() As Boolean
        Get
            Return Me.btnVisualizar.Visible
        End Get
        Set(value As Boolean)
            Me.btnVisualizar.Visible = value
        End Set
    End Property

    Public Property btnAgregarDocumentoVisible() As Boolean
        Get
            Return Me.btnAgregarDocumento.Visible
        End Get
        Set(value As Boolean)
            Me.btnAgregarDocumento.Visible = value
        End Set
    End Property

    Public Property btnConfigurarNovoReporteVisible() As Boolean
        Get
            Return Me.btnConfigurarNovoReporte.Visible
        End Get
        Set(value As Boolean)
            Me.btnConfigurarNovoReporte.Visible = value
        End Set
    End Property

    Public ReadOnly Property UniqueIDbtnConfirmar As String
        Get
            Return btnConfirmar.UniqueID
        End Get
    End Property

    Public Property btnModificarTerminoVisible() As Boolean
        Get
            Return Me.btnModificarTermino.Visible
        End Get
        Set(value As Boolean)
            Me.btnModificarTermino.Visible = value
        End Set
    End Property

    Public Property btnSalvarTerminoVisible() As Boolean
        Get
            Return Me.btnSalvarTerminos.Visible
        End Get
        Set(value As Boolean)
            Me.btnSalvarTerminos.Visible = value
        End Set
    End Property

    Private _MovimientodeAceptacionAutomatica As Boolean
    Public Property MovimientodeAceptacionAutomatica() As Boolean
        Get
            Return _MovimientodeAceptacionAutomatica
        End Get
        Set(value As Boolean)
            _MovimientodeAceptacionAutomatica = value
        End Set
    End Property


    Public Property btnGuardarConfirmarText() As String
        Get
            Return Me.btnGuardarConfirmar.Text
        End Get
        Set(value As String)
            Me.btnGuardarConfirmar.Text = value
        End Set
    End Property

    Public Property btnConfirmarText() As String
        Get
            Return Me.btnConfirmar.Text
        End Get
        Set(value As String)
            Me.btnConfirmar.Text = value
        End Set
    End Property

    'Public Property btnAgregarBultoVisible() As Boolean
    '    Get
    '        Return Me.btnAgregarBulto.Visible
    '    End Get
    '    Set(value As Boolean)
    '        Me.btnAgregarBulto.Visible = value
    '    End Set
    'End Property

    'Public Property btnAgregarParcialVisible() As Boolean
    '    Get
    '        Return Me.btnAgregarParcial.Visible
    '    End Get
    '    Set(value As Boolean)
    '        Me.btnAgregarParcial.Visible = value
    '    End Set
    'End Property

#End Region

#Region "[BOTONES]"

    Public Delegate Sub Accion_Handler()

    Public Event onAccionGuardar As Accion_Handler
    Public Event onAccionGuardarConfirmar As Accion_Handler
    Public Event onAccionConfirmar As Accion_Handler
    Public Event onAccionAceptar As Accion_Handler
    Public Event onAccionRechazar As Accion_Handler
    Public Event onAccionCancelar As Accion_Handler
    Public Event onAccionImprimir As Accion_Handler
    Public Event onAccionAnular As Accion_Handler
    Public Event onAccionVisualizar As Accion_Handler
    Public Event onAccionModificarTerminos As Accion_Handler
    Public Event onAccionSalvarTerminos As Accion_Handler
    Public Event onAccionModificar As Accion_Handler
    Public Event onAccionAgregarDocumento As Accion_Handler
    Public Event onAccionConfigurarNovoReporte As Accion_Handler
    'Public Event onAccionAgregarBulto As Accion_Handler
    'Public Event onAccionAgregarParcial As Accion_Handler

    Protected Sub AccionGuardarClick(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        RaiseEvent onAccionGuardar()
    End Sub

    Protected Sub AccionGuardarConfirmarClick(sender As Object, e As System.EventArgs) Handles btnGuardarConfirmar.Click
        RaiseEvent onAccionGuardarConfirmar()
    End Sub

    Protected Sub AccionConfirmar_Click(sender As Object, e As System.EventArgs) Handles btnConfirmar.Click
        RaiseEvent onAccionConfirmar()
    End Sub

    Protected Sub AccionAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click
        RaiseEvent onAccionAceptar()
    End Sub

    Protected Sub AccionRechazar_Click(sender As Object, e As System.EventArgs) Handles btnRechazar.Click
        RaiseEvent onAccionRechazar()
    End Sub

    Protected Sub AccionImprimir_Click(sender As Object, e As System.EventArgs) Handles btnImprimir.Click
        RaiseEvent onAccionImprimir()
    End Sub

    Protected Sub AccionAgregarDocumento_Click(sender As Object, e As System.EventArgs) Handles btnAgregarDocumento.Click
        RaiseEvent onAccionAgregarDocumento()
    End Sub

    Protected Sub AccionConfigurarNovoReporte_Click(sender As Object, e As System.EventArgs) Handles btnConfigurarNovoReporte.Click
        RaiseEvent onAccionConfigurarNovoReporte()
    End Sub

    'Protected Sub AccionAgregarBulto_Click(sender As Object, e As EventArgs) Handles btnAgregarBulto.Click
    '    RaiseEvent onAccionAgregarBulto()
    'End Sub

    'Protected Sub AccionAgregarParcial_Click(sender As Object, e As EventArgs) Handles btnAgregarParcial.Click
    '    RaiseEvent onAccionAgregarParcial()
    'End Sub

    Protected Sub AccionCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        RaiseEvent onAccionCancelar()
    End Sub

    Protected Sub AccionAnular_Click(sender As Object, e As System.EventArgs) Handles btnAnular.Click
        RaiseEvent onAccionAnular()
    End Sub

    Protected Sub AccionModificar_Click(sender As Object, e As System.EventArgs) Handles btnModificar.Click
        Me.btnCancelar.Text = Traduzir("015_accion_cancelar")
        RaiseEvent onAccionModificar()
    End Sub

    Protected Sub AccionVisualizar_Click(sender As Object, e As System.EventArgs) Handles btnVisualizar.Click
        RaiseEvent onAccionVisualizar()
    End Sub

    Protected Sub AccionModificarTermino_Click(sender As Object, e As System.EventArgs) Handles btnModificarTermino.Click
        RaiseEvent onAccionModificarTerminos()
    End Sub

    Protected Sub AccionSalvarTerminos_Click(sender As Object, e As System.EventArgs) Handles btnSalvarTerminos.Click
        RaiseEvent onAccionSalvarTerminos()
    End Sub

#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub TraduzirControles()
        MyBase.TraduzirControles()
        Me.btnGuardar.Text = Traduzir("015_accion_guardar")

        If MovimientodeAceptacionAutomatica Then
            Me.btnGuardarConfirmar.Text = Traduzir("015_accion_guardaraceptar")
            Me.btnConfirmar.Text = Traduzir("015_accion_aceptar")

            btnGuardarConfirmar.BotonOnClientClick = "return confirm('" & Traduzir("015_accion_guardaraceptar_documento") & "')"
            btnGuardarConfirmar.ImagenOnClientClick = "return confirm('" & Traduzir("015_accion_guardaraceptar_documento") & "')"

        Else
            Me.btnGuardarConfirmar.Text = Traduzir("015_accion_guardarconfirmar")
            Me.btnConfirmar.Text = Traduzir("015_accion_confirmar")

            btnGuardarConfirmar.BotonOnClientClick = "return confirm('" & Traduzir("015_accion_guardarconfirmar_documento") & "')"
            btnGuardarConfirmar.ImagenOnClientClick = "return confirm('" & Traduzir("015_accion_guardarconfirmar_documento") & "')"

        End If
        
        Me.btnAceptar.Text = Traduzir("015_accion_aceptar")
        Me.btnRechazar.Text = Traduzir("015_accion_rechazar")
        Me.btnAnular.Text = Traduzir("015_accion_anular")
        Me.btnImprimir.Text = Traduzir("015_accion_imprimir")
        Me.btnCancelar.Text = Traduzir("015_accion_volver")
        Me.btnModificar.Text = Traduzir("015_accion_modificar")
        Me.btnModificarTermino.Text = Traduzir("015_accion_modificar_terminos")
        Me.btnSalvarTerminos.Text = Traduzir("015_accion_salvar_terminos")
        Me.btnVisualizar.Text = Traduzir("015_accion_visualizar")
        Me.btnAgregarDocumento.Text = Traduzir("015_accion_agregarDocumento")
        Me.btnConfigurarNovoReporte.Text = Traduzir("068_accion_configurarnovoreporte")
        'Me.btnAgregarBulto.Text = Traduzir("015_accion_agregarBulto")
        'Me.btnAgregarParcial.Text = Traduzir("015_accion_agregarParcial")

        btnAnular.BotonOnClientClick = "return confirm('" & Traduzir("015_accion_anular_documento") & "')"
        btnAnular.ImagenOnClientClick = "return confirm('" & Traduzir("015_accion_anular_documento") & "')"



    End Sub

    'Public Overrides Sub ConfigurarTabIndexControle()
    '    MyBase.ConfigurarTabIndexControle()
    '    'Aplicacao.Util.Utilidad.ConfigurarTabIndex(btnGuardar, btnConfirmar, btnAceptar, btnRechazar, _
    '    '                                           btnAnular, btnModificar, btnImprimir, btnVisualizar, btnCancelar, _
    '    '                                           btnAgregarDocumento, btnAgregarBulto, btnAgregarParcial)
    '    Aplicacao.Util.Utilidad.ConfigurarTabIndex(btnGuardar, btnConfirmar, btnAceptar, btnRechazar, _
    '                                               btnAnular, btnModificar, btnImprimir, btnVisualizar, btnCancelar, _
    '                                               btnAgregarDocumento)
    'End Sub

    Public Sub registrarScriptBotonGrabar(script As String)
        If Not String.IsNullOrEmpty(script) Then
            btnGuardar.BotonOnClientClick = "return " & script
            btnGuardar.ImagenOnClientClick = "return " & script
        End If
    End Sub

    Public Sub registrarScriptBotonConfirmar(script As String)
        If Not String.IsNullOrEmpty(script) Then
            btnConfirmar.BotonOnClientClick = "return " & script
            btnConfirmar.ImagenOnClientClick = "return " & script
        End If
    End Sub

    Public Sub registrarScriptBotonImprimir(codigoComprobante As String, nombreReporte As String)
        If Not String.IsNullOrEmpty(codigoComprobante) AndAlso Not String.IsNullOrEmpty(nombreReporte) Then
            btnImprimir.BotonOnClientClick = String.Format("AbrirJanela('DocumentoImpresion.aspx?COD_COMPROBANTE={0}&NombrePopupModal=DocumentoImpresion&IDReporte={1}'); return false;", codigoComprobante, nombreReporte)
        End If
    End Sub

#End Region

End Class