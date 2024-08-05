Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports System.Threading.Tasks
Imports System.Text
Imports System.Globalization
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports Prosegur.Framework.Dicionario

Namespace Genesis

    Public Class Remesa

#Region " Procedure - Recuperar"

        Public Shared Function recuperarRemesas(identificadoresRemesas As List(Of String),
                                                identificadoresDocumentos As List(Of String),
                                                usuario As String) As ObservableCollection(Of Clases.Remesa)

            Dim _remesas As ObservableCollection(Of Clases.Remesa) = Nothing

            Try
                Dim ds As DataSet = Nothing

                Dim spw As SPWrapper = ColectarRemesasRecuperar(identificadoresRemesas, identificadoresDocumentos, usuario)
                ds = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

                _remesas = poblarRemesas(ds)

            Catch ex As Exception
                Throw ex
            End Try

            Return _remesas
        End Function

        Public Shared Function ColectarRemesasRecuperar(identificadoresRemesas As List(Of String),
                                                        identificadoresDocumentos As List(Of String),
                                                        usuario As String) As SPWrapper

            Dim SP As String = String.Format("sapr_premesa_{0}.srecuperar_remesas", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oids_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$oids_documentos", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ele_rc_elementos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_elementos")
            spw.AgregarParam("par$ele_rc_cuentas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_cuentas")
            spw.AgregarParam("par$ele_rc_carac_tipo_sector", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_carac_tipo_sector")
            spw.AgregarParam("par$ele_rc_val_det_efectivo", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_valores_det_efectivo")
            spw.AgregarParam("par$ele_rc_val_det_medio_pago", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_valores_det_medio_pago")
            spw.AgregarParam("par$ele_rc_val_totales", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_valores_totales")
            spw.AgregarParam("par$ele_rc_lista_valor", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_lista_valor")
            spw.AgregarParam("par$ele_rc_iac", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_iac")
            spw.AgregarParam("par$ele_rc_terminos_iac", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_terminos_iac")
            spw.AgregarParam("par$ele_rc_valor_termino_iac", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_valor_termino_iac")
            spw.AgregarParam("par$ele_rc_divisas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_divisas")
            spw.AgregarParam("par$ele_rc_denominaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_denominaciones")
            spw.AgregarParam("par$ele_rc_medio_pago", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_medio_pago")
            spw.AgregarParam("par$ele_rc_unidad_medida", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_unidad_medida")
            spw.AgregarParam("par$ele_rc_calidad", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_calidad")
            spw.AgregarParam("par$ele_rc_valor_termino", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_valor_termino")
            spw.AgregarParam("par$ele_rc_cont_precintos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_cont_precintos")
            spw.AgregarParam("par$ejecucion_interna", ParamTypes.Integer, 0, ParameterDirection.Output, False)
            spw.AgregarParam("par$usuario", ParamTypes.String, usuario, ParameterDirection.Input, False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ParamTypes.Long, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$inserts", ParamTypes.Long, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$selects", ParamTypes.Long, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            Dim _identificadoresRemesas = If(identificadoresRemesas IsNot Nothing AndAlso identificadoresRemesas.Count > 0, identificadoresRemesas.Distinct, Nothing)
            Dim _identificadoresDocumentos = If(identificadoresDocumentos IsNot Nothing AndAlso identificadoresDocumentos.Count > 0, identificadoresDocumentos.Distinct, Nothing)

            If _identificadoresRemesas IsNot Nothing AndAlso _identificadoresRemesas.Count > 0 Then
                For Each identifacadores In _identificadoresRemesas
                    spw.Param("par$oids_remesa").AgregarValorArray(identifacadores)
                Next
            Else
                spw.Param("par$oids_remesa").AgregarValorArray(DBNull.Value)
            End If

            If _identificadoresDocumentos IsNot Nothing AndAlso _identificadoresDocumentos.Count > 0 Then
                For Each identifacadores In _identificadoresDocumentos
                    spw.Param("par$oids_documentos").AgregarValorArray(identifacadores)
                Next
            Else
                spw.Param("par$oids_documentos").AgregarValorArray(DBNull.Value)
            End If

            Return spw
        End Function

        Public Shared Function poblarRemesas(ds As DataSet,
                                             Optional ByRef _identificadorDocumento As String = "",
                                             Optional _cuentas As List(Of Clases.Cuenta) = Nothing) As ObservableCollection(Of Clases.Remesa)

            If _cuentas Is Nothing Then

                Dim _identificadorDelegacion As New Dictionary(Of String, String)
                Dim _delegaciones As List(Of Clases.Delegacion) = GenesisSaldos.Documento.CargarDelegacion(ds)
                Dim _tipoSectores As List(Of Clases.TipoSector) = GenesisSaldos.Documento.CargarTipoSector(ds)
                Dim _plantas As List(Of Clases.Planta) = GenesisSaldos.Documento.CargarPlanta(ds, _identificadorDelegacion, _tipoSectores)
                Dim _sectores As List(Of Clases.Sector) = GenesisSaldos.Documento.CargarSector(ds, _identificadorDelegacion, _delegaciones, _tipoSectores, _plantas)

                _cuentas = New List(Of Clases.Cuenta)
                _cuentas = GenesisSaldos.Documento.CargarCuenta(ds)

            End If

            Dim _remesas As ObservableCollection(Of Clases.Remesa) = Nothing

            If ds.Tables.Contains("ele_rc_elementos") AndAlso ds.Tables("ele_rc_elementos").Rows.Count > 0 AndAlso
                ds.Tables.Contains("ele_rc_carac_tipo_sector") AndAlso ds.Tables("ele_rc_carac_tipo_sector").Rows.Count > 0 Then

                Dim dtRemesas As DataTable = ds.Tables("ele_rc_elementos")

                For Each rowRemesa In dtRemesas.Rows

                    Dim _identificadorRemesa As String = Util.AtribuirValorObj(rowRemesa("R_OID_REMESA"), GetType(String))
                    Dim _identificadorDocumentoRemesa As String = Util.AtribuirValorObj(rowRemesa("R_OID_DOCUMENTO_DE"), GetType(String))

                    If String.IsNullOrEmpty(_identificadorDocumento) OrElse (Not String.IsNullOrEmpty(_identificadorDocumento) AndAlso _identificadorDocumento = _identificadorDocumentoRemesa) Then

                        If _remesas Is Nothing Then
                            _remesas = New ObservableCollection(Of Clases.Remesa)
                        End If

                        If _remesas.FirstOrDefault(Function(r) r.Identificador = _identificadorRemesa AndAlso r.IdentificadorDocumento = _identificadorDocumentoRemesa) Is Nothing Then

                            Dim remesa As New Clases.Remesa
                            Dim objValoresTermino As List(Of KeyValuePair(Of String, String)) = Nothing
                            Dim IdentificadorRemesaOrigen As String = String.Empty

                            With remesa

                                .Identificador = _identificadorRemesa
                                .IdentificadorExterno = Util.AtribuirValorObj(rowRemesa("R_OID_EXTERNO"), GetType(String))
                                .Cuenta = _cuentas.FirstOrDefault(Function(c) c.Identificador = rowRemesa("R_OID_CUENTA"))
                                .CuentaSaldo = _cuentas.FirstOrDefault(Function(c) c.Identificador = If(rowRemesa.Table.Columns.Contains("R_OID_CUENTA_SALDO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowRemesa("R_OID_CUENTA_SALDO"), GetType(String))), rowRemesa("R_OID_CUENTA_SALDO"), Nothing))
                                .Estado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoRemesa)(Util.AtribuirValorObj(rowRemesa("R_COD_ESTADO"), GetType(String)))
                                .FechaHoraCreacion = Util.AtribuirValorObj(rowRemesa("R_GMT_CREACION"), GetType(DateTime))
                                .FechaHoraModificacion = Util.AtribuirValorObj(rowRemesa("R_GMT_MODIFICACION"), GetType(DateTime))
                                .CodigoExterno = Util.AtribuirValorObj(rowRemesa("R_COD_EXTERNO"), GetType(String))
                                .FechaHoraTransporte = Util.AtribuirValorObj(rowRemesa("R_FYH_TRANSPORTE"), GetType(DateTime))
                                .FechaHoraInicioConteo = Util.AtribuirValorObj(rowRemesa("R_FYH_CONTEO_INICIO"), GetType(DateTime))
                                .FechaHoraFinConteo = Util.AtribuirValorObj(rowRemesa("R_FYH_CONTEO_FIN"), GetType(DateTime))
                                .IdentificadorDocumento = Util.AtribuirValorObj(rowRemesa("R_OID_DOCUMENTO_DE"), GetType(String))
                                .Parada = Util.AtribuirValorObj(rowRemesa("R_NEL_PARADA"), GetType(Int64?))
                                .TrabajaPorBulto = Util.AtribuirValorObj(rowRemesa("R_BOL_GESTION_BULTO"), GetType(Boolean))
                                .PuestoResponsable = Util.AtribuirValorObj(rowRemesa("R_COD_PUESTO_RESPONSABLE"), GetType(String))
                                .Ruta = Util.AtribuirValorObj(rowRemesa("R_COD_RUTA"), GetType(String))
                                .UsuarioCreacion = Util.AtribuirValorObj(rowRemesa("R_DES_USUARIO_CREACION"), GetType(String))
                                .UsuarioModificacion = Util.AtribuirValorObj(rowRemesa("R_DES_USUARIO_MODIFICACION"), GetType(String))
                                .UsuarioResponsable = Util.AtribuirValorObj(rowRemesa("R_COD_USUARIO_RESPONSABLE"), GetType(String))
                                .NoEntregue = Util.AtribuirValorObj(rowRemesa("R_BOL_NOENTREGUE"), GetType(Boolean))
                                .EstaAnulado = Util.AtribuirValorObj(rowRemesa("R_BOL_ANULADO"), GetType(Boolean))
                                .RemesaOrigen = New Clases.Remesa
                                .RemesaOrigen.Identificador = Util.AtribuirValorObj(rowRemesa("R_OID_REMESA_ORIGEN"), GetType(String))
                                .EstadoDocumentoElemento = Extenciones.RecuperarEnum(Of Enumeradores.EstadoDocumentoElemento)(Util.AtribuirValorObj(rowRemesa("R_COD_ESTADO_DOCXELEMENTO"), GetType(String)))
                                .ConfiguracionNivelSaldos = If(rowRemesa.Table.Columns.Contains("R_COD_NIVEL_DETALLE"), Extenciones.RecuperarEnum(Of Enumeradores.ConfiguracionNivelSaldos)(Util.AtribuirValorObj(rowRemesa("R_COD_NIVEL_DETALLE"), GetType(String))), Nothing)
                                If rowRemesa("R_ROWVER") Is DBNull.Value Then
                                    .Rowver = 0
                                Else
                                    .Rowver = Util.AtribuirValorObj(rowRemesa("R_ROWVER"), GetType(Int64))
                                End If

                                If (Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowRemesa("R_OID_REMESA_PADRE"), GetType(String)))) Then
                                    .IdentificadorRemesaPadre = Util.AtribuirValorObj(rowRemesa("R_OID_REMESA_PADRE"), GetType(String))
                                    .ElementoPadre = New Clases.Remesa With {.Identificador = Util.AtribuirValorObj(rowRemesa("R_OID_REMESA_PADRE"), GetType(String))}
                                End If
                                If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowRemesa("R_COD_CAJERO"), GetType(String))) Then
                                    .DatosATM = New Clases.ATM With {.CodigoCajero = Util.AtribuirValorObj(rowRemesa("R_COD_CAJERO"), GetType(String)),
                                                                     .ModalidadRecogida = Enumeradores.ModalidadRecogida.NoDefinido}
                                End If
                                If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowRemesa("R_OID_IAC"), GetType(String))) Then
                                    .GrupoTerminosIAC = New Clases.GrupoTerminosIAC With {.Identificador = Util.AtribuirValorObj(rowRemesa("R_OID_IAC"), GetType(String))}
                                End If
                                .CantidadBultos = Util.AtribuirValorObj(rowRemesa("R_NEL_CANTIDAD_BULTOS"), GetType(Integer))

                                Dim result = dtRemesas.Select("B_OID_BULTO <> '' AND R_OID_REMESA = '" & .Identificador & "' AND R_OID_DOCUMENTO_DE = '" & Util.AtribuirValorObj(rowRemesa("R_OID_DOCUMENTO_DE"), GetType(String)) & "'")
                                Dim dtBultos = result.CopyToDataTable()
                                .Bultos = Genesis.Bulto.cargarBultos(dtBultos, _cuentas)
                                .Divisas = New ObservableCollection(Of Clases.Divisa)

                            End With

                            _remesas.Add(remesa)

                        End If
                    End If

                Next

                poblarValoresRemesas(_remesas, ds)

            End If

            Return _remesas
        End Function

        Private Shared Sub poblarValoresRemesas(ByRef _remesas As ObservableCollection(Of Clases.Remesa), ds As DataSet)

            If _remesas IsNot Nothing AndAlso _remesas.Count > 0 Then

                Dim dtValoresDetalladaEfectivo As DataTable = ds.Tables("ele_rc_valores_det_efectivo")
                Dim dtValoresDetalladaMedioPago As DataTable = ds.Tables("ele_rc_valores_det_medio_pago")
                Dim dtValoresTotales As DataTable = ds.Tables("ele_rc_valores_totales")
                Dim dtListaValor As DataTable = ds.Tables("ele_rc_lista_valor")
                Dim dtIAC As DataTable = ds.Tables("ele_rc_iac")
                Dim dtTerminosIAC As DataTable = ds.Tables("ele_rc_terminos_iac")
                Dim dtValorTerminoIAC As DataTable = ds.Tables("ele_rc_valor_termino_iac")
                Dim dtDivisas As DataTable = ds.Tables("ele_rc_divisas")
                Dim dtDenominaciones As DataTable = ds.Tables("ele_rc_denominaciones")
                Dim dtMedioPago As DataTable = ds.Tables("ele_rc_medio_pago")
                Dim dtUnidadMedida As DataTable = ds.Tables("ele_rc_unidad_medida")
                Dim dtCalidad As DataTable = ds.Tables("ele_rc_calidad")
                Dim dtValorTermino As DataTable = ds.Tables("ele_rc_valor_termino")

                Dim _divisas As ObservableCollection(Of Clases.Divisa) = Nothing
                Dim grupoTerminosIAC As ObservableCollection(Of Clases.GrupoTerminosIAC) = Nothing

                _divisas = Genesis.Divisas.cargarDivisas(dtDivisas, dtDenominaciones, dtMedioPago)
                grupoTerminosIAC = cargarGrupoTerminosIAC(dtIAC, dtTerminosIAC)

                For Each _remesa In _remesas

                    ' Preenche valores Remesa

                    ' Cargar Divisas
                    CargarValoresDivisa_v2(_remesa.Divisas, _divisas, dtValoresDetalladaEfectivo, dtValoresDetalladaMedioPago,
                                        dtValoresTotales, dtUnidadMedida, dtCalidad, dtValorTermino, _remesa.Identificador, Nothing, Nothing)

                    ' Cargar GrupoTerminosIAC
                    If _remesa.GrupoTerminosIAC IsNot Nothing AndAlso Not String.IsNullOrEmpty(_remesa.GrupoTerminosIAC.Identificador) Then
                        cargarGrupoTerminosIACElemento(_remesa.GrupoTerminosIAC, grupoTerminosIAC, dtValorTerminoIAC, _remesa.GrupoTerminosIAC.Identificador, Enumeradores.TipoElemento.Remesa)
                    End If

                    ' Preenche valores Bultos
                    If _remesa.Bultos IsNot Nothing AndAlso _remesa.Bultos.Count > 0 Then

                        For Each _bulto In _remesa.Bultos

                            ' Cargar Divisas
                            CargarValoresDivisa_v2(_bulto.Divisas, _divisas, dtValoresDetalladaEfectivo, dtValoresDetalladaMedioPago,
                                                dtValoresTotales, dtUnidadMedida, dtCalidad, dtValorTermino, _remesa.Identificador, _bulto.Identificador, Nothing)

                            ' Cargar TipoServicio
                            _bulto.TipoServicio = cargarTipoServicio(dtListaValor, _remesa.Identificador, _bulto.Identificador)

                            ' Cargar TipoFormato
                            _bulto.TipoFormato = cargarTipoFormato(dtListaValor, _remesa.Identificador, _bulto.Identificador, Nothing)

                            ' Cargar GrupoTerminosIAC
                            If _bulto.GrupoTerminosIAC IsNot Nothing AndAlso Not String.IsNullOrEmpty(_bulto.GrupoTerminosIAC.Identificador) Then
                                cargarGrupoTerminosIACElemento(_bulto.GrupoTerminosIAC, grupoTerminosIAC, dtValorTerminoIAC, _bulto.GrupoTerminosIAC.Identificador, Enumeradores.TipoElemento.Bulto)
                            End If

                            ' Cargar GrupoTerminosIAC
                            If _bulto.GrupoTerminosIACParciales IsNot Nothing AndAlso Not String.IsNullOrEmpty(_bulto.GrupoTerminosIACParciales.Identificador) Then
                                cargarGrupoTerminosIACElemento(_bulto.GrupoTerminosIACParciales, grupoTerminosIAC, Nothing, _bulto.GrupoTerminosIACParciales.Identificador, Enumeradores.TipoElemento.Parcial)
                            End If

                            ' Preenche valores Parcial
                            If _bulto.Parciales IsNot Nothing AndAlso _bulto.Parciales.Count > 0 Then

                                For Each _parcial In _bulto.Parciales

                                    ' Cargar Divisas
                                    CargarValoresDivisa_v2(_parcial.Divisas, _divisas, dtValoresDetalladaEfectivo, dtValoresDetalladaMedioPago,
                                                dtValoresTotales, dtUnidadMedida, dtCalidad, dtValorTermino, _remesa.Identificador, _bulto.Identificador, _parcial.Identificador)

                                    ' Cargar TipoFormato
                                    _parcial.TipoFormato = cargarTipoFormato(dtListaValor, _remesa.Identificador, _bulto.Identificador, _parcial.Identificador)

                                    ' Cargar GrupoTerminosIAC
                                    If _parcial.GrupoTerminosIAC IsNot Nothing AndAlso Not String.IsNullOrEmpty(_parcial.GrupoTerminosIAC.Identificador) Then
                                        cargarGrupoTerminosIACElemento(_parcial.GrupoTerminosIAC, grupoTerminosIAC, dtValorTerminoIAC, _parcial.GrupoTerminosIAC.Identificador, Enumeradores.TipoElemento.Parcial)
                                    End If

                                Next

                            End If

                        Next

                    End If

                Next


            End If

        End Sub

        Public Shared Sub poblarRemesas_Confirmar(ByRef _remesa As Clases.Remesa,
                                                  ds As DataSet)

            If ds.Tables.Contains("ele_rc_elementos") AndAlso ds.Tables("ele_rc_elementos").Rows.Count > 0 Then

                Dim dtRemesas As DataTable = ds.Tables("ele_rc_elementos")

                Dim rRemesa() As DataRow = dtRemesas.Select("OID_REMESA ='" & _remesa.Identificador & "'")
                If rRemesa IsNot Nothing AndAlso rRemesa.Count > 0 Then
                    Dim rowRemesa As DataRow = rRemesa(0)
                    With _remesa

                        .Identificador = Util.AtribuirValorObj(rowRemesa("OID_REMESA"), GetType(String))
                        .IdentificadorExterno = Util.AtribuirValorObj(rowRemesa("R_OID_EXTERNO"), GetType(String))
                        .Estado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoRemesa)(Util.AtribuirValorObj(rowRemesa("R_COD_ESTADO"), GetType(String)))
                        .FechaHoraCreacion = Util.AtribuirValorObj(rowRemesa("R_GMT_CREACION"), GetType(DateTime))
                        .FechaHoraModificacion = Util.AtribuirValorObj(rowRemesa("R_GMT_MODIFICACION"), GetType(DateTime))
                        .CodigoExterno = Util.AtribuirValorObj(rowRemesa("R_COD_EXTERNO"), GetType(String))
                        .FechaHoraTransporte = Util.AtribuirValorObj(rowRemesa("R_FYH_TRANSPORTE"), GetType(DateTime))
                        .FechaHoraInicioConteo = Util.AtribuirValorObj(rowRemesa("R_FYH_CONTEO_INICIO"), GetType(DateTime))
                        .FechaHoraFinConteo = Util.AtribuirValorObj(rowRemesa("R_FYH_CONTEO_FIN"), GetType(DateTime))
                        .IdentificadorDocumento = Util.AtribuirValorObj(rowRemesa("R_OID_DOCUMENTO"), GetType(String))
                        .Parada = Util.AtribuirValorObj(rowRemesa("R_NEL_PARADA"), GetType(Int64?))
                        .TrabajaPorBulto = Util.AtribuirValorObj(rowRemesa("BOL_GESTION_BULTO"), GetType(Boolean))
                        .PuestoResponsable = Util.AtribuirValorObj(rowRemesa("R_COD_PUESTO_RESPONSABLE"), GetType(String))
                        .Ruta = Util.AtribuirValorObj(rowRemesa("R_COD_RUTA"), GetType(String))
                        .UsuarioCreacion = Util.AtribuirValorObj(rowRemesa("R_DES_USUARIO_CREACION"), GetType(String))
                        .UsuarioModificacion = Util.AtribuirValorObj(rowRemesa("R_DES_USUARIO_MODIFICACION"), GetType(String))
                        .UsuarioResponsable = Util.AtribuirValorObj(rowRemesa("R_COD_USUARIO_RESPONSABLE"), GetType(String))
                        .NoEntregue = Util.AtribuirValorObj(rowRemesa("BOL_NOENTREGUE"), GetType(Boolean))
                        .EstaAnulado = Util.AtribuirValorObj(rowRemesa("BOL_ANULADO"), GetType(Boolean))
                        .RemesaOrigen = New Clases.Remesa
                        .RemesaOrigen.Identificador = Util.AtribuirValorObj(rowRemesa("R_OID_REMESA_ORIGEN"), GetType(String))
                        .EstadoDocumentoElemento = Extenciones.RecuperarEnum(Of Enumeradores.EstadoDocumentoElemento)(Util.AtribuirValorObj(rowRemesa("COD_ESTADO_DOCXELEMENTO_R"), GetType(String)))
                        .ConfiguracionNivelSaldos = If(rowRemesa.Table.Columns.Contains("R_COD_NIVEL_DETALLE"), Extenciones.RecuperarEnum(Of Enumeradores.ConfiguracionNivelSaldos)(Util.AtribuirValorObj(rowRemesa("R_COD_NIVEL_DETALLE"), GetType(String))), Nothing)
                        If rowRemesa("R_ROWVER") Is DBNull.Value Then
                            .Rowver = 0
                        Else
                            .Rowver = Util.AtribuirValorObj(rowRemesa("R_ROWVER"), GetType(Int64))
                        End If

                        If (Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowRemesa("OID_REMESA_PADRE"), GetType(String)))) Then
                            .IdentificadorRemesaPadre = Util.AtribuirValorObj(rowRemesa("OID_REMESA_PADRE"), GetType(String))
                            .ElementoPadre = New Clases.Remesa With {.Identificador = Util.AtribuirValorObj(rowRemesa("OID_REMESA_PADRE"), GetType(String))}
                        End If
                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowRemesa("R_COD_CAJERO"), GetType(String))) Then
                            .DatosATM = New Clases.ATM With {.CodigoCajero = Util.AtribuirValorObj(rowRemesa("R_COD_CAJERO"), GetType(String)),
                                                             .ModalidadRecogida = Enumeradores.ModalidadRecogida.NoDefinido}
                        End If
                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowRemesa("R_OID_IAC"), GetType(String))) Then
                            .GrupoTerminosIAC = New Clases.GrupoTerminosIAC With {.Identificador = Util.AtribuirValorObj(rowRemesa("R_OID_IAC"), GetType(String))}
                        End If

                        For Each _bulto In _remesa.Bultos

                            Dim rBulto() As DataRow = dtRemesas.Select("OID_BULTO ='" & _bulto.Identificador & "'")
                            If rBulto IsNot Nothing AndAlso rBulto.Count > 0 Then
                                Dim rowBultos As DataRow = rBulto(0)
                                With _bulto

                                    .Estado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoBulto)(Util.AtribuirValorObj(rowBultos("B_COD_ESTADO"), GetType(String)))
                                    .Identificador = Util.AtribuirValorObj(rowBultos("OID_BULTO"), GetType(String))
                                    .IdentificadorExterno = Util.AtribuirValorObj(rowBultos("B_OID_EXTERNO"), GetType(String))
                                    .IdentificadorDocumento = Util.AtribuirValorObj(rowBultos("B_OID_DOCUMENTO"), GetType(String))
                                    .FechaHoraCreacion = Util.AtribuirValorObj(rowBultos("B_GMT_CREACION"), GetType(DateTime))
                                    .FechaHoraModificacion = Util.AtribuirValorObj(rowBultos("B_GMT_MODIFICACION"), GetType(DateTime))
                                    .FechaProcessoLegado = Util.AtribuirValorObj(rowBultos("B_FYH_PROCESO_LEGADO"), GetType(DateTime))
                                    .FechaHoraTransporte = Util.AtribuirValorObj(rowBultos("R_FYH_TRANSPORTE"), GetType(DateTime))
                                    .FechaHoraInicioConteo = Util.AtribuirValorObj(rowBultos("B_FYH_CONTEO_INICIO"), GetType(DateTime))
                                    .FechaHoraFinConteo = Util.AtribuirValorObj(rowBultos("B_FYH_CONTEO_FIN"), GetType(DateTime))
                                    .CodigoBolsa = Util.AtribuirValorObj(rowBultos("B_COD_BOLSA"), GetType(String))
                                    .CantidadParciales = Util.AtribuirValorObj(rowBultos("B_NEL_CANTIDAD_PARCIALES"), GetType(Integer))
                                    .Cuadrado = Util.AtribuirValorObj(rowBultos("BOL_CUADRADO"), GetType(Boolean))
                                    .Precintos = New ObservableCollection(Of String)
                                    .Precintos.Add(Util.AtribuirValorObj(rowBultos("B_COD_PRECINTO"), GetType(String)))
                                    .PuestoResponsable = Util.AtribuirValorObj(rowBultos("B_COD_PUESTO_RESPONSABLE"), GetType(String))
                                    .TipoUbicacion = If(String.IsNullOrEmpty(Util.AtribuirValorObj(rowBultos("B_TipoUbicacion"), GetType(String))), Nothing,
                                                      Extenciones.RecuperarEnum(Of Enumeradores.TipoUbicacion)(Util.AtribuirValorObj(rowBultos("B_TipoUbicacion"), GetType(String))))
                                    .UsuarioCreacion = Util.AtribuirValorObj(rowBultos("B_DES_USUARIO_CREACION"), GetType(String))
                                    .UsuarioModificacion = Util.AtribuirValorObj(rowBultos("B_DES_USUARIO_MODIFICACION"), GetType(String))
                                    .UsuarioResponsable = Util.AtribuirValorObj(rowBultos("B_COD_USUARIO_RESPONSABLE"), GetType(String))
                                    .EstadoDocumentoElemento = Extenciones.RecuperarEnum(Of Enumeradores.EstadoDocumentoElemento)(Util.AtribuirValorObj(rowBultos("COD_ESTADO_DOCXELEMENTO_B"), GetType(String)))
                                    .ConfiguracionNivelSaldos = If(rowBultos.Table.Columns.Contains("B_COD_NIVEL_DETALLE"), Extenciones.RecuperarEnum(Of Enumeradores.ConfiguracionNivelSaldos)(Util.AtribuirValorObj(rowBultos("B_COD_NIVEL_DETALLE"), GetType(String))), Nothing)
                                    If (Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowBultos("OID_BULTO_PADRE"), GetType(String)))) Then
                                        .ElementoPadre = New Clases.Remesa With {.Identificador = Util.AtribuirValorObj(rowBultos("OID_BULTO_PADRE"), GetType(String))}
                                    End If

                                    If rowBultos("B_ROWVER") Is DBNull.Value Then
                                        .Rowver = 0
                                    Else
                                        .Rowver = Util.AtribuirValorObj(rowBultos("B_ROWVER"), GetType(Int64))
                                    End If

                                    If _bulto.Parciales IsNot Nothing AndAlso _bulto.Parciales.Count > 0 Then

                                        For Each _parcial In _bulto.Parciales

                                            Dim rParcial() As DataRow = dtRemesas.Select("OID_PARCIAL ='" & _parcial.Identificador & "'")
                                            If rParcial IsNot Nothing AndAlso rParcial.Count > 0 Then
                                                Dim rowParcial As DataRow = rParcial(0)
                                                With _parcial

                                                    .Identificador = Util.AtribuirValorObj(rowParcial("OID_PARCIAL"), GetType(String))
                                                    .IdentificadorExterno = Util.AtribuirValorObj(rowParcial("P_OID_EXTERNO"), GetType(String))
                                                    .EsFicticio = False
                                                    .Estado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoParcial)(Util.AtribuirValorObj(rowParcial("P_COD_ESTADO"), GetType(String)))
                                                    .FechaHoraCreacion = Util.AtribuirValorObj(rowParcial("P_GMT_CREACION"), GetType(DateTime))
                                                    .FechaHoraFinConteo = Util.AtribuirValorObj(rowParcial("P_FYH_CONTEO_FIN"), GetType(DateTime))
                                                    .FechaHoraInicioConteo = Util.AtribuirValorObj(rowParcial("P_FYH_CONTEO_INICIO"), GetType(DateTime))
                                                    .FechaHoraModificacion = Util.AtribuirValorObj(rowParcial("P_GMT_MODIFICACION"), GetType(DateTime))
                                                    .Precintos = New ObservableCollection(Of String)
                                                    .Precintos.Add(Util.AtribuirValorObj(rowParcial("P_COD_PRECINTO"), GetType(String)))
                                                    .PuestoResponsable = Util.AtribuirValorObj(rowParcial("P_COD_PUESTO_RESPONSABLE"), GetType(String))
                                                    .Secuencia = Util.AtribuirValorObj(rowParcial("P_NEC_SECUENCIA"), GetType(Integer))
                                                    .UsuarioCreacion = Util.AtribuirValorObj(rowParcial("P_DES_USUARIO_CREACION"), GetType(String))
                                                    .UsuarioModificacion = Util.AtribuirValorObj(rowParcial("P_DES_USUARIO_MODIFICACION"), GetType(String))
                                                    .UsuarioResponsable = Util.AtribuirValorObj(rowParcial("P_COD_USUARIO_RESPONSABLE"), GetType(String))

                                                End With
                                            End If

                                        Next

                                    End If

                                End With

                            End If

                        Next

                    End With

                End If

            End If



        End Sub

