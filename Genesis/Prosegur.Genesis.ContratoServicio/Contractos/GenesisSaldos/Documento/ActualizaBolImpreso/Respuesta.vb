Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.GenesisSaldos.Documento.ActualizaBolImpreso

    <XmlType(Namespace:="urn:ActualizaBolImpreso")> _
    <XmlRoot(Namespace:="urn:ActualizaBolImpreso")> _
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


        Public Property RowVerDocumento As Integer
    End Class

End Namespace