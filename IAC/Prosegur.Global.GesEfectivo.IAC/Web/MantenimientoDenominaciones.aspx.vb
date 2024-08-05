Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' PopUp de Manteniminento de Denominaciones 
''' </summary>
''' <remarks></remarks>
''' <history>[PDA] 04/02/09 - Criado</history>
Partial Public Class MantenimientoDenominaciones
    Inherits Base
    Dim ParametroMantenimientoClientesDivisasPorPantalla As Boolean

#Region "[Propriedades]"
    Private ReadOnly Property divModal() As String
        Get
            Return Request.QueryString("divModal").ToString()
        End Get
    End Property
    Private ReadOnly Property ifrModal() As String
        Get
            Return Request.QueryString("ifrModal").ToString()
        End Get
    End Property
    Private ReadOnly Property btnExecutar() As String
        Get
            Return Request.QueryString("btnExecutar").ToString()
        End Get
    End Property

#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

        ' btnCancelar.FuncaoJavascript = "window.close();"

        txtValorDenominacion.Attributes.Add("onkeyup", String.Format("moeda(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
        txtValorDenominacion.Attributes.Add("onblur", String.Format("VerificarNumeroDecimal(this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
        txtValorDenominacion.Attributes.Add("onkeypress", "bloqueialetras(event,this);")
        txtValorDenominacion.Attributes.Add("onKeyDown", "BloquearColar();")

        txtPesoDenominacion.Attributes.Add("onkeypress", "return ValorNumerico(event);")
        txtPesoDenominacion.Attributes.Add("onKeyDown", "BloquearColar();")

        'seta o foco para o proximo controle quando presciona o enter.
        txtCodigoDenominacion.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoDenominacion.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoDenominacion.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtValorDenominacion.ClientID & "').focus();return false;}} else {return true}; ")

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)
    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigoDenominacion.TabIndex = 1
        txtDescricaoDenominacion.TabIndex = 2
        txtValorDenominacion.TabIndex = 3
        txtPesoDenominacion.TabIndex = 4
        txtCodigoAcceso.TabIndex = 5
        chkIndicadorBilhete.TabIndex = 6
        chkVigente.TabIndex = 7
        btnGrabar.TabIndex = 8
        '       btnCancelar.TabIndex = 9

        Me.DefinirRetornoFoco(btnGrabar, txtCodigoDenominacion)

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

            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            If Not Page.IsPostBack Then

                ParametroMantenimientoClientesDivisasPorPantalla = Prosegur.Genesis.Parametros.Genesis.Parametros.ParametrosIntegracion.Where(Function(o) o.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_MANTENIMIENTO_CLIENTES_DIVISAS_POR_PANTALLA).First.ValorParametro

                'Possíveis Ações passadas pela página BusquedaCanales:
                ' [-] Aplicacao.Util.Utilidad.eAcao.Alta
                ' [-] Aplicacao.Util.Utilidad.eAcao.Modificacion
                ' [-] Aplicacao.Util.Utilidad.eAcao.Consulta

                If Not (MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta) Then

                    MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Erro

                    'Informa ao usuário que o parâmetro passado 
                    Throw New Exception(Traduzir("err_passagem_parametro"))

                End If

                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion _
                    OrElse MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    'Denominação passado na tela de "Mantenimiento de Divisas"
                    ConsomeDenominacion()
                    txtDescricaoDenominacion.Focus()
                Else
                    txtCodigoDenominacion.Focus()
                End If

                CodigoDivisa = Request.QueryString("CodigoDivisa")

                If Acao <> Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    ConsomeDenominacionesTemporario()
                End If


            End If
            ConsomeCodigoAjeno()


            TrataFoco()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    Protected Overrides Sub PreRenderizar()

        Try
            ControleBotoes()
            UpdatePanelCodigoDenominacion.Attributes.Add("style", "margin: 0px 0px 0px -2px !Important;")
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("005_titulo_mantenimiento_denominaciones")
        lblTituloDenominacion.Text = Traduzir("005_lbl_subtitulosmantenimientodenominacion")

        lblCodigoDenominacion.Text = Traduzir("005_lbl_denominacion_codigo")
        lblDescricaoDenominacion.Text = Traduzir("005_lbl_denominacion_descricao")
        lblCodigoAcceso.Text = Traduzir("005_lbl_denominacion_codigo_acceso")
        lblValor.Text = Traduzir("005_lbl_denominacion_valor")
        lblPeso.Text = Traduzir("005_lbl_denominacion_peso")
        lblIndicadorBilhete.Text = Traduzir("005_lbl_indicadorbilhete")
        lblVigente.Text = Traduzir("005_lbl_vigente")
        lblCodigoAjeno.Text = Traduzir("037_lbl_CodigoAjeno")
        lblDesCodigoAjeno.Text = Traduzir("037_lbl_DesCodigoAjeno")

        btnGrabar.Text = Traduzir("btnAceptar")
        btnAltaAjenoDenominacion.Text = Traduzir("btnCodigoAjeno")
        btnGrabar.ToolTip = Traduzir("btnAceptar")
        btnAltaAjenoDenominacion.ToolTip = Traduzir("btnCodigoAjeno")

        csvCodigoDenominacionObrigatorio.ErrorMessage = Traduzir("005_msg_denominacion_codigo_obligatorio")
        csvDescripcionDenominacionObrigatorio.ErrorMessage = Traduzir("005_msg_denominacion_descripcion_obligatorio")
        csvValorDenominacionObrigatorio.ErrorMessage = Traduzir("005_msg_denominacion_valor_obligatorio")
        csvCodigoDenominacionExistente.ErrorMessage = Traduzir("005_msg_denominacion_codigo_existente")

    End Sub

#End Region

#Region "[PROPRIEDADES]"
    Private Property OidDenominacion() As String
        Get
            Return ViewState("OidDenominacion")
        End Get
        Set(value As String)
            ViewState("OidDenominacion") = value
        End Set
    End Property
    Private Property CodigoValidado() As Boolean
        Get
            Return ViewState("CodigoValidado")
        End Get
        Set(value As Boolean)
            ViewState("CodigoValidado") = value
        End Set
    End Property

    Private Property CodigoDivisa() As String
        Get
            Return ViewState("CodigoDivisa")
        End Get
        Set(value As String)
            ViewState("CodigoDivisa") = value
        End Set
    End Property

    Private Property CodigoAccesoValidado() As Boolean
        Get
            Return ViewState("CodigoAccesoValidado")
        End Get
        Set(value As Boolean)
            ViewState("CodigoAccesoValidado") = value
        End Set
    End Property

    Private Property DescricaoValidada() As Boolean
        Get
            Return ViewState("DescricaoValidada")
        End Get
        Set(value As Boolean)
            ViewState("DescricaoValidada") = value
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

    Public Property Denominacion() As IAC.ContractoServicio.Divisa.Denominacion
        Get
            Return DirectCast(ViewState("setDenominacion"), IAC.ContractoServicio.Divisa.Denominacion)
        End Get
        Set(value As IAC.ContractoServicio.Divisa.Denominacion)
            ViewState("setDenominacion") = value
        End Set
    End Property

    Private Property DenominacionesTemporario() As IAC.ContractoServicio.Divisa.DenominacionColeccion
        Get
            Return ViewState("DenominacionesTemporario")
        End Get
        Set(value As IAC.ContractoServicio.Divisa.DenominacionColeccion)
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

    Private Property CodigosAjenosPeticion As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Get
            Return DirectCast(ViewState("CodigosAjenosPeticion"), ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
        End Get

        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
            ViewState("CodigosAjenosPeticion") = value
        End Set

    End Property


#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS BOTOES]"

    ''' <summary>
    ''' Clique do botão Grabar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnGrabar_Click(sender As Object, e As System.EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()
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


            ValidarCamposObrigatorios = True
            'Valida os campos          
            Dim strErro As String = MontaMensagensErro(True)
            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If

            'Grva conforme ação
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                Dim objDenominacion As New IAC.ContractoServicio.Divisa.Denominacion

                objDenominacion.Codigo = txtCodigoDenominacion.Text.Trim
                objDenominacion.Descripcion = txtDescricaoDenominacion.Text.Trim
                objDenominacion.CodigoAccesoDenominacion = txtCodigoAcceso.Text.Trim

                If txtValorDenominacion.Text.Trim.Equals(String.Empty) Then
                    objDenominacion.Valor = 0
                Else
                    objDenominacion.Valor = txtValorDenominacion.Text.Trim
                End If

                If txtPesoDenominacion.Text.Trim.Equals(String.Empty) Then
                    objDenominacion.Peso = 0
                Else
                    objDenominacion.Peso = txtPesoDenominacion.Text.Trim
                End If

                If Denominacion IsNot Nothing AndAlso Denominacion.CodigosAjenos IsNot Nothing Then
                    objDenominacion.CodigosAjenos = Denominacion.CodigosAjenos
                End If


                objDenominacion.EsBillete = chkIndicadorBilhete.Checked

                If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                    objDenominacion.Vigente = True
                Else
                    objDenominacion.Vigente = chkVigente.Checked
                End If

                Session("objDenominacion") = objDenominacion

                Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
                ' fechar janela
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ToleranciaOk", jsScript, True)

            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "Denominacione", "window.close();", True)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region
#End Region

#Region "[EVENTOS TEXTBOX]"

    ''' <summary>
    ''' Evento de mudança de texto do campo Código Denominação
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtCodigoDenominacion_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoDenominacion.TextChanged

        Try

            If ExisteCodigoDenominacion(txtCodigoDenominacion.Text) Then
                CodigoExistente = True
            Else
                CodigoExistente = False
            End If

            CodigoValidado = True

            Threading.Thread.Sleep(200)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub


    Private Sub txtCodigoAcceso_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoAcceso.TextChanged
        Try

            If ExisteCodigoAccesoDenominacion(txtCodigoDenominacion.Text) Then
                CodigoAccesoExistente = True
            Else
                CodigoAccesoExistente = False
            End If

            CodigoAccesoValidado = True

            Threading.Thread.Sleep(200)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub txtDescricaoDenominacion_TextChanged(sender As Object, e As System.EventArgs) Handles txtDescricaoDenominacion.TextChanged
        Threading.Thread.Sleep(200)
    End Sub

    Private Sub txtValorDenominacion_TextChanged(sender As Object, e As System.EventArgs) Handles txtValorDenominacion.TextChanged
        Threading.Thread.Sleep(200)
    End Sub

    Private Sub btnAltaAjenoDenominacion_Click(sender As Object, e As System.EventArgs) Handles btnAltaAjenoDenominacion.Click
        Try

            Dim url As String = String.Empty
            Dim tablaGenesis As New MantenimientoCodigosAjenos.CodigoAjenoSimples

            tablaGenesis.CodTablaGenesis = txtCodigoDenominacion.Text
            tablaGenesis.DesTablaGenesis = txtDescricaoDenominacion.Text
            tablaGenesis.OidTablaGenesis = OidDenominacion
            If Denominacion IsNot Nothing AndAlso Denominacion.CodigosAjenos IsNot Nothing Then
                tablaGenesis.CodigosAjenos = Denominacion.CodigosAjenos
            End If

            Session("objGEPR_TDENOMINACION") = tablaGenesis

            If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
                url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TDENOMINACION"
            Else
                url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TDENOMINACION"
            End If

            '   ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_Ajeno", "AbrirPopupModal('" & url & "', 550, 900,'btnAltaAjenoDenominacion');", True)
            Master.ExibirModal(url, Traduzir("034_titulo_codigo_ajeno"), 350, 750, False, True, btnConsomeCodigoAjeno.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region

#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()

        If ParametroMantenimientoClientesDivisasPorPantalla Then
            Select Case MyBase.Acao
                Case Aplicacao.Util.Utilidad.eAcao.Alta
                    txtCodigoDenominacion.Enabled = True
                    txtDescricaoDenominacion.Enabled = True
                    txtPesoDenominacion.Enabled = True
                    txtValorDenominacion.Enabled = True
                    chkIndicadorBilhete.Enabled = True

                    lblVigente.Visible = False
                    chkVigente.Visible = False
                    btnGrabar.Enabled = True

                    Me.DefinirRetornoFoco(btnGrabar, txtCodigoDenominacion)

                Case Aplicacao.Util.Utilidad.eAcao.Baja
                    'Controles
                Case Aplicacao.Util.Utilidad.eAcao.Consulta
                    txtCodigoDenominacion.Enabled = False
                    txtDescricaoDenominacion.Enabled = False
                    txtPesoDenominacion.Enabled = False
                    txtValorDenominacion.Enabled = False
                    chkIndicadorBilhete.Enabled = False

                    lblVigente.Visible = True
                    chkVigente.Visible = True
                    chkVigente.Enabled = False
                    btnGrabar.Visible = False


                Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                    txtCodigoDenominacion.Enabled = False
                    txtDescricaoDenominacion.Enabled = True
                    txtPesoDenominacion.Enabled = True
                    txtValorDenominacion.Enabled = True
                    chkIndicadorBilhete.Enabled = True

                    chkVigente.Visible = True
                    lblVigente.Visible = True
                    btnGrabar.Enabled = True

                    Me.DefinirRetornoFoco(btnGrabar, txtDescricaoDenominacion)

                Case Aplicacao.Util.Utilidad.eAcao.NoAction
                    'Controles
                Case Aplicacao.Util.Utilidad.eAcao.Inicial
                    'Controles
                Case Aplicacao.Util.Utilidad.eAcao.Busca
                    'Controles
                Case Aplicacao.Util.Utilidad.eAcao.Erro
                    btnGrabar.Visible = False
            End Select
        Else
            ' Permite el mantenimiento solamente del código de acceso y del peso de las denominaciones.
            'Permite consultar divisas y denominaciones.

            Select Case MyBase.Acao
                Case Aplicacao.Util.Utilidad.eAcao.Consulta
                    txtCodigoDenominacion.Enabled = False
                    txtDescricaoDenominacion.Enabled = False
                    txtPesoDenominacion.Enabled = False
                    txtValorDenominacion.Enabled = False
                    chkIndicadorBilhete.Enabled = False

                    lblVigente.Visible = True
                    chkVigente.Visible = True
                    chkVigente.Enabled = False
                    btnGrabar.Visible = False

                Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                    txtCodigoDenominacion.Enabled = False
                    txtDescricaoDenominacion.Enabled = False
                    txtPesoDenominacion.Enabled = True
                    txtValorDenominacion.Enabled = False
                    chkIndicadorBilhete.Enabled = False

                    chkVigente.Visible = True
                    chkVigente.Enabled = False
                    lblVigente.Visible = True
                    btnGrabar.Enabled = True

                    Me.DefinirRetornoFoco(btnGrabar, txtDescricaoDenominacion)

                Case Aplicacao.Util.Utilidad.eAcao.Erro
                    btnGrabar.Visible = False

            End Select
        End If
    End Sub

#End Region

#Region "[MÉTODOS]"
    Private Sub ConsomeCodigoAjeno()
        If Session("objRespuestaGEPR_TDENOMINACION") IsNot Nothing Then

            If Denominacion Is Nothing Then
                Denominacion = New IAC.ContractoServicio.Divisa.Denominacion
            End If
            Denominacion.CodigosAjenos = Session("objRespuestaGEPR_TDENOMINACION")

            Session.Remove("objRespuestaGEPR_TDENOMINACION")

            Dim iCodigoAjeno = (From item In Denominacion.CodigosAjenos
                                Where item.BolDefecto = True).SingleOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            Session("objRespuestaDENOMINACION") = Denominacion.CodigosAjenos

            If Denominacion.CodigosAjenos IsNot Nothing Then
                CodigosAjenosPeticion = Denominacion.CodigosAjenos
            Else
                CodigosAjenosPeticion = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
            End If
        End If

    End Sub
    ''' <summary>
    ''' Método responsável por consumir a denominação passada pela tela de Mantenimiento de Divisas
    ''' Obs: A denominação só é consumido no modo Modificacion ou Consulta
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeDenominacion()

        If Session("setDenominacion") IsNot Nothing Then

            Denominacion = DirectCast(Session("setDenominacion"), IAC.ContractoServicio.Divisa.Denominacion)

            txtCodigoDenominacion.Text = Denominacion.Codigo
            txtCodigoDenominacion.ToolTip = Denominacion.Codigo

            txtCodigoAcceso.Text = Denominacion.CodigoAccesoDenominacion
            txtCodigoAcceso.ToolTip = Denominacion.CodigoAccesoDenominacion
            'CodigosAjenosPeticion
            If Denominacion.CodigosAjenos IsNot Nothing Then
                Dim iCodigoAjeno As ContractoServicio.CodigoAjeno.CodigoAjenoBase = (From item In Denominacion.CodigosAjenos
                                    Where item.BolDefecto = True).FirstOrDefault()

                If iCodigoAjeno IsNot Nothing Then
                    txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                    txtCodigoAjeno.ToolTip = iCodigoAjeno.CodAjeno
                    txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
                    txtDesCodigoAjeno.ToolTip = iCodigoAjeno.DesAjeno
                End If
            End If

            txtDescricaoDenominacion.Text = Denominacion.Descripcion?.ToString()
            txtDescricaoDenominacion.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, Denominacion.Descripcion, String.Empty)

            txtValorDenominacion.Text = Denominacion.Valor?.ToString()
            txtValorDenominacion.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, Denominacion.Valor, String.Empty)

            txtPesoDenominacion.Text = Denominacion.Peso?.ToString()
            txtPesoDenominacion.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, Denominacion.Peso, String.Empty)

            chkIndicadorBilhete.Checked = Denominacion.EsBillete

            chkVigente.Checked = Denominacion.Vigente

            If Denominacion.Vigente Then
                chkVigente.Enabled = False
            End If


        End If
    End Sub

    Private Function ExisteCodigoDenominacion(codigoDenominacion As String) As Boolean

        Dim objRespostaVerificarDenominacion As IAC.ContractoServicio.Divisa.VerificarCodigoDenominacion.Respuesta
        Try
            Dim strCodDenominacion As String = txtCodigoDenominacion.Text.Trim

            Dim objProxyDivisa As New Comunicacion.ProxyDivisa
            Dim objPeticionVerificarCodigoDenominacion As New IAC.ContractoServicio.Divisa.VerificarCodigoDenominacion.Peticion

            'Verifica se o código do canal existe no BD
            objPeticionVerificarCodigoDenominacion.Codigo = strCodDenominacion

            objRespostaVerificarDenominacion = objProxyDivisa.VerificarCodigoDenominacion(objPeticionVerificarCodigoDenominacion)

            If Master.ControleErro.VerificaErro(objRespostaVerificarDenominacion.CodigoError, objRespostaVerificarDenominacion.NombreServidorBD, objRespostaVerificarDenominacion.MensajeError) Then

                If objRespostaVerificarDenominacion.Existe OrElse verificaCodigoDenominacionMemoria(strCodDenominacion) Then
                    Return True
                Else
                    Return False
                End If
            Else
                Master.ControleErro.ShowError(objRespostaVerificarDenominacion.MensajeError) 'TODO: Exibe a mensagem de erro. Apenas para Debug. Retirar para Release.
                MyBase.MostraMensagem(objRespostaVerificarDenominacion.MensajeError)
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Function

    Private Function ExisteCodigoAccesoDenominacion(codigoDenominacion As String) As Boolean

        Dim objRespostaVerificarCodigoAccesoDenominacion As IAC.ContractoServicio.Utilidad.VerificarCodigoAccesoDenominacion.Respuesta

        Try
            Dim strCodAccesoDenominacion As String = txtCodigoAcceso.Text.Trim

            Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
            Dim objPeticionVerificarCodigoAccesoDenominacion As New IAC.ContractoServicio.Utilidad.VerificarCodigoAccesoDenominacion.Peticion

            'Verifica se o código do canal existe no BD
            objPeticionVerificarCodigoAccesoDenominacion.CodigoAcceso = strCodAccesoDenominacion
            objPeticionVerificarCodigoAccesoDenominacion.CodigoDivisa = CodigoDivisa

            objRespostaVerificarCodigoAccesoDenominacion = objProxyUtilidad.VerificarCodigoAccesoDenominacion(objPeticionVerificarCodigoAccesoDenominacion)

            If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoAccesoDenominacion.CodigoError, objRespostaVerificarCodigoAccesoDenominacion.NombreServidorBD, objRespostaVerificarCodigoAccesoDenominacion.MensajeError) Then

                If objRespostaVerificarCodigoAccesoDenominacion.Existe OrElse verificaCodigoAccesoDenominacionMemoria(strCodAccesoDenominacion) Then
                    Return True
                Else
                    Return False
                End If
            Else
                Master.ControleErro.ShowError(objRespostaVerificarCodigoAccesoDenominacion.MensajeError) 'TODO: Exibe a mensagem de erro. Apenas para Debug. Retirar para Release.
                MyBase.MostraMensagem(objRespostaVerificarCodigoAccesoDenominacion.MensajeError)
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Function

    ''' <summary>
    ''' Trata do foco da página
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
    ''' MontaMensagensErro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            'Verifica se é para validar se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Verifica se o código do subcanal é obrigatório
                If txtCodigoDenominacion.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoDenominacionObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoDenominacionObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoDenominacion.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoDenominacionObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do subcanal é obrigatório
                If txtDescricaoDenominacion.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescripcionDenominacionObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescripcionDenominacionObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoDenominacion.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescripcionDenominacionObrigatorio.IsValid = True
                End If

                'Verifica se o valor da denominação foi preenchido
                If txtValorDenominacion.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvValorDenominacionObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvValorDenominacionObrigatorio.IsValid = False

                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtValorDenominacion.Focus()
                        focoSetado = True
                    End If

                Else
                    csvValorDenominacionObrigatorio.IsValid = True
                End If

            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If CodigoExistente Then

                strErro.Append(csvCodigoDenominacionExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoDenominacionExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoDenominacion.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoDenominacionExistente.IsValid = True
            End If



        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' Método responsável por consumir a coleção de denominações passada pela tela de Mantenimiento de Divisas
    ''' Obs: A coleção de denominações só é consumido no modo Modificacion ou Consulta
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeDenominacionesTemporario()

        If Session("colDenominacionesTemporario") IsNot Nothing Then

            DenominacionesTemporario = Session("colDenominacionesTemporario")
            Session("colDenominacionesTemporario") = Nothing

        End If

    End Sub

    ''' <summary>
    ''' Verifica se o código da denominação existe na memória
    ''' </summary>
    ''' <param name="codigoDenominacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function verificaCodigoDenominacionMemoria(codigoDenominacion As String) As Boolean

        Dim retorno = From c In DenominacionesTemporario Where c.Codigo = codigoDenominacion

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' Verifica se o código da denominação existe na memória
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function verificaCodigoAccesoDenominacionMemoria(codigoAccesoDenominacion As String) As Boolean

        Dim retorno = From c In DenominacionesTemporario Where c.CodigoAccesoDenominacion = codigoAccesoDenominacion

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function


#End Region

    Private Sub btnConsomeCodigoAjeno_Click(sender As Object, e As EventArgs) Handles btnConsomeCodigoAjeno.Click
        Try
            ConsomeCodigoAjeno()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
End Class