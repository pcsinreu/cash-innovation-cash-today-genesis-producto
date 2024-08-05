Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Util.EscritorMoedaCorrente
Imports System.Drawing
Imports System.IO
Imports System.Drawing.Imaging
Imports System.Windows.Forms

Public Class Util

#Region "[VARIÁVEIS]"

    'Variáveis com o formato de data e hora recuperado da cultura corrente
    Private Shared _FormatoData As String = Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern
    Private Shared _FormatoHora As String = Globalization.CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Retorna string com o formato de data de acordo com a cultura corrente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [rafael.nasorri] 01/10/2009 Criado
    ''' </history>
    Public Shared ReadOnly Property FormatoData() As String
        Get
            Return _FormatoData
        End Get
    End Property

    ''' <summary>
    ''' Retorna string com o formato de hora de acordo com a cultura corrente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <history>[jorge.viana]	19/07/2010	Creado</history>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property FormatoHora() As String
        Get
            Return _FormatoHora
        End Get
    End Property

    ''' <summary>
    ''' Retorna string com o formato de data e hora de acordo com a cultura corrente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [rafael.nasorri] 01/10/2009 Criado
    ''' </history>
    Public Shared ReadOnly Property FormatoDataHora() As String
        Get
            Return _FormatoData & " " & _FormatoHora
        End Get
    End Property

#End Region

