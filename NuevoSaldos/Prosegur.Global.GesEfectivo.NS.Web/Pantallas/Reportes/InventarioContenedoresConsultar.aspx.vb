Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Report
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores
Imports Prosegur.Genesis
Imports Newtonsoft.Json
Imports Prosegur.Genesis.Comon.Extenciones.EnumExtension

Public Class InventarioContenedoresConsultar
    Inherits Base

#Region "[PROPRIEDADES]"
    Public Property Sectores As ObservableCollection(Of Clases.Sector)
        Get
            Return ucSectores.Sectores
        End Get
        Set(value As ObservableCollection(Of Clases.Sector))
            ucSectores.Sectores = value
        End Set
    End Property
    Public Property Plantas As ObservableCollection(Of Clases.Planta)
        Get
            Return ucSectores.Plantas
        End Get
        Set(value As ObservableCollection(Of Clases.Planta))
            ucSectores.Plantas = value
        End Set
    End Property
    Public Property Delegaciones As ObservableCollection(Of Clases.Delegacion)
        Get
            Return ucSectores.Delegaciones
        End Get
        Set(value As ObservableCollection(Of Clases.Delegacion))
            ucSectores.Delegaciones = value
        End Set
    End Property
    Public Property Clientes As ObservableCollection(Of Clases.Cliente)
        Get
            Return ucClientes.Clientes
        End Get
        Set(value As ObservableCollection(Of Clases.Cliente))
            ucClientes.Clientes = value
        End Set
    End Property

    Private WithEvents _ucClientes As ucCliente
    Public Property ucClientes() As ucCliente
        Get
            If _ucClientes Is Nothing Then
                _ucClientes = LoadControl("~\Controles\ucCliente.ascx")
                _ucClientes.ID = "ucClientes"
                AddHandler _ucClientes.Erro, AddressOf ErroControles
                phCliente.Controls.Add(_ucClientes)
            End If
            Return _ucClientes
        End Get
        Set(value As ucCliente)
            _ucClientes = value
        End Set
    End Property

    Private WithEvents _ucSectores As ucSector
    Public Property ucSectores() As ucSector
        Get
            If _ucSectores Is Nothing Then
                _ucSectores = LoadControl("~\Controles\ucSector.ascx")
                _ucSectores.ID = "ucSectores"
                AddHandler _ucSectores.Erro, AddressOf ErroControles
                phSector.Controls.Add(_ucSectores)
            End If
            Return _ucSectores
        End Get
        Set(value As ucSector)
            _ucSectores = value
        End Set
    End Property

#End Region

