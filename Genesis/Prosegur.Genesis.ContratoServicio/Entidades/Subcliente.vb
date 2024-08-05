Imports Prosegur.Genesis.Comon

Namespace Entidades

    <Serializable()>
    Public NotInheritable Class Subcliente
        Inherits BaseEntidad

        Public Property OidSubcliente As String
        Public Property OidCliente As String
        Public Property CodSubcliente As String
        Public Property DesSubcliente As String
        Public Property BolVigente As Boolean
        Public Property CodUsuario As String
        Public Property FyhActualizacion As DateTime
        Public Property BolEnviadoSaldos As Boolean
        Public Property OidTipoSubcliente As String
        Public Property CodMigracion As String
        Public Property BolTotalizadorSaldo As Boolean

        ' Navegações Entity Framework
        Public Property PuntosServicio As List(Of PuntoServicio)

    End Class

End Namespace
