Imports Prosegur.Framework.Dicionario.Tradutor

''' <summary>
''' Página de Manutenção de Grupos
''' </summary>
''' <remarks></remarks>
''' <history>
''' [bruno.costa]  18/01/2011  criado
''' </history>
Partial Public Class MantenimientoGrupo
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
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If

        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.ATM

    End Sub

    Protected Overrides Sub Inicializar()

        Try
            Master.HabilitarHistorico = False

            If Not Page.IsPostBack Then

                'Possíveis Ações passadas pela página BusquedaDivisaes:
                ' [-] Aplicacao.Util.Utilidad.eAcao.Alta

                If Not (MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta) Then

                    Acao = Aplicacao.Util.Utilidad.eAcao.Erro
                    'Informa ao usuário que o parâmetro passado são inválidos
                    Throw New Exception(Traduzir("err_passagem_parametro"))

                End If

                ' seta foco inicial
                txtCodigo.Focus()

            End If

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Protected Overrides Sub PreRenderizar()

        Try
            ControleBotoes()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()

        ' títulos
        Page.Title = Traduzir("024_titulo_grupo")
        lblTituloGrupo.Text = Traduzir("024_subtitulo_grupo")
        btnGrabar.Text = Traduzir("btnGrabar")

        ' labels
        lblCodigo.Text = Traduzir("024_codigo_grupo")
        lblDescripcion.Text = Traduzir("024_desc_grupo")

        ' mensagens validação
        csvCodigoExistente.ErrorMessage = Traduzir("024_msg_codigo_existente")
        csvCodigoObrigatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("024_codigo_grupo"))
        csvDescricaoObrigatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("024_desc_grupo"))

    End Sub

#End Region

#Region "[DADOS]"


#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS BOTÕES]"
    
    ''' <summary>
    ''' Clique do botão Grabar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()
    End Sub

    'Ações que podem ser chamadas a qualquer momento
#Region "Ações Botões Independentes"

    ''' <summary>
    ''' Função do botão grabar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  27/01/2011  criado
    ''' </history>
    Public Sub ExecutarGrabar()

        Try

            ValidarCamposObrigatorios = True

            Dim strErros = MontaMensagensErro(True)
            If strErros.Length > 0 Then
                MyBase.MostraMensagem(strErros)
                Exit Sub
            End If

            Dim objGrupo As New Negocio.Grupo

            objGrupo.CodGrupo = txtCodigo.Text
            objGrupo.DesGrupo = txtDescricao.Text
            objGrupo.CodUsuario = MyBase.LoginUsuario

            ' cria grupo
            objGrupo.Insertar()

            ' trata erros
            If Master.ControleErro.VerificaErro(objGrupo.Respuesta.CodigoError, objGrupo.Respuesta.NombreServidorBD, objGrupo.Respuesta.MensajeError) Then

                ' seta para atualizar combo na janela pai
                Me.AtualizaGrupo = True

                Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
                ' fechar janela
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "FinalizaComponente", jsScript, True)
             
            Else

                MyBase.MostraMensagem(objGrupo.Respuesta.MensajeError)

            End If

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    
#End Region

#End Region

#End Region

#Region "[MÉTODOS]"

    Private Function VerificaCodigoGrupo(codGrupo As String) As Boolean
        Try

            If codGrupo.Trim() = String.Empty Then
                Exit Function
            End If

            Dim objGrupo As New Negocio.Grupo

            ' verifica se existe grupo com o codigo
            Dim bolExiste As Boolean = objGrupo.VerificarGrupo(codGrupo.Trim())

            If Not Master.ControleErro.VerificaErro(objGrupo.Respuesta.CodigoError, objGrupo.Respuesta.NombreServidorBD, objGrupo.Respuesta.MensajeError) Then
                MyBase.MostraMensagem(objGrupo.Respuesta.MensajeError)
                ' se ocorreu algum erro, finaliza
                Exit Function
            End If

            Return bolExiste

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try
    End Function
    ''' <summary>
    ''' MontaMensagensErro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            If ValidarCamposObrigatorios Then

                strErro.Append(MyBase.TratarCampoObrigatorio(txtCodigo, csvCodigoObrigatorio, SetarFocoControle, focoSetado))
                strErro.Append(MyBase.TratarCampoObrigatorio(txtDescricao, csvDescricaoObrigatorio, SetarFocoControle, focoSetado))

            End If

            'Verifica se o código existe
            If Not String.IsNullOrEmpty(txtCodigo.Text.Trim()) AndAlso VerificaCodigoGrupo(txtCodigo.Text.Trim()) Then

                strErro.Append(csvCodigoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigo.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function

#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta

                btnGrabar.Visible = True
                btnGrabar.Enabled = True
               
            Case Aplicacao.Util.Utilidad.eAcao.Erro

                btnGrabar.Visible = False
             
        End Select
        
    End Sub

#End Region

#Region "[PROPRIEDADES]"

    Private Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property

    Private Property CodigoExistente() As Boolean
        Get
            Return ViewState("CodigoExistente")
        End Get
        Set(value As Boolean)
            ViewState("CodigoExistente") = value
        End Set
    End Property

    ''' <summary>
    ''' indica se o combo grupo deve ser recarregado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  28/01/2011  criado
    ''' </history>
    Private Property AtualizaGrupo() As Boolean
        Get
            Return Session("AtualizaGrupo")
        End Get
        Set(value As Boolean)
            Session("AtualizaGrupo") = value
        End Set
    End Property

    Private ReadOnly Property divModal() As String
        Get
            Return Request.QueryString("divModal").ToString()
        End Get
    End Property
    Private ReadOnly Property ifrModal() As String
        Get
            Return Request.QueryString("ifrModal").ToString()
        End Get
    End Property
    Private ReadOnly Property btnExecutar() As String
        Get
            Return Request.QueryString("btnExecutar").ToString()
        End Get
    End Property
#End Region

End Class