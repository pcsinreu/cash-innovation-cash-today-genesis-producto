Imports System.ComponentModel
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
        MyBase.Acao = Enumeradores.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.MENU
        ' desativar validação de ação
        MyBase.ValidarAcao = False
    End Sub

    Protected Overrides Sub Inicializar()
        Try
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarCabecalho = True
            Master.MenuRodapeVisivel = False
            Master.MostrarRodape = True
            Master.Titulo = Traduzir("001_lbl_titulo")
        Catch ex As Exception
            MyBase.MostraMensagem(ex.Message)
        End Try
    End Sub

    Protected Overrides Sub PreRenderizar()

    End Sub

    Protected Overrides Sub TraduzirControles()

    End Sub

    Protected Overrides Sub ConfigurarEstadoPagina()

    End Sub

#End Region

End Class
