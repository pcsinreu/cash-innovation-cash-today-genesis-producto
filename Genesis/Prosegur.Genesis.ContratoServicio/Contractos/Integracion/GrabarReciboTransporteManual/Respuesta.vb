Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.Integracion.GrabarReciboTransporteManual

    <XmlType(Namespace:="urn:GrabarReciboTransporteManual")>
    <XmlRoot(Namespace:="urn:GrabarReciboTransporteManual")>
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

        Public Property RecibosRepetidos As List(Of String)

    End Class

End Namespace