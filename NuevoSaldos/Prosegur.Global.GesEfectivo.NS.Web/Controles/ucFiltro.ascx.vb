Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.LogicaNegocio
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Parametros = Prosegur.Genesis.Web.Login.Parametros
Imports System.Reflection

Public Class ucFiltro
    Inherits UcBase

#Region "[PROPRIEDADES]"

    Private Elementos As ObservableCollection(Of Comon.Clases.Elemento)
    Private SaldoCuentas As ObservableCollection(Of Comon.Clases.SaldoCuenta)

    Public CodigoIsoDivisaDefecto As String
    Public Filtros As New Clases.Transferencias.Filtro
    Public BuscarValoresDisponibles As Nullable(Of Boolean) = Nothing
    Public excluirSectoresHijos As Boolean = False

    Public Property ExibirConsiderarSomaZero As Boolean
    Private _Cliente As ucCliente
    Public Property Cliente() As ucCliente
        Get
            If _Cliente Is Nothing Then
                _Cliente = LoadControl("~\Controles\ucCliente.ascx")
                _Cliente.ID = "Cliente"
                AddHandler _Cliente.Erro, AddressOf ErroControles

                _Cliente.Clientes = Nothing

                'Controle Cliente
                _Cliente.Modo = Enumeradores.Modo.Alta
                _Cliente.SelecaoMultipla = False
                _Cliente.ClienteHabilitado = True
                _Cliente.SubClienteHabilitado = True
                _Cliente.PtoServicioHabilitado = True

            End If
            Return _Cliente
        End Get
        Set(value As ucCliente)
            _Cliente = value
        End Set
    End Property

    Private _Canal As ucCanal
    Public Property Canal() As ucCanal
        Get
            If _Canal Is Nothing Then
                _Canal = LoadControl("~\Controles\ucCanal.ascx")
                _Canal.ID = "Canal"
                AddHandler _Canal.Erro, AddressOf ErroControles

                'Controle Canal
                _Canal.Modo = Enumeradores.Modo.Alta
                _Canal.SelecaoMultipla = False
                _Canal.CanalHabilitado = True
                _Canal.SubCanalHabilitado = True

            End If
            Return _Canal
        End Get
        Set(value As ucCanal)
            _Canal = value
        End Set
    End Property

    Private _Sector As ucSector
    Public Property Sector() As ucSector
        Get
            If _Sector Is Nothing Then
                _Sector = LoadControl("~\Controles\ucSector.ascx")
                _Sector.ID = "Sector"
                AddHandler _Sector.Erro, AddressOf ErroControles

                'Controle Sector
                _Sector.Modo = Enumeradores.Modo.Alta
                _Sector.SelecaoMultipla = False
                _Sector.ConsiderarPermissoes = True
                _Sector.SectorHabilitado = True
                _Sector.DelegacionHabilitado = False
                _Sector.PlantaHabilitado = False
                _Sector.SolamenteFamiliaSector = True

                _Sector.ucDelegacion.Visible = False
                _Sector.ucPlanta.Visible = False
                _Sector.Delegaciones.Add(Base.InformacionUsuario.DelegacionSeleccionada)
                _Sector.Plantas.Add(Base.InformacionUsuario.DelegacionSeleccionada.Plantas(0))

            End If
            Return _Sector
        End Get
        Set(value As ucSector)
            _Sector = value
        End Set
    End Property

    Private _FiltroDocumento As ucFiltroElementos
    Public ReadOnly Property FiltroDocumento() As ucFiltroElementos
        Get
            If _FiltroDocumento Is Nothing Then
                _FiltroDocumento = LoadControl("~\Controles\ucFiltroElementos.ascx")
                _FiltroDocumento.ID = "FiltroDocumento"
                _FiltroDocumento.CargarFiltro(EnumeradoresPantalla.TipoFiltroElemento.Documento)
                AddHandler _FiltroDocumento.Erro, AddressOf ErroControles
            End If
            Return _FiltroDocumento
        End Get
    End Property

    Private _FiltroContenedor As ucFiltroElementos
    Public ReadOnly Property FiltroContenedor() As ucFiltroElementos
        Get
            If _FiltroContenedor Is Nothing Then
                _FiltroContenedor = LoadControl("~\Controles\ucFiltroElementos.ascx")
                _FiltroContenedor.ID = "FiltroContenedor"
                _FiltroContenedor.CargarFiltro(EnumeradoresPantalla.TipoFiltroElemento.Contenedor)
                AddHandler _FiltroContenedor.Erro, AddressOf ErroControles
            End If
            Return _FiltroContenedor
        End Get
    End Property

    Private _FiltroRemesa As ucFiltroElementos
    Public ReadOnly Property FiltroRemesa() As ucFiltroElementos
        Get
            If _FiltroRemesa Is Nothing Then
                _FiltroRemesa = LoadControl("~\Controles\ucFiltroElementos.ascx")
                _FiltroRemesa.ID = "FiltroRemesa"
                _FiltroRemesa.CargarFiltro(EnumeradoresPantalla.TipoFiltroElemento.Remesa)
                AddHandler _FiltroRemesa.Erro, AddressOf ErroControles
            End If
            Return _FiltroRemesa
        End Get
    End Property

    Private _FiltroBulto As ucFiltroElementos
    Public ReadOnly Property FiltroBulto() As ucFiltroElementos
        Get
            If _FiltroBulto Is Nothing Then
                _FiltroBulto = LoadControl("~\Controles\ucFiltroElementos.ascx")
                _FiltroBulto.ID = "FiltroBulto"
                _FiltroBulto.CargarFiltro(EnumeradoresPantalla.TipoFiltroElemento.Bulto)
                AddHandler _FiltroBulto.Erro, AddressOf ErroControles
            End If
            Return _FiltroBulto
        End Get
    End Property

    Private _FiltroParcial As ucFiltroElementos
    Public ReadOnly Property FiltroParcial() As ucFiltroElementos
        Get
            If _FiltroParcial Is Nothing Then
                _FiltroParcial = LoadControl("~\Controles\ucFiltroElementos.ascx")
                _FiltroParcial.ID = "FiltroParcial"
                _FiltroParcial.CargarFiltro(EnumeradoresPantalla.TipoFiltroElemento.Parcial)
                AddHandler _FiltroParcial.Erro, AddressOf ErroControles
            End If
            Return _FiltroParcial
        End Get
    End Property

    Private _FiltroSaldosCuenta As ucFiltroSaldosCuenta
    Public ReadOnly Property FiltroSaldosCuenta() As ucFiltroSaldosCuenta
        Get
            If _FiltroSaldosCuenta Is Nothing Then
                _FiltroSaldosCuenta = LoadControl("~\Controles\ucFiltroSaldosCuenta.ascx")
                _FiltroSaldosCuenta.ID = "FiltroSaldosCuenta"
                AddHandler _FiltroSaldosCuenta.Erro, AddressOf ErroControles
            End If
            _FiltroSaldosCuenta.CodigoIsoDivisaDefecto = CodigoIsoDivisaDefecto

            If ExibirConsiderarSomaZero Then
                _FiltroSaldosCuenta.ExibirConsiderarSomaZero()
            End If

            Return _FiltroSaldosCuenta
        End Get
    End Property

    Private _ClienteVisible As Boolean
    Public Property ClienteVisible() As Boolean
        Get
            Return _ClienteVisible
        End Get
        Set(value As Boolean)
            _ClienteVisible = value
            If _ClienteVisible Then
                phCliente.Controls.Add(Cliente)
            End If
        End Set
    End Property

    Private _CanalVisible As Boolean
    Public Property CanalVisible() As Boolean
        Get
            Return _CanalVisible
        End Get
        Set(value As Boolean)
            _CanalVisible = value
            If _CanalVisible Then
                phCanal.Controls.Add(Canal)
            End If
        End Set
    End Property

    Private _SectorVisible As Boolean
    Public Property SectorVisible() As Boolean
        Get
            Return _SectorVisible
        End Get
        Set(value As Boolean)
            _SectorVisible = value
            If _SectorVisible Then
                phSector.Controls.Add(Sector)
            End If
        End Set
    End Property

    Private _FiltroDocumentoVisible As Boolean
    Public Property FiltroDocumentoVisible() As Boolean
        Get
            Return _FiltroDocumentoVisible
        End Get
        Set(value As Boolean)
            _FiltroDocumentoVisible = value
            If _FiltroDocumentoVisible Then
                phFiltroDocumento.Controls.Add(FiltroDocumento)
            End If
        End Set
    End Property

    Private _FiltroContenedorVisible As Boolean
    Public Property FiltroContenedorVisible() As Boolean
        Get
            Return _FiltroContenedorVisible
        End Get
        Set(value As Boolean)
            _FiltroContenedorVisible = value
            If _FiltroContenedorVisible Then
                phFiltroContenedor.Controls.Add(FiltroContenedor)
            End If
        End Set
    End Property

    Private _FiltroRemesaVisible As Boolean
    Public Property FiltroRemesaVisible() As Boolean
        Get
            Return _FiltroRemesaVisible
        End Get
        Set(value As Boolean)
            _FiltroRemesaVisible = value
            If _FiltroRemesaVisible Then
                phFiltroRemesa.Controls.Add(FiltroRemesa)
            End If
        End Set
    End Property

    Private _FiltroBultoVisible As Boolean
    Public Property FiltroBultoVisible() As Boolean
        Get
            Return _FiltroBultoVisible
        End Get
        Set(value As Boolean)
            _FiltroBultoVisible = value
            If _FiltroBultoVisible Then
                phFiltroBulto.Controls.Add(FiltroBulto)
            End If
        End Set
    End Property

    Private _FiltroParcialVisible As Boolean
    Public Property FiltroParcialVisible() As Boolean
        Get
            Return _FiltroParcialVisible
        End Get
        Set(value As Boolean)
            _FiltroParcialVisible = value
            If _FiltroParcialVisible Then
                phFiltroParcial.Controls.Add(FiltroParcial)
            End If
        End Set
    End Property

    Private _FiltroSaldosCuentaVisible As Boolean
    Public Property FiltroSaldosCuentaVisible() As Boolean
        Get
            Return _FiltroSaldosCuentaVisible
        End Get
        Set(value As Boolean)
            _FiltroSaldosCuentaVisible = value
            If _FiltroSaldosCuentaVisible Then
                phFiltroDivisas.Controls.Add(FiltroSaldosCuenta)
            End If
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
    End Sub

    ''' <summary>
    ''' Método que permite o desenvolvedor traduzir os controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()
        MyBase.TraduzirControles()
        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpiar.Text = Traduzir("btnLimpar")
    End Sub

    ''' <summary>
    ''' Método que valida os campos obrigatorios do controle.
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Function ValidarControl() As List(Of String)
        Dim retorno As New List(Of String)
        Return retorno
    End Function

