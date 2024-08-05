Imports System.Xml.Serialization
Imports System.Xml

Namespace Modulo.SetModulo

    <XmlType(Namespace:="urn:SetModulo")> _
    <XmlRoot(Namespace:="urn:SetModulo")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Private _codModulo As String
        Public Property CodModulo() As String
            Get
                Return _codModulo
            End Get

            Set(Value As String)
                _codModulo = Value
            End Set
        End Property

    End Class
End Namespace
