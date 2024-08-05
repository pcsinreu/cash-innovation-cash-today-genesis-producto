Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones

Public Class ucFiltroElementos
    Inherits UcBase

#Region "[PROPRIEDADES]"

    Private _TipoElemento As EnumeradoresPantalla.TipoFiltroElemento
    Public Property TipoElemento() As EnumeradoresPantalla.TipoFiltroElemento
        Get
            Return _TipoElemento
        End Get
        Set(value As EnumeradoresPantalla.TipoFiltroElemento)
            _TipoElemento = value
        End Set
    End Property

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Metodo Inicializar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()
        MyBase.Inicializar()
        ConfigurarVisibilidadeControle()

        If Not IsPostBack Then
            BuscaDefecto()
        End If
    End Sub

    ''' <summary>
    ''' Método que permite o desenvolvedor traduzir os controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()
        MyBase.TraduzirControles()
        Select Case TipoElemento
            Case EnumeradoresPantalla.TipoFiltroElemento.Documento
                lblTitulo.Text = Traduzir("041_titulo_documento")
                lblNumeroExterno.Text = Traduzir("041_lbl_numero_externo")
                lblCodigoComprovante.Text = Traduzir("041_lbl_codigo_comprovante")
            Case EnumeradoresPantalla.TipoFiltroElemento.Contenedor
                lblTitulo.Text = Traduzir("041_titulo_contenedor")
                lblPrecinto.Text = Traduzir("041_lbl_Precinto")
                lblTipoContenedor.Text = Traduzir("041_lbl_tipo_contenedor")
                lblCodigoContenedor.Text = Traduzir("041_lbl_codigo_contenedor")
            Case EnumeradoresPantalla.TipoFiltroElemento.Remesa
                lblTitulo.Text = Traduzir("041_titulo_remesa")
                lblCodigoExterno.Text = Traduzir("041_lbl_codigo_externo")
                lblCodigoRuta.Text = Traduzir("041_lbl_ruta")
            Case EnumeradoresPantalla.TipoFiltroElemento.Bulto
                lblTitulo.Text = Traduzir("041_titulo_bulto")
                lblPrecinto.Text = Traduzir("041_lbl_Precinto")
                lblTipoFormato.Text = Traduzir("041_lbl_formato_bulto")
                lblTipoServicio.Text = Traduzir("041_lbl_tipo_servicio")
            Case EnumeradoresPantalla.TipoFiltroElemento.Parcial
                lblTitulo.Text = Traduzir("041_titulo_parcial")
                lblPrecinto.Text = Traduzir("041_lbl_parcial")
                lblTipoFormato.Text = Traduzir("041_lbl_formato_parcial")
        End Select
        If TipoElemento <> EnumeradoresPantalla.TipoFiltroElemento.Documento Then
            lblFechaAltaDesde.Text = Traduzir("041_lbl_fecha_alta_desde")
            lblFechaAltaHasta.Text = Traduzir("041_lbl_fecha_alta_hasta")
        End If
    End Sub

    ''' <summary>
    ''' Método que valida os campos obrigatorios do controle.
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Function ValidarControl() As List(Of String)
        Dim retorno As New List(Of String)
        Return retorno
    End Function

    ''' <summary>
    ''' Adiciona os scripts
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub AdicionarScripts()
        MyBase.AdicionarScripts()
        Dim script As String = String.Format("AbrirCalendario('{0}','{1}');", _
                                             txtFechaAltaDesde.ClientID, "True")
        script &= String.Format("AbrirCalendario('{0}','{1}');", _
                                     txtFechaAltaHasta.ClientID, "True")
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR" & Me.ID, script, True)
    End Sub

    'Public Overrides Sub ConfigurarTabIndexControle()
    '    MyBase.ConfigurarTabIndexControle()
    '    Aplicacao.Util.Utilidad.ConfigurarTabIndex(txtPrecinto, ddlTipoServicio, ddlTipoFormato, txtFechaAltaDesde, txtFechaAltaHasta, txtNumeroExterno, txtCodigoComprovante, txtCodigoExterno, txtCodigoRuta, txtCodigoContenedor)
    'End Sub

#End Region

#Region "[EVENTOS]"

#End Region

