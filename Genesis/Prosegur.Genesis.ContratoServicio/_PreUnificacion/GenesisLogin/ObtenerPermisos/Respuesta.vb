Imports System.Xml.Serialization
Imports System.Runtime.Serialization
Imports Prosegur.Genesis.ContractoServicio.Login.EjecutarLogin

Namespace GenesisLogin.ObtenerPermisos

    <Serializable()> _
    <XmlType(Namespace:="urn:ObtenerPermisos")> _
    <XmlRoot(Namespace:="urn:ObtenerPermisos")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Sub New()
            Delegaciones = New List(Of Delegacion)()
        End Sub

        <DataMember()>
        Public Property Delegaciones As List(Of Delegacion)

    End Class

End Namespace
