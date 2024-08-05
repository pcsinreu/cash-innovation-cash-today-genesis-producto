Imports Prosegur.Framework.Dicionario
Imports System.IO
Imports System.Xml
Imports Microsoft.Reporting.WinForms
Imports System.Drawing
Imports System.Windows.Forms

Namespace NuevoSalidas

    ''' <summary>
    ''' Classe Impresion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gfraga] 23/08/2010 Criado
    ''' </history>
    Public Class GesImpresion

#Region "[CONSTANTES]"
#End Region

#Region "[VARIÁVEIS]"

        ' Qdo for impressão Windows A4, trava o envio de páginas para a impressora para serem enviadas de uma vez só ao final da impressão
        Private Shared _travarPaginas As Boolean = False

#End Region

#Region "[PROPRIEDADES]"

        Public Shared ReadOnly Property EnvioPaginasTravado() As Boolean
            Get
                Return _travarPaginas
            End Get
        End Property

#End Region

#Region "[METODOS]"

        ''' <summary>
        ''' Imprime etiqueta por bulto
        ''' </summary>
        ''' <param name="pObjBulto">Estrutura de dados que será exibido na impressão</param>
        ''' <param name="pImpressoraNome">Nome da Impressora que será usada para impressão</param>
        ''' <history>[gfraga] 22/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Shared Sub EtiquetaBulto(pObjBulto As Impresion.NuevoSalidas.Ticket.Bulto.Parametros.Bulto, _
                                        pImpressoraNome As String)

            'Esta etiqueta utilizará apenas impressora ZEBRA
            'Chama o método para realizar a impressão passando o nome da impressora.
            Impresion.NuevoSalidas.Ticket.Bulto.Zebra.Layout.Imprimir(pObjBulto, pImpressoraNome)

        End Sub

        ''' <summary>
        ''' Método que valida as informações que irão gerar o recibo Transporte F22 - Argentina
        ''' </summary>
        ''' <param name="objRemesa"></param>
        ''' <history>[jorge.viana]	31/08/2010	Creado</history>
        ''' <remarks></remarks>
        Private Shared Sub ValidarDadosReciboTransporteF22(objRemesa As Impresion.NuevoSalidas.Recibo.TransporteF22.Parametros.Remesa)

            'variável que armazena as mensagens de validação
            Dim lstDados As New System.Text.StringBuilder

            'valida se a remessa não está nula
            If objRemesa IsNot Nothing Then
                'valida a quantidade de bultos
                If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 30 Then
                    lstDados.AppendLine(Tradutor.Traduzir("rpt_027_validacion_qtdebultos"))
                End If
                'verifica se existe efectivos
                If objRemesa.Efectivos IsNot Nothing Then
                    'valida a quantidade de divisas
                    If objRemesa.Efectivos.Count > 8 Then
                        lstDados.AppendLine(Tradutor.Traduzir("rpt_027_validacion_qtdedivisas"))
                    Else
                        'valida a quantidade de denominações
                        Dim fDen = (From e In objRemesa.Efectivos, d In e.EfectivoDetalles _
                                    Where (e.EfectivoDetalles IsNot Nothing AndAlso e.EfectivoDetalles.Count > 0) _
                                    Select d.CodDenominacion).Count
                        If fDen > 96 Then
                            lstDados.AppendLine(Tradutor.Traduzir("rpt_027_validacion_qtdedenominaciones"))
                        End If
                    End If
                End If
            Else
                'informa que não há informações para gerar o recibo
                lstDados.AppendLine(Tradutor.Traduzir("rpt_027_requerido_remesa"))
            End If

            'verifica se algum campo não foi preenchido e levanta uma exception do tipo negocio
            If lstDados.Length > 0 Then
                'Levanta uma exception do tipo NegocioException
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, lstDados.ToString)
            End If

        End Sub

        ''' <summary>
        ''' Imprime o recibo Transporte F22 (Argentina)
        ''' </summary>
        ''' <param name="objRemesa"></param>
        ''' <history>[jorge.viana]	24/08/2010	Creado</history>
        ''' <remarks></remarks>
        Public Shared Function ReciboTransporteF22(objRemesa As Impresion.NuevoSalidas.Recibo.TransporteF22.Parametros.Remesa,
                                                   ImprimeArquivo As Boolean,
                                                   LogEnDisco As Boolean,
                                          Optional printName As String = Nothing,
                                          Optional EsPrimerObjeto As Boolean = True) As String

            Dim imprimir As String = String.Empty

            If objRemesa.TipoReciboRemesa = TipoReciboRemesa.Argentina Then
                imprimir = ReciboTransporteF22Argentina(objRemesa, ImprimeArquivo, LogEnDisco, printName, EsPrimerObjeto)

            ElseIf objRemesa.TipoReciboRemesa = TipoReciboRemesa.Peru Then
                imprimir = ReciboTransporteF22Peru(objRemesa, ImprimeArquivo, LogEnDisco, printName, EsPrimerObjeto)
            End If

            Return imprimir
        End Function

        ''' <summary>
        ''' Imprime recibo Envio Puesto
        ''' </summary>
        ''' <param name="pObjMovimentacionFondo">Estrutura de dados que será exibido na impressão</param>
        ''' <param name="pLoginUsuario">Nome do Login do usuário</param>
        ''' <history>[gfraga] 25/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Shared Sub ReciboEnvioPuesto(pObjMovimentacionFondo As Impresion.NuevoSalidas.Recibo.EnvioPuesto.Parametros.MovimentacionFondo, _
                                            pLoginUsuario As String, _
                                            ImprimeArquivo As Boolean)

            Try
                'Armazena o nome do arquivo a ser gerado
                'Foi retirado o nome do usuário e a data para gerar o arquivo.
                Dim nomeArquivo As String = "ReciboEnvioPuesto"

                ' Cria o DataSet tipado
                Dim objDsReciboMovimentacionFondo As New dsReciboMovimentacionFondo

                ' Popula o data set com os dados recuperados do objeto
                objDsReciboMovimentacionFondo.PopularTabelas(pObjMovimentacionFondo)

                'Cria uma nova instância do controle Crystal
                Dim rptEnvioPuesto As New ucCrystal
                ' Passa o título do relatório
                rptEnvioPuesto.TituloRelatorio = Tradutor.Traduzir("rpt_027_titulo")
                ' Carrega os dados do relatório
                rptEnvioPuesto.FonteDados = objDsReciboMovimentacionFondo
                ' Carrega o reporte de acordo com o formato de saída
                rptEnvioPuesto.Report = "NuevoSalidas\Recibo\Windows_A4\crReciboMovimentacionFondo.rpt"
                ' Passa o nome do arquivo que deverá ser gerado
                rptEnvioPuesto.NomeArquivo = nomeArquivo
                ' Passa o parametro indicativo se é para imprimir
                rptEnvioPuesto.ImprimeArquivo = ImprimeArquivo

                'chama o método para exportar o arquivo PDF
                rptEnvioPuesto.CarregarRelatorio()

            Catch ex As Exception
                Throw
            End Try

        End Sub

        Public Shared Sub TravarImpressao()
            _travarPaginas = True
        End Sub

        Public Shared Sub LiberarImpressao()
            _travarPaginas = False
        End Sub

        Public Shared Sub FinalizarImpressao()

            If _travarPaginas Then
                _travarPaginas = False
            End If

        End Sub

        ''' <summary>
        ''' Imprime o recibo Transporte Peru (Argentina)
        ''' </summary>
        ''' <param name="objRemesa"></param>
        ''' <history>[Claudioniz]	20/10/2015	Creado</history>
        ''' <remarks></remarks>
        Public Shared Function ReciboTransporteF22Peru(objRemesa As Impresion.NuevoSalidas.Recibo.TransporteF22.Parametros.Remesa,
                                                   ImprimeArquivo As Boolean,
                                                   LogEnDisco As Boolean,
                                          Optional printName As String = Nothing,
                                          Optional EsPrimerObjeto As Boolean = True) As String
            Dim retorno As String = String.Empty
            Try


                Dim contemBolsa As Boolean = False

                'armazena o nome do arquivo a ser gerado
                Dim nomeArquivo As String = "ReciboTransporteF22"

                ' mensagem de log
                If LogEnDisco Then
                    Impresion.Util.LogMensagemEmDisco("   Inicio ReciboTransporteF22")
                End If

                'chama o método para validar as informações do relatório
                'ValidarDadosReciboTransporteF22(objRemesa)

                ' mensagem de log
                If LogEnDisco Then
                    Impresion.Util.LogMensagemEmDisco("      ReciboTransporteF22 - Validação OK")
                End If

                ' Cria o DataSet tipado
                Dim objDsTransporteF22 As New dsReciboTransporteF22Peru

                ' Popula o data set com os dados recuperados do objeto
                objDsTransporteF22.PopularTabelas(objRemesa, LogEnDisco)

                Dim objReportViewer = New ReportViewer()
                objReportViewer.LocalReport.DataSources.Clear()
                For index = 0 To objDsTransporteF22.Tables.Count - 1
                    objReportViewer.LocalReport.DataSources.Add(New ReportDataSource(objDsTransporteF22.Tables(index).TableName, objDsTransporteF22.Tables(index)))
                Next

                objReportViewer.LocalReport.ReportPath = String.Format("{0}/{1}", My.Application.Info.DirectoryPath, "NuevoSalidas\Recibo\Windows_A4\ReciboTransporteF22PeruDivisas.rdlc")
                objReportViewer.RefreshReport()

                AddHandler objReportViewer.LocalReport.SubreportProcessing, AddressOf SubreportProcessingEventHandler

                'Export to PDF
                Dim mimeType As String = Nothing
                Dim encoding As String = Nothing
                Dim fileNameExtension As String = Nothing
                Dim streams As String() = Nothing
                Dim warnings As Microsoft.Reporting.WinForms.Warning() = Nothing
                Dim pdfContent As Byte() = objReportViewer.LocalReport.Render("PDF", Nothing, mimeType, encoding, fileNameExtension, streams, warnings)

                Dim caminhoArq As String = Util.RetornaCaminhoFisicoRelatorioPDF(nomeArquivo)

                Using stream = New FileStream(caminhoArq, FileMode.Create, FileAccess.Write)
                    stream.Write(pdfContent, 0, pdfContent.Length)
                    stream.Close()
                End Using

                'verifica se o arquivo PDF deve ser exportado ou não
                If ImprimeArquivo Then
                    retorno = Util.ImprimirArquivo("ReciboTransporteF22", caminhoArq, LogEnDisco, printName, EsPrimerObjeto)
                End If

                ' mensagem de log
                If LogEnDisco Then
                    Util.LogMensagemEmDisco("   Fim ReciboTransportePeru")
                End If


            Catch ex As IO.IOException
                'Exibe uma mensagem informando que houve algum erro ao gerar o arquivo PDF
                MessageBox.Show(String.Format(Tradutor.Traduzir("rpt_027_errogerarpdf"), ex.Message), _
                                Tradutor.Traduzir("imp_titulo_msgbox"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                Throw
            End Try

            Return retorno

        End Function

        Public Shared Function ReciboTransporteF22Argentina(objRemesa As Impresion.NuevoSalidas.Recibo.TransporteF22.Parametros.Remesa,
                                                   ImprimeArquivo As Boolean,
                                                   LogEnDisco As Boolean,
                                          Optional printName As String = Nothing,
                                          Optional EsPrimerObjeto As Boolean = True) As String
            Dim contemBolsa As Boolean = False
            ' mensagem de log
            If LogEnDisco Then
                Impresion.Util.LogMensagemEmDisco("   Inicio ReciboTransporteF22")
            End If

            'chama o método para validar as informações do relatório
            ValidarDadosReciboTransporteF22(objRemesa)

            ' mensagem de log
            If LogEnDisco Then
                Impresion.Util.LogMensagemEmDisco("      ReciboTransporteF22 - Validação OK")
            End If

            'armazena o nome do arquivo a ser gerado
            Dim nomeArquivo As String = "ReciboTransporteF22"

            ' Cria o DataSet tipado
            Dim objDsTransporteF22 As New dsReciboTransporteF22Argentina

            ' Popula o data set com os dados recuperados do objeto
            objDsTransporteF22.PopularTabelas(objRemesa, LogEnDisco, contemBolsa)

            'Cria uma nova instância do controle Crystal
            Dim rptTransporteF22 As New ucCrystal

            ' Passa o título do relatório
            rptTransporteF22.TituloRelatorio = Tradutor.Traduzir("rpt_027_titulo")
            ' Carrega os dados do relatório
            rptTransporteF22.FonteDados = objDsTransporteF22
            ' Carrega o reporte de acordo com o formato de saída
            rptTransporteF22.Report = "NuevoSalidas\Recibo\Windows_A4\crReciboTransporteF22Argentina.rpt"
            ' Passa o nome do arquivo que deverá ser gerado
            rptTransporteF22.NomeArquivo = nomeArquivo
            ' Informa que o arquivo PDF deve ser impresso
            rptTransporteF22.ImprimeArquivo = ImprimeArquivo
            ' Gravar Log
            rptTransporteF22.LogEnDisco = LogEnDisco
            'chama o método para exportar o arquivo PDF
            Dim retorno = rptTransporteF22.CarregarRelatorio(printName, EsPrimerObjeto, contemBolsa)

            ' mensagem de log
            If LogEnDisco Then
                Util.LogMensagemEmDisco("   Fim ReciboTransporteF22")
            End If

            Return retorno

        End Function

        Private Shared Sub SubreportProcessingEventHandler(sender As Object, e As SubreportProcessingEventArgs)
            Dim objLocalReport = DirectCast(sender, LocalReport)
            If objLocalReport IsNot Nothing AndAlso objLocalReport.DataSources IsNot Nothing Then
                If e.DataSourceNames IsNot Nothing Then
                    For Each dsName In e.DataSourceNames
                        Dim dataSource = objLocalReport.DataSources.Where(Function(ds) ds.Name = dsName).FirstOrDefault()
                        If dataSource IsNot Nothing Then
                            e.DataSources.Add(New ReportDataSource(dsName, dataSource.Value))
                        End If
                    Next
                End If
            End If
            
        End Sub
#End Region

    End Class

End Namespace
