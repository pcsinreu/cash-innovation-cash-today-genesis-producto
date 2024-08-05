Imports System.Xml.Serialization

Namespace CargaPreviaEletronica.GetConfiguraciones

    <XmlType(Namespace:="urn:GetConfiguraciones")> _
    <XmlRoot(Namespace:="urn:GetConfiguraciones")> _
    <Serializable()>
    Public Class Respuesta
        Inherits RespuestaGenerico

        Private _configuraciones As Configuracion_CPColeccion

        Public Property Configuraciones() As Configuracion_CPColeccion
            Get
                Return _configuraciones
            End Get
            Set(value As Configuracion_CPColeccion)
                _configuraciones = value
            End Set
        End Property


    End Class

End Namespace
