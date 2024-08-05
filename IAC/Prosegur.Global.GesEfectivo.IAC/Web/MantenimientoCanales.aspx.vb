Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' Página de Gerenciamento de Canais 
''' </summary>
''' <remarks></remarks>
''' <history>[PDA] 27/01/08 - Criado</history>
Partial Public Class MantenimientoCanales
    Inherits Base

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

        'Seta o foco para o proximo controle quando presciona o enter.

        txtCodigoCanal.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoCanal.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoCanal.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtObservaciones.ClientID & "').focus();return false;}} else {return true}; ")
        txtObservaciones.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onblur", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onkeyup", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")

        'Controle de precedência(Ajax)
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)

        'Validação Clique Botões do Grid 
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


    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigoCanal.TabIndex = 1
        txtDescricaoCanal.TabIndex = 2
        btnAjeno.TabIndex = 3
        txtObservaciones.TabIndex = 4
        chkVigente.TabIndex = 5
        btnAlta.TabIndex = 6
        btnBaja.TabIndex = 7
        btnModificacion.TabIndex = 8
        btnConsulta.TabIndex = 9
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
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CANALES

    End Sub

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                'Recebe o código do Canal

                Dim strCodCanal As String = Request.QueryString("codcanal")

                'Possíveis Ações passadas pela página BusquedaCanales:
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

                If strCodCanal <> String.Empty Then
                    'Estado Inicial dos control
                    CarregaDados(strCodCanal)
                End If

                If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    txtDescricaoCanal.Focus()
                Else
                    txtCodigoCanal.Focus()
                End If

            End If

            'Consome o SubCanal passado pela PopUp de "SubCanais"
            ConsomeSubCanal()

            'Consome o preenchimento de código ajeno
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

        Master.TituloPagina = Traduzir("001_titulo_mantenimiento_canales")
        lblCodigoCanal.Text = Traduzir("001_lbl_codigocanal")
        lblDescricaoCanal.Text = Traduzir("001_lbl_descricaocanal")
        lblVigente.Text = Traduzir("001_lbl_vigente")
        lblTituloCanales.Text = Traduzir("001_lbl_subtituloscanales")
        lblSubTitulosCanales.Text = Traduzir("001_lbl_subtitulossubcanales")
        lblObservaciones.Text = Traduzir("001_lbl_observaciones")
        lblCodigoAjeno.Text = Traduzir("019_lbl_codigoAjeno")
        lblDescriAjeno.Text = Traduzir("019_lbl_descricaoAjeno")

        ProsegurGridView1.PagerSummary = Traduzir("grid_lbl_pagersummary")

        csvCodigoObrigatorio.ErrorMessage = Traduzir("001_msg_canalcodigoobligatorio")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("001_msg_canaldescripcionobligatorio")
        csvDescripcionExistente.ErrorMessage = Traduzir("001_msg_descricaocanalexistente")
        csvCodigoCanalExistente.ErrorMessage = Traduzir("001_msg_codigocanalexistente")

        btnAjeno.ExibirLabelBtn = False
    End Sub

#End Region

#Region "[DADOS]"

    Public Function getSubCanalesByCanal(codigoCanal As String) As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.CanalColeccion

        Dim objProxyCanal As New Comunicacion.ProxyCanal
        Dim objPeticionCanal As New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.Peticion
        Dim objRespuestaCanal As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.Respuesta

        'Recebe os valores do filtro
        Dim lista As New List(Of String)
        lista.Add(codigoCanal)

        objPeticionCanal.codigoCanal = lista

        objRespuestaCanal = objProxyCanal.getSubCanalesByCanal(objPeticionCanal)

        Return objRespuestaCanal.Canales


    End Function

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS GRIDVIEW]"

    ''' <summary>
    ''' Evento ao criar as linhas do grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 05/03/2009 Criado
    ''' </history>
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

            objDT = ProsegurGridView1.ConvertListToDataTable(SubCanalesTemporario)

            objDT.DefaultView.Sort = ProsegurGridView1.SortCommand

            ProsegurGridView1.ControleDataSource = objDT

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

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
    ''' RowDataBound do GridView
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowDataBound

        Try

            'Índice das celulas do GridView Configuradas
            '0 - Código
            '1 - Descripción
            '2 - Observación
            '3 - Vigente

            If e.Row.DataItem IsNot Nothing Then

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                If Not e.Row.DataItem("descripcion") Is DBNull.Value AndAlso e.Row.DataItem("descripcion").Length > NumeroMaximoLinha Then
                    e.Row.Cells(1).Text = e.Row.DataItem("descripcion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                If Not e.Row.DataItem("observaciones") Is DBNull.Value AndAlso e.Row.DataItem("observaciones").Length > NumeroMaximoLinha Then
                    e.Row.Cells(2).Text = e.Row.DataItem("observaciones").ToString.Substring(0, NumeroMaximoLinha) & " ..."
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
    ''' RowCreated do GridView
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowCreated

        Try
            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("001_lbl_grd_codigo")
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("001_lbl_grd_descripcion")
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("001_lbl_grd_observacion")
                CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("001_lbl_grd_vigente")
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

