Imports System.Xml.Serialization
Imports System.Xml

Namespace Iac.VerificarCodigoIac
    <XmlType(Namespace:="urn:VerificarCodigoIac")> _
    <XmlRoot(Namespace:="urn:VerificarCodigoIac")> _
    <Serializable()> _
    Public Class Peticion

        Private _codigoTerminoIac As String

        Public Property CodigoTerminoIac() As String
            Get
                Return _codigoTerminoIac
            End Get
            Set(value As String)
                _codigoTerminoIac = value
            End Set
        End Property

    End Class

End Namespace
