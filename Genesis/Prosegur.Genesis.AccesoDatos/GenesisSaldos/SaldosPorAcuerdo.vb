Imports System.Globalization
Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports System.Linq


Namespace GenesisSaldos
    Public Class SaldosPorAcuerdo

        Public Shared Sub Recuperar(identificadorLlamada As String, peticion As RecuperarSaldosAcuerdo.Peticion,
                            ByRef respuesta As RecuperarSaldosAcuerdo.Respuesta,
                   Optional ByRef log As StringBuilder = Nothing)
            Try
                If log Is Nothing Then log = New StringBuilder
                Dim TiempoParcial As DateTime = Now

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                TiempoParcial = Now

                spw = ColectarPeticion(identificadorLlamada, peticion)
                log.AppendLine("Tiempo de acceso a datos (Parametros para procedure): " & Now.Subtract(TiempoParcial).ToString() & "; ")

                TiempoParcial = Now
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                log.AppendLine("Tiempo de acceso a datos (Ejecutar procedure): " & Now.Subtract(TiempoParcial).ToString() & "; ")

                TiempoParcial = Now
                PoblarRespuesta(ds, respuesta)

                spw = Nothing
                ds.Dispose()

                log.AppendLine("Tiempo de acceso a datos (Poblar objecto de respuesta): " & Now.Subtract(TiempoParcial).ToString() & "; ")

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

        End Sub

        Private Shared Function ColectarPeticion(identificadorLlamada As String, peticion As RecuperarSaldosAcuerdo.Peticion) As SPWrapper
            Dim SP As String = String.Format("SAPR_PSALDOS_{0}.srecuperar_saldos_acuerdos", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada, , False)
            spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Descricao_Curta, peticion.Configuracion.IdentificadorAjeno, , False)
            spw.AgregarParam("par$cod_pais", ProsegurDbType.Descricao_Curta, peticion.CodigoPais, , False)

            If peticion.Fecha <> DateTime.MinValue Then
                spw.AgregarParam("par$cod_fecha_saldos", ProsegurDbType.Descricao_Curta, peticion.Fecha.ToString("dd/MM/yyyy HH:mm:ss zzz"), , False)
            Else
                spw.AgregarParam("par$cod_fecha_saldos", ProsegurDbType.Descricao_Curta, Nothing, , False)
            End If

            spw.AgregarParam("par$des_sourcerefenceid", ProsegurDbType.Descricao_Longa, peticion.SourceReferenceId, , False)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Curta, peticion.Configuracion.Usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Curta, Util.GetCultureUser())
            spw.AgregarParam("par$rc_puntos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "puntos")
            spw.AgregarParam("par$rc_movimientos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "movimientos")
            spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")

            Return spw

        End Function

        Private Shared Sub PoblarRespuesta(ds As DataSet, respuesta As RecuperarSaldosAcuerdo.Respuesta)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                If ds.Tables.Contains("puntos") AndAlso ds.Tables("puntos").Rows.Count > 0 Then
                    For Each rowPunto In ds.Tables("puntos").Rows
                        Dim puntoServicio = New RecuperarSaldosAcuerdo.PuntoServicio
                        Util.AtribuirValorObjeto(puntoServicio.SourceReferenceId, rowPunto("SourceReferenceId"), GetType(String))
                        Util.AtribuirValorObjeto(puntoServicio.ServiceOrderId, rowPunto("ServiceOrderId"), GetType(String))
                        Util.AtribuirValorObjeto(puntoServicio.ServiceOrderCode, rowPunto("ServiceOrderCode"), GetType(String))
                        Util.AtribuirValorObjeto(puntoServicio.ProductCode, rowPunto("ProductCode"), GetType(String))
                        Util.AtribuirValorObjeto(puntoServicio.ContractId, rowPunto("ContractId"), GetType(String))
                        Util.AtribuirValorObjeto(puntoServicio.CodigoPuntoServicio, rowPunto("CodigoPuntoServicio"), GetType(String))

                        For Each rowMovimiento In ds.Tables("movimientos").Select("SOURCEREFERENCEID = '" & puntoServicio.SourceReferenceId & "'")
                            CargarMovimientoEnLista(rowMovimiento, puntoServicio)
                        Next

                        respuesta.PuntosServicio.Add(puntoServicio)
                    Next
                End If

                ' Validaciones
                If ds.Tables.Contains("validaciones") AndAlso ds.Tables("validaciones").Rows.Count > 0 Then

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ContractoServicio.Contractos.Integracion.Comon.Detalle)
                    For Each row As DataRow In ds.Tables("validaciones").Rows
                        respuesta.Resultado.Detalles.Add(New ContractoServicio.Contractos.Integracion.Comon.Detalle With {.Codigo = Util.AtribuirValorObj(row(0), GetType(String)), .Descripcion = Util.AtribuirValorObj(row(1), GetType(String))})
                    Next
                Else
                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ContractoServicio.Contractos.Integracion.Comon.Detalle)

                    Dim unDetalle = New ContractoServicio.Contractos.Integracion.Comon.Detalle

                    Util.resultado(unDetalle.Codigo,
                       unDetalle.Descripcion,
                       Prosegur.Genesis.Comon.Enumeradores.Mensajes.Tipo.Exito,
                       Prosegur.Genesis.Comon.Enumeradores.Mensajes.Contexto.Integraciones,
                       Prosegur.Genesis.Comon.Enumeradores.Mensajes.Funcionalidad.RecuperarSaldosAcuerdo,
                       "0000", "", True)

                    respuesta.Resultado.Detalles.Add(unDetalle)
                End If

            End If
        End Sub

        Private Shared Sub CargarMovimientoEnLista(rowMovimiento As DataRow, puntoServicio As RecuperarSaldosAcuerdo.PuntoServicio)
            'General
            Dim fechaHoraInicio As DateTime
            Dim fechaHoraFin As DateTime
            Dim tipoMercancia As String = String.Empty
            Dim divisa As String = String.Empty

            Util.AtribuirValorObjeto(divisa, rowMovimiento("Cod_Divisa"), GetType(String))
            Util.AtribuirValorObjeto(fechaHoraFin, rowMovimiento("FechaHoraFin"), GetType(DateTime))
            Util.AtribuirValorObjeto(fechaHoraInicio, rowMovimiento("FechaHoraInicio"), GetType(DateTime))
            Util.AtribuirValorObjeto(tipoMercancia, rowMovimiento("TipoMercancia"), GetType(String))

            ' CashIn
            Dim cashInCantidadTransacciones As Integer = 0
            Dim cashInTotal As Double = 0

            Util.AtribuirValorObjeto(cashInCantidadTransacciones, rowMovimiento("CantidadTransaccionesCashIn"), GetType(Integer))
            Util.AtribuirValorObjeto(cashInTotal, rowMovimiento("TotalCashIn"), GetType(Double))

            If (cashInCantidadTransacciones > 0 OrElse cashInTotal > 0) AndAlso puntoServicio.ProductCode.ToUpper() = Prosegur.Genesis.Comon.Constantes.CODIGO_PRODUCTO_TRANSACCION Then
                If puntoServicio.CashIns Is Nothing Then puntoServicio.CashIns = New List(Of RecuperarSaldosAcuerdo.CashIn)()

                Dim objCashIn As New RecuperarSaldosAcuerdo.CashIn With {
                    .CantidadTransacciones = cashInCantidadTransacciones,
                    .Total = cashInTotal,
                    .Divisa = divisa,
                    .TipoMercancia = tipoMercancia,
                    .FechaHoraFin = fechaHoraFin,
                    .FechaHoraInicio = fechaHoraInicio
                }

                puntoServicio.CashIns.Add(objCashIn)
            End If

            'ShipOut
            Dim shipOutCantidadTransacciones As Integer = 0
            Dim shipOutInTotal As Double = 0

            Util.AtribuirValorObjeto(shipOutCantidadTransacciones, rowMovimiento("CantidadTransaccionesShipOut"), GetType(Integer))
            Util.AtribuirValorObjeto(shipOutInTotal, rowMovimiento("TotalShipOut"), GetType(Double))

            If (shipOutCantidadTransacciones > 0 OrElse shipOutInTotal > 0) AndAlso puntoServicio.ProductCode.ToUpper() = Prosegur.Genesis.Comon.Constantes.CODIGO_PRODUCTO_TRANSACCION Then
                If puntoServicio.ShipOuts Is Nothing Then puntoServicio.ShipOuts = New List(Of RecuperarSaldosAcuerdo.ShipOut)()

                Dim objShipOut As New RecuperarSaldosAcuerdo.ShipOut With {
                    .CantidadTransacciones = shipOutCantidadTransacciones,
                    .Total = shipOutInTotal,
                    .TipoMercancia = tipoMercancia,
                    .FechaHoraFin = fechaHoraFin,
                    .FechaHoraInicio = fechaHoraInicio,
                    .Divisa = divisa
                }

                puntoServicio.ShipOuts.Add(objShipOut)
            End If


            'Acreditacion
            Dim acreditacionTotalAcreditacion As Double = 0
            Dim acreditacionTotalTransacciones As Double = 0
            Dim acreditacionTotalComision As Double = 0
            Dim fechaHoraInicioVigencia As DateTime

            Util.AtribuirValorObjeto(acreditacionTotalAcreditacion, rowMovimiento("TotalAcreditacionAcred"), GetType(Double))
            Util.AtribuirValorObjeto(acreditacionTotalTransacciones, rowMovimiento("TotalTransaccionesAcred"), GetType(Double))
            Util.AtribuirValorObjeto(acreditacionTotalComision, rowMovimiento("TotalComisionAcred"), GetType(Double))

            'SALACU.NUM_TOTAL_CASHIN_ACRED > 0 AND SALACU.NUM_TOTAL_ACREDITACION > 0
            If (acreditacionTotalTransacciones > 0 OrElse acreditacionTotalAcreditacion > 0) AndAlso puntoServicio.ProductCode.ToUpper() = Prosegur.Genesis.Comon.Constantes.CODIGO_PRODUCTO_FECHA_VALOR Then
                If puntoServicio.Acreditaciones Is Nothing Then puntoServicio.Acreditaciones = New List(Of RecuperarSaldosAcuerdo.Acreditacion)()

                'Busco si ya se encuentra una acreditación para la divisa 
                Dim objAcreditacion = puntoServicio.Acreditaciones.FirstOrDefault(Function(x) x.Divisa = divisa)


                If objAcreditacion IsNot Nothing Then
                    'En caso de encontrarse incremento los valores
                    objAcreditacion.TotalAcreditacion += acreditacionTotalAcreditacion
                    objAcreditacion.TotalComision += acreditacionTotalComision
                    objAcreditacion.TotalTransacciones += acreditacionTotalTransacciones
                Else
                    'En caso de no encontrarse agrego el nuevo registro a la colección
                    objAcreditacion = New RecuperarSaldosAcuerdo.Acreditacion()
                    Util.AtribuirValorObjeto(fechaHoraInicioVigencia, rowMovimiento("FechaHoraInicioVigencia"), GetType(DateTime))
                    objAcreditacion.Divisa = divisa
                    objAcreditacion.TotalAcreditacion = acreditacionTotalAcreditacion
                    objAcreditacion.TotalComision = acreditacionTotalComision
                    objAcreditacion.TotalTransacciones = acreditacionTotalTransacciones
                    objAcreditacion.FechaHoraAcreditacion = fechaHoraFin
                    objAcreditacion.FechaHoraInicioVigencia = fechaHoraInicioVigencia

                    puntoServicio.Acreditaciones.Add(objAcreditacion)
                End If


            End If

        End Sub
    End Class
End Namespace

