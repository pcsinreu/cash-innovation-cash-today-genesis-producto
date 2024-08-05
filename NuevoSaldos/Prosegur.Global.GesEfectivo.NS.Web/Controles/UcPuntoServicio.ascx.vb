Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones

Public Class UcPuntoServicio
    Inherits UcBase



#Region "[PROPRIEDADES]"
    Public Property Delegaciones As List(Of String)
        Get
            If Session(ID & "_Delegaciones") Is Nothing Then
                Session(ID & "_Delegaciones") = New List(Of String)
            End If
            Return Session(ID & "_Delegaciones")
        End Get
        Set(value As List(Of String))
            Session(ID & "_Delegaciones") = value
        End Set
    End Property

    Public Property Clientes As ObservableCollection(Of Cliente)
        Get
            If ViewState(ID & "_Clientes") Is Nothing Then
                ViewState(ID & "_Clientes") = New ObservableCollection(Of Cliente)
            End If
            Return ViewState(ID & "_Clientes")
        End Get
        Set(value As ObservableCollection(Of Cliente))
            ViewState(ID & "_Clientes") = value
        End Set
    End Property

    Public Property Modo As Genesis.Comon.Enumeradores.Modo

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

    Public Property SelecaoMultipla As Boolean = False

    ' Eventos.
    Public Event UpdatedControl(sender As Object)

    Private joinSQL As UtilHelper.JoinSQL

    Public Property TotalizadorSaldo As Boolean = False

    Public Property EsBanco As Boolean = False
    Public Property EsBancoCapital As Boolean = False

#Region "[ucCliente]"

    Private WithEvents _ucCliente As ucHelperBusquedaDatos
    Public Property ucCliente() As ucHelperBusquedaDatos
        Get
            If _ucCliente Is Nothing Then
                _ucCliente = LoadControl("~\Controles\ucHelperBusquedaDatos.ascx")
                _ucCliente.ID = "ucCliente"
                _ucCliente.HeightPopUp = 700
                AddHandler _ucCliente.Erro, AddressOf ErroControles
                If phCliente.Controls.Count = 0 Then
                    phCliente.Controls.Add(_ucCliente)
                End If
            End If
            Return _ucCliente
        End Get
        Set(value As ucHelperBusquedaDatos)
            _ucCliente = value
        End Set
    End Property

    ''' <summary>
    ''' Define o Filtro a ser incluído na pesquisa de Canais.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _ClienteFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property ClienteFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        Get
            If _ClienteFiltro Is Nothing Then
                ' Seta valor defector
                _ClienteFiltro = New Dictionary(Of UtilHelper.Tabela, List(Of ArgumentosFiltro)) From
                {
                    {
                        New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Cliente},
                        New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("BOL_VIGENTE", "1")}
                    }
                }

                If EsBancoCapital Then
                    _ClienteFiltro(New UtilHelper.Tabela With {.Tabela = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.Cliente}).Add(New ArgumentosFiltro("BOL_BANCO_CAPITAL", "1"))
                End If


            End If
            Return _ClienteFiltro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _ClienteFiltro = value
        End Set
    End Property

    ''' <summary>
    ''' Define as junções a serem incluídas para a pesquisa de Cliente.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>    
    Private _ClienteJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
    Public Property ClienteJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
        Get
            _ClienteJuncao = New Dictionary(Of String, UtilHelper.JoinSQL)

            If EsBanco Then

                ' Obtém código de Maquina selecionado.
                joinSQL = New UtilHelper.JoinSQL With {.TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = TabelaHelper.TipoCliente},
                                                           .CampoComumTabEsq = "OID_TIPO_CLIENTE",
                                                           .TabelaDireita = New UtilHelper.Tabela With {.Tabela = TabelaHelper.Cliente},
                                                           .CampoComumTabDireita = "OID_TIPO_CLIENTE",
                                                           .NomeCampoChave = "COD_TIPO_CLIENTE",
                                                           .ValorCampoChave = "1"
                                                          }

                ' Atribui um nome a chave do dicionário e os valores a serem incluídos na cláusula Join.        
                _ClienteJuncao.Add(("Join_" + joinSQL.TabelaEsquerda.Tabela.ToString()), joinSQL)

            End If

            Return _ClienteJuncao
        End Get
        Set(value As Dictionary(Of String, UtilHelper.JoinSQL))
            _ClienteJuncao = value
        End Set
    End Property

    ''' <summary>
    ''' Define como os dados retornados da consulta de Canais deverão ser ordenados.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _ClienteOrden As Dictionary(Of String, UtilHelper.OrderSQL)
    Public Property ClienteOrden As Dictionary(Of String, UtilHelper.OrderSQL)
        Get
            If _ClienteOrden Is Nothing Then
                ' Seta valor defector
                _ClienteOrden = New Dictionary(Of String, OrderSQL) From {{"COD_CLIENTE", New OrderSQL("COD_CLIENTE")}}
            End If
            Return _ClienteOrden
        End Get
        Set(value As Dictionary(Of String, UtilHelper.OrderSQL))
            _ClienteOrden = value
        End Set
    End Property

    Public Property ClienteHabilitado As Boolean = False
    Public Property ClienteQueryDefecto As UtilHelper.QueryHelperControl = Nothing
    Public Property NoExhibirCliente As Boolean? = Nothing

    Private _ClienteVisible As Boolean = True
    Private Property ClienteVisible As Boolean
        Get
            If NoExhibirCliente IsNot Nothing Then
                Return Not NoExhibirCliente
            Else
                Return _ClienteVisible
            End If
        End Get
        Set(value As Boolean)
            _ClienteVisible = value
        End Set
    End Property


