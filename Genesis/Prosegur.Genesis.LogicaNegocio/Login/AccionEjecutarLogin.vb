Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Global

Public Class AccionEjecutarLogin

    ''' <summary>
    ''' fluxo principal da operação
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [prezende]  23/05/2012  criado
    ''' </history>
    Public Shared Function Ejecutar(Peticion As Login.EjecutarLogin.Peticion) As Login.EjecutarLogin.Respuesta

        Dim respuesta As New Login.EjecutarLogin.Respuesta

        Try

            ValidarPeticion(Peticion)
            EjecutarLoginDelegacion(Peticion, respuesta)

        Catch ex As Excepcion.NegocioExcepcion

            respuesta.CodigoError = ex.Codigo
            respuesta.MensajeError = ex.Descricao
            respuesta.ResultadoOperacion = Login.EjecutarLogin.ResultadoOperacionLoginLocal.Error

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            respuesta.MensajeError = ex.ToString()
            respuesta.ResultadoOperacion = Login.EjecutarLogin.ResultadoOperacionLoginLocal.Error

        End Try

        Return respuesta

    End Function

    ''' <summary>
    ''' Ejecuta o login na Delegação é recupera todas as versões das aplicações que o usuário tem permissão
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <param name="respuesta"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 15/06/2012 Criado
    ''' </history>
    Public Shared Function EjecutarLoginDelegacion(Peticion As Login.EjecutarLogin.Peticion, ByRef respuesta As Login.EjecutarLogin.Respuesta)

        Dim objRespuestaLogin As Seguridad.ContractoServicio.LoginDelegacion.Respuesta
        'Dim respuesta As Login.EjecutarLogin.Respuesta

        ' efetua login do usuário e retorna permissões
        objRespuestaLogin = EjecutarLoginDelegacion(Peticion)

        If objRespuestaLogin.Codigo = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

            ' inicializa a variável
            respuesta.Usuario = New Login.EjecutarLogin.Usuario()

            ' aplicacionversion
            respuesta.Aplicaciones = New Login.EjecutarLogin.AplicacionVersionColeccion()

            ' preenche mensagem de resposta
            With respuesta
                .ResultadoOperacion = Login.EjecutarLogin.ResultadoOperacionLoginLocal.Autenticado
                .Usuario.Identificador = objRespuestaLogin.Usuario.Identificador
                .Usuario.Contrasena = objRespuestaLogin.Usuario.Contrasena
                .Usuario.Apellido = objRespuestaLogin.Usuario.Apellido
                .Usuario.Nombre = objRespuestaLogin.Usuario.Nombre
                .Usuario.Idioma = objRespuestaLogin.Usuario.Idioma
                .Usuario.OidUsuario = objRespuestaLogin.Usuario.OidUsuario
                .Usuario.Login = objRespuestaLogin.Usuario.DesLogin.ToUpper
                .Usuario.Password = Peticion.Password

                For Each aplicacionVersion In objRespuestaLogin.Aplicaciones

                    respuesta.Aplicaciones.Add(New Login.EjecutarLogin.AplicacionVersion() With { _
                    .OidAplicacion = aplicacionVersion.OidAplicacion, _
                    .CodigoAplicacion = aplicacionVersion.CodigoAplicacion, _
                    .CodigoVersion = aplicacionVersion.CodigoVersion, _
                    .DescripcionAplicacion = aplicacionVersion.DescripcionAplicacion, _
                    .CodigoBuild = aplicacionVersion.CodigoBuild, _
                    .DesURLServicio = aplicacionVersion.DesURLServicio,
                    .DesURLSitio = aplicacionVersion.DesURLSitio})

                Next

            End With

            ' converte os dados dos continentes, paises, etc
            Dim conv As New Conversor
            respuesta.Usuario.Continentes = conv.RealizarConversao(objRespuestaLogin, Peticion.TrabajaPorPlanta)

        ElseIf objRespuestaLogin.Codigo = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT Then
            respuesta.MensajeError = objRespuestaLogin.Descripcion
            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            respuesta.ResultadoOperacion = Login.EjecutarLogin.ResultadoOperacionLoginLocal.NoEsValido
        ElseIf objRespuestaLogin.Codigo = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
            respuesta.MensajeError = objRespuestaLogin.Descripcion
            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            respuesta.ResultadoOperacion = Login.EjecutarLogin.ResultadoOperacionLoginLocal.VersionAplicacionNoEncontrada
        Else
            respuesta.MensajeError = objRespuestaLogin.Descripcion
            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            respuesta.ResultadoOperacion = Login.EjecutarLogin.ResultadoOperacionLoginLocal.Error
        End If

        Return respuesta

    End Function

    ''' <summary>
    ''' Executa serviço Login Delegacion
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [prezende]  23/05/2012  criado
    ''' </history>
    Private Shared Function EjecutarLoginDelegacion(Peticion As Login.EjecutarLogin.Peticion) As Seguridad.ContractoServicio.LoginDelegacion.Respuesta

        Dim objPeticionLogin As New Seguridad.ContractoServicio.LoginDelegacion.Peticion()
        Dim objProxyLogin As New LoginGlobal.Seguridad()

        objPeticionLogin.NombreAplicacion = Peticion.CodigoAplicacion
        objPeticionLogin.CodigoDelegacion = Peticion.CodigoDelegacion
        objPeticionLogin.Login = Peticion.Login
        objPeticionLogin.Password = Peticion.Password
        objPeticionLogin.CodigoPlanta = Peticion.Planta
        objPeticionLogin.VersionAplicacion = Peticion.VersionAplicacion

        'Valida o usuário e retorna suas permissões/roles e supervisores.
        Return objProxyLogin.LoginDelegacion(objPeticionLogin)

    End Function

    ''' <summary>
    ''' Sobrecarga do método para buscar as delegaciones do usuário
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <param name="respuesta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.seabra]  07/03/2013  criado
    ''' </history>
    Public Shared Function EjecutarLoginDelegacion(Peticion As Login.GetDelegacionesUsuario.Peticion, ByRef respuesta As Login.GetDelegacionesUsuario.Respuesta)

        Dim objRespuestaLogin As New Seguridad.ContractoServicio.LoginDelegacion.Respuesta
        Dim objEntrada As New Login.EjecutarLogin.Peticion

        objEntrada.CodigoDelegacion = String.Empty
        objEntrada.EsWeb = False
        objEntrada.Login = Peticion.Login
        objEntrada.Password = Peticion.Password
        objEntrada.VersionAplicacion = Peticion.VersionAplicacion

        ' efetua login do usuário e retorna permissões
        objRespuestaLogin = EjecutarLoginDelegacion(objEntrada)

        If objRespuestaLogin.Codigo = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

            For Each objDelegacion As Seguridad.ContractoServicio.LoginDelegacion.DelegacionLoginDelegacion In objRespuestaLogin.Delegaciones
                respuesta.Delegaciones.Add(New Login.GetDelegacionesUsuario.Delegacion() With { _
                                           .Codigo = objDelegacion.Codigo, _
                                           .Descricao = objDelegacion.Nombre})
            Next

        ElseIf objRespuestaLogin.Codigo = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT Then
            respuesta.MensajeError = objRespuestaLogin.Descripcion
            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            ' respuesta.ResultadoOperacion = Login.EjecutarLogin.ResultadoOperacionLoginLocal.NoEsValido
        ElseIf objRespuestaLogin.Codigo = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
            respuesta.MensajeError = objRespuestaLogin.Descripcion
            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            ' respuesta.ResultadoOperacion = Login.EjecutarLogin.ResultadoOperacionLoginLocal.VersionAplicacionNoEncontrada
        Else
            respuesta.MensajeError = objRespuestaLogin.Descripcion
            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            ' respuesta.ResultadoOperacion = Login.EjecutarLogin.ResultadoOperacionLoginLocal.Error
        End If

        Return respuesta

    End Function



    ''' <summary>
    ''' valida parámetro de entrada
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  19/01/2011  criado
    ''' </history>
    Private Shared Sub ValidarPeticion(Peticion As Login.EjecutarLogin.Peticion)

        ' Valida se o campo Login não é nulo ou vazio.
        If String.IsNullOrEmpty(Peticion.Login) Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("003_atr_login")))

        End If

        ' Valida se o campo Password não é nulo ou vazio.
        If String.IsNullOrEmpty(Peticion.Password) Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("003_atr_password")))

        End If


    End Sub

#Region "[CLASSES]"

    ''' <summary>
    ''' classe utilizada para converter classes do namespace Seguridad.ContractoServicio.Login para ATM.ContractoServicio.Login
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  19/01/2011  criado
    ''' </history>
    Private Class Conversor