#Region "[EVENTOS]"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ConfigurarControle_Sector()
        ConfigurarControle_Cliente()

        If Not Me.IsPostBack Then
            AjustarControlFormato()
            AjustarControlDiscriminarPor()
            AnadirFechasIniciales()
            'Ao entrar na tela, carregar os dados inciais
            If InformacionUsuario.SectorSeleccionado IsNot Nothing Then
                'Al ingresar en la pantalla, el campo será cargado con los datos de la delegación logada.
                Me.ucSectores.Delegaciones = New ObservableCollection(Of Clases.Delegacion)
                Me.ucSectores.Delegaciones.Add(InformacionUsuario.DelegacionSeleccionada)
                Me.ucSectores.AtualizarRegistrosDelegacion()

                'Al ingresar en la pantalla, el campo será cargado con los datos de la planta logada.
                Me.ucSectores.Plantas = New ObservableCollection(Of Clases.Planta)
                Dim planta = InformacionUsuario.SectorSeleccionado.Planta.Clonar
                'Identificador pai da delegação
                planta.CodigoMigracion = InformacionUsuario.DelegacionSeleccionada.Identificador
                Me.ucSectores.Plantas.Add(planta)
                Me.ucSectores.AtualizarRegistrosPlanta()

                Me.ucSectores.Sectores = New ObservableCollection(Of Clases.Sector)

                'Al ingresar en la pantalla, el campo será cargado con los datos del sector padre primer nivel do sector logado.
                Dim sector = Prosegur.Genesis.LogicaNegocio.Genesis.Sector.ObtenerSectorPadrePrimerNivel(Base.InformacionUsuario.SectorSeleccionado.Identificador)

                'Identificador pai do sector
                sector.CodigoMigracion = planta.Identificador
                Me.ucSectores.Sectores.Add(sector)
                Me.ucSectores.AtualizarRegistrosSector()

                'Foco no próximo controle vazio
            Else
                Me.ucSectores.Focus()
            End If
        End If

    End Sub
    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        Try

            ValidarBuscar()

            Dim Peticion = New Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarInventarioContenedor.Peticion

            Peticion.CodigoUsuario = InformacionUsuario.Nombre
            Peticion.Inventario = New Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarInventarioContenedor.Inventario

            Peticion.Inventario.codInventario = IIf(String.IsNullOrEmpty(txtCodInventario.Text), Nothing, txtCodInventario.Text)
            Peticion.Inventario.fechaHoraInventarioDesde = Convert.ToDateTime(txtFechaInventarioDesde.Text)
            Peticion.Inventario.fechaHoraInventarioHasta = Convert.ToDateTime(txtFechaInventarioHasta.Text)

            If Me.Delegaciones IsNot Nothing AndAlso Me.Delegaciones.Count > 0 Then

                For Each objDelegacion In Delegaciones
                    Peticion.Inventario.Sector = New Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarInventarioContenedor.Sector
                    Peticion.Inventario.Sector.codDelegacion = objDelegacion.Codigo

                    If Me.Plantas IsNot Nothing AndAlso Me.Plantas.Count Then
                        For Each objPlanta In Plantas
                            Peticion.Inventario.Sector.codPlanta = objPlanta.Codigo

                            If Me.Sectores IsNot Nothing AndAlso Me.Sectores.Count > 0 Then
                                Dim ParamSector = String.Empty

                                For Each objSector In Me.Sectores
                                    If (ParamSector = String.Empty) Then
                                        ParamSector = objSector.Codigo
                                    Else
                                        ParamSector += "," + objSector.Codigo
                                    End If
                                Next

                                Peticion.Inventario.Sector.codSector = ParamSector

                            End If
                        Next
                    End If
                Next
            End If

            Dim ParamCliente = String.Empty

            If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then

                Peticion.Inventario.Cliente = New Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarInventarioContenedor.Cliente

                For Each objCliente In Clientes

                    If (ParamCliente = String.Empty) Then
                        ParamCliente = objCliente.Codigo
                    Else
                        ParamCliente += "," + objCliente.Codigo
                    End If

                    Dim ParamSubCliente = String.Empty

                    If objCliente.SubClientes IsNot Nothing AndAlso objCliente.SubClientes.Count > 0 Then

                        For Each objSubCliente In objCliente.SubClientes

                            If (ParamSubCliente = String.Empty) Then
                                ParamSubCliente = objSubCliente.Codigo
                            Else
                                ParamSubCliente += "," + objSubCliente.Codigo
                            End If

                            If objSubCliente.PuntosServicio IsNot Nothing AndAlso objSubCliente.PuntosServicio.Count > 0 Then

                                Dim ParamPuntoServicio = String.Empty
                                For Each objPtoServicio In objSubCliente.PuntosServicio
                                    If (ParamPuntoServicio = String.Empty) Then
                                        ParamPuntoServicio = objPtoServicio.Codigo
                                    Else
                                        ParamPuntoServicio += "," + objPtoServicio.Codigo
                                    End If
                                Next
                                Peticion.Inventario.Cliente.codPuntoServicio += IIf(String.IsNullOrEmpty(Peticion.Inventario.Cliente.codPuntoServicio), ParamPuntoServicio, "," + ParamPuntoServicio)
                            End If
                        Next
                        Peticion.Inventario.Cliente.codSubcliente += IIf(String.IsNullOrEmpty(Peticion.Inventario.Cliente.codSubcliente), ParamSubCliente, "," + ParamSubCliente)
                    End If
                Next
                Peticion.Inventario.Cliente.codCliente = ParamCliente
            End If

            Dim Respuesta = Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Contenedor.ConsultarInventarioContenedor(Peticion)

            CargarDataGrid(Respuesta)

            If Not String.IsNullOrEmpty(Respuesta.MensajeError) Then
                Throw New Excepciones.ExcepcionLogica(Respuesta.MensajeError)
            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub
    Private Sub btnGenerarReporte_Click(sender As Object, e As System.EventArgs) Handles btnGenerarReporte.Click
        Try
            GenerarReporte()
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub
    Protected Sub gdv_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvContenedor.RowDataBound

        Try
            If e.Row.RowType = DataControlRowType.Header Then

                e.Row.Cells(1).Text = Traduzir("077_HeaderSector")
                e.Row.Cells(2).Text = Traduzir("077_HeaderCliente")
                e.Row.Cells(3).Text = Traduzir("077_HeaderCodigoInventario")
                e.Row.Cells(4).Text = Traduzir("077_HeaderFechaHoraInvetario")
                e.Row.Cells(5).Text = Traduzir("077_HeaderCantiContTeóricos")
                e.Row.Cells(6).Text = Traduzir("077_HeaderCantiContInventariados")
                e.Row.Cells(7).Text = Traduzir("077_HeaderDiferencia")

            End If

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try

    End Sub
    Protected Sub gdv_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        sender.DataSource = ViewState("gdvDataSource")
        sender.PageIndex = e.NewPageIndex
        sender.DataBind()
    End Sub
    Protected Sub rbSelecionado_CheckedChanged(sender As Object, e As System.EventArgs)

        For index As Integer = 0 To gdvContenedor.Rows.Count - 1
            Dim rb As RadioButton = gdvContenedor.Rows(index).FindControl("rbSelecionado")
            If Not rb.Equals(sender) Then
                rb.Checked = False
            End If
        Next

    End Sub

