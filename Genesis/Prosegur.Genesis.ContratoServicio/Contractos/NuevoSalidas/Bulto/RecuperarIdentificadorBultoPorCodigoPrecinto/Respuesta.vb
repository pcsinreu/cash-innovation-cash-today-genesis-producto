Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Bulto.RecuperarIDsBultosPorCodigosPrecintos

    <XmlType(Namespace:="urn:RecuperarIDsBultosPorCodigosPrecintos")> _
    <XmlRoot(Namespace:="urn:RecuperarIDsBultosPorCodigosPrecintos")> _
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

        Public Property Bultos As List(Of Clases.Bulto)

    End Class

End Namespace
