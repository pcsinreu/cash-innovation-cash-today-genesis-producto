Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Report
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Util
Imports System.Reflection

Public Class ConsultaSaldo
    Inherits Base

#Region "[PROPRIEDADES]"

    Private BuscarSaldo As Boolean = False
    Private buscarTotal As Boolean = False
    Private _Acciones As ucAcciones
    Public ReadOnly Property Acciones() As ucAcciones
        Get
            If _Acciones Is Nothing Then
                _Acciones = LoadControl("~\Controles\UcAcciones.ascx")
                _Acciones.ID = Me.ID & "_Acciones"
                AddHandler _Acciones.Erro, AddressOf ErroControles
                phAcciones.Controls.Add(_Acciones)
            End If
            Return _Acciones
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

    Private _identificadorPlanta As String = Nothing
    Public ReadOnly Property identificadorPlanta() As String
        Get
            If String.IsNullOrEmpty(_identificadorPlanta) Then
                _identificadorPlanta = Request.QueryString("identificadorPlanta")
            End If
            Return _identificadorPlanta
        End Get
    End Property

    Private _identificadorSector As String = Nothing
    Public ReadOnly Property identificadorSector() As String
        Get
            If String.IsNullOrEmpty(_identificadorSector) Then
                _identificadorSector = Request.QueryString("identificadorSector")
            End If
            Return _identificadorSector
        End Get
    End Property

    Public Property Sectores As ObservableCollection(Of Clases.Sector)
        Get
            Return ucSectores.Sectores
        End Get
        Set(value As ObservableCollection(Of Clases.Sector))
            ucSectores.Sectores = value
        End Set
    End Property

    Public Property Plantas As ObservableCollection(Of Clases.Planta)
        Get
            Return ucSectores.Plantas
        End Get
        Set(value As ObservableCollection(Of Clases.Planta))
            ucSectores.Plantas = value
        End Set
    End Property

    Public Property Delegaciones As ObservableCollection(Of Clases.Delegacion)
        Get
            Return ucSectores.Delegaciones
        End Get
        Set(value As ObservableCollection(Of Clases.Delegacion))
            ucSectores.Delegaciones = value
        End Set
    End Property

    Public Property Clientes As ObservableCollection(Of Clases.Cliente)
        Get
            Return ucClientes.Clientes
        End Get
        Set(value As ObservableCollection(Of Clases.Cliente))
            ucClientes.Clientes = value
        End Set
    End Property

    Public Property Canales As ObservableCollection(Of Clases.Canal)
        Get
            Return ucCanal.Canales
        End Get
        Set(value As ObservableCollection(Of Clases.Canal))
            ucCanal.Canales = value
        End Set
    End Property

    Private WithEvents _ucCanal As ucCanal
    Public Property ucCanal() As ucCanal
        Get
            If _ucCanal Is Nothing Then
                _ucCanal = LoadControl("~\Controles\ucCanal.ascx")
                _ucCanal.ID = Me.ID & "_ucCanal"
                AddHandler _ucCanal.Erro, AddressOf ErroControles
                phCanal.Controls.Add(_ucCanal)
            End If
            Return _ucCanal
        End Get
        Set(value As ucCanal)
            _ucCanal = value
        End Set
    End Property

    Private WithEvents _ucClientes As ucCliente
    Public Property ucClientes() As ucCliente
        Get
            If _ucClientes Is Nothing Then
                _ucClientes = LoadControl("~\Controles\ucCliente.ascx")
                _ucClientes.ID = Me.ID & "_ucClientes"
                AddHandler _ucClientes.Erro, AddressOf ErroControles
                phCliente.Controls.Add(_ucClientes)
            End If
            Return _ucClientes
        End Get
        Set(value As ucCliente)
            _ucClientes = value
        End Set
    End Property

    Private WithEvents _ucSectores As ucSector
    Public Property ucSectores() As ucSector
        Get
            If _ucSectores Is Nothing Then
                _ucSectores = LoadControl("~\Controles\ucSector.ascx")
                _ucSectores.ID = Me.ID & "_ucSectores"
                AddHandler _ucSectores.Erro, AddressOf ErroControles
                phSector.Controls.Add(_ucSectores)
            End If
            Return _ucSectores
        End Get
        Set(value As ucSector)
            _ucSectores = value
        End Set
    End Property

    Private WithEvents _ucConsiderarValores As ucRadioButtonList
    Public Property ucConsiderarValores() As ucRadioButtonList
        Get
            If _ucConsiderarValores Is Nothing Then
                _ucConsiderarValores = LoadControl("~\Controles\ucRadioButtonList.ascx")
                _ucConsiderarValores.ID = Me.ID & "_ucConsiderarValores"
                _ucConsiderarValores.Titulo = Traduzir("057_considerarvalores")
                AddHandler _ucConsiderarValores.Erro, AddressOf ErroControles
                phConsiderarValores.Controls.Add(_ucConsiderarValores)
            End If
            Return _ucConsiderarValores
        End Get
        Set(value As ucRadioButtonList)
            _ucConsiderarValores = value
        End Set
    End Property

    Private WithEvents _ucDiscriminarPor As ucRadioButtonList
    Public Property ucDiscriminarPor() As ucRadioButtonList
        Get
            If _ucDiscriminarPor Is Nothing Then
                _ucDiscriminarPor = LoadControl("~\Controles\ucRadioButtonList.ascx")
                _ucDiscriminarPor.ID = Me.ID & "_ucDiscriminarPor"
                _ucDiscriminarPor.Titulo = Traduzir("062_DiscriminarPor")
                _ucDiscriminarPor.AutoPostBack = True
                AddHandler _ucDiscriminarPor.Erro, AddressOf ErroControles
                AddHandler _ucDiscriminarPor.SelectedIndexChanged, AddressOf ucDiscriminarPor_SelectedIndexChanged
                Me.phDiscriminarPor.Controls.Add(_ucDiscriminarPor)
            End If
            Return _ucDiscriminarPor
        End Get
        Set(value As ucRadioButtonList)
            _ucDiscriminarPor = value
        End Set
    End Property

    Private _Filtros As Clases.Transferencias.FiltroConsultaSaldo
    Public Property Filtros() As Clases.Transferencias.FiltroConsultaSaldo
        Get
            If _Filtros Is Nothing Then
                _Filtros = New Clases.Transferencias.FiltroConsultaSaldo
            End If
            Return _Filtros
        End Get
        Set(value As Clases.Transferencias.FiltroConsultaSaldo)
            _Filtros = value
        End Set
    End Property

    Public Property RespuestaSaldo() As Respuesta(Of List(Of DataRow))
        Get
            Return ViewState("_RespuestaSaldo")
        End Get
        Set(value As Respuesta(Of List(Of DataRow)))
            ViewState("_RespuestaSaldo") = value
        End Set
    End Property

