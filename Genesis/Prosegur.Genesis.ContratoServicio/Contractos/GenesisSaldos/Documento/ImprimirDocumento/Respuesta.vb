Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.GenesisSaldos.Documento.ImprimirElemento

    <XmlType(Namespace:="urn:ImprimirElemento")> _
    <XmlRoot(Namespace:="urn:ImprimirElemento")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property documento As Clases.Documento

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal mensaje As String)
            MyBase.New(mensaje)
        End Sub

        Sub New(ByVal exception As Exception)
            MyBase.New(exception)
        End Sub

    End Class

End Namespace