Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.Reportes.GrabarRecepcionRuta

    <XmlType(Namespace:="urn:GrabarRecepcionRuta")> _
    <XmlRoot(Namespace:="urn:GrabarRecepcionRuta")> _
    <Serializable()>
    Public NotInheritable Class GrabarRecepcionRutaRespuesta
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