Imports System.Xml.Serialization
Imports System.Xml

Namespace GetMediosPago

    <XmlType(Namespace:="urn:GetMediosPagoIntegracion")> _
    <XmlRoot(Namespace:="urn:GetMediosPagoIntegracion")> _
    <Serializable()> _
    Public Class Peticion
#Region "Variáveis"

        Private _fechaInical As DateTime
        Private _fechaFinal As DateTime
        Private _vigente As Nullable(Of Boolean)

#End Region

#Region "Propriedades"

        Public Property FechaInical() As DateTime
            Get
                Return _fechaInical
            End Get
            Set(value As DateTime)
                _fechaInical = value
            End Set
        End Property

        Public Property FechaFinal() As DateTime
            Get
                Return _fechaFinal
            End Get
            Set(value As DateTime)
                _fechaFinal = value
            End Set
        End Property

        Public Property Vigente() As Nullable(Of Boolean)
            Get
                Return _vigente
            End Get
            Set(value As Nullable(Of Boolean))
                _vigente = value
            End Set
        End Property

#End Region


    End Class

End Namespace