#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CONSULTAR_SALDO
        MyBase.ValidarAcesso = True
        MyBase.ValidarPemissaoAD = True
    End Sub

    Protected Overrides Sub TraduzirControles()
        Master.Titulo = Traduzir("062_lblTitulo")
        Me.lblFiltros.Text = Traduzir("062_lblFiltro")
        Me.lblResultados.Text = Traduzir("062_lblResultados")
        Me.btnBuscar.Text = Traduzir("btnBuscar")
        Me.chkConsiderarSaldoSectoresHijos.Text = Traduzir("062_considerar_sectores_hijos")

        Me.grvResultadoSaldo.Columns(0).HeaderText = Traduzir("062_sector")
        Me.grvResultadoSaldo.Columns(1).HeaderText = Traduzir("062_cliente_canal")
        Me.grvResultadoSaldo.Columns(3).HeaderText = Traduzir("062_divisa")
        Me.grvResultadoSaldo.Columns(4).HeaderText = Traduzir("062_valor_disponible")
        Me.grvResultadoSaldo.Columns(5).HeaderText = Traduzir("062_valor_no_disponible")
        Me.grvResultadoSaldo.Columns(6).HeaderText = Traduzir("062_suma")

        Me.gridTotal.Columns(0).HeaderText = Traduzir("062_Total")
        Me.gridTotal.Columns(2).HeaderText = Traduzir("062_divisa")
        Me.gridTotal.Columns(3).HeaderText = Traduzir("062_valor_disponible")
        Me.gridTotal.Columns(4).HeaderText = Traduzir("062_valor_no_disponible")
        Me.gridTotal.Columns(5).HeaderText = Traduzir("062_suma")

        Me.lblSinSaldo.Text = Traduzir("lblSemSaldo")
    End Sub

#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        GoogleAnalyticsHelper.TrackAnalytics(Me, "Consulta Saldo")
        Me.CargarConsiderarValores()
        Me.CargarDiscriminarPor()
        ' Se foi chamada pela tela de Documentos não mostra os filtros
        If String.IsNullOrEmpty(identificadorSector) Then
            Me.dvFiltros.Style.Item("display") = "block"
            Me.dvTituloFiltro.Style.Item("display") = "block"
            Me.ConfigurarControle_Sector()
            Me.ConfigurarControle_Cliente()
            Me.ConfigurarControle_Canal()
            Me.ucSectores.Focus()

            If Me.IsPostBack Then
                Me.BuscarSaldo = True
                Me.buscarTotal = True
            End If
        Else
            Me.BuscarSaldo = True
            Me.buscarTotal = True

            RecuperarFiltros()
            ConsultaSaldoTotal()

            'Quando popup
            Master.HabilitarMenu = False
            Me.dvResultado.Style.Item("display") = "block"
        End If

        AjustarAcciones()

    End Sub

    Private Sub PopulaGridSaldo()

        Try
            Dim respuesta As Respuesta(Of List(Of DataRow))
            If Me.BuscarSaldo Then
                'Para não executar a busca ao entrar na página
                respuesta = ConsultaSaldo()
                Me.RespuestaSaldo = respuesta
            Else
                respuesta = Me.RespuestaSaldo
            End If

            If respuesta IsNot Nothing AndAlso respuesta.Retorno IsNot Nothing AndAlso respuesta.Retorno.Count > 0 Then
                Me.dvTituloResultado.Style.Item("display") = "block"
                Me.dvResultado.Style.Item("display") = "block"
                Me.grvResultadoSaldo.CssClass = "ui-datatable ui-datatable-data"

                If Filtros.DetallarSaldoSectoresHijos Then
                    If Filtros.DiscriminarPor = Enumeradores.DiscriminarPor.Cuenta Then
                        Me.grvResultadoSaldo.DataSource = OrdenaResultadoCliente(respuesta.Retorno)
                    Else
                        Me.grvResultadoSaldo.DataSource = OrdenaResultado(respuesta.Retorno)
                    End If
                Else
                    Me.grvResultadoSaldo.DataSource = respuesta.Retorno
                End If

            Else
                If String.IsNullOrEmpty(identificadorSector) Then
                    Me.dvTituloResultado.Style.Item("display") = "block"
                End If
                Me.dvResultado.Style.Item("display") = "block"
                Me.grvResultadoSaldo.CssClass = ""

                If Me.BuscarSaldo Then
                    Me.BuscarSaldo = False
                    'Se não achou nenhum registro
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro", _
                                                       Aplicacao.Util.Utilidad.CriarChamadaMensagemErro(Traduzir("062_no_hay_saldo"), Nothing), True)
                End If

            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try

    End Sub

    Private Sub grvResultadoSaldo_PreRender(sender As Object, e As System.EventArgs) Handles grvResultadoSaldo.PreRender
        Me.ucDiscriminarPor.GuardarDatos()
        Dim discriminarPor = RecuperarEnum(Of Enumeradores.DiscriminarPor)(Me.ucDiscriminarPor.ItemSelecionado)
        Dim colunaInicial As Integer, colunaFinal As Integer

        If discriminarPor = Enumeradores.DiscriminarPor.Sector Then
            colunaInicial = 0
            colunaFinal = 0
            Me.grvResultadoSaldo.Columns(0).Visible = True
            Me.grvResultadoSaldo.Columns(1).Visible = False
        ElseIf discriminarPor = Enumeradores.DiscriminarPor.ClienteyCanal Then
            colunaInicial = 1
            colunaFinal = 1
            Me.grvResultadoSaldo.Columns(0).Visible = False
            Me.grvResultadoSaldo.Columns(1).Visible = True
        Else
            colunaInicial = 0
            colunaFinal = 1
            Me.grvResultadoSaldo.Columns(0).Visible = True
            Me.grvResultadoSaldo.Columns(1).Visible = True
        End If

        MergeRows(Me.grvResultadoSaldo, colunaInicial, colunaFinal)
    End Sub

    Private Sub grvResultadoSaldo_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grvResultadoSaldo.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                TratativaRowGrvResultado(e)
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub gridTotal_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridTotal.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                'Pega os dados do item atual.
                Dim Item As DataRowView = e.Row.DataItem
                If Not String.IsNullOrEmpty(AtribuirValorObj(Item("COD_COLOR"), GetType(String))) Then
                    e.Row.Cells(1).Style.Add("background", If(AtribuirValorObj(Item("COD_COLOR"), GetType(String)).Substring(0, 1).ToString <> "#", "#" & AtribuirValorObj(Item("COD_COLOR"), GetType(String)), AtribuirValorObj(Item("COD_COLOR"), GetType(String))) & " !important")
                End If
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click

        Me.BuscarSaldo = True
        Me.buscarTotal = True
        grvResultadoSaldo.PageIndex = 0
        PopulaGridSaldo()
        grvResultadoSaldo.DataBind()

        ConsultaSaldoTotal()

    End Sub

    Public Shared Sub MergeRows(gridView As GridView, colunaInicial As Integer, colunaFinal As Integer)
        For rowIndex As Integer = gridView.Rows.Count - 2 To 0 Step -1
            Dim row As GridViewRow = gridView.Rows(rowIndex)
            Dim previousRow As GridViewRow = gridView.Rows(rowIndex + 1)

            For i As Integer = colunaInicial To colunaFinal
                If row.Cells(i).Text = previousRow.Cells(i).Text Then
                    row.Cells(i).RowSpan = If(previousRow.Cells(i).RowSpan < 2, 2, previousRow.Cells(i).RowSpan + 1)
                    previousRow.Cells(i).Visible = False
                End If
            Next
        Next
    End Sub

    Public Sub ucCanal_OnControleAtualizado() Handles _ucCanal.UpdatedControl
        Try
            If ucCanal.Canales IsNot Nothing Then
                Canales = ucCanal.Canales
            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Public Sub ucSectores_OnControleAtualizado() Handles _ucSectores.UpdatedControl
        Try
            If Me.ucSectores.Delegaciones IsNot Nothing Then
                Delegaciones = Me.ucSectores.Delegaciones
            End If
            If Me.ucSectores.Plantas IsNot Nothing Then
                Plantas = Me.ucSectores.Plantas
            End If
            If Me.ucSectores.Sectores IsNot Nothing Then
                Sectores = Me.ucSectores.Sectores
            End If

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Public Sub ucClientes_OnControleAtualizado() Handles _ucClientes.UpdatedControl
        Try
            If ucClientes.Clientes IsNot Nothing Then
                Clientes = ucClientes.Clientes
            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

