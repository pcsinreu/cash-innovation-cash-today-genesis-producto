Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Genesis.Comon.Extenciones

Public Class PrincipalPag
    Inherits Base

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

        Dim pbo As PostBackOptions
        Dim s As String = String.Empty

        pbo = New PostBackOptions(btnConfirmar)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnConfirmar.FuncaoJavascript = s & ";if (event.keyCode == 13 || event.keyCode == 32)desabilitar_botoes('" & btnConfirmar.ClientID & "');"

        'Adiciona a Precedencia ao Buscar
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnConfirmar.ClientID & "';", True)

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        ddlDelegacion.TabIndex = 1
        btnConfirmar.TabIndex = 2        

    End Sub

    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Enumeradores.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.PRINCIPAL
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False
    End Sub

    Protected Overrides Sub Inicializar()

        Try

            ' ocultar menu
            Master.MostrarMenu = False

            ' carregar delegações da sessão para o combo
            Util.CarregarDelegacoes(ddlDelegacion, MyBase.InformacionUsuario.Delegaciones)

            ' setar foco primeiro controle
            ddlDelegacion.Focus()

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Protected Overrides Sub PreRenderizar()

    End Sub

    Protected Overrides Sub ConfigurarEstadoPagina()

    End Sub

    Protected Overrides Sub TraduzirControles()

        ' titulo
        lblTitulo.Text = Traduzir("012_titulo")

        ' controles
        lblDelegacion.Text = Traduzir("012_lbl_delegacion")

    End Sub

#End Region

#Region "[EVENTOS]"

    ''' <summary>
    ''' Evento clique do botão confirmar para selecionar a delegação a ser utilizada no sistema
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.Fraga] 16/03/2011 Criado
    ''' </history>
    Protected Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click

        Try
            Dim errosValidacao As New StringBuilder

            ' delegacao selecionada
            If ddlDelegacion.SelectedIndex <= 1 Then

                If errosValidacao.ToString.Equals(String.Empty) Then
                    ddlDelegacion.Focus()
                End If

                Me.csvDelegacion.IsValid = False
                errosValidacao.Append(String.Format(Traduzir("err_campo_obrigatorio"), lblDelegacion.Text) & Constantes.LineBreak)

            End If

            If Not errosValidacao.ToString.Equals(String.Empty) Then

                ' apresentar erros para o usuário
                Master.ControleErro.ShowError(errosValidacao.ToString(), False)

            Else

                Dim objDelegacionSelecionada As Prosegur.Global.GesEfectivo.Reportes.ContractoServ.Login.Delegacion = Nothing
                objDelegacionSelecionada = MyBase.InformacionUsuario.Delegaciones.First(Function(f) f.Codigo = ddlDelegacion.SelectedValue)

                ' se a delegação foi encontrada na lista de delegações que o usuário tem permisão
                If objDelegacionSelecionada IsNot Nothing Then

                    ' seta a sessão do website coma  delegação selecionada
                    MyBase.InformacionUsuario.DelegacionLogin = objDelegacionSelecionada

                    ' guardar delegacao
                    Dim DelegacionDic As New Dictionary(Of String, String)
                    DelegacionDic.Add(MyBase.InformacionUsuario.DelegacionLogin.Codigo, MyBase.InformacionUsuario.DelegacionLogin.Descripcion)
                    DelegacionConectada = DelegacionDic

                    ' redirecionar para tela inicial do sistema
                    Response.Redireccionar(Constantes.NOME_PAGINA_MENU, False)

                End If

            End If

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        Finally

            btnConfirmar.Habilitado = True

        End Try

    End Sub

#End Region

End Class