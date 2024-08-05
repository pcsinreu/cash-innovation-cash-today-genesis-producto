Imports Prosegur.Framework.Dicionario.Tradutor
Imports AjaxPro
Imports System.Reflection
Imports System.ComponentModel
Imports Prosegur.Genesis.Comon.Clases
Imports System.Web.Services
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.LogicaNegocio.Genesis
Imports Prosegur.Genesis

Partial Public MustInherit Class Base
    Inherits BaseCentralNotificacion
    Implements IPostBackEventHandler

#Region "[VARIÁVEIS]"

    Private _ValidacaoAcesso As ValidacaoAcesso = Nothing
    Protected _DecimalSeparador As String = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator
    Protected _MilharSeparador As String = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator

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
    Public Shared Property InformacionUsuario() As InformacionUsuario
        Get
            Return SessionManager.InformacionUsuario
        End Get
        Set(value As InformacionUsuario)
            SessionManager.InformacionUsuario = value
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
            If value <> Aplicacao.Util.Utilidad.eTelas.RESULTADO_CERTIFICADO_CONSULTAR Then
                Session("FiltroCertificado") = Nothing
            End If
            ViewState("BasePaginaAtual") = value
        End Set
    End Property

    Public Property ClientIDControleFoco As String
        Get
            Return ViewState("CONTROLE_FOCO")
        End Get
        Set(value As String)
            ViewState("CONTROLE_FOCO") = value
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

#End Region

