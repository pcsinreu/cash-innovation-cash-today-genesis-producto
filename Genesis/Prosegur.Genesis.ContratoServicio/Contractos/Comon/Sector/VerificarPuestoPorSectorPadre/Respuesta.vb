Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.Sector.VerificarPuestoPorSectorPadre

    <Serializable()> _
    <XmlType(Namespace:="urn:VerificarPuestoPorSectorPadre")> _
    <XmlRoot(Namespace:="urn:VerificarPuestoPorSectorPadre")> _
    Public Class Respuesta
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

        Public Property esPuestoValido As Boolean

    End Class

End Namespace

