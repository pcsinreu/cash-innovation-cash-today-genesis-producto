Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConfigurarClientes

    <XmlType(Namespace:="urn:ConfigurarClientes.Salida")>
    <XmlRoot(Namespace:="urn:ConfigurarClientes.Salida")>
    <Serializable()>
    Public Class Respuesta

        Public Property Resultado As Salida.Resultado
        Public Property Clientes As List(Of Salida.Cliente)

        Public Sub New()
            Resultado = New Salida.Resultado
        End Sub

    End Class

End Namespace