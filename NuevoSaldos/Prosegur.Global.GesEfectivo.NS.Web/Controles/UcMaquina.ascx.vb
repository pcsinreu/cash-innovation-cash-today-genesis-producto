Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones
Partial Class UcMaquina
    Inherits UcBase

#Region "[PROPRIEDADES]"

    Public Property Sector As Sector
        Get
            If SelecaoMultipla Then
                Return Nothing
            Else
                Dim objSector As Clases.Sector = New Clases.Sector

                If Delegaciones IsNot Nothing AndAlso Delegaciones.Count > 0 Then
                    objSector.Delegacion = Delegaciones(0)
                End If

                'If Plantas IsNot Nothing AndAlso Plantas.Count > 0 Then
                '    If objSector.Delegacion IsNot Nothing AndAlso Not String.IsNullOrEmpty(objSector.Delegacion.Identificador) Then
                '        Dim objPlanta = Plantas.Find(Function(x) x.CodigoMigracion = objSector.Delegacion.Identificador)
                '        If objPlanta Is Nothing Then
                '            objSector.Planta = Plantas(0)
                '        Else
                '            objSector.Planta = objPlanta
                '        End If
                '    Else
                '        objSector.Planta = Plantas(0)
                '    End If
                'End If

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
            Delegaciones = Nothing
            TiposSectores = Nothing

            If value IsNot Nothing Then
                Sectores = New ObservableCollection(Of Sector)
                ' Plantas = New ObservableCollection(Of Planta)
                Delegaciones = New ObservableCollection(Of Delegacion)
                TiposSectores = New ObservableCollection(Of TipoSector)

                'If value.Planta IsNot Nothing AndAlso Not String.IsNullOrEmpty(value.Planta.Identificador) Then
                '    Plantas.Add(value.Planta)
                'End If
                If value.Delegacion IsNot Nothing AndAlso Not String.IsNullOrEmpty(value.Delegacion.Identificador) Then
                    Delegaciones.Add(value.Delegacion)
                End If
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

    Public Property Delegaciones As ObservableCollection(Of Delegacion)
        Get
            If ViewState(ID & "_Delegaciones") Is Nothing Then
                ViewState(ID & "_Delegaciones") = New ObservableCollection(Of Delegacion)
            End If
            Return ViewState(ID & "_Delegaciones")
        End Get
        Set(value As ObservableCollection(Of Delegacion))
            ViewState(ID & "_Delegaciones") = value
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

#Region "[ucDelegacion]"

    Private WithEvents _ucDelegacion As ucHelperBusquedaDatos
    Public Property ucDelegacion() As ucHelperBusquedaDatos
        Get
            If _ucDelegacion Is Nothing Then
                _ucDelegacion = LoadControl("~\Controles\ucHelperBusquedaDatos.ascx")
                _ucDelegacion.ID = "ucDelegacion"
                AddHandler _ucDelegacion.Erro, AddressOf ErroControles
                If phDelegacion.Controls.Count = 0 Then
                    phDelegacion.Controls.Add(_ucDelegacion)
                End If
                _ucDelegacion.HeightPopUp = 550
            End If
            Return _ucDelegacion
        End Get
        Set(value As ucHelperBusquedaDatos)
            _ucDelegacion = value
        End Set
    End Property

    ''' <summary>
    ''' Define o Filtro a ser incluído na pesquisa de SubClientes.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _DelegacionFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property DelegacionFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        Get
            If _DelegacionFiltro Is Nothing Then

                _DelegacionFiltro = New Dictionary(Of Tabela, List(Of ArgumentosFiltro))

                Dim valoresFiltros As New List(Of ArgumentosFiltro)
                valoresFiltros.Add(New ArgumentosFiltro("BOL_VIGENTE", "1", Helper.Enumeradores.EnumHelper.TipoCondicion.Igual))

                If Me.ConsiderarPermissoes Then
                    For Each delegacion In Base.InformacionUsuario.Delegaciones
                        valoresFiltros.Add(New ArgumentosFiltro("OID_DELEGACION", delegacion.Identificador, Helper.Enumeradores.EnumHelper.TipoCondicion.Igual))
                    Next
                End If

                _DelegacionFiltro.Add(New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Delegacion}, valoresFiltros)
            End If

            Return _DelegacionFiltro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _DelegacionFiltro = value
        End Set
    End Property

    ''' <summary>
    ''' Define as junções a serem incluídas para a pesquisa de SubCliente.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>    
    Private _DelegacionJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
    Public Property DelegacionJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
        Get
            Return _DelegacionJuncao
        End Get
        Set(value As Dictionary(Of String, UtilHelper.JoinSQL))
            _DelegacionJuncao = value
        End Set
    End Property

    ''' <summary>
    ''' Define como os dados retornados da consulta de SubCanais deverão ser ordenados.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _DelegacionOrden As Dictionary(Of String, UtilHelper.OrderSQL)
    Public Property DelegacionOrden As Dictionary(Of String, UtilHelper.OrderSQL)
        Get
            If _DelegacionOrden Is Nothing Then
                ' Seta valor defector
                _DelegacionOrden = New Dictionary(Of String, OrderSQL) From {{"COD_DELEGACION", New OrderSQL("COD_DELEGACION")}}
            End If
            Return _DelegacionOrden
        End Get
        Set(value As Dictionary(Of String, UtilHelper.OrderSQL))
            _DelegacionOrden = value
        End Set
    End Property

    Public Property DelegacionHabilitado() As Boolean = False
    Public Property DelegacionQueryDefecto As UtilHelper.QueryHelperControl = Nothing
    Public Property NoExhibirDelegacion As Boolean? = Nothing

    Private _DelegacionVisible As Boolean = True
    Private Property DelegacionVisible As Boolean
        Get
            If NoExhibirDelegacion IsNot Nothing Then
                Return Not NoExhibirDelegacion
            Else
                Return _DelegacionVisible
            End If
        End Get
        Set(value As Boolean)
            _DelegacionVisible = value
        End Set
    End Property

