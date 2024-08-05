Imports System.Web.UI
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones

Public Class ucSector
    Inherits UcBase

#Region "[PROPRIEDADES]"

    Public Property Sectores As ObservableCollection(Of Sector)
        Get
            If Session(ID & "_Sectores") Is Nothing Then
                Session(ID & "_Sectores") = New ObservableCollection(Of Sector)
            End If
            Return Session(ID & "_Sectores")
        End Get
        Set(value As ObservableCollection(Of Sector))
            Session(ID & "_Sectores") = value
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

    Public Property UtilizadoForm As Boolean = False
    ' Eventos.
    Public Event UpdatedControl(sender As Object)

    Private joinSQL As UtilHelper.JoinSQL
    
#Region "[ucSector]"

    Private WithEvents _ucSector As ucHelperBusquedaDatosSector
    Public Property ucSector() As ucHelperBusquedaDatosSector
        Get
            If _ucSector Is Nothing Then
                _ucSector = LoadControl(ResolveUrl("~\Controles\Helpers\ucHelperBusquedaDatosSector.ascx"))
                _ucSector.ID = Me.ID & "_ucHelperBusquedaDatosSector"
                _ucSector.Obrigatorio = Me.SectorObrigatorio
                AddHandler _ucSector.Erro, AddressOf ErroControles
                If phSector.Controls.Count = 0 Then
                    phSector.Controls.Add(_ucSector)
                End If
            End If
            Return _ucSector
        End Get
        Set(value As ucHelperBusquedaDatosSector)
            _ucSector = value
        End Set
    End Property

    ''' <summary>
    ''' Define o Filtro a ser incluído na pesquisa de Canais.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _SectorFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property SectorFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        Get
            If _SectorFiltro Is Nothing Then
                ' Seta valor defector
                _SectorFiltro = New Dictionary(Of UtilHelper.Tabela, List(Of ArgumentosFiltro)) From
                {
                    {
                        New UtilHelper.Tabela With {.Tabela = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.Sector}, _
                        New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("BOL_ACTIVO", "1")}
                    }
                }
            End If

            'Verifica se está sendo utilizado dentro do área de formulario da pagina de busca
            If UtilizadoForm Then
                If Session("CentroProcessoForm") IsNot Nothing Then
                    ' Seta valor defector
                    _SectorFiltro = New Dictionary(Of UtilHelper.Tabela, List(Of ArgumentosFiltro)) From
                    {
                        {
                            New UtilHelper.Tabela With {.Tabela = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.Sector}, _
                            New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("BOL_CENTRO_PROCESO", If(CType(Session("CentroProcessoForm"), Boolean), "1", "0"))}
                        }
                    }
                End If
            Else
                If Session("CentroProcesso") IsNot Nothing Then
                    ' Seta valor defector
                    _SectorFiltro = New Dictionary(Of UtilHelper.Tabela, List(Of ArgumentosFiltro)) From
                    {
                        {
                            New UtilHelper.Tabela With {.Tabela = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.Sector}, _
                            New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("BOL_CENTRO_PROCESO", If(CType(Session("CentroProcesso"), Boolean), "1", "0"))}
                        }
                    }
                End If
            End If


            Return _SectorFiltro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _SectorFiltro = value
        End Set
    End Property

    ''' <summary>
    ''' Define as junções a serem incluídas para a pesquisa de Sector.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>    
    Private _SectorJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
    Public Property SectorJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
        Get
            If UtilizadoForm Then
                If Session("CodigoPlantaForm") IsNot Nothing Then
                    _SectorJuncao = New Dictionary(Of String, JoinSQL)
                    joinSQL = New UtilHelper.JoinSQL With {.TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = TabelaHelper.Planta},
                                                             .CampoComumTabEsq = "OID_PLANTA",
                                                             .TabelaDireita = New UtilHelper.Tabela With {.Tabela = TabelaHelper.Sector},
                                                             .CampoComumTabDireita = "OID_PLANTA",
                                                             .NomeCampoChave = "OID_PLANTA",
                                                             .ValorCampoChave = Session("CodigoPlantaForm").ToString()
                                                            }

                    ' Atribui um nome a chave do dicionário e os valores a serem incluídos na cláusula Join.        
                    _SectorJuncao.Add(("LeftJoin1_" + joinSQL.TabelaEsquerda.Tabela.ToString()), joinSQL)
                End If
            Else
                If Session("CodigoPlanta") IsNot Nothing Then
                    _SectorJuncao = New Dictionary(Of String, JoinSQL)
                    joinSQL = New UtilHelper.JoinSQL With {.TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = TabelaHelper.Planta},
                                                             .CampoComumTabEsq = "OID_PLANTA",
                                                             .TabelaDireita = New UtilHelper.Tabela With {.Tabela = TabelaHelper.Sector},
                                                             .CampoComumTabDireita = "OID_PLANTA",
                                                             .NomeCampoChave = "OID_PLANTA",
                                                             .ValorCampoChave = Session("CodigoPlanta").ToString()
                                                            }

                    ' Atribui um nome a chave do dicionário e os valores a serem incluídos na cláusula Join.        
                    _SectorJuncao.Add(("LeftJoin1_" + joinSQL.TabelaEsquerda.Tabela.ToString()), joinSQL)
                End If
                If Session("CodigoTipoSector") IsNot Nothing Then
                    joinSQL = New UtilHelper.JoinSQL With {.TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = TabelaHelper.TipoSector},
                                                             .CampoComumTabEsq = "OID_TIPO_SECTOR",
                                                             .TabelaDireita = New UtilHelper.Tabela With {.Tabela = TabelaHelper.Sector},
                                                             .CampoComumTabDireita = "OID_TIPO_SECTOR",
                                                             .NomeCampoChave = "OID_TIPO_SECTOR",
                                                             .ValorCampoChave = Session("CodigoTipoSector").ToString()
                                                            }

                    ' Atribui um nome a chave do dicionário e os valores a serem incluídos na cláusula Join.        
                    _SectorJuncao.Add(("LeftJoin2_" + joinSQL.TabelaEsquerda.Tabela.ToString()), joinSQL)
                End If
            End If


            Return _SectorJuncao
        End Get
        Set(value As Dictionary(Of String, UtilHelper.JoinSQL))
            _SectorJuncao = value
        End Set
    End Property

    ''' <summary>
    ''' Define como os dados retornados da consulta de Canais deverão ser ordenados.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _SectorOrden As Dictionary(Of String, UtilHelper.OrderSQL)
    Public Property SectorOrden As Dictionary(Of String, UtilHelper.OrderSQL)
        Get
            If _SectorOrden Is Nothing Then
                ' Seta valor defector
                _SectorOrden = New Dictionary(Of String, OrderSQL) From {{"COD_SECTOR", New OrderSQL("COD_SECTOR")}}
            End If
            Return _SectorOrden
        End Get
        Set(value As Dictionary(Of String, UtilHelper.OrderSQL))
            _SectorOrden = value
        End Set
    End Property

    Public Property SectorHabilitado As Boolean = False
    Public Property SectorObrigatorio As Boolean = False

    Public Property SectorTitulo As String = Traduzir("122_Sector_Titulo")

    Public Property SectorQueryDefecto As UtilHelper.QueryHelperControl = Nothing
    Public Property NoExhibirSector As Boolean? = Nothing

    Private _SectorVisible As Boolean = True
    Private Property SectorVisible As Boolean
        Get
            If NoExhibirSector IsNot Nothing Then
                Return Not NoExhibirSector
            Else
                Return _SectorVisible
            End If
        End Get
        Set(value As Boolean)
            _SectorVisible = value
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

            If Sectores IsNot Nothing AndAlso Sectores.Count > 0 Then

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
        ucSector.Focus()
    End Sub

