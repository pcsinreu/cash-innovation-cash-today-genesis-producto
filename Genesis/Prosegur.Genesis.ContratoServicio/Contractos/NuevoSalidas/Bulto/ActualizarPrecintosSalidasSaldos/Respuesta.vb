Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.NuevoSalidas.Bulto.ActualizarPrecintosSalidasSaldos

    <XmlType(Namespace:="urn:ActualizarPrecintosSalidasSaldos")> _
    <XmlRoot(Namespace:="urn:ActualizarPrecintosSalidasSaldos")> _
    <Serializable()>
    Public NotInheritable Class Respuesta
        Inherits BaseRespuesta

        Sub New()
            MyBase.New()
        End Sub

        Sub New(mensaje As String)
            MyBase.New(mensaje)
        End Sub

        Sub New(exception As Exception)
            MyBase.New(exception)
        End Sub

    End Class
End Namespace
