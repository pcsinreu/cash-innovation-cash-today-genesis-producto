Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comunicacion.ProxyWS.IAC

''' <summary>
''' Página de Mantenimiento de Sector 
''' </summary>
''' <remarks></remarks>
''' <history>[pgoncalves] 14/03/13 - Criado</history>
Public Class MantenimientoSector
    Inherits Base

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()
        'Adicionar scripts na página
        txtCodigoSector.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtCodigoSector.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoSetor.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoSetor.ClientID & "').focus();return false;}} else {return true}; ")
        txtSectorPadre.Attributes.Add("onkeyup", "limitaCaracteres('" & txtSectorPadre.ClientID & "','" & txtSectorPadre.MaxLength & "');")
        txtCodigoSector.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtCodigoSector.ClientID & "','" & txtCodigoSector.MaxLength & "');")
        txtDescricaoSetor.Attributes.Add("onblur", "limitaCaracteres('" & txtDescricaoSetor.ClientID & "','" & txtDescricaoSetor.MaxLength & "');")
        ddlPlanta.Attributes.Add("onKeyDown", "BloquearColar();")
        ddlTipoSector.Attributes.Add("onKeyDown", "BloquearColar();")

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)
    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        ddlPlanta.TabIndex = 1
        ddlTipoSector.TabIndex = 2
        chkCentroProceso.TabIndex = 3
        btnBuscarSector.TabIndex = 4
        txtCodigoSector.TabIndex = 5
        txtDescricaoSetor.TabIndex = 6
        btnAjeno.TabIndex = 7
        chkVigente.TabIndex = 8
        btnImporteMaximo.TabIndex = 9
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
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.SECTOR

    End Sub

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                'Recebe o código do Termino
                Dim strOidSector As String = Request.QueryString("OidSector")

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

                PreencherddlTipoSetor()
                PreencherddlPlanta(Nothing)
                PreencherddlDelegacion()

                If strOidSector <> String.Empty _
                    AndAlso strOidSector <> String.Empty Then
                    'Estado Inicial dos control
                    CarregaDados(strOidSector)
                End If

                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion _
                    OrElse MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    ddlPlanta.Focus()
                Else
                    ddlPlanta.Focus()
                    chkVigente.Checked = True
                End If
            End If

            ConsomeSector()
            ConsomeCodigoAjeno()
            ConsomeImporteMaximo()

            'Trata o foco dos campos
            TrataFoco()

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

        Master.TituloPagina = Traduzir("054_msg_tipo_pantalla_Sector")
        lblTituloSectores.Text = Traduzir("054_lbl_titulo_manutencao_setores")
        lblPlanta.Text = Traduzir("054_ddl_planta_manutencao")
        lblTipoSector.Text = Traduzir("054_ddl_TipoSector")
        lblCentroProcesso.Text = Traduzir("054_lbl_centro_proceso_manutencao")
        lblSectorPadre.Text = Traduzir("054_lbl_sector_padre_manutencao")
        lblCodigoSector.Text = Traduzir("054_lbl_Codigo_sector_manutencao")
        lblDescriptionSector.Text = Traduzir("054_lbl_descricao_sector_manutencao")
        lblCodigoAjeno.Text = Traduzir("054_lbl_codigo_prosegur_manutencao")
        lblVigente.Text = Traduzir("054_lbl_vigente")
        lblCodigoAjeno.Text = Traduzir("019_lbl_codigoAjeno")
        lblDescricaoCodigo.Text = Traduzir("019_lbl_descricaoAjeno")
        lblDelegacion.Text = Traduzir("019_lbl_delegacion")
        csvCodigoExistente.ErrorMessage = Traduzir("054_msg_erro_codigoExistente")
        csvCodigoSector.ErrorMessage = Traduzir("054_msg_codigo_setor_obrigatorio")
        csvDescricaoSetor.ErrorMessage = Traduzir("054_msg_Descricao_obrigatoria")
        csvTipoSector.ErrorMessage = Traduzir("054_msg_TipoSector_obrigatorio")
        csvPlanta.ErrorMessage = Traduzir("054_msg_planta_obrigatorio")
        csvDelegacion.ErrorMessage = Traduzir("054_msg_erroDelegacion")

        btnBuscarSector.ExibirLabelBtn = False
        btnAjeno.ExibirLabelBtn = False
    End Sub

#End Region

#Region "[EVENTOS]"
    Protected Sub btnImporteMaximo_Click(sender As Object, e As EventArgs) Handles btnImporteMaximo.Click
        Dim url As String = String.Empty
        Dim oidImporteMaximo As String = ""

        If Sector IsNot Nothing AndAlso Sector.ImportesMaximos IsNot Nothing AndAlso Sector.ImportesMaximos.Count > 0 Then
            oidImporteMaximo = Sector.ImportesMaximos.First.OidImporteMaximo

            Dim importsSession = Session("ImporteMaximoEditar")

            If importsSession Is Nothing OrElse importsSession.Count <> Sector.ImportesMaximos.Count Then
                Session("ImporteMaximoEditar") = Sector.ImportesMaximos
            End If
        End If

        If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
            url = "MantenimientoImporteMaximo.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&codimporte=" & txtCodigoSector.Text & "&descImporte=" & txtDescricaoSetor.Text & "&oidimporte=" & oidImporteMaximo
        ElseIf (Aplicacao.Util.Utilidad.eAcao.Modificacion = MyBase.Acao) Then
            url = "MantenimientoImporteMaximo.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&codimporte=" & txtCodigoSector.Text & "&descImporte=" & txtDescricaoSetor.Text & "&oidimporte=" & oidImporteMaximo
        ElseIf (Aplicacao.Util.Utilidad.eAcao.Alta = MyBase.Acao) Then
            url = "MantenimientoImporteMaximo.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&codimporte=" & txtCodigoSector.Text & "&descImporte=" & txtDescricaoSetor.Text
        End If
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_importeMaximo", "AbrirPopupModal('" & url & "', 550, 900,'btnImporteMaximo'); ", True)

    End Sub
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

    ''' <summary>
    ''' Ação ao clicar no botão buscar Sector
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnBuscarSector_Click(sender As Object, e As EventArgs) Handles btnBuscarSector.Click

        Dim entidadeDados As New BusquedaSimplesSectores.DadosPesquisa

        Try

            If ddlPlanta.SelectedValue.Equals(String.Empty) Then
                Master.ControleErro.ShowError(Traduzir("054_msg_erro_CodigoPlantaVazio"), False)
                csvPlanta.IsValid = False
                ddlPlanta.Focus()
                Exit Sub
            End If

            If ddlTipoSector.SelectedValue.Equals(String.Empty) Then
                Master.ControleErro.ShowError(Traduzir("054_msg_erro_CodigoTipoSector"), False)
                csvTipoSector.IsValid = False
                ddlTipoSector.Focus()
                Exit Sub
            End If

            entidadeDados.BolCentroProceso = chkCentroProceso.Checked
            entidadeDados.OidPlanta = ddlPlanta.SelectedValue
            entidadeDados.OidTipoSector = ddlTipoSector.SelectedValue

            Session("EntidadeDados") = entidadeDados

            Dim url As String = "BusquedaSimplesSectores.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & Aplicacao.Util.Utilidad.eAcao.Consulta & "&oidSetor=" & OidSector
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnBuscarSector');", True)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Protected Sub txtCodigoSector_TextChanged(sender As Object, e As EventArgs) Handles txtCodigoSector.TextChanged

        If (String.IsNullOrEmpty(txtCodigoSector.Text)) Then
            Exit Sub
        End If

        If ddlPlanta.SelectedValue.Equals(String.Empty) Then
            Exit Sub
        End If

        Try
            If ExisteCodigoSector(txtCodigoSector.Text, ddlPlanta.SelectedValue) Then
                CodigoExistente = True
            Else
                CodigoExistente = False
            End If
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    Protected Sub ddlPlanta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPlanta.SelectedIndexChanged

        If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse Aplicacao.Util.Utilidad.eAcao.Alta Then
            txtCodigoSector.Enabled = True
            txtDescricaoSetor.Enabled = True
            ddlTipoSector.Enabled = True
            chkCentroProceso.Enabled = True
            txtSectorPadre.Text = ""
        End If
    End Sub

    Protected Sub ddlTipoSector_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoSector.SelectedIndexChanged
        txtSectorPadre.Text = ""
        OidSectorPadre = String.Empty

        If Not ddlTipoSector.SelectedValue.Equals(String.Empty) Then
            Session("CodigoTipoSector") = ddlTipoSector.SelectedValue
        End If

    End Sub

    Private Sub btnAjeno_Click(sender As Object, e As System.EventArgs) Handles btnAjeno.Click
        Dim url As String = String.Empty
        Dim tablaGenesis As New MantenimientoCodigosAjenos.CodigoAjenoSimples

        tablaGenesis.CodTablaGenesis = txtCodigoSector.Text
        tablaGenesis.DesTablaGenesis = txtDescricaoSetor.Text
        tablaGenesis.OidTablaGenesis = OidSector
        If Sector IsNot Nothing AndAlso Sector.CodigosAjenos IsNot Nothing Then
            tablaGenesis.CodigosAjenos = Sector.CodigosAjenos
        End If

        Session("objGEPR_TSECTOR") = tablaGenesis

        If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TSECTOR"
        Else
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TSECTOR"
        End If

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 900,'btnAjeno');", True)
    End Sub

    Protected Sub ddlDelegacion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDelegacion.SelectedIndexChanged

        If Not ddlDelegacion.SelectedValue.Equals(String.Empty) Then
            PreencherddlPlanta(ddlDelegacion.SelectedValue)
        Else
            PreencherddlPlanta(Nothing)
        End If

    End Sub
#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Função do botão grabar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 14/03/2013 - Criado
    ''' </history>
    Public Sub ExecutarGrabar()
        Try

            Dim objProxySetor As New Comunicacion.ProxySector
            Dim objRespuestaSetor As IAC.ContractoServicio.Setor.SetSectores.Respuesta
            Dim strErro As New Text.StringBuilder(String.Empty)

            ValidarCamposObrigatorios = True

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If

            Dim objPeticion As New IAC.ContractoServicio.Setor.SetSectores.Peticion
            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticion.bolActivo = True
            Else
                objPeticion.bolActivo = chkVigente.Checked
            End If

            ' atualizar propriedade vigente
            EsVigente = chkVigente.Checked

            objPeticion.bolCentroProceso = chkCentroProceso.Checked
            objPeticion.codSector = txtCodigoSector.Text
            objPeticion.desSector = txtDescricaoSetor.Text

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticion.gmtCreacion = DateTime.Now
                objPeticion.desUsuarioCreacion = MyBase.LoginUsuario
            Else
                objPeticion.gmtCreacion = Sector.gmtCreacion
                objPeticion.desUsuarioCreacion = Sector.desUsuarioCreacion
            End If

            objPeticion.codMigracion = If(Sector IsNot Nothing AndAlso Not String.IsNullOrEmpty(Sector.codMigracion), Sector.codMigracion, "")
            objPeticion.gmtModificacion = DateTime.Now
            objPeticion.desUsuarioModificacion = MyBase.LoginUsuario
            objPeticion.oidPlanta = ddlPlanta.SelectedValue
            objPeticion.oidSectorPadre = OidSectorPadre
            objPeticion.oidTipoSector = ddlTipoSector.SelectedValue
            objPeticion.oidSector = OidSector

            objPeticion.CodigosAjenos = CodigosAjenosPeticion

            objPeticion.ImporteMaximo = ImportesMaximoPeticion

            objRespuestaSetor = objProxySetor.setSectores(objPeticion)

            Dim url As String = "BusquedaSector.aspx"

            Session.Remove("objRespuestaGEPR_TSECTOR")
            Session.Remove("ImporteMaximoEditar")
            If Master.ControleErro.VerificaErro(objRespuestaSetor.CodigoError, objRespuestaSetor.NombreServidorBD, objRespuestaSetor.MensajeError) Then
                'Registro gravado com sucesso
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_grabado_suceso", "alert('" & Traduzir("001_msg_grabado_suceso") & _
                                                                                    "'); RedirecionaPaginaNormal('" & url & "');", True)
            Else
                If objRespuestaSetor.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    Master.ControleErro.ShowError(objRespuestaSetor.MensajeError, False)
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
    ''' [pgoncalves] 14/03/2013 - Criado
    ''' </history>
    Public Sub ExecutarCancelar()
        If Session("objRespuestaGEPR_TSECTOR") IsNot Nothing Then
            Session.Remove("objRespuestaGEPR_TSECTOR")
        End If

        If Session("ImporteMaximoEditar") IsNot Nothing Then
            Session.Remove("ImporteMaximoEditar")
        End If

        Response.Redirect("~/BusquedaSector.aspx", False)
    End Sub

    ''' <summary>
    ''' Função do botão volver.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 14/03/2013 - Criado
    ''' </history>
    Public Sub ExecutarVolver()
        If Session("objRespuestaGEPR_TSECTOR") IsNot Nothing Then
            Session.Remove("objRespuestaGEPR_TSECTOR")
        End If
        If Session("objRespuestaImporte") IsNot Nothing Then
            Session.Remove("objRespuestaImporte")
        End If
        Response.Redirect("~/BusquedaSector.aspx", False)
    End Sub

    ''' <summary>
    ''' Preenche o dropDown de Tipo Setor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PreencherddlTipoSetor()

        Dim objProxyTipoSetor As New Comunicacion.ProxyTipoSetor
        Dim objPeticion As New ContractoServicio.TipoSetor.GetTiposSectores.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.TipoSetor.GetTiposSectores.Respuesta

        objPeticion.bolActivo = True
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objProxyTipoSetor.GetTiposSectores(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ViewState("TipoSetor") = objRespuesta.TipoSetor

        ddlTipoSector.AppendDataBoundItems = True
        ddlTipoSector.Items.Clear()
        ddlTipoSector.Items.Add(New ListItem(Traduzir("054_ddl_selecione"), String.Empty))
        ddlTipoSector.DataTextField = "desTipoSector"
        ddlTipoSector.DataValueField = "oidTipoSector"
        ddlTipoSector.DataSource = objRespuesta.TipoSetor.OrderBy(Function(b) b.desTipoSector)
        ddlTipoSector.DataBind()

    End Sub

    ''' <summary>
    ''' Preenche o dropdownbox de Planta
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PreencherddlPlanta(oidDelegacion As String)

        Dim objProxyPlanta As New Comunicacion.ProxyPlanta
        Dim objPeticionPlanta As New ContractoServicio.Planta.GetPlanta.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Planta.GetPlanta.Respuesta

        If String.IsNullOrEmpty(oidDelegacion) Then
            ddlPlanta.Items.Clear()
            ddlPlanta.Items.Add(New ListItem(Traduzir("054_ddl_selecione"), String.Empty))
            Exit Sub
        Else
            objPeticionPlanta.oidDelegacion = oidDelegacion
        End If

        objPeticionPlanta.BolActivo = True
        objPeticionPlanta.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objProxyPlanta.GetPlantas(objPeticionPlanta)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        If objRespuesta.Planta.Count = 0 Then
            ddlPlanta.Items.Clear()
            ddlPlanta.Items.Add(New ListItem(Traduzir("054_ddl_selecione"), String.Empty))
            Exit Sub
        Else
            ddlPlanta.AppendDataBoundItems = True
            ddlPlanta.Items.Clear()
            ddlPlanta.Items.Add(New ListItem(Traduzir("054_ddl_selecione"), String.Empty))
            ddlPlanta.DataTextField = "DesPlanta"
            ddlPlanta.DataValueField = "OidPlanta"
            ddlPlanta.DataSource = objRespuesta.Planta.OrderBy(Function(b) b.DesPlanta)
            ddlPlanta.DataBind()
            ddlPlanta.Enabled = True
        End If

    End Sub

    ''' Preencher a dropdownbox de delegaciones
    Public Sub PreencherddlDelegacion()

        Dim objPeticion As New ContractoServicio.Delegacion.GetDelegacion.Peticion
        Dim objRespuesta As New ContractoServicio.Delegacion.GetDelegacion.Respuesta
        Dim objProxy As New ProxyDelegacion

        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objPeticion.BolVigente = True

        objRespuesta = objProxy.GetDelegaciones(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) _
           OrElse objRespuesta.Delegacion.Count = 0 Then
            ddlPlanta.Items.Clear()
            ddlPlanta.Items.Add(New ListItem(Traduzir("054_ddl_selecione"), String.Empty))
            Exit Sub
        End If

        If objRespuesta.Delegacion.Count > 0 Then
            ddlDelegacion.AppendDataBoundItems = True
            ddlDelegacion.Items.Clear()
            ddlDelegacion.Items.Add(New ListItem(Traduzir("054_ddl_selecione"), String.Empty))
            ddlDelegacion.DataTextField = "DesDelegacion"
            ddlDelegacion.DataValueField = "OidDelegacion"
            ddlDelegacion.DataSource = objRespuesta.Delegacion.OrderBy(Function(b) b.DesDelegacion)
            ddlDelegacion.DataBind()
        End If

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
    ''' Carrega os dados do sector enviado por parametro
    ''' </summary>
    ''' <param name="oidSetor"></param>
    ''' <remarks></remarks>
    Public Sub CarregaDados(oidSetor As String)

        Dim objSector As IAC.ContractoServicio.Setor.GetSectoresDetail.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.Setor.GetSectoresDetail.Peticion
        Dim objProxySector As New Comunicacion.ProxySector
        Dim itemSelecionadoPlanta As ListItem
        Dim itemSelecionadoTpSector As ListItem
        Dim itemSelecionadoDelegacion As ListItem

        objPeticion.OidSector = oidSetor

        objSector = objProxySector.getSetorDetail(objPeticion)

        ' tratar retorno
        If Not Master.ControleErro.VerificaErro(objSector.CodigoError, objSector.NombreServidorBD, objSector.MensajeError) Then
            Return
        End If

        Sector = objSector.Sector

        If objSector.Sector IsNot Nothing Then

            Dim iCodigoAjeno = (From iten In objSector.Sector.CodigosAjenos
                                Where iten.BolDefecto = True).FirstOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtCodigoAjeno.ToolTip = iCodigoAjeno.CodAjeno
                txtDescricaoAjeno.Text = iCodigoAjeno.DesAjeno
                txtDescricaoAjeno.ToolTip = iCodigoAjeno.DesAjeno
            End If

            'Preenche os controles do formulario
            txtCodigoSector.Text = objSector.Sector.codSector
            txtCodigoSector.ToolTip = If(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objSector.Sector.codSector, String.Empty)

            txtDescricaoSetor.Text = objSector.Sector.desSector
            txtDescricaoSetor.ToolTip = If(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objSector.Sector.desSector, String.Empty)

            If Not String.IsNullOrEmpty(objSector.Sector.desSectorPadre) _
                AndAlso Not String.IsNullOrEmpty(objSector.Sector.codSectorPadre) Then
                txtSectorPadre.Text = objSector.Sector.codSectorPadre & " - " & objSector.Sector.desSectorPadre
                txtSectorPadre.ToolTip = If(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objSector.Sector.desSectorPadre, String.Empty)
            End If

            chkCentroProceso.Checked = objSector.Sector.bolCentroProceso

            chkVigente.Checked = objSector.Sector.bolActivo
            If objSector.Sector.bolActivo Then
                chkVigente.Enabled = False
            Else
                chkVigente.Enabled = True
            End If

            EsVigente = objSector.Sector.bolActivo
            OidPlanta = objSector.Sector.oidPlanta
            OidTipoSector = objSector.Sector.oidTipoSector
            OidSectorPadre = objSector.Sector.oidSectorPadre
            OidSector = objSector.Sector.oidSector

            If Not ddlTipoSector.Items.Contains(New ListItem(objSector.Sector.desTipoSector, objSector.Sector.oidTipoSector)) Then

                Dim lstTipoSetor As IAC.ContractoServicio.TipoSetor.GetTiposSectores.TipoSetorColeccion = ViewState("TipoSetor")
                Dim objAux As New IAC.ContractoServicio.TipoSetor.GetTiposSectores.TipoSetor()

                objAux.oidTipoSector = objSector.Sector.oidTipoSector
                objAux.desTipoSector = objSector.Sector.desTipoSector

                lstTipoSetor = New IAC.ContractoServicio.TipoSetor.GetTiposSectores.TipoSetorColeccion
                lstTipoSetor.Add(objAux)

                ddlTipoSector.DataSource = lstTipoSetor
                ddlTipoSector.DataBind()

                ddlTipoSector.SelectedValue = objSector.Sector.oidTipoSector

            End If

            If Not ddlDelegacion.Items.Contains(New ListItem(objSector.Sector.DesDelegacion, objSector.Sector.OidDelegacion)) Then

                Dim lstDelegacion As IAC.ContractoServicio.Delegacion.GetDelegacion.DelegacionColeccion = ViewState("Delegacion")
                Dim objAux As New IAC.ContractoServicio.Delegacion.GetDelegacion.Delegacion()

                objAux.OidDelegacion = objSector.Sector.OidDelegacion
                objAux.DesDelegacion = objSector.Sector.DesDelegacion

                lstDelegacion = New IAC.ContractoServicio.Delegacion.GetDelegacion.DelegacionColeccion
                lstDelegacion.Add(objAux)

                ddlDelegacion.DataSource = lstDelegacion
                ddlDelegacion.DataBind()

                ddlDelegacion.SelectedValue = objSector.Sector.oidTipoSector

            End If

            itemSelecionadoDelegacion = ddlDelegacion.Items.FindByValue(objSector.Sector.OidDelegacion)
            If itemSelecionadoDelegacion IsNot Nothing Then
                itemSelecionadoDelegacion.Selected = True
            End If

            PreencherddlPlanta(itemSelecionadoDelegacion.Value)

            'Seleciona o valor
            itemSelecionadoPlanta = ddlPlanta.Items.FindByValue(objSector.Sector.oidPlanta)
            If itemSelecionadoPlanta IsNot Nothing Then
                itemSelecionadoPlanta.Selected = True
            End If

            itemSelecionadoTpSector = ddlTipoSector.Items.FindByValue(objSector.Sector.oidTipoSector)
            If itemSelecionadoTpSector IsNot Nothing Then
                itemSelecionadoTpSector.Selected = True
            End If

            ddlPlanta.SelectedValue = objSector.Sector.oidPlanta
            ddlTipoSector.SelectedValue = objSector.Sector.oidTipoSector
            ddlDelegacion.SelectedValue = objSector.Sector.OidDelegacion
            Session("ImporteMaximoEditar") = objSector.Sector.ImportesMaximos
        Else
            Response.Redirect("~/BusquedaSector.aspx", False)
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

                'Verifica se a delegação foi selecionada
                If ddlDelegacion.Visible AndAlso ddlDelegacion.SelectedValue.Equals(String.Empty) Then

                    strErro.Append(csvDelegacion.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDelegacion.IsValid = False

                    'Setar o foco no primeiro campo que deu erro
                    If focoSetado AndAlso Not focoSetado Then
                        ddlDelegacion.Focus()
                        focoSetado = True
                    End If
                Else
                    csvDelegacion.IsValid = True
                End If

                'Verifica se a planta foi enviada
                If ddlPlanta.Visible AndAlso ddlPlanta.SelectedValue.Equals(String.Empty) Then

                    strErro.Append(csvPlanta.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvPlanta.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlPlanta.Focus()
                        focoSetado = True
                    End If
                Else
                    csvPlanta.IsValid = True
                End If

                'Verifica se o código da delegação foi enviado
                If txtCodigoSector.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoSector.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoSector.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoSector.Focus()
                        focoSetado = True
                    End If
                Else
                    csvCodigoSector.IsValid = True
                End If

                'Verifica se a descrição foi enviada
                If txtDescricaoSetor.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoSetor.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoSetor.IsValid = False
                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoSetor.Focus()
                        focoSetado = True
                    End If
                Else
                    csvDescricaoSetor.IsValid = True
                End If

                'Verifica se o tipo setor foi enviado
                If ddlTipoSector.Visible AndAlso ddlTipoSector.SelectedValue.Equals(String.Empty) Then

                    strErro.Append(csvTipoSector.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTipoSector.IsValid = False

                    'Setar o foco no primeiro que controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlTipoSector.Focus()
                        focoSetado = True
                    End If
                Else
                    csvTipoSector.IsValid = True
                End If

            End If

            'Verifica se o código existe
            If CodigoExistente Then

                strErro.Append(csvCodigoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoSector.Focus()
                    focoSetado = True
                End If
            Else
                csvCodigoExistente.IsValid = True
            End If
        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' Informa se o código do setor já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 14/03/2013 - Criado
    ''' </history>
    Private Function ExisteCodigoSector(codigo As String, planta As String) As Boolean

        Try

            Dim objProxySetor As New Comunicacion.ProxySector
            Dim objPeticion As New IAC.ContractoServicio.Setor.GetSectores.Peticion
            Dim objRespuesta As IAC.ContractoServicio.Setor.GetSectores.Respuesta

            'Verifica se o código do setor existe no BD
            objPeticion.codSector = codigo
            objPeticion.oidPlanta = planta
            objPeticion.bolActivo = Nothing
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False

            objRespuesta = objProxySetor.getSectores(objPeticion)

            If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                If objRespuesta.Setor.Count > 0 Then
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

    Private Sub ConsomeSector()

        If Session("SectorSelecionado") IsNot Nothing Then

            Dim objSetor As New ContractoServicio.Setor.GetSectores.Setor
            objSetor = TryCast(Session("SectorSelecionado"), ContractoServicio.Setor.GetSectores.Setor)

            If objSetor IsNot Nothing Then

                OidSectorPadre = objSetor.oidSector

                ' setar controles da tela
                txtSectorPadre.Text = objSetor.codSector & " - " & objSetor.desSector
                txtSectorPadre.ToolTip = objSetor.codSector & " - " & objSetor.desSector

            End If

            Session.Remove("SectorSelecionado")
            Session.Remove("OidSetor")

        End If

    End Sub

    'Mostrar Mensagens de Erro caso seja postback
    Private Sub MostrarMensagem()
        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem de erro
        Dim MensagemErro As String = MontaMensagensErro()

        If MensagemErro <> String.Empty Then
            Master.ControleErro.ShowError(MensagemErro, False)
        End If
    End Sub
    Private Sub ConsomeImporteMaximo()

        If Session("objRespuestaImporte") IsNot Nothing Then

            If Sector Is Nothing Then
                Sector = New ContractoServicio.Setor.GetSectoresDetail.Sector
            End If

            Sector.ImportesMaximos = Session("objRespuestaImporte")
            Session.Remove("objRespuestaImporte")

            If Sector.ImportesMaximos IsNot Nothing Then
                ImportesMaximoPeticion = Sector.ImportesMaximos
            Else
                ImportesMaximoPeticion = New ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase
            End If
        End If

    End Sub
    Private Sub ConsomeCodigoAjeno()
        If Session("objRespuestaGEPR_TSECTOR") IsNot Nothing Then

            If Sector Is Nothing Then
                Sector = New ContractoServicio.Setor.GetSectoresDetail.Sector
            End If
            Sector.CodigosAjenos = Session("objRespuestaGEPR_TSECTOR")
            Session.Remove("objRespuestaGEPR_TSECTOR")

            Dim iCodigoAjeno = (From item In Sector.CodigosAjenos
                                Where item.BolDefecto = True).SingleOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDescricaoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            If Sector.CodigosAjenos IsNot Nothing Then
                CodigosAjenosPeticion = Sector.CodigosAjenos
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

                'Estado Inicial Controles                                
                btnVolver.Visible = False       '3
                btnGrabar.Habilitado = True
                txtSectorPadre.Enabled = False
                chkCentroProceso.Checked = True
                lblVigente.Visible = False
                chkVigente.Checked = True
                chkVigente.Visible = False
                pnlBuscaSetor.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta

                btnCancelar.Visible = False              '2
                btnVolver.Visible = True                 '3

                'Estado Inicial Controles
                txtDescricaoSetor.Enabled = False
                txtCodigoSector.Enabled = False
                txtSectorPadre.Enabled = False
                ddlPlanta.Enabled = False
                ddlTipoSector.Enabled = False
                btnGrabar.Visible = False                '1
                chkCentroProceso.Enabled = False
                chkVigente.Enabled = False
                pnlBuscaSetor.Visible = False

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                txtCodigoSector.Enabled = False
                btnGrabar.Visible = True               '1
                btnCancelar.Visible = True             '2
                btnVolver.Visible = False              '3
                btnGrabar.Habilitado = True
                txtDescricaoSetor.Enabled = True
                ddlPlanta.Enabled = True
                ddlTipoSector.Enabled = True
                btnGrabar.Visible = True                '1
                chkCentroProceso.Enabled = True
                pnlBuscaSetor.Visible = True

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
    Private Property ImportesMaximoPeticion() As ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase
        Get
            Return DirectCast(ViewState("ImportesMaximoPeticion"), ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase)
        End Get

        Set(value As ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase)
            ViewState("ImportesMaximoPeticion") = value
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

    Private Property OidPlanta() As String
        Get
            Return ViewState("OidPlanta")
        End Get
        Set(value As String)
            ViewState("OidPlanta") = value
        End Set
    End Property

    Private Property OidTipoSector As String
        Get
            Return ViewState("OidTipoSector")
        End Get
        Set(value As String)
            ViewState("OidTipoSector") = value
        End Set
    End Property

    Private Property OidSectorPadre As String
        Get
            Return ViewState("OidSectorPadre")
        End Get
        Set(value As String)
            ViewState("OidSectorPadre") = value
        End Set
    End Property

    Private Property OidSector As String
        Get
            Return ViewState("OidSector")
        End Get
        Set(value As String)
            ViewState("OidSector") = value
        End Set
    End Property

    Private Property Sector As ContractoServicio.Setor.GetSectoresDetail.Sector
        Get
            Return DirectCast(ViewState("Sector"), ContractoServicio.Setor.GetSectoresDetail.Sector)
        End Get
        Set(value As ContractoServicio.Setor.GetSectoresDetail.Sector)
            ViewState("Sector") = value
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

End Class