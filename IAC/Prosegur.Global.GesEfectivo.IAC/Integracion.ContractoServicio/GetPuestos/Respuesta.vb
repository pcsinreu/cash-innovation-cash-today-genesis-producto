Imports System.Xml.Serialization
Imports System.Xml

Namespace GetPuestos

    <XmlType(Namespace:="urn:GetPuestos")> _
    <XmlRoot(Namespace:="urn:GetPuestos")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _Puestos As PuestoColeccion
#End Region

#Region "Propriedades"

        Public Property Puestos() As PuestoColeccion
            Get
                Return _Puestos
            End Get
            Set(value As PuestoColeccion)
                _Puestos = value
            End Set
        End Property
#End Region

    End Class
End Namespace