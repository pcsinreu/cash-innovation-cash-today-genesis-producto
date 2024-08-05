Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.LogicaNegocio
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.NuevoSaldos.Web.HelperDocumento
Imports Parametros = Prosegur.Genesis.Web.Login.Parametros
Imports Prosegur.Genesis
Imports System.Collections.ObjectModel
Imports Newtonsoft.Json
Imports System.Threading.Tasks

Public Class Documento
    Inherits Base

#Region "[PROPRIEDADES]"

    Public Property Documento() As Clases.Documento
        Get
            Return ViewState("_Documento")
        End Get
        Set(value As Clases.Documento)
            ViewState("_Documento") = value
        End Set
    End Property

    Public ReadOnly Property CaracteristicasFormulario() As List(Of Enumeradores.CaracteristicaFormulario)
        Get
            Return Documento.Formulario.Caracteristicas
        End Get
    End Property

    Private _ClaveDocumento As String = Nothing
    Public ReadOnly Property ClaveDocumento() As String
        Get
            If String.IsNullOrEmpty(_ClaveDocumento) Then
                _ClaveDocumento = Request.QueryString("ClaveDocumento")
            End If
            Return _ClaveDocumento
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

    Private _esGrupoDocumento As Boolean?
    Public ReadOnly Property esGrupoDocumento() As Boolean
        Get
            If Not _esGrupoDocumento.HasValue Then
                _esGrupoDocumento = Not String.IsNullOrEmpty(Request.QueryString("esGrupoDocumento"))
            End If
            Return _esGrupoDocumento.Value
        End Get
    End Property

    Private _SeImprime As Boolean?
    Public Property SeImprime() As Boolean
        Get
            If Not _SeImprime.HasValue Then
                If String.IsNullOrEmpty(ViewState("SeImprime")) Then
                    If String.IsNullOrEmpty(Request.QueryString("SeImprime")) Then
                        _SeImprime = False
                    Else
                        _SeImprime = Convert.ToBoolean(Request.QueryString("SeImprime"))
                    End If
                Else
                    _SeImprime = Convert.ToBoolean(ViewState("SeImprime"))
                End If
            End If
            Return _SeImprime.Value
        End Get
        Set(value As Boolean)
            ViewState("SeImprime") = value
            _SeImprime = value
        End Set
    End Property

    Private _IdentificadorDocumento As String = Nothing
    Public ReadOnly Property IdentificadorDocumento() As String
        Get
            If String.IsNullOrEmpty(_IdentificadorDocumento) Then
                _IdentificadorDocumento = Request.QueryString("IdentificadorDocumento")
            End If
            Return _IdentificadorDocumento
        End Get
    End Property

    Private _IdentificadorFormulario As String = Nothing
    Public ReadOnly Property IdentificadorFormulario() As String
        Get
            If String.IsNullOrEmpty(_IdentificadorFormulario) Then
                _IdentificadorFormulario = Request.QueryString("IdentificadorFormulario")
            End If
            Return _IdentificadorFormulario
        End Get
    End Property



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





    Private _NombrePopupModal As String = Nothing
    Public ReadOnly Property NombrePopupModal() As String
        Get
            If String.IsNullOrEmpty(_NombrePopupModal) Then
                _NombrePopupModal = Request.QueryString("NombrePopupModal")
            End If
            Return _NombrePopupModal
        End Get
    End Property

    Private _EsModal As Boolean?
    Public ReadOnly Property EsModal() As Boolean
        Get
            If (Request.QueryString.AllKeys.Contains("EsPopup")) Then
                _EsModal = Request.QueryString("EsPopup")
            End If

            If Not _EsModal.HasValue Then
                _EsModal = Not String.IsNullOrEmpty(Request.QueryString("ClaveDocumento"))
            End If
            Return _EsModal.Value
        End Get
    End Property

    Private _EsDocumentoIndividual As Boolean?
    Public ReadOnly Property EsDocumentoIndividual() As Boolean
        Get
            If Not _EsDocumentoIndividual.HasValue Then
                _EsDocumentoIndividual = Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.DocumentoEsDelTipoIndividual)
            End If
            Return _EsDocumentoIndividual.Value
        End Get
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

    Private _TipoElemento As Enumeradores.TipoElemento?
    Public ReadOnly Property TipoElemento() As Enumeradores.TipoElemento
        Get
            If Not _TipoElemento.HasValue Then
                _TipoElemento = ObtenerTipoElemento(Documento.Elemento)
            End If
            Return _TipoElemento
        End Get
    End Property

    Private _ElementoContenedor As Clases.Contenedor = Nothing
    Public ReadOnly Property ElementoContenedor() As Clases.Contenedor
        Get
            If _ElementoContenedor Is Nothing Then
                _ElementoContenedor = DirectCast(Documento.Elemento, Clases.Contenedor)
            End If
            Return _ElementoContenedor
        End Get
    End Property

    Private _ElementoRemesa As Clases.Remesa = Nothing
    Public ReadOnly Property ElementoRemesa() As Clases.Remesa
        Get
            If _ElementoRemesa Is Nothing Then
                _ElementoRemesa = DirectCast(Documento.Elemento, Clases.Remesa)
            End If
            Return _ElementoRemesa
        End Get
    End Property

    Private _ElementoBulto As Clases.Bulto = Nothing
    Public ReadOnly Property ElementoBulto() As Clases.Bulto
        Get
            If _ElementoBulto Is Nothing Then
                _ElementoBulto = DirectCast(Documento.Elemento, Clases.Bulto)
            End If
            Return _ElementoBulto
        End Get
    End Property

    Private _ElementoParcial As Clases.Parcial = Nothing
    Public ReadOnly Property ElementoParcial() As Clases.Parcial
        Get
            If _ElementoParcial Is Nothing Then
                _ElementoParcial = DirectCast(Documento.Elemento, Clases.Parcial)
            End If
            Return _ElementoParcial
        End Get
    End Property

    Private WithEvents _DatosDocumento As ucDatosDocumento
    Public ReadOnly Property DatosDocumento() As ucDatosDocumento
        Get
            If _DatosDocumento Is Nothing Then
                _DatosDocumento = LoadControl("~\Controles\ucDatosDocumento.ascx")

                _DatosDocumento.Attributes.Add("identificadorSector", SectorSelecionado.Identificador)
                _DatosDocumento.ID = "DatosDocumento"
                AddHandler _DatosDocumento.Erro, AddressOf ErroControles
                phDatosDocumento.Controls.Add(_DatosDocumento)
            End If
            Return _DatosDocumento
        End Get
    End Property

    Private _CuentaOrigen As ucCuenta
    Public ReadOnly Property CuentaOrigen() As ucCuenta
        Get
            If _CuentaOrigen Is Nothing Then
                _CuentaOrigen = LoadControl("~\Controles\ucCuenta.ascx")
                _CuentaOrigen.ID = "CuentaOrigen"
                _CuentaOrigen.Attributes.Add("identificadorSector", SectorSelecionado.Identificador)
                AddHandler _CuentaOrigen.ControleAtualizado, AddressOf CuentaOrigen_ControleAtualizado
                AddHandler _CuentaOrigen.Erro, AddressOf ErroControles
                If phCuentaOrigen.Controls.Count = 0 Then
                    phCuentaOrigen.Controls.Add(_CuentaOrigen)
                End If
            End If
            Return _CuentaOrigen
        End Get
    End Property

    Private _CuentaDestino As ucCuenta
    Public ReadOnly Property CuentaDestino() As ucCuenta
        Get
            If _CuentaDestino Is Nothing Then
                _CuentaDestino = LoadControl("~\Controles\ucCuenta.ascx")
                _CuentaDestino.ID = "CuentaDestino"
                _CuentaDestino.Attributes.Add("identificadorSector", SectorSelecionado.Identificador)
                AddHandler _CuentaDestino.ControleAtualizado, AddressOf CuentaDestino_ControleAtualizado
                AddHandler _CuentaDestino.Erro, AddressOf ErroControles
                If phCuentaDestino.Controls.Count = 0 Then
                    phCuentaDestino.Controls.Add(_CuentaDestino)
                End If
            End If
            Return _CuentaDestino
        End Get
    End Property

    Private WithEvents _Elemento As UcElemento
    Public ReadOnly Property Elemento() As UcElemento
        Get
            If _Elemento Is Nothing Then
                _Elemento = LoadControl("~\Controles\UcElemento.ascx")
                _Elemento.ID = "Elemento"
                _Elemento.ConfigNivelSaldo = Parametros.Parametro.ConfigNivelDetalle
                AddHandler _Elemento.Erro, AddressOf ErroControles
                phElemento.Controls.Add(_Elemento)
            End If
            Return _Elemento
        End Get
    End Property

    Private WithEvents _Valores As ucValores
    Public ReadOnly Property Valores() As ucValores
        Get
            If _Valores Is Nothing Then
                _Valores = LoadControl("~\Controles\ucValores.ascx")
                _Valores.ID = "Valores"
                AddHandler _Valores.Erro, AddressOf ErroControles
                phValores.Controls.Add(_Valores)
            End If
            Return _Valores
        End Get
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

    Private _Acciones As ucAcciones
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

    Private _SectorHijo As Boolean
    Public ReadOnly Property SectorHijo() As Boolean
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("SectorHijo")) Then
                _SectorHijo = Convert.ToBoolean(Request.QueryString("SectorHijo"))
            End If
            Return _SectorHijo
        End Get
    End Property

    Private WithEvents _Saldo As ucSaldo
    Public ReadOnly Property Saldo() As ucSaldo
        Get
            If _Saldo Is Nothing Then
                _Saldo = LoadControl("~\Controles\ucSaldo.ascx")
                _Saldo.ID = "Saldo"
                AddHandler _Saldo.Erro, AddressOf ErroControles
                phValores.Controls.Add(_Saldo)
            End If
            Return _Saldo
        End Get
    End Property

    Private WithEvents _InfContenedor As ucInfContenedor
    Public ReadOnly Property InfContenedor() As ucInfContenedor
        Get
            If _InfContenedor Is Nothing Then
                _InfContenedor = LoadControl("~\Controles\ucInfContenedor.ascx")
                _InfContenedor.ID = "InfContenedor"
                AddHandler _InfContenedor.Erro, AddressOf ErroControles
                phInfContenedor.Controls.Add(_InfContenedor)
            End If
            Return _InfContenedor
        End Get
    End Property

    Private Property ValidacionDiferencias As System.Text.StringBuilder
        Get
            Return ViewState("_validaciondiferencias")
        End Get
        Set(value As System.Text.StringBuilder)
            ViewState("_validaciondiferencias") = value
        End Set
    End Property

    Public ReadOnly Property bol_gestion_bulto() As Boolean
        Get
            If Documento IsNot Nothing AndAlso Documento.Formulario IsNot Nothing AndAlso Documento.Formulario.Caracteristicas IsNot Nothing AndAlso
                Documento.Formulario.Caracteristicas.Count > 0 AndAlso Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeBultos) Then
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
            MyBase.CodFuncionalidad = "ABM_DOCUMENTO"
            MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.DOCUMENTO
            MyBase.ValidarAcesso = True
            MyBase.ValidarPemissaoAD = True
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Protected Overrides Sub TraduzirControles()
        Try
            MyBase.TraduzirControles()
            Master.Titulo = Traduzir("033_Titulo")
            lblTitulo_MensajeExterno.Text = Traduzir("033_MensajeExterna")

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Protected Overrides Sub Inicializar()
        Try
            MyBase.Inicializar()
            GoogleAnalyticsHelper.TrackAnalytics(Me, "Documentos")
            If Not Page.IsPostBack Then
                'Verifica se está criando um novo documento ou um novo elemento do documento.
                If EsModal Then
                    Master.HabilitarMenu = False

                    If (String.IsNullOrEmpty(Me.ClaveDocumento)) Then
                        If (Modo = Enumeradores.Modo.Consulta OrElse Modo = Enumeradores.Modo.ModificarTerminos) Then
                            ModoModificacion()
                        End If
                    Else
                        Documento = Me.ObtenerCache(ClaveDocumento)
                    End If
                Else
                    Select Case Modo
                        Case Enumeradores.Modo.Alta
                            ModoAlta()
                        Case Enumeradores.Modo.Modificacion, Enumeradores.Modo.Consulta, Enumeradores.Modo.ModificarTerminos
                            ModoModificacion()
                    End Select
                End If
            End If

            If InformacionUsuario Is Nothing OrElse InformacionUsuario.Nombre Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(MyBase.RecuperarValorDic("WARNING_MSG_SESSION_HAS_EXPIRED"))
            End If

            ConfigurarControles()
            If SeImprime AndAlso Documento IsNot Nothing Then
                SeImprime = False
                Call Acciones_onAccionImprimir()
            End If

            If (Not Me.IsPostBack) Then
                If (_DatosDocumento IsNot Nothing) Then
                    _DatosDocumento.Focus()
                End If
            End If

            If ValidacionDiferencias IsNot Nothing AndAlso ValidacionDiferencias.Length > 0 Then

                MyBase.MostraMensagemErro(ValidacionDiferencias.ToString)

            End If
            _SectorSelecionado = LogicaNegocio.Genesis.Sector.ObtenerPorOid(IdentificadorSector, cargarCodigosAjenos:=True)

            If SectorSelecionado.CodigosAjenos.FirstOrDefault(Function(x) x.CodigoIdentificador = "MAE") Is Nothing Then
                Dim msgError = String.Format(MyBase.RecuperarValorDic("MSG_INFO_SIN_CODIGO_AJENO_MAE"), _SectorSelecionado.Codigo)
                MostraMensagemErro(msgError, "window.location.href = ""Formularios.aspx?IdentificadorSector=" & IdentificadorSector + """; ")
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

#End Region

#Region "[EVENTOS]"

    Private Sub _ucInfAdicionales_ControleAtualizado(sender As Object, e As System.EventArgs) Handles _InfAdicionales.ControleAtualizado

    End Sub

    Private Sub Documento_Error(sender As Object, e As System.EventArgs) Handles Me.Error
        MyBase.MostraMensagemErro(Server.GetLastError().ToString())
    End Sub

    Private Sub Documento_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim script As String = String.Format("alteraTeclaEnterPorTeclaTab(); ")
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), Me.ID, script, True)

        If Request("__EVENTTARGET") = Acciones.UniqueIDbtnConfirmar Then
            Acciones_onAccionConfirmar()
        End If

    End Sub

    Private Sub Documento_PopupCerrado(nombrePopup As String, argumento As String) Handles Me.PopupCerrado
        Try
            Dim NombrePopupModalCerrada As EnumeradoresPantalla.NombrePopupModal = [Enum].Parse(GetType(EnumeradoresPantalla.NombrePopupModal), nombrePopup)
            Dim accionPopupCerrado As EnumeradoresPantalla.AccionPopupCerrado = [Enum].Parse(GetType(EnumeradoresPantalla.AccionPopupCerrado), argumento)
            Dim documentoPopup As Clases.Documento = Me.BorrarCache(ClaveDocumentoPopup)

            Select Case NombrePopupModalCerrada
                Case EnumeradoresPantalla.NombrePopupModal.AgregarBulto, EnumeradoresPantalla.NombrePopupModal.AgregarParcial
                    Select Case accionPopupCerrado
                        Case EnumeradoresPantalla.AccionPopupCerrado.Guardar
                            Documento.DocumentosRelacionados.Add(documentoPopup)
                            Select Case TipoElemento
                                Case Enumeradores.TipoElemento.Contenedor
                                    ElementoContenedor.Elementos.Add(documentoPopup.Elemento)

                                Case Enumeradores.TipoElemento.Remesa
                                    'Se for gestão de remessa todos os bultos devem ter o mesmo tipo serviço
                                    'O tipo serviço de todos os bultos são definidos a partir do ultimo bulto adicionado/modificado
                                    If Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeRemesas) Then
                                        If ElementoRemesa IsNot Nothing AndAlso ElementoRemesa.Bultos IsNot Nothing AndAlso ElementoRemesa.Bultos.Count > 0 Then
                                            For Each bulto In ElementoRemesa.Bultos
                                                bulto.TipoServicio = DirectCast(documentoPopup.Elemento, Clases.Bulto).TipoServicio
                                            Next
                                        End If
                                    End If

                                    'Verifica precinto repetido BUG: 972
                                    If ElementoRemesa IsNot Nothing AndAlso ElementoRemesa.Bultos IsNot Nothing AndAlso ElementoRemesa.Bultos.Count > 0 Then
                                        For Each prec In DirectCast(documentoPopup.Elemento, Clases.Bulto).Precintos
                                            If ElementoRemesa.Bultos.Where(Function(a) a.Precintos.Contains(prec)).Count > 0 Then
                                                MyBase.MostraMensagemErro(Traduzir("026_Agregar_Precinto_Repetido_" + TipoElemento.ToString()))
                                                Exit For
                                            End If
                                        Next
                                    End If

                                    ElementoRemesa.Bultos.Add(documentoPopup.Elemento)

                                Case Enumeradores.TipoElemento.Bulto
                                    'Verifica precinto repetido BUG: 972
                                    If ElementoBulto IsNot Nothing AndAlso ElementoBulto.Parciales IsNot Nothing AndAlso ElementoBulto.Parciales.Count > 0 Then
                                        For Each prec In DirectCast(documentoPopup.Elemento, Clases.Parcial).Precintos
                                            If ElementoBulto.Parciales.Where(Function(a) a.Precintos.Contains(prec)).Count > 0 Then
                                                MyBase.MostraMensagemErro(Traduzir("026_Agregar_Precinto_Repetido_" + TipoElemento.ToString()))
                                                Exit For
                                            End If
                                        Next
                                    End If

                                    ElementoBulto.Parciales.Add(documentoPopup.Elemento)

                            End Select

                    End Select

                Case EnumeradoresPantalla.NombrePopupModal.DetallarElemento
                    Dim documentoPai As Clases.Documento = ObtenerPadreDocumentoRelacionado(Documento, documentoPopup)

                    'Se for gestão de remessa todos os bultos devem ter o mesmo tipo serviço
                    'O tipo serviço de todos os bultos são definidos a partir do ultimo bulto adicionado/modificado
                    If Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeRemesas) Then
                        If documentoPai IsNot Nothing AndAlso documentoPai.Elemento IsNot Nothing AndAlso (TypeOf documentoPai.Elemento Is Clases.Remesa) AndAlso DirectCast(documentoPai.Elemento, Clases.Remesa).Bultos IsNot Nothing AndAlso DirectCast(documentoPai.Elemento, Clases.Remesa).Bultos.Count > 0 Then

                            For Each bulto In DirectCast(documentoPai.Elemento, Clases.Remesa).Bultos
                                bulto.TipoServicio = DirectCast(documentoPopup.Elemento, Clases.Bulto).TipoServicio
                            Next

                        End If
                    End If

                    ActualizarDocumento(documentoPai, documentoPopup)
                    ActualizarElemento(documentoPai.Elemento, documentoPopup.Elemento)

                    Select Case accionPopupCerrado
                        Case EnumeradoresPantalla.AccionPopupCerrado.Anular
                            If documentoPopup.Estado = Enumeradores.EstadoDocumento.Nuevo Then
                                documentoPai.DocumentosRelacionados.Remove(documentoPai.DocumentosRelacionados.Find(Function(d) d.Identificador = documentoPopup.Identificador))
                            Else
                                documentoPopup.Estado = Enumeradores.EstadoDocumento.Anulado
                            End If
                            Select Case ObtenerTipoElemento(documentoPopup.Elemento)
                                Case Enumeradores.TipoElemento.Contenedor
                                    Dim contenedor As Clases.Contenedor = DirectCast(documentoPopup.Elemento, Clases.Contenedor)
                                    If contenedor.Estado = Enumeradores.EstadoContenedor.Nuevo Then
                                        Dim elementoPai As Clases.Contenedor = ObtenerPadreElemento(Documento.Elemento, documentoPopup.Elemento)
                                        elementoPai.Elementos.Remove(elementoPai.Elementos.Find(Function(e) e.Identificador = documentoPopup.Elemento.Identificador))
                                    Else
                                        contenedor.Estado = Enumeradores.EstadoContenedor.Anulado
                                    End If

                                Case Enumeradores.TipoElemento.Remesa
                                    Dim remesa As Clases.Remesa = DirectCast(documentoPopup.Elemento, Clases.Remesa)
                                    If remesa.Estado = Enumeradores.EstadoRemesa.Nuevo Then
                                        Dim elementoPai As Clases.Contenedor = ObtenerPadreElemento(Documento.Elemento, documentoPopup.Elemento)
                                        elementoPai.Elementos.Remove(elementoPai.Elementos.Find(Function(e) e.Identificador = documentoPopup.Elemento.Identificador))
                                    Else
                                        remesa.Estado = Enumeradores.EstadoRemesa.Anulado
                                    End If

                                Case Enumeradores.TipoElemento.Bulto
                                    Dim bulto As Clases.Bulto = DirectCast(documentoPopup.Elemento, Clases.Bulto)
                                    If bulto.Estado = Enumeradores.EstadoBulto.Nuevo Then
                                        Dim elementoPai As Clases.Remesa = ObtenerPadreElemento(Documento.Elemento, documentoPopup.Elemento)
                                        elementoPai.Bultos.Remove(elementoPai.Bultos.Find(Function(b) b.Identificador = documentoPopup.Elemento.Identificador))
                                    Else
                                        bulto.Estado = Enumeradores.EstadoBulto.Anulado
                                    End If

                                Case Enumeradores.TipoElemento.Parcial
                                    Dim parcial As Clases.Parcial = DirectCast(documentoPopup.Elemento, Clases.Parcial)
                                    If parcial.Estado = Enumeradores.EstadoParcial.Nuevo Then
                                        Dim elementoPai As Clases.Bulto = ObtenerPadreElemento(Documento.Elemento, documentoPopup.Elemento)
                                        elementoPai.Parciales.Remove(elementoPai.Parciales.Find(Function(p) p.Identificador = documentoPopup.Elemento.Identificador))
                                    Else
                                        parcial.Estado = Enumeradores.EstadoParcial.Anulado
                                    End If

                            End Select

                    End Select

            End Select

            Elemento.ActualizarDatos()
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub _Elemento_CrearElemento(sender As Object, e As UcElemento.CrearElementoEventArgs) Handles _Elemento.CrearElemento
        Try
            If (_DatosDocumento IsNot Nothing) Then
                Me._DatosDocumento.GuardarDatos()
            End If
            Select Case TipoElemento
                Case Enumeradores.TipoElemento.Remesa
                    ClaveDocumentoPopup = Me.RegistrarCache(AgregarNuevoDocumentoElemento(Of Clases.Bulto)(e.Elemento))
                    If e.AcaoAgregar = UcElemento.AcaoAgregarEnum.Avanzado Then
                        Me.AbrirPopup(Page.AppRelativeVirtualPath, "Modo=" & Enumeradores.Modo.Alta.ToString() & "&ClaveDocumento=" & ClaveDocumentoPopup & "&NombrePopupModal=" & EnumeradoresPantalla.NombrePopupModal.AgregarBulto.ToString())
                    Else
                        Documento_PopupCerrado(EnumeradoresPantalla.NombrePopupModal.AgregarBulto.ToString(), EnumeradoresPantalla.AccionPopupCerrado.Guardar)
                    End If

                Case Enumeradores.TipoElemento.Bulto
                    ClaveDocumentoPopup = Me.RegistrarCache(AgregarNuevoDocumentoElemento(Of Clases.Parcial)(e.Elemento))
                    If e.AcaoAgregar = UcElemento.AcaoAgregarEnum.Avanzado Then
                        Me.AbrirPopup(Page.AppRelativeVirtualPath, "Modo=" & Enumeradores.Modo.Alta.ToString() & "&ClaveDocumento=" & ClaveDocumentoPopup & "&NombrePopupModal=" & EnumeradoresPantalla.NombrePopupModal.AgregarParcial.ToString())
                    Else
                        Documento_PopupCerrado(EnumeradoresPantalla.NombrePopupModal.AgregarParcial.ToString(), EnumeradoresPantalla.AccionPopupCerrado.Guardar)
                    End If

            End Select
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Elemento_DetallarElemento(sender As Object, e As ucListaElementos.DetallarElementoEventArgs) Handles _Elemento.DetallarElemento
        Try
            Dim modoPopup As Enumeradores.Modo
            If Modo = Enumeradores.Modo.Alta OrElse Modo = Enumeradores.Modo.Modificacion Then
                modoPopup = Enumeradores.Modo.Modificacion
            Else
                modoPopup = Enumeradores.Modo.Consulta
            End If
            ActualizaDocumentosDocumento(Me.Documento.DocumentosRelacionados)
            Dim documentoElemento As Clases.Documento = ObtenerGenerarDocumentoElemento(Documento, e.Elemento)
            Comon.Util.BorrarItemsDivisaSinValoresCantidades(documentoElemento)
            ClaveDocumentoPopup = Me.RegistrarCache(documentoElemento)
            Me.AbrirPopup(Page.AppRelativeVirtualPath, "IdentificadorSector=" & IdentificadorSector & "&Modo=" & modoPopup.ToString() & "&ClaveDocumento=" & ClaveDocumentoPopup & "&NombrePopupModal=" & EnumeradoresPantalla.NombrePopupModal.DetallarElemento.ToString())
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub ListaElemento_SeleccionarElemento(sender As Object, e As UcElemento.ElementoSeleccionadosElementoEventArgs) Handles _Elemento.SeleccionarElemento

        For Each elementoselecionado In e.Elemento
            Dim elementoselecionadoLocal = elementoselecionado

            If Documento.Estado = Enumeradores.EstadoDocumento.Nuevo Then
                Documento.DocumentosRelacionados.Remove(Documento.DocumentosRelacionados.Find(Function(value As Clases.Documento)
                                                                                                  Return value.Elemento.Identificador = elementoselecionadoLocal.Identificador
                                                                                              End Function))

                Select Case TipoElemento
                    Case Enumeradores.TipoElemento.Contenedor
                        ElementoContenedor.Elementos.Remove(ElementoContenedor.Elementos.Find(Function(value As Clases.Elemento)
                                                                                                  Return value.Identificador = elementoselecionadoLocal.Identificador
                                                                                              End Function))

                    Case Enumeradores.TipoElemento.Remesa
                        ElementoRemesa.Bultos.Remove(ElementoRemesa.Bultos.Find(Function(value As Clases.Elemento)
                                                                                    Return value.Identificador = elementoselecionadoLocal.Identificador
                                                                                End Function))

                    Case Enumeradores.TipoElemento.Bulto
                        ElementoBulto.Parciales.Remove(ElementoBulto.Parciales.Find(Function(value As Clases.Elemento)
                                                                                        Return value.Identificador = elementoselecionadoLocal.Identificador
                                                                                    End Function))

                End Select

            ElseIf Documento.Estado = Enumeradores.EstadoDocumento.EnCurso Then

                If Documento.DocumentosRelacionados IsNot Nothing AndAlso
                   Documento.DocumentosRelacionados.Count > 0 Then
                    Documento.DocumentosRelacionados.Find(Function(value As Clases.Documento)
                                                              Return value.Elemento.Identificador = elementoselecionadoLocal.Identificador
                                                          End Function).Estado = Enumeradores.EstadoDocumento.Eliminado
                End If

                Select Case TipoElemento

                    Case Enumeradores.TipoElemento.Remesa
                        ElementoRemesa.Bultos.Find(Function(value As Clases.Elemento)
                                                       Return value.Identificador = elementoselecionadoLocal.Identificador
                                                   End Function).Estado = Enumeradores.EstadoBulto.Eliminado

                    Case Enumeradores.TipoElemento.Bulto
                        ElementoBulto.Parciales.Find(Function(value As Clases.Elemento)
                                                         Return value.Identificador = elementoselecionadoLocal.Identificador
                                                     End Function).Estado = Enumeradores.EstadoBulto.Eliminado

                End Select
            End If
        Next
        Elemento.ActualizarDatos()

    End Sub

    Private Sub CuentaOrigen_ControleAtualizado(sender As Object, e As ControleEventArgs)
        Try
            If CuentaOrigen.Cuenta IsNot Nothing Then
                'Limpa o identificador da conta para buscar novamente
                CuentaOrigen.Cuenta.Identificador = Nothing
                Documento.CuentaOrigen = CuentaOrigen.Cuenta
            End If

            'Se for modificar o documento e alterar a conta tem que buscar a conta saldo novamente
            If Modo = Enumeradores.Modo.Modificacion Then
                Documento.CuentaSaldoOrigen = Nothing
            End If
            If Caracteristicas.Util.VerificarCaracteristicas(Documento.Formulario.Caracteristicas,
                New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                        Enumeradores.CaracteristicaFormulario.GestiondeRemesas,
                        Enumeradores.CaracteristicaFormulario.GestiondeBultos)) Then
                If Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Altas) Then
                    Documento.CuentaDestino = CuentaOrigen.Cuenta
                End If

            ElseIf Caracteristicas.Util.VerificarCaracteristicas(Documento.Formulario.Caracteristicas,
                                                                                 New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.And,
                                                                                                                                     Enumeradores.CaracteristicaFormulario.GestiondeFondos),
                                                                                 New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                                                                                                                                     Enumeradores.CaracteristicaFormulario.Classificacion)) Then

                If Me.Documento IsNot Nothing AndAlso Me.Documento.CuentaOrigen IsNot Nothing _
                        AndAlso Me.Documento.CuentaOrigen.Cliente IsNot Nothing AndAlso
                        Me.Documento.CuentaOrigen.SubCanal IsNot Nothing Then

                    If e IsNot Nothing AndAlso (e.Controle = "SubCanal" OrElse e.Controle = "Cliente" OrElse e.Controle = "SubCliente" OrElse e.Controle = "PuntoServicio") Then

                        Dim divisas As ObservableCollection(Of Clases.Divisa) = Nothing
                        Dim divisasCuadre As ObservableCollection(Of Clases.Divisa) = Nothing
                        Dim divisasSaldoAnterior As ObservableCollection(Of Clases.Divisa) = Nothing

                        Dim tfDivisas As Task
                        Dim tfDivisasSaldoAnterior As Task
                        Dim tfDivisasCuadre As Task
                        Dim codigoIsoDefecto As String = Parametros.Parametro.CodigoIsoDivisaDefecto
                        tfDivisas = New Task(Sub()

                                                 Dim UtilizarTotalizador As Boolean = True
                                                 If Caracteristicas.Util.VerificarCaracteristicas(Documento.Formulario.Caracteristicas,
                                                                                                    New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.And,
                                                                                                    Enumeradores.CaracteristicaFormulario.Classificacion)) Then
                                                     UtilizarTotalizador = False

                                                 End If

                                                 divisas = LogicaNegocio.GenesisSaldos.Saldo.ObtenerSaldoModificar(Documento.CuentaOrigen, UtilizarTotalizador)

                                                 divisas = LogicaNegocio.GenesisSaldos.Saldo.OrdenarDivisasPorCodigoISODefectoEAlfabeto(divisas, codigoIsoDefecto)
                                             End Sub)

                        tfDivisasSaldoAnterior = New Task(Sub()

                                                              If Modo = Enumeradores.Modo.Consulta OrElse Modo = Enumeradores.Modo.ModificarTerminos Then
                                                                  ' Recuperar saldo anterior pelo documento
                                                                  divisasSaldoAnterior = LogicaNegocio.GenesisSaldos.Saldo.ObtenerSaldoAnterior(Documento.Identificador, Enumeradores.Modo.Consulta)

                                                              ElseIf Modo = Enumeradores.Modo.Modificacion Then
                                                                  divisasSaldoAnterior = LogicaNegocio.GenesisSaldos.Saldo.ObtenerSaldoAnteriorModificar(Documento.Identificador)
                                                              End If

                                                              divisasSaldoAnterior = LogicaNegocio.GenesisSaldos.Saldo.OrdenarDivisasPorCodigoISODefectoEAlfabeto(divisasSaldoAnterior, codigoIsoDefecto)

                                                          End Sub)

                        tfDivisasCuadre = New Task(Sub()
                                                       If Modo = Enumeradores.Modo.Consulta OrElse Modo = Enumeradores.Modo.ModificarTerminos Then
                                                           divisasCuadre = LogicaNegocio.GenesisSaldos.Saldo.ObtenerCuadreSaldoAtualXAnterior(Documento.Identificador, Documento.CuentaSaldoOrigen.Identificador, Nothing)
                                                           divisasCuadre = LogicaNegocio.GenesisSaldos.Saldo.OrdenarDivisasPorCodigoISODefectoEAlfabeto(divisasCuadre, codigoIsoDefecto)
                                                       End If
                                                   End Sub)

                        tfDivisas.Start()
                        tfDivisasSaldoAnterior.Start()
                        tfDivisasCuadre.Start()

                        Task.WaitAll(New Task() {tfDivisas, tfDivisasSaldoAnterior, tfDivisasCuadre})

                        If Modo = Enumeradores.Modo.Consulta OrElse Modo = Enumeradores.Modo.ModificarTerminos Then
                            Me.Saldo.DivisasAnterior = divisasSaldoAnterior
                            CuadreSaldoAnteriorConsultar(Me.Saldo.DivisasAnterior, divisasCuadre)

                        ElseIf Modo = Enumeradores.Modo.Modificacion Then
                            ActualizarCampoDetallarSaldoActualModificar(divisasSaldoAnterior, divisas)

                        End If

                        Me.Saldo.Divisas = divisas

                        If Me.Saldo.Divisas Is Nothing OrElse Me.Saldo.Divisas.Count = 0 Then
                            MyBase.MostraMensagemErro(Traduzir("072_cuentasinsaldo"))
                        End If

                    Else
                        Me.Saldo.Divisas = Nothing
                        Me.Saldo.DivisasAnterior = Nothing
                    End If

                Else
                    Me.Saldo.Divisas = Nothing
                    Me.Saldo.DivisasAnterior = Nothing

                End If

                Me.Saldo.AtualizarDivisas()

            End If

            ActualizaElementosDocumento()
            ActualizaDocumentosDocumento(Me.Documento.DocumentosRelacionados)

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub

    Private Sub CuentaDestino_ControleAtualizado(sender As Object, e As System.EventArgs)
        Try
            If CuentaDestino.Cuenta IsNot Nothing Then
                'Limpa o identificador da conta para buscar novamente
                CuentaDestino.Cuenta.Identificador = Nothing
                Documento.CuentaDestino = CuentaDestino.Cuenta
            End If
            'Se for modificar o documento e alterar a conta tem que buscar a conta saldo novamente
            If Modo = Enumeradores.Modo.Modificacion Then
                Documento.CuentaSaldoDestino = Nothing
            End If
            ActualizaElementosDocumento()
            ActualizaDocumentosDocumento(Me.Documento.DocumentosRelacionados)
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

#End Region

#Region "[METODOS]"

    Private Sub ModoAlta()
        Documento = GenesisSaldos.MaestroDocumentos.CrearDocumento(GenesisSaldos.MaestroFormularios.ObtenerFormulario(IdentificadorFormulario))
        'Base.InformacionUsuario.DelegacionSeleccionada.Plantas(0).Sectores.FirstOrDefault(Function(n) n.Identificador = _IdentificadorSector)
        Documento.CuentaOrigen = New Clases.Cuenta() With {.Sector = SectorSelecionado}
        Documento.FechaHoraPlanificacionCertificacion = DateTime.UtcNow.QuieroExibirEstaFechaEnLaPatalla(Base.InformacionUsuario.DelegacionSeleccionada)
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

        Documento.Elemento = remesa

        If Documento.GrupoTerminosIAC Is Nothing Then
            Documento.GrupoTerminosIAC = If(Documento.Formulario IsNot Nothing, Documento.Formulario.GrupoTerminosIACIndividual, Nothing)
        End If

        Documento.UsuarioCreacion = Parametros.Permisos.Usuario.Login
        Documento.UsuarioModificacion = Parametros.Permisos.Usuario.Login
        Documento.Elemento.UsuarioCreacion = Parametros.Permisos.Usuario.Login
        Documento.Elemento.UsuarioModificacion = Parametros.Permisos.Usuario.Login
    End Sub

    Private Sub ModoModificacion()

        Dim TempoInicial As DateTime = Now
        Dim Tempo As New StringBuilder

        If Session("Documento_" & IdentificadorDocumento) IsNot Nothing Then
            Documento = Session("Documento_" & IdentificadorDocumento)
            Session("Documento_" & IdentificadorDocumento) = Nothing
        Else
            Documento = LogicaNegocio.GenesisSaldos.Documento.recuperarDocumentoPorIdentificador(IdentificadorDocumento, Parametros.Permisos.Usuario.Login, Nothing)
        End If

        Tempo.AppendLine(Now.Subtract(TempoInicial).ToString())

    End Sub

    Private Sub ConfigurarControles()

        ConfiguraAcciones()
        ConfiguraDatosDocumento()
        ConfiguraCuentaOrigen()
        ConfiguraCuentaDestino()
        ConfiguraElemento()
        ConfiguraValores()
        ConfiguraInfAdicionales()
        ConfiguraSaldo()
        ConfiguraMensajeExterna()
        ConfiguraInfContenedor()
    End Sub

    Private Sub ConfiguraMensajeExterna()

        If Not String.IsNullOrEmpty(Documento.MensajeExterno) Then
            dv_MensajeExterno.Visible = True

            If (Not String.IsNullOrEmpty(Documento.MensajeExterno) AndAlso Documento.MensajeExterno.Contains("|")) Then

                Dim listaMesaje As String() = Documento.MensajeExterno.Split("|")
                Dim builder As New StringBuilder("")

                For Each mesaje In listaMesaje
                    mesaje = mesaje.TrimStart(" ")
                    mesaje = mesaje.TrimEnd(" ")
                    builder.AppendLine(mesaje)
                    builder.AppendLine(" <br /> ")
                Next

                lblMensajeExterno.Text = builder.ToString()

            Else
                lblMensajeExterno.Text = Documento.MensajeExterno
            End If


        Else
            dv_MensajeExterno.Visible = False
        End If

    End Sub

    Private Sub ConfiguraDatosDocumento()
        'o if de modal foi retirado para que quando for um documento individual exibir os campos de codigoExterno e data gestion
        DatosDocumento.Documento = Documento
        DatosDocumento.Modo = RetornaModoConsulta()
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
            Me.CuentaOrigen.TipoSitio = Enumeradores.TipoSitio.Origen
            Me.CuentaOrigen.Modo = RetornaModoConsulta()
            Me.CuentaOrigen.SelecaoMultipla = False
            Me.CuentaOrigen.ucSector.identificadorFormulario = Documento.Formulario.Identificador
            If Documento.CuentaOrigen IsNot Nothing Then
                Me.CuentaOrigen.Cuenta = Documento.CuentaOrigen.Clonar()
            End If
            If Documento.CuentaSaldoOrigen IsNot Nothing Then
                Me.CuentaOrigen.CuentaSaldo = Documento.CuentaSaldoOrigen.Clonar()
            End If

            If Caracteristicas.Util.VerificarCaracteristicas(CaracteristicasFormulario,
                New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                        Enumeradores.CaracteristicaFormulario.GestiondeRemesas,
                        Enumeradores.CaracteristicaFormulario.GestiondeBultos)) Then

                If CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Altas) Then

                    'Controle Cliente: Se for Consulta os campos ficam desabilitado
                    Me.CuentaOrigen.ucCliente.ClienteHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)
                    Me.CuentaOrigen.ucCliente.SubClienteHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)
                    Me.CuentaOrigen.ucCliente.PtoServicioHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)

                    'Controle Canal: Se for Consulta os campos ficam desabilitado
                    Me.CuentaOrigen.ucCanal.CanalHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)
                    Me.CuentaOrigen.ucCanal.SubCanalHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)

                End If

            ElseIf CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Cierres) Then

                ' Todos campos ficam desabilitados

            ElseIf CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.GestiondeFondos) Then

                If CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Classificacion) Then

                    'Controle Cliente: Se for Alta os campos ficam habilitados
                    Me.CuentaOrigen.ucCliente.ClienteHabilitado = (Modo = Enumeradores.Modo.Alta)
                    Me.CuentaOrigen.ucCliente.SubClienteHabilitado = (Modo = Enumeradores.Modo.Alta)
                    Me.CuentaOrigen.ucCliente.PtoServicioHabilitado = (Modo = Enumeradores.Modo.Alta)

                    'Controle Canal: Se for Alta os campos ficam habilitados
                    Me.CuentaOrigen.ucCanal.CanalHabilitado = (Modo = Enumeradores.Modo.Alta)
                    Me.CuentaOrigen.ucCanal.SubCanalHabilitado = (Modo = Enumeradores.Modo.Alta)

                Else

                    'Controle Cliente: Se for Consulta os campos ficam desabilitado
                    Me.CuentaOrigen.ucCliente.ClienteHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)
                    Me.CuentaOrigen.ucCliente.SubClienteHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)
                    Me.CuentaOrigen.ucCliente.PtoServicioHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)

                    'Controle Canal: Se for Consulta os campos ficam desabilitado
                    Me.CuentaOrigen.ucCanal.CanalHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)
                    Me.CuentaOrigen.ucCanal.SubCanalHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)

                End If

            ElseIf Caracteristicas.Util.VerificarCaracteristicas(CaracteristicasFormulario,
                New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                        Enumeradores.CaracteristicaFormulario.GestiondeContenedores,
                        Enumeradores.CaracteristicaFormulario.OtrosMovimientos)) Then

                'Controle Cliente: Se for Consulta os campos ficam desabilitado
                Me.CuentaOrigen.ucCliente.ClienteHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)
                Me.CuentaOrigen.ucCliente.SubClienteHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)
                Me.CuentaOrigen.ucCliente.PtoServicioHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)

                'Controle Canal: Se for Consulta os campos ficam desabilitado
                Me.CuentaOrigen.ucCanal.CanalHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)
                Me.CuentaOrigen.ucCanal.SubCanalHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)

            End If


        End If

    End Sub

    Private Sub ConfiguraCuentaDestino()

        Dim _respModo As Enumeradores.Modo = RetornaModoConsulta()

        If _respModo = Enumeradores.Modo.Consulta AndAlso Documento.CuentaOrigen.Identificador <> Documento.CuentaSaldoDestino.Identificador Then

            CuentaDestino.TipoSitio = Enumeradores.TipoSitio.Destino
            CuentaDestino.Modo = _respModo
            CuentaDestino.SelecaoMultipla = False
            Me.CuentaDestino.Cuenta = Documento.CuentaDestino
            If Documento.CuentaSaldoDestino IsNot Nothing Then
                Me.CuentaDestino.CuentaSaldo = Documento.CuentaSaldoDestino.Clonar()
            End If
            Me.CuentaDestino.ucSector.identificadorFormulario = Documento.Formulario.Identificador

        Else

            If Caracteristicas.Util.VerificarCaracteristicas(CaracteristicasFormulario,
            New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                    Enumeradores.CaracteristicaFormulario.GestiondeRemesas,
                    Enumeradores.CaracteristicaFormulario.GestiondeBultos)) AndAlso
                    CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Reenvios) Then


                CuentaDestino.TipoSitio = Enumeradores.TipoSitio.Destino
                CuentaDestino.Modo = _respModo
                CuentaDestino.SelecaoMultipla = False
                If Documento.CuentaDestino Is Nothing Then
                    Documento.CuentaDestino = Documento.CuentaOrigen.Clonar()
                End If
                If Documento.CuentaSaldoDestino IsNot Nothing Then
                    Me.CuentaDestino.CuentaSaldo = Documento.CuentaSaldoDestino.Clonar()
                End If

                Me.CuentaDestino.Cuenta = Documento.CuentaDestino
                Me.CuentaDestino.ucSector.identificadorFormulario = Documento.Formulario.Identificador

                Me.CuentaDestino.ucSector.SectorHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos) AndAlso Not esGrupoDocumento

                If (Me.Modo = Enumeradores.Modo.Alta) AndAlso Not IsPostBack Then
                    Documento.CuentaDestino.Sector.Identificador = Nothing
                    Documento.CuentaDestino.Sector.Descripcion = Nothing
                    Documento.CuentaDestino.Sector.Codigo = Nothing
                End If

                If CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.EntreSectoresdePlantasDelegacionesDiferentes) Then

                    Me.CuentaDestino.ucSector.PlantaHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos) AndAlso Not esGrupoDocumento
                    Me.CuentaDestino.ucSector.DelegacionHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos) AndAlso Not esGrupoDocumento
                    If (Me.Modo = Enumeradores.Modo.Alta) AndAlso Not IsPostBack Then
                        Documento.CuentaDestino.Sector = New Clases.Sector
                    End If
                End If

            ElseIf CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Cierres) AndAlso
            CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.CierreDeCaja) Then

                CuentaDestino.TipoSitio = Enumeradores.TipoSitio.Destino
                CuentaDestino.Modo = RetornaModoConsulta()
                CuentaDestino.SelecaoMultipla = False
                If Documento.CuentaDestino Is Nothing Then
                    Documento.CuentaDestino = Documento.CuentaOrigen.Clonar()
                End If
                Me.CuentaDestino.Cuenta = Documento.CuentaDestino
                If Documento.CuentaSaldoDestino IsNot Nothing Then
                    Me.CuentaDestino.CuentaSaldo = Documento.CuentaSaldoDestino.Clonar()
                End If
                Me.CuentaDestino.ucSector.identificadorFormulario = Documento.Formulario.Identificador

                Me.CuentaDestino.ucSector.SectorHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos) AndAlso Not esGrupoDocumento

                If (Me.Modo = Enumeradores.Modo.Alta) AndAlso Not IsPostBack Then
                    Documento.CuentaDestino.Sector.Identificador = Nothing
                    Documento.CuentaDestino.Sector.Descripcion = Nothing
                    Documento.CuentaDestino.Sector.Codigo = Nothing
                End If

                If CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.EntreSectoresdePlantasDelegacionesDiferentes) Then

                    Me.CuentaDestino.ucSector.PlantaHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos) AndAlso Not esGrupoDocumento
                    Me.CuentaDestino.ucSector.DelegacionHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos) AndAlso Not esGrupoDocumento
                    If (Me.Modo = Enumeradores.Modo.Alta) AndAlso Not IsPostBack Then
                        Documento.CuentaDestino.Sector = New Clases.Sector
                    End If
                End If

            ElseIf CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.GestiondeFondos) AndAlso
               CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.MovimientodeFondos) Then

                CuentaDestino.TipoSitio = Enumeradores.TipoSitio.Destino
                CuentaDestino.Modo = RetornaModoConsulta()
                CuentaDestino.SelecaoMultipla = False
                If Documento.CuentaDestino Is Nothing Then
                    Documento.CuentaDestino = Documento.CuentaOrigen.Clonar()
                End If
                Me.CuentaDestino.Cuenta = Documento.CuentaDestino
                If Documento.CuentaSaldoDestino IsNot Nothing Then
                    Me.CuentaDestino.CuentaSaldo = Documento.CuentaSaldoDestino.Clonar()
                End If
                Me.CuentaDestino.ucSector.identificadorFormulario = Documento.Formulario.Identificador

                If CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.EntreSectoresdePlantasDelegacionesDiferentes) Then

                    If (Me.Modo = Enumeradores.Modo.Alta) AndAlso Not IsPostBack Then
                        Documento.CuentaDestino.Sector = New Clases.Sector
                    End If
                    Me.CuentaDestino.ucSector.DelegacionHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos) AndAlso Not esGrupoDocumento
                    Me.CuentaDestino.ucSector.PlantaHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos) AndAlso Not esGrupoDocumento
                    Me.CuentaDestino.ucSector.SectorHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos) AndAlso Not esGrupoDocumento

                ElseIf CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.EntreSectoresDeLaMismaPlanta) Then

                    If (Me.Modo = Enumeradores.Modo.Alta) AndAlso Not IsPostBack Then
                        Documento.CuentaDestino.Sector.Identificador = Nothing
                        Documento.CuentaDestino.Sector.Descripcion = Nothing
                        Documento.CuentaDestino.Sector.Codigo = Nothing
                    End If

                    Me.CuentaDestino.ucSector.NoExhibirDelegacion = False
                    Me.CuentaDestino.ucSector.NoExhibirPlanta = False
                    Me.CuentaDestino.ucSector.NoExhibirSector = False
                    Me.CuentaDestino.ucSector.SectorHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos) AndAlso Not esGrupoDocumento
                    If esGrupoDocumento AndAlso Me.Documento.CuentaDestino.Sector.Identificador Is Nothing Then
                        Me.CuentaDestino.ucSector.NoExhibirSector = True
                    End If

                ElseIf CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.EntreCanales) Then

                    If (Me.Modo = Enumeradores.Modo.Alta) AndAlso Not IsPostBack Then
                        Me.CuentaDestino.ucCanal.Canales = New ObservableCollection(Of Clases.Canal)
                    End If
                    Me.CuentaDestino.ucCanal.CanalHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)
                    Me.CuentaDestino.ucCanal.SubCanalHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)

                ElseIf CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.EntreClientes) Then

                    If (Me.Modo = Enumeradores.Modo.Alta) AndAlso Not IsPostBack Then
                        Me.CuentaDestino.ucCliente.Clientes = New ObservableCollection(Of Clases.Cliente)
                    End If

                    Me.CuentaDestino.ucCliente.ClienteHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)
                    Me.CuentaDestino.ucCliente.SubClienteHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)
                    Me.CuentaDestino.ucCliente.PtoServicioHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)

                End If

            ElseIf Caracteristicas.Util.VerificarCaracteristicas(CaracteristicasFormulario,
            New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                    Enumeradores.CaracteristicaFormulario.GestiondeContenedores,
                    Enumeradores.CaracteristicaFormulario.OtrosMovimientos)) Then

                CuentaDestino.TipoSitio = Enumeradores.TipoSitio.Destino
                CuentaDestino.Modo = RetornaModoConsulta()
                CuentaDestino.SelecaoMultipla = False
                If Documento.CuentaDestino Is Nothing Then
                    Documento.CuentaDestino = Documento.CuentaOrigen.Clonar()
                End If
                If Documento.CuentaSaldoDestino IsNot Nothing Then
                    Me.CuentaDestino.CuentaSaldo = Documento.CuentaSaldoDestino.Clonar()
                End If
                Me.CuentaDestino.Cuenta = Documento.CuentaDestino
                Me.CuentaDestino.ucSector.identificadorFormulario = Documento.Formulario.Identificador

                Me.CuentaDestino.ucSector.DelegacionHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos) AndAlso Not esGrupoDocumento
                Me.CuentaDestino.ucSector.PlantaHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos) AndAlso Not esGrupoDocumento
                Me.CuentaDestino.ucSector.SectorHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos) AndAlso Not esGrupoDocumento

                'Controle Cliente: Se for Consulta os campos ficam desabilitado
                Me.CuentaDestino.ucCliente.ClienteHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)
                Me.CuentaDestino.ucCliente.SubClienteHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)
                Me.CuentaDestino.ucCliente.PtoServicioHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)

                'Controle Canal: Se for Consulta os campos ficam desabilitado
                Me.CuentaDestino.ucCanal.CanalHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)
                Me.CuentaDestino.ucCanal.SubCanalHabilitado = (Modo <> Enumeradores.Modo.Consulta) AndAlso (Modo <> Enumeradores.Modo.ModificarTerminos)
            End If

        End If

    End Sub

    Private Sub ConfiguraElemento()
        If Caracteristicas.Util.VerificarCaracteristicas(Documento.Formulario.Caracteristicas,
                                                         New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                                                                                                  Enumeradores.CaracteristicaFormulario.GestiondeRemesas,
                                                                                                  Enumeradores.CaracteristicaFormulario.GestiondeBultos),
                                                         New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Or,
                                                                                                  Enumeradores.CaracteristicaFormulario.Altas,
                                                                                                  Enumeradores.CaracteristicaFormulario.Bajas,
                                                                                                  Enumeradores.CaracteristicaFormulario.Actas,
                                                                                                  Enumeradores.CaracteristicaFormulario.Reenvios,
                                                                                                  Enumeradores.CaracteristicaFormulario.Sustitucion)) Then
            'AddHandler Elemento.DetallarElemento, AddressOf Elemento_DetallarElemento
            Elemento.Documento = Documento
            Elemento.Modo = RetornaModoConsulta()
            Elemento.esGestionBulto = CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.GestiondeBultos)

            If CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Actas) OrElse
                CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Sustitucion) Then
                Elemento.TipoValor = Enumeradores.TipoValor.Contado
            Else
                Elemento.TipoValor = Enumeradores.TipoValor.Declarado
            End If


            If Not String.IsNullOrEmpty(DatosDocumento.idCampoNumeroExterno) Then
                Elemento.CargaScriptCodigoExterno(DatosDocumento.idCampoNumeroExterno)
            End If
        End If
    End Sub

    Private Sub ConfiguraValores()
        If Caracteristicas.Util.VerificarCaracteristicas(Documento.Formulario.Caracteristicas,
                                                                     New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.And,
                                                                                                                         Enumeradores.CaracteristicaFormulario.GestiondeFondos),
                                                                     New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                                                                                                                         Enumeradores.CaracteristicaFormulario.Ajustes,
                                                                                                                         Enumeradores.CaracteristicaFormulario.MovimientodeFondos,
                                                                                                                         Enumeradores.CaracteristicaFormulario.Sustitucion)) _
                 OrElse
            Caracteristicas.Util.VerificarCaracteristicas(Documento.Formulario.Caracteristicas,
                                                                     New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.And,
                                                                                                                         Enumeradores.CaracteristicaFormulario.Cierres),
                                                                     New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                                                                                                                         Enumeradores.CaracteristicaFormulario.CierreDeCaja,
                                                                                                                         Enumeradores.CaracteristicaFormulario.CierreDeTesoro)) OrElse
            Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeContenedores) Then

            If (Modo <> Enumeradores.Modo.Consulta AndAlso Modo <> Enumeradores.Modo.ModificarTerminos) OrElse ((Modo = Enumeradores.Modo.Consulta OrElse Modo = Enumeradores.Modo.ModificarTerminos) AndAlso Documento.Divisas IsNot Nothing AndAlso Documento.Divisas.Count > 0) Then
                Valores.Modo = RetornaModoConsulta()
                Valores.Divisas = Documento.Divisas
                Valores.Titulo = Traduzir("033_titulValores")
                Valores.TipoValor = Enumeradores.TipoValor.NoDefinido

            End If

        End If
    End Sub

    Private Sub ConfiguraInfAdicionales()
        If (Documento.GrupoTerminosIAC IsNot Nothing AndAlso Documento.GrupoTerminosIAC.TerminosIAC IsNot Nothing) Then
            InfAdicionales.Modo = Me.Modo
            InfAdicionales.Terminos = Documento.GrupoTerminosIAC.TerminosIAC
        End If
    End Sub

    Private Sub ConfiguraInfContenedor()
        If Documento IsNot Nothing AndAlso Documento.Elemento IsNot Nothing AndAlso Documento.Elemento.GetType() Is GetType(Clases.Contenedor) Then
            InfContenedor.Contenedor = DirectCast(Documento.Elemento, Clases.Contenedor)
        End If
    End Sub


    Private Sub ConfiguraSaldo()
        If Caracteristicas.Util.VerificarCaracteristicas(Documento.Formulario.Caracteristicas,
                                                                     New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.And,
                                                                                                                         Enumeradores.CaracteristicaFormulario.GestiondeFondos),
                                                                     New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                                                                                                                         Enumeradores.CaracteristicaFormulario.Classificacion)) Then
            Saldo.Modo = RetornaModoConsulta()

            If Modo <> Enumeradores.Modo.Alta AndAlso Not Page.IsPostBack Then

                Dim divisasCuadre As ObservableCollection(Of Clases.Divisa) = Nothing
                Dim divisasSaldoAnterior As ObservableCollection(Of Clases.Divisa) = Nothing

                ' task  -   saldo atual da conta
                Dim tfDivisasSaldoActual As Task
                ' task  -   saldo anterior/histórico do documento
                Dim tfDivisasSaldoAnterior As Task
                ' task  -   cuadre entre saldo atual X saldo anterior
                Dim tfDivisasCuadre As Task
                Dim codigoIsoDefecto As String = Parametros.Parametro.CodigoIsoDivisaDefecto
                tfDivisasSaldoActual = New Task(Sub()
                                                    Dim UtilizarTotalizador As Boolean = True
                                                    If Caracteristicas.Util.VerificarCaracteristicas(Documento.Formulario.Caracteristicas,
                                                                                                       New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.And,
                                                                                                       Enumeradores.CaracteristicaFormulario.Classificacion)) Then
                                                        UtilizarTotalizador = False

                                                    End If
                                                    Me.Saldo.Divisas = LogicaNegocio.GenesisSaldos.Saldo.ObtenerSaldoModificar(Documento.CuentaOrigen, UtilizarTotalizador)

                                                End Sub)

                tfDivisasSaldoAnterior = New Task(Sub()

                                                      If Modo = Enumeradores.Modo.Consulta OrElse Modo = Enumeradores.Modo.ModificarTerminos Then
                                                          ' Recuperar saldo anterior pelo documento
                                                          divisasSaldoAnterior = LogicaNegocio.GenesisSaldos.Saldo.ObtenerSaldoAnterior(Documento.Identificador, Enumeradores.Modo.Consulta)
                                                          Documento.DivisasSaldoAnterior = divisasSaldoAnterior

                                                      ElseIf Modo = Enumeradores.Modo.Modificacion Then
                                                          divisasSaldoAnterior = LogicaNegocio.GenesisSaldos.Saldo.ObtenerSaldoAnteriorModificar(Documento.Identificador)

                                                      End If

                                                  End Sub)

                tfDivisasCuadre = New Task(Sub()
                                               If Modo = Enumeradores.Modo.Consulta OrElse Modo = Enumeradores.Modo.ModificarTerminos Then
                                                   divisasCuadre = LogicaNegocio.GenesisSaldos.Saldo.ObtenerCuadreSaldoAtualXAnterior(Documento.Identificador, Documento.CuentaSaldoOrigen.Identificador, Nothing)
                                               End If
                                           End Sub)

                tfDivisasSaldoActual.Start()
                tfDivisasSaldoAnterior.Start()
                tfDivisasCuadre.Start()

                Task.WaitAll(New Task() {tfDivisasSaldoActual, tfDivisasSaldoAnterior, tfDivisasCuadre})

                If Modo = Enumeradores.Modo.Consulta OrElse Modo = Enumeradores.Modo.ModificarTerminos Then

                    Me.Saldo.Divisas = Documento.Divisas
                    Me.Saldo.DivisasAnterior = divisasSaldoAnterior

                    If Documento.Estado = Enumeradores.EstadoDocumento.EnCurso Then
                        CuadreSaldoAnteriorConsultar(Me.Saldo.DivisasAnterior, divisasCuadre)
                    End If

                Else
                    ' agrupar divisas para que tanto controle (Modificar) quanto (Detalhar) tenham mesma quantidade de abas 
                    Dim divisasModificar = AgregarDivisasSaldoActual(Me.Saldo.Divisas, Documento.Divisas)

                    ' actualização do campo detalhar, para saber quais linhas do controle (Modificar) deverão ser marcadas
                    ActualizarCampoDetallarSaldoActualModificar(divisasSaldoAnterior, Me.Saldo.Divisas)

                    Me.Saldo.DivisasAnterior = Me.Saldo.Divisas.Clonar
                    Me.Saldo.Divisas = divisasModificar

                    Comon.Util.BorrarItemsDenominacionesMediosPagoSinValoresCantidades(Me.Saldo.Divisas)

                End If

                ' orderna as listas de divisas para que as abas sejam correspondentes
                OrdenarDivisas()

                Me.Saldo.AtualizarDivisas()

            End If

        End If
    End Sub

    ''' <summary>
    ''' Ordenar as listas de divisas para que o controle de abas esteje correspondentes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OrdenarDivisas()

        Dim codigoIsoDefecto As String = Parametros.Parametro.CodigoIsoDivisaDefecto
        Me.Saldo.Divisas = LogicaNegocio.GenesisSaldos.Saldo.OrdenarDivisasPorCodigoISODefectoEAlfabeto(Me.Saldo.Divisas, codigoIsoDefecto)
        Me.Saldo.DivisasAnterior = LogicaNegocio.GenesisSaldos.Saldo.OrdenarDivisasPorCodigoISODefectoEAlfabeto(Me.Saldo.DivisasAnterior, codigoIsoDefecto)
        'If Me.Saldo.Divisas IsNot Nothing AndAlso Me.Saldo.Divisas.Count > 0 Then
        '    Me.Saldo.Divisas = Me.Saldo.Divisas.OrderBy(Function(e) e.Descripcion).ToObservableCollection()
        'End If
        'If Me.Saldo.DivisasAnterior IsNot Nothing AndAlso Me.Saldo.DivisasAnterior.Count > 0 Then
        '    Me.Saldo.DivisasAnterior = Me.Saldo.DivisasAnterior.OrderBy(Function(e) e.Descripcion).ToObservableCollection()
        'End If

    End Sub

    Private Function AgregarDivisasSaldoActual(divisasSaldoActual As ObservableCollection(Of Clases.Divisa),
                                               ByRef divisasDocumentoEnCurso As ObservableCollection(Of Clases.Divisa)) As ObservableCollection(Of Clases.Divisa)

        Dim divisas As New ObservableCollection(Of Clases.Divisa)

        If divisasSaldoActual IsNot Nothing AndAlso divisasSaldoActual.Count > 0 AndAlso divisasDocumentoEnCurso IsNot Nothing AndAlso divisasDocumentoEnCurso.Count > 0 Then

            Dim auxSaldoActual = divisasSaldoActual.Clonar
            Comon.Util.ZerarValoresDivisas(auxSaldoActual, True)

            divisas.AddRange(auxSaldoActual)

            For indice = 0 To divisas.Count - 1

                Dim idx As Integer = indice

                Dim divEnCurso = divisasDocumentoEnCurso.FirstOrDefault(Function(e) e.Identificador = divisas(idx).Identificador)

                If divEnCurso IsNot Nothing Then

                    If divisas(idx).Denominaciones IsNot Nothing AndAlso divisas(idx).Denominaciones.Count > 0 Then

                        If divEnCurso.Denominaciones IsNot Nothing AndAlso divEnCurso.Denominaciones.Count > 0 Then

                            'Recuperar os obejtos que estão na coleção de divisas(saldo atual) mas não estão na coleçao divisaEnCurso( saldo do documento em curso)
                            Dim denominaciones = (From dt In divisas(idx).Denominaciones Join ct In
                                                 (From dtp In divisas(idx).Denominaciones Select dtp.Identificador).Except _
                                                 (From ctp In divEnCurso.Denominaciones Select ctp.Identificador)
                                                  On dt.Identificador Equals ct
                                                  Select dt).ToObservableCollection

                            If denominaciones IsNot Nothing AndAlso denominaciones.Count > 0 Then
                                divEnCurso.Denominaciones.AddRange(denominaciones)
                            End If

                        Else
                            divEnCurso.Denominaciones.AddRange(divisas(idx).Denominaciones)
                        End If

                    End If

                    If divisas(idx).MediosPago IsNot Nothing AndAlso divisas(idx).MediosPago.Count > 0 Then

                        If divEnCurso.MediosPago IsNot Nothing AndAlso divEnCurso.MediosPago.Count > 0 Then

                            'Recuperar os obejtos que estão na coleção de divisas(saldo atual) mas não estão na coleçao divisaEnCurso( saldo do documento em curso)
                            Dim mediosPagos = (From dt In divisas(idx).MediosPago Join ct In
                                              (From dtp In divisas(idx).MediosPago Select dtp.Identificador).Except _
                                              (From ctp In divEnCurso.MediosPago Select ctp.Identificador)
                                               On dt.Identificador Equals ct
                                               Select dt).ToObservableCollection

                            If mediosPagos IsNot Nothing AndAlso mediosPagos.Count > 0 Then
                                divEnCurso.MediosPago.AddRange(mediosPagos)
                            End If
                        Else
                            divEnCurso.MediosPago = divisas(idx).MediosPago
                        End If

                    End If

                    divisas(idx) = divEnCurso
                End If

            Next

        End If

        Return divisas

    End Function

    Private Sub ConfiguraAcciones()

        AddHandler Acciones.onAccionGuardar, AddressOf Acciones_onAccionGuardar
        AddHandler Acciones.onAccionGuardarConfirmar, AddressOf Acciones_onAccionGuardarConfirmar
        AddHandler Acciones.onAccionConfirmar, AddressOf Acciones_onAccionConfirmar
        AddHandler Acciones.onAccionAceptar, AddressOf Acciones_onAccionAceptar
        AddHandler Acciones.onAccionRechazar, AddressOf Acciones_onAccionRechazar
        AddHandler Acciones.onAccionCancelar, AddressOf Acciones_onAccionCancelar
        AddHandler Acciones.onAccionAnular, AddressOf Acciones_onAccionAnular
        AddHandler Acciones.onAccionVisualizar, AddressOf Acciones_onAccionVisualizar
        AddHandler Acciones.onAccionModificar, AddressOf Acciones_onAccionModificar
        AddHandler Acciones.onAccionModificarTerminos, AddressOf Acciones_onAccionModificarTerminos
        AddHandler Acciones.onAccionSalvarTerminos, AddressOf Acciones_onAccionSalvarTerminos
        AddHandler Acciones.onAccionAgregarDocumento, AddressOf Acciones_onAccionAgregarDocumento
        'AddHandler Acciones.onAccionAgregarBulto, AddressOf Acciones_onAccionAgregarBulto
        'AddHandler Acciones.onAccionAgregarParcial, AddressOf Acciones_onAccionAgregarParcial

        Dim esAltas As Boolean = Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Altas)
        Dim esAjustes As Boolean = Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Ajustes)
        Dim esMovimientodeFondos As Boolean = Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.MovimientodeFondos)
        Dim esClassificacion As Boolean = Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Classificacion)
        Dim permiteModificarTermino As Boolean = Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.ModificarTerminos)
        Dim sectorOrigenIgualDestino As Boolean
        Dim sectorLogadoIgualDestino As Boolean

        If esClassificacion Then

            Dim _Diccionario As New Dictionary(Of String, String)() From {
                {"msg_valoresinsuficienteefectivos", Traduzir("072_msg_valoresInsuficienteEfectivos")},
                {"msg_valoresinsuficientesmediospago", Traduzir("072_msg_valoresInsuficientesMediosPago")},
                {"msg_sinvalores", Traduzir("072_msg_sinValores")}
            }

            If (Modo = Enumeradores.Modo.Consulta OrElse Modo = Enumeradores.Modo.ModificarTerminos) AndAlso Documento.Estado = Enumeradores.EstadoDocumento.EnCurso Then
                Acciones.registrarScriptBotonConfirmar("validarSaldoClasificacion('" & Documento.Identificador & "', '" & Documento.CuentaSaldoDestino.Identificador & "', '" & Acciones.UniqueIDbtnConfirmar & "')")
            End If

            Acciones.registrarScriptBotonGrabar("validarValoresClasificacion(JSON.parse('" & JsonConvert.SerializeObject(_Diccionario) & "'), '" & Saldo.dvUcSaldoEfectivoMPModificar & "', '" & Saldo.pnlSaldoEfectivoModificar & "', '" & Saldo.pnlSaldoEfectivoDetallar & "', '" & Saldo.pnlSaldoMedioPagoModificar & "', '" & Saldo.pnlSaldoMedioPagoDetallar & "')")

        End If

        If Documento.CuentaOrigen IsNot Nothing AndAlso Documento.CuentaOrigen.Sector IsNot Nothing AndAlso
             Not String.IsNullOrEmpty(Documento.CuentaOrigen.Sector.Codigo) AndAlso Documento.CuentaDestino IsNot Nothing _
             AndAlso Documento.CuentaDestino.Sector IsNot Nothing AndAlso Not String.IsNullOrEmpty(Documento.CuentaDestino.Sector.Codigo) Then
            sectorOrigenIgualDestino = (Documento.CuentaOrigen.Sector.Codigo = Documento.CuentaDestino.Sector.Codigo)
        End If

        If Documento.CuentaDestino IsNot Nothing AndAlso Documento.CuentaDestino.Sector IsNot Nothing AndAlso
             Not String.IsNullOrEmpty(Documento.CuentaDestino.Sector.Codigo) Then
            sectorLogadoIgualDestino = (Documento.CuentaDestino.Sector.Codigo = SectorSelecionado.Codigo)
        End If

        If Not SectorHijo OrElse (SectorHijo AndAlso Base.PossuiPermissao(Aplicacao.Util.Utilidad.eTelas.MANTENIMIENTO_DOCUMENTOS_HIJOS)) Then

            Select Case Documento.Estado
                Case Comon.Enumeradores.EstadoDocumento.Nuevo
                    If Modo <> Enumeradores.Modo.Consulta Then
                        Acciones.btnGuardarVisible = True
                        If Not EsModal Then
                            If esClassificacion Then
                                Acciones.btnGuardarConfirmarVisible = False
                            Else
                                Acciones.btnGuardarConfirmarVisible = True
                            End If

                        End If
                    End If
                Case Comon.Enumeradores.EstadoDocumento.EnCurso
                    If Modo <> Enumeradores.Modo.Consulta Then
                        Acciones.btnGuardarVisible = True
                    Else
                        If Not EsModal AndAlso (esAltas OrElse esAjustes OrElse esMovimientodeFondos OrElse esClassificacion) Then

                            Acciones.btnModificarVisible = True

                            Acciones.btnAnularVisible = True
                            Acciones.btnConfirmarVisible = True
                        End If
                    End If
                Case Comon.Enumeradores.EstadoDocumento.Confirmado
                    If sectorLogadoIgualDestino AndAlso Not EsModal AndAlso Modo <> Enumeradores.Modo.ModificarTerminos Then
                        Acciones.btnAceptarVisible = True
                        Acciones.btnRechazarVisible = True
                    End If
                    If permiteModificarTermino AndAlso Modo <> Enumeradores.Modo.ModificarTerminos Then
                        Acciones.btnModificarTerminoVisible = True
                    End If
                Case Enumeradores.EstadoDocumento.Aceptado
                    If permiteModificarTermino AndAlso Modo <> Enumeradores.Modo.ModificarTerminos Then
                        Acciones.btnModificarTerminoVisible = True
                    End If
                Case Enumeradores.EstadoDocumento.Sustituido
                    If permiteModificarTermino AndAlso Modo <> Enumeradores.Modo.ModificarTerminos Then
                        Acciones.btnModificarTerminoVisible = True
                    End If
                Case Enumeradores.EstadoDocumento.Rechazado
                    If permiteModificarTermino AndAlso Modo <> Enumeradores.Modo.ModificarTerminos Then
                        Acciones.btnModificarTerminoVisible = True
                    End If
            End Select

        End If

        If Modo = Enumeradores.Modo.ModificarTerminos Then
            Acciones.btnSalvarTerminoVisible = True
        End If

        If Documento.Estado <> Enumeradores.EstadoDocumento.Nuevo AndAlso Documento.Estado <> Enumeradores.EstadoDocumento.EnCurso AndAlso
           Documento.Estado <> Enumeradores.EstadoDocumento.Anulado AndAlso Documento.Formulario.TieneImpresion Then

            Acciones.btnImprimirVisible = True
            Dim reporte As String = Prosegur.Genesis.Utilidad.Util.RecuperarNomeRelatorio(Documento)
            Acciones.registrarScriptBotonImprimir(Documento.CodigoComprobante, reporte)

        End If

        'Seta o botão default do formulário para ao submetê-lo rodar o evento do botão guardar.
        Me.Form.DefaultButton = Me.Acciones.FindControl("btnGuardar").FindControl("btnBoton").UniqueID

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
            Documento.Estado = Enumeradores.EstadoDocumento.Aceptado
            Documento.UsuarioModificacion = Parametros.Permisos.Usuario.Login
            GenesisSaldos.MaestroDocumentos.GuardarDocumento(Documento, True, False, bol_gestion_bulto, Nothing, Nothing)
            Response.Redireccionar(Page.AppRelativeVirtualPath & "?IdentificadorSector=" & IdentificadorSector & "&Modo=" & Enumeradores.Modo.Consulta.ToString() & "&IdentificadorDocumento=" & Documento.Identificador)
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion

            If ex.Message = Traduzir("msgconcurrenciasobreregistro") Then
                MyBase.MostraMensagemErroConScript(Traduzir("msgactualizarpantalla"), "window.location.href = " & Chr(34) & "Documento.aspx?IdentificadorSector=" & IdentificadorSector & "&IdentificadorDocumento=" & Documento.Identificador & "&Modo=Consulta" & Chr(34))
            Else
                MyBase.MostraMensagemErro(ex.Descricao.ToString())
            End If

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionAnular()
        Try
            Documento.Estado = Enumeradores.EstadoDocumento.Anulado
            Documento.UsuarioModificacion = Parametros.Permisos.Usuario.Login
            If EsModal Then
                CerrarPopup(NombrePopupModal, EnumeradoresPantalla.AccionPopupCerrado.Anular.ToString())
            Else
                GenesisSaldos.MaestroDocumentos.GuardarDocumento(Documento, True, False, bol_gestion_bulto, Nothing, Nothing)
                Response.Redireccionar("~/Pantallas/Formularios.aspx")
                'Response.Redirect(Page.AppRelativeVirtualPath & "?Modo=" & Enumeradores.Modo.Consulta.ToString() & "&IdentificadorDocumento=" & Documento.Identificador)
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            If ex.Message = Traduzir("msgconcurrenciasobreregistro") Then
                MyBase.MostraMensagemErroConScript(Traduzir("msgactualizarpantalla"), "window.location.href = " & Chr(34) & "Documento.aspx?IdentificadorSector=" & IdentificadorSector & "&IdentificadorDocumento=" & Documento.Identificador & "&Modo=Consulta" & Chr(34))
            Else
                MyBase.MostraMensagemErro(ex.Descricao.ToString())
            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionCancelar()
        Try
            If EsModal Then
                CerrarPopup(NombrePopupModal, EnumeradoresPantalla.AccionPopupCerrado.Cancelar.ToString())
            Else
                If Modo = Enumeradores.Modo.Modificacion Then
                    Response.Redireccionar(Page.AppRelativeVirtualPath & "?IdentificadorSector=" & IdentificadorSector & "&Modo=" & Enumeradores.Modo.Consulta.ToString() & "&IdentificadorDocumento=" & Documento.Identificador)
                Else 'Alta
                    If Master.Historico.Count > 1 Then

                        Dim pagina = Master.Historico(Master.Historico.Count - 2).Key

                        If pagina.Contains("Formularios.aspx") Then

                            Response.Redireccionar("~/Pantallas/Formularios.aspx?IdentificadorSector=" & IdentificadorSector)

                        ElseIf pagina.Contains("Documentos.aspx") Then
                            Dim estadoTela = String.Empty

                            estadoTela = "oid_d=" + Request.QueryString("oid_d")
                            estadoTela = estadoTela + "&oid_s=" + Request.QueryString("oid_s")
                            estadoTela = estadoTela + "&ddl_s=" + Request.QueryString("ddl_s")
                            estadoTela = estadoTela + "&ddl_e=" + Request.QueryString("ddl_e")
                            estadoTela = estadoTela + "&ddl_d=" + Request.QueryString("ddl_d")
                            estadoTela = estadoTela + "&ddl_b=" + Request.QueryString("ddl_b")
                            estadoTela = estadoTela + "&dt_de=" + Request.QueryString("dt_de")
                            estadoTela = estadoTela + "&dt_ate=" + Request.QueryString("dt_ate")
                            estadoTela = estadoTela + "&num_de=" + Request.QueryString("num_de")
                            estadoTela = estadoTela + "&num_ate=" + Request.QueryString("num_ate")
                            Response.Redireccionar("~/Pantallas/Documentos.aspx?" + estadoTela)
                        End If



                        'Dim estadoTela = String.Empty
                        'If Request.QueryString("oid_d") IsNot Nothing Then
                        '    If pagina.Contains(Request.QueryString("oid_d")) Then
                        '        pagina = pagina.Substring(0, pagina.IndexOf(Request.QueryString("oid_d")))

                        '    End If
                        '    estadoTela = "&oid_d=" + Request.QueryString("oid_d")
                        '    estadoTela = estadoTela + "&oid_s=" + Request.QueryString("oid_d")
                        '    estadoTela = estadoTela + "&ddl_s=" + Request.QueryString("ddl_s")
                        '    estadoTela = estadoTela + "&ddl_e=" + Request.QueryString("ddl_e")
                        '    estadoTela = estadoTela + "&ddl_d=" + Request.QueryString("ddl_d")
                        '    estadoTela = estadoTela + "&ddl_b=" + Request.QueryString("ddl_b")
                        '    estadoTela = estadoTela + "&dt_de=" + Request.QueryString("dt_de")
                        '    estadoTela = estadoTela + "&dt_ate=" + Request.QueryString("dt_ate")
                        '    estadoTela = estadoTela + "&num_de=" + Request.QueryString("num_de")
                        '    estadoTela = estadoTela + "&num_ate=" + Request.QueryString("num_ate")


                        'End If
                        'If pagina.Substring(pagina.Length - 5).ToUpper.Equals(".ASPX") Then
                        '    pagina = pagina + "?"
                        'End If

                        'If Not pagina.ToUpper.Contains("IDENTIFICADORSECTOR") Then
                        '    If Not pagina.Substring(pagina.Length - 5).ToUpper.Equals(".ASPX?") Then
                        '        pagina = pagina + "&"
                        '    End If
                        '    pagina = pagina + "IdentificadorSector=" & IdentificadorSector
                        'End If
                        'Response.Redireccionar(pagina + estadoTela)
                    Else
                        Response.Redireccionar(Constantes.NOME_PAGINA_MENU)
                    End If
                End If
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionConfirmar()
        Try
            Documento.Estado = Enumeradores.EstadoDocumento.Confirmado
            Documento.UsuarioModificacion = Parametros.Permisos.Usuario.Login
            GenesisSaldos.MaestroDocumentos.GuardarDocumento(Documento, True, False, bol_gestion_bulto, Nothing, Nothing)

            If CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Altas) OrElse CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.CierreDeCaja) Then
                Session("Documento_" & Documento.Identificador) = Documento
            End If

            If Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.AlConfirmarElDocumentoSeImprime) Then
                Response.Redireccionar(Page.AppRelativeVirtualPath & "?IdentificadorSector=" & IdentificadorSector & "&Modo=" & Enumeradores.Modo.Consulta.ToString() & "&IdentificadorDocumento=" & Documento.Identificador & "&SeImprime=" & Boolean.TrueString, True)
            Else
                Response.Redireccionar(Page.AppRelativeVirtualPath & "?IdentificadorSector=" & IdentificadorSector & "&Modo=" & Enumeradores.Modo.Consulta.ToString() & "&IdentificadorDocumento=" & Documento.Identificador & "&SeImprime=" & Boolean.FalseString, True)
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            If ex.Message = Traduzir("msgconcurrenciasobreregistro") Then
                MyBase.MostraMensagemErroConScript(Traduzir("msgactualizarpantalla"), "window.location.href = " & Chr(34) & "Documento.aspx?IdentificadorSector=" & IdentificadorSector & "&IdentificadorDocumento=" & Documento.Identificador & "&Modo=Consulta" & Chr(34))
            Else
                MyBase.MostraMensagemErro(ex.Descricao.ToString())
            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionGuardar()
        Try
            Dim resultadoValidacao As List(Of String) = ValidarControles()
            If resultadoValidacao.Count > 0 Then
                MyBase.MostraMensagemErro(String.Join(Environment.NewLine, resultadoValidacao.ToArray()))
            Else
                ActualizaObjetoDocumento()
                ValidarFechaCertificacion()
                ValidarCodigoExterno()
                If EsModal Then
                    Me.ActualizarCache(ClaveDocumento, Documento)
                    CerrarPopup(NombrePopupModal, EnumeradoresPantalla.AccionPopupCerrado.Guardar.ToString())
                Else
                    If CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Altas) Then
                        If Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeRemesas) Then
                            GenesisSaldos.Documento.calcularValorDelDocumentoAltas(Documento, False)
                        ElseIf Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeBultos) Then
                            GenesisSaldos.Documento.calcularValorDelDocumentoAltas(Documento, True)
                        End If

                    ElseIf CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Classificacion) Then
                        ' corrige todos os valores que tenham sido mal informados no nível de denominações
                        Comon.Util.CorrigeImporteCantidadDenominaciones(Documento)

                    End If

                    Documento.UsuarioModificacion = Parametros.Permisos.Usuario.Login
                    GenesisSaldos.MaestroDocumentos.GuardarDocumento(Documento, True, False, bol_gestion_bulto, Nothing, Nothing)

                    Response.Redireccionar(Page.AppRelativeVirtualPath & "?IdentificadorSector=" & IdentificadorSector & "&Modo=" & Enumeradores.Modo.Consulta.ToString() & "&IdentificadorDocumento=" & Documento.Identificador)

                End If
            End If
        Catch ex As Excepciones.ExcepcionLogica
            MyBase.MostraMensagemErro(ex.Message)
        Catch ex As Excepcion.NegocioExcepcion
            If ex.Message = Traduzir("msgconcurrenciasobreregistro") Then
                MyBase.MostraMensagemErroConScript(Traduzir("msgactualizarpantalla"), "window.location.href = " & Chr(34) & "Documento.aspx?IdentificadorSector=" & IdentificadorSector & "&IdentificadorDocumento=" & Documento.Identificador & "&Modo=Consulta" & Chr(34))
            Else
                MyBase.MostraMensagemErro(ex.Descricao.ToString())
            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionGuardarConfirmar()
        Try
            Dim resultadoValidacao As List(Of String) = ValidarControles()
            If resultadoValidacao.Count > 0 Then
                MyBase.MostraMensagemErro(String.Join(Environment.NewLine, resultadoValidacao.ToArray()))
            Else
                ActualizaObjetoDocumento()
                ValidarFechaCertificacion()
                ValidarCodigoExterno()
                If EsModal Then
                    Me.ActualizarCache(ClaveDocumento, Documento)
                    CerrarPopup(NombrePopupModal, EnumeradoresPantalla.AccionPopupCerrado.Guardar.ToString())
                Else
                    Documento.UsuarioModificacion = Parametros.Permisos.Usuario.Login

                    If CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Altas) Then
                        If Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeRemesas) Then
                            GenesisSaldos.Documento.calcularValorDelDocumentoAltas(Documento, False)
                        ElseIf Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeBultos) Then
                            GenesisSaldos.Documento.calcularValorDelDocumentoAltas(Documento, True)
                        End If
                    End If

                    GenesisSaldos.MaestroDocumentos.GuardarDocumento(Documento, True, True, bol_gestion_bulto, Nothing, Nothing)

                    If CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Altas) Then
                        Session("Documento_" & Documento.Identificador) = Documento
                    End If

                    Response.Redireccionar(Page.AppRelativeVirtualPath & "?IdentificadorSector=" & IdentificadorSector & "&Modo=" & Enumeradores.Modo.Consulta.ToString() & "&IdentificadorDocumento=" & Documento.Identificador)
                End If
            End If
        Catch ex As Excepciones.ExcepcionLogica
            MyBase.MostraMensagemErro(ex.Message)
        Catch ex As Excepcion.NegocioExcepcion
            If ex.Message = Traduzir("msgconcurrenciasobreregistro") Then
                MyBase.MostraMensagemErroConScript(Traduzir("msgactualizarpantalla"), "window.location.href = " & Chr(34) & "Documento.aspx?IdentificadorSector=" & IdentificadorSector & "&IdentificadorDocumento=" & Documento.Identificador & "&Modo=Consulta" & Chr(34))
            Else
                MyBase.MostraMensagemErro(ex.Descricao.ToString())
            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionImprimir()
        Try
            Dim reporte As String = Prosegur.Genesis.Utilidad.Util.RecuperarNomeRelatorio(Documento)
            If String.IsNullOrEmpty(reporte) Then
                '* Não existe implementação no sistema para documentos do tipo “Gestão de Contenedores” 
                'e “Outros Movimentos”. Por este motivo, a impressão não estará preparada para trabalhar com os mesmos neste momento. 
                Return
            End If

            ClaveDocumentoPopup = Me.RegistrarCache(Documento)
            Me.AbrirPopup("DocumentoImpresion.aspx" & "?COD_COMPROBANTE=" & Documento.CodigoComprobante & "&NombrePopupModal=" & EnumeradoresPantalla.NombrePopupModal.DocumentoImpresion.ToString & "&IDReporte=" & reporte & "&")


        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionModificar()
        Try
            Response.Redireccionar(Page.AppRelativeVirtualPath & "?IdentificadorSector=" & IdentificadorSector & "&Modo=" & Enumeradores.Modo.Modificacion.ToString() & "&IdentificadorDocumento=" & Documento.Identificador)
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionModificarTerminos()
        Try
            If (EsModal) Then
                Response.Redireccionar(Page.AppRelativeVirtualPath & "?IdentificadorSector=" & IdentificadorSector & "&Modo=" & Enumeradores.Modo.ModificarTerminos.ToString() & "&EsPopup=" & Request.QueryString("EsPopup") & "&NombrePopupModal=" & Request.QueryString("NombrePopupModal") & "&IdentificadorDocumento=" & Documento.Identificador)
            Else
                Response.Redireccionar(Page.AppRelativeVirtualPath & "?IdentificadorSector=" & IdentificadorSector & "&Modo=" & Enumeradores.Modo.ModificarTerminos.ToString() & "&IdentificadorDocumento=" & Documento.Identificador)
            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionSalvarTerminos()
        Try
            Dim resultadoValidacao As List(Of String) = ValidarControles()
            If resultadoValidacao.Count > 0 Then
                MyBase.MostraMensagemErro(String.Join(Environment.NewLine, resultadoValidacao.ToArray()))
            Else
                If InfAdicionales IsNot Nothing Then
                    InfAdicionales.GuardarDatos()
                End If

                If EsModal Then
                    If Modo = Enumeradores.Modo.ModificarTerminos Then
                        Documento.UsuarioModificacion = Parametros.Permisos.Usuario.Login
                        LogicaNegocio.Genesis.TerminoIAC.ActualizarValoresTerminoDocumento(Documento, Parametros.Permisos.Usuario.Login)
                        Response.Redireccionar(Page.AppRelativeVirtualPath & "?IdentificadorSector=" & IdentificadorSector & "&Modo=" & Enumeradores.Modo.Consulta.ToString() & "&EsPopup=" & Request.QueryString("EsPopup") & "&NombrePopupModal=" & Request.QueryString("NombrePopupModal") & "&IdentificadorDocumento=" & Documento.Identificador)
                        'Response.Redireccionar(Page.AppRelativeVirtualPath & "?Modo=" & Enumeradores.Modo.Consulta.ToString() & "&IdentificadorDocumento=" & Documento.Identificador)
                    Else
                        Me.ActualizarCache(ClaveDocumento, Documento)
                        CerrarPopup(NombrePopupModal, EnumeradoresPantalla.AccionPopupCerrado.Guardar.ToString())
                    End If
                Else
                    Documento.UsuarioModificacion = Parametros.Permisos.Usuario.Login
                    LogicaNegocio.Genesis.TerminoIAC.ActualizarValoresTerminoDocumento(Documento, Parametros.Permisos.Usuario.Login)
                    Response.Redireccionar(Page.AppRelativeVirtualPath & "?IdentificadorSector=" & IdentificadorSector & "&Modo=" & Enumeradores.Modo.Consulta.ToString() & "&IdentificadorDocumento=" & Documento.Identificador)
                End If
            End If
        Catch ex As Excepciones.ExcepcionLogica
            MyBase.MostraMensagemErro(ex.Message)
        Catch ex As Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Message)
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionAgregarDocumento()
        Try
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionRechazar()
        Try
            Documento.Estado = Enumeradores.EstadoDocumento.Rechazado
            Documento.UsuarioModificacion = Parametros.Permisos.Usuario.Login
            GenesisSaldos.MaestroDocumentos.GuardarDocumento(Documento, True, False, bol_gestion_bulto, Nothing, Nothing)
            Response.Redireccionar(Page.AppRelativeVirtualPath & "?IdentificadorSector=" & IdentificadorSector & "&Modo=" & Enumeradores.Modo.Consulta.ToString() & "&IdentificadorDocumento=" & Documento.Identificador)
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            If ex.Message = Traduzir("msgconcurrenciasobreregistro") Then
                MyBase.MostraMensagemErroConScript(Traduzir("msgactualizarpantalla"), "window.location.href = " & Chr(34) & "Documento.aspx?IdentificadorSector=" & IdentificadorSector & "&IdentificadorDocumento=" & Documento.Identificador & "&Modo=Consulta" & Chr(34))
            Else
                MyBase.MostraMensagemErro(ex.Descricao.ToString())
            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub Acciones_onAccionVisualizar()
        Try
            ' O que fazer ???
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagemErro(If(TypeOf e.Erro Is Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub

    Private Function AgregarNuevoDocumentoElemento(Of TElemento As Clases.Elemento)(Elemento As TElemento) As Clases.Documento
        If Not (GetType(TElemento) Is GetType(Clases.Bulto) OrElse GetType(TElemento) Is GetType(Clases.Parcial)) Then
            Throw New InvalidCastException(GetType(TElemento).ToString())
        End If

        Dim nuevoDocumento As Clases.Documento = Documento.Clonar()
        nuevoDocumento.Identificador = System.Guid.NewGuid().ToString()
        nuevoDocumento.Elemento = Elemento
        If GetType(TElemento) Is GetType(Clases.Bulto) Then
            If ElementoRemesa.Bultos Is Nothing Then
                ElementoRemesa.Bultos = New ObservableCollection(Of Clases.Bulto)()
            End If
        Else
            If ElementoBulto.Parciales Is Nothing Then
                ElementoBulto.Parciales = New ObservableCollection(Of Clases.Parcial)()
            End If
        End If
        nuevoDocumento.Elemento.Identificador = System.Guid.NewGuid().ToString()

        nuevoDocumento.UsuarioCreacion = Parametros.Permisos.Usuario.Login
        nuevoDocumento.UsuarioModificacion = Parametros.Permisos.Usuario.Login
        nuevoDocumento.Elemento.UsuarioCreacion = Parametros.Permisos.Usuario.Login
        nuevoDocumento.Elemento.UsuarioModificacion = Parametros.Permisos.Usuario.Login

        Return nuevoDocumento

    End Function

    Private Function ValidarControles() As List(Of String)
        'Valida se os controles obrigatorios estãom preenchidos e se os valores são válidos.
        Dim resultadoValidacao As New List(Of String)()
        If Caracteristicas.Util.VerificarCaracteristicas(Documento.Formulario.Caracteristicas,
                                                         New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                                                                                                  Enumeradores.CaracteristicaFormulario.GestiondeRemesas,
                                                                                                  Enumeradores.CaracteristicaFormulario.GestiondeBultos)) Then

            If Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Altas) Then

                'ConfiguraFormulario()
                'Não valida nada. Somente exibição.

                If Me.CuentaOrigen IsNot Nothing Then
                    resultadoValidacao.AddRange(CuentaOrigen.ValidarControl())
                End If

                'ConfiguraDatosDocumento()
                If (_DatosDocumento IsNot Nothing) Then
                    resultadoValidacao.AddRange(_DatosDocumento.ValidarControl())
                End If

                'ConfiguraElemento()
                resultadoValidacao.AddRange(Elemento.ValidarControl())

                ''ConfiguraInformacionAdicional()
                'resultadoValidacao.AddRange(InformacionAdicional.ValidarControl())

                'ConfiguraAcciones()
                'Não valida nada. Somente exibição.

            ElseIf Caracteristicas.Util.VerificarCaracteristicas(Documento.Formulario.Caracteristicas,
                                                     New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Or,
                                                                                              Enumeradores.CaracteristicaFormulario.Bajas,
                                                                                              Enumeradores.CaracteristicaFormulario.Actas)) Then

                'ConfiguraFormulario()
                'Não valida nada. Somente exibição.

                'ConfiguraDatosDocumento()
                If (_DatosDocumento IsNot Nothing) Then
                    resultadoValidacao.AddRange(_DatosDocumento.ValidarControl())
                End If

                'Filtro???

                'Lista de Elementos???

                'ConfiguraElemento()
                resultadoValidacao.AddRange(Elemento.ValidarControl())

                ''ConfiguraInformacionAdicional()
                'resultadoValidacao.AddRange(InformacionAdicional.ValidarControl())

                'ConfiguraAcciones()
                'Não valida nada. Somente exibição.

            ElseIf Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Reenvios) Then

                'ConfiguraFormulario()
                'Não valida nada. Somente exibição.

                'ConfiguraDatosDocumento()
                If (_DatosDocumento IsNot Nothing) Then
                    resultadoValidacao.AddRange(_DatosDocumento.ValidarControl())
                End If

                If Me.CuentaDestino IsNot Nothing Then
                    resultadoValidacao.AddRange(CuentaDestino.ValidarControl())
                End If

                'Filtro???

                'Lista de Elementos???

                'ConfiguraElemento()
                resultadoValidacao.AddRange(Elemento.ValidarControl())

                ''ConfiguraInformacionAdicional()
                'resultadoValidacao.AddRange(InformacionAdicional.ValidarControl())

                'ConfiguraAcciones()
                'Não valida nada. Somente exibição.

            End If
        ElseIf Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeFondos) Then

            If (_DatosDocumento IsNot Nothing) Then
                resultadoValidacao.AddRange(_DatosDocumento.ValidarControl())
            End If

            If _CuentaOrigen IsNot Nothing Then
                resultadoValidacao.AddRange(CuentaOrigen.ValidarControl())
            End If

            If Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.MovimientodeFondos) Then
                If _CuentaDestino IsNot Nothing Then
                    resultadoValidacao.AddRange(CuentaDestino.ValidarControl())
                End If
            End If

            ' se não contem caracteristica de classificação
            If Not Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Classificacion) Then

                If _Valores IsNot Nothing Then
                    resultadoValidacao.AddRange(Valores.ValidarControl())
                End If

            Else
                If _Saldo IsNot Nothing Then
                    resultadoValidacao.AddRange(Saldo.ValidarControl())
                End If

            End If

        End If

        Return resultadoValidacao

    End Function

    Private Sub ActualizaObjetoDocumento()
        Dim esDocumentoDevalor As Boolean = True

        If InfAdicionales IsNot Nothing Then
            InfAdicionales.GuardarDatos()
        End If

        If (_DatosDocumento IsNot Nothing) Then
            _DatosDocumento.GuardarDatos()
        End If

        If Caracteristicas.Util.VerificarCaracteristicas(CaracteristicasFormulario,
                           New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                                                                    Enumeradores.CaracteristicaFormulario.GestiondeRemesas,
                                                                    Enumeradores.CaracteristicaFormulario.GestiondeBultos)) Then
            esDocumentoDevalor = False
        End If

        If _CuentaOrigen IsNot Nothing Then
            RecuperarGenerarCuenta(Documento.CuentaOrigen, Documento.CuentaSaldoOrigen, esDocumentoDevalor)
        End If

        If Caracteristicas.Util.VerificarCaracteristicas(CaracteristicasFormulario,
                           New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                                                                    Enumeradores.CaracteristicaFormulario.GestiondeRemesas,
                                                                    Enumeradores.CaracteristicaFormulario.GestiondeBultos)) Then

            Elemento.GuardarDatos()

            If CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Reenvios) AndAlso
                CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.GestiondeRemesas) Then

                Elemento.ActualizarDatos()

            End If

            If _CuentaDestino IsNot Nothing Then
                RecuperarGenerarCuenta(Documento.CuentaDestino, Documento.CuentaSaldoDestino, esDocumentoDevalor)
            Else
                Documento.CuentaDestino = Documento.CuentaOrigen.Clonar
                Documento.CuentaSaldoDestino = Documento.CuentaSaldoOrigen.Clonar
            End If

            ActualizaElementosDocumento(Documento.Elemento, Nothing)
            RemoverDocumentoRelacionados()

        ElseIf Caracteristicas.Util.VerificarCaracteristicas(CaracteristicasFormulario,
                               New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.And,
                                                                        Enumeradores.CaracteristicaFormulario.GestiondeFondos),
                               New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                                                                        Enumeradores.CaracteristicaFormulario.Ajustes,
                                                                        Enumeradores.CaracteristicaFormulario.MovimientodeFondos)) Then

            Documento.Divisas = Valores.ActualizaDivisas()

            Comon.Util.CalcularTotalesItemsDivisas(Documento.Divisas, Enumeradores.TipoValor.NoDefinido)

            ' Regras de actualizaciones de la Cuenta Destino
            If Not CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.Ajustes) AndAlso _CuentaDestino IsNot Nothing Then

                ' Copia a conta de origem e atualiza apenas os valores necessarios
                Dim objCuenta As Clases.Cuenta = CuentaOrigen.Cuenta.Clonar()
                If (CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.GestiondeFondos) AndAlso CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.EntreSectoresDeLaMismaPlanta)) _
                    OrElse (CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.GestiondeFondos) AndAlso CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.EntreSectoresdePlantasDelegacionesDiferentes)) Then

                    If Not esGrupoDocumento Then
                        objCuenta.Identificador = Nothing
                        objCuenta.Sector = CuentaDestino.Cuenta.Sector
                    End If

                ElseIf CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.GestiondeFondos) AndAlso CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.EntreCanales) Then
                    objCuenta.Identificador = Nothing
                    objCuenta.Canal = CuentaDestino.Cuenta.Canal
                    If objCuenta.Canal IsNot Nothing AndAlso objCuenta.Canal.SubCanales IsNot Nothing Then
                        objCuenta.SubCanal = objCuenta.Canal.SubCanales.First
                    End If
                ElseIf CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.GestiondeFondos) AndAlso CaracteristicasFormulario.Contains(Enumeradores.CaracteristicaFormulario.EntreClientes) Then
                    objCuenta.Identificador = Nothing
                    objCuenta.Cliente = CuentaDestino.Cuenta.Cliente
                    If objCuenta.Cliente IsNot Nothing AndAlso objCuenta.Cliente.SubClientes IsNot Nothing Then
                        objCuenta.SubCliente = objCuenta.Cliente.SubClientes.First
                        If objCuenta.SubCliente IsNot Nothing AndAlso objCuenta.SubCliente.PuntosServicio IsNot Nothing Then
                            objCuenta.PuntoServicio = objCuenta.SubCliente.PuntosServicio.First
                        Else
                            objCuenta.PuntoServicio = Nothing
                        End If
                    Else
                        objCuenta.SubCliente = Nothing
                        objCuenta.PuntoServicio = Nothing
                    End If
                End If

                RecuperarGenerarCuenta(objCuenta, Documento.CuentaSaldoDestino, esDocumentoDevalor)
                Documento.CuentaDestino = objCuenta
            Else
                Documento.CuentaDestino = Documento.CuentaOrigen.Clonar
                Documento.CuentaSaldoDestino = Documento.CuentaSaldoOrigen.Clonar
            End If

        ElseIf Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Classificacion) Then

            ' divisas classificadas
            Documento.Divisas = Me.Saldo.RecuperarDivisas
            ' divisas saldo anterior
            Documento.DivisasSaldoAnterior = Me.Saldo.RecuperarDivisasSaldoAnterior

            Comon.Util.CalcularTotalesItemsDivisas(Documento.Divisas, Enumeradores.TipoValor.NoDefinido)
            Comon.Util.CalcularTotalesItemsDivisas(Documento.DivisasSaldoAnterior, Enumeradores.TipoValor.NoDefinido)

            Documento.CuentaDestino = Documento.CuentaOrigen.Clonar
            Documento.CuentaSaldoDestino = Documento.CuentaSaldoOrigen.Clonar

        Else
            Documento.CuentaDestino = Documento.CuentaOrigen.Clonar
            Documento.CuentaSaldoDestino = Documento.CuentaSaldoOrigen.Clonar

        End If

    End Sub

    Private Sub RecuperarGenerarCuenta(ByRef cuentaMovimiento As Clases.Cuenta,
                                       ByRef cuentaSaldo As Clases.Cuenta,
                                       esDocumentoDeValor As Boolean)

        Dim _validaciones As New List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError)
        If cuentaMovimiento IsNot Nothing Then
            cuentaMovimiento.UsuarioCreacion = Parametros.Permisos.Usuario.Login
            cuentaMovimiento.UsuarioModificacion = Parametros.Permisos.Usuario.Login
        End If

        If cuentaSaldo IsNot Nothing Then
            cuentaSaldo.UsuarioCreacion = Parametros.Permisos.Usuario.Login
            cuentaSaldo.UsuarioModificacion = Parametros.Permisos.Usuario.Login
        End If

        If EsModal Then
            LogicaNegocio.Genesis.MaestroCuenta.ObtenerCuentas(cuentaMovimiento, cuentaSaldo, esDocumentoDeValor, "", False, _validaciones)
        End If

        If _validaciones IsNot Nothing AndAlso _validaciones.Count > 1 Then
            Throw New Excepcion.NegocioExcepcion(String.Join(vbCrLf, _validaciones.Select(Function(f) f.descripcion).ToArray))
        End If

    End Sub

    Private Sub RemoverDocumentoRelacionados()
        Documento.DocumentosRelacionados.Clear()
    End Sub

    Private Sub ActualizaElementosDocumento()
        If Caracteristicas.Util.VerificarCaracteristicas(Documento.Formulario.Caracteristicas,
                                                 New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                                                                                          Enumeradores.CaracteristicaFormulario.GestiondeRemesas,
                                                                                          Enumeradores.CaracteristicaFormulario.GestiondeBultos),
                                                 New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Or,
                                                                                          Enumeradores.CaracteristicaFormulario.Altas,
                                                                                          Enumeradores.CaracteristicaFormulario.Bajas,
                                                                                          Enumeradores.CaracteristicaFormulario.Actas,
                                                                                          Enumeradores.CaracteristicaFormulario.Reenvios)) Then
            If Documento.Elemento IsNot Nothing Then
                ActualizaElementosDocumento(Documento.Elemento, Nothing)
                Elemento.ActualizarDatos()
            End If
        End If
    End Sub

    Private Sub ActualizaElementosDocumento(elementoDocumento As Clases.Elemento,
                                            elementoPai As Clases.Elemento)
        If elementoDocumento IsNot Nothing Then
            ActualizaElemento(elementoDocumento, elementoPai)
            Select Case ObtenerTipoElemento(elementoDocumento)
                Case Enumeradores.TipoElemento.Contenedor
                    Dim contenedor As Clases.Contenedor = DirectCast(elementoDocumento, Clases.Contenedor)
                    ActualizaElementoContenedor(contenedor, elementoPai)
                    If contenedor.Elementos IsNot Nothing Then
                        For Each itemContenedor As Clases.Elemento In contenedor.Elementos
                            ActualizaElementosDocumento(itemContenedor, contenedor)
                        Next
                    End If

                Case Enumeradores.TipoElemento.Remesa
                    Dim remesa As Clases.Remesa = DirectCast(elementoDocumento, Clases.Remesa)
                    ActualizaElementoRemesa(remesa, elementoPai)
                    If remesa.Bultos IsNot Nothing Then
                        For Each bulto As Clases.Bulto In remesa.Bultos
                            ActualizaElementosDocumento(bulto, remesa)
                        Next
                    End If

                Case Enumeradores.TipoElemento.Bulto
                    Dim bulto As Clases.Bulto = DirectCast(elementoDocumento, Clases.Bulto)
                    ActualizaElementoBulto(bulto, elementoPai)
                    If bulto.Parciales IsNot Nothing Then
                        For Each parcial As Clases.Parcial In bulto.Parciales
                            ActualizaElementosDocumento(parcial, bulto)
                        Next
                    End If

                Case Enumeradores.TipoElemento.Parcial
                    ActualizaElementoParcial(DirectCast(elementoDocumento, Clases.Parcial), elementoPai)
            End Select
        End If
    End Sub

    Private Sub ActualizaElemento(objElemento As Clases.Elemento, elementoPai As Clases.Elemento)
        If objElemento IsNot Nothing Then
            If Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Altas) Then
                objElemento.Cuenta = Documento.CuentaOrigen.Clonar
            End If
        End If
    End Sub

    Private Sub ActualizaElementoContenedor(contenedor As Clases.Contenedor, contenedorPai As Clases.Contenedor)
        If contenedor IsNot Nothing Then

        End If
    End Sub

    Private Sub ActualizaElementoRemesa(remesa As Clases.Remesa, contenedor As Clases.Contenedor)
        If remesa IsNot Nothing Then
            remesa.CodigoExterno = Documento.NumeroExterno
            remesa.TrabajaPorBulto = If(Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeRemesas), False, True)
        End If
    End Sub

    Private Sub ActualizaElementoBulto(bulto As Clases.Bulto, remesa As Clases.Remesa)
        If bulto IsNot Nothing Then
            If remesa IsNot Nothing AndAlso remesa.Cuenta IsNot Nothing AndAlso (bulto.Cuenta Is Nothing OrElse String.IsNullOrEmpty(bulto.Cuenta.Identificador)) Then
                bulto.Cuenta = remesa.Cuenta
            End If
        End If
    End Sub

    Private Sub ActualizaElementoParcial(parcial As Clases.Parcial, bulto As Clases.Bulto)
        If parcial IsNot Nothing Then
        End If
    End Sub

    Private Sub ActualizaDocumentosDocumento(documentosRelacionados As ObservableCollection(Of Clases.Documento))
        If documentosRelacionados IsNot Nothing Then

            For Each itemDocumento In documentosRelacionados
                itemDocumento.NumeroExterno = Documento.NumeroExterno

                If (_CuentaOrigen IsNot Nothing) Then
                    itemDocumento.CuentaOrigen = Me.CuentaOrigen.Cuenta
                End If

                If (_CuentaDestino IsNot Nothing) Then
                    itemDocumento.CuentaDestino = Me.CuentaDestino.Cuenta
                End If

                ActualizaDocumentosDocumento(itemDocumento.DocumentosRelacionados)
            Next
        End If
    End Sub

    Private Sub DatosDocumento_ConsultarSaldo()
        Try
            ClaveDocumentoPopup = Me.RegistrarCache(New Clases.Documento)
            Me.AbrirPopup("~/Pantallas/ConsultaSaldo.aspx", "identificadorSector=" & SectorSelecionado.Identificador & "&identificadorPlanta=" & Base.InformacionUsuario.DelegacionSeleccionada.Plantas(0).Identificador & "&Modo=" & Enumeradores.Modo.Consulta.ToString() & "&NombrePopupModal=" & EnumeradoresPantalla.NombrePopupModal.ConsultaSaldo.ToString() & "&ClaveDocumento=" & String.Empty)

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub ValidarCodigoExterno()


        If Genesis.LogicaNegocio.GenesisSaldos.MaestroDocumentos.ValidarSiExisteCodigoExterno(DatosDocumento.NumeroExterno) Then
            Throw New Excepcion.NegocioExcepcion("Codigo Documento ja exite")
        End If
    End Sub

    Private Sub ValidarFechaCertificacion()
        Dim fechaHora As DateTime

        If (Not String.IsNullOrEmpty(Documento.FechaHoraPlanificacionCertificacion)) Then
            If (Not DateTime.TryParse(Documento.FechaHoraPlanificacionCertificacion, fechaHora)) Then
                Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("genTxtValorInvalido"), Traduzir("034_FechaHoraPlanificacionCertificacion").ToLower.Replace(":", "")))
            End If
        Else
            fechaHora = DateTime.UtcNow.QuieroExibirEstaFechaEnLaPatalla(Base.InformacionUsuario.DelegacionSeleccionada)
        End If

        If Documento.FechaHoraPlanificacionCertificacion <> DateTime.MinValue AndAlso (Not String.IsNullOrEmpty(Me.Documento.CuentaOrigen.Identificador) AndAlso Not String.IsNullOrEmpty(Me.Documento.CuentaDestino.Identificador)) Then
            Dim codigoCertificadoConflito As String = String.Empty

            Dim esDocumentoDevalor As Boolean = True
            If Caracteristicas.Util.VerificarCaracteristicas(CaracteristicasFormulario,
                           New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                                                                    Enumeradores.CaracteristicaFormulario.GestiondeRemesas,
                                                                    Enumeradores.CaracteristicaFormulario.GestiondeBultos)) Then
                esDocumentoDevalor = False
            End If

            If Me.Documento.CuentaSaldoOrigen Is Nothing Then
                LogicaNegocio.Genesis.MaestroCuenta.ObtenerCuentas(Me.Documento.CuentaOrigen, Me.Documento.CuentaSaldoOrigen, esDocumentoDevalor)
            End If
            If Me.Documento.CuentaSaldoDestino Is Nothing Then
                LogicaNegocio.Genesis.MaestroCuenta.ObtenerCuentas(Me.Documento.CuentaDestino, Me.Documento.CuentaSaldoDestino, esDocumentoDevalor)
            End If

            If (Not LogicaNegocio.GenesisSaldos.Certificacion.AccionValidaCertificacion _
               .EsFechaHoraPlanCertificacionValidaPorCuentaMovimento(Me.Documento.CuentaSaldoOrigen, Me.Documento.CuentaSaldoDestino,
                                                                     fechaHora, Parametros.Parametro.CrearConfiguiracionNivelSaldo,
                                                                     Parametros.Permisos.Usuario.Login, codigoCertificadoConflito)) Then

                If Documento.Estado = Enumeradores.EstadoDocumento.Nuevo OrElse Documento.Estado = Enumeradores.EstadoDocumento.EnCurso Then
                    Documento.FechaHoraPlanificacionCertificacion = DateTime.UtcNow.QuieroExibirEstaFechaEnLaPatalla(Base.InformacionUsuario.DelegacionSeleccionada)
                End If

            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para comparar e marcar onde estão localizadas as diferenças entre (saldo Anterior x saldo Actual)
    ''' </summary>
    ''' <param name="divisasSaldoAnterior"></param>
    ''' <param name="divisasCuadre"></param>
    ''' <remarks></remarks>
    Private Sub CuadreSaldoAnteriorConsultar(ByRef divisasSaldoAnterior As ObservableCollection(Of Clases.Divisa), divisasCuadre As ObservableCollection(Of Clases.Divisa))

        Dim divisasDiferencias As New List(Of Tuple(Of String, String))
        Dim divisasMedioPagoDiferencias As New List(Of Tuple(Of String, String, String))

        If divisasCuadre IsNot Nothing AndAlso divisasCuadre.Count > 0 AndAlso divisasSaldoAnterior IsNot Nothing AndAlso divisasSaldoAnterior.Count > 0 Then

            For Each divCuadre In divisasCuadre

                If divisasSaldoAnterior.Exists(Function(sa) sa.Identificador = divCuadre.Identificador) Then

                    Dim divSaldoAnterior = divisasSaldoAnterior.FirstOrDefault(Function(sa) sa.Identificador = divCuadre.Identificador)

                    CuadreTotalesEfectivosConsultar(divCuadre, divSaldoAnterior, divisasDiferencias)
                    CuadreDetalleEfectivosConsultar(divCuadre, divSaldoAnterior, divisasDiferencias)

                    CuadreTotalesTiposMediosPagoConsultar(divCuadre, divSaldoAnterior, divisasMedioPagoDiferencias)
                    CuadreDetalleMediosPagoConsultar(divCuadre, divSaldoAnterior, divisasMedioPagoDiferencias)

                End If

            Next divCuadre

        End If

        Dim efectivos As List(Of String) = Nothing
        Dim mediosPagos As List(Of String) = Nothing

        If divisasDiferencias.Count > 0 Then
            efectivos = (From d In divisasDiferencias Select d.Item2).ToList
        End If

        If divisasMedioPagoDiferencias.Count > 0 Then
            mediosPagos = (From d In divisasMedioPagoDiferencias Select d.Item2 & "/" & d.Item3).ToList
        End If

        ValidacionDiferencias = New StringBuilder
        If efectivos IsNot Nothing AndAlso efectivos.Count > 0 AndAlso mediosPagos IsNot Nothing AndAlso mediosPagos.Count > 0 Then
            ValidacionDiferencias.AppendLine(Traduzir("072_diferenciaefectivo") & " " & Join(efectivos.ToArray, ", "))
            ValidacionDiferencias.AppendLine("")
            ValidacionDiferencias.AppendLine(Traduzir("072_diferenciamediopago") & " " & Join(mediosPagos.ToArray, ", "))

        ElseIf efectivos IsNot Nothing AndAlso efectivos.Count > 0 AndAlso (mediosPagos Is Nothing OrElse mediosPagos.Count = 0) Then
            ValidacionDiferencias.AppendLine(Traduzir("072_diferenciaefectivo") & " " & Join(efectivos.ToArray, ", "))

        ElseIf (efectivos Is Nothing OrElse efectivos.Count = 0) AndAlso mediosPagos IsNot Nothing AndAlso mediosPagos.Count > 0 Then
            ValidacionDiferencias.AppendLine(Traduzir("072_diferenciamediopago") & " " & Join(mediosPagos.ToArray, ", "))

        End If

    End Sub

    ''' <summary>
    ''' Metodo para comparar quais saldos estão iguais (saldo Anterior x Saldo Actual) para marcar o checkbox's no grid de saldo actual ao carregar a tela
    ''' </summary>
    ''' <param name="saldoAnterior"></param>
    ''' <param name="saldoActual"></param>
    ''' <remarks></remarks>
    Private Sub ActualizarCampoDetallarSaldoActualModificar(ByRef saldoAnterior As ObservableCollection(Of Clases.Divisa), ByRef saldoActual As ObservableCollection(Of Clases.Divisa))

        If saldoActual IsNot Nothing AndAlso saldoActual.Count > 0 AndAlso saldoAnterior IsNot Nothing AndAlso saldoAnterior.Count > 0 Then

            For Each sActual In saldoActual

                If saldoAnterior.Exists(Function(sa) sa.Identificador = sActual.Identificador) Then

                    Dim divSaldoAnterior = saldoAnterior.FirstOrDefault(Function(sa) sa.Identificador = sActual.Identificador)

                    ActualizarTotalesEfectivosModificar(sActual, divSaldoAnterior)
                    ActualizarDetallesEfectivosModificar(sActual, divSaldoAnterior)

                    ActualizarTotalesTiposMediosPagoModificar(sActual, divSaldoAnterior)
                    ActualizarDetallesMediosPagoModificar(sActual, divSaldoAnterior)

                End If

            Next sActual

        End If

    End Sub

    Private Sub CuadreTotalesEfectivosConsultar(divCuadre As Clases.Divisa,
                                                ByRef divSaldoAnterior As Clases.Divisa,
                                                ByRef divisasDiferencias As List(Of Tuple(Of String, String)))

        If divCuadre.ValoresTotalesEfectivo IsNot Nothing AndAlso divCuadre.ValoresTotalesEfectivo.Count > 0 AndAlso
                       divSaldoAnterior.ValoresTotalesEfectivo IsNot Nothing AndAlso divSaldoAnterior.ValoresTotalesEfectivo.Count > 0 Then

            If divCuadre.ValoresTotalesEfectivo.Sum(Function(s) s.Importe) <> divSaldoAnterior.ValoresTotalesEfectivo.Sum(Function(s) s.Importe) Then

                If Not divisasDiferencias.Exists(Function(e) e.Item1 = divCuadre.Identificador) Then
                    divisasDiferencias.Add(New Tuple(Of String, String)(divCuadre.Identificador, divCuadre.Descripcion))
                End If

                divSaldoAnterior.ValoresTotalesEfectivo.FirstOrDefault.Importe = divCuadre.ValoresTotalesEfectivo.FirstOrDefault.Importe
                divSaldoAnterior.ValoresTotalesEfectivo.FirstOrDefault.Color = Constantes.COLOR_CON_DIFERENCIA
            Else
                divSaldoAnterior.ValoresTotalesEfectivo.FirstOrDefault.Color = Constantes.COLOR_SIN_DIFERENCIA
            End If

        End If

    End Sub

    Private Sub CuadreDetalleEfectivosConsultar(divCuadre As Clases.Divisa,
                                                ByRef divSaldoAnterior As Clases.Divisa,
                                                ByRef divisasDiferencias As List(Of Tuple(Of String, String)))

        If divCuadre.Denominaciones IsNot Nothing AndAlso divCuadre.Denominaciones.Count > 0 Then

            For Each denCuadre In divCuadre.Denominaciones.Where(Function(d) d.ValorDenominacion IsNot Nothing AndAlso d.ValorDenominacion.Count > 0)

                For Each valorCuadre In denCuadre.ValorDenominacion

                    If valorCuadre.Diferencia Then

                        Dim denSaldoAnterior = divSaldoAnterior.Denominaciones.Where(Function(d) d.Identificador = denCuadre.Identificador AndAlso d.ValorDenominacion IsNot Nothing AndAlso d.ValorDenominacion.Count > 0).FirstOrDefault

                        If denSaldoAnterior IsNot Nothing Then

                            Dim valorSaldoAnterior = (From vd In denSaldoAnterior.ValorDenominacion
                                                      Where If(valorCuadre.Calidad Is Nothing, vd.Calidad Is Nothing, If(vd.Calidad Is Nothing, vd.Calidad Is Nothing, vd.Calidad.Identificador = valorCuadre.Calidad.Identificador)) AndAlso
                                                            If(vd.UnidadMedida Is Nothing, vd.UnidadMedida Is Nothing, If(vd.UnidadMedida Is Nothing, vd.UnidadMedida Is Nothing, vd.UnidadMedida.Identificador = valorCuadre.UnidadMedida.Identificador)) AndAlso
                                                            vd.TipoValor = valorCuadre.TipoValor).FirstOrDefault

                            If valorSaldoAnterior IsNot Nothing Then

                                If Not divisasDiferencias.Exists(Function(e) e.Item1 = divCuadre.Identificador) Then
                                    divisasDiferencias.Add(New Tuple(Of String, String)(divCuadre.Identificador, divCuadre.Descripcion))
                                End If

                                valorSaldoAnterior.Color = Constantes.COLOR_CON_DIFERENCIA
                                valorSaldoAnterior.Cantidad = valorCuadre.Cantidad
                                valorSaldoAnterior.Importe = valorCuadre.Importe
                            Else
                                valorSaldoAnterior.Color = Constantes.COLOR_SIN_DIFERENCIA
                            End If

                        End If

                    End If

                Next

            Next denCuadre

        End If

    End Sub

    Private Sub CuadreTotalesTiposMediosPagoConsultar(divCuadre As Clases.Divisa,
                                                     ByRef divSaldoAnterior As Clases.Divisa,
                                                     ByRef divisasMedioPagoDiferencias As List(Of Tuple(Of String, String, String)))

        If divCuadre.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso divCuadre.ValoresTotalesTipoMedioPago.Count > 0 AndAlso
                       divSaldoAnterior.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso divSaldoAnterior.ValoresTotalesTipoMedioPago.Count > 0 Then

            ' CHEQUE - Diferencia Totales
            Dim divChequeCuadre = divCuadre.ValoresTotalesTipoMedioPago.FirstOrDefault(Function(tmp) tmp.TipoMedioPago = Enumeradores.TipoMedioPago.Cheque)
            Dim divChequeSaldoAnterior = divSaldoAnterior.ValoresTotalesTipoMedioPago.FirstOrDefault(Function(tmp) tmp.TipoMedioPago = Enumeradores.TipoMedioPago.Cheque)

            If divChequeCuadre IsNot Nothing AndAlso divChequeSaldoAnterior IsNot Nothing Then

                If divChequeCuadre.Importe <> divChequeSaldoAnterior.Importe Then

                    If Not divisasMedioPagoDiferencias.Exists(Function(e) e.Item1 = divCuadre.Identificador AndAlso e.Item2 = divChequeCuadre.TipoMedioPago.RecuperarValor) Then
                        divisasMedioPagoDiferencias.Add(New Tuple(Of String, String, String)(divCuadre.Identificador, divChequeCuadre.TipoMedioPago.ToString, divCuadre.Descripcion))
                    End If

                    divChequeSaldoAnterior.Importe = divChequeCuadre.Importe
                    divChequeSaldoAnterior.Color = Constantes.COLOR_CON_DIFERENCIA

                Else
                    divSaldoAnterior.ValoresTotalesTipoMedioPago.FirstOrDefault.Color = Constantes.COLOR_SIN_DIFERENCIA

                End If

            End If

            ' OUTRO VALOR - Diferencia Totales
            Dim divOtroValorCuadre = divCuadre.ValoresTotalesTipoMedioPago.FirstOrDefault(Function(tmp) tmp.TipoMedioPago = Enumeradores.TipoMedioPago.OtroValor)
            Dim divOtroValorSaldoAnterior = divSaldoAnterior.ValoresTotalesTipoMedioPago.FirstOrDefault(Function(tmp) tmp.TipoMedioPago = Enumeradores.TipoMedioPago.OtroValor)

            If divOtroValorCuadre IsNot Nothing AndAlso divOtroValorSaldoAnterior IsNot Nothing Then

                If divOtroValorCuadre.Importe <> divOtroValorSaldoAnterior.Importe Then

                    If Not divisasMedioPagoDiferencias.Exists(Function(e) e.Item1 = divCuadre.Identificador AndAlso e.Item2 = divOtroValorCuadre.TipoMedioPago.RecuperarValor) Then
                        divisasMedioPagoDiferencias.Add(New Tuple(Of String, String, String)(divCuadre.Identificador, divOtroValorCuadre.TipoMedioPago.ToString, divCuadre.Descripcion))
                    End If

                    divOtroValorSaldoAnterior.Importe = divOtroValorCuadre.Importe
                    divOtroValorSaldoAnterior.Color = Constantes.COLOR_CON_DIFERENCIA

                Else
                    divOtroValorSaldoAnterior.Color = Constantes.COLOR_SIN_DIFERENCIA

                End If

            End If

            ' TICKET - Diferencia Totales
            Dim divTicketValorCuadre = divCuadre.ValoresTotalesTipoMedioPago.FirstOrDefault(Function(tmp) tmp.TipoMedioPago = Enumeradores.TipoMedioPago.Ticket)
            Dim divTicketValorSaldoAnterior = divSaldoAnterior.ValoresTotalesTipoMedioPago.FirstOrDefault(Function(tmp) tmp.TipoMedioPago = Enumeradores.TipoMedioPago.Ticket)

            If divTicketValorCuadre IsNot Nothing AndAlso divTicketValorSaldoAnterior IsNot Nothing Then

                If divTicketValorCuadre.Importe <> divTicketValorSaldoAnterior.Importe Then

                    If Not divisasMedioPagoDiferencias.Exists(Function(e) e.Item1 = divCuadre.Identificador AndAlso e.Item2 = divTicketValorCuadre.TipoMedioPago.RecuperarValor) Then
                        divisasMedioPagoDiferencias.Add(New Tuple(Of String, String, String)(divCuadre.Identificador, divTicketValorCuadre.TipoMedioPago.ToString, divCuadre.Descripcion))
                    End If

                    divTicketValorSaldoAnterior.Importe = divOtroValorCuadre.Importe
                    divTicketValorSaldoAnterior.Color = Constantes.COLOR_CON_DIFERENCIA

                Else
                    divTicketValorSaldoAnterior.Color = Constantes.COLOR_SIN_DIFERENCIA

                End If

            End If

            ' TARJETA - Diferencia Totales
            Dim divTarjetaValorCuadre = divCuadre.ValoresTotalesTipoMedioPago.FirstOrDefault(Function(tmp) tmp.TipoMedioPago = Enumeradores.TipoMedioPago.Tarjeta)
            Dim divTarjetaValorSaldoAnterior = divSaldoAnterior.ValoresTotalesTipoMedioPago.FirstOrDefault(Function(tmp) tmp.TipoMedioPago = Enumeradores.TipoMedioPago.Tarjeta)

            If divTarjetaValorCuadre IsNot Nothing AndAlso divTarjetaValorSaldoAnterior IsNot Nothing Then

                If divTarjetaValorCuadre.Importe <> divTarjetaValorSaldoAnterior.Importe Then

                    If Not divisasMedioPagoDiferencias.Exists(Function(e) e.Item1 = divCuadre.Identificador AndAlso e.Item2 = divTarjetaValorCuadre.TipoMedioPago.RecuperarValor) Then
                        divisasMedioPagoDiferencias.Add(New Tuple(Of String, String, String)(divCuadre.Identificador, divTarjetaValorCuadre.TipoMedioPago.ToString, divCuadre.Descripcion))
                    End If

                    divTarjetaValorSaldoAnterior.Importe = divOtroValorCuadre.Importe
                    divTarjetaValorSaldoAnterior.Color = Constantes.COLOR_CON_DIFERENCIA

                Else
                    divTarjetaValorSaldoAnterior.Color = Constantes.COLOR_SIN_DIFERENCIA

                End If

            End If

        End If

    End Sub

    Private Sub CuadreDetalleMediosPagoConsultar(divCuadre As Clases.Divisa,
                                                 ByRef divSaldoAnterior As Clases.Divisa,
                                                 ByRef divisasMedioPagoDiferencias As List(Of Tuple(Of String, String, String)))

        If divCuadre.MediosPago IsNot Nothing AndAlso divCuadre.MediosPago.Count > 0 Then

            For Each mpCuadre In divCuadre.MediosPago.Where(Function(d) d.Valores IsNot Nothing AndAlso d.Valores.Count > 0)

                For Each valorCuadre In mpCuadre.Valores

                    If valorCuadre.Diferencia Then

                        Dim mpSaldoAnterior = divSaldoAnterior.MediosPago.Where(Function(d) d.Identificador = mpCuadre.Identificador AndAlso d.Valores IsNot Nothing AndAlso d.Valores.Count > 0).FirstOrDefault

                        If mpSaldoAnterior IsNot Nothing Then

                            Dim valorSaldoAnterior = (From vd In mpSaldoAnterior.Valores
                                                      Where vd.TipoValor = valorCuadre.TipoValor).FirstOrDefault

                            If valorSaldoAnterior IsNot Nothing Then

                                If Not divisasMedioPagoDiferencias.Exists(Function(e) e.Item1 = divCuadre.Identificador AndAlso e.Item2 = mpCuadre.Tipo.RecuperarValor) Then
                                    divisasMedioPagoDiferencias.Add(New Tuple(Of String, String, String)(divCuadre.Identificador, mpCuadre.Tipo.ToString, divCuadre.Descripcion))
                                End If

                                valorSaldoAnterior.Color = Constantes.COLOR_CON_DIFERENCIA
                                valorSaldoAnterior.Cantidad = valorCuadre.Cantidad
                                valorSaldoAnterior.Importe = valorCuadre.Importe
                            Else
                                valorSaldoAnterior.Color = Constantes.COLOR_SIN_DIFERENCIA
                            End If

                        End If

                    End If

                Next

            Next mpCuadre

        End If

    End Sub

    Private Sub ActualizarTotalesEfectivosModificar(ByRef divSaldoActual As Clases.Divisa,
                                                    divSaldoAnterior As Clases.Divisa)

        If divSaldoActual.ValoresTotalesEfectivo IsNot Nothing AndAlso divSaldoActual.ValoresTotalesEfectivo.Count > 0 AndAlso
           divSaldoAnterior.ValoresTotalesEfectivo IsNot Nothing AndAlso divSaldoAnterior.ValoresTotalesEfectivo.Count > 0 Then

            If divSaldoActual.ValoresTotalesEfectivo.Exists(Function(e) e.TipoDetalleEfectivo = divSaldoAnterior.ValoresTotalesEfectivo.FirstOrDefault.TipoDetalleEfectivo) Then
                divSaldoActual.ValoresTotalesEfectivo.FirstOrDefault.Detallar = True
            End If

        End If

    End Sub

    Private Sub ActualizarDetallesEfectivosModificar(ByRef divSaldoActual As Clases.Divisa,
                                                     divSaldoAnterior As Clases.Divisa)

        For Each denSaldoActual In divSaldoActual.Denominaciones.Where(Function(d) d.ValorDenominacion IsNot Nothing AndAlso d.ValorDenominacion.Count > 0)

            Dim denSaldoAnterior = divSaldoAnterior.Denominaciones.Where(Function(d) d.Identificador = denSaldoActual.Identificador).FirstOrDefault

            If denSaldoAnterior IsNot Nothing AndAlso denSaldoAnterior.ValorDenominacion IsNot Nothing AndAlso denSaldoAnterior.ValorDenominacion.Count > 0 Then

                For Each valorSaldoActual In denSaldoActual.ValorDenominacion

                    Dim valorSaldoAnterior As Clases.ValorDenominacion = Nothing

                    For Each valorDenominacionSaldoAnterior In denSaldoAnterior.ValorDenominacion

                        If (((valorSaldoActual.Calidad Is Nothing AndAlso valorDenominacionSaldoAnterior.Calidad Is Nothing) OrElse
                            (valorSaldoActual.Calidad IsNot Nothing AndAlso valorDenominacionSaldoAnterior.Calidad IsNot Nothing AndAlso valorSaldoActual.Calidad.Identificador = valorDenominacionSaldoAnterior.Calidad.Identificador)) AndAlso
                            ((valorSaldoActual.UnidadMedida Is Nothing AndAlso valorDenominacionSaldoAnterior.UnidadMedida Is Nothing) OrElse
                            (valorSaldoActual.UnidadMedida IsNot Nothing AndAlso valorDenominacionSaldoAnterior.UnidadMedida IsNot Nothing AndAlso valorSaldoActual.UnidadMedida.Identificador = valorDenominacionSaldoAnterior.UnidadMedida.Identificador)) AndAlso
                            (valorSaldoActual.TipoValor = valorDenominacionSaldoAnterior.TipoValor)) Then

                            valorSaldoAnterior = valorDenominacionSaldoAnterior

                        End If

                    Next

                    If valorSaldoAnterior IsNot Nothing Then
                        valorSaldoActual.Detallar = True
                    End If

                Next

            End If

        Next denSaldoActual

    End Sub

    Private Sub ActualizarTotalesTiposMediosPagoModificar(ByRef divSaldoActual As Clases.Divisa,
                                                          divSaldoAnterior As Clases.Divisa)

        If divSaldoActual.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso divSaldoActual.ValoresTotalesTipoMedioPago.Count > 0 AndAlso
           divSaldoAnterior.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso divSaldoAnterior.ValoresTotalesTipoMedioPago.Count > 0 Then

            ' CHEQUE - Diferencia Totales
            Dim divChequeSaldoActual = divSaldoActual.ValoresTotalesTipoMedioPago.FirstOrDefault(Function(tmp) tmp.TipoMedioPago = Enumeradores.TipoMedioPago.Cheque)
            Dim divChequeSaldoAnterior = divSaldoAnterior.ValoresTotalesTipoMedioPago.FirstOrDefault(Function(tmp) tmp.TipoMedioPago = Enumeradores.TipoMedioPago.Cheque)

            If divChequeSaldoActual IsNot Nothing AndAlso divChequeSaldoAnterior IsNot Nothing Then

                If divChequeSaldoActual.TipoMedioPago <> divChequeSaldoAnterior.TipoMedioPago Then
                    divChequeSaldoActual.Detallar = True
                End If

            End If

            ' OUTRO VALOR - Diferencia Totales
            Dim divOtroValorSaldoActual = divSaldoActual.ValoresTotalesTipoMedioPago.FirstOrDefault(Function(tmp) tmp.TipoMedioPago = Enumeradores.TipoMedioPago.OtroValor)
            Dim divOtroValorSaldoAnterior = divSaldoAnterior.ValoresTotalesTipoMedioPago.FirstOrDefault(Function(tmp) tmp.TipoMedioPago = Enumeradores.TipoMedioPago.OtroValor)

            If divOtroValorSaldoActual IsNot Nothing AndAlso divOtroValorSaldoAnterior IsNot Nothing Then

                If divOtroValorSaldoActual.TipoMedioPago = divOtroValorSaldoAnterior.TipoMedioPago Then
                    divOtroValorSaldoActual.Detallar = True
                End If

            End If

            ' TICKET - Diferencia Totales
            Dim divTicketValorSaldoActual = divSaldoActual.ValoresTotalesTipoMedioPago.FirstOrDefault(Function(tmp) tmp.TipoMedioPago = Enumeradores.TipoMedioPago.Ticket)
            Dim divTicketValorSaldoAnterior = divSaldoAnterior.ValoresTotalesTipoMedioPago.FirstOrDefault(Function(tmp) tmp.TipoMedioPago = Enumeradores.TipoMedioPago.Ticket)

            If divTicketValorSaldoActual IsNot Nothing AndAlso divTicketValorSaldoAnterior IsNot Nothing Then

                If divTicketValorSaldoActual.TipoMedioPago = divTicketValorSaldoAnterior.TipoMedioPago Then
                    divTicketValorSaldoActual.Detallar = True
                End If

            End If

            ' TARJETA - Diferencia Totales
            Dim divTarjetaValorSaldoActual = divSaldoActual.ValoresTotalesTipoMedioPago.FirstOrDefault(Function(tmp) tmp.TipoMedioPago = Enumeradores.TipoMedioPago.Tarjeta)
            Dim divTarjetaValorSaldoAnterior = divSaldoAnterior.ValoresTotalesTipoMedioPago.FirstOrDefault(Function(tmp) tmp.TipoMedioPago = Enumeradores.TipoMedioPago.Tarjeta)

            If divTarjetaValorSaldoActual IsNot Nothing AndAlso divTarjetaValorSaldoAnterior IsNot Nothing Then

                If divTarjetaValorSaldoActual.TipoMedioPago = divTarjetaValorSaldoAnterior.TipoMedioPago Then
                    divTarjetaValorSaldoActual.Detallar = True
                End If

            End If

        End If

    End Sub

    Private Sub ActualizarDetallesMediosPagoModificar(ByRef divSaldoActual As Clases.Divisa,
                                                      divSaldoAnterior As Clases.Divisa)

        If divSaldoActual.MediosPago IsNot Nothing AndAlso divSaldoActual.MediosPago.Count > 0 Then

            For Each mpSaldoActual In divSaldoActual.MediosPago.Where(Function(d) d.Valores IsNot Nothing AndAlso d.Valores.Count > 0)

                For Each valorSaldoActual In mpSaldoActual.Valores

                    Dim mpSaldoAnterior = divSaldoAnterior.MediosPago.Where(Function(d) d.Identificador = mpSaldoActual.Identificador).FirstOrDefault

                    If mpSaldoAnterior IsNot Nothing Then
                        valorSaldoActual.Detallar = True
                    End If

                Next

            Next mpSaldoActual

        End If

    End Sub

    Private Function RetornaModoConsulta() As Enumeradores.Modo

        Dim _respModo As Enumeradores.Modo = Modo

        If (EsModal AndAlso bol_gestion_bulto AndAlso (TypeOf Documento.Elemento Is Clases.Remesa)) OrElse
            Modo = Enumeradores.Modo.ModificarTerminos Then
            _respModo = Enumeradores.Modo.Consulta
        End If

        Return _respModo

    End Function

#End Region

End Class