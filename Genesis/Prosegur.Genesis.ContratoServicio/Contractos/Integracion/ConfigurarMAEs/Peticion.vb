Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConfigurarMAEs

    <XmlType(Namespace:="urn:ConfigurarMAEs.Entrada")>
    <XmlRoot(Namespace:="urn:ConfigurarMAEs.Entrada")>
    <Serializable()>
    Public Class Peticion

        Public Property Configuracion As Entrada.Configuracion

        Public Property Maquinas As List(Of Entrada.Maquina)

    End Class

End Namespace