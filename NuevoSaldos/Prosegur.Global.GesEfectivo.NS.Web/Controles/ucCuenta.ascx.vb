Imports System.Web.UI
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Public Class ucCuenta
    Inherits UcBase

#Region "[PROPRIEDADES]"

    Private _SectorSelecionado As Clases.Sector = Nothing
    Public ReadOnly Property SectorSelecionado() As Clases.Sector
        Get
            If _SectorSelecionado Is Nothing Then
                _SectorSelecionado = Prosegur.Genesis.LogicaNegocio.Genesis.Sector.ObtenerPorOid(Me.Attributes("identificadorSector").ToString())
            End If
            Return _SectorSelecionado
        End Get

    End Property
    Public Property Cuenta As Cuenta
        Get
            If ViewState(ID & "_Cuenta") Is Nothing Then
                ViewState(ID & "_Cuenta") = New Cuenta
            End If
            Return ViewState(ID & "_Cuenta")
        End Get
        Set(value As Cuenta)
            ViewState(ID & "_Cuenta") = value
        End Set
    End Property

    Public Property CuentaSaldo As Cuenta
        Get
            If ViewState(ID & "_CuentaSaldo") Is Nothing Then
                ViewState(ID & "_CuentaSaldo") = New Cuenta
            End If
            Return ViewState(ID & "_CuentaSaldo")
        End Get
        Set(value As Cuenta)
            ViewState(ID & "_CuentaSaldo") = value
        End Set
    End Property

    Public Property Modo As Genesis.Comon.Enumeradores.Modo

    Public Property TipoSitio As Enumeradores.TipoSitio

    Public Property SelecaoMultipla As Boolean = False

    ''' <summary>
    ''' Define o número máximo de registros a serem retornados no Grid por paginação.
    ''' * Obs: El valor defecto es 0 (cero). Enviando 0 (cero) para ucHelperBusquedaAvanzada, el componente exibe 10 registros por pagina
    ''' </summary>
    Private _MaxRegistroPorPagina As Integer = 10
    Public Property MaxRegistroPorPagina As Integer
        Get
            Return _MaxRegistroPorPagina
        End Get
        Set(value As Integer)
            _MaxRegistroPorPagina = value
        End Set
    End Property

    ' Eventos.
    Public Event UpdatedControl(sender As Object)

#Region "[ucSector]"

    Private WithEvents _ucSector As ucSector
    Public Property ucSector() As ucSector
        Get
            If _ucSector Is Nothing Then
                _ucSector = LoadControl("~\Controles\ucSector.ascx")
                _ucSector.ID = "ucSector"
                _ucSector.SelecaoMultipla = SelecaoMultipla
                _ucSector.Modo = Modo
                AddHandler _ucSector.Erro, AddressOf ErroControles
                phSector.Controls.Add(_ucSector)
            End If
            Return _ucSector
        End Get
        Set(value As ucSector)
            _ucSector = value
        End Set
    End Property

#End Region

#Region "[ucCliente]"

    Private WithEvents _ucCliente As ucCliente
    Public Property ucCliente() As ucCliente
        Get
            If _ucCliente Is Nothing Then
                _ucCliente = LoadControl("~\Controles\ucCliente.ascx")
                _ucCliente.ID = "ucCliente"
                _ucCliente.SelecaoMultipla = SelecaoMultipla
                _ucCliente.Modo = Modo
                AddHandler _ucCliente.Erro, AddressOf ErroControles
                phCliente.Controls.Add(_ucCliente)
            End If
            Return _ucCliente
        End Get
        Set(value As ucCliente)
            _ucCliente = value
        End Set
    End Property

#End Region

