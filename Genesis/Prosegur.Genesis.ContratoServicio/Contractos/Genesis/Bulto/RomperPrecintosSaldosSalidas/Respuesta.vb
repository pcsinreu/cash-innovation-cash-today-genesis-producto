Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.Genesis.Bulto.RomperPrecintosSaldosSalidas

    <XmlType(Namespace:="urn:RomperPrecintosSaldosSalidas")> _
    <XmlRoot(Namespace:="urn:RomperPrecintosSaldosSalidas")> _
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
