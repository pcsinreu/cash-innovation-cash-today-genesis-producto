Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConfigurarClientes

    <XmlType(Namespace:="urn:ConfigurarClientes.Entrada")>
    <XmlRoot(Namespace:="urn:ConfigurarClientes.Entrada")>
    <Serializable()>
    Public Class Peticion

        Public Property Configuracion As Entrada.Configuracion
        Public Property CodigoPais As String
        Public Property Clientes As List(Of Entrada.Cliente)

    End Class

End Namespace