Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Data
Imports System.Collections.ObjectModel

Namespace GenesisSaldos

    Public Class Contenedor
#Region "ConsultarContenedorxPosicion"
        Public Shared Function ConsultarContenedorxPosicion(Peticion As ConsultarContenedorxPosicion.Peticion) As ConsultarContenedorxPosicion.Respuesta

            Dim objRespuesta As New ConsultarContenedorxPosicion.Respuesta

            Try

                If Peticion.Contenedor.sector.codDelegacion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_Delegacion")))
                ElseIf Peticion.Contenedor.sector.codPlanta Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_Planta")))
                ElseIf Peticion.Contenedor.sector.codSector Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_Sector")))
                Else
                    Dim dsCont As DataSet = AccesoDatos.GenesisSaldos.Contenedor.ConsultarContenedorxPosicion(Peticion)
                    objRespuesta = PreencheConsultarContenedoresxPosicion(dsCont)
                    objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                    objRespuesta.MensajeError = String.Empty
                End If
            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.CodigoError = ex.Codigo
                objRespuesta.MensajeError = ex.Descricao
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString
            End Try

            Return objRespuesta
        End Function

        Private Shared Function PreencheConsultarContenedoresxPosicion(dsRetorno As DataSet) As ConsultarContenedorxPosicion.Respuesta
            Dim objRespuesta As New ConsultarContenedorxPosicion.Respuesta
            If dsRetorno IsNot Nothing AndAlso dsRetorno.Tables.Count > 0 Then
                If dsRetorno.Tables.Contains("CONTENEDORES") Then
                    Dim dtContenedores = dsRetorno.Tables("CONTENEDORES") '.DefaultView.ToTable(True, "OID_CONTENEDOR", "COD_TIPO_CONTENEDOR", "DES_TIPO_CONTENEDOR", "GMT_CREACION", "DIAS_ARMADO", "FEC_VENCIMIENTO", "DIAS_VENCIDO")
                    If dtContenedores IsNot Nothing AndAlso dtContenedores.Rows.Count > 0 Then
                        If objRespuesta.contenedores Is Nothing Then
                            objRespuesta.contenedores = New List(Of ConsultarContenedorxPosicion.ContenedorRespuesta)
                        End If

                        Dim objContenedor As ConsultarContenedorxPosicion.ContenedorRespuesta = Nothing
                        Dim IdentificadorContenedor As String = String.Empty

                        For Each dtRow In dtContenedores.Rows

                            IdentificadorContenedor = Util.AtribuirValorObj(dtRow("OID_CONTENEDOR"), GetType(String))

                            objContenedor = (From c In objRespuesta.contenedores Where c.identificador = IdentificadorContenedor).FirstOrDefault

                            If objContenedor Is Nothing Then


                                objRespuesta.contenedores.Add(New ConsultarContenedorxPosicion.ContenedorRespuesta With { _
                                                              .identificador = IdentificadorContenedor,
                                                              .codTipoContenedor = Util.AtribuirValorObj(dtRow("COD_TIPO_CONTENEDOR"), GetType(String)),
                                                              .desTipoContenedor = Util.AtribuirValorObj(dtRow("DES_TIPO_CONTENEDOR"), GetType(String)),
                                                              .fechaArmado = Util.AtribuirValorObj(dtRow("FECHAARMADO"), GetType(DateTime)),
                                                              .fechaArmadoMobile = Util.AtribuirValorObj(dtRow("FECHAARMADO"), GetType(DateTime)).ToString(),
                                                              .precintos = New List(Of ConsultarContenedorxPosicion.Precinto),
                                                              .detEfectivo = PreencheConsultarContenedorxPosicionDetEfec(dsRetorno, IdentificadorContenedor),
                                                             .cliente = New ConsultarContenedorxPosicion.Cliente With {.oidCliente = Util.AtribuirValorObj(dtRow("OID_CLIENTE"), GetType(String)), _
                                                                                          .codCliente = Util.AtribuirValorObj(dtRow("COD_CLIENTE"), GetType(String)), _
                                                                                          .desCliente = Util.AtribuirValorObj(dtRow("DES_CLIENTE"), GetType(String)), _
                                                                                          .codSubcliente = Util.AtribuirValorObj(dtRow("COD_SUBCLIENTE"), GetType(String)), _
                                                                                          .oidPuntoServicio = Util.AtribuirValorObj(dtRow("OID_PTO_SERVICIO"), GetType(String))},
                                                              .Sector = New ConsultarContenedorxPosicion.SectorRespuesta With {.oidDelegacion = Util.AtribuirValorObj(dtRow("OID_DELEGACION"), GetType(String)), _
                                                                                                                               .codDelegacion = Util.AtribuirValorObj(dtRow("COD_DELEGACION"), GetType(String)), _
                                                                                                                               .desDelegacion = Util.AtribuirValorObj(dtRow("DES_DELEGACION"), GetType(String)), _
                                                                                                                               .codPlanta = Util.AtribuirValorObj(dtRow("COD_PLANTA"), GetType(String)), _
                                                                                                                               .oidPlanta = Util.AtribuirValorObj(dtRow("OID_PLANTA"), GetType(String)), _
                                                                                                                               .desPlanta = Util.AtribuirValorObj(dtRow("DES_PLANTA"), GetType(String)), _
                                                                                                                               .codPosicion = Util.AtribuirValorObj(dtRow("COD_POSICION"), GetType(String)), _
                                                                                                                               .desSector = Util.AtribuirValorObj(dtRow("DES_SECTOR"), GetType(String)), _
                                                                                                                               .codSector = Util.AtribuirValorObj(dtRow("COD_SECTOR"), GetType(String)), _
                                                                                                                               .oidSector = Util.AtribuirValorObj(dtRow("OID_SECTOR"), GetType(String))},
                                                               .canal = New ConsultarContenedorxPosicion.Canal With {.oidCanal = Util.AtribuirValorObj(dtRow("OID_CANAL"), GetType(String)), _
                                                                                          .codCanal = Util.AtribuirValorObj(dtRow("COD_CANAL"), GetType(String)), _
                                                                                          .codSubcanal = Util.AtribuirValorObj(dtRow("COD_SUBCANAL"), GetType(String)), _
                                                                                          .oidSubCanal = Util.AtribuirValorObj(dtRow("OID_SUBCANAL"), GetType(String)), _
                                                                                          .desCanal = Util.AtribuirValorObj(dtRow("DES_CANAL"), GetType(String)), _
                                                                                          .desSubCanal = Util.AtribuirValorObj(dtRow("DES_SUBCANAL"), GetType(String))}})

                                objContenedor = (From c In objRespuesta.contenedores Where c.identificador = IdentificadorContenedor).FirstOrDefault

                            End If

                            objContenedor.precintos.Add(New ConsultarContenedorxPosicion.Precinto With { _
                                                        .CodigoPrecinto = Util.AtribuirValorObj(dtRow("COD_PRECINTO"), GetType(String)),
                                                        .EsAutomatico = Util.AtribuirValorObj(dtRow("BOL_PRECINTO_AUTOMATICO"), GetType(Boolean))})
                        Next

                    End If
                End If
            End If
            Return objRespuesta
        End Function
#End Region


#Region "ConsultarContenedoresSector"
        Public Shared Function ConsultarContenedoresSector(Peticion As ConsultarContenedoresSector.Peticion) As ConsultarContenedoresSector.Respuesta

            Dim objRespuesta As New ConsultarContenedoresSector.Respuesta

            Try

                Dim dsContSector As DataSet = AccesoDatos.GenesisSaldos.Contenedor.ConsultarContenedoresSector(Peticion)

                objRespuesta = PreencheConsultarContenedoresSector(dsContSector)

            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.Mensajes.Add(ex.Descricao)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.Excepciones.Add(ex.ToString)
            End Try

            Return objRespuesta
        End Function

        Private Shared Function PreencheConsultarContenedoresSector(dsContSector As DataSet) As ConsultarContenedoresSector.Respuesta

            Dim objRespuesta As New ConsultarContenedoresSector.Respuesta
            Dim objContenedor As ConsultarContenedoresSector.ContenedorRespuesta = Nothing
            Dim objCuenta As ConsultarContenedoresSector.Cuenta = Nothing

            If dsContSector IsNot Nothing AndAlso dsContSector.Tables.Count > 0 Then

                If dsContSector.Tables.Contains("CONTENEDORES") Then

                    Dim dtContenedores = dsContSector.Tables("CONTENEDORES")

                    If dtContenedores IsNot Nothing AndAlso dtContenedores.Rows.Count > 0 Then

                        If objRespuesta.Contenedores Is Nothing Then
                            objRespuesta.Contenedores = New List(Of ConsultarContenedoresSector.ContenedorRespuesta)
                        End If

                        Dim CodigoPrecinto As String = String.Empty
                        Dim IdentificadorContenedor As String = String.Empty
                        Dim CodigoDelegacion As String = String.Empty
                        Dim CodigoPlanta As String = String.Empty
                        Dim CodigoSector As String = String.Empty
                        Dim CodigoCliente As String = String.Empty
                        Dim DescricaoCliente As String = String.Empty
                        Dim CodigoSubCliente As String = String.Empty
                        Dim CodigoPuntoServicio As String = String.Empty
                        Dim CodigoCanal As String = String.Empty
                        Dim CodigoSubCanal As String = String.Empty
                        Dim IdentificadorCuenta As String = String.Empty
                        Dim PrecintoAutomatico As Boolean = False
                        Dim DescricaoSubCliente As String = String.Empty
                        Dim DescricaoPuntoServicio As String = String.Empty
                        Dim DescricaoTipoContenedor As String = String.Empty
                        Dim DescricaoCanal As String = String.Empty
                        Dim DescricaoSubCanal As String = String.Empty
                        Dim CodigoTipoContenedor As String = String.Empty
                        Dim DescripcionTipoContenedor As String = String.Empty

                        For Each dtRow In dtContenedores.Rows

                            CodigoPrecinto = Util.AtribuirValorObj(dtRow("COD_PRECINTO"), GetType(String))
                            IdentificadorContenedor = Util.AtribuirValorObj(dtRow("OID_CONTENEDOR"), GetType(String))

                            CodigoDelegacion = Util.AtribuirValorObj(dtRow("COD_DELEGACION"), GetType(String))
                            CodigoPlanta = Util.AtribuirValorObj(dtRow("COD_PLANTA"), GetType(String))
                            CodigoSector = Util.AtribuirValorObj(dtRow("COD_SECTOR"), GetType(String))

                            CodigoCliente = Util.AtribuirValorObj(dtRow("COD_CLIENTE"), GetType(String))
                            DescricaoCliente = Util.AtribuirValorObj(dtRow("DES_CLIENTE"), GetType(String))
                            CodigoSubCliente = Util.AtribuirValorObj(dtRow("COD_SUBCLIENTE"), GetType(String))
                            CodigoPuntoServicio = Util.AtribuirValorObj(dtRow("COD_PTO_SERVICIO"), GetType(String))
                            CodigoCanal = Util.AtribuirValorObj(dtRow("COD_CANAL"), GetType(String))
                            CodigoSubCanal = Util.AtribuirValorObj(dtRow("COD_SUBCANAL"), GetType(String))
                            IdentificadorCuenta = Util.AtribuirValorObj(dtRow("OID_CUENTA"), GetType(String))
                            PrecintoAutomatico = Convert.ToBoolean(Util.AtribuirValorObj(dtRow("BOL_PRECINTO_AUTOMATICO"), GetType(Integer)))

                            DescricaoSubCliente = Util.AtribuirValorObj(dtRow("DES_SUBCLIENTE"), GetType(String))
                            DescricaoPuntoServicio = Util.AtribuirValorObj(dtRow("DES_PTO_SERVICIO"), GetType(String))

                            DescricaoCanal = Util.AtribuirValorObj(dtRow("DES_CANAL"), GetType(String))
                            DescricaoSubCanal = Util.AtribuirValorObj(dtRow("DES_SUBCANAL"), GetType(String))

                            CodigoTipoContenedor = Util.AtribuirValorObj(dtRow("COD_TIPO_CONTENEDOR"), GetType(String))
                            DescricaoTipoContenedor = Util.AtribuirValorObj(dtRow("DES_TIPO_CONTENEDOR"), GetType(String))

                            objContenedor = (From c In objRespuesta.Contenedores
                                             Where c.IdentificadorContenedor = IdentificadorContenedor).FirstOrDefault

                            If objContenedor Is Nothing Then

                                objRespuesta.Contenedores.Add(New ConsultarContenedoresSector.ContenedorRespuesta With { _
                                                              .IdentificadorContenedor = IdentificadorContenedor,
                                                              .codEstadoContenedor = Util.AtribuirValorObj(dtRow("COD_ESTADO"), GetType(String)),
                                                              .fechaHoraArmado = Util.AtribuirValorObj(dtRow("GMT_CREACION"), GetType(DateTime)),
                                                              .FechaVencimento = Util.AtribuirValorObj(dtRow("FEC_VENCIMIENTO"), GetType(DateTime)),
                                                              .CodigoPosicion = Util.AtribuirValorObj(dtRow("COD_POSICION"), GetType(String)),
                                                              .UsuarioCreacion = Util.AtribuirValorObj(dtRow("DES_USUARIO_CREACION"), GetType(String)),
                                                              .Precintos = New List(Of String),
                                                              .Sector = New ConsultarContenedoresSector.SectorRespuesta With { _
                                                                        .CodigoDelegacion = CodigoDelegacion,
                                                                        .CodigoPlanta = CodigoPlanta,
                                                                        .CodigoSector = CodigoSector},
                                                              .Cuentas = New List(Of ConsultarContenedoresSector.Cuenta),
                                                              .codTipoContenedor = CodigoTipoContenedor,
                                                              .DesTipoContenedor = DescricaoTipoContenedor,
                                                              .CodPuesto = Util.AtribuirValorObj(dtRow("COD_PUESTO"), GetType(String)),
                                                              .AceptaPico = Util.AtribuirValorObj(dtRow("NEC_CANTIDAD"), GetType(Integer))})

                                objContenedor = (From c In objRespuesta.Contenedores
                                            Where c.IdentificadorContenedor = Util.AtribuirValorObj(dtRow("OID_CONTENEDOR"), GetType(String))).FirstOrDefault

                            End If

                            If Not objContenedor.Precintos.Exists(Function(p) p = CodigoPrecinto) Then
                                objContenedor.Precintos.Add(Util.AtribuirValorObj(dtRow("COD_PRECINTO"), GetType(String)))

                                If PrecintoAutomatico Then
                                    objContenedor.PrecintoAutomatico = CodigoPrecinto
                                End If

                            End If

                            objCuenta = (From c In objContenedor.Cuentas Where c.CodigoCliente = CodigoCliente AndAlso
                                                                               c.CodigoSubCliente = CodigoSubCliente AndAlso
                                                                               c.CodigoPuntoServicio = CodigoPuntoServicio AndAlso
                                                                               c.CodigoCanal = CodigoCanal AndAlso
                                                                               c.CodigoSubCanal = CodigoSubCanal).FirstOrDefault

                            If objCuenta Is Nothing Then

                                objContenedor.Cuentas.Add(New ConsultarContenedoresSector.Cuenta With { _
                                                           .IdentificadorCuenta = IdentificadorCuenta,
                                                           .CodigoCliente = CodigoCliente,
                                                           .DescricaoCliente = DescricaoCliente,
                                                           .CodigoSubCliente = CodigoSubCliente,
                                                           .CodigoPuntoServicio = CodigoPuntoServicio,
                                                           .CodigoCanal = CodigoCanal,
                                                           .CodigoSubCanal = CodigoSubCanal,
                                                           .DescripcionCanal = DescricaoCanal,
                                                           .DescripcionSubCanal = DescricaoSubCanal,
                                                           .DescripcionSubCliente = DescricaoSubCliente,
                                                           .DescripcionPuntoServicio = DescricaoPuntoServicio,
                                                           .DetallesEfectivo = New List(Of ConsultarContenedoresSector.DetalleEfectivo),
                                                           .DetallesMedioPago = New List(Of ConsultarContenedoresSector.DetalleMedioPago)})

                                objCuenta = (From c In objContenedor.Cuentas Where c.CodigoCliente = CodigoCliente AndAlso
                                                                                   c.CodigoSubCliente = CodigoSubCliente AndAlso
                                                                                   c.CodigoPuntoServicio = CodigoPuntoServicio AndAlso
                                                                                   c.CodigoCanal = CodigoCanal AndAlso
                                                                                   c.CodigoSubCanal = CodigoSubCanal).FirstOrDefault

                                If dsContSector.Tables.Contains("EFECTIVOS") Then

                                    Dim dtEfectivos = dsContSector.Tables("EFECTIVOS")

                                    If dtEfectivos IsNot Nothing AndAlso dtEfectivos.Rows.Count > 0 Then

                                        For Each dr In dtEfectivos.Rows

                                            If Util.AtribuirValorObj(dr("OID_CUENTA"), GetType(String)) = IdentificadorCuenta AndAlso
                                                Util.AtribuirValorObj(dr("OID_CONTENEDOR"), GetType(String)) = IdentificadorContenedor Then

                                                objCuenta.DetallesEfectivo.Add(New ConsultarContenedoresSector.DetalleEfectivo With { _
                                                                               .Bloqueado = Util.AtribuirValorObj(dr("BOL_BLOQUEADO"), GetType(Boolean)),
                                                                               .Cantidad = Util.AtribuirValorObj(dr("NEL_CANTIDAD"), GetType(Integer)),
                                                                               .CodigoCalidad = Util.AtribuirValorObj(dr("COD_CALIDAD"), GetType(String)),
                                                                               .CodigoDenominacion = Util.AtribuirValorObj(dr("COD_DENOMINACION"), GetType(String)),
                                                                               .NumValor = Util.AtribuirValorObj(dr("NUM_VALOR"), GetType(Integer)),
                                                                               .CodigoIsoDivisa = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String)),
                                                                               .Disponible = Util.AtribuirValorObj(dr("BOL_DISPONIBLE"), GetType(Boolean)),
                                                                               .Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Decimal))})





                                            End If

                                        Next

                                    End If

                                End If

                                If dsContSector.Tables.Contains("MEDIOS_PAGO") Then

                                    Dim dtMedioPago = dsContSector.Tables("MEDIOS_PAGO")

                                    If dtMedioPago IsNot Nothing AndAlso dtMedioPago.Rows.Count > 0 Then

                                        For Each dr In dtMedioPago.Rows

                                            If Util.AtribuirValorObj(dr("OID_CUENTA"), GetType(String)) = IdentificadorCuenta AndAlso
                                                Util.AtribuirValorObj(dr("OID_CONTENEDOR"), GetType(String)) = IdentificadorContenedor Then

                                                objCuenta.DetallesMedioPago.Add(New ConsultarContenedoresSector.DetalleMedioPago With { _
                                                                               .Bloqueado = Util.AtribuirValorObj(dr("BOL_BLOQUEADO"), GetType(Boolean)),
                                                                               .Cantidad = Util.AtribuirValorObj(dr("NEL_CANTIDAD"), GetType(Integer)),
                                                                               .CodigoTipoMedioPago = Extenciones.RecuperarEnum(Of Comon.Enumeradores.TipoMedioPago)(Util.AtribuirValorObj(dr("COD_TIPO_MEDIO_PAGO"), GetType(String))),
                                                                               .CodigoMedioPago = Util.AtribuirValorObj(dr("COD_MEDIO_PAGO"), GetType(String)),
                                                                               .CodigoIsoDivisa = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String)),
                                                                               .Disponible = Util.AtribuirValorObj(dr("BOL_DISPONIBLE"), GetType(Boolean)),
                                                                               .Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Decimal))})





                                            End If

                                        Next

                                    End If

                                End If

                            End If

                        Next

                    End If
                End If
            End If

            Return objRespuesta
        End Function

        'Private Shared Function PreencheConsultarContenedoresSectorContenedorRespuesta(dsContSector As DataSet, oidContenedor As String) As ConsultarContenedoresSector.ContenedorRespuesta
        '    Dim objContenedor As ConsultarContenedoresSector.ContenedorRespuesta = Nothing
        '    If dsContSector.Tables.Contains("CONTENEDORES") Then
        '        Dim dtContenedor As DataTable = dsContSector.Tables("CONTENEDORES")
        '        If dtContenedor.Rows.Count > 0 Then
        '            For Each dtRow In dtContenedor.Select(String.Format("OID_CONTENEDOR = '{0}'", oidContenedor))
        '                objContenedor = New ConsultarContenedoresSector.ContenedorRespuesta
        '                With objContenedor
        '                    .codEstadoContenedor = Util.AtribuirValorObj(dtRow("COD_ESTADO"), GetType(String))
        '                    .codTipoContenedor = Util.AtribuirValorObj(dtRow("COD_TIPO_CONTENEDOR"), GetType(String))
        '                    .fechaHoraArmado = Util.AtribuirValorObj(dtRow("GMT_CREACION"), GetType(DateTime))
        '                    .Precintos = PreenchePrecintos(dsContSector, oidContenedor)
        '                    .Sectores = PreencheConsultarContenedoresSectores(dsContSector, oidContenedor)
        '                    '.Canais = PreencheConsultarContenedoresClienteCanais(dsContSector, oidContenedor)
        '                    '.DetalheEfectivo = PreencheConsultarContenedoresClienteDetEfec(dsContSector, oidContenedor)
        '                    '.DetalheMedioPago = PreencheConsultarContenedoresClienteDetMP(dsContSector, oidContenedor)
        '                End With
        '                Return objContenedor
        '            Next
        '        End If
        '    End If
        '    Return objContenedor
        'End Function

        'Private Shared Function PreencheConsultarContenedoresSectores(dsContCliente As DataSet, oidContenedor As String) As List(Of ConsultarContenedoresSector.Sector)
        '    Dim lstSectores As List(Of ConsultarContenedoresSector.Sector) = Nothing
        '    If dsContCliente.Tables.Contains("CLIENTES") Then
        '        Dim dtSectores As DataTable = dsContCliente.Tables("CLIENTES").DefaultView.ToTable(True, "OID_CONTENEDOR", "COD_DELEGACION", "COD_PLANTA", "COD_SECTOR")
        '        If dtSectores.Rows.Count > 0 Then
        '            lstSectores = New List(Of ConsultarContenedoresSector.Sector)
        '            For Each dtRow In dtSectores.Select(String.Format("OID_CONTENEDOR = '{0}'", oidContenedor))
        '                Dim objSector As New ConsultarContenedoresSector.Sector
        '                With objSector
        '                    .codDelegacion = Util.AtribuirValorObj(dtRow("COD_DELEGACION"), GetType(String))
        '                    .codPlanta = Util.AtribuirValorObj(dtRow("COD_PLANTA"), GetType(String))
        '                    .codSector = Util.AtribuirValorObj(dtRow("COD_SECTOR"), GetType(String))
        '                End With
        '                lstSectores.Add(objSector)
        '            Next
        '        End If
        '    End If
        '    Return lstSectores
        'End Function
