
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Partial Public Class MantenimientoNivelesSaldos
    Inherits Base

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarScripts()

        'Aciona botão incluir quando o enter o pressionado.
        txtNivelSaldo.Attributes.Add("onkeypress", "EventoEnter('" & btnAnadir.ID & "_img', event);")
        txtCliente.Attributes.Add("onkeypress", "EventoEnter('" & btnAnadir.ID & "_img', event);")
        txtSubCliente.Attributes.Add("onkeypress", "EventoEnter('" & btnAnadir.ID & "_img', event);")
        txtPtoServicio.Attributes.Add("onkeypress", "EventoEnter('" & btnAnadir.ID & "_img', event);")
        txtNivelSaldo.Attributes.Add("onkeypress", "EventoEnter('" & btnAnadir.ID & "_img', event);")
        txtDefecto.Attributes.Add("onkeypress", "EventoEnter('" & btnAnadir.ID & "_img', event);")

        'Fecha a janela corrente
        btnCancelar.FuncaoJavascript = "window.close();"

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        txtCliente.TabIndex = 1
        txtSubCliente.TabIndex = 2
        txtPtoServicio.TabIndex = 3
        txtCliente.TabIndex = 4
        txtDefecto.TabIndex = 5
        ddlSubCanal.TabIndex = 6
        txtNivelSaldo.TabIndex = 7
        btnAnadir.TabIndex = 8
        btnLimpiar.TabIndex = 9
        btnGrabar.TabIndex = 10
        btnCancelar.TabIndex = 11
        Me.DefinirRetornoFoco(btnCancelar, txtCliente)

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        'Define como ação inical
        Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.NIVELESSALDOS
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False
        'Pegando a ação
        MyBase.Acao = Request.QueryString("acao")
    End Sub

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                If (Peticion Is Nothing) Then

                    'Informa ao usuário que o parâmetro passado 
                    Throw New Exception("err_passagem_parametro")

                End If

                ConsomeEntidade()

                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                    btnAnadir.Titulo = "btnAnadir"
                    btnAnadir.Tipo = Prosegur.Web.TipoBotao.Adicionar
                End If

                CarregarCampos()
                CarregarSubCanal()
                CarregarNivelSaldos()

            End If

            ' trata o foco dos campos
            TrataFoco()
            ConsomeNivelSaldo()

            ' chama a função que seta o tamanho das linhas do grid.
            TamanhoLinhas()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Protected Overrides Sub PreRenderizar()

        Try
            ControleBotoes()
        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("043_lbl_titulo_pagina")
        lblTitulo.Text = Traduzir("043_lbl_titulo_pagina")
        lblCliente.Text = Traduzir("043_lbl_cliente")
        lblSubCliente.Text = Traduzir("043_lbl_subcliente")
        lblPtoServicio.Text = Traduzir("043_lbl_puntoservicio")
        lblSubCanal.Text = Traduzir("043_lbl_sub_canal")
        lblNivelSaldo.Text = Traduzir("043_lbl_nivel_saldo")
        lblDefecto.Text = Traduzir("043_lbl_nivel_saldo_defecto")

    End Sub

    ''' <summary>
    ''' Controla o estado dos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 22/05/2013 Criado
    ''' </history>
    Public Sub ControleBotoes()

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta
                btnAnadir.Titulo = "btnAnadir"
                btnAnadir.Tipo = Prosegur.Web.TipoBotao.Adicionar

            Case Aplicacao.Util.Utilidad.eAcao.Baja
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                setContultar()

            Case Aplicacao.Util.Utilidad.eAcao.Busca

            Case Aplicacao.Util.Utilidad.eAcao.NoAction

            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                txtCliente.Focus()

        End Select

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

#End Region

#Region "[PROPRIEDADES]"

    Public Shared Property Peticion As PeticionNivelSaldos
        Get
            Return HttpContext.Current.Session("objPeticionNivelSaldos")
        End Get
        Set(value As PeticionNivelSaldos)
            HttpContext.Current.Session("objPeticionNivelSaldos") = value
        End Set
    End Property

    Public Shared Property Respuesta As RespuestaNivelSaldos
        Get
            Return HttpContext.Current.Session("objRespuestaNivelSaldos")
        End Get
        Set(value As RespuestaNivelSaldos)
            HttpContext.Current.Session("objRespuestaNivelSaldos") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena em viewstate os nivel saldo encontrados na busca.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 22/05/2013 - Criado
    ''' </history>
    Private Property NivelSaldo() As ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMovColeccion

        Get
            Return ViewState("NivelSaldo")
        End Get

        Set(value As ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMovColeccion)
            ViewState("NivelSaldo") = value
        End Set

    End Property

    ''' <summary>
    '''  Armazerna em viewstate os lista de nivelsaldo concatenados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ListaNivelSaldo() As List(Of ConfigNivelMovConcat)

        Get
            Return ViewState("ListaNivelSaldo")
        End Get

        Set(value As List(Of ConfigNivelMovConcat))
            ViewState("ListaNivelSaldo") = value
        End Set

    End Property

    ''' <summary>
    ''' Armazena coleção de SubCanais
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ListaSubCanal() As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion

        Get
            Return ViewState("ListaSubCanal")
        End Get

        Set(value As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion)
            ViewState("ListaSubCanal") = value
        End Set

    End Property

    ''' <summary>
    ''' Armazena em viewstate os nivel saldo editados.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 22/05/2013 - Criado
    ''' </history>
    Private Property NivelSaldosEditar() As ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMov

        Get
            Return ViewState("NivelSaldosEditar")
        End Get

        Set(value As ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMov)
            ViewState("NivelSaldosEditar") = value
        End Set

    End Property

    ''' <summary>
    ''' Armazena nivel saldo defecto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property Defecto() As ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMov

        Get
            Return ViewState("Defecto")
        End Get

        Set(value As ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMov)
            ViewState("Defecto") = value
        End Set

    End Property

    ''' <summary>
    '''  Armazena em viewstate os excluídos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property NivelSaldosExcluidos() As ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMovColeccion

        Get
            Return ViewState("NivelSaldosExcluidos")
        End Get

        Set(value As ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMovColeccion)
            ViewState("NivelSaldosExcluidos") = value
        End Set

    End Property

    ''' <summary>
    '''  Armazena em viewstate o nivel saldo concatenado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' [danielnunes] 22/05/2013 - Criado
    Private ReadOnly Property NivelSaldosEditarConcat() As ConfigNivelMovConcat

        Get
            If NivelSaldosEditar IsNot Nothing Then
                Return PrepararItemNivelSaldo(NivelSaldosEditar)
            Else
                Return Nothing
            End If
        End Get

    End Property

    ''' <summary>
    ''' viewstate que armazena entidade selecionada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property NivelSaldoEntidade() As Entidade
        Get
            Return ViewState("NivelSaldoEntidade")
        End Get
        Set(value As Entidade)
            ViewState("NivelSaldoEntidade") = value
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

    <Serializable()> _
    Public Class PeticionNivelSaldos
        Public Property OidCliente As String
        Public Property CodCliente As String
        Public Property DesCliente As String
        Public Property OidSubCliente As String
        Public Property CodSubCliente As String
        Public Property DesSubCliente As String
        Public Property OidPtoServicio As String
        Public Property CodPtoServicio As String
        Public Property DesPtoServicio As String
        Public Property NivelSaldos As ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMovColeccion
    End Class

    <Serializable()> _
    Public Class RespuestaNivelSaldos
        Public Property NivelSaldos As ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMovColeccion
    End Class

    <Serializable()> _
    Public Class ConfigNivelMovConcat
        Public Property OidConfigNivelMov As String
        Public Property CodCliente As String
        Public Property CodSubCliente As String
        Public Property CodPtoServicio As String
        Public Property SubCanal As String
        Public Property NivelSaldo As String
    End Class

    <Serializable()> _
    Public Class Entidade
        Public Property OidEntidade As String
        Public Property CodEntidade As String
        Public Property DesEntidade As String
    End Class

#End Region

#Region "[METODOS]"

    Private Sub CarregarCampos()

        If Peticion IsNot Nothing Then

            ' Cliente, SubCliente e PtoServicio
            Dim Cliente As New List(Of String)
            Dim SubCliente As New List(Of String)
            Dim PtoServicio As New List(Of String)

            If Peticion.CodCliente IsNot Nothing Then Cliente.Add(Peticion.CodCliente)
            If Peticion.DesCliente IsNot Nothing Then Cliente.Add(Peticion.DesCliente)
            txtCliente.Text = String.Join(" - ", Cliente.ToArray())

            If Peticion.CodSubCliente IsNot Nothing Then SubCliente.Add(Peticion.CodSubCliente)
            If Peticion.DesSubCliente IsNot Nothing Then SubCliente.Add(Peticion.DesSubCliente)
            txtSubCliente.Text = String.Join(" - ", SubCliente.ToArray())

            If Peticion.CodPtoServicio IsNot Nothing Then PtoServicio.Add(Peticion.CodPtoServicio)
            If Peticion.DesPtoServicio IsNot Nothing Then PtoServicio.Add(Peticion.DesPtoServicio)
            txtPtoServicio.Text = String.Join(" - ", PtoServicio.ToArray())

            ' Nivel Saldo
            If Peticion.NivelSaldos IsNot Nothing AndAlso Peticion.NivelSaldos.Count > 0 Then
                Dim nivelDefecto = Peticion.NivelSaldos.FirstOrDefault(Function(n) n.codSubCanal Is Nothing)

                If nivelDefecto IsNot Nothing Then
                    If nivelDefecto.configNivelSaldo IsNot Nothing Then
                        If (nivelDefecto.configNivelSaldo.codCliente IsNot Nothing) Then
                            txtDefecto.Text = nivelDefecto.configNivelSaldo.codCliente & " - " & nivelDefecto.configNivelSaldo.desCliente
                        End If
                        If nivelDefecto.configNivelSaldo.codSubcliente IsNot Nothing Then
                            txtDefecto.Text &= " / " & nivelDefecto.configNivelSaldo.codSubcliente & " - " & nivelDefecto.configNivelSaldo.desSubcliente
                        End If
                        If nivelDefecto.configNivelSaldo.codPtoServicio IsNot Nothing Then
                            txtDefecto.Text &= " / " & nivelDefecto.configNivelSaldo.codPtoServicio & " - " & nivelDefecto.configNivelSaldo.desPtoServicio
                        End If
                    End If
                End If
            End If

        End If

    End Sub

    ''' <summary>
    ''' Consome a entidade recebida da tela chamadora
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeEntidade()
        'Busca nome da entidade na QueryString
        If Request.QueryString("Entidade") IsNot Nothing Then
            ViewStateEntidade = Request.QueryString("Entidade")
        End If
    End Sub

    Private Sub ConsomeNivelSaldo()

        If BusquedaNivelSaldoPopup.Respuesta IsNot Nothing Then

            If NivelSaldo Is Nothing Then
                NivelSaldo = New ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMovColeccion()
            End If

            Dim nuevo As New ContractoServicio.Cliente.SetClientes.ConfigNivelMov

            If NivelSaldosEditar IsNot Nothing AndAlso NivelSaldosEditar.oidConfigNivelMovimiento IsNot Nothing Then
                nuevo.oidConfigNivelMovimiento = NivelSaldosEditar.oidConfigNivelMovimiento
            End If

            nuevo.configNivelSaldo = New ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelSaldo

            ' entidade de edição
            nuevo.bolActivo = True

            If Peticion IsNot Nothing Then

                If Peticion.CodCliente IsNot Nothing Then
                    nuevo.oidCliente = Peticion.OidCliente
                    nuevo.codCliente = Peticion.CodCliente
                    nuevo.desCliente = Peticion.DesCliente
                End If

                If Peticion.CodSubCliente IsNot Nothing Then
                    nuevo.oidSubCliente = Peticion.OidSubCliente
                    nuevo.codSubCliente = Peticion.CodSubCliente
                    nuevo.desSubCliente = Peticion.DesSubCliente
                End If

                If Peticion.CodPtoServicio IsNot Nothing Then
                    nuevo.oidPtoServicio = Peticion.OidPtoServicio
                    nuevo.codPtoServicio = Peticion.CodPtoServicio
                    nuevo.desPtoServicio = Peticion.DesPtoServicio
                End If

            End If

            ' dados selecionados na busca de nivel saldo
            If BusquedaNivelSaldoPopup.Respuesta.Cliente IsNot Nothing Then
                nuevo.configNivelSaldo.codCliente = BusquedaNivelSaldoPopup.Respuesta.Cliente.Codigo
                nuevo.configNivelSaldo.desCliente = BusquedaNivelSaldoPopup.Respuesta.Cliente.Descripcion
                nuevo.configNivelSaldo.oidCliente = BusquedaNivelSaldoPopup.Respuesta.Cliente.OidCliente
                txtNivelSaldo.Text = BusquedaNivelSaldoPopup.Respuesta.Cliente.Codigo & " - " & BusquedaNivelSaldoPopup.Respuesta.Cliente.Descripcion
            End If

            ' caso tenha selecionado subcliente
            If BusquedaNivelSaldoPopup.Respuesta.SubCliente IsNot Nothing Then
                nuevo.configNivelSaldo.codSubcliente = BusquedaNivelSaldoPopup.Respuesta.SubCliente.Codigo
                nuevo.configNivelSaldo.desSubcliente = BusquedaNivelSaldoPopup.Respuesta.SubCliente.Descripcion
                nuevo.configNivelSaldo.oidSubcliente = BusquedaNivelSaldoPopup.Respuesta.SubCliente.OidSubCliente
                txtNivelSaldo.Text &= " / " & BusquedaNivelSaldoPopup.Respuesta.SubCliente.Codigo & " - " & BusquedaNivelSaldoPopup.Respuesta.SubCliente.Descripcion
            End If

            ' caso tenha selecionado punto servicio
            If BusquedaNivelSaldoPopup.Respuesta.PuntoServicio IsNot Nothing Then
                nuevo.configNivelSaldo.oidPtoServicio = BusquedaNivelSaldoPopup.Respuesta.PuntoServicio.OidPuntoServicio
                nuevo.configNivelSaldo.codPtoServicio = BusquedaNivelSaldoPopup.Respuesta.PuntoServicio.Codigo
                nuevo.configNivelSaldo.desPtoServicio = BusquedaNivelSaldoPopup.Respuesta.PuntoServicio.Descripcion
                txtNivelSaldo.Text &= " / " & BusquedaNivelSaldoPopup.Respuesta.PuntoServicio.Codigo & " - " & BusquedaNivelSaldoPopup.Respuesta.PuntoServicio.Descripcion
            End If

            NivelSaldosEditar = nuevo

            BusquedaNivelSaldoPopup.Respuesta = Nothing

        End If

    End Sub

    ''' <summary>
    ''' Seta o tamanho das linhas do grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 22/05/2013 Criado
    ''' </history>
    Private Sub TamanhoLinhas()
        ProsegurGridViewNivelSaldo.RowStyle.Height = 20
    End Sub

    ''' <summary>
    ''' Carrega combo SubCanal
    ''' </summary>
    ''' <remarks></remarks>
    ''' [danielnunes] 22/05/2013 Criado
    Public Sub CarregarSubCanal()

        Dim objProxySubCanal As New Comunicacion.ProxyUtilidad
        Dim objPeticion As New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Respuesta
        Dim objSubCanal As IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal
        Dim objSubCanalCol As IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion = Nothing

        objRespuesta = objProxySubCanal.GetComboSubcanalesByCanal(objPeticion)

        If objRespuesta.Canales Is Nothing Then
            ddlSubCanal.Items.Clear()
            ddlSubCanal.Items.Add(New ListItem(Traduzir("043_ddl_selecione"), String.Empty))
            Exit Sub
        End If

        If Not ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            ddlSubCanal.Items.Clear()
            ddlSubCanal.Items.Add(New ListItem(Traduzir("043_ddl_selecione"), String.Empty))
            Exit Sub
        End If

        objSubCanalCol = New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion
        For Each item In objRespuesta.Canales
            For Each subcanal In item.SubCanales
                objSubCanal = New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal
                objSubCanalCol.Add(subcanal)
            Next
        Next

        ListaSubCanal = objSubCanalCol

        ddlSubCanal.AppendDataBoundItems = True
        ddlSubCanal.Items.Clear()
        ddlSubCanal.Items.Add(New ListItem(Traduzir("043_ddl_selecione"), String.Empty))
        ddlSubCanal.DataTextField = "descripcion"
        ddlSubCanal.DataValueField = "codigo"
        ddlSubCanal.DataSource = objSubCanalCol
        ddlSubCanal.DataBind()

    End Sub

    ''' <summary>
    ''' Setar configurações componentes da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' [danielnunes] 22/05/2013 Criado
    Private Sub setContultar()

        btnAnadir.Visible = False
        btnGrabar.Visible = False
        btnLimpiar.Visible = False
        btnCancelar.Visible = True
        btnBusquedaNivelSaldo.Visible = False

        txtCliente.Enabled = False
        txtSubCliente.Enabled = False
        txtPtoServicio.Enabled = False
        txtDefecto.Enabled = False
        ddlSubCanal.Enabled = False
        txtNivelSaldo.Enabled = False

        ProsegurGridViewNivelSaldo.Enabled = False
        ProsegurGridViewNivelSaldo.Columns(ProsegurGridViewNivelSaldo.Columns.Count - 1).Visible = False

        Me.DefinirRetornoFoco(btnCancelar, btnCancelar)

    End Sub

    ''' <summary>
    ''' Carrega Niveis Saldos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 22/05/2013 Criado
    ''' </history>
    Private Sub CarregarNivelSaldos(Optional recarregar As Boolean = False)

        If Not recarregar Then

            ' obter niveis saldos
            If (Peticion.NivelSaldos IsNot Nothing AndAlso Peticion.NivelSaldos.Count > 0) Then
                Defecto = New ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMov
                Defecto = Peticion.NivelSaldos.FirstOrDefault(Function(n) n.oidSubCanal Is Nothing AndAlso n.codSubCanal Is Nothing)

                If Defecto IsNot Nothing Then
                    Peticion.NivelSaldos.Remove(Defecto)
                End If

                If (Peticion.NivelSaldos IsNot Nothing AndAlso Peticion.NivelSaldos.Count > 0) Then

                    Me.NivelSaldo = New ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMovColeccion
                    Peticion.NivelSaldos.Where(Function(n) n.codSubCanal IsNot Nothing).ToList.ForEach(Sub(s) Me.NivelSaldo.Add(s))
                End If

            Else
                Me.NivelSaldo = Nothing
            End If

        End If

        ' validar se encontrou clientes
        If Me.NivelSaldo IsNot Nothing AndAlso Me.NivelSaldo.Count > 0 Then

            Me.ListaNivelSaldo = PreencheDadosGridNivelSaldo(Me.NivelSaldo)
            Dim objDt As DataTable = ProsegurGridViewNivelSaldo.ConvertListToDataTable(Me.ListaNivelSaldo)

            ' carregar controle
            ProsegurGridViewNivelSaldo.CarregaControle(objDt)
            pnlSemRegistro.Visible = False

        Else

            'Limpa a consulta
            ProsegurGridViewNivelSaldo.DataSource = Nothing
            ProsegurGridViewNivelSaldo.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
            pnlSemRegistro.Visible = True

        End If

    End Sub

    ''' <summary>
    ''' Formata os campos do objeto NivelSaldo
    ''' </summary>
    ''' <param name="objNivelSaldo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrepararItemNivelSaldo(objNivelSaldo As ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMov) As ConfigNivelMovConcat

        Dim SubCanal As New List(Of String)
        Dim Cliente As New List(Of String)
        Dim SubCliente As New List(Of String)
        Dim PtoServicio As New List(Of String)
        Dim cli As String = String.Empty
        Dim subCli As String = String.Empty
        Dim ptoServ As String = String.Empty

        Dim NivelSaldo As New List(Of String)
        Dim configItem As New ConfigNivelMovConcat

        ' subcanal
        If objNivelSaldo.codSubCanal IsNot Nothing Then SubCanal.Add(objNivelSaldo.codSubCanal)
        If objNivelSaldo.desSubCanal IsNot Nothing Then SubCanal.Add(objNivelSaldo.desSubCanal)

        ' nivel saldo
        If (objNivelSaldo.configNivelSaldo IsNot Nothing) Then

            ' cliente
            If objNivelSaldo.configNivelSaldo.codCliente IsNot Nothing Then Cliente.Add(objNivelSaldo.configNivelSaldo.codCliente)
            If objNivelSaldo.configNivelSaldo.desCliente IsNot Nothing Then Cliente.Add(objNivelSaldo.configNivelSaldo.desCliente)
            cli = String.Join(" - ", Cliente.ToArray())

            ' subcliente
            If objNivelSaldo.configNivelSaldo.codSubcliente IsNot Nothing Then SubCliente.Add(objNivelSaldo.configNivelSaldo.codSubcliente)
            If objNivelSaldo.configNivelSaldo.desSubcliente IsNot Nothing Then SubCliente.Add(objNivelSaldo.configNivelSaldo.desSubcliente)
            subCli = String.Join(" - ", SubCliente.ToArray())

            'PtoServicio
            If objNivelSaldo.configNivelSaldo.codPtoServicio IsNot Nothing Then PtoServicio.Add(objNivelSaldo.configNivelSaldo.codPtoServicio)
            If objNivelSaldo.configNivelSaldo.desPtoServicio IsNot Nothing Then PtoServicio.Add(objNivelSaldo.configNivelSaldo.desPtoServicio)
            ptoServ = String.Join(" - ", PtoServicio.ToArray())

            If Not String.IsNullOrEmpty(cli) Then NivelSaldo.Add(cli)
            If Not String.IsNullOrEmpty(subCli) Then NivelSaldo.Add(subCli)
            If Not String.IsNullOrEmpty(ptoServ) Then NivelSaldo.Add(ptoServ)

            configItem.OidConfigNivelMov = objNivelSaldo.oidConfigNivelMovimiento
            configItem.SubCanal = String.Join(" - ", SubCanal.ToArray())
            configItem.NivelSaldo = String.Join("/", NivelSaldo.ToArray())

            configItem.CodCliente = objNivelSaldo.configNivelSaldo.codCliente
            configItem.CodSubCliente = objNivelSaldo.configNivelSaldo.codSubcliente
            configItem.CodPtoServicio = objNivelSaldo.configNivelSaldo.codPtoServicio

        End If

        objNivelSaldo = Nothing

        Return configItem

    End Function

    ''' <summary>
    '''  Preenche o grid com niveis de saldo
    ''' </summary>
    ''' <param name="objNivelSaldoColeccion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PreencheDadosGridNivelSaldo(objNivelSaldoColeccion As ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMovColeccion) As List(Of ConfigNivelMovConcat)
        Dim listaNivelSaldoColeccion As New List(Of ConfigNivelMovConcat)
        If objNivelSaldoColeccion IsNot Nothing AndAlso objNivelSaldoColeccion.Count > 0 Then
            For Each item In objNivelSaldoColeccion
                If (item.bolActivo) Then
                    listaNivelSaldoColeccion.Add(PrepararItemNivelSaldo(item))
                End If
            Next
        End If
        Return listaNivelSaldoColeccion
    End Function

    ''' <summary>
    '''  Validar subcanal e nivelsaldo
    ''' </summary>
    ''' <param name="subCanal">subcanal selecionado</param>
    ''' <param name="nivelsaldo">item nivel saldo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Validado(subCanal As String, nivelsaldo As ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMov) As Boolean

        Dim Msg As String = ""

        If String.IsNullOrEmpty(subCanal) Then
            Msg &= String.Format(Traduzir("err_campo_obligatorio"), Traduzir("043_lbl_sub_canal")) & "<br/>"
            csvSubCanal.IsValid = False
        Else
            csvSubCanal.IsValid = True
        End If

        If (nivelsaldo Is Nothing) Then
            Msg &= String.Format(Traduzir("err_campo_obligatorio"), Traduzir("043_lbl_nivel_saldo")) & "<br/>"
            csvNivelSaldo.IsValid = False
        Else
            csvNivelSaldo.IsValid = True
        End If

        If Not String.IsNullOrEmpty(Msg) Then
            If Not ControleErro.VerificaErro(100, "", Msg) Then
                Exit Function
            End If
        End If

        Return True

    End Function

    ''' <summary>
    ''' Limpar os campos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LimparCampos()
        ddlSubCanal.Focus()
        ddlSubCanal.SelectedIndex = 0
        txtNivelSaldo.Text = String.Empty
        NivelSaldosEditar = Nothing
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        btnAnadir.Tipo = Prosegur.Web.TipoBotao.Adicionar
        btnAnadir.Titulo = "btnAnadir"
    End Sub

#End Region

#Region "[EVENTOS]"

    ''' <summary>
    ''' Esta função faz a conversão da linha selecionada no grid em um dt.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 22/05/2013 Criado
    ''' </history>
    Protected Sub ProsegurGridViewNivelSaldo_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridViewNivelSaldo.EPreencheDados

        Try

            Dim objDT As DataTable


            If Me.NivelSaldo IsNot Nothing AndAlso Me.NivelSaldo.Count > 0 Then

                pnlSemRegistro.Visible = False

                ' converter objeto para datatable(para efetuar a ordenação)
                Me.ListaNivelSaldo = PreencheDadosGridNivelSaldo(Me.NivelSaldo)

                objDT = ProsegurGridViewNivelSaldo.ConvertListToDataTable(Me.ListaNivelSaldo)

                ProsegurGridViewNivelSaldo.ControleDataSource = objDT

            Else

                'Limpa a consulta
                ProsegurGridViewNivelSaldo.DataSource = Nothing
                ProsegurGridViewNivelSaldo.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
                pnlSemRegistro.Visible = True

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 22/05/2013 Criado
    ''' </history>
    Protected Sub ProsegurGridViewNivelSaldo_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridViewNivelSaldo.EPager_SetCss
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
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Tradução do cabecalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 22/05/2013 Criado
    ''' </history>
    Private Sub ProsegurGridViewNivelSaldo_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewNivelSaldo.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("043_lbl_gdr_sub_canal")
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("043_lbl_gdr_nivel_saldo")
                CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("043_lbl_gdr_modificar")
                CType(e.Row.Cells(4).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("043_lbl_gdr_eliminar")
            End If

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento clique do botão aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 22/05/2013 Criado
    ''' </history>
    Private Sub btnGrabar_Click(sender As Object, e As System.EventArgs) Handles btnGrabar.Click

        Try

            If NivelSaldosEditar IsNot Nothing Then
                PreencherNivelSaldo()
            End If

            ' Verificando se objeto respuesta foi instaciado
            If Respuesta Is Nothing Then Respuesta = New RespuestaNivelSaldos

            ' Verificando de viewstate possue algum nivel saldo
            If Me.NivelSaldo IsNot Nothing AndAlso Me.NivelSaldo.Count > 0 Then

                ' adicionar defecto
                If Me.Defecto IsNot Nothing Then
                    NivelSaldo.Add(Defecto)
                End If

                ' adicionando niveis saldo excluidos
                If Me.NivelSaldosExcluidos IsNot Nothing AndAlso Me.NivelSaldosExcluidos.Count > 0 Then

                    Me.NivelSaldo.AddRange(Me.NivelSaldosExcluidos)

                End If

                ' atribuindo coleção a Respuesta
                Respuesta.NivelSaldos = Me.NivelSaldo

            Else

                If Me.Defecto IsNot Nothing Then
                    NivelSaldo.Add(Defecto)
                End If

                ' adicionando niveis saldo excluídos
                If NivelSaldosExcluidos IsNot Nothing AndAlso NivelSaldosExcluidos.Count > 0 Then

                    Respuesta.NivelSaldos = Me.NivelSaldosExcluidos

                Else

                    Respuesta.NivelSaldos = Nothing

                End If

            End If


            ' Fechar janela
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "NivelSaldo", "FecharAtualizarPaginaPai();", True)

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento clique do botão limpar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 22/05/2013 Criado
    ''' </history>
    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        LimparCampos()
    End Sub

    ''' <summary>
    ''' Evento clique do botão cancelar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' [danielnunes] 22/05/2013 Criado
    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click

        Try

            Session("objRespuestaNivelSaldos") = Nothing

            ' fechar janela
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "NivelSaldo", "window.close();", True)

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Private Sub PreencherNivelSaldo()

        Dim Msg As String = String.Empty

        'Aqui faz as validações baseadas nos campos da tela
        If Validado(ddlSubCanal.Text, NivelSaldosEditar) Then

            If btnAnadir.Tipo = Prosegur.Web.TipoBotao.Adicionar Then

                If (ListaNivelSaldo Is Nothing) Then
                    ListaNivelSaldo = New List(Of ConfigNivelMovConcat)
                End If

                ' buscar canal selecionado
                Dim SubCanal = ListaSubCanal.FirstOrDefault(Function(f) f.Codigo = ddlSubCanal.SelectedValue)

                ' buscar subcanal selecionado na lista existente
                Dim excepcionesSubCanal = NivelSaldo.FirstOrDefault(Function(f) f.oidSubCanal = SubCanal.OidSubCanal)

                ' buscar nivel saldo selecionado na lista existente
                Dim excepcionesNivelSaldo = NivelSaldo.FirstOrDefault(Function(f) f.configNivelSaldo.oidCliente = NivelSaldosEditar.oidCliente AndAlso _
                                                                                  f.configNivelSaldo.oidSubcliente = NivelSaldosEditar.oidSubCliente AndAlso _
                                                                                  f.configNivelSaldo.oidPtoServicio = NivelSaldosEditar.oidPtoServicio)

                If (Not excepcionesSubCanal IsNot Nothing AndAlso Not excepcionesNivelSaldo IsNot Nothing) Then

                    ' atribuir codigo e descrição do subcanal
                    NivelSaldosEditar.oidSubCanal = SubCanal.OidSubCanal
                    NivelSaldosEditar.codSubCanal = SubCanal.Codigo
                    NivelSaldosEditar.desSubCanal = SubCanal.Descripcion
                    NivelSaldosEditar.fyhVigencia = DateTime.Now

                    ' adicionar item novo na viewstate
                    NivelSaldo.Add(NivelSaldosEditar)

                    'Recarrega grid com dados da viewstate
                    CarregarNivelSaldos(True)

                    ' limpar variaveis
                    NivelSaldosEditar = Nothing
                    LimparCampos()

                Else
                    Msg = String.Format(Traduzir("043_msg_subcanal_ya_configurado"), ViewStateEntidade)
                    If Not ControleErro.VerificaErro(100, "", Msg) Then
                        Exit Sub
                    End If

                End If

            ElseIf btnAnadir.Tipo = Prosegur.Web.TipoBotao.Editar Then

                ' buscar subcanal selecionado
                Dim SubCanal = ListaSubCanal.FirstOrDefault(Function(f) f.Codigo = ddlSubCanal.SelectedValue)

                ' atribuir codigo e descrição do subcanal a viewstate
                NivelSaldosEditar.oidSubCanal = SubCanal.OidSubCanal
                NivelSaldosEditar.codSubCanal = SubCanal.Codigo
                NivelSaldosEditar.desSubCanal = SubCanal.Descripcion
                NivelSaldosEditar.fyhVigencia = DateTime.Now

                ' buscar item selecionado para alterar
                Dim removido1 = Me.NivelSaldo.FirstOrDefault(Function(f) f.oidConfigNivelMovimiento = NivelSaldosEditar.oidConfigNivelMovimiento)

                ' Caso o item seja novo, não haverá oid portanto a busca será por cliente, subcliente e ptoservicio
                If removido1 Is Nothing Then
                    removido1 = Me.NivelSaldo.FirstOrDefault(Function(f) f.codCliente = NivelSaldosEditar.codCliente _
                                                                 AndAlso f.codSubCliente = NivelSaldosEditar.codSubCliente _
                                                                 AndAlso f.codPtoServicio = NivelSaldosEditar.codPtoServicio)
                End If

                ' obter indice do registro
                Dim indice As Integer = Me.NivelSaldo.IndexOf(removido1)

                ' excluir item anterior
                Me.NivelSaldo.Remove(removido1)

                ' incluir item novo modificado
                Me.NivelSaldo.Insert(indice, NivelSaldosEditar)

                ' buscar item selecionado para alterar
                Dim removido2 = Me.ListaNivelSaldo.FirstOrDefault(Function(f) f.OidConfigNivelMov = NivelSaldosEditarConcat.OidConfigNivelMov)

                ' caso o item seja novo, não haverá oid portanto a busca será por cliente, subcliente e ptoservicio
                If removido2 Is Nothing Then
                    removido2 = Me.ListaNivelSaldo.FirstOrDefault(Function(f) f.CodCliente = NivelSaldosEditar.codCliente _
                                                                      AndAlso f.CodSubCliente = NivelSaldosEditar.codSubCliente _
                                                                      AndAlso f.CodPtoServicio = NivelSaldosEditar.codPtoServicio)
                End If

                ' obter indice do registro
                indice = Me.ListaNivelSaldo.IndexOf(removido2)

                ' excluir item anterior
                Me.ListaNivelSaldo.Remove(removido2)

                ' incluir item novo modificado
                Me.ListaNivelSaldo.Insert(indice, NivelSaldosEditarConcat)

                'Recarrega grid com dados da viewstate
                CarregarNivelSaldos(True)

                ' limpar variaveis
                NivelSaldosEditar = Nothing
                LimparCampos()

            End If

        End If

    End Sub

    ''' <summary>
    ''' Método grava ou editar nivel saldo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAnadir_Click(sender As Object, e As EventArgs) Handles btnAnadir.Click

        Try
            PreencherNivelSaldo()
        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Chamada da tela para obter cliente, subcliente e ponto servicio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnBusquedaNivelSaldo_Click(sender As Object, e As EventArgs) Handles btnBusquedaNivelSaldo.Click
        Try

            BusquedaNivelSaldoPopup.Peticion = New BusquedaNivelSaldoPopup.PeticionBusqueda()

            If NivelSaldosEditar IsNot Nothing Then

                ' CLIENTE
                Dim Cliente = New ContractoServicio.Utilidad.GetComboClientes.Cliente
                Dim SubCliente = New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente
                Dim PuntoServicio = New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio

                If NivelSaldosEditar.configNivelSaldo.codCliente IsNot Nothing Then
                    Cliente.Codigo = NivelSaldosEditar.configNivelSaldo.codCliente
                    Cliente.Descripcion = NivelSaldosEditar.configNivelSaldo.desCliente
                    Cliente.OidCliente = NivelSaldosEditar.configNivelSaldo.oidCliente
                    Cliente.TotalizadorSaldo = True

                    Session("ClienteSelecionado") = Cliente
                Else
                    Session("ClienteSelecionado") = Nothing
                End If

                ' SUBCLIENTE
                If NivelSaldosEditar.configNivelSaldo.codSubcliente IsNot Nothing Then
                    Dim subClientes As New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion
                    subClientes.Add(SubCliente)

                    SubCliente.Codigo = NivelSaldosEditar.configNivelSaldo.codSubcliente
                    SubCliente.Descripcion = NivelSaldosEditar.configNivelSaldo.desSubcliente
                    SubCliente.OidSubCliente = NivelSaldosEditar.configNivelSaldo.oidSubcliente
                    SubCliente.TotalizadorSaldo = True

                    Session("SubClientesSelecionados") = subClientes
                Else
                    Session("SubClientesSelecionados") = Nothing
                End If

                ' PUNTO SERVICIO
                If NivelSaldosEditar.configNivelSaldo.codPtoServicio IsNot Nothing Then
                    Dim puntos As New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion
                    puntos.Add(PuntoServicio)
                    PuntoServicio.Codigo = NivelSaldosEditar.configNivelSaldo.codPtoServicio
                    PuntoServicio.Descripcion = NivelSaldosEditar.configNivelSaldo.desPtoServicio
                    PuntoServicio.OidPuntoServicio = NivelSaldosEditar.configNivelSaldo.oidPtoServicio
                    PuntoServicio.TotalizadorSaldo = True

                    Session("PuntosServicioSelecionados") = puntos
                Else
                    Session("PuntosServicioSelecionados") = Nothing
                End If

            Else
                Session("ClienteSelecionado") = Nothing
                Session("SubClientesSelecionados") = Nothing
                Session("PuntosServicioSelecionados") = Nothing
            End If

            Dim url As String = "BusquedaNivelSaldoPopup.aspx?acao=" & MyBase.Acao & "&campoObrigatorio=False"

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "script_popup_nivel", "AbrirPopupModal('" & url & "', 300, 788,'btnBusquedaNivelSaldo');", True)

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Verificar qual accion foi selecionada no grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ProsegurGridViewNivelSaldo_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles ProsegurGridViewNivelSaldo.RowCommand
        Try
            Select Case e.CommandName

                Case "AccionEditar"
                    NivelSaldosEditar = NivelSaldo.FirstOrDefault(Function(f) f.oidConfigNivelMovimiento = e.CommandArgument)
                    ddlSubCanal.SelectedIndex = ddlSubCanal.Items.IndexOf(ddlSubCanal.Items.FindByValue(NivelSaldosEditar.codSubCanal))
                    txtNivelSaldo.Text = PrepararItemNivelSaldo(NivelSaldosEditar).NivelSaldo

                    btnAnadir.Tipo = Prosegur.Web.TipoBotao.Editar
                    btnAnadir.Titulo = "btnModificacion"
                    MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion
                    MyBase.TraduzirControles()
                    CarregarNivelSaldos(True)

                Case "AccionBorrar"

                    Dim nivelExcluido = NivelSaldo.FirstOrDefault(Function(f) f.oidConfigNivelMovimiento = e.CommandArgument)
                    If (Not String.IsNullOrEmpty(nivelExcluido.oidConfigNivelMovimiento)) Then
                        nivelExcluido.bolActivo = False
                        If NivelSaldosExcluidos Is Nothing Then
                            NivelSaldosExcluidos = New ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMovColeccion
                        End If
                        NivelSaldosExcluidos.Add(nivelExcluido)
                    End If

                    NivelSaldo.Remove(NivelSaldo.FirstOrDefault(Function(f) f.oidConfigNivelMovimiento = e.CommandArgument))
                    CarregarNivelSaldos(True)

            End Select

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

#End Region

End Class