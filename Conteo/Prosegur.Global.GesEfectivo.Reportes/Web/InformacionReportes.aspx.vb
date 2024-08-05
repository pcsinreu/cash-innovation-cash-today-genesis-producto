Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.Comon.Extenciones

Public Class InformacionReportes
    Inherits Base

    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        MyBase.Acao = Enumeradores.eAcao.NoAction

        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False

        MyBase.ValidarAcesso = False

    End Sub

    Protected Overrides Sub Inicializar()
        Try
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarCabecalho = True
            Master.MenuRodapeVisivel = True
            Master.MostrarRodape = True
        Catch ex As Exception
            MyBase.MostraMensagem(ex.Message)
        End Try
    End Sub

    Protected Overrides Sub TraduzirControles()

        Master.Titulo = String.Format(Tradutor.Traduzir("InformacionReportes_titulo_pagina"), Genesis.Comon.Util.VersionPantallas(System.Reflection.Assembly.GetExecutingAssembly))
        lblSubTitulo.Text = String.Format(Tradutor.Traduzir("InformacionReportes_lbl_SubTitulo"), Genesis.Comon.Util.VersionPantallas(System.Reflection.Assembly.GetExecutingAssembly))
        btnExportar.Text = Tradutor.Traduzir("InformacionReportes_btnExportar")

    End Sub

    Protected Sub btnExportar_Click(sender As Object, e As EventArgs)
        Me.informacionGenesis.Exportar(Genesis.Comon.Enumeradores.Aplicacion.GenesisReportes.RecuperarValor(),
                                        InformacionUsuario.Nombre,
                                        MyBase.DelegacionConectada.Keys(0),
                                        Nothing,
                                        url:=HttpContext.Current.Request.Url.AbsoluteUri)
    End Sub
End Class