#Region "[METODOS]"

    Public Sub ActualizarFiltros(ByRef objFiltros As Clases.Transferencias.Filtro)
        Select Case TipoElemento
            Case EnumeradoresPantalla.TipoFiltroElemento.Documento

                Dim objFiltroDocumento As New Clases.Transferencias.FiltroDocumento
                objFiltros.Documento = New ObservableCollection(Of Clases.Transferencias.FiltroDocumento)

                If Not String.IsNullOrEmpty(txtNumeroExterno.Text) Then

                    Dim NumeroExterno() As String = txtNumeroExterno.Text.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)

                    If NumeroExterno.Count > 0 Then
                        For Each numExterno In NumeroExterno
                            objFiltros.Documento.Add(New Clases.Transferencias.FiltroDocumento With { _
                                                     .NumeroExterno = numExterno})
                        Next
                    End If

                End If

                If Not String.IsNullOrEmpty(txtCodigoComprovante.Text) Then

                    Dim CodigoComprobante() As String = txtCodigoComprovante.Text.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)

                    If CodigoComprobante.Count > 0 Then
                        For Each codComprobante In CodigoComprobante
                            objFiltros.Documento.Add(New Clases.Transferencias.FiltroDocumento With { _
                                                     .CodigoComprovante = codComprobante})
                        Next
                    End If

                End If

            Case EnumeradoresPantalla.TipoFiltroElemento.Contenedor

                Dim objFiltroContenedor As New Clases.Transferencias.FiltroContenedor

                If Not String.IsNullOrEmpty(txtPrecinto.Text) Then
                    If objFiltroContenedor.Precintos Is Nothing Then
                        objFiltroContenedor.Precintos = New ObservableCollection(Of String)
                    End If
                    objFiltroContenedor.Precintos.Add(txtPrecinto.Text.Trim)
                End If
                If Not String.IsNullOrEmpty(txtFechaAltaDesde.Text) Then
                    objFiltroContenedor.FechaAltaDesde = Convert.ToDateTime(txtFechaAltaDesde.Text.Trim)
                End If
                If Not String.IsNullOrEmpty(txtFechaAltaHasta.Text) Then
                    objFiltroContenedor.FechaAltaHasta = Convert.ToDateTime(txtFechaAltaHasta.Text.Trim)
                End If
                If ddlTipoContenedor.SelectedIndex > 0 Then
                    If objFiltroContenedor.TipoContenedor Is Nothing Then
                        objFiltroContenedor.TipoContenedor = New Comon.Clases.TipoContenedor
                    End If
                    objFiltroContenedor.TipoContenedor.Codigo = ddlTipoContenedor.Items(ddlTipoContenedor.SelectedIndex).Value
                    objFiltroContenedor.TipoContenedor.Descripcion = ddlTipoContenedor.Items(ddlTipoContenedor.SelectedIndex).Text
                End If
                If Not String.IsNullOrEmpty(txtCodigoContenedor.Text) Then
                    objFiltroContenedor.Codigo = txtCodigoContenedor.Text.Trim
                End If

                If VerificaCampoInformado() Then
                    objFiltros.Contenedor = New ObservableCollection(Of Clases.Transferencias.FiltroContenedor)
                    objFiltros.Contenedor.Add(objFiltroContenedor)
                End If

            Case (EnumeradoresPantalla.TipoFiltroElemento.Remesa)

                Dim objFiltroRemesa As New Clases.Transferencias.FiltroRemesa

                If Not String.IsNullOrEmpty(txtFechaAltaDesde.Text) Then
                    objFiltroRemesa.FechaAltaDesde = Convert.ToDateTime(txtFechaAltaDesde.Text.Trim)
                End If
                If Not String.IsNullOrEmpty(txtFechaAltaHasta.Text) Then
                    objFiltroRemesa.FechaAltaHasta = Convert.ToDateTime(txtFechaAltaHasta.Text.Trim)
                End If

                If Not String.IsNullOrEmpty(txtCodigoRuta.Text) Then
                    objFiltroRemesa.CodigoRuta = txtCodigoRuta.Text.Trim
                End If

                If VerificaCampoInformado() Then
                    objFiltros.Remesa = New ObservableCollection(Of Clases.Transferencias.FiltroRemesa)
                    objFiltros.Remesa.Add(objFiltroRemesa)
                End If

                If Not String.IsNullOrEmpty(txtCodigoExterno.Text) Then

                    Dim CodigoExterno() As String = txtCodigoExterno.Text.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)

                    If CodigoExterno.Count > 0 Then
                        For Each codExterno In CodigoExterno
                            objFiltros.Remesa.Add(New Clases.Transferencias.FiltroRemesa With { _
                                                  .CodigoExterno = codExterno})
                        Next
                    End If

                End If

            Case (EnumeradoresPantalla.TipoFiltroElemento.Bulto)

                Dim objFiltroBulto As New Clases.Transferencias.FiltroBulto

                If Not String.IsNullOrEmpty(txtPrecinto.Text) Then
                    If objFiltroBulto.Precintos Is Nothing Then
                        objFiltroBulto.Precintos = New ObservableCollection(Of String)
                    End If

                    Dim PrecintoBulto() As String = txtPrecinto.Text.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    objFiltroBulto.Precintos = (From p In PrecintoBulto Select p).ToObservableCollection

                End If

                If Not String.IsNullOrEmpty(txtFechaAltaDesde.Text) Then
                    objFiltroBulto.FechaAltaDesde = Convert.ToDateTime(txtFechaAltaDesde.Text.Trim)
                End If
                If Not String.IsNullOrEmpty(txtFechaAltaHasta.Text) Then
                    objFiltroBulto.FechaAltaHasta = Convert.ToDateTime(txtFechaAltaHasta.Text.Trim)
                End If
                If ddlTipoServicio.SelectedIndex > 0 Then
                    objFiltroBulto.TipoServicio = Prosegur.Genesis.LogicaNegocio.Genesis.TipoServicio.ObtenerTipoServicioPorIdentificador(ddlTipoServicio.SelectedValue)
                End If
                If ddlTipoFormato.SelectedIndex > 0 Then
                    objFiltroBulto.TipoFormato = Prosegur.Genesis.LogicaNegocio.Genesis.TipoFormato.ObtenerTipoFormatoPorIdentificador(ddlTipoFormato.SelectedValue)
                End If

                If VerificaCampoInformado() Then
                    objFiltros.Bulto = New ObservableCollection(Of Clases.Transferencias.FiltroBulto)
                    objFiltros.Bulto.Add(objFiltroBulto)
                End If

            Case (EnumeradoresPantalla.TipoFiltroElemento.Parcial)

                Dim objFiltroParcial As New Clases.Transferencias.FiltroParcial

                If Not String.IsNullOrEmpty(txtPrecinto.Text) Then
                    If objFiltroParcial.Precintos Is Nothing Then
                        objFiltroParcial.Precintos = New ObservableCollection(Of String)
                    End If

                    Dim PrecintoParcial() As String = txtPrecinto.Text.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    objFiltroParcial.Precintos = (From p In PrecintoParcial Select p).ToObservableCollection

                End If
                If Not String.IsNullOrEmpty(txtFechaAltaDesde.Text) Then
                    objFiltroParcial.FechaAltaDesde = Convert.ToDateTime(txtFechaAltaDesde.Text.Trim)
                End If
                If Not String.IsNullOrEmpty(txtFechaAltaHasta.Text) Then
                    objFiltroParcial.FechaAltaHasta = Convert.ToDateTime(txtFechaAltaHasta.Text.Trim)
                End If
                If ddlTipoFormato.SelectedIndex > 0 Then
                    objFiltroParcial.TipoFormato = Prosegur.Genesis.LogicaNegocio.Genesis.TipoFormato.ObtenerTipoFormatoPorIdentificador(ddlTipoFormato.SelectedValue)
                End If

                If VerificaCampoInformado() Then
                    objFiltros.Parcial = New ObservableCollection(Of Clases.Transferencias.FiltroParcial)
                    objFiltros.Parcial.Add(objFiltroParcial)
                End If
        End Select

    End Sub

    ''' <summary>
    ''' Se foi informado algum campo, retorna True
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function VerificaCampoInformado() As Boolean
        If Not String.IsNullOrEmpty(txtPrecinto.Text) OrElse Not String.IsNullOrEmpty(txtFechaAltaDesde.Text) OrElse _
            Not String.IsNullOrEmpty(txtCodigoExterno.Text) OrElse Not String.IsNullOrEmpty(txtCodigoRuta.Text) OrElse _
            Not String.IsNullOrEmpty(txtNumeroExterno.Text) OrElse Not String.IsNullOrEmpty(txtCodigoComprovante.Text) OrElse _
            Not String.IsNullOrEmpty(txtFechaAltaHasta.Text) OrElse ddlTipoContenedor.SelectedIndex > 0 OrElse _
            ddlTipoFormato.SelectedIndex > 0 OrElse ddlTipoServicio.SelectedIndex > 0 OrElse _
            Not String.IsNullOrEmpty(txtCodigoContenedor.Text) Then
            Return True
        End If
        Return False
    End Function

    Private Function ValidarCamposObligatorios() As Boolean
        If String.IsNullOrEmpty(txtCodigoExterno.Text) Then
            Return True
        End If
    End Function

    Private Sub ConfigurarVisibilidadeControle()
        Select Case TipoElemento
            Case EnumeradoresPantalla.TipoFiltroElemento.Documento
                dvNumeroExterno.Style.Add("display", "block")
                dvCodigoComprovante.Style.Add("display", "block")
            Case EnumeradoresPantalla.TipoFiltroElemento.Contenedor
                dvCodigoContenedor.Style.Add("display", "block")
                dvTipoContenedor.Style.Add("display", "block")
            Case EnumeradoresPantalla.TipoFiltroElemento.Remesa
                dvCodigoExterno.Style.Add("display", "block")
                dvCodigoRuta.Style.Add("display", "block")
            Case EnumeradoresPantalla.TipoFiltroElemento.Bulto
                dvTipoServicio.Style.Add("display", "block")
                dvTipoFormato.Style.Add("display", "block")
            Case EnumeradoresPantalla.TipoFiltroElemento.Parcial
                dvTipoFormato.Style.Add("display", "block")
        End Select
        If TipoElemento <> EnumeradoresPantalla.TipoFiltroElemento.Documento Then
            dvFechaAltaDesde.Style.Add("display", "block")
            dvFechaAltaHasta.Style.Add("display", "block")
            If TipoElemento <> EnumeradoresPantalla.TipoFiltroElemento.Remesa Then
                dvPrecinto.Style.Add("display", "block")
            End If
        End If
    End Sub

    ''' <summary>
    ''' Recibir datos de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CargarFiltro(_TipoElemento As EnumeradoresPantalla.TipoFiltroElemento)
        TipoElemento = _TipoElemento
        CarregarCombo()
    End Sub

    ''' <summary>
    ''' Carrega a combo tipo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CarregarCombo()
        Select Case TipoElemento
            Case EnumeradoresPantalla.TipoFiltroElemento.Contenedor
                Aplicacao.Util.Utilidad.CargarComboTipoContenedor(ddlTipoContenedor)
            Case EnumeradoresPantalla.TipoFiltroElemento.Remesa

            Case EnumeradoresPantalla.TipoFiltroElemento.Bulto
                Aplicacao.Util.Utilidad.CargarComboTipoServicio(ddlTipoServicio)
                Aplicacao.Util.Utilidad.CargarComboTipoFormato(ddlTipoFormato)
            Case EnumeradoresPantalla.TipoFiltroElemento.Parcial
                Aplicacao.Util.Utilidad.CargarComboTipoFormato(ddlTipoFormato)
        End Select
    End Sub

    ''' <summary>
    ''' Limpa os controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Limpiar()
        txtFechaAltaDesde.Text = String.Empty
        txtFechaAltaHasta.Text = String.Empty
        txtPrecinto.Text = String.Empty
        txtNumeroExterno.Text = String.Empty
        txtCodigoContenedor.Text = String.Empty
        txtCodigoRuta.Text = String.Empty
        txtCodigoExterno.Text = String.Empty
        txtCodigoComprovante.Text = String.Empty
        ddlTipoFormato.SelectedValue = Nothing
        ddlTipoServicio.SelectedValue = Nothing
        ddlTipoContenedor.SelectedValue = Nothing

        BuscaDefecto()
    End Sub

    ''' <summary>
    ''' Valores defecto 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub BuscaDefecto()

        If TipoElemento = EnumeradoresPantalla.TipoFiltroElemento.Remesa Then
            txtFechaAltaDesde.Text = Now.AddDays(-1).ToString("dd/MM/yyyy HH:mm:ss")
            txtFechaAltaHasta.Text = Now.AddDays(1).ToString("dd/MM/yyyy HH:mm:ss")
        End If

    End Sub

#End Region

End Class
