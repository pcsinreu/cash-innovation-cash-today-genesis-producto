Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Transactions
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Saldo
Imports System.Data
Imports Prosegur.Framework.Dicionario
Imports System.Text
Imports System.Threading.Tasks

Namespace GenesisSaldos

    ''' <summary>
    ''' Classe responsável pelo gerenciamento de saldo
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Saldo

#Region "RecuperarSaldoExpuestoxDetallado"


        ''' <summary>
        ''' Recupera o saldo Detallado, exposto e elementos
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Ejecutar(Peticion As RecuperarSaldoExpuestoxDetallado.Peticion) As RecuperarSaldoExpuestoxDetallado.Respuesta

            Dim Respuesta As New RecuperarSaldoExpuestoxDetallado.Respuesta

            Try

                Util.ValidarCampoObrigatorio(Peticion.CodigoDelegacion, "NUEVO_SALDOS_029_CodigoDelegacion", GetType(String), False, True)
                Util.ValidarCampoObrigatorio(Peticion.CodigoPlanta, "NUEVO_SALDOS_029_CodigoPlanta", GetType(String), False, True)

                If String.IsNullOrEmpty(Peticion.CodigoSectorPadre) Then
                    Util.ValidarCampoObrigatorio(Peticion.CodigosSectores, "NUEVO_SALDOS_029_CodigosSectores", GetType(ObservableCollection(Of String)), True, True)
                End If

                If Not String.IsNullOrEmpty(Peticion.CodigoCliente) Then
                    Util.ValidarCampoObrigatorio(Peticion.CodigoCanal, "NUEVO_SALDOS_029_CodigoCanal", GetType(String), False, True)
                    Util.ValidarCampoObrigatorio(Peticion.CodigoSubCanal, "NUEVO_SALDOS_029_CodigoSubCanal", GetType(String), False, True)
                End If

                'Recupera o saldo de acordo com o tipo
                Select Case Peticion.TipoDeSaldo

                    Case Enumeradores.TipoBuscaSaldo.Total

                        'Verifica se é para recuperar o saldo por sector
                        If String.IsNullOrEmpty(Peticion.CodigoCliente) Then

                            'Recupera o saldo a nivel de sector
                            Respuesta.Saldos = AccesoDatos.GenesisSaldos.SaldoEfectivo.RecuperarSaldoTotalEfectivoSector(Peticion.CodigosSectores,
                                                                                                                         Peticion.CodigoSectorPadre,
                                                                                                                         Peticion.CodigoPlanta,
                                                                                                                         Peticion.CodigoDelegacion, Nothing, False)

                        Else

                            'Recupera o saldo a nivel de conta
                            Respuesta.Saldos = AccesoDatos.GenesisSaldos.SaldoEfectivo.RecuperarSaldoPorCuenta(Peticion.CodigosSectores,
                                                                                                               Peticion.CodigoCliente,
                                                                                                               Peticion.CodigoSubCliente,
                                                                                                               Peticion.CodigoPuntoServicio,
                                                                                                               Peticion.CodigoSectorPadre,
                                                                                                               Peticion.CodigoPlanta,
                                                                                                               Peticion.CodigoDelegacion,
                                                                                                               Peticion.CodigoSubCanal, Nothing, False)


                        End If

                    Case Enumeradores.TipoBuscaSaldo.Exposto

                        ''Recupera o saldo a nivel de conta
                        Respuesta.Saldos = AccesoDatos.GenesisSaldos.SaldoEfectivo.RecuperarSaldoPorCuenta(Peticion.CodigosSectores,
                                                                                                                   Peticion.CodigoCliente,
                                                                                                                   Peticion.CodigoSubCliente,
                                                                                                                   Peticion.CodigoPuntoServicio,
                                                                                                                   Peticion.CodigoSectorPadre,
                                                                                                                   Peticion.CodigoPlanta,
                                                                                                                   Peticion.CodigoDelegacion,
                                                                                                                   Peticion.CodigoSubCanal, True, False)
                        If Respuesta.Saldos IsNot Nothing AndAlso Respuesta.Saldos.Count > 0 Then

                            For Each Saldo In Respuesta.Saldos
                                If Saldo.Divisas IsNot Nothing AndAlso Saldo.Divisas.Count > 0 Then
                                    For Each Divisa In Saldo.Divisas
                                        Divisa.MediosPago = Nothing
                                    Next
                                End If
                            Next

                        End If

                    Case Enumeradores.TipoBuscaSaldo.ExpostoSinMedioPago

                        ''Recupera o saldo a nivel de conta
                        Respuesta.Saldos = AccesoDatos.GenesisSaldos.SaldoEfectivo.RecuperarSaldoPorCuenta(Peticion.CodigosSectores,
                                                                                                           Peticion.CodigoCliente,
                                                                                                           Peticion.CodigoSubCliente,
                                                                                                           Peticion.CodigoPuntoServicio,
                                                                                                           Peticion.CodigoSectorPadre,
                                                                                                           Peticion.CodigoPlanta,
                                                                                                           Peticion.CodigoDelegacion,
                                                                                                           Peticion.CodigoSubCanal, True, False)



                    Case Enumeradores.TipoBuscaSaldo.ExpuestoMedioPago

                        ''Recupera o saldo a nivel de conta
                        Respuesta.Saldos = AccesoDatos.GenesisSaldos.SaldoEfectivo.RecuperarSaldoPorCuenta(Peticion.CodigosSectores,
                                                                                                           Peticion.CodigoCliente,
                                                                                                           Peticion.CodigoSubCliente,
                                                                                                           Peticion.CodigoPuntoServicio,
                                                                                                           Peticion.CodigoSectorPadre,
                                                                                                           Peticion.CodigoPlanta,
                                                                                                           Peticion.CodigoDelegacion,
                                                                                                           Peticion.CodigoSubCanal, True, True)
                    Case Enumeradores.TipoBuscaSaldo.TotalDisponibleSector

                        'Recupera o saldo a nivel de sector
                        Respuesta.Saldos = AccesoDatos.GenesisSaldos.SaldoEfectivo.RecuperarSaldoTotalEfectivoSector(Peticion.CodigosSectores,
                                                                                                                     Peticion.CodigoSectorPadre,
                                                                                                                     Peticion.CodigoPlanta,
                                                                                                                     Peticion.CodigoDelegacion, True, False)

                    Case Enumeradores.TipoBuscaSaldo.ElementoEnviarPrecintado

                        Respuesta.Saldos = AccesoDatos.Genesis.Remesa.RecuperarElementosPrecintados(Peticion.CodigosSectores,
                                                                                                    Peticion.CodigoCliente,
                                                                                                    Peticion.CodigoSubCliente,
                                                                                                    Peticion.CodigoPuntoServicio,
                                                                                                    Peticion.CodigoSectorPadre,
                                                                                                    Peticion.CodigoPlanta,
                                                                                                    Peticion.CodigoDelegacion,
                                                                                                    Peticion.CodigoSubCanal,
                                                                                                    Enumeradores.EstadoRemesa.Pendiente,
                                                                                                    Enumeradores.EstadoBulto.Cerrado,
                                                                                                    Peticion.BuscarBultosSinCuadre,
                                                                                                    Peticion.BuscarClienteTodosNiveis)

                    Case Enumeradores.TipoBuscaSaldo.Elemento

                        'Recupera os elementos
                        Respuesta.Saldos = RecuperarElementos(Peticion)

                        If Peticion.BuscaInfoAdicionalesElementos Then
                            'Recupera informações adicionais que serão utilizadas no Salidas
                            If Respuesta IsNot Nothing AndAlso Respuesta.Saldos IsNot Nothing AndAlso Respuesta.Saldos.Count > 0 Then
                                Dim identificadoresElementos As New List(Of String)
                                Respuesta.Saldos.Foreach(Sub(s)
                                                             If s IsNot Nothing AndAlso s.Elementos IsNot Nothing AndAlso s.Elementos.Count > 0 Then
                                                                 s.Elementos.Foreach(Sub(e)
                                                                                         If e IsNot Nothing AndAlso Not String.IsNullOrEmpty(e.Identificador) Then

                                                                                             If identificadoresElementos.Find(Function(ir) ir.Equals(e.Identificador)) Is Nothing Then
                                                                                                 identificadoresElementos.Add(e.Identificador)
                                                                                             End If

                                                                                             If CType(e, Clases.Remesa).Bultos IsNot Nothing AndAlso
                                                                                                 CType(e, Clases.Remesa).Bultos.Count > 0 Then

                                                                                                 For Each b In CType(e, Clases.Remesa).Bultos
                                                                                                     If identificadoresElementos.Find(Function(ib) ib.Equals(b.Identificador)) Is Nothing Then
                                                                                                         identificadoresElementos.Add(b.Identificador)
                                                                                                     End If
                                                                                                 Next

                                                                                             End If
                                                                                         End If
                                                                                     End Sub)
                                                             End If
                                                         End Sub)

                                If identificadoresElementos.Count > 0 Then
                                    Respuesta.InfoAdicionalesElementos = AccesoDatos.GenesisSaldos.DocumentoElemento.DocumentoElementoRecuperarInformacionesAdicionales(identificadoresElementos)
                                End If
                            End If
                        End If

                    Case Enumeradores.TipoBuscaSaldo.Ambos

                        'Recupera valores e elementos
                        Respuesta.Saldos = RetonarElementosYValores(Peticion)

                End Select

            Catch ex As Excepcion.NegocioExcepcion
                Respuesta.Mensajes.Add(ex.Descricao)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                Respuesta.Excepciones.Add(ex.Message)
            End Try

            Return Respuesta
        End Function

        ''' <summary>
        ''' Recupera os valors e os elementos
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function RetonarElementosYValores(Peticion As RecuperarSaldoExpuestoxDetallado.Peticion) As ObservableCollection(Of Clases.Saldo)


            Dim objSaldosRetorno As ObservableCollection(Of Clases.Saldo) = Nothing

            'Recupera os elementos.
            Dim objSaldosElementos As ObservableCollection(Of Clases.Saldo) = RecuperarElementos(Peticion)


            'Recupera os valores
            objSaldosRetorno = AccesoDatos.GenesisSaldos.SaldoEfectivo.RecuperarSaldoPorCuenta(Peticion.CodigosSectores,
                                                                                               Peticion.CodigoCliente,
                                                                                               Peticion.CodigoSubCliente,
                                                                                               Peticion.CodigoPuntoServicio,
                                                                                               Peticion.CodigoSectorPadre,
                                                                                               Peticion.CodigoPlanta,
                                                                                               Peticion.CodigoDelegacion,
                                                                                               Peticion.CodigoSubCanal, True, False)

            If objSaldosElementos IsNot Nothing AndAlso objSaldosElementos.Count > 0 Then

                Dim objSaldo As Clases.SaldoCuenta = Nothing
                If objSaldosRetorno Is Nothing Then objSaldosRetorno = New ObservableCollection(Of Clases.Saldo)

                For Each objSalEl In objSaldosElementos

                    objSaldo = (From objSR In objSaldosRetorno
                                Where DirectCast(objSR, Clases.SaldoCuenta).Cuenta IsNot Nothing AndAlso _
                                      DirectCast(objSR, Clases.SaldoCuenta).Cuenta.Identificador = DirectCast(objSalEl, Clases.SaldoCuenta).Cuenta.Identificador).FirstOrDefault

                    If objSaldo Is Nothing Then
                        objSaldosRetorno.Add(New Clases.SaldoCuenta With {.Cuenta = DirectCast(objSalEl, Clases.SaldoCuenta).Cuenta})

                        objSaldo = (From objSR In objSaldosRetorno
                               Where DirectCast(objSR, Clases.SaldoCuenta).Cuenta IsNot Nothing AndAlso _
                                     DirectCast(objSR, Clases.SaldoCuenta).Cuenta.Identificador = DirectCast(objSalEl, Clases.SaldoCuenta).Cuenta.Identificador).FirstOrDefault

                    End If

                    objSaldo.Elementos = objSalEl.Elementos

                Next

            End If

            Return objSaldosRetorno
        End Function

        ''' <summary>
        ''' Recupera os elementos
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function RecuperarElementos(Peticion As RecuperarSaldoExpuestoxDetallado.Peticion) As ObservableCollection(Of Clases.Saldo)

            Dim objSaldoCuentas As ObservableCollection(Of Clases.Saldo) = Nothing

            'Recupera os identificadores das remesas a serem recuperadas.
            Dim objIdentificadoresRemesasYBultos As List(Of Clases.Remesa) = _
                        AccesoDatos.Genesis.Remesa.RecuperarIdentificadoresRemesasYBultos(Peticion.CodigosSectores,
                                                                                           Peticion.CodigoCliente,
                                                                                           Peticion.CodigoSubCliente,
                                                                                           Peticion.CodigoPuntoServicio,
                                                                                           Peticion.CodigoSectorPadre,
                                                                                           Peticion.CodigoPlanta,
                                                                                           Peticion.CodigoDelegacion,
                                                                                           Peticion.CodigoSubCanal,
                                                                                           Enumeradores.EstadoRemesa.Pendiente, _
                                                                                           Enumeradores.EstadoBulto.Cerrado, _
                                                                                           Enumeradores.EstadoDocumento.Aceptado, _
                                                                                           Peticion.BuscarBultosSinCuadre, _
                                                                                           Peticion.BuscarClienteTodosNiveis)

            If objIdentificadoresRemesasYBultos IsNot Nothing AndAlso objIdentificadoresRemesasYBultos.Count > 0 Then

                objSaldoCuentas = New ObservableCollection(Of Clases.Saldo)()

                'Dim IdentificadoresRemesas As List(Of String) = (From objRem In objIdentificadoresRemesasYBultos Where (objRem.Bultos Is Nothing OrElse objRem.Bultos.Count = 0) Select objRem.Identificador).ToList
                'Dim IdentificadoresBultos As List(Of String) = (From objRem In objIdentificadoresRemesasYBultos, objBul In objRem.Bultos Where (objRem.Bultos IsNot Nothing AndAlso objRem.Bultos.Count > 0) Select objBul.Identificador).ToList

                'Recupera as Remesas
                RecuperarRemesas(objIdentificadoresRemesasYBultos, objSaldoCuentas)

                'Recupera os Bultos
                'RecuperarBultos(IdentificadoresBultos, objSaldoCuentas)

            End If

            Return objSaldoCuentas
        End Function

        ''' <summary>
        ''' Recupera as remesas
        ''' </summary>
        ''' <param name="remesas"></param>
        ''' <param name="objSaldoCuentas"></param>
        ''' <remarks></remarks>
        Private Shared Sub RecuperarRemesas(remesas As List(Of Clases.Remesa), _
                                            ByRef objSaldoCuentas As ObservableCollection(Of Clases.Saldo))

            'Recupera as remesas
            If remesas IsNot Nothing AndAlso remesas.Count > 0 Then

                Dim identificadoresRemesa As List(Of String) = New List(Of String)

                For Each remesa In remesas
                    identificadoresRemesa.Add(remesa.Identificador)
                Next

                Dim objRemesas As ObservableCollection(Of Clases.Remesa) = AccesoDatos.Genesis.Remesa.recuperarRemesas(identificadoresRemesa, Nothing, "RecuperarSaldoExpuestoxDetallado")

                If objRemesas IsNot Nothing AndAlso objRemesas.Count > 0 Then

                    If objSaldoCuentas Is Nothing Then objSaldoCuentas = New ObservableCollection(Of Clases.Saldo)

                    Dim objSaldoCuenta As Clases.SaldoCuenta = Nothing

                    For Each objR In objRemesas

                        objSaldoCuenta = (From objSC In objSaldoCuentas Where DirectCast(objSC, Clases.SaldoCuenta).Cuenta IsNot Nothing _
                                                                              AndAlso DirectCast(objSC, Clases.SaldoCuenta).Cuenta.Identificador = objR.Cuenta.Identificador).FirstOrDefault

                        If objSaldoCuenta Is Nothing Then

                            objSaldoCuentas.Add(New Clases.SaldoCuenta With {.Cuenta = objR.Cuenta, _
                                                                             .Elementos = New ObservableCollection(Of Clases.Elemento)})

                            objSaldoCuenta = (From objSC In objSaldoCuentas Where DirectCast(objSC, Clases.SaldoCuenta).Cuenta IsNot Nothing _
                                                                              AndAlso DirectCast(objSC, Clases.SaldoCuenta).Cuenta.Identificador = objR.Cuenta.Identificador).FirstOrDefault

                        End If

                        objSaldoCuenta.Elementos.Add(objR)
                    Next

                End If

            End If

        End Sub

