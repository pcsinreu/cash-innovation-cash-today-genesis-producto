Imports System.Collections.ObjectModel
Imports System.Threading.Tasks
Imports Prosegur.Genesis.Report
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.Framework.Dicionario.Tradutor

Namespace Prosegur.Genesis.GenerarReporte.Classes

    Public Class ReporteCertificacion
        Inherits Reporte

#Region "Construtor"

        Public Sub New(objParametros As List(Of String), objCaminhoArchivos As String,
                       objCaminhoArchivoLog As String, objGenerarLog As Boolean, objConfigReporte As Comon.Clases.ConfiguracionReporte)

            MyBase.New(objParametros, objCaminhoArchivos, objCaminhoArchivoLog, objGenerarLog, objConfigReporte)

        End Sub

#End Region

#Region "Metodos"


        Public Overrides Sub GenerarReporte()

            Util.GravarArquivo("Inicio", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

            Dim IdentificadorCertificado As String = String.Empty
            Dim IdentificadorSubCanal As String = String.Empty
            Dim CodigoCertificado As String = String.Empty
            Dim DireccionReporte As String = String.Empty
            Dim CodigoEstado As String = String.Empty
            Dim CodigoDelegacion As String = String.Empty
            Dim CodigoSubCanal As String = String.Empty
            Dim CodigoConfiguracion As String = String.Empty

            Util.GravarArquivo("Inicio - Preenchimento de Variáveis", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

            IdentificadorCertificado = Util.RecuperarValorParametro(MyBase.Parametros, Comon.Constantes.CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_CERTIFICADO)
            CodigoCertificado = Util.RecuperarValorParametro(MyBase.Parametros, Comon.Constantes.CODIGO_PARAMETRO_REPORTE_CODIGO_CERTIFICADO)
            IdentificadorSubCanal = Util.RecuperarValorParametro(MyBase.Parametros, Comon.Constantes.CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_SUBCANAL)
            DireccionReporte = ConfigReporte.Direccion
            CodigoEstado = Util.RecuperarValorParametro(MyBase.Parametros, Comon.Constantes.CODIGO_PARAMETRO_REPORTE_CODIGO_ESTADO_CERTIFICADO)
            CodigoDelegacion = Util.RecuperarValorParametro(MyBase.Parametros, Comon.Constantes.CODIGO_PARAMETRO_REPORTE_CODIGO_DELEGACION)
            CodigoSubCanal = Util.RecuperarValorParametro(MyBase.Parametros, Comon.Constantes.CODIGO_PARAMETRO_REPORTE_CODIGO_SUBCANAL)
            CodigoConfiguracion = ConfigReporte.Codigo

            If String.IsNullOrEmpty(IdentificadorCertificado) Then

                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                   String.Format(Traduzir("001_parametro_no_rellenado"),
                                                                 Comon.Constantes.CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_CERTIFICADO))
            End If

            If String.IsNullOrEmpty(CodigoCertificado) Then

                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                   String.Format(Traduzir("001_parametro_no_rellenado"),
                                                                 Comon.Constantes.CODIGO_PARAMETRO_REPORTE_CODIGO_CERTIFICADO))
            End If

            If String.IsNullOrEmpty(IdentificadorSubCanal) Then

                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                   String.Format(Traduzir("001_parametro_no_rellenado"),
                                                                 Comon.Constantes.CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_SUBCANAL))
            End If

            If String.IsNullOrEmpty(DireccionReporte) Then

                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                   String.Format(Traduzir("001_parametro_no_rellenado"),
                                                                 Comon.Constantes.CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_SUBCANAL))
            End If

            If String.IsNullOrEmpty(CodigoEstado) Then

                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                   String.Format(Traduzir("001_parametro_no_rellenado"),
                                                                 Comon.Constantes.CODIGO_PARAMETRO_REPORTE_CODIGO_ESTADO_CERTIFICADO))
            End If

            If String.IsNullOrEmpty(CodigoEstado) Then

                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                   String.Format(Traduzir("001_parametro_no_rellenado"),
                                                                 Comon.Constantes.CODIGO_PARAMETRO_REPORTE_CODIGO_ESTADO_CERTIFICADO))
            End If

            If String.IsNullOrEmpty(CodigoDelegacion) Then

                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                   String.Format(Traduzir("001_parametro_no_rellenado"),
                                                                 Comon.Constantes.CODIGO_PARAMETRO_REPORTE_CODIGO_DELEGACION))
            End If

            If String.IsNullOrEmpty(CodigoSubCanal) Then

                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                   String.Format(Traduzir("001_parametro_no_rellenado"),
                                                                 Comon.Constantes.CODIGO_PARAMETRO_REPORTE_CODIGO_SUBCANAL))
            End If


            Util.GravarArquivo("Fim - Preenchimento de Variáveis", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

            Try

                AccesoDatos.GenesisSaldos.ResultadoReporte.ActualizarResultadoReporte(ConfigReporte.Identificador, _
                                                                        ContractoServicio.Constantes.CONST_RESULTADO_CERTIFICADO_ENCURSO,
                                                                        DateTime.UtcNow, Nothing, String.Empty, IdentificadorCertificado,
                                                                        IdentificadorSubCanal, Nothing)

                GerarReporte(IdentificadorCertificado, CodigoCertificado, IdentificadorSubCanal,
                             DireccionReporte, CodigoEstado, CodigoDelegacion, CodigoSubCanal, CodigoConfiguracion)

                AccesoDatos.GenesisSaldos.ResultadoReporte.ActualizarResultadoReporte(ConfigReporte.Identificador, _
                                                                                      ContractoServicio.Constantes.CONST_RESULTADO_CERTIFICADO_PROCESADO,
                                                                                      Nothing, DateTime.UtcNow, String.Empty, IdentificadorCertificado,
                                                                                      IdentificadorSubCanal, Nothing)

            Catch ex As Exception
                AccesoDatos.GenesisSaldos.ResultadoReporte.ActualizarResultadoReporte(ConfigReporte.Identificador, _
                                                                                      ContractoServicio.Constantes.CONST_RESULTADO_CERTIFICADO_ERRO,
                                                                                      Nothing, DateTime.UtcNow, ex.Message.ToString, IdentificadorCertificado,
                                                                                      IdentificadorSubCanal, Nothing)
                Throw ex
            End Try

            Util.GravarArquivo("Fim", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

        End Sub

        Private Sub GerarReporte(IdentificadorCertificado As String, CodigoCertificado As String,
                                IdentificadorSubCanal As String,
                                DireccionReporte As String, CodigoEstado As String,
                                CodigoDelegacion As String, CodigoSubCanal As String, CodigoConfiguracion As String)
            Try

                Util.GravarArquivo("Inicio - GerarReporte", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                Util.GravarArquivo("Inicio - Recuperando Parametros Reporte", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                Dim objPeticionRS As New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Peticion
                With objPeticionRS
                    .CodigoAplicacion = Comon.Constantes.CODIGO_APLICACION_GENESIS_REPORTES
                    .CodigoDelegacion = CodigoDelegacion
                    .Parametros = New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.ParametroColeccion() _
                                  From {New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Parametro() _
                                                           With {.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_RS_REPORT_SEVICE_URL},
                                        New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Parametro() _
                                                           With {.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_RS_A_USER},
                                        New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Parametro() _
                                                           With {.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_RS_A_PASS},
                                        New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Parametro() _
                                                           With {.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_RS_A_DOMAIN},
                                        New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Parametro() _
                                                           With {.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_RS_CARPETA_REPORTES}}

                End With


                'Classe proxy do IAC
                Dim ojbAccionIAC As New Prosegur.Global.GesEfectivo.IAC.LogicaNegocio.AccionIntegracion

                Dim objRespuestaRS As GetParametrosDelegacionPais.Respuesta = ojbAccionIAC.GetParametrosDelegacionPais(objPeticionRS)

                ' Recupera os parâmetros da delegação
                If objRespuestaRS IsNot Nothing AndAlso objRespuestaRS.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                    Throw New Excepcion.NegocioExcepcion(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, objRespuestaRS.MensajeError)
                End If

                Util.GravarArquivo("Inicio - Preenchendo Variáveis parametros reporte", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                Dim strUrlReportService As String = String.Empty
                Dim strUser As String = String.Empty
                Dim strClave As String = String.Empty
                Dim strDomain As String = String.Empty
                Dim strCarpetaReportes As String = String.Empty

                If objRespuestaRS IsNot Nothing AndAlso
                   objRespuestaRS.Parametros IsNot Nothing AndAlso objRespuestaRS.Parametros.Count > 0 Then

                    strUrlReportService = (From p In objRespuestaRS.Parametros
                                           Where p.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_RS_REPORT_SEVICE_URL
                                           Select p.ValorParametro).FirstOrDefault

                    strUser = (From p In objRespuestaRS.Parametros
                                          Where p.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_RS_A_USER
                                          Select p.ValorParametro).FirstOrDefault

                    strClave = (From p In objRespuestaRS.Parametros
                                         Where p.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_RS_A_PASS
                                         Select p.ValorParametro).FirstOrDefault

                    strDomain = (From p In objRespuestaRS.Parametros
                                         Where p.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_RS_A_DOMAIN
                                         Select p.ValorParametro).FirstOrDefault

                    strCarpetaReportes = (From p In objRespuestaRS.Parametros
                                          Where p.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_RS_CARPETA_REPORTES
                                          Select p.ValorParametro).FirstOrDefault


                End If

                Util.GravarArquivo("Fim - Preenchendo Variáveis parametros reporte", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                Util.GravarArquivo("Inicio - Validando Variáveis parametros reporte", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                If String.IsNullOrEmpty(strCarpetaReportes) Then

                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                         String.Format(Traduzir("001_parametro_iac_no_rellenado"),
                                                                       Comon.Constantes.CODIGO_PARAMETRO_RS_CARPETA_REPORTES))
                End If

                If String.IsNullOrEmpty(strDomain) Then

                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                         String.Format(Traduzir("001_parametro_iac_no_rellenado"),
                                                                       Comon.Constantes.CODIGO_PARAMETRO_RS_A_DOMAIN))
                End If

                If String.IsNullOrEmpty(strClave) Then

                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                         String.Format(Traduzir("001_parametro_iac_no_rellenado"),
                                                                       Comon.Constantes.CODIGO_PARAMETRO_RS_A_PASS))
                End If

                If String.IsNullOrEmpty(strUser) Then

                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                         String.Format(Traduzir("001_parametro_iac_no_rellenado"),
                                                                       Comon.Constantes.CODIGO_PARAMETRO_RS_A_USER))
                End If

                If String.IsNullOrEmpty(strUrlReportService) Then

                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                         String.Format(Traduzir("001_parametro_iac_no_rellenado"),
                                                                       Comon.Constantes.CODIGO_PARAMETRO_RS_REPORT_SEVICE_URL))
                End If

                Util.GravarArquivo("Inicio - Validando Variáveis parametros reporte", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)
                Util.GravarArquivo("Fim - Recuperando Parametros Reporte", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                Util.GravarArquivo("Inicio - Recuperando parametros Genesis", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)
                Dim objPeticionGenesis As New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Peticion
                With objPeticionGenesis
                    .CodigoAplicacion = Comon.Constantes.CODIGO_APLICACION_GENESIS
                    .CodigoDelegacion = CodigoDelegacion
                    .Parametros = New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.ParametroColeccion() _
                                  From {New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Parametro() _
                                                           With {.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_IAC_DELEGACION_DIRECCION_REPORTES_GENERADOS}}

                End With

                Dim objRespuestaGenesis As GetParametrosDelegacionPais.Respuesta = ojbAccionIAC.GetParametrosDelegacionPais(objPeticionGenesis)

                ' Recupera os parâmetros da delegação
                If objRespuestaGenesis IsNot Nothing AndAlso objRespuestaGenesis.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                    Throw New Excepcion.NegocioExcepcion(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, objRespuestaGenesis.MensajeError)
                End If

                Util.GravarArquivo("Inicio - Preenchendo variáveis parametros Genesis", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                Dim strDireccionReportes As String = String.Empty

                If objRespuestaGenesis IsNot Nothing AndAlso
                   objRespuestaGenesis.Parametros IsNot Nothing AndAlso objRespuestaGenesis.Parametros.Count > 0 Then

                    strDireccionReportes = (From p In objRespuestaGenesis.Parametros
                                           Where p.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_IAC_DELEGACION_DIRECCION_REPORTES_GENERADOS
                                           Select p.ValorParametro).FirstOrDefault


                End If

                Util.GravarArquivo("Fim - Preenchendo variáveis parametros Genesis", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                Util.GravarArquivo("Inicio - Validando variáveis parametros Genesis", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                If String.IsNullOrEmpty(strDireccionReportes) Then

                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                         String.Format(Traduzir("001_parametro_iac_no_rellenado"),
                                                                       Comon.Constantes.CODIGO_PARAMETRO_IAC_DELEGACION_DIRECCION_REPORTES_GENERADOS))
                End If

                Util.GravarArquivo("Fim - Validando variáveis parametros Genesis", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                Util.GravarArquivo("Fim - Recuperando parametros Genesis", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                Util.GravarArquivo("Inicio - Gerando Reporte", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                Util.GravarArquivo("Inicio - Autenticando Credenciais", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                ' Recupera os parametros do relatório.
                Dim objReport As New Prosegur.Genesis.Report.Gerar()
                objReport.Autenticar(strUrlReportService, strUser, strClave, strDomain)

                Util.GravarArquivo("Fim - Autenticando Credenciais", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                Util.GravarArquivo("Inicio - Recuperando Carpeta Reporte", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                Dim listaParametros2010 As List(Of RS2010.ItemParameter)
                Dim objValores2010 As RS2010.ParameterValue() = Nothing

                Dim fullPathReport As String = Nothing

                ' Verificar se existe "/" no final da URL configurada para o Report
                If (strCarpetaReportes.Substring(strCarpetaReportes.Length - 1, 1) = "/") Then
                    strCarpetaReportes = strCarpetaReportes.Substring(0, strCarpetaReportes.Length - 1)
                End If
                Dim direccion As String = DireccionReporte
                If Not String.IsNullOrEmpty(direccion) Then
                    If direccion.StartsWith("/") Then
                        direccion = direccion.Substring(1)
                    End If
                    If direccion.EndsWith(".rdl") Then
                        direccion = direccion.Replace(".rdl", "")
                    End If
                End If

                fullPathReport = String.Format("{0}/{1}", strCarpetaReportes, direccion)

                Util.GravarArquivo("Fim - Recuperando Carpeta Reporte", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                Util.GravarArquivo("Inicio - Listando Parametros Reporte", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                listaParametros2010 = objReport.ListarParametros(fullPathReport, objValores2010)

                Dim listaParametros As New List(Of RSE.ParameterValue)
                Dim bolCertificadoConsulta As Integer = 0

                listaParametros.Add(New RSE.ParameterValue() With {.Name = "OID_CERTIFICADO", .Value = IdentificadorCertificado})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "OID_SUBCANAL", .Value = IdentificadorSubCanal})
                listaParametros.Add(New RSE.ParameterValue() With {.Name = "BOL_CONSULTA", .Value = 0})

                listaParametros.Add(New RSE.ParameterValue() With {.Name = "LISTA_PLANTAS", .Value = ""})


                Util.GravarArquivo("Fim - Listando Parametros Reporte", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                Dim objReportes As Byte() = Nothing

                If Not System.IO.Directory.Exists(strDireccionReportes) Then
                    System.IO.Directory.CreateDirectory(strDireccionReportes)
                End If

                Util.GravarArquivo("Inicio - Rederizando Reporte", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                Dim NombreArchivo As String = CodigoCertificado & "_" & CodigoConfiguracion & "_" & CodigoSubCanal

                If CodigoEstado = Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO Then

                    objReportes = objReport.RenderReport(fullPathReport, "PDF", listaParametros, "pdf")

                    If objReportes IsNot Nothing Then

                        If System.IO.File.Exists(String.Format("{0}\{1}.pdf", strDireccionReportes, NombreArchivo)) Then
                            System.IO.File.Delete(String.Format("{0}\{1}.pdf", strDireccionReportes, NombreArchivo))
                        End If

                        'Salva arquivo no disco
                        System.IO.File.WriteAllBytes(String.Format("{0}\{1}.pdf", strDireccionReportes, NombreArchivo), objReportes)
                    End If

                Else
                    objReportes = objReport.RenderReport(fullPathReport, "Excel", listaParametros, "xls")

                    If objReportes IsNot Nothing Then

                        If System.IO.File.Exists(String.Format("{0}\{1}.xls", strDireccionReportes, NombreArchivo)) Then
                            System.IO.File.Delete(String.Format("{0}\{1}.xls", strDireccionReportes, NombreArchivo))
                        End If

                        'Salva arquivo no disco
                        System.IO.File.WriteAllBytes(String.Format("{0}\{1}.xls", strDireccionReportes, NombreArchivo), objReportes)
                    End If

                End If

                Util.GravarArquivo("Fim - Rederizando Reporte", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                Util.GravarArquivo("Fim - Gerando Reporte", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                Util.GravarArquivo("Fim - GerarReporte", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

#End Region

    End Class

End Namespace