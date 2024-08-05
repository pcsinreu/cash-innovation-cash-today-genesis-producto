Imports System.Web.UI
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones

Public Class ucCanal
    Inherits UcBase

#Region "[PROPRIEDADES]"

    Public Property Canales As ObservableCollection(Of Canal)
        Get
            If Session(ID & "_Canales") Is Nothing Then
                Session(ID & "_Canales") = New ObservableCollection(Of Canal)
            End If
            Return Session(ID & "_Canales")
        End Get
        Set(value As ObservableCollection(Of Canal))
            Session(ID & "_Canales") = value
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

#Region "[ucCanal]"

    Private WithEvents _ucCanal As ucHelperBusquedaDatos
    Public Property ucCanal() As ucHelperBusquedaDatos
        Get
            If _ucCanal Is Nothing Then
                _ucCanal = LoadControl("~\Controles\Helpers\ucHelperBusquedaDatos.ascx")
                _ucCanal.ID = "ucCanal"
                AddHandler _ucCanal.Erro, AddressOf ErroControles
                If phCanal.Controls.Count = 0 Then
                    phCanal.Controls.Add(_ucCanal)
                End If
            End If
            Return _ucCanal
        End Get
        Set(value As ucHelperBusquedaDatos)
            _ucCanal = value
        End Set
    End Property

    ''' <summary>
    ''' Define o Filtro a ser incluído na pesquisa de Canais.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _CanalFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property CanalFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        Get
            If _CanalFiltro Is Nothing Then
                ' Seta valor defector
                _CanalFiltro = New Dictionary(Of UtilHelper.Tabela, List(Of ArgumentosFiltro)) From
                {
                    {
                        New UtilHelper.Tabela With {.Tabela = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.CanalPorCodigo},
                        New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("BOL_VIGENTE", "1")}
                    }
                }
            End If
            Return _CanalFiltro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _CanalFiltro = value
        End Set
    End Property

    ''' <summary>
    ''' Define as junções a serem incluídas para a pesquisa de Canal.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>    
    Private _CanalJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
    Public Property CanalJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
        Get
            Return _CanalJuncao
        End Get
        Set(value As Dictionary(Of String, UtilHelper.JoinSQL))
            _CanalJuncao = value
        End Set
    End Property

    ''' <summary>
    ''' Define como os dados retornados da consulta de Canais deverão ser ordenados.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _CanalOrden As Dictionary(Of String, UtilHelper.OrderSQL)
    Public Property CanalOrden As Dictionary(Of String, UtilHelper.OrderSQL)
        Get
            If _CanalOrden Is Nothing Then
                ' Seta valor defector
                _CanalOrden = New Dictionary(Of String, OrderSQL) From {{"COD_CANAL", New OrderSQL("COD_CANAL")}}
            End If
            Return _CanalOrden
        End Get
        Set(value As Dictionary(Of String, UtilHelper.OrderSQL))
            _CanalOrden = value
        End Set
    End Property

    Public Property CanalHabilitado As Boolean = False
    Public Property CanalQueryDefecto As UtilHelper.QueryHelperControl = Nothing
    Public Property NoExhibirCanal As Boolean? = Nothing

    Private _CanalVisible As Boolean = True
    Private Property CanalVisible As Boolean
        Get
            If NoExhibirCanal IsNot Nothing Then
                Return Not NoExhibirCanal
            Else
                Return _CanalVisible
            End If
        End Get
        Set(value As Boolean)
            _CanalVisible = value
        End Set
    End Property

#End Region

