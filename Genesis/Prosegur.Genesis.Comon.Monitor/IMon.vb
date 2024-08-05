Imports Prosegur.Genesis.Comon.Monitor.Transaction

Public Interface IMon
    Function startMon(transactionNameEnum As TransactionNameEnum, codigoPais As String, codigoDelegacion As String, login As String) As String
    Function startMon(transactionNameEnum As TransactionNameEnum, codigoPais As String, codigoDelegacion As String, login As String, obs As String) As String
    Sub endMon(uid As String)
    Sub doMon(transactionName As TransactionNameEnum, codigoPais As String, codigoDelegacion As String, login As String, obs As String, start As Date, [end] As Date)
End Interface
