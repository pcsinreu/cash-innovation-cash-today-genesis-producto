Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Comon.Paginacion

Public Class MantenimientoCodigosAjenosBancoPlanificacion
    Inherits Base

    <Serializable()>
    Public Class CodigoAjenoSimples

        Public Property OidCliente As String

        Public Property CodCliente As String

        Public Property DesCliente As String

        Public Property CodigoAjenoCliente As ContractoServicio.CodigoAjeno.CodigoAjenoBase

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

                End If
            End If

            ' trata o foco dos campos
            TrataFoco()

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
            Return ViewState("CodigoAjenoEntradaOriginal")
        End Get
        Set(value As CodigoAjenoSimples)
            ViewState("CodigoAjenoEntradaOriginal") = value
        End Set
    End Property

    Public Property CodigosAjenosBancoPlanificacion As CodigoAjenoSimples
        Get
            Return Session("objMantenimientoCodigosAjenosBancoPlanificacion")
        End Get
        Set(value As CodigoAjenoSimples)
            Session("objMantenimientoCodigosAjenosBancoPlanificacion") = value
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
                CodigosAjenosBancoPlanificacion = Me.ViewStateCodigoAjenoEntrada

                Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
                ' fechar janela
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "MantenimientoCodigosAjenosBancoPlanificacion", jsScript, True)
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
        If Me.ViewStateCodigoAjenoOriginal Is Nothing OrElse Me.ViewStateCodigoAjenoOriginal.CodigoAjenoCliente Is Nothing OrElse Me.ViewStateCodigoAjenoOriginal.CodigoAjenoCliente.CodAjeno <> txtCodAjenoCliente.Text Then

            If Me.ViewStateCodigoAjenoEntrada Is Nothing Then
                Me.ViewStateCodigoAjenoEntrada = New CodigoAjenoSimples()
            End If

            If Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente Is Nothing Then
                Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente = New ContractoServicio.CodigoAjeno.CodigoAjenoBase()
            End If

            Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.CodAjeno = If(String.IsNullOrEmpty(txtCodAjenoCliente.Text), Nothing, txtCodAjenoCliente.Text)
            Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.DesAjeno = If(String.IsNullOrEmpty(txtDesAjenoCliente.Text), Nothing, txtDesAjenoCliente.Text)

            objPeticion.CodTipoTablaGenesis = "GEPR_TCLIENTE"
            objPeticion.CodIdentificador = Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.CodIdentificador
            objPeticion.CodAjeno = Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.CodAjeno
            objPeticion.OidCodigoAjeno = Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.OidCodigoAjeno
            objPeticion.CodIdentificador = "MAE"

            'If objPeticion.OidCodigoAjeno Is Nothing OrElse String.IsNullOrEmpty(objPeticion.OidCodigoAjeno) Then
            '    objPeticion.OidCodigoAjeno = String.Format("{0}-{1}", objPeticion.CodTipoTablaGenesis, objPeticion.CodAjeno)
            '    Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.OidCodigoAjeno = String.Format("{0}-{1}", objPeticion.CodTipoTablaGenesis, objPeticion.CodAjeno)
            'End If


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

            'CodAjeno
            'DesAjeno
            Dim modificado As Boolean = False



            If ViewStateCodigoAjenoOriginal IsNot Nothing AndAlso Aplicacao.Util.Utilidad.HayModificaciones(ViewStateCodigoAjenoEntrada.CodigoAjenoCliente, ViewStateCodigoAjenoOriginal.CodigoAjenoCliente) Then
                If ViewStateCodigoAjenoOriginal.CodigoAjenoCliente IsNot Nothing OrElse (ViewStateCodigoAjenoOriginal.CodigoAjenoCliente Is Nothing AndAlso (ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.CodAjeno IsNot Nothing OrElse ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.DesAjeno IsNot Nothing)) Then
                    modificado = True
                End If
            End If
            If modificado Then


                MyBase.ExibirMensagemSimNao(MyBase.RecuperarValorDic("MSG_INFO_CERRAR_PANTALLA_CODIGOS_AJENOS"), jsScript)

            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "MantenimientoCodigoAjeno", jsScript, True)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

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
                txtCodAjenoCliente.Focus()

        End Select

    End Sub

    Private Sub setConsultar()

        btnGrabar.Enabled = False
        txtDesAjenoCliente.Enabled = False
        txtCodAjenoCliente.Enabled = False
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

        If CodigosAjenosBancoPlanificacion IsNot Nothing Then

            'Consome os subclientes passados
            ViewStateCodigoAjenoEntrada = CodigosAjenosBancoPlanificacion
            ViewStateCodigoAjenoOriginal = CodigosAjenosBancoPlanificacion

            'Remove da sessão
            CodigosAjenosBancoPlanificacion = Nothing

        End If

    End Sub



    Private Sub GrabarDatos()

        If (Me.ViewStateCodigoAjenoEntrada Is Nothing) Then Me.ViewStateCodigoAjenoEntrada = New CodigoAjenoSimples
        If (Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente Is Nothing) Then Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente = New ContractoServicio.CodigoAjeno.CodigoAjenoBase

        Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.CodAjeno = If(String.IsNullOrEmpty(txtCodAjenoCliente.Text), Nothing, txtCodAjenoCliente.Text)
        Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.DesAjeno = If(String.IsNullOrEmpty(txtDesAjenoCliente.Text), Nothing, txtDesAjenoCliente.Text)


        Me.ViewStateCodigoAjenoEntrada.Completo = False

        If Not String.IsNullOrEmpty(Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.CodAjeno) AndAlso
                Not String.IsNullOrEmpty(Me.ViewStateCodigoAjenoEntrada.CodigoAjenoCliente.DesAjeno) Then

            Me.ViewStateCodigoAjenoEntrada.Completo = True

        End If
    End Sub
#End Region


End Class