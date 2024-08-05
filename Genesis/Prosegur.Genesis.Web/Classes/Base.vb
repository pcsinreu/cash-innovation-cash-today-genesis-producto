Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Web.Login

Public Class Base
    Inherits Page

    Public Sub SetarFocus(controle As WebControl)
        ScriptManager.RegisterStartupScript(Me, Me.GetType, Guid.NewGuid.ToString,
                                                String.Format("window.setTimeout(function(){{document.getElementById('{0}').focus();}}, 200);", controle.ClientID), True)

    End Sub

    Public Sub ExibirMensagemErro(mensagem As String)
        mensagem = mensagem.Replace("\", "\\").
            Replace(vbCrLf, "\n").
            Replace(vbCr, "\n").
            Replace(vbLf, "\n").
            Replace("'", "\'")
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), Guid.NewGuid.ToString(),
                                                String.Format("alert('{0}')", mensagem), True)
    End Sub

    Protected Sub CarregarVersion(lbl As Label)

        lbl.Text = String.Format("{0} {1}",
                                    Traduzir("lgn_lblversion"),
                                    Comon.Util.VersionPantallas(System.Reflection.Assembly.GetExecutingAssembly))

    End Sub

    Protected Function obtenerVersion() As String

        Return String.Format("{0} {1}",
                                    Traduzir("lgn_lblversion"),
                                    Comon.Util.VersionPantallas(System.Reflection.Assembly.GetExecutingAssembly))

    End Function

    ''' <summary>
    ''' Retorna o Id do controle que receberá o foco
    ''' </summary>
    ''' <param name="Controle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function RetornarIdControleFoco(Controle As Control) As String

        ' Verifica se o tipo do controle é um PlaceHolder
        If TypeOf Controle Is UI.WebControls.PlaceHolder Then
            ' Retorna o Id do primeiro controle do Place Holder
            RetornarIdControleFoco(Controle.Controls(0))
            ' Se o controle é um botão
        ElseIf TypeOf Controle Is Prosegur.Web.Botao Then
            ' Retorna o Id do controle concatenado com "_img"
            Return (Controle.ID & "_img")

        Else
            ' Retorna o Id do controle
            Return (Controle.ClientID)

        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' Define para qual controle o foco deve ir quando o último controle perder o foco
    ''' </summary>
    ''' <param name="UltimoControle"></param>
    ''' <param name="PrimeiroControle"></param>
    ''' <remarks></remarks>
    Public Sub DefinirRetornoFoco(UltimoControle As Control, PrimeiroControle As Control)

        ' Recupera o client id do primeiro controle (para o qual o foco deve ser retornado)
        Dim IdControleRetorno As String = RetornarIdControleFoco(PrimeiroControle)

        ' Se o controle é um menu
        If (TypeOf PrimeiroControle Is WebControls.Menu) Then
            ' Define o nome do primeiro nodo
            IdControleRetorno &= "n0"
        End If

        ' Dependendo do tipo de controle seta o atributo para retornar o foco ao 1o controle quando perder o foco
        If TypeOf UltimoControle Is Prosegur.Web.Botao Then

            CType(UltimoControle, Prosegur.Web.Botao).AdicionarAtributoIcone("onfocusout", "SetarProximoFoco('" & IdControleRetorno & "')")

        ElseIf TypeOf UltimoControle Is WebControls.TextBox Then

            CType(UltimoControle, WebControls.TextBox).Attributes.Add("onblur", "SetarProximoFoco('" & IdControleRetorno & "')")

        ElseIf TypeOf UltimoControle Is WebControls.LinkButton Then

            CType(UltimoControle, WebControls.LinkButton).Attributes.Add("onblur", "SetarProximoFoco('" & IdControleRetorno & "')")
        Else

            CType(UltimoControle, HtmlControl).Attributes.Add("onblur", "SetarProximoFoco('" & IdControleRetorno & "')")

        End If

    End Sub


#Region "[EVENTOS]"

    ''' <summary>
    ''' Evento load.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 11/02/2009 Criado
    ''' </history>
    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            ' configura parametros da base antes de iniciar as validações e iniciar a página filha.
            DefinirParametrosBase()
        End If

        ' traduzir controles da tela
        TraduzirControles()

        'Seta configuração do Response para não armazenar a página em cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        ' inicializar página filha
        Inicializar()

        'scripts
        AdicionarScripts()

        ' configurar tabindex
        ConfigurarTabIndex()

    End Sub



    ''' <summary>
    ''' Trata o retorno dos serviços
    ''' </summary>
    ''' <param name="objPeticion">Objeto com a petição solicitada</param>
    ''' <param name="IdentificadorOperacion">Identidicador da Operação que foi acionada</param>
    ''' <param name="objRespuesta">Objeto com a resposta da requisição</param>
    ''' <param name="ExhibirMensaje">Indica se a mensagem de negócio deve ser exibida na tela</param>
    ''' <returns>retorna true para validação ok e false para erro</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [prezende] 22/05/2012 Criado
    ''' </history>
    Public Function TratarRetornoServico(objPeticion As Object, _
                                                IdentificadorOperacion As Comunicacion.Metodo, _
                                                ByRef objRespuesta As ContractoServicio.RespuestaGenerico, _
                                                Optional ExhibirMensaje As Boolean = True) As Boolean

        ' verifica se o retorno não é nothing
        If objRespuesta IsNot Nothing Then

            ' se houve erro e o código for maior ou igual a 100
            If objRespuesta.CodigoError >= 100 Then

                If ExhibirMensaje Then
                    ' exibir mensagem
                    ExibirMensagemErro(objRespuesta.MensajeError)
                End If

            ElseIf objRespuesta.CodigoError > 0 AndAlso objRespuesta.CodigoError < 100 Then

                'loga o erro
                Util.LogarErroAplicacao(objRespuesta)
                ExibirMensagemErro(Traduzir("000_msgerrogenerico"))

            ElseIf objRespuesta.CodigoError = 0 Then
                ' resposta do serviço ok
                Return True
            End If

        End If

        ' se chegou até aqui, houve algum erro no serviço
        Return False

    End Function



    ''' <summary>
    ''' Evento pre-render.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 11/02/2009 Criado
    ''' </history>
    Private Sub Base_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender

        ' executa funções da página filha no pre-render da página.
        PreRenderizar()

    End Sub

#End Region

#Region "[ASSINATURA DE MÉTODOS]"

    ''' <summary>
    ''' Permite definir parametros da base antes de validar acessos e chamar o método inicializar.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub DefinirParametrosBase()
    End Sub

    ''' <summary>
    ''' Método chamado no load da base.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub Inicializar()
    End Sub

    ''' <summary>
    ''' Método chamado ao renderizar a base.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub PreRenderizar()
    End Sub

    ''' <summary>
    ''' Método que permite o desenvoledor adicionar controles para validação.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub AdicionarControlesValidacao()
    End Sub

    ''' <summary>
    ''' Método que permite o desenvolvedor traduzir os controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub TraduzirControles()
    End Sub

    ''' <summary>
    ''' Método que permite o desenvolvedor adicionar scripts para controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub AdicionarScripts()
    End Sub

    ''' <summary>
    ''' Método que permite o desenvolvedor configurar os tab index dos controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub ConfigurarTabIndex()
    End Sub

    ''' <summary>
    ''' Método que permite o desenvolvedor configurar o estado da página, seja ele de inserção, alteração ou exclusão
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub ConfigurarEstadoPagina()
    End Sub

#End Region


End Class
