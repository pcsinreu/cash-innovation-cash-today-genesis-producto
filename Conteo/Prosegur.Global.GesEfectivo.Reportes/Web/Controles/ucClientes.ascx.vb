Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Framework.Dicionario.Tradutor
Imports IacUtilidad = Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad
Imports Prosegur.Genesis

Public Class ucClientes
    Inherits PopupBase

#Region "[CONSTRUTORES]"

    Public Sub New()
        TipoDeBusca = ucClientes.ETipoBusca.Cliente
    End Sub

#End Region

#Region "[ENUMERADORES]"

    Public Enum ETipoBusca
        Cliente
        SubCliente
        PontoServico
    End Enum

    Public Property CodigoGrid As String
        Get
            Return CType(Session("Codigo_" & Me.ID), String)
        End Get
        Set(value As String)
            Session("Codigo_" & Me.ID) = value
        End Set
    End Property

    Public Property DescricaoGrid As String
        Get
            Return CType(Session("Descricao_" & Me.ID), String)
        End Get
        Set(value As String)
            Session("Descricao_" & Me.ID) = value
        End Set
    End Property

#End Region

#Region "[PROPRIEDADES]"

    Private Property ClientesSelecionados() As IAC.ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion
        Get
            If ViewState("ClientesSelecionados") Is Nothing Then
                ViewState("ClientesSelecionados") = New IAC.ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion
            End If
            Return ViewState("ClientesSelecionados")
        End Get
        Set(value As IAC.ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion)
            ViewState("ClientesSelecionados") = value
        End Set
    End Property

    Private Property SubClienteSelecionados As IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.ClienteColeccion
        Get
            If ViewState("SubClienteSelecionados") Is Nothing Then
                ViewState("SubClienteSelecionados") = New IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.ClienteColeccion
            End If
            Return ViewState("SubClienteSelecionados")
        End Get
        Set(value As IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.ClienteColeccion)
            ViewState("SubClienteSelecionados") = value
        End Set
    End Property

    Private Property PtoServicoSelecionados As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion
        Get
            If ViewState("PtoServicoSelecionados") Is Nothing Then
                ViewState("PtoServicoSelecionados") = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion
            End If
            Return ViewState("PtoServicoSelecionados")
        End Get
        Set(value As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion)
            ViewState("PtoServicoSelecionados") = value
        End Set
    End Property

    Public Property MultiSelecaoPuntoServicio As Boolean
        Get
            Return Session(ID & "_MultiSelecaoPuntoServicio")
        End Get
        Set(value As Boolean)
            Session(ID & "_MultiSelecaoPuntoServicio") = value
        End Set
    End Property

    Public Property MultiSelecaoSubCliente As Boolean
        Get
            Return Session(ID & "_MultiSelecaoSubCliente")
        End Get
        Set(value As Boolean)
            Session(ID & "_MultiSelecaoSubCliente") = value
        End Set
    End Property

    Public Property TipoDeBusca As ETipoBusca
        Get
            Return ViewState("BuscaCliente")
        End Get
        Private Set(value As ETipoBusca)
            ViewState("BuscaCliente") = value
        End Set
    End Property

    Public Property MultiSelecaoCliente As Boolean
        Get
            Return Session(ID & "_MultiSelecaoCliente")
        End Get
        Set(value As Boolean)
            Session(ID & "_MultiSelecaoCliente") = value
        End Set
    End Property

    Public Property PropTxtCodigo As TextBox
        Get
            Return Me.txtCodigo
        End Get
        Set(value As TextBox)
            value = Me.txtCodigo
        End Set
    End Property

    Public Property ExisteListado As Boolean
        Get
            If ViewState(ID & "_ExisteListado") Is Nothing Then
                ViewState(ID & "_ExisteListado") = True
            End If
            Return ViewState(ID & "_ExisteListado")
        End Get
        Set(value As Boolean)
            ViewState(ID & "_ExisteListado") = value
        End Set
    End Property

    Private Property lstMarcados As IEnumerable(Of String)
        Get
            Dim separador As String() = {"|"}
            Dim arrIdentificador As String() = hdnSelecionado.Value.Split(separador, StringSplitOptions.RemoveEmptyEntries)
            Session(ID & "_lstMarcados") = arrIdentificador.ToList()
            Return CType(Session(ID & "_lstMarcados"), IEnumerable(Of String)).Distinct()
        End Get
        Set(value As IEnumerable(Of String))
            Session(ID & "_lstMarcados") = value
        End Set
    End Property
