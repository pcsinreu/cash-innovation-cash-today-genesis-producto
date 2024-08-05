Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports System.Threading.Tasks

' Alias
Imports CSCertificacion = Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion

Namespace GenesisSaldos

    Public Class Cuenta

#Region "[Obtener Cuenta]"

        Public Shared Function cargarCuentas(dtCuentas As DataTable, dtCaracteristicasTipoSector As DataTable) As ObservableCollection(Of Clases.Cuenta)

            Dim cuentas As New ObservableCollection(Of Clases.Cuenta)

            If dtCuentas IsNot Nothing AndAlso dtCuentas.Rows.Count > 0 Then

                Dim identificadoresCaracteristicasTipoSector As List(Of String) = dtCuentas.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_TIPO_SECTOR") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_TIPO_SECTOR"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_TIPO_SECTOR")) _
                                                                         .Distinct() _
                                                                         .ToList()

                For Each rowCuenta In dtCuentas.Rows

                    Dim cuenta As New Clases.Cuenta

                    Dim caracteristicas As New ObservableCollection(Of Enumeradores.CaracteristicaTipoSector)
                    If rowCuenta.Table.Columns.Contains("OID_TIPO_SECTOR") Then
                        Dim _CaracteristicasTipoSector = dtCaracteristicasTipoSector.Select(" OID_TIPO_SECTOR = '" & Util.AtribuirValorObj(rowCuenta("OID_TIPO_SECTOR"), GetType(String)) & "'")

                        If _CaracteristicasTipoSector IsNot Nothing Then
                            For Each _valor In _CaracteristicasTipoSector
                                If ExisteEnum(Of Enumeradores.CaracteristicaTipoSector)(_valor("COD_CARACT_TIPOSECTOR")) Then
                                    caracteristicas.Add(RecuperarEnum(Of Enumeradores.CaracteristicaTipoSector)(_valor("COD_CARACT_TIPOSECTOR").ToString))
                                End If
                            Next
                        End If
                    End If

                    With cuenta
                        ' CU - Cuenta
                        .Identificador = If(rowCuenta.Table.Columns.Contains("OID_CUENTA"), Util.AtribuirValorObj(rowCuenta("OID_CUENTA"), GetType(String)), Nothing)

                        .UsuarioCreacion = If(rowCuenta.Table.Columns.Contains("DES_USUARIO_CREACION"), Util.AtribuirValorObj(rowCuenta("DES_USUARIO_CREACION"), GetType(String)), Nothing)
                        .UsuarioModificacion = If(rowCuenta.Table.Columns.Contains("DES_USUARIO_MODIFICACION"), Util.AtribuirValorObj(rowCuenta("DES_USUARIO_MODIFICACION"), GetType(String)), Nothing)

                        If rowCuenta.Table.Columns.Contains("COD_TIPO_CUENTA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowCuenta("COD_TIPO_CUENTA"), GetType(String))) Then
                            Select Case Util.AtribuirValorObj(rowCuenta("COD_TIPO_CUENTA"), GetType(String))
                                Case "A"
                                    .TipoCuenta = Enumeradores.TipoCuenta.Ambos
                                Case "M"
                                    .TipoCuenta = Enumeradores.TipoCuenta.Movimiento
                                Case "S"
                                    .TipoCuenta = Enumeradores.TipoCuenta.Saldo
                            End Select
                        End If

                        ' CL - Clienta
                        .Cliente = New Clases.Cliente With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_CLIENTE"), Util.AtribuirValorObj(rowCuenta("COD_CLIENTE"), GetType(String)), Nothing), _
                                                            .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_CLIENTE"), Util.AtribuirValorObj(rowCuenta("DES_CLIENTE"), GetType(String)), Nothing), _
                                                            .EstaActivo = If(rowCuenta.Table.Columns.Contains("CL_BOL_VIGENTE"), Util.AtribuirValorObj(rowCuenta("CL_BOL_VIGENTE"), GetType(Boolean)), Nothing), _
                                                            .EsTotalizadorSaldo = If(rowCuenta.Table.Columns.Contains("CL_BOL_TOTALIZADOR_SALDO"), Util.AtribuirValorObj(rowCuenta("CL_BOL_TOTALIZADOR_SALDO"), GetType(Boolean)), Nothing), _
                                                            .EstaEnviadoSaldos = If(rowCuenta.Table.Columns.Contains("CL_BOL_ENVIADO_SALDOS"), Util.AtribuirValorObj(rowCuenta("CL_BOL_ENVIADO_SALDOS"), GetType(Boolean)), Nothing), _
                                                            .Identificador = If(rowCuenta.Table.Columns.Contains("OID_CLIENTE"), Util.AtribuirValorObj(rowCuenta("OID_CLIENTE"), GetType(String)), Nothing)}

                        ' PTO - Punto Servicio
                        If rowCuenta.Table.Columns.Contains("OID_PTO_SERVICIO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowCuenta("OID_PTO_SERVICIO"), GetType(String))) Then

                            .PuntoServicio = New Clases.PuntoServicio With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_PTO_SERVICIO"), Util.AtribuirValorObj(rowCuenta("COD_PTO_SERVICIO"), GetType(String)), Nothing), _
                                                                            .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_PTO_SERVICIO"), Util.AtribuirValorObj(rowCuenta("DES_PTO_SERVICIO"), GetType(String)), Nothing), _
                                                                            .EstaActivo = If(rowCuenta.Table.Columns.Contains("PTO_BOL_VIGENTE"), Util.AtribuirValorObj(rowCuenta("PTO_BOL_VIGENTE"), GetType(Boolean)), Nothing), _
                                                                            .EstaEnviadoSaldos = If(rowCuenta.Table.Columns.Contains("PTO_BOL_ENVIADO_SALDOS"), Util.AtribuirValorObj(rowCuenta("PTO_BOL_ENVIADO_SALDOS"), GetType(Boolean)), Nothing), _
                                                                            .EsTotalizadorSaldo = If(rowCuenta.Table.Columns.Contains("PTO_BOL_TOTALIZADOR_SALDO"), Util.AtribuirValorObj(rowCuenta("PTO_BOL_TOTALIZADOR_SALDO"), GetType(Boolean)), Nothing), _
                                                                            .Identificador = If(rowCuenta.Table.Columns.Contains("OID_PTO_SERVICIO"), Util.AtribuirValorObj(rowCuenta("OID_PTO_SERVICIO"), GetType(String)), Nothing)}

                        End If


                        ' SCL - SubCliente
                        If rowCuenta.Table.Columns.Contains("OID_SUBCLIENTE") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowCuenta("OID_SUBCLIENTE"), GetType(String))) Then

                            .SubCliente = New Clases.SubCliente With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_SUBCLIENTE"), Util.AtribuirValorObj(rowCuenta("COD_SUBCLIENTE"), GetType(String)), Nothing), _
                                                                      .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_SUBCLIENTE"), Util.AtribuirValorObj(rowCuenta("DES_SUBCLIENTE"), GetType(String)), Nothing), _
                                                                      .EstaActivo = If(rowCuenta.Table.Columns.Contains("SCL_BOL_VIGENTE"), Util.AtribuirValorObj(rowCuenta("SCL_BOL_VIGENTE"), GetType(Boolean)), Nothing), _
                                                                      .EstaEnviadoSaldos = If(rowCuenta.Table.Columns.Contains("SCL_BOL_ENVIADO_SALDOS"), Util.AtribuirValorObj(rowCuenta("SCL_BOL_ENVIADO_SALDOS"), GetType(Boolean)), Nothing), _
                                                                      .EsTotalizadorSaldo = If(rowCuenta.Table.Columns.Contains("SCL_BOL_TOTALIZADOR_SALDO"), Util.AtribuirValorObj(rowCuenta("SCL_BOL_TOTALIZADOR_SALDO"), GetType(Boolean)), Nothing), _
                                                                      .Identificador = If(rowCuenta.Table.Columns.Contains("OID_SUBCLIENTE"), Util.AtribuirValorObj(rowCuenta("OID_SUBCLIENTE"), GetType(String)), Nothing)}

                        End If

                        ' CAN - Canal
                        .Canal = New Clases.Canal With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_CANAL"), Util.AtribuirValorObj(rowCuenta("COD_CANAL"), GetType(String)), Nothing), _
                                                        .Identificador = If(rowCuenta.Table.Columns.Contains("OID_CANAL"), Util.AtribuirValorObj(rowCuenta("OID_CANAL"), GetType(String)), Nothing), _
                                                        .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_CANAL"), Util.AtribuirValorObj(rowCuenta("DES_CANAL"), GetType(String)), Nothing), _
                                                        .EstaActivo = If(rowCuenta.Table.Columns.Contains("CAN_BOL_VIGENTE"), Util.AtribuirValorObj(rowCuenta("CAN_BOL_VIGENTE"), GetType(Boolean)), Nothing)}

                        ' SBC - SubCanal
                        .SubCanal = New Clases.SubCanal With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_SUBCANAL"), Util.AtribuirValorObj(rowCuenta("COD_SUBCANAL"), GetType(String)), Nothing), _
                                                              .Identificador = If(rowCuenta.Table.Columns.Contains("OID_SUBCANAL"), Util.AtribuirValorObj(rowCuenta("OID_SUBCANAL"), GetType(String)), Nothing), _
                                                              .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_SUBCANAL"), Util.AtribuirValorObj(rowCuenta("DES_SUBCANAL"), GetType(String)), Nothing), _
                                                              .EstaActivo = If(rowCuenta.Table.Columns.Contains("SBC_BOL_VIGENTE"), Util.AtribuirValorObj(rowCuenta("SBC_BOL_VIGENTE"), GetType(Boolean)), Nothing)}

                        .Sector = New Clases.Sector With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_SECTOR"), Util.AtribuirValorObj(rowCuenta("COD_SECTOR"), GetType(String)), Nothing), _
                                                          .Identificador = If(rowCuenta.Table.Columns.Contains("OID_SECTOR"), Util.AtribuirValorObj(rowCuenta("OID_SECTOR"), GetType(String)), Nothing), _
                                                          .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_SECTOR"), Util.AtribuirValorObj(rowCuenta("DES_SECTOR"), GetType(String)), Nothing), _
                                                          .EsActivo = If(rowCuenta.Table.Columns.Contains("SE_BOL_ACTIVO"), Util.AtribuirValorObj(rowCuenta("SE_BOL_ACTIVO"), GetType(Boolean)), Nothing), _
                                                          .EsCentroProceso = If(rowCuenta.Table.Columns.Contains("BOL_CENTRO_PROCESO"), Util.AtribuirValorObj(rowCuenta("BOL_CENTRO_PROCESO"), GetType(Boolean)), Nothing), _
                                                          .EsConteo = If(rowCuenta.Table.Columns.Contains("BOL_CONTEO"), Util.AtribuirValorObj(rowCuenta("BOL_CONTEO"), GetType(Boolean)), Nothing), _
                                                          .EsTesoro = If(rowCuenta.Table.Columns.Contains("BOL_TESORO"), Util.AtribuirValorObj(rowCuenta("BOL_TESORO"), GetType(Boolean)), Nothing), _
                                                          .PemitirDisponerValor = If(rowCuenta.Table.Columns.Contains("BOL_PERMITE_DISPONER_VALOR"), Util.AtribuirValorObj(rowCuenta("BOL_PERMITE_DISPONER_VALOR"), GetType(Boolean)), Nothing), _
                                                          .Delegacion = New Clases.Delegacion With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_DELEGACION"), Util.AtribuirValorObj(rowCuenta("COD_DELEGACION"), GetType(String)), Nothing), _
                                                                                            .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_DELEGACION"), Util.AtribuirValorObj(rowCuenta("DES_DELEGACION"), GetType(String)), Nothing), _
                                                                                            .Identificador = If(rowCuenta.Table.Columns.Contains("OID_DELEGACION"), Util.AtribuirValorObj(rowCuenta("OID_DELEGACION"), GetType(String)), Nothing), _
                                                                                            .EsActivo = If(rowCuenta.Table.Columns.Contains("D_BOL_VIGENTE"), Util.AtribuirValorObj(rowCuenta("D_BOL_VIGENTE"), GetType(Boolean)), Nothing), _
                                                                                            .HusoHorarioEnMinutos = If(rowCuenta.Table.Columns.Contains("NEC_GMT_MINUTOS"), Util.AtribuirValorObj(rowCuenta("NEC_GMT_MINUTOS"), GetType(Integer)), Nothing), _
                                                                                            .FechaHoraVeranoInicio = If(rowCuenta.Table.Columns.Contains("FYH_VERANO_INICIO"), Util.AtribuirValorObj(rowCuenta("FYH_VERANO_INICIO"), GetType(Date)), Nothing), _
                                                                                            .FechaHoraVeranoFin = If(rowCuenta.Table.Columns.Contains("FYH_VERANO_FIN"), Util.AtribuirValorObj(rowCuenta("FYH_VERANO_FIN"), GetType(Date)), Nothing), _
                                                                                            .AjusteHorarioVerano = If(rowCuenta.Table.Columns.Contains("NEC_VERANO_AJUSTE"), Util.AtribuirValorObj(rowCuenta("NEC_VERANO_AJUSTE"), GetType(Integer)), Nothing), _
                                                                                            .Zona = If(rowCuenta.Table.Columns.Contains("DES_ZONA"), Util.AtribuirValorObj(rowCuenta("DES_ZONA"), GetType(String)), Nothing), _
                                                                                            .CodigoPais = If(rowCuenta.Table.Columns.Contains("COD_PAIS"), Util.AtribuirValorObj(rowCuenta("COD_PAIS"), GetType(String)), Nothing)}, _
                                                          .TipoSector = New Clases.TipoSector With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_TIPO_SECTOR"), Util.AtribuirValorObj(rowCuenta("COD_TIPO_SECTOR"), GetType(String)), Nothing), _
                                                                                            .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_TIPO_SECTOR"), Util.AtribuirValorObj(rowCuenta("DES_TIPO_SECTOR"), GetType(String)), Nothing), _
                                                                                            .Identificador = If(rowCuenta.Table.Columns.Contains("OID_TIPO_SECTOR"), Util.AtribuirValorObj(rowCuenta("OID_TIPO_SECTOR"), GetType(String)), Nothing), _
                                                                                            .EstaActivo = If(rowCuenta.Table.Columns.Contains("TSE_BOL_ACTIVO"), Util.AtribuirValorObj(rowCuenta("TSE_BOL_ACTIVO"), GetType(Boolean)), Nothing), _
                                                                                            .CaracteristicasTipoSector = caracteristicas}, _
                                                          .Planta = New Clases.Planta With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_PLANTA"), Util.AtribuirValorObj(rowCuenta("COD_PLANTA"), GetType(String)), Nothing), _
                                                                                            .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_PLANTA"), Util.AtribuirValorObj(rowCuenta("DES_PLANTA"), GetType(String)), Nothing), _
                                                                                            .Identificador = If(rowCuenta.Table.Columns.Contains("OID_PLANTA"), Util.AtribuirValorObj(rowCuenta("OID_PLANTA"), GetType(String)), Nothing), _
                                                                                            .EsActivo = If(rowCuenta.Table.Columns.Contains("P_BOL_ACTIVO"), Util.AtribuirValorObj(rowCuenta("P_BOL_ACTIVO"), GetType(Boolean)), Nothing)}}

                    End With

                    cuentas.Add(cuenta)
                Next
            End If

            Return cuentas

        End Function















        Public Shared Function ObtenerCuentaPorIdentificador(IdentificadorCuenta As String,
                                                             TipoCuenta As Enumeradores.TipoCuenta,
                                                             DescripcionUsuario As String) As Clases.Cuenta

            Dim _cuentas As ObservableCollection(Of Clases.Cuenta) = ObtenerCuentasPorIdentificadores(New List(Of String) From {IdentificadorCuenta}, TipoCuenta, DescripcionUsuario)
            If _cuentas IsNot Nothing AndAlso _cuentas.Count > 0 Then
                Return _cuentas(0)
            End If
            Return Nothing
        End Function

        Public Shared Function ObtenerCuentasPorIdentificadores(IdentificadoresCuentas As List(Of String),
                                                                TipoCuenta As Enumeradores.TipoCuenta,
                                                                DescripcionUsuario As String,
                                                       Optional ByRef log As StringBuilder = Nothing) As ObservableCollection(Of Clases.Cuenta)

            Dim _cuentas As New ObservableCollection(Of Clases.Cuenta)

            If log Is Nothing Then log = New StringBuilder
            Dim Tiempo As DateTime = Now

            Dim dtCuentas As DataTable = HacerConsultaConCuentas(My.Resources.CuentaObtenerCuenta_v2.ToString(), IdentificadoresCuentas)
            log.AppendLine("_____Tiempo 'ObtenerCuentasPorIdentificadores_v2.HacerConsulta': " & Now.Subtract(Tiempo).ToString() & "; ")

            Tiempo = Now
            _cuentas = cargarCuentas(dtCuentas, TipoCuenta, DescripcionUsuario)
            log.AppendLine("_____Tiempo 'ObtenerCuentasPorIdentificadores_v2.Probar': " & Now.Subtract(Tiempo).ToString() & "; ")

            Return _cuentas

        End Function

        Private Shared Function cargarCuentas(dtCuentas As DataTable,
                                             TipoCuenta As Enumeradores.TipoCuenta,
                                             DescripcionUsuario As String) As ObservableCollection(Of Clases.Cuenta)

            Dim cuentas As New ObservableCollection(Of Clases.Cuenta)

            If dtCuentas IsNot Nothing AndAlso dtCuentas.Rows.Count > 0 Then

                Dim identificadoresCaracteristicasTipoSector As List(Of String) = dtCuentas.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_TIPO_SECTOR") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_TIPO_SECTOR"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_TIPO_SECTOR")) _
                                                                         .Distinct() _
                                                                         .ToList()

                Dim dtCaracteristicasTipoSector As DataTable = Genesis.CaracteristicaTipoSector.obtenerCaracteristicasPorTipoSector(identificadoresCaracteristicasTipoSector)

                For Each rowCuenta In dtCuentas.Rows

                    Dim cuenta As New Clases.Cuenta

                    Dim caracteristicas As New ObservableCollection(Of Enumeradores.CaracteristicaTipoSector)
                    If rowCuenta.Table.Columns.Contains("OID_TIPO_SECTOR") Then
                        Dim _CaracteristicasTipoSector = dtCaracteristicasTipoSector.Select(" OID_TIPO_SECTOR = '" & Util.AtribuirValorObj(rowCuenta("OID_TIPO_SECTOR"), GetType(String)) & "'")

                        If _CaracteristicasTipoSector IsNot Nothing Then
                            For Each _valor In _CaracteristicasTipoSector
                                If ExisteEnum(Of Enumeradores.CaracteristicaTipoSector)(_valor("COD_CARACT_TIPOSECTOR")) Then
                                    caracteristicas.Add(RecuperarEnum(Of Enumeradores.CaracteristicaTipoSector)(_valor("COD_CARACT_TIPOSECTOR").ToString))
                                End If
                            Next
                        End If
                    End If

                    With cuenta
                        ' CU - Cuenta
                        .Identificador = If(rowCuenta.Table.Columns.Contains("OID_CUENTA"), Util.AtribuirValorObj(rowCuenta("OID_CUENTA"), GetType(String)), Nothing)

                        .UsuarioCreacion = If(rowCuenta.Table.Columns.Contains("DES_USUARIO_CREACION"), Util.AtribuirValorObj(rowCuenta("DES_USUARIO_CREACION"), GetType(String)), Nothing)
                        .UsuarioModificacion = If(rowCuenta.Table.Columns.Contains("DES_USUARIO_MODIFICACION"), Util.AtribuirValorObj(rowCuenta("DES_USUARIO_MODIFICACION"), GetType(String)), Nothing)

                        If rowCuenta.Table.Columns.Contains("COD_TIPO_CUENTA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowCuenta("COD_TIPO_CUENTA"), GetType(String))) Then
                            Select Case Util.AtribuirValorObj(rowCuenta("COD_TIPO_CUENTA"), GetType(String))
                                Case "A"
                                    .TipoCuenta = Enumeradores.TipoCuenta.Ambos
                                Case "M"
                                    .TipoCuenta = Enumeradores.TipoCuenta.Movimiento
                                Case "S"
                                    .TipoCuenta = Enumeradores.TipoCuenta.Saldo
                            End Select
                        End If

                        ' CL - Clienta
                        .Cliente = New Clases.Cliente With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_CLIENTE"), Util.AtribuirValorObj(rowCuenta("COD_CLIENTE"), GetType(String)), Nothing), _
                                                            .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_CLIENTE"), Util.AtribuirValorObj(rowCuenta("DES_CLIENTE"), GetType(String)), Nothing), _
                                                            .EstaActivo = If(rowCuenta.Table.Columns.Contains("CL_BOL_VIGENTE"), Util.AtribuirValorObj(rowCuenta("CL_BOL_VIGENTE"), GetType(Boolean)), Nothing), _
                                                            .EsTotalizadorSaldo = If(rowCuenta.Table.Columns.Contains("CL_BOL_TOTALIZADOR_SALDO"), Util.AtribuirValorObj(rowCuenta("CL_BOL_TOTALIZADOR_SALDO"), GetType(Boolean)), Nothing), _
                                                            .EstaEnviadoSaldos = If(rowCuenta.Table.Columns.Contains("CL_BOL_ENVIADO_SALDOS"), Util.AtribuirValorObj(rowCuenta("CL_BOL_ENVIADO_SALDOS"), GetType(Boolean)), Nothing), _
                                                            .Identificador = If(rowCuenta.Table.Columns.Contains("OID_CLIENTE"), Util.AtribuirValorObj(rowCuenta("OID_CLIENTE"), GetType(String)), Nothing)}

                        ' PTO - Punto Servicio
                        If rowCuenta.Table.Columns.Contains("OID_PTO_SERVICIO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowCuenta("OID_PTO_SERVICIO"), GetType(String))) Then

                            .PuntoServicio = New Clases.PuntoServicio With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_PTO_SERVICIO"), Util.AtribuirValorObj(rowCuenta("COD_PTO_SERVICIO"), GetType(String)), Nothing), _
                                                                            .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_PTO_SERVICIO"), Util.AtribuirValorObj(rowCuenta("DES_PTO_SERVICIO"), GetType(String)), Nothing), _
                                                                            .EstaActivo = If(rowCuenta.Table.Columns.Contains("PTO_BOL_VIGENTE"), Util.AtribuirValorObj(rowCuenta("PTO_BOL_VIGENTE"), GetType(Boolean)), Nothing), _
                                                                            .EstaEnviadoSaldos = If(rowCuenta.Table.Columns.Contains("PTO_BOL_ENVIADO_SALDOS"), Util.AtribuirValorObj(rowCuenta("PTO_BOL_ENVIADO_SALDOS"), GetType(Boolean)), Nothing), _
                                                                            .EsTotalizadorSaldo = If(rowCuenta.Table.Columns.Contains("PTO_BOL_TOTALIZADOR_SALDO"), Util.AtribuirValorObj(rowCuenta("PTO_BOL_TOTALIZADOR_SALDO"), GetType(Boolean)), Nothing), _
                                                                            .Identificador = If(rowCuenta.Table.Columns.Contains("OID_PTO_SERVICIO"), Util.AtribuirValorObj(rowCuenta("OID_PTO_SERVICIO"), GetType(String)), Nothing)}

                        End If


                        ' SCL - SubCliente
                        If rowCuenta.Table.Columns.Contains("OID_SUBCLIENTE") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowCuenta("OID_SUBCLIENTE"), GetType(String))) Then

                            .SubCliente = New Clases.SubCliente With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_SUBCLIENTE"), Util.AtribuirValorObj(rowCuenta("COD_SUBCLIENTE"), GetType(String)), Nothing), _
                                                                      .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_SUBCLIENTE"), Util.AtribuirValorObj(rowCuenta("DES_SUBCLIENTE"), GetType(String)), Nothing), _
                                                                      .EstaActivo = If(rowCuenta.Table.Columns.Contains("SCL_BOL_VIGENTE"), Util.AtribuirValorObj(rowCuenta("SCL_BOL_VIGENTE"), GetType(Boolean)), Nothing), _
                                                                      .EstaEnviadoSaldos = If(rowCuenta.Table.Columns.Contains("SCL_BOL_ENVIADO_SALDOS"), Util.AtribuirValorObj(rowCuenta("SCL_BOL_ENVIADO_SALDOS"), GetType(Boolean)), Nothing), _
                                                                      .EsTotalizadorSaldo = If(rowCuenta.Table.Columns.Contains("SCL_BOL_TOTALIZADOR_SALDO"), Util.AtribuirValorObj(rowCuenta("SCL_BOL_TOTALIZADOR_SALDO"), GetType(Boolean)), Nothing), _
                                                                      .Identificador = If(rowCuenta.Table.Columns.Contains("OID_SUBCLIENTE"), Util.AtribuirValorObj(rowCuenta("OID_SUBCLIENTE"), GetType(String)), Nothing)}

                        End If

                        ' CAN - Canal
                        .Canal = New Clases.Canal With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_CANAL"), Util.AtribuirValorObj(rowCuenta("COD_CANAL"), GetType(String)), Nothing), _
                                                        .Identificador = If(rowCuenta.Table.Columns.Contains("OID_CANAL"), Util.AtribuirValorObj(rowCuenta("OID_CANAL"), GetType(String)), Nothing), _
                                                        .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_CANAL"), Util.AtribuirValorObj(rowCuenta("DES_CANAL"), GetType(String)), Nothing), _
                                                        .EstaActivo = If(rowCuenta.Table.Columns.Contains("CAN_BOL_VIGENTE"), Util.AtribuirValorObj(rowCuenta("CAN_BOL_VIGENTE"), GetType(Boolean)), Nothing)}

                        ' SBC - SubCanal
                        .SubCanal = New Clases.SubCanal With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_SUBCANAL"), Util.AtribuirValorObj(rowCuenta("COD_SUBCANAL"), GetType(String)), Nothing), _
                                                              .Identificador = If(rowCuenta.Table.Columns.Contains("OID_SUBCANAL"), Util.AtribuirValorObj(rowCuenta("OID_SUBCANAL"), GetType(String)), Nothing), _
                                                              .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_SUBCANAL"), Util.AtribuirValorObj(rowCuenta("DES_SUBCANAL"), GetType(String)), Nothing), _
                                                              .EstaActivo = If(rowCuenta.Table.Columns.Contains("SBC_BOL_VIGENTE"), Util.AtribuirValorObj(rowCuenta("SBC_BOL_VIGENTE"), GetType(Boolean)), Nothing)}

                        .Sector = New Clases.Sector With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_SECTOR"), Util.AtribuirValorObj(rowCuenta("COD_SECTOR"), GetType(String)), Nothing), _
                                                          .Identificador = If(rowCuenta.Table.Columns.Contains("OID_SECTOR"), Util.AtribuirValorObj(rowCuenta("OID_SECTOR"), GetType(String)), Nothing), _
                                                          .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_SECTOR"), Util.AtribuirValorObj(rowCuenta("DES_SECTOR"), GetType(String)), Nothing), _
                                                          .EsActivo = If(rowCuenta.Table.Columns.Contains("SE_BOL_ACTIVO"), Util.AtribuirValorObj(rowCuenta("SE_BOL_ACTIVO"), GetType(Boolean)), Nothing), _
                                                          .EsCentroProceso = If(rowCuenta.Table.Columns.Contains("BOL_CENTRO_PROCESO"), Util.AtribuirValorObj(rowCuenta("BOL_CENTRO_PROCESO"), GetType(Boolean)), Nothing), _
                                                          .EsConteo = If(rowCuenta.Table.Columns.Contains("BOL_CONTEO"), Util.AtribuirValorObj(rowCuenta("BOL_CONTEO"), GetType(Boolean)), Nothing), _
                                                          .EsTesoro = If(rowCuenta.Table.Columns.Contains("BOL_TESORO"), Util.AtribuirValorObj(rowCuenta("BOL_TESORO"), GetType(Boolean)), Nothing), _
                                                          .PemitirDisponerValor = If(rowCuenta.Table.Columns.Contains("BOL_PERMITE_DISPONER_VALOR"), Util.AtribuirValorObj(rowCuenta("BOL_PERMITE_DISPONER_VALOR"), GetType(Boolean)), Nothing), _
                                                          .Delegacion = New Clases.Delegacion With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_DELEGACION"), Util.AtribuirValorObj(rowCuenta("COD_DELEGACION"), GetType(String)), Nothing), _
                                                                                            .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_DELEGACION"), Util.AtribuirValorObj(rowCuenta("DES_DELEGACION"), GetType(String)), Nothing), _
                                                                                            .Identificador = If(rowCuenta.Table.Columns.Contains("OID_DELEGACION"), Util.AtribuirValorObj(rowCuenta("OID_DELEGACION"), GetType(String)), Nothing), _
                                                                                            .EsActivo = If(rowCuenta.Table.Columns.Contains("D_BOL_VIGENTE"), Util.AtribuirValorObj(rowCuenta("D_BOL_VIGENTE"), GetType(Boolean)), Nothing), _
                                                                                            .HusoHorarioEnMinutos = If(rowCuenta.Table.Columns.Contains("NEC_GMT_MINUTOS"), Util.AtribuirValorObj(rowCuenta("NEC_GMT_MINUTOS"), GetType(Integer)), Nothing), _
                                                                                            .FechaHoraVeranoInicio = If(rowCuenta.Table.Columns.Contains("FYH_VERANO_INICIO"), Util.AtribuirValorObj(rowCuenta("FYH_VERANO_INICIO"), GetType(Date)), Nothing), _
                                                                                            .FechaHoraVeranoFin = If(rowCuenta.Table.Columns.Contains("FYH_VERANO_FIN"), Util.AtribuirValorObj(rowCuenta("FYH_VERANO_FIN"), GetType(Date)), Nothing), _
                                                                                            .AjusteHorarioVerano = If(rowCuenta.Table.Columns.Contains("NEC_VERANO_AJUSTE"), Util.AtribuirValorObj(rowCuenta("NEC_VERANO_AJUSTE"), GetType(Integer)), Nothing), _
                                                                                            .Zona = If(rowCuenta.Table.Columns.Contains("DES_ZONA"), Util.AtribuirValorObj(rowCuenta("DES_ZONA"), GetType(String)), Nothing), _
                                                                                            .CodigoPais = If(rowCuenta.Table.Columns.Contains("COD_PAIS"), Util.AtribuirValorObj(rowCuenta("COD_PAIS"), GetType(String)), Nothing)}, _
                                                          .TipoSector = New Clases.TipoSector With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_TIPO_SECTOR"), Util.AtribuirValorObj(rowCuenta("COD_TIPO_SECTOR"), GetType(String)), Nothing), _
                                                                                            .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_TIPO_SECTOR"), Util.AtribuirValorObj(rowCuenta("DES_TIPO_SECTOR"), GetType(String)), Nothing), _
                                                                                            .Identificador = If(rowCuenta.Table.Columns.Contains("OID_TIPO_SECTOR"), Util.AtribuirValorObj(rowCuenta("OID_TIPO_SECTOR"), GetType(String)), Nothing), _
                                                                                            .EstaActivo = If(rowCuenta.Table.Columns.Contains("TSE_BOL_ACTIVO"), Util.AtribuirValorObj(rowCuenta("TSE_BOL_ACTIVO"), GetType(Boolean)), Nothing), _
                                                                                            .CaracteristicasTipoSector = caracteristicas}, _
                                                          .Planta = New Clases.Planta With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_PLANTA"), Util.AtribuirValorObj(rowCuenta("COD_PLANTA"), GetType(String)), Nothing), _
                                                                                            .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_PLANTA"), Util.AtribuirValorObj(rowCuenta("DES_PLANTA"), GetType(String)), Nothing), _
                                                                                            .Identificador = If(rowCuenta.Table.Columns.Contains("OID_PLANTA"), Util.AtribuirValorObj(rowCuenta("OID_PLANTA"), GetType(String)), Nothing), _
                                                                                            .EsActivo = If(rowCuenta.Table.Columns.Contains("P_BOL_ACTIVO"), Util.AtribuirValorObj(rowCuenta("P_BOL_ACTIVO"), GetType(Boolean)), Nothing)}}

                    End With

                    If cuenta.TipoCuenta <> Enumeradores.TipoCuenta.Ambos AndAlso cuenta.TipoCuenta <> TipoCuenta Then
                        ActualizarTipoCuenta(cuenta.Identificador, ContractoServicio.GenesisSaldos.Enumeraciones.TipoCuenta.Ambos, DescripcionUsuario)
                        cuenta.TipoCuenta = Enumeradores.TipoCuenta.Ambos
                    End If

                    cuentas.Add(cuenta)
                Next
            End If

            Return cuentas

        End Function

        Private Shared Function HacerConsultaConCuentas(ByRef query As String,
                                               Optional ByRef identificadoresCuentas As List(Of String) = Nothing) As DataTable
            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim filtro As String = ""

                If identificadoresCuentas IsNot Nothing Then
                    If identificadoresCuentas.Count = 1 Then
                        filtro &= " AND CU.OID_CUENTA = []OID_CUENTA "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA", ProsegurDbType.Descricao_Curta, identificadoresCuentas(0)))
                    ElseIf identificadoresCuentas.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresCuentas, "OID_CUENTA", cmd, "AND", "CU", , False)
                    End If
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function

        Public Shared Function RecuperarConfiguracaoNivelMovimientoSaldo(IdentificadorCliente As String,
                                                                     Optional IdentificadorSubCliente As String = Nothing,
                                                                     Optional IdentificadorPuntoServicio As String = Nothing,
                                                                     Optional IdentificadorSubCanal As String = Nothing,
                                                                     Optional SolamenteConfiguracionAnterior As Boolean = False) As Dictionary(Of String, String)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim query As New StringBuilder

            comando.CommandText = My.Resources.CuentaRecuperarConfiguracionNivelMovimiento.ToString
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "vROWNUM", ProsegurDbType.Inteiro_Curto, If(SolamenteConfiguracionAnterior, 2, 1)))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Objeto_Id, IdentificadorCliente))

            If String.IsNullOrEmpty(IdentificadorSubCliente) Then
                query.Append(" AND CNM.OID_SUBCLIENTE IS NULL ")
            Else
                query.Append(" AND CNM.OID_SUBCLIENTE = []OID_SUBCLIENTE ")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, IdentificadorSubCliente))
            End If

            If String.IsNullOrEmpty(IdentificadorPuntoServicio) Then
                query.Append(" AND CNM.OID_PTO_SERVICIO IS NULL ")
            Else
                query.Append(" AND CNM.OID_PTO_SERVICIO = []OID_PTO_SERVICIO ")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, IdentificadorPuntoServicio))
            End If

            If String.IsNullOrEmpty(IdentificadorSubCanal) Then
                query.Append(" AND CNM.OID_SUBCANAL IS NULL ")
            Else
                query.Append(" AND CNM.OID_SUBCANAL = []OID_SUBCANAL ")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCANAL", ProsegurDbType.Objeto_Id, IdentificadorSubCanal))
            End If

            comando.CommandText = comando.CommandText.Replace("####FILTROS####", query.ToString)
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

            Dim configuracionNivelSaldo As New Dictionary(Of String, String)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                configuracionNivelSaldo.Add("OID_CONFIG_NIVEL_MOVIMIENTO", dt.Rows(0)("OID_CONFIG_NIVEL_MOVIMIENTO"))
                configuracionNivelSaldo.Add("OID_CONFIG_NIVEL_SALDO", dt.Rows(0)("OID_CONFIG_NIVEL_SALDO"))
                configuracionNivelSaldo.Add("OID_CLIENTE", dt.Rows(0)("OID_CLIENTE"))

                If Not IsDBNull(dt.Rows(0)("OID_SUBCLIENTE")) AndAlso Not String.IsNullOrEmpty(dt.Rows(0)("OID_SUBCLIENTE")) Then

                    configuracionNivelSaldo.Add("OID_SUBCLIENTE", dt.Rows(0)("OID_SUBCLIENTE"))

                End If

                If Not IsDBNull(dt.Rows(0)("OID_PTO_SERVICIO")) AndAlso Not String.IsNullOrEmpty(dt.Rows(0)("OID_PTO_SERVICIO")) Then

                    configuracionNivelSaldo.Add("OID_PTO_SERVICIO", dt.Rows(0)("OID_PTO_SERVICIO"))

                End If

            End If

            Return configuracionNivelSaldo

        End Function

        Public Shared Function RecuperarConfiguracaoNivelMovimiento(IdentificadorCliente As String,
                                                                     Optional IdentificadorSubCliente As String = Nothing,
                                                                     Optional IdentificadorPuntoServicio As String = Nothing,
                                                                     Optional IdentificadorSubCanal As String = Nothing,
                                                                     Optional IdentificadorConfigNivelSaldo As String = Nothing) As List(Of String)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim query As New StringBuilder

            comando.CommandText = My.Resources.CuentaRecuperarConfiguracionNivelMovimiento1.ToString
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Objeto_Id, IdentificadorCliente))

            If String.IsNullOrEmpty(IdentificadorSubCliente) Then
                query.Append(" AND CNM.OID_SUBCLIENTE IS NULL ")
            Else
                query.Append(" AND CNM.OID_SUBCLIENTE = []OID_SUBCLIENTE ")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, IdentificadorSubCliente))
            End If

            If String.IsNullOrEmpty(IdentificadorPuntoServicio) Then
                query.Append(" AND CNM.OID_PTO_SERVICIO IS NULL ")
            Else
                query.Append(" AND CNM.OID_PTO_SERVICIO = []OID_PTO_SERVICIO ")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, IdentificadorPuntoServicio))
            End If

            If String.IsNullOrEmpty(IdentificadorSubCanal) Then
                query.Append(" AND CNM.OID_SUBCANAL IS NULL ")
            Else
                query.Append(" AND CNM.OID_SUBCANAL = []OID_SUBCANAL ")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCANAL", ProsegurDbType.Objeto_Id, IdentificadorSubCanal))
            End If

            If Not String.IsNullOrEmpty(IdentificadorConfigNivelSaldo) Then
                query.Append(" AND CNS.OID_CONFIG_NIVEL_SALDO = []OID_CONFIG_NIVEL_SALDO ")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_NIVEL_SALDO", ProsegurDbType.Objeto_Id, IdentificadorConfigNivelSaldo))
            End If

            comando.CommandText = comando.CommandText.Replace("####FILTROS####", query.ToString)
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

            Dim lstConfigNivelMov As New List(Of String)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each dr In dt.Rows
                    lstConfigNivelMov.Add(dr("OID_CONFIG_NIVEL_MOVIMIENTO").ToString())
                Next

            End If

            Return lstConfigNivelMov

        End Function

        Public Shared Function ObtenerCuentaSaldoPorCuentaMovimiento_v2(IdentificadorCuentaMovimiento As String,
                                                            Optional IdentificadorCuentaSaldos As String = "") As String

            Dim IdentificadorCuentaSaldo As String = String.Empty

            If Not String.IsNullOrEmpty(IdentificadorCuentaMovimiento) Then

                Try
                    Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                        Dim inner As String = ""
                        Dim filtro As String = ""

                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_MOVIMIENTO", ProsegurDbType.Objeto_Id, IdentificadorCuentaMovimiento))

                        If Not String.IsNullOrEmpty(IdentificadorCuentaSaldos) Then

                            filtro &= " AND NS.OID_CUENTA_SALDO = []OID_CUENTA_SALDO "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO", _
                                                                                        ProsegurDbType.Objeto_Id, IdentificadorCuentaSaldos))

                        Else

                            inner &= " INNER JOIN SAPR_TCONFIG_NIVEL_MOVIMIENTO CNM ON CNM.OID_CONFIG_NIVEL_MOVIMIENTO = NS.OID_CONFIG_NIVEL_MOVIMIENTO "
                            filtro &= " AND CNM.BOL_DEFECTO = 1 "

                        End If

                        command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.CuentaRecuperarNivelSaldo, inner, filtro))
                        command.CommandType = CommandType.Text

                        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            IdentificadorCuentaSaldo = dt.Rows(0)("OID_CUENTA_SALDO")
                        End If

                    End Using

                Catch ex As Exception
                    Throw
                Finally
                    GC.Collect()
                End Try

            End If

            Return IdentificadorCuentaSaldo

        End Function

        Public Shared Function RecuperarConfiguracaoNivelSaldo(IdentificadorCliente As String, Optional IdentificadorSubCliente As String = Nothing, Optional IdentificadorPuntoServicio As String = Nothing) As Dictionary(Of String, String)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim query As New StringBuilder

            comando.CommandText = My.Resources.CuentaRecuperarConfiguracionNivelSaldo.ToString
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Objeto_Id, IdentificadorCliente))

            If String.IsNullOrEmpty(IdentificadorSubCliente) Then
                query.Append(" AND OID_SUBCLIENTE IS NULL ")
            Else
                query.Append(" AND OID_SUBCLIENTE = []OID_SUBCLIENTE ")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, IdentificadorSubCliente))
            End If

            If String.IsNullOrEmpty(IdentificadorPuntoServicio) Then
                query.Append(" AND OID_PTO_SERVICIO IS NULL ")
            Else
                query.Append(" AND OID_PTO_SERVICIO = []OID_PTO_SERVICIO ")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, IdentificadorPuntoServicio))
            End If

            comando.CommandText = comando.CommandText.Replace("####FILTROS####", query.ToString)
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

            Dim configuracionNivelSaldo As New Dictionary(Of String, String)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                configuracionNivelSaldo.Add("OID_CONFIG_NIVEL_SALDO", dt.Rows(0)("OID_CONFIG_NIVEL_SALDO"))
                configuracionNivelSaldo.Add("OID_CLIENTE", dt.Rows(0)("OID_CLIENTE"))

                If Not IsDBNull(dt.Rows(0)("OID_SUBCLIENTE")) AndAlso Not String.IsNullOrEmpty(dt.Rows(0)("OID_SUBCLIENTE")) Then

                    configuracionNivelSaldo.Add("OID_SUBCLIENTE", dt.Rows(0)("OID_SUBCLIENTE"))

                End If

                If Not IsDBNull(dt.Rows(0)("OID_PTO_SERVICIO")) AndAlso Not String.IsNullOrEmpty(dt.Rows(0)("OID_PTO_SERVICIO")) Then

                    configuracionNivelSaldo.Add("OID_PTO_SERVICIO", dt.Rows(0)("OID_PTO_SERVICIO"))

                End If

            End If

            Return configuracionNivelSaldo

        End Function

        Public Shared Function RecuperarNivelSaldoPorConfiguracionMovimiento(IdentificadorConfiguracionMovimiento As String) As DataTable

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CuentaRecuperarNivelSaldoPorConfiguracionMovimiento)
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_NIVEL_MOVIMIENTO", ProsegurDbType.Identificador_Alfanumerico, IdentificadorConfiguracionMovimiento))

            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

        End Function

        Public Shared Function RecuperarConfiguracaoNivelSaldoPorIdentificador(IdentificadorConfiguracionNivelSaldo As String) As Dictionary(Of String, String)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim query As New StringBuilder

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CuentaRecuperarConfiguracionNivelSaldoPorIdentificador)
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_NIVEL_SALDO", ProsegurDbType.Identificador_Alfanumerico, IdentificadorConfiguracionNivelSaldo))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

            Dim configuracionNivelSaldo As New Dictionary(Of String, String)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                configuracionNivelSaldo.Add("OID_CONFIG_NIVEL_SALDO", IdentificadorConfiguracionNivelSaldo)
                configuracionNivelSaldo.Add("OID_CLIENTE", dt.Rows(0)("OID_CLIENTE"))

                If Not IsDBNull(dt.Rows(0)("OID_SUBCLIENTE")) AndAlso Not String.IsNullOrEmpty(dt.Rows(0)("OID_SUBCLIENTE")) Then

                    configuracionNivelSaldo.Add("OID_SUBCLIENTE", dt.Rows(0)("OID_SUBCLIENTE"))

                End If

                If Not IsDBNull(dt.Rows(0)("OID_PTO_SERVICIO")) AndAlso Not String.IsNullOrEmpty(dt.Rows(0)("OID_PTO_SERVICIO")) Then

                    configuracionNivelSaldo.Add("OID_PTO_SERVICIO", dt.Rows(0)("OID_PTO_SERVICIO"))

                End If

            End If

            Return configuracionNivelSaldo

        End Function

