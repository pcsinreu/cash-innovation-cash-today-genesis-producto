Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' Página de Busca de Mantenimiento de Terminos 
''' </summary>
''' <remarks></remarks>
''' <history>[PDA] 17/02/09 - Criado</history>
Partial Public Class MantenimientoTerminos
    Inherits Base

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()
        'Adicionar scripts na página
        txtLongitud.Attributes.Add("onkeypress", "return ValorNumerico(event);")
        'seta o foco para o proximo controle quando presciona o enter.
        txtCodigoTermino.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoTermino.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoTermino.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtObservaciones.ClientID & "').focus();return false;}} else {return true}; ")
        txtObservaciones.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onblur", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onkeyup", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtLongitud.Attributes.Add("onKeyDown", "BloquearColar();")

        'Controle de precedência(Ajax)
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)
    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigoTermino.TabIndex = 1
        txtDescricaoTermino.TabIndex = 2
        txtObservaciones.TabIndex = 3
        chkMostrarCodigo.TabIndex = 4
        chkVigente.TabIndex = 5
        ddlTipoFormato.TabIndex = 6
        txtLongitud.TabIndex = 7

        rbtSinValidacion.TabIndex = 8
        rbtMascara.TabIndex = 9
        rbtListaValores.TabIndex = 10
        rbtFormula.TabIndex = 11

        ddlMascara.TabIndex = 12
        ddlAlgoritmo.TabIndex = 13

        btnGrabar.TabIndex = 14
        btnCancelar.TabIndex = 15
        btnVolver.TabIndex = 16

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.TERMINOS_IAC

    End Sub

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                'Recebe o código do Termino
                Dim strCodTermino As String = Request.QueryString("codTermino")

                'Possíveis Ações passadas pela página BusquedaTerminos:
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
                PreencherComboTipoFormato()

                If strCodTermino <> String.Empty Then
                    'Estado Inicial dos control
                    CarregaDados(strCodTermino)
                End If

                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion _
                    OrElse MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    txtDescricaoTermino.Focus()
                Else
                    txtCodigoTermino.Focus()
                    rbtSinValidacion.Checked = True
                End If

            End If

            'Trata o foco dos campos
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

        Master.TituloPagina = Traduzir("010_titulo_mantenimiento_terminos")
        lblCodigoTermino.Text = Traduzir("010_lbl_codigotermino")
        lblDescricaoTermino.Text = Traduzir("010_lbl_descricaotermino")
        lblVigente.Text = Traduzir("010_lbl_vigente")
        lblTituloTermino.Text = Traduzir("010_lbl_titulosterminos")
        lblObservaciones.Text = Traduzir("010_lbl_observaciones")

        csvCodigoObrigatorio.ErrorMessage = Traduzir("010_msg_terminocodigoobligatorio")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("010_msg_terminodescripcionobligatorio")
        csvDescripcionExistente.ErrorMessage = Traduzir("010_msg_descricaoterminoexistente")
        csvCodigoTerminoExistente.ErrorMessage = Traduzir("010_msg_codigoterminoexistente")
        csvTipoFormatoObrigatorio.ErrorMessage = Traduzir("010_msg_formatoobligatorio")
        csvLongitudeObrigatorio.ErrorMessage = Traduzir("010_msg_longitudobligatoria")
        csvLongitude.ErrorMessage = String.Format(Traduzir("010_msg_longitudvalorinvalido"), Integer.MaxValue)
        csvMascaraObrigatorio.ErrorMessage = Traduzir("010_msg_mascaraobligatoria")
        csvAlgoritmoObrigatorio.ErrorMessage = Traduzir("010_msg_algoritmoobligatorio")

        lblLongitud.Text = Traduzir("010_lbl_longitud")
        lblMostrarCodigo.Text = Traduzir("010_lbl_mostrarcodigo")
        lblTipoFormato.Text = Traduzir("010_lbl_tipoformato")
        lblValidacao.Text = Traduzir("010_lbl_validacao")
        lblAceptarDigitacion.Text = Traduzir("010_lbl_listavaloreseditable")

        rbtFormula.Text = Traduzir("010_lbl_formula")
        rbtListaValores.Text = Traduzir("010_lbl_listavalores")
        rbtMascara.Text = Traduzir("010_lbl_mascara")
        rbtSinValidacion.Text = Traduzir("010_lbl_sinvalidacion")

    End Sub

#End Region

#Region "[DADOS]"

    Public Function getTerminosDetail(codigoTermino As String) As IAC.ContractoServicio.TerminoIac.GetTerminoDetailIac.TerminoDetailIacColeccion

        Dim objProxyTermino As New Comunicacion.ProxyTermino
        Dim objPeticionTermino As New IAC.ContractoServicio.TerminoIac.GetTerminoDetailIac.Peticion
        Dim objRespuestaTermino As IAC.ContractoServicio.TerminoIac.GetTerminoDetailIac.Respuesta

        'Recebe os valores do filtro
        Dim lista As New List(Of String)
        lista.Add(codigoTermino)

        objPeticionTermino.CodigoTermino = lista

        objRespuestaTermino = objProxyTermino.getTerminoDetail(objPeticionTermino)

        Return objRespuestaTermino.TerminosDetailIac

    End Function

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS BOTÕES]"

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

    'Ações que podem ser chamadas a qualquer momento
#Region "Ações Botões Independentes"

    ''' <summary>
    ''' Função do botão grabar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 07/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarGrabar()
        Try

            Dim objProxyTermino As New Comunicacion.ProxyTermino
            Dim objRespuestaTermino As IAC.ContractoServicio.TerminoIac.SetTerminoIac.Respuesta
            Dim strErro As New Text.StringBuilder(String.Empty)

            ValidarCamposObrigatorios = True
            ValidarMascara = True
            ValidarAlgoritimo = True

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If

            Dim objTerminoPeticion As New IAC.ContractoServicio.TerminoIac.SetTerminoIac.Peticion
            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objTerminoPeticion.Vigente = True
            Else
                objTerminoPeticion.Vigente = chkVigente.Checked
            End If

            ' atualizar propriedade vigente
            EsVigente = chkVigente.Checked

            objTerminoPeticion.Codigo = txtCodigoTermino.Text.Trim
            objTerminoPeticion.Descripcion = txtDescricaoTermino.Text.Trim
            objTerminoPeticion.Observacion = txtObservaciones.Text
            objTerminoPeticion.AceptarDigitacion = chkAceptarDigitacion.Checked

            If String.IsNullOrEmpty(txtLongitud.Text.Trim) Then
                objTerminoPeticion.Longitud = Nothing
            Else
                objTerminoPeticion.Longitud = txtLongitud.Text
            End If

            objTerminoPeticion.MostrarCodigo = chkMostrarCodigo.Checked
            objTerminoPeticion.CodigoFormato = ddlTipoFormato.SelectedValue

            'Valida Radio Button´s
            If rbtMascara.Checked Then
                objTerminoPeticion.CodigoMascara = ddlMascara.SelectedValue
                objTerminoPeticion.CodigoAlgoritmo = String.Empty
                objTerminoPeticion.AdmiteValoresPosibles = False
            ElseIf rbtFormula.Checked Then
                objTerminoPeticion.CodigoAlgoritmo = ddlAlgoritmo.SelectedValue
                objTerminoPeticion.CodigoMascara = String.Empty
                objTerminoPeticion.AdmiteValoresPosibles = False
            ElseIf rbtListaValores.Checked Then
                objTerminoPeticion.CodigoAlgoritmo = String.Empty
                objTerminoPeticion.CodigoMascara = String.Empty
                objTerminoPeticion.AdmiteValoresPosibles = True
            Else
                objTerminoPeticion.CodigoAlgoritmo = String.Empty
                objTerminoPeticion.CodigoMascara = String.Empty
                objTerminoPeticion.AdmiteValoresPosibles = False
            End If

            'Informação do usuário corrente
            objTerminoPeticion.CodigoUsuario = MyBase.LoginUsuario

            objRespuestaTermino = objProxyTermino.setTermino(objTerminoPeticion)

            'Define a ação de busca somente se houve retorno de canais
            If Master.ControleErro.VerificaErro(objRespuestaTermino.CodigoError, objRespuestaTermino.NombreServidorBD, objRespuestaTermino.MensajeError) Then
                Response.Redirect("~/BusquedaTerminos.aspx", False)
            Else
                If objRespuestaTermino.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    Master.ControleErro.ShowError(objRespuestaTermino.MensajeError, False)
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
    ''' [anselmo.gois] 07/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarCancelar()
        Response.Redirect("~/BusquedaTerminos.aspx", False)
    End Sub

    ''' <summary>
    ''' Função do botão volver.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 07/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarVolver()
        Response.Redirect("~/BusquedaTerminos.aspx", False)
    End Sub

