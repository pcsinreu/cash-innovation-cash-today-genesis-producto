Namespace Clases
    <Serializable()>
    Public Class FiltroFormulario
        Inherits BaseClase

        Public Property Identificador As String
        Public Property Descripcion As String
        Public Property SoloDisponible As Boolean
        Public Property ConValor As Boolean
        Public Property ConBulto As Boolean
        Public Property SoloReenvio As Boolean
        Public Property SoloSustitucion As Boolean
        Public Property ConFechaEspecifica As Boolean
        Public Property NecDiasBusquedaInicio As Integer
        Public Property NecDiasBusquedaFim As Integer
        Public Property CodigoMigracion As String
        Public Property EsActivo As Boolean
        Public Property FechaCreacion As DateTime
        Public Property UsuarioCriacion As String
        Public Property FechaModificacion As DateTime
        Public Property UsuarioModificacion As String

    End Class

End Namespace
