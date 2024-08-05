Imports System.Collections.ObjectModel

Namespace Clases
    ''' <summary>
    ''' Classe de AccionContable.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Class AccionContable
        Inherits BaseClase

        Public Property Identificador As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property EstaActivo As Boolean
        Public Property FechaHoraCreacion As DateTime
        Public Property UsuarioCreacion As String
        Public Property FechaHoraModificacion As DateTime
        Public Property UsuarioModificacion As String
        Public Property Acciones As ObservableCollection(Of AccionTransaccion)
    End Class

End Namespace

