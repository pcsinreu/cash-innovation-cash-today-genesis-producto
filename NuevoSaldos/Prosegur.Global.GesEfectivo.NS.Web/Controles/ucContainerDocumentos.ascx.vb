Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.LogicaNegocio
Imports Prosegur.Genesis.Comon.Extenciones
Imports Parametros = Prosegur.Genesis.Web.Login.Parametros
Imports System.Collections.ObjectModel

Public Class ucContainerDocumentos
    Inherits UcBase

#Region "[PROPRIEDADES]"

    Public Property RespuestaFiltro() As ucFiltro.FiltroActualizadoEventArgs
        Get
            If ViewState(Me.ID & "_RespuestaFiltro") Is Nothing Then
                ViewState(Me.ID & "_RespuestaFiltro") = New ucFiltro.FiltroActualizadoEventArgs With {.Elementos = New ObservableCollection(Of Clases.Elemento), .SaldoCuentas = New ObservableCollection(Of Clases.SaldoCuenta)}
            End If
            Return ViewState(Me.ID & "_RespuestaFiltro")
        End Get
        Set(value As ucFiltro.FiltroActualizadoEventArgs)
            ViewState(Me.ID & "_RespuestaFiltro") = value
            AjustaElementosGestionBultos()
            RemoverElementosSelecionadosdoFiltro()
            RemoverValoresSelecionadosdoFiltro()
        End Set
    End Property

    Public ReadOnly Property LegendaFiltro() As String
        Get
            Return RespuestaFiltro.Legenda
        End Get
    End Property

    Public Property DocumentosSeleccionados() As ObservableCollection(Of Clases.Documento)
        Get
            If ViewState(Me.ID & "_DocumentosSeleccionados") Is Nothing Then
                ViewState(Me.ID & "_DocumentosSeleccionados") = New ObservableCollection(Of Clases.Documento)
            End If
            Return ViewState(Me.ID & "_DocumentosSeleccionados")
        End Get
        Set(value As ObservableCollection(Of Clases.Documento))
            ViewState(Me.ID & "_DocumentosSeleccionados") = value
        End Set
    End Property

    Public Property SaldoCuentasSeleccionados As ObservableCollection(Of Clases.SaldoCuenta)
        Get
            If ViewState(Me.ID & "_SaldoCuentasSeleccionados") Is Nothing Then
                ViewState(Me.ID & "_SaldoCuentasSeleccionados") = New ObservableCollection(Of Clases.SaldoCuenta)
            End If
            Return ViewState(Me.ID & "_SaldoCuentasSeleccionados")
        End Get
        Set(value As ObservableCollection(Of Clases.SaldoCuenta))
            ViewState(Me.ID & "_SaldoCuentasSeleccionados") = value
        End Set
    End Property

    Public Property DocumentoDefecto() As Clases.Documento
        Get
            If ViewState(Me.ID & "_DocumentoDefecto") Is Nothing Then
                Dim objDocumentoDefecto As New Clases.Documento
                objDocumentoDefecto = GenesisSaldos.MaestroDocumentos.CrearDocumento(Formulario)

                objDocumentoDefecto.FechaHoraPlanificacionCertificacion = DateTime.UtcNow.QuieroExibirEstaFechaEnLaPatalla(Base.InformacionUsuario.DelegacionSeleccionada)
                objDocumentoDefecto.FechaHoraCreacion = DateTime.UtcNow
                objDocumentoDefecto.FechaHoraModificacion = DateTime.UtcNow
                objDocumentoDefecto.UsuarioCreacion = Parametros.Permisos.Usuario.Login
                objDocumentoDefecto.UsuarioModificacion = Parametros.Permisos.Usuario.Login

                ViewState(Me.ID & "_DocumentoDefecto") = objDocumentoDefecto
            End If
            Return ViewState(Me.ID & "_DocumentoDefecto")
        End Get
        Set(value As Clases.Documento)
            ViewState(Me.ID & "_DocumentoDefecto") = value
        End Set
    End Property

    Private _Modo As Enumeradores.Modo?
    Public Property Modo() As Enumeradores.Modo
        Get
            Return _Modo.Value
        End Get
        Set(value As Enumeradores.Modo)
            _Modo = value
        End Set
    End Property

    Private WithEvents _Filtros As ucFiltro
    Public ReadOnly Property Filtros() As ucFiltro
        Get
            If _Filtros Is Nothing Then
                _Filtros = LoadControl("~/Controles/ucFiltro.ascx")
                _Filtros.ID = Me.ID & "_Filtros"
                _Filtros.Attributes.Add("identificadorSector", Me.Attributes("identificadorSector").ToString())
                AddHandler _Filtros.Erro, AddressOf ErroControles
                phFiltros.Controls.Add(_Filtros)
            End If
            Return _Filtros
        End Get
    End Property

    Private WithEvents _RespuestaDelFiltroValores As ucListaValores
    Public Property RespuestaDelFiltroValores() As ucListaValores
        Get
            If _RespuestaDelFiltroValores Is Nothing Then
                phRespuestaDelFiltro.Controls.Clear()
                _RespuestaDelFiltroValores = New ucListaValores
                _RespuestaDelFiltroValores = LoadControl("~/Controles/ucListaValores.ascx")
                _RespuestaDelFiltroValores.ID = "RespuestaDelFiltroValores"
                _RespuestaDelFiltroValores.Titulo = ""

                _Filtros.Attributes.Add("identificadorSector", Me.Attributes("identificadorSector").ToString())
                _RespuestaDelFiltroValores.esResultadoFiltro = True
                _RespuestaDelFiltroValores.TipoValor = TipoValor
                _RespuestaDelFiltroValores.ExcluirSectoresHijos = Filtros.excluirSectoresHijos
                _RespuestaDelFiltroValores.Attributes.Add("identificadorSector", Me.Attributes("identificadorSector").ToString() )
                AddHandler _RespuestaDelFiltroValores.Erro, AddressOf ErroControles
                phRespuestaDelFiltro.Controls.Add(_RespuestaDelFiltroValores)
            End If
            Return _RespuestaDelFiltroValores
        End Get
        Set(value As ucListaValores)
            _RespuestaDelFiltroValores = value
        End Set
    End Property

    Private WithEvents _ListaValoresSelecionados As ucListaValores
    Public Property ListaValoresSelecionados() As ucListaValores
        Get
            If _ListaValoresSelecionados Is Nothing Then
                phListaElementosDelGrupo.Controls.Clear()
                _ListaValoresSelecionados = New ucListaValores
                _ListaValoresSelecionados = LoadControl("~/Controles/ucListaValores.ascx")
                _ListaValoresSelecionados.ID = "ListaValoresSelecionados"
                _Filtros.Attributes.Add("identificadorSector", Me.Attributes("identificadorSector").ToString())
                _ListaValoresSelecionados.Titulo = LegendaFieldSetGrupo
                _ListaValoresSelecionados.esResultadoFiltro = False
                _ListaValoresSelecionados.ExhibirSeleccion = False
                _ListaValoresSelecionados.TipoValor = TipoValor
                _ListaValoresSelecionados.ExcluirSectoresHijos = True
                AddHandler _ListaValoresSelecionados.Erro, AddressOf ErroControles
                phListaElementosDelGrupo.Controls.Add(_ListaValoresSelecionados)
            End If
            Return _ListaValoresSelecionados
        End Get
        Set(value As ucListaValores)
            _ListaValoresSelecionados = value
        End Set
    End Property

    Private WithEvents _RespuestaDelFiltroElementos As ucElementosSelecionar
    Public Property RespuestaDelFiltroElementos() As ucElementosSelecionar
        Get
            If _RespuestaDelFiltroElementos Is Nothing Then
                phRespuestaDelFiltro.Controls.Clear()
                _RespuestaDelFiltroElementos = New ucElementosSelecionar
                _RespuestaDelFiltroElementos = LoadControl("~/Controles/ucElementosSelecionar.ascx")
                _RespuestaDelFiltroElementos.ID = "RespuestaDelFiltroElementos"
                ' Neste caso, sempre será exibido o valor declarado
                _RespuestaDelFiltroElementos.TipoValor = Enumeradores.TipoValor.Declarado
                AddHandler _RespuestaDelFiltroElementos.Erro, AddressOf ErroControles
                phRespuestaDelFiltro.Controls.Add(_RespuestaDelFiltroElementos)
            End If
            Return _RespuestaDelFiltroElementos
        End Get
        Set(value As ucElementosSelecionar)
            _RespuestaDelFiltroElementos = value
        End Set
    End Property

    Private WithEvents _ListaElementosSelecionados As ucElementosSelecionados
    Public Property ListaElementosSelecionados() As ucElementosSelecionados
        Get
            If _ListaElementosSelecionados Is Nothing Then
                _ListaElementosSelecionados = New ucElementosSelecionados
                _ListaElementosSelecionados = LoadControl("~/Controles/ucElementosSelecionados.ascx")
                _ListaElementosSelecionados.ID = "ListaElementosSelecionados"
                _ListaElementosSelecionados.Titulo = LegendaFieldSetGrupo
                _ListaElementosSelecionados.TipoValor = TipoValor
                AddHandler _ListaElementosSelecionados.Erro, AddressOf ErroControles
                phListaElementosDelGrupo.Controls.Clear()
                phListaElementosDelGrupo.Controls.Add(_ListaElementosSelecionados)
            End If
            Return _ListaElementosSelecionados
        End Get
        Set(value As ucElementosSelecionados)
            _ListaElementosSelecionados = value
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

    Public Property LegendaFieldSetFiltro As String
    Public Property LegendaFieldSetGrupo As String
    Public Property EsUmBultoPorRemesa As Boolean = False
    Public Property EsGestionBulto As Boolean = False
    Public Property mensajeVacio As String
    Public Property usaFiltro As Boolean
    Public Property permiteModificar As Boolean
    Public Property trabajaConElementos As Boolean = True
    Public Property Formulario As Clases.Formulario
    Public Property TipoValor As Enumeradores.TipoValor = Enumeradores.TipoValor.Declarado
    Public identificadorSectorActual As String

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Metodo Inicializar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()

        MyBase.Inicializar()

        Filtros.Filtros.EsGestionBulto = EsGestionBulto
        Filtros.Filtros.EsUmBultoPorRemesa = EsUmBultoPorRemesa

        If usaFiltro AndAlso Not IsPostBack AndAlso Modo <> Enumeradores.Modo.Consulta AndAlso Not trabajaConElementos Then
            Filtros.Buscar(True)
        Else
            Configurar_Grids()
        End If

        'Configurar_Grids()

        btnVisualizarFiltro.Attributes.Add("onclick", "visualizarFiltro('block', '" & dvFiltroPopup.ClientID & "')")
        dvCerrarPopUp.Attributes.Add("onclick", "visualizarFiltro('none', '" & dvFiltroPopup.ClientID & "')")

    End Sub

    ''' <summary>
    ''' Método que permite o desenvolvedor traduzir os controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()
        MyBase.TraduzirControles()
        lblTituloFiltro.Text = LegendaFieldSetFiltro
        lblTituloPopUpFiltro.Text = LegendaFieldSetFiltro
        lblLegendaFiltro.Text = LegendaFiltro
    End Sub

