Imports System.Windows.Forms
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Public Class Util
    Public Shared Function TratarRetornoServico(ByRef objRespuesta As ContractoServicio.RespuestaGenerico, _
                                                Optional ExhibirMensaje As Boolean = True) As Boolean

        ' verifica se o retorno não é nothing
        If objRespuesta IsNot Nothing Then

            ' se houve erro e o código for maior ou igual a 100
            If objRespuesta.CodigoError >= 100 Then

                If ExhibirMensaje Then
                    ' exibir mensagem
                    MessageBox.Show(objRespuesta.MensajeError, Traduzir("GENESIS_000_titulo_msgbox"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            ElseIf objRespuesta.CodigoError > 0 AndAlso objRespuesta.CodigoError < 100 Then

                'loga o erro
                GerenciadorAplicacion.LogarErroAplicacao(objRespuesta)

                If ExhibirMensaje Then
                    ' exibir mensagem
                    MessageBox.Show(objRespuesta.MensajeError, Traduzir("GENESIS_000_titulo_msgbox"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            ElseIf objRespuesta.CodigoError = 0 Then
                ' resposta do serviço ok
                Return True
            End If

        End If

        ' se chegou até aqui, houve algum erro no serviço
        Return False

    End Function

    Public Shared Function RecuperarNomeRelatorio(objDocumento As Prosegur.Genesis.Comon.Clases.Documento) As String
        Dim nomeRelatorio As String = String.Empty
        If objDocumento IsNot Nothing Then
            'Se o formulário for de gestão de remssa ou gestão de bultos, altas
            If (objDocumento.Formulario.Caracteristicas.Contains(Prosegur.Genesis.Comon.Enumeradores.CaracteristicaFormulario.GestiondeRemesas)) OrElse
                (objDocumento.Formulario.Caracteristicas.Contains(Prosegur.Genesis.Comon.Enumeradores.CaracteristicaFormulario.GestiondeBultos) AndAlso _
                                                               objDocumento.Formulario.Caracteristicas.Contains(Prosegur.Genesis.Comon.Enumeradores.CaracteristicaFormulario.Altas)) Then

                nomeRelatorio = "rptElementoIndividualRemesa"

            ElseIf objDocumento.Formulario.Caracteristicas.Contains(Prosegur.Genesis.Comon.Enumeradores.CaracteristicaFormulario.Classificacion) Then
                nomeRelatorio = "rptValoresIndividualClassificacion"

            ElseIf objDocumento.Formulario.Caracteristicas.Contains(Prosegur.Genesis.Comon.Enumeradores.CaracteristicaFormulario.GestiondeBultos) Then
                nomeRelatorio = "rptElementoIndividualBulto"

            ElseIf objDocumento.Formulario.Caracteristicas.Contains(Prosegur.Genesis.Comon.Enumeradores.CaracteristicaFormulario.Cierres) OrElse
                    objDocumento.Formulario.Caracteristicas.Contains(Prosegur.Genesis.Comon.Enumeradores.CaracteristicaFormulario.GestiondeFondos) Then
                nomeRelatorio = "rptValoresIndividual"
            ElseIf objDocumento.Formulario.Caracteristicas.Contains(Prosegur.Genesis.Comon.Enumeradores.CaracteristicaFormulario.GestiondeContenedores) Then
                nomeRelatorio = "rptElementoIndividualContenedor"
            Else
                '* Não existe implementação no sistema para documentos do tipo “Gestão de Contenedores” 
                'e “Outros Movimentos”. Por este motivo, a impressão não estará preparada para trabalhar com os mesmos neste momento. 
            End If
        End If

        Return nomeRelatorio
    End Function

    Public Shared Function RecuperarNomeRelatorio(objGrupoDocumentos As Prosegur.Genesis.Comon.Clases.GrupoDocumentos) As String
        Dim nomeRelatorio As String = String.Empty
        If objGrupoDocumentos IsNot Nothing Then
            If objGrupoDocumentos.Formulario.Caracteristicas.Contains(Prosegur.Genesis.Comon.Enumeradores.CaracteristicaFormulario.GestiondeRemesas) OrElse
            objGrupoDocumentos.Formulario.Caracteristicas.Contains(Prosegur.Genesis.Comon.Enumeradores.CaracteristicaFormulario.GestiondeBultos) Then
                nomeRelatorio = "rptElementoGrupoSalidasRecorrido"
            ElseIf objGrupoDocumentos.Formulario.Caracteristicas.Contains(Prosegur.Genesis.Comon.Enumeradores.CaracteristicaFormulario.Cierres) Then
                nomeRelatorio = "rptValoresGrupoCierre"
            ElseIf objGrupoDocumentos.Formulario.Caracteristicas.Contains(Prosegur.Genesis.Comon.Enumeradores.CaracteristicaFormulario.GestiondeFondos) Then
                nomeRelatorio = "rptValoresGrupo"
            ElseIf objGrupoDocumentos.Formulario.Caracteristicas.Contains(Prosegur.Genesis.Comon.Enumeradores.CaracteristicaFormulario.GestiondeContenedores) Then
                nomeRelatorio = "rptElementoGrupoContenedor"
            Else
                '* Não existe implementação no sistema para documentos do tipo “Gestão de Contenedores” 
                'e “Outros Movimentos”. Por este motivo, a impressão não estará preparada para trabalhar com os mesmos neste momento.
            End If
        End If

        Return nomeRelatorio
    End Function

    Public Shared Function GenerarImporteDocumento(codigoComprobante As String,
                                                   nomeRelatorio As String,
                                                  reportesUrl As String,
                                                reportesCarpeta As String,
                                                reportesUser As String,
                                                reportesPass As String,
                                                reportesDomain As String,
                                                usuarioSolicitante As String,
                                                reporteDocumentoNegocio As String,
                                                reporteDocumentoDomicilioTelefono As String) As String

        Dim objReport As New Prosegur.Genesis.Report.Gerar()
        objReport.Autenticar(reportesUrl, reportesUser, reportesPass, reportesDomain)
        
        Dim Parametros As New List(Of Prosegur.Genesis.Report.RSE.ParameterValue)
        Parametros.Add(New Prosegur.Genesis.Report.RSE.ParameterValue() With {.Name = "P_COD_COMPROBANTE", .Value = codigoComprobante})
        Parametros.Add(New Prosegur.Genesis.Report.RSE.ParameterValue() With {.Name = "P_DES_USUARIO_SOLICITANTE", .Value = usuarioSolicitante})
        Parametros.Add(New Prosegur.Genesis.Report.RSE.ParameterValue() With {.Name = "P_ReporteDocumentoNegocio", .Value = IIf(String.IsNullOrEmpty(reporteDocumentoNegocio), "  ", reporteDocumentoNegocio)})
        Parametros.Add(New Prosegur.Genesis.Report.RSE.ParameterValue() With {.Name = "P_ReporteDocumentoDomicilioTelefono", .Value = If(String.IsNullOrEmpty(reporteDocumentoDomicilioTelefono), "  ", reporteDocumentoDomicilioTelefono)})

        Dim Buffer = objReport.RenderReport(String.Format("{0}/DOCUMENTO/{1}", reportesCarpeta, nomeRelatorio), "PDF", Parametros, "PDF", String.Empty, reportesUrl)
        Dim pdfReporte As String = AppDomain.CurrentDomain.BaseDirectory()

        If (DateTime.Now.Day Mod 2) = 0 Then
            'Exclui os arquivos dos dias impares
            If System.IO.Directory.Exists(pdfReporte + "impar") Then
                Try
                    System.IO.Directory.Delete(pdfReporte + "impar", True)
                Catch ex As Exception
                    'Nao faz nada
                End Try
            End If

            pdfReporte = pdfReporte + "par"
            If Not System.IO.Directory.Exists(pdfReporte) Then
                System.IO.Directory.CreateDirectory(pdfReporte)
            End If
        Else
            'Exclui os arquivos dos dias pares
            If System.IO.Directory.Exists(pdfReporte + "par") Then
                Try
                    System.IO.Directory.Delete(pdfReporte + "par", True)
                Catch ex As Exception
                    'Não faz nada
                End Try
            End If

            pdfReporte = pdfReporte + "impar"
            If Not System.IO.Directory.Exists(pdfReporte) Then
                System.IO.Directory.CreateDirectory(pdfReporte)
            End If
        End If

        pdfReporte = pdfReporte + "\doc_" + DateTime.Now.ToString("ddHHmmssfff") + ".pdf"
        File.WriteAllBytes(pdfReporte, Buffer)

        Return pdfReporte

    End Function

    Public Shared Function ValidarMorfologiaEmail(email As String)

        Dim padraoRegex As String = "^[-a-zA-Z0-9][-.a-zA-Z0-9]*@[-.a-zA-Z0-9]+(\.[-.a-zA-Z0-9]+)*\." & _
                                        "(com|edu|info|gov|int|mil|net|org|biz|name|museum|coop|aero|pro|tv|[a-zA-Z]{2})$"


        Dim verificionEmail As New RegularExpressions.Regex(padraoRegex, RegexOptions.IgnorePatternWhitespace)

        Return verificionEmail.Match(email).Success

    End Function

End Class
