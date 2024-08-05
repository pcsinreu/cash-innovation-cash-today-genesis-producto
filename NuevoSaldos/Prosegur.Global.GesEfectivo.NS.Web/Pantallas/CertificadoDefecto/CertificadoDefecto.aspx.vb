Imports Prosegur.Genesis.Report.RSE
Imports Prosegur.Genesis.Report
Imports System.IO
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Genesis.Web.Login
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos
Imports Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Certificacion

''' <summary>
''' Certificado Defecto - Reporte del certificado
''' </summary>
''' <remarks></remarks>
Public Class CertificadoDefecto
    Inherits Base

#Region "Variaveis"

    Public Property objRespuestaCertificado() As Certificacion.RecuperarFiltrosCertificado.Respuesta
        Get
            Return ViewState("RespuestaCertificado")
        End Get
        Set(value As Certificacion.RecuperarFiltrosCertificado.Respuesta)
            ViewState("RespuestaCertificado") = value
        End Set
    End Property

#End Region

#Region "Overrides"

    ''' <summary>
    ''' Definição de parâmetros
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        ' MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.ABM_PADRAO
        MyBase.ValidarAcesso = False

    End Sub

    ' ''' <summary>
    ' ''' Configuración del foco.
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Protected Overrides Sub ConfigurarTabIndex()
    '    txtCertificado.Focus()

    'End Sub

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento Load de la pagina
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ScriptManager.GetCurrent(Me.Page).RegisterPostBackControl(btnGenerarReporte)
        If Not Me.IsPostBack Then
            Me.txtCertificado.Text = Traduzir("005_msgComboVazio")
            Me.hidLegendaCertificado.Value = Traduzir("005_msgComboVazio")
            Me.ControleCampos(Not String.IsNullOrEmpty(Me.txtCertificado.Text.Trim) AndAlso Not String.IsNullOrEmpty(Me.ddlSubCanal.SelectedValue))
        End If
    End Sub

    ''' <summary>
    ''' Traduz os controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Claudioniz.Pereira] 06/06/2013 - Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()
        Master.Titulo = Traduzir("005_lblTitulo")
        Me.lblFiltro.Text = Traduzir("005_lblFiltro")
        Me.lblCodigoCertificado.Text = Traduzir("005_lblCodigoCertificado")
        Me.lblSubCanal.Text = Traduzir("005_lblSubCanal")
        Me.btnGenerarReporte.Text = Traduzir("005_btnGerar")
    End Sub

    ''' <summary>
    ''' Click del botón generar reporte.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnGenerarReporte_Click(sender As Object, e As EventArgs) Handles btnGenerarReporte.Click

        Try

            Dim mensagem = ValidarGerar()

            If (mensagem = String.Empty) Then
                ExportarExcel()
            Else
                MyBase.MostraMensagemErro(mensagem)
            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Changed del campo Codigo del certificado.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub txtCertificado_TextChanged(sender As Object, e As EventArgs) Handles txtCertificado.TextChanged
        Try

            If String.IsNullOrEmpty(txtCertificado.Text) Then
                Me.ControleCampos(False)
            End If

            ' Verifica se o valor digitado é númerico
            If Not String.IsNullOrEmpty(Me.txtCertificado.Text.Trim) Then
                Me.CarregarSubCanales()
            End If

            'If Me.ddlDelegacion.Items.Count = 0 AndAlso Me.ddlSubCanal.Items.Count = 0 Then
            If Me.ddlSubCanal.Items.Count = 0 Then
                Me.txtCertificado.Text = Traduzir("005_msgComboVazio")
                Me.ControleCampos(False)
                MyBase.MostraMensagemErro(Traduzir("005_msgCertificadoNoEncontrato"))

            End If

            'If Not ddlDelegacion.SelectedValue.Equals(String.Empty) AndAlso _
            'If Not ddlSubCanal.SelectedValue.Equals(String.Empty) Then
            Me.ControleCampos(Not String.IsNullOrEmpty(ddlSubCanal.SelectedValue))
            'Me.btnGerarReporte.Enabled = True
            'Me.ddlSubCanal.Enabled = True
            'End If

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Exportar Datos para el archivo excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ExportarExcel()

        ' Recupera os parametros do relatório.
        Dim objReport As New Prosegur.Genesis.Report.Gerar()
        objReport.Autenticar(False)

        Dim listaParametros2010 As List(Of RS2010.ItemParameter)
        Dim objValores2010 As RS2010.ParameterValue() = Nothing

        'Lista os parametros do relatório
        Dim dirRelatorio = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CarpetaReportes")
        Dim nomeRelatorio As String = "RptResumenDeCuenta"
        Dim fullPathReport As String = Nothing

        ' Verificar se existe "/" no final da URL configurada para o Report
        If (dirRelatorio.Substring(dirRelatorio.Length - 1, 1) = "/") Then
            dirRelatorio = dirRelatorio.Substring(0, dirRelatorio.Length - 1)
        End If
        fullPathReport = String.Format("{0}/RESUMEN_DE_CUENTA/{1}", dirRelatorio, nomeRelatorio)
        listaParametros2010 = objReport.ListarParametros(fullPathReport, objValores2010)

        Dim listaParametros As New List(Of RSE.ParameterValue)
        Dim bolCertificadoConsulta As Integer = 0

        If (objRespuestaCertificado.CodigoEstado <> "CO") Then
            bolCertificadoConsulta = 0
        Else
            bolCertificadoConsulta = 1
        End If
        listaParametros.Add(New RSE.ParameterValue() With {.Name = "OID_CERTIFICADO", .Value = objRespuestaCertificado.IdentificadorCertificado})
        listaParametros.Add(New RSE.ParameterValue() With {.Name = "OID_SUBCANAL", .Value = Me.ddlSubCanal.SelectedValue})
        listaParametros.Add(New RSE.ParameterValue() With {.Name = "BOL_CONSULTA", .Value = bolCertificadoConsulta})

        If (objRespuestaCertificado.Delegaciones.Count() > 0) Then
            listaParametros.Add(New RSE.ParameterValue() With {.Name = "LISTA_PLANTAS", .Value = Join(objRespuestaCertificado.Delegaciones.Select(Function(d) Join(d.Plantas.Select(Function(p) String.Format("{0}-{1}", p.Codigo, p.Descripcion)).Distinct().ToArray(), ", ")).Distinct().ToArray(), ", ")})
        Else
            listaParametros.Add(New RSE.ParameterValue() With {.Name = "LISTA_PLANTAS", .Value = ""})
        End If

        Dim Buffer = objReport.RenderReport(fullPathReport, "Excel", listaParametros, "xls")
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}.xls", objRespuestaCertificado.CodigoCertificado))
        Response.AddHeader("Content-Length", Buffer.Length)
        Response.BinaryWrite(Buffer)
        Response.Buffer = True
        Response.Flush()
        Response.Clear()
        Response.End()

    End Sub

    ''' <summary>
    ''' Cargar los subcanales correspondientes al codigo del certificado informado.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CarregarSubCanales()

        Try

            Dim objPeticionCertificado As New Certificacion.RecuperarFiltrosCertificado.Peticion With {.CodigoCertificado = HttpUtility.HtmlEncode(Me.txtCertificado.Text.Trim), .Delegacion = InformacionUsuario.DelegacionSeleccionada}

            Dim objAccion As New AccionRecuperarFiltrosCertificado()
            objRespuestaCertificado = objAccion.Ejecutar(objPeticionCertificado)

            If Not MyBase.VerificaRespuestaSemErro(objRespuestaCertificado.CodigoError) Then
                MyBase.MostraMensagemErro(objRespuestaCertificado.MensajeError)
                Exit Sub
            End If

            If Not String.IsNullOrEmpty(objRespuestaCertificado.IdentificadorCertificado) AndAlso objRespuestaCertificado.SubCanales IsNot Nothing AndAlso _
                objRespuestaCertificado.SubCanales.Count > 0 Then

                Me.ddlSubCanal.Items.Clear()
                For Each subcanal In objRespuestaCertificado.SubCanales
                    Me.ddlSubCanal.Items.Add(New ListItem(subcanal.Descripcion, subcanal.Identificador))
                Next

            Else
                MyBase.MostraMensagemErro(Traduzir("004_msgSinRegistro"))
            End If

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Validación de los campos informados.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidarGerar() As String
        Dim sb As New StringBuilder
        If String.IsNullOrEmpty(Me.txtCertificado.Text.Trim()) Then
            sb.AppendFormat(Traduzir("msg_campo_obrigatorio"), Traduzir("005_lblCodigoCertificado"))
            sb.AppendLine()
        End If

        If Me.ddlSubCanal.SelectedValue.Equals(String.Empty) Then
            sb.AppendFormat(Traduzir("msg_campo_obrigatorio"), Traduzir("005_lblSubCanal"))
        End If

        Return sb.ToString()
    End Function

    ''' <summary>
    ''' Controle de habilitación de los campos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ControleCampos(Estado As Boolean)

        If Not Estado Then Me.ddlSubCanal.Items.Clear()
        Me.btnGenerarReporte.Enabled = Estado
        Me.ddlSubCanal.Enabled = Estado

    End Sub

#End Region

End Class