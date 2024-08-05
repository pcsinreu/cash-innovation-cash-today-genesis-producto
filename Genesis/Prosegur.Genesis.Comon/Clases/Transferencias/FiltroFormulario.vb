Namespace Clases.Transferencias
    ''' <summary>
    ''' Classe que representa o Filtro de Formulário.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Class FiltroFormulario
        Inherits BaseClase

        Public Property SoloMovimientoDeSustitucion As Boolean
        Public Property MovimientoConFechaEspecifica As Boolean
        Public Property EstaActivo As Boolean
        Public Property CantidadDiasBusquedaInicio As Integer
        Public Property CantidadDiasBusquedaFin As String
        Public Property FechaHoraCreacion As DateTime
        Public Property UsuarioCreacion As String
        Public Property FechaHoraModificacion As DateTime
        Public Property UsuarioModificacion As String
        Public Property Identificador As String
        Public Property Descripcion As String
        Public Property SoloMovimientoDisponible As Boolean
        Public Property UtilizaDocumentosConValor As Boolean
        Public Property UtilizaDocumentosConBulto As Boolean
        Public Property SoloMovimientoDeReenvio As Boolean

    End Class
End Namespace