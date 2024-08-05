Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarClientes

    <XmlType(Namespace:="urn:RecuperarClientes")>
    <XmlRoot(Namespace:="urn:RecuperarClientes")>
    <Serializable()>
    Public Class Respuesta

        Public Property Resultado As Salida.Resultado
        Public Property Clientes As List(Of Salida.Cliente)

        Public Property Paginacion As Comon.Paginacion

        Public Sub New()
            Resultado = New Salida.Resultado
        End Sub

    End Class

End Namespace