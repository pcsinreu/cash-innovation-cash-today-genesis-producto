Imports System.Xml.Serialization
Imports System.Xml

Namespace Puesto.GetPuestoDetail

    <XmlType(Namespace:="urn:GetPuestoDetail")> _
    <XmlRoot(Namespace:="urn:GetPuestoDetail")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"
        Private _Puesto As Puesto
#End Region

#Region "Propriedades"

        Public Property Puesto() As Puesto
            Get
                Return _Puesto
            End Get
            Set(value As Puesto)
                _Puesto = value
            End Set
        End Property
#End Region

    End Class
End Namespace