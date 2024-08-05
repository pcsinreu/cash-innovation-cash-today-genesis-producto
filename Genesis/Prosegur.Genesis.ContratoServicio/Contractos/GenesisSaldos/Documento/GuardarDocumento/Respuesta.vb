Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.GenesisSaldos.Documento.GuardarDocumento

    <XmlType(Namespace:="urn:GuardarDocumento")> _
    <XmlRoot(Namespace:="urn:GuardarDocumento")> _
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

    End Class

End Namespace