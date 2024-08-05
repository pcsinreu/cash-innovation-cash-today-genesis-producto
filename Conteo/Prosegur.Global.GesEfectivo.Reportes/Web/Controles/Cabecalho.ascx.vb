Imports Prosegur.Framework.Dicionario.Tradutor

Partial Public Class Cabecalho
    Inherits System.Web.UI.UserControl

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ' traduzir os controles da tela
        TraduzirControles()

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