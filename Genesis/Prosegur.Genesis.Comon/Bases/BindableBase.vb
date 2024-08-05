Imports System.Reflection
Imports System.ComponentModel

<Serializable()>
Public MustInherit Class BindableBase
    Implements INotifyPropertyChanged

    <NonSerialized>
    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Protected Function SetProperty(Of T)(ByRef storage As T, value As T, propertyName As String) As Boolean

        If Object.Equals(storage, value) Then
            Return False
        End If
        storage = value
        Me.OnPropertyChanged(propertyName)
        Return True
    End Function

    Protected Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

End Class
