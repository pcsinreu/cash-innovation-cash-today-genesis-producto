Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.Reportes.GrabarTraspaseResponsabilidad

    <XmlType(Namespace:="urn:GrabarTraspaseResponsabilidad")> _
    <XmlRoot(Namespace:="urn:GrabarTraspaseResponsabilidad")> _
    <Serializable()>
    Public NotInheritable Class GrabarTraspaseResponsabilidadRespuesta
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