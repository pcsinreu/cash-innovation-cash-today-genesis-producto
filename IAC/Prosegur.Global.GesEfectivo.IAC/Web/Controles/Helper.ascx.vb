Imports Prosegur.Web

''' <summary>
''' Controle helper
''' </summary>
''' <remarks></remarks>
''' <history>
''' [bruno.costa]  05/01/2011  criado
''' </history>
Partial Public Class Helper
    Inherits System.Web.UI.UserControl

#Region "[EVENTOS DO CONTROLE]"

    Public Delegate Sub btnHelperClickDelegate(sender As Object, e As EventArgs)
    Public Event btnHelperClick As btnHelperClickDelegate

    Public Delegate Sub txtHelperTextChangedDelegate(sender As Object, e As EventArgs)
    Public Event txtHelperTextChanged As txtHelperTextChangedDelegate

    Public Delegate Sub LimparControlesDelegate(sender As Object, e As EventArgs)
    Public Event LimparControles As LimparControlesDelegate

#End Region

#Region "[VARIÁVEIS]"

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Texto do textbox
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  05/01/2011  criado
    ''' </history>
    Public Property TextTextBox() As String
        Get
            Return txtHelper.Text
        End Get
        Set(value As String)
            txtHelper.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Tooltip do textbox
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011  criado
    ''' </history>
    Public Property ToolTipTextBox() As String
        Get
            Return txtHelper.ToolTip
        End Get
        Set(value As String)
            txtHelper.ToolTip = value
        End Set
    End Property

    ''' <summary>
    ''' Chave de tradução do botão
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  05/01/2011  criado
    ''' </history>
    Public Property TituloBotao() As String
        Get
            Return btnHelper.Titulo
        End Get
        Set(value As String)
            btnHelper.Titulo = value
        End Set
    End Property

    ''' <summary>
    ''' ClientID do botão
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011  criado
    ''' </history>
    Public ReadOnly Property BtnClientID() As String
        Get
            Return btnHelper.ID & "_img"
        End Get
    End Property

    ''' <summary>
    ''' TabIndex do botão
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011  criado
    ''' </history>
    Public Property BtnTabIndex() As Short
        Get
            Return btnHelper.TabIndex
        End Get
        Set(value As Short)
            btnHelper.TabIndex = value
        End Set
    End Property

    ''' <summary>
    ''' Propriedade habilitado do botão
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011  criado
    ''' </history>
    Public Property BtnHabilitado() As Boolean
        Get
            Return btnHelper.Habilitado
        End Get
        Set(value As Boolean)
            btnHelper.Habilitado = value
        End Set
    End Property

    ''' <summary>
    ''' Custom validator do textbox
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011  criado
    ''' </history>
    Public Property Validator() As CustomValidator
        Get
            Return csvTxtHelper
        End Get
        Set(value As CustomValidator)
            csvTxtHelper = value
        End Set
    End Property

    ''' <summary>
    ''' Nome da session onde o objeto selecionado será armazenado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011  criado
    ''' </history>
    Public Property NomeSessionValor() As String
        Get
            Return hidSessionValor.Value
        End Get
        Set(value As String)
            hidSessionValor.Value = value
        End Set
    End Property

    ''' <summary>
    ''' Nome da session que informa se os dados da tela pai devem ser limpos.
    ''' (Usuário limpou seleção e clicou aceitar)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011  criado
    ''' </history>
    Public Property NomeSessionLimpar() As String
        Get
            Return hidSessionLimpar.Value
        End Get
        Set(value As String)
            hidSessionLimpar.Value = value
        End Set
    End Property

    ''' <summary>
    ''' Retorna objeto selecionado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011  criado
    ''' </history>
    Public Property ValorSelecionado() As Object
        Get
            Return Session(hidSessionValor.Value)
        End Get
        Set(value As Object)
            ' guarda valor selecionado na session
            Session(hidSessionValor.Value) = value
        End Set
    End Property

#End Region

#Region "[CONSTRUTOR]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not Me.Page.IsPostBack Then
            ConfigurarControles()
        Else
            VerificarLimparControlesFormPai()
        End If

    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' configura controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  05/01/2011  criado
    ''' </history>
    Private Sub ConfigurarControles()

        txtHelper.Enabled = False

    End Sub

    ''' <summary>
    ''' Seta foco no botão btnHelper
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  05/01/2011  criado
    ''' </history>
    Public Sub BtnHelperFocus()

        'btnHelper.Focus()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SETAFOCUS", "document.getElementById('" & Me.BtnClientID & "').focus();", True)

    End Sub

    ''' <summary>
    ''' Limpa valor selecionado 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011  criado
    ''' </history>
    Public Sub Limpar()

        Me.ValorSelecionado = Nothing
        txtHelper.Text = String.Empty

    End Sub

    ''' <summary>
    ''' verifica se os controles do form pai devem ser limpos
    ''' (usuário limpou seleção e clicou aceitar)
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011  criado
    ''' </history>
    Private Sub VerificarLimparControlesFormPai()

        If Session(hidSessionLimpar.Value) IsNot Nothing Then

            ' dispara evento para ser tratado no form pai (todos os controles dependentes do helper
            ' deverão ser limpos
            RaiseEvent LimparControles(Me, New System.EventArgs())

            ' limpa sessão limpar
            Session(hidSessionLimpar.Value) = Nothing

        End If

    End Sub

    ''' <summary>
    ''' Adiciona algum código javascript a um evento do textbox
    ''' </summary>
    ''' <param name="Evento">Evento javascript. Exemplo: onkeydown</param>
    ''' <param name="CodigoJavaScript">String com código javascript</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011  criado
    ''' </history>
    Public Sub AdicionarJSTextBox(Evento As String, CodigoJavaScript As String)

        txtHelper.Attributes.Add(Evento, CodigoJavaScript)

    End Sub

#End Region

#Region "[EVENTOS]"

    ''' <summary>
    ''' Evento click do botão
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  05/01/2011  criado
    ''' </history>
    Private Sub btnHelper_Click(sender As Object, e As System.EventArgs) Handles btnHelper.Click

        RaiseEvent btnHelperClick(sender, e)

    End Sub

    ''' <summary>
    ''' Evento TextChanged do textbox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011  criado
    ''' </history>
    Private Sub txtHelper_TextChanged(sender As Object, e As System.EventArgs) Handles txtHelper.TextChanged

        RaiseEvent txtHelperTextChanged(sender, e)

    End Sub


#End Region

End Class