#End Region

#Region "[HELPERS]"
    Protected Sub ConfigurarControle_Sector()
        Me.ucSectores.SolamenteSectoresPadre = True
        Me.ucSectores.SelecaoMultipla = True
        Me.ucSectores.SectorHabilitado = True
        Me.ucSectores.DelegacionHabilitado = True
        Me.ucSectores.PlantaHabilitado = True

        If Delegaciones IsNot Nothing Then
            Me.ucSectores.Delegaciones = Delegaciones
        End If
        If Plantas IsNot Nothing Then
            Me.ucSectores.Plantas = Plantas
        End If
        If Sectores IsNot Nothing Then
            Me.ucSectores.Sectores = Sectores
        End If

    End Sub
    Protected Sub ConfigurarControle_Cliente()

        Me.ucClientes.SelecaoMultipla = True
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.SubClienteHabilitado = True
        Me.ucClientes.PtoServicioHabilitado = True

        If Clientes IsNot Nothing Then
            Me.ucClientes.Clientes = Clientes
        End If

    End Sub

#End Region

#Region "[OVERRIDES]"
    Protected Overrides Sub DefinirParametrosBase()

        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.RELATORIO_CONTENEDORES
        MyBase.ValidarAcesso = True

    End Sub
    Protected Overrides Sub TraduzirControles()

        Me.Master.Titulo = Traduzir("077_CabeceraConsultar")
        Me.lblSubTitulo.Text = Traduzir("077_CabeceraConsultar")

        Me.ucFormato.Titulo = Traduzir("057_formato")
        Me.lblFechaInventarioDesde.Text = Traduzir("077_lblFechaInventarioDesde")
        Me.lblFechaInventarioHasta.Text = Traduzir("077_lblFechaInventarioHasta")
        Me.lblCodInventario.Text = Traduzir("077_lbl_CodigoInventario")
        Me.ucDiscriminarPor.Titulo = Traduzir("077_TituloUcDiscriminarPor")
        Me.chkNoConsiderarHijos.Text = Traduzir("077_TextChkNoConsiderarHijos")
        Me.lblResultadosBusqueda.Text = Traduzir("077_lblResultadoBusqueda")
        Me.btnBuscar.Text = Traduzir("btnBuscar")
        btnGenerarReporte.Text = Traduzir("005_btnGerar")

    End Sub
    Protected Overrides Sub AdicionarScripts()
        MyBase.AdicionarScripts()

        Dim script As String = String.Format("AbrirCalendario('{0}','{1}');", _
                                             txtFechaInventarioDesde.ClientID, "True")

        script &= String.Format("AbrirCalendario('{0}','{1}');", _
                                             txtFechaInventarioHasta.ClientID, "True")

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR", script, True)
    End Sub

