Imports Prosegur.Framework.Dicionario.Tradutor
Partial Public Class Erro
    Inherits System.Web.UI.UserControl

#Region "Variaveis"

    Dim _Traduzir As Boolean = True
    Dim _Error As String
    Dim _ErroTraduzido As String
    Dim _ExibirUltimoErro As Boolean = False
    Dim _TipoMensagem As Enumeradores.eMensagem = Enumeradores.eMensagem.Erro
    Dim _ExibeDetalhesExcecaoLoad As Boolean = False
#End Region

#Region "Propriedades"

    Private Property ErrorMessage() As String
        Get
            Return _Error
        End Get
        Set(value As String)

            If _Traduzir Then
                _ErroTraduzido = Traduzir(value)
            Else
                _ErroTraduzido = value
            End If

            If _ErroTraduzido <> String.Empty Then
                _Error = _ErroTraduzido
            Else
                _Error = value
            End If

        End Set
    End Property

    ''' <summary>
    ''' Código da delegação no qual o usuário está logado.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 21/07/2009 Criado
    ''' </history>
    Private ReadOnly Property DelegacionConectada() As Dictionary(Of String, String)
        Get
            Return IIf(Session("BaseDelegacionConectada") Is Nothing, New Dictionary(Of String, String), Session("BaseDelegacionConectada"))
        End Get
    End Property

    ''' <summary>
    ''' Armazena login do usuário logado.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 21/07/2009 Criado
    ''' </history>
    Private ReadOnly Property LoginUsuario() As String
        Get
            Return IIf(Session("BaseLoginUsuario") Is Nothing, String.Empty, Session("BaseLoginUsuario"))
        End Get
    End Property

    ''' <summary>
    ''' Armazena o ultimo erro que foi exibido
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 21/07/2009 Criado
    ''' </history>
    Public Property LastErrorShow() As String
        Get
            Return ViewState("LastErrorShow")
        End Get
        Set(value As String)
            ViewState("LastErrorShow") = value
        End Set
    End Property

    ''' <summary>
    ''' Permite exibir o ultimo erro associado ao controle
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 21/07/2009 Criado
    ''' </history>
    Public Property ExibirUltimoErro() As Boolean
        Get
            Return _ExibirUltimoErro
        End Get
        Set(value As Boolean)
            _ExibirUltimoErro = value
        End Set
    End Property

    ''' <summary>
    ''' Habilita/desabilita a exibição dos detalhes de uma exceção
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [blcosta] 23/06/2010 Criado
    ''' </history>
    Public ReadOnly Property ExibeDetalhesExcecao() As Boolean
        Get

            If Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("EXIBE_EXCECAO") = "1" Then
                Return True
            Else
                Return False
            End If

        End Get
    End Property

    ''' <summary>
    ''' Habilita/desabilita a exibição dos detalhes de uma exceção ao carregar a pagina
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick.santos] 19/07/2011 Criado
    ''' </history>
    Public Property ExibeDetalhesExcecaoLoad() As Boolean
        Get
            Return _ExibeDetalhesExcecaoLoad
        End Get
        Set(value As Boolean)
            _ExibeDetalhesExcecaoLoad = value
        End Set
    End Property

    ''' <summary>
    ''' Retorna o id do primeiro controle habilitado a receber o foco
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [blcosta] 23/06/2010 Criado
    ''' </history>
    Public ReadOnly Property LinkDetalhes() As WebControls.LinkButton
        Get
            Return lkbExibeDetalhes
        End Get
    End Property


    Public ReadOnly Property HayErrors As Boolean
        Get
            Return Not String.IsNullOrEmpty(ErrorMessage)
        End Get
    End Property

#End Region

#Region "Métodos"


    ''' <summary>
    ''' Registra os javascripts para o controle
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [blcosta]  23/06/2010  criado
    ''' </history>
    Private Sub RegistrarJavascripts()

        Dim jsExibirDetalhes As New StringBuilder()

        With jsExibirDetalhes
            .AppendLine("function ExibirDetalhes(control){")
            .AppendLine(" var text = control.innerText;")
            .AppendLine(" text = text.substr(2,text.length -2);")
            .AppendLine(" if (document.getElementById('divTxtErro').style.display == ""inline""){")
            .AppendLine("    document.getElementById('divTxtErro').style.display = ""none"";")
            .AppendLine("    control.innerText = ""+ "" + text;")
            .AppendLine("  }")
            .AppendLine("  else{")
            .AppendLine("    document.getElementById('divTxtErro').style.display = ""inline"";")
            .AppendLine("    control.innerText = ""- "" + text;")
            .AppendLine(" }")
            .AppendLine(" return false; ")
            .AppendLine("}")
        End With

        ' javascript exibir/esconder detalhes da exceção
        Page.ClientScript.RegisterClientScriptBlock(Me.Page.GetType(), "ExibirDetalhes", jsExibirDetalhes.ToString(), True)

        Dim jsCopiar As New StringBuilder()

        With jsCopiar
            .AppendLine("function Copiar(controlId){")
            .AppendLine(" var msg = document.getElementById(controlId).value;")
            .AppendLine(" window.clipboardData.setData('Text', msg);")
            .AppendLine(" return false; ")
            .AppendLine("}")
        End With

        ' javascript exibir/esconder detalhes da exceção
        Page.ClientScript.RegisterClientScriptBlock(Me.Page.GetType(), "Copiar", jsCopiar.ToString(), True)

    End Sub

    ''' <summary>
    ''' Traduz texto dos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [blcosta]  23/06/2010  criado
    ''' </history>
    Private Sub TraduzirControles()

        lkbExibeDetalhes.Text = Traduzir("lbl_btnDetalhes")
        lkbCopiar.Text = Traduzir("lbl_btnCopiar")

    End Sub

    ''' <summary>
    ''' Configura estado inicial dos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [blcosta]  23/06/2010  criado
    ''' </history>
    Private Sub ConfigurarEstadoInicial()

        trLkbDetalhes.Visible = False

    End Sub

    ''' <summary>
    ''' Configura controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [blcosta]  23/06/2010  criado
    ''' </history>
    Private Sub ConfigurarControles()

        lkbCopiar.OnClientClick = "return Copiar('" & txtErro.ClientID & "');"

    End Sub

    ''' <summary>
    ''' Verifica o erro que foi gerado
    ''' </summary>
    ''' <param name="CodErro">Código do erro</param>
    ''' <param name="MsgError">Mensagem de erros</param>
    ''' <param name="showError">Define se é para exibir o erro</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 20/07/2009 Criado
    ''' </history>
    Public Function VerificaErro(CodErro As Integer, _
                                 resultadoOperacion As ContractoServ.Login.ResultadoOperacionLoginLocal, _
                                 Optional MsgError As String = "", _
                                 Optional showError As Boolean = True, _
                                 Optional objtraduzir As Boolean = True) As Boolean

        Dim errMsg As String = String.Empty
        Dim status As Boolean = False

        ' tratar retorno do serviço
        status = Util.TratarRetornoServico(CodErro, MsgError, resultadoOperacion, errMsg, objtraduzir, LoginUsuario, DelegacionConectada.Keys(0))

        'Exibe a mensagem de erro na renderização do controle 
        If showError Then
            _Traduzir = objtraduzir
            ErrorMessage = errMsg

            'Se houve retorno de erro do tratamento do serviço
            If Not String.IsNullOrEmpty(ErrorMessage) Then

                ' logar erro no banco
                Util.LogarErroAplicacao(Nothing, MsgError, String.Empty, LoginUsuario, DelegacionConectada.Keys(0))

                ' verifica se a exibição dos detalhes da exceção está habilitada
                If ExibeDetalhesExcecao Then

                    ' configura label do link detalhes
                    lkbExibeDetalhes.Text = String.Format("{0} {1}", "+", traduzir("lbl_btnDetalhes"))

                    ' quando estiver exibindo erros gerado por exceção, mostra link Detalhes
                    trLkbDetalhes.Visible = True

                    ' configura mensagem da exceção
                    txtErro.Text = MsgError

                End If

            End If

        End If

        Return status

    End Function

    Public Function VerificaErro2(CodErro As Integer, _
                             resultadoOperacion As ContractoServ.Login.ResultadoOperacionLoginLocal,
                             ByRef MsgExibirAlerta As String, _
                             Optional MsgError As String = "", _
                             Optional showError As Boolean = True, _
                             Optional objtraduzir As Boolean = True) As Boolean

        MsgExibirAlerta = String.Empty
        Dim errMsg As String = String.Empty
        Dim status As Boolean = False

        ' tratar retorno do serviço
        status = Util.TratarRetornoServico(CodErro, MsgError, resultadoOperacion, errMsg, objtraduzir, LoginUsuario, DelegacionConectada.Keys(0))

        'Exibe a mensagem de erro na renderização do controle 
        If showError Then
            _Traduzir = objtraduzir
            ErrorMessage = errMsg

            'Se houve retorno de erro do tratamento do serviço
            'Se houve retorno de erro do tratamento do serviço
            If Not String.IsNullOrEmpty(ErrorMessage) Then

                ' logar erro no banco
                Util.LogarErroAplicacao(Nothing, MsgError, String.Empty, LoginUsuario, DelegacionConectada.Keys(0))

                ' verifica se a exibição dos detalhes da exceção está habilitada
                ErrorMessage = ErrorMessage.Replace(vbCrLf, "</br>")

                MsgExibirAlerta = ErrorMessage
            End If

        End If

        Return status

    End Function

    ''' <summary>
    ''' TratarErroException
    ''' </summary>
    ''' <param name="Ex"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 20/07/2009 Criado
    ''' </history>
    Public Sub TratarErroException(Ex As Exception)


        If Not TypeOf Ex Is System.Threading.ThreadAbortException Then

            ' logar erro no banco
            Util.LogarErroAplicacao(Nothing, Ex.ToString(), String.Empty, LoginUsuario, DelegacionConectada.Keys(0))

            ' exibir mensagem para o usuario
            ShowError(Util.RetornarMensagemErro(Ex.ToString, "err_padrao_aplicacao"))

            ' verifica se a exibição dos detalhes da exceção está habilitada
            If ExibeDetalhesExcecao Then

                ' configura label do link detalhes
                lkbExibeDetalhes.Text = String.Format("{0} {1}", "+", Traduzir("lbl_btnDetalhes"))

                ' quando estiver exibindo erros gerado por exceção, mostra link Detalhes
                trLkbDetalhes.Visible = True

                ' configura mensagem da exceção
                txtErro.Text = Ex.ToString()

            End If

        End If

    End Sub

    ''' <summary>
    ''' Mostra um lista de mensagem ao usuário
    ''' </summary>
    ''' <param name="LstMsgErros">Lista de mensagens a serem exibidas</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 21/07/2009 Criado
    ''' </history>
    Public Sub ShowError(LstMsgErros As List(Of String))

        ShowError(LstMsgErros, Enumeradores.eMensagem.Erro)

    End Sub

    ''' <summary>
    ''' Mostra um lista de mensagem ao usuário
    ''' </summary>
    ''' <param name="LstMsgErros">Lista de mensagens a serem exibidas</param>
    ''' <param name="TipoMensagem">Tipo da mensagem a ser exibida</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 21/07/2009 Criado
    ''' </history>
    Public Sub ShowError(LstMsgErros As List(Of String), ByRef TipoMensagem As Enumeradores.eMensagem)

        Dim MsgErroRetorno As String = String.Empty

        If (LstMsgErros IsNot Nothing) Then
            For Each MsgErro As String In LstMsgErros
                MsgErroRetorno &= MsgErro & Constantes.LineBreak
            Next
        End If

        ' Define que a tradução não será realizada
        ShowError(MsgErroRetorno, TipoMensagem, False)

    End Sub

    Public Sub AddError(MsgError As String, ByRef TipoMensagem As Enumeradores.eMensagem)
        Dim Erro As New List(Of String)
        Erro.Add(ErrorMessage)
        Erro.Add(MsgError)
        ShowError(Erro, TipoMensagem)
    End Sub

    ''' <summary>
    ''' Mostra a mensagem ao usuário
    ''' </summary>
    ''' <param name="MsgError">Mensagem a ser exibida</param>
    ''' <param name="TipoMensagem">Tipo da Mensagem</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 21/07/2009 Criado
    ''' </history>
    Public Sub ShowError(MsgError As String, TipoMensagem As Enumeradores.eMensagem)

        ' Define o tipo da mensagem
        _TipoMensagem = TipoMensagem

        ' Define a mensagem a ser exibida
        ErrorMessage = MsgError

        ' Define a ultima mensagem exibida
        LastErrorShow = ErrorMessage

        Select Case _TipoMensagem
            Case Enumeradores.eMensagem.Atencao
                imgErro.ImageUrl = "~/Imagenes/warning.jpg"
            Case Enumeradores.eMensagem.Erro
                imgErro.ImageUrl = "~/Imagenes/error.jpg"
            Case Enumeradores.eMensagem.Informacao
                imgErro.ImageUrl = "~/Imagenes/info.jpg"
            Case Enumeradores.eMensagem.Download
                imgErro.ImageUrl = "~/Imagenes/download.jpg"
        End Select

        ' não exibe link "detalhes"
        trLkbDetalhes.Visible = False

    End Sub

    ''' <summary>
    ''' Mostra a mensagem ao usuarário
    ''' </summary>
    ''' <param name="MsgError">Mensagem a ser exibida</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 21/07/2009 Criado
    ''' </history>
    Public Sub ShowError(MsgError As String)

        ' Passa a mensagem e o tipo de mensagem
        ShowError(MsgError, Enumeradores.eMensagem.Erro)

    End Sub

    ''' <summary>
    ''' Mostra a Mensagem ao usuário
    ''' </summary>
    ''' <param name="MsgError">Mensagem a ser exibida</param>
    ''' <param name="Traduzir">Define se é para traduzir a mensagem</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 21/07/2009 Criado 
    ''' </history>
    Public Sub ShowError(MsgError As String, Traduzir As Boolean)

        ' Passa a mensagem e se é para traduzir
        ShowError(MsgError, Enumeradores.eMensagem.Erro, Traduzir)

    End Sub

    ''' <summary>
    ''' Mostra a Mensagem ao usuário
    ''' </summary>
    ''' <param name="MsgError">Mensagem a ser exibida</param>
    ''' <param name="TipoMensagem">Define o tipo da mensagem a ser exibida</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 21/07/2009 Criado 
    ''' </history>
    Public Sub ShowError(MsgError As String, TipoMensagem As Enumeradores.eMensagem, objtraduzir As Boolean)

        ' Define que traduz a mensagem
        _Traduzir = objtraduzir

        ' Traduz a mensagem a ser exibida
        ShowError(MsgError, TipoMensagem)

    End Sub

#End Region

#Region "Eventos"

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        RegistrarJavascripts()

        If Not Page.IsPostBack Then

            TraduzirControles()
            If Not ExibeDetalhesExcecaoLoad Then
                ConfigurarEstadoInicial()
            End If

        End If

    End Sub

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender

        If Not Page.IsPostBack Then
            ConfigurarControles()
        End If

        'Caso exista alguma mensagem a ser exibida, o erro será exibido
        If ErrorMessage <> String.Empty Then
            tbMensagem.Visible = True
            lblError.Text = ErrorMessage
            imgErro.Visible = True
        ElseIf ExibirUltimoErro Then
            tbMensagem.Visible = True
            lblError.Text = LastErrorShow
            imgErro.Visible = True
        Else
            tbMensagem.Visible = False
            lblError.Text = String.Empty
            imgErro.Visible = False
            trLkbDetalhes.Visible = False
        End If

    End Sub

#End Region

End Class