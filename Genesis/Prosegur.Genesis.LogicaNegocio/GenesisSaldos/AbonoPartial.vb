Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Transactions
Imports Prosegur.Genesis.AccesoDatos
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports System.IO
Imports System.IO.Compression
Imports CommonConstantes = Prosegur.Genesis.Comon.Constantes

Namespace GenesisSaldos
    Partial Public Class Abono
        Private Shared Sub GenerarDocumentoAgrupadorParaAbono(objAbono As Clases.Abono.Abono, sectorSelecionado As Clases.Sector, ByRef transaccion As DataBaseHelper.Transaccion)

            Try

                Dim Formulario = ObtenerFormularioDocPases(sectorSelecionado.Identificador, objAbono.UsuarioCreacion, transaccion)
                Dim grupoDocumento = MaestroGrupoDocumentos.CrearGrupoDocumentos(Formulario)
                Dim Divisas = New ObservableCollection(Of Clases.Divisa)

                ValidarGrupoTerminosIACEnFormulario(Formulario, grupoDocumento)

                ObtenerDivisasAbonoValor(Divisas, objAbono, transaccion)

                For Each abonoValor As Clases.Abono.AbonoValor In objAbono.AbonosValor

                    Dim documento = MaestroDocumentos.CrearDocumento(Formulario)

                    CargarTerminosAbono(documento, abonoValor)
                    CargarTerminosInternosDocumentosIndividualesAbono(documento, abonoValor, objAbono)
                    CargarCuentasOrigenEDestino(documento, abonoValor, objAbono, sectorSelecionado, transaccion)
                    CargarDivisa(documento, abonoValor, Divisas)

                    documento.UsuarioCreacion = objAbono.UsuarioCreacion
                    documento.FechaHoraCreacion = objAbono.FechaHoraCreacion
                    documento.UsuarioModificacion = objAbono.UsuarioModificacion
                    documento.FechaHoraModificacion = objAbono.FechaHoraModificacion
                    documento.FechaHoraPlanificacionCertificacion = objAbono.FechaHoraCreacion
                    documento.FechaHoraGestion = documento.FechaHoraPlanificacionCertificacion

                    ' remove valores de divisas nulos ou iguais a zero
                    ' no nível do documento e do elemento relacionado a ele (incluindo todos os níveis - remesa, bulto, parcial)
                    Comon.Util.BorrarItemsDivisaSinValoresCantidades(documento)

                    grupoDocumento.Documentos.Add(documento)
                    abonoValor.AbonoSaldo.IdentificadorDocumento = documento.Identificador
                Next

                grupoDocumento.CuentaOrigen = grupoDocumento.Documentos(0).CuentaOrigen
                grupoDocumento.CuentaDestino = grupoDocumento.Documentos(0).CuentaDestino
                grupoDocumento.UsuarioCreacion = objAbono.UsuarioCreacion
                grupoDocumento.FechaHoraCreacion = objAbono.FechaHoraCreacion
                grupoDocumento.UsuarioModificacion = objAbono.UsuarioModificacion
                grupoDocumento.FechaHoraModificacion = objAbono.FechaHoraModificacion


                Dim termino = grupoDocumento.GrupoTerminosIAC.TerminosIAC.FirstOrDefault(Function(t) t.Codigo = CommonConstantes.COD_TERMINO_ABONO_CODIGO_PROCESO)
                termino.Valor = objAbono.Codigo

                MaestroGrupoDocumentos.GuardarGrupoDocumentos(grupoDocumento, False, True, Nothing, Nothing, transaccion)

                Dim dic = objAbono.AbonosValor _
                          .Select(Function(a) New With {.key = a.AbonoSaldo.Identificador, .value = a.AbonoSaldo.IdentificadorDocumento}) _
                          .ToDictionary(Function(d) d.key, Function(d) d.value)

                AccesoDatos.GenesisSaldos.Abono.Saldo.ActualizarIdentificadorDocumento(dic, transaccion)

                objAbono.IdentificadorGrupoDocumento = grupoDocumento.Identificador
                AccesoDatos.GenesisSaldos.Abono.Comun.ActualizarIdentificadorGrupoDocumento(objAbono, transaccion)


            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Sub

        Private Shared Sub CargarCuentasOrigenEDestino(ByRef documento As Clases.Documento,
                                                       abonoValor As Clases.Abono.AbonoValor,
                                                       objAbono As Clases.Abono.Abono,
                                                       sectorSelecionado As Clases.Sector,
                                                       ByRef transaccion As DataBaseHelper.Transaccion)

            ' Cuenta Origen
            documento.CuentaOrigen = New Clases.Cuenta()

            Dim idenficadorSector As String = Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerIdentificadorSector(abonoValor.AbonoSaldo.SectoresDocumento(0).Codigo, objAbono.Delegacion.Codigo, transaccion)

            documento.CuentaOrigen.Sector = New Clases.Sector With {.Identificador = idenficadorSector,
                                                                    .Codigo = abonoValor.AbonoSaldo.SectoresDocumento(0).Codigo,
                                                                    .Delegacion = New Clases.Delegacion With {.Codigo = objAbono.Delegacion.Codigo},
                                                                    .Planta = New Clases.Planta With {.Codigo = sectorSelecionado.Planta.Codigo}}

            If objAbono.TipoAbono = Enumeradores.TipoAbono.Pedido Then
                documento.CuentaOrigen.Cliente = New Clases.Cliente With {.Codigo = objAbono.Bancos(0).Codigo}
            Else
                documento.CuentaOrigen.Cliente = New Clases.Cliente With {.Codigo = abonoValor.Cliente.Codigo}
            End If

            If abonoValor.SubCliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(abonoValor.SubCliente.Codigo) Then
                documento.CuentaOrigen.SubCliente = New Clases.SubCliente With {.Codigo = abonoValor.SubCliente.Codigo}
            End If
            If abonoValor.PtoServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(abonoValor.PtoServicio.Codigo) Then
                documento.CuentaOrigen.PuntoServicio = New Clases.PuntoServicio With {.Codigo = abonoValor.PtoServicio.Codigo}
            End If

            Dim snap = objAbono.SnapshotsAbonoSaldo.Where(Function(s) s.AbonoSaldo.IdentificadorSnapshot = abonoValor.AbonoSaldo.IdentificadorSnapshot).FirstOrDefault()


            If abonoValor.AbonoSaldo.CanalesDocumento IsNot Nothing AndAlso abonoValor.AbonoSaldo.CanalesDocumento.Count > 0 Then

                abonoValor.AbonoSaldo.CanalesDocumento(0).Codigo = snap.AbonoSaldo.Canal.Codigo
            Else
                abonoValor.AbonoSaldo.CanalesDocumento = New List(Of Clases.Abono.AbonoInformacion)
                abonoValor.AbonoSaldo.CanalesDocumento.Add(New Clases.Abono.AbonoInformacion With { _
                                                           .Codigo = snap.AbonoSaldo.Canal.Codigo})
            End If

            If abonoValor.AbonoSaldo.SubCanalesDocumento IsNot Nothing AndAlso abonoValor.AbonoSaldo.SubCanalesDocumento.Count > 0 Then
                abonoValor.AbonoSaldo.SubCanalesDocumento(0).Codigo = snap.AbonoSaldo.SubCanal.Codigo
            Else
                abonoValor.AbonoSaldo.SubCanalesDocumento = New List(Of Clases.Abono.AbonoInformacion)
                abonoValor.AbonoSaldo.SubCanalesDocumento.Add(New Clases.Abono.AbonoInformacion With { _
                                                              .Codigo = snap.AbonoSaldo.SubCanal.Codigo})
            End If

            documento.CuentaOrigen.Canal = New Clases.Canal With {.Codigo = abonoValor.AbonoSaldo.CanalesDocumento(0).Codigo}
            documento.CuentaOrigen.SubCanal = New Clases.SubCanal With {.Codigo = abonoValor.AbonoSaldo.SubCanalesDocumento(0).Codigo}
            documento.CuentaOrigen.UsuarioModificacion = abonoValor.UsuarioCreacion
            documento.CuentaOrigen.TipoCuenta = Comon.Enumeradores.TipoCuenta.Movimiento

            ' CuentaDestino
            documento.CuentaDestino = New Clases.Cuenta()

            documento.CuentaDestino.Sector = New Clases.Sector With {.Identificador = idenficadorSector,
                                                                     .Codigo = abonoValor.AbonoSaldo.SectoresDocumento(0).Codigo,
                                                                     .Delegacion = New Clases.Delegacion With {.Codigo = objAbono.Delegacion.Codigo},
                                                                    .Planta = New Clases.Planta With {.Codigo = sectorSelecionado.Planta.Codigo}}

            If objAbono.TipoAbono = Enumeradores.TipoAbono.Saldos Then
                documento.CuentaDestino.Cliente = New Clases.Cliente With {.Codigo = objAbono.Bancos(0).Codigo}
            Else
                documento.CuentaDestino.Cliente = New Clases.Cliente With {.Codigo = abonoValor.BancoCuenta.Codigo}
            End If

            documento.CuentaDestino.Canal = New Clases.Canal With {.Codigo = abonoValor.AbonoSaldo.CanalesDocumento(0).Codigo}
            documento.CuentaDestino.SubCanal = New Clases.SubCanal With {.Codigo = abonoValor.AbonoSaldo.SubCanalesDocumento(0).Codigo}
            documento.CuentaDestino.UsuarioModificacion = abonoValor.UsuarioCreacion
            documento.CuentaDestino.TipoCuenta = Comon.Enumeradores.TipoCuenta.Movimiento

        End Sub

        Private Shared Sub CargarDivisa(ByRef documento As Clases.Documento, abonoValor As Clases.Abono.AbonoValor, divisas As ObservableCollection(Of Clases.Divisa))

            Dim divisaAbonoValor = divisas.Where(Function(e) e.Identificador = abonoValor.Divisa.Identificador).FirstOrDefault()
            If divisaAbonoValor IsNot Nothing Then
                Dim divisaDocto = divisaAbonoValor.Clonar()
                'Carrega as denominações
                For Each efect In abonoValor.Divisa.ListaEfectivo
                    If (efect.Cantidad <> 0) Then
                        Dim denominacionDocto = divisaDocto.Denominaciones.FirstOrDefault(Function(d) d.Codigo = efect.Codigo)
                        If denominacionDocto IsNot Nothing Then
                            Dim valorDeno = New Clases.ValorDenominacion()
                            valorDeno.Cantidad = efect.Cantidad
                            valorDeno.Importe = efect.Importe
                            valorDeno.TipoValor = Enumeradores.TipoValor.NoDefinido
                            If (denominacionDocto.ValorDenominacion Is Nothing) Then denominacionDocto.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)()
                            denominacionDocto.ValorDenominacion.Add(valorDeno)
                        End If
                    End If
                Next
                'Carrega os medio pagos
                For Each mp In abonoValor.Divisa.ListaMedioPago
                    If (mp.Cantidad <> 0) Then
                        Dim medioPagtoDocto = divisaDocto.MediosPago.FirstOrDefault(Function(d) d.Codigo = mp.Codigo AndAlso d.Tipo = mp.TipoMedioPago)
                        If medioPagtoDocto IsNot Nothing Then
                            Dim valorMp = New Clases.ValorMedioPago()
                            valorMp.Cantidad = mp.Cantidad
                            valorMp.Importe = mp.Importe
                            valorMp.TipoValor = Enumeradores.TipoValor.NoDefinido
                            If (medioPagtoDocto.Valores Is Nothing) Then medioPagtoDocto.Valores = New ObservableCollection(Of Clases.ValorMedioPago)()
                            medioPagtoDocto.Valores.Add(valorMp)
                        End If
                    End If
                Next
                'Carrega totais cheque
                If abonoValor.Divisa.Totales.TotalCheque <> 0 Then
                    Dim total = New Clases.ValorTipoMedioPago()
                    total.Importe = abonoValor.Divisa.Totales.TotalCheque
                    total.TipoMedioPago = Enumeradores.TipoMedioPago.Cheque
                    total.TipoValor = Enumeradores.TipoValor.NoDefinido
                    If (divisaDocto.ValoresTotalesTipoMedioPago Is Nothing) Then divisaDocto.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)()
                    divisaDocto.ValoresTotalesTipoMedioPago.Add(total)
                End If
                'Carrega totais OtroValor
                If abonoValor.Divisa.Totales.TotalOtroValor <> 0 Then
                    Dim total = New Clases.ValorTipoMedioPago()
                    total.Importe = abonoValor.Divisa.Totales.TotalOtroValor
                    total.TipoMedioPago = Enumeradores.TipoMedioPago.OtroValor
                    total.TipoValor = Enumeradores.TipoValor.NoDefinido
                    If (divisaDocto.ValoresTotalesTipoMedioPago Is Nothing) Then divisaDocto.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)()
                    divisaDocto.ValoresTotalesTipoMedioPago.Add(total)
                End If
                'Carrega totais tarjeta
                If abonoValor.Divisa.Totales.TotalTarjeta <> 0 Then
                    Dim total = New Clases.ValorTipoMedioPago()
                    total.Importe = abonoValor.Divisa.Totales.TotalTarjeta
                    total.TipoMedioPago = Enumeradores.TipoMedioPago.Tarjeta
                    total.TipoValor = Enumeradores.TipoValor.NoDefinido
                    If (divisaDocto.ValoresTotalesTipoMedioPago Is Nothing) Then divisaDocto.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)()
                    divisaDocto.ValoresTotalesTipoMedioPago.Add(total)
                End If
                'Carrega totais ticket
                If abonoValor.Divisa.Totales.TotalTicket <> 0 Then
                    Dim total = New Clases.ValorTipoMedioPago()
                    total.Importe = abonoValor.Divisa.Totales.TotalTicket
                    total.TipoMedioPago = Enumeradores.TipoMedioPago.Ticket
                    total.TipoValor = Enumeradores.TipoValor.NoDefinido
                    If (divisaDocto.ValoresTotalesTipoMedioPago Is Nothing) Then divisaDocto.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)()
                    divisaDocto.ValoresTotalesTipoMedioPago.Add(total)
                End If
                'Carrega totais efectivo
                If abonoValor.Divisa.Totales.TotalEfectivo <> 0 Then
                    Dim total = New Clases.ValorEfectivo()
                    total.Importe = abonoValor.Divisa.Totales.TotalEfectivo
                    total.TipoValor = Enumeradores.TipoValor.NoDefinido
                    total.TipoDetalleEfectivo = RecuperarEnum(Of Enumeradores.TipoDetalleEfectivo)(abonoValor.Divisa.Totales.CodigoTipoEfectivoTotal)
                    If (divisaDocto.ValoresTotalesEfectivo Is Nothing) Then divisaDocto.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)()
                    divisaDocto.ValoresTotalesEfectivo.Add(total)
                End If

                documento.Divisas.Add(divisaDocto)
            End If


        End Sub

        Private Shared Sub CargarTerminosAbono(ByRef documento As Clases.Documento, abonoValor As Clases.Abono.AbonoValor)

            For Each Termino As Clases.TerminoIAC In documento.GrupoTerminosIAC.TerminosIAC

                Dim TerminoAbono = abonoValor.AbonoSaldo.ListaTerminoIAC.Find(Function(e) e.Codigo = Termino.Codigo)

                If (TerminoAbono IsNot Nothing) Then
                    Termino.Valor = TerminoAbono.Valor
                End If
            Next

        End Sub

        Private Shared Sub CargarTerminosInternosDocumentosIndividualesAbono(ByRef documento As Clases.Documento, abonoValor As Clases.Abono.AbonoValor, abono As Clases.Abono.Abono)
            Try
                Dim AbonoCodigoProceso = documento.GrupoTerminosIAC.TerminosIAC.Where(Function(t) t.Codigo = CommonConstantes.COD_TERMINO_ABONO_CODIGO_PROCESO).ToList()
                If (AbonoCodigoProceso.Count > 0) Then
                    For Each termino In AbonoCodigoProceso
                        termino.Valor = abono.Codigo
                    Next
                End If

                Dim AbonoDocumento = documento.GrupoTerminosIAC.TerminosIAC.Where(Function(t) t.Codigo = CommonConstantes.COD_TERMINO_ABONO_DOCUMENTO).ToList()
                If (AbonoDocumento.Count > 0) Then
                    For Each termino In AbonoDocumento
                        termino.Valor = abonoValor.Cuenta.CodigoDocumento
                    Next
                End If

                Dim AbonoNumeroCuenta = documento.GrupoTerminosIAC.TerminosIAC.Where(Function(t) t.Codigo = CommonConstantes.COD_TERMINO_ABONO_NUMERO_CUENTA).ToList()
                If (AbonoNumeroCuenta.Count > 0) Then
                    For Each termino In AbonoNumeroCuenta
                        termino.Valor = abonoValor.Cuenta.CodigoCuentaBancaria
                    Next
                End If

                Dim AbonoObservaciones = documento.GrupoTerminosIAC.TerminosIAC.Where(Function(t) t.Codigo = CommonConstantes.COD_TERMINO_ABONO_OBSERVACIONES).ToList()
                If (AbonoObservaciones.Count > 0) Then
                    For Each termino In AbonoObservaciones
                        termino.Valor = abonoValor.Observaciones
                    Next
                End If

                Dim AbonoTipoCuenta = documento.GrupoTerminosIAC.TerminosIAC.Where(Function(t) t.Codigo = CommonConstantes.COD_TERMINO_ABONO_TIPO_CUENTA).ToList()
                If (AbonoTipoCuenta.Count > 0) Then
                    For Each termino In AbonoTipoCuenta
                        termino.Valor = abonoValor.Cuenta.CodigoTipoCuentaBancaria
                    Next
                End If

                Dim AbonoTitularidad = documento.GrupoTerminosIAC.TerminosIAC.Where(Function(t) t.Codigo = CommonConstantes.COD_TERMINO_ABONO_TITULARIDAD).ToList()
                If (AbonoTitularidad.Count > 0) Then
                    For Each termino In AbonoTitularidad
                        termino.Valor = abonoValor.Cuenta.DescripcionTitularidad
                    Next
                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Sub

        Private Shared Sub ValidarGrupoTerminosIACEnFormulario(formulario As Clases.Formulario, grupoDocumento As Clases.GrupoDocumentos)
            If formulario.GrupoTerminosIACGrupo IsNot Nothing AndAlso formulario.GrupoTerminosIACIndividual IsNot Nothing Then

                If formulario.GrupoTerminosIACGrupo.TerminosIAC.Where(Function(e) e.Codigo _
                        = CommonConstantes.COD_TERMINO_ABONO_CODIGO_PROCESO).FirstOrDefault() Is Nothing Then

                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_terminos_de_abono_no_configurados_en_formulario_de_passes_agrupador"), _
                                                                       formulario.Descripcion, formulario.Codigo))

                ElseIf formulario.GrupoTerminosIACIndividual.TerminosIAC.Where(Function(e) e.Codigo _
                        = CommonConstantes.COD_TERMINO_ABONO_CODIGO_PROCESO).FirstOrDefault() Is Nothing OrElse _
                    formulario.GrupoTerminosIACIndividual.TerminosIAC.Where(Function(e) e.Codigo _
                        = CommonConstantes.COD_TERMINO_ABONO_DOCUMENTO).FirstOrDefault() Is Nothing OrElse _
                    formulario.GrupoTerminosIACIndividual.TerminosIAC.Where(Function(e) e.Codigo _
                        = CommonConstantes.COD_TERMINO_ABONO_NUMERO_CUENTA).FirstOrDefault() Is Nothing OrElse _
                    formulario.GrupoTerminosIACIndividual.TerminosIAC.Where(Function(e) e.Codigo _
                        = CommonConstantes.COD_TERMINO_ABONO_OBSERVACIONES).FirstOrDefault() Is Nothing OrElse _
                    formulario.GrupoTerminosIACIndividual.TerminosIAC.Where(Function(e) e.Codigo _
                        = CommonConstantes.COD_TERMINO_ABONO_TIPO_CUENTA).FirstOrDefault() Is Nothing OrElse _
                    formulario.GrupoTerminosIACIndividual.TerminosIAC.Where(Function(e) e.Codigo _
                        = CommonConstantes.COD_TERMINO_ABONO_TITULARIDAD).FirstOrDefault() Is Nothing Then

                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_terminos_de_abono_no_configurados_en_formulario_de_passes_individual"), _
                                                                       formulario.Descripcion, formulario.Codigo))
                End If

            End If
        End Sub

        Private Shared Sub ObtenerDivisasAbonoValor(ByRef divisas As ObservableCollection(Of Clases.Divisa), objAbono As Clases.Abono.Abono, transaccion As DataBaseHelper.Transaccion)

            Dim CodigosDivisas = New List(Of String)

            For Each abonoValor As Clases.Abono.AbonoValor In objAbono.AbonosValor
                CodigosDivisas.Add(abonoValor.Divisa.CodigoISO)
            Next

            divisas = AccesoDatos.Genesis.Divisas.ObtenerDivisasPorCodigosConTransaccion(CodigosDivisas, transaccion)

        End Sub

    End Class
End Namespace
