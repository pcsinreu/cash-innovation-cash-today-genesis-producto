Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Net
Imports System.Globalization
Imports Prosegur.Genesis.Comon.Extenciones

Partial Public Class Base
    Inherits System.Web.UI.Page

#Region "[VARIÁVEIS]"

    Private _ValidacaoAcesso As ValidacaoAcesso = Nothing
    'Protected Shared objRs2010 As RS2010.ReportingService2010
    'Protected Shared objRs2005 As RS2005.ReportingService2005
    'Protected Shared objRSG As RSE.ReportExecutionService
    Protected objGerarReport As Prosegur.Genesis.Report.Gerar
    Protected scriptManager As ScriptManager

#End Region

#Region "[PROPRIEDADES]"

    Private Shared ReadOnly Property AlertModal As Boolean
        Get
            If HttpContext.Current.Request.QueryString("divModal") Is Nothing Then
                Return False
            Else
                Return True
            End If

        End Get
    End Property

    'Lista de relatórios e parametros..
    Public Property ParametrosReporte As Dictionary(Of String, List(Of Prosegur.Genesis.Report.Parametro))
        Get
            Return Application("ParametrosReporte")
        End Get
        Set(value As Dictionary(Of String, List(Of Prosegur.Genesis.Report.Parametro)))
            Application("ParametrosReporte") = value
        End Set
    End Property

    ''' <summary>
    ''' Informacoes do usuario logado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 10/02/2009 Criado
    ''' </history>
    Public Property InformacionUsuario() As ContractoServ.Login.InformacionUsuario
        Get

            ' se sessão não foi setado
            If Session("BaseInformacoesUsuario") IsNot Nothing Then

                ' tentar recuperar objeto da sessao
                Dim Info = TryCast(Session("BaseInformacoesUsuario"), ContractoServ.Login.InformacionUsuario)

                ' retornar objeto
                Return Info

            End If

            Return New ContractoServ.Login.InformacionUsuario

        End Get
        Set(value As ContractoServ.Login.InformacionUsuario)
            Session("BaseInformacoesUsuario") = value
        End Set
    End Property

    ''' <summary>
    ''' Flag que determina se deve ou não validar acesso à pagina.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 11/02/2009 Criado
    ''' </history>
    Public Property ValidarAcesso() As Boolean
        Get
            If ViewState("BaseValidarAcesso") Is Nothing Then
                Return True
            End If
            Return ViewState("BaseValidarAcesso")
        End Get
        Set(value As Boolean)
            ViewState("BaseValidarAcesso") = value
        End Set
    End Property

    ''' <summary>
    ''' Flag que determina se deve ou não validar ação da pagina.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 11/02/2009 Criado
    ''' </history>
    Public Property ValidarAcao() As Boolean
        Get
            If ViewState("BaseValidarAcao") Is Nothing Then
                Return True
            End If
            Return ViewState("BaseValidarAcao")
        End Get
        Set(value As Boolean)
            ViewState("BaseValidarAcao") = value
        End Set
    End Property

    ''' <summary>
    ''' Flag que determina se deve validar as pemissoes do active directory.
    ''' Caso esteja falso, irá validar apenas se existe uma sessão válida.
    ''' Muito utilizado para páginas padrões que não necessitam validar pemissões
    ''' vindas do AD. Ex: Busca cliente, Busca Sub-cliente, etc.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 17/03/2009 Criado
    ''' </history>
    Public Property ValidarPemissaoAD() As Boolean
        Get
            If ViewState("BaseValidarPemissaoAD") Is Nothing Then
                Return True
            End If
        End Get
        Set(value As Boolean)
            ViewState("BaseValidarPemissaoAD") = value
        End Set
    End Property

    ''' <summary>
    ''' Página atual em que o usuário se encontra.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 11/02/2009 Criado
    ''' </history>
    Public Property PaginaAtual() As Enumeradores.eTelas
        Get
            Return ViewState("BasePaginaAtual")
        End Get
        Set(value As Enumeradores.eTelas)
            ViewState("BasePaginaAtual") = value
        End Set
    End Property

    ''' <summary>
    ''' Ação atual da tela.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 11/02/2009 Criado
    ''' </history>
    Public Property Acao() As Enumeradores.eAcao
        Get
            Return ViewState("BaseAcao")
        End Get
        Set(value As Enumeradores.eAcao)
            ViewState("BaseAcao") = value
        End Set
    End Property

    ''' <summary>
    ''' Propriedade que informa a base se deve adicionar scripts no load da página.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 25/02/2009 Criado
    ''' </history>
    Public Property AddScripts() As Boolean
        Get
            If ViewState("BaseAdicionarScripts") Is Nothing Then
                Return True
            End If
            Return ViewState("BaseAdicionarScripts")
        End Get
        Set(value As Boolean)
            ViewState("BaseAdicionarScripts") = value
        End Set
    End Property

    ''' <summary>
    ''' Código da delegação no qual o usuário está logado.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 25/02/2009 Criado
    ''' </history>
    Public Property DelegacionConectada() As Dictionary(Of String, String)
        Get
            Return Session("BaseDelegacionConectada")
        End Get
        Set(value As Dictionary(Of String, String))
            Session("BaseDelegacionConectada") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena login do usuário logado.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 25/03/2009 Criado
    ''' </history>
    Public Property LoginUsuario() As String
        Get
            Return Session("BaseLoginUsuario")
        End Get
        Set(value As String)
            Session("BaseLoginUsuario") = value
        End Set
    End Property