#Region "[ucSubCanal]"


    Private WithEvents _ucSubCanal As ucHelperBusquedaDatos
    Public Property ucSubCanal() As ucHelperBusquedaDatos
        Get
            If _ucSubCanal Is Nothing Then
                _ucSubCanal = LoadControl("~\Controles\Helpers\ucHelperBusquedaDatos.ascx")
                _ucSubCanal.ID = "ucSubCanal"
                AddHandler _ucSubCanal.Erro, AddressOf ErroControles
                phSubCanal.Controls.Add(_ucSubCanal)
            End If
            Return _ucSubCanal
        End Get
        Set(value As ucHelperBusquedaDatos)
            _ucSubCanal = value
        End Set
    End Property

    ''' <summary>
    ''' Define o Filtro a ser incluído na pesquisa de SubCanais.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _SubCanalFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property SubCanalFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        Get
            If _SubCanalFiltro Is Nothing Then
                ' Seta valor defector
                _SubCanalFiltro = New Dictionary(Of UtilHelper.Tabela, List(Of ArgumentosFiltro)) From
                {
                    {
                        New UtilHelper.Tabela With {.Tabela = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.SubCanalPorCodigo},
                        New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("BOL_VIGENTE", "1")}
                    }
                }
            End If
            Return _SubCanalFiltro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _SubCanalFiltro = value
        End Set
    End Property

    ''' <summary>
    ''' Define as junções a serem incluídas para a pesquisa de SubCanal.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>    
    Private _SubCanalJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
    Public Property SubCanalJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
        Get
            _SubCanalJuncao = New Dictionary(Of String, UtilHelper.JoinSQL)

            If Canales IsNot Nothing AndAlso Canales.Count > 0 Then

                Dim objValorCampoChave As String = ""
                For Each c As Clases.Canal In Canales
                    If Not String.IsNullOrEmpty(c.Identificador) Then
                        objValorCampoChave &= "," & c.Identificador
                    End If
                Next

                If Not String.IsNullOrEmpty(objValorCampoChave) AndAlso Not String.IsNullOrEmpty(objValorCampoChave.Substring(1)) Then
                    joinSQL = New UtilHelper.JoinSQL With {.TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = TabelaHelper.CanalPorCodigo},
                                                   .CampoComumTabEsq = "OID_CANAL",
                                                   .TabelaDireita = New UtilHelper.Tabela With {.Tabela = TabelaHelper.SubCanalPorCodigo},
                                                   .CampoComumTabDireita = "OID_CANAL",
                                                   .NomeCampoChave = "OID_CANAL",
                                                   .ValorCampoChave = objValorCampoChave.Substring(1)
                                                  }
                    _SubCanalJuncao.Add(("Join_" + joinSQL.TabelaEsquerda.Tabela.ToString()), joinSQL)
                End If

            End If

            Return _SubCanalJuncao
        End Get
        Set(value As Dictionary(Of String, UtilHelper.JoinSQL))
            _SubCanalJuncao = value
        End Set
    End Property

    ''' <summary>
    ''' Define como os dados retornados da consulta de SubCanais deverão ser ordenados.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _SubCanalOrden As Dictionary(Of String, UtilHelper.OrderSQL)
    Public Property SubCanalOrden As Dictionary(Of String, UtilHelper.OrderSQL)
        Get
            If _SubCanalOrden Is Nothing Then
                ' Seta valor defector
                _SubCanalOrden = New Dictionary(Of String, OrderSQL) From {{"COD_SUBCANAL", New OrderSQL("COD_SUBCANAL")}}
            End If
            Return _SubCanalOrden
        End Get
        Set(value As Dictionary(Of String, UtilHelper.OrderSQL))
            _SubCanalOrden = value
        End Set
    End Property

    Public Property SubCanalHabilitado As Boolean = False
    Public Property SubCanalQueryDefecto As UtilHelper.QueryHelperControl = Nothing
    Public Property NoExhibirSubCanal As Boolean? = Nothing

    Private _SubCanalVisible As Boolean = False
    Private Property SubCanalVisible As Boolean
        Get
            If NoExhibirSubCanal IsNot Nothing Then
                Return Not NoExhibirSubCanal
            Else
                Return _SubCanalVisible
            End If
        End Get
        Set(value As Boolean)
            _SubCanalVisible = value
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

            If Canales IsNot Nothing AndAlso Canales.Count > 0 Then
                Dim objCanales = Canales.FindAll(Function(c) c.SubCanales IsNot Nothing AndAlso c.SubCanales.Count > 0)
                If Modo <> Enumeradores.Modo.Consulta OrElse (objCanales IsNot Nothing AndAlso objCanales.Count > 0) Then
                    SubCanalVisible = True
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
        ucCanal.Focus()
    End Sub

