Imports Prosegur.Framework.Dicionario.Tradutor

Partial Public Class ErroNaoTratado
    Inherits Base

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.ERRO
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
    End Sub

    Protected Overrides Sub Inicializar()

        Master.MostrarCabecalho = True
        Master.HabilitarMenu = True
        Master.HabilitarHistorico = False
        Master.MenuRodapeVisivel = False

        Master.MostrarRodape = True
        Master.Titulo = Traduzir("err_padrao_aplicacao")
        MyBase.MostraMensagem(Traduzir("err_generico"))
    End Sub

    Protected Overrides Sub PreRenderizar()

    End Sub

    Protected Overrides Sub TraduzirControles()
        '  lblerror.Text = Traduzir("err_generico")
        lblTitulo.Text = Traduzir("err_padrao_aplicacao")
    End Sub

End Class