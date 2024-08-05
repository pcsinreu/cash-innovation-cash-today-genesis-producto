Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comunicacion.ProxyWS.IAC

''' <summary>
''' Página de Mantenimiento de Delegaciones 
''' </summary>
''' <remarks></remarks>
''' <history>[pgoncalves] 14/02/13 - Criado</history>
Public Class MantenimientoDelegaciones
    Inherits Base

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()
        'Adicionar scripts na página
        txtCodigoDelegacion.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoDelegaciones.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoDelegaciones.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtGmtMinutos.ClientID & "').focus();return false;}} else {return true}; ")
        txtCodigoDelegacion.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtCodigoDelegacion.ClientID & "','" & txtCodigoDelegacion.MaxLength & "');")
        txtDescricaoDelegaciones.Attributes.Add("onblur", "limitaCaracteres('" & txtDescricaoDelegaciones.ClientID & "','" & txtDescricaoDelegaciones.MaxLength & "');")
        txtGmtMinutos.Attributes.Add("onkeyup", "limitaCaracteres('" & txtGmtMinutos.ClientID & "','" & txtGmtMinutos.MaxLength & "');")
        txtFechaVeranoInicio.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtFechaVeranoInicio.ClientID & "','" & txtFechaVeranoInicio.MaxLength & "');")
        txtFechaVeranoFim.Attributes.Add("onblur", "limitaCaracteres('" & txtFechaVeranoFim.ClientID & "','" & txtFechaVeranoFim.MaxLength & "');")
        txtCantidadMinAjuste.Attributes.Add("onkeyup", "limitaCaracteres('" & txtCantidadMinAjuste.ClientID & "','" & txtCantidadMinAjuste.MaxLength & "');")

        txtCantidadMinAjuste.Attributes.Add("onKeyDown", "BloquearColar();")
        txtGmtMinutos.Attributes.Add("onKeyDown", "BloquearColar();")

        txtZona.Attributes.Add("onkeyup", "limitaCaracteres('" & txtZona.ClientID & "','" & txtZona.MaxLength & "');")
        ddlPais.Attributes.Add("onKeyDown", "BloquearColar();")

        txtFechaVeranoFim.Attributes.Add("onkeyup", "javascript:return ApenasData(this);")
        txtFechaVeranoFim.Attributes.Add("onblur", "javascript:validarData(this, 'dd/MM/yyyy', '" & Traduzir("gen_data_invalida") & "');")

        txtFechaVeranoInicio.Attributes.Add("onkeyup", "javascript:return ApenasData(this);")
        txtFechaVeranoInicio.Attributes.Add("onblur", "javascript:validarData(this, 'dd/MM/yyyy', '" & Traduzir("gen_data_invalida") & "');")

        txtGmtMinutos.Attributes.Add("onkeypress", "return ValorNumericoGmt(event);")
        txtCantidadMinAjuste.Attributes.Add("onkeypress", "return ValorNumericoGmt(event);")

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)
    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        ddlPais.TabIndex = 1
        txtCodigoDelegacion.TabIndex = 2
        btnAltaAjeno.TabIndex = 3
        txtCodigoAjeno.TabIndex = 5
        txtDesCodigoAjeno.TabIndex = 6
        txtDescricaoDelegaciones.TabIndex = 3
        txtGmtMinutos.TabIndex = 4
        txtFechaVeranoInicio.TabIndex = 5
        txtFechaVeranoFim.TabIndex = 6
        txtCantidadMinAjuste.TabIndex = 7
        txtZona.TabIndex = 8
        chkVigente.TabIndex = 9
        btnGrabar.TabIndex = 10
        btnCancelar.TabIndex = 11
        btnVolver.TabIndex = 12

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.DELEGACION

    End Sub

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                'Recebe o código do Termino
                Dim strCodDelegacion As String = Request.QueryString("codDelegaciones")

                'Possíveis Ações passadas pela página BusquedaDelegaciones:
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

                'Preenche Combos
                PreencherComboDelegacione()

                If strCodDelegacion <> String.Empty Then
                    'Estado Inicial dos control
                    CarregaDados(strCodDelegacion)
                End If



                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion _
                    OrElse MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    ddlPais.Focus()
                Else
                    ddlPais.Focus()
                    chkVigente.Checked = True
                End If

            End If

            'Trata o foco dos campos
            TrataFoco()
            ConsomeCodigoAjeno()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Protected Overrides Sub PreRenderizar()
        Try

            If Not Page.IsPostBack Then
                ControleBotoes()
            End If

            MostrarMensagem()
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    Protected Overrides Sub TraduzirControles()

        Master.TituloPagina = Traduzir("013_titulo_mantenimiento_Delegaciones")
        lblCantidadMinAjuste.Text = Traduzir("013_lbl_cantidadAjuste")
        lblCodigoDelegacion.Text = Traduzir("013_lbl_codigoDelegacion")
        lblDescricaoDelegaciones.Text = Traduzir("013_lbl_descDelegacion")
        lblFechaVeranoFim.Text = Traduzir("013_lbl_FechaVeranoFim")
        lblFechaVeranoInicio.Text = Traduzir("013_lbl_FechaVeranoInicio")
        lblGmtMinutos.Text = Traduzir("013_lbl_gmtMinutos")
        lblPais.Text = Traduzir("013_lbl_pais")
        lblTituloDelegacione.Text = Traduzir("013_lbl_TituloDelegaciones")
        lblZona.Text = Traduzir("013_lbl_zona")
        lblVigente.Text = Traduzir("013_lbl_vigente")
        lblCodigoAjeno.Text = Traduzir("037_lbl_CodigoAjeno")
        lblDesCodigoAjeno.Text = Traduzir("037_lbl_DesCodigoAjeno")


        csvCodigoObrigatorio.ErrorMessage = Traduzir("013_msg_Delegacioncodigoobligatorio")
        csvPais.ErrorMessage = Traduzir("013_msg_PaisObligatorio")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("013_msg_Delegaciondescripcionobligatorio")
        csvFechaVeranoFim.ErrorMessage = Traduzir("013_msg_FechaVeranoFin")
        csvFechaVeranoInicio.ErrorMessage = Traduzir("013_msg_FechaVeranoIncio")
        csvGmtMinutosObrigatorio.ErrorMessage = Traduzir("013_msg_DelegacionGmtMinutos")
        csvZona.ErrorMessage = Traduzir("013_msg_delegacionZona")
        csvCantiAjuste.ErrorMessage = Traduzir("013_msg_Delegacion")
        csvCodigoExistente.ErrorMessage = Traduzir("013_msg_Existente")
        csvFechaVeranoInicioInvalida.ErrorMessage = Traduzir("013_msg_erro_Data")
    End Sub

#End Region

#Region "[EVENTOS]"

    ''' <summary>
    ''' Clique do botão Volver
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        ExecutarVolver()
    End Sub

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
        ExecutarGrabar()
    End Sub

    Protected Sub ddlPais_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPais.SelectedIndexChanged

        If (ddlPais.SelectedValue <> Nothing) Then
            OidPais = ddlPais.SelectedValue
            ddlPais.ToolTip = ddlPais.SelectedItem.Text
        Else
            OidPais = String.Empty
            Exit Sub
        End If

        If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
            If ddlPais.SelectedValue <> Nothing Then
                txtCodigoDelegacion.Enabled = True
                chkVigente.Enabled = True
                txtCantidadMinAjuste.Enabled = True
                txtDescricaoDelegaciones.Enabled = True
                txtFechaVeranoFim.Enabled = True
                txtFechaVeranoInicio.Enabled = True
                txtGmtMinutos.Enabled = True
                txtZona.Enabled = True
            Else
                txtCodigoDelegacion.Enabled = False
                chkVigente.Enabled = False
                txtCantidadMinAjuste.Enabled = False
                txtDescricaoDelegaciones.Enabled = False
                txtFechaVeranoFim.Enabled = False
                txtFechaVeranoInicio.Enabled = False
                txtGmtMinutos.Enabled = False
                txtZona.Enabled = False
            End If
        Else
            txtCodigoDelegacion.Enabled = False
        End If

    End Sub

    Protected Sub txtCodigoDelegacion_TextChanged(sender As Object, e As EventArgs) Handles txtCodigoDelegacion.TextChanged

        If (String.IsNullOrEmpty(txtCodigoDelegacion.Text)) Then
            Exit Sub
        End If

        If (OidPais.Equals(String.Empty)) Then
            Exit Sub
        End If

        Try
            If ExisteCodigoDelegacion(txtCodigoDelegacion.Text, ddlPais.SelectedValue) Then
                CodigoExistente = True
            Else
                CodigoExistente = False
            End If
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    Private Sub btnAltaAjeno_Click(sender As Object, e As System.EventArgs) Handles btnAltaAjeno.Click
        Dim url As String = String.Empty
        Dim tablaGenesis As New MantenimientoCodigosAjenos.CodigoAjenoSimples

        tablaGenesis.CodTablaGenesis = txtCodigoDelegacion.Text
        tablaGenesis.DesTablaGenesis = txtDescricaoDelegaciones.Text
        tablaGenesis.OidTablaGenesis = OidDelegacion
        If Delegacion IsNot Nothing AndAlso Delegacion.Count > 0 AndAlso Delegacion.FirstOrDefault IsNot Nothing AndAlso Delegacion.FirstOrDefault.CodigosAjenos IsNot Nothing Then
            tablaGenesis.CodigosAjenos = Delegacion.FirstOrDefault.CodigosAjenos
        End If

        Session("objPeticionGEPR_TDELEGACION") = tablaGenesis.CodigosAjenos
        Session("objGEPR_TDELEGACION") = tablaGenesis

        If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TDELEGACION"
        Else
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TDELEGACION"
        End If

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 900,'btnAltaAjeno');", True)
    End Sub


#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Função do botão grabar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 14/02/2013 - Criado
    ''' </history>
    Public Sub ExecutarGrabar()
        Try
            Dim objPeticion As New ContractoServicio.Pais.GetPaisDetail.Peticion
            Dim objRespuesta As New ContractoServicio.Pais.GetPaisDetail.Respuesta
            Dim objProxyPais As New Comunicacion.ProxyPaises

            Dim objProxyDelegacion As New ProxyDelegacion
            Dim objRespuestaDelegacion As IAC.ContractoServicio.Delegacion.SetDelegacion.Respuesta
            Dim strErro As New Text.StringBuilder(String.Empty)

            ValidarCamposObrigatorios = True

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If

            Dim objDelegacionPeticion As New IAC.ContractoServicio.Delegacion.SetDelegacion.Peticion
            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objDelegacionPeticion.BolVigente = True
            Else
                objDelegacionPeticion.BolVigente = chkVigente.Checked
            End If

            ' atualizar propriedade vigente
            EsVigente = chkVigente.Checked

            objDelegacionPeticion.OidPais = ddlPais.SelectedValue
            objDelegacionPeticion.CodDelegacion = txtCodigoDelegacion.Text.Trim
            objDelegacionPeticion.CodigoAjeno = CodigosAjenosPeticion
            objDelegacionPeticion.DesDelegacion = txtDescricaoDelegaciones.Text
            objDelegacionPeticion.NecGmtMinutos = txtGmtMinutos.Text
            objDelegacionPeticion.FyhVeranoInicio = Convert.ToDateTime(txtFechaVeranoInicio.Text)
            objDelegacionPeticion.FyhVeranoFin = Convert.ToDateTime(txtFechaVeranoFim.Text)
            objDelegacionPeticion.NecVeranoAjuste = txtCantidadMinAjuste.Text
            objDelegacionPeticion.DesZona = txtZona.Text
            If String.IsNullOrEmpty(CodigoPais) Then
                objPeticion.CodigoPais = OidPais
                objRespuesta = objProxyPais.GetPaisDetail(objPeticion)
                objDelegacionPeticion.CodPais = objRespuesta.Pais(0).CodPais
            Else
                objDelegacionPeticion.CodPais = CodigoPais
            End If

            objDelegacionPeticion.OidDelegacion = OidDelegacion
            objDelegacionPeticion.OidPais = OidPais

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objDelegacionPeticion.GmtCreacion = DateTime.Now
                objDelegacionPeticion.DesUsuarioCreacion = MyBase.LoginUsuario
            Else
                objDelegacionPeticion.GmtCreacion = Delegacion(0).GmtCreation
                objDelegacionPeticion.DesUsuarioCreacion = Delegacion(0).Des_Usuario_Create
            End If

            objDelegacionPeticion.GmtModificacion = DateTime.Now
            objDelegacionPeticion.DesUsuarioModificacion = MyBase.LoginUsuario

            objRespuestaDelegacion = objProxyDelegacion.SetDelegaciones(objDelegacionPeticion)

            Dim url As String = "BusquedaDelegaciones.aspx"

            If Master.ControleErro.VerificaErro(objRespuestaDelegacion.CodigoError, objRespuestaDelegacion.NombreServidorBD, objRespuestaDelegacion.MensajeError) Then
                'Registro gravado com sucesso
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_grabado_suceso", "alert('" & Traduzir("001_msg_grabado_suceso") & _
                                                                                    "'); RedirecionaPaginaNormal('" & url & "');", True)
            Else
                If objRespuestaDelegacion.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    Master.ControleErro.ShowError(objRespuestaDelegacion.MensajeError, False)
                End If
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
    ''' [pgoncalves] 14/05/2013 - Criado
    ''' </history>
    Public Sub ExecutarCancelar()
        Response.Redirect("~/BusquedaDelegaciones.aspx", False)
    End Sub

    ''' <summary>
    ''' Função do botão volver.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 14/02/2013 - Criado
    ''' </history>
    Public Sub ExecutarVolver()
        Response.Redirect("~/BusquedaDelegaciones.aspx", False)
    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 14/02/2013 Criado
    ''' </history>
    Public Sub PreencherComboDelegacione()

        Dim objProxyDelegacione As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboPais.Respuesta
        objRespuesta = objProxyDelegacione.GetComboPais

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ddlPais.AppendDataBoundItems = True
        ddlPais.Items.Clear()
        ddlPais.Items.Add(New ListItem(Traduzir("013_ddl_selecione"), String.Empty))
        ddlPais.DataTextField = "Description"
        ddlPais.DataValueField = "OidPais"
        ddlPais.DataSource = objRespuesta.Pais.OrderBy(Function(b) b.Description)

        ddlPais.DataBind()
    End Sub

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
    ''' Carrega os dados do gridView quando a página é carregada pela primeira vez
    ''' </summary>
    ''' <param name="codDelegacione"></param>
    ''' <remarks></remarks>
    Public Sub CarregaDados(codDelegacione As String)

        Dim objDelegacion As IAC.ContractoServicio.Delegacion.DelegacionColeccion
        Dim itemSelecionado As ListItem

        objDelegacion = getDelegacioneDetail(codDelegacione)

        If objDelegacion.Count > 0 Then

            'Coloca na VState
            Delegacion = objDelegacion


            'Preenche os controles do formulario
            txtCodigoDelegacion.Text = objDelegacion(0).CodDelegacion
            txtCodigoDelegacion.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objDelegacion(0).CodDelegacion, String.Empty)

            ' AJENO
            Dim iCodigoAjeno = (From item In objDelegacion(0).CodigosAjenos
                  Where item.BolDefecto = True).FirstOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            txtDescricaoDelegaciones.Text = objDelegacion(0).Description
            txtDescricaoDelegaciones.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objDelegacion(0).Description, String.Empty)

            If objDelegacion(0).Vigente Then
                chkVigente.Enabled = False
            Else
                chkVigente.Enabled = True
            End If

            chkVigente.Checked = objDelegacion(0).Vigente

            txtFechaVeranoFim.Text = objDelegacion(0).FhyVeraoFim.ToString("dd/MM/yyyy")
            txtFechaVeranoFim.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objDelegacion(0).FhyVeraoFim.ToString("dd/MM/yyyy"), String.Empty)

            txtFechaVeranoInicio.Text = objDelegacion(0).FhyVeraoInicio.ToString("dd/MM/yyyy")
            txtFechaVeranoInicio.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objDelegacion(0).FhyVeraoInicio.ToString("dd/MM/yyyy"), String.Empty)

            txtGmtMinutos.Text = objDelegacion(0).NecGmTminutes.ToString()
            txtGmtMinutos.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objDelegacion(0).NecGmTminutes.ToString(), String.Empty)

            txtCantidadMinAjuste.Text = objDelegacion(0).NecVeraoAjuste.ToString()
            txtGmtMinutos.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objDelegacion(0).NecGmTminutes.ToString(), String.Empty)

            txtZona.Text = objDelegacion(0).Zona
            txtZona.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objDelegacion(0).Zona, String.Empty)

            ddlPais.SelectedValue = objDelegacion(0).OidPais

            EsVigente = objDelegacion(0).Vigente
            OidDelegacion = objDelegacion(0).OidDelegacion
            OidPais = objDelegacion(0).OidPais
            CodigoPais = objDelegacion(0).CodPais

            'Seleciona o valor
            itemSelecionado = ddlPais.Items.FindByValue(objDelegacion(0).OidPais)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
            End If

        Else
            Response.Redirect("~/BusquedaDelegaciones.aspx", False)
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
        Dim data1, data2 As DateTime

        If Page.IsPostBack Then

            'Verifica se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Verifica se o país foi enviado
                If ddlPais.Visible AndAlso ddlPais.SelectedValue.Equals(String.Empty) Then

                    strErro.Append(csvPais.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvPais.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlPais.Focus()
                        focoSetado = True
                    End If
                Else
                    csvPais.IsValid = True
                End If

                'Verifica se o código da delegação foi enviado
                If txtCodigoDelegacion.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoDelegacion.Focus()
                        focoSetado = True
                    End If
                Else
                    csvCodigoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição foi enviada
                If txtDescricaoDelegaciones.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False
                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoDelegaciones.Focus()
                        focoSetado = True
                    End If
                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

                'Verifica se os minutos para ajustes de horario de verão foi enviada 
                If txtGmtMinutos.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvGmtMinutosObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvGmtMinutosObrigatorio.IsValid = False

                    'Setar o foco no primeiro que controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtGmtMinutos.Focus()
                        focoSetado = True
                    End If
                Else
                    csvGmtMinutosObrigatorio.IsValid = True
                End If

                'Verifica se a data de inicio do horario do verão foi enviado
                If txtFechaVeranoInicio.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvFechaVeranoInicio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvFechaVeranoInicio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtFechaVeranoInicio.Focus()
                        focoSetado = True
                    End If
                Else
                    csvFechaVeranoInicio.IsValid = True
                End If

                'Verifica se a data de termino do horario de verão foi enviada
                If txtFechaVeranoFim.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvFechaVeranoFim.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvFechaVeranoFim.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtFechaVeranoFim.Focus()
                        focoSetado = True
                    End If
                Else
                    csvFechaVeranoFim.IsValid = True
                End If

                'Verifica se os minutos de ajustes foram enviados
                If txtCantidadMinAjuste.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCantiAjuste.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCantiAjuste.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCantidadMinAjuste.Focus()
                        focoSetado = True
                    End If
                Else
                    csvCantiAjuste.IsValid = True
                End If

                'Verifica se a zona foi enviada
                If txtZona.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvZona.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvZona.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtZona.Focus()
                        focoSetado = True
                    End If
                Else
                    csvZona.IsValid = True
                End If

                If Not String.IsNullOrEmpty(txtFechaVeranoFim.Text) _
                     AndAlso Not String.IsNullOrEmpty(txtFechaVeranoInicio.Text) Then

                    data1 = CType(txtFechaVeranoInicio.Text, DateTime)
                    data2 = CType(txtFechaVeranoFim.Text, DateTime)

                    If (data1 > data2) Then
                        strErro.Append(csvFechaVeranoInicioInvalida.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                        'strErro.AppendLine(Traduzir("013_msg_erro_Data"))
                        csvFechaVeranoInicioInvalida.IsValid = False

                        'Seta o foco no primeiro controle que deu erro
                        If SetarFocoControle AndAlso Not focoSetado Then
                            txtFechaVeranoFim.Focus()
                            focoSetado = True
                        End If
                    Else
                        csvFechaVeranoInicioInvalida.IsValid = True
                    End If
                End If
            End If

            'Verifica se o código existe
            If CodigoExistente Then

                strErro.Append(csvCodigoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoDelegacion.Focus()
                    focoSetado = True
                End If
            Else
                csvCodigoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' Informa se o código da delegacion já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 23/04/2013 - Criado
    ''' </history>
    Private Function ExisteCodigoDelegacion(codigo As String, Pais As String) As Boolean

        Try

            Dim objProxy As New ProxyDelegacion
            Dim objPeticion As New IAC.ContractoServicio.Delegacion.GetDelegacion.Peticion
            Dim objRespuesta As IAC.ContractoServicio.Delegacion.GetDelegacion.Respuesta

            'Verifica se o código do Termino existe no BD
            objPeticion.CodDelegacion = codigo
            objPeticion.OidPais = Pais
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False

            objRespuesta = objProxy.GetDelegaciones(objPeticion)

            If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                If objRespuesta.Delegacion.Count > 0 Then
                    Return True
                End If
            Else
                Master.ControleErro.ShowError(objRespuesta.MensajeError)
                Return False
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Function

    Public Function getDelegacioneDetail(codigoDelegacione As String) As IAC.ContractoServicio.Delegacion.DelegacionColeccion

        Dim objProxyDelegacion As New ProxyDelegacion
        Dim objPeticionDelegacion As New IAC.ContractoServicio.Delegacion.GetDelegacionDetail.Peticion
        Dim objRespuestaDelegacion As IAC.ContractoServicio.Delegacion.GetDelegacionDetail.Respuesta

        If Not String.IsNullOrEmpty(codigoDelegacione) Then
            objPeticionDelegacion.CodigoDelegacione = codigoDelegacione
        End If

        objRespuestaDelegacion = objProxyDelegacion.GetDelegacioneDetail(objPeticionDelegacion)

        Return objRespuestaDelegacion.Delegacion

    End Function

    'Mostrar Mensagens de Erro caso seja postback
    Private Sub MostrarMensagem()
        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem de erro
        Dim MensagemErro As String = MontaMensagensErro()

        If MensagemErro <> String.Empty Then
            Master.ControleErro.ShowError(MensagemErro, False)
        End If
    End Sub


    Private Sub ConsomeCodigoAjeno()

        If Session("objRespuestaGEPR_TDELEGACION") IsNot Nothing Then
            Dim objDelegacionPeticion As New IAC.ContractoServicio.Delegacion.SetDelegacion.Peticion
            objDelegacionPeticion.CodigoAjeno = Session("objRespuestaGEPR_TDELEGACION")

            Session.Remove("objRespuestaGEPR_TDELEGACION")

            Dim iCodigoAjeno = (From item In objDelegacionPeticion.CodigoAjeno
                                Where item.BolDefecto = True).SingleOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            If objDelegacionPeticion.CodigoAjeno IsNot Nothing Then
                CodigosAjenosPeticion = objDelegacionPeticion.CodigoAjeno
            Else
                CodigosAjenosPeticion = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
            End If


            Session("objPeticionGEPR_TDELEGACION") = objDelegacionPeticion.CodigoAjeno

        End If

    End Sub

#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()
        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta

                btnGrabar.Visible = True        '1
                btnCancelar.Visible = True      '2

                'Estado Inicial Controles                                
                btnVolver.Visible = False       '3
                btnGrabar.Habilitado = True
                lblVigente.Visible = False
                chkVigente.Checked = True
                chkVigente.Visible = False
                txtCodigoDelegacion.Enabled = False
                txtCantidadMinAjuste.Enabled = False
                txtDescricaoDelegaciones.Enabled = False
                txtFechaVeranoFim.Enabled = False
                txtFechaVeranoInicio.Enabled = False
                txtGmtMinutos.Enabled = False
                txtZona.Enabled = False

            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta

                btnCancelar.Visible = False              '2
                btnVolver.Visible = True                 '3

                'Estado Inicial Controles
                txtCantidadMinAjuste.Enabled = False
                txtCodigoDelegacion.Enabled = False
                txtDescricaoDelegaciones.Enabled = False
                txtFechaVeranoFim.Enabled = False
                txtFechaVeranoInicio.Enabled = False
                txtGmtMinutos.Enabled = False
                txtZona.Enabled = False
                ddlPais.Enabled = False
                ddlPais.ToolTip = ddlPais.SelectedItem.Text
                chkVigente.Enabled = False
                btnGrabar.Visible = False                '1

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                txtCodigoDelegacion.Enabled = False
                btnGrabar.Visible = True               '1
                btnCancelar.Visible = True             '2
                btnVolver.Visible = False              '3
                btnGrabar.Habilitado = True

            Case Aplicacao.Util.Utilidad.eAcao.NoAction
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Busca
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.NoAction

            Case Aplicacao.Util.Utilidad.eAcao.Erro
                btnGrabar.Visible = False        '2
                btnCancelar.Visible = False      '3                
                btnVolver.Visible = True         '7

        End Select

        txtCodigoAjeno.Enabled = False
        txtDesCodigoAjeno.Enabled = False

        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem de erro
        Dim MensagemErro As String = MontaMensagensErro()

        If MensagemErro <> String.Empty Then
            Master.ControleErro.ShowError(MensagemErro, False)
        End If

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

    Private Property EsVigente() As Boolean
        Get
            Return ViewState("EsVigente")
        End Get
        Set(value As Boolean)
            ViewState("EsVigente") = value
        End Set
    End Property

    Private Property OidDelegacion() As String
        Get
            Return ViewState("OidDelegacion")
        End Get
        Set(value As String)
            ViewState("OidDelegacion") = value
        End Set
    End Property

    Private Property OidPais As String
        Get
            Return ViewState("OidPais")
        End Get
        Set(value As String)
            ViewState("OidPais") = value
        End Set
    End Property

    Private Property CodigoPais As String
        Get
            Return ViewState("CodigoPais")
        End Get
        Set(value As String)
            ViewState("CodigoPais") = value
        End Set
    End Property

    Private Property Delegacion As ContractoServicio.Delegacion.DelegacionColeccion
        Get
            Return DirectCast(ViewState("Delegacion"), ContractoServicio.Delegacion.DelegacionColeccion)
        End Get
        Set(value As ContractoServicio.Delegacion.DelegacionColeccion)
            ViewState("Delegacion") = value
        End Set
    End Property

    Private Property CodigosAjenosPeticion As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Get
            Return DirectCast(ViewState("CodigosAjenosPeticion"), ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
        End Get

        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
            ViewState("CodigosAjenosPeticion") = value
        End Set

    End Property

#End Region

End Class