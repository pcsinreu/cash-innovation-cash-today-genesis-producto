Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos
Imports Prosegur.DbHelper
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports Prosegur.Genesis.Comon.Extenciones

Namespace GenesisSaldos
    Public Class GenerarInforme

        Public Shared Function Ejecutar(Peticion As Reporte.GenerarInforme.Peticion) As Reporte.GenerarInforme.Respuesta

            Dim objRespuesta As New Reporte.GenerarInforme.Respuesta

            Try

                Dim objTransacion As New DbHelper.Transacao(AccesoDatos.Constantes.CONEXAO_GENESIS)

                If Peticion.Reportes IsNot Nothing AndAlso Peticion.Reportes.Count > 0 Then

                    Dim IdentificadorResultadoReporte As String = String.Empty
                    Dim objParametrosReporte As List(Of Comon.Clases.ParametroReporte) = Nothing

                    For Each DatosCertificado In Peticion.Reportes

                        AccesoDatos.GenesisSaldos.Certificacion.Comun.GenerarReporteCertificado(DatosCertificado, DateTime.MinValue, objTransacion)

                        objParametrosReporte = AccesoDatos.GenesisSaldos.ParametroReporte.RecuperarParametrosPorTipo(DatosCertificado.TipoReporte)

                        If DatosCertificado.ParametrosReporte IsNot Nothing AndAlso DatosCertificado.ParametrosReporte.Count > 0 Then

                            If DatosCertificado.TipoReporte = Comon.Enumeradores.TipoReporte.Certificacion Then

                                Dim objParametrosAux As New ObservableCollection(Of Comon.Clases.ParametroReporte)
                                Dim objParametroAux As Comon.Clases.ParametroReporte = Nothing

                                objParametroAux = (From p In DatosCertificado.ParametrosReporte
                                                   Where p.Codigo = Comon.Constantes.CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_CERTIFICADO).FirstOrDefault

                                If objParametroAux IsNot Nothing Then
                                    objParametrosAux.Add(objParametroAux)
                                End If

                                objParametroAux = (From p In DatosCertificado.ParametrosReporte
                                                   Where p.Codigo = Comon.Constantes.CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_SUBCANAL).FirstOrDefault

                                If objParametroAux IsNot Nothing Then
                                    objParametrosAux.Add(objParametroAux)
                                End If

                                DatosCertificado.ParametrosReporte.Clear()

                                DatosCertificado.ParametrosReporte = objParametrosAux

                            End If

                            For Each objP In DatosCertificado.ParametrosReporte

                                If objParametrosReporte IsNot Nothing Then
                                    objP.Identificador = (From pr In objParametrosReporte Where pr.Codigo = objP.Codigo Select pr.Identificador).FirstOrDefault
                                End If

                            Next

                        End If

                        If DatosCertificado.TipoReporte = Comon.Enumeradores.TipoReporte.Certificacion Then

                            IdentificadorResultadoReporte = AccesoDatos.GenesisSaldos.ResultadoReporte.RecuperarIdentificadorResultadoReporte(DatosCertificado.IdentificadorConfiguracionReporte,
                                                                                                                                              RecuperarValorParametro(DatosCertificado.ParametrosReporte, Comon.Constantes.CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_CERTIFICADO),
                                                                                                                                              RecuperarValorParametro(DatosCertificado.ParametrosReporte, Comon.Constantes.CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_SUBCANAL))

                        Else
                            IdentificadorResultadoReporte = AccesoDatos.GenesisSaldos.ResultadoReporte.RecuperarIdentificadorResultadoReporteConParametros(DatosCertificado.IdentificadorConfiguracionReporte, DatosCertificado.ParametrosReporte)
                        End If

                        If String.IsNullOrEmpty(IdentificadorResultadoReporte) Then

                            If DatosCertificado.TipoReporte = Comon.Enumeradores.TipoReporte.Certificacion Then

                                IdentificadorResultadoReporte = AccesoDatos.GenesisSaldos.ResultadoReporte.InserirResultadoReporte(DatosCertificado.IdentificadorConfiguracionReporte,
                                                                                          ContractoServicio.Constantes.CONST_RESULTADO_CERTIFICADO_PENDIENTE,
                                                                                           DateTime.UtcNow,
                                                                                           RecuperarValorParametro(DatosCertificado.ParametrosReporte, Comon.Constantes.CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_CERTIFICADO),
                                                                                           RecuperarValorParametro(DatosCertificado.ParametrosReporte, Comon.Constantes.CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_SUBCANAL),
                                                                                           objTransacion)

                            Else

                                IdentificadorResultadoReporte = AccesoDatos.GenesisSaldos.ResultadoReporte.InserirResultadoReporte(DatosCertificado.IdentificadorConfiguracionReporte,
                                                                                         ContractoServicio.Constantes.CONST_RESULTADO_CERTIFICADO_PENDIENTE,
                                                                                          DateTime.UtcNow, String.Empty, String.Empty,
                                                                                          objTransacion)

                            End If

                            AccesoDatos.GenesisSaldos.ParametroReporte.InserirValorParametrosConfigCertificado(IdentificadorResultadoReporte,
                                                                                                               DatosCertificado.ParametrosReporte, objTransacion)

                        Else

                            AccesoDatos.GenesisSaldos.ResultadoReporte.ActualizarResultadoReporte(IdentificadorResultadoReporte,
                                                                                                  ContractoServicio.Constantes.CONST_RESULTADO_CERTIFICADO_PENDIENTE,
                                                                                                  DateTime.UtcNow, Nothing, String.Empty, objTransacion, "")

                            AccesoDatos.GenesisSaldos.ParametroReporte.ExcluirValorParametrosConfigCertificado(IdentificadorResultadoReporte, objTransacion)

                            AccesoDatos.GenesisSaldos.ParametroReporte.InserirValorParametrosConfigCertificado(IdentificadorResultadoReporte,
                                                                                                               DatosCertificado.ParametrosReporte, objTransacion)

                        End If


                    Next

                End If

                objTransacion.RealizarTransacao()


            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.Mensajes.Add(ex.Message)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.Excepciones.Add(ex.Message.ToString)
            End Try

            Return objRespuesta
        End Function

        ''' <summary>
        ''' Recupera o valor do parametro
        ''' </summary>
        ''' <param name="objParametros"></param>
        ''' <param name="CodigoParametro"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarValorParametro(objParametros As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.ParametroReporte),
                                                       CodigoParametro As String) As String

            If objParametros IsNot Nothing AndAlso objParametros.Count > 0 Then

                Return (From p In objParametros Where p.Codigo = CodigoParametro Select p.DescripcionValor).FirstOrDefault
            End If

            Return String.Empty
        End Function

    End Class

End Namespace