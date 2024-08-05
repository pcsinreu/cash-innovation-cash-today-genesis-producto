Imports System.Web.UI
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones

Public Class ucTipoContenedor
    Inherits UcBase

#Region "[PROPRIEDADES]"

    Public Property TiposContenedores As ObservableCollection(Of TipoContenedor)
        Get
            If ViewState(ID & "_TiposContenedores") Is Nothing Then
                ViewState(ID & "_TiposContenedores") = New ObservableCollection(Of TipoContenedor)
            End If
            Return ViewState(ID & "_TiposContenedores")
        End Get
        Set(value As ObservableCollection(Of TipoContenedor))
            ViewState(ID & "_TiposContenedores") = value
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
    Public Event UpdatedControl(sender As ControleEventArgs)

    Private joinSQL As UtilHelper.JoinSQL

#Region "[ucTipoContenedor]"

    Private WithEvents _ucTipoContenedor As ucHelperBusquedaDatos
    Public Property ucTipoContenedor() As ucHelperBusquedaDatos
        Get
            If _ucTipoContenedor Is Nothing Then
                _ucTipoContenedor = LoadControl("~\Controles\ucHelperBusquedaDatos.ascx")
                _ucTipoContenedor.ID = "ucTipoContenedor"
                AddHandler _ucTipoContenedor.Erro, AddressOf ErroControles
                If phTipoContenedor.Controls.Count = 0 Then
                    phTipoContenedor.Controls.Add(_ucTipoContenedor)
                End If
            End If
            Return _ucTipoContenedor
        End Get
        Set(value As ucHelperBusquedaDatos)
            _ucTipoContenedor = value
        End Set
    End Property

    ''' <summary>
    ''' Define o Filtro a ser incluído na pesquisa de Canais.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _TipoContenedorFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property TipoContenedorFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        Get
            If _TipoContenedorFiltro Is Nothing Then
                ' Seta valor defector
                _TipoContenedorFiltro = New Dictionary(Of UtilHelper.Tabela, List(Of ArgumentosFiltro)) From
                {
                    {
                        New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.TipoContenedor}, _
                        New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("BOL_ACTIVO", "1")}
                    }
                }
            End If
            Return _TipoContenedorFiltro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _TipoContenedorFiltro = value
        End Set
    End Property


    ''' <summary>
    ''' Define como os dados retornados da consulta de tipo contenedores deverão ser ordenados.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _TipoContenedorOrden As Dictionary(Of String, UtilHelper.OrderSQL)
    Public Property TipoContenedorOrden As Dictionary(Of String, UtilHelper.OrderSQL)
        Get
            If _TipoContenedorOrden Is Nothing Then
                ' Seta valor defector
                _TipoContenedorOrden = New Dictionary(Of String, OrderSQL) From {{"COD_TIPO_CONTENEDOR", New OrderSQL("COD_TIPO_CONTENEDOR")}}
            End If
            Return _TipoContenedorOrden
        End Get
        Set(value As Dictionary(Of String, UtilHelper.OrderSQL))
            _TipoContenedorOrden = value
        End Set
    End Property

    Public Property TipoContenedorHabilitado As Boolean = False
    Public Property TipoContenedorQueryDefecto As UtilHelper.QueryHelperControl = Nothing
    Public Property NoExhibirTipoContenedor As Boolean? = Nothing

    Private _TipoContenedorVisible As Boolean = True
    Private Property TipoContenedorVisible As Boolean
        Get
            If NoExhibirTipoContenedor IsNot Nothing Then
                Return Not NoExhibirTipoContenedor
            Else
                Return _TipoContenedorVisible
            End If
        End Get
        Set(value As Boolean)
            _TipoContenedorVisible = value
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
        ucTipoContenedor.Focus()
    End Sub

#End Region

