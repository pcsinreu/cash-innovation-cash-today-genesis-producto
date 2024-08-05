Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.Sector.ObtenerCodigosSectoresPorSectorPadre

    <Serializable()> _
    <XmlType(Namespace:="urn:ObtenerCodigosSectoresPorSectorPadre")> _
    <XmlRoot(Namespace:="urn:ObtenerCodigosSectoresPorSectorPadre")> _
    Public Class Respuesta
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

        Public Property codigosSectores As New List(Of String)

    End Class

End Namespace