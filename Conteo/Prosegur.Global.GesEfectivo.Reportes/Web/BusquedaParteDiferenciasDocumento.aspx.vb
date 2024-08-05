Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Genesis.Comunicacion

Public Class BusquedaParteDiferenciasDocumento
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Enumeradores.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.PARTE_DIFERENCIAS
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False
    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub PreRenderizar()
        Try
            Me.ConfigurarEstadoPagina()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Overrides Sub Inicializar()
        Master.MostrarRodape = True
        Master.MenuRodapeVisivel = False
        Master.HabilitarHistorico = False
        Master.MostrarCabecalho = True
        Master.HabilitarMenu = False
        Master.Titulo = Traduzir("017_titulo_pagina")
    End Sub

#End Region

#Region "[EVENTOS]"

    Private Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit

        Try
            Dim tipo As String = Request.QueryString("tipo")
            Dim nome As String = Request.QueryString("nome")
            Dim id As String = Request.QueryString("id")

            If Not String.IsNullOrEmpty(tipo) AndAlso Not String.IsNullOrEmpty(id) Then

                ' proxy
                Dim objParteDiferencias As New ListadosConteo.ProxyParteDiferencias

                ' petição do documento
                Dim objPeticion As New ContractoServ.ParteDiferencias.GetDocumentos.Peticion

                objPeticion.ID = id

                Select Case tipo
                    Case "G"
                        objPeticion.General = True
                    Case "I"
                        objPeticion.Comentario = True
                    Case "J"
                        objPeticion.Justificativa = True
                    Case Else
                        ' TODO: erro no tipo de documento
                End Select

                ' recupera o documento
                Dim objRespuesta As ContractoServ.ParteDiferencias.GetDocumentos.Respuesta = objParteDiferencias.RecuperarDocumentos(objPeticion)

                ' verifica o retorno do serviço
                Dim msgErro As String = String.Empty
                If Not Master.ControleErro.VerificaErro2(objRespuesta.CodigoError, ContractoServ.Login.ResultadoOperacionLoginLocal.Autenticado, msgErro, objRespuesta.MensajeError, True) Then
                    MyBase.MostraMensagem(msgErro)
                    Exit Sub
                End If

                Dim conteudo() As Byte = Nothing

                Select Case tipo
                    Case "G"
                        ReDim conteudo(objRespuesta.Documentos.General.GetLength(0))
                        conteudo = objRespuesta.Documentos.General
                    Case "I"
                        ReDim conteudo(objRespuesta.Documentos.Comentario.GetLength(0))
                        conteudo = objRespuesta.Documentos.Comentario
                    Case "J"
                        ReDim conteudo(objRespuesta.Documentos.Justificativa.GetLength(0))
                        conteudo = objRespuesta.Documentos.Justificativa
                    Case Else
                        ' TODO: erro no tipo de documento
                End Select

                Response.Clear()
                Response.ClearContent()
                Response.ClearHeaders()
                Response.ContentType = "application/pdf"
                Response.AppendHeader("Content-Disposition", "attachment; filename=" & nome & ".pdf")
                Response.AppendHeader("Content-Length", "" & conteudo.Length)
                Response.BinaryWrite(conteudo)
                Response.End()

            End If

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

#End Region

End Class