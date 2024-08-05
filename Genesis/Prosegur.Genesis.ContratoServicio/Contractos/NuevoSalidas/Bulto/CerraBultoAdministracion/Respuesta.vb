Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace NuevoSalidas.Bulto.CerrarBultoAdministracion

    <XmlType(Namespace:="urn:CerrarBultoAdministracion")> _
    <XmlRoot(Namespace:="urn:CerrarBultoAdministracion")> _
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
        Public Property ErrorIntegracionSOL As String

        Public Property MensajeValidacion As String

    End Class

End Namespace