#Region "[EVENTOS]"

    <System.Web.Services.WebMethod()>
    Public Shared Sub PingCache(clave As String)

        Dim temp = HttpRuntime.Cache.Get(clave)
        temp = Nothing

    End Sub

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
        Return Constantes.NOME_PAGINA_LOGIN & "?SesionExpirada=1"
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

        AjaxPro.Utility.RegisterTypeForAjax(GetType(Base))

        Try
            Aplicacao.Util.Utilidad.ZerarTabIndex()
            If Not Page.IsPostBack Then

                ' configura parametros da base antes de iniciar as validações e iniciar a página filha.
                DefinirParametrosBase()

                CarregaChavesDicinario(Me.CodFuncionalidadGenerica)
                CarregaChavesDicinario(Me.CodFuncionalidad)

            End If

            If Me.ValidarAcesso Then
                ' criar objeto validação de acesso
                _ValidacaoAcesso = New ValidacaoAcesso(Me.ValidarAcesso,
                                                       Me.PaginaAtual,
                                                       InformacionUsuario)
                '' validar acesso à página
                Dim ValAcessoPagina As String = _ValidacaoAcesso.ValidarAcessoPagina()
                If Not String.IsNullOrEmpty(ValAcessoPagina) Then
                    If ValAcessoPagina.Contains("?SesionExpirada=1") Then
                        If Not String.IsNullOrEmpty(Request.QueryString("ClaveDocumento")) Then
                            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "cerrarPopup", "window.returnValue = 'CerrarPopup|SesionExpirada|" & Page.ResolveUrl(Constantes.NOME_PAGINA_LOGIN) & "'; window.close();", True)
                            Exit Sub
                            'Response.End()
                        End If
                    End If
                    If Me.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.SALDOS_CONSULTAR_TRANSACCIONES Then
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "cerrarPopup", "Alert('" + Traduzir("msg_sessao_finalizada") + "'); window.close();", True)

                    End If
                    'If Request.QueryString("esModal") IsNot Nothing AndAlso Request.QueryString("esModal") = "True" Then
                    '    ' fechar janela
                    '    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ClienteOk", "window.returnValue=0;window.close();", True)
                    'Else
                    Try

                        Response.Redireccionar(ValAcessoPagina)
                    Catch ex As Exception
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "cerrarPopup", "Alert('" + Traduzir("msg_sessao_finalizada") + "'); window.close();", True)
                    End Try
                    Exit Sub
                    'End If
                End If
            End If


            '' validar ação na página
            'Dim ValAcaoPagina As String = _ValidacaoAcesso.ValidarAcaoPagina()
            'If Not String.IsNullOrEmpty(ValAcaoPagina) Then
            '    Response.Redirect(ValAcaoPagina, False)
            '    Exit Sub
            'End If

            'Seta configuração do Response para não armazenar a página em cache.
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            ' Caso a Session tenha expirado não executa o Inicializar
            'If Not ValAcessoPagina.Contains("SesionExpirada=1") Then
            ' inicializar página filha
            Inicializar()

            'End If

            ' traduzir controles da tela
            TraduzirControles()

            If InformacionUsuario IsNot Nothing AndAlso InformacionUsuario.DelegacionSeleccionada IsNot Nothing Then

                '
                Dim data = Date.UtcNow.AddMinutes(InformacionUsuario.DelegacionSeleccionada.HusoHorarioEnMinutos)
                If InformacionUsuario.DelegacionSeleccionada.AjusteHorarioVerano > 0 AndAlso
                    data >= InformacionUsuario.DelegacionSeleccionada.FechaHoraVeranoInicio AndAlso
                    data < InformacionUsuario.DelegacionSeleccionada.FechaHoraVeranoFin.AddMinutes(InformacionUsuario.DelegacionSeleccionada.AjusteHorarioVerano) Then

                    'ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "SetGMT", "var _GMT = " & InformacionUsuario.GMT & "; var _AjusteVerano = " & InformacionUsuario.GetAjusteVerano & ";", True)
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "SetGMT", "var _GMT = " & InformacionUsuario.DelegacionSeleccionada.HusoHorarioEnMinutos & "; var _AjusteVerano = " & InformacionUsuario.DelegacionSeleccionada.AjusteHorarioVerano & ";", True)
                Else
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "SetGMT", "var _GMT = " & InformacionUsuario.DelegacionSeleccionada.HusoHorarioEnMinutos & "; var _AjusteVerano = 0;", True)
                End If

            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "SetGMT", "var _GMT = 0; var _AjusteVerano = 0;", True)
            End If

            'Registra variaveis de tradução que são usadas no calendario
            'Dim imgUrl As String = ResolveUrl("~/App_Themes/Padrao/css/img/button/Calendar.png")
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

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "SeparadoresNumericos",
                              String.Format("var _DecimalSeparador = '{0}';" &
                                            "var _MilharSeparador = '{1}';",
                                            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator,
                                            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator), True)

        Catch ex As Exception
            Aplicacao.Util.Utilidad.LogarErroAplicacao(Nothing, ex.ToString, String.Empty, String.Empty, String.Empty)
            Prosegur.BugsnagHelper.NotifyIfEnabled(ex)
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
            If Me.ValidarAcesso Then
                _ValidacaoAcesso.ValidarControlesPagina()
            End If

            '' configurar tabindex
            'ConfigurarTabIndex()
        Catch ex As Exception
            Prosegur.BugsnagHelper.NotifyIfEnabled(ex)
            Aplicacao.Util.Utilidad.LogarErroAplicacao(Nothing, ex.ToString, String.Empty, String.Empty, String.Empty)
        End Try

    End Sub


    Public Function ValidarAcaoPagina(acao As Aplicacao.Util.Utilidad.eAcao) As Boolean

        ' criar objeto validação de acesso
        _ValidacaoAcesso = New ValidacaoAcesso(False,
                                               PaginaAtual,
                                               InformacionUsuario)

        Return String.IsNullOrEmpty(_ValidacaoAcesso.ValidarAcaoPagina(acao))


    End Function

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