#End Region

#Region "ConsultarContenedoresCliente"

        Public Shared Function ConsultarContenedoresCliente(Peticion As ConsultarContenedoresCliente.Peticion) As ConsultarContenedoresCliente.Respuesta

            Dim objRespuesta As New ConsultarContenedoresCliente.Respuesta

            Try
                If Peticion.Clientes.FindAll(Function(x) x.codCliente Is Nothing).Count > 0 Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_cliente")))
                End If

                If Peticion.Sectores.FindAll(Function(x) x.codDelegacion Is Nothing).Count > 0 Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_codDelegacion")))
                End If

                If Peticion.Sectores.FindAll(Function(x) x.codPlanta Is Nothing).Count > 0 Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_codPlanta")))
                End If

                If Peticion.Sectores.FindAll(Function(x) x.codSector Is Nothing).Count > 0 Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_codSector")))
                End If

                If Peticion.Canais.FindAll(Function(x) x.codCanal Is Nothing).Count > 0 Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_codCanal")))
                End If

                If Peticion.Canais.FindAll(Function(x) x.codSubcanal Is Nothing).Count > 0 Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_codSubCanal")))
                End If

                Dim dsContCliente As DataSet = AccesoDatos.GenesisSaldos.Contenedor.ConsultarContenedoresCliente(Peticion)

                objRespuesta = PreencheConsultarContenedoresCliente(dsContCliente)

            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.Mensajes.Add(ex.Descricao)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.Excepciones.Add(ex.ToString)
            End Try

            Return objRespuesta
        End Function

        Private Shared Function PreencheConsultarContenedoresCliente(dsContCliente As DataSet) As ConsultarContenedoresCliente.Respuesta
            Dim objRespuesta As New ConsultarContenedoresCliente.Respuesta
            If dsContCliente IsNot Nothing AndAlso dsContCliente.Tables.Count > 0 Then
                If dsContCliente.Tables.Contains("CLIENTES") Then
                    Dim dtClientes = dsContCliente.Tables("CLIENTES")
                    If dtClientes IsNot Nothing AndAlso dtClientes.Rows.Count > 0 Then
                        If objRespuesta.Clientes Is Nothing Then
                            objRespuesta.Clientes = New List(Of ConsultarContenedoresCliente.ClienteRespuesta)
                        End If
                        For Each dtRow In dtClientes.Rows
                            Dim codCliente As String = Util.AtribuirValorObj(dtRow("COD_CLIENTE"), GetType(String))
                            Dim codSubcliente As String = Util.AtribuirValorObj(dtRow("COD_SUBCLIENTE"), GetType(String))
                            Dim codPuntoServicio As String = Util.AtribuirValorObj(dtRow("COD_PTO_SERVICIO"), GetType(String))
                            Dim objCliente As ConsultarContenedoresCliente.ClienteRespuesta = objRespuesta.Clientes.Find(Function(a) a.codCliente = codCliente AndAlso a.codSubcliente = codSubcliente AndAlso a.codSubcliente = codSubcliente)
                            If objCliente Is Nothing Then
                                objCliente = New ConsultarContenedoresCliente.ClienteRespuesta
                                With objCliente
                                    .codCliente = codCliente
                                    .codSubcliente = codSubcliente
                                    .codPuntoServicio = codPuntoServicio
                                    .Contenedores = New List(Of ConsultarContenedoresCliente.ContenedorRespuesta)
                                    .Contenedores.Add(PreencheConsultarContenedoresClienteContenedorRespuesta(dsContCliente, Util.AtribuirValorObj(dtRow("OID_CONTENEDOR"), GetType(String))))
                                End With
                                objRespuesta.Clientes.Add(objCliente)
                            Else
                                If objCliente.Contenedores Is Nothing Then
                                    objCliente.Contenedores = New List(Of ConsultarContenedoresCliente.ContenedorRespuesta)
                                End If
                                objCliente.Contenedores.Add(PreencheConsultarContenedoresClienteContenedorRespuesta(dsContCliente, Util.AtribuirValorObj(dtRow("OID_CONTENEDOR"), GetType(String))))
                            End If
                        Next
                    End If
                End If
            End If

            Return objRespuesta
        End Function

        Private Shared Function PreencheConsultarContenedoresClienteContenedorRespuesta(dsContCliente As DataSet, oidContenedor As String) As ConsultarContenedoresCliente.ContenedorRespuesta
            Dim objContenedor As ConsultarContenedoresCliente.ContenedorRespuesta = Nothing
            If dsContCliente.Tables.Contains("CONTENEDORES") Then
                Dim dtContenedor As DataTable = dsContCliente.Tables("CONTENEDORES")
                If dtContenedor.Rows.Count > 0 Then
                    For Each dtRow In dtContenedor.Select(String.Format("OID_CONTENEDOR = '{0}'", oidContenedor))
                        objContenedor = New ConsultarContenedoresCliente.ContenedorRespuesta
                        With objContenedor
                            .codEstadoContenedor = Util.AtribuirValorObj(dtRow("COD_ESTADO"), GetType(String))
                            .codTipoContenedor = Util.AtribuirValorObj(dtRow("COD_TIPO_CONTENEDOR"), GetType(String))
                            .fechaHoraArmado = Util.AtribuirValorObj(dtRow("GMT_CREACION"), GetType(DateTime))
                            .Precintos = PreenchePrecintos(dsContCliente, oidContenedor)
                            .Sectores = PreencheConsultarContenedoresClienteSectores(dsContCliente, oidContenedor)
                            .Canais = PreencheConsultarContenedoresClienteCanais(dsContCliente, oidContenedor)
                            .DetalheEfectivo = PreencheConsultarContenedoresClienteDetEfec(dsContCliente, oidContenedor)
                            .DetalheMedioPago = PreencheConsultarContenedoresClienteDetMP(dsContCliente, oidContenedor)
                        End With
                        Return objContenedor
                    Next
                End If
            End If
            Return objContenedor
        End Function

        Private Shared Function PreenchePrecintos(dsRetorno As DataSet, oidContenedor As String) As System.Collections.ObjectModel.ObservableCollection(Of String)
            Dim lstPrecinto As System.Collections.ObjectModel.ObservableCollection(Of String) = Nothing
            If dsRetorno.Tables.Contains("PRECINTOS") Then
                Dim dtPrecinto As DataTable = dsRetorno.Tables("PRECINTOS").DefaultView.ToTable(True, "OID_CONTENEDOR", "OID_PRECINTOXCONTENEDOR", "COD_PRECINTO")
                If dtPrecinto.Rows.Count > 0 Then
                    lstPrecinto = New System.Collections.ObjectModel.ObservableCollection(Of String)
                    For Each dtRow In dtPrecinto.Select(String.Format("OID_CONTENEDOR = '{0}'", oidContenedor))
                        lstPrecinto.Add(Util.AtribuirValorObj(dtRow("COD_PRECINTO"), GetType(String)))
                    Next
                End If
            End If
            Return lstPrecinto
        End Function

        Private Shared Function PreencheConsultarContenedoresClienteSectores(dsContCliente As DataSet, oidContenedor As String) As List(Of ConsultarContenedoresCliente.Sector)
            Dim lstSectores As List(Of ConsultarContenedoresCliente.Sector) = Nothing
            If dsContCliente.Tables.Contains("CLIENTES") Then
                Dim dtSectores As DataTable = dsContCliente.Tables("CLIENTES").DefaultView.ToTable(True, "OID_CONTENEDOR", "COD_DELEGACION", "COD_PLANTA", "COD_SECTOR")
                If dtSectores.Rows.Count > 0 Then
                    lstSectores = New List(Of ConsultarContenedoresCliente.Sector)
                    For Each dtRow In dtSectores.Select(String.Format("OID_CONTENEDOR = '{0}'", oidContenedor))
                        Dim objSector As New ConsultarContenedoresCliente.Sector
                        With objSector
                            .codDelegacion = Util.AtribuirValorObj(dtRow("COD_DELEGACION"), GetType(String))
                            .codPlanta = Util.AtribuirValorObj(dtRow("COD_PLANTA"), GetType(String))
                            .codSector = Util.AtribuirValorObj(dtRow("COD_SECTOR"), GetType(String))
                        End With
                        lstSectores.Add(objSector)
                    Next
                End If
            End If
            Return lstSectores
        End Function

        Private Shared Function PreencheConsultarContenedoresClienteCanais(dsContCliente As DataSet, oidContenedor As String) As List(Of ConsultarContenedoresCliente.Canal)
            Dim lstCanais As List(Of ConsultarContenedoresCliente.Canal) = Nothing
            If dsContCliente.Tables.Contains("CLIENTES") Then
                Dim dtCanais As DataTable = dsContCliente.Tables("CLIENTES").DefaultView.ToTable(True, "OID_CONTENEDOR", "COD_CANAL", "COD_SUBCANAL")
                If dtCanais.Rows.Count > 0 Then
                    lstCanais = New List(Of ConsultarContenedoresCliente.Canal)
                    For Each dtRow In dtCanais.Select(String.Format("OID_CONTENEDOR = '{0}'", oidContenedor))
                        Dim objCanal As New ConsultarContenedoresCliente.Canal
                        With objCanal
                            .codCanal = Util.AtribuirValorObj(dtRow("COD_CANAL"), GetType(String))
                            .codSubcanal = Util.AtribuirValorObj(dtRow("COD_SUBCANAL"), GetType(String))
                        End With
                        lstCanais.Add(objCanal)
                    Next
                End If
            End If
            Return lstCanais
        End Function

        Private Shared Function PreencheConsultarContenedoresClienteDetEfec(dsContCliente As DataSet, oidContenedor As String) As List(Of ConsultarContenedoresCliente.DetalleEfectivo)
            Dim lstDetEfec As List(Of ConsultarContenedoresCliente.DetalleEfectivo) = Nothing
            If dsContCliente.Tables.Contains("DETEFECMP") Then
                Dim dtEfecMP As DataTable = dsContCliente.Tables("DETEFECMP").DefaultView.ToTable(True, "OID_CONTENEDOR", "COD_ISO_DIVISA", "COD_DENOMINACION", "COD_CALIDAD", "COD_TIPO_MEDIO_PAGO", "COD_MEDIO_PAGO", "BOL_DISPONIBLE", "BOL_BLOQUEADO", "NEL_CANTIDAD", "NUM_IMPORTE")
                If dtEfecMP.Rows.Count > 0 Then
                    lstDetEfec = New List(Of ConsultarContenedoresCliente.DetalleEfectivo)
                    For Each dtRow In dtEfecMP.Select(String.Format("OID_CONTENEDOR = '{0}' AND COD_MEDIO_PAGO IS NULL", oidContenedor))
                        Dim objDetEfec As New ConsultarContenedoresCliente.DetalleEfectivo
                        With objDetEfec
                            .codDivisa = Util.AtribuirValorObj(dtRow("COD_ISO_DIVISA"), GetType(String))
                            .codDenominacion = Util.AtribuirValorObj(dtRow("COD_DENOMINACION"), GetType(String))
                            .codCalidad = Util.AtribuirValorObj(dtRow("COD_CALIDAD"), GetType(String))
                            .disponible = Util.AtribuirValorObj(dtRow("BOL_DISPONIBLE"), GetType(Boolean))
                            .bloqueado = Util.AtribuirValorObj(dtRow("BOL_BLOQUEADO"), GetType(Boolean))
                            .cantidad = Util.AtribuirValorObj(dtRow("NEL_CANTIDAD"), GetType(Integer))
                            .importe = Util.AtribuirValorObj(dtRow("NUM_IMPORTE"), GetType(Decimal))
                        End With
                        lstDetEfec.Add(objDetEfec)
                    Next
                End If
            End If
            Return lstDetEfec
        End Function

        Private Shared Function PreencheConsultarContenedoresClienteDetMP(dsContCliente As DataSet, oidContenedor As String) As List(Of ConsultarContenedoresCliente.DetalleMedioPago)
            Dim lstDetMP As List(Of ConsultarContenedoresCliente.DetalleMedioPago) = Nothing
            If dsContCliente.Tables.Contains("DETMPMP") Then
                Dim dtEfecMP As DataTable = dsContCliente.Tables("DETMPMP").DefaultView.ToTable(True, "OID_CONTENEDOR", "COD_ISO_DIVISA", "COD_DENOMINACION", "COD_CALIDAD", "COD_TIPO_MEDIO_PAGO", "COD_MEDIO_PAGO", "BOL_DISPONIBLE", "BOL_BLOQUEADO", "NEL_CANTIDAD", "NUM_IMPORTE")
                If dtEfecMP.Rows.Count > 0 Then
                    lstDetMP = New List(Of ConsultarContenedoresCliente.DetalleMedioPago)
                    For Each dtRow In dtEfecMP.Select(String.Format("OID_CONTENEDOR = '{0}' AND COD_MEDIO_PAGO IS NOT NULL", oidContenedor))
                        Dim objDetMP As New ConsultarContenedoresCliente.DetalleMedioPago
                        With objDetMP
                            .codDivisa = Util.AtribuirValorObj(dtRow("COD_ISO_DIVISA"), GetType(String))
                            .codTipoMedioPago = Util.AtribuirValorObj(dtRow("COD_TIPO_MEDIO_PAGO"), GetType(String))
                            .codMedioPago = Util.AtribuirValorObj(dtRow("COD_MEDIO_PAGO"), GetType(String))
                            .disponible = Util.AtribuirValorObj(dtRow("BOL_DISPONIBLE"), GetType(Boolean))
                            .bloqueado = Util.AtribuirValorObj(dtRow("BOL_BLOQUEADO"), GetType(Boolean))
                            .cantidad = Util.AtribuirValorObj(dtRow("NEL_CANTIDAD"), GetType(Integer))
                            .importe = Util.AtribuirValorObj(dtRow("NUM_IMPORTE"), GetType(Decimal))
                        End With
                        lstDetMP.Add(objDetMP)
                    Next
                End If
            End If
            Return lstDetMP
        End Function

#End Region

