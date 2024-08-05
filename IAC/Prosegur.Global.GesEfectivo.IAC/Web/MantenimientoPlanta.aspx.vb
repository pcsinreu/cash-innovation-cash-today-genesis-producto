Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comunicacion.ProxyWS.IAC

''' <summary>
''' Página de Busca de Mantenimiento de Plantas 
''' </summary>
''' <remarks></remarks>
''' <history>[pgoncalves] 20/02/13 - Criado</history>
''' 
Public Class MantenimientoPlanta_aspx
    Inherits Base

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()
        'Adicionar scripts na página
        txtCodigoPlanta.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtCodigoPlanta.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoPlanta.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoPlanta.ClientID & "').focus();return false;}} else {return true}; ")
        ddlDelegacion.Attributes.Add("onKeyDown", "BloquearColar();")

        'Controle de precedência(Ajax)
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)
    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        ddlDelegacion.TabIndex = 1
        txtCodigoPlanta.TabIndex = 2
        txtDescricaoPlanta.TabIndex = 3
        chkVigente.TabIndex = 4

        btnImporteMaximo.TabIndex = 5
        btnGrabar.TabIndex = 6
        btnCancelar.TabIndex = 7
        btnVolver.TabIndex = 8

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

                'Recebe o código do planta
                Dim strCodTermino As String = Request.QueryString("codPlanta")

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

                If strCodTermino <> String.Empty Then
                    'Estado Inicial dos control
                    CarregaDados(strCodTermino)
                End If

                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion _
                    OrElse MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    ddlDelegacion.Focus()

                Else
                    ddlDelegacion.Focus()
                    chkVigente.Checked = True
                End If

            End If

            'Trata o foco dos campos
            TrataFoco()

            'Consome o preenchimento de código ajeno
            ConsomeCodigoAjeno()
            ConsomeImporteMaximo()
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

        Master.TituloPagina = Traduzir("014_titulo_mantenimiento_Planta")
        lblCodigoPlanta.Text = Traduzir("014_lbl_codigoPlanta")
        lblDelegacion.Text = Traduzir("014_lbl_Delegacion")
        lblDescricaoPlanta.Text = Traduzir("014_lbl_DescPlanta")
        lblTituloPlanta.Text = Traduzir("014_lbl_TituloPlaneta")
        lblVigente.Text = Traduzir("014_lbl_Vigente")
        lblCodigoAjeno.Text = Traduzir("019_lbl_codigoAjeno")
        lblDesCodigoAjeno.Text = Traduzir("019_lbl_descricaoAjeno")
        csvCodigoPlanta.ErrorMessage = Traduzir("014_msg_CodigoPlanta")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("014_msg_PlantaDescripcion")
        csvDelegacion.ErrorMessage = Traduzir("014_msg_Delegacion")
        csvCodigoExistente.ErrorMessage = Traduzir("053_msg_erro_PlantaExistente")
        btnAlta.ExibirLabelBtn = False

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

    Protected Sub btnImporteMaximo_Click(sender As Object, e As EventArgs) Handles btnImporteMaximo.Click
        Dim url As String = String.Empty
        Dim oidImporteMaximo As String = ""

        If Planta IsNot Nothing AndAlso Planta.ImportesMaximos IsNot Nothing AndAlso Planta.ImportesMaximos.Count > 0 Then
            oidImporteMaximo = Planta.ImportesMaximos.First.OidImporteMaximo
            If Session("ImporteMaximoEditar") Is Nothing Then
                Session("ImporteMaximoEditar") = Planta.ImportesMaximos
            End If
        End If

        If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
            url = "MantenimientoImporteMaximo.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&codimporte=" & txtCodigoPlanta.Text & "&descImporte=" & txtDescricaoPlanta.Text & "&oidimporte=" & oidImporteMaximo
        ElseIf (Aplicacao.Util.Utilidad.eAcao.Modificacion = MyBase.Acao) Then
            url = "MantenimientoImporteMaximo.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&codimporte=" & txtCodigoPlanta.Text & "&descImporte=" & txtDescricaoPlanta.Text & "&oidimporte=" & oidImporteMaximo
        ElseIf (Aplicacao.Util.Utilidad.eAcao.Alta = MyBase.Acao) Then
            url = "MantenimientoImporteMaximo.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&codimporte=" & txtCodigoPlanta.Text & "&descImporte=" & txtDescricaoPlanta.Text
        End If
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_importeMaximo", "AbrirPopupModal('" & url & "', 550, 900,'btnImporteMaximo'); ", True)
    End Sub

    Protected Sub txtCodigoPlanta_TextChanged(sender As Object, e As EventArgs) Handles txtCodigoPlanta.TextChanged

        ValidaCodigoPlantaExiste()
    End Sub

    Private Sub btnAlta_Click(sender As Object, e As System.EventArgs) Handles btnAlta.Click
        Dim url As String = String.Empty
        Dim tablaGenesis As New MantenimientoCodigosAjenos.CodigoAjenoSimples

        tablaGenesis.CodTablaGenesis = txtCodigoPlanta.Text
        tablaGenesis.DesTablaGenesis = txtDescricaoPlanta.Text
        tablaGenesis.OidTablaGenesis = OidPlanta
        If Planta IsNot Nothing AndAlso Planta.CodigosAjenos IsNot Nothing Then
            tablaGenesis.CodigosAjenos = Planta.CodigosAjenos
        End If

        Session("objGEPR_TPLANTA") = tablaGenesis

        If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TPLANTA"
        Else
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TPLANTA"
        End If

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 900,'btnAlta');", True)
    End Sub