#End Region

#Region "[EVENTOS]"

#Region "[ELEMENTOS]"

    Private Sub RespuestaDelFiltro_Agregar(sender As Object, e As ucElementosSelecionar.ElementoEventArgs) Handles _RespuestaDelFiltroElementos.Agregar

        Dim TempoInicial As DateTime = Now
        Dim Tempo As New StringBuilder

        Try

            If e.Elementos IsNot Nothing AndAlso e.Elementos.Count > 0 Then
                ' Incluir em documentos selecionados
                For Each objElemento In e.Elementos.Clonar()
                    Dim objElementoLocal = objElemento
                    DocumentosSeleccionados.Add(CriarDocumento(Nothing, objElemento))
                    If (EsGestionBulto) Then
                        Dim _ItemSelecionado As Clases.Remesa = DirectCast(objElementoLocal, Clases.Remesa)

                        Dim _remesa = RespuestaFiltro.Elementos _
                                      .Where(Function(x) x.Identificador = _ItemSelecionado.Identificador AndAlso
                                                 CType(x, Clases.Remesa).Bultos.First.Identificador = _ItemSelecionado.Bultos.First.Identificador) _
                                      .FirstOrDefault()

                        RespuestaFiltro.Elementos.Remove(_remesa)
                    Else
                        RespuestaFiltro.Elementos.Remove(RespuestaFiltro.Elementos.Find(Function(x) x.Identificador = objElementoLocal.Identificador))
                    End If
                Next
            Else
                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(Traduzir("032_NenhumValoresSeleccionado"))
            End If

            Configurar_Grids()

            Tempo.AppendLine(Now.Subtract(TempoInicial).ToString())

        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try

    End Sub

    Private Sub RespuestaDelFiltro_DetallarPrecinto(sender As Object, e As ucElementosSelecionar.ElementoEventArgs) Handles _RespuestaDelFiltroElementos.DetallarPrecinto
        Try
            If e.Elementos IsNot Nothing AndAlso e.Elementos.Count > 0 Then
                Dim objDocumento As New Clases.Documento
                'Convert o elemento para remesa
                Dim objRemesa = DirectCast(e.Elementos(0), Clases.Remesa)

                objDocumento = CriarDocumento(Nothing, objRemesa)

                If objDocumento IsNot Nothing Then
                    ClaveDocumento = DirectCast(Page, Base).RegistrarCache(objDocumento)
                    DirectCast(Page, Base).AbrirPopup("~/Pantallas/Documento.aspx", "ClaveDocumento=" & ClaveDocumento & "&Modo=" & Enumeradores.Modo.Consulta.ToString() & "&NombrePopupModal=" & EnumeradoresPantalla.NombrePopupModal.DetallarElemento.ToString())
                End If
            Else
                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(Traduzir("032_NenhumValoresSeleccionado"))
            End If

            Configurar_Grids()

        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try
    End Sub

    Private Sub RespuestaDelFiltro_DetallarPrecintoPadre(sender As Object, e As ucElementosSelecionar.ElementoEventArgs) Handles _RespuestaDelFiltroElementos.DetallarPrecintoPadre
        Try
            If e.Elementos IsNot Nothing AndAlso e.Elementos.Count > 0 Then
                Dim objDocumento As New Clases.Documento
                objDocumento = CriarDocumento(Nothing, e.Elementos(0))

                If objDocumento IsNot Nothing Then
                    ClaveDocumento = DirectCast(Page, Base).RegistrarCache(objDocumento)
                    DirectCast(Page, Base).AbrirPopup("~/Pantallas/Documento.aspx", "ClaveDocumento=" & ClaveDocumento & "&Modo=" & Enumeradores.Modo.Consulta.ToString() & "&esGrupoDocumento=True&NombrePopupModal=ContainerDocumentosView")
                End If
            Else
                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(Traduzir("032_NenhumValoresSeleccionado"))
            End If

            Configurar_Grids()

        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try
    End Sub

    Private Sub ListaElementosSelecionados_Quitar(sender As Object, e As ucElementosSelecionados.ElementoEventArgs) Handles _ListaElementosSelecionados.Quitar
        Try
            If e.Elemento IsNot Nothing Then
                If TypeOf e.Elemento Is Clases.Remesa Then
                    'recupera o documento que contem o elemento
                    If Me.EsGestionBulto Then
                        Dim objRemesa = DirectCast(e.Elemento, Clases.Remesa)
                        If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
                            DocumentosSeleccionados.RemoveAll(Function(d) DirectCast(d.Elemento, Clases.Remesa).Bultos.Any(Function(b) b.Identificador = objRemesa.Bultos(0).Identificador)).FirstOrDefault()
                            'Adiciona o elemento na lista
                            RespuestaFiltro.Elementos.Add(e.Elemento)
                        End If
                    Else
                        DocumentosSeleccionados.RemoveAll(Function(d) d.Elemento.Identificador = e.Elemento.Identificador)
                        'Adiciona o elemento na lista
                        RespuestaFiltro.Elementos.Add(e.Elemento)
                    End If
                End If
            Else
                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(Traduzir("032_NenhumValoresSeleccionado"))
            End If

            Configurar_Grids()
        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try
    End Sub

    Private Sub ListaElementosSelecionados_DetallarPrecinto(sender As Object, e As ucElementosSelecionados.ElementoEventArgs) Handles _ListaElementosSelecionados.DetallarPrecinto
        Try
            Dim objDocumento As New Clases.Documento
            objDocumento = DocumentosSeleccionados(e.ItemIdex)
            If objDocumento IsNot Nothing Then

                'If EsGestionBulto Then
                '    Dim objRemesa As Clases.Remesa = DirectCast(objDocumento.Elemento, Clases.Remesa)
                '    If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
                '        objDocumento.Elemento = objRemesa.Bultos.FirstOrDefault()
                '    End If
                'End If

                ClaveDocumento = DirectCast(Page, Base).RegistrarCache(objDocumento)

                If Modo <> Enumeradores.Modo.Consulta AndAlso permiteModificar Then
                    DirectCast(Page, Base).AbrirPopup("~/Pantallas/Documento.aspx", "ClaveDocumento=" & ClaveDocumento & "&Modo=" & Modo.ToString() & "&NombrePopupModal=" & EnumeradoresPantalla.NombrePopupModal.DetallarElemento.ToString())
                Else
                    DirectCast(Page, Base).AbrirPopup("~/Pantallas/Documento.aspx", "ClaveDocumento=" & ClaveDocumento & "&Modo=" & Enumeradores.Modo.Consulta.ToString() & "&NombrePopupModal=" & EnumeradoresPantalla.NombrePopupModal.DetallarElemento.ToString())
                End If
            End If

        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try
    End Sub

    Private Sub ListaElementosSelecionados_DetallarSaldoCuenta(sender As Object, e As ucElementosSelecionados.ElementoEventArgs) Handles _ListaElementosSelecionados.DetallarSaldoCuenta
        Try

        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try
    End Sub