#Region "ConsultarContenedoresPackModular"

        Public Shared Function ConsultarContenedoresPackModular(Peticion As ConsultarContenedoresPackModular.Peticion) As ConsultarContenedoresPackModular.Respuesta

            Dim objRespuesta As New ConsultarContenedoresPackModular.Respuesta

            Try

                Dim dsRetorno As DataSet = AccesoDatos.GenesisSaldos.Contenedor.ConsultarContenedoresPackModular(Peticion)

                objRespuesta = PreencheConsultarContenedoresPackModular(dsRetorno)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = String.Empty

            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.CodigoError = ex.Codigo
                objRespuesta.MensajeError = ex.Descricao
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString
            End Try

            Return objRespuesta
        End Function

        Private Shared Function PreencheConsultarContenedoresPackModular(dsRetorno As DataSet) As ConsultarContenedoresPackModular.Respuesta
            Dim objRespuesta As New ConsultarContenedoresPackModular.Respuesta
            If dsRetorno IsNot Nothing AndAlso dsRetorno.Tables.Count > 0 Then
                If dsRetorno.Tables.Contains("CONTENEDORES") Then
                    Dim dtContenedores = dsRetorno.Tables("CONTENEDORES").DefaultView.ToTable(True, "OID_CONTENEDOR", "COD_TIPO_CONTENEDOR", "DES_TIPO_CONTENEDOR", "GMT_CREACION", "DIAS_ARMADO", "FEC_VENCIMIENTO", "DIAS_VENCIDO")
                    If dtContenedores IsNot Nothing AndAlso dtContenedores.Rows.Count > 0 Then
                        If objRespuesta.contenedores Is Nothing Then
                            objRespuesta.contenedores = New List(Of ConsultarContenedoresPackModular.Contenedor)
                        End If
                        For Each dtRow In dtContenedores.Rows
                            Dim oidContenedor As String = Util.AtribuirValorObj(dtRow("OID_CONTENEDOR"), GetType(String))
                            Dim objContenedor As New ConsultarContenedoresPackModular.Contenedor
                            With objContenedor
                                .codTipoContenedor = Util.AtribuirValorObj(dtRow("COD_TIPO_CONTENEDOR"), GetType(String))
                                .desTipoContenedor = Util.AtribuirValorObj(dtRow("DES_TIPO_CONTENEDOR"), GetType(String))
                                .fechaArmado = Util.AtribuirValorObj(dtRow("GMT_CREACION"), GetType(DateTime))
                                .diasArmado = Util.AtribuirValorObj(dtRow("DIAS_ARMADO"), GetType(Integer))
                                .fechaVencimiento = Util.AtribuirValorObj(dtRow("FEC_VENCIMIENTO"), GetType(DateTime))
                                .diasVencido = Util.AtribuirValorObj(dtRow("DIAS_VENCIDO"), GetType(Integer))
                                .precintos = PreenchePrecintos(dsRetorno, oidContenedor)
                                .alertasVencimento = PreencheConsultarContenedoresPackModularAlertas(dsRetorno, oidContenedor)
                                .sector = PreencheConsultarContenedoresPackModularSector(dsRetorno, oidContenedor)
                                .clientes = PreencheConsultarContenedoresPackModularCliente(dsRetorno, oidContenedor)
                                .canales = PreencheConsultarContenedoresPackModularCanais(dsRetorno, oidContenedor)
                                .detEfectivo = PreencheConsultarContenedoresPackModularDetEfec(dsRetorno, oidContenedor)
                            End With
                            objRespuesta.contenedores.Add(objContenedor)
                        Next
                    End If
                End If
            End If

            Return objRespuesta
        End Function
        Private Shared Function PreencheConsultarContenedorxPosicionSectores(dsRetorno As DataSet, oidContenedor As String) As List(Of ConsultarContenedorxPosicion.SectorRespuesta)
            Dim lstSectores As List(Of ConsultarContenedorxPosicion.SectorRespuesta) = Nothing
            If dsRetorno.Tables.Contains("CLIENTES") Then
                Dim dtSectores As DataTable = dsRetorno.Tables("CLIENTES").DefaultView.ToTable(True, "OID_CONTENEDOR", "COD_DELEGACION", "COD_PLANTA", "COD_SECTOR", "COD_POSICION")
                If dtSectores.Rows.Count > 0 Then
                    lstSectores = New List(Of ConsultarContenedorxPosicion.SectorRespuesta)
                    For Each dtRow In dtSectores.Select(String.Format("OID_CONTENEDOR = '{0}'", oidContenedor))
                        Dim objSector As New ConsultarContenedorxPosicion.SectorRespuesta
                        With objSector
                            .codDelegacion = Util.AtribuirValorObj(dtRow("COD_DELEGACION"), GetType(String))
                            .codPlanta = Util.AtribuirValorObj(dtRow("COD_PLANTA"), GetType(String))
                            .codSector = Util.AtribuirValorObj(dtRow("COD_SECTOR"), GetType(String))
                            .codPosicion = Util.AtribuirValorObj(dtRow("COD_POSICION"), GetType(String))
                        End With
                        lstSectores.Add(objSector)
                    Next
                End If
            End If
            Return lstSectores
        End Function

        Private Shared Function PreencheConsultarContenedoresPackModularSector(dsRetorno As DataSet, oidContenedor As String) As ConsultarContenedoresPackModular.SectorRespuesta
            Dim sector As ConsultarContenedoresPackModular.SectorRespuesta = Nothing
            If dsRetorno.Tables.Contains("CLIENTES") Then
                Dim dtSectores As DataTable = dsRetorno.Tables("CLIENTES").DefaultView.ToTable(True, "OID_CONTENEDOR", "COD_DELEGACION", "COD_PLANTA", "OID_SECTOR", "COD_SECTOR", "DES_SECTOR", "COD_POSICION")

                If dtSectores.Rows.Count > 0 Then
                    Dim row = dtSectores.Select(String.Format("OID_CONTENEDOR = '{0}'", oidContenedor))

                    sector = New ConsultarContenedoresPackModular.SectorRespuesta
                    With sector
                        .codDelegacion = Util.AtribuirValorObj(row(0)("COD_DELEGACION"), GetType(String))
                        .codPlanta = Util.AtribuirValorObj(row(0)("COD_PLANTA"), GetType(String))
                        .codSector = Util.AtribuirValorObj(row(0)("COD_SECTOR"), GetType(String))
                        .identificadorSector = Util.AtribuirValorObj(row(0)("OID_SECTOR"), GetType(String))
                        .codPosicion = Util.AtribuirValorObj(row(0)("COD_POSICION"), GetType(String))
                        .descSector = Util.AtribuirValorObj(row(0)("DES_SECTOR"), GetType(String))
                    End With
                End If
            End If
            Return sector
        End Function

        Private Shared Function PreencheConsultarContenedoresPackModularCliente(dsRetorno As DataSet, oidContenedor As String) As ConsultarContenedoresPackModular.Cliente
            Dim cliente As ConsultarContenedoresPackModular.Cliente = Nothing
            If dsRetorno.Tables.Contains("CLIENTES") Then
                Dim dtClientes As DataTable = dsRetorno.Tables("CLIENTES").DefaultView.ToTable(True, "OID_CONTENEDOR", "COD_CLIENTE", "DES_CLIENTE", "COD_SUBCLIENTE", "COD_PTO_SERVICIO")
                If dtClientes.Rows.Count > 0 Then
                    Dim row = dtClientes.Select(String.Format("OID_CONTENEDOR = '{0}'", oidContenedor))
                    cliente = New ConsultarContenedoresPackModular.Cliente
                    With cliente
                        .codCliente = Util.AtribuirValorObj(row(0)("COD_CLIENTE"), GetType(String))
                        .codSubcliente = Util.AtribuirValorObj(row(0)("COD_SUBCLIENTE"), GetType(String))
                        .codPuntoServicio = Util.AtribuirValorObj(row(0)("COD_PTO_SERVICIO"), GetType(String))
                        .descCliente = Util.AtribuirValorObj(row(0)("DES_CLIENTE"), GetType(String))
                    End With
                End If
            End If
            Return cliente
        End Function

        Private Shared Function PreencheConsultarContenedoresPackModularCanais(dsRetorno As DataSet, oidContenedor As String) As List(Of ConsultarContenedoresPackModular.Canal)
            Dim lstCanais As List(Of ConsultarContenedoresPackModular.Canal) = Nothing
            If dsRetorno.Tables.Contains("CLIENTES") Then
                Dim dtCanais As DataTable = dsRetorno.Tables("CLIENTES").DefaultView.ToTable(True, "OID_CONTENEDOR", "COD_CANAL", "DES_CANAL", "COD_SUBCANAL", "DES_SUBCANAL")
                If dtCanais.Rows.Count > 0 Then
                    lstCanais = New List(Of ConsultarContenedoresPackModular.Canal)
                    For Each dtRow In dtCanais.Select(String.Format("OID_CONTENEDOR = '{0}'", oidContenedor))
                        Dim objCanal As New ConsultarContenedoresPackModular.Canal
                        With objCanal
                            .codCanal = Util.AtribuirValorObj(dtRow("COD_CANAL"), GetType(String))
                            .desCanal = Util.AtribuirValorObj(dtRow("DES_CANAL"), GetType(String))
                            .codSubcanal = Util.AtribuirValorObj(dtRow("COD_SUBCANAL"), GetType(String))
                            .desSubcanal = Util.AtribuirValorObj(dtRow("DES_SUBCANAL"), GetType(String))
                        End With
                        lstCanais.Add(objCanal)
                    Next
                End If
            End If
            Return lstCanais
        End Function

        Private Shared Function PreencheConsultarContenedorxPosicionDetEfec(dsRetorno As DataSet, oidContenedor As String) As List(Of ConsultarContenedorxPosicion.DetalleEfectivo)
            Dim lstDetalleEfectivo As List(Of ConsultarContenedorxPosicion.DetalleEfectivo) = Nothing
            If dsRetorno.Tables.Contains("DETEFECMP") Then
                Dim dtEfec As DataTable = dsRetorno.Tables("DETEFECMP") '.DefaultView.ToTable(True, "OID_CONTENEDOR", "COD_ISO_DIVISA", "DES_DIVISA", "COD_SIMBOLO", "COD_DENOMINACION", "COD_CALIDAD", "BOL_DISPONIBLE", "BOL_BLOQUEADO", "NEL_CANTIDAD", "NUM_IMPORTE")
                If dtEfec.Rows.Count > 0 Then
                    lstDetalleEfectivo = New List(Of ConsultarContenedorxPosicion.DetalleEfectivo)
                    For Each dtRow In dtEfec.Select(String.Format("OID_CONTENEDOR = '{0}'", oidContenedor))
                        Dim objDetalleEfectivo As New ConsultarContenedorxPosicion.DetalleEfectivo
                        With objDetalleEfectivo
                            .codDivisa = Util.AtribuirValorObj(dtRow("COD_ISO_DIVISA"), GetType(String))
                            .desDivisa = Util.AtribuirValorObj(dtRow("DES_DIVISA"), GetType(String))
                            .codSimbolo = Util.AtribuirValorObj(dtRow("COD_SIMBOLO"), GetType(String))
                            .codDenominacion = Util.AtribuirValorObj(dtRow("COD_DENOMINACION"), GetType(String))
                            .desDenonimacion = Util.AtribuirValorObj(dtRow("DES_DENOMINACION"), GetType(String))
                            .codCalidad = Util.AtribuirValorObj(dtRow("COD_CALIDAD"), GetType(String))
                            .disponible = Util.AtribuirValorObj(dtRow("BOL_DISPONIBLE"), GetType(Boolean))
                            .bloqueado = Util.AtribuirValorObj(dtRow("BOL_BLOQUEADO"), GetType(Boolean))
                            .cantidad = Util.AtribuirValorObj(dtRow("NEL_CANTIDAD"), GetType(Integer))
                            .importe = Util.AtribuirValorObj(dtRow("NUM_IMPORTE"), GetType(Decimal))
                        End With
                        lstDetalleEfectivo.Add(objDetalleEfectivo)
                    Next
                End If
            End If
            Return lstDetalleEfectivo
        End Function

        Private Shared Function PreencheConsultarContenedoresPackModularDetEfec(dsRetorno As DataSet, oidContenedor As String) As List(Of ConsultarContenedoresPackModular.DetalleEfectivo)
            Dim lstDetalleEfectivo As List(Of ConsultarContenedoresPackModular.DetalleEfectivo) = Nothing
            If dsRetorno.Tables.Contains("DETEFEC") Then
                Dim dtEfec As DataTable = dsRetorno.Tables("DETEFEC").DefaultView.ToTable(True, "OID_CONTENEDOR", "COD_ISO_DIVISA", "DES_DIVISA", "COD_SIMBOLO", "COD_DENOMINACION", "COD_CALIDAD", "BOL_DISPONIBLE", "BOL_BLOQUEADO", "NEL_CANTIDAD", "NUM_IMPORTE")
                If dtEfec.Rows.Count > 0 Then
                    lstDetalleEfectivo = New List(Of ConsultarContenedoresPackModular.DetalleEfectivo)
                    For Each dtRow In dtEfec.Select(String.Format("OID_CONTENEDOR = '{0}'", oidContenedor))
                        Dim objDetalleEfectivo As New ConsultarContenedoresPackModular.DetalleEfectivo
                        With objDetalleEfectivo
                            .codDivisa = Util.AtribuirValorObj(dtRow("COD_ISO_DIVISA"), GetType(String))
                            .desDivisa = Util.AtribuirValorObj(dtRow("DES_DIVISA"), GetType(String))
                            .codSimbolo = Util.AtribuirValorObj(dtRow("COD_SIMBOLO"), GetType(String))
                            .codDenominacion = Util.AtribuirValorObj(dtRow("COD_DENOMINACION"), GetType(String))
                            .codCalidad = Util.AtribuirValorObj(dtRow("COD_CALIDAD"), GetType(String))
                            .disponible = Util.AtribuirValorObj(dtRow("BOL_DISPONIBLE"), GetType(Boolean))
                            .bloqueado = Util.AtribuirValorObj(dtRow("BOL_BLOQUEADO"), GetType(Boolean))
                            .cantidad = Util.AtribuirValorObj(dtRow("NEL_CANTIDAD"), GetType(Integer))
                            .importe = Util.AtribuirValorObj(dtRow("NUM_IMPORTE"), GetType(Decimal))
                        End With
                        lstDetalleEfectivo.Add(objDetalleEfectivo)
                    Next
                End If
            End If
            Return lstDetalleEfectivo
        End Function

        Private Shared Function PreencheConsultarContenedoresPackModularAlertas(dsRetorno As DataSet, oidContenedor As String) As List(Of ConsultarContenedoresPackModular.AlertaVencimento)
            Dim lstAlertas As List(Of ConsultarContenedoresPackModular.AlertaVencimento) = Nothing
            If dsRetorno.Tables.Contains("ALERTAS") Then
                Dim dtCanais As DataTable = dsRetorno.Tables("ALERTAS").DefaultView.ToTable(True, "OID_CONTENEDOR", "GMT_CREACION", "COD_TIPO_ALERTA", "DIAS_ALERTA", "NEC_DIAS_VENCER", "DES_EMAILS")
                If dtCanais.Rows.Count > 0 Then
                    lstAlertas = New List(Of ConsultarContenedoresPackModular.AlertaVencimento)
                    For Each dtRow In dtCanais.Select(String.Format("OID_CONTENEDOR = '{0}'", oidContenedor))
                        Dim objAlerta As New ConsultarContenedoresPackModular.AlertaVencimento
                        With objAlerta
                            .fechaAlerta = Util.AtribuirValorObj(dtRow("GMT_CREACION"), GetType(DateTime))
                            .codTipoAlerta = Util.AtribuirValorObj(dtRow("COD_TIPO_ALERTA"), GetType(String))
                            .diasAlerta = Util.AtribuirValorObj(dtRow("DIAS_ALERTA"), GetType(Integer))
                            .diasVencer = Util.AtribuirValorObj(dtRow("NEC_DIAS_VENCER"), GetType(Integer))
                            .emails = Util.AtribuirValorObj(dtRow("DES_EMAILS"), GetType(String))
                        End With
                        lstAlertas.Add(objAlerta)
                    Next
                End If
            End If
            Return lstAlertas
        End Function
#End Region