#End Region

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

                _SectorFiltro.Add(New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.MaquinaSector}, valoresFiltros)
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
                                               .TabelaDireita = New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.MaquinaSector},
                                               .CampoComumTabEsq = "OID_TIPO_SECTOR",
                                               .CampoComumTabDireita = "OID_TIPO_SECTOR",
                                               .NomeCampoChave = "OID_FORMULARIO",
                                               .ValorCampoChave = identificadorFormulario}}}
            End If

            If Delegaciones IsNot Nothing AndAlso Delegaciones.Count > 0 Then

                Dim objValorCampoChave As String = ""
                'For Each objDelegacion As Clases.Delegacion In Delegaciones
                '    If Not String.IsNullOrEmpty(objDelegacion.Identificador) Then
                '        objValorCampoChave &= "," & objDelegacion.Identificador
                '    End If
                'Next

                'If Not String.IsNullOrEmpty(objValorCampoChave) AndAlso Not String.IsNullOrEmpty(objValorCampoChave.Substring(1)) Then
                joinSQL = New UtilHelper.JoinSQL With {.TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = TabelaHelper.Planta},
                                                       .CampoComumTabEsq = "OID_PLANTA",
                                                       .TabelaDireita = New UtilHelper.Tabela With {.Tabela = TabelaHelper.MaquinaSector},
                                                       .CampoComumTabDireita = "OID_PLANTA"
                                                       }
                '.NomeCampoChave = "OID_PLANTA",
                '.ValorCampoChave = objValorCampoChave.Substring(1)
                '}

                '    ' Atribui um nome a chave do dicionário e os valores a serem incluídos na cláusula Join.        
                _SectorJuncao.Add(("Join_" + joinSQL.TabelaEsquerda.Tabela.ToString()), joinSQL)
                'End If

                objValorCampoChave = ""
                For Each objDelegacion As Clases.Delegacion In Delegaciones
                    If Not String.IsNullOrEmpty(objDelegacion.Identificador) Then
                        objValorCampoChave &= "," & objDelegacion.Identificador
                    End If
                Next

                If Not String.IsNullOrEmpty(objValorCampoChave) AndAlso Not String.IsNullOrEmpty(objValorCampoChave.Substring(1)) Then
                    ' Obtém código de SubCliente selecionado.
                    joinSQL = New UtilHelper.JoinSQL With {.TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = TabelaHelper.Delegacion},
                                                       .CampoComumTabEsq = "OID_DELEGACION",
                                                       .TabelaDireita = New UtilHelper.Tabela With {.Tabela = TabelaHelper.Planta},
                                                       .CampoComumTabDireita = "OID_DELEGACION",
                                                       .NomeCampoChave = "OID_DELEGACION",
                                                       .ValorCampoChave = objValorCampoChave.Substring(1)
                                                      }

                    ' Atribui um nome a chave do dicionário e os valores a serem incluídos na cláusula Join.        
                    _SectorJuncao.Add(("Join_" + joinSQL.TabelaEsquerda.Tabela.ToString()), joinSQL)
                End If

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

    Private _SectorVisible As Boolean = False
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

            If Delegaciones IsNot Nothing AndAlso Delegaciones.Count > 0 Then
                SectorVisible = True
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
        ucDelegacion.Focus()
    End Sub

