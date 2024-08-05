Imports ContractoLogin = Prosegur.Genesis.ContractoServicio.GenesisLogin
Imports ContractosSeguridad = Prosegur.Global.Seguridad.ContractoServicio
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.LogicaNegocio
Imports Prosegur.Genesis.ContractoServicio.Login.EjecutarLogin

Namespace AccionGenesisLogin

    Public Class ObtenerDelegaciones

        ''' <summary>
        ''' Flujo principal de la operación
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Ejecutar(Peticion As ContractoLogin.ObtenerDelegaciones.Peticion) As ContractoLogin.ObtenerDelegaciones.Respuesta

            Dim respuesta As New ContractoLogin.ObtenerDelegaciones.Respuesta

            Try

                ValidarPeticion(Peticion)

                Dim respuestaSeguridad As ContractosSeguridad.Genesis.ObtenerDelegaciones.Respuesta = llamarSeguridad(Peticion)

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
        Private Shared Sub ValidarPeticion(Peticion As ContractoLogin.ObtenerDelegaciones.Peticion)
            ' Valida se o campo identificadorUsuario não é nulo ou vazio.
            If String.IsNullOrEmpty(Peticion.identificadorUsuario) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("gen_atr_obtenerdelegaciones_identificadorusuario")))
            End If
        End Sub

        ''' <summary>
        ''' Comunicación con el servicio de Seguridad
        ''' </summary>
        ''' <param name="peticion"></param>
        Private Shared Function llamarSeguridad(peticion As ContractoLogin.ObtenerDelegaciones.Peticion) As ContractosSeguridad.Genesis.ObtenerDelegaciones.Respuesta

            Dim proxySeguridad As New Proxy.Seguridad()
            Dim peticionSeguridad As New ContractosSeguridad.Genesis.ObtenerDelegaciones.Peticion() With {
                .codigoPais = peticion.codigoPais,
                .identificadorUsuario = peticion.identificadorUsuario
            }

            Return proxySeguridad.GenesisObtenerDelegaciones(peticionSeguridad)

        End Function

        Private Shared Sub TransformaObjectoRespuesta(ByRef respuesta As ContractoServicio.GenesisLogin.ObtenerDelegaciones.Respuesta,
                                                      respuestaSeguridad As ContractosSeguridad.Genesis.ObtenerDelegaciones.Respuesta)

            If Not IsNothing(respuestaSeguridad.Pais) Then
                Dim objContinente As New Continente() With {.Nombre = respuestaSeguridad.Pais.DescripcionContinente}

                Dim objPais As New Pais() With {
                    .Codigo = respuestaSeguridad.Pais.Codigo,
                    .CodigoISODivisa = respuestaSeguridad.Pais.CodigoISODivisa,
                    .Nombre = respuestaSeguridad.Pais.Descripcion
                }
                objContinente.Paises = New List(Of Pais)()
                objContinente.Paises.Add(objPais)

                If Not IsNothing(respuestaSeguridad.Pais.Delegaciones) Then
                    objPais.Delegaciones = TransformaDelegaciones(respuestaSeguridad.Pais.Delegaciones)
                End If

                respuesta.Continentes.Add(objContinente)
            Else
                respuesta.CodigoError = respuestaSeguridad.Codigo
                respuesta.MensajeError = respuestaSeguridad.Descripcion
            End If

        End Sub

        Private Shared Function TransformaDelegaciones(delegacionesSeguridad As List(Of ContractosSeguridad.Genesis.Comun.Delegacion)) As List(Of Delegacion)
            Dim listaDelegaciones As New List(Of Delegacion)()

            For Each objDelegacionSeguridad In delegacionesSeguridad
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

                listaDelegaciones.Add(objDelegacion)

            Next

            Return listaDelegaciones
        End Function

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

                listaTiposSectores.Add(objTipoSector)
            Next

            Return listaTiposSectores
        End Function

    End Class

End Namespace