#End Region

#Region "RecuperarSaldos"


        Public Shared Function ObtenerSaldos(_peticion As Prosegur.Genesis.Comon.Peticion(Of Clases.Sector), _
                                             _filtros As Clases.Transferencias.FiltroConsultaSaldo) As Comon.Respuesta(Of List(Of DataRow))

            Dim _respuesta As New Comon.Respuesta(Of List(Of DataRow))

            Try

                Select Case _filtros.DiscriminarPor

                    'Verifica se é para recuperar o saldo por conta
                    Case Enumeradores.DiscriminarPor.Cuenta

                        'Recupera o saldo a nivel de conta
                        _respuesta.Retorno = AccesoDatos.GenesisSaldos.Saldo.ObtenerSaldoPorCuenta(_peticion, _respuesta, _filtros)
                    Case Enumeradores.DiscriminarPor.Sector

                        'Recupera o saldo a nivel de sector
                        _respuesta.Retorno = AccesoDatos.GenesisSaldos.Saldo.ObtenerSaldoPorSector(_peticion, _respuesta, _filtros)
                    Case Enumeradores.DiscriminarPor.ClienteyCanal

                        'Recupera o saldo a nivel de cliente e canal
                        _respuesta.Retorno = AccesoDatos.GenesisSaldos.Saldo.ObtenerSaldoPorClienteyCanal(_peticion, _respuesta, _filtros)
                End Select

            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try

            Return _respuesta

        End Function

        Public Shared Function ObtenerSaldoTotal(_filtros As Clases.Transferencias.FiltroConsultaSaldo) As DataTable
            Try
                Return AccesoDatos.GenesisSaldos.Saldo.ObtenerSaldoTotal(_filtros)
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try
        End Function

        Public Shared Function ObtenerSaldoTotalSinCanalSF(_filtros As Clases.Transferencias.FiltroConsultaSaldo) As DataTable
            Try
                Return AccesoDatos.GenesisSaldos.Saldo.ObtenerSaldoTotalSinCanalSF(_filtros)
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try
        End Function

        Public Shared Function obtenerSaldoCuentas_v2(Filtro As Comon.Clases.Transferencias.Filtro, _
                               Optional ByRef log As StringBuilder = Nothing) As ObservableCollection(Of Comon.Clases.SaldoCuenta)

            ' Validaciones
            ValidarDatosObligatorio(Filtro)

            Return AccesoDatos.GenesisSaldos.Saldo.obtenerSaldoCuentas_v2(Filtro, log)

        End Function


        ''' <summary>
        ''' Recupera el saldos de las cuentas, de acuerdo con el Filtro
        ''' </summary>
        ''' <param name="Filtro">Filtro para búsqueda</param>
        ''' <param name="esDisponibleNoDefinido">Define si es para cambiar solamente los valores disponibles para NoDefinido</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function obtenerSaldoCuentas(Filtro As Comon.Clases.Transferencias.Filtro, _
                                       Optional esDisponibleNoDefinido As Boolean = False, _
                                       Optional BuscarValoresDisponibles As Nullable(Of Boolean) = Nothing,
                                       Optional ByRef log As StringBuilder = Nothing) As ObservableCollection(Of Comon.Clases.SaldoCuenta)

            Dim TiempoInicial As DateTime = Now
            Dim Tiempo As DateTime = Now

            Dim objSaldoCuentasDetalle As New ObservableCollection(Of Comon.Clases.SaldoCuentaDetalle)
            Dim objSaldoCuentas As New ObservableCollection(Of Comon.Clases.SaldoCuenta)

            Dim logValidacion As New StringBuilder
            Dim logConsultaPrincipal As New StringBuilder
            Dim logCargar As New StringBuilder
            Dim logCuentas As New StringBuilder
            Dim logDivisas As New StringBuilder
            Dim logUnidadMedida As New StringBuilder
            Dim logCalidad As New StringBuilder

            Try

                ' Validaciones
                ValidarDatosObligatorio(Filtro)
                logValidacion.AppendLine("____Tiempo 'ValidarDatosObligatorio': " & Now.Subtract(Tiempo).ToString() & "; ")

                Tiempo = Now
                Dim tdCuentas As DataTable = AccesoDatos.GenesisSaldos.Saldo.obtenerSaldoCuentas(Filtro, esDisponibleNoDefinido, BuscarValoresDisponibles)
                logConsultaPrincipal.AppendLine("____Tiempo 'obtenerSaldoCuentas': " & Now.Subtract(Tiempo).ToString() & "; ")

                If tdCuentas IsNot Nothing AndAlso tdCuentas.Rows.Count > 0 Then

                    Dim identificadoresCuentas As List(Of String) = tdCuentas.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_CUENTA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_CUENTA"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_CUENTA")) _
                                                                         .Distinct() _
                                                                         .ToList()

                    ' Divisa
                    Dim identificadoresDivisas As List(Of String) = tdCuentas.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_DIVISA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DIVISA"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_DIVISA")) _
                                                                         .Distinct() _
                                                                         .ToList()

                    Dim identificadoresDenominaciones As List(Of String) = tdCuentas.AsEnumerable() _
                                                                             .Where(Function(r) r.Field(Of String)("OID_DENOMINACION") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DENOMINACION"))) _
                                                                             .Select(Function(r) r.Field(Of String)("OID_DENOMINACION")) _
                                                                             .Distinct() _
                                                                             .ToList()

                    Dim identificadoresMediosPagos As List(Of String) = tdCuentas.AsEnumerable() _
                                                                             .Where(Function(r) r.Field(Of String)("OID_MEDIO_PAGO") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_MEDIO_PAGO"))) _
                                                                             .Select(Function(r) r.Field(Of String)("OID_MEDIO_PAGO")) _
                                                                             .Distinct() _
                                                                             .ToList()

                    Dim identificadoresUnidadMedida As List(Of String) = tdCuentas.AsEnumerable() _
                                                                             .Where(Function(r) r.Field(Of String)("OID_UNIDAD_MEDIDA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_UNIDAD_MEDIDA"))) _
                                                                             .Select(Function(r) r.Field(Of String)("OID_UNIDAD_MEDIDA")) _
                                                                             .Distinct() _
                                                                             .ToList()

                    Dim identificadoresCalidad As List(Of String) = tdCuentas.AsEnumerable() _
                                                                             .Where(Function(r) r.Field(Of String)("OID_CALIDAD") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_CALIDAD"))) _
                                                                             .Select(Function(r) r.Field(Of String)("OID_CALIDAD")) _
                                                                             .Distinct() _
                                                                             .ToList()

                    Dim divisasPosibles As ObservableCollection(Of Clases.Divisa) = Nothing
                    Dim cuentasPosibles As ObservableCollection(Of Clases.Cuenta) = Nothing
                    Dim dtUnidadMedida As DataTable = Nothing
                    Dim dtCalidad As DataTable = Nothing

                    ' DivisasPosibles
                    Dim TDivisas As New Task(Sub()
                                                 Dim TiempoDivisas As DateTime = Now
                                                 divisasPosibles = AccesoDatos.Genesis.Divisas.ObtenerDivisas_v2(Nothing, identificadoresDivisas, Nothing, identificadoresDenominaciones, Nothing, identificadoresMediosPagos)
                                                 logDivisas.AppendLine("____Tiempo 'ObtenerDivisas_v2': " & Now.Subtract(TiempoDivisas).ToString() & "; ")
                                             End Sub)
                    TDivisas.Start()

                    ' CuentasPosibles                    
                    Dim TCuentas As New Task(Sub()
                                                 Dim TiempoCuentas As DateTime = Now
                                                 cuentasPosibles = AccesoDatos.GenesisSaldos.Cuenta.ObtenerCuentasPorIdentificadores(identificadoresCuentas, Enumeradores.TipoCuenta.Saldo, "ObtenerDatosDeLosDocumentos", logCuentas)
                                                 logCuentas.AppendLine("____Tiempo 'ObtenerCuentasPorIdentificadores_v2': " & Now.Subtract(TiempoCuentas).ToString() & "; ")
                                             End Sub)
                    TCuentas.Start()

                    Dim TUnidadMedida As New Task(Sub()
                                                      Dim TiempoUnidadMedida As DateTime = Now
                                                      dtUnidadMedida = AccesoDatos.Genesis.UnidadMedida.ObtenerUnidadMedidaPorDivisa_v2(Nothing, identificadoresUnidadMedida)
                                                      logUnidadMedida.AppendLine("____Tiempo 'ObtenerUnidadMedidaPorDivisa_v2': " & Now.Subtract(TiempoUnidadMedida).ToString() & "; ")
                                                  End Sub)
                    TUnidadMedida.Start()

                    Dim TCalidad As New Task(Sub()
                                                 Dim TiempoCalidad As DateTime = Now
                                                 dtCalidad = AccesoDatos.Genesis.Calidad.ObtenerCalidadPorDivisa_v2(Nothing, identificadoresCalidad)
                                                 logCalidad.AppendLine("____Tiempo 'ObtenerCalidadPorDivisa_v2': " & Now.Subtract(TiempoCalidad).ToString() & "; ")
                                             End Sub)
                    TCalidad.Start()

                    Task.WaitAll(New Task() {TCuentas, TDivisas, TUnidadMedida, TCalidad})

                    Tiempo = Now
                    For Each objCuenta As Clases.Cuenta In cuentasPosibles

                        Dim result = From d In tdCuentas.AsEnumerable() _
                                         Where d.Field(Of String)("OID_CUENTA") = objCuenta.Identificador
                                         Select d

                        Dim importesCuenta = result.CopyToDataTable()

                        If importesCuenta IsNot Nothing AndAlso importesCuenta.Rows.Count > 0 Then

                            'Busca as divisas da conta
                            Dim objDivisa As ObservableCollection(Of Clases.Divisa) = LogicaNegocio.Genesis.Divisas.cargarDivisas(importesCuenta, divisasPosibles, False, esDisponibleNoDefinido, Nothing, dtUnidadMedida, dtCalidad)

                            'Para cada divisa da conta será criada uma conta de detalle
                            For Each div In objDivisa
                                Dim saldoCuentaDetalle As New Clases.SaldoCuentaDetalle
                                saldoCuentaDetalle.Cuenta = objCuenta
                                saldoCuentaDetalle.Divisas = New ObservableCollection(Of Clases.Divisa)
                                saldoCuentaDetalle.Divisas.Add(div)
                                objSaldoCuentasDetalle.Add(saldoCuentaDetalle)
                            Next
                        End If
                    Next

                    logCargar.AppendLine("____Tiempo 'cargarObjetos': " & Now.Subtract(Tiempo).ToString() & "; ")
                End If

                'Agrupa os saldos das contas por CLIENTE,SUBCLIENTE,PUNTO_SERVICIO e SUBCANAL
                For Each grupo In (From cd In objSaldoCuentasDetalle Group By oid_cliente = cd.Cuenta.Cliente.Identificador,
                                        oid_subCliente = If(cd.Cuenta.SubCliente IsNot Nothing, cd.Cuenta.SubCliente.Identificador, Nothing),
                                        oid_puntoServicio = If(cd.Cuenta.PuntoServicio IsNot Nothing, cd.Cuenta.PuntoServicio.Identificador, Nothing),
                                        oid_subCanal = If(cd.Cuenta.SubCanal IsNot Nothing, cd.Cuenta.SubCanal.Identificador, Nothing),
                                        codigoISO = cd.Divisas.First.CodigoISO
                                     Into Group
                                     Select oid_cliente, oid_subCliente, oid_puntoServicio, oid_subCanal, codigoISO)

                    'Recupera todas as contas para esse grupo
                    Dim lstSaldoCuentaDetalle = objSaldoCuentasDetalle.Where(Function(a) a.Cuenta.Cliente.Identificador = grupo.oid_cliente _
                                                                        AndAlso If(a.Cuenta.SubCliente IsNot Nothing, a.Cuenta.SubCliente.Identificador, Nothing) = grupo.oid_subCliente _
                                                                        AndAlso If(a.Cuenta.PuntoServicio IsNot Nothing, a.Cuenta.PuntoServicio.Identificador, Nothing) = grupo.oid_puntoServicio _
                                                                        AndAlso a.Divisas.Exists(Function(d) d.CodigoISO = grupo.codigoISO) _
                                                                        AndAlso a.Cuenta.SubCanal.Identificador = grupo.oid_subCanal).ToList

                    Dim divisas As New ObservableCollection(Of Clases.Divisa)
                    For Each saldoCuentaDetalle In lstSaldoCuentaDetalle
                        'pega todas as divisas para somar
                        divisas.AddRange(saldoCuentaDetalle.Divisas.Clonar)
                    Next

                    'soma as divisas
                    Comon.Util.UnificaItemsDivisas(divisas)

                    Dim objSaldoCuenta As New Clases.SaldoCuenta
                    objSaldoCuenta.Cuenta = lstSaldoCuentaDetalle.First.Cuenta.Clonar
                    objSaldoCuenta.Divisas = divisas
                    objSaldoCuenta.SaldoCuentaDetalle = lstSaldoCuentaDetalle
                    objSaldoCuentas.Add(objSaldoCuenta)
                Next

            Catch ex As Exception
                Throw
            End Try

            If log IsNot Nothing Then
                log.Append(logValidacion)
                log.Append(logConsultaPrincipal)
                log.Append(logCuentas)
                log.Append(logDivisas)
                log.Append(logCalidad)
                log.Append(logUnidadMedida)
                log.Append(logCargar)
                log.AppendLine("Tiempo totalle: " & Now.Subtract(TiempoInicial).ToString() & "; ")
            End If

            Return objSaldoCuentas
        End Function

        Public Shared Function RecuperarSaldoCuentasDetallado(Filtro As Comon.Clases.Transferencias.Filtro, _
                                       Optional esDisponibleNoDefinido As Boolean = False, _
                                       Optional BuscarValoresDisponibles As Nullable(Of Boolean) = Nothing) As List(Of DataRow)

            Try

                ValidarDatosObligatorio(Filtro)

                'OLD == Dim objCuentas As List(Of DataRow) = AccesoDatos.GenesisSaldos.Saldo.RecuperarSaldoCuentas(Filtro, esDisponibleNoDefinido, BuscarValoresDisponibles)
                Dim tdCuentas As DataTable = AccesoDatos.GenesisSaldos.Saldo.RecuperarSaldoCuentasDetallado(Filtro, esDisponibleNoDefinido, BuscarValoresDisponibles)
                Return tdCuentas.Select().ToList()

            Catch ex As Exception
                Throw
            End Try

        End Function
        ''' <summary>
        ''' Valida os dados obrigatórios
        ''' </summary>
        ''' <param name="Filtro"></param>
        ''' <remarks></remarks>
        Private Shared Sub ValidarDatosObligatorio(Filtro As Comon.Clases.Transferencias.Filtro)

            Dim erros As New System.Text.StringBuilder

            ' Valida la información de Sector
            If Filtro Is Nothing OrElse Filtro.Sector Is Nothing OrElse String.IsNullOrEmpty(Filtro.Sector.Identificador) Then
                erros.AppendLine(Traduzir("040_validar_sector"))
            End If

            If erros.Length > 0 Then
                Throw New Excepcion.CampoObrigatorioException(erros.ToString)
            End If

        End Sub

        Public Shared Function OrdenarDivisasPorCodigoISODefectoEAlfabeto(divisas As ObservableCollection(Of Clases.Divisa), codigoIsoDefecto As String) As ObservableCollection(Of Clases.Divisa)

            If divisas IsNot Nothing AndAlso divisas.Count > 0 Then

                divisas = New ObservableCollection(Of Clases.Divisa)(divisas.OrderBy(Function(item) item.Descripcion))

                Dim divisaDefecto = (From div In divisas
                                       Where div.CodigoISO = codigoIsoDefecto
                                       Select div).FirstOrDefault

                If divisaDefecto IsNot Nothing Then
                    divisas.Move(divisas.IndexOf(divisaDefecto), 0)
                End If

                Return divisas

            End If

            Return Nothing

        End Function

        Public Shared Function ObtenerSaldoModificar(cuenta As Clases.Cuenta, Optional utilizarClienteTotalizador As Boolean = True) As ObservableCollection(Of Clases.Divisa)

            Try
                Dim divisas As ObservableCollection(Of Clases.Divisa) = AccesoDatos.GenesisSaldos.Saldo.ObtenerSaldoModificar(cuenta, utilizarClienteTotalizador)

                Comon.Util.UnificaItemsDivisas(divisas)

                Return divisas

            Catch ex As Excepcion.NegocioExcepcion
                Throw

            Catch ex As Exception
                Throw

            End Try

        End Function

        Public Shared Function ObtenerSaldoAnterior(identificadorDocumento As String, modo As Enumeradores.Modo) As ObservableCollection(Of Clases.Divisa)

            Try
                Dim divisas As ObservableCollection(Of Clases.Divisa) = AccesoDatos.GenesisSaldos.Saldo.ObtenerSaldoAnterior(identificadorDocumento, modo)

                Comon.Util.UnificaItemsDivisas(divisas)

                Return divisas

            Catch ex As Excepcion.NegocioExcepcion
                Throw

            Catch ex As Exception
                Throw

            End Try

        End Function

        Public Shared Function ObtenerSaldoAnteriorModificar(identificadorDocumento As String) As ObservableCollection(Of Clases.Divisa)

            Try
                Dim divisas As ObservableCollection(Of Clases.Divisa) = AccesoDatos.GenesisSaldos.Saldo.ObtenerSaldoAnteriorModificar(identificadorDocumento)

                Comon.Util.UnificaItemsDivisas_v2(divisas)

                Return divisas

            Catch ex As Excepcion.NegocioExcepcion
                Throw

            Catch ex As Exception
                Throw

            End Try

        End Function

        Public Shared Function validarSaldoEfectivoAnteriorXAtual(identificadorDocumento As String, identificadorCuentaSaldos As String) As String

            Dim respuesta As String

            Respuesta = AccesoDatos.GenesisSaldos.Saldo.validarSaldoEfectivoAnteriorXAtual(identificadorDocumento, identificadorCuentaSaldos)
            respuesta &= "|" & AccesoDatos.GenesisSaldos.Saldo.validarSaldoMedioPagoAnteriorXAtual(identificadorDocumento, identificadorCuentaSaldos)

            Return Respuesta

        End Function


        Public Shared Function ObtenerCuadreSaldoAtualXAnterior(identificadorDocumento As String, identificadorCuenta As String, cuenta As Clases.Cuenta) As ObservableCollection(Of Clases.Divisa)

            Try
                Dim divisas As ObservableCollection(Of Clases.Divisa) = AccesoDatos.GenesisSaldos.Saldo.ObtenerCuadreSaldoAtualXAnterior(identificadorDocumento, identificadorCuenta, cuenta)

                Comon.Util.UnificaItemsDivisas(divisas)

                Return divisas

            Catch ex As Excepcion.NegocioExcepcion
                Throw

            Catch ex As Exception
                Throw

            End Try

        End Function

