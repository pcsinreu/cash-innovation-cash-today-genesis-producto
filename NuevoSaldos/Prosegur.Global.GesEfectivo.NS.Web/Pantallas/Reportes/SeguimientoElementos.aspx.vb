Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Report
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones.EnumExtension
Imports Prosegur.Framework.Dicionario

Public Class SeguimientoElementos
    Inherits Base

#Region "[PROPRIEDADES]"
    Public Property TipoElemento As String
        Get
            Return ViewState(Me.ID + "TipoElemento")
        End Get
        Set(value As String)
            ViewState(Me.ID + "TipoElemento") = value
        End Set
    End Property

#End Region

#Region "[OVERRIDES]"
    Protected Overrides Sub DefinirParametrosBase()

        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.RELATORIO_SEGUIMIENTO_ELEMENTOS
        MyBase.ValidarAcesso = True

    End Sub
    Protected Overrides Sub TraduzirControles()

        Me.Master.Titulo = Traduzir("074_Cabecera")
        Me.btnGenerarReporte.Text = Traduzir("057_btnGerar")
        Me.ucFormato.Titulo = Traduzir("057_formato")
        Me.lblTipoElemento.Text = Traduzir("064_TipoElemento")
        Me.btnBuscar.Text = Traduzir("btnBuscar")
        Me.ucPrecintoDelElemento.Titulo = Traduzir("074_TituloPrecintoDelElemento")
        Me.lblFiltros.Text = Traduzir("074_lblFiltros")

    End Sub

#End Region