#End Region

#Region "[METODOS]"

    Private Sub PreencherGridClientes(sender As Object)
        Try
            Dim objRespuesta As IAC.ContractoServicio.Utilidad.GetComboClientes.Respuesta = GetComboClientes()
            Dim objDt As DataTable = Nothing

            If objRespuesta.Clientes IsNot Nothing _
                    AndAlso objRespuesta.Clientes.Count > 0 Then

                ' Se existe apena um cliente
                ' converter objeto para datatable
                Dim j = (From p In objRespuesta.Clientes
                        Select Codigo = p.Codigo, Descripcion = p.Descripcion, _
                        CodigoAjeno = If(IsDBNull(p.CodigoAjenoCliente) OrElse p.CodigoAjenoCliente Is Nothing, String.Empty, p.CodigoAjenoCliente), _
                        DescripcionAjeno = If(IsDBNull(p.DescripcionAjenoCliente) OrElse p.DescripcionAjenoCliente Is Nothing, String.Empty, p.DescripcionAjenoCliente)).ToList()

                objDt = Util.ConverterListParaDataTable(j)
                
                    objDt.DefaultView.Sort = " codigo asc"
                    
                    gvDatos.DataSource = objDt
                    gvDatos.DataBind()

            Else

                gvDatos.DataSource = Nothing
                gvDatos.DataBind()

            End If
        Catch ex As Excepcion.NegocioExcepcion
            Me.NotificarErro(ex)
        Catch ex As Exception
            Me.NotificarErro(ex)
        End Try
    End Sub

    Private Function GetComboClientes() As IAC.ContractoServicio.Utilidad.GetComboClientes.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboClientes.Peticion
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objPeticion.Vigente = True

        If Not String.IsNullOrEmpty(CodigoGrid) Then
            objPeticion.Codigo = New List(Of String)
            objPeticion.Codigo.Add(CodigoGrid)
        End If

        If Not String.IsNullOrEmpty(DescricaoGrid) Then
            objPeticion.Descripcion = New List(Of String)
            objPeticion.Descripcion.Add(DescricaoGrid)
        End If

        ' criar objeto proxy
        Dim objProxy As New Comunicacion.ProxyUtilidad

        ' chamar servicio
        Return objProxy.GetComboClientes(objPeticion)

    End Function

    ''' <summary>
    ''' Preenche o grid de subclientes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PreencherGridSubClientes(sender As Object)
        Try
            ' obter clientes
            Dim objRespuesta As IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.Respuesta = GetComboSubClientes()

            ' validar se encontrou subclientes
            If objRespuesta.Clientes IsNot Nothing AndAlso _
               objRespuesta.Clientes.Count > 0 AndAlso _
               objRespuesta.Clientes(0).SubClientes IsNot Nothing AndAlso _
               objRespuesta.Clientes(0).SubClientes.Count > 0 Then


                Dim i = (From p In objRespuesta.Clientes
                         From s In p.SubClientes
                        Select Codigo = s.Codigo, Descripcion = s.Descripcion, _
                        CodigoAjeno = If(IsDBNull(s.CodigoAjenoSubCliente) OrElse s.CodigoAjenoSubCliente Is Nothing, String.Empty, s.CodigoAjenoSubCliente), _
                        DescripcionAjeno = If(IsDBNull(s.DescripcionAjenoSubCliente) OrElse s.DescripcionAjenoSubCliente Is Nothing, String.Empty, s.DescripcionAjenoSubCliente)).ToList()

                ' converter objeto para datatable
                Dim objDt As DataTable = Util.ConverterListParaDataTable(i)


                objDt.DefaultView.Sort = " Codigo asc"

                gvDatos.DataSource = objDt
                gvDatos.DataBind()
                
            Else

                'Limpa a consulta
                gvDatos.DataSource = Nothing
                gvDatos.DataBind()

            End If
        Catch ex As Excepcion.NegocioExcepcion
            Me.NotificarErro(ex)
        Catch ex As Exception
            Me.NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Obtém o combo de subclientes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetComboSubClientes() As IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.Peticion

        objPeticion.CodigosClientes = New List(Of String)
        Dim clientes = (From p In ClientesSelecionados
                       Select p.Codigo).ToList()
        objPeticion.CodigosClientes = clientes



        If Not String.IsNullOrEmpty(CodigoGrid) Then
            objPeticion.CodigoSubcliente = CodigoGrid
        End If

        If Not String.IsNullOrEmpty(DescricaoGrid) Then
            objPeticion.DescripcionSubcliente = DescricaoGrid
        End If

        ' criar objeto proxy
        Dim objProxy As New Comunicacion.ProxyUtilidad

        ' chamar servicio
        Return objProxy.GetComboSubclientesByCliente(objPeticion)

    End Function

    ''' <summary>
    ''' Obtém o combo de puntos de servicios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetComboPuntoServicio() As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Respuesta

        ' criar objeto proxy
        Dim objProxy As New Comunicacion.ProxyUtilidad

        Dim teste As New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Peticion
        
        Dim clientes As New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion
        clientes.AddRange(From c In SubClienteSelecionados
                          From s In c.SubClientes
                          Select New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Cliente With { _
                              .Codigo = c.Codigo, .Descripcion = c.Descripcion, .SubClientes = RetornaSubClientesSelecionados(s)
                                                                             })

        ' chamar servicio
        Return objProxy.GetComboPuntosServiciosByClientesSubclientes( _
            New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Peticion With {.Clientes = clientes})
    End Function

    ''' <summary>
    ''' Preenche o grid de puntosservicio
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PreencherGridPuntosServicio(sender As Object)
        Try
            ' obter puntos de servicio
            Dim objRespuesta As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Respuesta = GetComboPuntoServicio()

            ' validar se encontrou subclientes
            If objRespuesta.Clientes IsNot Nothing AndAlso _
               objRespuesta.Clientes.Count > 0 Then

                Dim resultado = (From c In objRespuesta.Clientes
                                From s In c.SubClientes
                                From p In s.PuntosServicio
                                Select Codigo = p.Codigo, Descripcion = p.Descripcion, _
                                CodigoAjeno = If(IsDBNull(p.CodigoAjenoPuntoServicio) OrElse _
                                                 String.IsNullOrEmpty(p.CodigoAjenoPuntoServicio), String.Empty, p.CodigoAjenoPuntoServicio), _
                                DescripcionAjeno = If(IsDBNull(p.DescripcionAjenoPuntoServicio) OrElse _
                                                               String.IsNullOrEmpty(p.DescripcionAjenoPuntoServicio), String.Empty, _
                                                               p.DescripcionAjenoPuntoServicio) _
                                                      ).ToList()

                ' converter objeto para datatable
                Dim objDt As DataTable = Util.ConverterListParaDataTable(resultado)


                objDt.DefaultView.Sort = " codigo asc"
                gvDatos.DataSource = objDt
                gvDatos.DataBind()
                
            Else

                'Limpa a consulta
                gvDatos.DataSource = Nothing
                gvDatos.DataBind()

            End If
        Catch ex As Excepcion.NegocioExcepcion
            Me.NotificarErro(ex)
        Catch ex As Exception
            Me.NotificarErro(ex)
        End Try
    End Sub

    Private Function RetornaSubClientesSelecionados(s As IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente) As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.SubClienteColeccion
        Try
            Dim subClientes As New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.SubClienteColeccion
            If Not String.IsNullOrEmpty(CodigoGrid) OrElse Not String.IsNullOrEmpty(DescricaoGrid) Then
                Dim puntoServico As New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.PuntoServicio _
                With {.Codigo = CodigoGrid, _
                      .Descripcion = DescricaoGrid}
                Dim subCliente1 As New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.SubCliente With { _
                            .Codigo = s.Codigo, .Descripcion = s.Descripcion, _
                            .PuntosServicio = New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.PuntoServicioColeccion}
                subCliente1.PuntosServicio.Add(puntoServico)
                subClientes.Add(subCliente1)
            End If

            Dim subCliente As New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.SubCliente With { _
                            .Codigo = s.Codigo, .Descripcion = s.Descripcion}
            subClientes.Add(subCliente)
            Return subClientes
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub TraduzirControlesLocal()
        Select Case TipoDeBusca
            Case ETipoBusca.Cliente
                Titulo = Traduzir("028_tituloCliente")
                lblTitulo.Text = Traduzir("028_lblCliente")
            Case ETipoBusca.SubCliente
                Titulo = Traduzir("028_tituloSubCliente")
                lblTitulo.Text = Traduzir("028_lblSubcliente")
            Case ETipoBusca.PontoServico
                Titulo = Traduzir("028_tituloPtoServico")
                lblTitulo.Text = Traduzir("028_lblPuntoServicio")
        End Select

        'Tradução Grid
        gvDatos.Columns(1).Caption = Traduzir("028_lblCodigo")
        gvDatos.Columns(2).Caption = Traduzir("028_lblDescricao")
        gvDatos.Columns(3).Caption = Traduzir("028_lblCodigoAlheio")
        gvDatos.Columns(4).Caption = Traduzir("028_lblDescricaoAlheio")

        gvDatos.SettingsPager.Summary.Text = Traduzir("pagerFormatGrid")
        gvDatos.SettingsText.EmptyDataRow = Traduzir("info_msg_grd_vazio")

        btnCancelar.Text = Traduzir("btnCancelar")
        btnAceptar.Text = Traduzir("btnAceitar")

    End Sub

    Private Sub RegistrarScripts()
        Try
            txtCodigo.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ID & "_img', event);")
            txtDescricao.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        Catch ex As Excepcion.NegocioExcepcion
            Me.NotificarErro(ex)
        Catch ex As Exception
            Me.NotificarErro(ex)
        End Try
    End Sub

    Private Sub LimparCampos()
        Try
            'Limpa a consulta
            gvDatos.DataSource = Nothing
            gvDatos.DataBind()
            txtCodigo.Text = String.Empty
            txtDescricao.Text = String.Empty
            hdnSelecionado.Value = String.Empty
            CodigoGrid = String.Empty
            DescricaoGrid = String.Empty

        Catch ex As Excepcion.NegocioExcepcion
            Me.NotificarErro(ex)
        Catch ex As Exception
            Me.NotificarErro(ex)
        End Try
    End Sub

    Private Sub GuardaSelecionadoGrid()
        Try

            If lstMarcados.Count > 0 Then
                ClientesSelecionados.Clear()
                Dim sel As New IAC.ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion
                For Each a In lstMarcados
                    CodigoGrid = a
                    Dim objRespuesta As IAC.ContractoServicio.Utilidad.GetComboClientes.Respuesta = GetComboClientes()
                    If objRespuesta IsNot Nothing AndAlso objRespuesta.Clientes.Count > 0 Then
                        Dim cliente As IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente = objRespuesta.Clientes.FirstOrDefault(Function(x) x.Codigo.Equals(a))
                        If cliente IsNot Nothing Then
                            sel.Add(cliente)
                        End If
                    End If
                Next
                ClientesSelecionados = sel
            Else
                ClientesSelecionados.Clear()
            End If
           
        Catch ex As Excepcion.NegocioExcepcion
            Me.NotificarErro(ex)
        Catch ex As Exception
            Me.NotificarErro(ex)
        End Try
    End Sub

    Private Sub GuardaSelecionadosSubCliente()
        
        Try
            If lstMarcados.Count > 0 Then
                SubClienteSelecionados.Clear()
                Dim sel As New IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.ClienteColeccion
                For Each a In lstMarcados
                    CodigoGrid = a
                    Dim objRespuesta As IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.Respuesta = GetComboSubClientes()
                    If objRespuesta IsNot Nothing AndAlso objRespuesta.Clientes.Count > 0 Then
                        Dim cliente As IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.Cliente = objRespuesta.Clientes.FirstOrDefault(Function(x) (From f In x.SubClientes Where f.Codigo.Equals(a)).Any())
                        If cliente IsNot Nothing Then
                            sel.Add(cliente)
                        End If
                    End If
                Next
                SubClienteSelecionados = sel
            Else
                SubClienteSelecionados.Clear()
            End If
           
        Catch ex As Excepcion.NegocioExcepcion
            Me.NotificarErro(ex)
        Catch ex As Exception
            Me.NotificarErro(ex)
        End Try

    End Sub

    Private Sub GuardaSelecionadosPtoServico()

        Try
            If lstMarcados.Count > 0 Then
                PtoServicoSelecionados.Clear()
                Dim sel As New IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion
                For Each a In lstMarcados
                    CodigoGrid = a
                    Dim objRespuesta As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Respuesta = GetComboPuntoServicio()
                    If objRespuesta IsNot Nothing AndAlso objRespuesta.Clientes.Count > 0 Then

                        objRespuesta.Clientes.FirstOrDefault.SubClientes.RemoveAll(Function(x) x.PuntosServicio.RemoveAll(Function(xx) xx.Codigo <> a) AndAlso
                                                                                               (x.PuntosServicio Is Nothing OrElse x.PuntosServicio.Count = 0))

                        Dim cliente As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Cliente = (From C In objRespuesta.Clientes From s In C.SubClientes From p In s.PuntosServicio Where p.Codigo.Equals(a) Select C).FirstOrDefault()
                        If cliente IsNot Nothing Then
                            sel.Add(cliente)
                        End If
                    End If
                Next
                PtoServicoSelecionados = sel
            Else
                PtoServicoSelecionados.Clear()
            End If

        Catch ex As Excepcion.NegocioExcepcion
            Me.NotificarErro(ex)
        Catch ex As Exception
            Me.NotificarErro(ex)
        End Try

    End Sub
  

    Public Sub InicializarControle(ClientesSelecionados As IAC.ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion, _
                                   Optional TipoBusca As ETipoBusca = ucClientes.ETipoBusca.Cliente)
        Me.ClientesSelecionados = ClientesSelecionados
        InicializarControle(TipoBusca)
        
    End Sub

    Public Sub InicializarControle(ClientesSelecionados As IAC.ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion, _
                                   SubClienteSelecionados As IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.ClienteColeccion, _
                                   Optional TipoBusca As ETipoBusca = ucClientes.ETipoBusca.Cliente)

        Me.SubClienteSelecionados = SubClienteSelecionados
        InicializarControle(ClientesSelecionados, TipoBusca)
  
    End Sub

    Public Sub InicializarControle(SubClienteSelecionados As IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.ClienteColeccion, _
                                   Optional TipoBusca As ETipoBusca = ucClientes.ETipoBusca.Cliente)

        Me.SubClienteSelecionados = SubClienteSelecionados
        Me.PtoServicoSelecionados = PtoServicoSelecionados
        InicializarControle(TipoBusca)
       
    End Sub

    Public Sub InicializarControle(SubClienteSelecionados As IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.ClienteColeccion, _
                                   PtoServicoSelecionados As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion, _
                                   Optional TipoBusca As ETipoBusca = ucClientes.ETipoBusca.Cliente)

        Me.SubClienteSelecionados = SubClienteSelecionados
        Me.PtoServicoSelecionados = PtoServicoSelecionados
        InicializarControle(TipoBusca)

    End Sub

    Public Sub InicializarControle(Optional TipoBusca As ETipoBusca = ucClientes.ETipoBusca.Cliente)
        Me.TipoDeBusca = TipoBusca
        RegistrarScripts()
        TraduzirControlesLocal()
        LimparCampos()
        Select Case TipoDeBusca
            Case ETipoBusca.Cliente
                If ClientesSelecionados IsNot Nothing AndAlso ClientesSelecionados.Count > 0 Then
                    For Each a In ClientesSelecionados
                        hdnSelecionado.Value &= a.Codigo & "|"
                    Next
                End If
                PreencherGridClientes(Nothing)
                gvDatos.PageIndex = 0
            Case ETipoBusca.SubCliente
                If SubClienteSelecionados IsNot Nothing AndAlso SubClienteSelecionados.Count > 0 Then
                    For Each a In SubClienteSelecionados
                        For Each b In a.SubClientes
                            hdnSelecionado.Value &= b.Codigo & "|"
                        Next
                    Next
                End If
                PreencherGridSubClientes(Nothing)
                gvDatos.PageIndex = 0
            Case ETipoBusca.PontoServico
                If PtoServicoSelecionados IsNot Nothing AndAlso PtoServicoSelecionados.Count > 0 Then
                    For Each a In PtoServicoSelecionados
                        For Each b In a.SubClientes
                            For Each c In b.PuntosServicio
                                hdnSelecionado.Value &= c.Codigo & "|"
                            Next
                        Next
                    Next
                End If
                PreencherGridPuntosServicio(Nothing)
                gvDatos.PageIndex = 0
        End Select

    End Sub

