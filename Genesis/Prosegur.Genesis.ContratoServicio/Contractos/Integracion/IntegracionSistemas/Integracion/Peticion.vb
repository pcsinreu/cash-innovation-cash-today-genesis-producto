Imports System.Xml.Serialization
Imports System.ComponentModel
Imports Prosegur.Genesis.Comon

Namespace Contractos.Integracion.IntegracionSistemas.Integracion
    Public Class Peticion

        Public Property CodigoTablaIntegracion As String
        Public Property IdentificadorTablaIntegracion As String
        Public Property Estado As Enumeradores.EstadoIntegracion
        Public Property EstadoDetalle As Enumeradores.EstadoIntegracionDetalle
        Public Property ModuloOrigem As String
        Public Property ModuloDestino As String
        Public Property CodigoProceso As String
        Public Property IncrementoIntento As Integer
        Public Property ReiniciarIntento As Boolean
        Public Property Usuario As String
        Public Property Detalle As Comon.Entidad
        Public Property Log As String
    End Class

End Namespace