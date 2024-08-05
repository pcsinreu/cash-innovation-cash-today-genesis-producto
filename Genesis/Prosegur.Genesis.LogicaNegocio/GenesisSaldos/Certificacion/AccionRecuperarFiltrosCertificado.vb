Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Data
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones

Namespace GenesisSaldos.Certificacion

    Public Class AccionRecuperarFiltrosCertificado

        Public Function Ejecutar(Peticion As RecuperarFiltrosCertificado.Peticion) As RecuperarFiltrosCertificado.Respuesta

            Dim objRespuesta As New RecuperarFiltrosCertificado.Respuesta

            Try

                If (String.IsNullOrEmpty(Peticion.IdentificadorCertificado) AndAlso String.IsNullOrEmpty(Peticion.CodigoCertificado)) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributos_nao_preenchidos"), "IdentificadorCertificado", "CodigoCertificado"))
                Else

                    Dim dtDetalles As DataTable = AccesoDatos.GenesisSaldos.Certificacion.Filtros.RecuperarDetalles(Peticion.IdentificadorCertificado, Peticion.CodigoCertificado)

                    If dtDetalles Is Nothing OrElse dtDetalles.Rows Is Nothing OrElse dtDetalles.Rows.Count = 0 Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_item_no_encontrado_con_parametros_informados"), "Certificado"))
                    End If

                    With objRespuesta
                        .IdentificadorCertificado = Util.AtribuirValorObj(dtDetalles.Rows(0)("OID_CERTIFICADO"), GetType(String))
                        .CodigoCertificado = Util.AtribuirValorObj(dtDetalles.Rows(0)("COD_CERTIFICADO"), GetType(String))
                        .CodigoExternoCertificado = Util.AtribuirValorObj(dtDetalles.Rows(0)("COD_EXTERNO"), GetType(String))
                        .CodigoEstado = Util.AtribuirValorObj(dtDetalles.Rows(0)("COD_ESTADO"), GetType(String))
                        .FechaHoraCertificado = Util.AtribuirValorObj(dtDetalles.Rows(0)("FYH_CERTIFICADO"), GetType(Date))
                        If Peticion.Delegacion IsNot Nothing Then
                            .FechaHoraCertificado = .FechaHoraCertificado.QuieroExibirEstaFechaEnLaPatalla(Peticion.Delegacion)
                        End If
                        .IdentificadorCliente = Util.AtribuirValorObj(dtDetalles.Rows(0)("OID_CLIENTE"), GetType(String))
                        .CodigoCliente = Util.AtribuirValorObj(dtDetalles.Rows(0)("COD_CLIENTE"), GetType(String))
                        .DescripcionCliente = Util.AtribuirValorObj(dtDetalles.Rows(0)("DES_CLIENTE"), GetType(String))
                        .TodosSectores = Util.AtribuirValorObj(dtDetalles.Rows(0)("BOL_TODOS_SECTORES"), GetType(Boolean))
                        .TodasDelegaciones = Util.AtribuirValorObj(dtDetalles.Rows(0)("BOL_TODAS_DELEGACIONES"), GetType(Boolean))
                        .TodosSubCanales = Util.AtribuirValorObj(dtDetalles.Rows(0)("BOL_TODOS_CANALES"), GetType(Boolean))
                    End With

                    Dim dtSectores As DataTable = AccesoDatos.GenesisSaldos.Certificacion.Filtros.RecuperarSectores(objRespuesta.IdentificadorCertificado)
                    If dtSectores IsNot Nothing AndAlso dtSectores.Rows IsNot Nothing AndAlso dtSectores.Rows.Count > 0 Then
                        objRespuesta.Sectores = New List(Of RecuperarFiltrosCertificado.Sector)()
                        For Each Sector As DataRow In dtSectores.Rows
                            objRespuesta.Sectores.Add(New RecuperarFiltrosCertificado.Sector With {.Identificador = Util.AtribuirValorObj(Sector("OID_SECTOR"), GetType(String)), .Codigo = Util.AtribuirValorObj(Sector("COD_SECTOR"), GetType(String)), .Descripcion = Util.AtribuirValorObj(Sector("DES_SECTOR"), GetType(String))})
                        Next
                    End If

                    Dim dtSubCanales As DataTable = AccesoDatos.GenesisSaldos.Certificacion.Filtros.RecuperarSubCanales(objRespuesta.IdentificadorCertificado)
                    If dtSubCanales IsNot Nothing AndAlso dtSubCanales.Rows IsNot Nothing AndAlso dtSubCanales.Rows.Count > 0 Then
                        objRespuesta.SubCanales = New List(Of RecuperarFiltrosCertificado.SubCanal)()
                        For Each SubCanal As DataRow In dtSubCanales.Rows
                            objRespuesta.SubCanales.Add(New RecuperarFiltrosCertificado.SubCanal With {.Identificador = Util.AtribuirValorObj(SubCanal("OID_SUBCANAL"), GetType(String)), .Codigo = Util.AtribuirValorObj(SubCanal("COD_SUBCANAL"), GetType(String)), .Descripcion = Util.AtribuirValorObj(SubCanal("DES_SUBCANAL"), GetType(String))})
                        Next
                    End If

                    Dim dtDelegacionesPlantas As DataTable = AccesoDatos.GenesisSaldos.Certificacion.Filtros.RecuperarDelegacionesPlantas(objRespuesta.IdentificadorCertificado)
                    If dtDelegacionesPlantas IsNot Nothing AndAlso dtDelegacionesPlantas.Rows IsNot Nothing AndAlso dtDelegacionesPlantas.Rows.Count > 0 Then
                        objRespuesta.Delegaciones = New List(Of RecuperarFiltrosCertificado.Delegacion)()
                        Dim delegacion As RecuperarFiltrosCertificado.Delegacion = Nothing
                        For Each dr As DataRow In dtDelegacionesPlantas.Rows

                            If delegacion Is Nothing OrElse Not delegacion.Identificador.Equals(Util.AtribuirValorObj(dr("OID_DELEGACION"), GetType(String))) Then

                                If delegacion IsNot Nothing AndAlso Not delegacion.Identificador.Equals(Util.AtribuirValorObj(dr("OID_DELEGACION"), GetType(String))) Then
                                    objRespuesta.Delegaciones.Add(delegacion)
                                End If

                                delegacion = New RecuperarFiltrosCertificado.Delegacion()
                                delegacion.Identificador = Util.AtribuirValorObj(dr("OID_DELEGACION"), GetType(String))
                                delegacion.Codigo = Util.AtribuirValorObj(dr("COD_DELEGACION"), GetType(String))
                                delegacion.Descripcion = Util.AtribuirValorObj(dr("DES_DELEGACION"), GetType(String))
                                delegacion.Plantas = New List(Of RecuperarFiltrosCertificado.Planta)()

                            End If

                            delegacion.Plantas.Add(New RecuperarFiltrosCertificado.Planta With {.Identificador = Util.AtribuirValorObj(dr("OID_PLANTA"), GetType(String)), .Codigo = Util.AtribuirValorObj(dr("COD_PLANTA"), GetType(String)), .Descripcion = Util.AtribuirValorObj(dr("DES_PLANTA"), GetType(String))})

                        Next
                        If delegacion IsNot Nothing Then
                            objRespuesta.Delegaciones.Add(delegacion)
                        End If
                    End If

                End If

                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = String.Empty

            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.CodigoError = ex.Codigo
                objRespuesta.MensajeError = ex.Message
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString
            End Try

            Return objRespuesta

        End Function

    End Class
End Namespace