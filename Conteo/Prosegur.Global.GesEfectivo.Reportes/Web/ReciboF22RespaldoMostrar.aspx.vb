Imports Prosegur.Framework.Dicionario.Tradutor

Public Class ReciboF22RespaldoMostrar
    Inherits Base

#Region "[ATRIBUTOS]"

    Private _NomeArquivo As String
    Private Const CONST_DIRETORIO_LISTADO As String = "\Arquivos\"

#End Region

#Region "[MÉTODOS]"

    Protected Overrides Sub Inicializar()
        Master.MostrarRodape = True
        Master.MenuRodapeVisivel = False
        Master.HabilitarHistorico = False
        Master.MostrarCabecalho = True
        Master.HabilitarMenu = False
        Master.Titulo = Traduzir("013_titulo_pagina")
    End Sub

    ''' <summary>
    ''' Recupera os parametros passados para a página
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 28/03/2011 Criado    
    ''' </history>
    Private Sub RecuperarParametros()

        ' Se existir
        If (Request.QueryString("NomeArquivo") IsNot Nothing) Then
            ' Recupera o nome do arquivo
            _NomeArquivo = Request.QueryString("NomeArquivo")
        End If

    End Sub

    ''' <summary>
    ''' Carrega os dados no banco e preenche o grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 28/02/2011 Criado    
    ''' </history>
    Private Sub CarregarDados()

        Dim arqRelReciboF22Respaldo As New System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory() & CONST_DIRETORIO_LISTADO & _NomeArquivo)

        'Limpa o conteúdo de saída atual do buffer
        Response.Clear()
        'Adiciona um cabeçalho que especifica o nome default para a caixa de diálogos Salvar Como...
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=""" & arqRelReciboF22Respaldo.Name & """")
        'Adiciona ao cabeçalho o tamanho do arquivo para que o browser possa exibir o progresso do download
        Response.AddHeader("Content-Length", arqRelReciboF22Respaldo.Length.ToString())
        Response.Flush()
        Response.WriteFile(arqRelReciboF22Respaldo.FullName)

    End Sub

#End Region

#Region "[EVENTOS]"

    Private Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit

        Try
            ' Recuperar Parametros
            RecuperarParametros()

            ' Carrega os dados do relatório
            CarregarDados()

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

#End Region

End Class