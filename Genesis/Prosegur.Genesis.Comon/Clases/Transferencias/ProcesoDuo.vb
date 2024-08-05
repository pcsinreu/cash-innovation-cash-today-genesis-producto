Imports System.Collections.ObjectModel

Namespace Clases.Transferencias

    <Serializable()>
    Public Class ProcesoDuo
        Inherits BindableBase

        Public Property Identificador As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property CantidadThreads As String
        Public Property CantMaximaItemsThread As String
        Public Property Delimitador As String
        Public Property CantidadMaxIntentos As Integer
        Public Property ItensProceso As ObservableCollection(Of ItensProcesoDuo)

    End Class

End Namespace
