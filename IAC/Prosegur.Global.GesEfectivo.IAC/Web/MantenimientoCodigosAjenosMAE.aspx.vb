Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Comon.Paginacion

Public Class MantenimientoCodigosAjenosMAE
    Inherits Base

    <Serializable()> _
    Public Class CodigoAjenoSimples

        Public Property CodDelegacion As String
        Public Property OidCliente As String

        Public Property CodCliente As String

        Public Property DesCliente As String

        Public Property CodigoAjenoCliente As ContractoServicio.CodigoAjeno.CodigoAjenoBase

        Public Property OidSubcliente As String

        Public Property CodSubcliente As String

        Public Property DesSubcliente As String

        Public Property CodigoAjenoSubcliente As ContractoServicio.CodigoAjeno.CodigoAjenoBase

        Public Property OidPuntoServicio As String

        Public Property CodPuntoServicio As String

        Public Property DesPuntoServicio As String

        Public Property CodigoAjenoPuntoServicio As ContractoServicio.CodigoAjeno.CodigoAjenoBase

        Public Property OidSectorMAE As String

        Public Property CodigoSectorMAE As String

        Public Property Completo As Boolean

    End Class

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

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()
        'DefinirRetornoFoco(btnGrabar, txtIdentificador)
    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        'Define como ação inical
        Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        'MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CODIGO_AJENO
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False
        'Pegando a ação
        MyBase.Acao = Request.QueryString("acao")
        MyBase.CodFuncionalidad = "ABM_MAE"
    End Sub

    Protected Overrides Sub Inicializar()

        Try
            If Not Page.IsPostBack Then

                'Consome os ojetos passados
                ConsomeObjeto()

                If (ViewStateCodigoAjenoEntrada Is Nothing) Then

                    'Informa ao usuário que o parâmetro passado 
                    Throw New Exception(Traduzir("err_passagem_parametro"))

                End If

                ' Preenche grid de codigos ajenos
                BuscarCodigosAjenos()

                If (ViewStateCodigoAjenoEntrada IsNot Nothing) Then
                    ' Preenche os campos do cabeçalho
                    txtCodCliente.Text = ViewStateCodigoAjenoEntrada.CodCliente
                    If ViewStateCodigoAjenoEntrada.CodigoAjenoCliente IsNot Nothing Then
                        txtCodAjenoCliente.Text = ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.CodAjeno
                        txtDesAjenoCliente.Text = ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.DesAjeno
                    End If
                    txtCodSubcliente.Text = ViewStateCodigoAjenoEntrada.CodSubcliente
                    If ViewStateCodigoAjenoEntrada.CodigoAjenoSubcliente IsNot Nothing Then
                        txtCodAjenoSubcliente.Text = ViewStateCodigoAjenoEntrada.CodigoAjenoSubcliente.CodAjeno
                        txtDesAjenoSubcliente.Text = ViewStateCodigoAjenoEntrada.CodigoAjenoSubcliente.DesAjeno
                    End If
                    txtCodPtoServicio.Text = ViewStateCodigoAjenoEntrada.CodPuntoServicio
                    If ViewStateCodigoAjenoEntrada.CodigoAjenoPuntoServicio IsNot Nothing Then
                        txtCodAjenoPtoServicio.Text = ViewStateCodigoAjenoEntrada.CodigoAjenoPuntoServicio.CodAjeno
                        txtDesAjenoPtoServicio.Text = ViewStateCodigoAjenoEntrada.CodigoAjenoPuntoServicio.DesAjeno
                    End If
                End If

                txtSectorMAE.Text = CalculaCodigoSectorMAE()

            End If

            ' trata o foco dos campos
            TrataFoco()

            ' chama a função que seta o tamanho das linhas do grid.
            If String.IsNullOrEmpty(txtDesAjenoPtoServicio.Text) Then
                txtDesAjenoPtoServicio.Focus()
            End If
            If String.IsNullOrEmpty(txtCodAjenoPtoServicio.Text) Then
                txtCodAjenoPtoServicio.Focus()
            End If
            If String.IsNullOrEmpty(txtDesAjenoSubcliente.Text) Then
                txtDesAjenoSubcliente.Focus()
            End If
            If String.IsNullOrEmpty(txtCodAjenoSubcliente.Text) Then
                txtCodAjenoSubcliente.Focus()
            End If
            If String.IsNullOrEmpty(txtDesAjenoCliente.Text) Then
                txtDesAjenoCliente.Focus()
            End If
            If String.IsNullOrEmpty(txtCodAjenoCliente.Text) Then
                txtCodAjenoCliente.Focus()
            End If
        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Protected Overrides Sub PreRenderizar()

        Try
            ControleBotoes()
            'UpdatePanelIdentificador.Attributes.Add("style", "margin:0px !important;")
            'UpdatePanel2.Attributes.Add("style", "margin:0px !important;")
            'txtDescripcionAjena.Attributes.Add("style", "margin-left:2px;")
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = MyBase.RecuperarValorDic("mod_cod_ajeno_titulo")
        lblSubTitulo.Text = MyBase.RecuperarValorDic("mod_cod_ajeno_titulo")
        lblCliente.Text = MyBase.RecuperarValorDic("lbl_cliente")
        lblSucliente.Text = MyBase.RecuperarValorDic("lbl_subcliente")
        lblPtoServicio.Text = MyBase.RecuperarValorDic("lbl_pto_servicio")
        lblSectorMAE.Text = MyBase.RecuperarValorDic("lbl_sector_mae")
        lblCodGenesis.Text = MyBase.RecuperarValorDic("lbl_cod_genesis")
        lblCodAjeno.Text = MyBase.RecuperarValorDic("lbl_cod_ajeno")
        lblDesAjeno.Text = MyBase.RecuperarValorDic("lbl_des_ajeno")
        btnGrabar.Text = Traduzir("btnGrabar")
        btnGrabar.ToolTip = Traduzir("btnGrabar")
        btnCancelar.Text = Traduzir("btnCancelar")
        btnCancelar.ToolTip = Traduzir("btnCancelar")

    End Sub

