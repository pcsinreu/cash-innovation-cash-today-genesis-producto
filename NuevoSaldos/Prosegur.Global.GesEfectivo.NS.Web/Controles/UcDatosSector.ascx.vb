Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones
Partial Class UcDatosSector
    Inherits UcBase

#Region "[PROPRIEDADES]"

    Public Property Sector As Sector
        Get
            If SelecaoMultipla Then
                Return Nothing
            Else
                Dim objSector As Clases.Sector = New Clases.Sector


                If Sectores IsNot Nothing AndAlso Sectores.Count > 0 Then
                    If objSector.Planta IsNot Nothing AndAlso Not String.IsNullOrEmpty(objSector.Planta.Identificador) Then
                        Dim aux = Sectores.Find(Function(x) x.CodigoMigracion = objSector.Planta.Identificador)
                        If aux Is Nothing Then
                            objSector.Identificador = Sectores(0).Identificador
                            objSector.Codigo = Sectores(0).Codigo
                            objSector.Descripcion = Sectores(0).Descripcion
                        Else
                            objSector.Identificador = aux.Identificador
                            objSector.Codigo = aux.Codigo
                            objSector.Descripcion = aux.Descripcion
                        End If
                    ElseIf objSector.TipoSector IsNot Nothing AndAlso Not String.IsNullOrEmpty(objSector.TipoSector.Identificador) Then
                        Dim aux = Sectores.Find(Function(x) x.CodigoMigracion = objSector.TipoSector.Identificador)
                        If aux Is Nothing Then
                            objSector.Identificador = Sectores(0).Identificador
                            objSector.Codigo = Sectores(0).Codigo
                            objSector.Descripcion = Sectores(0).Descripcion
                        Else
                            objSector.Identificador = aux.Identificador
                            objSector.Codigo = aux.Codigo
                            objSector.Descripcion = aux.Descripcion
                        End If
                    Else
                        objSector.Identificador = Sectores(0).Identificador
                        objSector.Codigo = Sectores(0).Codigo
                        objSector.Descripcion = Sectores(0).Descripcion
                    End If
                End If

                Return objSector
            End If
        End Get
        Set(value As Sector)

            Sectores = Nothing
            '    Plantas = Nothing

            TiposSectores = Nothing

            If value IsNot Nothing Then
                Sectores = New ObservableCollection(Of Sector)
                TiposSectores = New ObservableCollection(Of TipoSector)


                If value.TipoSector IsNot Nothing AndAlso Not String.IsNullOrEmpty(value.TipoSector.Identificador) Then
                    TiposSectores.Add(value.TipoSector)
                End If

                Sectores.Add(New Clases.Sector With {.Identificador = value.Identificador, .Descripcion = value.Descripcion, .Codigo = value.Codigo})
            End If
        End Set
    End Property

    Public Property Sectores As ObservableCollection(Of Sector)
        Get
            If ViewState(ID & "_Sectores") Is Nothing Then
                ViewState(ID & "_Sectores") = New ObservableCollection(Of Sector)
            End If
            Return ViewState(ID & "_Sectores")
        End Get
        Set(value As ObservableCollection(Of Sector))
            ViewState(ID & "_Sectores") = value
        End Set
    End Property


    Public Property TiposSectores As ObservableCollection(Of TipoSector)
        Get
            If ViewState(ID & "_TipoSectores") Is Nothing Then
                ViewState(ID & "_TipoSectores") = New ObservableCollection(Of TipoSector)
            End If
            Return ViewState(ID & "_TipoSectores")
        End Get
        Set(value As ObservableCollection(Of TipoSector))
            ViewState(ID & "_TipoSectores") = value
        End Set
    End Property

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

    Public identificadorFormulario As String

    Public Property SelecaoMultipla As Boolean = False

    Public Event UpdatedControl(sender As Object)

    Private joinSQL As UtilHelper.JoinSQL

    Private joinSQL2 As UtilHelper.JoinSQL
    Public Property ConsiderarPermissoes As Boolean
        Get
            If ViewState(ID & "_ConsiderarPermissoes") Is Nothing Then
                ViewState(ID & "_ConsiderarPermissoes") = False
            End If
            Return ViewState(ID & "_ConsiderarPermissoes")
        End Get
        Set(value As Boolean)
            ViewState(ID & "_ConsiderarPermissoes") = value
        End Set
    End Property

    Public Property SolamenteSectoresPadre As Boolean
        Get
            If ViewState(ID & "_SolamenteSectoresPadre") Is Nothing Then
                ViewState(ID & "_SolamenteSectoresPadre") = False
            End If
            Return ViewState(ID & "_SolamenteSectoresPadre")
        End Get
        Set(value As Boolean)
            ViewState(ID & "_SolamenteSectoresPadre") = value
        End Set
    End Property

    Public Property SolamenteFamiliaSector As Boolean
        Get
            If ViewState(ID & "_SolamenteFamiliaSector") Is Nothing Then
                ViewState(ID & "_SolamenteFamiliaSector") = False
            End If
            Return ViewState(ID & "_SolamenteFamiliaSector")
        End Get
        Set(value As Boolean)
            ViewState(ID & "_SolamenteFamiliaSector") = value
        End Set
    End Property

    Public Property SectorPadre As Sector



