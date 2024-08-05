Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones

Public Class ucDatosDocumento
    Inherits UcBase

#Region "BOTONES"

    'Public Event onConsultaSaldo As EventHandler
    'Protected Sub ConsultaSaldo_Click(sender As Object, e As System.EventArgs) Handles btnConsultaSaldos.Click
    '    RaiseEvent onConsultaSaldo(sender, e)
    'End Sub

#End Region

#Region "[PROPRIEDADES]"


    Private _IdentificadorSector As String = Nothing
    Public ReadOnly Property IdentificadorSector() As String
        Get
            If String.IsNullOrEmpty(_IdentificadorSector) Then
                _IdentificadorSector = Request.QueryString("IdentificadorSector")


            End If
            Return _IdentificadorSector
        End Get
    End Property


    Private _SectorSelecionado As Clases.Sector = Nothing
    Public ReadOnly Property SectorSelecionado() As Clases.Sector
        Get
            If _SectorSelecionado Is Nothing Then
                _SectorSelecionado = LogicaNegocio.Genesis.Sector.ObtenerPorOid(IdentificadorSector, cargarCodigosAjenos:=True)
                'Base.InformacionUsuario.DelegacionSeleccionada.Plantas(0).Sectores.FirstOrDefault(Function(n) n.Identificador = IdentificadorSector)
            End If
            Return _SectorSelecionado
        End Get
    End Property


    Private _Documento As Comon.Clases.Documento
    Private _GrupoDocumentos As Comon.Clases.GrupoDocumentos
    Private _CodigoComprobante As String
    Public Property Modo As Comon.Enumeradores.Modo

    Private Enum TipoDatosDocumento
        NoInformado
        Documento
        GrupoDocumento
    End Enum

    Private ReadOnly Property objTipoDatosDocumento() As TipoDatosDocumento
        Get
            If Documento IsNot Nothing Then
                Return TipoDatosDocumento.Documento
            ElseIf GrupoDocumentos IsNot Nothing Then
                Return TipoDatosDocumento.GrupoDocumento
            Else
                Return TipoDatosDocumento.NoInformado
            End If
        End Get
    End Property

    Private ReadOnly Property Estado() As Comon.Enumeradores.EstadoDocumento
        Get
            If objTipoDatosDocumento = TipoDatosDocumento.Documento Then
                Return Documento.Estado
            ElseIf objTipoDatosDocumento = TipoDatosDocumento.GrupoDocumento Then
                Return GrupoDocumentos.Estado
            Else
                Throw New Exception(String.Format("{0} ", Traduzir("034_DatosInvalidos")))
            End If
        End Get
    End Property

    Private Property FechaHoraPlanificacionCertificacion As DateTime
        Get
            If objTipoDatosDocumento = TipoDatosDocumento.Documento Then
                Return Documento.FechaHoraPlanificacionCertificacion
            Else
                Throw New Exception(String.Format("{0} ", Traduzir("034_DatosInvalidos")))
            End If
        End Get
        Set(value As DateTime)
            If (Documento IsNot Nothing) Then
                Documento.FechaHoraPlanificacionCertificacion = value
            End If
        End Set
    End Property

    Private Property FechaHoraGestion As DateTime?
        Get
            If objTipoDatosDocumento = TipoDatosDocumento.Documento Then
                Return Documento.FechaHoraGestion
            Else
                Throw New Exception(String.Format("{0} ", Traduzir("034_DatosInvalidos")))
            End If
        End Get
        Set(value As DateTime?)
            If (Documento IsNot Nothing) Then
                Documento.FechaHoraGestion = value
            End If
        End Set
    End Property

    Private ReadOnly Property EstaCertificado As Boolean
        Get
            If objTipoDatosDocumento = TipoDatosDocumento.Documento Then
                Return Documento.EstaCertificado
            Else
                Throw New Exception(String.Format("{0} ", Traduzir("034_DatosInvalidos")))
            End If
        End Get
    End Property

    Public Property Documento As Comon.Clases.Documento
        Get
            Return _Documento
        End Get
        Set(value As Comon.Clases.Documento)
            If objTipoDatosDocumento = TipoDatosDocumento.GrupoDocumento Then
                Throw New Exception(String.Format("{0} ", Traduzir("034_DatosInvalidos")))
            End If
            _Documento = value
        End Set
    End Property

    Public Property GrupoDocumentos As Comon.Clases.GrupoDocumentos
        Get
            Return _GrupoDocumentos
        End Get
        Set(value As Comon.Clases.GrupoDocumentos)
            If objTipoDatosDocumento = TipoDatosDocumento.Documento Then
                Throw New Exception(String.Format("{0} ", Traduzir("034_DatosInvalidos")))
            End If
            _GrupoDocumentos = value
        End Set
    End Property

    Public ReadOnly Property Formulario() As Clases.Formulario
        Get
            If objTipoDatosDocumento = TipoDatosDocumento.Documento Then
                Return Documento.Formulario
            ElseIf objTipoDatosDocumento = TipoDatosDocumento.GrupoDocumento Then
                Return GrupoDocumentos.Formulario
            Else
                Throw New Exception(String.Format("{0} ", Traduzir("034_DatosInvalidos")))
            End If
        End Get
    End Property

    Public Property NumeroExterno() As String
        Get
            If objTipoDatosDocumento = TipoDatosDocumento.Documento Then
                Return Documento.NumeroExterno
            Else
                Throw New Exception(String.Format("{0} ", Traduzir("034_DatosInvalidos")))
            End If
        End Get
        Set(value As String)
            If (Documento IsNot Nothing) Then
                Documento.NumeroExterno = value
            End If
        End Set
    End Property

    Public Property CodigoComprobante() As String
        Get
            If String.IsNullOrEmpty(_CodigoComprobante) AndAlso objTipoDatosDocumento = TipoDatosDocumento.Documento AndAlso
                Not String.IsNullOrEmpty(Documento.CodigoComprobante) Then
                Return Documento.CodigoComprobante
            ElseIf String.IsNullOrEmpty(_CodigoComprobante) AndAlso objTipoDatosDocumento = TipoDatosDocumento.GrupoDocumento AndAlso
                Not String.IsNullOrEmpty(GrupoDocumentos.CodigoComprobante) Then
                Return GrupoDocumentos.CodigoComprobante
            Else
                Return _CodigoComprobante
            End If
        End Get
        Set(value As String)
            _CodigoComprobante = value
        End Set
    End Property

    Public ReadOnly Property objHistorico() As ObservableCollection(Of Comon.Clases.HistoricoMovimientoDocumento)
        Get
            If objTipoDatosDocumento = TipoDatosDocumento.Documento Then
                Return Documento.Historico
            ElseIf objTipoDatosDocumento = TipoDatosDocumento.GrupoDocumento Then
                Return GrupoDocumentos.Historico
            Else
                Throw New Exception(String.Format("{0} ", Traduzir("034_DatosInvalidos")))
            End If
        End Get
    End Property

    Public ReadOnly Property idCampoNumeroExterno() As String
        Get
            Return txtNumeroExterno.ClientID
        End Get
    End Property

