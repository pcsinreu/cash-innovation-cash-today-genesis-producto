Imports Prosegur.Global.GesEfectivo.IAC.AccesoDatos
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.Global.GesEfectivo
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comunicacion.ProxyWS
Imports System.Configuration
Imports Prosegur.Genesis

Public Class AccionPuesto
    Implements ContractoServicio.IPuesto

#Region "[MÉTODOS WS]"

    ''' <summary>
    ''' responsable por obtener los datos de todos los ATMs pertenecientes al grupo de la petición.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 19/08/2011 Criado
    ''' </history>
    Public Function GetPuestos(Peticion As ContractoServicio.Puesto.GetPuestos.Peticion) As ContractoServicio.Puesto.GetPuestos.Respuesta Implements IPuesto.GetPuestos
        Dim objRespuesta As New ContractoServicio.Puesto.GetPuestos.Respuesta
        Try

            ValidarPeticion(Peticion)
            objRespuesta.Puestos = IAC.AccesoDatos.Puesto.GetPuestos(Peticion.CodigoDelegacion, Peticion.CodigoAplicacion, Peticion.CodigoPuesto, Peticion.HostPuesto, Peticion.BolVigente, Peticion.Permisos, Peticion.BolSoloMecanizado, Peticion.Aplicaciones)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' responsable por obtener los datos de todos los ATMs pertenecientes al grupo de la petición.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 19/08/2011 Criado
    ''' </history>
    Function GetPuestoDetail(Peticion As ContractoServicio.Puesto.GetPuestoDetail.Peticion) As ContractoServicio.Puesto.GetPuestoDetail.Respuesta Implements ContractoServicio.IPuesto.GetPuestoDetail
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Puesto.GetPuestoDetail.Respuesta
        Try

            ValidarPeticion(Peticion)
            objRespuesta.Puesto = IAC.AccesoDatos.Puesto.GetPuestoDetail(Peticion.CodigoAplicacion, Peticion.HostPuesto, Peticion.CodigoPuesto)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' responsable por obtener los datos de todos los ATMs pertenecientes al grupo de la petición.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 19/08/2011 Criado
    ''' </history>
    Function SetPuesto(Peticion As ContractoServicio.Puesto.SetPuesto.Peticion) As ContractoServicio.Puesto.SetPuesto.Respuesta Implements ContractoServicio.IPuesto.SetPuesto
        'TODO:Refazer a decrição do método
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Puesto.SetPuesto.Respuesta
        Try

            ' Valida os campos obrigatórios
            ValidarCamposObrigatorios(Peticion)

            'Define o caixa alta para o codigo do posto
            Peticion.CodigoPuesto = Peticion.CodigoPuesto.ToUpper
            Peticion.CodigoUsuario = Peticion.CodigoUsuario.ToUpper

            ' Identificador do Posto
            Dim OIDPuesto As String = Puesto.ObterOIDPuesto(Peticion.CodigoDelegacion, Peticion.CodigoAplicacion, Peticion.CodigoPuesto)

            ' Código do Host do Posto
            Dim existeHostPuesto As Boolean = Puesto.ValidarExistePuesto(Peticion.CodigoPuesto, Peticion.CodigoAplicacion, Peticion.CodigoHostPuesto)

            If ((Peticion.CodigoAplicacion.ToUpper = Comon.Constantes.CODIGO_APLICACION_GENESIS_SALIDAS.ToUpper OrElse Peticion.CodigoAplicacion.ToUpper = Comon.Constantes.CODIGO_APLICACION_GENESIS_SALIDAS_ANTIGUO.ToUpper) AndAlso
                (String.IsNullOrEmpty(ConfigurationManager.AppSettings("UrlServicio")) OrElse
                    Not Util.URLValida(ConfigurationManager.AppSettings("UrlServicio") & "Salidas/Integracion.asmx"))) Then

                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, Traduzir("030_msg_parametro_url_invalida"))

            End If

            ' Caso seja uma inserção
            If Peticion.Accion = Enumeradores.Accion.Alta Then

                If Not String.IsNullOrEmpty(OIDPuesto) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("029_msg_puesto_registrado"))
                End If

                If existeHostPuesto Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("029_msg_host_puesto_registrado"))
                End If

                Dim OIDAplicacion As String = AccesoDatos.Aplicacion.ObterOIDAplicacion(Peticion.CodigoAplicacion)
                If String.IsNullOrEmpty(OIDAplicacion) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("029_msg_aplicacion_no_encontrada"))
                End If

                Dim OIDelegacion As String = Delegacion.ObterOIDDelegacion(Peticion.CodigoDelegacion)
                If String.IsNullOrEmpty(OIDelegacion) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("029_msg_delegacion_no_encontrada"))
                End If

                IAC.AccesoDatos.Puesto.AltaPuesto(Peticion.CodigoPuesto, OIDAplicacion, Peticion.CodigoHostPuesto, Peticion.PuestoVigente, Peticion.CodigoUsuario, OIDelegacion)

            Else

                If String.IsNullOrEmpty(OIDPuesto) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("029_msg_puesto_no_encontrada"))
                End If

                IAC.AccesoDatos.Puesto.ActualizarPuesto(OIDPuesto, Peticion.CodigoHostPuesto, Peticion.PuestoVigente, Peticion.CodigoUsuario)

            End If


            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
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
    ''' [danielnunes] 18/06/2013 - Criado
    ''' </history>
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IPuesto.Test
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

#End Region

#Region "[VALIDAR]"
    Private Sub ValidarCamposObrigatorios(Peticion As ContractoServicio.Puesto.SetPuesto.Peticion)
        If String.IsNullOrEmpty(Peticion.CodigoDelegacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("029_msg_delegacion"))
        End If
        If String.IsNullOrEmpty(Peticion.CodigoAplicacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("029_msg_Aplicacion"))
        End If
        If String.IsNullOrEmpty(Peticion.CodigoPuesto) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("029_msg_puesto"))
        End If
        If String.IsNullOrEmpty(Peticion.CodigoHostPuesto) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("029_msg_hostPuesto"))
        End If
        If String.IsNullOrEmpty(Peticion.CodigoUsuario) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("029_msg_usuario"))
        End If
    End Sub

    Private Sub ValidarPeticion(Peticion As ContractoServicio.Puesto.GetPuestos.Peticion)
        If String.IsNullOrEmpty(Peticion.CodigoDelegacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("029_msg_delegacion"))
        End If
    End Sub

    Private Sub ValidarPeticion(Peticion As ContractoServicio.Puesto.GetPuestoDetail.Peticion)
        If String.IsNullOrEmpty(Peticion.HostPuesto) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("029_msg_hostPuesto"))
        End If
        If String.IsNullOrEmpty(Peticion.CodigoAplicacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("029_msg_Aplicacion"))
        End If
        If String.IsNullOrEmpty(Peticion.CodigoPuesto) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("029_msg_puesto"))
        End If
    End Sub
#End Region


End Class
