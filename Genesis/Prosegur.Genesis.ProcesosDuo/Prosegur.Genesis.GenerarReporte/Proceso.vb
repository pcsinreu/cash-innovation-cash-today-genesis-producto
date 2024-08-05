Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Parametro
Imports System.Configuration.ConfigurationManager
Imports System.Text
Imports System.IO
Imports System.Collections.ObjectModel
Imports System.Threading.Tasks
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio

Namespace Prosegur.Genesis.GenerarReporte

    Public Class Proceso
        Inherits Prosegur.Duo.HelperProcesso.Processo

        ' utilizado para garantir o acesso a objetos entre threads diferentes sem causar um erro de acesso a threads cruzadas
        Private Shared ReadOnly LockObject As Object = New Object()

        Private _CaminhoArchivos As String = String.Empty
        Private _GenerarLog As Boolean = False
        Private _CaminhoArquivoLog As String = String.Empty

#Region "[Constructors]"

        Public Sub New()
            ConfigurationManagerProxy.SetupProxy(Me.GetType().Namespace)
        End Sub

#End Region

        Protected Overrides Sub ExecutarProcesso()

            Try

                _CaminhoArchivos = MyBase.DiretorioArquivosProcesso & "LOG\GenerarReporte\"
                _CaminhoArquivoLog = _CaminhoArchivos & "LOG.txt"
                _GenerarLog = If(Not String.IsNullOrEmpty(AppSettings("GENERAR_ARCHIVO_LOG")) AndAlso AppSettings("GENERAR_ARCHIVO_LOG") = "1", True, False)

                GravarArquivo("Inicio")

                Dim objColParametros As List(Of String) = Nothing
                Dim objReporte As GenerarReporte.Classes.Reporte = Nothing
                Dim TipoReporte As Comon.Enumeradores.TipoReporte
                Dim objConfiguracionReporte As Comon.Clases.ConfiguracionReporte = Nothing
                Dim IdentificadorConfiguracionReporte As String = String.Empty

                GravarArquivo("Inicio - Preenchimento de Variáveis")

                If MyBase.Parametros IsNot Nothing AndAlso MyBase.Parametros.Count > 0 Then

                    objColParametros = (From p In MyBase.Parametros Select Convert.ToString(p)).ToList

                    Dim objTipoReporte As String = Classes.Util.RecuperarValorParametro(objColParametros, Comon.Constantes.CODIGO_PARAMETRO_REPORTE_TIPO_REPORTE)

                    If Not String.IsNullOrEmpty(objTipoReporte) Then
                        TipoReporte = Prosegur.Genesis.Comon.Extenciones.RecuperarEnum(Of Comon.Enumeradores.TipoReporte)(objTipoReporte)
                    Else
                        TipoReporte = Comon.Enumeradores.TipoReporte.NoDefinido
                    End If

                    IdentificadorConfiguracionReporte = Classes.Util.RecuperarValorParametro(objColParametros, Comon.Constantes.CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_CONFIG_REPORTE)

                    If Not String.IsNullOrEmpty(IdentificadorConfiguracionReporte) Then
                        objConfiguracionReporte = LogicaNegocio.GenesisSaldos.FormulariosCertificados.ObtenerConfiguracionReporte(IdentificadorConfiguracionReporte)
                    ElseIf TipoReporte <> Comon.Enumeradores.TipoReporte.NoDefinido Then

                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                            String.Format(Traduzir("001_parametro_no_rellenado"),
                                                                          Comon.Constantes.CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_CONFIG_REPORTE))
                    End If

                    If objConfiguracionReporte Is Nothing AndAlso TipoReporte <> Comon.Enumeradores.TipoReporte.NoDefinido Then

                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                             Traduzir("001_Configuracion_Reporte_No_Encontrado"))
                    End If

                End If

                GravarArquivo("Fim - Preenchimento de Variáveis")

                If objConfiguracionReporte IsNot Nothing AndAlso
                   objConfiguracionReporte.ParametrosReporte IsNot Nothing AndAlso
                   objConfiguracionReporte.ParametrosReporte.Count > 0 Then

                    For Each objParametro In objConfiguracionReporte.ParametrosReporte
                        objParametro.DescripcionValor = Classes.Util.RecuperarValorParametro(objColParametros, objParametro.Codigo)
                    Next

                End If

                Select Case TipoReporte

                    Case Comon.Enumeradores.TipoReporte.Certificacion

                        objReporte = New GenerarReporte.Classes.ReporteCertificacion(objColParametros, _CaminhoArchivos, _CaminhoArquivoLog, _GenerarLog, objConfiguracionReporte)

                        objReporte.GenerarReporte()

                    Case Comon.Enumeradores.TipoReporte.NoDefinido

                        objReporte = New GenerarReporte.Classes.ReporteCertificacionAntiguo(objColParametros, _CaminhoArchivos, _CaminhoArquivoLog, _GenerarLog, objConfiguracionReporte)

                        objReporte.GenerarReporte()


                    Case Else

                        objReporte = New GenerarReporte.Classes.ReporteAbono(objColParametros, _CaminhoArchivos, _CaminhoArquivoLog, _GenerarLog, objConfiguracionReporte)

                        objReporte.GenerarReporte()

                End Select

                GravarArquivo("Fim")

            Catch ex As Exception
                EnviarEmail(ex.ToString, Nothing)
                Throw ex
            End Try

        End Sub


        Private Sub GravarArquivo(str As String)

            ' verifica se o parâmetro GENERAR_ARCHIVO_LOG foi informado corretamente
            If _GenerarLog Then

                ' garante o acesso em threads paralelas
                SyncLock LockObject

                    ' Valida existencia do diretorio
                    If Not Directory.Exists(_CaminhoArchivos) Then
                        ' se não existe, cria o diretorio
                        Dim _Dir As DirectoryInfo = Directory.CreateDirectory(_CaminhoArchivos)
                        _CaminhoArchivos = _Dir.FullName

                    End If

                    ' grava o log em disco
                    Dim fil As New StreamWriter(_CaminhoArquivoLog, True)
                    fil.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff tt") & "-" & str)
                    fil.Close()

                End SyncLock

            End If

        End Sub

        ''' <summary>
        ''' Função que retorna a string com os valores de um select in()
        ''' </summary>
        ''' <param name="strLista">list of string</param>
        ''' <returns>retorna o valor formatado com aspas simples e virgula</returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [mhermeto]04/07/2008 Criado
        ''' </history>
        Public Shared Function RetornaListOfStringFormatado(strLista As List(Of String), strSeparador As String) As String
            Dim strValor As New StringBuilder
            Dim icount As Integer = 0
            If strLista IsNot Nothing Then

                For Each ite In strLista
                    strValor.Append(ite)
                    strValor.Append(strSeparador)
                Next

                Return Mid(strValor.ToString(), 1, strValor.Length - 1)
            Else
                Return String.Empty
            End If

        End Function

        ''' <summary>
        ''' Envia o e-mail dos indicadores.
        ''' </summary>
        ''' <param name="conteudo">Conteúdo HTML pronto para o envio.</param>
        ''' <remarks></remarks>
        Private Sub EnviarEmail(conteudo As String, anexos As List(Of String))

            ' só envia o e-mail caso o conteúdo não seja vazio
            If Not String.IsNullOrEmpty(conteudo) Then

                ' Recupera os parâmetros para envio do e-mail
                Dim servidorSmtp As String = AppSettings("ServidorSMTP").ToString()
                Dim remetente As String = AppSettings("Remetente").ToString()
                Dim destinatarios As String = RetornaListOfStringFormatado(MyBase.EmailsResponsaveis, ";")

                If String.IsNullOrEmpty(destinatarios) Then
                    destinatarios = Configuration.ConfigurationManager.AppSettings("EmailDestinatario")
                End If

                'TODO: Se for necessário, deve-se colocar no dicionário os textos abaixo.
                Dim titulo As String = String.Format(MyBase.CodigoProcesso & Traduzir("001_cabecalho_Email"), Environment.MachineName, Date.Now.ToString("dd/MM/yyyy HH:mm"), AppSettings("InformacaoAdicional")) & " "

                If anexos IsNot Nothing AndAlso anexos.Count > 0 Then
                    ' Envia o e-mail com anexos
                    Prosegur.EmailHelper.Email.EnviarEmail(servidorSmtp, remetente, destinatarios, titulo, conteudo, anexos.ToArray, Net.Mail.MailPriority.High, EmailHelper.PadraoEmail.PadraoFW2)
                Else
                    ' Envia o e-mail sem anexos
                    Prosegur.EmailHelper.Email.EnviarEmail(servidorSmtp, remetente, destinatarios, titulo, conteudo, Nothing, Net.Mail.MailPriority.High, EmailHelper.PadraoEmail.PadraoFW2)
                End If

            End If

        End Sub

    End Class

End Namespace

