Imports System.Reflection
Imports System.Text

Namespace Transaction
    Public Class Transaction
        Implements IDisposable

        Private Const SEPARATOR As String = "|"

        Private uid As String = Nothing
        Private Const sistema As String = "Génesis"
        Private transactionNameEnum As TransactionNameEnum = Nothing
        Private ReadOnly systemVersion As String = Assembly.GetExecutingAssembly().GetName().Version.ToString()
        Private codigoPais As String = Nothing
        Private codigoDelegacion As String = Nothing
        Private start As DateTime? = Nothing
        Private [end] As DateTime? = Nothing
        Private login As String = Nothing
        Private obs As String = Nothing
        Public ReadOnly Property getUid As String
            Get
                Return uid
            End Get
        End Property

        Public Sub setEnd([end] As Date)
            Me.end = [end]
        End Sub

        Public ReadOnly Property getTransactionNameEnum As TransactionNameEnum
            Get
                Return Me.transactionNameEnum
            End Get
        End Property

        Public Sub New(transactionNameEnum As TransactionNameEnum, codigoPais As String, codigoDelegacion As String, login As String, obs As String)
            Me.transactionNameEnum = transactionNameEnum
            Me.codigoPais = codigoPais
            Me.codigoDelegacion = codigoDelegacion
            Me.uid = Guid.NewGuid.ToString()
            Me.start = DateTime.Now
            Me.login = login
            Me.obs = obs
        End Sub

        Public Sub New(transactionNameEnum As TransactionNameEnum, codigoPais As String, codigoDelegacion As String, login As String, obs As String, start As Date, [end] As Date)
            Me.New(transactionNameEnum, codigoPais, codigoDelegacion, login, obs)
            Me.start = start
            Me.end = [end]
        End Sub

        Public Overrides Function ToString() As String
            Dim sb As New StringBuilder()

            sb.Append(Me.uid)
            sb.Append(SEPARATOR)
            sb.Append(Transaction.sistema)
            sb.Append(SEPARATOR)
            sb.Append(Me.transactionNameEnum.ToString())
            sb.Append(SEPARATOR)
            sb.Append(Me.systemVersion)
            sb.Append(SEPARATOR)
            sb.Append(Me.codigoPais)
            sb.Append(SEPARATOR)
            sb.Append(If(Me.codigoDelegacion IsNot Nothing, Me.codigoDelegacion, "NA"))
            sb.Append(SEPARATOR)
            sb.Append(DateTime.Now.ToString())
            sb.Append(SEPARATOR)
            sb.Append(Me.end - Me.start)
            sb.Append(SEPARATOR)
            sb.Append(If(Me.login IsNot Nothing, Me.login, "NA"))
            sb.Append(SEPARATOR)
            sb.Append(If(Me.obs IsNot Nothing, Me.obs, "NA"))

            Return sb.ToString()
        End Function

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace