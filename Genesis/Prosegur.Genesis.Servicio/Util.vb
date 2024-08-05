Imports Prosegur.Genesis.Comon.Monitor.Impl
Imports Prosegur.Genesis.Comon.Monitor.Transaction

Public Class Util

    Private Shared _Mon As New Mon(Prosegur.Genesis.Comon.Monitor.MonSenderDesktop.getInstance())

    Private Shared ReadOnly _Util As New Util
    Public Shared Function getInstance() As Util
        Return _Util
    End Function

    Public Function startMon(transactionNameEnum As TransactionNameEnum) As String
        Dim codigoPais As String = Nothing
        Dim codigoDelegacion As String = Nothing
        Dim login As String = Nothing

        login = Environment.UserName
        codigoPais = Globalization.CultureInfo.CurrentCulture.DisplayName
        codigoDelegacion = TimeZone.CurrentTimeZone.StandardName

        Return _Mon.startMon(transactionNameEnum, codigoPais, codigoDelegacion, login)
    End Function

    Public Function startMon(transactionNameEnum As TransactionNameEnum, obs As String)
        Dim codigoPais As String = Nothing
        Dim codigoDelegacion As String = Nothing
        Dim login As String = Nothing

        login = Environment.UserName
        codigoPais = Globalization.CultureInfo.CurrentCulture.DisplayName
        codigoDelegacion = TimeZone.CurrentTimeZone.StandardName

        Return _Mon.startMon(transactionNameEnum, codigoPais, codigoDelegacion, login, obs)
    End Function

    Public Function startMon(transactionNameEnum As TransactionNameEnum, codigoPais As String, codigoDelegacion As String, login As String, obs As String)

        If String.IsNullOrEmpty(login) Then
            login = Environment.UserName
        End If
        If String.IsNullOrEmpty(codigoPais) Then
            codigoPais = Globalization.CultureInfo.CurrentCulture.DisplayName
        End If
        If String.IsNullOrEmpty(codigoDelegacion) Then
            codigoDelegacion = TimeZone.CurrentTimeZone.StandardName
        End If

        Return _Mon.startMon(transactionNameEnum, codigoPais, codigoDelegacion, login, obs)
    End Function

    Public Sub endMon(uid As String)
        _Mon.endMon(uid)
    End Sub

    Public Sub Mon(transactionName As TransactionNameEnum)
        Dim codigoDelegacion As String = Nothing
        Dim login As String = Nothing

        login = Environment.UserName
        codigoDelegacion = TimeZone.CurrentTimeZone.StandardName

        Mon(transactionName, codigoDelegacion, login, Nothing, DateTime.Now, DateTime.Now)
    End Sub

    Public Sub Mon(transactionName As TransactionNameEnum, codigoDelegacion As String, login As String, obs As String, start As Date, [end] As Date)
        Dim codigoPais As String = Nothing
        codigoPais = Globalization.CultureInfo.CurrentCulture.DisplayName

        _Mon.doMon(transactionName, codigoPais, codigoDelegacion, login, obs, start, [end])
    End Sub

End Class
