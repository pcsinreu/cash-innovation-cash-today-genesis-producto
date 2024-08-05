Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Report

Public Class SeguimientoBultos
    Inherits Base

    ''' <summary>
    ''' Definição de parâmetros
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.RELATORIO_SEGUIMIENTO_BULTOS
        MyBase.ValidarAcesso = True
    End Sub

    'Protected Overrides Sub ConfigurarTabIndex()
    '    MyBase.ConfigurarTabIndex()
    '    Aplicacao.Util.Utilidad.ConfigurarTabIndex(Me.Codigo, Me.ucListaFiltrarPor)
    'End Sub


    ''' <summary>
    ''' Traduz os controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Claudioniz.Pereira] 06/06/2013 - Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()
        Master.Titulo = Traduzir("060_lblTitulo")

        Me.lblSubTitulo.Text = Traduzir("060_lblSubTitulo")

        Me.Codigo.Titulo = Traduzir("060_lblCodigo")
        Me.ucListaFiltrarPor.Titulo = Traduzir("060_FiltrarPor")
        Me.ucFormato.Titulo = Traduzir("060_FormatoGenerar")
        Me.btnGenerarReporte.Text = Traduzir("060_btnGerar")
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ScriptManager.GetCurrent(Me.Page).RegisterPostBackControl(btnGenerarReporte)
        If Not Me.IsPostBack Then
            AjustarControlFiltrarPor()
            AjustarControlFormato()
            Me.ucListaFiltrarPor.Focus()
        End If
    End Sub

    Private Sub AjustarControlFiltrarPor()
        Dim lista As New List(Of KeyValuePair(Of String, String))
        lista.Add(New KeyValuePair(Of String, String)("NumeroExterno", Traduzir("060_FiltrarNumeroExterno")))
        lista.Add(New KeyValuePair(Of String, String)("CodigoBolsa", Traduzir("060_FiltrarCodigoBolsa")))
        lista.Add(New KeyValuePair(Of String, String)("Precinto", Traduzir("060_FiltrarPrecinto")))
        Me.ucListaFiltrarPor.Opciones = lista
    End Sub

    Private Sub AjustarControlFormato()
        Dim lista As New List(Of KeyValuePair(Of String, String))
        lista.Add(New KeyValuePair(Of String, String)("PDF", Traduzir("060_formato_pdf")))
        lista.Add(New KeyValuePair(Of String, String)("EXCEL", Traduzir("060_formato_excel")))
        Me.ucFormato.Opciones = lista
    End Sub

    Private Function ValidarGerar() As String
        Dim retorno As String = String.Empty
        Me.ucListaFiltrarPor.GuardarDatos()
        Me.Codigo.GuardarDatos()
        Me.ucFormato.GuardarDatos()
        If Me.ucListaFiltrarPor.ItemSelecionado Is Nothing OrElse String.IsNullOrEmpty(Me.Codigo.Valor) Then
            retorno = Traduzir("060_InformaCodigo")
        End If

        Return retorno
    End Function

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
                ' Recupera os parametros do relatório.
                Dim objReport As New Prosegur.Genesis.Report.Gerar()
                objReport.Autenticar(False)

                Dim listaParametros2010 As List(Of RS2010.ItemParameter)
                Dim objValores2010 As RS2010.ParameterValue() = Nothing

                'Lista os parametros do relatório
                Dim dirRelatorio = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CarpetaReportes")
                Dim nomeRelatorio As String = "SEGUIMIENTO_BULTOS"
                Dim fullPathReport As String = Nothing

                ' Verificar se existe "/" no final da URL configurada para o Report
                If (dirRelatorio.Substring(dirRelatorio.Length - 1, 1) = "/") Then
                    dirRelatorio = dirRelatorio.Substring(0, dirRelatorio.Length - 1)
                End If

                fullPathReport = String.Format("{0}/SEGUIMIENTO_BULTOS/{1}", dirRelatorio, nomeRelatorio)
                listaParametros2010 = objReport.ListarParametros(fullPathReport, objValores2010)

                Dim listaParametros As New List(Of RSE.ParameterValue)


                'Parametros de execução
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "TIPO", .Value = ucListaFiltrarPor.ItemSelecionado})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COD_EXTERNO", .Value = If(ucListaFiltrarPor.ItemSelecionado = "NumeroExterno", Codigo.Valor, String.Empty)})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COD_BOLSA", .Value = If(ucListaFiltrarPor.ItemSelecionado = "CodigoBolsa", Codigo.Valor, String.Empty)})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "P_COD_PRECINTO", .Value = If(ucListaFiltrarPor.ItemSelecionado = "Precinto", Codigo.Valor, String.Empty)})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "Usuario", .Value = InformacionUsuario.Nombre & " " & InformacionUsuario.Apelido})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "URLDocumento", .Value = Request.Url.Host & ":" & Request.Url.Port & "/" & Request.Url.Segments(1) & Request.Url.Segments(2) & "Documento.aspx?IdentificadorDocumento="})

                'Parametros para tradução de textos
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "TradDireccion", .Value = Traduzir("060_TradDireccion")})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "TradNombreReporte", .Value = Traduzir("060_TradNombreReporte")})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "TradPagina", .Value = Traduzir("060_TradPagina")})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "TradCodigoExterno", .Value = Traduzir("060_TradCodigoExterno")})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "TradUsuario", .Value = Traduzir("060_TradUsuario")})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "TradFecha", .Value = Traduzir("060_TradFecha")})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "TradFormulario", .Value = Traduzir("060_TradFormulario")})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "TradBulto", .Value = Traduzir("060_TradBulto")})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "TradPrecinto", .Value = Traduzir("060_TradPrecinto")})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "TradTipoDestino", .Value = Traduzir("060_TradTipoDestino")})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "TradCertificado", .Value = Traduzir("060_TradCertificado")})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "TradSectorOrigen", .Value = Traduzir("060_TradSectorOrigen")})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "TradSectorDestino", .Value = Traduzir("060_TradSectorDestino")})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "TradSubCanalOrigen", .Value = Traduzir("060_TradSubCanalOrigen")})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "TradSubCanalDestino", .Value = Traduzir("060_TradSubCanalDestino")})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "TradClienteOrigen", .Value = Traduzir("060_TradClienteOrigen")})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "TradClienteDestino", .Value = Traduzir("060_TradClienteDestino")})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "TradOrigen", .Value = Traduzir("060_TradOrigen")})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "TradDestino", .Value = Traduzir("060_TradDestino")})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "TradProsegur", .Value = Traduzir("060_TradProsegur")})


                Dim extensao = If(Me.ucFormato.ItemSelecionado = "PDF", "pdf", "xls")

                Dim Buffer = objReport.RenderReport(fullPathReport, Me.ucFormato.ItemSelecionado, listaParametros, extensao)
                Response.ClearHeaders()
                Response.ClearContent()
                Response.Clear()
                Response.ContentType = "application/octet-stream"
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}.{1}", nomeRelatorio, extensao))
                Response.AddHeader("Content-Length", Buffer.Length)
                Response.BinaryWrite(Buffer)
                Response.Buffer = True
                Response.Flush()
                Response.Clear()
                Response.End()
            Else
                MyBase.MostraMensagemErro(mensagem)
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub


End Class