Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionConfiguracionGeneral
    Implements ContractoServicio.IConfiguracionGeneral

    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IConfiguracionGeneral.Test
        Dim objRespuesta As New ContractoServicio.Test.Respuesta

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

        End Try

        Return objRespuesta
    End Function

    Public Function GetConfiguracionGeneralReportes() As ContractoServicio.Configuracion.General.Respuesta Implements ContractoServicio.IConfiguracionGeneral.GetConfiguracionGeneralReportes
        Dim objRespuesta As New ContractoServicio.Configuracion.General.Respuesta

        Try
            objRespuesta = AccesoDatos.ConfiguracionGeneral.GetConfiguracionGeneralReportes()

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = Traduzir("001_SemErro")

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString
        End Try

        Return objRespuesta
    End Function

    Public Function InserirConfiguracionGeneralReporte(peticion As ContractoServicio.Configuracion.General.Peticion) As ContractoServicio.Configuracion.General.Respuesta Implements ContractoServicio.IConfiguracionGeneral.InserirConfiguracionGeneralReporte
        Dim objRespuesta As New ContractoServicio.Configuracion.General.Respuesta

        Try
            objRespuesta = AccesoDatos.ConfiguracionGeneral.InserirConfiguracionGeneralReporte(peticion)

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = Traduzir("001_SemErro")
        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            If ex.Message.Contains("AK_IAPR_TCONFIG_GENERAL_REP_1") Then
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = Traduzir("062_msg_configuracionGeneralDuplicado")
            Else
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString
            End If

        End Try

        Return objRespuesta
    End Function

    Public Function ExcluirConfiguracionGeneralReporte(peticion As ContractoServicio.Configuracion.General.Peticion) As ContractoServicio.Configuracion.General.Respuesta Implements ContractoServicio.IConfiguracionGeneral.ExcluirConfiguracionGeneralReporte
        Dim objRespuesta As New ContractoServicio.Configuracion.General.Respuesta

        Try
            objRespuesta = AccesoDatos.ConfiguracionGeneral.ExcluirConfiguracionGeneralReporte(peticion)

            If objRespuesta IsNot Nothing AndAlso String.IsNullOrEmpty(objRespuesta.MensajeError) Then
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = Traduzir("001_SemErro")
            End If

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            If ex.Message.Contains("AK_IAPR_TCONFIG_GENERAL_REP_1") Then
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = Traduzir("062_msg_configuracionGeneralDuplicado")
            Else
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString
            End If

        End Try

        Return objRespuesta
    End Function

    Public Function AtualizarConfiguracionGeneralReporte(peticion As ContractoServicio.Configuracion.General.Peticion) As ContractoServicio.Configuracion.General.Respuesta Implements ContractoServicio.IConfiguracionGeneral.AtualizarConfiguracionGeneralReporte
        Dim objRespuesta As New ContractoServicio.Configuracion.General.Respuesta

        Try
            objRespuesta = AccesoDatos.ConfiguracionGeneral.AtualizarConfiguracionGeneralReporte(peticion)

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = Traduzir("001_SemErro")
        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            If ex.Message.Contains("AK_IAPR_TCONFIG_GENERAL_REP_1") Then
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = Traduzir("062_msg_configuracionGeneralDuplicado")
            Else
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString
            End If

        End Try

        Return objRespuesta
    End Function

    Public Function GetConfiguracionGeneralReporte(peticion As ContractoServicio.Configuracion.General.Peticion) As ContractoServicio.Configuracion.General.Respuesta Implements ContractoServicio.IConfiguracionGeneral.GetConfiguracionGeneralReporte
        Dim objRespuesta As New ContractoServicio.Configuracion.General.Respuesta

        Try
            objRespuesta = AccesoDatos.ConfiguracionGeneral.GetConfiguracionGeneralReporte(peticion)

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = Traduzir("001_SemErro")

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString
        End Try

        Return objRespuesta
    End Function
End Class
