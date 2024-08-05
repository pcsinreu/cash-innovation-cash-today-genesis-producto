Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Divisa
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DBHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' Classe AccionMedioPago
''' </summary>
''' <remarks></remarks>
''' <history>
''' [pda] 19/02/2009 Criado
''' </history>
Public Class AccionMedioPago
    Implements ContractoServicio.IMedioPago

    ''' <summary>
    ''' Obtém os medios de pagos através do codigo, descrição, vigente,  codigo divisa, descrição divisa, codigo tipo medio pago e descrição tipo medio pago. Caso nenhum seja informado, todos registros serão retornados.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 19/02/2009 Criado
    ''' </history>
    Public Function GetMedioPagos(Peticion As ContractoServicio.MedioPago.GetMedioPagos.Peticion) As ContractoServicio.MedioPago.GetMedioPagos.Respuesta Implements ContractoServicio.IMedioPago.GetMedioPagos
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.MedioPago.GetMedioPagos.Respuesta

        Try
            ' obter divisas
            objRespuesta.MedioPagos = AccesoDatos.MedioPago.GetMedioPagos(Peticion)

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
    ''' Obtém os terminos medios de pagos através do codigo médio pago.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 20/02/2009 Criado
    ''' </history>
    Public Function GetMedioPagoDetail(Peticion As ContractoServicio.MedioPago.GetMedioPagoDetail.Peticion) As ContractoServicio.MedioPago.GetMedioPagoDetail.Respuesta Implements ContractoServicio.IMedioPago.GetMedioPagoDetail
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.MedioPago.GetMedioPagoDetail.Respuesta

        Try
            ' obter divisas
            objRespuesta.MedioPagos = AccesoDatos.MedioPago.GetMedioPagoDetail(Peticion)

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
    ''' Verifica se o codigo Medio de Pago já existe
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>[pda] 25/02/2009 Criado</history>
    Public Function VerificarCodigoMedioPago(Peticion As ContractoServicio.MedioPago.VerificarCodigoMedioPago.Peticion) As ContractoServicio.MedioPago.VerificarCodigoMedioPago.Respuesta Implements ContractoServicio.IMedioPago.VerificarCodigoMedioPago
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.MedioPago.VerificarCodigoMedioPago.Respuesta

        Try

            ' Verificar os parametros obrigatorios
            If String.IsNullOrEmpty(Peticion.Codigo) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("014_msg_MedioPagoCodigoVazio"))
            End If

            If String.IsNullOrEmpty(Peticion.Divisa) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("014_msg_MedioPagoCodigoTipoMedioPagoVazio"))
            End If

            ' executar verificação no banco
            objRespuesta.Existe = AccesoDatos.MedioPago.VerificarCodigoMedioPago(Peticion)

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
    ''' Verifica se o codigo de Termino do Medio de Pago já existe
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>[pda] 25/02/2009 Criado</history>
    Public Function VerificarCodigoTerminoMedioPago(Peticion As ContractoServicio.MedioPago.VerificarCodigoTerminoMedioPago.Peticion) As ContractoServicio.MedioPago.VerificarCodigoTerminoMedioPago.Respuesta Implements ContractoServicio.IMedioPago.VerificarCodigoTerminoMedioPago
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.MedioPago.VerificarCodigoTerminoMedioPago.Respuesta

        Try

            ' Verificar se codigo do medio de pago foi enviado
            If String.IsNullOrEmpty(Peticion.Codigo) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("014_msg_TerminoMedioPagoCodigoVazio"))
            End If
            If String.IsNullOrEmpty(Peticion.CodigoMedioPago) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("014_msg_MedioPagoCodigoVazio"))
            End If


            ' executar verificação no banco
            objRespuesta.Existe = AccesoDatos.MedioPago.VerificarCodigoTerminoMedioPago(Peticion)

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
    ''' 
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetMedioPago(Peticion As ContractoServicio.MedioPago.SetMedioPago.Peticion) As ContractoServicio.MedioPago.SetMedioPago.Respuesta Implements ContractoServicio.IMedioPago.SetMedioPago

        Dim objRespuesta As New ContractoServicio.MedioPago.SetMedioPago.Respuesta
        objRespuesta.RespuestaMedioPagos = New ContractoServicio.MedioPago.SetMedioPago.RespuestaMedioPagoColeccion
        Dim temErro As Boolean = False

        For Each objMedioPago As ContractoServicio.MedioPago.SetMedioPago.MedioPago In Peticion.MedioPagos

            Dim objRespuestaMedioPago As New ContractoServicio.MedioPago.SetMedioPago.RespuestaMedioPago
            objRespuestaMedioPago.Codigo = objMedioPago.Codigo
            objRespuestaMedioPago.Descripcion = objMedioPago.Descripcion

            Try

                ' Verificar se codigo do medio de pago foi enviado
                If String.IsNullOrEmpty(objMedioPago.Codigo) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("014_msg_MedioPagoCodigoVazio"))
                End If

                'Removendo trecho codigo
                'Se cambia la operación  setMedioPago para que el parámetro  codigoAccesoMedioPago no sea obligatorio. 
                '(Task 2847:2687 - IAC - Medios de Pagos - Cambios en la pantalla y servicio para integración con SOL - Cambiar Servicio)
               

                Dim OidDivisa As String = AccesoDatos.Divisa.ObterOidDivisa(objMedioPago.CodigoDivisa)

                If String.IsNullOrEmpty(objMedioPago.CodigoAccesoMedioPago) Then
                    If AccesoDatos.MedioPago.VerificarCodAccesoMedioPagoExiste(objMedioPago.CodigoAccesoMedioPago, objMedioPago.Codigo, OidDivisa) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("014_msg_codigo_acceso_existe"))
                    End If
                End If

                ' obter oid medio pado
                Dim oidMedioPago As String = IAC.AccesoDatos.MedioPago.ObterOidMedioPago(objMedioPago.CodigoDivisa, objMedioPago.Codigo, objMedioPago.CodigoTipoMedioPago)

                ' se encontrou oid medio pago
                If oidMedioPago <> String.Empty Then

                    If Not objMedioPago.Vigente AndAlso IAC.AccesoDatos.MedioPago.VerificarEntidadesVigentesComMedioPago(oidMedioPago) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("014_msg_MedioPagoUtilizado"))
                    Else
                        ModificarMedioPago(objMedioPago, Peticion.CodigoUsuario, oidMedioPago)
                    End If

                Else

                    ' Verificar se a descrição do medio de pago foi enviada
                    If String.IsNullOrEmpty(objMedioPago.Descripcion) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("014_msg_MedioPagoDescripcionVazio"))
                    End If

                    ' Verificar se o codigo do tipo de medio pago foi enviado
                    If String.IsNullOrEmpty(objMedioPago.CodigoTipoMedioPago) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("014_msg_MedioPagoCodigoTipoMedioPagoVazio"))
                    End If

                    ' Verificar se codigo da divisa do medio de pago foi enviado
                    If String.IsNullOrEmpty(objMedioPago.CodigoDivisa) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("014_msg_MedioPagoCodigoDivisaVazio"))
                    End If

                    ' Verificar se ja existe uma denominacion com o codigo medio pago informado
                    If AccesoDatos.Denominacion.VerificarSeHayDenominacionConElCodigo(objMedioPago.Codigo) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("014_msg_MedioPagoCodigoDenominacion"))
                    End If

                    'Insere o medio de pago
                    IAC.AccesoDatos.MedioPago.AltaMedioPago(objMedioPago, Peticion.CodigoUsuario)

                End If

                objRespuestaMedioPago.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuestaMedioPago.MensajeError = String.Empty

            Catch ex As Excepcion.NegocioExcepcion

                'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
                objRespuestaMedioPago.CodigoError = ex.Codigo
                objRespuestaMedioPago.MensajeError = ex.Descricao
                temErro = False

            Catch ex As Exception

                'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
                objRespuestaMedioPago.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuestaMedioPago.MensajeError = ex.ToString()
                temErro = True

            Finally

                objRespuesta.RespuestaMedioPagos.Add(objRespuestaMedioPago)

            End Try

        Next
        If temErro Then
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = Traduzir("014_msg_ErroCollecionMedioPagos")
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        Else
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty
        End If

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Metodo responsaval por fazer o update, exclusão ou inserção de medios de pago.
    ''' </summary>
    ''' <param name="objMedioPago"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 26/02/2009 Created
    ''' </history>
    Private Sub ModificarMedioPago(objMedioPago As ContractoServicio.MedioPago.SetMedioPago.MedioPago, _
                                   CodigoUsuario As String, _
                                   oidMedioPago As String)

        Dim objtransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

        'Atualiza o Canal
        IAC.AccesoDatos.MedioPago.ActualizarMedioPago(objMedioPago, CodigoUsuario, oidMedioPago, objtransacion)

        If objMedioPago.TerminosMedioPago IsNot Nothing _
            AndAlso objMedioPago.TerminosMedioPago.Count > 0 Then

            ' Chamar metodo que Retorna todos os terminos do medio de pago com o objetivo de ganhar performace
            ' não sendo necessário ir ao banco verificar se existe em cada processamento
            Dim objTerminos As New ContractoServicio.MedioPago.SetMedioPago.TerminoMedioPagoColeccion
            objTerminos = IAC.AccesoDatos.TerminoMedioPago.BuscaTodosTerminos(oidMedioPago)

            For Each objTermino As IAC.ContractoServicio.MedioPago.SetMedioPago.TerminoMedioPago In objMedioPago.TerminosMedioPago

                ' Verificar se codigo do termino foi enviado
                If String.IsNullOrEmpty(objTermino.Codigo) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("014_msg_TerminoMedioPagoCodigoVazio"))
                End If

                ' Utilizando linq verifica em memoria se o término existe (terminos do banco)
                ' Se existe chama o modificiacion de término medio de pago
                ' Se não existe chama o insert de término medio de pago
                If Not VerificarTerminoExiste(objTerminos, objTermino.Codigo) Then

                    ' Verificar se descricao do termino foi enviado
                    If String.IsNullOrEmpty(objTermino.Descripcion) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("014_msg_TerminoMedioPagoDescripcionVazio"))
                    End If

                    ' verificar se o codigo do termino foi enviado
                    If String.IsNullOrEmpty(objTermino.CodigoFormato) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("014_msg_TerminoMedioPagoCodigoFormatoVazio"))
                    End If

                    If objTermino.CodigoFormato.Equals(ContractoServicio.Constantes.COD_FORMATO_TEXTO) OrElse _
                       objTermino.CodigoFormato.Equals(ContractoServicio.Constantes.COD_FORMATO_ENTERO) OrElse _
                       objTermino.CodigoFormato.Equals(ContractoServicio.Constantes.COD_FORMATO_DECIMAL) Then

                        ' verificar se a longitude foi enviada
                        If objTermino.Longitud < 1 Then
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("014_msg_TerminoMedioPagoLongitudeInvalido"))
                        End If

                    End If

                    ' verificar se a orden do termino foi enviado
                    If String.IsNullOrEmpty(objTermino.OrdenTermino) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("014_msg_TerminoMedioPagoOrdenTerminoVazio"))
                    End If

                    'Insere o Término                                        
                    IAC.AccesoDatos.TerminoMedioPago.AltaTerminoMedioPago(objTermino, CodigoUsuario, oidMedioPago, objtransacion)

                Else

                    'Atualizao o Término
                    ModificarTerminoMedioPago(objTermino, CodigoUsuario, oidMedioPago, objtransacion)

                End If

            Next

        End If

        ' caso o medio pago nao seja vigente
        If Not objMedioPago.Vigente Then
            ' deve efetuar a baixa logica para todos os terminos do medio pago
            IAC.AccesoDatos.TerminoMedioPago.BajaTerminoMediosPagoPorMedioPago(oidMedioPago, CodigoUsuario, objtransacion)
        End If

        objtransacion.RealizarTransacao()

    End Sub

    ''' <summary>
    ''' Verifica se o Término Existe
    ''' </summary>
    ''' <param name="objTerminos"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 26/02/2009 Created
    ''' </history>
    Private Function VerificarTerminoExiste(objTerminos As IAC.ContractoServicio.MedioPago.SetMedioPago.TerminoMedioPagoColeccion, codigoTermino As String) As Boolean

        Dim retorno = From c In objTerminos Where c.Codigo = codigoTermino

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' ''' Metodo responsaval por fazer o update, exclusão ou inserção de terminos de medio de pago e seus valores.
    ''' </summary>
    ''' <param name="objTerminoMedioPago"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 26/02/2009 Created
    ''' </history>
    Private Sub ModificarTerminoMedioPago(objTerminoMedioPago As ContractoServicio.MedioPago.SetMedioPago.TerminoMedioPago, _
                                          CodigoUsuario As String, _
                                          oidMedioPago As String, _
                                          ByRef objTransacao As Transacao)

        'Atualiza o Canal
        IAC.AccesoDatos.TerminoMedioPago.ActualizarTerminoMedioPago(objTerminoMedioPago, CodigoUsuario, oidMedioPago, objTransacao)

        If objTerminoMedioPago.ValoresTermino IsNot Nothing AndAlso objTerminoMedioPago.ValoresTermino.Count > 0 Then

            ' obter oid termino
            Dim oidTermino As String = IAC.AccesoDatos.TerminoMedioPago.ObterOidTerminoMedioPago(objTerminoMedioPago.Codigo, oidMedioPago)

            ' Chamar metodo que Retorna todos os valores de terminos do medio de pago com o objetivo de ganhar performace
            ' não sendo necessário ir ao banco verificar se existe em cada processamento
            Dim objValoresTerminosMedioPago As New ContractoServicio.MedioPago.SetMedioPago.ValorTerminoColeccion
            objValoresTerminosMedioPago = IAC.AccesoDatos.ValorTerminoMedioPago.BuscaTodosValoresTerminos(oidTermino)

            For Each objValorTerminoMedioPago As IAC.ContractoServicio.MedioPago.SetMedioPago.ValorTermino In objTerminoMedioPago.ValoresTermino

                ' Verificar se codigo do termino foi enviado
                If String.IsNullOrEmpty(objValorTerminoMedioPago.Codigo) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("014_msg_ValorTerminoMedioPagoCodigoVazio"))
                End If

                ' Utilizando linq verifica em memoria se o valore de termino existe (terminos do banco)
                ' Se não existe chama o insertar termino
                ' Se existe chama o modificiacion de termino
                If Not VerificarValorTerminoExiste(objValoresTerminosMedioPago, objValorTerminoMedioPago.Codigo) Then

                    ' Verificar se codigo do termino foi enviado
                    If String.IsNullOrEmpty(objValorTerminoMedioPago.Descripcion) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("014_msg_ValorTerminoMedioPagoDescripcionVazio"))
                    End If

                    IAC.AccesoDatos.ValorTerminoMedioPago.AltaValorTerminoMedioPago(objValorTerminoMedioPago, CodigoUsuario, oidTermino, objTransacao)

                Else

                    'Atualiza o valor de termino                    
                    IAC.AccesoDatos.ValorTerminoMedioPago.ActualizarValorTerminoMedioPago(objValorTerminoMedioPago, CodigoUsuario, oidTermino, objTransacao)

                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Verifica se o Valor de Término Existe
    ''' </summary>
    ''' <param name="objValoresTerminos"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 26/02/2009 Created
    ''' </history>
    Private Function VerificarValorTerminoExiste(objValoresTerminos As IAC.ContractoServicio.MedioPago.SetMedioPago.ValorTerminoColeccion, codigoValorTermino As String) As Boolean

        Dim retorno = From c In objValoresTerminos Where c.Codigo = codigoValorTermino

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 05/02/2010 - Criado
    ''' </history>
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IMedioPago.Test
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