#Region "[MÉTODOS]"
    Protected Overrides Sub InitializeCulture()
        Dim langs = HttpContext.Current.Request.UserLanguages
        If langs.Count > 0 Then
            Dim lang = langs(0)

            If (Not String.IsNullOrEmpty(lang)) Then
                Culture = lang
                UICulture = lang
            End If
        End If
    End Sub

    <WebMethod()>
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
                                               Me.PaginaAtual,
                                               InformacionUsuario)

        If String.IsNullOrEmpty(_ValidacaoAcesso.ValidarPermissao(Permissao)) Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Shared Function PossuiPermissao(ParamArray PaginaAtual() As Aplicacao.Util.Utilidad.eTelas) As Boolean
        Dim retorno As Boolean = False
        ' Dim permisosSector As New List(Of String) = InformacionUsuario.Permisos

        'If Not String.IsNullOrEmpty(InformacionUsuario.SectorSeleccionado.Identificador) Then
        '    Dim objTipoSector = InformacionUsuario.TipoSector.FirstOrDefault(Function(x) InformacionUsuario.SectorSeleccionado.TipoSector IsNot Nothing AndAlso x.Codigo = InformacionUsuario.SectorSeleccionado.TipoSector.Codigo)

        '    If objTipoSector IsNot Nothing Then
        '        permisosSector = objTipoSector.Permisos
        '    End If
        'End If

        Array.ForEach(Of Aplicacao.Util.Utilidad.eTelas)(PaginaAtual, Sub(p) retorno = retorno OrElse InformacionUsuario.Permisos.Contains(p.ToString()))

        Return retorno
    End Function

    ' ''' <summary>
    ' ''' Retorna o Id do controle que receberá o foco
    ' ''' </summary>
    ' ''' <param name="Controle"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Private Shared Function RetornarIdControleFoco(Controle As Control) As String

    '    ' Verifica se o tipo do controle é um PlaceHolder
    '    If TypeOf Controle Is UI.WebControls.PlaceHolder Then
    '        ' Retorna o Id do primeiro controle do Place Holder
    '        RetornarIdControleFoco(Controle.Controls(0))
    '        ' Se o controle é um botão
    '    ElseIf TypeOf Controle Is Prosegur.Web.Botao Then
    '        ' Retorna o Id do controle concatenado com "_img"
    '        Return (Controle.ID & "_img")

    '    Else
    '        ' Retorna o Id do controle
    '        Return (Controle.ClientID)

    '    End If

    '    Return String.Empty

    'End Function

    ' ''' <summary>
    ' ''' Define para qual controle o foco deve ir quando o último controle perder o foco
    ' ''' </summary>
    ' ''' <param name="UltimoControle"></param>
    ' ''' <param name="PrimeiroControle"></param>
    ' ''' <remarks></remarks>
    'Public Sub DefinirRetornoFoco(UltimoControle As Control, PrimeiroControle As Control, Optional TelaLogin As Boolean = False)

    '    ' Recupera o client id do primeiro controle (para o qual o foco deve ser retornado)
    '    Dim IdControleRetorno As String = RetornarIdControleFoco(PrimeiroControle)

    '    ' Se o controle é um menu
    '    If (TypeOf PrimeiroControle Is WebControls.Menu) Then
    '        ' Define o nome do primeiro nodo
    '        IdControleRetorno &= "n0"
    '    End If

    '    ' Cria o script para colocar o foco no primeiro controle
    '    Dim ScriptFoco As New StringBuilder

    '    If TelaLogin Then
    '        ' se for a tela login, não terá menu. Após o último controle deverá selecionar o link "detalhes" (se estiver visível na tela)
    '        With ScriptFoco
    '            .AppendLine("if (document.getElementById(idControleErro) == undefined){")
    '            .AppendLine("document.getElementById('" & IdControleRetorno & "').focus();")
    '            .AppendLine("}")
    '            .AppendLine("else{")
    '            .AppendLine("document.getElementById(idControleErro).focus();")
    '            .AppendLine("}")
    '        End With
    '    Else
    '        With ScriptFoco
    '            .AppendLine("document.getElementById('" & IdControleRetorno & "').focus();")
    '        End With
    '    End If

    '    ' Dependendo do tipo de controle seta o atributo para retornar o foco ao 1o controle quando perder o foco

    '    If TypeOf UltimoControle Is Prosegur.Web.Botao Then

    '        CType(UltimoControle, Prosegur.Web.Botao).AdicionarAtributoIcone("onfocusout", ScriptFoco.ToString())

    '    ElseIf TypeOf UltimoControle Is WebControls.TextBox Then

    '        CType(UltimoControle, WebControls.TextBox).Attributes.Add("onblur", ScriptFoco.ToString())

    '    ElseIf TypeOf UltimoControle Is WebControls.LinkButton Then

    '        CType(UltimoControle, WebControls.LinkButton).Attributes.Add("onblur", ScriptFoco.ToString())

    '    End If

    'End Sub

    ' ''' <summary>
    ' ''' trata campos obrigatórios
    ' ''' </summary>
    ' ''' <param name="Controle"></param>
    ' ''' <param name="Validator"></param>
    ' ''' <param name="SetarFocoControle"></param>
    ' ''' <param name="FocoSetado"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    ' ''' <history>
    ' ''' [bruno.costa]  30/12/2010  criado
    ' ''' </history>
    'Public Function TratarCampoObrigatorio(Controle As Object, ByRef Validator As CustomValidator, _
    '                                       SetarFocoControle As Boolean, ByRef FocoSetado As Boolean) As String

    '    Dim strErro As String = String.Empty
    '    Dim bolVazio As Boolean = False

    '    ' verifica se o controle está vazio de acordo com o seu tipo
    '    If TypeOf Controle Is DropDownList Then

    '        If String.IsNullOrEmpty(CType(Controle, DropDownList).SelectedValue) Then
    '            bolVazio = True
    '        End If

    '    ElseIf TypeOf Controle Is TreeView Then

    '        If CType(Controle, TreeView).Nodes.Count = 0 Then
    '            bolVazio = True
    '        End If

    '    ElseIf TypeOf Controle Is TextBox Then

    '        If String.IsNullOrEmpty(CType(Controle, TextBox).Text.Trim()) Then
    '            bolVazio = True
    '        End If

    '    ElseIf TypeOf Controle Is Helper Then

    '        If String.IsNullOrEmpty(CType(Controle, Helper).TextTextBox.Trim()) Then
    '            bolVazio = True
    '        End If

    '    ElseIf TypeOf Controle Is ListBox Then

    '        If (CType(Controle, ListBox)).Items.Count = 0 Then
    '            bolVazio = True
    '        End If

    '    End If

    '    If bolVazio Then

    '        strErro = Validator.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak
    '        Validator.IsValid = False

    '        'Seta o foco no primeiro controle que deu erro
    '        If SetarFocoControle AndAlso Not FocoSetado Then

    '            If TypeOf Controle Is Helper Then
    '                CType(Controle, Helper).BtnHelperFocus()
    '            Else
    '                Controle.Focus()
    '            End If

    '            FocoSetado = True

    '        End If

    '    Else

    '        Validator.IsValid = True

    '    End If

    '    Return strErro

    'End Function

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


    ''' <summary>
    ''' Mostra Mensagem de Erro na tela
    ''' </summary>
    ''' <param name="erro"></param>
    ''' <remarks></remarks>
    ''' <history>[adans.klevanskis] 05/06/2013 - criado</history>
    Public Sub MostraMensagemErro(erro As String, script As String)
        erro = erro.Replace(vbCrLf, "<br />")
        erro = erro.Replace(vbCr, "<br />")
        erro = erro.Replace(vbLf, "<br />")
        erro = erro.Replace("'", "&#39;")

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro",
                                                   Aplicacao.Util.Utilidad.CriarChamadaMensagemErro(erro, script) _
                                                       , True)
    End Sub

    Public Sub MostraMensagemErro(erro As String)
        MostraMensagemErro(erro, String.Empty)
        If Base.InformacionUsuario IsNot Nothing AndAlso Base.InformacionUsuario.DelegacionSeleccionada IsNot Nothing Then
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.GuardarLogExecucao(erro,
                                                                          Base.InformacionUsuario.Nombre,
                                                                          Base.InformacionUsuario.DelegacionSeleccionada.Codigo,
                                                                          Genesis.Comon.Enumeradores.Aplicacion.GenesisNuevoSaldos)
        Else
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.GuardarLogExecucao(erro,
                                                                          Base.InformacionUsuario.Nombre,
                                                                          Nothing,
                                                                          Genesis.Comon.Enumeradores.Aplicacion.GenesisNuevoSaldos)
        End If
    End Sub

    Public Sub MostraMensagemErroConScript(erro As String, Optional script As String = "")
        MostraMensagemErro(erro, script)
        If Base.InformacionUsuario IsNot Nothing AndAlso Base.InformacionUsuario.DelegacionSeleccionada IsNot Nothing Then
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.GuardarLogExecucao(erro,
                                                                          Base.InformacionUsuario.Nombre,
                                                                          Base.InformacionUsuario.DelegacionSeleccionada.Codigo,
                                                                          Genesis.Comon.Enumeradores.Aplicacion.GenesisNuevoSaldos)
        Else
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.GuardarLogExecucao(erro,
                                                                          Base.InformacionUsuario.Nombre,
                                                                          Nothing,
                                                                          Genesis.Comon.Enumeradores.Aplicacion.GenesisNuevoSaldos)
        End If
    End Sub

    ''' <summary>
    ''' Verifica se codigo da Mensagem de Erro possui erros
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <remarks></remarks>
    ''' <history>[adans.klevanskis] 05/06/2013 - criado</history>
    Public Function VerificaRespuestaSemErro(codigo As Integer) As Boolean
        If codigo = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
            Return True
        End If
        Return False
    End Function

    'Public Overrides Sub Focus()
    '    MyBase.Focus()

    '    Me.SetarFoco(Me.Controls)
    'End Sub

    Private controleWeb As WebControl
    'Protected Sub SetarFoco(controles As ControlCollection)
    '    For Each controle As Control In controles
    '        If (controle.HasControls()) Then
    '            Me.SetarFoco(controle.Controls)
    '        End If

    '        If (TypeOf (controle) Is WebControl) Then
    '            If (DirectCast(controle, WebControl).TabIndex <> 0 AndAlso DirectCast(controle, WebControl).Enabled AndAlso DirectCast(controle, WebControl).Visible) Then
    '                If (controleWeb Is Nothing) Then
    '                    controleWeb = DirectCast(controle, WebControl)
    '                End If

    '                If (DirectCast(controle, WebControl).TabIndex <= controleWeb.TabIndex) Then
    '                    controleWeb = DirectCast(controle, WebControl)
    '                    controleWeb.Focus()
    '                End If
    '            End If
    '        End If
    '    Next
    'End Sub

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

    ' ''' <summary>
    ' ''' Método que permite o desenvolvedor configurar os tab index dos controles da tela.
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Protected Overridable Sub ConfigurarTabIndex()

    'End Sub

