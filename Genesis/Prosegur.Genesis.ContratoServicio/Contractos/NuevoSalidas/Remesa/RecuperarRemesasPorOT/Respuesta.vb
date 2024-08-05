Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization
Imports System.Data

Namespace Contractos.NuevoSalidas.Remesa.RecuperarRemesasPorOT

    <XmlType(Namespace:="urn:RecuperarRemesasPorOT")> _
    <XmlRoot(Namespace:="urn:RecuperarRemesasPorOT")> _
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

        Public Property DatosRemesas As DataTable

    End Class

End Namespace