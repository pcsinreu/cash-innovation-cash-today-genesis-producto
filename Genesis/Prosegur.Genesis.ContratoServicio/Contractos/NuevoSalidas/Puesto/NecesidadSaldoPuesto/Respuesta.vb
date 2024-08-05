Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.NuevoSalidas.Puesto.NecesidadFondoPuesto

    <XmlType(Namespace:="urn:NecesidadFondoPuesto")> _
    <XmlRoot(Namespace:="urn:NecesidadFondoPuesto")> _
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

        Public Property Divisas As ObservableCollection(Of Clases.Divisa)

    End Class

End Namespace