#Region "ConsultarDocumentosGestionContenedores"

        Public Shared Function ConsultarDocumentosGestionContenedores(Peticion As ConsultarDocumentosGestionContenedores.Peticion) As ConsultarDocumentosGestionContenedores.Respuesta

            Dim objRespuesta As New ConsultarDocumentosGestionContenedores.Respuesta

            Try

                Dim dsRetorno As DataSet = AccesoDatos.GenesisSaldos.Contenedor.ConsultarDocumentosGestionContenedores(Peticion)

                objRespuesta = PreencheConsultarDocumentosGestionContenedores(dsRetorno)

            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.Mensajes.Add(ex.Descricao)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.Excepciones.Add(ex.ToString)
            End Try

            Return objRespuesta
        End Function

        Private Shared Function PreencheConsultarDocumentosGestionContenedores(dsRetorno As DataSet) As ConsultarDocumentosGestionContenedores.Respuesta
            Dim objRespuesta As New ConsultarDocumentosGestionContenedores.Respuesta
            If dsRetorno IsNot Nothing AndAlso dsRetorno.Tables.Count > 0 Then
                If dsRetorno.Tables.Contains("DOCUMENTOS") Then
                    Dim dtDocumentos = dsRetorno.Tables("DOCUMENTOS") '.DefaultView.ToTable(True, "OID_DOCUMENTO", "OID_CONTENEDOR", "COD_COMPROBANTE", "COD_EXTERNO", "COD_ESTADO", "COD_SECTOR_ORIGEN", "DES_SECTOR_ORIGEN", "COD_PLANTA_ORIGEN", "COD_DELEGACION_ORIGEN", "COD_SECTOR_DESTINO", "DES_SECTOR_DESTINO", "COD_PLANTA_DESTINO", "COD_DELEGACION_DESTINO", "COD_TIPO_CONTENEDOR", "COD_ESTADO_CONTENEDOR", "GMT_CREACION", "GMT_MODIFICACION")
                    If dtDocumentos IsNot Nothing AndAlso dtDocumentos.Rows.Count > 0 Then
                        If objRespuesta.documentos Is Nothing Then
                            objRespuesta.documentos = New List(Of ContractoServicio.GenesisSaldos.Contenedores.Comon.Documento)
                        End If

                        Dim objDocumento As ContractoServicio.GenesisSaldos.Contenedores.Comon.Documento = Nothing
                        Dim IdentificadorDocumento As String = String.Empty

                        For Each dtRow In dtDocumentos.Rows

                            Dim oidContenedor As String = Util.AtribuirValorObj(dtRow("OID_CONTENEDOR"), GetType(String))
                            IdentificadorDocumento = Util.AtribuirValorObj(dtRow("OID_DOCUMENTO"), GetType(String))

                            objDocumento = (From d In objRespuesta.documentos Where d.Identificador = IdentificadorDocumento).FirstOrDefault

                            If objDocumento Is Nothing Then

                                objRespuesta.documentos.Add(New ContractoServicio.GenesisSaldos.Contenedores.Comon.Documento With { _
                                                               .Rowver = Util.AtribuirValorObj(dtRow("ROWVER"), GetType(Integer)),
                                .CodComprobante = Util.AtribuirValorObj(dtRow("COD_COMPROBANTE"), GetType(String)),
                                .Identificador = Util.AtribuirValorObj(dtRow("OID_DOCUMENTO"), GetType(String)),
                                .CodEstadoDocumento = Util.AtribuirValorObj(dtRow("COD_ESTADO"), GetType(String)),
                                .CodExterno = Util.AtribuirValorObj(dtRow("COD_EXTERNO"), GetType(String)),
                                .FechaCreacion = Util.AtribuirValorObj(dtRow("GMT_CREACION"), GetType(DateTime)),
                                .FechaModificacion = Util.AtribuirValorObj(dtRow("GMT_MODIFICACION"), GetType(DateTime)),
                                .EsGrupo = Util.AtribuirValorObj(dtRow("ES_GRUPO"), GetType(Boolean))})

                                objDocumento = (From d In objRespuesta.documentos Where d.Identificador = IdentificadorDocumento).FirstOrDefault

                                With objDocumento
                                    .SectorOrigen = New Comon.Clases.Sector
                                    .SectorOrigen.Codigo = Util.AtribuirValorObj(dtRow("COD_SECTOR_ORIGEN"), GetType(String))
                                    .SectorOrigen.Descripcion = Util.AtribuirValorObj(dtRow("DES_SECTOR_ORIGEN"), GetType(String))
                                    .SectorOrigen.Planta = New Comon.Clases.Planta
                                    .SectorOrigen.Planta.Codigo = Util.AtribuirValorObj(dtRow("COD_PLANTA_ORIGEN"), GetType(String))
                                    .SectorOrigen.Delegacion = New Comon.Clases.Delegacion
                                    .SectorOrigen.Delegacion.Codigo = Util.AtribuirValorObj(dtRow("COD_DELEGACION_ORIGEN"), GetType(String))
                                    .SectorDestino = New Comon.Clases.Sector
                                    .SectorDestino.Codigo = Util.AtribuirValorObj(dtRow("COD_SECTOR_DESTINO"), GetType(String))
                                    .SectorDestino.Descripcion = Util.AtribuirValorObj(dtRow("DES_SECTOR_DESTINO"), GetType(String))
                                    .SectorDestino.Planta = New Comon.Clases.Planta
                                    .SectorDestino.Planta.Codigo = Util.AtribuirValorObj(dtRow("COD_PLANTA_DESTINO"), GetType(String))
                                    .SectorDestino.Delegacion = New Comon.Clases.Delegacion
                                    .SectorDestino.Delegacion.Codigo = Util.AtribuirValorObj(dtRow("COD_DELEGACION_DESTINO"), GetType(String))
                                    .Contenedores = New ObservableCollection(Of ContractoServicio.GenesisSaldos.Contenedores.Comon.Contenedor)
                                End With

                            End If

                            Dim objCuentaOrigen As New Comon.Clases.Cuenta
                            objCuentaOrigen.Cliente = New Comon.Clases.Cliente
                            objCuentaOrigen.Cliente.Codigo = Util.AtribuirValorObj(dtRow("COD_CLIENTE"), GetType(String))
                            objCuentaOrigen.Cliente.Descripcion = Util.AtribuirValorObj(dtRow("DES_CLIENTE"), GetType(String))
                            objCuentaOrigen.SubCliente = New Comon.Clases.SubCliente
                            objCuentaOrigen.SubCliente.Codigo = Util.AtribuirValorObj(dtRow("COD_SUBCLIENTE"), GetType(String))
                            objCuentaOrigen.SubCliente.Descripcion = Util.AtribuirValorObj(dtRow("DES_SUBCLIENTE"), GetType(String))
                            objCuentaOrigen.PuntoServicio = New Comon.Clases.PuntoServicio
                            objCuentaOrigen.PuntoServicio.Codigo = Util.AtribuirValorObj(dtRow("COD_PTO_SERVICIO"), GetType(String))
                            objCuentaOrigen.PuntoServicio.Descripcion = Util.AtribuirValorObj(dtRow("DES_PTO_SERVICIO"), GetType(String))
                            objCuentaOrigen.Canal = New Comon.Clases.Canal
                            objCuentaOrigen.Canal.Codigo = Util.AtribuirValorObj(dtRow("COD_CANAL"), GetType(String))
                            objCuentaOrigen.Canal.Descripcion = Util.AtribuirValorObj(dtRow("DES_CANAL"), GetType(String))
                            objCuentaOrigen.SubCanal = New Comon.Clases.SubCanal
                            objCuentaOrigen.SubCanal.Codigo = Util.AtribuirValorObj(dtRow("COD_SUBCANAL"), GetType(String))
                            objCuentaOrigen.SubCanal.Descripcion = Util.AtribuirValorObj(dtRow("DES_SUBCANAL"), GetType(String))

                            PreencheConsultarDocumentosGestionContenedoresDetEfectivoMedioPagoDocumento(dsRetorno, objDocumento)

                            objDocumento.Contenedores.Add(New ContractoServicio.GenesisSaldos.Contenedores.Comon.Contenedor With { _
                              .CodTipoContenedor = Util.AtribuirValorObj(dtRow("COD_TIPO_CONTENEDOR"), GetType(String)),
                              .CodEstadoContenedor = Util.AtribuirValorObj(dtRow("COD_ESTADO_CONTENEDOR"), GetType(String)),
                              .Precintos = PreenchePrecintos(dsRetorno, oidContenedor),
                              .CuentaContenedor = New Comon.Clases.CuentasContenedor With {.Cuenta = objCuentaOrigen,
                                                                                           .Divisas = objDocumento.Divisas}})

                            objDocumento.Cuenta = objCuentaOrigen

                            'PreencheConsultarDocumentosGestionContenedoresCuentaDocumento(dtRow, objDocumento)

                        Next

                    End If
                End If
            End If

            Return objRespuesta
        End Function

        Private Shared Function PreencheConsultarDocumentosGestionContenedoresCuentas(dsRetorno As DataSet, oidContenedor As String) As Comon.Clases.CuentasContenedor

            Dim cuentaContenedor As Comon.Clases.CuentasContenedor = Nothing
            If dsRetorno.Tables.Contains("DETEFECMP") Then
                Dim dtCuentas As DataTable = dsRetorno.Tables("DETEFECMP").DefaultView.ToTable(True, "OID_CONTENEDOR", "COD_ISO_DIVISA", "COD_DENOMINACION", "COD_CALIDAD", "COD_TIPO_MEDIO_PAGO", "COD_MEDIO_PAGO", "DES_MEDIO_PAGO", "BOL_DISPONIBLE", "BOL_BLOQUEADO", "NEL_CANTIDAD", "NUM_IMPORTE", "COD_CLIENTE", "DES_CLIENTE", "COD_SUBCLIENTE", "DES_SUBCLIENTE", "COD_PTO_SERVICIO", "DES_PTO_SERVICIO", "COD_CANAL", "DES_CANAL", "COD_SUBCANAL", "DES_SUBCANAL")
                If dtCuentas.Rows.Count > 0 Then
                    cuentaContenedor = New Comon.Clases.CuentasContenedor

                    Dim codCliente As String = Util.AtribuirValorObj(dtCuentas(0)("COD_CLIENTE"), GetType(String))
                    Dim codSubCliente As String = Util.AtribuirValorObj(dtCuentas(0)("COD_SUBCLIENTE"), GetType(String))
                    Dim codPtoServicio As String = Util.AtribuirValorObj(dtCuentas(0)("COD_PTO_SERVICIO"), GetType(String))
                    Dim codCanal As String = Util.AtribuirValorObj(dtCuentas(0)("COD_CANAL"), GetType(String))
                    Dim codSubCanal As String = Util.AtribuirValorObj(dtCuentas(0)("COD_SUBCANAL"), GetType(String))

                    Dim objCuenta As New Comon.Clases.Cuenta
                    objCuenta.Cliente = New Comon.Clases.Cliente
                    objCuenta.Cliente.Codigo = codCliente
                    objCuenta.Cliente.Descripcion = Util.AtribuirValorObj(dtCuentas(0)("DES_CLIENTE"), GetType(String))
                    objCuenta.SubCliente = New Comon.Clases.SubCliente
                    objCuenta.SubCliente.Codigo = codSubCliente
                    objCuenta.SubCliente.Descripcion = Util.AtribuirValorObj(dtCuentas(0)("DES_SUBCLIENTE"), GetType(String))
                    objCuenta.PuntoServicio = New Comon.Clases.PuntoServicio
                    objCuenta.PuntoServicio.Codigo = codPtoServicio
                    objCuenta.PuntoServicio.Descripcion = Util.AtribuirValorObj(dtCuentas(0)("DES_PTO_SERVICIO"), GetType(String))
                    objCuenta.Canal = New Comon.Clases.Canal
                    objCuenta.Canal.Codigo = codCanal
                    objCuenta.Canal.Descripcion = Util.AtribuirValorObj(dtCuentas(0)("DES_CANAL"), GetType(String))
                    objCuenta.SubCanal = New Comon.Clases.SubCanal
                    objCuenta.SubCanal.Codigo = codSubCanal
                    objCuenta.SubCanal.Descripcion = Util.AtribuirValorObj(dtCuentas(0)("DES_SUBCANAL"), GetType(String))
                    cuentaContenedor.Cuenta = objCuenta
                    cuentaContenedor.Divisas = New ObservableCollection(Of Comon.Clases.Divisa)

                    For Each dtRow In dtCuentas.Select(String.Format("OID_CONTENEDOR = '{0}'", oidContenedor))

                        CargarDivisas(dtRow, cuentaContenedor.Divisas)

                    Next

                End If

            End If

            Return cuentaContenedor

        End Function

        Private Shared Sub PreencheConsultarDocumentosGestionContenedoresDetEfectivoMedioPagoDocumento(dsRetorno As DataSet,
                                                                                                       ByRef objDocumento As ContractoServicio.GenesisSaldos.Contenedores.Comon.Documento)
            If dsRetorno.Tables.Contains("DETEFECMPDOC") Then

                Dim dtEfecsMps As DataTable = dsRetorno.Tables("DETEFECMPDOC").DefaultView.ToTable(True, "OID_DOCUMENTO", "COD_ISO_DIVISA", "COD_DENOMINACION", "COD_CALIDAD", "COD_TIPO_MEDIO_PAGO", "COD_MEDIO_PAGO", "DES_MEDIO_PAGO", "NEL_CANTIDAD", "NUM_IMPORTE")

                If dtEfecsMps.Rows.Count > 0 Then

                    For Each dtRow As DataRow In dtEfecsMps.Select(String.Format("OID_DOCUMENTO = '{0}'", objDocumento.Identificador))

                        CargarDivisas(dtRow, objDocumento.Divisas)

                    Next

                End If

            End If

        End Sub

        Private Shared Sub PreencheConsultarDocumentosGestionContenedoresCuentaDocumento(dtRow As DataRow, ByRef objDocumento As ContractoServicio.GenesisSaldos.Contenedores.Comon.Documento)

            If Not DBNull.Value.Equals(dtRow("COD_CLIENTE")) Then

                For Each Contenedor As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.Comon.Contenedor In objDocumento.Contenedores
                    If Contenedor.CuentaContenedor Is Nothing Then
                        Contenedor.CuentaContenedor = New Clases.CuentasContenedor
                    End If
                    Contenedor.CuentaContenedor.Cuenta = objDocumento.Cuenta
                Next
            End If

        End Sub

        Private Shared Sub CargarDivisas(dtRow As DataRow, ByRef divisas As ObservableCollection(Of Comon.Clases.Divisa))

            Dim codigoIsoDivisa As String = Util.AtribuirValorObj(dtRow("COD_ISO_DIVISA"), GetType(String))
            Dim codigoDenominacion As String = Util.AtribuirValorObj(dtRow("COD_DENOMINACION"), GetType(String))
            Dim codigoTipoMedioPago As String = Util.AtribuirValorObj(dtRow("COD_TIPO_MEDIO_PAGO"), GetType(String))
            Dim codigoMedioPago As String = Util.AtribuirValorObj(dtRow("COD_MEDIO_PAGO"), GetType(String))
            Dim codigoCalidad As String = Util.AtribuirValorObj(dtRow("COD_CALIDAD"), GetType(String))

            If divisas Is Nothing Then
                divisas = New ObservableCollection(Of Comon.Clases.Divisa)
            End If

            Dim objdivisa As Comon.Clases.Divisa = Nothing

            Dim objDenominacion As Comon.Clases.Denominacion = Nothing
            Dim objValorDenominacion As Comon.Clases.ValorDenominacion = Nothing

            Dim objMedioPago As Comon.Clases.MedioPago = Nothing
            Dim objValorTipoMedioPago As Comon.Clases.ValorTipoMedioPago = Nothing

            ' recupera ou cria nova divisa
            If divisas.Exists(Function(e) e.CodigoISO = codigoIsoDivisa) Then
                objdivisa = divisas.FirstOrDefault(Function(e) e.CodigoISO = codigoIsoDivisa)

            Else
                objdivisa = New Comon.Clases.Divisa With
                            {
                                .CodigoISO = codigoIsoDivisa
                            }
                divisas.Add(objdivisa)

            End If

            ' Verifica se itemLoop é Efetivo ou Meio Pagamento
            ' EFETIVO
            If String.IsNullOrEmpty(codigoTipoMedioPago) Then

                ' Verifica se valor é: TOTAL / DETALHADO
                ' -------------------------------------------- TOTAL --------------------------------------------
                If String.IsNullOrEmpty(codigoDenominacion) Then

                    If objdivisa.ValoresTotalesEfectivo IsNot Nothing AndAlso objdivisa.ValoresTotalesEfectivo.Count > 0 Then
                        objdivisa.ValoresTotalesEfectivo.FirstOrDefault.Importe += Util.AtribuirValorObj(dtRow("NUM_IMPORTE"), GetType(Decimal))

                    Else
                        objdivisa.ValoresTotalesEfectivo = New ObservableCollection(Of Comon.Clases.ValorEfectivo) From
                                {
                                    New Comon.Clases.ValorEfectivo With
                                        {
                                            .Importe = Util.AtribuirValorObj(dtRow("NUM_IMPORTE"), GetType(Decimal)),
                                            .TipoValor = Comon.Enumeradores.TipoValor.Declarado
                                        }
                                }
                    End If

                Else ' -------------------------------------------- DETALHADO --------------------------------------------
                    If objdivisa.Denominaciones Is Nothing Then
                        objdivisa.Denominaciones = New ObservableCollection(Of Comon.Clases.Denominacion)
                    End If

                    If objdivisa.Denominaciones.Exists(Function(e) e.Codigo = codigoDenominacion) Then
                        objDenominacion = objdivisa.Denominaciones.FirstOrDefault(Function(e) e.Codigo = codigoDenominacion)

                    Else
                        objDenominacion = New Comon.Clases.Denominacion With
                                          {
                                              .Codigo = codigoDenominacion
                                          }
                        objdivisa.Denominaciones.Add(objDenominacion)
                    End If

                    If objDenominacion.ValorDenominacion Is Nothing Then
                        objDenominacion.ValorDenominacion = New ObservableCollection(Of Comon.Clases.ValorDenominacion)
                    End If

                    If String.IsNullOrEmpty(codigoCalidad) Then

                        If objDenominacion.ValorDenominacion.Exists(Function(e) e.Calidad Is Nothing OrElse String.IsNullOrWhiteSpace(e.Calidad.Codigo)) Then
                            objValorDenominacion = objDenominacion.ValorDenominacion.FirstOrDefault(Function(e) e.Calidad Is Nothing OrElse String.IsNullOrWhiteSpace(e.Calidad.Codigo))
                        End If

                    Else
                        If objDenominacion.ValorDenominacion.Exists(Function(e) e.Calidad IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(e.Calidad.Codigo) AndAlso
                                                                                e.Calidad.Codigo.Trim.ToUpper = codigoCalidad.Trim.ToUpper) Then
                            objValorDenominacion = objDenominacion.ValorDenominacion.FirstOrDefault(Function(e) e.Calidad IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(e.Calidad.Codigo) AndAlso
                                                                                                                e.Calidad.Codigo.Trim.ToUpper = codigoCalidad.Trim.ToUpper)
                        End If

                    End If

                    If objValorDenominacion IsNot Nothing Then

                        objValorDenominacion.Importe += Util.AtribuirValorObj(dtRow("NUM_IMPORTE"), GetType(Decimal))
                        objValorDenominacion.Cantidad += Util.AtribuirValorObj(dtRow("NEL_CANTIDAD"), GetType(Integer))

                    Else

                        objValorDenominacion = New Comon.Clases.ValorDenominacion With
                                                   {
                                                       .Importe = Util.AtribuirValorObj(dtRow("NUM_IMPORTE"), GetType(Decimal)),
                                                       .Cantidad = Util.AtribuirValorObj(dtRow("NEL_CANTIDAD"), GetType(Integer)),
                                                       .TipoValor = Comon.Enumeradores.TipoValor.Declarado,
                                                       .Calidad = If(String.IsNullOrEmpty(codigoCalidad), Nothing, New Comon.Clases.Calidad With {.Codigo = codigoCalidad})
                                                   }
                        objDenominacion.ValorDenominacion.Add(objValorDenominacion)


                    End If

                End If

            Else ' MEDIO PAGO

                ' Verifica se valor é: TOTAL / DETALHADO
                ' -------------------------------------------- TOTAL --------------------------------------------
                If String.IsNullOrEmpty(codigoMedioPago) Then

                    If objdivisa.ValoresTotalesTipoMedioPago Is Nothing Then
                        objdivisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Comon.Clases.ValorTipoMedioPago)
                    End If

                    If objdivisa.ValoresTotalesTipoMedioPago.Exists(Function(e) e.TipoMedioPago = codigoTipoMedioPago) Then

                        objValorTipoMedioPago = objdivisa.ValoresTotalesTipoMedioPago.FirstOrDefault(Function(e) e.TipoMedioPago = codigoTipoMedioPago)

                        objValorTipoMedioPago.Importe += Util.AtribuirValorObj(dtRow("NUM_IMPORTE"), GetType(Decimal))
                        objValorTipoMedioPago.Cantidad += Util.AtribuirValorObj(dtRow("NEL_CANTIDAD"), GetType(Integer))

                    Else
                        ' TODO - ver necessidade de recuperar TIPOVALOR
                        objdivisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Comon.Clases.ValorTipoMedioPago) From
                                {
                                    New Comon.Clases.ValorTipoMedioPago With
                                        {
                                            .Importe = Util.AtribuirValorObj(dtRow("NUM_IMPORTE"), GetType(Decimal)),
                                            .Cantidad = Util.AtribuirValorObj(dtRow("NEL_CANTIDAD"), GetType(Integer)),
                                            .TipoValor = Comon.Enumeradores.TipoValor.Declarado,
                                            .TipoMedioPago = codigoTipoMedioPago
                                        }
                                }
                    End If

                Else ' -------------------------------------------- DETALHADO --------------------------------------------

                    If objdivisa.MediosPago Is Nothing Then
                        objdivisa.MediosPago = New ObservableCollection(Of Comon.Clases.MedioPago)
                    End If

                    If objdivisa.MediosPago.Exists(Function(e) e.Codigo = codigoMedioPago) Then
                        objMedioPago = objdivisa.MediosPago.FirstOrDefault(Function(e) e.Codigo = codigoMedioPago)

                    Else

                        objMedioPago = New Comon.Clases.MedioPago With
                                       {
                                           .Codigo = codigoMedioPago,
                                           .Descripcion = Util.AtribuirValorObj(dtRow("DES_MEDIO_PAGO"), GetType(String)),
                                           .Tipo = RecuperarEnum(Of Comon.Enumeradores.TipoMedioPago)(codigoTipoMedioPago)
                                       }
                        objdivisa.MediosPago.Add(objMedioPago)
                    End If

                    If objMedioPago.Valores Is Nothing Then
                        objMedioPago.Valores = New ObservableCollection(Of Comon.Clases.ValorMedioPago)
                    End If

                    If objMedioPago.Valores.Count > 0 Then
                        objMedioPago.Valores.FirstOrDefault.Importe += Util.AtribuirValorObj(dtRow("NUM_IMPORTE"), GetType(Decimal))
                        objMedioPago.Valores.FirstOrDefault.Cantidad += Util.AtribuirValorObj(dtRow("NEL_CANTIDAD"), GetType(Decimal))

                    Else
                        Dim objvalorMedioPago As New Comon.Clases.ValorMedioPago With
                            {
                                .Importe = Util.AtribuirValorObj(dtRow("NUM_IMPORTE"), GetType(Decimal)),
                                .Cantidad = Util.AtribuirValorObj(dtRow("NEL_CANTIDAD"), GetType(Decimal)),
                                .TipoValor = Comon.Enumeradores.TipoValor.Declarado
                            }
                    End If
                End If

            End If
        End Sub

