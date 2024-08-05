Imports System.Xml.Serialization
Imports System.Xml

Namespace Contractos.Dashboard.ObtenerClientes

    <XmlType(Namespace:="urn:ObtenerClientes")> _
    <XmlRoot(Namespace:="urn:ObtenerClientes")> _
    <Serializable()> _
    Public Class Peticion

#Region "[PROPRIEDADES]"
        Public Property BolVigente As Boolean

#End Region

    End Class

End Namespace