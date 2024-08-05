Imports System.Xml.Serialization
Imports System.Xml

Namespace GetMediosPago

    <XmlType(Namespace:="urn:GetMediosPagoIntegracion")> _
    <XmlRoot(Namespace:="urn:GetMediosPagoIntegracion")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _mediosdePago As MedioPagoColeccion
#End Region

#Region "Propriedades"


        Public Property MediosdePago() As MedioPagoColeccion
            Get
                Return _mediosdePago
            End Get
            Set(value As MedioPagoColeccion)
                _mediosdePago = value
            End Set
        End Property

#End Region

    End Class
End Namespace