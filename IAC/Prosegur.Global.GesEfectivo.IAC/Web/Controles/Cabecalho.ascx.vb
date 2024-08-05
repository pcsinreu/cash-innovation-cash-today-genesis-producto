Imports Prosegur.Framework.Dicionario.Tradutor

Partial Public Class Cabecalho
    Inherits System.Web.UI.UserControl

#Region "[EVENTOS]"

    Private _VersionVisible As Boolean = True
    Public Property VersionVisible As Boolean
        Get
            Return _VersionVisible
        End Get
        Set(value As Boolean)
            _VersionVisible = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ' traduzir os controles da tela
        TraduzirControles()

        If Not VersionVisible Then
            dvVersion.Style.Item("display") = "none"
        End If
    End Sub

    Public Event Sair_Click(sender As Object, e As EventArgs)
    Private Sub lbSair_Click(sender As Object, e As EventArgs) Handles lbSair.Click
        RaiseEvent Sair_Click(sender, e)
    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Traduz os controles da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 10/02/2009 Criado
    ''' </history>
    Private Sub TraduzirControles()

        ' label
        lblTitulo.Text = Traduzir("lbl_base_titulo")

    End Sub

#End Region

End Class