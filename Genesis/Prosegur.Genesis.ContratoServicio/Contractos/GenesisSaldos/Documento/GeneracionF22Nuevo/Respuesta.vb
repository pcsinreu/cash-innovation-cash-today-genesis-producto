Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Documento.GeneracionF22Nuevo

    <XmlType(Namespace:="urn:GeneracionF22Nuevo")> _
    <XmlRoot(Namespace:="urn:GeneracionF22Nuevo")> _
    <Serializable()>
    Public Class Respuesta
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

        Public Property InfoRemesas As ObservableCollection(Of InfoRemesa)

        Public Property ErrorIntegracionSOL As String

    End Class

End Namespace