#End Region

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Clique do botão Alta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAlta_Click(sender As Object, e As EventArgs) Handles btnAlta.Click
        ExecutarAlta()
    End Sub

    ''' <summary>
    ''' Clique do botão Baja
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click
        ExecutarBaja()
    End Sub

    ''' <summary>
    ''' Clique do botão Modificacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnModificacion_Click(sender As Object, e As EventArgs) Handles btnModificacion.Click
        ExecutarModificacion()
    End Sub

    ''' <summary>
    ''' Clique do botão Consulta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnConsulta_Click(sender As Object, e As EventArgs) Handles btnConsulta.Click
        'Threading.Thread.Sleep(2000)
        ExecutarConsulta()
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
        'Threading.Thread.Sleep(2000)
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

            Dim objProxyCanal As New Comunicacion.ProxyCanal
            Dim objPeticionCanal As New IAC.ContractoServicio.Canal.SetCanal.Peticion
            Dim objRespuestaCanal As IAC.ContractoServicio.Canal.SetCanal.Respuesta
            Dim strErro As New Text.StringBuilder(String.Empty)

            ValidarCamposObrigatorios = True

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If

            Dim objcanal As New IAC.ContractoServicio.Canal.SetCanal.Canal
            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Acao = eAcaoEspecifica.AltaConRegistro Then
                objcanal.Vigente = True
            Else
                objcanal.Vigente = chkVigente.Checked
            End If

            ' atualizar propriedade vigente
            EsVigente = chkVigente.Checked

            objcanal.Codigo = txtCodigoCanal.Text.Trim
            objcanal.Descripcion = txtDescricaoCanal.Text.Trim
            objcanal.Observaciones = txtObservaciones.Text
            objcanal.SubCanales = RetornaCanaisEnvio(SubCanalesTemporario, SubCanalesClone)
            objcanal.CodigoAjeno = CodigosAjenosPeticion

            If Not String.IsNullOrEmpty(OidCanal) Then
                objcanal.gmtCreacion = CanalColeccion(0).FyhActualizacion
                objcanal.desUsuarioCreacion = CanalColeccion(0).CodigoUsuario
            Else
                objcanal.gmtCreacion = DateTime.Now
                objcanal.desUsuarioCreacion = MyBase.LoginUsuario
            End If

            objcanal.gmtModificacion = DateTime.Now
            objcanal.desUsuarioModificacion = MyBase.LoginUsuario

            'Cria a coleção
            Dim objColCanal As New IAC.ContractoServicio.Canal.SetCanal.CanalColeccion
            objColCanal.Add(objcanal)

            objPeticionCanal.Canales = objColCanal
            objPeticionCanal.CodUsuario = MyBase.LoginUsuario

            objRespuestaCanal = objProxyCanal.setCanales(objPeticionCanal)

            Session.Remove("objRespuestaGEPR_TCANAL")
            'Define a ação de busca somente se houve retorno de canais
            If Master.ControleErro.VerificaErro(objRespuestaCanal.CodigoError, objRespuestaCanal.NombreServidorBD, objRespuestaCanal.MensajeError) Then
                If Master.ControleErro.VerificaErro(objRespuestaCanal.RespuestaCanales(0).CodigoError, objRespuestaCanal.NombreServidorBD, objRespuestaCanal.RespuestaCanales(0).MensajeError) Then
                    Response.Redirect("~/BusquedaCanales.aspx", False)
                End If
            Else
                If objRespuestaCanal.RespuestaCanales IsNot Nothing _
                    AndAlso objRespuestaCanal.RespuestaCanales.Count > 0 _
                    AndAlso objRespuestaCanal.RespuestaCanales(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    Master.ControleErro.ShowError(objRespuestaCanal.RespuestaCanales(0).MensajeError, False)
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
        If Session("objRespuestaGEPR_TCANAL") IsNot Nothing Then
            Session.Remove("objRespuestaGEPR_TCANAL")
        End If
        Response.Redirect("~/BusquedaCanales.aspx", False)
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

            Dim url As String = "MantenimientoSubCanales.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta

            'Passa a coleção com o objeto temporario de subcanais
            Session("colCanalesTemporario") = SubCanalesTemporario

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "script_popup_subcanal", "AbrirPopup('" & url & "', 'page', 400, 788, '');", True)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

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

            'Criando um subcanal para exclusão
            Dim objSubCanal As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal = RetornaSubCanalExiste(SubCanalesTemporario, strCodigo)
            objSubCanal.Vigente = False

            'Carrega os SubCanais no GridView
            Dim objDT As DataTable
            objDT = ProsegurGridView1.ConvertListToDataTable(SubCanalesTemporario)
            ProsegurGridView1.CarregaControle(objDT)

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
            Dim url As String = "MantenimientoSubCanales.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta

            'Seta a session com o subcanal que será consmida na abertura da PopUp de Mantenimiento de SubCanales
            SetSubCanalSelecionadoPopUp()

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_subcanal", "AbrirPopupModal('" & url & "', 325, 788,'Canales');", True)

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

            Dim url As String = "MantenimientoSubCanales.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Modificacion

            'Seta a session com o subcanal que será consmida na abertura da PopUp de Mantenimiento de SubCanales
            SetSubCanalSelecionadoPopUp()

            'Passa a coleção com o objeto temporario de subcanais
            Session("colCanalesTemporario") = SubCanalesTemporario

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_subcanal", "AbrirPopupModal('" & url & "', 325, 788,'CanlesModificacion');", True)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Função do Botão Volver
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarVolver()
        If Session("objRespuestaGEPR_TCANAL") IsNot Nothing Then
            Session.Remove("objRespuestaGEPR_TCANAL")
        End If
        Response.Redirect("~/BusquedaCanales.aspx", False)
    End Sub
