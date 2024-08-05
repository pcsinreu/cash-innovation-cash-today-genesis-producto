Namespace Genesis

    ''' <summary>
    ''' Classe Contenedor
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Contenedor

        '#Region "RecuperarContenedor"

        '        ''' <summary>
        '        ''' Recupera os contenedores com os filtros informados.
        '        ''' </summary>
        '        ''' <param name="Filtro"></param>
        '        ''' <returns></returns>
        '        ''' <remarks></remarks>
        '        Public Shared Function RecuperarContenedor(Filtro As Comon.Clases.Transferencias.FiltroElementoValor) As List(Of Comon.Clases.Contenedor)

        '            Dim objContenedores As List(Of Comon.Clases.Contenedor) = Nothing

        '            Try

        '                'Recupera os contenedores
        '                objContenedores = AccesoDatos.Genesis.Contenedor.RecuperarContenedor(Filtro)

        '            Catch ex As Exception
        '                Throw 
        '            End Try

        '            Return objContenedores
        '        End Function

        '#End Region

#Region "VerificarRemesaTieneContenedor"

        ''' <summary>
        ''' Recupera os contenedores com os filtros informados.
        ''' </summary>
        ''' <param name="Remesa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function VerificarRemesaTieneContenedor(Remesa As Comon.Clases.Remesa) As Boolean

            Dim objContenedores As Boolean = False

            Try

                'Recupera os contenedores
                objContenedores = AccesoDatos.Genesis.Contenedor.VerificarRemesaTieneContenedor(Remesa)

            Catch ex As Exception
                Throw
            End Try

            Return objContenedores
        End Function

#End Region

    End Class

End Namespace