#End Region

#Region "[PROPRIEDADES]"
    Public Property ViewStateCodigoAjenoEntrada() As CodigoAjenoSimples
        Get
            Return Session("CodigoAjenoEntrada")
        End Get
        Set(value As CodigoAjenoSimples)
            Session("CodigoAjenoEntrada") = value
        End Set
    End Property

    Public Property ViewStateCodigoAjenoOriginal() As CodigoAjenoSimples
        Get
            Return Session("CodigoAjenoEntradaOriginal")
        End Get
        Set(value As CodigoAjenoSimples)
            Session("CodigoAjenoEntradaOriginal") = value
        End Set
    End Property
#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Evento clique do botão aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 29/04/2013 Criado
    ''' </history>
    Private Sub btnGrabar_Click(sender As Object, e As System.EventArgs) Handles btnGrabar.Click

        Try

            If Validado() Then
                GrabarDatos()

                ' aqui deve gravar na sessão
                Session("objRespMantenimientoCodigosAjenosMAE") = Me.ViewStateCodigoAjenoEntrada

                Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
                ' fechar janela
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "MantenimientoCodigosAjenosMAE", jsScript, True)

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Function Validado() As Boolean

        ' criar objeto peticion
        Dim objPeticion As New ContractoServicio.CodigoAjeno.VerificarIdentificadorXCodigoAjeno.Peticion
        Dim objProxy As New Comunicacion.ProxyCodigoAjeno

        'Preenche peticion
        If Me.ViewStateCodigoAjenoOriginal.CodigoAjenoCliente.CodAjeno <> txtCodAjenoCliente.Text Then

            Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.CodAjeno = If(String.IsNullOrEmpty(txtCodAjenoCliente.Text), Nothing, txtCodAjenoCliente.Text)
            Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.DesAjeno = If(String.IsNullOrEmpty(txtDesAjenoCliente.Text), Nothing, txtDesAjenoCliente.Text)

            objPeticion.CodTipoTablaGenesis = "GEPR_TCLIENTE"
            objPeticion.CodIdentificador = Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.CodIdentificador
            objPeticion.CodAjeno = Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.CodAjeno
            objPeticion.OidCodigoAjeno = Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.OidCodigoAjeno

            ' chamar servicio
            Dim objRespuesta As ContractoServicio.CodigoAjeno.VerificarIdentificadorXCodigoAjeno.Respuesta = objProxy.VerificarIdentificadorXCodigoAjeno(objPeticion)

            ' tratar retorno
            If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, "", objRespuesta.MensajeError) Then
                MyBase.MostraMensagem(objRespuesta.MensajeError)
                Exit Function
            End If

        End If

        If Me.ViewStateCodigoAjenoEntrada.CodigoAjenoSubcliente.CodAjeno <> txtCodAjenoSubcliente.Text Then

            Me.ViewStateCodigoAjenoEntrada.CodigoAjenoSubcliente.CodAjeno = If(String.IsNullOrEmpty(txtCodAjenoSubcliente.Text), Nothing, txtCodAjenoSubcliente.Text)
            Me.ViewStateCodigoAjenoEntrada.CodigoAjenoSubcliente.DesAjeno = If(String.IsNullOrEmpty(txtDesAjenoSubcliente.Text), Nothing, txtDesAjenoSubcliente.Text)

            objPeticion.CodTipoTablaGenesis = "GEPR_TSUBCLIENTE"
            objPeticion.CodIdentificador = Me.ViewStateCodigoAjenoEntrada.CodigoAjenoSubcliente.CodIdentificador
            objPeticion.CodAjeno = Me.ViewStateCodigoAjenoEntrada.CodigoAjenoSubcliente.CodAjeno
            objPeticion.OidCodigoAjeno = Me.ViewStateCodigoAjenoEntrada.CodigoAjenoSubcliente.OidCodigoAjeno

            ' chamar servicio
            Dim objRespuesta As ContractoServicio.CodigoAjeno.VerificarIdentificadorXCodigoAjeno.Respuesta = objProxy.VerificarIdentificadorXCodigoAjeno(objPeticion)

            ' tratar retorno
            If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, "", objRespuesta.MensajeError) Then
                MyBase.MostraMensagem(objRespuesta.MensajeError)
                Exit Function
            End If

        End If

        If Me.ViewStateCodigoAjenoEntrada.CodigoAjenoPuntoServicio.CodAjeno <> txtCodAjenoPtoServicio.Text Then

            Me.ViewStateCodigoAjenoEntrada.CodigoAjenoPuntoServicio.CodAjeno = If(String.IsNullOrEmpty(txtCodAjenoPtoServicio.Text), Nothing, txtCodAjenoPtoServicio.Text)
            Me.ViewStateCodigoAjenoEntrada.CodigoAjenoPuntoServicio.DesAjeno = If(String.IsNullOrEmpty(txtDesAjenoPtoServicio.Text), Nothing, txtDesAjenoPtoServicio.Text)

            objPeticion.CodTipoTablaGenesis = "GEPR_TPUNTO_SERVICIO"
            objPeticion.CodIdentificador = Me.ViewStateCodigoAjenoEntrada.CodigoAjenoPuntoServicio.CodIdentificador
            objPeticion.CodAjeno = Me.ViewStateCodigoAjenoEntrada.CodigoAjenoPuntoServicio.CodAjeno
            objPeticion.OidCodigoAjeno = Me.ViewStateCodigoAjenoEntrada.CodigoAjenoPuntoServicio.OidCodigoAjeno

            ' chamar servicio
            Dim objRespuesta As ContractoServicio.CodigoAjeno.VerificarIdentificadorXCodigoAjeno.Respuesta = objProxy.VerificarIdentificadorXCodigoAjeno(objPeticion)

            ' tratar retorno
            If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, "", objRespuesta.MensajeError) Then
                MyBase.MostraMensagem(objRespuesta.MensajeError)
                Exit Function
            End If

        End If

        Return True

    End Function

    Private Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click

        Try

            GrabarDatos()

            Dim jsScript As String = "window.parent.FecharModal(" & Chr(34) & "#" & divModal & Chr(34) & "," & Chr(34) & "#" & ifrModal & Chr(34) & ",null);"

            If ViewStateCodigoAjenoOriginal IsNot Nothing AndAlso (Aplicacao.Util.Utilidad.HayModificaciones(ViewStateCodigoAjenoEntrada.CodigoAjenoCliente, ViewStateCodigoAjenoOriginal.CodigoAjenoCliente) _
                    OrElse Aplicacao.Util.Utilidad.HayModificaciones(ViewStateCodigoAjenoEntrada.CodigoAjenoSubcliente, ViewStateCodigoAjenoOriginal.CodigoAjenoSubcliente) _
                        OrElse Aplicacao.Util.Utilidad.HayModificaciones(ViewStateCodigoAjenoEntrada.CodigoAjenoPuntoServicio, ViewStateCodigoAjenoOriginal.CodigoAjenoPuntoServicio)) Then
                MyBase.ExibirMensagemSimNao(MyBase.RecuperarValorDic("MSG_INFO_CERRAR_PANTALLA_CODIGOS_AJENOS"), jsScript)
            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "MantenimientoCodigoAjeno", jsScript, True)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

    Protected Sub txtCodAjenoPtoServicio_TextChanged(sender As Object, e As EventArgs) Handles txtCodAjenoPtoServicio.TextChanged
        txtSectorMAE.Text = CalculaCodigoSectorMAE()
    End Sub

    Protected Sub txtCodAjenoSubcliente_TextChanged(sender As Object, e As EventArgs) Handles txtCodAjenoSubcliente.TextChanged
        txtSectorMAE.Text = CalculaCodigoSectorMAE()
    End Sub

    Protected Sub txtCodAjenoCliente_TextChanged(sender As Object, e As EventArgs) Handles txtCodAjenoCliente.TextChanged
        txtSectorMAE.Text = CalculaCodigoSectorMAE()
    End Sub

#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' Controla o estado dos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 26/04/2013 Criado
    ''' </history>
    Public Sub ControleBotoes()

        MyBase.Acao = Request.QueryString("acao")

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta

            Case Aplicacao.Util.Utilidad.eAcao.Baja
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                setConsultar()

            Case Aplicacao.Util.Utilidad.eAcao.Busca

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion


            Case Aplicacao.Util.Utilidad.eAcao.NoAction

            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                txtCodAjenoPtoServicio.Focus()

        End Select

    End Sub

    Private Sub setConsultar()

        btnGrabar.Enabled = False
        txtDesAjenoCliente.Enabled = False
        txtDesAjenoSubcliente.Enabled = False
        txtDesAjenoPtoServicio.Enabled = False
        txtCodAjenoCliente.Enabled = False
        txtCodAjenoPtoServicio.Enabled = False
        txtCodAjenoSubcliente.Enabled = False
        btnGrabar.Visible = False

    End Sub

