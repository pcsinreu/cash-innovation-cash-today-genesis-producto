Public Class BusquedaDatosBancariosComentarioPopup
    Inherits Base

#Region "[Propriedades]"
    Private ReadOnly Property divModal() As String
        Get
            Return Request.QueryString("divModal")
        End Get
    End Property
    Private ReadOnly Property ifrModal() As String
        Get
            Return Request.QueryString("ifrModal")
        End Get
    End Property
    Private ReadOnly Property btnExecutar() As String
        Get
            Return Request.QueryString("btnExecutar")
        End Get
    End Property

#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.APROBACION_CUENTAS_BANCARIAS
        MyBase.ValidarAcao = False
        MyBase.ValidarPemissaoAD = False
    End Sub

    Protected Overrides Sub Inicializar()

        Try
            'Si abro el popup limpio la variable de sesion
            Session("Dto_Banc_Comentario") = String.Empty
            If Not Page.IsPostBack Then
                txtComentario.Attributes.Add("maxlength", txtComentario.MaxLength.ToString())
                txtComentario.Focus()
            End If

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Protected Overrides Sub AdicionarScripts()
        Dim jsScript As String = "window.parent.FecharModal(" & Chr(34) & "#" & divModal & Chr(34) & "," & Chr(34) & "#" & ifrModal & Chr(34) & ",null);"
        btnCancelar.OnClientClick = jsScript
    End Sub

    Protected Overrides Sub TraduzirControles()


        MyBase.CodFuncionalidad = "UCDATOSBANCARIOS"
        MyBase.CarregaDicinario()
        lblMensaje.Text = MyBase.RecuperarValorDic("msg_alert_aprobador")
        lblTituloComentarioPopup.Text = MyBase.RecuperarValorDic("lblTituloComentarioPopup")
        lblComentario.Text = MyBase.RecuperarValorDic("lblComentario")
        Me.Page.Title = MyBase.RecuperarValorDic("087_lbl_titulo")

        'Botoes
        btnAceptar.Text = MyBase.RecuperarValorDic("btnAceptar")
        btnAceptar.ToolTip = MyBase.RecuperarValorDic("btnAceptar")
        btnCancelar.Text = MyBase.RecuperarValorDic("btnCancelar")
        btnCancelar.ToolTip = MyBase.RecuperarValorDic("btnCancelar")


    End Sub


#End Region


#Region "[EVENTOS]"

    Private Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click

        Try
            'Analizo si el campo txtComentario tiene valor y lo asigno a la variable de sesion
            If Not String.IsNullOrWhiteSpace(txtComentario.Text) Then
                Session("Dto_Banc_Comentario") = txtComentario.Text
                Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
                ' fechar janela
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ComentarioOK", jsScript, True)
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
#End Region
End Class