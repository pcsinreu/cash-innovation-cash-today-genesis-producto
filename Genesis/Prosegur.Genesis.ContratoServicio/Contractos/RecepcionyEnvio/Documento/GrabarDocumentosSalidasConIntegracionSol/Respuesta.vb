Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Documento.GrabarDocumentosSalidasConIntegracionSol

    <XmlType(Namespace:="urn:GrabarDocumentosSalidasConIntegracionSol")> _
    <XmlRoot(Namespace:="urn:GrabarDocumentosSalidasConIntegracionSol")> _
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
        Public Property CodigoExternoRemesasAnuladas As List(Of String)
    End Class

End Namespace