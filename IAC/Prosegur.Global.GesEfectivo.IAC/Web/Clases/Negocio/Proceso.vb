Imports Prosegur.Framework.Dicionario.Tradutor

Namespace Negocio

    ''' <summary>
    ''' Classe de negócio Proceso
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  12/01/2011 criado
    ''' </history>
    <Serializable()> _
Public Class Proceso
        Inherits BaseEntidade

#Region "[VARIÁVEIS]"

        Private _oidProceso As String
        Private _codDelegacion As String
        Private _desProceso As String
        Private _codProceso As String
        Private _bolVigente As Boolean
        Private _bolContarChequesTotal As Boolean
        Private _bolContarTicketsTotal As Boolean
        Private _bolContarOtrosTotal As Boolean
        Private _bolContarTarjetasTotal As Boolean
        Private _codUsuario As String

        Private _clienteFacturacion As New Cliente
        Private _cliente As New Cliente
        Private _canal As New Canal
        Private _producto As New Producto
        Private _modalidad As New Modalidad
        Private _IAC As New InformacionAdicional
        Private _MediosPago As New List(Of MedioPago)

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodProceso As String
            Get
                Return _codProceso
            End Get
            Set(value As String)
                _codProceso = value
            End Set
        End Property

        Public Property CodUsuario() As String
            Get
                Return _codUsuario
            End Get
            Set(value As String)
                _codUsuario = value
            End Set
        End Property

        Public Property OidProceso() As String
            Get
                Return _oidProceso
            End Get
            Set(value As String)
                _oidProceso = value
            End Set
        End Property

        Public Property CodDelegacion() As String
            Get
                Return _codDelegacion
            End Get
            Set(value As String)
                _codDelegacion = value
            End Set
        End Property

        Public Property DesProceso() As String
            Get
                Return _desProceso
            End Get
            Set(value As String)
                _desProceso = value
            End Set
        End Property

        Public Property BolVigente() As Boolean
            Get
                Return _bolVigente
            End Get
            Set(value As Boolean)
                _bolVigente = value
            End Set
        End Property

        Public Property BolContarChequesTotal As Boolean
            Get
                Return _bolContarChequesTotal
            End Get
            Set(value As Boolean)
                _bolContarChequesTotal = value
            End Set
        End Property

        Public Property BolContarTicketsTotal As Boolean
            Get
                Return _bolContarTicketsTotal
            End Get
            Set(value As Boolean)
                _bolContarTicketsTotal = value
            End Set
        End Property

        Public Property BolContarOtrosTotal As Boolean
            Get
                Return _bolContarOtrosTotal
            End Get
            Set(value As Boolean)
                _bolContarOtrosTotal = value
            End Set
        End Property

        Public Property BolContarTarjetasTotal As Boolean
            Get
                Return _bolContarTarjetasTotal
            End Get
            Set(value As Boolean)
                _bolContarTarjetasTotal = value
            End Set
        End Property

        Public Property ClienteFacturacion() As Cliente
            Get
                Return _clienteFacturacion
            End Get
            Set(value As Cliente)
                _clienteFacturacion = value
            End Set
        End Property

        Public ReadOnly Property CodDesClienteFacturacion As String
            Get

                If _clienteFacturacion Is Nothing Then
                    Return String.Empty
                Else
                    Return _clienteFacturacion.CodigoCliente & " - " & _clienteFacturacion.DesCliente
                End If

            End Get
        End Property

        Public Property Canal() As Canal
            Get
                Return _canal
            End Get
            Set(value As Canal)
                _canal = value
            End Set
        End Property

        Public ReadOnly Property DesProducto As String
            Get
                If _producto Is Nothing Then
                    Return String.Empty
                Else
                    Return _producto.DesProducto
                End If
            End Get
        End Property

        Public Property Producto As Producto
            Get
                Return _producto
            End Get
            Set(value As Producto)
                _producto = value
            End Set
        End Property

        Public ReadOnly Property DesModoContage As String
            Get
                Return ObtenerDescModoContage()
            End Get
        End Property

        Public ReadOnly Property DesCanal As String
            Get
                If _canal Is Nothing Then
                    Return String.Empty
                Else
                    Return _canal.DesCanal
                End If
            End Get
        End Property

        Public ReadOnly Property DesSubCanal As String
            Get
                If _canal Is Nothing OrElse _canal.SubCanais Is Nothing OrElse _canal.SubCanais.Count = 0 Then
                    Return String.Empty
                Else
                    Return _canal.SubCanais(0).DesSubcanal
                End If
            End Get
        End Property

        Public ReadOnly Property DesModalidad As String
            Get
                If _modalidad Is Nothing Then
                    Return String.Empty
                Else
                    Return _modalidad.DesModalidad
                End If
            End Get
        End Property

        Public Property Modalidad As Modalidad
            Get
                Return _modalidad
            End Get
            Set(value As Modalidad)
                _modalidad = value
            End Set
        End Property

        Public Property IAC As InformacionAdicional
            Get
                Return _IAC
            End Get
            Set(value As InformacionAdicional)
                _IAC = value
            End Set
        End Property

        Public ReadOnly Property DesIAC As String
            Get
                If _IAC Is Nothing Then
                    Return String.Empty
                Else
                    Return _IAC.DesIAC
                End If
            End Get
        End Property

        Public Property Cliente As Cliente
            Get
                Return _cliente
            End Get
            Set(value As Cliente)
                _cliente = value
            End Set
        End Property

        Public Property MediosPago As List(Of MedioPago)
            Get
                Return _MediosPago
            End Get
            Set(value As List(Of MedioPago))
                _MediosPago = value
            End Set
        End Property

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New()

        End Sub

        Public Sub New(Oid As String, Codigo As String, Descripcion As String)

            _oidProceso = Oid
            _codProceso = Codigo
            _desProceso = DesProceso

        End Sub

        Public Sub New(Proceso As ContractoServicio.ATM.GetATMDetail.Proceso)

            If Proceso Is Nothing Then
                Exit Sub
            End If

            _oidProceso = Proceso.OidProceso

            If Proceso.RespuestaProcesoDetail Is Nothing OrElse Proceso.RespuestaProcesoDetail.Procesos Is Nothing OrElse _
                Proceso.RespuestaProcesoDetail.Procesos.Count = 0 Then
                Exit Sub
            End If

            _desProceso = Proceso.RespuestaProcesoDetail.Procesos(0).Descripcion
            _bolVigente = Proceso.RespuestaProcesoDetail.Procesos(0).Vigente
            _codDelegacion = Proceso.RespuestaProcesoDetail.Procesos(0).CodigoDelegacion
            _bolContarChequesTotal = Proceso.RespuestaProcesoDetail.Procesos(0).ContarChequesTotal
            _bolContarOtrosTotal = Proceso.RespuestaProcesoDetail.Procesos(0).ContarOtrosTotal
            _bolContarTarjetasTotal = Proceso.RespuestaProcesoDetail.Procesos(0).ContarTajetasTotal
            _bolContarTicketsTotal = Proceso.RespuestaProcesoDetail.Procesos(0).ContarTicketsTotal

            ' produto
            _producto.CodProducto = Proceso.RespuestaProcesoDetail.Procesos(0).CodigoProducto
            _producto.DesProducto = Proceso.RespuestaProcesoDetail.Procesos(0).DescripcionProducto

            ' canal
            _canal = New Canal
            _canal.SubCanais = New List(Of SubCanal)
            _canal.CodCanal = Proceso.RespuestaProcesoDetail.Procesos(0).CodigoCanal
            _canal.DesCanal = Proceso.RespuestaProcesoDetail.Procesos(0).DescripcionCanal

            ' subcanal
            Dim subCanal As New SubCanal
            subCanal.CodSubcanal = Proceso.RespuestaProcesoDetail.Procesos(0).CodigoSubcanal
            subCanal.DesSubcanal = Proceso.RespuestaProcesoDetail.Procesos(0).DescripcionSubcanal

            ' modalidade
            _modalidad.CodModalidad = Proceso.RespuestaProcesoDetail.Procesos(0).CodigoTipoProcesado
            _modalidad.DesModalidad = Proceso.RespuestaProcesoDetail.Procesos(0).DescripcionTipoProcesado

            ' inf. adicional
            _IAC.CodIAC = Proceso.RespuestaProcesoDetail.Procesos(0).CodigoIac
            _IAC.DesIAC = Proceso.RespuestaProcesoDetail.Procesos(0).DescripcionIac

            ' cliente facturacion
            _clienteFacturacion.CodigoCliente = Proceso.RespuestaProcesoDetail.Procesos(0).CodigoClienteFacturacion
            _clienteFacturacion.DesCliente = Proceso.RespuestaProcesoDetail.Procesos(0).DescripcionClienteFacturacion

            ' cliente 
            _cliente.CodigoCliente = Proceso.RespuestaProcesoDetail.Procesos(0).CodigoCliente
            _cliente.DesCliente = Proceso.RespuestaProcesoDetail.Procesos(0).DescripcionCliente

            ' subcliente 
            _cliente.Subclientes.Add(New Subcliente)
            _cliente.Subclientes(0).CodigoSubcliente = Proceso.RespuestaProcesoDetail.Procesos(0).CodigoSubcliente
            _cliente.Subclientes(0).DesSubcliente = Proceso.RespuestaProcesoDetail.Procesos(0).DescripcionSubcliente

            ' pto servicio
            _cliente.Subclientes(0).PtosServicio.Add(New PuntoServicio)
            _cliente.Subclientes(0).PtosServicio(0).CodigoPuntoServicio = Proceso.RespuestaProcesoDetail.Procesos(0).CodigoPuntoServicio
            _cliente.Subclientes(0).PtosServicio(0).DesPuntoServicio = Proceso.RespuestaProcesoDetail.Procesos(0).DescripcionPuntoServicio

            _canal.SubCanais.Add(subCanal)

            ' preenche términos de medios pago
            _MediosPago = New List(Of MedioPago)
            For Each mp In Proceso.RespuestaProcesoDetail.Procesos(0).MediosDePagoProceso
                _MediosPago.Add(New MedioPago(mp))
            Next

        End Sub

#End Region

#Region "[MÉTODOS]"

        Public Function ConvertToSetATM(CodCliente As String, CodSubcliente As String, CodPtoServicio As String, _
                                        Divisas As List(Of Divisa)) As ContractoServicio.ATM.SetATM.Proceso

            Dim Proceso As New ContractoServicio.ATM.SetATM.Proceso
            Proceso.PeticionProceso = New ContractoServicio.Proceso.SetProceso.Peticion
            Proceso.PeticionProceso.Proceso = New ContractoServicio.Proceso.SetProceso.Proceso
            Proceso.PeticionProceso.Proceso.Cliente = New ContractoServicio.Proceso.SetProceso.Cliente
            Proceso.PeticionProceso.Proceso.Cliente.SubClientes = New ContractoServicio.Proceso.SetProceso.SubClienteColeccion
            Proceso.PeticionProceso.Proceso.SubCanal = New ContractoServicio.Proceso.SetProceso.SubCanalColeccion
            Proceso.PeticionProceso.Proceso.DivisaProceso = New ContractoServicio.Proceso.SetProceso.DivisaProcesoColeccion
            Proceso.PeticionProceso.Proceso.MediosPagoProceso = New ContractoServicio.Proceso.SetProceso.MedioPagoProcesoColeccion


            ' inicializa variáveis
            Proceso.PeticionProceso.Proceso.Cliente.SubClientes.Add(New ContractoServicio.Proceso.SetProceso.SubCliente)
            Proceso.PeticionProceso.Proceso.Cliente.SubClientes(0).PuntosServicio = New ContractoServicio.Proceso.SetProceso.PuntoServicioColeccion
            Proceso.PeticionProceso.Proceso.Cliente.SubClientes(0).PuntosServicio.Add(New ContractoServicio.Proceso.SetProceso.PuntoServicio)
            Proceso.PeticionProceso.Proceso.SubCanal.Add(New ContractoServicio.Proceso.SetProceso.SubCanal)

            Proceso.PeticionProceso.CodigoUsuario = _codUsuario

            With Proceso.PeticionProceso.Proceso
                .Descripcion = _desProceso
                .CodigoDelegacion = _codDelegacion
                .Vigente = _bolVigente
                ' ATM só tem 1 cliente, 1 subcliente e 1 ponto de serviço
                .Cliente.Codigo = CodCliente
                .Cliente.SubClientes(0).Codigo = CodSubcliente
                .Cliente.SubClientes(0).PuntosServicio(0).Codigo = CodPtoServicio
                If _canal IsNot Nothing Then
                    .SubCanal(0).Codigo = _canal.SubCanais(0).CodSubcanal
                End If
                '.CodigoTipoProcesado
                If _IAC IsNot Nothing Then
                    .CodigoIac = _IAC.CodIAC
                End If
                If _producto IsNot Nothing Then
                    .CodigoProducto = _producto.CodProducto
                End If
                If _clienteFacturacion IsNot Nothing Then
                    .CodigoClienteFacturacion = _clienteFacturacion.CodigoCliente
                End If
                If _modalidad IsNot Nothing Then
                    .CodigoTipoProcesado = _modalidad.CodModalidad
                End If
                .IndicadorMediosPago = True
                .ContarChequesTotal = _bolContarChequesTotal
                .ContarOtrosTotal = _bolContarOtrosTotal
                .ContarTicketsTotal = _bolContarTicketsTotal
                .ContarTarjetasTotal = _bolContarTarjetasTotal
            End With

            If Divisas IsNot Nothing AndAlso Divisas.Count > 0 Then

                Dim objDiv As ContractoServicio.Proceso.SetProceso.DivisaProceso

                For Each div In Divisas

                    objDiv = New ContractoServicio.Proceso.SetProceso.DivisaProceso()
                    objDiv.Codigo = div.CodigoDivisa
                    Proceso.PeticionProceso.Proceso.DivisaProceso.Add(objDiv)

                Next

            End If

            If _MediosPago IsNot Nothing AndAlso _MediosPago.Count > 0 Then

                Dim objMP As ContractoServicio.Proceso.SetProceso.MedioPagoProceso
                Dim objTermino As ContractoServicio.Proceso.SetProceso.TerminoMedioPago

                For Each mp In _MediosPago

                    objMP = New ContractoServicio.Proceso.SetProceso.MedioPagoProceso

                    objMP.Codigo = mp.CodMedioPago
                    objMP.CodigoIsoDivisa = mp.Divisa.CodigoDivisa
                    objMP.CodigoTipoMedioPago = mp.TipoMedioPago.CodTipoMedioPago

                    objMP.TerminosMedioPago = New ContractoServicio.Proceso.SetProceso.TerminoMedioPagoColeccion

                    ' envia somente os términos selecionados
                    For Each t In mp.Terminos

                        objTermino = New ContractoServicio.Proceso.SetProceso.TerminoMedioPago

                        objTermino.Codigo = t.CodigoTermino
                        objTermino.EsObligatorioTerminoMedioPago = t.EsObligatorio

                        objMP.TerminosMedioPago.Add(objTermino)

                    Next

                    Proceso.PeticionProceso.Proceso.MediosPagoProceso.Add(objMP)

                Next

            End If

            Return Proceso

        End Function

        Public Function ObtenerDescModoContage() As String

            Dim desc As String = String.Empty

            If BolContarChequesTotal Then
                desc &= Traduzir("023_chk_contar_cheque_totales") & ", "
            End If

            If BolContarOtrosTotal Then
                desc &= Traduzir("023_chk_contar_otros_totales") & ", "
            End If

            If BolContarTarjetasTotal Then
                desc &= Traduzir("023_chk_tarjetas_totales") & ", "
            End If

            If BolContarTicketsTotal Then
                desc &= Traduzir("023_chk_contar_tickets_totales") & ", "
            End If

            ' remove última vírgula
            If Not String.IsNullOrEmpty(desc) Then
                desc = desc.Substring(0, desc.Length - 2)
            End If

            Return desc

        End Function

        Public Function ObtenerTermino(CodIsoDivisa As String, CodTipoMedioPago As String, CodMedioPago As String, CodTermino As String) As TerminoMedioPago

            ' obtém medio pago selecionado
            Dim mp As MedioPago = (From item In _MediosPago Where item.Divisa.CodigoDivisa = CodIsoDivisa _
                                   AndAlso item.TipoMedioPago.CodTipoMedioPago = CodTipoMedioPago _
                                   AndAlso item.CodMedioPago = CodMedioPago).FirstOrDefault()

            If mp Is Nothing Then
                Return Nothing
            End If

            ' obtém término selecionado
            Dim termino As TerminoMedioPago = (From t In mp.Terminos Where t.CodigoTermino = CodTermino).FirstOrDefault()

            Return termino

        End Function

#End Region

    End Class

End Namespace
