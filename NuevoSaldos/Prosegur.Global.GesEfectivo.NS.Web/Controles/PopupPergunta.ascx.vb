Imports Prosegur.Framework.Dicionario

Public Class PopupPergunta
    Inherits PopupBase

    Public Enum TipoMensagem
        none
        alert
        info
    End Enum

    Public WriteOnly Property FormatoImagem As TipoMensagem
        Set(value As TipoMensagem)
            Select Case value
                Case TipoMensagem.none
                Case TipoMensagem.info
                    ltlimgPergunta.Text = "<span class='ui-icon ui-icon-info' style='float: left; margin: 0 7px 50px 0;'></span>"
                Case TipoMensagem.alert
                    ltlimgPergunta.Text = "<span class='ui-icon ui-icon-alert' style='float: left; margin: 0 7px 50px 0;'></span>"
            End Select

        End Set
    End Property
    Public Property pergunta As String
        Get
            Return lblPegunta.Text
        End Get
        Set(value As String)
            lblPegunta.Text = value
        End Set
    End Property


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Titulo = Tradutor.Traduzir("aplicacao")
    End Sub

    Protected Sub btnAceitar_Click(sender As Object, e As EventArgs) Handles btnAceitar.Click
        Me.FecharPopup(True)
    End Sub


    Protected Sub btnRecusar_Click(sender As Object, e As EventArgs) Handles btnRecusar.Click
        Me.FecharPopup(False)
    End Sub
End Class