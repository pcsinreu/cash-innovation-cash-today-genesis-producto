Imports Ionic.Zip
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos
Imports Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Certificacion
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Newtonsoft.Json
Imports Prosegur.Genesis.Report
Imports System.IO

Public Class ResultadoCertificacion
    Inherits Base

#Region "[VARIAVEIS]"

    Const colFyhCertificado As Integer = 1
    Const colTipo As Integer = 2
    Const colCliente As Integer = 3
    Const colSubCanal As Integer = 4
    Const colReporte As Integer = 5

#End Region

#Region "[PROPRIEDADES]"

    Public Property FiltroCertificado As FiltroResultadoCertificado
        Get
            Return Session("FiltroCertificado")
        End Get
        Set(value As FiltroResultadoCertificado)
            Session("FiltroCertificado") = value
        End Set
    End Property

    Private WithEvents _ucClientes As ucCliente
    Public Property ucClientes() As ucCliente
        Get
            If _ucClientes Is Nothing Then
                _ucClientes = LoadControl("~\Controles\ucCliente.ascx")
                _ucClientes.ID = Me.ID & "_ucClientes"
                AddHandler _ucClientes.Erro, AddressOf ErroControles
                phCliente.Controls.Add(_ucClientes)
            End If
            Return _ucClientes
        End Get
        Set(value As ucCliente)
            _ucClientes = value
        End Set
    End Property

    Private WithEvents _ucDelegacion As ucSector
    Public Property ucDelegacion() As ucSector
        Get
            If _ucDelegacion Is Nothing Then
                _ucDelegacion = LoadControl("~\Controles\ucSector.ascx")
                _ucDelegacion.ID = "ucSector"
                AddHandler _ucDelegacion.Erro, AddressOf ErroControles
                If phDelegacion.Controls.Count = 0 Then
                    phDelegacion.Controls.Add(_ucDelegacion)
                End If
            End If
            Return _ucDelegacion
        End Get
        Set(value As ucSector)
            _ucDelegacion = value
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

    Public Property Delegaciones As ObservableCollection(Of Clases.Delegacion)
        Get
            Return ucDelegacion.Delegaciones
        End Get
        Set(value As ObservableCollection(Of Clases.Delegacion))
            ucDelegacion.Delegaciones = value
        End Set
    End Property

    Private _resultadoBusca As ObservableCollection(Of Genesis.ContractoServicio.GenesisSaldos.Certificacion.Certificado)

    Public Property resultadoBusca As ObservableCollection(Of Genesis.ContractoServicio.GenesisSaldos.Certificacion.Certificado)
        Get
            _resultadoBusca = New ObservableCollection(Of Certificacion.Certificado)
            If Session("resultadoBusca") IsNot Nothing Then
                _resultadoBusca = Session("resultadoBusca")
                certificados = _resultadoBusca
            End If
            Return _resultadoBusca
        End Get
        Set(value As ObservableCollection(Of Genesis.ContractoServicio.GenesisSaldos.Certificacion.Certificado))
            _resultadoBusca = value
            Session("resultadoBusca") = _resultadoBusca
            certificados = _resultadoBusca
        End Set
    End Property

    Private Shared _certificados As ObservableCollection(Of Genesis.ContractoServicio.GenesisSaldos.Certificacion.Certificado)

    Public Shared Property certificados As ObservableCollection(Of Genesis.ContractoServicio.GenesisSaldos.Certificacion.Certificado)
        Get
            Return _certificados
        End Get
        Set(value As ObservableCollection(Of Genesis.ContractoServicio.GenesisSaldos.Certificacion.Certificado))
            _certificados = value
        End Set
    End Property

    Private Shared _CarpetaReportes As String
    Public Shared ReadOnly Property CarpetaReportes As String
        Get
            If String.IsNullOrEmpty(_CarpetaReportes) Then

                Dim objPeticionGenesis As New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Peticion
                With objPeticionGenesis
                    .CodigoAplicacion = Comon.Constantes.CODIGO_APLICACION_GENESIS
                    .CodigoDelegacion = InformacionUsuario.DelegacionSeleccionada.Codigo
                    .Parametros = New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.ParametroColeccion() _
                                  From {New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Parametro() _
                                                           With {.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_IAC_DELEGACION_DIRECCION_REPORTES_GENERADOS}}

                End With

                'Classe proxy do IAC
                Dim ojbAccionIAC As New Prosegur.Global.GesEfectivo.IAC.LogicaNegocio.AccionIntegracion

                Dim objRespuestaGenesis As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Respuesta = ojbAccionIAC.GetParametrosDelegacionPais(objPeticionGenesis)

                ' Recupera os parâmetros da delegação
                If objRespuestaGenesis IsNot Nothing AndAlso objRespuestaGenesis.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                    Throw New Excepcion.NegocioExcepcion(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, objRespuestaGenesis.MensajeError)
                End If

                If objRespuestaGenesis.Parametros.Count > 0 Then
                    _CarpetaReportes = objRespuestaGenesis.Parametros.First.ValorParametro
                End If

            End If

            Return _CarpetaReportes

        End Get

    End Property