#End Region

#Region "[EVENTOS]"

    Public Sub ucCanal_OnControleAtualizado() Handles _ucCanal.UpdatedControl
        Try
            If ucCanal.RegistrosSelecionados IsNot Nothing AndAlso ucCanal.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucCanal.RegistrosSelecionados.DatosRespuesta.Count > 0 Then
                'Verifica se foi selecionado apenas um canal
                Dim identificadorCanal As String = String.Empty

                If Canales Is Nothing OrElse Canales.Count = 0 Then
                    'Verifica se foi selecionado apenas um canal
                    If ucCanal.RegistrosSelecionados.DatosRespuesta.Count = 1 Then
                        identificadorCanal = ucCanal.RegistrosSelecionados.DatosRespuesta(0).Identificador
                    End If

                    For Each objDatosRespuesta In ucCanal.RegistrosSelecionados.DatosRespuesta



                        Canales.Add(New Clases.Canal With {.Identificador = objDatosRespuesta.Identificador,
                                                      .Codigo = objDatosRespuesta.Codigo,
                                                      .Descripcion = objDatosRespuesta.Descricao,
                                                      .SubCanales = Nothing})

                    Next

                ElseIf Canales IsNot Nothing Then

                    'Verifica se foi selecionado apenas um canal
                    If ucCanal.RegistrosSelecionados.DatosRespuesta.Count = 1 Then
                        identificadorCanal = ucCanal.RegistrosSelecionados.DatosRespuesta(0).Identificador
                    Else
                        'Verifica se foi selecionado apenas uma canal, verificando os canais selecionados anteriormente e os canais atuais..
                        'se por exemplo existiam 3 canais selecionados e agora foi retornado 4, então foi selecionado apenas um canal.
                        If ucCanal.RegistrosSelecionados.DatosRespuesta.Count - Canales.Count = 1 Then
                            'descobrir qual o novo canal que foi selecionado
                            Dim canal = ucCanal.RegistrosSelecionados.DatosRespuesta.Where(Function(c) Not Canales.Exists(Function(old) old.Identificador = c.Identificador)).FirstOrDefault
                            If canal IsNot Nothing Then
                                identificadorCanal = canal.Identificador
                            End If
                        End If
                    End If

                    For Each objCanal As Clases.Canal In Canales.Clonar()
                        Dim objCanalLocal = objCanal
                        Dim aux = ucCanal.RegistrosSelecionados.DatosRespuesta.Find(Function(x) x.Identificador = objCanalLocal.Identificador)
                        If aux Is Nothing Then
                            Canales.RemoveAll(Function(c) c.Identificador = objCanalLocal.Identificador)
                        End If
                    Next

                    For Each objDatosRespuesta In ucCanal.RegistrosSelecionados.DatosRespuesta
                        Dim objDatosRespuestaLocal = objDatosRespuesta
                        Dim aux = Canales.Find(Function(x) x.Identificador = objDatosRespuestaLocal.Identificador)
                        If aux Is Nothing Then
                            Canales.Add(New Clases.Canal With {.Identificador = objDatosRespuesta.Identificador,
                                                                .Codigo = objDatosRespuesta.Codigo,
                                                                .Descripcion = objDatosRespuesta.Descricao})
                        End If
                    Next

                End If

                RaiseEvent UpdatedControl(Me)

                'If Not String.IsNullOrEmpty(identificadorCanal) Then
                '    BuscarSubCanalPorCanal(identificadorCanal)
                'End If

                If Canales IsNot Nothing AndAlso Canales.Count > 0 Then
                    Dim objCanales = Canales.FindAll(Function(c) c.SubCanales IsNot Nothing AndAlso c.SubCanales.Count > 0)
                    If Modo <> Enumeradores.Modo.Consulta OrElse (objCanales IsNot Nothing AndAlso objCanales.Count > 0) Then
                        SubCanalVisible = True
                    End If
                End If
            Else

                ucCanal.LimparViewState()
                CanalFiltro = Nothing
                CanalJuncao = Nothing
                CanalOrden = Nothing
                CanalQueryDefecto = Nothing
                Canales = New ObservableCollection(Of Canal)
                'Me.CanalHabilitado = True
                ucCanal = Nothing
                ucCanal.ExibirDados(False)
                If SubCanalVisible Then
                    ucSubCanal.LimparViewState()
                    SubCanalVisible = False
                    SubCanalFiltro = Nothing
                    SubCanalJuncao = Nothing
                    SubCanalOrden = Nothing
                    SubCanalQueryDefecto = Nothing
                    ucSubCanal = Nothing
                    phSubCanal.Controls.Clear()
                End If

                ucCanal.FocusControle()

                RaiseEvent UpdatedControl(Me)
            End If

            TraduzirControle()
            Inicializar()

        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    Public Sub ucSubCanal_OnControleAtualizado() Handles _ucSubCanal.UpdatedControl
        Try
            If ucSubCanal.RegistrosSelecionados IsNot Nothing AndAlso ucSubCanal.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucSubCanal.RegistrosSelecionados.DatosRespuesta.Count > 0 _
                AndAlso Canales IsNot Nothing AndAlso Canales.Count > 0 Then

                For Each objCanal In Canales
                    Dim objCanalLocal = objCanal

                    Dim objSubCanal As List(Of Prosegur.Genesis.Comon.Helper.Respuesta) = ucSubCanal.RegistrosSelecionados.DatosRespuesta.FindAll(Function(x) x.IdentificadorPai = objCanalLocal.Identificador)

                    If objSubCanal Is Nothing Then
                        objCanal.SubCanales.Clear()
                    Else

                        If objCanal.SubCanales Is Nothing Then
                            objCanal.SubCanales = New ObservableCollection(Of SubCanal)
                        End If
                        For Each s In objCanal.SubCanales.Clonar()
                            Dim sLocal = s
                            Dim aux = objSubCanal.Find(Function(x) x.Identificador = sLocal.Identificador)
                            If aux Is Nothing Then
                                objCanal.SubCanales.Remove(objCanal.SubCanales.Find(Function(x) x.Identificador = sLocal.Identificador))
                            End If
                        Next

                        For Each objDatosRespuesta In objSubCanal
                            Dim objDatosRespuestaLocal = objDatosRespuesta
                            Dim aux = objCanal.SubCanales.Find(Function(x) x.Identificador = objDatosRespuestaLocal.Identificador)
                            If aux Is Nothing Then
                                objCanal.SubCanales.Add(New Clases.SubCanal With {.Identificador = objDatosRespuesta.Identificador,
                                                                                  .Codigo = objDatosRespuesta.Codigo,
                                                                                  .Descripcion = objDatosRespuesta.Descricao})
                            End If
                        Next
                    End If

                Next

            Else

                If Canales IsNot Nothing AndAlso Canales.Count > 0 Then
                    For Each objCanal As Clases.Canal In Canales.Where(Function(c) c.SubCanales IsNot Nothing)
                        objCanal.SubCanales.Clear()
                    Next
                End If

            End If

            ucSubCanal.FocusControle()
            RaiseEvent UpdatedControl(Me)

            TraduzirControle()
            Inicializar()

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
        If CanalVisible Then
            ucCanal.Titulo = Traduzir("024_Canal_Titulo")
            ucCanal.Popup_Titulo = Traduzir("024_Canal_Popup_Titulo")
            ucCanal.Popup_Resultado = Traduzir("024_Canal_Popup_Resultado")
            ucCanal.Popup_Filtro = Traduzir("024_Canal_Popup_Filtro")
        End If

        If SubCanalVisible Then
            ucSubCanal.Titulo = Traduzir("025_SubCanal_Titulo")
            ucSubCanal.Popup_Titulo = Traduzir("025_SubCanal_Popup_Titulo")
            ucSubCanal.Popup_Resultado = Traduzir("025_SubCanal_Popup_Resultado")
            ucSubCanal.Popup_Filtro = Traduzir("025_SubCanal_Popup_Filtro")
        End If
    End Sub

    ''' <summary>
    ''' Carrega Canal e SubCanal utilizando os parâmetros definidos pelo usuário.
    ''' </summary>
    Protected Sub ConfigurarControles()

        TraduzirControle()

        If CanalVisible Then
            ConfigurarControle_Canal()
        End If

        If SubCanalVisible Then
            ConfigurarControle_SubCanal()
        End If

    End Sub

    Protected Sub ConfigurarControle_Canal()

        Me.ucCanal.FiltroConsulta = Me.CanalFiltro
        Me.ucCanal.OrdenacaoConsulta = Me.CanalOrden
        Me.ucCanal.JoinConsulta = Me.CanalJuncao
        Me.ucCanal.MaxRegistroPorPagina = MaxRegistroPorPagina
        Me.ucCanal.Tabela = New Tabela With {.Tabela = TabelaHelper.CanalPorCodigo}
        Me.ucCanal.MultiSelecao = Me.SelecaoMultipla
        Me.ucCanal.ControleHabilitado = Me.CanalHabilitado
        Me.ucCanal.QueryDefault = Me.CanalQueryDefecto

        If Canales IsNot Nothing AndAlso Canales.Count > 0 Then

            Dim dadosCanal As New Comon.RespuestaHelper
            dadosCanal.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

            For Each c As Clases.Canal In Canales
                If Not String.IsNullOrEmpty(c.Identificador) Then
                    Dim DadosExibir As New Comon.Helper.Respuesta
                    With DadosExibir
                        .IdentificadorPai = Nothing
                        .Identificador = c.Identificador
                        .Codigo = c.Codigo
                        .Descricao = c.Descripcion
                    End With
                    dadosCanal.DatosRespuesta.Add(DadosExibir)
                End If
            Next
            ucCanal.RegistrosSelecionados = dadosCanal
            'Me.CanalHabilitado = (Modo <> Enumeradores.Modo.Consulta)
            ucCanal.ExibirDados(False)
        Else
            'Me.CanalHabilitado = True
            Me.SubCanalVisible = False
        End If



    End Sub

    Protected Sub ConfigurarControle_SubCanal()

        Me.ucSubCanal.FiltroConsulta = Me.SubCanalFiltro
        Me.ucSubCanal.OrdenacaoConsulta = Me.SubCanalOrden
        Me.ucSubCanal.JoinConsulta = Me.SubCanalJuncao
        Me.ucSubCanal.MaxRegistroPorPagina = MaxRegistroPorPagina
        Me.ucSubCanal.Tabela = New Tabela With {.Tabela = TabelaHelper.SubCanalPorCodigo}
        Me.ucSubCanal.MultiSelecao = Me.SelecaoMultipla
        Me.ucSubCanal.ControleHabilitado = Me.SubCanalHabilitado
        Me.ucSubCanal.QueryDefault = Me.SubCanalQueryDefecto

        If Canales IsNot Nothing AndAlso Canales.Count > 0 Then

            Dim dadosSubCanal As New Comon.RespuestaHelper
            dadosSubCanal.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

            For Each c As Clases.Canal In Canales.FindAll(Function(x) Not String.IsNullOrEmpty(x.Identificador))

                If c.SubCanales IsNot Nothing AndAlso c.SubCanales.Count > 0 Then

                    For Each sc As Clases.SubCanal In c.SubCanales

                        If Not String.IsNullOrEmpty(sc.Identificador) Then
                            Dim DadosExibir As New Comon.Helper.Respuesta
                            With DadosExibir
                                .IdentificadorPai = c.Identificador
                                .Identificador = sc.Identificador
                                .Codigo = sc.Codigo
                                .Descricao = sc.Descripcion
                            End With
                            dadosSubCanal.DatosRespuesta.Add(DadosExibir)
                        End If

                    Next

                End If

            Next

            ucSubCanal.RegistrosSelecionados = dadosSubCanal
            'Me.SubCanalHabilitado = (Modo <> Enumeradores.Modo.Consulta)
            ucSubCanal.ExibirDados(False)

        Else
            'Me.SubCanalHabilitado = True
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

    Private Sub BuscarSubCanalPorCanal(identificadorCanal As String)
        Dim objRegistrosSelecionados As New RespuestaHelper

        ' Prepara o objeto Petición
        Dim objPeticion As New PeticionHelper
        objPeticion.Tabela = New Tabela With {.Tabela = TabelaHelper.SubCanalPorCodigo}
        objPeticion.Query = Me.SubCanalQueryDefecto

        joinSQL = New UtilHelper.JoinSQL With {.TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = TabelaHelper.CanalPorCodigo},
                                   .CampoComumTabEsq = "OID_CANAL",
                                   .TabelaDireita = New UtilHelper.Tabela With {.Tabela = TabelaHelper.SubCanalPorCodigo},
                                   .CampoComumTabDireita = "OID_CANAL",
                                   .NomeCampoChave = "OID_CANAL",
                                   .ValorCampoChave = identificadorCanal
                                  }
        objPeticion.JuncaoSQL = New SerializableDictionary(Of String, JoinSQL)
        objPeticion.JuncaoSQL.Add(("Join_" + joinSQL.TabelaEsquerda.Tabela.ToString()), joinSQL)
        objPeticion.ParametrosPaginacion = New Genesis.Comon.Paginacion.ParametrosPeticionPaginacion()
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objPeticion.ParametrosPaginacion.IndicePagina = 0
        objPeticion.ParametrosPaginacion.RegistrosPorPagina = Me.MaxRegistroPorPagina

        'Filtro para recuperar somente registro ativos
        objPeticion.FiltroSQL = New SerializableDictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        objPeticion.FiltroSQL.Add(New UtilHelper.Tabela With {.Tabela = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.SubCanalPorCodigo}, New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("BOL_VIGENTE", "1")})

        ' Busca Resultado
        objRegistrosSelecionados = Prosegur.Genesis.LogicaNegocio.Classes.Helper.Busqueda(objPeticion)

        Dim datosRespuestaSubCanais As New List(Of Prosegur.Genesis.Comon.Helper.Respuesta)
        Dim objSubCanaisSelecionados As RespuestaHelper = Nothing

        'If ucSubCanal.RegistrosSelecionados IsNot Nothing AndAlso ucSubCanal.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucSubCanal.RegistrosSelecionados.DatosRespuesta.Count > 0 Then
        '    objSubCanaisSelecionados = ucSubCanal.RegistrosSelecionados.Clonar

        '    For Each objSubCanal As Prosegur.Genesis.Comon.Helper.Respuesta In ucSubCanal.RegistrosSelecionados.DatosRespuesta
        '        If Canales.Exists(Function(c) c.Identificador = objSubCanal.IdentificadorPai) Then
        '            datosRespuestaSubCanais.Add(objSubCanal.Clonar)
        '        End If
        '    Next
        'End If

        'se retorno apenas um subcanal então insere na lista
        If objRegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso objRegistrosSelecionados.DatosRespuesta.Count = 1 Then
            If objSubCanaisSelecionados Is Nothing Then
                objSubCanaisSelecionados = New RespuestaHelper
                objSubCanaisSelecionados.DatosRespuesta = New List(Of Prosegur.Genesis.Comon.Helper.Respuesta)
            End If

            objSubCanaisSelecionados.DatosRespuesta.Add(objRegistrosSelecionados.DatosRespuesta(0).Clonar)
        End If

        If objSubCanaisSelecionados IsNot Nothing Then
            ucSubCanal.RegistrosSelecionados = objSubCanaisSelecionados
            ucSubCanal.ControleHabilitado = False
            ucSubCanal.ExibirDados()
            ucCanal.FocusControle()
        Else
            ucSubCanal.ExibirDados(False)
            ucSubCanal.FocusControle()
        End If
    End Sub
#End Region


End Class