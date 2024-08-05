Imports Prosegur.Framework.Dicionario.Tradutor

''' <summary>
''' Classe utilizada para exibir os relatórios de Recibo
''' </summary>
''' <history>[jorge.viana]	24/08/2010	Creado</history>
''' <remarks></remarks>
Public Class ReimpresionRecibo

#Region "[VARIÁVEIS]"

    'Armazena o controle do relatório Crystal
    Private _ucRelatorio As ucCrystal
    'Armazena se a tela deve ser exibida ou não
    Private _ExibeTela As Boolean = True

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Propriedade ucRelatorio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <history>[jorge.viana]	24/08/2010	Creado</history>
    ''' <remarks></remarks>
    Public ReadOnly Property ucRelatorio() As ucCrystal
        Get
            Return _ucRelatorio
        End Get
    End Property

    ''' <summary>
    ''' Propriedade ExibeTela
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <history>[jorge.viana]	24/08/2010	Creado</history>
    ''' <remarks></remarks>
    Public ReadOnly Property ExibeTela() As Boolean
        Get
            Return _ExibeTela
        End Get
    End Property

#End Region

#Region "[CONSTRUTORES]"

    ''' <summary>
    ''' Construtor Default
    ''' </summary>
    ''' <param name="ucRelatorio"></param>
    ''' <history>[jorge.viana]	24/08/2010	Creado</history>
    ''' <remarks></remarks>
    Public Sub New(ucRelatorio As ucCrystal)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        If ucRelatorio IsNot Nothing Then

            ' Atualiza as variáveis globais
            _ucRelatorio = ucRelatorio

            ' traduz os controles da tela
            TraduzirControles()

        Else
            _ExibeTela = False
            Me.Close()
        End If

    End Sub

#End Region

#Region "[EVENTOS]"

    ''' <summary>
    ''' Evento KeyDown da tela
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>[jorge.viana]	24/08/2010	Creado</history>
    ''' <remarks></remarks>
    Private Sub ReimpresionRecibo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub

    ''' <summary>
    ''' Evento Load da tela
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>[jorge.viana]	24/08/2010	Creado</history>
    ''' <remarks></remarks>
    Private Sub ReimpresionRecibo_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Adiciona o controle na tela
        Me.Controls.Add(_ucRelatorio)
        _ucRelatorio.Dock = Windows.Forms.DockStyle.Fill
        _ucRelatorio.Relatorio.Show()
    End Sub

#End Region

#Region "[MÉTODOS/FUNÇÕES]"

    ''' <summary>
    ''' Método para traduzir controles da tela
    ''' </summary>
    ''' <history>[jorge.viana]	25/08/2010	Creado</history>
    ''' <remarks></remarks>
    Private Sub TraduzirControles()
        'Título da Tela
        Me.Text = _ucRelatorio.TituloRelatorio
    End Sub

#End Region

End Class