#End Region

#Region "[Obtener/Generar Cuenta]"

        Public Shared crearCuenta As New Object

        Public Shared Function ObtenerCuenta(identificadorCliente As String,
                                      identificadorSubCliente As String,
                                      identificadorPuntoServicio As String,
                                      identificadorCanal As String,
                                      identificadorSubCanal As String,
                                      identificadorDelegacion As String,
                                      identificadorPlanta As String,
                                      identificadorSector As String,
                                      tipo As Enumeradores.TipoSitio,
                                      TipoCuenta As Enumeradores.TipoCuenta,
                                      ByRef validaciones As List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError),
                                      DescripcionUsuario As String,
                                      obtenerDatosCuentas As Boolean) As Clases.Cuenta

            If validaciones Is Nothing Then validaciones = New List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError)

            Dim _validaciones As New List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError)
            _validaciones.Add(New Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL118", .descripcion = String.Format(Tradutor.Traduzir("VAL118"), tipo.ToString)})

            Dim cuenta As New Clases.Cuenta

            Try

                cuenta = ejecutarConsultaCuentaPorIdentificador(identificadorCliente,
                                                            identificadorSubCliente,
                                                            identificadorPuntoServicio,
                                                            identificadorCanal,
                                                            identificadorSubCanal,
                                                            identificadorDelegacion,
                                                            identificadorPlanta,
                                                            identificadorSector)


                If cuenta Is Nothing Then

                    Dim identificador As String = ""

                    SyncLock crearCuenta

                        cuenta = ejecutarConsultaCuentaPorIdentificador(identificadorCliente,
                                                                        identificadorSubCliente,
                                                                        identificadorPuntoServicio,
                                                                        identificadorCanal,
                                                                        identificadorSubCanal,
                                                                        identificadorDelegacion,
                                                                        identificadorPlanta,
                                                                        identificadorSector)

                        If cuenta Is Nothing Then

                            identificador = GenerarCuenta(identificadorCliente,
                                                          identificadorSubCliente,
                                                          identificadorPuntoServicio,
                                                          identificadorCanal,
                                                          identificadorSubCanal,
                                                          identificadorDelegacion,
                                                          identificadorPlanta,
                                                          identificadorSector,
                                                          DescripcionUsuario,
                                                          Enumeraciones.TipoCuenta.Movimiento)
                        End If

                    End SyncLock

                    cuenta = New Clases.Cuenta With {.Identificador = identificador}

                End If

                If obtenerDatosCuentas Then
                    cuenta = ObtenerCuentaPorIdentificador(cuenta.Identificador, TipoCuenta, DescripcionUsuario)
                End If

            Catch ex As Excepcion.NegocioExcepcion
                Throw New Excepcion.NegocioExcepcion(ex.Message)
            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return cuenta

        End Function

        Public Shared Function ObtenerCuentaPorCodigos(cuenta As Clases.Cuenta,
                                   IdentificadorAjeno As String) As Clases.Cuenta

            Dim cuentaRespuesta As Clases.Cuenta = Nothing

            Dim _usuario As String = cuenta.UsuarioModificacion
            Dim _tipoCuenta As ContractoServicio.GenesisSaldos.Enumeraciones.TipoCuenta = cuenta.TipoCuenta

            Dim _codigoCliente As String = cuenta.Cliente.Codigo
            Dim _codigoSubCliente As String = If(cuenta.SubCliente IsNot Nothing, cuenta.SubCliente.Codigo, String.Empty)
            Dim _codigoPuntoServicio As String = If(cuenta.PuntoServicio IsNot Nothing, cuenta.PuntoServicio.Codigo, String.Empty)
            Dim _codigoCanal As String = cuenta.Canal.Codigo
            Dim _codigoSubCanal As String = cuenta.SubCanal.Codigo
            Dim _codigoDelegacion As String = cuenta.Sector.Delegacion.Codigo
            Dim _codigoPlanta As String = cuenta.Sector.Planta.Codigo
            Dim _codigoSector As String = cuenta.Sector.Codigo

            Dim _identificadorCliente As String = ""
            Dim _identificadorSubCliente As String = ""
            Dim _identificadorPuntoServicio As String = ""
            Dim _identificadorCanal As String = ""
            Dim _identificadorSubCanal As String = ""
            Dim _identificadorDelegacion As String = ""
            Dim _identificadorPlanta As String = ""
            Dim _identificadorSector As String = ""
            Dim identificadorCuenta As String = String.Empty
            Try

                If String.IsNullOrEmpty(IdentificadorAjeno) Then

                    Dim objCuenta = ejecutarConsultaCuentaPorCodigos(_codigoCliente,
                                                              _codigoSubCliente,
                                                              _codigoPuntoServicio,
                                                              _codigoCanal,
                                                              _codigoSubCanal,
                                                              _codigoDelegacion,
                                                              _codigoPlanta,
                                                              _codigoSector)
                    If objCuenta IsNot Nothing Then
                        identificadorCuenta = objCuenta.Identificador
                        cuenta.Identificador = objCuenta.Identificador
                        cuenta.TipoCuenta = objCuenta.TipoCuenta
                    Else
                        cuenta = Nothing
                    End If
                Else

                    Dim identificadores As DataTable = ObtenerIdentificadorPorCodigosAjeno(IdentificadorAjeno,
                                                                                           _codigoCliente,
                                                                                           _codigoSubCliente,
                                                                                           _codigoPuntoServicio,
                                                                                           _codigoCanal,
                                                                                           _codigoSubCanal,
                                                                                           _codigoDelegacion,
                                                                                           _codigoPlanta,
                                                                                           _codigoSector)

                    If identificadores IsNot Nothing AndAlso identificadores.Rows.Count > 0 Then

                        If Not String.IsNullOrEmpty(_codigoCliente) Then
                            Dim cliente = identificadores.Select(" COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE' AND COD_AJENO = '" & _codigoCliente & "'")
                            If cliente IsNot Nothing AndAlso cliente.Count = 1 Then
                                _identificadorCliente = Util.AtribuirValorObj(cliente(0)("OID_TABLA_GENESIS"), GetType(String))
                            End If
                            If String.IsNullOrEmpty(_identificadorCliente) Then
                                Throw New Excepcion.NegocioExcepcion(Tradutor.Traduzir("msg_codigo_cliente_vazio"))
                            End If
                        End If

                        If Not String.IsNullOrEmpty(_codigoSubCliente) Then
                            Dim SubCliente = identificadores.Select(" COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE' AND COD_AJENO = '" & _codigoSubCliente & "'")
                            If SubCliente IsNot Nothing AndAlso SubCliente.Count = 1 Then
                                _identificadorSubCliente = Util.AtribuirValorObj(SubCliente(0)("OID_TABLA_GENESIS"), GetType(String))
                            End If
                            If String.IsNullOrEmpty(_identificadorSubCliente) Then
                                Throw New Excepcion.NegocioExcepcion(Tradutor.Traduzir("msg_codigo_subcliente_vazio"))
                            End If
                        End If

                        If Not String.IsNullOrEmpty(_codigoPuntoServicio) Then
                            Dim puntoServicio = identificadores.Select(" COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO' AND COD_AJENO = '" & _codigoPuntoServicio & "'")
                            If puntoServicio IsNot Nothing AndAlso puntoServicio.Count = 1 Then
                                _identificadorPuntoServicio = Util.AtribuirValorObj(puntoServicio(0)("OID_TABLA_GENESIS"), GetType(String))
                            End If
                            If String.IsNullOrEmpty(_identificadorPuntoServicio) Then
                                Throw New Excepcion.NegocioExcepcion(Tradutor.Traduzir("msg_codigo_puntoservicio_vazio"))
                            End If
                        End If

                        If Not String.IsNullOrEmpty(_codigoCanal) Then
                            Dim Canal = identificadores.Select(" COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL' AND COD_AJENO = '" & _codigoCanal & "'")
                            If Canal IsNot Nothing AndAlso Canal.Count = 1 Then
                                _identificadorCanal = Util.AtribuirValorObj(Canal(0)("OID_TABLA_GENESIS"), GetType(String))
                            End If
                            If String.IsNullOrEmpty(_identificadorCanal) Then
                                Throw New Excepcion.NegocioExcepcion(Tradutor.Traduzir("msg_codigo_canal_vazio"))
                            End If
                        End If

                        If Not String.IsNullOrEmpty(_codigoSubCanal) Then
                            Dim subCanal = identificadores.Select(" COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCANAL' AND COD_AJENO = '" & _codigoSubCanal & "'")
                            If subCanal IsNot Nothing AndAlso subCanal.Count = 1 Then
                                _identificadorSubCanal = Util.AtribuirValorObj(subCanal(0)("OID_TABLA_GENESIS"), GetType(String))
                            End If
                            If String.IsNullOrEmpty(_identificadorSubCanal) Then
                                Throw New Excepcion.NegocioExcepcion(Tradutor.Traduzir("msg_codigo_subcanal_vazio"))
                            End If
                        End If

                        If Not String.IsNullOrEmpty(_codigoDelegacion) Then
                            Dim delegacion = identificadores.Select(" COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION' AND COD_AJENO = '" & _codigoDelegacion & "'")
                            If delegacion IsNot Nothing AndAlso delegacion.Count = 1 Then
                                _identificadorDelegacion = Util.AtribuirValorObj(delegacion(0)("OID_TABLA_GENESIS"), GetType(String))
                            End If
                            If String.IsNullOrEmpty(_identificadorDelegacion) Then
                                Throw New Excepcion.NegocioExcepcion(Tradutor.Traduzir("msg_codigo_delegacion_vazio"))
                            End If
                        End If

                        If Not String.IsNullOrEmpty(_codigoPlanta) Then
                            Dim planta = identificadores.Select(" COD_TIPO_TABLA_GENESIS = 'GEPR_TPLANTA' AND COD_AJENO = '" & _codigoPlanta & "'")
                            If planta IsNot Nothing AndAlso planta.Count = 1 Then
                                _identificadorPlanta = Util.AtribuirValorObj(planta(0)("OID_TABLA_GENESIS"), GetType(String))
                            End If
                            If String.IsNullOrEmpty(_identificadorPlanta) Then
                                Throw New Excepcion.NegocioExcepcion(Tradutor.Traduzir("msg_codigo_planta_vazio"))
                            End If
                        End If

                        If Not String.IsNullOrEmpty(_codigoSector) Then
                            Dim sector = identificadores.Select(" COD_TIPO_TABLA_GENESIS = 'GEPR_TSECTOR' AND COD_AJENO = '" & _codigoSector & "'")
                            If sector IsNot Nothing AndAlso sector.Count = 1 Then
                                _identificadorSector = Util.AtribuirValorObj(sector(0)("OID_TABLA_GENESIS"), GetType(String))
                            End If
                            If String.IsNullOrEmpty(_identificadorSector) Then
                                Throw New Excepcion.NegocioExcepcion(Tradutor.Traduzir("msg_codigo_sector_vazio"))
                            End If
                        End If

                        If String.IsNullOrEmpty(_identificadorCliente) Then
                            Throw New Excepcion.NegocioExcepcion(Tradutor.Traduzir("msg_codigo_cliente_vazio"))
                        End If

                        If String.IsNullOrEmpty(_identificadorSubCanal) Then
                            Throw New Excepcion.NegocioExcepcion(Tradutor.Traduzir("msg_codigo_subcanal_vazio"))
                        End If

                        If String.IsNullOrEmpty(_identificadorSector) Then
                            Throw New Excepcion.NegocioExcepcion(Tradutor.Traduzir("msg_codigo_sector_vazio"))
                        End If

                        cuenta = ejecutarConsultaCuentaPorIdentificador(_identificadorCliente,
                                                         _identificadorSubCliente,
                                                         _identificadorPuntoServicio,
                                                         _identificadorCanal,
                                                         _identificadorSubCanal,
                                                         _identificadorDelegacion,
                                                         _identificadorPlanta,
                                                         _identificadorSector)


                    End If
                End If

                If cuenta Is Nothing OrElse String.IsNullOrEmpty(cuenta.Identificador) Then

                    SyncLock crearCuenta

                        If String.IsNullOrEmpty(IdentificadorAjeno) Then

                            Dim objCuenta = ejecutarConsultaCuentaPorCodigos(_codigoCliente,
                                                         _codigoSubCliente,
                                                         _codigoPuntoServicio,
                                                         _codigoCanal,
                                                         _codigoSubCanal,
                                                         _codigoDelegacion,
                                                         _codigoPlanta,
                                                         _codigoSector)

                            If objCuenta Is Nothing Then
                                identificadorCuenta = GenerarCuentaPorCodigo(IdentificadorAjeno,
                                                                       _codigoCliente,
                                                                       _codigoSubCliente,
                                                                       _codigoPuntoServicio,
                                                                       _codigoCanal,
                                                                       _codigoSubCanal,
                                                                       _codigoDelegacion,
                                                                       _codigoPlanta,
                                                                       _codigoSector,
                                                                       _usuario,
                                                                       _tipoCuenta)
                            Else
                                identificadorCuenta = objCuenta.Identificador
                                cuenta.Identificador = objCuenta.Identificador
                                cuenta.TipoCuenta = objCuenta.TipoCuenta
                            End If
                        Else

                            cuenta = ejecutarConsultaCuentaPorIdentificador(_identificadorCliente,
                                                         _identificadorSubCliente,
                                                         _identificadorPuntoServicio,
                                                         _identificadorCanal,
                                                         _identificadorSubCanal,
                                                         _identificadorDelegacion,
                                                         _identificadorPlanta,
                                                         _identificadorSector)

                            If cuenta Is Nothing Then
                                identificadorCuenta = GenerarCuenta(_identificadorCliente,
                                                              _identificadorSubCliente,
                                                              _identificadorPuntoServicio,
                                                              _identificadorCanal,
                                                              _identificadorSubCanal,
                                                              _identificadorDelegacion,
                                                              _identificadorPlanta,
                                                              _identificadorSector,
                                                              _usuario,
                                                              _tipoCuenta)
                            End If

                        End If

                    End SyncLock
                Else
                    cuentaRespuesta = ObtenerCuentaPorIdentificador(cuenta.Identificador, _tipoCuenta, _usuario)
                End If

                If Not String.IsNullOrWhiteSpace(identificadorCuenta) Then
                    cuentaRespuesta = ObtenerCuentaPorIdentificador(identificadorCuenta, _tipoCuenta, _usuario)
                End If
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return cuentaRespuesta
        End Function

        Private Shared Function ejecutarConsultaCuentaPorCodigos(codigoCliente As String,
                                                         codigoSubCliente As String,
                                                         codigoPuntoServicio As String,
                                                         codigoCanal As String,
                                                         codigoSubCanal As String,
                                                         codigoDelegacion As String,
                                                         codigoPlanta As String,
                                                         codigoSector As String) As Clases.Cuenta

            Dim cuenta As New Clases.Cuenta
            Dim dt As DataTable = Nothing

            Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim objQuery As New StringBuilder

                ' === São obrigatorios para ter uma CUENTA ====
                '   Sector
                objQuery.AppendLine(" AND SE.COD_SECTOR = []COD_SECTOR AND SE.BOL_ACTIVO = 1 ")
                command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR", ProsegurDbType.Identificador_Alfanumerico, codigoSector))
                '   Cliente
                objQuery.AppendLine(" AND CL.COD_CLIENTE = []COD_CLIENTE AND CL.BOL_VIGENTE = 1 ")
                command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, codigoCliente))
                '   SubCanal
                objQuery.AppendLine(" AND SBC.COD_SUBCANAL = []COD_SUBCANAL AND SBC.BOL_VIGENTE = 1 ")
                command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, codigoSubCanal))

                ' === Valores opcionais ====
                '   Sub-Cliente
                If Not String.IsNullOrEmpty(codigoSubCliente) Then
                    objQuery.AppendLine(" AND SCL.COD_SUBCLIENTE = []COD_SUBCLIENTE AND SCL.BOL_VIGENTE = 1 ")
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, codigoSubCliente))
                Else
                    objQuery.AppendLine(" AND CU.OID_SUBCLIENTE IS NULL")
                End If
                '   Punto Servicio
                If Not String.IsNullOrEmpty(codigoPuntoServicio) Then
                    objQuery.AppendLine(" AND PTO.COD_PTO_SERVICIO = []COD_PTO_SERVICIO AND PTO.BOL_VIGENTE = 1 ")
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, codigoPuntoServicio))
                Else
                    objQuery.AppendLine(" AND CU.OID_PTO_SERVICIO IS NULL")
                End If
                'Filtro Planta
                If Not String.IsNullOrEmpty(codigoPlanta) Then
                    objQuery.AppendLine(" AND P.COD_PLANTA = []COD_PLANTA AND P.BOL_ACTIVO = 1 ")
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Identificador_Alfanumerico, codigoPlanta))
                End If
                'Filtro Delegacion
                If Not String.IsNullOrEmpty(codigoDelegacion) Then
                    objQuery.AppendLine(" AND D.COD_DELEGACION = []COD_DELEGACION AND D.BOL_VIGENTE = 1 ")
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))
                End If
                'Filtro Canal
                If Not String.IsNullOrEmpty(codigoCanal) Then
                    objQuery.AppendLine(" AND CAN.COD_CANAL = []COD_CANAL AND CAN.BOL_VIGENTE = 1 ")
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CANAL", ProsegurDbType.Identificador_Alfanumerico, codigoCanal))
                End If

                command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.Cuente_ObtenerIdentificaroCuentaPorCodigos & objQuery.ToString)
                command.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

            End Using

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                With cuenta
                    .Identificador = If(dt.Rows(0).Table.Columns.Contains("OID_CUENTA"), Util.AtribuirValorObj(dt.Rows(0)("OID_CUENTA"), GetType(String)), Nothing)
                    If dt.Rows(0).Table.Columns.Contains("COD_TIPO_CUENTA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(dt.Rows(0)("COD_TIPO_CUENTA"), GetType(String))) Then
                        Select Case Util.AtribuirValorObj(dt.Rows(0)("COD_TIPO_CUENTA"), GetType(String))
                            Case "A"
                                .TipoCuenta = Enumeradores.TipoCuenta.Ambos
                            Case "M"
                                .TipoCuenta = Enumeradores.TipoCuenta.Movimiento
                            Case "S"
                                .TipoCuenta = Enumeradores.TipoCuenta.Saldo
                        End Select
                    End If
                End With

            Else
                cuenta = Nothing
            End If

            Return cuenta

        End Function

        Public Shared Function ejecutarConsultaCuentaPorIdentificador(identificadorCliente As String,
                                                         identificadorSubCliente As String,
                                                         identificadorPuntoServicio As String,
                                                         identificadorCanal As String,
                                                         identificadorSubCanal As String,
                                                         identificadorDelegacion As String,
                                                         identificadorPlanta As String,
                                                         identificadorSector As String) As Clases.Cuenta

            Dim cuenta As New Clases.Cuenta
            Dim dt As DataTable = Nothing

            Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim objQuery As New StringBuilder

                ' === São obrigatorios para ter uma CUENTA ====
                '   Sector
                objQuery.AppendLine(" AND SE.OID_SECTOR = []OID_SECTOR AND SE.BOL_ACTIVO = 1 ")
                command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Identificador_Alfanumerico, identificadorSector))
                '   Cliente
                objQuery.AppendLine(" AND CL.OID_CLIENTE = []OID_CLIENTE AND CL.BOL_VIGENTE = 1 ")
                command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, identificadorCliente))
                '   SubCanal
                objQuery.AppendLine(" AND SBC.OID_SUBCANAL = []OID_SUBCANAL AND SBC.BOL_VIGENTE = 1 ")
                command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, identificadorSubCanal))

                ' === Valores opcionais ====
                '   Sub-Cliente
                If Not String.IsNullOrEmpty(identificadorSubCliente) Then
                    objQuery.AppendLine(" AND SCL.OID_SUBCLIENTE = []OID_SUBCLIENTE AND SCL.BOL_VIGENTE = 1 ")
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, identificadorSubCliente))
                Else
                    objQuery.AppendLine(" AND CU.OID_SUBCLIENTE IS NULL")
                End If
                '   Punto Servicio
                If Not String.IsNullOrEmpty(identificadorPuntoServicio) Then
                    objQuery.AppendLine(" AND PTO.OID_PTO_SERVICIO = []OID_PTO_SERVICIO AND PTO.BOL_VIGENTE = 1 ")
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, identificadorPuntoServicio))
                Else
                    objQuery.AppendLine(" AND CU.OID_PTO_SERVICIO IS NULL")
                End If
                'Filtro Planta
                If Not String.IsNullOrEmpty(identificadorPlanta) Then
                    objQuery.AppendLine(" AND P.OID_PLANTA = []OID_PLANTA AND P.BOL_ACTIVO = 1 ")
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PLANTA", ProsegurDbType.Identificador_Alfanumerico, identificadorPlanta))
                End If
                'Filtro Delegacion
                If Not String.IsNullOrEmpty(identificadorDelegacion) Then
                    objQuery.AppendLine(" AND D.OID_DELEGACION = []OID_DELEGACION AND D.BOL_VIGENTE = 1 ")
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, identificadorDelegacion))
                End If
                'Filtro Canal
                If Not String.IsNullOrEmpty(identificadorCanal) Then
                    objQuery.AppendLine(" AND CAN.OID_CANAL = []OID_CANAL AND CAN.BOL_VIGENTE = 1 ")
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CANAL", ProsegurDbType.Identificador_Alfanumerico, identificadorCanal))
                End If

                command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.Cuente_ObtenerIdentificaroCuentaPorCodigos & objQuery.ToString)
                command.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

            End Using

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                With cuenta
                    .Identificador = If(dt.Rows(0).Table.Columns.Contains("OID_CUENTA"), Util.AtribuirValorObj(dt.Rows(0)("OID_CUENTA"), GetType(String)), Nothing)
                    If dt.Rows(0).Table.Columns.Contains("COD_TIPO_CUENTA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(dt.Rows(0)("COD_TIPO_CUENTA"), GetType(String))) Then
                        Select Case Util.AtribuirValorObj(dt.Rows(0)("COD_TIPO_CUENTA"), GetType(String))
                            Case "A"
                                .TipoCuenta = Enumeradores.TipoCuenta.Ambos
                            Case "M"
                                .TipoCuenta = Enumeradores.TipoCuenta.Movimiento
                            Case "S"
                                .TipoCuenta = Enumeradores.TipoCuenta.Saldo
                        End Select
                    End If
                End With

            Else
                cuenta = Nothing
            End If

            Return cuenta
        End Function

        Shared Function ObtenerIdentificadorPorCodigosAjeno(codigoAjeno As String,
                                                            codigoCliente As String,
                                                            codigoSubCliente As String,
                                                            codigoPuntoServicio As String,
                                                            codigoCanal As String,
                                                            codigoSubCanal As String,
                                                            codigoDelegacion As String,
                                                            codigoPlanta As String,
                                                            codigoSector As String) As DataTable

            Dim dt As DataTable = Nothing

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim codigos As New List(Of String)
                    Dim filtro As String = ""


                    If Not String.IsNullOrEmpty(codigoCliente) Then
                        codigos.Add(codigoCliente)
                    End If
                    If Not String.IsNullOrEmpty(codigoSubCliente) Then
                        codigos.Add(codigoSubCliente)
                    End If
                    If Not String.IsNullOrEmpty(codigoPuntoServicio) Then
                        codigos.Add(codigoPuntoServicio)
                    End If
                    If Not String.IsNullOrEmpty(codigoCanal) Then
                        codigos.Add(codigoCanal)
                    End If
                    If Not String.IsNullOrEmpty(codigoSubCanal) Then
                        codigos.Add(codigoSubCanal)
                    End If
                    If Not String.IsNullOrEmpty(codigoDelegacion) Then
                        codigos.Add(codigoDelegacion)
                    End If
                    If Not String.IsNullOrEmpty(codigoPlanta) Then
                        codigos.Add(codigoPlanta)
                    End If
                    If Not String.IsNullOrEmpty(codigoSector) Then
                        codigos.Add(codigoSector)
                    End If

                    filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigos, "COD_AJENO", command, "AND", "CA")
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDENTIFICADOR", ProsegurDbType.Observacao_Longa, codigoAjeno))

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.Cuente_ObtenerIdentificaroPorCodigosAjeno, filtro))
                    command.CommandType = CommandType.Text

                    dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return dt

        End Function

