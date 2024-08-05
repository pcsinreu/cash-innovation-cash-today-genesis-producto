Imports System.Xml.Serialization

Namespace Contractos.Integracion.ActualizarMovimientos

    <Serializable()>
    Public Class MovimientoIterno

        Public Property Identificador As String
        Public Property CodigoExterno As String
        Public Property CodigoDelegacion As String
        Public Property CodigoPlanta As String
        Public Property CodigoSector As String
        Public Property CodigoCanal As String
        Public Property FechaGestion As DateTime
        Public Property Usuario As String
        Public Property IdentificadorMaquina As String
        Public Property IdentificadorPlanificacion As String
        Public Property IdentificadorPeriodo As String
        Public Property CodigoTipoPlanificacion As String
        Public Property PeriodoCreadoEnServicio As Boolean
        Public Property Validaciones As List(Of Comon.ValidacionError)

    End Class

End Namespace

