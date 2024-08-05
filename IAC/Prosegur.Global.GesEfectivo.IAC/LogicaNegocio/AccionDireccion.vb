Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Delegacion
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DbHelper
Imports Prosegur.Global.GesEfectivo.IAC.AccesoDatos
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionDireccion
    Implements ContractoServicio.IDireccion

    ''' <summary>
    ''' Assinatura do método GetDirecciones
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 25/04/2013 Criado
    ''' </history>
    Public Function GetDirecciones(Peticion As ContractoServicio.Direccion.GetDirecciones.Peticion) As ContractoServicio.Direccion.GetDirecciones.Respuesta Implements ContractoServicio.IDireccion.GetDirecciones

        'Cria objeto de resposta
        Dim objRespuesta As New ContractoServicio.Direccion.GetDirecciones.Respuesta

        Try
            'Verfica se os parametros de paginação foram enviados
            If Peticion.ParametrosPaginacion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))
            ElseIf (Peticion.ParametrosPaginacion.RealizarPaginacion AndAlso Peticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))
            End If

            objRespuesta.Direccion = AccesoDatos.Direccion.GetDirecciones(Peticion, objRespuesta.ParametrosPaginacion)
            objRespuesta.Resultado = 0
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty
        Catch ex As Excepcion.NegocioExcepcion
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        End Try
        'Retorna os registros contidos no banco de dados
        Return objRespuesta
    End Function

    ''' <summary>
    ''' Assinatura do método SetDirecciones
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 25/04/2013 Criado
    ''' </history>
    Public Function SetDirecciones(Peticion As ContractoServicio.Direccion.SetDirecciones.Peticion) As ContractoServicio.Direccion.SetDirecciones.Respuesta Implements ContractoServicio.IDireccion.SetDirecciones

        'Cria objeto de Respuesta
        Dim objRespuesta As New ContractoServicio.Direccion.SetDirecciones.Respuesta

        Try

            Dim objTransacion As New Transacao(Constantes.CONEXAO_GE)

            Dim bolBaja As Boolean
            Dim CodTipoTablaGenesis As String = String.Empty

            EjecutarSetDireccion(Peticion, bolBaja, CodTipoTablaGenesis, objTransacion)

            objTransacion.RealizarTransacao()

            objRespuesta.bolBaja = bolBaja
            objRespuesta.codTipoTablaGenesis = CodTipoTablaGenesis
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        End Try
        'Retorna o resultado da inserção ou atualização
        Return objRespuesta
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <param name="objTransacion"></param>
    ''' <remarks></remarks>
    Public Shared Sub EjecutarSetDireccion(Peticion As ContractoServicio.Direccion.SetDirecciones.Peticion, _
                                           ByRef bolBaja As Boolean, ByRef CodTipoTablaGenesis As String, _
                                           ByRef objTransacion As Transacao)

        If Peticion.bolBaja = False Then

            'Verifica se os parametros foram enviados pela petição
            If String.IsNullOrEmpty(Peticion.oidTablaGenesis) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "oidTablaGenesis"))
            End If

            If Peticion.gmtModificacion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "gmtModificacion"))
            End If

            If String.IsNullOrEmpty(Peticion.desUsuarioModificacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "desUsuarioModificacion"))
            End If

            If String.IsNullOrEmpty(Peticion.codTipoTablaGenesis) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "codTipoTablaGenesis"))
            End If

            'Se a petição possui Oid atualiza, senão insere 'VERIFIAR SE REALMENTE NAO TERÁ MAIS ALTERAÇÃO
            If String.IsNullOrEmpty(Peticion.oidDireccion) Then
                AccesoDatos.Direccion.SetDirecciones(Peticion, Peticion.oidDireccion, CodTipoTablaGenesis, objTransacion)

            Else
                AccesoDatos.Direccion.AtualizaDireccion(Peticion, Peticion.oidDireccion, CodTipoTablaGenesis, objTransacion)
            End If
        Else
            If String.IsNullOrEmpty(Peticion.oidDireccion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("057_msg_erro_OidDireccion"), "oidDireccion"))
            End If

            AccesoDatos.Direccion.BajaDireccion(Peticion, bolBaja, CodTipoTablaGenesis, objTransacion)

        End If

    End Sub

    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 18/06/2013 - Criado
    ''' </history>
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IDireccion.Test
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