Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Data
Imports Prosegur.Global.Saldos
Imports Prosegur.Global.Saldos.ContractoServicio

Public Class AccionManipularDocumentos

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Esta operación es utilizada para crear documentos, crear e imprimir documentos, aceptar documentos, crear documentos ya aceptados, substituir documentos, actualizar documentos y rechazar documentos en el Saldos de acuerdo con los parámetros recibidos a través del mensaje de entrada.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Ejecutar(peticion As ManipularDocumentos.Peticion) As ManipularDocumentos.Respuesta

        Dim respuesta As New ManipularDocumentos.Respuesta

        ' setar codigo 0 e mensagem erro vazio
        respuesta.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
        respuesta.MensajeError = String.Empty

        Try

            ' valida os dados passados na petição
            If ValidaDadosPeticion(peticion, respuesta) Then

                ' recupera do web.config os valores de formulários de ingresso de remessas
                Dim codigosFormularioIngreso As String() = RecuperaArrayValoresParametroWebConfig("CodFormularioIngresso")

                ' para cada documento enviado
                For Each doc In peticion.Documentos

                    Dim documentoExistente As Boolean
                    Dim idDocumentoExistente As String = String.Empty
                    Dim idFormularioDocumentoExistente As String = String.Empty
                    Dim estadoDocumentoExistente As String = String.Empty

                    ' verifica se o documento existe (baseado no número externo) e retorna as demais informações
                    ' caso exista algum erro, segue para o próximo documento
                    If Not RecuperaDadosDocumentoExistente(doc, respuesta, idDocumentoExistente, idFormularioDocumentoExistente, estadoDocumentoExistente) Then
                        Continue For
                    End If

                    documentoExistente = Not String.IsNullOrEmpty(idDocumentoExistente)

                    ' aplica todas as regras de negócio no documento
                    ' caso exista algum erro, segue para o próximo documento
                    If Not ValidaDadosDocumento(doc, peticion, respuesta, codigosFormularioIngreso, documentoExistente, idFormularioDocumentoExistente, estadoDocumentoExistente) Then
                        Continue For
                    End If

                    ' verifica o tipo de ação executada no documento
                    Select Case doc.Accion

                        Case Enumeradores.eAccion.Rechazar

                            ' executa a chamada para Rechazar o documento
                            ' caso exista algum erro, segue para o próximo documento
                            If Not ExecutaRechazarDocumento(doc, respuesta) Then
                                Continue For
                            End If

                        Case Else

                            ' executa a chamada do serviço GuardarDatosDocumento
                            ' caso exista algum erro, segue para o próximo documento
                            If Not ExecutaGuardarDatosDocumento(doc, peticion, respuesta, documentoExistente, idDocumentoExistente) Then
                                Continue For
                            End If

                    End Select

                Next

            End If

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            respuesta.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            respuesta.MensajeError = ex.ToString()

        End Try

        Return respuesta

    End Function

    ''' <summary>
    ''' Tenta recuperar os dados de um documento baseado no número externo informado
    ''' </summary>
    ''' <param name="doc"></param>
    ''' <param name="respuesta"></param>
    ''' <param name="idDocumentoExistente"></param>
    ''' <param name="idFormularioDocumentoExistente"></param>
    ''' <param name="estadoDocumentoExistente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RecuperaDadosDocumentoExistente(doc As ManipularDocumentos.Documento, ByRef respuesta As ManipularDocumentos.Respuesta, ByRef idDocumentoExistente As String, ByRef idFormularioDocumentoExistente As String, ByRef estadoDocumentoExistente As String) As Boolean

        Try

            ' pesquisa os documentos com o número externo informado
            Dim dadosDoc As DataTable = Negocio.Documento.BuscaDocumentoPorNumExterno(doc.NumExterno)

            If dadosDoc IsNot Nothing AndAlso dadosDoc.Rows.Count > 0 Then

                Dim docExistente As New Negocio.Documento()

                ' pesquisa os demais dados do primeiro documento retornado
                docExistente.Id = dadosDoc.Rows(0)("IDDOCUMENTO")
                docExistente.Realizar()

                ' popula as variáveis de acordo com os dados retornados
                idDocumentoExistente = docExistente.Id
                idFormularioDocumentoExistente = docExistente.Formulario.Id
                estadoDocumentoExistente = docExistente.EstadoComprobante.Codigo

            End If

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' adiciona uma mensagem com o erro na execução
            AdicionaMensagemResultado(respuesta.Resultados, doc.IdDocumento, doc.NumExterno, Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, ex.ToString())
            Return False
        End Try

        Return True

    End Function

    ''' <summary>
    ''' Recupera o código ClienteOrigen de acordo com as regras do Autómatas
    ''' </summary>
    ''' <param name="doc"></param>
    ''' <param name="respuesta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RecuperaClienteOrigenReglaAutomatas(doc As ManipularDocumentos.Documento, ByRef respuesta As ManipularDocumentos.Respuesta, ByRef idClienteOrigen As String) As Boolean

        Dim codigoClienteOriginal As String = doc.CodigoCliente
        Dim codigoSubClienteOriginal As String = doc.CodigoSubCliente
        Dim codigoClienteFormatado As String = String.Empty
        Dim codigoSubClienteFormatado As String = String.Empty
        Dim codigoClienteOrigemTemp As String = String.Empty

        ' *************************
        ' Formatação Código Cliente
        ' *************************
        If Not String.IsNullOrEmpty(codigoClienteOriginal) Then

            ' -	Si existe en codigoCliente el guión “-” se recuperará los valores a izquierda del guión.
            If codigoClienteOriginal.IndexOf("-") > 0 Then
                codigoClienteFormatado = codigoClienteOriginal.Substring(0, codigoClienteOriginal.IndexOf("-"))
            Else
                codigoClienteFormatado = codigoClienteOriginal
            End If

            ' -	El código recuperado no debe ultrapasar 5 caracteres. Entonces se recuperará antes del primer 
            ' guión, las cinco primeras posiciones de la derecha para izquierda. Caso el código no tenga el 
            ' guión "-" se recuperará las cinco últimas posiciones del código.
            If codigoClienteFormatado.Length > 5 Then
                Dim diferencaTamanhoMaximo As Integer = codigoClienteFormatado.Length - 5
                codigoClienteFormatado = codigoClienteFormatado.Substring(diferencaTamanhoMaximo, 5)
            End If

        Else

            ' se não existe código cliente, o código não deve continuar
            ' a sua execução
            Return False

        End If

        ' ****************************
        ' Formatação Código SubCliente
        ' ****************************
        If Not String.IsNullOrEmpty(codigoSubClienteOriginal) Then

            ' -	Si existe en codigoSubcliente el guión “-” se recuperará los valores a derecha del guión. 
            ' o	En caso de existir más de un guión en el código, se recuperará los valores a derecha del último guión. 
            If codigoSubClienteOriginal.IndexOf("-") > 0 Then
                codigoSubClienteFormatado = codigoSubClienteOriginal.Substring(codigoSubClienteOriginal.LastIndexOf("-") + 1)
            Else
                codigoSubClienteFormatado = codigoSubClienteOriginal
            End If

            ' -	A partir de los valores recuperados se utilizará los tres últimos caracteres, caso el 
            ' valor recuperado tenga menos de 3 caracteres se completará con cero a izquierda, pero se 
            ' la formación del código sea "000" deberá ser desconsiderado.
            If codigoSubClienteFormatado.Length > 3 Then
                Dim diferencaTamanhoMaximo As Integer = codigoSubClienteFormatado.Length - 3
                codigoSubClienteFormatado = codigoSubClienteFormatado.Substring(diferencaTamanhoMaximo, 3)
            End If
            codigoSubClienteFormatado = codigoSubClienteFormatado.PadLeft(3, "0")

        End If

        ' 1ª pesquisa
        ' -	PD_CLIENTE.IDPS = código cliente formateado + "-" + código subcliente formateado
        If Not String.IsNullOrEmpty(codigoSubClienteFormatado) Then
            codigoClienteOrigemTemp = Negocio.Cliente.RealizarBusquedaIdSubClientePuntoServicio(codigoClienteFormatado & "-" & codigoSubClienteFormatado)
            If Not String.IsNullOrEmpty(codigoClienteOrigemTemp) AndAlso Not codigoClienteOrigemTemp.Equals("0") Then
                idClienteOrigen = codigoClienteFormatado & "-" & codigoSubClienteFormatado
                Return True
            End If
        End If

        ' 2ª pesquisa
        ' -	PD_CLIENTE.IDPS = código cliente formateado
        codigoClienteOrigemTemp = Negocio.Cliente.RealizarBusquedaIdSubClientePuntoServicio(codigoClienteFormatado)
        If Not String.IsNullOrEmpty(codigoClienteOrigemTemp) AndAlso Not codigoClienteOrigemTemp.Equals("0") Then
            idClienteOrigen = codigoClienteFormatado
            Return True
        End If

        ' 3ª pesquisa
        ' -	PD_CLIENTE.IDPS = código cliente formateado + ‘-000’
        codigoClienteOrigemTemp = Negocio.Cliente.RealizarBusquedaIdSubClientePuntoServicio(codigoClienteFormatado & "-000")
        If Not String.IsNullOrEmpty(codigoClienteOrigemTemp) AndAlso Not codigoClienteOrigemTemp.Equals("0") Then
            idClienteOrigen = codigoClienteFormatado & "-000"
            Return True
        Else
            ' se chegou até a 3ª pesquisa sem encontrar dados no Saldos
            ' então deve retornar false (informado que não teve sucesso nas buscas)
            Return False
        End If

    End Function

    ''' <summary>
    ''' Executa a chamada para Rechazar um documento
    ''' </summary>
    ''' <param name="doc"></param>
    ''' <param name="respuesta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExecutaRechazarDocumento(doc As ManipularDocumentos.Documento, ByRef respuesta As ManipularDocumentos.Respuesta) As Boolean

        Dim documentoRechazar As New Negocio.Documento()
        Dim usuarioLogin As String

        ' valida se o parâmetro UsuarioLogin existe no web.config e se contém um valor
        If System.Configuration.ConfigurationManager.AppSettings("UsuarioLogin") IsNot Nothing AndAlso Not String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings("UsuarioLogin").Trim()) Then
            usuarioLogin = System.Configuration.ConfigurationManager.AppSettings("UsuarioLogin")
        Else
            AdicionaMensagemResultado(respuesta.Resultados, doc.IdDocumento, doc.NumExterno, Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("11_msg_parametro_vacio_webconfig"), "UsuarioLogin"))
            Return False
        End If

        Try

            ' verifica se o documento existe
            If Negocio.Documento.VerificarExistenciaDocumentoIdPs(doc.IdDocumento, doc.IdCentroProcesoOrigen) > 0 Then

                ' preenche o id do documento
                documentoRechazar.Id = doc.IdDocumento

                ' recupera os dados do documento
                documentoRechazar.Realizar()

                Dim campoCP = documentoRechazar.Formulario.Campos.FirstOrDefault(Function(f) f.Nombre = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.CentroProcesoOrigen)
                Dim cp As Negocio.CentrosProceso = Nothing
                If campoCP IsNot Nothing Then
                    cp = New Negocio.CentrosProceso
                    cp.IdPS = campoCP.Valor
                    cp.Realizar()
                    If cp.Count > 0 Then
                        documentoRechazar.GMTVeranoAjuste = If(Not String.IsNullOrEmpty(cp(0).Planta.CodDelegacionGenesis), CType(Util.GetGMTVeranoAjuste(cp(0).Planta.CodDelegacionGenesis), Short?), Nothing)
                    End If
                End If

                ' efetiva o rechazo
                documentoRechazar.Rechazar()

            Else
                AdicionaMensagemResultado(respuesta.Resultados, doc.IdDocumento, doc.NumExterno, Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("11_msg_documento_no_existe_rechazo"), doc.IdDocumento, doc.IdCentroProcesoOrigen))
                Return False
            End If

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' adiciona uma mensagem com o erro na execução
            AdicionaMensagemResultado(respuesta.Resultados, doc.IdDocumento, doc.NumExterno, Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, ex.ToString())
            Return False
        End Try

        ' adiciona a mensagem de resultado de sucesso
        AdicionaMensagemResultado(respuesta.Resultados, doc.IdDocumento, doc.NumExterno, Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT, String.Empty)

        Return True

    End Function

    ''' <summary>
    ''' Executa a chamada ao serviço de GuardarDatosDocumento
    ''' </summary>
    ''' <param name="doc"></param>
    ''' <param name="peticion"></param>
    ''' <param name="respuesta"></param>
    ''' <param name="documentoExistente"></param>
    ''' <param name="idDocumentoExistente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExecutaGuardarDatosDocumento(ByRef doc As ManipularDocumentos.Documento, peticion As ManipularDocumentos.Peticion, ByRef respuesta As ManipularDocumentos.Respuesta, documentoExistente As Boolean, idDocumentoExistente As String) As Boolean

        Dim peticionGuardarDatosDocumento As New GuardarDatosDocumento.Peticion()
        Dim respuestaGuardarDatosDocumento As GuardarDatosDocumento.Respuesta
        Dim guardarDatosDocumento As New AccionGuardarDatosDocumento()
        Dim usuarioLogin As String
        Dim usuarioClave As String

        ' valida se o parâmetro UsuarioLogin existe no web.config e se contém um valor
        If System.Configuration.ConfigurationManager.AppSettings("UsuarioLogin") IsNot Nothing AndAlso Not String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings("UsuarioLogin").Trim()) Then
            usuarioLogin = System.Configuration.ConfigurationManager.AppSettings("UsuarioLogin")
        Else
            AdicionaMensagemResultado(respuesta.Resultados, doc.IdDocumento, doc.NumExterno, Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("11_msg_parametro_vacio_webconfig"), "UsuarioLogin"))
            Return False
        End If

        ' valida se o parâmetro UsuarioClave existe no web.config e se contém um valor
        If System.Configuration.ConfigurationManager.AppSettings("UsuarioClave") IsNot Nothing AndAlso Not String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings("UsuarioClave").Trim()) Then
            usuarioClave = System.Configuration.ConfigurationManager.AppSettings("UsuarioClave")
        Else
            AdicionaMensagemResultado(respuesta.Resultados, doc.IdDocumento, doc.NumExterno, Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("11_msg_parametro_vacio_webconfig"), "UsuarioClave"))
            Return False
        End If

        Try

            ' configura o documento para ser enviado ao serviço de GuardarDatosDocumento
            If Not PreenchePeticaoGuardarDatosDocumento(doc, respuesta, peticionGuardarDatosDocumento, usuarioLogin, usuarioClave, If(documentoExistente, idDocumentoExistente, String.Empty), peticion.Reglas.Contains(Enumeradores.eRegla.Automata)) Then
                ' se encontrou algum erro, termina a chamada do documento
                Return False
            End If

            ' chama o serviço GuardarDatosDocumento
            respuestaGuardarDatosDocumento = guardarDatosDocumento.Ejecutar(peticionGuardarDatosDocumento)

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' adiciona uma mensagem com o erro na execução
            AdicionaMensagemResultado(respuesta.Resultados, doc.IdDocumento, doc.NumExterno, Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, ex.ToString())
            Return False
        End Try

        ' adiciona a mensagem de resultado de sucesso
        AdicionaMensagemResultado(respuesta.Resultados, doc.IdDocumento, doc.NumExterno, respuestaGuardarDatosDocumento.CodigoError, respuestaGuardarDatosDocumento.MensajeError)

        Return True

    End Function

    ''' <summary>
    ''' Verifica as regras de negócio referentes ao documento
    ''' </summary>
    ''' <param name="doc"></param>
    ''' <param name="peticion"></param>
    ''' <param name="respuesta"></param>
    ''' <param name="codigosFormularioIngreso"></param>
    ''' <param name="documentoExistente"></param>
    ''' <param name="idFormularioDocumentoExistente"></param>
    ''' <param name="estadoDocumentoExistente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidaDadosDocumento(ByRef doc As ManipularDocumentos.Documento, peticion As ManipularDocumentos.Peticion, ByRef respuesta As ManipularDocumentos.Respuesta, codigosFormularioIngreso As String(), documentoExistente As Boolean, idFormularioDocumentoExistente As String, estadoDocumentoExistente As String) As Boolean

        Dim idFormulario As String = doc.IdFormulario

        ' verifica o tipo de ação executada no documento
        Select Case doc.Accion

            Case Enumeradores.eAccion.Crear, _
                 Enumeradores.eAccion.CrearAceptar, _
                 Enumeradores.eAccion.CrearImprimir

                ' verifica se existe algum formulário de ingresso configurado
                ' se não, adiciona o erro ao retorno do serviço (para o documento específico)
                ' e segue para o próximo documento
                If codigosFormularioIngreso Is Nothing OrElse codigosFormularioIngreso.Count = 0 Then
                    AdicionaMensagemResultado(respuesta.Resultados, doc.IdDocumento, doc.NumExterno, Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("11_msg_parametro_vacio_webconfig"), "CodFormularioIngresso"))
                    Return False
                End If

                ' verifica se o documento tem um valor no parâmetro IdFormulario que seja
                ' compatível com a coleção de valores da variável codigosFormularioIngreso
                If codigosFormularioIngreso IsNot Nothing AndAlso codigosFormularioIngreso.Count > 0 AndAlso
                   Array.FindAll(codigosFormularioIngreso, Function(c) c.ToString().Trim().Contains(idFormulario)).Count > 0 Then

                    ' recupera do web.config os valores de formulários de salidas
                    Dim codigosFormularioSalida As String() = RecuperaArrayValoresParametroWebConfig("CodFormularioSalida")

                    ' verifica se existe algum formulário de salida
                    ' se não, adiciona o erro ao retorno do serviço (para o documento específico)
                    ' e segue para o próximo documento
                    If codigosFormularioSalida Is Nothing OrElse codigosFormularioSalida.Count = 0 Then
                        AdicionaMensagemResultado(respuesta.Resultados, doc.IdDocumento, doc.NumExterno, Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("11_msg_parametro_vacio_webconfig"), "CodFormularioSalida"))
                        Return False
                    End If

                    ' verifica se existe um documento com o mesmo número externo 
                    If documentoExistente Then

                        ' uma vez encontrado um número externo repetido, deve verificar
                        ' se existe a regra que permite a inserção de dados repetidos
                        If Not peticion.Reglas.Contains(Enumeradores.eRegla.IngresarRemesaNumeroExternoRepetido) Then
                            AdicionaMensagemResultado(respuesta.Resultados, doc.IdDocumento, doc.NumExterno, Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("11_msg_numeroexterno_no_valido"), doc.NumExterno))
                            Return False
                        End If

                        ' verifica se o idFormulario retornado não é do tipo salida
                        If Not Array.FindAll(codigosFormularioSalida, Function(c) c.ToString().Trim().Contains(idFormularioDocumentoExistente)).Count > 0 Then
                            AdicionaMensagemResultado(respuesta.Resultados, doc.IdDocumento, doc.NumExterno, Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("11_msg_numeroexterno_repetido_sin_salida"))
                            Return False
                        End If

                    End If

                End If

        End Select

        ' verifica se existe um documento com o mesmo número externo 
        If documentoExistente AndAlso idFormulario = idFormularioDocumentoExistente Then

            ' verifica o estado do documento já existente
            Select Case estadoDocumentoExistente.Trim().ToUpper()

                ' valida se o estado do documento existente é impresso 
                Case Prosegur.Global.Saldos.ContractoServicio.Constantes.CONST_ESTADO_COMPROBANTE_IMPRESO
                    ' se a ação desejada é de criar / imprimir
                    If doc.Accion = Enumeradores.eAccion.CrearImprimir Then
                        AdicionaMensagemResultado(respuesta.Resultados, doc.IdDocumento, doc.NumExterno, Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("11_msg_documento_impreso"))
                        Return False
                    End If

                    ' valida se o estado do documento existente foi aceito
                Case Prosegur.Global.Saldos.ContractoServicio.Constantes.CONST_ESTADO_COMPROBANTE_ACEPTADO
                    AdicionaMensagemResultado(respuesta.Resultados, doc.IdDocumento, doc.NumExterno, Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("11_msg_documento_aceito"))
                    Return False

                    ' valida se o estado do documento existente foi recusado
                Case Prosegur.Global.Saldos.ContractoServicio.Constantes.CONST_ESTADO_COMPROBANTE_RECHAZADO
                    AdicionaMensagemResultado(respuesta.Resultados, doc.IdDocumento, doc.NumExterno, Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("11_msg_documento_rechazado"))
                    Return False

                    ' valida se o estado do documento existente esta em processo
                Case Prosegur.Global.Saldos.ContractoServicio.Constantes.CONST_ESTADO_COMPROBANTE_EN_PROCESO
                    ' deve alterar a ação para "Actualizar"
                    doc.Accion = Enumeradores.eAccion.Actualizar

            End Select

        End If

        Return True

    End Function

    ''' <summary>
    ''' Método responsável pelo preenchimento da petição do GuardarDatosDocumento
    ''' </summary>
    ''' <param name="doc"></param>
    ''' <param name="usuarioLogin"></param>
    ''' <param name="usuarioClave"></param>
    ''' <param name="idDocumentoOrigen"></param>
    ''' <param name="reglaAutomata"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PreenchePeticaoGuardarDatosDocumento(doc As ManipularDocumentos.Documento, ByRef respuesta As ManipularDocumentos.Respuesta, ByRef peticao As GuardarDatosDocumento.Peticion, usuarioLogin As String, usuarioClave As String, idDocumentoOrigen As String, reglaAutomata As Boolean) As Boolean

        With peticao
            ' dados gerais
            .Accion = doc.Accion
            .Usuario = New GuardarDatosDocumento.Usuario()
            .Usuario.Login = usuarioLogin
            .Usuario.Clave = usuarioClave
            .Documento = New GuardarDatosDocumento.Documento()
            .Documento.IdDocumento = If(IsNumeric(doc.IdDocumento), Integer.Parse(doc.IdDocumento), 0)
            .Documento.IdCaracteristica = doc.IdFormulario
            .Documento.IdOrigen = If(IsNumeric(idDocumentoOrigen), Integer.Parse(idDocumentoOrigen), 0)
            '.Documento.FechaGestion = Now
            ' campos
            .Documento.Campos = New GuardarDatosDocumento.Campos()
            .Documento.Campos.Add(New GuardarDatosDocumento.Campo() With {.Nombre = Enumeradores.eCampos.NumExterno, .Valor = doc.NumExterno})
            .Documento.Campos.Add(New GuardarDatosDocumento.Campo() With {.Nombre = Enumeradores.eCampos.CentroProcesoOrigen, .Valor = doc.IdCentroProcesoOrigen})

            'preenche campo ".Documento.FechaGestion" com cálculo de GMT
            Dim cp As New Negocio.CentrosProceso
            cp.IdPS = doc.IdCentroProcesoOrigen
            cp.Realizar()
            If cp.Count > 0 Then
                .Documento.FechaGestion = Util.GetDateTime(cp(0).Planta.CodDelegacionGenesis)
            End If

            If reglaAutomata Then
                Dim idClienteOrigen As String = String.Empty
                If Not RecuperaClienteOrigenReglaAutomatas(doc, respuesta, idClienteOrigen) Then
                    AdicionaMensagemResultado(respuesta.Resultados, doc.IdDocumento, doc.NumExterno, Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("11_msg_cliente_origen_regla_automatas"), doc.CodigoCliente, doc.CodigoSubCliente))
                    Return False
                End If
                .Documento.Campos.Add(New GuardarDatosDocumento.Campo() With {.Nombre = Enumeradores.eCampos.ClienteOrigen, .Valor = idClienteOrigen})
            Else
                .Documento.Campos.Add(New GuardarDatosDocumento.Campo() With {.Nombre = Enumeradores.eCampos.ClienteOrigen, .Valor = doc.IdClienteOrigen})
            End If
            .Documento.Campos.Add(New GuardarDatosDocumento.Campo() With {.Nombre = Enumeradores.eCampos.Banco, .Valor = doc.IdCanalOrigen})
            .Documento.Campos.Add(New GuardarDatosDocumento.Campo() With {.Nombre = Enumeradores.eCampos.CentroProcesoDestino, .Valor = doc.IdCentroProcesoDestino})
            .Documento.Campos.Add(New GuardarDatosDocumento.Campo() With {.Nombre = Enumeradores.eCampos.ClienteDestino, .Valor = doc.IdClienteDestino})
            .Documento.Campos.Add(New GuardarDatosDocumento.Campo() With {.Nombre = Enumeradores.eCampos.BancoDeposito, .Valor = doc.IdCanalDestino})
            ' campos extras
            If doc.CamposExtras IsNot Nothing AndAlso doc.CamposExtras.Count > 0 Then
                .Documento.CamposExtras = New GuardarDatosDocumento.CamposExtras()
                For Each campoExtra In doc.CamposExtras
                    .Documento.CamposExtras.Add(New GuardarDatosDocumento.CampoExtra() With {.Nombre = campoExtra.Nombre, .Valor = campoExtra.Valor})
                Next
            End If
            ' bultos
            If doc.Bultos IsNot Nothing AndAlso doc.Bultos.Count > 0 Then
                .Documento.Bultos = New GuardarDatosDocumento.Bultos()
                For Each bulto In doc.Bultos
                    .Documento.Bultos.Add(New GuardarDatosDocumento.Bulto() With {.NumeroPrecinto = bulto.CodPrecinto, .IdDestino = bulto.CodUbicacion, .CodigoBolsa = bulto.IdBultoOrigen})
                Next
            End If
            ' detalles
            If doc.Detalles IsNot Nothing AndAlso doc.Detalles.Count > 0 Then
                .Documento.Detalles = New GuardarDatosDocumento.Detalles()
                For Each detalle In doc.Detalles
                    .Documento.Detalles.Add(New GuardarDatosDocumento.Detalle() With {.IdMoneda = detalle.IdMoneda, .IdEspecie = detalle.IdEspecie, .Cantidad = detalle.Cantidad, .Importe = detalle.Importe})
                Next
            End If
            ' sobres
            If doc.Parciales IsNot Nothing AndAlso doc.Parciales.Count > 0 Then
                .Documento.Sobres = New GuardarDatosDocumento.Sobres()
                For Each parcial In doc.Parciales
                    .Documento.Sobres.Add(New GuardarDatosDocumento.Sobre() With {.NumeroSobre = parcial.NumeroParcial, .ConDiferencia = parcial.ConDiferencia, .Importe = parcial.Importe, .IdMoneda = parcial.IdMoneda})
                Next
            End If


        End With

        Return True

    End Function

    ''' <summary>
    ''' Recupera um parâmetro informado no Web.Config em forma de Array de valores
    ''' </summary>
    ''' <param name="nomeParametro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RecuperaArrayValoresParametroWebConfig(nomeParametro As String) As String()

        Dim valores As String() = {}

        ' valida se o parâmetro informado na variável nomeParametro existe no web.config e se contém um valor
        If System.Configuration.ConfigurationManager.AppSettings(nomeParametro) IsNot Nothing AndAlso Not String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings(nomeParametro).Trim()) Then

            ' o parâmetro informado na variável nomeParametro pode ser informado com valores 
            ' separados por ; e por este motivo, se torna uma coleção de valores
            valores = System.Configuration.ConfigurationManager.AppSettings(nomeParametro).Split(";")

        End If

        Return valores

    End Function

    ''' <summary>
    ''' Adiciona um resultado a coleção de resultados
    ''' </summary>
    ''' <param name="resultados"></param>
    ''' <param name="idDocumento"></param>
    ''' <param name="numExterno"></param>
    ''' <param name="codigoError"></param>
    ''' <param name="mensajeError"></param>
    ''' <remarks></remarks>
    Private Sub AdicionaMensagemResultado(ByRef resultados As ManipularDocumentos.Resultados,
                                          idDocumento As String,
                                          numExterno As String,
                                          codigoError As Integer,
                                          mensajeError As String)

        If resultados Is Nothing Then
            resultados = New ManipularDocumentos.Resultados()
        End If

        Dim resultado As New ManipularDocumentos.Resultado()
        With resultado
            .IdDocumento = idDocumento
            .NumExterno = numExterno
            .CodigoError = codigoError
            .MensajeError = mensajeError
        End With

        resultados.Add(resultado)

    End Sub

    ''' <summary>
    ''' Verifica se todos os dados da petição estaõ de acordo com especificado (campos obrigatórios, valores, etc)
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <param name="Respuesta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidaDadosPeticion(peticion As ManipularDocumentos.Peticion, _
                                         ByRef respuesta As ManipularDocumentos.Respuesta) As Boolean

        ' valida se foi informado pelo menos um documento
        If peticion Is Nothing OrElse peticion.Documentos Is Nothing OrElse peticion.Documentos.Count = 0 Then
            respuesta.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            respuesta.MensajeError = String.Format(Traduzir("Gen_msg_atributo_vacio"), "Documentos")
            Return False
        End If

        ' valida se alguma regra foi especificada
        ' caso seja, tem que ser uma regra válida
        If peticion.Reglas IsNot Nothing AndAlso peticion.Reglas.Count > 0 Then

            Dim regrasInvalidas = (From r In peticion.Reglas
                                   Where Not [Enum].IsDefined(GetType(Enumeradores.eRegla), r)
                                   Select r)

            If regrasInvalidas.Count > 0 Then
                respuesta.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                respuesta.MensajeError = String.Format(Traduzir("11_msg_regla_invalida"), regrasInvalidas.FirstOrDefault())
                Return False
            End If

        End If

        ' variável usada pra identificar as mensagens de erro nos documentos
        Dim indice As Integer = 0
        ' variável usada para armazenar os erros encontrados nos documentos
        Dim resultados As New ManipularDocumentos.Resultados()

        For Each doc In peticion.Documentos

            indice += 1

            Dim resultado As New ManipularDocumentos.Resultado()

            ' valida se o documento é vazio
            If doc Is Nothing Then
                resultado.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                resultado.MensajeError = Traduzir("11_msg_documentos_documento_vacio")
                resultados.Add(resultado)
                Continue For
            End If

            resultado.IdDocumento = doc.IdDocumento
            resultado.NumExterno = doc.NumExterno

            ' valida se o parâmetro IdFormulario foi informado
            If String.IsNullOrEmpty(doc.IdFormulario) Then
                resultado.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                resultado.MensajeError = String.Format(Traduzir("11_msg_atributo_vacio_documento"), "IdFormulario", indice.ToString())
                resultados.Add(resultado)
                Continue For
            End If

            ' valida se o parâmetro NumExterno foi informado
            If String.IsNullOrEmpty(doc.NumExterno) Then
                resultado.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                resultado.MensajeError = String.Format(Traduzir("11_msg_atributo_vacio_documento"), "NumExterno", indice.ToString())
                resultados.Add(resultado)
                Continue For
            End If

            ' valida se o parâmetro IdCentroProcesoOrigen foi informado
            If String.IsNullOrEmpty(doc.IdCentroProcesoOrigen) Then
                resultado.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                resultado.MensajeError = String.Format(Traduzir("11_msg_atributo_vacio_documento"), "IdCentroProcesoOrigen", indice.ToString())
                resultados.Add(resultado)
                Continue For
            End If

            ' valida se o parâmetro IdClienteOrigen foi informado
            If String.IsNullOrEmpty(doc.IdClienteOrigen) Then
                resultado.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                resultado.MensajeError = String.Format(Traduzir("11_msg_atributo_vacio_documento"), "IdClienteOrigen", indice.ToString())
                resultados.Add(resultado)
                Continue For
            End If

            ' valida se o parâmetro IdCanalOrigen foi informado
            If String.IsNullOrEmpty(doc.IdCanalOrigen) Then
                resultado.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                resultado.MensajeError = String.Format(Traduzir("11_msg_atributo_vacio_documento"), "IdCanalOrigen", indice.ToString())
                resultados.Add(resultado)
                Continue For
            End If

            ' valida se algum campo extra foi especificado
            ' caso seja, tem que validar se foram informados todos os valores
            If doc.CamposExtras IsNot Nothing AndAlso doc.CamposExtras.Count > 0 Then

                Dim indiceCampoExtra As Integer = 0

                For Each campoExtra In doc.CamposExtras

                    indiceCampoExtra += 1

                    ' valida se o parâmetro Nombre foi informado
                    If String.IsNullOrEmpty(campoExtra.Nombre) Then
                        resultado.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                        resultado.MensajeError = String.Format(Traduzir("11_msg_atributo_vacio_documento_campoextra"), "Nombre", indiceCampoExtra.ToString(), indice.ToString())
                        resultados.Add(resultado)
                        Exit For
                    End If

                    ' valida se o parâmetro Valor foi informado
                    If String.IsNullOrEmpty(campoExtra.Valor) Then
                        resultado.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                        resultado.MensajeError = String.Format(Traduzir("11_msg_atributo_vacio_documento_campoextra"), "Valor", indiceCampoExtra.ToString(), indice.ToString())
                        resultados.Add(resultado)
                        Exit For
                    End If

                Next

                ' valida se encontrou algum erro nas validações de detalles, 
                ' se sim, segue o loop para o próximo documento
                If resultado.CodigoError > 0 Then
                    Continue For
                End If

            End If

            ' valida se algum bulto foi especificado
            ' caso seja, tem que validar se foram informados todos os valores
            If doc.Bultos IsNot Nothing AndAlso doc.Bultos.Count > 0 Then

                Dim indiceBulto As Integer = 0

                For Each bulto In doc.Bultos

                    indiceBulto += 1

                    ' valida se o bulto é vazio
                    If bulto Is Nothing Then
                        resultado.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                        resultado.MensajeError = String.Format(Traduzir("11_msg_atributo_vacio_bulto"), indiceBulto.ToString(), indice.ToString())
                        resultados.Add(resultado)
                        Exit For
                    End If

                    ' valida se o parâmetro CodPrecinto foi informado
                    If String.IsNullOrEmpty(bulto.CodPrecinto) Then
                        resultado.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                        resultado.MensajeError = String.Format(Traduzir("11_msg_atributo_vacio_documento_bulto"), "CodPrecinto", indiceBulto.ToString(), indice.ToString())
                        resultados.Add(resultado)
                        Exit For
                    End If

                    ' valida se o parâmetro CodUbicacion foi informado
                    If String.IsNullOrEmpty(bulto.CodUbicacion) Then
                        resultado.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                        resultado.MensajeError = String.Format(Traduzir("11_msg_atributo_vacio_documento_bulto"), "CodUbicacion", indiceBulto.ToString(), indice.ToString())
                        resultados.Add(resultado)
                        Exit For
                    End If

                    ' valida se o parâmetro IdBultoOrigen foi informado
                    If String.IsNullOrEmpty(bulto.IdBultoOrigen) Then
                        resultado.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                        resultado.MensajeError = String.Format(Traduzir("11_msg_atributo_vacio_documento_bulto"), "IdBultoOrigen", indiceBulto.ToString(), indice.ToString())
                        resultados.Add(resultado)
                        Exit For
                    End If

                Next

                ' valida se encontrou algum erro nas validações de bultos, 
                ' se sim, segue o loop para o próximo documento
                If resultado.CodigoError > 0 Then
                    Continue For
                End If

            End If

            ' valida se algum detalle foi especificado
            ' caso seja, tem que validar se foram informados todos os valores
            If doc.Detalles IsNot Nothing AndAlso doc.Detalles.Count > 0 Then

                Dim indiceDetalle As Integer = 0

                For Each detalle In doc.Detalles

                    indiceDetalle += 1

                    ' valida se o detalle é vazio
                    If detalle Is Nothing Then
                        resultado.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                        resultado.MensajeError = String.Format(Traduzir("11_msg_atributo_vacio_detalle"), indiceDetalle.ToString(), indice.ToString())
                        resultados.Add(resultado)
                        Exit For
                    End If

                    ' valida se o parâmetro IdEspecie foi informado
                    If String.IsNullOrEmpty(detalle.IdEspecie) Then
                        resultado.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                        resultado.MensajeError = String.Format(Traduzir("11_msg_atributo_vacio_documento_detalle"), "IdEspecie", indiceDetalle.ToString(), indice.ToString())
                        resultados.Add(resultado)
                        Exit For
                    End If

                    ' valida se o parâmetro IdMoneda foi informado
                    If String.IsNullOrEmpty(detalle.IdMoneda) Then
                        resultado.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                        resultado.MensajeError = String.Format(Traduzir("11_msg_atributo_vacio_documento_detalle"), "IdMoneda", indiceDetalle.ToString(), indice.ToString())
                        resultados.Add(resultado)
                        Exit For
                    End If

                Next

                ' valida se encontrou algum erro nas validações de detalles, 
                ' se sim, segue o loop para o próximo documento
                If resultado.CodigoError > 0 Then
                    Continue For
                End If

            End If

            ' valida se algum parcial foi especificado
            ' caso seja, tem que validar se foram informados todos os valores
            If doc.Parciales IsNot Nothing AndAlso doc.Parciales.Count > 0 Then

                Dim indiceParcial As Integer = 0

                For Each parcial In doc.Parciales

                    indiceParcial += 1

                    ' valida se o detalle é vazio
                    If parcial Is Nothing Then
                        resultado.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                        resultado.MensajeError = String.Format(Traduzir("11_msg_atributo_vacio_parcial"), indiceParcial.ToString(), indice.ToString())
                        resultados.Add(resultado)
                        Exit For
                    End If

                    ' valida se o parâmetro NumeroParcial foi informado
                    If String.IsNullOrEmpty(parcial.NumeroParcial) Then
                        resultado.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                        resultado.MensajeError = String.Format(Traduzir("11_msg_atributo_vacio_documento_parcial"), "NumeroParcial", indiceParcial.ToString(), indice.ToString())
                        resultados.Add(resultado)
                        Exit For
                    End If

                    ' valida se o parâmetro IdMoneda foi informado
                    If String.IsNullOrEmpty(parcial.IdMoneda) Then
                        resultado.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                        resultado.MensajeError = String.Format(Traduzir("11_msg_atributo_vacio_documento_parcial"), "IdMoneda", indiceParcial.ToString(), indice.ToString())
                        resultados.Add(resultado)
                        Exit For
                    End If

                Next

                ' valida se encontrou algum erro nas validações de detalles, 
                ' se sim, segue o loop para o próximo documento
                If resultado.CodigoError > 0 Then
                    Continue For
                End If

            End If

        Next

        ' verifica se existem objetos do tipo "Resultados", se sim, adiciona a coleção na resposta
        ' e retorna o método como false (indicando a existência de erros)
        If resultados IsNot Nothing AndAlso resultados.Count > 0 Then
            respuesta.Resultados = resultados
            Return False
        End If

        Return True

    End Function

#End Region

End Class