#End Region

#Region "[MÉTODOS]"
    Private Sub BuscarCodigosAjenos()

        Try
            'If String.IsNullOrEmpty(ViewStateCodigoAjenoEntrada.OidTablaGenesis) Then
            '    Acao = Aplicacao.Util.Utilidad.eAcao.Alta
            'Else
            '    ' Acao = Aplicacao.Util.Utilidad.eAcao.Busca
            'End If
            If ViewStateCodigoAjenoEntrada.CodigoAjenoCliente Is Nothing OrElse String.IsNullOrEmpty(ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.OidCodigoAjeno) Then
                Dim respCliente = GetCodigosAjenos(ViewStateCodigoAjenoEntrada.OidCliente, "GEPR_TCLIENTE")
                If respCliente.EntidadCodigosAjenos IsNot Nothing _
                        AndAlso respCliente.EntidadCodigosAjenos.Count > 0 _
                            AndAlso respCliente.EntidadCodigosAjenos.First.CodigosAjenos IsNot Nothing _
                                AndAlso respCliente.EntidadCodigosAjenos.First.CodigosAjenos.Count > 0 Then
                    ViewStateCodigoAjenoEntrada.CodigoAjenoCliente = New ContractoServicio.CodigoAjeno.CodigoAjenoBase With {
                                                   .CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor(),
                                                   .CodAjeno = respCliente.EntidadCodigosAjenos.First.CodigosAjenos.First.CodAjeno,
                                                   .DesAjeno = respCliente.EntidadCodigosAjenos.First.CodigosAjenos.First.DesAjeno,
                                                   .OidCodigoAjeno = respCliente.EntidadCodigosAjenos.First.CodigosAjenos.First.OidCodigoAjeno}
                End If
            End If

            If ViewStateCodigoAjenoEntrada.CodigoAjenoSubcliente Is Nothing OrElse String.IsNullOrEmpty(ViewStateCodigoAjenoEntrada.CodigoAjenoSubcliente.OidCodigoAjeno) Then
                Dim respSubcliente = GetCodigosAjenos(ViewStateCodigoAjenoEntrada.OidSubcliente, "GEPR_TSUBCLIENTE")
                If respSubcliente.EntidadCodigosAjenos IsNot Nothing _
                        AndAlso respSubcliente.EntidadCodigosAjenos.Count > 0 _
                            AndAlso respSubcliente.EntidadCodigosAjenos.First.CodigosAjenos IsNot Nothing _
                                AndAlso respSubcliente.EntidadCodigosAjenos.First.CodigosAjenos.Count > 0 Then
                    ViewStateCodigoAjenoEntrada.CodigoAjenoSubcliente = New ContractoServicio.CodigoAjeno.CodigoAjenoBase With {
                                                   .CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor(),
                                                   .CodAjeno = respSubcliente.EntidadCodigosAjenos.First.CodigosAjenos.First.CodAjeno,
                                                   .DesAjeno = respSubcliente.EntidadCodigosAjenos.First.CodigosAjenos.First.DesAjeno,
                                                   .OidCodigoAjeno = respSubcliente.EntidadCodigosAjenos.First.CodigosAjenos.First.OidCodigoAjeno}
                End If
            End If

            If ViewStateCodigoAjenoEntrada.CodigoAjenoPuntoServicio Is Nothing OrElse String.IsNullOrEmpty(ViewStateCodigoAjenoEntrada.CodigoAjenoPuntoServicio.OidCodigoAjeno) Then
                Dim respPtoServico = GetCodigosAjenos(ViewStateCodigoAjenoEntrada.OidPuntoServicio, "GEPR_TPUNTO_SERVICIO")
                If respPtoServico.EntidadCodigosAjenos IsNot Nothing _
                        AndAlso respPtoServico.EntidadCodigosAjenos.Count > 0 _
                            AndAlso respPtoServico.EntidadCodigosAjenos.First.CodigosAjenos IsNot Nothing _
                                AndAlso respPtoServico.EntidadCodigosAjenos.First.CodigosAjenos.Count > 0 Then
                    ViewStateCodigoAjenoEntrada.CodigoAjenoPuntoServicio = New ContractoServicio.CodigoAjeno.CodigoAjenoBase With {
                                                   .CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor(),
                                                   .CodAjeno = respPtoServico.EntidadCodigosAjenos.First.CodigosAjenos.First.CodAjeno,
                                                   .DesAjeno = respPtoServico.EntidadCodigosAjenos.First.CodigosAjenos.First.DesAjeno,
                                                   .OidCodigoAjeno = respPtoServico.EntidadCodigosAjenos.First.CodigosAjenos.First.OidCodigoAjeno}
                End If
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Obtém os dados do codigo ajeno por oidTablaGenesis
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 26/04/2013 Criado
    ''' </history>
    Private Function GetCodigosAjenos(idTablaGenesis As String, codTipoTablaGenesis As String) As ContractoServicio.CodigoAjeno.GetCodigosAjenos.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New ContractoServicio.CodigoAjeno.GetCodigosAjenos.Peticion

        'Preenche Codigo Ajeno 
        objPeticion.CodigosAjeno = New ContractoServicio.CodigoAjeno.GetCodigosAjenos.CodigoAjeno
        objPeticion.CodigosAjeno.CodTipoTablaGenesis = codTipoTablaGenesis
        objPeticion.CodigosAjeno.OidTablaGenesis = idTablaGenesis
        objPeticion.CodigosAjeno.CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor()
        objPeticion.ParametrosPaginacion = New ParametrosPeticionPaginacion
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        Dim objAccionCodigoAjeno As New LogicaNegocio.AccionCodigoAjeno

        ' chamar servicio
        Return objAccionCodigoAjeno.GetCodigosAjenos(objPeticion)

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
    ''' Consome a entidade recebida da tela chamadora
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeObjeto()

        If Session("objMantenimientoCodigosAjenosMAE") IsNot Nothing Then

            'Consome os subclientes passados
            ViewStateCodigoAjenoEntrada = CType(Session("objMantenimientoCodigosAjenosMAE"), CodigoAjenoSimples)
            ViewStateCodigoAjenoOriginal = CType(Session("objMantenimientoCodigosAjenosMAE"), CodigoAjenoSimples)

            'Remove da sessão
            Session("objMantenimientoCodigosAjenosMAE") = Nothing

        End If

    End Sub

    '•	AA: representa las 2 primeras posiciones correspondientes al código de país (GEPR_TPAIS.COD_PAIS)
    '•	BBB: representa las 3 primeras posiciones correspondientes al código de delegación (GEPR_TDELEGACION.COD_DELEGACION) – sólo deberán ser considerados los caracteres númericosnuméricos 
    '   (ejemplo: para un código “A1B2C3D4E5F6”, se deberá primero separar los valores numéricos, “123456” y después tomar los tres primeros caracteres “123”). De ser necesario se completará con 0 a la izquierda.
    '•	CCCCCC: representa las 6 primeras posiciones correspondientes al código ajeno del cliente. De ser necesario se completará con 0 a la izquierda (debe existir por lo menos un carácter informado).
    '•	DDDD: representa las 4 primeras últimas posiciones correspondientes al código ajeno del subcliente. De ser necesario se completará con 0 a la izquierda (debe existir por lo menos un carácter informado).
    '•	EE: representa las 2 primeras últimas posiciones correspondientes al código ajeno del punto de servicio. De ser necesario se completará con 0 a la izquierda (debe existir por lo menos un carácter informado).
    Public Shared Function CalculaCodigoSectorMAE(codigoDelegacion As String,
                                                  codigoCliente As String,
                                                  codigoSubCliente As String,
                                                  codigoPuntoServicio As String) As String

        ' Codigo delegacion
        'Dim codigoDelegacionNumerico As String = String.Empty
        '' Solamente numeros
        'For i As Integer = 0 To codigoDelegacion.Length - 1
        '    If IsNumeric(codigoDelegacion.Substring(i, 1)) Then codigoDelegacionNumerico &= codigoDelegacion.Substring(i, 1)
        'Next

        'codigoPais = codigoPais.Trim.PadLeft(2, "0")
        'codigoDelegacionNumerico = codigoDelegacionNumerico.Trim.PadLeft(3, "0")

        'Dim codigoSectorMAE As String = codigoPais.Substring(0, 2)
        'codigoSectorMAE &= codigoDelegacionNumerico.Substring(0, 3)

        Dim codigoSectorMAE As String = codigoDelegacion.Trim
        If Not String.IsNullOrEmpty(codigoCliente) Then
            codigoCliente = codigoCliente.Trim.PadLeft(6, "0")
            codigoSectorMAE &= codigoCliente.Substring(0, 6)
            If Not String.IsNullOrEmpty(codigoSubCliente) Then
                codigoSubCliente = codigoSubCliente.Trim.PadLeft(4, "0")
                codigoSectorMAE &= codigoSubCliente.Substring(codigoSubCliente.Length - 4)
                If Not String.IsNullOrEmpty(codigoPuntoServicio) Then
                    codigoPuntoServicio = codigoPuntoServicio.Trim.PadLeft(2, "0")
                    codigoSectorMAE &= codigoPuntoServicio.Substring(codigoPuntoServicio.Length - 2)
                    Return codigoSectorMAE.ToUpper()
                End If
            End If
        End If

        Return String.Empty
    End Function

    Private Function CalculaCodigoSectorMAE() As String

        If String.IsNullOrEmpty(ViewStateCodigoAjenoEntrada.CodigoSectorMAE) Then
            Dim codPais As String = Prosegur.Genesis.Web.Login.Parametros.Permisos.Usuario.CodigoPais

            Dim ajenoCliente As String = txtCodAjenoCliente.Text
            Dim ajenoSubCliente As String = txtCodAjenoSubcliente.Text
            Dim ajenoPtoServicio As String = txtCodAjenoPtoServicio.Text

            Return CalculaCodigoSectorMAE(ViewStateCodigoAjenoEntrada.CodDelegacion, ajenoCliente, ajenoSubCliente, ajenoPtoServicio)
        End If

        Return ViewStateCodigoAjenoEntrada.CodigoSectorMAE
    End Function

    Private Sub GrabarDatos()
        Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.CodAjeno = IIf(String.IsNullOrEmpty(txtCodAjenoCliente.Text), Nothing, txtCodAjenoCliente.Text)
        Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.DesAjeno = IIf(String.IsNullOrEmpty(txtDesAjenoCliente.Text), Nothing, txtDesAjenoCliente.Text)

        Me.ViewStateCodigoAjenoEntrada.CodigoAjenoSubcliente.CodAjeno = IIf(String.IsNullOrEmpty(txtCodAjenoSubcliente.Text), Nothing, txtCodAjenoSubcliente.Text)
        Me.ViewStateCodigoAjenoEntrada.CodigoAjenoSubcliente.DesAjeno = IIf(String.IsNullOrEmpty(txtDesAjenoSubcliente.Text), Nothing, txtDesAjenoSubcliente.Text)

        Me.ViewStateCodigoAjenoEntrada.CodigoAjenoPuntoServicio.CodAjeno = IIf(String.IsNullOrEmpty(txtCodAjenoPtoServicio.Text), Nothing, txtCodAjenoPtoServicio.Text)
        Me.ViewStateCodigoAjenoEntrada.CodigoAjenoPuntoServicio.DesAjeno = IIf(String.IsNullOrEmpty(txtDesAjenoPtoServicio.Text), Nothing, txtDesAjenoPtoServicio.Text)

        Me.ViewStateCodigoAjenoEntrada.Completo = False

        If Not String.IsNullOrEmpty(Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.CodAjeno) AndAlso
                Not String.IsNullOrEmpty(Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.DesAjeno) AndAlso
                    Not String.IsNullOrEmpty(Me.ViewStateCodigoAjenoEntrada.CodigoAjenoSubcliente.CodAjeno) AndAlso
                        Not String.IsNullOrEmpty(Me.ViewStateCodigoAjenoEntrada.CodigoAjenoSubcliente.DesAjeno) AndAlso
                            Not String.IsNullOrEmpty(Me.ViewStateCodigoAjenoEntrada.CodigoAjenoPuntoServicio.CodAjeno) AndAlso
                                Not String.IsNullOrEmpty(Me.ViewStateCodigoAjenoEntrada.CodigoAjenoPuntoServicio.DesAjeno) Then

            Me.ViewStateCodigoAjenoEntrada.CodigoSectorMAE = txtSectorMAE.Text
            Me.ViewStateCodigoAjenoEntrada.Completo = True

        End If
    End Sub
#End Region


End Class