#End Region

#Region "[ucMaquina]"

    Private WithEvents _ucMaquina As ucHelperBusquedaDatos
    Public Property ucMaquina() As ucHelperBusquedaDatos
        Get
            If _ucMaquina Is Nothing Then
                _ucMaquina = LoadControl("~\Controles\ucHelperBusquedaDatos.ascx")
                _ucMaquina.ID = "ucMaquina"
                AddHandler _ucMaquina.Erro, AddressOf ErroControles
                If phMaquina.Controls.Count = 0 Then
                    phMaquina.Controls.Add(_ucMaquina)
                End If
            End If
            Return _ucMaquina
        End Get
        Set(value As ucHelperBusquedaDatos)
            _ucMaquina = value
        End Set
    End Property

    ''' <summary>
    ''' Define o Filtro a ser incluído na pesquisa de Maquinas.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _MaquinaFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property MaquinaFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        Get
            If _MaquinaFiltro Is Nothing Then
                ' Seta valor defector
                _MaquinaFiltro = New Dictionary(Of UtilHelper.Tabela, List(Of ArgumentosFiltro)) From
                {
                    {
                        New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.MaquinaPunto},
                        New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("BOL_ACTIVO", "1")}
                    }
                }

                Dim objValorCampoChave As String = ""
                If delegaciones IsNot Nothing Then

                    For Each d As String In delegaciones
                        If Not String.IsNullOrEmpty(d) Then
                            objValorCampoChave &= ",'" & d & "'"
                        End If
                    Next
                    If Not String.IsNullOrEmpty(objValorCampoChave) Then

                        _MaquinaFiltro(New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.MaquinaPunto}).Add(
                        New ArgumentosFiltro("OID_DELEGACION", "in (" + objValorCampoChave.Substring(1) + ")", Helper.Enumeradores.EnumHelper.TipoCondicion.Avancado))
                    End If
                End If

            End If
            Return _MaquinaFiltro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _MaquinaFiltro = value
        End Set
    End Property

    ''' <summary>
    ''' Define as junções a serem incluídas para a pesquisa de Maquina.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>    
    Private _MaquinaJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
    Public Property MaquinaJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
        Get

            _MaquinaJuncao = New Dictionary(Of String, UtilHelper.JoinSQL)

            If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then

                Dim objValorCampoChave As String = ""
                For Each c As Clases.Cliente In Clientes
                    If Not String.IsNullOrEmpty(c.Identificador) Then
                        objValorCampoChave &= "," & c.Identificador
                    End If
                Next

                If Not String.IsNullOrEmpty(objValorCampoChave) AndAlso Not String.IsNullOrEmpty(objValorCampoChave.Substring(1)) Then
                    ' Obtém código de SubCliente selecionado.
                    joinSQL = New UtilHelper.JoinSQL With {.TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = TabelaHelper.Cliente},
                                                           .CampoComumTabEsq = "OID_CLIENTE",
                                                           .TabelaDireita = New UtilHelper.Tabela With {.Tabela = TabelaHelper.MaquinaPunto},
                                                           .CampoComumTabDireita = "OID_CLIENTE",
                                                           .NomeCampoChave = "OID_CLIENTE",
                                                           .ValorCampoChave = objValorCampoChave.Substring(1)
                                                          }

                    ' Atribui um nome a chave do dicionário e os valores a serem incluídos na cláusula Join.        
                    _MaquinaJuncao.Add(("Join_" + joinSQL.TabelaEsquerda.Tabela.ToString()), joinSQL)
                End If

            End If

            Return _MaquinaJuncao
        End Get
        Set(value As Dictionary(Of String, UtilHelper.JoinSQL))
            _MaquinaJuncao = value
        End Set
    End Property

    ''' <summary>
    ''' Define como os dados retornados da consulta de SubCanais deverão ser ordenados.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _MaquinaOrden As Dictionary(Of String, UtilHelper.OrderSQL)
    Public Property MaquinaOrden As Dictionary(Of String, UtilHelper.OrderSQL)
        Get
            If _MaquinaOrden Is Nothing Then
                ' Seta valor defector
                _MaquinaOrden = New Dictionary(Of String, OrderSQL) From {{"DES_SECTOR", New OrderSQL("DES_SECTOR")}}
            End If
            Return _MaquinaOrden
        End Get
        Set(value As Dictionary(Of String, UtilHelper.OrderSQL))
            _MaquinaOrden = value
        End Set
    End Property

    Public Property MaquinaHabilitado As Boolean = False
    Public Property MaquinaQueryDefecto As UtilHelper.QueryHelperControl = Nothing
    Public Property NoExhibirMaquina As Boolean? = Nothing

    Private _MaquinaVisible As Boolean = False
    Private Property MaquinaVisible As Boolean
        Get
            If NoExhibirMaquina IsNot Nothing Then
                Return Not NoExhibirMaquina
            Else
                Return _MaquinaVisible
            End If
        End Get
        Set(value As Boolean)
            _MaquinaVisible = value
        End Set
    End Property