#End Region

#Region "ConsultarContenedoresPorFIFO"

        Public Shared Function ConsultarContenedoresPorFIFO(Peticion As ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedorFIFO.Peticion) As ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedorFIFO.Respuesta

            Dim respuesta As New ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedorFIFO.Respuesta

            Try

                ValidarCamposObligatoriosFIFO(Peticion)
                Dim ds As DataSet = AccesoDatos.GenesisSaldos.Contenedor.ConsultarContenedoresPorFIFO(Peticion.Contenedor.CodTipoContenedor,
                                                                                                      Peticion.Contenedor.CuentaContenedor.Cuenta.Sector.Delegacion.Identificador,
                                                                                                      Peticion.Contenedor.CuentaContenedor.Cuenta.Sector.Planta.Identificador,
                                                                                                      Peticion.Contenedor.CuentaContenedor.Cuenta.Sector.Identificador,
                                                                                                      Peticion.Contenedor.CuentaContenedor.Cuenta.Cliente.Identificador,
                                                                                                      If(Peticion.Contenedor.CuentaContenedor.Cuenta.SubCliente Is Nothing, String.Empty, Peticion.Contenedor.CuentaContenedor.Cuenta.SubCliente.Identificador),
                                                                                                      If(Peticion.Contenedor.CuentaContenedor.Cuenta.PuntoServicio Is Nothing, String.Empty, Peticion.Contenedor.CuentaContenedor.Cuenta.PuntoServicio.Identificador),
                                                                                                      Peticion.Contenedor.CuentaContenedor.Cuenta.Canal.Identificador,
                                                                                                      Peticion.Contenedor.CuentaContenedor.Cuenta.SubCanal.Identificador,
                                                                                                      Peticion.Contenedor.ValoresContenedor.First.IdentificadorDivisa,
                                                                                                      Peticion.Contenedor.ValoresContenedor.First.IdentificadorDenominacion,
                                                                                                      Peticion.Contenedor.ValoresContenedor.First.Importe,
                                                                                                      Peticion.Contenedor.CuentaContenedor.Cuenta.Canal.Codigo,
                                                                                                      Peticion.Contenedor.CuentaContenedor.Cuenta.SubCanal.Codigo, Peticion.CodigoUsuario)

                If ds Is Nothing OrElse ds.Tables.Count = 0 Then
                    Throw New Excepcion.NegocioExcepcion(Traduzir("FIFO_sin_contenedores"))
                End If

                respuesta = CargarValoresRecuperadosFIFO(ds, Peticion)

                If respuesta.TotalImporte < Peticion.Contenedor.ValoresContenedor.First.Importe Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("FIFO_importe_menor"), "$", respuesta.TotalImporte))
                End If

            Catch ex As Excepcion.NegocioExcepcion

                If ex.InnerException IsNot Nothing AndAlso ex.InnerException.InnerException IsNot Nothing AndAlso Not String.IsNullOrEmpty(ex.InnerException.InnerException.Message) Then
                    Try
                        Dim i As Integer = ex.InnerException.InnerException.Message.IndexOf("#")
                        Dim mensaje As String = ex.InnerException.InnerException.Message.Substring(i + 1, ex.InnerException.InnerException.Message.IndexOf("#", i + 1) - i - 1)
                        respuesta.Mensajes.Add(mensaje)
                    Catch ex1 As Exception
                        Util.TratarErroBugsnag(ex)
                        respuesta.Mensajes.Add(ex.InnerException.InnerException.Message)
                    End Try
                Else
                    respuesta.Mensajes.Add(ex.Message)
                End If

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                If ex.InnerException IsNot Nothing AndAlso ex.InnerException.InnerException IsNot Nothing AndAlso Not String.IsNullOrEmpty(ex.InnerException.InnerException.Message) Then
                    Try
                        Dim i As Integer = ex.InnerException.InnerException.Message.IndexOf("#")
                        Dim mensaje As String = ex.InnerException.InnerException.Message.Substring(i + 1, ex.InnerException.InnerException.Message.IndexOf("#", i + 1) - i - 1)
                        respuesta.Excepciones.Add(mensaje)
                    Catch ex1 As Exception
                        Util.TratarErroBugsnag(ex)
                        respuesta.Excepciones.Add(ex.InnerException.InnerException.Message)
                    End Try
                Else
                    respuesta.Excepciones.Add(ex.Message)
                End If

            End Try

            Return respuesta

        End Function

        Private Shared Sub ValidarCamposObligatoriosFIFO(Peticion As ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedorFIFO.Peticion)

            If Peticion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("gen_srv_msg_atributo"), "Petición"))
            End If

            Dim _mensajes As New List(Of String)

            With Peticion

                If .Contenedor Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("gen_srv_msg_atributo"), "Contenedor"))
                End If

                If .Contenedor.CuentaContenedor Is Nothing OrElse .Contenedor.CuentaContenedor.Cuenta Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("gen_srv_msg_atributo"), "Cuenta"))
                End If

                If .Contenedor.CuentaContenedor.Cuenta.Cliente Is Nothing OrElse String.IsNullOrEmpty(.Contenedor.CuentaContenedor.Cuenta.Cliente.Identificador) Then
                    _mensajes.Add(String.Format(Traduzir("gen_srv_msg_atributo"), "Cliente"))
                End If

                If .Contenedor.CuentaContenedor.Cuenta.Canal Is Nothing OrElse (String.IsNullOrEmpty(.Contenedor.CuentaContenedor.Cuenta.Canal.Identificador) AndAlso String.IsNullOrEmpty(.Contenedor.CuentaContenedor.Cuenta.Canal.Codigo)) Then
                    _mensajes.Add(String.Format(Traduzir("gen_srv_msg_atributo"), "Canal"))
                End If

                If .Contenedor.CuentaContenedor.Cuenta.SubCanal Is Nothing OrElse (String.IsNullOrEmpty(.Contenedor.CuentaContenedor.Cuenta.SubCanal.Identificador) AndAlso String.IsNullOrEmpty(.Contenedor.CuentaContenedor.Cuenta.SubCanal.Codigo)) Then
                    _mensajes.Add(String.Format(Traduzir("gen_srv_msg_atributo"), "SubCanal"))
                End If


                If .Contenedor.CuentaContenedor.Cuenta.Sector.Delegacion Is Nothing OrElse String.IsNullOrEmpty(.Contenedor.CuentaContenedor.Cuenta.Sector.Delegacion.Identificador) Then
                    _mensajes.Add(String.Format(Traduzir("gen_srv_msg_atributo"), "Delegación"))
                End If

                If .Contenedor.CuentaContenedor.Cuenta.Sector.Planta Is Nothing OrElse String.IsNullOrEmpty(.Contenedor.CuentaContenedor.Cuenta.Sector.Planta.Identificador) Then
                    _mensajes.Add(String.Format(Traduzir("gen_srv_msg_atributo"), "Planta"))
                End If

                If .Contenedor.CuentaContenedor.Cuenta.Sector Is Nothing OrElse String.IsNullOrEmpty(.Contenedor.CuentaContenedor.Cuenta.Sector.Identificador) Then
                    _mensajes.Add(String.Format(Traduzir("gen_srv_msg_atributo"), "Sector"))
                End If

                If .Contenedor.ValoresContenedor Is Nothing OrElse String.IsNullOrEmpty(.Contenedor.ValoresContenedor.First.IdentificadorDivisa) Then
                    _mensajes.Add(String.Format(Traduzir("gen_srv_msg_atributo"), "Divisa"))
                End If

                If .Contenedor.ValoresContenedor Is Nothing OrElse .Contenedor.ValoresContenedor.First.Importe <= 0.0 Then
                    _mensajes.Add(String.Format(Traduzir("gen_srv_msg_atributo"), "Importe"))
                End If

            End With

            If _mensajes.Count > 0 Then
                Throw New Excepcion.NegocioExcepcion(Join(_mensajes.ToArray, vbNewLine))
            End If

        End Sub

        Private Shared Function CargarValoresRecuperadosFIFO(ds As DataSet, Peticion As ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedorFIFO.Peticion) As ConsultarContenedorFIFO.Respuesta
            Dim objRespuesta As New ConsultarContenedorFIFO.Respuesta

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                If ds.Tables.Contains(Comon.Constantes.CONST_RECUPERAR_CONTENEDORES_FIFO_TABLA_RC_CONTENEDORES) AndAlso ds.Tables(Comon.Constantes.CONST_RECUPERAR_CONTENEDORES_FIFO_TABLA_RC_CONTENEDORES).Rows.Count > 0 Then

                    Dim dtContenedores = ds.Tables(Comon.Constantes.CONST_RECUPERAR_CONTENEDORES_FIFO_TABLA_RC_CONTENEDORES). _
                                                        DefaultView.ToTable(True, "OID_CONTENEDOR", "GMT_CREACION", "OID_TIPO_CONTENEDOR", "COD_TIPO_CONTENEDOR",
                                                                                  "DES_TIPO_CONTENEDOR", "COD_POSICION", "OID_DIVISA", "OID_DENOMINACION", "OID_CALIDAD", "BOL_DISPONIBLE",
                                                                                  "BOL_BLOQUEADO", "NEL_CANTIDAD", "NUM_IMPORTE")

                    If dtContenedores IsNot Nothing AndAlso dtContenedores.Rows.Count > 0 Then

                        If objRespuesta.Contenedores Is Nothing Then
                            objRespuesta.Contenedores = New List(Of ContractoServicio.GenesisSaldos.Contenedores.Comon.Contenedor)
                        End If

                        For Each row In dtContenedores.Rows

                            Dim identificadorContenedor As String = Util.AtribuirValorObj(row("OID_CONTENEDOR"), GetType(String))

                            Dim contenedor As ContractoServicio.GenesisSaldos.Contenedores.Comon.Contenedor = Nothing

                            If objRespuesta.Contenedores.Exists(Function(x) x.IdentificadorContenedor = identificadorContenedor) Then
                                contenedor = objRespuesta.Contenedores.First(Function(x) x.IdentificadorContenedor = identificadorContenedor)

                            Else
                                contenedor = New ContractoServicio.GenesisSaldos.Contenedores.Comon.Contenedor
                                contenedor.CuentaContenedor = New Clases.CuentasContenedor
                                contenedor.CuentaContenedor.Divisas = New ObservableCollection(Of Clases.Divisa)

                                Dim cuenta As New Clases.Cuenta With
                                    {
                                        .Sector = New Clases.Sector,
                                        .Cliente = New Clases.Cliente,
                                        .SubCliente = New Clases.SubCliente,
                                        .PuntoServicio = New Clases.PuntoServicio,
                                        .Canal = New Clases.Canal,
                                        .SubCanal = New Clases.SubCanal
                                    }

                                cuenta.Sector.Delegacion = New Clases.Delegacion
                                cuenta.Sector.Planta = New Clases.Planta

                                cuenta.Sector.Identificador = Peticion.Contenedor.CuentaContenedor.Cuenta.Sector.Identificador
                                cuenta.Sector.Delegacion.Identificador = Peticion.Contenedor.CuentaContenedor.Cuenta.Sector.Delegacion.Identificador
                                cuenta.Sector.Planta.Identificador = Peticion.Contenedor.CuentaContenedor.Cuenta.Sector.Planta.Identificador
                                cuenta.Cliente.Identificador = Peticion.Contenedor.CuentaContenedor.Cuenta.Cliente.Identificador
                                cuenta.SubCliente.Identificador = If(Peticion.Contenedor.CuentaContenedor.Cuenta.SubCliente Is Nothing, String.Empty, Peticion.Contenedor.CuentaContenedor.Cuenta.SubCliente.Identificador)
                                cuenta.PuntoServicio.Identificador = If(Peticion.Contenedor.CuentaContenedor.Cuenta.PuntoServicio Is Nothing, String.Empty, Peticion.Contenedor.CuentaContenedor.Cuenta.PuntoServicio.Identificador)
                                cuenta.Canal.Identificador = Peticion.Contenedor.CuentaContenedor.Cuenta.Canal.Identificador
                                cuenta.SubCanal.Identificador = Peticion.Contenedor.CuentaContenedor.Cuenta.SubCanal.Identificador
                                contenedor.CuentaContenedor.Cuenta = cuenta

                                contenedor.CodigoPosicion = Util.AtribuirValorObj(row("COD_POSICION"), GetType(String))

                                contenedor.IdentificadorContenedor = identificadorContenedor
                                contenedor.CodTipoContenedor = Util.AtribuirValorObj(row("COD_TIPO_CONTENEDOR"), GetType(String))
                                contenedor.TipoContenedor = New Clases.TipoContenedor With {.Identificador = Util.AtribuirValorObj(row("OID_TIPO_CONTENEDOR"), GetType(String)),
                                                                                            .Codigo = Util.AtribuirValorObj(row("COD_TIPO_CONTENEDOR"), GetType(String)),
                                                                                            .Descripcion = Util.AtribuirValorObj(row("DES_TIPO_CONTENEDOR"), GetType(String))
                                                                                           }
                                contenedor.FechaHoraCreacion = Util.AtribuirValorObj(row("GMT_CREACION"), GetType(DateTime))
                                contenedor.FechaHoraCreacionMobile = contenedor.FechaHoraCreacion.ToString

                                objRespuesta.Contenedores.Add(contenedor)

                            End If

                            With contenedor

                                Dim identificadorDivisa As String = Util.AtribuirValorObj(row("OID_DIVISA"), GetType(String))
                                Dim identificadorDenominacion As String = Util.AtribuirValorObj(row("OID_DENOMINACION"), GetType(String))
                                Dim identificadorCalidad As String = Util.AtribuirValorObj(row("OID_CALIDAD"), GetType(String))

                                Dim divisa As Clases.Divisa = Nothing

                                If .CuentaContenedor.Divisas.Exists(Function(x) x.Identificador = identificadorDivisa) Then
                                    divisa = .CuentaContenedor.Divisas.First(Function(x) x.Identificador = identificadorDivisa)

                                Else
                                    divisa = New Clases.Divisa
                                    divisa.Identificador = identificadorDivisa
                                    divisa.Denominaciones = New ObservableCollection(Of Clases.Denominacion)
                                    divisa.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)

                                    .CuentaContenedor.Divisas.Add(divisa)

                                End If

                                ' Total
                                If String.IsNullOrEmpty(identificadorDenominacion) Then

                                    Dim valorTotalEfectivo As Clases.ValorEfectivo = Nothing

                                    If divisa.ValoresTotalesEfectivo.Count > 0 Then
                                        valorTotalEfectivo = divisa.ValoresTotalesEfectivo.First
                                    Else
                                        valorTotalEfectivo = New Clases.ValorEfectivo
                                        divisa.ValoresTotalesEfectivo.Add(valorTotalEfectivo)
                                    End If

                                    valorTotalEfectivo.Importe += Util.AtribuirValorObj(row("NUM_IMPORTE"), GetType(Decimal))

                                Else ' detalhado

                                    Dim denominacion As Clases.Denominacion = Nothing

                                    If divisa.Denominaciones.Exists(Function(x) x.Identificador = identificadorDenominacion) Then
                                        denominacion = divisa.Denominaciones.First(Function(x) x.Identificador = identificadorDenominacion)
                                    Else

                                        denominacion = New Clases.Denominacion
                                        denominacion.Identificador = identificadorDenominacion
                                        denominacion.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)
                                        divisa.Denominaciones.Add(denominacion)
                                    End If

                                    Dim valorDenominacion As New Clases.ValorDenominacion

                                    If denominacion.ValorDenominacion.Exists(Function(x) x.Calidad IsNot Nothing AndAlso x.Calidad.Identificador = identificadorCalidad) Then
                                        valorDenominacion = denominacion.ValorDenominacion.First(Function(x) x.Calidad IsNot Nothing AndAlso x.Calidad.Identificador = identificadorCalidad)
                                    ElseIf denominacion.ValorDenominacion.Exists(Function(x) x.Calidad Is Nothing OrElse String.IsNullOrEmpty(x.Calidad.Identificador)) Then
                                        valorDenominacion = denominacion.ValorDenominacion.First(Function(x) x.Calidad Is Nothing OrElse String.IsNullOrEmpty(x.Calidad.Identificador))
                                    Else
                                        valorDenominacion = New Clases.ValorDenominacion
                                        If Not String.IsNullOrEmpty(identificadorCalidad) Then
                                            valorDenominacion.Calidad = New Clases.Calidad With {.Identificador = identificadorCalidad}
                                        End If
                                        denominacion.ValorDenominacion.Add(valorDenominacion)
                                    End If

                                    valorDenominacion.Cantidad += Util.AtribuirValorObj(row("NEL_CANTIDAD"), GetType(Int64))
                                    valorDenominacion.Importe += Util.AtribuirValorObj(row("NUM_IMPORTE"), GetType(Decimal))

                                End If

                                If ds.Tables.Contains(Comon.Constantes.CONST_RECUPERAR_CONTENEDORES_FIFO_TABLA_RC_PRECINTOS) Then

                                    Dim rowsPrecintos = ds.Tables(Comon.Constantes.CONST_RECUPERAR_CONTENEDORES_FIFO_TABLA_RC_PRECINTOS). _
                                                                                Select(String.Format("OID_CONTENEDOR = '{0}'", identificadorContenedor))

                                    If rowsPrecintos IsNot Nothing AndAlso rowsPrecintos.Count > 0 Then
                                        .Precintos = New ObservableCollection(Of String)
                                        For indice = 0 To rowsPrecintos.Length - 1
                                            .Precintos.Add(Util.AtribuirValorObj(rowsPrecintos(indice)("COD_PRECINTO"), GetType(String)))
                                        Next
                                    End If

                                End If

                            End With

                        Next

                    End If

                End If

            End If

            Return objRespuesta

        End Function

#End Region

