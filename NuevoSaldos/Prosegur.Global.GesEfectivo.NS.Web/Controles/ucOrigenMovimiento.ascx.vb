Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Framework.Dicionario

Public Class ucOrigenMovimiento
    Inherits UcBase

    '#Region "Propriedades"

    '    ''' <summary>
    '    ''' Recupera e Grava dados de Delegação.
    '    ''' </summary>
    '    Public Property Delegacao As Clases.Delegacion
    '        Get
    '            Return ViewState("Delegacion")
    '        End Get
    '        Set(value As Clases.Delegacao)
    '            ViewState("Delegacion") = value
    '        End Set
    '    End Property

    '    ''' <summary>
    '    ''' Recupera e Grava dados de Planta.
    '    ''' </summary>
    '    Public Property Planta As GenesisSaldos.Planta
    '        Get
    '            Return ViewState("Planta")
    '        End Get
    '        Set(value As GenesisSaldos.Planta)
    '            ViewState("Planta") = value
    '        End Set
    '    End Property

    '    ''' <summary>
    '    ''' Recupera e Grava dados de Setor.
    '    ''' </summary>
    '    Public Property Setor As GenesisSaldos.Setor
    '        Get
    '            Return ViewState("Sector")
    '        End Get
    '        Set(value As GenesisSaldos.Setor)
    '            ViewState("Sector") = value
    '        End Set
    '    End Property

    '#End Region

    '#Region "Eventos"

    '    ''' <summary>
    '    ''' Metodo referente ao evento de carregamento da página.
    '    ''' </summary>
    '    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    '        Try
    '            If Not (IsPostBack) Then
    '                Me.TraduzirControle()
    '                Me.PreencherCampos()
    '            End If
    '        Catch ex As Exception
    '            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro", _
    '                                                   Aplicacao.Util.Utilidad.CriarChamadaMensagemErro(ex.Message, Nothing), True)
    '        End Try

    '    End Sub

    '#End Region

    '#Region "Métodos"

    '    ''' <summary>
    '    ''' Obtém dados para exibição da Delegação, Planta e Setor onde serão realizados o movimento.
    '    ''' </summary>
    '    ''' <param name="objDelegacao">Objeto contendo informações de Delegação.</param>
    '    ''' <param name="objPlanta">Objeto contendo informações de Planta.</param>
    '    ''' <param name="objSetor">Objeto contendo informações de Setor.</param>
    '    Public Sub ExibirOrigemMovimento(objDelegacao As GenesisSaldos.Delegacao, objPlanta As GenesisSaldos.Planta, objSetor As GenesisSaldos.Setor)

    '        Me.Delegacao = objDelegacao
    '        Me.Planta = objPlanta
    '        Me.Setor = objSetor

    '    End Sub

    '    ''' <summary>
    '    ''' Define valores padrão a serem inseridos nas caixas de texto do controle.
    '    ''' </summary>    
    '    Private Sub TraduzirControle()

    '        txtOrigen.Text = Tradutor.Traduzir("010_desOrigem")
    '        txtCodDelegacion.Text = Tradutor.Traduzir("010_codDelegacao")
    '        txtDelegacion.Text = Tradutor.Traduzir("010_desDelegacao")
    '        txtCodPlanta.Text = Tradutor.Traduzir("010_codPlanta")
    '        txtDesPlanta.Text = Tradutor.Traduzir("010_desPlanta")
    '        txtCodSector.Text = Tradutor.Traduzir("010_codSetor")
    '        txtDesSector.Text = Tradutor.Traduzir("010_desSetor")

    '    End Sub

    '    ''' <summary>
    '    ''' Preenche controle Origem Movimento.
    '    ''' </summary>    
    '    Private Sub PreencherCampos()

    '        ' Preenche campo Delegação.
    '        If (Me.Delegacao IsNot Nothing) Then
    '            txtCodDelegacion.Text = Me.Delegacao.Codigo
    '            txtDelegacion.Text = Me.Delegacao.Descricao
    '        End If

    '        ' Preenche campo Planta.
    '        If (Me.Planta IsNot Nothing) Then
    '            txtCodPlanta.Text = Me.Planta.Codigo
    '            txtDesPlanta.Text = Me.Planta.Descricao
    '        End If

    '        ' Preenche campo Setor.
    '        If (Me.Setor IsNot Nothing) Then
    '            txtCodSector.Text = Me.Setor.Codigo
    '            txtDesSector.Text = Me.Setor.Descricao
    '        End If

    '    End Sub

    '#End Region

End Class