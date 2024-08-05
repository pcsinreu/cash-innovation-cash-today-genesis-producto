Imports Prosegur.Global.GesEfectivo.IAC.AccesoDatos
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionParametro
    Implements ContractoServicio.IParametro

#Region "[MÉTODOS WS]"

    ''' <summary>
    ''' responsable por obtener los datos de todos los Parametros.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 19/08/2011 Criado
    ''' </history>
    Function GetParametros(Peticion As ContractoServicio.Parametro.GetParametros.Peticion) As ContractoServicio.Parametro.GetParametros.Respuesta Implements ContractoServicio.IParametro.GetParametros
        Dim objRespuesta As New ContractoServicio.Parametro.GetParametros.Respuesta
        Try

            objRespuesta.Parametros = IAC.AccesoDatos.Parametro.GetParametros(Peticion.CodigoAplicacion, Peticion.CodigoNivel, Peticion.DesCortaAgrupacion, Peticion.CodParametro, Peticion.Permisos, Peticion.Aplicaciones)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

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
    ''' responsable por obtener los datos de todos los detailes del parametro.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 23/08/2011 Criado
    ''' </history>
    Function GetParametroDetail(peticion As ContractoServicio.Parametro.GetParametroDetail.Peticion) As ContractoServicio.Parametro.GetParametroDetail.Respuesta Implements ContractoServicio.IParametro.GetParametroDetail
        Dim objRespuesta As New ContractoServicio.Parametro.GetParametroDetail.Respuesta
        Try
            ValidarPeticion(peticion)
            objRespuesta.Parametro = IAC.AccesoDatos.Parametro.GetParametroDetail(peticion.CodigoAplicacion, peticion.CodigoParametro)
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
    ''' responsable por obtener los datos de todos las opciones de lo parametro.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 23/08/2011 Criado
    ''' </history>
    Function GetParametroOpciones(peticion As ContractoServicio.Parametro.GetParametroOpciones.Peticion) As ContractoServicio.Parametro.GetParametroOpciones.Respuesta Implements ContractoServicio.IParametro.GetParametroOpciones
        Dim objRespuesta As New ContractoServicio.Parametro.GetParametroOpciones.Respuesta
        Try
            ValidarPeticion(peticion)
            objRespuesta.Opciones = IAC.AccesoDatos.Parametro.GetParametroOpciones(peticion.CodigoAplicacion, peticion.CodigoParametro)
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
    ''' responsable por obtener los datos de todos los valore de los parametros.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 01/09/2011 Criado
    ''' </history>
    Function GetParametrosValues(peticion As ContractoServicio.Parametro.GetParametrosValues.Peticion) As ContractoServicio.Parametro.GetParametrosValues.Respuesta Implements ContractoServicio.IParametro.GetParametrosValues
        Dim objRespuesta As New ContractoServicio.Parametro.GetParametrosValues.Respuesta
        Try
            ValidarPeticion(peticion)

            Dim OIDPuesto As String = Nothing
            Dim DelegacionVO As DelegacionVO = Nothing

            DelegacionVO = Delegacion.ObterOIDDelegacionyCodigoPais(peticion.CodigoDelegacion)
            If DelegacionVO IsNot Nothing AndAlso String.IsNullOrEmpty(DelegacionVO.OIDDelegacion) AndAlso String.IsNullOrEmpty(DelegacionVO.CodigoPais) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_agrupacion"))
            End If

            If Not String.IsNullOrEmpty(peticion.CodigoPuesto) Then
                OIDPuesto = Puesto.ObterOIDPuesto(peticion.CodigoDelegacion, peticion.CodigoAplicacion, peticion.CodigoPuesto)
                If String.IsNullOrEmpty(OIDPuesto) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("029_msg_puesto_no_encontrada"))
                End If
            End If

            objRespuesta.Niveles = IAC.AccesoDatos.ParametroValue.GetParametrosValues(DelegacionVO.CodigoPais, DelegacionVO.OIDDelegacion, OIDPuesto, peticion.CodigoAplicacion)
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
    ''' 
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 23/08/2011 Criado
    ''' </history>
    Function SetParametro(peticion As ContractoServicio.Parametro.SetParametro.Peticion) As ContractoServicio.Parametro.SetParametro.Respuesta Implements ContractoServicio.IParametro.SetParametro
        Dim objRespuesta As New ContractoServicio.Parametro.SetParametro.Respuesta
        Try

            ValidarPeticion(peticion)

            Dim OIDAgrupacion As String = Nothing
            If Not String.IsNullOrEmpty(peticion.DescripcionAgrupacion) Then
                OIDAgrupacion = AgrupacionParametro.ObterOIDAgrupacionParametro(peticion.CodigoAplicacion, peticion.CodigoNivel, peticion.DescripcionAgrupacion)
                If String.IsNullOrEmpty(OIDAgrupacion) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_agrupacion"))
                End If
            End If

            Dim OIDAplicacion As String = Nothing
            OIDAplicacion = AccesoDatos.Aplicacion.ObterOIDAplicacion(peticion.CodigoAplicacion)
            If String.IsNullOrEmpty(OIDAplicacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_Aplicacion"))
            End If

            If Not Parametro.ValidarParametroExiste(peticion.CodigoAplicacion, peticion.CodigoParametro) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_parametro_no_encontrado"))
            End If

            Dim oidParametroOpcion As String = String.Empty
            For index = 0 To peticion.ParametroOpciones.Count - 1
                oidParametroOpcion = IAC.AccesoDatos.Parametro.ObterOidParametroOpcion(peticion.ParametroOpciones(index).CodigoOpcion, peticion.ParametroOpciones(index).Parametro.CodParametro, OIDAplicacion)

                If (String.IsNullOrEmpty(oidParametroOpcion)) Then
                    IAC.AccesoDatos.Parametro.AltaParametroOpcion(peticion.ParametroOpciones(index), peticion.ParametroOpciones(index).Parametro.CodParametro, OIDAplicacion)
                Else
                    IAC.AccesoDatos.Parametro.ActualizarParametroOpcion(peticion.ParametroOpciones(index))
                End If
            Next

            IAC.AccesoDatos.Parametro.ActualizarParametro(OIDAplicacion, OIDAgrupacion, peticion.CodigoParametro, peticion.DescripcionCortaParametro, peticion.DescripcionLargaParametro, peticion.NecOrden, peticion.CodigoUsuario)
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
    ''' 
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 23/08/2011 Criado
    ''' </history>
    Function SetParametrosValues(peticion As ContractoServicio.Parametro.SetParametrosValues.Peticion) As ContractoServicio.Parametro.SetParametrosValues.Respuesta Implements ContractoServicio.IParametro.SetParametrosValues
        Dim objRespuesta As New ContractoServicio.Parametro.SetParametrosValues.Respuesta
        Try
            ValidarPeticion(peticion)

            Dim DelegacionVO As DelegacionVO = Nothing
            Dim OIDPuesto As String = Nothing
            Dim codigosParametros As List(Of String) = peticion.Parametros.Select(Function(i) i.CodigoParametro).ToList
            Dim ParametrosValues As List(Of ParametroValueVO) = Nothing

            ' Recupera o identificador da delegación
            DelegacionVO = Delegacion.ObterOIDDelegacionyCodigoPais(peticion.CodigoDelegacion)
            If DelegacionVO IsNot Nothing AndAlso String.IsNullOrEmpty(DelegacionVO.OIDDelegacion) AndAlso String.IsNullOrEmpty(DelegacionVO.CodigoPais) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_delegacion_no_encontrada"))
            End If

            'Verifica a existência da aplicação
            Dim OIDAplicacion As String = AccesoDatos.Aplicacion.ObterOIDAplicacion(peticion.CodigoAplicacion)
            If String.IsNullOrEmpty(OIDAplicacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_aplicacion_no_encontrada"))
            End If

            ' Verifica se o código do puesto foi preenchido
            If Not String.IsNullOrEmpty(peticion.CodigoPuesto) Then
                ' Recupera o identificador do posto
                OIDPuesto = Puesto.ObterOIDPuesto(peticion.CodigoDelegacion, peticion.CodigoAplicacion, peticion.CodigoPuesto)
                ' Se não existe, retorna uma mensagem de erro
                If String.IsNullOrEmpty(OIDPuesto) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_puesto_no_encontrado"))
                End If
            End If


            ParametrosValues = ParametroValue.ListarOIDParametrosValues(DelegacionVO.CodigoPais, DelegacionVO.OIDDelegacion, OIDPuesto, peticion.CodigoAplicacion, codigosParametros)

            ' Verifica se existe parâmetros para o posto
            If ParametrosValues IsNot Nothing AndAlso ParametrosValues.Count > 0 Then

                ' Para cada parâmetro recuperado
                For Each item As ParametroValueVO In ParametrosValues
                    Dim itemLocal = item
                    ' Define o seu valor, de acordo com o valor que foi informado na petição
                    Dim itemParametroValor As ContractoServicio.Parametro.SetParametrosValues.Parametro = peticion.Parametros.SingleOrDefault(Function(i) i.CodigoParametro = itemLocal.CodigoParametro)
                    If itemParametroValor IsNot Nothing Then
                        item.Valor = itemParametroValor.ValorParametro
                    End If
                Next

            End If

            ' Grava os valores dos parâmetros da delegação
            Parametro.SetParametroValue(ParametrosValues, DelegacionVO.OIDDelegacion, OIDPuesto, DelegacionVO.CodigoPais, peticion.CodigoUsuario)

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
    ''' 
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 23/08/2011 Criado
    ''' </history>
    Function GetAgrupaciones(peticion As ContractoServicio.Parametro.GetAgrupaciones.Peticion) As ContractoServicio.Parametro.GetAgrupaciones.Respuesta Implements ContractoServicio.IParametro.GetAgrupaciones
        Dim objRespuesta As New ContractoServicio.Parametro.GetAgrupaciones.Respuesta
        Try
            ValidarPeticion(peticion)

            objRespuesta.Agrupaciones = IAC.AccesoDatos.AgrupacionParametro.GetAgrupaciones(peticion.CodigoAplicacion, peticion.CodigoNivel, peticion.DesAgrupacion, peticion.Permisos, peticion.Aplicaciones)
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
    ''' 
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 23/08/2011 Criado
    ''' </history>
    Function GetAgrupacionDetail(peticion As ContractoServicio.Parametro.GetAgrupacionDetail.Peticion) As ContractoServicio.Parametro.GetAgrupacionDetail.Respuesta Implements ContractoServicio.IParametro.GetAgrupacionDetail
        Dim objRespuesta As New ContractoServicio.Parametro.GetAgrupacionDetail.Respuesta
        Try
            ValidarPeticion(peticion)

            objRespuesta.Agrupaciones = IAC.AccesoDatos.AgrupacionParametro.GetAgrupacionDetail(peticion.CodigoAplicacion, peticion.CodigoNivel, peticion.DesAgrupacion)
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
    ''' 
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 23/08/2011 Criado
    ''' </history>
    Function SetAgrupacion(peticion As ContractoServicio.Parametro.SetAgrupacion.Peticion) As ContractoServicio.Parametro.SetAgrupacion.Respuesta Implements ContractoServicio.IParametro.SetAgrupacion
        Dim objRespuesta As New ContractoServicio.Parametro.SetAgrupacion.Respuesta
        Try
            ValidarPeticion(peticion)

            Dim OIDAgrupacion As String = Nothing
            OIDAgrupacion = AgrupacionParametro.ObterOIDAgrupacionParametro(peticion.CodigoAplicacion, peticion.CodigoNivel, peticion.DescripcionCorta)

            If String.IsNullOrEmpty(OIDAgrupacion) Then

                Dim OIDNivel As String = Nothing
                OIDNivel = Nivel.ObterOIDNivel(peticion.CodigoNivel)
                If String.IsNullOrEmpty(OIDNivel) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_nivel_no_encontrada"))
                End If

                Dim OIDAplicacion As String = Nothing
                OIDAplicacion = AccesoDatos.Aplicacion.ObterOIDAplicacion(peticion.CodigoAplicacion)
                If String.IsNullOrEmpty(OIDAplicacion) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_aplicacion_no_encontrada"))
                End If

                AgrupacionParametro.AltaAgrupacionParametro(peticion.CodigoAplicacion, OIDAplicacion, peticion.DescripcionCorta, peticion.DescripcionLarga, peticion.CodigoUsuario, peticion.NecOrden, OIDNivel)
            Else
                AgrupacionParametro.ActualizarAgrupacionParametro(OIDAgrupacion, peticion.DescripcionLarga, peticion.CodigoUsuario, peticion.NecOrden)
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
    ''' 
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 23/08/2011 Criado
    ''' </history>
    Function BajarAgrupacion(peticion As ContractoServicio.Parametro.BajarAgrupacion.Peticion) As ContractoServicio.Parametro.BajarAgrupacion.Respuesta Implements ContractoServicio.IParametro.BajarAgrupacion
        Dim objRespuesta As New ContractoServicio.Parametro.BajarAgrupacion.Respuesta
        Try
            ValidarPeticion(peticion)


            Dim OIDAgrupacion As String = Nothing
            OIDAgrupacion = AgrupacionParametro.ObterOIDAgrupacionParametro(peticion.CodigoAplicacion, peticion.CodigoNivel, peticion.DesAgrupacion)
            If String.IsNullOrEmpty(OIDAgrupacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_agrupacion"))
            End If

            If AgrupacionParametro.ValidarAgrupacionConParametroAsociado(peticion.CodigoAplicacion, peticion.CodigoNivel, peticion.DesAgrupacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_agrupacion_bajar"))
            End If

            IAC.AccesoDatos.AgrupacionParametro.BajarAgrupacion(OIDAgrupacion)
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
    ''' 
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function VerificarCodigoParametroOpcion(peticion As ContractoServicio.Parametro.VerificarCodigoParametroOpcion.Peticion, codParametro As String, codAplicacion As String) As ContractoServicio.Parametro.VerificarCodigoParametroOpcion.Respuesta Implements ContractoServicio.IParametro.VerificarCodigoParametroOpcion

        Dim objRespuesta As New ContractoServicio.Parametro.VerificarCodigoParametroOpcion.Respuesta

        Try

            Dim OIDAplicacion As String = Nothing
            OIDAplicacion = AccesoDatos.Aplicacion.ObterOIDAplicacion(codAplicacion)
            If String.IsNullOrEmpty(OIDAplicacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_Aplicacion"))
            End If

            objRespuesta.Existe = AccesoDatos.Parametro.VerificaCodigoOpcaoMemoria(peticion.Codigo, codParametro, OIDAplicacion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try
        Return objRespuesta

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function VerificarDescricaoParametroOpcion(peticion As ContractoServicio.Parametro.VerificarDescricaoParametroOpcion.Peticion, codParametro As String, codAplicacion As String) As ContractoServicio.Parametro.VerificarDescricaoParametroOpcion.Respuesta Implements ContractoServicio.IParametro.VerificarDescricaoParametroOpcion

        Dim objRespuesta As New ContractoServicio.Parametro.VerificarDescricaoParametroOpcion.Respuesta

        Try
            Dim OIDAplicacion As String = Nothing
            OIDAplicacion = AccesoDatos.Aplicacion.ObterOIDAplicacion(codAplicacion)
            If String.IsNullOrEmpty(OIDAplicacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_Aplicacion"))
            End If

            objRespuesta.Existe = AccesoDatos.Parametro.VerificaDescricaoOpcaoMemoria(peticion.Descripcion, codParametro, OIDAplicacion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
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
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IParametro.Test
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

        If String.IsNullOrEmpty(Peticion.CodigoHostPuesto) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("029_msg_hostPuesto"))
        End If

        If String.IsNullOrEmpty(Peticion.CodigoUsuario) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("029_msg_usuario"))
        End If
    End Sub

    Private Sub ValidarPeticion(Peticion As ContractoServicio.Parametro.GetParametros.Peticion)


    End Sub

    Private Sub ValidarPeticion(Peticion As ContractoServicio.Parametro.GetParametrosValues.Peticion)
        If String.IsNullOrEmpty(Peticion.CodigoAplicacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_Aplicacion"))
        End If
        If String.IsNullOrEmpty(Peticion.CodigoDelegacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("029_msg_delegacion"))
        End If
    End Sub

    Private Sub ValidarPeticion(Peticion As ContractoServicio.Parametro.GetAgrupaciones.Peticion)


    End Sub

    Private Sub ValidarPeticion(Peticion As ContractoServicio.Parametro.GetParametroDetail.Peticion)
        If String.IsNullOrEmpty(Peticion.CodigoAplicacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_Aplicacion"))
        End If
        If String.IsNullOrEmpty(Peticion.CodigoParametro) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_parametro"))
        End If

    End Sub

    Private Sub ValidarPeticion(Peticion As ContractoServicio.Parametro.GetParametroOpciones.Peticion)
        If String.IsNullOrEmpty(Peticion.CodigoAplicacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_Aplicacion"))
        End If
        If String.IsNullOrEmpty(Peticion.CodigoParametro) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_parametro"))
        End If

    End Sub

    Private Sub ValidarPeticion(Peticion As ContractoServicio.Parametro.SetParametro.Peticion)
        If String.IsNullOrEmpty(Peticion.CodigoAplicacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_Aplicacion"))
        End If
        If String.IsNullOrEmpty(Peticion.CodigoParametro) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_parametro"))
        End If
        If String.IsNullOrEmpty(Peticion.CodigoUsuario) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_usuario"))
        End If
        If String.IsNullOrEmpty(Peticion.CodigoNivel) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_nivel"))
        End If

    End Sub

    Private Sub ValidarPeticion(peticion As ContractoServicio.Parametro.GetAgrupacionDetail.Peticion)
        If String.IsNullOrEmpty(peticion.CodigoAplicacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_Aplicacion"))
        End If
        If String.IsNullOrEmpty(peticion.CodigoNivel) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_nivel"))
        End If
        If String.IsNullOrEmpty(peticion.DesAgrupacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_agrupacion_descripcion"))
        End If
    End Sub

    Private Sub ValidarPeticion(peticion As ContractoServicio.Parametro.BajarAgrupacion.Peticion)
        If String.IsNullOrEmpty(peticion.CodigoAplicacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_Aplicacion"))
        End If
        If String.IsNullOrEmpty(peticion.CodigoNivel) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_nivel"))
        End If
        If String.IsNullOrEmpty(peticion.DesAgrupacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_agrupacion_descripcion"))
        End If
    End Sub

    Private Sub ValidarPeticion(peticion As ContractoServicio.Parametro.SetAgrupacion.Peticion)
        If String.IsNullOrEmpty(peticion.CodigoAplicacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_Aplicacion"))
        End If
        If String.IsNullOrEmpty(peticion.CodigoNivel) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_nivel"))
        End If
        If String.IsNullOrEmpty(peticion.CodigoUsuario) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_usuario"))
        End If
        If String.IsNullOrEmpty(peticion.DescripcionCorta) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_agrupacion_descripcion"))
        End If
        If Not peticion.NecOrden.HasValue Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_nec_orden"))
        End If
        If String.IsNullOrEmpty(peticion.DescripcionLarga) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_descripcion_larga"))
        End If
    End Sub

    Private Sub ValidarPeticion(Peticion As ContractoServicio.Parametro.SetParametrosValues.Peticion)
        If String.IsNullOrEmpty(Peticion.CodigoAplicacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_Aplicacion"))
        End If
        If String.IsNullOrEmpty(Peticion.CodigoDelegacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("029_msg_delegacion"))
        End If
        If String.IsNullOrEmpty(Peticion.CodigoUsuario) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_usuario"))
        End If
        If Peticion.Parametros Is Nothing OrElse Peticion.Parametros.Count = 0 Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_permisos_no_encontrados"))
        End If
        If Peticion.Parametros.Exists(Function(i) String.IsNullOrEmpty(i.CodigoParametro)) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("030_msg_parametro"))
        End If

    End Sub


#End Region

End Class