#End Region

#Region "Dashboard"
        ''' <summary>
        ''' Retorna datos para grafico que mostrará los valores disponibles o no de efectivo para cada sector de una delegación
        ''' </summary>
        Public Shared Function RetornaSaldoDisponivelEfetivo(Peticion As RetornaSaldoDisponivelEfetivo.Peticion) As RetornaSaldoDisponivelEfetivo.Respuesta
            Dim objRespuesta = New RetornaSaldoDisponivelEfetivo.Respuesta

            Try
                Dim dt = AccesoDatos.GenesisSaldos.Saldo.RetornaSaldoDisponivelEfetivo(Peticion.CodigosDelegacao, Peticion.IdentificadoresDivisa, Peticion.Disponible, Peticion.CodigosSector)

                If (Not IsNothing(dt) AndAlso dt.Rows.Count > 0) Then
                    objRespuesta.Dados = New List(Of RetornaSaldoDisponivelEfetivo.Dados)

                    For Each dr As DataRow In dt.Rows
                        Dim dados = New RetornaSaldoDisponivelEfetivo.Dados()
                        dados.CodigoDelegacao = dr("COD_DELEGACION")
                        dados.CodigoSetor = dr("COD_SECTOR")
                        dados.DescricaoSetor = dr("DES_SECTOR")
                        dados.TipoValor = dr("TIPO_VALOR")
                        dados.CodigoIsoDivisa = dr("COD_ISO_DIVISA")
                        dados.Saldo = dr("SALDO")
                        objRespuesta.Dados.Add(dados)
                    Next
                End If
            Catch ex As ArgumentException
                objRespuesta.Excepciones.Add(ex.Message)
            End Try

            Return objRespuesta
        End Function

        ''' <summary>
        ''' Retorna datos para grafico que mostrará los valores disponibles o no de medio pago para cada sector de una delegación
        ''' </summary>
        Public Shared Function RetornaSaldoDisponivelMedioPago(Peticion As RetornaSaldoDisponivelMedioPago.Peticion) As RetornaSaldoDisponivelMedioPago.Respuesta
            Dim objRespuesta = New RetornaSaldoDisponivelMedioPago.Respuesta

            Try
                Dim dt = AccesoDatos.GenesisSaldos.Saldo.RetornaSaldoDisponivelMedioPago(Peticion.CodigosDelegacao, Peticion.IdentificadoresDivisa, Peticion.Disponible)

                If (Not IsNothing(dt) AndAlso dt.Rows.Count > 0) Then
                    objRespuesta.Dados = New List(Of RetornaSaldoDisponivelMedioPago.Dados)

                    For Each dr As DataRow In dt.Rows
                        Dim dados = New RetornaSaldoDisponivelMedioPago.Dados()
                        dados.CodigoDelegacao = dr("COD_DELEGACION")
                        dados.CodigoSetor = dr("COD_SECTOR")
                        dados.DescricaoSetor = dr("DES_SECTOR")
                        dados.TipoValor = dr("TIPO_VALOR")
                        dados.CodigoIsoDivisa = dr("COD_ISO_DIVISA")
                        dados.Saldo = dr("SALDO")
                        objRespuesta.Dados.Add(dados)
                    Next
                End If
            Catch ex As ArgumentException
                objRespuesta.Excepciones.Add(ex.Message)
            End Try

            Return objRespuesta
        End Function

        ''' <summary>
        ''' Retorna datos para grafico que mostrará los valores que se encuentren dentro de una delegación para un cliente. 
        ''' Los valores se encontrarán discriminados por Disponible / No Disponible /  Efectivo / Medio de Pago
        ''' </summary>
        Public Shared Function RetornaSaldosCliente(Peticion As RetornaSaldosCliente.Peticion) As RetornaSaldosCliente.Respuesta
            Dim objRespuesta = New RetornaSaldosCliente.Respuesta

            Try
                Dim dt = AccesoDatos.GenesisSaldos.Saldo.RetornaSaldosCliente(Peticion.IdentificadorCliente, Peticion.IdentificadoresDivisa, Peticion.CodigosSector)

                If (Not IsNothing(dt) AndAlso dt.Rows.Count > 0) Then
                    objRespuesta.Dados = New List(Of RetornaSaldosCliente.Dados)

                    For Each dr As DataRow In dt.Rows
                        Dim dados = New RetornaSaldosCliente.Dados()
                        dados.CodigoCliente = dr("COD_CLIENTE")
                        dados.DescricaoCliente = dr("DES_CLIENTE")
                        dados.DescricaoDivisa = dr("DES_DIVISA")
                        dados.TipoValor = dr("TIPO_VALOR")
                        dados.Saldo = dr("SALDO")
                        objRespuesta.Dados.Add(dados)
                    Next
                End If
            Catch ex As ArgumentException
                objRespuesta.Excepciones.Add(ex.Message)
            End Try

            Return objRespuesta
        End Function

        ''' <summary>
        ''' Retorna datos para grafico que mostrará el billetaje por sector para la divisa seleccionada para un sector específico
        ''' </summary>
        Public Shared Function RetornaBilletajexSector(Peticion As RetornaBilletajexSector.Peticion) As RetornaBilletajexSector.Respuesta
            Dim objRespuesta = New RetornaBilletajexSector.Respuesta

            Try
                Dim dt = AccesoDatos.GenesisSaldos.Saldo.RetornaBilletajexSector(Peticion.CodigosSector, Peticion.IdentificadoresDivisa)

                If (Not IsNothing(dt) AndAlso dt.Rows.Count > 0) Then
                    objRespuesta.Dados = New List(Of RetornaBilletajexSector.Dados)

                    For Each dr As DataRow In dt.Rows
                        Dim dados = New RetornaBilletajexSector.Dados()
                        dados.DescricaoDivisa = dr("DES_DIVISA")
                        dados.DescricaoDenominacao = dr("DES_DENOMINACION")
                        dados.TipoValor = dr("TIPO_VALOR")
                        dados.Disponivel = dr("BOL_DISPONIBLE")
                        dados.Saldo = dr("SALDO")
                        objRespuesta.Dados.Add(dados)
                    Next
                End If
            Catch ex As ArgumentException
                objRespuesta.Excepciones.Add(ex.Message)
            End Try

            Return objRespuesta
        End Function

        ''' <summary>
        ''' Retorna datos para grafico que mostrará los primeros 10 clientes con más saldo disponible atesorado en la compañía
        ''' </summary>
        Public Shared Function RetornaRankingClientes(Peticion As RetornaRankingClientes.Peticion) As RetornaRankingClientes.Respuesta
            Dim objRespuesta = New RetornaRankingClientes.Respuesta

            Try
                Dim dt = AccesoDatos.GenesisSaldos.Saldo.RetornaRankingClientes(Peticion.CodigosDelegacao, Peticion.IdentificadoresDivisa, Peticion.CodigosSector)

                If (Not IsNothing(dt) AndAlso dt.Rows.Count > 0) Then
                    objRespuesta.Dados = New List(Of RetornaRankingClientes.Dados)

                    For Each dr As DataRow In dt.Rows
                        Dim dados = New RetornaRankingClientes.Dados()
                        dados.IdentificadorCliente = dr("OID_CLIENTE")
                        dados.CodigoCliente = dr("COD_CLIENTE")
                        dados.DescricaoCliente = dr("DES_CLIENTE")
                        dados.SumaEfetivo = dr("SUM_EFE_DIS")
                        If Not IsDBNull(dr("EFE_DIS")) Then
                            dados.EfetivoDisponivel = dr("EFE_DIS")
                        End If
                        If Not IsDBNull(dr("EFE_NDI")) Then
                            dados.EfetivoNaoDisponivel = dr("EFE_NDI")
                        End If


                        objRespuesta.Dados.Add(dados)
                    Next
                End If
            Catch ex As ArgumentException
                objRespuesta.Excepciones.Add(ex.Message)
            End Try

            Return objRespuesta
        End Function
#End Region

    End Class

End Namespace