#End Region

#Region "[VALORES]"

    Private Sub RespuestaDelFiltro_DetallarValores(sender As Object, e As ucListaValores.DetallarValoresEventArgs) Handles _RespuestaDelFiltroValores.DetallarValores

        Try

            Dim objDocumento As New Clases.Documento
            Dim objSaldoCuenta As Clases.SaldoCuenta = e.SaldoCuentas(0)

            If objSaldoCuenta IsNot Nothing Then
                objDocumento = CriarDocumento(objSaldoCuenta, Nothing)
            End If

            If objDocumento IsNot Nothing Then

                ClaveDocumento = DirectCast(Page, Base).RegistrarCache(objDocumento)
                DirectCast(Page, Base).AbrirPopup("~/Pantallas/Documento.aspx", "ClaveDocumento=" & ClaveDocumento & "&Modo=" & Enumeradores.Modo.Consulta.ToString() & "&esGrupoDocumento=True&NombrePopupModal=ContainerDocumentosView")
            End If

        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try
    End Sub

    Private Sub RespuestaDelFiltro_AgregarValores(sender As Object, e As ucListaValores.DetallarValoresEventArgs) Handles _RespuestaDelFiltroValores.AgregarValores

        Dim TempoInicial As DateTime = Now
        Dim Tempo As New StringBuilder

        Try
            Dim objSaldoCuenta As New ObservableCollection(Of Clases.SaldoCuenta)

            If e.SaldoCuentas IsNot Nothing Then
                objSaldoCuenta = e.SaldoCuentas
                'SaldoCuentasSeleccionados = e.SaldoCuentas
                VerificaValoresSeleccionados(objSaldoCuenta)
            End If

            Configurar_Grids()

            Tempo.AppendLine("Tiempo 'AgregarTodosValores': " & Now.Subtract(TempoInicial).ToString())

        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try
    End Sub

    Private Sub RespuestaDelFiltro_SaldoHijos(sender As Object, e As ucListaValores.DetallarValoresEventArgs) Handles _RespuestaDelFiltroValores.SaldoHijos
        Try
            Dim objDocumento As New Clases.Documento
            Dim objSaldoCuenta As Clases.SaldoCuenta = e.SaldoCuentas(0)

            If objSaldoCuenta IsNot Nothing Then
                Dim objFiltros As New Clases.Transferencias.Filtro

                objFiltros = Me.Filtros.RecuperarFiltro(False)
                objFiltros.Cliente = objSaldoCuenta.Cuenta.Cliente
                If objSaldoCuenta.Cuenta.SubCliente IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(objSaldoCuenta.Cuenta.SubCliente.Codigo) Then
                    objFiltros.Cliente.SubClientes = New ObservableCollection(Of Clases.SubCliente)
                    Dim subCliente = objSaldoCuenta.Cuenta.SubCliente

                    If objSaldoCuenta.Cuenta.PuntoServicio IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(objSaldoCuenta.Cuenta.PuntoServicio.Codigo) Then
                        subCliente.PuntosServicio = New ObservableCollection(Of Clases.PuntoServicio)
                        subCliente.PuntosServicio.Add(objSaldoCuenta.Cuenta.PuntoServicio)
                    End If

                    objFiltros.Cliente.SubClientes.Add(subCliente)
                End If

                objFiltros.Canais = New ObservableCollection(Of Clases.Canal)
                objFiltros.Canais.Add(objSaldoCuenta.Cuenta.Canal)
                objFiltros.Divisas = objSaldoCuenta.Divisas
                objFiltros.Sector = objSaldoCuenta.Cuenta.Sector

                Dim ClaveCuenta = DirectCast(Page, Base).RegistrarCache(objFiltros)
                DirectCast(Page, Base).AbrirPopup("~/Pantallas/SaldoHijos.aspx", "ClaveCuenta=" & ClaveCuenta + "&NombrePopupModal=SaldoHijos")
            End If

        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try
    End Sub

    Private Sub ListaValoresDelGrupo_DetallarValores(sender As Object, e As ucListaValores.DetallarValoresEventArgs) Handles _ListaValoresSelecionados.DetallarValores
        Try

            Dim objDocumento As New Clases.Documento
            Dim objSaldoCuenta As Clases.SaldoCuenta = e.SaldoCuentas(0)

            If objSaldoCuenta IsNot Nothing Then
                objDocumento = DocumentosSeleccionados.Where(Function(d) If(d.Estado = Enumeradores.EstadoDocumento.Nuevo AndAlso Not d.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeFondos),
                                                                            d.CuentaDestino.Identificador = e.IdentificadorCuenta,
                                                                            d.CuentaOrigen.Identificador = e.IdentificadorCuenta)).FirstOrDefault.Clonar
            End If

            If objDocumento IsNot Nothing Then

                ClaveDocumento = DirectCast(Page, Base).RegistrarCache(objDocumento)
                If Modo <> Enumeradores.Modo.Consulta AndAlso permiteModificar Then
                    DirectCast(Page, Base).AbrirPopup("~/Pantallas/Documento.aspx", "ClaveDocumento=" & ClaveDocumento & "&Modo=" & Modo.ToString() & "&esGrupoDocumento=True&NombrePopupModal=ContainerDocumentos")
                Else
                    DirectCast(Page, Base).AbrirPopup("~/Pantallas/Documento.aspx", "ClaveDocumento=" & ClaveDocumento & "&Modo=" & Enumeradores.Modo.Consulta.ToString() & "&esGrupoDocumento=True&NombrePopupModal=ContainerDocumentos")
                End If
            End If

        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try
    End Sub

    Private Sub ListaValoresDelGrupo_QuitarValores(sender As Object, e As ucListaValores.DetallarValoresEventArgs) Handles _ListaValoresSelecionados.QuitarValores
        Try

            Dim objSaldoCuenta As New ObservableCollection(Of Clases.SaldoCuenta)

            If e.SaldoCuentas IsNot Nothing Then
                objSaldoCuenta = e.SaldoCuentas.Clonar()
                QuitarValoresDelGrupo(objSaldoCuenta)
            End If

            Configurar_Grids()

        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try
    End Sub