#Region "[ucCanal]"

    Private WithEvents _ucCanal As ucCanal
    Public Property ucCanal() As ucCanal
        Get
            If _ucCanal Is Nothing Then
                _ucCanal = LoadControl("~\Controles\ucCanal.ascx")
                _ucCanal.ID = "ucCanal"
                _ucCanal.SelecaoMultipla = SelecaoMultipla
                _ucCanal.Modo = Modo
                AddHandler _ucCanal.Erro, AddressOf ErroControles
                phCanal.Controls.Add(_ucCanal)
            End If
            Return _ucCanal
        End Get
        Set(value As ucCanal)
            _ucCanal = value
        End Set
    End Property

#End Region

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Inicializa controles na tela.
    ''' </summary>
    Protected Overrides Sub Inicializar()
        Try
            Me.ConfigurarControles()
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Configura foco nos UserControls 
    ''' </summary>
    Public Overrides Sub Focus()
        MyBase.Focus()
    End Sub

    Public Overrides Function ValidarControl() As System.Collections.Generic.List(Of String)
        Dim mensajes As New List(Of String)
        Dim nomeDicionario = String.Empty
        Dim campo As String = String.Empty
        If Me.ClientID.ToUpper().EndsWith("ORIGEN") Then
            nomeDicionario = Traduzir("017_Titulo_CuentaOrigen")
        ElseIf Me.ClientID.ToUpper().EndsWith("DESTINO") Then
            nomeDicionario = Traduzir("017_Titulo_CuentaDestino")
        End If

        If Me.ucSector IsNot Nothing AndAlso Me.ucSector.SectorHabilitado AndAlso (Me.ucSector.Sector Is Nothing OrElse String.IsNullOrEmpty(Me.ucSector.Sector.Identificador)) Then
            campo = String.Format("{0} {1}", Traduzir("018_Sector_Titulo"), nomeDicionario)
            mensajes.Add(String.Format(Traduzir("msg_campo_obrigatorio"), campo))
        End If

        If Me.ucCliente IsNot Nothing AndAlso Me.ucCliente.ClienteHabilitado Then
            If Me.ucCliente.Clientes IsNot Nothing AndAlso Me.ucCliente.Clientes.Count = 0 Then
                campo = String.Format("{0} {1}", Traduzir("016_TituloCliente"), nomeDicionario)
                mensajes.Add(String.Format(Traduzir("msg_campo_obrigatorio"), campo))
            End If
        End If

        If Me.ucCanal IsNot Nothing AndAlso Me.ucCanal.CanalHabilitado Then
            If Me.ucCanal.Canales IsNot Nothing AndAlso Me.ucCanal.Canales.Count = 0 Then
                campo = String.Format("{0} {1}", Traduzir("016_TituloCanal"), nomeDicionario)
                mensajes.Add(String.Format(Traduzir("msg_campo_obrigatorio"), campo))
            Else
                If Me.ucCanal.ucSubCanal IsNot Nothing AndAlso Me.ucCanal.ucSubCanal.ControleHabilitado Then
                    If Me.ucCanal.Canales(0).SubCanales Is Nothing OrElse Me.ucCanal.Canales(0).SubCanales.Count = 0 Then
                        campo = String.Format("{0} {1}", Traduzir("016_TituloSubCanal"), nomeDicionario)
                        mensajes.Add(String.Format(Traduzir("msg_campo_obrigatorio"), campo))
                    End If
                End If
            End If
        End If

        Return mensajes
    End Function
#End Region

