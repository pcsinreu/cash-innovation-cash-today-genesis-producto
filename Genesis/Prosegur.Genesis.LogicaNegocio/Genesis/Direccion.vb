Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Genesis
    Public Class Direccion

        Public Shared Function ObtenerDireccionesPuntoServicio(codPuntoServicio As String, _
                                                     codSubCliente As String, _
                                                     codCliente As String) As List(Of Clases.Direccion)

            Try
                Return AccesoDatos.Genesis.Direccion.ObtenerDireccionesPuntoServicio(codPuntoServicio, codSubCliente, codCliente)

            Catch ex As Exception
                Throw

            End Try

        End Function

        Public Shared Function ObtenerDireccionesSubCliente(codSubCliente As String, _
                                                     codCliente As String) As List(Of Clases.Direccion)

            Try
                Return AccesoDatos.Genesis.Direccion.ObtenerDireccionesSubCliente(codSubCliente, codCliente)

            Catch ex As Exception
                Throw

            End Try

        End Function

        Public Shared Function ObtenerDireccionesCliente(codCliente As String) As List(Of Clases.Direccion)

            Try
                Return AccesoDatos.Genesis.Direccion.ObtenerDireccionesCliente(codCliente)

            Catch ex As Exception
                Throw

            End Try

        End Function

    End Class
End Namespace