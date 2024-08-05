Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comunicacion.ProxyWS.IAC

''' <summary>
''' Página de Gerenciamento de Canais 
''' </summary>
''' <remarks></remarks>
''' <history>[PDA] 27/01/08 - Criado</history>
Partial Public Class MantenimientoPuestos
    Inherits Base


#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

        'Seta o foco para o proximo controle quando presciona o enter.

        txtCodigoPuesto.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtHostPuesto.ClientID & "').focus();return false;}} else {return true}; ")
        If Me.Acao <> Aplicacao.Util.Utilidad.eAcao.Alta Then
            txtHostPuesto.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & chkVigente.ClientID & "').focus();return false;}} else {return true}; ")
        End If

        'Controle de precedência(Ajax)
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)

        btnCancelar.FuncaoJavascript = "if(VerificarConfirmacaoCanelamento('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgSairPagina) & "')) window.location = 'BusquedaPuestos.aspx'; else return false; "


    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        ddlAplicacion.TabIndex = 1
        txtCodigoPuesto.TabIndex = 2
        txtHostPuesto.TabIndex = 3
        chkVigente.TabIndex = 4
        btnGrabar.TabIndex = 5
        btnCancelar.TabIndex = 6

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.PUESTOS

    End Sub

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then



                Dim strCodPuesto As String = Request.QueryString("codpuesto")
                Dim strCodHostPuesto As String = Request.QueryString("codhostpuesto")
                Dim strCodAplicacion As String = Request.QueryString("codaplicacion")

                'Possíveis Ações passadas pela página BusquedaPuestos:
                ' [-] Aplicacao.Util.Utilidad.eAcao.Alta
                ' [-] Aplicacao.Util.Utilidad.eAcao.Modificacion
                ' [-] Aplicacao.Util.Utilidad.eAcao.Duplicar



                If Not (MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar) Then

                    Acao = Aplicacao.Util.Utilidad.eAcao.Erro

                    Throw New Exception(Traduzir("err_passagem_parametro"))

                End If

                PreencherComboAplicacion()
                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                    txtDelegacion.Text = DelegacionConectada(DelegacionConectada.Keys(0))
                    txtDelegacion.ToolTip = DelegacionConectada(DelegacionConectada.Keys(0))
                End If

                If Not String.IsNullOrEmpty(strCodPuesto) AndAlso Not String.IsNullOrEmpty(strCodHostPuesto) AndAlso Not String.IsNullOrEmpty(strCodAplicacion) Then
                    CarregaDados(strCodAplicacion, strCodHostPuesto, strCodPuesto)
                End If

                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar Then
                    txtCodigoPuesto.Text = ""
                    txtHostPuesto.Text = ""
                    ddlAplicacion.Enabled = False
                    txtCodigoPuesto.Focus()
                End If

                If Not MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar Then
                    ddlAplicacion.Focus()
                End If

            End If

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

        Master.TituloPagina = Traduzir("025_titulo_mantenimiento_puestos")
        lblDelegacion.Text = Traduzir("025_lbl_man_delegacion")
        lblAplicacion.Text = Traduzir("025_lbl_man_aplicacion")
        lblCodigoPuesto.Text = Traduzir("025_lbl_man_codigopuesto")
        lblHostPuesto.Text = Traduzir("025_lbl_man_hostpuesto")
        lblVigente.Text = Traduzir("001_lbl_vigente")
        lblTituloPuesto.Text = Traduzir("025_titulo_mantenimiento_puestos")
        rfvAplicacion.ErrorMessage = Traduzir("025_msg_aplicacionobligatorio")
        rfvCodigoPuesto.ErrorMessage = Traduzir("025_msg_codigopuestoobligatorio")
        rfvhostPuesto.ErrorMessage = Traduzir("025_msg_hostpuestoobligatorio")
    End Sub

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Clique do botão Cancelar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click

        ExecutarCancelar()

    End Sub

    ''' <summary>
    ''' Clique do botão Grabar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        ExecutarGravar()
    End Sub

    'Ações que podem ser chamadas a qualquer momento
#Region "Ações Botões Independentes"

    ''' <summary>
    ''' Função do Botão Grabar 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarGravar()

        Try
            Dim erroMensage As String = MontaMensagensErro(True)
            If Not String.IsNullOrEmpty(erroMensage) Then
                Master.ControleErro.ShowError(erroMensage, False)
                Exit Sub
            End If

            Dim objProxyPuesto As New ProxyPuesto
            Dim objRespuestaPuesto As IAC.ContractoServicio.Puesto.SetPuesto.Respuesta
            Dim strErro As New Text.StringBuilder(String.Empty)

            Dim objPeticion As New IAC.ContractoServicio.Puesto.SetPuesto.Peticion
            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar Then
                objPeticion.PuestoVigente = True
                objPeticion.Accion = ContractoServicio.Enumeradores.Accion.Alta
            Else
                objPeticion.PuestoVigente = chkVigente.Checked
                objPeticion.Accion = ContractoServicio.Enumeradores.Accion.Modificacion
            End If

            objPeticion.CodigoAplicacion = ddlAplicacion.SelectedValue
            objPeticion.CodigoDelegacion = DelegacionConectada.Keys(0)
            objPeticion.CodigoHostPuesto = Trim(txtHostPuesto.Text)
            objPeticion.CodigoPuesto = Trim(txtCodigoPuesto.Text)
            objPeticion.CodigoUsuario = MyBase.LoginUsuario


            objRespuestaPuesto = objProxyPuesto.SetPuesto(objPeticion)

            If Master.ControleErro.VerificaErro(objRespuestaPuesto.CodigoError, objRespuestaPuesto.NombreServidorBD, objRespuestaPuesto.MensajeError) Then
                If Me.Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar Then
                    Dim objRespuestaParametro As ContractoServicio.Parametro.SetParametrosValues.Respuesta = DuplicarParametrosValues(objPeticion)
                    If Master.ControleErro.VerificaErro(objRespuestaPuesto.CodigoError, objRespuestaPuesto.NombreServidorBD, objRespuestaPuesto.MensajeError) Then
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_puesto_salvo", "alert('" & Traduzir("025_msg_grabado_suceso") & "'); window.location = 'BusquedaPuestos.aspx';", True)

                    End If
                Else
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_puesto_salvo", "alert('" & Traduzir("025_msg_grabado_suceso") & "'); window.location = 'BusquedaPuestos.aspx';", True)
                End If
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Private Function DuplicarParametrosValues(objPeticion As ContractoServicio.Puesto.SetPuesto.Peticion) As ContractoServicio.Parametro.SetParametrosValues.Respuesta

        Dim strCodPuesto As String = Request.QueryString("codpuesto")
        Dim strCodDelegacion As String = Request.QueryString("coddelegacion")
        Dim strCodAplicacion As String = Request.QueryString("codaplicacion")

        Dim parametrosValues As ContractoServicio.Parametro.GetParametrosValues.NivelColeccion
        Dim peticion As New ContractoServicio.Parametro.SetParametrosValues.Peticion
        If Not String.IsNullOrEmpty(strCodPuesto) AndAlso Not String.IsNullOrEmpty(strCodDelegacion) AndAlso Not String.IsNullOrEmpty(strCodAplicacion) Then
            parametrosValues = getParametrosValue(strCodAplicacion, strCodDelegacion, strCodPuesto)
            If parametrosValues.Count > 0 Then
                Dim objProxy As New Comunicacion.ProxyParametro
                peticion.CodigoAplicacion = objPeticion.CodigoAplicacion
                peticion.CodigoDelegacion = objPeticion.CodigoDelegacion
                peticion.CodigoPuesto = objPeticion.CodigoPuesto
                peticion.CodigoUsuario = MyBase.LoginUsuario
                peticion.Parametros = New ContractoServicio.Parametro.SetParametrosValues.ParametroColeccion

                For Each parametroValue As ContractoServicio.Parametro.GetParametrosValues.Nivel In parametrosValues
                    If parametroValue.CodigoNivel = ContractoServicio.Parametro.TipoNivel.Puesto Then
                        For Each agrupacion In parametroValue.Agrupaciones
                            For Each parametro In agrupacion.Parametros
                                Dim parametroCopia As New ContractoServicio.Parametro.SetParametrosValues.Parametro
                                parametroCopia.CodigoParametro = parametro.CodigoParametro
                                parametroCopia.ValorParametro = parametro.ValorParametro
                                peticion.Parametros.Add(parametroCopia)
                            Next
                        Next
                    End If
                Next
                Return objProxy.SetParametrosValues(peticion)
            End If
        End If
        Return New ContractoServicio.Parametro.SetParametrosValues.Respuesta
    End Function


    ''' <summary>
    ''' Função do Botão Cancelar
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarCancelar()
        Try
            Response.Redirect("~/BusquedaPuestos.aspx", False)
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

#End Region

#End Region

    Private Sub ddlAplicacion_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAplicacion.SelectedIndexChanged

        ddlAplicacion.ToolTip = ddlAplicacion.SelectedItem.Text
        ddlAplicacion.Focus()

    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Carrega os dados do gridView quando a página é carregada pela primeira vez
    ''' </summary>
    ''' <param name="codPuesto"></param>
    ''' <remarks></remarks>
    Public Sub CarregaDados(codAplicacion As String, hostPuesto As String, codPuesto As String)

        Dim objPuesto As New IAC.ContractoServicio.Puesto.GetPuestoDetail.Puesto
        objPuesto = getParametroDetail(codAplicacion, hostPuesto, codPuesto)

        'Preenche os controles do formulario
        txtDelegacion.Text = objPuesto.DescripcionDelegacion
        txtDelegacion.ToolTip = objPuesto.DescripcionDelegacion

        ddlAplicacion.SelectedValue = objPuesto.Aplicacion.CodigoAplicacion
        ddlAplicacion.ToolTip = ddlAplicacion.SelectedItem.Text

        txtCodigoPuesto.Text = objPuesto.CodigoPuesto
        txtCodigoPuesto.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion, objPuesto.CodigoPuesto, String.Empty)

        txtHostPuesto.Text = objPuesto.CodigoHostPuesto
        chkVigente.Checked = objPuesto.PuestoVigente


    End Sub

    Private Function getParametroDetail(codAplicacion As String, hostPuesto As String, codPuesto As String) As ContractoServicio.Puesto.GetPuestoDetail.Puesto
        Dim objProxyPuesto As New ProxyPuesto
        Dim objRespuesta As New IAC.ContractoServicio.Puesto.GetPuestoDetail.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.Puesto.GetPuestoDetail.Peticion
        objPeticion.CodigoAplicacion = codAplicacion
        objPeticion.HostPuesto = hostPuesto
        objPeticion.CodigoPuesto = codPuesto

        objRespuesta = objProxyPuesto.GetPuestoDetail(objPeticion)
        Return objRespuesta.Puesto

    End Function

    Public Sub PreencherComboAplicacion()
        Try
            ddlAplicacion.DataSource = Nothing
            ddlAplicacion.DataBind()
            ' Caso exista apenas um registro a mesmo será aplicado como default
            If InformacionUsuario.Aplicaciones.Count > 1 Then
                ddlAplicacion.AppendDataBoundItems = True
                ddlAplicacion.Items.Clear()
                ddlAplicacion.Items.Add(New ListItem(Traduzir("010_ddl_selecione"), String.Empty))
            End If
            ddlAplicacion.DataTextField = "DescripcionAplicacion"
            ddlAplicacion.DataValueField = "CodigoAplicacion"
            ddlAplicacion.DataSource = InformacionUsuario.Aplicaciones.OrderBy(Function(x) x.DescripcionAplicacion)
            ddlAplicacion.DataBind()

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    Private Function getParametrosValue(codAplicacion As String, codDelegacion As String, codPuesto As String) As ContractoServicio.Parametro.GetParametrosValues.NivelColeccion

        Dim objProxy As New Comunicacion.ProxyParametro
        Dim objRespuesta As New IAC.ContractoServicio.Parametro.GetParametrosValues.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.Parametro.GetParametrosValues.Peticion
        objPeticion.CodigoAplicacion = codAplicacion
        objPeticion.CodigoDelegacion = codDelegacion
        objPeticion.CodigoPuesto = codPuesto

        objRespuesta = objProxy.GetParametrosValues(objPeticion)
        If objRespuesta.CodigoError <> 0 Then
            Return New ContractoServicio.Parametro.GetParametrosValues.NivelColeccion
        End If

        Return objRespuesta.Niveles

    End Function
