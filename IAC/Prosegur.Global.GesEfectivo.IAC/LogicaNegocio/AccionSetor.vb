Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Setor
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DbHelper
Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.AccesoDatos
Imports Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.TipoSector

Public Class AccionSetor
    Implements ContractoServicio.ISetor


    Public Function getSectores(objPeticion As ContractoServicio.Setor.GetSectores.Peticion) As ContractoServicio.Setor.GetSectores.Respuesta Implements ContractoServicio.ISetor.getSectores

        'Cria Objeto de Respuesta
        Dim objRespuesta As New ContractoServicio.Setor.GetSectores.Respuesta

        Try

            If String.IsNullOrEmpty(objPeticion.oidTipoSector) AndAlso String.IsNullOrEmpty(objPeticion.oidPlanta) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "oidPlanta"))
            End If

            If objPeticion.ParametrosPaginacion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))
            ElseIf (objPeticion.ParametrosPaginacion.RealizarPaginacion AndAlso objPeticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))
            End If

            objRespuesta.Setor = AccesoDatos.Sector.getSectores(objPeticion, objRespuesta.ParametrosPaginacion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty
            objRespuesta.Resultado = 0
        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
            objRespuesta.Resultado = 1
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
            objRespuesta.Resultado = 1
        End Try

        Return objRespuesta

    End Function

    Public Function setSectores(objPeticion As ContractoServicio.Setor.SetSectores.Peticion) As ContractoServicio.Setor.SetSectores.Respuesta Implements ContractoServicio.ISetor.setSectores

        ' Cria Objeto de Retorno
        Dim objRespuesta As New ContractoServicio.Setor.SetSectores.Respuesta
        Dim objResposta As New IAC.LogicaNegocio.AccionCodigoAjeno
        Dim oidSector As String = Nothing
        Dim oidDelegacion As String = Nothing
        Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)
        Dim objRespostaImporteMaximo As New IAC.LogicaNegocio.AccionImporteMaximo

        Try

            If String.IsNullOrEmpty(objPeticion.codSector) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "codSector"))
            End If

            If String.IsNullOrEmpty(objPeticion.desSector) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "desSector"))
            End If

            If String.IsNullOrEmpty(objPeticion.oidTipoSector) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "oidTipoSector"))
            End If

            If String.IsNullOrEmpty(objPeticion.oidPlanta) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "oidPlanta"))
            End If

            If VerificaTipoSector(objPeticion.oidTipoSector) = False Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("054_msg_erro_tipo_sector_inativo"))
            End If

            If objPeticion.bolCentroProceso = True _
           AndAlso Not String.IsNullOrEmpty(objPeticion.oidSectorPadre) Then
                If (VerificaCentroProceso(objPeticion) = False) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("054_msg_planta_CentroProceso"))
                End If
            End If

            If Not String.IsNullOrEmpty(objPeticion.oidPlanta) Then
                If Not AccesoDatos.Sector.VerificaPlantaAtiva(objPeticion.oidPlanta) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("054_msg_erro_SectorPlantaDesativada"))
                End If
            End If

            If Not String.IsNullOrEmpty(objPeticion.oidSectorPadre) Then
                If Not AccesoDatos.Sector.VerificaSectorAtivo(objPeticion.oidSectorPadre) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("054_msg_erro_SectorSectorPadreDesativado"))
                End If
            End If

            objPeticion.codSector = objPeticion.codSector.ToUpper()

            oidDelegacion = AccesoDatos.Planta.ObterOidDelegacionPorOidPlanta(objPeticion.oidPlanta)

            ' Gravando Relacionamento no IAC
            ' Verificando se já existe relacionamento para delegação
            If VerificaCodigoExistenteTipoSectorDelegacion(objPeticion.oidTipoSector, oidDelegacion) Then

                ' Relacionamento da Delegacion
                Dim objPeticionTipoSectorDelegacion As New ContractoServicio.Setor.GrabarTipoSectorDelegacion.Peticion
                objPeticionTipoSectorDelegacion.OidTipoSectorDelegacion = Guid.NewGuid.ToString()
                objPeticionTipoSectorDelegacion.OidDelegacion = oidDelegacion
                objPeticionTipoSectorDelegacion.OidTipoSector = objPeticion.oidTipoSector
                objPeticionTipoSectorDelegacion.GmtCreacion = Date.Now
                objPeticionTipoSectorDelegacion.GmtModificacion = Date.Now
                objPeticionTipoSectorDelegacion.DesUsuarioCreacion = objPeticion.desUsuarioCreacion
                objPeticionTipoSectorDelegacion.DesUsuarioModificacion = objPeticion.desUsuarioModificacion

                Dim objRespuetaTipoSectorDelegacion As New ContractoServicio.Setor.GrabarTipoSectorDelegacion.Respuesta
                objRespuetaTipoSectorDelegacion = GrabarTipoSectorDelegacion(objPeticionTipoSectorDelegacion)
                If Not objRespuetaTipoSectorDelegacion.CodigoError.Equals("0") AndAlso Not String.IsNullOrEmpty(objRespuetaTipoSectorDelegacion.MensajeError) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(objRespuetaTipoSectorDelegacion.MensajeError))
                End If

            End If

            ' Verificando se já existe relacionamento para planta
            If VerificaCodigoExistenteTipoSectorPlanta(objPeticion.oidTipoSector, objPeticion.oidPlanta) Then

                ' Relacionamento da Planta
                Dim objPeticionTipoSectorPlanta As New ContractoServicio.Setor.GrabarTipoSectorPlanta.Peticion
                objPeticionTipoSectorPlanta.OidTipoSectorPlanta = Guid.NewGuid.ToString()
                objPeticionTipoSectorPlanta.OidPlanta = objPeticion.oidPlanta
                objPeticionTipoSectorPlanta.OidTipoSector = objPeticion.oidTipoSector
                objPeticionTipoSectorPlanta.GmtCreacion = Date.Now
                objPeticionTipoSectorPlanta.GmtModificacion = Date.Now
                objPeticionTipoSectorPlanta.DesUsuarioCreacion = objPeticion.desUsuarioCreacion
                objPeticionTipoSectorPlanta.DesUsuarioModificacion = objPeticion.desUsuarioModificacion

                Dim objRespuetaTipoSectorPlanta As New ContractoServicio.Setor.GrabarTipoSectorPlanta.Respuesta
                objRespuetaTipoSectorPlanta = GrabarTipoSectorPlanta(objPeticionTipoSectorPlanta)
                If Not objRespuetaTipoSectorPlanta.CodigoError.Equals("0") AndAlso Not String.IsNullOrEmpty(objRespuetaTipoSectorPlanta.MensajeError) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(objRespuetaTipoSectorPlanta.MensajeError))
                End If

            End If

            If (Not String.IsNullOrEmpty(objPeticion.oidSector)) Then

                If objPeticion.bolActivo = False Then
                    If Not AccesoDatos.Sector.VerificaUtilizacaoSectorPai(objPeticion.oidSector) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("054_msg_erro_Sector_Pai_Utilizado"))
                    End If
                End If

                objRespuesta.codSector = AccesoDatos.Sector.AtualizarSector(objPeticion, objTransacao)

                If objPeticion.CodigosAjenos.Count > 0 Then

                    Dim objCodigoAjeno As ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion = PreencheCodigoAjeno(objPeticion)

                    objRespuesta.CodigosAjenos = objResposta.SetCodigosAjenos(objCodigoAjeno, objTransacao)

                End If


                If objPeticion.ImporteMaximo.Count > 0 Then

                    Dim objImporteMaximo As ContractoServicio.ImporteMaximo.SetImporteMaximo.Peticion = PreencheImporteMaximo(objPeticion)

                    objRespuesta.ImportesMaximos = objRespostaImporteMaximo.SetImporteMaximo(objImporteMaximo, objTransacao)

                End If
            Else

                ' Verificando se o codigo existe
                If VerificaCodigoExistente(objPeticion.oidPlanta, objPeticion.codSector, objPeticion.oidSectorPadre) = False Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("054_msg_erro_codigoExistente"))
                End If

                ' Gravando setor no IAC
                oidSector = AccesoDatos.Sector.setSectores(objPeticion, objTransacao)

                If objPeticion.CodigosAjenos.Count > 0 Then

                    Dim objCodigoAjeno As ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion = PreencheCodigoAjeno(objPeticion, oidSector)

                    objRespuesta.CodigosAjenos = objResposta.SetCodigosAjenos(objCodigoAjeno, objTransacao)

                End If

                ' VERIFICA SE CONTÉM IMPORTE MAXIMO
                If objPeticion.ImporteMaximo.Count > 0 Then

                    Dim objImporteMaximo As ContractoServicio.ImporteMaximo.SetImporteMaximo.Peticion = PreencheImporteMaximo(objPeticion, oidSector)

                    objRespuesta.ImportesMaximos = objRespostaImporteMaximo.SetImporteMaximo(objImporteMaximo, objTransacao)

                End If

            End If

            objTransacao.RealizarTransacao()

            If (objPeticion.BolEliminaCodigosAjenos) Then
                Dim objAccionAjeno As New IAC.LogicaNegocio.AccionCodigoAjeno
                objAccionAjeno.BorrarCodigosAjenos(objPeticion.oidSector, "GEPR_TSECTOR", Nothing)
            End If

            objRespuesta.codSector = objPeticion.codSector
            objRespuesta.Resultado = 0
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.codSector = objPeticion.codSector
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
            objRespuesta.Resultado = 1
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.codSector = objPeticion.codSector
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
            objRespuesta.Resultado = 1
        End Try

        Return objRespuesta

    End Function


    Public Function GetSectoresIAC(objPeticion As ContractoServicio.Sector.GetSectoresIAC.Peticion) As ContractoServicio.Sector.GetSectoresIAC.Respuesta Implements ContractoServicio.ISetor.GetSectoresIAC
        Dim objRespuesta As New ContractoServicio.Sector.GetSectoresIAC.Respuesta

        Try
            If String.IsNullOrEmpty(objPeticion.CodigoDelegacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodigoDelegacion"))
            End If

            If String.IsNullOrEmpty(objPeticion.CodigoPlanta) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodigoPlanta"))
            End If

            If String.IsNullOrEmpty(objPeticion.CodigoTipoSitio) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodigoTipoSitio"))
            End If

            objRespuesta.Sectores = AccesoDatos.Sector.GetSectoresIAC(objPeticion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try
        Return objRespuesta
    End Function

    Public Function GetSectoresTesoro(objPeticion As ContractoServicio.Sector.GetSectoresTesoro.Peticion) As ContractoServicio.Sector.GetSectoresTesoro.Respuesta Implements ContractoServicio.ISetor.GetSectoresTesoro

        Dim objRespuesta As New ContractoServicio.Sector.GetSectoresTesoro.Respuesta

        Try
            If String.IsNullOrEmpty(objPeticion.CodigoDelegacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodigoDelegacion"))
            End If

            If String.IsNullOrEmpty(objPeticion.CodigoPlanta) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodigoPlanta"))
            End If

            If String.IsNullOrEmpty(objPeticion.CodigoTipoSector) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodigoTipoSector"))
            End If

            objRespuesta.Sectores = AccesoDatos.Sector.GetSectoresTesoro(objPeticion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try
        Return objRespuesta
    End Function

    Public Function GrabarTipoSectorDelegacion(objPeticion As ContractoServicio.Setor.GrabarTipoSectorDelegacion.Peticion) As ContractoServicio.Setor.GrabarTipoSectorDelegacion.Respuesta

        Dim objRespuesta As New ContractoServicio.Setor.GrabarTipoSectorDelegacion.Respuesta

        Try
            AccesoDatos.Sector.GrabarTipoSectirDelegacion(objPeticion)
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.OidTipoSectorDelegacion = objPeticion.OidTipoSectorDelegacion
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        End Try

        Return objRespuesta

    End Function

    Public Function GrabarTipoSectorPlanta(objPeticion As ContractoServicio.Setor.GrabarTipoSectorPlanta.Peticion) As ContractoServicio.Setor.GrabarTipoSectorPlanta.Respuesta

        Dim objRespuesta As New ContractoServicio.Setor.GrabarTipoSectorPlanta.Respuesta

        Try
            AccesoDatos.Sector.GrabarTipoSectorPlanta(objPeticion)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.OidTipoSectorPlanta = objPeticion.OidTipoSectorPlanta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        End Try

        Return objRespuesta

    End Function

    Private Function PreencheImporteMaximo(ByRef objPeticion As ContractoServicio.Setor.SetSectores.Peticion,
                                      Optional OidSector As String = Nothing) As ContractoServicio.ImporteMaximo.SetImporteMaximo.Peticion

        Dim objImporteMaximo As New ContractoServicio.ImporteMaximo.SetImporteMaximo.Peticion

        objImporteMaximo.ImportesMaximo = New ContractoServicio.ImporteMaximo.SetImporteMaximo.ImporteMaximoColeccion

        For Each objItem In objPeticion.ImporteMaximo
            Dim objItemCast As New ContractoServicio.ImporteMaximo.SetImporteMaximo.ImporteMaximo
            Dim codigotablaGenesis As String = String.Empty

            objItemCast.OidImporteMaximo = objItem.OidImporteMaximo
            objItemCast.oidPlanta = Nothing
            objItemCast.Cliente = objItem.Cliente
            objItemCast.Divisa = objItem.Divisa
            objItemCast.Canal = objItem.Canal

            If String.IsNullOrEmpty(OidSector) AndAlso Not String.IsNullOrEmpty(objPeticion.oidSector) Then
                objItemCast.Sector = New ContractoServicio.Utilidad.GetComboSectores.Sector1 With {.OidSector = objPeticion.oidSector}
            ElseIf String.IsNullOrEmpty(objPeticion.oidSector) AndAlso Not String.IsNullOrEmpty(OidSector) Then
                objItemCast.Sector = New ContractoServicio.Utilidad.GetComboSectores.Sector1 With {.OidSector = OidSector}
            End If

            objItemCast.SubCanal = objItem.SubCanal
            objItemCast.ValorMaximo = objItem.ValorMaximo
            objItemCast.BolVigente = objItem.BolVigente
            objItemCast.DesUsuarioCreacion = objPeticion.desUsuarioCreacion
            objItemCast.DesUsuarioModificacion = objPeticion.desUsuarioModificacion
            objItemCast.GmtCreacion = objPeticion.gmtCreacion
            objItemCast.GmtModificacion = objPeticion.gmtModificacion
            objImporteMaximo.ImportesMaximo.Add(objItemCast)

        Next

        Return objImporteMaximo

    End Function
    Public Function PreencheCodigoAjeno(ByRef objPeticion As ContractoServicio.Setor.SetSectores.Peticion,
                                    Optional OidSector As String = Nothing) As ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion

        Dim objCodigoAjeno As New ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion

        objCodigoAjeno.CodigosAjenos = New ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjenoColeccion

        For Each objItem In objPeticion.CodigosAjenos
            Dim objItemCast As New ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjeno
            Dim codigotablaGenesis As String = String.Empty

            objItemCast.OidCodigoAjeno = objItem.OidCodigoAjeno
            objItemCast.BolActivo = objItem.BolActivo
            objItemCast.BolDefecto = objItem.BolDefecto
            objItemCast.CodAjeno = objItem.CodAjeno
            objItemCast.CodIdentificador = objItem.CodIdentificador
            objItemCast.DesAjeno = objItem.DesAjeno
            objItemCast.DesUsuarioCreacion = objPeticion.desUsuarioCreacion
            objItemCast.DesUsuarioModificacion = objPeticion.desUsuarioModificacion
            objItemCast.GmtCreacion = objPeticion.gmtCreacion
            objItemCast.GmtModificacion = objPeticion.gmtModificacion
            If String.IsNullOrEmpty(OidSector) AndAlso String.IsNullOrEmpty(objPeticion.oidSector) Then
                objItemCast.OidTablaGenesis = objPeticion.oidPlanta
            Else
                objItemCast.OidTablaGenesis = If(String.IsNullOrEmpty(OidSector), objPeticion.oidSector, OidSector)
            End If
            codigotablaGenesis = ContractoServicio.Constantes.COD_SECTOR
            objItemCast.CodTipoTablaGenesis = (From item In AccesoDatos.Constantes.MapeoEntidadesCodigoAjeno
                                               Where item.CodTipoTablaGenesis = codigotablaGenesis
                                               Select item.Entidade).FirstOrDefault()

            objCodigoAjeno.CodigosAjenos.Add(objItemCast)

        Next

        Return objCodigoAjeno

    End Function

    Private Function VerificaCentroProceso(ObjPeticion As ContractoServicio.Setor.SetSectores.Peticion) As Boolean

        Dim Retorno As Boolean = False

        Retorno = AccesoDatos.Sector.GetSectoresCentroProcesso(ObjPeticion)

        Return Retorno

    End Function

    Private Function VerificaCodigoExistente(oidPlanta As String, CodSector As String, oidSectorPadre As String) As Boolean

        Dim ObjPeticion As New ContractoServicio.Setor.GetSectores.Peticion
        Dim objRespuesta As New ContractoServicio.Setor.GetSectores.Respuesta

        ObjPeticion.oidPlanta = oidPlanta
        ObjPeticion.codSector = CodSector
        ObjPeticion.oidSectorPadre = oidSectorPadre

        objRespuesta.Setor = AccesoDatos.Sector.getSectores(ObjPeticion, objRespuesta.ParametrosPaginacion)

        If objRespuesta.Setor.Count > 0 Then
            Return False
        End If

        Return True
    End Function

    Private Function VerificaTipoSector(oidTipoSector As String) As Boolean

        Dim Retorno As Boolean = False

        Retorno = AccesoDatos.Sector.VerificaTipoSectores(oidTipoSector)

        Return Retorno

    End Function

    Private Function VerificaCodigoExistenteTipoSectorDelegacion(oidTipoSector As String, oidDelegacion As String) As Boolean

        Dim Retorno As Boolean = False

        Retorno = AccesoDatos.Sector.VerificaCodigoExistenteTipoSectorDelegacion(oidTipoSector, oidDelegacion)

        Return Retorno

    End Function

    Private Function VerificaCodigoExistenteTipoSectorPlanta(oidTipoSector As String, oidPlanta As String) As Boolean

        Dim Retorno As Boolean = False

        Retorno = AccesoDatos.Sector.VerificaCodigoExistenteTipoSectorPlanta(oidTipoSector, oidPlanta)

        Return Retorno

    End Function

    Public Function getSetorDetail(objPeticion As ContractoServicio.Setor.GetSectoresDetail.Peticion) As ContractoServicio.Setor.GetSectoresDetail.Respuesta Implements ContractoServicio.ISetor.getSetorDetail
        'Cria Objeto de Respuesta
        Dim objRespuesta As New ContractoServicio.Setor.GetSectoresDetail.Respuesta

        Try

            objRespuesta.Sector = AccesoDatos.Sector.getSectoresDetail(objPeticion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty
            objRespuesta.Resultado = 0
        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
            objRespuesta.Resultado = 1
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
            objRespuesta.Resultado = 1
        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 18/06/2013 - Criado
    ''' </history>
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ISetor.Test
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

