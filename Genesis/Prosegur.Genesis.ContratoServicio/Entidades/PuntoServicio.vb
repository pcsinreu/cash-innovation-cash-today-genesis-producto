Imports Prosegur.Genesis.Comon

Namespace Entidades

    <Serializable()>
    Public NotInheritable Class PuntoServicio
        Inherits BaseEntidad

        Public Property OidPtoServicio As String
        Public Property OidSubcliente As String
        Public Property CodPtoServicio As String
        Public Property DesPtoServicio As String
        Public Property BolVigente As Boolean
        Public Property CodUsuario As String
        Public Property FyhActualizacion As DateTime
        Public Property BolEnviadoSaldos As Boolean
        Public Property OidTipoPuntoServicio As String
        Public Property CodMigracion As String
        Public Property BolTotalizadorSaldo As Boolean

    End Class

End Namespace
