Imports System.IO
Imports System.Configuration.ConfigurationManager
Namespace Prosegur.Genesis.ProcesosDuo.Util
    Public Class Util
        Friend Shared Sub EnviarEmail(Titulo As String, Conteudo As String,
                                            ByRef Anexo As List(Of KeyValuePair(Of String, MemoryStream)),
                                            EmailsResponsaveis As List(Of String),
                                            NombreArchivo As String,
                                            Optional EmailsNegocio As List(Of String) = Nothing)
            Try

                'Se existir emails responsáveis
                If EmailsResponsaveis IsNot Nothing AndAlso EmailsResponsaveis.Count > 0 Then

                    'obtem a lista de emails
                    Dim Para As String = String.Join(";", EmailsResponsaveis.ToArray)

                    'obtem a lista de emails de negócio
                    If EmailsNegocio IsNot Nothing AndAlso EmailsNegocio.Count > 0 Then
                        Para += If(String.IsNullOrEmpty(Para), "", ";") & String.Join(";", EmailsNegocio.ToArray)
                    End If

                    If Anexo IsNot Nothing Then

                        Prosegur.EmailHelper.Email.EnviarEmail(AppSettings("ServidorSMTP"), AppSettings("Remetente"), _
                                                               Para, "", "", Titulo, Conteudo, Anexo, Net.Mail.MailPriority.Normal, EmailHelper.PadraoEmail.PadraoFW2, 15000)

                    Else
                        'Envia o email
                        Prosegur.EmailHelper.Email.EnviarEmail(AppSettings("ServidorSMTP"), _
                                                               AppSettings("Remetente"), _
                                                               Para, _
                                                               Titulo, _
                                                               Conteudo, EmailHelper.PadraoEmail.PadraoFW1)
                    End If

                End If


            Catch ex As Exception
                'Util.LogMensagemEmDisco("Error Enviar Email: " & Tradutor.Traduzir("001_msg_error_no_envio_email"), NombreArchivo)
            End Try

        End Sub
    End Class

End Namespace