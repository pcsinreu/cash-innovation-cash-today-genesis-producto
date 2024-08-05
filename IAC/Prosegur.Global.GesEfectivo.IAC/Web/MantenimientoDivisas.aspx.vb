Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' Página de Gerenciamento de Divisas 
''' </summary>
''' <remarks></remarks>
''' <history>[PDA] 03/01/09 - Criado</history>
Partial Public Class MantenimientoDivisas
    Inherits Base
    Dim ParametroMantenimientoClientesDivisasPorPantalla As Boolean

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

        Dim url As String = "MantenimientoDenominaciones.aspx"

        btnBaja.FuncaoJavascript = "if (VerificarRegistroSelecionadoGridView('" & GdvDenominaciones.ClientID & "','" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgSeleccionarRegistro) & "','" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "'))__doPostBack('" & Me.btnBaja.ID & "', null);"

        Dim pbo As PostBackOptions
        Dim s As String = String.Empty
        'Pega a ação de PostBack do controle e associa a função Js
        '   Botão Baja
        pbo = New PostBackOptions(btnBaja)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnBaja.FuncaoJavascript = "if(VerificarRegistroSelecionadoGridView('" & GdvDenominaciones.ClientID & "','" & _
                                            Traduzir(Aplicacao.Util.Utilidad.InfoMsgSeleccionarRegistro) & "','" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & _
                                            "','" & Traduzir("info_msg_registro_borrado") & " '))" & s & ";"

        '   Botão Modificacion
        pbo = New PostBackOptions(btnModificacion)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnModificacion.FuncaoJavascript = "if(VerificarRegistroSelecionadoGridView('" & GdvDenominaciones.ClientID & "','" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgSeleccionarRegistro) & "',''))" & s & ";"

        '   Botão Consulta
        pbo = New PostBackOptions(btnConsulta)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnConsulta.FuncaoJavascript = "if(VerificarRegistroSelecionadoGridView('" & GdvDenominaciones.ClientID & "','" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgSeleccionarRegistro) & "',''))" & s & ";"

        'Quando presciona o enter seta o foco para o proximo controle.
        txtCodigoDivisa.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoDivisa.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoDivisa.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoDivisa.ClientID & "').focus();return false;}} else {return true}; ")

        Dim strBuider As New StringBuilder

        ' Script para mudar a cor de fundo do controle
        strBuider.Append("function colorChanged(sender)")
        strBuider.Append("{")
        strBuider.Append("  sender.get_element().style.backgroundColor = '#' + sender.get_selectedColor();")
        strBuider.Append("}")

        'Adiciona o script na página
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ColorChanged", strBuider.ToString, True)

        txtColor.Attributes.Add("onKeyDown", "return false;")


        'Controle de precedência(Ajax)

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)
    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigoDivisa.TabIndex = 1
        txtDescricaoDivisa.TabIndex = 2
        txtSimbolo.TabIndex = 3
        txtColor.TabIndex = 4
        txtCodigoAcesso.TabIndex = 5
        chkVigente.TabIndex = 6
        btnAlta.TabIndex = 17
        btnBaja.TabIndex = 18
        btnModificacion.TabIndex = 19
        btnConsulta.TabIndex = 20
        btnGrabar.TabIndex = 21
        btnCancelar.TabIndex = 22
        btnVolver.TabIndex = 23

        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta

                Me.DefinirRetornoFoco(Master.Sair, Master.RetornaMenu)
                Master.PrimeiroControleTelaID = txtCodigoDivisa.ClientID

            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta

                ' Define o foco para o botão de buscar cliente
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SETAFOCUS", "document.getElementById('btnConsulta_img').focus();", True)
                Me.DefinirRetornoFoco(Master.Sair, Master.RetornaMenu)
                Master.PrimeiroControleTelaID = "btnConsulta_img"

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion

                Master.PrimeiroControleTelaID = txtDescricaoDivisa.ClientID
                Me.DefinirRetornoFoco(Master.Sair, Master.RetornaMenu)

            Case Aplicacao.Util.Utilidad.eAcao.NoAction
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Busca
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.NoAction

            Case eAcaoEspecifica.AltaConRegistro

                Master.PrimeiroControleTelaID = txtCodigoDivisa.ClientID
                Me.DefinirRetornoFoco(Master.Sair, Master.RetornaMenu)

            Case Aplicacao.Util.Utilidad.eAcao.Erro

                Master.PrimeiroControleTelaID = txtCodigoDivisa.ClientID
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
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.DIVISAS

    End Sub

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                ParametroMantenimientoClientesDivisasPorPantalla = Prosegur.Genesis.Parametros.Genesis.Parametros.ParametrosIntegracion.Where(Function(o) o.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_MANTENIMIENTO_CLIENTES_DIVISAS_POR_PANTALLA ).First.ValorParametro

                'Recebe o código do Divisa
                Dim strCodDivisa As String = Request.QueryString("coddivisa")

                'Possíveis Ações passadas pela página BusquedaDivisaes:
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


                If strCodDivisa <> String.Empty Then
                    'Estado Inicial dos control
                    CarregaDados(strCodDivisa)
                End If

                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion _
                    OrElse MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    txtDescricaoDivisa.Focus()
                Else
                    txtCodigoDivisa.Focus()
                End If

            End If

            DesabilitarCampos(ParametroMantenimientoClientesDivisasPorPantalla)
            'Consome a Denominação passado pela PopUp de "Denominação"
            ConsomeDenominacion()
            ConsomeCodigoAjeno()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Private Sub DesabilitarCampos(HabilitarTodosCampos As Boolean)
        
        If HabilitarTodosCampos Then
            txtDescricaoDivisa.Enabled = True
            txtCodigoDivisa.Enabled = True
            txtCodigoAcesso.Enabled = True
            txtColor.Enabled = True
            txtSimbolo.Enabled = True
            chkVigente.Enabled = True
        Else
            'Permite el mantenimiento solamente del color y del código de acceso de las divisas.
            'Permite el mantenimiento solamente del código de acceso y del peso de las denominaciones.
            'Permite consultar divisas y denominaciones.
            'Permite el mantenimiento de divisas a través del servicio Divisa.
            txtDescricaoDivisa.Enabled = False
            txtCodigoDivisa.Enabled = False
            txtCodigoAcesso.Enabled = True
            txtColor.Enabled = True
            txtSimbolo.Enabled = False
            chkVigente.Enabled = False
        End If
       

    End Sub



    Protected Overrides Sub PreRenderizar()

        Try
            ControleBotoes()
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()

        Master.TituloPagina = Traduzir("005_titulo_mantenimiento_divisas")
        lblCodigoDivisa.Text = Traduzir("005_lbl_codigoiso")
        lblDescricaoDivisa.Text = Traduzir("005_lbl_descricaoDivisa")
        lblVigente.Text = Traduzir("005_lbl_vigente")
        lblTituloDivisas.Text = Traduzir("005_lbl_subtitulosDivisas")
        lblSubTitulosDivisas.Text = Traduzir("005_lbl_subtitulosDenominaciones")
        lblSimbolo.Text = Traduzir("005_lbl_simbolo")
        lblColor.Text = Traduzir("005_lbl_cor")
        lblCodigoAcesso.Text = Traduzir("005_lbl_Codigoacceso")
        lblCodigoAjeno.Text = Traduzir("037_lbl_CodigoAjeno")
        lblDesCodigoAjeno.Text = Traduzir("037_lbl_DesCodigoAjeno")
        GdvDenominaciones.PagerSummary = Traduzir("grid_lbl_pagersummary")

        csvCodigoObrigatorio.ErrorMessage = Traduzir("005_msg_divisacodigoobligatorio")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("005_msg_divisadescripcionobligatorio")
        csvCodigoColorObrigatorio.ErrorMessage = Traduzir("005_msg_codigocolorobligatorio")
        csvCodigoAcessoObrigatorio.ErrorMessage = Traduzir("005_msg_codigoaccesoobligatorio")
        csvCodigoDivisaExistente.ErrorMessage = Traduzir("005_msg_codigodivisaexistente")
        csvDescricaoDivisaExistente.ErrorMessage = Traduzir("005_msg_descripciondivisaexistente")
        csvCodigoAcessoExiste.ErrorMessage = Traduzir("005_msg_codigoaccesoexistente")

    End Sub

#End Region

#Region "[DADOS]"

    Public Function getDenominacionByDivisa(codigoDivisa As String) As IAC.ContractoServicio.Divisa.DivisaColeccion

        Dim objProxyDivisa As New Comunicacion.ProxyDivisa
        Dim objPeticionDivisa As New IAC.ContractoServicio.Divisa.GetDenominacionesByDivisa.Peticion
        Dim objRespuestaDivisa As IAC.ContractoServicio.Divisa.GetDenominacionesByDivisa.Respuesta

        'Recebe os valores do filtro
        Dim lista As New List(Of String)
        lista.Add(codigoDivisa)

        objPeticionDivisa.CodigoIso = lista

        objRespuestaDivisa = objProxyDivisa.getDenominacionesByDivisa(objPeticionDivisa)

        Return objRespuestaDivisa.Divisas


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
    Private Sub ProsegurGridView1_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvDenominaciones.EOnClickRowClientScript

        Try

            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            GdvDenominaciones.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("Vigente").ToString.ToLower & ");"

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
    Protected Sub GdvDenominaciones_EPreencheDados(sender As Object, e As EventArgs) Handles GdvDenominaciones.EPreencheDados

        Try

            Dim objDT As DataTable

            objDT = GdvDenominaciones.ConvertListToDataTable(DenominacionesTemporario)

            objDT.DefaultView.Sort = GdvDenominaciones.SortCommand

            GdvDenominaciones.ControleDataSource = objDT

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento que configura o css do objeto pager do Gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub GdvDenominaciones_EPager_SetCss(sender As Object, e As EventArgs) Handles GdvDenominaciones.EPager_SetCss

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
            CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 16

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
    Private Sub GdvDenominaciones_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvDenominaciones.RowDataBound

        Try
            'Índice das celulas do GridView Configuradas
            '0 - Código
            '1 - Descripción
            '2 - Valor
            '3 - Peso
            '4 - Bilhete
            '5 - Vigente

            If e.Row.DataItem IsNot Nothing Then

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                If Not e.Row.DataItem("descripcion") Is DBNull.Value AndAlso e.Row.DataItem("descripcion").Length > NumeroMaximoLinha Then
                    e.Row.Cells(1).Text = e.Row.DataItem("descripcion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                If CBool(e.Row.DataItem("EsBillete")) Then
                    CType(e.Row.Cells(4).FindControl("imgBillete"), Image).ImageUrl = "~/Imagenes/money.gif"
                Else
                    CType(e.Row.Cells(4).FindControl("imgBillete"), Image).ImageUrl = "~/Imagenes/coins.gif"
                End If

                If CBool(e.Row.DataItem("Vigente")) Then
                    CType(e.Row.Cells(5).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
                Else
                    CType(e.Row.Cells(5).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
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
    Private Sub GdvDenominaciones_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvDenominaciones.RowCreated

        Try
            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("005_lbl_grd_mantenimiento_codigo")
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 4
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 5
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("005_lbl_grd_mantenimiento_descripcion")
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 6
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 7
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("005_lbl_grd_mantenimiento_valor")
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 8
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 9
                CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("005_lbl_grd_mantenimiento_peso")
                CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 10
                CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 11
                CType(e.Row.Cells(4).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("005_lbl_grd_mantenimiento_bilhete")
                CType(e.Row.Cells(4).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 12
                CType(e.Row.Cells(4).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 13
                CType(e.Row.Cells(5).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("005_lbl_grd_mantenimiento_vigente")
                CType(e.Row.Cells(5).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 14
                CType(e.Row.Cells(5).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 15

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
        ExecutarGrabar()
    End Sub

    Private Sub btnAltaAjeno_Click(sender As Object, e As System.EventArgs) Handles btnAltaAjeno.Click
        Dim url As String = String.Empty
        Dim tablaGenesis As New MantenimientoCodigosAjenos.CodigoAjenoSimples

        tablaGenesis.CodTablaGenesis = txtCodigoDivisa.Text
        tablaGenesis.DesTablaGenesis = txtDescricaoDivisa.Text
        tablaGenesis.OidTablaGenesis = OidDivisa
        If Divisas IsNot Nothing AndAlso Divisas.Count > 0 AndAlso Divisas.FirstOrDefault IsNot Nothing AndAlso Divisas.FirstOrDefault.CodigosAjenos IsNot Nothing Then
            tablaGenesis.CodigosAjenos = Divisas.FirstOrDefault.CodigosAjenos
        End If

        Session("objPeticionGEPR_TDIVISA") = tablaGenesis.CodigosAjenos
        Session("objGEPR_TDIVISA") = tablaGenesis

        If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TDIVISA"
        Else
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TDIVISA"
        End If

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_Divisas", "AbrirPopupModal('" & url & "', 550, 900,'btnAltaAjeno');", True)
    End Sub

    'Ações que podem ser chamadas a qualquer momento
#Region "Ações Botões Independentes"

    ''' <summary>
    ''' Função do botão grabar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarGrabar()
        Try

            Dim objProxyDivisa As New Comunicacion.ProxyDivisa
            Dim objPeticionDivisa As New IAC.ContractoServicio.Divisa.SetDivisasDenominaciones.Peticion
            Dim objRespuestaDivisa As IAC.ContractoServicio.Divisa.SetDivisasDenominaciones.Respuesta
            Dim strErro As New Text.StringBuilder(String.Empty)

            ValidarCamposObrigatorios = True

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If

            Dim objDivisa As New IAC.ContractoServicio.Divisa.Divisa
            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Acao = eAcaoEspecifica.AltaConRegistro Then
                objDivisa.Vigente = True
            Else
                objDivisa.Vigente = chkVigente.Checked
            End If

            ' atualizar propriedade vigente
            EsVigente = chkVigente.Checked

            objDivisa.CodigoIso = txtCodigoDivisa.Text.Trim
            objDivisa.Descripcion = txtDescricaoDivisa.Text.Trim
            objDivisa.CodigoSimbolo = txtSimbolo.Text.Trim
            objDivisa.ColorDivisa = txtColor.Text.Trim
            objDivisa.CodigoAccesoDivisa = txtCodigoAcesso.Text.Trim
            objDivisa.Denominaciones = RetornaDenominacionesEnvio(DenominacionesTemporario, DenominacionesClone)

            'Cria a coleção
            Dim objColDivisa As New IAC.ContractoServicio.Divisa.DivisaColeccion
            objColDivisa.Add(objDivisa)

            objPeticionDivisa.Divisas = objColDivisa
            objPeticionDivisa.CodigoUsuario = MyBase.LoginUsuario
            objPeticionDivisa.CodigoAjeno = CodigosAjenosPeticion
            objRespuestaDivisa = objProxyDivisa.setDivisaDenominaciones(objPeticionDivisa)

            'Define a ação de busca somente se houve retorno de canais

            If Master.ControleErro.VerificaErro(objRespuestaDivisa.CodigoError, objRespuestaDivisa.NombreServidorBD, objRespuestaDivisa.MensajeError) Then

                If Master.ControleErro.VerificaErro(objRespuestaDivisa.RespuestaDivisas(0).CodigoError, objRespuestaDivisa.NombreServidorBD, objRespuestaDivisa.RespuestaDivisas(0).MensajeError) Then

                    Response.Redirect("~/BusquedaDivisas.aspx", False)

                End If

            Else

                If objRespuestaDivisa.RespuestaDivisas IsNot Nothing _
                    AndAlso objRespuestaDivisa.RespuestaDivisas.Count > 0 _
                    AndAlso objRespuestaDivisa.RespuestaDivisas(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then

                    Master.ControleErro.ShowError(objRespuestaDivisa.RespuestaDivisas(0).MensajeError, False)

                ElseIf objRespuestaDivisa.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT Then

                    Master.ControleErro.ShowError(objRespuestaDivisa.MensajeError, False)

                End If

            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' função do botão cancelar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarCancelar()
        Response.Redirect("~/BusquedaDivisas.aspx", False)
    End Sub

    ''' <summary>
    ''' Função do botão volver.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarVolver()
        Response.Redirect("~/BusquedaDivisas.aspx", False)
    End Sub

    ''' <summary>
    ''' Função do botão alta.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarAlta()
        Try
            Dim url As String = "MantenimientoDenominaciones.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&CodigoDivisa =" & txtCodigoDivisa.Text.Trim

            'Passa a coleção com o objeto temporario de denominacoes
            SetDenominacionesTemporarioCollectionPopUp()

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_denominaciones", "AbrirPopupModal('" & url & "', 325, 788,'DenominacionAlta');", True)
            ' ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_denominaciones", "AbrirPopupModal('" & url & "', 325, 788,'DivisasAlta');", True)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub


    ''' <summary>
    ''' Função do botão baja.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarBaja()
        Try
            'Retorna o valor da linha selecionada no grid
            Dim strCodigo As String = GdvDenominaciones.getValorLinhaSelecionada

            'Criando uma denominação para exclusão
            Dim objDenominacion As IAC.ContractoServicio.Divisa.Denominacion = RetornaDenominacionExiste(DenominacionesTemporario, strCodigo)
            objDenominacion.Vigente = False

            'Carrega os SubCanais no GridView
            Dim objDT As DataTable
            objDT = GdvDenominaciones.ConvertListToDataTable(DenominacionesTemporario)
            GdvDenominaciones.CarregaControle(objDT)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Função do botão consulta.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarConsulta()
        Try
            Dim url As String = "MantenimientoDenominaciones.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&CodigoDivisa =" & txtCodigoDivisa.Text.Trim

            'Seta a session com o Denominacion que será consmida na abertura da PopUp de Mantenimiento de Denominaciones
            SetDenominacionSelecionadoPopUp()

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_denominaciones", "AbrirPopupModal('" & url & "', 380, 788,'DivisasConsulta');", True)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Função do botão modificacion.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarModificacion()
        Try
            Dim url As String = "MantenimientoDenominaciones.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Modificacion & "&CodigoDivisa=" & txtCodigoDivisa.Text.Trim

            'Seta a session com o Denominação que será consmida na abertura da PopUp de Mantenimiento de Denominacion
            SetDenominacionSelecionadoPopUp()

            'Passa a coleção com o objeto temporario de denominações
            SetDenominacionesTemporarioCollectionPopUp()

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_denominaciones", "AbrirPopupModal('" & url & "', 380, 788,'DivisasModificacion');", True)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

#End Region

#End Region

#Region "[EVENTOS TEXTBOX]"

    ''' <summary>
    ''' Evento de mudança de texto do campo Código Divisa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtCodigoDivisa_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoDivisa.TextChanged
        Try

            If ExisteCodigoDivisa(txtCodigoDivisa.Text) Then
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
    ''' Verifica se o codigo acesso existe.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtCodigoAcesso_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoAcesso.TextChanged

        Try

            If ExisteCodigoAccesoDivisa(txtCodigoAcesso.Text) Then
                CodigoAccesoExistente = True
            Else
                CodigoAccesoExistente = False
            End If

            Threading.Thread.Sleep(100)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento de mudança de texto do campo Descriçao Divisa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtDescricaoDivisa_TextChanged(sender As Object, e As System.EventArgs) Handles txtDescricaoDivisa.TextChanged
        Try

            If ExisteDescricaoDivisa(txtDescricaoDivisa.Text) Then
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
    ''' Carrega os dados do gridView quando a página é carregada pela primeira vez
    ''' </summary>
    ''' <param name="codDivisa"></param>
    ''' <remarks></remarks>
    Public Sub CarregaDados(codDivisa As String)

        Dim objColDivisa As IAC.ContractoServicio.Divisa.DivisaColeccion
        objColDivisa = getDenominacionByDivisa(codDivisa)

        If objColDivisa.Count > 0 Then

            'Preenche os controles do formulario
            txtCodigoDivisa.Text = objColDivisa(0).CodigoIso
            txtCodigoDivisa.ToolTip = objColDivisa(0).CodigoIso
            txtDescricaoDivisa.Text = objColDivisa(0).Descripcion
            txtDescricaoDivisa.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objColDivisa(0).Descripcion, String.Empty)
            txtSimbolo.Text = objColDivisa(0).CodigoSimbolo
            txtSimbolo.ToolTip = objColDivisa(0).CodigoSimbolo
            txtColor.Text = objColDivisa.First.ColorDivisa
            txtCodigoAcesso.Text = objColDivisa.First.CodigoAccesoDivisa
            ' AJENO
            Dim iCodigoAjeno = (From item In objColDivisa(0).CodigosAjenos
                  Where item.BolDefecto = True).FirstOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            If Not String.IsNullOrEmpty(objColDivisa.First.ColorDivisa) Then
                txtColor.BackColor = Drawing.ColorTranslator.FromHtml(String.Format("#{0}", objColDivisa.First.ColorDivisa))
            End If

            chkVigente.Checked = objColDivisa(0).Vigente

            ' preenche a propriedade da tela
            EsVigente = objColDivisa(0).Vigente

            'Se for modificação então guarda a descriçaõ atual para validação
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                DescricaoAtual = txtDescricaoDivisa.Text
                CodigoAccesoAtual = txtCodigoAcesso.Text
            End If

            'Faz um clone da coleção de Divisa
            DenominacionesClone = objColDivisa(0).Denominaciones

            'Carrega os SubCanais no GridView
            Dim objDT As DataTable
            objDT = GdvDenominaciones.ConvertListToDataTable(objColDivisa(0).Denominaciones)
            GdvDenominaciones.CarregaControle(objDT)

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
                If txtCodigoDivisa.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoDivisa.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If txtDescricaoDivisa.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoDivisa.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If txtColor.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoColorObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoColorObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtColor.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoColorObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If txtCodigoAcesso.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoAcessoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoAcessoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoAcesso.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoAcessoObrigatorio.IsValid = True
                End If

            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If CodigoExistente Then

                strErro.Append(csvCodigoDivisaExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoDivisaExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoDivisa.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoDivisaExistente.IsValid = True
            End If

            'Verifica se a descrição existe
            If DescricaoExistente Then

                strErro.Append(csvDescricaoDivisaExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescricaoDivisaExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescricaoDivisa.Focus()
                    focoSetado = True
                End If

            Else
                csvDescricaoDivisaExistente.IsValid = True
            End If

            'Verifica se a descrição existe
            If CodigoAccesoExistente Then

                strErro.Append(csvCodigoAcessoExiste.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoAcessoExiste.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoAcesso.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoAcessoExiste.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' Informa se o código do canal já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/06/2009 - Criado
    ''' </history>
    Private Function ExisteCodigoDivisa(CodigoDivisa As String) As Boolean

        Dim objRespostaVerificarCodigoDivisa As IAC.ContractoServicio.Divisa.VerificarCodigoDivisa.Respuesta

        Try

            Dim objProxyDivisa As New Comunicacion.ProxyDivisa
            Dim objPeticionVerificarCodigoDivisa As New IAC.ContractoServicio.Divisa.VerificarCodigoDivisa.Peticion

            'Verifica se o código do Divisa existe no BD
            objPeticionVerificarCodigoDivisa.CodigoIso = CodigoDivisa.Trim
            objRespostaVerificarCodigoDivisa = objProxyDivisa.VerificarCodigoDivisa(objPeticionVerificarCodigoDivisa)

            If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoDivisa.CodigoError, objRespostaVerificarCodigoDivisa.NombreServidorBD, objRespostaVerificarCodigoDivisa.MensajeError) Then
                Return objRespostaVerificarCodigoDivisa.Existe
            Else
                Master.ControleErro.ShowError(objRespostaVerificarCodigoDivisa.MensajeError)
                Return False
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Function

    ''' <summary>
    ''' Informa se a descrição do canal já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/06/2009 - Criado
    ''' </history>
    Private Function ExisteDescricaoDivisa(descricao As String) As Boolean

        Dim objRespostaVerificarDescricaoDivisa As IAC.ContractoServicio.Divisa.VerificarDescripcionDivisa.Respuesta
        Try


            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                If txtDescricaoDivisa.Text.Trim.Equals(DescricaoAtual) Then
                    DescricaoExistente = False
                    Return False
                End If
            End If


            Dim objProxyDivisa As New Comunicacion.ProxyDivisa
            Dim objPeticionVerificarDescricaoDivisa As New IAC.ContractoServicio.Divisa.VerificarDescripcionDivisa.Peticion

            'Verifica se o código do Divisa existe no BD
            objPeticionVerificarDescricaoDivisa.DescripcionDivisa = txtDescricaoDivisa.Text
            objRespostaVerificarDescricaoDivisa = objProxyDivisa.VerificarDescripcionDivisa(objPeticionVerificarDescricaoDivisa)

            If Master.ControleErro.VerificaErro(objRespostaVerificarDescricaoDivisa.CodigoError, objRespostaVerificarDescricaoDivisa.NombreServidorBD, objRespostaVerificarDescricaoDivisa.MensajeError) Then
                Return objRespostaVerificarDescricaoDivisa.Existe
            Else
                Master.ControleErro.ShowError(objRespostaVerificarDescricaoDivisa.MensajeError) 'TODO: Exibe a mensagem de erro. Apenas para Debug. Retirar para Release.
                Return False
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Function

    ''' <summary>
    ''' Informa se a descrição do canal já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/06/2009 - Criado
    ''' </history>
    Private Function ExisteCodigoAccesoDivisa(CodigoAcceso As String) As Boolean

        Dim objRespostaVerificarCodigoAccesoDivisa As IAC.ContractoServicio.Utilidad.VerificarCodigoAccesoDivisa.Respuesta

        Try


            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                If txtCodigoAcesso.Text.Trim.Equals(CodigoAccesoAtual) Then
                    DescricaoExistente = False
                    Return False
                End If
            End If


            Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
            Dim objPeticionVerificarCodigoAccesoDivisa As New IAC.ContractoServicio.Utilidad.VerificarCodigoAccesoDivisa.Peticion

            'Verifica se o código do Divisa existe no BD
            objPeticionVerificarCodigoAccesoDivisa.CodigoAcceso = txtCodigoAcesso.Text
            objRespostaVerificarCodigoAccesoDivisa = objProxyUtilidad.VerificarCodigoAccesoDivisa(objPeticionVerificarCodigoAccesoDivisa)

            If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoAccesoDivisa.CodigoError, objRespostaVerificarCodigoAccesoDivisa.NombreServidorBD, objRespostaVerificarCodigoAccesoDivisa.MensajeError) Then
                Return objRespostaVerificarCodigoAccesoDivisa.Existe
            Else
                Master.ControleErro.ShowError(objRespostaVerificarCodigoAccesoDivisa.MensajeError) 'TODO: Exibe a mensagem de erro. Apenas para Debug. Retirar para Release.
                Return False
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Function

    Private Sub ConsomeCodigoAjeno()

        If Session("objRespuestaGEPR_TDIVISA") IsNot Nothing Then
            Dim objDivisaPeticion As New IAC.ContractoServicio.Divisa.SetDivisasDenominaciones.Peticion
            objDivisaPeticion.CodigoAjeno = Session("objRespuestaGEPR_TDIVISA")

            Session.Remove("objRespuestaGEPR_TDIVISA")

            Dim iCodigoAjeno = (From item In objDivisaPeticion.CodigoAjeno
                                Where item.BolDefecto = True).SingleOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            If objDivisaPeticion.CodigoAjeno IsNot Nothing Then
                CodigosAjenosPeticion = objDivisaPeticion.CodigoAjeno
            Else
                CodigosAjenosPeticion = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
            End If


            Session("objRespuestaGEPR_TDIVISA") = objDivisaPeticion.CodigoAjeno

        End If

    End Sub
    ''' <summary>
    ''' Consome a denominação passado pela PopUp de Mantenimiento de Denominaciones. 
    ''' Obs: A denominação só pode ser consumido no modo "Alta" ou "Modificacion"
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeDenominacion()

        If Session("objDenominacion") IsNot Nothing Then

            Dim objDenominacion As IAC.ContractoServicio.Divisa.Denominacion
            objDenominacion = DirectCast(Session("objDenominacion"), IAC.ContractoServicio.Divisa.Denominacion)

            'Se existe o denominação na coleção
            If Not VerificarDenominacionExiste(DenominacionesTemporario, objDenominacion.Codigo) Then
                DenominacionesTemporario.Add(objDenominacion)
            Else
                ModificaDenominacion(DenominacionesTemporario, objDenominacion)
            End If


            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                'Seta o estado da página corrente para modificação
                Acao = eAcaoEspecifica.AltaConRegistro
            End If


            'Carrega os SubCanais no GridView
            Dim objDT As DataTable
            objDT = GdvDenominaciones.ConvertListToDataTable(DenominacionesTemporario)
            GdvDenominaciones.CarregaControle(objDT)


            Session("objDenominacion") = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Verifica se uma denominação especifico existe na coleção informada
    ''' </summary>
    ''' <param name="objDenominaciones"></param>
    ''' <param name="codigoDenominacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>    
    Private Function VerificarDenominacionExiste(objDenominaciones As IAC.ContractoServicio.Divisa.DenominacionColeccion, codigoDenominacion As String) As Boolean

        Dim retorno = From c In objDenominaciones Where c.Codigo = codigoDenominacion

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' ' Retorna uma coleção de denominações que tem a mais no objeto temporario em relação ao clone e o que é foi modificado no temporario em relação ao clone
    ''' </summary>
    ''' <param name="objDenominacionesTemporario"></param>
    ''' <param name="objDenominacionesClone"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetornaDenominacionesEnvio(objDenominacionesTemporario As IAC.ContractoServicio.Divisa.DenominacionColeccion, objDenominacionesClone As IAC.ContractoServicio.Divisa.DenominacionColeccion) As IAC.ContractoServicio.Divisa.DenominacionColeccion

        ' Retorna o que tem a mais no temporario em relação ao clone e o que é foi modificado no temporario em relação ao clone
        Dim retorno = (From c As IAC.ContractoServicio.Divisa.Denominacion In objDenominacionesTemporario Join d As IAC.ContractoServicio.Divisa.Denominacion In objDenominacionesClone On c.Codigo Equals d.Codigo _
                            Where (c.Descripcion <> d.Descripcion OrElse c.Valor <> d.Valor OrElse c.Peso <> d.Peso OrElse c.EsBillete <> d.EsBillete OrElse c.Vigente <> d.Vigente OrElse c.CodigoAccesoDenominacion <> d.CodigoAccesoDenominacion) _
                            Select c.Codigo, c.Descripcion, c.Valor, c.Peso, c.EsBillete, c.Vigente, c.CodigoAccesoDenominacion, c.CodigosAjenos) _
                            .Union(From x As IAC.ContractoServicio.Divisa.Denominacion In objDenominacionesTemporario Where Not (From o As IAC.ContractoServicio.Divisa.Denominacion In objDenominacionesClone Select o.Codigo).Contains(x.Codigo) _
                            Select x.Codigo, x.Descripcion, x.Valor, x.Peso, x.EsBillete, x.Vigente, x.CodigoAccesoDenominacion, x.CodigosAjenos)


        Dim objDenominacionesCol As New IAC.ContractoServicio.Divisa.DenominacionColeccion

        Dim objDenominacionEnvio As IAC.ContractoServicio.Divisa.Denominacion
        For Each objRetorno As Object In retorno
            objDenominacionEnvio = New IAC.ContractoServicio.Divisa.Denominacion
            objDenominacionEnvio.Codigo = objRetorno.Codigo
            objDenominacionEnvio.Descripcion = objRetorno.Descripcion
            objDenominacionEnvio.Valor = objRetorno.Valor
            objDenominacionEnvio.Peso = objRetorno.Peso
            objDenominacionEnvio.EsBillete = objRetorno.EsBillete
            objDenominacionEnvio.Vigente = objRetorno.Vigente
            objDenominacionEnvio.CodigoAccesoDenominacion = objRetorno.CodigoAccesoDenominacion
            objDenominacionEnvio.CodigosAjenos = objRetorno.CodigosAjenos
            'Adiciona na coleção
            objDenominacionesCol.Add(objDenominacionEnvio)
        Next

        Return objDenominacionesCol

    End Function

    ''' <summary>
    ''' Modifica uma denominação existe na coleção informada
    ''' </summary>
    ''' <param name="objDenominacoes"></param>
    ''' <param name="objDenominacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ModificaDenominacion(ByRef objDenominacoes As IAC.ContractoServicio.Divisa.DenominacionColeccion, objDenominacion As IAC.ContractoServicio.Divisa.Denominacion) As Boolean

        Dim retorno = From c In objDenominacoes Where c.Codigo = objDenominacion.Codigo

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else

            Dim objDenominacionTmp As IAC.ContractoServicio.Divisa.Denominacion = DirectCast(retorno.ElementAt(0), IAC.ContractoServicio.Divisa.Denominacion)

            objDenominacionTmp.Descripcion = objDenominacion.Descripcion
            objDenominacionTmp.Peso = objDenominacion.Peso
            objDenominacionTmp.Valor = objDenominacion.Valor
            objDenominacionTmp.Vigente = objDenominacion.Vigente
            objDenominacionTmp.CodigoAccesoDenominacion = objDenominacion.CodigoAccesoDenominacion
            objDenominacionTmp.EsBillete = objDenominacion.EsBillete
            objDenominacion.CodigosAjenos = objDenominacion.CodigosAjenos
            Return True
        End If
    End Function

    ''' <summary>
    ''' Retorna uma denominação da coleção informada    
    ''' </summary>
    ''' <param name="objDenominaciones"></param>
    ''' <param name="codigoDenominacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetornaDenominacionExiste(ByRef objDenominaciones As IAC.ContractoServicio.Divisa.DenominacionColeccion, codigoDenominacion As String) As IAC.ContractoServicio.Divisa.Denominacion

        Dim retorno = From c In objDenominaciones Where c.Codigo = codigoDenominacion

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return Nothing
        Else
            Return retorno.ElementAt(0)
        End If

    End Function

    ''' <summary>
    '''  Envia a denominação para ser consumido pela PopUp de Mantenimento de Denominações
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDenominacionSelecionadoPopUp()

        'Cria o Divisa para ser consumido na página de Denominaciones
        Dim objDenominacion As New IAC.ContractoServicio.Divisa.Denominacion
        objDenominacion = RetornaDenominacionExiste(DenominacionesTemporario, GdvDenominaciones.getValorLinhaSelecionada)

        'Envia a denominação para ser consumido pela PopUp de Mantenimento de Denominações
        Session("setDenominacion") = objDenominacion

    End Sub

    ''' <summary>
    ''' Envia a coleção denominação para ser consumido pela PopUp de Mantenimento de Denominações
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDenominacionesTemporarioCollectionPopUp()

        'Envia a coleção denominação para ser consumido pela PopUp de Mantenimento de Denominações
        Session("colDenominacionesTemporario") = DenominacionesTemporario

    End Sub

#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()

        If ParametroMantenimientoClientesDivisasPorPantalla Then
            Select Case MyBase.Acao
                Case Aplicacao.Util.Utilidad.eAcao.Alta
                    btnAlta.Visible = True          '1
                    btnGrabar.Visible = True        '2
                    btnCancelar.Visible = True      '3

                    'Estado Inicial Controles                                
                    txtCodigoDivisa.Enabled = True

                    btnBaja.Visible = False         '4
                    btnModificacion.Visible = False '5
                    btnConsulta.Visible = False     '6 
                    btnVolver.Visible = False       '7

                    lblVigente.Visible = False
                    chkVigente.Checked = True
                    chkVigente.Enabled = False
                    chkVigente.Visible = False

                    btnGrabar.Habilitado = True
                    btnAlta.Habilitado = True

                    txtCodigoDivisa.Focus()

                Case Aplicacao.Util.Utilidad.eAcao.Baja
                    'Controles
                Case Aplicacao.Util.Utilidad.eAcao.Consulta
                    btnAlta.Visible = False                 '1
                    btnCancelar.Visible = False              '2

                    If DenominacionesTemporario.Count > 0 Then
                        btnConsulta.Visible = True          '3
                    Else
                        btnConsulta.Visible = False
                    End If
                    btnVolver.Visible = True                '4

                    'Estado Inicial Controles
                    txtCodigoDivisa.Enabled = False
                    txtDescricaoDivisa.Enabled = False
                    txtSimbolo.Enabled = False
                    lblVigente.Visible = True
                    chkVigente.Enabled = False

                    btnBaja.Visible = False                 '5
                    btnModificacion.Visible = False         '6
                    btnGrabar.Visible = False               '7

                Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                    txtCodigoDivisa.Enabled = False
                    chkVigente.Visible = True

                    btnAlta.Visible = True                 '2
                    If DenominacionesTemporario.Count > 0 Then
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



                Case eAcaoEspecifica.AltaConRegistro

                    btnModificacion.Visible = True         '1                
                    chkVigente.Visible = True

                    btnAlta.Visible = True                 '2
                    If DenominacionesTemporario.Count > 0 Then
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

                    btnCancelar.Visible = True             '6
                    btnVolver.Visible = False              '7

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
        Else
            Select Case MyBase.Acao

                Case Aplicacao.Util.Utilidad.eAcao.Consulta
                    btnAlta.Visible = False                 '1
                    btnCancelar.Visible = False              '2

                    If DenominacionesTemporario.Count > 0 Then
                        btnConsulta.Visible = True          '3
                    Else
                        btnConsulta.Visible = False
                    End If
                    btnVolver.Visible = True                '4

                    'Estado Inicial Controles
                    txtCodigoDivisa.Enabled = False
                    txtDescricaoDivisa.Enabled = False
                    txtSimbolo.Enabled = False
                    lblVigente.Visible = True
                    chkVigente.Enabled = False

                    btnBaja.Visible = False                 '5
                    btnModificacion.Visible = False         '6
                    btnGrabar.Visible = False               '7

                Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                    txtCodigoDivisa.Enabled = False
                    chkVigente.Visible = True

                    btnAlta.Visible = True                 '2
                    If DenominacionesTemporario.Count > 0 Then
                        btnBaja.Visible = False             '3
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
                    If chkVigente.Checked AndAlso EsVigente Then
                        chkVigente.Enabled = False
                    Else
                        chkVigente.Enabled = True
                    End If

                    btnGrabar.Habilitado = True
                    btnAlta.Habilitado = False
                    btnModificacion.Habilitado = True
                    btnConsulta.Habilitado = True
                    btnBaja.Habilitado = False

                Case eAcaoEspecifica.AltaConRegistro

                    btnModificacion.Visible = True         '1                
                    chkVigente.Visible = True

                    btnAlta.Visible = True                 '2
                    If DenominacionesTemporario.Count > 0 Then
                        btnBaja.Visible = True             '3
                        btnConsulta.Visible = True         '4
                    Else
                        btnBaja.Visible = False
                        btnConsulta.Visible = False
                    End If

                    btnGrabar.Habilitado = True
                    btnAlta.Habilitado = False
                    btnModificacion.Habilitado = True
                    btnConsulta.Habilitado = True
                    btnBaja.Habilitado = False

                    btnCancelar.Visible = True             '6
                    btnVolver.Visible = False              '7

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
        End If

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
    Private Property OidDivisa() As String
        Get
            Return ViewState("OidDivisa")
        End Get
        Set(value As String)
            ViewState("OidDivisa") = value
        End Set
    End Property
    Public ReadOnly Property DenominacionesTemporario() As IAC.ContractoServicio.Divisa.DenominacionColeccion
        Get
            If ViewState("DenominacionesTemporario") Is Nothing Then
                ViewState("DenominacionesTemporario") = New IAC.ContractoServicio.Divisa.DenominacionColeccion
            End If

            Return DirectCast(ViewState("DenominacionesTemporario"), IAC.ContractoServicio.Divisa.DenominacionColeccion)
        End Get
    End Property

    Private Property Divisas As ContractoServicio.Divisa.DivisaColeccion
        Get
            Return DirectCast(ViewState("Divisas"), ContractoServicio.Divisa.DivisaColeccion)
        End Get
        Set(value As ContractoServicio.Divisa.DivisaColeccion)
            ViewState("Divisas") = value
        End Set
    End Property

    Public Property DenominacionesClone() As IAC.ContractoServicio.Divisa.DenominacionColeccion
        Get
            If ViewState("DenominacionesClone") Is Nothing Then
                ViewState("DenominacionesClone") = New IAC.ContractoServicio.Divisa.DenominacionColeccion
            End If

            Return DirectCast(ViewState("DenominacionesClone"), IAC.ContractoServicio.Divisa.DenominacionColeccion)
        End Get
        Set(value As IAC.ContractoServicio.Divisa.DenominacionColeccion)
            ViewState("DenominacionesClone") = value
            ViewState("DenominacionesTemporario") = value
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

    Private Property CodigoAccesoExistente() As Boolean
        Get
            Return ViewState("CodigoAccesoExistente")
        End Get
        Set(value As Boolean)
            ViewState("CodigoAccesoExistente") = value
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