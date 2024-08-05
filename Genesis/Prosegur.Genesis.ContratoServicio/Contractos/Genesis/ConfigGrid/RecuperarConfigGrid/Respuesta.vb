Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.Genesis.ConfigGrid.RecuperarConfigGrid

    <XmlType(Namespace:="urn:RecuperarConfigGrid")> _
    <XmlRoot(Namespace:="urn:RecuperarConfigGrid")> _
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

        Property ConfigGrids As ObservableCollection(Of Clases.ConfigGrid)

    End Class


End Namespace