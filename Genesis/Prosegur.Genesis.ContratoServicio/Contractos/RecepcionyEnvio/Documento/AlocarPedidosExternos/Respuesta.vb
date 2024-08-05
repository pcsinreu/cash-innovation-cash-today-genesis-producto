Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon
Imports System.Xml

Namespace Documento.AlocarPedidosExternos

    <XmlType(Namespace:="urn:AlocarPedidosExternos")> _
    <XmlRoot(Namespace:="urn:AlocarPedidosExternos")> _
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