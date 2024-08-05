Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.NuevoSalidas.Remesa.CerrarHabilitacionATM

    <XmlType(Namespace:="urn:CerrarHabilitacionATM")> _
    <XmlRoot(Namespace:="urn:CerrarHabilitacionATM")> _
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

        Public Property ErrorIntegracionSOL As String

    End Class

End Namespace