Imports Prosegur.Framework.Dicionario.Tradutor
Imports CrystalDecisions.Shared

Partial Public Class Crystal
    Inherits System.Web.UI.UserControl

#Region "[VARIAVEIS]"

    Private _TituloRelatorio As String
    Private _Report As String
    Private _TipoRelatorio As ContractoServ.Enumeradores.eFormatoSalida = ContractoServ.Enumeradores.eFormatoSalida.CSV
    Private _FonteDados As Object
    Private _NomeArquivo As String

#End Region

#Region "[PROPRIEDADES]"

    Public Property TituloRelatorio() As String
        Get
            Return _TituloRelatorio
        End Get
        Set(value As String)
            _TituloRelatorio = value
        End Set
    End Property

    Public Property Report() As String
        Get
            Return _Report
        End Get
        Set(value As String)
            _Report = value
        End Set
    End Property

    Public Property FonteDados() As Object
        Get
            Return _FonteDados
        End Get
        Set(value As Object)
            _FonteDados = value
        End Set
    End Property

    Public Property TipoRelatorio() As ContractoServ.Enumeradores.eFormatoSalida
        Get
            Return _TipoRelatorio
        End Get
        Set(value As ContractoServ.Enumeradores.eFormatoSalida)
            _TipoRelatorio = value
        End Set
    End Property

    Public Property NomeArquivo() As String
        Get
            Return _NomeArquivo
        End Get
        Set(value As String)
            _NomeArquivo = value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Carrega o relatório
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 26/06/2009
    ''' </history>
    Private Sub CarregarRelatorio()

        Dim objReportDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument = Nothing

        Try
            ' Define a cultura corrente
            Threading.Thread.CurrentThread.CurrentCulture = Util.ResolveCultureFromBrowser()

            ' Define a cultura do relatório
            CrystalDecisions.Shared.SharedUtils.RequestLcid = Threading.Thread.CurrentThread.CurrentCulture.LCID

            ' Cria um novo ReportDocument
            objReportDoc = New CrystalDecisions.CrystalReports.Engine.ReportDocument()

            ' Carrega o relatório que vai ser montado
            objReportDoc.Load(Server.MapPath("~/Reportes/" & Report), OpenReportMethod.OpenReportByTempCopy)

            ' Define as legendas do relatório de acordo com a cultura
            Util.TraduzirRelatorio(objReportDoc, Me._TituloRelatorio)

            ' Atualiza o relatório
            objReportDoc.Refresh()

            ' Define a fonte de dados do relatório
            objReportDoc.SetDataSource(FonteDados)

            ' Define o tipo de exportação do relatório
            Select Case Me._TipoRelatorio

                Case ContractoServ.Enumeradores.eFormatoSalida.PDF
                    ' Relatório será exportado no formato PDF
                    crvRelatorios.Visible = False
                    objReportDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Me.Response, True, _NomeArquivo)

            End Select

        Catch ex As Exception

            ' se a página contém o "ControleErro", se sim, executa o método "TratarErroException" enviando a exception 
            If Page.GetType().GetProperty("ControleErro") IsNot Nothing Then
                Page.GetType().GetProperty("ControleErro").GetValue(Page, Nothing).GetType().InvokeMember("TratarErroException", Reflection.BindingFlags.InvokeMethod OrElse Reflection.BindingFlags.Public OrElse Reflection.BindingFlags.Instance, Nothing, Page.GetType().GetProperty("ControleErro").GetValue(Page, Nothing), New Object() {ex})
            End If

        Finally

            If objReportDoc IsNot Nothing Then
                objReportDoc.Close()
                objReportDoc.Dispose()
                GC.Collect()
            End If

        End Try
    End Sub

#End Region

#Region "[EVENTOS]"

    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init

        ' Carrega o relatório
        Me.CarregarRelatorio()

    End Sub

#End Region

End Class