#Region "ArmarContenedores"

        Public Shared Function ArmarContenedores(Peticion As ArmarContenedores.Peticion) As ArmarContenedores.Respuesta

            Dim objRespuesta As New ArmarContenedores.Respuesta

            Try

                If Peticion.Documento IsNot Nothing AndAlso Peticion.Documento.Contenedores IsNot Nothing AndAlso Peticion.Documento.Contenedores.Count > 0 Then

                    Dim cuentasContenedor As List(Of Comon.Clases.CuentasContenedor) = Nothing

                    If Peticion.Documento.Contenedores IsNot Nothing AndAlso Peticion.Documento.Contenedores.Count > 0 Then

                        If Peticion.Documento.Contenedores.Count > 1 Then

                            Dim objGrupoDocumentos As Comon.Clases.GrupoDocumentos = CrearGrupoDocumentoContenedor(Peticion.Documento)
                            AccesoDatos.GenesisSaldos.GrupoDocumentos.GuardarGrupoDocumentoContenedor(objGrupoDocumentos, objRespuesta.CodigoComprobante)

                        Else

                            Dim objDocumento As Comon.Clases.Documento = CrearDocumentoContenedor(Peticion.Documento)
                            AccesoDatos.GenesisSaldos.Documento.GuardarDocumentoContenedor(objDocumento, objRespuesta.CodigoComprobante, objRespuesta.IdentificadorDocumento)
                            objRespuesta.FechaCreacionContenedor = ObtenerFechaCreacionContenedor(objRespuesta.IdentificadorDocumento)

                        End If

                    End If

                End If


            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.Mensajes.Add(ex.Descricao)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.Excepciones.Add(ex.ToString)
            End Try

            Return objRespuesta
        End Function

        ''' <summary>
        ''' Obtem la fecha creacion del contenedor para impressión de la etiqueta
        ''' </summary>
        ''' <param name="identificadorDocumento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function ObtenerFechaCreacionContenedor(identificadorDocumento As String)
            Dim fechaCreacionContenedor As DateTime
            Try
                fechaCreacionContenedor = AccesoDatos.GenesisSaldos.Contenedor.ObtenerFechaCreacionContenedor(identificadorDocumento)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                fechaCreacionContenedor = DateTime.Now
            End Try
            Return fechaCreacionContenedor
        End Function

        ''' <summary>
        ''' Cria o grupo de documentos do contenedor
        ''' </summary>
        ''' <param name="Documento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function CrearGrupoDocumentoContenedor(Documento As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ArmarContenedores.Documento) As Comon.Clases.GrupoDocumentos

            Dim objGrupoDocumento As New Comon.Clases.GrupoDocumentos

            objGrupoDocumento.Formulario = New Comon.Clases.Formulario
            objGrupoDocumento.Formulario.Codigo = Documento.CodigoFormulario

            objGrupoDocumento.Estado = Documento.EstadoDocumento
            objGrupoDocumento.UsuarioCreacion = Documento.CodigoUsuario
            objGrupoDocumento.Rowver = Documento.VersaoDocumento

            objGrupoDocumento.CuentaOrigen = New Comon.Clases.Cuenta With { _
                                        .Cliente = Nothing,
                                        .SubCliente = Nothing,
                                        .PuntoServicio = Nothing,
                                        .Canal = Nothing,
                                        .SubCanal = Nothing,
                                        .Sector = New Comon.Clases.Sector With { _
                                                  .Identificador = Documento.IdentificadorSector,
                                                  .Delegacion = New Comon.Clases.Delegacion With {.Identificador = Documento.IdentificadorDelegacion},
                                                  .Planta = New Comon.Clases.Planta With {.Identificador = Documento.IdentificadorPlanta}}}

            objGrupoDocumento.CuentaDestino = New Comon.Clases.Cuenta With { _
                                            .Cliente = Nothing,
                                            .SubCliente = Nothing,
                                            .PuntoServicio = Nothing,
                                            .Canal = Nothing,
                                            .SubCanal = Nothing,
                                            .Sector = New Comon.Clases.Sector With { _
                                                      .Identificador = Documento.IdentificadorSector,
                                                      .Delegacion = New Comon.Clases.Delegacion With {.Identificador = Documento.IdentificadorDelegacion},
                                                      .Planta = New Comon.Clases.Planta With {.Identificador = Documento.IdentificadorPlanta}}}

            objGrupoDocumento.Documentos = CrearDocumentosContenedor(Documento)

            Return objGrupoDocumento
        End Function

        ''' <summary>
        ''' Cria o documento de contenedor
        ''' </summary>
        ''' <param name="Documento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function CrearDocumentoContenedor(Documento As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ArmarContenedores.Documento) As Comon.Clases.Documento

            Dim objDocumento As New Comon.Clases.Documento
            objDocumento.Formulario = New Comon.Clases.Formulario

            objDocumento.Formulario.Codigo = Documento.CodigoFormulario

            objDocumento.UsuarioCreacion = Documento.CodigoUsuario
            objDocumento.Estado = Documento.EstadoDocumento
            objDocumento.FechaHoraGestion = Documento.FechaGestion
            objDocumento.FechaHoraPlanificacionCertificacion = DateTime.UtcNow
            objDocumento.NumeroExterno = Documento.NumeroExterno
            objDocumento.Rowver = Nothing

            objDocumento.Elemento = New Comon.Clases.Contenedor

            Dim objCliente As Comon.Clases.Cliente = Nothing
            Dim objSubCliente As Comon.Clases.SubCliente = Nothing
            Dim objPuntoServicio As Comon.Clases.PuntoServicio = Nothing
            Dim objCanal As Comon.Clases.Canal = Nothing
            Dim objSubCanal As Comon.Clases.SubCanal = Nothing
            Dim objDivisas As ObservableCollection(Of Comon.Clases.Divisa) = Nothing

            If Documento.Contenedores.First.Cuenta.Cuenta.Cliente IsNot Nothing Then
                objCliente = Documento.Contenedores.First.Cuenta.Cuenta.Cliente
            End If

            If Documento.Contenedores.First.Cuenta.Cuenta.SubCliente IsNot Nothing Then
                objSubCliente = Documento.Contenedores.First.Cuenta.Cuenta.SubCliente
            End If

            If Documento.Contenedores.First.Cuenta.Cuenta.PuntoServicio IsNot Nothing Then
                objPuntoServicio = Documento.Contenedores.First.Cuenta.Cuenta.PuntoServicio
            End If

            If Documento.Contenedores.First.Cuenta.Cuenta.Canal IsNot Nothing Then
                objCanal = Documento.Contenedores.First.Cuenta.Cuenta.Canal
            End If

            If Documento.Contenedores.First.Cuenta.Cuenta.SubCanal IsNot Nothing Then
                objSubCanal = Documento.Contenedores.First.Cuenta.Cuenta.SubCanal
            End If


            objDivisas = Documento.Contenedores.First.Cuenta.Divisas.ToObservableCollection

            Prosegur.Genesis.Comon.Util.UnificaItemsDivisas(objDivisas, True)

            objDocumento.Divisas = objDivisas

            objDocumento.CuentaOrigen = New Comon.Clases.Cuenta With { _
                                        .Cliente = objCliente,
                                        .SubCliente = objSubCliente,
                                        .PuntoServicio = objPuntoServicio,
                                        .Canal = objCanal,
                                        .SubCanal = objSubCanal,
                                        .Sector = New Comon.Clases.Sector With { _
                                                  .Identificador = Documento.IdentificadorSector,
                                                  .Delegacion = New Comon.Clases.Delegacion With {.Identificador = Documento.IdentificadorDelegacion},
                                                  .Planta = New Comon.Clases.Planta With {.Identificador = Documento.IdentificadorPlanta}}}


            objDocumento.CuentaDestino = New Comon.Clases.Cuenta With { _
                                                   .Cliente = objCliente,
                                                   .SubCliente = objSubCliente,
                                                   .PuntoServicio = objPuntoServicio,
                                                   .Canal = objCanal,
                                                   .SubCanal = objSubCanal,
                                                   .Sector = New Comon.Clases.Sector With { _
                                                             .Identificador = Documento.IdentificadorSector,
                                                             .Delegacion = New Comon.Clases.Delegacion With {.Identificador = Documento.IdentificadorDelegacion},
                                                             .Planta = New Comon.Clases.Planta With {.Identificador = Documento.IdentificadorPlanta}}}

            objDocumento.Elemento = New Comon.Clases.Contenedor With { _
                                          .AgrupaContenedor = False,
                                          .Codigo = Documento.Contenedores.First.CodigoContenedor,
                                          .TipoContenedor = Documento.Contenedores.First.TipoContenedor,
                                          .TipoServicio = Documento.Contenedores.First.TipoServicio,
                                          .TipoFormato = Documento.Contenedores.First.TipoFormato,
                                          .Elementos = Documento.Contenedores.First.Elementos.ToObservableCollection,
                                          .PuestoResponsable = Documento.Contenedores.First.CodigoPuesto,
                                          .Precintos = Documento.Contenedores.First.Precintos.ToObservableCollection,
                                          .PrecintoAutomatico = Documento.Contenedores.First.PrecintoAutomatico,
                                          .Divisas = Documento.Contenedores.First.Cuenta.Divisas.ToObservableCollection}



            Return objDocumento
        End Function

        ''' <summary>
        ''' Cria o documento de contenedor
        ''' </summary>
        ''' <param name="Documento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function CrearDocumentosContenedor(Documento As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ArmarContenedores.Documento) As ObservableCollection(Of Comon.Clases.Documento)

            Dim objDocumentos As New ObservableCollection(Of Comon.Clases.Documento)

            Dim objDocumento As Comon.Clases.Documento = Nothing

            If Documento.Contenedores IsNot Nothing AndAlso Documento.Contenedores.Count > 0 Then

                For Each objDoc In Documento.Contenedores

                    If objDoc.Cuenta IsNot Nothing Then

                        objDocumento = New Comon.Clases.Documento

                        objDocumento.Formulario = New Comon.Clases.Formulario

                        objDocumento.Formulario.Codigo = Documento.CodigoFormulario

                        objDocumento.UsuarioCreacion = Documento.CodigoUsuario
                        objDocumento.Estado = Documento.EstadoDocumento
                        objDocumento.FechaHoraGestion = Documento.FechaGestion
                        objDocumento.FechaHoraPlanificacionCertificacion = Documento.FechaPlanCertificacion

                        Dim objDivisas As ObservableCollection(Of Comon.Clases.Divisa) = Nothing

                        objDivisas = objDoc.Cuenta.Divisas

                        Prosegur.Genesis.Comon.Util.UnificaItemsDivisas(objDivisas, True)

                        objDocumento.Divisas = objDivisas

                        objDocumento.CuentaOrigen = New Comon.Clases.Cuenta With { _
                                                    .Cliente = objDoc.Cuenta.Cuenta.Cliente,
                                                    .SubCliente = objDoc.Cuenta.Cuenta.SubCliente,
                                                    .PuntoServicio = objDoc.Cuenta.Cuenta.PuntoServicio,
                                                    .Canal = objDoc.Cuenta.Cuenta.Canal,
                                                    .SubCanal = objDoc.Cuenta.Cuenta.SubCanal,
                                                    .Sector = New Comon.Clases.Sector With { _
                                                              .Identificador = Documento.IdentificadorSector,
                                                              .Delegacion = New Comon.Clases.Delegacion With {.Identificador = Documento.IdentificadorDelegacion},
                                                              .Planta = New Comon.Clases.Planta With {.Identificador = Documento.IdentificadorPlanta}}}

                        objDocumento.CuentaDestino = New Comon.Clases.Cuenta With { _
                                                   .Cliente = objDoc.Cuenta.Cuenta.Cliente,
                                                   .SubCliente = objDoc.Cuenta.Cuenta.SubCliente,
                                                   .PuntoServicio = objDoc.Cuenta.Cuenta.PuntoServicio,
                                                   .Canal = objDoc.Cuenta.Cuenta.Canal,
                                                   .SubCanal = objDoc.Cuenta.Cuenta.SubCanal,
                                                   .Sector = New Comon.Clases.Sector With { _
                                                             .Identificador = Documento.IdentificadorSector,
                                                             .Delegacion = New Comon.Clases.Delegacion With {.Identificador = Documento.IdentificadorDelegacion},
                                                             .Planta = New Comon.Clases.Planta With {.Identificador = Documento.IdentificadorPlanta}}}

                        objDoc.Cuenta.Cuenta = objDocumento.CuentaOrigen

                        objDocumento.Elemento = New Comon.Clases.Contenedor With { _
                                                      .AgrupaContenedor = False,
                                                      .Codigo = objDoc.CodigoContenedor,
                                                      .TipoContenedor = objDoc.TipoContenedor,
                                                      .TipoServicio = objDoc.TipoServicio,
                                                      .TipoFormato = objDoc.TipoFormato,
                                                      .Elementos = objDoc.Elementos.ToObservableCollection,
                                                      .PrecintoAutomatico = objDoc.PrecintoAutomatico,
                                                      .Cuenta = objDoc.Cuenta.Cuenta}


                        objDocumentos.Add(objDocumento)


                    End If

                Next

            End If

            Return objDocumentos
        End Function

#End Region

#Region "Reenvio Entre Sectores"

        Public Shared Sub ValidarDatos(Peticion As ReenvioEntreSectores.Peticion)

            Util.ValidarCampoObrigatorio(Peticion.CodigoUsuario, "CodigoUsuario", GetType(String), False, True)

            Util.ValidarCampoObrigatorio(Peticion.Documento, "Documento", GetType(ReenvioEntreSectores.Documento), False, True)

            Util.ValidarCampoObrigatorio(Peticion.Documento.SectorOrigen, "SectorOrigen", GetType(ContractoServicio.GenesisSaldos.Contenedores.Comon.Sector), False, True)
            Util.ValidarCampoObrigatorio(Peticion.Documento.SectorOrigen.IdentificadorDelegacion, "IdentificadorDelegacionOrigen", GetType(String), False, True)
            Util.ValidarCampoObrigatorio(Peticion.Documento.SectorOrigen.IdentificadorPlanta, "IdentificadorPlantaOrigen", GetType(String), False, True)
            Util.ValidarCampoObrigatorio(Peticion.Documento.SectorOrigen.IdentificadorSector, "IdentificadorSectorOrigen", GetType(String), False, True)


            Util.ValidarCampoObrigatorio(Peticion.Documento.SectorDestino, "SectorDestino", GetType(ContractoServicio.GenesisSaldos.Contenedores.Comon.Sector), False, True)
            Util.ValidarCampoObrigatorio(Peticion.Documento.SectorOrigen.IdentificadorDelegacion, "IdentificadorDelegacionDestino", GetType(String), False, True)
            Util.ValidarCampoObrigatorio(Peticion.Documento.SectorOrigen.IdentificadorPlanta, "IdentificadorPlantaDestino", GetType(String), False, True)
            Util.ValidarCampoObrigatorio(Peticion.Documento.SectorOrigen.IdentificadorSector, "IdentificadorSectorDestino", GetType(String), False, True)

            Util.ValidarCampoObrigatorio(Peticion.Documento.Contenedores, "Contenedores", GetType(ReenvioEntreSectores.Contenedor), True, True)

            ValidarMesmoSetor(Peticion)

            If (From c In Peticion.Documento.Contenedores Where String.IsNullOrEmpty(c.CodigoPrecinto) AndAlso String.IsNullOrEmpty(c.IdentificadorContenedor)).Count > 0 Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("NUEVO_SALDOS_DEBE_TENER_OID_O_PRECINTO_CONTENEDOR"))
            End If

        End Sub

        Private Shared Sub ValidarMesmoSetor(Peticion As ReenvioEntreSectores.Peticion)
            Dim mesmoSector = Peticion.Documento.SectorOrigen.IdentificadorSector.Equals(Peticion.Documento.SectorDestino.IdentificadorSector)
            If mesmoSector Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("NUEVO_SALDOS_DEBE_SER_SECTORES_DISTINTOS"))
            End If
        End Sub

        Public Shared Function ReenvioEntreSectores(Peticion As ReenvioEntreSectores.Peticion) As ReenvioEntreSectores.Respuesta

            Dim respuesta As New ReenvioEntreSectores.Respuesta

            Try

                ValidarDatos(Peticion)

                AccesoDatos.GenesisSaldos.Contenedor.ReenvioEntreSectores(Peticion.Documento, respuesta.CodigoCombrobante, respuesta.IdentificadorDocumento)

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.Mensajes.Add(ex.Descricao)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.Excepciones.Add(ex.ToString)
            End Try


            Return respuesta
        End Function
#End Region

#Region "Reenvio Entre Clientes"

        Public Shared Sub ValidarDatos(Peticion As ReenvioEntreClientes.Peticion)

            Util.ValidarCampoObrigatorio(Peticion.CodigoUsuario, "CodigoUsuario", GetType(String), False, True)

            Util.ValidarCampoObrigatorio(Peticion.Documento, "Documento", GetType(ReenvioEntreClientes.Documento), False, True)

            Util.ValidarCampoObrigatorio(Peticion.Documento.Sector, "SectorOrigen", GetType(ContractoServicio.GenesisSaldos.Contenedores.Comon.Sector), False, True)
            Util.ValidarCampoObrigatorio(Peticion.Documento.Sector.IdentificadorDelegacion, "IdentificadorDelegacion", GetType(String), False, True)
            Util.ValidarCampoObrigatorio(Peticion.Documento.Sector.IdentificadorPlanta, "IdentificadorPlanta", GetType(String), False, True)
            Util.ValidarCampoObrigatorio(Peticion.Documento.Sector.IdentificadorSector, "IdentificadorSector", GetType(String), False, True)


            Util.ValidarCampoObrigatorio(Peticion.Documento.ClienteDestino, "ClienteDestino", GetType(ContractoServicio.GenesisSaldos.Contenedores.Comon.Cliente), False, True)
            Util.ValidarCampoObrigatorio(Peticion.Documento.ClienteDestino.IdentificadorCliente, "IdentificadorCliente", GetType(String), False, True)

            Util.ValidarCampoObrigatorio(Peticion.Documento.Contenedores, "Contenedores", GetType(ReenvioEntreClientes.Contenedor), True, True)

            If (From c In Peticion.Documento.Contenedores Where String.IsNullOrEmpty(c.CodigoPrecinto) AndAlso String.IsNullOrEmpty(c.IdentificadorContenedor)).Count > 0 Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("NUEVO_SALDOS_DEBE_TENER_OID_O_PRECINTO_CONTENEDOR"))
            End If

        End Sub

        Public Shared Function ReenvioEntreClientes(Peticion As ReenvioEntreClientes.Peticion) As ReenvioEntreClientes.Respuesta

            Dim respuesta As New ReenvioEntreClientes.Respuesta

            Try

                ValidarDatos(Peticion)

                AccesoDatos.GenesisSaldos.Contenedor.ReenvioEntreClientes(Peticion.Documento, respuesta.CodigoCombrobante, respuesta.IdentificadorDocumento)

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.Mensajes.Add(ex.Descricao)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.Excepciones.Add(ex.ToString)
            End Try


            Return respuesta
        End Function
#End Region