#End Region

#End Region

#Region "[EVENTOS TEXTBOX]"

    ''' <summary>
    ''' Evento de mudanção de texto do campo Código  Termino
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtCodigoTermino_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoTermino.TextChanged
        Try

            If ExisteCodigoTermino(txtCodigoTermino.Text) Then
                CodigoExistente = True
            Else
                CodigoExistente = False
            End If

            Threading.Thread.Sleep(200)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try


    End Sub

    ''' <summary>
    ''' Evento de mudança de texto do campo Descrição Termino
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtDescricaoTermino_TextChanged(sender As Object, e As System.EventArgs) Handles txtDescricaoTermino.TextChanged

        'Try

        '    If ExisteDescricaoTermino(txtDescricaoTermino.Text) Then
        '        DescricaoExistente = True
        '    Else
        '        DescricaoExistente = False
        '    End If

        '    Threading.Thread.Sleep(200)

        'Catch ex As Exception
        '    Master.ControleErro.TratarErroException(ex)
        'End Try

    End Sub

    Private Sub txtLongitud_TextChanged(sender As Object, e As System.EventArgs) Handles txtLongitud.TextChanged
        Threading.Thread.Sleep(200)
    End Sub


    Private Sub ddlTipoFormato_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlTipoFormato.SelectedIndexChanged
        Threading.Thread.Sleep(200)

        ddlTipoFormato.ToolTip = ddlTipoFormato.SelectedItem.Text

    End Sub

    Private Sub ddlMascara_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlMascara.SelectedIndexChanged
        Threading.Thread.Sleep(200)

        ddlMascara.ToolTip = ddlMascara.SelectedItem.Text

    End Sub

    Protected Sub ddlAlgoritmo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAlgoritmo.SelectedIndexChanged

        'Se o tipo de algoritmo selecionado for Clave Rib
        If TryCast(sender, DropDownList).SelectedValue.ToUpper() = ContractoServicio.Constantes.COD_ALGORITMO_VALIDACION_RIB.ToUpper() Then

            'Seta automaticamente o valor dos campos
            ddlTipoFormato.SelectedIndex = enumTipoFormato.Texto
            txtLongitud.Text = ContractoServicio.Constantes.CONST_VALOR_LONGITUDE_CLAVE_RIB

            'Desabilita os campos 
            ddlTipoFormato.Enabled = False
            txtLongitud.ReadOnly = True
            txtLongitud.Enabled = False

        Else

            'Seta automaticamente o valor dos campos
            ddlTipoFormato.SelectedIndex = 0
            txtLongitud.Text = String.Empty

            'Se o tipo não for Clave RIB os campos são habilitados
            ddlTipoFormato.Enabled = True
            txtLongitud.ReadOnly = False
            txtLongitud.Enabled = True

        End If

        'Atualizando os campos alterados
        UpdatePanelTipoFormato.Update()
        UpdatePanelLongitud.Update()

        ddlAlgoritmo.ToolTip = ddlAlgoritmo.SelectedItem.Text

    End Sub

