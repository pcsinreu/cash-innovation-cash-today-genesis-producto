Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionLogin
    Implements ContractoServicio.ILogin

    ''' <summary>
    ''' Efetua login do usuário
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 09/02/2009 Criado
    ''' </history>
    Public Function EfetuarLogin(objPeticion As ContractoServicio.Login.Peticion) As ContractoServicio.Login.Respuesta Implements ContractoServicio.ILogin.EfetuarLogin

        ' crir objeto respuesta
        Dim objRespuesta As New ContractoServicio.Login.Respuesta

        Try

            ' Efetuar login ------------------------
            Dim objPeticionLogin As New Seguridad.ContractoServicio.Login.Peticion()
            Dim objRespuestaLogin As Seguridad.ContractoServicio.Login.Respuesta
            Dim objProxyLogin As New LoginGlobal.Seguridad()

            objPeticionLogin.NombreAplicacion = Parametros.Configuracion.Aplicacion
            objPeticionLogin.CodigoDelegacion = objPeticion.Delegacion
            objPeticionLogin.Login = objPeticion.IdentificadorUsuario
            objPeticionLogin.Password = objPeticion.Password

            ' valida o usuário e retorna suas permissões/roles e supervisores.
            objRespuestaLogin = objProxyLogin.Login(objPeticionLogin)
            ' --------------------------------------

            ' se validou usuário com sucesso
            If objRespuestaLogin.Codigo = 0 Then 'Seguridad.ContractoServicio.Login.ResultadoOperacionLogin.Autenticado Then 'Sucesso

                ' preencher objeto respuesta
                objRespuesta.ResultadoOperacion = ContractoServicio.Login.ResultadoOperacionLoginLocal.Autenticado
                objRespuesta.InformacionUsuario.Apelido = objRespuestaLogin.Usuario.Apellido
                objRespuesta.InformacionUsuario.Nombre = objRespuestaLogin.Usuario.Nombre

                If objRespuestaLogin.Usuario.Continentes.Count > 0 AndAlso _
                objRespuestaLogin.Usuario.Continentes(0).Paises.Count > 0 AndAlso _
                objRespuestaLogin.Usuario.Continentes(0).Paises(0).Delegaciones.Count > 0 AndAlso _
                objRespuestaLogin.Usuario.Continentes(0).Paises(0).Delegaciones(0).Sectores.Count > 0 Then

                    For Each permiso In objRespuestaLogin.Usuario.Continentes(0).Paises(0).Delegaciones(0).Sectores(0).Permisos
                        objRespuesta.InformacionUsuario.Permisos.Add(permiso.Nombre)
                    Next

                    For Each role In objRespuestaLogin.Usuario.Continentes(0).Paises(0).Delegaciones(0).Sectores(0).Roles
                        objRespuesta.InformacionUsuario.Permisos.Add(role.Nombre)
                    Next

                End If

                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT

            ElseIf objRespuestaLogin.Codigo = 1 Then
                objRespuesta.MensajeError = objRespuestaLogin.Descripcion
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.ResultadoOperacion = ContractoServicio.Login.ResultadoOperacionLoginLocal.NoEsValido
            Else
                objRespuesta.MensajeError = objRespuestaLogin.Descripcion
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.ResultadoOperacion = ContractoServicio.Login.ResultadoOperacionLoginLocal.Error
            End If

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.ResultadoOperacion = ContractoServicio.Login.ResultadoOperacionLoginLocal.Error

        Catch ex As Exception

            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.ResultadoOperacion = ContractoServicio.Login.ResultadoOperacionLoginLocal.Error
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 05/02/2010 - Criado
    ''' </history>
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ILogin.Test
        Dim objRespuesta As New ContractoServicio.Test.Respuesta

        Try

            AccesoDatos.Test.TestarConexao()

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = Traduzir("021_SemErro")

        Catch ex As Excepcion.NegocioExcepcion

            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao


        Catch ex As Exception

            Util.TratarErroBugsnag(ex)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta
    End Function

End Class