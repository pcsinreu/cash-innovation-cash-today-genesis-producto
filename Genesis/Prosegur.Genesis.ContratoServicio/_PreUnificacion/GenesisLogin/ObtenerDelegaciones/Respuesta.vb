Imports System.Xml.Serialization
Imports Prosegur.Genesis.ContractoServicio.Login.EjecutarLogin
Imports System.Runtime.Serialization

Namespace GenesisLogin.ObtenerDelegaciones

    <Serializable()> _
    <XmlType(Namespace:="urn:ObtenerDelegaciones")> _
    <XmlRoot(Namespace:="urn:ObtenerDelegaciones")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Sub New()
            Continentes = New List(Of Continente)()
        End Sub

        <DataMember()>
        Public Property Continentes As List(Of Continente)

    End Class

End Namespace