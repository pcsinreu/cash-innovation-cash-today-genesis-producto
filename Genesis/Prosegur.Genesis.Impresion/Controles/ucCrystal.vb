Imports Prosegur.Framework.Dicionario.Tradutor
Imports CrystalDecisions.Shared
Imports System.Windows.Forms
Imports System.Drawing

Public Class ucCrystal

#Region "[VARIAVEIS]"

    Private _TituloRelatorio As String
    Private _Report As String
    Private _FonteDados As Object
    Private _NomeArquivo As String
    Private _ExportarArquivo As Boolean = True
    Private _ImprimirArquivo As Boolean = False
    Private _LogEnDisco As Boolean = False

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

    Public Property NomeArquivo() As String
        Get
            Return _NomeArquivo
        End Get
        Set(value As String)
            _NomeArquivo = value
        End Set
    End Property

    Public ReadOnly Property Relatorio() As CrystalDecisions.Windows.Forms.CrystalReportViewer
        Get
            Return crvRelatorios
        End Get
    End Property

    Public Property ExportarArquivo() As Boolean
        Get
            Return _ExportarArquivo
        End Get
        Set(value As Boolean)
            _ExportarArquivo = value
        End Set
    End Property

    Public Property ImprimeArquivo() As Boolean
        Get
            Return _ImprimirArquivo
        End Get
        Set(value As Boolean)
            _ImprimirArquivo = value
        End Set
    End Property

    Public Property LogEnDisco() As Boolean
        Get
            Return _LogEnDisco
        End Get
        Set(value As Boolean)
            _LogEnDisco = value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Carrega o relatório
    ''' </summary>
    ''' <history>[jorge.viana]	24/08/2010	Creado</history>
    ''' <remarks></remarks>
    Public Function CarregarRelatorio(Optional printName As String = Nothing, Optional EsPrimerObjeto As Boolean = True, Optional contemBolsa As Boolean = False) As String
        Dim retorno As String = Nothing
        Try

            ' mensagem de log
            If LogEnDisco Then
                Impresion.Util.LogMensagemEmDisco("      ReciboTransporteF22 - Inicio Criar Arquivo")
            End If

            ' Define a cultura do relatório
            CrystalDecisions.Shared.SharedUtils.RequestLcid = Threading.Thread.CurrentThread.CurrentCulture.LCID

            ' Cria um novo ReportDocument
            Dim objReportDoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument()

            ' Carrega o relatório que vai ser montado
            objReportDoc.Load(String.Format("{0}/{1}", My.Application.Info.DirectoryPath, _Report))

            ' mensagem de log
            If LogEnDisco Then
                Impresion.Util.LogMensagemEmDisco("         ReciboTransporteF22 - objReportDoc: " & objReportDoc.Name)
            End If

            ' Define as legendas do relatório de acordo com a cultura
            Util.TraduzirRelatorio(objReportDoc, Me._TituloRelatorio)

            ' Atualiza o relatório
            objReportDoc.Refresh()

            ' Define a fonte de dados do relatório
            objReportDoc.SetDataSource(FonteDados)

            ' dispara o evento AtualizarControlesRelatorio
            RaiseEvent AtualizarControlesRelatorio(objReportDoc)

            'verifica se o arquivo PDF deve ser exportado ou não
            If _ExportarArquivo Then

                'armazena o caminho completo do arquivo PDF
                Dim caminhoArq As String = Util.RetornaCaminhoFisicoRelatorioPDF(_NomeArquivo)

                ' mensagem de log
                If LogEnDisco Then
                    Impresion.Util.LogMensagemEmDisco("         ReciboTransporteF22 - caminhoArq: " & caminhoArq)
                End If

                'Exporta o arquivo PDF para o caminho recuperado acima
                objReportDoc.ExportToDisk(ExportFormatType.PortableDocFormat, caminhoArq)

                If _ImprimirArquivo Then
                    retorno = Util.ImprimirArquivo("ReciboTransporteF22", caminhoArq, LogEnDisco, printName, EsPrimerObjeto)
                End If

            Else
                'Relatório será exibido no formato PDF
                crvRelatorios.ReportSource = objReportDoc
            End If

            ' Cierra el reporte
            objReportDoc.Close()
            objReportDoc.Dispose()

            ' mensagem de log
            If LogEnDisco Then
                Impresion.Util.LogMensagemEmDisco("      ReciboTransporteF22 - Fim Impressão")
            End If

        Catch ex As IO.IOException
            'Exibe uma mensagem informando que houve algum erro ao gerar o arquivo PDF
            MessageBox.Show(String.Format(Traduzir("rpt_027_errogerarpdf"), ex.Message), _
                            Traduzir("imp_titulo_msgbox"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            Throw
        End Try

        Return retorno
    End Function

#End Region

#Region "[EVENTOS]"

    'Evento que permite o usuário atualizar controles do relatório
    Public Delegate Sub AtualizarControlesRelatorioDelegate(pReport As CrystalDecisions.CrystalReports.Engine.ReportDocument)
    Public Event AtualizarControlesRelatorio As AtualizarControlesRelatorioDelegate

    ''' <summary>
    ''' Evento Load do controle ucCrystal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>[jorge.viana]	24/08/2010	Creado</history>
    ''' <remarks></remarks>
    Private Sub ucCrystal_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ' Carrega o relatório
        Me.CarregarRelatorio()
    End Sub

#End Region

End Class
