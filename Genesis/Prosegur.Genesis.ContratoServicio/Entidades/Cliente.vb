Imports Prosegur.Genesis.Comon

Namespace Entidades

    <Serializable()>
    Public NotInheritable Class Cliente
        Inherits BaseEntidad

        Public Property OidCliente As String
        Public Property CodCliente As String
        Public Property DesCliente As String
        Public Property BolVigente As Boolean
        Public Property CodUsuario As String
        Public Property FyhActualizacion As DateTime
        Public Property BolEnviadoSaldos As Boolean
        Public Property OidTipoCliente As String
        Public Property CodMigracion As String
        Public Property BolTotalizadorSaldo As Boolean

        ' Navegações Entity Framework
        Public Property Subclientes As List(Of Subcliente)

    End Class

End Namespace
