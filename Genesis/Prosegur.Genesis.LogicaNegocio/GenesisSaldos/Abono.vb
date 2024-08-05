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
Imports Prosegur.Genesis.Comon.Clases.Abono

Namespace GenesisSaldos
    Partial Public Class Abono

        Public Shared Function RecuperarAbonos(filtro As Clases.Transferencias.FiltroConsultaAbono, delegacion As Clases.Delegacion) As List(Of Clases.Abono.Abono)

            Dim abonos As List(Of Clases.Abono.Abono)

            Try

                If filtro Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Filtro"))
                End If

                If String.IsNullOrEmpty(filtro.IdentificadorDelegacion) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "IdentificadorDelegacion"))
                End If

                abonos = AccesoDatos.GenesisSaldos.Abono.Comun.RecuperarAbonos(filtro)

                'Recupera os reportes dos abonos
                If abonos IsNot Nothing AndAlso abonos.Count > 0 Then
                    'Ajusta GMT
                    For Each abono In abonos
                        abono.FechaHoraCreacion = abono.FechaHoraCreacion.QuieroExibirEstaFechaEnLaPatalla(delegacion)
                        abono.FechaHoraModificacion = abono.FechaHoraModificacion.QuieroExibirEstaFechaEnLaPatalla(delegacion)
                        abono.Fecha = abono.Fecha.QuieroExibirEstaFechaEnLaPatalla(delegacion)
                        abono.FechaFormatada = String.Format(abono.Fecha, "dd/MM/yyyy hh:mm:ss")
                    Next
                    RecuperarReportesGenerados(abonos)
                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            Return abonos

        End Function

        Public Shared Sub CambiarEstadoAbono(abono As Clases.Abono.Abono)

            Try

                If abono Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Abono"))
                End If

                If String.IsNullOrEmpty(abono.Identificador) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Identificador"))
                End If

                If String.IsNullOrEmpty(abono.UsuarioModificacion) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Usuario Modificacion"))
                End If

                AccesoDatos.GenesisSaldos.Abono.Comun.CambiarEstadoAbono(abono.Identificador, abono.CodigoEstado, abono.UsuarioModificacion)

            Catch ex As Exception
                Throw New Exception(ex.Message)

            End Try

        End Sub

        ''' <summary>
        ''' Recuperar os arquivos relacionados aos abonos e retorna um arquivo zip
        ''' </summary>
        ''' <param name="abonos">Lista de Abonos</param>
        ''' <remarks></remarks>
        Public Shared Sub RecuperarReportesGenerados(ByRef abonos As List(Of Clases.Abono.Abono))

            Try

                If abonos Is Nothing OrElse abonos.Count = 0 Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Identificadores Abonos"))
                End If

                Dim objTipoCliente As Comon.Clases.TipoCliente = LogicaNegocio.Genesis.TipoCliente.RecuperarTipoCliente(Comon.Constantes.CODIGO_TIPO_CLIENTE_BANCO)

                If objTipoCliente IsNot Nothing Then

                    Dim objTipoReporte As New List(Of Enumeradores.TipoReporte)

                    objTipoReporte.Add(Enumeradores.TipoReporte.AbonoExportacion)
                    objTipoReporte.Add(Enumeradores.TipoReporte.AbonoVisualizacion)

                    For Each Abono In abonos

                        If Abono.Bancos IsNot Nothing AndAlso Abono.Bancos.Count > 0 AndAlso Not String.IsNullOrEmpty(Abono.Bancos(0).Identificador) Then

                            Dim objConfigReporte As List(Of Comon.Clases.ConfiguracionReporte) = FormulariosCertificados.ObtenerConfiguracionesReporte(Abono.Bancos(0).Identificador,
                                                                                                                                               objTipoCliente.Identificador,
                                                                                                                                               objTipoReporte)
                            If objConfigReporte IsNot Nothing AndAlso objConfigReporte.Count > 0 Then

                                For Each cr In objConfigReporte

                                    If cr.ParametrosReporte IsNot Nothing AndAlso cr.ParametrosReporte.Count > 0 Then

                                        For Each pr In cr.ParametrosReporte

                                            Select Case pr.Codigo

                                                Case Comon.Constantes.CODIGO_PARAMETRO_REPORTE_CODIGO_PROCESO_ABONO
                                                    pr.DescripcionValor = Abono.Codigo

                                                Case Comon.Constantes.CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_ABONO
                                                    pr.DescripcionValor = Abono.Identificador

                                            End Select

                                        Next
                                    End If

                                    Dim _DatosReporte As List(Of Clases.Abono.ReporteAbono) = AccesoDatos.GenesisSaldos.ResultadoReporte.ObtenerResultadosReportesAbono(cr.Identificador, cr.ParametrosReporte)

                                    If _DatosReporte IsNot Nothing AndAlso _DatosReporte.Count > 0 Then
                                        If Abono.DatosReporte Is Nothing Then Abono.DatosReporte = New List(Of Clases.Abono.ReporteAbono)
                                        Abono.DatosReporte.AddRange(_DatosReporte)
                                    End If
                                Next
                            End If
                        End If
                    Next

                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Sub

        Public Shared Sub GrabarAbono(ByRef objAbono As Clases.Abono.Abono,
                                      EsGrabarYFinalizar As Boolean,
                                      usuario As String,
                                      sectorSelecionado As Clases.Sector,
                                      Optional ValorParametroFormatoCodigoProcesoAbono As String = "")

            Try

                Dim transaccion As New DataBaseHelper.Transaccion

                Try
                    transaccion.IniciarTransaccion = True

                    objAbono.FechaHoraModificacion = DateTime.Now
                    objAbono.UsuarioModificacion = usuario
                    objAbono.CodigoEstado = If(EsGrabarYFinalizar, Enumeradores.EstadoAbono.Procesado, Enumeradores.EstadoAbono.EnCurso)

                    ValidarValoresGrabarAbono(objAbono)

                    If EsGrabarYFinalizar Then

                        Dim ParametrosGeneracionCodigo As New Dictionary(Of String, String)

                        ParametrosGeneracionCodigo.Add(Comon.Constantes.CODIGO_PARAMETRO_ABONO_CODIGO_DELEGACION, objAbono.Delegacion.Codigo)
                        ParametrosGeneracionCodigo.Add(Comon.Constantes.CODIGO_PARAMETRO_ABONO_FECHA_HORA, objAbono.Fecha)
                        ParametrosGeneracionCodigo.Add(Comon.Constantes.CODIGO_PARAMETRO_ABONO_TIPO_ABONO, objAbono.TipoAbono.RecuperarValor)
                        ParametrosGeneracionCodigo.Add(Comon.Constantes.CODIGO_PARAMETRO_ABONO_TIPO_VALORES, objAbono.TipoValor.RecuperarValor)
                        ParametrosGeneracionCodigo.Add(Comon.Constantes.CODIGO_PARAMETRO_ABONO_CODIGO_BANCO, objAbono.Bancos(0).Codigo)
                        ParametrosGeneracionCodigo.Add(Comon.Constantes.CODIGO_PARAMETRO_ABONO_DESCRIPCION_BANCO, objAbono.Bancos(0).Descripcion)

                        objAbono.Codigo = Comon.Util.GeneracionDinamicaTexto(ValorParametroFormatoCodigoProcesoAbono, ParametrosGeneracionCodigo)
                        objAbono.Fecha = DateTime.Now

                    End If

                    If String.IsNullOrEmpty(objAbono.Identificador) Then
                        objAbono.FechaHoraCreacion = DateTime.Now
                        objAbono.UsuarioCreacion = usuario
                        objAbono.Fecha = DateTime.Now

                        objAbono.Identificador = AccesoDatos.GenesisSaldos.Abono.Comun.InserirAbono(objAbono, sectorSelecionado.Delegacion, transaccion)

                    Else

                        AccesoDatos.GenesisSaldos.Abono.Comun.ActualizarAbono(objAbono, sectorSelecionado.Delegacion, transaccion)

                        If (objAbono.TipoAbono = Enumeradores.TipoAbono.Elemento) Then
                            AccesoDatos.GenesisSaldos.Abono.Elemento.ActualizarEstadosAbonosRemesas(objAbono.Identificador, Enumeradores.EstadoAbonoElemento.NoAbonado, transaccion)
                        End If

                        AccesoDatos.GenesisSaldos.Abono.Elemento.DeletarAbonoElemento(objAbono.Identificador, transaccion)
                        AccesoDatos.GenesisSaldos.Abono.SaldoTermino.BorrarTerminoAbonoSaldo(objAbono.Identificador, transaccion)
                        AccesoDatos.GenesisSaldos.Abono.Saldo.BorrarAbonoSaldo(objAbono.Identificador, transaccion)
                        AccesoDatos.GenesisSaldos.Abono.SaldoEfectivo.BorrarAbonoSaldoEfectivo(objAbono.Identificador, transaccion)
                        AccesoDatos.GenesisSaldos.Abono.SaldoMedioPago.BorrarAbonoSaldoMedioPago(objAbono.Identificador, transaccion)
                        AccesoDatos.GenesisSaldos.Abono.SnapShot.BorrarAbonoSaldoSnapShot(objAbono.Identificador, transaccion)

                        AccesoDatos.GenesisSaldos.Abono.ValorEfectivo.DeletarAbonoValorEfectivo(objAbono.Identificador, transaccion)
                        AccesoDatos.GenesisSaldos.Abono.ValorMedioPago.DeletarAbonoValorMedioPago(objAbono.Identificador, transaccion)
                        AccesoDatos.GenesisSaldos.Abono.Valor.DeletarAbonoValor(objAbono.Identificador, transaccion)

                    End If

                    If (objAbono.TipoAbono = Enumeradores.TipoAbono.Saldos OrElse
                        objAbono.TipoAbono = Enumeradores.TipoAbono.Pedido) AndAlso
                        objAbono.SnapshotsAbonoSaldo IsNot Nothing Then

                        For Each snapshotAbonoSaldo As Clases.Abono.AbonoValor In objAbono.SnapshotsAbonoSaldo

                            snapshotAbonoSaldo.UsuarioCreacion = usuario
                            snapshotAbonoSaldo.UsuarioModificacion = usuario

                            AccesoDatos.GenesisSaldos.Abono.SnapShot.AnadirAbonoSaldoSnapshot(snapshotAbonoSaldo, objAbono.Identificador, transaccion)

                            For Each snapDet In snapshotAbonoSaldo.AbonoSaldo.ListaSaldoCuenta
                                'Grabar lista abonos saldo efectivos e Total abono saldo efectivo
                                GrabarSaldoEfectivo(snapshotAbonoSaldo, snapDet, transaccion)

                                'Grabar lista abonos saldo medio pagos e Total abono saldo medio pagos
                                GrabarSaldoMedioPago(snapshotAbonoSaldo, snapDet, transaccion)
                            Next
                        Next

                    End If


                    If objAbono.AbonosValor IsNot Nothing AndAlso objAbono.AbonosValor.Count > 0 Then

                        For Each AV In objAbono.AbonosValor

                            AV.UsuarioCreacion = usuario
                            AV.UsuarioModificacion = usuario

                            If AV.Divisa IsNot Nothing Then

                                Dim identificadorBanco As String
                                If objAbono.TipoAbono = Enumeradores.TipoAbono.Pedido Then
                                    identificadorBanco = AV.BancoCuenta.Identificador
                                Else
                                    identificadorBanco = objAbono.Bancos(0).Identificador
                                End If

                                AV.Identificador = AccesoDatos.GenesisSaldos.Abono.Valor.InserirAbonoValor(AV, objAbono.Identificador, identificadorBanco, transaccion)

                                If AV.Divisa.ListaEfectivo IsNot Nothing AndAlso AV.Divisa.ListaEfectivo.Count > 0 Then

                                    For Each abe In AV.Divisa.ListaEfectivo

                                        abe.UsuarioCreacion = usuario
                                        abe.UsuarioModificacion = usuario

                                        AccesoDatos.GenesisSaldos.Abono.ValorEfectivo.InserirAbonoValorEfectivo(abe, AV.Identificador, AV.Divisa.Identificador, transaccion)

                                    Next

                                End If

                                If AV.Divisa.ListaMedioPago IsNot Nothing AndAlso AV.Divisa.ListaMedioPago.Count > 0 Then

                                    For Each MP In AV.Divisa.ListaMedioPago

                                        MP.UsuarioCreacion = usuario
                                        MP.UsuarioModificacion = usuario

                                        AccesoDatos.GenesisSaldos.Abono.ValorMedioPago.InserirAbonoValorMedioPago(MP, AV.Identificador, AV.Divisa.Identificador, transaccion)
                                    Next

                                End If

                                If AV.Divisa.Totales IsNot Nothing Then

                                    If AV.Divisa.Totales.TotalEfectivo <> 0 Then
                                        AccesoDatos.GenesisSaldos.Abono.ValorEfectivo.InserirAbonoValorTotalEfectivo(AV.Divisa.Totales.TotalEfectivo, AV.Identificador,
                                                                                                                    usuario, AV.Divisa.Totales.CodigoTipoEfectivoTotal, transaccion)
                                    End If

                                    If AV.Divisa.Totales.TotalCheque <> 0 Then
                                        AccesoDatos.GenesisSaldos.Abono.ValorMedioPago.InserirAbonoValorTotalMedioPago(AV.Divisa.Totales.TotalCheque, AV.Identificador,
                                                                                                                      Enumeradores.TipoMedioPago.Cheque, usuario, transaccion)
                                    End If

                                    If AV.Divisa.Totales.TotalOtroValor <> 0 Then
                                        AccesoDatos.GenesisSaldos.Abono.ValorMedioPago.InserirAbonoValorTotalMedioPago(AV.Divisa.Totales.TotalOtroValor, AV.Identificador,
                                                                                                                      Enumeradores.TipoMedioPago.OtroValor, usuario, transaccion)
                                    End If

                                    If AV.Divisa.Totales.TotalTarjeta <> 0 Then
                                        AccesoDatos.GenesisSaldos.Abono.ValorMedioPago.InserirAbonoValorTotalMedioPago(AV.Divisa.Totales.TotalTarjeta, AV.Identificador,
                                                                                                                      Enumeradores.TipoMedioPago.Tarjeta, usuario, transaccion)
                                    End If

                                    If AV.Divisa.Totales.TotalTicket <> 0 Then
                                        AccesoDatos.GenesisSaldos.Abono.ValorMedioPago.InserirAbonoValorTotalMedioPago(AV.Divisa.Totales.TotalTicket, AV.Identificador,
                                                                                                                      Enumeradores.TipoMedioPago.Ticket, usuario, transaccion)
                                    End If

                                End If

                            End If

                            If objAbono.TipoAbono = Enumeradores.TipoAbono.Elemento AndAlso AV.AbonoElemento IsNot Nothing Then

                                AV.AbonoElemento.UsuarioCreacion = usuario
                                AV.AbonoElemento.UsuarioModificacion = usuario

                                AccesoDatos.GenesisSaldos.Abono.Elemento.InserirAbonoElemento(AV.AbonoElemento, AV.Identificador, transaccion)
                                LogicaNegocio.Genesis.Remesa.ActualizarEstadoAbonoRemesa(AV.AbonoElemento.IdentificadorRemesa, Enumeradores.EstadoAbonoElemento.Abonado.RecuperarValor(), usuario, False, transaccion)

                            ElseIf (objAbono.TipoAbono = Enumeradores.TipoAbono.Saldos OrElse
                                    objAbono.TipoAbono = Enumeradores.TipoAbono.Pedido) AndAlso
                                    AV.AbonoSaldo IsNot Nothing Then

                                AV.AbonoSaldo.UsuarioCreacion = usuario
                                AV.AbonoSaldo.UsuarioModificacion = usuario

                                AccesoDatos.GenesisSaldos.Abono.Saldo.AnadirAbonoSaldo(AV.AbonoSaldo, AV.Identificador, objAbono.Identificador, transaccion)

                                For Each termino As Clases.TerminoIAC In AV.AbonoSaldo.ListaTerminoIAC
                                    AccesoDatos.GenesisSaldos.Abono.SaldoTermino.AnadirTerminoAbonoSaldo(termino, AV.AbonoSaldo.Identificador, _
                                                                                                          DateTime.Now, usuario, DateTime.Now, usuario, transaccion)
                                Next
                            End If

                        Next

                        If objAbono.CrearDocumentoPases AndAlso EsGrabarYFinalizar Then
                            GenerarDocumentoAgrupadorParaAbono(objAbono, sectorSelecionado, transaccion)
                        End If

                    End If

                    DataBaseHelper.AccesoDB.TransactionCommit(transaccion)

                Catch ex As Exception
                    DataBaseHelper.AccesoDB.TransactionRollback(transaccion)
                    Throw New Exception(ex.Message)
                End Try

                If objAbono.TipoAbono = Enumeradores.TipoAbono.Elemento Then
                    GenerarInforme(objAbono, EsGrabarYFinalizar, sectorSelecionado.Delegacion)
                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Sub

        Private Shared Sub GrabarSaldoEfectivo(ByRef snapshotAbonoSaldo As Clases.Abono.AbonoValor, ByRef snapDet As Clases.Abono.SnapshotSaldo, ByRef transaccion As DataBaseHelper.Transaccion)

            For Each efectivoAbono As Clases.Abono.EfectivoAbono In snapDet.Divisa.ListaEfectivo

                efectivoAbono.UsuarioCreacion = snapshotAbonoSaldo.UsuarioCreacion
                efectivoAbono.UsuarioModificacion = snapshotAbonoSaldo.UsuarioModificacion

                AccesoDatos.GenesisSaldos.Abono.SaldoEfectivo.AnadirAbonoSaldoEfectivo(efectivoAbono,
                                                                                      snapshotAbonoSaldo.AbonoSaldo.IdentificadorSnapshot, snapDet.IdentificadorCuenta, _
                                                                                      snapDet.Divisa.Identificador, transaccion)
            Next

            If (snapDet.Divisa.Totales IsNot Nothing AndAlso snapDet.Divisa.Totales.TotalEfectivo <> 0) Then

                AccesoDatos.GenesisSaldos.Abono.SaldoEfectivo.AnadirAbonoSaldoTotalEfectivo(snapshotAbonoSaldo.AbonoSaldo.IdentificadorSnapshot, snapDet.IdentificadorCuenta, _
                                                                                            snapDet.Divisa.Identificador, _
                                                                                            snapDet.Divisa.Totales.CodigoTipoEfectivoTotal, _
                                                                                            snapDet.Divisa.Totales.TotalEfectivo, _
                                                                                            snapshotAbonoSaldo.UsuarioCreacion, snapshotAbonoSaldo.UsuarioModificacion, transaccion)
            End If

        End Sub

        Private Shared Sub GrabarSaldoMedioPago(ByRef snapshotAbonoSaldo As Clases.Abono.AbonoValor, ByRef snapDet As Clases.Abono.SnapshotSaldo, ByRef transaccion As DataBaseHelper.Transaccion)

            For Each medioPagoAbono As Clases.Abono.MedioPagoAbono In snapDet.Divisa.ListaMedioPago

                medioPagoAbono.UsuarioCreacion = snapshotAbonoSaldo.UsuarioCreacion
                medioPagoAbono.UsuarioModificacion = snapshotAbonoSaldo.UsuarioModificacion

                AccesoDatos.GenesisSaldos.Abono.SaldoMedioPago.AnadirAbonoSaldoMedioPago(medioPagoAbono, _
                                                                                      snapshotAbonoSaldo.AbonoSaldo.IdentificadorSnapshot, snapDet.IdentificadorCuenta, _
                                                                                      snapDet.Divisa.Identificador, transaccion)
            Next

            If (snapDet.Divisa.Totales IsNot Nothing AndAlso snapDet.Divisa.Totales.TotalCheque <> 0) Then
                AccesoDatos.GenesisSaldos.Abono.SaldoMedioPago.AnadirAbonoSaldoTotalMedioPago(snapshotAbonoSaldo.AbonoSaldo.IdentificadorSnapshot, snapDet.IdentificadorCuenta, _
                                                                                              snapDet.Divisa.Identificador, Enumeradores.TipoMedioPago.Cheque, _
                                                                                              snapDet.Divisa.Totales.TotalCheque, _
                                                                                              snapshotAbonoSaldo.UsuarioCreacion, snapshotAbonoSaldo.UsuarioModificacion, transaccion)
            End If

            If (snapDet.Divisa.Totales IsNot Nothing AndAlso snapDet.Divisa.Totales.TotalOtroValor <> 0) Then
                AccesoDatos.GenesisSaldos.Abono.SaldoMedioPago.AnadirAbonoSaldoTotalMedioPago(snapshotAbonoSaldo.AbonoSaldo.IdentificadorSnapshot, snapDet.IdentificadorCuenta, _
                                                                                            snapDet.Divisa.Identificador, Enumeradores.TipoMedioPago.OtroValor, _
                                                                                            snapDet.Divisa.Totales.TotalOtroValor, _
                                                                                            snapshotAbonoSaldo.UsuarioCreacion, snapshotAbonoSaldo.UsuarioModificacion, transaccion)
            End If

            If (snapDet.Divisa.Totales IsNot Nothing AndAlso snapDet.Divisa.Totales.TotalTarjeta <> 0) Then
                AccesoDatos.GenesisSaldos.Abono.SaldoMedioPago.AnadirAbonoSaldoTotalMedioPago(snapshotAbonoSaldo.AbonoSaldo.IdentificadorSnapshot, snapDet.IdentificadorCuenta, _
                                                                                              snapDet.Divisa.Identificador, Enumeradores.TipoMedioPago.Tarjeta, _
                                                                                              snapDet.Divisa.Totales.TotalTarjeta, _
                                                                                              snapshotAbonoSaldo.UsuarioCreacion, snapshotAbonoSaldo.UsuarioModificacion, transaccion)
            End If

            If (snapDet.Divisa.Totales IsNot Nothing AndAlso snapDet.Divisa.Totales.TotalTicket <> 0) Then
                AccesoDatos.GenesisSaldos.Abono.SaldoMedioPago.AnadirAbonoSaldoTotalMedioPago(snapshotAbonoSaldo.AbonoSaldo.IdentificadorSnapshot, snapDet.IdentificadorCuenta, _
                                                                                              snapDet.Divisa.Identificador, Enumeradores.TipoMedioPago.Ticket, _
                                                                                              snapDet.Divisa.Totales.TotalTicket, _
                                                                                              snapshotAbonoSaldo.UsuarioCreacion, snapshotAbonoSaldo.UsuarioModificacion, transaccion)
            End If

        End Sub

        ''' <summary>
        ''' Gera o agendamento do relatorio
        ''' </summary>
        ''' <param name="objAbono"></param>
        ''' <param name="EsGrabarYFinalizar"></param>
        ''' <remarks></remarks>
        Public Shared Sub GenerarInforme(objAbono As Clases.Abono.Abono,
                                          EsGrabarYFinalizar As Boolean,
                                          DelegacionLogada As Clases.Delegacion)

            If EsGrabarYFinalizar Then

                Dim objTipoCliente As Comon.Clases.TipoCliente = LogicaNegocio.Genesis.TipoCliente.RecuperarTipoCliente(Comon.Constantes.CODIGO_TIPO_CLIENTE_BANCO)
                Dim objPeticionGenerarInforme As New ContractoServicio.Contractos.GenesisSaldos.Reporte.GenerarInforme.Peticion

                If objTipoCliente IsNot Nothing Then

                    Dim objTipoReporte As New List(Of Enumeradores.TipoReporte)

                    objTipoReporte.Add(Enumeradores.TipoReporte.AbonoExportacion)
                    objTipoReporte.Add(Enumeradores.TipoReporte.AbonoVisualizacion)

                    Dim objConfigReporte As List(Of Comon.Clases.ConfiguracionReporte) = FormulariosCertificados.ObtenerConfiguracionesReporte(objAbono.Bancos(0).Identificador,
                                                                                                                                               objTipoCliente.Identificador,
                                                                                                                                               objTipoReporte)

                    If objConfigReporte IsNot Nothing AndAlso objConfigReporte.Count > 0 Then

                        objPeticionGenerarInforme.Reportes = New ObservableCollection(Of Contractos.GenesisSaldos.Reporte.GenerarInforme.Reporte)

                        For Each cr In objConfigReporte

                            With objPeticionGenerarInforme

                                If cr.ParametrosReporte IsNot Nothing AndAlso cr.ParametrosReporte.Count > 0 Then

                                    If cr.ParametrosReporte.Exists(Function(p) p.Codigo = Comon.Constantes.CODIGO_PARAMETRO_REPORTE_CODIGO_DIVISA) Then

                                        Dim divisas As New List(Of String)
                                        For Each av In objAbono.AbonosValor

                                            If divisas.Count = 0 OrElse (divisas.Count > 0 AndAlso Not divisas.Contains(av.Divisa.CodigoISO)) Then

                                                Dim _parametrosReporte As ObservableCollection(Of Clases.ParametroReporte) = cr.ParametrosReporte.Clonar

                                                If _parametrosReporte IsNot Nothing AndAlso _parametrosReporte.Count > 0 Then

                                                    For Each pr In _parametrosReporte

                                                        Select Case pr.Codigo

                                                            Case Comon.Constantes.CODIGO_PARAMETRO_REPORTE_CODIGO_PROCESO_ABONO
                                                                pr.DescripcionValor = objAbono.Codigo

                                                            Case Comon.Constantes.CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_ABONO
                                                                pr.DescripcionValor = objAbono.Identificador

                                                            Case Comon.Constantes.CODIGO_PARAMETRO_REPORTE_CODIGO_DIVISA
                                                                pr.DescripcionValor = av.Divisa.CodigoISO

                                                        End Select

                                                    Next

                                                End If

                                                .Reportes.Add(New ContractoServicio.Contractos.GenesisSaldos.Reporte.GenerarInforme.Reporte With { _
                                                          .CodigoDelegacion = objAbono.Delegacion.Codigo, _
                                                          .IdentificadorConfiguracionReporte = cr.Identificador, _
                                                              .ParametrosReporte = _parametrosReporte, _
                                                          .TipoReporte = cr.TipoReporte})

                                                divisas.Add(av.Divisa.CodigoISO)

                                            End If

                                        Next

                                    Else

                                        If cr.ParametrosReporte IsNot Nothing AndAlso cr.ParametrosReporte.Count > 0 Then

                                            For Each pr In cr.ParametrosReporte

                                                Select Case pr.Codigo

                                                    Case Comon.Constantes.CODIGO_PARAMETRO_REPORTE_CODIGO_PROCESO_ABONO
                                                        pr.DescripcionValor = objAbono.Codigo

                                                    Case Comon.Constantes.CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_ABONO
                                                        pr.DescripcionValor = objAbono.Identificador

                                                End Select

                                            Next

                                        End If

                                        .Reportes.Add(New ContractoServicio.Contractos.GenesisSaldos.Reporte.GenerarInforme.Reporte With { _
                                                      .CodigoDelegacion = objAbono.Delegacion.Codigo, _
                                                      .IdentificadorConfiguracionReporte = cr.Identificador, _
                                                      .ParametrosReporte = cr.ParametrosReporte, _
                                                      .TipoReporte = cr.TipoReporte})
                                    End If

                                End If

                            End With

                        Next

                    End If

                    Dim objRespuetaGenerarInforme As ContractoServicio.Contractos.GenesisSaldos.Reporte.GenerarInforme.Respuesta = Nothing

                    objRespuetaGenerarInforme = GenesisSaldos.GenerarInforme.Ejecutar(objPeticionGenerarInforme)

                    If objRespuetaGenerarInforme.Excepciones.Count > 0 Then
                        Throw New Exception(Join((From e In objRespuetaGenerarInforme.Excepciones Select e).ToArray, vbCrLf))
                    End If

                    If objRespuetaGenerarInforme.Mensajes.Count > 0 Then
                        Throw New Exception(Join((From e In objRespuetaGenerarInforme.Mensajes Select e).ToArray, vbCrLf))
                    End If

                End If

            End If

        End Sub

        Private Shared Sub ValidarValoresGrabarAbono(objAbono As Clases.Abono.Abono)

            If objAbono Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Abono"))
            End If

            If objAbono.Bancos Is Nothing OrElse objAbono.Bancos.Count = 0 Then
                Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Banco"))
            End If

            If String.IsNullOrEmpty(objAbono.Bancos(0).Codigo) Then
                Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Codigo Banco"))
            End If

            If String.IsNullOrEmpty(objAbono.Bancos(0).Descripcion) Then
                Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Descripción Banco"))
            End If

            If String.IsNullOrEmpty(objAbono.Bancos(0).Identificador) Then
                Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Identificador Banco"))
            End If

            If objAbono.AbonosValor Is Nothing OrElse objAbono.AbonosValor.Count = 0 Then
                Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Abono Valor"))
            End If

            If objAbono.Delegacion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Abono Valor"))
            End If

            If String.IsNullOrEmpty(objAbono.Delegacion.Codigo) Then
                Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Abono Valor"))
            End If

            If String.IsNullOrEmpty(objAbono.Delegacion.Identificador) Then
                Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Abono Valor"))
            End If

            For Each av In objAbono.AbonosValor

                If av.Cuenta Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Abono Valor: Cuenta"))
                End If

                If av.CuentasDisponibles Is Nothing OrElse av.CuentasDisponibles.Count = 0 Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Abono Valor: Cuentas Disponibles"))
                End If


                If av.Cliente Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Cliente"))
                End If

                If String.IsNullOrEmpty(av.Cliente.Identificador) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Identificador Cliente"))
                End If

                If (av.Cuenta Is Nothing) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Cuenta"))
                End If

                If String.IsNullOrEmpty(av.Cuenta.CodigoTipoCuentaBancaria) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Identificador Cliente"))
                End If

                If String.IsNullOrEmpty(av.Cuenta.CodigoCuentaBancaria) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Identificador Cliente"))
                End If

                'If String.IsNullOrEmpty(av.Cuenta.CodigoDocumento) Then
                '    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Codigo Documento"))
                'End If

                If String.IsNullOrEmpty(av.Cuenta.DescripcionTitularidad) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Descripcion Titularidad"))
                End If

                If av.Divisa Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Divisa"))
                End If

                If (av.Divisa.ListaEfectivo Is Nothing OrElse av.Divisa.ListaEfectivo.Count = 0) AndAlso
                   (av.Divisa.ListaMedioPago Is Nothing OrElse av.Divisa.ListaMedioPago.Count = 0) AndAlso
                   (av.Divisa.Totales Is Nothing OrElse (av.Divisa.Totales.TotalCheque = 0 AndAlso av.Divisa.Totales.TotalOtroValor = 0 AndAlso
                                                         av.Divisa.Totales.TotalTarjeta = 0 AndAlso av.Divisa.Totales.TotalTicket = 0 AndAlso
                                                         av.Divisa.Totales.TotalEfectivo = 0)) Then

                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "Divisa"))

                End If

                If objAbono.TipoAbono = Enumeradores.TipoAbono.Elemento Then

                    If av.AbonoElemento Is Nothing Then
                        Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "AbonoElemento"))
                    End If
                Else
                    If av.AbonoSaldo Is Nothing Then
                        Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo", "AbonoSaldo"))
                    End If

                    'Validar terminos do AbonoSaldo
                End If

            Next

        End Sub

        Public Shared Function ObtenerDivisasAbonar(filtro As Clases.Transferencias.FiltroConsultaValoresAbono, abono As Clases.Abono.Abono) As List(Of Clases.Abono.AbonoValor)
            Dim lista As List(Of Clases.Abono.AbonoValor)
            If abono.TipoAbono = Enumeradores.TipoAbono.Elemento Then
                lista = ObtenerDivisasAbonarElementos(filtro, abono)

                For Each av In lista
                    av.AbonoElemento.Divisa.ListaEfectivo = av.AbonoElemento.Divisa.ListaEfectivo.
                    OrderByDescending(Function(item) item.EsBillete).ThenByDescending(Function(item) item.Valor).ToList()
                Next

            Else
                lista = ObtenerDivisasAbonarSaldos(filtro, abono)

                For Each av In lista
                    av.AbonoSaldo.Divisa.ListaEfectivo = av.AbonoSaldo.Divisa.ListaEfectivo.
                    OrderByDescending(Function(item) item.EsBillete).ThenByDescending(Function(item) item.Valor).ToList()
                Next

            End If



            Return lista
        End Function

        Public Shared Function ObtenerDivisasAbonarElementos(filtro As Clases.Transferencias.FiltroConsultaValoresAbono, abono As Clases.Abono.Abono) As List(Of Clases.Abono.AbonoValor)
            Dim lista = AccesoDatos.GenesisSaldos.Abono.Comun.ObtenerDivisasAbonarElementos(filtro, abono)

            '***[INI] Definição para atender o Peru:
            'Abono Elemento [Declarado X Contado]
            '- Considerar somente os totais no Abono Declarado;
            '- Totalizar o detalhe para Abono Contado nos totais;
            If abono.TipoValor = Clases.Abono.TipoValorAbono.Declarados Then
                For Each aValor In lista
                    aValor.Divisa.ListaEfectivo.Clear()
                    aValor.Divisa.ListaMedioPago.Clear()
                Next
            ElseIf abono.TipoValor = Clases.Abono.TipoValorAbono.Contados Then
                For Each aValor In lista
                    aValor.AbonoElemento.Divisa.Totales.TotalEfectivo = 0
                    For Each efec In aValor.AbonoElemento.Divisa.ListaEfectivo
                        aValor.AbonoElemento.Divisa.Totales.TotalEfectivo += efec.Importe
                    Next
                    aValor.AbonoElemento.Divisa.ListaEfectivo.Clear()
                    aValor.AbonoElemento.Divisa.Totales.TotalCheque = 0
                    aValor.AbonoElemento.Divisa.Totales.TotalOtroValor = 0
                    aValor.AbonoElemento.Divisa.Totales.TotalTarjeta = 0
                    aValor.AbonoElemento.Divisa.Totales.TotalTicket = 0
                    For Each mp In aValor.AbonoElemento.Divisa.ListaMedioPago
                        If mp.TipoMedioPago = Enumeradores.TipoMedioPago.Cheque Then aValor.AbonoElemento.Divisa.Totales.TotalCheque += mp.Importe
                        If mp.TipoMedioPago = Enumeradores.TipoMedioPago.OtroValor Then aValor.AbonoElemento.Divisa.Totales.TotalOtroValor += mp.Importe
                        If mp.TipoMedioPago = Enumeradores.TipoMedioPago.Tarjeta Then aValor.AbonoElemento.Divisa.Totales.TotalTarjeta += mp.Importe
                        If mp.TipoMedioPago = Enumeradores.TipoMedioPago.Ticket Then aValor.AbonoElemento.Divisa.Totales.TotalTicket += mp.Importe
                    Next
                    aValor.AbonoElemento.Divisa.ListaMedioPago.Clear()

                    'Buscar o Abono Declarado para remessa
                    Dim abonoDeclarado = abono.Clonar()
                    abonoDeclarado.TipoValor = Clases.Abono.TipoValorAbono.Declarados
                    Dim filtroDeclarado = New Clases.Transferencias.FiltroConsultaValoresAbono
                    filtroDeclarado.NumeroExterno = aValor.AbonoElemento.CodigoElemento
                    filtroDeclarado.IdentificadoresDivisas = New List(Of String)()
                    filtroDeclarado.IdentificadoresDivisas.Add(aValor.AbonoElemento.Divisa.Identificador)
                    filtroDeclarado.ObtenerParaCalcularDiferencas = True
                    Dim listaAbonosValorDeclarado = ObtenerDivisasAbonarElementos(filtroDeclarado, abonoDeclarado)
                    'Se possui Abono Declarado, calcula as diferenças nos totais
                    If (listaAbonosValorDeclarado.Count > 0) Then
                        Dim aValorDeclarado = listaAbonosValorDeclarado.FirstOrDefault()
                        aValor.AbonoElemento.Divisa.Totales.TotalEfectivo -= aValorDeclarado.AbonoElemento.Divisa.Totales.TotalEfectivo
                        aValor.AbonoElemento.Divisa.Totales.TotalCheque -= aValorDeclarado.AbonoElemento.Divisa.Totales.TotalCheque
                        aValor.AbonoElemento.Divisa.Totales.TotalOtroValor -= aValorDeclarado.AbonoElemento.Divisa.Totales.TotalOtroValor
                        aValor.AbonoElemento.Divisa.Totales.TotalTarjeta -= aValorDeclarado.AbonoElemento.Divisa.Totales.TotalTarjeta
                        aValor.AbonoElemento.Divisa.Totales.TotalTicket -= aValorDeclarado.AbonoElemento.Divisa.Totales.TotalTicket
                    End If
                    aValor.AbonoElemento.Importe = aValor.AbonoElemento.Divisa.Totales.TotalEfectivo + _
                        aValor.AbonoElemento.Divisa.Totales.TotalCheque + aValor.AbonoElemento.Divisa.Totales.TotalOtroValor + _
                        aValor.AbonoElemento.Divisa.Totales.TotalTarjeta + aValor.AbonoElemento.Divisa.Totales.TotalTicket
                Next

            End If

            '***[FIM] Definição para atender o Peru

            Return lista
        End Function

        Public Shared Function ObtenerDivisasAbonarSaldos(filtro As Clases.Transferencias.FiltroConsultaValoresAbono, abono As Clases.Abono.Abono) As List(Of Clases.Abono.AbonoValor)
            Return AccesoDatos.GenesisSaldos.Abono.Comun.ObtenerDivisasAbonarSaldos(filtro, abono)
        End Function

        Public Shared Function ObtenerAbono(identificadorAbono As String, identificadorDelegacion As String, codigoDelegacion As String) As Clases.Abono.Abono

            Dim abonos As New List(Of Clases.Abono.Abono)
            Dim abono As Clases.Abono.Abono = AccesoDatos.GenesisSaldos.Abono.Comun.ObtenerAbono(identificadorAbono, identificadorDelegacion)

            'Preencher terminos abono saldo caso existam
            If abono.TipoAbono <> Enumeradores.TipoAbono.Elemento Then
                abono.CrearDocumentoPases = Not String.IsNullOrEmpty(abono.IdenficadorGrupoDocumento)
                CargarTerminosAbonosSaldos(abono, codigoDelegacion)
            End If

            For Each abonoValor In abono.AbonosValor

                Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Abono.ObtenerDatosBancarios(abonoValor, abono.TipoAbono, Nothing)
                abonoValor.CuentasDisponibles = abonoValor.CuentasDisponibles.OrderByDescending(Function(item) item.Identificador).ToList()

                If Not String.IsNullOrEmpty(abonoValor.BancoCuenta.Identificador) Then
                    Dim bancoSelecionado = abonoValor.CuentasDisponibles.Find(Function(item) item.Identificador = abonoValor.BancoCuenta.Identificador)

                    If bancoSelecionado IsNot Nothing Then
                        abonoValor.BancoCuenta = bancoSelecionado
                    End If

                End If

                If abonoValor.CuentasDisponibles.Count > 1 Then
                    Dim banco = abonoValor.CuentasDisponibles.Find(Function(item) item.Identificador = abonoValor.BancoCuenta.Identificador)
                    If banco IsNot Nothing Then
                        abonoValor.CuentasDisponibles.Remove(banco)
                        abonoValor.CuentasDisponibles.Insert(0, banco)
                    End If
                End If

                OrdenarListaEfectivoAbonoValorPorTipo(abonoValor, abono.TipoAbono)

            Next

            If abono.SnapshotsAbonoSaldo IsNot Nothing AndAlso abono.SnapshotsAbonoSaldo.Count > 0 Then

                abono.SnapshotsAbonoSaldo.ForEach(Sub(snapShot) OrdenarListaEfectivoAbonoValorPorTipo(snapShot, abono.TipoAbono))

            End If

            If abono IsNot Nothing Then
                abonos.Add(abono)
                RecuperarReportesGenerados(abonos)
                abono = abonos(0)
            End If
            Return abono
        End Function

        Private Shared Sub OrdenarListaEfectivoAbonoValorPorTipo(abonoValor As AbonoValor, tipoAbono As Enumeradores.TipoAbono)
            If tipoAbono = Enumeradores.TipoAbono.Elemento AndAlso abonoValor.AbonoElemento.Divisa IsNot Nothing AndAlso abonoValor.AbonoElemento.Divisa.ListaEfectivo IsNot Nothing AndAlso abonoValor.AbonoElemento.Divisa.ListaEfectivo.Count > 0 Then
                abonoValor.AbonoElemento.Divisa.ListaEfectivo = abonoValor.AbonoElemento.Divisa.ListaEfectivo.
                                                                OrderByDescending(Function(item) item.EsBillete).ThenByDescending(Function(item) item.Valor).ToList()
            ElseIf abonoValor.AbonoSaldo.Divisa IsNot Nothing AndAlso abonoValor.AbonoSaldo.Divisa.ListaEfectivo IsNot Nothing AndAlso abonoValor.AbonoSaldo.Divisa.ListaEfectivo.Count > 0 Then
                abonoValor.AbonoSaldo.Divisa.ListaEfectivo = abonoValor.AbonoSaldo.Divisa.ListaEfectivo.
                                                                OrderByDescending(Function(item) item.EsBillete).ThenByDescending(Function(item) item.Valor).ToList()

            End If

            If abonoValor.Divisa IsNot Nothing AndAlso abonoValor.Divisa.ListaEfectivo IsNot Nothing AndAlso abonoValor.Divisa.ListaEfectivo.Count > 0 Then
                abonoValor.Divisa.ListaEfectivo = abonoValor.Divisa.ListaEfectivo.
                                                                OrderByDescending(Function(item) item.EsBillete).ThenByDescending(Function(item) item.Valor).ToList()
            End If
            
        End Sub

        Private Shared Sub CargarTerminosAbonosSaldos(abono As Clases.Abono.Abono, docigoDelegacion As String)

            Dim terminosDocPases = ObtenerTerminosFormularioDocPases(docigoDelegacion)

            For Each abonoValor In abono.AbonosValor
                If abonoValor.AbonoSaldo IsNot Nothing Then
                    Dim dtTerminos As System.Data.DataTable = Prosegur.Genesis.AccesoDatos.GenesisSaldos.Abono.SaldoTermino.ObtenerTerminosSaldos(abonoValor.AbonoSaldo.Identificador)

                    If dtTerminos IsNot Nothing AndAlso dtTerminos.Rows.Count > 0 Then

                        If abonoValor.AbonoSaldo.ListaTerminoIAC Is Nothing Then
                            abonoValor.AbonoSaldo.ListaTerminoIAC = New List(Of Clases.TerminoIAC)()
                        End If

                        For Each row In dtTerminos.Rows
                            Dim termino = terminosDocPases.Find(Function(item) item.Identificador = Util.AtribuirValorObj(row("OID_TERMINO"), GetType(String)))

                            If termino IsNot Nothing Then
                                termino.Valor = Util.AtribuirValorObj(row("DES_VALOR"), GetType(String))
                                abonoValor.AbonoSaldo.ListaTerminoIAC.Add(termino)

                            End If
                        Next
                    End If
                End If
            Next

        End Sub

        Public Shared Function ObtenerListaCuentasDisponibles(ByRef listaCuentas As List(Of Clases.DatoBancario)) As List(Of Clases.Abono.BancoInformacion)
            Dim listaBancoInformacion As New List(Of Clases.Abono.BancoInformacion)()

            Dim listaCuentaAgrupaPorBanco = (From cuenta In listaCuentas
                                                Group cuenta By IdentificadorBanco = cuenta.Banco.Identificador, CodigoBanco = cuenta.Banco.Codigo, _
                                                Descripcion = cuenta.Banco.Descripcion Into Group
                                                Select IdentificadorBanco, CodigoBanco, Descripcion, Group)

            For Each groupBanco In listaCuentaAgrupaPorBanco

                Dim bancoInformacion = New Clases.Abono.BancoInformacion()
                With bancoInformacion
                    .Identificador = groupBanco.IdentificadorBanco
                    .Codigo = groupBanco.CodigoBanco
                    .Descripcion = groupBanco.Descripcion
                    .DatosBancarios = New List(Of Clases.Abono.DatoBancario)()
                End With

                For index = 0 To (groupBanco.Group.Count - 1)
                    Dim datoBancario = New Clases.Abono.DatoBancario(groupBanco.Group(index).CodigoTipoCuentaBancaria,
                                                groupBanco.Group(index).CodigoCuentaBancaria, groupBanco.Group(index).CodigoDocumento,
                                                groupBanco.Group(index).DescripcionTitularidad, groupBanco.Group(index).bolDefecto,
                                                groupBanco.Group(index).Identificador, groupBanco.Group(index).Banco, groupBanco.Group(index).DescripcionObs)

                    bancoInformacion.DatosBancarios.Add(datoBancario)
                Next

                listaBancoInformacion.Add(bancoInformacion)
            Next

            Return listaBancoInformacion
        End Function

        Public Shared Function ObtenerDatosBancarios(ByRef abonoVincular As Clases.Abono.AbonoValor, tipoAbono As Enumeradores.TipoAbono, _
                                                  identificadorSolicitante As String) As Boolean

            If tipoAbono = Enumeradores.TipoAbono.Pedido Then
                Return ObtenerDatosBancariosPorPedido(abonoVincular, tipoAbono, identificadorSolicitante)
            Else
                Return ObtenerDatosBancariosPorSaldoYElemento(abonoVincular, tipoAbono, identificadorSolicitante)
            End If

        End Function

        Private Shared Function ObtenerDatosBancariosPorSaldoYElemento(ByRef abonoVincular As Clases.Abono.AbonoValor, tipoAbono As Enumeradores.TipoAbono, identificadorBanco As String) As Boolean
            If abonoVincular Is Nothing OrElse
                (abonoVincular.Cliente Is Nothing) OrElse
                abonoVincular.SubCliente Is Nothing OrElse
                abonoVincular.PtoServicio Is Nothing OrElse
                abonoVincular.AbonoPorTipo(tipoAbono) Is Nothing OrElse
                abonoVincular.AbonoPorTipo(tipoAbono).Divisa Is Nothing Then
                Return False
            End If

            'Busca lista de DatosBancario
            Dim accionDatoBancario = New Prosegur.Global.GesEfectivo.IAC.LogicaNegocio.AccionDatoBancario()
            Dim peticionDatoBancario = New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.DatoBancario.GetDatosBancarios.Peticion()
            peticionDatoBancario.IdentificadorCliente = abonoVincular.Cliente.Identificador
            If Not String.IsNullOrEmpty(abonoVincular.SubCliente.Identificador) Then
                peticionDatoBancario.IdentificadorSubCliente = abonoVincular.SubCliente.Identificador
            End If
            If Not String.IsNullOrEmpty(abonoVincular.PtoServicio.Identificador) Then
                peticionDatoBancario.IdentificadorPuntoServicio = abonoVincular.PtoServicio.Identificador
            End If

            peticionDatoBancario.IdentificadorDivisa = abonoVincular.AbonoPorTipo(tipoAbono).Divisa.Identificador
            peticionDatoBancario.ObtenerSubNiveis = True

            peticionDatoBancario.IdentificadorBanco = identificadorBanco

            Dim respuesta = accionDatoBancario.GetDatosBancarios(peticionDatoBancario)
            Dim listaCuentas = respuesta.DatosBancarios

            If listaCuentas IsNot Nothing AndAlso listaCuentas.Count > 0 Then
                abonoVincular.CuentasDisponibles = Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Abono.ObtenerListaCuentasDisponibles(listaCuentas)
            Else
                Return False
            End If
            Return True

        End Function

        Private Shared Function ObtenerDatosBancariosPorPedido(ByRef abonoVincular As Clases.Abono.AbonoValor, tipoAbono As Enumeradores.TipoAbono, identificadorBanco As String) As Boolean
            If abonoVincular Is Nothing OrElse
                (abonoVincular.Cliente Is Nothing) OrElse
                abonoVincular.AbonoPorTipo(tipoAbono) Is Nothing OrElse
                abonoVincular.AbonoPorTipo(tipoAbono).Divisa Is Nothing Then
                Return False
            End If

            'Busca lista de DatosBancario
            Dim accionDatoBancario = New Prosegur.Global.GesEfectivo.IAC.LogicaNegocio.AccionDatoBancario()
            Dim peticionDatoBancario = New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.DatoBancario.GetDatosBancarios.Peticion()
            peticionDatoBancario.IdentificadorCliente = abonoVincular.Cliente.Identificador

            peticionDatoBancario.IdentificadorDivisa = abonoVincular.AbonoPorTipo(tipoAbono).Divisa.Identificador
            peticionDatoBancario.ObtenerSubNiveis = True

            Dim respuesta = accionDatoBancario.GetDatosBancarios(peticionDatoBancario)
            Dim listaCuentas = respuesta.DatosBancarios

            If listaCuentas IsNot Nothing AndAlso listaCuentas.Count > 0 Then
                abonoVincular.CuentasDisponibles = Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Abono.ObtenerListaCuentasDisponibles(listaCuentas)
            Else
                Return False
            End If
            Return True

        End Function

        Public Shared Function ObtenerNovoAbono(delegacion As Clases.Delegacion) As Clases.Abono.Abono
            Dim abono As New Clases.Abono.Abono
            abono.Delegacion = delegacion
            Return abono
        End Function

        Public Shared Function ObtenerTerminosFormularioDocPases(delegacion As String) As List(Of Clases.TerminoIAC)

            Dim parametroFormularioDocPases As Clases.Parametro = ObtenerParametroFormularioDocPasesPorParametro(delegacion)
            Dim peticionTerminoIAC As New Contractos.Comon.Terminos.RecuperarTerminosIAC.Peticion()

            peticionTerminoIAC.IdentificadorIAC = MaestroFormularios.ObtenerIdentificadorGrupoTerminosIACPorCodigoFormulario(parametroFormularioDocPases.valorParametro, True)

            Dim listaTerminos As List(Of Clases.TerminoIAC) = Nothing
            Dim respuesta As Contractos.Comon.Terminos.RecuperarTerminosIAC.Respuesta = LogicaNegocio.Genesis.TerminoIAC.RecuperarTerminosIAC(peticionTerminoIAC)

            If respuesta IsNot Nothing Then
                listaTerminos = respuesta.TerminosIAC.OrderBy(Function(termino) termino.Orden).ToList()
            End If

            Return FiltrarTerminosDocPases(listaTerminos)

        End Function

        Public Shared Function ObtenerFormularioDocPases(identificadorSector As String, usuario As String, ByRef transaccion As DataBaseHelper.Transaccion) As Clases.Formulario

            Return AccesoDatos.GenesisSaldos.Formulario.recuperarFormularioAbono(identificadorSector, usuario, transaccion)
        End Function

        Public Shared Sub VerificarFormularioDocPasesPermiteSaldoNegativo(identificadorSectorSelecionado As String, usuario As String, codigoFormulario As String, descripcionFormulario As String, permiteSaldoNegativo As Boolean)

            Try
                Dim formulario As Clases.Formulario = LogicaNegocio.GenesisSaldos.Abono.ObtenerFormularioDocPases(identificadorSectorSelecionado, usuario, Nothing)

                If formulario IsNot Nothing Then
                    codigoFormulario = formulario.Codigo
                    descripcionFormulario = formulario.Descripcion

                    ' verifica se formulario possui carcteristica para 'chegarsaldonegativo'
                    permiteSaldoNegativo = formulario.Caracteristicas IsNot Nothing AndAlso formulario.Caracteristicas.Count > 0 AndAlso formulario.Caracteristicas.Exists(Function(x) x = Enumeradores.CaracteristicaFormulario.PermiteLlegarASaldoNegativo)

                End If

            Catch ex As Exception
                'No faser nada
            End Try


        End Sub

        Private Shared Function ObtenerParametroFormularioDocPasesPorParametro(delegacion As String) As Clases.Parametro
            Dim peticion As New Prosegur.Genesis.ContractoServicio.Contractos.Comon.Parametro.obtenerParametros.Peticion

            peticion.codigoAplicacion = Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS
            peticion.codigoDelegacion = delegacion
            peticion.codigosParametro = New List(Of String)
            peticion.codigosParametro.Add(Comon.Constantes.CODIGO_PARAMETRO_FORMULARIO_PASES_PROCESO_ABONO)

            Dim respuesta As Prosegur.Genesis.ContractoServicio.Contractos.Comon.Parametro.obtenerParametros.Respuesta = _
                LogicaNegocio.Genesis.Parametros.obtenerParametros(peticion)

            If respuesta Is Nothing OrElse respuesta.parametros Is Nothing OrElse respuesta.parametros.FirstOrDefault() Is Nothing OrElse respuesta.parametros.FirstOrDefault().valorParametro Is Nothing Then
                Throw New Excepciones.ExcepcionLogica(Traduzir("071_Abono_msg_ParametroDocPasesNoEncontrado"))
            End If

            Return respuesta.parametros.FirstOrDefault()
        End Function

        Private Shared Function FiltrarTerminosDocPases(listaTerminos As List(Of Clases.TerminoIAC)) As List(Of Clases.TerminoIAC)

            Dim terminosInternosAbono As New List(Of String)(New String() {CommonConstantes.COD_TERMINO_ABONO_CODIGO_PROCESO, CommonConstantes.COD_TERMINO_ABONO_DOCUMENTO, _
                                                                           CommonConstantes.COD_TERMINO_ABONO_NUMERO_CUENTA, CommonConstantes.COD_TERMINO_ABONO_OBSERVACIONES, _
                                                                           CommonConstantes.COD_TERMINO_ABONO_TIPO_CUENTA, CommonConstantes.COD_TERMINO_ABONO_TITULARIDAD})

            Dim listaRetorno = listaTerminos.Where(Function(termino) Not terminosInternosAbono.Any(Function(t) t = termino.Codigo)).ToList()
            listaRetorno.ForEach(Sub(t) t.Valor = String.Empty)
            Return listaRetorno
        End Function

    End Class
End Namespace
