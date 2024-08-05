Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.CheckSumInfTolerancias

    ''' <summary>
    ''' Classe CheckSumInfTolerancias
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/03/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:CheckSumInfTolerancias")> _
    <XmlRoot(Namespace:="urn:CheckSumInfTolerancias")> _
    <Serializable()> _
    Public Class CheckSumInfTolerancias

#Region "[VARIÁVEIS]"

        Private _agrupaciones As AgrupacionColeccion
        Private _mediosPago As MedioPagoColeccion
        Private _divisas As DivisaColeccion
        Private _bolMedioPago As Boolean

#End Region

#Region "[PROPRIEDADES]"

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

        Public Property BolMedioPago() As Boolean
            Get
                Return _bolMedioPago
            End Get
            Set(value As Boolean)
                _bolMedioPago = value
            End Set
        End Property

#End Region

    End Class

End Namespace