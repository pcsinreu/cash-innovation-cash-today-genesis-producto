Imports Prosegur.Framework.Dicionario.Tradutor
Imports AjaxPro
Imports System.Reflection
Imports System.ComponentModel
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util
Imports System.Web.Services
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon

Partial Public MustInherit Class Base
    Inherits System.Web.UI.Page

#Region "[VARIÁVEIS]"

    Private _ValidacaoAcesso As ValidacaoAcesso = Nothing
    Protected _DecimalSeparador As String = Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator
    Protected _MilharSeparador As String = Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator

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

    ''' <summary>
    ''' Informacoes do usuario logado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 10/02/2009 Criado
    ''' </history>
    Public Property InformacionUsuario() As ContractoServicio.Login.InformacionUsuario
        Get

            ' se sessão não foi setado
            If Session("BaseInformacoesUsuario") IsNot Nothing Then

                ' tentar recuperar objeto da sessao
                Dim Info = TryCast(Session("BaseInformacoesUsuario"), ContractoServicio.Login.InformacionUsuario)

                ' retornar objeto
                Return Info

            End If

            Return New ContractoServicio.Login.InformacionUsuario

        End Get
        Set(value As ContractoServicio.Login.InformacionUsuario)
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
    Public Property PaginaAtual() As Aplicacao.Util.Utilidad.eTelas
        Get
            Return ViewState("BasePaginaAtual")
        End Get
        Set(value As Aplicacao.Util.Utilidad.eTelas)
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
    Public Property Acao() As Aplicacao.Util.Utilidad.eAcao
        Get
            Return ViewState("BaseAcao")
        End Get
        Set(value As Aplicacao.Util.Utilidad.eAcao)
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

    Public Property SessaoCriada() As Boolean
        Get
            If ViewState("SessaoCriada") Is Nothing Then
                Return False
            End If
            Return ViewState("SessaoCriada")
        End Get
        Set(value As Boolean)
            ViewState("SessaoCriada") = value
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
    Public Property DelegacionLogada() As Clases.Delegacion
        Get


            If Session("DelegacionLogada") Is Nothing Then

                Dim DelLogin = TryCast(Prosegur.Genesis.Web.Login.Parametros.Permisos.Usuario.Delegacion, Prosegur.Genesis.ContractoServicio.Login.EjecutarLogin.Delegacion)

                Dim objDelegacion As New Clases.Delegacion
                objDelegacion.Identificador = DelLogin.Identificador
                objDelegacion.Codigo = DelLogin.Codigo
                objDelegacion.Descripcion = DelLogin.Nombre
                objDelegacion.HusoHorarioEnMinutos = DelLogin.GMT
                objDelegacion.FechaHoraVeranoInicio = DelLogin.VeranoFechaHoraIni
                objDelegacion.FechaHoraVeranoFin = DelLogin.VeranoFechaHoraFin
                objDelegacion.AjusteHorarioVerano = DelLogin.VeranoAjuste

                Session("DelegacionLogada") = objDelegacion

            End If

            Return Session("DelegacionLogada")

        End Get
        Set(value As Clases.Delegacion)
            Session("DelegacionLogada") = value
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

    Private Property dicionario() As Comon.SerializableDictionary(Of String, Comon.SerializableDictionary(Of String, String))
        Get
            If Session("dicionario") IsNot Nothing Then
                Return Session("dicionario")
            Else
                Session("dicionario") = New Comon.SerializableDictionary(Of String, Comon.SerializableDictionary(Of String, String))
            End If

            Return Session("dicionario")
        End Get
        Set(value As Comon.SerializableDictionary(Of String, Comon.SerializableDictionary(Of String, String)))
            Session("dicionario") = value
        End Set
    End Property
    Public Property CodFuncionalidad() As String
        Get
            Return ViewState("CodFuncionalidad")
        End Get
        Set(value As String)
            ViewState("CodFuncionalidad") = value
        End Set
    End Property
    Public ReadOnly Property CodFuncionalidadGenerica() As String
        Get
            Return "GENERICO"
        End Get
    End Property

#End Region

#Region "[EVENTOS]"

    <System.Web.Services.WebMethod()>
    Public Shared Function isSessionExpired() As Boolean

        Dim retorno As Boolean = False

        If HttpContext.Current.Session IsNot Nothing Then
            'check the IsNewSession value, this will tell us if the session has been reset
            If HttpContext.Current.Session.IsNewSession Then
                'now we know it's a new session, so we check to see if a cookie is present
                Dim cookie As String = HttpContext.Current.Request.Headers("Cookie")
                'now we determine if there is a cookie does it contains what we're looking for
                If (cookie IsNot Nothing) AndAlso (cookie.IndexOf("ASP.NET_SessionId") >= 0) Then
                    'since it's a new session but a ASP.Net cookie exist we know
                    'the session has expired so we need to redirect them
                    Return True
                End If
            End If
        End If

        Return retorno

    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function urlLoginExpired() As String
        Return ContractoServicio.Constantes.NOME_PAGINA_LOGIN & "?SesionExpirada=1"
    End Function
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

        '    AjaxPro.Utility.RegisterTypeForAjax(GetType(Base))

        Try

            If Not Page.IsPostBack Then

                ' configura parametros da base antes de iniciar as validações e iniciar a página filha.
                DefinirParametrosBase()

                CarregaChavesDicinario(Me.CodFuncionalidadGenerica)
                CarregaChavesDicinario(Me.CodFuncionalidad)
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
                If Request.QueryString("esModal") IsNot Nothing AndAlso Request.QueryString("esModal") = "True" Then
                    ' fechar janela
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ClienteOk", "window.returnValue=0;window.close();", True)
                Else
                    Response.Redirect(ValAcessoPagina, False)
                    Exit Sub
                End If
            End If

            ' validar ação na página
            Dim ValAcaoPagina As String = _ValidacaoAcesso.ValidarAcaoPagina()
            If Not String.IsNullOrEmpty(ValAcaoPagina) Then
                Response.Redirect(ValAcaoPagina, False)
            End If

            ' traduzir controles da tela
            TraduzirControles()

            'Seta configuração do Response para não armazenar a página em cache.
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            ' Caso a Session tenha expirado não executa o Inicializar
            If Not ValAcessoPagina.Contains("SesionExpirada=1") Then
                ' inicializar página filha
                Inicializar()
            End If

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "SetGMT", "var _GMT = 0 ; var _AjusteVerano = 0;", True)

            'Registra variaveis de tradução que são usadas no calendario
            ' Dim imgUrl As String = ResolveUrl("~/App_Themes/Padrao/css/img/button/Calendar.png")
            Dim imgUrl As String = ResolveUrl("~/Imagenes/Calendar.png")
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "SetTraducao",
                                                    String.Format("var _bntProximo = '{0}';" &
                                                                  "var _btnAnterior = '{1}';" &
                                                                  "var _btnAgora = '{2}';" &
                                                                  "var _btnConfirma = '{3}';" &
                                                                  "var _meses = '{4}';" &
                                                                  "var _dias = '{5}';" &
                                                                  "var _horas = '{6}';" &
                                                                  "var _minutos = '{7}';" &
                                                                  "var _segundos = '{8}';" &
                                                                  "var _imgCalendar = '{9}';",
                                                                  Traduzir("bntProximo"),
                                                                  Traduzir("btnAnterior"),
                                                                  Traduzir("btnAgora"),
                                                                  Traduzir("btnConfirma"),
                                                                  Traduzir("meses"),
                                                                  Traduzir("dias"),
                                                                  Traduzir("horas"),
                                                                  Traduzir("minutos"),
                                                                  Traduzir("segundos"),
                                                                  imgUrl), True)

            ' se pode adicionar scripts
            If AddScripts Then
                ' adicionar scripts
                AdicionarScripts()
            End If

            ' configurar tabindex
            ConfigurarTabIndex()

        Catch ex As InicializarException
            Throw

        Catch ex As Exception
            Prosegur.BugsnagHelper.NotifyIfEnabled(ex)
            Aplicacao.Util.Utilidad.LogarErroAplicacao(Nothing, ex.ToString, String.Empty, String.Empty, String.Empty)
            Response.End()
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
            Aplicacao.Util.Utilidad.LogarErroAplicacao(Nothing, ex.ToString, String.Empty, String.Empty, String.Empty)
        End Try

    End Sub


    Public Function ValidarAcaoPagina(paginaAtual As Aplicacao.Util.Utilidad.eTelas, acao As Aplicacao.Util.Utilidad.eAcao) As Boolean

        ' criar objeto validação de acesso
        _ValidacaoAcesso = New ValidacaoAcesso(False,
                                               True,
                                               False,
                                               paginaAtual,
                                               Me.InformacionUsuario,
                                               acao)

        Return String.IsNullOrEmpty(_ValidacaoAcesso.ValidarAcaoPagina())


    End Function
#End Region

#Region "[MÉTODOS]"

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
    ''' Método responsável por verificar se o usuário possui uma determinada permissão
    ''' </summary>
    ''' <param name="Permissao"></param>
    ''' <remarks></remarks>
    ''' ''' <history>
    ''' [flag.gustavo.seabra] 25/07/2012 Criado
    ''' </history>
    Protected Function PossuiPermissao(Permissao As String) As Boolean

        ' criar objeto validação de acesso
        _ValidacaoAcesso = New ValidacaoAcesso(Me.ValidarAcesso,
                                               Me.ValidarAcao,
                                               Me.ValidarPemissaoAD,
                                               Me.PaginaAtual,
                                               Me.InformacionUsuario,
                                               Me.Acao)

        If String.IsNullOrEmpty(_ValidacaoAcesso.ValidarPermissao(Permissao)) Then
            Return True
        Else
            Return False
        End If

    End Function



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

        ' Cria o script para colocar o foco no primeiro controle
        Dim ScriptFoco As New StringBuilder

        If TelaLogin Then
            ' se for a tela login, não terá menu. Após o último controle deverá selecionar o link "detalhes" (se estiver visível na tela)
            With ScriptFoco
                .AppendLine("if (document.getElementById(idControleErro) == undefined){")
                .AppendLine("document.getElementById('" & IdControleRetorno & "').focus();")
                .AppendLine("}")
                .AppendLine("else{")
                .AppendLine("document.getElementById(idControleErro).focus();")
                .AppendLine("}")
            End With
        Else
            With ScriptFoco
                .AppendLine("document.getElementById('" & IdControleRetorno & "').focus();")
            End With
        End If

        ' Dependendo do tipo de controle seta o atributo para retornar o foco ao 1o controle quando perder o foco

        If TypeOf UltimoControle Is Prosegur.Web.Botao Then

            CType(UltimoControle, Prosegur.Web.Botao).AdicionarAtributoIcone("onfocusout", ScriptFoco.ToString())

        ElseIf TypeOf UltimoControle Is WebControls.TextBox Then

            CType(UltimoControle, WebControls.TextBox).Attributes.Add("onblur", ScriptFoco.ToString())

        ElseIf TypeOf UltimoControle Is WebControls.LinkButton Then

            CType(UltimoControle, WebControls.LinkButton).Attributes.Add("onblur", ScriptFoco.ToString())

        End If

    End Sub

    ''' <summary>
    ''' trata campos obrigatórios
    ''' </summary>
    ''' <param name="Controle"></param>
    ''' <param name="Validator"></param>
    ''' <param name="SetarFocoControle"></param>
    ''' <param name="FocoSetado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  30/12/2010  criado
    ''' </history>
    Public Function TratarCampoObrigatorio(Controle As Object, ByRef Validator As CustomValidator,
                                           SetarFocoControle As Boolean, ByRef FocoSetado As Boolean) As String

        Dim strErro As String = String.Empty
        Dim bolVazio As Boolean = False

        ' verifica se o controle está vazio de acordo com o seu tipo
        If TypeOf Controle Is DropDownList Then

            If String.IsNullOrEmpty(CType(Controle, DropDownList).SelectedValue) Then
                bolVazio = True
            End If

        ElseIf TypeOf Controle Is TreeView Then

            If CType(Controle, TreeView).Nodes.Count = 0 Then
                bolVazio = True
            End If

        ElseIf TypeOf Controle Is TextBox Then

            If String.IsNullOrEmpty(CType(Controle, TextBox).Text.Trim()) Then
                bolVazio = True
            End If

        ElseIf TypeOf Controle Is Helper Then

            If String.IsNullOrEmpty(CType(Controle, Helper).TextTextBox.Trim()) Then
                bolVazio = True
            End If

        ElseIf TypeOf Controle Is ListBox Then

            If (CType(Controle, ListBox)).Items.Count = 0 Then
                bolVazio = True
            End If

        End If

        If bolVazio Then

            strErro = Validator.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak
            Validator.IsValid = False

            'Seta o foco no primeiro controle que deu erro
            If SetarFocoControle AndAlso Not FocoSetado Then

                If TypeOf Controle Is Helper Then
                    CType(Controle, Helper).BtnHelperFocus()
                Else
                    Controle.Focus()
                End If

                FocoSetado = True

            End If

        Else

            Validator.IsValid = True

        End If

        Return strErro

    End Function

    ''' <summary>
    ''' trata campos data. Verifica se a data informada é válida
    ''' </summary>
    ''' <param name="Controle"></param>
    ''' <param name="Validator"></param>
    ''' <param name="SetarFocoControle"></param>
    ''' <param name="FocoSetado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  30/12/2010  criado
    ''' </history>
    Public Function TratarDataInvalida(Controle As TextBox, ByRef Validator As CustomValidator,
                                       SetarFocoControle As Boolean, ByRef FocoSetado As Boolean) As String

        Dim strErro As String = String.Empty

        Dim bolDataInvalida As Boolean = False

        If Not IsDate(Controle.Text) Then

            ' se o valor do textbox não for uma data válida:

            strErro = Validator.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak
            Validator.IsValid = False

            'Seta o foco no primeiro controle que deu erro
            If SetarFocoControle AndAlso Not FocoSetado Then

                Controle.Focus()
                FocoSetado = True

            End If

        Else

            Validator.IsValid = True

        End If

        Return strErro

    End Function

    <WebMethod()>
    Public Shared Function GetDateServerUTC() As String
        Return Date.UtcNow.ToString("yyyy, MM, dd, HH, mm, ss")
    End Function

    Public Function RecuperarValorDic(chave) As String
        Try
            If dicionario IsNot Nothing AndAlso dicionario.Count > 0 Then

                If Not String.IsNullOrWhiteSpace(Me.CodFuncionalidad) AndAlso dicionario.ContainsKey(Me.CodFuncionalidad) Then
                    Dim chavesDic = dicionario(Me.CodFuncionalidad)
                    If chavesDic IsNot Nothing AndAlso chavesDic.ContainsKey(chave) Then
                        Return chavesDic(chave)
                    End If
                End If

                If (Not String.IsNullOrWhiteSpace(Me.CodFuncionalidadGenerica) AndAlso dicionario.ContainsKey(Me.CodFuncionalidadGenerica)) Then
                    Dim chavesDic = dicionario(Me.CodFuncionalidadGenerica)
                    If chavesDic IsNot Nothing AndAlso chavesDic.ContainsKey(chave) Then
                        Return chavesDic(chave)
                    End If
                End If

            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try

        Return chave
    End Function

    Public Function RecuperarChavesDic() As Comon.SerializableDictionary(Of String, String)
        Try
            If dicionario IsNot Nothing AndAlso dicionario.Count > 0 AndAlso (dicionario.ContainsKey(Me.CodFuncionalidad) OrElse dicionario.ContainsKey(Me.CodFuncionalidadGenerica)) Then
                If dicionario.ContainsKey(Me.CodFuncionalidad) Then
                    Return dicionario(Me.CodFuncionalidad)
                ElseIf dicionario.ContainsKey(Me.CodFuncionalidadGenerica) Then
                    Return dicionario(Me.CodFuncionalidadGenerica)
                End If
            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try

        Return Nothing
    End Function
    Public Sub CarregaDicinario()

        CarregaChavesDicinario(Me.CodFuncionalidadGenerica)
        CarregaChavesDicinario(Me.CodFuncionalidad)

    End Sub

    Private Sub CarregaChavesDicinario(CodigoFuncionalidad As String)
        If Not String.IsNullOrEmpty(CodigoFuncionalidad) Then
            If dicionario Is Nothing Then
                dicionario = New Comon.SerializableDictionary(Of String, Comon.SerializableDictionary(Of String, String))
            End If

            'Se já tiver carregado os dicionarios da funcionalidade nao carrega novamente
            If dicionario.ContainsKey(CodigoFuncionalidad) AndAlso dicionario(CodigoFuncionalidad).Values IsNot Nothing AndAlso dicionario(CodigoFuncionalidad).Values.Count > 0 Then
                Exit Sub
            End If

            Dim codigoCultura As String = If(CulturaSistema IsNot Nothing AndAlso
                                                                             Not String.IsNullOrEmpty(CulturaSistema.Name),
                                                                             CulturaSistema.Name,
                                                                             If(CulturaPadrao IsNot Nothing, CulturaPadrao.Name, Globalization.CultureInfo.CurrentCulture.ToString()))

            Dim peticion As New Prosegur.Genesis.ContractoServicio.Contractos.Comon.Diccionario.ObtenerValoresDiccionario.Peticion
            With peticion
                .CodigoFuncionalidad = CodigoFuncionalidad
                .Cultura = codigoCultura
            End With
            Dim respuesta = Prosegur.Genesis.LogicaNegocio.Genesis.Diccionario.ObtenerValoresDicionario(peticion)

            If dicionario.ContainsKey(CodigoFuncionalidad) Then
                dicionario(CodigoFuncionalidad) = respuesta.Valores
            Else
                dicionario.Add(CodigoFuncionalidad, respuesta.Valores)
            End If
        End If
    End Sub

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

#End Region

#Region "Metodos ExibirMensagens"
    Public Sub MostraMensagemErro(erro As String, script As String)
        erro = erro.Replace(vbCrLf, "<br />")
        erro = erro.Replace(vbCr, "<br />")
        erro = erro.Replace(vbLf, "<br />")
        erro = erro.Replace("'", "&#39;")

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro",
                                                   CriarChamadaMensagemErro(erro, script) _
                                                       , True)
    End Sub

    Public Sub MostraMensagem(erro As String)
        MostraMensagemErro(erro, String.Empty)
    End Sub

    Public Shared Sub TratarErroBugsnag(e As Exception)

        If (Not (TypeOf e Is Excepcion.NegocioExcepcion)) Then
            Prosegur.BugsnagHelper.NotifyIfEnabled(e)
        End If

    End Sub


    Public Sub MostraMensagemExcecao(exception As Exception)

        TratarErroBugsnag(exception)
        ' logar erro no banco
        Utilidad.LogarErroAplicacao(Nothing, exception.ToString(), String.Empty, LoginUsuario, DelegacionConectada.Keys(0))
        MostraMensagemErro(exception.ToString(), String.Empty)
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
            Return String.Format("ExibirMensagem2('{0}','{1}', '{2}' , '{3}');",
                                                               erro,
                                                                Traduzir("aplicacao"), script, Traduzir("btnFechar"))
        Else
            Return String.Format("ExibirMensagem('{0}','{1}', '{2}' , '{3}');",
                                                                           erro,
                                                                            Traduzir("aplicacao"), script, Traduzir("btnFechar"))
        End If

    End Function

    Public Shared Function CriarChamadaMensagemSimNao(erro As String, acaoSim As String) As String
        erro = erro.Replace(vbCrLf, "<br />")
        erro = erro.Replace(vbCr, "<br />")
        erro = erro.Replace(vbLf, "<br />")

        Return String.Format("ExibirMensagemSimNao('{0}','{1}', '{2}' , '{3}', '{4}');",
                                                                erro,
                                                                 Traduzir("aplicacao"), acaoSim, Traduzir("gen_opcion_si"), Traduzir("gen_opcion_no"))
    End Function
    Public Shared Function CriarChamadaMensagemNaoSim(erro As String, acaoSim As String, acaoNao As String) As String
        erro = erro.Replace(vbCrLf, "<br />")
        erro = erro.Replace(vbCr, "<br />")
        erro = erro.Replace(vbLf, "<br />")

        Return String.Format("ExibirMensagemNaoSim('{0}','{1}', '{2}' , '{3}', '{4}', '{5}');",
                                                                erro,
                                                                 Traduzir("aplicacao"), acaoSim, acaoNao, Traduzir("gen_opcion_si"), Traduzir("gen_opcion_no"))
    End Function
    Public Sub ExibirMensagemNaoSim(erro As String, acaoSim As String, acaoNao As String)
        erro = erro.Replace(vbCrLf, "<br />")
        erro = erro.Replace(vbCr, "<br />")
        erro = erro.Replace(vbLf, "<br />")
        erro = erro.Replace("'", "&#39;")

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro",
                                                   CriarChamadaMensagemNaoSim(erro, acaoSim, acaoNao) _
                                                       , True)
    End Sub

    Public Sub ExibirMensagemSimNao(erro As String, acaoSim As String)
        erro = erro.Replace(vbCrLf, "<br />")
        erro = erro.Replace(vbCr, "<br />")
        erro = erro.Replace(vbLf, "<br />")
        erro = erro.Replace("'", "&#39;")

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro",
                                                   CriarChamadaMensagemSimNao(erro, acaoSim) _
                                                       , True)
    End Sub

#End Region
    Public Shared Sub CopyProperties(src As Object, dest As Object)
        For Each item As PropertyDescriptor In TypeDescriptor.GetProperties(src)
            item.SetValue(dest, item.GetValue(src))
        Next
    End Sub

End Class