#End Region
















        Public Shared Function ObtenerRemesas(filtro As Clases.Transferencias.Filtro,
                                        Optional ByRef log As StringBuilder = Nothing) As ObservableCollection(Of Clases.Remesa)

            Dim remesas As ObservableCollection(Of Clases.Remesa) = Nothing
            Dim Tiempo As DateTime = Now

            If filtro IsNot Nothing Then

                Dim dtRemesas As DataTable = Nothing

                Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim QueryInner As New StringBuilder
                    Dim QueryWhere As New StringBuilder
                    QueryWhere.Append(" WHERE ")
                    Dim QuerySelect As New StringBuilder
                    Dim QueryOrder As New StringBuilder
                    cmd.CommandText = My.Resources.ObtenerRemesas

                    ' Monta query Cuenta
                    Util.PreencherQueryCuenta(filtro, QueryWhere, cmd)
                    QueryWhere = QueryWhere.Replace(" WHERE  AND ", " WHERE ")

                    If filtro.ExcluirSectoresHijos OrElse (filtro.Sector Is Nothing OrElse String.IsNullOrEmpty(filtro.Sector.Identificador)) Then
                        QueryInner.AppendLine(" INNER JOIN GEPR_TSECTOR SE ON SE.OID_SECTOR = CU.OID_SECTOR ")
                        QueryInner.AppendLine(" INNER JOIN GEPR_TTIPO_SECTOR TSE ON TSE.OID_TIPO_SECTOR = SE.OID_TIPO_SECTOR ")
                        QueryInner.AppendLine(" INNER JOIN GEPR_TPLANTA PT ON PT.OID_PLANTA = SE.OID_PLANTA ")
                        QueryInner.AppendLine(" INNER JOIN GEPR_TDELEGACION DEL ON DEL.OID_DELEGACION = PT.OID_DELEGACION ")
                    Else
                        QuerySelect.AppendLine(" ,SE.NIVEL ")
                        QuerySelect.AppendLine(" ,SE.OID_SECTOR_PADRE ")
                        QuerySelect.AppendLine(" ,SE.LINHA ")

                        QueryInner.AppendLine(" JOIN (SELECT SECT.OID_SECTOR, SECT.OID_SECTOR_PADRE, LEVEL AS NIVEL, ROWNUM AS LINHA, ")
                        QueryInner.AppendLine(" SECT.COD_SECTOR, SECT.COD_MIGRACION, SECT.DES_SECTOR, SECT.BOL_ACTIVO, SECT.BOL_CENTRO_PROCESO, SECT.BOL_CONTEO, ")
                        QueryInner.AppendLine(" SECT.BOL_TESORO, SECT.GMT_CREACION, SECT.GMT_MODIFICACION, ")
                        QueryInner.AppendLine(" SECT.BOL_PERMITE_DISPONER_VALOR, SECT.OID_TIPO_SECTOR, SECT.OID_PLANTA ")
                        QueryInner.AppendLine("         FROM GEPR_TSECTOR SECT ")
                        QueryInner.AppendLine("         START WITH 1 = 1 AND SECT.OID_SECTOR = []OID_SECTOR ")
                        QueryInner.AppendLine("         CONNECT BY NOCYCLE PRIOR SECT.OID_SECTOR = SECT.OID_SECTOR_PADRE) SE ON SE.OID_SECTOR = CU.OID_SECTOR ")
                        QueryInner.AppendLine(" INNER JOIN GEPR_TTIPO_SECTOR TSE ON TSE.OID_TIPO_SECTOR = SE.OID_TIPO_SECTOR ")
                        QueryInner.AppendLine(" INNER JOIN GEPR_TPLANTA PT ON PT.OID_PLANTA = SE.OID_PLANTA ")
                        QueryInner.AppendLine(" INNER JOIN GEPR_TDELEGACION DEL ON DEL.OID_DELEGACION = PT.OID_DELEGACION ")
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Objeto_Id, filtro.Sector.Identificador))

                        QueryOrder.AppendLine(" ORDER BY SE.NIVEL, SE.LINHA")
                    End If

                    If filtro.Documento IsNot Nothing AndAlso filtro.Documento.Count > 0 Then

                        Dim documentoIdentificador As New List(Of String)
                        Dim documentoCodigoComprovante As New List(Of String)
                        Dim documentoNumeroExterno As New List(Of String)

                        For Each documento As Clases.Transferencias.FiltroDocumento In filtro.Documento
                            If Not String.IsNullOrEmpty(documento.Identificador) Then
                                documentoIdentificador.Add(documento.Identificador)
                            End If
                            If Not String.IsNullOrEmpty(documento.CodigoComprovante) Then
                                documentoCodigoComprovante.Add(documento.CodigoComprovante)
                            End If
                            If Not String.IsNullOrEmpty(documento.NumeroExterno) Then
                                documentoNumeroExterno.Add(documento.NumeroExterno)
                            End If
                        Next

                        If documentoIdentificador IsNot Nothing AndAlso documentoIdentificador.Count > 0 Then
                            QueryWhere.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, documentoIdentificador, "OID_DOCUMENTO", cmd, If(QueryWhere.ToString <> " WHERE ", "AND", ""), "DOCR", "DOCR"))
                        End If
                        If documentoCodigoComprovante IsNot Nothing AndAlso documentoCodigoComprovante.Count > 0 Then
                            QueryWhere.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, documentoCodigoComprovante, "COD_COMPROBANTE", cmd, If(QueryWhere.ToString <> " WHERE ", "AND", ""), "DOCR", "DOCR"))
                        End If
                        If documentoNumeroExterno IsNot Nothing AndAlso documentoNumeroExterno.Count > 0 Then
                            QueryWhere.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, documentoNumeroExterno, "COD_EXTERNO", cmd, If(QueryWhere.ToString <> " WHERE ", "AND", ""), "DOCR", "DOCR"))
                        End If

                    End If

                    If filtro.Remesa IsNot Nothing Then

                        Dim RemesaIdentificador As New List(Of String)
                        Dim RemesaCodigoExterno As New List(Of String)
                        Dim RemesaCodigoRuta As New List(Of String)
                        Dim RemesaFechaAltaDesde As Nullable(Of DateTime)
                        Dim RemesaFechaAltaHasta As Nullable(Of DateTime)

                        For Each remesa In filtro.Remesa
                            If Not String.IsNullOrEmpty(remesa.Identificador) Then
                                RemesaIdentificador.Add(remesa.Identificador)
                            End If
                            If Not String.IsNullOrEmpty(remesa.CodigoExterno) Then
                                RemesaCodigoExterno.Add(remesa.CodigoExterno)
                            End If
                            If Not String.IsNullOrEmpty(remesa.CodigoRuta) Then
                                RemesaCodigoRuta.Add(remesa.CodigoRuta)
                            End If
                            If remesa.FechaAltaDesde IsNot Nothing Then
                                Dim _FechaAltaDesde As DateTime = remesa.FechaAltaDesde
                                RemesaFechaAltaDesde = _FechaAltaDesde.QuieroGrabarGMTZeroEnLaBBDD(filtro.Delegacion)
                            End If
                            If remesa.FechaAltaHasta IsNot Nothing Then
                                Dim _FechaAltaHasta As DateTime = remesa.FechaAltaHasta
                                RemesaFechaAltaHasta = _FechaAltaHasta.QuieroGrabarGMTZeroEnLaBBDD(filtro.Delegacion)
                            End If
                        Next

                        If RemesaIdentificador IsNot Nothing AndAlso RemesaIdentificador.Count > 0 Then
                            QueryWhere.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, RemesaIdentificador, "OID_REMESA", cmd, If(QueryWhere.ToString <> " WHERE ", "AND", ""), "DER", "REM"))
                        End If
                        If RemesaCodigoExterno IsNot Nothing AndAlso RemesaCodigoExterno.Count > 0 Then
                            QueryWhere.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, RemesaCodigoExterno, "COD_EXTERNO", cmd, If(QueryWhere.ToString <> " WHERE ", "AND", ""), "R", "REM"))
                        End If
                        If RemesaCodigoRuta IsNot Nothing AndAlso RemesaCodigoRuta.Count > 0 Then
                            QueryWhere.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, RemesaCodigoRuta, "COD_RUTA", cmd, If(QueryWhere.ToString <> " WHERE ", "AND", ""), "R", "REM"))
                        End If
                        If RemesaFechaAltaDesde IsNot Nothing Then

                            QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND R.GMT_CREACION >= TO_TIMESTAMP_TZ([]GMT_CREACION_REMESA_DESDE || ' +00:00','DD/MM/YYYY HH24:MI:SS TZH:TZM') ",
                                                  " R.GMT_CREACION >= TO_TIMESTAMP_TZ([]GMT_CREACION_REMESA_DESDE || ' +00:00','DD/MM/YYYY HH24:MI:SS TZH:TZM') "))

                            Dim _FechaAltaDesde As DateTime = filtro.Remesa.First.FechaAltaDesde
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_CREACION_REMESA_DESDE", ProsegurDbType.Descricao_Curta,
                                                                                _FechaAltaDesde.QuieroGrabarGMTZeroEnLaBBDD(filtro.Delegacion).ToString("dd/MM/yyyy HH:mm:ss")))


                        End If
                        If RemesaFechaAltaHasta IsNot Nothing Then

                            QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND R.GMT_CREACION < TO_TIMESTAMP_TZ([]GMT_CREACION_REMESA_HASTA || ' +00:00','DD/MM/YYYY HH24:MI:SS TZH:TZM') ",
                                                  " R.GMT_CREACION < TO_TIMESTAMP_TZ([]GMT_CREACION_REMESA_HASTA || ' +00:00','DD/MM/YYYY HH24:MI:SS TZH:TZM') "))

                            Dim _FechaAltaHasta As DateTime = filtro.Remesa.First.FechaAltaHasta
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_CREACION_REMESA_HASTA", ProsegurDbType.Descricao_Curta,
                                                                                _FechaAltaHasta.QuieroGrabarGMTZeroEnLaBBDD(filtro.Delegacion).ToString("dd/MM/yyyy HH:mm:ss")))
                        End If
                    End If

                    Dim CodigosValorBulto As New List(Of String)
                    Dim CodigosValorParcial As New List(Of String)
                    If filtro.Bulto IsNot Nothing Then

                        Dim BultoIdentificador As New List(Of String)
                        Dim BultoPrecintos As New List(Of String)
                        Dim BultoTipoServicioCodigo As New List(Of String)
                        Dim BultoFechaAltaDesde As Nullable(Of DateTime)
                        Dim BultoFechaAltaHasta As Nullable(Of DateTime)

                        For Each objBulto In filtro.Bulto

                            If Not String.IsNullOrEmpty(objBulto.Identificador) Then
                                BultoIdentificador.Add(objBulto.Identificador)
                            End If

                            If objBulto.Precintos IsNot Nothing Then
                                For Each precinto In objBulto.Precintos
                                    BultoPrecintos.Add(precinto)
                                Next
                            End If

                            If objBulto.TipoFormato IsNot Nothing Then
                                CodigosValorBulto.Add(objBulto.TipoFormato.Codigo)
                            End If

                            If objBulto.TipoServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(objBulto.TipoServicio.Codigo) Then
                                BultoTipoServicioCodigo.Add(objBulto.TipoServicio.Codigo)
                            End If

                            If objBulto.FechaAltaDesde IsNot Nothing Then
                                Dim _FechaAltaDesde As DateTime = objBulto.FechaAltaDesde
                                BultoFechaAltaDesde = _FechaAltaDesde.QuieroGrabarGMTZeroEnLaBBDD(filtro.Delegacion)
                            End If
                            If objBulto.FechaAltaHasta IsNot Nothing Then
                                Dim _FechaAltaHasta As DateTime = objBulto.FechaAltaHasta
                                BultoFechaAltaHasta = _FechaAltaHasta.QuieroGrabarGMTZeroEnLaBBDD(filtro.Delegacion)
                            End If
                        Next

                        If BultoIdentificador IsNot Nothing AndAlso BultoIdentificador.Count > 0 Then
                            QueryWhere.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, BultoIdentificador, "OID_BULTO", cmd, If(QueryWhere.ToString <> " WHERE ", "AND", ""), "B", "BUL"))
                        End If

                        If BultoPrecintos IsNot Nothing AndAlso BultoPrecintos.Count > 0 Then
                            QueryWhere.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, BultoPrecintos, "COD_PRECINTO", cmd, If(QueryWhere.ToString <> " WHERE ", "AND", ""), "B", "BUL"))
                        End If

                        If BultoTipoServicioCodigo IsNot Nothing AndAlso BultoTipoServicioCodigo.Count > 0 Then
                            QueryWhere.Append(If(QueryWhere.ToString <> " WHERE ", "AND", ""))
                            QueryWhere.Append(" B.OID_BULTO IN")
                            QueryWhere.Append("(SELECT LVL.OID_BULTO FROM SAPR_TLISTA_VALORXELEMENTO LVL " & Environment.NewLine &
                            " INNER JOIN GEPR_TLISTA_VALOR LV ON LV.OID_LISTA_VALOR = LVL.OID_LISTA_VALOR " & Environment.NewLine &
                            " AND LV.OID_LISTA_TIPO = LVL.OID_LISTA_TIPO  " & Environment.NewLine &
                            " INNER JOIN GEPR_TLISTA_TIPO LT ON LT.OID_LISTA_TIPO = LV.OID_LISTA_TIPO " & Environment.NewLine &
                            " AND LT.COD_TIPO = '01' " & Environment.NewLine &
                            " AND LV.COD_VALOR =[]TIPO_SERVICIO_BULTO)")

                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "TIPO_SERVICIO_BULTO", ProsegurDbType.Descricao_Curta, BultoTipoServicioCodigo.FirstOrDefault))
                        End If

                        If BultoFechaAltaDesde IsNot Nothing Then

                            QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND B.GMT_CREACION >= TO_TIMESTAMP_TZ([]GMT_CREACION_BULTO_DESDE || ' +00:00','DD/MM/YYYY HH24:MI:SS TZH:TZM') ",
                                                  " B.GMT_CREACION >= TO_TIMESTAMP_TZ([]GMT_CREACION_BULTO_DESDE || ' +00:00','DD/MM/YYYY HH24:MI:SS TZH:TZM') "))

                            Dim _FechaAltaDesde As DateTime = filtro.Bulto.First.FechaAltaDesde
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_CREACION_BULTO_DESDE", ProsegurDbType.Descricao_Curta,
                                                                                _FechaAltaDesde.QuieroGrabarGMTZeroEnLaBBDD(filtro.Delegacion).ToString("dd/MM/yyyy HH:mm:ss")))

                        End If

                        If BultoFechaAltaHasta IsNot Nothing Then

                            QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND B.GMT_CREACION < TO_TIMESTAMP_TZ([]GMT_CREACION_BULTO_HASTA || ' +00:00','DD/MM/YYYY HH24:MI:SS TZH:TZM') ",
                                                  " B.GMT_CREACION < TO_TIMESTAMP_TZ([]GMT_CREACION_BULTO_HASTA || ' +00:00','DD/MM/YYYY HH24:MI:SS TZH:TZM') "))

                            Dim _FechaAltaHasta As DateTime = filtro.Bulto.First.FechaAltaHasta
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_CREACION_BULTO_HASTA", ProsegurDbType.Descricao_Curta,
                                                                                _FechaAltaHasta.QuieroGrabarGMTZeroEnLaBBDD(filtro.Delegacion).ToString("dd/MM/yyyy HH:mm:ss")))

                        End If
                    End If

                    If filtro.Parcial IsNot Nothing Then

                        Dim ParcialIdentificador As New List(Of String)
                        Dim ParcialPrecintos As New List(Of String)
                        Dim ParcialFechaAltaDesde As Nullable(Of DateTime)
                        Dim ParcialFechaAltaHasta As Nullable(Of DateTime)

                        For Each objParcial In filtro.Parcial
                            If Not String.IsNullOrEmpty(objParcial.Identificador) Then
                                ParcialIdentificador.Add(objParcial.Identificador)
                            End If

                            If objParcial.Precintos IsNot Nothing Then
                                For Each precinto In objParcial.Precintos
                                    ParcialPrecintos.Add(precinto)
                                Next
                            End If

                            If objParcial.TipoFormato IsNot Nothing Then
                                CodigosValorParcial.Add(objParcial.TipoFormato.Codigo)
                            End If

                            If objParcial.FechaAltaDesde IsNot Nothing Then
                                ParcialFechaAltaDesde = objParcial.FechaAltaDesde
                            End If
                            If objParcial.FechaAltaHasta IsNot Nothing Then
                                ParcialFechaAltaHasta = objParcial.FechaAltaHasta
                            End If
                        Next


                        If ParcialIdentificador IsNot Nothing AndAlso ParcialIdentificador.Count > 0 Then
                            QueryWhere.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, ParcialIdentificador, "OID_PARCIAL", cmd, If(QueryWhere.ToString <> " WHERE ", "AND", ""), "P", "PAR"))
                        End If

                        If ParcialPrecintos IsNot Nothing AndAlso ParcialPrecintos.Count > 0 Then
                            QueryWhere.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, ParcialPrecintos, "COD_PRECINTO", cmd, If(QueryWhere.ToString <> " WHERE ", "AND", ""), "P", "PAR"))
                        End If

                        If ParcialFechaAltaDesde IsNot Nothing Then

                            QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND P.GMT_CREACION >= TO_TIMESTAMP_TZ([]GMT_CREACION_PARCIAL_DESDE || ' +00:00','DD/MM/YYYY HH24:MI:SS TZH:TZM') ",
                                                  " P.GMT_CREACION >= TO_TIMESTAMP_TZ([]GMT_CREACION_PARCIAL_DESDE || ' +00:00','DD/MM/YYYY HH24:MI:SS TZH:TZM') "))

                            Dim _FechaAltaDesde As DateTime = filtro.Parcial.First.FechaAltaDesde
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_CREACION_PARCIAL_DESDE", ProsegurDbType.Descricao_Curta,
                                                                                _FechaAltaDesde.QuieroGrabarGMTZeroEnLaBBDD(filtro.Delegacion).ToString("dd/MM/yyyy HH:mm:ss")))

                        End If
                        If ParcialFechaAltaHasta IsNot Nothing Then

                            QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND P.GMT_CREACION < TO_TIMESTAMP_TZ([]GMT_CREACION_PARCIAL_HASTA || ' +00:00','DD/MM/YYYY HH24:MI:SS TZH:TZM') ",
                                                  " P.GMT_CREACION < TO_TIMESTAMP_TZ([]GMT_CREACION_PARCIAL_HASTA || ' +00:00','DD/MM/YYYY HH24:MI:SS TZH:TZM') "))

                            Dim _FechaAltaHasta As DateTime = filtro.Parcial.First.FechaAltaHasta
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_CREACION_PARCIAL_HASTA", ProsegurDbType.Descricao_Curta,
                                                                                _FechaAltaHasta.QuieroGrabarGMTZeroEnLaBBDD(filtro.Delegacion).ToString("dd/MM/yyyy HH:mm:ss")))

                        End If

                    End If

                    If CodigosValorBulto.Count > 0 Then

                        QueryInner.Append("INNER JOIN SAPR_TLISTA_VALORXELEMENTO LVE ON LVE.OID_REMESA = B.OID_REMESA AND LVE.OID_BULTO = B.OID_BULTO AND LVE.OID_PARCIAL IS NULL" & Environment.NewLine &
                                          "INNER JOIN GEPR_TLISTA_TIPO LT ON LT.OID_LISTA_TIPO = LVE.OID_LISTA_TIPO " & Environment.NewLine &
                                          "INNER JOIN GEPR_TLISTA_VALOR LV ON LV.OID_LISTA_VALOR = LVE.OID_LISTA_VALOR")

                        QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND (", " ("))
                        QueryWhere.Append(" LT.COD_TIPO = '" & Prosegur.Genesis.Comon.Enumeradores.TipoListaValor.TipoFormato.RecuperarValor() & "' ")
                        QueryWhere.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, CodigosValorBulto, "COD_VALOR", cmd, "AND", "LV", "LVA"))
                        QueryWhere.Append(")")

                    ElseIf CodigosValorParcial.Count > 0 Then

                        QueryInner.Append("INNER JOIN SAPR_TLISTA_VALORXELEMENTO LVE ON LVE.OID_REMESA = B.OID_REMESA AND LVE.OID_BULTO = B.OID_BULTO AND LVE.OID_PARCIAL = P.OID_PARCIAL " & Environment.NewLine &
                                              "INNER JOIN GEPR_TLISTA_TIPO LT ON LT.OID_LISTA_TIPO = LVE.OID_LISTA_TIPO " & Environment.NewLine &
                                              "INNER JOIN GEPR_TLISTA_VALOR LV ON LV.OID_LISTA_VALOR = LVE.OID_LISTA_VALOR")

                        QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND (", " ("))
                        QueryWhere.Append(" LT.COD_TIPO = '" & Prosegur.Genesis.Comon.Enumeradores.TipoListaValor.TipoFormato.RecuperarValor() & "' ")
                        QueryWhere.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, CodigosValorParcial, "COD_VALOR", cmd, "AND", "LV", "LVA"))
                        QueryWhere.Append(")")

                    End If


                    ' Não é permitido Remesas sem Bultos
                    QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND (B.OID_BULTO IS NOT NULL OR B.OID_BULTO <> '') ", " (B.OID_BULTO IS NOT NULL OR B.OID_BULTO <> '') "))

                    If filtro.RestringirEstados Then
                        ' Não é permitido retornar documentos nos estados:
                        QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND DOCR.COD_ESTADO NOT IN ('" & Enumeradores.EstadoDocumento.EnCurso.RecuperarValor() & "', '" & Enumeradores.EstadoDocumento.Confirmado.RecuperarValor() & "') ", " DOCR.COD_ESTADO NOT IN ('" & Enumeradores.EstadoDocumento.EnCurso.RecuperarValor() & "', '" & Enumeradores.EstadoDocumento.Confirmado.RecuperarValor() & "') "))

                        ' Só é permitido retornar remesas e bultos nos estados:
                        QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND R.COD_ESTADO = '" & Enumeradores.EstadoRemesa.Pendiente.RecuperarValor() & "' ", " R.COD_ESTADO = '" & Enumeradores.EstadoRemesa.Pendiente.RecuperarValor() & "' "))
                        QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND B.COD_ESTADO = '" & Enumeradores.EstadoBulto.Cerrado.RecuperarValor() & "' ", " B.COD_ESTADO = '" & Enumeradores.EstadoBulto.Cerrado.RecuperarValor() & "' "))

                        ' Não é permitido bultos já incluidos em outros documentos, e que seu estadoDocumentoElemento for diferente de C
                        Dim strExcluirBultosUtilizados As String = " DER.COD_ESTADO_DOCXELEMENTO ='" & Enumeradores.EstadoDocumentoElemento.Concluido.RecuperarValor() & "' AND B.OID_BULTO NOT IN(SELECT DOCE.OID_BULTO " &
                       " FROM SAPR_TDOCUMENTOXELEMENTO DOCE WHERE(DOCE.OID_BULTO Is Not NULL) " &
                       " AND DOCE.COD_ESTADO_DOCXELEMENTO ='" & Enumeradores.EstadoDocumentoElemento.EnTransito.RecuperarValor() & "') "
                        QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND " & strExcluirBultosUtilizados, strExcluirBultosUtilizados))
                    End If

                    If filtro.EsGestionBulto IsNot Nothing Then
                        QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND R.BOL_GESTION_BULTO = []BOL_GESTION_BULTO", "R.BOL_GESTION_BULTO = []BOL_GESTION_BULTO"))
                        QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND B.BOL_GESTION_BULTO = []BOL_GESTION_BULTO", "B.BOL_GESTION_BULTO = []BOL_GESTION_BULTO"))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_GESTION_BULTO", ProsegurDbType.Logico, filtro.EsGestionBulto))
                    End If

                    If QueryWhere.ToString = " WHERE " Then
                        QueryWhere = New StringBuilder
                    End If

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPOUBICACION", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(Enumeradores.Tipos.TipoUbicacion)))

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, QueryInner.ToString(), QueryWhere.ToString(), QuerySelect.ToString(), QueryOrder.ToString()))

                    cmd.CommandType = CommandType.Text

                    dtRemesas = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                End Using

                If log IsNot Nothing Then
                    log.AppendLine("______Tiempo 'ObtenerRemesasPorDocumentosEnBase_v2': " & Now.Subtract(Tiempo).ToString() & "; ")
                End If

                If dtRemesas IsNot Nothing AndAlso dtRemesas.Rows.Count > 0 Then

                    Tiempo = Now
                    remesas = cargarRemesas_SinProcedure(dtRemesas)

                    If log IsNot Nothing Then
                        log.AppendLine("______Tiempo 'cargarRemesas_v2': " & Now.Subtract(Tiempo).ToString() & "; ")
                    End If

                    Tiempo = Now
                    CargarValoresRemesas(remesas, dtRemesas)

                    If log IsNot Nothing Then
                        log.AppendLine("______Tiempo 'CargarValoresRemesas': " & Now.Subtract(Tiempo).ToString() & "; ")
                    End If

                End If

            End If

            Return remesas
        End Function

        ''' <summary>
        ''' Obsoleto: deve morrer e passar tudo para procedure
        ''' </summary>
        ''' <param name="identificadoresDocumentos"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerRemesasPorDocumentos_SinProcedure(identificadoresDocumentos As List(Of String),
                                                     Optional ByRef log As StringBuilder = Nothing) As ObservableCollection(Of Clases.Remesa)

            Dim remesas As ObservableCollection(Of Clases.Remesa) = Nothing
            Dim dtRemesas As DataTable = Nothing
            Dim Tiempo As DateTime = Now

            If identificadoresDocumentos IsNot Nothing AndAlso identificadoresDocumentos.Count > 0 Then

                dtRemesas = ObtenerRemesasPorDocumentosEnBase(identificadoresDocumentos)

                If log IsNot Nothing Then
                    log.AppendLine("______Tiempo 'ObtenerRemesasPorDocumentosEnBase_v2': " & Now.Subtract(Tiempo).ToString() & "; ")
                End If

                If dtRemesas IsNot Nothing AndAlso dtRemesas.Rows.Count > 0 Then

                    Tiempo = Now
                    remesas = cargarRemesas_SinProcedure(dtRemesas)

                    If log IsNot Nothing Then
                        log.AppendLine("______Tiempo 'cargarRemesas_v2': " & Now.Subtract(Tiempo).ToString() & "; ")
                    End If

                    Tiempo = Now
                    CargarValoresRemesas(remesas, dtRemesas)

                    If log IsNot Nothing Then
                        log.AppendLine("______Tiempo 'CargarValoresRemesas': " & Now.Subtract(Tiempo).ToString() & "; ")
                    End If

                End If

            End If

            Return remesas
        End Function

        Public Shared Function ObtenerRemesasPorDocumentosEnBase(ByRef identificadoresDocumentos As List(Of String)) As DataTable

            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.RemesasObtener_v2
                Dim filtro As String = ""

                If identificadoresDocumentos IsNot Nothing Then
                    If identificadoresDocumentos.Count = 1 Then
                        filtro &= " AND DOCR.OID_DOCUMENTO = []OID_DOCUMENTO "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Descricao_Curta, identificadoresDocumentos(0)))
                    ElseIf identificadoresDocumentos.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresDocumentos, "OID_DOCUMENTO", cmd, "AND", "DOCR", , False)
                    End If
                End If

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPOUBICACION", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(Enumeradores.Tipos.TipoUbicacion)))

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt

        End Function

        Public Shared Function RellenarRemesaEnviarEfectivoPrecintado(dtRemesas As DataTable) As ObservableCollection(Of Clases.Remesa)

            Dim remesas As ObservableCollection(Of Clases.Remesa) = Nothing

            If dtRemesas IsNot Nothing AndAlso dtRemesas.Rows.Count > 0 Then

                remesas = New ObservableCollection(Of Clases.Remesa)

                Dim identificadoresCuenta As List(Of String) = CargarIdentificadoresCuentas(dtRemesas)

                Dim cuentas As ObservableCollection(Of Clases.Cuenta) = GenesisSaldos.Cuenta.ObtenerCuentasPorIdentificadores(identificadoresCuenta, Enumeradores.TipoCuenta.Ambos, "RellenarRemesa")

                For Each IdentificadorRemesa In (From dr As DataRow In dtRemesas.Rows Select dr("OID_REMESA") Distinct)

                    Dim rowRemesa = (From dr As DataRow In dtRemesas.Rows Where dr("OID_REMESA") = IdentificadorRemesa).FirstOrDefault

                    If remesas.Find(Function(r) r.Identificador = rowRemesa("OID_REMESA")) Is Nothing Then

                        Dim remesa As New Clases.Remesa
                        Dim objValoresTermino As List(Of KeyValuePair(Of String, String)) = Nothing
                        Dim IdentificadorRemesaOrigen As String = String.Empty

                        With remesa

                            .Identificador = Util.AtribuirValorObj(rowRemesa("OID_REMESA"), GetType(String))
                            .Cuenta = cuentas.FirstOrDefault(Function(c) c.Identificador = rowRemesa("R_OID_CUENTA"))
                            .CuentaSaldo = cuentas.FirstOrDefault(Function(c) c.Identificador = If(rowRemesa.Table.Columns.Contains("R_OID_CUENTA_SALDO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowRemesa("R_OID_CUENTA_SALDO"), GetType(String))), rowRemesa("R_OID_CUENTA_SALDO"), Nothing))
                            .FechaHoraCreacion = Util.AtribuirValorObj(rowRemesa("R_GMT_CREACION"), GetType(DateTime))
                            .FechaHoraModificacion = Util.AtribuirValorObj(rowRemesa("R_GMT_MODIFICACION"), GetType(DateTime))
                            .UsuarioCreacion = Util.AtribuirValorObj(rowRemesa("R_DES_USUARIO_CREACION"), GetType(String))
                            .UsuarioModificacion = Util.AtribuirValorObj(rowRemesa("R_DES_USUARIO_MODIFICACION"), GetType(String))
                            .CodigoExterno = Util.AtribuirValorObj(rowRemesa("R_COD_EXTERNO"), GetType(String))
                            .IdentificadorDocumento = Util.AtribuirValorObj(rowRemesa("R_OID_DOCUMENTO"), GetType(String))
                            .TrabajaPorBulto = Util.AtribuirValorObj(rowRemesa("BOL_GESTION_BULTO"), GetType(Boolean))
                            If rowRemesa("R_ROWVER") Is DBNull.Value Then
                                .Rowver = 0
                            Else
                                .Rowver = Util.AtribuirValorObj(rowRemesa("R_ROWVER"), GetType(Int64))
                            End If

                            Dim result = From d In dtRemesas.AsEnumerable()
                                         Where d.Field(Of String)("OID_REMESA") = .Identificador
                                         Select d
                            Dim dtBultos = result.CopyToDataTable()
                            .Bultos = Genesis.Bulto.RellenarBultosEnviarEfectivoPrecintado(dtBultos, cuentas)
                            .Divisas = New ObservableCollection(Of Clases.Divisa)

                        End With

                        remesas.Add(remesa)
                    End If

                Next

            End If

            Return remesas
        End Function

        ''' <summary>
        ''' Obsoleto: deve morrer e passar tudo para procedure
        ''' </summary>
        ''' <param name="dtRemesas"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function cargarRemesas_SinProcedure(dtRemesas As DataTable) As ObservableCollection(Of Clases.Remesa)

            Dim remesas As ObservableCollection(Of Clases.Remesa) = Nothing

            If dtRemesas IsNot Nothing AndAlso dtRemesas.Rows.Count > 0 Then

                remesas = New ObservableCollection(Of Clases.Remesa)

                Dim identificadoresCuenta As List(Of String) = CargarIdentificadoresCuentas(dtRemesas)

                Dim cuentas As ObservableCollection(Of Clases.Cuenta) = GenesisSaldos.Cuenta.ObtenerCuentasPorIdentificadores(identificadoresCuenta, Enumeradores.TipoCuenta.Ambos, "ObtenerRemesas")

                Dim dvRemesas As DataTable = dtRemesas.DefaultView.ToTable(True, "OID_REMESA")

                For Each rowIdRemesa In dvRemesas.Rows

                    Dim rowRemesa As DataRow = dtRemesas.Select("OID_REMESA = '" + rowIdRemesa("OID_REMESA").ToString() + "'")(0)

                    Dim remesa As New Clases.Remesa
                    Dim objValoresTermino As List(Of KeyValuePair(Of String, String)) = Nothing
                    Dim IdentificadorRemesaOrigen As String = String.Empty


                    With remesa
                        If rowRemesa("R_ROWVER") Is DBNull.Value Then
                            .Rowver = 0
                        Else
                            .Rowver = Util.AtribuirValorObj(rowRemesa("R_ROWVER"), GetType(Int64))
                        End If

                        .Identificador = Util.AtribuirValorObj(rowRemesa("OID_REMESA"), GetType(String))
                        .IdentificadorExterno = Util.AtribuirValorObj(rowRemesa("R_OID_EXTERNO"), GetType(String))
                        .Cuenta = cuentas.FirstOrDefault(Function(c) c.Identificador = rowRemesa("R_OID_CUENTA"))
                        .CuentaSaldo = cuentas.FirstOrDefault(Function(c) c.Identificador = If(rowRemesa.Table.Columns.Contains("R_OID_CUENTA_SALDO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowRemesa("R_OID_CUENTA_SALDO"), GetType(String))), rowRemesa("R_OID_CUENTA_SALDO"), Nothing))
                        .Estado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoRemesa)(Util.AtribuirValorObj(rowRemesa("R_COD_ESTADO"), GetType(String)))
                        .FechaHoraCreacion = Util.AtribuirValorObj(rowRemesa("R_GMT_CREACION"), GetType(DateTime))
                        .FechaHoraModificacion = Util.AtribuirValorObj(rowRemesa("R_GMT_MODIFICACION"), GetType(DateTime))
                        .CodigoExterno = Util.AtribuirValorObj(rowRemesa("R_COD_EXTERNO"), GetType(String))
                        .FechaHoraTransporte = Util.AtribuirValorObj(rowRemesa("R_FYH_TRANSPORTE"), GetType(DateTime))
                        .FechaHoraInicioConteo = Util.AtribuirValorObj(rowRemesa("R_FYH_CONTEO_INICIO"), GetType(DateTime))
                        .FechaHoraFinConteo = Util.AtribuirValorObj(rowRemesa("R_FYH_CONTEO_FIN"), GetType(DateTime))
                        .IdentificadorDocumento = Util.AtribuirValorObj(rowRemesa("R_OID_DOCUMENTO"), GetType(String))
                        .Parada = Util.AtribuirValorObj(rowRemesa("R_NEL_PARADA"), GetType(Int64?))
                        .TrabajaPorBulto = Util.AtribuirValorObj(rowRemesa("BOL_GESTION_BULTO"), GetType(Boolean))
                        .PuestoResponsable = Util.AtribuirValorObj(rowRemesa("R_COD_PUESTO_RESPONSABLE"), GetType(String))
                        .Ruta = Util.AtribuirValorObj(rowRemesa("R_COD_RUTA"), GetType(String))
                        .UsuarioCreacion = Util.AtribuirValorObj(rowRemesa("R_DES_USUARIO_CREACION"), GetType(String))
                        .UsuarioModificacion = Util.AtribuirValorObj(rowRemesa("R_DES_USUARIO_MODIFICACION"), GetType(String))
                        .UsuarioResponsable = Util.AtribuirValorObj(rowRemesa("R_COD_USUARIO_RESPONSABLE"), GetType(String))
                        .NoEntregue = Util.AtribuirValorObj(rowRemesa("BOL_NOENTREGUE"), GetType(Boolean))
                        .EstaAnulado = Util.AtribuirValorObj(rowRemesa("BOL_ANULADO"), GetType(Boolean))
                        .RemesaOrigen = New Clases.Remesa
                        .RemesaOrigen.Identificador = Util.AtribuirValorObj(rowRemesa("R_OID_REMESA_ORIGEN"), GetType(String))
                        .EstadoDocumentoElemento = Extenciones.RecuperarEnum(Of Enumeradores.EstadoDocumentoElemento)(Util.AtribuirValorObj(rowRemesa("COD_ESTADO_DOCXELEMENTO_R"), GetType(String)))
                        .ConfiguracionNivelSaldos = If(rowRemesa.Table.Columns.Contains("R_COD_NIVEL_DETALLE"), Extenciones.RecuperarEnum(Of Enumeradores.ConfiguracionNivelSaldos)(Util.AtribuirValorObj(rowRemesa("R_COD_NIVEL_DETALLE"), GetType(String))), Nothing)
                        If (Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowRemesa("OID_REMESA_PADRE"), GetType(String)))) Then
                            .IdentificadorRemesaPadre = Util.AtribuirValorObj(rowRemesa("OID_REMESA_PADRE"), GetType(String))
                            .ElementoPadre = New Clases.Remesa With {.Identificador = Util.AtribuirValorObj(rowRemesa("OID_REMESA_PADRE"), GetType(String))}
                        End If
                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowRemesa("R_COD_CAJERO"), GetType(String))) Then
                            .DatosATM = New Clases.ATM With {.CodigoCajero = Util.AtribuirValorObj(rowRemesa("R_COD_CAJERO"), GetType(String)),
                                                             .ModalidadRecogida = Enumeradores.ModalidadRecogida.NoDefinido}
                        End If
                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowRemesa("R_OID_IAC"), GetType(String))) Then
                            .GrupoTerminosIAC = New Clases.GrupoTerminosIAC With {.Identificador = Util.AtribuirValorObj(rowRemesa("R_OID_IAC"), GetType(String))}
                        End If

                        Dim result = From d In dtRemesas.AsEnumerable()
                                     Where d.Field(Of String)("OID_REMESA") = .Identificador
                                     Select d
                        Dim dtBultos = result.CopyToDataTable()
                        .Bultos = Genesis.Bulto.cargarBultos_SinProcedure(dtBultos, cuentas)
                        .Divisas = New ObservableCollection(Of Clases.Divisa)

                    End With

                    remesas.Add(remesa)

                Next

            End If

            Return remesas
        End Function

        Private Shared Function CargarIdentificadoresCuentas(dtRemesas As DataTable) As List(Of String)

            Dim identificadoresCuenta As New List(Of String)

            For Each rowRemesa In dtRemesas.Rows

                If rowRemesa.Table.Columns.Contains("R_OID_CUENTA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowRemesa("R_OID_CUENTA"), GetType(String))) Then
                    Dim identificadorCuenta As String = Util.AtribuirValorObj(rowRemesa("R_OID_CUENTA"), GetType(String))
                    If Not identificadoresCuenta.Contains(identificadorCuenta) Then
                        identificadoresCuenta.Add(identificadorCuenta)
                    End If
                End If
                If rowRemesa.Table.Columns.Contains("R_OID_CUENTA_SALDO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowRemesa("R_OID_CUENTA_SALDO"), GetType(String))) Then
                    Dim identificadorCuenta As String = Util.AtribuirValorObj(rowRemesa("R_OID_CUENTA_SALDO"), GetType(String))
                    If Not identificadoresCuenta.Contains(identificadorCuenta) Then
                        identificadoresCuenta.Add(identificadorCuenta)
                    End If
                End If

                If rowRemesa.Table.Columns.Contains("B_OID_CUENTA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowRemesa("B_OID_CUENTA"), GetType(String))) Then
                    Dim identificadorCuenta As String = Util.AtribuirValorObj(rowRemesa("B_OID_CUENTA"), GetType(String))
                    If Not identificadoresCuenta.Contains(identificadorCuenta) Then
                        identificadoresCuenta.Add(identificadorCuenta)
                    End If
                End If
                If rowRemesa.Table.Columns.Contains("B_OID_CUENTA_SALDO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowRemesa("B_OID_CUENTA_SALDO"), GetType(String))) Then
                    Dim identificadorCuenta As String = Util.AtribuirValorObj(rowRemesa("B_OID_CUENTA_SALDO"), GetType(String))
                    If Not identificadoresCuenta.Contains(identificadorCuenta) Then
                        identificadoresCuenta.Add(identificadorCuenta)
                    End If
                End If

            Next

            Return identificadoresCuenta

        End Function

        Private Shared Sub CargarValoresRemesasEnviarEfectivoPrecintado(ByRef remesas As ObservableCollection(Of Clases.Remesa), ByRef dtRemesas As DataTable)

            If remesas IsNot Nothing AndAlso remesas.Count > 0 AndAlso dtRemesas IsNot Nothing AndAlso dtRemesas.Rows.Count > 0 Then

                Dim identificadoresRemesa As List(Of String) = dtRemesas.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_REMESA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_REMESA"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_REMESA")) _
                                                                         .Distinct() _
                                                                         .ToList()

                Dim dtValoresDenominaciones As DataTable = Nothing
                Dim dtValoresMedioPago As DataTable = Nothing
                Dim dtValoresTotales As DataTable = Nothing
                Dim dtTipoServicio As DataTable = Nothing
                Dim dtTipoFormato As DataTable = Nothing
                Dim divisas As ObservableCollection(Of Clases.Divisa) = Nothing

                Dim TDenominaciones As New Task(Sub()
                                                    dtValoresDenominaciones = Genesis.Denominacion.ObtenerValoresElemento_v3(identificadoresRemesa)
                                                End Sub)
                TDenominaciones.Start()

                Dim TMedioPago As New Task(Sub()
                                               dtValoresMedioPago = Genesis.MedioPago.ObtenerValoresElemento_v3(identificadoresRemesa)
                                           End Sub)
                TMedioPago.Start()

                Dim TTotales As New Task(Sub()
                                             dtValoresTotales = Genesis.Totales.ObtenerValoresElemento_v3(identificadoresRemesa)
                                         End Sub)
                TTotales.Start()


                Task.WaitAll(New Task() {TDenominaciones, TMedioPago, TTotales})


                Dim identificadoresDivisas As List(Of String) = dtValoresDenominaciones.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_DIVISA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DIVISA"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_DIVISA")) _
                                                                         .Distinct() _
                                                                         .ToList()

                identificadoresDivisas.AddRange(dtValoresMedioPago.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_DIVISA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DIVISA"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_DIVISA")) _
                                                                         .Distinct() _
                                                                         .ToList())

                identificadoresDivisas.AddRange(dtValoresTotales.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_DIVISA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DIVISA"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_DIVISA")) _
                                                                         .Distinct() _
                                                                         .ToList())

                Dim identificadoresDenominaciones As List(Of String) = dtValoresDenominaciones.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_DENOMINACION") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DENOMINACION"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_DENOMINACION")) _
                                                                         .Distinct() _
                                                                         .ToList()

                Dim identificadoresMediosPagos As List(Of String) = dtValoresMedioPago.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_MEDIO_PAGO") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_MEDIO_PAGO"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_MEDIO_PAGO")) _
                                                                         .Distinct() _
                                                                         .ToList()


                ' Preenche valores Remesa
                For Each _remesa In remesas

                    ' Cargar Divisas
                    CargarValoresDivisa_v3(_remesa.Divisas, divisas, dtValoresDenominaciones, dtValoresMedioPago,
                                        dtValoresTotales, _remesa.Identificador, Nothing, Nothing)

                    ' Preenche valores Bultos
                    If _remesa.Bultos IsNot Nothing AndAlso _remesa.Bultos.Count > 0 Then

                        For Each _bulto In _remesa.Bultos

                            ' Cargar Divisas
                            CargarValoresDivisa_v3(_bulto.Divisas, divisas, dtValoresDenominaciones, dtValoresMedioPago,
                                                dtValoresTotales, _remesa.Identificador, _bulto.Identificador, Nothing)

                        Next

                    End If


                Next

            End If

        End Sub

        Private Shared Sub CargarValoresRemesas(ByRef remesas As ObservableCollection(Of Clases.Remesa), ByRef dtRemesas As DataTable)

            If remesas IsNot Nothing AndAlso remesas.Count > 0 AndAlso dtRemesas IsNot Nothing AndAlso dtRemesas.Rows.Count > 0 Then

                Dim identificadoresRemesa As List(Of String) = dtRemesas.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_REMESA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_REMESA"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_REMESA")) _
                                                                         .Distinct() _
                                                                         .ToList()

                Dim identificadoresBulto As List(Of String) = dtRemesas.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_BULTO") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_BULTO"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_BULTO")) _
                                                                         .Distinct() _
                                                                         .ToList()

                Dim identificadoresParcial As List(Of String) = dtRemesas.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_PARCIAL") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_PARCIAL"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_PARCIAL")) _
                                                                         .Distinct() _
                                                                         .ToList()

                Dim identificadoresIAC As List(Of String) = dtRemesas.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("R_OID_IAC") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("R_OID_IAC"))) _
                                                                         .Select(Function(r) r.Field(Of String)("R_OID_IAC")) _
                                                                         .Distinct() _
                                                                         .ToList()

                identificadoresIAC.AddRange(dtRemesas.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("B_OID_IAC") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("B_OID_IAC"))) _
                                                                         .Select(Function(r) r.Field(Of String)("B_OID_IAC")) _
                                                                         .Distinct() _
                                                                         .ToList())

                identificadoresIAC.AddRange(dtRemesas.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("B_OID_IAC_PARCIALES") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("B_OID_IAC_PARCIALES"))) _
                                                                         .Select(Function(r) r.Field(Of String)("B_OID_IAC_PARCIALES")) _
                                                                         .Distinct() _
                                                                         .ToList())

                identificadoresIAC.AddRange(dtRemesas.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("P_OID_IAC") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("P_OID_IAC"))) _
                                                                         .Select(Function(r) r.Field(Of String)("P_OID_IAC")) _
                                                                         .Distinct() _
                                                                         .ToList())

                Dim dtValoresDenominaciones As DataTable = Nothing
                Dim dtValoresMedioPago As DataTable = Nothing
                Dim dtValoresTotales As DataTable = Nothing
                Dim dtTipoServicio As DataTable = Nothing
                Dim dtTipoFormato As DataTable = Nothing
                Dim dtIAC As DataTable = Nothing
                Dim dtTerminosIAC As DataTable = Nothing
                Dim dtValorTerminoIAC As DataTable = Nothing
                Dim divisas As ObservableCollection(Of Clases.Divisa) = Nothing

                Dim TDenominaciones As New Task(Sub()
                                                    dtValoresDenominaciones = Genesis.Denominacion.ObtenerValoresElemento_v2(identificadoresRemesa)
                                                End Sub)
                TDenominaciones.Start()

                Dim TMedioPago As New Task(Sub()
                                               dtValoresMedioPago = Genesis.MedioPago.ObtenerValoresElemento_v2(identificadoresRemesa)
                                           End Sub)
                TMedioPago.Start()

                Dim TTotales As New Task(Sub()
                                             dtValoresTotales = Genesis.Totales.ObtenerValoresElemento_v2(identificadoresRemesa)
                                         End Sub)
                TTotales.Start()

                Dim TTipoServicio As New Task(Sub()
                                                  dtTipoServicio = Genesis.TipoServicio.ObtenerTipoServicioDeElementos(identificadoresRemesa)
                                              End Sub)
                TTipoServicio.Start()

                Dim TTipoFormato As New Task(Sub()
                                                 dtTipoFormato = Genesis.TipoFormato.ObtenerTipoServicioDeElementos(identificadoresRemesa)
                                             End Sub)
                TTipoFormato.Start()

                Dim TIAC As New Task(Sub()
                                         dtIAC = Genesis.GrupoTerminosIAC.ObtenerGrupoTerminosIAC(identificadoresIAC)
                                     End Sub)
                TIAC.Start()

                Dim TTerminoIAC As New Task(Sub()
                                                dtTerminosIAC = Genesis.TerminoIAC.ObtenerTerminosIAC(identificadoresIAC)
                                            End Sub)
                TTerminoIAC.Start()

                Dim TValorTerminoIAC As New Task(Sub()
                                                     dtValorTerminoIAC = Genesis.ValorTerminoIAC.ObtenerValorTerminoIAC(identificadoresRemesa, identificadoresBulto, identificadoresParcial)
                                                 End Sub)
                TValorTerminoIAC.Start()


                Task.WaitAll(New Task() {TDenominaciones, TMedioPago, TTotales, TTipoServicio, TTipoFormato, TIAC, TTerminoIAC, TValorTerminoIAC})


                Dim identificadoresDivisas As List(Of String) = dtValoresDenominaciones.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_DIVISA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DIVISA"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_DIVISA")) _
                                                                         .Distinct() _
                                                                         .ToList()

                identificadoresDivisas.AddRange(dtValoresMedioPago.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_DIVISA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DIVISA"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_DIVISA")) _
                                                                         .Distinct() _
                                                                         .ToList())

                identificadoresDivisas.AddRange(dtValoresTotales.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_DIVISA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DIVISA"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_DIVISA")) _
                                                                         .Distinct() _
                                                                         .ToList())

                Dim identificadoresDenominaciones As List(Of String) = dtValoresDenominaciones.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_DENOMINACION") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DENOMINACION"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_DENOMINACION")) _
                                                                         .Distinct() _
                                                                         .ToList()

                Dim identificadoresMediosPagos As List(Of String) = dtValoresMedioPago.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_MEDIO_PAGO") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_MEDIO_PAGO"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_MEDIO_PAGO")) _
                                                                         .Distinct() _
                                                                         .ToList()

                Dim identificadoresUnidadMedida As List(Of String) = dtValoresDenominaciones.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_UNIDAD_MEDIDA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_UNIDAD_MEDIDA"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_UNIDAD_MEDIDA")) _
                                                                         .Distinct() _
                                                                         .ToList()

                Dim identificadoresCalidad As List(Of String) = dtValoresDenominaciones.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_CALIDAD") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_CALIDAD"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_CALIDAD")) _
                                                                         .Distinct() _
                                                                         .ToList()

                Dim identificadorContadoDeclaradoMedioPago As List(Of String) = dtValoresMedioPago.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_CONTADO_MEDIO_PAGO") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_CONTADO_MEDIO_PAGO"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_CONTADO_MEDIO_PAGO")) _
                                                                         .Distinct() _
                                                                         .ToList()

                identificadorContadoDeclaradoMedioPago.AddRange(dtValoresMedioPago.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_DECLARADO_MEDIO_PAGO") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DECLARADO_MEDIO_PAGO"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_DECLARADO_MEDIO_PAGO")) _
                                                                         .Distinct() _
                                                                         .ToList())

                divisas = Genesis.Divisas.ObtenerDivisasPorIdentificadores_v2(identificadoresDivisas,
                                                                              identificadoresDenominaciones,
                                                                              identificadoresMediosPagos)

                Dim dtUnidadMedida As DataTable = Nothing
                Dim dtCalidad As DataTable = Nothing
                Dim dtValorTermino As DataTable = Nothing
                Dim grupoTerminosIAC As ObservableCollection(Of Clases.GrupoTerminosIAC) = Nothing

                Dim TUnidadMedida As New Task(Sub()
                                                  dtUnidadMedida = UnidadMedida.ObtenerUnidadMedidaPorDivisa_v2(Nothing, identificadoresUnidadMedida)
                                              End Sub)
                TUnidadMedida.Start()

                Dim TCalidad As New Task(Sub()
                                             dtCalidad = Calidad.ObtenerCalidadPorDivisa_v2(Nothing, identificadoresCalidad)
                                         End Sub)
                TCalidad.Start()

                Dim TValorTermino As New Task(Sub()
                                                  dtValorTermino = ValorTerminoMedioPago.ObtenerValorTerminoMedioPagoElemento_v2(identificadorContadoDeclaradoMedioPago)
                                              End Sub)
                TValorTermino.Start()

                Dim TcargarGrupoTerminosIAC As New Task(Sub()
                                                            grupoTerminosIAC = cargarGrupoTerminosIAC(dtIAC, dtTerminosIAC)
                                                        End Sub)
                TcargarGrupoTerminosIAC.Start()


                Task.WaitAll(New Task() {TUnidadMedida, TCalidad, TValorTermino, TcargarGrupoTerminosIAC})

                Parallel.ForEach(remesas, Sub(_remesa As Clases.Remesa)

                                              ' Preenche valores Remesa

                                              ' Cargar Divisas
                                              CargarValoresDivisa_v2(_remesa.Divisas, divisas, dtValoresDenominaciones, dtValoresMedioPago,
                                                                  dtValoresTotales, dtUnidadMedida, dtCalidad, dtValorTermino, _remesa.Identificador, Nothing, Nothing)

                                              ' Cargar GrupoTerminosIAC
                                              If _remesa.GrupoTerminosIAC IsNot Nothing AndAlso Not String.IsNullOrEmpty(_remesa.GrupoTerminosIAC.Identificador) Then
                                                  cargarGrupoTerminosIACElemento(_remesa.GrupoTerminosIAC, grupoTerminosIAC, dtValorTerminoIAC, _remesa.GrupoTerminosIAC.Identificador, Enumeradores.TipoElemento.Remesa)
                                              End If

                                              ' Preenche valores Bultos
                                              If _remesa.Bultos IsNot Nothing AndAlso _remesa.Bultos.Count > 0 Then

                                                  Parallel.ForEach(_remesa.Bultos, Sub(_bulto As Clases.Bulto)

                                                                                       ' Cargar Divisas
                                                                                       CargarValoresDivisa_v2(_bulto.Divisas, divisas, dtValoresDenominaciones, dtValoresMedioPago,
                                                                                                           dtValoresTotales, dtUnidadMedida, dtCalidad, dtValorTermino, _remesa.Identificador, _bulto.Identificador, Nothing)

                                                                                       ' Cargar TipoServicio
                                                                                       _bulto.TipoServicio = cargarTipoServicio(dtTipoServicio, _remesa.Identificador, _bulto.Identificador)

                                                                                       ' Cargar TipoFormato
                                                                                       _bulto.TipoFormato = cargarTipoFormato(dtTipoFormato, _remesa.Identificador, _bulto.Identificador, Nothing)

                                                                                       ' Cargar GrupoTerminosIAC
                                                                                       If _bulto.GrupoTerminosIAC IsNot Nothing AndAlso Not String.IsNullOrEmpty(_bulto.GrupoTerminosIAC.Identificador) Then
                                                                                           cargarGrupoTerminosIACElemento(_bulto.GrupoTerminosIAC, grupoTerminosIAC, dtValorTerminoIAC, _bulto.GrupoTerminosIAC.Identificador, Enumeradores.TipoElemento.Bulto)
                                                                                       End If

                                                                                       ' Cargar GrupoTerminosIAC
                                                                                       If _bulto.GrupoTerminosIACParciales IsNot Nothing AndAlso Not String.IsNullOrEmpty(_bulto.GrupoTerminosIACParciales.Identificador) Then
                                                                                           cargarGrupoTerminosIACElemento(_bulto.GrupoTerminosIACParciales, grupoTerminosIAC, Nothing, _bulto.GrupoTerminosIACParciales.Identificador, Enumeradores.TipoElemento.Parcial)
                                                                                       End If

                                                                                       ' Preenche valores Parcial
                                                                                       If _bulto.Parciales IsNot Nothing AndAlso _bulto.Parciales.Count > 0 Then

                                                                                           Parallel.ForEach(_bulto.Parciales, Sub(_parcial As Clases.Parcial)

                                                                                                                                  ' Cargar Divisas
                                                                                                                                  CargarValoresDivisa_v2(_parcial.Divisas, divisas, dtValoresDenominaciones, dtValoresMedioPago,
                                                                                                                                              dtValoresTotales, dtUnidadMedida, dtCalidad, dtValorTermino, _remesa.Identificador, _bulto.Identificador, _parcial.Identificador)

                                                                                                                                  ' Cargar TipoFormato
                                                                                                                                  _parcial.TipoFormato = cargarTipoFormato(dtTipoFormato, _remesa.Identificador, _bulto.Identificador, _parcial.Identificador)

                                                                                                                                  ' Cargar GrupoTerminosIAC
                                                                                                                                  If _parcial.GrupoTerminosIAC IsNot Nothing AndAlso Not String.IsNullOrEmpty(_parcial.GrupoTerminosIAC.Identificador) Then
                                                                                                                                      cargarGrupoTerminosIACElemento(_parcial.GrupoTerminosIAC, grupoTerminosIAC, dtValorTerminoIAC, _parcial.GrupoTerminosIAC.Identificador, Enumeradores.TipoElemento.Parcial)
                                                                                                                                  End If

                                                                                                                              End Sub)

                                                                                       End If

                                                                                   End Sub)
                                              End If

                                          End Sub)

            End If

        End Sub

        Private Shared Sub CargarValoresDivisa_v2(ByRef divisas As ObservableCollection(Of Clases.Divisa),
                                       divisasPosibles As ObservableCollection(Of Clases.Divisa),
                                       dtValoresDenominaciones As DataTable,
                                       dtValoresMedioPago As DataTable,
                                       dtValoresTotales As DataTable,
                                       dtUnidadMedida As DataTable,
                                       dtCalidad As DataTable,
                                       dtValorTermino As DataTable,
                                       identificadorRemesa As String,
                                       identificadorBulto As String,
                                       identificadorParcial As String)

            If Not String.IsNullOrEmpty(identificadorRemesa) Then

                Dim divisa As New Clases.Divisa
                Dim objDenominacion As Clases.Denominacion = Nothing
                Dim objMedioPago As Clases.MedioPago = Nothing

                ' Crea filtro
                Dim consulta As String = "OID_REMESA = '" & identificadorRemesa & "'"
                If Not String.IsNullOrEmpty(identificadorBulto) Then
                    consulta &= " AND OID_BULTO = '" & identificadorBulto & "'"
                Else
                    consulta &= " AND OID_BULTO IS NULL "
                End If
                If Not String.IsNullOrEmpty(identificadorParcial) Then
                    consulta &= " AND OID_PARCIAL = '" & identificadorParcial & "'"
                Else
                    consulta &= " AND OID_PARCIAL IS NULL "
                End If

                ' Valores Denominaciones
                If dtValoresDenominaciones IsNot Nothing AndAlso dtValoresDenominaciones.Rows.Count > 0 Then
                    Dim _ValoresDenominaciones = dtValoresDenominaciones.Select(consulta)

                    If _ValoresDenominaciones IsNot Nothing Then

                        For Each _valor In _ValoresDenominaciones

                            If _valor.Table.Columns.Contains("OID_DIVISA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("OID_DIVISA"), GetType(String))) Then

                                divisa = divisas.FirstOrDefault(Function(x) x.Identificador = Util.AtribuirValorObj(_valor("OID_DIVISA"), GetType(String)))

                                If divisa Is Nothing Then
                                    divisa = divisasPosibles.FirstOrDefault(Function(x) x.Identificador = Util.AtribuirValorObj(_valor("OID_DIVISA"), GetType(String))).Clonar()

                                    If divisa Is Nothing Then
                                        Throw New Exception("Error divisa.")
                                    End If

                                    divisas.Add(divisa)
                                End If

                                If _valor.Table.Columns.Contains("OID_DENOMINACION") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("OID_DENOMINACION"), GetType(String))) AndAlso
                                       _valor.Table.Columns.Contains("IMPORTE") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("IMPORTE"), GetType(String))) AndAlso
                                       divisa.Denominaciones IsNot Nothing AndAlso divisa.Denominaciones.Count > 0 Then

                                    objDenominacion = (From den In divisa.Denominaciones Where den.Identificador = Util.AtribuirValorObj(_valor("OID_DENOMINACION"), GetType(String))).FirstOrDefault

                                    If objDenominacion IsNot Nothing Then

                                        If objDenominacion.ValorDenominacion Is Nothing Then objDenominacion.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)

                                        Dim objValor As New Clases.ValorDenominacion
                                        objValor.Cantidad = If(_valor.Table.Columns.Contains("CANTIDAD"), Util.AtribuirValorObj(_valor("CANTIDAD"), GetType(Int64)), Nothing)
                                        objValor.Importe = If(_valor.Table.Columns.Contains("IMPORTE"), Util.AtribuirValorObj(_valor("IMPORTE"), GetType(Decimal)), Nothing)

                                        If _valor.Table.Columns.Contains("TIPO") Then
                                            Select Case _valor("TIPO")
                                                Case "CONTADO_EFECTIVO"
                                                    objValor.TipoValor = Enumeradores.TipoValor.Contado
                                                Case "DIFERENCIA_EFECTIVO"
                                                    objValor.TipoValor = Enumeradores.TipoValor.Diferencia
                                                Case "DECLARADO_EFECTIVO"
                                                    objValor.TipoValor = Enumeradores.TipoValor.Declarado
                                            End Select
                                        End If

                                        If _valor.Table.Columns.Contains("OID_CALIDAD") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("OID_CALIDAD"), GetType(String))) AndAlso
                                            dtCalidad IsNot Nothing AndAlso dtCalidad.Rows.Count > 0 Then

                                            Dim calidad = dtCalidad.Select("OID_CALIDAD = '" & Util.AtribuirValorObj(_valor("OID_CALIDAD"), GetType(String)) & "'")

                                            If calidad IsNot Nothing AndAlso calidad.Count > 0 Then
                                                objValor.Calidad = New Clases.Calidad With {
                                                                .Identificador = If(calidad(0).Table.Columns.Contains("OID_CALIDAD"), Util.AtribuirValorObj(calidad(0)("OID_CALIDAD"), GetType(String)), Nothing),
                                                                .Codigo = If(calidad(0).Table.Columns.Contains("COD_CALIDAD"), Util.AtribuirValorObj(calidad(0)("COD_CALIDAD"), GetType(String)), Nothing),
                                                                .Descripcion = If(calidad(0).Table.Columns.Contains("DES_CALIDAD"), Util.AtribuirValorObj(calidad(0)("DES_CALIDAD"), GetType(String)), Nothing)
                                                    }
                                            End If

                                        End If

                                        If _valor.Table.Columns.Contains("OID_UNIDAD_MEDIDA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("OID_UNIDAD_MEDIDA"), GetType(String))) AndAlso
                                            dtUnidadMedida IsNot Nothing AndAlso dtUnidadMedida.Rows.Count > 0 Then

                                            Dim unidadMedida = dtUnidadMedida.Select("OID_UNIDAD_MEDIDA = '" & Util.AtribuirValorObj(_valor("OID_UNIDAD_MEDIDA"), GetType(String)) & "'")

                                            If unidadMedida IsNot Nothing AndAlso unidadMedida.Count > 0 Then
                                                objValor.UnidadMedida = New Clases.UnidadMedida With {
                                                                .Identificador = If(unidadMedida(0).Table.Columns.Contains("OID_UNIDAD_MEDIDA"), Util.AtribuirValorObj(unidadMedida(0)("OID_UNIDAD_MEDIDA"), GetType(String)), Nothing),
                                                                .Codigo = If(unidadMedida(0).Table.Columns.Contains("COD_UNIDAD_MEDIDA"), Util.AtribuirValorObj(unidadMedida(0)("COD_UNIDAD_MEDIDA"), GetType(String)), Nothing),
                                                                .Descripcion = If(unidadMedida(0).Table.Columns.Contains("DES_UNIDAD_MEDIDA"), Util.AtribuirValorObj(unidadMedida(0)("DES_UNIDAD_MEDIDA"), GetType(String)), Nothing),
                                                                .EsPadron = If(unidadMedida(0).Table.Columns.Contains("BOL_DEFECTO"), Util.AtribuirValorObj(unidadMedida(0)("BOL_DEFECTO"), GetType(Boolean)), Nothing),
                                                                .TipoUnidadMedida = RecuperarEnum(Of Enumeradores.TipoUnidadMedida)(Util.AtribuirValorObj(unidadMedida(0)("COD_TIPO_UNIDAD_MEDIDA"), GetType(String))),
                                                                .ValorUnidad = If(unidadMedida(0).Table.Columns.Contains("NUM_VALOR_UNIDAD"), Util.AtribuirValorObj(unidadMedida(0)("NUM_VALOR_UNIDAD"), GetType(Decimal)), Nothing)
                                                    }
                                            End If
                                        End If

                                        objDenominacion.ValorDenominacion.Add(objValor)

                                    End If

                                End If

                            End If

                        Next

                    End If

                End If

                ' Valores MedioPago
                If dtValoresMedioPago IsNot Nothing AndAlso dtValoresMedioPago.Rows.Count > 0 Then
                    Dim _ValoresMedioPago = dtValoresMedioPago.Select(consulta)

                    If _ValoresMedioPago IsNot Nothing Then

                        For Each _valor In _ValoresMedioPago

                            If _valor.Table.Columns.Contains("OID_DIVISA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("OID_DIVISA"), GetType(String))) Then

                                divisa = divisas.FirstOrDefault(Function(x) x.Identificador = Util.AtribuirValorObj(_valor("OID_DIVISA"), GetType(String)))

                                If divisa Is Nothing Then
                                    divisa = divisasPosibles.FirstOrDefault(Function(x) x.Identificador = Util.AtribuirValorObj(_valor("OID_DIVISA"), GetType(String))).Clonar()

                                    If divisa Is Nothing Then
                                        Throw New Exception("Error divisa.")
                                    End If

                                    divisas.Add(divisa)
                                End If

                                If _valor.Table.Columns.Contains("OID_MEDIO_PAGO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("OID_MEDIO_PAGO"), GetType(String))) AndAlso
                                       _valor.Table.Columns.Contains("IMPORTE") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("IMPORTE"), GetType(String))) AndAlso
                                       divisa.MediosPago IsNot Nothing AndAlso divisa.MediosPago.Count > 0 Then

                                    objMedioPago = (From MP In divisa.MediosPago Where MP.Identificador = Util.AtribuirValorObj(_valor("OID_MEDIO_PAGO"), GetType(String))).FirstOrDefault

                                    If objMedioPago IsNot Nothing Then

                                        Dim valoresTerminos As New ObservableCollection(Of Clases.Termino)
                                        If _valor.Table.Columns.Contains("OID_CONTADO_MEDIO_PAGO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("OID_CONTADO_MEDIO_PAGO"), GetType(String))) AndAlso
                                            _valor.Table.Columns.Contains("OID_DECLARADO_MEDIO_PAGO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("OID_DECLARADO_MEDIO_PAGO"), GetType(String))) AndAlso
                                            objMedioPago.Terminos IsNot Nothing Then


                                            Dim valorTermino = Nothing

                                            If _valor.Table.Columns.Contains("TIPO") Then
                                                Select Case _valor("TIPO")
                                                    Case "CONTADO_MEDIO_PAGO"
                                                        valorTermino = dtValorTermino.Select("OID_CONTADO_MEDIO_PAGO = '" & _valor("OID_CONTADO_MEDIO_PAGO") & "' ")
                                                    Case "DECLARADO_MEDIO_PAGO"
                                                        valorTermino = dtValorTermino.Select("OID_DECLARADO_MEDIO_PAGO = '" & _valor("OID_DECLARADO_MEDIO_PAGO") & "' ")
                                                End Select
                                            End If

                                            If valorTermino IsNot Nothing AndAlso valorTermino.Count > 0 Then

                                                For Each v In valorTermino
                                                    Dim termino As Clases.Termino = objMedioPago.Terminos.Select(Function(t) t.Identificador = Util.AtribuirValorObj(v("OID_MEDIO_PAGO"), GetType(String)))
                                                    If termino IsNot Nothing Then
                                                        termino.Valor = Util.AtribuirValorObj(v("DES_VALOR"), GetType(String))
                                                        termino.NecIndiceGrupo = Util.AtribuirValorObj(v("NEC_INDICE_GRUPO"), GetType(String))
                                                        valoresTerminos.Add(termino)
                                                    End If
                                                Next

                                            End If
                                        End If

                                        If objMedioPago.Valores Is Nothing Then objMedioPago.Valores = New ObservableCollection(Of Clases.ValorMedioPago)

                                        Dim objValor As New Clases.ValorMedioPago

                                        With objValor
                                            .Cantidad = If(_valor.Table.Columns.Contains("CANTIDAD"), Util.AtribuirValorObj(_valor("CANTIDAD"), GetType(Int64)), Nothing)
                                            .Importe = If(_valor.Table.Columns.Contains("IMPORTE"), Util.AtribuirValorObj(_valor("IMPORTE"), GetType(Decimal)), Nothing)
                                            .Terminos = valoresTerminos
                                            If _valor.Table.Columns.Contains("TIPO") Then
                                                Select Case _valor("TIPO")
                                                    Case "CONTADO_MEDIO_PAGO"
                                                        .TipoValor = Enumeradores.TipoValor.Contado
                                                    Case "DIFERENCIA_MEDIO_PAGO"
                                                        .TipoValor = Enumeradores.TipoValor.Diferencia
                                                    Case "DECLARADO_MEDIO_PAGO"
                                                        .TipoValor = Enumeradores.TipoValor.Declarado
                                                End Select
                                            End If
                                        End With

                                        objMedioPago.Valores.Add(objValor)
                                    End If

                                End If

                            End If

                        Next

                    End If

                End If

                ' Valores Totales
                If dtValoresTotales IsNot Nothing AndAlso dtValoresTotales.Rows.Count > 0 Then
                    Dim _ValoresTotales = dtValoresTotales.Select(consulta)

                    If _ValoresTotales IsNot Nothing Then

                        For Each _valor In _ValoresTotales

                            If _valor.Table.Columns.Contains("OID_DIVISA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("OID_DIVISA"), GetType(String))) Then

                                divisa = divisas.FirstOrDefault(Function(x) x.Identificador = Util.AtribuirValorObj(_valor("OID_DIVISA"), GetType(String)))

                                If divisa Is Nothing Then
                                    divisa = divisasPosibles.FirstOrDefault(Function(x) x.Identificador = Util.AtribuirValorObj(_valor("OID_DIVISA"), GetType(String))).Clonar()

                                    If divisa Is Nothing Then
                                        Throw New Exception("Error divisa.")
                                    End If

                                    divisas.Add(divisa)
                                End If

                                If _valor.Table.Columns.Contains("IMPORTE") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("IMPORTE"), GetType(String))) Then


                                    If (Util.AtribuirValorObj(_valor("TIPO"), GetType(String)) = "DECLARADO_EFECTIVO" OrElse Util.AtribuirValorObj(_valor("TIPO"), GetType(String)) = "DIFERENCIA_EFECTIVO") Then


                                        Dim objValor As Clases.Valor = Nothing
                                        Dim nivelDetalle As Enumeradores.TipoNivelDetalhe = Extenciones.RecuperarEnum(Of Enumeradores.TipoNivelDetalhe)(Util.AtribuirValorObj(_valor("COD_NIVEL_DETALLE"), GetType(String)))


                                        If (nivelDetalle = Enumeradores.TipoNivelDetalhe.Total) Then

                                            If (divisa.ValoresTotalesEfectivo Is Nothing) Then divisa.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)

                                            objValor = New Clases.ValorEfectivo
                                            objValor.Importe = Util.AtribuirValorObj(_valor("IMPORTE"), GetType(String))

                                            If Util.AtribuirValorObj(_valor("TIPO"), GetType(String)) = "DECLARADO_EFECTIVO" Then
                                                objValor.TipoValor = Enumeradores.TipoValor.Declarado
                                            ElseIf Util.AtribuirValorObj(_valor("TIPO"), GetType(String)) = "DIFERENCIA_EFECTIVO" Then
                                                objValor.TipoValor = Enumeradores.TipoValor.Diferencia
                                            End If

                                            divisa.ValoresTotalesEfectivo.Add(objValor)

                                        ElseIf (nivelDetalle = Enumeradores.TipoNivelDetalhe.TotalGeral) Then
                                            objValor = New Clases.ValorDivisa

                                            If (divisa.ValoresTotalesDivisa Is Nothing) Then divisa.ValoresTotalesDivisa = New ObservableCollection(Of Clases.ValorDivisa)

                                            objValor = New Clases.ValorDivisa
                                            objValor.Importe = Util.AtribuirValorObj(_valor("IMPORTE"), GetType(String))

                                            If Util.AtribuirValorObj(_valor("TIPO"), GetType(String)) = "DECLARADO_EFECTIVO" Then
                                                objValor.TipoValor = Enumeradores.TipoValor.Declarado
                                            ElseIf Util.AtribuirValorObj(_valor("TIPO"), GetType(String)) = "DIFERENCIA_EFECTIVO" Then
                                                objValor.TipoValor = Enumeradores.TipoValor.Diferencia
                                            End If

                                            divisa.ValoresTotalesDivisa.Add(objValor)
                                        End If

                                    Else

                                        Dim objValor As New Clases.ValorTipoMedioPago

                                        With objValor

                                            .Importe = Util.AtribuirValorObj(_valor("IMPORTE"), GetType(String))

                                            If Util.AtribuirValorObj(_valor("TIPO"), GetType(String)) = "DECLARADO_MEDIO_PAGO" Then
                                                objValor.TipoValor = Enumeradores.TipoValor.Declarado
                                            ElseIf Util.AtribuirValorObj(_valor("TIPO"), GetType(String)) = "DIFERENCIA_MEDIO_PAGO" Then
                                                objValor.TipoValor = Enumeradores.TipoValor.Diferencia
                                            End If

                                            .TipoMedioPago = Extenciones.RecuperarEnum(Of Enumeradores.TipoMedioPago)(Util.AtribuirValorObj(_valor("COD_TIPO_MEDIO_PAGO"), GetType(String)))
                                            .InformadoPor = Enumeradores.TipoContado.NoDefinido
                                        End With

                                        If (divisa.ValoresTotalesTipoMedioPago Is Nothing) Then divisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)

                                        divisa.ValoresTotalesTipoMedioPago.Add(objValor)
                                    End If
                                End If
                            End If
                        Next

                    End If

                End If

            End If


        End Sub

        Private Shared Sub CargarValoresDivisa_v3(ByRef divisas As ObservableCollection(Of Clases.Divisa),
                                                  ByRef divisasPosibles As ObservableCollection(Of Clases.Divisa),
                                                  ByRef dtValoresDenominaciones As DataTable,
                                                  ByRef dtValoresMedioPago As DataTable,
                                                  ByRef dtValoresTotales As DataTable,
                                                  ByRef identificadorRemesa As String,
                                                  ByRef identificadorBulto As String,
                                                  ByRef identificadorParcial As String)

            If Not String.IsNullOrEmpty(identificadorRemesa) Then

                Dim divisa As New Clases.Divisa
                Dim objDenominacion As Clases.Denominacion = Nothing
                Dim objMedioPago As Clases.MedioPago = Nothing

                ' Crea filtro
                Dim consulta As String = "OID_REMESA = '" & identificadorRemesa & "'"
                If Not String.IsNullOrEmpty(identificadorBulto) Then
                    consulta &= " AND OID_BULTO = '" & identificadorBulto & "'"
                Else
                    consulta &= " AND OID_BULTO IS NULL "
                End If
                If Not String.IsNullOrEmpty(identificadorParcial) Then
                    consulta &= " AND OID_PARCIAL = '" & identificadorParcial & "'"
                Else
                    consulta &= " AND OID_PARCIAL IS NULL "
                End If

                ' Valores Denominaciones
                If dtValoresDenominaciones IsNot Nothing AndAlso dtValoresDenominaciones.Rows.Count > 0 Then
                    Dim _ValoresDenominaciones = dtValoresDenominaciones.Select(consulta)

                    If _ValoresDenominaciones IsNot Nothing Then

                        For Each _valor In _ValoresDenominaciones

                            If _valor.Table.Columns.Contains("OID_DIVISA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("OID_DIVISA"), GetType(String))) Then

                                divisa = divisas.FirstOrDefault(Function(x) x.Identificador = Util.AtribuirValorObj(_valor("OID_DIVISA"), GetType(String)))

                                If divisa Is Nothing Then
                                    divisa = New Clases.Divisa With {.Identificador = Util.AtribuirValorObj(_valor("OID_DIVISA"), GetType(String))}
                                    divisas.Add(divisa)
                                End If

                                If _valor.Table.Columns.Contains("OID_DENOMINACION") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("OID_DENOMINACION"), GetType(String))) AndAlso
                                   _valor.Table.Columns.Contains("IMPORTE") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("IMPORTE"), GetType(String))) Then

                                    objDenominacion = Nothing

                                    If divisa.Denominaciones IsNot Nothing AndAlso divisa.Denominaciones.Count > 0 Then
                                        objDenominacion = (From den In divisa.Denominaciones Where den.Identificador = Util.AtribuirValorObj(_valor("OID_DENOMINACION"), GetType(String))).FirstOrDefault
                                    End If

                                    If objDenominacion Is Nothing Then

                                        objDenominacion = New Clases.Denominacion With {.Identificador = Util.AtribuirValorObj(_valor("OID_DENOMINACION"), GetType(String))}
                                        If divisa.Denominaciones Is Nothing Then
                                            divisa.Denominaciones = New ObservableCollection(Of Clases.Denominacion)
                                        End If

                                        divisa.Denominaciones.Add(objDenominacion)

                                    End If

                                    If objDenominacion IsNot Nothing Then

                                        If objDenominacion.ValorDenominacion Is Nothing Then objDenominacion.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)

                                        Dim objValor As New Clases.ValorDenominacion
                                        objValor.Cantidad += If(_valor.Table.Columns.Contains("CANTIDAD"), Util.AtribuirValorObj(_valor("CANTIDAD"), GetType(Int64)), Nothing)
                                        objValor.Importe += If(_valor.Table.Columns.Contains("IMPORTE"), Util.AtribuirValorObj(_valor("IMPORTE"), GetType(Decimal)), Nothing)

                                        If _valor.Table.Columns.Contains("TIPO") Then
                                            Select Case _valor("TIPO")
                                                Case "DECLARADO_EFECTIVO"
                                                    objValor.TipoValor = Enumeradores.TipoValor.Declarado
                                            End Select
                                        End If

                                        objDenominacion.ValorDenominacion.Add(objValor)

                                    End If

                                End If

                            End If

                        Next

                    End If

                End If

                ' Valores MedioPago
                If dtValoresMedioPago IsNot Nothing AndAlso dtValoresMedioPago.Rows.Count > 0 Then
                    Dim _ValoresMedioPago = dtValoresMedioPago.Select(consulta)

                    If _ValoresMedioPago IsNot Nothing Then

                        For Each _valor In _ValoresMedioPago

                            If _valor.Table.Columns.Contains("OID_DIVISA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("OID_DIVISA"), GetType(String))) Then

                                divisa = divisas.FirstOrDefault(Function(x) x.Identificador = Util.AtribuirValorObj(_valor("OID_DIVISA"), GetType(String)))

                                If divisa Is Nothing Then
                                    divisa = New Clases.Divisa With {.Identificador = Util.AtribuirValorObj(_valor("OID_DIVISA"), GetType(String))}

                                    divisas.Add(divisa)
                                End If

                                If _valor.Table.Columns.Contains("OID_MEDIO_PAGO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("OID_MEDIO_PAGO"), GetType(String))) AndAlso
                                   _valor.Table.Columns.Contains("IMPORTE") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("IMPORTE"), GetType(String))) Then

                                    objMedioPago = Nothing

                                    If divisa.MediosPago IsNot Nothing AndAlso divisa.MediosPago.Count > 0 Then
                                        objMedioPago = (From MP In divisa.MediosPago Where MP.Identificador = Util.AtribuirValorObj(_valor("OID_MEDIO_PAGO"), GetType(String))).FirstOrDefault
                                    End If

                                    If objMedioPago Is Nothing Then

                                        objMedioPago = New Clases.MedioPago With {.Identificador = Util.AtribuirValorObj(_valor("OID_MEDIO_PAGO"), GetType(String))}
                                        If divisa.MediosPago Is Nothing Then
                                            divisa.MediosPago = New ObservableCollection(Of Clases.MedioPago)
                                        End If

                                        divisa.MediosPago.Add(objMedioPago)

                                    End If

                                    If objMedioPago IsNot Nothing Then

                                        If objMedioPago.Valores Is Nothing Then objMedioPago.Valores = New ObservableCollection(Of Clases.ValorMedioPago)

                                        Dim objValor As New Clases.ValorMedioPago

                                        With objValor
                                            .Cantidad += If(_valor.Table.Columns.Contains("CANTIDAD"), Util.AtribuirValorObj(_valor("CANTIDAD"), GetType(Int64)), Nothing)
                                            .Importe += If(_valor.Table.Columns.Contains("IMPORTE"), Util.AtribuirValorObj(_valor("IMPORTE"), GetType(Decimal)), Nothing)
                                            If _valor.Table.Columns.Contains("TIPO") Then
                                                Select Case _valor("TIPO")
                                                    Case "DECLARADO_MEDIO_PAGO"
                                                        .TipoValor = Enumeradores.TipoValor.Declarado
                                                End Select
                                            End If
                                        End With

                                        objMedioPago.Valores.Add(objValor)
                                    End If

                                End If

                            End If

                        Next

                    End If

                End If

                ' Valores Totales
                If dtValoresTotales IsNot Nothing AndAlso dtValoresTotales.Rows.Count > 0 Then
                    Dim _ValoresTotales = dtValoresTotales.Select(consulta)

                    If _ValoresTotales IsNot Nothing Then

                        For Each _valor In _ValoresTotales

                            If _valor.Table.Columns.Contains("OID_DIVISA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("OID_DIVISA"), GetType(String))) Then

                                divisa = divisas.FirstOrDefault(Function(x) x.Identificador = Util.AtribuirValorObj(_valor("OID_DIVISA"), GetType(String)))

                                If divisa Is Nothing Then
                                    divisa = New Clases.Divisa With {.Identificador = Util.AtribuirValorObj(_valor("OID_DIVISA"), GetType(String))}

                                    divisas.Add(divisa)
                                End If

                                If _valor.Table.Columns.Contains("IMPORTE") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("IMPORTE"), GetType(String))) Then

                                    If (Util.AtribuirValorObj(_valor("TIPO"), GetType(String)) = "DECLARADO_EFECTIVO" OrElse Util.AtribuirValorObj(_valor("TIPO"), GetType(String)) = "DIFERENCIA_EFECTIVO") Then

                                        Dim objValor As Clases.Valor = Nothing
                                        Dim nivelDetalle As Enumeradores.TipoNivelDetalhe = Extenciones.RecuperarEnum(Of Enumeradores.TipoNivelDetalhe)(Util.AtribuirValorObj(_valor("COD_NIVEL_DETALLE"), GetType(String)))

                                        If (nivelDetalle = Enumeradores.TipoNivelDetalhe.Total) Then

                                            If (divisa.ValoresTotalesEfectivo Is Nothing) Then divisa.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)

                                            objValor = New Clases.ValorEfectivo
                                            objValor.Importe += Util.AtribuirValorObj(_valor("IMPORTE"), GetType(String))

                                            If Util.AtribuirValorObj(_valor("TIPO"), GetType(String)) = "DECLARADO_EFECTIVO" Then
                                                objValor.TipoValor = Enumeradores.TipoValor.Declarado
                                            End If

                                            divisa.ValoresTotalesEfectivo.Add(objValor)

                                        ElseIf (nivelDetalle = Enumeradores.TipoNivelDetalhe.TotalGeral) Then
                                            objValor = New Clases.ValorDivisa

                                            If (divisa.ValoresTotalesDivisa Is Nothing) Then divisa.ValoresTotalesDivisa = New ObservableCollection(Of Clases.ValorDivisa)

                                            objValor = New Clases.ValorDivisa
                                            objValor.Importe += Util.AtribuirValorObj(_valor("IMPORTE"), GetType(String))

                                            If Util.AtribuirValorObj(_valor("TIPO"), GetType(String)) = "DECLARADO_EFECTIVO" Then
                                                objValor.TipoValor = Enumeradores.TipoValor.Declarado
                                            End If

                                            divisa.ValoresTotalesDivisa.Add(objValor)
                                        End If

                                    Else

                                        Dim objValor As New Clases.ValorTipoMedioPago

                                        With objValor

                                            .Importe += Util.AtribuirValorObj(_valor("IMPORTE"), GetType(String))

                                            If Util.AtribuirValorObj(_valor("TIPO"), GetType(String)) = "DECLARADO_MEDIO_PAGO" Then
                                                objValor.TipoValor = Enumeradores.TipoValor.Declarado
                                            End If

                                            .TipoMedioPago = Extenciones.RecuperarEnum(Of Enumeradores.TipoMedioPago)(Util.AtribuirValorObj(_valor("COD_TIPO_MEDIO_PAGO"), GetType(String)))
                                            .InformadoPor = Enumeradores.TipoContado.NoDefinido
                                        End With

                                        If (divisa.ValoresTotalesTipoMedioPago Is Nothing) Then divisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)

                                        divisa.ValoresTotalesTipoMedioPago.Add(objValor)
                                    End If
                                End If
                            End If
                        Next

                    End If

                End If

            End If


        End Sub

        Private Shared Function cargarTipoServicio(ByRef dtTipoServicio As DataTable, ByRef identificadorRemesa As String, ByRef identificadorBulto As String) As Clases.TipoServicio

            Dim _tipoServicio As Clases.TipoServicio = Nothing

            If dtTipoServicio IsNot Nothing AndAlso dtTipoServicio.Rows.Count > 0 AndAlso Not String.IsNullOrEmpty(identificadorRemesa) AndAlso Not String.IsNullOrEmpty(identificadorBulto) Then

                _tipoServicio = New Clases.TipoServicio

                Dim _tipos = dtTipoServicio.Select(" OID_REMESA = '" & identificadorRemesa & "' AND OID_BULTO = '" & identificadorBulto & "' AND COD_TIPO = '" & Enumeradores.TipoListaValor.TipoServicio.RecuperarValor() & "' ")

                If _tipos IsNot Nothing AndAlso _tipos.Count > 0 Then

                    With _tipoServicio

                        .Identificador = Util.AtribuirValorObj(_tipos(0)("OID_LISTA_VALOR"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(_tipos(0)("COD_VALOR"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(_tipos(0)("DES_VALOR"), GetType(String))
                        .EsDefecto = Util.AtribuirValorObj(_tipos(0)("BOL_DEFECTO"), GetType(String))

                    End With

                End If

            End If

            Return _tipoServicio

        End Function

        Private Shared Function cargarTipoFormato(dtTipoFormato As DataTable, identificadorRemesa As String, identificadorBulto As String, identificadorParcial As String) As Clases.TipoFormato

            Dim _tipoFormato As Clases.TipoFormato = Nothing

            If dtTipoFormato IsNot Nothing AndAlso Not String.IsNullOrEmpty(identificadorRemesa) AndAlso Not String.IsNullOrEmpty(identificadorBulto) Then

                _tipoFormato = New Clases.TipoFormato

                ' Crea filtro
                Dim consulta As String = " COD_TIPO = '" & Enumeradores.TipoListaValor.TipoFormato.RecuperarValor() & "' AND OID_REMESA = '" & identificadorRemesa & "'"
                If Not String.IsNullOrEmpty(identificadorBulto) Then
                    consulta &= " AND OID_BULTO = '" & identificadorBulto & "'"
                Else
                    consulta &= " AND OID_BULTO IS NULL "
                End If
                If Not String.IsNullOrEmpty(identificadorParcial) Then
                    consulta &= " AND OID_PARCIAL = '" & identificadorParcial & "'"
                Else
                    consulta &= " AND OID_PARCIAL IS NULL "
                End If

                Dim _tipos = dtTipoFormato.Select(consulta)

                If _tipos IsNot Nothing AndAlso _tipos.Count > 0 Then

                    With _tipoFormato

                        .Identificador = Util.AtribuirValorObj(_tipos(0)("OID_LISTA_VALOR"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(_tipos(0)("COD_VALOR"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(_tipos(0)("DES_VALOR"), GetType(String))
                        .EsDefecto = Util.AtribuirValorObj(_tipos(0)("BOL_DEFECTO"), GetType(String))

                    End With

                    If (_tipos(0)("OID_MODULO") IsNot DBNull.Value) Then
                        _tipoFormato.Modulo = New Prosegur.Genesis.Comon.Clases.Modulo()
                        _tipoFormato.Modulo.Identificador = Util.AtribuirValorObj(_tipos(0)("OID_MODULO"), GetType(String))
                    End If
                End If

            End If

            Return _tipoFormato

        End Function

        Private Shared Function cargarGrupoTerminosIAC(dtIAC As DataTable, dtTerminosIAC As DataTable) As ObservableCollection(Of Clases.GrupoTerminosIAC)
            Dim _GruposTerminosIAC As ObservableCollection(Of Clases.GrupoTerminosIAC) = Nothing

            If dtIAC IsNot Nothing AndAlso dtIAC.Rows.Count > 0 AndAlso dtTerminosIAC IsNot Nothing AndAlso dtTerminosIAC.Rows.Count > 0 Then

                For Each _iac In dtIAC.Rows

                    Dim _GrupoTerminosIAC = New Clases.GrupoTerminosIAC

                    With _GrupoTerminosIAC

                        .Identificador = If(_iac.Table.Columns.Contains("OID_IAC"), Util.AtribuirValorObj(_iac("OID_IAC"), GetType(String)), Nothing)
                        .Codigo = If(_iac.Table.Columns.Contains("COD_IAC"), Util.AtribuirValorObj(_iac("COD_IAC"), GetType(String)), Nothing)
                        .Descripcion = If(_iac.Table.Columns.Contains("DES_IAC"), Util.AtribuirValorObj(_iac("DES_IAC"), GetType(String)), Nothing)
                        .Observacion = If(_iac.Table.Columns.Contains("OBS_IAC"), Util.AtribuirValorObj(_iac("OBS_IAC"), GetType(String)), Nothing)
                        .EstaActivo = If(_iac.Table.Columns.Contains("BOL_VIGENTE"), Util.AtribuirValorObj(_iac("BOL_VIGENTE"), GetType(Boolean)), Nothing)
                        .EsInvisible = If(_iac.Table.Columns.Contains("BOL_INVISIBLE"), Util.AtribuirValorObj(_iac("BOL_INVISIBLE"), GetType(Boolean)), Nothing)
                        .CopiarDeclarados = If(_iac.Table.Columns.Contains("BOL_COPIA_DECLARADOS"), Util.AtribuirValorObj(_iac("BOL_COPIA_DECLARADOS"), GetType(Boolean)), Nothing)
                        .TerminosIAC = New ObservableCollection(Of Clases.TerminoIAC)

                        If dtTerminosIAC IsNot Nothing AndAlso dtTerminosIAC.Rows.Count > 0 AndAlso
                            _iac.Table.Columns.Contains("OID_IAC") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_iac("OID_IAC"), GetType(String))) Then

                            Dim _Terminos = dtTerminosIAC.Select("OID_IAC = '" & Util.AtribuirValorObj(_iac("OID_IAC"), GetType(String)) & "'")

                            If _Terminos IsNot Nothing Then

                                For Each termino In _Terminos

                                    Dim terminoIAC As New Clases.TerminoIAC
                                    terminoIAC.Formato = New Clases.Formato

                                    With terminoIAC
                                        .Identificador = If(termino.Table.Columns.Contains("OID_TERMINO"), Util.AtribuirValorObj(termino("OID_TERMINO"), GetType(String)), Nothing)
                                        .BuscarParcial = If(termino.Table.Columns.Contains("BOL_BUSQUEDA_PARCIAL"), Util.AtribuirValorObj(termino("BOL_BUSQUEDA_PARCIAL"), GetType(Boolean)), Nothing)
                                        .EsCampoClave = If(termino.Table.Columns.Contains("BOL_CAMPO_CLAVE"), Util.AtribuirValorObj(termino("BOL_CAMPO_CLAVE"), GetType(Boolean)), Nothing)
                                        .Orden = If(termino.Table.Columns.Contains("NEC_ORDEN"), Util.AtribuirValorObj(termino("NEC_ORDEN"), GetType(Integer)), Nothing)
                                        .EsObligatorio = If(termino.Table.Columns.Contains("BOL_ES_OBLIGATORIO"), Util.AtribuirValorObj(termino("BOL_ES_OBLIGATORIO"), GetType(Boolean)), Nothing)
                                        .EsTerminoCopia = If(termino.Table.Columns.Contains("BOL_TERMINO_COPIA"), Util.AtribuirValorObj(termino("BOL_TERMINO_COPIA"), GetType(Boolean)), Nothing)
                                        .EsProtegido = If(termino.Table.Columns.Contains("BOL_ES_PROTEGIDO"), Util.AtribuirValorObj(termino("BOL_ES_PROTEGIDO"), GetType(Boolean)), Nothing)
                                        .Codigo = If(termino.Table.Columns.Contains("COD_TERMINO"), Util.AtribuirValorObj(termino("COD_TERMINO"), GetType(String)), Nothing)
                                        .Observacion = If(termino.Table.Columns.Contains("OBS_TERMINO"), Util.AtribuirValorObj(termino("OBS_TERMINO"), GetType(String)), Nothing)
                                        .Longitud = If(termino.Table.Columns.Contains("NEC_LONGITUD"), Util.AtribuirValorObj(termino("NEC_LONGITUD"), GetType(Integer)), Nothing)
                                        .MostrarDescripcionConCodigo = If(termino.Table.Columns.Contains("BOL_MOSTRAR_CODIGO"), Util.AtribuirValorObj(termino("BOL_MOSTRAR_CODIGO"), GetType(Boolean)), Nothing)
                                        .TieneValoresPosibles = If(termino.Table.Columns.Contains("BOL_VALORES_POSIBLES"), Util.AtribuirValorObj(termino("BOL_VALORES_POSIBLES"), GetType(Boolean)), Nothing)
                                        .AceptarDigitacion = If(termino.Table.Columns.Contains("BOL_ACEPTAR_DIGITACION"), Util.AtribuirValorObj(termino("BOL_ACEPTAR_DIGITACION"), GetType(Boolean)), Nothing)
                                        .EstaActivo = If(termino.Table.Columns.Contains("BOL_VIGENTE"), Util.AtribuirValorObj(termino("BOL_VIGENTE"), GetType(Boolean)), Nothing)
                                        .EsEspecificoDeSaldos = If(termino.Table.Columns.Contains("BOL_ESPECIFICO_DE_SALDOS"), Util.AtribuirValorObj(termino("BOL_ESPECIFICO_DE_SALDOS"), GetType(Boolean)), Nothing)
                                        .Descripcion = If(termino.Table.Columns.Contains("DES_TERMINO"), Util.AtribuirValorObj(termino("DES_TERMINO"), GetType(String)), Nothing)

                                        With terminoIAC.Formato
                                            .Identificador = If(termino.Table.Columns.Contains("OID_FORMATO"), Util.AtribuirValorObj(termino("OID_FORMATO"), GetType(String)), Nothing)
                                            .Codigo = If(termino.Table.Columns.Contains("COD_FORMATO"), Util.AtribuirValorObj(termino("COD_FORMATO"), GetType(String)), Nothing)
                                            .Descripcion = If(termino.Table.Columns.Contains("DES_FORMATO"), Util.AtribuirValorObj(termino("DES_FORMATO"), GetType(String)), Nothing)
                                        End With

                                        'Verifica se possui algoritimo de validação.
                                        If termino.Table.Columns.Contains("OID_ALGORITMO_VALIDACION") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(termino("OID_ALGORITMO_VALIDACION"), GetType(String))) Then
                                            .AlgoritmoValidacion = New Clases.AlgoritmoValidacion
                                            With .AlgoritmoValidacion
                                                .Identificador = If(termino.Table.Columns.Contains("OID_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(termino("OID_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                                                .Codigo = If(termino.Table.Columns.Contains("COD_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(termino("COD_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                                                .Descripcion = If(termino.Table.Columns.Contains("DES_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(termino("DES_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                                                .Observacion = If(termino.Table.Columns.Contains("OBS_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(termino("OBS_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                                            End With
                                        End If

                                        'Verifica se possui mascara.
                                        If termino.Table.Columns.Contains("OID_MASCARA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(termino("OID_MASCARA"), GetType(String))) Then
                                            .Mascara = New Clases.Mascara
                                            With .Mascara
                                                .Identificador = If(termino.Table.Columns.Contains("OID_MASCARA"), Util.AtribuirValorObj(termino("OID_MASCARA"), GetType(String)), Nothing)
                                                .Codigo = If(termino.Table.Columns.Contains("COD_MASCARA"), Util.AtribuirValorObj(termino("COD_MASCARA"), GetType(String)), Nothing)
                                                .Descripcion = If(termino.Table.Columns.Contains("DES_MASCARA"), Util.AtribuirValorObj(termino("DES_MASCARA"), GetType(String)), Nothing)
                                                .ExpresionRegular = If(termino.Table.Columns.Contains("DES_EXP_REGULAR"), Util.AtribuirValorObj(termino("DES_EXP_REGULAR"), GetType(String)), Nothing)
                                            End With
                                        End If
                                    End With

                                    .TerminosIAC.Add(terminoIAC)

                                Next
                            End If

                        End If

                    End With

                    If _GruposTerminosIAC Is Nothing Then _GruposTerminosIAC = New ObservableCollection(Of Clases.GrupoTerminosIAC)

                    _GruposTerminosIAC.Add(_GrupoTerminosIAC)
                Next
            End If

            Return _GruposTerminosIAC
        End Function

        Private Shared Sub cargarGrupoTerminosIACElemento(grupoTerminosIAC As Clases.GrupoTerminosIAC,
                                                          grupoTerminosIACPosibles As ObservableCollection(Of Clases.GrupoTerminosIAC),
                                                          dtValorTerminoIAC As DataTable,
                                                          identificador As String,
                                                          tipoElemento As Enumeradores.TipoElemento)

            Dim _GrupoTerminosIAC As Clases.GrupoTerminosIAC = Nothing

            If grupoTerminosIAC IsNot Nothing AndAlso grupoTerminosIAC.TerminosIAC IsNot Nothing AndAlso grupoTerminosIAC.TerminosIAC.Count > 0 AndAlso
                grupoTerminosIACPosibles IsNot Nothing AndAlso grupoTerminosIACPosibles.Count > 0 AndAlso Not String.IsNullOrEmpty(identificador) Then

                _GrupoTerminosIAC = grupoTerminosIACPosibles.FirstOrDefault(Function(X) X.Identificador = identificador).Clonar

                ' Cargar Valores Terminos
                If dtValorTerminoIAC IsNot Nothing AndAlso dtValorTerminoIAC.Rows.Count > 0 Then

                    ' Crea filtro
                    Dim consulta As String = ""
                    Select Case tipoElemento
                        Case Enumeradores.TipoElemento.Remesa
                            consulta = "OID_REMESA = '" & identificador & "'"
                        Case Enumeradores.TipoElemento.Bulto
                            consulta = "OID_BULTO = '" & identificador & "'"
                        Case Enumeradores.TipoElemento.Parcial
                            consulta = "OID_PARCIAL = '" & identificador & "'"
                    End Select

                    Dim _valores = dtValorTerminoIAC.Select(consulta)

                    If _valores IsNot Nothing AndAlso _valores.Count > 0 Then

                        For Each _valor In _valores

                            Dim _termino = grupoTerminosIAC.TerminosIAC.FirstOrDefault(Function(x) x.Identificador = _valor("OID_TERMINO"))

                            If _termino IsNot Nothing Then
                                _termino.Valor = Util.AtribuirValorObj(_valor("DES_VALOR_IAC"), GetType(String))
                            End If
                        Next

                    End If

                End If

            End If

        End Sub



#Region "[CONSULTAS]"

        Public Shared Function ValidarRemesaAnulada(identificadoresRemesas As List(Of String)) As ObservableCollection(Of Clases.Remesa)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            cmd.CommandText = My.Resources.Salidas_Remesa_ValidarRemesaAnulada
            cmd.CommandType = CommandType.Text

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, identificadoresRemesas, "OID_REMESA", cmd, "WHERE", "R")))
            cmd.CommandText = cmd.CommandText + Util.PrepararQuery(Constantes.CONEXAO_GENESIS, Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, identificadoresRemesas, "OID_REMESA_PADRE", cmd, "OR", "R"))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt.Rows.Count > 0 Then

                Dim remesas As New ObservableCollection(Of Clases.Remesa)

                For Each row In dt.Rows
                    remesas.Add(New Clases.Remesa With {.Identificador = Util.AtribuirValorObj(Of String)(row("OID_REMESA")),
                                                        .Estado = RecuperarEnum(Of Enumeradores.EstadoRemesa)(Util.AtribuirValorObj(Of String)(row("COD_ESTADO_REMESA"))),
                                                        .FyhInicio = Util.AtribuirValorObj(Of DateTime)(row("FYH_INICIO"))})
                Next

                Return remesas

            End If

            Return Nothing

        End Function

        Public Shared Function RecuperarElementosPrecintados(CodigoSectores As ObservableCollection(Of String),
                                                             CodigoCliente As String,
                                                             CodigoSubCliente As String,
                                                             CodigoPuntoServicio As String,
                                                             CodigoSectorPadre As String,
                                                             CodigoPlanta As String,
                                                             CodigoDelegacion As String,
                                                             CodigoSubCanal As String,
                                                             EstadoRemesa As Enumeradores.EstadoRemesa,
                                                             EstadoBulto As Enumeradores.EstadoBulto,
                                                             SoloBultosSinCuadrar As Boolean,
                                                             BuscarClienteTodosNiveis As Boolean) As ObservableCollection(Of Clases.Saldo)

            Dim objSaldoCuentas As ObservableCollection(Of Clases.Saldo) = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim query As New StringBuilder

            cmd.CommandText = My.Resources.RemesaRecuperarElementosPrecintados_v3
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDelegacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Identificador_Alfanumerico, CodigoPlanta))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_REMESA", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(EstadoRemesa)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_BULTO", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(EstadoBulto)))

            If Not String.IsNullOrEmpty(CodigoCliente) Then

                query.Append(" AND CL.COD_CLIENTE = []COD_CLIENTE ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodigoCliente))

                If Not String.IsNullOrEmpty(CodigoSubCliente) Then

                    query.AppendLine(" AND SCL.COD_SUBCLIENTE = []COD_SUBCLIENTE ")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodigoSubCliente))

                Else
                    If Not BuscarClienteTodosNiveis Then
                        query.AppendLine(" AND CU.OID_SUBCLIENTE IS NULL ")
                    End If
                End If

                If Not String.IsNullOrEmpty(CodigoPuntoServicio) Then

                    query.AppendLine(" AND PTO.COD_PTO_SERVICIO = []COD_PTO_SERVICIO ")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, CodigoPuntoServicio))

                Else
                    If Not BuscarClienteTodosNiveis Then
                        query.AppendLine(" AND CU.OID_PTO_SERVICIO IS NULL ")
                    End If
                End If

            End If

            If Not String.IsNullOrEmpty(CodigoSubCanal) Then

                query.Append(" AND SC.COD_SUBCANAL = []COD_SUBCANAL ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, CodigoSubCanal))

            End If

            'Recupera os setores filhos
            If Not String.IsNullOrEmpty(CodigoSectorPadre) Then

                Dim objSector As Clases.Sector = Nothing
                objSector = Genesis.Sector.ObtenerSector(CodigoDelegacion, CodigoPlanta, CodigoSectorPadre)

                If objSector IsNot Nothing Then

                    query.AppendLine(" AND (SE.OID_SECTOR_PADRE = []OID_SECTOR_PADRE OR SE.OID_SECTOR = []OID_SECTOR_PADRE) ")

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR_PADRE", ProsegurDbType.Objeto_Id, objSector.Identificador))

                End If

            Else
                query.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, CodigoSectores, "COD_SECTOR", cmd, "AND", "SE"))
            End If

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, query.ToString))


            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Dim objRemesas As ObservableCollection(Of Clases.Remesa) = RellenarRemesaEnviarEfectivoPrecintado(dt)

                CargarValoresRemesasEnviarEfectivoPrecintado(objRemesas, dt)

                If objRemesas IsNot Nothing AndAlso objRemesas.Count > 0 Then

                    If objSaldoCuentas Is Nothing Then objSaldoCuentas = New ObservableCollection(Of Clases.Saldo)

                    Dim objSaldoCuenta As Clases.SaldoCuenta = Nothing

                    For Each objR In objRemesas

                        objSaldoCuenta = (From objSC In objSaldoCuentas Where DirectCast(objSC, Clases.SaldoCuenta).Cuenta IsNot Nothing _
                                                                              AndAlso DirectCast(objSC, Clases.SaldoCuenta).Cuenta.Identificador = objR.Cuenta.Identificador).FirstOrDefault

                        If objSaldoCuenta Is Nothing Then

                            objSaldoCuentas.Add(New Clases.SaldoCuenta With {.Cuenta = objR.Cuenta,
                                                                             .Elementos = New ObservableCollection(Of Clases.Elemento)})

                            objSaldoCuenta = (From objSC In objSaldoCuentas Where DirectCast(objSC, Clases.SaldoCuenta).Cuenta IsNot Nothing _
                                                                              AndAlso DirectCast(objSC, Clases.SaldoCuenta).Cuenta.Identificador = objR.Cuenta.Identificador).FirstOrDefault

                        End If

                        objSaldoCuenta.Elementos.Add(objR)
                    Next

                End If

            End If

            Return objSaldoCuentas
        End Function

        ''' <summary>
        ''' Recupera os identificadores das remesas e dos bultos quando se trabalha por bulto
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarIdentificadoresRemesasYBultos(CodigoSectores As ObservableCollection(Of String),
                                                                      CodigoCliente As String,
                                                                      CodigoSubCliente As String,
                                                                      CodigoPuntoServicio As String,
                                                                      CodigoSectorPadre As String,
                                                                      CodigoPlanta As String,
                                                                      CodigoDelegacion As String,
                                                                      CodigoSubCanal As String,
                                                                      EstadoRemesa As Enumeradores.EstadoRemesa,
                                                                      EstadoBulto As Enumeradores.EstadoBulto,
                                                                      EstadoDocumento As Enumeradores.EstadoDocumento,
                                                                      SoloBultosSinCuadrar As Boolean,
                                                                      BuscarClienteTodosNiveis As Boolean) As List(Of Clases.Remesa)

            Dim objRemesas As List(Of Clases.Remesa) = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim query As New StringBuilder

            If SoloBultosSinCuadrar Then
                cmd.CommandText = My.Resources.RemesaRecuperarIdentificadoresRemesasYBultosNoCuadrados
            Else
                cmd.CommandText = My.Resources.RemesaRecuperarIdentificadoresRemesasYBultos
            End If
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDelegacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Identificador_Alfanumerico, CodigoPlanta))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(EstadoDocumento)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_REMESA", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(EstadoRemesa)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_BULTO", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(EstadoBulto)))

            If Not String.IsNullOrEmpty(CodigoCliente) Then

                query.Append(" AND CL.COD_CLIENTE = []COD_CLIENTE ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodigoCliente))

                If Not String.IsNullOrEmpty(CodigoSubCliente) Then

                    query.AppendLine(" AND SCL.COD_SUBCLIENTE = []COD_SUBCLIENTE ")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodigoSubCliente))

                Else
                    If Not BuscarClienteTodosNiveis Then
                        query.AppendLine(" AND CU.OID_SUBCLIENTE IS NULL ")
                    End If
                End If

                If Not String.IsNullOrEmpty(CodigoPuntoServicio) Then

                    query.AppendLine(" AND PTO.COD_PTO_SERVICIO = []COD_PTO_SERVICIO ")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, CodigoPuntoServicio))

                Else
                    If Not BuscarClienteTodosNiveis Then
                        query.AppendLine(" AND CU.OID_PTO_SERVICIO IS NULL ")
                    End If
                End If

            End If

            If Not String.IsNullOrEmpty(CodigoSubCanal) Then

                query.Append(" AND SC.COD_SUBCANAL = []COD_SUBCANAL ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, CodigoSubCanal))

            End If

            'Recupera os setores filhos
            If Not String.IsNullOrEmpty(CodigoSectorPadre) Then

                Dim objSector As Clases.Sector = Nothing
                objSector = Genesis.Sector.ObtenerSector(CodigoDelegacion, CodigoPlanta, CodigoSectorPadre)

                If objSector IsNot Nothing Then

                    query.AppendLine(" AND (SE.OID_SECTOR_PADRE = []OID_SECTOR_PADRE OR SE.OID_SECTOR = []OID_SECTOR_PADRE) ")

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR_PADRE", ProsegurDbType.Objeto_Id, objSector.Identificador))

                End If

            Else
                query.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, CodigoSectores, "COD_SECTOR", cmd, "AND", "SE"))
            End If

            'Somente os bultos que não foram cuadrados
            If SoloBultosSinCuadrar Then
                query.Clear()
                query.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, CodigoSectores, "COD_SECTOR", cmd, "AND", "SE"))
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, query.ToString))
            Else
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, query.ToString))
            End If

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objRemesas = New List(Of Clases.Remesa)()
                Dim objRemesa As Clases.Remesa = Nothing
                Dim IdentificadorRemesa As String = String.Empty
                Dim IdentificadorBulto As String = String.Empty
                Dim IdentificadorCuenta As String = String.Empty

                For Each dr In dt.Rows

                    IdentificadorRemesa = Util.AtribuirValorObj(dr("OID_REMESA"), GetType(String))
                    IdentificadorBulto = Util.AtribuirValorObj(dr("OID_BULTO"), GetType(String))
                    IdentificadorCuenta = Util.AtribuirValorObj(dr("OID_CUENTA"), GetType(String))

                    objRemesa = (From r In objRemesas Where r.Identificador = IdentificadorRemesa).FirstOrDefault

                    If objRemesa Is Nothing Then

                        objRemesas.Add(New Clases.Remesa With {.Identificador = IdentificadorRemesa,
                                                               .Cuenta = If(Not String.IsNullOrEmpty(IdentificadorCuenta), New Clases.Cuenta With {.Identificador = IdentificadorCuenta}, Nothing)})

                        objRemesa = (From r In objRemesas Where r.Identificador = IdentificadorRemesa).FirstOrDefault

                    End If

                    If Not String.IsNullOrEmpty(IdentificadorBulto) Then

                        If objRemesa.Bultos Is Nothing Then objRemesa.Bultos = New ObservableCollection(Of Clases.Bulto)

                        objRemesa.Bultos.Add(New Clases.Bulto With {.Identificador = IdentificadorBulto})

                    End If

                Next

            End If

            Return objRemesas
        End Function

        Shared Function ObtenerUltimoDocumento(identificadorRemesa As String, identificadorDocumento As String, estadoDocumentoElemento As Enumeradores.EstadoDocumentoElemento) As String

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandType = CommandType.Text

            If Not String.IsNullOrEmpty(identificadorRemesa) Then
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RemesaObtenerUltimoDocumentoIdRemesa)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Identificador_Alfanumerico, identificadorRemesa))
            End If

            If Not String.IsNullOrEmpty(identificadorDocumento) Then
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RemesaObtenerUltimoDocumentoIdDocumento)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, identificadorDocumento))
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_DOCXELEMENTO", ProsegurDbType.Identificador_Alfanumerico, estadoDocumentoElemento.RecuperarValor()))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)

        End Function

        Public Shared Function RecuperarRemesasPorIdentificadorCodigoExternos(identificadoresExternos As List(Of String), codigosExternos As List(Of String)) As List(Of String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
            cmd.CommandType = CommandType.Text

            If identificadoresExternos IsNot Nothing AndAlso identificadoresExternos.Count > 0 Then

                cmd.CommandText = "SELECT R.OID_REMESA FROM SAPR_TREMESA R {0} "
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, Util.MontarClausulaIn(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, identificadoresExternos, "OID_EXTERNO", cmd, "WHERE", "R")))

            End If

            If codigosExternos IsNot Nothing AndAlso codigosExternos.Count > 0 Then

                If cmd.CommandText.Length > 0 Then
                    cmd.CommandText &= " UNION "
                End If

                cmd.CommandText &= "SELECT R.OID_REMESA FROM SAPR_TREMESA R {0} "
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, Util.MontarClausulaIn(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, codigosExternos, "COD_EXTERNO", cmd, "WHERE", "R")))

            End If

            If cmd.CommandText.Length > 0 Then

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)

                If dt.Rows.Count > 0 Then

                    Dim IdentificadoresRemesas As New List(Of String)

                    For Each row In dt.Rows

                        Dim identificadorRemesa As String = Util.AtribuirValorObj(row("OID_REMESA"), GetType(String))

                        If Not String.IsNullOrEmpty(identificadorRemesa) AndAlso Not IdentificadoresRemesas.Contains(identificadorRemesa) Then
                            IdentificadoresRemesas.Add(identificadorRemesa)
                        End If

                    Next row

                    Return IdentificadoresRemesas

                End If

            End If

            Return Nothing

        End Function

        Public Shared Function DocumentoPadrePorCodigoExterno(codigoExterno As String, IdentificadorDelegacion As String, gestionBulto As Boolean) As DataTable

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
            cmd.CommandType = CommandType.Text
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RemesaDocumentoPadrePorCodigoExterno)
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_EXTERNO", ProsegurDbType.Descricao_Longa, codigoExterno))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DELEGACION", ProsegurDbType.Objeto_Id, IdentificadorDelegacion))
            If gestionBulto Then
                cmd.CommandText = String.Format(cmd.CommandText, " AND DOCE.OID_BULTO IS NOT NULL")
            Else
                cmd.CommandText = String.Format(cmd.CommandText, " AND DOCE.OID_BULTO IS NULL")
            End If

            Return AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)
        End Function

        Public Shared Function ValidarRemesaAnulada(identificadorRemesa As String) As Boolean
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = "SELECT BOL_ANULADO FROM SAPR_TREMESA REM WHERE REM.OID_REMESA = []OID_REMESA"
            cmd.CommandType = CommandType.Text

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Identificador_Alfanumerico, identificadorRemesa))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)
        End Function
