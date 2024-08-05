Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConfigurarMAEs

    <XmlType(Namespace:="urn:ConfigurarMAEs.Salida")>
    <XmlRoot(Namespace:="urn:ConfigurarMAEs.Salida")>
    <Serializable()>
    Public Class Respuesta

        Public Property Resultado As Salida.Resultado
        Public Property Maquinas As List(Of Salida.Maquina)

        Public Sub New()
            Resultado = New Salida.Resultado
        End Sub

    End Class

End Namespace