#Region "[EVENTOS]"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            Me.CargarFormato()
            Me.CargarTipoElementos()

        End If

    End Sub
    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click

        Try
            Dim mensagem = ValidarBuscar()

            If Not String.IsNullOrEmpty(mensagem) Then
                MyBase.MostraMensagemErro(mensagem)
            Else

                Dim Peticion = New Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarSeguimientoElemento.Peticion
                Peticion.Elemento = New Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarSeguimientoElemento.Elemento
                Peticion.Elemento.codTipoElemento = ddlTipoElemento.SelectedValue
                Peticion.Elemento.codPrecinto = ucPrecintoDelElemento.Valor

                Dim respuesta = Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Contenedor.ConsultarSeguimientoElemento(Peticion)

                If (Not String.IsNullOrEmpty(respuesta.MensajeError)) Then
                    Throw New Exception(respuesta.MensajeError)
                End If

                TipoElemento = ddlTipoElemento.SelectedValue
                CargarGridsBusca(respuesta)
                ConfigurarPantallaPorTipoBuscaElemento(TipoElemento)
            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try

    End Sub
    Private Sub btnGenerarReporte_Click(sender As Object, e As System.EventArgs) Handles btnGenerarReporte.Click
        Try
            Dim mensagem As String = ValidarGerar()

            If Not String.IsNullOrEmpty(mensagem) Then
                MyBase.MostraMensagemErro(mensagem)
            Else
                ' Recupera os parametros do relatório.
                Dim objReport As New Prosegur.Genesis.Report.Gerar()
                objReport.Autenticar(False)

                'Lista os parametros do relatório
                Dim dirRelatorio = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CarpetaReportes")
                Dim nomeRelatorio As String = "rptSeguimientoElementos"
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

                listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_TIPOELEMENTO", .Value = TipoElemento})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "PAR_CODBUSCA", .Value = RecuperarSelecionadoGrid()})

                Dim extensao = If(Me.ucFormato.ItemSelecionado = "PDF", "pdf", "xls")

                Dim Buffer = objReport.RenderReport(fullPathReport, Me.ucFormato.ItemSelecionado, listaParametros, extensao)
                Dim nomeArquivo = String.Format("{0}.{1}", Traduzir("074_lblSubTitulo"), extensao)
                Session(nomeArquivo) = Buffer
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "download_relatorio", String.Format("window.location.href = '../Download.aspx?NOME_ARQUIVO={0}'", nomeArquivo), True)
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub
    Protected Sub gdv_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvContenedor.RowDataBound, gdvBulto.RowDataBound, gdvRemesa.RowDataBound

        Try
            If (TipoElemento = Enumeradores.TipoElemento.Contenedor.RecuperarValor()) Then

                If e.Row.RowType = DataControlRowType.Header Then
                    e.Row.Cells(1).Text = Traduzir("074_CabeceraGridPrecintos")
                    e.Row.Cells(2).Text = Traduzir("074_CabeceraGridFechaHoraArmado")
                    e.Row.Cells(3).Text = Traduzir("074_CabeceraGridState")
                    e.Row.Cells(4).Text = Traduzir("074_CabeceraGridAgrupaElementos")
                    e.Row.Cells(5).Text = Traduzir("074_CabeceraGridCuentas")
                End If

            ElseIf (TipoElemento = Enumeradores.TipoElemento.Remesa.RecuperarValor()) Then

                If e.Row.RowType = DataControlRowType.Header Then
                    e.Row.Cells(1).Text = Traduzir("074_CabeceraCodigoExterno")
                    e.Row.Cells(2).Text = Traduzir("074_CabeceraGridFechaHoraCreacion")
                    e.Row.Cells(3).Text = Traduzir("074_CabeceraGridState")
                    e.Row.Cells(4).Text = Traduzir("074_CabeceraGridFechaHoraTransport")
                    e.Row.Cells(5).Text = Traduzir("074_CabeceraGridCuenta")
                End If

            Else

                If e.Row.RowType = DataControlRowType.Header Then
                    e.Row.Cells(1).Text = Traduzir("074_CabeceraGridPrecinto")
                    e.Row.Cells(2).Text = Traduzir("074_CabeceraCodigoExterno")
                    e.Row.Cells(3).Text = Traduzir("074_CabeceraGridFechaHoraCreacion")
                    e.Row.Cells(4).Text = Traduzir("074_CabeceraGridState")
                    e.Row.Cells(5).Text = Traduzir("074_CabeceraGridTipoServicio")
                    e.Row.Cells(6).Text = Traduzir("074_CabeceraGridCuenta")
                End If

            End If

        Catch ex As Exception
            'MyBase.NotificarErro(ex)
        End Try

    End Sub
    Protected Sub gdv_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        sender.DataSource = ViewState("gdvDataSource")
        sender.PageIndex = e.NewPageIndex
        sender.DataBind()
    End Sub
    Protected Sub rbSelecionado_CheckedChanged(sender As Object, e As System.EventArgs)
        If gdvBulto.Visible Then
            CambiarRadioButtonFalse(sender, gdvBulto)
        ElseIf gdvRemesa.Visible Then
            CambiarRadioButtonFalse(sender, gdvRemesa)
        ElseIf gdvContenedor.Visible Then
            CambiarRadioButtonFalse(sender, gdvContenedor)
        End If
    End Sub

#End Region