#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub TraduzirControles()

        Me.lblTipoDocumento.Text = String.Format("{0} ", Traduzir("034_tpdocumento"))
        lblEstado.Text = String.Format("{0} ", Traduzir("034_Estado"))
        lblNumeroExterno.Text = String.Format("{0} ", Traduzir("034_NumeroExterno"))
        lblFechaHoraPlanificacionCertificacion.Text = String.Format("{0} ", Traduzir("034_FechaHoraPlanificacionCertificacion"))
        lblCodComprobante.Text = String.Format("{0} ", Traduzir("034_Codigo_Comprobante"))
        litTituloHistorico.Text = String.Format("{0} ", Traduzir("034_HistEstado"))
        lblFechaHoraGestion.Text = Traduzir("028_fecha_hora_gestion")
        btnConsultaSaldos.Attributes("value") = Traduzir("034_Consulta_Saldo")
    End Sub

    Public Overrides Function ValidarControl() As List(Of String)
        Dim retorno As New List(Of String)
        If _Documento IsNot Nothing Then
            If Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.CodigoExternoObligatorio) Then

                If (Me.txtFechaHoraGestion.Enabled AndAlso Me.txtFechaHoraGestion.Visible) Then
                    If String.IsNullOrEmpty(Me.txtFechaHoraGestion.Text) Then
                        retorno.Add(String.Format(Traduzir("msg_campo_obrigatorio"), Me.lblFechaHoraGestion.Text.Replace(":", "")))
                    End If
                End If

                If (Me.txtSecuencia.Enabled AndAlso Me.txtSecuencia.Visible) Then
                    If String.IsNullOrEmpty(Me.txtSecuencia.Text) Then
                        retorno.Add(String.Format(Traduzir("msg_campo_obrigatorio"), Me.lblSecuencia.Text.Replace(":", "")))
                    End If
                End If

                If (Me.txtPrecinto.Enabled AndAlso Me.txtPrecinto.Visible AndAlso Formulario.Codigo = "MAEREC") Then
                    If String.IsNullOrEmpty(Me.txtPrecinto.Text) Then
                        retorno.Add(String.Format(Traduzir("msg_campo_obrigatorio"), Me.lblPrecinto.Text.Replace(":", "")))
                    End If
                End If


            End If
        End If

        Return retorno
    End Function

    'Public Overrides Sub ConfigurarTabIndexControle()
    '    MyBase.ConfigurarTabIndexControle()

    '    Aplicacao.Util.Utilidad.ConfigurarTabIndex(Me.txtNumeroExterno, Me.txtFechaHoraGestion,
    '                            Me.txtFechaHoraPlanificacionCertificacion)
    'End Sub

    Protected Overrides Sub AdicionarScripts()
        If (Me.Modo <> Enumeradores.Modo.Consulta) Then

            Dim script As String = String.Empty
            Dim _formulario As Clases.Formulario = Nothing

            If Documento IsNot Nothing AndAlso Documento.Formulario IsNot Nothing Then
                _formulario = Documento.Formulario
            ElseIf GrupoDocumentos IsNot Nothing AndAlso GrupoDocumentos.Formulario IsNot Nothing Then
                _formulario = GrupoDocumentos.Formulario
            End If

            If Not Comon.Util.ValidarEsGeneracionF22(_formulario) Then
                script = String.Format("AbrirCalendario('{0}','{1}'); ",
                                                 txtFechaHoraPlanificacionCertificacion.ClientID, "True")
            End If

            script += String.Format("AbrirCalendario('{0}','{1}', '1');",
                                                txtFechaHoraGestion.ClientID, "True")

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR", script, True)
        End If

        Dim argumentos As String = "identificadorSector=" & SectorSelecionado.Identificador & "&identificadorPlanta=" & Base.InformacionUsuario.DelegacionSeleccionada.Plantas(0).Identificador & "&Modo=" & Enumeradores.Modo.Consulta.ToString() & "&NombrePopupModal=" & EnumeradoresPantalla.NombrePopupModal.ConsultaSaldo.ToString() & "&ClaveDocumento=" & String.Empty
        Dim direccion As String = ResolveUrl("~/Pantallas/ConsultaSaldo.aspx") & "?EsPopup=True" & If(argumentos Is Nothing, Nothing, "&" & argumentos)
        btnConsultaSaldos.Attributes("onclick") = "$(function(){" & String.Format("AbrirPopupModal('{0}', '{1}', '{2}', {3}, '{4}', {5});", direccion, 600, 1000, Boolean.TrueString.ToLower, "nombre_" & Guid.NewGuid.ToString().Replace("-", ""), Boolean.FalseString.ToLower) & "}); return false;"

        MyBase.AdicionarScripts()
    End Sub

