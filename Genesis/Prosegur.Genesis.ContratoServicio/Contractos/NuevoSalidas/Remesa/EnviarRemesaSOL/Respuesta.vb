Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization
Imports System.Data

Namespace Contractos.NuevoSalidas.Remesa.EnviarRemesaSOL

    <XmlType(Namespace:="urn:EnviarRemesaSOL")> _
    <XmlRoot(Namespace:="urn:EnviarRemesaSOL")> _
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

    End Class

End Namespace