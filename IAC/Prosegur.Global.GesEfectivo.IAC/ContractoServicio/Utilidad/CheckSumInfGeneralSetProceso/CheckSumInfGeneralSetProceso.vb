Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.CheckSumInfGeneralSetProceso

    ''' <summary>
    ''' Classe CheckSumInfGeneralSetProceso
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/03/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:CheckSumInfGeneralSetProceso")> _
    <XmlRoot(Namespace:="urn:CheckSumInfGeneralSetProceso")> _
    <Serializable()> _
    Public Class CheckSumInfGeneralSetProceso

#Region "[VARIÁVEIS]"

        Private _delegacion As String
        Private _desProducto As String
        Private _desModalidad As String
        Private _bolMedioPago As Boolean
        Private _codigoIacParcial As String
        Private _codigoIACBulto As String
        Private _codigoIACRemesa As String
        Private _agrupaciones As AgrupacionColeccion
        Private _mediosPago As MedioPagoColeccion
        Private _divisas As DivisaColeccion
        Private _bolCuentaChequeTotal As Boolean
        Private _bolCuentaTicketTotal As Boolean
        Private _bolCuentaOtrosTotal As Boolean
        Private _bolCuentaTarjetasTotal As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigoIacParcial() As String
            Get
                Return _codigoIacParcial
            End Get
            Set(value As String)
                _codigoIacParcial = value
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

        Public Property Delegacion() As String
            Get
                Return _delegacion
            End Get
            Set(value As String)
                _delegacion = value
            End Set
        End Property

        Public Property DesProducto() As String
            Get
                Return _desProducto
            End Get
            Set(value As String)
                _desProducto = value
            End Set
        End Property

        Public Property DesModalidad() As String
            Get
                Return _desModalidad
            End Get
            Set(value As String)
                _desModalidad = value
            End Set
        End Property

        Public Property BolMedioPago() As Boolean
            Get
                Return _bolMedioPago
            End Get
            Set(value As Boolean)
                _bolMedioPago = value
            End Set
        End Property

        Public Property Agrupaciones() As AgrupacionColeccion
            Get
                Return _agrupaciones
            End Get
            Set(value As AgrupacionColeccion)
                _agrupaciones = value
            End Set
        End Property

        Public Property MediosPago() As MedioPagoColeccion
            Get
                Return _mediosPago
            End Get
            Set(value As MedioPagoColeccion)
                _mediosPago = value
            End Set
        End Property

        Public Property Divisas() As DivisaColeccion
            Get
                Return _divisas
            End Get
            Set(value As DivisaColeccion)
                _divisas = value
            End Set
        End Property

        Public Property BolCuentaChequeTotal() As Boolean
            Get
                Return _bolCuentaChequeTotal
            End Get
            Set(value As Boolean)
                _bolCuentaChequeTotal = value
            End Set
        End Property

        Public Property BolCuentaTicketTotal() As Boolean
            Get
                Return _bolCuentaTicketTotal
            End Get
            Set(value As Boolean)
                _bolCuentaTicketTotal = value
            End Set
        End Property

        Public Property BolCuentaOtrosTotal() As Boolean
            Get
                Return _bolCuentaOtrosTotal
            End Get
            Set(value As Boolean)
                _bolCuentaOtrosTotal = value
            End Set
        End Property

        Public Property BolCuentaTarjetasTotal() As Boolean
            Get
                Return _bolCuentaTarjetasTotal
            End Get
            Set(value As Boolean)
                _bolCuentaTarjetasTotal = value
            End Set
        End Property

#End Region

    End Class

End Namespace