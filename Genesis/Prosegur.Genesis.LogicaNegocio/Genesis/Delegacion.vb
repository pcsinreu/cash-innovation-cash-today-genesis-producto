Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.ContractoServicio
Imports System.Data
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global

Namespace Genesis

    ''' <summary>
    ''' Clase AccionDelegacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [henrique.ribeiro] 11/10/2013 - Criado
    ''' </history>
    Public Class Delegacion
        
        Public Shared Function ObtenerDelegacionesPorCodigos(codigosDelegaciones As List(Of String), codigosTipoSectores As List(Of String)) As ObservableCollection(Of Clases.Delegacion)
            Dim delegaciones As New ObservableCollection(Of Clases.Delegacion)

            Try
                Dim dtdelegaciones As DataTable = AccesoDatos.Genesis.Delegacion.ObtenerDelegacionesPorCodigos(codigosDelegaciones, codigosTipoSectores)

                If dtdelegaciones IsNot Nothing AndAlso dtdelegaciones.Rows IsNot Nothing AndAlso dtdelegaciones.Rows.Count > 0 Then

                    For Each _row In dtdelegaciones.Rows

                        Dim _sectorPadre As Clases.Sector = Nothing
                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(_row("OID_SECTOR_PADRE"), GetType(String))) Then
                            _sectorPadre = New Clases.Sector With {.Identificador = Util.AtribuirValorObj(_row("OID_SECTOR_PADRE"), GetType(String))}
                        End If

                        Dim _delegacion As Clases.Delegacion = New Clases.Delegacion With {.Identificador = Util.AtribuirValorObj(_row("OID_DELEGACION"), GetType(String)),
                                                                                               .Codigo = Util.AtribuirValorObj(_row("COD_DELEGACION"), GetType(String)),
                                                                                               .Descripcion = Util.AtribuirValorObj(_row("DES_DELEGACION"), GetType(String)),
                                                                                               .HusoHorarioEnMinutos = Util.AtribuirValorObj(_row("NEC_GMT_MINUTOS"), GetType(Integer)),
                                                                                               .AjusteHorarioVerano = Util.AtribuirValorObj(_row("NEC_VERANO_AJUSTE"), GetType(Integer)),
                                                                                               .FechaHoraVeranoInicio = Util.AtribuirValorObj(_row("FYH_VERANO_INICIO"), GetType(DateTime)),
                                                                                               .FechaHoraVeranoFin = Util.AtribuirValorObj(_row("FYH_VERANO_FIN"), GetType(DateTime)),
                                                                                               .CodigoPais = Util.AtribuirValorObj(_row("COD_PAIS"), GetType(String))}

                        Dim _planta As Clases.Planta = New Clases.Planta With {.Identificador = Util.AtribuirValorObj(_row("OID_PLANTA"), GetType(String)),
                                                                               .Codigo = Util.AtribuirValorObj(_row("COD_PLANTA"), GetType(String)),
                                                                               .Descripcion = Util.AtribuirValorObj(_row("DES_PLANTA"), GetType(String))}

                        Dim _tiposector As Clases.TipoSector = New Clases.TipoSector With {.Identificador = Util.AtribuirValorObj(_row("OID_TIPO_SECTOR"), GetType(String)),
                                                                                           .Codigo = Util.AtribuirValorObj(_row("COD_TIPO_SECTOR"), GetType(String)),
                                                                                           .Descripcion = Util.AtribuirValorObj(_row("DES_TIPO_SECTOR"), GetType(String))}

                        Dim _delegacionAtual As Clases.Delegacion = delegaciones.FirstOrDefault(Function(d) d.Identificador = Util.AtribuirValorObj(_row("OID_DELEGACION"), GetType(String)))

                        If _delegacionAtual Is Nothing Then

                            Dim _plantaInt As Clases.Planta = Util.ClonarObjeto(_planta)
                            _plantaInt.TiposSector = New ObservableCollection(Of Clases.TipoSector)
                            _plantaInt.TiposSector.Add(_tiposector)
                            _plantaInt.Sectores = New ObservableCollection(Of Clases.Sector)
                            _plantaInt.Sectores.Add(New Clases.Sector With {.Identificador = Util.AtribuirValorObj(_row("OID_SECTOR"), GetType(String)),
                                                                          .Codigo = Util.AtribuirValorObj(_row("COD_SECTOR"), GetType(String)),
                                                                          .Descripcion = Util.AtribuirValorObj(_row("DES_SECTOR"), GetType(String)),
                                                                          .Planta = _planta,
                                                                          .Delegacion = _delegacion,
                                                                          .TipoSector = _tiposector,
                                                                          .SectorPadre = _sectorPadre,
                                                                          .EsPuesto = Util.AtribuirValorObj(_row("ESPUESTO"), GetType(Boolean))})

                            Dim _delegacionInt As Clases.Delegacion = Util.ClonarObjeto(_delegacion)
                            _delegacionInt.Plantas = New ObservableCollection(Of Clases.Planta)
                            _delegacionInt.Plantas.Add(_plantaInt)

                            delegaciones.Add(_delegacionInt)

                        Else
                            Dim _plantaAtual As Clases.Planta = _delegacionAtual.Plantas.FirstOrDefault(Function(p) p.Identificador = Util.AtribuirValorObj(_row("OID_PLANTA"), GetType(String)))

                            If _plantaAtual Is Nothing Then

                                Dim _plantaInt As Clases.Planta = Util.ClonarObjeto(_planta)
                                _plantaInt.TiposSector = New ObservableCollection(Of Clases.TipoSector)
                                _plantaInt.TiposSector.Add(_tiposector)
                                _plantaInt.Sectores = New ObservableCollection(Of Clases.Sector)
                                _plantaInt.Sectores.Add(New Clases.Sector With {.Identificador = Util.AtribuirValorObj(_row("OID_SECTOR"), GetType(String)),
                                                                              .Codigo = Util.AtribuirValorObj(_row("COD_SECTOR"), GetType(String)),
                                                                              .Descripcion = Util.AtribuirValorObj(_row("DES_SECTOR"), GetType(String)),
                                                                              .Planta = _planta,
                                                                              .Delegacion = _delegacion,
                                                                              .TipoSector = _tiposector,
                                                                              .SectorPadre = _sectorPadre,
                                                                              .EsPuesto = Util.AtribuirValorObj(_row("ESPUESTO"), GetType(Boolean))})

                                _delegacionAtual.Plantas.Add(_plantaInt)

                            Else

                                _plantaAtual.Sectores.Add(New Clases.Sector With {.Identificador = Util.AtribuirValorObj(_row("OID_SECTOR"), GetType(String)),
                                                                      .Codigo = Util.AtribuirValorObj(_row("COD_SECTOR"), GetType(String)),
                                                                      .Descripcion = Util.AtribuirValorObj(_row("DES_SECTOR"), GetType(String)),
                                                                      .Planta = _planta,
                                                                      .Delegacion = _delegacion,
                                                                      .TipoSector = _tiposector,
                                                                      .SectorPadre = _sectorPadre,
                                                                      .EsPuesto = Util.AtribuirValorObj(_row("ESPUESTO"), GetType(Boolean))})


                            End If

                        End If
                        
                    Next

                End If

            Catch ex As Exception
                Throw
            End Try

            Return delegaciones

        End Function

        ''' <summary>
        ''' Método que recupera a delegação pelo seu OID.
        ''' </summary>
        ''' <param name="oid">OID a ser pesquisado</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPorOid(oid As String) As Prosegur.Genesis.Comon.Clases.Delegacion
            Return Prosegur.Genesis.AccesoDatos.Genesis.Delegacion.ObtenerPorOid(oid)
        End Function

        Public Shared Function ObtenerDelegaciones(Peticion As Prosegur.Genesis.ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegaciones.Peticion) As Prosegur.Genesis.ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegaciones.Respuesta
            Dim respuesta As New Prosegur.Genesis.ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegaciones.Respuesta

            Try

                If Peticion IsNot Nothing AndAlso Peticion.CodigosDelegaciones IsNot Nothing AndAlso Peticion.CodigosDelegaciones.Count > 0 Then

                    respuesta.Delegaciones = ObtenerDelegacionesPorCodigos(Peticion.CodigosDelegaciones, Nothing)

                End If

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.CodigoError = ex.Codigo
                respuesta.MensajeError = ex.Descricao
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                respuesta.MensajeError = ex.ToString()
            End Try

            Return respuesta
        End Function

        Public Shared Function ObtenerDelegacionesDelUsuario(Peticion As Prosegur.Genesis.ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegacionesDelUsuario.Peticion) As Prosegur.Genesis.ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegacionesDelUsuario.Respuesta

            Dim respuesta As New Prosegur.Genesis.ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegacionesDelUsuario.Respuesta

            Try

                If String.IsNullOrEmpty(Peticion.login) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("003_atr_login")))
                End If

                Dim objRespuestaSeguridad As Seguridad.ContractoServicio.ObtenerDelegacionesDelUsuario.Respuesta = ObtenerDelegacionesDelUsuarioSeguridad(Peticion)

                If objRespuestaSeguridad IsNot Nothing Then

                    If objRespuestaSeguridad.Codigo <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                        Throw New Excepcion.NegocioExcepcion(objRespuestaSeguridad.Codigo, objRespuestaSeguridad.Descripcion)
                    End If

                    If Not Peticion.obtenerTodasInformaciones Then

                        respuesta.Delegaciones = obtenerDelegacionesEnGenesis(objRespuestaSeguridad.Delegaciones)

                    Else

                        'TODO

                    End If

                End If

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.CodigoError = ex.Codigo
                respuesta.MensajeError = ex.Descricao
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                respuesta.MensajeError = ex.ToString()
            End Try

            Return respuesta

        End Function
        Public Shared Function ObtenerDelegacionGMT(Peticion As Prosegur.Genesis.ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegacionGMT.Peticion) As Prosegur.Genesis.ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegacionGMT.Respuesta

            Dim respuesta As New Prosegur.Genesis.ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegacionGMT.Respuesta

            Try

                If String.IsNullOrEmpty(Peticion.codigoDelegacion) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("codigoDelegacion")))
                End If

                respuesta.Delegacion = AccesoDatos.Genesis.Delegacion.ObtenerDelegacionGMT(Peticion.codigoDelegacion)

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.CodigoError = ex.Codigo
                respuesta.MensajeError = ex.Descricao
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                respuesta.MensajeError = ex.ToString()
            End Try

            Return respuesta

        End Function

        Private Shared Function ObtenerDelegacionesDelUsuarioSeguridad(Peticion As Prosegur.Genesis.ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegacionesDelUsuario.Peticion) As Seguridad.ContractoServicio.ObtenerDelegacionesDelUsuario.Respuesta

            Dim objPeticionSeguridad As New Seguridad.ContractoServicio.ObtenerDelegacionesDelUsuario.Peticion
            Dim objProxyLogin As New LoginGlobal.Seguridad()

            objPeticionSeguridad.codigoAplicacion = Peticion.codigoAplicacion
            objPeticionSeguridad.login = Peticion.login
            objPeticionSeguridad.codigoPais = Peticion.codigoPais

            Return objProxyLogin.ObtenerDelegacionesDelUsuario(objPeticionSeguridad)

        End Function

        Private Shared Function obtenerDelegacionesEnGenesis(delegaciones As List(Of Seguridad.ContractoServicio.ObtenerDelegacionesDelUsuario.Delegacion)) As ObservableCollection(Of Clases.Delegacion)

            Dim _codigosDelegaciones As New List(Of String)
            Dim _codigosTipoSectores As New List(Of String)

            If delegaciones IsNot Nothing AndAlso delegaciones.Count > 0 Then
                For Each _delegacion In delegaciones

                    If Not _codigosDelegaciones.Contains(_delegacion.Codigo) Then

                        _codigosDelegaciones.Add(_delegacion.Codigo)

                        If _delegacion.Plantas IsNot Nothing AndAlso _delegacion.Plantas.Count > 0 Then

                            For Each _plantar In _delegacion.Plantas

                                If _plantar.TiposSectores IsNot Nothing AndAlso _plantar.TiposSectores.Count > 0 Then

                                    For Each _tipoSectores In _plantar.TiposSectores
                                        If Not _codigosTipoSectores.Contains(_tipoSectores.Codigo) Then
                                            _codigosTipoSectores.Add(_tipoSectores.Codigo)
                                        End If
                                    Next

                                End If

                            Next

                        End If
                    End If

                Next
            End If

            Return ObtenerDelegacionesPorCodigos(_codigosDelegaciones, _codigosTipoSectores)

        End Function

        Shared Function ObtenerDelegacionJSON(codigo As String, descripcion As String) As List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)
            Return AccesoDatos.Genesis.Delegacion.ObtenerDelegacionJSON(codigo, descripcion)
        End Function

    End Class
End Namespace