Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace NuevoSalidas.TipoBulto.ObtenerTiposBulto

    <XmlType(Namespace:="urn:ObtenerTiposBulto")> _
    <XmlRoot(Namespace:="urn:ObtenerTiposBulto")> _
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

        Property TiposBulto As ObservableCollection(Of Clases.TipoBulto)

    End Class

End Namespace