#Region "DesarmarContenedores"

        Public Shared Function DesarmarContenedores(Peticion As DesarmarContenedores.Peticion) As DesarmarContenedores.Respuesta

            Dim respuesta As New DesarmarContenedores.Respuesta

            Try

                ValidarCamposObligatorios(Peticion)

                AccesoDatos.GenesisSaldos.Contenedor.DesarmarContenedores(Peticion.Documento, respuesta.CodigoCombrobante, respuesta.IdentificadorDocumento)

            Catch ex As Excepcion.NegocioExcepcion
                If ex.InnerException IsNot Nothing AndAlso ex.InnerException.InnerException IsNot Nothing AndAlso Not String.IsNullOrEmpty(ex.InnerException.InnerException.Message) Then
                    Try
                        Dim i As Integer = ex.InnerException.InnerException.Message.IndexOf("#")
                        Dim mensaje As String = ex.InnerException.InnerException.Message.Substring(i + 1, ex.InnerException.InnerException.Message.IndexOf("#", i + 1) - i - 1)
                        respuesta.Mensajes.Add(mensaje)
                    Catch ex1 As Exception
                        Util.TratarErroBugsnag(ex)
                        respuesta.Mensajes.Add(ex.InnerException.InnerException.Message)
                    End Try
                Else
                    respuesta.Mensajes.Add(ex.Message)
                End If

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                If ex.InnerException IsNot Nothing AndAlso ex.InnerException.InnerException IsNot Nothing AndAlso Not String.IsNullOrEmpty(ex.InnerException.InnerException.Message) Then
                    Try
                        Dim i As Integer = ex.InnerException.InnerException.Message.IndexOf("#")
                        Dim mensaje As String = ex.InnerException.InnerException.Message.Substring(i + 1, ex.InnerException.InnerException.Message.IndexOf("#", i + 1) - i - 1)
                        respuesta.Excepciones.Add(mensaje)
                    Catch ex1 As Exception
                        Util.TratarErroBugsnag(ex)
                        respuesta.Excepciones.Add(ex.InnerException.InnerException.Message)
                    End Try
                Else
                    respuesta.Excepciones.Add(ex.Message)
                End If

            End Try

            Return respuesta

        End Function

        Private Shared Sub ValidarCamposObligatorios(Peticion As DesarmarContenedores.Peticion)

            If Peticion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("gen_srv_msg_atributo"), "Petición"))
            End If

            Dim _mensajes As New List(Of String)

            With Peticion

                If .Documento Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("gen_srv_msg_atributo"), "Documento"))
                End If

                If String.IsNullOrEmpty(.Documento.CodFormulario) Then
                    _mensajes.Add(String.Format(Traduzir("gen_srv_msg_atributo"), "CodigoFormulario"))
                End If

                If .Documento.FechaGestion = Date.MinValue Then
                    _mensajes.Add(String.Format(Traduzir("gen_srv_msg_atributo"), "Fecha Gestión"))
                End If

                If .Documento.SectorOrigen Is Nothing OrElse .Documento.SectorOrigen.Delegacion Is Nothing OrElse String.IsNullOrEmpty(.Documento.SectorOrigen.Delegacion.Identificador) Then
                    _mensajes.Add(String.Format(Traduzir("gen_srv_msg_atributo"), "Delegación"))
                End If

                If .Documento.SectorOrigen Is Nothing OrElse .Documento.SectorOrigen.Planta Is Nothing OrElse String.IsNullOrEmpty(.Documento.SectorOrigen.Planta.Identificador) Then
                    _mensajes.Add(String.Format(Traduzir("gen_srv_msg_atributo"), "Planta"))
                End If

                If .Documento.SectorOrigen Is Nothing OrElse String.IsNullOrEmpty(Peticion.Documento.SectorOrigen.Identificador) Then
                    _mensajes.Add(String.Format(Traduzir("gen_srv_msg_atributo"), "Sector"))
                End If

                If .Documento.Contenedores Is Nothing OrElse .Documento.Contenedores.Count = 0 Then
                    _mensajes.Add(String.Format(Traduzir("gen_srv_msg_atributo"), "Contenedores"))
                End If

                For Each contenedor In .Documento.Contenedores

                    If String.IsNullOrEmpty(contenedor.IdentificadorContenedor) AndAlso
                       (contenedor.Precintos Is Nothing OrElse contenedor.Precintos.Count = 0 OrElse contenedor.Precintos.Where(Function(e) String.IsNullOrWhiteSpace(e)).Count = contenedor.Precintos.Count) Then
                        _mensajes.Add(Traduzir("NUEVO_SALDOS_DEBE_TENER_OID_O_PRECINTO_CONTENEDOR"))
                        Exit For
                    End If

                Next

            End With

            If _mensajes.Count > 0 Then
                Throw New Excepcion.NegocioExcepcion(Join(_mensajes.ToArray, vbNewLine))
            End If

        End Sub

        Private Shared Function ConverterDocumentoContenedor(Peticion As DesarmarContenedores.Peticion) As Comon.Clases.Documento

            If Peticion IsNot Nothing AndAlso Peticion.Documento IsNot Nothing Then

                Dim documento As New Comon.Clases.Documento

                With Peticion.Documento

                    documento.Formulario = New Comon.Clases.Formulario With {.Codigo = Peticion.Documento.CodFormulario}
                    documento.FechaHoraPlanificacionCertificacion = .FechaPlanCertificacion
                    documento.FechaHoraGestion = .FechaGestion
                    documento.FechaHoraCreacion = .FechaCreacion
                    documento.FechaHoraModificacion = .FechaModificacion
                    documento.Estado = RecuperarEnum(Of Comon.Enumeradores.EstadoDocumento)(.CodEstadoDocumento)

                    If .Cuenta IsNot Nothing Then
                        documento.CuentaOrigen = .Cuenta
                    End If

                    'If .Contenedores IsNot Nothing AndAlso .Contenedores.Count > 0 Then

                    '    documento.Contenedores = New ObservableCollection(Of Comon.Clases.Contenedor)

                    '    For Each contenedor In .Contenedores

                    '        documento.Contenedores.Add(New Comon.Clases.Contenedor With
                    '                                   {
                    '                                       .Identificador = contenedor.IdentificadorContenedor,
                    '                                       .Precintos = contenedor.Precintos
                    '                                   })

                    '    Next contenedor

                    'End If

                End With

                Return documento

            End If

            Return Nothing

        End Function

#End Region

#Region "GrabarAlertaVencimiento"
        Public Shared Function GrabarAlertaVencimiento(Peticion As GrabarAlertaVencimiento.Peticion) As GrabarAlertaVencimiento.Respuesta

            Dim objRespuesta As New GrabarAlertaVencimiento.Respuesta

            Try
                If Peticion.Contenedor.codPrecinto Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_Precinto")))
                ElseIf Peticion.Contenedor.AlertaVencimento.codTipoAlerta Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_CodTipoAlerta")))
                ElseIf Peticion.Contenedor.AlertaVencimento.codTipoAlerta = "AV" AndAlso Peticion.Contenedor.AlertaVencimento.diasVencer = 0 Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_diasVencer")))
                ElseIf Peticion.Contenedor.AlertaVencimento.emails Is Nothing OrElse String.IsNullOrEmpty(Peticion.Contenedor.AlertaVencimento.emails) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_emails")))
                Else
                    AccesoDatos.GenesisSaldos.Contenedor.GrabarAlertaVencimiento(Peticion)

                    objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                    objRespuesta.MensajeError = String.Empty
                End If
            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.CodigoError = ex.Codigo
                objRespuesta.MensajeError = ex.Descricao
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString
            End Try

            Return objRespuesta

        End Function
#End Region

#Region "GrabarInventarioContenedor"
        Public Shared Function GrabarInventarioContenedor(Peticion As GrabarInventarioContenedor.Peticion) As GrabarInventarioContenedor.Respuesta

            Dim objRespuesta As New GrabarInventarioContenedor.Respuesta

            Try
                If Peticion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), "Peticion"))
                ElseIf Peticion.Inventario Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), "Inventario"))
                ElseIf Peticion.Inventario.Sector Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), "Sector"))
                ElseIf String.IsNullOrEmpty(Peticion.Inventario.Sector.codDelegacion) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_Delegacion")))
                ElseIf String.IsNullOrEmpty(Peticion.Inventario.Sector.codPlanta) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_Planta")))
                ElseIf String.IsNullOrEmpty(Peticion.Inventario.Sector.codSector) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_Sector")))
                ElseIf String.IsNullOrEmpty(Peticion.Inventario.UsuarioCreacion) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_UsuarioCreacion")))
                Else
                    'Si el par?metro Inventario.codInventario est? rellenado:
                    'SAPR_TINVENTARIO.COD_INVENTARIO = Par?metro Inventario.codInventario*/
                    ' Caso contrario, generar el c?digo autom?ticamente: El precinto generado ser? compuesto por el C?digo del Sector/B?veda + C?digo Cliente (si inventario es por cliente) + Fecha y hora actual (formato yyyymmddmmss).
                    '  Ejemplo: 00101028800020151218152053 donde:
                    '  o  C?digo del Sector/B?veda Inventariado = 001010
                    '  o  C?digo del Cliente = 288-000
                    '  o  Fecha y hora actual = 18/12/2015 15:20:53*/

                    If String.IsNullOrEmpty(Peticion.Inventario.codInventario) Then
                        Peticion.Inventario.codInventario = Peticion.Inventario.Sector.codSector & If(Peticion.Inventario.Cliente Is Nothing, String.Empty, Peticion.Inventario.Cliente.codCliente) & Date.Now.ToString("dd/MM/yyyy HH:mm:ss")
                        Peticion.Inventario.codInventario = Peticion.Inventario.codInventario.Replace("-", "").Replace(":", "").Replace("/", "").Replace(" ", "")
                    End If

                    Dim dsCont As DataSet = AccesoDatos.GenesisSaldos.Contenedor.GrabarInventarioContenedor(Peticion)
                    objRespuesta = PreencheGravarInventarioContenedor(dsCont)

                    objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                    objRespuesta.MensajeError = String.Empty
                End If
            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.CodigoError = ex.Codigo
                objRespuesta.MensajeError = ex.Descricao
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString
            End Try

            Return objRespuesta

        End Function

        Private Shared Function PreencheGravarInventarioContenedor(dsRetorno As DataSet) As GrabarInventarioContenedor.Respuesta
            Dim objRespuesta As New GrabarInventarioContenedor.Respuesta
            Dim objClientes As New List(Of GrabarInventarioContenedor.Cliente)
            Dim objCanales As New List(Of GrabarInventarioContenedor.Canal)

            If dsRetorno IsNot Nothing AndAlso dsRetorno.Tables.Count > 0 Then
                If dsRetorno.Tables.Contains("INVENTARIO") Then
                    Dim dtContenedores = dsRetorno.Tables("INVENTARIO")
                    If dtContenedores IsNot Nothing AndAlso dtContenedores.Rows.Count > 0 Then
                        objRespuesta.codInventario = Util.AtribuirValorObj(dtContenedores(0)("COD_INVENTARIO"), GetType(String))

                        If objRespuesta.contenedores Is Nothing Then
                            objRespuesta.contenedores = New List(Of GrabarInventarioContenedor.ContenedorRespuesta)
                        End If

                        For Each dtRow In dtContenedores.Rows
                            Dim oidContenedor As String = Util.AtribuirValorObj(dtRow("OID_CONTENEDOR"), GetType(String))
                            Dim objContenedor As New GrabarInventarioContenedor.ContenedorRespuesta

                            With objContenedor
                                .codTipoContenedor = Util.AtribuirValorObj(dtRow("COD_TIPO_CONTENEDOR"), GetType(String))
                                .desTipoContenedor = Util.AtribuirValorObj(dtRow("DES_TIPO_CONTENEDOR"), GetType(String))
                                .fechaArmado = Util.AtribuirValorObj(dtRow("FECHAARMADO"), GetType(DateTime))
                                .Sector = New GrabarInventarioContenedor.SectorRespuesta With {.codDelegacion = Util.AtribuirValorObj(dtRow("COD_TIPO_CONTENEDOR"), GetType(String)), _
                                                                                                   .codPlanta = Util.AtribuirValorObj(dtRow("COD_TIPO_CONTENEDOR"), GetType(String)), _
                                                                                                   .codPosicion = Util.AtribuirValorObj(dtRow("COD_TIPO_CONTENEDOR"), GetType(String)), _
                                                                                                   .codSector = Util.AtribuirValorObj(dtRow("COD_TIPO_CONTENEDOR"), GetType(String))
                                                                                                 }
                                objClientes.Add(New GrabarInventarioContenedor.Cliente With {.codCliente = Util.AtribuirValorObj(dtRow("COD_TIPO_CONTENEDOR"), GetType(String)), _
                                                                                                                                       .codSubcliente = Util.AtribuirValorObj(dtRow("COD_TIPO_CONTENEDOR"), GetType(String)), _
                                                                                                                                       .codPuntoServicio = Util.AtribuirValorObj(dtRow("COD_TIPO_CONTENEDOR"), GetType(String))})
                                .clientes = objClientes
                                objCanales.Add(New GrabarInventarioContenedor.Canal With {.oidCanal = Util.AtribuirValorObj(dtRow("OID_CANAL"), GetType(String)), _
                                                                                            .codCanal = Util.AtribuirValorObj(dtRow("COD_CANAL"), GetType(String)), _
                                                                                            .codSubcanal = Util.AtribuirValorObj(dtRow("COD_SUBCANAL"), GetType(String))})
                                .canales = objCanales


                                .DetalleEfectivo = PreencheGravarInventarioContenedorDetEfec(dsRetorno, oidContenedor)
                            End With
                            objRespuesta.contenedores.Add(objContenedor)
                        Next
                    End If
                End If
            End If
            Return objRespuesta
        End Function

        Private Shared Function PreencheGravarInventarioContenedorDetEfec(dsRetorno As DataSet, oidContenedor As String) As List(Of GrabarInventarioContenedor.DetalleEfectivo)
            Dim lstDetalleEfectivo As List(Of GrabarInventarioContenedor.DetalleEfectivo) = Nothing
            If dsRetorno.Tables.Contains("DETEFECMP") Then
                Dim dtEfec As DataTable = dsRetorno.Tables("DETEFECMP") '.DefaultView.ToTable(True, "OID_CONTENEDOR", "COD_ISO_DIVISA", "DES_DIVISA", "COD_SIMBOLO", "COD_DENOMINACION", "COD_CALIDAD", "BOL_DISPONIBLE", "BOL_BLOQUEADO", "NEL_CANTIDAD", "NUM_IMPORTE")
                If dtEfec.Rows.Count > 0 Then
                    lstDetalleEfectivo = New List(Of GrabarInventarioContenedor.DetalleEfectivo)
                    For Each dtRow In dtEfec.Select(String.Format("OID_CONTENEDOR = '{0}'", oidContenedor))
                        Dim objDetalleEfectivo As New GrabarInventarioContenedor.DetalleEfectivo
                        With objDetalleEfectivo
                            .codDivisa = Util.AtribuirValorObj(dtRow("COD_ISO_DIVISA"), GetType(String))
                            .desDivisa = Util.AtribuirValorObj(dtRow("DES_DIVISA"), GetType(String))
                            .codSimbolo = Util.AtribuirValorObj(dtRow("COD_SIMBOLO"), GetType(String))
                            .codDenominacion = Util.AtribuirValorObj(dtRow("COD_DENOMINACION"), GetType(String))
                            .codCalidad = Util.AtribuirValorObj(dtRow("COD_CALIDAD"), GetType(String))
                            .disponible = Util.AtribuirValorObj(dtRow("BOL_DISPONIBLE"), GetType(Boolean))
                            .bloqueado = Util.AtribuirValorObj(dtRow("BOL_BLOQUEADO"), GetType(Boolean))
                            .cantidad = Util.AtribuirValorObj(dtRow("NEL_CANTIDAD"), GetType(Integer))
                            .importe = Util.AtribuirValorObj(dtRow("NUM_IMPORTE"), GetType(Decimal))
                        End With
                        lstDetalleEfectivo.Add(objDetalleEfectivo)
                    Next
                End If
            End If
            Return lstDetalleEfectivo
        End Function
#End Region

#Region "ConsultarInventarioContenedor"
        Public Shared Function ConsultarInventarioContenedor(Peticion As ConsultarInventarioContenedor.Peticion) As ConsultarInventarioContenedor.Respuesta

            Dim objRespuesta As New ConsultarInventarioContenedor.Respuesta

            Try
                Dim dsCont As DataSet = AccesoDatos.GenesisSaldos.Contenedor.ConsultarInventarioContenedor(Peticion)
                objRespuesta = PreencheConsultarInventarioContenedor(dsCont)

                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = String.Empty
            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.CodigoError = ex.Codigo
                objRespuesta.MensajeError = ex.Descricao
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString
            End Try

            Return objRespuesta

        End Function
        Private Shared Function PreencheConsultarInventarioContenedor(dsRetorno As DataSet) As ConsultarInventarioContenedor.Respuesta
            Dim objRespuesta As New ConsultarInventarioContenedor.Respuesta
            Dim objClientes As New ConsultarInventarioContenedor.Cliente
            Dim objCanales As New ConsultarInventarioContenedor.Canal
            Dim objCuentas As New List(Of ConsultarInventarioContenedor.Cuenta)
            Dim objdetalheEfectivo As List(Of ConsultarInventarioContenedor.DetalleEfectivo)

            If dsRetorno IsNot Nothing AndAlso dsRetorno.Tables.Count > 0 Then

                If dsRetorno.Tables.Contains("INVENTARIO") Then

                    Dim dtInventario = dsRetorno.Tables("INVENTARIO")

                    If dtInventario IsNot Nothing AndAlso dtInventario.Rows.Count > 0 Then

                        objRespuesta.contenedores = New List(Of ConsultarInventarioContenedor.ContenedorRespuesta)
                        objRespuesta.inventarios = New List(Of ConsultarInventarioContenedor.InventarioRespuesta)

                        For Each dtRow In dtInventario.Rows

                            If objRespuesta.inventarios.Where(Function(e) e.codInventario _
                                                              = Util.AtribuirValorObj(dtRow("COD_INVENTARIO"), GetType(String))).FirstOrDefault() _
                                                              Is Nothing Then

                                Dim objInventario = New ConsultarInventarioContenedor.InventarioRespuesta
                                With objInventario
                                    .codInventario = Util.AtribuirValorObj(dtRow("COD_INVENTARIO"), GetType(String))
                                    .desUsuario = Util.AtribuirValorObj(dtRow("DESUSUARIO"), GetType(String))
                                    .fechaHoraInventario = Util.AtribuirValorObj(dtRow("FECHAHORAINVENTARIO"), GetType(DateTime))
                                    .cantidadeInventariada = Util.AtribuirValorObj(dtRow("CANTIDADINVENTARIADA"), GetType(String))
                                    .cantidadeLogica = Util.AtribuirValorObj(dtRow("CANTIDADLOGICA"), GetType(String))
                                    .Sector = New ConsultarInventarioContenedor.Sector With {.codDelegacion = Util.AtribuirValorObj(dtRow("COD_DELEGACION"), GetType(String)), _
                                                                                                                                      .codPlanta = Util.AtribuirValorObj(dtRow("COD_PLANTA"), GetType(String)), _
                                                                                                                                      .codSector = Util.AtribuirValorObj(dtRow("COD_SECTOR"), GetType(String)), _
                                                                                                                                      .desDelegacion = Util.AtribuirValorObj(dtRow("DES_DELEGACION"), GetType(String)), _
                                                                                                                                      .desPlanta = Util.AtribuirValorObj(dtRow("DES_PLANTA"), GetType(String)), _
                                                                                                                                      .desSector = Util.AtribuirValorObj(dtRow("DES_SECTOR"), GetType(String))}
                                End With
                                objRespuesta.inventarios.Add(objInventario)

                            End If

                            Dim objContenedor As New ConsultarInventarioContenedor.ContenedorRespuesta
                            Dim oidcontenedor As String = Util.AtribuirValorObj(dtRow("OID_CONTENEDOR"), GetType(String))
                            objCuentas = New List(Of ConsultarInventarioContenedor.Cuenta)
                            With objContenedor

                                .codEstadoContInventario = Util.AtribuirValorObj(dtRow("CODESTADOCONTINVENTARIO"), GetType(String))
                                .codPrecinto = Util.AtribuirValorObj(dtRow("COD_PRECINTO"), GetType(String))
                                objClientes = New ConsultarInventarioContenedor.Cliente With {.codCliente = Util.AtribuirValorObj(dtRow("COD_CLIENTE"), GetType(String)), _
                                                                                                                                      .codSubcliente = Util.AtribuirValorObj(dtRow("COD_SUBCLIENTE"), GetType(String)), _
                                                                                                                                      .codPuntoServicio = Util.AtribuirValorObj(dtRow("COD_PTO_SERVICIO"), GetType(String)), _
                                                                                                                                      .desCliente = Util.AtribuirValorObj(dtRow("DES_CLIENTE"), GetType(String)), _
                                                                                                                                      .desSubcliente = Util.AtribuirValorObj(dtRow("DES_SUBCLIENTE"), GetType(String)), _
                                                                                                                                      .desPuntoServicio = Util.AtribuirValorObj(dtRow("DES_PTO_SERVICIO"), GetType(String))}

                                objCanales = New ConsultarInventarioContenedor.Canal With {.oidCanal = Util.AtribuirValorObj(dtRow("OID_CANAL"), GetType(String)), _
                                                                                            .codCanal = Util.AtribuirValorObj(dtRow("COD_CANAL"), GetType(String)), _
                                                                                            .codSubcanal = Util.AtribuirValorObj(dtRow("COD_SUBCANAL"), GetType(String))}

                                objdetalheEfectivo = PreencheConsultarInventarioContenedorDetEfec(dsRetorno, oidcontenedor)

                                objCuentas.Add(New ConsultarInventarioContenedor.Cuenta With {.Cliente = objClientes, .Canal = objCanales, .DetalleEfectivo = objdetalheEfectivo})
                                .Cuentas = objCuentas
                                '.clientes = objClientes


                            End With
                            objRespuesta.contenedores.Add(objContenedor)
                        Next
                    End If
                End If
            End If
            Return objRespuesta
        End Function

        Private Shared Function PreencheConsultarInventarioContenedorDetEfec(dsRetorno As DataSet, oidContenedor As String) As List(Of ConsultarInventarioContenedor.DetalleEfectivo)
            Dim lstDetalleEfectivo As List(Of ConsultarInventarioContenedor.DetalleEfectivo) = Nothing
            If dsRetorno.Tables.Contains("DETEFECMP") Then
                Dim dtEfec As DataTable = dsRetorno.Tables("DETEFECMP")
                If dtEfec.Rows.Count > 0 Then
                    lstDetalleEfectivo = New List(Of ConsultarInventarioContenedor.DetalleEfectivo)
                    For Each dtRow In dtEfec.Select(String.Format("OID_CONTENEDOR = '{0}'", oidContenedor))
                        Dim objDetalleEfectivo As New ConsultarInventarioContenedor.DetalleEfectivo
                        With objDetalleEfectivo
                            .codDivisa = Util.AtribuirValorObj(dtRow("COD_ISO_DIVISA"), GetType(String))
                            .desDivisa = Util.AtribuirValorObj(dtRow("DES_DIVISA"), GetType(String))
                            .codSimbolo = Util.AtribuirValorObj(dtRow("COD_SIMBOLO"), GetType(String))
                            .codDenominacion = Util.AtribuirValorObj(dtRow("COD_DENOMINACION"), GetType(String))
                            .codCalidad = Util.AtribuirValorObj(dtRow("COD_CALIDAD"), GetType(String))
                            .disponible = Util.AtribuirValorObj(dtRow("BOL_DISPONIBLE"), GetType(Boolean))
                            .bloqueado = Util.AtribuirValorObj(dtRow("BOL_BLOQUEADO"), GetType(Boolean))
                            .cantidad = Util.AtribuirValorObj(dtRow("NEL_CANTIDAD"), GetType(Integer))
                        End With
                        lstDetalleEfectivo.Add(objDetalleEfectivo)
                    Next
                End If
            End If
            Return lstDetalleEfectivo
        End Function
