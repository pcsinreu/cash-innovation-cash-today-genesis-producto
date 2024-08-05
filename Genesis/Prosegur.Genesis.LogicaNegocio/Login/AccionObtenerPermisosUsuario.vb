Imports Prosegur.Genesis.ContractoServicio.Login
Imports Prosegur.Global
Imports Prosegur.Framework.Dicionario.Tradutor

Public Class AccionObtenerPermisosUsuario

    Public Shared Function Ejecutar(Peticion As ObtenerPermisosUsuario.Peticion) As ObtenerPermisosUsuario.Respuesta

        Dim respuesta As New ObtenerPermisosUsuario.Respuesta

        Try

            If String.IsNullOrEmpty(Peticion.Login) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("003_atr_login")))
            End If

            Dim objRespuestaSeguridad As Seguridad.ContractoServicio.ObtenerPermisosUsuario.Respuesta = RecuperarPermisosUsuariosSeguridad(Peticion)

            If objRespuestaSeguridad IsNot Nothing Then

                If objRespuestaSeguridad.Codigo <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                    Throw New Excepcion.NegocioExcepcion(objRespuestaSeguridad.Codigo, objRespuestaSeguridad.Descripcion)
                End If

                respuesta.Continentes = ConverterContinentes(objRespuestaSeguridad.Continentes)

            End If

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

    Private Shared Function RecuperarPermisosUsuariosSeguridad(Peticion As ObtenerPermisosUsuario.Peticion) As Seguridad.ContractoServicio.ObtenerPermisosUsuario.Respuesta

        Dim objPeticionSeguridad As New Seguridad.ContractoServicio.ObtenerPermisosUsuario.Peticion
        Dim objProxyLogin As New LoginGlobal.Seguridad()

        objPeticionSeguridad.CodigoAplicacion = Peticion.CodigoAplicacion
        objPeticionSeguridad.CodigoDelegacion = Peticion.CodigoDelegacion
        objPeticionSeguridad.Login = Peticion.Login
        objPeticionSeguridad.CodigoPlanta = Peticion.CodigoPlanta
        objPeticionSeguridad.CodigoTipoSector = Peticion.CodigoTipoSector
        objPeticionSeguridad.RecuperarPermisos = Peticion.RecuperarPermisos
        objPeticionSeguridad.CodigoPais = Peticion.CodigoPais

        'Valida o usuário e retorna suas permissões/roles e supervisores.
        Return objProxyLogin.ObtenerPermisosUsuario(objPeticionSeguridad)

    End Function

    ''' <summary>
    ''' Converte os continentes
    ''' </summary>
    ''' <param name="objContinentesSeguridad"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ConverterContinentes(objContinentesSeguridad As Seguridad.ContractoServicio.ObtenerPermisosUsuario.ContinenteColeccion) As  _
        ObtenerPermisosUsuario.ContinenteColeccion

        Dim objContientes As ObtenerPermisosUsuario.ContinenteColeccion = Nothing

        If objContinentesSeguridad IsNot Nothing AndAlso objContinentesSeguridad.Count > 0 Then

            objContientes = New ObtenerPermisosUsuario.ContinenteColeccion

            For Each Continente In objContinentesSeguridad

                objContientes.Add(New ObtenerPermisosUsuario.Continente With { _
                                  .Nombre = Continente.Nombre, _
                                  .Paises = ConverterPaises(Continente.Paises)})
            Next

        End If

        Return objContientes
    End Function

    ''' <summary>
    ''' Converte os paises
    ''' </summary>
    ''' <param name="objPaisesSeguridad"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ConverterPaises(objPaisesSeguridad As Seguridad.ContractoServicio.ObtenerPermisosUsuario.PaisColeccion) As ObtenerPermisosUsuario.PaisColeccion

        Dim objPaises As ObtenerPermisosUsuario.PaisColeccion = Nothing

        If objPaisesSeguridad IsNot Nothing AndAlso objPaisesSeguridad.Count > 0 Then

            objPaises = New ObtenerPermisosUsuario.PaisColeccion

            For Each Pais In objPaisesSeguridad

                objPaises.Add(New ObtenerPermisosUsuario.Pais With { _
                              .Codigo = Pais.Codigo, _
                              .CodigoISODivisa = Pais.CodigoISODivisa, _
                              .Nombre = Pais.Nombre,
                              .Delegaciones = ConverterDelegaciones(Pais.Delegaciones)})
            Next

        End If

        Return objPaises
    End Function

    ''' <summary>
    ''' Converte as delegaçoes
    ''' </summary>
    ''' <param name="objDelegacionesSeguridad"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ConverterDelegaciones(objDelegacionesSeguridad As Seguridad.ContractoServicio.ObtenerPermisosUsuario.DelegacionColeccion) As  _
        ObtenerPermisosUsuario.DelegacionColeccion

        Dim objDelegaciones As ObtenerPermisosUsuario.DelegacionColeccion = Nothing

        If objDelegacionesSeguridad IsNot Nothing AndAlso objDelegacionesSeguridad.Count > 0 Then

            objDelegaciones = New ObtenerPermisosUsuario.DelegacionColeccion

            For Each Delegacion In objDelegacionesSeguridad

                objDelegaciones.Add(New ObtenerPermisosUsuario.Delegacion With { _
                                    .CantidadMetrosBase = Delegacion.CantidadMetrosBase, _
                                    .CantidadMinutosIni = Delegacion.CantidadMinutosIni, _
                                    .CantidadMinutosSalida = Delegacion.CantidadMinutosSalida, _
                                    .Codigo = Delegacion.Codigo, _
                                    .GMT = Delegacion.GMT, _
                                    .Nombre = Delegacion.Nombre, _
                                    .VeranoAjuste = Delegacion.VeranoAjuste, _
                                    .VeranoFechaHoraFin = Delegacion.VeranoFechaHoraFin, _
                                    .VeranoFechaHoraIni = Delegacion.VeranoFechaHoraIni, _
                                    .Zona = Delegacion.Zona, _
                                    .Plantas = ConverterPlantas(Delegacion.Plantas)})
            Next

        End If

        Return objDelegaciones
    End Function

    ''' <summary>
    ''' Converte as plantas
    ''' </summary>
    ''' <param name="objPlantasSeguridad"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ConverterPlantas(objPlantasSeguridad As Seguridad.ContractoServicio.ObtenerPermisosUsuario.PlantaColeccion) As ObtenerPermisosUsuario.PlantaColeccion

        Dim objPlantas As ObtenerPermisosUsuario.PlantaColeccion = Nothing

        If objPlantasSeguridad IsNot Nothing AndAlso objPlantasSeguridad.Count > 0 Then

            objPlantas = New ObtenerPermisosUsuario.PlantaColeccion

            For Each Planta In objPlantasSeguridad

                objPlantas.Add(New ObtenerPermisosUsuario.Planta With { _
                               .Codigo = Planta.Codigo, _
                               .Descricao = Planta.Descricao, _
                               .TiposSectores = ConverterTiposSectores(Planta.TiposSectores)})

            Next

        End If

        Return objPlantas
    End Function

    ''' <summary>
    ''' Convete os tipos de sectores
    ''' </summary>
    ''' <param name="objTiposSectoresSeguridad"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ConverterTiposSectores(objTiposSectoresSeguridad As Seguridad.ContractoServicio.ObtenerPermisosUsuario.TipoSectorColeccion) As ObtenerPermisosUsuario.TipoSectorColeccion

        Dim objTiposSectores As ObtenerPermisosUsuario.TipoSectorColeccion = Nothing

        If objTiposSectoresSeguridad IsNot Nothing AndAlso objTiposSectoresSeguridad.Count > 0 Then

            objTiposSectores = New ObtenerPermisosUsuario.TipoSectorColeccion

            For Each TipoSector In objTiposSectoresSeguridad

                objTiposSectores.Add(New ObtenerPermisosUsuario.TipoSector With { _
                                     .Codigo = TipoSector.Codigo, _
                                     .Permisos = ConverterPermisos(TipoSector.Permisos)})
            Next

        End If

        Return objTiposSectores
    End Function

    ''' <summary>
    ''' Converte as permissoes
    ''' </summary>
    ''' <param name="objPermisosSeguridad"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ConverterPermisos(objPermisosSeguridad As Seguridad.ContractoServicio.ObtenerPermisosUsuario.PermisoColeccion) As ObtenerPermisosUsuario.PermisoColeccion

        Dim objPermisos As ObtenerPermisosUsuario.PermisoColeccion = Nothing

        If objPermisosSeguridad IsNot Nothing AndAlso objPermisosSeguridad.Count > 0 Then

            objPermisos = New ObtenerPermisosUsuario.PermisoColeccion

            For Each Permiso In objPermisosSeguridad

                objPermisos.Add(New ObtenerPermisosUsuario.Permiso With { _
                                .CodigoAplicacion = Permiso.CodigoAplicacion, _
                                .DescripcionAplicacion = Permiso.DescripcionAplicacion, _
                                .CodigoPermiso = Permiso.CodigoPermiso})

            Next

        End If

        Return objPermisos
    End Function
End Class
