Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.LogicaNegocio
Imports Prosegur.Genesis.Unificacion
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.NuevoSaldos.Web.HelperDocumento
Imports Parametros = Prosegur.Genesis.Web.Login.Parametros
Imports Prosegur.Genesis
Imports System.Collections.ObjectModel

Public Class GrupoDocumento
    Inherits Base

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
                _SectorSelecionado = Prosegur.Genesis.LogicaNegocio.Genesis.Sector.ObtenerPorOid(IdentificadorSector)
            End If
            Return _SectorSelecionado
        End Get
    End Property



    Private _GrupoDocumentos As Clases.GrupoDocumentos
    Public Property GrupoDocumentos() As Clases.GrupoDocumentos
        Get
            If _GrupoDocumentos Is Nothing Then
                _GrupoDocumentos = ViewState("_GrupoDocumentos")
            End If
            Return _GrupoDocumentos
        End Get
        Set(value As Clases.GrupoDocumentos)
            _GrupoDocumentos = value
            ViewState("_GrupoDocumentos") = _GrupoDocumentos
        End Set
    End Property

    Public ReadOnly Property CaracteristicasFormulario() As List(Of Enumeradores.CaracteristicaFormulario)
        Get
            Return GrupoDocumentos.Formulario.Caracteristicas
        End Get
    End Property

    Private _ClaveDocumentoPopup As String = Nothing
    Public Property ClaveDocumentoPopup() As String
        Get
            If String.IsNullOrEmpty(_ClaveDocumentoPopup) Then
                _ClaveDocumentoPopup = ViewState("_ClaveDocumentoPopup")
            End If
            Return _ClaveDocumentoPopup
        End Get
        Set(value As String)
            _ClaveDocumentoPopup = value
            ViewState("_ClaveDocumentoPopup") = value
        End Set
    End Property

    Private _ClaveDocumento As String = Nothing
    Public Property ClaveDocumento() As String
        Get
            If String.IsNullOrEmpty(_ClaveDocumento) Then
                _ClaveDocumento = ViewState("_ClaveDocumento")
            End If
            Return _ClaveDocumento
        End Get
        Set(value As String)
            _ClaveDocumento = value
            ViewState("_ClaveDocumento") = value
        End Set
    End Property

    Private _Modo As Enumeradores.Modo?
    Public ReadOnly Property Modo() As Enumeradores.Modo
        Get
            If Not _Modo.HasValue Then
                _Modo = [Enum].Parse(GetType(Enumeradores.Modo), Request.QueryString("Modo"), True)
            End If
            Return _Modo.Value
        End Get
    End Property

    Private WithEvents _IdentificadorGrupoDocumento As String = Nothing
    Public ReadOnly Property IdentificadorGrupoDocumento() As String
        Get
            If String.IsNullOrEmpty(_IdentificadorGrupoDocumento) Then
                _IdentificadorGrupoDocumento = Request.QueryString("IdentificadorGrupoDocumentos").Trim()
            End If
            Return _IdentificadorGrupoDocumento
        End Get
    End Property

    Private WithEvents _DatosDocumento As ucDatosDocumento
    Public ReadOnly Property DatosDocumento() As ucDatosDocumento
        Get
            If _DatosDocumento Is Nothing Then
                _DatosDocumento = LoadControl("~\Controles\ucDatosDocumento.ascx")
                _DatosDocumento.ID = "DatosDocumento"
                _DatosDocumento.Attributes.Add("identificadorSector", SectorSelecionado.Identificador)
                AddHandler _DatosDocumento.Erro, AddressOf ErroControles
                phDatosDocumento.Controls.Add(_DatosDocumento)
            End If
            Return _DatosDocumento
        End Get
    End Property

    Private WithEvents _CuentaOrigen As ucCuenta
    Public ReadOnly Property CuentaOrigen() As ucCuenta
        Get
            If _CuentaOrigen Is Nothing Then
                _CuentaOrigen = LoadControl("~\Controles\ucCuenta.ascx")
                _CuentaOrigen.ID = "CuentaOrigen"
                _CuentaOrigen.Attributes.Add("identificadorSector", SectorSelecionado.Identificador)
                AddHandler _CuentaOrigen.Erro, AddressOf ErroControles
                If phCuentaOrigen.Controls.Count = 0 Then
                    phCuentaOrigen.Controls.Add(_CuentaOrigen)
                End If
            End If
            Return _CuentaOrigen
        End Get
    End Property

    Private WithEvents _CuentaDestino As ucCuenta
    Public ReadOnly Property CuentaDestino() As ucCuenta
        Get
            If _CuentaDestino Is Nothing Then
                _CuentaDestino = LoadControl("~\Controles\ucCuenta.ascx")
                _CuentaDestino.ID = "CuentaDestino"
                _CuentaDestino.Attributes.Add("identificadorSector", SectorSelecionado.Identificador)
                AddHandler _CuentaDestino.Erro, AddressOf ErroControles
                phCuentaDestino.Controls.Add(_CuentaDestino)
            End If
            Return _CuentaDestino
        End Get
    End Property

    Private WithEvents _ContainerDocumentos As ucContainerDocumentos
    Public Property ContainerDocumentos() As ucContainerDocumentos
        Get
            If _ContainerDocumentos Is Nothing Then
                _ContainerDocumentos = New ucContainerDocumentos
                _ContainerDocumentos = LoadControl("~/Controles/ucContainerDocumentos.ascx")
                _ContainerDocumentos.ID = "ListaElemento"
                _ContainerDocumentos.Attributes.Add("identificadorSector", SectorSelecionado.Identificador)
                AddHandler _ContainerDocumentos.Erro, AddressOf ErroControles
                phContainerDocumentos.Controls.Add(_ContainerDocumentos)
            End If
            Return _ContainerDocumentos
        End Get
        Set(value As ucContainerDocumentos)
            _ContainerDocumentos = value
        End Set
    End Property

    Private WithEvents _ListaSimplificadaDeDocumentos As ucListaDocumentos
    Public Property ListaSimplificadaDeDocumentos() As ucListaDocumentos
        Get
            If _ListaSimplificadaDeDocumentos Is Nothing Then
                _ListaSimplificadaDeDocumentos = New ucListaDocumentos
                _ListaSimplificadaDeDocumentos = LoadControl("~/Controles/ucListaDocumentos.ascx")
                _ListaSimplificadaDeDocumentos.ID = "ListaSimplificadaDeDocumentos"
                _ListaSimplificadaDeDocumentos.Attributes.Add("identificadorSector", SectorSelecionado.Identificador)
                AddHandler _ListaSimplificadaDeDocumentos.Erro, AddressOf ErroControles
                phListaSimplificadaDeDocumentos.Controls.Add(_ListaSimplificadaDeDocumentos)
            End If
            Return _ListaSimplificadaDeDocumentos
        End Get
        Set(value As ucListaDocumentos)
            _ListaSimplificadaDeDocumentos = value
        End Set
    End Property

    Private WithEvents _ListaContenedoresGrp As ucListaContenedoresGrp
    Public Property ListaContenedoresGrp() As ucListaContenedoresGrp
        Get
            If _ListaContenedoresGrp Is Nothing Then
                _ListaContenedoresGrp = New ucListaContenedoresGrp
                _ListaContenedoresGrp = LoadControl("~/Controles/ucListaContenedoresGrp.ascx")
                _ListaContenedoresGrp.ID = "ListaContenedoresGrp"
                AddHandler _ListaContenedoresGrp.Erro, AddressOf ErroControles
                phListaContenedoresGrp.Controls.Add(_ListaContenedoresGrp)
            End If
            Return _ListaContenedoresGrp
        End Get
        Set(value As ucListaContenedoresGrp)
            _ListaContenedoresGrp = value
        End Set
    End Property

    Private WithEvents _InfAdicionales As ucInfAdicionales
    Public ReadOnly Property InfAdicionales() As ucInfAdicionales
        Get
            If _InfAdicionales Is Nothing Then
                _InfAdicionales = LoadControl("~\Controles\ucInfAdicionales.ascx")
                _InfAdicionales.ID = "InfAdicionales"
                AddHandler _InfAdicionales.Erro, AddressOf ErroControles
                phInfAdicionales.Controls.Add(_InfAdicionales)
            End If
            Return _InfAdicionales
        End Get
    End Property

    Private WithEvents _Acciones As ucAcciones
    Public ReadOnly Property Acciones() As ucAcciones
        Get
            If _Acciones Is Nothing Then
                _Acciones = LoadControl("~\Controles\UcAcciones.ascx")
                _Acciones.ID = "Acciones"
                AddHandler _Acciones.Erro, AddressOf ErroControles
                phAcciones.Controls.Add(_Acciones)
            End If
            Return _Acciones
        End Get
    End Property

    Private _SeImprime As Boolean
    Public Property SeImprime() As Boolean
        Get
            If String.IsNullOrEmpty(ViewState("SeImprime")) Then
                If String.IsNullOrEmpty(Request.QueryString("SeImprime")) Then
                    _SeImprime = False
                Else
                    _SeImprime = Convert.ToBoolean(Request.QueryString("SeImprime"))
                End If
            Else
                _SeImprime = Convert.ToBoolean(ViewState("SeImprime"))
            End If

            Return _SeImprime
        End Get
        Set(value As Boolean)
            ViewState("SeImprime") = value
            _SeImprime = value
        End Set
    End Property

    Private _SectorHijo As Boolean
    Public ReadOnly Property SectorHijo() As Boolean
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("SectorHijo")) Then
                _SectorHijo = Convert.ToBoolean(Request.QueryString("SectorHijo"))
            End If
            Return _SectorHijo
        End Get
    End Property

    Public ReadOnly Property bol_gestion_bulto() As Boolean
        Get
            If GrupoDocumentos IsNot Nothing AndAlso GrupoDocumentos.Formulario IsNot Nothing AndAlso GrupoDocumentos.Formulario.Caracteristicas IsNot Nothing AndAlso
                GrupoDocumentos.Formulario.Caracteristicas.Count > 0 AndAlso GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeBultos) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Permite definir parametros da base antes de validar acessos e chamar o método inicializar.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DefinirParametrosBase()
        Try
            MyBase.DefinirParametrosBase()
            MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.GRUPO_DOCUMENTO
            MyBase.ValidarAcesso = True
            MyBase.ValidarPemissaoAD = True
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    ''' <summary>
    ''' Método que permite o desenvolvedor traduzir os controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()
        Try
            MyBase.TraduzirControles()
            Master.Titulo = String.Format("{0} ", Traduzir("032_Titulo"))
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Protected Overrides Sub Inicializar()
        Try
            MyBase.Inicializar()
            If Not Page.IsPostBack Then
                Select Case Modo
                    Case Enumeradores.Modo.Alta
                        ModoAlta()
                    Case Enumeradores.Modo.Modificacion, Enumeradores.Modo.Consulta
                        ModoModificacion()

                        If SeImprime Then
                            SeImprime = False
                            Acciones_onAccionImprimir()
                        End If
                End Select
            End If
            ConfigurarControles()
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