#Region "[VARIÁVEIS]"

        Dim timeout As Integer = Integer.MaxValue

#End Region

#Region "[MÉTODOS]"

        ''' <summary>
        ''' Retorna uma lista de componentes com dos dados recebidos do serviço login
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  19/01/2011  criado
        ''' </history>
        Public Function RealizarConversao(ObjRespuestaLogin As Seguridad.ContractoServicio.LoginDelegacion.Respuesta,
                                          TrabajaPorPlanta As Boolean) As List(Of Login.EjecutarLogin.Continente)

            Dim continentes As New List(Of Login.EjecutarLogin.Continente)
            Dim objContinente As Login.EjecutarLogin.Continente

            If ObjRespuestaLogin.Usuario.Continentes IsNot Nothing AndAlso ObjRespuestaLogin.Usuario.Continentes.Count > 0 Then

                For Each continente As Seguridad.ContractoServicio.LoginDelegacion.ContinenteLoginDelegacion In ObjRespuestaLogin.Usuario.Continentes

                    objContinente = New Login.EjecutarLogin.Continente()
                    continentes.Add(objContinente)
                    objContinente.Nombre = continente.Nombre

                    ' adiciona dados dos paises do continente
                    For Each pais As Seguridad.ContractoServicio.LoginDelegacion.PaisLoginDelegacion In continente.Paises
                        objContinente.Paises.Add(ConvertToPais(pais, TrabajaPorPlanta))
                    Next

                Next

            End If

            Return continentes

        End Function

        ''' <summary>
        ''' Converte um objeto do tipo Seguridad.ContractoServicio.Login.Pais para ATM.ContractoServicio.Login.Pais
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  19/01/2011  criado
        ''' </history>
        Private Function ConvertToPais(Pais As Seguridad.ContractoServicio.LoginDelegacion.PaisLoginDelegacion, TrabajaPorPlanta As Boolean) As Login.EjecutarLogin.Pais

            Dim objPais As New Login.EjecutarLogin.Pais

            objPais.Codigo = Pais.Codigo
            objPais.CodigoISODivisa = Pais.CodigoISODivisa
            objPais.Nombre = Pais.Nombre

            If Pais.Delegaciones IsNot Nothing AndAlso Pais.Delegaciones.Count > 0 Then

                ' adiciona delegações do país
                For Each delegacion As Seguridad.ContractoServicio.LoginDelegacion.DelegacionLoginDelegacion In Pais.Delegaciones
                    objPais.Delegaciones.Add(ConvertToDelegacion(delegacion, TrabajaPorPlanta))
                Next

            End If

            Return objPais

        End Function

        ''' <summary>
        ''' Converte um objeto do tipo Seguridad.ContractoServicio.Login.Pais para ATM.ContractoServicio.Login.Pais
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  19/01/2011  criado
        ''' </history>
        Private Function ConvertToDelegacion(Delegacion As Seguridad.ContractoServicio.LoginDelegacion.DelegacionLoginDelegacion, TrabajaPorPlanta As Boolean) As Login.EjecutarLogin.Delegacion

            Dim objDelegacion As Login.EjecutarLogin.Delegacion = Nothing

            If TrabajaPorPlanta Then
                objDelegacion = New Login.EjecutarLogin.DelegacionPlanta
            Else
                objDelegacion = New Login.EjecutarLogin.Delegacion
            End If

            objDelegacion.CantidadMetrosBase = Delegacion.CantidadMetrosBase
            objDelegacion.CantidadMinutosIni = Delegacion.CantidadMinutosIni
            objDelegacion.CantidadMinutosSalida = Delegacion.CantidadMinutosSalida
            objDelegacion.Codigo = Delegacion.Codigo
            objDelegacion.GMT = Delegacion.GMT
            objDelegacion.Identificador = Delegacion.Identificador
            objDelegacion.Nombre = Delegacion.Nombre
            objDelegacion.VeranoAjuste = Delegacion.VeranoAjuste
            objDelegacion.VeranoFechaHoraFin = Delegacion.VeranoFechaHoraFin
            objDelegacion.VeranoFechaHoraIni = Delegacion.VeranoFechaHoraIni
            objDelegacion.Zona = Delegacion.Zona

            If Delegacion.DelegacionesLegado IsNot Nothing AndAlso Delegacion.DelegacionesLegado.Count > 0 Then

                ' adiciona delegações legado
                For Each delegacionLegado As Seguridad.ContractoServicio.Login.DelegacionLegado In Delegacion.DelegacionesLegado
                    objDelegacion.DelegacionesLegado.Add(ConvertToDelegacionLegado(delegacionLegado))
                Next

            End If

            If Delegacion.Plantas IsNot Nothing AndAlso Delegacion.Plantas.Count > 0 Then

                ' adiciona plantas da delegação
                For Each planta As Seguridad.ContractoServicio.LoginDelegacion.PlantaLoginDelegacion In Delegacion.Plantas

                    If TrabajaPorPlanta Then

                        DirectCast(objDelegacion, Login.EjecutarLogin.DelegacionPlanta).Plantas.Add(ConvertirPlanta(planta))

                    End If

                    If planta.Sectores IsNot Nothing AndAlso planta.Sectores.Count > 0 Then

                        For Each Sector In planta.Sectores

                            If objDelegacion.Sectores.FindAll(Function(s) s.Identificador = Sector.Identificador).Count = 0 Then
                                objDelegacion.Sectores.Add(ConvertToSector(Sector))
                            End If

                        Next

                    End If

                Next

            End If

            Return objDelegacion

        End Function


        ''' <summary>
        ''' Converte um objeto do tipo Seguridad.ContractoServicio.LoginDelegacion.PlantaLoginDelegacion para Login.EjecutarLogin.Planta
        ''' </summary>
        ''' <param name="Planta">Planta.</param>
        ''' <returns></returns>
        ''' <history>
        ''' [guilherme.corsino]  24/09/2013  criado
        '''   </history>
        Private Function ConvertirPlanta(Planta As Seguridad.ContractoServicio.LoginDelegacion.PlantaLoginDelegacion) As Login.EjecutarLogin.Planta

            Dim objPlanta As New Login.EjecutarLogin.Planta

            objPlanta.CodigoPlanta = Planta.Codigo
            objPlanta.DesPlanta = Planta.Descricao
            objPlanta.oidPlanta = Planta.Identificador

            If Planta.Sectores IsNot Nothing AndAlso Planta.Sectores.Count > 0 Then

                For Each sector As Seguridad.ContractoServicio.LoginDelegacion.SectorLoginDelegacion In Planta.Sectores
                    objPlanta.TiposSectores.Add(ConvertToTipoSector(sector))
                Next

            End If

            Return objPlanta

        End Function


        ''' <summary>
        ''' Converte um objeto do tipo Seguridad.ContractoServicio.Login.Sector para ATM.ContractoServicio.Login.Sector
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  19/01/2011  criado
        ''' </history>
        Private Function ConvertToTipoSector(Sector As Seguridad.ContractoServicio.LoginDelegacion.SectorLoginDelegacion) As Login.EjecutarLogin.TipoSector

            Dim objTipoSector As New Login.EjecutarLogin.TipoSector

            objTipoSector.Codigo = Sector.Codigo
            objTipoSector.Identificador = Sector.Identificador

            If Sector.Permisos IsNot Nothing AndAlso Sector.Permisos.Count > 0 Then

                For Each permiso As Seguridad.ContractoServicio.LoginDelegacion.PermisoLoginDelegacion In Sector.Permisos
                    objTipoSector.Permisos.Add(ConvertToPermiso(permiso))
                Next

            End If

            Return objTipoSector

        End Function

        ''' <summary>
        ''' Converte um objeto do tipo Seguridad.ContractoServicio.Login.Sector para ATM.ContractoServicio.Login.Sector
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  19/01/2011  criado
        ''' </history>
        Private Function ConvertToSector(Sector As Seguridad.ContractoServicio.LoginDelegacion.SectorLoginDelegacion) As Login.EjecutarLogin.Sector

            Dim objSector As New Login.EjecutarLogin.Sector

            objSector.Codigo = Sector.Codigo
            objSector.Identificador = Sector.Identificador

            If Sector.Permisos IsNot Nothing AndAlso Sector.Permisos.Count > 0 Then

                For Each permiso As Seguridad.ContractoServicio.LoginDelegacion.PermisoLoginDelegacion In Sector.Permisos
                    objSector.Permisos.Add(ConvertToPermiso(permiso))
                Next

            End If

            Return objSector

        End Function


        ''' <summary>
        ''' Converte um objeto do tipo Seguridad.ContractoServicio.Login.Permiso para ATM.ContractoServicio.Login.Permiso
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  19/01/2011  criado
        ''' </history>
        Private Function ConvertToPermiso(Permiso As Seguridad.ContractoServicio.LoginDelegacion.PermisoLoginDelegacion) As Login.EjecutarLogin.Permiso

            Dim objPermiso As New Login.EjecutarLogin.Permiso

            objPermiso.CodigoAplicacion = Permiso.CodigoAplicacion
            objPermiso.Nombre = Permiso.Nombre

            Return objPermiso

        End Function

        ''' <summary>
        ''' Converte um objeto do tipo Seguridad.ContractoServicio.Login.Role para ATM.ContractoServicio.Login.Role
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  19/01/2011  criado
        ''' </history>
        Private Function ConvertToRole(Role As Seguridad.ContractoServicio.Login.Role) As Login.EjecutarLogin.Role

            Dim objRole As New Login.EjecutarLogin.Role

            objRole.Nombre = Role.Nombre
            'Armazena as roles e guarda o timeout mais restritivo
            If Convert.ToInt32(Role.Timeout) = 0 Then
                Role.Timeout = Parametros.Configuracion.Caducidad
            End If

            objRole.Timeout = Role.Timeout
            If Convert.ToInt32(objRole.Timeout) < timeout Then
                timeout = objRole.Timeout
            End If

            Return objRole

        End Function

        ''' <summary>
        ''' Converte um objeto do tipo Seguridad.ContractoServicio.Login.DelegacionLegado para ContractoServicio.Login.DelegacionLegado
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  19/01/2011  criado
        ''' </history>
        Private Function ConvertToDelegacionLegado(DelegacionLegado As Seguridad.ContractoServicio.Login.DelegacionLegado) As Login.EjecutarLogin.DelegacionLegado

            Dim objDelegacionLegado As New Login.EjecutarLogin.DelegacionLegado

            objDelegacionLegado.Aplicacion = DelegacionLegado.Aplicacion
            objDelegacionLegado.Codigo = DelegacionLegado.Codigo

            Return objDelegacionLegado

        End Function