#End Region




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
    Public Sub MostraMensagemExcecao(exception As Exception)
        Prosegur.BugsnagHelper.NotifyIfEnabled(exception)
        ' logar erro no banco
        'Utilidad.LogarErroAplicacao(Nothing, exception.ToString(), String.Empty, LoginUsuario, DelegacionConectada.Keys(0))
        MostraMensagemErro(exception.ToString(), String.Empty)
    End Sub

    ''' <summary>
    ''' Registra no cache o objeto informado, adicionado um timeout em slide de 10 minutos.
    ''' </summary>
    ''' <param name="objeto">Objeto a ser inserido no cache</param>
    ''' <returns>Identificado do cache inserido</returns>
    ''' <remarks></remarks>
    Public Function RegistrarCache(objeto As Object) As String

        Dim clave As String = Guid.NewGuid().ToString("N")

        HttpRuntime.Cache.Insert(clave, objeto, Nothing, Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(10000), CacheItemPriority.Default, Nothing)

        Return clave

    End Function

    ''' <summary>
    ''' Atualiza o cache pela chave com o valor informado
    ''' </summary>
    ''' <param name="clave">Chave do cache</param>
    ''' <param name="objeto">Novo objeto a ser atualizado</param>
    ''' <remarks></remarks>
    Public Sub ActualizarCache(clave As String, objeto As Object)

        HttpRuntime.Cache(clave) = objeto

    End Sub

    ''' <summary>
    ''' Retorna o objeto do cache identificado pela chave.
    ''' </summary>
    ''' <param name="clave">Chave do objeto inserido anteriormente no cache.</param>
    ''' <returns>Objeto do cache</returns>
    ''' <remarks>Ao chamar este método a página recebe um script para atualizar o cache a cada 3 minutos para manter o cache vivo na tela em que o mesmo esta sendo utilizado. Ao fechar a tela o script não é executado mais, e o cache expira em seu tempo padrão de 10 minutos.</remarks>
    Protected Function ObtenerCache(clave As String) As Object

        Dim objeto As Object = HttpRuntime.Cache(clave)

        If objeto IsNot Nothing Then

            Dim script = "var interval_{0} = null;" &
                        "interval_{0} = window.setInterval(function () {{" &
                        "PageMethods.PingCache('{0}');" &
                        "}}, 3 * 60000);"

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "cachePing_" & clave, String.Format(script, clave), True)

        End If

        Return objeto

    End Function

    ''' <summary>
    ''' Retorna o objeto do cache identificado pela chave e o remove do cache.
    ''' </summary>
    ''' <param name="clave">Chave do objeto inserido anteriormente no cache.</param>
    ''' <returns>Objeto do cache</returns>
    ''' <remarks></remarks>
    Protected Function BorrarCache(clave As String) As Object

        Return HttpRuntime.Cache.Remove(clave)

    End Function

    ''' <summary>
    ''' Evento chamado ao chamar o método CerrarPopup() em um popup que foi aberto pela pagina atual.
    ''' </summary>
    ''' <param name="nombrePopup">Identificador opcional do popup que foi fechado</param>
    ''' <param name="argumento">Argumento opcional para customização do evento.</param>
    Protected Event PopupCerrado(nombrePopup As String, argumento As String)

    ''' <summary>
    ''' Abre um popup na url especificada passando os argumentos informados.
    ''' </summary>
    ''' <param name="url">Url do popup a ser aberto</param>
    ''' <param name="argumentos">Argumentos opcionais para o popup</param>
    ''' <param name="altura">Altura opcional</param>
    ''' <param name="largura">Largura opcional</param>
    ''' <remarks></remarks>
    Public Sub AbrirPopup(url As String, Optional argumentos As String = Nothing, Optional altura As Integer = 600, Optional largura As Integer = 1000)

        Dim script As String = RetornaAbrirPopup(url, argumentos, altura, largura)

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, Guid.NewGuid.ToString(), script, True)

    End Sub
    ''' <summary>
    ''' Retorna script AbrirPopupModal
    ''' </summary>
    ''' <param name="url">The URL.</param>
    ''' <param name="argumentos">The argumentos.</param>
    ''' <param name="altura">The altura.</param>
    ''' <param name="largura">The largura.</param>
    Public Function RetornaAbrirPopup(url As String, Optional argumentos As String = Nothing, Optional altura As Integer = 600, Optional largura As Integer = 1000) As String

        Dim direccion As String = ResolveUrl(url) & "?EsPopup=True" & If(argumentos Is Nothing, Nothing, "&" & argumentos)

        Dim script As String = "$(function(){" & String.Format("AbrirPopupModal('{0}', '{1}', '{2}', {3}, '{4}', {5});", direccion, altura, largura, Boolean.TrueString.ToLower, "nombre_" & Guid.NewGuid.ToString().Replace("-", ""), Boolean.TrueString.ToLower) & "});"

        Return script

    End Function

    ''' <summary>
    ''' Fecha a janela atual chamando o evento PopupCerrado() na página que abriu este popup.
    ''' </summary>
    ''' <param name="nombrePopup">Identificador opcional do popup atual.</param>
    ''' <param name="argumento">Argumento opcional para customização do evento.</param>>
    ''' <remarks></remarks>
    Protected Sub CerrarPopup(Optional nombrePopup As String = Nothing, Optional argumento As String = Nothing)

        ' Argumentos utilizados para identificação deste evento específico no método BaseRaisePostBackEvent
        Dim args = "CerrarPopup|" & nombrePopup & "|" & argumento
        Dim script As String = "window.returnValue = '{0}'; window.close();"
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "cerrarPopup", String.Format(script, args), True)

    End Sub

    ''' <summary>
    ''' Executa tratamento de postbacks customizados, no caso implementado somente o do popup.
    ''' </summary>
    ''' <param name="eventArgument">Argumento do postback.</param>
    ''' <remarks>Este método não deve ser chamado diretamente, é invocado automaticamente a cada postback.</remarks>
    Private Sub BaseRaisePostBackEvent(eventArgument As String) Implements System.Web.UI.IPostBackEventHandler.RaisePostBackEvent

        If Not String.IsNullOrEmpty(eventArgument) Then

            Dim args As String() = eventArgument.Split("|")

            If args.Length > 0 Then

                Select Case args(0)

                    Case "CerrarPopup"

                        If args.Length = 1 Then
                            RaiseEvent PopupCerrado(Nothing, Nothing)

                        ElseIf args.Length = 2 Then
                            RaiseEvent PopupCerrado(args(1), Nothing)

                        ElseIf args.Length = 3 Then
                            RaiseEvent PopupCerrado(args(1), args(2))

                        End If

                End Select

            End If

        End If

    End Sub

    Private Sub TrataFoco()

        If (IsPostBack) Then
            If Request("__LASTFOCUS") IsNot Nothing AndAlso Request("__LASTFOCUS") <> String.Empty Then

                Page.SetFocus(Request("__LASTFOCUS"))

            End If

            If (Me.ClientIDControleFoco IsNot Nothing) Then
                Page.SetFocus(Me.ClientIDControleFoco)
                Me.ClientIDControleFoco = Nothing
            End If
        End If

    End Sub
End Class