#End Region

#Region "[EVENTOS]"

    Private Sub GrupoDocumento_PopupCerrado(nombrePopup As String, argumento As String) Handles Me.PopupCerrado
        Try
            Dim accionPopupCerrado As EnumeradoresPantalla.AccionPopupCerrado = [Enum].Parse(GetType(EnumeradoresPantalla.AccionPopupCerrado), argumento)
            Dim objDocumento As Clases.Documento

            If nombrePopup = "ContainerDocumentos" OrElse nombrePopup = "ContainerDocumentosView" Then

                If (GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeFondos) AndAlso
                GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.MovimientodeFondos) AndAlso
                (GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.EntreClientes) OrElse
                GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.EntreCanales))) OrElse
                   GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeContenedores) Then
                    objDocumento = Me.BorrarCache(ClaveDocumento)
                    ListaSimplificadaDeDocumentos.DocumentosSeleccionados = GrupoDocumentos.Documentos
                Else
                    objDocumento = Me.BorrarCache(ContainerDocumentos.ClaveDocumento)
                    ContainerDocumentos.ActualizarDocumento(objDocumento, nombrePopup)
                End If

                ConfigurarContainerDocumentos()
            Else
                If ClaveDocumento IsNot Nothing Then
                    objDocumento = Me.BorrarCache(ClaveDocumento)
                    If objDocumento IsNot Nothing Then
                        Select Case accionPopupCerrado
                            Case EnumeradoresPantalla.AccionPopupCerrado.Guardar, EnumeradoresPantalla.AccionPopupCerrado.Anular



                                If GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeFondos) Then


                                    Dim _doc As Clases.Documento = GrupoDocumentos.Documentos.Find(Function(value As Clases.Documento)
                                                                                                       Return value.Identificador = objDocumento.Identificador
                                                                                                   End Function)

                                    If _doc IsNot Nothing Then
                                        GrupoDocumentos.Documentos.Remove(GrupoDocumentos.Documentos.Find(Function(value As Clases.Documento)
                                                                                                              Return value.Identificador = objDocumento.Identificador
                                                                                                          End Function))
                                        GrupoDocumentos.Documentos.Add(objDocumento)

                                    Else

                                        'Dim _docCuenta As Clases.Documento = GrupoDocumentos.Documentos.FirstOrDefault(Function(x) x.CuentaOrigen.Identificador = objDocumento.CuentaOrigen.Identificador AndAlso x.CuentaDestino.Identificador = objDocumento.CuentaDestino.Identificador)

                                        'If _docCuenta IsNot Nothing Then

                                        '    For Each _div In objDocumento.Divisas
                                        '        _docCuenta.Divisas.Add(_div)
                                        '    Next
                                        '    Comon.Util.UnificaItemsDivisas_v2(_docCuenta.Divisas)
                                        'Else
                                        GrupoDocumentos.Documentos.Add(objDocumento)
                                        'End If

                                    End If


                                Else
                                    GrupoDocumentos.Documentos.Remove(GrupoDocumentos.Documentos.Find(Function(value As Clases.Documento)
                                                                                                          Return value.Identificador = objDocumento.Identificador
                                                                                                      End Function))
                                    GrupoDocumentos.Documentos.Add(objDocumento)
                                End If

                                ConfigurarContainerDocumentos()
                        End Select

                    End If
                End If


            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub ListaSimplificadaDeDocumentos_DetallarDocumento(sender As Object, e As ucListaDocumentos.DetallarDocumentoEventArgs)
        Try

            Dim _documento As New Clases.Documento

            If GrupoDocumentos.Documentos IsNot Nothing AndAlso GrupoDocumentos.Documentos.Count > 0 Then
                _documento = GrupoDocumentos.Documentos.FirstOrDefault(Function(x) x.Identificador = e.IdentificadorDocumento)
                If _documento IsNot Nothing Then
                    ClaveDocumento = DirectCast(Page, Base).RegistrarCache(_documento)
                    DirectCast(Page, Base).AbrirPopup("~/Pantallas/Documento.aspx", "ClaveDocumento=" & ClaveDocumento & "&Modo=" & Modo.ToString() & "&esGrupoDocumento=True&NombrePopupModal=ContainerDocumentosView")
                End If
            End If
            ConfigurarContainerDocumentos()

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub ListaContenedoresGrp_DetallarDocumento(sender As Object, e As ucListaContenedoresGrp.DetallarDocumentoEventArgs)
        Try

            Dim _documento As New Clases.Documento

            If GrupoDocumentos.Documentos IsNot Nothing AndAlso GrupoDocumentos.Documentos.Count > 0 Then
                _documento = GrupoDocumentos.Documentos.FirstOrDefault(Function(x) x.Identificador = e.IdentificadorDocumento)
                If _documento IsNot Nothing Then
                    ClaveDocumento = DirectCast(Page, Base).RegistrarCache(_documento)
                    DirectCast(Page, Base).AbrirPopup("~/Pantallas/Documento.aspx", "ClaveDocumento=" & ClaveDocumento & "&Modo=" & Modo.ToString() & "&esGrupoDocumento=True&NombrePopupModal=ContainerDocumentosView")
                End If
            End If
            ConfigurarContainerDocumentos()

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub ListaSimplificadaDeDocumentos_QuitarDocumento(sender As Object, e As ucListaDocumentos.DetallarDocumentoEventArgs)
        Try
            If GrupoDocumentos.Documentos IsNot Nothing AndAlso GrupoDocumentos.Documentos.Count > 0 Then
                GrupoDocumentos.Documentos.Remove(GrupoDocumentos.Documentos.FirstOrDefault(Function(x) x.Identificador = e.IdentificadorDocumento))
            End If
            ConfigurarContainerDocumentos()

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub DatosDocumento_ControleAtualizado(sender As Object, e As System.EventArgs)
        Try
            ' O que tem que ser atualizado ???

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub CuentaDestino_ControleAtualizado(sender As Object, e As System.EventArgs)
        Try
            If CuentaDestino.Cuenta IsNot Nothing Then
                GrupoDocumentos.CuentaDestino = CuentaDestino.Cuenta
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub CuentaOrigen_ControleAtualizado(sender As Object, e As System.EventArgs)
        Try
            'If CuentaOrigen.Cuenta IsNot Nothing Then
            '    GrupoDocumentos.CuentaOrigen = CuentaOrigen.Cuenta
            'End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub

    Private Sub DatosDocumento_ConsultarSaldo()
        Try

            Me.AbrirPopup("~/Pantallas/ConsultaSaldo.aspx", "identificadorSector=" & SectorSelecionado.Identificador & "&identificadorPlanta=" & Base.InformacionUsuario.DelegacionSeleccionada.Plantas(0).Identificador & "&Modo=" & Enumeradores.Modo.Consulta.ToString())

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

