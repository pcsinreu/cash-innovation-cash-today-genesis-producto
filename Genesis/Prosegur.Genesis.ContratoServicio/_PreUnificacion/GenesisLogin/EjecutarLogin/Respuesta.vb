Imports System.Xml.Serialization
Imports Prosegur.Genesis.ContractoServicio.Login.EjecutarLogin

Namespace GenesisLogin.EjecutarLogin

    <Serializable()> _
    <XmlType(Namespace:="urn:EjecutarLogin")> _
    <XmlRoot(Namespace:="urn:EjecutarLogin")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Sub New()
            Usuario = New Usuario
        End Sub

        Public Property Usuario() As Usuario

        Public Property ResultadoOperacion() As ResultadoOperacionLoginLocal

    End Class

End Namespace
