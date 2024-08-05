Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Canal
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DBHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionIac
    Implements ContractoServicio.IIac

    ''' <summary>
    '''Metodo faz a busca das informações adicionais ao cliente.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/02/2009 Created
    ''' </history>
    Public Function GetIac(peticion As ContractoServicio.Iac.GetIac.Peticion) As ContractoServicio.Iac.GetIac.Respuesta Implements ContractoServicio.IIac.GetIac

        Dim objRespuesta As New ContractoServicio.Iac.GetIac.Respuesta

        Try

            objRespuesta.Iacs = AccesoDatos.Iac.GetIac(peticion)
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
    '''Metodo faz a busca detalhada das informações adicionais ao cliente.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/02/2009 Created
    ''' </history>
    Public Function GetIacDetail(peticion As ContractoServicio.Iac.GetIacDetail.Peticion) As ContractoServicio.Iac.GetIacDetail.Respuesta Implements ContractoServicio.IIac.GetIacDetail

        Dim objRespuesta As New ContractoServicio.Iac.GetIacDetail.Respuesta

        Try

            objRespuesta.Iacs = AccesoDatos.Iac.GetIacDetail(peticion)
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
    ''' Metodo responsaval por fazer toda transação de deletar, atualizar e inserir 
    ''' as informações adicionais ao cliente e seus terminos.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2009 Created
    ''' [octavio.piramo] 10/09/2009 Alterado
    ''' </history>
    Public Function SetIac(peticion As ContractoServicio.Iac.SetIac.Peticion) As ContractoServicio.Iac.SetIac.Respuesta Implements ContractoServicio.IIac.SetIac

        Dim objRespuesta As New ContractoServicio.Iac.SetIac.Respuesta

        Try

            ' Verificar se codigo iac foi enviado
            If String.IsNullOrEmpty(peticion.CodidoIac) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("006_msg_IacCodigoVazio"))
            End If

            'verifica se a descrição não e nula
            If String.IsNullOrEmpty(peticion.DescripcionIac) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("006_msg_IacDescripcion"))
            End If

            'verifica se a descrição não e nula
            If peticion.EsInvisible AndAlso peticion.TerminosIac.Exists(Function(f) f.EsObligatorio) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("006_msg_ErroInvisibleObligatorios"))
            End If

            ' obter oid iac
            Dim oidIac As String = IAC.AccesoDatos.Iac.BuscaOidIac(peticion.CodidoIac)

            ' caso encontre um oid
            If oidIac <> String.Empty Then

                ' caso não seja vigente e possui entidades dependentes
                If Not peticion.vigente AndAlso IAC.AccesoDatos.Iac.verificarSiPoseeProcesoVigente(peticion.CodidoIac) Then
                    ' lançar erro de negocio
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("006_msg_IacProcessoVigente"))
                Else
                    ' modificar iac
                    ModificarIac(peticion, oidIac)
                End If

            Else

                'insere a informação adicional e se tiver terminos faz a inserção tambem
                IAC.AccesoDatos.Iac.AltaIac(peticion)

            End If

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
    ''' Metodo responsaval por fazer o update, exclusão ou inserção de terminos iac.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2009 Created
    ''' </history>
    Private Sub ModificarIac(Peticion As ContractoServicio.Iac.SetIac.Peticion, _
                             oidIac As String)

        Dim objtransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

        'Atualiza a informação adicional
        IAC.AccesoDatos.Iac.ActualizarIac(Peticion, objtransacion)

        ' remover relacionamento terminos e iac
        IAC.AccesoDatos.TerminoIac.BajaTerminoPorIac(oidIac, objtransacion)

        ' para cada termino enviado pela aplicação
        For Each terIac As IAC.ContractoServicio.Iac.SetIac.TerminosIac In Peticion.TerminosIac

            ' Verificar se codigo do termino iac foi enviado
            If String.IsNullOrEmpty(terIac.CodigoTermino) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("006_msg_TerminoIacCodigoVazio"))
            End If

            If terIac.EsCampoClave AndAlso terIac.EsTerminoCopia Then

                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("006_msg_ErroCampoClave"))
            End If

            IAC.AccesoDatos.TerminoIac.AltaTerminoPorIac(terIac, Peticion.CodUsuario, oidIac, objtransacion)

        Next

        ' realizar a transação
        objtransacion.RealizarTransacao()

    End Sub

    ''' <summary>
    ''' Verifica se o termino iac Existe
    ''' </summary>
    ''' <param name="objTerminoIac"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2009 Created
    ''' </history>
    Private Function VerificarTerminoIacExiste(objTerminoIac As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion, codigoTerminoIac As String) As Boolean

        Dim teriac = From c In objTerminoIac Where c.CodigoTermino = codigoTerminoIac

        If teriac Is Nothing OrElse teriac.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' Verifica se o codigo da informação adicional já existe.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2009 Created
    ''' </history>
    Public Function VerificarCodigoIac(Peticion As ContractoServicio.Iac.VerificarCodigoIac.Peticion) As ContractoServicio.Iac.VerificarCodigoIac.Respuesta Implements ContractoServicio.IIac.VerificarCodigoIac
        Dim objRespuesta As New ContractoServicio.Iac.VerificarCodigoIac.Respuesta

        Try

            objRespuesta.Existe = AccesoDatos.Iac.VerificarCodigoIac(Peticion.CodigoTerminoIac)
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
    ''' Verifica se a descrição da informação adicional já existe.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2009 Created
    ''' </history>
    Public Function VerificarDescripcionIac(Peticion As ContractoServicio.Iac.VerificarDescripcionIac.Peticion) As ContractoServicio.Iac.VerificarDescripcionIac.Respuesta Implements ContractoServicio.IIac.VerificarDescripcionIac
        Dim objRespuesta As New ContractoServicio.Iac.VerificarDescripcionIac.Respuesta

        Try

            objRespuesta.Existe = AccesoDatos.Iac.VerificarDescripcionIac(Peticion.DescripcionTerminoIac)
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
    ''' [anselmo.gois] 04/02/2010 - Criado
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IIac.Test
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