#Region "[MÉTODOS/FUNÇÕES]"

    ''' <summary>
    ''' Função que gera o código de barra conforme um valor passado como parâmetro
    ''' </summary>
    ''' <param name="sValorCodigo">String</param>
    ''' <returns>Byte()</returns>
    ''' <history>[jorge.viana]	24/08/2010	Creado</history>
    ''' <remarks></remarks>
    Public Shared Function GerarCodigoBarra(sValorCodigo As String) As Byte()
        'objeto da classe CodigoBarras
        Dim objBarCode As New CodigoBarras
        'objeto que irá armazenar o desenho do código de barra
        Dim objGrafico As Graphics
        'objeto que irá armazenar a imagem do código de barra
        Dim objBitmap As New Bitmap(273, 50)
        'objeto de retorno, os bytes do arquivo de imagem
        Dim arrBytes As Byte() = Nothing
        'objeto que irá ler os bytes do arquivo de imagem
        Dim ms As MemoryStream = New MemoryStream()

        Try
            'Cria um novo gráfico baseando no arquivo de imagem do objeto objBitmap
            objGrafico = Graphics.FromImage(objBitmap)
            'preenche o fundo com a cor branca
            objGrafico.FillRectangle(Brushes.White, 0, 0, objBitmap.Width, objBitmap.Height)
            'Passa o número de origem para gerar o código de barra
            objBarCode.LeftMargin = 0
            objBarCode.TopMargin = 0
            objBarCode.BarCode = sValorCodigo
            'chama o método para desenhar o código de barras no objeto objGrafico
            objBarCode.Desenhar(objGrafico)

            'Trecho de código feito pelo usuário [cbomtempo] com o objetivo de alinhar a imagem
            'do código de barras à direita
            'Inicio
            Dim posY As Integer = Math.Ceiling(objBitmap.Height / 2)
            Dim posX As Integer = 0
            Dim cor = Color.FromArgb(255, 0, 0, 0)
            For i As Integer = 0 To objBitmap.Width - 1
                If objBitmap.GetPixel(i, posY) = cor Then
                    posX = i
                End If
            Next
            Dim objBitmapFinal As New Bitmap(posX + 1, objBitmap.Height)
            Dim objGrafico2 = Graphics.FromImage(objBitmapFinal)
            objGrafico2.DrawImageUnscaled(objBitmap, 0, 0)
            objGrafico2.Dispose()
            objGrafico.DrawImage(objBitmapFinal, New Rectangle(objBitmap.Width - posX - 2, 0, objBitmap.Width, _
                                                               objBitmap.Height), New Rectangle(0, 0, objBitmap.Width, objBitmap.Height), GraphicsUnit.Pixel)
            objGrafico.FillRectangle(Brushes.White, 0, 0, objBitmap.Width - posX - 2, objBitmap.Height)
            'Fim

            'Salva a imagem do código de barra no objeto ms
            objBitmap.Save(ms, ImageFormat.Png)
            'atualiza a variável de retorno com os bytes do arquivo de imagem
            arrBytes = ms.GetBuffer()

        Catch ex As Exception
            Throw
        Finally
            'libera os objetos da memória
            objBitmap.Dispose()
            ms.Close()
        End Try
        'retorna o array de bytes da imagem do código de barra
        Return arrBytes
    End Function

    ''' <summary>
    ''' Função que recupera os bytes da imagem do logotipo da Prosegur
    ''' </summary>
    ''' <returns></returns>
    ''' <history>[jorge.viana]	24/08/2010	Creado</history>
    ''' <remarks></remarks>
    Public Shared Function RecuperarLogoRelatorio() As Byte()
        'objeto de retorno, os bytes do arquivo de imagem
        Dim arrBytes As Byte() = Nothing
        'objeto que irá ler os bytes do arquivo de imagem
        Dim ms As MemoryStream = New MemoryStream()
        Try
            'Salva a imagem do logo no objeto ms
            My.Resources.LogoProsegurPB.Save(ms, ImageFormat.Jpeg)
            'atualiza a variável de retorno com os bytes do arquivo de imagem do logo
            arrBytes = ms.GetBuffer()
        Catch ex As Exception
            Throw
        End Try
        'retorna o array de bytes da imagem do logo da Prosegur
        Return arrBytes
    End Function

    ''' <summary>
    ''' Função que retorna o caminho físico do arquivo PDF a ser gerado
    ''' </summary>
    ''' <param name="sNomeArquivo"></param>
    ''' <returns></returns>
    ''' <history>[jorge.viana]	25/08/2010	Creado</history>
    ''' <remarks></remarks>
    Public Shared Function RetornaCaminhoFisicoRelatorioPDF(sNomeArquivo As String) As String
        'armazena o caminho completo do arquivo PDF
        Dim sCaminho As String = String.Empty
        'armazena o diretório dos relatórios
        Dim sDirRelatorio As String = "\Relatorios"
        'verifica se o diretório de relatório não existe
        If Not System.IO.Directory.Exists(Environment.CurrentDirectory & sDirRelatorio) Then
            'cria o diretório de relatórios
            System.IO.Directory.CreateDirectory(Environment.CurrentDirectory & sDirRelatorio)
        End If
        'monta o caminho completo do arquivo PDF
        sCaminho = Environment.CurrentDirectory & sDirRelatorio & "\" & sNomeArquivo & ".pdf"
        'verifica se o arquivo já existe e deleta o mesmo
        If System.IO.File.Exists(sCaminho) Then
            System.IO.File.Delete(sCaminho)
        End If
        Return sCaminho
    End Function

    ''' <summary>
    ''' Função que escreve por extenso um valor decimal 
    ''' </summary>
    ''' <param name="valor">Valor a ser escrito</param>
    ''' <param name="pCodDivisa">Indica a divisa que será escrita por extenso</param>
    ''' <returns></returns>
    ''' <history>[jorge.viana]	01/09/2010	Creado</history>
    ''' <history>[gustavo.fraga] 03/09/2010	Alterado</history>
    ''' <remarks></remarks>
    Public Shared Function EscrevePorExtenso(valor As Decimal, pCodDivisa As String) As String

        'Busca nos dicionários os valores das chaves de singular e plural de acordo com a divisa
        Dim objConfiguracaoMoeda As New ConfiguracaoMoeda

        objConfiguracaoMoeda.MoedaSingular = IIf(String.IsNullOrEmpty(pCodDivisa.ToLower), String.Empty, Traduzir("imp_moneda_sing_" & pCodDivisa.ToLower))
        objConfiguracaoMoeda.MoedaPlural = IIf(String.IsNullOrEmpty(pCodDivisa.ToLower), String.Empty, Traduzir("imp_moneda_plural_" & pCodDivisa.ToLower))
        objConfiguracaoMoeda.FracaoSingular = IIf(String.IsNullOrEmpty(pCodDivisa.ToLower), String.Empty, Traduzir("imp_centavo_sing_" & pCodDivisa.ToLower))
        objConfiguracaoMoeda.FracaoPlural = IIf(String.IsNullOrEmpty(pCodDivisa.ToLower), String.Empty, Traduzir("imp_centavo_plural_" & pCodDivisa.ToLower))

        'Instância do FrameWork para exibir por extenso
        Dim fabricaEscritor As New FabricaEscritorMoedaCorrente
        Dim escritor As IEscritorMoedaCorrente
        escritor = fabricaEscritor.EscritorMoedaCorrente()

        'Solicitando o retorno do extenso de acordo com a ISO divisa desejada.
        Return escritor.ObterEscritor().Escrever(valor, objConfiguracaoMoeda)

    End Function

    ''' <summary>
    ''' Adiciona uma mensagem ao arquivo de Log em disco
    ''' </summary>
    ''' <param name="mensagem"></param>
    ''' <remarks></remarks>
    Public Shared Sub LogMensagemEmDisco(mensagem As String)

        LogMensagemEmDisco(mensagem, "inf_Salidas.txt")

    End Sub

    ''' <summary>
    ''' Adiciona uma mensagem ao arquivo de Log em disco
    ''' </summary>
    ''' <param name="mensagem"></param>
    ''' <remarks></remarks>
    Public Shared Sub LogMensagemEmDisco(mensagem As String, nomeArquivo As String)

        Dim arquivo As StreamWriter = Nothing

        Try

            ' grava o arquivo de log em disco (mesmo nível em que os assemblys se encontram
            arquivo = New StreamWriter(AppDomain.CurrentDomain.BaseDirectory() + "\" + nomeArquivo, True)

            ' trata a mensagem para que ele tenha uma identação correta no arquivo txt (caso haja quebra de linha)
            mensagem = mensagem.Replace(vbCrLf, vbCrLf + "                    ")

            ' adiciona a linha no arquivo
            arquivo.WriteLine(Now.ToString("dd-MM-yyyy hh:mm:ss") + " " + mensagem)

        Finally

            If (arquivo IsNot Nothing) Then
                arquivo.Dispose()
                arquivo.Close()
            End If

        End Try

    End Sub


#End Region

#Region "[RELATÓRIOS]"

    ''' <summary>
    ''' Traduz os campos de título do relatório, passa o título como parâmetro
    ''' </summary>
    ''' <param name="pReport">Template do relatório</param>
    ''' <param name="pTitulo">Título do relatório</param>
    ''' <history>[jorge.viana]	24/08/2010	Creado</history>
    ''' <remarks></remarks>
    Public Shared Sub TraduzirRelatorio(pReport As CrystalDecisions.CrystalReports.Engine.ReportDocument, Optional pTitulo As String = "")

        ' Para cada seção existente no Relatorio
        For Each oSection In pReport.ReportDefinition.Sections

            ' Para cada objeto existente na seção
            For Each oRepObj In oSection.ReportObjects

                ' Se o tipo do objeto é 'SubreportObject'
                If TypeOf oRepObj Is CrystalDecisions.CrystalReports.Engine.SubreportObject Then

                    TraduzirRelatorio(pReport.Subreports(oRepObj.SubReportName))

                End If

                ' Verifica o tipo do objeto
                If TypeOf oRepObj Is CrystalDecisions.CrystalReports.Engine.FieldHeadingObject _
                OrElse TypeOf oRepObj Is CrystalDecisions.CrystalReports.Engine.TextObject Then

                    ' Se o objeto for o título do relatório
                    If oRepObj.Name = "lbl_titulo_relatorio" Then
                        ' Atribui o título do relatório
                        oRepObj.Text = pTitulo
                    Else
                        Dim traducao As String = Traduzir(oRepObj.Name)
                        If String.Format("[{0}]", oRepObj.Name) <> traducao Then
                            ' Atribui o valor do texto
                            oRepObj.Text = traducao
                        End If
                        'se não conseguiu traduzir então exibi o conteudo do objeto
                    End If

                End If

            Next
        Next

    End Sub

    Public Shared Function ImprimirArquivo(nomeReporte As String,
                                            caminhoArquivo As String,
                                                LogEnDisco As Boolean,
                                                Optional printName As String = Nothing,
                                                Optional EsPrimerObjeto As Boolean = True) As String
        Dim retorno As String = String.Empty
        ' mensagem de log
        If LogEnDisco Then
            Impresion.Util.LogMensagemEmDisco(String.Format("{0} - caminhoArq:{1} ", nomeReporte, caminhoArquivo))
        End If

        If System.IO.File.Exists(caminhoArquivo) Then

            ' mensagem de log
            If LogEnDisco Then
                Util.LogMensagemEmDisco(String.Format("{0} - Solicitar impresora:{1} ", nomeReporte, caminhoArquivo))
            End If

            If String.IsNullOrEmpty(printName) Then
                Dim objDoc As New Printing.PrintDocument
                objDoc.DocumentName = caminhoArquivo
                Dim objPrint As New PrintDialog
                objPrint.Document = objDoc
                ' Condição inserida para aceitar impressão somente uma vez
                If EsPrimerObjeto Then
                    If objPrint.ShowDialog() = DialogResult.OK Then

                        retorno = objPrint.PrinterSettings.PrinterName
                    Else
                        'Se cancelar a tela retorna "|" para não chamar a tela de impressora novamente
                        retorno = "|"
                    End If
                End If
            Else
                retorno = printName
            End If

            ' se existir nome da impressora
            If Not String.IsNullOrEmpty(retorno) AndAlso retorno <> "|" Then

                ' mensagem de log
                If LogEnDisco Then
                    Util.LogMensagemEmDisco(String.Format("{0} - Imprimir", nomeReporte))
                End If

                RawPrinterHelper.SendFileToPrinter(retorno, caminhoArquivo)

            End If
        End If

        Return retorno
    End Function
#End Region

End Class
