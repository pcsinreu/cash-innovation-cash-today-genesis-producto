Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio

Namespace GestionPermisos
    Public Class Usuario
        ''' <summary>
        ''' Obtener una lista de usuarios
        ''' </summary>
        ''' <param name="peticion"></param>
        ''' <returns></returns>
        Public Shared Function ObtenerUsuarios(peticion As Contractos.Permisos.PeticionRecuperarUsuario, modoDetallado As Boolean) As List(Of Contractos.Permisos.RespuestaRecuperarUsuario)

            Try
                Return AccesoDatos.Usuario.ObtenerUsuarios(peticion, modoDetallado)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        ''' <summary>
        ''' Graba usuario
        ''' </summary>
        ''' <param name="peticion">Petición</param>
        ''' <param name="respuesta">Respuesta</param>
        Public Shared Sub GrabarUsuario(ByRef peticion As Contractos.Permisos.PeticionGrabarUsuario, ByRef respuesta As Contractos.Permisos.RespuestaGrabarUsuario)
            Try
                AccesoDatos.Usuario.GrabarUsuario(peticion, respuesta)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
    End Class
End Namespace

