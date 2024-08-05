Imports System.Xml.Serialization
Imports System.Xml

Namespace Parametro.GetAgrupacionDetail

    <XmlType(Namespace:="urn:GetAgrupacionDetail")> _
    <XmlRoot(Namespace:="urn:GetAgrupacionDetail")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"
        Private _Agrupaciones As Agrupacion
#End Region

#Region "Propriedades"
        Public Property Agrupaciones() As Agrupacion
            Get
                Return _Agrupaciones
            End Get
            Set(value As Agrupacion)
                _Agrupaciones = value
            End Set
        End Property
#End Region

    End Class
End Namespace