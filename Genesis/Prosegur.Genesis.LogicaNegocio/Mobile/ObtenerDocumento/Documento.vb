Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Parametro
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.AccesoDatos
Imports System.Collections.ObjectModel
Imports System.Data

Namespace Mobile.ObtenerDocumento
    ''' <summary>
    ''' LogicaNegocio obtiene documento para Mobile
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Documento

        Public Shared Function ObtenerDocumento(Peticion As ContractoServicio.Documento.Mobile.ObtenerDocumento.Peticion) As ContractoServicio.Documento.Mobile.ObtenerDocumento.Respuesta

            Dim respuesta As New ContractoServicio.Documento.Mobile.ObtenerDocumento.Respuesta
            Dim fechaRecepcion As DateTime = DateTime.Now
            Try

                ValidarPeticion(Peticion)

                Dim codigoSectorSalidaPorDefecto = RecuperarCodigoSectorSalidaPorDefecto(Peticion.codigoDelegacion)
                Dim dtDocumentos As DataTable = AccesoDatos.GenesisSaldos.Documento.obtenerDocumentosPorRuta_Mobile(Peticion, codigoSectorSalidaPorDefecto)

                respuesta.documento = ResolverDocumentos(dtDocumentos, Peticion.bolDetalleBulto, fechaRecepcion)
                respuesta.exito = True

            Catch exn As Excepcion.NegocioExcepcion
                Dim errorDocumento As New ContractoServicio.Documento.Mobile.ObtenerDocumento.ErrorRespuesta
                errorDocumento.errorCodigo = exn.Codigo
                errorDocumento.errorMensaje = exn.Descricao
                respuesta.AddError(errorDocumento)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                Dim errorDocumento As New ContractoServicio.Documento.Mobile.ObtenerDocumento.ErrorRespuesta
                errorDocumento.errorCodigo = 0
                errorDocumento.errorMensaje = ex.Message
                respuesta.AddError(errorDocumento)
            End Try

            Return respuesta

        End Function

        Private Shared Sub ValidarPeticion(Peticion As ContractoServicio.Documento.Mobile.ObtenerDocumento.Peticion)

            If Peticion Is Nothing Then
                'Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo"), "Petición")
                Throw New Excepcion.NegocioExcepcion(1, "Petición")
            End If

            If Peticion.datosRuta Is Nothing OrElse Peticion.datosRuta.Count = 0 Then
                'Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("gen_srv_msg_atributo"), "Ruta"))
                Throw New Excepcion.NegocioExcepcion(2, "Ruta")
            End If
            If String.IsNullOrEmpty(Peticion.codigoDelegacion) Then
                'Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo"), "delegacionCodigo")
                Throw New Excepcion.NegocioExcepcion(3, "delegacionCodigo")
            End If

            For i = 0 To Peticion.datosRuta.Count - 1 Step 1

                If Peticion.bolTodosRutas = False Then
                    If String.IsNullOrEmpty(Peticion.datosRuta(i).codigoRuta) Then
                        'Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo"), "¡El parámetro 'codigoRuta' es obligatorio!")
                        Throw New Excepcion.NegocioExcepcion(4, "¡El parámetro 'codigoRuta' es obligatorio!")
                    End If
                End If

                If IsNothing(Peticion.datosRuta(i).fechaRuta) Then
                    'Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo"), "fechaRuta")
                    Throw New Excepcion.NegocioExcepcion(5, "fechaRuta")
                End If

            Next

        End Sub

        ''' <summary>
        ''' Obtiene los datos de la DB y ensambla los documentos
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function ResolverDocumentos(dtDocumentos As DataTable,
                                                   bolDetalleBulto As Boolean,
                                                   fechaRecepcion As DateTime) As List(Of ContractoServicio.Documento.Mobile.ObtenerDocumento.Documento)

            If dtDocumentos Is Nothing OrElse dtDocumentos.Rows.Count = 0 Then
                Return Nothing
            End If

            Dim documentos As New List(Of ContractoServicio.Documento.Mobile.ObtenerDocumento.Documento)()

            For Each row In dtDocumentos.Rows

                If ValidaIntervaloHora(row, fechaRecepcion) Then

                    Dim documento As ContractoServicio.Documento.Mobile.ObtenerDocumento.Documento

                    'Verifico si el documento ya existe
                    documento = documentos.Where(Function(doc) doc.IdDocumento = Util.AtribuirValorObj(row("OID_DOCUMENTO"), GetType(String))).FirstOrDefault()

                    If IsNothing(documento) Then
                        documento = EnsamblarDocumento(row)
                        documentos.Add(documento)
                    End If

                    If bolDetalleBulto Then 'Cargo bultos{declarados/desglose}

                        If IsNothing(documento.Bulto) Then
                            documento.Bulto = New List(Of ContractoServicio.Documento.Mobile.ObtenerDocumento.Bulto)()
                        End If
                        'Verifico si ya existe el bulto
                        Dim bulto As ContractoServicio.Documento.Mobile.ObtenerDocumento.Bulto = documento.Bulto.Where(Function(bul) bul.idBulto = Util.AtribuirValorObj(row("OID_BULTO"), GetType(String))).FirstOrDefault()
                        If IsNothing(bulto) Then
                            bulto = EnsamblarBulto(row)
                            documento.Bulto.Add(bulto)
                        End If

                        Dim declarado As ContractoServicio.Documento.Mobile.ObtenerDocumento.Detalle = Nothing
                        If IsNothing(bulto.declarado) Then
                            bulto.declarado = New ObservableCollection(Of ContractoServicio.Documento.Mobile.ObtenerDocumento.Detalle)()
                        End If
                        If Not IsDBNull(row("DIVISA_BULTO_EFECTIVO")) Then

                            declarado = bulto.declarado.Where(Function(dec) dec.DesDivisa = Util.AtribuirValorObj(row("DIVISA_BULTO_EFECTIVO"), GetType(String))).FirstOrDefault()
                            If IsNothing(declarado) Then
                                declarado = EnsamblarDeclaradoBultoEfectivo(row)
                                bulto.declarado.Add(declarado)
                            End If

                        Else
                            declarado = bulto.declarado.Where(Function(dec) dec.DesDivisa = Util.AtribuirValorObj(row("DIVISA_BULTO_MEDIOPAGO"), GetType(String))).FirstOrDefault()
                            If IsNothing(declarado) Then
                                declarado = EnsamblarDeclaradoBultoMedioPago(row)
                                bulto.declarado.Add(declarado)
                            End If

                        End If
                        Dim desglose As ContractoServicio.Documento.Mobile.ObtenerDocumento.Desglose = Nothing
                        If Not IsDBNull(row("OID_DENOMINACION_BULTO")) Then
                            If IsNothing(declarado.Desglose) Then
                                declarado.Desglose = New List(Of ContractoServicio.Documento.Mobile.ObtenerDocumento.Desglose)()
                            End If

                            desglose = declarado.Desglose.Where(Function(des) des.IdDesglose = Util.AtribuirValorObj(row("OID_DENOMINACION_BULTO"), GetType(String))).FirstOrDefault()
                            If IsNothing(desglose) Then
                                desglose = EnsamblarDesgloseBulto(row)
                                declarado.Desglose.Add(desglose)
                                declarado.Importe += desglose.Importe
                            End If

                        End If

                    Else 'Cargo Declarados/Desglose a nivel Remesa
                        If IsNothing(documento.Declarado) Then
                            documento.Declarado = New List(Of ContractoServicio.Documento.Mobile.ObtenerDocumento.Detalle)()
                        End If

                        Dim declarado As ContractoServicio.Documento.Mobile.ObtenerDocumento.Detalle = Nothing
                        If Not IsDBNull(row("DIVISA_REMESA_EFECTIVO")) Then

                            declarado = documento.Declarado.Where(Function(dec) dec.DesDivisa = Util.AtribuirValorObj(row("DIVISA_REMESA_EFECTIVO"), GetType(String))).FirstOrDefault()
                            If IsNothing(declarado) Then
                                declarado = EnsamblarDeclaradoRemesaEfectivo(row)
                                documento.Declarado.Add(declarado)
                            End If

                        Else
                            declarado = documento.Declarado.Where(Function(dec) dec.DesDivisa = Util.AtribuirValorObj(row("DIVISA_REMESA_MEDIOPAGO"), GetType(String))).FirstOrDefault()
                            If IsNothing(declarado) Then
                                declarado = EnsamblarDeclaradoRemesaMedioPago(row)
                                documento.Declarado.Add(declarado)
                            End If

                        End If
                        'desglose
                        If Not IsNothing(declarado) Then
                            Dim desglose As ContractoServicio.Documento.Mobile.ObtenerDocumento.Desglose = Nothing

                            If Not IsDBNull(row("OID_DENOMINACION_REMESA")) Then
                                If IsNothing(declarado.Desglose) Then
                                    declarado.Desglose = New List(Of ContractoServicio.Documento.Mobile.ObtenerDocumento.Desglose)()
                                End If
                                desglose = declarado.Desglose.Where(Function(des) des.IdDesglose = Util.AtribuirValorObj(row("OID_DENOMINACION_REMESA"), GetType(String))).FirstOrDefault()
                                If IsNothing(desglose) Then
                                    desglose = EnsamblarDesgloseRemesa(row)
                                    declarado.Desglose.Add(desglose)
                                End If

                            End If

                        End If

                    End If

                End If

            Next

            Return documentos
        End Function

        Private Shared Function ValidaIntervaloHora(ByRef row As DataRow, fechaRecepcion As DateTime) As Boolean

            If IsNothing(row) OrElse IsDBNull(row) Then
                Return False
            End If
            If IsDBNull(row("HORASALIDA")) Then
                Return True
            End If
            Dim IntervaloInicial As DateTime = fechaRecepcion.AddHours(-1)
            Dim IntervavoFinal As DateTime = fechaRecepcion.AddHours(1)
            Dim HoraSalida As DateTime = Util.AtribuirValorObj(row("FYH_TRANSPORTE"), GetType(DateTime))
            If HoraSalida >= IntervaloInicial AndAlso HoraSalida <= IntervavoFinal Then
                Return True
            End If
            Return False

        End Function

        Private Shared Function EnsamblarDocumento(ByRef row As DataRow) As ContractoServicio.Documento.Mobile.ObtenerDocumento.Documento
            Dim documento As New ContractoServicio.Documento.Mobile.ObtenerDocumento.Documento()
            documento.IdDocumento = Util.AtribuirValorObj(row("OID_DOCUMENTO"), GetType(String))
            documento.CodDocumento = Util.AtribuirValorObj(row("COD_EXTERNO"), GetType(String))
            documento.CodRuta = Util.AtribuirValorObj(row("COD_RUTA"), GetType(String))
            documento.FecRuta = Util.AtribuirValorObj(row("FYH_TRANSPORTE"), GetType(DateTime))
            documento.FecSalidaRuta = Util.AtribuirValorObj(row("HORASALIDA"), GetType(DateTime))
            documento.DesEntidad = Util.AtribuirValorObj(row("DES_CLIENTE"), GetType(String))
            documento.DesCentro = Util.AtribuirValorObj(row("DES_SUBCLIENTE"), GetType(String))
            documento.DesPunto = Util.AtribuirValorObj(row("COD_PTO_SERVICIO"), GetType(String))
            Return documento
        End Function

        Private Shared Function EnsamblarBulto(ByRef row As DataRow) As ContractoServicio.Documento.Mobile.ObtenerDocumento.Bulto
            Dim bulto As New ContractoServicio.Documento.Mobile.ObtenerDocumento.Bulto()
            bulto.idBulto = Util.AtribuirValorObj(row("OID_BULTO"), GetType(String))
            bulto.codPrecinto = Util.AtribuirValorObj(row("COD_PRECINTO"), GetType(String))
            Return bulto
        End Function

        Private Shared Function EnsamblarDeclaradoBultoEfectivo(ByRef row As DataRow) As ContractoServicio.Documento.Mobile.ObtenerDocumento.Detalle

            Dim declarado As New ContractoServicio.Documento.Mobile.ObtenerDocumento.Detalle()
            declarado.DesDivisa = Util.AtribuirValorObj(row("DIVISA_BULTO_EFECTIVO"), GetType(String))
            declarado.DesTipoMercancia = "Efectivo"
            declarado.Cantidad = Nothing
            Return declarado

        End Function

        Private Shared Function EnsamblarDeclaradoRemesaEfectivo(ByRef row As DataRow) As ContractoServicio.Documento.Mobile.ObtenerDocumento.Detalle

            Dim declarado As New ContractoServicio.Documento.Mobile.ObtenerDocumento.Detalle()
            declarado.DesDivisa = Util.AtribuirValorObj(row("DIVISA_REMESA_EFECTIVO"), GetType(String))
            declarado.DesTipoMercancia = "Efectivo"
            declarado.Cantidad = Nothing
            declarado.Importe = Util.AtribuirValorObj(row("IMPORTE_DENOMINACION_REMESA"), GetType(Double))
            Return declarado

        End Function

        Private Shared Function EnsamblarDeclaradoBultoMedioPago(ByRef row As DataRow) As ContractoServicio.Documento.Mobile.ObtenerDocumento.Detalle

            Dim declarado As New ContractoServicio.Documento.Mobile.ObtenerDocumento.Detalle()
            declarado.DesDivisa = Util.AtribuirValorObj(row("DIVISA_BULTO_MEDIOPAGO"), GetType(String))
            declarado.DesTipoMercancia = Util.AtribuirValorObj(row("MEDIOPAGO_BULTO"), GetType(String))
            declarado.Cantidad = Util.AtribuirValorObj(row("CANTIDAD_TOTAL_BULTO_MEDIOPAGO"), GetType(Integer))
            declarado.Importe = Util.AtribuirValorObj(row("IMPORTE_TOTAL_BULTO_MEDIOPAGO"), GetType(Double))
            Return declarado

        End Function

        Private Shared Function EnsamblarDeclaradoRemesaMedioPago(ByRef row As DataRow) As ContractoServicio.Documento.Mobile.ObtenerDocumento.Detalle

            Dim declarado As New ContractoServicio.Documento.Mobile.ObtenerDocumento.Detalle()
            declarado.DesDivisa = Util.AtribuirValorObj(row("DIVISA_REMESA_MEDIOPAGO"), GetType(String))
            declarado.DesTipoMercancia = Util.AtribuirValorObj(row("MEDIOPAGO_REMESA"), GetType(String))
            declarado.Cantidad = Util.AtribuirValorObj(row("CANTIDAD_TOTAL_REMESA_MP"), GetType(Integer))
            declarado.Importe = Util.AtribuirValorObj(row("IMPORTE_TOTAL_REMESA_MP"), GetType(Double))
            Return declarado

        End Function

        Private Shared Function EnsamblarDesgloseBulto(ByRef row As DataRow) As ContractoServicio.Documento.Mobile.ObtenerDocumento.Desglose

            Dim desglose As New ContractoServicio.Documento.Mobile.ObtenerDocumento.Desglose()
            desglose.IdDesglose = Util.AtribuirValorObj(row("OID_DENOMINACION_BULTO"), GetType(String))
            desglose.DesDenominacion = Util.AtribuirValorObj(row("DES_DENOMINACION_BULTO"), GetType(String))
            desglose.Cantidad = Util.AtribuirValorObj(row("CANTIDAD_DENOMINACION_BULTO"), GetType(Integer))
            desglose.Importe = Util.AtribuirValorObj(row("IMPORTE_DENOMINACION_BULTO"), GetType(Double))
            Return desglose

        End Function

        Private Shared Function EnsamblarDesgloseRemesa(ByRef row As DataRow) As ContractoServicio.Documento.Mobile.ObtenerDocumento.Desglose

            Dim desglose As New ContractoServicio.Documento.Mobile.ObtenerDocumento.Desglose()
            desglose.IdDesglose = Util.AtribuirValorObj(row("OID_DENOMINACION_REMESA"), GetType(String))
            desglose.DesDenominacion = Util.AtribuirValorObj(row("DES_DENOMINACION_REMESA"), GetType(String))
            desglose.Cantidad = Util.AtribuirValorObj(row("CANTIDAD_DENOMINACION_REMESA"), GetType(Integer))
            desglose.Importe = Util.AtribuirValorObj(row("IMPORTE_DENOMINACION_REMESA"), GetType(Double))
            Return desglose

        End Function

        Private Shared Function RecuperarCodigoSectorSalidaPorDefecto(codigoDelegacion As String) As Object

            Dim valor As New Object

            Dim peticion As New obtenerParametros.Peticion
            peticion.codigoAplicacion = Comon.Constantes.CODIGO_APLICACION_GENESIS_RECEPCION_Y_ENVIO
            peticion.codigoDelegacion = codigoDelegacion
            peticion.codigoPais = String.Empty
            peticion.obtenerParametrosGenesis = False
            peticion.obtenerParametrosReportes = False
            peticion.codigoPuesto = String.Empty
            peticion.codigosParametro = New List(Of String) From {"CodigoSectorSalidaPorDefecto"}
            Dim respuesta As obtenerParametros.Respuesta = Prosegur.Genesis.LogicaNegocio.Genesis.Parametros.obtenerParametros(peticion)

            If respuesta IsNot Nothing AndAlso respuesta.codigoResultado <> Excepcion.Constantes.CONST_CODIGO_INTEGRACION_SEM_ERRO Then
                Throw New Excepcion.NegocioExcepcion(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_ATENCION, respuesta.descripcionResultado)
            ElseIf respuesta.parametros Is Nothing OrElse respuesta.parametros.Count = 0 OrElse _
                respuesta.parametros.FirstOrDefault(Function(x) x.codigoAplicacion = Comon.Constantes.CODIGO_APLICACION_GENESIS_RECEPCION_Y_ENVIO) Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_ATENCION, String.Format(Traduzir("NoFuePosibleCargarParametrosAplicacion"), Comon.Constantes.CODIGO_APLICACION_GENESIS_RECEPCION_Y_ENVIO))
            End If

            If respuesta.parametros IsNot Nothing AndAlso respuesta.parametros.Count > 0 Then
                Select Case respuesta.parametros(0).tipoParametro

                    Case 3
                        '3 - CHECKBOX
                        Dim valorBoolean As Boolean = False

                        If Not String.IsNullOrEmpty(respuesta.parametros(0).valorParametro) AndAlso respuesta.parametros(0).valorParametro = "1" Then
                            valorBoolean = True
                        End If

                        valor = valorBoolean
                    Case Else
                        Dim valorString As String = respuesta.parametros(0).valorParametro
                        valor = valorString
                End Select
            End If

            Return valor

        End Function

    End Class

End Namespace