#End Region

#Region "[METODOS]"

#Region "     Helpers     "

    Protected Sub ConfigurarControle_Sector()
        Me.ucSectores.ConsiderarPermissoes = False
        Me.ucSectores.SelecaoMultipla = True
        Me.ucSectores.SectorHabilitado = True
        Me.ucSectores.DelegacionHabilitado = True
        Me.ucSectores.PlantaHabilitado = True
        Me.ucSectores.SolamenteSectoresPadre = True

        If Delegaciones IsNot Nothing Then
            Me.ucSectores.Delegaciones = Delegaciones
        End If
        If Plantas IsNot Nothing Then
            Me.ucSectores.Plantas = Plantas
        End If
        If Sectores IsNot Nothing Then
            Me.ucSectores.Sectores = Sectores
        End If
    End Sub

    Protected Sub ConfigurarControle_Cliente()

        Me.ucClientes.SelecaoMultipla = True
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.SubClienteHabilitado = True
        Me.ucClientes.PtoServicioHabilitado = True

        If Clientes IsNot Nothing Then
            Me.ucClientes.Clientes = Clientes
        End If

    End Sub

    Protected Sub ConfigurarControle_Canal()

        Me.ucCanal.SelecaoMultipla = True
        Me.ucCanal.CanalHabilitado = True
        Me.ucCanal.SubCanalHabilitado = True

        If Canales IsNot Nothing Then
            Me.ucCanal.Canales = Canales
        End If

    End Sub

#End Region

