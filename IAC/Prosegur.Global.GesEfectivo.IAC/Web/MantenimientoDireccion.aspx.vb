Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis


''' <summary>
''' PopUp de Direccion
''' </summary>
''' <remarks></remarks>
''' <history>
''' [pgoncalves] 30/04/2013 Criado
''' </history>
Public Class MantenimientoDireccion
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Classes usadas apenas para transferir dados da entidade a ser consultada 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>[pgoncalves] 02/05/2013 - Criado</history>
    <Serializable()> _
    Public Class CodigoTablaGenesis


        Public Property CodGenesis As String

        Public Property DesGenesis As String

        Public Property OidGenesis As String

        Public Property Direcion As ContractoServicio.Direccion.DireccionBase

        Public Shared Function ObtenerDirecion(CodTipoTablaGenesis As String, OidTablaGenesis As String,
                                                Master As Prosegur.[Global].GesEfectivo.IAC.Web.Master.Master,
                                                ControleErro As Global.Prosegur.[Global].GesEfectivo.IAC.Web.Erro) As ContractoServicio.Direccion.Direccion

            Dim objPeticion As New ContractoServicio.Direccion.GetDirecciones.Peticion
            Dim objRespuesta As New ContractoServicio.Direccion.GetDirecciones.Respuesta
            Dim objProxy As New Comunicacion.ProxyDireccion

            objPeticion.codTipoTablaGenesis = CodTipoTablaGenesis
            objPeticion.oidTablaGenesis = OidTablaGenesis
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False

            objRespuesta = objProxy.GetDirecciones(objPeticion)

            If Master IsNot Nothing Then
                'Trata resposta do serviço
                If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                    Master.ControleErro.ShowError(objRespuesta.MensajeError, False)
                    Return Nothing
                End If
            End If
            If ControleErro IsNot Nothing Then
                ' tratar retorno
                If Not ControleErro.VerificaErro(objRespuesta.CodigoError, "", objRespuesta.MensajeError, ) Then
                    Return Nothing
                End If
            End If

            If objRespuesta.Direccion IsNot Nothing AndAlso objRespuesta.Direccion.Count > 0 Then
                Return objRespuesta.Direccion(0)
            Else
                Return Nothing
            End If

        End Function

    End Class

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 30/04/2013 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascript
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 30/04/2013 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim s As String = String.Empty
    End Sub

    ''' <summary>
    ''' Seta tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 30/04/2013 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()
        
    End Sub

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
    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 30/04/2013 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.DIRECCION
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False

        If Request("acao") IsNot Nothing Then
            MyBase.Acao = Request("acao")
        End If

    End Sub

    ''' <summary>
    ''' Primeiro metodo chamado quando inicia a pagina
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 30/04/2013 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            'Cabecalho1.VersionVisible = False
            If Not Page.IsPostBack Then

                'Consome os ojetos passados
                ConsomeEntidade()

                If (ViewStateDireccionEntrada Is Nothing) Then
                    'Informa ao usuário que o parâmetro passado 
                    Throw New Exception(Traduzir("err_passagem_parametro"))
                End If

                If (ViewStateDireccionEntrada IsNot Nothing) Then
                    ' Preenche os campos do cabeçalho
                    txtCodigo.Text = ViewStateDireccionEntrada.CodGenesis
                    txtDescricao.Text = ViewStateDireccionEntrada.DesGenesis
                    'Carrega os dados
                    getDireccion()
                End If

                Dim CodTipoTablaGenesis As String = Request.QueryString("Entidade")

                txtPais.Focus()
            End If
            ' trata o foco dos campos
            TrataFoco()

            'End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 24/04/2013 Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()
            btnBaja.OnClientClick = "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBajaConfirmado.ClientID & "');"
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Traduz os controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 24/04/2013 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("035_lbl_direccion")
        lblDadosAdicionais.Text = Traduzir("035_lbl_dados_adicionais")
        lblDescricao.Text = Traduzir("035_lbl_descripcion")
        lblDireccion.Text = Traduzir("035_lbl_direccion")
        lblDireccion1.Text = Traduzir("035_lbl_direccion1")
        lblDireccion2.Text = Traduzir("035_lbl_direccion2")
        lblEmail.Text = Traduzir("035_lbl_email")
        lblCampo1.Text = Traduzir("035_lbl_campoadicional")
        lblCampo2.Text = Traduzir("035_lbl_campoadicional2")
        lblCampo3.Text = Traduzir("035_lbl_campoadicional3")
        lblCategoria1.Text = Traduzir("035_lbl_categoriaadicional")
        lblCategoria2.Text = Traduzir("035_lbl_categoriaadicional2")
        lblCategoria3.Text = Traduzir("035_lbl_categoriaadicional3")
        lblCiudad.Text = Traduzir("035_lbl_ciudad")
        lblCodigo.Text = Traduzir("035_lbl_codigo")
        lblCodigoPostal.Text = Traduzir("035_lbl_codigopostal")
        lblNFiscal.Text = Traduzir("035_lbl_nfiscal")
        lblPais.Text = Traduzir("035_lbl_pais")
        lblProvincia.Text = Traduzir("035_lbl_Provincia")
        lblTelefono.Text = Traduzir("035_lbl_telefono")
        csvDireccion.ErrorMessage = Traduzir("035_msg_erro_Direccion")
        csvDireccion2.ErrorMessage = Traduzir("035_msg_erro_Direccion")
        csvPais.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("035_lbl_pais"))

        'botoes
        btnGrabar.Text = Traduzir("btnGrabar")
        btnBaja.Text = Traduzir("btnBaja")

    End Sub

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Consome a entidade recebida da tela chamadora
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeEntidade()
        'Busca nome da entidade na QueryString
        If Request.QueryString("Entidade") IsNot Nothing Then
            ViewStateEntidade = Request.QueryString("Entidade")
        End If

        If Session("obj" & ViewStateEntidade) IsNot Nothing Then

            'Consome os subclientes passados
            ViewStateDireccionEntrada = CType(Session("obj" & ViewStateEntidade), CodigoTablaGenesis)
            Me.Direccion = ViewStateDireccionEntrada.Direcion

            'Remove da sessão
            'Session("obj" & ViewStateEntidade) = Nothing

            'Remove da session 2
            Session.Remove("obj" & ViewStateEntidade)
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

    Private Sub getDireccion()

        'Dim objPeticion As New ContractoServicio.Direccion.GetDirecciones.Peticion
        'Dim objRespuesta As New ContractoServicio.Direccion.GetDirecciones.Respuesta
        'Dim objProxy As New Comunicacion.ProxyDireccion

        'objPeticion.codTipoTablaGenesis = CodTipoTablaGenesis
        'objPeticion.oidTablaGenesis = OidTablaGenesis
        'objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        'objRespuesta = objProxy.GetDirecciones(objPeticion)

        If Direccion IsNot Nothing Then

            Oid_Direccion = Direccion.oidDireccion
            txtCampo2.Text = Direccion.desCampoAdicional2
            txtCampo3.Text = Direccion.desCampoAdicional3
            txtCampos1.Text = Direccion.desCampoAdicional1
            txtCategoria1.Text = Direccion.desCategoriaAdicional1
            txtCategoria2.Text = Direccion.desCategoriaAdicional2
            txtCategoria3.Text = Direccion.desCategoriaAdicional3
            txtCiudad.Text = Direccion.desCiudad
            txtCodigoPostal.Text = Direccion.codPostal
            txtDireccion.Text = Direccion.desDireccionLinea1
            txtDireccion2.Text = Direccion.desDireccionLinea2
            txtEmail.Text = Direccion.desEmail
            txtNFiscal.Text = Direccion.codFiscal
            txtPais.Text = Direccion.desPais
            txtProvincia.Text = Direccion.desProvincia
            txtTelefono.Text = Direccion.desNumeroTelefono

            btnBaja.Visible = True
        End If

    End Sub

    Private Sub ExecutaGrabar()
        Try
            ValidarCamposObrigatorios = True

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If

            Dim objPeticion As New ContractoServicio.Direccion.DireccionBase

            objPeticion.bolBaja = False
            objPeticion.codFiscal = txtNFiscal.Text
            objPeticion.codPostal = txtCodigoPostal.Text
            objPeticion.desCampoAdicional1 = txtCampos1.Text
            objPeticion.desCampoAdicional2 = txtCampo2.Text
            objPeticion.desCampoAdicional3 = txtCampo3.Text
            objPeticion.desCategoriaAdicional1 = txtCategoria1.Text
            objPeticion.desCategoriaAdicional2 = txtCategoria2.Text
            objPeticion.desCategoriaAdicional3 = txtCategoria3.Text
            objPeticion.desCiudad = txtCiudad.Text
            objPeticion.desDireccionLinea1 = txtDireccion.Text
            objPeticion.desDireccionLinea2 = txtDireccion2.Text
            objPeticion.desEmail = txtEmail.Text
            objPeticion.desNumeroTelefono = txtTelefono.Text
            objPeticion.desPais = txtPais.Text
            objPeticion.desProvincia = txtProvincia.Text
            objPeticion.oidDireccion = Oid_Direccion

            'Atribui na seção a petição preenchida.
            DireccionPeticion = New ContractoServicio.Direccion.DireccionColeccionBase

            'Adiciona apenas um
            DireccionPeticion.Add(objPeticion)

            'ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_grabado_suceso", "alert('" & Traduzir("001_msg_grabado_suceso") & "'); ", True)

            'ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "DireccionOk", "window.returnValue=0;window.close();", True)
            Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','');"
            ' fechar janela
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ToleranciaOk", jsScript, True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
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

                If String.IsNullOrEmpty(txtPais.Text) Then
                    strErro.Append(csvPais.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvPais.IsValid = False

                    txtPais.Focus()
                    focoSetado = True
                Else
                    csvPais.IsValid = True
                End If

                'Verifica se ao menos uma direccion foi enviada.
                If String.IsNullOrEmpty(txtDireccion.Text) AndAlso _
                    String.IsNullOrEmpty(txtDireccion2.Text) Then

                    strErro.Append(csvDireccion.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDireccion.IsValid = False
                    csvDireccion2.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDireccion.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDireccion.IsValid = True
                    csvDireccion2.IsValid = True
                End If

            End If

        End If
        Return strErro.ToString

    End Function

#End Region

#Region "[EVENTOS]"

    Private Sub btnGrabar_Click(sender As Object, e As System.EventArgs) Handles btnGrabar.Click
        ExecutaGrabar()
    End Sub

#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' Controla o estado dos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 30/04/2013 Criado
    ''' </history>
    Public Sub ControleBotoes()

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Busca

            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                setConsultar()

            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                txtPais.Focus()

            Case Aplicacao.Util.Utilidad.eAcao.NoAction


        End Select

        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem de erro
      
    End Sub

    Private Sub setConsultar()

        txtCampos1.Enabled = False
        txtCampo2.Enabled = False
        txtCampo3.Enabled = False
        txtCategoria1.Enabled = False
        txtCategoria2.Enabled = False
        txtCategoria3.Enabled = False
        txtCiudad.Enabled = False
        txtCodigo.Enabled = False
        txtCodigoPostal.Enabled = False
        txtDescricao.Enabled = False
        txtDireccion.Enabled = False
        txtDireccion2.Enabled = False
        txtEmail.Enabled = False
        txtNFiscal.Enabled = False
        txtPais.Enabled = False
        txtProvincia.Enabled = False
        txtTelefono.Enabled = False

        btnBaja.Visible = False
        btnGrabar.Visible = False


    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Armazena o endereço encontrado na busca.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 30/04/2013 Criado
    ''' </history>
    Private Property Direccion() As ContractoServicio.Direccion.DireccionBase
        Get
            Return DirectCast(ViewState("Direccion"), ContractoServicio.Direccion.DireccionBase)
        End Get
        Set(value As ContractoServicio.Direccion.DireccionBase)
            ViewState("Direccion") = value
        End Set
    End Property

    ''' <summary>
    ''' Persistir os dados de Direccion na sessão
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 30/04/2013 Criado
    ''' </history>
    Private Property DireccionPeticion() As ContractoServicio.Direccion.DireccionColeccionBase
        Get
            Return DirectCast(Session("DireccionPeticion"), ContractoServicio.Direccion.DireccionColeccionBase)
        End Get
        Set(value As ContractoServicio.Direccion.DireccionColeccionBase)
            Session("DireccionPeticion") = value
        End Set
    End Property


    ''' <summary>
    ''' Determina se é necessário validar os campos obrigatórios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena codigo da entidade passada por QueryString
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ViewStateEntidade() As String
        Get
            Return ViewState("Entidade")
        End Get
        Set(value As String)
            ViewState("Entidade") = value
        End Set
    End Property

    ''' <summary>
    ''' Objeto CodigoAjenoSimples passado por session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ViewStateDireccionEntrada() As CodigoTablaGenesis
        Get
            Return Session("CodigoAjenoEntrada")
        End Get
        Set(value As CodigoTablaGenesis)
            Session("CodigoAjenoEntrada") = value
        End Set
    End Property

    'Armazena o Oid_Direecion vindo da busca
    Public Property Oid_Direccion() As String
        Get
            Return ViewState("Oid_Direccion")
        End Get
        Set(value As String)
            ViewState("Oid_Direccion") = value
        End Set
    End Property

