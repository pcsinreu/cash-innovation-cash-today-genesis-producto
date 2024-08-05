Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Divisa
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DBHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionAgrupacion
    Implements ContractoServicio.IAgrupacion

    ''' <summary>
    ''' Verifica se um codigo agrupacion existe na base
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Public Function VerificarCodigoAgrupacion(objPeticion As ContractoServicio.Agrupacion.VerificarCodigoAgrupacion.Peticion) As ContractoServicio.Agrupacion.VerificarCodigoAgrupacion.Respuesta Implements ContractoServicio.IAgrupacion.VerificarCodigoAgrupacion

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Agrupacion.VerificarCodigoAgrupacion.Respuesta

        Try

            ' verificar se o codigo existe
            objRespuesta.Existe = AccesoDatos.Agrupacion.VerificarCodigoAgrupacion(objPeticion)

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

    ''' <summary>
    ''' Verifica se a descrição da agrupacion existe na base
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Public Function VerificarDescripcionAgrupacion(objPeticion As ContractoServicio.Agrupacion.VerificarDescripcionAgrupacion.Peticion) As ContractoServicio.Agrupacion.VerificarDescripcionAgrupacion.Respuesta Implements ContractoServicio.IAgrupacion.VerificarDescripcionAgrupacion

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Agrupacion.VerificarDescripcionAgrupacion.Respuesta

        Try

            ' verificar se a descrição existe
            objRespuesta.Existe = AccesoDatos.Agrupacion.VerificarDescripcionAgrupacion(objPeticion)

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

    ''' <summary>
    ''' Obtém as agrupaciones
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Public Function GetAgrupaciones(objPeticion As ContractoServicio.Agrupacion.GetAgrupaciones.Peticion) As ContractoServicio.Agrupacion.GetAgrupaciones.Respuesta Implements ContractoServicio.IAgrupacion.GetAgrupaciones

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Agrupacion.GetAgrupaciones.Respuesta

        Try

            ' verificar se a descrição existe
            objRespuesta.Agrupaciones = AccesoDatos.Agrupacion.getAgrupaciones(objPeticion)

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

    ''' <summary>
    ''' Obtém detalhes da agrupação
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 03/02/2009 Criado
    ''' </history>
    Public Function GetAgrupacionesDetail(objPeticion As ContractoServicio.Agrupacion.GetAgrupacionesDetail.Peticion) As ContractoServicio.Agrupacion.GetAgrupacionesDetail.Respuesta Implements ContractoServicio.IAgrupacion.GetAgrupacionesDetail

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Agrupacion.GetAgrupacionesDetail.Respuesta

        Try

            ' verificar se foi informado algum item
            If objPeticion.CodigoAgrupacion.Count = 0 Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("003_msg_NenhumCodigoAgrupacionInformado"))
            End If

            ' obter as agrupaciones
            objRespuesta.Agrupaciones = AccesoDatos.Agrupacion.ObterAgrupaciones(objPeticion)

            ' se retornou alguma agrupacion
            If objRespuesta.Agrupaciones IsNot Nothing _
                AndAlso objRespuesta.Agrupaciones.Count > 0 Then

                ' para cada agrupacion encontrada
                For Each objAgrupacion In objRespuesta.Agrupaciones

                    ' obter divisas para a agrupacao
                    objAgrupacion.Divisas = AccesoDatos.Divisa.ObterDivisasPorAgrupacion(objAgrupacion.Codigo)

                    ' se encontrou alguma divisa
                    If objAgrupacion.Divisas IsNot Nothing _
                        AndAlso objAgrupacion.Divisas.Count > 0 Then

                        ' para cada divisa
                        For Each objDivisa In objAgrupacion.Divisas

                            ' obter tipo medio pago e medio pago
                            objDivisa.TiposMedioPago = AccesoDatos.MedioPago.ObterTipoMedioPagoEMedioPago(objAgrupacion.Codigo, objDivisa.CodigoIso)

                        Next

                    End If

                Next

            End If

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

    ''' <summary>
    ''' Insere, altera e baixa agrupaciones
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 05/02/2009 Criado
    ''' </history>
    Public Function SetAgrupaciones(objPeticion As ContractoServicio.Agrupacion.SetAgrupaciones.Peticion) As ContractoServicio.Agrupacion.SetAgrupaciones.Respuesta Implements ContractoServicio.IAgrupacion.SetAgrupaciones

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Agrupacion.SetAgrupaciones.Respuesta
        objRespuesta.RespuestaAgrupaciones = New ContractoServicio.Agrupacion.SetAgrupaciones.RespuestaAgrupacionColeccion

        ' criar flag que controle se houve erro
        Dim temErro As Boolean = False

        ' para casa agrupacion
        For Each objAgrupacion As ContractoServicio.Agrupacion.SetAgrupaciones.Agrupacion In objPeticion.Agrupaciones

            ' criar objeto respuesta agrupacion
            Dim objRespuestaAgrupacion As New ContractoServicio.Agrupacion.SetAgrupaciones.RespuestaAgrupacion
            objRespuestaAgrupacion.Codigo = objAgrupacion.Codigo
            objRespuestaAgrupacion.Descripcion = objAgrupacion.Descripcion

            Try

                ' verificar se codigo agrupacion foi enviado
                If String.IsNullOrEmpty(objAgrupacion.Codigo) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("003_msg_AgrupacionCodigoVazio"))
                End If

                ' obter o oid da agrupacion
                Dim oidAgrupacion As String = AccesoDatos.Agrupacion.ObterOidAgrupacion(objAgrupacion.Codigo)

                ' verifica se a agrupacion já existe
                If oidAgrupacion <> String.Empty Then

                    If Not objAgrupacion.Vigente AndAlso AccesoDatos.Agrupacion.VerificarEntidadesVigentesComDivisa(objAgrupacion.Codigo) Then
                        ' lançar erro avisando que existe entidades vigentes que utilizam a agrupacion
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("003_msg_AgrupacionComEntidadeVigente"))
                    Else
                        ' efetuar alteração da agrupacion
                        AccesoDatos.Agrupacion.ActualizarAgrupacion(objAgrupacion, objPeticion.CodigoUsuario, oidAgrupacion)
                    End If

                Else

                    ' verificar se a descrição agrupacion foi enviada
                    If String.IsNullOrEmpty(objAgrupacion.Descripcion) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("003_msg_AgrupacionDescripcionVazio"))
                    End If

                    ' efetuar inserção da agrupacion
                    AccesoDatos.Agrupacion.AltaAgrupacion(objAgrupacion, objPeticion.CodigoUsuario)

                End If

                objRespuestaAgrupacion.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuestaAgrupacion.MensajeError = String.Empty

            Catch ex As Excepcion.NegocioExcepcion

                ' Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
                objRespuestaAgrupacion.CodigoError = ex.Codigo
                objRespuestaAgrupacion.MensajeError = ex.Descricao
                temErro = False

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)

                ' Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
                objRespuestaAgrupacion.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuestaAgrupacion.MensajeError = ex.ToString()
                temErro = True

            Finally

                ' adicionar objeto respuesta
                objRespuesta.RespuestaAgrupaciones.Add(objRespuestaAgrupacion)

            End Try

        Next

        ' caso tenha acontecido algum erro
        If temErro Then
            ' Define o código do erro
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT

            ' Recupera os erros que são de ambiente
            Dim errosAgrupacao = From ra As ContractoServicio.Agrupacion.SetAgrupaciones.RespuestaAgrupacion In objRespuesta.RespuestaAgrupaciones _
                                 Where ra.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT

            If errosAgrupacao IsNot Nothing AndAlso errosAgrupacao.Count > 0 Then
                objRespuesta.MensajeError = errosAgrupacao.First.MensajeError
            Else
                objRespuesta.MensajeError = Traduzir("003_msg_ErroCollecionAgrupacion")
            End If
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        Else
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty
        End If

            Return objRespuesta

    End Function

    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/04/2010 - Criado
    ''' </history>
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IAgrupacion.Test
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