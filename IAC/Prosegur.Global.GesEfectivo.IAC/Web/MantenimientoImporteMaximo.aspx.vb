Imports System.Collections.ObjectModel
Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Paginacion

Public Class MantenimientoImporteMaximo
    Inherits Base

#Region "[VARIÁVEIS]"

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

#Region "[HelpersCliente]"
    Public Property Clientes As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
        Get
            Return ucClientes.Clientes
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))
            ucClientes.Clientes = value
        End Set
    End Property

    Private WithEvents _ucClientes As ucCliente
    Public Property ucClientes() As ucCliente
        Get
            If _ucClientes Is Nothing Then
                _ucClientes = LoadControl(ResolveUrl("~\Controles\Helpers\ucCliente.ascx"))
                _ucClientes.ID = Me.ID & "_ucClientesImporteMaximo"
                AddHandler _ucClientes.Erro, AddressOf ErroControles
                phCliente.Controls.Add(_ucClientes)
            End If
            Return _ucClientes
        End Get
        Set(value As ucCliente)
            _ucClientes = value
        End Set
    End Property

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagem(If(TypeOf e.Erro Is Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub
    Private Sub ConfigurarControle_Cliente()

        Me.ucClientes.SelecaoMultipla = False
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.ClienteObrigatorio = True
        Me.ucClientes.TipoBanco = True
        Me.ucClientes.ClienteTitulo = Traduzir("087_lbl_banco")
        Me.ucClientes.TotalizadorSaldo = True
        Me.ucClientes.ucCliente.ControleHabilitado = True

        If Clientes IsNot Nothing Then
            Me.ucClientes.Clientes = Clientes
        End If

    End Sub
    Private Sub ucClientes_OnControleAtualizado() Handles _ucClientes.UpdatedControl
        Try
            If ucClientes.Clientes IsNot Nothing Then
                Clientes = ucClientes.Clientes
                ConsomeCliente()
            End If

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub AtualizaDadosHelperCliente(observableCollection As ObservableCollection(Of Comon.Clases.Cliente))
        Dim dadosCliente As New Comon.RespuestaHelper
        dadosCliente.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

        For Each c In observableCollection
            If Not String.IsNullOrEmpty(c.Identificador) Then
                Dim DadosExibir As New Comon.Helper.Respuesta
                With DadosExibir
                    .IdentificadorPai = Nothing
                    .Identificador = c.Identificador
                    .Codigo = c.Codigo
                    .Descricao = c.Descripcion
                End With
                dadosCliente.DatosRespuesta.Add(DadosExibir)
            End If
        Next

        ucClientes.ucCliente.RegistrosSelecionados = dadosCliente
        ucClientes.ucCliente.ExibirDados(True)
    End Sub
    Private Sub LimparHelper()
        Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
        Clientes.Clear()
        Clientes.Add(objCliente)

        AtualizaDadosHelperCliente(Clientes)
    End Sub
#End Region
#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarScripts()

        'txtValorMaximo.Attributes.Add("onkeyup", String.Format("moeda(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
        txtValorMaximo.Attributes.Add("onblur", String.Format("VerificarNumeroDecimal(this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
        txtValorMaximo.Attributes.Add("onkeypress", "bloqueialetras(event,this);")
        txtValorMaximo.Attributes.Add("onKeyDown", "BloquearColar();")

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.NIVELESSALDOS
        MyBase.ValidarAcao = False
        MyBase.ValidarPemissaoAD = False

        If Request("acao") IsNot Nothing Then
            MyBase.Acao = Request("acao")
        End If

    End Sub

    Protected Overrides Sub Inicializar()
        'Session("objRespuestaImporte")
        Try
            ASPxGridView.RegisterBaseScript(Page)

            Master.HabilitarHistorico = False

            ConfigurarControle_Cliente()

            'Cabecalho1.VersionVisible = False
            If Not Page.IsPostBack Then
                Clientes = Nothing

                txtCodImporte.Text = Request.QueryString("codimporte")
                txtDescImporte.Text = Request.QueryString("descImporte")

                txtCodImporte.Enabled = False
                txtDescImporte.Enabled = False

                CarregarCanal()
                CarregarDivisa()

                If Session("ImporteMaximoEditar") IsNot Nothing Then
                    ImportesMaximo = Session("ImporteMaximoEditar")
                    If ImportesMaximo IsNot Nothing AndAlso ImportesMaximo.Count > 0 Then
                        PreencherGridImporteMaximo()
                    End If
                End If


            End If



            ' TrataFoco()
            
            ConsomeDivisa()

            ConsomeCanal()


        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Private Sub TrataFoco()

        If (Not IsPostBack) Then
            Aplicacao.Util.Utilidad.HookOnFocus(DirectCast(Me.Page, Control))
        Else
            If Request("__LASTFOCUS") IsNot Nothing AndAlso Request("__LASTFOCUS") <> String.Empty Then
                Page.SetFocus(Request("__LASTFOCUS"))
            End If
        End If

    End Sub

    Protected Overrides Sub TraduzirControles()

        lblCodigoImporte.Text = Traduzir("056_lbl_codigoImporte")
        lblCanal.Text = Traduzir("056_lbl_CanalImporte")
        lblDescricaoImporte.Text = Traduzir("056_lbl_DescricaoImporte")
        lblSubCanal.Text = Traduzir("056_lbl_SubCanalImporte")
        lblValorMaximo.Text = Traduzir("056_lbl_ValorMaximoImporte")
        lblVigente.Text = Traduzir("056_lbl_Vigente")
        lblDivisa.Text = Traduzir("056_lbl_DivisaImporte")

        If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
            btnAceptar.Text = Traduzir("btnAceptar")
        ElseIf MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
            btnAceptar.Text = Traduzir("btnModificacion")
        End If
        btnLimpar.Text = Traduzir("btnLimpiar")
        btnGrabar.Text = Traduzir("btnGrabar")

        'Grid
        ProsegurGridViewImporteMaximo.Columns(2).HeaderText = Traduzir("056_lbl_codigoImporte")
        ProsegurGridViewImporteMaximo.Columns(3).HeaderText = Traduzir("057_lbl_cliente")
        ProsegurGridViewImporteMaximo.Columns(4).HeaderText = Traduzir("056_lbl_DivisaImporte")
        ProsegurGridViewImporteMaximo.Columns(5).HeaderText = Traduzir("056_lbl_ValorMaximoImporte")
        ProsegurGridViewImporteMaximo.Columns(6).HeaderText = Traduzir("034_lbl_gdr_vigente")
        ProsegurGridViewImporteMaximo.Columns(1).HeaderText = Traduzir("034_lbl_gdr_modificar")
        
    End Sub

    Protected Overrides Sub PreRenderizar()
        Try
            If Not Page.IsPostBack Then
                ControleBotoes()
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Public Sub ControleBotoes()

        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Consulta, Aplicacao.Util.Utilidad.eAcao.Busca
                btnAceptar.Visible = False
                pnHelper.Enabled = False
                btnLimpar.Visible = False
                txtCodImporte.Enabled = False
                txtDescImporte.Enabled = False
                txtValorMaximo.Enabled = False
                ddlCanal.Enabled = False
                ddlSubCanal.Enabled = False
                ddlDivisa.Enabled = False
                chkVigenteTela.Enabled = False

                'No modo de consulta o botão modificar fica desabilitado
                Dim imgBtn As ImageButton = Nothing
                For Each row In ProsegurGridViewImporteMaximo.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        imgBtn = row.FindControl("imbModificar")
                        If imgBtn IsNot Nothing Then
                            imgBtn.Enabled = False
                        End If
                    End If
                Next
            Case Aplicacao.Util.Utilidad.eAcao.Alta
                chkVigenteTela.Checked = True
                chkVigenteTela.Enabled = True
                ' btnAceptar.Tipo = Prosegur.Web.TipoBotao.Adicionar
            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                ' btnAceptar.Tipo = Prosegur.Web.TipoBotao.Editar
                chkVigenteTela.Checked = False
        End Select

       
      
    End Sub

#End Region

#Region "[PROPRIEDADES]"

    Public Property ImporteMaximoSelecionado As String
        Get
            Return Request.QueryString("oidimporte")
        End Get

        Set(value As String)

        End Set
    End Property


    Private Property ImporteMaximoEditar() As ContractoServicio.ImporteMaximo.ImporteMaximoBase

        Get
            Return ViewState("VSImporteMaximoEditar")
        End Get

        Set(value As ContractoServicio.ImporteMaximo.ImporteMaximoBase)
            ViewState("VSImporteMaximoEditar") = value
        End Set

    End Property

    Public Property ViewStateImporteMaximoEntrada() As ImporteMaximoSimples
        Get
            Return ViewState("ImporteMaximoEntrada")
        End Get
        Set(value As ImporteMaximoSimples)
            ViewState("ImporteMaximoEntrada") = value
        End Set
    End Property

    Private Property ImportesMaximo() As ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase

        Get
            Return ViewState("ImportesMaximo")
        End Get

        Set(value As ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase)
            ViewState("ImportesMaximo") = value
        End Set

    End Property
    Private Property ImporteMaximoEditarClone() As ContractoServicio.ImporteMaximo.ImporteMaximoBase

        Get
            Return ViewState("ImporteMaximoEditarClone")
        End Get

        Set(value As ContractoServicio.ImporteMaximo.ImporteMaximoBase)
            ViewState("ImporteMaximoEditarClone") = value
        End Set

    End Property

    Private Property ListaCanal() As ContractoServicio.Utilidad.GetComboCanales.CanalColeccion

        Get
            Return ViewState("ListaCanal")
        End Get

        Set(value As ContractoServicio.Utilidad.GetComboCanales.CanalColeccion)
            ViewState("ListaCanal") = value
        End Set

    End Property

    Private Property ListaSubCanal() As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion

        Get
            Return ViewState("ListaSubCanal")
        End Get

        Set(value As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion)
            ViewState("ListaSubCanal") = value
        End Set

    End Property

    Private Property ListaDivisa() As ContractoServicio.Utilidad.GetComboDivisas.DivisaColeccion

        Get
            Return ViewState("ListaDivisa")
        End Get

        Set(value As ContractoServicio.Utilidad.GetComboDivisas.DivisaColeccion)
            ViewState("ListaDivisa") = value
        End Set

    End Property

    Public Shared Property Peticion As PeticionBusqueda
        Get
            Return HttpContext.Current.Session("Dto_Banc_objEntidade")
        End Get
        Set(value As PeticionBusqueda)
            HttpContext.Current.Session("Dto_Banc_objEntidade") = value
        End Set
    End Property

    Public Shared Property Respuesta As RespuestaBusqueda
        Get
            Return HttpContext.Current.Session("Dto_Banc_objRespuesta")
        End Get
        Set(value As RespuestaBusqueda)
            HttpContext.Current.Session("Dto_Banc_objRespuesta") = value
        End Set
    End Property

    Private Property ClienteSelecionado As ContractoServicio.Utilidad.GetComboClientes.Cliente
        Get
            Return ViewState("ClienteSelecionado")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboClientes.Cliente)
            ViewState("ClienteSelecionado") = value
        End Set
    End Property

    Private Property NroDocumentoSelecionado As String
        Get
            Return ViewState("NroDocumentoSelecionado")
        End Get
        Set(value As String)
            ViewState("NroDocumentoSelecionado") = value
        End Set
    End Property

    Private Property TipoSelecionado As String
        Get
            Return ViewState("TipoSelecionado")
        End Get
        Set(value As String)
            ViewState("TipoSelecionado") = value
        End Set
    End Property

    Private Property ObservacionesSelecionado As String
        Get
            Return ViewState("ObservacionesSelecionado")
        End Get
        Set(value As String)
            ViewState("ObservacionesSelecionado") = value
        End Set
    End Property

    Private Property DivisaSelecionado As ContractoServicio.Utilidad.GetComboDivisas.Divisa
        Get
            Return ViewState("DivisaSelecionado")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboDivisas.Divisa)
            ViewState("DivisaSelecionado") = value
        End Set
    End Property

    Private Property CanalSelecionado As ContractoServicio.Utilidad.GetComboCanales.Canal
        Get
            Return ViewState("CanalSelecionado")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboCanales.Canal)
            ViewState("CanalSelecionado") = value
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

#Region "[METODOS]"
    Private Sub PreencherGridImporteMaximo(Optional recarregar As Boolean = False)

        If ImportesMaximo IsNot Nothing AndAlso ImportesMaximo.Count > 0 Then
            Dim reposta2 As New ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase
            Dim importemaximoEditar As New ContractoServicio.ImporteMaximo.ImporteMaximoBase

            For Each importeMaximo In ImportesMaximo

                If Not String.IsNullOrEmpty(importeMaximo.OidImporteMaximo) Then
                    importemaximoEditar.OidImporteMaximo = importeMaximo.OidImporteMaximo
                End If
                importemaximoEditar.BolVigente = importeMaximo.BolVigente
                importemaximoEditar.CodigoImporte = importeMaximo.CodigoImporte
                importemaximoEditar.DescricaoImporte = importeMaximo.DescricaoImporte
                importemaximoEditar.ValorMaximo = CType(importeMaximo.ValorMaximo, Decimal).ToString("N2")

                importemaximoEditar.Cliente = importeMaximo.Cliente
                importemaximoEditar.CodigoImporte = importeMaximo.Cliente.Codigo.ToString
                importemaximoEditar.Canal = importeMaximo.Canal
                importemaximoEditar.SubCanal = importeMaximo.SubCanal

                importemaximoEditar.Divisa = importeMaximo.Divisa


                reposta2.Add(importemaximoEditar)

                importemaximoEditar = New ContractoServicio.ImporteMaximo.ImporteMaximoBase
            Next

            Dim objDt2 As DataTable

            'Ordena quando adiciona novo registro no grid, caso contrario ja vem ordenado do banco
            If (recarregar) Then
                Dim resp = reposta2.OrderBy(Function(f) f.CodigoImporte).ThenBy(Function(f) f.Divisa.Descripcion). _
                ThenBy(Function(f) f.ValorMaximo).ToList

                objDt2 = ProsegurGridViewImporteMaximo.ConvertListToDataTable(resp)
            Else
                objDt2 = ProsegurGridViewImporteMaximo.ConvertListToDataTable(reposta2)
            End If

            ' carregar controle
            ProsegurGridViewImporteMaximo.CarregaControle(objDt2)

            pnlSemRegistro.Visible = False
            Session("objRespuestaImporte") = reposta2

            LimparCampos()
        End If


    End Sub
    Private Sub ConsomeCliente()

        If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
            LimparCliente()
            'Utilizado para recuperar o cliente correto já que o serviço retorna muitos valores contendo o codigo informado
            Dim lstClientes = GetComboClientes(Clientes.FirstOrDefault().Codigo).Clientes
            ClienteSelecionado = lstClientes.FirstOrDefault(Function(c) c.Codigo = Clientes.FirstOrDefault().Codigo)
        End If
        
    End Sub
    Private Function GetComboClientes(codCliente) As ContractoServicio.Utilidad.GetComboClientes.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New ContractoServicio.Utilidad.GetComboClientes.Peticion
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        If Not String.IsNullOrEmpty(codCliente) Then
            objPeticion.Codigo = New List(Of String)
            objPeticion.Codigo.Add(codCliente)
        End If

        ' criar objeto proxy
        Dim objProxy As New Comunicacion.ProxyUtilidad

        ' chamar servicio
        Return objProxy.GetComboClientes(objPeticion)

    End Function

    Private Sub ConsomeDivisa()

        If Session("DivisaSelecionado") IsNot Nothing Then

            LimparDivisa()

            DivisaSelecionado = Session("DivisaSelecionado")

            If DivisaSelecionado.Identificador IsNot Nothing Then
                ddlDivisa.SelectedValue = DivisaSelecionado.Identificador
            End If

            Session("DivisaSelecionado") = Nothing

        End If

    End Sub

    Private Sub ConsomeCanal()

        If Session("Dto_Banc_CanalSelecionado") IsNot Nothing Then

            LimparCanal()

            CanalSelecionado = Session("Dto_Banc_CanalSelecionado")

            If CanalSelecionado.Identificador IsNot Nothing Then
                ddlCanal.SelectedValue = CanalSelecionado.Identificador
            End If

            Session("Dto_Banc_CanalSelecionado") = Nothing

        End If

    End Sub


    Private Function Validado() As Boolean

        Dim msg As String = ""

        If Clientes Is Nothing OrElse Clientes.Count = 0 Then
            msg &= String.Format(Traduzir("err_campo_obligatorio"), Traduzir("057_lbl_cliente")) & "<br/>"
        End If
        
        If ddlCanal.SelectedItem IsNot Nothing AndAlso ddlCanal.SelectedItem.Text = Traduzir("043_ddl_selecione") Then
            msg &= String.Format(Traduzir("err_campo_obligatorio"), Traduzir("056_lbl_CanalImporte")) & "<br/>"
            csvCanalObrigatorio.IsValid = False
        Else
            csvCanalObrigatorio.IsValid = True
        End If

        If ddlSubCanal.SelectedItem IsNot Nothing AndAlso ddlSubCanal.SelectedItem.Text = Traduzir("043_ddl_selecione") Then
            msg &= String.Format(Traduzir("err_campo_obligatorio"), Traduzir("056_lbl_SubCanalImporte")) & "<br/>"
            csvSubCanalObrigatorio.IsValid = False
        Else
            csvSubCanalObrigatorio.IsValid = True
        End If

        If ddlDivisa.SelectedItem IsNot Nothing AndAlso ddlDivisa.SelectedItem.Text = Traduzir("043_ddl_selecione") Then
            msg &= String.Format(Traduzir("err_campo_obligatorio"), Traduzir("056_lbl_DivisaImporte")) & "<br/>"
            csvDivisaObrigatorio.IsValid = False
        Else
            csvDivisaObrigatorio.IsValid = True
        End If

        If String.IsNullOrEmpty(txtValorMaximo.Text) Then
            msg &= String.Format(Traduzir("err_campo_obligatorio"), Traduzir("056_lbl_ValorMaximoImporte")) & "<br/>"
            csvValorMaximoObrigatorio.IsValid = False
        Else
            csvValorMaximoObrigatorio.IsValid = True
        End If


        If Not String.IsNullOrEmpty(msg) Then
            If Not Master.ControleErro.VerificaErro(100, "", msg) Then
                MyBase.MostraMensagem(msg)
                Exit Function
            End If
        End If

        Return True

    End Function

    Private Sub LimparDivisa()
        DivisaSelecionado = Nothing
        ddlDivisa.SelectedValue = String.Empty
    End Sub

    Private Sub LimparCanal()
        CanalSelecionado = Nothing
        ddlCanal.SelectedValue = String.Empty
    End Sub

    Private Sub LimparCliente()
        ClienteSelecionado = Nothing
       
    End Sub

    Public Sub SetClienteSelecionadoPopUp()
        Session("Dto_Banc_objBanco") = ClienteSelecionado
    End Sub

    Public Sub CarregarDivisa()
        
        Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboDivisas.Respuesta
        Dim objDivisa As IAC.ContractoServicio.Utilidad.GetComboDivisas.Divisa
        Dim objDivisaCol As IAC.ContractoServicio.Utilidad.GetComboDivisas.DivisaColeccion = Nothing

        objRespuesta = objProxyUtilidad.GetComboDivisas()

        If objRespuesta.Divisas Is Nothing Then
            ddlDivisa.Items.Clear()
            ddlDivisa.Items.Add(New ListItem(Traduzir("043_ddl_selecione"), String.Empty))
            Exit Sub
        End If

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            ddlDivisa.Items.Clear()
            ddlDivisa.Items.Add(New ListItem(Traduzir("043_ddl_selecione"), String.Empty))
            Exit Sub
        End If

        objDivisaCol = New IAC.ContractoServicio.Utilidad.GetComboDivisas.DivisaColeccion
        For Each item In objRespuesta.Divisas
            objDivisa = New ContractoServicio.Utilidad.GetComboDivisas.Divisa
            objDivisaCol.Add(item)
        Next

        ListaDivisa = New ContractoServicio.Utilidad.GetComboDivisas.DivisaColeccion
        ListaDivisa.AddRange(objDivisaCol.OrderBy(Function(s) s.Descripcion))

        ddlDivisa.AppendDataBoundItems = True
        ddlDivisa.Items.Clear()
        ddlDivisa.Items.Add(New ListItem(Traduzir("043_ddl_selecione"), String.Empty))
        ddlDivisa.DataTextField = "Descripcion"
        ddlDivisa.DataValueField = "Identificador"
        ddlDivisa.DataSource = ListaDivisa
        ddlDivisa.DataBind()

    End Sub

    Public Sub CarregarCanal()
        Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboCanales.Respuesta
        Dim objCanal As IAC.ContractoServicio.Utilidad.GetComboCanales.Canal
        Dim objCanalCol As IAC.ContractoServicio.Utilidad.GetComboCanales.CanalColeccion = Nothing

        objRespuesta = objProxyUtilidad.GetComboCanales()

        If objRespuesta.Canales Is Nothing Then
            ddlCanal.Items.Clear()
            ddlCanal.Items.Add(New ListItem(Traduzir("043_ddl_selecione"), String.Empty))
            Exit Sub
        End If

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            ddlCanal.Items.Clear()
            ddlCanal.Items.Add(New ListItem(Traduzir("043_ddl_selecione"), String.Empty))
            Exit Sub
        End If

        objCanalCol = New IAC.ContractoServicio.Utilidad.GetComboCanales.CanalColeccion
        For Each item In objRespuesta.Canales
            objCanal = New ContractoServicio.Utilidad.GetComboCanales.Canal
            objCanalCol.Add(item)
        Next

        ListaCanal = New ContractoServicio.Utilidad.GetComboCanales.CanalColeccion
        ListaCanal.AddRange(objCanalCol.OrderBy(Function(s) s.Descripcion))

        ddlCanal.AppendDataBoundItems = True
        ddlCanal.Items.Clear()
        ddlCanal.Items.Add(New ListItem(Traduzir("043_ddl_selecione"), String.Empty))
        ddlCanal.Items.Add(New ListItem(Traduzir("gen_opcion_todos"), "Todos"))
        ddlCanal.DataTextField = "Descripcion"
        ddlCanal.DataValueField = "Codigo"
        ddlCanal.DataSource = ListaCanal
        ddlCanal.DataBind()

    End Sub

    Public Sub CarregarSubCanal()

        If ddlCanal.SelectedItem IsNot Nothing AndAlso ddlCanal.SelectedItem.Text <> Traduzir("043_ddl_selecione") AndAlso ddlCanal.SelectedItem IsNot Nothing AndAlso ddlCanal.SelectedItem.Text <> "Todos" Then

            Dim listaCodigoCanal As New List(Of String)
            Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
            Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Respuesta
            Dim objSubCanal As IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal
            Dim objSubCanalCol As IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion = Nothing
            Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Peticion

            listaCodigoCanal.Add(ddlCanal.SelectedValue)
            objPeticion.Codigo = listaCodigoCanal

            objRespuesta = objProxyUtilidad.GetComboSubcanalesByCanal(objPeticion)

            If objRespuesta.Canales.Count > 0 AndAlso objRespuesta.Canales(0).SubCanales Is Nothing Then
                ddlSubCanal.Items.Clear()
                ddlSubCanal.Enabled = True
                ddlSubCanal.Items.Add(New ListItem(Traduzir("043_ddl_selecione"), String.Empty))
                Exit Sub
            ElseIf ddlCanal.SelectedValue = Traduzir("gen_opcion_todos") Then
                ddlSubCanal.Enabled = False
                Exit Sub
            End If

            If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                ddlSubCanal.Items.Clear()
                ddlSubCanal.Items.Add(New ListItem(Traduzir("043_ddl_selecione"), String.Empty))
                Exit Sub
            End If

            objSubCanalCol = New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion
            For Each item In objRespuesta.Canales(0).SubCanales
                objSubCanal = New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal
                objSubCanalCol.Add(item)
            Next

            ListaSubCanal = New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion
            ListaSubCanal.AddRange(objSubCanalCol.OrderBy(Function(s) s.Descripcion))

            ddlSubCanal.AppendDataBoundItems = True
            ddlSubCanal.Items.Clear()
            ddlSubCanal.Items.Add(New ListItem(Traduzir("043_ddl_selecione"), String.Empty))
            ddlSubCanal.DataTextField = "Descripcion"
            ddlSubCanal.DataValueField = "oidsubCanal"
            ddlSubCanal.DataSource = ListaSubCanal
            ddlSubCanal.DataBind()

            updatePanelSubCanal.Update()
        Else
            ddlSubCanal.Items.Add(New ListItem(Traduzir("043_ddl_selecione"), String.Empty))
            ddlSubCanal.Enabled = False
            updatePanelSubCanal.Update()
        End If
    End Sub

    Private Sub LimparCampos()

        LimparCliente()
        LimparHelper()

        chkVigenteTela.Checked = True
        ddlCanal.SelectedIndex = 0
        ddlSubCanal.SelectedIndex = 0
        ddlSubCanal.Items.Clear()
        ddlDivisa.SelectedIndex = 0
        txtValorMaximo.Text = String.Empty
        txtCodImporte.Focus()
        btnAceptar.Text = Traduzir("btnAceptar")

        updatePanelSubCanal.Update()
        updatePanelCanal.Update()
        updatePanelDivisa.Update()
        UpdatePanelValor.Update()
        upChkVigenteTela.Update()
        upBotaoAceptar.Update()

    End Sub
#End Region

#Region "[EVENTOS]"

    Private Sub btnGrabar_Click(sender As Object, e As System.EventArgs) Handles btnGrabar.Click

        Try
            ' ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "BusquedaNivelOk", "FecharAtualizarPaginaPai();", True)
            Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
            ' fechar janela
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ImportMaximoOK", jsScript, True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
 
    Private Sub btnAceptar_click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click
        Try
            If Validado() Then
                Try
                    Dim oidImporteMaximo As String = If(ImporteMaximoEditar IsNot Nothing, ImporteMaximoEditar.OidImporteMaximo, "")


                    If btnAceptar.Text.Equals(Traduzir("btnAceptar")) Then
                        'Recarrega grid com dados da viewstate
                        Dim objimportemaximo As New ContractoServicio.ImporteMaximo.ImporteMaximoBase
                        objimportemaximo.BolVigente = chkVigenteTela.Checked
                        objimportemaximo.CodigoImporte = txtCodImporte.Text
                        objimportemaximo.DescricaoImporte = txtDescImporte.Text
                        objimportemaximo.ValorMaximo = txtValorMaximo.Text
                        If ClienteSelecionado IsNot Nothing Then
                            objimportemaximo.Cliente = New ContractoServicio.Utilidad.GetComboClientes.Cliente With {.OidCliente = ClienteSelecionado.OidCliente, .Codigo = ClienteSelecionado.Codigo, .Descripcion = ClienteSelecionado.Descripcion}
                        End If

                        If ddlCanal.SelectedItem.Text <> "Todos" Then
                            objimportemaximo.Canal = New ContractoServicio.Utilidad.GetComboCanales.Canal
                            If ListaCanal IsNot Nothing Then
                                Dim objCanalSelec = ListaCanal.Find(Function(a) a.Codigo = ddlCanal.SelectedValue)
                                If objCanalSelec IsNot Nothing Then
                                    objimportemaximo.Canal = New ContractoServicio.Utilidad.GetComboCanales.Canal With {.Identificador = objCanalSelec.Identificador, .Codigo = objCanalSelec.Codigo, .Descripcion = objCanalSelec.Descripcion}
                                End If
                            End If

                            objimportemaximo.SubCanal = New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal
                            If ListaSubCanal IsNot Nothing Then
                                Dim objSubCanalSelec = ListaSubCanal.Find(Function(a) a.OidSubCanal = ddlSubCanal.SelectedValue)
                                If objSubCanalSelec IsNot Nothing Then
                                    objimportemaximo.SubCanal = New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal With {.OidSubCanal = objSubCanalSelec.OidSubCanal, .Codigo = objSubCanalSelec.Codigo, .Descripcion = objSubCanalSelec.Descripcion}
                                End If
                            Else
                                Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
                                Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Respuesta
                                Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Peticion
                                Dim listaCodigoCanal As New List(Of String)
                                listaCodigoCanal.Add(ddlCanal.SelectedValue)
                                objPeticion.Codigo = listaCodigoCanal

                                objRespuesta = objProxyUtilidad.GetComboSubcanalesByCanal(objPeticion)
                                If objRespuesta.Canales.Count > 0 AndAlso objRespuesta.Canales(0).SubCanales IsNot Nothing AndAlso objRespuesta.Canales(0).SubCanales.Count > 0 Then
                                    objimportemaximo.SubCanal = New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal With {.OidSubCanal = objRespuesta.Canales(0).SubCanales.Find(Function(a) a.OidSubCanal = ddlSubCanal.SelectedValue).OidSubCanal}
                                End If

                            End If
                        Else
                            objimportemaximo.Canal = Nothing
                            objimportemaximo.SubCanal = Nothing
                        End If


                        objimportemaximo.Divisa = New ContractoServicio.Utilidad.GetComboDivisas.Divisa
                        If ListaDivisa IsNot Nothing Then
                            Dim objDivisaSelec = ListaDivisa.Find(Function(a) a.Identificador = ddlDivisa.SelectedValue)
                            If objDivisaSelec IsNot Nothing Then
                                objimportemaximo.Divisa = New ContractoServicio.Utilidad.GetComboDivisas.Divisa With {.Identificador = objDivisaSelec.Identificador, .CodigoIso = objDivisaSelec.CodigoIso, .Descripcion = objDivisaSelec.Descripcion}
                            End If

                        End If

                        If Me.ImportesMaximo Is Nothing Then
                            Me.ImportesMaximo = New ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase
                        End If

                        If Me.ImportesMaximo.Count > 0 Then
                            Dim msg As String = ""
                            Dim objEncontrado As Boolean = False
                            For Each imp In Me.ImportesMaximo
                                If imp.Cliente.OidCliente <> objimportemaximo.Cliente.OidCliente Then
                                    Continue For
                                End If

                                If imp.Divisa.CodigoIso <> objimportemaximo.Divisa.CodigoIso Then
                                    Continue For
                                End If

                                If imp.Canal IsNot Nothing AndAlso objimportemaximo.Canal IsNot Nothing Then
                                    If imp.Canal.Codigo <> objimportemaximo.Canal.Codigo Then
                                        Continue For
                                    End If
                                ElseIf (imp.Canal Is Nothing AndAlso objimportemaximo.Canal IsNot Nothing) OrElse (imp.Canal IsNot Nothing AndAlso objimportemaximo.Canal Is Nothing) Then
                                    Continue For
                                End If

                                If imp.SubCanal IsNot Nothing AndAlso objimportemaximo.SubCanal IsNot Nothing Then
                                    If imp.SubCanal.Codigo <> objimportemaximo.SubCanal.Codigo Then
                                        Continue For
                                    End If
                                ElseIf (imp.SubCanal Is Nothing AndAlso objimportemaximo.SubCanal IsNot Nothing) OrElse (imp.SubCanal IsNot Nothing AndAlso objimportemaximo.SubCanal Is Nothing) Then
                                    Continue For
                                End If

                                objEncontrado = True
                                Exit For
                            Next

                            If Not objEncontrado Then
                                Me.ImportesMaximo.Add(objimportemaximo)
                            Else
                                'exibir msg
                                msg &= String.Format("O item já foi adicionado anteriormente!") & "<br/>"
                                If Not String.IsNullOrEmpty(msg) Then
                                    If Not Master.ControleErro.VerificaErro(100, "", msg) Then
                                        MyBase.MostraMensagem(msg)
                                        Exit Sub
                                    End If
                                End If
                            End If
                        Else
                            Me.ImportesMaximo.Add(objimportemaximo)
                        End If
                        'Recarrega grid com dados da viewstate
                        PreencherGridImporteMaximo(True)

                    ElseIf btnAceptar.Text.Equals(Traduzir("btnModificacion")) Then
                        ImporteMaximoEditar.BolVigente = chkVigenteTela.Checked
                        ImporteMaximoEditar.CodigoImporte = txtCodImporte.Text
                        ImporteMaximoEditar.DescricaoImporte = txtDescImporte.Text
                        ImporteMaximoEditar.ValorMaximo = txtValorMaximo.Text

                        If ClienteSelecionado IsNot Nothing Then
                            ImporteMaximoEditar.Cliente = New ContractoServicio.Utilidad.GetComboClientes.Cliente With {.OidCliente = ClienteSelecionado.OidCliente, .Codigo = ClienteSelecionado.Codigo, .Descripcion = ClienteSelecionado.Descripcion}
                        End If

                        If ddlCanal.SelectedItem.Text <> "Todos" Then
                            ImporteMaximoEditar.Canal = New ContractoServicio.Utilidad.GetComboCanales.Canal
                            If ListaCanal IsNot Nothing Then
                                Dim objCanalSelec = ListaCanal.Find(Function(a) a.Codigo = ddlCanal.SelectedValue)
                                If objCanalSelec IsNot Nothing Then
                                    ImporteMaximoEditar.Canal = New ContractoServicio.Utilidad.GetComboCanales.Canal With {.Identificador = objCanalSelec.Identificador, .Codigo = objCanalSelec.Codigo, .Descripcion = objCanalSelec.Descripcion}
                                End If
                            End If

                            ImporteMaximoEditar.SubCanal = New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal
                            If ListaSubCanal IsNot Nothing Then
                                Dim objSubCanalSelec = ListaSubCanal.Find(Function(a) a.OidSubCanal = ddlSubCanal.SelectedValue)
                                If objSubCanalSelec IsNot Nothing Then
                                    ImporteMaximoEditar.SubCanal = New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal With {.OidSubCanal = objSubCanalSelec.OidSubCanal, .Codigo = objSubCanalSelec.Codigo, .Descripcion = objSubCanalSelec.Descripcion}
                                End If
                            Else
                                Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
                                Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Respuesta
                                Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Peticion
                                Dim listaCodigoCanal As New List(Of String)
                                listaCodigoCanal.Add(ddlCanal.SelectedValue)
                                objPeticion.Codigo = listaCodigoCanal

                                objRespuesta = objProxyUtilidad.GetComboSubcanalesByCanal(objPeticion)
                                If objRespuesta.Canales.Count > 0 AndAlso objRespuesta.Canales(0).SubCanales IsNot Nothing AndAlso objRespuesta.Canales(0).SubCanales.Count > 0 Then
                                    ImporteMaximoEditar.SubCanal = New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal With {.OidSubCanal = objRespuesta.Canales(0).SubCanales.Find(Function(a) a.OidSubCanal = ddlSubCanal.SelectedValue).OidSubCanal, .Codigo = objRespuesta.Canales(0).SubCanales.Find(Function(a) a.OidSubCanal = ddlSubCanal.SelectedValue).Codigo, .Descripcion = objRespuesta.Canales(0).SubCanales.Find(Function(a) a.OidSubCanal = ddlSubCanal.SelectedValue).Descripcion}
                                End If

                            End If
                        Else
                            ImporteMaximoEditar.Canal = Nothing
                            ImporteMaximoEditar.SubCanal = Nothing
                        End If



                        ImporteMaximoEditar.Divisa = New ContractoServicio.Utilidad.GetComboDivisas.Divisa
                        If ListaDivisa IsNot Nothing Then
                            Dim objDivisaSelec = ListaDivisa.Find(Function(a) a.Identificador = ddlDivisa.SelectedValue)
                            If objDivisaSelec IsNot Nothing Then
                                ImporteMaximoEditar.Divisa = New ContractoServicio.Utilidad.GetComboDivisas.Divisa With {.Identificador = objDivisaSelec.Identificador, .CodigoIso = objDivisaSelec.CodigoIso, .Descripcion = objDivisaSelec.Descripcion}
                            End If
                        End If

                        Dim removido = Me.ImportesMaximo.FirstOrDefault(Function(f) f.OidImporteMaximo = ImporteMaximoEditarClone.OidImporteMaximo)
                        If ImporteMaximoEditarClone.OidImporteMaximo Is Nothing Then
                            removido = Me.ImportesMaximo.FirstOrDefault(Function(f) f.OidImporteMaximo = ImporteMaximoEditarClone.OidImporteMaximo)
                        End If

                        Dim indice As Integer = Me.ImportesMaximo.IndexOf(removido)
                        If indice >= 0 Then
                            Me.ImportesMaximo.Remove(removido)
                        End If

                        Me.ImportesMaximo.Insert(indice, ImporteMaximoEditar)

                        ImporteMaximoEditarClone = Nothing


                        PreencherGridImporteMaximo(True)
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta
                    End If



                Catch ex As Exception
                    MyBase.MostraMensagemExcecao(ex)
                End Try
                upGridImporteMAximo.Update()
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#Region "[EVENTOS GRIDVIEW]"
    Protected Sub ProsegurGridViewImporteMaximo_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridViewImporteMaximo.EPager_SetCss
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
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub ProsegurGridViewImporteMaximo_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewImporteMaximo.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha
                'If Not e.Row.DataItem("Cliente") Is DBNull.Value AndAlso e.Row.DataItem("Cliente").count > NumeroMaximoLinha Then
                e.Row.Cells(0).Text = e.Row.DataItem("oidimportemaximo").ToString
                e.Row.Cells(2).Text = DirectCast(e.Row.DataItem("Cliente"), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente).Codigo.ToString
                e.Row.Cells(3).Text = DirectCast(e.Row.DataItem("Cliente"), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente).Descripcion.ToString
                e.Row.Cells(4).Text = DirectCast(e.Row.DataItem("Divisa"), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboDivisas.Divisa).Descripcion.ToString
                e.Row.Cells(5).Text = CType(e.Row.DataItem("ValorMaximo").ToString, Decimal).ToString("N2")

                Dim chk As System.Web.UI.WebControls.CheckBox
                chk = e.Row.Cells(6).Controls(1)
                If e.Row.DataItem("BolVigente") Then
                    chk.Checked = True
                Else
                    chk.Checked = False
                End If

                e.Row.Cells(7).Text = If(e.Row.DataItem("Canal").GetType().Name.Equals("DBNull"), Traduzir("gen_opcion_todos"), DirectCast(e.Row.DataItem("Canal"), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboCanales.Canal).Descripcion.ToString)
                e.Row.Cells(8).Text = If(e.Row.DataItem("SubCanal").GetType().Name.Equals("DBNull"), Traduzir("gen_opcion_todos"), DirectCast(e.Row.DataItem("SubCanal"), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal).Descripcion)

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub ProsegurGridViewImporteMaximo_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridViewImporteMaximo.EPreencheDados
        Try

            Dim objDT As DataTable

            Dim reposta2 As ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase = Nothing
            Dim importemaximoEditar As New ContractoServicio.ImporteMaximo.ImporteMaximoBase

            For Each importeMaximo In ImportesMaximo

                If Not String.IsNullOrEmpty(importeMaximo.OidImporteMaximo) Then
                    importemaximoEditar.OidImporteMaximo = importeMaximo.OidImporteMaximo
                End If
                importemaximoEditar.BolVigente = importeMaximo.BolVigente
                importemaximoEditar.CodigoImporte = importeMaximo.CodigoImporte
                importemaximoEditar.DescricaoImporte = importeMaximo.DescricaoImporte
                importemaximoEditar.ValorMaximo = CType(importeMaximo.ValorMaximo, Decimal).ToString("N2")

                importemaximoEditar.Cliente = importeMaximo.Cliente
                importemaximoEditar.CodigoImporte = importeMaximo.Cliente.Codigo.ToString
                importemaximoEditar.Canal = importeMaximo.Canal
                importemaximoEditar.SubCanal = importeMaximo.SubCanal

                importemaximoEditar.Divisa = importeMaximo.Divisa

                If reposta2 Is Nothing Then
                    reposta2 = New ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase
                End If

                reposta2.Add(importemaximoEditar)

                importemaximoEditar = New ContractoServicio.ImporteMaximo.ImporteMaximoBase
            Next

            If reposta2 IsNot Nothing Then

                pnlSemRegistro.Visible = False
                
                ' converter objeto para datatable(para efetuar a ordenação)
                objDT = ProsegurGridViewImporteMaximo.ConvertListToDataTable(reposta2)


                ProsegurGridViewImporteMaximo.ControleDataSource = objDT
                ProsegurGridViewImporteMaximo.DataBind()

            Else

                'Limpa a consulta
                ProsegurGridViewImporteMaximo.DataSource = Nothing
                ProsegurGridViewImporteMaximo.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
                pnlSemRegistro.Visible = True


            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub ProsegurGridViewImporteMaximo_SelectedIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles ProsegurGridViewImporteMaximo.SelectedIndexChanging
        Try
            Dim linhaSelecionada As Integer = e.NewSelectedIndex
            Dim gridrow = ProsegurGridViewImporteMaximo.Rows(linhaSelecionada)
            Dim oidImporteMaximo = gridrow.Cells(0).Text

            ImporteMaximoEditar = ImportesMaximo.FirstOrDefault(Function(f) f.OidImporteMaximo = oidImporteMaximo)
            ImporteMaximoEditarClone = ImporteMaximoEditar

            'Carregar o Cliente
            Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
            objCliente.Identificador = ImporteMaximoEditar.Cliente.Codigo.ToString
            objCliente.Codigo = ImporteMaximoEditar.Cliente.Codigo.ToString
            objCliente.Descripcion = ImporteMaximoEditar.Cliente.Descripcion.ToString
            Clientes.Clear()
            Clientes.Add(objCliente)

            AtualizaDadosHelperCliente(Clientes)

          
            If ImporteMaximoEditar.BolVigente Then
                chkVigenteTela.Checked = True
            Else
                chkVigenteTela.Checked = False
            End If


            ddlCanal.SelectedValue = If(ImporteMaximoEditar.Canal IsNot Nothing, ImporteMaximoEditar.Canal.Codigo, "Todos")

            If ImporteMaximoEditar.SubCanal IsNot Nothing Then
                ddlSubCanal.Enabled = True
                ddlSubCanal.Items.Clear()
                ddlSubCanal.Items.Add(New ListItem(If(ImporteMaximoEditar.SubCanal.Descripcion IsNot Nothing, ImporteMaximoEditar.SubCanal.Descripcion.ToString, String.Empty), ImporteMaximoEditar.SubCanal.OidSubCanal.ToString))
            Else
                ddlSubCanal.Items.Clear()
                ddlSubCanal.Items.Add(New ListItem(Traduzir("gen_opcion_todos"), "Todos"))
                ddlSubCanal.Enabled = False
                updatePanelSubCanal.Update()
            End If

            ddlDivisa.SelectedValue = ImporteMaximoEditar.Divisa.Identificador
            txtValorMaximo.Text = CType(ImporteMaximoEditar.ValorMaximo, Decimal).ToString("N2")


            btnAceptar.Text = Traduzir("btnModificacion")
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion

            ' UpdatePanelCliente.Update()
            updatePanelCanal.Update()
            updatePanelSubCanal.Update()
            updatePanelDivisa.Update()
            UpdatePanelValor.Update()
            upChkVigenteTela.Update()
            upBotaoAceptar.Update()

            MyBase.TraduzirControles()


        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub



#End Region
    
    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click
        Try
            LimparCampos()
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub TamanhoLinhas()
        ProsegurGridViewImporteMaximo.RowStyle.Height = 20
    End Sub

   
    Protected Sub ddlCanal_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        If ddlCanal.SelectedItem.Text = "Todos" Then
            ddlSubCanal.Items.Clear()
            ddlSubCanal.Items.Add(New ListItem(Traduzir("gen_opcion_todos"), "Todos"))
            ddlSubCanal.Enabled = False
            updatePanelSubCanal.Update()
        Else
            ddlSubCanal.Enabled = True
            ddlSubCanal.Items.Clear()
            updatePanelSubCanal.Update()
            CarregarSubCanal()
        End If

    End Sub
#End Region

#Region "[PROPRIEDADES]"

    <Serializable()> _
    Public Class PeticionBusqueda

        'REMOVER
        Public Property CodGenesis As String
        Public Property DesGenesis As String
        Public Property OidGenesis As String
        'REMOVER

        Public Property Identificador As String

    End Class

    <Serializable()> _
    Public Class RespuestaBusqueda

        Public Property Peticion As PeticionBusqueda

        Public Property Cliente As ContractoServicio.Utilidad.GetComboClientes.Cliente
        Public Property CodigoImporte As String
        Public Property DescricaoImporte As String
        Public Property ValorMaximo As String
        Public Property BolVigente As Boolean
        Public Property Canal As ContractoServicio.Utilidad.GetComboCanales.Canal
        Public Property Subcanal As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal
        Public Property Divisa As ContractoServicio.Utilidad.GetComboDivisas.Divisa

    End Class

#End Region
    <Serializable()> _
    Public Class ImporteMaximoSimples

        Public Property CodTablaGenesis As String

        Public Property DesTablaGenesis As String

        Public Property OidTablaGenesis As String

        Public Property ImportesMaximo As ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase

    End Class

End Class