#Region "[EVENTOS]"

    Public Sub ucCanal_OnControleAtualizado(sender As ControleEventArgs) Handles _ucCanal.UpdatedControl
        Try
            If Me.ucCanal.Canales IsNot Nothing AndAlso Me.ucCanal.Canales.Count > 0 Then
                Me.Cuenta.Canal = Me.ucCanal.Canales(0)
                If Me.ucCanal.Canales(0).SubCanales IsNot Nothing AndAlso Me.ucCanal.Canales(0).SubCanales.Count > 0 Then
                    Me.Cuenta.SubCanal = Me.ucCanal.Canales(0).SubCanales(0)
                End If
            Else
                Me.Cuenta.Canal = Nothing
            End If
            NotificarControleAtualizado(sender.Controle)
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    Public Sub ucSector_OnControleAtualizado() Handles _ucSector.UpdatedControl
        Try
            If Me.ucSector.Sector IsNot Nothing Then
                Me.Cuenta.Sector = Me.ucSector.Sector
            Else
                Me.Cuenta.Sector = Nothing
            End If
            NotificarControleAtualizado("Sector")
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    Public Sub ucCliente_OnControleAtualizado() Handles _ucCliente.UpdatedControl
        Try
            If Me.ucCliente.Clientes IsNot Nothing AndAlso Me.ucCliente.Clientes.Count > 0 Then
                Me.Cuenta.Cliente = Me.ucCliente.Clientes(0)
                If Me.ucCliente.Clientes(0).SubClientes IsNot Nothing AndAlso Me.ucCliente.Clientes(0).SubClientes.Count > 0 Then
                    Me.Cuenta.SubCliente = Me.ucCliente.Clientes(0).SubClientes(0)

                    If Me.ucCliente.Clientes(0).SubClientes(0).PuntosServicio IsNot Nothing AndAlso Me.ucCliente.Clientes(0).SubClientes(0).PuntosServicio.Count > 0 Then
                        Me.Cuenta.PuntoServicio = Me.ucCliente.Clientes(0).SubClientes(0).PuntosServicio(0)
                    Else
                        Me.Cuenta.PuntoServicio = Nothing
                    End If
                Else
                    Me.Cuenta.Cliente.SubClientes = Nothing
                    Me.Cuenta.SubCliente = Nothing
                    Me.Cuenta.PuntoServicio = Nothing
                End If
            Else
                Me.Cuenta.Cliente = Nothing
            End If

            NotificarControleAtualizado("Cliente")
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Configura Controles do Controle. Ignorando o Overrides.
    ''' </summary>
    Protected Sub TraduzirControle()
        If (Me.TipoSitio = Enumeradores.TipoSitio.Origen) Then
            lblTitulo.Text = Traduzir("017_Titulo_CuentaOrigen")
        Else
            lblTitulo.Text = Traduzir("017_Titulo_CuentaDestino")
        End If
    End Sub

    ''' <summary>
    ''' Carrega Canal e SubCanal utilizando os parâmetros definidos pelo usuário.
    ''' </summary>
    Protected Sub ConfigurarControles()

        TraduzirControle()

        ConfigurarControle_Sector()
        ConfigurarControle_Cliente()
        ConfigurarControle_Canal()
        ConfigurarControle_Totalizador()

    End Sub

    Protected Sub ConfigurarControle_Sector()

        If Me.Cuenta IsNot Nothing AndAlso Me.Cuenta.Sector IsNot Nothing Then
            Me.ucSector.Sector = Me.Cuenta.Sector
        End If

        If Me.TipoSitio = Enumeradores.TipoSitio.Origen Then
            Me.ucSector.SectorFiltro.Add(New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.SectorXFormulario}, _
                                         New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("COD_RELACION_CON_FORMULARIO", "O")})
        Else
            Me.ucSector.SectorFiltro.Add(New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.SectorXFormulario}, _
                                         New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("COD_RELACION_CON_FORMULARIO", "D")})

            ' Verifica se a tabela de setor existe
            Dim filtroSector = Me.ucSector.SectorFiltro.FirstOrDefault(Function(sf) sf.Key.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Sector)

            ' Se não existe
            If filtroSector.Value Is Nothing Then

                ' Adiciona o filtro
                Me.ucSector.SectorFiltro.Add(New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Sector},
                                             New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("COD_SECTOR", SectorSelecionado.Codigo, Helper.Enumeradores.EnumHelper.TipoCondicion.Diferente)})
            Else

                ' atualiza o filtro
                filtroSector.Value.Add(New ArgumentosFiltro("COD_SECTOR", SectorSelecionado.Codigo, Helper.Enumeradores.EnumHelper.TipoCondicion.Diferente))

            End If

        End If


    End Sub

    Protected Sub ConfigurarControle_Cliente()
        If Me.Cuenta IsNot Nothing AndAlso Me.Cuenta.Cliente IsNot Nothing Then
            Me.ucCliente.Clientes.Clear()
            Me.ucCliente.Clientes.Add(Me.Cuenta.Cliente)
            If Me.Cuenta.SubCliente IsNot Nothing Then
                If Me.ucCliente.Clientes(0).SubClientes Is Nothing Then
                    Me.ucCliente.Clientes(0).SubClientes = New ObservableCollection(Of SubCliente)
                End If
                Me.ucCliente.Clientes(0).SubClientes.Clear()
                Me.ucCliente.Clientes(0).SubClientes.Add(Me.Cuenta.SubCliente)

                If Me.Cuenta.PuntoServicio IsNot Nothing Then

                    If Me.ucCliente.Clientes(0).SubClientes(0).PuntosServicio Is Nothing Then
                        Me.ucCliente.Clientes(0).SubClientes(0).PuntosServicio = New ObservableCollection(Of PuntoServicio)
                    End If
                    Me.ucCliente.Clientes(0).SubClientes(0).PuntosServicio.Clear()
                    Me.ucCliente.Clientes(0).SubClientes(0).PuntosServicio.Add(Me.Cuenta.PuntoServicio)

                End If
            End If
        End If
    End Sub

    Protected Sub ConfigurarControle_Canal()
        If Me.Cuenta IsNot Nothing AndAlso Me.Cuenta.Canal IsNot Nothing Then
            Me.ucCanal.Canales.Clear()
            Me.ucCanal.Canales.Add(Me.Cuenta.Canal)

            If Me.Cuenta.SubCanal IsNot Nothing Then
                If Me.ucCanal.Canales(0).SubCanales Is Nothing Then
                    Me.ucCanal.Canales(0).SubCanales = New ObservableCollection(Of SubCanal)
                End If
                Me.ucCanal.Canales(0).SubCanales.Clear()
                Me.ucCanal.Canales(0).SubCanales.Add(Me.Cuenta.SubCanal)

            End If

        End If
    End Sub

    Protected Sub ConfigurarControle_Totalizador()

        If Modo = Enumeradores.Modo.Consulta AndAlso
           Me.CuentaSaldo IsNot Nothing AndAlso
           Me.CuentaSaldo.Cliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(Me.CuentaSaldo.Cliente.Codigo) Then

            Me.lblTotalizador.Text = Traduzir("017_totalizaen")

            Me.lblValorTotalizador.Text = Me.CuentaSaldo.Cliente.Codigo + " - " + Me.CuentaSaldo.Cliente.Descripcion

            If Me.CuentaSaldo.SubCliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(Me.CuentaSaldo.SubCliente.Codigo) Then
                Me.lblValorTotalizador.Text += " / " + Me.CuentaSaldo.SubCliente.Codigo + " - " + Me.CuentaSaldo.SubCliente.Descripcion
            End If

            If Me.CuentaSaldo.PuntoServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(Me.CuentaSaldo.PuntoServicio.Codigo) Then
                Me.lblValorTotalizador.Text += " / " + Me.CuentaSaldo.PuntoServicio.Codigo + " - " + Me.CuentaSaldo.PuntoServicio.Descripcion
            End If

            dvTotalizador.Style.Item("display") = "block"

        Else
            Me.lblTotalizador.Text = String.Empty
            Me.lblValorTotalizador.Text = String.Empty
            dvTotalizador.Style.Item("display") = "none"

        End If

    End Sub

    ''' <summary>
    ''' Trata mensgem de erro dos Controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.NotificarErro(e.Erro)
    End Sub

#End Region


End Class