#End Region

#Region "[EVENTOS]"

    ''' <summary>
    ''' Evento load.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 11/02/2009 Criado
    ''' </history>
    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Try

            scriptManager = scriptManager.GetCurrent(Me)

            If Not Page.IsPostBack Then
                ' configura parametros da base antes de iniciar as validações e iniciar a página filha.
                DefinirParametrosBase()
            End If

            ' criar objeto validação de acesso
            _ValidacaoAcesso = New ValidacaoAcesso(Me.ValidarAcesso,
                                                   Me.ValidarAcao,
                                                   Me.ValidarPemissaoAD,
                                                   Me.PaginaAtual,
                                                   Me.InformacionUsuario,
                                                   Me.Acao)

            ' validar acesso à página
            Dim ValAcessoPagina As String = _ValidacaoAcesso.ValidarAcessoPagina()
            If Not String.IsNullOrEmpty(ValAcessoPagina) Then
                Response.Redireccionar(ValAcessoPagina, True)
            End If

            ' validar ação na página
            Dim ValAcaoPagina As String = _ValidacaoAcesso.ValidarAcaoPagina()
            If Not String.IsNullOrEmpty(ValAcaoPagina) Then
                Response.Redireccionar(ValAcaoPagina, True)
            End If

            ' traduzir controles da tela
            TraduzirControles()

            'Seta configuração do Response para não armazenar a página em cache.
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            ' inicializar página filha
            Inicializar()

            ' se pode adicionar scripts
            If AddScripts Then
                ' adicionar scripts
                AdicionarScripts()
            End If

            ' configurar tabindex
            ConfigurarTabIndex()

        Catch ex As Exception
            Prosegur.BugsnagHelper.NotifyIfEnabled(ex)
            MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento pre-render.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 11/02/2009 Criado
    ''' </history>
    Private Sub Base_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender

        Try

            ' executa funções da página filha no pre-render da página.
            PreRenderizar()

            ' validar controles da página
            AdicionarControlesValidacao()
            _ValidacaoAcesso.ValidarControlesPagina()

        Catch ex As Exception
            Prosegur.BugsnagHelper.NotifyIfEnabled(ex)
            Util.LogarErroAplicacao(Nothing, ex.ToString, String.Empty, String.Empty, String.Empty)
        End Try

    End Sub

#End Region

#Region "[MÉTODOS]"

    <System.Web.Services.WebMethod()> _
    Public Shared Function urlLoginExpired()
        Return [Global].GesEfectivo.IAC.ContractoServicio.Constantes.NOME_PAGINA_LOGIN & "?SesionExpirada=1"
    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function GetDateServerUTC() As String
        'Return Date.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss+0000")
        Return Date.UtcNow.ToString("yyyy, MM, dd, HH, mm, ss")
    End Function

    ''' <summary>
    ''' Adiciona controles para validação.
    ''' </summary>
    ''' <param name="Acao"></param>
    ''' <param name="Controle"></param>
    ''' <remarks></remarks>
    Protected Sub AddControleValidarPermissao(Acao As String, Controle As System.Web.UI.Control)

        If _ValidacaoAcesso IsNot Nothing Then
            _ValidacaoAcesso.AddControleValidarPermissao(Acao, Controle)
        End If

    End Sub

    ''' <summary>
    ''' Retorna o Id do controle que receberá o foco
    ''' </summary>
    ''' <param name="Controle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function RetornarIdControleFoco(Controle As Control) As String

        ' Verifica se o tipo do controle é um PlaceHolder
        If TypeOf Controle Is UI.WebControls.PlaceHolder Then
            ' Retorna o Id do primeiro controle do Place Holder
            RetornarIdControleFoco(Controle.Controls(0))
            ' Se o controle é um botão
        ElseIf TypeOf Controle Is Prosegur.Web.Botao Then
            ' Retorna o Id do controle concatenado com "_img"
            Return (Controle.ID & "_img")

        Else
            ' Retorna o Id do controle
            Return (Controle.ClientID)

        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' Define para qual controle o foco deve ir quando o último controle perder o foco
    ''' </summary>
    ''' <param name="UltimoControle"></param>
    ''' <param name="PrimeiroControle"></param>
    ''' <remarks></remarks>
    Public Sub DefinirRetornoFoco(UltimoControle As Control, PrimeiroControle As Control, Optional TelaLogin As Boolean = False)

        ' Recupera o client id do primeiro controle (para o qual o foco deve ser retornado)
        Dim IdControleRetorno As String = RetornarIdControleFoco(PrimeiroControle)

        ' Se o controle é um menu
        If (TypeOf PrimeiroControle Is WebControls.Menu) Then
            ' Define o nome do primeiro nodo
            IdControleRetorno &= "n0"
        End If

        ' Dependendo do tipo de controle seta o atributo para retornar o foco ao 1o controle quando perder o foco
        If TypeOf UltimoControle Is Prosegur.Web.Botao Then

            CType(UltimoControle, Prosegur.Web.Botao).AdicionarAtributoIcone("onfocusout", "SetarProximoFoco('" & IdControleRetorno & "','" & TelaLogin & "')")

        ElseIf TypeOf UltimoControle Is WebControls.TextBox Then

            CType(UltimoControle, WebControls.TextBox).Attributes.Add("onblur", "SetarProximoFoco('" & IdControleRetorno & "','" & TelaLogin & "')")

        ElseIf TypeOf UltimoControle Is WebControls.LinkButton Then

            CType(UltimoControle, WebControls.LinkButton).Attributes.Add("onblur", "SetarProximoFoco('" & IdControleRetorno & "','" & TelaLogin & "')")

        End If

    End Sub

