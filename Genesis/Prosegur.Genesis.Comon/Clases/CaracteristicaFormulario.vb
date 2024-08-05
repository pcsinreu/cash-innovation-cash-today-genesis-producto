Namespace Clases

    <Serializable()>
    Public Class CaracteristicaFormulario
        Inherits BaseClase

        Sub New()  'we need a parameter-less constructor to make it serializable
        End Sub

        Public Property Identificador As String
        Public Property IdentificadorFormulario As String
        Public Property IdentificadorCaracteristica As String
        Public Property CodigoCaracteristica As String
        Public Property DescripcionCaracteristica As String
        Public Property CodigoMigracion As String
        Public Property FechaHoraCreacion As DateTime
        Public Property UsuarioCreacion As String
        Public Property FechaHoraModificacion As DateTime
        Public Property UsuarioModificacion As String

    End Class

End Namespace