#Region "     General     "

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagemErro(If(TypeOf e.Erro Is Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub

    Private Function ConsultaSaldo() As Respuesta(Of List(Of DataRow))

        RecuperarFiltros()

        Dim objRespuesta As Respuesta(Of List(Of DataRow)) = Nothing
        Dim objPeticion As New Peticion(Of Clases.Sector)
        objPeticion.ParametrosPaginacion = New Paginacion.ParametrosPeticionPaginacion()
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = LogicaNegocio.GenesisSaldos.Saldo.ObtenerSaldos(objPeticion, Filtros)

        Return objRespuesta

    End Function

    Private Sub ConsultaSaldoTotal()

        RecuperarFiltros()

        If Me.buscarTotal Then
            Dim saldoTotal = LogicaNegocio.GenesisSaldos.Saldo.ObtenerSaldoTotal(Filtros)
            If saldoTotal Is Nothing OrElse saldoTotal.Rows Is Nothing OrElse saldoTotal.Rows.Count < 1 Then
                Me.lblSinSaldo.Visible = True
            Else
                Me.gridTotal.DataSource = saldoTotal
                Me.gridTotal.DataBind()
            End If
            
            Me.buscarTotal = False

            'Merge total
            If Me.gridTotal.Rows.Count > 0 Then
                Me.gridTotal.HeaderRow.Cells(0).RowSpan = gridTotal.Rows.Count + 1
                For i As Integer = 0 To gridTotal.Rows.Count - 1
                    gridTotal.Rows(i).Cells(0).Visible = False
                Next
            End If
        End If

    End Sub

    Private Sub RecuperarFiltros()

        If InformacionUsuario.Nombre Is Nothing Then
            Return
        End If
        Filtros = New Clases.Transferencias.FiltroConsultaSaldo

        'Quando a Consulta Saldo é chamado pela tela de documentos deve considerar apenas o sector passado
        If Not String.IsNullOrEmpty(identificadorPlanta) AndAlso Not String.IsNullOrEmpty(identificadorSector) Then

            Filtros.identificadoresPlantas.Add(identificadorPlanta)
            Filtros.identificadoresSectores.Add(identificadorSector)
            Filtros.Disponibilidad = Enumeradores.Disponibilidad.Ambos
            Filtros.DiscriminarPor = Enumeradores.DiscriminarPor.Sector
            ' Filtros.ConsiderarSaldoSectoresHijos = False

        Else

            ' Plantas Selecionadas
            If Me.Plantas IsNot Nothing AndAlso Me.Plantas.Count > 0 Then
                For Each _planta In Me.Plantas
                    Filtros.identificadoresPlantas.Add(_planta.Identificador)
                Next
            End If

            ' Sectores Selecionadas
            If Me.Sectores IsNot Nothing AndAlso Me.Sectores.Count > 0 Then
                For Each _Sector In Me.Sectores
                    Filtros.identificadoresSectores.Add(_Sector.Identificador)
                Next
            End If

            ' Clientes Selecionadas
            If Me.Clientes IsNot Nothing AndAlso Me.Clientes.Count > 0 Then
                For Each _cliente In Me.Clientes
                    Filtros.identificadoresClientes.Add(_cliente.Identificador)

                    ' SubClientes Selecionadas
                    If _cliente.SubClientes IsNot Nothing AndAlso _cliente.SubClientes.Count > 0 Then
                        For Each _subCliente In _cliente.SubClientes
                            Filtros.identificadoresSubClientes.Add(_subCliente.Identificador)

                            ' PtoServicios Selecionadas
                            If _subCliente.PuntosServicio IsNot Nothing AndAlso _subCliente.PuntosServicio.Count > 0 Then
                                For Each _ptoServicio In _subCliente.PuntosServicio
                                    Filtros.identificadoresPtoServicios.Add(_ptoServicio.Identificador)
                                Next
                            End If
                        Next
                    End If
                Next
            End If

            ' Canales Selecionadas
            If Me.Canales IsNot Nothing AndAlso Me.Canales.Count > 0 Then
                For Each _canal In Me.Canales
                    Filtros.identificadoresCanales.Add(_canal.Identificador)

                    ' SubCanales Selecionadas
                    If _canal.SubCanales IsNot Nothing AndAlso _canal.SubCanales.Count > 0 Then
                        For Each _subCanal In _canal.SubCanales
                            Filtros.identificadoresSubCanales.Add(_subCanal.Identificador)
                        Next
                    End If
                Next
            End If

            'Disponible
            Me.ucConsiderarValores.GuardarDatos()
            If Me.ucConsiderarValores.ItemSelecionado = "AMBOS" Then
                Filtros.Disponibilidad = Enumeradores.Disponibilidad.Ambos
            ElseIf Me.ucConsiderarValores.ItemSelecionado = "DISPONIVEL" Then
                Filtros.Disponibilidad = Enumeradores.Disponibilidad.Disponible
            Else
                Filtros.Disponibilidad = Enumeradores.Disponibilidad.NoDisponible
            End If

            Me.ucDiscriminarPor.GuardarDatos()
            Filtros.DiscriminarPor = RecuperarEnum(Of Enumeradores.DiscriminarPor)(Me.ucDiscriminarPor.ItemSelecionado)

        End If

        'Veririca se selecionou alguma planta
        If Filtros.identificadoresPlantas.Count = 0 Then
            If Me.ucSectores.Delegaciones IsNot Nothing AndAlso Me.ucSectores.Delegaciones.Count > 0 Then
                'Recuperar as plantas da delegação selecionada em que o usuário tenha permissão
                'For Each objSector In Base.InformacionUsuario.Delegaciones.Where(Function(d) d.Plantas IsNot Nothing).SelectMany(Function(d) d.Plantas).Where(Function(p) p.TiposSector IsNot Nothing).SelectMany(Function(t) t.TiposSector.Select(Function(td) td.Identificador)).Distinct
                For Each del In Me.ucSectores.Delegaciones
                    Filtros.identificadoresPlantas.AddRange(Base.InformacionUsuario.Delegaciones.Where(Function(d) d.Identificador = del.Identificador AndAlso d.Plantas IsNot Nothing).SelectMany(Function(d) d.Plantas).Select(Function(p) p.Identificador).Distinct.ToList())
                Next
            Else
                Filtros.identificadoresPlantas.AddRange(Base.InformacionUsuario.Delegaciones.Where(Function(d) d.Plantas IsNot Nothing).SelectMany(Function(d) d.Plantas).Select(Function(p) p.Identificador).Distinct.ToList())
            End If
        End If

        If Filtros.identificadoresPlantas.Count = 0 Then
            Throw New Exception(Traduzir("062_plantaobligatorio"))
        End If

        If Filtros.identificadoresSectores.Count = 0 Then
            For Each _planta In Filtros.identificadoresPlantas
                Filtros.identificadoresSectores.AddRange(Base.InformacionUsuario.Delegaciones.Where(Function(d) d.Plantas IsNot Nothing).SelectMany(Function(d) d.Plantas).Where(Function(p) p.Sectores IsNot Nothing AndAlso p.Identificador = _planta).SelectMany(Function(p) p.Sectores).Where(Function(s) s.TipoSector IsNot Nothing AndAlso s.SectorPadre Is Nothing).Select(Function(s) s.Identificador).Distinct.ToList())
            Next
        End If

        If Filtros.identificadoresSectores.Count = 0 Then
            Throw New Exception(Traduzir("062_sectorobligatorio"))
        End If

        Filtros.Version = Prosegur.Genesis.Comon.Util.Version()
        Filtros.DetallarSaldoSectoresHijos = (chkConsiderarSaldoSectoresHijos.Enabled AndAlso chkConsiderarSaldoSectoresHijos.Checked AndAlso Filtros.identificadoresSectores IsNot Nothing AndAlso Filtros.identificadoresSectores.Count > 0)

    End Sub

    Private Sub CargarConsiderarValores()
        Dim Opciones As New List(Of KeyValuePair(Of String, String))
        Opciones.Add(New KeyValuePair(Of String, String)("AMBOS", Traduzir("062_ambos")))
        Opciones.Add(New KeyValuePair(Of String, String)("DISPONIVEL", Traduzir("062_disponible")))
        Opciones.Add(New KeyValuePair(Of String, String)("NDISPONIVEL", Traduzir("062_ndisponible")))
        Me.ucConsiderarValores.Opciones = Opciones
    End Sub

    Private Sub CargarDiscriminarPor()
        Dim Opciones As New List(Of KeyValuePair(Of String, String))
        Opciones.Add(New KeyValuePair(Of String, String)(Enumeradores.DiscriminarPor.Sector.RecuperarValor, Traduzir("062_sector")))
        Opciones.Add(New KeyValuePair(Of String, String)(Enumeradores.DiscriminarPor.ClienteyCanal.RecuperarValor, Traduzir("062_cliente_canal")))
        Opciones.Add(New KeyValuePair(Of String, String)(Enumeradores.DiscriminarPor.Cuenta.RecuperarValor, Traduzir("062_cuenta")))
        Me.ucDiscriminarPor.Opciones = Opciones
    End Sub

    Protected Sub ucDiscriminarPor_SelectedIndexChanged()
        Me.ucDiscriminarPor.GuardarDatos()
        If Not String.IsNullOrEmpty(Me.ucDiscriminarPor.ItemSelecionado) Then
            Dim discriminarPor = RecuperarEnum(Of Enumeradores.DiscriminarPor)(Me.ucDiscriminarPor.ItemSelecionado)
            Me.chkConsiderarSaldoSectoresHijos.Enabled = (discriminarPor = Enumeradores.DiscriminarPor.Cuenta OrElse discriminarPor = Enumeradores.DiscriminarPor.Sector)
        Else
            Me.chkConsiderarSaldoSectoresHijos.Enabled = False
        End If
    End Sub

    Private Sub AjustarAcciones()

        AddHandler Acciones.onAccionCancelar, AddressOf Acciones_onAccionCancelar
        Acciones.btnCancelarVisible = True

    End Sub

    Private Sub Acciones_onAccionCancelar()
        Try
            'Quando modal
            If Not String.IsNullOrEmpty(identificadorSector) Then
                CerrarPopup(NombrePopupModal, "")
            Else
                If Master.Historico.Count > 1 Then
                    Response.Redireccionar(Master.Historico(Master.Historico.Count - 2).Key)
                Else
                    Response.Redireccionar(Constantes.NOME_PAGINA_MENU)
                End If
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub TratativaRowGrvResultado(e As System.Web.UI.WebControls.GridViewRowEventArgs)

        'Pega os dados do item atual.
        Dim Item As DataRow = e.Row.DataItem
        DirectCast(e.Row.FindControl("Divisa"), Label).Text = AtribuirValorObj(Item("DES_DIVISA"), GetType(String))
        DirectCast(e.Row.FindControl("ValorDisponible"), Label).Text = String.Format("{0:N}", AtribuirValorObj(Item("NUM_IMPORTE_DISP"), GetType(Double)))
        DirectCast(e.Row.FindControl("ValorNoDisponible"), Label).Text = String.Format("{0:N}", AtribuirValorObj(Item("NUM_IMPORTE_NODISP"), GetType(Double)))
        DirectCast(e.Row.FindControl("Suma"), Label).Text = String.Format("{0:N}", AtribuirValorObj(Item("NUM_IMPORTE"), GetType(Double)))

        If Not String.IsNullOrEmpty(AtribuirValorObj(Item("COD_COLOR"), GetType(String))) Then
            e.Row.Cells(2).Style.Add("background", If(AtribuirValorObj(Item("COD_COLOR"), GetType(String)).Substring(0, 1).ToString <> "#", "#" & AtribuirValorObj(Item("COD_COLOR"), GetType(String)), AtribuirValorObj(Item("COD_COLOR"), GetType(String))) & " !important")
        End If

        Me.ucDiscriminarPor.GuardarDatos()
        Dim discriminarPor = RecuperarEnum(Of Enumeradores.DiscriminarPor)(Me.ucDiscriminarPor.ItemSelecionado)

        If discriminarPor = Enumeradores.DiscriminarPor.Sector Then

            e.Row.Cells(0).Text = AtribuirValorObj(Item("DES_SECTOR"), GetType(String))
            'AtribuirValorObj(Item("COD_SECTOR"), GetType(String)) & " - " & 

        ElseIf discriminarPor = Enumeradores.DiscriminarPor.ClienteyCanal Then
            'Cuando por cliente e canal
            Dim clienteCanal As String

            clienteCanal = AtribuirValorObj(Item("COD_CLIENTE"), GetType(String)) & ": " & AtribuirValorObj(Item("DES_CLIENTE"), GetType(String))

            If Not String.IsNullOrEmpty(AtribuirValorObj(Item("COD_SUBCLIENTE"), GetType(String))) Then
                clienteCanal = clienteCanal & " | " & AtribuirValorObj(Item("COD_SUBCLIENTE"), GetType(String)) & ": " & AtribuirValorObj(Item("DES_SUBCLIENTE"), GetType(String))
            End If

            If Not String.IsNullOrEmpty(AtribuirValorObj(Item("COD_PTO_SERVICIO"), GetType(String))) Then
                clienteCanal = clienteCanal & " | " & AtribuirValorObj(Item("COD_PTO_SERVICIO"), GetType(String)) & ": " & AtribuirValorObj(Item("DES_PTO_SERVICIO"), GetType(String))
            End If

            clienteCanal = clienteCanal & " | " & AtribuirValorObj(Item("COD_CANAL"), GetType(String)) & ": " & AtribuirValorObj(Item("DES_CANAL"), GetType(String))
            clienteCanal = clienteCanal & " | " & AtribuirValorObj(Item("COD_SUBCANAL"), GetType(String)) & ": " & AtribuirValorObj(Item("DES_SUBCANAL"), GetType(String))
            e.Row.Cells(1).Text = clienteCanal
        ElseIf discriminarPor = Enumeradores.DiscriminarPor.Cuenta Then
            'Cuando por cuenta
            e.Row.Cells(0).Text = AtribuirValorObj(Item("DES_SECTOR"), GetType(String))
            'AtribuirValorObj(Item("COD_SECTOR"), GetType(String)) & " - " & 

            Dim clienteCanal As String

            clienteCanal = AtribuirValorObj(Item("COD_CLIENTE"), GetType(String)) & ": " & AtribuirValorObj(Item("DES_CLIENTE"), GetType(String))

            If Not String.IsNullOrEmpty(AtribuirValorObj(Item("COD_SUBCLIENTE"), GetType(String))) Then
                clienteCanal = clienteCanal & " | " & AtribuirValorObj(Item("COD_SUBCLIENTE"), GetType(String)) & ": " & AtribuirValorObj(Item("DES_SUBCLIENTE"), GetType(String))
            End If

            If Not String.IsNullOrEmpty(AtribuirValorObj(Item("COD_PTO_SERVICIO"), GetType(String))) Then
                clienteCanal = clienteCanal & " | " & AtribuirValorObj(Item("COD_PTO_SERVICIO"), GetType(String)) & ": " & AtribuirValorObj(Item("DES_PTO_SERVICIO"), GetType(String))
            End If

            clienteCanal = clienteCanal & " | " & AtribuirValorObj(Item("COD_CANAL"), GetType(String)) & ": " & AtribuirValorObj(Item("DES_CANAL"), GetType(String))
            clienteCanal = clienteCanal & " | " & AtribuirValorObj(Item("COD_SUBCANAL"), GetType(String)) & ": " & AtribuirValorObj(Item("DES_SUBCANAL"), GetType(String))

            e.Row.Cells(1).Text = clienteCanal
        End If

        If Filtros.DetallarSaldoSectoresHijos Then

            Dim sectorPadre As String = AtribuirValorObj(Item("OID_SECTOR_PADRE"), GetType(String))
            e.Row.Cells(0).Font.Bold = String.IsNullOrEmpty(sectorPadre)

        End If

    End Sub

    Private Function OrdenaResultado(list As List(Of DataRow)) As List(Of DataRow)
        Dim listaOrdenada As New List(Of DataRow)

        If list IsNot Nothing AndAlso list.Count > 0 Then

            'Remove duplicado
            list.RemoveAll(Function(a) a("COD_ISO_DIVISA") Is DBNull.Value AndAlso list.Where(Function(b) b("COD_ISO_DIVISA") IsNot DBNull.Value AndAlso a("OID_SECTOR") = b("OID_SECTOR")).Count > 0)

            'Lista todos ID dos pais
            Dim lstPais = list.Where(Function(a) a("OID_SECTOR_PADRE") Is DBNull.Value).OrderBy(Function(a) a("DES_SECTOR")).Select(Function(b) b("OID_SECTOR")).Distinct()

            For Each sIdPai As String In lstPais

                'Adiciona todos os registros do pai
                listaOrdenada.AddRange(list.Where(Function(a) AtribuirValorObj(a("OID_SECTOR"), GetType(String)) = sIdPai).OrderBy(Function(a) a("DES_DIVISA")))
                'Adiciona os filhos
                Dim lstFilhos = AdicionaFilhos(listaOrdenada, list, sIdPai, 1)

                SomarSaldoFilhos(listaOrdenada, lstFilhos)

            Next

            'Adiciona os orfãos
            listaOrdenada.AddRange(list.Where(Function(a) a("OID_SECTOR_PADRE") IsNot DBNull.Value AndAlso list.Where(Function(b) b("OID_SECTOR") = a("OID_SECTOR_PADRE")).Count = 0).OrderBy(Function(a) a("DES_SECTOR")).ThenBy(Function(a) a("DES_DIVISA")))

            'Remove todos sem saldo
            listaOrdenada.RemoveAll(Function(a) a("COD_ISO_DIVISA") Is DBNull.Value)

            'Trata orfãos
            Dim lstIdOrfaos = listaOrdenada.Where(Function(a) a("OID_SECTOR_PADRE") IsNot DBNull.Value AndAlso listaOrdenada.Where(Function(b) b("OID_SECTOR") = a("OID_SECTOR_PADRE")).Count = 0).Select(Function(b) b("OID_SECTOR")).Distinct().ToList()

            If lstIdOrfaos IsNot Nothing AndAlso lstIdOrfaos.Count > 0 Then
                For Each sIdOrfao As String In lstIdOrfaos
                    Dim lstOrfaos = listaOrdenada.Where(Function(a) a("OID_SECTOR") = sIdOrfao).OrderBy(Function(a) a("DES_DIVISA")).ToList()

                    Dim pai = listaOrdenada.Where(Function(a) a("OID_SECTOR") = lstOrfaos.First.Item("OID_SECTOR_PADRE"))
                    If pai Is Nothing OrElse pai.Count = 0 Then
                        pai = list.Where(Function(a) a("OID_SECTOR") = lstOrfaos.First.Item("OID_SECTOR_PADRE")).ToList()
                        If pai IsNot Nothing AndAlso pai.Count > 0 Then
                            listaOrdenada.InsertRange(listaOrdenada.IndexOf(lstOrfaos.First), pai)
                        End If
                    Else
                        listaOrdenada.RemoveAll(Function(a) a("OID_SECTOR") = sIdOrfao)
                        listaOrdenada.InsertRange(listaOrdenada.IndexOf(pai.Last) + 1, lstOrfaos)
                    End If
                Next
            End If

            If listaOrdenada.Count = 0 Then
                listaOrdenada = list
            End If

        End If

        Return listaOrdenada
    End Function

    Private Function AdicionaFilhos(ByRef listaOrdenada As List(Of DataRow), list As List(Of DataRow), sIdPai As String, nivel As Integer) As List(Of DataRow)
        Dim retorno As New List(Of DataRow)

        Dim lstFilhos = list.Where(Function(a) AtribuirValorObj(a("OID_SECTOR_PADRE"), GetType(String)) = sIdPai).OrderBy(Function(a) a("DES_SECTOR")).ThenBy(Function(a) a("DES_DIVISA")).ToList()
        listaOrdenada.AddRange(lstFilhos)

        If lstFilhos IsNot Nothing AndAlso lstFilhos.Count > 0 Then
            For Each filho In lstFilhos
                If Not filho.Table.Columns.Contains("NIVEL") Then
                    filho.Table.Columns.Add("NIVEL")
                End If
                filho("NIVEL") = nivel
            Next
        End If

        retorno.AddRange(lstFilhos)
        nivel += 1
        For Each sIdfilho As String In lstFilhos.Select(Function(a) a("OID_SECTOR")).Distinct()
            retorno.AddRange(AdicionaFilhos(listaOrdenada, list, sIdfilho, nivel))
        Next
        Return retorno
    End Function

    Private Sub SomarSaldoFilhos(ByRef listaOrdenada As List(Of DataRow), lstFilhos As List(Of DataRow))
        If listaOrdenada IsNot Nothing AndAlso listaOrdenada.Count > 0 Then

            If lstFilhos IsNot Nothing AndAlso lstFilhos.Count > 0 Then

                Dim nivelMax = lstFilhos.Max(Function(a) a("NIVEL"))

                For i As Integer = nivelMax To 0 Step -1

                    Dim valor_i As Integer = i
                    Dim divisasNivel = From filho In lstFilhos
                                       Where filho("NIVEL") = valor_i AndAlso filho("COD_ISO_DIVISA") IsNot DBNull.Value
                                       Order By filho("COD_ISO_DIVISA")
                                       Group filho By divisa = filho("COD_ISO_DIVISA") Into grpFilho = Group _
                                       Select New With {.CodIsoDivisa = divisa, _
                                                        .DesDivisa = grpFilho.First.Item("DES_DIVISA"), _
                                                        .CodColor = grpFilho.First.Item("COD_COLOR"), _
                                                        .NumImporteDisp = grpFilho.Where(Function(a) a("NUM_IMPORTE_DISP") IsNot DBNull.Value).Sum(Function(r) Convert.ToDouble(r("NUM_IMPORTE_DISP"))), _
                                                        .NumImporteNoDisp = grpFilho.Where(Function(a) a("NUM_IMPORTE_NODISP") IsNot DBNull.Value).Sum(Function(r) Convert.ToDouble(r("NUM_IMPORTE_NODISP"))), _
                                                        .NumImporte = grpFilho.Where(Function(a) a("NUM_IMPORTE") IsNot DBNull.Value).Sum(Function(r) Convert.ToDouble(r("NUM_IMPORTE")))}

                    Dim lstIdPaiFIlho = lstFilhos.Where(Function(a) a("NIVEL") IsNot DBNull.Value AndAlso a("NIVEL") = valor_i).Select(Function(b) b("OID_SECTOR_PADRE")).Distinct().ToList()
                    For Each idPaifilho As String In lstIdPaiFIlho

                        Dim paiFilho = listaOrdenada.Where(Function(a) a("OID_SECTOR") = idPaifilho)

                        If paiFilho IsNot Nothing AndAlso paiFilho.Count > 0 Then

                            Dim indicePai = listaOrdenada.IndexOf(paiFilho.First)
                            Dim lstDivisa As New List(Of DataRow)
                            lstDivisa.AddRange(paiFilho.Where(Function(a) a("COD_ISO_DIVISA") IsNot DBNull.Value))

                            For Each divisa In divisasNivel

                                Dim divisaPai = paiFilho.Where(Function(a) a("COD_ISO_DIVISA") IsNot DBNull.Value AndAlso a("COD_ISO_DIVISA") = divisa.CodIsoDivisa)
                                If divisaPai IsNot Nothing AndAlso divisaPai.Count > 0 Then

                                    divisaPai.First.Item("NUM_IMPORTE_DISP") = If(divisaPai.First.Item("NUM_IMPORTE_DISP") IsNot DBNull.Value, Convert.ToDouble(divisaPai.First.Item("NUM_IMPORTE_DISP")) + divisa.NumImporteDisp, 0)
                                    divisaPai.First.Item("NUM_IMPORTE_NODISP") = If(divisaPai.First.Item("NUM_IMPORTE_NODISP") IsNot DBNull.Value, Convert.ToDouble(divisaPai.First.Item("NUM_IMPORTE_NODISP")) + divisa.NumImporteNoDisp, 0)
                                    divisaPai.First.Item("NUM_IMPORTE") = If(divisaPai.First.Item("NUM_IMPORTE") IsNot DBNull.Value, Convert.ToDouble(divisaPai.First.Item("NUM_IMPORTE")) + divisa.NumImporte, 0)

                                Else

                                    Dim dtRow As DataRow = paiFilho.First.Table.NewRow()
                                    dtRow.ItemArray = paiFilho.First.ItemArray

                                    dtRow("COD_ISO_DIVISA") = divisa.CodIsoDivisa
                                    dtRow("DES_DIVISA") = divisa.DesDivisa
                                    dtRow("COD_COLOR") = divisa.CodColor
                                    dtRow("NUM_IMPORTE_DISP") = divisa.NumImporteDisp
                                    dtRow("NUM_IMPORTE_NODISP") = divisa.NumImporteNoDisp
                                    dtRow("NUM_IMPORTE") = divisa.NumImporte

                                    lstDivisa.Add(dtRow)

                                End If

                            Next

                            listaOrdenada.RemoveAll(Function(a) a("OID_SECTOR") = idPaifilho)
                            listaOrdenada.InsertRange(indicePai, lstDivisa.OrderBy(Function(a) a("DES_DIVISA")))
                        End If

                        lstFilhos.RemoveAll(Function(a) a("OID_SECTOR") = idPaifilho)
                        lstFilhos.AddRange(listaOrdenada.Where(Function(a) a("OID_SECTOR") = idPaifilho))

                    Next

                Next

            End If

        End If
    End Sub

    Private Function OrdenaResultadoCliente(list As List(Of DataRow)) As List(Of DataRow)
        Dim listaOrdenada As New List(Of DataRow)

        If list IsNot Nothing AndAlso list.Count > 0 Then

            'Remove duplicado
            list.RemoveAll(Function(a) a("COD_ISO_DIVISA") Is DBNull.Value AndAlso list.Where(Function(b) b("COD_ISO_DIVISA") IsNot DBNull.Value AndAlso a("OID_SECTOR") = b("OID_SECTOR")).Count > 0)

            Dim lstClientes = list.Where(Function(a) a("DES_CLIENTE") IsNot DBNull.Value).Select(Function(b) New With {.CodCliente = b("COD_CLIENTE"), .Cliente = b("DES_CLIENTE"), .CodCanal = b("COD_CANAL"), .Canal = b("DES_CANAL"), .CodSubCanal = b("COD_SUBCANAL"), .SubCanal = b("DES_SUBCANAL")}).OrderBy(Function(c) c.Cliente).ThenBy(Function(d) d.Canal).Distinct()
            Dim clienteAnt As String = String.Empty
            Dim canalAnt As String = String.Empty
            For Each cliente In lstClientes

                If cliente.CodCliente <> clienteAnt OrElse cliente.CodCanal <> canalAnt Then

                    'Lista todos ID dos pais
                    Dim lstPais = list.Where(Function(a) a("OID_SECTOR_PADRE") Is DBNull.Value AndAlso AtribuirValorObj(a("COD_CLIENTE"), GetType(String)) = cliente.CodCliente AndAlso AtribuirValorObj(a("COD_CANAL"), GetType(String)) = cliente.CodCanal).OrderBy(Function(a) a("DES_SECTOR")).Select(Function(b) AtribuirValorObj(b("OID_SECTOR"), GetType(String))).Distinct()

                    For Each sIdPai As String In lstPais

                        'Adiciona todos os registros do pai para o cliente/canal
                        listaOrdenada.AddRange(list.Where(Function(a) AtribuirValorObj(a("OID_SECTOR"), GetType(String)) = sIdPai AndAlso AtribuirValorObj(a("COD_CLIENTE"), GetType(String)) = cliente.CodCliente AndAlso AtribuirValorObj(a("COD_CANAL"), GetType(String)) = cliente.CodCanal).OrderBy(Function(a) a("DES_DIVISA")))
                        'Adiciona os filhos
                        AdicionaFilhosCliente(listaOrdenada, list, sIdPai, cliente.CodCliente, cliente.CodCanal)

                    Next

                    'Adiciona os orfãos
                    Dim lstClienteCanal = list.Where(Function(a) AtribuirValorObj(a("COD_CLIENTE"), GetType(String)) = cliente.CodCliente AndAlso AtribuirValorObj(a("COD_CANAL"), GetType(String)) = cliente.CodCanal)
                    Dim listaOrdenadaCliCan = listaOrdenada.Where(Function(a) AtribuirValorObj(a("COD_CLIENTE"), GetType(String)) = cliente.CodCliente AndAlso AtribuirValorObj(a("COD_CANAL"), GetType(String)) = cliente.CodCanal)
                    listaOrdenada.AddRange(lstClienteCanal.Where(Function(a) a("OID_SECTOR_PADRE") IsNot DBNull.Value AndAlso lstClienteCanal.Where(Function(b) b("OID_SECTOR") = a("OID_SECTOR_PADRE")).Count = 0 AndAlso listaOrdenadaCliCan.Where(Function(c) c("OID_SECTOR") = a("OID_SECTOR")).Count = 0).OrderBy(Function(a) a("DES_SECTOR")).ThenBy(Function(a) a("DES_DIVISA")))

                    'Remove todos sem saldo
                    listaOrdenada.RemoveAll(Function(a) a("COD_ISO_DIVISA") Is DBNull.Value AndAlso AtribuirValorObj(a("COD_CLIENTE"), GetType(String)) = cliente.CodCliente AndAlso AtribuirValorObj(a("COD_CANAL"), GetType(String)) = cliente.CodCanal)

                    'Trata orfãos
                    Dim lstIdOrfaos = listaOrdenada.Where(Function(a) a("OID_SECTOR_PADRE") IsNot DBNull.Value AndAlso AtribuirValorObj(a("COD_CLIENTE"), GetType(String)) = cliente.CodCliente AndAlso AtribuirValorObj(a("COD_CANAL"), GetType(String)) = cliente.CodCanal AndAlso listaOrdenada.Where(Function(b) b("OID_SECTOR") = a("OID_SECTOR_PADRE") AndAlso AtribuirValorObj(b("COD_CLIENTE"), GetType(String)) = cliente.CodCliente AndAlso AtribuirValorObj(b("COD_CANAL"), GetType(String)) = cliente.CodCanal).Count = 0).Select(Function(b) b("OID_SECTOR")).Distinct().ToList()

                    If lstIdOrfaos IsNot Nothing AndAlso lstIdOrfaos.Count > 0 Then
                        For Each sIdOrfao As String In lstIdOrfaos
                            Dim lstOrfaos = listaOrdenada.Where(Function(a) a("OID_SECTOR") = sIdOrfao AndAlso AtribuirValorObj(a("COD_CLIENTE"), GetType(String)) = cliente.CodCliente AndAlso AtribuirValorObj(a("COD_CANAL"), GetType(String)) = cliente.CodCanal).OrderBy(Function(a) a("DES_DIVISA")).ToList()

                            Dim pai = listaOrdenada.Where(Function(a) a("OID_SECTOR") = lstOrfaos.First.Item("OID_SECTOR_PADRE") AndAlso AtribuirValorObj(a("COD_CLIENTE"), GetType(String)) = cliente.CodCliente AndAlso AtribuirValorObj(a("COD_CANAL"), GetType(String)) = cliente.CodCanal)
                            If pai Is Nothing OrElse pai.Count = 0 Then
                                pai = list.Where(Function(a) a("OID_SECTOR") = lstOrfaos.First.Item("OID_SECTOR_PADRE") AndAlso AtribuirValorObj(a("COD_CLIENTE"), GetType(String)) = cliente.CodCliente AndAlso AtribuirValorObj(a("COD_CANAL"), GetType(String)) = cliente.CodCanal).ToList()
                                If pai IsNot Nothing AndAlso pai.Count > 0 Then
                                    listaOrdenada.Insert(listaOrdenada.IndexOf(lstOrfaos.First), pai.First)
                                Else
                                    pai = list.Where(Function(a) a("OID_SECTOR") = lstOrfaos.First.Item("OID_SECTOR_PADRE") AndAlso a("COD_CLIENTE") Is DBNull.Value).ToList()
                                    If pai IsNot Nothing AndAlso pai.Count > 0 Then
                                        Dim dtRow As DataRow = pai.First.Table.NewRow()
                                        dtRow.ItemArray = pai.First.ItemArray
                                        dtRow("COD_CLIENTE") = cliente.CodCliente
                                        dtRow("DES_CLIENTE") = cliente.Cliente
                                        dtRow("COD_CANAL") = cliente.CodCanal
                                        dtRow("DES_CANAL") = cliente.Canal
                                        dtRow("COD_SUBCANAL") = cliente.CodSubCanal
                                        dtRow("DES_SUBCANAL") = cliente.SubCanal
                                        listaOrdenada.Insert(listaOrdenada.IndexOf(lstOrfaos.First), dtRow)
                                    End If
                                End If
                            Else
                                listaOrdenada.RemoveAll(Function(a) a("OID_SECTOR") = sIdOrfao AndAlso AtribuirValorObj(a("COD_CLIENTE"), GetType(String)) = cliente.CodCliente AndAlso AtribuirValorObj(a("COD_CANAL"), GetType(String)) = cliente.CodCanal)
                                listaOrdenada.InsertRange(listaOrdenada.IndexOf(pai.Last) + 1, lstOrfaos)
                            End If
                        Next
                    End If

                End If

                clienteAnt = cliente.CodCliente
                canalAnt = cliente.CodCanal
            Next

            If listaOrdenada.Count = 0 Then
                listaOrdenada = list
            End If

            listaOrdenada = SomarSaldoFilhosCliente(listaOrdenada)

        End If

        Return listaOrdenada
    End Function

    Private Sub AdicionaFilhosCliente(ByRef listaOrdenada As List(Of DataRow), list As List(Of DataRow), sIdPai As String, sCliPai As String, sCanPai As String)
        Dim lstFilhos = list.Where(Function(a) AtribuirValorObj(a("OID_SECTOR_PADRE"), GetType(String)) = sIdPai AndAlso AtribuirValorObj(a("COD_CLIENTE"), GetType(String)) = sCliPai AndAlso AtribuirValorObj(a("COD_CANAL"), GetType(String)) = sCanPai).OrderBy(Function(a) a("DES_SECTOR")).ThenBy(Function(a) a("DES_DIVISA")).ToList()
        Dim lstFilhosSemSaldo = list.Where(Function(a) AtribuirValorObj(a("OID_SECTOR_PADRE"), GetType(String)) = sIdPai AndAlso a("COD_CLIENTE") Is DBNull.Value).OrderBy(Function(a) a("DES_SECTOR")).ToList()
        Dim sIdAnt As String = String.Empty
        For Each filhoSemSaldo In lstFilhosSemSaldo
            If sIdAnt <> filhoSemSaldo("OID_SECTOR") Then
                Dim dtRow As DataRow = filhoSemSaldo.Table.NewRow()
                dtRow.ItemArray = filhoSemSaldo.ItemArray
                dtRow("COD_CLIENTE") = sCliPai
                dtRow("COD_CANAL") = sCanPai
                lstFilhos.Add(dtRow)
            End If
            sIdAnt = filhoSemSaldo("OID_SECTOR")
        Next
        listaOrdenada.AddRange(lstFilhos)

        For Each filho As DataRow In lstFilhos
            AdicionaFilhosCliente(listaOrdenada, list, AtribuirValorObj(filho("OID_SECTOR"), GetType(String)), AtribuirValorObj(filho("COD_CLIENTE"), GetType(String)), AtribuirValorObj(filho("COD_CANAL"), GetType(String)))
        Next
    End Sub

    Private Function SomarSaldoFilhosCliente(lista As List(Of DataRow)) As List(Of DataRow)
        Dim listaOrdenada As New List(Of DataRow)
        If lista IsNot Nothing AndAlso lista.Count > 0 Then

            Dim lstClientes = lista.Where(Function(a) a("DES_CLIENTE") IsNot DBNull.Value).Select(Function(b) New With {.CodCliente = b("COD_CLIENTE"), .Cliente = b("DES_CLIENTE"), .CodCanal = b("COD_CANAL"), .Canal = b("DES_CANAL"), .CodSubCanal = b("COD_SUBCANAL"), .SubCanal = b("DES_SUBCANAL")}).OrderBy(Function(c) c.Cliente).ThenBy(Function(d) d.Canal).Distinct()
            Dim clienteAnt As String = String.Empty
            Dim canalAnt As String = String.Empty
            For Each cliente In lstClientes

                If cliente.CodCliente <> clienteAnt OrElse cliente.CodCanal <> canalAnt Then

                    Dim listaOrdenadaCliente As IEnumerable(Of DataRow) = lista.Where(Function(a) AtribuirValorObj(a("COD_CLIENTE"), GetType(String)) = cliente.CodCliente AndAlso AtribuirValorObj(a("COD_CANAL"), GetType(String)) = cliente.CodCanal).ToList()

                    'Lista todos ID dos pais
                    Dim lstPais = listaOrdenadaCliente.Where(Function(a) a("OID_SECTOR_PADRE") Is DBNull.Value).OrderBy(Function(a) a("DES_SECTOR")).Select(Function(b) b("OID_SECTOR")).Distinct()

                    For Each sIdPai As String In lstPais

                        'Adiciona os filhos
                        Dim lstFilhos = RetornaFilhos(listaOrdenadaCliente, sIdPai, 1)

                        SomarSaldoFilhos(listaOrdenadaCliente, lstFilhos)

                    Next

                    listaOrdenada.AddRange(listaOrdenadaCliente)

                End If

                clienteAnt = cliente.CodCliente
                canalAnt = cliente.CodCanal
            Next

        End If
        Return listaOrdenada
    End Function

    Private Function RetornaFilhos(listaOrdenadaCliente As List(Of DataRow), sIdPai As String, nivel As Integer) As List(Of DataRow)
        Dim retorno As New List(Of DataRow)

        Dim lstFilhos = listaOrdenadaCliente.Where(Function(a) AtribuirValorObj(a("OID_SECTOR_PADRE"), GetType(String)) = sIdPai).OrderBy(Function(a) a("DES_SECTOR")).ThenBy(Function(a) a("DES_DIVISA")).ToList()

        If lstFilhos IsNot Nothing AndAlso lstFilhos.Count > 0 Then
            For Each filho In lstFilhos
                If Not filho.Table.Columns.Contains("NIVEL") Then
                    filho.Table.Columns.Add("NIVEL")
                End If
                filho("NIVEL") = nivel
            Next
        End If

        retorno.AddRange(lstFilhos)
        nivel += 1
        For Each sIdfilho As String In lstFilhos.Select(Function(a) a("OID_SECTOR")).Distinct()
            retorno.AddRange(RetornaFilhos(listaOrdenadaCliente, sIdfilho, nivel))
        Next
        Return retorno
    End Function

#End Region

#End Region


End Class