#End Region

#Region "[METODOS]"


    ''' <summary>
    ''' Trata mensgem de erro dos Controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagemErro(If(TypeOf e.Erro Is Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub

    ''' <summary>
    ''' Recupera las informaciones necesarias para la Alta de un documento
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModoAlta()

        Dim TiempoInicial As DateTime = Now
        Dim Tiempo As DateTime = Now
        Dim log As New StringBuilder

        GrupoDocumentos = GenesisSaldos.MaestroGrupoDocumentos.CrearGrupoDocumentos(GenesisSaldos.MaestroFormularios.ObtenerFormulario(Request.QueryString("IdentificadorFormulario").Trim()))
        GrupoDocumentos.CuentaOrigen = New Clases.Cuenta With {.Sector = SectorSelecionado}
        GrupoDocumentos.CuentaDestino = GrupoDocumentos.CuentaOrigen.Clonar()

        If GrupoDocumentos.GrupoTerminosIAC Is Nothing Then
            GrupoDocumentos.GrupoTerminosIAC = If(GrupoDocumentos.Formulario.GrupoTerminosIACGrupo IsNot Nothing, GrupoDocumentos.Formulario.GrupoTerminosIACGrupo, Nothing)
        End If

        log.AppendLine("Tiempo 'ModoAlta': " & Now.Subtract(Tiempo).ToString() & "; ")
    End Sub

    ''' <summary>
    ''' Recupera las informaciones necesarias para la Modificación de un documento
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModoModificacion()

        Dim TiempoInicial As DateTime = Now
        Dim Tiempo As DateTime = Now
        Dim log As New StringBuilder

        If Session("GrupoDocumentos_" & IdentificadorGrupoDocumento) IsNot Nothing Then
            GrupoDocumentos = Session("GrupoDocumentos_" & IdentificadorGrupoDocumento)
            Session("GrupoDocumentos_" & IdentificadorGrupoDocumento) = Nothing
        Else
            GrupoDocumentos = GenesisSaldos.MaestroGrupoDocumentos.recuperarGrupoDocumentos(IdentificadorGrupoDocumento, Parametros.Permisos.Usuario.Login, Nothing)
        End If
        log.AppendLine("Tiempo 'ModoModificacion': " & Now.Subtract(Tiempo).ToString() & "; ")

        If GrupoDocumentos IsNot Nothing Then
            RemoverBultosErrados(GrupoDocumentos.Documentos)
        End If
    End Sub

    Private Sub RemoverBultosErrados(documentos As ObservableCollection(Of Clases.Documento))

        If documentos IsNot Nothing Then
            For Each doc In documentos
                If doc.Elemento IsNot Nothing AndAlso doc.Elemento.GetType() Is GetType(Clases.Remesa) Then
                    Dim remesa = DirectCast(doc.Elemento, Clases.Remesa)
                    If remesa.Bultos IsNot Nothing Then
                        remesa.Bultos.RemoveAll(Function(b) b.IdentificadorDocumento <> doc.Identificador)
                    End If
                End If
            Next
        End If

    End Sub

    ''' <summary>
    ''' Valida si todas las informaciones necesarias fueran informadas
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidarInformaciones() As Boolean

        Try

            If GrupoDocumentos Is Nothing OrElse GrupoDocumentos.Formulario Is Nothing OrElse GrupoDocumentos.Formulario.Caracteristicas Is Nothing _
                OrElse GrupoDocumentos.Formulario.Caracteristicas.Count < 1 OrElse
                Not GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.DocumentoEsDelTipoGrupo) Then

                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                 Traduzir("032_InformacionesInvalidas"))
            End If

            TratamientoFormulario(GrupoDocumentos.Formulario)

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao)
            Return False
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
            Return False
        End Try

        Return True
    End Function

    Public Sub TratamientoFormulario(_objFormulario As Clases.Formulario)
        Dim Mensajes As New StringBuilder

        If String.IsNullOrEmpty(_objFormulario.Identificador) Then
            Mensajes.Append(String.Format(Traduzir("008_campo_vazio"), "Identificador", "Formulario") & vbCrLf)
        End If
        If String.IsNullOrEmpty(_objFormulario.Codigo) Then
            Mensajes.Append(String.Format(Traduzir("008_campo_vazio"), "Codigo", "Formulario") & vbCrLf)
        End If
        If String.IsNullOrEmpty(_objFormulario.Descripcion) Then
            Mensajes.Append(String.Format(Traduzir("008_campo_vazio"), Traduzir("008_desformulario"), "Formulario") & vbCrLf)
        End If
        If IsDBNull(_objFormulario.Color) Then
            Mensajes.Append(String.Format(Traduzir("008_campo_vazio"), Traduzir("008_codcolor"), "Formulario") & vbCrLf)
        End If
        If String.IsNullOrEmpty(_objFormulario.TipoDocumento.Descripcion) Then
            Mensajes.Append(String.Format(Traduzir("008_campo_vazio"), Traduzir("008_destpdocumento"), "TipoDocumento") & vbCrLf)
        End If
        If String.IsNullOrEmpty(_objFormulario.TipoDocumento.Codigo) Then
            Mensajes.Append(String.Format(Traduzir("008_campo_vazio"), "Codigo", "TipoDocumento"))
        End If

        If Mensajes.Length > 0 Then
            Throw New Exception(Mensajes.ToString)
        End If

    End Sub

    ''' <summary>
    ''' Configurar los Controles en la pantallas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConfigurarControles()

        ValidarInformaciones()
        ConfigurarDatosDocumento()
        ConfigurarAcciones()
        ConfigurarFiltro()
        ConfigurarContainerDocumentos()
        ConfiguraCuentaOrigen()
        ConfigurarCuentaDestino()
        ConfiguraInfAdicionales()

    End Sub

    ''' <summary>
    ''' Configuración del Componente DatosDocumento - Guarda las informaciones pincipales del Documento
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConfigurarDatosDocumento()
        AddHandler DatosDocumento.ControleAtualizado, AddressOf DatosDocumento_ControleAtualizado
        DatosDocumento.GrupoDocumentos = GrupoDocumentos
        DatosDocumento.Modo = Modo
        'AddHandler DatosDocumento.onConsultaSaldo, AddressOf DatosDocumento_ConsultarSaldo
    End Sub

    Private Sub ConfiguraCuentaOrigen()

        If Caracteristicas.Util.VerificarCaracteristicas(CaracteristicasFormulario,
                New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                        Enumeradores.CaracteristicaFormulario.GestiondeRemesas,
                        Enumeradores.CaracteristicaFormulario.GestiondeBultos,
                        Enumeradores.CaracteristicaFormulario.Cierres,
                        Enumeradores.CaracteristicaFormulario.GestiondeFondos,
                        Enumeradores.CaracteristicaFormulario.GestiondeContenedores,
                        Enumeradores.CaracteristicaFormulario.OtrosMovimientos)) Then


            ' Configuração padrão para todos cénarios
            AddHandler Me.CuentaOrigen.ControleAtualizado, AddressOf CuentaOrigen_ControleAtualizado
            Me.CuentaOrigen.TipoSitio = Enumeradores.TipoSitio.Origen
            Me.CuentaOrigen.Modo = Enumeradores.Modo.Consulta
            Me.CuentaOrigen.SelecaoMultipla = False
            Me.CuentaOrigen.ucSector.identificadorFormulario = GrupoDocumentos.Formulario.Identificador
            If GrupoDocumentos.CuentaOrigen IsNot Nothing Then
                Me.CuentaOrigen.Cuenta = GrupoDocumentos.CuentaOrigen.Clonar()
            End If

            Me.CuentaOrigen.ucSector.PlantaHabilitado = False
            Me.CuentaOrigen.ucSector.DelegacionHabilitado = False
            Me.CuentaOrigen.ucSector.SectorHabilitado = False

            Me.CuentaOrigen.ucCliente.NoExhibirCliente = True
            Me.CuentaOrigen.ucCliente.NoExhibirSubCliente = True
            Me.CuentaOrigen.ucCliente.NoExhibirPtoServicio = True

            Me.CuentaOrigen.ucCanal.NoExhibirCanal = True
            Me.CuentaOrigen.ucCanal.NoExhibirSubCanal = True


        End If

    End Sub

    ''' <summary>
    ''' Configuración del Componente Cuenta - Recupera la Cuenta Destino configurada por el usuario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConfigurarCuentaDestino()

        If Caracteristicas.Util.VerificarCaracteristicas(CaracteristicasFormulario,
            New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                    Enumeradores.CaracteristicaFormulario.GestiondeRemesas,
                    Enumeradores.CaracteristicaFormulario.GestiondeBultos)) AndAlso
                    CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Reenvios) Then

            AddHandler Me.CuentaDestino.ControleAtualizado, AddressOf CuentaDestino_ControleAtualizado
            CuentaDestino.TipoSitio = Enumeradores.TipoSitio.Destino
            CuentaDestino.Modo = Modo
            CuentaDestino.SelecaoMultipla = False
            If GrupoDocumentos.CuentaDestino Is Nothing Then
                GrupoDocumentos.CuentaDestino = GrupoDocumentos.CuentaOrigen.Clonar()
            End If
            Me.CuentaDestino.Cuenta = GrupoDocumentos.CuentaDestino
            Me.CuentaDestino.ucSector.identificadorFormulario = GrupoDocumentos.Formulario.Identificador

            Me.CuentaDestino.ucSector.SectorHabilitado = (Modo <> Enumeradores.Modo.Consulta)

            If (Me.Modo = Enumeradores.Modo.Alta) AndAlso Not IsPostBack Then
                GrupoDocumentos.CuentaDestino.Sector.Identificador = Nothing
                GrupoDocumentos.CuentaDestino.Sector.Descripcion = Nothing
                GrupoDocumentos.CuentaDestino.Sector.Codigo = Nothing
            End If

            If CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.EntreSectoresdePlantasDelegacionesDiferentes) Then

                Me.CuentaDestino.ucSector.PlantaHabilitado = (Modo <> Enumeradores.Modo.Consulta)
                Me.CuentaDestino.ucSector.DelegacionHabilitado = (Modo <> Enumeradores.Modo.Consulta)
                If (Me.Modo = Enumeradores.Modo.Alta) AndAlso Not IsPostBack Then
                    GrupoDocumentos.CuentaDestino.Sector = New Clases.Sector
                End If
            End If

            ' Controle Cliente
            Me.CuentaDestino.ucCliente.NoExhibirCliente = True
            Me.CuentaDestino.ucCliente.NoExhibirSubCliente = True
            Me.CuentaDestino.ucCliente.NoExhibirPtoServicio = True
            ' Controle Canal
            Me.CuentaDestino.ucCanal.NoExhibirCanal = True
            Me.CuentaDestino.ucCanal.NoExhibirSubCanal = True

        ElseIf CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Cierres) AndAlso
            CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.CierreDeCaja) Then

            AddHandler Me.CuentaDestino.ControleAtualizado, AddressOf CuentaDestino_ControleAtualizado
            CuentaDestino.TipoSitio = Enumeradores.TipoSitio.Destino
            CuentaDestino.Modo = Modo
            CuentaDestino.SelecaoMultipla = False
            If GrupoDocumentos.CuentaDestino Is Nothing Then
                GrupoDocumentos.CuentaDestino = GrupoDocumentos.CuentaOrigen.Clonar()
            End If
            Me.CuentaDestino.Cuenta = GrupoDocumentos.CuentaDestino
            Me.CuentaDestino.ucSector.identificadorFormulario = GrupoDocumentos.Formulario.Identificador

            Me.CuentaDestino.ucSector.SectorHabilitado = (Modo <> Enumeradores.Modo.Consulta)

            If (Me.Modo = Enumeradores.Modo.Alta) AndAlso Not IsPostBack Then
                GrupoDocumentos.CuentaDestino.Sector.Identificador = Nothing
                GrupoDocumentos.CuentaDestino.Sector.Descripcion = Nothing
                GrupoDocumentos.CuentaDestino.Sector.Codigo = Nothing
            End If

            If CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.EntreSectoresdePlantasDelegacionesDiferentes) Then

                Me.CuentaDestino.ucSector.PlantaHabilitado = (Modo <> Enumeradores.Modo.Consulta)
                Me.CuentaDestino.ucSector.DelegacionHabilitado = (Modo <> Enumeradores.Modo.Consulta)
                If (Me.Modo = Enumeradores.Modo.Alta) AndAlso Not IsPostBack Then
                    GrupoDocumentos.CuentaDestino.Sector = New Clases.Sector
                End If

            End If

            ' Controle Cliente
            Me.CuentaDestino.ucCliente.NoExhibirCliente = True
            Me.CuentaDestino.ucCliente.NoExhibirSubCliente = True
            Me.CuentaDestino.ucCliente.NoExhibirPtoServicio = True

            ' Controle Canal
            Me.CuentaDestino.ucCanal.NoExhibirCanal = True
            Me.CuentaDestino.ucCanal.NoExhibirSubCanal = True

        ElseIf CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.GestiondeFondos) AndAlso
            CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.MovimientodeFondos) Then

            If CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.EntreSectoresdePlantasDelegacionesDiferentes) Then

                AddHandler Me.CuentaDestino.ControleAtualizado, AddressOf CuentaDestino_ControleAtualizado
                CuentaDestino.TipoSitio = Enumeradores.TipoSitio.Destino
                CuentaDestino.Modo = Modo
                CuentaDestino.SelecaoMultipla = False
                If GrupoDocumentos.CuentaDestino Is Nothing Then
                    GrupoDocumentos.CuentaDestino = GrupoDocumentos.CuentaOrigen.Clonar()
                End If
                Me.CuentaDestino.Cuenta = GrupoDocumentos.CuentaDestino
                Me.CuentaDestino.ucSector.identificadorFormulario = GrupoDocumentos.Formulario.Identificador

                If (Me.Modo = Enumeradores.Modo.Alta) AndAlso Not IsPostBack Then
                    GrupoDocumentos.CuentaDestino.Sector = New Clases.Sector
                End If
                Me.CuentaDestino.ucSector.DelegacionHabilitado = (Modo <> Enumeradores.Modo.Consulta)
                Me.CuentaDestino.ucSector.PlantaHabilitado = (Modo <> Enumeradores.Modo.Consulta)
                Me.CuentaDestino.ucSector.SectorHabilitado = (Modo <> Enumeradores.Modo.Consulta)

                ' Controle Cliente
                Me.CuentaDestino.ucCliente.NoExhibirCliente = True
                Me.CuentaDestino.ucCliente.NoExhibirSubCliente = True
                Me.CuentaDestino.ucCliente.NoExhibirPtoServicio = True
                ' Controle Canal
                Me.CuentaDestino.ucCanal.NoExhibirCanal = True
                Me.CuentaDestino.ucCanal.NoExhibirSubCanal = True

            ElseIf CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.EntreSectoresDeLaMismaPlanta) Then

                AddHandler Me.CuentaDestino.ControleAtualizado, AddressOf CuentaDestino_ControleAtualizado
                CuentaDestino.TipoSitio = Enumeradores.TipoSitio.Destino
                CuentaDestino.Modo = Modo
                CuentaDestino.SelecaoMultipla = False
                If GrupoDocumentos.CuentaDestino Is Nothing Then
                    GrupoDocumentos.CuentaDestino = GrupoDocumentos.CuentaOrigen.Clonar()
                End If
                Me.CuentaDestino.Cuenta = GrupoDocumentos.CuentaDestino
                Me.CuentaDestino.ucSector.identificadorFormulario = GrupoDocumentos.Formulario.Identificador

                If (Me.Modo = Enumeradores.Modo.Alta) AndAlso Not IsPostBack Then
                    GrupoDocumentos.CuentaDestino.Sector.Identificador = Nothing
                    GrupoDocumentos.CuentaDestino.Sector.Descripcion = Nothing
                    GrupoDocumentos.CuentaDestino.Sector.Codigo = Nothing
                End If
                Me.CuentaDestino.ucSector.NoExhibirDelegacion = False
                Me.CuentaDestino.ucSector.NoExhibirPlanta = False
                Me.CuentaDestino.ucSector.SectorHabilitado = (Modo <> Enumeradores.Modo.Consulta)

                ' Controle Cliente
                Me.CuentaDestino.ucCliente.NoExhibirCliente = True
                Me.CuentaDestino.ucCliente.NoExhibirSubCliente = True
                Me.CuentaDestino.ucCliente.NoExhibirPtoServicio = True
                ' Controle Canal
                Me.CuentaDestino.ucCanal.NoExhibirCanal = True
                Me.CuentaDestino.ucCanal.NoExhibirSubCanal = True

            End If

        ElseIf Caracteristicas.Util.VerificarCaracteristicas(CaracteristicasFormulario,
            New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                    Enumeradores.CaracteristicaFormulario.GestiondeContenedores,
                    Enumeradores.CaracteristicaFormulario.OtrosMovimientos)) Then

            AddHandler Me.CuentaDestino.ControleAtualizado, AddressOf CuentaDestino_ControleAtualizado
            CuentaDestino.TipoSitio = Enumeradores.TipoSitio.Destino
            CuentaDestino.Modo = Modo
            CuentaDestino.SelecaoMultipla = False
            If GrupoDocumentos.CuentaDestino Is Nothing Then
                GrupoDocumentos.CuentaDestino = GrupoDocumentos.CuentaOrigen.Clonar()
            End If
            Me.CuentaDestino.Cuenta = GrupoDocumentos.CuentaDestino
            Me.CuentaDestino.ucSector.identificadorFormulario = GrupoDocumentos.Formulario.Identificador

            Me.CuentaDestino.ucSector.DelegacionHabilitado = (Modo <> Enumeradores.Modo.Consulta)
            Me.CuentaDestino.ucSector.PlantaHabilitado = (Modo <> Enumeradores.Modo.Consulta)
            Me.CuentaDestino.ucSector.SectorHabilitado = (Modo <> Enumeradores.Modo.Consulta)

            ' Controle Cliente
            Me.CuentaDestino.ucCliente.NoExhibirCliente = True
            Me.CuentaDestino.ucCliente.NoExhibirSubCliente = True
            Me.CuentaDestino.ucCliente.NoExhibirPtoServicio = True
            ' Controle Canal
            Me.CuentaDestino.ucCanal.NoExhibirCanal = True
            Me.CuentaDestino.ucCanal.NoExhibirSubCanal = True

        End If

    End Sub

    ''' <summary>
    ''' Configuración del Componente Filtro - Responsable por recuperar los documentos (Elementos/Valores) del Sector logado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConfigurarFiltro()

        If Caracteristicas.Util.VerificarCaracteristicas(GrupoDocumentos.Formulario.Caracteristicas,
                  New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                        Enumeradores.CaracteristicaFormulario.Reenvios,
                        Enumeradores.CaracteristicaFormulario.Actas,
                        Enumeradores.CaracteristicaFormulario.Bajas,
                        Enumeradores.CaracteristicaFormulario.CierreDeCaja,
                        Enumeradores.CaracteristicaFormulario.CierreDeTesoro)) AndAlso Modo <> Enumeradores.Modo.Consulta Then

            ContainerDocumentos.Filtros.ClienteVisible = True
            ContainerDocumentos.Filtros.CanalVisible = True
            If Not Caracteristicas.Util.VerificarCaracteristicas(GrupoDocumentos.Formulario.Caracteristicas, New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor, Enumeradores.CaracteristicaFormulario.ExcluirSectoresHijos)) Then
                ContainerDocumentos.Filtros.SectorVisible = True
                ContainerDocumentos.Filtros.Sector.SectorPadre = SectorSelecionado
            Else
                ContainerDocumentos.Filtros.excluirSectoresHijos = True
            End If

            If Caracteristicas.Util.VerificarCaracteristicas(GrupoDocumentos.Formulario.Caracteristicas,
                      New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                            Enumeradores.CaracteristicaFormulario.GestiondeRemesas,
                            Enumeradores.CaracteristicaFormulario.GestiondeBultos)) Then
                ContainerDocumentos.Filtros.FiltroDocumentoVisible = True
                ContainerDocumentos.Filtros.FiltroRemesaVisible = True
                ContainerDocumentos.Filtros.FiltroBultoVisible = True
                ContainerDocumentos.Filtros.FiltroParcialVisible = True
                ' El sistema aún no esta preparado para trabajar con Contenedor
                'Filtros.FiltroContenedorVisible = True

            ElseIf GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Cierres) Then
                ContainerDocumentos.Filtros.FiltroSaldosCuentaVisible = True
                ContainerDocumentos.Filtros.BuscarValoresDisponibles = True

                If GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.CierreDeTesoro) Then
                    ContainerDocumentos.Filtros.ExibirConsiderarSomaZero = True
                End If

                ContainerDocumentos.Filtros.FiltroSaldosCuenta.CodigoIsoDivisaDefecto = Parametros.Parametro.CodigoIsoDivisaDefecto
            End If

        End If
    End Sub

    ''' <summary>
    ''' Configuracion del Componente ContainerDocumentos - Responsable por organizar los documentos del filtro e los documentos que serán gravados en el grupo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConfigurarContainerDocumentos()

        If GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeContenedores) Then

            ListaContenedoresGrp.DocumentosSeleccionados = GrupoDocumentos.Documentos
            ListaContenedoresGrp.ConfigurarControles()
            ListaContenedoresGrp.mensajeVacio = Traduzir("032_ListaDocumentosSeleccionadosVacio")
            ListaContenedoresGrp.Titulo = Traduzir("032_lista_contenedor_titulo")

            AddHandler ListaContenedoresGrp.DetallarDocumento, AddressOf ListaContenedoresGrp_DetallarDocumento

        ElseIf GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeFondos) AndAlso
                GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.MovimientodeFondos) AndAlso
                (GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.EntreClientes) OrElse
                GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.EntreCanales)) Then

            ' Escenario especifico para Documento de Pases
            ListaSimplificadaDeDocumentos.DocumentosSeleccionados = GrupoDocumentos.Documentos
            ListaSimplificadaDeDocumentos.ConfigurarControles()
            ListaSimplificadaDeDocumentos.mensajeVacio = Traduzir("032_ListaDocumentosSeleccionadosVacio")
            ListaSimplificadaDeDocumentos.Modo = Modo
            ListaSimplificadaDeDocumentos.Titulo = Traduzir("032_ListaDocumentosSeleccionados")
            ListaSimplificadaDeDocumentos.esEntreCanales = GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.EntreCanales)

            AddHandler ListaSimplificadaDeDocumentos.DetallarDocumento, AddressOf ListaSimplificadaDeDocumentos_DetallarDocumento
            AddHandler ListaSimplificadaDeDocumentos.QuitarDocumento, AddressOf ListaSimplificadaDeDocumentos_QuitarDocumento

        Else

            If Caracteristicas.Util.VerificarCaracteristicas(GrupoDocumentos.Formulario.Caracteristicas,
                          New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                                Enumeradores.CaracteristicaFormulario.GestiondeRemesas,
                                Enumeradores.CaracteristicaFormulario.GestiondeBultos)) Then
                ContainerDocumentos.LegendaFieldSetFiltro = Traduzir("032_ListaElementosFiltro")
                ContainerDocumentos.LegendaFieldSetGrupo = Traduzir("032_ListaElementosSeleccionados")
                ContainerDocumentos.mensajeVacio = Traduzir("032_ListaElementosVacio")
                ContainerDocumentos.trabajaConElementos = True
            Else
                ContainerDocumentos.LegendaFieldSetFiltro = Traduzir("032_ListaValoresFiltro")
                ContainerDocumentos.LegendaFieldSetGrupo = Traduzir("032_ListaValoresSeleccionados")
                ContainerDocumentos.mensajeVacio = Traduzir("032_ListaValoresVacio")
                ContainerDocumentos.trabajaConElementos = False
                ContainerDocumentos.identificadorSectorActual = SectorSelecionado.Identificador
            End If

            ContainerDocumentos.Formulario = GrupoDocumentos.Formulario
            'Verifica se os codigos comprovantes está vazios
            'se tiver cria um código randomico
            If GrupoDocumentos.Documentos IsNot Nothing Then
                Dim tick = System.DateTime.UtcNow.QuieroExibirEstaFechaEnLaPatalla(Base.InformacionUsuario.DelegacionSeleccionada).Ticks.ToString
                Dim i As Int16 = 0
                For Each doc In GrupoDocumentos.Documentos.Where(Function(d) String.IsNullOrWhiteSpace(d.CodigoComprobante))
                    Dim random As New Random(Convert.ToInt32(tick.Substring(tick.Length - 9, 9) + i))
                    doc.CodigoComprobante = random.Next(1, 999999).ToString("000000")
                    i += 1
                Next
            End If

            ContainerDocumentos.DocumentosSeleccionados = GrupoDocumentos.Documentos
            ContainerDocumentos.Modo = Modo
            ContainerDocumentos.EsGestionBulto = GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeBultos)
            ContainerDocumentos.EsUmBultoPorRemesa = True

            If Caracteristicas.Util.VerificarCaracteristicas(GrupoDocumentos.Formulario.Caracteristicas,
                      New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                            Enumeradores.CaracteristicaFormulario.Reenvios,
                            Enumeradores.CaracteristicaFormulario.Actas,
                            Enumeradores.CaracteristicaFormulario.Bajas,
                            Enumeradores.CaracteristicaFormulario.CierreDeCaja,
                            Enumeradores.CaracteristicaFormulario.CierreDeTesoro)) AndAlso Modo <> Enumeradores.Modo.Consulta Then
                ContainerDocumentos.usaFiltro = True
                ContainerDocumentos.permiteModificar = Not Caracteristicas.Util.VerificarCaracteristicas(GrupoDocumentos.Formulario.Caracteristicas,
                                                            New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                                                                Enumeradores.CaracteristicaFormulario.Reenvios,
                                                                Enumeradores.CaracteristicaFormulario.CierreDeCaja,
                                                                Enumeradores.CaracteristicaFormulario.CierreDeTesoro,
                                                                Enumeradores.CaracteristicaFormulario.Bajas,
                                                                Enumeradores.CaracteristicaFormulario.ActaDeDesembolsado))
            Else
                ContainerDocumentos.usaFiltro = False
                ContainerDocumentos.permiteModificar = True
            End If

            If Caracteristicas.Util.VerificarCaracteristicas(GrupoDocumentos.Formulario.Caracteristicas,
                          New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                                Enumeradores.CaracteristicaFormulario.GestiondeRemesas,
                                Enumeradores.CaracteristicaFormulario.GestiondeBultos)) Then

                If Not GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Actas) Then
                    ContainerDocumentos.TipoValor = Enumeradores.TipoValor.Declarado
                Else
                    ContainerDocumentos.TipoValor = Enumeradores.TipoValor.Contado
                End If

            Else
                ContainerDocumentos.TipoValor = Enumeradores.TipoValor.NoDefinido
            End If

            If Modo <> Enumeradores.Modo.Consulta Then
                ContainerDocumentos.Configurar_Grids()
            End If

        End If

    End Sub

    Private Sub ConfiguraInfAdicionales()
        If (GrupoDocumentos.GrupoTerminosIAC IsNot Nothing AndAlso GrupoDocumentos.GrupoTerminosIAC.TerminosIAC IsNot Nothing) Then
            InfAdicionales.Modo = Me.Modo
            InfAdicionales.Terminos = GrupoDocumentos.GrupoTerminosIAC.TerminosIAC
        End If
    End Sub

    ''' <summary>
    ''' Configuración del Controle Acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConfigurarAcciones()

        AddHandler Acciones.onAccionGuardar, AddressOf Acciones_onAccionGuardar
        AddHandler Acciones.onAccionGuardarConfirmar, AddressOf Acciones_onAccionGuardarConfirmar
        AddHandler Acciones.onAccionConfirmar, AddressOf Acciones_onAccionConfirmar
        AddHandler Acciones.onAccionAceptar, AddressOf Acciones_onAccionAceptar
        AddHandler Acciones.onAccionRechazar, AddressOf Acciones_onAccionRechazar
        AddHandler Acciones.onAccionCancelar, AddressOf Acciones_onAccionCancelar
        AddHandler Acciones.onAccionImprimir, AddressOf Acciones_onAccionImprimir
        AddHandler Acciones.onAccionAnular, AddressOf Acciones_onAccionAnular
        AddHandler Acciones.onAccionVisualizar, AddressOf Acciones_onAccionVisualizar
        AddHandler Acciones.onAccionModificar, AddressOf Acciones_onAccionModificar
        AddHandler Acciones.onAccionAgregarDocumento, AddressOf Acciones_onAccionAgregarDocumento

        Dim esAltas As Boolean = (GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Altas))
        Dim esGestionFondos As Boolean = (GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeFondos))
        Dim sectorLogadoIgualDestino As Boolean

        If GrupoDocumentos.CuentaDestino IsNot Nothing AndAlso GrupoDocumentos.CuentaDestino.Sector IsNot Nothing AndAlso
             Not String.IsNullOrEmpty(GrupoDocumentos.CuentaDestino.Sector.Codigo) Then
            sectorLogadoIgualDestino = (GrupoDocumentos.CuentaDestino.Sector.Codigo = SectorSelecionado.Codigo)
        End If

        If Not SectorHijo OrElse (SectorHijo AndAlso Base.PossuiPermissao(Aplicacao.Util.Utilidad.eTelas.MANTENIMIENTO_DOCUMENTOS_HIJOS)) Then

            Select Case GrupoDocumentos.Estado
                Case Comon.Enumeradores.EstadoDocumento.Nuevo
                    If Modo <> Enumeradores.Modo.Consulta Then
                        Acciones.btnGuardarVisible = True
                        Acciones.btnGuardarConfirmarVisible = True
                        If esAltas OrElse esGestionFondos Then
                            Acciones.btnAgregarDocumentoVisible = True
                        End If
                    End If
                Case Comon.Enumeradores.EstadoDocumento.EnCurso
                    If Modo <> Enumeradores.Modo.Consulta Then
                        Acciones.btnGuardarVisible = True
                        If esAltas OrElse esGestionFondos Then
                            Acciones.btnAgregarDocumentoVisible = True
                        End If
                    Else
                        Acciones.btnModificarVisible = True
                        Acciones.btnAnularVisible = True
                        Acciones.btnConfirmarVisible = True
                    End If
                Case Comon.Enumeradores.EstadoDocumento.Confirmado
                    If sectorLogadoIgualDestino Then
                        Acciones.btnAceptarVisible = True
                        Acciones.btnRechazarVisible = True
                    End If
            End Select

        End If

        If GrupoDocumentos.Estado <> Enumeradores.EstadoDocumento.Nuevo AndAlso
            GrupoDocumentos.Estado <> Enumeradores.EstadoDocumento.EnCurso AndAlso GrupoDocumentos.Estado <> Enumeradores.EstadoDocumento.Anulado Then
            Acciones.btnImprimirVisible = True
            'Acciones.btnVisualizarVisible = True
        End If

        ' Es aceptar automatico
        If CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.MovimientodeAceptacionAutomatica) OrElse
             (Not CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Reenvios) AndAlso
              Not CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.CierreDeCaja) AndAlso
              Not CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.EntreSectoresdePlantasDelegacionesDiferentes) AndAlso
              Not CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.EntreSectoresDeLaMismaPlanta) AndAlso
              Not CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.EntreCanales) AndAlso
              Not CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.EntreClientes)) Then

            Acciones.MovimientodeAceptacionAutomatica = True
        Else
            Acciones.MovimientodeAceptacionAutomatica = False
        End If

    End Sub

    Private Sub Acciones_onAccionAceptar()
        Try
            GuardarGrupoDocumentos(Enumeradores.EstadoDocumento.Aceptado, False)
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionCancelar()
        Try
            If Master.Historico.Count > 1 Then
                Response.Redireccionar(Master.Historico(Master.Historico.Count - 2).Key)
            Else
                Response.Redireccionar(Constantes.NOME_PAGINA_MENU)
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub

    Private Sub Acciones_onAccionAnular()
        Try
            GuardarGrupoDocumentos(Enumeradores.EstadoDocumento.Anulado, False)
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionConfirmar()
        Try
            GuardarGrupoDocumentos(Enumeradores.EstadoDocumento.Confirmado, False)
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionGuardar()
        Try
            GuardarGrupoDocumentos(Enumeradores.EstadoDocumento.Nuevo, False)
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionGuardarConfirmar()
        Try
            GuardarGrupoDocumentos(Enumeradores.EstadoDocumento.Nuevo, True)
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub


    Private Sub Acciones_onAccionImprimir()
        Try
            Dim reporte As String = Prosegur.Genesis.Utilidad.Util.RecuperarNomeRelatorio(GrupoDocumentos)
            If String.IsNullOrEmpty(reporte) Then
                '* Não existe implementação no sistema para documentos do tipo “Gestão de Contenedores” 
                'e “Outros Movimentos”. Por este motivo, a impressão não estará preparada para trabalhar com os mesmos neste momento. 
                Return
            End If

            ClaveDocumentoPopup = Me.RegistrarCache(GrupoDocumentos)
            Me.AbrirPopup("DocumentoImpresion.aspx" & "?COD_COMPROBANTE=" & GrupoDocumentos.CodigoComprobante & "&NombrePopupModal=" & EnumeradoresPantalla.NombrePopupModal.DocumentoImpresion.ToString & "&IDReporte=" & reporte & "&", 1000, 1000)
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionModificar()
        Try
            Response.Redireccionar(Page.AppRelativeVirtualPath & "?IdentificadorSector=" & IdentificadorSector & "&Modo=" & Enumeradores.Modo.Modificacion.ToString() & "&IdentificadorGrupoDocumentos=" & GrupoDocumentos.Identificador)
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionRechazar()
        Try
            GuardarGrupoDocumentos(Enumeradores.EstadoDocumento.Rechazado, False)
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionVisualizar()
        Try
            Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(Traduzir("genNoImplementado"))
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionAgregarDocumento()
        Try
            ClaveDocumento = Me.RegistrarCache(AgregarNuevoDocumentoElemento())
            AbrirPopup("~/Pantallas/Documento.aspx", "ClaveDocumento=" & ClaveDocumento & "&Modo=" & Enumeradores.Modo.Alta.ToString() & "&esGrupoDocumento=True")
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub ActualizaObjetoGrupoDocumentos()

        ' Actualiza las Fechas
        If GrupoDocumentos.Estado = Enumeradores.EstadoDocumento.Nuevo Then
            GrupoDocumentos.FechaHoraCreacion = DateTime.UtcNow
            GrupoDocumentos.UsuarioCreacion = Parametros.Permisos.Usuario.Login
        End If
        If Modo <> Enumeradores.Modo.Consulta Then
            GrupoDocumentos.FechaHoraModificacion = DateTime.UtcNow
            GrupoDocumentos.UsuarioModificacion = Parametros.Permisos.Usuario.Login
        End If

        If InfAdicionales IsNot Nothing Then
            InfAdicionales.GuardarDatos()
        End If

        ' Actualiza informaciones del Documento, más las Informaciones Adcionales
        DatosDocumento.GuardarDatos()

        'Actualiza Documentos
        If Not (GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeFondos) AndAlso
                GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.MovimientodeFondos) AndAlso
                (GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.EntreClientes) OrElse
                GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.EntreCanales))) Then
            GrupoDocumentos.Documentos = ContainerDocumentos.DocumentosSeleccionados
        End If


        ' Regras de actualizaciones de la Cuenta Destino
        If _CuentaDestino IsNot Nothing Then
            GrupoDocumentos.CuentaDestino = _CuentaDestino.Cuenta
            For Each doc In GrupoDocumentos.Documentos
                Dim esDocumentoDeValor As Boolean = True

                If Caracteristicas.Util.VerificarCaracteristicas(CaracteristicasFormulario,
               New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                       Enumeradores.CaracteristicaFormulario.GestiondeRemesas,
                       Enumeradores.CaracteristicaFormulario.GestiondeBultos)) Then
                    esDocumentoDeValor = False
                End If

                doc.CuentaDestino = New Clases.Cuenta() With {.Cliente = doc.CuentaOrigen.Cliente,
                                                                         .Canal = doc.CuentaOrigen.Canal,
                                                                         .SubCanal = doc.CuentaOrigen.SubCanal,
                                                                         .Sector = CuentaDestino.Cuenta.Sector,
                                                                         .SubCliente = doc.CuentaOrigen.SubCliente,
                                                                         .UsuarioCreacion = Parametros.Permisos.Usuario.Login,
                                                                         .UsuarioModificacion = Parametros.Permisos.Usuario.Login,
                                                                         .PuntoServicio = doc.CuentaOrigen.PuntoServicio}

                RecuperarGenerarCuenta(doc.CuentaDestino, doc.CuentaSaldoDestino, esDocumentoDeValor)

            Next
        End If

        'Adicionado para corrigir o problema ao fazer o reenvio não copiar os términos do documento anterior (padre)
        'GENPLATINT-2090 - Brasil - Recepção - Na tela some precinto da remessa depois de reenvia-la a outro setor.
        'If GrupoDocumentos.Documentos IsNot Nothing AndAlso GrupoDocumentos.Documentos.Count > 0 Then
        '    For Each documento In GrupoDocumentos.Documentos

        '        ' verifica se existe um documento padre e se o mesmo é válido
        '        If documento.DocumentoPadre IsNot Nothing Then
        '            If Not String.IsNullOrEmpty(documento.DocumentoPadre.Identificador) Then
        '                Dim respuestaPadreRecuperado = GenesisSaldos.MaestroDocumentos.RecuperarDocumentoPorIdentificador(New ContractoServicio.Contractos.GenesisSaldos.Documento.RecuperarDocumentoPorIdentificador.Peticion With {
        '                                                                                                                    .IdentificadorDocumento = documento.DocumentoPadre.Identificador
        '                                                                                                                    })

        '                If respuestaPadreRecuperado.HayMensajes Then
        '                    Throw New Excepcion.NegocioExcepcion(respuestaPadreRecuperado.TodasMensajesYExcepciones)
        '                End If

        '                Dim documentoPadreRecuperado As Clases.Documento = respuestaPadreRecuperado.Documento
        '                If documentoPadreRecuperado Is Nothing Then
        '                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("028_msg_documento_padre_no_encontrado"), documento.DocumentoPadre.Identificador))
        '                End If
        '                ' verifica se o documento é de substituição
        '                If documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Sustitucion) Then
        '                    ' verifica se existe uma referência, se não, cria-se uma
        '                    If String.IsNullOrEmpty(documentoPadreRecuperado.IdentificadorSustituto) Then
        '                        documentoPadreRecuperado.IdentificadorSustituto = documento.Identificador
        '                    End If
        '                    ' sendo substituição, os documentos padres obrigatoriamente deverão estar nos estados "Aceptado" o "Rechazado"
        '                    If Not documentoPadreRecuperado.Estado.Equals(Enumeradores.EstadoDocumento.Aceptado) AndAlso _
        '                       Not documentoPadreRecuperado.Estado.Equals(Enumeradores.EstadoDocumento.Rechazado) Then
        '                        Throw New Excepcion.NegocioExcepcion(Traduzir("028_msg_estado_documento_sustituto_invalido"))
        '                    End If
        '                End If
        '                ' força o documento padre para o recuperado
        '                documento.DocumentoPadre = documentoPadreRecuperado
        '            End If
        '        End If

        '        ' valida os términos (caso haja)
        '        If documento.GrupoTerminosIAC IsNot Nothing AndAlso documento.GrupoTerminosIAC.TerminosIAC IsNot Nothing Then


        '            ' Aparato Técnico Improvisado: para resolver temporariamente o problema de reenvio de documentos com terminos IAC
        '            ' Este terminos deveriam estar a nivel de Remesas, a demanda ser desenvolvida posteriormente
        '            If documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Reenvios) AndAlso _
        '                documento.DocumentoPadre IsNot Nothing AndAlso documento.DocumentoPadre.GrupoTerminosIAC IsNot Nothing AndAlso _
        '                documento.GrupoTerminosIAC.Identificador = documento.DocumentoPadre.GrupoTerminosIAC.Identificador AndAlso _
        '                documento.DocumentoPadre.GrupoTerminosIAC.TerminosIAC IsNot Nothing Then

        '                Dim z As Boolean = True

        '                For Each termino In documento.GrupoTerminosIAC.TerminosIAC
        '                    If termino IsNot Nothing AndAlso Not String.IsNullOrEmpty(termino.Valor) Then
        '                        z = False
        '                    End If
        '                Next

        '                If z Then
        '                    documento.GrupoTerminosIAC = documento.DocumentoPadre.GrupoTerminosIAC
        '                End If
        '            End If

        '            For Each termino In documento.GrupoTerminosIAC.TerminosIAC
        '                If termino IsNot Nothing AndAlso termino.EsObligatorio AndAlso String.IsNullOrEmpty(termino.Valor) Then
        '                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("028_msg_termino_valor_obligatorio"), termino.Descripcion))
        '                End If
        '            Next
        '        End If

        '    Next
        'End If

    End Sub

    Private Sub RecuperarGenerarCuenta(ByRef cuentaMovimiento As Clases.Cuenta,
                                       ByRef cuentaSaldo As Clases.Cuenta,
                                       esDocumentoDevalor As Boolean)

        Dim _validaciones As New List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError)

        'LogicaNegocio.Genesis.MaestroCuenta.ObtenerCuentas(cuentaMovimiento, cuentaSaldo, esDocumentoDevalor, "", False, _validaciones)

        If _validaciones IsNot Nothing AndAlso _validaciones.Count > 1 Then
            Throw New Excepcion.NegocioExcepcion(String.Join(vbCrLf, _validaciones.Select(Function(f) f.descripcion).ToArray))
        End If

    End Sub

    Private Sub GuardarGrupoDocumentos(estado As Enumeradores.EstadoDocumento,
                                       confirmar_doc As Boolean)

        Dim TiempoInicial As DateTime = Now
        Dim Tiempo As DateTime = Now
        Dim log As New StringBuilder

        Try

            Dim resultadoValidacao As List(Of String) = ValidarControles()
            If resultadoValidacao.Count > 0 Then
                MyBase.MostraMensagemErro(String.Join(Environment.NewLine, resultadoValidacao.ToArray()))
            Else
                ActualizaObjetoGrupoDocumentos()
                If estado <> Enumeradores.EstadoDocumento.Nuevo Then
                    CambiarEstadoGrupoDocumento(estado)
                End If

                If CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Reenvios) Then
                    If GrupoDocumentos IsNot Nothing Then
                        If GrupoDocumentos.SectorDestino IsNot Nothing AndAlso String.IsNullOrEmpty(GrupoDocumentos.SectorDestino.Identificador) Then
                            Dim campo As String = String.Empty
                            campo = String.Format("{0} {1}", Traduzir("018_Sector_Titulo"), Traduzir("017_Titulo_CuentaDestino"))
                            resultadoValidacao.Add(String.Format(Traduzir("msg_campo_obrigatorio"), campo))
                            MyBase.MostraMensagemErro(String.Format(Traduzir("msg_campo_obrigatorio"), campo))
                            Return
                        End If
                    End If
                End If

                For Each Documento In GrupoDocumentos.Documentos
                    Select Case True
                        Case TypeOf Documento.Elemento Is Clases.Remesa
                            DirectCast(Documento.Elemento, Clases.Remesa).IdentificadorDocumento = Documento.Identificador
                            For Each bulto In DirectCast(Documento.Elemento, Clases.Remesa).Bultos
                                DirectCast(bulto, Clases.Bulto).IdentificadorDocumento = Documento.Identificador
                            Next
                        Case TypeOf Documento.Elemento Is Clases.Bulto
                            DirectCast(Documento.Elemento, Clases.Bulto).IdentificadorDocumento = Documento.Identificador
                    End Select
                Next

                GrupoDocumentos.UsuarioModificacion = Parametros.Permisos.Usuario.Login
                GenesisSaldos.MaestroGrupoDocumentos.GuardarGrupoDocumentos(GrupoDocumentos, True, confirmar_doc, bol_gestion_bulto, Nothing, Nothing)

                If confirmar_doc AndAlso (CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Altas) OrElse
                    CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Reenvios) OrElse
                    CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Bajas) OrElse
                    CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Cierres)) Then
                    Session("GrupoDocumentos_" & GrupoDocumentos.Identificador) = GrupoDocumentos
                End If

                If estado = Enumeradores.EstadoDocumento.Anulado Then
                    Response.Redireccionar("~/Pantallas/Formularios.aspx")

                ElseIf estado = Enumeradores.EstadoDocumento.Confirmado Then
                    Dim imprimir As String = String.Empty
                    If GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.AlConfirmarElDocumentoSeImprime) Then
                        imprimir = "&SeImprime=True"
                    End If

                    Response.Redireccionar(Page.AppRelativeVirtualPath & "?IdentificadorSector=" & IdentificadorSector & "&Modo=" & Enumeradores.Modo.Consulta.ToString() & "&IdentificadorGrupoDocumentos=" & GrupoDocumentos.Identificador & imprimir)
                Else

                    Response.Redireccionar(Page.AppRelativeVirtualPath & "?IdentificadorSector=" & IdentificadorSector & "&Modo=" & Enumeradores.Modo.Consulta.ToString() & "&IdentificadorGrupoDocumentos=" & GrupoDocumentos.Identificador)
                End If
            End If

        Catch ex As Excepciones.ExcepcionLogica
            MyBase.MostraMensagemErro(ex.Message)
        Catch ex As Excepcion.NegocioExcepcion
            If ex.Codigo = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_HAYCIERRECAJA Then
                MyBase.MostraMensagemErroConScript(ex.Message, "window.location.href = " & Chr(34) & "Formularios.aspx" & Chr(34))
            ElseIf ex.Message = Traduzir("msgconcurrenciasobreregistro") Then
                MyBase.MostraMensagemErroConScript(Traduzir("msgactualizarpantalla"), "window.location.href = " & Chr(34) & "GrupoDocumento.aspx?IdentificadorSector=" & IdentificadorSector & "&IdentificadorGrupoDocumentos=" & GrupoDocumentos.Identificador & "&Modo=Consulta" & Chr(34))
            Else
                MyBase.MostraMensagemErro(ex.Message)
            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

        log.AppendLine("Tiempo 'GuardarGrupo': " & Now.Subtract(Tiempo).ToString() & "; ")

    End Sub

    Private Sub CambiarEstadoGrupoDocumento(estado As Enumeradores.EstadoDocumento)

        GrupoDocumentos.Estado = estado

        For Each Documento In GrupoDocumentos.Documentos
            If Documento.Estado <> Enumeradores.EstadoDocumento.Anulado Then
                Documento.Estado = estado
                Documento.FechaHoraModificacion = DateTime.UtcNow
                Documento.UsuarioModificacion = Parametros.Permisos.Usuario.Login
            End If
        Next

    End Sub

    ''' <summary>
    ''' Valida se os controles obrigatorios estão preenchidos e se os valores são válidos.
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidarControles() As List(Of String)

        'Valida se os controles obrigatorios estãom preenchidos e se os valores são válidos.
        Dim resultadoValidacao As New List(Of String)()

        If GrupoDocumentos.Documentos Is Nothing OrElse GrupoDocumentos.Documentos.Count < 1 Then
            resultadoValidacao.Add(Traduzir("032_ElementoObrigatorio"))
        End If

        If _CuentaDestino IsNot Nothing Then
            resultadoValidacao.AddRange(CuentaDestino.ValidarControl())
        End If

        Return resultadoValidacao

    End Function

    Private Function AgregarNuevoDocumentoElemento(Optional elemento As Clases.Remesa = Nothing) As Clases.Documento

        If elemento IsNot Nothing AndAlso elemento.Cuenta IsNot Nothing Then
            GrupoDocumentos.CuentaOrigen = elemento.Cuenta.Clonar()
        End If

        Dim nuevoDocumento As Clases.Documento = GenesisSaldos.MaestroDocumentos.CrearDocumento(GrupoDocumentos.Formulario)
        nuevoDocumento.CuentaOrigen = GrupoDocumentos.CuentaOrigen

        If nuevoDocumento.GrupoTerminosIAC Is Nothing Then
            nuevoDocumento.GrupoTerminosIAC = If(nuevoDocumento.Formulario IsNot Nothing, nuevoDocumento.Formulario.GrupoTerminosIACIndividual, Nothing)
        End If

        If elemento IsNot Nothing Then
            nuevoDocumento.Elemento = elemento
            If elemento.CodigoExterno IsNot Nothing Then
                nuevoDocumento.NumeroExterno = elemento.CodigoExterno
            End If
        Else
            Dim remesa As New Clases.Remesa()
            remesa.Identificador = System.Guid.NewGuid().ToString()

            Select Case Parametros.Parametro.ConfigNivelDetalle
                Case Comon.Constantes.CODIGO_CONFIGURACION_NIVEL_DETALLE
                    remesa.ConfiguracionNivelSaldos = Enumeradores.ConfiguracionNivelSaldos.Detalle
                Case Comon.Constantes.CODIGO_CONFIGURACION_NIVEL_TOTAL
                    remesa.ConfiguracionNivelSaldos = Enumeradores.ConfiguracionNivelSaldos.Total
                Case Else
                    remesa.ConfiguracionNivelSaldos = Enumeradores.ConfiguracionNivelSaldos.Ambos
            End Select

            nuevoDocumento.Elemento = remesa
            nuevoDocumento.UsuarioCreacion = Parametros.Permisos.Usuario.Login
            nuevoDocumento.Elemento.UsuarioCreacion = Parametros.Permisos.Usuario.Login
        End If

        nuevoDocumento.UsuarioModificacion = Parametros.Permisos.Usuario.Login
        nuevoDocumento.Elemento.UsuarioModificacion = Parametros.Permisos.Usuario.Login
        nuevoDocumento.FechaHoraPlanificacionCertificacion = DateTime.UtcNow.QuieroExibirEstaFechaEnLaPatalla(Base.InformacionUsuario.DelegacionSeleccionada)
        nuevoDocumento.FechaHoraGestion = DateTime.UtcNow.QuieroExibirEstaFechaEnLaPatalla(Base.InformacionUsuario.DelegacionSeleccionada)

        Return nuevoDocumento

    End Function

#End Region

End Class