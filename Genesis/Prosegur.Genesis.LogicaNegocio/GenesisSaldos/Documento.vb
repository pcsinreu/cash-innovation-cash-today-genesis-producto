Imports Prosegur.Global.Saldos.ContractoServicio
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comunicacion
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.DataBaseHelper
Imports System.Text
Imports System.Data
Imports contractoDocumento = Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Documento
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion

Namespace GenesisSaldos

    ''' <summary>
    ''' Clase Documento
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 12/09/2013 - Criado
    ''' </history>
    Public Class Documento

#Region "Procedure - Recuperar Transacciones Pantalla"

        Public Shared Sub RecuperarTransacciones(ByVal peticion As RecuperarTransacciones.Peticion,
                                                 ByVal usuario As String,
                                                 ByRef transacciones As RecuperarTransacciones.Respuesta)

            AccesoDatos.GenesisSaldos.Documento.RecuperarTransacciones(peticion, usuario, transacciones)
        End Sub
        Public Shared Sub RecuperarTransaccioneDetalle(ByVal peticion As RecuperarTransacciones.PeticionDetalle,
                                                       ByVal usuario As String,
                                                       ByRef Respuesta As RecuperarTransacciones.RespuestaDetalle)

            AccesoDatos.GenesisSaldos.Documento.RecuperarDetalle(peticion, usuario, Respuesta)
        End Sub

#End Region

#Region " Procedure - Recuperar"

        Public Shared Function recuperarDocumentosPorIdentificadores(identificadoresDocumentos As List(Of String),
                                                                     usuario As String,
                                                                     ByRef TransaccionActual As Transaccion) As ObservableCollection(Of Clases.Documento)

            Dim documentos As New ObservableCollection(Of Clases.Documento)

            Try

                If identificadoresDocumentos IsNot Nothing AndAlso identificadoresDocumentos.Count > 0 Then

                    documentos = AccesoDatos.GenesisSaldos.Documento.recuperarDocumentosPorIdentificadores(identificadoresDocumentos, usuario, TransaccionActual)

                End If

            Catch ex As Excepciones.ExcepcionLogica
                Throw New Exception(ex.Message)

            Catch ex As Exception
                Throw

            Finally
                If documentos.Count = 0 Then
                    documentos = Nothing
                End If
            End Try

            Return documentos

        End Function

        Public Shared Function recuperarDocumentoPorIdentificador(identificadorDocumento As String,
                                                                  usuario As String,
                                                                  ByRef TransaccionActual As Transaccion) As Clases.Documento
            Dim documento As New Clases.Documento

            Try

                If Not String.IsNullOrEmpty(identificadorDocumento) Then

                    Dim listaDocumentos As New List(Of String)
                    listaDocumentos.Add(identificadorDocumento)

                    Dim documentos As ObservableCollection(Of Clases.Documento) = recuperarDocumentosPorIdentificadores(listaDocumentos, usuario, TransaccionActual)

                    If documentos IsNot Nothing AndAlso documentos.Count > 0 Then
                        documento = documentos.FirstOrDefault()
                    End If

                End If

            Catch ex As Excepciones.ExcepcionLogica
                Throw New Exception(ex.Message)

            Catch ex As Exception
                Throw

            Finally
                If documento IsNot Nothing AndAlso String.IsNullOrEmpty(documento.Identificador) Then
                    documento = Nothing
                End If
            End Try

            Return documento
        End Function

        Public Shared Function recuperarUltimoDocumentosPorIdentificadores(identificadoresDocumentos As List(Of String),
                                                                     TrabajaPorBulto As Boolean,
                                                                     usuario As String) As ObservableCollection(Of Clases.Documento)

            Dim documentos As New ObservableCollection(Of Clases.Documento)

            Try

                If identificadoresDocumentos IsNot Nothing AndAlso identificadoresDocumentos.Count > 0 Then

                    documentos = AccesoDatos.GenesisSaldos.Documento.recuperarUltimoDocumentosPorIdentificadores(identificadoresDocumentos, TrabajaPorBulto, usuario)

                End If

            Catch ex As Excepciones.ExcepcionLogica
                Throw New Exception(ex.Message)

            Catch ex As Exception
                Throw

            Finally
                If documentos.Count = 0 Then
                    documentos = Nothing
                End If
            End Try

            Return documentos

        End Function

        Public Shared Function recuperarUltimoDocumentoPorIdentificador(identificadorDocumento As String,
                                                                  TrabajaPorBulto As Boolean,
                                                                  usuario As String) As Clases.Documento
            Dim documento As New Clases.Documento

            Try

                If Not String.IsNullOrEmpty(identificadorDocumento) Then

                    Dim listaDocumentos As New List(Of String)
                    listaDocumentos.Add(identificadorDocumento)

                    Dim documentos As ObservableCollection(Of Clases.Documento) = recuperarUltimoDocumentosPorIdentificadores(listaDocumentos, TrabajaPorBulto, usuario)

                    If documentos IsNot Nothing AndAlso documentos.Count > 0 Then
                        documento = documentos.FirstOrDefault()
                    End If

                End If

            Catch ex As Excepciones.ExcepcionLogica
                Throw New Exception(ex.Message)

            Catch ex As Exception
                Throw

            Finally
                If documento IsNot Nothing AndAlso String.IsNullOrEmpty(documento.Identificador) Then
                    documento = Nothing
                End If
            End Try

            Return documento
        End Function

#End Region

        ''' <summary>
        ''' Recupera os documentos no Saldos, onde seu ultimo formulario não seja um SalidaRecorrido
        ''' </summary>
        ''' <param name="peticion"></param>
        ''' <returns></returns>
        Public Shared Function RecuperarDocumentosSinSalidaRecorrido(peticion As contractoDocumento.RecuperarDocumentosSinSalidaRecorrido.Peticion) As contractoDocumento.RecuperarDocumentosSinSalidaRecorrido.Respuesta

            Dim respuesta As New contractoDocumento.RecuperarDocumentosSinSalidaRecorrido.Respuesta

            Try

                If peticion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("gen_srv_msg_atributo"), "Petición"))
                End If

                If peticion.CodigosExternos Is Nothing OrElse peticion.CodigosExternos.Count = 0 Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("gen_srv_msg_atributo"), "CodigosExternos"))
                End If

                respuesta.Documentos = AccesoDatos.GenesisSaldos.Documento.RecuperarDocumentosSinSalidaRecorrido(peticion.CodigosExternos, respuesta.CodigosExternosConSalidaRecorrido)

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.Mensajes.Add(ex.Message)

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.Excepciones.Add(ex.Message)

            End Try

            Return respuesta

        End Function



        Public Shared Function RecuperarDocumentoParaAlocacion(peticion As contractoDocumento.RecuperarDocumentoParaAlocacion.Peticion) As contractoDocumento.RecuperarDocumentoParaAlocacion.Respuesta

            Dim respuesta As New contractoDocumento.RecuperarDocumentoParaAlocacion.Respuesta

            Try

                If peticion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("gen_srv_msg_atributo"), "Petición"))
                End If

                If String.IsNullOrEmpty(peticion.IdentificadorSector) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("gen_srv_msg_atributo"), "IdentificadorSector"))
                End If

                If String.IsNullOrEmpty(peticion.CodigoExterno) AndAlso String.IsNullOrEmpty(peticion.CodigoPrecintoBulto) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("gen_srv_msg_atributo"), "CodigoExterno / CodigoPrecintoBulto"))
                End If

                respuesta.Documentos = AccesoDatos.GenesisSaldos.Documento.RecuperarDocumentoParaAlocacion(peticion.IdentificadorSector, peticion.CodigoExterno, peticion.CodigoPrecintoBulto)

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.Mensajes.Add(ex.Message)

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.Excepciones.Add(ex.Message)

            End Try

            Return respuesta

        End Function

        Public Shared Function RecuperarDocumentosElementosConcluidos(filtro As Clases.Transferencias.FiltroDocumentos_v2) As ObservableCollection(Of Clases.Documento)

            Dim documentos As ObservableCollection(Of Clases.Documento)

            Try

                If filtro Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo"), "Filtro")
                End If

                documentos = AccesoDatos.GenesisSaldos.Documento.RecuperarDocumentosElementosConcluidos(filtro)

            Catch ex As Excepcion.NegocioExcepcion
                Throw

            Catch ex As Exception
                Throw

            End Try

            Return documentos

        End Function


        Public Shared Function obtenerDocumentosPorFiltro(filtro As Clases.Transferencias.FiltroDocumentos_v2,
                                                          Optional obtenerPadre As Boolean = True,
                                                          Optional simplificado As Boolean = False) As ObservableCollection(Of Clases.Documento)

            Dim documentos As ObservableCollection(Of Clases.Documento)

            Try

                If filtro Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo"), "Filtro")
                End If

                documentos = AccesoDatos.GenesisSaldos.Documento.obtenerDocumentosPorFiltro(filtro, obtenerPadre, simplificado)

            Catch ex As Excepcion.NegocioExcepcion
                Throw

            Catch ex As Exception
                Throw

            End Try

            Return documentos

        End Function

        Public Shared Function RecuperarDocumentosElementosConcluidos(peticion As contractoDocumento.RecuperarDocumentosElementosConcluidos.Peticion) As contractoDocumento.RecuperarDocumentosElementosConcluidos.Respuesta

            Dim respuesta As New contractoDocumento.RecuperarDocumentosElementosConcluidos.Respuesta

            Try

                If peticion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo"), "Petición")
                End If

                If String.IsNullOrEmpty(peticion.CodigoDelegacion) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("gen_srv_msg_atributo"), "CodigoDelegacion"))
                End If

                If String.IsNullOrEmpty(peticion.IdentificadorSector) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("gen_srv_msg_atributo"), "IdentificadorSector"))
                End If

                Dim _filtros As New Clases.Transferencias.FiltroDocumentos_v2

                _filtros.CodigoDelegacion = peticion.CodigoDelegacion
                _filtros.IdentificadorSector = peticion.IdentificadorSector
                _filtros.FechaTransporteDesde = peticion.FechaTransporteDesde
                _filtros.FechaTransporteHasta = peticion.FechaTransporteHasta
                _filtros.CodigoRuta = peticion.CodigoRuta
                _filtros.CodigoEmisor = peticion.CodigoEmisor
                _filtros.CodigoExterno = peticion.CodigoExterno
                _filtros.FechaCreacion = peticion.FechaCreacion

                respuesta.Documentos = RecuperarDocumentosElementosConcluidos(_filtros)

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.Mensajes.Add(ex.Message)

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.Excepciones.Add(ex.Message)
            End Try

            Return respuesta

        End Function

        Public Shared Function ejecutarObtenerDocumentos(peticion As contractoDocumento.obtenerDocumentos.Peticion) As contractoDocumento.obtenerDocumentos.Respuesta

            Dim respuesta As New contractoDocumento.obtenerDocumentos.Respuesta

            Try

                If peticion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format("gen_srv_msg_atributo"), "Petición")
                End If

                If String.IsNullOrEmpty(peticion.IdentificadorSector) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("gen_srv_msg_atributo"), "IdentificadorSector"))
                End If

                Dim _peticion As New Clases.Transferencias.FiltroDocumentos_v2
                _peticion.IdentificadorSector = peticion.IdentificadorSector
                _peticion.CodigoEstadoDocumentoIgual = peticion.CodigoEstadoDocumentoIgual
                _peticion.CodigoEstadoDocumento = peticion.CodigoEstadoDocumento
                _peticion.CodigoEstadoBulto = peticion.CodigoEstadoBulto
                _peticion.CodigoEmisor = peticion.CodigoEmisor
                _peticion.FechaTransporteDesde = peticion.FechaTransporte

                If Not String.IsNullOrEmpty(peticion.CodigoPrecintoBulto) Then
                    _peticion.CodigoPrecintoBulto = peticion.CodigoPrecintoBulto
                Else
                    _peticion.CodigoExterno = peticion.CodigoExterno
                End If


                respuesta.Documentos = obtenerDocumentosPorFiltro(_peticion)

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.Mensajes.Add(ex.Message)

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.Excepciones.Add(ex.Message)
            End Try

            Return respuesta

        End Function

        Public Shared Function obtenerDocumentosNoEnviadoSol(identificadoresRemesas As List(Of String), numMaxIntentos As Integer,
                                                    Optional obtenerPadre As Boolean = False) As ObservableCollection(Of Clases.Documento)

            Dim documentos As ObservableCollection(Of Clases.Documento) = Nothing

            Try

                If identificadoresRemesas IsNot Nothing AndAlso identificadoresRemesas.Count > 0 Then
                    documentos = AccesoDatos.GenesisSaldos.Documento.obtenerDocumentosNoEnviadoSol(identificadoresRemesas, numMaxIntentos, obtenerPadre)
                End If

            Catch ex As Excepcion.NegocioExcepcion
                Throw

            Catch ex As Exception
                Throw

            End Try

            Return documentos

        End Function

        Public Shared Function ObtenerListaDocumentos(objPeticion As Comon.Peticion(Of Clases.Transferencias.FiltroDocumentos)) As Comon.Respuesta(Of List(Of Clases.Transferencias.DocumentoGrupoDocumento))

            Dim objDocumentos As New Comon.Respuesta(Of List(Of Clases.Transferencias.DocumentoGrupoDocumento))
            Try
                Dim delegacion As Clases.Delegacion

                If objPeticion.Parametro.Delegacion IsNot Nothing Then
                    delegacion = objPeticion.Parametro.Delegacion
                Else
                    Dim objSector As Comon.Clases.Sector = AccesoDatos.Genesis.Sector.ObtenerPorOid(objPeticion.Parametro.IdentificadorSector)
                    delegacion = objSector.Delegacion
                End If
                Dim tdDocumentos As DataTable

                'Solicitação pelo Ricardo: se existir filtro utiliza consulta de versões anteriores
                If objPeticion.Parametro.PorDisponibilidad IsNot Nothing Then
                    tdDocumentos = AccesoDatos.GenesisSaldos.Documento.ObtenerListaDocumentosFiltro(objPeticion, objDocumentos, delegacion)
                Else
                    tdDocumentos = AccesoDatos.GenesisSaldos.Documento.ObtenerListaDocumentos(objPeticion, objDocumentos, delegacion)
                End If

                If delegacion IsNot Nothing AndAlso tdDocumentos IsNot Nothing AndAlso tdDocumentos.Rows.Count > 0 Then
                    objDocumentos.Retorno = cargarDocumentosPaginacion(tdDocumentos, delegacion, objPeticion.Parametro.TipoSitioDocumento, False)
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            Return objDocumentos
        End Function

        Public Shared Function ObtenerDocumentosPendientes(objPeticion As Comon.Peticion(Of Clases.Transferencias.FiltroDocumentos),
                                                           DelegacionLogada As Clases.Delegacion) As Comon.Respuesta(Of List(Of Clases.Transferencias.DocumentoGrupoDocumento))

            Dim objDocumentos As New Comon.Respuesta(Of List(Of Clases.Transferencias.DocumentoGrupoDocumento))
            Try
                Dim tdDocumentos As DataTable = AccesoDatos.GenesisSaldos.Documento.ObtenerDocumentosPendientes(objPeticion, objDocumentos, DelegacionLogada)
                objDocumentos.Retorno = cargarDocumentosPaginacion(tdDocumentos, DelegacionLogada, objPeticion.Parametro.TipoSitioDocumento, True)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            Return objDocumentos
        End Function

        Public Shared Function RecuperarDocumentosMAE(codigoDelegacion As String,
                                                    ByRef codigoMAE As String, fechaTransacciones As Date) As List(Of Clases.DocumentoMae)
            Return AccesoDatos.GenesisSaldos.Documento.RecuperarDocumentosMAE(codigoDelegacion, codigoMAE, fechaTransacciones)
        End Function

        Private Shared Function cargarDocumentosPaginacion(tdDocumentos As DataTable, DelegacionLogada As Comon.Clases.Delegacion,
                                                           TipoSitioDocumento As Comon.Enumeradores.TipoSitio, buscarGeneracionF22 As Boolean) As List(Of Clases.Transferencias.DocumentoGrupoDocumento)

            Dim Retorno As New List(Of Clases.Transferencias.DocumentoGrupoDocumento)

            For Each objRow In tdDocumentos.Rows

                Dim objDocumento = New Clases.Transferencias.DocumentoGrupoDocumento

                With objDocumento
                    .Identificador = If(objRow.Table.Columns.Contains("IDENTIFICADOR"), Util.AtribuirValorObj(objRow("IDENTIFICADOR"), GetType(String)), Nothing)
                    If objRow.Table.Columns.Contains("COD_COLOR") AndAlso objRow("COD_COLOR") IsNot Nothing Then
                        .CorFormulario = System.Drawing.Color.FromName(objRow("COD_COLOR").ToString)
                    End If
                    .DescripcionFormulario = If(objRow.Table.Columns.Contains("DES_FORMULARIO"), Util.AtribuirValorObj(objRow("DES_FORMULARIO"), GetType(String)), Nothing)
                    .Tipo = If(objRow.Table.Columns.Contains("TIPO"), Util.AtribuirValorObj(objRow("TIPO"), GetType(String)), Nothing)
                    .IdentificadorSector = If(objRow.Table.Columns.Contains("OID_SECTOR"), Util.AtribuirValorObj(objRow("OID_SECTOR"), GetType(String)), Nothing)
                    .DescripcionSector = If(objRow.Table.Columns.Contains("DES_SECTOR"), Util.AtribuirValorObj(objRow("DES_SECTOR"), GetType(String)), Nothing)
                    .IdentificadorSectorOrigenDestino = If(objRow.Table.Columns.Contains("OID_SECTOR_DES"), Util.AtribuirValorObj(objRow("OID_SECTOR_DES"), GetType(String)), Nothing)
                    .DescripcionSectorOrigenDestino = If(objRow.Table.Columns.Contains("DES_SECTOR_DES"), Util.AtribuirValorObj(objRow("DES_SECTOR_DES"), GetType(String)), Nothing)
                    .DescripcionSubCanal = If(objRow.Table.Columns.Contains("DES_SUBCANAL"), Util.AtribuirValorObj(objRow("DES_SUBCANAL"), GetType(String)), Nothing)
                    .UsuarioModificacion = If(objRow.Table.Columns.Contains("DES_USUARIO_MODIFICACION"), Util.AtribuirValorObj(objRow("DES_USUARIO_MODIFICACION"), GetType(String)), Nothing)
                    .CodigoComprobante = If(objRow.Table.Columns.Contains("COD_COMPROBANTE"), Util.AtribuirValorObj(objRow("COD_COMPROBANTE"), GetType(String)), Nothing)
                    .UsuarioCreacion = If(objRow.Table.Columns.Contains("DES_USUARIO_CREACION"), Util.AtribuirValorObj(objRow("DES_USUARIO_CREACION"), GetType(String)), Nothing)
                    .NumeroExterno = If(objRow.Table.Columns.Contains("COD_EXTERNO"), Util.AtribuirValorObj(objRow("COD_EXTERNO"), GetType(String)), Nothing)
                    .TipoSitio = TipoSitioDocumento

                    If buscarGeneracionF22 Then
                        If objRow.Table.Columns.Contains("IDENTIFICADOR") AndAlso objRow.Table.Columns.Contains("OID_FORMULARIO") Then
                            .EsGeneracionF22 = AccesoDatos.GenesisSaldos.Documento.EsGeneracionF22(objRow("IDENTIFICADOR"), objRow("OID_FORMULARIO"))
                        End If
                    End If

                    'Recupera a hora e aplica o GMT da delegação
                    If objRow.Table.Columns.Contains("GMT_MODIFICACION") Then
                        .FechaHoraModificacion = Util.AtribuirValorObj(objRow("GMT_MODIFICACION"), GetType(DateTime))
                        .FechaHoraModificacion = .FechaHoraModificacion.QuieroExibirEstaFechaEnLaPatalla(DelegacionLogada)
                    End If
                    If objRow.Table.Columns.Contains("GMT_CREACION") Then
                        .FechaHoraCreacion = Util.AtribuirValorObj(objRow("GMT_CREACION"), GetType(DateTime))
                        .FechaHoraCreacion = .FechaHoraCreacion.QuieroExibirEstaFechaEnLaPatalla(DelegacionLogada)
                    End If

                    If objRow.Table.Columns.Contains("OID_CUENTA_SALDO_ORIGEN") Then
                        .CuentaSaldoOrigen = New Clases.Cuenta
                        .CuentaSaldoOrigen.Cliente = New Clases.Cliente
                        .CuentaSaldoOrigen.SubCliente = New Clases.SubCliente
                        .CuentaSaldoOrigen.SubCanal = New Clases.SubCanal
                        .CuentaSaldoOrigen.Sector = New Clases.Sector

                        .CuentaSaldoOrigen.Identificador = If(objRow.Table.Columns.Contains("OID_CUENTA_SALDO_ORIGEN"), Util.AtribuirValorObj(objRow("OID_CUENTA_SALDO_ORIGEN"), GetType(String)), Nothing)
                        .CuentaSaldoOrigen.Cliente.Identificador = If(objRow.Table.Columns.Contains("OID_CLIENTE"), Util.AtribuirValorObj(objRow("OID_CLIENTE"), GetType(String)), Nothing)
                        .CuentaSaldoOrigen.Cliente.Descripcion = If(objRow.Table.Columns.Contains("DES_CLIENTE"), Util.AtribuirValorObj(objRow("DES_CLIENTE"), GetType(String)), Nothing)
                        .CuentaSaldoOrigen.SubCliente.Identificador = If(objRow.Table.Columns.Contains("OID_SUBCLIENTE"), Util.AtribuirValorObj(objRow("OID_SUBCLIENTE"), GetType(String)), Nothing)
                        .CuentaSaldoOrigen.SubCliente.Descripcion = If(objRow.Table.Columns.Contains("DES_SUBCLIENTE"), Util.AtribuirValorObj(objRow("DES_SUBCLIENTE"), GetType(String)), Nothing)
                        .CuentaSaldoOrigen.SubCanal.Identificador = If(objRow.Table.Columns.Contains("OID_SUBCANAL"), Util.AtribuirValorObj(objRow("OID_SUBCANAL"), GetType(String)), Nothing)
                        .CuentaSaldoOrigen.SubCanal.Descripcion = If(objRow.Table.Columns.Contains("DES_SUBCANAL"), Util.AtribuirValorObj(objRow("DES_SUBCANAL"), GetType(String)), Nothing)
                        .CuentaSaldoOrigen.Sector.Identificador = If(objRow.Table.Columns.Contains("OID_SECTOR"), Util.AtribuirValorObj(objRow("OID_SECTOR"), GetType(String)), Nothing)
                        .CuentaSaldoOrigen.Sector.Descripcion = If(objRow.Table.Columns.Contains("DES_SECTOR"), Util.AtribuirValorObj(objRow("DES_SECTOR"), GetType(String)), Nothing)
                    End If

                    If objRow.Table.Columns.Contains("OID_CUENTA_SALDO_DESTINO") Then
                        .CuentaSaldoDestino = New Clases.Cuenta
                        .CuentaSaldoDestino.Cliente = New Clases.Cliente
                        .CuentaSaldoDestino.SubCliente = New Clases.SubCliente
                        .CuentaSaldoDestino.SubCanal = New Clases.SubCanal
                        .CuentaSaldoDestino.Sector = New Clases.Sector

                        .CuentaSaldoDestino.Identificador = If(objRow.Table.Columns.Contains("OID_CUENTA_SALDO_DESTINO"), Util.AtribuirValorObj(objRow("OID_CUENTA_SALDO_DESTINO"), GetType(String)), Nothing)
                        .CuentaSaldoDestino.Cliente.Identificador = If(objRow.Table.Columns.Contains("OID_CLIENTE_DESTINO"), Util.AtribuirValorObj(objRow("OID_CLIENTE_DESTINO"), GetType(String)), Nothing)
                        .CuentaSaldoDestino.Cliente.Descripcion = If(objRow.Table.Columns.Contains("DES_CLIENTE_DESTINO"), Util.AtribuirValorObj(objRow("DES_CLIENTE_DESTINO"), GetType(String)), Nothing)
                        .CuentaSaldoDestino.SubCliente.Identificador = If(objRow.Table.Columns.Contains("OID_SUBCLIENTE_DESTINO"), Util.AtribuirValorObj(objRow("OID_SUBCLIENTE_DESTINO"), GetType(String)), Nothing)
                        .CuentaSaldoDestino.SubCliente.Descripcion = If(objRow.Table.Columns.Contains("DES_SUBCLIENTE_DESTINO"), Util.AtribuirValorObj(objRow("DES_SUBCLIENTE_DESTINO"), GetType(String)), Nothing)
                        .CuentaSaldoDestino.SubCanal.Identificador = If(objRow.Table.Columns.Contains("OID_SUBCANAL_DESTINO"), Util.AtribuirValorObj(objRow("OID_SUBCANAL_DESTINO"), GetType(String)), Nothing)
                        .CuentaSaldoDestino.SubCanal.Descripcion = If(objRow.Table.Columns.Contains("DES_SUBCANAL_DESTINO"), Util.AtribuirValorObj(objRow("DES_SUBCANAL_DESTINO"), GetType(String)), Nothing)
                        .CuentaSaldoDestino.Sector.Identificador = If(objRow.Table.Columns.Contains("OID_SECTOR_DESTINO"), Util.AtribuirValorObj(objRow("OID_SECTOR_DESTINO"), GetType(String)), Nothing)
                        .CuentaSaldoDestino.Sector.Descripcion = If(objRow.Table.Columns.Contains("DES_SECTOR_DESTINO"), Util.AtribuirValorObj(objRow("DES_SECTOR_DESTINO"), GetType(String)), Nothing)
                        .CuentaSaldoDestino.Sector.Delegacion = DelegacionLogada
                    End If

                    If objRow.Table.Columns.Contains("FYH_PLAN_CERTIFICACION") Then
                        .FechaPlanCertificacion = Util.AtribuirValorObj(objRow("FYH_PLAN_CERTIFICACION"), GetType(DateTime))
                        .FechaPlanCertificacion = .FechaPlanCertificacion.QuieroExibirEstaFechaEnLaPatalla(DelegacionLogada)
                    End If
                    .NoCertificar = If(objRow.Table.Columns.Contains("BOL_NO_CERTIFICAR"), Util.AtribuirValorObj(objRow("BOL_NO_CERTIFICAR"), GetType(Boolean)), False)

                End With

                Retorno.Add(objDocumento)

            Next

            Return Retorno

        End Function

