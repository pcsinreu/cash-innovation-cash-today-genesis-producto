Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldos
    <Serializable()>
    Public Class Maquina
        Inherits Comon.Entidad
        Public Property Canales As List(Of Canal)

        <XmlAttributeAttribute()>
        Public Property Vigente As Boolean

    End Class
End Namespace
