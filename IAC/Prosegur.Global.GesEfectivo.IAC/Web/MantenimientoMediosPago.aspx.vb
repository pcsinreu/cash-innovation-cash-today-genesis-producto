Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' Página de Gerenciamento de Medios de Pago 
''' </summary>
''' <remarks></remarks>
''' <history>[PDA] 05/03/09 - Criado</history>
Partial Public Class MantenimientoMediosPago
    Inherits Base

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

        Dim url As String = "MantenimientoTerminoMediosPago.aspx"

        txtCodigoMedioPago.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoMedioPago.ClientID & "').focus();return false;}} else {return true}; ")
        txtObservaciones.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onblur", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onkeyup", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")

        Dim pbo As PostBackOptions
        Dim s As String = String.Empty


        pbo = New PostBackOptions(btnBaja)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnBaja.FuncaoJavascript = "if(VerificarRegistroSelecionadoGridView('" & ProsegurGridView1.ClientID & "','" & _
                                           Traduzir(Aplicacao.Util.Utilidad.InfoMsgSeleccionarRegistro) & "','" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & _
                                           "','" & Traduzir("info_msg_registro_borrado") & " '))" & s & ";"

        pbo = New PostBackOptions(btnConsulta)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnConsulta.FuncaoJavascript = "if(VerificarRegistroSelecionadoGridView('" & ProsegurGridView1.ClientID & "','" & _
                                            Traduzir(Aplicacao.Util.Utilidad.InfoMsgSeleccionarRegistro) & _
                                            "',''))" & s & ";"

        pbo = New PostBackOptions(btnModificacion)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnModificacion.FuncaoJavascript = "if(VerificarRegistroSelecionadoGridView('" & ProsegurGridView1.ClientID & "','" & _
                                            Traduzir(Aplicacao.Util.Utilidad.InfoMsgSeleccionarRegistro) & _
                                            "',''))" & s & ";"

        'Controle de precedência(Ajax)
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)
    End Sub

    Protected Overrides Sub ConfigurarTabIndex()
        ddlTipoMedioPago.TabIndex = 1
        ddlDivisa.TabIndex = 2
        txtCodigoMedioPago.TabIndex = 3
        txtDescricaoMedioPago.TabIndex = 4
        txtCodigoAcceso.TabIndex = 5
        txtObservaciones.TabIndex = 6
        chkVigente.TabIndex = 7
        btnAcima.TabIndex = 8
        btnAbaixo.TabIndex = 9
        btnAlta.TabIndex = 10
        btnBaja.TabIndex = 11
        btnModificacion.TabIndex = 12
        btnConsulta.TabIndex = 13
        btnGrabar.TabIndex = 14
        btnCancelar.TabIndex = 15
        btnVolver.TabIndex = 16

        Master.PrimeiroControleTelaID = ddlTipoMedioPago.ClientID
        Me.DefinirRetornoFoco(Master.Sair, Master.RetornaMenu)

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Consulta

                If TerminoMediosPagoTemporario.Count > 0 Then
                    btnConsulta.Focus()
                    Master.PrimeiroControleTelaID = "btnConsulta_img"
                Else
                    btnVolver.Focus()
                    Master.PrimeiroControleTelaID = "btnVolver_img"
                End If

                Me.DefinirRetornoFoco(Master.Sair, Master.RetornaMenu)

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion

                txtDescricaoMedioPago.Focus()
                Master.PrimeiroControleTelaID = txtDescricaoMedioPago.ClientID
                Me.DefinirRetornoFoco(Master.Sair, Master.RetornaMenu)

        End Select

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.MEDIO_DE_PAGO

    End Sub

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                'Recebe o código do MedioPago
                Dim strCodMedioPago As String = Request.QueryString("codMedioPago")
                'Recebe o código da divisa MedioPago
                Dim strCodDivisa As String = Request.QueryString("codDivisaMedioPago")
                'Recebe o código do TipoMedioPago
                Dim strCodTipoMedioPago As String = Request.QueryString("codTipoMedioPago")

                'Possíveis Ações passadas pela página BusquedaMediosPago:
                ' [-] Aplicacao.Util.Utilidad.eAcao.Alta
                ' [-] Aplicacao.Util.Utilidad.eAcao.Modificacion
                ' [-] Aplicacao.Util.Utilidad.eAcao.Consulta
                ' [-] Aplicacao.Util.Utilidad.eAcao.Duplicar

                If Not (MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar _
                        ) Then

                    Acao = Aplicacao.Util.Utilidad.eAcao.Erro
                    'Informa ao usuário que o parâmetro passado são inválidos
                    Throw New Exception(Traduzir("err_passagem_parametro"))

                End If

                'Preenche Combo
                PreencherComboDivisa()

                'Preenche CheckBoxlist
                PreencherComboTipoMedioPago()

                If strCodMedioPago <> String.Empty AndAlso _
                   strCodDivisa <> String.Empty AndAlso _
                   strCodTipoMedioPago <> String.Empty Then

                    'Estado Inicial dos control
                    CarregaDados(strCodMedioPago, strCodDivisa, strCodTipoMedioPago)

                End If

                'Foco no campo divisa
                ddlTipoMedioPago.Focus()

            End If

            'Consome o TerminoMedioPago passado pela PopUp de "TerminosMedioPago"
            ConsomeTerminoMedioPago()

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

        'Titula da Página
        Master.TituloPagina = Traduzir("014_titulo_mantenimiento_mediospago")
        'SubTitulo da Página
        lblTituloMediosPago.Text = Traduzir("014_lbl_subtitulosmediospago")
        'Subtitulo do Formulário

        lblSubTitulosMediosPago.Text = Traduzir("014_lbl_subtitulosterminomediospago")

        'Campos do formulário

        lblDivisa.Text = Traduzir("014_lbl_divisa")
        lblTipoMedioPago.Text = Traduzir("014_lbl_tipo_medio_pago")
        lblCodigoMedioPago.Text = Traduzir("014_lbl_codigomediopago")
        lblDescricaoMedioPago.Text = Traduzir("014_lbl_descricaomediopago")
        lblMercancia.Text = Traduzir("014_lbl_mercancia")
        lblVigente.Text = Traduzir("014_lbl_vigente")
        lblObservaciones.Text = Traduzir("014_lbl_observaciones")
        lblCodigoAcceso.Text = Traduzir("014_lbl_codigoacceso")

        'GridView
        ProsegurGridView1.PagerSummary = Traduzir("grid_lbl_pagersummary")

        'Custom Validator
        csvCodigoObrigatorio.ErrorMessage = Traduzir("014_msg_mediopagocodigoobligatorio")
        csvTipoMedioPagoExistente.ErrorMessage = Traduzir("014_msg_codigomediopagoexistente")
        csvDivisaMedioPagoExistente.ErrorMessage = Traduzir("014_msg_codigomediopagoexistente")
        csvCodigoMedioPagoExistente.ErrorMessage = Traduzir("014_msg_codigomediopagoexistente")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("014_msg_mediopagodescripcionobligatorio")
        csvTipoMedioPagoObrigatorio.ErrorMessage = Traduzir("014_msg_mediopagotipoobligatorio")
        csvDivisaObrigatorio.ErrorMessage = Traduzir("014_msg_mediopagodivisaobligatorio")
        csvCodigoAccesoObligatorio.ErrorMessage = Traduzir("014_msg_codigoaccesoobligatorio")
        csvCodigoAccesoExistente.ErrorMessage = Traduzir("014_msg_codigoaccesoexistente")

    End Sub

#End Region

#Region "[DADOS]"

    Public Function getMedioPagoDetail(codigoMedioPago As String, codigoDivisa As String, codigoTipoMedioPago As String) As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.MedioPagoColeccion

        Dim objProxyMedioPago As New Comunicacion.ProxyMedioPago
        Dim objPeticionMedioPago As New IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.Peticion
        Dim objRespuestaMedioPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.Respuesta

        'Recebe os valores do filtro
        Dim listaCodigoMedioPago As New List(Of String)
        listaCodigoMedioPago.Add(codigoMedioPago)

        Dim listaCodigoDivisaMedioPago As New List(Of String)
        listaCodigoDivisaMedioPago.Add(codigoDivisa)

        Dim listaCodigoTipoMedioPago As New List(Of String)
        listaCodigoTipoMedioPago.Add(codigoTipoMedioPago)

        objPeticionMedioPago.CodigoMedioPago = listaCodigoMedioPago
        objPeticionMedioPago.CodigoIsoDivisa = listaCodigoDivisaMedioPago
        objPeticionMedioPago.CodigoTipoMedioPago = listaCodigoTipoMedioPago

        objRespuestaMedioPago = objProxyMedioPago.GetMedioPagoDetail(objPeticionMedioPago)

        Return objRespuestaMedioPago.MedioPagos

    End Function

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS GRIDVIEW]"

    ''' <summary>
    ''' Evento disparado quando a linha do Gridview esta sendo populada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ProsegurGridView1_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.EOnClickRowClientScript
        Try

            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            ProsegurGridView1.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("vigente").ToString.ToLower & ");"

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento de preencher o Gridview quando o mesmo é paginado ou ordenado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPreencheDados

        Try
            Dim objDT As DataTable

            objDT = ProsegurGridView1.ConvertListToDataTable(TerminoMediosPagoTemporario)

            objDT.DefaultView.Sort = ProsegurGridView1.SortCommand

            ProsegurGridView1.ControleDataSource = objDT

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try


    End Sub

    ''' <summary>
    ''' Configura o css do objeto pager do Gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"


        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try


    End Sub

    ''' <summary>
    ''' RowDataBound do Gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowDataBound

        Try

            'Índice das celulas do GridView Configuradas
            '0 - Código
            '1 - Descripción
            '2 - Formato
            '3 - Vigente

            If e.Row.DataItem IsNot Nothing Then

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                If Not e.Row.DataItem("descripcion") Is DBNull.Value AndAlso e.Row.DataItem("descripcion").Length > NumeroMaximoLinha Then
                    e.Row.Cells(1).Text = e.Row.DataItem("descripcion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                If CBool(e.Row.DataItem("vigente")) Then
                    CType(e.Row.Cells(3).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
                Else
                    CType(e.Row.Cells(3).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
                End If
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' RowCreated do Gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("014_lbl_grd_mantenimiento_codigo")
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("014_lbl_grd_mantenimiento_descripcion")
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("014_lbl_grd_mantenimiento_formato")
                CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("014_lbl_grd_mantenimiento_vigente")
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

#End Region

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Clique botão Alta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAlta_Click(sender As Object, e As EventArgs) Handles btnAlta.Click
        ExecutarAlta()
    End Sub

    ''' <summary>
    ''' Clique botão Baja
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click
        ExecutarBaja()
    End Sub

    ''' <summary>
    ''' Clique botão Modificacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnModificacion_Click(sender As Object, e As EventArgs) Handles btnModificacion.Click
        ExecutarModificacion()
    End Sub

    ''' <summary>
    ''' Clique botão Consulta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnConsulta_Click(sender As Object, e As EventArgs) Handles btnConsulta.Click
        ExecutarConsulta()
    End Sub

    ''' <summary>
    ''' Clique Botão Volver
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        ExecutarVolver()
    End Sub

    ''' <summary>
    ''' Clique botão Cancelar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        ExecutarCancelar()
    End Sub

    ''' <summary>
    ''' Clique botão Grabar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()
    End Sub

    ''' <summary>
    ''' Clique botão Acima
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAcima_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAcima.Click

        Dim strCodigoTerminoSelecionado As String = ProsegurGridView1.getValorLinhaSelecionada

        Dim objTerminoMedioPago As New IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago
        objTerminoMedioPago = RetornaTerminoMedioPagoExiste(TerminoMediosPagoTemporario, ProsegurGridView1.getValorLinhaSelecionada)

        Dim objPosterior As ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago = Nothing
        For Each objLinha In TerminoMediosPagoTemporario
            If objLinha.Codigo.Equals(strCodigoTerminoSelecionado) AndAlso objPosterior IsNot Nothing Then
                Dim auxSelecionado As Integer = objLinha.OrdenTermino
                objLinha.OrdenTermino = objPosterior.OrdenTermino
                objPosterior.OrdenTermino = auxSelecionado
            End If
            objPosterior = objLinha
        Next

        'Carrega os Terminos no GridView        
        TerminoMediosPagoTemporario = OrdenaColecao(TerminoMediosPagoTemporario)
        Dim objDT As DataTable
        objDT = ProsegurGridView1.ConvertListToDataTable(TerminoMediosPagoTemporario)
        ProsegurGridView1.CarregaControle(objDT, False, False)

    End Sub

    ''' <summary>
    ''' Clique botão Abaixo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAbaixo_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAbaixo.Click

        Dim strCodigoTerminoSelecionado As String = ProsegurGridView1.getValorLinhaSelecionada

        Dim objTerminoMedioPago As New IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago
        objTerminoMedioPago = RetornaTerminoMedioPagoExiste(TerminoMediosPagoTemporario, ProsegurGridView1.getValorLinhaSelecionada)

        Dim objInferior As ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago = Nothing
        Dim objCorrente As ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago = Nothing
        Dim bolSairProximo As Boolean = False
        For Each objLinha In TerminoMediosPagoTemporario

            If bolSairProximo Then
                objInferior = objLinha
                Exit For
            End If

            If objLinha.Codigo.Equals(strCodigoTerminoSelecionado) Then
                objCorrente = objLinha
                bolSairProximo = True
            End If
        Next

        If objCorrente IsNot Nothing AndAlso objInferior IsNot Nothing Then
            Dim auxSelecionado As Integer = objCorrente.OrdenTermino
            objCorrente.OrdenTermino = objInferior.OrdenTermino
            objInferior.OrdenTermino = auxSelecionado
        End If

        'Carrega os Terminos no GridView
        TerminoMediosPagoTemporario = OrdenaColecao(TerminoMediosPagoTemporario)
        Dim objDT As DataTable
        objDT = ProsegurGridView1.ConvertListToDataTable(TerminoMediosPagoTemporario)
        ProsegurGridView1.CarregaControle(objDT, False, False)

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
    Public Sub ExecutarGrabar()
        Try

            Dim objProxyMedioPago As New Comunicacion.ProxyMedioPago
            Dim objPeticionMedioPago As New IAC.ContractoServicio.MedioPago.SetMedioPago.Peticion
            Dim objRespuestaMedioPago As IAC.ContractoServicio.MedioPago.SetMedioPago.Respuesta
            Dim strErro As New Text.StringBuilder(String.Empty)

            ValidarCamposObrigatorios = True

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If


            Dim objMedioPago As New IAC.ContractoServicio.MedioPago.SetMedioPago.MedioPago

            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Acao = eAcaoEspecifica.AltaConRegistro OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar Then
                objMedioPago.Vigente = True
            Else
                objMedioPago.Vigente = chkVigente.Checked
            End If

            objMedioPago.Codigo = txtCodigoMedioPago.Text.Trim
            objMedioPago.Descripcion = txtDescricaoMedioPago.Text.Trim
            objMedioPago.Observaciones = txtObservaciones.Text
            objMedioPago.CodigoDivisa = ddlDivisa.SelectedValue
            objMedioPago.CodigoTipoMedioPago = ddlTipoMedioPago.SelectedValue
            objMedioPago.CodigoAccesoMedioPago = txtCodigoAcceso.Text.Trim()
            objMedioPago.TerminosMedioPago = RetornaTerminosMediosPagoEnvio(TerminoMediosPagoTemporario, TerminoMediosPagoClone)
            objMedioPago.EsMercancia = chkMercancia.Checked

            'Cria a coleção
            Dim objColMedioPago As New IAC.ContractoServicio.MedioPago.SetMedioPago.MedioPagoColeccion
            objColMedioPago.Add(objMedioPago)

            objPeticionMedioPago.MedioPagos = objColMedioPago
            objPeticionMedioPago.CodigoUsuario = MyBase.LoginUsuario

            objRespuestaMedioPago = objProxyMedioPago.SetMedioPago(objPeticionMedioPago)

            'Define a ação de busca somente se houve retorno de MediosPago 
            If Master.ControleErro.VerificaErro(objRespuestaMedioPago.CodigoError, objRespuestaMedioPago.NombreServidorBD, objRespuestaMedioPago.MensajeError) Then

                If Master.ControleErro.VerificaErro(objRespuestaMedioPago.RespuestaMedioPagos(0).CodigoError, objRespuestaMedioPago.NombreServidorBD, objRespuestaMedioPago.RespuestaMedioPagos(0).MensajeError) Then
                    Response.Redirect("~/BusquedaMediosPago.aspx", False)
                End If

            Else

                If objRespuestaMedioPago.RespuestaMedioPagos IsNot Nothing _
                    AndAlso objRespuestaMedioPago.RespuestaMedioPagos.Count > 0 _
                    AndAlso objRespuestaMedioPago.RespuestaMedioPagos(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then

                    Master.ControleErro.ShowError(objRespuestaMedioPago.RespuestaMedioPagos(0).MensajeError, False)

                ElseIf objRespuestaMedioPago.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT Then

                    Master.ControleErro.ShowError(objRespuestaMedioPago.MensajeError, False)

                End If

            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Função do Botão Cancelar
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarCancelar()
        Response.Redirect("~/BusquedaMediosPago.aspx", False)
    End Sub

    ''' <summary>
    ''' Função do Botão Volver
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarVolver()
        Response.Redirect("~/BusquedaMediosPago.aspx", False)
    End Sub

    ''' <summary>
    ''' Função do Botão Baja
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarBaja()
        Try

            'Retorna o valor da linha selecionada no grid
            Dim strCodigo As String = ProsegurGridView1.getValorLinhaSelecionada

            'Modifica o Termino Medio de Pago para exclusão
            Dim objTerminoRetorno As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago = RetornaTerminoMedioPagoExiste(TerminoMediosPagoTemporario, strCodigo)
            objTerminoRetorno.Vigente = False

            'Carrega os Terminos no GridView
            TerminoMediosPagoTemporario = OrdenaColecao(TerminoMediosPagoTemporario)
            Dim objDT As DataTable
            objDT = ProsegurGridView1.ConvertListToDataTable(TerminoMediosPagoTemporario)
            ProsegurGridView1.CarregaControle(objDT)


        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Função do Botão Alta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarAlta()
        Try


            Dim url As String = "MantenimientoTerminoMediosPago.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&codMedioPago=" & txtCodigoMedioPago.Text.Trim()

            'Seta a session com a coleção de TerminoMedioPago que será consmida na abertura da PopUp de Mantenimiento de TerminosMedioPago
            SetTerminosMedioPagoColecaoPopUp()

            ' limpar sessao das telas seguintes
            Session("objColValorTerminoMedioPago") = Nothing

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_TerminoMedioPago", "AbrirPopupModal('" & url & "', 465, 788,'MedioPagoAlta');", True)


        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Função do Botão Consulta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarConsulta()
        Try
            Dim url As String = "MantenimientoTerminoMediosPago.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta

            'Seta a session com o TerminoMedioPago que será consmida na abertura da PopUp de Mantenimiento de TerminosMedioPago
            SetTerminoMedioPagoSelecionadoPopUp()

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_TerminoMedioPago", "AbrirPopupModal('" & url & "', 465, 788,'MedioPagoConsulta');", True)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Função do Botão Modificacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarModificacion()
        Try

            Dim url As String = "MantenimientoTerminoMediosPago.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Modificacion

            'Seta a session com o TerminoMedioPago que será consmida na abertura da PopUp de Mantenimiento de TerminosMedioPago
            SetTerminoMedioPagoSelecionadoPopUp()

            'Seta a session com a coleção de TerminoMedioPago que será consmida na abertura da PopUp de Mantenimiento de TerminosMedioPago
            SetTerminosMedioPagoColecaoPopUp()

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_TerminoMedioPago", "AbrirPopupModal('" & url & "', 465, 788,'MedioPagoModificacion');", True)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

#End Region

#End Region

#Region "[EVENTOS TEXTBOX]"

    ''' <summary>
    ''' Evento de mudança de texto do campo Código Medio Pago
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtCodigoMedioPago_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoMedioPago.TextChanged
        Try

            If ExisteCodigoMedioPago(txtCodigoMedioPago.Text, ddlDivisa.SelectedValue) Then
                CodigoExistente = True
            Else
                CodigoExistente = False
            End If

            Threading.Thread.Sleep(100)
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Private Sub txtCodigoAcceso_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoAcceso.TextChanged

        Try

            If ExisteCodigoAccesoMedioPago(txtCodigoAcceso.Text, ddlDivisa.SelectedValue) Then
                CodigoAccesoExistente = True
            Else
                CodigoAccesoExistente = False
            End If

            Threading.Thread.Sleep(100)
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Private Sub txtDescricaoMedioPago_TextChanged(sender As Object, e As System.EventArgs) Handles txtDescricaoMedioPago.TextChanged
        Threading.Thread.Sleep(100)
    End Sub

#End Region

#Region "[DROPDOWN LIST]"

    Protected Sub ddlTipoMedioPago_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoMedioPago.SelectedIndexChanged

        Try

            ddlTipoMedioPago.ToolTip = ddlTipoMedioPago.SelectedItem.Text

            If ExisteCodigoMedioPago(txtCodigoMedioPago.Text, ddlDivisa.SelectedValue) Then
                CodigoExistente = True
            Else
                CodigoExistente = False
            End If

            Threading.Thread.Sleep(100)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Protected Sub ddlDivisa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDivisa.SelectedIndexChanged

        Try

            ddlDivisa.ToolTip = ddlDivisa.SelectedItem.Text

            If ExisteCodigoMedioPago(txtCodigoMedioPago.Text, ddlDivisa.SelectedValue) Then
                CodigoExistente = True
            Else
                CodigoExistente = False
            End If

            Threading.Thread.Sleep(100)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

#End Region

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Public Sub PreencherComboDivisa()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboDivisas.Respuesta
        objRespuesta = objProxyUtilida.GetComboDivisas

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ddlDivisa.AppendDataBoundItems = True
        ddlDivisa.Items.Clear()
        ddlDivisa.Items.Add(New ListItem(Traduzir("014_ddl_selecione"), String.Empty))
        ddlDivisa.DataTextField = "Descripcion"
        ddlDivisa.DataValueField = "CodigoIso"
        ddlDivisa.DataSource = objRespuesta.Divisas
        ddlDivisa.DataBind()

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Public Sub PreencherComboTipoMedioPago()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboTiposMedioPago.Respuesta
        objRespuesta = objProxyUtilida.GetComboTiposMedioPago

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ddlTipoMedioPago.AppendDataBoundItems = True
        ddlTipoMedioPago.Items.Clear()
        ddlTipoMedioPago.Items.Add(New ListItem(Traduzir("014_ddl_selecione"), String.Empty))
        ddlTipoMedioPago.DataTextField = "Descripcion"
        ddlTipoMedioPago.DataValueField = "Codigo"
        ddlTipoMedioPago.DataSource = objRespuesta.TiposMedioPago
        ddlTipoMedioPago.DataBind()

    End Sub

    ''' <summary>
    ''' Carrega os dados do gridView quando a página é carregada pela primeira vez
    ''' </summary>
    ''' <param name="codMedioPago"></param>
    ''' <remarks></remarks>
    Public Sub CarregaDados(codMedioPago As String, codDivisaMedioPago As String, codTipoMedioPago As String)
        Dim itemSelecionado As ListItem
        Dim objColMedioPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.MedioPagoColeccion
        objColMedioPago = getMedioPagoDetail(codMedioPago, codDivisaMedioPago, codTipoMedioPago)

        If objColMedioPago.Count > 0 Then

            'Preenche os controles do formulario
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                txtCodigoMedioPago.Text = objColMedioPago(0).Codigo
                txtCodigoMedioPago.ToolTip = objColMedioPago(0).Codigo
                txtCodigoAcceso.Text = objColMedioPago.First.CodigoAccesoMedioPago
                txtCodigoAcceso.ToolTip = objColMedioPago.First.CodigoAccesoMedioPago
            ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar Then
                txtCodigoMedioPago.Text = String.Empty
                txtCodigoAcceso.Text = String.Empty
            End If

            txtDescricaoMedioPago.Text = objColMedioPago(0).Descripcion
            txtDescricaoMedioPago.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objColMedioPago(0).Descripcion, String.Empty)

            txtObservaciones.Text = objColMedioPago(0).Observaciones
            chkMercancia.Checked = objColMedioPago(0).EsMercancia
            chkVigente.Checked = objColMedioPago(0).Vigente

            ' preenche a propriedade da tela
            EsVigente = objColMedioPago(0).Vigente
            EsMercancia = objColMedioPago(0).EsMercancia

            'Seleciona o valor divisa
            itemSelecionado = ddlDivisa.Items.FindByValue(objColMedioPago(0).CodigoDivisa)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
                ddlDivisa.ToolTip = itemSelecionado.ToString
            End If

            'Seleciona o valor tipo de medio de pago
            itemSelecionado = ddlTipoMedioPago.Items.FindByValue(objColMedioPago(0).CodigoTipoMedioPago)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
                ddlTipoMedioPago.ToolTip = itemSelecionado.ToString
            End If

            'Se for modificação então guarda a descriçaõ atual para validação
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                DescricaoAtual = txtDescricaoMedioPago.Text
                CodigoAccesoAtual = txtCodigoAcceso.Text
            End If

            'Faz um clone da coleção de MedioPago e Cria o objeto temporario no mesmo instante
            TerminoMediosPagoClone = OrdenaColecao(objColMedioPago(0).TerminosMedioPago)

            'Carrega os TerminosMedioPago no GridView            
            Dim objDT As DataTable
            objDT = ProsegurGridView1.ConvertListToDataTable(TerminoMediosPagoClone)
            ProsegurGridView1.CarregaControle(objDT)

        End If


    End Sub

    ''' <summary>
    ''' Ordena a coleção de términos pelo número ordem informado
    ''' </summary>
    ''' <param name="objColecao"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function OrdenaColecao(objColecao As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion) As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion

        Dim retorno = From objeto In objColecao Order By objeto.OrdenTermino
        Dim objTerminoMedioPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago = Nothing
        Dim objColTerminoMedioPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion = Nothing

        For Each objRetorno As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago In retorno


            'Cria o objeto de termino de envio
            objTerminoMedioPago = New IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago
            objTerminoMedioPago.Codigo = objRetorno.Codigo
            objTerminoMedioPago.Descripcion = objRetorno.Descripcion
            objTerminoMedioPago.Observacion = objRetorno.Observacion
            objTerminoMedioPago.Vigente = objRetorno.Vigente

            objTerminoMedioPago.Longitud = objRetorno.Longitud
            objTerminoMedioPago.MostrarCodigo = objRetorno.MostrarCodigo
            objTerminoMedioPago.ValorInicial = objRetorno.ValorInicial
            objTerminoMedioPago.OrdenTermino = objRetorno.OrdenTermino

            objTerminoMedioPago.CodigoFormato = objRetorno.CodigoFormato
            objTerminoMedioPago.DescripcionFormato = objRetorno.DescripcionFormato

            objTerminoMedioPago.CodigoMascara = objRetorno.CodigoMascara
            objTerminoMedioPago.DescripcionMascara = objRetorno.DescripcionMascara

            objTerminoMedioPago.CodigoAlgoritmo = objRetorno.CodigoAlgoritmo
            objTerminoMedioPago.DescripcionAlgoritmo = objRetorno.DescripcionAlgoritmo

            objTerminoMedioPago.ValoresTermino = objRetorno.ValoresTermino

            If objColTerminoMedioPago Is Nothing Then
                objColTerminoMedioPago = New IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion
                objColTerminoMedioPago.Add(objTerminoMedioPago)
            Else
                objColTerminoMedioPago.Add(objTerminoMedioPago)
            End If
        Next

        Return objColTerminoMedioPago

    End Function

    ''' <summary>
    ''' Consome o TerminoMedioPago passado pela PopUp de Mantenimiento de TerminosMedioPago. 
    ''' Obs: O TerminoMedioPago só pode ser consumido no modo "Alta" ou "Modificacion"
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeTerminoMedioPago()

        If Session("objTerminoMedioPago") IsNot Nothing Then

            Dim objTerminoMedioPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago
            objTerminoMedioPago = DirectCast(Session("objTerminoMedioPago"), IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago)

            'Se existe o TerminoMedioPago na coleção
            If Not VerificarTerminoMedioPagoExiste(TerminoMediosPagoTemporario, objTerminoMedioPago.Codigo) Then
                TerminoMediosPagoTemporario.Add(objTerminoMedioPago)
            Else
                ModificaTerminoMedioPago(TerminoMediosPagoTemporario, objTerminoMedioPago)
            End If

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar Then
                'Seta o estado da página corrente para modificação
                Acao = eAcaoEspecifica.AltaConRegistro
            End If

            'cria um objeto temporario somente para preencher o grid, pois o metodo que faz a conversão da coleção de terminos para um datatable
            'não aceita variavel inteira como nothing.
            Dim objColTerminos As ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion

            objColTerminos = TerminoMediosPagoTemporario

            If objColTerminos IsNot Nothing AndAlso objColTerminos.Count > 0 Then

                For Each objTermino In objColTerminos
                    If Not objTermino.Longitud.HasValue Then
                        objTermino.Longitud = 0
                    End If
                Next

            End If

            'Carrega os TerminosMedioPago no GridView
            Dim objDT As DataTable
            objDT = ProsegurGridView1.ConvertListToDataTable(objColTerminos)
            ProsegurGridView1.CarregaControle(objDT)

            Session("objTerminoMedioPago") = Nothing

        End If

    End Sub

    ''' <summary>
    ''' Verifica se um Termino de MedioPago especifico existe na coleção informada
    ''' </summary>
    ''' <param name="objTerminoMediosPago"></param>
    ''' <param name="codigoTerminoMedioPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>    
    Private Function VerificarTerminoMedioPagoExiste(objTerminoMediosPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion, codigoTerminoMedioPago As String) As Boolean

        Dim retorno = From c In objTerminoMediosPago Where c.Codigo = codigoTerminoMedioPago

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' ' Retorna uma coleção de terminos que tem a mais no objeto temporario em relação ao clone e o que é foi modificado no temporario em relação ao clone
    ''' </summary>
    ''' <param name="objTerminoMediosPagoTemporario"></param>
    ''' <param name="objTerminoMediosPagoClone"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetornaTerminosMediosPagoEnvio(objTerminoMediosPagoTemporario As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion, objTerminoMediosPagoClone As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion) As IAC.ContractoServicio.MedioPago.SetMedioPago.TerminoMedioPagoColeccion

        Dim retorno As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion = objTerminoMediosPagoTemporario

        Dim objTerminoMedioPagoCol As New IAC.ContractoServicio.MedioPago.SetMedioPago.TerminoMedioPagoColeccion

        Dim objTerminoMedioPagoEnvio As IAC.ContractoServicio.MedioPago.SetMedioPago.TerminoMedioPago
        For Each objRetorno As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago In retorno

            'Cria o objeto de termino de envio
            objTerminoMedioPagoEnvio = New IAC.ContractoServicio.MedioPago.SetMedioPago.TerminoMedioPago
            objTerminoMedioPagoEnvio.Codigo = objRetorno.Codigo
            objTerminoMedioPagoEnvio.Descripcion = objRetorno.Descripcion
            objTerminoMedioPagoEnvio.Observacion = objRetorno.Observacion
            objTerminoMedioPagoEnvio.Vigente = objRetorno.Vigente

            objTerminoMedioPagoEnvio.Longitud = objRetorno.Longitud
            objTerminoMedioPagoEnvio.MostrarCodigo = objRetorno.MostrarCodigo
            objTerminoMedioPagoEnvio.ValorInicial = objRetorno.ValorInicial
            objTerminoMedioPagoEnvio.OrdenTermino = objRetorno.OrdenTermino

            objTerminoMedioPagoEnvio.CodigoFormato = objRetorno.CodigoFormato
            objTerminoMedioPagoEnvio.CodigoMascara = objRetorno.CodigoMascara
            objTerminoMedioPagoEnvio.CodigoAlgoritmo = objRetorno.CodigoAlgoritmo

            If objRetorno.ValoresTermino IsNot Nothing Then
                objTerminoMedioPagoEnvio.ValoresTermino = New ContractoServicio.MedioPago.SetMedioPago.ValorTerminoColeccion

                Dim objValorEnvio As ContractoServicio.MedioPago.SetMedioPago.ValorTermino
                For Each objValor As ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTermino In objRetorno.ValoresTermino
                    'Cria o objeto de valor de envio
                    objValorEnvio = New ContractoServicio.MedioPago.SetMedioPago.ValorTermino
                    objValorEnvio.Codigo = objValor.Codigo
                    objValorEnvio.Descripcion = objValor.Descripcion
                    objValorEnvio.Vigente = objValor.Vigente

                    'Adiciona no termino de envio
                    objTerminoMedioPagoEnvio.ValoresTermino.Add(objValorEnvio)
                Next

            End If

            'Adiciona na coleção
            objTerminoMedioPagoCol.Add(objTerminoMedioPagoEnvio)
        Next

        Return objTerminoMedioPagoCol

    End Function

    ''' <summary>
    ''' Modifica um Medio de Pago existe na coleção informada
    ''' </summary>
    ''' <param name="objTerminoMediosPago"></param>
    ''' <param name="objTerminoMedioPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ModificaTerminoMedioPago(ByRef objTerminoMediosPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion, objTerminoMedioPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago) As Boolean

        Dim retorno = From c In objTerminoMediosPago Where c.Codigo = objTerminoMedioPago.Codigo

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else

            Dim objTerminoMedioPagoTmp As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago = DirectCast(retorno.ElementAt(0), IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago)

            'Campos Texto
            objTerminoMedioPagoTmp.Descripcion = objTerminoMedioPago.Descripcion
            objTerminoMedioPagoTmp.Observacion = objTerminoMedioPago.Observacion
            objTerminoMedioPagoTmp.ValorInicial = objTerminoMedioPago.ValorInicial
            objTerminoMedioPagoTmp.Longitud = objTerminoMedioPago.Longitud
            'Campos CheckBox            
            objTerminoMedioPagoTmp.MostrarCodigo = objTerminoMedioPago.MostrarCodigo
            objTerminoMedioPagoTmp.Vigente = objTerminoMedioPago.Vigente
            'Campos DropDownList
            objTerminoMedioPagoTmp.CodigoFormato = objTerminoMedioPago.CodigoFormato
            objTerminoMedioPagoTmp.CodigoMascara = objTerminoMedioPago.CodigoMascara
            objTerminoMedioPagoTmp.CodigoAlgoritmo = objTerminoMedioPago.CodigoAlgoritmo
            objTerminoMedioPagoTmp.DescripcionFormato = objTerminoMedioPago.DescripcionFormato
            objTerminoMedioPagoTmp.DescripcionAlgoritmo = objTerminoMedioPago.DescripcionAlgoritmo
            objTerminoMedioPagoTmp.DescripcionMascara = objTerminoMedioPago.DescripcionMascara
            'Demais Campos
            objTerminoMedioPagoTmp.OrdenTermino = objTerminoMedioPago.OrdenTermino

            'Valores de Termino
            objTerminoMedioPagoTmp.ValoresTermino = objTerminoMedioPago.ValoresTermino

            Return True
        End If

    End Function

    ''' <summary>
    ''' Retorna um TerminoMedioPago da coleção informada    
    ''' </summary>
    ''' <param name="objTerminoMediosPago"></param>
    ''' <param name="codigoTerminoMedioPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetornaTerminoMedioPagoExiste(ByRef objTerminoMediosPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion, codigoTerminoMedioPago As String) As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago

        Dim retorno = From c In objTerminoMediosPago Where c.Codigo = codigoTerminoMedioPago

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return Nothing
        Else
            Return retorno.ElementAt(0)
        End If

    End Function

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
    ''' Seta o termino para a PopUp que será aberta
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTerminoMedioPagoSelecionadoPopUp()

        'Cria o MedioPago para ser consumido na página de TerminosMedioPago
        Dim objTerminoMedioPago As New IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago
        objTerminoMedioPago = RetornaTerminoMedioPagoExiste(TerminoMediosPagoTemporario, ProsegurGridView1.getValorLinhaSelecionada)

        'Envia o TerminoMedioPago para ser consumido pela PopUp de Mantenimento de TerminoMedioPago
        Session("setTerminoMedioPago") = objTerminoMedioPago

    End Sub

    ''' <summary>
    ''' Seta a coleção de Terminos em memória para a PopUp que será aberta
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTerminosMedioPagoColecaoPopUp()

        'Passa a coleção com o objeto temporario de Terminos
        Session("colTerminosMedioPago") = TerminoMediosPagoTemporario

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

            If ValidarCamposObrigatorios Then

                'Verifica se o campo é obrigatório
                'quando o botão salvar é acionado
                'Verifica o tipo medio pago
                If ddlTipoMedioPago.SelectedValue.Trim.Equals(String.Empty) Then

                    strErro.Append(csvTipoMedioPagoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTipoMedioPagoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlTipoMedioPago.Focus()
                        focoSetado = True
                    End If

                Else
                    csvTipoMedioPagoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If ddlDivisa.SelectedValue.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDivisaObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDivisaObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlDivisa.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDivisaObrigatorio.IsValid = True
                End If

                'Verifica se o código do canal é obrigatório
                If txtCodigoMedioPago.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoMedioPago.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If txtDescricaoMedioPago.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoMedioPago.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If txtCodigoAcceso.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoAccesoObligatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoAccesoObligatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoAcceso.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoAccesoObligatorio.IsValid = True
                End If

            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If CodigoExistente Then

                strErro.Append(csvCodigoMedioPagoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoMedioPagoExistente.IsValid = False
                csvDivisaMedioPagoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoMedioPago.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoMedioPagoExistente.IsValid = True
                csvTipoMedioPagoExistente.IsValid = True
                csvDivisaMedioPagoExistente.IsValid = True
            End If

            'Verifica se o código existe
            If CodigoAccesoExistente Then

                strErro.Append(csvCodigoAccesoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoAccesoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoAcceso.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoAccesoExistente.IsValid = True
            End If
            'If CodigoExistente Then

            '    strErro.Append(csvTipoMedioPagoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)


            '    'Seta o foco no primeiro controle que deu erro
            '    If SetarFocoControle AndAlso Not focoSetado Then
            '        ddlTipoMedioPago.Focus()
            '        focoSetado = True
            '    End If

            'Else

            'End If

            'If CodigoExistente Then

            '    strErro.Append(csvDivisaMedioPagoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)


            '    'Seta o foco no primeiro controle que deu erro
            '    If SetarFocoControle AndAlso Not focoSetado Then
            '        ddlDivisa.Focus()
            '        focoSetado = True
            '    End If

            'Else

            'End If

        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' Informa se o código do canal já existe na base de dados para o Código Medio Pago e a divisa selecionada.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.frga] 14/03/2011 - Criado
    ''' </history>
    Private Function ExisteCodigoMedioPago(codigoMedioPago As String, divisa As String) As Boolean

        Return ExisteCodigoMedioPago(codigoMedioPago, divisa, Nothing)

    End Function


    ''' <summary>
    ''' Informa se o código do canal já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/07/2009 - Criado
    ''' </history>
    Private Function ExisteCodigoMedioPago(codigoMedioPago As String, divisa As String, tipoMedioPago As String) As Boolean

        ' Se o tipo medio pago deve ser considerado como obrigatório
        Dim blTipoMedioPagoPreenchido As Boolean = True
        If tipoMedioPago IsNot Nothing Then
            blTipoMedioPagoPreenchido = Not String.IsNullOrEmpty(ddlTipoMedioPago.SelectedValue)
        End If

        If Not String.IsNullOrEmpty(txtCodigoMedioPago.Text.Trim()) AndAlso _
          Not String.IsNullOrEmpty(ddlDivisa.SelectedValue) AndAlso _
          blTipoMedioPagoPreenchido Then

            Dim objRespostaVerificarCodigoMedioPago As IAC.ContractoServicio.MedioPago.VerificarCodigoMedioPago.Respuesta
            Try

                Dim objProxyMedioPago As New Comunicacion.ProxyMedioPago
                Dim objPeticionVerificarCodigoMedioPago As New IAC.ContractoServicio.MedioPago.VerificarCodigoMedioPago.Peticion

                'Verifica se o código do MedioPago existe no BD
                objPeticionVerificarCodigoMedioPago.Codigo = codigoMedioPago.Trim
                objPeticionVerificarCodigoMedioPago.Divisa = divisa.Trim

                'Verifica se o tipo medio pago vai fazer parte da chave de busca
                If tipoMedioPago IsNot Nothing Then
                    objPeticionVerificarCodigoMedioPago.Tipo = tipoMedioPago.Trim
                End If

                objRespostaVerificarCodigoMedioPago = objProxyMedioPago.VerificarCodigoMedioPago(objPeticionVerificarCodigoMedioPago)

                If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoMedioPago.CodigoError, objRespostaVerificarCodigoMedioPago.NombreServidorBD, objRespostaVerificarCodigoMedioPago.MensajeError) Then
                    Return objRespostaVerificarCodigoMedioPago.Existe
                Else
                    Master.ControleErro.ShowError(objRespostaVerificarCodigoMedioPago.MensajeError)
                    Return False
                End If


            Catch ex As Exception
                Master.ControleErro.TratarErroException(ex)
            End Try

        End If

    End Function


    ''' <summary>
    ''' Informa se o código do canal já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/07/2009 - Criado
    ''' </history>
    Private Function ExisteCodigoAccesoMedioPago(codigoAccesoMedioPago As String, divisa As String) As Boolean

        If Not String.IsNullOrEmpty(txtCodigoAcceso.Text.Trim()) AndAlso _
          Not String.IsNullOrEmpty(ddlDivisa.SelectedValue) Then

            Dim objRespostaVerificarCodigoAccesoMedioPago As IAC.ContractoServicio.Utilidad.VerificarCodigoAccesoMedioPago.Respuesta

            Try

                Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
                Dim objPeticionVerificarCodigoAccesoMedioPago As New IAC.ContractoServicio.Utilidad.VerificarCodigoAccesoMedioPago.Peticion

                'Verifica se o código do MedioPago existe no BD
                objPeticionVerificarCodigoAccesoMedioPago.CodigoAcceso = codigoAccesoMedioPago.Trim
                objPeticionVerificarCodigoAccesoMedioPago.CodigoDivisa = divisa.Trim

                objRespostaVerificarCodigoAccesoMedioPago = objProxyUtilidad.VerificarCodigoAccesoMedioPago(objPeticionVerificarCodigoAccesoMedioPago)

                If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoAccesoMedioPago.CodigoError, objRespostaVerificarCodigoAccesoMedioPago.NombreServidorBD, objRespostaVerificarCodigoAccesoMedioPago.MensajeError) Then
                    Return objRespostaVerificarCodigoAccesoMedioPago.Existe
                Else
                    Master.ControleErro.ShowError(objRespostaVerificarCodigoAccesoMedioPago.MensajeError)
                    Return False
                End If


            Catch ex As Exception
                Master.ControleErro.TratarErroException(ex)
            End Try

        End If

    End Function
#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()

        ' parámetro MantenimientoClientesDivisasPorPantalla = Falso –, la pantalla permitirá solamente:
        '-	Consulta y 
        '-	Modificación solamente de los códigos ajenos, totalizadores de saldo y el tipo del Cliente.
        Dim ParametroMantenimientoClientesDivisasPorPantalla As Boolean = Prosegur.Genesis.Parametros.Genesis.Parametros.ParametrosIntegracion.Where(Function(o) o.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_MANTENIMIENTO_CLIENTES_DIVISAS_POR_PANTALLA).First.ValorParametro

        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta
                btnAlta.Visible = True          '1
                btnGrabar.Visible = True        '2
                btnCancelar.Visible = True      '3

                'Estado Inicial Controles                                
                txtCodigoMedioPago.Enabled = ParametroMantenimientoClientesDivisasPorPantalla

                btnBaja.Visible = False         '4
                btnModificacion.Visible = False '5
                btnConsulta.Visible = False     '6 
                btnVolver.Visible = False       '7
                btnAcima.Visible = False        '8
                btnAbaixo.Visible = False       '9

                chkMercancia.Checked = False
                chkMercancia.Enabled = Me.ddlTipoMedioPago.SelectedValue = ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_OTROSVALORES

                lblVigente.Visible = False
                chkVigente.Checked = True
                chkVigente.Enabled = False
                chkVigente.Visible = False

                btnGrabar.Habilitado = True
                btnAlta.Habilitado = True
                Me.txtDescricaoMedioPago.Enabled = ParametroMantenimientoClientesDivisasPorPantalla
                Me.txtCodigoMedioPago.Enabled = ParametroMantenimientoClientesDivisasPorPantalla
                Me.ddlDivisa.Enabled = ParametroMantenimientoClientesDivisasPorPantalla
                Me.ddlTipoMedioPago.Enabled = ParametroMantenimientoClientesDivisasPorPantalla

            Case Aplicacao.Util.Utilidad.eAcao.Duplicar
                btnAlta.Visible = True          '1
                btnGrabar.Visible = True        '2
                btnCancelar.Visible = True      '3
                btnVolver.Visible = False       '4

                'Estado Inicial Controles                                
                txtCodigoMedioPago.Enabled = True

                If TerminoMediosPagoTemporario.Count > 0 Then
                    btnBaja.Visible = True             '5
                    btnConsulta.Visible = True         '6
                    btnModificacion.Visible = True     '7
                    btnAcima.Visible = True            '8
                    btnAbaixo.Visible = True           '9
                Else
                    btnBaja.Visible = False
                    btnConsulta.Visible = False
                    btnModificacion.Visible = False
                    btnAcima.Visible = False
                    btnAbaixo.Visible = False
                End If

                chkMercancia.Checked = False
                chkMercancia.Enabled = Me.ddlTipoMedioPago.SelectedValue = ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_OTROSVALORES

                chkVigente.Checked = True
                chkVigente.Enabled = False
                lblVigente.Visible = False

                btnGrabar.Habilitado = True
                btnAlta.Habilitado = True

            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                btnAlta.Visible = False                  '1
                btnCancelar.Visible = False              '2

                If TerminoMediosPagoTemporario.Count > 0 Then
                    btnConsulta.Visible = True          '3
                Else
                    btnConsulta.Visible = False
                End If
                btnVolver.Visible = True                '4

                'Estado Inicial Controles
                txtCodigoMedioPago.Enabled = False
                txtDescricaoMedioPago.Enabled = False
                txtObservaciones.Enabled = False
                chkMercancia.Enabled = False
                lblVigente.Visible = True
                chkVigente.Enabled = False
                ddlDivisa.Enabled = False
                ddlTipoMedioPago.Enabled = False

                btnBaja.Visible = False                 '5
                btnModificacion.Visible = False         '6
                btnGrabar.Visible = False               '7
                btnAcima.Visible = False                 '8
                btnAbaixo.Visible = False                '9

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                txtCodigoMedioPago.Enabled = False
                chkVigente.Visible = True

                btnAlta.Visible = True                 '2
                If TerminoMediosPagoTemporario.Count > 0 Then
                    btnBaja.Visible = True             '3
                    btnConsulta.Visible = True         '4
                    btnModificacion.Visible = True     '1
                    btnAcima.Visible = True            '8
                    btnAbaixo.Visible = True           '9
                Else
                    btnBaja.Visible = False
                    btnConsulta.Visible = False
                    btnModificacion.Visible = False    '1
                    btnAcima.Visible = False            '8
                    btnAbaixo.Visible = False           '9
                End If

                btnGrabar.Visible = True               '5
                btnCancelar.Visible = True             '6
                btnVolver.Visible = False              '7

                ' se estiver checado e objeto for mercancia
                If chkMercancia.Checked AndAlso EsMercancia Then
                    chkMercancia.Enabled = False
                Else
                    chkMercancia.Enabled = True
                End If

                lblVigente.Visible = True
                ' se estiver checado e objeto for vigente
                If chkVigente.Checked AndAlso EsVigente Then
                    chkVigente.Enabled = False
                Else
                    chkVigente.Enabled = True
                End If

                btnGrabar.Habilitado = True
                btnAlta.Habilitado = True
                btnModificacion.Habilitado = True
                btnConsulta.Habilitado = True
                btnBaja.Habilitado = True

                'TODO: Verificar definição espanha sobre bloqueio dos campos abaixo
                ddlDivisa.Enabled = False
                ddlTipoMedioPago.Enabled = False
                Me.txtDescricaoMedioPago.Enabled = ParametroMantenimientoClientesDivisasPorPantalla

            Case Aplicacao.Util.Utilidad.eAcao.NoAction
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Busca
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.NoAction

            Case eAcaoEspecifica.AltaConRegistro

                btnModificacion.Visible = True         '1                
                chkVigente.Visible = True

                btnAlta.Visible = True                 '2
                If TerminoMediosPagoTemporario.Count > 0 Then
                    btnBaja.Visible = True             '3
                    btnConsulta.Visible = True         '4
                Else
                    btnBaja.Visible = False
                    btnConsulta.Visible = False
                End If


                btnGrabar.Habilitado = True
                btnAlta.Habilitado = True
                btnModificacion.Habilitado = True
                btnConsulta.Habilitado = True
                btnBaja.Habilitado = True

                '5
                btnCancelar.Visible = True             '6
                btnVolver.Visible = False              '7
                btnAcima.Visible = True                '8
                btnAbaixo.Visible = True               '9

                chkVigente.Enabled = False
                chkVigente.Visible = False
                chkVigente.Checked = True


            Case Aplicacao.Util.Utilidad.eAcao.Erro
                btnAlta.Visible = False          '1
                btnGrabar.Visible = False        '2
                btnCancelar.Visible = False      '3
                btnBaja.Visible = False          '4
                btnModificacion.Visible = False  '5
                btnConsulta.Visible = False      '6 
                btnVolver.Visible = True         '7

        End Select

        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem de erro
        If MontaMensagensErro() <> String.Empty Then
            Master.ControleErro.ShowError(MontaMensagensErro, False)
        End If

    End Sub

    Private Enum eAcaoEspecifica As Integer
        AltaConRegistro = 20
    End Enum

#End Region

#Region "[PROPRIEDADES]"

    Public Property TerminoMediosPagoTemporario() As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion
        Get
            If ViewState("TerminoMediosPagoTemporario") Is Nothing Then
                ViewState("TerminoMediosPagoTemporario") = New IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion
            End If

            Return DirectCast(ViewState("TerminoMediosPagoTemporario"), IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion)
        End Get
        Set(value As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion)
            ViewState("TerminoMediosPagoTemporario") = value
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

    Private Property CodigoAccesoAtual() As String
        Get
            Return ViewState("CodigoAccesoAtual")
        End Get
        Set(value As String)
            ViewState("CodigoAccesoAtual") = value.Trim
        End Set
    End Property

    Public Property TerminoMediosPagoClone() As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion
        Get
            If ViewState("TerminoMediosPagoClone") Is Nothing Then
                ViewState("TerminoMediosPagoClone") = New IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion
            End If

            Return DirectCast(ViewState("TerminoMediosPagoClone"), IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion)
        End Get
        Set(value As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion)
            ViewState("TerminoMediosPagoClone") = value
            ViewState("TerminoMediosPagoTemporario") = value
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

    Private Property CodigoAccesoExistente() As Boolean
        Get
            Return ViewState("CodigoAccesoExistente")
        End Get
        Set(value As Boolean)
            ViewState("CodigoAccesoExistente") = value
        End Set
    End Property

    Private Property EsMercancia() As Boolean
        Get
            If ViewState("EsMercancia") Is Nothing Then
                ViewState("EsMercancia") = False
            End If
            Return ViewState("EsMercancia")
        End Get
        Set(value As Boolean)
            ViewState("EsMercancia") = value
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

End Class