#End Region

#Region "[EVENTOS]"

    Private Sub Page_Fechado(sender As Object, e As PopupEventArgs) Handles Me.Fechado
        LimparCampos()
        ClientesSelecionados = Nothing
        SubClienteSelecionados = Nothing
        PtoServicoSelecionados = Nothing
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        RegistrarScripts()
        TraduzirControlesLocal()
        
    End Sub

#Region "[EVENTOS - GRID]"

    Protected Sub gvDatos_OnHtmlRowCreated(sender As Object, e As ASPxGridViewTableRowEventArgs)
        Try
            ' Preenche Cabeçalho das colunas do Grid.
            If e.RowType = DevExpress.Web.ASPxGridView.GridViewRowType.Header Then
                e.Row.Cells(0).Text = String.Empty
                e.Row.Cells(1).Text = Traduzir("014_codigo").Replace(":", "")
                e.Row.Cells(2).Text = Traduzir("014_descricion").Replace(":", "")
            End If

            ' Preenche linhas do Grid.
            If (e.RowType = DevExpress.Web.ASPxGridView.GridViewRowType.Data) Then
                Dim item As String = gvDatos.GetRowValues(e.VisibleIndex, "Codigo")
                Dim desc As String = gvDatos.GetRowValues(e.VisibleIndex, "Descripcion")
                Dim codigoAjeno As String = gvDatos.GetRowValues(e.VisibleIndex, "CodigoAjeno")
                Dim descripcionAjeno As String = gvDatos.GetRowValues(e.VisibleIndex, "DescripcionAjeno")

                Dim lblCodigoAjeno As Label = CType(gvDatos.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "lblCodigoAjeno"), Label)
                Dim lblDescripcionAjeno As Label = CType(gvDatos.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "lblDescripcionAjeno"), Label)

                Dim lblItemCodigo As Label = CType(gvDatos.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "lblItemCodigo"), Label)
                Dim lblItemDescricao As Label = CType(gvDatos.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "lblItemDescricao"), Label)


                lblItemCodigo.Text = If(item.Length > 20, String.Format("{0}...", item.Substring(0, 20)), item)
                lblItemCodigo.ToolTip = item

                lblItemDescricao.Text = If(desc.Length > 20, String.Format("{0}...", desc.Substring(0, 20)), desc)
                lblItemDescricao.ToolTip = desc

                lblCodigoAjeno.Text = If(codigoAjeno.Length > 20, String.Format("{0}...", codigoAjeno.Substring(0, 20)), codigoAjeno)
                lblCodigoAjeno.ToolTip = codigoAjeno

                lblDescripcionAjeno.Text = If(descripcionAjeno.Length > 20, String.Format("{0}...", descripcionAjeno.Substring(0, 20)), descripcionAjeno)
                lblDescripcionAjeno.ToolTip = descripcionAjeno


                Dim rbSelecionado As HtmlInputRadioButton = CType(gvDatos.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "rbSelecionado"), HtmlInputRadioButton)
                rbSelecionado.Value = gvDatos.GetRowValues(e.VisibleIndex, gvDatos.KeyFieldName).ToString()
                Dim jsScript As String = "javascript: $('.radio_selecao').not(this).each(function () { this.checked = false; }); AddRemovIdSelect2(this,'" & hdnSelecionado.ClientID & "',true,'" & btnAceptar.ClientID & "'); "
                rbSelecionado.Attributes.Add("onclick", jsScript)

                Dim chkSelecionado As HtmlInputCheckBox = CType(gvDatos.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "chkSelecionado"), HtmlInputCheckBox)
                chkSelecionado.Attributes.Add("value", gvDatos.GetRowValues(e.VisibleIndex, gvDatos.KeyFieldName).ToString())
                Dim jsScript2 As String = "javascript: AddRemovIdSelect2(this,'" & hdnSelecionado.ClientID & "',false,''); "
                chkSelecionado.Attributes.Add("onclick", jsScript2)

                Dim marcado As Boolean = (lstMarcados IsNot Nothing AndAlso lstMarcados.Count > 0 AndAlso lstMarcados.Where(Function(f) f = gvDatos.GetRowValues(e.VisibleIndex, gvDatos.KeyFieldName).ToString()).Count > 0)

                Select Case TipoDeBusca
                    Case ETipoBusca.Cliente
                        If (Me.MultiSelecaoCliente) Then
                            rbSelecionado.Visible = False
                            chkSelecionado.Visible = True
                            chkSelecionado.Checked = marcado
                        Else
                            rbSelecionado.Visible = True
                            chkSelecionado.Visible = False
                        End If
                    Case ETipoBusca.SubCliente
                        If (Me.MultiSelecaoSubCliente) Then
                            rbSelecionado.Visible = False
                            chkSelecionado.Visible = True
                            chkSelecionado.Checked = marcado
                        Else
                            rbSelecionado.Visible = True
                            chkSelecionado.Visible = False
                        End If
                    Case ETipoBusca.PontoServico
                        If (Me.MultiSelecaoPuntoServicio) Then
                            rbSelecionado.Visible = False
                            chkSelecionado.Visible = True
                            chkSelecionado.Checked = marcado
                        Else
                            rbSelecionado.Visible = True
                            chkSelecionado.Visible = False
                        End If
                End Select

            End If
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    Protected Sub gvDatos_OnPageIndexChanged(sender As Object, e As EventArgs)
        Select Case TipoDeBusca
            Case ETipoBusca.Cliente
                PreencherGridClientes(sender)
            Case ETipoBusca.SubCliente
                PreencherGridSubClientes(sender)
            Case ETipoBusca.PontoServico
                PreencherGridPuntosServicio(sender)
        End Select

    End Sub

    Protected Sub gvDatos_OnProcessOnClickRowFilter(sender As Object, e As ASPxGridViewOnClickRowFilterEventArgs)
        If e.Criteria.Any(Function(x) x.Key = "Codigo") Then
            Dim valCodigo As String = e.Values.FirstOrDefault(Function(x) x.Key = "Codigo").Value
            CodigoGrid = valCodigo
        End If
        If e.Criteria.Any(Function(x) x.Key = "Descripcion") Then
            Dim valDescricao As String = e.Values.FirstOrDefault(Function(x) x.Key = "Descripcion").Value
            DescricaoGrid = valDescricao
        End If

        Select Case TipoDeBusca
            Case ETipoBusca.Cliente
                PreencherGridClientes(sender)
            Case ETipoBusca.SubCliente
                PreencherGridSubClientes(sender)
            Case ETipoBusca.PontoServico
                PreencherGridPuntosServicio(sender)
        End Select
    End Sub

