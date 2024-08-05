Imports System.Xml.Serialization
Imports System.Xml

Namespace GetProcesos

    <XmlType(Namespace:="urn:GetProcesos")> _
    <XmlRoot(Namespace:="urn:GetProcesos")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _proceso As ProcesoColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property Proceso() As ProcesoColeccion
            Get
                Return _proceso
            End Get
            Set(value As ProcesoColeccion)
                _proceso = value
            End Set
        End Property

#End Region

    End Class

End Namespace
