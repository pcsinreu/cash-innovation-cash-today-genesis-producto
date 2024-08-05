Imports System.Xml.Serialization
Imports System.Xml

Namespace Iac.VerificarDescripcionIac
    <XmlType(Namespace:="urn:VerificarDescripcionIac")> _
    <XmlRoot(Namespace:="urn:VerificarDescripcionIac")> _
    <Serializable()> _
    Public Class Peticion

        Private _descripcionTerminoIac As String

        Public Property DescripcionTerminoIac() As String
            Get
                Return _descripcionTerminoIac
            End Get
            Set(value As String)
                _descripcionTerminoIac = value
            End Set
        End Property

    End Class

End Namespace
