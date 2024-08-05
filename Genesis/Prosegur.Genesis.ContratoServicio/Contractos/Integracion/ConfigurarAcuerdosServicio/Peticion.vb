Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConfigurarAcuerdosServicio
    <XmlType(Namespace:="urn:ConfigurarAcuerdosServicio")>
    <XmlRoot(Namespace:="urn:ConfigurarAcuerdosServicio")>
    <Serializable()>
    Public Class Peticion

        Public Property CodigoPais As String

        Public Property Configuracion As Entrada.Configuracion
        Public Property Acuerdos As List(Of Entrada.Acuerdo)
    End Class

End Namespace