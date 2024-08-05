Imports System.Xml.Serialization
Imports System.ComponentModel
Imports Prosegur.Genesis.Comon

Namespace Contractos.Integracion.IntegracionSistemas.Integracion
    Public Class Configuracion
        Public Sub New()
            CodigoEstadoDetalle = Enumeradores.EstadoIntegracionDetalle.Vacio
            Detener = False
        End Sub

        Public Property Link As String
        Public Property SistemaOrigem As String
        Public Property SistemaDestino As String
        Public Property CodigoTablaIntegracion As String
        Public Property CodigoProceso As String
        Public Property Usuario As String
        Public Property IdentificadoresIntegracion As List(Of String)
        Public Property ReiniciarIntento As Boolean
        Public Property Detener As Boolean
        Public Property CodigoEstadoDetalle As Enumeradores.EstadoIntegracionDetalle
        Public Property Mensaje As String
        Public Property NombreParametroReintentoMaximo As String
        Public Property EstadosBusquedaIntegracion As List(Of Enumeradores.EstadoIntegracion)
        Public Property CodigoPais As String
        Public Property IdentificadorLlamada As String
        Public Property CodigoAplicacion As String
        Public Property NombreParametroUrl As String
        Public Property IdentificadorAjeno As String
    End Class

End Namespace