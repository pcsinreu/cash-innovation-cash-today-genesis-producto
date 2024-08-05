Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Partial Public Class Erro
    Inherits System.Web.UI.UserControl

#Region "Propriedades"

    Dim _Traduzir As Boolean = True
    Dim _Error As String
    Dim _ErroTraduzido As String
    Dim _ExibirUltimoErro As Boolean = False

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
    ''' [leandro.pedrosa] 01/04/2009 Criado
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
    ''' [leandro.pedrosa] 01/04/2009 Criado
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
    ''' Retorna o id do primeiro controle habilitado a receber o foco
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [blcosta] 23/06/2010 Criado
    ''' </history>
    Public ReadOnly Property IdPrimeiroControle() As String
        Get
            Return lkbExibeDetalhes.ClientID
        End Get
    End Property

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Registra os javascripts para o controle
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [blcosta]  22/06/2010  criado
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
    ''' [blcosta]  22/06/2010  criado
    ''' </history>
    Private Sub TraduzirControles()

        lkbExibeDetalhes.Text = Traduzir("btnDetalhes")
        lkbCopiar.Text = Traduzir("btnCopiar")

    End Sub

    ''' <summary>
    ''' Configura estado inicial dos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [blcosta]  22/06/2010  criado
    ''' </history>
    Private Sub ConfigurarEstadoInicial()

        trLkbDetalhes.Visible = False

    End Sub

    ''' <summary>
    ''' Configura controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [blcosta]  22/06/2010  criado
    ''' </history>
    Private Sub ConfigurarControles()

        lkbCopiar.OnClientClick = "return Copiar('" & txtErro.ClientID & "');"

    End Sub

    Public Function VerificaErro(CodErro As Integer, _
                                 NombreServidorBD As String, _
                                 Optional MsgError As String = "", _
                                 Optional showError As Boolean = True, _
                                 Optional objtraduzir As Boolean = True) As Boolean

        Dim errMsg As String = String.Empty
        Dim status As Boolean = False

        ' tratar retorno do serviço
        status = Aplicacao.Util.Utilidad.TratarRetornoServico(CodErro, MsgError, errMsg, objtraduzir, LoginUsuario, DelegacionConectada.Keys(0))

        'Exibe a mensagem de erro na renderização do controle 
        If showError Then

            _Traduzir = objtraduzir
            ErrorMessage = Aplicacao.Util.Utilidad.RetornarMensagemErro(MsgError, errMsg, NombreServidorBD, True)

            ' verifica se a exibição dos detalhes da exceção está habilitada
            If ExibeDetalhesExcecao AndAlso CodErro = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT Then

                ' configura label do link detalhes
                lkbExibeDetalhes.Text = String.Format("{0} {1}", "+", traduzir("btnDetalhes"))

                ' quando estiver exibindo erros gerado por exceção, mostra link Detalhes
                trLkbDetalhes.Visible = True

                ' configura mensagem da exceção
                txtErro.Text = MsgError

            End If

        End If

        Return status

    End Function
    Public Function VerificaErro2(CodErro As Integer, _
                                NombreServidorBD As String, _
                                ByRef MsgExibirAlerta As String, _
                                Optional MsgError As String = "", _
                                Optional showError As Boolean = True, _
                                Optional objtraduzir As Boolean = True) As Boolean

        Dim errMsg As String = String.Empty
        Dim status As Boolean = False

        ' tratar retorno do serviço
        status = Aplicacao.Util.Utilidad.TratarRetornoServico(CodErro, MsgError, errMsg, objtraduzir, LoginUsuario, DelegacionConectada.Keys(0))

        'Exibe a mensagem de erro na renderização do controle 
        If showError Then

            _Traduzir = objtraduzir
            ErrorMessage = Aplicacao.Util.Utilidad.RetornarMensagemErro(MsgError, errMsg, NombreServidorBD, True)
            MsgExibirAlerta = ErrorMessage
            ' verifica se a exibição dos detalhes da exceção está habilitada
            If ExibeDetalhesExcecao AndAlso CodErro = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT Then

                ' configura label do link detalhes
                lkbExibeDetalhes.Text = String.Format("{0} {1}", "+", Traduzir("btnDetalhes"))

                ' quando estiver exibindo erros gerado por exceção, mostra link Detalhes
                trLkbDetalhes.Visible = True

                ' configura mensagem da exceção
                txtErro.Text = MsgError

            End If

        End If

        Return status

    End Function

    ''' <summary>
    ''' TratarErroException
    ''' </summary>
    ''' <param name="Ex"></param>
    ''' <remarks></remarks>
    ''' <history>[pda] 08/06/2009 - Alterado</history>
    Public Sub TratarErroException(Ex As Exception)

        Base.TratarErroBugsnag(Ex)
        'If Not TypeOf Ex Is System.Threading.ThreadAbortException Then

        ' logar erro no banco
        Aplicacao.Util.Utilidad.LogarErroAplicacao(Nothing, Ex.ToString, String.Empty, LoginUsuario, DelegacionConectada.Keys(0))

        ' exibir mensagem para o usuario
        ShowError(Aplicacao.Util.Utilidad.RetornarMensagemErro(Ex.ToString, "err_padrao_aplicacao"))

        ' verifica se a exibição dos detalhes da exceção está habilitada
        If ExibeDetalhesExcecao Then

            ' configura label do link detalhes
            lkbExibeDetalhes.Text = String.Format("{0} {1}", "+", Traduzir("btnDetalhes"))

            ' quando estiver exibindo erros gerado por exceção, mostra link Detalhes
            trLkbDetalhes.Visible = True

            ' configura mensagem da exceção
            txtErro.Text = Ex.ToString()

        End If

        'End If

    End Sub

    Public Sub ShowError(MsgError As String)

        ErrorMessage = MsgError
        LastErrorShow = ErrorMessage

        tblError.Visible = True

    End Sub

    Public Sub ShowError(MsgError As String, objtraduzir As Boolean)

        _Traduzir = objtraduzir
        ErrorMessage = MsgError

        If objtraduzir Then
            LastErrorShow = Traduzir(MsgError)
        Else
            LastErrorShow = ErrorMessage
        End If

        tblError.Visible = True

    End Sub

#End Region

#Region "Eventos"

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        RegistrarJavascripts()

        If Not Page.IsPostBack Then

            TraduzirControles()

        End If

        ConfigurarEstadoInicial()

    End Sub

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender


        If Not Page.IsPostBack Then

            ConfigurarControles()

        End If

        'Caso exista alguma mensagem a ser exibida, o erro será exibido
        If ErrorMessage <> String.Empty Then
            lblError.Text = ErrorMessage
            ' imgErro.Visible = True
        ElseIf ExibirUltimoErro Then
            lblError.Text = LastErrorShow
            ' imgErro.Visible = True
        Else
            lblError.Text = String.Empty
            ' imgErro.Visible = False
            trLkbDetalhes.Visible = False
        End If

    End Sub

#End Region

End Class