#End Region

#End Region

#Region "[EVENTOS TEXTBOX]"

    ''' <summary>
    ''' Evento de mudança de texto do campo Código Canal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtCodigoCanal_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoCanal.TextChanged

        If ExisteCodigoCanal(txtCodigoCanal.Text) Then
            CodigoExistente = True
        Else
            CodigoExistente = False
        End If

        Threading.Thread.Sleep(100)

    End Sub


    ''' <summary>
    ''' Evento de mudança de texto do campo Descrição Canal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtDescricaoCanal_TextChanged(sender As Object, e As System.EventArgs) Handles txtDescricaoCanal.TextChanged

        Try

            If ExisteDescricaoCanal(txtDescricaoCanal.Text) Then
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

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Informa se a descrição do canal já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 05/06/2009 - Criado
    ''' </history>
    Private Function ExisteDescricaoCanal(descricao As String) As Boolean
        Dim objRespostaVerificarDescricaoCanal As IAC.ContractoServicio.Canal.VerificarDescripcionCanal.Respuesta

        Try
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                If descricao.Trim.Equals(DescricaoAtual) Then
                    DescricaoExistente = False
                    Return False
                End If
            End If

            Dim objProxyCanal As New Comunicacion.ProxyCanal
            Dim objPeticionVerificarDescricaoCanal As New IAC.ContractoServicio.Canal.VerificarDescripcionCanal.Peticion

            'Verifica se o código do canal existe no BD
            objPeticionVerificarDescricaoCanal.Descripcion = descricao.Trim()
            objRespostaVerificarDescricaoCanal = objProxyCanal.VerificarDescripcionCanal(objPeticionVerificarDescricaoCanal)

            If Master.ControleErro.VerificaErro(objRespostaVerificarDescricaoCanal.CodigoError, objRespostaVerificarDescricaoCanal.NombreServidorBD, objRespostaVerificarDescricaoCanal.MensajeError) Then
                Return objRespostaVerificarDescricaoCanal.Existe
            Else
                Master.ControleErro.ShowError(objRespostaVerificarDescricaoCanal.MensajeError) 'TODO: Exibe a mensagem de erro. Apenas para Debug. Retirar para Release.
                Return False
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Function

    ''' <summary>
    ''' Informa se o código do canal já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 05/06/2009 - Criado
    ''' </history>
    Private Function ExisteCodigoCanal(codigoCanal As String) As Boolean
        Dim objRespostaVerificarCodigoCanal As IAC.ContractoServicio.Canal.VerificarCodigoCanal.Respuesta
        Try

            Dim objProxyCanal As New Comunicacion.ProxyCanal
            Dim objPeticionVerificarCodigoCanal As New IAC.ContractoServicio.Canal.VerificarCodigoCanal.Peticion

            'Verifica se o código do canal existe no BD
            objPeticionVerificarCodigoCanal.Codigo = codigoCanal.Trim()
            objRespostaVerificarCodigoCanal = objProxyCanal.VerificarCodigoCanal(objPeticionVerificarCodigoCanal)

            If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoCanal.CodigoError, objRespostaVerificarCodigoCanal.NombreServidorBD, objRespostaVerificarCodigoCanal.MensajeError) Then
                Return objRespostaVerificarCodigoCanal.Existe
            Else
                Return False
                Master.ControleErro.ShowError(objRespostaVerificarCodigoCanal.MensajeError) 'TODO: Exibe a mensagem de erro. Apenas para Debug. Retirar para Release.
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Function

    ''' <summary>
    ''' Seta o subcanal para a PopUp de SubCanais
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSubCanalSelecionadoPopUp()

        'Cria o canal para ser consumido na página de SubCanais
        Dim objSubCanal As New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal
        objSubCanal = RetornaSubCanalExiste(SubCanalesTemporario, ProsegurGridView1.getValorLinhaSelecionada)

        'Envia o SubCanal para ser consumido pela PopUp de Mantenimento de SubCanal
        Session("setSubCanal") = objSubCanal

    End Sub

    ''' <summary>
    ''' Carrega os dados do gridView quando a página é carregada pela primeira vez
    ''' </summary>
    ''' <param name="codCanal"></param>
    ''' <remarks></remarks>
    Public Sub CarregaDados(codCanal As String)

        Dim objColCanal As New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.CanalColeccion
        objColCanal = getSubCanalesByCanal(codCanal)

        If objColCanal.Count > 0 Then

            CanalColeccion = objColCanal
            Dim iCodigoAjeno As ContractoServicio.CodigoAjeno.CodigoAjenoBase = (From item In objColCanal(0).CodigosAjenos
                                Where item.BolDefecto = True).FirstOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.codAjeno
                txtCodigoAjeno.ToolTip = iCodigoAjeno.codAjeno
                txtDescricaoAjeno.Text = iCodigoAjeno.desAjeno
                txtDescricaoAjeno.ToolTip = iCodigoAjeno.desAjeno
            End If

            OidCanal = objColCanal(0).OidCanal
            'Preenche os controles do formulario
            txtCodigoCanal.Text = objColCanal(0).Codigo
            txtCodigoCanal.ToolTip = objColCanal(0).Codigo

            txtDescricaoCanal.Text = objColCanal(0).Descripcion
            txtDescricaoCanal.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objColCanal(0).Descripcion, String.Empty)

            txtObservaciones.Text = objColCanal(0).Observaciones
            chkVigente.Checked = objColCanal(0).Vigente

            ' preenche a propriedade da tela
            EsVigente = objColCanal(0).Vigente

            'Se for modificação então guarda a descriçaõ atual para validação
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                DescricaoAtual = txtDescricaoCanal.Text
            End If

            'Preenche (CodigosAjenosSet com os valores do CodigosAjenos) dos objColCanal(0).SubCanales
            objColCanal(0).SubCanales.ForEach(Sub(s) s.CodigosAjenosSet = CodigoAjenoBaseToSet(s.CodigosAjenos))

            'Faz um clone da coleção de canal
            SubCanalesClone = objColCanal(0).SubCanales

            'Carrega os SubCanais no GridView
            Dim objDT As DataTable
            objDT = ProsegurGridView1.ConvertListToDataTable(objColCanal(0).SubCanales)
            ProsegurGridView1.CarregaControle(objDT)

        End If

    End Sub

    Private Function CodigoAjenoBaseToSet(CodigosAjenos As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase) As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        If CodigosAjenos Is Nothing Then
            Return Nothing
        End If
        Dim objCodigoAjeno As New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase

        For Each item In CodigosAjenos

            Dim objCodeAjeno As New ContractoServicio.CodigoAjeno.CodigoAjenoBase
            objCodeAjeno.BolActivo = item.BolActivo
            objCodeAjeno.BolDefecto = item.BolDefecto
            objCodeAjeno.CodAjeno = item.CodAjeno
            objCodeAjeno.CodIdentificador = item.CodIdentificador
            'objCodeAjeno.CodTipoTablaGenesis = Comum.Constantes.COD_SUBCANAL
            'objCodeAjeno.DesUsuarioCreacion = MyBase.LoginUsuario
            'objCodeAjeno.DesUsuarioModificacion = MyBase.LoginUsuario
            objCodeAjeno.OidCodigoAjeno = item.OidCodigoAjeno
            objCodeAjeno.DesAjeno = item.DesAjeno

            objCodigoAjeno.Add(objCodeAjeno)
        Next
        Return objCodigoAjeno

    End Function

    ''' <summary>
    ''' Consome o subcanal passado pela PopUp de Mantenimiento de SubCanales. 
    ''' Obs: O subCanal só pode ser consumido no modo "Alta" ou "Modificacion"
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeSubCanal()

        If Session("objSubCanal") IsNot Nothing Then

            Dim objSubCanal As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal
            objSubCanal = DirectCast(Session("objSubCanal"), IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal)

            'Se existe o subcanal na coleção
            If Not VerificarSubCanalExiste(SubCanalesTemporario, objSubCanal.Codigo) Then
                SubCanalesTemporario.Add(objSubCanal)
            Else
                ModificaSubCanal(SubCanalesTemporario, objSubCanal)
            End If


            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                'Seta o estado da página corrente para modificação
                Acao = eAcaoEspecifica.AltaConRegistro
            End If


            'Carrega os SubCanais no GridView
            Dim objDT As DataTable
            objDT = ProsegurGridView1.ConvertListToDataTable(SubCanalesTemporario)
            ProsegurGridView1.CarregaControle(objDT)

            Session("objSubCanal") = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Verifica se um canal especifico existe na coleção informada
    ''' </summary>
    ''' <param name="objsubCanales"></param>
    ''' <param name="codigoSubCanal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>    
    Private Function VerificarSubCanalExiste(objsubCanales As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion, codigoSubCanal As String) As Boolean

        Dim retorno = From c In objsubCanales Where c.Codigo = codigoSubCanal

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' ' Retorna uma coleção de subcanais que tem a mais no objeto temporario em relação ao clone e o que é foi modificado no temporario em relação ao clone
    ''' </summary>
    ''' <param name="objSubCanalesTemporario"></param>
    ''' <param name="objSubCanalesClone"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetornaCanaisEnvio(objSubCanalesTemporario As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion, objSubCanalesClone As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion) As IAC.ContractoServicio.Canal.SetCanal.SubCanalColeccion

        ' Retorna o que tem a mais no temporario em relação ao clone e o que é foi modificado no temporario em relação ao clone
        Dim retorno = (From c As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal In objSubCanalesTemporario _
                       Join d As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal In objSubCanalesClone _
                       On c.Codigo Equals d.Codigo _
                            Where (c.Descripcion <> d.Descripcion OrElse c.Observaciones <> d.Observaciones OrElse c.Vigente <> d.Vigente OrElse _
                                  (d.CodigosAjenosSet Is Nothing AndAlso c.CodigosAjenosSet IsNot Nothing OrElse _
                                   c.CodigosAjenosSet.ToString.GetHashCode.ToString <> d.CodigosAjenosSet.ToString.GetHashCode.ToString)) _
                            Select c.Codigo, c.Descripcion, c.Observaciones, c.Vigente, c.CodigosAjenosSet) _
                            .Union(From x As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal In objSubCanalesTemporario _
                                   Where Not (From o As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal In objSubCanalesClone _
                                              Select o.Codigo).Contains(x.Codigo) _
                            Select x.Codigo, x.Descripcion, x.Observaciones, x.Vigente, x.CodigosAjenosSet)


        Dim objSubCanalCol As New IAC.ContractoServicio.Canal.SetCanal.SubCanalColeccion

        Dim objSubCanalEnvio As IAC.ContractoServicio.Canal.SetCanal.SubCanal
        For Each objRetorno As Object In retorno
            objSubCanalEnvio = New IAC.ContractoServicio.Canal.SetCanal.SubCanal
            objSubCanalEnvio.Codigo = objRetorno.codigo
            objSubCanalEnvio.Descripcion = objRetorno.Descripcion
            objSubCanalEnvio.Observaciones = objRetorno.Observaciones
            objSubCanalEnvio.Vigente = objRetorno.Vigente
            objSubCanalEnvio.CodigoAjeno = objRetorno.CodigosAjenosSet

            'Adiciona na coleção
            objSubCanalCol.Add(objSubCanalEnvio)
        Next

        Return objSubCanalCol

    End Function


    ''' <summary>
    ''' Modifica um sub canal existe na coleção informada
    ''' </summary>
    ''' <param name="objsubCanales"></param>
    ''' <param name="objSubCanal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ModificaSubCanal(ByRef objsubCanales As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion, objSubCanal As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal) As Boolean

        Dim retorno = From c In objsubCanales Where c.Codigo = objSubCanal.Codigo

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else

            Dim objSubCanalTmp As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal = DirectCast(retorno.ElementAt(0), IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal)

            objSubCanalTmp.Descripcion = objSubCanal.Descripcion
            objSubCanalTmp.Observaciones = objSubCanal.Observaciones
            objSubCanalTmp.Vigente = objSubCanal.Vigente
            objSubCanalTmp.CodigosAjenosSet = objSubCanal.CodigosAjenosSet

            Return True
        End If
    End Function

    ''' <summary>
    ''' Retorna um subcanal da coleção informada    
    ''' </summary>
    ''' <param name="objsubCanales"></param>
    ''' <param name="codigoSubCanal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetornaSubCanalExiste(ByRef objsubCanales As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion, codigoSubCanal As String) As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal

        Dim retorno = From c In objsubCanales Where c.Codigo = codigoSubCanal

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return Nothing
        Else
            Return retorno.ElementAt(0)
        End If

    End Function

