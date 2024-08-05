Namespace GenesisConteo.Cuadrar

    ''' <summary>
    ''' Classe DescuadreDiferencias
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois]  14/08/2013 Criado
    ''' </history>
    <Serializable()>
    Public NotInheritable Class DiferenciasAgrupaciones

        Public Property Descripcion As String
        Public Property ValorDeclarado As Decimal
        Public Property ValorContado As Decimal
        Public Property DiferenciaValores As Decimal
        Public Property EsDentroTolerancia As Boolean

    End Class

End Namespace