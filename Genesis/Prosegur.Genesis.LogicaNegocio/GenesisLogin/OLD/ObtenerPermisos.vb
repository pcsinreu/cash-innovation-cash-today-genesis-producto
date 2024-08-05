Imports ContractoLogin = Prosegur.Genesis.ContractoServicio.GenesisLogin
Imports ContractosSeguridad = Prosegur.Global.Seguridad.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Login.EjecutarLogin
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.LogicaNegocio

Namespace AccionGenesisLogin

    Public Class ObtenerPermisos

        ''' <summary>
        ''' Flujo principal de la operación
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Ejecutar(Peticion As ContractoLogin.ObtenerPermisos.Peticion) As ContractoLogin.ObtenerPermisos.Respuesta

            Dim respuesta As New ContractoLogin.ObtenerPermisos.Respuesta

            Try

                ValidarPeticion(Peticion)

                Dim respuestaSeguridad As ContractosSeguridad.Genesis.ObtenerPermisos.Respuesta = llamarSeguridad(Peticion)

                TransformaObjectoRespuesta(respuesta, respuestaSeguridad)

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
        ''' Validar los parámetros de entrada
        ''' </summary>
        Private Shared Sub ValidarPeticion(Peticion As ContractoLogin.ObtenerPermisos.Peticion)
            ' Valida se o campo identificadorUsuario não é nulo ou vazio.
            If String.IsNullOrEmpty(Peticion.identificadorUsuario) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("gen_atr_obtenerpermisos_identificadorusuario")))
            End If
            ' Valida se o campo identificadorAplicacion não é nulo ou vazio.
            If String.IsNullOrEmpty(Peticion.identificadorAplicacion) Then
                Throw New ApplicationException(String.Format(Traduzir("Gen_msg_atributo"), Traduzir("gen_atr_obtenerpermisos_identificadoraplicacion")))
            End If
            ' Valida se o campo codigoPais não é nulo ou vazio.
            If String.IsNullOrEmpty(Peticion.codigoPais) Then
                Throw New ApplicationException(String.Format(Traduzir("Gen_msg_atributo"), Traduzir("gen_atr_obtenerpermisos_codigopais")))
            End If
        End Sub

        ''' <summary>
        ''' Comunicación con el servicio de Seguridad
        ''' </summary>
        ''' <param name="peticion"></param>
        Private Shared Function llamarSeguridad(peticion As ContractoLogin.ObtenerPermisos.Peticion) As ContractosSeguridad.Genesis.ObtenerPermisos.Respuesta

            Dim proxySeguridad As New Proxy.Seguridad()
            Dim peticionSeguridad As New ContractosSeguridad.Genesis.ObtenerPermisos.Peticion() With {
                .codigoDelegacion = peticion.codigoDelegacion,
                .codigoPais = peticion.codigoPais,
                .codigoPlanta = peticion.codigoPlanta,
                .identificadorAplicacion = peticion.identificadorAplicacion,
                .identificadorUsuario = peticion.identificadorUsuario
            }

            Return proxySeguridad.GenesisObtenerPermisos(peticionSeguridad)

        End Function

        Private Shared Sub TransformaObjectoRespuesta(ByRef respuesta As ContractoServicio.GenesisLogin.ObtenerPermisos.Respuesta,
                                                      respuestaSeguridad As ContractosSeguridad.Genesis.ObtenerPermisos.Respuesta)

            If Not IsNothing(respuestaSeguridad.Delegaciones) Then
                For Each objDelegacionSeguridad In respuestaSeguridad.Delegaciones
                    Dim objDelegacion As New DelegacionPlanta() With {
                        .CantidadMetrosBase = objDelegacionSeguridad.CantidadMetrosBase,
                        .CantidadMinutosIni = objDelegacionSeguridad.CantidadMinutosIni,
                        .CantidadMinutosSalida = objDelegacionSeguridad.CantidadMinutosSalida,
                        .Codigo = objDelegacionSeguridad.Codigo,
                        .GMT = objDelegacionSeguridad.GMT,
                        .Identificador = objDelegacionSeguridad.Identificador,
                        .Nombre = objDelegacionSeguridad.Descripcion,
                        .VeranoAjuste = objDelegacionSeguridad.VeranoAjuste,
                        .VeranoFechaHoraFin = objDelegacionSeguridad.VeranoFechaHoraFin,
                        .VeranoFechaHoraIni = objDelegacionSeguridad.VeranoFechaHoraIni,
                        .Zona = objDelegacionSeguridad.Zona
                    }

                    If Not IsNothing(objDelegacionSeguridad.Plantas) Then
                        objDelegacion.Plantas = TransformaPlantas(objDelegacionSeguridad.Plantas)
                    End If

                    respuesta.Delegaciones.Add(objDelegacion)
                Next
            End If

        End Sub

        Private Shared Function TransformaPlantas(plantasSeguridad As List(Of ContractosSeguridad.Genesis.Comun.Planta)) As List(Of Planta)
            Dim listaPlantas As New List(Of Planta)()

            For Each objPlantaSeguridad In plantasSeguridad
                Dim objPlanta As New Planta() With {
                    .CodigoPlanta = objPlantaSeguridad.Codigo,
                    .DesPlanta = objPlantaSeguridad.Descripcion,
                    .oidPlanta = objPlantaSeguridad.Identificador
                }

                If Not IsNothing(objPlantaSeguridad.TipoSectores) Then
                    objPlanta.TiposSectores = TransformaTiposSectores(objPlantaSeguridad.TipoSectores)
                End If

                listaPlantas.Add(objPlanta)
            Next

            Return listaPlantas
        End Function

        Private Shared Function TransformaTiposSectores(tiposSectorSeguridad As List(Of ContractosSeguridad.Genesis.Comun.TipoSector)) As List(Of TipoSector)
            Dim listaTiposSectores As New List(Of TipoSector)()

            For Each objTipoSectorSeguridad In tiposSectorSeguridad
                Dim objTipoSector As New TipoSector() With {
                    .Codigo = objTipoSectorSeguridad.Codigo,
                    .Identificador = objTipoSectorSeguridad.Identificador
                }

                If Not IsNothing(objTipoSectorSeguridad.Aplicaciones) Then
                    objTipoSector.Permisos = TransformaPermisos(objTipoSectorSeguridad.Aplicaciones)
                End If

                listaTiposSectores.Add(objTipoSector)
            Next

            Return listaTiposSectores
        End Function

        Private Shared Function TransformaPermisos(aplicacionesSeguridad As List(Of ContractosSeguridad.Genesis.Comun.Aplicacion)) As List(Of Permiso)
            Dim listaPermisos As New List(Of Permiso)()

            For Each objAplicacionSeguridad In aplicacionesSeguridad
                If Not IsNothing(objAplicacionSeguridad.Permisos) Then
                    For Each objPermisoSeguridad In objAplicacionSeguridad.Permisos
                        Dim objPermiso As New Permiso() With {
                            .CodigoAplicacion = objAplicacionSeguridad.codigoAplicacion,
                            .Nombre = objPermisoSeguridad.Codigo
                        }

                        listaPermisos.Add(objPermiso)
                    Next
                End If
            Next

            Return listaPermisos
        End Function

    End Class

End Namespace