#End Region

#Region "[Inserir]"

        Private Shared Function GenerarCuentaPorCodigo(IdentificadorAjeno As String,
                                                      codigoCliente As String,
                                                      codigoSubCliente As String,
                                                      codigoPuntoServicio As String,
                                                      codigoCanal As String,
                                                      codigoSubCanal As String,
                                                      codigoDelegacion As String,
                                                      codigoPlanta As String,
                                                      codigoSector As String, _
                                                      DescripcionUsuario As String,
                                                      TipoCuenta As ContractoServicio.GenesisSaldos.Enumeraciones.TipoCuenta) As String

            Dim _Validaciones As New List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError)

            Dim _identificadorCliente As String = ""
            Dim _identificadorSubCliente As String = ""
            Dim _identificadorPuntoServicio As String = ""
            Dim _identificadorCanal As String = ""
            Dim _identificadorSubCanal As String = ""
            Dim _identificadorDelegacion As String = ""
            Dim _identificadorPlanta As String = ""
            Dim _identificadorSector As String = ""

            ValidarCuenta(IdentificadorAjeno,
                          codigoCliente,
                          codigoSubCliente,
                          codigoPuntoServicio,
                          codigoCanal,
                          codigoSubCanal,
                          codigoDelegacion,
                          codigoPlanta,
                          codigoSector,
                          Enumeradores.TipoSitio.NoDefinido,
                          _identificadorCliente,
                          _identificadorSubCliente,
                          _identificadorPuntoServicio,
                          _identificadorCanal,
                          _identificadorSubCanal,
                          _identificadorDelegacion,
                          _identificadorPlanta,
                          _identificadorSector,
                          _Validaciones)

            If _Validaciones IsNot Nothing AndAlso _Validaciones.Count > 1 Then
                Throw New Excepcion.NegocioExcepcion(String.Join(vbCrLf, _Validaciones.Select(Function(f) f.descripcion).ToArray))
            End If

            Return GenerarCuenta(_identificadorCliente,
                                 _identificadorSubCliente,
                                 _identificadorPuntoServicio,
                                 _identificadorCanal,
                                 _identificadorSubCanal,
                                 _identificadorDelegacion,
                                 _identificadorPlanta,
                                 _identificadorSector,
                                 DescripcionUsuario,
                                 TipoCuenta)

        End Function

        Private Shared Function GenerarCuenta(IdentificadorCliente As String,
                                             IdentificadorSubCliente As String,
                                             IdentificadorPuntoServicio As String,
                                             IdentificadorCanal As String,
                                             IdentificadorSubCanal As String,
                                             IdentificadorDelegacion As String,
                                             IdentificadorPlanta As String,
                                             IdentificadorSector As String,
                                             DescripcionUsuario As String,
                                             TipoCuenta As ContractoServicio.GenesisSaldos.Enumeraciones.TipoCuenta) As String

            Dim _Validaciones As New List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError)

            VerificaIntegridad(IdentificadorCliente,
                               IdentificadorSubCliente,
                               IdentificadorPuntoServicio,
                               IdentificadorCanal,
                               IdentificadorSubCanal,
                               IdentificadorDelegacion,
                               IdentificadorPlanta,
                               IdentificadorSector,
                               _Validaciones)

            If _Validaciones IsNot Nothing AndAlso _Validaciones.Count > 1 Then
                Throw New Excepcion.NegocioExcepcion(String.Join(vbCrLf, _Validaciones.Select(Function(f) f.descripcion).ToArray))
            End If

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CuentaGenerarCuenta)
            comando.CommandType = CommandType.Text

            Dim IdentificadorCuenta As String = Guid.NewGuid.ToString

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA", ProsegurDbType.Objeto_Id, IdentificadorCuenta))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Objeto_Id, IdentificadorCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCANAL", ProsegurDbType.Objeto_Id, IdentificadorSubCanal))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Objeto_Id, IdentificadorSector))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, Util.RetornaValorOuDbNull(IdentificadorSubCliente)))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, Util.RetornaValorOuDbNull(IdentificadorPuntoServicio)))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_CUENTA", ProsegurDbType.Descricao_Curta, If(TipoCuenta = ContractoServicio.GenesisSaldos.Enumeraciones.TipoCuenta.Ambos, "A", If(TipoCuenta = ContractoServicio.GenesisSaldos.Enumeraciones.TipoCuenta.Saldo, "S", "M"))))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, DescripcionUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, DescripcionUsuario))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, comando)

            Return IdentificadorCuenta

        End Function

        Public Shared Function GenerarConfiguracionNivelSaldo(IdentificadorCliente As String,
                                                              DescripcionUsuario As String,
                                                     Optional IdentificadorSubCliente As String = Nothing,
                                                     Optional IdentificadorPuntoServicio As String = Nothing,
                                                     Optional ByRef Transacion As DbHelper.Transacao = Nothing) As String

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CuentaGenerarConfiguracionNivelSaldo)
            comando.CommandType = CommandType.Text

            Dim OIDConfiguracionNivelSaldo As String = Guid.NewGuid.ToString

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_NIVEL_SALDO", ProsegurDbType.Objeto_Id, OIDConfiguracionNivelSaldo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Objeto_Id, IdentificadorCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, Util.RetornaValorOuDbNull(IdentificadorSubCliente)))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, Util.RetornaValorOuDbNull(IdentificadorPuntoServicio)))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, DescripcionUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, DescripcionUsuario))

            If Transacion IsNot Nothing Then
                Transacion.AdicionarItemTransacao(comando)
            Else
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, comando)
            End If

            ActualizarClienteTotalizadorSaldo(IdentificadorCliente, IdentificadorSubCliente, IdentificadorPuntoServicio, Transacion)

            Return OIDConfiguracionNivelSaldo

        End Function

        Public Shared Function GenerarConfiguracionNivelMovimiento(IdentificadorCliente As String,
                                                                   IdentificadorConfiguracionNivelSaldo As String,
                                                                   DescripcionUsuario As String,
                                                          Optional IdentificadorSubCliente As String = Nothing,
                                                          Optional IdentificadorPuntoServicio As String = Nothing,
                                                          Optional IdentificadorSubCanal As String = Nothing,
                                                          Optional ByRef Transacion As DbHelper.Transacao = Nothing,
                                                          Optional bolDefecto As Boolean = False) As String

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CuentaGenerarConfiguracionNivelMovimiento)
            comando.CommandType = CommandType.Text

            Dim OIDConfiguracionNivelMovimiento As String = Guid.NewGuid.ToString

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_NIVEL_MOVIMIENTO", ProsegurDbType.Objeto_Id, OIDConfiguracionNivelMovimiento))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_NIVEL_SALDO", ProsegurDbType.Objeto_Id, IdentificadorConfiguracionNivelSaldo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Objeto_Id, IdentificadorCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, Util.RetornaValorOuDbNull(IdentificadorSubCliente)))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, Util.RetornaValorOuDbNull(IdentificadorPuntoServicio)))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCANAL", ProsegurDbType.Objeto_Id, Util.RetornaValorOuDbNull(IdentificadorSubCanal)))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_ACTIVO", ProsegurDbType.Logico, 1))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, DescripcionUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, DescripcionUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_DEFECTO", ProsegurDbType.Logico, Convert.ToInt32(bolDefecto)))

            If Transacion IsNot Nothing Then
                Transacion.AdicionarItemTransacao(comando)
            Else
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, comando)
            End If

            Return OIDConfiguracionNivelMovimiento

        End Function

        Public Shared Function GenerarNivelSaldo(IdentificadorCuentaMovimiento As String,
                                         IdentificadorCuentaSaldo As String,
                                         IdentificadorConfiguracionNivelSaldo As String,
                                         DescripcionUsuario As String,
                                Optional ByRef Transacion As DbHelper.Transacao = Nothing) As String

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CuentaGenerarNivelSaldo)
            comando.CommandType = CommandType.Text

            Dim OIDNivelSaldo As String = Guid.NewGuid.ToString

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_NIVEL_SALDO", ProsegurDbType.Objeto_Id, OIDNivelSaldo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_MOVIMIENTO", ProsegurDbType.Objeto_Id, IdentificadorCuentaMovimiento))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO", ProsegurDbType.Objeto_Id, IdentificadorCuentaSaldo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_NIVEL_MOVIMIENTO", ProsegurDbType.Objeto_Id, IdentificadorConfiguracionNivelSaldo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, DescripcionUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, DescripcionUsuario))

            If Transacion IsNot Nothing Then
                Transacion.AdicionarItemTransacao(comando)
            Else
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, comando)
            End If

            Return OIDNivelSaldo

        End Function

