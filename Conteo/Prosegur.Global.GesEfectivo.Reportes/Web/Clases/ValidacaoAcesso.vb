﻿Public Class ValidacaoAcesso

#Region "[VARIÁVEIS]"

    Private _ValidarAcesso As Boolean = True
    Private _ValidarAcao As Boolean = True
    Private _ValidarPemissaoAD As Boolean = True
    Private _PaginaAtual As Enumeradores.eTelas = Enumeradores.eTelas.EMBRANCO
    Private _InformacionUsuario As ContractoServ.Login.InformacionUsuario
    Private _ControlesValidar As Dictionary(Of String, System.Web.UI.Control) = Nothing
    Private _Acao As Enumeradores.eAcao = Enumeradores.eAcao.NoAction

#End Region

#Region "[CONSTRUTORES]"

    ''' <summary>
    ''' Construtor genérico.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

    End Sub

    ''' <summary>
    ''' Construtor
    ''' </summary>
    ''' <param name="InfoUsuario"></param>
    ''' <remarks></remarks>
    Public Sub New(InfoUsuario As ContractoServ.Login.InformacionUsuario)

        Me.InformacionUsuario = InfoUsuario

    End Sub

    ''' <summary>
    ''' Construtor
    ''' </summary>
    ''' <param name="ValAcesso"></param>
    ''' <param name="ValAcao"></param>
    ''' <param name="PagAtual"></param>
    ''' <param name="InfoUsuario"></param>
    ''' <remarks></remarks>
    Public Sub New(ValAcesso As Boolean, _
                   ValAcao As Boolean, _
                   ValPemissaoAD As Boolean, _
                   PagAtual As String, _
                   InfoUsuario As ContractoServ.Login.InformacionUsuario, _
                   Acao As Enumeradores.eAcao)

        Me.ValidarAcesso = ValAcesso
        Me.ValidarAcao = ValAcao
        Me.ValidarPemissaoAD = ValPemissaoAD
        Me.PaginaAtual = PagAtual
        Me.InformacionUsuario = InfoUsuario
        Me.Acao = Acao

    End Sub

#End Region

#Region "[PROPRIEDADE]"

    ''' <summary>
    ''' Propriedade que define se o usuário possui acesso a tela.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidarAcesso() As Boolean
        Get
            Return _ValidarAcesso
        End Get
        Set(value As Boolean)
            _ValidarAcesso = value
        End Set
    End Property

    ''' <summary>
    ''' Propriedade que define se o usuário possui acesso a ação da tela.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidarAcao() As Boolean
        Get
            Return _ValidarAcao
        End Get
        Set(value As Boolean)
            _ValidarAcao = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidarPemissaoAD() As Boolean
        Get
            Return _ValidarPemissaoAD
        End Get
        Set(value As Boolean)
            _ValidarPemissaoAD = value
        End Set
    End Property

    ''' <summary>
    ''' Propriedade que recebe/retorna a página atual.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PaginaAtual() As Enumeradores.eTelas
        Get
            Return _PaginaAtual
        End Get
        Set(value As Enumeradores.eTelas)
            _PaginaAtual = value
        End Set
    End Property

    ''' <summary>
    ''' Propriedade que recebe/retorna as informações do usuário.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InformacionUsuario() As ContractoServ.Login.InformacionUsuario
        Get
            Return _InformacionUsuario
        End Get
        Set(value As ContractoServ.Login.InformacionUsuario)
            _InformacionUsuario = value
        End Set
    End Property

    ''' <summary>
    ''' Lista dos controles de uma determinada página que deverão ser validados.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ControlesValidar() As Dictionary(Of String, System.Web.UI.Control)
        Get
            Return _ControlesValidar
        End Get
        Set(value As Dictionary(Of String, System.Web.UI.Control))
            _ControlesValidar = value
        End Set
    End Property

    ''' <summary>
    ''' Verifica se o usuário esta logado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UsuarioLogado() As Boolean
        Get
            If _InformacionUsuario Is Nothing _
                OrElse _InformacionUsuario.Delegaciones.Count = 0 Then
                Return False
            End If
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Define a ação da página
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Acao() As Enumeradores.eAcao
        Get
            Return _Acao
        End Get
        Set(value As Enumeradores.eAcao)
            _Acao = value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Validar se os controles tem permissao de uso pelo usuário logado.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/07/2009 Criado
    ''' </history>
    Public Sub ValidarControlesPagina()

        If InformacionUsuario IsNot Nothing _
            AndAlso InformacionUsuario.DelegacionLogin IsNot Nothing _
            AndAlso InformacionUsuario.DelegacionLogin.Plantas IsNot Nothing _
            AndAlso ControlesValidar IsNot Nothing _
            AndAlso ControlesValidar.Count > 0 Then

            Dim Permissao As String = String.Empty

            ' percorrer todos os controles
            For Each Controle In ControlesValidar

                ' montar permissao
                Permissao = PaginaAtual.ToString & Controle.Key.ToString

                Dim blTienePermiso As Boolean = False

                For Each objPlanta As Prosegur.Global.GesEfectivo.Reportes.ContractoServ.Login.Planta In InformacionUsuario.DelegacionLogin.Plantas

                    If objPlanta.TiposSectores IsNot Nothing AndAlso objPlanta.TiposSectores.Count > 0 Then

                        For Each objSector As Prosegur.Global.GesEfectivo.Reportes.ContractoServ.Login.TipoSector In objPlanta.TiposSectores
                            If objSector.Permisos IsNot Nothing Then
                                ' verificar se existe na lista de permissoes
                                If objSector.Permisos.Contains(Permissao) Then
                                    blTienePermiso = True
                                    Exit For
                                End If
                            End If
                        Next

                    End If

                Next

                ' caso não exista na lista, deve desabilitar o controle.
                Controle.Value.Visible = blTienePermiso

            Next

        End If

    End Sub

    ''' <summary>
    ''' Valida se o usuário tem permissão para acessar a pagina
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/07/2009 Criado
    ''' </history>
    Public Function ValidarAcessoPagina() As String

        ' verificar se o usuário está logado e se deve validar acesso
        If Not UsuarioLogado AndAlso ValidarAcesso Then

            ' usuário não está logado, retorna página para a qual deverá ser redirecionada.

            ' usuário não está logado, retorna página para a qual deverá ser redirecionada.
            If PaginaAtual = Enumeradores.eTelas.MENU Then
                Return Constantes.NOME_PAGINA_LOGIN & "?SinPermisos=1"
            Else
                Return Constantes.NOME_PAGINA_LOGIN & "?SesionExpirada=1"
            End If

            ' verificar se deve validar acesso e se o usuário ainda não selecionou a delegação na página principal do sistema
        ElseIf ValidarAcesso AndAlso _
               InformacionUsuario.DelegacionLogin Is Nothing AndAlso _
               PaginaAtual <> Enumeradores.eTelas.PRINCIPAL Then

            ' usuário logado mas não selecionou a delegação na tela principal
            Return Constantes.NOME_PAGINA_PRINCIPAL

            ' verificar se deve validar acesso e se página está na lista de permissões
        ElseIf ValidarAcesso AndAlso ValidarPemissaoAD AndAlso PaginaAtual <> Enumeradores.eTelas.MENU _
            AndAlso PaginaAtual <> Enumeradores.eTelas.ERRO Then

            Dim blHayPermisoPagina As Boolean = False
            If InformacionUsuario.DelegacionLogin IsNot Nothing AndAlso
               InformacionUsuario.DelegacionLogin.Plantas IsNot Nothing AndAlso
               InformacionUsuario.DelegacionLogin.Plantas.Count > 0 Then 'Se o usuário selecionou a delegação na tela principal

                For Each Planta As Prosegur.Global.GesEfectivo.Reportes.ContractoServ.Login.Planta In InformacionUsuario.DelegacionLogin.Plantas

                    If Planta.TiposSectores IsNot Nothing AndAlso Planta.TiposSectores.Count > 0 Then

                        ' Para cada setor: valida se existe permissão para a página
                        For Each objSector As Prosegur.Global.GesEfectivo.Reportes.ContractoServ.Login.TipoSector In Planta.TiposSectores

                            Dim pesquisa = From Permissoes In objSector.Permisos _
                                           Where Permissoes.StartsWith(PaginaAtual.ToString)

                            ' se existir permissão para a página, o loop é interrompido
                            If pesquisa.Count > 0 Then
                                blHayPermisoPagina = True
                                Exit For
                            End If

                        Next

                    End If

                Next

            End If

            ' se não encontrou a página na lista de permissões.
            If Not blHayPermisoPagina Then
                ' usuário logado mas não tem permissão para acessar a página informada. Deverá ser redirecionado para menu.
                Return Constantes.NOME_PAGINA_MENU
            End If

        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' Valida se o usuário tem permissão para acessar a página com uma determinada ação informada
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/07/2009 Criado
    ''' </history>
    Public Function ValidarAcaoPagina() As String

        ' se não é parar validar a ação
        If Not ValidarAcao Then
            Return String.Empty
        End If

        Dim strAcao As String = String.Empty

        Select Case Acao
            Case Enumeradores.eAcao.Consulta
                strAcao = Enumeradores.eAcoesTela._CONSULTAR.ToString()
            Case Else
                Return String.Empty
        End Select

        ' valida o acesso do usuário
        Return ValidarPermissao(PaginaAtual.ToString() & strAcao)

    End Function

    ''' <summary>
    ''' Validar se tem permissao para ação.
    ''' </summary>
    ''' <param name="Tela"></param>
    ''' <param name="Acao"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/07/2009 Criado
    ''' </history>
    Public Function ValidarAcaoPagina(Tela As Enumeradores.eTelas, _
                                      Acao As Enumeradores.eAcoesTela) As Boolean

        If InformacionUsuario IsNot Nothing _
            AndAlso InformacionUsuario.DelegacionLogin IsNot Nothing _
            AndAlso InformacionUsuario.DelegacionLogin.Plantas IsNot Nothing _
            AndAlso InformacionUsuario.DelegacionLogin.Plantas.Count > 0 Then

            ' permissao
            Dim Permissao As String = Tela.ToString() & Acao.ToString()

            For Each objPlanta As Prosegur.Global.GesEfectivo.Reportes.ContractoServ.Login.Planta In InformacionUsuario.DelegacionLogin.Plantas

                If objPlanta.TiposSectores IsNot Nothing AndAlso objPlanta.TiposSectores.Count > 0 Then

                    ' verifica em cada setor se existe permisos para a validação de ação da página
                    For Each objSector As Prosegur.Global.GesEfectivo.Reportes.ContractoServ.Login.TipoSector In objPlanta.TiposSectores

                        ' verificar se existe na lista de permissoes
                        If objSector.Permisos.Contains(Permissao) Then
                            Return True
                        End If

                    Next

                End If

            Next

        End If

        Return False

    End Function

    ''' <summary>
    ''' Verifica se a permissão está presente nas permissões do usuário. 
    ''' Caso não esteja, redireciona para outra página.
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidarPermissao(Permissao As String) As String

        ' se usuário logado e página não for vazia
        If UsuarioLogado _
            AndAlso Permissao <> Enumeradores.eTelas.EMBRANCO.ToString Then

            ' se a pagina não for menu
            If Permissao <> Enumeradores.eTelas.MENU.ToString Then

                Dim blExistePermisaoPagina As Boolean = False
                If InformacionUsuario.DelegacionLogin IsNot Nothing AndAlso _
                   InformacionUsuario.DelegacionLogin.Plantas IsNot Nothing AndAlso InformacionUsuario.DelegacionLogin.Plantas.Count > 0 Then

                    For Each objPlanta As Prosegur.Global.GesEfectivo.Reportes.ContractoServ.Login.Planta In InformacionUsuario.DelegacionLogin.Plantas

                        ' para cada setor valida se existe permisao para página em cada permiso
                        For Each objSector As Prosegur.Global.GesEfectivo.Reportes.ContractoServ.Login.TipoSector In objPlanta.TiposSectores
                            If objSector.Permisos.Contains(Permissao) Then
                                blExistePermisaoPagina = True
                                Exit For
                            End If
                        Next

                    Next

                End If

                If Not blExistePermisaoPagina Then
                    ' usuário logado, porém não tem permissão de acesso na página
                    Return Constantes.NOME_PAGINA_MENU
                End If

            End If

        Else

            ' usuário não está logado, redirecionar para login
            Return Constantes.NOME_PAGINA_LOGIN & "?SinPermisos=1"

        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' Adiciona controles para validação.
    ''' </summary>
    ''' <param name="Acao"></param>
    ''' <param name="Controle"></param>
    ''' <remarks></remarks>
    Public Sub AddControleValidarPermissao(Acao As String, Controle As System.Web.UI.Control)

        If _ControlesValidar Is Nothing Then
            _ControlesValidar = New Dictionary(Of String, System.Web.UI.Control)
        End If

        _ControlesValidar.Add(Acao, Controle)

    End Sub

#End Region

End Class
