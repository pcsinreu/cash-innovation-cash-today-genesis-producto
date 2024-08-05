Public Class ValidacaoAcesso

#Region "[VARIÁVEIS]"

    Private _ValidarAcesso As Boolean = True
    Private _ValidarAcao As Boolean = True
    Private _ValidarPemissaoAD As Boolean = True
    Private _PaginaAtual As Aplicacao.Util.Utilidad.eTelas = Aplicacao.Util.Utilidad.eTelas.EMBRANCO
    Private _InformacionUsuario As ContractoServicio.Login.InformacionUsuario
    Private _ControlesValidar As Dictionary(Of String, System.Web.UI.Control) = Nothing
    Private _Acao As Aplicacao.Util.Utilidad.eAcao = Aplicacao.Util.Utilidad.eAcao.NoAction

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
    Public Sub New(InfoUsuario As ContractoServicio.Login.InformacionUsuario)

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
                   InfoUsuario As ContractoServicio.Login.InformacionUsuario, _
                   Acao As Aplicacao.Util.Utilidad.eAcao)

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
    ''' 
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
    ''' 
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
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PaginaAtual() As Aplicacao.Util.Utilidad.eTelas
        Get
            Return _PaginaAtual
        End Get
        Set(value As Aplicacao.Util.Utilidad.eTelas)
            _PaginaAtual = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InformacionUsuario() As ContractoServicio.Login.InformacionUsuario
        Get
            Return _InformacionUsuario
        End Get
        Set(value As ContractoServicio.Login.InformacionUsuario)
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
                OrElse _InformacionUsuario.Permisos.Count = 0 Then
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
    Public Property Acao() As Aplicacao.Util.Utilidad.eAcao
        Get
            Return _Acao
        End Get
        Set(value As Aplicacao.Util.Utilidad.eAcao)
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
    ''' [octavio.piramo] 20/02/2009 Criado
    ''' </history>
    Public Sub ValidarControlesPagina()

        If InformacionUsuario IsNot Nothing _
            AndAlso InformacionUsuario.Permisos IsNot Nothing _
            AndAlso InformacionUsuario.Permisos.Count _
            AndAlso ControlesValidar IsNot Nothing _
            AndAlso ControlesValidar.Count > 0 Then

            Dim Permissao As String = String.Empty

            ' percorrer todos os controles
            For Each Controle In ControlesValidar

                ' montar permissao
                Permissao = PaginaAtual.ToString & Controle.Key.ToString

                ' verificar se existe na lista de permissoes
                If Not InformacionUsuario.Permisos.Contains(Permissao) Then
                    ' caso não exista na lista, deve desabilitar o controle.
                    Controle.Value.Visible = False
                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Valida se o usuário tem permissão para acessar a pagina
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 20/02/2009 Criado
    ''' </history>
    Public Function ValidarAcessoPagina() As String

        ' verificar se o usuário está logado e se deve validar acesso
        If Not UsuarioLogado AndAlso ValidarAcesso Then

            ' usuário não está logado, retorna página para a qual deverá ser redirecionada.
            If PaginaAtual = Aplicacao.Util.Utilidad.eTelas.MENU Then
                Return ContractoServicio.Constantes.NOME_PAGINA_LOGIN & "?SinPermisos=1"
            Else
                Return ContractoServicio.Constantes.NOME_PAGINA_LOGIN & "?SesionExpirada=1"
            End If

            ' verificar se deve validar acesso e se página está na lista de permissões
        ElseIf ValidarAcesso AndAlso ValidarPemissaoAD AndAlso PaginaAtual <> Aplicacao.Util.Utilidad.eTelas.MENU _
            AndAlso PaginaAtual <> Aplicacao.Util.Utilidad.eTelas.ERRO Then

            Dim pesquisa = From Permissoes In InformacionUsuario.Permisos _
                           Where Permissoes.StartsWith(PaginaAtual.ToString)

            ' se não controu a página na lista de permissões.
            If pesquisa.Count = 0 Then
                ' usuário logado mas não tem permissão para acessar a página informada. Deverá ser redirecionado para menu.
                Return ContractoServicio.Constantes.NOME_PAGINA_MENU
            End If

        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' Valida se o usuário tem permissão para acessar a página com uma determinada ação informada
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 12/02/2009 Criado
    ''' </history>
    Public Function ValidarAcaoPagina() As String

        ' se não é parar validar a ação
        If Not ValidarAcao Then
            Return String.Empty
        End If

        Dim strAcao As String = String.Empty

        Select Case Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta
                strAcao = Aplicacao.Util.Utilidad.eAcoesTela._DAR_ALTA.ToString()
            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                strAcao = Aplicacao.Util.Utilidad.eAcoesTela._MODIFICAR.ToString()
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                strAcao = Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR.ToString()
            Case Aplicacao.Util.Utilidad.eAcao.Baja
                strAcao = Aplicacao.Util.Utilidad.eAcoesTela._DAR_BAJA.ToString()
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
    ''' [octavio.piramo] 11/02/2009 Criado
    ''' </history>
    Public Function ValidarAcaoPagina(Tela As Aplicacao.Util.Utilidad.eTelas, _
                                      Acao As Aplicacao.Util.Utilidad.eAcoesTela) As Boolean

        If InformacionUsuario IsNot Nothing _
            AndAlso InformacionUsuario.Permisos IsNot Nothing _
            AndAlso InformacionUsuario.Permisos.Count Then

            ' permissao
            Dim Permissao As String = Tela.ToString() & Acao.ToString()

            ' verificar se existe na lista de permissoes
            If InformacionUsuario.Permisos.Contains(Permissao) Then
                Return True
            End If

        End If

        Return False

    End Function

    Public Function ValidarAcaoPagina(Telas As Aplicacao.Util.Utilidad.eTelas(), _
                                      Acao As Aplicacao.Util.Utilidad.eAcoesTela) As Boolean

        If InformacionUsuario IsNot Nothing _
            AndAlso InformacionUsuario.Permisos IsNot Nothing _
            AndAlso InformacionUsuario.Permisos.Count Then

            Dim Permissao As String
            Dim strAcao As String = Acao.ToString()

            For Each tela In Telas
                ' permissao
                Permissao = tela.ToString() & strAcao

                ' verificar se existe na lista de permissoes
                If InformacionUsuario.Permisos.Contains(Permissao) Then
                    Return True
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
    Public Function ValidarPermissao(Permissao As String) As String

        ' se usuário logado e página não for vazia
        If UsuarioLogado _
            AndAlso Permissao <> Aplicacao.Util.Utilidad.eTelas.EMBRANCO.ToString Then

            ' se a pagina não for menu
            If Permissao <> Aplicacao.Util.Utilidad.eTelas.MENU.ToString Then

                If Not InformacionUsuario.Permisos.Contains(Permissao) Then
                    ' usuário logado, porém não tem permissão de acesso na página
                    Return ContractoServicio.Constantes.NOME_PAGINA_MENU
                End If

            End If

        Else

            ' usuário não está logado, redirecionar para login
            Return ContractoServicio.Constantes.NOME_PAGINA_LOGIN & "?SinPermisos=1"

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