#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()

        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta
                btnGrabar.Visible = True        '2
                btnCancelar.Visible = True      '3
                chkVigente.Enabled = False
                chkVigente.Checked = True
            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                btnGrabar.Visible = True        '2
                btnCancelar.Visible = True      '3
                chkVigente.Enabled = True
                txtCodigoPuesto.Enabled = False
                ddlAplicacion.Enabled = False
            Case Aplicacao.Util.Utilidad.eAcao.NoAction
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Busca
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.NoAction
            Case Aplicacao.Util.Utilidad.eAcao.Erro
                btnGrabar.Visible = False
                btnCancelar.Visible = True
            Case Aplicacao.Util.Utilidad.eAcao.Duplicar

        End Select

    End Sub

    Private Enum eAcaoEspecifica As Integer
        AltaConRegistro = 20
    End Enum

    ''' <summary>
    ''' MontaMensagensErro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New StringBuilder
        Me.Validate("Gravar")
        For Each validator As IValidator In Me.Validators
            If validator IsNot Nothing AndAlso Not validator.IsValid Then
                strErro.Append(validator.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
            End If
        Next

        'Dim strErro As New Text.StringBuilder(String.Empty)
        'Dim focoSetado As Boolean = False


        'If Page.IsPostBack Then

        '    'Verifica se o campo é obrigatório
        '    'quando o botão salvar é acionado
        '    If ValidarCamposObrigatorios Then

        '        'Verifica se o código do canal é obrigatório
        '        If txtCodigoCanal.Text.Trim.Equals(String.Empty) Then

        '            strErro.Append(csvCodigoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
        '            csvCodigoObrigatorio.IsValid = False

        '            'Seta o foco no primeiro controle que deu erro
        '            If SetarFocoControle AndAlso Not focoSetado Then
        '                txtCodigoCanal.Focus()
        '                focoSetado = True
        '            End If

        '        Else
        '            csvCodigoObrigatorio.IsValid = True
        '        End If

        '        'Verifica se a descrição do canal é obrigatório
        '        If txtDescricaoCanal.Text.Trim.Equals(String.Empty) Then

        '            strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
        '            csvDescricaoObrigatorio.IsValid = False

        '            'Seta o foco no primeiro controle que deu erro
        '            If SetarFocoControle AndAlso Not focoSetado Then
        '                txtDescricaoCanal.Focus()
        '                focoSetado = True
        '            End If

        '        Else
        '            csvDescricaoObrigatorio.IsValid = True
        '        End If


        '    End If

        '    'Validações constantes durante o ciclo de vida de execução da página

        '    'Verifica se o código existe
        '    If CodigoExistente Then

        '        strErro.Append(csvCodigoCanalExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
        '        csvCodigoCanalExistente.IsValid = False

        '        'Seta o foco no primeiro controle que deu erro
        '        If SetarFocoControle AndAlso Not focoSetado Then
        '            txtCodigoCanal.Focus()
        '            focoSetado = True
        '        End If

        '    Else
        '        csvCodigoCanalExistente.IsValid = True
        '    End If

        '    'Verifica se a descrição existe
        '    If DescricaoExistente Then

        '        strErro.Append(csvDescripcionExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
        '        csvDescripcionExistente.IsValid = False

        '        'Seta o foco no primeiro controle que deu erro
        '        If SetarFocoControle AndAlso Not focoSetado Then
        '            txtDescricaoCanal.Focus()
        '            focoSetado = True
        '        End If

        '    Else
        '        csvDescripcionExistente.IsValid = True
        '    End If

        'End If

        Return strErro.ToString

    End Function


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
#End Region


End Class