#Region "[CALCULAR VALOR DEL DOCUMENTO]"

        Public Shared Sub calcularValorDelDocumentoAltas(ByRef documento As Clases.Documento, TrabajaPorBulto As Boolean)

            ' Se o documento tem Elementos
            If documento.Elemento IsNot Nothing Then

                ' Limpo as divisas do Documento
                documento.Divisas = New ObservableCollection(Of Clases.Divisa)

                ' Remesa do documento
                Dim _remesa As Clases.Remesa = DirectCast(documento.Elemento, Clases.Remesa)

                Dim configuracionNivelSaldos As Comon.Enumeradores.ConfiguracionNivelSaldos = _remesa.ConfiguracionNivelSaldos

                If documento.SectorDestino IsNot Nothing AndAlso documento.SectorDestino.Delegacion IsNot Nothing AndAlso Not String.IsNullOrEmpty(documento.SectorDestino.Delegacion.Codigo) Then

                    'se o parametro já foi informado então não precisa recupera no IAC.
                    If configuracionNivelSaldos <> Comon.Enumeradores.ConfiguracionNivelSaldos.Detalle AndAlso
                        configuracionNivelSaldos <> Comon.Enumeradores.ConfiguracionNivelSaldos.Total Then

                        Dim listParametros As New List(Of String)
                        listParametros.Add(Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_CONFIG_NIVEL_DETALLE)
                        Dim parametros As List(Of Clases.Parametro) = AccesoDatos.Genesis.Parametros.ObtenerParametrosDelegacionPais(documento.SectorDestino.Delegacion.Codigo, Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS, listParametros)
                        If parametros IsNot Nothing AndAlso parametros.Count > 0 Then
                            Select Case parametros(0).valorParametro
                                Case "T"
                                    configuracionNivelSaldos = Comon.Enumeradores.ConfiguracionNivelSaldos.Total
                                Case "D"
                                    configuracionNivelSaldos = Comon.Enumeradores.ConfiguracionNivelSaldos.Detalle
                                Case Else
                                    configuracionNivelSaldos = Comon.Enumeradores.ConfiguracionNivelSaldos.Ambos
                            End Select
                        End If
                    End If

                End If

                If Not TrabajaPorBulto Then

                    'Quando for Gestão de Remesas e Altas/Reenvio, deve copiar os valores declarado da Remesa
                    Dim _divisa As ObservableCollection(Of Clases.Divisa) = _remesa.Divisas.Clonar()

                    ' Remove todos os items que não tenham o tipo valor informado no parâmetro
                    Comon.Util.BorrarItemsDivisasDiferentesTipoValor(_divisa, Comon.Enumeradores.TipoValor.Declarado)

                    If _divisa IsNot Nothing AndAlso _divisa.Count > 0 Then
                        CopiarDivisasParaDocumento(_divisa, documento.Divisas, configuracionNivelSaldos)
                    End If

                Else

                    For Each _bulto In _remesa.Bultos

                        'Quando for Gestão de Bultos e Altas/Reenvio, deve copiar os valores declarado dos Bultos
                        Dim _divisa As ObservableCollection(Of Clases.Divisa) = _bulto.Divisas.Clonar()

                        ' Remove todos os items que não tenham o tipo valor informado no parâmetro
                        Comon.Util.BorrarItemsDivisasDiferentesTipoValor(_divisa, Comon.Enumeradores.TipoValor.Declarado)

                        If _divisa IsNot Nothing AndAlso _divisa.Count > 0 Then
                            CopiarDivisasParaDocumento(_divisa, documento.Divisas, configuracionNivelSaldos)
                        End If

                    Next
                End If

            End If

        End Sub


        ''' <summary>
        ''' Calcular valor del documento, valor que va impactar el Saldo.
        ''' </summary>
        ''' <param name="documento"></param>
        ''' <remarks></remarks>
        Public Shared Sub calcularValorDelDocumentoPorElElemento(ByRef documento As Clases.Documento, ByRef TransaccionActual As Transaccion)

            ' Se o documento tem Elementos
            If documento.Elemento IsNot Nothing Then

                ' Limpo as divisas do Documento
                documento.Divisas = New ObservableCollection(Of Clases.Divisa)

                ' Variavel que vai armazenar as caracteristicas do docuemnto
                Dim caracteristicas As List(Of Comon.Enumeradores.CaracteristicaFormulario) = documento.Formulario.Caracteristicas

                ' Se o documento for de substituição, então deve ser buscado as caracteristicas do primeiro pai não Sustituto
                If caracteristicas IsNot Nothing AndAlso caracteristicas.Count > 0 AndAlso _
                    caracteristicas.Contains(Comon.Enumeradores.CaracteristicaFormulario.Sustitucion) Then


                    Dim CaracteristicasPadreNoSustituto As List(Of Comon.Enumeradores.CaracteristicaFormulario)

                    If documento.DocumentoPadre IsNot Nothing AndAlso documento.DocumentoPadre.Formulario IsNot Nothing AndAlso _
                        documento.DocumentoPadre.Formulario.Caracteristicas IsNot Nothing AndAlso documento.DocumentoPadre.Formulario.Caracteristicas.Count > 0 Then

                        If documento.DocumentoPadre.Formulario.Caracteristicas.Contains(Comon.Enumeradores.CaracteristicaFormulario.Sustitucion) Then
                            If TransaccionActual IsNot Nothing Then
                                CaracteristicasPadreNoSustituto = AccesoDatos.GenesisSaldos.DocumentoPadre.ObtenerCaracteristicasDocPadreNoSustituto(documento.DocumentoPadre.Identificador, TransaccionActual)
                            Else
                                CaracteristicasPadreNoSustituto = AccesoDatos.GenesisSaldos.DocumentoPadre.ObtenerCaracteristicasDocPadreNoSustituto(documento.DocumentoPadre.Identificador)
                            End If
                        Else
                            CaracteristicasPadreNoSustituto = documento.DocumentoPadre.Formulario.Caracteristicas
                        End If

                    Else
                        If TransaccionActual IsNot Nothing Then
                            CaracteristicasPadreNoSustituto = AccesoDatos.GenesisSaldos.DocumentoPadre.ObtenerCaracteristicasDocPadreNoSustituto(documento.Identificador, TransaccionActual)
                        Else
                            CaracteristicasPadreNoSustituto = AccesoDatos.GenesisSaldos.DocumentoPadre.ObtenerCaracteristicasDocPadreNoSustituto(documento.Identificador)
                        End If
                    End If

                    If CaracteristicasPadreNoSustituto Is Nothing OrElse CaracteristicasPadreNoSustituto.Count = 0 Then
                        Throw New Excepcion.NegocioExcepcion(Traduzir("028_documento_padre_nao_informado"))
                    End If

                    caracteristicas = CaracteristicasPadreNoSustituto
                End If

                If caracteristicas Is Nothing OrElse caracteristicas.Count = 0 Then
                    Throw New Excepcion.NegocioExcepcion("Caracteristicas del formulario")
                End If

                Dim objRemesa As Clases.Remesa = DirectCast(documento.Elemento, Clases.Remesa)

                Dim configuracionNivelSaldos As Comon.Enumeradores.ConfiguracionNivelSaldos = objRemesa.ConfiguracionNivelSaldos

                If documento.SectorDestino IsNot Nothing AndAlso documento.SectorDestino.Delegacion IsNot Nothing AndAlso Not String.IsNullOrEmpty(documento.SectorDestino.Delegacion.Codigo) Then

                    'se o parametro já foi informado então não precisa recupera no IAC.
                    If configuracionNivelSaldos <> Comon.Enumeradores.ConfiguracionNivelSaldos.Detalle AndAlso
                        configuracionNivelSaldos <> Comon.Enumeradores.ConfiguracionNivelSaldos.Total Then

                        Dim listParametros As New List(Of String)
                        listParametros.Add(Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_CONFIG_NIVEL_DETALLE)

                        Dim parametros As List(Of Clases.Parametro)
                        If TransaccionActual IsNot Nothing Then
                            parametros = AccesoDatos.Genesis.Parametros.ObtenerParametrosDelegacionPais(documento.SectorDestino.Delegacion.Codigo, Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS, listParametros, TransaccionActual)
                        Else
                            parametros = AccesoDatos.Genesis.Parametros.ObtenerParametrosDelegacionPais(documento.SectorDestino.Delegacion.Codigo, Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS, listParametros)
                        End If

                        If parametros IsNot Nothing AndAlso parametros.Count > 0 Then
                            Select Case parametros(0).valorParametro
                                Case "T"
                                    configuracionNivelSaldos = Comon.Enumeradores.ConfiguracionNivelSaldos.Total
                                Case "D"
                                    configuracionNivelSaldos = Comon.Enumeradores.ConfiguracionNivelSaldos.Detalle
                                Case Else
                                    configuracionNivelSaldos = Comon.Enumeradores.ConfiguracionNivelSaldos.Ambos
                            End Select
                        End If
                    End If

                End If

                If caracteristicas.Contains(Comon.Enumeradores.CaracteristicaFormulario.GestiondeRemesas) Then

                    If caracteristicas.Contains(Comon.Enumeradores.CaracteristicaFormulario.Actas) AndAlso _
                        Not caracteristicas.Contains(Comon.Enumeradores.CaracteristicaFormulario.ActaDeDesembolsado) Then
                        'Quando for Gestão de Remesas e Acta, deve copiar os valores contados da Remesa
                        'caso não tenha valores na remesa, copia os valores da Parcial

                        Dim objDivisa As ObservableCollection(Of Clases.Divisa) = objRemesa.Divisas.Clonar()

                        ' Remove todos os items que não tenham o tipo valor informado no parâmetro
                        Comon.Util.BorrarItemsDivisasDiferentesTipoValor(objDivisa, Comon.Enumeradores.TipoValor.Contado)

                        If objDivisa IsNot Nothing AndAlso objDivisa.Count > 0 Then
                            CopiarDivisasParaDocumento(objDivisa, documento.Divisas, configuracionNivelSaldos)
                        Else

                            If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
                                For Each objBulto In objRemesa.Bultos
                                    If objBulto.Parciales IsNot Nothing AndAlso objBulto.Parciales.Count > 0 Then
                                        For Each objParcial In objBulto.Parciales

                                            objDivisa = objParcial.Divisas.Clonar()

                                            ' Remove todos os items que não tenham o tipo valor informado no parâmetro
                                            Comon.Util.BorrarItemsDivisasDiferentesTipoValor(objDivisa, Comon.Enumeradores.TipoValor.Contado)

                                            If objDivisa IsNot Nothing AndAlso objDivisa.Count > 0 Then
                                                CopiarDivisasParaDocumento(objDivisa, documento.Divisas, configuracionNivelSaldos)
                                            End If
                                        Next
                                    End If
                                Next
                            End If

                        End If

                    Else
                        'Quando for Gestão de Remesas e Altas/Reenvio, deve copiar os valores declarado da Remesa

                        Dim objDivisa As ObservableCollection(Of Clases.Divisa) = objRemesa.Divisas.Clonar()

                        ' Remove todos os items que não tenham o tipo valor informado no parâmetro
                        Comon.Util.BorrarItemsDivisasDiferentesTipoValor(objDivisa, Comon.Enumeradores.TipoValor.Declarado)

                        If objDivisa IsNot Nothing AndAlso objDivisa.Count > 0 Then
                            CopiarDivisasParaDocumento(objDivisa, documento.Divisas, configuracionNivelSaldos)

                        End If

                    End If

                ElseIf caracteristicas.Contains(Comon.Enumeradores.CaracteristicaFormulario.GestiondeBultos) Then

                    For Each objBulto In objRemesa.Bultos

                        If caracteristicas.Contains(Comon.Enumeradores.CaracteristicaFormulario.Actas) Then
                            'Quando for Gestão de Bultos e Acta, deve copiar os valores contados dos Bultos
                            'caso não tenha valores nos Bultos, copia os valores da Parcial

                            Dim objDivisa As ObservableCollection(Of Clases.Divisa) = objBulto.Divisas.Clonar()

                            ' Remove todos os items que não tenham o tipo valor informado no parâmetro
                            Comon.Util.BorrarItemsDivisasDiferentesTipoValor(objDivisa, Comon.Enumeradores.TipoValor.Contado)

                            If objDivisa IsNot Nothing AndAlso objDivisa.Count > 0 Then
                                CopiarDivisasParaDocumento(objDivisa, documento.Divisas, configuracionNivelSaldos)
                            Else

                                If objBulto.Parciales IsNot Nothing AndAlso objBulto.Parciales.Count > 0 Then
                                    For Each objParcial In objBulto.Parciales
                                        If objParcial.Divisas IsNot Nothing AndAlso objParcial.Divisas.Count > 0 Then

                                            objDivisa = objParcial.Divisas.Clonar()

                                            ' Remove todos os items que não tenham o tipo valor informado no parâmetro
                                            Comon.Util.BorrarItemsDivisasDiferentesTipoValor(objDivisa, Comon.Enumeradores.TipoValor.Contado)

                                            CopiarDivisasParaDocumento(objDivisa, documento.Divisas, configuracionNivelSaldos)
                                        End If
                                    Next
                                End If

                            End If

                        Else
                            'Quando for Gestão de Bultos e Altas/Reenvio, deve copiar os valores declarado dos Bultos

                            Dim objDivisa As ObservableCollection(Of Clases.Divisa) = objBulto.Divisas.Clonar()

                            ' Remove todos os items que não tenham o tipo valor informado no parâmetro
                            Comon.Util.BorrarItemsDivisasDiferentesTipoValor(objDivisa, Comon.Enumeradores.TipoValor.Declarado)
                            CopiarDivisasParaDocumento(objDivisa, documento.Divisas, configuracionNivelSaldos)
                        End If
                    Next
                End If
            End If

        End Sub

        Public Shared Sub CopiarDivisas(divisasOrigem As ObservableCollection(Of Clases.Divisa), _
                                        ByRef divisasDestino As ObservableCollection(Of Clases.Divisa))
            ' valida  divisaOrigen
            If divisasOrigem IsNot Nothing AndAlso divisasOrigem.Count > 0 Then

                ' Valida divisaDestino
                If divisasDestino Is Nothing Then divisasDestino = New ObservableCollection(Of Clases.Divisa)

                For Each div In divisasOrigem

                    If divisasDestino.Count = 0 OrElse divisasDestino.FirstOrDefault(Function(d) d.Identificador = div.Identificador) Is Nothing Then

                        ' Caso a divisa não exista no destino, simplesmente copia toda a divisa para o destino
                        divisasDestino.Add(div)

                    Else

                        Dim divisaDestino As Clases.Divisa = divisasDestino.FirstOrDefault(Function(d) d.Identificador = div.Identificador)

                        ' Denominações
                        If div.Denominaciones IsNot Nothing AndAlso div.Denominaciones.Count() > 0 Then

                            For Each denominacionOrigem In div.Denominaciones

                                ' Valida Denominacion
                                If divisaDestino.Denominaciones Is Nothing Then divisaDestino.Denominaciones = New ObservableCollection(Of Clases.Denominacion)()

                                Dim denoninacionDestino = divisaDestino.Denominaciones.FirstOrDefault(Function(d) d.Identificador = denominacionOrigem.Identificador)
                                If denoninacionDestino Is Nothing Then

                                    ' Si la denominación aún no existe en objeto destino
                                    divisaDestino.Denominaciones.Add(denominacionOrigem)

                                Else

                                    ' Si existe, hay que validar los valores
                                    For Each valorOrigem In denominacionOrigem.ValorDenominacion
                                        Dim valorDestino As New Clases.ValorDenominacion
                                        valorDestino = denoninacionDestino.ValorDenominacion.FirstOrDefault(Function(f) ((f.Calidad IsNot Nothing _
                                                                                                       AndAlso valorOrigem.Calidad IsNot Nothing _
                                                                                                       AndAlso f.Calidad.Identificador = valorOrigem.Calidad.Identificador) OrElse _
                                                                                                         (f.Calidad Is Nothing AndAlso valorOrigem.Calidad Is Nothing)) _
                                                                                                       AndAlso f.InformadoPor = valorOrigem.InformadoPor _
                                                                                                       AndAlso (f.UnidadMedida IsNot Nothing AndAlso valorOrigem.UnidadMedida IsNot Nothing _
                                                                                                       AndAlso f.UnidadMedida.Identificador = valorOrigem.UnidadMedida.Identificador))
                                        If valorDestino Is Nothing Then
                                            valorDestino = New Clases.ValorDenominacion
                                            denoninacionDestino.ValorDenominacion.Add(valorOrigem.Clonar)
                                        Else
                                            'Soma os valores
                                            valorDestino.Importe += valorOrigem.Importe
                                            valorDestino.Cantidad += valorOrigem.Cantidad
                                        End If
                                    Next
                                End If
                            Next

                        End If

                        ' Medio de pagos
                        If div.MediosPago IsNot Nothing AndAlso div.MediosPago.Count() > 0 Then

                            For Each medioPagoOrigem In div.MediosPago

                                ' Valida MedioPago
                                If divisaDestino.MediosPago Is Nothing Then divisaDestino.MediosPago = New ObservableCollection(Of Clases.MedioPago)

                                Dim medioPagoDestino = divisaDestino.MediosPago.FirstOrDefault(Function(m) m.Identificador = medioPagoOrigem.Identificador)

                                If medioPagoDestino Is Nothing Then

                                    ' Si el MediosPago aún no existe en objeto destino
                                    divisaDestino.MediosPago.Add(medioPagoOrigem.Clonar)

                                Else

                                    For Each valorOrigem In medioPagoOrigem.Valores

                                        If medioPagoDestino.Valores Is Nothing Then medioPagoDestino.Valores = New ObservableCollection(Of Clases.ValorMedioPago)

                                        Dim valorDestino = medioPagoDestino.Valores.FirstOrDefault(Function(v) v.InformadoPor = valorOrigem.InformadoPor)

                                        If valorDestino Is Nothing Then

                                            medioPagoDestino.Valores.Add(valorOrigem.Clonar)

                                        Else
                                            'Soma os valores
                                            valorDestino.Cantidad += valorOrigem.Cantidad
                                            valorDestino.Importe += valorOrigem.Importe
                                        End If
                                    Next

                                End If
                            Next

                        End If

                        ' Geral
                        If div.ValoresTotalesDivisa IsNot Nothing AndAlso div.ValoresTotalesDivisa.Count() > 0 Then

                            For Each valor In div.ValoresTotalesDivisa

                                ' Valida ValoresTotalesDivisa
                                If divisaDestino.ValoresTotalesDivisa Is Nothing Then divisaDestino.ValoresTotalesDivisa = New ObservableCollection(Of Clases.ValorDivisa)

                                Dim valorDestino = divisaDestino.ValoresTotalesDivisa.FirstOrDefault(Function(t) t.InformadoPor = valor.InformadoPor)
                                If valorDestino Is Nothing Then
                                    divisaDestino.ValoresTotalesDivisa.Add(valor.Clonar)
                                Else
                                    'Soma los valores
                                    valorDestino.Importe += valor.Importe
                                End If

                            Next

                        End If

                        ' Total efectivo
                        If div.ValoresTotalesEfectivo IsNot Nothing AndAlso div.ValoresTotalesEfectivo.Count() > 0 Then

                            For Each valor In div.ValoresTotalesEfectivo

                                ' Valida ValoresTotalesEfectivo
                                If divisaDestino.ValoresTotalesEfectivo Is Nothing Then divisaDestino.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)

                                Dim valorDestino = divisaDestino.ValoresTotalesEfectivo.FirstOrDefault(Function(t) t.InformadoPor = valor.InformadoPor)
                                If valorDestino Is Nothing Then
                                    divisaDestino.ValoresTotalesEfectivo.Add(valor.Clonar)
                                Else
                                    'Soma os valores
                                    valorDestino.Importe += valor.Importe
                                End If
                            Next

                        End If

                        ' Total tipo medio de pago
                        If div.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso div.ValoresTotalesTipoMedioPago.Count() > 0 Then

                            For Each valor In div.ValoresTotalesTipoMedioPago

                                ' Valida ValoresTotalesTipoMedioPago
                                If divisaDestino.ValoresTotalesTipoMedioPago Is Nothing Then divisaDestino.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)

                                Dim valorDestino = divisaDestino.ValoresTotalesTipoMedioPago.FirstOrDefault(Function(t) t.InformadoPor = valor.InformadoPor AndAlso t.TipoMedioPago = valor.TipoMedioPago)
                                If valorDestino Is Nothing Then
                                    divisaDestino.ValoresTotalesTipoMedioPago.Add(valor.Clonar)
                                Else
                                    'Soma os valores
                                    valorDestino.Importe += valor.Importe
                                    valorDestino.Cantidad += valor.Cantidad
                                End If
                            Next

                        End If

                    End If


                Next

            End If
        End Sub


        ''' <summary>
        ''' Copia divisasOrigem para divisasDestino, de acuerdo con el tipo del Valor. Por Defecto los valores da divisaDestino son del tipo NoDefinido
        ''' </summary>
        ''' <param name="divisasOrigem"></param>
        ''' <param name="divisasDestino"></param>
        ''' <remarks></remarks>
        Private Shared Sub CopiarDivisasParaDocumento(divisasOrigem As ObservableCollection(Of Clases.Divisa), _
                                        ByRef divisasDestino As ObservableCollection(Of Clases.Divisa),
                                        configuracionNivelSaldos As Prosegur.Genesis.Comon.Enumeradores.ConfiguracionNivelSaldos)

            Genesis.Divisas.configuracionNivelSaldos(divisasOrigem, configuracionNivelSaldos)

            ' valida  divisaOrigen
            If divisasOrigem IsNot Nothing AndAlso divisasOrigem.Count > 0 Then

                ' Valida divisaDestino
                If divisasDestino Is Nothing Then divisasDestino = New ObservableCollection(Of Clases.Divisa)

                For Each div In divisasOrigem

                    If divisasDestino.Count = 0 OrElse divisasDestino.FirstOrDefault(Function(d) d.Identificador = div.Identificador) Is Nothing Then

                        cambiarTipoValor(div, Comon.Enumeradores.TipoValor.NoDefinido)

                        ' Caso a divisa não exista no destino, simplesmente copia toda a divisa para o destino
                        divisasDestino.Add(div)

                    Else

                        Dim divisaDestino As Clases.Divisa = divisasDestino.FirstOrDefault(Function(d) d.Identificador = div.Identificador)

                        ' Denominações
                        If div.Denominaciones IsNot Nothing AndAlso div.Denominaciones.Count() > 0 Then

                            For Each denominacionOrigem In div.Denominaciones

                                ' Valida Denominacion
                                If divisaDestino.Denominaciones Is Nothing Then divisaDestino.Denominaciones = New ObservableCollection(Of Clases.Denominacion)()

                                Dim denoninacionDestino = divisaDestino.Denominaciones.FirstOrDefault(Function(d) d.Identificador = denominacionOrigem.Identificador)
                                If denoninacionDestino Is Nothing Then

                                    For Each valor In denominacionOrigem.ValorDenominacion
                                        valor.TipoValor = Comon.Enumeradores.TipoValor.NoDefinido
                                    Next
                                    ' Si la denominación aún no existe en objeto destino
                                    divisaDestino.Denominaciones.Add(denominacionOrigem)

                                Else

                                    ' Si existe, hay que validar los valores
                                    For Each valorOrigem In denominacionOrigem.ValorDenominacion
                                        Dim valorDestino As New Clases.ValorDenominacion
                                        valorDestino = denoninacionDestino.ValorDenominacion.FirstOrDefault(Function(f) ((f.Calidad IsNot Nothing _
                                                                                                       AndAlso valorOrigem.Calidad IsNot Nothing _
                                                                                                       AndAlso f.Calidad.Identificador = valorOrigem.Calidad.Identificador) OrElse _
                                                                                                         (f.Calidad Is Nothing AndAlso valorOrigem.Calidad Is Nothing)) _
                                                                                                       AndAlso f.InformadoPor = valorOrigem.InformadoPor _
                                                                                                       AndAlso (f.UnidadMedida IsNot Nothing AndAlso valorOrigem.UnidadMedida IsNot Nothing _
                                                                                                       AndAlso f.UnidadMedida.Identificador = valorOrigem.UnidadMedida.Identificador))
                                        If valorDestino Is Nothing Then
                                            valorDestino = New Clases.ValorDenominacion
                                            valorOrigem.TipoValor = Comon.Enumeradores.TipoValor.NoDefinido
                                            denoninacionDestino.ValorDenominacion.Add(valorOrigem.Clonar)
                                        Else
                                            valorDestino.TipoValor = Comon.Enumeradores.TipoValor.NoDefinido
                                            'Soma os valores
                                            valorDestino.Importe += valorOrigem.Importe
                                            valorDestino.Cantidad += valorOrigem.Cantidad
                                        End If
                                    Next
                                End If
                            Next

                        End If

                        ' Medio de pagos
                        If div.MediosPago IsNot Nothing AndAlso div.MediosPago.Count() > 0 Then

                            For Each medioPagoOrigem In div.MediosPago

                                ' Valida MedioPago
                                If divisaDestino.MediosPago Is Nothing Then divisaDestino.MediosPago = New ObservableCollection(Of Clases.MedioPago)

                                Dim medioPagoDestino = divisaDestino.MediosPago.FirstOrDefault(Function(m) m.Identificador = medioPagoOrigem.Identificador)

                                If medioPagoDestino Is Nothing Then

                                    For Each valor In medioPagoOrigem.Valores
                                        valor.TipoValor = Comon.Enumeradores.TipoValor.NoDefinido
                                    Next
                                    ' Si el MediosPago aún no existe en objeto destino
                                    divisaDestino.MediosPago.Add(medioPagoOrigem.Clonar)

                                Else

                                    For Each valorOrigem In medioPagoOrigem.Valores

                                        If medioPagoDestino.Valores Is Nothing Then medioPagoDestino.Valores = New ObservableCollection(Of Clases.ValorMedioPago)

                                        Dim valorDestino = medioPagoDestino.Valores.FirstOrDefault(Function(v) v.InformadoPor = valorOrigem.InformadoPor)

                                        If valorDestino Is Nothing Then

                                            valorOrigem.TipoValor = Comon.Enumeradores.TipoValor.NoDefinido
                                            medioPagoDestino.Valores.Add(valorOrigem.Clonar)

                                        Else
                                            'Soma os valores
                                            valorDestino.Cantidad += valorOrigem.Cantidad
                                            valorDestino.Importe += valorOrigem.Importe
                                            valorDestino.TipoValor = Comon.Enumeradores.TipoValor.NoDefinido
                                        End If
                                    Next

                                End If
                            Next

                        End If

                        ' Geral
                        If div.ValoresTotalesDivisa IsNot Nothing AndAlso div.ValoresTotalesDivisa.Count() > 0 Then

                            For Each valor In div.ValoresTotalesDivisa

                                ' Valida ValoresTotalesDivisa
                                If divisaDestino.ValoresTotalesDivisa Is Nothing Then divisaDestino.ValoresTotalesDivisa = New ObservableCollection(Of Clases.ValorDivisa)

                                Dim valorDestino = divisaDestino.ValoresTotalesDivisa.FirstOrDefault(Function(t) t.InformadoPor = valor.InformadoPor)
                                If valorDestino Is Nothing Then
                                    valor.TipoValor = Comon.Enumeradores.TipoValor.NoDefinido
                                    divisaDestino.ValoresTotalesDivisa.Add(valor.Clonar)
                                Else
                                    'Soma los valores
                                    valorDestino.Importe += valor.Importe
                                    valorDestino.TipoValor = Comon.Enumeradores.TipoValor.NoDefinido
                                End If

                            Next

                        End If

                        ' Total efectivo
                        If div.ValoresTotalesEfectivo IsNot Nothing AndAlso div.ValoresTotalesEfectivo.Count() > 0 Then

                            For Each valor In div.ValoresTotalesEfectivo

                                ' Valida ValoresTotalesEfectivo
                                If divisaDestino.ValoresTotalesEfectivo Is Nothing Then divisaDestino.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)

                                Dim valorDestino = divisaDestino.ValoresTotalesEfectivo.FirstOrDefault(Function(t) t.InformadoPor = valor.InformadoPor)
                                If valorDestino Is Nothing Then
                                    valor.TipoValor = Comon.Enumeradores.TipoValor.NoDefinido
                                    divisaDestino.ValoresTotalesEfectivo.Add(valor.Clonar)
                                Else
                                    'Soma os valores
                                    valorDestino.Importe += valor.Importe
                                    valorDestino.TipoValor = Comon.Enumeradores.TipoValor.NoDefinido
                                End If
                            Next

                        End If

                        ' Total tipo medio de pago
                        If div.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso div.ValoresTotalesTipoMedioPago.Count() > 0 Then

                            For Each valor In div.ValoresTotalesTipoMedioPago

                                ' Valida ValoresTotalesTipoMedioPago
                                If divisaDestino.ValoresTotalesTipoMedioPago Is Nothing Then divisaDestino.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)

                                Dim valorDestino = divisaDestino.ValoresTotalesTipoMedioPago.FirstOrDefault(Function(t) t.InformadoPor = valor.InformadoPor AndAlso t.TipoMedioPago = valor.TipoMedioPago)
                                If valorDestino Is Nothing Then
                                    valor.TipoValor = Comon.Enumeradores.TipoValor.NoDefinido
                                    divisaDestino.ValoresTotalesTipoMedioPago.Add(valor.Clonar)
                                Else
                                    'Soma os valores
                                    valorDestino.Importe += valor.Importe
                                    valorDestino.Cantidad += valor.Cantidad
                                    valorDestino.TipoValor = Comon.Enumeradores.TipoValor.NoDefinido
                                End If
                            Next

                        End If

                    End If


                Next

            End If

        End Sub

        ''' <summary>
        ''' Cambiar todos los TipoValor de una divisa
        ''' </summary>
        ''' <param name="divisa"></param>
        ''' <param name="TipoValor"></param>
        ''' <remarks></remarks>
        Private Shared Sub cambiarTipoValor(ByRef divisa As Clases.Divisa, _
                                            TipoValor As Comon.Enumeradores.TipoValor)

            If divisa IsNot Nothing Then

                ' Denominacion
                If divisa.Denominaciones IsNot Nothing AndAlso divisa.Denominaciones.Count > 0 Then
                    For Each den In divisa.Denominaciones
                        If den.ValorDenominacion IsNot Nothing AndAlso den.ValorDenominacion.Count > 0 Then
                            For Each valor In den.ValorDenominacion
                                valor.TipoValor = Comon.Enumeradores.TipoValor.NoDefinido
                            Next
                        End If
                    Next
                End If

                ' MedioPago
                If divisa.MediosPago IsNot Nothing AndAlso divisa.MediosPago.Count > 0 Then
                    For Each mp In divisa.MediosPago
                        If mp.Valores IsNot Nothing AndAlso mp.Valores.Count > 0 Then
                            For Each valor In mp.Valores
                                valor.TipoValor = Comon.Enumeradores.TipoValor.NoDefinido
                            Next
                        End If
                    Next
                End If

                ' Geral
                If divisa.ValoresTotalesDivisa IsNot Nothing AndAlso divisa.ValoresTotalesDivisa.Count() > 0 Then
                    For Each valor In divisa.ValoresTotalesDivisa
                        valor.TipoValor = TipoValor
                    Next
                End If

                ' Total efectivo
                If divisa.ValoresTotalesEfectivo IsNot Nothing AndAlso divisa.ValoresTotalesEfectivo.Count() > 0 Then
                    For Each valor In divisa.ValoresTotalesEfectivo
                        valor.TipoValor = TipoValor
                    Next
                End If

                ' Total tipo medio de pago
                If divisa.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso divisa.ValoresTotalesTipoMedioPago.Count() > 0 Then
                    For Each valor In divisa.ValoresTotalesTipoMedioPago
                        valor.TipoValor = TipoValor
                    Next
                End If
            End If

        End Sub


#End Region

    End Class

End Namespace