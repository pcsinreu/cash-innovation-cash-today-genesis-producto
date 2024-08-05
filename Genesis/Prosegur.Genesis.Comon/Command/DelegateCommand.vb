Imports System.Windows.Input

Namespace Command

    <Serializable>
    Public Class DelegateCommand
        Implements ICommand

        Private ReadOnly _execute As Action(Of Object)
        Private ReadOnly _canExecute As Predicate(Of Object)
        Private _canExecuteOneTime As Boolean = False
        Private _doExecute As Boolean = False
        Private _resultCanExecute As Boolean = False

        Public Sub New(execute As Action(Of Object))
            Me.New(execute, Nothing)
        End Sub

        Public Sub New(execute As Action(Of Object), canExecute As Predicate(Of Object))
            If execute Is Nothing Then
                Throw New ArgumentNullException("execute")
            End If
            _execute = execute
            _canExecute = canExecute
        End Sub

        Public Sub New(execute As Action(Of Object), canExecute As Predicate(Of Object), canExecuteOneTime As Boolean)
            If execute Is Nothing Then
                Throw New ArgumentNullException("execute")
            End If
            _execute = execute
            _canExecute = canExecute
            _canExecuteOneTime = canExecuteOneTime
        End Sub

        Public Custom Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
            AddHandler(value As EventHandler)
                AddHandler CommandManager.RequerySuggested, value
            End AddHandler
            RemoveHandler(value As EventHandler)
                RemoveHandler CommandManager.RequerySuggested, value
            End RemoveHandler
            RaiseEvent(sender As System.Object, e As System.EventArgs)
            End RaiseEvent
        End Event

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute

            If (_canExecute Is Nothing) Then
                Return True
            ElseIf _doExecute Then
                Return _resultCanExecute
            Else
                _doExecute = _canExecuteOneTime
                _resultCanExecute = _canExecute.Invoke(parameter)
                Return _resultCanExecute
            End If

        End Function

        Public Sub CanExecute(canExecute As Boolean)

            _doExecute = Not canExecute

        End Sub

        Public Sub Execute(parameter As Object) Implements ICommand.Execute
            _execute.Invoke(parameter)
        End Sub

    End Class

    Public Class DelegateCommand(Of T)
        Implements ICommand

        Private ReadOnly _execute As Action(Of T)
        Private ReadOnly _canExecute As Predicate(Of T)

        Public Sub New(execute As Action(Of T))
            Me.New(execute, Nothing)
        End Sub

        Public Sub New(execute As Action(Of T), canExecute As Predicate(Of T))
            If execute Is Nothing Then
                Throw New ArgumentNullException("execute")
            End If

            _execute = execute
            _canExecute = canExecute
        End Sub

        <DebuggerStepThrough()> _
        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            If _canExecute Is Nothing Then
                Return True
            Else
                Return _canExecute.Invoke(CType(parameter, T))
            End If
        End Function

        Public Custom Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
            AddHandler(value As EventHandler)
                AddHandler CommandManager.RequerySuggested, value
            End AddHandler
            RemoveHandler(value As EventHandler)
                RemoveHandler CommandManager.RequerySuggested, value
            End RemoveHandler
            RaiseEvent(sender As System.Object, e As System.EventArgs)
            End RaiseEvent
        End Event

        Public Sub Execute(parameter As Object) Implements ICommand.Execute
            _execute(CType(parameter, T))
        End Sub
    End Class

End Namespace