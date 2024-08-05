Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis


Public Class MantenimientoParametro
    Inherits Base

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Propriedades da pagina
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 08/09/2011 - Criado
    ''' </history>

    Property CodAplicacion() As String
        Get
            Return ViewState("CodAplicacion")
        End Get
        Set(value As String)
            ViewState("CodAplicacion") = value
        End Set
    End Property

    Property CodParametro() As String
        Get
            Return ViewState("CodParametro")
        End Get
        Set(value As String)
            ViewState("CodParametro") = value
        End Set
    End Property

    Property CodigoNivel() As String
        Get
            Return ViewState("CodigoNivel")
        End Get
        Set(value As String)
            ViewState("CodigoNivel") = value
        End Set
    End Property

    Property DescripcionAgrupacion() As String
        Get
            Return ViewState("DescripcionAgrupacion")
        End Get
        Set(value As String)
            ViewState("DescripcionAgrupacion") = value
        End Set
    End Property

    Property DescripcionCortaParametro() As String
        Get
            Return ViewState("DescripcionCortaParametro")
        End Get
        Set(value As String)
            ViewState("DescripcionCortaParametro") = value
        End Set
    End Property

    Property DescripcionLargaParametro() As String
        Get
            Return ViewState("DescripcionLargaParametro")
        End Get
        Set(value As String)
            ViewState("DescripcionLargaParametro") = value
        End Set
    End Property

    Property NecOrden() As String
        Get
            Return ViewState("NecOrden")
        End Get
        Set(value As String)
            ViewState("NecOrden") = value
        End Set
    End Property

    Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property

    Public Property ParametroOpcionTemporario() As IAC.ContractoServicio.Parametro.GetParametroOpciones.OpcionColeccion
        Get
            If ViewState("ParametroOpcionTemporario") Is Nothing Then
                ViewState("ParametroOpcionTemporario") = New IAC.ContractoServicio.Parametro.GetParametroOpciones.OpcionColeccion
            End If

            Return ViewState("ParametroOpcionTemporario") 'DirectCast(ViewState("ParametroOpcionTemporario"), IAC.ContractoServicio.Parametro.GetParametroOpciones.OpcionColeccion)
        End Get
        Set(value As IAC.ContractoServicio.Parametro.GetParametroOpciones.OpcionColeccion)
            ViewState("ParametroOpcionTemporario") = value
        End Set
    End Property

    Public Property TipoControle As ContractoServicio.Parametro.TipoComponente
        Get
            Return ViewState("TipoControle")
        End Get
        Set(value As ContractoServicio.Parametro.TipoComponente)
            ViewState("TipoControle") = value
        End Set
    End Property

    Public Property Parametro As ContractoServicio.Parametro.GetParametroDetail.Parametro
        Get
            Return ViewState("Parametro")
        End Get
        Set(value As ContractoServicio.Parametro.GetParametroDetail.Parametro)
            ViewState("Parametro") = value
        End Set
    End Property

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona validação aos controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 06/09/2011 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

        MyBase.AddControleValidarPermissao(Aplicacao.Util.Utilidad.eAcoesTela._MODIFICAR.ToString, btnModificacionOpcion)
        MyBase.AddControleValidarPermissao(Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR.ToString, btnConsultarOpcion)
        MyBase.AddControleValidarPermissao(Aplicacao.Util.Utilidad.eAcoesTela._DAR_BAJA.ToString, btnBajaOpcion)
        MyBase.AddControleValidarPermissao(Aplicacao.Util.Utilidad.eAcoesTela._DAR_ALTA.ToString, btnAltaOpcion)

    End Sub

    ''' <summary>
    ''' Adiciona javascript aos controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 06/09/2011 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()
        Dim pbo As PostBackOptions
        Dim s As String = String.Empty

        'Deletar
        pbo = New PostBackOptions(btnBajaOpcion)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnBajaOpcion.FuncaoJavascript = "if(VerificarRegistroSelecionadoGridView('" & ProsegurGridView1.ClientID & "','" & _
                                           Traduzir(Aplicacao.Util.Utilidad.InfoMsgSeleccionarRegistro) & "','" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & _
                                           "','" & Traduzir("info_msg_registro_borrado") & " '))" & s & ";"


        'Modificar
        pbo = New PostBackOptions(btnModificacionOpcion)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnModificacionOpcion.FuncaoJavascript = "if(VerificarRegistroSelecionadoGridView('" & ProsegurGridView1.ClientID & "','" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgSeleccionarRegistro) & "',''))" & s & ";"

        'Consultar
        pbo = New PostBackOptions(btnConsultarOpcion)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnConsultarOpcion.FuncaoJavascript = "if(VerificarRegistroSelecionadoGridView('" & ProsegurGridView1.ClientID & "','" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgSeleccionarRegistro) & "',''))" & s & ";"


        btnCancelar.FuncaoJavascript = "if(VerificarConfirmacaoCanelamento('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgSairPagina) & "')) window.location = 'BusquedaParametros.aspx'; else return false; "

        pbo = New PostBackOptions(btnGrabar)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnGrabar.FuncaoJavascript = "if (VerificarConfirmacaoCanelamento('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgGrabarRegistro) & "')) " & s & " ; "

        txtOrden.Attributes.Add("onkeypress", "bloqueialetras(event,this);")
        txtOrden.Attributes.Add("onKeyDown", "BloquearColar();")

        txtDescripcionLarga.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtDescripcionLarga.ClientID & "','" & txtDescripcionLarga.MaxLength & "');")
        txtDescripcionLarga.Attributes.Add("onblur", "limitaCaracteres('" & txtDescripcionLarga.ClientID & "','" & txtDescripcionLarga.MaxLength & "');")
        txtDescripcionLarga.Attributes.Add("onkeyup", "limitaCaracteres('" & txtDescripcionLarga.ClientID & "','" & txtDescripcionLarga.MaxLength & "');")

    End Sub

    ''' <summary>
    ''' Configuração do tabindex.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 06/09/2011 - Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

        ddlAgrupacion.TabIndex = 1
        txtDescripcionCorto.TabIndex = 2
        txtDescripcionLarga.TabIndex = 3
        txtOrden.TabIndex = 4
        btnGrabar.TabIndex = 5
        btnCancelar.TabIndex = 6
        btnVolver.TabIndex = 7

        Master.PrimeiroControleTelaID = String.Format("{0}", ddlAgrupacion.ClientID)
        Me.DefinirRetornoFoco(Master.Sair, Master.RetornaMenu)

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 06/09/2011 - Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.PARAMETRO
    End Sub

    ''' <summary>
    ''' Metodo chamado quando inicializa a pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 06/09/2011 - Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            CodAplicacion = Request.QueryString("codaplicacion")
            CodParametro = Request.QueryString("codparametro")
            If Not Page.IsPostBack Then

                'Preenche Niveles 
                PreencherTela()

            End If

            TrataFoco()

            'Consome o ParametroOpcion passado pela PopUp de "ParametroOpcion"
            ConsomeParametroOpcion()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    ''' <summary>
    ''' Metodo chamado para preencher o objeto ParametroOpcion com a modificação feita na tela "ParametroOpcion"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gccosta] 02/09/2012 - Criado
    ''' </history>
    Public Sub ConsomeParametroOpcion()

        If Session("objParametroOpcion") IsNot Nothing Then

            Dim objParametroOpcion As IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion
            objParametroOpcion = DirectCast(Session("objParametroOpcion"), IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion)

            'Se existe o TerminoMedioPago na coleção
            If Not VerificarParametroOpcionExiste(ParametroOpcionTemporario, objParametroOpcion.CodigoOpcion) Then
                ParametroOpcionTemporario.Add(objParametroOpcion)
            Else
                ModificaParametroOpcion(ParametroOpcionTemporario, objParametroOpcion)
            End If


            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar Then
                'Seta o estado da página corrente para modificação
                Acao = eAcaoEspecifica.AltaConRegistro
            End If

            'Carrega os TerminosMedioPago no GridView
            Dim objDT As DataTable
            objDT = ProsegurGridView1.ConvertListToDataTable(ParametroOpcionTemporario)
            ProsegurGridView1.CarregaControle(objDT)

            Session("objParametroOpcion") = Nothing

        End If

    End Sub
    Private Enum eAcaoEspecifica As Integer
        AltaConRegistro = 20
    End Enum

    ''' <summary>
    ''' Modifica um Medio de Pago existe na coleção informada
    ''' </summary>
    ''' <param name="objParametroOpcionColeccion"></param>
    ''' <param name="objParametroOpcion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ModificaParametroOpcion(ByRef objParametroOpcionColeccion As IAC.ContractoServicio.Parametro.GetParametroOpciones.OpcionColeccion, objParametroOpcion As IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion) As Boolean

        Dim retorno = From c In objParametroOpcionColeccion Where c.CodigoOpcion = objParametroOpcion.CodigoOpcion

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else

            Dim objParametroOpcionTmp As IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion = DirectCast(retorno.ElementAt(0), IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion)

            'Campos Texto
            objParametroOpcionTmp.DescripcionOpcion = objParametroOpcion.DescripcionOpcion
            objParametroOpcionTmp.EsVigente = objParametroOpcion.EsVigente

            objParametroOpcionTmp.CodDelegacion = objParametroOpcion.CodDelegacion

            Return True
        End If

    End Function

    ''' <summary>
    ''' Pre render.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 06/09/2011 - Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Traduz os controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 06/09/2011 - Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Master.TituloPagina = Traduzir("026_titulo_mantenimento_parametros")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("026_subtitulo_mantenimento_parametros")
        lblAplicacion.Text = Traduzir("026_lbl_aplicacion_mant")
        lblNivel.Text = Traduzir("026_lbl_nivel_mant")
        lblCodParametro.Text = Traduzir("026_lbl_codparametro_mant")
        lblDescripcionLarga.Text = Traduzir("026_lbl_DescripcionLarga_mant")
        lblTipoComponent.Text = Traduzir("026_lbl_tipocomponent_mant")
        lblObligatorio.Text = Traduzir("026_lbl_obligatorio_mant")
        lblTipoDado.Text = Traduzir("026_lbl_tipodato_mant")
        lblAgrupacion.Text = Traduzir("026_lbl_agrupacion_mant")
        lblDescripcionCorto.Text = Traduzir("026_lbl_descripcioncorto_mant")
        lblOrden.Text = Traduzir("026_lbl_orden_mant")
        lblSubTituloOpciones.Text = Traduzir("026_titulo_lista_opciones")

        csvDdlAgrupacionObrigatorio.ErrorMessage = Traduzir("026_msg_agrupacionobligatorio")
        csvTxtDescripcionCortoObrigatorio.ErrorMessage = Traduzir("026_msg_descripcioncortoobligatorio")
        csvTxtDescripcionLargaObrigatorio.ErrorMessage = Traduzir("026_msg_DescripcionLargaobligatorio")
        csvTxtOrden.ErrorMessage = Traduzir("026_msg_Dordenobligatorio")

        'GridView
        ProsegurGridView1.PagerSummary = Traduzir("grid_lbl_pagersummary")

    End Sub

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Preeche os dados do combo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PreencherComboBoxAgrupaciones()
        Dim objRespuestaGetAgrupaciones As New IAC.ContractoServicio.Parametro.GetAgrupaciones.Respuesta

        objRespuestaGetAgrupaciones = getAgrupaciones()

        If Not Master.ControleErro.VerificaErro(objRespuestaGetAgrupaciones.CodigoError, objRespuestaGetAgrupaciones.NombreServidorBD, objRespuestaGetAgrupaciones.MensajeError) Then
            Exit Sub
        End If

        ddlAgrupacion.DataSource = Nothing

        ' Caso exista apenas um registro a mesmo será aplicado como default
        If objRespuestaGetAgrupaciones.Agrupaciones.Count > 1 Then
            ddlAgrupacion.AppendDataBoundItems = True
            ddlAgrupacion.Items.Clear()
            ddlAgrupacion.Items.Add(New ListItem(Traduzir("010_ddl_selecione"), String.Empty))
        End If
        ddlAgrupacion.DataTextField = "DescripcionCorta"
        ddlAgrupacion.DataValueField = "DescripcionCorta"
        ddlAgrupacion.DataSource = objRespuestaGetAgrupaciones.Agrupaciones
        ddlAgrupacion.DataBind()

    End Sub

    ''' <summary>
    ''' Preeche os dados da tela
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PreencherTela()

        Dim objRespuestaGetParametroDetail As IAC.ContractoServicio.Parametro.GetParametroDetail.Respuesta
        Dim objRespuestaGetOpcion As IAC.ContractoServicio.Parametro.GetParametroOpciones.Respuesta

        objRespuestaGetParametroDetail = getParametroDetail()
        Parametro = objRespuestaGetParametroDetail.Parametro

        If Not Master.ControleErro.VerificaErro(objRespuestaGetParametroDetail.CodigoError, objRespuestaGetParametroDetail.NombreServidorBD, objRespuestaGetParametroDetail.MensajeError) Then
            Exit Sub
        End If

        'Armazena o tipo de componente.
        TipoControle = objRespuestaGetParametroDetail.Parametro.NecTipoComponente

        If (objRespuestaGetParametroDetail.Parametro.NecTipoComponente = ContractoServicio.Parametro.TipoComponente.Combobox) Then

            objRespuestaGetOpcion = getParametroOpcion()

            If Not Master.ControleErro.VerificaErro(objRespuestaGetOpcion.CodigoError, objRespuestaGetOpcion.NombreServidorBD, objRespuestaGetOpcion.MensajeError) Then
                Exit Sub
            End If

            PreencherGridParametroOpciones(objRespuestaGetOpcion)

        End If


        CodigoNivel = objRespuestaGetParametroDetail.Parametro.Nivel.CodigoNivel
        'preenche combo agrupaciones
        PreencherComboBoxAgrupaciones()

        ddlAgrupacion.SelectedIndex = ddlAgrupacion.Items.IndexOf(ddlAgrupacion.Items.FindByValue(objRespuestaGetParametroDetail.Parametro.Agrupacion.DescripcionCorta))
        ddlAgrupacion.ToolTip = ddlAgrupacion.SelectedItem.Text

        txtAplicacion.Text = objRespuestaGetParametroDetail.Parametro.Aplicacion.CodigoAplicacion
        txtAplicacion.ToolTip = objRespuestaGetParametroDetail.Parametro.Aplicacion.CodigoAplicacion

        txtNivel.Text = objRespuestaGetParametroDetail.Parametro.Nivel.DescripcionNivel
        txtNivel.ToolTip = objRespuestaGetParametroDetail.Parametro.Nivel.DescripcionNivel

        txtCodParametro.Text = objRespuestaGetParametroDetail.Parametro.CodParametro
        txtCodParametro.ToolTip = objRespuestaGetParametroDetail.Parametro.CodParametro

        txtDescripcionCorto.Text = objRespuestaGetParametroDetail.Parametro.DesCortaParametro
        txtDescripcionLarga.Text = objRespuestaGetParametroDetail.Parametro.DesLargaParametro

        txtTipoComponent.Text = objRespuestaGetParametroDetail.Parametro.NecTipoComponente.ToString
        txtTipoComponent.ToolTip = objRespuestaGetParametroDetail.Parametro.NecTipoComponente.ToString

        lblValueTipoDado.Text = objRespuestaGetParametroDetail.Parametro.NecTipoDato.ToString
        lblValueObligatorio.Text = If(objRespuestaGetParametroDetail.Parametro.BolObligatorio, Traduzir(Aplicacao.Util.Utilidad.GenOpcionSi), Traduzir(Aplicacao.Util.Utilidad.GenOpcionNo))
        txtOrden.Text = objRespuestaGetParametroDetail.Parametro.NecOrden

    End Sub

    ''' <summary>
    ''' Responsavel por ir no DB e buscar as informações para preencher as ParamentroOpcion.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gccosta] 28/02/2012 - Criado
    ''' </history>
    Public Function getParametroOpcion() As IAC.ContractoServicio.Parametro.GetParametroOpciones.Respuesta
        Dim objProxyParametro As New Comunicacion.ProxyParametro
        Dim objRespuestaGetOpciones As New IAC.ContractoServicio.Parametro.GetParametroOpciones.Respuesta
        Dim objPeticionGetAgrupaciones As New IAC.ContractoServicio.Parametro.GetParametroOpciones.Peticion

        objPeticionGetAgrupaciones.CodigoAplicacion = CodAplicacion
        objPeticionGetAgrupaciones.CodigoParametro = CodParametro

        objRespuestaGetOpciones = objProxyParametro.GetParametroOpciones(objPeticionGetAgrupaciones)
        For index = 0 To objRespuestaGetOpciones.Opciones.Count - 1
            objRespuestaGetOpciones.Opciones(index).Parametro = Parametro
        Next

        Return objRespuestaGetOpciones
    End Function

    ''' <summary>
    ''' Responsavel por ir no DB e buscar as informações para preencher o ddlAgrupacion.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 08/09/2011 - Criado
    ''' </history>
    Public Function getAgrupaciones() As IAC.ContractoServicio.Parametro.GetAgrupaciones.Respuesta
        Dim objProxyParametro As New Comunicacion.ProxyParametro
        Dim objRespuestaGetAgrupaciones As New IAC.ContractoServicio.Parametro.GetAgrupaciones.Respuesta
        Dim objPeticionGetAgrupaciones As New IAC.ContractoServicio.Parametro.GetAgrupaciones.Peticion

        objPeticionGetAgrupaciones.CodigoAplicacion = CodAplicacion
        objPeticionGetAgrupaciones.CodigoNivel = CodigoNivel

        objRespuestaGetAgrupaciones = objProxyParametro.GetAgrupaciones(objPeticionGetAgrupaciones)

        Return objRespuestaGetAgrupaciones
    End Function

    ''' <summary>
    ''' Responsavel por ir no DB e buscar as informações para preencher o dd.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 08/09/2011 - Criado
    ''' </history>
    Public Function getParametroDetail() As IAC.ContractoServicio.Parametro.GetParametroDetail.Respuesta

        Dim objProxyParametro As New Comunicacion.ProxyParametro
        Dim objRespuestaGetParametroDetail As New IAC.ContractoServicio.Parametro.GetParametroDetail.Respuesta
        Dim objPeticionGetParametroDetail As New IAC.ContractoServicio.Parametro.GetParametroDetail.Peticion

        objPeticionGetParametroDetail.CodigoAplicacion = CodAplicacion
        objPeticionGetParametroDetail.CodigoParametro = CodParametro

        objRespuestaGetParametroDetail = objProxyParametro.GetParametroDetail(objPeticionGetParametroDetail)

        Return objRespuestaGetParametroDetail
    End Function

    ''' <summary>
    ''' Responsavel por ir no DB e setar as informações da tela.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 13/09/2011 - Criado
    ''' </history>
    Public Function setParametro() As IAC.ContractoServicio.Parametro.SetParametro.Respuesta

        Dim objProxyParametro As New Comunicacion.ProxyParametro
        Dim objRespuestaSetParametro As New IAC.ContractoServicio.Parametro.SetParametro.Respuesta
        Dim objPeticionSetParametro As New IAC.ContractoServicio.Parametro.SetParametro.Peticion

        objPeticionSetParametro.CodigoAplicacion = CodAplicacion
        objPeticionSetParametro.CodigoParametro = CodParametro
        objPeticionSetParametro.CodigoNivel = CodigoNivel
        objPeticionSetParametro.DescripcionAgrupacion = DescripcionAgrupacion
        objPeticionSetParametro.DescripcionCortaParametro = DescripcionCortaParametro
        objPeticionSetParametro.DescripcionLargaParametro = DescripcionLargaParametro
        objPeticionSetParametro.NecOrden = If(String.IsNullOrEmpty(NecOrden), "0", NecOrden)
        objPeticionSetParametro.CodigoUsuario = MyBase.LoginUsuario

        ' ParametroOpcionTemporario
        objPeticionSetParametro.ParametroOpciones = ParametroOpcionTemporario
        objRespuestaSetParametro = objProxyParametro.SetParametro(objPeticionSetParametro)

        Return objRespuestaSetParametro

    End Function

    ''' <summary>
    ''' Responsavel gravar dados da tela.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 13/09/2011 - Criado
    ''' </history>
    Public Sub ExecutarGravar()
        ' Executa controle de validação dos campos
        ValidarCamposObrigatorios = True
        Dim erros As String = MontaMensagensErro(True)
        If Not String.IsNullOrEmpty(erros) Then
            Master.ControleErro.ShowError(erros, False)
            Exit Sub
        End If

        DescripcionAgrupacion = ddlAgrupacion.SelectedValue
        DescripcionCortaParametro = txtDescripcionCorto.Text
        DescripcionLargaParametro = txtDescripcionLarga.Text

        If String.IsNullOrEmpty(txtOrden.Text) OrElse IsNumeric(txtOrden.Text) Then
            NecOrden = txtOrden.Text
        Else
            ' exibir mensagem ao usuário
            Master.ControleErro.ShowError(Traduzir("err_passagem_parametro") & " - " & lblOrden.Text, False)
            txtOrden.Focus()
            Exit Sub
        End If

        Dim objRespuestaSetParametro As IAC.ContractoServicio.Parametro.SetParametro.Respuesta

        objRespuestaSetParametro = setParametro()

        If Not Master.ControleErro.VerificaErro(objRespuestaSetParametro.CodigoError, objRespuestaSetParametro.NombreServidorBD, objRespuestaSetParametro.MensajeError) Then
            Exit Sub
        End If

        Response.Redirect("BusquedaParametros.aspx", False)

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

                'Laço que verifica todos os validators
                For Each validator As BaseValidator In Me.Validators
                    'Checa validator se é ou não válido
                    validator.Validate()
                    If Not validator.IsValid Then
                        'Caso o campo esteja inválido é adicionado uma mensagem de erro a lista
                        strErro.Append(validator.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                        If Not focoSetado Then
                            'Seta o foco no primeiro campo com erro
                            validator.NamingContainer.FindControl(validator.ControlToValidate).Focus()
                            focoSetado = True
                        End If
                    End If
                Next

            End If
        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' Função do Botão Alta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gccosta] 23/02/2012 - Criado
    ''' </history>
    Public Sub ExecutarAlta()
        Try

            Dim url As String = "MantenimientoOpcionesParametro.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&codparametro=" & CodParametro & "&codaplicacion=" & CodAplicacion

            'Seta a session com a coleção de Opciones que será consumida na abertura da PopUp de Mantenimiento de Opciones
            SetParamentroOpcionColecaoPopUp()

            ' limpar sessao das telas seguintes
            Session("objColOpcionParametro") = Nothing

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_OpcionParametros", "AbrirPopupModal('" & url & "', 465, 788,'ParametroAlta');", True)


        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Seta a coleção de Opciones de Parametro para a PopUp que será aberta
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamentroOpcionColecaoPopUp()

        'Passa a coleção com o objeto de opciones de parametro
        Session("colOpcionParametro") = ParametroOpcionTemporario

    End Sub

    ''' <summary>
    ''' Preenche o grid com a lista de ParametroOpcion
    ''' </summary>
    ''' <param name="objRespuestaGetOpcion"></param>
    ''' <remarks></remarks>
    Private Sub PreencherGridParametroOpciones(objRespuestaGetOpcion As ContractoServicio.Parametro.GetParametroOpciones.Respuesta)
        If objRespuestaGetOpcion.Opciones.Count > 0 Then
            'Carrega os TerminosMedioPago no GridView            
            Dim objDT As DataTable
            objDT = ProsegurGridView1.ConvertListToDataTable(objRespuestaGetOpcion.Opciones)
            ProsegurGridView1.CarregaControle(objDT)
            ParametroOpcionTemporario = objRespuestaGetOpcion.Opciones
        End If
    End Sub

    ''' <summary>
    ''' Função do Botão Baja
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gccosta] 29/02/2012 - Criado
    ''' </history>
    Public Sub ExecutarBaja()
        Try

            'Retorna o valor da linha selecionada no grid
            Dim strCodigo As String = ProsegurGridView1.getValorLinhaSelecionada

            'Modifica o Termino Medio de Pago para exclusão
            Dim objParametroOpcionRetorno As IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion = RetornaobjParamentroOpcionExiste(ParametroOpcionTemporario, strCodigo)
            objParametroOpcionRetorno.EsVigente = False

            'Carrega os Terminos no GridView
            Dim objDT As DataTable
            objDT = ProsegurGridView1.ConvertListToDataTable(ParametroOpcionTemporario)
            ProsegurGridView1.CarregaControle(objDT)


        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Retorna um ParametroOpcion da coleção informada    
    ''' </summary>
    ''' <param name="objParametroOpcion"></param>
    ''' <param name="codigoParametroOpcion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetornaobjParamentroOpcionExiste(ByRef objParametroOpcion As IAC.ContractoServicio.Parametro.GetParametroOpciones.OpcionColeccion, codigoParametroOpcion As String) As IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion

        Dim retorno = From c In objParametroOpcion Where c.CodigoOpcion = codigoParametroOpcion

        If retorno Is Nothing OrElse retorno.Count = 0.0R Then
            Return Nothing
        Else
            Return retorno.ElementAt(0)
        End If

    End Function

    ''' <summary>
    ''' Função do Botão Modificacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gccosta] 29/02/2012 - Criado
    ''' </history>
    Public Sub ExecutarModificacion()
        Try

            Dim url As String = "MantenimientoOpcionesParametro.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Modificacion & "&codparametro=" & CodParametro

            'Seta a session com o ParametroOpcion que será consmida na abertura da PopUp de Mantenimiento de ParametroOpcion
            SetParamentroOpcionSelecionadoPopUp()

            'Seta a session com a coleção de ParametroOpcion que será consmida na abertura da PopUp de Mantenimiento de ParametroOpcion
            SetParamentroOpcionColecaoPopUp()

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_ParametroOpcion", "AbrirPopupModal('" & url & "', 465, 788,'ParametroModificacion');", True)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub


    ''' <summary>
    ''' Seta o termino para a PopUp que será aberta
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamentroOpcionSelecionadoPopUp()

        'Cria o ParamentroOpcion para ser consumido na página de ParamentroOpcion
        Dim objParametroOpcion As New IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion
        objParametroOpcion = RetornaobjParamentroOpcionExiste(ParametroOpcionTemporario, ProsegurGridView1.getValorLinhaSelecionada)

        'Envia o TerminoMedioPago para ser consumido pela PopUp de Mantenimento de TerminoMedioPago
        Session("setParametroOpcion") = objParametroOpcion

    End Sub

    ''' <summary>
    ''' Função do Botão Consulta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gccosta] 29/02/2012 - Criado
    ''' </history>
    Public Sub ExecutarConsulta()
        Try
            Dim url As String = "MantenimientoOpcionesParametro.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta

            'Seta a session com o ParamentroOpcion que será consmida na abertura da PopUp de Mantenimiento de ParamentroOpcion
            SetParamentroOpcionSelecionadoPopUp()

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_ParamentroOpcion", "AbrirPopupModal('" & url & "', 465, 788,'ParametroConsulta');", True)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Verifica se um Parametro Opcion especifico existe na coleção informada
    ''' </summary>
    ''' <param name="objParametroOpcion"></param>
    ''' <param name="codigoParametroOpcion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>    
    Private Function VerificarParametroOpcionExiste(objParametroOpcion As IAC.ContractoServicio.Parametro.GetParametroOpciones.OpcionColeccion, codigoParametroOpcion As String) As Boolean

        Dim retorno = From c In objParametroOpcion Where c.CodigoOpcion = codigoParametroOpcion

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()

        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta

                If TipoControle = ContractoServicio.Parametro.TipoComponente.Combobox Then
                    pnlOpciones.Visible = True
                End If

                btnAltaOpcion.Visible = True            '1
                btnGrabar.Visible = True                '2
                btnCancelar.Visible = True              '3

                btnBajaOpcion.Visible = False           '4
                btnModificacionOpcion.Visible = False   '5
                btnConsultarOpcion.Visible = False      '6 
                btnVolver.Visible = False               '7

                btnGrabar.Habilitado = True
                btnAltaOpcion.Habilitado = True

            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta

                If TipoControle = ContractoServicio.Parametro.TipoComponente.Combobox Then
                    pnlOpciones.Visible = True
                End If

                btnAltaOpcion.Visible = False                 '1
                btnCancelar.Visible = False                   '2

                btnVolver.Visible = True                      '3

                btnBajaOpcion.Visible = False                 '4
                btnModificacionOpcion.Visible = False         '5
                btnGrabar.Visible = False                     '6

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion

                If TipoControle = ContractoServicio.Parametro.TipoComponente.Combobox Then
                    pnlOpciones.Visible = True
                End If

                btnAltaOpcion.Visible = True           '1
                btnGrabar.Visible = True               '2
                btnCancelar.Visible = True             '3
                btnVolver.Visible = False              '4

                btnGrabar.Habilitado = True
                btnAltaOpcion.Habilitado = True
                btnModificacionOpcion.Habilitado = True
                btnConsultarOpcion.Habilitado = True
                btnBajaOpcion.Habilitado = True

            Case Aplicacao.Util.Utilidad.eAcao.NoAction
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Busca
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.NoAction

            Case Aplicacao.Util.Utilidad.eAcao.Erro
                btnAltaOpcion.Visible = False          '1
                btnGrabar.Visible = False        '2
                btnCancelar.Visible = False      '3
                btnBajaOpcion.Visible = False          '4
                btnModificacionOpcion.Visible = False  '5
                btnConsultarOpcion.Visible = False      '6 
                btnVolver.Visible = True         '7

        End Select

        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem de erro
        If MontaMensagensErro() <> String.Empty Then
            Master.ControleErro.ShowError(MontaMensagensErro, False)
        End If

    End Sub

#End Region

#End Region

#Region "[EVENTOS]"

    Protected Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        Try
            ExecutarGravar()
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Clique botão Baja
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnBajaOpcion_Click(sender As Object, e As EventArgs) Handles btnBajaOpcion.Click
        Try
            ExecutarBaja()
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Clique botão Modificacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnModificacionOpcion_Click(sender As Object, e As EventArgs) Handles btnModificacionOpcion.Click
        Try
            ExecutarModificacion()
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Clique botão Consulta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnConsultarOpcion_Click(sender As Object, e As EventArgs) Handles btnConsultarOpcion.Click
        Try
            ExecutarConsulta()
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Clique botão Alta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAltaOpcion_Click(sender As Object, e As EventArgs) Handles btnAltaOpcion.Click
        Try
            ExecutarAlta()
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento disparado quando a linha do Gridview esta sendo populada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ProsegurGridView1_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.EOnClickRowClientScript
        Try

            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            ProsegurGridView1.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("esvigente").ToString.ToLower & ");"

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

    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPreencheDados

        Try
            Dim objDT As DataTable

            objDT = ProsegurGridView1.ConvertListToDataTable(ParametroOpcionTemporario)

            objDT.DefaultView.Sort = ProsegurGridView1.SortCommand

            ProsegurGridView1.ControleDataSource = objDT

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try


    End Sub

    Protected Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("014_lbl_grd_mantenimiento_codigo")
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("014_lbl_grd_mantenimiento_descripcion")
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("014_lbl_grd_mantenimiento_vigente")
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    Protected Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowDataBound

        Try
            'Índice das celulas do GridView Configuradas
            '0 - Código
            '1 - Descripción
            '2 - Vigente

            If e.Row.DataItem IsNot Nothing Then

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                If Not e.Row.DataItem("descripcionopcion") Is DBNull.Value AndAlso e.Row.DataItem("descripcionopcion").Length > NumeroMaximoLinha Then
                    e.Row.Cells(1).Text = e.Row.DataItem("descripcionopcion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                If CBool(e.Row.DataItem("esvigente")) Then
                    CType(e.Row.Cells(2).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
                Else
                    CType(e.Row.Cells(2).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
                End If
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Private Sub ddlAgrupacion_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAgrupacion.SelectedIndexChanged
        ddlAgrupacion.ToolTip = ddlAgrupacion.SelectedItem.Text
    End Sub

#End Region

End Class