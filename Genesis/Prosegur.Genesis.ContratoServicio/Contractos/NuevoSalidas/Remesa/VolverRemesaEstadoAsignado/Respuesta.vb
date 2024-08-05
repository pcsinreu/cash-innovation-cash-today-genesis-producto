Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace NuevoSalidas.Remesa.VolverRemesaEstadoAsignado

    <XmlType(Namespace:="urn:VolverRemesaEstadoAsignado")> _
    <XmlRoot(Namespace:="urn:VolverRemesaEstadoAsignado")> _
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