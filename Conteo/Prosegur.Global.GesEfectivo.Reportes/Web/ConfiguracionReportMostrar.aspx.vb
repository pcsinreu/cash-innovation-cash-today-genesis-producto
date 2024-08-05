Imports Ionic.Zip
Imports System.IO
Imports Prosegur.Framework.Dicionario.Tradutor

Public Class ConfiguracionReportMostrar
    Inherits Base

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        CompactaDiretorio()

    End Sub

    ''' <summary>
    ''' Compactar e disponibilizar para download relatórios onde a rota não foi informada.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub CompactaDiretorio()
        Dim caminhoDiretorio As String = System.Web.HttpContext.Current.Session("caminoRuta")
        Dim NomeArquivoZip As String = String.Format("{0}.{1}", System.Web.HttpContext.Current.Session("NombreArchivoZip"), "zip")

        Dim arquivoZip As ZipFile = New ZipFile()

        arquivoZip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression

        arquivoZip.Name = NomeArquivoZip

        If Directory.GetFiles(caminhoDiretorio).Count() > 0 Then
            arquivoZip.AddFiles(Directory.GetFiles(caminhoDiretorio), String.Empty)
        End If

        For Each diretorio As DirectoryInfo In New DirectoryInfo(caminhoDiretorio).GetDirectories()

            arquivoZip.AddDirectory(diretorio.FullName, diretorio.Name)
        Next

        Dim caminho As String = Path.Combine(caminhoDiretorio, arquivoZip.Name)

        arquivoZip.Save(caminho)
        arquivoZip.Dispose()

        'Obtiene la respuesta actual
        Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response

        'Limpa o conteúdo de saída atual do buffer
        response.Clear()
        response.ClearContent()
        response.ClearHeaders()
        'Adiciona um cabeçalho que especifica o nome default para a caixa de diálogos Salvar Como...
        response.ContentType = "Content-type, application/zip"
        response.AddHeader("Content-Disposition", "attachment; filename=""" & NomeArquivoZip & """")
        response.WriteFile(caminho)
    End Sub

End Class