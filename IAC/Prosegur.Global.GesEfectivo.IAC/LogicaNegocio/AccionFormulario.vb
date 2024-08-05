
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio

Public Class AccionFormulario
    Implements ContractoServicio.IFormulario


    Public Function getFormularios(Peticion As Formulario.GetFormularios.Peticion) As Formulario.GetFormularios.Respuesta Implements IFormulario.getFormularios
        ' criar objeto respuesta
        Dim objRespuesta As New Formulario.GetFormularios.Respuesta

        Try

            ' obter divisas
            objRespuesta.Formularios = AccesoDatos.Formulario.getFormularios(Peticion)

            ' preparar codigos e mensagens do respuesta
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
End Class
