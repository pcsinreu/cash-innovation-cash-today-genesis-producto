Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.NuevoSalidas.Remesa.RecuperarTerminosIACRemesa

    <XmlType(Namespace:="urn:RecuperarTerminosIACRemesa")> _
    <XmlRoot(Namespace:="urn:RecuperarTerminosIACRemesa")> _
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

        Public Property TerminosIAC As List(Of Clases.TerminoIAC)
    End Class

End Namespace