#Region "[ucSector]"

    Private WithEvents _ucMaquina As ucHelperBusquedaDatos
    Public Property ucMaquina() As ucHelperBusquedaDatos
        Get
            If _ucMaquina Is Nothing Then
                _ucMaquina = LoadControl("~\Controles\ucHelperBusquedaDatos.ascx")
                _ucMaquina.ID = "ucSectorDelegacion"
                AddHandler _ucMaquina.Erro, AddressOf ErroControles
                If phSector.Controls.Count = 0 Then
                    phSector.Controls.Add(_ucMaquina)
                End If
                _ucMaquina.HeightPopUp = 550
            End If
            Return _ucMaquina
        End Get
        Set(value As ucHelperBusquedaDatos)
            _ucMaquina = value
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

                _SectorFiltro = New Dictionary(Of Tabela, List(Of ArgumentosFiltro))

                Dim valoresFiltros As New List(Of ArgumentosFiltro)
                valoresFiltros.Add(New ArgumentosFiltro("BOL_ACTIVO", "1", Helper.Enumeradores.EnumHelper.TipoCondicion.Igual))

                ' valoresFiltros.Add(New ArgumentosFiltro(" COD_TIPO_SECTOR", "IN('MAE001','MAETC')", Helper.Enumeradores.EnumHelper.TipoCondicion.Avancado))

                If Me.ConsiderarPermissoes Then
                    'Permitir recuperar somente planta que o usuário tenha permissão
                    For Each objSector In Base.InformacionUsuario.Delegaciones.Where(Function(d) d.Plantas IsNot Nothing).SelectMany(Function(d) d.Plantas).Where(Function(p) p.Sectores IsNot Nothing).SelectMany(Function(p) p.Sectores).Where(Function(s) s.TipoSector IsNot Nothing).Select(Function(s) s.TipoSector.Identificador).Distinct()
                        valoresFiltros.Add(New ArgumentosFiltro("OID_TIPO_SECTOR", objSector, Helper.Enumeradores.EnumHelper.TipoCondicion.Igual))
                    Next
                End If

                _SectorFiltro.Add(New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Sector}, valoresFiltros)
            End If
            Return _SectorFiltro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _SectorFiltro = value
        End Set
    End Property

    ''' <summary>
    ''' Define as junções a serem incluídas para a pesquisa de Cliente.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>    
    Private _SectorJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
    Public Property SectorJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
        Get
            _SectorJuncao = New Dictionary(Of String, UtilHelper.JoinSQL)

            If Not String.IsNullOrEmpty(identificadorFormulario) Then

                ' Atribui um nome a chave do dicionário e os valores a serem incluídos na cláusula Join.        
                _SectorJuncao = New Dictionary(Of String, UtilHelper.JoinSQL) From {{"JOIN_GEPR_TTIPO_SECTORXFORMULARIO_OID_FORMULARIO",
                                               New JoinSQL() With {.TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.SectorXFormulario},
                                               .TabelaDireita = New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Sector},
                                               .CampoComumTabEsq = "OID_TIPO_SECTOR",
                                               .CampoComumTabDireita = "OID_TIPO_SECTOR",
                                               .NomeCampoChave = "OID_FORMULARIO",
                                               .ValorCampoChave = identificadorFormulario}}}
            End If



            'TIPO SECTOR

            Dim strJoinTs As New StringBuilder
            strJoinTs.AppendLine(" JOIN GEPR_TTIPO_SECTOR TSEC ")
            strJoinTs.AppendLine(" ON TSEC.OID_TIPO_SECTOR = S.OID_TIPO_SECTOR AND TSEC.BOL_ACTIVO = '1' ")

            joinSQL = New UtilHelper.JoinSQL With {.JoinPersonalizado = strJoinTs.ToString()}
            _SectorJuncao.Add(("Join_TipoSector"), joinSQL)




            If Me.SolamenteFamiliaSector AndAlso Me.SectorPadre IsNot Nothing Then
                Dim strJoin As New StringBuilder
                strJoin.AppendLine(" JOIN (SELECT SECT.OID_SECTOR ")
                strJoin.AppendLine(" FROM GEPR_TSECTOR SECT ")
                strJoin.AppendLine(" START WITH 1 = 1 AND SECT.OID_SECTOR = '{0}' ")
                strJoin.AppendLine(" CONNECT BY NOCYCLE PRIOR SECT.OID_SECTOR = SECT.OID_SECTOR_PADRE) SEC ON SEC.OID_SECTOR = S.OID_SECTOR ")

                joinSQL = New UtilHelper.JoinSQL With {.JoinPersonalizado = String.Format(strJoin.ToString(), Me.SectorPadre.Identificador)}
                _SectorJuncao.Add(("Join_SECTORHIJOS"), joinSQL)
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
                _SectorOrden = New Dictionary(Of String, OrderSQL) From {{"COD_SECTOR_ORDER", New OrderSQL("COD_SECTOR")}}
            End If
            Return _SectorOrden
        End Get
        Set(value As Dictionary(Of String, UtilHelper.OrderSQL))
            _SectorOrden = value
        End Set
    End Property

    Public Property SectorHabilitado As Boolean = False
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
        ucMaquina.Focus()
    End Sub