#End Region

#Region "DefinirCambiarPosicionContenedor"
        Public Shared Function DefinirCambiarExtraerPosicionContenedor(Peticion As DefinirCambiarExtraerPosicionContenedor.Peticion) As DefinirCambiarExtraerPosicionContenedor.Respuesta

            Dim objRespuesta As New DefinirCambiarExtraerPosicionContenedor.Respuesta

            Try

                If Peticion IsNot Nothing Then

                    If Peticion.Sector IsNot Nothing Then

                        If Peticion.Sector.codDelegacion Is Nothing Then
                            Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_Delegacion")))
                        ElseIf Peticion.Sector.codPlanta Is Nothing Then
                            Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_Planta")))
                        ElseIf Peticion.Sector.codSector Is Nothing Then
                            Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_Sector")))
                        End If

                    End If

                    If Peticion.Contenedores IsNot Nothing AndAlso Peticion.Contenedores.Count > 0 Then

                        For Each Contenedor In Peticion.Contenedores

                            If Contenedor.Posicion.codPrecinto Is Nothing Then
                                Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_Precinto")))
                            ElseIf Contenedor.Posicion.codPosicionDestino IsNot Nothing AndAlso Contenedor.Posicion.codPosicion Is Nothing Then
                                Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("parametro_Posicion")))

                            End If

                        Next

                    End If

                    AccesoDatos.GenesisSaldos.Contenedor.DefinirCambiarExtraerPosicionContenedor(Peticion)

                    objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                    objRespuesta.MensajeError = String.Empty

                End If

            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.CodigoError = ex.Codigo
                objRespuesta.MensajeError = ex.Descricao
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString
            End Try

            Return objRespuesta

        End Function
#End Region

#Region "ConsultarSeguimientoElemento"
        Public Shared Function ConsultarSeguimientoElemento(Peticion As ConsultarSeguimientoElemento.Peticion) As ConsultarSeguimientoElemento.Respuesta

            Dim objRespuesta As New ConsultarSeguimientoElemento.Respuesta

            Try
                If Peticion.Elemento.codPrecinto Is Nothing AndAlso Peticion.Elemento.codTipoElemento Is Nothing AndAlso Peticion.Elemento.oidElemento Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(Traduzir("msg_gen_todos_campos_vazios"))
                Else
                    Dim dsCont As DataSet = AccesoDatos.GenesisSaldos.Contenedor.ConsultarSeguimientoElemento(Peticion)
                    objRespuesta = PreencheConsultarSeguimientoElemento(dsCont)

                    objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                    objRespuesta.MensajeError = String.Empty
                End If
            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.CodigoError = ex.Codigo
                objRespuesta.MensajeError = ex.Descricao
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString
            End Try

            Return objRespuesta

        End Function

        Private Shared Function PreencheConsultarSeguimientoElemento(dsRetorno As DataSet) As ConsultarSeguimientoElemento.Respuesta
            Dim objRespuesta As New ConsultarSeguimientoElemento.Respuesta
            Dim objCuentasOrigen As New List(Of ConsultarSeguimientoElemento.Cuenta)
            Dim objCuentasDestino As New List(Of ConsultarSeguimientoElemento.Cuenta)


            If dsRetorno IsNot Nothing AndAlso dsRetorno.Tables.Count > 0 Then
                If dsRetorno.Tables.Contains("ELEMENTOS") Then
                    Dim dtElementos = dsRetorno.Tables("ELEMENTOS")
                    If dtElementos IsNot Nothing AndAlso dtElementos.Rows.Count > 0 Then
                        If objRespuesta.Elementos Is Nothing Then
                            objRespuesta.Elementos = New List(Of ConsultarSeguimientoElemento.ElementoRespuesta)
                        End If
                        For Each dtRow In dtElementos.Rows

                            Dim objElemento As New ConsultarSeguimientoElemento.ElementoRespuesta
                            With objElemento
                                .codTipoElemento = Util.AtribuirValorObj(dtRow("CODTIPOELEMENTO"), GetType(String))
                                .codEstado = Util.AtribuirValorObj(dtRow("CODESTADOELEMENTO"), GetType(String))
                            End With

                            Dim oidDocumentoElemento As String = Util.AtribuirValorObj(dtRow("OID_DOCUMENTO"), GetType(String))

                            If objElemento.codTipoElemento = "C" Then
                                Dim objContenedor As New ConsultarSeguimientoElemento.ContenedorRespuesta
                                With objContenedor
                                    .oidContenedor = Util.AtribuirValorObj(dtRow("OID_CONTENEDOR"), GetType(String))
                                    .codTipoContenedor = Util.AtribuirValorObj(dtRow("COD_TIPO_CONTENEDOR"), GetType(String))
                                    .desTipoContenedor = Util.AtribuirValorObj(dtRow("DES_TIPO_CONTENEDOR"), GetType(String))
                                    .packModular = Util.AtribuirValorObj(dtRow("BOL_PACK_MODULAR"), GetType(String))
                                    .fechaHoraArmado = Util.AtribuirValorObj(dtRow("FEC_ARMADO"), GetType(String))
                                    ' .agrupaElementos = Util.AtribuirValorObj(dtRow("COD_PRECINTO"), GetType(String))

                                    If dsRetorno.Tables.Contains("PRECINTOS") Then
                                        Dim rowsPrecintos = dsRetorno.Tables("PRECINTOS"). _
                                                                                Select(String.Format("OID_CONTENEDOR = '{0}'", objContenedor.oidContenedor))

                                        If rowsPrecintos IsNot Nothing AndAlso rowsPrecintos.Count > 0 Then
                                            .Precintos = New ObservableCollection(Of String)
                                            For indice = 0 To rowsPrecintos.Length - 1
                                                .Precintos.Add(Util.AtribuirValorObj(rowsPrecintos(indice)("COD_PRECINTO"), GetType(String)))
                                            Next
                                        End If

                                    End If
                                End With
                                objElemento.Contenedor = objContenedor

                            ElseIf objElemento.codTipoElemento = "R" OrElse objElemento.codTipoElemento = "B" Then
                                Dim objRemesa As New ConsultarSeguimientoElemento.Remesa
                                Dim objBulto As New ConsultarSeguimientoElemento.Bulto

                                With objBulto
                                    .codigoBolsa = Util.AtribuirValorObj(dtRow("COD_BOLSA"), GetType(String))
                                    .codPrecinto = Util.AtribuirValorObj(dtRow("COD_PRECINTO"), GetType(String))
                                    .fechaHoraCriacion = Util.AtribuirValorObj(dtRow("GMT_CREACION"), GetType(DateTime))
                                    If objElemento.codTipoElemento = "B" Then
                                        .tipoServicio = Util.AtribuirValorObj(dtRow("TIPO_SERVICIO"), GetType(String))
                                    End If
                                End With

                                With objRemesa
                                    .codigoExterno = Util.AtribuirValorObj(dtRow("COD_EXTERNO"), GetType(String))
                                    .codRuta = Util.AtribuirValorObj(dtRow("COD_RUTA"), GetType(String))
                                    .fechaHoraTransporte = Util.AtribuirValorObj(dtRow("FYH_TRANSPORTE"), GetType(String))
                                    .fechaHoraCriacion = Util.AtribuirValorObj(dtRow("GMT_CREACION"), GetType(DateTime))
                                    .Bulto = objBulto
                                End With

                                objElemento.Remesa = objRemesa

                            End If

                            'busca documentos do elemento em questao
                            If dsRetorno.Tables.Contains("DOCUMENTOS") Then
                                Dim dtDocumentos = dsRetorno.Tables("DOCUMENTOS")

                                If dtDocumentos IsNot Nothing AndAlso dtDocumentos.Rows.Count > 0 Then
                                    Dim objDocumentos As New List(Of ConsultarSeguimientoElemento.Documento)
                                    For Each dtRowdocumento In dtDocumentos.Select(String.Format("OID_DOCUMENTO = '{0}'", oidDocumentoElemento))
                                        If dsRetorno.Tables.Contains("CUENTAS") Then
                                            For Each dtRowCuentas In dsRetorno.Tables("CUENTAS").Select(String.Format("OID_CUENTA = '{0}'", Util.AtribuirValorObj(dtRowdocumento("OID_CUENTA_ORIGEN"), GetType(String))))
                                                If Util.AtribuirValorObj(dtRowCuentas("TIPOCUENTA"), GetType(String)) = "ORIGEM" Then

                                                    Dim CuentaOrigen = New ConsultarSeguimientoElemento.Cuenta
                                                    Dim Sector = New ConsultarSeguimientoElemento.Sector

                                                    With Sector
                                                        .codDelegacion = Util.AtribuirValorObj(dtRowCuentas("COD_DELEGACION"), GetType(String))
                                                        .codPlanta = Util.AtribuirValorObj(dtRowCuentas("COD_PLANTA"), GetType(String))
                                                        .codSector = Util.AtribuirValorObj(dtRowCuentas("COD_SECTOR"), GetType(String))
                                                        .desDelegacion = Util.AtribuirValorObj(dtRowCuentas("DES_DELEGACION"), GetType(String))
                                                        .desPlanta = Util.AtribuirValorObj(dtRowCuentas("DES_PLANTA"), GetType(String))
                                                        .desSector = Util.AtribuirValorObj(dtRowCuentas("DES_SECTOR"), GetType(String))
                                                    End With

                                                    CuentaOrigen.Sector = Sector
                                                    Dim Cliente = New ConsultarSeguimientoElemento.Cliente

                                                    With Cliente
                                                        .codCliente = Util.AtribuirValorObj(dtRowCuentas("COD_CLIENTE"), GetType(String))
                                                        .codSubCliente = Util.AtribuirValorObj(dtRowCuentas("COD_SUBCLIENTE"), GetType(String))
                                                        .codPtoServicio = Util.AtribuirValorObj(dtRowCuentas("COD_PTO_SERVICIO"), GetType(String))
                                                        .desCliente = Util.AtribuirValorObj(dtRowCuentas("DES_CLIENTE"), GetType(String))
                                                        .desSubCliente = Util.AtribuirValorObj(dtRowCuentas("DES_SUBCLIENTE"), GetType(String))
                                                        .desPtoServicio = Util.AtribuirValorObj(dtRowCuentas("DES_PTO_SERVICIO"), GetType(String))
                                                    End With

                                                    CuentaOrigen.Cliente = Cliente

                                                    Dim Canal = New ConsultarSeguimientoElemento.Canal
                                                    With Canal
                                                        .codCanal = Util.AtribuirValorObj(dtRowCuentas("COD_CANAL"), GetType(String))
                                                        .codSubcanal = Util.AtribuirValorObj(dtRowCuentas("COD_SUBCANAL"), GetType(String))
                                                        .desCanal = Util.AtribuirValorObj(dtRowCuentas("DES_CANAL"), GetType(String))
                                                        .desSubcanal = Util.AtribuirValorObj(dtRowCuentas("DES_SUBCANAL"), GetType(String))
                                                    End With

                                                    CuentaOrigen.Canal = Canal

                                                    objCuentasOrigen.Add(CuentaOrigen)

                                                ElseIf Util.AtribuirValorObj(dtRowCuentas("TIPOCUENTA"), GetType(String)) = "DESTINO" Then

                                                    Dim CuentaDestino = New ConsultarSeguimientoElemento.Cuenta
                                                    Dim Sector = New ConsultarSeguimientoElemento.Sector

                                                    With Sector
                                                        .codDelegacion = Util.AtribuirValorObj(dtRowCuentas("COD_DELEGACION"), GetType(String))
                                                        .codPlanta = Util.AtribuirValorObj(dtRowCuentas("COD_PLANTA"), GetType(String))
                                                        .codSector = Util.AtribuirValorObj(dtRowCuentas("COD_SECTOR"), GetType(String))
                                                        .desDelegacion = Util.AtribuirValorObj(dtRowCuentas("DES_DELEGACION"), GetType(String))
                                                        .desPlanta = Util.AtribuirValorObj(dtRowCuentas("DES_PLANTA"), GetType(String))
                                                        .desSector = Util.AtribuirValorObj(dtRowCuentas("DES_SECTOR"), GetType(String))
                                                    End With

                                                    CuentaDestino.Sector = Sector
                                                    Dim Cliente = New ConsultarSeguimientoElemento.Cliente

                                                    With Cliente
                                                        .codCliente = Util.AtribuirValorObj(dtRowCuentas("COD_CLIENTE"), GetType(String))
                                                        .codSubCliente = Util.AtribuirValorObj(dtRowCuentas("COD_SUBCLIENTE"), GetType(String))
                                                        .codPtoServicio = Util.AtribuirValorObj(dtRowCuentas("COD_PTO_SERVICIO"), GetType(String))
                                                        .desCliente = Util.AtribuirValorObj(dtRowCuentas("DES_CLIENTE"), GetType(String))
                                                        .desSubCliente = Util.AtribuirValorObj(dtRowCuentas("DES_SUBCLIENTE"), GetType(String))
                                                        .desPtoServicio = Util.AtribuirValorObj(dtRowCuentas("DES_PTO_SERVICIO"), GetType(String))
                                                    End With

                                                    CuentaDestino.Cliente = Cliente

                                                    Dim Canal = New ConsultarSeguimientoElemento.Canal
                                                    With Canal
                                                        .codCanal = Util.AtribuirValorObj(dtRowCuentas("COD_CANAL"), GetType(String))
                                                        .codSubcanal = Util.AtribuirValorObj(dtRowCuentas("COD_SUBCANAL"), GetType(String))
                                                        .desCanal = Util.AtribuirValorObj(dtRowCuentas("DES_CANAL"), GetType(String))
                                                        .desSubcanal = Util.AtribuirValorObj(dtRowCuentas("DES_SUBCANAL"), GetType(String))
                                                    End With

                                                    CuentaDestino.Canal = Canal
                                                    objCuentasDestino.Add(CuentaDestino)
                                                End If
                                            Next
                                        End If

                                        Dim objDocumento As New ConsultarSeguimientoElemento.Documento
                                        With objDocumento
                                            .oidDocumento = Util.AtribuirValorObj(dtRowdocumento("OID_DOCUMENTO"), GetType(String))
                                            .esAgrupador = Util.AtribuirValorObj(dtRowdocumento("ESAGRUPADOR"), GetType(String))
                                            .codFormulario = Util.AtribuirValorObj(dtRowdocumento("COD_FORMULARIO"), GetType(String))
                                            .desFormulario = Util.AtribuirValorObj(dtRowdocumento("DES_FORMULARIO"), GetType(String))
                                            .codEstado = Util.AtribuirValorObj(dtRowdocumento("COD_ESTADO"), GetType(String))
                                            .fechaModificacion = Util.AtribuirValorObj(dtRowdocumento("FECHAMODIFICACION"), GetType(String))

                                            .desUsuarioModificacion = Util.AtribuirValorObj(dtRowdocumento("DES_USUARIO_MODIFICACION"), GetType(String))
                                            .CuentasOrigen = objCuentasOrigen
                                            .CuentasDestino = objCuentasDestino

                                            If dsRetorno.Tables.Contains("ACIONES") Then
                                                Dim rowsAcciones = dsRetorno.Tables("ACIONES"). _
                                                                                        Select(String.Format("OID_DOCUMENTO = '{0}'", objDocumento.oidDocumento))

                                                If rowsAcciones IsNot Nothing AndAlso rowsAcciones.Count > 0 Then
                                                    .Acciones = New ObservableCollection(Of String)
                                                    For indice = 0 To rowsAcciones.Length - 1
                                                        .Acciones.Add(Util.AtribuirValorObj(rowsAcciones(indice)("COD_CARACT_FORMULARIO"), GetType(String)))
                                                    Next
                                                End If

                                            End If
                                        End With
                                        objDocumentos.Add(objDocumento)
                                    Next
                                    objElemento.Documentos = objDocumentos
                                End If
                            End If

                            objRespuesta.Elementos.Add(objElemento)
                        Next
                    End If
                End If
            End If
            Return objRespuesta
        End Function
#End Region

    End Class

End Namespace