#End Region
#Region "Metodos ExibirMensagens"
    Public Sub MostraMensagemErro(erro As String, script As String)
        erro = erro.Replace(vbCrLf, "<br />")
        erro = erro.Replace(vbCr, "<br />")
        erro = erro.Replace(vbLf, "<br />")
        erro = erro.Replace("'", "&#39;")

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro", _
                                                   CriarChamadaMensagemErro(erro, script) _
                                                       , True)
    End Sub

    Public Sub MostraMensagem(erro As String)
        MostraMensagemErro(erro, String.Empty)
    End Sub
    Public Sub MostraMensagemExcecao(exception As Exception)
        Prosegur.BugsnagHelper.NotifyIfEnabled(exception)
        ' logar erro no banco
        Util.LogarErroAplicacao(Nothing, exception.ToString(), String.Empty, LoginUsuario, DelegacionConectada.Keys(0))
        MostraMensagemErro(exception.Message, String.Empty)
    End Sub

    Public Sub MostraMensagemErroConScript(erro As String, Optional script As String = "")
        MostraMensagemErro(erro, script)
    End Sub

    Public Shared Function CriarChamadaMensagemErro(erro As String, script As String) As String
        erro = erro.Replace(vbCrLf, "<br />")
        erro = erro.Replace(vbCr, "<br />")
        erro = erro.Replace(vbLf, "<br />")
        erro = erro.Replace("\", "\\")

        If AlertModal Then
            Return String.Format("ExibirMensagem2('{0}','{1}', '{2}' , '{3}');", _
                                                               erro, _
                                                                Traduzir("aplicacao"), script, Traduzir("btnFechar"))
        Else
            Return String.Format("ExibirMensagem('{0}','{1}', '{2}' , '{3}');", _
                                                                           erro, _
                                                                            Traduzir("aplicacao"), script, Traduzir("btnFechar"))
        End If

    End Function
#End Region
#Region "[ASSINATURA DE MÉTODOS]"

    ''' <summary>
    ''' Permite definir parametros da base antes de validar acessos e chamar o método inicializar.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub DefinirParametrosBase()
    End Sub

    ''' <summary>
    ''' Método chamado no load da base.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub Inicializar()
    End Sub

    ''' <summary>
    ''' Método chamado ao renderizar a base.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub PreRenderizar()
    End Sub

    ''' <summary>
    ''' Método que permite o desenvoledor adicionar controles para validação.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub AdicionarControlesValidacao()
    End Sub

    ''' <summary>
    ''' Método que permite o desenvolvedor traduzir os controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub TraduzirControles()
    End Sub

    ''' <summary>
    ''' Método que permite o desenvolvedor adicionar scripts para controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub AdicionarScripts()
    End Sub

    ''' <summary>
    ''' Método que permite o desenvolvedor configurar os tab index dos controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub ConfigurarTabIndex()
    End Sub

    ''' <summary>
    ''' Método que permite o desenvolvedor configurar o estado da página, seja ele de inserção, alteração ou exclusão
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub ConfigurarEstadoPagina()
    End Sub

#End Region

End Class