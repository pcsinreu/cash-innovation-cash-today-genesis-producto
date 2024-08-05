Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.GenesisSaldos.Documento.AperturarElemento

    <XmlType(Namespace:="urn:AperturarElemento")> _
    <XmlRoot(Namespace:="urn:AperturarElemento")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property documento As Clases.Documento

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