#End Region

#Region "[MÉTODOS]"
    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagemErro(e.Erro.Message)
    End Sub
    Private Sub AjustarControlFormato()
        Dim lista As New List(Of KeyValuePair(Of String, String))
        lista.Add(New KeyValuePair(Of String, String)("PDF", Traduzir("056_formato_pdf")))
        lista.Add(New KeyValuePair(Of String, String)("EXCEL", Traduzir("056_formato_excel")))
        Me.ucFormato.Opciones = lista
    End Sub
    Private Sub AjustarControlDiscriminarPor()
        Dim lista As New List(Of KeyValuePair(Of String, String))
        lista.Add(New KeyValuePair(Of String, String)("S", Traduzir("075_OpcionDiscriminadoPorSector")))
        lista.Add(New KeyValuePair(Of String, String)("SCC", Traduzir("075_OpcionDiscriminadoPorSectorClienteYCanal")))
        Me.ucDiscriminarPor.Opciones = lista
    End Sub
    Private Function GenerarInventario() As String
        Dim Peticion = New Genesis.ContractoServicio.GenesisSaldos.Contenedores.GrabarInventarioContenedor.Peticion
        Peticion.Inventario = New Genesis.ContractoServicio.GenesisSaldos.Contenedores.GrabarInventarioContenedor.Inventario
        Peticion.Inventario.Sector = New Genesis.ContractoServicio.GenesisSaldos.Contenedores.GrabarInventarioContenedor.Sector
        Peticion.Inventario.Cliente = New Genesis.ContractoServicio.GenesisSaldos.Contenedores.GrabarInventarioContenedor.Cliente

        Peticion.Inventario.regresarDetalles = True
        Peticion.Inventario.codInventario = IIf(Not String.IsNullOrEmpty(txtCodInventario.Text), txtCodInventario.Text, Nothing)
        Peticion.Inventario.UsuarioCreacion = InformacionUsuario.Nombre

        If Me.Sectores IsNot Nothing AndAlso Me.Sectores.Count > 0 Then
            Dim ParamSector = String.Empty

            For Each objSector In Me.Sectores
                If (ParamSector = String.Empty) Then
                    ParamSector = objSector.Codigo
                Else
                    ParamSector += "," + objSector.Codigo
                End If
            Next

            Peticion.Inventario.Sector.codSector = ParamSector
        Else
            Peticion.Inventario.Sector.codSector = Nothing
        End If

        If Me.Delegaciones IsNot Nothing AndAlso Me.Delegaciones.Count > 0 Then
            Dim ParamDeleg = String.Empty

            For Each objDeleg In Me.Delegaciones
                If (ParamDeleg = String.Empty) Then
                    ParamDeleg = objDeleg.Codigo
                Else
                    ParamDeleg += "," + objDeleg.Codigo
                End If
            Next

            Peticion.Inventario.Sector.codDelegacion = ParamDeleg
        Else
            Peticion.Inventario.Sector.codDelegacion = Nothing
        End If

        If Me.Plantas IsNot Nothing AndAlso Me.Plantas.Count > 0 Then
            Dim ParamPlanta = String.Empty

            For Each objPlanta In Me.Plantas
                If (ParamPlanta = String.Empty) Then
                    ParamPlanta = objPlanta.Codigo
                Else
                    ParamPlanta += "," + objPlanta.Codigo
                End If
            Next

            Peticion.Inventario.Sector.codPlanta = ParamPlanta
        Else
            Peticion.Inventario.Sector.codPlanta = Nothing
        End If

        If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
            For Each objCliente In Clientes

                If objCliente.SubClientes IsNot Nothing AndAlso objCliente.SubClientes.Count > 0 Then

                    Peticion.Inventario.Cliente.codCliente = objCliente.Codigo

                    For Each objSubCliente In objCliente.SubClientes

                        If objSubCliente.PuntosServicio IsNot Nothing AndAlso objSubCliente.PuntosServicio.Count > 0 Then
                            Peticion.Inventario.Cliente.codSubcliente = objSubCliente.Codigo

                            For Each objPtoServicio In objSubCliente.PuntosServicio
                                Peticion.Inventario.Cliente.codPuntoServicio = objPtoServicio.Identificador
                            Next
                        Else
                            'se não escolheu punto de servicio então envia subcliente
                            Peticion.Inventario.Cliente.codSubcliente = objSubCliente.Identificador
                        End If
                    Next
                Else
                    'se não escolheu niveis de subcliente então envia o cliente..
                    Peticion.Inventario.Cliente.codCliente = objCliente.Identificador
                    Peticion.Inventario.Cliente.codSubcliente = Nothing
                    Peticion.Inventario.Cliente.codPuntoServicio = Nothing
                End If
            Next
        Else
            Peticion.Inventario.Cliente.codCliente = Nothing
            Peticion.Inventario.Cliente.codSubcliente = Nothing
            Peticion.Inventario.Cliente.codPuntoServicio = Nothing
        End If

        Dim Respuesta = Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Contenedor.GrabarInventarioContenedor(Peticion)
        If Not String.IsNullOrEmpty(Respuesta.MensajeError) Then
            Throw New Excepciones.ExcepcionLogica(Respuesta.MensajeError)
        End If

        Return Respuesta.codInventario
    End Function
    Private Sub GenerarReporte(Optional codInventario As String = Nothing)
        Try
            ValidarGerar()

            Dim ParamBusca = IIf(String.IsNullOrEmpty(codInventario), RecuperarSelecionadoGrid(), codInventario)
            Dim ParamDelegacion = InformacionUsuario.DelegacionSeleccionada.Identificador

            ' Recupera os parametros do relatório.
            Dim objReport As New Prosegur.Genesis.Report.Gerar()
            objReport.Autenticar(False)

            'Lista os parametros do relatório
            Dim dirRelatorio = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CarpetaReportes")
            Dim nomeRelatorio As String = IIf(ucDiscriminarPor.ItemSelecionado = "S", "rptInventarioContenedores", "rptInventarioContenedoresSector")
            Dim fullPathReport As String = Nothing

            ' Verificar se existe "/" no final da URL configurada para o Report
            If (dirRelatorio.Substring(dirRelatorio.Length - 1, 1) = "/") Then
                dirRelatorio = dirRelatorio.Substring(0, dirRelatorio.Length - 1)
            End If

            fullPathReport = String.Format("{0}/PACK_MODULAR/{1}", dirRelatorio, nomeRelatorio)

            Dim listaParametros As New List(Of RSE.ParameterValue)
            Dim bolCertificadoConsulta As Integer = 0

            'Recupera o pais da delegação que o usuário está logado.
            Dim pais As New Prosegur.Genesis.Comon.Clases.Pais
            If Base.InformacionUsuario.DelegacionSeleccionada IsNot Nothing Then
                pais = Prosegur.Genesis.LogicaNegocio.Genesis.Pais.ObtenerPaisPorDelegacion(Base.InformacionUsuario.DelegacionSeleccionada.Codigo)
            End If

            listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COM_IDIOMA", .Value = If(Tradutor.CulturaSistema IsNot Nothing AndAlso _
                                                                            Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name), _
                                                                            Tradutor.CulturaSistema.Name, _
                                                                            If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty))})
            listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_SECTOR_CLIENTE_CANAL", .Value = If(ucDiscriminarPor.ItemSelecionado = "S", False, True)})
            listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_CODINVENTARIO", .Value = ParamBusca})
            listaParametros.Add(New RSE.ParameterValue() With {.Name = "PARAM_DELEGACION", .Value = ParamDelegacion})


            Dim extensao = If(Me.ucFormato.ItemSelecionado = "PDF", "pdf", "xls")

            Dim Buffer = objReport.RenderReport(fullPathReport, Me.ucFormato.ItemSelecionado, listaParametros, extensao)
            Dim nomeArquivo = String.Format("{0}.{1}", Traduzir("077_Menu"), extensao)
            Session(nomeArquivo) = Buffer

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "download_relatorio", String.Format("window.location.href = '../Download.aspx?NOME_ARQUIVO={0}'", nomeArquivo), True)

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub
    Private Sub ValidarGerar()
        ucDiscriminarPor.GuardarDatos()
        ucFormato.GuardarDatos()

        If String.IsNullOrEmpty(RecuperarSelecionadoGrid()) Then
            Throw New Excepciones.ExcepcionLogica("Seleção de inventário obrigatória")
        End If
    End Sub
    Private Sub ValidarBuscar()
        If String.IsNullOrEmpty(txtFechaInventarioDesde.Text) OrElse String.IsNullOrEmpty(txtFechaInventarioHasta.Text) Then
            Throw New Excepciones.ExcepcionLogica("Data de início e fim deve ser selecionado")
        End If
    End Sub
    Private Sub CargarDataGrid(respuesta As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarInventarioContenedor.Respuesta)

        Dim dt = New DataTable("INVENTARIO")

        dt.Columns.Add(New DataColumn("Sector"))
        dt.Columns.Add(New DataColumn("Cliente"))
        dt.Columns.Add(New DataColumn("CodigoInventario"))
        dt.Columns.Add(New DataColumn("FechaInventario"))
        dt.Columns.Add(New DataColumn("CantContTeorico"))
        dt.Columns.Add(New DataColumn("CantContInventariados"))
        dt.Columns.Add(New DataColumn("Diferencia"))

        If Not IsNothing(respuesta.inventarios) AndAlso respuesta.inventarios.Count > 0 Then
            Dim cont As Integer = 0
            For Each inventario As ConsultarInventarioContenedor.InventarioRespuesta In respuesta.inventarios

                Dim DataRow = dt.NewRow()
                If inventario.Sector IsNot Nothing Then
                    DataRow("Sector") = inventario.Sector.codSector + " - " + inventario.Sector.desSector
                End If

                If respuesta.contenedores IsNot Nothing AndAlso respuesta.contenedores.Count > cont AndAlso
                   respuesta.contenedores(cont).Cuentas IsNot Nothing AndAlso respuesta.contenedores(cont).Cuentas.Count > 0 AndAlso
                   respuesta.contenedores(cont).Cuentas(0).Cliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(respuesta.contenedores(cont).Cuentas(0).Cliente.codCliente) Then
                    DataRow("Cliente") = respuesta.contenedores(cont).Cuentas(0).Cliente.codCliente + " - " + respuesta.contenedores(cont).Cuentas(0).Cliente.desCliente
                End If

                DataRow("CodigoInventario") = inventario.codInventario
                DataRow("FechaInventario") = inventario.fechaHoraInventario
                DataRow("CantContTeorico") = inventario.cantidadeLogica
                DataRow("CantContInventariados") = inventario.cantidadeInventariada
                DataRow("Diferencia") = inventario.cantidadeLogica - inventario.cantidadeInventariada

                dt.Rows.Add(DataRow)
                cont += 1
            Next

        End If

        gdvContenedor.DataSource = dt
        ViewState("gdvDataSource") = dt

        gdvContenedor.DataBind()
    End Sub
    Public Function RecuperarSelecionadoGrid() As String
        Dim ParamBusca As String = String.Empty

        Dim selecionados = (From p In gdvContenedor.Rows.Cast(Of GridViewRow)() _
        Where DirectCast(p.Cells(0).FindControl("rbSelecionado"), RadioButton).Checked _
        Select Convert.ToInt32(p.RowIndex)).ToList
        If selecionados.Count > 0 Then
            ParamBusca = gdvContenedor.DataKeys(selecionados(0)).Values(0)
        End If

        Dim Precintos = ParamBusca.Split("|")

        ParamBusca = String.Empty
        For Each precinto As String In Precintos
            If String.IsNullOrEmpty(ParamBusca) Then
                ParamBusca += precinto.Trim()
            Else
                ParamBusca += "|" + precinto.Trim()
            End If
        Next

        Return ParamBusca
    End Function
    Private Sub AnadirFechasIniciales()
        txtFechaInventarioDesde.Text = DateTime.Now.Date.AddMonths(-1).ToString()
        txtFechaInventarioHasta.Text = DateTime.Now.AddDays(+1).AddSeconds(-1).ToString()
    End Sub

#End Region

End Class