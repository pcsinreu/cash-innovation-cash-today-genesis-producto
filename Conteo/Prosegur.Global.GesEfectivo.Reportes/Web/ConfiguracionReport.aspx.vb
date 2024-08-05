Imports System.Collections.ObjectModel
Imports System.Diagnostics.Eventing.Reader
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Report
Imports IacUtilidad = Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad
Imports Prosegur.Genesis.Report.Constantes
Imports System.IO
Imports System.IO.Compression
Imports System.Security.AccessControl
Imports System.Security.Permissions
Imports DevExpress.Web.ASPxGridView
Imports Ionic.Zip
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Helper
Imports Prosegur.Genesis.Comunicacion

Public Class ConfiguracionReport
    Inherits Base
    Dim Valida As New List(Of String)


    Private CantidadCaracteresTextBox As Integer = 45

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Armazena os clientes encontrados na busca.
    ''' </summary> 
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Private Property Clientes() As IacUtilidad.GetComboClientes.ClienteColeccion
        Get

            If Session("ClientesSelecionados") IsNot Nothing Then
                ' se for exibir clientes, guarda lista na viewstate no primeiro acesso a propriedade
                ViewState("VSClientesSelecionados") = Session("ClientesSelecionados")
                Session("ClientesSelecionados") = Nothing
            End If

            Return ViewState("VSClientesSelecionados")
        End Get
        Set(value As IacUtilidad.GetComboClientes.ClienteColeccion)
            ViewState("VSClientesSelecionados") = value
        End Set
    End Property

    Private Property Parametros As List(Of Prosegur.Genesis.Report.Parametro)
        Get
            Dim param As New List(Of Prosegur.Genesis.Report.Parametro)
            If Not ViewState("Parametros") Is Nothing Then
                param = CType(ViewState("Parametros"), List(Of Prosegur.Genesis.Report.Parametro))
            End If

            Return param
        End Get
        Set(value As List(Of Prosegur.Genesis.Report.Parametro))
            ViewState("Parametros") = value
        End Set
    End Property

    Private Property IdentificadorConfiguracion As String
        Get
            Return ViewState("IdentificadorConfiguracion")
        End Get
        Set(value As String)
            ViewState("IdentificadorConfiguracion") = value
        End Set
    End Property

    Private Property lstDesabilitar As List(Of String)
        Get
            If ViewState("lstDesabilitar") Is Nothing Then
                Return New List(Of String)
            Else
                Return ViewState("lstDesabilitar")
            End If
        End Get
        Set(value As List(Of String))
            ViewState("lstDesabilitar") = value
        End Set
    End Property

    Private Property ConfiguracionesGenerales As List(Of IAC.ContractoServicio.Configuracion.General.ConfiguracionGeneral)
        Get
            Return ViewState("ConfiguracionesGenerales")
        End Get
        Set(value As List(Of IAC.ContractoServicio.Configuracion.General.ConfiguracionGeneral))
            ViewState("ConfiguracionesGenerales") = value
        End Set
    End Property

    Public ReadOnly Property reportesSelecionados() As List(Of String)
        Get
            If String.IsNullOrEmpty(hdReportes.Value) Then
                Return New List(Of String)
            Else
                Dim separador As String() = {"|"}
                Dim arrIdentificador As String() = hdReportes.Value.Split(separador, StringSplitOptions.RemoveEmptyEntries)

                If arrIdentificador IsNot Nothing AndAlso arrIdentificador.Count > 0 Then
                    Return arrIdentificador.ToList()
                End If
            End If

            Return DirectCast(ViewState("reportesConfgSelecionados"), List(Of String))
        End Get
    End Property

    Private Property DatosControles As IAC.Integracion.ContractoServicio.Reportes.ParametroReporteColeccion
        Get
            If Session("DatosControles") Is Nothing Then
                Return Nothing
            Else
                Return Session("DatosControles")
            End If
        End Get
        Set(value As IAC.Integracion.ContractoServicio.Reportes.ParametroReporteColeccion)
            Session("DatosControles") = value
        End Set
    End Property

#End Region

    Private Sub ConfigurarControles()

        Dim _ValidaAcesso = New ValidacaoAcesso(InformacionUsuario)

        If Not _ValidaAcesso.ValidarAcaoPagina(Enumeradores.eTelas.REPORTES, _
                                           Enumeradores.eAcoesTela._MODIFICAR) Then

            btnGrabar.Visible = False
            txtDescripcion.ReadOnly = True

        End If

    End Sub

    Protected Overrides Sub PreRenderizar()
        Try

            MyBase.PreRenderizar()

            DesabilitaRelatorios()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Primeiro metodo chamado quando inicia a pagina
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 20/07/2009 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            If Not IsPostBack Then
                'Recupera os relatórios e os parametros
                If MyBase.ParametrosReporte Is Nothing Then
                    MyBase.ParametrosReporte = Util.RecuperarParametrosRelatorios()
                End If
            End If

            If MyBase.objGerarReport Is Nothing Then MyBase.objGerarReport = New Prosegur.Genesis.Report.Gerar

            If Not Page.IsPostBack Then
                ' Carrega o cambo de relatório
                PopularRelatorios()
                If Acao = Enumeradores.eAcao.Modificacion AndAlso Session("IdentificadorConfiguracion") IsNot Nothing Then
                    IdentificadorConfiguracion = Session("IdentificadorConfiguracion")
                    Session("IdentificadorConfiguracion") = Nothing
                    If Not String.IsNullOrEmpty(IdentificadorConfiguracion) Then

                        Dim peticion As New IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportesDetail.Peticion
                        Dim respuesta As IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportesDetail.Respuesta = Nothing
                        Dim objProxy As New Comunicacion.ProxyIacIntegracion

                        Dim erros As New List(Of String)
                        peticion.IdentificadoresConfiguracion = New List(Of String)
                        peticion.IdentificadoresConfiguracion.Add(IdentificadorConfiguracion)
                        respuesta = objProxy.GetConfiguracionesReportesDetail(peticion)
                        Dim msgErro As String = String.Empty
                        If Master.ControleErro.VerificaErro2(respuesta.CodigoError, _
                                                   ContractoServ.Login.ResultadoOperacionLoginLocal.Error, msgErro, _
                                                   respuesta.MensajeError) Then

                            If respuesta.ConfiguracionesReportes IsNot Nothing AndAlso respuesta.ConfiguracionesReportes.Count() > 0 Then

                                Dim oidsReportes = From p In respuesta.ConfiguracionesReportes(0).Reportes
                                                   Select p.IdentificadorConfiguracionGeneral

                                If ConfiguracionesGenerales IsNot Nothing AndAlso ConfiguracionesGenerales.Count > 0 Then

                                    ConfiguracionSelecionados = (From cg In ConfiguracionesGenerales Where oidsReportes.Contains(cg.OIDConfiguracionGeneral)).ToList()

                                    For Each a In ConfiguracionSelecionados
                                        hdReportes.Value &= a.OIDConfiguracionGeneral & "|"
                                    Next

                                End If
                                txtDescripcion.Text = respuesta.ConfiguracionesReportes.First.DesConfiguracion
                                txtNombreArchivo.Text = respuesta.ConfiguracionesReportes.First.NombreArchivo
                                txtRuta.Text = respuesta.ConfiguracionesReportes.First.DesRuta

                                Me.RecuperarParametros()
                                Me.DesabilitaRelatorios()
                                Me.CriarControles(respuesta.ConfiguracionesReportes(0))
                            Else
                                MyBase.MostraMensagem(msgErro)
                            End If

                        End If

                    End If

                End If
                gvReportes.DataBind()
            Else
                'Verifica se é para recriar os controles
                If Not Request.Params("__EVENTTARGET").Contains("chbSelecionar") AndAlso Not Request.Params("__EVENTTARGET").Contains("btnCliente") Then
                    Me.CriarControles()
                    If Request.Params("__EVENTTARGET").IndexOf("textBox_") > -1 Then
                        Dim control = tabelaCampos.FindControl(Request.Params("__EVENTTARGET").Replace("textBox_", String.Empty))
                        If Not control Is Nothing Then
                            Dim txt As TextBox = CType(control, TextBox)
                            txt.Text = Request.Params("__EVENTARGUMENT")
                            Me.PostBack(control, Nothing)
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Me.DatosControles = Nothing
            Session("SubClientesSeleccionados") = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Popula a combo de relatorios
    ''' </summary>
    ''' <remarks></remarks>
    Private Function getConfiguracionesReportes() As List(Of IAC.ContractoServicio.Configuracion.General.ConfiguracionGeneral)

        Dim listaConfiguraciones As New List(Of IAC.ContractoServicio.Configuracion.General.ConfiguracionGeneral)
        Dim listaRetorno As New List(Of IAC.ContractoServicio.Configuracion.General.ConfiguracionGeneral)
        Dim respuesta As IAC.ContractoServicio.Configuracion.General.Respuesta = Nothing
        Dim objProxy As New Comunicacion.ProxyConfiguracionGeneral
        respuesta = objProxy.GetConfiguracionGeneralReportes()
        If respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
            listaConfiguraciones = respuesta.ConfiguracionGeneralColeccion

            Dim objReport As New Prosegur.Genesis.Report.Gerar()
            objReport.Autenticar(False)

            Dim Relatorios As Prosegur.Genesis.Report.RS2010.CatalogItem() = objReport.ListaCatalogItem(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CarpetaReportes"))
            If Relatorios IsNot Nothing AndAlso Relatorios.Count > 0 Then
                'Verifica os relatórios já foram configurados
                ' se sim então recupera as configuraçães
                'var result = db.Parts.Where(p => query.All(q => p.partName.Contains(q)));
                Dim nomesRelatorios As List(Of String) = (From r In Relatorios Select r.Name).ToList
                For Each config In listaConfiguraciones.Where(Function(r) nomesRelatorios.Contains(r.DesReporte))
                    Dim configLocal = config
                    config.FormatoArchivo = Util.TraduzirFormatoArchivo(config.FormatoArchivo)
                    config.Path = (Relatorios.Where(Function(r) r.Name = configLocal.DesReporte)).FirstOrDefault().Path
                    listaRetorno.Add(config)
                Next
            End If
        End If

        Return listaRetorno

    End Function

    ''' <summary>
    ''' Preenche o gridview Reportes
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 09/07/2013 Criado
    ''' </history>
    Private Sub PreencherGridViewReportes(sender As Object, configuraciones As List(Of IAC.ContractoServicio.Configuracion.General.ConfiguracionGeneral))
        Try
            If configuraciones IsNot Nothing AndAlso configuraciones.Count > 0 Then

                Dim objDt As DataTable = Util.ConverterListParaDataTable(configuraciones)


                objDt.DefaultView.Sort = " DesReporte ASC"

                gvReportes.DataSource = objDt
                gvReportes.DataBind()

            Else
                gvReportes.DataSource = Nothing
                gvReportes.DataBind()

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Popula a combo de relatorios
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PopularRelatorios()

        ConfiguracionesGenerales = getConfiguracionesReportes()

        Me.PreencherGridViewReportes(Nothing, ConfiguracionesGenerales())

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 20/07/2009 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Enumeradores.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If

        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.REPORTES
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False

        MyBase.ValidarAcesso = False

    End Sub

    Private Sub RecuperarParametros()
        Dim listaParametros As New List(Of Prosegur.Genesis.Report.Parametro)

        'Lista os parametros do relatório
        For Each relatorio In Me.ConfiguracionSelecionados
            Dim parametros = Me.ObtenerParametros(relatorio.DesReporte)

            For Each param In parametros
                Dim paramLocal = param
                ' verifica se o parametro já existe na lista    
                If Not listaParametros.Exists(Function(p) p.Name = paramLocal.Name) Then
                    listaParametros.Add(param)
                End If
            Next
        Next

        Me.Parametros = listaParametros
    End Sub

    ''' <summary>
    ''' obtem os parametros do relatório
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 06/12/2013 - Criado
    ''' </history>
    Private Function ObtenerParametros(nomeRelatorio As String) As List(Of Prosegur.Genesis.Report.Parametro)

        Dim parametros As New List(Of Prosegur.Genesis.Report.Parametro)

        'Verifica se o relatório existe na lista,
        If ParametrosReporte.ContainsKey(nomeRelatorio) Then
            parametros = ParametrosReporte(nomeRelatorio)
        End If

        Return parametros

    End Function

    Private Sub CriarControles(Optional datos As IAC.Integracion.ContractoServicio.Reportes.ConfiguracionReporte = Nothing)

        Dim listaParametros = Me.Parametros
        ' Cria somente os parametros visible.
        Dim i = 0

        'lista de parametro de cliente, subcliente e ponto de serviço.
        Dim objColParametrosCliente As List(Of Prosegur.Genesis.Report.Parametro) = Nothing
        Dim parametroClienteCargado As Boolean = False

        For Each param In listaParametros.Where(Function(p) p.Visible)
            Dim paramLocal = param


            If param.Visible Then

                i = i + 1
                Dim nomeParametro As String = param.Name
                Dim row As New HtmlTableRow
                Dim cell As New HtmlTableCell
                Dim img As New HtmlImage
                Dim lbl As New Label
                row.ID = String.Format("row_{0}", param.Name)

                If Not (param.Name = CONST_P_CODIGO_CLIENTE OrElse
                        param.Name = CONST_P_CODIGO_SUBCLIENTE OrElse
                        param.Name = CONST_P_CODIGO_PUNTO_SERVICIO OrElse
                        param.Name = CONST_P_CODIGO_GRUPO_CLIENTES OrElse
                        param.Name = CONST_P_CODIGO_DELEGACION OrElse
                        param.Name = CONST_P_CODIGO_DELEGACION_USUARIO OrElse
                        param.Name = CONST_P_FECHA_CONTEO_DESDE OrElse
                        param.Name = CONST_P_FECHA_CONTEO_HASTA OrElse
                        param.Name = CONST_P_FECHA_TRANSPORTE_DESDE OrElse
                        param.Name = CONST_P_FECHA_TRANSPORTE_HASTA) Then

                    'Label
                    cell = New HtmlTableCell
                    cell.Align = "Left"
                    cell.Attributes.Add("class", "tamanho_celula")
                    lbl.ID = String.Format("lbl_{0}", param.Name)
                    lbl.Text = param.Prompt
                    lbl.CssClass = "label2"

                    cell.Controls.Add(lbl)
                    row.Cells.Add(cell)
                End If

                cell = New HtmlTableCell
                Dim csv As New CustomValidator
                csv.ID = String.Format("csv{0}", param.Name)
                csv.Text = "*"
                csv.IsValid = True

                If (param.Name = CONST_P_CODIGO_CLIENTE OrElse
                         param.Name = CONST_P_CODIGO_SUBCLIENTE OrElse
                         param.Name = CONST_P_CODIGO_PUNTO_SERVICIO OrElse
                         param.Name = CONST_P_CODIGO_GRUPO_CLIENTES) Then

                    If Not parametroClienteCargado Then


                        Dim ucCliente As ucClienteSubPto = LoadControl("~/Controles/ucClienteSubPto.ascx")
                        ucCliente.ID = param.Name
                        ucCliente.MostrarJanelaCarregar = False
                        AddHandler ucCliente.OcorreuErro, AddressOf ControleOcorreuErro
                        objColParametrosCliente = (From objParam In listaParametros
                                                   Where objParam.Name = CONST_P_CODIGO_CLIENTE OrElse
                                                         objParam.Name = CONST_P_CODIGO_SUBCLIENTE OrElse
                                                         objParam.Name = CONST_P_CODIGO_PUNTO_SERVICIO OrElse
                                                         objParam.Name = CONST_P_CODIGO_GRUPO_CLIENTES OrElse
                                                         objParam.Name = CONST_P_CODIGO_GRUPO_CLIENTES).ToList()

                        If objColParametrosCliente IsNot Nothing AndAlso objColParametrosCliente.Count > 0 Then
                            Dim inserirClientes As New RespuestaCliSubPto

                            For Each objParam In objColParametrosCliente

                                Select Case objParam.Name

                                    Case CONST_P_CODIGO_CLIENTE
                                        If datos IsNot Nothing Then
                                            Dim result = From p In datos.ParametrosReporte
                                                                  Where p.CodNombreParametro = CONST_P_CODIGO_CLIENTE
                                                                  Select p

                                            If result IsNot Nothing AndAlso result.Count() > 0 Then
                                                inserirClientes.ClientesSeleccionados.AddRange(From p In result
                                                                                               Select New IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente _
                                                                                               With {.Codigo = p.CodParametro, _
                                                                                                     .Descripcion = p.DesParametro})
                                            End If
                                        End If

                                        If (param.MultiValue) Then
                                            ucCliente.MultiSelecaoCliente = True
                                        Else
                                            ucCliente.MultiSelecaoCliente = False
                                        End If

                                        ucCliente.CampoObrigatorioCliente = param.Required
                                        ucCliente.ClienteVisivel = objParam.Visible

                                        If Me.DatosControles IsNot Nothing Then
                                            Dim clientesSeleccionados = Me.DatosControles.FindAll(Function(c) c.CodNombreParametro = CONST_P_CODIGO_CLIENTE)
                                            If clientesSeleccionados IsNot Nothing AndAlso clientesSeleccionados.Count <> 0 Then
                                                For Each cli In clientesSeleccionados
                                                    Dim objCliente As New IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente
                                                    objCliente.Codigo = cli.CodParametro
                                                    objCliente.Descripcion = cli.DesParametro
                                                    ucCliente.Selecionados.ClientesSeleccionados.Add(objCliente)
                                                Next
                                                ucCliente.CarregarListBoxClientes()
                                            End If

                                        End If

                                    Case CONST_P_CODIGO_SUBCLIENTE
                                        If (param.MultiValue) Then
                                            ucCliente.MultiSelecaoSubCliente = True
                                        Else
                                            ucCliente.MultiSelecaoSubCliente = False
                                        End If

                                        If datos IsNot Nothing Then
                                            Dim result = From p In datos.ParametrosReporte
                                                                  Where p.CodNombreParametro = CONST_P_CODIGO_SUBCLIENTE
                                                                  Select p

                                            If result IsNot Nothing AndAlso result.Count() > 0 Then
                                                If result.Count = 1 AndAlso result(0).EsTodos Then
                                                    inserirClientes.BolTodosSubClientesSeleccionados = True
                                                Else
                                                    For Each item In result
                                                        Dim subcliente As New IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.Cliente
                                                        subcliente.Codigo = item.CodParametro.Split("|")(0)
                                                        subcliente.SubClientes = New IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion
                                                        subcliente.SubClientes.Add(New IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente _
                                                                                   With {.Codigo = item.CodParametro.Split("|")(1), _
                                                                                         .Descripcion = item.DesParametro})
                                                        inserirClientes.SubClientesSeleccionados.Add(subcliente)
                                                    Next
                                                End If
                                            End If
                                        End If

                                        ucCliente.SubclienteVisivel = objParam.Visible
                                        ucCliente.CampoObrigatorioSubCliente = param.Required

                                        If Me.DatosControles IsNot Nothing Then
                                            Dim subClientesSeleccionados = Me.DatosControles.FindAll(Function(c) c.CodNombreParametro = CONST_P_CODIGO_SUBCLIENTE)
                                            If subClientesSeleccionados IsNot Nothing AndAlso subClientesSeleccionados.Count <> 0 Then
                                                For Each subCli In subClientesSeleccionados
                                                    If subCli.EsTodos Then
                                                        ucCliente.CambiarEstadoChkTodosSubClientes(True)
                                                        Session("SubClientesSeleccionados") = Nothing
                                                        Exit For
                                                    End If
                                                Next
                                                If Session("SubClientesSeleccionados") IsNot Nothing Then
                                                    ucCliente.Selecionados.SubClientesSeleccionados = Session("SubClientesSeleccionados")
                                                End If
                                                ucCliente.CarregarListBoxSubClientes()
                                            End If

                                        End If

                                    Case CONST_P_CODIGO_PUNTO_SERVICIO
                                        If (param.MultiValue) Then
                                            ucCliente.MultiSelecaoPuntoServicio = True
                                        Else
                                            ucCliente.MultiSelecaoPuntoServicio = False
                                        End If

                                        If datos IsNot Nothing Then
                                            Dim result = From p In datos.ParametrosReporte
                                                                  Where p.CodNombreParametro = CONST_P_CODIGO_PUNTO_SERVICIO
                                                                  Select p

                                            If result IsNot Nothing AndAlso result.Count() > 0 Then
                                                If result.Count = 1 AndAlso result(0).EsTodos Then
                                                    inserirClientes.BolTodosPuntoServicioSeleccionados = True
                                                Else
                                                    For Each item In result
                                                        Dim cliente As New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Cliente
                                                        cliente.Codigo = item.CodParametro.Split("|")(0)
                                                        cliente.SubClientes = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.SubClienteColeccion
                                                        Dim subcliente = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.SubCliente _
                                                                                   With {.Codigo = item.CodParametro.Split("|")(1), _
                                                                                         .PuntosServicio = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.PuntoServicioColeccion}
                                                        subcliente.PuntosServicio.Add(New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.PuntoServicio _
                                                                                      With {.Codigo = item.CodParametro.Split("|")(2), _
                                                                                            .Descripcion = item.DesParametro})
                                                        cliente.SubClientes.Add(subcliente)
                                                        If inserirClientes.PuntoServicioSeleccionados Is Nothing Then
                                                            inserirClientes.PuntoServicioSeleccionados = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion
                                                        End If

                                                        inserirClientes.PuntoServicioSeleccionados.Add(cliente)
                                                    Next
                                                End If
                                            End If
                                        End If

                                        ucCliente.PtoServicoVisivel = objParam.Visible
                                        ucCliente.CampoObrigatorioPuntoServicio = param.Required

                                        If Me.DatosControles IsNot Nothing Then
                                            Dim puntosServicioSeleccionados = Me.DatosControles.FindAll(Function(c) c.CodNombreParametro = CONST_P_CODIGO_PUNTO_SERVICIO)
                                            If puntosServicioSeleccionados IsNot Nothing AndAlso puntosServicioSeleccionados.Count <> 0 Then
                                                For Each punto In puntosServicioSeleccionados
                                                    If punto.EsTodos Then
                                                        ucCliente.CambiarEstadochkTodosPtosServico(True)
                                                        Exit For
                                                    End If

                                                    Dim cliente As New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Cliente
                                                    cliente.Codigo = punto.CodParametro.Split("|")(0)
                                                    cliente.SubClientes = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.SubClienteColeccion
                                                    Dim subcliente = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.SubCliente _
                                                                                   With {.Codigo = punto.CodParametro.Split("|")(1),
                                                                                         .PuntosServicio = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.PuntoServicioColeccion}
                                                    subcliente.PuntosServicio.Add(New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.PuntoServicio _
                                                                                      With {.Codigo = punto.CodParametro.Split("|")(2),
                                                                                            .Descripcion = punto.DesParametro})
                                                    cliente.SubClientes.Add(subcliente)
                                                    If ucCliente.Selecionados.PuntoServicioSeleccionados Is Nothing Then
                                                        ucCliente.Selecionados.PuntoServicioSeleccionados = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion
                                                    End If

                                                    ucCliente.Selecionados.PuntoServicioSeleccionados.Add(cliente)
                                                Next

                                                ucCliente.CarregarListBoxPtoServico()
                                            End If

                                        End If

                                    Case CONST_P_CODIGO_GRUPO_CLIENTES
                                        If (param.MultiValue) Then
                                            ucCliente.MultiSelecaoGrupoCliente = True
                                        Else
                                            ucCliente.MultiSelecaoGrupoCliente = False
                                        End If

                                        If datos IsNot Nothing Then
                                            Dim result = From p In datos.ParametrosReporte
                                                                  Where p.CodNombreParametro = CONST_P_CODIGO_GRUPO_CLIENTES
                                                                  Select p

                                            If result IsNot Nothing AndAlso result.Count() > 0 Then

                                                For Each item In result
                                                    inserirClientes.GrupoClienteSeleccionados = New IAC.ContractoServicio.GrupoCliente.GrupoClienteColeccion
                                                    inserirClientes.GrupoClienteSeleccionados.AddRange(From p In result
                                                                                                       Select New IAC.ContractoServicio.GrupoCliente.GrupoCliente _
                                                                                                       With {.Codigo = p.CodParametro, _
                                                                                                             .Descripcion = p.DesParametro})
                                                Next

                                            End If
                                        End If

                                        ucCliente.GrupoClienteVisivel = objParam.Visible
                                        ucCliente.CampoObrigatorioGrupoCliente = param.Required
                                End Select

                                'Verifica se ele depende de alguem.
                                If param.Dependencies Is Nothing Then
                                    'ucCliente.CargarClientesPreSeleccionados(("Descripcion", "Codigo", Me.RecuperarDados(param))

                                End If

                            Next
                            If (inserirClientes.ClientesSeleccionados IsNot Nothing AndAlso inserirClientes.ClientesSeleccionados.Count > 0) OrElse inserirClientes.GrupoClienteSeleccionados IsNot Nothing AndAlso inserirClientes.GrupoClienteSeleccionados.Count > 0 Then
                                ucCliente.CargarClientesPreSeleccionados(inserirClientes)
                            End If

                        End If

                        cell.Controls.Add(ucCliente)
                        row.Cells.Add(cell)
                    End If


                ElseIf param.Name = CONST_P_CODIGO_DELEGACION Then

                    Dim ucDelegacion As ucDelegacion = LoadControl("~/Controles/ucDelegacion.ascx")
                    ucDelegacion.ID = param.Name
                    If (param.MultiValue) Then
                        ucDelegacion.MultiSelecao = True
                    Else
                        ucDelegacion.MultiSelecao = False
                    End If

                    'Verifica se alguem depende dele.
                    If listaParametros.Exists(Function(p) Not p.Dependencies Is Nothing AndAlso p.Dependencies.Where(Function(x) x.Codigo.Contains(param.Name)).Count > 0) Then
                        ucDelegacion.AutoPostBack(True)
                        AddHandler ucDelegacion.SelectedChanged, AddressOf Me.PostBack
                    End If

                    If datos IsNot Nothing Then
                        Dim result = From p In datos.ParametrosReporte
                                     Where p.CodNombreParametro = CONST_P_CODIGO_DELEGACION
                                     Select p
                        If result IsNot Nothing AndAlso result.Count() > 0 Then
                            If result.Count = 1 AndAlso result(0).EsTodos AndAlso param.MultiValue Then
                                ucDelegacion.PopularControle(True)
                            Else
                                Dim deleg As New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion
                                deleg.AddRange(From p In result
                                               Select New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.Delegacion _
                                             With {.Codigo = p.CodParametro,
                                                   .Descripcion = p.DesParametro})

                                ucDelegacion.PopularControle(deleg)
                            End If
                        End If

                    Else
                        ucDelegacion.PopularControle()
                    End If

                    ucDelegacion.CampoObrigatorio = param.Required
                    cell.Controls.Add(ucDelegacion)

                    AddHandler ucDelegacion.OcorreuErro, AddressOf ControleOcorreuErro
                    row.Cells.Add(cell)

                    If Me.DatosControles IsNot Nothing Then
                        Dim delegaciones = Me.DatosControles.FindAll(Function(d) d.CodNombreParametro = CONST_P_CODIGO_DELEGACION)
                        For Each del In delegaciones
                            ucDelegacion.CambiarEstadoDelegacion(True, del.CodParametro, param.MultiValue)
                        Next
                    End If

                ElseIf param.Name = CONST_P_CODIGO_DELEGACION_USUARIO Then

                    Dim ucDelegacion As ucDelegacion = LoadControl("~/Controles/ucDelegacion.ascx")
                    ucDelegacion.ID = param.Name
                    AddHandler ucDelegacion.OcorreuErro, AddressOf ControleOcorreuErro
                    If (param.MultiValue) Then
                        ucDelegacion.MultiSelecao = True
                    Else
                        ucDelegacion.MultiSelecao = False
                    End If

                    'Verifica se alguem depende dele.
                    If listaParametros.Exists(Function(p) Not p.Dependencies Is Nothing AndAlso p.Dependencies.Where(Function(x) x.Codigo.Contains(param.Name)).Count > 0) Then
                        ucDelegacion.AutoPostBack(True)
                        AddHandler ucDelegacion.SelectedChanged, AddressOf Me.PostBack
                    End If

                    Dim delegaciones As New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion
                    delegaciones.AddRange((From p In Me.InformacionUsuario.Delegaciones
                                           Select New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.Delegacion With {
                                               .Codigo = p.Codigo,
                                               .Descripcion = p.Descripcion}).ToList())

                    If datos IsNot Nothing Then
                        Dim delSelecionadas = From p In datos.ParametrosReporte
                                                                 Where p.CodNombreParametro = CONST_P_CODIGO_DELEGACION_USUARIO
                                                                 Select p
                        If delSelecionadas IsNot Nothing AndAlso delSelecionadas.Count() > 0 Then
                            If delSelecionadas.Count = 1 AndAlso delSelecionadas(0).EsTodos AndAlso param.MultiValue Then
                                ucDelegacion.PopularControle(True, delegaciones)
                            Else
                                Dim deleg As New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion
                                deleg.AddRange(From p In delSelecionadas
                                             Select New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.Delegacion _
                                             With {.Codigo = p.CodParametro, _
                                                   .Descripcion = p.DesParametro})

                                ucDelegacion.PopularControle(deleg, delegaciones)
                            End If
                        Else
                            ucDelegacion.PopularControle(New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion, delegaciones)
                        End If
                    Else
                        ucDelegacion.PopularControle(New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion, delegaciones)
                    End If

                    If Me.DatosControles IsNot Nothing Then
                        Dim del = Me.DatosControles.FindAll(Function(d) d.CodNombreParametro = CONST_P_CODIGO_DELEGACION_USUARIO)
                        For Each d In del
                            ucDelegacion.CambiarEstadoDelegacion(True, d.CodParametro, param.MultiValue)
                        Next
                    End If

                    ucDelegacion.CampoObrigatorio = param.Required
                    cell.Controls.Add(ucDelegacion)
                    row.Cells.Add(cell)
                ElseIf param.Name = CONST_P_CODIGO_INVENTARIO Then
                    Dim inventario As ucInventario = LoadControl("~/Controles/ucInventario.ascx")
                    inventario.ID = param.Name
                    inventario.SelecionarMenu()
                    cell.Controls.Add(inventario)
                    row.Cells.Add(cell)
                ElseIf param.Name = CONST_P_FECHA_CONTEO_DESDE OrElse param.Name = CONST_P_FECHA_CONTEO_HASTA Then
                    If param.Name = CONST_P_FECHA_CONTEO_DESDE Then
                        Dim data As ucData = LoadControl("Controles/ucData.ascx")
                        data.ID = param.Name
                        data.LabelData = Traduzir("024_lblFechaRecaudacionIni")

                        If datos IsNot Nothing Then
                            Dim result = From p In datos.ParametrosReporte
                                                  Where p.CodNombreParametro = CONST_P_FECHA_CONTEO_DESDE
                                                  Select p

                            If result IsNot Nothing AndAlso result.Count() > 0 Then
                                data.DataInicio = result(0).CodParametro
                            End If

                        End If

                        Dim datafinal = From p In listaParametros.Where(Function(p) p.Visible)
                                        Where p.Name = CONST_P_FECHA_CONTEO_HASTA
                                        Select p
                        If datafinal IsNot Nothing AndAlso datafinal.Count() > 0 Then
                            data.DataFinalVisivel = True
                            If datos IsNot Nothing Then
                                Dim result = From p In datos.ParametrosReporte
                                                      Where p.CodNombreParametro = CONST_P_FECHA_CONTEO_HASTA
                                                      Select p

                                If result IsNot Nothing AndAlso result.Count() > 0 Then
                                    data.DataFin = result(0).CodParametro
                                End If

                            End If
                        Else
                            data.DataFinalVisivel = False
                        End If
                        data.DatasObrigatorias = param.Required

                        cell.Controls.Add(data)
                        row.Cells.Add(cell)

                        If Me.DatosControles IsNot Nothing Then
                            Dim dataSelecionada = Me.DatosControles.Find(Function(d) d.CodNombreParametro = CONST_P_FECHA_CONTEO_DESDE)
                            If dataSelecionada IsNot Nothing Then
                                data.DataInicio = dataSelecionada.CodParametro
                            End If

                            dataSelecionada = Me.DatosControles.Find(Function(d) d.CodNombreParametro = CONST_P_FECHA_CONTEO_HASTA)
                            If dataSelecionada IsNot Nothing Then
                                data.DataFin = dataSelecionada.CodParametro
                            End If

                        End If

                    End If

                ElseIf param.Name = CONST_P_FECHA_TRANSPORTE_DESDE OrElse param.Name = CONST_P_FECHA_TRANSPORTE_HASTA Then
                    If param.Name = CONST_P_FECHA_TRANSPORTE_DESDE Then

                        Dim data As ucData = LoadControl("Controles/ucData.ascx")
                        data.ID = param.Name
                        data.LabelData = Traduzir("024lblFechaTransporteIni")

                        If datos IsNot Nothing Then
                            Dim result = From p In datos.ParametrosReporte
                                                  Where p.CodNombreParametro = CONST_P_FECHA_TRANSPORTE_DESDE
                                                  Select p

                            If result IsNot Nothing AndAlso result.Count() > 0 Then
                                data.DataInicio = result(0).CodParametro
                            End If

                        End If

                        Dim datafinal = From p In listaParametros.Where(Function(p) p.Visible)
                                                            Where p.Name = CONST_P_FECHA_TRANSPORTE_HASTA
                                                            Select p
                        If datafinal IsNot Nothing AndAlso datafinal.Count() > 0 Then
                            data.DataFinalVisivel = True

                            If datos IsNot Nothing Then
                                Dim result = From p In datos.ParametrosReporte
                                                      Where p.CodNombreParametro = CONST_P_FECHA_TRANSPORTE_HASTA
                                                      Select p

                                If result IsNot Nothing AndAlso result.Count() > 0 Then
                                    data.DataFin = result(0).CodParametro
                                End If

                            End If

                        Else
                            data.DataFinalVisivel = False
                        End If

                        data.DatasObrigatorias = param.Required

                        cell.Controls.Add(data)
                        row.Cells.Add(cell)

                        If Me.DatosControles IsNot Nothing Then
                            Dim dataSelecionada = Me.DatosControles.Find(Function(d) d.CodNombreParametro = CONST_P_FECHA_TRANSPORTE_DESDE)
                            If dataSelecionada IsNot Nothing Then
                                data.DataInicio = dataSelecionada.CodParametro
                            End If

                            dataSelecionada = Me.DatosControles.Find(Function(d) d.CodNombreParametro = CONST_P_FECHA_TRANSPORTE_HASTA)
                            If dataSelecionada IsNot Nothing Then
                                data.DataFin = dataSelecionada.CodParametro
                            End If

                        End If

                    End If
                ElseIf (param.DataSet) Then

                    If (param.MultiValue) Then

                        Dim chk As ucCheckBoxList = LoadControl("Controles/ucCheckBoxList.ascx")
                        chk.ID = param.Name

                        ' verifica se esse parametro é pai de algum outro
                        If listaParametros.Exists(Function(p) Not p.Dependencies Is Nothing AndAlso p.Dependencies.Where(Function(x) x.Codigo.Contains(nomeParametro)).Count > 0) Then
                            chk.AutoPostBack = True
                            AddHandler chk.SelectedChanged, AddressOf Me.PostBack
                        End If

                        chk.Popular("Descripcion", "Codigo", Me.RecuperarDados(param))
                        chk.DataBind()

                        If datos IsNot Nothing Then
                            Dim result As List(Of String) = (From p In datos.ParametrosReporte
                                                             Where p.CodNombreParametro = paramLocal.Name
                                                             Select p.CodParametro).ToList()

                            If result IsNot Nothing AndAlso result.Count() > 0 Then
                                chk.PreencherValoresSeleccionados(result)
                            End If

                        End If

                        If Me.DatosControles IsNot Nothing Then
                            Dim itemSeleccionado = (From p In DatosControles
                                                             Where p.CodNombreParametro = paramLocal.Name
                                                             Select p.CodParametro).ToList()
                            If itemSeleccionado IsNot Nothing Then
                                chk.PreencherValoresSeleccionados(itemSeleccionado)
                            End If
                        End If

                        cell.Controls.Add(chk)
                        row.Cells.Add(cell)
                        If (param.Required) Then
                            cell = New HtmlTableCell()
                            csv.ErrorMessage = String.Format(Traduzir("lbl_campo_nao_preenchido"), param.Prompt)
                            cell.Controls.Add(csv)
                            row.Cells.Add(cell)
                        End If

                    Else

                        Dim ddl As New DropDownList
                        ddl.ID = param.Name
                        ddl.AppendDataBoundItems = True
                        ddl.Items.Clear()
                        ddl.Items.Add(New ListItem("<-- selecione -->", String.Empty))
                        'Verifica se alguem depende dele.
                        If listaParametros.Exists(Function(p) Not p.Dependencies Is Nothing AndAlso p.Dependencies.Where(Function(x) x.Codigo.Contains(nomeParametro)).Count > 0) Then
                            ddl.AutoPostBack = True
                            AddHandler ddl.SelectedIndexChanged, AddressOf Me.PostBack
                        End If

                        'Verifica se ele depende de alguem.
                        If param.Dependencies Is Nothing Then
                            ddl.DataTextField = "Descripcion"
                            ddl.DataValueField = "Codigo"
                            ddl.DataSource = Me.RecuperarDados(param)
                            ddl.DataBind()

                        End If

                        If datos IsNot Nothing Then
                            Dim result = From p In datos.ParametrosReporte Where p.CodNombreParametro = paramLocal.Name

                            If result.Count > 0 Then

                                'passa os selecionados
                                For Each item As ListItem In ddl.Items
                                    Dim itemLocal = item
                                    Dim achou = From p In result
                                                Where p.CodParametro = itemLocal.Value
                                    If achou.Count > 0 Then
                                        ddl.SelectedValue = item.Value
                                    End If
                                Next
                            End If
                        End If

                        If Me.DatosControles IsNot Nothing Then
                            Dim itemSeleccionado = Me.DatosControles.Find(Function(d) d.CodNombreParametro = param.Name)
                            If itemSeleccionado IsNot Nothing Then
                                ddl.SelectedValue = itemSeleccionado.CodParametro
                            End If
                        End If

                        cell.Controls.Add(ddl)

                        If (param.Required) Then
                            csv.ErrorMessage = String.Format(Traduzir("lbl_campo_nao_preenchido"), param.Prompt)
                            cell.Controls.Add(csv)
                        End If

                        row.Cells.Add(cell)

                    End If

                Else
                    If (param.DataType = "String" OrElse param.DataType = "Integer" OrElse param.DataType = "Float") Then
                        Dim txt As New TextBox
                        txt.ID = param.Name
                        txt.Width = "200"
                        txt.CssClass = "ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                        If listaParametros.Exists(Function(p) Not p.Dependencies Is Nothing AndAlso p.Dependencies.Where(Function(x) x.Codigo.Contains(nomeParametro)).Count > 0) Then
                            txt.AutoPostBack = True
                            AddHandler txt.TextChanged, AddressOf Me.PostBack
                        End If

                        If Me.DatosControles IsNot Nothing Then
                            Dim datosTxt = Me.DatosControles.Find(Function(t) t.CodNombreParametro = param.Name)
                            If datosTxt IsNot Nothing Then
                                txt.Text = datosTxt.CodParametro
                            End If
                        End If

                        If datos IsNot Nothing Then
                            Dim result = From p In datos.ParametrosReporte
                                         Where p.CodNombreParametro = paramLocal.Name
                                         Select p

                            If result IsNot Nothing AndAlso result.Count() > 0 Then
                                'passa os selecionados
                                txt.Text = result(0).CodParametro
                            End If
                        End If

                        cell.Controls.Add(txt)

                        If (param.Required) Then
                            txt.CssClass = "ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                            csv.ErrorMessage = String.Format(Traduzir("lbl_campo_nao_preenchido"), param.Prompt)
                            cell.Controls.Add(csv)
                        End If

                        row.Cells.Add(cell)
                    ElseIf (param.DataType = "Bolean" OrElse param.DataType = "Boolean") Then
                        Dim chk As New CheckBox
                        chk.ID = param.Name
                        chk.Text = param.Prompt
                        If listaParametros.Exists(Function(p) Not p.Dependencies Is Nothing AndAlso p.Dependencies.Where(Function(x) x.Codigo.Contains(nomeParametro)).Count > 0) Then
                            chk.AutoPostBack = True
                            AddHandler chk.CheckedChanged, AddressOf Me.PostBack
                        End If

                        If Me.DatosControles IsNot Nothing Then
                            Dim datosChk = Me.DatosControles.Find(Function(t) t.CodNombreParametro = param.Name)
                            If datosChk IsNot Nothing Then
                                chk.Checked = Convert.ToBoolean(datosChk.CodParametro)
                            End If
                        End If

                        If datos IsNot Nothing Then
                            Dim result = From p In datos.ParametrosReporte
                                         Where p.CodNombreParametro = paramLocal.Name
                                         Select p

                            If result IsNot Nothing AndAlso result.Count() > 0 Then
                                'passa os selecionados
                                chk.Checked = True
                            End If
                        End If

                        cell.Controls.Add(chk)

                        If (param.Required) Then
                            csv.ErrorMessage = String.Format(Traduzir("lbl_campo_nao_preenchido"), param.Prompt)
                            cell.Controls.Add(csv)
                        End If

                        row.Cells.Add(cell)
                    ElseIf (param.DataType = "DateTime") Then
                        Dim txt As New TextBox
                        txt.ID = param.Name
                        txt.Width = "130"
                        txt.MaxLength = "19"
                        txt.CssClass = "ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                        ' Define a mascara da data
                        txt.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##:##');")

                        If listaParametros.Exists(Function(p) Not p.Dependencies Is Nothing AndAlso p.Dependencies.Where(Function(x) x.Codigo.Contains(nomeParametro)).Count > 0) Then
                            txt.Attributes.Add("onblur", "PostBack(this);")
                            txt.Attributes.Add("onchange", "atualizarValor(this);")
                            txt.Attributes.Add("valor-anterior", "")
                        End If

                        If datos IsNot Nothing Then
                            Dim result = From p In datos.ParametrosReporte
                                                  Where p.CodNombreParametro = paramLocal.Name
                                                  Select p

                            If result IsNot Nothing AndAlso result.Count() > 0 Then
                                'passa os selecionados
                                txt.Text = result(0).CodParametro
                            End If
                        End If

                        If Me.DatosControles IsNot Nothing Then
                            Dim dataSelecionada = Me.DatosControles.Find(Function(d) d.CodNombreParametro = param.Name)
                            If dataSelecionada IsNot Nothing Then
                                txt.Text = dataSelecionada.CodParametro
                            End If
                        End If

                        cell.Controls.Add(txt)

                        ' Obtém a lista de idiomas
                        Dim Idiomas As List(Of String) = ObterIdiomas()

                        ' Recupera o idioma corrente
                        Dim IdiomaCorrente As String = Idiomas(0).Split("-")(0)

                        Dim script As String = String.Empty

                        script &= String.Format("AbrirCalendario('{0}','{1}',{2});", _
                                 "ctl00_ContentPlaceHolder1_" & txt.ClientID, "true", 1)

                        scriptManager.RegisterClientScriptBlock(Me, Me.GetType(), Guid.NewGuid().ToString(), script, True)

                        If (param.Required) Then
                            txt.CssClass = "ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                            csv.ErrorMessage = String.Format(Traduzir("lbl_campo_nao_preenchido"), param.Prompt)
                            cell.Controls.Add(csv)
                        End If

                        row.Cells.Add(cell)
                    End If
                End If

                If (param.Name = CONST_P_CODIGO_CLIENTE OrElse
                    param.Name = CONST_P_CODIGO_SUBCLIENTE OrElse
                    param.Name = CONST_P_CODIGO_PUNTO_SERVICIO OrElse _
                    param.Name = CONST_P_CODIGO_GRUPO_CLIENTES) AndAlso Not parametroClienteCargado Then

                    parametroClienteCargado = True
                    If tabelaCampos1.FindControl(row.ID) Is Nothing Then
                        Me.tabelaCampos1.Rows.Add(row)
                    End If
                ElseIf param.Name = CONST_P_CODIGO_DELEGACION OrElse _
                    param.Name = CONST_P_CODIGO_DELEGACION_USUARIO OrElse _
                    param.Name = CONST_P_FECHA_CONTEO_DESDE OrElse _
                    param.Name = CONST_P_FECHA_CONTEO_HASTA OrElse _
                    param.Name = CONST_P_FECHA_TRANSPORTE_DESDE OrElse _
                    param.Name = CONST_P_FECHA_TRANSPORTE_HASTA Then

                    If tabelaCampos1.FindControl(row.ID) Is Nothing Then
                        Me.tabelaCampos1.Rows.Add(row)
                    End If
                Else
                    If tabelaCampos.FindControl(row.ID) Is Nothing Then
                        Me.tabelaCampos.Rows.Add(row)
                    End If
                End If

            End If

        Next
        upPrincipal.Update()
    End Sub

    Private Function RecuperaDatosControles(operacion As Enumeradores.eTipoOperacion) As IAC.Integracion.ContractoServicio.Reportes.ParametroReporteColeccion

        If Parametros IsNot Nothing AndAlso Parametros.Count > 0 Then

            Dim ParametrosControles As New IAC.Integracion.ContractoServicio.Reportes.ParametroReporteColeccion

            Dim parametrosDelegacion = From p In Parametros.Where(Function(p) p.Visible)
                                         Where p.Name = CONST_P_CODIGO_DELEGACION OrElse
                                         p.Name = CONST_P_CODIGO_DELEGACION_USUARIO

            If parametrosDelegacion IsNot Nothing AndAlso parametrosDelegacion.Count > 0 Then
                For Each parametroDelegacion In parametrosDelegacion
                    Dim parametroDelegacionLocal = parametroDelegacion
                    Dim linha = tabelaCampos.FindControl(String.Format("row_{0}", parametroDelegacion.Name))
                    If linha IsNot Nothing Then
                        Dim DelegacionControl As ucDelegacion = linha.FindControl(parametroDelegacion.Name)

                        If DelegacionControl IsNot Nothing Then
                            Dim validar As Boolean = operacion <> Enumeradores.eTipoOperacion.PostBack
                            Dim delegaciones = DelegacionControl.RecuperarSelecionado(validar)
                            If delegaciones IsNot Nothing Then

                                ParametrosControles.AddRange(From p In delegaciones
                                                              Select New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte With {
                                                                  .CodNombreParametro = parametroDelegacionLocal.Name, _
                                                                  .CodParametro = p.Codigo, _
                                                                  .DesParametro = p.Descripcion})
                            End If

                        End If

                    End If

                Next

            End If

            Dim ParametrosCliente = From p In Parametros.Where(Function(p) p.Visible)
                                         Where p.Name = CONST_P_CODIGO_CLIENTE OrElse
                                         p.Name = CONST_P_CODIGO_SUBCLIENTE OrElse
                                         p.Name = CONST_P_CODIGO_PUNTO_SERVICIO OrElse
                                         p.Name = CONST_P_CODIGO_GRUPO_CLIENTES


            If ParametrosCliente IsNot Nothing AndAlso ParametrosCliente.Count > 0 Then
                Dim clientes As RespuestaCliSubPto = Nothing

                For Each parametroCliente In ParametrosCliente
                    Dim linha = tabelaCampos.FindControl(String.Format("row_{0}", parametroCliente.Name))
                    If linha IsNot Nothing Then
                        Dim ClienteControl As ucClienteSubPto = linha.FindControl(parametroCliente.Name)

                        If ClienteControl IsNot Nothing Then
                            clientes = ClienteControl.Respuesta
                            Exit For
                        End If
                    End If
                Next
                If clientes IsNot Nothing Then

                    For Each parametroCliente In ParametrosCliente
                        Dim parametroClienteLocal = parametroCliente
                        Select Case parametroCliente.Name
                            Case CONST_P_CODIGO_GRUPO_CLIENTES
                                If clientes.GrupoClienteSeleccionados IsNot Nothing Then

                                    Dim ListaGrupo As List(Of String)

                                    For Each Grupo In clientes.GrupoClienteSeleccionados

                                        ListaGrupo = New List(Of String)
                                        ListaGrupo.Add(Grupo.Codigo)


                                        If operacion = Enumeradores.eTipoOperacion.Generar Then

                                            ' Recupara os cliente, subcliente e pontos de serviços do grupo
                                            RetornarParametrosGrupoClienteSubClientePuntoServico(ParametrosControles,
                                                                                                 New IAC.ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Peticion With {.Codigo = ListaGrupo})
                                        End If

                                        ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte With { _
                                                                .CodNombreParametro = parametroCliente.NameOriginal, _
                                                                .CodParametro = Grupo.Codigo, _
                                                                .DesParametro = Grupo.Descripcion})
                                    Next

                                Else
                                    'Verifica se o parâmetro "P_COM_GRUPO" está permitindo valor nulo no Report Server, pois quando seleciona
                                    'cliente,sub e pto o grupo é passado nulo
                                    If parametroCliente.Required Then
                                        'Resgatar o nome do reporte, pois o parametroCliente.NomeRelatorio está vindo vazio
                                        Dim path As New List(Of String)
                                        path = parametroCliente.PathRelatorio.Split("/").ToList
                                        Dim reporte As String = String.Empty
                                        If path.Count > 0 Then
                                            reporte = path(path.Count - 1)
                                        End If

                                        If operacion <> Enumeradores.eTipoOperacion.PostBack Then
                                            MyBase.MostraMensagem(String.Format(Traduzir("027_msg_parametro_permitir_nulo"), parametroCliente.Name, reporte))
                                            Master.ControleErro.AddError(String.Format(Traduzir("027_msg_parametro_permitir_nulo"), parametroCliente.Name, reporte), Enumeradores.eMensagem.Atencao)
                                        End If


                                    End If
                                End If

                            Case CONST_P_CODIGO_PUNTO_SERVICIO
                                If clientes.SubClientesSeleccionados IsNot Nothing OrElse clientes.BolTodosSubClientesSeleccionados Then

                                    If clientes.BolTodosPuntoServicioSeleccionados Then
                                        ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte With {
                                                                       .CodNombreParametro = parametroCliente.Name, _
                                                                       .CodParametro = "-1", _
                                                                       .EsTodos = True})
                                    Else
                                        If clientes.PuntoServicioSeleccionados IsNot Nothing Then
                                            ParametrosControles.AddRange(From c In clientes.PuntoServicioSeleccionados
                                                                         From s In c.SubClientes
                                                                         From p In s.PuntosServicio
                                                                              Select New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte With {
                                                                                  .CodNombreParametro = parametroClienteLocal.Name, _
                                                                                  .CodParametro = c.Codigo & "|" & s.Codigo & "|" & p.Codigo, _
                                                                                  .DesParametro = p.Descripcion})

                                        End If
                                    End If
                                End If
                            Case CONST_P_CODIGO_SUBCLIENTE
                                If clientes.ClientesSeleccionados IsNot Nothing Then
                                    If clientes.BolTodosSubClientesSeleccionados Then
                                        ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte With {
                                                             .CodNombreParametro = parametroCliente.Name, _
                                                             .CodParametro = "-1", _
                                                             .EsTodos = True})
                                    Else
                                        If clientes.SubClientesSeleccionados IsNot Nothing Then
                                            ParametrosControles.AddRange(From p In clientes.SubClientesSeleccionados
                                                                         From s In p.SubClientes
                                                        Select New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte With {
                                                            .CodNombreParametro = parametroClienteLocal.Name, _
                                                            .CodParametro = p.Codigo & "|" & s.Codigo, _
                                                            .DesParametro = s.Descripcion})
                                        End If
                                    End If
                                End If

                            Case CONST_P_CODIGO_CLIENTE
                                If clientes.ClientesSeleccionados IsNot Nothing Then
                                    ParametrosControles.AddRange(From p In clientes.ClientesSeleccionados
                                                     Select New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte With {
                                                         .CodNombreParametro = parametroClienteLocal.Name, _
                                                         .CodParametro = p.Codigo, _
                                                         .DesParametro = p.Descripcion})
                                End If
                        End Select
                    Next
                End If

            End If

            Dim objParam As Prosegur.Genesis.Report.Parametro = (From param In Parametros Where param.Name = CONST_P_DE_PARA).FirstOrDefault

            If objParam IsNot Nothing Then

                If objParam.ValidValue IsNot Nothing AndAlso objParam.ValidValue.ParameterValues IsNot Nothing AndAlso objParam.ValidValue.ParameterValues.Count > 0 Then

                    For Each Valor In objParam.ValidValue.ParameterValues

                        ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte With { _
                                                .CodNombreParametro = CONST_P_DE_PARA, _
                                                .CodParametro = Valor.Value, _
                                                .DesParametro = Valor.Label})

                    Next

                End If

            End If

            For Each param In Parametros.Where(Function(p) p.Visible AndAlso
                                               (p.Name <> CONST_P_CODIGO_CLIENTE AndAlso
                                                p.Name <> CONST_P_CODIGO_SUBCLIENTE AndAlso
                                                p.Name <> CONST_P_CODIGO_PUNTO_SERVICIO AndAlso
                                                p.Name <> CONST_P_CODIGO_GRUPO_CLIENTES AndAlso
                                                p.Name <> CONST_P_CODIGO_DELEGACION AndAlso
                                                p.Name <> CONST_P_CODIGO_DELEGACION_USUARIO))
                Dim paramLocal = param
                Select Case param.Name

                    Case CONST_P_FECHA_CONTEO_DESDE, CONST_P_FECHA_CONTEO_HASTA
                        If param.Name = CONST_P_FECHA_CONTEO_DESDE Then

                            Dim linha = tabelaCampos.FindControl(String.Format("row_{0}", CONST_P_FECHA_CONTEO_DESDE))
                            If linha IsNot Nothing Then
                                Dim Data As ucData = linha.FindControl(CONST_P_FECHA_CONTEO_DESDE)

                                If Data IsNot Nothing Then
                                    Try
                                        If param.Required Then
                                            If operacion <> Enumeradores.eTipoOperacion.PostBack Then
                                                Data.Validar()
                                            End If


                                            ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte _
                                                                    With {.CodNombreParametro = param.Name, _
                                                                          .CodParametro = Data.DataInicio})
                                            Dim datafinal = From p In Parametros.Where(Function(p) p.Visible)
                                                      Where p.Name = CONST_P_FECHA_CONTEO_HASTA
                                                      Select p
                                            If datafinal IsNot Nothing AndAlso datafinal.Count() > 0 Then
                                                ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte _
                                                                   With {.CodNombreParametro = CONST_P_FECHA_CONTEO_HASTA, _
                                                                         .CodParametro = Data.DataFin})
                                            End If
                                        Else
                                            If operacion <> Enumeradores.eTipoOperacion.PostBack Then
                                                Data.Validar()
                                            End If


                                            If Not String.IsNullOrEmpty(Data.DataInicio) Then
                                                ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte _
                                                                        With {.CodNombreParametro = param.Name, _
                                                                              .CodParametro = Data.DataInicio})
                                            End If

                                            Dim datafinal = From p In Parametros.Where(Function(p) p.Visible)
                                                   Where p.Name = CONST_P_FECHA_CONTEO_HASTA
                                                   Select p
                                            If datafinal IsNot Nothing AndAlso datafinal.Count() > 0 Then
                                                If Not String.IsNullOrEmpty(Data.DataFin) Then
                                                    ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte _
                                                                            With {.CodNombreParametro = CONST_P_FECHA_CONTEO_HASTA, _
                                                                                  .CodParametro = Data.DataFin})
                                                End If
                                            End If

                                        End If
                                    Catch ex As Excepcion.NegocioExcepcion
                                        MyBase.MostraMensagemExcecao(ex)
                                    Catch ex As Exception
                                        MyBase.MostraMensagemExcecao(ex)
                                    End Try

                                End If
                            End If
                        End If

                    Case CONST_P_FECHA_TRANSPORTE_DESDE, CONST_P_FECHA_TRANSPORTE_HASTA
                        If param.Name = CONST_P_FECHA_TRANSPORTE_DESDE Then


                            Dim linha = tabelaCampos.FindControl(String.Format("row_{0}", CONST_P_FECHA_TRANSPORTE_DESDE))
                            If linha IsNot Nothing Then
                                Dim Data As ucData = linha.FindControl(CONST_P_FECHA_TRANSPORTE_DESDE)

                                If Data IsNot Nothing Then

                                    Try
                                        If param.Required Then
                                            If operacion <> Enumeradores.eTipoOperacion.PostBack Then
                                                Data.Validar()
                                            End If

                                            ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte With {.CodNombreParametro = param.Name, _
                                                                                                                                      .CodParametro = Data.DataInicio})

                                            Dim datafinal = From p In Parametros.Where(Function(p) p.Visible)
                                                       Where p.Name = CONST_P_FECHA_TRANSPORTE_HASTA
                                                       Select p
                                            If datafinal IsNot Nothing AndAlso datafinal.Count() > 0 Then
                                                ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte _
                                                                        With {.CodNombreParametro = CONST_P_FECHA_TRANSPORTE_HASTA, _
                                                                              .CodParametro = Data.DataInicio})
                                            End If
                                        Else
                                            If operacion <> Enumeradores.eTipoOperacion.PostBack Then
                                                Data.Validar()
                                            End If


                                            If Not String.IsNullOrEmpty(Data.DataInicio) Then
                                                ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte _
                                                                        With {.CodNombreParametro = param.Name, _
                                                                              .CodParametro = Data.DataInicio})
                                            End If

                                            Dim datafinal = From p In Parametros.Where(Function(p) p.Visible)
                                                       Where p.Name = CONST_P_FECHA_TRANSPORTE_HASTA
                                                       Select p
                                            If datafinal IsNot Nothing AndAlso datafinal.Count() > 0 Then
                                                If Not String.IsNullOrEmpty(Data.DataFin) Then
                                                    ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte _
                                                                            With {.CodNombreParametro = CONST_P_FECHA_TRANSPORTE_HASTA, _
                                                                                  .CodParametro = Data.DataFin})
                                                End If
                                            End If

                                        End If
                                    Catch ex As Excepcion.NegocioExcepcion
                                        MyBase.MostraMensagemExcecao(ex)
                                    Catch ex As Exception
                                        MyBase.MostraMensagemExcecao(ex)
                                    End Try

                                End If
                            End If
                        End If

                    Case CONST_P_CODIGO_INVENTARIO
                        Me.DadosInventario(ParametrosControles)

                    Case Else

                        Dim linha = tabelaCampos.FindControl(String.Format("row_{0}", param.Name))
                        If param.DataSet Then

                            If param.MultiValue Then
                                If param.Required Then
                                    Dim ucCheckBox As ucCheckBoxList = linha.FindControl(param.Name)
                                    Dim checados = ucCheckBox.Selecionados()
                                    If checados Is Nothing OrElse checados.Count = 0 Then
                                        Dim csvControl As CustomValidator = linha.FindControl(String.Format("csv{0}", param.Name))
                                        csvControl.IsValid = False
                                        If operacion <> Enumeradores.eTipoOperacion.PostBack Then
                                            MyBase.MostraMensagem(csvControl.ErrorMessage)
                                            Master.ControleErro.AddError(csvControl.ErrorMessage, Enumeradores.eMensagem.Atencao)
                                        End If
                                    Else
                                        ParametrosControles.AddRange(From value In checados
                                                                      Select New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte _
                                                                      With {.CodNombreParametro = paramLocal.Name, _
                                                                            .CodParametro = value})
                                    End If
                                Else
                                    Dim ucCheckBox As ucCheckBoxList = linha.FindControl(param.Name)
                                    Dim checados = ucCheckBox.Selecionados()
                                    If checados IsNot Nothing AndAlso checados.Count > 0 Then
                                        ParametrosControles.AddRange(From value In checados
                                                                      Select New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte _
                                                                      With {.CodNombreParametro = paramLocal.Name, _
                                                                            .CodParametro = value})
                                    End If
                                End If
                            Else
                                Dim dropDown As DropDownList = linha.FindControl(param.Name)
                                If param.Required Then
                                    If dropDown.SelectedIndex = 0 Then
                                        Dim csvControl As CustomValidator = linha.FindControl(String.Format("csv{0}", param.Name))
                                        csvControl.IsValid = False
                                        If operacion <> Enumeradores.eTipoOperacion.PostBack Then
                                            MyBase.MostraMensagem(csvControl.ErrorMessage)
                                            Master.ControleErro.AddError(csvControl.ErrorMessage, Enumeradores.eMensagem.Atencao)
                                        End If
                                    Else
                                        ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte With {.CodNombreParametro = param.Name, _
                                                                                                                                  .DesParametro = dropDown.SelectedItem.Text, _
                                                                                                                                  .CodParametro = dropDown.SelectedItem.Value})
                                    End If
                                Else

                                    If dropDown.SelectedIndex > 0 Then
                                        ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte With {.CodNombreParametro = param.Name, _
                                                                                                                                  .DesParametro = dropDown.SelectedItem.Text, _
                                                                                                                                  .CodParametro = dropDown.SelectedItem.Value})
                                    End If
                                End If
                            End If
                        Else
                            Select Case param.DataType
                                Case "DateTime"
                                    Dim textbox As TextBox = linha.FindControl(param.Name)
                                    If param.Required Then
                                        If DataVazia(textbox.Text) Then
                                            Dim csvControl As CustomValidator = linha.FindControl(String.Format("csv{0}", param.Name))
                                            csvControl.IsValid = False
                                            If operacion <> Enumeradores.eTipoOperacion.PostBack Then
                                                MyBase.MostraMensagem(csvControl.ErrorMessage)
                                                Master.ControleErro.AddError(csvControl.ErrorMessage, Enumeradores.eMensagem.Atencao)
                                            End If
                                        Else
                                            ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte With {.CodNombreParametro = param.Name, _
                                                                                                                                     .CodParametro = textbox.Text})
                                        End If
                                    Else
                                        If Not DataVazia(textbox.Text) Then
                                            ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte With {.CodNombreParametro = param.Name, _
                                                                                                                                     .CodParametro = textbox.Text})
                                        End If
                                    End If

                                Case "String", "Integer", "Float"
                                    Dim textbox As TextBox = linha.FindControl(param.Name)
                                    If param.Required Then
                                        If String.IsNullOrEmpty(textbox.Text) Then
                                            Dim csvControl As CustomValidator = linha.FindControl(String.Format("csv{0}", param.Name))
                                            csvControl.IsValid = False
                                            If operacion <> Enumeradores.eTipoOperacion.PostBack Then
                                                MyBase.MostraMensagem(csvControl.ErrorMessage)
                                                Master.ControleErro.AddError(csvControl.ErrorMessage, Enumeradores.eMensagem.Atencao)
                                            End If
                                        Else
                                            ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte With {.CodNombreParametro = param.Name, _
                                                                                                                                     .CodParametro = textbox.Text})
                                        End If
                                    Else
                                        If Not String.IsNullOrEmpty(textbox.Text) Then
                                            ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte With {.CodNombreParametro = param.Name, _
                                                                                                                                    .CodParametro = textbox.Text})
                                        End If
                                    End If


                                Case "Bolean", "Boolean"
                                    Dim checkbox As CheckBox = linha.FindControl(param.Name)
                                    ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte With {.CodNombreParametro = param.Name, _
                                                                                                                                  .CodParametro = checkbox.Checked})
                            End Select

                        End If

                End Select
            Next

            If Not Master.ControleErro.HayErrors OrElse operacion = Enumeradores.eTipoOperacion.PostBack Then
                Return ParametrosControles
            Else
                Return Nothing
            End If
        Else
            Return Nothing
        End If
    End Function

    Private Sub RetornarParametrosGrupoClienteSubClientePuntoServico(ByRef objParametros As IAC.Integracion.ContractoServicio.Reportes.ParametroReporteColeccion,
                                                                     objPeticionGrupoClienteDetail As IAC.ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Peticion)

        Dim objProxyGrupoClientes As New Comunicacion.ProxyGrupoClientes
        Dim objRespuesta As IAC.ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Respuesta = Nothing

        objRespuesta = objProxyGrupoClientes.GetGruposClientesDetalle(objPeticionGrupoClienteDetail)

        If objRespuesta.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

            Exit Sub
        End If

        Dim ParametrosReporteCorrente As List(Of Parametro) = Nothing

        If ConfiguracionSelecionados IsNot Nothing AndAlso ConfiguracionSelecionados.Count > 0 Then

            ParametrosReporteCorrente = New List(Of Parametro)
            Dim ObjColParametrosReporte As List(Of Parametro) = Nothing

            For Each Reportes In ConfiguracionSelecionados

                ObjColParametrosReporte = ParametrosReporte(Reportes.DesReporte)

                If ObjColParametrosReporte IsNot Nothing AndAlso ObjColParametrosReporte.Count > 0 Then
                    ParametrosReporteCorrente.AddRange(ObjColParametrosReporte)
                End If

            Next

        End If

        ' Para cada cliente existente no grupo
        For Each cliente In objRespuesta.GrupoCliente.First.Clientes

            ' Se existe o parâmetro de Cliente
            If ParametrosReporteCorrente.Where(Function(pr) pr.Name = CONST_P_CODIGO_CLIENTE).Count > 0 AndAlso
                String.IsNullOrEmpty(cliente.CodSubCliente) AndAlso String.IsNullOrEmpty(cliente.CodPtoServicio) Then

                ' Adiciona o cliente a lista de parâmetros
                objParametros.Add(RecuperarValorParametro(CONST_P_CODIGO_CLIENTE, cliente.DesCliente, cliente.CodCliente))

            End If

            ' Para cada subcliente existente
            If Not String.IsNullOrEmpty(cliente.CodSubCliente) Then

                ' Se existe o parâmetro de SubCliente
                If ParametrosReporteCorrente.Where(Function(pr) pr.Name = CONST_P_CODIGO_SUBCLIENTE).Count > 0 AndAlso String.IsNullOrEmpty(cliente.CodPtoServicio) Then

                    ' Adiciona o subcliente a lista de parâmetros
                    objParametros.Add(RecuperarValorParametro(CONST_P_CODIGO_SUBCLIENTE, cliente.DesSubCliente, cliente.CodCliente & "|" & cliente.CodSubCliente))

                End If

                ' Para cada ponto de serviço existente
                If Not String.IsNullOrEmpty(cliente.CodPtoServicio) Then

                    ' Se existe o parâmetro de Ponto de Serviço
                    If ParametrosReporteCorrente.Where(Function(pr) pr.Name = CONST_P_CODIGO_PUNTO_SERVICIO).Count > 0 Then

                        ' Adiciona o ponto de serviço a lista de parâmetros
                        objParametros.Add(RecuperarValorParametro(CONST_P_CODIGO_PUNTO_SERVICIO, cliente.DesPtoServivico, cliente.CodCliente & "|" & cliente.CodSubCliente & "|" & cliente.CodPtoServicio))

                    End If

                End If

            End If

        Next

        ' Se existe o parametro de Cliente e não existe o codigo do cliente
        If ParametrosReporteCorrente.Where(Function(pr) pr.Name = CONST_P_CODIGO_CLIENTE).Count > 0 AndAlso objParametros.Where(Function(p) p.CodNombreParametro = CONST_P_CODIGO_CLIENTE).Count = 0 Then
            ' Informa, pois é necessario pelo menos um código em branco
            objParametros.Add(RecuperarValorParametro(CONST_P_CODIGO_CLIENTE, String.Empty, String.Empty))
        End If

        ' Se existe o parametro de SubCliente e não existe o codigo do subcliente
        If ParametrosReporteCorrente.Where(Function(pr) pr.Name = CONST_P_CODIGO_SUBCLIENTE).Count > 0 AndAlso objParametros.Where(Function(p) p.CodNombreParametro = CONST_P_CODIGO_SUBCLIENTE).Count = 0 Then
            ' Informa, pois é necessario pelo menos um código em branco
            objParametros.Add(RecuperarValorParametro(CONST_P_CODIGO_SUBCLIENTE, String.Empty, String.Empty))
        End If

        ' Se existe o parametro de Ponto de Serviço e não existe o codigo do ponto de serviço
        If ParametrosReporteCorrente.Where(Function(pr) pr.Name = CONST_P_CODIGO_PUNTO_SERVICIO).Count > 0 AndAlso objParametros.Where(Function(p) p.CodNombreParametro = CONST_P_CODIGO_PUNTO_SERVICIO).Count = 0 Then
            ' Informa, pois é necessario pelo menos um código em branco
            objParametros.Add(RecuperarValorParametro(CONST_P_CODIGO_PUNTO_SERVICIO, String.Empty, String.Empty))
        End If


    End Sub

    ''' <summary>
    ''' Recupera o valor do parametro.
    ''' </summary>
    ''' <param name="CodigoParametro"></param>
    ''' <param name="DesParametro"></param>
    ''' <param name="DesValorParametro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RecuperarValorParametro(CodigoParametro As String, DesParametro As String, DesValorParametro As String) As IAC.Integracion.ContractoServicio.Reportes.ParametroReporte

        Return New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte With {.CodNombreParametro = CodigoParametro, _
                                                                                    .DesParametro = DesParametro, _
                                                                                    .CodParametro = DesValorParametro}
    End Function

    Private Sub PostBack(sender As Object, e As System.EventArgs)
        Try
            If (TypeOf sender Is TextBox) Then

                Dim txt As TextBox = CType(sender, TextBox)
                Me.AtualizarParametro(txt.ID, txt.Text.Trim)
                Me.PopularFilho(txt.ID)
            ElseIf (TypeOf sender Is DropDownList) Then

                Dim ddl As DropDownList = CType(sender, DropDownList)
                Me.AtualizarParametro(ddl.ID, ddl.SelectedValue)
                Me.PopularFilho(ddl.ID)

            ElseIf (TypeOf sender Is ucCheckBoxList) Then
                Dim chk As ucCheckBoxList = CType(sender, ucCheckBoxList)
                Dim sel = chk.Selecionados()

                Me.AtualizarParametro(chk.ID, sel)
                Me.PopularFilho(chk.ID)

            ElseIf (TypeOf sender Is ucDelegacion) Then
                Dim uc As ucDelegacion = CType(sender, ucDelegacion)
                Dim delegaciones = uc.RecuperarSelecionado(False)
                Dim sel As New List(Of String)
                If delegaciones IsNot Nothing Then
                    sel.AddRange(delegaciones.Select(Function(d) d.Codigo))
                End If

                Me.AtualizarParametro(uc.ID, sel)
                Me.PopularFilho(uc.ID)

            ElseIf (TypeOf sender Is CheckBox) Then

            End If

            For Each validator In Page.GetValidators(String.Empty)
                Dim csv As CustomValidator = CType(validator, CustomValidator)
                If (Not csv.IsValid) Then
                    Valida.Add(csv.ErrorMessage)
                End If
            Next

            If (Valida.Count > 0) Then
                MyBase.MostraMensagem(String.Join("<br>", Valida.ToArray()))
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    Private Sub LimparFilho(IDFilho As String)
        'atualiza o filtro para todos as dependencias.
        For Each param In Me.Parametros.Where(Function(p) Not p.Dependencies Is Nothing AndAlso p.Dependencies.Where(Function(x) x.Codigo.Contains(IDFilho)).Count > 0)
            Dim ctl = Me.tabelaCampos.FindControl(param.Name)
            param.Valores = New List(Of String)

            If (Not ctl Is Nothing) Then
                If (TypeOf ctl Is TextBox) Then
                    Dim txtFiho = CType(ctl, TextBox)
                    txtFiho.Text = String.Empty

                ElseIf (TypeOf ctl Is DropDownList) Then
                    Dim ddl = CType(ctl, DropDownList)
                    ddl.AppendDataBoundItems = True
                    ddl.Items.Clear()
                ElseIf (TypeOf ctl Is ucCheckBoxList) Then
                    Dim chk As ucCheckBoxList = CType(ctl, ucCheckBoxList)
                    chk.Limpar()
                ElseIf (TypeOf ctl Is CheckBox) Then
                    Dim chk = CType(ctl, CheckBox)
                    chk.Checked = False
                End If
            End If
        Next
    End Sub

    Private Sub PopularFilho(codigoParametro As String)
        'atualiza o filtro para todos as dependencias.

        For Each param In Me.Parametros.Where(Function(p) Not p.Dependencies Is Nothing AndAlso p.Dependencies.Where(Function(x) x.Codigo.Contains(codigoParametro)).Count > 0)
            Dim ctl = Me.tabelaCampos.FindControl(param.Name)

            If (Not ctl Is Nothing) Then
                If (TypeOf ctl Is TextBox) Then
                    Dim txtFiho = CType(ctl, TextBox)
                    txtFiho.Text = String.Empty

                ElseIf (TypeOf ctl Is DropDownList) Then
                    Dim ddl = CType(ctl, DropDownList)
                    ddl.AppendDataBoundItems = True
                    ddl.Items.Clear()
                    ddl.Items.Add(New ListItem("<-- selecione -->", String.Empty))
                    ddl.DataTextField = "Descripcion"
                    ddl.DataValueField = "Codigo"
                    ddl.DataSource = Me.RecuperarDados(param)
                    ddl.DataBind()

                    'Limpa os registros que depedem desse.
                    Me.LimparFilho(param.Name)
                ElseIf (TypeOf ctl Is ucCheckBoxList) Then
                    Dim chk As ucCheckBoxList = CType(ctl, ucCheckBoxList)
                    chk.Popular("Descripcion", "Codigo", Me.RecuperarDados(param))
                    Me.LimparFilho(param.Name)
                ElseIf (TypeOf ctl Is CheckBox) Then
                    Dim chk = CType(ctl, CheckBox)
                    chk.Checked = False
                    Me.LimparFilho(param.Name)
                End If
            End If
        Next
    End Sub

    Private Function RecuperarDados(param As Parametro) As List(Of Genesis.ContractoServicio.Valor)
        Dim objRetorno As New List(Of Genesis.ContractoServicio.Valor)

        Try
            If ValidaDependencie(param) Then
                Dim proxy As New ListadosConteo.ProxyDinamico
                Dim peticion As New Prosegur.Genesis.ContractoServicio.Dinamico.Peticion
                Dim respuesta As New Prosegur.Genesis.ContractoServicio.Dinamico.Respuesta

                peticion.CommandText = param.ValidValue.DataSetReference.CommandText
                peticion.StoredProcedure = param.ValidValue.DataSetReference.StoredProcedure
                If param.ValidValue.DataSetReference.Conexao Is Nothing Then
                    Throw New Exception(String.Format(Traduzir("024_erro_config_reporte"), param.NomeRelatorio.ToUpper(), param.Prompt))
                End If
                Dim caminhoConexao = param.ValidValue.DataSetReference.Conexao.Split("/")
                peticion.Conexao = caminhoConexao(caminhoConexao.Count() - 1)
                peticion.LabelField = param.ValidValue.DataSetReference.LabelField
                peticion.ValueField = param.ValidValue.DataSetReference.ValueField
                If Not param.Dependencies Is Nothing Then

                    peticion.Parametros = New List(Of Genesis.ContractoServicio.Parametro)
                    For Each paramDepen In param.Dependencies
                        Dim paramDepenLocal = paramDepen
                        Dim parametro = Me.Parametros.Where(Function(p) p.Name = paramDepenLocal.Codigo).FirstOrDefault()

                        'Verifica se o valor foi informado
                        peticion.Parametros.Add(New Genesis.ContractoServicio.Parametro() With {.Codigo = parametro.Name, .Valores = parametro.Valores, .MultiValue = parametro.MultiValue})
                    Next
                End If


                respuesta = proxy.Consultar(peticion)
                If Not String.IsNullOrEmpty(respuesta.MensajeError) Then
                    MyBase.MostraMensagem(respuesta.MensajeError)
                End If
                objRetorno = respuesta.Valores
            Else
                objRetorno = Nothing
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

        Return objRetorno

    End Function

    Protected Sub chbSelecionar_CheckedChanged(sender As Object, e As EventArgs)
        Dim chk As CheckBox = CType(sender, CheckBox)
        Try
            Dim PathRelatorio As String = chk.Attributes("PathRelatorio")

            Me.RecuperarParametros()
            Me.DesabilitaRelatorios()

            Me.CriarControles()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Valida os controle que possuiem dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ValidaDependencie(param As Parametro) As Boolean
        Dim valida As Boolean = True

        If Not param.Dependencies Is Nothing Then
            For Each codParametro In param.Dependencies
                Dim codParametroLocal = codParametro
                Dim parametro = Me.Parametros.Where(Function(p) p.Name = codParametroLocal.Codigo).FirstOrDefault()
                'recupera o validator
                Dim csv As CustomValidator = Me.tabelaCampos.FindControl(String.Format("csv{0}", parametro.Name))
                If (parametro.Valores Is Nothing) OrElse (Not parametro.Valores Is Nothing AndAlso parametro.Valores.Count = 0) OrElse (Not parametro.Valores Is Nothing AndAlso parametro.Valores.Count > 0 AndAlso parametro.Valores(0) = String.Empty) Then
                    If Not csv Is Nothing Then
                        csv.IsValid = False
                        valida = False
                    End If
                Else
                    If Not csv Is Nothing Then
                        csv.IsValid = True
                    End If
                End If
            Next
        End If

        Return valida

    End Function

    Private Sub AtualizarParametro(codigoParametro As String, valorParametro As Object)
        Dim parametro = Me.Parametros.Where(Function(p) p.Name = codigoParametro).FirstOrDefault()
        Dim lista As New List(Of String)
        If (parametro.MultiValue) Then
            If TypeOf valorParametro Is String Then
                lista = valorParametro.ToString().Split(",").ToList()
            Else
                lista = CType(valorParametro, List(Of String))
            End If
        Else
            If TypeOf valorParametro Is String Then
                lista.Add(valorParametro.ToString)
            Else
                lista = CType(valorParametro, List(Of String))
            End If
        End If

        parametro.Valores = lista
    End Sub

    Protected Sub DesabilitaRelatorios()

        Try
            Dim ParametrosAtuais As New List(Of Prosegur.Genesis.Report.Parametro)
            ParametrosAtuais = Me.Parametros

            Dim listaAtivo As New List(Of String)
            'Recupera o parametro de todos os relatórios que ainda não existe na lista e não esteja desabilitado
            For Each row As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Configuracion.General.ConfiguracionGeneral In ConfiguracionesGenerales
                If Not ConfiguracionSelecionados.Exists(Function(d) d.DesReporte = row.DesReporte) Then
                    listaAtivo.Add(row.DesReporte)
                End If
            Next

            Dim ListaParametros As New List(Of Prosegur.Genesis.Report.Parametro)

            'Lista os parametros do relatório
            For Each relatorio In listaAtivo
                Dim parametros = Me.ObtenerParametros(relatorio)
                For Each param In parametros
                    ListaParametros.Add(param)
                Next
            Next

            lstDesabilitar = New List(Of String)

            'para cada parametro já selecionado, verifica nos relatórios ativos se algum tem o mesmo nome e propriedades diferentes.
            For Each atual As Prosegur.Genesis.Report.Parametro In ParametrosAtuais
                Dim atualLocal = atual
                Dim mesmosNomes = ListaParametros.Where(Function(p) p.Name.ToUpper = atualLocal.Name.ToUpper AndAlso p.Visible)
                If mesmosNomes IsNot Nothing Then
                    For Each item In mesmosNomes
                        If Not atual.Igual(item) Then
                            lstDesabilitar.Add(item.NomeRelatorio.ToUpper())
                        End If
                    Next
                End If
            Next

            PopularRelatorios()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

        Try
            MyBase.objGerarReport.Autenticar(False)
            Dim Relatorios As Prosegur.Genesis.Report.RS2010.CatalogItem() = MyBase.objGerarReport.DisplayItems(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CarpetaReportes"))
            If ValidaDatos(Relatorios, Enumeradores.eAcao.Alta) Then

                Dim datos As IAC.Integracion.ContractoServicio.Reportes.ParametroReporteColeccion = RecuperaDatosControles(Enumeradores.eTipoOperacion.Grabar)

                If Not String.IsNullOrEmpty(IdentificadorConfiguracion) Then
                    If datos IsNot Nothing OrElse (Parametros Is Nothing OrElse Parametros.Count = 0) Then

                        Dim peticion As New IAC.Integracion.ContractoServicio.Reportes.SetConfiguracionReporte.Peticion
                        peticion.ConfiguracionesReportes = New IAC.Integracion.ContractoServicio.Reportes.ConfiguracionReporteColeccion


                        Dim objreportes As IAC.Integracion.ContractoServicio.Reportes.ReportesColeccion = Nothing


                        If ConfiguracionSelecionados IsNot Nothing AndAlso ConfiguracionSelecionados.Count > 0 Then

                            objreportes = New IAC.Integracion.ContractoServicio.Reportes.ReportesColeccion

                            For Each Valor In ConfiguracionSelecionados
                                objreportes.Add(New IAC.Integracion.ContractoServicio.Reportes.Reportes With { _
                                             .IdentificadorConfiguracionGeneral = Valor.OIDConfiguracionGeneral})
                            Next

                            peticion.ConfiguracionesReportes.Add(New IAC.Integracion.ContractoServicio.Reportes.ConfiguracionReporte With { _
                                                                 .CodUsuario = LoginUsuario, _
                                                                 .DesConfiguracion = txtDescripcion.Text, _
                                                                 .DesRuta = txtRuta.Text, _
                                                                 .FyhActualizacion = DateTime.Now, _
                                                                 .IdentificadorConfiguracion = IdentificadorConfiguracion, _
                                                                 .NombreArchivo = txtNombreArchivo.Text, _
                                                                 .ParametrosReporte = datos, _
                                                                 .Reportes = objreportes})

                            Dim objProxy As New Prosegur.Genesis.Comunicacion.ProxyIacIntegracion
                            Dim respuesta = objProxy.SetConfiguracionReporte(peticion)
                            If respuesta.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                                MyBase.MostraMensagem(respuesta.MensajeError)
                            Else
                                IdentificadorConfiguracion = respuesta.IdentificadorConfiguracion
                                scriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_sucesso", "alert('" & Traduzir("info_msg_grabadosucesso") & "');", True)
                            End If

                        End If

                    End If
                Else
                    If datos IsNot Nothing OrElse (Parametros Is Nothing OrElse Parametros.Count = 0) Then

                        Dim peticion As New IAC.Integracion.ContractoServicio.Reportes.SetConfiguracionReporte.Peticion
                        peticion.ConfiguracionesReportes = New IAC.Integracion.ContractoServicio.Reportes.ConfiguracionReporteColeccion

                        Dim objreportes As IAC.Integracion.ContractoServicio.Reportes.ReportesColeccion = Nothing

                        If ConfiguracionSelecionados IsNot Nothing AndAlso ConfiguracionSelecionados.Count > 0 Then

                            objreportes = New IAC.Integracion.ContractoServicio.Reportes.ReportesColeccion

                            For Each Valor In ConfiguracionSelecionados
                                objreportes.Add(New IAC.Integracion.ContractoServicio.Reportes.Reportes With { _
                                             .IdentificadorConfiguracionGeneral = Valor.OIDConfiguracionGeneral})
                            Next

                            peticion.ConfiguracionesReportes.Add(New IAC.Integracion.ContractoServicio.Reportes.ConfiguracionReporte With { _
                                                                 .CodUsuario = LoginUsuario, _
                                                                 .DesConfiguracion = txtDescripcion.Text, _
                                                                 .DesRuta = txtRuta.Text, _
                                                                 .FyhActualizacion = DateTime.Now, _
                                                                 .IdentificadorConfiguracion = IdentificadorConfiguracion, _
                                                                 .NombreArchivo = txtNombreArchivo.Text, _
                                                                 .ParametrosReporte = datos, _
                                                                 .Reportes = objreportes})

                            Dim objProxy As New Comunicacion.ProxyIacIntegracion
                            Dim respuesta = objProxy.SetConfiguracionReporte(peticion)
                            If respuesta.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                                MyBase.MostraMensagem(respuesta.MensajeError)
                            Else
                                IdentificadorConfiguracion = respuesta.IdentificadorConfiguracion
                                scriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_sucesso", "alert('" & Traduzir("info_msg_grabadosucesso") & "');", True)
                            End If

                        End If

                    End If
                End If
            End If
        Catch ex As Excepcion.NegocioExcepcion
            MostraMensagemExcecao(ex)
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Property ConfiguracionSelecionados As List(Of IAC.ContractoServicio.Configuracion.General.ConfiguracionGeneral)
        Get
            If ViewState("ConfiguracionSelecionados") Is Nothing Then
                Return New List(Of IAC.ContractoServicio.Configuracion.General.ConfiguracionGeneral)
            Else
                Return ViewState("ConfiguracionSelecionados")
            End If
        End Get
        Set(value As List(Of IAC.ContractoServicio.Configuracion.General.ConfiguracionGeneral))
            ViewState("ConfiguracionSelecionados") = value
        End Set
    End Property

    Protected Sub btnGenerarInforme_Click(sender As Object, e As EventArgs) Handles btnGenerarInforme.Click
        Try
            MyBase.objGerarReport.Autenticar(False)

            'Recupera os relatorios
            Dim Relatorios As Prosegur.Genesis.Report.RS2010.CatalogItem() = MyBase.objGerarReport.DisplayItems(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CarpetaReportes"))

            If ValidaDatos(Relatorios, Enumeradores.eAcao.Consulta) Then

                Dim datos = RecuperaDatosControles(Enumeradores.eTipoOperacion.Generar)
                Dim strRuta As String = String.Empty 'Ajustes

                If Not String.IsNullOrEmpty(txtRuta.Text.Trim) Then
                    Dim erro As New StringBuilder
                    Dim ReporteUrl As String = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("REPO_URL")
                    If String.IsNullOrEmpty(ReporteUrl) OrElse Not (ReporteUrl.StartsWith("\\")) Then
                        erro.Append(String.Format(Traduzir("WEBCONFIG_CHAVE_INVALIDA"), "Reportes_REPO_URL", ReporteUrl))
                    End If

                    Dim ReporteHome As String = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("REPO_HOME")
                    If String.IsNullOrEmpty(ReporteHome) OrElse Not (ReporteHome.StartsWith("\")) Then
                        erro.Append("</br>")
                        erro.Append(String.Format(Traduzir("WEBCONFIG_CHAVE_INVALIDA"), "Reportes_REPO_HOME", ReporteHome))
                    End If

                    If erro.Length > 0 Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, erro.ToString)
                    End If

                    strRuta = RetornarDiretorio(txtRuta.Text)
                Else
                    If InformacionUsuario Is Nothing OrElse InformacionUsuario.Nombre Is Nothing Then
                        Throw New Excepcion.NegocioExcepcion(Traduzir("002_msg_usuario_sessao_expirada"))
                    End If

                    strRuta = Path.Combine(Path.GetTempPath(), InformacionUsuario.Nombre)
                    If Directory.Exists(strRuta) Then
                        Directory.Delete(strRuta, True)
                    End If
                    Directory.CreateDirectory(strRuta)
                End If

                If datos IsNot Nothing OrElse (Parametros Is Nothing OrElse Parametros.Count = 0) Then
                    If ConfiguracionSelecionados IsNot Nothing AndAlso ConfiguracionSelecionados.Count > 0 Then

                        For Each Reportes In ConfiguracionSelecionados
                            Dim ReportesLocal = Reportes

                            Dim parametrosRePortesRecuperados As New List(Of Prosegur.Genesis.Report.ParametroReporte)
                            For Each param As Parametro In ParametrosReporte(Reportes.DesReporte)
                                Dim paramLocal = param
                                Dim achou = From p In datos
                                            Where p.CodNombreParametro = paramLocal.Name
                                            Select New Prosegur.Genesis.Report.ParametroReporte _
                                                          With {.CodParametro = paramLocal.NameOriginal,
                                                               .DesParametro = p.DesParametro,
                                                               .DesValorParametro = p.CodParametro}
                                If achou IsNot Nothing AndAlso achou.Count() > 0 Then
                                    parametrosRePortesRecuperados.AddRange(achou)
                                Else
                                    parametrosRePortesRecuperados.Add(New Prosegur.Genesis.Report.ParametroReporte With {
                                                                      .CodParametro = param.Name,
                                                                      .DesValorParametro = Nothing})
                                End If
                            Next

                            If parametrosRePortesRecuperados IsNot Nothing AndAlso ParametrosReporte.Count > 0 Then

                                Dim RelatoriosSelecionados As Prosegur.Genesis.Report.RS2010.CatalogItem() = (From p In Relatorios
                                               Where p.Name = ReportesLocal.DesReporte
                                               Select p).ToArray()

                                objGerarReport.GerarInforme(txtDescripcion.Text, Reportes.DesReporte, RelatoriosSelecionados, Reportes.FormatoArchivo, _
                                                        strRuta, txtDescripcion.Text, _
                                                        parametrosRePortesRecuperados, Reportes.CodReporte, txtNombreArchivo.Text, Reportes.ExtensionArchivo, Reportes.Separador)
                            End If

                        Next
                    End If

                    If Not String.IsNullOrEmpty(txtRuta.Text) Then
                        MyBase.MostraMensagem(Traduzir("info_msg_sucesso") & Constantes.LineBreak & String.Format(Traduzir("info_msg_ruta"), RetornarDiretorio(txtRuta.Text)))
                    Else

                        If Directory.GetFiles(strRuta).Count() > 0 Then
                            Session("caminoRuta") = strRuta
                            Session("NombreArchivoZip") = Constantes.CONST_REPORTES_NOMBRE_ZIP
                            ' Chama a página que exibirá o relatório
                            scriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_sucesso", "window.location.href = 'ConfiguracionReportMostrar.aspx'", True)
                        End If
                    End If
                End If

            End If
        Catch ex As Excepcion.NegocioExcepcion
            MyBase.MostraMensagemExcecao(ex)
        Catch ex As Exception
            'Essa verificação está sendo feita porque o objecto Exception criado na classe Gerar é de outro tipo..
            If ex.ToString.Contains("NegocioExcepcion") Then
                MyBase.MostraMensagem(ex.Message)
            Else
                MyBase.MostraMensagemExcecao(ex)
            End If

        End Try
    End Sub

    ''' <summary>
    ''' Retorna o diretorio onde vai ser saldo o reporte
    ''' </summary>
    ''' <param name="DesRuta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetornarDiretorio(DesRuta As String) As String
        Dim ReporteUrl As String = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("REPO_URL")
        Dim ReporteHome As String = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("REPO_HOME")
        Dim Path As String = String.Empty

        If Not String.IsNullOrEmpty(ReporteUrl) Then
            ReporteUrl = ReporteUrl.Replace("\", "")
        End If

        If Not String.IsNullOrEmpty(ReporteHome) Then
            ReporteHome = ReporteHome.Replace("\", "")
        End If

        If Not (String.IsNullOrEmpty(ReporteUrl) AndAlso String.IsNullOrEmpty(ReporteHome) AndAlso String.IsNullOrEmpty(DesRuta)) Then
            Path = String.Format("\\{0}\{1}\{2}", ReporteUrl, ReporteHome, DesRuta)
        End If

        Return Path
    End Function


    Private Sub ControleOcorreuErro(sender As Object, e As ExcepcionEventArgs)
        MyBase.MostraMensagem(e.Erro)
    End Sub


    Private Function DataInValida(data As String) As Boolean
        Return String.IsNullOrEmpty(data) OrElse (Not data.Length >= 8 OrElse Not IsDate(data))
    End Function

    Private Function DataVazia(data As String) As Boolean
        Return String.IsNullOrEmpty(data) OrElse DataInValida(data)
    End Function

    Protected Overrides Sub AdicionarScripts()
        MyBase.AdicionarScripts()

        Dim post As New PostBackOptions(Me.Page)

        'Dim str = "SelecionarRegistroGridTipoCheckBox(""" & gdvReportes.ClientID & """, """ & ClientScript.GetPostBackEventReference(post) & """);"

        'scriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "checked", str, True)

        ConfigurarControles()

    End Sub

    Private Function ValidaNomeExtensao(PathRelatorio As String, nomeExtensao As String, campo As String) As Boolean
        Dim erros As List(Of String) = New List(Of String)
        Dim errosValidacao As List(Of String) = New List(Of String)
        Dim objReport As New Prosegur.Genesis.Report.Gerar()
        objReport.Autenticar(False)

        'Validar nome do arquivo
        If nomeExtensao.Trim.ToUpper <> "CSV" Then
            errosValidacao = objReport.ValidarNomeExtensao(PathRelatorio, nomeExtensao, campo)
            For Each item In errosValidacao
                erros.Add(item)
            Next
        End If

        If erros IsNot Nothing AndAlso erros.Count > 0 Then
            MyBase.MostraMensagem(String.Join("<br>", erros.ToArray()))
            Return False
        End If

        Return True

    End Function

    Private Function ValidaDatos(reportes As Prosegur.Genesis.Report.RS2010.CatalogItem(), acao As Enumeradores.eAcao) As Boolean
        Dim erros As List(Of String) = New List(Of String)

        If ConfiguracionSelecionados Is Nothing OrElse (ConfiguracionSelecionados IsNot Nothing AndAlso ConfiguracionSelecionados.Count = 0) Then
            erros.Add(Traduzir("027_msg_error_selecionar_reporte"))
        End If

        If String.IsNullOrEmpty(txtDescripcion.Text) AndAlso acao = Enumeradores.eAcao.Alta Then
            erros.Add(String.Format(Traduzir("err_campo_obrigatorio"), lblDescripcion.Text))
        End If


        If String.IsNullOrEmpty(txtNombreArchivo.Text) Then
            erros.Add(String.Format(Traduzir("err_campo_obrigatorio"), lblNombreArchivo.Text))
        End If

        If reportes IsNot Nothing AndAlso ConfiguracionSelecionados IsNot Nothing Then
            For Each reporte In ConfiguracionSelecionados
                Dim reporteLocal = reporte
                'Verifica se o reporte salvo na configuração existe no Report Server
                Dim existeReporte = (From r In reportes Where r.Name.Equals(reporteLocal.DesReporte)).FirstOrDefault
                If existeReporte Is Nothing Then
                    erros.Add(String.Format(Traduzir("024_msg_erro_reporte_nao_localizado"), reporte.DesReporte))
                End If
            Next
        End If

        If erros IsNot Nothing AndAlso erros.Count > 0 Then
            MyBase.MostraMensagem(String.Join("<br>", erros.ToArray()))
            Return False
        Else
            For Each item In ConfiguracionSelecionados
                If Not ValidaNomeExtensao(item.Path, txtNombreArchivo.Text, lblNombreArchivo.Text) Then
                    Return False
                End If
            Next
        End If
        Return True
    End Function

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        If ConfiguracionSelecionados IsNot Nothing Then
            ConfiguracionSelecionados.Clear()
        End If
        PopularRelatorios()
        upPrincipal.Update()
        Me.RecuperarParametros()
        tabelaCampos.Controls.Clear()
        tabelaCampos1.Controls.Clear()
        txtDescripcion.Text = String.Empty
        txtNombreArchivo.Text = String.Empty
        txtRuta.Text = String.Empty
        hdReportes.Value = String.Empty
    End Sub


    Protected Overrides Sub TraduzirControles()

        MyBase.TraduzirControles()
        lblDescripcion.Text = Traduzir("024_lblDescripcion")
        lblNombreArchivo.Text = Traduzir("024_lblNombreArchivo")
        lblTituloConfiguracionReportes.Text = Traduzir("mn_reportes")
        lblFiltros.Text = Traduzir("027_lblFiltros")

        btnLimpiar.Text = Traduzir("btnLimpiar")
        btnGrabar.Text = Traduzir("btnGrabar")
        btnGenerarInforme.Text = Traduzir("024_btnGenerarInforme")


        'gvReportes.Columns(0).Caption = Traduzir("026_col_selecionar")
        gvReportes.Columns(1).Caption = Traduzir("026_col_DesReporte")
        gvReportes.Columns(2).Caption = Traduzir("026_col_CodReporte")
        gvReportes.Columns(3).Caption = Traduzir("026_col_FormatoArchivo")
        gvReportes.Columns(4).Caption = Traduzir("026_col_ExtensionArchivo")
        gvReportes.Columns(5).Caption = Traduzir("026_col_Separador")

        gvReportes.SettingsPager.Summary.Text = Traduzir("pagerFormatGrid")
        gvReportes.SettingsText.EmptyDataRow = Traduzir("info_msg_grd_vazio")

    End Sub

    Private Sub DadosInventario(ByRef ParametrosControles As IAC.Integracion.ContractoServicio.Reportes.ParametroReporteColeccion, Optional mostraMsg As Boolean = True)

        'Verifica o campo obrigatório foi selecionado.
        Dim linha = tabelaCampos.FindControl(String.Format("row_{0}", CONST_P_CODIGO_INVENTARIO))
        Dim inventario As ucInventario = linha.FindControl(CONST_P_CODIGO_INVENTARIO)

        If inventario IsNot Nothing Then
            Dim erro As String = String.Empty
            Dim valor = inventario.recuperarSelecionado(erro)
            If Not (String.IsNullOrEmpty(valor)) Then
                ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte _
                                        With {.CodNombreParametro = CONST_P_CODIGO_INVENTARIO, _
                                            .DesParametro = inventario.DescricaoSetor, _
                                            .CodParametro = valor})
            Else
                If mostraMsg Then
                    MyBase.MostraMensagem(erro)
                End If
            End If

            'Verifica se o parametro de ordenanação existe no relatório
            Dim param As Parametro = Parametros.Where(Function(p) p.Name.ToUpper = CONST_P_ORDENACAO).FirstOrDefault

            If param IsNot Nothing Then
                ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte _
                                        With {.CodNombreParametro = param.NameOriginal, _
                                           .CodParametro = inventario.recuperarOrdenacao()})
            End If

            param = Parametros.Where(Function(p) p.Name.ToUpper = CONST_P_LOGIN).FirstOrDefault
            If param IsNot Nothing Then
                ParametrosControles.Add(New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte _
                                        With {.CodNombreParametro = param.NameOriginal, _
                                           .CodParametro = InformacionUsuario.Nombre})
            End If
        End If
    End Sub

    Protected Sub gvReportes_OnHtmlRowCreated(sender As Object, e As ASPxGridViewTableRowEventArgs)
        Try
            If (e.RowType = DevExpress.Web.ASPxGridView.GridViewRowType.Data) Then

                'Seta as propriedades do radio button

                Dim rbSelecionado As HtmlInputCheckBox = CType(gvReportes.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "ckSelecionado"), HtmlInputCheckBox)
                rbSelecionado.Value = gvReportes.GetRowValues(e.VisibleIndex, gvReportes.KeyFieldName).ToString()
                Dim jsScript As String = "javascript: AddRemovIdSelect2(this,'" & hdReportes.ClientID & "',false,''); ExecutarClick('" & btnConfEscogido.ClientID & "');"
                rbSelecionado.Attributes.Add("onclick", jsScript)

                Dim existe = (From p In reportesSelecionados
                       Where p.Equals(gvReportes.GetRowValues(e.VisibleIndex, gvReportes.KeyFieldName).ToString())
                       Select p).ToList()
                If existe IsNot Nothing AndAlso existe.Count() = 1 Then
                    rbSelecionado.Checked = True
                End If

                Dim desabilita = (From p In lstDesabilitar
                       Where p.Equals(gvReportes.GetRowValues(e.VisibleIndex, "DesReporte").ToString().ToUpper())
                       Select p).ToList()

                If desabilita IsNot Nothing AndAlso desabilita.Count() > 0 Then
                    rbSelecionado.Attributes.Add("disabled", "disabled")
                Else
                    rbSelecionado.Attributes.Remove("disabled")
                End If

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub gvReportes_OnPageIndexChanged(sender As Object, e As EventArgs)
        PopularRelatorios()
    End Sub

    Private Sub btnConfEscogido_Click(sender As Object, e As EventArgs) Handles btnConfEscogido.Click
        Try
            If tabelaCampos.Controls.Count > 0 OrElse tabelaCampos1.Controls.Count > 0 Then
                Me.DatosControles = RecuperaDatosControles(Enumeradores.eTipoOperacion.PostBack)
            Else
                Me.DatosControles = Nothing
            End If
            tabelaCampos.Controls.Clear()
            tabelaCampos1.Controls.Clear()
            ConfiguracionSelecionados = New List(Of IAC.ContractoServicio.Configuracion.General.ConfiguracionGeneral)
            If reportesSelecionados.Count > 0 Then
                For Each a In reportesSelecionados
                    Dim conf As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Configuracion.General.ConfiguracionGeneral = ConfiguracionesGenerales.FirstOrDefault(Function(x) x.OIDConfiguracionGeneral = a)
                    If conf IsNot Nothing Then
                        ConfiguracionSelecionados.Add(conf)
                    End If
                Next
            End If
            Me.RecuperarParametros()
            Me.DesabilitaRelatorios()
            Me.CriarControles()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
End Class