Imports System.Web.UI
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones

Public Class ucCliente
    Inherits UcBase

#Region "[PROPRIEDADES]"

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
    Public Property EsBancoComision As Boolean = False

#Region "[ucCliente]"

    Private WithEvents _ucCliente As ucHelperAvanzadoBusquedaDatos
    Public Property ucCliente() As ucHelperAvanzadoBusquedaDatos
        Get
            If _ucCliente Is Nothing Then
                _ucCliente = LoadControl("~\Controles\ucHelperAvanzadoBusquedaDatos.ascx")
                _ucCliente.ID = "ucCliente"
                _ucCliente.HeightPopUp = 700
                AddHandler _ucCliente.Erro, AddressOf ErroControles
                If phCliente.Controls.Count = 0 Then
                    phCliente.Controls.Add(_ucCliente)
                End If
            End If
            Return _ucCliente
        End Get
        Set(value As ucHelperAvanzadoBusquedaDatos)
            _ucCliente = value
        End Set
    End Property


    Private _ClienteBuscaAvanzada As Dictionary(Of String, String)
    Public Property ClienteBuscaAvanzada As Dictionary(Of String, String)
        Get
            If _ClienteBuscaAvanzada Is Nothing Then
                ' Seta valor defector
                _ClienteBuscaAvanzada = New Dictionary(Of String, String)

                Dim auxBuscaAvabzada As String = String.Empty

                If EsBancoCapital Then
                    auxBuscaAvabzada &= " AND CLIE.BOL_BANCO_CAPITAL = '1' "
                End If

                If EsBancoComision Then
                    auxBuscaAvabzada &= " AND CLIE.BOL_BANCO_COMISION = '1' "
                End If

                If EsBanco Then
                    auxBuscaAvabzada &= " AND CLIT.COD_TIPO_CLIENTE = '1' "
                End If

                _ClienteBuscaAvanzada.Add("{0}", " AND UPPER(CLIE.COD_CLIENTE) &CODIGO " & auxBuscaAvabzada)
                _ClienteBuscaAvanzada.Add("{1}", " AND UPPER(COAJ.COD_AJENO) || ' *' &CODIGO " & auxBuscaAvabzada)
                _ClienteBuscaAvanzada.Add("{2}", " AND UPPER(CLIE.DES_CLIENTE) &DESCRIPCION ")
                ''{
                ''    {
                ''        New UtilHelper.Tabela With {.Tabela = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.Cliente},                     
                ''        New List(Of ArgumentosFiltro)  From {New ArgumentosFiltro("BOL_VIGENTE", "1")}
                ''    }
                ''}
                'If EsBancoCapital Then
                '    _ClienteFiltro(New UtilHelper.Tabela With {.Tabela = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.Cliente}).Add(New ArgumentosFiltro("BOL_BANCO_CAPITAL", "1"))
                'End If
                'If EsBancoComision Then
                '    _ClienteFiltro(New UtilHelper.Tabela With {.Tabela = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.Cliente}).Add(New ArgumentosFiltro("BOL_BANCO_COMISION", "1"))
                'End If
            End If
            Return _ClienteBuscaAvanzada
        End Get
        Set(value As Dictionary(Of String, String))
            _ClienteBuscaAvanzada = value
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
                '' Seta valor defector
                '_ClienteFiltro = New Dictionary(Of UtilHelper.Tabela, List(Of ArgumentosFiltro)) From
                '{
                '    {
                '        New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Cliente},
                '        New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("BOL_VIGENTE", "1")}
                '    }
                '}

                'If EsBancoCapital Then
                '    _ClienteFiltro(New UtilHelper.Tabela With {.Tabela = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.Cliente}).Add(New ArgumentosFiltro("BOL_BANCO_CAPITAL", "1"))
                'End If

                'If EsBancoComision Then
                '    _ClienteFiltro(New UtilHelper.Tabela With {.Tabela = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.Cliente}).Add(New ArgumentosFiltro("BOL_BANCO_COMISION", "1"))
                'End If


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

            'If EsBanco Then

            '    ' Obtém código de SubCliente selecionado.
            '    joinSQL = New UtilHelper.JoinSQL With {.TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = TabelaHelper.TipoCliente},
            '                                               .CampoComumTabEsq = "OID_TIPO_CLIENTE",
            '                                               .TabelaDireita = New UtilHelper.Tabela With {.Tabela = TabelaHelper.Cliente},
            '                                               .CampoComumTabDireita = "OID_TIPO_CLIENTE",
            '                                               .NomeCampoChave = "COD_TIPO_CLIENTE",
            '                                               .ValorCampoChave = "1"
            '                                              }

            '    ' Atribui um nome a chave do dicionário e os valores a serem incluídos na cláusula Join.        
            '    _ClienteJuncao.Add(("Join_" + joinSQL.TabelaEsquerda.Tabela.ToString()), joinSQL)

            'End If

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