#End Region

#Region "[INSERIR]"

        ''' <summary>
        ''' Grabar una nueva Remesa.
        ''' </summary>
        ''' <param name="objRemesa">Objeto remessa preenchido.</param>
        ''' <remarks></remarks>
        Public Shared Sub GrabarRemesa(objRemesa As Clases.Remesa,
                             Optional ByRef _transacion As DbHelper.Transacao = Nothing)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RemessaInserir)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, objRemesa.Identificador))

            If objRemesa.RemesaOrigen IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA_ORIGEN", ProsegurDbType.Descricao_Longa, objRemesa.RemesaOrigen.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA_ORIGEN", ProsegurDbType.Descricao_Longa, Nothing))
            End If

            If objRemesa.IdentificadorExterno IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_EXTERNO", ProsegurDbType.Objeto_Id, objRemesa.IdentificadorExterno))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_EXTERNO", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If objRemesa.GrupoTerminosIAC IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC", ProsegurDbType.Objeto_Id, objRemesa.GrupoTerminosIAC.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC", ProsegurDbType.Objeto_Id, Nothing))
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, objRemesa.IdentificadorDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA", ProsegurDbType.Objeto_Id, objRemesa.Cuenta.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_RECIBO_SALIDA", ProsegurDbType.Descricao_Curta, objRemesa.CodigoReciboSalida))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, objRemesa.Estado.RecuperarValor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_USUARIO_RESPONSABLE", ProsegurDbType.Descricao_Longa, objRemesa.UsuarioResponsable))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PUESTO_RESPONSABLE", ProsegurDbType.Descricao_Curta, objRemesa.PuestoResponsable))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_RUTA", ProsegurDbType.Descricao_Curta, objRemesa.Ruta))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_PARADA", ProsegurDbType.Inteiro_Curto, objRemesa.Parada))

            If objRemesa.FechaHoraTransporte <> DateTime.MinValue Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_TRANSPORTE", ProsegurDbType.Data_Hora, objRemesa.FechaHoraTransporte))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_TRANSPORTE", ProsegurDbType.Data_Hora, Nothing))
            End If

            If objRemesa.Bultos IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_CANTIDAD_BULTOS", ProsegurDbType.Descricao_Curta, objRemesa.Bultos.Count))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_CANTIDAD_BULTOS", ProsegurDbType.Descricao_Curta, Nothing))
            End If

            If objRemesa.FechaHoraInicioConteo <> DateTime.MinValue Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_CONTEO_INICIO", ProsegurDbType.Data_Hora, objRemesa.FechaHoraInicioConteo))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_CONTEO_INICIO", ProsegurDbType.Data_Hora, Nothing))
            End If

            If objRemesa.FechaHoraFinConteo <> DateTime.MinValue Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_CONTEO_FIN", ProsegurDbType.Data_Hora, objRemesa.FechaHoraFinConteo))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_CONTEO_FIN", ProsegurDbType.Data_Hora, Nothing))
            End If

            If objRemesa.ElementoPadre IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA_PADRE", ProsegurDbType.Objeto_Id, objRemesa.ElementoPadre.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA_PADRE", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If objRemesa.ElementoSustituto IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA_SUSTITUTA", ProsegurDbType.Objeto_Id, objRemesa.ElementoSustituto.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA_SUSTITUTA", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If objRemesa.DatosATM IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CAJERO", ProsegurDbType.Descricao_Curta, objRemesa.DatosATM.CodigoCajero))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CAJERO", ProsegurDbType.Descricao_Curta, Nothing))
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_NIVEL_DETALLE", ProsegurDbType.Descricao_Curta, objRemesa.ConfiguracionNivelSaldos.RecuperarValor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, objRemesa.UsuarioCreacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, objRemesa.UsuarioModificacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_EXTERNO", ProsegurDbType.Descricao_Longa, objRemesa.CodigoExterno))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_GESTION_BULTO", ProsegurDbType.Logico, objRemesa.TrabajaPorBulto))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_ABONO", ProsegurDbType.Identificador_Alfanumerico, Enumeradores.EstadoAbonoElemento.NoAbonado.RecuperarValor()))

            If objRemesa.CuentaSaldo IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO", ProsegurDbType.Objeto_Id, objRemesa.CuentaSaldo.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If _transacion IsNot Nothing Then
                _transacion.AdicionarItemTransacao(cmd)
            Else
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            End If

        End Sub

#End Region

#Region "[ACTUALIZAR]"

        Public Shared Function ActualizarFyhInicio(identificadorOT As String, fechaHoraActualizacion As DateTime, codigoUsuario As String) As Boolean

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.Salidas_Remesa_ActualizarFyhInicioRemesa)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_OT", ProsegurDbType.Objeto_Id, identificadorOT))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_INICIO", ProsegurDbType.Data_Hora, fechaHoraActualizacion))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Function

        ''' <summary>
        ''' Actualizar Remesa.
        ''' </summary>
        ''' <param name="objRemesa">Objeto remessa preenchido.</param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarRemesa(objRemesa As Clases.Remesa)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RemessaAtualizar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, objRemesa.Identificador))

            If objRemesa.RemesaOrigen IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA_ORIGEN", ProsegurDbType.Descricao_Longa, objRemesa.RemesaOrigen.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA_ORIGEN", ProsegurDbType.Descricao_Longa, Nothing))
            End If

            If objRemesa.GrupoTerminosIAC IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC", ProsegurDbType.Objeto_Id, objRemesa.GrupoTerminosIAC.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC", ProsegurDbType.Objeto_Id, Nothing))
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, objRemesa.IdentificadorDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_RECIBO_SALIDA", ProsegurDbType.Descricao_Curta, objRemesa.ReciboTransporte))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, objRemesa.Estado.RecuperarValor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_USUARIO_RESPONSABLE", ProsegurDbType.Descricao_Longa, objRemesa.UsuarioResponsable))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PUESTO_RESPONSABLE", ProsegurDbType.Descricao_Curta, objRemesa.PuestoResponsable))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_RUTA", ProsegurDbType.Descricao_Curta, objRemesa.Ruta))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_PARADA", ProsegurDbType.Inteiro_Curto, objRemesa.Parada))

            If objRemesa.Bultos IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_CANTIDAD_BULTOS", ProsegurDbType.Descricao_Curta, objRemesa.Bultos.Count))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_CANTIDAD_BULTOS", ProsegurDbType.Descricao_Curta, Nothing))
            End If

            If objRemesa.FechaHoraInicioConteo <> DateTime.MinValue Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_CONTEO_INICIO", ProsegurDbType.Data_Hora, objRemesa.FechaHoraInicioConteo))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_CONTEO_INICIO", ProsegurDbType.Data_Hora, Nothing))
            End If

            If objRemesa.FechaHoraFinConteo <> DateTime.MinValue Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_CONTEO_FIN", ProsegurDbType.Data_Hora, objRemesa.FechaHoraFinConteo))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_CONTEO_FIN", ProsegurDbType.Data_Hora, Nothing))
            End If

            If objRemesa.FechaHoraTransporte <> DateTime.MinValue Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_TRANSPORTE", ProsegurDbType.Data_Hora, objRemesa.FechaHoraTransporte))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_TRANSPORTE", ProsegurDbType.Data_Hora, Nothing))
            End If

            If objRemesa.ElementoPadre IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA_PADRE", ProsegurDbType.Objeto_Id, objRemesa.ElementoPadre.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA_PADRE", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If objRemesa.ElementoSustituto IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA_SUSTITUTA", ProsegurDbType.Objeto_Id, objRemesa.ElementoSustituto.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA_SUSTITUTA", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If objRemesa.DatosATM IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CAJERO", ProsegurDbType.Descricao_Curta, objRemesa.DatosATM.CodigoCajero))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CAJERO", ProsegurDbType.Descricao_Curta, Nothing))
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_NIVEL_DETALLE", ProsegurDbType.Descricao_Curta, objRemesa.ConfiguracionNivelSaldos.RecuperarValor))

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, objRemesa.UsuarioModificacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA", ProsegurDbType.Objeto_Id, objRemesa.Cuenta.Identificador))

            If objRemesa.CuentaSaldo IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO", ProsegurDbType.Objeto_Id, objRemesa.CuentaSaldo.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO", ProsegurDbType.Objeto_Id, Nothing))
            End If

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        End Sub

        ''' <summary>
        ''' Actualizar estado de la remesa.
        ''' </summary>
        ''' <param name="objRemesa">Objeto re preenchido.</param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarEstadoRemesa(objRemesa As Clases.Remesa)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RemessaAtualizarEstado)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, objRemesa.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, objRemesa.Estado.RecuperarValor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, objRemesa.UsuarioModificacion))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ''' <summary>
        ''' Actualizar estado del abono de la remesa.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarEstadoAbonoRemesa(identificadorRemesa As String, codigoEstadoAbono As String, usuario As String,
                                                      soloCuandoNoEsNoAbonado As Boolean,
                                                      ByRef transaccion As DataBaseHelper.Transaccion)

            Dim query As String = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RemessaAtualizarEstadoAbono)

            If soloCuandoNoEsNoAbonado Then
                query += " AND COD_ESTADO_ABONO <> 'NA' "
            End If

            Dim wrapper As New DataBaseHelper.SPWrapper(query, True, CommandType.Text)

            wrapper.AgregarParam("OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa)
            wrapper.AgregarParam("COD_ESTADO_ABONO", ProsegurDbType.Descricao_Curta, codigoEstadoAbono)
            wrapper.AgregarParam("DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

        End Sub
        Public Shared Function HayDiferencaEnLaRemesa(identificadorRemesa As String) As Boolean

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim dt As New DataTable
            Dim hayDiferenca As Boolean = False
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RemessaHayDiferenca)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))

            dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row In dt.Rows
                    If Util.AtribuirValorObj(Of Boolean)(row("DIF")) Then
                        hayDiferenca = True
                        Exit For
                    End If
                Next
            End If
            Return hayDiferenca
        End Function

        Public Shared Sub ActualizarEstadoRemesa(identificador As String,
                                                 estado As Enumeradores.EstadoRemesa,
                                                 usuario As String,
                                                 cuantidadBultos As Integer,
                             Optional ByRef _transacion As DbHelper.Transacao = Nothing)

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    command.CommandText = My.Resources.RemessaAtualizarEstado

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificador))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, estado.RecuperarValor))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))
                    command.CommandText &= " AND (SELECT COUNT(1) FROM SAPR_TBULTO B WHERE B.OID_REMESA = []OID_REMESA) = []CUANTIDADBULTOS "
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "CUANTIDADBULTOS", ProsegurDbType.Inteiro_Curto, cuantidadBultos))

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, command.CommandText)
                    command.CommandType = CommandType.Text

                    If _transacion IsNot Nothing Then
                        _transacion.AdicionarItemTransacao(command)
                    Else
                        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, command)
                    End If

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

        End Sub

        ''' <summary>
        ''' Actualizar cuenta de la remesa.
        ''' </summary>
        ''' <param name="identificadorRemesa"></param>
        ''' <param name="identificadorCuenta"></param>
        ''' <param name="usuario"></param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarCuenta(identificadorRemesa As String, identificadorCuenta As String, usuario As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RemesaActualizarCuenta)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA", ProsegurDbType.Descricao_Curta, identificadorCuenta))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ''' <summary>
        ''' Actualizar identificador documento de la Remesa
        ''' </summary>
        ''' <param name="objRemesa">Objeto parcial preenchido.</param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarIdentificadorDocumentoDeLaRemesa(objRemesa As Clases.Remesa)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RemessaAtualizarIdentificadorDocumento)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, objRemesa.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Descricao_Curta, objRemesa.IdentificadorDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, objRemesa.UsuarioModificacion))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        Public Shared Function AnularRemesasSaldos(IdentificadoresRemesasSaldos As List(Of String), codigoUsuario As String, fechahoraActualizacion As DateTime) As Integer

            'Cria o comando SQL 
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandType = CommandType.Text
            cmd.CommandText = My.Resources.RemesaActualizarBolAnuladoSalidas
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, IdentificadoresRemesasSaldos, "OID_REMESA", cmd, "AND", "R")))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, codigoUsuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, fechahoraActualizacion))

            Return AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        End Function

        Public Shared Sub ActualizarCodigoRutaRemesa(IdenticadoresExternos As List(Of String),
                                                     CodigoRuta As String)

            'Cria comando SQL
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            comando.CommandType = CommandType.Text
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RemesaAcutalizarRotaConOIdExterno)
            comando.CommandText = String.Format(comando.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, IdenticadoresExternos, "OID_EXTERNO", comando, "WHERE", "R"))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_RUTA", ProsegurDbType.Identificador_Alfanumerico, CodigoRuta))
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, comando)
        End Sub

        Public Shared Function ActualizaRemesasSaldos(identificadoresRemesasSaldos As List(Of String),
                                                      CodigoRuta As String,
                                                      FechaSalida As DateTime,
                                                      codigoSecuencia As String) As Integer

            ActualizarTerminoDocumentoSaldos(identificadoresRemesasSaldos, FechaSalida, codigoSecuencia)

            'Cria o comando SQL 
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandType = CommandType.Text
            cmd.CommandText = My.Resources.RemesaActualizaRuta
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, String.Format(cmd.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresRemesasSaldos, "OID_REMESA", cmd, "AND", "R")))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_RUTA", ProsegurDbType.Descricao_Longa, CodigoRuta))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_SALIDA", ProsegurDbType.Data_Hora, FechaSalida))

            Return AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        End Function

        Private Shared Sub ActualizarTerminoDocumentoSaldos(identificadoresRemesasSaldos As List(Of String),
                                                            fechaSalida As DateTime,
                                                            codigoSecuencia As String)

            Dim cmdIDDocumentos As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim cmdDocumentosTerminos As IDbCommand = Nothing

            Try

                Dim IdentificadoresDocumentos As New List(Of String)

                cmdIDDocumentos.CommandType = CommandType.Text
                cmdIDDocumentos.CommandText = My.Resources.RemesaRecuperarIdentificadorDocumento

                cmdIDDocumentos.CommandText = String.Format(cmdIDDocumentos.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresRemesasSaldos, "OID_REMESA", cmdIDDocumentos, "AND", "R"))
                cmdIDDocumentos.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmdIDDocumentos.CommandText)

                Dim dtIdentificadoresDocumentos As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmdIDDocumentos)

                If dtIdentificadoresDocumentos IsNot Nothing AndAlso dtIdentificadoresDocumentos.Rows.Count > 0 Then

                    For Each row In dtIdentificadoresDocumentos.Rows
                        IdentificadoresDocumentos.Add(row("OID_DOCUMENTO"))
                    Next

                    cmdIDDocumentos.Dispose()

                End If

                If IdentificadoresDocumentos.Count > 0 Then

                    Dim horaSalida As String = "00:00:00"

                    If Not IsDBNull(fechaSalida) AndAlso fechaSalida <> Date.MinValue Then
                        ' recupera a hora da data
                        horaSalida = fechaSalida.ToString("h:mm:ss tt", CultureInfo.InvariantCulture)
                    End If

                    Dim terminos As New List(Of Tuple(Of String, String)) From
                            {
                                New Tuple(Of String, String)(Enumeradores.TerminosDatosRuta.NumeroSecuencia.RecuperarValor, If(String.IsNullOrEmpty(codigoSecuencia), "0", codigoSecuencia)),
                                New Tuple(Of String, String)(Enumeradores.TerminosDatosRuta.HoraSalida.RecuperarValor, horaSalida)
                            }

                    For Each termino In terminos

                        cmdDocumentosTerminos = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                        cmdDocumentosTerminos.CommandType = CommandType.Text
                        cmdDocumentosTerminos.CommandText = My.Resources.RemesaActualizarTerminoDocumento

                        cmdDocumentosTerminos.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TERMINO", ProsegurDbType.Descricao_Longa, termino.Item1))
                        cmdDocumentosTerminos.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "VALOR_TERMINO", ProsegurDbType.Descricao_Longa, termino.Item2))

                        cmdDocumentosTerminos.CommandText = String.Format(cmdDocumentosTerminos.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, IdentificadoresDocumentos, "OID_DOCUMENTO", cmdDocumentosTerminos, "AND", "TDOC"))
                        cmdDocumentosTerminos.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmdDocumentosTerminos.CommandText)

                        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmdDocumentosTerminos)

                        cmdDocumentosTerminos = Nothing

                    Next termino

                End If

            Catch ex As Exception
                Throw

            Finally
                If cmdIDDocumentos IsNot Nothing Then cmdIDDocumentos.Dispose()
                If cmdDocumentosTerminos IsNot Nothing Then cmdDocumentosTerminos.Dispose()
            End Try

        End Sub

#End Region

#Region "[ELIMINAR]"

        ''' <summary>
        ''' Eliminar la remesa.
        ''' </summary>
        ''' <param name="identificadorRemesa">identificadorRemesa.</param>
        ''' <remarks></remarks>
        Public Shared Sub EliminarRemesa(identificadorRemesa As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RemesaExcluir)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

#End Region

        Public Shared Sub MarcarNoEntregue(identificador As String, noEntrega As Boolean)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.MarcarNoEntregue)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_NOENTREGUE", ProsegurDbType.Logico, noEntrega))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        Shared Function verificaGestionPorBulto(identificador As String, codigoExterno As String) As Boolean

            Dim gestionaPorBulto As Boolean

            ' TO DO

            Return gestionaPorBulto

        End Function

    End Class

End Namespace
