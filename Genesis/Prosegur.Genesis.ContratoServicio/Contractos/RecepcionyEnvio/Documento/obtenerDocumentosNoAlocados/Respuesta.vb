Imports System.Xml.Serialization
Imports System.Xml
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Documento.obtenerDocumentosNoAlocados

    <XmlType(Namespace:="urn:obtenerDocumentosNoAlocados")> _
    <XmlRoot(Namespace:="urn:obtenerDocumentosNoAlocados")> _
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

        Public Property ListaDocumentos As ObservableCollection(Of Clases.Documento)
    End Class

End Namespace