Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.NuevoSalidas.Remesa.CerrarPreparacion

    <XmlType(Namespace:="urn:CerrarPreparacion")> _
    <XmlRoot(Namespace:="urn:CerrarPreparacion")> _
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

        Property exito As Boolean
        Property Remesa As Clases.Remesa

    End Class

End Namespace