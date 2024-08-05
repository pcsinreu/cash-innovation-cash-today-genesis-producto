Imports System.Collections.ObjectModel
Imports System.Threading.Tasks
Imports Prosegur.Genesis.Report
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon.Extenciones

Namespace Prosegur.Genesis.GenerarReporte.Classes

    Public Class ReporteAbono
        Inherits Reporte

#Region "Construtor"

        Public Sub New(objParametros As List(Of String), objCaminhoArchivos As String,
                       objCaminhoArchivoLog As String, objGenerarLog As Boolean, objConfigReporte As Comon.Clases.ConfiguracionReporte)

            MyBase.New(objParametros, objCaminhoArchivos, objCaminhoArchivoLog, objGenerarLog, objConfigReporte)

        End Sub

#End Region

#Region "Variaveis"

        Private _objConfiguracionReporte As Comon.Clases.ConfiguracionReporte = Nothing

#End Region

#Region "Metodos"

        Public Overrides Sub GenerarReporte()

            Util.GravarArquivo("Inicio", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

            Dim IdentificadorConfiguracionReporte As String = String.Empty
            Dim IdentificadorResultadoReporte As String = String.Empty

            Util.GravarArquivo("Inicio - Preenchimento de Variáveis", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

            If MyBase.Parametros IsNot Nothing AndAlso MyBase.Parametros.Count > 0 Then
                IdentificadorConfiguracionReporte = Util.RecuperarValorParametro(MyBase.Parametros, Comon.Constantes.CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_CONFIG_REPORTE)
            End If


            IdentificadorResultadoReporte = AccesoDatos.GenesisSaldos.ResultadoReporte.RecuperarIdentificadorResultadoReporteConParametros(IdentificadorConfiguracionReporte,
                                                                                                                                           ConfigReporte.ParametrosReporte)

            If String.IsNullOrEmpty(IdentificadorResultadoReporte) Then
                
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                    Traduzir("001_Identificador_Resultado_No_Encontrado"))
            End If

            Util.GravarArquivo("Fim - Preenchimento de Variáveis", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

            Try

                AccesoDatos.GenesisSaldos.ResultadoReporte.ActualizarResultadoReporte(IdentificadorResultadoReporte, _
                                                                                      ContractoServicio.Constantes.CONST_RESULTADO_CERTIFICADO_ENCURSO,
                                                                                      DateTime.Now, Nothing, String.Empty, Nothing, "")

                Dim NombreArchivoReporte As String = String.Empty

                GerarReporte(NombreArchivoReporte)

                AccesoDatos.GenesisSaldos.ResultadoReporte.ActualizarResultadoReporte(IdentificadorResultadoReporte, _
                                                                                      ContractoServicio.Constantes.CONST_RESULTADO_CERTIFICADO_PROCESADO,
                                                                                      Nothing, DateTime.Now, String.Empty, Nothing, NombreArchivoReporte)


            Catch ex As Exception

                AccesoDatos.GenesisSaldos.ResultadoReporte.ActualizarResultadoReporte(IdentificadorResultadoReporte, _
                                                                                    ContractoServicio.Constantes.CONST_RESULTADO_CERTIFICADO_ERRO,
                                                                                    Nothing, DateTime.Now, ex.Message.ToString, Nothing, "")

                Throw ex
            End Try

            Util.GravarArquivo("Fim", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

        End Sub

        Private Sub GerarReporte(ByRef NombreArchivoReporete As String)
            Try

                Dim CodigoDelegacion As String = Util.RecuperarValorParametro(MyBase.Parametros, Comon.Constantes.CODIGO_PARAMETRO_REPORTE_CODIGO_DELEGACION)

                If String.IsNullOrEmpty(CodigoDelegacion) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                        String.Format(Traduzir("001_parametro_no_rellenado"),
                                                                      Comon.Constantes.CODIGO_PARAMETRO_REPORTE_CODIGO_DELEGACION))
                End If

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

                Dim direccion As String = ConfigReporte.Direccion

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
                Dim objParametrosGenerarNombre As New Dictionary(Of String, String)

                If ConfigReporte.ParametrosReporte IsNot Nothing AndAlso ConfigReporte.ParametrosReporte.Count > 0 Then

                    Dim ValorParametro As String = String.Empty

                    For Each objParametro In ConfigReporte.ParametrosReporte

                        ValorParametro = Util.RecuperarValorParametro(MyBase.Parametros, objParametro.Codigo)

                        listaParametros.Add(New RSE.ParameterValue() With {.Name = objParametro.Codigo, .Value = ValorParametro})
                        objParametrosGenerarNombre.Add(objParametro.Codigo, ValorParametro)

                    Next

                End If

                Util.GravarArquivo("Fim - Listando Parametros Reporte", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                Dim objReportes As Byte() = Nothing

                If Not System.IO.Directory.Exists(strDireccionReportes) Then
                    System.IO.Directory.CreateDirectory(strDireccionReportes)
                End If

                Util.GravarArquivo("Inicio - Rederizando Reporte", GenerarLog, CaminhoArchivos, CaminhoArquivoLog)

                Dim NombreArchivo As String = Comon.Util.GeneracionDinamicaTexto(ConfigReporte.MascaraNombre, objParametrosGenerarNombre)
                Dim Extension As String = If(ConfigReporte.CodigoRedenrizador = Comon.Enumeradores.TipoRenderizador.CSV, ConfigReporte.DescripcionExtension, ConfigReporte.CodigoRedenrizador.RecuperarValor)

                objReportes = objReport.RenderReport(fullPathReport, ConfigReporte.CodigoRedenrizador.RecuperarValor, listaParametros, Extension)

                If objReportes IsNot Nothing Then

                    ' Para corrigir um bug e não alterar muito o codigo (entregas urgentes), dupliquei esta linha
                    Extension = If(ConfigReporte.CodigoRedenrizador = Comon.Enumeradores.TipoRenderizador.CSV, ConfigReporte.DescripcionExtension, ConfigReporte.CodigoRedenrizador.RecuperarValor)

                    If System.IO.File.Exists(String.Format("{0}\{1}.{2}", strDireccionReportes, NombreArchivo, Extension)) Then
                        System.IO.File.Delete(String.Format("{0}\{1}.{2}", strDireccionReportes, NombreArchivo, Extension))
                    End If

                    'Salva arquivo no disco
                    System.IO.File.WriteAllBytes(String.Format("{0}\{1}.{2}", strDireccionReportes, NombreArchivo, Extension), objReportes)
                End If

                NombreArchivoReporete = String.Format("{0}.{1}", NombreArchivo, Extension)

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