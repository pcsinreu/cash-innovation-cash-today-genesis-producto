Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Sector
Imports System.Collections.ObjectModel
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Data

''' <summary>
''' Clase Sector
''' </summary>
''' <remarks></remarks>
''' <history>
''' [maoliveira] 01/04/2014 - Criado
''' </history>
Public Class Sector

    Public Shared Function ObtenerSectoresPorCaracteristicas(Peticion As ObtenerSectoresPorCaracteristicas.ObtenerSectoresPorCaracteristicasPeticion) As ObtenerSectoresPorCaracteristicas.ObtenerSectoresPorCaracteristicasRespuesta

        'Cria a resposta do serviço
        Dim objRespuesta As New ObtenerSectoresPorCaracteristicas.ObtenerSectoresPorCaracteristicasRespuesta

        Try

            ' Se a petição foi informada
            If Peticion IsNot Nothing Then

                ' Recupera os setores
                objRespuesta.ListaSectores = LogicaNegocio.Genesis.Sector.ObtenerSectoresPorCaracteristicas(Peticion.CodigoDelegacion, Peticion.CodigoPlanta, Peticion.Caracteristicas, Peticion.CargarCodigosAjenos)

            End If

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.Excepciones.Add(ex.Message)

        End Try

        ' Retorna uma lista de denominações
        Return objRespuesta

    End Function

    Public Shared Function ObtenerSectoresPorCaracteristicasSimultaneas(Peticion As ObtenerSectoresPorCaracteristicasSimultaneasPeticion) As ObtenerSectoresPorCaracteristicasSimultaneasRespuesta

        'Cria a resposta do serviço
        Dim objRespuesta As New ObtenerSectoresPorCaracteristicasSimultaneasRespuesta

        Try

            ' Se a petição foi informada
            If Peticion IsNot Nothing Then

                ' Recupera os setores
                objRespuesta.ListaSectores = LogicaNegocio.Genesis.Sector.ObtenerSectoresPorCaracteristicasSimultaneas(Peticion.CodigoDelegacion, Peticion.CodigoPlanta, Peticion.Caracteristicas)

            End If

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.Excepciones.Add(ex.Message)

        End Try

        ' Retorna uma lista de denominações
        Return objRespuesta

    End Function

    Public Shared Function ObtenerSectoresTesoro(Peticion As ObtenerSectoresTesoro.Peticion) As ContractoServicio.Contractos.GenesisMovil.ObtenerSectoresTesoro.Respuesta

        'Cria a resposta do serviço
        Dim objRespuesta As New ContractoServicio.Contractos.GenesisMovil.ObtenerSectoresTesoro.Respuesta

        Try

            If String.IsNullOrEmpty(Peticion.CodigoDelegacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodigoDelegacion"))
            End If

            If String.IsNullOrEmpty(Peticion.CodigoPlanta) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodigoPlanta"))
            End If

            If String.IsNullOrEmpty(Peticion.DesLogin) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "DesLogin"))
            End If

            ' Recupera os setores
            objRespuesta.Sectores = LogicaNegocio.Genesis.Sector.ObtenerSectoresTesoro(Peticion.CodigoDelegacion, Peticion.CodigoPlanta, Peticion.DesLogin)

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.Excepciones.Add(ex.Message)

        End Try

        ' Retorna uma lista de denominações
        Return objRespuesta

    End Function

    Public Shared Function ObtenerSectoresPorSectorPadre(Peticion As ObtenerSectoresPorSectorPadre.ObtenerSectoresPorSectorPadrePeticion) As ObtenerSectoresPorSectorPadre.ObtenerSectoresPorSectorPadreRespuesta

        'Cria a resposta do serviço
        Dim objRespuesta As New ObtenerSectoresPorSectorPadre.ObtenerSectoresPorSectorPadreRespuesta

        Try

            ' Se a petição foi informada
            If Peticion IsNot Nothing Then

                ' Recupera os setores
                objRespuesta.ListaSectores = LogicaNegocio.Genesis.Sector.ObtenerSectoresPorSectorPadre(Peticion.CodigoDelegacion, Peticion.CodigoPlanta, Peticion.CodigoSectorPadre)

            End If

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.Excepciones.Add(ex.Message)

        End Try

        ' Retorna uma lista de denominações
        Return objRespuesta

    End Function

    Public Shared Function ObtenerSectores(Peticion As ContractoServicio.Contractos.Comon.Sector.ObtenerSectoresPeticion) As ContractoServicio.Contractos.Comon.Sector.ObtenerSectoresRespuesta

        'Cria a resposta do serviço
        Dim Respuesta As New ContractoServicio.Contractos.Comon.Sector.ObtenerSectoresRespuesta

        Try

            ' Se a petição foi informada
            If Peticion IsNot Nothing Then

                ' Recupera os setores
                Respuesta.Sectores = LogicaNegocio.Genesis.Sector.ObtenerSectores(Peticion.CodigoDelegacion, Peticion.CodigoPlanta, Peticion.TiposSectores, Peticion.CargarCodigosAjenos)

            End If

        Catch ex As Excepcion.NegocioExcepcion
            Respuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            Respuesta.Excepciones.Add(ex.Message)

        End Try

        ' Retorna uma lista de denominações
        Return Respuesta

    End Function

    Friend Shared Function ObtenerPlantasDelegacionesPorSector(pSectores As List(Of String), pIdentificadorAjeno As String) As DataTable
        Return Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerPlantasDelegacionesPorSector(pSectores, pIdentificadorAjeno)
    End Function

    Public Shared Function RecuperarSectores(Peticion As ContractoServicio.Contractos.Comon.Sector.ObtenerSectoresPeticion) As ContractoServicio.Contractos.Comon.Sector.ObtenerSectoresRespuesta

        'Cria a resposta do serviço
        Dim Respuesta As New ContractoServicio.Contractos.Comon.Sector.ObtenerSectoresRespuesta

        Try

            ' Se a petição foi informada
            If Peticion IsNot Nothing Then

                ' Recupera os setores
                Respuesta.Sectores = LogicaNegocio.Genesis.Sector.ObtenerSectores(Peticion.CodigoDelegacion, Peticion.CodigoPlanta, Peticion.TiposSectores, Peticion.CargarCodigosAjenos)

            End If

        Catch ex As Excepcion.NegocioExcepcion
            Respuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            Respuesta.Excepciones.Add(ex.Message)

        End Try

        ' Retorna uma lista de denominações
        Return Respuesta


    End Function

    Public Shared Function ObtenerCodigosSectoresPorCodigoTiposSectores(listaCodigoTiposSectores As List(Of String)) As List(Of String)
        Return Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerCodigosSectoresPorCodigoTiposSectores(listaCodigoTiposSectores)
    End Function
    Public Shared Function ObtenerDatosResumidos(Peticion As ContractoServicio.Contractos.Comon.Sector.ObtenerSectoresPeticion) As ContractoServicio.Contractos.Comon.Sector.ObtenerSectoresRespuesta

        'Cria a resposta do serviço
        Dim Respuesta As New ContractoServicio.Contractos.Comon.Sector.ObtenerSectoresRespuesta

        Try

            ' Se a petição foi informada
            If Peticion IsNot Nothing Then

                ' Recupera os setores
                Respuesta.Sectores = LogicaNegocio.Genesis.Sector.ObtenerDatosResumidos(Peticion.CodigoDelegacion, Peticion.CodigoPlanta)

            End If

        Catch ex As Excepcion.NegocioExcepcion
            Respuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            Respuesta.Excepciones.Add(ex.Message)

        End Try

        ' Retorna uma lista de denominações
        Return Respuesta


    End Function

    Public Shared Function ObtenerSectoresPorDelegacion(Peticion As ContractoServicio.Contractos.Comon.Sector.ObtenerSectoresPorDelegacion.Peticion) As ContractoServicio.Contractos.Comon.Sector.ObtenerSectoresPorDelegacion.Respuesta

        'Cria a resposta do serviço
        Dim Respuesta As New ContractoServicio.Contractos.Comon.Sector.ObtenerSectoresPorDelegacion.Respuesta

        Try

            ' Se a petição foi informada
            If Peticion IsNot Nothing AndAlso Peticion.codigoDelegacion IsNot Nothing AndAlso Peticion.codigoDelegacion.Count > 0 Then

                'Verifica obtenção somente de setores MAE
                Dim tiposSectoresMAE As List(Of String) = Nothing
                If Peticion.SolamenteTiposSectoresMAE Then
                    tiposSectoresMAE = RecuperarParametroTiposSectoresMAE(Peticion.codigoDelegacion(0))
                End If

                ' Recupera os setores
                Respuesta.Sectores = LogicaNegocio.Genesis.Sector.ObtenerSectoresPorCodigoDelegacion(Peticion.codigoDelegacion, tiposSectoresMAE, Peticion.SolamentePadres)

            End If

        Catch ex As Excepcion.NegocioExcepcion
            Respuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            Respuesta.MensajeError = ex.Message

        End Try

        ' Retorna uma lista de denominações
        Return Respuesta


    End Function

    Public Shared Function RecuperarSectoresSalidas(Peticion As ContractoServicio.Contractos.Comon.Sector.RecuperarSectoresSalidasPeticion) As ContractoServicio.Contractos.Comon.Sector.RecuperarSectoresSalidasRespuesta
        'Cria a resposta do serviço
        Dim Respuesta As New ContractoServicio.Contractos.Comon.Sector.RecuperarSectoresSalidasRespuesta

        Try

            ' Se a petição foi informada
            If Peticion IsNot Nothing Then

                ' Recupera os setores
                Respuesta.Sectores = Prosegur.Genesis.AccesoDatos.Genesis.Sector.RecuperarSectoresSalidas(Peticion.CodigoDelegacion, Peticion.CodigoPlanta)

            End If

        Catch ex As Excepcion.NegocioExcepcion
            Respuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            Respuesta.Excepciones.Add(ex.Message)

        End Try

        ' Retorna uma lista de denominações
        Return Respuesta

    End Function

    Public Shared Function RecuperarParametroTiposSectoresMAE(codDelegacion As String) As List(Of String)
        Dim Peticion As New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Peticion
        Peticion.CodigoAplicacion = "Genesis"
        Peticion.ValidarParametros = False
        Peticion.CodigoDelegacion = codDelegacion
        Peticion.Parametros = New [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.ParametroColeccion
        Peticion.Parametros.Add(New [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Parametro())
        Peticion.Parametros(0).CodigoParametro = "TiposSectoresMAE"

        Dim objNegocio As New [Global].GesEfectivo.IAC.LogicaNegocio.AccionIntegracion
        Dim respuesta = objNegocio.GetParametrosDelegacionPais(Peticion)
        If respuesta IsNot Nothing AndAlso respuesta.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
            Throw New Excepcion.NegocioExcepcion(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_ATENCION, respuesta.MensajeError)
        End If

        Dim valorParametro = respuesta.Parametros _
            .Where(Function(p) Not String.IsNullOrEmpty(p.ValorParametro)) _
            .Select(Function(p) p.ValorParametro) _
            .FirstOrDefault()

        If (valorParametro Is Nothing) Then Return Nothing

        Dim canales = valorParametro.Split(",")
        Return canales.ToList()

    End Function

    Shared Function VerificarPuestoPorSectorPadre(peticion As VerificarPuestoPorSectorPadre.Peticion) As VerificarPuestoPorSectorPadre.Respuesta
        'Cria a resposta do serviço
        Dim respuesta As New VerificarPuestoPorSectorPadre.Respuesta

        Try

            ' Se a petição foi informada
            If peticion IsNot Nothing Then

                ' Recupera os setores
                respuesta.esPuestoValido = AccesoDatos.Genesis.Sector.VerificarPuestoPorSectorPadre(peticion.CodigoDelegacion, peticion.CodigoPlanta, peticion.CodigoSectorPadre, peticion.CodigoPuesto)

            End If

        Catch ex As Excepcion.NegocioExcepcion
            respuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            respuesta.Excepciones.Add(ex.Message)

        End Try

        ' Retorna uma lista de denominações
        Return respuesta
    End Function

    Public Shared Function ObtenerCodigosSectoresPorSectorPadre(Peticion As ObtenerCodigosSectoresPorSectorPadre.Peticion) As ObtenerCodigosSectoresPorSectorPadre.Respuesta
        'Cria a resposta do serviço
        Dim respuesta As New ObtenerCodigosSectoresPorSectorPadre.Respuesta

        Try

            ' Se a petição foi informada
            If Peticion IsNot Nothing Then

                respuesta.codigosSectores = Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerCodigosSectoresPorSectorPadre(Peticion.CodigoDelegacion, Peticion.CodigoPlanta, Peticion.CodigoSectorPadre)

            End If

        Catch ex As Excepcion.NegocioExcepcion
            respuesta.Excepciones.Add(ex.Descricao)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            respuesta.Excepciones.Add(ex.Message)

        End Try

        ' Retorna uma lista de denominações
        Return respuesta
    End Function


End Class

