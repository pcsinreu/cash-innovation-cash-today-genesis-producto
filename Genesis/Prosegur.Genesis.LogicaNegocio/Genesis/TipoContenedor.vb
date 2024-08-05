Imports Prosegur.Genesis.Comon

Namespace Genesis

    ''' <summary>
    ''' Classe TipoContenedor
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TipoContenedor

        ''' <summary>
        ''' Retorna os tipos de contenedor
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerTipoContenedor() As List(Of Clases.TipoContenedor)

            Dim objTiposContenedor As New List(Of Clases.TipoContenedor)

            Try
                objTiposContenedor = AccesoDatos.Genesis.TipoContenedor.ObtenerTipoContenedor
            Catch ex As Exception
                Throw
            End Try

            Return objTiposContenedor
        End Function


        ''' <summary>
        ''' Retorna o identificador e a descrição dos tipos de contenedor
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerTipoContenedorSencillo() As List(Of Clases.TipoContenedor)

            Dim objTiposContenedor As New List(Of Clases.TipoContenedor)

            Try

                objTiposContenedor = AccesoDatos.Genesis.TipoContenedor.ObtenerTipoContenedorSencillo

            Catch ex As Exception
                Throw
            End Try

            Return objTiposContenedor
        End Function

    End Class

End Namespace