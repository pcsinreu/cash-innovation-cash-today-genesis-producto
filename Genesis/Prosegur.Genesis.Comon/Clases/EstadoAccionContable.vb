Namespace Clases
    ''' <summary>
    ''' Represanta a table sapr_testadoxaccion_contable.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Class EstadoAccionContable
        Public Property Identificador As String
        Public Property IdentificadorAccionContable As String
        Public Property Codigo As String
        Public Property OrigemDisponible As String
        Public Property OrigemNoDisponible As String
        Public Property DestinoDisponible As String
        Public Property DestinoNoDisponible As String
        Public Property OrigenDisponibleBloqueado As String
        Public Property DestinoDisponibleBloqueado As String
        Public Property FechaHoraCreacion As DateTime
        Public Property UsuarioCreacion As String
        Public Property FechaHoraModificacion As DateTime
        Public Property UsuarioModificacion As String
    End Class
End Namespace

