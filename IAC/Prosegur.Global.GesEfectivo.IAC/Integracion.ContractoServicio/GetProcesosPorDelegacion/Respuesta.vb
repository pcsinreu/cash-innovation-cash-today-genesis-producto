Imports System.Xml.Serialization
Imports System.Xml

Namespace GetProcesosPorDelegacion

    <XmlType(Namespace:="urn:GetProcesosPorDelegacion")> _
    <XmlRoot(Namespace:="urn:GetProcesosPorDelegacion")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _procesos As ProcesoColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property Procesos() As ProcesoColeccion
            Get
                Return _procesos
            End Get
            Set(value As ProcesoColeccion)
                _procesos = value
            End Set
        End Property

#End Region

    End Class

End Namespace