#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Função do botão grabar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 20/02/2013 - Criado
    ''' </history>
    Public Sub ExecutarGrabar()
        Try

            Dim objProxyPlanta As New Comunicacion.ProxyPlanta
            Dim objRespuestaPlanta As IAC.ContractoServicio.Planta.SetPlanta.Respuesta
            Dim strErro As New Text.StringBuilder(String.Empty)
            Dim objPlanta As New IAC.ContractoServicio.Planta.SetPlanta.Peticion

            ValidarCamposObrigatorios = True

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If

            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPlanta.BolActivo = True
            Else
                objPlanta.BolActivo = chkVigente.Checked
            End If

            ' atualizar propriedade vigente
            EsVigente = chkVigente.Checked

            objPlanta.CodPlanta = txtCodigoPlanta.Text
            objPlanta.DesPlanta = txtDescricaoPlanta.Text.Trim
            objPlanta.OidDelegacion = ddlDelegacion.SelectedValue
            objPlanta.OidPlanta = OidPlanta

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPlanta.GmtCreacion = DateTime.Now
                objPlanta.DesUsuarioCreacion = MyBase.LoginUsuario
            Else
                objPlanta.DesUsuarioCreacion = Planta.DesUsuarioCreacion
            End If

            objPlanta.GmtModificacion = DateTime.Now
            objPlanta.DesUsuarioModificacion = MyBase.LoginUsuario

            objPlanta.CodigosAjenos = CodigosAjenosPeticion

            objPlanta.ImporteMaximo = ImportesMaximoPeticion

            objRespuestaPlanta = objProxyPlanta.SetPlantas(objPlanta)

            Dim url As String = "BusquedaPlanta.aspx"

            Session.Remove("objRespuestaGEPR_PLANTA")
            Session.Remove("ImporteMaximoEditar")

            If Master.ControleErro.VerificaErro(objRespuestaPlanta.CodigoError, objRespuestaPlanta.NombreServidorBD, objRespuestaPlanta.MensajeError) Then
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_grabado_suceso", "alert('" & Traduzir("001_msg_grabado_suceso") & _
                                                                                    "'); RedirecionaPaginaNormal('" & url & "');", True)
            Else
                If objRespuestaPlanta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    Master.ControleErro.ShowError(objRespuestaPlanta.MensajeError, False)
                End If
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub
    Public Sub ValidaCodigoPlantaExiste()

        If (String.IsNullOrEmpty(txtCodigoPlanta.Text)) Then
            Master.ControleErro.Visible = False
            Exit Sub
        End If

        If (ddlDelegacion.SelectedValue.Equals(String.Empty)) Then
            Master.ControleErro.Visible = False
            Exit Sub
        End If

        Try
            Master.ControleErro.Visible = True
            If ExisteCodigoPlanta(txtCodigoPlanta.Text, ddlDelegacion.SelectedValue) Then
                CodigoExistente = True
            Else
                CodigoExistente = False
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
    ''' [pgoncalves] 20/05/2013 - Criado
    ''' </history>
    Public Sub ExecutarCancelar()
        If Session("objRespuestaGEPR_TPLANTA") IsNot Nothing Then
            Session.Remove("objRespuestaGEPR_TPLANTA")
        End If
        If Session("ImporteMaximoEditar") IsNot Nothing Then
            Session.Remove("ImporteMaximoEditar")
        End If
        Response.Redirect("~/BusquedaPlanta.aspx", False)
    End Sub

    ''' <summary>
    ''' Função do botão volver.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 20/02/2013 - Criado
    ''' </history>
    Public Sub ExecutarVolver()
        If Session("objRespuestaGEPR_TPLANTA") IsNot Nothing Then
            Session.Remove("objRespuestaGEPR_TPLANTA")
        End If
        If Session("ImporteMaximoEditar") IsNot Nothing Then
            Session.Remove("ImporteMaximoEditar")
        End If
        Response.Redirect("~/BusquedaPlanta.aspx", False)
    End Sub

    Public Function getPlantaDetail(codigoPlanta As String) As IAC.ContractoServicio.Planta.GetPlantaDetail.Planta

        Dim objProxyPlanta As New Comunicacion.ProxyPlanta
        Dim objPeticionPlanta As New IAC.ContractoServicio.Planta.GetPlantaDetail.Peticion
        Dim objRespuestaPlanta As New IAC.ContractoServicio.Planta.GetPlantaDetail.Respuesta

        objPeticionPlanta.OidPlanta = codigoPlanta

        objRespuestaPlanta = objProxyPlanta.GetPlantaDetail(objPeticionPlanta)
        ' tratar retorno
        If Not Master.ControleErro.VerificaErro(objRespuestaPlanta.CodigoError, objRespuestaPlanta.NombreServidorBD, objRespuestaPlanta.MensajeError) Then
            Return Nothing
        End If

        Return objRespuestaPlanta.Planta

    End Function

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 20/02/2013 Criado
    ''' </history>
    Public Sub PreencherComboDelegacione()

        Dim objPeticion As New ContractoServicio.Delegacion.GetDelegacion.Peticion
        Dim objProxyDelegacione As New ProxyDelegacion
        Dim objRespuesta As New IAC.ContractoServicio.Delegacion.GetDelegacion.Respuesta

        Dim objProxyPais As New Comunicacion.ProxyPaises
        Dim objRespuestaPais As New IAC.ContractoServicio.Pais.GetPais.Respuesta

        objRespuestaPais = objProxyPais.GetPais()
        objPeticion.BolVigente = True
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objRespuesta = objProxyDelegacione.GetDelegaciones(objPeticion)

        For Each delegacion In objRespuesta.Delegacion
            Dim dx = delegacion
            Dim pais = objRespuestaPais.Pais.FirstOrDefault(Function(f) f.OidPais = dx.OidPais)
            If pais IsNot Nothing Then
                dx.DesDelegacion = pais.DesPais & " - " & dx.DesDelegacion
            End If
        Next

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ddlDelegacion.AppendDataBoundItems = True
        ddlDelegacion.Items.Clear()
        ddlDelegacion.Items.Add(New ListItem(Traduzir("014_ddl_selecione"), String.Empty))
        ddlDelegacion.DataTextField = "DesDelegacion"
        ddlDelegacion.DataValueField = "OidDelegacion"
        ddlDelegacion.DataSource = objRespuesta.Delegacion.OrderBy(Function(b) b.DesDelegacion)

        ddlDelegacion.DataBind()
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
    ''' <param name="codPlanta"></param>
    ''' <remarks></remarks>
    Public Sub CarregaDados(codPlanta As String)
        Dim objColPlanta As IAC.ContractoServicio.Planta.GetPlantaDetail.Planta
        Dim itemSelecionado As ListItem

        objColPlanta = getPlantaDetail(codPlanta)

        Planta = objColPlanta

        If objColPlanta IsNot Nothing Then

            Dim iCodigoAjeno = (From item In objColPlanta.CodigosAjenos
                               Where item.BolDefecto = True).FirstOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtCodigoAjeno.ToolTip = iCodigoAjeno.CodAjeno
                txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
                txtDesCodigoAjeno.ToolTip = iCodigoAjeno.DesAjeno
            End If

            'Preenche os controles do formulario
            txtCodigoPlanta.Text = objColPlanta.CodPlanta
            txtCodigoPlanta.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objColPlanta.CodPlanta, String.Empty)

            txtDescricaoPlanta.Text = objColPlanta.DesPlanta
            txtDescricaoPlanta.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objColPlanta.DesPlanta, String.Empty)

            If objColPlanta.BolActivo Then
                chkVigente.Enabled = False
            Else
                chkVigente.Enabled = True
            End If

            chkVigente.Checked = objColPlanta.BolActivo

            EsVigente = objColPlanta.BolActivo
            OidPlanta = objColPlanta.OidPlanta

            Session("ImporteMaximoEditar") = objColPlanta.ImportesMaximos

            If Not ddlDelegacion.Items.Contains(New ListItem(objColPlanta.DesDelegacion, objColPlanta.OidDelegacion)) Then

                Dim lstDelegacion As IAC.ContractoServicio.Delegacion.GetDelegacion.DelegacionColeccion = ViewState("Delegacion")
                Dim objAux As New IAC.ContractoServicio.Delegacion.GetDelegacion.Delegacion()

                objAux.OidDelegacion = objColPlanta.OidDelegacion
                objAux.DesDelegacion = objColPlanta.DesDelegacion

                lstDelegacion = New IAC.ContractoServicio.Delegacion.GetDelegacion.DelegacionColeccion
                lstDelegacion.Add(objAux)

                ddlDelegacion.DataSource = lstDelegacion
                ddlDelegacion.DataBind()

                ddlDelegacion.SelectedValue = objColPlanta.OidDelegacion

            End If

            'Seleciona o valor
            itemSelecionado = ddlDelegacion.Items.FindByValue(objColPlanta.OidDelegacion)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
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

                'Verifica se o codigo da planta foi enviado
                If txtCodigoPlanta.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoPlanta.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoPlanta.IsValid = False
                    UpdatePanelCodigo.Update()

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoPlanta.Focus()
                        focoSetado = True
                    End If
                Else
                    csvCodigoPlanta.IsValid = True
                End If

                'Verifica se a descrição da planta foi enviado
                If txtDescricaoPlanta.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False
                    UpdatePanelDescricao.Update()

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoPlanta.Focus()
                        focoSetado = True
                    End If
                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

                'Verifica se a delegação foi selecionado
                If ddlDelegacion.Visible AndAlso ddlDelegacion.SelectedValue.Equals(String.Empty) Then

                    strErro.Append(csvDelegacion.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDelegacion.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlDelegacion.Focus()
                        focoSetado = True
                    End If
                Else
                    csvDelegacion.IsValid = True
                End If

            End If


            'Verifica se o código existe
            If CodigoExistente Then

                strErro.Append(csvCodigoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoExistente.IsValid = False
                UpdatePanelCodigo.Update()

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    csvCodigoExistente.Focus()
                    focoSetado = True
                End If
            Else
                csvCodigoExistente.IsValid = True
                UpdatePanelCodigo.Update()
            End If

        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' Informa se o código da planta ja existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 20/02/2013 - Criado
    ''' </history>
    Private Function ExisteCodigoPlanta(codigo As String, delegacao As String) As Boolean

        Try

            Dim objPeticion As New IAC.ContractoServicio.Planta.GetPlanta.Peticion
            Dim objRespuesta As New IAC.ContractoServicio.Planta.GetPlanta.Respuesta
            Dim objProxyPlanta As New Comunicacion.ProxyPlanta

            objPeticion.oidDelegacion = delegacao
            objPeticion.CodPlanta = codigo
            objPeticion.BolActivo = Nothing
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False

            objRespuesta = objProxyPlanta.GetPlantas(objPeticion)

            If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                If objRespuesta.Planta.Count > 0 Then
                    Return True
                End If
            Else
                Return False
                Master.ControleErro.ShowError(objRespuesta.MensajeError)
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Function

    Private Sub ConsomeImporteMaximo()

        If Session("objRespuestaImporte") IsNot Nothing Then

            If Planta Is Nothing Then
                Planta = New ContractoServicio.Planta.GetPlantaDetail.Planta
            End If

            Planta.ImportesMaximos = Session("objRespuestaImporte")
            Session.Remove("objRespuestaImporte")

            If Planta.ImportesMaximos IsNot Nothing Then
                ImportesMaximoPeticion = Planta.ImportesMaximos
            Else
                ImportesMaximoPeticion = New ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase
            End If
        End If

    End Sub

    ''' <summary>
    ''' Consome o valor da session da coleção de código ajeno
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 07/05/2013 - Criado
    ''' </history>
    Private Sub ConsomeCodigoAjeno()

        If Session("objRespuestaGEPR_TPLANTA") IsNot Nothing Then

            If Planta Is Nothing Then
                Planta = New ContractoServicio.Planta.GetPlantaDetail.Planta
            End If

            Planta.CodigosAjenos = Session("objRespuestaGEPR_TPLANTA")
            Session.Remove("objRespuestaGEPR_TPLANTA")

            Dim iCodigoAjeno = (From item In Planta.CodigosAjenos
                                Where item.BolDefecto = True).SingleOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            If Planta.CodigosAjenos IsNot Nothing Then
                CodigosAjenosPeticion = Planta.CodigosAjenos
            Else
                CodigosAjenosPeticion = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
            End If
        End If

    End Sub
#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()
        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta
                btnGrabar.Visible = True        '1
                btnCancelar.Visible = True      '2
                btnVolver.Visible = False '3
                chkVigente.Visible = False
                chkVigente.Checked = True
                btnGrabar.Habilitado = True
                lblVigente.Visible = False
            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                btnCancelar.Visible = False              '2
                btnVolver.Visible = True                 '3
                txtCodigoPlanta.Enabled = False
                txtDescricaoPlanta.Enabled = False
                ddlDelegacion.Enabled = False
                ddlDelegacion.ToolTip = ddlDelegacion.SelectedItem.Text
                chkVigente.Enabled = False
                btnGrabar.Visible = False                '1

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                txtCodigoPlanta.Enabled = False
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

        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem de erro
        Dim MensagemErro As String = MontaMensagensErro()

        If MensagemErro <> String.Empty Then
            Master.ControleErro.ShowError(MensagemErro, False)
        End If

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    Private Property OidPlanta As String
        Get
            Return ViewState("OidPlanta")
        End Get
        Set(value As String)
            ViewState("OidPlanta") = value
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

    Private Property Planta() As ContractoServicio.Planta.GetPlantaDetail.Planta
        Get
            Return DirectCast(ViewState("Planta"), ContractoServicio.Planta.GetPlantaDetail.Planta)
        End Get
        Set(value As ContractoServicio.Planta.GetPlantaDetail.Planta)
            ViewState("Planta") = value
        End Set
    End Property

    ' ''' <summary>
    ' ''' Armazena em viewstate os códigos ajenos encontrados na busca.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    ' ''' <history>
    ' ''' [pgoncalves] 07/07/2013 - Criado
    ' ''' </history>
    'Private Property CodigosAjenos() As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
    '    Get
    '        Return DirectCast(ViewState("CodigosAjenos"), ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
    '    End Get

    '    Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
    '        ViewState("CodigosAjenos") = value
    '    End Set

    'End Property

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

    Private Property ImportesMaximoPeticion() As ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase
        Get
            Return DirectCast(ViewState("ImportesMaximoPeticion"), ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase)
        End Get

        Set(value As ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase)
            ViewState("ImportesMaximoPeticion") = value
        End Set

    End Property



#End Region

    Private Sub ddlDelegacion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDelegacion.SelectedIndexChanged
        ValidaCodigoPlantaExiste()
    End Sub
End Class