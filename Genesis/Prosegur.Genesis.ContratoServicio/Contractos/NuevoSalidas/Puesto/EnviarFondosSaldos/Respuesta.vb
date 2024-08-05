Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.NuevoSalidas.Puesto.EnviarFondosSaldos

    <XmlType(Namespace:="urn:EnviarFondosSaldos")> _
    <XmlRoot(Namespace:="urn:EnviarFondosSaldos")> _
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

        Property Documento As Clases.Documento

    End Class

End Namespace