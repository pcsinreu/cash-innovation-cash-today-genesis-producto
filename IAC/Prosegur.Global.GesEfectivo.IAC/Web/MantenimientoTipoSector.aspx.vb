Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' Página de Gerenciamento de Tipos Sectores 
''' </summary>
''' <remarks></remarks>
''' <history>[pgoncalves] 04/03/13 - Criado</history>
Public Class MantenimientoTipoSector
    Inherits Base

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()
        'seta o foco para o proximo controle quando preciona o enter
        txtCodigoTipoSetor.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtCodigoTipoSetor.ClientID & "','" & txtCodigoTipoSetor.MaxLength & "');")
        txtDescricaoTiposSetor.Attributes.Add("onblur", "limitaCaracteres('" & txtDescricaoTiposSetor.ClientID & "','" & txtDescricaoTiposSetor.MaxLength & "');")
        txtCodigoTipoSetor.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoTiposSetor.ClientID & "').focus();return false;}} else {return true}; ")

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)
    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigoTipoSetor.TabIndex = 1
        txtDescricaoTiposSetor.TabIndex = 2
        btnAlta.TabIndex = 3
        chkVigente.TabIndex = 4
        lstCaracteristicasDisponiveis.TabIndex = 5
        imbAdicionarTodasCaracteristicas.TabIndex = 6
        imbAdicionarCaracteristicasSelecionadas.TabIndex = 7
        imbRemoverCaracteristicasSelecionadas.TabIndex = 8
        imbRemoverTodasCaracteristicas.TabIndex = 9
        lstCaracteristicasSelecionadas.TabIndex = 10

        btnGrabar.TabIndex = 11
        btnCancelar.TabIndex = 12
        btnVolver.TabIndex = 13

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.TIPOSECTOR

        ' não deve adicionar scripts no load da base
        MyBase.AddScripts = False

    End Sub

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

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
                Dim strCodigoTipoSector = Request.QueryString("codTipoSector")

                'Preenche a lista de características
                PreencherListaCaracteristicas()

                Select Case MyBase.Acao

                    Case Aplicacao.Util.Utilidad.eAcao.Modificacion, Aplicacao.Util.Utilidad.eAcao.Consulta

                        CarregaDados(strCodigoTipoSector)
                        txtDescricaoTiposSetor.Focus()

                        If MyBase.Acao = MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then

                            lstCaracteristicasDisponiveis.Visible = False
                            imbAdicionarCaracteristicasSelecionadas.Visible = False
                            imbAdicionarTodasCaracteristicas.Visible = False
                            imbRemoverCaracteristicasSelecionadas.Visible = False
                            imbRemoverTodasCaracteristicas.Visible = False

                            lstCaracteristicasSelecionadas.Enabled = False

                        End If
                    Case Else
                        txtCodigoTipoSetor.Focus()
                End Select
            End If
            'chama a função que trata o foco
            TrataFoco()

            AdicionarScripts()

            ConsomeCodigoAjeno()
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

        lblCodigoTiposSetor.Text = Traduzir("019_lbl_codigo")
        lblDescricaoTiposSetor.Text = Traduzir("019_lbl_descripciontiposSetor")
        lblVigente.Text = Traduzir("019_chk_vigente")
        chkVigente.Text = String.Empty
        lblTituloTiposSetor.Text = Traduzir("019_titulo_matenimentotiposSetor")
        lblTituloCaracteristicas.Text = Traduzir("019_titulo_caracteristicas_matenimentotiposSetor")
        lblCodigoAjeno.Text = Traduzir("019_lbl_codigoAjeno")
        lblDescricaoCodeA.Text = Traduzir("019_lbl_descricaoAjeno")
        csvCodigoObrigatorio.ErrorMessage = Traduzir("019_msg_tipoSetorcodigoobligatorio")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("019_msg_tipoSetordescripcionobligatorio")
        csvCodigoExistente.ErrorMessage = Traduzir("019_msg_codigotiposSetorexistente")
        csvlstCaracteristicas.ErrorMessage = Traduzir("019_msg_CaracteristicaObrigatoria")
        Master.TituloPagina = Traduzir("019_titulo_matenimentotiposSetor")
        btnAlta.ExibirLabelBtn = False
    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Propriedades
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 05/03/2013 Criado
    ''' </history> 
    Public Property TipoSetor() As IAC.ContractoServicio.TipoSetor.GetTiposSectores.TipoSetor
        Get
            Return DirectCast(ViewState("TipoSetor"), IAC.ContractoServicio.TipoSetor.GetTiposSectores.TipoSetor)
        End Get
        Set(value As IAC.ContractoServicio.TipoSetor.GetTiposSectores.TipoSetor)
            ViewState("TipoSetor") = value
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

    Private Property EsVigente() As Boolean
        Get
            Return ViewState("EsVigente")
        End Get
        Set(value As Boolean)
            ViewState("EsVigente") = value
        End Set
    End Property

    Private Property DescricaoAtual() As String
        Get
            Return ViewState("DescricaoAtual")
        End Get
        Set(value As String)
            ViewState("DescricaoAtual") = value.Trim
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

    Private Property OidTipoSetor() As String
        Get
            Return ViewState("OidTipoSetor")
        End Get
        Set(value As String)
            ViewState("OidTipoSetor") = value
        End Set
    End Property


    ''' <summary>
    ''' Armazena em viewstate os códigos ajenos na petição
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 07/07/2013 - Criado
    ''' </history>
    Private Property CodigosAjenosPeticion() As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Get
            Return DirectCast(ViewState("CodigosAjenosPeticion"), ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
        End Get

        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
            ViewState("CodigosAjenosPeticion") = value
        End Set

    End Property

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Função do Botão Grabar
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 - Criado
    ''' </history>
    Public Sub ExecutarGrabar()
        Try

            Dim objProxyTipoSetor As New Comunicacion.ProxyTipoSetor
            Dim objPeticionTipoSetor As New IAC.ContractoServicio.TipoSetor.SetTiposSectores.Peticion
            Dim objRespuestaTipoSetor As IAC.ContractoServicio.TipoSetor.SetTiposSectores.Respuesta
            Dim strErro As New Text.StringBuilder(String.Empty)

            ValidarCamposObrigatorios = True

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If

            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticionTipoSetor.bolActivo = True
            Else
                objPeticionTipoSetor.bolActivo = chkVigente.Checked
            End If

            ' atualizar propriedade
            EsVigente = chkVigente.Checked

            objPeticionTipoSetor.codTipoSector = txtCodigoTipoSetor.Text
            objPeticionTipoSetor.desTipoSector = txtDescricaoTiposSetor.Text
            objPeticionTipoSetor.oidTipoSector = OidTipoSetor

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticionTipoSetor.gmtCreacion = DateTime.Now
                objPeticionTipoSetor.desUsuarioCreacion = MyBase.LoginUsuario
            Else
                objPeticionTipoSetor.gmtCreacion = TipoSetor.gmtCreacion
                objPeticionTipoSetor.desUsuarioCreacion = TipoSetor.desUsuarioCreacion
            End If

            objPeticionTipoSetor.gmtModificacion = DateTime.Now
            objPeticionTipoSetor.desUsuarioModificacion = MyBase.LoginUsuario

            objPeticionTipoSetor.codCaractTipoSector = New ContractoServicio.TipoSetor.SetTiposSectores.CaracteristicaColeccion()

            For Each objItem As ListItem In lstCaracteristicasSelecionadas.Items
                objPeticionTipoSetor.codCaractTipoSector.Add(New ContractoServicio.TipoSetor.SetTiposSectores.Caracteristica() With {.codCaractTipoSector = objItem.Value})
            Next

            objPeticionTipoSetor.CodigosAjenos = CodigosAjenosPeticion

            objRespuestaTipoSetor = objProxyTipoSetor.SetTiposSectores(objPeticionTipoSetor)
            'Limpar o oidTipoSetor
            OidTipoSetor = String.Empty

            Dim url As String = "BusquedaTipoSector.aspx"

            Session.Remove("objRespuestaGEPR_TTIPO_SECTOR")
            If Master.ControleErro.VerificaErro(objRespuestaTipoSetor.CodigoError, objRespuestaTipoSetor.NombreServidorBD, objRespuestaTipoSetor.MensajeError) Then
                If Master.ControleErro.VerificaErro(objRespuestaTipoSetor.CodigoError, objRespuestaTipoSetor.NombreServidorBD, objRespuestaTipoSetor.MensajeError) Then
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_grabado_suceso", "alert('" & Traduzir("001_msg_grabado_suceso") & _
                                                                                    "'); RedirecionaPaginaNormal('" & url & "');", True)
                Else
                    Exit Sub
                End If
            Else
                If objRespuestaTipoSetor.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    If objRespuestaTipoSetor.MensajeError <> String.Empty Then
                        Master.ControleErro.ShowError(objRespuestaTipoSetor.MensajeError, False)
                    End If
                End If
                Exit Sub
            End If
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Função do botão cancelar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 - Criado
    ''' </history>
    Public Sub ExecutarCancelar()
        Response.Redirect("~/BusquedaTipoSector.aspx", False)
    End Sub

    ''' <summary>
    ''' Função do botão volver
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 - Criado
    ''' </history>
    Public Sub ExecutarVolver()
        Response.Redirect("~/BusquedaTipoSector.aspx", False)
    End Sub

    ''' <summary>
    '''  Carrega os dados quando a página é carregada pela primeira vez
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Criado
    ''' </history> 
    Public Sub CarregaDados(codigo As String)

        Dim objProxy As New Comunicacion.ProxyTipoSetor
        Dim objPeticion As New ContractoServicio.TipoSetor.GetTiposSectores.Peticion
        Dim objRespuesta As New ContractoServicio.TipoSetor.GetTiposSectores.Respuesta

        objPeticion.codTipoSector = codigo
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objProxy.GetTiposSectores(objPeticion)
        TipoSetor = objRespuesta.TipoSetor(0)

        If objRespuesta.TipoSetor IsNot Nothing Then
            Dim objColCaracteristicas As New ContractoServicio.TipoSetor.GetTiposSectores.CaracteristicaRespuestaColeccion

            Dim iCodigoAjeno = (From item In objRespuesta.TipoSetor(0).CodigosAjenos
                               Where item.bolDefecto = True).FirstOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.codAjeno
                txtCodigoAjeno.ToolTip = iCodigoAjeno.codAjeno
                txtDescricaoCodigoAjeno.Text = iCodigoAjeno.desAjeno
                txtDescricaoCodigoAjeno.ToolTip = iCodigoAjeno.desAjeno
            End If

            txtCodigoTipoSetor.Text = objRespuesta.TipoSetor(0).codTipoSector
            txtCodigoTipoSetor.ToolTip = objRespuesta.TipoSetor(0).codTipoSector

            txtDescricaoTiposSetor.Text = objRespuesta.TipoSetor(0).desTipoSector
            txtDescricaoTiposSetor.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objRespuesta.TipoSetor(0).desTipoSector, String.Empty)

            If objRespuesta.TipoSetor(0).bolActivo Then
                chkVigente.Enabled = False
            Else
                chkVigente.Enabled = True
            End If
            chkVigente.Checked = objRespuesta.TipoSetor(0).bolActivo
            EsVigente = objRespuesta.TipoSetor(0).bolActivo
            OidTipoSetor = objRespuesta.TipoSetor(0).oidTipoSector

            If objRespuesta.TipoSetor(0).CaractTipoSector IsNot Nothing Then

                For Each objCarac As ContractoServicio.TipoSetor.GetTiposSectores.CaracteristicaRespuesta In objRespuesta.TipoSetor(0).CaractTipoSector
                    objCarac.Descripcion = objCarac.Codigo & " - " & objCarac.Descripcion
                    objColCaracteristicas.Add(objCarac)
                Next

                lstCaracteristicasSelecionadas.AppendDataBoundItems = True
                lstCaracteristicasSelecionadas.Items.Clear()
                lstCaracteristicasSelecionadas.DataTextField = "Descripcion"
                lstCaracteristicasSelecionadas.DataValueField = "Codigo"
                lstCaracteristicasSelecionadas.DataSource = objColCaracteristicas
                lstCaracteristicasSelecionadas.DataBind()

                For Each objItem As ListItem In lstCaracteristicasSelecionadas.Items
                    lstCaracteristicasDisponiveis.Items.Remove(lstCaracteristicasDisponiveis.Items.FindByValue(objItem.Value))
                Next

            End If

        End If

        'Se for modificação então guarda a descriçaõ atual para validação
        If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
            DescricaoAtual = txtDescricaoTiposSetor.Text
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
                If txtCodigoTipoSetor.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoTipoSetor.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If txtDescricaoTiposSetor.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoTiposSetor.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

                If lstCaracteristicasSelecionadas.Items.Count = 0 Then
                    strErro.Append(csvlstCaracteristicas.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvlstCaracteristicas.IsValid = False
                End If

                If SetarFocoControle AndAlso Not focoSetado Then
                    imbAdicionarCaracteristicasSelecionadas.Focus()
                    focoSetado = True
                End If

            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If CodigoExistente Then

                strErro.Append(csvCodigoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoTipoSetor.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    '''  Faz o tratamento de foco da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Criado
    ''' </history> 
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
    ''' Função responsavel por preencher a lista de características.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 05/03/2013 Created
    ''' </history>
    Public Sub PreencherListaCaracteristicas()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboCaractTipoSector.Respuesta
        objRespuesta = objProxyUtilida.GetComboCaractTipoSector()

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Master.ControleErro.ShowError(objRespuesta.MensajeError, False)
            Exit Sub
        End If

        If objRespuesta.Caracteristicas.Count = 0 Then
            Exit Sub
        End If

        For Each objCaracteristica As IAC.ContractoServicio.Utilidad.GetComboCaractTipoSector.Caracteristica In objRespuesta.Caracteristicas
            objCaracteristica.Descripcion = objCaracteristica.Codigo & " - " & objCaracteristica.Descripcion
        Next

        lstCaracteristicasDisponiveis.AppendDataBoundItems = True
        lstCaracteristicasDisponiveis.Items.Clear()
        lstCaracteristicasDisponiveis.DataTextField = "Descripcion"
        lstCaracteristicasDisponiveis.DataValueField = "Codigo"
        lstCaracteristicasDisponiveis.DataSource = objRespuesta.Caracteristicas
        lstCaracteristicasDisponiveis.DataBind()

    End Sub

    ''' <summary>
    ''' Verifica se o código tipo setor ja foi cadastrado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 06/03/2013 - Criado
    ''' </history>
    Private Function ExisteCodigoTipoSetor(codigoTipoSetor As String) As Boolean

        Dim objRespostaVerificarCodigoTipoSetor As IAC.ContractoServicio.TipoSetor.GetTiposSectores.Respuesta
        Try

            Dim objProxyTipoSetor As New Comunicacion.ProxyTipoSetor
            Dim objPeticionVerificarCodigoTipoSetor As New IAC.ContractoServicio.TipoSetor.GetTiposSectores.Peticion

            'Verifica se o código do Tipo existe no BD
            objPeticionVerificarCodigoTipoSetor.codTipoSector = codigoTipoSetor.Trim
            objPeticionVerificarCodigoTipoSetor.bolActivo = Nothing
            objPeticionVerificarCodigoTipoSetor.ParametrosPaginacion.RealizarPaginacion = False

            objRespostaVerificarCodigoTipoSetor = objProxyTipoSetor.GetTiposSectores(objPeticionVerificarCodigoTipoSetor)

            If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoTipoSetor.CodigoError, objRespostaVerificarCodigoTipoSetor.NombreServidorBD, objRespostaVerificarCodigoTipoSetor.MensajeError) Then
                If (objRespostaVerificarCodigoTipoSetor.TipoSetor.Count > 0) Then
                    Return True
                End If
            Else
                Return False
                Master.ControleErro.ShowError(objRespostaVerificarCodigoTipoSetor.MensajeError)
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Function

    ''' <summary>
    ''' Consome o valor da session da coleção de código ajeno
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 07/05/2013 - Criado
    ''' </history>
    Private Sub ConsomeCodigoAjeno()

        If Session("objRespuestaGEPR_TTIPO_SECTOR") IsNot Nothing Then

            If TipoSetor Is Nothing Then
                TipoSetor = New ContractoServicio.TipoSetor.GetTiposSectores.TipoSetor
            End If

            TipoSetor.CodigosAjenos = Session("objRespuestaGEPR_TTIPO_SECTOR")
            Session.Remove("objRespuestaGEPR_TTIPO_SECTOR")

            Dim iCodigoAjeno = (From item In TipoSetor.CodigosAjenos
                                Where item.BolDefecto = True).SingleOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDescricaoCodigoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            If TipoSetor.CodigosAjenos IsNot Nothing Then
                CodigosAjenosPeticion = TipoSetor.CodigosAjenos
            Else
                CodigosAjenosPeticion = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
            End If
        End If
    End Sub

#End Region

#Region "[EVENTOS]"

    ''' <summary>
    '''  Evento do botão gravar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Criado
    ''' </history> 
    Private Sub btnGrabar_Click(sender As Object, e As System.EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()
    End Sub

    ''' <summary>
    '''  Evento do botão cancelar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Criado
    ''' </history> 
    Private Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        If Session("objRespuestaGEPR_TTIPO_SECTOR") IsNot Nothing Then
            Session.Remove("objRespuestaGEPR_TTIPO_SECTOR")
        End If
        ExecutarCancelar()
    End Sub

    ''' <summary>
    '''  Evento do botão voltar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Criado
    ''' </history> 
    Private Sub btnVolver_Click(sender As Object, e As System.EventArgs) Handles btnVolver.Click
        If Session("objRespuestaGEPR_TTIPO_SECTOR") IsNot Nothing Then
            Session.Remove("objRespuestaGEPR_TTIPO_SECTOR")
        End If
        ExecutarVolver()
    End Sub

    ''' <summary>
    '''  Metodo verifica se o codigo ja existe.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Criado
    ''' </history> 
    Protected Sub txtCodigoTipoSetor_TextChanged(sender As Object, e As EventArgs) Handles txtCodigoTipoSetor.TextChanged

        If (String.IsNullOrEmpty(txtCodigoTipoSetor.Text)) Then
            Exit Sub
        End If

        Try
            If ExisteCodigoTipoSetor(txtCodigoTipoSetor.Text) Then
                CodigoExistente = True
            Else
                CodigoExistente = False
            End If
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    Protected Sub imbAdicionarTodasCaracteristicas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbAdicionarTodasCaracteristicas.Click
        Try
            Dim objListItem As ListItem
            While lstCaracteristicasDisponiveis.Items.Count > 0
                objListItem = lstCaracteristicasDisponiveis.Items(0)
                lstCaracteristicasDisponiveis.Items.Remove(objListItem)
                lstCaracteristicasSelecionadas.Items.Add(objListItem)
            End While
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    Protected Sub imbAdicionarCaracteristicasSelecionadas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbAdicionarCaracteristicasSelecionadas.Click
        Try
            While lstCaracteristicasDisponiveis.SelectedItem IsNot Nothing
                Dim objListItem As ListItem
                objListItem = lstCaracteristicasDisponiveis.SelectedItem
                lstCaracteristicasDisponiveis.Items.Remove(lstCaracteristicasDisponiveis.SelectedItem)
                lstCaracteristicasSelecionadas.Items.Add(objListItem)
            End While
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    Protected Sub imbRemoverCaracteristicasSelecionadas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbRemoverCaracteristicasSelecionadas.Click
        Try
            While lstCaracteristicasSelecionadas.SelectedItem IsNot Nothing
                Dim objListItem As ListItem
                objListItem = lstCaracteristicasSelecionadas.SelectedItem
                lstCaracteristicasSelecionadas.Items.Remove(lstCaracteristicasSelecionadas.SelectedItem)
                lstCaracteristicasDisponiveis.Items.Add(objListItem)
            End While
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    Protected Sub imbRemoverTodasCaracteristicas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbRemoverTodasCaracteristicas.Click
        Try
            Dim objListItem As ListItem
            While lstCaracteristicasSelecionadas.Items.Count > 0
                objListItem = lstCaracteristicasSelecionadas.Items(0)
                lstCaracteristicasSelecionadas.Items.Remove(objListItem)
                lstCaracteristicasDisponiveis.Items.Add(objListItem)
            End While
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    Protected Sub btnAlta_Click(sender As Object, e As EventArgs) Handles btnAlta.Click
        Dim url As String = String.Empty
        Dim tablaGenesis As New MantenimientoCodigosAjenos.CodigoAjenoSimples

        tablaGenesis.CodTablaGenesis = txtCodigoTipoSetor.Text
        tablaGenesis.DesTablaGenesis = txtDescricaoTiposSetor.Text
        tablaGenesis.OidTablaGenesis = OidTipoSetor
        If TipoSetor IsNot Nothing AndAlso TipoSetor.CodigosAjenos IsNot Nothing Then
            tablaGenesis.CodigosAjenos = TipoSetor.CodigosAjenos
        End If

        Session("objGEPR_TTIPO_SECTOR") = tablaGenesis

        If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TTIPO_SECTOR"
        Else
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TTIPO_SECTOR"
        End If

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 900,'btnAlta');", True)

    End Sub

#Region "[METODOS]"

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
    ''' [pgoncalves] 04/03/2013 Criado
    ''' </history> 
    Public Sub ControleBotoes()
        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta
                'Estado Inicial Controles                                
                btnGrabar.Visible = True
                btnCancelar.Visible = True
                txtCodigoTipoSetor.Enabled = True
                btnVolver.Visible = False
                chkVigente.Checked = True
                chkVigente.Visible = False
                lblVigente.Visible = False
                lstCaracteristicasDisponiveis.Visible = True
                imbAdicionarCaracteristicasSelecionadas.Visible = True
                imbAdicionarTodasCaracteristicas.Visible = True
                imbRemoverCaracteristicasSelecionadas.Visible = True
                imbRemoverTodasCaracteristicas.Visible = True

                lstCaracteristicasSelecionadas.Enabled = True
                btnGrabar.Habilitado = True

            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                'Estado Inicial Controles
                btnCancelar.Visible = False
                btnVolver.Visible = True
                txtCodigoTipoSetor.Enabled = False
                txtDescricaoTiposSetor.Enabled = False
                btnGrabar.Visible = False
                chkVigente.Enabled = False

                lstCaracteristicasDisponiveis.Visible = False
                imbAdicionarCaracteristicasSelecionadas.Visible = False
                imbAdicionarTodasCaracteristicas.Visible = False
                imbRemoverCaracteristicasSelecionadas.Visible = False
                imbRemoverTodasCaracteristicas.Visible = False

                lstCaracteristicasSelecionadas.Enabled = False

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                txtCodigoTipoSetor.Enabled = False
                btnGrabar.Visible = True               '5
                btnCancelar.Visible = True             '6
                btnVolver.Visible = False              '7
                btnGrabar.Habilitado = True

                lstCaracteristicasDisponiveis.Visible = True
                imbAdicionarCaracteristicasSelecionadas.Visible = True
                imbAdicionarTodasCaracteristicas.Visible = True
                imbRemoverCaracteristicasSelecionadas.Visible = True
                imbRemoverTodasCaracteristicas.Visible = True

                lstCaracteristicasSelecionadas.Enabled = True

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
        Dim MensagemErro As String = MontaMensagensErro()

        If MensagemErro <> String.Empty Then
            Master.ControleErro.ShowError(MensagemErro, False)
        End If

    End Sub

#End Region

End Class