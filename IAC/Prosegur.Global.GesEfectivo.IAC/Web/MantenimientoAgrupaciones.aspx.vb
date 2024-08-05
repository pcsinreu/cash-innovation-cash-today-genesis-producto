Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' Página de Gerenciamento de Agrupações 
''' </summary>
''' <remarks></remarks>
''' <history>[PDA] 27/01/08 - Criado</history>
Partial Public Class MantenimientoAgrupaciones
    Inherits Base

#Region "[CONSTANTES]"

    Const TreeViewNodeEfectivo As String = "008_lbl_efectivo"

#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()
        'seta o foco para o proximo controle quando presciona o enter.
        txtCodigoAgrupacion.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoAgrupacion.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoAgrupacion.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtObservaciones.ClientID & "').focus();return false;}} else {return true}; ")
        txtObservaciones.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onblur", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onkeyup", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")

        'Controle de precedência(Ajax)

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)
    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigoAgrupacion.TabIndex = 1
        txtDescricaoAgrupacion.TabIndex = 2
        txtObservaciones.TabIndex = 3
        chkVigente.TabIndex = 4
        TrvDivisas.TabIndex = 5
        imgBtnIncluir.TabIndex = 6
        imgBtnExcluir.TabIndex = 7
        TrvAgrupaciones.TabIndex = 8
        btnGrabar.TabIndex = 9
        btnCancelar.TabIndex = 10
        btnVolver.TabIndex = 11

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.AGRUPACIONES

    End Sub

    Protected Overrides Sub Inicializar()

        Try
            If Not Page.IsPostBack Then

                'Recebe o código do Agrupacion
                Dim strCodAgrupacion As String = Request.QueryString("codAgrupacion")

                'Possíveis Ações passadas pela página BusquedaAgrupaciones:
                ' [-] Aplicacao.Util.Utilidad.eAcao.Alta
                ' [-] Aplicacao.Util.Utilidad.eAcao.Modificacion
                ' [-] Aplicacao.Util.Utilidad.eAcao.Consulta

                If Not (MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta) Then

                    Acao = Aplicacao.Util.Utilidad.eAcao.Erro

                    'Informa ao usuário que o parâmetro passado são inválidos
                    Throw New Exception(Traduzir("err_passagem_parametro"))

                End If

                If strCodAgrupacion <> String.Empty Then
                    'Estado Inicial dos control
                    CarregaDados(strCodAgrupacion)
                End If

                ' Caso a ação seja de consulta, desabilitada o treeview "TrvAgrupaciones"
                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    TrvAgrupaciones.Enabled = False
                Else
                    TrvAgrupaciones.Enabled = True
                End If

                If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion _
                    OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    txtDescricaoAgrupacion.Focus()
                Else
                    txtCodigoAgrupacion.Focus()
                End If

                'Carrega a Treeview com as posíveis divisas
                CarregaTreeViewDividasPossiveis()

            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Protected Overrides Sub PreRenderizar()

        Try
            ControleBotoes()
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()

        Master.TituloPagina = Traduzir("008_titulo_mantenimiento_agrupaciones")

        lblCodigoAgrupacion.Text = Traduzir("008_lbl_codigoagrupacion")
        lblDescricaoAgrupacion.Text = Traduzir("008_lbl_descricaoagrupacion")
        lblVigente.Text = Traduzir("008_lbl_vigente")
        lblTituloAgrupaciones.Text = Traduzir("008_lbl_titulos_mantenimento_agrupaciones")
        lblSubTitulosAgrupaciones.Text = Traduzir("008_lbl_subtitulos_mantenimento_agrupaciones")
        lblObservaciones.Text = Traduzir("008_lbl_observaciones")

        csvCodigoObrigatorio.ErrorMessage = Traduzir("008_msg_agrupacioncodigoobligatorio")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("008_msg_agrupaciondescripcionobligatorio")
        csvDescripcionExistente.ErrorMessage = Traduzir("008_msg_descricaoagrupacionexistente")
        csvCodigoAgrupacionExistente.ErrorMessage = Traduzir("008_msg_codigoagrupacionexistente")
        csvTrvAgrupaciones.ErrorMessage = Traduzir("008_msg_treeviewnodesobligatorio")

    End Sub

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Clique do botão volver
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        ExecutarVolver()
    End Sub

    ''' <summary>
    ''' Clique do botão cancelar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        ExecutarCancelar()
    End Sub

    ''' <summary>
    ''' Clique do botão gravar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()
    End Sub

    ''' <summary>
    ''' Clique do botão Incluir(p/ Treeview)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub imgBtnIncluir_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnIncluir.Click

        Try

            If TrvDivisas.SelectedNode IsNot Nothing Then
                InsereNaArvoreDinamica(TrvAgrupaciones.Nodes, MontaArvoreSelecionada(TrvDivisas.SelectedNode))
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Clique do botão excluir(p/ Treeview)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub imgBtnExcluir_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnExcluir.Click

        Try
            If TrvAgrupaciones.SelectedNode IsNot Nothing Then
                RemoveNode(TrvAgrupaciones)
            End If
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    'Ações que podem ser chamadas a qualquer momento
#Region "Ações Botões Independentes"

    ''' <summary>
    ''' Função do botão grabar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExecutarGrabar()
        Try

            Dim objProxyAgrupacion As New Comunicacion.ProxyAgrupacion
            Dim objPeticionAgrupacion As New IAC.ContractoServicio.Agrupacion.SetAgrupaciones.Peticion
            Dim objRespuestaAgrupacion As IAC.ContractoServicio.Agrupacion.SetAgrupaciones.Respuesta

            ValidarCamposObrigatorios = True

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If

            Dim objAgrupacion As New IAC.ContractoServicio.Agrupacion.SetAgrupaciones.Agrupacion

            'Recebe os valores do formulário
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objAgrupacion.Vigente = True
            Else
                objAgrupacion.Vigente = chkVigente.Checked
            End If

            ' atualizar propriedade vigente
            EsVigente = chkVigente.Checked
            objAgrupacion.Codigo = txtCodigoAgrupacion.Text.Trim
            objAgrupacion.Descripcion = txtDescricaoAgrupacion.Text.Trim
            objAgrupacion.Observacion = txtObservaciones.Text
            objAgrupacion.Efectivos = New ContractoServicio.Agrupacion.SetAgrupaciones.EfetivoColeccion
            objAgrupacion.MediosPago = New ContractoServicio.Agrupacion.SetAgrupaciones.MedioPagoColeccion

            '############################################# 
            'Obter os dados da Treeview de Agrupação

            For Each objTreeNodeDividas As TreeNode In TrvAgrupaciones.Nodes

                For Each objTreeNodeTipoMedioPago As TreeNode In objTreeNodeDividas.ChildNodes

                    If objTreeNodeTipoMedioPago.Text.Equals(Traduzir(TreeViewNodeEfectivo)) Then

                        Dim objEfectivo As New ContractoServicio.Agrupacion.SetAgrupaciones.Efectivo
                        objEfectivo.CodigoIsoDivisa = objTreeNodeTipoMedioPago.Value
                        objAgrupacion.Efectivos.Add(objEfectivo)

                    Else

                        For Each objTreeNodeMedioPago As TreeNode In objTreeNodeTipoMedioPago.ChildNodes

                            Dim objMedioPago As New ContractoServicio.Agrupacion.SetAgrupaciones.MedioPago
                            objMedioPago.CodigoIsoDivisa = objTreeNodeDividas.Value
                            objMedioPago.CodigoTipoMedioPago = objTreeNodeTipoMedioPago.Value
                            objMedioPago.CodigoMedioPago = objTreeNodeMedioPago.Value
                            objAgrupacion.MediosPago.Add(objMedioPago)

                        Next
                    End If

                Next
            Next

            '#############################################            

            'Cria a coleção para envio
            Dim objColAgrupacion As New IAC.ContractoServicio.Agrupacion.SetAgrupaciones.AgrupacionColeccion
            objColAgrupacion.Add(objAgrupacion)

            'Passa a coleção para a agrupação
            objPeticionAgrupacion.Agrupaciones = objColAgrupacion
            objPeticionAgrupacion.CodigoUsuario = MyBase.LoginUsuario

            'Obtem o objeto de resposta para validação
            objRespuestaAgrupacion = objProxyAgrupacion.SetAgrupaciones(objPeticionAgrupacion)

            If Master.ControleErro.VerificaErro(objRespuestaAgrupacion.CodigoError, objRespuestaAgrupacion.NombreServidorBD, objRespuestaAgrupacion.MensajeError) Then

                If Master.ControleErro.VerificaErro(objRespuestaAgrupacion.RespuestaAgrupaciones(0).CodigoError, objRespuestaAgrupacion.NombreServidorBD, objRespuestaAgrupacion.RespuestaAgrupaciones(0).MensajeError) Then

                    Response.Redirect("~/BusquedaAgrupaciones.aspx", False)

                End If

            Else

                If objRespuestaAgrupacion.RespuestaAgrupaciones IsNot Nothing _
                    AndAlso objRespuestaAgrupacion.RespuestaAgrupaciones.Count > 0 _
                    AndAlso objRespuestaAgrupacion.RespuestaAgrupaciones(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then

                    Master.ControleErro.ShowError(objRespuestaAgrupacion.RespuestaAgrupaciones(0).MensajeError, False)

                End If

            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' função do botão volver
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExecutarVolver()
        Response.Redirect("~/BusquedaAgrupaciones.aspx", False)
    End Sub

    ''' <summary>
    ''' Função do botão cancelar.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExecutarCancelar()
        Response.Redirect("~/BusquedaAgrupaciones.aspx", False)
    End Sub
#End Region


#End Region

#Region "[EVENTOS TEXTBOX]"

    ''' <summary>
    ''' Evento de mudança de texto no campo código da agrupação
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtCodigoAgrupacion_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoAgrupacion.TextChanged

        If ExisteCodigoAgrupacion(txtCodigoAgrupacion.Text) Then
            CodigoExistente = True
        Else
            CodigoExistente = False
        End If

        Threading.Thread.Sleep(100)
    End Sub

    ''' <summary>
    ''' Evento de mudança de texto no campo descrição da agrupação
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtDescricaoAgrupacion_TextChanged(sender As Object, e As System.EventArgs) Handles txtDescricaoAgrupacion.TextChanged
        Try

            If ExisteDescricaoAgrupacion(txtDescricaoAgrupacion.Text) Then
                DescricaoExistente = True
            Else
                DescricaoExistente = False
            End If

            Threading.Thread.Sleep(100)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

#End Region

#Region "[EVENTOS TREEVIEW]"

    ''' <summary>
    ''' Evento SelectedNodeChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TrvDivisas_SelectedNodeChanged(sender As Object, e As EventArgs) Handles TrvDivisas.SelectedNodeChanged
        Try
            System.Threading.Thread.Sleep(100)
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento SelectedNodeChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TrvAgrupaciones_SelectedNodeChanged(sender As Object, e As EventArgs) Handles TrvAgrupaciones.SelectedNodeChanged
        Try
            System.Threading.Thread.Sleep(100)
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub
#End Region

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Carrega os dados do gridView quando a página é carregada pela primeira vez. Modo de Modificação
    ''' </summary>
    ''' <param name="codAgrupacion"></param>
    ''' <remarks></remarks>
    Public Sub CarregaDados(codAgrupacion As String)

        Dim objColAgrupacion As IAC.ContractoServicio.Agrupacion.GetAgrupacionesDetail.AgrupacionColeccion
        objColAgrupacion = getAgrupacion(codAgrupacion)

        If objColAgrupacion IsNot Nothing AndAlso objColAgrupacion.Count > 0 Then

            'Preenche os controles do formulario
            txtCodigoAgrupacion.Text = objColAgrupacion(0).Codigo
            txtCodigoAgrupacion.ToolTip = objColAgrupacion(0).Codigo

            txtDescricaoAgrupacion.Text = objColAgrupacion(0).Descripcion
            txtDescricaoAgrupacion.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objColAgrupacion(0).Descripcion, String.Empty)

            txtObservaciones.Text = objColAgrupacion(0).Observacion
            chkVigente.Checked = objColAgrupacion(0).Vigente

            ' preenche a propriedade da tela
            EsVigente = objColAgrupacion(0).Vigente

            'Se for modificação então guarda a descriçaõ atual para validação
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                DescricaoAtual = txtDescricaoAgrupacion.Text
            End If

            If objColAgrupacion(0).Divisas IsNot Nothing AndAlso objColAgrupacion(0).Divisas.Count > 0 Then
                CarregaTreeview(TrvAgrupaciones, objColAgrupacion(0).Divisas, eExpadirNivel.Segundo)
            End If

        End If

    End Sub

    ''' <summary>
    ''' MontaMensagensErro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False


        If Page.IsPostBack Then

            'Verifica se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Verifica se o código do canal é obrigatório
                If txtCodigoAgrupacion.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoAgrupacion.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If txtDescricaoAgrupacion.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoAgrupacion.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If TrvAgrupaciones.Nodes.Count = 0 Then

                    strErro.Append(csvTrvAgrupaciones.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTrvAgrupaciones.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        TrvAgrupaciones.Focus()
                        focoSetado = True
                    End If

                Else
                    csvTrvAgrupaciones.IsValid = True
                End If

            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If CodigoExistente Then

                strErro.Append(csvCodigoAgrupacionExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoAgrupacionExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoAgrupacion.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoAgrupacionExistente.IsValid = True
            End If

            'Verifica se a descrição existe
            If DescricaoExistente Then

                strErro.Append(csvDescripcionExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescripcionExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescricaoAgrupacion.Focus()
                    focoSetado = True
                End If

            Else
                csvDescripcionExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' Informa se o código da agrupacion já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo] 01/07/2009 - Criado
    ''' </history>
    Private Function ExisteCodigoAgrupacion(codigoAgrupacion As String) As Boolean

        Dim objRespostaVerificarCodigoAgrupacion As IAC.ContractoServicio.Agrupacion.VerificarCodigoAgrupacion.Respuesta
        Try

            Dim objProxyAgrupacion As New Comunicacion.ProxyAgrupacion
            Dim objPeticionVerificarCodigoAgrupacion As New IAC.ContractoServicio.Agrupacion.VerificarCodigoAgrupacion.Peticion

            'Verifica se o código do Agrupacion existe no BD
            objPeticionVerificarCodigoAgrupacion.CodigoAgrupacion = codigoAgrupacion
            objRespostaVerificarCodigoAgrupacion = objProxyAgrupacion.VerificarCodigoAgrupacion(objPeticionVerificarCodigoAgrupacion)

            If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoAgrupacion.CodigoError, objRespostaVerificarCodigoAgrupacion.NombreServidorBD, objRespostaVerificarCodigoAgrupacion.MensajeError) Then
                Return objRespostaVerificarCodigoAgrupacion.Existe
            Else
                Master.ControleErro.ShowError(objRespostaVerificarCodigoAgrupacion.MensajeError)
                Return False
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Function

    ''' <summary>
    ''' Informa se a descrição da agrupacion já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/07/2009 - Criado
    ''' </history>
    Private Function ExisteDescricaoAgrupacion(descricao As String) As Boolean
        Dim objRespostaVerificarDescricaoAgrupacion As IAC.ContractoServicio.Agrupacion.VerificarDescripcionAgrupacion.Respuesta
        Try

            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then

                If descricao.Trim.Equals(DescricaoAtual) Then
                    DescricaoExistente = False
                    Return False
                End If

            End If

            Dim objProxyAgrupacion As New Comunicacion.ProxyAgrupacion
            Dim objPeticionVerificarDescricaoAgrupacion As New IAC.ContractoServicio.Agrupacion.VerificarDescripcionAgrupacion.Peticion

            'Verifica se o código do Agrupacion existe no BD
            objPeticionVerificarDescricaoAgrupacion.DescripcionAgrupacion = txtDescricaoAgrupacion.Text.Trim
            objRespostaVerificarDescricaoAgrupacion = objProxyAgrupacion.VerificarDescripcionAgrupacion(objPeticionVerificarDescricaoAgrupacion)

            If Master.ControleErro.VerificaErro(objRespostaVerificarDescricaoAgrupacion.CodigoError, objRespostaVerificarDescricaoAgrupacion.NombreServidorBD, objRespostaVerificarDescricaoAgrupacion.MensajeError) Then
                Return objRespostaVerificarDescricaoAgrupacion.Existe
            Else
                Master.ControleErro.ShowError(objRespostaVerificarDescricaoAgrupacion.MensajeError) 'TODO: Exibe a mensagem de erro. Apenas para Debug. Retirar para Release.
                Return False
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Function

    ''' <summary>
    ''' Carrega os dados na treeview quando a página é carregada pela primeira vez
    ''' </summary>    
    ''' <remarks></remarks>
    Public Sub CarregaTreeViewDividasPossiveis()

        CarregaTreeview(TrvDivisas, getDivisasPosiveis, eExpadirNivel.Primeiro)

    End Sub

    ''' <summary>
    ''' Retorna as divisas posíveis que serão apresentadas na treview
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDivisasPosiveis() As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.DivisaColeccion

        ' invocar logica negocio
        Dim objProxy As New Comunicacion.ProxyUtilidad
        'Dim lnAccionAgrupacion As New LogicaNegocio.AccionAgrupacion
        Dim objRespuesta As ContractoServicio.Utilidad.GetDivisasMedioPago.Respuesta = objProxy.GetDivisasMedioPago()

        If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Return objRespuesta.Divisas
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Retorna a agrupação passada como parâmetro
    ''' </summary>
    ''' <param name="pstrCodAgrupacao"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getAgrupacion(pstrCodAgrupacao As String) As IAC.ContractoServicio.Agrupacion.GetAgrupacionesDetail.AgrupacionColeccion

        Dim objPeticion As New ContractoServicio.Agrupacion.GetAgrupacionesDetail.Peticion
        objPeticion.CodigoAgrupacion = New List(Of String)
        objPeticion.CodigoAgrupacion.Add(pstrCodAgrupacao)

        ' invocar logica negocio
        Dim objProxy As New Comunicacion.ProxyAgrupacion
        'Dim lnAccionAgrupacion As New LogicaNegocio.AccionAgrupacion
        Dim objRespuesta As ContractoServicio.Agrupacion.GetAgrupacionesDetail.Respuesta = objProxy.GetAgrupacionesDetail(objPeticion)

        If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Return objRespuesta.Agrupaciones
        Else
            Return Nothing
        End If


    End Function

    ''' <summary>
    ''' Método que carrega a Treeview com uma coleção de Divisas (getDetail)
    ''' </summary>
    ''' <param name="pobjTreeView"></param>
    ''' <param name="pObjColDivisas"></param>
    ''' <remarks></remarks>
    Public Sub CarregaTreeview(ByRef pobjTreeView As TreeView, pObjColDivisas As IAC.ContractoServicio.Agrupacion.GetAgrupacionesDetail.DivisaColeccion, pNaoExpandirAPartirNivel As eExpadirNivel)

        pobjTreeView.Nodes.Clear()
        If pObjColDivisas IsNot Nothing Then
            For Each objDivisa As IAC.ContractoServicio.Agrupacion.GetAgrupacionesDetail.Divisa In pObjColDivisas

                Dim objTreeNodeDivisa As New TreeNode(objDivisa.Descripcion, objDivisa.CodigoIso)
                Dim objTreeNodeTipoMedioPago As TreeNode
                Dim objTreeNodeMedioPago As TreeNode

                If objDivisa.TieneEfectivo Then
                    'Adiciona o nó efetivo
                    objTreeNodeTipoMedioPago = New TreeNode(Traduzir(TreeViewNodeEfectivo), objDivisa.CodigoIso)
                    objTreeNodeDivisa.ChildNodes.Add(objTreeNodeTipoMedioPago)
                End If

                For Each TipoMedioPago As IAC.ContractoServicio.Agrupacion.GetAgrupacionesDetail.TipoMedioPago In objDivisa.TiposMedioPago
                    'Adiciona Nós de Tipo Médio Pago
                    objTreeNodeTipoMedioPago = New TreeNode(TipoMedioPago.Descripcion, TipoMedioPago.Codigo)

                    For Each MedioPago As IAC.ContractoServicio.Agrupacion.GetAgrupacionesDetail.MedioPago In TipoMedioPago.MediosPago
                        'Adiciona Nós de Médio Pago
                        objTreeNodeMedioPago = New TreeNode(MedioPago.Descripcion, MedioPago.Codigo)
                        objTreeNodeTipoMedioPago.ChildNodes.AddAt(IndiceOrdenacao(objTreeNodeTipoMedioPago.ChildNodes, objTreeNodeMedioPago), objTreeNodeMedioPago)
                    Next

                    If Not (pNaoExpandirAPartirNivel = eExpadirNivel.Segundo) Then
                        objTreeNodeTipoMedioPago.Expanded = False 'Não expande o nó 
                    Else
                        objTreeNodeTipoMedioPago.Expanded = True 'Expande o nó 
                    End If

                    objTreeNodeDivisa.ChildNodes.AddAt(IndiceOrdenacao(objTreeNodeDivisa.ChildNodes, objTreeNodeTipoMedioPago), objTreeNodeTipoMedioPago)

                Next

                If pNaoExpandirAPartirNivel = eExpadirNivel.Primeiro Then
                    objTreeNodeDivisa.Expanded = False
                Else
                    objTreeNodeDivisa.Expanded = True
                End If

                'Adiciona a divisa na Tree
                pobjTreeView.Nodes.AddAt(IndiceOrdenacao(pobjTreeView.Nodes, objTreeNodeDivisa), objTreeNodeDivisa)

            Next
        End If

    End Sub

    ''' <summary>
    ''' Método que carrega a Treeview com uma coleção de Divisas (Utilidad)
    ''' </summary>
    ''' <param name="pobjTreeView"></param>
    ''' <param name="pObjColDivisas"></param>
    ''' <remarks></remarks>
    Public Sub CarregaTreeview(ByRef pobjTreeView As TreeView, pObjColDivisas As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.DivisaColeccion, pNaoExpandirAPartirNivel As eExpadirNivel)

        pobjTreeView.Nodes.Clear()

        If pObjColDivisas IsNot Nothing Then
            For Each objDivisa As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.Divisa In pObjColDivisas

                Dim objTreeNodeDivisa As New TreeNode(objDivisa.Descripcion, objDivisa.CodigoIso)
                Dim objTreeNodeTipoMedioPago As TreeNode
                Dim objTreeNodeMedioPago As TreeNode

                'Adiciona o nó efetivo
                objTreeNodeTipoMedioPago = New TreeNode(Traduzir(TreeViewNodeEfectivo), objDivisa.CodigoIso)
                objTreeNodeDivisa.ChildNodes.Add(objTreeNodeTipoMedioPago)


                For Each TipoMedioPago As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.TipoMedioPago In objDivisa.TiposMedioPago
                    'Adiciona Nós de Tipo Médio Pago
                    objTreeNodeTipoMedioPago = New TreeNode(TipoMedioPago.Descripcion, TipoMedioPago.Codigo)

                    For Each MedioPago As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.MedioPago In TipoMedioPago.MediosPago
                        'Adiciona Nós de Médio Pago
                        objTreeNodeMedioPago = New TreeNode(MedioPago.Descripcion, MedioPago.Codigo)
                        objTreeNodeTipoMedioPago.ChildNodes.AddAt(IndiceOrdenacao(objTreeNodeTipoMedioPago.ChildNodes, objTreeNodeMedioPago), objTreeNodeMedioPago)
                    Next

                    'Não expande o nó 
                    If pNaoExpandirAPartirNivel = eExpadirNivel.Segundo Then
                        objTreeNodeTipoMedioPago.Expanded = False
                    Else
                        objTreeNodeTipoMedioPago.Expanded = True
                    End If

                    objTreeNodeDivisa.ChildNodes.AddAt(IndiceOrdenacao(objTreeNodeDivisa.ChildNodes, objTreeNodeTipoMedioPago), objTreeNodeTipoMedioPago)

                Next

                If pNaoExpandirAPartirNivel = eExpadirNivel.Primeiro Then
                    objTreeNodeDivisa.Expanded = False
                Else
                    objTreeNodeDivisa.Expanded = True
                End If

                'Adiciona a divisa na Tree
                pobjTreeView.Nodes.AddAt(IndiceOrdenacao(pobjTreeView.Nodes, objTreeNodeDivisa), objTreeNodeDivisa)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Enumerado de níveis a expandir
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eExpadirNivel As Integer

        Primeiro = 0
        Segundo = 1
        Nenhum = 2

    End Enum

#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()

        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta
                btnGrabar.Visible = True        '1
                btnCancelar.Visible = True      '2
                btnVolver.Visible = False       '3

                'Estado Inicial Controles                                
                txtCodigoAgrupacion.Enabled = True
                lblVigente.Visible = False
                chkVigente.Checked = True
                chkVigente.Enabled = False
                chkVigente.Visible = False
                btnGrabar.Habilitado = True
                TrvDivisas.Visible = True
                imgBtnIncluir.Visible = True
                imgBtnExcluir.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                btnGrabar.Visible = False               '1
                btnCancelar.Visible = False             '2
                btnVolver.Visible = True                '3

                'Estado Inicial Controles
                txtCodigoAgrupacion.Enabled = False
                txtDescricaoAgrupacion.Enabled = False
                txtObservaciones.Enabled = False
                lblVigente.Visible = True
                chkVigente.Enabled = False
                'TdTreeViewDivisa.Style.Add("border-width", "0px")
                TrvDivisas.Visible = False
                imgBtnIncluir.Visible = False
                imgBtnExcluir.Visible = False
                TdTreeViewDivisa.Visible = False
                TdBtnInEx.Visible = False

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                btnGrabar.Visible = True               '1
                btnCancelar.Visible = True             '2
                btnVolver.Visible = False              '3

                txtCodigoAgrupacion.Enabled = False
                chkVigente.Visible = True
                lblVigente.Visible = True

                If chkVigente.Checked AndAlso EsVigente Then
                    chkVigente.Enabled = False
                Else
                    chkVigente.Enabled = True
                End If

                btnGrabar.Habilitado = True

                TrvDivisas.Visible = True
                imgBtnIncluir.Visible = True
                imgBtnExcluir.Visible = True


            Case Aplicacao.Util.Utilidad.eAcao.NoAction
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Busca
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.NoAction

            Case Aplicacao.Util.Utilidad.eAcao.Erro

                btnGrabar.Visible = False        '1
                btnCancelar.Visible = False      '2                
                btnVolver.Visible = True         '3

        End Select

        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem de erro
        If MontaMensagensErro() <> String.Empty Then
            Master.ControleErro.ShowError(MontaMensagensErro, False)
        End If

    End Sub

    Private Property DescricaoAtual() As String
        Get
            Return ViewState("DescricaoAtual")
        End Get
        Set(value As String)
            ViewState("DescricaoAtual") = value.Trim
        End Set
    End Property

#End Region

#Region "[PROPRIEDADES]"

    Private Property CodigoExistente() As Boolean
        Get
            Return ViewState("CodigoExistente")
        End Get
        Set(value As Boolean)
            ViewState("CodigoExistente") = value
        End Set
    End Property

    Private Property DescricaoExistente() As Boolean
        Get
            Return ViewState("DescricaoExistente")
        End Get
        Set(value As Boolean)
            ViewState("DescricaoExistente") = value
        End Set
    End Property

    Private Property EsVigente() As Boolean
        Get
            If ViewState("EsVigente") Is Nothing Then
                ViewState("EsVigente") = False
            End If
            Return ViewState("EsVigente")
        End Get
        Set(value As Boolean)
            ViewState("EsVigente") = value
        End Set
    End Property

    Private Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property

#End Region

#Region "[ÁRVORE BINÁRIA]"

    ''' <summary>
    ''' Copia o nó
    ''' </summary>
    ''' <param name="objNode">Nó selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CopyNode(objNode As TreeNode) As TreeNode

        Dim TempNode As New TreeNode
        TempNode.Text = objNode.Text
        TempNode.Value = objNode.Value
        TempNode.Selected = objNode.Selected
        TempNode.Expanded = objNode.Expanded
        TempNode.ImageUrl = objNode.ImageUrl
        TempNode.ToolTip = objNode.ToolTip

        Return TempNode

    End Function

    ''' <summary>
    ''' Retorna os filhos de um nó selecionado
    ''' </summary>
    ''' <param name="objTreeNode">No Selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getChilds(objTreeNode As TreeNode) As TreeNode

        Dim objTreeNodeRetorno As TreeNode = CopyNode(objTreeNode)

        If objTreeNode.ChildNodes.Count > 0 Then
            Dim objFilhoRetorno As TreeNode
            For Each objFilho As TreeNode In objTreeNode.ChildNodes
                objFilhoRetorno = getChilds(objFilho)
                objTreeNodeRetorno.ChildNodes.Add(objFilhoRetorno)
            Next
        End If

        Return objTreeNodeRetorno

    End Function

    ''' <summary>
    ''' Retorna os páis do nó de um nó selecionado
    ''' </summary>
    ''' <param name="objTreeNode">No Selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getParent(ByRef objTreeNode As TreeNode)

        Dim objTreeNodeCorrente As TreeNode = CopyNode(objTreeNode)
        If objTreeNode.Parent IsNot Nothing Then
            Dim objPai As TreeNode = getParent(objTreeNode.Parent)
            objPai.ChildNodes.Add(objTreeNodeCorrente)
            Return objPai.ChildNodes(0)
        Else
            Return objTreeNodeCorrente
        End If

    End Function

    ''' <summary>
    ''' Retorna o nó selecionado de forma hierárquica
    ''' </summary>
    ''' <param name="pObjSelecionado">Objeto nó selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MontaArvoreSelecionada(pObjSelecionado As TreeNode) As TreeNode

        'Se for o nível de raiz,inclui os filhos
        If pObjSelecionado.Depth = 0 Then

            Dim objNoFilhos As TreeNode = getChilds(pObjSelecionado)
            Return objNoFilhos

        Else

            'Dado um objeto nó selecionado, retorna os pais e filhos a serem inseridos na coleção
            Dim objNoSelecionado As TreeNode = getParent(pObjSelecionado)
            Dim objNoFilhos As TreeNode = getChilds(pObjSelecionado)

            'Adiciona os filhos
            If objNoSelecionado IsNot Nothing Then
                Dim objTreeNodeChildCol As TreeNodeCollection = objNoFilhos.ChildNodes
                While objTreeNodeChildCol.Count > 0
                    objNoSelecionado.ChildNodes.Add(objNoFilhos.ChildNodes(0))
                End While
            End If

            'Suspende para o nível Root
            Dim objGetFather As TreeNode = objNoSelecionado
            While objGetFather.Parent IsNot Nothing
                objGetFather = objGetFather.Parent
            End While

            'Retorna o nó selecionado de forma hierárquica
            Return objGetFather
        End If

    End Function

    ''' <summary>
    ''' Remode o nó selecionado da Treeview informada
    ''' </summary>
    ''' <param name="pObjTreeView">Treevie a ser retirado o nó</param>
    ''' <remarks></remarks>
    Public Sub RemoveNode(ByRef pObjTreeView As TreeView)

        Dim objPai As TreeNode = pObjTreeView.SelectedNode.Parent
        Dim objDelete As TreeNode = pObjTreeView.SelectedNode

        While objPai IsNot Nothing
            objPai.ChildNodes.Remove(objDelete)

            If objPai.ChildNodes.Count = 0 Then
                objDelete = objPai
                objPai = objPai.Parent
            Else
                Exit While
            End If
        End While

        If objDelete IsNot Nothing Then
            pObjTreeView.Nodes.Remove(objDelete)
        End If

    End Sub

    ''' <summary>
    ''' Posiciona o nó selecionado na árvore de destino
    ''' </summary>
    ''' <param name="pObjTreeView">Coleção de nós a ser verificada</param>
    ''' <param name="pObjSelecionado">Objeto selecionado(Hierárquico)</param>
    ''' <remarks></remarks>
    Public Sub InsereNaArvoreDinamica(pObjTreeView As TreeNodeCollection, pObjSelecionado As TreeNode)

        Dim objExiste As TreeNode = pObjSelecionado
        Dim ObjColCorrente As TreeNodeCollection = pObjTreeView

        'Caso não exista nenhum nó na árvore adiciona o primeiro
        If ObjColCorrente.Count = 0 Then
            ObjColCorrente.Add(objExiste)
            Exit Sub
        End If

        While objExiste IsNot Nothing

            Dim addNo As Boolean = True
            'Verifica na árvore de destino se o objeto selecionado existe
            For Each pObjSelecao As TreeNode In ObjColCorrente
                If pObjSelecao.Text = objExiste.Text Then
                    'Se existe filho então continua o processamento
                    If pObjSelecao.ChildNodes.Count > 0 Then
                        'Se selecionou um nó pai inclui novamente os filhos a partir da seleção efetuada
                        If objExiste.ChildNodes.Count > 0 AndAlso objExiste.Selected Then
                            pObjSelecao.ChildNodes.Clear()
                            Dim objNoSelecionado As TreeNode = pObjSelecao
                            'Seleciona o nó
                            objNoSelecionado.Selected = True

                            Dim objNoFilhos As TreeNode = getChilds(objExiste)
                            If objNoSelecionado IsNot Nothing Then
                                Dim objTreeNodeChildCol As TreeNodeCollection = objNoFilhos.ChildNodes
                                While objTreeNodeChildCol.Count > 0
                                    objNoSelecionado.ChildNodes.Add(objNoFilhos.ChildNodes(0))
                                End While
                            End If
                            Exit Sub
                        End If
                        'Passa o próximo filho do objeto selecionado para ser verificado
                        objExiste = objExiste.ChildNodes(0)
                        'Passa a próxima coleção da árvore de destino para ser verificada
                        ObjColCorrente = pObjSelecao.ChildNodes
                        addNo = False
                        Exit For
                    Else
                        'Seleciona o nó
                        pObjSelecao.Selected = True
                        Exit Sub
                    End If
                End If

            Next

            'Adiciona na árvore de forma ordenada
            If addNo Then
                ObjColCorrente.AddAt(IndiceOrdenacao(ObjColCorrente, objExiste), objExiste)
                Exit While
            End If

        End While

    End Sub

    ''' <summary>
    ''' Retorna o índice antes de inserir o nó na coleção passada
    ''' </summary>
    ''' <param name="TreeNodeCol">Coleção a ser verificada a posição</param>
    ''' <param name="treenode">Nó para inclusão</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IndiceOrdenacao(TreeNodeCol As TreeNodeCollection, treenode As TreeNode) As Integer

        Dim treeNodeColTemp As New TreeNodeCollection

        For Each obj As TreeNode In TreeNodeCol
            treeNodeColTemp.Add(CopyNode(obj))
        Next

        If treeNodeColTemp.Count > 0 Then
            treeNodeColTemp.Add(CopyNode(treenode))

            Dim retorno = From objTree In treeNodeColTemp Order By objTree.Text Ascending

            Dim i As Integer = 0
            For Each objRetorno As Object In retorno
                If objRetorno.text = treenode.Text Then
                    Return i
                End If
                i += 1
            Next
        End If

        Return 0

    End Function

#End Region

End Class