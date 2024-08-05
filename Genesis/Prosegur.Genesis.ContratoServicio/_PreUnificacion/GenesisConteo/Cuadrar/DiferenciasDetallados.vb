Namespace GenesisConteo.Cuadrar

    ''' <summary>
    ''' Classe DescuadreDiferencias
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois]  14/08/2013 Criado
    ''' </history>
    <Serializable()>
    Public NotInheritable Class DiferenciasDetallados

#Region "PROPRIEDADES"

        Public Property CodIsoDivisa As String
        Public Property NombreDivisa As String
        Public Property CodDenominacion As String
        Public Property NombreDenominacion As String
        Public Property UnidadesContadas As Decimal
        Public Property UnidadesDeclaradas As Decimal
        Public Property DiferenciaUnidades As Decimal
        Public Property DesTipoDiferencia As String
        Public Property ObsDiferencia As String
        Public Property CodNivelDetalle As String
        Public Property ValorFacial As Decimal

#End Region

    End Class

End Namespace