#End Region

#Region "[ucPtoServicio]"

    Private WithEvents _ucPtoServicio As ucHelperBusquedaDatos
    Public Property ucPtoServicio() As ucHelperBusquedaDatos
        Get
            If _ucPtoServicio Is Nothing Then
                _ucPtoServicio = LoadControl("~\Controles\ucHelperBusquedaDatos.ascx")
                _ucPtoServicio.ID = "ucPtoServicio"
                AddHandler _ucPtoServicio.Erro, AddressOf ErroControles
                If phPtoServicio.Controls.Count = 0 Then
                    phPtoServicio.Controls.Add(_ucPtoServicio)
                End If
            End If
            Return _ucPtoServicio
        End Get
        Set(value As ucHelperBusquedaDatos)
            _ucPtoServicio = value
        End Set
    End Property

    ''' <summary>
    ''' Define o Filtro a ser incluído na pesquisa de Maquinas.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _PtoServicioFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property PtoServicioFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        Get
            If _PtoServicioFiltro Is Nothing Then
                ' Seta valor defector
                _PtoServicioFiltro = New Dictionary(Of UtilHelper.Tabela, List(Of ArgumentosFiltro)) From
                {
                    {
                        New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.PuntoServicioMaquina},
                        New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("BOL_VIGENTE", "1")}
                    }
                }
            End If
            Return _PtoServicioFiltro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _PtoServicioFiltro = value
        End Set
    End Property

    ''' <summary>
    ''' Define as junções a serem incluídas para a pesquisa de Maquina.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>    
    Private _PtoServicioJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
    Public Property PtoServicioJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
        Get
            _PtoServicioJuncao = New Dictionary(Of String, UtilHelper.JoinSQL)

            If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then

                Dim objValorCampoChave As String = ""
                For Each c As Clases.Cliente In Clientes
                    If c.Maquinas IsNot Nothing AndAlso c.Maquinas.Count > 0 Then
                        For Each s As Clases.Maquina In c.Maquinas
                            If Not String.IsNullOrEmpty(s.Identificador) Then
                                objValorCampoChave &= "," & s.Identificador
                            End If
                        Next
                    End If
                Next

                If Not String.IsNullOrEmpty(objValorCampoChave) AndAlso Not String.IsNullOrEmpty(objValorCampoChave.Substring(1)) Then
                    joinSQL = New UtilHelper.JoinSQL With {.TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = TabelaHelper.MaquinaPunto},
                                                   .CampoComumTabEsq = "OID_MAQUINA",
                                                   .TabelaDireita = New UtilHelper.Tabela With {.Tabela = TabelaHelper.PuntoServicioMaquina},
                                                   .CampoComumTabDireita = "OID_MAQUINA",
                                                   .NomeCampoChave = "OID_MAQUINA",
                                                   .ValorCampoChave = objValorCampoChave.Substring(1)
                                                  }
                    _PtoServicioJuncao.Add(("Join_" + joinSQL.TabelaEsquerda.Tabela.ToString()), joinSQL)
                End If

            End If

            Return _PtoServicioJuncao
        End Get
        Set(value As Dictionary(Of String, UtilHelper.JoinSQL))
            _PtoServicioJuncao = value
        End Set
    End Property

    ''' <summary>
    ''' Define como os dados retornados da consulta de SubCanais deverão ser ordenados.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _PtoServicioOrden As Dictionary(Of String, UtilHelper.OrderSQL)
    Public Property PtoServicioOrden As Dictionary(Of String, UtilHelper.OrderSQL)
        Get
            If _PtoServicioOrden Is Nothing Then
                ' Seta valor defector
                _PtoServicioOrden = New Dictionary(Of String, OrderSQL) From {{"COD_PTO_SERVICIO", New OrderSQL("COD_PTO_SERVICIO")}}
            End If
            Return _PtoServicioOrden
        End Get
        Set(value As Dictionary(Of String, UtilHelper.OrderSQL))
            _PtoServicioOrden = value
        End Set
    End Property

    Public Property PtoServicioHabilitado As Boolean = False
    Public Property PtoServicioQueryDefecto As UtilHelper.QueryHelperControl = Nothing
    Public Property NoExhibirPtoServicio As Boolean? = Nothing

    Private _PtoServicioVisible As Boolean = False
    Private Property PtoServicioVisible As Boolean
        Get
            If NoExhibirPtoServicio IsNot Nothing Then
                Return Not NoExhibirPtoServicio
            Else
                Return _PtoServicioVisible
            End If
        End Get
        Set(value As Boolean)
            _PtoServicioVisible = value
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

            If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
                Dim objClientes = Clientes.FindAll(Function(c) c.Maquinas IsNot Nothing AndAlso c.Maquinas.Count > 0)
                If (objClientes IsNot Nothing AndAlso objClientes.Count > 0) Then
                    MaquinaVisible = True

                    For Each objCliente In objClientes
                        Dim objMaquinas = objCliente.Maquinas.FindAll(Function(c) c.PuntosServicio IsNot Nothing AndAlso c.PuntosServicio.Count > 0)
                        If (objMaquinas IsNot Nothing AndAlso objMaquinas.Count > 0) OrElse Modo <> Enumeradores.Modo.Consulta Then
                            PtoServicioVisible = True
                            Exit For
                        End If
                    Next
                ElseIf Modo <> Enumeradores.Modo.Consulta Then
                    MaquinaVisible = True
                End If
            End If

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
        ucCliente.Focus()
    End Sub

