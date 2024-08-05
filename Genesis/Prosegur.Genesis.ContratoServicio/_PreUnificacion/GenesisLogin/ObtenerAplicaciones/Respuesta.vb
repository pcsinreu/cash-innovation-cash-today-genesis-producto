Imports System.Xml.Serialization
Imports Prosegur.Genesis.ContractoServicio.Login.EjecutarLogin

Namespace GenesisLogin.ObtenerAplicaciones

    <Serializable()> _
    <XmlType(Namespace:="urn:ObtenerAplicaciones")> _
    <XmlRoot(Namespace:="urn:ObtenerAplicaciones")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Sub New()
            Aplicaciones = New AplicacionVersionColeccion()
        End Sub

        Public Property Aplicaciones As AplicacionVersionColeccion

    End Class

End Namespace
