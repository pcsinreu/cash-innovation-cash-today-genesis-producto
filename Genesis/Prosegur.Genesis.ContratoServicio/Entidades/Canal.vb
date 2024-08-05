Imports Prosegur.Genesis.Comon

Namespace Entidades

    <Serializable()>
    Public NotInheritable Class Canal
        Inherits BaseEntidad

        Public Property OidCanal As String
        Public Property CodCanal As String
        Public Property DesCanal As String
        Public Property ObsCanal As String
        Public Property BolVigente As Boolean
        Public Property CodUsuario As String
        Public Property FyhActualizacion As DateTime
        Public Property CodMigracion As String

        ' Navegações Entity Framework.
        Public Property SubCanales As List(Of SubCanal)

    End Class

End Namespace