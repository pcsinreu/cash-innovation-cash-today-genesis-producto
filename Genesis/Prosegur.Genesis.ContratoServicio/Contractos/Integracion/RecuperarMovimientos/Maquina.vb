Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarMovimientos
    <Serializable()>
    Public Class Maquina
        Inherits Comon.Entidad
        Public Property Clientes As List(Of Cliente)

        <XmlAttributeAttribute()>
        Public Property Vigente As Boolean

    End Class
End Namespace