#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub Inicializar()
        Try
            MyBase.Inicializar()

            ConfigurarControle_Cliente()
            ConfigurarControle_Delegacion()
            btnVolver.BotonOnClientClick = "window.history.back(); return false;"

            'Seta o foco no controle
            If Not IsPostBack Then

                If Session("DireccionReporte") IsNot Nothing AndAlso Request.QueryString("download") = "true" Then

                    Dim DireccionReporte As String = Session("DireccionReporte")

                    If Not String.IsNullOrEmpty(DireccionReporte) AndAlso _
                        File.Exists(DireccionReporte) Then

                        Dim buffer As Byte() = File.ReadAllBytes(DireccionReporte)

                        Dim NombreArchivo As String = DireccionReporte.Split("\").LastOrDefault

                        If Not String.IsNullOrEmpty(NombreArchivo) Then

                            Response.ClearHeaders()
                            Response.ClearContent()
                            Response.Clear()
                            Response.ContentType = "application/octet-stream"
                            Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}", NombreArchivo))
                            Response.AddHeader("Content-Length", buffer.Length)
                            Response.BinaryWrite(buffer)
                            Response.Buffer = True
                            Response.Flush()
                            Response.Clear()
                            Response.End()

                        End If

                    End If

                Else
                    resultadoBusca = Nothing
                    CarregaFiltroRadioEstados()

                    Me.txtCodigoCertificado.Text = Request.QueryString("Codcertificado")
                    Me.txtCodigoCertificado.Focus()
                    If Not String.IsNullOrEmpty(txtCodigoCertificado.Text) Then
                        dvFiltros.Visible = False
                        dvCodigo.Visible = True
                        Me.EjecutarBusquedaCertificado()
                    Else
                        dvFiltros.Visible = True
                        dvCodigo.Visible = False
                    End If
                End If

                RellenarFiltros()

            End If

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()
        ' Titulos FieldSet
        lblTitulo.Text = Traduzir("069_lblTitulo")
        lblTitulo1.Text = Traduzir("069_lblTitulo1")

        Me.lblCodigoCertificado.Text = Traduzir("069_lblCodigoCertificado")
        Me.btnEjecutarInforme.Text = Traduzir("069_btnEjecutarInforme")
        Me.lblFechaCertificacion.Text = Traduzir("069_lblFechaCertificacion")
        Me.lblEstado.Text = Traduzir("069_lblEstado")
        Me.btnBuscar.Text = Traduzir("069_btnBuscar")
        Me.btnConvertirProvisionalSinCierre.Text = Traduzir("069_btnConvertirProvisionalSinCierre")
        Me.btnConvertirProvisionalConCierre.Text = Traduzir("069_btnConvertirProvisionalConCierre")
        Me.btnConvertirDefinitivo.Text = Traduzir("069_btnConvertirDefinitivo")
        Me.btnConvertirRelacionados.Text = Traduzir("073_btnConvertirRelacionado")
        Me.lblTituloConvertirCertificado.Text = Traduzir("073_lblConvertirCertificado")
        Me.btnVolver.Text = Traduzir("069_btnVolver")

        lblSemRegistro.Text = Traduzir("lblSemRegistro")

        Master.Titulo = Traduzir("069_Titulo")

    End Sub

    Protected Overrides Sub AdicionarScripts()
        MyBase.AdicionarScripts()
        Dim script As String = String.Format("AbrirCalendario('{0}','{1}');", txtFechaCertificacion.ClientID, "True")
        script &= String.Format("AbrirCalendario('{0}','{1}');", txtFechaCertificacion.ClientID, "True")
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR", script, True)
    End Sub

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.RESULTADO_CERTIFICADO_CONSULTAR
        MyBase.ValidarAcesso = True
    End Sub

#End Region

