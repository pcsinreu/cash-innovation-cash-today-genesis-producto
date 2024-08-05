Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Genesis
    Public Class Denominacion

        ''' <summary>
        ''' Obtener denominaciones (Componentes de los reportes)
        ''' </summary>
        ''' <param name="Filtros">Recibe un keyvaluepair conteniendo el Campo del filtro y una lista con valores para el filtro</param>
        ''' <param name="EsActivo">Recibi un valor boleano para las divisas activas o inactivas</param>
        ''' <param name="Ordenacion">Recibe un enumerador de ordenación</param>
        ''' <returns>Lista de divisas</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerDenominaciones(Filtros As List(Of KeyValuePair(Of String, List(Of String))), _
                                                     EsActivo As Boolean, _
                                                     Ordenacion As String) As ObservableCollection(Of Clases.Denominacion)

            Try
                Return AccesoDatos.Genesis.Denominacion.ObtenerDenominaciones(Filtros, EsActivo, Ordenacion)

            Catch ex As Exception
                Throw

            End Try

        End Function

        Public Shared Function ObtenerDenominaciones(IdentificadorDivisa As String, _
                                                     ListaIdentificadores As ObservableCollection(Of String), _
                                                    Optional EsNotIn As Boolean = False) As ObservableCollection(Of Clases.Denominacion)

            Try
                Return AccesoDatos.Genesis.Denominacion.RecuperarDenominaciones(IdentificadorDivisa, ListaIdentificadores, EsNotIn)

            Catch ex As Exception
                Throw

            End Try

        End Function

    End Class
End Namespace