Public Class UserControlBase
    Inherits UserControl

    Public Event OcorreuErro As EventHandler(Of ExcepcionEventArgs)

    Protected Overridable Sub OnOcorreuErro(e As ExcepcionEventArgs)
        RaiseEvent OcorreuErro(Me, e)
    End Sub

    Public Sub EnviarMensagemErro(e As String)
        OnOcorreuErro(New ExcepcionEventArgs(e))
    End Sub

    ''' <summary>
    ''' Informacoes do usuario logado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 10/02/2009 Criado
    ''' </history>
    Public Property InformacionUsuario() As ContractoServ.Login.InformacionUsuario
        Get

            ' se sessão não foi setado
            If Session("BaseInformacoesUsuario") IsNot Nothing Then

                ' tentar recuperar objeto da sessao
                Dim Info = TryCast(Session("BaseInformacoesUsuario"), ContractoServ.Login.InformacionUsuario)

                ' retornar objeto
                Return Info

            End If

            Return New ContractoServ.Login.InformacionUsuario

        End Get
        Set(value As ContractoServ.Login.InformacionUsuario)
            Session("BaseInformacoesUsuario") = value
        End Set
    End Property

    ''' <summary>
    ''' Mostra Mensagem de Erro na tela
    ''' </summary>
    ''' <param name="erro"></param>
    ''' <remarks></remarks>
    ''' <history>[claudioniz.pereira] 09/08/2013 - criado</history>
    Public Sub MostraMensagemErro(erro As String, script As String)
        erro = erro.Replace(vbCrLf, "<br />")
        erro = erro.Replace(vbCr, "<br />")
        erro = erro.Replace(vbLf, "<br />")
        erro = erro.Replace("'", "&apos")
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro", Util.CriarChamadaMensagemErro(erro, script), True)
    End Sub

    Public Sub MostraMensagemErro(erro As String)
        MostraMensagemErro(erro, String.Empty)
    End Sub

End Class
