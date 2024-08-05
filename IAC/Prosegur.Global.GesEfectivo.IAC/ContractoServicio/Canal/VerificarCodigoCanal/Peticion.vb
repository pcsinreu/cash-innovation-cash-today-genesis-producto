Imports System.Xml.Serialization
Imports System.Xml

Namespace Canal.VerificarCodigoCanal
    <XmlType(Namespace:="urn:VerificarCodigoCanal")> _
    <XmlRoot(Namespace:="urn:VerificarCodigoCanal")> _
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

