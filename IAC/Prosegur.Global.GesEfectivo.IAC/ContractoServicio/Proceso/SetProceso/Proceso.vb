Imports System.Xml.Serialization
Imports System.Xml

Namespace Proceso.SetProceso

    ''' <summary>
    ''' Classe Proceso
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 17/03/2009 Criado
    ''' </history>    
    <Serializable()> _
    Public Class Proceso

#Region "[VARIÁVEIS]"

        Private _codigoDelegacion As String
        Private _descripcion As String
        Private _observacion As String
        Private _vigente As Boolean
        Private _cliente As Cliente
        Private _subCanal As SubCanalColeccion
        Private _codigoTipoProcesado As String
        Private _codigoIac As String
        Private _codigoIACBulto As String
        Private _codigoIACRemesa As String
        Private _codigoProducto As String
        Private _codigoClienteFacturacion As String
        Private _indicadorMediosPago As Boolean
        Private _contarChequesTotal As Boolean
        Private _contarTicketsTotal As Boolean
        Private _contarOtrosTotal As Boolean
        Private _contarTarjetasTotal As Boolean
        Private _divisaProceso As DivisaProcesoColeccion
        Private _mediosPagoProceso As MedioPagoProcesoColeccion
        Private _agrupacionesProceso As AgrupacionProcesoColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigoDelegacion() As String
            Get
                Return _codigoDelegacion
            End Get
            Set(value As String)
                _codigoDelegacion = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
            End Set
        End Property

        Public Property Observacion() As String
            Get
                Return _observacion
            End Get
            Set(value As String)
                _observacion = value
            End Set
        End Property

        Public Property Vigente() As Boolean
            Get
                Return _vigente
            End Get
            Set(value As Boolean)
                _vigente = value
            End Set
        End Property

        Public Property Cliente() As Cliente
            Get
                Return _cliente
            End Get
            Set(value As Cliente)
                _cliente = value
            End Set
        End Property


        Public Property SubCanal() As SubCanalColeccion
            Get
                Return _subCanal
            End Get
            Set(value As SubCanalColeccion)
                _subCanal = value
            End Set
        End Property


        Public Property CodigoTipoProcesado() As String
            Get
                Return _codigoTipoProcesado
            End Get
            Set(value As String)
                _codigoTipoProcesado = value
            End Set
        End Property

        Public Property CodigoIac() As String
            Get
                Return _codigoIac
            End Get
            Set(value As String)
                _codigoIac = value
            End Set
        End Property

        Public Property CodigoIACBulto() As String
            Get
                Return _codigoIACBulto
            End Get
            Set(value As String)
                _codigoIACBulto = value
            End Set
        End Property

        Public Property CodigoIACRemesa() As String
            Get
                Return _codigoIACRemesa
            End Get
            Set(value As String)
                _codigoIACRemesa = value
            End Set
        End Property

        Public Property CodigoProducto() As String
            Get
                Return _codigoProducto
            End Get
            Set(value As String)
                _codigoProducto = value
            End Set
        End Property

        Public Property CodigoClienteFacturacion() As String
            Get
                Return _codigoClienteFacturacion
            End Get
            Set(value As String)
                _codigoClienteFacturacion = value
            End Set
        End Property

        Public Property IndicadorMediosPago() As Boolean
            Get
                Return _indicadorMediosPago
            End Get
            Set(value As Boolean)
                _indicadorMediosPago = value
            End Set
        End Property

        Public Property ContarChequesTotal() As Boolean
            Get
                Return _contarChequesTotal
            End Get
            Set(value As Boolean)
                _contarChequesTotal = value
            End Set
        End Property

        Public Property ContarTicketsTotal() As Boolean
            Get
                Return _contarTicketsTotal
            End Get
            Set(value As Boolean)
                _contarTicketsTotal = value
            End Set
        End Property

        Public Property ContarOtrosTotal() As Boolean
            Get
                Return _contarOtrosTotal
            End Get
            Set(value As Boolean)
                _contarOtrosTotal = value
            End Set
        End Property

        Public Property ContarTarjetasTotal As Boolean
            Get
                Return _contarTarjetasTotal
            End Get
            Set(value As Boolean)
                _contarTarjetasTotal = value
            End Set
        End Property

        Public Property DivisaProceso() As DivisaProcesoColeccion
            Get
                Return _divisaProceso
            End Get
            Set(value As DivisaProcesoColeccion)
                _divisaProceso = value
            End Set
        End Property

        Public Property MediosPagoProceso() As MedioPagoProcesoColeccion
            Get
                Return _mediosPagoProceso
            End Get
            Set(value As MedioPagoProcesoColeccion)
                _mediosPagoProceso = value
            End Set
        End Property

        Public Property AgrupacionesProceso() As AgrupacionProcesoColeccion
            Get
                Return _agrupacionesProceso
            End Get
            Set(value As AgrupacionProcesoColeccion)
                _agrupacionesProceso = value
            End Set
        End Property

#End Region

    End Class

End Namespace