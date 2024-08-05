Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon
Imports System.Xml

Namespace Documento.CrearDocumento

    <XmlType(Namespace:="urn:CrearDocumento")> _
    <XmlRoot(Namespace:="urn:CrearDocumento")> _
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

        Public Property Documento As Clases.Documento

    End Class

End Namespace