#End Region

#Region "[EVENTOS]"


    Public Sub ucSector_OnControleAtualizado() Handles _ucMaquina.UpdatedControl
        Try
            If ucMaquina.RegistrosSelecionados IsNot Nothing AndAlso ucMaquina.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucMaquina.RegistrosSelecionados.DatosRespuesta.Count > 0 Then
                Me.AtualizarSectores()
            Else

                Sectores = New ObservableCollection(Of Sector)

            End If

            ucMaquina.FocusControle()

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
            ucMaquina.Titulo = Traduzir("075_Maquina_Titulo")
            ucMaquina.Popup_Titulo = Traduzir("075_Maquina_Popup_Titulo")
            ucMaquina.Popup_Resultado = Traduzir("075_Maquina_Popup_Resultado")
            ucMaquina.Popup_Filtro = Traduzir("075_Maquina_Popup_Filtro")
        End If
    End Sub

    ''' <summary>
    ''' Carrega Cliente e SubCliente utilizando os parâmetros definidos pelo usuário.
    ''' </summary>
    Protected Sub ConfigurarControles()

        TraduzirControle()



        If SectorVisible Then
            ConfigurarControle_Sector()
        End If

    End Sub





    Public Sub AtualizarRegistrosSector()
        If Sectores IsNot Nothing AndAlso Sectores.Count > 0 Then
            Dim dadosSector As New Comon.RespuestaHelper
            dadosSector.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

            For Each objSector As Clases.Sector In Sectores

                If Not String.IsNullOrEmpty(objSector.Identificador) Then

                    Dim DadosExibir As New Comon.Helper.Respuesta
                    With DadosExibir
                        ' Foi usado o campo CodigoMigracion para guardar o Id da Delegacion
                        .IdentificadorPai = objSector.CodigoMigracion
                        .Identificador = objSector.Identificador
                        .Codigo = objSector.Codigo
                        .Descricao = objSector.Descripcion
                    End With

                    'Para não exibir registros duplicados
                    If Not dadosSector.DatosRespuesta.Exists(Function(s) s.Identificador = DadosExibir.Identificador) Then
                        dadosSector.DatosRespuesta.Add(DadosExibir)
                    End If
                End If
            Next

            Me.ucMaquina.RegistrosSelecionados = dadosSector
        End If

        Me.ucMaquina.ExibirDados(False)
    End Sub

    Protected Sub ConfigurarControle_Sector()
        Me.ucMaquina.FiltroConsulta = Me.SectorFiltro
        Me.ucMaquina.OrdenacaoConsulta = Me.SectorOrden
        Me.ucMaquina.JoinConsulta = Me.SectorJuncao
        Me.ucMaquina.MaxRegistroPorPagina = MaxRegistroPorPagina
        Me.ucMaquina.Tabela = New Tabela With {.Tabela = TabelaHelper.Sector}
        Me.ucMaquina.MultiSelecao = Me.SelecaoMultipla
        Me.ucMaquina.ControleHabilitado = Me.SectorHabilitado
        Me.ucMaquina.QueryDefault = Me.SectorQueryDefecto
        AtualizarRegistrosSector()
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


    Private Sub AtualizarSectores()
        If Me.ucMaquina.RegistrosSelecionados IsNot Nothing AndAlso Me.ucMaquina.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso Me.ucMaquina.RegistrosSelecionados.DatosRespuesta.Count > 0 Then


            'Atualiza as plantas
            Dim objSectores As New ObservableCollection(Of Sector)

            For Each objDatosRespuestas In Me.ucMaquina.RegistrosSelecionados.DatosRespuesta
                Dim objSector As Clases.Sector = New Clases.Sector With {.Identificador = objDatosRespuestas.Identificador,
                                                                                     .Codigo = objDatosRespuestas.Codigo,
                                                                                     .Descripcion = objDatosRespuestas.Descricao,
                                                                            .CodigoMigracion = objDatosRespuestas.IdentificadorPai}
                objSectores.Add(objSector)
            Next

            Me.Sectores = objSectores
        End If

        SectorVisible = False

    End Sub

#End Region

End Class
