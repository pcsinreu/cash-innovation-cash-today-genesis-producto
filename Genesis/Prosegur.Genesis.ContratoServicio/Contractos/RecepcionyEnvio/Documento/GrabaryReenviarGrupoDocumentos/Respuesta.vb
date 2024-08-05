Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon
Imports System.Xml

Namespace Documento.GrabaryReenviarGrupoDocumentos

    <XmlType(Namespace:="urn:GrabaryReenviarGrupoDocumentos")> _
    <XmlRoot(Namespace:="urn:GrabaryReenviarGrupoDocumentos")> _
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