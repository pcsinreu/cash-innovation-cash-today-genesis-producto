Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.NuevoSalidas.Bulto.IniciarBulto

    <XmlType(Namespace:="urn:IniciarBulto")> _
    <XmlRoot(Namespace:="urn:IniciarBulto")> _
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

        Public Property fyhActualizacionRemesa As DateTime?
        Public Property fyhActualizacionBulto As DateTime?

        Public Property EstadoBulto As Enumeradores.EstadoBulto
        Public Property EstadoRemesa As Enumeradores.EstadoRemesa

    End Class

End Namespace