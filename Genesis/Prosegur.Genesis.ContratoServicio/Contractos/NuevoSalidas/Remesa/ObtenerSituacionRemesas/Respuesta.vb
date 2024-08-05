Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace NuevoSalidas.Remesa.ObtenerSituacionRemesas

    <XmlType(Namespace:="urn:ObtenerSituacionRemesas")> _
    <XmlRoot(Namespace:="urn:ObtenerSituacionRemesas")> _
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

        Property SituacionesRemesa As SituacionesRemesa

    End Class

End Namespace