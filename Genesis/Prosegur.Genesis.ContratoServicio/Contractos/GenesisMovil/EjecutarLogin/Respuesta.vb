Imports System.Xml.Serialization
Imports System.Xml


Namespace Contractos.GenesisMovil.EjecutarLogin
    <Serializable()> _
    Public Class Respuesta

        Public Property CodigoError() As Integer
        Public Property MensajeError() As String
        Public Property TiposSectores As List(Of TipoSector)
        Public Property ResultadoOperacion As Integer

    End Class
End Namespace
