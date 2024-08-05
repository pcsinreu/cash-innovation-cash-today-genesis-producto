Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Documento.GrabarDocumentosSalidas

    <XmlType(Namespace:="urn:GrabarDocumentosSalidas")> _
    <XmlRoot(Namespace:="urn:GrabarDocumentosSalidas")> _
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

        Public Property CodigoComprobante As String
    End Class

End Namespace