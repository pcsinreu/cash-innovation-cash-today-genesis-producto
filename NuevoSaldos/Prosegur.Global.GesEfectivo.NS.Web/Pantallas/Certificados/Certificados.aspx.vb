Public Class Certificados
    Inherits Base

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CERTIFICADOS
        MyBase.ValidarAcesso = True
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Master.Titulo = "Certificados > Provisional > Generar"
        Master.MenuRodapeVisivel = False
    End Sub

End Class