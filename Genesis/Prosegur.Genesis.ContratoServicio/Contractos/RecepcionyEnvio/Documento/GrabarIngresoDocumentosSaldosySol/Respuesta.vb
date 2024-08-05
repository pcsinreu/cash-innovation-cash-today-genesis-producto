Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Documento.GrabarIngresoDocumentosSaldosySol

    <XmlType(Namespace:="urn:GrabarIngresoDocumentosSaldosySol")> _
    <XmlRoot(Namespace:="urn:GrabarIngresoDocumentosSaldosySol")> _
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

        Public Property Grabo As Boolean
        Public Property Ruta As Clases.Ruta = Nothing

    End Class

End Namespace