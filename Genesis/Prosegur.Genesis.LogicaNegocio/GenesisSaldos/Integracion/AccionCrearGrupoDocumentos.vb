Imports Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.GrupoDocumento

Namespace Integracion

    Public Class AccionCrearGrupoDocumentos

        Public Shared Function GuardarGrupoDocumentos(Peticion As GuardarGrupoDocumento.Peticion) As GuardarGrupoDocumento.Respuesta

            Dim Respuesta As New GuardarGrupoDocumento.Respuesta

            Try

                Peticion.GrupoDocumento.UsuarioModificacion = Peticion.UsuarioLogado
                LogicaNegocio.GenesisSaldos.MaestroGrupoDocumentos.GuardarGrupoDocumentos(Peticion.GrupoDocumento, True, False, Nothing, Nothing, Nothing)

            Catch ex As Excepcion.NegocioExcepcion
                Respuesta.Mensajes.Add(ex.Descricao)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                Respuesta.Excepciones.Add(ex.Message)
            End Try


            Return Respuesta
        End Function

    End Class

End Namespace

