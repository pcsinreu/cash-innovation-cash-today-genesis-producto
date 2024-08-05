Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon
Imports System.Xml

Namespace Documento.AlocarDesalocarDocumento

    <XmlType(Namespace:="urn:AlocarDesalocarDocumento")> _
    <XmlRoot(Namespace:="urn:AlocarDesalocarDocumento")> _
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

        Public Property IdentificadorExterno As String

    End Class

End Namespace