#Region "[ucSubCliente]"

    Private WithEvents _ucSubCliente As ucHelperAvanzadoBusquedaDatos
    Public Property ucSubCliente() As ucHelperAvanzadoBusquedaDatos
        Get
            If _ucSubCliente Is Nothing Then
                _ucSubCliente = LoadControl("~\Controles\ucHelperAvanzadoBusquedaDatos.ascx")
                _ucSubCliente.ID = "ucSubCliente"
                AddHandler _ucSubCliente.Erro, AddressOf ErroControles
                If phSubCliente.Controls.Count = 0 Then
                    phSubCliente.Controls.Add(_ucSubCliente)
                End If
            End If
            Return _ucSubCliente
        End Get
        Set(value As ucHelperAvanzadoBusquedaDatos)
            _ucSubCliente = value
        End Set
    End Property

    ''' <summary>
    ''' Define o Filtro a ser incluído na pesquisa de SubClientes.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _SubClienteFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property SubClienteFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        Get
            If _SubClienteFiltro Is Nothing Then
                ' Seta valor defector
                _SubClienteFiltro = New Dictionary(Of UtilHelper.Tabela, List(Of ArgumentosFiltro)) From
                {
                    {
                        New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Cliente},
                        New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("BOL_VIGENTE", "1")}
                    }
                }
            End If
            Return _SubClienteFiltro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _SubClienteFiltro = value
        End Set
    End Property



    Private _SubClienteBuscaAvanzada As Dictionary(Of String, String)
    Public Property SubClienteBuscaAvanzada As Dictionary(Of String, String)
        Get
            If _SubClienteBuscaAvanzada Is Nothing Then
                ' Seta valor defector
                _SubClienteBuscaAvanzada = New Dictionary(Of String, String)

                _SubClienteBuscaAvanzada.Add("{0}", " AND SCLI.BOL_VIGENTE = '1' AND UPPER(SCLI.COD_SUBCLIENTE) &CODIGO ")
                _SubClienteBuscaAvanzada.Add("{1}", " AND SCLI.BOL_VIGENTE = '1' AND UPPER(COAJ.COD_AJENO) &CODIGO ")
                _SubClienteBuscaAvanzada.Add("{2}", " AND UPPER(SCLI.DES_SUBCLIENTE) &DESCRIPCION ")
            End If
            Return _SubClienteBuscaAvanzada
        End Get
        Set(value As Dictionary(Of String, String))
            _SubClienteBuscaAvanzada = value
        End Set
    End Property


    ''' <summary>
    ''' Define as junções a serem incluídas para a pesquisa de SubCliente.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>    
    Private _SubClienteJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
    Public Property SubClienteJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
        Get
            _SubClienteJuncao = New Dictionary(Of String, UtilHelper.JoinSQL)

            If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then

                Dim objValorCampoChave As String = " ' ' "
                For Each c As Clases.Cliente In Clientes
                    If Not String.IsNullOrEmpty(c.Identificador) Then
                        objValorCampoChave &= ",'" & c.Identificador & "'"
                    End If
                Next
                If Not String.IsNullOrEmpty(objValorCampoChave) AndAlso Not String.IsNullOrEmpty(objValorCampoChave.Substring(1)) Then
                    If SubClienteBuscaAvanzada.ContainsKey("{0}") Then
                        SubClienteBuscaAvanzada("{0}") &= " AND SCLI.OID_CLIENTE IN (" & objValorCampoChave & ") "
                    Else
                        SubClienteBuscaAvanzada.Add("{0}", " AND SCLI.OID_CLIENTE IN (" & objValorCampoChave & ") ")
                    End If



                    If SubClienteBuscaAvanzada.ContainsKey("{0}") Then
                        SubClienteBuscaAvanzada("{1}") &= " AND SCLI.OID_CLIENTE IN (" & objValorCampoChave & ") "
                    Else
                        SubClienteBuscaAvanzada.Add("{1}", " AND SCLI.OID_CLIENTE IN (" & objValorCampoChave & ") ")
                    End If
                End If

                'If Not String.IsNullOrEmpty(objValorCampoChave) AndAlso Not String.IsNullOrEmpty(objValorCampoChave.Substring(1)) Then
                '    ' Obtém código de SubCliente selecionado.
                '    joinSQL = New UtilHelper.JoinSQL With {.TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = TabelaHelper.Cliente},
                '                                       .CampoComumTabEsq = "OID_CLIENTE",
                '                                       .TabelaDireita = New UtilHelper.Tabela With {.Tabela = TabelaHelper.SubCliente},
                '                                       .CampoComumTabDireita = "OID_CLIENTE",
                '                                       .NomeCampoChave = "OID_CLIENTE",
                '                                       .ValorCampoChave = objValorCampoChave.Substring(1)
                '                                      }

                '    ' Atribui um nome a chave do dicionário e os valores a serem incluídos na cláusula Join.        
                '    _SubClienteJuncao.Add(("Join_" + joinSQL.TabelaEsquerda.Tabela.ToString()), joinSQL)
                'End If

            End If

            Return _SubClienteJuncao
        End Get
        Set(value As Dictionary(Of String, UtilHelper.JoinSQL))
            _SubClienteJuncao = value
        End Set
    End Property

    ''' <summary>
    ''' Define como os dados retornados da consulta de SubCanais deverão ser ordenados.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _SubClienteOrden As Dictionary(Of String, UtilHelper.OrderSQL)
    Public Property SubClienteOrden As Dictionary(Of String, UtilHelper.OrderSQL)
        Get
            If _SubClienteOrden Is Nothing Then
                ' Seta valor defector
                _SubClienteOrden = New Dictionary(Of String, OrderSQL) From {{"COD_SUBCLIENTE", New OrderSQL("COD_SUBCLIENTE")}}
            End If
            Return _SubClienteOrden
        End Get
        Set(value As Dictionary(Of String, UtilHelper.OrderSQL))
            _SubClienteOrden = value
        End Set
    End Property

    Public Property SubClienteHabilitado As Boolean = False
    Public Property SubClienteQueryDefecto As UtilHelper.QueryHelperControl = Nothing
    Public Property NoExhibirSubCliente As Boolean? = Nothing

    Private _SubClienteVisible As Boolean = False
    Private Property SubClienteVisible As Boolean
        Get
            If NoExhibirSubCliente IsNot Nothing Then
                Return Not NoExhibirSubCliente
            Else
                Return _SubClienteVisible
            End If
        End Get
        Set(value As Boolean)
            _SubClienteVisible = value
        End Set
    End Property

