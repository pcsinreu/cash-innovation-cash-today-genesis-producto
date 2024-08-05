Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboMaquinas

    <XmlType(Namespace:="urn:GetComboMaquinas")> _
    <XmlRoot(Namespace:="urn:GetComboMaquinas")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Private _descripcion As List(Of String)

        Public Property Descripcion() As List(Of String)
            Get
                Return _descripcion
            End Get
            Set(value As List(Of String))
                _descripcion = value
            End Set
        End Property

    End Class
End Namespace