Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionLogin
    Implements ContractoServ.ILogin

    ''' <summary>
    ''' Efetua login do usuário
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.olivera] 16/07/2009 Criado
    ''' </history>
    Public Function EfetuarLogin(objPeticion As ContractoServ.Login.Peticion) As ContractoServ.Login.Respuesta Implements ContractoServ.ILogin.EfetuarLogin

        ' crir objeto respuesta
        Dim objRespuesta As New ContractoServ.Login.Respuesta

        Try

            ' Efetuar login ------------------------
            Dim objPeticionLogin As New Seguridad.ContractoServicio.LoginDelegacion.Peticion()
            Dim objRespuestaLogin As Seguridad.ContractoServicio.LoginDelegacion.Respuesta
            Dim objProxyLogin As New LoginGlobal.Seguridad()

            objPeticionLogin.NombreAplicacion = Parametros.Configuracion.Aplicacion
            'objPeticionLogin.CodigoDelegacion = objPeticion.Delegacion
            objPeticionLogin.Login = objPeticion.IdentificadorUsuario
            objPeticionLogin.Password = objPeticion.Password

            ' valida o usuário e retorna suas permissões/roles e supervisores.
            objRespuestaLogin = objProxyLogin.LoginDelegacion(objPeticionLogin)
            ' --------------------------------------

            ' se validou usuário com sucesso
            If objRespuestaLogin.Codigo = 0 Then '= Seguridad.ContractoServicio.Login.ResultadoOperacionLogin.Autenticado Then 'Sucesso

                ' preencher objeto respuesta
                objRespuesta.ResultadoOperacion = ContractoServ.Login.ResultadoOperacionLoginLocal.Autenticado
                objRespuesta.InformacionUsuario.Apelido = objRespuestaLogin.Usuario.Apellido
                objRespuesta.InformacionUsuario.Nombre = objRespuestaLogin.Usuario.Nombre

                If objRespuestaLogin.Usuario.Continentes.Count > 0 AndAlso
                objRespuestaLogin.Usuario.Continentes(0).Paises.Count > 0 AndAlso
                objRespuestaLogin.Usuario.Continentes(0).Paises(0).Delegaciones.Count > 0 AndAlso
                objRespuestaLogin.Usuario.Continentes(0).Paises(0).Delegaciones(0).Sectores.Count > 0 Then

                    ' para cada delegação retornada do serviço seguridad, inserir na lista de informações do usuário
                    For Each pais As Prosegur.Global.Seguridad.ContractoServicio.LoginDelegacion.PaisLoginDelegacion In objRespuestaLogin.Usuario.Continentes(0).Paises

                        For Each delegacion As Prosegur.Global.Seguridad.ContractoServicio.LoginDelegacion.DelegacionLoginDelegacion In pais.Delegaciones

                            ' Cria o objeto delegacion do contrato serviço Reportes
                            Dim objDelegacion As New Prosegur.Global.GesEfectivo.Reportes.ContractoServ.Login.Delegacion
                            objDelegacion.Codigo = delegacion.Codigo
                            objDelegacion.Descripcion = delegacion.Nombre

                            For Each planta As Prosegur.Global.Seguridad.ContractoServicio.LoginDelegacion.PlantaLoginDelegacion In delegacion.Plantas

                                Dim objPlanta As New Prosegur.Global.GesEfectivo.Reportes.ContractoServ.Login.Planta
                                objPlanta.Codigo = planta.Codigo
                                objPlanta.Descricao = planta.Descricao
                                objPlanta.Identificador = planta.Identificador

                                ' adiciona cada descrição de permiso para a delegação e sector
                                For Each sector As Prosegur.Global.Seguridad.ContractoServicio.LoginDelegacion.SectorLoginDelegacion In planta.Sectores

                                    Dim objSector As New Prosegur.Global.GesEfectivo.Reportes.ContractoServ.Login.TipoSector

                                    For Each permiso As Prosegur.Global.Seguridad.ContractoServicio.LoginDelegacion.PermisoLoginDelegacion In sector.Permisos
                                        ' Adiciona se a permissão não existe na lista
                                        If Not objSector.Permisos.Contains(permiso.Nombre) Then
                                            objSector.Permisos.Add(permiso.Nombre)
                                        End If
                                    Next

                                    ' adiciona cada descrição de role para a delegação do loop
                                    For Each role As Prosegur.Global.Seguridad.ContractoServicio.Login.Role In sector.Roles
                                        ' Adiciona se a Role não existe na lista
                                        If Not objSector.Rol.Contains(role.Nombre) Then
                                            objSector.Rol.Add(role.Nombre)
                                        End If
                                    Next

                                    objPlanta.TiposSectores.Add(objSector)
                                Next

                                objDelegacion.Plantas.Add(objPlanta)
                            Next

                            ' adiciona a lista de delegações onde o usuário tem permisão
                            objRespuesta.InformacionUsuario.Delegaciones.Add(objDelegacion)

                        Next
                    Next

                End If

            ElseIf objRespuestaLogin.Codigo = 1 Then
                objRespuesta.MensajeError = objRespuestaLogin.Descripcion
                objRespuesta.CodigoError = objRespuestaLogin.Codigo
                objRespuesta.ResultadoOperacion = ContractoServ.Login.ResultadoOperacionLoginLocal.NoEsValido
            Else
                objRespuesta.MensajeError = objRespuestaLogin.Descripcion
                objRespuesta.CodigoError = objRespuestaLogin.Codigo
                objRespuesta.ResultadoOperacion = ContractoServ.Login.ResultadoOperacionLoginLocal.Error
            End If

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.ResultadoOperacion = ContractoServ.Login.ResultadoOperacionLoginLocal.Error

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.ResultadoOperacion = ContractoServ.Login.ResultadoOperacionLoginLocal.Error

        Finally
            objRespuesta.MensajeErrorDescriptiva = Util.TratarError(objRespuesta.MensajeError)
        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 08/02/2010 - Criado
    ''' </history>
    Public Function Test() As ContractoServ.Test.Respuesta Implements ContractoServ.ILogin.Test
        Dim objRespuesta As New ContractoServ.Test.Respuesta

        Try

            AccesoDatos.Test.TestarConexao()

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = Traduzir("001_SemErro")

        Catch ex As Excepcion.NegocioExcepcion

            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao


        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString

        Finally
            objRespuesta.MensajeErrorDescriptiva = Util.TratarError(objRespuesta.MensajeError)

        End Try

        Return objRespuesta
    End Function

End Class