#End Region

    Private Sub ucFiltros_FiltroActualizado(sender As Object, e As ucFiltro.FiltroActualizadoEventArgs) Handles _Filtros.FiltroActualizado
        RespuestaFiltro = e
        lblLegendaFiltro.Text = LegendaFiltro
        Configurar_Grids()

        dvFiltroPopup.Style.Item("display") = "none"

    End Sub

#End Region

#Region "[METODOS]"

    Public Sub Configurar_Grids()

        Dim TempoInicial As DateTime = Now
        Dim Tempo As New StringBuilder

        If usaFiltro Then
            dvFiltro.Style.Item("display") = "block"
            Configurar_RespuestaDelFiltro()
        End If

        Configurar_ElementosValoresDelGrupo()

        Tempo.AppendLine(Now.Subtract(TempoInicial).ToString())

    End Sub

    Public Sub Configurar_RespuestaDelFiltro()

        If Not trabajaConElementos Then
            RespuestaDelFiltroValores.SaldoCuentas = RespuestaFiltro.SaldoCuentas.Clonar()
            RespuestaDelFiltroValores.Modo = Modo
            RespuestaDelFiltroValores.identificadorSectorActual = identificadorSectorActual
            RespuestaDelFiltroValores.mensajeVacio = mensajeVacio
            RespuestaDelFiltroValores.ActualizaGrid()
        Else
            'Configura Grid com o Resultado do Filtro
            RespuestaDelFiltroElementos.Elementos = RespuestaFiltro.Elementos
            RespuestaDelFiltroElementos.Modo = Modo
            RespuestaDelFiltroElementos.mensajeVacio = mensajeVacio
            RespuestaDelFiltroElementos.esGestionBulto = EsGestionBulto
            RespuestaDelFiltroElementos.ActualizaGrid()
        End If

    End Sub

    Public Sub Configurar_ElementosValoresDelGrupo()

        If Not trabajaConElementos Then

            If Not usaFiltro OrElse (usaFiltro AndAlso (SaldoCuentasSeleccionados Is Nothing OrElse SaldoCuentasSeleccionados.Count = 0) AndAlso _
                (DocumentosSeleccionados IsNot Nothing AndAlso DocumentosSeleccionados.Count > 0)) Then

                SaldoCuentasSeleccionados.Clear()

                If Modo = Enumeradores.Modo.Consulta Then

                    For Each _documento In DocumentosSeleccionados

                        Dim objSaldoCuenta As New Clases.SaldoCuenta
                        objSaldoCuenta.CodigosComprobantes = New List(Of String)
                        objSaldoCuenta.SaldoCuentaDetalle = New List(Of Clases.SaldoCuentaDetalle)
                        objSaldoCuenta.CodigosComprobantes.Add(_documento.CodigoComprobante)

                        Dim saldoCuentaDetalle As New Clases.SaldoCuentaDetalle
                        saldoCuentaDetalle.Cuenta = _documento.CuentaOrigen
                        saldoCuentaDetalle.Divisas = _documento.Divisas
                        objSaldoCuenta.SaldoCuentaDetalle.Add(saldoCuentaDetalle)

                        objSaldoCuenta.Cuenta = _documento.CuentaOrigen
                        objSaldoCuenta.Divisas = _documento.Divisas
                        SaldoCuentasSeleccionados.Add(objSaldoCuenta)
                    Next
                Else

                    'Agrupa os saldos das contas por CLIENTE,SUBCLIENTE,PUNTO_SERVICIO e SUBCANAL
                    For Each grupo In (From cd In DocumentosSeleccionados Group By oid_cliente = cd.CuentaOrigen.Cliente.Identificador,
                                            oid_subCliente = If(cd.CuentaOrigen.SubCliente IsNot Nothing, cd.CuentaOrigen.SubCliente.Identificador, Nothing),
                                            oid_puntoServicio = If(cd.CuentaOrigen.PuntoServicio IsNot Nothing, cd.CuentaOrigen.PuntoServicio.Identificador, Nothing),
                                            oid_subCanal = If(cd.CuentaOrigen.SubCanal IsNot Nothing, cd.CuentaOrigen.SubCanal.Identificador, Nothing)
                                            Into Group
                                         Select oid_cliente, oid_subCliente, oid_puntoServicio, oid_subCanal)

                        'Recupera todas as contas para esse grupo
                        Dim documentos = DocumentosSeleccionados.Where(Function(a) a.CuentaOrigen.Cliente.Identificador = grupo.oid_cliente _
                                                                            AndAlso If(a.CuentaOrigen.SubCliente IsNot Nothing, a.CuentaOrigen.SubCliente.Identificador, Nothing) = grupo.oid_subCliente _
                                                                            AndAlso If(a.CuentaOrigen.PuntoServicio IsNot Nothing, a.CuentaOrigen.PuntoServicio.Identificador, Nothing) = grupo.oid_puntoServicio _
                                                                            AndAlso a.CuentaOrigen.SubCanal.Identificador = grupo.oid_subCanal).ToList

                        Dim objSaldoCuenta As New Clases.SaldoCuenta
                        objSaldoCuenta.CodigosComprobantes = New List(Of String)
                        objSaldoCuenta.SaldoCuentaDetalle = New List(Of Clases.SaldoCuentaDetalle)
                        Dim divisas As New ObservableCollection(Of Clases.Divisa)
                        For Each documento In documentos.Clonar
                            objSaldoCuenta.CodigosComprobantes.Add(documento.CodigoComprobante)
                            Comon.Util.BorrarItemsDivisaSinValoresCantidades(documento)
                            Dim saldoCuentaDetalle As New Clases.SaldoCuentaDetalle
                            saldoCuentaDetalle.Cuenta = documento.CuentaOrigen
                            saldoCuentaDetalle.Divisas = documento.Divisas
                            objSaldoCuenta.SaldoCuentaDetalle.Add(saldoCuentaDetalle)

                            'pega todas as divisas da conta
                            divisas.AddRange(documento.Divisas.Clonar)
                        Next

                        'soma as divisas
                        Comon.Util.UnificaItemsDivisas(divisas)

                        objSaldoCuenta.Cuenta = documentos.First.CuentaOrigen.Clonar
                        objSaldoCuenta.Divisas = divisas
                        SaldoCuentasSeleccionados.Add(objSaldoCuenta)
                    Next

                End If

            End If

            ListaValoresSelecionados.SaldoCuentas = SaldoCuentasSeleccionados
            ListaValoresSelecionados.identificadorSectorActual = identificadorSectorActual
            ListaValoresSelecionados.Modo = Modo
            ListaValoresSelecionados.mensajeVacio = mensajeVacio
            ListaValoresSelecionados.ActualizaGrid()
        Else
            'Configura Grid com o Resultado do Filtro
            ListaElementosSelecionados.Elementos.Clear()
            For Each documento In DocumentosSeleccionados.Clonar()
                ListaElementosSelecionados.Elementos.Add(documento.Elemento)
            Next
            ListaElementosSelecionados.Modo = Modo
            ListaElementosSelecionados.esGestionBulto = EsGestionBulto
            ListaElementosSelecionados.mensajeVacio = mensajeVacio
            ListaElementosSelecionados.esResultadoFiltro = usaFiltro
            ListaElementosSelecionados.ActualizaGrid()
        End If

    End Sub

    Private Sub VerificaValoresSeleccionados(ValoresSeleccionados As ObservableCollection(Of Clases.SaldoCuenta))
        If ValoresSeleccionados.Count > 0 Then

            For Each objValores In ValoresSeleccionados.Clonar
                'Verifica se ja existe algum documento para essa conta de detalle
                'se existir então adiciona a divisa 
                'senão cria um novo documento

                'para cada cuenta detalle cria um documento
                For Each saldoCuentaDetalle In objValores.SaldoCuentaDetalle
                    Dim doc As Clases.Documento = DocumentosSeleccionados.Where(Function(d) d.CuentaOrigen.Identificador = saldoCuentaDetalle.Cuenta.Identificador).FirstOrDefault
                    If doc IsNot Nothing Then
                        'se já existe um documento para a conta, então adiciona a divisa nesse documento
                        doc.Divisas.AddRange(saldoCuentaDetalle.Divisas.Clonar)
                    Else
                        'senão, cria um novo documento para essa conta
                        DocumentosSeleccionados.Add(CriarDocumento(saldoCuentaDetalle.Clonar()))
                    End If
                Next

                Dim codigosComprobantes = DocumentosSeleccionados.Where(Function(doc) doc.CuentaOrigen.Cliente.Identificador = objValores.Cuenta.Cliente.Identificador _
                                                                        AndAlso If(doc.CuentaOrigen.SubCliente IsNot Nothing, doc.CuentaOrigen.SubCliente.Identificador, Nothing) = If(objValores.Cuenta.SubCliente IsNot Nothing, objValores.Cuenta.SubCliente.Identificador, Nothing) _
                                                                        AndAlso If(doc.CuentaOrigen.PuntoServicio IsNot Nothing, doc.CuentaOrigen.PuntoServicio.Identificador, Nothing) = If(objValores.Cuenta.PuntoServicio IsNot Nothing, objValores.Cuenta.PuntoServicio.Identificador, Nothing) _
                                                                        AndAlso doc.CuentaOrigen.SubCanal.Identificador = objValores.Cuenta.SubCanal.Identificador).Select(Function(doc) doc.CodigoComprobante).ToList

                Dim saldoCuenta = SaldoCuentasSeleccionados.Where(Function(a) a.Cuenta.Cliente.Identificador = objValores.Cuenta.Cliente.Identificador _
                                                                        AndAlso If(a.Cuenta.SubCliente IsNot Nothing, a.Cuenta.SubCliente.Identificador, Nothing) = If(objValores.Cuenta.SubCliente IsNot Nothing, objValores.Cuenta.SubCliente.Identificador, Nothing) _
                                                                        AndAlso If(a.Cuenta.PuntoServicio IsNot Nothing, a.Cuenta.PuntoServicio.Identificador, Nothing) = If(objValores.Cuenta.PuntoServicio IsNot Nothing, objValores.Cuenta.PuntoServicio.Identificador, Nothing) _
                                                                        AndAlso a.Cuenta.SubCanal.Identificador = objValores.Cuenta.SubCanal.Identificador).FirstOrDefault

                'se já existe uma conta para esse cliente,subcliente,pontoServicio e subcanal
                'então agrupa as divisas
                If saldoCuenta IsNot Nothing Then
                    saldoCuenta.CodigosComprobantes = codigosComprobantes
                    saldoCuenta.Divisas.AddRange(objValores.Divisas)
                Else
                    objValores.CodigosComprobantes = codigosComprobantes
                    SaldoCuentasSeleccionados.Add(objValores)
                End If

                ' Remover do resultado do filtro
                RespuestaFiltro.SaldoCuentas.RemoveAll(Function(a) a.Cuenta.Identificador = objValores.Cuenta.Identificador AndAlso a.Divisas.Where(Function(b) objValores.Divisas.Where(Function(c) c.Identificador = b.Identificador).Count > 0).Count > 0)
            Next
        Else
            Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(Traduzir("032_NenhumValoresSeleccionado"))
        End If
    End Sub

    Private Sub QuitarValoresDelGrupo(ValoresSeleccionados As ObservableCollection(Of Clases.SaldoCuenta))
        If ValoresSeleccionados.Count > 0 Then
            For Each objValores In ValoresSeleccionados.Clonar

                Dim oidDocumentos As New List(Of String)
                For Each divisa In objValores.Divisas
                    Dim objSaldoCuenta As New Clases.SaldoCuenta
                    objSaldoCuenta.Cuenta = objValores.Cuenta
                    objSaldoCuenta.Divisas = New ObservableCollection(Of Clases.Divisa)
                    objSaldoCuenta.Divisas.Add(divisa)
                    objSaldoCuenta.SaldoCuentaDetalle = New List(Of Clases.SaldoCuentaDetalle)

                    For Each documento In DocumentosSeleccionados.Where(Function(d) d.CuentaOrigen.Cliente.Identificador = objValores.Cuenta.Cliente.Identificador _
                                                                    AndAlso If(d.CuentaOrigen.SubCliente IsNot Nothing, d.CuentaOrigen.SubCliente.Identificador, Nothing) = If(objValores.Cuenta.SubCliente IsNot Nothing, objValores.Cuenta.SubCliente.Identificador, Nothing) _
                                                                    AndAlso If(d.CuentaOrigen.PuntoServicio IsNot Nothing, d.CuentaOrigen.PuntoServicio.Identificador, Nothing) = If(objValores.Cuenta.PuntoServicio IsNot Nothing, objValores.Cuenta.PuntoServicio.Identificador, Nothing) _
                                                                    AndAlso d.Divisas.Exists(Function(div) div.CodigoISO = divisa.CodigoISO) _
                                                                    AndAlso d.CuentaOrigen.SubCanal.Identificador = objValores.Cuenta.SubCanal.Identificador).ToList

                        Dim saldoCuentaDetalle As New Clases.SaldoCuentaDetalle
                        saldoCuentaDetalle.Cuenta = documento.CuentaOrigen.Clonar
                        saldoCuentaDetalle.Divisas = New ObservableCollection(Of Clases.Divisa)
                        saldoCuentaDetalle.Divisas.Add(documento.Divisas.Where(Function(div) div.CodigoISO = divisa.CodigoISO).FirstOrDefault)
                        objSaldoCuenta.SaldoCuentaDetalle.Add(saldoCuentaDetalle)
                        oidDocumentos.Add(documento.Identificador)
                    Next

                    RespuestaFiltro.SaldoCuentas.Add(objSaldoCuenta)
                Next

                ' Remover em documentos selecionados
                DocumentosSeleccionados.RemoveAll(Function(d) oidDocumentos.Contains(d.Identificador))

                SaldoCuentasSeleccionados.Remove(SaldoCuentasSeleccionados.Find(Function(x) x.Cuenta.Identificador = objValores.Cuenta.Identificador))
            Next
        Else
            Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(Traduzir("032_NenhumValoresSeleccionado"))
        End If
    End Sub

    Private Sub RemoverElementosSelecionadosdoFiltro()

        ' Verifica se existem elementos
        If RespuestaFiltro.Elementos IsNot Nothing AndAlso RespuestaFiltro.Elementos.Count > 0 Then

            ' Para cada documento selecionado
            For Each _documento In DocumentosSeleccionados

                ' Remover do resultado do filtro
                Dim _elemento As Clases.Elemento = RespuestaFiltro.Elementos.Find(Function(x) _documento.Elemento IsNot Nothing AndAlso x.Identificador = _documento.Elemento.Identificador)

                ' Verifica se existe o documento
                If _elemento IsNot Nothing Then

                    ' Elemento Remesa - Elementos do Filtro
                    Dim objRemesaFiltro As Clases.Remesa = DirectCast(_elemento, Clases.Remesa)
                    ' Elemento Remesa - Documentos selecionados
                    Dim objRemesa As Clases.Remesa = DirectCast(_documento.Elemento, Clases.Remesa)

                    For Each objBulto In objRemesa.Bultos
                        Dim objBultoLocal = objBulto
                        objRemesaFiltro.Bultos.Remove(objRemesaFiltro.Bultos.Find(Function(x) x.Identificador = objBultoLocal.Identificador))
                    Next

                    If objRemesaFiltro.Bultos Is Nothing OrElse objRemesaFiltro.Bultos.Count < 1 Then
                        RespuestaFiltro.Elementos.Remove(RespuestaFiltro.Elementos.Find(Function(x) x.Identificador = objRemesaFiltro.Identificador))
                    End If

                End If
            Next
        End If
    End Sub

    Private Sub AjustaElementosGestionBultos()
        ' Verifica se existem elementos
        If Me.EsGestionBulto AndAlso RespuestaFiltro.Elementos IsNot Nothing AndAlso RespuestaFiltro.Elementos.Count > 0 Then
            Dim _elementos As New ObservableCollection(Of Clases.Elemento)

            For Each _elemento In RespuestaFiltro.Elementos

                Dim _remesa As Clases.Remesa = DirectCast(_elemento, Clases.Remesa)

                If _remesa.Bultos IsNot Nothing Then

                    For Each _bulto In _remesa.Bultos
                        Dim _remesaPorBulto As Clases.Remesa = DirectCast(_elemento, Clases.Remesa).Clonar
                        _remesaPorBulto.Bultos.Clear()
                        _remesaPorBulto.Bultos.Add(_bulto)
                        _elementos.Add(_remesaPorBulto)
                    Next

                End If

            Next

            RespuestaFiltro.Elementos = _elementos
        End If

    End Sub


    Private Sub RemoverValoresSelecionadosdoFiltro()
        If RespuestaFiltro.SaldoCuentas IsNot Nothing AndAlso RespuestaFiltro.SaldoCuentas.Count > 0 Then

            'Agrupa os saldos das contas por CLIENTE,SUBCLIENTE,PUNTO_SERVICIO e SUBCANAL
            For Each grupo In (From cd In DocumentosSeleccionados Group By oid_cliente = cd.CuentaOrigen.Cliente.Identificador,
                                    oid_subCliente = If(cd.CuentaOrigen.SubCliente IsNot Nothing, cd.CuentaOrigen.SubCliente.Identificador, Nothing),
                                    oid_puntoServicio = If(cd.CuentaOrigen.PuntoServicio IsNot Nothing, cd.CuentaOrigen.PuntoServicio.Identificador, Nothing),
                                    oid_subCanal = If(cd.CuentaOrigen.SubCanal IsNot Nothing, cd.CuentaOrigen.SubCanal.Identificador, Nothing)
                                    Into Group
                                 Select oid_cliente, oid_subCliente, oid_puntoServicio, oid_subCanal)

                'Recupera todas as contas para esse grupo
                Dim documentos = DocumentosSeleccionados.Where(Function(a) a.CuentaOrigen.Cliente.Identificador = grupo.oid_cliente _
                                                                    AndAlso If(a.CuentaOrigen.SubCliente IsNot Nothing, a.CuentaOrigen.SubCliente.Identificador, Nothing) = grupo.oid_subCliente _
                                                                    AndAlso If(a.CuentaOrigen.PuntoServicio IsNot Nothing, a.CuentaOrigen.PuntoServicio.Identificador, Nothing) = grupo.oid_puntoServicio _
                                                                    AndAlso a.CuentaOrigen.SubCanal.Identificador = grupo.oid_subCanal).ToList

                For Each documento In documentos
                    For Each Divisa In documento.Divisas
                        RespuestaFiltro.SaldoCuentas.Remove(RespuestaFiltro.SaldoCuentas.Find(Function(s) s.Cuenta.Identificador = documento.CuentaOrigen.Identificador AndAlso s.Divisas.Exists(Function(d) d.CodigoISO = Divisa.CodigoISO)))
                    Next
                Next

                'se ficou algum registro que não possui divisa, então remove
                RespuestaFiltro.SaldoCuentas.Remove(RespuestaFiltro.SaldoCuentas.Find(Function(s) s.Divisas Is Nothing OrElse s.Divisas.Count = 0))
            Next
        End If
    End Sub

    Public Sub ActualizarDocumento(objDocumento As Clases.Documento, nombrePopup As String)
        Try
            If nombrePopup = "ContainerDocumentos" Then
                If objDocumento.Elemento IsNot Nothing Then
                    Select Case True
                        Case TypeOf objDocumento.Elemento Is Clases.Remesa

                            DocumentosSeleccionados.Remove(DocumentosSeleccionados.Find(Function(x) x.Identificador = objDocumento.Identificador))
                            DocumentosSeleccionados.Add(objDocumento)

                        Case TypeOf objDocumento.Elemento Is Clases.Bulto

                            Dim actualizarDocumento As Clases.Documento = DocumentosSeleccionados.Find(Function(x) x.Identificador = objDocumento.Identificador)
                            Dim actualizarRemesa As Clases.Remesa = DirectCast(actualizarDocumento.Elemento, Clases.Remesa)
                            actualizarRemesa.Bultos.Remove(actualizarRemesa.Bultos.Find(Function(x) x.Identificador = objDocumento.Elemento.Identificador))
                            actualizarRemesa.Bultos.Add(objDocumento.Elemento)
                            objDocumento.Elemento = actualizarRemesa

                            DocumentosSeleccionados.Remove(DocumentosSeleccionados.Find(Function(x) x.Identificador = objDocumento.Identificador))
                            DocumentosSeleccionados.Add(objDocumento)

                        Case TypeOf objDocumento.Elemento Is Clases.Parcial

                            Dim actualizarDocumento As Clases.Documento = DocumentosSeleccionados.Find(Function(x) x.Identificador = objDocumento.Identificador)
                            Dim actualizarRemesa As Clases.Remesa = DirectCast(actualizarDocumento.Elemento, Clases.Remesa)
                            Dim actualizarBulto As Clases.Bulto = actualizarRemesa.Bultos.Find(Function(x) x.Identificador = objDocumento.Elemento.Identificador)

                            For Each objBulto In actualizarRemesa.Bultos
                                Dim objBultoLocal = objBulto
                                If objBulto.Parciales.Find(Function(x) x.Identificador = objDocumento.Elemento.Identificador) IsNot Nothing Then
                                    ' Actuliza Bulto con la parcial
                                    objBulto.Parciales.Remove(objBulto.Parciales.Find(Function(x) x.Identificador = objDocumento.Elemento.Identificador))
                                    objBulto.Parciales.Add(objDocumento.Elemento)

                                    'Actualiza Remesa con el bulto
                                    actualizarRemesa.Bultos.Remove(actualizarRemesa.Bultos.Find(Function(x) x.Identificador = objBultoLocal.Identificador))
                                    actualizarRemesa.Bultos.Add(objBulto)
                                    Exit For
                                End If
                            Next
                            objDocumento.Elemento = actualizarRemesa

                            DocumentosSeleccionados.Remove(DocumentosSeleccionados.Find(Function(x) x.Identificador = objDocumento.Identificador))
                            DocumentosSeleccionados.Add(objDocumento)

                    End Select
                Else
                    DocumentosSeleccionados.Remove(DocumentosSeleccionados.Find(Function(x) x.Identificador = objDocumento.Identificador))
                    DocumentosSeleccionados.Add(objDocumento)
                End If

            End If

            Configurar_Grids()

        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try
    End Sub

    Public Function CriarDocumento(saldoCuenta As Clases.SaldoCuenta, elemento As Clases.Elemento) As Clases.Documento

        Dim Documento As New Clases.Documento
        Documento = DocumentoDefecto.Clonar()
        Documento.Identificador = System.Guid.NewGuid.ToString()
        Documento.FechaHoraGestion = DocumentoDefecto.FechaHoraCreacion

        If Not trabajaConElementos Then
            Documento.CuentaOrigen = saldoCuenta.Cuenta.Clonar()
            Documento.CuentaDestino = saldoCuenta.Cuenta.Clonar()
            Documento.Divisas = saldoCuenta.Divisas
        Else

            Dim _cuenta As Clases.Cuenta = Nothing

            If EsGestionBulto Then
                Dim _remesa As Clases.Remesa = DirectCast(elemento, Clases.Remesa)
                _cuenta = _remesa.Bultos.FirstOrDefault.Cuenta.Clonar
            Else
                _cuenta = elemento.Cuenta.Clonar()
            End If

            Documento.CuentaOrigen = _cuenta
            Documento.CuentaDestino = _cuenta
            Documento.Elemento = elemento.Clonar
            Select Case True
                Case TypeOf elemento Is Clases.Remesa
                    Dim objRemesa = DirectCast(Documento.Elemento, Clases.Remesa)
                    If objRemesa.TrabajaPorBulto Then
                        If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
                            Documento.DocumentoPadre = New Clases.Documento With {.Identificador = objRemesa.Bultos(0).IdentificadorDocumento}
                        End If
                    Else
                        Documento.DocumentoPadre = New Clases.Documento With {.Identificador = objRemesa.IdentificadorDocumento}
                    End If

                    Documento.NumeroExterno = objRemesa.CodigoExterno
                Case TypeOf elemento Is Clases.Bulto
                    Documento.DocumentoPadre = New Clases.Documento With {.Identificador = DirectCast(Documento.Elemento, Clases.Bulto).IdentificadorDocumento}
            End Select
        End If
        Return Documento
    End Function

    Public Function CriarDocumento(saldoCuenta As Clases.SaldoCuentaDetalle) As Clases.Documento
        Dim Documento As New Clases.Documento
        Documento = DocumentoDefecto.Clonar()
        Documento.Identificador = System.Guid.NewGuid.ToString()
        Documento.FechaHoraGestion = DocumentoDefecto.FechaHoraCreacion
        Documento.CuentaOrigen = saldoCuenta.Cuenta.Clonar()
        Documento.CuentaDestino = saldoCuenta.Cuenta.Clonar()
        Documento.Divisas = saldoCuenta.Divisas
        Dim tick = System.DateTime.UtcNow.QuieroExibirEstaFechaEnLaPatalla(Base.InformacionUsuario.DelegacionSeleccionada).Ticks.ToString
        Dim random As New Random(Convert.ToInt32(tick.Substring(tick.Length - 9, 9)))
        Documento.CodigoComprobante = random.Next(1, 999999).ToString("000000")
        Return Documento
    End Function

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.NotificarErro(e.Erro)
    End Sub

#End Region

End Class