#End Region

#Region "[EVENTOS]"

    Public Sub ucDelegacion_OnControleAtualizado() Handles _ucDelegacion.UpdatedControl
        Try
            If ucDelegacion.RegistrosSelecionados IsNot Nothing AndAlso ucDelegacion.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucDelegacion.RegistrosSelecionados.DatosRespuesta.Count > 0 Then
                Dim identificadorDelegacion As String = String.Empty
                If Delegaciones IsNot Nothing AndAlso Delegaciones.Count > 0 Then
                    'Verifica se foi selecionado apenas uma delegacion, verificando as delegaciones selecionadas anteriormente e as delegaciones atuais..
                    'se por exemplo existiam 3 delegaciones selecionadas e agora foi retornado 4, então foi selecionado apenas uma.
                    If ucDelegacion.RegistrosSelecionados.DatosRespuesta.Count - Delegaciones.Count = 1 Then
                        'descobrir qual a nova delegaion que foi selecionada
                        Dim delegacion = ucDelegacion.RegistrosSelecionados.DatosRespuesta.Where(Function(c) Not Delegaciones.Exists(Function(old) old.Identificador = c.Identificador)).FirstOrDefault
                        If delegacion IsNot Nothing Then
                            identificadorDelegacion = delegacion.Identificador
                        End If
                    End If
                Else
                    'Verifica se foi selecionado apenas uma delegacion
                    If ucDelegacion.RegistrosSelecionados.DatosRespuesta.Count = 1 Then
                        identificadorDelegacion = ucDelegacion.RegistrosSelecionados.DatosRespuesta(0).Identificador
                    End If
                End If


                Dim objDelegaciones As New ObservableCollection(Of Delegacion)
                Dim listaDelegacionExcluir As List(Of String) = Nothing
                For Each objDatosRespuestas In ucDelegacion.RegistrosSelecionados.DatosRespuesta
                    Dim objDelegacion As Clases.Delegacion = New Clases.Delegacion With {.Identificador = objDatosRespuestas.Identificador,
                                                                                         .Codigo = objDatosRespuestas.Codigo,
                                                                                         .Descripcion = objDatosRespuestas.Descricao}
                    objDelegaciones.Add(objDelegacion)
                Next

                'If Me.Delegaciones IsNot Nothing AndAlso Me.Delegaciones.Count > 0 Then
                '    listaDelegacionExcluir = Me.Delegaciones.Where(Function(old) Not objDelegaciones.Exists(Function(atual) atual.Identificador = old.Identificador)).Select(Function(d) d.Identificador).ToList
                '    If Me.ucPlanta.RegistrosSelecionados IsNot Nothing AndAlso Me.ucPlanta.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso Me.ucPlanta.RegistrosSelecionados.DatosRespuesta.Count > 0 Then
                '        Dim listaPlantasExcluir As List(Of String) = Me.ucPlanta.RegistrosSelecionados.DatosRespuesta.Where(Function(p) listaDelegacionExcluir.Contains(p.IdentificadorPai)).Select(Function(d) d.Identificador).ToList

                '        'se existe planta para ser exclida então excluir os sectores
                '        If listaPlantasExcluir IsNot Nothing AndAlso listaPlantasExcluir.Count > 0 Then
                '            'Excluir os sectores
                '            Me.ucSector.RegistrosSelecionados.DatosRespuesta.RemoveAll(Function(s) listaPlantasExcluir.Contains(s.IdentificadorPai))
                '            Me.Sectores.RemoveAll(Function(r) Not Me.ucSector.RegistrosSelecionados.DatosRespuesta.Exists(Function(s) s.Identificador = r.Identificador))

                '            'Exclui as plantas
                '            Me.ucPlanta.RegistrosSelecionados.DatosRespuesta.RemoveAll(Function(p) listaDelegacionExcluir.Contains(p.IdentificadorPai))
                '            Me.Plantas.RemoveAll(Function(r) Not Me.ucPlanta.RegistrosSelecionados.DatosRespuesta.Exists(Function(p) p.Identificador = r.Identificador))
                '        End If
                '    End If
                'End If

                Me.Delegaciones = objDelegaciones

                If Not String.IsNullOrEmpty(identificadorDelegacion) Then
                    BuscarSectorPorDelegacion(identificadorDelegacion)
                Else
                    'Me.AtualizarPlantas()
                    Me.AtualizarSectores()
                End If
            Else

                ucDelegacion.LimparViewState()
                DelegacionVisible = False
                DelegacionFiltro = Nothing
                DelegacionJuncao = Nothing
                DelegacionOrden = Nothing
                DelegacionQueryDefecto = Nothing
                Delegaciones = New ObservableCollection(Of Delegacion)
                ucDelegacion = Nothing
                ucDelegacion.ExibirDados(False)
                'If PlantaVisible Then
                '    ucPlanta.LimparViewState()
                '    PlantaVisible = False
                '    PlantaFiltro = Nothing
                '    PlantaJuncao = Nothing
                '    PlantaOrden = Nothing
                '    PlantaQueryDefecto = Nothing
                '    Plantas = New ObservableCollection(Of Planta)
                '    ucPlanta = Nothing
                '    phPlanta.Controls.Clear()
                'End If
                If SectorVisible Then
                    ucMaquina.LimparViewState()
                    SectorVisible = False
                    SectorFiltro = Nothing
                    ' SectorJuncao = Nothing
                    SectorOrden = Nothing
                    SectorQueryDefecto = Nothing
                    Sectores = New ObservableCollection(Of Sector)
                    ucMaquina = Nothing
                    phSector.Controls.Clear()
                End If

                ucDelegacion.FocusControle()
            End If

            RaiseEvent UpdatedControl(Me)

            TraduzirControle()
            Inicializar()

        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

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
        If DelegacionVisible Then
            ucDelegacion.Titulo = Traduzir("020_Delegacion_Titulo")
            ucDelegacion.Popup_Titulo = Traduzir("020_Delegacion_Popup_Titulo")
            ucDelegacion.Popup_Resultado = Traduzir("020_Delegacion_Popup_Resultado")
            ucDelegacion.Popup_Filtro = Traduzir("020_Delegacion_Popup_Filtro")
        End If

        If SectorVisible Then
            ucMaquina.Titulo = Traduzir("077_Maquina_Titulo")
            ucMaquina.Popup_Titulo = Traduzir("077_Maquina_Popup_Titulo")
            ucMaquina.Popup_Resultado = Traduzir("077_Maquina_Popup_Resultado")
            ucMaquina.Popup_Filtro = Traduzir("077_Maquina_Popup_Filtro")
        End If
    End Sub

    ''' <summary>
    ''' Carrega Cliente e SubCliente utilizando os parâmetros definidos pelo usuário.
    ''' </summary>
    Protected Sub ConfigurarControles()

        TraduzirControle()

        If DelegacionVisible Then
            ConfigurarControle_Delegacion()
        End If

        If SectorVisible Then
            ConfigurarControle_Sector()
        End If

    End Sub

    Protected Sub ConfigurarControle_Delegacion()

        Me.ucDelegacion.FiltroConsulta = Me.DelegacionFiltro
        Me.ucDelegacion.OrdenacaoConsulta = Me.DelegacionOrden
        Me.ucDelegacion.JoinConsulta = Me.DelegacionJuncao
        Me.ucDelegacion.MaxRegistroPorPagina = MaxRegistroPorPagina
        Me.ucDelegacion.Tabela = New Tabela With {.Tabela = TabelaHelper.Delegacion}
        Me.ucDelegacion.MultiSelecao = Me.SelecaoMultipla
        Me.ucDelegacion.ControleHabilitado = Me.DelegacionHabilitado
        Me.ucDelegacion.QueryDefault = Me.DelegacionQueryDefecto

        Me.AtualizarRegistrosDelegacion()
    End Sub

    Public Sub AtualizarRegistrosDelegacion()
        If Delegaciones IsNot Nothing AndAlso Delegaciones.Count > 0 Then
            Dim dadosSector As New Comon.RespuestaHelper
            dadosSector.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

            For Each objDelegacion As Clases.Delegacion In Delegaciones

                Dim DadosExibir As New Comon.Helper.Respuesta
                With DadosExibir
                    .IdentificadorPai = Nothing
                    .Identificador = objDelegacion.Identificador
                    .Codigo = objDelegacion.Codigo
                    .Descricao = objDelegacion.Descripcion
                End With

                'Para não exibir registros duplicados
                If Not dadosSector.DatosRespuesta.Exists(Function(s) s.Identificador = DadosExibir.Identificador) Then
                    dadosSector.DatosRespuesta.Add(DadosExibir)
                End If
            Next

            Me.ucDelegacion.RegistrosSelecionados = dadosSector
            Me.ucDelegacion.ExibirDados(False)
        Else
            Me.SectorVisible = False
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
        Me.ucMaquina.Tabela = New Tabela With {.Tabela = TabelaHelper.MaquinaSector}
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

    Private Sub BuscarSectorPorDelegacion(identificadorDelegacion As String)
        ' Prepara o objeto Petición
        Dim objPeticion As New PeticionHelper
        objPeticion.Tabela = New Tabela With {.Tabela = TabelaHelper.MaquinaSector}
        objPeticion.Query = Me.SectorQueryDefecto
        objPeticion.JuncaoSQL = New SerializableDictionary(Of String, JoinSQL)

        joinSQL = New UtilHelper.JoinSQL With {.TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = TabelaHelper.Planta},
                                   .CampoComumTabEsq = "OID_PLANTA",
                                   .TabelaDireita = New UtilHelper.Tabela With {.Tabela = TabelaHelper.MaquinaSector},
                                   .CampoComumTabDireita = "OID_PLANTA"
                                  }

        objPeticion.JuncaoSQL.Add(("Join_" + joinSQL.TabelaEsquerda.Tabela.ToString()), joinSQL)

        joinSQL2 = New UtilHelper.JoinSQL With {.TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = TabelaHelper.Delegacion},
                                   .CampoComumTabEsq = "OID_DELEGACION",
                                   .TabelaDireita = New UtilHelper.Tabela With {.Tabela = TabelaHelper.Planta},
                                   .CampoComumTabDireita = "OID_DELEGACION",
                                   .NomeCampoChave = "OID_DELEGACION",
                                   .ValorCampoChave = identificadorDelegacion
                                  }
        objPeticion.JuncaoSQL.Add(("Join_" + joinSQL2.TabelaEsquerda.Tabela.ToString()), joinSQL2)
        objPeticion.ParametrosPaginacion = New Genesis.Comon.Paginacion.ParametrosPeticionPaginacion()
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objPeticion.ParametrosPaginacion.IndicePagina = 0
        objPeticion.ParametrosPaginacion.RegistrosPorPagina = Me.MaxRegistroPorPagina

        'Filtro para recuperar somente registro ativos
        objPeticion.FiltroSQL = New SerializableDictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        objPeticion.FiltroSQL.Add(New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.MaquinaSector}, New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("BOL_ACTIVO", "1")})
        objPeticion.FiltroSQL.Add(New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Planta}, New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("BOL_ACTIVO", "1")})

        'Busca os sectores das plantas selecionadas
        'Busca as plantas das delegacoes selecionadas
        Dim objRespuestaHelper As RespuestaHelper = Prosegur.Genesis.LogicaNegocio.Classes.Helper.Busqueda(objPeticion)

        Dim datosRespuesta As New List(Of Helper.Respuesta)
        Dim objSelecionados As RespuestaHelper = Nothing

        If Me.ucMaquina.RegistrosSelecionados IsNot Nothing AndAlso ucMaquina.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucMaquina.RegistrosSelecionados.DatosRespuesta.Count > 0 Then
            objSelecionados = ucMaquina.RegistrosSelecionados.Clonar
        End If

        'se retornou apenas uma planta
        If objRespuestaHelper.DatosRespuesta IsNot Nothing AndAlso objRespuestaHelper.DatosRespuesta.Count = 1 Then
            If objSelecionados Is Nothing Then
                objSelecionados = New RespuestaHelper
                objSelecionados.DatosRespuesta = New List(Of Helper.Respuesta)
            End If

            objSelecionados.DatosRespuesta.Add(objRespuestaHelper.DatosRespuesta(0).Clonar)
        End If

        If objSelecionados IsNot Nothing Then
            ucMaquina.RegistrosSelecionados = objSelecionados
            ucMaquina.ControleHabilitado = False
            ucMaquina.ExibirDados()
            ucMaquina.FocusControle()
        Else
            ucMaquina.ExibirDados(False)
            ucMaquina.FocusControle()
        End If

        Me.AtualizarSectores()
    End Sub

    Private Sub AtualizarSectores()
        If Me.ucMaquina.RegistrosSelecionados IsNot Nothing AndAlso Me.ucMaquina.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso Me.ucMaquina.RegistrosSelecionados.DatosRespuesta.Count > 0 Then
            'Remove as plantas que não existe na lista de delegacion
            Me.ucMaquina.RegistrosSelecionados.DatosRespuesta.RemoveAll(Function(p) Not Me.Delegaciones.Exists(Function(d) d.Identificador = p.IdentificadorPai))

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

        If Delegaciones IsNot Nothing AndAlso Delegaciones.Count > 0 Then
            SectorVisible = True
        Else
            Me.ucMaquina.LimparViewState()
            Me.phSector.Controls.Clear()
        End If
    End Sub

#End Region

End Class