#End Region

    Private Sub btnBajaConfirmado_Click(sender As Object, e As EventArgs) Handles btnBajaConfirmado.Click
        Try
            Dim objPeticion As New ContractoServicio.Direccion.DireccionBase

            objPeticion.bolBaja = True
            objPeticion.codFiscal = txtNFiscal.Text
            objPeticion.codPostal = txtCodigoPostal.Text
            objPeticion.desCampoAdicional1 = txtCampos1.Text
            objPeticion.desCampoAdicional2 = txtCampo2.Text
            objPeticion.desCampoAdicional3 = txtCampo3.Text
            objPeticion.desCategoriaAdicional1 = txtCategoria1.Text
            objPeticion.desCategoriaAdicional2 = txtCategoria2.Text
            objPeticion.desCategoriaAdicional3 = txtCategoria3.Text
            objPeticion.desCiudad = txtCiudad.Text
            objPeticion.desDireccionLinea1 = txtDireccion.Text
            objPeticion.desDireccionLinea2 = txtDireccion2.Text
            objPeticion.desEmail = txtEmail.Text
            objPeticion.desNumeroTelefono = txtTelefono.Text
            objPeticion.desPais = txtPais.Text
            objPeticion.desProvincia = txtProvincia.Text
            objPeticion.oidDireccion = Direccion.oidDireccion

            'Atribui na seção a petição preenchida.
            DireccionPeticion = New ContractoServicio.Direccion.DireccionColeccionBase

            'Atribui na seção a petição preenchida.
            DireccionPeticion.Add(objPeticion)

            ' fechar janela
            ' ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ClienteOk", "window.close();", True)
            Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','');"
            ' fechar janela
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ToleranciaOk", jsScript, True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
End Class