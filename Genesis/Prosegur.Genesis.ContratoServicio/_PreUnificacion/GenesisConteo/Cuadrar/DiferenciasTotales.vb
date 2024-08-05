Namespace GenesisConteo.Cuadrar

    ''' <summary>
    ''' Classe DescuadreDiferencias
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois]  14/08/2013 Criado
    ''' </history>
    <Serializable()>
    Public NotInheritable Class DiferenciasTotales

#Region "PROPRIEDADES"

        Public Property NombreTipo As String 'Efectivo, Ticket, Cheque, Otros Valores e Total
        Public Property CodTipoMedioPago As String
        Public Property NombreDivisa As String 'Nome da divisa
        Public Property CodigoIsoDivisa As String 'Codigo ISO da divisa
        Public Property ImporteContado As Decimal 'Valor Contado
        Public Property ImporteDeclarado As Decimal 'Valor Declarado
        Public Property DiferenciaImporte As Decimal 'Valor apurado
        Public Property EsDentroTolerancia As Boolean 'Indica se a diferença está dentro da tolerância ou não
        Public Property DesTipoDiferencia As String
        Public Property ObsDiferencia As String
        Public Property CodNivelDetalle As String

#End Region

    End Class

End Namespace