#End Region

#Region "[EVENTOS]"

    Public Sub ucCliente_OnControleAtualizado() Handles _ucCliente.UpdatedControl
        Try
            If ucCliente.RegistrosSelecionados IsNot Nothing AndAlso ucCliente.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucCliente.RegistrosSelecionados.DatosRespuesta.Count > 0 Then

                If Clientes Is Nothing OrElse Clientes.Count = 0 Then

                    For Each objDatosRespuesta In ucCliente.RegistrosSelecionados.DatosRespuesta

                        Clientes.Add(New Clases.Cliente With {.Identificador = objDatosRespuesta.Identificador,
                                                      .Codigo = objDatosRespuesta.Codigo,
                                                      .Descripcion = objDatosRespuesta.Descricao,
                                                      .Maquinas = Nothing})

                    Next

                ElseIf Clientes IsNot Nothing Then

                    For Each objCliente As Clases.Cliente In Clientes.Clonar()
                        Dim objClienteLocal = objCliente
                        Dim aux = ucCliente.RegistrosSelecionados.DatosRespuesta.Find(Function(x) x.Identificador = objClienteLocal.Identificador)
                        If aux Is Nothing Then
                            Clientes.RemoveAll(Function(c) c.Identificador = objClienteLocal.Identificador)
                        End If
                    Next

                    For Each objDatosRespuesta In ucCliente.RegistrosSelecionados.DatosRespuesta
                        Dim objDatosRespuestaLocal = objDatosRespuesta
                        Dim aux = Clientes.Find(Function(x) x.Identificador = objDatosRespuestaLocal.Identificador)
                        If aux Is Nothing Then
                            Clientes.Add(New Clases.Cliente With {.Identificador = objDatosRespuesta.Identificador,
                                                                .Codigo = objDatosRespuesta.Codigo,
                                                                .Descripcion = objDatosRespuesta.Descricao,
                                                                .Maquinas = Nothing})
                        End If
                    Next

                End If

                ucMaquina.LimparViewState()
                ucPtoServicio.LimparViewState()

                MaquinaVisible = False
                PtoServicioVisible = False

                If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
                    Dim objClientes = Clientes.FindAll(Function(c) c.Maquinas IsNot Nothing)
                    If Modo <> Enumeradores.Modo.Consulta OrElse (objClientes IsNot Nothing AndAlso objClientes.Count > 0) Then
                        MaquinaVisible = True
                    Else
                        Me.ucMaquina.LimparViewState()
                        Me.phMaquina.Controls.Clear()
                    End If
                    For Each objMaquinas In objClientes
                        If (objMaquinas.Maquinas IsNot Nothing AndAlso objMaquinas.Maquinas.Count > 0) Then
                            PtoServicioVisible = True
                            Exit For
                        End If
                    Next
                End If

                If Not PtoServicioVisible Then
                    Me.ucPtoServicio.LimparViewState()
                    Me.phPtoServicio.Controls.Clear()
                End If

                ucMaquina.FocusControle()

            Else

                ucCliente.LimparViewState()
                ClienteFiltro = Nothing
                ClienteJuncao = Nothing
                ClienteOrden = Nothing
                ClienteQueryDefecto = Nothing
                Clientes = New ObservableCollection(Of Cliente)
                Me.ClienteHabilitado = True
                ucCliente = Nothing
                ucCliente.ExibirDados(False)
                If MaquinaVisible Then
                    ucMaquina.LimparViewState()
                    MaquinaVisible = False
                    MaquinaFiltro = Nothing
                    MaquinaJuncao = Nothing
                    MaquinaOrden = Nothing
                    MaquinaQueryDefecto = Nothing
                    ucMaquina = Nothing
                    phMaquina.Controls.Clear()
                End If
                If PtoServicioVisible Then
                    ucPtoServicio.LimparViewState()
                    PtoServicioVisible = False
                    PtoServicioFiltro = Nothing
                    PtoServicioJuncao = Nothing
                    PtoServicioOrden = Nothing
                    PtoServicioQueryDefecto = Nothing
                    ucPtoServicio = Nothing
                    phPtoServicio.Controls.Clear()
                End If

                ucCliente.FocusControle()
            End If

            RaiseEvent UpdatedControl(Me)

            TraduzirControle()
            Inicializar()

        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    Public Sub ucMaquina_OnControleAtualizado() Handles _ucMaquina.UpdatedControl
        Try
            If ucMaquina.RegistrosSelecionados IsNot Nothing AndAlso ucMaquina.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucMaquina.RegistrosSelecionados.DatosRespuesta.Count > 0 _
                AndAlso Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then

                For Each objCliente In Clientes
                    Dim objClienteLocal = objCliente

                    Dim objMaquina As List(Of Helper.Respuesta) = ucMaquina.RegistrosSelecionados.DatosRespuesta.FindAll(Function(x) x.IdentificadorPai = objClienteLocal.Identificador)

                    If objMaquina Is Nothing Then
                        objCliente.Maquinas.Clear()
                    Else

                        If objCliente.Maquinas Is Nothing Then
                            objCliente.Maquinas = New ObservableCollection(Of Maquina)
                        End If
                        For Each s In objCliente.Maquinas.Clonar()
                            Dim sLocal = s
                            Dim aux = objMaquina.Find(Function(x) x.Identificador = sLocal.Identificador)
                            If aux Is Nothing Then
                                objCliente.Maquinas.Remove(objCliente.Maquinas.Find(Function(x) x.Identificador = sLocal.Identificador))
                            End If
                        Next

                        For Each objDatosRespuesta In objMaquina
                            Dim objDatosRespuestaLocal = objDatosRespuesta
                            Dim aux = objCliente.Maquinas.Find(Function(x) x.Identificador = objDatosRespuestaLocal.Identificador)
                            If aux Is Nothing Then
                                objCliente.Maquinas.Add(New Clases.Maquina With {.Identificador = objDatosRespuesta.Identificador,
                                                                                  .Codigo = objDatosRespuesta.Codigo,
                                                                                  .Descripcion = objDatosRespuesta.Descricao})
                            End If
                        Next
                    End If

                Next


                ucPtoServicio.LimparViewState()
                PtoServicioVisible = False

                If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
                    Dim objClientes = Clientes.FindAll(Function(c) c.Maquinas IsNot Nothing AndAlso c.Maquinas.Count > 0)
                    If objClientes IsNot Nothing Then
                        For Each objMaquinas In objClientes
                            If Modo <> Enumeradores.Modo.Consulta OrElse (objMaquinas.Maquinas IsNot Nothing AndAlso objMaquinas.Maquinas.Count > 0) Then
                                PtoServicioVisible = True
                                Exit For
                            End If
                        Next
                    End If
                End If

                If PtoServicioVisible Then
                    ucPtoServicio.ExibirDados(False)
                    ucPtoServicio.FocusControle()
                End If

            Else

                If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
                    For Each objCliente As Clases.Cliente In Clientes
                        If objCliente.Maquinas IsNot Nothing Then
                            objCliente.Maquinas.Clear()
                        End If
                    Next
                End If

                If PtoServicioVisible Then
                    ucPtoServicio.LimparViewState()
                    PtoServicioVisible = False
                    PtoServicioFiltro = Nothing
                    PtoServicioJuncao = Nothing
                    PtoServicioOrden = Nothing
                    PtoServicioQueryDefecto = Nothing
                    ucPtoServicio = Nothing
                    phPtoServicio.Controls.Clear()
                End If

                ucMaquina.FocusControle()
            End If

            RaiseEvent UpdatedControl(Me)
            TraduzirControle()
            Inicializar()

        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    Public Sub ucPtoServicio_OnControleAtualizado() Handles _ucPtoServicio.UpdatedControl
        Try
            If ucPtoServicio.RegistrosSelecionados IsNot Nothing AndAlso ucPtoServicio.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucPtoServicio.RegistrosSelecionados.DatosRespuesta.Count > 0 _
                AndAlso Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then

                For Each objCliente In Clientes

                    If objCliente.Maquinas IsNot Nothing Then
                        For Each objMaquina In objCliente.Maquinas
                            Dim objMaquinaLocal = objMaquina


                            Dim objPtoServicio As List(Of Helper.Respuesta) = ucPtoServicio.RegistrosSelecionados.DatosRespuesta.FindAll(Function(x) x.IdentificadorPai = objMaquinaLocal.Identificador)

                            If objPtoServicio Is Nothing Then
                                objMaquina.PuntosServicio.Clear()
                            Else

                                If objMaquina.PuntosServicio Is Nothing Then
                                    objMaquina.PuntosServicio = New ObservableCollection(Of PuntoServicio)
                                End If
                                For Each s In objMaquina.PuntosServicio.Clonar()
                                    Dim sLocal = s
                                    Dim aux = objPtoServicio.Find(Function(x) x.Identificador = sLocal.Identificador)
                                    If aux Is Nothing Then
                                        objMaquina.PuntosServicio.Remove(objMaquina.PuntosServicio.Find(Function(x) x.Identificador = sLocal.Identificador))
                                    End If
                                Next

                                For Each objDatosRespuesta In objPtoServicio
                                    Dim objDatosRespuestaLocal = objDatosRespuesta
                                    Dim aux = objMaquina.PuntosServicio.Find(Function(x) x.Identificador = objDatosRespuestaLocal.Identificador)
                                    If aux Is Nothing Then
                                        objMaquina.PuntosServicio.Add(New Clases.PuntoServicio With {.Identificador = objDatosRespuesta.Identificador,
                                                                                                        .Codigo = objDatosRespuesta.Codigo,
                                                                                                        .Descripcion = objDatosRespuesta.Descricao})
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next

            Else

                If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
                    For Each objCliente As Clases.Cliente In Clientes
                        If objCliente.Maquinas IsNot Nothing AndAlso objCliente.Maquinas.Count > 0 Then
                            For Each objMaquina As Clases.Maquina In objCliente.Maquinas
                                If objMaquina.PuntosServicio IsNot Nothing Then
                                    objMaquina.PuntosServicio.Clear()
                                End If
                            Next
                        End If
                    Next
                End If

            End If

            RaiseEvent UpdatedControl(Me)
            TraduzirControle()
            Inicializar()
            ucPtoServicio.FocusControle()

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
        If ClienteVisible Then
            If EsBanco Then
                ucCliente.Titulo = Traduzir("021_Banco_Titulo")
                ucCliente.Popup_Titulo = Traduzir("021_Banco_Popup_Titulo")
                ucCliente.Popup_Resultado = Traduzir("021_Banco_Popup_Resultado")
                ucCliente.Popup_Filtro = Traduzir("021_Banco_Popup_Filtro")
            ElseIf EsBancoCapital Then
                ucCliente.Titulo = Traduzir("021_BancoCapital_Titulo")
                ucCliente.Popup_Titulo = Traduzir("021_BancoCapital_Popup_Titulo")
                ucCliente.Popup_Resultado = Traduzir("021_BancoCapital_Popup_Resultado")
                ucCliente.Popup_Filtro = Traduzir("021_BancoCapital_Popup_Filtro")
            Else
                ucCliente.Titulo = Traduzir("021_Cliente_Titulo")
                ucCliente.Popup_Titulo = Traduzir("021_Cliente_Popup_Titulo")
                ucCliente.Popup_Resultado = Traduzir("021_Cliente_Popup_Resultado")
                ucCliente.Popup_Filtro = Traduzir("021_Cliente_Popup_Filtro")
            End If
        End If

        If MaquinaVisible Then
            ucMaquina.Titulo = Traduzir("022_Maquina_Titulo")
            ucMaquina.Popup_Titulo = Traduzir("022_Maquina_Popup_Titulo")
            ucMaquina.Popup_Resultado = Traduzir("022_Maquina_Popup_Resultado")
            ucMaquina.Popup_Filtro = Traduzir("022_Maquina_Popup_Filtro")
        End If

        If PtoServicioVisible Then
            ucPtoServicio.Titulo = Traduzir("023_PtoServicio_Titulo")
            ucPtoServicio.Popup_Titulo = Traduzir("023_PtoServicio_Popup_Titulo")
            ucPtoServicio.Popup_Resultado = Traduzir("023_PtoServicio_Popup_Resultado")
            ucPtoServicio.Popup_Filtro = Traduzir("023_PtoServicio_Popup_Filtro")
        End If
    End Sub

    ''' <summary>
    ''' Carrega Cliente e Maquina utilizando os parâmetros definidos pelo usuário.
    ''' </summary>
    Protected Sub ConfigurarControles()

        TraduzirControle()

        If ClienteVisible Then
            ConfigurarControle_Cliente()
        End If

        If MaquinaVisible Then
            ConfigurarControle_Maquina()
        End If

        If PtoServicioVisible Then
            ConfigurarControle_PtoServicio()
        End If

    End Sub

    Protected Sub ConfigurarControle_Cliente()

        Me.ucCliente.FiltroConsulta = Me.ClienteFiltro
        Me.ucCliente.OrdenacaoConsulta = Me.ClienteOrden
        Me.ucCliente.JoinConsulta = Me.ClienteJuncao
        Me.ucCliente.MaxRegistroPorPagina = MaxRegistroPorPagina
        Me.ucCliente.Tabela = New Tabela With {.Tabela = TabelaHelper.Cliente}
        Me.ucCliente.MultiSelecao = Me.SelecaoMultipla
        Me.ucCliente.ControleHabilitado = Me.ClienteHabilitado
        Me.ucCliente.QueryDefault = Me.ClienteQueryDefecto
        Me.ucCliente.HeightPopUp = 600
        ucCliente.ucPopup.Height = 560

        If TotalizadorSaldo Then
            Dim tabela = New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Cliente}
            If Me.ucCliente.FiltroConsulta.ContainsKey(tabela) Then
                Me.ucCliente.FiltroConsulta(tabela).Add(New ArgumentosFiltro("BOL_TOTALIZADOR_SALDO", "1"))
            Else
                Me.ucCliente.FiltroConsulta.Add(New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Cliente},
                                            New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("BOL_TOTALIZADOR_SALDO", "1")})
            End If

        End If

        If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then


            Dim dadosCliente As New Comon.RespuestaHelper
            dadosCliente.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

            For Each c As Clases.Cliente In Clientes
                If Not String.IsNullOrEmpty(c.Identificador) Then
                    Dim DadosExibir As New Comon.Helper.Respuesta
                    With DadosExibir
                        .IdentificadorPai = Nothing
                        .Identificador = c.Identificador
                        .Codigo = c.Codigo
                        .Descricao = c.Descripcion
                    End With
                    dadosCliente.DatosRespuesta.Add(DadosExibir)
                End If
            Next
            ucCliente.RegistrosSelecionados = dadosCliente
            ucCliente.ExibirDados(False)

        Else
            Me.MaquinaVisible = False
            Me.PtoServicioVisible = False
        End If

    End Sub

    Protected Sub ConfigurarControle_Maquina()

        If MaquinaVisible AndAlso Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then

            Me.ucMaquina.FiltroConsulta = Me.MaquinaFiltro
            Me.ucMaquina.OrdenacaoConsulta = Me.MaquinaOrden
            Me.ucMaquina.JoinConsulta = Me.MaquinaJuncao
            Me.ucMaquina.MaxRegistroPorPagina = MaxRegistroPorPagina
            Me.ucMaquina.Tabela = New Tabela With {.Tabela = TabelaHelper.MaquinaPunto}

            If Not Me.ucMaquina.MultiSelecao Then
                Me.ucMaquina.MultiSelecao = Me.SelecaoMultipla
            End If

            Me.ucMaquina.ControleHabilitado = Me.MaquinaHabilitado
            Me.ucMaquina.QueryDefault = Me.MaquinaQueryDefecto

            Dim objClientes = Clientes.FindAll(Function(x) x.Maquinas IsNot Nothing)

            If objClientes IsNot Nothing AndAlso objClientes.Count > 0 Then
                Dim dadosMaquina As New Comon.RespuestaHelper
                dadosMaquina.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

                For Each objCliente As Clases.Cliente In Clientes.FindAll(Function(x) x.Maquinas IsNot Nothing AndAlso x.Maquinas.Count > 0)

                    For Each objMaquina As Clases.Maquina In objCliente.Maquinas

                        Dim DadosExibir As New Comon.Helper.Respuesta
                        With DadosExibir
                            .IdentificadorPai = objCliente.Identificador
                            .Identificador = objMaquina.Identificador
                            .Codigo = objMaquina.Codigo
                            .Descricao = objMaquina.Descripcion
                        End With

                        dadosMaquina.DatosRespuesta.Add(DadosExibir)

                    Next
                Next

                ucMaquina.RegistrosSelecionados = dadosMaquina
                ucMaquina.ExibirDados(False)

            Else
                ucMaquina.ExibirDados(False)
                Me.PtoServicioVisible = False
            End If

        Else
            phMaquina.Controls.Clear()
        End If

    End Sub

    Protected Sub ConfigurarControle_PtoServicio()

        If PtoServicioVisible AndAlso Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then

            Dim objClientes = Clientes.FindAll(Function(x) x.Maquinas IsNot Nothing)
            If objClientes IsNot Nothing AndAlso objClientes.Count > 0 Then

                Me.ucPtoServicio.FiltroConsulta = Me.PtoServicioFiltro
                Me.ucPtoServicio.OrdenacaoConsulta = Me.PtoServicioOrden
                Me.ucPtoServicio.JoinConsulta = Me.PtoServicioJuncao
                Me.ucPtoServicio.MaxRegistroPorPagina = MaxRegistroPorPagina
                Me.ucPtoServicio.Tabela = New Tabela With {.Tabela = TabelaHelper.PuntoServicioMaquina}
                If Not Me.ucPtoServicio.MultiSelecao Then
                    Me.ucPtoServicio.MultiSelecao = Me.SelecaoMultipla
                End If

                Me.ucPtoServicio.ControleHabilitado = Me.PtoServicioHabilitado
                Me.ucPtoServicio.QueryDefault = Me.PtoServicioQueryDefecto

                Dim dadosPtoServicio As New Comon.RespuestaHelper
                dadosPtoServicio.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

                For Each objCliente As Clases.Cliente In Clientes.FindAll(Function(x) x.Maquinas IsNot Nothing)
                    Dim objMaquinas = objCliente.Maquinas.FindAll(Function(x) x.PuntosServicio IsNot Nothing)
                    If objMaquinas IsNot Nothing Then
                        For Each objMaquina As Clases.Maquina In objMaquinas
                            If objMaquina.PuntosServicio IsNot Nothing AndAlso objMaquina.PuntosServicio.Count > 0 Then
                                For Each objPtoServicio As Clases.PuntoServicio In objMaquina.PuntosServicio
                                    Dim DadosExibir As New Comon.Helper.Respuesta
                                    With DadosExibir
                                        .IdentificadorPai = objMaquina.Identificador
                                        .Identificador = objPtoServicio.Identificador
                                        .Codigo = objPtoServicio.Codigo
                                        .Descricao = objPtoServicio.Descripcion
                                    End With
                                    dadosPtoServicio.DatosRespuesta.Add(DadosExibir)
                                Next
                            End If
                        Next
                    End If
                Next

                ucPtoServicio.RegistrosSelecionados = dadosPtoServicio
                ucPtoServicio.ExibirDados(False)

            End If
        Else
            phPtoServicio.Controls.Clear()
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