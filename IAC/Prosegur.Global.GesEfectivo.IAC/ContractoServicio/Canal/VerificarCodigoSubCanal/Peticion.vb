Imports System.Xml.Serialization
Imports System.Xml

Namespace Canal.VerificarCodigoSubCanal
    <XmlType(Namespace:="urn:VerificarCodigoSubCanal")> _
    <XmlRoot(Namespace:="urn:VerificarCodigoSubCanal")> _
    <Serializable()> _
    Public Class Peticion

        Private _codigo As String

        Public Property Codigo() As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

    End Class

End Namespace

