Imports Prosegur.Genesis.Comon

Namespace GenesisConteo.Cuadrar

    ''' <summary>
    ''' Classe DescuadreDiferencias
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois]  14/08/2013 Criado
    ''' </history>
    <Serializable()>
    Public NotInheritable Class DescuadreDiferencias
        Inherits BaseEntidad

#Region "PROPRIEDADES"

        Public Property Totales As DiferenciasTotalesColeccion
        Public Property Detalles As DiferenciasDetalladosColeccion
        Public Property Agrupaciones As DiferenciasAgrupacionesColeccion
        Public Property MedioPago As DiferenciasMedioPagoColeccion
        Public Property DifDeclarados As DifDeclarados
        Public Property CodTipoContenedor As String
        Public Property DiferenciasNumeroParciales As String
        Public Property CantidadBultoDeclarado As Integer
        Public Property CantidadBultoContado As Integer
        Public Property CantidadParcialDeclarado As Integer
        Public Property CantidadParcialContado As Integer

#End Region

    End Class

End Namespace