#End Region

#Region "[EVENTOS]"

    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        Try
            Buscar()
        Catch ex As Exception
            MyBase.NotificarErro(ex)
            'Mantem a tela de filtro elemento aberta ao exibir a mensagem de campos obrigatórios.
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Filtro", "visualizarFiltro('block', 'ctl00_ContentPlaceHolder1_ListaElemento_dvFiltroPopup')", True)

        End Try
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As System.EventArgs) Handles btnLimpiar.Click
        Try
            Limpiar()
        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try
    End Sub
#End Region

#Region "[DELEGATE]"

    <Serializable()>
    Public Class FiltroActualizadoEventArgs
        Public Elementos As ObservableCollection(Of Comon.Clases.Elemento)
        Public SaldoCuentas As ObservableCollection(Of Comon.Clases.SaldoCuenta)
        Public Legenda As String
    End Class

    Public Event FiltroActualizado(sender As Object, e As FiltroActualizadoEventArgs)

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Trata mensgem de erro dos Controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.NotificarErro(e.Erro)
    End Sub

    ''' <summary>
    ''' Atualiza o objeto Filtro e recupera os documentos da base
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar(Optional esBuscaDefecto As Boolean = False)

        Dim TempoInicial As DateTime = Now
        Dim Tempo As New StringBuilder
        Dim objLegenda As String = String.Empty
        Dim esGestionBulto As Boolean = False
        If Filtros.EsGestionBulto.HasValue Then
            esGestionBulto = Filtros.EsGestionBulto.Value
        End If

        Filtros = Me.RecuperarFiltro(esBuscaDefecto, objLegenda, esGestionBulto)

        If Not _FiltroSaldosCuentaVisible Then

            If ValidarFiltrosElementos(Filtros) Then
                Filtros.RestringirEstados = True

                Elementos = New ObservableCollection(Of Clases.Elemento)
                Dim Remesas As ObservableCollection(Of Clases.Remesa) = LogicaNegocio.Genesis.Remesa.ObtenerRemesas(Filtros)
                If Remesas IsNot Nothing AndAlso Remesas.Count > 0 Then
                    Elementos.AddRange(Remesas)
                End If

                If Elementos IsNot Nothing Then

                    Elementos.RemoveAll(Function(r) DirectCast(r, Clases.Remesa).Bultos Is Nothing OrElse DirectCast(r, Clases.Remesa).Bultos.Count = 0)

                    'Dispara o evento com os valores atualizados
                    RaiseEvent FiltroActualizado(Me, New FiltroActualizadoEventArgs With {.Elementos = Elementos, .SaldoCuentas = New ObservableCollection(Of Clases.SaldoCuenta), .Legenda = objLegenda})
                End If
            Else
                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(Traduzir("040_validas_campos_obligatorios"))
            End If
        Else
            Dim log As StringBuilder = New StringBuilder

            SaldoCuentas = LogicaNegocio.GenesisSaldos.Saldo.obtenerSaldoCuentas(Filtros, True, BuscarValoresDisponibles, log)

            If SaldoCuentas IsNot Nothing Then

                'Dispara o evento com os valores atualizados
                RaiseEvent FiltroActualizado(Me, New FiltroActualizadoEventArgs With {.Elementos = New ObservableCollection(Of Clases.Elemento), .SaldoCuentas = SaldoCuentas, .Legenda = objLegenda})
            End If
        End If

        Tempo.AppendLine(Now.Subtract(TempoInicial).ToString())
    End Sub

    ''' <summary>
    ''' Limpa os componentes do controle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Limpiar()
        'Limpa o filtro de elementos
        Filtros = New Comon.Clases.Transferencias.Filtro

        Cliente = Nothing
        phCliente.Controls.Clear()
        phCliente.Controls.Add(Cliente)

        Canal = Nothing
        phCanal.Controls.Clear()
        phCanal.Controls.Add(Canal)

        Sector = Nothing
        phSector.Controls.Clear()
        phSector.Controls.Add(Sector)

        'Limpa campos
        If Not _FiltroSaldosCuentaVisible Then

            If _FiltroDocumento IsNot Nothing Then
                _FiltroDocumento.Limpiar()
            End If
            If _FiltroContenedor IsNot Nothing Then
                _FiltroContenedor.Limpiar()
            End If
            If _FiltroRemesa IsNot Nothing Then
                _FiltroRemesa.Limpiar()
            End If
            If _FiltroBulto IsNot Nothing Then
                _FiltroBulto.Limpiar()
            End If
            If _FiltroParcial IsNot Nothing Then
                _FiltroParcial.Limpiar()
            End If

        Else
            If _FiltroSaldosCuenta IsNot Nothing Then
                _FiltroSaldosCuenta.Limpiar()
            End If
        End If

        If _FiltroSaldosCuentaVisible Then
            Buscar(True)
        Else
            'Dispara o evento com os valores atualizados
            RaiseEvent FiltroActualizado(Me, New FiltroActualizadoEventArgs With {.Elementos = New ObservableCollection(Of Clases.Elemento), .SaldoCuentas = New ObservableCollection(Of Clases.SaldoCuenta)})
        End If

    End Sub

    Private Function ValidarFiltrosElementos(ByRef objFiltros As Clases.Transferencias.Filtro) As Boolean

        'If ((Filtros.Cliente IsNot Nothing) AndAlso (Not String.IsNullOrEmpty(Filtros.Cliente.Codigo) AndAlso Not String.IsNullOrEmpty(Filtros.Cliente.Descripcion))) OrElse _
        '    (objFiltros.Remesa IsNot Nothing AndAlso objFiltros.Remesa.Count > 0 AndAlso Not String.IsNullOrEmpty(objFiltros.Remesa(0).CodigoExterno)) OrElse _
        '    ((objFiltros.Documento IsNot Nothing AndAlso objFiltros.Documento.Count > 0) AndAlso (Not String.IsNullOrEmpty(objFiltros.Documento(0).NumeroExterno) OrElse _
        '                                                                                          Not String.IsNullOrEmpty(objFiltros.Documento(0).CodigoComprovante))) OrElse _
        '    (objFiltros.Bulto IsNot Nothing AndAlso objFiltros.Bulto.Count > 0) OrElse _
        '    (objFiltros.Parcial IsNot Nothing AndAlso objFiltros.Parcial.Count > 0) Then
        '    Return True
        'End If

        'Return False
        Return True
    End Function

    Public Shared Function ObtenerVersion() As String
        Dim version = Assembly.GetExecutingAssembly.GetName.Version
        Return version.Build.ToString.PadLeft(4, "0"c) & version.Revision.ToString.PadLeft(4, "0"c)
    End Function

    Public Function RecuperarFiltro(Optional esBuscaDefecto As Boolean = False, Optional ByRef objLegenda As String = "", Optional esGestionBulto As Boolean = False) As Comon.Clases.Transferencias.Filtro
        Dim filtro As New Clases.Transferencias.Filtro

        If Cliente.Clientes IsNot Nothing AndAlso Cliente.Clientes.Count > 0 Then
            filtro.Cliente = Cliente.Clientes(0)
        Else
            filtro.Cliente = New Clases.Cliente
        End If

        If filtro.Canais Is Nothing Then
            filtro.Canais = New ObservableCollection(Of Clases.Canal)
        End If
        If Canal.Canales IsNot Nothing AndAlso Canal.Canales.Count > 0 Then
            filtro.Canais.Add(Canal.Canales(0))
        End If
        If Sector.Sector IsNot Nothing AndAlso Not String.IsNullOrEmpty(Sector.Sector.Identificador) Then
            filtro.ExcluirSectoresHijos = True
            filtro.Sector = Sector.Sector
        Else
            filtro.ExcluirSectoresHijos = excluirSectoresHijos

            filtro.Sector = New Clases.Sector With {.Identificador = Me.Attributes("identificadorSector").ToString()}
        End If

        ' Preparar Legenda
        objLegenda = Base.InformacionUsuario.DelegacionSeleccionada.Descripcion
        objLegenda &= " - " & Base.InformacionUsuario.DelegacionSeleccionada.Plantas(0).Descripcion
        objLegenda &= " - " & Base.InformacionUsuario.DelegacionSeleccionada.Plantas(0).Sectores.FirstOrDefault(Function(n) n.Identificador = Me.Attributes("identificadorSector").ToString()).Descripcion

        If filtro.Cliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(filtro.Cliente.Descripcion) Then
            objLegenda &= " - " & filtro.Cliente.Descripcion

            If filtro.Cliente.SubClientes IsNot Nothing AndAlso filtro.Cliente.SubClientes.Count > 0 AndAlso Not String.IsNullOrEmpty(filtro.Cliente.SubClientes(0).Descripcion) Then
                objLegenda &= " - " & filtro.Cliente.SubClientes(0).Descripcion

                If filtro.Cliente.SubClientes(0).PuntosServicio IsNot Nothing AndAlso filtro.Cliente.SubClientes(0).PuntosServicio.Count > 0 AndAlso Not String.IsNullOrEmpty(filtro.Cliente.SubClientes(0).PuntosServicio(0).Descripcion) Then
                    objLegenda &= " - " & filtro.Cliente.SubClientes(0).PuntosServicio(0).Descripcion
                End If

            End If
        End If

        If filtro.Canais IsNot Nothing AndAlso filtro.Canais.Count > 0 AndAlso Not String.IsNullOrEmpty(filtro.Canais(0).Descripcion) Then
            objLegenda &= " - " & filtro.Canais(0).Descripcion

            If filtro.Canais(0).SubCanales IsNot Nothing AndAlso filtro.Canais(0).SubCanales.Count > 0 AndAlso Not String.IsNullOrEmpty(filtro.Canais(0).SubCanales(0).Descripcion) Then
                objLegenda &= " - " & filtro.Canais(0).SubCanales(0).Descripcion
            End If
        End If

        If Not _FiltroSaldosCuentaVisible Then

            If Base.InformacionUsuario IsNot Nothing AndAlso Base.InformacionUsuario IsNot Nothing AndAlso
                Base.InformacionUsuario.DelegacionSeleccionada IsNot Nothing Then
                filtro.Delegacion = Base.InformacionUsuario.DelegacionSeleccionada
            End If

            'Prepara o Filtro com as informações informada pelo usuario
            If _FiltroDocumento IsNot Nothing Then
                _FiltroDocumento.ActualizarFiltros(filtro)
            End If

            If _FiltroContenedor IsNot Nothing Then
                _FiltroContenedor.ActualizarFiltros(filtro)
            End If

            If _FiltroRemesa IsNot Nothing Then
                _FiltroRemesa.ActualizarFiltros(filtro)
                If esBuscaDefecto AndAlso filtro.Remesa Is Nothing Then
                    filtro.Remesa = New ObservableCollection(Of Clases.Transferencias.FiltroRemesa)
                    filtro.Remesa.Add(New Clases.Transferencias.FiltroRemesa With {.FechaAltaDesde = DateTime.UtcNow.QuieroExibirEstaFechaEnLaPatalla(Base.InformacionUsuario.DelegacionSeleccionada).AddDays(-1).ToString("dd/MM/yyyy HH:mm:ss"), .FechaAltaHasta = DateTime.UtcNow.QuieroExibirEstaFechaEnLaPatalla(Base.InformacionUsuario.DelegacionSeleccionada).ToString("dd/MM/yyyy HH:mm:ss")})
                End If
            End If

            If _FiltroBulto IsNot Nothing Then
                _FiltroBulto.ActualizarFiltros(filtro)
            End If

            If _FiltroParcial IsNot Nothing Then
                _FiltroParcial.ActualizarFiltros(filtro)
            End If

            If ValidarFiltrosElementos(filtro) Then
                filtro.RestringirEstados = True
            Else
                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(Traduzir("040_validas_campos_obligatorios"))
            End If
        Else

            'Prepara o Filtro com as Divisas Selecionadas
            If _FiltroSaldosCuenta IsNot Nothing Then
                _FiltroSaldosCuenta.ActualizarFiltros(filtro, objLegenda, esBuscaDefecto)
            End If

        End If

        filtro.EsGestionBulto = esGestionBulto
        filtro.Usuario = Parametros.Permisos.Usuario.Login

        Return filtro

    End Function
#End Region

End Class