#Region "[METODOS]"
    Private Sub CargarFormato()

        Dim lista As New List(Of KeyValuePair(Of String, String))
        lista.Add(New KeyValuePair(Of String, String)("PDF", Traduzir("056_formato_pdf")))
        lista.Add(New KeyValuePair(Of String, String)("EXCEL", Traduzir("056_formato_excel")))
        Me.ucFormato.Opciones = lista

    End Sub
    Private Sub CargarTipoElementos()

        ddlTipoElemento.Items.Clear()
        ddlTipoElemento.Items.Add(New ListItem(Traduzir("026_contenedor"), Enumeradores.TipoElemento.Contenedor.RecuperarValor()))
        ddlTipoElemento.Items.Add(New ListItem(Traduzir("026_remesa"), Enumeradores.TipoElemento.Remesa.RecuperarValor()))
        ddlTipoElemento.Items.Add(New ListItem(Traduzir("026_bulto"), Enumeradores.TipoElemento.Bulto.RecuperarValor()))

    End Sub
    Private Sub CargarGridsBusca(respuesta As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarSeguimientoElemento.Respuesta)

        If (ddlTipoElemento.SelectedValue = Enumeradores.TipoElemento.Contenedor.RecuperarValor()) Then
            CargarGridContenedor(respuesta.Elementos)
        ElseIf (ddlTipoElemento.SelectedValue = Enumeradores.TipoElemento.Bulto.RecuperarValor()) Then
            CargarGridBulto(respuesta.Elementos)
        Else
            CargarGridRemesa(respuesta.Elementos)
        End If
    End Sub
    Private Sub CargarGridContenedor(elementos As List(Of Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarSeguimientoElemento.ElementoRespuesta))

        Dim dt = New DataTable("CONTENEDOR")

        dt.Columns.Add(New DataColumn("Precintos"))
        dt.Columns.Add(New DataColumn("FechaHoraArmado"))
        dt.Columns.Add(New DataColumn("Estado"))
        dt.Columns.Add(New DataColumn("AgrupaElementos"))
        dt.Columns.Add(New DataColumn("Cuentas"))

        If elementos IsNot Nothing Then
            For Each elemento As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarSeguimientoElemento.ElementoRespuesta In elementos

                Dim Precintos = ExtraerLabelPrecintos(elemento.Contenedor.Precintos)

                If dt.Select("Precintos = '" + Precintos + "'").Length = 0 Then
                    Dim DataRow = dt.NewRow()
                    DataRow("Precintos") = Precintos
                    DataRow("FechaHoraArmado") = elemento.Contenedor.fechaHoraArmado
                    DataRow("Estado") = elemento.codEstado
                    DataRow("AgrupaElementos") = IIf(elemento.Contenedor.agrupaElementos, Traduzir("034_Si"), Traduzir("034_No"))
                    If (elemento.Documentos IsNot Nothing) Then
                        DataRow("Cuentas") = ExtraerDescripcionCuentas(elemento.Documentos)
                    End If
                    dt.Rows.Add(DataRow)
                End If

            Next
        End If

        BindDataSource(gdvContenedor, dt)
    End Sub
    Private Sub CargarGridBulto(elementos As List(Of Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarSeguimientoElemento.ElementoRespuesta))

        Dim dt = New DataTable("BULTO")
        dt.Columns.Add(New DataColumn("Precinto"))
        dt.Columns.Add(New DataColumn("CodigoExterno"))
        dt.Columns.Add(New DataColumn("FechaHoraCreacion"))
        dt.Columns.Add(New DataColumn("Estado"))
        dt.Columns.Add(New DataColumn("TipoServicio"))
        dt.Columns.Add(New DataColumn("Cuenta"))

        If elementos IsNot Nothing Then
            For Each elemento As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarSeguimientoElemento.ElementoRespuesta In elementos

                Dim Precinto = elemento.Remesa.Bulto.codPrecinto
                If dt.Select("Precinto = '" + Precinto + "'").Length = 0 Then

                    Dim DataRow = dt.NewRow()
                    DataRow("Precinto") = elemento.Remesa.Bulto.codPrecinto
                    DataRow("CodigoExterno") = elemento.Remesa.codigoExterno
                    DataRow("FechaHoraCreacion") = elemento.Remesa.Bulto.fechaHoraCriacion
                    DataRow("Estado") = elemento.codEstado
                    DataRow("TipoServicio") = elemento.Remesa.Bulto.tipoServicio

                    If (elemento.Documentos IsNot Nothing) Then
                        DataRow("Cuenta") = ExtraerDescripcionCuentas(elemento.Documentos)
                    End If
                    dt.Rows.Add(DataRow)
                End If
            Next
        End If

        BindDataSource(gdvBulto, dt)
    End Sub
    Private Sub CargarGridRemesa(elementos As List(Of Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarSeguimientoElemento.ElementoRespuesta))

        Dim dt = New DataTable("REMESA")
        dt.Columns.Add(New DataColumn("CodigoExterno"))
        dt.Columns.Add(New DataColumn("FechaHoraCreacion"))
        dt.Columns.Add(New DataColumn("Estado"))
        dt.Columns.Add(New DataColumn("FechaHoraTransporte"))
        dt.Columns.Add(New DataColumn("Cuenta"))

        If elementos IsNot Nothing Then

            For Each elemento As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarSeguimientoElemento.ElementoRespuesta In elementos

                Dim CodExterno = elemento.Remesa.codigoExterno

                If dt.Select("CodigoExterno = '" + CodExterno + "'").Length = 0 Then
                    Dim DataRow = dt.NewRow()
                    DataRow("CodigoExterno") = elemento.Remesa.codigoExterno
                    DataRow("FechaHoraCreacion") = elemento.Remesa.fechaHoraCriacion
                    DataRow("Estado") = elemento.codEstado
                    DataRow("FechaHoraTransporte") = elemento.Remesa.fechaHoraTransporte
                    If (elemento.Documentos IsNot Nothing) Then
                        DataRow("Cuenta") = ExtraerDescripcionCuentas(elemento.Documentos)
                    End If
                    dt.Rows.Add(DataRow)
                End If
            Next
        End If
        BindDataSource(gdvRemesa, dt)

    End Sub
    Private Sub ConfigurarPantallaPorTipoBuscaElemento(tipoElemento)

        dvTituloGrid.Visible = True

        If (tipoElemento = Enumeradores.TipoElemento.Contenedor.RecuperarValor()) Then

            gdvContenedor.Visible = True
            gdvBulto.Visible = Not gdvContenedor.Visible
            gdvRemesa.Visible = Not gdvContenedor.Visible

            lblTituloGrid.Text = Traduzir("074_LblConsultaContenedor")

        ElseIf (tipoElemento = Enumeradores.TipoElemento.Bulto.RecuperarValor()) Then

            gdvBulto.Visible = True
            gdvContenedor.Visible = Not gdvBulto.Visible
            gdvRemesa.Visible = Not gdvBulto.Visible

            lblTituloGrid.Text = Traduzir("074_LblConsultaBulto")

        Else

            gdvRemesa.Visible = True
            gdvContenedor.Visible = Not gdvRemesa.Visible
            gdvBulto.Visible = Not gdvRemesa.Visible
            lblTituloGrid.Text = Traduzir("074_LblConsultaRemesa")

        End If

    End Sub
    Private Function ValidarGerar() As String

        Dim retorno = New StringBuilder

        Me.ucFormato.GuardarDatos()

        If String.IsNullOrEmpty(RecuperarSelecionadoGrid()) Then
            retorno.AppendLine(Traduzir("074_msg_Obrigatorio_Selecionar_Uno_Registro"))
        End If

        Return retorno.ToString()

    End Function
    Private Function ValidarBuscar() As String

        ucPrecintoDelElemento.GuardarDatos()

        Dim retorno = New StringBuilder
        If String.IsNullOrEmpty(ucPrecintoDelElemento.Valor) Then
            retorno.AppendLine(Traduzir("074_msg_campo_precinto_Del_Elemento_Obrigatorio"))
        End If

        Return retorno.ToString()
    End Function
    Public Function RecuperarSelecionadoGrid() As String
        Dim ParamBusca As String = String.Empty
        Try
            If (TipoElemento = Enumeradores.TipoElemento.Contenedor.RecuperarValor()) Then

                Dim selecionados = (From p In gdvContenedor.Rows.Cast(Of GridViewRow)() _
                Where DirectCast(p.Cells(0).FindControl("rbSelecionado"), RadioButton).Checked _
                Select Convert.ToInt32(p.RowIndex)).ToList
                If selecionados.Count > 0 Then
                    ParamBusca = gdvContenedor.DataKeys(selecionados(0)).Values(0)
                End If

                Dim Precintos = ParamBusca.Split("|")

                ParamBusca = String.Empty
                For Each precinto As String In Precintos
                    ParamBusca += "|" + precinto.Trim()
                Next

            ElseIf (TipoElemento = Enumeradores.TipoElemento.Remesa.RecuperarValor()) Then

                Dim selecionados = (From p In gdvRemesa.Rows.Cast(Of GridViewRow)() _
                Where DirectCast(p.Cells(0).FindControl("rbSelecionado"), RadioButton).Checked _
                Select Convert.ToInt32(p.RowIndex)).ToList
                If selecionados.Count > 0 Then
                    ParamBusca = gdvRemesa.DataKeys(selecionados(0)).Values(0)
                End If

            Else

                Dim selecionados = (From p In gdvBulto.Rows.Cast(Of GridViewRow)() _
                Where DirectCast(p.Cells(0).FindControl("rbSelecionado"), RadioButton).Checked _
                Select Convert.ToInt32(p.RowIndex)).ToList
                If selecionados.Count > 0 Then
                    ParamBusca = gdvBulto.DataKeys(selecionados(0)).Values(0)
                End If

            End If

        Catch ex As Exception
            'MyBase.NotificarErro(ex)
        End Try

        Return ParamBusca
    End Function
    Private Function ExtraerDescripcionCuentas(documentos As List(Of Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarSeguimientoElemento.Documento)) As String
        Dim StrBuilder = New StringBuilder

        For Each documento As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarSeguimientoElemento.Documento In documentos
            Dim CuentaAInsertar = String.Empty

            If (documento.CuentasDestino IsNot Nothing OrElse documento.CuentasDestino.Count > 0) Then

                CuentaAInsertar += documento.CuentasDestino(0).Sector.desDelegacion + "/" + documento.CuentasDestino(0).Sector.desPlanta + "/" + documento.CuentasDestino(0).Sector.desSector
                CuentaAInsertar += " | " + documento.CuentasDestino(0).Cliente.desCliente + IIf(String.IsNullOrEmpty(documento.CuentasDestino(0).Cliente.desSubCliente), String.Empty, "/" + documento.CuentasDestino(0).Cliente.desSubCliente) + _
                                                                                           IIf(String.IsNullOrEmpty(documento.CuentasDestino(0).Cliente.desPtoServicio), String.Empty, "/" + documento.CuentasDestino(0).Cliente.desPtoServicio)
                CuentaAInsertar += " | " + IIf(String.IsNullOrEmpty(documento.CuentasDestino(0).Canal.desCanal), String.Empty, documento.CuentasDestino(0).Canal.desCanal) + _
                                        IIf(String.IsNullOrEmpty(documento.CuentasDestino(0).Canal.desSubcanal), String.Empty, "/" + documento.CuentasDestino(0).Canal.desSubcanal)

                If (Not StrBuilder.ToString().Contains(CuentaAInsertar)) Then
                    StrBuilder.AppendLine(CuentaAInsertar)
                End If
            End If
        Next

        Return AnadirQuebraDeLineasCuenta(StrBuilder.ToString())
    End Function
    Private Function ExtraerLabelPrecintos(precintos As ObservableCollection(Of String)) As Object

        Dim LabelPrecintos = String.Empty
        For Each precinto As String In precintos
            LabelPrecintos += IIf(String.IsNullOrEmpty(LabelPrecintos), precinto, " | " + precinto)
        Next

        Return LabelPrecintos
    End Function
    Private Sub CambiarRadioButtonFalse(editedRb As RadioButton, grid As GridView)

        For index As Integer = 0 To grid.Rows.Count - 1
            Dim rb As RadioButton = grid.Rows(index).FindControl("rbSelecionado")
            If Not rb.Equals(editedRb) Then
                rb.Checked = False
            End If
        Next
    End Sub
    Private Function AnadirQuebraDeLineasCuenta(cuentaToFormat As String) As String

        If cuentaToFormat.Equals(DBNull.Value) Then
            Return String.Empty
        Else
            Return cuentaToFormat.Replace("\n", "&lt;br/&gt;")
        End If

    End Function
    Private Sub BindDataSource(gdv As Object, datasource As DataTable)

        gdv.DataSource = datasource
        gdv.DataBind()

        ViewState("gdvDataSource") = datasource
    End Sub

#End Region

End Class