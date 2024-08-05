Imports Oracle.DataAccess.Client

Public Class Transaccion
    Public TransaccionarAtomico As Boolean
    Public TransaccionEnCurso As OracleTransaction
    Public IniciarTransaccion As Boolean
    Public CerrarTransaccion As Boolean
End Class