#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If objTipoDatosDocumento <> TipoDatosDocumento.NoInformado Then
            rellenarDatos()
        End If

        If (Not Me.IsPostBack) Then
            If (txtNumeroExterno.Visible) Then
                txtNumeroExterno.Focus()
            End If
        End If

    End Sub

    Protected Sub txtFechaHoraGestion_TextChanged(sender As Object, e As System.EventArgs) Handles txtFechaHoraGestion.TextChanged, txtSecuencia.TextChanged, txtPrecinto.TextChanged
        MontarNumeroExterno()
        AdicionarScripts()
    End Sub

    ''' <summary>
    ''' Trata mensgem de erro dos Controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        NotificarErro(e.Erro)
    End Sub

    Private Sub GridHistorico_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridHistorico.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = Traduzir("038_estado")
            e.Row.Cells(1).Text = Traduzir("038_hora_modificacion")
            e.Row.Cells(2).Text = Traduzir("038_usu_modificacion")
        End If
    End Sub

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Rellenar los datos de la pantalla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub rellenarDatos()

        dvimgFormulario.Style.Item("display") = "block"
        dvDescFormulario.Style.Item("display") = "block"
        dvTipoDocumento.Style.Item("display") = "block"
        dvConsultaSaldos.Style.Item("display") = "block"
        dvEstado.Style.Item("display") = If(Not (Estado = Enumeradores.EstadoDocumento.Nuevo), "block", "none")

        txtDescFormulario.Text = Formulario.Codigo & " - " & Formulario.Descripcion
        txtTipoDocumento.Text = Formulario.TipoDocumento.Descripcion
        lblDescFormulario.Text = String.Format("{0} ({1})", Traduzir("034_formulario"), Formulario.DescripcionCodigoExterno)

        'txtDescFormulario.Style.Add("color", If(Formulario.Color.Name.Substring(0, 1).ToString <> "#", "#" & Formulario.Color.Name, Formulario.Color.Name))
        dvimgFormulario.Style.Add("background", If(Formulario.Color.Name.Substring(0, 1).ToString <> "#", "#" & Formulario.Color.Name, Formulario.Color.Name))

        'Verifica se existe icone para o item.
        If Formulario.Icono IsNot Nothing Then
            Aplicacao.Util.Utilidad.CriarCachePorIdentificador("imagen", Formulario.Identificador, Formulario.Icono)
            litimgFormulario.Text = "<img src='" & String.Format("../Imagem.ashx?id={0}", Formulario.Identificador) & "' name='ImgIcono' alt='" & Formulario.Descripcion & "' style='height:35px; width:35px;' />"
        End If

        txtEstado.Text = Estado.ToString()

        If Estado <> Enumeradores.EstadoDocumento.Nuevo Then
            dvHistEstado.Style.Item("display") = "block"
            imgHistEstado.AlternateText = Traduzir("034_HistEstado")
            rellenarHistorico()
        End If

        txtCodComprobante.Text = CodigoComprobante
        If (Modo = Comon.Enumeradores.Modo.Consulta OrElse Modo = Comon.Enumeradores.Modo.Modificacion) AndAlso Not String.IsNullOrEmpty(CodigoComprobante) Then
            dvCodComprobante.Style.Item("display") = "block"
        End If

        If Not objTipoDatosDocumento = TipoDatosDocumento.GrupoDocumento Then

            dvNumeroExterno.Style.Item("display") = "block"
            dvFechaHoraGestion.Style.Item("display") = "block"
            If String.IsNullOrWhiteSpace(Me.txtNumeroExterno.Text) AndAlso Documento IsNot Nothing AndAlso Documento.NumeroExterno IsNot Nothing Then
                Me.txtNumeroExterno.Text = Documento.NumeroExterno
            Else
                MontarNumeroExterno()
            End If
            If Modo = Enumeradores.Modo.Alta Then

                dvSecuencia.Style.Item("display") = "block"
                txtSecuencia.Text = "1"
                If Formulario.Codigo = "MAEREC" Then
                    dvPrecinto.Style.Item("display") = "block"
                End If
            End If

            'GENPLATINT-425 Cambiar la pantalla Maestro de Documentos
            If (Estado = Enumeradores.EstadoDocumento.Nuevo) Then
                dvEstaCertificado.Style.Item("display") = "none"
            Else
                dvEstaCertificado.Style.Item("display") = "block"
                If Me.Documento.CodigoCertificacionCuentas = "N" Then
                    imgCertificacion.ImageUrl = "~/Imagenes/no_certificado.png"
                    lblEstaCertificado.Text = Traduzir("034_NoCertificado")

                ElseIf Me.Documento.CodigoCertificacionCuentas = "O" Then
                    imgCertificacion.ImageUrl = "~/Imagenes/certificado_parcialmente.png"
                    lblEstaCertificado.Text = Traduzir("034_Certificado_Parcialmente_Origen")

                ElseIf Me.Documento.CodigoCertificacionCuentas = "D" Then
                    imgCertificacion.ImageUrl = "~/Imagenes/certificado_parcialmente.png"
                    lblEstaCertificado.Text = Traduzir("034_Certificado_Parcialmente_Destino")
                Else
                    imgCertificacion.ImageUrl = "~/Imagenes/certificado.png"
                    lblEstaCertificado.Text = Traduzir("034_Certificado")
                End If
            End If

            'Task 5665:Cambios en la pantalla de documento
            If (Estado = Enumeradores.EstadoDocumento.Nuevo) Then
                dvSaldoSuprimido.Style.Item("display") = "none"

            ElseIf Me.Documento.SaldoSuprimido Then

                dvSaldoSuprimido.Style.Item("display") = "block"
                imgSaldoSuprimido.ImageUrl = "~/Imagenes/saldoSuprimido.png"
                imgSaldoSuprimido.AlternateText = Traduzir("034_SaldoSuprimido_image")
                lblSaldoSuprimido.Text = Traduzir("034_SaldoSuprimido")

            End If

            ' 5679: Acreditación y Notificación Online
            If (Estado = Enumeradores.EstadoDocumento.Nuevo) Then
                dvNotificado.Style.Item("display") = "none"

            ElseIf Me.Documento.Notificado Then

                dvNotificado.Style.Item("display") = "block"
                imgNotificado.ImageUrl = "~/Imagenes/Si.png"
                imgNotificado.AlternateText = Traduzir("034_Notificado_image")
                lblNotificado.Text = Traduzir("034_Notificado")

            End If

            'Verifica se a datá não está vazia
            If FechaHoraPlanificacionCertificacion <> DateTime.MinValue Then
                txtFechaHoraPlanificacionCertificacion.Text = FechaHoraPlanificacionCertificacion.ToString("dd/MM/yyyy HH:mm:ss")
            End If

            If FechaHoraGestion IsNot Nothing AndAlso FechaHoraGestion <> DateTime.MinValue Then
                txtFechaHoraGestion.Text = Convert.ToDateTime(FechaHoraGestion).ToString("dd/MM/yyyy HH:mm:ss")
            End If

            If (Estado = Comon.Enumeradores.EstadoDocumento.EnCurso OrElse
                    Estado = Comon.Enumeradores.EstadoDocumento.Nuevo) AndAlso (Modo = Comon.Enumeradores.Modo.Alta _
                    OrElse Modo = Comon.Enumeradores.Modo.Modificacion) Then

                ''txtNumeroExterno.Enabled = If(Modo = Enumeradores.Modo.Consulta, False, True)
                txtFechaHoraPlanificacionCertificacion.Enabled = If(Modo = Enumeradores.Modo.Consulta, False, True)
                txtFechaHoraGestion.Enabled = If(Modo = Enumeradores.Modo.Consulta, False, True)
            End If

            If Formulario IsNot Nothing AndAlso Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Classificacion) Then
                dvConsultaSaldos.Style.Item("display") = "none"
                ' txtNumeroExterno.Enabled = Modo = Enumeradores.Modo.Alta
                txtFechaHoraPlanificacionCertificacion.Enabled = Modo = Enumeradores.Modo.Alta
                txtFechaHoraGestion.Enabled = Modo = Enumeradores.Modo.Alta
            End If

            If Comon.Util.ValidarEsGeneracionF22(Documento.Formulario) Then
                txtFechaHoraPlanificacionCertificacion.Text = String.Empty
                FechaHoraPlanificacionCertificacion = DateTime.MinValue
                txtFechaHoraPlanificacionCertificacion.Enabled = False
                dvFechaHoraPlanificacionCertificacion.Style.Item("display") = "none"
            Else
                dvFechaHoraPlanificacionCertificacion.Style.Item("display") = "block"
            End If

        End If
    End Sub

    Private Sub MontarNumeroExterno()
        Dim numeroExterno As String = String.Empty
        Dim fechaHora As DateTime

        If Not String.IsNullOrEmpty(txtFechaHoraGestion.Text) Then
            DateTime.TryParse(Me.txtFechaHoraGestion.Text, fechaHora)

            numeroExterno = fechaHora.ToString("yyyyMMddHHmmss")
        Else
            numeroExterno = "00000000000000"

        End If

        Dim ajenoSector = SectorSelecionado.CodigosAjenos.FirstOrDefault(Function(x) x.CodigoIdentificador = "MAE")
        If ajenoSector IsNot Nothing Then

            Dim deviceId = ajenoSector.Codigo
            If deviceId.Length >= 5 Then
                deviceId = deviceId.Substring(5)
            End If


            If deviceId.Length > 12 Then
                deviceId = deviceId.Substring(0, 12)
            End If

            numeroExterno = numeroExterno + "_" + deviceId

            numeroExterno = numeroExterno + "_" + Formulario.DescripcionCodigoExterno

            If Not String.IsNullOrEmpty(txtPrecinto.Text) Then
                numeroExterno = numeroExterno + "_" + txtPrecinto.Text
            ElseIf Formulario.Codigo = "MAEREC" Then
                numeroExterno = numeroExterno + "_0"
            End If


            If Not String.IsNullOrEmpty(txtSecuencia.Text) Then
                numeroExterno = numeroExterno + "_" + txtSecuencia.Text
            ElseIf True Then
                numeroExterno = numeroExterno + "_0"
            End If


            txtNumeroExterno.Text = numeroExterno
            Documento.NumeroExterno = numeroExterno

        End If

    End Sub




    Private Sub rellenarHistorico()

        Dim objHistoricoUC As New List(Of HistoricoMovimientoDocumentoGrid)

        If objHistorico IsNot Nothing AndAlso objHistorico.Count > 0 Then
            For Each obj In objHistorico
                Dim objHistUc As New HistoricoMovimientoDocumentoGrid
                objHistUc.Estado = obj.Estado.ToString
                objHistUc.FechaHoraModificacion = obj.FechaHoraModificacion
                objHistUc.UsuarioModificacion = obj.UsuarioModificacion
                objHistoricoUC.Add(objHistUc)
            Next
        End If

        GridHistorico.DataSource = objHistoricoUC
        GridHistorico.DataBind()
    End Sub

    Public Sub GuardarDatos()
        Dim fechaHora As DateTime
        DateTime.TryParse(Me.txtFechaHoraPlanificacionCertificacion.Text, fechaHora)
        Me.FechaHoraPlanificacionCertificacion = fechaHora
        Me.NumeroExterno = txtNumeroExterno.Text

        'se data estiver vazia então inclui a data atual
        If String.IsNullOrEmpty(txtFechaHoraGestion.Text) Then
            txtFechaHoraGestion.Text = DateTime.UtcNow.QuieroExibirEstaFechaEnLaPatalla(Base.InformacionUsuario.DelegacionSeleccionada).ToString("dd/MM/yyyy HH:mm:ss")
        End If

        DateTime.TryParse(Me.txtFechaHoraGestion.Text, fechaHora)
        Me.FechaHoraGestion = fechaHora
    End Sub

#End Region

End Class

Public Class HistoricoMovimientoDocumentoGrid

    'Classe para converter o HistoricoMovimientoDocumento
    'pois a propiedade estado não é reconhecida no DataSource do grid (apenas os tipos nativos, string, etc)
    'e também se adicionar alguma propriedade na classe HistoricoMovimientoDocumento não aparecer no grid, pois o grid está autogenerate

    Public Property Estado As String
    Public Property FechaHoraModificacion As DateTime
    Public Property UsuarioModificacion As String

End Class