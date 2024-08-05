Imports Prosegur.Framework.Dicionario.Tradutor

Partial Public Class _Default
    Inherits Base

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.MENU
        ' desativar validação de ação
        MyBase.ValidarAcao = False
    End Sub

    Protected Overrides Sub Inicializar()

        Master.MostrarCabecalho = True
        Master.HabilitarMenu = True
        Master.HabilitarHistorico = True

        Master.MostrarRodape = True
        Master.MenuRodapeVisivel = False
        Master.Titulo = Traduzir("TituloDefault")

    End Sub

    Protected Overrides Sub PreRenderizar()

    End Sub

    Protected Overrides Sub TraduzirControles()

    End Sub

#End Region

End Class