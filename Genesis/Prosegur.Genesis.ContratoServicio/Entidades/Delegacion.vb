Imports Prosegur.Genesis.Comon

Namespace Entidades

    <Serializable()>
    Public NotInheritable Class Delegacion
        Inherits BaseEntidad

        Public Property OidDelegacion As String
        Public Property CodDelegacion As String
        Public Property CodPais As String
        Public Property DesDelegacion As String
        Public Property BolVigente As Boolean
        Public Property OidPais As String
        Public Property NecGmtMinutos As Integer
        Public Property FyhVeranoInicio As DateTime
        Public Property FyhVeranoFin As DateTime
        Public Property NecVeranoAjuste As Integer
        Public Property DesZona As String
        Public Property GmtCreacion As DateTime
        Public Property DesUsuarioCreacion As String
        Public Property GmtModificacion As String
        Public Property DesUsuarioModificacion As String

    End Class

End Namespace