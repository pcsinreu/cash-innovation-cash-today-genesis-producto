Imports System.Xml.Serialization
Imports System.Xml

Namespace Modulo.SetModulo

    <XmlType(Namespace:="urn:SetModulo")> _
    <XmlRoot(Namespace:="urn:SetModulo")> _
    <Serializable()> _
    Public Class Peticion

        Private _modulo As Modulo

        Public Property Modulo() As Modulo
            Get
                Return _modulo
            End Get
            Set(value As Modulo)
                _modulo = value
            End Set
        End Property

    End Class
End Namespace