#End Region

#Region "[EVENTOS - BOTAO]"

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            gvDatos.DataSource = Nothing
            gvDatos.DataBind()

            Select Case TipoDeBusca
                Case ETipoBusca.Cliente
                    PreencherGridClientes(Nothing)
                Case ETipoBusca.SubCliente
                    PreencherGridSubClientes(Nothing)
                Case ETipoBusca.PontoServico
                    PreencherGridPuntosServicio(Nothing)
            End Select
        Catch ex As Excepcion.NegocioExcepcion
            Me.NotificarErro(ex)
        Catch ex As Exception
            Me.NotificarErro(ex)
        End Try
    End Sub

    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        LimparCampos()
        ClientesSelecionados = Nothing
        SubClienteSelecionados = Nothing
        PtoServicoSelecionados = Nothing
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Try
            Select Case TipoDeBusca
                Case ETipoBusca.Cliente
                    GuardaSelecionadoGrid()
                    Me.FecharPopup(ClientesSelecionados)
                Case ETipoBusca.SubCliente
                    GuardaSelecionadosSubCliente()
                    Me.FecharPopup(SubClienteSelecionados)
                Case ETipoBusca.PontoServico
                    GuardaSelecionadosPtoServico()
                    Me.FecharPopup(PtoServicoSelecionados)
                Case Else
                    Me.FecharPopup()
            End Select
        Catch ex As Excepcion.NegocioExcepcion
            Me.NotificarErro(ex)
        Catch ex As Exception
            Me.NotificarErro(ex)
     End Try
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        LimparCampos()
        ClientesSelecionados = Nothing
        SubClienteSelecionados = Nothing
        PtoServicoSelecionados = Nothing
        Me.FecharPopup()
    End Sub

#End Region

#End Region




    Private Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If MultiSelecaoCliente OrElse MultiSelecaoSubCliente OrElse MultiSelecaoPuntoServicio Then
            divBotoes.Attributes.Add("style", "display:block;")
        Else
            divBotoes.Attributes.Add("style", "display:none;")
        End If
    End Sub
End Class