#End Region

#Region "[EVENTOS RADIOBUTTON]"

    ''' <summary>
    ''' Evento de mudança de valor do RadioButton
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rbtMascara_CheckedChanged(sender As Object, e As EventArgs) Handles rbtMascara.CheckedChanged

        If rbtMascara.Checked Then
            ValidarMascara = False
            PreencherComboMascara()
        Else
            ddlMascara.Items.Clear()
            ddlMascara.Items.Add(New ListItem(Traduzir("010_ddl_selecione"), String.Empty))
        End If

    End Sub

    ''' <summary>
    ''' Evento de mudança de valor do RadioButton
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rbtFormula_CheckedChanged(sender As Object, e As EventArgs) Handles rbtFormula.CheckedChanged

        If rbtFormula.Checked Then
            ValidarAlgoritimo = False
            PreencherComboAlgoritmo()
        Else
            ddlAlgoritmo.Items.Clear()
            ddlAlgoritmo.Items.Add(New ListItem(Traduzir("010_ddl_selecione"), String.Empty))

        End If

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
    Public Sub PreencherComboTipoFormato()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboFormatos.Respuesta
        objRespuesta = objProxyUtilida.GetComboFormatos

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ddlTipoFormato.AppendDataBoundItems = True
        ddlTipoFormato.Items.Clear()
        ddlTipoFormato.Items.Add(New ListItem(Traduzir("010_ddl_selecione"), String.Empty))
        ddlTipoFormato.DataTextField = "Descripcion"
        ddlTipoFormato.DataValueField = "Codigo"
        ddlTipoFormato.DataSource = objRespuesta.Formatos
        ddlTipoFormato.DataBind()

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Public Sub PreencherComboMascara()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboMascaras.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboMascaras.Respuesta
        objPeticion.AplicaTerminosIac = True

        objRespuesta = objProxyUtilida.GetComboMascaras(objPeticion)


        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ddlMascara.AppendDataBoundItems = True
        ddlMascara.Items.Clear()
        ddlMascara.Items.Add(New ListItem(Traduzir("010_ddl_selecione"), String.Empty))
        ddlMascara.DataTextField = "Descripcion"
        ddlMascara.DataValueField = "Codigo"
        ddlMascara.DataSource = objRespuesta.Mascaras
        ddlMascara.DataBind()

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
    ''' Informa se o código do canal já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 01/07/2009 - Criado
    ''' </history>
    Private Function ExisteCodigoTermino(codigo As String) As Boolean
        Dim objRespostaVerificarCodigoTermino As IAC.ContractoServicio.TerminoIac.VerificarCodigoTermino.Respuesta
        Try

            Dim objProxyTermino As New Comunicacion.ProxyTermino
            Dim objPeticionVerificarCodigoTermino As New IAC.ContractoServicio.TerminoIac.VerificarCodigoTermino.Peticion

            'Verifica se o código do Termino existe no BD
            objPeticionVerificarCodigoTermino.Codigo = codigo
            objRespostaVerificarCodigoTermino = objProxyTermino.VerificarCodigoTermino(objPeticionVerificarCodigoTermino)

            If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoTermino.CodigoError, objRespostaVerificarCodigoTermino.NombreServidorBD, objRespostaVerificarCodigoTermino.MensajeError) Then
                Return objRespostaVerificarCodigoTermino.Existe
            Else
                Master.ControleErro.ShowError(objRespostaVerificarCodigoTermino.MensajeError)
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
    ''' [carlos.bomtempo] 05/06/2009 - Criado
    ''' </history>
    Private Function ExisteDescricaoTermino(descricao As String) As Boolean

        Dim objRespostaVerificarDescricaoTermino As IAC.ContractoServicio.TerminoIac.VerificarDescripcionTermino.Respuesta

        Try
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                If descricao.Trim.Equals(DescricaoAtual) Then
                    DescricaoExistente = False
                    Exit Function
                End If
            End If

            Dim objProxyTermino As New Comunicacion.ProxyTermino
            Dim objPeticionVerificarDescricaoTermino As New IAC.ContractoServicio.TerminoIac.VerificarDescripcionTermino.Peticion

            'Verifica se o código do Termino existe no BD
            objPeticionVerificarDescricaoTermino.Descripcion = descricao.Trim
            objRespostaVerificarDescricaoTermino = objProxyTermino.VerificarDescripcionTermino(objPeticionVerificarDescricaoTermino)

            If Master.ControleErro.VerificaErro(objRespostaVerificarDescricaoTermino.CodigoError, objRespostaVerificarDescricaoTermino.NombreServidorBD, objRespostaVerificarDescricaoTermino.MensajeError) Then
                Return objRespostaVerificarDescricaoTermino.Existe
            Else
                Master.ControleErro.ShowError(objRespostaVerificarDescricaoTermino.MensajeError) 'TODO: Exibe a mensagem de erro. Apenas para Debug. Retirar para Release.
                Return False
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Function

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Public Sub PreencherComboAlgoritmo()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboAlgoritmos.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboAlgoritmos.Respuesta
        objPeticion.AplicaTerminosIac = True

        objRespuesta = objProxyUtilida.GetComboAlgoritmos(objPeticion)


        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ddlAlgoritmo.AppendDataBoundItems = True
        ddlAlgoritmo.Items.Clear()
        ddlAlgoritmo.Items.Add(New ListItem(Traduzir("010_ddl_selecione"), String.Empty))
        ddlAlgoritmo.DataTextField = "Descripcion"
        ddlAlgoritmo.DataValueField = "Codigo"
        ddlAlgoritmo.DataSource = objRespuesta.Algoritmos
        ddlAlgoritmo.DataBind()

    End Sub

    ''' <summary>
    ''' Carrega os dados do gridView quando a página é carregada pela primeira vez
    ''' </summary>
    ''' <param name="codTermino"></param>
    ''' <remarks></remarks>
    Public Sub CarregaDados(codTermino As String)

        Dim objColTermino As IAC.ContractoServicio.TerminoIac.GetTerminoDetailIac.TerminoDetailIacColeccion
        Dim itemSelecionado As ListItem
        objColTermino = getTerminosDetail(codTermino)

        If objColTermino.Count > 0 Then

            'Preenche os controles do formulario
            txtCodigoTermino.Text = objColTermino(0).Codigo
            txtCodigoTermino.ToolTip = objColTermino(0).Codigo

            txtDescricaoTermino.Text = objColTermino(0).Descripcion
            txtDescricaoTermino.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objColTermino(0).Descripcion, String.Empty)

            txtObservaciones.Text = objColTermino(0).Observacion
            txtObservaciones.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objColTermino(0).Observacion, String.Empty)

            chkVigente.Checked = objColTermino(0).Vigente
            chkMostrarCodigo.Checked = objColTermino(0).MostrarCodigo
            chkAceptarDigitacion.Checked = objColTermino(0).AceptarDigitiacion

            txtLongitud.Text = objColTermino(0).Longitud
            txtLongitud.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objColTermino(0).Longitud, String.Empty)

            EsVigente = objColTermino(0).Vigente

            'Seleciona o valor
            itemSelecionado = ddlTipoFormato.Items.FindByValue(objColTermino(0).CodigoFormato)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
            End If

            'Preenche os radiobutton's
            If objColTermino(0).ValoresPosibles Then
                rbtListaValores.Checked = True
            Else
                If objColTermino(0).CodigoAlgoritmo <> String.Empty Then
                    rbtFormula.Checked = True
                    PreencherComboAlgoritmo()

                    If ddlAlgoritmo.Items.Count > 0 Then
                        itemSelecionado = ddlAlgoritmo.Items.FindByValue(objColTermino(0).CodigoAlgoritmo)
                        If itemSelecionado IsNot Nothing Then
                            itemSelecionado.Selected = True
                        End If
                    End If


                ElseIf objColTermino(0).CodigoMascara <> String.Empty Then
                    rbtMascara.Checked = True
                    PreencherComboMascara()
                    itemSelecionado = ddlMascara.Items.FindByValue(objColTermino(0).CodigoMascara)
                    If itemSelecionado IsNot Nothing Then
                        itemSelecionado.Selected = True
                    End If

                Else
                    rbtSinValidacion.Checked = True
                End If


            End If

            'Demais campos

            'Se for modificação então guarda a descriçaõ atual para validação
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                DescricaoAtual = txtDescricaoTermino.Text
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

                'Verifica se o código do termino é obrigatório
                If txtCodigoTermino.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoTermino.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do termino é obrigatório
                If txtDescricaoTermino.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoTermino.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

                'Verifica se a longitude do termino é obrigatório
                If ddlTipoFormato.SelectedValue.Trim.Equals(String.Empty) Then

                    strErro.Append(csvTipoFormatoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTipoFormatoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlTipoFormato.Focus()
                        focoSetado = True
                    End If

                    'Verifica se a longitude do termino é obrigatório
                    If txtLongitud.Text.Trim.Equals(String.Empty) Then

                        strErro.Append(csvLongitudeObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                        csvLongitudeObrigatorio.IsValid = False

                        'Seta o foco no primeiro controle que deu erro
                        If SetarFocoControle AndAlso Not focoSetado Then
                            txtLongitud.Focus()
                            focoSetado = True
                        End If
                    ElseIf Not IsNumeric(txtLongitud.Text) OrElse Convert.ToInt64(txtLongitud.Text) < 1 OrElse Convert.ToInt64(txtLongitud.Text) > Integer.MaxValue Then

                        strErro.Append(csvLongitude.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                        csvLongitude.IsValid = False

                        'Seta o foco no primeiro controle que deu erro
                        If SetarFocoControle AndAlso Not focoSetado Then
                            txtLongitud.Focus()
                            focoSetado = True
                        End If

                    Else
                        csvLongitude.IsValid = True
                    End If

                Else

                    csvTipoFormatoObrigatorio.IsValid = True

                    If ddlTipoFormato.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_TEXTO) OrElse _
                        ddlTipoFormato.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_DECIMAL) OrElse _
                        ddlTipoFormato.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_ENTERO) Then

                        'Verifica se a longitude do termino é obrigatório
                        If txtLongitud.Text.Trim.Equals(String.Empty) Then

                            strErro.Append(csvLongitudeObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                            csvLongitudeObrigatorio.IsValid = False

                            'Seta o foco no primeiro controle que deu erro
                            If SetarFocoControle AndAlso Not focoSetado Then
                                txtLongitud.Focus()
                                focoSetado = True
                            End If

                        Else

                            csvLongitude.IsValid = True

                            If ddlTipoFormato.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_TEXTO) AndAlso (Convert.ToInt32(txtLongitud.Text) < 1 OrElse Convert.ToInt32(txtLongitud.Text) > ContractoServicio.Constantes.MAX_LONGITUDE) Then

                                strErro.Append(String.Format(Traduzir("010_msg_longitudvalorinvalido"), 255) & Aplicacao.Util.Utilidad.LineBreak)
                                csvLongitude.IsValid = False

                            ElseIf ddlTipoFormato.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_ENTERO) AndAlso (Convert.ToInt32(txtLongitud.Text) < 1 OrElse Convert.ToInt32(txtLongitud.Text) > Len(Int32.MaxValue.ToString)) Then

                                strErro.Append(String.Format(Traduzir("010_msg_longitudvalorinvalido"), Len(Int32.MaxValue.ToString)) & Aplicacao.Util.Utilidad.LineBreak)
                                csvLongitude.IsValid = False

                            ElseIf ddlTipoFormato.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_DECIMAL) AndAlso (Convert.ToInt32(txtLongitud.Text) < 1 OrElse Convert.ToInt32(txtLongitud.Text) > Len(Decimal.MaxValue.ToString) - 1) Then

                                strErro.Append(String.Format(Traduzir("010_msg_longitudvalorinvalido"), Len(Decimal.MaxValue.ToString) - 1) & Aplicacao.Util.Utilidad.LineBreak)
                                csvLongitude.IsValid = False

                            End If

                            'Seta o foco no primeiro controle que deu erro
                            If SetarFocoControle AndAlso Not focoSetado Then
                                txtLongitud.Focus()
                                focoSetado = True
                            End If

                        End If

                    Else
                        csvLongitude.IsValid = True
                    End If

                End If


                'Verifica se a longitude do termino é obrigatório
                If ddlMascara.Visible AndAlso ddlMascara.SelectedValue.Equals(String.Empty) AndAlso ValidarMascara Then

                    strErro.Append(csvMascaraObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvMascaraObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlMascara.Focus()
                        focoSetado = True
                    End If

                Else

                    csvMascaraObrigatorio.IsValid = True

                End If

                'Verifica se a longitude do termino é obrigatório
                If ddlAlgoritmo.Visible AndAlso ddlAlgoritmo.SelectedValue.Equals(String.Empty) AndAlso ValidarAlgoritimo Then

                    strErro.Append(csvAlgoritmoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvAlgoritmoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlAlgoritmo.Focus()
                        focoSetado = True
                    End If

                Else
                    csvAlgoritmoObrigatorio.IsValid = True
                End If

            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If CodigoExistente Then

                strErro.Append(csvCodigoTerminoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoTerminoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoTermino.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoTerminoExistente.IsValid = True
            End If

            'Verifica se a descrição existe
            If DescricaoExistente Then

                strErro.Append(csvDescripcionExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescripcionExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescricaoTermino.Focus()
                    focoSetado = True
                End If

            Else
                csvDescripcionExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function

    Public Sub HabilitaDesabilitaLongitud()

        If ddlTipoFormato.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_FECHA) OrElse _
           ddlTipoFormato.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_FECHA_HORA) OrElse _
           ddlTipoFormato.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_BOLEANO) Then

            txtLongitud.Text = String.Empty
            txtLongitud.Enabled = False
        Else

            'Verifica se o campo longitude está somente leitura, isto acontece quando o tipo de algoritmo é selecionado
            If Not txtLongitud.ReadOnly Then
                txtLongitud.Enabled = True
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
                txtCodigoTermino.Enabled = True
                btnVolver.Visible = False       '3
                lblVigente.Visible = False
                chkVigente.Checked = True
                chkVigente.Enabled = False
                chkVigente.Visible = False
                btnGrabar.Habilitado = True
                HabilitaDesabilitaLongitud()

                'Valida os combos
                If rbtFormula.Checked Then
                    ddlAlgoritmo.Visible = True
                Else
                    ddlAlgoritmo.Visible = False
                End If


                'Valida os combos
                If rbtMascara.Checked Then
                    ddlMascara.Visible = True
                Else
                    ddlMascara.Visible = False
                End If

            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta

                btnCancelar.Visible = False              '2

                btnVolver.Visible = True                 '3

                'Estado Inicial Controles
                txtCodigoTermino.Enabled = False
                txtDescricaoTermino.Enabled = False
                txtObservaciones.Enabled = False
                lblVigente.Visible = True
                chkVigente.Enabled = False
                btnGrabar.Visible = False                '1

                'Valida os combos
                If rbtFormula.Checked Then
                    ddlAlgoritmo.Visible = True
                    ddlAlgoritmo.Enabled = False
                Else
                    ddlAlgoritmo.Visible = False
                End If

                'Valida os combos
                If rbtMascara.Checked Then
                    ddlMascara.Visible = True
                    ddlMascara.Enabled = False
                Else
                    ddlMascara.Visible = False
                End If

                ddlTipoFormato.Enabled = False
                ddlTipoFormato.ToolTip = ddlTipoFormato.SelectedItem.Text

                chkMostrarCodigo.Enabled = False
                txtLongitud.Enabled = False
                rbtFormula.Enabled = False
                rbtListaValores.Enabled = False
                rbtSinValidacion.Enabled = False
                rbtMascara.Enabled = False
                chkAceptarDigitacion.Enabled = False

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                txtCodigoTermino.Enabled = False
                chkVigente.Visible = True
                btnGrabar.Visible = True               '1
                btnCancelar.Visible = True             '2
                btnVolver.Visible = False              '3
                lblVigente.Visible = True

                If chkVigente.Checked AndAlso EsVigente Then
                    chkVigente.Enabled = False
                Else
                    chkVigente.Enabled = True
                End If

                btnGrabar.Habilitado = True

                'Valida os combos
                If rbtFormula.Checked Then
                    ddlAlgoritmo.Visible = True
                Else
                    ddlAlgoritmo.Visible = False
                End If

                'Valida os combos
                If rbtMascara.Checked Then
                    ddlMascara.Visible = True
                Else
                    ddlMascara.Visible = False
                End If
                HabilitaDesabilitaLongitud()


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

    Private Property ValidarMascara() As Boolean
        Get
            Return ViewState("ValidarMascara")
        End Get
        Set(value As Boolean)
            ViewState("ValidarMascara") = value
        End Set
    End Property

    Private Property ValidarAlgoritimo() As Boolean
        Get
            Return ViewState("ValidarAlgoritimo")
        End Get
        Set(value As Boolean)
            ViewState("ValidarAlgoritimo") = value
        End Set
    End Property

    ''' <summary>
    ''' Enumerador com os possíveis tipo de formato para a fórmula em Términos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 07/12/2010 Created
    ''' </history>
    Private Enum enumTipoFormato As Integer
        Texto = 1
        Entero = 2
        Decima = 3
        Fecha = 4
        FechaYHora = 5
        Booleano = 6
    End Enum

#End Region

End Class