#End Region

#Region "[ucPtoServicio]"

    Private WithEvents _ucPtoServicio As ucHelperAvanzadoBusquedaDatos
    Public Property ucPtoServicio() As ucHelperAvanzadoBusquedaDatos
        Get
            If _ucPtoServicio Is Nothing Then
                _ucPtoServicio = LoadControl("~\Controles\ucHelperAvanzadoBusquedaDatos.ascx")
                _ucPtoServicio.ID = "ucPtoServicio"
                AddHandler _ucPtoServicio.Erro, AddressOf ErroControles
                If phPtoServicio.Controls.Count = 0 Then
                    phPtoServicio.Controls.Add(_ucPtoServicio)
                End If
            End If
            Return _ucPtoServicio
        End Get
        Set(value As ucHelperAvanzadoBusquedaDatos)
            _ucPtoServicio = value
        End Set
    End Property

    Private _PtoServicioBuscaAvanzada As Dictionary(Of String, String)
    Public Property PtoServicioBuscaAvanzada As Dictionary(Of String, String)
        Get
            If _PtoServicioBuscaAvanzada Is Nothing Then
                ' Seta valor defector
                _PtoServicioBuscaAvanzada = New Dictionary(Of String, String)

                _PtoServicioBuscaAvanzada.Add("{0}", " AND PTO.BOL_VIGENTE = '1' AND UPPER(PTO.COD_PTO_SERVICIO) &CODIGO ")
                _PtoServicioBuscaAvanzada.Add("{1}", " AND PTO.BOL_VIGENTE = '1' AND UPPER(COAJ.COD_AJENO) &CODIGO ")
                _PtoServicioBuscaAvanzada.Add("{2}", " AND UPPER(PTO.DES_PTO_SERVICIO) &DESCRIPCION ")
            End If
            Return _PtoServicioBuscaAvanzada
        End Get
        Set(value As Dictionary(Of String, String))
            _PtoServicioBuscaAvanzada = value
        End Set
    End Property

    ''' <summary>
    ''' Define o Filtro a ser incluído na pesquisa de SubClientes.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _PtoServicioFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property PtoServicioFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        Get
            If _PtoServicioFiltro Is Nothing Then
                '' Seta valor defector
                '_PtoServicioFiltro = New Dictionary(Of UtilHelper.Tabela, List(Of ArgumentosFiltro)) From
                '{
                '    {
                '        New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.PuntoServicio},
                '        New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("BOL_VIGENTE", "1")}
                '    }
                '}
            End If
            Return _PtoServicioFiltro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _PtoServicioFiltro = value
        End Set
    End Property

    ''' <summary>
    ''' Define as junções a serem incluídas para a pesquisa de SubCliente.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>    
    Private _PtoServicioJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
    Public Property PtoServicioJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
        Get
            _PtoServicioJuncao = New Dictionary(Of String, UtilHelper.JoinSQL)

            If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then

                Dim objValorCampoChave As String = " ' ' "
                For Each c As Clases.Cliente In Clientes
                    If c.SubClientes IsNot Nothing AndAlso c.SubClientes.Count > 0 Then
                        For Each s As Clases.SubCliente In c.SubClientes
                            If Not String.IsNullOrEmpty(s.Identificador) Then
                                objValorCampoChave &= ", '" & s.Identificador & "'"
                            End If
                        Next
                    End If
                Next

                If Not String.IsNullOrEmpty(objValorCampoChave) AndAlso Not String.IsNullOrEmpty(objValorCampoChave.Substring(1)) Then

                    If PtoServicioBuscaAvanzada.ContainsKey("{0}") Then
                        PtoServicioBuscaAvanzada("{0}") &= " AND PTO.OID_SUBCLIENTE IN (" & objValorCampoChave & ") "
                    Else
                        PtoServicioBuscaAvanzada.Add("{0}", " AND PTO.OID_SUBCLIENTE IN (" & objValorCampoChave & ") ")
                    End If



                    If PtoServicioBuscaAvanzada.ContainsKey("{0}") Then
                        PtoServicioBuscaAvanzada("{1}") &= " AND PTO.OID_SUBCLIENTE IN (" & objValorCampoChave & ") "
                    Else
                        PtoServicioBuscaAvanzada.Add("{1}", " AND PTO.OID_SUBCLIENTE IN (" & objValorCampoChave & ") ")
                    End If


                    '' Obtém código de SubCliente selecionado.
                    'joinSQL = New UtilHelper.JoinSQL With {.TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = TabelaHelper.SubCliente},
                    '                                   .CampoComumTabEsq = "OID_SUBCLIENTE",
                    '                                   .TabelaDireita = New UtilHelper.Tabela With {.Tabela = TabelaHelper.PuntoServicio},
                    '                                   .CampoComumTabDireita = "OID_SUBCLIENTE",
                    '                                   .NomeCampoChave = "OID_SUBCLIENTE",
                    '                                   .ValorCampoChave = objValorCampoChave.Substring(1)}

                    '' Atribui um nome a chave do dicionário e os valores a serem incluídos na cláusula Join.        
                    '_PtoServicioJuncao.Add(("Join_" + joinSQL.TabelaEsquerda.Tabela.ToString()), joinSQL)
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
                Dim objClientes = Clientes.FindAll(Function(c) c.SubClientes IsNot Nothing AndAlso c.SubClientes.Count > 0)
                If (objClientes IsNot Nothing AndAlso objClientes.Count > 0) Then
                    SubClienteVisible = True

                    For Each objCliente In objClientes
                        Dim objSubClientes = objCliente.SubClientes.FindAll(Function(c) c.PuntosServicio IsNot Nothing AndAlso c.PuntosServicio.Count > 0)
                        If (objSubClientes IsNot Nothing AndAlso objSubClientes.Count > 0) OrElse Modo <> Enumeradores.Modo.Consulta Then
                            PtoServicioVisible = True
                            Exit For
                        End If
                    Next
                ElseIf Modo <> Enumeradores.Modo.Consulta Then
                    SubClienteVisible = True
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
                                                      .SubClientes = Nothing})

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
                                                                .SubClientes = Nothing})
                        End If
                    Next

                End If

                ucSubCliente.LimparViewState()
                ucPtoServicio.LimparViewState()

                SubClienteVisible = False
                PtoServicioVisible = False

                If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
                    Dim objClientes = Clientes.FindAll(Function(c) c.SubClientes IsNot Nothing)
                    If Modo <> Enumeradores.Modo.Consulta OrElse (objClientes IsNot Nothing AndAlso objClientes.Count > 0) Then
                        SubClienteVisible = True
                    Else
                        Me.ucSubCliente.LimparViewState()
                        Me.phSubCliente.Controls.Clear()
                    End If
                    For Each objSubClientes In objClientes
                        If (objSubClientes.SubClientes IsNot Nothing AndAlso objSubClientes.SubClientes.Count > 0) Then
                            PtoServicioVisible = True
                            Exit For
                        End If
                    Next
                End If

                If Not PtoServicioVisible Then
                    Me.ucPtoServicio.LimparViewState()
                    Me.phPtoServicio.Controls.Clear()
                End If

                ucSubCliente.FocusControle()

            Else

                ucCliente.LimparViewState()
                ClienteFiltro = Nothing
                ClienteBuscaAvanzada = Nothing
                ClienteJuncao = Nothing
                ClienteOrden = Nothing
                ClienteQueryDefecto = Nothing
                Clientes = New ObservableCollection(Of Cliente)
                Me.ClienteHabilitado = True
                ucCliente = Nothing
                ucCliente.ExibirDados(False)
                If SubClienteVisible Then
                    ucSubCliente.LimparViewState()
                    SubClienteVisible = False
                    SubClienteFiltro = Nothing
                    SubClienteJuncao = Nothing
                    SubClienteOrden = Nothing
                    SubClienteQueryDefecto = Nothing
                    ucSubCliente = Nothing
                    phSubCliente.Controls.Clear()
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

    Public Sub ucSubCliente_OnControleAtualizado() Handles _ucSubCliente.UpdatedControl
        Try
            If ucSubCliente.RegistrosSelecionados IsNot Nothing AndAlso ucSubCliente.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucSubCliente.RegistrosSelecionados.DatosRespuesta.Count > 0 _
                AndAlso Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then

                For Each objCliente In Clientes
                    Dim objClienteLocal = objCliente

                    Dim objSubCliente As List(Of Helper.Respuesta) = ucSubCliente.RegistrosSelecionados.DatosRespuesta.FindAll(Function(x) x.IdentificadorPai = objClienteLocal.Identificador)

                    If objSubCliente Is Nothing Then
                        objCliente.SubClientes.Clear()
                    Else

                        If objCliente.SubClientes Is Nothing Then
                            objCliente.SubClientes = New ObservableCollection(Of SubCliente)
                        End If
                        For Each s In objCliente.SubClientes.Clonar()
                            Dim sLocal = s
                            Dim aux = objSubCliente.Find(Function(x) x.Identificador = sLocal.Identificador)
                            If aux Is Nothing Then
                                objCliente.SubClientes.Remove(objCliente.SubClientes.Find(Function(x) x.Identificador = sLocal.Identificador))
                            End If
                        Next

                        For Each objDatosRespuesta In objSubCliente
                            Dim objDatosRespuestaLocal = objDatosRespuesta
                            Dim aux = objCliente.SubClientes.Find(Function(x) x.Identificador = objDatosRespuestaLocal.Identificador)
                            If aux Is Nothing Then
                                objCliente.SubClientes.Add(New Clases.SubCliente With {.Identificador = objDatosRespuesta.Identificador,
                                                                                  .Codigo = objDatosRespuesta.Codigo,
                                                                                  .Descripcion = objDatosRespuesta.Descricao})
                            End If
                        Next
                    End If

                Next


                ucPtoServicio.LimparViewState()
                PtoServicioVisible = False

                If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
                    Dim objClientes = Clientes.FindAll(Function(c) c.SubClientes IsNot Nothing AndAlso c.SubClientes.Count > 0)
                    If objClientes IsNot Nothing Then
                        For Each objSubClientes In objClientes
                            If Modo <> Enumeradores.Modo.Consulta OrElse (objSubClientes.SubClientes IsNot Nothing AndAlso objSubClientes.SubClientes.Count > 0) Then
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
                        If objCliente.SubClientes IsNot Nothing Then
                            objCliente.SubClientes.Clear()
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

                ucSubCliente.FocusControle()
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

                    If objCliente.SubClientes IsNot Nothing Then
                        For Each objSubCliente In objCliente.SubClientes
                            Dim objSubClienteLocal = objSubCliente


                            Dim objPtoServicio As List(Of Helper.Respuesta) = ucPtoServicio.RegistrosSelecionados.DatosRespuesta.FindAll(Function(x) x.IdentificadorPai = objSubClienteLocal.Identificador)

                            If objPtoServicio Is Nothing Then
                                objSubCliente.PuntosServicio.Clear()
                            Else

                                If objSubCliente.PuntosServicio Is Nothing Then
                                    objSubCliente.PuntosServicio = New ObservableCollection(Of PuntoServicio)
                                End If
                                For Each s In objSubCliente.PuntosServicio.Clonar()
                                    Dim sLocal = s
                                    Dim aux = objPtoServicio.Find(Function(x) x.Identificador = sLocal.Identificador)
                                    If aux Is Nothing Then
                                        objSubCliente.PuntosServicio.Remove(objSubCliente.PuntosServicio.Find(Function(x) x.Identificador = sLocal.Identificador))
                                    End If
                                Next

                                For Each objDatosRespuesta In objPtoServicio
                                    Dim objDatosRespuestaLocal = objDatosRespuesta
                                    Dim aux = objSubCliente.PuntosServicio.Find(Function(x) x.Identificador = objDatosRespuestaLocal.Identificador)
                                    If aux Is Nothing Then
                                        objSubCliente.PuntosServicio.Add(New Clases.PuntoServicio With {.Identificador = objDatosRespuesta.Identificador,
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
                        If objCliente.SubClientes IsNot Nothing AndAlso objCliente.SubClientes.Count > 0 Then
                            For Each objSubCliente As Clases.SubCliente In objCliente.SubClientes
                                If objSubCliente.PuntosServicio IsNot Nothing Then
                                    objSubCliente.PuntosServicio.Clear()
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
            ElseIf EsBancoComision Then
                ucCliente.Titulo = Traduzir("021_BancoComision_Titulo")
                ucCliente.Popup_Titulo = Traduzir("021_BancoComision_Popup_Titulo")
                ucCliente.Popup_Resultado = Traduzir("021_BancoComision_Popup_Resultado")
                ucCliente.Popup_Filtro = Traduzir("021_BancoComision_Popup_Filtro")
            Else
                ucCliente.Titulo = Traduzir("021_Cliente_Titulo")
                ucCliente.Popup_Titulo = Traduzir("021_Cliente_Popup_Titulo")
                ucCliente.Popup_Resultado = Traduzir("021_Cliente_Popup_Resultado")
                ucCliente.Popup_Filtro = Traduzir("021_Cliente_Popup_Filtro")
            End If
        End If

        If SubClienteVisible Then
            If EsBancoCapital Then

                ucSubCliente.Titulo = Traduzir("022_BancoTesoreria_Titulo")
                ucSubCliente.Popup_Titulo = Traduzir("022_BancoTesoreria_Popup_Titulo")
                ucSubCliente.Popup_Resultado = Traduzir("022_BancoTesoreria_Popup_Resultado")
                ucSubCliente.Popup_Filtro = Traduzir("022_BancoTesoreria_Popup_Filtro")
            Else
                ucSubCliente.Titulo = Traduzir("022_SubCliente_Titulo")
                ucSubCliente.Popup_Titulo = Traduzir("022_SubCliente_Popup_Titulo")
                ucSubCliente.Popup_Resultado = Traduzir("022_SubCliente_Popup_Resultado")
                ucSubCliente.Popup_Filtro = Traduzir("022_SubCliente_Popup_Filtro")
            End If
        End If

        If PtoServicioVisible Then
            If EsBancoCapital Then

                ucPtoServicio.Titulo = Traduzir("023_CuentaTesoreria_Titulo")
                ucPtoServicio.Popup_Titulo = Traduzir("023_CuentaTesoreria_Popup_Titulo")
                ucPtoServicio.Popup_Resultado = Traduzir("023_CuentaTesoreria_Popup_Resultado")
                ucPtoServicio.Popup_Filtro = Traduzir("023_CuentaTesoreria_Popup_Filtro")
            Else
                ucPtoServicio.Titulo = Traduzir("023_PtoServicio_Titulo")
                ucPtoServicio.Popup_Titulo = Traduzir("023_PtoServicio_Popup_Titulo")
                ucPtoServicio.Popup_Resultado = Traduzir("023_PtoServicio_Popup_Resultado")
                ucPtoServicio.Popup_Filtro = Traduzir("023_PtoServicio_Popup_Filtro")
            End If
        End If
    End Sub

    ''' <summary>
    ''' Carrega Cliente e SubCliente utilizando os parâmetros definidos pelo usuário.
    ''' </summary>
    Protected Sub ConfigurarControles()

        TraduzirControle()

        If ClienteVisible Then
            ConfigurarControle_Cliente()
        End If

        If SubClienteVisible Then
            ConfigurarControle_SubCliente()
        End If

        If PtoServicioVisible Then
            ConfigurarControle_PtoServicio()
        End If

    End Sub

    Protected Sub ConfigurarControle_Cliente()

        Me.ucCliente.FiltroConsulta = Me.ClienteFiltro
        Me.ucCliente.FiltroAvanzado = Me.ClienteBuscaAvanzada
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
            Me.SubClienteVisible = False
            Me.PtoServicioVisible = False
        End If

    End Sub

    Protected Sub ConfigurarControle_SubCliente()

        If SubClienteVisible AndAlso Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then

            Me.ucSubCliente.FiltroConsulta = Me.SubClienteFiltro
            Me.ucSubCliente.FiltroAvanzado = Me.SubClienteBuscaAvanzada
            Me.ucSubCliente.OrdenacaoConsulta = Me.SubClienteOrden
            Me.ucSubCliente.JoinConsulta = Me.SubClienteJuncao
            Me.ucSubCliente.MaxRegistroPorPagina = MaxRegistroPorPagina
            Me.ucSubCliente.Tabela = New Tabela With {.Tabela = TabelaHelper.SubCliente}

            If Not Me.ucSubCliente.MultiSelecao Then
                Me.ucSubCliente.MultiSelecao = Me.SelecaoMultipla
            End If

            Me.ucSubCliente.ControleHabilitado = Me.SubClienteHabilitado
            Me.ucSubCliente.QueryDefault = Me.SubClienteQueryDefecto

            Dim objClientes = Clientes.FindAll(Function(x) x.SubClientes IsNot Nothing)

            If objClientes IsNot Nothing AndAlso objClientes.Count > 0 Then
                Dim dadosSubCliente As New Comon.RespuestaHelper
                dadosSubCliente.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

                For Each objCliente As Clases.Cliente In Clientes.FindAll(Function(x) x.SubClientes IsNot Nothing AndAlso x.SubClientes.Count > 0)

                    For Each objSubCliente As Clases.SubCliente In objCliente.SubClientes

                        Dim DadosExibir As New Comon.Helper.Respuesta
                        With DadosExibir
                            .IdentificadorPai = objCliente.Identificador
                            .Identificador = objSubCliente.Identificador
                            .Codigo = objSubCliente.Codigo
                            .Descricao = objSubCliente.Descripcion
                        End With

                        dadosSubCliente.DatosRespuesta.Add(DadosExibir)

                    Next
                Next

                ucSubCliente.RegistrosSelecionados = dadosSubCliente
                ucSubCliente.ExibirDados(False)

            Else
                ucSubCliente.ExibirDados(False)
                Me.PtoServicioVisible = False
            End If

        Else
            phSubCliente.Controls.Clear()
        End If

    End Sub

    Protected Sub ConfigurarControle_PtoServicio()

        If PtoServicioVisible AndAlso Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then

            Dim objClientes = Clientes.FindAll(Function(x) x.SubClientes IsNot Nothing)
            If objClientes IsNot Nothing AndAlso objClientes.Count > 0 Then

                Me.ucPtoServicio.FiltroConsulta = Me.PtoServicioFiltro
                Me.ucPtoServicio.FiltroAvanzado = Me.PtoServicioBuscaAvanzada
                Me.ucPtoServicio.OrdenacaoConsulta = Me.PtoServicioOrden
                Me.ucPtoServicio.JoinConsulta = Me.PtoServicioJuncao
                Me.ucPtoServicio.MaxRegistroPorPagina = MaxRegistroPorPagina
                Me.ucPtoServicio.Tabela = New Tabela With {.Tabela = TabelaHelper.PuntoServicio}
                If Not Me.ucPtoServicio.MultiSelecao Then
                    Me.ucPtoServicio.MultiSelecao = Me.SelecaoMultipla
                End If

                Me.ucPtoServicio.ControleHabilitado = Me.PtoServicioHabilitado
                Me.ucPtoServicio.QueryDefault = Me.PtoServicioQueryDefecto

                Dim dadosPtoServicio As New Comon.RespuestaHelper
                dadosPtoServicio.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

                For Each objCliente As Clases.Cliente In Clientes.FindAll(Function(x) x.SubClientes IsNot Nothing)
                    Dim objSubClientes = objCliente.SubClientes.FindAll(Function(x) x.PuntosServicio IsNot Nothing)
                    If objSubClientes IsNot Nothing Then
                        For Each objSubCliente As Clases.SubCliente In objSubClientes
                            If objSubCliente.PuntosServicio IsNot Nothing AndAlso objSubCliente.PuntosServicio.Count > 0 Then
                                For Each objPtoServicio As Clases.PuntoServicio In objSubCliente.PuntosServicio
                                    Dim DadosExibir As New Comon.Helper.Respuesta
                                    With DadosExibir
                                        .IdentificadorPai = objSubCliente.Identificador
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