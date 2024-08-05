Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionGetUsuariosDetail
    Implements ContractoServ.IGetUsuariosDetail


    ''' <summary>
    ''' GetUsuariosDetail
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 01/10/2012 Criado
    ''' </history>
    Public Function GetUsuariosDetail(objPeticion As ContractoServ.GetUsuariosDetail.Peticion) As ContractoServ.GetUsuariosDetail.Respuesta Implements ContractoServ.IGetUsuariosDetail.GetUsuariosDetail

        ' crir objeto respuesta
        Dim objRespuesta As New ContractoServ.GetUsuariosDetail.Respuesta

        Try

            ' Objetos do serviço de Login Global
            Dim objPeticionGetUsuariosDetail As New Seguridad.ContractoServicio.
                GetUsuariosDetail.Peticion() With {.Apellido1 = objPeticion.Apellido1,
                                                   .Aplicacion = objPeticion.Aplicacion,
                                                   .Delegacion = objPeticion.Delegacion,
                                                   .Login = objPeticion.Login,
                                                   .Nombre = objPeticion.Nombre,
                                                   .Permiso = objPeticion.Permiso,
                                                   .Role = objPeticion.Role,
                                                   .Sector = objPeticion.Sector}

            Dim objRespuestaGetUsuariosDetail As Seguridad.ContractoServicio.GetUsuariosDetail.Respuesta
            'Cria proxy do seguridade
            Dim objProxySeguridad As New LoginGlobal.Seguridad()

            objRespuestaGetUsuariosDetail = objProxySeguridad.GetUsuariosDetail(objPeticionGetUsuariosDetail)

            If objRespuestaGetUsuariosDetail.Codigo = 0 Then
                objRespuesta.Usuarios = New ContractoServ.GetUsuariosDetail.UsuarioColeccion()
                objRespuesta.Usuarios.AddRange(objRespuestaGetUsuariosDetail.Usuarios.
                                               Select(Function(f) New ContractoServ.GetUsuariosDetail.Usuario _
                                                        With {.Apellido1 = f.Apellido1,
                                                            .Login = f.Login,
                                                            .Nombre = f.Nombre,
                                                            .Delegacion = f.Delegacion,
                                                            .Activo = f.Activo}).ToList())
            End If

            objRespuesta.CodigoError = objRespuestaGetUsuariosDetail.Codigo
            objRespuesta.MensajeError = objRespuestaGetUsuariosDetail.Descripcion

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta

    End Function



End Class