Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace NuevoSalidas.Remesa.IniciarRemesa

    <XmlType(Namespace:="urn:IniciarRemesa")> _
    <XmlRoot(Namespace:="urn:IniciarRemesa")> _
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

        Public Property FechaHoraActualizacion As DateTime

    End Class

End Namespace