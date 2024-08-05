Imports System.Xml.Serialization
Imports System.Xml
Imports Prosegur.Genesis.Comon

Namespace Contractos.GenesisMovil.ObtenerSectoresTesoro
    <Serializable()> _
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property CodigoError() As Integer
        Public Property MensajeError() As String
        Public Property Sectores As List(Of Sector)

    End Class
End Namespace
