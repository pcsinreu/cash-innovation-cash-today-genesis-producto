Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Genesis

    ''' <summary>
    ''' Clase UnidadMedida
    ''' </summary>
    ''' [marcel.espiritosanto] Creado 07/10/2013
    ''' <remarks></remarks>
    Public Class UnidadMedida

        Public Shared Function ObtenerUnidadesMedida() As ObservableCollection(Of Clases.UnidadMedida)

            Try
                Return AccesoDatos.Genesis.UnidadMedida.ObtenerUnidadesMedida()

            Catch ex As Exception
                Throw

            End Try

        End Function

        Public Shared Function RecuperarUnidadMedida(Optional RecuperarUnidadPadron As Boolean? = Nothing, _
                                                     Optional IdentificadorUnidadeMedida As String = "", _
                                                     Optional objTipoUnidadMedida As Enumeradores.TipoUnidadMedida? = Nothing) As Clases.UnidadMedida

            Try
                Return AccesoDatos.Genesis.UnidadMedida.RecuperarUnidadMedida(RecuperarUnidadPadron, IdentificadorUnidadeMedida, objTipoUnidadMedida)

            Catch ex As Exception
                Throw

            End Try

        End Function

    End Class
End Namespace