#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()

        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta
                btnAlta.Visible = True          '1
                btnGrabar.Visible = True        '2
                btnCancelar.Visible = True      '3

                'Estado Inicial Controles                                
                txtCodigoCanal.Enabled = True

                btnBaja.Visible = False         '4
                btnModificacion.Visible = False '5
                btnConsulta.Visible = False     '6 
                btnVolver.Visible = False       '7

                lblVigente.Visible = False
                chkVigente.Checked = True
                chkVigente.Enabled = True
                chkVigente.Visible = False

                btnGrabar.Habilitado = True
                btnAlta.Habilitado = True

            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                btnAlta.Visible = False                 '1
                btnCancelar.Visible = False              '2

                If SubCanalesTemporario.Count > 0 Then
                    btnConsulta.Visible = True          '3
                Else
                    btnConsulta.Visible = False
                End If
                btnVolver.Visible = True                '4

                'Estado Inicial Controles
                txtCodigoCanal.Enabled = False
                txtDescricaoCanal.Enabled = False
                txtObservaciones.Enabled = False
                lblVigente.Visible = True
                chkVigente.Enabled = False

                btnBaja.Visible = False                 '5
                btnModificacion.Visible = False         '6
                btnGrabar.Visible = False               '7
                btnAjeno.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                txtCodigoCanal.Enabled = False
                chkVigente.Visible = True

                btnAlta.Visible = True                 '2
                If SubCanalesTemporario.Count > 0 Then
                    btnBaja.Visible = True             '3
                    btnConsulta.Visible = True         '4
                    btnModificacion.Visible = True     '1
                Else
                    btnBaja.Visible = False
                    btnConsulta.Visible = False
                    btnModificacion.Visible = False    '1
                End If

                btnGrabar.Visible = True               '5
                btnCancelar.Visible = True             '6
                btnVolver.Visible = False              '7

                lblVigente.Visible = True
                ' se estiver checado e objeto for vigente
               chkVigente.Enabled = True



                btnGrabar.Habilitado = True
                btnAlta.Habilitado = True
                btnModificacion.Habilitado = True
                btnConsulta.Habilitado = True
                btnBaja.Habilitado = True

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
                If SubCanalesTemporario.Count > 0 Then
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

                chkVigente.Enabled = True
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
                If txtCodigoCanal.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoCanal.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If txtDescricaoCanal.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoCanal.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If


            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If CodigoExistente Then

                strErro.Append(csvCodigoCanalExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoCanalExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoCanal.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoCanalExistente.IsValid = True
            End If

            'Verifica se a descrição existe
            If DescricaoExistente Then

                strErro.Append(csvDescripcionExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescripcionExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescricaoCanal.Focus()
                    focoSetado = True
                End If

            Else
                csvDescripcionExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function


#End Region

#Region "[PROPRIEDADES]"

    Public ReadOnly Property SubCanalesTemporario() As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion
        Get
            If ViewState("SubCanalesTemporario") Is Nothing Then
                ViewState("SubCanalesTemporario") = New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion
            End If

            Return DirectCast(ViewState("SubCanalesTemporario"), IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion)
        End Get
    End Property

    Public Property SubCanalesClone() As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion
        Get
            If ViewState("SubCanalesClone") Is Nothing Then
                ViewState("SubCanalesClone") = New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion
            End If

            Return DirectCast(ViewState("SubCanalesClone"), IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion)
        End Get
        Set(value As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion)
            ViewState("SubCanalesClone") = value
            ViewState("SubCanalesTemporario") = value
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

    Private Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
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

    Private Property DescricaoAtual() As String
        Get
            Return ViewState("DescricaoAtual")
        End Get
        Set(value As String)
            ViewState("DescricaoAtual") = value.Trim
        End Set
    End Property

    Public Property OidCanal() As String
        Get
            Return ViewState("OidCanal")
        End Get
        Set(value As String)
            ViewState("OidCanal") = value
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

    Private Property CanalColeccion() As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.CanalColeccion
        Get
            Return DirectCast(ViewState("CanalColeccion"), IAC.ContractoServicio.Canal.GetSubCanalesByCanal.CanalColeccion)
        End Get

        Set(value As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.CanalColeccion)
            ViewState("CanalColeccion") = value
        End Set

    End Property

#End Region

    Protected Sub btnAjeno_Click(sender As Object, e As EventArgs) Handles btnAjeno.Click
        Dim url As String = String.Empty
        Dim tablaGenesis As New MantenimientoCodigosAjenos.CodigoAjenoSimples

        tablaGenesis.CodTablaGenesis = txtCodigoCanal.Text
        tablaGenesis.DesTablaGenesis = txtDescricaoCanal.Text
        tablaGenesis.OidTablaGenesis = OidCanal
        If CanalColeccion IsNot Nothing AndAlso CanalColeccion.Count > 0 AndAlso CanalColeccion.FirstOrDefault IsNot Nothing AndAlso CanalColeccion.FirstOrDefault.CodigosAjenos IsNot Nothing Then
            tablaGenesis.CodigosAjenos = CanalColeccion.FirstOrDefault.CodigosAjenos
        End If

        Session("objGEPR_TCANAL") = tablaGenesis

        If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TCANAL"
        Else
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TCANAL"
        End If

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 900,'btnAjeno'); ", True)
    End Sub

    ''' <summary>
    ''' Consome o valor da session da coleção de código ajeno
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 07/05/2013 - Criado
    ''' </history>
    Private Sub ConsomeCodigoAjeno()

        If Session("objRespuestaGEPR_TCANAL") IsNot Nothing Then

            If CanalColeccion Is Nothing OrElse CanalColeccion.Count = 0 OrElse CanalColeccion.FirstOrDefault Is Nothing Then
                CanalColeccion = New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.CanalColeccion

                CanalColeccion.Add(New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.Canal)

            End If
            CanalColeccion.FirstOrDefault.CodigosAjenos = Session("objRespuestaGEPR_TCANAL")
            Session.Remove("objRespuestaGEPR_TCANAL")

            Dim iCodigoAjeno = (From item In CanalColeccion.FirstOrDefault.CodigosAjenos
                                Where item.BolDefecto = True).SingleOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDescricaoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            If CanalColeccion.FirstOrDefault.CodigosAjenos IsNot Nothing Then
                CodigosAjenosPeticion = CanalColeccion.FirstOrDefault.CodigosAjenos
            Else
                CodigosAjenosPeticion = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
            End If
        End If

    End Sub

End Class