#End Region

    End Class

#End Region
    Public Shared Function EjecutarLoginAplicacion(Peticion As Login.EjecutarLoginAplicacion.Peticion) As Login.EjecutarLoginAplicacion.Respuesta
        ' crir objeto respuesta
        Dim objRespuesta As New ContractoServicio.Login.EjecutarLoginAplicacion.Respuesta

        Try

            ' Efetuar login ------------------------
            Dim objPeticionLogin As New Seguridad.ContractoServicio.LoginDelegacion.Peticion()
            Dim objRespuestaLogin As Seguridad.ContractoServicio.LoginDelegacion.Respuesta
            Dim objProxyLogin As New LoginGlobal.Seguridad()

            objPeticionLogin.NombreAplicacion = Peticion.Aplicacion
            objPeticionLogin.CodigoDelegacion = Peticion.Delegacion
            objPeticionLogin.CodigoPlanta = Peticion.Planta
            objPeticionLogin.Login = Peticion.IdentificadorUsuario.ToUpper
            objPeticionLogin.Password = Peticion.Password
            objPeticionLogin.VersionAplicacion = Peticion.VersionAplicacion

            ' valida o usuário e retorna suas permissões/roles e supervisores.
            objRespuestaLogin = objProxyLogin.LoginDelegacion(objPeticionLogin)
            ' --------------------------------------

            ' se validou usuário com sucesso
            If objRespuestaLogin.Codigo = 0 Then '= Seguridad.ContractoServicio.Login.ResultadoOperacionLogin.Autenticado Then 'Sucesso

                ' preencher objeto respuesta
                objRespuesta.ResultadoOperacion = ContractoServicio.Login.EjecutarLoginAplicacion.ResultadoOperacionLoginLocal.Autenticado
                objRespuesta.InformacionUsuario.Apelido = objRespuestaLogin.Usuario.Apellido
                objRespuesta.InformacionUsuario.Nombre = objRespuestaLogin.Usuario.Nombre

                If objRespuestaLogin.Usuario.Continentes.Count > 0 AndAlso
                objRespuestaLogin.Usuario.Continentes(0).Paises.Count > 0 AndAlso
                objRespuestaLogin.Usuario.Continentes(0).Paises(0).Delegaciones.Count > 0 AndAlso
                objRespuestaLogin.Usuario.Continentes(0).Paises(0).Delegaciones(0).Sectores.Count > 0 Then

                    ' Para cada continente existente
                    For Each continente As Prosegur.Global.Seguridad.ContractoServicio.LoginDelegacion.ContinenteLoginDelegacion In objRespuestaLogin.Usuario.Continentes

                        ' Para cada pais existente (Busca o pais passado como parâmetro)
                        For Each pais As Prosegur.Global.Seguridad.ContractoServicio.LoginDelegacion.PaisLoginDelegacion In continente.Paises.Where(Function(p) p.Codigo = Peticion.Pais OrElse String.IsNullOrEmpty(Peticion.Pais))

                            ' para cada delegação retornada do serviço seguridad, inserir na lista de informações do usuário
                            For Each delegacion As Prosegur.Global.Seguridad.ContractoServicio.LoginDelegacion.DelegacionLoginDelegacion In pais.Delegaciones

                                ' Cria o objeto delegacion do contrato serviço Reportes
                                Dim objDelegacion As New ContractoServicio.Login.EjecutarLoginAplicacion.Delegacion
                                objDelegacion.Codigo = delegacion.Codigo
                                objDelegacion.Descripcion = delegacion.Nombre

                                For Each Planta As Prosegur.Global.Seguridad.ContractoServicio.LoginDelegacion.PlantaLoginDelegacion In delegacion.Plantas

                                    Dim objPlanta As New ContractoServicio.Login.EjecutarLoginAplicacion.Planta

                                    objPlanta.Codigo = Planta.Codigo
                                    objPlanta.Descricao = Planta.Descricao
                                    objPlanta.Identificador = Planta.Identificador

                                    ' adiciona cada descrição de permiso para a delegação e sector
                                    For Each sector As Prosegur.Global.Seguridad.ContractoServicio.LoginDelegacion.SectorLoginDelegacion In Planta.Sectores

                                        Dim objTipoSector As New ContractoServicio.Login.EjecutarLoginAplicacion.TipoSector

                                        objTipoSector.Codigo = sector.Codigo

                                        For Each permiso As Prosegur.Global.Seguridad.ContractoServicio.LoginDelegacion.PermisoLoginDelegacion In sector.Permisos
                                            ' Adiciona se a permissão não existe na lista
                                            If Not objTipoSector.Permisos.Contains(permiso.Nombre) Then
                                                objTipoSector.Permisos.Add(permiso.Nombre)
                                            End If
                                        Next

                                        ' adiciona cada descrição de role para a delegação do loop
                                        For Each role As Prosegur.Global.Seguridad.ContractoServicio.Login.Role In sector.Roles
                                            ' Adiciona se a Role não existe na lista
                                            If Not objTipoSector.Rol.Contains(role.Nombre) Then
                                                objTipoSector.Rol.Add(role.Nombre)
                                            End If
                                        Next

                                        objPlanta.TiposSectores.Add(objTipoSector)
                                    Next

                                    objDelegacion.Plantas.Add(objPlanta)
                                Next

                                ' adiciona a lista de delegações onde o usuário tem permisão
                                objRespuesta.InformacionUsuario.Delegaciones.Add(objDelegacion)

                            Next

                        Next

                    Next

                End If

            ElseIf objRespuestaLogin.Codigo = 1 Then
                objRespuesta.MensajeError = objRespuestaLogin.Descripcion
                objRespuesta.CodigoError = objRespuestaLogin.Codigo
                objRespuesta.ResultadoOperacion = ContractoServicio.Login.EjecutarLoginAplicacion.ResultadoOperacionLoginLocal.NoEsValido
            Else
                objRespuesta.MensajeError = objRespuestaLogin.Descripcion
                objRespuesta.CodigoError = objRespuestaLogin.Codigo
                objRespuesta.ResultadoOperacion = ContractoServicio.Login.EjecutarLoginAplicacion.ResultadoOperacionLoginLocal.Error
            End If

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.ResultadoOperacion = ContractoServicio.Login.EjecutarLoginAplicacion.ResultadoOperacionLoginLocal.Error

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.ResultadoOperacion = ContractoServicio.Login.EjecutarLoginAplicacion.ResultadoOperacionLoginLocal.Error

        Finally
            'objRespuesta.MensajeErrorDescriptiva = Util.TratarError(objRespuesta.MensajeError)
        End Try

        Return objRespuesta
    End Function




End Class