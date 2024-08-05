Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' Página de Gerenciamento de IAC 
''' </summary>
''' <remarks></remarks>
''' <history>[anselmo.gois] 02/02/09 - Criado</history>
Partial Public Class MantenimientoIac
    Inherits Base

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

        'seta o foco para o proximo controle quando presciona o enter.
        txtCodigoIac.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoIac.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoIac.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtObservacionesIac.ClientID & "').focus();return false;}} else {return true}; ")
        txtObservacionesIac.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtObservacionesIac.ClientID & "','" & txtObservacionesIac.MaxLength & "');")
        txtObservacionesIac.Attributes.Add("onblur", "limitaCaracteres('" & txtObservacionesIac.ClientID & "','" & txtObservacionesIac.MaxLength & "');")
        txtObservacionesIac.Attributes.Add("onkeyup", "limitaCaracteres('" & txtObservacionesIac.ClientID & "','" & txtObservacionesIac.MaxLength & "');")

        'Controle de precedência(Ajax)

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)
    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigoIac.TabIndex = 1
        txtDescricaoIac.TabIndex = 2
        txtObservacionesIac.TabIndex = 3
        chkVigente.TabIndex = 4
        chkInvisible.TabIndex = 5
        chkDisponivelNuevoSaldos.TabIndex = 6
        ProsegurGridView1.TabIndex = 7
        btnAdiciona.TabIndex = 8
        btnremove.TabIndex = 9
        ProsegurGridView2.TabIndex = 10
        btnAcima.TabIndex = 11
        btnAbaixo.TabIndex = 12
        btnGrabar.TabIndex = 13
        btnCancelar.TabIndex = 14
        btnVolver.TabIndex = 15

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.INFORMACION_ADICIONAL_CLIENTE
        ' não deve adicionar scripts no load da base
        MyBase.AddScripts = False

    End Sub

    Protected Overrides Sub Inicializar()

        Try
            If Not Page.IsPostBack Then

                'Recebe o código do Canal
                Dim strCodIac As String = Request.QueryString("codiac")
                'Possíveis Ações passadas pela página BusqueadaTiposProcesado:
                ' [-] Aplicacao.Util.Utilidad.eAcao.Alta
                ' [-] Aplicacao.Util.Utilidad.eAcao.Modificacion
                ' [-] Aplicacao.Util.Utilidad.eAcao.Consulta

                If Not (MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta) Then

                    'Informa ao usuário que o parâmetro passado 
                    Throw New Exception(Traduzir("err_passagem_parametro"))

                End If

                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion _
                    OrElse MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then

                    txtDescricaoIac.Focus()
                Else
                    txtCodigoIac.Focus()
                End If
                CabecalhoVazioTerminos()
                CabecalhoVazioTerminosIac()
                CarregaDados(strCodIac)
                CarregaGridTerminosIac()
                getTerminos()
                CarregaGrid()
                AdicionarScripts()

            End If

            AdicionaJavaScriptDesabilitaControle()
            TrataFoco()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
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

        lblCodigoIac.Text = Traduzir("006_lbl_codigoiac")
        lblDescricaoIac.Text = Traduzir("006_lbl_descripcioniac")

        lblVigente.Text = Traduzir("006_chk_vigente")
        chkVigente.Text = String.Empty
        lblInvisible.Text = Traduzir("006_chk_invisible")
        chkInvisible.Text = String.Empty

        lblCopiarDeclarados.Text = Traduzir("006_lbl_copiardeclarado")
        chkCopiarDeclarados.Text = String.Empty

        lblDisponivelNuevoSaldos.Text = Traduzir("006_lbl_disponivelnuevosaldos")
        chkDisponivelNuevoSaldos.Text = String.Empty

        lblTituloIac.Text = Traduzir("006_titulo_matenimentoiac")
        lblObservacionesIac.Text = Traduzir("006_lbl_observacion")
        csvCodigoObrigatorio.ErrorMessage = Traduzir("006_msg_iaccodigoobligatorio")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("006_msg_iacdescripcionobligatorio")
        csvCodigoExistente.ErrorMessage = Traduzir("006_msg_codigoiacexistente")
        csvDescricaoExistente.ErrorMessage = Traduzir("006_msg_descricaoiacexistente")
        csvTerminoObligatorio.ErrorMessage = Traduzir("006_msg_iacTerminoObligatorio")

        Master.TituloPagina = Traduzir("006_title_mantenimentoiac")

        'ProsegurGridView1.PagerSummary = Traduzir("grid_lbl_pagersummary")

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Propriedades
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/02/2009 Criado
    ''' </history> 
    Public Property Iac() As IAC.ContractoServicio.Iac.GetIac.Iac
        Get
            Return DirectCast(ViewState("Iac"), IAC.ContractoServicio.Iac.GetIac.Iac)
        End Get
        Set(value As IAC.ContractoServicio.Iac.GetIac.Iac)
            ViewState("Iac") = value
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

    Private Property DescricaoExistente() As Boolean
        Get
            Return ViewState("DescricaoExistente")
        End Get
        Set(value As Boolean)
            ViewState("DescricaoExistente") = value
        End Set
    End Property

    Public Property TerminosIacTemPorario() As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion
        Get
            If ViewState("TerminosIacTemPorario") Is Nothing Then
                ViewState("TerminosIacTemPorario") = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion
            End If

            Return DirectCast(ViewState("TerminosIacTemPorario"), IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion)
        End Get

        Set(value As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion)
            ViewState("TerminosIacTemPorario") = value
        End Set
    End Property

    Public Property TerminosTemPorario() As IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion
        Get
            If ViewState("TerminosTemPorario") Is Nothing Then
                ViewState("TerminosTemPorario") = New IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion
            End If

            Return DirectCast(ViewState("TerminosTemPorario"), IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion)
        End Get

        Set(value As IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion)
            ViewState("TerminosTemPorario") = value
        End Set
    End Property

    Public Property TerminosIacCompletos() As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion
        Get
            If ViewState("TerminosIacCompletos") Is Nothing Then
                ViewState("TerminosIacCompletos") = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion
            End If

            Return DirectCast(ViewState("TerminosIacCompletos"), IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion)
        End Get

        Set(value As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion)
            ViewState("TerminosIacCompletos") = value
        End Set
    End Property

    Private Property ValorDescricao() As String
        Get
            Return ViewState("ValorDescricao")
        End Get
        Set(value As String)
            ViewState("ValorDescricao") = value
        End Set
    End Property

    Private Property ValorCodigo() As String
        Get
            Return ViewState("ValorCodigo")
        End Get
        Set(value As String)
            ViewState("ValorCodigo") = value
        End Set
    End Property

    Private Property ValorVigente() As Boolean
        Get
            Return ViewState("ValorVigente")
        End Get
        Set(value As Boolean)
            ViewState("ValorVigente") = value
        End Set
    End Property

    Private Property ValorInvisible() As Boolean
        Get
            Return ViewState("ValorInvisible")
        End Get
        Set(value As Boolean)
            ViewState("ValorInvisible") = value
        End Set
    End Property

    Private Property Salvar() As Boolean
        Get
            Return ViewState("Salvar")
        End Get
        Set(value As Boolean)
            ViewState("Salvar") = value
        End Set
    End Property

    Private Property ValorObservaciones() As String
        Get
            Return ViewState("ValorObservaciones")
        End Get
        Set(value As String)
            ViewState("ValorObservaciones") = value
        End Set
    End Property

    Private Property EsVigente() As Boolean
        Get
            Return ViewState("EsVigente")
        End Get
        Set(value As Boolean)
            If ViewState("EsVigente") Is Nothing Then
                ViewState("EsVigente") = value
            End If
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

    ''' <summary>
    '''  Armazena a descrição atual.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/02/2009 Criado
    ''' </history> 
    Private Property DescricaoAtual() As String
        Get
            Return ViewState("DescricaoAtual")
        End Get
        Set(value As String)
            ViewState("DescricaoAtual") = value.Trim
        End Set
    End Property

#End Region

#Region "[DADOS]"

    ''' <summary>
    ''' Metodo faz a chamada do metodo getIacDetail do acesso datos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/02/2009 Criado
    ''' </history>
    Public Function getIacDetail(codigoCanal As String) As IAC.ContractoServicio.Iac.GetIacDetail.IacColeccion

        Dim objProxyIac As New Comunicacion.ProxyIac
        Dim objPeticionIac As New IAC.ContractoServicio.Iac.GetIacDetail.Peticion
        Dim objRespuestaIac As New IAC.ContractoServicio.Iac.GetIacDetail.Respuesta
        Dim objTerminosIac As New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac


        'Recebe os valores do filtro
        Dim lista As New List(Of String)
        lista.Add(codigoCanal)

        objPeticionIac.CodidoIac = lista

        objRespuestaIac = objProxyIac.GetIacDetail(objPeticionIac)
        If objRespuestaIac.Iacs.Count > 0 Then
            TerminosIacTemPorario = objRespuestaIac.Iacs.Item(0).TerminosIac
            TerminosIacCompletos = objRespuestaIac.Iacs.Item(0).TerminosIac
            Return objRespuestaIac.Iacs
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Metodo faz a chamada do metodo getterminos do acesso datos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/02/2009 Criado
    ''' </history>
    Public Sub getTerminos()

        Dim objProxyTermino As New Comunicacion.ProxyTermino
        Dim objPeticionTermino As New IAC.ContractoServicio.TerminoIac.GetTerminoIac.Peticion
        Dim objRespuestaTermino As IAC.ContractoServicio.TerminoIac.GetTerminoIac.Respuesta

        'Passa os parametros
        objPeticionTermino.MostrarCodigo = Nothing
        objPeticionTermino.VigenteTermino = True

        'chama o proxytermino
        objRespuestaTermino = objProxyTermino.getTerminos(objPeticionTermino)

        'faz a validaçãos dos dados recebidos pelo metodo gettermino e so mostra os dados que não existem nos terminos iac
        Dim objTerminos As New IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIac

        If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
            If TerminosIacTemPorario.Count > 0 Then
                For Each objTerminos In objRespuestaTermino.TerminosIac

                    If SelectTerminosIac(TerminosIacTemPorario, objTerminos.Codigo) = False Then

                        TerminosTemPorario.Add(objTerminos)

                    End If
                Next
            Else
                TerminosTemPorario = objRespuestaTermino.TerminosIac
            End If
        Else
            TerminosTemPorario = objRespuestaTermino.TerminosIac
        End If
    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Trata o foco da página
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrataFoco()

        If (Not IsPostBack) Then
            Aplicacao.Util.Utilidad.HookOnFocus(DirectCast(Me.Page, Control))
        Else
            If Request("__LASTFOCUS") IsNot Nothing AndAlso Request("__LASTFOCUS") <> String.Empty Then
                Page.SetFocus(Request("__LASTFOCUS"))
            End If
        End If

    End Sub

    ''' <summary>
    '''  Carrega os dados quando a página é carregada pela primeira vez
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/02/2009 Criado
    ''' </history> 
    Public Sub CarregaDados(codigo As String)

        Dim objColIac As IAC.ContractoServicio.Iac.GetIacDetail.IacColeccion
        If codigo <> String.Empty AndAlso codigo <> Nothing Then

            objColIac = getIacDetail(codigo)
            If objColIac IsNot Nothing AndAlso objColIac.Count > 0 Then

                'Preenche os controles do formulario
                txtCodigoIac.Text = objColIac(0).CodidoIac
                txtCodigoIac.ToolTip = objColIac(0).CodidoIac

                txtDescricaoIac.Text = objColIac(0).DescripcionIac
                txtDescricaoIac.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objColIac(0).DescripcionIac, String.Empty)

                txtObservacionesIac.Text = objColIac(0).ObservacionesIac
                chkVigente.Checked = objColIac(0).vigente
                chkInvisible.Checked = objColIac(0).EsInvisible
                chkCopiarDeclarados.Checked = objColIac(0).EsDeclaradoCopia
                chkDisponivelNuevoSaldos.Checked = objColIac(0).EspecificoSaldos

                ValorCodigo = objColIac(0).CodidoIac
                ValorDescricao = objColIac(0).DescripcionIac
                ValorObservaciones = objColIac(0).ObservacionesIac
                ValorVigente = objColIac(0).vigente
                ValorInvisible = objColIac(0).EsInvisible

                ' preenche a propriedade da tela
                EsVigente = objColIac(0).vigente

                'Se for modificação então guarda a descriçaõ atual para validação
                If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                    DescricaoAtual = txtDescricaoIac.Text
                End If
            End If
        End If

    End Sub

    ''' <summary>
    '''  Carrega os dados do grid de terminos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/02/2009 Criado
    ''' </history> 
    Public Sub CarregaGrid()

        Dim objColTerminos As IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion
        objColTerminos = TerminosTemPorario

        If objColTerminos.Count > 0 Then

            'Preenche os controles do formulario

            'Se for modificação então guarda a descriçaõ atual para validação
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                DescricaoAtual = txtDescricaoIac.Text
            End If


            ProsegurGridView1.PageSize = Aplicacao.Util.Utilidad.getMaximoRegistroGrid
            'Verifica se a consulta não retornou mais registros que o permitido
            If objColTerminos.Count > Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then
                ProsegurGridView1.AllowPaging = True
                ProsegurGridView1.PaginacaoAutomatica = True
            End If

            Dim objDt As DataTable
            objDt = ProsegurGridView1.ConvertListToDataTable(objColTerminos)
            objDt.DefaultView.Sort = " Codigo ASC"

            ProsegurGridView1.CarregaControle(objDt)

        Else


            'Limpa a consulta

            ProsegurGridView1.ExibirCabecalhoQuandoVazio = True
            ProsegurGridView1.EmptyDataText = Traduzir("info_msg_grd_vazio")
            ProsegurGridView1.CarregaControle(Nothing)

            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
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
                If txtCodigoIac.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoIac.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If txtDescricaoIac.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoIac.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

                'Verifica se existem términos selecionados.
                If ProsegurGridView2.Rows.Count <= 0 Then
                    strErro.Append(csvTerminoObligatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTerminoObligatorio.IsValid = False
                Else
                    csvTerminoObligatorio.IsValid = True
                End If

            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If CodigoExistente Then

                strErro.Append(csvCodigoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoIac.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoExistente.IsValid = True
            End If

            'Verifica se a descrição existe
            If DescricaoExistente Then

                strErro.Append(csvDescricaoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescricaoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescricaoIac.Focus()
                    focoSetado = True
                End If

            Else
                csvDescricaoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' Informa se o código do iac já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/07/2009 - Criado
    ''' </history>
    Private Function ExisteCodigoIac(codigoIac As String) As Boolean
        Dim objRespostaVerificarCodigoIac As IAC.ContractoServicio.Iac.VerificarCodigoIac.Respuesta
        Try

            Dim objProxyIac As New Comunicacion.ProxyIac
            Dim objPeticionVerificarCodigoIac As New IAC.ContractoServicio.Iac.VerificarCodigoIac.Peticion

            'Verifica se o código do canal existe no BD
            objPeticionVerificarCodigoIac.CodigoTerminoIac = codigoIac
            objRespostaVerificarCodigoIac = objProxyIac.VerificarCodigoIac(objPeticionVerificarCodigoIac)

            If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoIac.CodigoError, objRespostaVerificarCodigoIac.NombreServidorBD, objRespostaVerificarCodigoIac.MensajeError) Then
                Return objRespostaVerificarCodigoIac.Existe
            Else
                Master.ControleErro.ShowError(objRespostaVerificarCodigoIac.MensajeError)
                Return False
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Function

    ''' <summary>
    ''' Informa se a descrição do iac já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/07/2009 - Criado
    ''' </history>
    Private Function ExisteDescricaoIac(descricao As String) As Boolean
        Dim objRespostaVerificarDescricaoIac As IAC.ContractoServicio.Iac.VerificarDescripcionIac.Respuesta
        Try

            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                If descricao.Trim.Equals(DescricaoAtual) Then
                    DescricaoExistente = False
                    Return False
                End If
            End If

            Dim objProxyIac As New Comunicacion.ProxyIac
            Dim objPeticionVerificarDescricaoIac As New IAC.ContractoServicio.Iac.VerificarDescripcionIac.Peticion

            'Verifica se o código do canal existe no BD
            objPeticionVerificarDescricaoIac.DescripcionTerminoIac = txtDescricaoIac.Text
            objRespostaVerificarDescricaoIac = objProxyIac.VerificarDescripcionIac(objPeticionVerificarDescricaoIac)

            If Master.ControleErro.VerificaErro(objRespostaVerificarDescricaoIac.CodigoError, objRespostaVerificarDescricaoIac.NombreServidorBD, objRespostaVerificarDescricaoIac.MensajeError) Then
                Return objRespostaVerificarDescricaoIac.Existe
            Else
                Master.ControleErro.ShowError(objRespostaVerificarDescricaoIac.MensajeError) 'TODO: Exibe a mensagem de erro. Apenas para Debug. Retirar para Release.
                Return False
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Function


    ''' <summary>
    '''  Carrega os dados do grid de terminos iac.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/02/2009 Criado
    ''' </history> 
    Public Sub CarregaGridTerminosIac()

        Dim objColTerminosIacs As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion
        objColTerminosIacs = TerminosIacCompletos

        If objColTerminosIacs.Count > 0 Then

            'Preenche os controles do formulario

            'Se for modificação então guarda a descriçaõ atual para validação
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                DescricaoAtual = txtDescricaoIac.Text
            End If

            If objColTerminosIacs IsNot Nothing AndAlso objColTerminosIacs.Count > 0 Then

                'pnlSemRegistro.Visible = False
                Dim objDt As DataTable
                objDt = ProsegurGridView2.ConvertListToDataTable(objColTerminosIacs)

                'If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                objDt.DefaultView.Sort = " OrdenTermino ASC"
                'Else
                'objDt.DefaultView.Sort = ProsegurGridView1.SortCommand
                'End If

                ProsegurGridView2.CarregaControle(objDt)

                Dim esCampoClave As Boolean
                Dim esTerminoCopia As Boolean

                For Each row As GridViewRow In ProsegurGridView2.Rows
                    Dim rowLocal = row
                    esCampoClave = (From t In TerminosIacCompletos Where t.CodigoTermino = rowLocal.Cells(1).Text _
                                    AndAlso t.EsCampoClave = True).Count > 0

                    If esCampoClave Then
                        'Registro gravado com sucesso
                        DirectCast(row.FindControl("chkCopiaTermino"), CheckBox).InputAttributes.Add("disabled", "disabled")
                    Else
                        DirectCast(row.FindControl("chkIdMecanizado"), CheckBox).InputAttributes.Add("disabled", "disabled")
                    End If

                    esTerminoCopia = (From t In TerminosIacCompletos Where t.CodigoTermino = rowLocal.Cells(1).Text _
                                      AndAlso t.EsTerminoCopia = True).Count > 0

                    If esTerminoCopia Then
                        DirectCast(row.FindControl("chkAdicionaGridTerminosIac"), CheckBox).InputAttributes.Add("disabled", "disabled")
                    End If

                    If hidTerminoIacSelecionado.Value <> String.Empty Then
                        If row.Cells(1).Text = hidTerminoIacSelecionado.Value Then
                            Dim objCheck As CheckBox = DirectCast(row.FindControl("chkBusqueda"), CheckBox)
                            'objCheck.Checked = True
                            objCheck.Attributes.Add("onchange", "ResultadosTrueOrFalse(this,'" & row.Cells(1).Text & "','" & hidBusqueda.ClientID & "','" & hidBusquedaFalse.ClientID & "','" & hidBusquedaValorTrue.ClientID & "','" & hidBusquedaValorFalse.ClientID & "')")
                            If Acao <> Aplicacao.Util.Utilidad.eAcao.Consulta Then
                                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Click", "SelecionaCheckBoxTerminosIac('" & objCheck.ClientID & "','" & row.Cells(1).Text & "','" & hidTerminoIacSelecionado.ClientID & "','" & row.RowIndex + 1 & "','" & hidOrden.ClientID & "')", True)
                                'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Click", "SelecionaCheckBoxTerminosIac('" & objCheck.ClientID & "','" & row.Cells(1).Text & "','" & hidTerminoIacSelecionado.ClientID & "','" & row.Cells(8).Text & "','" & hidOrden.ClientID & "')", True)
                            End If
                        End If
                    End If

                Next

            Else

                'Limpa a consulta
                'ProsegurGridView2.DataSource = Nothing
                'ProsegurGridView2.DataBind()
                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If
        Else

            ProsegurGridView2.ExibirCabecalhoQuandoVazio = True
            ProsegurGridView2.EmptyDataText = Traduzir("info_msg_grd_vazio")

            ProsegurGridView2.CarregaControle(Nothing)
        End If

    End Sub

    ''' <summary>
    ''' Função responsável por fazer um select e verificar se o codigo informado existe na coleção retornando true or false.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2008 Criado
    ''' </history>
    Private Function SelectTerminosIac(TerminosIac As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion, codigo As String) As Boolean

        Dim retorno = From c In TerminosIac Where c.CodigoTermino = codigo

        If retorno.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Função responsável por fazer um select verificando se o codigo informado existe na coleção desejada, se existir
    ''' retorna o codigo, descrição e observação.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Function SelectInformacoesTerminosIac(TerminosIac As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion, codigo As String) As IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIac

        Dim retorno = From c In TerminosIac Where c.CodigoTermino = codigo Select c.CodigoTermino, c.DescripcionTermino, c.ObservacionesTermino

        Dim objTerminos As New IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIac

        Dim en As IEnumerator = retorno.GetEnumerator()

        Dim objRetorno As New Object

        If en.MoveNext Then
            objRetorno = en.Current

            objTerminos.Codigo = objRetorno.CodigoTermino
            objTerminos.Descripcion = objRetorno.DescripcionTermino
            objTerminos.Observacion = objRetorno.ObservacionesTermino

        End If

        Return objTerminos
    End Function

    ''' <summary>
    ''' Função responsável por fazer um select verificando se o codigo informado existe na coleção desejada, se existir
    ''' retorna os dados expecificados nos select da função.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Function SelectInformacoesBusqueda(TerminosIac As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion, codigo As String) As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac

        Dim retorno = From c In TerminosIac Where c.CodigoTermino = codigo Select c.CodigoTermino, c.DescripcionTermino, c.ObservacionesTermino, c.AdmiteValoresPosibles, c.CodigoAlgoritmoTermino, _
                      c.CodigoFormatoTermino, c.CodigoMascaraTermino, c.DescripcionAlgoritmoTermino, c.DescripcionFormatoTermino, c.DescripcionMascaraTermino, c.EsObligatorio, c.EsBusquedaParcial, _
                      c.EsCampoClave, c.LongitudTermino, c.MostarCodigo, c.OrdenTermino, c.VigenteTermino, c.EsTerminoCopia, c.esProtegido, c.esInvisibleRpte, c.esIdMecanizado

        Dim objTerminos As New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac

        Dim en As IEnumerator = retorno.GetEnumerator()

        Dim objRetorno As New Object

        If en.MoveNext Then
            objRetorno = en.Current

            objTerminos.CodigoTermino = objRetorno.CodigoTermino
            objTerminos.DescripcionTermino = objRetorno.DescripcionTermino
            objTerminos.ObservacionesTermino = objRetorno.ObservacionesTermino
            objTerminos.AdmiteValoresPosibles = objRetorno.AdmiteValoresPosibles
            objTerminos.CodigoAlgoritmoTermino = objRetorno.CodigoAlgoritmoTermino
            objTerminos.CodigoFormatoTermino = objRetorno.CodigoFormatoTermino
            objTerminos.CodigoMascaraTermino = objRetorno.CodigoMascaraTermino
            objTerminos.DescripcionAlgoritmoTermino = objRetorno.DescripcionAlgoritmoTermino
            objTerminos.DescripcionFormatoTermino = objRetorno.DescripcionFormatoTermino
            objTerminos.DescripcionMascaraTermino = objRetorno.DescripcionMascaraTermino
            objTerminos.EsObligatorio = objRetorno.EsObligatorio
            objTerminos.EsBusquedaParcial = objRetorno.EsBusquedaParcial
            objTerminos.EsCampoClave = objRetorno.EsCampoClave
            objTerminos.LongitudTermino = objRetorno.LongitudTermino
            objTerminos.MostarCodigo = objRetorno.MostarCodigo
            objTerminos.OrdenTermino = objRetorno.OrdenTermino
            objTerminos.VigenteTermino = objRetorno.VigenteTermino
            objTerminos.EsTerminoCopia = objRetorno.EsTerminoCopia
            objTerminos.esProtegido = objRetorno.esProtegido
            objTerminos.esInvisibleRpte = objRetorno.esInvisibleRpte
            objTerminos.esIdMecanizado = objRetorno.esIdMecanizado

        End If

        Return objTerminos
    End Function

    ''' <summary>
    ''' Função responsável por fazer um select verificando se o codigo da ordem informado existe na coleção desejada, se existir
    ''' retorna os dados expecificados nos select da função.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Criado
    ''' </history>
    Private Function SelectInformacoesOrdenTermino(TerminosIac As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion, codigo As Integer) As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac

        Dim retorno = From c In TerminosIac Where c.OrdenTermino = codigo Select c.CodigoTermino, c.DescripcionTermino, c.ObservacionesTermino, c.AdmiteValoresPosibles, c.CodigoAlgoritmoTermino, _
                      c.CodigoFormatoTermino, c.CodigoMascaraTermino, c.DescripcionAlgoritmoTermino, c.DescripcionFormatoTermino, c.DescripcionMascaraTermino, c.EsObligatorio, c.EsBusquedaParcial, _
                      c.EsCampoClave, c.LongitudTermino, c.MostarCodigo, c.OrdenTermino, c.VigenteTermino, c.EsTerminoCopia, c.esProtegido, c.esInvisibleRpte, c.esIdMecanizado

        Dim objTerminos As New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac

        Dim en As IEnumerator = retorno.GetEnumerator()

        Dim objRetorno As New Object

        If en.MoveNext Then
            objRetorno = en.Current

            objTerminos.CodigoTermino = objRetorno.CodigoTermino
            objTerminos.DescripcionTermino = objRetorno.DescripcionTermino
            objTerminos.ObservacionesTermino = objRetorno.ObservacionesTermino
            objTerminos.AdmiteValoresPosibles = objRetorno.AdmiteValoresPosibles
            objTerminos.CodigoAlgoritmoTermino = objRetorno.CodigoAlgoritmoTermino
            objTerminos.CodigoFormatoTermino = objRetorno.CodigoFormatoTermino
            objTerminos.CodigoMascaraTermino = objRetorno.CodigoMascaraTermino
            objTerminos.DescripcionAlgoritmoTermino = objRetorno.DescripcionAlgoritmoTermino
            objTerminos.DescripcionFormatoTermino = objRetorno.DescripcionFormatoTermino
            objTerminos.DescripcionMascaraTermino = objRetorno.DescripcionMascaraTermino
            objTerminos.EsObligatorio = objRetorno.EsObligatorio
            objTerminos.EsBusquedaParcial = objRetorno.EsBusquedaParcial
            objTerminos.EsCampoClave = objRetorno.EsCampoClave
            objTerminos.LongitudTermino = objRetorno.LongitudTermino
            objTerminos.MostarCodigo = objRetorno.MostarCodigo
            objTerminos.OrdenTermino = objRetorno.OrdenTermino
            objTerminos.VigenteTermino = objRetorno.VigenteTermino
            objTerminos.EsTerminoCopia = objRetorno.EsTerminoCopia
            objTerminos.esProtegido = objRetorno.esProtegido
            objTerminos.esInvisibleRpte = objRetorno.esInvisibleRpte
            objTerminos.esIdMecanizado = objRetorno.esIdMecanizado
        End If

        Return objTerminos
    End Function

    ''' <summary>
    ''' Função responsável por fazer um select verificando se o codigo informado existe na coleção desejada, se existir
    ''' retorna o codigo, descrição e observação.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Function SelectInformacoesTerminos(Terminos As IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion, codigo As String) As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac

        Dim retorno = From c In Terminos Where c.Codigo = codigo Select c.Descripcion, c.Observacion

        Dim objTerminos As New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac

        Dim en As IEnumerator = retorno.GetEnumerator()

        Dim objRetorno As New Object

        If en.MoveNext Then
            objRetorno = en.Current

            objTerminos.CodigoTermino = codigo
            objTerminos.DescripcionTermino = objRetorno.Descripcion
            objTerminos.ObservacionesTermino = objRetorno.Observacion


        End If

        Return objTerminos
    End Function

    ''' <summary>
    ''' Ordena a coleção de string informada de forma crescente.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Function Orderacao(colecao As List(Of String)) As String()

        Dim colecaoConvertida As New List(Of Integer)

        For Each item In colecao
            colecaoConvertida.Add(CInt(item))
        Next

        Dim retorno = From c In colecaoConvertida Order By c

        Dim colecaoOrdenada() As String = Nothing

        Dim en As IEnumerator = retorno.GetEnumerator()

        Dim objRetorno As New Object

        While en.MoveNext

            objRetorno = en.Current

            If colecaoOrdenada Is Nothing Then
                ReDim colecaoOrdenada(0)
            Else
                ReDim Preserve colecaoOrdenada(colecaoOrdenada.Length)
            End If

            colecaoOrdenada(colecaoOrdenada.Length - 1) = objRetorno

        End While

        Return colecaoOrdenada
    End Function


    ''' <summary>
    ''' Ordena a coleção de string informada de forma decrescente.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Function OrderacaoDesc(colecao As List(Of String)) As String()

        Dim colecaoConvertida As New List(Of Integer)

        For Each item In colecao
            colecaoConvertida.Add(CInt(item))
        Next

        Dim retorno = From c In colecaoConvertida Order By c Descending

        Dim colecaoOrdenada() As String = Nothing

        Dim en As IEnumerator = retorno.GetEnumerator()

        Dim objRetorno As New Object

        While en.MoveNext

            objRetorno = en.Current

            If colecaoOrdenada Is Nothing Then
                ReDim colecaoOrdenada(0)
            Else
                ReDim Preserve colecaoOrdenada(colecaoOrdenada.Length)
            End If

            colecaoOrdenada(colecaoOrdenada.Length - 1) = objRetorno

        End While

        Return colecaoOrdenada
    End Function

    ''' <summary>
    ''' Deleta o item da coleção de terminos iac que contem o codigo informado.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub SelectDeleteTerminosIac(ByRef TerminosIac As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion, codigo As String)

        Dim objTermino As New ContractoServicio.Iac.GetIacDetail.TerminosIac

        For Each item As ContractoServicio.Iac.GetIacDetail.TerminosIac In TerminosIac

            'Item que sera removido
            If item.CodigoTermino.Equals(codigo) Then
                objTermino = item
                Exit For
            End If

        Next


        If objTermino IsNot Nothing AndAlso TerminosIac IsNot Nothing Then
            If TerminosIac.Count > 0 Then
                TerminosIac.Remove(objTermino)
            End If
        End If



    End Sub

    ''' <summary>
    ''' Deleta o item da coleção de terminos que contem o codigo informado.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub SelectDeleteTerminos(ByRef TerminosIac As IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion, codigo As String)

        Dim objTermino As New ContractoServicio.TerminoIac.GetTerminoIac.TerminoIac

        For Each item As ContractoServicio.TerminoIac.GetTerminoIac.TerminoIac In TerminosIac

            'Item que sera removido
            If item.Codigo.Equals(codigo) Then
                objTermino = item
                Exit For
            End If

        Next


        If objTermino IsNot Nothing AndAlso TerminosIac IsNot Nothing Then
            If TerminosIac.Count > 0 Then
                TerminosIac.Remove(objTermino)
            End If
        End If



    End Sub

    ''' <summary>
    ''' Faz a verificação do campo hiden trazendo dos os codigos existentes no campo, depois com os codigos existentes ele verifica
    ''' se o codigo esta no hiden de campos falsos ou verdadeiros e gravando na coleção se o campo e verdadeiro ou falso.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub VerificaCheckBoxBusqueda()
        Dim busquedaVerdadeira As String
        Dim busquedasTrue() As String

        Dim busquedaFalsa As String
        Dim busquedasFalse() As String

        Dim objTerminosIac As ContractoServicio.Iac.GetIacDetail.TerminosIac

        busquedaVerdadeira = hidBusqueda.Value
        busquedasTrue = busquedaVerdadeira.Split("|")

        busquedaFalsa = hidBusquedaFalse.Value
        busquedasFalse = busquedaFalsa.Split("|")
        If busquedasFalse IsNot Nothing Then

            For Each itemFalso As String In busquedasFalse
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then
                    If itemFalso <> String.Empty AndAlso itemFalso <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, itemFalso)

                        If objTerminosIac.CodigoTermino.Equals(itemFalso) Then
                            objTerminosIac.EsBusquedaParcial = False
                        End If

                        SelectDeleteTerminosIac(TerminosIacCompletos, itemFalso)
                        TerminosIacCompletos.Add(objTerminosIac)
                    End If
                End If
            Next
            Array.Clear(busquedasFalse, 0, busquedasFalse.Length - 1)
        End If

        If busquedasTrue IsNot Nothing Then

            For Each item As String In busquedasTrue
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then

                    If item <> String.Empty AndAlso item <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, item)

                        If objTerminosIac.CodigoTermino.Equals(item) Then
                            objTerminosIac.EsBusquedaParcial = True
                        End If


                        SelectDeleteTerminosIac(TerminosIacCompletos, item)
                        TerminosIacCompletos.Add(objTerminosIac)

                    End If

                End If

            Next

            Array.Clear(busquedasTrue, 0, busquedasTrue.Length - 1)
        End If

        hidBusqueda.Value = Nothing
        hidBusquedaFalse.Value = Nothing
        busquedaFalsa = String.Empty
        busquedaVerdadeira = String.Empty

    End Sub

    ''' <summary>
    ''' Faz a verificação do campo hiden trazendo dos os codigos existentes no campo, depois com os codigos existentes ele verifica
    ''' se o codigo esta no hiden de campos falsos ou verdadeiros e gravando na coleção se o campo e verdadeiro ou falso.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 28/05/2009 Criado
    ''' </history>
    Private Sub VerificaCheckBoxObligatorio()
        Dim obligatorioVerdadeira As String
        Dim obligatoriosTrue() As String

        Dim obligatorioFalso As String
        Dim obligatoriosFalse() As String

        Dim objTerminosIac As ContractoServicio.Iac.GetIacDetail.TerminosIac

        obligatorioVerdadeira = hidObligatorio.Value
        obligatoriosTrue = obligatorioVerdadeira.Split("|")

        obligatorioFalso = hidObligatorioFalse.Value
        obligatoriosFalse = obligatorioFalso.Split("|")
        If obligatoriosFalse IsNot Nothing Then

            For Each itemFalso As String In obligatoriosFalse
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then
                    If itemFalso <> String.Empty AndAlso itemFalso <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, itemFalso)

                        If objTerminosIac.CodigoTermino.Equals(itemFalso) Then
                            objTerminosIac.EsObligatorio = False
                        End If

                        SelectDeleteTerminosIac(TerminosIacCompletos, itemFalso)
                        TerminosIacCompletos.Add(objTerminosIac)
                    End If
                End If
            Next
            Array.Clear(obligatoriosFalse, 0, obligatoriosFalse.Length - 1)
        End If

        If obligatoriosTrue IsNot Nothing Then

            For Each item As String In obligatoriosTrue
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then

                    If item <> String.Empty AndAlso item <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, item)

                        If objTerminosIac.CodigoTermino.Equals(item) Then
                            objTerminosIac.EsObligatorio = True
                        End If


                        SelectDeleteTerminosIac(TerminosIacCompletos, item)
                        TerminosIacCompletos.Add(objTerminosIac)

                    End If

                End If

            Next

            Array.Clear(obligatoriosTrue, 0, obligatoriosTrue.Length - 1)
        End If

        hidObligatorio.Value = Nothing
        hidObligatorioFalse.Value = Nothing
        obligatorioFalso = String.Empty
        obligatorioVerdadeira = String.Empty

    End Sub

    ''' <summary>
    ''' Faz a verificação do campo hiden trazendo dos os codigos existentes no campo, depois com os codigos existentes ele verifica
    ''' se o codigo esta no hiden de campos falsos ou verdadeiros e gravando na coleção se o campo e verdadeiro ou falso.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub VerificaCheckBoxCampoClave()
        Dim claveVerdadeira As String
        Dim clavesTrue() As String

        Dim claveFalsa As String
        Dim clavesFalse() As String

        Dim objTerminosIac As ContractoServicio.Iac.GetIacDetail.TerminosIac

        claveVerdadeira = hidCampoClave.Value
        clavesTrue = claveVerdadeira.Split("|")

        claveFalsa = hidCampoClaveFalse.Value
        clavesFalse = claveFalsa.Split("|")

        If clavesFalse IsNot Nothing AndAlso clavesFalse.Length > 0 Then

            For Each itemFalso As String In clavesFalse
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then
                    If itemFalso <> String.Empty AndAlso itemFalso <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, itemFalso)

                        If objTerminosIac.CodigoTermino.Equals(itemFalso) Then
                            objTerminosIac.EsCampoClave = False
                        End If

                        SelectDeleteTerminosIac(TerminosIacCompletos, itemFalso)
                        TerminosIacCompletos.Add(objTerminosIac)


                    End If
                End If
            Next
            Array.Clear(clavesFalse, 0, clavesFalse.Length - 1)
        End If

        If clavesTrue IsNot Nothing AndAlso clavesTrue.Length > 0 Then

            For Each item As String In clavesTrue
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then

                    If item <> String.Empty AndAlso item <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, item)

                        If objTerminosIac.CodigoTermino.Equals(item) Then
                            objTerminosIac.EsCampoClave = True
                        End If


                        SelectDeleteTerminosIac(TerminosIacCompletos, item)
                        TerminosIacCompletos.Add(objTerminosIac)
                    End If

                End If

            Next

            Array.Clear(clavesTrue, 0, clavesTrue.Length - 1)
        End If

        hidCampoClave.Value = Nothing
        hidCampoClaveFalse.Value = Nothing
        claveFalsa = String.Empty
        claveVerdadeira = String.Empty
    End Sub

    ''' <summary>
    ''' Faz a verificação do campo hiden trazendo dos os codigos existentes no campo, depois com os codigos existentes ele verifica
    ''' se o codigo esta no hiden de campos falsos ou verdadeiros e gravando na coleção se o campo e verdadeiro ou falso.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub VerificaCheckBoxCopiarTermino()
        Dim CopiarTerminoVerdadeiro As String
        Dim CopiarTerminoTrue() As String

        Dim copiarTerminoFalso As String
        Dim CopiarTerminoFalse() As String

        Dim objTerminosIac As ContractoServicio.Iac.GetIacDetail.TerminosIac

        CopiarTerminoVerdadeiro = hidCopiarTermino.Value
        CopiarTerminoTrue = CopiarTerminoVerdadeiro.Split("|")

        copiarTerminoFalso = hidCopiarTerminoFalse.Value
        CopiarTerminoFalse = copiarTerminoFalso.Split("|")

        If CopiarTerminoFalse IsNot Nothing AndAlso CopiarTerminoFalse.Length > 0 Then

            For Each itemFalso As String In CopiarTerminoFalse
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then
                    If itemFalso <> String.Empty AndAlso itemFalso <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, itemFalso)

                        If objTerminosIac.CodigoTermino.Equals(itemFalso) Then
                            objTerminosIac.EsTerminoCopia = False
                        End If

                        SelectDeleteTerminosIac(TerminosIacCompletos, itemFalso)
                        TerminosIacCompletos.Add(objTerminosIac)


                    End If
                End If
            Next
            Array.Clear(CopiarTerminoFalse, 0, CopiarTerminoFalse.Length - 1)
        End If

        If CopiarTerminoTrue IsNot Nothing AndAlso CopiarTerminoTrue.Length > 0 Then

            For Each item As String In CopiarTerminoTrue
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then

                    If item <> String.Empty AndAlso item <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, item)

                        If objTerminosIac.CodigoTermino.Equals(item) Then
                            objTerminosIac.EsTerminoCopia = True
                        End If


                        SelectDeleteTerminosIac(TerminosIacCompletos, item)
                        TerminosIacCompletos.Add(objTerminosIac)
                    End If

                End If

            Next

            Array.Clear(CopiarTerminoTrue, 0, CopiarTerminoTrue.Length - 1)
        End If

        hidCopiarTermino.Value = Nothing
        hidCopiarTerminoFalse.Value = Nothing
        copiarTerminoFalso = String.Empty
        CopiarTerminoVerdadeiro = String.Empty
    End Sub

    ''' <summary>
    ''' Faz a verificação do campo hiden trazendo dos os codigos existentes no campo, depois com os codigos existentes ele verifica
    ''' se o codigo esta no hiden de campos falsos ou verdadeiros e gravando na coleção se o campo e verdadeiro ou falso.
    ''' </summary>
    Private Sub VerificaCheckBoxProtegido()
        Dim ProtegidoVerdadeira As String
        Dim ProtegidoTrue() As String

        Dim ProtegidoFalsa As String
        Dim ProtegidoFalse() As String

        Dim objTerminosIac As ContractoServicio.Iac.GetIacDetail.TerminosIac

        ProtegidoVerdadeira = hidProtegido.Value
        ProtegidoTrue = ProtegidoVerdadeira.Split("|")

        ProtegidoFalsa = hidProtegidoFalse.Value
        ProtegidoFalse = ProtegidoFalsa.Split("|")
        If ProtegidoFalse IsNot Nothing Then

            For Each itemFalso As String In ProtegidoFalse
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then
                    If itemFalso <> String.Empty AndAlso itemFalso <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, itemFalso)

                        If objTerminosIac.CodigoTermino.Equals(itemFalso) Then
                            objTerminosIac.esProtegido = False
                        End If

                        SelectDeleteTerminosIac(TerminosIacCompletos, itemFalso)
                        TerminosIacCompletos.Add(objTerminosIac)
                    End If
                End If
            Next
            Array.Clear(ProtegidoFalse, 0, ProtegidoFalse.Length - 1)
        End If

        If ProtegidoTrue IsNot Nothing Then

            For Each item As String In ProtegidoTrue
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then

                    If item <> String.Empty AndAlso item <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, item)

                        If objTerminosIac.CodigoTermino.Equals(item) Then
                            objTerminosIac.esProtegido = True
                        End If


                        SelectDeleteTerminosIac(TerminosIacCompletos, item)
                        TerminosIacCompletos.Add(objTerminosIac)

                    End If

                End If

            Next

            Array.Clear(ProtegidoTrue, 0, ProtegidoTrue.Length - 1)
        End If

        hidProtegido.Value = Nothing
        hidProtegidoFalse.Value = Nothing
        ProtegidoFalsa = String.Empty
        ProtegidoVerdadeira = String.Empty
    End Sub

    Private Sub VerificaCheckBoxInvisibleReporte()
        Dim InvisibleReporteVerdadeira As String
        Dim InvisibleReporteTrue() As String

        Dim InvisibleReporteFalsa As String
        Dim InvisibleReporteFalse() As String

        Dim objTerminosIac As ContractoServicio.Iac.GetIacDetail.TerminosIac

        InvisibleReporteVerdadeira = hidInvisibleReporte.Value()
        InvisibleReporteTrue = InvisibleReporteVerdadeira.Split("|")

        InvisibleReporteFalsa = hidInvisibleReporteFalse.Value
        InvisibleReporteFalse = InvisibleReporteFalsa.Split("|")
        If InvisibleReporteFalse IsNot Nothing Then

            For Each itemFalso As String In InvisibleReporteFalse
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then
                    If itemFalso <> String.Empty AndAlso itemFalso <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, itemFalso)

                        If objTerminosIac.CodigoTermino.Equals(itemFalso) Then
                            objTerminosIac.esInvisibleRpte = False
                        End If

                        SelectDeleteTerminosIac(TerminosIacCompletos, itemFalso)
                        TerminosIacCompletos.Add(objTerminosIac)
                    End If
                End If
            Next
            Array.Clear(InvisibleReporteFalse, 0, InvisibleReporteFalse.Length - 1)
        End If

        If InvisibleReporteTrue IsNot Nothing Then

            For Each item As String In InvisibleReporteTrue
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then

                    If item <> String.Empty AndAlso item <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, item)

                        If objTerminosIac.CodigoTermino.Equals(item) Then
                            objTerminosIac.esInvisibleRpte = True
                        End If


                        SelectDeleteTerminosIac(TerminosIacCompletos, item)
                        TerminosIacCompletos.Add(objTerminosIac)

                    End If

                End If

            Next

            Array.Clear(InvisibleReporteTrue, 0, InvisibleReporteTrue.Length - 1)
        End If

        hidInvisibleReporte.Value = Nothing
        hidInvisibleReporteFalse.Value = Nothing
        InvisibleReporteFalsa = String.Empty
        InvisibleReporteVerdadeira = String.Empty
    End Sub

    Private Sub VerificaCheckBoxIdMecanizado()
        Dim IdMecanizadoVerdadeira As String
        Dim IdMecanizadoTrue() As String

        Dim IdMecanizadoFalsa As String
        Dim IdMecanizadoFalse() As String

        Dim objTerminosIac As ContractoServicio.Iac.GetIacDetail.TerminosIac

        IdMecanizadoVerdadeira = hidIdMecanizado.Value()
        IdMecanizadoTrue = IdMecanizadoVerdadeira.Split("|")

        IdMecanizadoFalsa = hidIdMecanizadoFalse.Value
        IdMecanizadoFalse = IdMecanizadoFalsa.Split("|")
        If IdMecanizadoFalse IsNot Nothing Then

            For Each itemFalso As String In IdMecanizadoFalse
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then
                    If itemFalso <> String.Empty AndAlso itemFalso <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, itemFalso)

                        If objTerminosIac.CodigoTermino.Equals(itemFalso) Then
                            objTerminosIac.esIdMecanizado = False
                        End If

                        SelectDeleteTerminosIac(TerminosIacCompletos, itemFalso)
                        TerminosIacCompletos.Add(objTerminosIac)
                    End If
                End If
            Next
            Array.Clear(IdMecanizadoFalse, 0, IdMecanizadoFalse.Length - 1)
        End If

        If IdMecanizadoTrue IsNot Nothing Then

            For Each item As String In IdMecanizadoTrue
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then

                    If item <> String.Empty AndAlso item <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, item)

                        If objTerminosIac.CodigoTermino.Equals(item) Then
                            objTerminosIac.esIdMecanizado = True
                        End If


                        SelectDeleteTerminosIac(TerminosIacCompletos, item)
                        TerminosIacCompletos.Add(objTerminosIac)

                    End If

                End If

            Next

            Array.Clear(IdMecanizadoTrue, 0, IdMecanizadoTrue.Length - 1)
        End If

        hidIdMecanizado.Value = Nothing
        hidIdMecanizadoFalse.Value = Nothing
        IdMecanizadoFalsa = String.Empty
        IdMecanizadoVerdadeira = String.Empty
    End Sub

    ''' <summary>
    ''' Chama o java script para desabilitar os controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub AdicionaJavaScriptDesabilitaControle()


        Dim pbo As PostBackOptions
        Dim s As String = String.Empty

        pbo = New PostBackOptions(btnGrabar)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnGrabar.FuncaoJavascript = "if (!this.disabled){Desabilitar();" & s & "};"


        pbo = New PostBackOptions(btnAbaixo)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnAbaixo.Attributes.Add("onclick", "Desabilitar();" & s & ";")


        pbo = New PostBackOptions(btnAcima)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnAcima.Attributes.Add("onclick", "Desabilitar();" & s & ";")

        pbo = New PostBackOptions(btnAdiciona)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnAdiciona.Attributes.Add("onclick", "Desabilitar();" & s & ";")

        pbo = New PostBackOptions(btnremove)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnremove.Attributes.Add("onclick", "Desabilitar();" & s & ";")


        ClientScript.RegisterStartupScript(Me.Page.GetType, "Script", "DesabilitaControles(document.getElementById('" & btnAcima.ClientID & "'));" & _
                                                "DesabilitaControles(document.getElementById('" & btnAbaixo.ClientID & "'));" & _
                                                "DesabilitaControles(document.getElementById('" & btnAdiciona.ClientID & "'));" & _
                                                "DesabilitaControles(document.getElementById('" & btnremove.ClientID & "'));" & _
                                                "DesabilitaControles(document.getElementById('btnGrabar_tabela'));", True)

    End Sub

    ''' <summary>
    ''' Faz um distinct para não deixar adicionar na coleção valores repetidos
    ''' </summary>
    ''' <param name="valores"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 29/09/2009 - Criado
    ''' </history>
    Private Function VerificaValorRepetido(valores() As String) As List(Of String)

        Dim retorno = From c In valores Distinct

        Dim valoresRetorno As New List(Of String)

        If retorno.Count > 0 Then

            For Each item In retorno
                valoresRetorno.Add(item)
            Next
        End If

        Return valoresRetorno
    End Function

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS GRIDVIEW]"

    ''' <summary>
    ''' Seta o tamanho das linhas do grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Criado
    ''' </history>
    Private Sub TamanhoLinhas()

        ProsegurGridView1.RowStyle.Height = 20

        ProsegurGridView2.RowStyle.Height = 20
    End Sub

    ''' <summary>
    '''Adiciona um evento java script na linha do grid, quando clicar na linha o checkbox sera checado.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView1_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.EOnClickRowClientScript

        Dim chkAdiciona As System.Web.UI.WebControls.CheckBox
        chkAdiciona = e.Row.Cells(0).Controls(1)
        'Se for consulta desabilita o checkbox
        If Acao <> Aplicacao.Util.Utilidad.eAcao.Consulta Then
            ProsegurGridView1.OnClickRowClientScript = "SelecionaCheckBox('" & chkAdiciona.ClientID & "','" & e.Row.DataItem("Codigo") & "','" & hidTerminoSelecionado.ClientID & "')"
        End If

    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPager_SetCss

        Try

            'Configura o estilo dos controles presentes no pager
            CType(CType(sender, ArrayList)(0), Label).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(0), Label).Font.Bold = False
            CType(CType(sender, ArrayList)(0), Label).Font.Size = 9
            CType(CType(sender, ArrayList)(0), Label).Font.Name = "Verdana"

            CType(CType(sender, ArrayList)(1), TextBox).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(1), TextBox).Font.Bold = False
            CType(CType(sender, ArrayList)(1), TextBox).Font.Size = 9
            CType(CType(sender, ArrayList)(1), TextBox).Font.Name = "Verdana"
            CType(CType(sender, ArrayList)(1), TextBox).Style.Add("text-align", "center")
            CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 9

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    '''Cabelho do Grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub CabecalhoVazioTerminos()

        ProsegurGridView1.Columns(0).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView1.Columns(1).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView1.Columns(2).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView1.Columns(3).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

    End Sub

    ''' <summary>
    '''Cabelho do Grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub CabecalhoVazioTerminosIac()

        ProsegurGridView2.Columns(0).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView2.Columns(1).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView2.Columns(2).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView2.Columns(3).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView2.Columns(4).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView2.Columns(5).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView2.Columns(6).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView2.Columns(7).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView2.Columns(8).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView2.Columns(9).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

    End Sub

    ''' <summary>
    ''' Verifica se existe itens checados
    ''' </summary>
    ''' <param name="item"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirpatrick.santos] 25/07/2011 Criado
    ''' </history>
    Private Function esItemChecado(item As String) As Boolean
        'Return hidTerminosSelecionados.Value.IndexOf("|" & item & "|") >= 0
        Dim valor = hidTerminoSelecionado.Value
        Dim valores() As String = valor.Split("|")
        Return valores.FirstOrDefault(Function(f) f.Equals(item)) IsNot Nothing

    End Function

    ''' <summary>
    '''Adiciona um evento java script na linha do grid, quando clicar na linha o checkbox sera checado.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView2_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView2.EOnClickRowClientScript

        Dim chkAdicionaTerminos As System.Web.UI.WebControls.CheckBox
        chkAdicionaTerminos = e.Row.Cells(0).Controls(1)
        If Acao <> Aplicacao.Util.Utilidad.eAcao.Consulta Then
            ProsegurGridView2.OnClickRowClientScript = "SelecionaCheckBoxTerminosIac('" & chkAdicionaTerminos.ClientID & "','" & e.Row.DataItem("CodigoTermino") & "','" & hidTerminoIacSelecionado.ClientID & "','" & e.Row.DataItem("OrdenTermino") & "','" & hidOrden.ClientID & "')"
        End If

    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Criado
    ''' </history>
    Protected Sub ProsegurGridView2_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridView2.EPager_SetCss

        Try

            'Configura o estilo dos controles presentes no pager
            CType(CType(sender, ArrayList)(0), Label).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(0), Label).Font.Bold = False
            CType(CType(sender, ArrayList)(0), Label).Font.Size = 9
            CType(CType(sender, ArrayList)(0), Label).Font.Name = "Verdana"
            ProsegurGridView2.Columns(0).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

            CType(CType(sender, ArrayList)(1), TextBox).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(1), TextBox).Font.Bold = False
            CType(CType(sender, ArrayList)(1), TextBox).Font.Size = 9
            CType(CType(sender, ArrayList)(1), TextBox).Font.Name = "Verdana"
            CType(CType(sender, ArrayList)(1), TextBox).Style.Add("text-align", "center")
            ProsegurGridView2.Columns(1).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"
            ProsegurGridView2.Columns(2).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

            CType(CType(sender, ArrayList)(3), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(3), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(3), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(3), Object).Font.Name = "Verdana"
            ProsegurGridView2.Columns(3).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

            CType(CType(sender, ArrayList)(4), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(4), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(4), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(4), Object).Font.Name = "Verdana"
            ProsegurGridView2.Columns(4).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

            CType(CType(sender, ArrayList)(5), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(5), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(5), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(5), Object).Font.Name = "Verdana"
            ProsegurGridView2.Columns(5).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

            CType(CType(sender, ArrayList)(6), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(6), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(6), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(6), Object).Font.Name = "Verdana"
            ProsegurGridView2.Columns(6).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

            CType(CType(sender, ArrayList)(7), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(7), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(7), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(7), Object).Font.Name = "Verdana"
            ProsegurGridView2.Columns(7).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

            CType(CType(sender, ArrayList)(8), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(8), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(8), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(8), Object).Font.Name = "Verdana"
            ProsegurGridView2.Columns(8).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

            CType(CType(sender, ArrayList)(9), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(9), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(9), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(9), Object).Font.Name = "Verdana"
            ProsegurGridView2.Columns(9).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

            CType(CType(sender, ArrayList)(10), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(10), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(10), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(10), Object).Font.Name = "Verdana"
            ProsegurGridView2.Columns(10).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Configuração das colunas do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowDataBound

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then


                'Índice das celulas do GridView Configuradas
                '0 - Checkbox
                '1 - Codigo
                '2 - Descripcion
                '3 - Observaciones

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                Dim objChk As CheckBox = CType(e.Row.Cells(0).FindControl("chkAdicionaGridTerminos"), CheckBox)
                objChk.Attributes.Add("ValorChkSelecionado", e.Row.DataItem("Codigo"))

                If esItemChecado(e.Row.DataItem("Codigo").ToString) Then
                    objChk.Checked = True
                End If

                If Not e.Row.DataItem("Descripcion") Is DBNull.Value AndAlso e.Row.DataItem("Descripcion").Length > NumeroMaximoLinha Then
                    e.Row.Cells(2).Text = e.Row.DataItem("Descripcion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If


                If Not e.Row.DataItem("Observacion") Is DBNull.Value AndAlso e.Row.DataItem("Observacion").Length > NumeroMaximoLinha Then
                    e.Row.Cells(3).Text = e.Row.DataItem("Observacion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                Dim chk As System.Web.UI.WebControls.CheckBox
                chk = e.Row.Cells(0).Controls(1)
                chk.Attributes.Add("onclick", "executarlinha=false;")

                'Se for consulta desabilita o checkbox
                If Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    objChk.Enabled = False
                End If

            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Configuração das colunas do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Criado
    ''' </history>
    Private Sub ProsegurGridView2_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView2.RowDataBound

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                'Índice das celulas do GridView Configuradas
                '0 - checkbox
                '1 - Código
                '2 - Descripción
                '3 - Obligatorio
                '4 - Busqueda
                '5 - Campo Clave
                '6 - Protegido
                '7 - Orden Termino(coluna oculta)
                '8 - Invisible Report 
                '9 - Id Mecanizado

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha


                If Not e.Row.DataItem("DescripcionTermino") Is DBNull.Value AndAlso e.Row.DataItem("DescripcionTermino").Length > NumeroMaximoLinha Then
                    e.Row.Cells(2).Text = e.Row.DataItem("DescripcionTermino").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                If CBool(e.Row.DataItem("EsObligatorio")) Then
                    CType(e.Row.Cells(3).FindControl("chkObligatorio"), CheckBox).Checked = True
                Else
                    CType(e.Row.Cells(3).FindControl("chkObligatorio"), CheckBox).Checked = False
                End If

                If CBool(e.Row.DataItem("EsBusquedaParcial")) Then
                    CType(e.Row.Cells(4).FindControl("chkBusqueda1"), CheckBox).Checked = True
                Else
                    CType(e.Row.Cells(4).FindControl("chkBusqueda1"), CheckBox).Checked = False
                End If

                If CBool(e.Row.DataItem("EsCampoClave")) Then
                    CType(e.Row.Cells(5).FindControl("chkAdicionaGridTerminosIac"), CheckBox).Checked = True
                Else
                    CType(e.Row.Cells(5).FindControl("chkAdicionaGridTerminosIac"), CheckBox).Checked = False
                End If

                If CBool(e.Row.DataItem("EsTerminoCopia")) Then
                    CType(e.Row.Cells(6).FindControl("chkCopiaTermino"), CheckBox).Checked = True
                Else
                    CType(e.Row.Cells(6).FindControl("chkCopiaTermino"), CheckBox).Checked = False
                End If

                If CBool(e.Row.DataItem("EsProtegido")) Then
                    CType(e.Row.Cells(7).FindControl("chkProtegido"), CheckBox).Checked = True
                Else
                    CType(e.Row.Cells(7).FindControl("chkProtegido"), CheckBox).Checked = False
                End If

                'celula nove pois existe uma oculta antes (8)
                If CBool(e.Row.DataItem("esInvisibleRpte")) Then
                    CType(e.Row.Cells(9).FindControl("chkInvisibleRpte"), CheckBox).Checked = True
                Else
                    CType(e.Row.Cells(9).FindControl("chkInvisibleRpte"), CheckBox).Checked = False
                End If

                If CBool(e.Row.DataItem("esIdMecanizado")) Then
                    CType(e.Row.Cells(10).FindControl("chkIdMecanizado"), CheckBox).Checked = True
                Else
                    CType(e.Row.Cells(10).FindControl("chkIdMecanizado"), CheckBox).Checked = False
                End If

                Dim chkCopiar As System.Web.UI.WebControls.CheckBox
                chkCopiar = e.Row.Cells(6).Controls(1)

                Dim chkProt As System.Web.UI.WebControls.CheckBox
                chkProt = e.Row.Cells(7).Controls(1)

                Dim chkInvis As System.Web.UI.WebControls.CheckBox
                chkInvis = e.Row.Cells(9).Controls(1)

                Dim chkIdMecanizado As System.Web.UI.WebControls.CheckBox
                chkIdMecanizado = e.Row.Cells(10).Controls(1)

                Dim chk As System.Web.UI.WebControls.CheckBox
                chk = e.Row.Cells(0).Controls(1)
                chk.Attributes.Add("onclick", "executarlinha=false;")

                Dim chkObligatorio As System.Web.UI.WebControls.CheckBox
                chkObligatorio = e.Row.Cells(3).Controls(1)
                chkObligatorio.Attributes.Add("onclick", "ResultadosTrueOrFalse(this,'" & e.Row.DataItem("CodigoTermino") & "','" & hidObligatorio.ClientID & "','" & hidObligatorioFalse.ClientID & "','" & hidObligatorioValorTrue.ClientID & "','" & hidObligatorioValorFalse.ClientID & "')")

                Dim chkBusqueda As System.Web.UI.WebControls.CheckBox
                chkBusqueda = e.Row.Cells(4).Controls(1)
                chkBusqueda.Attributes.Add("onclick", "ResultadosTrueOrFalse(this,'" & e.Row.DataItem("CodigoTermino") & "','" & hidBusqueda.ClientID & "','" & hidBusquedaFalse.ClientID & "','" & hidBusquedaValorTrue.ClientID & "','" & hidBusquedaValorFalse.ClientID & "')")

                Dim chkClave As System.Web.UI.WebControls.CheckBox
                chkClave = e.Row.Cells(5).Controls(1)

                Dim striptClave As String = "BloqueiaCheckBoxResultadosTrueOrFalse(this,'" & e.Row.DataItem("CodigoTermino") & "','" & hidCampoClave.ClientID & "','" & hidCampoClaveFalse.ClientID & "','" & hidClaveValorVerdadeiro.ClientID & "','" & hidClaveValorFalso.ClientID & "','" & chkCopiar.ClientID & "','" & _
                                        hidCopiarTermino.ClientID & "','" & hidCopiarTerminoFalse.ClientID & "','" & hidCopiarTerminoValorVerdadeiro.ClientID & "','" & hidCopiarTerminoValorFalso.ClientID & "');"

                striptClave += "DesbloqueiaCheckBoxResultadosTrueOrFalse(this,'" & e.Row.DataItem("CodigoTermino") & "','" & hidCampoClave.ClientID & "','" & hidCampoClaveFalse.ClientID & "','" & hidClaveValorVerdadeiro.ClientID & "','" & hidClaveValorFalso.ClientID & "','" & chkIdMecanizado.ClientID & "','" & _
                                        hidIdMecanizado.ClientID & "','" & hidIdMecanizadoFalse.ClientID & "','" & hidIdMecanizadoValorTrue.ClientID & "','" & hidIdMecanizadoValorFalse.ClientID & "');"

                chkClave.Attributes.Add("onclick", striptClave)

                chkCopiar.Attributes.Add("onclick", "BloqueiaCheckBoxResultadosTrueOrFalse(this,'" & e.Row.DataItem("CodigoTermino") & "','" & hidCopiarTermino.ClientID & "','" & hidCopiarTerminoFalse.ClientID & "','" & hidCopiarTerminoValorVerdadeiro.ClientID & "','" & hidCopiarTerminoValorFalso.ClientID & "','" & chkClave.ClientID & "','" & _
                                         hidCampoClave.ClientID & "','" & hidCampoClaveFalse.ClientID & "','" & hidClaveValorVerdadeiro.ClientID & "','" & hidClaveValorFalso.ClientID & "')")

                chkProt.Attributes.Add("onclick", "ResultadosTrueOrFalse(this,'" & e.Row.DataItem("CodigoTermino") & "','" & hidProtegido.ClientID & "','" & hidProtegidoFalse.ClientID & "','" & hidProtegidoValorTrue.ClientID & "','" & hidProtegidoValorFalse.ClientID & "')")

                chkInvis.Attributes.Add("onclick", "ResultadosTrueOrFalse(this,'" & e.Row.DataItem("CodigoTermino") & "','" & hidInvisibleReporte.ClientID & "','" & hidInvisibleReporteFalse.ClientID & "','" & hidInvisibleReporteValorTrue.ClientID & "','" & hidInvisibleReporteValorFalse.ClientID & "')")

                'Dim scriptIdMecanizado As String = "ResultadosTrueOrFalse(this,'" & e.Row.DataItem("CodigoTermino") & "','" & hidIdMecanizado.ClientID & "','" & hidIdMecanizadoFalse.ClientID & "','" & hidIdMecanizadoValorTrue.ClientID & "','" & hidIdMecanizadoValorFalse.ClientID & "');"

                Dim scriptIdMecanizado As String = "DesmarcaTodosCheckBoxExceto(this,'" & ProsegurGridView2.ClientID & "','9','" & e.Row.DataItem("CodigoTermino") & "','" & hidIdMecanizado.ClientID & "','" & hidIdMecanizadoFalse.ClientID & "','" & hidIdMecanizadoValorTrue.ClientID & "','" & hidIdMecanizadoValorFalse.ClientID & "');"

                chkIdMecanizado.Attributes.Add("onclick", scriptIdMecanizado)
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Tradução do cabecalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("006_lbl_grd_checkbox")
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("006_lbl_grd_codigo")
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("006_lbl_grd_descripcion")
                CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("006_lbl_grd_observaciones")
            End If

            If e.Row.RowType = DataControlRowType.EmptyDataRow Then
                ProsegurGridView1.Columns(0).HeaderText = Traduzir("006_lbl_grd_checkbox")
                ProsegurGridView1.Columns(1).HeaderText = Traduzir("006_lbl_grd_codigo")
                ProsegurGridView1.Columns(2).HeaderText = Traduzir("006_lbl_grd_descripcion")
                ProsegurGridView1.Columns(3).HeaderText = Traduzir("006_lbl_grd_observaciones")
            End If
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Tradução do cabecalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Criado
    ''' </history>
    Private Sub ProsegurGridView2_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView2.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("006_lbl_grd_checkbox")
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("006_lbl_grd_codigo")
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("006_lbl_grd_descripcion")
                CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("006_lbl_grd_obligatorio")
                CType(e.Row.Cells(4).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("006_lbl_grd_busqueda")
                CType(e.Row.Cells(5).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("006_lbl_grd_campoclave")
                CType(e.Row.Cells(6).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("006_lbl_grd_copiarTermino")
                CType(e.Row.Cells(7).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("006_lbl_grd_Protegido")
                CType(e.Row.Cells(9).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("006_lbl_grd_InvisibleRpt")
                CType(e.Row.Cells(10).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("006_lbl_grd_IdMecanizado")
            End If

            If e.Row.RowType = DataControlRowType.EmptyDataRow Then
                ProsegurGridView2.Columns(0).HeaderText = Traduzir("006_lbl_grd_checkbox")
                ProsegurGridView2.Columns(1).HeaderText = Traduzir("006_lbl_grd_codigo")
                ProsegurGridView2.Columns(2).HeaderText = Traduzir("006_lbl_grd_descripcion")
                ProsegurGridView2.Columns(3).HeaderText = Traduzir("006_lbl_grd_obligatorio")
                ProsegurGridView2.Columns(4).HeaderText = Traduzir("006_lbl_grd_busqueda")
                ProsegurGridView2.Columns(5).HeaderText = Traduzir("006_lbl_grd_campoclave")
                ProsegurGridView2.Columns(6).HeaderText = Traduzir("006_lbl_grd_copiarTermino")
                ProsegurGridView2.Columns(7).HeaderText = Traduzir("006_lbl_grd_Protegido")
                ProsegurGridView2.Columns(9).HeaderText = Traduzir("006_lbl_grd_InvisibleRpt")
                ProsegurGridView2.Columns(10).HeaderText = Traduzir("006_lbl_grd_IdMecanizado")
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPreencheDados
        Try

            Dim objDT As DataTable


            Dim objColTerminos As IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion
            objColTerminos = TerminosTemPorario


            If objColTerminos IsNot Nothing _
                AndAlso objColTerminos.Count > 0 Then

                ProsegurGridView1.DataSource = Nothing
                ProsegurGridView1.DataBind()

                ' converter objeto para datatable(para efetuar a ordenação)
                objDT = ProsegurGridView1.ConvertListToDataTable(objColTerminos)

                objDT.DefaultView.Sort = " Codigo ASC"

                ProsegurGridView1.ControleDataSource = objDT

            Else

                'Limpa a consulta
                ProsegurGridView1.DataSource = Nothing
                ProsegurGridView1.DataBind()

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If


        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

#End Region

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Remove todos os terminos selecionados do grid e da coleção de terminos iac, e o adiciona no grid e na coleção de terminos.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub btnremove_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnremove.Click


        Dim valor As String
        Dim valores() As String
        Dim ListaValores As New List(Of String)
        Dim ordemTerminoIac As Integer = 0

        VerificaCheckBoxObligatorio()

        VerificaCheckBoxBusqueda()

        VerificaCheckBoxCampoClave()

        VerificaCheckBoxCopiarTermino()

        valor = hidTerminoIacSelecionado.Value

        valores = valor.Split("|")

        ListaValores = VerificaValorRepetido(valores)

        Dim TerminosIac As New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
        Dim ColTerminosIac As New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion

        Dim objTerminos As ContractoServicio.TerminoIac.GetTerminoIac.TerminoIac
        Dim objColTerminos As New ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion

        If hidTerminoIacSelecionado.Value <> String.Empty AndAlso hidTerminoIacSelecionado.Value <> Nothing Then

            For Each item As String In ListaValores
                If item <> Nothing AndAlso item <> String.Empty Then

                    objTerminos = New IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIac
                    objTerminos = SelectInformacoesTerminosIac(TerminosIacCompletos, item)

                    objColTerminos.Add(objTerminos)

                    TerminosTemPorario.Add(objTerminos)

                    SelectDeleteTerminosIac(TerminosIacCompletos, item)

                    Salvar = True

                End If
            Next

        End If

        If TerminosIacCompletos.Count > 0 Then

            TerminosIacCompletos.Sort(Function(a, b) a.OrdenTermino.CompareTo(b.OrdenTermino))

            For i = 0 To TerminosIacCompletos.Count - 1
                TerminosIacCompletos.Item(i).OrdenTermino = i + 1
            Next

        End If

        CarregaGrid()
        CarregaGridTerminosIac()
        valor = Nothing
        valores = Nothing
        ListaValores = Nothing
        hidTerminoIacSelecionado.Value = Nothing
        hidTerminoSelecionado.Value = Nothing
        hidOrden.Value = Nothing

    End Sub

    ''' <summary>
    ''' Adiciona todos os terminos selecionados no grid e na coleção de terminos iac, e os remove da coleção de terminos.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub btnAdiciona_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAdiciona.Click

        Dim valor As String
        Dim valores() As String
        Dim ListaValores As New List(Of String)

        VerificaCheckBoxObligatorio()

        VerificaCheckBoxBusqueda()

        VerificaCheckBoxCampoClave()

        VerificaCheckBoxCopiarTermino()

        valor = hidTerminoSelecionado.Value

        valores = valor.Split("|")

        ListaValores = VerificaValorRepetido(valores)

        Dim objTerminosIac As ContractoServicio.Iac.GetIacDetail.TerminosIac
        Dim objColTerminosIac As New ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion
        Dim objColTerminos As New ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion
        If hidTerminoSelecionado.Value <> String.Empty AndAlso hidTerminoSelecionado.Value <> Nothing Then

            For Each item As String In ListaValores
                If item <> Nothing AndAlso item <> String.Empty Then
                    objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                    objTerminosIac = SelectInformacoesTerminos(TerminosTemPorario, item)
                    objTerminosIac.OrdenTermino = TerminosIacCompletos.Count + 1

                    objColTerminosIac.Add(objTerminosIac)
                    If TerminosIacTemPorario.Count > 0 Then
                        If SelectTerminosIac(TerminosIacTemPorario, objTerminosIac.CodigoTermino) Then
                            TerminosIacCompletos.Add(objTerminosIac)
                            SelectDeleteTerminos(TerminosTemPorario, objTerminosIac.CodigoTermino)
                        Else
                            TerminosIacCompletos.Add(objTerminosIac)
                            SelectDeleteTerminos(TerminosTemPorario, objTerminosIac.CodigoTermino)
                        End If
                    Else
                        TerminosIacCompletos.Add(objTerminosIac)
                        SelectDeleteTerminos(TerminosTemPorario, objTerminosIac.CodigoTermino)
                    End If
                End If


            Next


        End If
        Salvar = True
        CarregaGridTerminosIac()
        CarregaGrid()
        valor = Nothing
        valores = Nothing
        ListaValores = Nothing
        hidTerminoSelecionado.Value = Nothing
        hidTerminoIacSelecionado.Value = Nothing

    End Sub

    ''' <summary>
    ''' Grava todas as alterações no banco.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub btnGrabar_Click(sender As Object, e As System.EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()
    End Sub

    ''' <summary>
    ''' Sai da pagina e volta para a pagina de busquedaIac.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        ExecutarCancelar()
    End Sub

    ''' <summary>
    ''' Sai da pagina e volta para a pagina de busquedaIac.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub btnVolver_Click(sender As Object, e As System.EventArgs) Handles btnVolver.Click
        ExecutarVolver()
    End Sub

    ''' <summary>
    ''' Altera a ordem dos registro, elevando o registro selecionado um nivel acima.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub btnAcima_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAcima.Click

        Dim valor As String
        Dim valores() As String
        Dim ListaValores As New List(Of String)

        VerificaCheckBoxObligatorio()

        VerificaCheckBoxBusqueda()

        VerificaCheckBoxCampoClave()

        VerificaCheckBoxCopiarTermino()

        valor = hidOrden.Value

        valores = valor.Split("|")

        ListaValores = VerificaValorRepetido(valores)

        Dim objTerminosIacSuperior As ContractoServicio.Iac.GetIacDetail.TerminosIac
        Dim objTerminosIacInferior As ContractoServicio.Iac.GetIacDetail.TerminosIac
        Dim contador As Integer = 0

        If valor <> String.Empty Then

            For Each item As String In Orderacao(ListaValores)
                objTerminosIacSuperior = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                objTerminosIacInferior = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac

                contador = contador + 1
                If item <> String.Empty AndAlso item <> Nothing Then

                    If valores.Count <> TerminosIacCompletos.Count Then

                        If item <> 1 Then

                            If item <> contador Then

                                Dim NumeroOrden As Integer
                                NumeroOrden = 0
                                NumeroOrden = CType(item, Integer)

                                objTerminosIacSuperior = SelectInformacoesOrdenTermino(TerminosIacCompletos, NumeroOrden)
                                objTerminosIacSuperior.OrdenTermino = NumeroOrden - 1

                                objTerminosIacInferior = SelectInformacoesOrdenTermino(TerminosIacCompletos, objTerminosIacSuperior.OrdenTermino)
                                objTerminosIacInferior.OrdenTermino = NumeroOrden

                                SelectDeleteTerminosIac(TerminosIacCompletos, objTerminosIacSuperior.CodigoTermino)
                                SelectDeleteTerminosIac(TerminosIacCompletos, objTerminosIacInferior.CodigoTermino)

                                TerminosIacCompletos.Add(objTerminosIacInferior)
                                TerminosIacCompletos.Add(objTerminosIacSuperior)

                            End If
                        End If
                    End If
                End If
            Next
        End If

        Salvar = True
        CarregaGridTerminosIac()

        valor = Nothing
        valores = Nothing
        ListaValores = Nothing
        hidOrden.Value = Nothing
        hidTerminoIacSelecionado.Value = Nothing

    End Sub

    ''' <summary>
    ''' Altera a ordem dos registro, sendo que o registro selecionado decresce um nivel abaixo.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub btnAbaixo_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAbaixo.Click

        Dim valor As String
        Dim valores() As String
        Dim ListaValores As New List(Of String)

        VerificaCheckBoxObligatorio()

        VerificaCheckBoxBusqueda()

        VerificaCheckBoxCampoClave()

        VerificaCheckBoxCopiarTermino()

        valor = hidOrden.Value

        valores = valor.Split("|")

        ListaValores = VerificaValorRepetido(valores)

        Dim objTerminosIacSuperior As ContractoServicio.Iac.GetIacDetail.TerminosIac
        Dim objTerminosIacInferior As ContractoServicio.Iac.GetIacDetail.TerminosIac
        Dim contador As Integer = TerminosIacCompletos.Count

        If valor <> String.Empty Then

            For Each item As String In OrderacaoDesc(ListaValores)
                objTerminosIacSuperior = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                objTerminosIacInferior = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac

                If item <> String.Empty AndAlso item <> Nothing Then

                    If valores.Count <> TerminosIacCompletos.Count Then

                        If item <> TerminosIacCompletos.Count Then

                            If item <> contador Then

                                Dim NumeroOrden As Integer
                                NumeroOrden = 0
                                NumeroOrden = CType(item, Integer)

                                objTerminosIacSuperior = SelectInformacoesOrdenTermino(TerminosIacCompletos, NumeroOrden)
                                objTerminosIacSuperior.OrdenTermino = NumeroOrden + 1

                                objTerminosIacInferior = SelectInformacoesOrdenTermino(TerminosIacCompletos, objTerminosIacSuperior.OrdenTermino)
                                objTerminosIacInferior.OrdenTermino = NumeroOrden


                                SelectDeleteTerminosIac(TerminosIacCompletos, objTerminosIacSuperior.CodigoTermino)
                                SelectDeleteTerminosIac(TerminosIacCompletos, objTerminosIacInferior.CodigoTermino)

                                TerminosIacCompletos.Add(objTerminosIacInferior)
                                TerminosIacCompletos.Add(objTerminosIacSuperior)
                            End If
                        End If
                    End If
                End If

                contador = contador - 1
            Next

        End If

        Salvar = True
        CarregaGridTerminosIac()
        valor = Nothing
        valores = Nothing
        ListaValores = Nothing
        hidOrden.Value = Nothing
        hidTerminoIacSelecionado.Value = Nothing

    End Sub

    'Ações que podem ser chamadas a qualquer momento
#Region "Ações Botões Independentes"

    ''' <summary>
    ''' Função do botão cancelar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarCancelar()
        Response.Redirect("~/BusquedaIac.aspx", False)
    End Sub

    ''' <summary>
    ''' Função do botão grabar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarGrabar()
        Try

            Dim objProxyIac As New Comunicacion.ProxyIac
            Dim objPeticionIac As New IAC.ContractoServicio.Iac.SetIac.Peticion
            Dim objRespuestaIac As IAC.ContractoServicio.Iac.SetIac.Respuesta
            Dim strErro As New Text.StringBuilder(String.Empty)

            ValidarCamposObrigatorios = True

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If


            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticionIac.vigente = True
            Else
                objPeticionIac.vigente = chkVigente.Checked
            End If

            EsVigente = chkVigente.Checked

            objPeticionIac.CodidoIac = txtCodigoIac.Text
            objPeticionIac.DescripcionIac = txtDescricaoIac.Text
            objPeticionIac.ObservacionesIac = txtObservacionesIac.Text
            objPeticionIac.CodUsuario = MyBase.LoginUsuario
            objPeticionIac.EsDeclaradoCopia = chkCopiarDeclarados.Checked
            objPeticionIac.EsInvisible = chkInvisible.Checked
            objPeticionIac.EspecificoSaldos = chkDisponivelNuevoSaldos.Checked

            VerificaCheckBoxObligatorio()

            VerificaCheckBoxBusqueda()

            VerificaCheckBoxCampoClave()

            VerificaCheckBoxCopiarTermino()

            VerificaCheckBoxProtegido()

            VerificaCheckBoxInvisibleReporte()

            VerificaCheckBoxIdMecanizado()

            Dim objColTerminosIac As New ContractoServicio.Iac.SetIac.TerminosIacColeccion
            Dim objTerminos As ContractoServicio.Iac.SetIac.TerminosIac

            For Each terminosIac As ContractoServicio.Iac.GetIacDetail.TerminosIac In TerminosIacCompletos
                objTerminos = New ContractoServicio.Iac.SetIac.TerminosIac

                objTerminos.CodigoTermino = terminosIac.CodigoTermino
                objTerminos.DescripcionTermino = terminosIac.DescripcionTermino
                objTerminos.EsBusquedaParcial = terminosIac.EsBusquedaParcial
                objTerminos.EsObligatorio = terminosIac.EsObligatorio
                objTerminos.EsCampoClave = terminosIac.EsCampoClave
                objTerminos.OrdenTermino = terminosIac.OrdenTermino
                objTerminos.EsTerminoCopia = terminosIac.EsTerminoCopia
                objTerminos.EsProtegido = terminosIac.esProtegido
                objTerminos.esInvisibleRpte = terminosIac.esInvisibleRpte
                objTerminos.esIdMecanizado = terminosIac.esIdMecanizado

                objColTerminosIac.Add(objTerminos)

            Next

            If TerminosIacCompletos.Count > 0 Then
                objPeticionIac.TerminosIac = objColTerminosIac
            End If

            objRespuestaIac = objProxyIac.SetIac(objPeticionIac)

            'Define a ação de busca somente se houve retorno de canais

            If Master.ControleErro.VerificaErro(objRespuestaIac.CodigoError, objRespuestaIac.NombreServidorBD, objRespuestaIac.MensajeError) Then

                If Master.ControleErro.VerificaErro(objRespuestaIac.CodigoError, objRespuestaIac.NombreServidorBD, objRespuestaIac.MensajeError) Then

                    Dim url As String = "BusquedaIac.aspx"

                    'Registro gravado com sucesso
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_grabado_suceso", "alert('" & Traduzir("001_msg_grabado_suceso") & _
                                                                                        "');RedirecionaPaginaNormal('" & url & "');", True)
                Else
                    Exit Sub
                End If

            Else

                If objRespuestaIac.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    Master.ControleErro.ShowError(objRespuestaIac.MensajeError)
                End If

                Exit Sub

            End If

            ValorCodigo = txtCodigoIac.Text
            ValorDescricao = txtDescricaoIac.Text
            ValorVigente = chkVigente.Checked
            ValorInvisible = chkInvisible.Checked
            ValorObservaciones = txtObservacionesIac.Text
            Salvar = False

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Função do botão volver.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarVolver()
        Response.Redirect("~/BusquedaIac.aspx", False)
    End Sub

#End Region
#End Region

#Region "[EVENTOS TEXTBOX]"

    ''' <summary>
    ''' Faz a verificação se o codigo ja existe informando a mensagem de erro, e verifica se o codigo sofreu alguma alteração
    ''' se sofreu abilita a mensagem perguntando se quer salvar a alteração.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub txtCodigoIac_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoIac.TextChanged

        Try
            If ExisteCodigoIac(txtCodigoIac.Text) Then
                CodigoExistente = True
            Else
                CodigoExistente = False
            End If

            Threading.Thread.Sleep(100)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Faz a verificação se a descrição ja existe informando a mensagem de erro, e verifica se a descrição sofreu alguma alteração
    ''' se sofreu abilita a mensagem perguntando se quer salvar a alteração.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub txtDescricaoIac_TextChanged(sender As Object, e As System.EventArgs) Handles txtDescricaoIac.TextChanged
        Try

            If ExisteDescricaoIac(txtDescricaoIac.Text) Then
                DescricaoExistente = True
            Else
                DescricaoExistente = False
            End If

            Threading.Thread.Sleep(100)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Verifica se a observação sofreu alguma alteração se sofreu abilita a mensagem perguntando se quer salvar a alteração.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub txtObservacionesIac_TextChanged(sender As Object, e As System.EventArgs) Handles txtObservacionesIac.TextChanged

        If ValorObservaciones <> txtObservacionesIac.Text Then
            Salvar = True
        Else
            Salvar = False
        End If

    End Sub

#End Region

#End Region

#Region "[CONTROLE DE ESTADO]"

    Private Enum eAcaoEspecifica As Integer
        AltaConRegistro = 20
    End Enum

    ''' <summary>
    '''  Controle dos botões
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/02/2009 Criado
    ''' </history> 
    Public Sub ControleBotoes()
        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta
                btnGrabar.Visible = True        '2
                btnCancelar.Visible = True      '3
                btnAcima.Enabled = True
                btnAbaixo.Enabled = True

                'Estado Inicial Controles                                
                txtCodigoIac.Enabled = True
                btnVolver.Visible = False       '7

                chkVigente.Checked = True
                chkVigente.Enabled = False
                chkVigente.Visible = False
                lblVigente.Visible = False

                btnGrabar.Habilitado = True

            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                btnCancelar.Visible = False              '2
                btnVolver.Visible = True                '4

                'Estado Inicial Controles
                txtCodigoIac.Enabled = False
                txtDescricaoIac.Enabled = False
                txtObservacionesIac.Enabled = False
                'lblVigente.Visible = True
                chkCopiarDeclarados.Enabled = False
                chkVigente.Enabled = False
                chkInvisible.Enabled = False
                chkDisponivelNuevoSaldos.Enabled = False
                btnGrabar.Visible = False               '7
                btnAcima.Enabled = False
                btnAbaixo.Enabled = False
                btnAdiciona.Enabled = False
                btnremove.Enabled = False
                ProsegurGridView1.Enabled = False
                ProsegurGridView2.Enabled = False

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                txtCodigoIac.Enabled = False
                chkVigente.Visible = True
                chkInvisible.Visible = True
                chkDisponivelNuevoSaldos.Visible = True
                lblVigente.Visible = True

                btnGrabar.Visible = True               '5
                btnCancelar.Visible = True             '6
                btnVolver.Visible = False              '7

                'lblVigente.Visible = True
                If chkVigente.Checked AndAlso EsVigente Then
                    chkVigente.Enabled = False
                Else
                    chkVigente.Enabled = True
                End If

                btnGrabar.Habilitado = True

            Case Aplicacao.Util.Utilidad.eAcao.NoAction
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Busca
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.NoAction

        End Select

        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem de erro
        If MontaMensagensErro() <> String.Empty Then
            Master.ControleErro.ShowError(MontaMensagensErro, False)
        End If

    End Sub

#End Region



    Public Sub New()

    End Sub
End Class