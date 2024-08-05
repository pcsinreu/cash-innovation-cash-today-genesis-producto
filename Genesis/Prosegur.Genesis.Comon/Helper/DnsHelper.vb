Imports System.Net

Public Class DnsHelper


    Public Shared Function GetHostName() As String

        Dim sHostName As String = String.Empty
        Try

            sHostName = Dns.GetHostName()

        Catch ex As Exception

            sHostName = Nothing

        End Try

        Return sHostName

    End Function

    Public Shared Function GetHostNameIp4() As String

        Dim ipE As String = String.Empty

        Try

            ipE = Dns.GetHostEntry(Dns.GetHostName()).AddressList _
                                                    .Where(Function(a As IPAddress) Not a.IsIPv6LinkLocal AndAlso Not a.IsIPv6Multicast AndAlso Not a.IsIPv6SiteLocal) _
                                                    .First() _
                                                    .ToString()
        Catch ex As Exception

            ipE = Nothing

        End Try

        Return ipE

    End Function

End Class
