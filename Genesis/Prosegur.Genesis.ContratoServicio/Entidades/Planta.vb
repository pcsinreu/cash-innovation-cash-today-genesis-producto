Imports Prosegur.Genesis.Comon

Namespace Entidades

    <Serializable()>
    Public NotInheritable Class Planta
        Inherits BaseEntidad

        Public Property OidPlanta As String
        Public Property OidDelegacion As String
        Public Property CodPlanta As String
        Public Property DesPlanta As String
        Public Property CodMigracion As String
        Public Property BolActivo As Boolean
        Public Property GmtCreacion As DateTime
        Public Property DesUsuarioCreacion As String
        Public Property GmtModificacion As DateTime
        Public Property DesUsuarioModificacion As String

    End Class

End Namespace