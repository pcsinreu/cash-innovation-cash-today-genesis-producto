Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Genesis

    ''' <summary>
    ''' Clase Calidad
    ''' </summary>
    ''' [marcel.espiritosanto] Creado 07/10/2013
    ''' <remarks></remarks>
    Public Class Calidad

        Public Shared Function ObtenerCalidades() As ObservableCollection(Of Clases.Calidad)

            Try
                Return AccesoDatos.Genesis.Calidad.ObtenerCalidades()

            Catch ex As Exception
                Throw

            End Try

        End Function

        Public Shared Function ObtenerCalidad(CodigoCalidad As String) As Clases.Calidad

            Try

                Return AccesoDatos.Genesis.Calidad.ObtenerCalidade(CodigoCalidad)

            Catch ex As Exception
                Throw

            End Try

        End Function

    End Class

End Namespace