#End Region

#Region "[EVENTOS]"

    Public Sub ucSector_OnControleAtualizado() Handles _ucSector.UpdatedControl
        Try
            If ucSector.RegistrosSelecionados IsNot Nothing AndAlso ucSector.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucSector.RegistrosSelecionados.DatosRespuesta.Count > 0 Then

                If Sectores Is Nothing OrElse Sectores.Count = 0 Then

                    For Each objDatosRespuesta In ucSector.RegistrosSelecionados.DatosRespuesta

                        Sectores.Add(New Clases.Sector() With {.Identificador = objDatosRespuesta.Identificador, _
                                                      .Codigo = objDatosRespuesta.Codigo, _
                                                      .Descripcion = objDatosRespuesta.Descricao _
                                                      })

                    Next

                ElseIf Sectores IsNot Nothing Then

                    For Each objSector As Clases.Sector In Sectores.Clonar()
                        Dim objSectorLocal = objSector
                        Dim aux = ucSector.RegistrosSelecionados.DatosRespuesta.Find(Function(x) x.Identificador = objSectorLocal.Identificador)
                        If aux Is Nothing Then
                            Sectores.RemoveAll(Function(c) c.Identificador = objSectorLocal.Identificador)
                        End If
                    Next

                    For Each objDatosRespuesta In ucSector.RegistrosSelecionados.DatosRespuesta
                        Dim objDatosRespuestaLocal = objDatosRespuesta
                        Dim aux = Sectores.Find(Function(x) x.Identificador = objDatosRespuestaLocal.Identificador)
                        If aux Is Nothing Then
                            Sectores.Add(New Clases.Sector() With {.Identificador = objDatosRespuesta.Identificador,
                                                                .Codigo = objDatosRespuesta.Codigo,
                                                                .Descripcion = objDatosRespuesta.Descricao})
                        End If
                    Next

                End If


            Else

                ucSector.LimparViewState()
                SectorFiltro = Nothing
                SectorJuncao = Nothing
                SectorOrden = Nothing
                SectorQueryDefecto = Nothing
                Sectores = New ObservableCollection(Of Sector)
                Me.SectorHabilitado = True
                ucSector = Nothing
                ucSector.ExibirDados(False)

                ucSector.FocusControle()
            End If

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
        If SectorVisible Then
            ucSector.Titulo = Me.SectorTitulo
            ucSector.Popup_Titulo = Traduzir("122_Sector_Popup_Titulo")
            ucSector.Popup_Resultado = Traduzir("122_Sector_Popup_Resultado")
            ucSector.Popup_Filtro = Traduzir("122_Sector_Popup_Filtro")
        End If

    End Sub

    ''' <summary>
    ''' Carrega Sector utilizando os parâmetros definidos pelo usuário.
    ''' </summary>
    Protected Sub ConfigurarControles()

        TraduzirControle()

        If SectorVisible Then
            ConfigurarControl_Sector()
        End If

    End Sub

    Protected Sub ConfigurarControl_Sector()

        Me.ucSector.FiltroConsulta = Me.SectorFiltro
        Me.ucSector.OrdenacaoConsulta = Me.SectorOrden
        Me.ucSector.JoinConsulta = Me.SectorJuncao
        Me.ucSector.MaxRegistroPorPagina = MaxRegistroPorPagina
        Me.ucSector.Tabela = New Tabela With {.Tabela = TabelaHelper.Sector}
        Me.ucSector.MultiSelecao = Me.SelecaoMultipla
        Me.ucSector.ControleHabilitado = Me.SectorHabilitado
        Me.ucSector.QueryDefault = Me.SectorQueryDefecto

        If Sectores IsNot Nothing AndAlso Sectores.Count > 0 Then


            Dim datosSector As New Comon.RespuestaHelper
            datosSector.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

            For Each c As Clases.Sector In Sectores
                If Not String.IsNullOrEmpty(c.Identificador) Then
                    Dim DadosExibir As New Comon.Helper.Respuesta
                    With DadosExibir
                        .IdentificadorPai = Nothing
                        .Identificador = c.Identificador
                        .Codigo = c.Codigo
                        .Descricao = c.Descripcion
                    End With
                    datosSector.DatosRespuesta.Add(DadosExibir)
                End If
            Next
            ucSector.RegistrosSelecionados = datosSector
            ucSector.ExibirDados(False)

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