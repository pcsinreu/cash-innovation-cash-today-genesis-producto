Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace GenesisConteo.Cuadrar

    <Serializable()>
    Public NotInheritable Class DiferenciasMedioPago

#Region "PROPRIEDADES"

        Public Property CodIsoDivisa As String
        Public Property NombreDivisa As String
        Public Property CodigoMedioPago As String
        Public Property NombreTipo As String
        Public Property DescripcionMedioPago As String
        Public Property UnidadesContadas As Integer
        Public Property UnidadesDeclaradas As Integer
        Public Property DiferenciaUnidades As Integer
        Public Property ImporteContado As Decimal
        Public Property ImporteDeclarado As Decimal
        Public Property DiferenciaImporte As Decimal
        Public Property DesTipoDiferencia As String
        Public Property ObsDiferencia As String
        Public Property CodNivelDetalle As String
        Public Property CodigoTipoMedioPago As String
        Public Property EsDentroTolerancias As Boolean
#End Region

    End Class

End Namespace