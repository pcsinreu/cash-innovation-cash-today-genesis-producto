Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace NuevoSalidas.Remesa.CerrarRemesa

    <XmlType(Namespace:="urn:CerrarRemesa")> _
    <XmlRoot(Namespace:="urn:CerrarRemesa")> _
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

    End Class

End Namespace