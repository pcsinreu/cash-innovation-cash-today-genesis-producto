Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Bulto.CerrarBulto

    <XmlType(Namespace:="urn:CerrarBulto")> _
    <XmlRoot(Namespace:="urn:CerrarBulto")> _
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

        Public Property RemesaProcesada As Boolean
        Public Property BultoProcesado As Boolean
        Public Property Bultos As ObservableCollection(Of Clases.Bulto)
        Public Property MensajeValidacion As String

    End Class

End Namespace