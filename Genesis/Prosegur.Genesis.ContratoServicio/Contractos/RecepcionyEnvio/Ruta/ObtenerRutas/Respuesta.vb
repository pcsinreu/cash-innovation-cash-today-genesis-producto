Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Ruta.ObtenerRutas

    <XmlType(Namespace:="urn:ObtenerRutas")> _
    <XmlRoot(Namespace:="urn:ObtenerRutas")> _
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

        Public Property Ruta As Clases.Ruta

        Public Property ListaDocumentos As ObservableCollection(Of Clases.Documento)
    End Class

End Namespace