#Region "[EVENTOS]"

    Protected Sub grvResultado_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(colFyhCertificado).Text = Traduzir("069_grid_fyhcertificado")
            e.Row.Cells(colTipo).Text = Traduzir("069_grid_tipo")
            e.Row.Cells(colCliente).Text = Traduzir("069_grid_cliente")
            e.Row.Cells(colSubCanal).Text = Traduzir("069_grid_subcanal")
            e.Row.Cells(colReporte).Text = Traduzir("069_grid_reporte")
            e.Row.Cells(colReporte).Text = Traduzir("069_grid_situacion")
        ElseIf e.Row.RowType = DataControlRowType.DataRow Then
            Dim certificado As Genesis.ContractoServicio.GenesisSaldos.Certificacion.Certificado = e.Row.DataItem
            If certificado IsNot Nothing Then
                Dim Tipo As Label = e.Row.FindControl("Tipo")
                If Tipo IsNot Nothing Then
                    Tipo.Text = Traduzir("069_estado_certificado_" + certificado.CodigoEstado)
                End If

                If certificado.ConfigReporte IsNot Nothing AndAlso certificado.ConfigReporte.ResultadoReporte IsNot Nothing Then

                    With certificado.ConfigReporte.ResultadoReporte

                        Select Case .CodigoEstado

                            Case ContractoServicio.Constantes.CONST_RESULTADO_CERTIFICADO_PENDIENTE
                                CType(e.Row.Cells(6).FindControl("imgSituacionReporte"), Image).ImageUrl = "~/Imagenes/Pendente.gif"
                                CType(e.Row.Cells(6).FindControl("lblSituacionReporte"), Label).Text = .FechaInicioEjecucion & " - " & Traduzir("069_lbl_en_cola")
                            Case ContractoServicio.Constantes.CONST_RESULTADO_CERTIFICADO_ENCURSO
                                CType(e.Row.Cells(6).FindControl("imgSituacionReporte"), Image).ImageUrl = "~/Imagenes/Executando.gif"
                                CType(e.Row.Cells(6).FindControl("lblSituacionReporte"), Label).Text = .FechaInicioEjecucion & " - " & Traduzir("069_lbl_en_curso")
                            Case ContractoServicio.Constantes.CONST_RESULTADO_CERTIFICADO_ERRO
                                CType(e.Row.Cells(6).FindControl("imgSituacionReporte"), Image).ImageUrl = "~/Imagenes/Erro.gif"
                                CType(e.Row.Cells(6).FindControl("lblSituacionReporte"), Label).Text = .FechaInicioEjecucion & " - " & Traduzir("069_lbl_erro_ejecucion")

                                If Not String.IsNullOrEmpty(certificado.ConfigReporte.ResultadoReporte.DescripcionErro) Then
                                    Dim imgErroReporte As ImageButton = CType(e.Row.Cells(7).FindControl("imgDownload"), ImageButton)
                                    imgErroReporte.Visible = True
                                    imgErroReporte.ImageUrl = "~/Imagenes/error1.png"
                                    Dim Erro As String = HttpUtility.HtmlEncode(certificado.ConfigReporte.ResultadoReporte.DescripcionErro)
                                    imgErroReporte.Attributes.Add("onclick", Aplicacao.Util.Utilidad.CriarChamadaMensagemErro(Erro, String.Empty))
                                End If
                                
                            Case ContractoServicio.Constantes.CONST_RESULTADO_CERTIFICADO_PROCESADO
                                CType(e.Row.Cells(6).FindControl("imgSituacionReporte"), Image).ImageUrl = "~/Imagenes/Concluido.gif"
                                CType(e.Row.Cells(6).FindControl("lblSituacionReporte"), Label).Text = .FechaFinEjecucion & " - " & Traduzir("069_lbl_procesado")
                                Dim imgDownloadReporte As ImageButton = CType(e.Row.Cells(7).FindControl("imgDownload"), ImageButton)
                                imgDownloadReporte.Visible = True
                                imgDownloadReporte.Attributes.Add("onclick", "DownloadReporte('" & certificado.CodigoCertificado & "', '" & certificado.CodigoEstado & "', '" & certificado.ConfigReporte.Codigo & "', '" & certificado.SubCanales.First.Codigo & "');")
                        End Select

                    End With

                End If
            End If
        End If
    End Sub

    Protected Sub btnEjecutarInforme_Click(sender As Object, e As System.EventArgs) Handles btnEjecutarInforme.Click
        Try
            Dim listaRelatorios As New List(Of Integer)
            For Each gdRow As GridViewRow In grvResultado.Rows
                Dim chbSelecionar As CheckBox = gdRow.FindControl("chbSelecionar")
                If chbSelecionar.Checked Then
                    listaRelatorios.Add(gdRow.RowIndex)
                End If
            Next

            If listaRelatorios.Count > 0 Then
                Session("DireccionReporte") = Nothing
                AgendarReporte(listaRelatorios)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try

    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        Try
            Me.EjecutarBusquedaCertificado()

            ' PROVISIONAL
            If rblEstado.SelectedValue = 1 AndAlso Me.grvResultado.Rows.Count > 0 Then
                Me.btnConvertirRelacionados.Visible = True
            Else
                Me.btnConvertirRelacionados.Visible = False
            End If

            Me.upBotonesRodapie.Update()

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try

    End Sub

    Protected Sub btnConvertirProvisionalConCierre_Click(sender As Object, e As System.EventArgs) Handles btnConvertirProvisionalConCierre.Click
        Try
            If Not String.IsNullOrEmpty(txtCodigoCertificado.Text) Then

                Dim objPeticion As New Certificacion.DatosCertificacion.Peticion
                With objPeticion
                    .CodigoCertificado = txtCodigoCertificado.Text
                    .DelegacionLogada = InformacionUsuario.DelegacionSeleccionada
                    .UsuarioCreacion = InformacionUsuario.Nombre
                    .CodigoEstado = ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE

                    Dim objAccionObtenerCertificado As New AccionRecuperarFiltrosCertificado
                    Dim objCertificado = objAccionObtenerCertificado.Ejecutar(New ContractoServicio.GenesisSaldos.Certificacion.RecuperarFiltrosCertificado.Peticion With {
                                                                            .CodigoCertificado = txtCodigoCertificado.Text, .Delegacion = InformacionUsuario.DelegacionSeleccionada
                                                                            })




                    If Not String.IsNullOrEmpty(objCertificado.MensajeError) Then
                        Throw New Excepcion.NegocioExcepcion(objCertificado.MensajeError)
                    End If

                    .Cliente = New Clases.Cliente With {
                                                .Codigo = objCertificado.CodigoCliente,
                                                .Identificador = objCertificado.IdentificadorCliente,
                                                .Descripcion = objCertificado.DescripcionCliente
                                                }

                    .EsTodasDelegaciones = objCertificado.TodasDelegaciones
                    If objCertificado.Delegaciones IsNot Nothing AndAlso objCertificado.Delegaciones.Count > 0 Then
                        .CodigosDelegaciones = objCertificado.Delegaciones.Select(Function(a) a.Codigo).ToList()
                    End If

                    .EsTodosSectores = objCertificado.TodosSectores
                    If objCertificado.Sectores IsNot Nothing AndAlso objCertificado.Sectores.Count > 0 Then
                        .CodigosSectores = objCertificado.Sectores.Select(Function(a) a.Codigo).ToList()
                    End If

                    .EsTodosCanales = objCertificado.TodosSubCanales
                    If objCertificado.SubCanales IsNot Nothing AndAlso objCertificado.SubCanales.Count > 0 Then
                        .CodigosSubCanales = objCertificado.SubCanales.Select(Function(a) a.Codigo).ToList()
                    End If

                    .FyhCertificado = objCertificado.FechaHoraCertificado
                End With

                Dim objAccion As New AccionConvertirCertificado()
                Dim objRespuesta = objAccion.Convertir(objPeticion)

                If Not String.IsNullOrEmpty(objRespuesta.MensajeError) Then
                    MyBase.MostraMensagemErro(objRespuesta.MensajeError)
                    Exit Sub
                Else
                    Response.Redireccionar("ResultadoCertificacion.aspx?Codcertificado=" + objRespuesta.Certificado.CodigoCertificado)
                End If

            End If

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try

    End Sub

    Private Sub btnConvertirDefinitivo_Click(sender As Object, e As System.EventArgs) Handles btnConvertirDefinitivo.Click
        Try
            If Not String.IsNullOrEmpty(txtCodigoCertificado.Text) Then

                Dim objPeticion As New Certificacion.DatosCertificacion.Peticion
                With objPeticion
                    .CodigoCertificado = txtCodigoCertificado.Text
                    .CodigoCertificadoDefinitivo = txtCodigoCertificado.Text
                    .DelegacionLogada = InformacionUsuario.DelegacionSeleccionada
                    .UsuarioCreacion = InformacionUsuario.Nombre
                    .CodigoEstado = ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO

                    Dim objAccionObtenerCertificado As New AccionRecuperarFiltrosCertificado
                    Dim objCertificado = objAccionObtenerCertificado.Ejecutar(New ContractoServicio.GenesisSaldos.Certificacion.RecuperarFiltrosCertificado.Peticion With {
                                                                            .CodigoCertificado = txtCodigoCertificado.Text, .Delegacion = InformacionUsuario.DelegacionSeleccionada
                                                                            })

                    .Cliente = New Clases.Cliente With {
                                                .Codigo = objCertificado.CodigoCliente,
                                                .Identificador = objCertificado.IdentificadorCliente,
                                                .Descripcion = objCertificado.DescripcionCliente
                                                }

                    .EsTodasDelegaciones = objCertificado.TodasDelegaciones
                    If objCertificado.Delegaciones IsNot Nothing AndAlso objCertificado.Delegaciones.Count > 0 Then
                        .CodigosDelegaciones = objCertificado.Delegaciones.Select(Function(a) a.Codigo).ToList()
                    End If

                    .EsTodosSectores = objCertificado.TodosSectores
                    If objCertificado.Sectores IsNot Nothing AndAlso objCertificado.Sectores.Count > 0 Then
                        .CodigosSectores = objCertificado.Sectores.Select(Function(a) a.Codigo).ToList()
                    End If

                    .EsTodosCanales = objCertificado.TodosSubCanales
                    If objCertificado.SubCanales IsNot Nothing AndAlso objCertificado.SubCanales.Count > 0 Then
                        .CodigosSubCanales = objCertificado.SubCanales.Select(Function(a) a.Codigo).ToList()
                    End If

                    .FyhCertificado = objCertificado.FechaHoraCertificado
                End With

                Dim objAccion As New AccionConvertirCertificado()
                Dim objRespuesta = objAccion.Convertir(objPeticion)

                If Not String.IsNullOrEmpty(objRespuesta.MensajeError) Then
                    MyBase.MostraMensagemErro(objRespuesta.MensajeError)
                    Exit Sub
                Else
                    Response.Redireccionar("ResultadoCertificacion.aspx?Codcertificado=" + objRespuesta.Certificado.CodigoCertificado)
                End If

            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub

    Private Sub btnConvertirProvisionalSinCierre_Click(sender As Object, e As System.EventArgs) Handles btnConvertirProvisionalSinCierre.Click
        Try
            If Not String.IsNullOrEmpty(txtCodigoCertificado.Text) Then

                Dim objPeticion As New Certificacion.DatosCertificacion.Peticion
                With objPeticion
                    .CodigoCertificado = txtCodigoCertificado.Text
                    .DelegacionLogada = InformacionUsuario.DelegacionSeleccionada
                    .UsuarioCreacion = InformacionUsuario.Nombre
                    .CodigoEstado = ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_SIN_CIERRE

                    Dim objAccionObtenerCertificado As New AccionRecuperarFiltrosCertificado
                    Dim objCertificado = objAccionObtenerCertificado.Ejecutar(New ContractoServicio.GenesisSaldos.Certificacion.RecuperarFiltrosCertificado.Peticion With {
                                                                            .CodigoCertificado = txtCodigoCertificado.Text, .Delegacion = InformacionUsuario.DelegacionSeleccionada
                                                                            })

                    .Cliente = New Clases.Cliente With {
                                                .Codigo = objCertificado.CodigoCliente,
                                                .Identificador = objCertificado.IdentificadorCliente,
                                                .Descripcion = objCertificado.DescripcionCliente
                                                }

                    .EsTodasDelegaciones = objCertificado.TodasDelegaciones
                    If objCertificado.Delegaciones IsNot Nothing AndAlso objCertificado.Delegaciones.Count > 0 Then
                        .CodigosDelegaciones = objCertificado.Delegaciones.Select(Function(a) a.Codigo).ToList()
                    End If

                    .EsTodosSectores = objCertificado.TodosSectores
                    If objCertificado.Sectores IsNot Nothing AndAlso objCertificado.Sectores.Count > 0 Then
                        .CodigosSectores = objCertificado.Sectores.Select(Function(a) a.Codigo).ToList()
                    End If

                    .EsTodosCanales = objCertificado.TodosSubCanales
                    If objCertificado.SubCanales IsNot Nothing AndAlso objCertificado.SubCanales.Count > 0 Then
                        .CodigosSubCanales = objCertificado.SubCanales.Select(Function(a) a.Codigo).ToList()
                    End If

                    .FyhCertificado = objCertificado.FechaHoraCertificado
                End With

                Dim objAccion As New AccionConvertirCertificado()
                Dim objRespuesta = objAccion.Convertir(objPeticion)

                If Not String.IsNullOrEmpty(objRespuesta.MensajeError) Then
                    MyBase.MostraMensagemErro(objRespuesta.MensajeError)
                    Exit Sub
                Else
                    Response.Redireccionar("ResultadoCertificacion.aspx?Codcertificado=" + objRespuesta.Certificado.CodigoCertificado)
                End If

            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub

    Private Sub btnConvertirRelacionados_Click(sender As Object, e As System.EventArgs) Handles btnConvertirRelacionados.Click
        Try
            Dim listaRelatorios As New List(Of Integer)
            For Each gdRow As GridViewRow In grvResultado.Rows
                Dim chbSelecionar As CheckBox = gdRow.FindControl("chbSelecionar")
                If chbSelecionar.Checked Then
                    listaRelatorios.Add(gdRow.RowIndex)
                End If
            Next

            If listaRelatorios.Count = 0 Then
                MyBase.MostraMensagemErro(Traduzir("073_lbl_certificado_nao_selecionado"))
            Else
                Dim certificados As New List(Of Certificacion.Certificado)
                For Each indice In listaRelatorios
                    Dim certificado = resultadoBusca(indice)
                    If Not certificados.Exists(Function(c) c.IdentificadorCertificado = certificado.IdentificadorCertificado) Then
                        certificados.Add(certificado.Clonar)
                    End If
                Next
                Me.ucConvertirCertificado.PopularGrid(certificados)

                Dim script As String = String.Format("displayElemento('{0}','block');", dvConvertirCertificadoPopup.ClientID)
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), Me.ID, script, True)
                Me.ucConvertirCertificado.Cancelar(dvConvertirCertificadoPopup.ClientID)

            End If

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub

