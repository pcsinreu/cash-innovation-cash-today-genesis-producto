Imports Prosegur.Genesis.Comon

Namespace Entidades

    <Serializable()>
    Public NotInheritable Class SubCanal
        Inherits BaseEntidad

        Public Property OidSubcanal As String
        Public Property CodSubcanal As String
        Public Property OidCanal As String
        Public Property DesSubcanal As String
        Public Property ObsSubcanal As String
        Public Property BolVigente As Boolean
        Public Property CodUsuario As String
        Public Property FyhActualizacion As DateTime
        Public Property BolPorDefecto As Boolean
        Public Property CodMigracion As String

    End Class

End Namespace