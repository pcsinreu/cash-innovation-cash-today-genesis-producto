Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Global

Public Class AccionValidarPermisosUsuario

    Public Shared Function Ejecutar(Peticion As Login.ValidarPermisosUsuario.Peticion) As Login.ValidarPermisosUsuario.Respuesta

        Dim respuesta As New Login.ValidarPermisosUsuario.Respuesta
        Dim objRespuestaValidarPermisosUsuario As Seguridad.ContractoServicio.ValidarPermisosUsuario.Respuesta

        Try

            ' obtem as versões
            objRespuestaValidarPermisosUsuario = ValidarUsuario(Peticion)

            respuesta.UsuarioValido = objRespuestaValidarPermisosUsuario.UsuarioValido
            respuesta.CodigoError = objRespuestaValidarPermisosUsuario.Codigo
            respuesta.MensajeError = objRespuestaValidarPermisosUsuario.Descripcion

        Catch ex As Excepcion.NegocioExcepcion

            respuesta.CodigoError = ex.Codigo
            respuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            respuesta.MensajeError = ex.ToString()

        End Try

        Return respuesta
    End Function

    ''' <summary>
    ''' Executa serviço ObtenerVersiones
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ValidarUsuario(Peticion As Login.ValidarPermisosUsuario.Peticion) As Seguridad.ContractoServicio.ValidarPermisosUsuario.Respuesta

        Dim objPeticionValidarPermisosUsuario As New Seguridad.ContractoServicio.ValidarPermisosUsuario.Peticion()
        Dim objProxyLogin As New LoginGlobal.Seguridad()

        With objPeticionValidarPermisosUsuario
            .ClaveSupervisor = Peticion.ClaveSupervisor
            .CodigoAplicacion = Peticion.CodigoAplicacion
            .CodigoDelegacion = Peticion.CodigoDelegacion
            .CodigoPlanta = Peticion.CodigoPlanta
            .CodigoTipoSector = Peticion.CodigoTipoSector
            .Login = Peticion.Login
            .Permisos = Peticion.Permisos
        End With


        'Obtém versão da aplicação
        Return objProxyLogin.ValidarPermisosUsuario(objPeticionValidarPermisosUsuario)

    End Function
End Class
