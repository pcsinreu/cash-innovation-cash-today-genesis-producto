Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperarCaracteristicasDocumento

    <XmlType(Namespace:="urn:RecuperarCaracteristicasDocumento")> _
    <XmlRoot(Namespace:="urn:RecuperarCaracteristicasDocumento")> _
    <Serializable()> _
    Public Class Peticion

        Private _IdFormulario As Integer

        Public Property IdFormulario() As Integer
            Get
                Return _IdFormulario
            End Get
            Set(value As Integer)
                _IdFormulario = value
            End Set
        End Property

    End Class

End Namespace