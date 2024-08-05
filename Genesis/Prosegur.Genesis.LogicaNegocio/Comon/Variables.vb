Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel

Public Class Variables2

    'Private Shared _formularios As New ObservableCollection(Of Clases.Formulario)
    'Public Shared Function ObtenerFormularioPorCaracteristicas(objCaracteristicasFormulario As List(Of Enumeradores.CaracteristicaFormulario)) As Clases.Formulario

    '    Dim formulario As Clases.Formulario = Nothing

    '    If _formularios IsNot Nothing AndAlso _formularios.Count > 0 Then

    '        SyncLock _formularios

    '            For Each f In _formularios

    '                Dim obtenerCaracteristicas As List(Of Enumeradores.CaracteristicaFormulario) = objCaracteristicasFormulario.Clonar()

    '                For Each c In objCaracteristicasFormulario

    '                    If f.Caracteristicas.Contains(c) Then

    '                        obtenerCaracteristicas.Remove(c)

    '                    End If

    '                Next
    '                If objCaracteristicasFormulario IsNot Nothing Then
    '                    formulario = f
    '                    Exit For
    '                End If

    '            Next

    '        End SyncLock

    '    End If

    '    If formulario Is Nothing Then

    '        Dim posiblesFormularios As ObservableCollection(Of Clases.Formulario) = LogicaNegocio.GenesisSaldos.MaestroFormularios.ObtenerFormulariosPorCaracteristicas_v2(objCaracteristicasFormulario)

    '        If (posiblesFormularios IsNot Nothing AndAlso posiblesFormularios.Count > 0) Then

    '            SyncLock _formularios
    '                _formularios.Add(posiblesFormularios.FirstOrDefault)
    '            End SyncLock

    '            formulario = posiblesFormularios.FirstOrDefault()

    '        End If

    '    End If

    '    Return formulario
    'End Function

    'Public Shared Function ObtenerFormularioPorCodigo(codigoFormulario As String) As Clases.Formulario

    '    Dim formulario As Clases.Formulario = Nothing

    '    If _formularios IsNot Nothing AndAlso _formularios.Count > 0 Then
    '        SyncLock _formularios
    '            formulario = _formularios.FirstOrDefault(Function(x) x.Codigo = codigoFormulario)
    '        End SyncLock
    '    End If

    '    If formulario Is Nothing Then

    '        formulario = LogicaNegocio.GenesisSaldos.MaestroFormularios.ObtenerFormularioPorCodigo(codigoFormulario)

    '        If (formulario IsNot Nothing) Then
    '            SyncLock _formularios
    '                _formularios.Add(formulario)
    '            End SyncLock
    '        End If

    '    End If

    '    Return formulario
    'End Function

    'Private Shared _parametros As New List(Of Clases.Parametro)
    'Public Shared Function ObtenerParametrosDelegacionPais(codigoDelegacion As String, CodigoAplicacion As String, listaParametros As List(Of String)) As List(Of Clases.Parametro)
    '    Dim parametros As New List(Of Clases.Parametro)
    '    Dim listaParametrosAux As New List(Of String)

    '    If _parametros IsNot Nothing AndAlso _parametros.Count > 0 Then
    '        SyncLock _parametros
    '            For Each p In listaParametros
    '                Dim par As Clases.Parametro = _parametros.Find(Function(x) x.CodigoParametro = p AndAlso x.CodigoDelegacion = codigoDelegacion AndAlso x.CodigoAplicacion = CodigoAplicacion)
    '                If par IsNot Nothing Then
    '                    parametros.Add(par)
    '                Else
    '                    listaParametrosAux.Add(p)
    '                End If
    '            Next
    '        End SyncLock

    '    Else
    '        listaParametrosAux = listaParametros
    '    End If

    '    If listaParametrosAux IsNot Nothing AndAlso listaParametrosAux.Count > 0 Then

    '        Dim parametrosBase As List(Of Clases.Parametro) = AccesoDatos.Genesis.Parametros.ObtenerParametrosDelegacionPais(codigoDelegacion, CodigoAplicacion, listaParametrosAux)
    '        For Each p In parametrosBase
    '            p.CodigoAplicacion = CodigoAplicacion
    '            p.CodigoDelegacion = codigoDelegacion

    '            SyncLock _parametros
    '                _parametros.Add(p)
    '            End SyncLock

    '            parametros.Add(p)
    '        Next

    '    End If

    '    If parametros.Count = 0 Then
    '        Return Nothing
    '    End If

    '    Return parametros
    'End Function

    'Private Shared _divisas As New ObservableCollection(Of Clases.Divisa)
    'Public Shared Function ObtenerDivisas(codigoIso As String) As Clases.Divisa
    '    Dim divisa As Clases.Divisa = Nothing

    '    If _divisas IsNot Nothing AndAlso _divisas.Count > 0 Then
    '        SyncLock _divisas
    '            divisa = _divisas.FirstOrDefault(Function(x) x.CodigoISO = codigoIso)
    '        End SyncLock
    '    End If

    '    If divisa Is Nothing Then
    '        divisa = LogicaNegocio.Genesis.Divisas.ObtenerDivisas(Nothing, New ObservableCollection(Of String) From {codigoIso}).FirstOrDefault

    '        SyncLock _divisas
    '            _divisas.Add(divisa)
    '        End SyncLock

    '    End If

    '    Return divisa.Clonar
    'End Function
    'Public Shared Function ObtenerDivisaPorCodigoDenominacion(codigoDenominacion As String) As Clases.Divisa
    '    Dim divisa As Clases.Divisa = Nothing

    '    If _divisas IsNot Nothing AndAlso _divisas.Count > 0 Then

    '        SyncLock _divisas
    '            For Each d In _divisas
    '                If d.Denominaciones.FirstOrDefault(Function(x) x.Codigo = codigoDenominacion) IsNot Nothing Then
    '                    divisa = d
    '                    Exit For
    '                End If
    '            Next
    '        End SyncLock

    '    End If

    '    If divisa Is Nothing Then
    '        divisa = AccesoDatos.Genesis.Divisas.ObtenerDivisaPorCodigoDenominacion(codigoDenominacion, True, True)
    '        SyncLock _divisas
    '            _divisas.Add(divisa)
    '        End SyncLock
    '    End If

    '    Return divisa.Clonar
    'End Function

    'Private Shared _clientes As New ObservableCollection(Of Clases.Cliente)
    'Public Shared Function ObtenerCliente(codigoCliente As String) As Clases.Cliente
    '    Dim cliente As Clases.Cliente = Nothing

    '    If _clientes IsNot Nothing AndAlso _clientes.Count > 0 Then
    '        SyncLock _clientes
    '            cliente = _clientes.FirstOrDefault(Function(x) x.Codigo = codigoCliente)
    '        End SyncLock
    '    End If

    '    If cliente Is Nothing Then
    '        cliente = Prosegur.Genesis.AccesoDatos.Genesis.Cliente.ObtenerCliente(codigoCliente)
    '        SyncLock _clientes
    '            _clientes.Add(cliente)
    '        End SyncLock
    '    End If

    '    Return cliente
    'End Function

    'Private Shared _cuentas As New ObservableCollection(Of Clases.Cuenta)
    'Public Shared Function ObtenerCuenta(CodigoCliente As String, _
    '                                     CodigoSubCliente As String, _
    '                                     CodigoPuntoServicio As String, _
    '                                     CodigoCanal As String, _
    '                                     CodigoSubCanal As String, _
    '                                     CodigoSector As String, _
    '                                     CodigoDelegacion As String, _
    '                                     CodigoPlanta As String, _
    '                                     DescripcionUsuario As String, _
    '                                     TipoCuenta As Enumeradores.TipoCuenta) As Clases.Cuenta

    '    Dim cuenta As Clases.Cuenta = Nothing

    '    'If _cuentas IsNot Nothing AndAlso _cuentas.Count > 0 Then

    '    '    SyncLock _cuentas
    '    '        For Each c In _cuentas


    '    '            cuenta = _cuentas.FirstOrDefault(Function(x) x.Canal.Codigo = CodigoCanal AndAlso x.SubCanal.Codigo = CodigoSubCanal AndAlso _
    '    '                               x.Cliente.Codigo = CodigoCliente AndAlso x.Sector.Codigo = CodigoSector AndAlso x.Sector.Planta.Codigo = CodigoPlanta AndAlso _
    '    '                               x.Sector.Delegacion.Codigo = CodigoDelegacion AndAlso (x.TipoCuenta = Enumeradores.TipoCuenta.Ambos OrElse x.TipoCuenta = TipoCuenta))



    '    '            If cuenta IsNot Nothing Then
    '    '                Exit For
    '    '            End If
    '    '        Next
    '    '    End SyncLock
    '    'End If

    '    'If cuenta Is Nothing Then

    '    '    If TipoCuenta = Enumeradores.TipoCuenta.Movimiento Then

    '    '        cuenta = LogicaNegocio.Genesis.MaestroCuenta.ObtenerCuentaMovimiento(CodigoCliente, _
    '    '                                                                             CodigoSubCliente, _
    '    '                                                                             CodigoPuntoServicio, _
    '    '                                                                             CodigoCanal, _
    '    '                                                                             CodigoSubCanal, _
    '    '                                                                             CodigoSector, _
    '    '                                                                             CodigoDelegacion, _
    '    '                                                                             CodigoPlanta, _
    '    '                                                                             DescripcionUsuario)

    '    '    ElseIf TipoCuenta = Enumeradores.TipoCuenta.Saldo Then

    '    '        cuenta = LogicaNegocio.Genesis.MaestroCuenta.ObtenerCuentaMovimiento(CodigoCliente, _
    '    '                                                                             CodigoSubCliente, _
    '    '                                                                             CodigoPuntoServicio, _
    '    '                                                                             CodigoCanal, _
    '    '                                                                             CodigoSubCanal, _
    '    '                                                                             CodigoSector, _
    '    '                                                                             CodigoDelegacion, _
    '    '                                                                             CodigoPlanta, _
    '    '                                                                             DescripcionUsuario)

    '    '        cuenta = LogicaNegocio.Genesis.MaestroCuenta.ObtenerCuentaSaldo(cuenta, DescripcionUsuario)

    '    '    End If

    '    '    SyncLock _cuentas
    '    '        _cuentas.Add(cuenta)
    '    '    End SyncLock

    '    'End If

    '    Return cuenta

    'End Function

    'Private Shared _procesos As New ObservableCollection(Of GetProceso.Proceso)
    'Public Shared Function ObtenerProceso(CodigoCliente As String, _
    '                                       CodigoSubCliente As String, _
    '                                       CodigoPuntoServicio As String, _
    '                                       CodigoSubCanal As String, _
    '                                       CodigoDelegacion As String) As GetProceso.Proceso

    '    Dim proceso As GetProceso.Proceso = Nothing

    '    If _procesos IsNot Nothing AndAlso _procesos.Count > 0 Then

    '        SyncLock _procesos
    '            For Each p In _procesos

    '                proceso = _procesos.FirstOrDefault(Function(x) x.Cliente = CodigoCliente AndAlso x.SubCliente = CodigoSubCliente AndAlso _
    '                                                       x.PuntoServicio = CodigoPuntoServicio AndAlso x.SubCanal = CodigoSubCanal AndAlso _
    '                                                        x.Delegacion = CodigoDelegacion)

    '                If proceso IsNot Nothing Then
    '                    Exit For
    '                End If
    '            Next
    '        End SyncLock

    '    End If

    '    If proceso Is Nothing Then

    '        'Istancia objeto do tipo GetProceso.Peticion
    '        Dim objPeticionGetProcesoIac As New GetProceso.Peticion

    '        'Popula objeto peticion da operação GetProceso do IAC
    '        objPeticionGetProcesoIac.CodigoCliente = CodigoCliente
    '        objPeticionGetProcesoIac.CodigoSubcliente = CodigoSubCliente
    '        objPeticionGetProcesoIac.CodigoPuntoServicio = CodigoPuntoServicio
    '        objPeticionGetProcesoIac.CodigoSubcanal = CodigoSubCanal
    '        objPeticionGetProcesoIac.CodigoDelegacion = CodigoDelegacion

    '        'Invoca operação GetProceso do IAC e armazena seu retorno
    '        Dim objNegocioIAC As New Prosegur.Global.GesEfectivo.IAC.LogicaNegocio.AccionIntegracion
    '        Dim objRespuestasGetProcesoIac As GetProceso.Respuesta = objNegocioIAC.GetProceso(objPeticionGetProcesoIac)

    '        If objRespuestasGetProcesoIac IsNot Nothing AndAlso objRespuestasGetProcesoIac.Proceso IsNot Nothing Then
    '            SyncLock _procesos
    '                _procesos.Add(objRespuestasGetProcesoIac.Proceso)
    '            End SyncLock
    '            proceso = objRespuestasGetProcesoIac.Proceso
    '        End If

    '    End If

    '    Return proceso

    'End Function


End Class