#End Region

#Region "[METODOS]"

    Private Sub RellenarFiltros()

        If FiltroCertificado IsNot Nothing AndAlso FiltroCertificado.Delegacion IsNot Nothing AndAlso _
           String.IsNullOrEmpty(txtCodigoCertificado.Text) Then

            If Not FiltroCertificado.FechaCertificado.Equals(Date.MinValue) Then
                txtFechaCertificacion.Text = FiltroCertificado.FechaCertificado
            End If

            If Delegaciones IsNot Nothing Then
                Delegaciones.Clear()
                Delegaciones.Add(FiltroCertificado.Delegacion)
            End If

            If Clientes IsNot Nothing AndAlso FiltroCertificado.Cliente IsNot Nothing Then
                Clientes.Clear()
                Clientes.Add(FiltroCertificado.Cliente)
            End If

            If Not String.IsNullOrEmpty(FiltroCertificado.TipoCertificado) Then
                rblEstado.SelectedValue = FiltroCertificado.TipoCertificado
            End If

            Me.EjecutarBusquedaCertificado()

        End If

    End Sub

    Private Sub EjecutarBusquedaCertificado()

        'Faz a validação dos dados
        If (Clientes Is Nothing OrElse Clientes.Count = 0) AndAlso (Delegaciones Is Nothing OrElse Delegaciones.Count = 0) Then
            MyBase.MostraMensagemErro(String.Format(Traduzir("msg_campo_obrigatorio"), Traduzir("069_msgCliente_Delegacion")))
            Exit Sub
        End If

        Dim objPeticion As New Certificacion.DatosCertificacion.Peticion
        objPeticion.DelegacionLogada = InformacionUsuario.DelegacionSeleccionada

        If String.IsNullOrEmpty(txtCodigoCertificado.Text) Then

            If FiltroCertificado Is Nothing Then FiltroCertificado = New FiltroResultadoCertificado

            With objPeticion
                If Delegaciones IsNot Nothing AndAlso Delegaciones.Count > 0 Then
                    If .CodigosDelegaciones Is Nothing Then
                        .CodigosDelegaciones = New List(Of String)
                    End If
                    FiltroCertificado.Delegacion = Delegaciones.First
                    .CodigosDelegaciones.Add(Delegaciones.First.Codigo)
                Else
                    FiltroCertificado.Delegacion = Nothing
                End If
                If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
                    FiltroCertificado.Cliente = Clientes.First
                    .Cliente = New Clases.Cliente With {.Codigo = Clientes.First.Codigo}
                Else
                    FiltroCertificado.Cliente = Nothing
                End If
                If Not String.IsNullOrEmpty(txtFechaCertificacion.Text) AndAlso IsDate(txtFechaCertificacion.Text) Then
                    FiltroCertificado.FechaCertificado = DateTime.Parse(txtFechaCertificacion.Text)
                    .FyhCertificado = DateTime.Parse(txtFechaCertificacion.Text)
                Else
                    FiltroCertificado.FechaCertificado = Date.MinValue
                End If
                FiltroCertificado.TipoCertificado = rblEstado.SelectedValue
                If rblEstado.SelectedValue = 0 Then
                    .CodigoEstado = ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO
                ElseIf rblEstado.SelectedValue = 1 Then
                    .CodigoEstado = ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE
                End If
            End With
        Else
            With objPeticion
                .CodigoCertificado = HttpUtility.HtmlEncode(txtCodigoCertificado.Text.Trim)
                .CodigosDelegaciones = New List(Of String)
                .CodigosDelegaciones.Add(InformacionUsuario.DelegacionSeleccionada.Codigo)
            End With
        End If

        Dim objRespuesta As List(Of Genesis.ContractoServicio.GenesisSaldos.Certificacion.Certificado)

        'Recupera o certificado
        Dim objAccion As New AccionGenerarCertificacion()
        objRespuesta = objAccion.RetornarCertificadosRelatorio(objPeticion)

        If objRespuesta IsNot Nothing AndAlso objRespuesta.Count > 0 Then
            Dim lstCertificado As New ObservableCollection(Of Certificacion.Certificado)
            objRespuesta.ForEach(Sub(a) lstCertificado.Add(a))
            resultadoBusca = lstCertificado
            grvResultado.DataSource = lstCertificado
            grvResultado.DataBind()
            dvEmptyData.Visible = False
            If Not String.IsNullOrEmpty(txtCodigoCertificado.Text) Then
                If Base.PossuiPermissao(Aplicacao.Util.Utilidad.eTelas.CERTIFICADOS_GENERAR_CONSULTAR) Then
                    If lstCertificado.First.CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_SIN_CIERRE Then
                        btnConvertirProvisionalConCierre.Visible = True
                    ElseIf lstCertificado.First.CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE Then
                        btnConvertirDefinitivo.Visible = True
                    End If
                End If
            End If
        Else
            resultadoBusca = Nothing
            grvResultado.DataSource = Nothing
            grvResultado.DataBind()
            dvEmptyData.Visible = True
        End If

    End Sub

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagemErro(If(TypeOf e.Erro Is Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub

    Protected Sub ConfigurarControle_Cliente()

        Me.ucClientes.SelecaoMultipla = False
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.SubClienteHabilitado = False
        Me.ucClientes.PtoServicioHabilitado = False
        Me.ucClientes.ucSubCliente.Visible = False
        Me.ucClientes.ucPtoServicio.Visible = False
        Me.ucClientes.TotalizadorSaldo = True

        If Clientes IsNot Nothing Then
            Me.ucClientes.Clientes = Clientes
        End If

    End Sub

    Protected Sub ConfigurarControle_Delegacion()
        Me.ucDelegacion.ConsiderarPermissoes = True
        Me.ucDelegacion.SelecaoMultipla = False
        Me.ucDelegacion.SectorHabilitado = False
        Me.ucDelegacion.DelegacionHabilitado = True
        Me.ucDelegacion.PlantaHabilitado = False

        Me.ucDelegacion.ucDelegacion.Visible = True
        Me.ucDelegacion.ucPlanta.Visible = False
        Delegaciones.Add(Base.InformacionUsuario.DelegacionSeleccionada)

        If Delegaciones IsNot Nothing Then
            Me.ucDelegacion.Delegaciones = Delegaciones
        End If

    End Sub

    Private Sub CarregaFiltroRadioEstados()
        Try
            rblEstado.Items.Add(New ListItem(Traduzir("069_rblEstado_definitivo"), 0))
            rblEstado.Items.Add(New ListItem(Traduzir("069_rblEstado_provisionales"), 1))
            rblEstado.Items(0).Selected = True
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub

    Public Sub AgendarReporte(indices As List(Of Integer))

        Dim objPeticion As New ContractoServicio.Contractos.GenesisSaldos.Reporte.GenerarInforme.Peticion

        With objPeticion
            .Reportes = New ObservableCollection(Of ContractoServicio.Contractos.GenesisSaldos.Reporte.GenerarInforme.Reporte)
            Dim objParametros As ObservableCollection(Of Clases.ParametroReporte)

            For Each indice In indices

                objParametros = New ObservableCollection(Of Clases.ParametroReporte)

                objParametros.Add(New Clases.ParametroReporte With { _
                                  .Codigo = Comon.Constantes.CODIGO_PARAMETRO_REPORTE_CODIGO_CERTIFICADO,
                                  .DescripcionValor = resultadoBusca(indice).CodigoCertificado})

                objParametros.Add(New Clases.ParametroReporte With { _
                                 .Codigo = Comon.Constantes.CODIGO_PARAMETRO_REPORTE_CODIGO_ESTADO_CERTIFICADO,
                                 .DescripcionValor = resultadoBusca(indice).CodigoEstado})

                objParametros.Add(New Clases.ParametroReporte With { _
                                .Codigo = Comon.Constantes.CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_CERTIFICADO,
                                .DescripcionValor = resultadoBusca(indice).IdentificadorCertificado})

                objParametros.Add(New Clases.ParametroReporte With { _
                                .Codigo = Comon.Constantes.CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_SUBCANAL,
                                .DescripcionValor = resultadoBusca(indice).SubCanales.First.Identificador})

                objParametros.Add(New Clases.ParametroReporte With { _
                                .Codigo = Comon.Constantes.CODIGO_PARAMETRO_REPORTE_CODIGO_SUBCANAL,
                                .DescripcionValor = resultadoBusca(indice).SubCanales.First.Codigo})

                .Reportes.Add(New ContractoServicio.Contractos.GenesisSaldos.Reporte.GenerarInforme.Reporte With { _
                                         .CodigoDelegacion = resultadoBusca(indice).CodigosDelegaciones.First, _
                                         .IdentificadorConfiguracionReporte = resultadoBusca(indice).ConfigReporte.Identificador, _
                                         .TipoReporte = Enumeradores.TipoReporte.Certificacion, _
                                         .ParametrosReporte = objParametros})
            Next

            Dim objRespuesta As ContractoServicio.Contractos.GenesisSaldos.Reporte.GenerarInforme.Respuesta = _
                LogicaNegocio.GenesisSaldos.GenerarInforme.Ejecutar(objPeticion)

            If objRespuesta IsNot Nothing Then

                If objRespuesta.Excepciones.Count = 0 AndAlso objRespuesta.Mensajes.Count = 0 Then

                    Me.EjecutarBusquedaCertificado()
                    upnSearchResults.Update()

                Else

                    If objRespuesta.Excepciones.Count > 0 Then
                        Throw New Exception(Join((From e In objRespuesta.Excepciones Select e).ToArray, vbCrLf))
                    Else
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                             (Join((From e In objRespuesta.Mensajes Select e).ToArray, vbCrLf)))
                    End If

                End If

            End If

        End With

    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Sub DownloadArchivoReporte(CodigoCertificado As String,
                                             CodigoEstado As String,
                                             CodigoConfigReporte As String,
                                             CodigoSubCanal As String)

        If Not String.IsNullOrEmpty(CodigoCertificado) AndAlso Not String.IsNullOrEmpty(CodigoEstado) AndAlso _
            Not String.IsNullOrEmpty(CodigoConfigReporte) AndAlso Not String.IsNullOrEmpty(CodigoSubCanal) Then

            Dim DireccionReporte As String = String.Empty
            If CodigoEstado = ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO Then
                DireccionReporte = String.Format(CarpetaReportes & "\{0}_{1}_{2}.pdf", CodigoCertificado, CodigoConfigReporte, CodigoSubCanal)
            Else
                DireccionReporte = String.Format(CarpetaReportes & "\{0}_{1}_{2}.xls", CodigoCertificado, CodigoConfigReporte, CodigoSubCanal)
            End If

            If Not File.Exists(DireccionReporte) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("069_msg_Arquivo_Inexistente"))
            End If

            HttpContext.Current.Session("DireccionReporte") = DireccionReporte

        End If


    End Sub

    Public Sub ucConvertirCertificado_OnControleAtualizado(reabrirModal As Boolean) Handles ucConvertirCertificado.UpdatedControl
        Try
            btnBuscar_Click(Nothing, Nothing)
            upnSearchResults.Update()
            Dim script As String = String.Format("displayElemento('{0}','none');", dvConvertirCertificadoPopup.ClientID)
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), Me.ID, script, True)

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub
#End Region
    <System.Web.Services.WebMethod()> _
    Public Shared Function ConverterCertificado(indexExecucao As Integer, total As Integer) As String

        Dim _respuesta As New ContractoServicio.Comon.BaseRespuestaJSON
        Dim certificadosConvertidos As New List(Of Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion.Certificado)
        Try
            Dim certificadosSelecionados As List(Of Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion.Certificado) = HttpContext.Current.Session("CertificadosSelecionados")

            Dim objCertificadoAtual = certificadosSelecionados(indexExecucao)

            Dim do_tipo As String = String.Empty
            Dim para_tipo As String = String.Empty
            If objCertificadoAtual.CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_SIN_CIERRE Then
                do_tipo = Traduzir("073_estado_certificado_" + Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_SIN_CIERRE)
                para_tipo = Traduzir("073_estado_certificado_" + Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE)
            ElseIf objCertificadoAtual.CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE Then
                do_tipo = Traduzir("073_estado_certificado_" + Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE)
                para_tipo = Traduzir("073_estado_certificado_" + Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO)
            End If

            _respuesta.Respuesta = String.Format(Traduzir("073_lbl_convertendo"), do_tipo, objCertificadoAtual.Cliente.Descripcion, para_tipo, "[faltam]", total)

            Dim objPeticion As New Genesis.ContractoServicio.GenesisSaldos.Certificacion.DatosCertificacion.Peticion
            With objPeticion

                .DelegacionLogada = HttpContext.Current.Session("Delegacion")
                .UsuarioCreacion = HttpContext.Current.Session("Usuario")

                Dim objAccionObtenerCertificado As New Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Certificacion.AccionRecuperarFiltrosCertificado
                Dim objCertificado = objAccionObtenerCertificado.Ejecutar(New Genesis.ContractoServicio.GenesisSaldos.Certificacion.RecuperarFiltrosCertificado.Peticion With {
                                                                        .IdentificadorCertificado = objCertificadoAtual.IdentificadorCertificado, .Delegacion = Base.InformacionUsuario.DelegacionSeleccionada
                                                                        })

                .CodigoCertificado = objCertificado.CodigoCertificado
                If objCertificado.CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_SIN_CIERRE Then
                    .CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE
                ElseIf objCertificado.CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE Then
                    .CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO
                    .CodigoCertificadoDefinitivo = objCertificado.CodigoCertificado
                End If

                .Cliente = New Clases.Cliente With {
                                            .Codigo = objCertificado.CodigoCliente,
                                            .Identificador = objCertificado.IdentificadorCliente,
                                            .Descripcion = objCertificado.DescripcionCliente
                                            }

                .EsTodasDelegaciones = objCertificado.TodasDelegaciones
                If objCertificado.Delegaciones IsNot Nothing AndAlso objCertificado.Delegaciones.Count > 0 Then
                    .CodigosDelegaciones = objCertificado.Delegaciones.Select(Function(a) a.Codigo).ToList()
                End If

                .EsTodosSectores = objCertificado.TodosSectores
                If objCertificado.Sectores IsNot Nothing AndAlso objCertificado.Sectores.Count > 0 Then
                    .CodigosSectores = objCertificado.Sectores.Select(Function(a) a.Codigo).ToList()
                End If

                .EsTodosCanales = objCertificado.TodosSubCanales
                If objCertificado.SubCanales IsNot Nothing AndAlso objCertificado.SubCanales.Count > 0 Then
                    .CodigosSubCanales = objCertificado.SubCanales.Select(Function(a) a.Codigo).ToList()
                End If

                .FyhCertificado = objCertificado.FechaHoraCertificado
            End With

            Dim objAccion As New Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Certificacion.AccionConvertirCertificado()
            Dim objRespuesta = objAccion.Convertir(objPeticion)

            If Not String.IsNullOrEmpty(objRespuesta.MensajeError) Then
                objCertificadoAtual.IdentificadorCertificadoAnterior = objRespuesta.MensajeError
                objCertificadoAtual.CodigoEstado = "ERROR"
            Else
                objCertificadoAtual.CodigoEstado = "OK"
            End If

        Catch ex As Exception
            _respuesta.CodigoError = "100"
            _respuesta.MensajeError = Traduzir("msg_producidoError") & ex.Message()
            _respuesta.MensajeErrorDescriptiva = ex.Message
            _respuesta.Respuesta = Nothing
        End Try

        Return JsonConvert.SerializeObject(_respuesta)
    End Function
End Class

Public Class FiltroResultadoCertificado

    Public Property Delegacion As Clases.Delegacion
    Public Property Cliente As Clases.Cliente
    Public Property FechaCertificado As DateTime
    Public Property TipoCertificado As String

End Class