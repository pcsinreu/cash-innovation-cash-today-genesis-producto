Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.NuevoSalidas.Puesto.ObtenerPuestosPorDelegacion

    <XmlType(Namespace:="urn:ObtenerPuestosPorDelegacion")> _
    <XmlRoot(Namespace:="urn:ObtenerPuestosPorDelegacion")> _
    <Serializable()>
    Public NotInheritable Class RespuestaCompresion
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

        Property PuestosCompresion As Byte()

    End Class

End Namespace