Imports Prosegur.Genesis.Comon

Namespace Entidades

    <Serializable()>
    Public NotInheritable Class Sector
        Inherits BaseEntidad

        Public Property OidSector As String
        Public Property OidSectorPadre As String
        Public Property OidTipoSector As String
        Public Property OidPlanta As String
        Public Property CodSector As String
        Public Property DesSector As String
        Public Property CodMigracion As String
        Public Property BolCentroProceso As Boolean
        Public Property BolPermiteDisponerValor As Boolean
        Public Property BolTesoro As Boolean
        Public Property BolConteo As Boolean
        Public Property BolActivo As Boolean
        Public Property GmtCreacion As DateTime
        Public Property DesUsuarioCreacion As String
        Public Property GmtModificacion As DateTime
        Public Property DesUsuarioModificacion As String

    End Class

End Namespace