#End Region

#Region "[Actualizaciones]"

        Public Shared Sub DesactivarConfiguracionNivelMovimiento(IdentificadorConfiguracionNivelMovimiento As String,
                                                                 DescripcionUsuario As String,
                                                                 Optional ByRef Transacion As DbHelper.Transacao = Nothing)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CuentaDesactivarConfiguracionNivelMovimiento)
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, DescripcionUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_NIVEL_MOVIMIENTO", ProsegurDbType.Objeto_Id, IdentificadorConfiguracionNivelMovimiento))

            If Transacion IsNot Nothing Then
                Transacion.AdicionarItemTransacao(comando)
            Else
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, comando)
            End If

        End Sub

        Public Shared Sub DeletarNivelSaldo(IdentificadorConfiguracionNivelMovimiento As String,
                                            Optional ByRef Transacion As DbHelper.Transacao = Nothing)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CuentaReciclarAtajosNivelSaldo)
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_NIVEL_MOVIMIENTO", ProsegurDbType.Objeto_Id, IdentificadorConfiguracionNivelMovimiento))

            If Transacion IsNot Nothing Then
                Transacion.AdicionarItemTransacao(comando)
            Else
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, comando)
            End If

        End Sub

        Public Shared Sub ActualizarNivelSaldo(IdentificadorNivelSaldo As String,
                                               IdentificadorCuentaMovimiento As String,
                                               IdentificadorCuentaSaldo As String,
                                               DescripcionUsuario As String,
                                      Optional ByRef Transacion As DbHelper.Transacao = Nothing)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CuentaActualizarNivelSaldo)
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_NIVEL_SALDO", ProsegurDbType.Objeto_Id, IdentificadorNivelSaldo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_MOVIMIENTO", ProsegurDbType.Objeto_Id, IdentificadorCuentaMovimiento))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO", ProsegurDbType.Objeto_Id, IdentificadorCuentaSaldo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, DescripcionUsuario))

            If Transacion IsNot Nothing Then
                Transacion.AdicionarItemTransacao(comando)
            Else
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, comando)
            End If

        End Sub

        Public Shared Sub ActualizarConfiguracionNivelMovimiento(IdentificadorConfiguracionNivelMovimiento As String,
                                                                 RegistroActivo As Boolean,
                                                                 DescripcionUsuario As String,
                                                        Optional ByRef Transacion As DbHelper.Transacao = Nothing)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CuentaActualizarConfiguracionNivelMovimiento)
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_ACTIVO", ProsegurDbType.Logico, RegistroActivo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, DescripcionUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_NIVEL_MOVIMIENTO", ProsegurDbType.Objeto_Id, IdentificadorConfiguracionNivelMovimiento))

            If Transacion IsNot Nothing Then
                Transacion.AdicionarItemTransacao(comando)
            Else
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, comando)
            End If

        End Sub

        Public Shared Sub ActualizarConfiguracionNivelMovimientoDefecto(IdentificadorConfiguracionNivelMovimiento As String,
                                                                 bolDefecto As Boolean,
                                                                 DescripcionUsuario As String,
                                                        Optional ByRef Transacion As DbHelper.Transacao = Nothing)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CuentaActualizarConfiguracionNivelMovimientoDefecto)
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_DEFECTO", ProsegurDbType.Logico, bolDefecto))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, DescripcionUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_NIVEL_MOVIMIENTO", ProsegurDbType.Objeto_Id, IdentificadorConfiguracionNivelMovimiento))

            If Transacion IsNot Nothing Then
                Transacion.AdicionarItemTransacao(comando)
            Else
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, comando)
            End If

        End Sub

        Private Shared Sub ActualizarClienteTotalizadorSaldo(IdentificadorCliente As String, _
                                                   Optional IdentificadorSubCliente As String = Nothing, _
                                                   Optional IdentificadorPuntoServicio As String = Nothing, _
                                                   Optional ByRef Transacion As DbHelper.Transacao = Nothing)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            comando.CommandType = CommandType.Text

            ' nivel de cliente
            If String.IsNullOrEmpty(IdentificadorPuntoServicio) AndAlso String.IsNullOrEmpty(IdentificadorSubCliente) Then
                comando.CommandText = String.Format(My.Resources.ActualizarClienteTotalizadorSaldo, "GEPR_TCLIENTE", "t.OID_CLIENTE").ToString
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE_TOTALIZADOR_SALDO", ProsegurDbType.Objeto_Id, IdentificadorCliente))

                ' nivel de subcliente
            ElseIf String.IsNullOrEmpty(IdentificadorPuntoServicio) AndAlso Not String.IsNullOrEmpty(IdentificadorSubCliente) Then
                comando.CommandText = String.Format(My.Resources.ActualizarClienteTotalizadorSaldo, "GEPR_TSUBCLIENTE", "t.OID_SUBCLIENTE").ToString
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE_TOTALIZADOR_SALDO", ProsegurDbType.Objeto_Id, IdentificadorSubCliente))

                ' nivel de puntoservicio
            ElseIf Not String.IsNullOrEmpty(IdentificadorPuntoServicio) AndAlso Not String.IsNullOrEmpty(IdentificadorSubCliente) Then
                comando.CommandText = String.Format(My.Resources.ActualizarClienteTotalizadorSaldo, "GEPR_TPUNTO_SERVICIO", "t.OID_PTO_SERVICIO").ToString
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE_TOTALIZADOR_SALDO", ProsegurDbType.Objeto_Id, IdentificadorPuntoServicio))

            End If
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)
            If Transacion IsNot Nothing Then
                Transacion.AdicionarItemTransacao(comando)
            Else
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, comando)
            End If

        End Sub

        Public Shared Sub ActualizarTipoCuenta(IdentificadorCuenta As String,
                                               TipoCuenta As ContractoServicio.GenesisSaldos.Enumeraciones.TipoCuenta,
                                               DescripcionUsuario As String)

            Try

                If String.IsNullOrEmpty(IdentificadorCuenta) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Tradutor.Traduzir("msg_campo_obrigatorio"), "IdentificadorCuenta"))
                End If

                If String.IsNullOrEmpty(DescripcionUsuario) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Tradutor.Traduzir("msg_campo_obrigatorio"), "DescripcionUsuario"))
                End If

                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_CUENTA", ProsegurDbType.Descricao_Curta, If(TipoCuenta = ContractoServicio.GenesisSaldos.Enumeraciones.TipoCuenta.Ambos, "A", If(TipoCuenta = ContractoServicio.GenesisSaldos.Enumeraciones.TipoCuenta.Saldo, "S", "M"))))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, DescripcionUsuario))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA", ProsegurDbType.Objeto_Id, IdentificadorCuenta))

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CuentaActualizarTipoCuenta)
                    command.CommandType = CommandType.Text

                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, command)

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

        End Sub