#Region "[EVENTOS]"

    Public Sub ucTipoContenedor_OnControleAtualizado() Handles _ucTipoContenedor.UpdatedControl
        Try
            If ucTipoContenedor.RegistrosSelecionados IsNot Nothing AndAlso ucTipoContenedor.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucTipoContenedor.RegistrosSelecionados.DatosRespuesta.Count > 0 Then
                'Verifica se foi selecionado apenas um TipoContenedor
                Dim identificadorTipoContenedor As String = String.Empty

                If TiposContenedores Is Nothing OrElse TiposContenedores.Count = 0 Then
                    'Verifica se foi selecionado apenas um TipoContenedor
                    If ucTipoContenedor.RegistrosSelecionados.DatosRespuesta.Count = 1 Then
                        identificadorTipoContenedor = ucTipoContenedor.RegistrosSelecionados.DatosRespuesta(0).Identificador
                    End If

                    For Each objDatosRespuesta In ucTipoContenedor.RegistrosSelecionados.DatosRespuesta



                        TiposContenedores.Add(New Clases.TipoContenedor With {.Identificador = objDatosRespuesta.Identificador, _
                                                      .Codigo = objDatosRespuesta.Codigo, _
                                                      .Descripcion = objDatosRespuesta.Descricao})

                    Next

                ElseIf TiposContenedores IsNot Nothing Then

                    'Verifica se foi selecionado apenas um TipoContenedor
                    If ucTipoContenedor.RegistrosSelecionados.DatosRespuesta.Count = 1 Then
                        identificadorTipoContenedor = ucTipoContenedor.RegistrosSelecionados.DatosRespuesta(0).Identificador
                    Else
                        'Verifica se foi selecionado apenas uma TipoContenedor, verificando os canais selecionados anteriormente e os canais atuais..
                        'se por exemplo existiam 3 canais selecionados e agora foi retornado 4, então foi selecionado apenas um TipoContenedor.
                        If ucTipoContenedor.RegistrosSelecionados.DatosRespuesta.Count - TiposContenedores.Count = 1 Then
                            'descobrir qual o novo TipoContenedor que foi selecionado
                            Dim TipoContenedor = ucTipoContenedor.RegistrosSelecionados.DatosRespuesta.Where(Function(c) Not TiposContenedores.Exists(Function(old) old.Identificador = c.Identificador)).FirstOrDefault
                            If TipoContenedor IsNot Nothing Then
                                identificadorTipoContenedor = TipoContenedor.Identificador
                            End If
                        End If
                    End If

                    For Each objCanal As Clases.TipoContenedor In TiposContenedores.Clonar()
                        Dim objCanalLocal = objCanal
                        Dim aux = ucTipoContenedor.RegistrosSelecionados.DatosRespuesta.Find(Function(x) x.Identificador = objCanalLocal.Identificador)
                        If aux Is Nothing Then
                            TiposContenedores.RemoveAll(Function(c) c.Identificador = objCanalLocal.Identificador)
                        End If
                    Next

                    For Each objDatosRespuesta In ucTipoContenedor.RegistrosSelecionados.DatosRespuesta
                        Dim objDatosRespuestaLocal = objDatosRespuesta
                        Dim aux = TiposContenedores.Find(Function(x) x.Identificador = objDatosRespuestaLocal.Identificador)
                        If aux Is Nothing Then
                            TiposContenedores.Add(New Clases.TipoContenedor With {.Identificador = objDatosRespuesta.Identificador, _
                                                                .Codigo = objDatosRespuesta.Codigo, _
                                                                .Descripcion = objDatosRespuesta.Descricao})
                        End If
                    Next

                End If

                RaiseEvent UpdatedControl(New ControleEventArgs With {.Controle = "TipoContenedor"})

                'If Not String.IsNullOrEmpty(identificadorCanal) Then
                '    BuscarSubCanalPorCanal(identificadorCanal)
                'End If

            Else

                ucTipoContenedor.LimparViewState()
                TipoContenedorFiltro = Nothing
                TipoContenedorOrden = Nothing
                TipoContenedorQueryDefecto = Nothing
                TiposContenedores = New ObservableCollection(Of TipoContenedor)
                'Me.CanalHabilitado = True
                ucTipoContenedor = Nothing
                ucTipoContenedor.ExibirDados(False)
                

                ucTipoContenedor.FocusControle()

                RaiseEvent UpdatedControl(New ControleEventArgs With {.Controle = "TipoContenedor"})
            End If

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
        If TipoContenedorVisible Then
            ucTipoContenedor.Titulo = Traduzir("073_TipoContenedor_Titulo")
            ucTipoContenedor.Popup_Titulo = Traduzir("073_TipoContenedor_Popup_Titulo")
            ucTipoContenedor.Popup_Resultado = Traduzir("073_TipoContenedor_Popup_Resultado")
            ucTipoContenedor.Popup_Filtro = Traduzir("073_TipoContenedor_Popup_Filtro")
        End If

    End Sub

    ''' <summary>
    ''' Carrega TipoContenedor e SubCanal utilizando os parâmetros definidos pelo usuário.
    ''' </summary>
    Protected Sub ConfigurarControles()

        TraduzirControle()

        If TipoContenedorVisible Then
            ConfigurarControle_TipoContenedor()
        End If

    End Sub

    Protected Sub ConfigurarControle_TipoContenedor()

        Me.ucTipoContenedor.FiltroConsulta = Me.TipoContenedorFiltro
        Me.ucTipoContenedor.OrdenacaoConsulta = Me.TipoContenedorOrden
        Me.ucTipoContenedor.MaxRegistroPorPagina = MaxRegistroPorPagina
        Me.ucTipoContenedor.Tabela = New Tabela With {.Tabela = TabelaHelper.TipoContenedor}
        Me.ucTipoContenedor.MultiSelecao = Me.SelecaoMultipla
        Me.ucTipoContenedor.ControleHabilitado = Me.TipoContenedorHabilitado
        Me.ucTipoContenedor.QueryDefault = Me.TipoContenedorQueryDefecto

        If TiposContenedores IsNot Nothing AndAlso TiposContenedores.Count > 0 Then

            Dim dadosTipoContenedor As New Comon.RespuestaHelper
            dadosTipoContenedor.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

            For Each c As Clases.TipoContenedor In TiposContenedores
                If Not String.IsNullOrEmpty(c.Identificador) Then
                    Dim DadosExibir As New Comon.Helper.Respuesta
                    With DadosExibir
                        .IdentificadorPai = Nothing
                        .Identificador = c.Identificador
                        .Codigo = c.Codigo
                        .Descricao = c.Descripcion
                    End With
                    dadosTipoContenedor.DatosRespuesta.Add(DadosExibir)
                End If
            Next
            ucTipoContenedor.RegistrosSelecionados = dadosTipoContenedor
            'Me.CanalHabilitado = (Modo <> Enumeradores.Modo.Consulta)
            ucTipoContenedor.ExibirDados(False)
        End If



    End Sub

    'Private Sub BuscarTipoContenedor(identificadorTipoContenedor As String)
    '    Dim objRegistrosSelecionados As New RespuestaHelper

    '    ' Prepara o objeto Petición
    '    Dim objPeticion As New PeticionHelper
    '    objPeticion.Tabela = New Tabela With {.Tabela = TabelaHelper.SubCanal}
    '    objPeticion.Query = Me.SubCanalQueryDefecto

    '    joinSQL = New UtilHelper.JoinSQL With {.TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = TabelaHelper.Canal},
    '                               .CampoComumTabEsq = "OID_CANAL",
    '                               .TabelaDireita = New UtilHelper.Tabela With {.Tabela = TabelaHelper.SubCanal},
    '                               .CampoComumTabDireita = "OID_CANAL",
    '                               .NomeCampoChave = "OID_CANAL",
    '                               .ValorCampoChave = identificadorCanal
    '                              }
    '    objPeticion.JuncaoSQL = New SerializableDictionary(Of String, JoinSQL)
    '    objPeticion.JuncaoSQL.Add(("Join_" + joinSQL.TabelaEsquerda.Tabela.ToString()), joinSQL)
    '    objPeticion.ParametrosPaginacion = New Genesis.Comon.Paginacion.ParametrosPeticionPaginacion()
    '    objPeticion.ParametrosPaginacion.RealizarPaginacion = False
    '    objPeticion.ParametrosPaginacion.IndicePagina = 0
    '    objPeticion.ParametrosPaginacion.RegistrosPorPagina = Me.MaxRegistroPorPagina

    '    'Filtro para recuperar somente registro ativos
    '    objPeticion.FiltroSQL = New SerializableDictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    '    objPeticion.FiltroSQL.Add(New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.SubCanal}, New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("BOL_VIGENTE", "1")})

    '    ' Busca Resultado
    '    objRegistrosSelecionados = Prosegur.Genesis.LogicaNegocio.Classes.Helper.Busqueda(objPeticion)

    '    Dim datosRespuestaSubCanais As New List(Of Helper.Respuesta)
    '    Dim objSubCanaisSelecionados As RespuestaHelper = Nothing

    '    If ucSubCanal.RegistrosSelecionados IsNot Nothing AndAlso ucSubCanal.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucSubCanal.RegistrosSelecionados.DatosRespuesta.Count > 0 Then
    '        objSubCanaisSelecionados = ucSubCanal.RegistrosSelecionados.Clonar

    '        For Each objSubCanal As Helper.Respuesta In ucSubCanal.RegistrosSelecionados.DatosRespuesta
    '            If Canales.Exists(Function(c) c.Identificador = objSubCanal.IdentificadorPai) Then
    '                datosRespuestaSubCanais.Add(objSubCanal.Clonar)
    '            End If
    '        Next
    '    End If

    '    'se retorno apenas um subcanal então insere na lista
    '    If objRegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso objRegistrosSelecionados.DatosRespuesta.Count = 1 Then
    '        If objSubCanaisSelecionados Is Nothing Then
    '            objSubCanaisSelecionados = New RespuestaHelper
    '            objSubCanaisSelecionados.DatosRespuesta = New List(Of Helper.Respuesta)
    '        End If

    '        objSubCanaisSelecionados.DatosRespuesta.Add(objRegistrosSelecionados.DatosRespuesta(0).Clonar)
    '    End If

    '    If objSubCanaisSelecionados IsNot Nothing Then
    '        ucSubCanal.RegistrosSelecionados = objSubCanaisSelecionados
    '        ucSubCanal.ControleHabilitado = False
    '        ucSubCanal.ExibirDados()
    '        ucCanal.FocusControle()
    '    Else
    '        ucSubCanal.ExibirDados(False)
    '        ucSubCanal.FocusControle()
    '    End If
    'End Sub

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