#End Region

#Region "[Validaciones]"

        Shared Sub ValidarCuenta(IdentificadorAjeno As String,
                                 codigoCliente As String,
                                 codigoSubCliente As String,
                                 codigoPuntoServicio As String,
                                 codigoCanal As String,
                                 codigoSubCanal As String,
                                 codigoDelegacion As String,
                                 codigoPlanta As String,
                                 codigoSector As String,
                                 tipo As Enumeradores.TipoSitio,
                                 ByRef _identificadorCliente As String,
                                 ByRef _identificadorSubCliente As String,
                                 ByRef _identificadorPuntoServicio As String,
                                 ByRef _identificadorCanal As String,
                                 ByRef _identificadorSubCanal As String,
                                 ByRef _identificadorDelegacion As String,
                                 ByRef _identificadorPlanta As String,
                                 ByRef _identificadorSector As String,
                                 ByRef validaciones As List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError))

            _identificadorCliente = ""
            _identificadorSubCliente = ""
            _identificadorPuntoServicio = ""
            _identificadorCanal = ""
            _identificadorSubCanal = ""
            _identificadorDelegacion = ""
            _identificadorPlanta = ""
            _identificadorSector = ""

            If validaciones Is Nothing Then validaciones = New List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError)

            Dim _validaciones As New List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError)
            _validaciones.Add(New Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL118", .descripcion = String.Format(Tradutor.Traduzir("VAL118"), tipo.ToString)})

            Dim msgAjeno As String = ""
            If Not String.IsNullOrEmpty(IdentificadorAjeno) Then
                msgAjeno = String.Format(Tradutor.Traduzir("msg_codigo_ajeno"), IdentificadorAjeno)
            End If

            ' Cliente
            If String.IsNullOrEmpty(codigoCliente) Then
                _validaciones.Add(New Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL112", .descripcion = Tradutor.Traduzir("msg_codigo_cliente_vazio")})
            Else
                _identificadorCliente = AccesoDatos.Genesis.Cliente.Validar(codigoCliente, IdentificadorAjeno)
                If String.IsNullOrEmpty(_identificadorCliente) Then
                    _validaciones.Add(New Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL112", .descripcion = String.Format(Tradutor.Traduzir("msg_cliente_vazio"), codigoCliente) & msgAjeno})
                End If
            End If

            ' SubClientes
            If Not String.IsNullOrEmpty(codigoSubCliente) Then
                _identificadorSubCliente = AccesoDatos.Genesis.SubCliente.Validar(_identificadorCliente, codigoSubCliente, IdentificadorAjeno)
                If String.IsNullOrEmpty(_identificadorSubCliente) Then
                    _validaciones.Add(New Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL112", .descripcion = String.Format(Tradutor.Traduzir("msg_subcliente_vazio"), codigoSubCliente) & msgAjeno})
                End If
            End If

            ' PtoServicio
            If Not String.IsNullOrEmpty(codigoPuntoServicio) Then
                _identificadorPuntoServicio = AccesoDatos.Genesis.PuntoServicio.Validar(_identificadorSubCliente, codigoPuntoServicio, IdentificadorAjeno)
                If String.IsNullOrEmpty(_identificadorPuntoServicio) Then
                    _validaciones.Add(New Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL112", .descripcion = String.Format(Tradutor.Traduzir("msg_ptoServicio_vazio"), codigoPuntoServicio) & msgAjeno})
                End If
            End If

            ' Canal
            If Not String.IsNullOrEmpty(codigoCanal) Then
                _identificadorCanal = AccesoDatos.Genesis.Canal.Validar(codigoCanal, IdentificadorAjeno)
                If String.IsNullOrEmpty(_identificadorCanal) Then
                    _validaciones.Add(New Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL112", .descripcion = String.Format(Tradutor.Traduzir("msg_canal_vazio"), codigoCanal) & msgAjeno})
                End If
            End If

            ' SubCanal
            If String.IsNullOrEmpty(codigoSubCanal) Then
                _validaciones.Add(New Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL112", .descripcion = Tradutor.Traduzir("msg_codigo_subcanal_vazio")})
            Else
                _identificadorSubCanal = AccesoDatos.Genesis.SubCanal.Validar(_identificadorCanal, codigoSubCanal, IdentificadorAjeno)
                If String.IsNullOrEmpty(_identificadorSubCanal) Then
                    _validaciones.Add(New Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL112", .descripcion = String.Format(Tradutor.Traduzir("msg_subcanal_vazio"), codigoSubCanal) & msgAjeno})
                End If
            End If

            ' Delegacion
            If Not String.IsNullOrEmpty(codigoDelegacion) Then
                _identificadorDelegacion = AccesoDatos.Genesis.Delegacion.Validar(codigoDelegacion, IdentificadorAjeno)
                If String.IsNullOrEmpty(_identificadorDelegacion) Then
                    _validaciones.Add(New Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL112", .descripcion = String.Format(Tradutor.Traduzir("msg_delegacion_vazio"), codigoDelegacion) & msgAjeno})
                End If
            End If

            ' Planta
            If Not String.IsNullOrEmpty(codigoPlanta) Then
                _identificadorPlanta = AccesoDatos.Genesis.Planta.Validar(_identificadorDelegacion, codigoPlanta, IdentificadorAjeno)
                If String.IsNullOrEmpty(_identificadorPlanta) Then
                    _validaciones.Add(New Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL112", .descripcion = String.Format(Tradutor.Traduzir("msg_planta_vazio"), codigoPlanta) & msgAjeno})
                End If
            End If

            ' Sector
            If String.IsNullOrEmpty(codigoSector) Then
                _validaciones.Add(New Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL112", .descripcion = Tradutor.Traduzir("msg_codigo_sector_vazio")})
            Else
                _identificadorSector = AccesoDatos.Genesis.Sector.Validar(_identificadorDelegacion, _identificadorPlanta, codigoSector, IdentificadorAjeno)
                If String.IsNullOrEmpty(_identificadorSector) Then
                    _validaciones.Add(New Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL112", .descripcion = String.Format(Tradutor.Traduzir("msg_sector_vazio"), codigoSector) & msgAjeno})
                End If
            End If

            If _validaciones.Count = 1 Then
                VerificaIntegridad(_identificadorCliente,
                                                                    _identificadorSubCliente,
                                                                    _identificadorPuntoServicio,
                                                                    _identificadorCanal,
                                                                    _identificadorSubCanal,
                                                                    _identificadorDelegacion,
                                                                    _identificadorPlanta,
                                                                    _identificadorSector,
                                                                    _validaciones)
            End If

            If _validaciones.Count > 1 Then
                validaciones.AddRange(_validaciones)
            End If

        End Sub

        Public Shared Sub VerificaIntegridad(identificadorCliente As String,
                                             identificadorSubCliente As String,
                                             identificadorPuntoServicio As String,
                                             identificadorCanal As String,
                                             identificadorSubCanal As String,
                                             identificadorDelegacion As String,
                                             identificadorPlanta As String,
                                             identificadorSector As String,
                                             ByRef validaciones As List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError))

            If validaciones Is Nothing Then validaciones = New List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError)

            ' Cliente/SubCliente/PuntoServicio
            If Not String.IsNullOrEmpty(identificadorCliente) Then

                Try
                    Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                        command.CommandText = My.Resources.VerificaIntegridadIdentificadoresClienteSubClientePuntoServicio
                        command.CommandType = CommandType.Text

                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Objeto_Id, identificadorCliente))

                        If Not String.IsNullOrEmpty(identificadorSubCliente) Then
                            command.CommandText &= " AND SUCL.OID_SUBCLIENTE = []OID_SUBCLIENTE AND SUCL.BOL_VIGENTE = 1 "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, identificadorSubCliente))
                        End If

                        If Not String.IsNullOrEmpty(identificadorPuntoServicio) Then
                            command.CommandText &= " AND PUSE.OID_PTO_SERVICIO = []OID_PTO_SERVICIO AND PUSE.BOL_VIGENTE = 1 "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, identificadorPuntoServicio))
                        End If

                        command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, command.CommandText)

                        If AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, command) = 0 Then
                            validaciones.Add(New Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL112", .descripcion = Tradutor.Traduzir("msg_relacion_cliente_subcliente_puntoservicio_invalida")})
                        End If

                    End Using

                Catch ex As Exception
                    Throw
                Finally
                    GC.Collect()
                End Try

            End If

            ' Canal/SubCanal
            If Not String.IsNullOrEmpty(identificadorCanal) AndAlso Not String.IsNullOrEmpty(identificadorSubCanal) Then

                Try
                    Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                        command.CommandText = My.Resources.VerificaIntegridadIdentificadoresCanalSubCanal
                        command.CommandType = CommandType.Text

                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CANAL", ProsegurDbType.Objeto_Id, identificadorCanal))
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCANAL", ProsegurDbType.Objeto_Id, identificadorSubCanal))

                        command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, command.CommandText)

                        If AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, command) = 0 Then
                            validaciones.Add(New Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL112", .descripcion = Tradutor.Traduzir("msg_relacion_canal_subcanal_invalida")})
                        End If

                    End Using

                Catch ex As Exception
                    Throw
                Finally
                    GC.Collect()
                End Try

            End If

            ' Delegacion/Planta/Sector
            If Not String.IsNullOrEmpty(identificadorDelegacion) AndAlso Not String.IsNullOrEmpty(identificadorPlanta) AndAlso Not String.IsNullOrEmpty(identificadorSector) Then
                Try
                    Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                        command.CommandText = My.Resources.VerificaIntegridadIdentificadoresDelegacionPlantaSector
                        command.CommandType = CommandType.Text

                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DELEGACION", ProsegurDbType.Objeto_Id, identificadorDelegacion))
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PLANTA", ProsegurDbType.Objeto_Id, identificadorPlanta))
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Objeto_Id, identificadorSector))

                        command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, command.CommandText)

                        If AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, command) = 0 Then
                            validaciones.Add(New Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL112", .descripcion = Tradutor.Traduzir("msg_relacion_delegacion_planta_sector_invalida")})
                        End If

                    End Using

                Catch ex As Exception
                    Throw
                Finally
                    GC.Collect()
                End Try
            End If

        End Sub

#End Region

#Region "[Certificacion]"

        ''' <summary>
        ''' Recupera as contas.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 04/06/2013 - Criado
        ''' </history>
        Public Shared Function RecuperarCuentasPorCodCliente(Peticion As CSCertificacion.DatosCertificacion.Peticion) As CSCertificacion.CuentaColeccion

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.CuentaRecuperarPorCodCliente
            cmd.CommandType = CommandType.Text

            Dim Query As New StringBuilder

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_CERTIFICADO", ProsegurDbType.Logico, False))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, If(Peticion.Cliente IsNot Nothing, Peticion.Cliente.Codigo, String.Empty)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION", ProsegurDbType.Data_Hora, Peticion.FyhCertificado.QuieroGrabarGMTZeroEnLaBBDD(Peticion.DelegacionLogada)))

            Query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.CodigosDelegaciones, "COD_DELEGACION", cmd, "AND", "D"))
            Query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.CodigosSectores, "COD_SECTOR", cmd, "AND", "SC"))
            Query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.CodigosSubCanales, "COD_SUBCANAL", cmd, "AND", "SBC"))

            cmd.CommandText = String.Format(cmd.CommandText, Query.ToString)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            Dim cuentas As CSCertificacion.CuentaColeccion = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                cuentas = New CSCertificacion.CuentaColeccion
                Dim cuenta As CSCertificacion.Cuenta = Nothing
                Dim documento As CSCertificacion.Documento = Nothing
                Dim IdentificadorCuenta As String = String.Empty
                Dim IdentificadorTransaccionEfectivo As String = String.Empty
                Dim IdentificadorTransaccionMedioPago As String = String.Empty
                Dim IdentificadorSaldoEfectivo As String = String.Empty
                Dim IdentificadorSaldoMedioPago As String = String.Empty
                Dim IdentificadorDocumento As String = String.Empty
                Dim objSaldoCuentaEfectvo As CSCertificacion.CertificadoSaldoEfectivoColeccion = Nothing
                Dim objSaldoCuentaMedioPago As CSCertificacion.CertificadoSaldoMedioPagoColeccion = Nothing

                'Recupera o saldo de medio pago
                Dim SaldoMedioPagoTransacciones As CSCertificacion.CuentaColeccion = AccesoDatos.GenesisSaldos.SaldoMedioPago.RecuperarSaldoMedioPago(Peticion)

                'Recupera o saldo de efectivo
                Dim SaldoEfectivoTransacciones As CSCertificacion.CuentaColeccion = AccesoDatos.GenesisSaldos.SaldoEfectivo.RecuperarSaldoEfectivo(Peticion)

                'Recupera o identificador do ultimo certificado definitivo.
                Dim IdentificadorUltimoCertificadoDefinitivo As String = AccesoDatos.GenesisSaldos.Certificacion.Comun.RecuperarOidUltimoCertificado(Peticion,
                                                                                                                               Prosegur.Genesis.ContractoServicio.Constantes.CONST_TIPO_CERTIFICADO_DEFINITIVO)

                'Recupera o saldo do ultimo certificado definitivo.
                Dim SaldosEfectivosUltimoCertificado As CSCertificacion.CuentaColeccion = AccesoDatos.GenesisSaldos.Certificacion.SaldoEfectivo.RecuperarSaldoCertificado(IdentificadorUltimoCertificadoDefinitivo)

                'Recupera o saldo do ultimo certificado definitivo.
                Dim SaldosMedioPagoUltimoCertDefinitivo As CSCertificacion.CuentaColeccion = AccesoDatos.GenesisSaldos.Certificacion.SaldoMedioPago.RecuperarSaldoCertificado(IdentificadorUltimoCertificadoDefinitivo)

                'Recupera o saldo do efectivo
                RecuperarSaldoEfectivoCuentasPorCodCliente(SaldoEfectivoTransacciones, SaldosEfectivosUltimoCertificado)

                'Recupera o saldo de medio pago
                RecuperarSaldoMedioPagoCuentasPorCodCliente(SaldoMedioPagoTransacciones, SaldosMedioPagoUltimoCertDefinitivo)

                For Each dr In dt.Rows

                    IdentificadorCuenta = Util.AtribuirValorObj(dr("OID_CUENTA"), GetType(String))

                    cuenta = (From c In cuentas Where c.IdentificadorCuenta = IdentificadorCuenta).FirstOrDefault

                    If cuenta Is Nothing Then

                        cuentas.Add(New CSCertificacion.Cuenta With { _
                                    .IdentificadorCuenta = IdentificadorCuenta, _
                                    .CodCliente = Util.AtribuirValorObj(dr("COD_CLIENTE"), GetType(String)), _
                                    .DesCliente = Util.AtribuirValorObj(dr("DES_CLIENTE"), GetType(String)), _
                                    .CodDelegacion = Util.AtribuirValorObj(dr("COD_DELEGACION"), GetType(String)), _
                                    .CodPtoServicio = Util.AtribuirValorObj(dr("COD_PTO_SERVICIO"), GetType(String)), _
                                    .DesPtoServicio = Util.AtribuirValorObj(dr("DES_PTO_SERVICIO"), GetType(String)), _
                                    .CodSector = Util.AtribuirValorObj(dr("COD_SECTOR"), GetType(String)), _
                                    .DesSector = Util.AtribuirValorObj(dr("DES_SECTOR"), GetType(String)), _
                                    .CodSubcanal = Util.AtribuirValorObj(dr("COD_SUBCANAL"), GetType(String)), _
                                    .DesSubcanal = Util.AtribuirValorObj(dr("DES_SUBCANAL"), GetType(String)), _
                                    .CodSubcliente = Util.AtribuirValorObj(dr("COD_SUBCLIENTE"), GetType(String)), _
                                    .DesSubcliente = Util.AtribuirValorObj(dr("DES_SUBCLIENTE"), GetType(String)), _
                                    .CodTipoCuenta = Util.AtribuirValorObj(dr("COD_TIPO_CUENTA"), GetType(String))})

                        cuenta = (From c In cuentas Where c.IdentificadorCuenta = IdentificadorCuenta).FirstOrDefault

                    End If

                    IdentificadorDocumento = Util.AtribuirValorObj(dr("OID_DOCUMENTO"), GetType(String))
                    IdentificadorTransaccionEfectivo = Util.AtribuirValorObj(dr("OID_TRANSACCION_EFECTIVO"), GetType(String))
                    IdentificadorTransaccionMedioPago = Util.AtribuirValorObj(dr("OID_TRANSACCION_MEDIO_PAGO"), GetType(String))

                    ' SaldoEfectivoTransacciones NÃO É DE PREENCHIMENTO OBRIGATÓRIO
                    'Recupera o saldo de efectivo
                    If SaldoEfectivoTransacciones IsNot Nothing AndAlso SaldoEfectivoTransacciones.Count > 0 AndAlso _
                       (cuenta.SaldosEfectivos Is Nothing OrElse cuenta.SaldosEfectivos.Count = 0) Then

                        cuenta.SaldosEfectivos = (From cu In SaldoEfectivoTransacciones Where cu.IdentificadorCuenta = cuenta.IdentificadorCuenta Select cu.SaldosEfectivos).FirstOrDefault
                    End If

                    ' SaldoMedioPagoTransacciones NÃO É DE PREENCHIMENTO OBRIGATÓRIO
                    'Recupera o saldo de meio de pagamento
                    If SaldoMedioPagoTransacciones IsNot Nothing AndAlso SaldoMedioPagoTransacciones.Count > 0 AndAlso _
                        (cuenta.SaldosMedioPagos Is Nothing OrElse cuenta.SaldosMedioPagos.Count = 0) Then

                        cuenta.SaldosMedioPagos = (From cu In SaldoMedioPagoTransacciones Where cu.IdentificadorCuenta = cuenta.IdentificadorCuenta Select cu.SaldosMedioPagos).FirstOrDefault
                    End If

                    ' AQUI DEVE FAZER VALIDAÇÃO DE NULL
                    'Recupera o documento
                    documento = AccesoDatos.GenesisSaldos.Documento.RecuperarDocumento(IdentificadorDocumento)

                    If documento IsNot Nothing Then

                        If documento IsNot Nothing AndAlso cuenta.Documentos Is Nothing Then
                            cuenta.Documentos = New CSCertificacion.DocumentoColeccion
                        End If
                        If documento IsNot Nothing Then
                            Dim dc = cuenta.Documentos.FirstOrDefault(Function(f) f.OidDocumento = IdentificadorDocumento)
                            If dc Is Nothing Then
                                cuenta.Documentos.Add(documento)
                            Else
                                documento = dc
                            End If
                        End If
                    End If


                    If Not String.IsNullOrEmpty(IdentificadorTransaccionEfectivo) Then

                        ' TransaccionEfectivoColeccion NÃO É DE PREENCHIMENTO OBRIGATÓRIO
                        Dim te = AccesoDatos.GenesisSaldos.TransaccionEfectivo.ObtenerTransaccionEfectivo(IdentificadorTransaccionEfectivo)
                        If te IsNot Nothing AndAlso documento.TransaccionesEfectivo Is Nothing Then
                            'Istancia a coleção de transações efectivo
                            documento.TransaccionesEfectivo = New CSCertificacion.TransaccionEfectivoColeccion()
                        End If
                        If te IsNot Nothing AndAlso
                                Not documento.TransaccionesEfectivo.Exists(Function(f) f IsNot Nothing AndAlso f.OidTransaccionEfectivo = IdentificadorTransaccionEfectivo) Then
                            documento.TransaccionesEfectivo.Add(te)
                        End If


                    End If

                    If Not String.IsNullOrEmpty(IdentificadorTransaccionMedioPago) Then

                        ' TransaccionMedioPagoColeccion NÃO É DE PREENCHIMENTO OBRIGATÓRIO
                        Dim tmp = AccesoDatos.GenesisSaldos.TransaccionMedioPago.RetornarTransaccionMedioPago(IdentificadorTransaccionMedioPago)
                        If tmp IsNot Nothing AndAlso documento.TransaccionesMedioPago Is Nothing Then
                            'Istancia a coleção de transações medio pago
                            documento.TransaccionesMedioPago = New CSCertificacion.TransaccionMedioPagoColeccion()
                        End If
                        If tmp IsNot Nothing AndAlso
                            Not documento.TransaccionesMedioPago.Exists(Function(f) f IsNot Nothing AndAlso f.IdentificadorTransaccionMedioPago = IdentificadorTransaccionMedioPago) Then
                            documento.TransaccionesMedioPago.Add(tmp)
                        End If

                    End If

                Next

                'Recupera o saldos das contas que não tem transação para o certficado corrente.
                If SaldoEfectivoTransacciones IsNot Nothing AndAlso SaldoEfectivoTransacciones.Count > 0 Then

                    For Each objCuenta In SaldoEfectivoTransacciones

                        If cuentas IsNot Nothing Then

                            cuenta = (From cu In cuentas Where cu.IdentificadorCuenta = objCuenta.IdentificadorCuenta).FirstOrDefault

                            If cuenta Is Nothing OrElse cuenta.SaldosEfectivos Is Nothing Then

                                If cuenta Is Nothing Then

                                    cuenta = RecuperarDatosCuenta(objCuenta.IdentificadorCuenta)

                                    cuentas.Add(cuenta)

                                    cuenta = (From cu In cuentas Where cu.IdentificadorCuenta = objCuenta.IdentificadorCuenta).FirstOrDefault

                                End If

                                cuenta.SaldosEfectivos = objCuenta.SaldosEfectivos

                            End If

                        End If

                    Next

                End If

                'Recupera o saldo dos medios pago
                If SaldoMedioPagoTransacciones IsNot Nothing AndAlso SaldoMedioPagoTransacciones.Count > 0 Then

                    For Each objCuenta In SaldoMedioPagoTransacciones

                        If cuentas IsNot Nothing Then

                            cuenta = (From cu In cuentas Where cu.IdentificadorCuenta = objCuenta.IdentificadorCuenta).FirstOrDefault

                            If cuenta Is Nothing OrElse cuenta.SaldosEfectivos Is Nothing Then

                                If cuenta Is Nothing Then

                                    cuenta = RecuperarDatosCuenta(objCuenta.IdentificadorCuenta)

                                    cuentas.Add(cuenta)

                                    cuenta = (From cu In cuentas Where cu.IdentificadorCuenta = objCuenta.IdentificadorCuenta).FirstOrDefault

                                End If

                                cuenta.SaldosMedioPagos = objCuenta.SaldosMedioPagos

                            End If

                        End If

                    Next

                End If

            End If

            Return cuentas
        End Function

        ''' <summary>
        ''' Recupera o saldo de efectivo das contas.
        ''' </summary>
        ''' <param name="SaldoMedioPagoTransacciones"></param>
        ''' <param name="SaldosMedioPagoUltimoCertDefinitivo"></param>
        ''' <remarks></remarks>
        Private Shared Sub RecuperarSaldoMedioPagoCuentasPorCodCliente(ByRef SaldoMedioPagoTransacciones As CSCertificacion.CuentaColeccion,
                                                                       SaldosMedioPagoUltimoCertDefinitivo As CSCertificacion.CuentaColeccion)

            Dim cuenta As CSCertificacion.Cuenta = Nothing

            If SaldoMedioPagoTransacciones IsNot Nothing AndAlso SaldoMedioPagoTransacciones.Count > 0 Then

                For Each cuenta In SaldoMedioPagoTransacciones

                    For Each Saldo In cuenta.SaldosMedioPagos

                        If SaldosMedioPagoUltimoCertDefinitivo IsNot Nothing AndAlso SaldosMedioPagoUltimoCertDefinitivo.Count > 0 Then

                            'Verifica se o saldo do certificado ja contem saldo para a conta corrente.
                            Dim objSaldoCertificado = From c In SaldosMedioPagoUltimoCertDefinitivo,
                                                      s In c.SaldosMedioPagos
                                                      Where c.IdentificadorCuenta = cuenta.IdentificadorCuenta AndAlso _
                                                      s.BolDisponible = Saldo.BolDisponible AndAlso _
                                                      s.CodigoMedioPago = Saldo.CodigoMedioPago AndAlso _
                                                      s.CodigoNivelDetalle = Saldo.CodigoNivelDetalle

                            If objSaldoCertificado.Count > 0 Then
                                Saldo.NumImporteInicial = objSaldoCertificado.First.s.NumImporteFinal
                            End If

                        End If

                        Saldo.NumImporteFinal = Saldo.NumImporte + Saldo.NumImporteInicial

                    Next

                Next

            End If

            'Atualiza o saldo para as denominações com saldo igual a zero
            If SaldosMedioPagoUltimoCertDefinitivo IsNot Nothing AndAlso SaldosMedioPagoUltimoCertDefinitivo.Count > 0 Then

                If SaldoMedioPagoTransacciones Is Nothing Then SaldoMedioPagoTransacciones = New CSCertificacion.CuentaColeccion
                Dim objCuenta As CSCertificacion.Cuenta = Nothing

                'Se não existe saldo para as transações atuais, atualiza o saldo
                'baseado no saldo do ultimo certificado
                For Each cuenta In SaldosMedioPagoUltimoCertDefinitivo

                    For Each Saldo In cuenta.SaldosMedioPagos

                        If SaldoMedioPagoTransacciones IsNot Nothing AndAlso SaldoMedioPagoTransacciones.Count > 0 Then

                            'Verifica se o saldo do certificado ja contem saldo para a conta corrente.
                            Dim objSaldoCertificado = From c In SaldoMedioPagoTransacciones,
                                                      s In c.SaldosMedioPagos
                                                      Where c.IdentificadorCuenta = cuenta.IdentificadorCuenta AndAlso _
                                                      s.BolDisponible = Saldo.BolDisponible AndAlso _
                                                      s.CodigoMedioPago = Saldo.CodigoMedioPago AndAlso _
                                                      s.CodigoNivelDetalle = Saldo.CodigoNivelDetalle

                            If objSaldoCertificado.Count > 0 Then
                                Continue For
                            End If

                        End If

                        objCuenta = (From cu In SaldoMedioPagoTransacciones Where cu.IdentificadorCuenta = cuenta.IdentificadorCuenta).FirstOrDefault

                        If objCuenta Is Nothing Then

                            SaldoMedioPagoTransacciones.Add(New CSCertificacion.Cuenta With { _
                                                            .IdentificadorCuenta = cuenta.IdentificadorCuenta})

                            objCuenta = (From cu In SaldoMedioPagoTransacciones Where cu.IdentificadorCuenta = cuenta.IdentificadorCuenta).FirstOrDefault

                            objCuenta.SaldosMedioPagos = New CSCertificacion.CertificadoSaldoMedioPagoColeccion

                        End If

                        objCuenta.SaldosMedioPagos.Add(New CSCertificacion.CertificadoSaldoMedioPago With { _
                                                      .BolDisponible = Saldo.BolDisponible, _
                                                      .CodigoMedioPago = Saldo.CodigoMedioPago, _
                                                      .CodigoNivelDetalle = Saldo.CodigoNivelDetalle, _
                                                      .NelCantidad = 0, _
                                                      .NumImporte = 0, _
                                                      .NumImporteInicial = Saldo.NumImporteFinal, _
                                                      .NumImporteFinal = .NumImporteFinal, _
                                                      .IdentificadorSaldoMedioPago = Saldo.IdentificadorSaldoMedioPago, _
                                                      .IdentificadorDivisa = Saldo.IdentificadorDivisa, _
                                                      .IdentificadorUnidadMedida = Saldo.IdentificadorUnidadMedida})

                    Next

                Next

            End If
        End Sub

        ''' <summary>
        ''' Recupera o saldo de efectivo das contas.
        ''' </summary>
        ''' <param name="SaldoEfectivoTransacciones"></param>
        ''' <param name="SaldosEfectivosUltimoCertificado"></param>
        ''' <remarks></remarks>
        Private Shared Sub RecuperarSaldoEfectivoCuentasPorCodCliente(ByRef SaldoEfectivoTransacciones As CSCertificacion.CuentaColeccion,
                                                                      SaldosEfectivosUltimoCertificado As CSCertificacion.CuentaColeccion)

            Dim cuenta As CSCertificacion.Cuenta = Nothing

            If SaldoEfectivoTransacciones IsNot Nothing AndAlso SaldoEfectivoTransacciones.Count > 0 Then

                For Each cuenta In SaldoEfectivoTransacciones

                    For Each Saldo In cuenta.SaldosEfectivos

                        If SaldosEfectivosUltimoCertificado IsNot Nothing AndAlso SaldosEfectivosUltimoCertificado.Count > 0 Then

                            'Verifica se o saldo do certificado ja contem saldo para a conta corrente.
                            Dim objSaldoCertificado = From c In SaldosEfectivosUltimoCertificado,
                                                      s In c.SaldosEfectivos Where c.IdentificadorCuenta = cuenta.IdentificadorCuenta AndAlso s.BolDisponible = Saldo.BolDisponible AndAlso _
                                                      s.CodigoDenominacion = Saldo.CodigoDenominacion AndAlso s.CodigoIsoDivisa = Saldo.CodigoIsoDivisa AndAlso _
                                                      s.CodigoNivelDetalle = Saldo.CodigoNivelDetalle AndAlso s.IdentificadorCalidad = Saldo.IdentificadorCalidad AndAlso _
                                                      s.CodigoTipoEfectivo = Saldo.CodigoTipoEfectivo

                            If objSaldoCertificado.Count > 0 Then
                                Saldo.NumImporteInicial = objSaldoCertificado.First.s.NumImporteFinal
                            End If

                        End If

                        Saldo.NumImporteFinal = Saldo.NumImporte + Saldo.NumImporteInicial

                    Next

                Next

            End If

            'Atualiza o saldo para as denominações com saldo igual a zero
            If SaldosEfectivosUltimoCertificado IsNot Nothing AndAlso SaldosEfectivosUltimoCertificado.Count > 0 Then

                If SaldoEfectivoTransacciones Is Nothing Then SaldoEfectivoTransacciones = New CSCertificacion.CuentaColeccion
                Dim objCuenta As CSCertificacion.Cuenta = Nothing

                'Se não existe saldo para as transações atuais, atualiza o saldo
                'baseado no saldo do ultimo certificado
                For Each cuenta In SaldosEfectivosUltimoCertificado

                    For Each Saldo In cuenta.SaldosEfectivos

                        If SaldoEfectivoTransacciones IsNot Nothing AndAlso SaldoEfectivoTransacciones.Count > 0 Then

                            'Verifica se o saldo do certificado ja contem saldo para a conta corrente.
                            Dim objSaldoCertificado = From c In SaldoEfectivoTransacciones,
                                                      s In c.SaldosEfectivos Where c.IdentificadorCuenta = cuenta.IdentificadorCuenta AndAlso s.BolDisponible = Saldo.BolDisponible AndAlso _
                                                      s.CodigoDenominacion = Saldo.CodigoDenominacion AndAlso s.CodigoIsoDivisa = Saldo.CodigoIsoDivisa AndAlso _
                                                      s.CodigoNivelDetalle = Saldo.CodigoNivelDetalle AndAlso s.IdentificadorCalidad = Saldo.IdentificadorCalidad AndAlso _
                                                      s.CodigoTipoEfectivo = Saldo.CodigoTipoEfectivo

                            If objSaldoCertificado.Count > 0 Then
                                Continue For
                            End If

                        End If

                        objCuenta = (From cu In SaldoEfectivoTransacciones Where cu.IdentificadorCuenta = cuenta.IdentificadorCuenta).FirstOrDefault

                        If objCuenta Is Nothing Then

                            SaldoEfectivoTransacciones.Add(New CSCertificacion.Cuenta With { _
                                                           .IdentificadorCuenta = cuenta.IdentificadorCuenta})

                            objCuenta = (From cu In SaldoEfectivoTransacciones Where cu.IdentificadorCuenta = cuenta.IdentificadorCuenta).FirstOrDefault

                            objCuenta.SaldosEfectivos = New CSCertificacion.CertificadoSaldoEfectivoColeccion

                        End If

                        objCuenta.SaldosEfectivos.Add(New CSCertificacion.CertificadoSaldoEfectivo With { _
                                                      .BolDisponible = Saldo.BolDisponible, _
                                                      .IdentificadorCalidad = Saldo.IdentificadorCalidad, _
                                                      .CodigoDenominacion = Saldo.CodigoDenominacion, _
                                                      .CodigoIsoDivisa = Saldo.CodigoIsoDivisa, _
                                                      .CodigoNivelDetalle = Saldo.CodigoNivelDetalle, _
                                                      .CodigoTipoEfectivo = Saldo.CodigoTipoEfectivo, _
                                                      .DescripcionDenominacion = Saldo.DescripcionDenominacion, _
                                                      .DescripcionDivisa = Saldo.DescripcionDivisa, _
                                                      .NelCantidad = 0, _
                                                      .NumImporte = 0, _
                                                      .NumImporteInicial = Saldo.NumImporteFinal, _
                                                      .NumImporteFinal = .NumImporteFinal, _
                                                      .IdentificadorSaldoEfectivo = Saldo.IdentificadorSaldoEfectivo, _
                                                      .IdentificadorDivisa = Saldo.IdentificadorDivisa})


                    Next

                Next

            End If
        End Sub

        ''' <summary>
        ''' Retorna os dados da conta.
        ''' </summary>
        ''' <param name="OidCuenta"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 20/06/2013 - Criado
        ''' </history>
        Private Shared Function RecuperarDatosCuenta(OidCuenta As String) As ContractoServicio.GenesisSaldos.Certificacion.Cuenta

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CuentaRecuperarDatos)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA", ProsegurDbType.Objeto_Id, OidCuenta))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            Dim objCuenta As ContractoServicio.GenesisSaldos.Certificacion.Cuenta = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objCuenta = New ContractoServicio.GenesisSaldos.Certificacion.Cuenta

                With objCuenta
                    .CodCliente = Util.AtribuirValorObj(dt.Rows(0)("COD_CLIENTE"), GetType(String))
                    .CodDelegacion = Util.AtribuirValorObj(dt.Rows(0)("COD_DELEGACION"), GetType(String))
                    .CodPtoServicio = Util.AtribuirValorObj(dt.Rows(0)("COD_PTO_SERVICIO"), GetType(String))
                    .CodSector = Util.AtribuirValorObj(dt.Rows(0)("COD_SECTOR"), GetType(String))
                    .CodSubcanal = Util.AtribuirValorObj(dt.Rows(0)("COD_SUBCANAL"), GetType(String))
                    .CodSubcliente = Util.AtribuirValorObj(dt.Rows(0)("COD_SUBCLIENTE"), GetType(String))
                    .CodTipoCuenta = Util.AtribuirValorObj(dt.Rows(0)("COD_TIPO_CUENTA"), GetType(String))
                    .DesCliente = Util.AtribuirValorObj(dt.Rows(0)("DES_CLIENTE"), GetType(String))
                    .DesDelegacion = Util.AtribuirValorObj(dt.Rows(0)("DES_DELEGACION"), GetType(String))
                    .DesPtoServicio = Util.AtribuirValorObj(dt.Rows(0)("DES_PTO_SERVICIO"), GetType(String))
                    .DesSector = Util.AtribuirValorObj(dt.Rows(0)("DES_SECTOR"), GetType(String))
                    .DesSubcanal = Util.AtribuirValorObj(dt.Rows(0)("DES_SUBCANAL"), GetType(String))
                    .DesSubcliente = Util.AtribuirValorObj(dt.Rows(0)("DES_SUBCLIENTE"), GetType(String))
                    .IdentificadorCuenta = Util.AtribuirValorObj(dt.Rows(0)("OID_CUENTA"), GetType(String))
                End With
            End If

            Return objCuenta
        End Function

        ''' <summary>
        ''' Recupera as contas.
        ''' </summary>
        ''' <param name="IdentificadorCertificado"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 04/06/2013 - Criado
        ''' </history>
        Public Shared Function RecuperarCuentas(IdentificadorCertificado As String) As CSCertificacion.CuentaColeccion

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CuentaRecuperarCuentasCertificado)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CERTIFICADO", ProsegurDbType.Objeto_Id, IdentificadorCertificado))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            Dim cuentas As CSCertificacion.CuentaColeccion = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                cuentas = New CSCertificacion.CuentaColeccion
                Dim cuenta As CSCertificacion.Cuenta = Nothing
                Dim documento As CSCertificacion.Documento = Nothing
                Dim OidCuenta As String = String.Empty
                Dim OidTransaccionEfectivo As String = String.Empty
                Dim OidTransaccionMedioPago As String = String.Empty
                Dim OidSaldoEfectivo As String = String.Empty
                Dim OidSaldoMedioPago As String = String.Empty
                Dim OidDocumento As String = String.Empty

                For Each dr In dt.Rows


                    OidCuenta = Util.AtribuirValorObj(dr("OID_CUENTA"), GetType(String))

                    cuenta = (From c In cuentas Where c.IdentificadorCuenta = OidCuenta).FirstOrDefault

                    If cuenta Is Nothing Then

                        cuentas.Add(New CSCertificacion.Cuenta With { _
                                    .IdentificadorCuenta = OidCuenta, _
                                    .CodCliente = Util.AtribuirValorObj(dr("COD_CLIENTE"), GetType(String)), _
                                    .DesCliente = Util.AtribuirValorObj(dr("DES_CLIENTE"), GetType(String)), _
                                    .CodDelegacion = Util.AtribuirValorObj(dr("COD_DELEGACION"), GetType(String)), _
                                    .CodPtoServicio = Util.AtribuirValorObj(dr("COD_PTO_SERVICIO"), GetType(String)), _
                                    .DesPtoServicio = Util.AtribuirValorObj(dr("DES_PTO_SERVICIO"), GetType(String)), _
                                    .CodSector = Util.AtribuirValorObj(dr("COD_SECTOR"), GetType(String)), _
                                    .DesSector = Util.AtribuirValorObj(dr("DES_SECTOR"), GetType(String)), _
                                    .CodSubcanal = Util.AtribuirValorObj(dr("COD_SUBCANAL"), GetType(String)), _
                                    .DesSubcanal = Util.AtribuirValorObj(dr("DES_SUBCANAL"), GetType(String)), _
                                    .CodSubcliente = Util.AtribuirValorObj(dr("COD_SUBCLIENTE"), GetType(String)), _
                                    .DesSubcliente = Util.AtribuirValorObj(dr("DES_SUBCLIENTE"), GetType(String)), _
                                    .CodTipoCuenta = Util.AtribuirValorObj(dr("COD_TIPO_CUENTA"), GetType(String))})

                        cuenta = (From c In cuentas Where c.IdentificadorCuenta = OidCuenta).FirstOrDefault


                    End If

                    OidDocumento = Util.AtribuirValorObj(dr("OID_DOCUMENTO"), GetType(String))
                    OidTransaccionEfectivo = Util.AtribuirValorObj(dr("OID_TRANSACCION_EFECTIVO"), GetType(String))
                    OidTransaccionMedioPago = Util.AtribuirValorObj(dr("OID_TRANSACCION_MEDIO_PAGO"), GetType(String))
                    OidSaldoEfectivo = Util.AtribuirValorObj(dr("OID_CERT_SALDO_EFECTIVO"), GetType(String))
                    OidSaldoMedioPago = Util.AtribuirValorObj(dr("OID_CERT_SALDO_MEDIO_PAGO"), GetType(String))


                    ' AQUI DEVE FAZER VALIDAÇÃO DE NULL
                    'Recupera o documento
                    documento = AccesoDatos.GenesisSaldos.Documento.RecuperarDocumento(OidDocumento)

                    If documento IsNot Nothing Then

                        If documento IsNot Nothing AndAlso cuenta.Documentos Is Nothing Then
                            cuenta.Documentos = New CSCertificacion.DocumentoColeccion
                        End If
                        If documento IsNot Nothing Then
                            Dim dc = cuenta.Documentos.FirstOrDefault(Function(f) f.OidDocumento = OidDocumento)
                            If dc Is Nothing Then
                                cuenta.Documentos.Add(documento)
                            Else
                                documento = dc
                            End If
                        End If


                    End If

                    If Not String.IsNullOrEmpty(OidSaldoEfectivo) Then

                        'Recupera o saldo do efectivo
                        Dim se = AccesoDatos.GenesisSaldos.Certificacion.SaldoEfectivo.RecuperarSaldoEfevtivo(OidSaldoEfectivo)

                        If se IsNot Nothing Then
                            'Istancia a coleção de saldo efectivo
                            If cuenta.SaldosEfectivos Is Nothing Then
                                cuenta.SaldosEfectivos = New CSCertificacion.CertificadoSaldoEfectivoColeccion
                            End If

                            If Not cuenta.SaldosEfectivos.Exists(Function(s) s IsNot Nothing AndAlso s.IdentificadorSaldoEfectivo = se.IdentificadorSaldoEfectivo) Then
                                cuenta.SaldosEfectivos.Add(se)
                            End If
                        End If

                    End If

                    If Not String.IsNullOrEmpty(OidSaldoMedioPago) Then

                        'Recupera o saldo do efectivo medio pago
                        Dim smp = AccesoDatos.GenesisSaldos.Certificacion.SaldoMedioPago.RecuperarSaldo(OidSaldoMedioPago)

                        If smp IsNot Nothing Then
                            'Istancia a coleção de saldo efectivo
                            If cuenta.SaldosMedioPagos Is Nothing Then
                                cuenta.SaldosMedioPagos = New CSCertificacion.CertificadoSaldoMedioPagoColeccion
                            End If

                            If Not cuenta.SaldosMedioPagos.Exists(Function(s) s IsNot Nothing AndAlso s.IdentificadorSaldoMedioPago = OidSaldoMedioPago) Then
                                cuenta.SaldosMedioPagos.Add(smp)
                            End If
                        End If

                    End If


                    If Not String.IsNullOrEmpty(OidTransaccionEfectivo) Then

                        ' TransaccionEfectivoColeccion NÃO É DE PREENCHIMENTO OBRIGATÓRIO
                        Dim te = AccesoDatos.GenesisSaldos.TransaccionEfectivo.ObtenerTransaccionEfectivo(OidTransaccionEfectivo)
                        If te IsNot Nothing AndAlso documento.TransaccionesEfectivo Is Nothing Then
                            'Istancia a coleção de transações efectivo
                            documento.TransaccionesEfectivo = New CSCertificacion.TransaccionEfectivoColeccion()
                        End If
                        If te IsNot Nothing AndAlso
                                Not documento.TransaccionesEfectivo.Exists(Function(f) f IsNot Nothing AndAlso f.OidTransaccionEfectivo = OidTransaccionEfectivo) Then
                            documento.TransaccionesEfectivo.Add(te)
                        End If


                    End If

                    If Not String.IsNullOrEmpty(OidTransaccionMedioPago) Then

                        ' TransaccionMedioPagoColeccion NÃO É DE PREENCHIMENTO OBRIGATÓRIO                                  
                        Dim tmp = AccesoDatos.GenesisSaldos.TransaccionMedioPago.RetornarTransaccionMedioPago(OidTransaccionMedioPago)
                        If tmp IsNot Nothing AndAlso documento.TransaccionesMedioPago Is Nothing Then
                            'Istancia a coleção de transações medio pago
                            documento.TransaccionesMedioPago = New CSCertificacion.TransaccionMedioPagoColeccion()
                        End If
                        If tmp IsNot Nothing AndAlso
                            Not documento.TransaccionesMedioPago.Exists(Function(f) f IsNot Nothing AndAlso f.IdentificadorTransaccionMedioPago = OidTransaccionMedioPago) Then
                            documento.TransaccionesMedioPago.Add(tmp)
                        End If

                    End If


                Next

            End If

            Return cuentas
        End Function

#End Region

    End Class

End Namespace